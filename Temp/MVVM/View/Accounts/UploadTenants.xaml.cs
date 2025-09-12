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

namespace FlexMold.MVVM.View.Accounts
{
    public partial class UploadTenants : Window
    {
        private DataTable currentDataTable = null;
        public UploadTenants()
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

            DataTable dataTable = null;
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
                    string fileExtension = Path.GetExtension(selectedFileName).ToLowerInvariant();

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

        private void InsertDataIntoSql(DataTable dataTable, string tableName)
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

        private DataTable ReadExcelData(string filePath, string sheetName, int headerRow, int dataStartRow)
        {
            DataTable dt = new DataTable();
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
                                    [RENT RECEIVED], DEPOSIT, [ARREAS PAID (RENT)], [ARREAS PAID(UTILITIES)],
                                    PREPAYMENT, [TOTAL MONEY PAID], [ARREAS B/F], ARREAS, [ARREAS C/F], 
                                    [BANK PAID], [EXTRA COMMENTS], [FILE PATH], UPLOADTIME, DATAFORMONTH
                                    FROM UploadsTenantRent 
                                    WHERE {arrearsCondition}";
                    else
                        query = $@"SELECT [UNIT NO], [TENANT NAME], [EXPECTED RENT], [WATER], SANITATION, 
                                    [RENT RECEIVED], DEPOSIT, [ARREAS PAID (RENT)], [ARREAS PAID(UTILITIES)],
                                    PREPAYMENT, [TOTAL MONEY PAID], [ARREAS B/F], ARREAS, [ARREAS C/F], 
                                    [BANK PAID], [EXTRA COMMENTS], [FILE PATH], UPLOADTIME, DATAFORMONTH
                                    FROM UploadsTenantRent 
                                    WHERE ([TENANT NAME] LIKE @SearchText 
                                    OR [UNIT NO] LIKE @SearchText 
                                    OR [DATAFORMONTH] LIKE @SearchText)
                                    AND {arrearsCondition}";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
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
                    DataTable dt = new DataTable();
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
            var textBox = sender as TextBox;
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

        private void TenantsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column is DataGridTextColumn textColumn)
            {
                textColumn.ElementStyle = this.FindResource("CenterTextStyle") as Style;
            }
        }
    }
}
