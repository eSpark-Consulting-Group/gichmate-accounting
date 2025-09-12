using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class CreditNoteView : UserControl
    {
        private string connectionString = "Server=DESKTOP-E09TG4T;Database=PSVAT5;Trusted_Connection=True;TrustServerCertificate=True;";
        private int? _selectedVchrTranId = null;

        public CreditNoteView()
        {
            InitializeComponent();
            LoadAccounts();
            LoadGrid();
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();

                    string query = @"
                    SELECT 
                        v.VchrTranId,
                        v.VchrNo AS VoucherNo,
                        CONVERT(varchar(10), v.VchrDate, 120) AS [Date],
                        ISNULL(accTo.AccountName,'') AS AccountName,
                        ISNULL(accFrom.AccountName,'') AS SupplierName,
                        ISNULL(v.VchrAmount,0) AS Amount,
                        ISNULL(v.Narration,'') AS Narration
                    FROM VchrTranLog v
                    LEFT JOIN Account accFrom ON accFrom.AccountId = v.FromAccountId
                    LEFT JOIN Account accTo ON accTo.AccountId = v.ToAccountId
                    WHERE v.VchrType = 'Credit Note'
                    ORDER BY v.VchrNo DESC";

                    new SqlDataAdapter(query, con).Fill(dt);
                    dgCreditNotes.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                    INSERT INTO VchrTranLog 
                    (VchrNo, VchrDate, FromAccountId, ToAccountId, VchrAmount, Narration, VchrType)
                    VALUES 
                    (@VchrNo, @VchrDate, @FromAccountId, @ToAccountId, @VchrAmount, @Narration, 'Credit Note')";

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
                MessageBox.Show("Credit Note saved successfully.");
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                    UPDATE VchrTranLog
                    SET VchrNo = @VchrNo,
                        VchrDate = @VchrDate,
                        FromAccountId = @FromAccountId,
                        ToAccountId = @ToAccountId,
                        VchrAmount = @VchrAmount,
                        Narration = @Narration
                    WHERE VchrTranId = @VchrTranId AND VchrType = 'Credit Note'";

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
                MessageBox.Show("Credit Note updated successfully.");
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId AND VchrType = 'Credit Note'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VchrTranId", _selectedVchrTranId.Value);
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                LoadGrid();
                MessageBox.Show("Credit Note deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data: " + ex.Message);
            }
        }

        private void dgCreditNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCreditNotes.SelectedItem == null) return;

            DataRowView row = dgCreditNotes.SelectedItem as DataRowView;
            if (row == null) return;

            _selectedVchrTranId = row["VchrTranId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["VchrTranId"]);

            numVoucherNo.Value = Convert.ToInt32(row["VoucherNo"]);
            dpDate.SelectedDate = DateTime.TryParse(row["Date"].ToString(), out DateTime date) ? date : (DateTime?)null;
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
            txtAmount.Text = "0.00";
            txtNarration.Clear();
            _selectedVchrTranId = null;
        }
    }
}
