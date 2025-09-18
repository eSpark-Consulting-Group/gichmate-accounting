using AccountsLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountsPRO.MVVM.View.Master
{
    public partial class DoubleJournalEntries : Window
    {
        private int? editingVchrId = null; 

        public DoubleJournalEntries()
        {
            InitializeComponent();
            LoadAccounts();
            LoadVouchers();
            DatePickBox.SelectedDate = DateTime.Now;
        }

        #region Models
        public class JournalEntry
        {
            public int SL { get; set; }
            public int VoucherNo { get; set; }
            public DateTime Date { get; set; }
            public string FromAccount { get; set; }
            public string ToAccount { get; set; }
            public double Amount { get; set; }
            public string Narration { get; set; }
            public int VchrId { get; set; }
        }

        public class Account
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }
        }
        #endregion

        #region Load Data
        private void LoadAccounts()
        {
            var accounts = new List<Account>();
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account", con);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        accounts.Add(new Account
                        {
                            AccountId = rdr.GetInt32(0),
                            AccountName = rdr.GetString(1)
                        });
                    }
                }
            }
            FromAccCreditBox.ItemsSource = accounts;
            FromAccCreditBox.DisplayMemberPath = "AccountName";
            FromAccCreditBox.SelectedValuePath = "AccountId";

            ToAccDebitBox.ItemsSource = accounts;
            ToAccDebitBox.DisplayMemberPath = "AccountName";
            ToAccDebitBox.SelectedValuePath = "AccountId";
        }

        private void LoadVouchers()
        {
            var entries = new List<JournalEntry>();
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand(@"SELECT VchrTranId, VchrNo, VchrDate, 
                                          FromAccountId, ToAccountId, VchrAmount, Narration 
                                          FROM VchrTranLog ORDER BY VchrNo", con);
                using (var rdr = cmd.ExecuteReader())
                {
                    int sl = 1;
                    while (rdr.Read())
                    {
                        entries.Add(new JournalEntry
                        {
                            SL = sl++,
                            VchrId = !rdr.IsDBNull(0) ? rdr.GetInt32(0) : 0,
                            VoucherNo = !rdr.IsDBNull(1) ? rdr.GetInt32(1) : 0,
                            Date = !rdr.IsDBNull(2) ? rdr.GetDateTime(2) : DateTime.MinValue,
                            FromAccount = !rdr.IsDBNull(3) ? GetAccountName(rdr.GetInt32(3)) : "",
                            ToAccount = !rdr.IsDBNull(4) ? GetAccountName(rdr.GetInt32(4)) : "",
                            Amount = !rdr.IsDBNull(5) ? Convert.ToDouble(rdr.GetDecimal(5)) : 0,
                            Narration = !rdr.IsDBNull(6) ? rdr.GetString(6) : ""
                        });

                    }
                }
            }
            JournalDataGrid.ItemsSource = entries;
        }

        private string GetAccountName(int accountId)
        {
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountName FROM Account WHERE AccountId=@id", con);
                cmd.Parameters.AddWithValue("@id", accountId);
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? "";
            }
        }
        #endregion

        #region Button Events
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (UpDownBox.Value == null || UpDownBox.Value == 0)
            {
                MessageBox.Show("Voucher number is required.");
                return;
            }

            if (string.IsNullOrEmpty(FromAccCreditBox.Text))
            {
                MessageBox.Show("Account Credit Selection is required");
                return;
            }

            if (string.IsNullOrEmpty(ToAccDebitBox.Text))
            {
                MessageBox.Show("Account Debit selection is required");
                return;
            }


            if (!decimal.TryParse(AmountBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount2) || amount2 <= 0m)
            {
                MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (amount2 > 99999999.99m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }

            int fromId = (int)FromAccCreditBox.SelectedValue;
            int toId = (int)ToAccDebitBox.SelectedValue;
            int vNo = (int)UpDownBox.Value;
            DateTime vDate = DatePickBox.SelectedDate ?? DateTime.Now;
            double amount = double.TryParse(AmountBox.Text, out double a) ? a : 0;
            string narration = NarrationBox.Text;

            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                SqlCommand cmd;

                if (editingVchrId.HasValue)
                {
                    // Update existing
                    cmd = new SqlCommand(@"UPDATE VchrTranLog 
                                           SET VchrNo=@vNo, VchrDate=@vDate, 
                                               FromAccountId=@fromId, ToAccountId=@toId, 
                                               VchrAmount=@amount, Narration=@narr
                                           WHERE VchrTranId=@id", con);
                    cmd.Parameters.AddWithValue("@id", editingVchrId.Value);
                }
                else
                {
                    // Insert new
                    cmd = new SqlCommand(@"INSERT INTO VchrTranLog 
                                           (VchrNo, VchrDate, FromAccountId, ToAccountId, VchrAmount, Narration) 
                                           VALUES(@vNo,@vDate,@fromId,@toId,@amount,@narr)", con);
                }

                cmd.Parameters.AddWithValue("@vNo", vNo);
                cmd.Parameters.AddWithValue("@vDate", vDate);
                cmd.Parameters.AddWithValue("@fromId", fromId);
                cmd.Parameters.AddWithValue("@toId", toId);
                cmd.Parameters.AddWithValue("@amount", (decimal)amount);
                cmd.Parameters.AddWithValue("@narr", narration);
                cmd.ExecuteNonQuery();
            }

            ClearForm();
            MessageBox.Show("Entry Successfully saved!");
            LoadVouchers();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                editingVchrId = selected.VchrId;
                UpDownBox.Value = selected.VoucherNo;
                DatePickBox.SelectedDate = selected.Date;

                FromAccCreditBox.SelectedValue = GetAccountIdByName(selected.FromAccount);
                ToAccDebitBox.SelectedValue = GetAccountIdByName(selected.ToAccount);

                AmountBox.Text = selected.Amount.ToString("N2");
                NarrationBox.Text = selected.Narration;
            }
            else
            {
                MessageBox.Show("Select a row to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using (var con = new SqlConnection(Settings.Default.strConnetionString))
                    {
                        con.Open();
                        var cmd = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId=@id", con);
                        cmd.Parameters.AddWithValue("@id", selected.VchrId);
                        cmd.ExecuteNonQuery();
                    }
                    ClearForm();
                    LoadVouchers();
                }
            }
            else
            {
                MessageBox.Show("Select a row to delete.");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            editingVchrId = null;
            UpDownBox.Value = 0;
            DatePickBox.SelectedDate = DateTime.Now;
            FromAccCreditBox.SelectedIndex = -1;
            ToAccDebitBox.SelectedIndex = -1;
            AmountBox.Text = "0.00";
            NarrationBox.Text = "";
        }
        #endregion

        #region Helpers
        private int GetAccountIdByName(string name)
        {
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountId FROM Account WHERE AccountName=@name", con);
                cmd.Parameters.AddWithValue("@name", name);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void Amount_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            
            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.

            // Allow only digits and optionally one dot for decimal
            //if (e.Text.All(char.IsDigit))
            //{
            //    // Input is valid
            //    e.Handled = false; // allow input
            //}
            //else
            //{
            //    // Input is invalid
            //    e.Handled = true; // block input
            //    MessageBox.Show("Please Enter Valid Numeric Data");
            //}
        
                 
        }
        #endregion

        private void JournalDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                editingVchrId = selected.VchrId;
                UpDownBox.Value = selected.VoucherNo;
                DatePickBox.SelectedDate = selected.Date;

                FromAccCreditBox.SelectedValue = GetAccountIdByName(selected.FromAccount);
                ToAccDebitBox.SelectedValue = GetAccountIdByName(selected.ToAccount);

                AmountBox.Text = selected.Amount.ToString("N2");
                NarrationBox.Text = selected.Narration;
            }
            else
            {
                MessageBox.Show("Select a row to edit.");
            }
        }
    }
}
