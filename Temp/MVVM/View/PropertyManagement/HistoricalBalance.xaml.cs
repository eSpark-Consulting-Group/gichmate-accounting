using AccountsLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class HistoricalBalance : UserControl
    {
        public HistoricalBalance()
        {
            InitializeComponent();

            FromMonthDatePicker.SelectedDate = DateTime.Today;
            ToMonthDatePicker.SelectedDate = DateTime.Today;

            LoadTenantNames();
            LoadHistoricalBalance();
            LoadUnitNo();

            TenantNameComboBox.SelectionChanged += (s, e) => LoadHistoricalBalance();
            UnitNoComboBox.SelectionChanged += (s, e) => LoadHistoricalBalance(); // <-- Fixed
            FromMonthDatePicker.SelectedDateChanged += (s, e) => LoadHistoricalBalance();
            ToMonthDatePicker.SelectedDateChanged += (s, e) => LoadHistoricalBalance();
        }

        public class HistoricalBalanceRecord
        {
            public string UnitNo { get; set; }
            public string TenantName { get; set; }
            public decimal ExpectedRent { get; set; }
            public decimal Deposit { get; set; }
            public string Month { get; set; }
        }

        private void LoadUnitNo()
        {
            try
            {
                List<string> units = new List<string>();
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string query = " SELECT DISTINCT [UNIT NO] FROM UploadsTenantRent ORDER BY [UNIT NO] ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    units.Add("ALL");
                    while (reader.Read())
                    {
                        units.Add(reader.GetString(0));
                    }
                }
                UnitNoComboBox.ItemsSource = units;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Units : " + ex.Message);
            }
        }

        private void LoadTenantNames()
        {
            try
            {
                List<string> tenants = new List<string>();
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT [TENANT NAME] FROM UploadsTenantRent ORDER BY [TENANT NAME]";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    tenants.Add("All");
                    while (reader.Read())
                    {
                        tenants.Add(reader["TENANT NAME"].ToString());
                    }
                }
                TenantNameComboBox.ItemsSource = tenants;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tenants: " + ex.Message);
            }
        }

        private void LoadHistoricalBalance()
        {
            try
            {
                if (!FromMonthDatePicker.SelectedDate.HasValue || !ToMonthDatePicker.SelectedDate.HasValue)
                    return;

                string tenantFilter = TenantNameComboBox.SelectedItem?.ToString();
                string unitFilter = UnitNoComboBox.SelectedItem?.ToString(); // <-- Added
                DateTime fromDate = FromMonthDatePicker.SelectedDate.Value;
                DateTime toDate = ToMonthDatePicker.SelectedDate.Value;

                List<HistoricalBalanceRecord> records = new List<HistoricalBalanceRecord>();

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();

                    string query = @"
                SELECT [UNIT NO], [TENANT NAME], [EXPECTED RENT], [DEPOSIT], [DATAFORMONTH]
                FROM UploadsTenantRent
                WHERE CAST([DATAFORMONTH] AS DATE) BETWEEN @FromDate AND @ToDate";

                    if (!string.IsNullOrWhiteSpace(tenantFilter) && !tenantFilter.Contains("All"))
                        query += " AND [TENANT NAME] = @TenantName";

                    if (!string.IsNullOrWhiteSpace(unitFilter) && !unitFilter.Contains("ALL")) // <-- Unit filter
                        query += " AND [UNIT NO] = @UnitNo";

                    query += " ORDER BY CAST([DATAFORMONTH] AS DATE)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@ToDate", toDate.Date);

                    if (!string.IsNullOrWhiteSpace(tenantFilter) && !tenantFilter.Contains("All"))
                        cmd.Parameters.AddWithValue("@TenantName", tenantFilter);

                    if (!string.IsNullOrWhiteSpace(unitFilter) && !unitFilter.Contains("ALL")) // <-- Unit parameter
                        cmd.Parameters.AddWithValue("@UnitNo", unitFilter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new HistoricalBalanceRecord
                            {
                                TenantName = reader["TENANT NAME"].ToString(),
                                UnitNo = reader["UNIT NO"].ToString(),
                                Month = Convert.ToDateTime(reader["DATAFORMONTH"]).ToString("yyyy-MM"),
                                ExpectedRent = reader["EXPECTED RENT"] != DBNull.Value ? Convert.ToDecimal(reader["EXPECTED RENT"]) : 0,
                                Deposit = reader["DEPOSIT"] != DBNull.Value ? Convert.ToDecimal(reader["DEPOSIT"]) : 0
                            });
                        }
                    }
                }

                HistoricalBalanceDataGrid.ItemsSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading historical balance: " + ex.Message);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            FromMonthDatePicker.SelectedDate = null;
            ToMonthDatePicker.SelectedDate = null;
            TenantNameComboBox.SelectedIndex = -1;
            UnitNoComboBox.SelectedIndex = -1;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
