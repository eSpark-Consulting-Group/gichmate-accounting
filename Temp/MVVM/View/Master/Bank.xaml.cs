using AccountsLibrary.Properties;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlexMold.MVVM.View.Master
{
    public partial class Bank : Window
    {
        private int? currentAccountId = null;
        public Bank()
        {
            InitializeComponent();
            LoadAccountGroups();
            DatePickBox.SelectedDate = DateTime.Now;

            // Hide everything below the Accounting Group until BANK is selected
            ToggleBelowDetails(false);
        }

        private void LoadAccountGroups()
        {
            try
            {
                AccountGroupComboBox.ItemsSource = null;
                AccountGroupComboBox.Items.Clear();

                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
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
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (currentAccountId == null)
                    {
                        cmd = new SqlCommand(@"INSERT INTO Account (
                            AccountName, AGroupId, OpeningBalance, DebitOrCridt, isActive, 
                            AccountDate, Remarks, AddressOne, AddressTwo, AddressThree, PhoneOne, 
                            MobileNo, FaxNo, EMailOne, TinNo, BankName, BankAddress, AccountNo
                        ) VALUES (
                            @AccountName, @AGroupId, @OpeningBalance, @DebitOrCridt, @isActive, 
                            @AccountDate, @Remarks, @AddressOne, @AddressTwo, @AddressThree, @PhoneOne, 
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
                            AccountDate = @AccountDate, 
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

                    cmd.Parameters.AddWithValue("@AccountName", AccountName.Text);
                    cmd.Parameters.AddWithValue("@AGroupId", AccountGroupComboBox.SelectedValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OpeningBalance", decimal.Parse(OpeningBalanceTextBox.Text));
                    cmd.Parameters.AddWithValue("@DebitOrCridt", (DrCrComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "DR");
                    cmd.Parameters.AddWithValue("@isActive", ActiveCheckbox.IsChecked ?? false);
                    cmd.Parameters.AddWithValue("@AccountDate", DatePickBox.SelectedDate ?? DateTime.Now);
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
            DatePickBox.SelectedDate = null;
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


            ToggleBelowDetails(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchAccountsGroup = new AccountsGroup();
            searchAccountsGroup.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchBank = new SearchBank();
            searchBank.Show();
        }

        private void LoadAccount(int accountId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.strConnetionString))
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
                            DatePickBox.SelectedDate = Convert.ToDateTime(reader["AccountDate"]);
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


                            ToggleBelowDetails(IsCurrentSelectionBank());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account: {ex.Message}");
            }
        }

        // BANK VISIBILITY LOGIC
        private void AccountGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleBelowDetails(IsCurrentSelectionBank());
        }

        private bool IsCurrentSelectionBank()
        {
            try
            {
                // If ItemsSource is DataView -> SelectedItem is DataRowView
                if (AccountGroupComboBox.SelectedItem is DataRowView drv)
                {
                    var name = drv["GroupName"]?.ToString();
                    return string.Equals(name?.Trim(), "CASH AT BANK", StringComparison.OrdinalIgnoreCase);
                }

                // If items were plain ComboBoxItem or text
                var text = AccountGroupComboBox.Text?.Trim();
                return string.Equals(text, "CASH AT BANK", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private void ToggleBelowDetails(bool show)
        {
            if (this.Content is Grid grid)
            {
                // Loop through all children in grid
                foreach (UIElement el in grid.Children)
                {
                    int r = Grid.GetRow(el);

                    // Last row is not included because it includes buttons that I visible at all times
                    int lastRow = grid.RowDefinitions.Count - 1;

                    // Hide/show all rows >= 2, but skip the last row
                    if (r >= 2 && r < lastRow)
                    {
                        el.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            }
        }

        private void BankOpeningBalance_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.
        }
    }
}

    
