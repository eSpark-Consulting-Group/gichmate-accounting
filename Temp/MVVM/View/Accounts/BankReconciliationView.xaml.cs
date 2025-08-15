using ControlzEx.Standard;
using Humanizer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static ServiceStack.Diagnostics.Events;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class BankReconciliationView : System.Windows.Controls.UserControl
    {
        private readonly string connectionString;
        private DataTable _gridTable;


        public BankReconciliationView()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["PSVAT5"]?.ConnectionString; // App.config has connection string with database!

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                System.Windows.MessageBox.Show("Connection string 'PSVAT5' not found. Check App.config <connectionStrings>.");
            }
            
            if (!dpDate.SelectedDate.HasValue)
                dpDate.SelectedDate = DateTime.Today;

            try
            {
                btnLoad_Click(this, null);
            }
            catch
            {
               
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql;
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        if (dpDate.SelectedDate.HasValue)
                        {                 
                            sql = @"
                                    SELECT 
                                        tl.VchrNo          AS InvoiceNo,
                                        tl.VchrType        AS InvoiceType,
                                        acc.AccountName    AS Particulars,
                                        tl.VchrDate        AS InvoiceDate,
                                        tl.DebitAmount     AS Debit,
                                        tl.CreditAmount    AS Credit,
                                        tl.Narration       AS Remarks,
                                        tl.VchrId,
                                        tl.VchrType        AS VchrTypeRaw,
                                        tl.VchrName,
                                        tl.TranId
                                    FROM dbo.TransactionLog tl
                                    LEFT JOIN dbo.Account acc ON acc.AccountId = tl.OppAccountId
                                    WHERE 
                                        tl.Status = 'Posted' 
                                        AND tl.VchrName IN ('Bank Payment', 'Bank Receipt')  
                                        AND CAST(tl.VchrDate AS DATE) = @SelectedDate
                                    ORDER BY tl.VchrDate, tl.VchrNo;";
                            cmd.CommandText = sql;
                            cmd.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = dpDate.SelectedDate.Value.Date;
                        }
                        else
                        {
                            // No date selected → show all data
                            sql = @"
                                    SELECT 
                                        tl.VchrNo          AS InvoiceNo,
                                        tl.VchrType        AS InvoiceType,
                                        acc.AccountName    AS Particulars,
                                        tl.VchrDate        AS InvoiceDate,
                                        tl.DebitAmount     AS Debit,
                                        tl.CreditAmount    AS Credit,
                                        tl.Narration       AS Remarks,
                                        tl.VchrId,
                                        tl.VchrType        AS VchrTypeRaw,
                                        tl.VchrName,
                                        tl.TranId
                                    FROM dbo.TransactionLog tl
                                    LEFT JOIN dbo.Account acc ON acc.AccountId = tl.OppAccountId
                                    WHERE 
                                        tl.Status = 'Posted' 
                                        AND tl.VchrName IN ('Bank Payment', 'Bank Receipt')  
                                    ORDER BY tl.VchrDate, tl.VchrNo;";
                            cmd.CommandText = sql;
                        }

                        //Shedding some light on the above 2 SQL Queries,
                        //The first SQL query is there to show data when the user has selected date. It will only show the record of that particular date.
                        //While, the second one is there to show all the data when no date has been selected, this was done while keeping in mind
                        //that the client might have large data,
                        //and correctly guessing the date won't be appropriate and would easily waste quite some time, hence
                        //by letting him see all the available record beforehand, it'lll be easier to select and see the date of a certain date after exposure...

                        var da = new SqlDataAdapter(cmd);
                        var dt = new DataTable();
                        da.Fill(dt);

                   
                        if (!dt.Columns.Contains("SL"))
                            dt.Columns.Add("SL", typeof(int));
                        if (!dt.Columns.Contains("BankDate"))
                            dt.Columns.Add("BankDate", typeof(DateTime));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["SL"] = i + 1;
                            if (dt.Rows[i]["InvoiceDate"] != DBNull.Value)
                                dt.Rows[i]["BankDate"] = dt.Rows[i]["InvoiceDate"];
                            else
                                dt.Rows[i]["BankDate"] = DBNull.Value;
                        }                        
                        _gridTable = dt;
                        
                        dgBankReconciliation.ItemsSource = _gridTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error loading data: " + ex.Message);
              
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_gridTable == null || _gridTable.Rows.Count == 0)
            {
                System.Windows.MessageBox.Show("Nothing to update. Load data first.");
                return;
            }

            dgBankReconciliation.CommitEdit(DataGridEditingUnit.Cell, true);
            dgBankReconciliation.CommitEdit(DataGridEditingUnit.Row, true);
            dgBankReconciliation.Items.Refresh();

            var rowsToUpdate = _gridTable.AsEnumerable()
                .Where(r => !r.IsNull("VchrId") && !r.IsNull("BankDate") &&
                       r.Field<DateTime>("BankDate") != r.Field<DateTime>("BankDate", DataRowVersion.Original))
                .ToList();

            if (rowsToUpdate.Count == 0)
            {
                System.Windows.MessageBox.Show("No changes detected to update.");
                return;
            }

            int totalUpdated = 0;
            var failedInvoices = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    foreach (var r in rowsToUpdate)
                    {
                        int vchrId = r.Field<int>("VchrId");
                        DateTime bankDate = r.Field<DateTime>("BankDate");

                        try
                        {
                            // Update VchrTranLog
                            using (var cmd1 = new SqlCommand(
                                @"UPDATE dbo.VchrTranLog 
                          SET VchrDate = @BankDate
                          WHERE VchrTranId = @VchrId;", con, tx))
                            {
                                cmd1.Parameters.AddWithValue("@BankDate", bankDate);
                                cmd1.Parameters.AddWithValue("@VchrId", vchrId);
                                cmd1.ExecuteNonQuery();
                            }

                            // Update TransactionLog
                            using (var cmd2 = new SqlCommand(
                                @"UPDATE dbo.TransactionLog 
                          SET VchrDate = @BankDate
                          WHERE VchrId = @VchrId;", con, tx))
                            {
                                cmd2.Parameters.AddWithValue("@BankDate", bankDate);
                                cmd2.Parameters.AddWithValue("@VchrId", vchrId);
                                int affected = cmd2.ExecuteNonQuery();
                                if (affected == 0)
                                    failedInvoices.Add($"VchrId={vchrId}");
                                else
                                {
                                    totalUpdated += affected;
                                    r.AcceptChanges();
                                }
                            }
                        }
                        catch
                        {
                            failedInvoices.Add($"VchrId={vchrId}");
                        }
                    }

                    tx.Commit();
                }
            }
            System.Windows.MessageBox.Show($"Update finished. {totalUpdated} rows updated.\n" +
                            (failedInvoices.Count > 0 ? "Failed: " + string.Join(", ", failedInvoices) : ""));
        }
    }
}

