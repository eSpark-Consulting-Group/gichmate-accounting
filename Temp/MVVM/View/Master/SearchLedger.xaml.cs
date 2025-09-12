using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using AccountsLibrary.Properties;

namespace FlexMold.MVVM.View.Master
{
    public partial class SearchLedger : Window
    {
        public DataRowView SelectedAccountRow { get; private set; }

        public SearchLedger()
        {
            InitializeComponent();
            Loaded += SearchLedger_Loaded;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void SearchLedger_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLedgerAccounts();
        }

        private void LoadLedgerAccounts()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Account.AccountId AS ID,
                         Account.AccountName AS AccountName,
                         AccountGroup.GroupName AS AccountGroup
                  FROM Account
                  JOIN AccountGroup ON Account.AGroupId = AccountGroup.AGroupId
                  ORDER BY Account.AccountName", conn))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SearchLedgerDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.Trim();

            using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Account.AccountId AS ID,
                         Account.AccountName AS AccountName,
                         AccountGroup.GroupName AS AccountGroup
                  FROM Account
                  JOIN AccountGroup ON Account.AGroupId = AccountGroup.AGroupId
                  WHERE CAST(Account.AccountId AS NVARCHAR) LIKE @search OR
                        Account.AccountName LIKE @search OR
                        AccountGroup.GroupName LIKE @search
                  ORDER BY Account.AccountName", conn))
            {
                cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SearchLedgerDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            if (SearchLedgerDataGrid.SelectedItem is DataRowView row)
            {
                SelectedAccountRow = row;                      
            }
            else
            {
                MessageBox.Show("Please select a row first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearButton(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (SearchLedgerDataGrid.SelectedItem is DataRowView row)
            {
                int accountId = Convert.ToInt32(row["ID"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM Account WHERE AccountId = @AccountId", conn))
                            {
                                cmd.Parameters.AddWithValue("@AccountId", accountId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Account deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadLedgerAccounts(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting account: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}