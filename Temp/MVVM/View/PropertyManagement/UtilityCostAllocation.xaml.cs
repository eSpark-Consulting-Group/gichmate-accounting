using AccountsLibrary.Properties;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using OfficeOpenXml;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class UtilityCostAllocation : UserControl
    {
        private bool _suppressSelectionChanged = false;

        public UtilityCostAllocation()
        {
            InitializeComponent();
            Loaded += UtilityCostAllocation_Loaded;

            MonthsComboBox.SelectionChanged += Filter_Changed;
            UnitNoComboBox.SelectionChanged += Filter_Changed;
            TenantsDataGrid.SelectionChanged += TenantsDataGrid_SelectionChanged;

            ExpectedRentTextBox.TextChanged += Field_TextChanged;
            WaterCostTextBox.TextChanged += Field_TextChanged;
            SanitationCostTextBox.TextChanged += Field_TextChanged;
        }

        private void UtilityCostAllocation_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonths();
            PopulateUnits();
            LoadTenantsData();
        }

        private void PopulateMonths()
        {
            try
            {
                MonthsComboBox.Items.Clear();
                MonthsComboBox.Items.Add("All");
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = @"SELECT DISTINCT 
                                    DATEPART(YEAR, TRY_CONVERT(date, DATAFORMONTH)) AS Yr,
                                    DATEPART(MONTH, TRY_CONVERT(date, DATAFORMONTH)) AS Mn
                                 FROM UploadsTenantRent
                                 WHERE TRY_CONVERT(date, DATAFORMONTH) IS NOT NULL
                                 ORDER BY Yr DESC, Mn DESC";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int yr = rdr.GetInt32(0);
                            int mn = rdr.GetInt32(1);
                            MonthsComboBox.Items.Add(new DateTime(yr, mn, 1).ToString("MMMM yyyy", CultureInfo.InvariantCulture));
                        }
                    }
                }
                MonthsComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading months: {ex.Message}");
            }
        }

        private void PopulateUnits()
        {
            try
            {
                var units = new List<string> { "All" };
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = "SELECT DISTINCT [UNIT NO] FROM UploadsTenantRent ORDER BY [UNIT NO]";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            if (!rdr.IsDBNull(0)) units.Add(rdr.GetString(0));
                    }
                }
                UnitNoComboBox.ItemsSource = units;
                UnitNoComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}");
            }
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_suppressSelectionChanged) return;
            LoadTenantsData();
        }

        private void LoadTenantsData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    var whereParts = new List<string>();

                    if (MonthsComboBox.SelectedItem != null && MonthsComboBox.SelectedItem.ToString() != "All")
                    {
                        if (DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMMM yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime chosenMonth))
                        {
                            whereParts.Add("YEAR(TRY_CONVERT(date, DATAFORMONTH))=@yr AND MONTH(TRY_CONVERT(date, DATAFORMONTH))=@mn");
                            cmd.Parameters.AddWithValue("@yr", chosenMonth.Year);
                            cmd.Parameters.AddWithValue("@mn", chosenMonth.Month);
                        }
                    }

                    if (UnitNoComboBox.SelectedItem != null && UnitNoComboBox.SelectedItem.ToString() != "All")
                    {
                        whereParts.Add("[UNIT NO]=@UnitNo");
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                    }

                    string whereClause = whereParts.Count > 0 ? "WHERE " + string.Join(" AND ", whereParts) : "";
                    cmd.CommandText = $@"
                        SELECT [UNIT NO],[TENANT NAME],[EXPECTED RENT],[WATER],SANITATION,
                               [RENT RECEIVED],DEPOSIT,[TOTAL MONEY PAID],DATAFORMONTH,UPLOADTIME
                        FROM UploadsTenantRent
                        {whereClause}
                        ORDER BY DATAFORMONTH DESC,[UNIT NO]";

                    var dt = new System.Data.DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    TenantsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant data: {ex.Message}");
            }
        }

        private void TenantsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TenantsDataGrid.SelectedItem == null) return;

                var drv = TenantsDataGrid.SelectedItem as DataRowView;
                if (drv == null) return;

                _suppressSelectionChanged = true;
                UnitNoComboBox.SelectedItem = drv["UNIT NO"]?.ToString();

                ExpectedRentTextBox.Text = drv["EXPECTED RENT"]?.ToString() ?? "0";
                WaterCostTextBox.Text = drv["WATER"]?.ToString() ?? "0";
                SanitationCostTextBox.Text = drv["SANITATION"]?.ToString() ?? "0";
                RentReceivedTextBox.Text = drv["RENT RECEIVED"]?.ToString() ?? "0";
                DepositTextBox.Text = drv["DEPOSIT"]?.ToString() ?? "0";

                ComputeTotal();
            }
            finally
            {
                _suppressSelectionChanged = false;
            }
        }

        private void Field_TextChanged(object sender, TextChangedEventArgs e) => ComputeTotal();

        private void ComputeTotal()
        {
            decimal.TryParse(ExpectedRentTextBox.Text, out decimal a);
            decimal.TryParse(WaterCostTextBox.Text, out decimal b);
            decimal.TryParse(SanitationCostTextBox.Text, out decimal c);
            TotalPerUnitTextBox.Text = (a + b + c).ToString("0.00");
        }

        private decimal GetDecimal(System.Windows.Controls.TextBox box)
        {
            return decimal.TryParse(box.Text, out decimal val) ? val : 0;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UnitNoComboBox.SelectedItem == null || UnitNoComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a valid Unit No.");
                    return;
                }

                if (MonthsComboBox.SelectedItem == null || MonthsComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a valid Month.");
                    return;
                }

                if (!DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMMM yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime chosenMonth))
                {
                    MessageBox.Show("Invalid month format.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO UploadsTenantRent
                        ([UNIT NO],[EXPECTED RENT],[WATER],SANITATION,[RENT RECEIVED],DEPOSIT,DATAFORMONTH,UPLOADTIME)
                        VALUES(@UnitNo,@Rent,@Water,@Sanitation,@Received,@Deposit,@Month,GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Rent", GetDecimal(ExpectedRentTextBox));
                        cmd.Parameters.AddWithValue("@Water", GetDecimal(WaterCostTextBox));
                        cmd.Parameters.AddWithValue("@Sanitation", GetDecimal(SanitationCostTextBox));
                        cmd.Parameters.AddWithValue("@Received", GetDecimal(RentReceivedTextBox));
                        cmd.Parameters.AddWithValue("@Deposit", GetDecimal(DepositTextBox));
                        cmd.Parameters.AddWithValue("@Month", new DateTime(chosenMonth.Year, chosenMonth.Month, 1));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Record saved successfully.");
                LoadTenantsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving record: {ex.Message}");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UnitNoComboBox.SelectedItem == null || UnitNoComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a Unit No before updating.");
                    return;
                }

                if (MonthsComboBox.SelectedItem == null || MonthsComboBox.SelectedItem.ToString() == "All")
                {
                    MessageBox.Show("Please select a Month before updating.");
                    return;
                }

                if (!DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMMM yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime chosenMonth))
                {
                    MessageBox.Show("Invalid month selected.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE UploadsTenantRent
                        SET [EXPECTED RENT]=@Rent,[WATER]=@Water,SANITATION=@Sanitation,
                            [RENT RECEIVED]=@Received,DEPOSIT=@Deposit,UPLOADTIME=GETDATE()
                        WHERE [UNIT NO]=@UnitNo
                          AND YEAR(TRY_CONVERT(date,DATAFORMONTH))=@yr
                          AND MONTH(TRY_CONVERT(date,DATAFORMONTH))=@mn";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Rent", GetDecimal(ExpectedRentTextBox));
                        cmd.Parameters.AddWithValue("@Water", GetDecimal(WaterCostTextBox));
                        cmd.Parameters.AddWithValue("@Sanitation", GetDecimal(SanitationCostTextBox));
                        cmd.Parameters.AddWithValue("@Received", GetDecimal(RentReceivedTextBox));
                        cmd.Parameters.AddWithValue("@Deposit", GetDecimal(DepositTextBox));
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@yr", chosenMonth.Year);
                        cmd.Parameters.AddWithValue("@mn", chosenMonth.Month);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? "Record updated successfully." : "No record found to update.");
                    }
                }

                LoadTenantsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MonthsComboBox.SelectedIndex = -1;
            UnitNoComboBox.SelectedIndex = -1;
            ExpectedRentTextBox.Clear();
            WaterCostTextBox.Clear();
            SanitationCostTextBox.Clear();
            RentReceivedTextBox.Clear();
            DepositTextBox.Clear();
            TotalPerUnitTextBox.Text = "0.00";
            IsPaidCheckBox.IsChecked = false;
        }

        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.]+$");
        }


        private void UtilityPerUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            var win = new UtilityPerUnitCostCharges();
            win.Show();
        }


        private void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            //Process Button
            //this is a process button
            //process Button that shows total or changes state of DataGrid
            //Also, do calculation according to Unit prices set, which are multiplied by units used that will be added
            //all of the above functions will work after the process button is clicked
            //Prepayment for the next month will be added

        }


        private void ImportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            //this is an import excel button,
            //user can upload from excel, only values which are:
            //WaterUsed, SanitationUsed, GasUsed, ElectricityUsed, InternetUsed, TVUsed, LaundryUsed, MiscUsed
        }



        //below added fucntion will create a csv file
        private void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dv = TenantsDataGrid.ItemsSource as DataView;
                if (dv == null)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }
                var dt = dv.ToTable();
                var dlg = new SaveFileDialog()
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"UtilityCostPerUnit_{DateTime.Now:ddMMMM}.csv"
                };
                if (dlg.ShowDialog() == true)
                {
                    using (var sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8))
                    {
                        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(c.ColumnName));
                        sw.WriteLine(string.Join(",", columnNames));
                        foreach (DataRow row in dt.Rows)
                        {
                            var fields = dt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(row[c].ToString()));
                            sw.WriteLine(string.Join(",", fields));
                        }
                    }
                    MessageBox.Show("Export complete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting CSV: {ex.Message}");
            }


        }
        private static string EscapeForCsv(string s)
        {
            if (s == null) return "";
            if (s.Contains("\"")) s = s.Replace("\"", "\"\"");
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
                return $"\"{s}\"";
            return s;
        }

        private void IsPaidCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    //Now records will show those where expected rent is greater than paid, it shows that it isn't paid in full
                    string sql = @"                               
                                       SELECT [UNIT NO],
                                       [TENANT NAME],
                                       [EXPECTED RENT],
                                       WATER,
                                       SANITATION,
                                       [RENT RECEIVED],
                                       DEPOSIT,
                                       PREPAYMENT,
                                       [TOTAL MONEY PAID]

 	                                FROM UploadsTenantRent
                                WHERE [EXPECTED RENT] > [TOTAL MONEY PAID]  
                                        "; 

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);
                        TenantsDataGrid.ItemsSource = dt.DefaultView; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing paid records: {ex.Message}");
            }
        }

        private void IsPaidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    string sql = @"
                               SELECT [UNIT NO],[TENANT NAME],[EXPECTED RENT],[WATER],SANITATION,
                               [RENT RECEIVED],DEPOSIT,[TOTAL MONEY PAID],DATAFORMONTH,UPLOADTIME
                               
                               FROM UploadsTenantRent
                               WHERE [EXPECTED RENT] > 0"; 

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);
                        TenantsDataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenant data: {ex.Message}");
            }
        }
    }
}
