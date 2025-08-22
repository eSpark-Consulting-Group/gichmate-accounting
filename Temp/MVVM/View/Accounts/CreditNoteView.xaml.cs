using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class CreditNoteView : UserControl
    {
        private string connectionString = "Server=DESKTOP-E09TG4T;Database=PSVAT5;Trusted_Connection=True;TrustServerCertificate=True;";
        private List<Voucher> vouchers = new List<Voucher>();
        private int? currentVoucherId = null;

        public CreditNoteView()
        {
            InitializeComponent();
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            dtpDate.SelectedDate = DateTime.Now;
            LoadAccounts();
            LoadVouchers();
            GenerateNextVoucherNo();
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load Party Accounts (Customers/Suppliers)
                    string partyQuery = @"SELECT AccountId, AccountName FROM Account 
                                        WHERE AGroupId IN (SELECT AGroupId FROM AccountGroup 
                                                          WHERE GroupName IN ('SUNDRY DEBTORS', 'SUNDRY CREDITORS'))";
                    SqlCommand partyCmd = new SqlCommand(partyQuery, conn);
                    SqlDataAdapter partyDa = new SqlDataAdapter(partyCmd);
                    DataTable partyDt = new DataTable();
                    partyDa.Fill(partyDt);
                    cmbPartyAccount.ItemsSource = partyDt.DefaultView;

                    // Load Credit Note Accounts
                    string creditQuery = @"SELECT AccountId, AccountName FROM Account 
                                         WHERE AGroupId IN (SELECT AGroupId FROM AccountGroup 
                                                           WHERE GroupName LIKE '%CREDIT NOTE%' OR GroupName LIKE '%DISCOUNT%')";
                    SqlCommand creditCmd = new SqlCommand(creditQuery, conn);
                    SqlDataAdapter creditDa = new SqlDataAdapter(creditCmd);
                    DataTable creditDt = new DataTable();
                    creditDa.Fill(creditDt);
                    cmbCreditAccount.ItemsSource = creditDt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}");
            }
        }

        private void LoadVouchers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT v.VchrTranId, v.VchrNo, v.VchrDate, v.VchrAmount, v.Narration,
                                    a1.AccountName as PartyAccountName, a2.AccountName as CreditAccountName
                                    FROM VchrTranLog v
                                    INNER JOIN Account a1 ON v.FromAccountId = a1.AccountId
                                    INNER JOIN Account a2 ON v.ToAccountId = a2.AccountId
                                    WHERE v.VchrType = 'Credit Note'
                                    ORDER BY v.VchrDate DESC, v.VchrNo DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    vouchers.Clear();
                    int sl = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        vouchers.Add(new Voucher
                        {
                            SL = sl++,
                            VchrTranId = Convert.ToInt32(row["VchrTranId"]),
                            VchrNo = Convert.ToInt32(row["VchrNo"]),
                            VchrDate = Convert.ToDateTime(row["VchrDate"]),
                            VchrAmount = Convert.ToDecimal(row["VchrAmount"]),
                            Narration = row["Narration"].ToString(),
                            PartyAccountName = row["PartyAccountName"].ToString(),
                            CreditAccountName = row["CreditAccountName"].ToString()
                        });
                    }

                    dgVouchers.ItemsSource = vouchers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vouchers: {ex.Message}");
            }
        }

        private void GenerateNextVoucherNo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ISNULL(MAX(VchrNo), 0) + 1 FROM VchrTranLog WHERE VchrType = 'Credit Note'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int nextNo = Convert.ToInt32(cmd.ExecuteScalar());
                    txtVoucherNo.Text = nextNo.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating voucher number: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                int partyAccountId = (int)cmbPartyAccount.SelectedValue;
                int creditAccountId = (int)cmbCreditAccount.SelectedValue;
                DateTime date = dtpDate.SelectedDate ?? DateTime.Now;
                int voucherNo = int.Parse(txtVoucherNo.Text);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (currentVoucherId == null) // New voucher
                    {
                        cmd = new SqlCommand(@"INSERT INTO VchrTranLog 
                                            (VchrNo, VchrName, VchrType, VchrAmount, VchrDate, 
                                             FromAccountId, ToAccountId, TranDate, UserId, Narration, FK_BranchId)
                                            VALUES 
                                            (@VchrNo, @VchrName, @VchrType, @VchrAmount, @VchrDate,
                                             @FromAccountId, @ToAccountId, @TranDate, @UserId, @Narration, @FK_BranchId);
                                            SELECT SCOPE_IDENTITY();", conn);
                    }
                    else // Update existing
                    {
                        // First delete transaction logs
                        DeleteTransactionLogs(currentVoucherId.Value);

                        cmd = new SqlCommand(@"UPDATE VchrTranLog SET 
                                            VchrNo = @VchrNo, VchrAmount = @VchrAmount, VchrDate = @VchrDate,
                                            FromAccountId = @FromAccountId, ToAccountId = @ToAccountId,
                                            Narration = @Narration, TranDate = @TranDate
                                            WHERE VchrTranId = @VchrTranId", conn);
                        cmd.Parameters.AddWithValue("@VchrTranId", currentVoucherId);
                    }

                    cmd.Parameters.AddWithValue("@VchrNo", voucherNo);
                    cmd.Parameters.AddWithValue("@VchrName", "Credit Note");
                    cmd.Parameters.AddWithValue("@VchrType", "Credit Note");
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@VchrDate", date);
                    cmd.Parameters.AddWithValue("@FromAccountId", partyAccountId);
                    cmd.Parameters.AddWithValue("@ToAccountId", creditAccountId);
                    cmd.Parameters.AddWithValue("@TranDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", 1); // Replace with actual user ID
                    cmd.Parameters.AddWithValue("@Narration", txtNarration.Text);
                    cmd.Parameters.AddWithValue("@FK_BranchId", 1); // Replace with actual branch ID

                    if (currentVoucherId == null)
                    {
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        CreateTransactionLogs(newId, voucherNo, partyAccountId, creditAccountId, amount, date);
                        MessageBox.Show("Credit Note saved successfully!");
                        currentVoucherId = newId;
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        CreateTransactionLogs(currentVoucherId.Value, voucherNo, partyAccountId, creditAccountId, amount, date);
                        MessageBox.Show("Credit Note updated successfully!");
                    }

                    ClearForm();
                    LoadVouchers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving credit note: {ex.Message}");
            }
        }

        private void CreateTransactionLogs(int vchrTranId, int vchrNo, int fromAccountId, int toAccountId, decimal amount, DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Main entry: Debit Party, Credit Credit Account
                string mainQuery = @"INSERT INTO TransactionLog 
                                   (VchrId, VchrNo, VchrType, TranDate, AccountId, 
                                    DebitAmount, CreditAmount, Narration, UserId, BranchId)
                                   VALUES 
                                   (@VchrId, @VchrNo, @VchrType, @TranDate, @FromAccountId,
                                    @DebitAmount, 0, @Narration, @UserId, @BranchId)";

                SqlCommand mainCmd = new SqlCommand(mainQuery, conn);
                mainCmd.Parameters.AddWithValue("@VchrId", vchrTranId);
                mainCmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                mainCmd.Parameters.AddWithValue("@VchrType", "Credit Note");
                mainCmd.Parameters.AddWithValue("@TranDate", date);
                mainCmd.Parameters.AddWithValue("@FromAccountId", fromAccountId);
                mainCmd.Parameters.AddWithValue("@DebitAmount", amount);
                mainCmd.Parameters.AddWithValue("@Narration", txtNarration.Text);
                mainCmd.Parameters.AddWithValue("@UserId", 1);
                mainCmd.Parameters.AddWithValue("@BranchId", 1);
                mainCmd.ExecuteNonQuery();

                // Sub entry: Credit Party, Debit Credit Account
                string subQuery = @"INSERT INTO TransactionLog 
                                  (VchrId, VchrNo, VchrType, TranDate, AccountId, 
                                   DebitAmount, CreditAmount, Narration, UserId, BranchId)
                                  VALUES 
                                  (@VchrId, @VchrNo, @VchrType, @TranDate, @ToAccountId,
                                   0, @CreditAmount, @Narration, @UserId, @BranchId)";

                SqlCommand subCmd = new SqlCommand(subQuery, conn);
                subCmd.Parameters.AddWithValue("@VchrId", vchrTranId);
                subCmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                subCmd.Parameters.AddWithValue("@VchrType", "Credit Note");
                subCmd.Parameters.AddWithValue("@TranDate", date);
                subCmd.Parameters.AddWithValue("@ToAccountId", toAccountId);
                subCmd.Parameters.AddWithValue("@CreditAmount", amount);
                subCmd.Parameters.AddWithValue("@Narration", txtNarration.Text);
                subCmd.Parameters.AddWithValue("@UserId", 1);
                subCmd.Parameters.AddWithValue("@BranchId", 1);
                subCmd.ExecuteNonQuery();
            }
        }

        private void DeleteTransactionLogs(int vchrTranId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM TransactionLog WHERE VchrId = @VchrId AND VchrType = 'Credit Note'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@VchrId", vchrTranId);
                cmd.ExecuteNonQuery();
            }
        }

        private bool ValidateInput()
        {
            if (cmbPartyAccount.SelectedValue == null)
            {
                MessageBox.Show("Please select a party/supplier");
                return false;
            }

            if (cmbCreditAccount.SelectedValue == null)
            {
                MessageBox.Show("Please select a credit account");
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero");
                return false;
            }

            if (dtpDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a date");
                return false;
            }

            return true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgVouchers.SelectedItem is Voucher selectedVoucher)
            {
                currentVoucherId = selectedVoucher.VchrTranId;
                txtVoucherNo.Text = selectedVoucher.VchrNo.ToString();
                dtpDate.SelectedDate = selectedVoucher.VchrDate;
                txtAmount.Text = selectedVoucher.VchrAmount.ToString("N2");
                txtNarration.Text = selectedVoucher.Narration;

                // Set combobox values
                foreach (DataRowView item in cmbPartyAccount.Items)
                {
                    if (item["AccountName"].ToString() == selectedVoucher.PartyAccountName)
                    {
                        cmbPartyAccount.SelectedValue = item["AccountId"];
                        break;
                    }
                }

                foreach (DataRowView item in cmbCreditAccount.Items)
                {
                    if (item["AccountName"].ToString() == selectedVoucher.CreditAccountName)
                    {
                        cmbCreditAccount.SelectedValue = item["AccountId"];
                        break;
                    }
                }

                btnSave.Content = "Update";
            }
            else
            {
                MessageBox.Show("Please select a voucher to edit");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgVouchers.SelectedItem is Voucher selectedVoucher)
            {
                if (MessageBox.Show("Are you sure you want to delete this credit note?", "Confirm Delete",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DeleteTransactionLogs(selectedVoucher.VchrTranId);

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@VchrTranId", selectedVoucher.VchrTranId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Credit Note deleted successfully!");
                        ClearForm();
                        LoadVouchers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting credit note: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a voucher to delete");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            currentVoucherId = null;
            txtVoucherNo.Text = "";
            dtpDate.SelectedDate = DateTime.Now;
            txtAmount.Text = "0.00";
            txtNarration.Text = "";
            cmbPartyAccount.SelectedIndex = -1;
            cmbCreditAccount.SelectedIndex = -1;
            btnSave.Content = "Save";
            GenerateNextVoucherNo();
        }

        private void dgVouchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Add selection handling if needed
        }
    }

    public class Voucher
    {
        public int SL { get; set; }
        public int VchrTranId { get; set; }
        public int VchrNo { get; set; }
        public DateTime VchrDate { get; set; }
        public decimal VchrAmount { get; set; }
        public string Narration { get; set; }
        public string PartyAccountName { get; set; }
        public string CreditAccountName { get; set; }
    }
}