//using AccountsPRO.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Linq;
using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using AccountsLibrary.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Shapes;
using AccountsLibrary;
using System.Diagnostics.Metrics;
using Humanizer;
using Microsoft.Office.Interop.Excel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Eventing.Reader;
//using System.Windows.Forms;

namespace AccountsPRO.MVVM.View.Accounts
{
    /// <summary>
    /// Interaction logic for UploadTenantsUserControl.xaml
    /// </summary>
    public partial class UploadTenantsUserControl : System.Windows.Controls.UserControl
    {
        private System.Data.DataTable currentDataTable = null;

        public UploadTenantsUserControl()
        {
            InitializeComponent();
            FilterAccounts("");

            PopulateComboBox(UnitNoComboBox, "UNIT NO");
            PopulateComboBox(TenantNameComboBox, "TENANT NAME");
        }

        private void UploadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.License.SetNonCommercialPersonal("LocalTest");

            string tableName = "UploadsTenantRent";
            int headerRowNumber = 3, dataStartRowNumber = 4;

            System.Data.DataTable dataTable = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            try
            {
                openFileDialog.Title = "Select a File";
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFileName = openFileDialog.FileName;
                    string fileExtension = System.IO.Path.GetExtension(selectedFileName).ToLowerInvariant();

                    if (fileExtension == ".xlsx")
                        dataTable = ReadExcelData(selectedFileName, this.SheetTextBox.Text, headerRowNumber, dataStartRowNumber);
                    else
                        return;

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        currentDataTable = dataTable;
                        TenantsDataGrid.ItemsSource = currentDataTable.DefaultView;
                        InsertDataIntoSql(dataTable, tableName);

                        PopulateComboBox(UnitNoComboBox, "UNIT NO");
                        PopulateComboBox(TenantNameComboBox, "TENANT NAME");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        private void InsertDataIntoSql(System.Data.DataTable dataTable, string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(DATAFORMONTH) RecordsFound FROM UploadsTenantRent WHERE DATAFORMONTH='" +
                        dataTable.Rows[0]["DATAFORMONTH"].ToString() + "' ", conn))
                    {
                        try
                        {
                            int getRecs = int.Parse(cmd.ExecuteScalar().ToString());
                            if (getRecs > 0) return;
                        }
                        catch { return; }
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            if (!string.IsNullOrEmpty(column.ColumnName))
                            {
                                if (!column.ColumnName.ToLower().Contains("column"))
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                                else
                                {
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, "EXTRA COMMENTS"));
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("FILE PATH", "FILE PATH"));
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("UPLOADTIME", "UPLOADTIME"));
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DATAFORMONTH", "DATAFORMONTH"));
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data into SQL Server: {ex.Message}");
            }
        }

        private bool IsNumericColumn(string columnName)
        {
            return columnName == "EXPECTED RENT" ||
                   columnName == "WATER" ||
                   columnName == "SANITATION" ||
                   columnName == "RENT RECEIVED" ||
                   columnName == "DEPOSIT" ||
                   columnName == "DEPOSIT (arreas paid" ||
                   columnName == "ARREAS PAID(UTILITIES)" ||
                   columnName == "ARREAS PAID(RENT)" ||
                   columnName == "ARREAS PAID(garbage &water )" ||
                   columnName == "PREPAYMENT" ||
                   columnName == "TOTAL MONEY PAID" ||
                   columnName == "ARREAS B/F" ||
                   columnName == "ARREAS" ||
                   columnName == "ARREAS C/F";
        }

        private System.Data.DataTable ReadExcelData(string filePath, string sheetName, int headerRow, int dataStartRow)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                Console.WriteLine($"Error: Excel file not found at {filePath}");
                return null;
            }

            try
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet;
                    try
                    {
                        worksheet = package.Workbook.Worksheets[sheetName];
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine($"Error: Sheet '{sheetName}' not found in the Excel file. Please check the sheet name.");
                        return null;
                    }

                    if (worksheet == null)
                    {
                        Console.WriteLine($"Error: Sheet '{sheetName}' not found or is empty.");
                        return null;
                    }

                    bool extraCommentsOccured = false;
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        string header = worksheet.Cells[headerRow, col].Text.Trim();
                        if (string.IsNullOrEmpty(header))
                        {
                            header = $"Column{col}";
                            extraCommentsOccured = true;
                        }
                        if (dt.Columns.Contains(header))
                        {
                            int i = 1;
                            string originalHeader = header;
                            while (dt.Columns.Contains(header))
                            {
                                header = $"{originalHeader}{i++}";
                            }
                        }
                        dt.Columns.Add(header);
                        if (extraCommentsOccured)
                            break;
                    }

                    dt.Columns.Add("FILE PATH");
                    dt.Columns.Add("UPLOADTIME");
                    dt.Columns.Add("DATAFORMONTH");


                    bool breakPri = false;

                    for (int row = dataStartRow; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (breakPri)
                            break;

                        DataRow dr = dt.NewRow();
                        for (int col = 1; col <= worksheet.Dimension.End.Column + 3; col++)//+3 we added 3 cols
                        {
                            try
                            {
                                string headerName = dt.Columns[col - 1].ColumnName;
                                object cellValue = worksheet.Cells[row, col].Value;
                                string cellText = (cellValue != null) ? cellValue.ToString().Trim() : string.Empty;

                                if (headerName.Contains("FILE PATH", StringComparison.OrdinalIgnoreCase))
                                {
                                    dr[col - 1] = filePath;
                                    continue;
                                }
                                if (headerName.Contains("UPLOADTIME", StringComparison.OrdinalIgnoreCase))
                                { dr[col - 1] = DateTime.Now; continue; }


                                // ********************************************************************************************//

                                if (headerName.Contains("DATAFORMONTH", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (DateTimePicker1.SelectedDate.HasValue)
                                        dr[col - 1] = DateTimePicker1.SelectedDate.Value.ToString("MMM yyyy");
                                    else
                                        dr[col - 1] = string.Empty;

                                    continue;
                                }




                                if (headerName.Contains("TENANT", StringComparison.OrdinalIgnoreCase) && (((string)cellValue).Contains("Total", StringComparison.OrdinalIgnoreCase) || cellText.Contains("Total", StringComparison.OrdinalIgnoreCase)))
                                { breakPri = true; break; }

                                if (IsNumericColumn(headerName))
                                {
                                    decimal parsedValue;

                                    if (decimal.TryParse(cellText, NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValue))
                                    {
                                        dr[col - 1] = parsedValue;
                                    }
                                    else
                                    {

                                        dr[col - 1] = DBNull.Value;
                                    }
                                }
                                else
                                {
                                    dr[col - 1] = cellText;
                                }
                            }
                            catch { }
                        }
                        if (dr[0] != null && dr[0].ToString().Length > 1)
                            dt.Rows.Add(dr);
                        else
                        { //Console.WriteLine($"Gotcha!!!"); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Excel data: {ex.Message}");
                //Console.WriteLine($"Details: {ex.ToString()}");
                return null;
            }

            return dt;
        }


        private void FilterAccounts(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string query = "";

                    string arrearsFilter = ArreasTxtBox.Text.Trim();
                    string arrearsCondition = "";

                    if (decimal.TryParse(arrearsFilter, out decimal arrearsValue))
                    {
                        if (arrearsValue == 0)
                            arrearsCondition = "[ARREAS] >= 0";
                        else
                            arrearsCondition = "[ARREAS] >= " + arrearsValue;
                    }
                    else
                    {
                        arrearsCondition = "1=1";
                    }

                    if (string.IsNullOrEmpty(searchText))
                        query = $@"SELECT [UNIT NO], [TENANT NAME], [EXPECTED RENT], [WATER], SANITATION, 
                                    [RENT RECEIVED], DEPOSIT, [ARREAS PAID (RENT)] AS [ARREARS PAID (RENT)], [ARREAS PAID(UTILITIES)] AS [ARREARS PAID(UTILITIES)],
                                    PREPAYMENT, [TOTAL MONEY PAID], [ARREAS B/F] AS [ARREARS BF], ARREAS AS ARREARS, [ARREAS C/F] AS [ARREARS C/F], 
                                    [BANK PAID], [EXTRA COMMENTS], [FILE PATH], UPLOADTIME, DATAFORMONTH
                                    FROM UploadsTenantRent 
                                    WHERE {arrearsCondition}";
                    else
                        query = $@"SELECT [UNIT NO], [TENANT NAME], [EXPECTED RENT], [WATER], SANITATION, 
                                    [RENT RECEIVED], DEPOSIT, [ARREAS PAID (RENT)] AS [ARREARS PAID (RENT)], [ARREAS PAID(UTILITIES)] AS [ARREARS PAID(UTILITIES)],
                                    PREPAYMENT, [TOTAL MONEY PAID], [ARREAS B/F] AS [ARREARS BF], ARREAS AS ARREARS, [ARREAS C/F] AS [ARREARS C/F], 
                                    [BANK PAID], [EXTRA COMMENTS], [FILE PATH], UPLOADTIME, DATAFORMONTH
                                    FROM UploadsTenantRent 
                                    WHERE ([TENANT NAME] LIKE @SearchText 
                                    OR [UNIT NO] LIKE @SearchText 
                                    OR [DATAFORMONTH] LIKE @SearchText)
                                    AND {arrearsCondition}";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    TenantsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching accounts: {ex.Message}");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAccounts(SearchTextBox.Text);
        }

        private void PopulateComboBox(ComboBox comboBox, string columnName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string query = $@"SELECT DISTINCT [{columnName}] 
                                      FROM UploadsTenantRent 
                                      WHERE [{columnName}] IS NOT NULL 
                                      ORDER BY [{columnName}]";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    comboBox.ItemsSource = dt.DefaultView;
                    comboBox.DisplayMemberPath = columnName;
                    comboBox.SelectedValuePath = columnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating {comboBox.Name}: {ex.Message}");
            }
        }

        private void UnitNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitNoComboBox.SelectedValue != null)
                FilterAccounts(UnitNoComboBox.SelectedValue.ToString());
        }

        private void TenantNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TenantNameComboBox.SelectedValue != null)
                FilterAccounts(TenantNameComboBox.SelectedValue.ToString());
        }

        private void ArreasTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox == null) return;

            if (!textBox.Text.All(char.IsDigit))
            {
                int caretIndex = textBox.CaretIndex - 1;
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                textBox.CaretIndex = Math.Max(caretIndex, 0);
            }

            FilterAccounts(SearchTextBox.Text); // keep your filter call
        }

        private void ClearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            ArreasTxtBox.Text = string.Empty;

            UnitNoComboBox.SelectedIndex = -1;
            TenantNameComboBox.SelectedIndex = -1;

            FilterAccounts("");
        }

        private void LoadReconciliationBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");

            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string whereClause = " WHERE t.DATAFORMONTH = (SELECT LEFT(DATENAME(MONTH, CONVERT(date, '08/07/2025', 103)), 3) + ' ' + CAST(YEAR(CONVERT(date, '08/07/2025', 103)) AS VARCHAR(4)) AS[MonthYear]) ";
                    string query = " SELECT r.[Transaction Details],r.[Payment reference],r.[Value Date],r.[Credit (Money In)],r.[Debit (Money Out)],r.[Balance],r.[UTR_ID],t.UTR_ID AS MAPPEDUTR,t.[TOTAL MONEY PAID],t.DATAFORMONTH,(SELECT LEFT(DATENAME(MONTH, CONVERT(date, '08/07/2025', 103)), 3) + ' ' + CAST(YEAR(CONVERT(date, '08/07/2025', 103)) AS VARCHAR(4)) AS[MonthYear]) AS convDATAFORMONTH " +
                                    " FROM UploadBankReceipts r " +
                                    " JOIN UploadsTenantRent t " +
                                        " ON r.[Transaction Details] LIKE '%' + REPLACE(t.[TENANT NAME], ' ', '%') + '%' ";
                    if ((bool)LoadTempchk.IsChecked)
                        query += " UNION ALL select *,NULL AS MAPPEDUTR,NULL AS[TOTAL MONEY PAID],'' AS DATAFORMONTH,(SELECT LEFT(DATENAME(MONTH, CONVERT(date, '08/07/2025', 103)), 3) + ' ' + CAST(YEAR(CONVERT(date, '08/07/2025', 103)) AS VARCHAR(4)) AS[MonthYear]) AS convDATAFORMONTH from UploadBankReceipts where [Transaction Details] LIKE '%BY:%' ORDER BY r.[Payment reference]  desc ";
                    else
                        query += whereClause + " UNION ALL select *,NULL AS MAPPEDUTR,NULL AS[TOTAL MONEY PAID],'' AS DATAFORMONTH,(SELECT LEFT(DATENAME(MONTH, CONVERT(date, '08/07/2025', 103)), 3) + ' ' + CAST(YEAR(CONVERT(date, '08/07/2025', 103)) AS VARCHAR(4)) AS[MonthYear]) AS convDATAFORMONTH from UploadBankReceipts where [Transaction Details] LIKE '%BY:%' ORDER BY r.[Payment reference]  desc ";





                        //Then new query required for each unit
                        //PAYED BY:HSE.36/111758384273/10-07-2025 17:52:12

                        //SELECT SUM(TRY_CAST(REPLACE([Debit (Money Out)], ',', '') AS DECIMAL(18,2))) AS TotalAmount
                        //FROM UploadBankReceipts where[Transaction Details] like '%BENARD MUNDI%'

                        SqlCommand cmd = new SqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("@SearchText", "%All%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    dgBankReconciliation.ItemsSource = dt.DefaultView;
                }

                //foreach (DataRow row_UploadBankReceipts in dt.Rows)
                //{
                //    if (DateTime.TryParse(Convert.ToString(row_UploadBankReceipts["Value Date"]), out var start))
                //    {
                //        //string sqlINS = @"INSERT INTO TenantsUnitRelation ([UNIT NO],[TENANT NAME],[TENANT ISACTIVE],[UPDATETIME]) VALUES (@UnitNo,@TenantName,1,@dtNow)";
                //        //using (SqlCommand cmd = new SqlCommand(sqlINS, conn))
                //        //{
                //        //    cmd.Parameters.AddWithValue("@UnitNo", dr["UNIT NO"].ToString());
                //        //    cmd.Parameters.AddWithValue("@TenantName", dr["TENANT NAME"].ToString());
                //        //    cmd.Parameters.AddWithValue("@dtNow", DateTime.Now.ToString());
                //        //    cmd.ExecuteNonQuery();
                //        //    counter++;
                //        //}
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching accounts: {ex.Message}");
            }

        }
    }
}