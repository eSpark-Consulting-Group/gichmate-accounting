using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using AccountsLibrary.Properties;

namespace AccountsPRO.MVVM.View.Accounts
{
    public partial class DebitNoteView : UserControl
    {
        private int? _selectedVchrTranId = null;

        public DebitNoteView()
        {
            InitializeComponent();
            LoadAccounts();
            LoadGrid();
            dpDate.SelectedDate = DateTime.Now;
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    DataTable dtAccounts = new DataTable();
                    new SqlDataAdapter("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", con).Fill(dtAccounts);

                    cmbParty.ItemsSource = dtAccounts.DefaultView;
                    cmbParty.DisplayMemberPath = "AccountName";
                    cmbParty.SelectedValuePath = "AccountId";

                    cmbAccount.ItemsSource = dtAccounts.DefaultView;
                    cmbAccount.DisplayMemberPath = "AccountName";
                    cmbAccount.SelectedValuePath = "AccountId";

                  //  cmbCashAccount.ItemsSource = dtAccounts.DefaultView;
                   // cmbCashAccount.DisplayMemberPath = "AccountName";
                   // cmbCashAccount.SelectedValuePath = "AccountId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading accounts: " + ex.Message);
            }
        }

        private void LoadGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();

                    // Updated query to match your database schema
                    string query = @"
                    SELECT 
                        v.VchrTranId,
                        v.VchrNo        AS VoucherNo,
                        CONVERT(varchar(10), v.VchrDate, 120) AS [Date],
                        ISNULL(accTo.AccountName,'')   AS AccountName,
                        ISNULL(accFrom.AccountName,'') AS SupplierName,
                        ISNULL(v.VchrAmount,0)         AS Amount,
                        ISNULL(v.Narration,'')         AS Narration,
                        '' AS CashAccount  -- Placeholder since your table doesn't have CashAccountId
                    FROM VchrTranLog v
                    LEFT JOIN Account accFrom ON accFrom.AccountId = v.FromAccountId
                    LEFT JOIN Account accTo   ON accTo.AccountId   = v.ToAccountId
                    WHERE v.VchrType = 'Debit Note'
                    ORDER BY v.VchrNo DESC";

                    new SqlDataAdapter(query, con).Fill(dt);
                    dgDebitNotes.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (numVoucherNo.Value == null || numVoucherNo.Value == 0)
            {
                MessageBox.Show("Voucher number is required.");
                return;
            }

            if (string.IsNullOrEmpty(cmbParty.Text))
            {
                MessageBox.Show("Party Supplier is required");
                return;
            }

            if (string.IsNullOrEmpty(cmbAccount.Text))
            {
                MessageBox.Show("Account Name is required");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid Amount greater than zero.");
                return;
            }

            else if (amount > 99999999.99m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }


            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    // Updated query to match your database schema (without CashAccountId)
                    string query = @"
                    INSERT INTO VchrTranLog 
                    (VchrNo, VchrDate, FromAccountId, ToAccountId, VchrAmount, Narration, VchrType)
                    VALUES 
                    (@VchrNo, @VchrDate, @FromAccountId, @ToAccountId, @VchrAmount, @Narration, 'Debit Note')";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VchrNo", numVoucherNo.Value);
                    cmd.Parameters.AddWithValue("@VchrDate", dpDate.SelectedDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@FromAccountId", cmbAccount.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToAccountId", cmbParty.SelectedValue);
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@Narration", txtNarration.Text);

                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadGrid();
                MessageBox.Show("Debit Note saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedVchrTranId == null)
            {
                MessageBox.Show("Select a record to update.");
                return;
            }

            if (cmbParty.SelectedIndex < 0 || cmbAccount.SelectedIndex < 0)
            {
                MessageBox.Show("Select Party and Account.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter valid amount.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    // Updated query to match your database schema (without CashAccountId)
                    string query = @"
                    UPDATE VchrTranLog
                    SET VchrNo = @VchrNo,
                        VchrDate = @VchrDate,
                        FromAccountId = @FromAccountId,
                        ToAccountId = @ToAccountId,
                        VchrAmount = @VchrAmount,
                        Narration = @Narration
                    WHERE VchrTranId = @VchrTranId AND VchrType = 'Debit Note'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VchrTranId", _selectedVchrTranId.Value);
                    cmd.Parameters.AddWithValue("@VchrNo", numVoucherNo.Value);
                    cmd.Parameters.AddWithValue("@VchrDate", dpDate.SelectedDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@FromAccountId", cmbAccount.SelectedValue);
                    cmd.Parameters.AddWithValue("@ToAccountId", cmbParty.SelectedValue);
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@Narration", txtNarration.Text);

                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadGrid();
                MessageBox.Show("Debit Note updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedVchrTranId == null)
            {
                MessageBox.Show("Select a record to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Settings.Default.strConnetionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId AND VchrType = 'Debit Note'", con);
                    cmd.Parameters.AddWithValue("@VchrTranId", _selectedVchrTranId.Value);
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadGrid();
                MessageBox.Show("Debit Note deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data: " + ex.Message);
            }
        }

        private void dgDebitNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDebitNotes.SelectedItem == null) return;

            DataRowView row = dgDebitNotes.SelectedItem as DataRowView;
            if (row == null) return;

            _selectedVchrTranId = row["VchrTranId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["VchrTranId"]);

            numVoucherNo.Value = Convert.ToInt32(row["VoucherNo"]);
            dpDate.SelectedDate = DateTime.TryParse(row["Date"].ToString(), out DateTime date) ? date : (DateTime?)null;

            // Set combo box values
            cmbParty.Text = row["SupplierName"].ToString();
            cmbAccount.Text = row["AccountName"].ToString();

            txtAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("N2");
            txtNarration.Text = row["Narration"].ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            numVoucherNo.Value = 0;
            dpDate.SelectedDate = DateTime.Now;
            cmbParty.SelectedIndex = -1;
            cmbAccount.SelectedIndex = -1;
           // cmbCashAccount.SelectedIndex = -1;
            txtAmount.Text = "0.00";
            txtNarration.Clear();
            _selectedVchrTranId = null;
        }

        //private void ToggleFields_Click(object sender, RoutedEventArgs e)
        //{
        //    if (FieldContainer.Visibility == Visibility.Visible)
        //        FieldContainer.Visibility = Visibility.Collapsed;
        //    else
        //        FieldContainer.Visibility = Visibility.Visible;
        //}
         
    }
}