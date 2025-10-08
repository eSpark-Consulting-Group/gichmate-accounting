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
    public partial class ArrearsAgingAnalysis : UserControl
    {
        public ArrearsAgingAnalysis()
        {
            InitializeComponent();
            Loaded += ArrearsAgingAnalysis_Loaded;
            MonthsComboBox.SelectionChanged += FilterControl_Changed;
            ArrearsAgingComboBox.SelectionChanged += FilterControl_Changed;
            TenantNameComboBox.SelectionChanged += FilterControl_Changed;
            UnitNoComboBox.SelectionChanged += FilterControl_Changed;
            ArrearsAgingComboBox.SelectedIndex = 0;
        }

        private void ArrearsAgingAnalysis_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonths();
            PopulateTenants();
            PopulateUnits();
            LoadTenants();
        }

        #region Populate Dropdowns
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
                                var monthName = new DateTime(yr, mn, 1).ToString("MMMM yyyy");
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
        #endregion

        #region Filtering
        private void FilterControl_Changed(object sender, SelectionChangedEventArgs e)
        {
            LoadTenants();
        }

        private void LoadTenants()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    var whereParts = new List<string>();
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;


                    int agingDaysLower = 0;
                    int agingDaysUpper = 0;
                    bool filterByAging = false;

                    if (ArrearsAgingComboBox.SelectedItem is ComboBoxItem cbi && cbi.Content != null)
                    {
                        var s = cbi.Content.ToString().Trim();

                        if (s.Contains("90+"))
                        {
                            filterByAging = true;
                            agingDaysLower = 90;
                            agingDaysUpper = int.MaxValue;
                        }
                        else if (s.Contains("-"))
                        {
                            s = s.Replace("Days", "").Trim();
                            var parts = s.Split('-');
                            if (parts.Length == 2 &&
                                int.TryParse(parts[0].Trim(), out int lower) &&
                                int.TryParse(parts[1].Trim(), out int upper))
                            {
                                filterByAging = true;
                                agingDaysLower = lower;
                                agingDaysUpper = upper;
                            }
                        }
                    }

                    // Always give ArrearsAging priority
                    if (filterByAging)
                    {
                        if (agingDaysUpper == int.MaxValue)
                        {
                            whereParts.Add("DATEDIFF(day, TRY_CONVERT(date, DATAFORMONTH), GETDATE()) >= @agingLower");
                            cmd.Parameters.AddWithValue("@agingLower", agingDaysLower);
                        }
                        else
                        {
                            whereParts.Add("DATEDIFF(day, TRY_CONVERT(date, DATAFORMONTH), GETDATE()) BETWEEN @agingLower AND @agingUpper");
                            cmd.Parameters.AddWithValue("@agingLower", agingDaysLower);
                            cmd.Parameters.AddWithValue("@agingUpper", agingDaysUpper);
                        }
                    }

                    if (TenantNameComboBox.SelectedItem != null && TenantNameComboBox.SelectedItem.ToString() != "All")
                    {
                        whereParts.Add("[TENANT NAME] = @TenantName");
                        cmd.Parameters.AddWithValue("@TenantName", TenantNameComboBox.SelectedItem.ToString());
                    }

                    if (UnitNoComboBox.SelectedItem != null && UnitNoComboBox.SelectedItem.ToString() != "All")
                    {
                        whereParts.Add("[UNIT NO] = @UnitNo");
                        cmd.Parameters.AddWithValue("@UnitNo", UnitNoComboBox.SelectedItem.ToString());
                    }

                    if (MonthsComboBox.SelectedItem != null && MonthsComboBox.SelectedItem.ToString() != "All")
                    {
                        if (DateTime.TryParseExact(MonthsComboBox.SelectedItem.ToString(), "MMMM yyyy",
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None, out DateTime chosenMonth))
                        {
                            int y = chosenMonth.Year;
                            int m = chosenMonth.Month;
                            whereParts.Add("YEAR(TRY_CONVERT(date, DATAFORMONTH)) = @yr AND MONTH(TRY_CONVERT(date, DATAFORMONTH)) = @mn");
                            cmd.Parameters.AddWithValue("@yr", y);
                            cmd.Parameters.AddWithValue("@mn", m);
                        }
                    }

                    string whereClause = whereParts.Count > 0 ? "WHERE " + string.Join(" AND ", whereParts) : "";

                    string query = $@"
                        SELECT 
                            [UNIT NO], 
                            [TENANT NAME], 
                            [EXPECTED RENT],
                            [RENT RECEIVED], 
                            DEPOSIT, 
                            [ARREAS PAID (RENT)] AS [ARREARS PAID (RENT)], 
                            [ARREAS PAID(UTILITIES)] AS [ARREARS PAID(UTILITIES)],
                            [ARREAS B/F] AS [ARREARS BF], 
                            ARREAS AS ARREARS, 
                            [ARREAS C/F] AS [ARREARS C/F], 
                            PREPAYMENT, 
                            [TOTAL MONEY PAID], 
                            DATEDIFF(day, TRY_CONVERT(date, DATAFORMONTH), GETDATE()) AS OverdueDays
                        FROM UploadsTenantRent
                        {whereClause}
                        ORDER BY OverdueDays DESC, [TENANT NAME]";

                    cmd.CommandText = query;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    if (!dt.Columns.Contains("IsTotalRow"))
                        dt.Columns.Add("IsTotalRow", typeof(bool));

                    foreach (DataRow r in dt.Rows)
                        r["IsTotalRow"] = false;

                    decimal totalArrears = 0m;
                    if (dt.Rows.Count > 0 && dt.Columns.Contains("ARREARS"))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            try
                            {
                                var raw = r["ARREARS"];
                                if (raw != null && raw != DBNull.Value)
                                {
                                    if (decimal.TryParse(raw.ToString(), out decimal v))
                                    {
                                        totalArrears += v;
                                    }
                                }
                            }
                            catch {  }
                        }
                    }

                    //  Add a totals row at the end
                    var totalsRow = dt.NewRow();
                    if (dt.Columns.Contains("UNIT NO")) totalsRow["UNIT NO"] = "Total";
                    if (dt.Columns.Contains("TENANT NAME")) totalsRow["TENANT NAME"] = string.Empty;
                    if (dt.Columns.Contains("ARREARS")) totalsRow["ARREARS"] = totalArrears;
                    
                    totalsRow["IsTotalRow"] = true;
                    dt.Rows.Add(totalsRow);

                    // Bindingg to grid
                    TenantsDataGrid.AutoGenerateColumns = true;
                    TenantsDataGrid.ItemsSource = dt.DefaultView;

                    TotalArrearsAmountTxt.Text = totalArrears.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tenants: {ex.Message}");
            }
        }
        #endregion

        #region Export & Email
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

                var exportDt = dv.ToTable().Clone();
                foreach (DataRow r in dv.ToTable().Rows)
                {
                    if (r.Table.Columns.Contains("IsTotalRow") && r["IsTotalRow"] != DBNull.Value && (bool)r["IsTotalRow"])
                        continue; 
                    exportDt.ImportRow(r);
                }

                if (exportDt.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }

                var dlg = new SaveFileDialog()
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"Arrears Aging Analysis_{DateTime.Now:ddMMMM}.csv"
                };

                if (dlg.ShowDialog() == true)
                {
                    using (var sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8))
                    {
                        var columnNames = exportDt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(c.ColumnName));
                        sw.WriteLine(string.Join(",", columnNames));

                        foreach (DataRow row in exportDt.Rows)
                        {
                            var fields = exportDt.Columns.Cast<DataColumn>().Select(c => EscapeForCsv(row[c].ToString()));
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

        private void SendReminderEmails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dv = TenantsDataGrid.ItemsSource as DataView;
                if (dv == null || dv.Count == 0)
                {
                    MessageBox.Show("No data available to send reminders.");
                    return;
                }

                int opened = 0;
                foreach (DataRowView drv in dv)
                {
                    // skip totals row
                    if (drv.Row.Table.Columns.Contains("IsTotalRow") && drv["IsTotalRow"] != DBNull.Value && (bool)drv["IsTotalRow"])
                        continue;

                    string tenantEmail = drv.Row.Table.Columns.Contains("TENANT EMAIL") ? drv["TENANT EMAIL"]?.ToString() : "";
                    string unitNo = drv.Row.Table.Columns.Contains("UNIT NO") ? drv["UNIT NO"]?.ToString() : "";
                    string arrears = drv.Row.Table.Columns.Contains("ARREARS") ? drv["ARREARS"]?.ToString() : "";

                    if (!string.IsNullOrEmpty(tenantEmail) && tenantEmail.Contains("@"))
                    {
                        string subject = Uri.EscapeDataString($"Rent Arrears Reminder - Unit {unitNo}");
                        string body = Uri.EscapeDataString($"Dear tenant,\n\nOur records show an outstanding arrears amount of {arrears} for Unit {unitNo}. Please contact us to settle.\n\nRegards,\nProperty Management");
                        string mailto = $"mailto:{tenantEmail}?subject={subject}&body={body}";
                        try
                        {
                            Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
                            opened++;
                        }
                        catch { }
                    }
                }
                MessageBox.Show($"Opened {opened} mail client windows (one per tenant email).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while trying to prepare reminder emails: {ex.Message}");
            }
        }
        #endregion

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MonthsComboBox.SelectedIndex = -1;
            ArrearsAgingComboBox.SelectedIndex = -1;
            TenantNameComboBox.SelectedIndex = -1;
            UnitNoComboBox.SelectedIndex = -1;
        }
    }
}
