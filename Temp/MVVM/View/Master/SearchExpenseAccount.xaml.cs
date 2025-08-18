using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    public partial class SearchExpenseAccount : Window
    {
        private readonly string connectionString =
            System.Configuration.ConfigurationManager
                .ConnectionStrings["PSVAT5"]
                .ConnectionString;

        public SearchExpenseAccount()
        {
            InitializeComponent();
            Loaded += SearchExpenseAccount_Loaded;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void SearchExpenseAccount_Loaded(object sender, RoutedEventArgs e)
        {
            LoadExpenseAccounts();
        }

        private void LoadExpenseAccounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT Account.AccountId AS ID,
                 Account.AccountName AS AccountName,
                 AccountGroup.GroupName AS AccountGroup
          FROM Account
          JOIN AccountGroup ON Account.AGroupId = AccountGroup.AGroupId
          WHERE AccountGroup.GroupName LIKE '%EXPENSE%'
          ORDER BY Account.AccountName", conn))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SearchExpenseDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                 @"SELECT Account.AccountId AS ID,
                  Account.AccountName AS AccountName,
                  AccountGroup.GroupName AS AccountGroup
                  FROM Account
                  JOIN AccountGroup ON Account.AGroupId = AccountGroup.AGroupId
                  WHERE AccountGroup.GroupName LIKE '%EXPENSE%' AND (
                        CAST(Account.AccountId AS NVARCHAR) LIKE @search OR
                        Account.AccountName LIKE @search OR
                        AccountGroup.GroupName LIKE @search)
                  ORDER BY Account.AccountName", conn))
            {
                cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                SearchExpenseDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (SearchExpenseDataGrid.SelectedItem is DataRowView row)
            {
                string selectedId = row["ID"].ToString();

                var result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Account WHERE AccountId = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadExpenseAccounts(); // Refresh grid
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            if (SearchExpenseDataGrid.SelectedItem is DataRowView row)
            {
                string selectedId = row["ID"].ToString();
                string selectedName = row["AccountName"].ToString();
                string selectedGroup = row["AccountGroup"].ToString();

                MessageBox.Show($"Selected: {selectedName} ({selectedGroup})", "Selection");
                // You can pass this to another window or store it as needed
            }
            else
            {
                MessageBox.Show("Please select a row first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: handle selection logic here
        }
    }
}