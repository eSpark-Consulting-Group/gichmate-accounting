using FlexMold.MVVM.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using Spire.Pdf.Texts;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    /// <summary>
    /// Interaction logic for UploadBankReceipts.xaml
    /// </summary>
    public partial class UploadBankReceipts : Window
    {
        private string connectionString = "Server=DESKTOP-E09TG4T;Database=PSVAT5;Trusted_Connection=True; TrustServerCertificate=True;";

        public UploadBankReceipts()
        {
            InitializeComponent();
            LoadDataFromDatabase();   // ✅ load data by default when window opens
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT [Transaction Details], [Payment Reference], [Value Date], [Credit (Money In)], [Debit (Money Out)], Balance FROM UploadBankReceipts";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        BankReceiptsDataGrid.ItemsSource = dt.DefaultView;
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
                    Console.WriteLine(string.Join(Environment.NewLine, Regex.Split(sst, "\\s\\s\\s*")));
                    //foreach (String st in Regex.Split(sst, "\\s\\s\\s*")) {
                    //[0]	"MPS 254797636391 TG113XGRQ1 Jane wangui mwangi 024"    string
                    //[1] "r83Ryn7JmIzF"  string
                    //[2] "S31378140" string
                    //[3] "01/07/2025"    string
                    //[4] "40,800.00" string
                    //[5] "309,102.19"    string
                    //[6] "0240192004109" string
                    //myObj = new UploadBankReceiptsObj();
                    String[] st = Regex.Split(sst, "\\s\\s\\s*");
                    //myObj.TransactionDetails = st[0].ToString();
                    //myObj.PaymentReference = st[1].ToString() + "---" + st[2].ToString();
                    //myObj.ValueDate = st[3].ToString();
                    //myObj.Credit_MoneyIn = st[4].ToString();
                    //myObj.Debit_MoneyOut = st[5].ToString();
                    //myObj.Balance = st[6].ToString();
                    //myList.Add(myObj);
                    //}
                    DataRow dr = dataTable.NewRow();
                    try
                    {
                        dr[0] = st[0].ToString();
                        dr[1] = st[1].ToString() + "---" + st[2].ToString();
                        dr[2] = st[2].ToString();
                        dr[3] = st[3].ToString();
                        dr[4] = st[4].ToString();
                        dr[5] = st[5].ToString();
                        dataTable.Rows.Add(dr);
                    }
                    catch { }
                    m = m.NextMatch();
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
