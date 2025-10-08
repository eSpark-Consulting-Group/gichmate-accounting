using AccountsLibrary.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class UtilityPerUnitCostCharges : Window
    {
        private string selectedId = null;

        public UtilityPerUnitCostCharges()
        {
            InitializeComponent();
            LoadUtilityCharges();
            UtilityCostPerUnitChargesGrid.SelectionChanged += UtilityCostPerUnitChargesGrid_SelectionChanged;
        }

        private void LoadUtilityCharges()
        {
            using (var conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                var dt = new DataTable();
                new SqlDataAdapter(
                    "SELECT UUCostID AS [ID], WATER AS [Water Per Unit], SANITATION AS [Sanitation Per Unit], GAS AS [Gas Per Unit], " +
                    "ELECTRICITY AS [Electricity Per Unit], INTERNET AS [Internet Per Unit], TV AS [TV Per Unit], " +
                    "LAUNDRY AS [Laundry Per Unit], MISC AS [MISC Per Unit], UPDATETIME AS [Updated Time] " +
                    "FROM UtilityUnitCost", conn).Fill(dt);

                UtilityCostPerUnitChargesGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void UtilityCostPerUnitChargesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UtilityCostPerUnitChargesGrid.SelectedItem is DataRowView row)
            {
                selectedId = row["ID"]?.ToString();
                WaterChargesPerUnitTxtBox.Text = row["Water Per Unit"]?.ToString();
                SanitationChargesPerUnitTxtBox.Text = row["Sanitation Per Unit"]?.ToString();
                GasChargesPerUnitTxtBox.Text = row["Gas Per Unit"]?.ToString();
                ElectricityChargesPerUnitTxtBox.Text = row["Electricity Per Unit"]?.ToString();
                InternetChargesPerUnitTxtBox.Text = row["Internet Per Unit"]?.ToString();
                TVChargesPerUnitTxtBox.Text = row["TV Per Unit"]?.ToString();
                LaundryChargesPerUnitTxtBox.Text = row["Laundry Per Unit"]?.ToString();
                MiscChargesPerUnitTxtBox.Text = row["MISC Per Unit"]?.ToString();
            }
        }

        private void SaveCurrentChargesBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                conn.Open();

                using (var check = new SqlCommand("SELECT COUNT(*) FROM UtilityUnitCost", conn))
                {
                    int count = (int)check.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Only one record allowed. Please use Update instead.");
                        return;
                    }
                }

                var cmd = new SqlCommand(@"
                    INSERT INTO UtilityUnitCost
                    (UUCostID, WATER, SANITATION, GAS, ELECTRICITY, INTERNET, TV, LAUNDRY, MISC, UPDATETIME)
                    VALUES (@ID, @WATER, @SANITATION, @GAS, @ELECTRICITY, @INTERNET, @TV, @LAUNDRY, @MISC, @UPDATETIME)", conn);

                cmd.Parameters.AddWithValue("@ID", GenerateShortID());
                cmd.Parameters.AddWithValue("@WATER", ParseOrNull(WaterChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@SANITATION", ParseOrNull(SanitationChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@GAS", ParseOrNull(GasChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@ELECTRICITY", ParseOrNull(ElectricityChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@INTERNET", ParseOrNull(InternetChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@TV", ParseOrNull(TVChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@LAUNDRY", ParseOrNull(LaundryChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@MISC", ParseOrNull(MiscChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@UPDATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Utility charges saved successfully.");
            LoadUtilityCharges();
        }

        private string GenerateShortID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rand = new Random();
            return new string(new[] { chars[rand.Next(chars.Length)], chars[rand.Next(chars.Length)] });
        }

        private void UpdateCurrentChargesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                MessageBox.Show("Select a record to update.");
                return;
            }

            using (var conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE UtilityUnitCost
                    SET WATER=@WATER, SANITATION=@SANITATION, GAS=@GAS, ELECTRICITY=@ELECTRICITY,
                        INTERNET=@INTERNET, TV=@TV, LAUNDRY=@LAUNDRY, MISC=@MISC,
                        UPDATETIME=@UPDATETIME
                    WHERE UUCostID=@ID", conn);

                cmd.Parameters.AddWithValue("@ID", selectedId);
                cmd.Parameters.AddWithValue("@WATER", ParseOrNull(WaterChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@SANITATION", ParseOrNull(SanitationChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@GAS", ParseOrNull(GasChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@ELECTRICITY", ParseOrNull(ElectricityChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@INTERNET", ParseOrNull(InternetChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@TV", ParseOrNull(TVChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@LAUNDRY", ParseOrNull(LaundryChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@MISC", ParseOrNull(MiscChargesPerUnitTxtBox.Text));
                cmd.Parameters.AddWithValue("@UPDATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Utility charges updated successfully.");
            LoadUtilityCharges();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                MessageBox.Show("Select a record to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm != MessageBoxResult.Yes) return;

            using (var conn = new SqlConnection(Settings.Default.strConnetionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM UtilityUnitCost WHERE UUCostID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", selectedId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Record deleted successfully.");
            ClearFields();
            LoadUtilityCharges();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            WaterChargesPerUnitTxtBox.Text = string.Empty;
            SanitationChargesPerUnitTxtBox.Text = string.Empty;
            GasChargesPerUnitTxtBox.Text = string.Empty;
            ElectricityChargesPerUnitTxtBox.Text = string.Empty;
            InternetChargesPerUnitTxtBox.Text = string.Empty;
            TVChargesPerUnitTxtBox.Text = string.Empty;
            LaundryChargesPerUnitTxtBox.Text = string.Empty;
            MiscChargesPerUnitTxtBox.Text = string.Empty;
        }

        private void ClearFields()
        {
            WaterChargesPerUnitTxtBox.Text = string.Empty;
            SanitationChargesPerUnitTxtBox.Text = string.Empty;
            GasChargesPerUnitTxtBox.Text = string.Empty;
            ElectricityChargesPerUnitTxtBox.Text = string.Empty;
            InternetChargesPerUnitTxtBox.Text = string.Empty;
            TVChargesPerUnitTxtBox.Text = string.Empty;
            LaundryChargesPerUnitTxtBox.Text = string.Empty;
            MiscChargesPerUnitTxtBox.Text = string.Empty;
            selectedId = null;
        }

        private object ParseOrNull(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) return DBNull.Value;
            if (decimal.TryParse(txt, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal val))
                return val;
            return DBNull.Value;
        }

        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.]+$");
        }
    }
}
