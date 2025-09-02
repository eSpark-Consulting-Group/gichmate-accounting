using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    public partial class SearchBank : Window
    {
        public int SelectedAccountId { get; private set; }
        public event Action<int> AccountSelected;

        private string connectionString = "Server=DESKTOP-E09TG4T;Database=PSVAT5;Trusted_Connection=True;TrustServerCertificate=True;";

        public SearchBank()
        {
            InitializeComponent();
            LoadAccounts();
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT AccountId, AccountName, AGroupId, OpeningBalance, DebitOrCridt, 
                                    isActive, Remarks, AddressOne, AddressTwo, AddressThree, AddressFour,
                                    PhoneOne, MobileNo, FaxNo, EMailOne, TinNo, CstNo,
                                    BankName, BankAddress, AccountNo,
                                    RootId, PCategoryId, IsBuiltIn, DebitLimit, IsLimitApplicable,
                                    FieldOne, FieldTwo, FieldThree, FieldFour
                             FROM Account";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    AccountsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}");
            }
        }

        private void FilterAccounts(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT AccountId, AccountName, AGroupId, OpeningBalance, DebitOrCridt, 
                                    isActive, Remarks, AddressOne, AddressTwo, AddressThree, AddressFour,
                                    PhoneOne, MobileNo, FaxNo, EMailOne, TinNo, CstNo,
                                    BankName, BankAddress, AccountNo,
                                    RootId, PCategoryId, IsBuiltIn, DebitLimit, IsLimitApplicable,
                                    FieldOne, FieldTwo, FieldThree, FieldFour
                             FROM Account 
                             WHERE AccountName LIKE @SearchText 
                             OR AccountNo LIKE @SearchText 
                             OR BankName LIKE @SearchText 
                             OR BankAddress LIKE @SearchText";


                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    AccountsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching accounts: {ex.Message}");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAccounts(SearchTextBox.Text);
        }

        private void ClearButton(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem is DataRowView row)
            {
                int selectedId = (int)row["AccountId"];
                SelectedAccountId = selectedId;
                AccountSelected?.Invoke(selectedId); 
                MessageBox.Show($"Selected AccountId: {selectedId}");
            }
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem is DataRowView row)
            {
                int accountId = (int)row["AccountId"];

                if (MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("DELETE FROM Account WHERE AccountId = @AccountId", conn);
                            cmd.Parameters.AddWithValue("@AccountId", accountId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Account deleted successfully.");
                        LoadAccounts(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting account: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.");
            }
        }

        
    }
}