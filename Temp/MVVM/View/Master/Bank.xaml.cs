using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Master
{
    public partial class Bank : Window
    {
        private int? currentAccountId = null;
        private string connectionString = "Your_Connection_String_Here";

        public Bank()
        {
            InitializeComponent();
            LoadAccountGroups();
            DatePickBox.SelectedDate = DateTime.Now;
        }

        private void LoadAccountGroups()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT AGroupId, GroupName FROM AccountGroup";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    AccountGroupComboBox.DisplayMemberPath = "GroupName";
                    AccountGroupComboBox.SelectedValuePath = "AGroupId";
                    AccountGroupComboBox.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account groups: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AccountName.Text))
            {
                MessageBox.Show("Account name is required");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (currentAccountId == null)
                    {
                        cmd = new SqlCommand(@"INSERT INTO Account (
                            AccountName, AGroupId, OpeningBalance, DebitOrCridt, isActive, 
                            Date, Remarks, AddressOne, AddressTwo, AddressThree, PhoneOne, 
                            MobileNo, FaxNo, EMailOne, TinNo, BankName, BankAddress, AccountNo
                        ) VALUES (
                            @AccountName, @AGroupId, @OpeningBalance, @DebitOrCridt, @isActive, 
                            @Date, @Remarks, @AddressOne, @AddressTwo, @AddressThree, @PhoneOne, 
                            @MobileNo, @FaxNo, @EMailOne, @TinNo, @BankName, @BankAddress, @AccountNo
                        ); SELECT SCOPE_IDENTITY();", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand(@"UPDATE Account SET 
                            AccountName = @AccountName, 
                            AGroupId = @AGroupId, 
                            OpeningBalance = @OpeningBalance, 
                            DebitOrCridt = @DebitOrCridt, 
                            isActive = @isActive, 
                            Date = @Date, 
                            Remarks = @Remarks, 
                            AddressOne = @AddressOne, 
                            AddressTwo = @AddressTwo, 
                            AddressThree = @AddressThree, 
                            PhoneOne = @PhoneOne, 
                            MobileNo = @MobileNo, 
                            FaxNo = @FaxNo, 
                            EMailOne = @EMailOne, 
                            TinNo = @TinNo, 
                            BankName = @BankName, 
                            BankAddress = @BankAddress, 
                            AccountNo = @AccountNo
                        WHERE AccountId = @AccountId", conn);
                        cmd.Parameters.AddWithValue("@AccountId", currentAccountId);
                    }

                    // Add parameters
                    cmd.Parameters.AddWithValue("@AccountName", AccountName.Text);
                    cmd.Parameters.AddWithValue("@AGroupId", AccountGroupComboBox.SelectedValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OpeningBalance", decimal.Parse(OpeningBalanceTextBox.Text));
                    cmd.Parameters.AddWithValue("@DebitOrCridt", (DrCrComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "DR");
                    cmd.Parameters.AddWithValue("@isActive", ActiveCheckbox.IsChecked ?? false);
                    cmd.Parameters.AddWithValue("@Date", DatePickBox.SelectedDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@Remarks", DescriptionInputBox.Text);
                    cmd.Parameters.AddWithValue("@AddressOne", RoadTextBox.Text);
                    cmd.Parameters.AddWithValue("@AddressTwo", BuildingTextBox.Text);
                    cmd.Parameters.AddWithValue("@AddressThree", BlockTextBox.Text);
                    cmd.Parameters.AddWithValue("@PhoneOne", PhoneTextBox.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", MobileTextBox.Text);
                    cmd.Parameters.AddWithValue("@FaxNo", FaxTextBox.Text);
                    cmd.Parameters.AddWithValue("@EMailOne", EmailAddressTextBox.Text);
                    cmd.Parameters.AddWithValue("@TinNo", GSTNoTextBox.Text);
                    cmd.Parameters.AddWithValue("@BankName", BankNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@BankAddress", BankDetailsAddress.Text);
                    cmd.Parameters.AddWithValue("@AccountNo", AccountNoTextBox.Text);

                    if (currentAccountId == null)
                    {
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show("Account saved successfully!");
                        currentAccountId = newId;
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account updated successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving account: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccountId == null)
            {
                MessageBox.Show("No account selected");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this account?",
                "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(
                            "DELETE FROM Account WHERE AccountId = @AccountId", conn);
                        cmd.Parameters.AddWithValue("@AccountId", currentAccountId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account deleted successfully");
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting account: {ex.Message}");
                }
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            currentAccountId = null;
            AccountName.Text = "";
            AccountGroupComboBox.SelectedIndex = -1;
            OpeningBalanceTextBox.Text = "0";
            DrCrComboBox.SelectedIndex = 0;
            ActiveCheckbox.IsChecked = false;
            DatePickBox.SelectedDate = DateTime.Now;
            DescriptionInputBox.Text = "";
            RoadTextBox.Text = "";
            BuildingTextBox.Text = "";
            BlockTextBox.Text = "";
            PhoneTextBox.Text = "";
            MobileTextBox.Text = "";
            FaxTextBox.Text = "";
            EmailAddressTextBox.Text = "";
            GSTNoTextBox.Text = "";
            BankNameTextBox.Text = "";
            BankDetailsAddress.Text = "";
            AccountNoTextBox.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var accountsGroup = new AccountsGroup();
            accountsGroup.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchBank = new SearchBank();
            if (searchBank.ShowDialog() == true)
            {
                LoadAccount(searchBank.SelectedAccountId);
            }
        }

        private void LoadAccount(int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Account WHERE AccountId = @AccountId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccountId", accountId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentAccountId = accountId;
                            AccountName.Text = reader["AccountName"].ToString();
                            AccountGroupComboBox.SelectedValue = reader["AGroupId"];
                            OpeningBalanceTextBox.Text = reader["OpeningBalance"].ToString();
                            DrCrComboBox.Text = reader["DebitOrCridt"].ToString();
                            ActiveCheckbox.IsChecked = Convert.ToBoolean(reader["isActive"]);
                            DatePickBox.SelectedDate = Convert.ToDateTime(reader["Date"]);
                            DescriptionInputBox.Text = reader["Remarks"].ToString();
                            RoadTextBox.Text = reader["AddressOne"].ToString();
                            BuildingTextBox.Text = reader["AddressTwo"].ToString();
                            BlockTextBox.Text = reader["AddressThree"].ToString();
                            PhoneTextBox.Text = reader["PhoneOne"].ToString();
                            MobileTextBox.Text = reader["MobileNo"].ToString();
                            FaxTextBox.Text = reader["FaxNo"].ToString();
                            EmailAddressTextBox.Text = reader["EMailOne"].ToString();
                            GSTNoTextBox.Text = reader["TinNo"].ToString();
                            BankNameTextBox.Text = reader["BankName"].ToString();
                            BankDetailsAddress.Text = reader["BankAddress"].ToString();
                            AccountNoTextBox.Text = reader["AccountNo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account: {ex.Message}");
            }
        }
    }
}