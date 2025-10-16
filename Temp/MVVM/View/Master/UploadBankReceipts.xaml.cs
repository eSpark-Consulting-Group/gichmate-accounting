using AccountsLibrary.Properties;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using Spire.Pdf.Texts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPRO.MVVM.View.Master
{
    /// <summary>
    /// Interaction logic for UploadBankReceipts.xaml
    /// </summary>
    public partial class UploadBankReceipts : Window
    {
        private DataTable dt = new DataTable();

        public UploadBankReceipts()
        {
            InitializeComponent();
            LoadDataFromDatabase();   
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                dt.Clear();
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();
                    string query = "SELECT [Transaction Details], [Payment Reference], [Value Date], [Credit (Money In)], [Debit (Money Out)], Balance, UTR_ID FROM UploadBankReceipts WHERE UTR_ID IS NULL";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //dt.Columns.Add("IsSelected", Type.GetType("CheckBox"));
                        dt.Columns.Add("IsSelected", typeof(bool));

                        BankReceiptsDataGrid.ItemsSource = dt.DefaultView;
                        AddTotalRow(true); //the true is the reason total row would appear the the top btw

                        MessageBox.Show($"Rows in DT: {dt.Rows.Count}");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void LoadFileBtn_Click(object sender, RoutedEventArgs e)
        {


            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                openFileDialog.Title = "Select a File";
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFileName = openFileDialog.FileName;
                    doc.LoadFromFile(selectedFileName);
                }
                else return;

            }
            catch { }
            
            //List<UploadBankReceiptsObj> myList = new List<UploadBankReceiptsObj>();
            //UploadBankReceiptsObj myObj = new UploadBankReceiptsObj();
            System.Data.DataTable dataTable = new System.Data.DataTable("UploadBankReceipts");
            dataTable.Columns.Add("Transaction Details");
            dataTable.Columns.Add("Payment reference");
            dataTable.Columns.Add("Value Date");
            dataTable.Columns.Add("Credit(Money In)");
            dataTable.Columns.Add("Debit(Money Out)");
            dataTable.Columns.Add("Balance");


            for (int i = 0; i <= doc.Pages.Count - 1; i++)
            {
                //Create a PdfTextExtractot object
                PdfTextExtractor textExtractor = new PdfTextExtractor(doc.Pages[i]);
                //Create a PdfTextExtractOptions object
                PdfTextExtractOptions extractOptions = new PdfTextExtractOptions();
                //Set isExtractAllText to true
                extractOptions.IsExtractAllText = true;
                //Read text from the PDF page
                string text = textExtractor.ExtractText(extractOptions);
                Regex r = new Regex(@"[\r\n]*.*[\r\n]*.*[\r\n]*.*", RegexOptions.IgnoreCase);//\n\n.*\n\n.*\n\n.*
                String textUpdated = "";
                try { textUpdated = text.Remove(0, (text.IndexOf("Debit (Money Out)         Balance") + 33)); } catch { }
                // Match the regular expression pattern against a text string.
                Match m = r.Match(textUpdated);
                int matchCount = 0;
                while (m.Success)
                {
                    if (m.Groups[0].Value.Contains("Debit (Money Out)         Balance") || m.Groups[0].Value.Contains("Disclaimer:") || m.Groups[0].Value.Length < 20)
                    { m = m.NextMatch(); continue; }
                    Console.WriteLine("Match" + (++matchCount));
                    //for (int i = 1; i <= 2; i++)
                    //{
                    //    Console.WriteLine("******************************************");
                    Console.WriteLine(m.Groups[0].Value);
                    String sst = m.Groups[0].Value.Trim();
                    Console.WriteLine(string.Join(Environment.NewLine, Regex.Split(sst, "\\s\\s\\s")));
                    //foreach (String st in Regex.Split(sst, "\\s\\s\\s*")) {
                    //[0]	"MPS 254797636391 TG113XGRQ1 Jane wangui mwangi 024"    string
                    //[1] "r83Ryn7JmIzF"  string
                    //[2] "S31378140" string
                    //[3] "01/07/2025"    string
                    //[4] "40,800.00" string
                    //[5] "309,102.19"    string
                    //[6] "0240192004109" string
                    //myObj = new UploadBankReceiptsObj();
                    String[] st = Regex.Split(sst, "\\s\\s\\s");
                    //myObj.TransactionDetails = st[0].ToString();
                    //myObj.PaymentReference = st[1].ToString() + "---" + st[2].ToString();
                    //myObj.ValueDate = st[3].ToString();
                    //myObj.Credit_MoneyIn = st[4].ToString();
                    //myObj.Debit_MoneyOut = st[5].ToString();
                    //myObj.Balance = st[6].ToString();
                    //myList.Add(myObj);
                    //}

                    //26/08/2025     Page 5/20
                    if (sst.Contains("Page "))
                    { m = m.NextMatch(); continue; }

                    DataRow dr = dataTable.NewRow();
                    ArrayList al = new ArrayList();
                    
                    foreach (String str in st)
                    {
                        if (str.Length > 3)
                            al.Add(str);
                    }

                    //if(sst.Contains("APP/RODAH KEMUNTO MORAA OSUMO"))
                    //    Console.WriteLine("");

                    DateTime chkRecs = DateTime.Now;
                    int dateloc = 1;
                    if (!DateTime.TryParse(al[dateloc].ToString().Trim(), out chkRecs))
                    {
                        dateloc = 2;
                        if (!DateTime.TryParse(al[dateloc].ToString().Trim(), out chkRecs))
                        { dateloc = 3;
                            if (!DateTime.TryParse(al[dateloc].ToString().Trim(), out chkRecs))
                                dateloc = 4;
                        }
                    }

                    if (dateloc == 1)//if (sst.Contains("BY:"))
                    {
                        //           PAYED BY:HSE - 23 / 250413289208 / 01 - 07 - 2025 19:33:36

                        //                                                                              54556384                  01 / 07 / 2025                   13,650.00                              313,479.20
                        //250413289208
                        try
                        {
                            dr[0] = al[0].ToString().Trim();//tran details
                            dr[1] = al[5].ToString().Trim();// + "---" + st[2].ToString(); // payment ref
                            dr[2] = al[2].ToString().Trim();//val date
                                                            //*****************dr[44] and dr[45] and dr[46] must not be null for confirm debit values present
                            if ((st[48].Length > 3 && st[49].Length > 3) || (st[49].Length > 3 && st[50].Length > 3) || (st[50].Length > 3 && st[51].Length > 3))
                            { dr[3] = "0"; dr[4] = al[3].ToString().Trim(); }//debit
                            else
                            { dr[3] = al[3].ToString().Trim(); dr[4] = "0"; }//credit

                            dr[5] = al[4].ToString().Trim();//balance
                            dataTable.Rows.Add(dr);
                        }
                        catch { }
                    }
                    else if (dateloc==2)//if (sst.Contains("BY:"))
                    {
                        //           PAYED BY:HSE - 23 / 250413289208 / 01 - 07 - 2025 19:33:36

                        //                                                                              54556384                  01 / 07 / 2025                   13,650.00                              313,479.20
                        //250413289208
                       try
                        {
                            dr[0] = al[0].ToString().Trim();//tran details
                            dr[1] = al[5].ToString().Trim();// + "---" + st[2].ToString(); // payment ref
                            dr[2] = al[2].ToString().Trim();//val date
                                                            //*****************dr[44] and dr[45] and dr[46] must not be null for confirm debit values present
                            if ((st[48].Length > 3 && st[49].Length > 3) || (st[49].Length > 3 && st[50].Length > 3) || (st[50].Length > 3 && st[51].Length > 3))
                            { dr[3] = "0"; dr[4] = al[3].ToString().Trim(); }//debit
                            else
                            { dr[3] = al[3].ToString().Trim(); dr[4] = "0"; }//credit

                            dr[5] = al[4].ToString().Trim();//balance
                            dataTable.Rows.Add(dr);
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            dr[0] = al[0].ToString().Trim();//tran details
                            dr[1] = al[2].ToString().Trim();// + "---" + st[2].ToString(); // payment ref
                            dr[2] = al[dateloc].ToString().Trim();//val date
                                                            //*****************dr[44] and dr[45] and dr[46] must not be null for confirm debit values present
                            if ((st[43].Length > 3 && st[44].Length > 3) || (st[44].Length > 3 && st[45].Length > 3) || (st[45].Length > 3 && st[46].Length > 3))
                            { dr[3] = "0"; dr[dateloc+1] = al[4].ToString().Trim(); }//debit
                            else
                            { dr[3] = al[dateloc + 1].ToString().Trim(); dr[4] = "0"; }//credit

                            dr[5] = al[dateloc + 2].ToString().Trim();//balance
                            dataTable.Rows.Add(dr);
                        }
                        catch { }
                    }
                    dr = dataTable.NewRow();

                    m = m.NextMatch();
                }
                using (SqlConnection connection = new SqlConnection(Settings.Default.strConnetionString))
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "UploadBankReceipts";
                        //foreach (DataColumn column in dataTable.Columns)
                        //{
                        //    //if (!string.IsNullOrEmpty(column.ColumnName))//&& !column.ColumnName.StartsWith("Column") && !string.IsNullOrWhiteSpace(column.ColumnName)
                        //    //{
                        //    //    if (!column.ColumnName.ToLower().Contains("column"))
                        //    //        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                        //    //    else
                        //    //    { //column16 and column17 is here for comments
                        //    //        {
                        //    //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, "EXTRA COMMENTS"));
                        //    //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("FILE PATH", "FILE PATH"));
                        //    //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("UPLOADTIME", "UPLOADTIME"));
                        //    //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DATAFORMONTH", "DATAFORMONTH"));
                        //    //            break;
                        //    //        }
                        //    //    }
                        //    //    //Console.WriteLine($"Mapping column: {column.ColumnName}");
                        //    //}
                        //    //else
                        //    //{
                        //    //    Console.WriteLine($"Skipping empty or generic column in DataTable: '{column.ColumnName}'");
                        //    //}
                        //}
                        try
                        {
                            bulkCopy.WriteToServer(dataTable);
                            //Console.WriteLine("Data successfully imported from file to SQL Server!");
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"SQL Bulk Copy Error: {ex.Message}");
                            //Console.WriteLine($"Details: {ex.ToString()}");
                        }
                    }
                }
            }

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAccounts(SearchTextBox.Text);
        }

        private void FilterAccounts(string searchText)
        {
            try
            {
                if (BankReceiptsDataGrid.ItemsSource is DataView view)
                {
                    if (string.IsNullOrWhiteSpace(searchText))
                    {
                        view.RowFilter = "";
                        return;
                    }

                    string text = searchText.Replace("'", "''"); 

                    // to update the values in rowfilter by reconcile button
                    //on click check check boxes in datagrid

                    view.RowFilter = string.Format(
                        "CONVERT([Transaction Details], 'System.String') LIKE '%{0}%' OR " +
                        "CONVERT([Payment Reference], 'System.String') LIKE '%{0}%' OR " +
                        "CONVERT([Value Date], 'System.String') LIKE '%{0}%' OR " +
                        "CONVERT([Credit (Money In)], 'System.String') LIKE '%{0}%' OR " +
                        "CONVERT([Debit (Money Out)], 'System.String') LIKE '%{0}%' OR " +
                        "CONVERT([Balance], 'System.String') LIKE '%{0}%'",
                        text
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while filtering: " + ex.Message);
            }
        }

        //reconcile records
        private void ReconcileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data available to reconcile.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    int updatedCount = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (dt.Columns.Contains("IsSelected") &&
                            row["IsSelected"] != DBNull.Value &&
                            Convert.ToBoolean(row["IsSelected"]))
                        {
                            string paymentRef = row["Payment Reference"]?.ToString()?.Trim() ?? "";
                            string valueDate = row["Value Date"]?.ToString()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(paymentRef) || string.IsNullOrEmpty(valueDate))
                                continue;

                            string query = @"
                        UPDATE TOP (1) UploadBankReceipts
                        SET UTR_ID = 0
                        WHERE UTR_ID IS NULL
                          AND [Payment Reference] = @PaymentRef
                          AND [Value Date] = @ValueDate";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.Parameters.AddWithValue("@PaymentRef", paymentRef);
                                cmd.Parameters.AddWithValue("@ValueDate", valueDate);
                                updatedCount += cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Reload table after reconciliation
                    string reloadQuery = "SELECT [Transaction Details], [Payment Reference], [Value Date], [Credit (Money In)], [Debit (Money Out)], Balance, UTR_ID FROM UploadBankReceipts WHERE UTR_ID IS NULL";
                    SqlDataAdapter da = new SqlDataAdapter(reloadQuery, con);
                    dt.Clear();
                    da.Fill(dt);

                    if (!dt.Columns.Contains("IsSelected"))
                        dt.Columns.Add("IsSelected", typeof(bool));

                    BankReceiptsDataGrid.ItemsSource = dt.DefaultView;
                    AddTotalRow(true);

                    MessageBox.Show($"{updatedCount} record(s) reconciled successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Reconciling: " + ex.Message);
            }
        }

        //this will add total rows in the column fields
        private void AddTotalRow(bool showOnTop = false)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                    return;

                //Remove theold TOTAL row if exists
                foreach (DataRow row in dt.Select("[Transaction Details] = 'TOTAL'"))
                    dt.Rows.Remove(row);

                // Compute totals
                double totalCredit = 0, totalDebit = 0, totalBalance = 0;
                foreach (DataRow row in dt.Rows)
                {
                    double.TryParse(row["Credit (Money In)"]?.ToString().Replace(",", ""), out double credit);
                    double.TryParse(row["Debit (Money Out)"]?.ToString().Replace(",", ""), out double debit);
                    double.TryParse(row["Balance"]?.ToString().Replace(",", ""), out double balance);

                    totalCredit += credit;
                    totalDebit += debit;
                    totalBalance += balance;
                }

                // Create total row
                DataRow totalRow = dt.NewRow();
                totalRow["Transaction Details"] = "TOTAL";
                totalRow["Credit (Money In)"] = totalCredit.ToString("N2");
                totalRow["Debit (Money Out)"] = totalDebit.ToString("N2");
                totalRow["Balance"] = totalBalance.ToString("N2");

                //Add to top or bottom 
                if (showOnTop)
                    dt.Rows.InsertAt(totalRow, 0);
                else
                    dt.Rows.Add(totalRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding total row: " + ex.Message);
            }
        }


    }
}
