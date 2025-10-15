using AccountsLibrary.Properties;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class PrepaymentTracking : UserControl
    {
        public PrepaymentTracking()
        {
            InitializeComponent();
            Loaded += PrepaymentTracking_Loaded;
            MonthsComboBox.SelectionChanged += FilterControl_Changed;
            UnitNoComboBox.SelectionChanged += FilterControl_Changed;
            TenantNameComboBox.SelectionChanged += FilterControl_Changed;
        }
        private void PrepaymentTracking_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonths();
            PopulateUnits();
            PopulateTenants();
            LoadPrepayments();
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
                    string q = @"
                        SELECT DISTINCT 
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
                            int yr = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                            int mn = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1);
                            if (yr != 0 && mn != 0)
                            {
                                var monthName = new DateTime(yr, mn, 1).ToString("MMM yyyy");
                                MonthsComboBox.Items.Add(monthName);
                            }
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
                UnitNoComboBox.Items.Clear();
                UnitNoComboBox.Items.Add("All");
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = "SELECT DISTINCT [UNIT NO] FROM UploadsTenantRent ORDER BY [UNIT NO]";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                UnitNoComboBox.Items.Add(rdr.GetString(0));
                        }
                    }
                }
                UnitNoComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}");
            }
        }
        private void PopulateTenants()
        {
            try
            {
                TenantNameComboBox.Items.Clear();
                TenantNameComboBox.Items.Add("All");
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string q = "SELECT DISTINCT [TENANT NAME] FROM UploadsTenantRent ORDER BY [TENANT NAME]";
                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                TenantNameComboBox.Items.Add(rdr.GetString(0));
                        }
                    }
                }
                TenantNameComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenants: {ex.Message}");
            }
        }
        private void FilterControl_Changed(object sender, SelectionChangedEventArgs e)
        {
            LoadPrepayments();
        }

        private void LoadPrepayments()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    var filters = new List<string>();
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // Prepayment > 0 condition as required
                    filters.Add("PREPAYMENT > 0");

                    // Tenant filter
                    if (TenantNameComboBox.SelectedItem != null && TenantNameComboBox.SelectedItem.ToString() != "All")
                    {
                        filters.Add("[TENANT NAME] = @TenantName");
                        cmd.Parameters.AddWithValue("@TenantName", TenantNameComboBox.SelectedItem.ToString());
                    }

                    // Unit filter
                    if (UnitNoComboBox.SelectedItem != null && UnitNoComboBox.SelectedItem.ToString() != "All")
                    {
                        filters.Add("[UNIT NO] = @UnitNo");
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                    }

                    // Month filter
                    if (MonthsComboBox.SelectedItem != null && MonthsComboBox.SelectedItem.ToString() != "All")
                    {
                        if (DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMM yyyy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out DateTime chosenMonth))
                        {
                            int y = chosenMonth.Year;
                            int m = chosenMonth.Month;
                            filters.Add("YEAR(TRY_CONVERT(date, DATAFORMONTH)) = @yr AND MONTH(TRY_CONVERT(date, DATAFORMONTH)) = @mn");
                            cmd.Parameters.AddWithValue("@yr", y);
                            cmd.Parameters.AddWithValue("@mn", m);
                        }
                    }

                  //  string whereClause = filters.Count > 0 ? "WHERE " + string.Join(" AND ", filters) : "";
                    string whereClause = filters.Count > 0 ? "WHERE " + string.Join(" AND ", filters) + " AND COALESCE(Vchrid,0)=0 " : " WHERE COALESCE(Vchrid,0)=0 ";
                    string query = $@"
                                    SELECT
                                        [UNIT NO],
                                        [TENANT NAME],
                                        PREPAYMENT,
                                        [EXPECTED RENT],
                                        [TOTAL MONEY PAID],
                                        DATAFORMONTH
                                    FROM UploadsTenantRent
                                    {whereClause}
                                    ORDER BY DATAFORMONTH DESC, [TENANT NAME]";

                    cmd.CommandText = query;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    PrepaymentDataGrid.AutoGenerateColumns = true;
                    PrepaymentDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading prepayment data: {ex.Message}");
            }
        }


        private void SendReminderEmails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dv = PrepaymentDataGrid.ItemsSource as DataView;
                if (dv == null || dv.Count == 0)
                {
                    MessageBox.Show("No data available to send reminders.");
                    return;
                }
                            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while trying to prepare reminder emails: {ex.Message}");
            }
        }

        //Below added fucntion will create a csv file
        private void ExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dv = PrepaymentDataGrid.ItemsSource as DataView;
                if (dv == null)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }
                var dt = dv.ToTable();
                var dlg = new SaveFileDialog()
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"Prepayments_{DateTime.Now:ddMMMM}.csv"
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

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Click();
        }

        private void Click()
        {

            MonthsComboBox.SelectedIndex = -1;
            UnitNoComboBox.SelectedIndex = -1;
            TenantNameComboBox.SelectedIndex = -1;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Click();
        }
    }
}
