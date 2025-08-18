using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    public partial class SearchLedger : Window
    {
        private readonly string connectionString =
            System.Configuration.ConfigurationManager
                .ConnectionStrings["PSVAT5"]
                .ConnectionString;

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
            using (SqlConnection conn = new SqlConnection(connectionString))
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

            using (SqlConnection conn = new SqlConnection(connectionString))
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
                DialogResult = true;
                Close();
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
    }
}