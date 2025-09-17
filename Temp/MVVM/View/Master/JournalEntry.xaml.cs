using AccountsLibrary.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlexMold.MVVM.View.Master
{
    public partial class JournalEntry : Window
    {

        public JournalEntry()
        {
            InitializeComponent();
            LoadPartyAccounts();
            DatePickBox.SelectedDate = DateTime.Now;
            VoucherNoBox.Value = GetNextVoucherNumber();
        }

        private void LoadPartyAccounts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();
                    string query = "SELECT AccountId, AccountName FROM Account ORDER BY AccountName";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    PartyAccountBox.ItemsSource = dt.DefaultView;
                    PartyAccountBox.DisplayMemberPath = "AccountName";
                    PartyAccountBox.SelectedValuePath = "AccountId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading party accounts: " + ex.Message);
            }
        }

        private int GetNextVoucherNumber()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(VchrNo), 0) + 1 FROM VchrTranLog WHERE VchrType='Journal Voucher'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return 1;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PartyAccountBox.SelectedIndex = -1;
            DebitAmountBox.Text = "0.00";
            CreditAmountBox.Text = "0.00";
            NarrationBox.Text = "";
            DatePickBox.SelectedDate = DateTime.Now;
            VoucherNoBox.Value = GetNextVoucherNumber();
            PartyAccountBox.Focus();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (PartyAccountBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a Party Account.");
                return;
            }

            if (!decimal.TryParse(DebitAmountBox.Text, out decimal debit))
                debit = 0;
            if (!decimal.TryParse(CreditAmountBox.Text, out decimal credit))
                credit = 0;

            if (debit <= 0 && credit <= 0)
            {
                MessageBox.Show("Please enter Debit or Credit amount.");
                return;
            }
            
            else if(debit > 9999999.99m && credit > 9999999.9m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }

                int voucherNo = (int)VoucherNoBox.Value;
            DateTime vDate = DatePickBox.SelectedDate ?? DateTime.Now;
            string narration = NarrationBox.Text.Trim();
            string accountId = PartyAccountBox.SelectedValue.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    // Determine debit/credit properly
                    string fromAccount = credit > 0 ? accountId : null;
                    string toAccount = debit > 0 ? accountId : null;

                    if (fromAccount == null && toAccount == null)
                    {
                        MessageBox.Show("Invalid Debit/Credit configuration.");
                        return;
                    }

                    string query = @"INSERT INTO VchrTranLog 
                             (VchrNo, VchrDate, FromAccountId, ToAccountId, VchrAmount, Narration, VchrType)
                             VALUES (@VNo, @VDate, @FromAcc, @ToAcc, @Amount, @Narration, 'Journal Voucher')";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@VNo", voucherNo);
                        cmd.Parameters.AddWithValue("@VDate", vDate);
                        cmd.Parameters.AddWithValue("@FromAcc", (object)fromAccount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ToAcc", (object)toAccount ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Amount", debit > 0 ? debit : credit);
                        cmd.Parameters.AddWithValue("@Narration", narration);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("Journal Entry saved successfully!");
                        else
                            MessageBox.Show("No rows were inserted. Check your data.");
                    }
                }

                // Clear after successful save
                ClearButton_Click(null, null);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DebitAmountBox_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.
        }

        private void CreditAmountBox_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.
        }
    }
}
