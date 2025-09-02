using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlexMold.MVVM.View.Master
{
    public partial class ExpenseAccount : Window
    {
        private readonly string connectionString =
            System.Configuration.ConfigurationManager
                .ConnectionStrings["PSVAT5"]
                .ConnectionString;

        public ExpenseAccount()
        {
            InitializeComponent();
            Loaded += ExpenseAccount_Loaded;
        }

        private void ExpenseAccount_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccountGroupComboBox();
        }

        private void LoadAccountGroupComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT GroupName FROM AccountGroup ORDER BY GroupName";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AccountGroupComboBox.Items.Clear();
                while (reader.Read())
                {
                    AccountGroupComboBox.Items.Add(reader["GroupName"].ToString());
                }
                conn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var accountsGroup = new AccountsGroup();
            accountsGroup.ShowDialog();
            LoadAccountGroupComboBox(); // Refresh after adding new group
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AccountName.Clear();
            AccountGroupComboBox.SelectedIndex = -1;
            AccountGroupComboBox.Text = string.Empty;
            OpeningBalanceTextBox.Text = "0";
            DrCrComboBox.SelectedIndex = 0;
            ActiveCheckbox.IsChecked = false;
            OpeningBalanceTextBox.Clear();
            DatePickBox.SelectedDate = null;
            DescriptionTextBox.Clear();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AccountName.Text.Trim();
            string groupName = AccountGroupComboBox.Text.Trim();
            string drcr = (DrCrComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string description = DescriptionTextBox.Text.Trim();
            DateTime date = DatePickBox.SelectedDate ?? DateTime.Now;
            bool isActive = ActiveCheckbox.IsChecked == true;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("Account Name and Group are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(OpeningBalanceTextBox.Text.Trim(), out decimal openingBalance))
            {
                MessageBox.Show("Invalid Opening Balance.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int groupId = 0;

            // Resolve AGroupId from GroupName
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT AGroupId FROM AccountGroup WHERE GroupName = @groupName", conn))
            {
                cmd.Parameters.AddWithValue("@groupName", groupName);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Selected group does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                groupId = Convert.ToInt32(result);
            }

            // Insert into ExpenseAccount
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Account 
                  (AccountName, AGroupId, OpeningBalance, DebitOrCridt, isActive, Remarks)
                  VALUES 
                  (@name, @groupId, @openingBalance, @drcr, @isActive, @description)", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                cmd.Parameters.AddWithValue("@openingBalance", openingBalance);
                cmd.Parameters.AddWithValue("@drcr", drcr);
                cmd.Parameters.AddWithValue("@isActive", isActive);
                cmd.Parameters.AddWithValue("@description", description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Record saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearButton_Click(null, null);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement delete logic here using pure SQL
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ClearButton_Click(null, null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var searchExpense = new SearchExpenseAccount();
            searchExpense.Show();
        }

        private void ExpenseOpeningBalance_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }



    }
}