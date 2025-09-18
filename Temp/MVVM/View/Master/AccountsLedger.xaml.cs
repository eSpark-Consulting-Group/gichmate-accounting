using AccountsLibrary.Properties;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountsPRO.MVVM.View.Master
{
    public partial class AccountsLedger : Window
    {        
        private int? selectedLedgerAccountId = null;
        private string selectedLedgerAccountName = "";

        public AccountsLedger()
        {
            InitializeComponent();
            Loaded += AccountsLedger_Loaded;
            DatePickTextBox.SelectedDate = DateTime.Now;
        }

        private void AccountsLedger_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccountGroupComboBox();
        }

        private void LoadAccountGroupComboBox()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
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
            LoadAccountGroupComboBox();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            AccountName.Clear();
            AccountGroupComboBox.SelectedIndex = -1;
            AccountGroupComboBox.Text = string.Empty;
            OpeningBalanceTextBox.Text = "0";
            DrCrComboBox.SelectedIndex = 0;
            ActiveCheckbox.IsChecked = false;
            DatePickTextBox.SelectedDate = null;
            DescriptionTextBox.Clear();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchLedger = new SearchLedger();
            bool? result = searchLedger.ShowDialog();

            if (result == true && searchLedger.SelectedAccountRow != null)
            {
                selectedLedgerAccountId = Convert.ToInt32(searchLedger.SelectedAccountRow["ID"]);
                selectedLedgerAccountName = searchLedger.SelectedAccountRow["AccountName"].ToString();

                MessageBox.Show($"Selected Account: {selectedLedgerAccountName} (ID: {selectedLedgerAccountId})", "Selected");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AccountName.Text.Trim();
            string groupName = AccountGroupComboBox.Text.Trim();
            string drcr = (DrCrComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Dr";
            string description = DescriptionTextBox.Text.Trim();
            DateTime date = DatePickTextBox.SelectedDate ?? DateTime.Now;
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

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                using (SqlCommand cmd = new SqlCommand("SELECT AGroupId FROM AccountGroup WHERE GroupName = @groupName", conn))
                {
                    cmd.Parameters.AddWithValue("@groupName", groupName);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Selected group does not exist in AccountGroup table.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    groupId = Convert.ToInt32(result);
                }

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
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
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearButton_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Insert failed. No rows affected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!selectedLedgerAccountId.HasValue)
            {
                MessageBox.Show("No account selected for deletion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Are you sure you want to delete '{selectedLedgerAccountName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Account WHERE AccountId = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", selectedLedgerAccountId.Value);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                       
                        MessageBox.Show("Account deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearButton_Click(null, null);
                        selectedLedgerAccountId = null;
                        selectedLedgerAccountName = "";
                    }
                    else
                    {
                        MessageBox.Show("Deletion failed. No matching record found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during deletion: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpeningBalance_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.
        }
    }
}