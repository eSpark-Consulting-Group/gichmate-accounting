using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class BankReconciliationView : UserControl
    {
        private readonly string connectionString;
        private DataTable _gridTable;

        public BankReconciliationView()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["PSVAT5"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Connection string 'PSVAT5' not found. Check App.config <connectionStrings>.");
                return;
            }

            if (!dpDate.SelectedDate.HasValue)
                dpDate.SelectedDate = DateTime.Today;

            btnLoad_Click(this, null); // initial load
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

                    string sql = @"
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
                            tl.VchrName IN ('Bank Payment', 'Bank Receipt')
                            {0}
                        ORDER BY tl.VchrDate, tl.VchrNo;";

                    if (dpDate.SelectedDate.HasValue)
                        sql = string.Format(sql, "AND CAST(tl.VchrDate AS DATE) = @SelectedDate");
                    else
                        sql = string.Format(sql, "");

                    using (var cmd = new SqlCommand(sql, con))
                    {
                        if (dpDate.SelectedDate.HasValue)
                            cmd.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = dpDate.SelectedDate.Value.Date;

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
                            dt.Rows[i]["BankDate"] = dt.Rows[i]["InvoiceDate"];
                        }

                        _gridTable = dt;
                        dgBankReconciliation.ItemsSource = _gridTable.DefaultView;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No data found for given date. Try removing the filter.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_gridTable == null || _gridTable.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to update. Load data first.");
                return;
            }

            dgBankReconciliation.CommitEdit(DataGridEditingUnit.Cell, true);
            dgBankReconciliation.CommitEdit(DataGridEditingUnit.Row, true);
            dgBankReconciliation.Items.Refresh();

            var rowsToUpdate = _gridTable.AsEnumerable()
                .Where(r => r.RowState == DataRowState.Modified && !r.IsNull("VchrId") && !r.IsNull("BankDate"))
                .ToList();

            if (rowsToUpdate.Count == 0)
            {
                MessageBox.Show("No changes detected to update.");
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

            MessageBox.Show($"Update finished. {totalUpdated} rows updated.\n" +
                            (failedInvoices.Count > 0 ? "Failed: " + string.Join(", ", failedInvoices) : ""));
        }

        private void BankReconciliationDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column is DataGridTextColumn textColumn)
            {
                textColumn.ElementStyle = this.FindResource("CenterTextStyle") as Style;
            }
        }
    }
}
