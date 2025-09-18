using AccountsLibrary.Properties;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccountsPRO.MVVM.View.Accounts
{
    public partial class CashPaymentView : UserControl
    {
        private int? _currentVchrTranId = null;
        private const string VCHR_TYPE = "Payment Voucher";
        private const string VCHR_NAME = "Cash Payment";

        public CashPaymentView()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            VoucherDateBox.SelectedDate = DateTime.Today;
            LoadAccounts();
            LoadEmployees();
            LoadFilterEmployees();
            ResetForm();
            LoadGrid();
        }

        private void LoadAccounts()
        {
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn);
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            AccountCombo.ItemsSource = dt.DefaultView;
            AccountCombo.DisplayMemberPath = "AccountName";
            AccountCombo.SelectedValuePath = "AccountId";

            BankAccountCombo.ItemsSource = dt.DefaultView;
            BankAccountCombo.DisplayMemberPath = "AccountName";
            BankAccountCombo.SelectedValuePath = "AccountId";
        }

        private void LoadEmployees()
        {
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn);
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            PaidByCombo.ItemsSource = dt.DefaultView;
            PaidByCombo.DisplayMemberPath = "EmpName";
            PaidByCombo.SelectedValuePath = "EmpID";
        }

        private void LoadFilterEmployees()
        {
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn);
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var dt2 = dt.Clone();
            var dr = dt2.NewRow();
            dr["EmpID"] = DBNull.Value;
            dr["EmpName"] = "All";
            dt2.Rows.Add(dr);

            foreach (DataRow r in dt.Rows) dt2.ImportRow(r);

            PaidByFilterCombo.ItemsSource = dt2.DefaultView;
            PaidByFilterCombo.DisplayMemberPath = "EmpName";
            PaidByFilterCombo.SelectedValuePath = "EmpID";
            PaidByFilterCombo.SelectedIndex = 0;
        }

        private void LoadGrid()
        {
            var dt = new DataTable();
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand(@"
                SELECT v.VchrTranId, v.VchrNo, v.VchrDate, v.VchrAmount,
                       v.FromAccountId, fa.AccountName AS FromAccountName,
                       v.ToAccountId, ta.AccountName AS ToAccountName,
                       v.SalesManId, ed.EmpName, v.Narration
                FROM VchrTranLog v
                LEFT JOIN Account fa ON fa.AccountId = v.FromAccountId
                LEFT JOIN Account ta ON ta.AccountId = v.ToAccountId
                LEFT JOIN EmployeeDetails ed ON ed.EmpID = v.SalesManId
                WHERE v.VchrType = @VchrType
                  AND v.VchrName = @VchrName
                  AND (@SalesManId IS NULL OR v.SalesManId = @SalesManId)
                  AND (@VchrDate IS NULL OR CONVERT(date, v.VchrDate) = @VchrDate)
                ORDER BY v.VchrDate DESC, v.VchrNo DESC
            ", cn);

            cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
            cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
            cmd.Parameters.AddWithValue("@SalesManId", PaidByFilterCombo.SelectedValue ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate.HasValue ? (object)VoucherDateBox.SelectedDate.Value.Date : DBNull.Value);

            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var dtWithRow = dt.Clone();
            dtWithRow.Columns.Add("RowNumber", typeof(int));
            int r = 1;
            foreach (DataRow row in dt.Rows)
            {
                var nr = dtWithRow.NewRow();
                foreach (DataColumn c in dt.Columns) nr[c.ColumnName] = row[c.ColumnName];
                nr["RowNumber"] = r++;
                dtWithRow.Rows.Add(nr);
            }

            dgCashPayments.ItemsSource = dtWithRow.DefaultView;


            decimal total = dtWithRow.AsEnumerable().Sum(row => row.Field<decimal?>("VchrAmount") ?? 0);
            TotalText.Text = total.ToString("N2", CultureInfo.InvariantCulture);
        }

        private void UpdateBankBalanceDisplay()
        {
            if (BankAccountCombo.SelectedValue == null)
            {
                BankBalanceText.Text = "0.00";
                return;
            }
            int accountId = Convert.ToInt32(BankAccountCombo.SelectedValue);
            DateTime asOf = VoucherDateBox.SelectedDate ?? DateTime.Today;
            BankBalanceText.Text = GetAccountBalance(accountId, asOf).ToString("N2", CultureInfo.InvariantCulture);
        }

        private decimal GetAccountBalance(int accountId, DateTime asOfDate)
        {
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand(@"
                SELECT ISNULL(SUM(DebitAmount),0) AS DebitSum, ISNULL(SUM(CreditAmount),0) AS CreditSum
                FROM TransactionLog
                WHERE (AccountId = @AccountId OR OppAccountId = @AccountId)
                  AND CONVERT(date, TranDate) <= @AsOfDate
            ", cn);
            cmd.Parameters.AddWithValue("@AccountId", accountId);
            cmd.Parameters.AddWithValue("@AsOfDate", asOfDate.Date);
            cn.Open();
            using var rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                decimal debit = rdr.IsDBNull(0) ? 0m : rdr.GetDecimal(0);
                decimal credit = rdr.IsDBNull(1) ? 0m : rdr.GetDecimal(1);
                return debit - credit;
            }
            return 0m;
        }



        #region Save / Edit / Delete / Clear

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            if (VoucherNoBox.Value == null || VoucherNoBox.Value == 0)
            {
                MessageBox.Show("Voucher number is required.");
                return;
            }

            if (string.IsNullOrEmpty(AccountCombo.Text))
            {
                MessageBox.Show("Account name is required");
                return;
            }

            if (string.IsNullOrEmpty(PaidByCombo.Text))
            {
                MessageBox.Show("Paid By field is required");
                return;
            }

            if (string.IsNullOrEmpty(BankAccountCombo.Text))
            {
                MessageBox.Show("Cash Account Name is required"); //CashAccountCombo but called BankAcountCombo to reduce the complexity or re0implementing/creating
                return;
            }

            if (!decimal.TryParse(AmountBox.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal amount) || amount <= 0m)
            {
                MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (amount > 99999999.99m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }

            decimal discount = 0;
            if (DiscountChk.IsChecked == true && !decimal.TryParse(DiscountBox.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out discount))
            {
                MessageBox.Show("Enter valid discount.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            int fromAccountId = AccountCombo.SelectedValue != null ? Convert.ToInt32(AccountCombo.SelectedValue) : 0;
            int toAccountId = BankAccountCombo.SelectedValue != null ? Convert.ToInt32(BankAccountCombo.SelectedValue) : 0;
            int? salesManId = PaidByCombo.SelectedValue != null ? (int?)Convert.ToInt32(PaidByCombo.SelectedValue) : null;
            DateTime vDate = VoucherDateBox.SelectedDate ?? DateTime.Today;
            string narration = NarrationBox.Text ?? string.Empty;

            try
            {
                if ((string)SaveBtn.Content == "Save") InsertVoucher(fromAccountId, toAccountId, salesManId, vDate, amount, discount, narration);
                else UpdateVoucher(fromAccountId, toAccountId, salesManId, vDate, amount, discount, narration);

                MessageBox.Show(((string)SaveBtn.Content == "Save") ? "Saved successfully." : "Updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ResetForm();
            LoadGrid();
        }

        private void InsertVoucher(int fromAccountId, int toAccountId, int? salesManId, DateTime vDate, decimal amount, decimal discount, string narration)
        {
            int next = GetNextVoucherNo();
            if (next <= 0) next = 1;

            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            cn.Open();
            using var tx = cn.BeginTransaction();
            try
            {
                int insertedVchrId;
                using (var cmd = new SqlCommand(@"
                    INSERT INTO VchrTranLog (VchrNo, VchrDate, VchrAmount, VchrType, VchrName, SalesManId, FromAccountId, ToAccountId, TranDate, UserId, Narration)
                    VALUES (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @VchrName, @SalesManId, @FromAccountId, @ToAccountId, GETDATE(), NULL, @Narration);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                ", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@VchrNo", next);
                    cmd.Parameters.AddWithValue("@VchrDate", vDate);
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                    cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                    cmd.Parameters.AddWithValue("@Narration", narration);

                    insertedVchrId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                _currentVchrTranId = insertedVchrId;

                // Debit / Credit Transaction Log
                InsertTransaction(cn, tx, insertedVchrId, next, fromAccountId, toAccountId, amount, narration);

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private void UpdateVoucher(int fromAccountId, int toAccountId, int? salesManId, DateTime vDate, decimal amount, decimal discount, string narration)
        {
            if (!_currentVchrTranId.HasValue) throw new Exception("Select a voucher to update.");
            int vchrNo = Convert.ToInt32(VoucherNoBox.Value);

            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            cn.Open();
            using var tx = cn.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand(@"
                    UPDATE VchrTranLog
                    SET VchrNo=@VchrNo, VchrDate=@VchrDate, VchrAmount=@VchrAmount, SalesManId=@SalesManId,
                        FromAccountId=@FromAccountId, ToAccountId=@ToAccountId, Narration=@Narration
                    WHERE VchrTranId=@VchrTranId
                ", cn, tx);

                cmd.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                cmd.Parameters.AddWithValue("@VchrAmount", amount);
                cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                cmd.Parameters.AddWithValue("@Narration", narration);
                cmd.ExecuteNonQuery();

                using var cmdDel = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId=@VchrId", cn, tx);
                cmdDel.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                cmdDel.ExecuteNonQuery();

                InsertTransaction(cn, tx, _currentVchrTranId.Value, vchrNo, fromAccountId, toAccountId, amount, narration);

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        private void InsertTransaction(SqlConnection cn, SqlTransaction tx, int vchrId, int vchrNo, int fromAccountId, int toAccountId, decimal amount, string narration)
        {
            // Credit entry
            using (var cmd = new SqlCommand(@"
                INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                VALUES (@VchrId,@VchrNo,@VchrType,@VchrDate,@AccountId,@OppAccountId,@DebitAmount,@CreditAmount,@Narration,GETDATE(),@VchrName)
            ", cn, tx))
            {
                cmd.Parameters.AddWithValue("@VchrId", vchrId);
                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate ?? DateTime.Today);
                cmd.Parameters.AddWithValue("@AccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                cmd.Parameters.AddWithValue("@OppAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                cmd.Parameters.AddWithValue("@DebitAmount", 0m);
                cmd.Parameters.AddWithValue("@CreditAmount", amount);
                cmd.Parameters.AddWithValue("@Narration", narration);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                cmd.ExecuteNonQuery();
            }

            // Debit entry
            using (var cmd = new SqlCommand(@"
                INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                VALUES (@VchrId,@VchrNo,@VchrType,@VchrDate,@AccountId,@OppAccountId,@DebitAmount,@CreditAmount,@Narration,GETDATE(),@VchrName)
            ", cn, tx))
            {
                cmd.Parameters.AddWithValue("@VchrId", vchrId);
                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate ?? DateTime.Today);
                cmd.Parameters.AddWithValue("@AccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                cmd.Parameters.AddWithValue("@OppAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                cmd.Parameters.AddWithValue("@DebitAmount", amount);
                cmd.Parameters.AddWithValue("@CreditAmount", 0m);
                cmd.Parameters.AddWithValue("@Narration", narration);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                cmd.ExecuteNonQuery();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentVchrTranId.HasValue) SaveBtn.Content = "Update";
            else MessageBox.Show("Select a voucher to edit.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentVchrTranId.HasValue)
            {
                MessageBox.Show("Select a voucher to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var res = MessageBox.Show("Confirm delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes) return;

            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            cn.Open();
            using var tx = cn.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId=@VchrId", cn, tx);
                cmd.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                cmd.ExecuteNonQuery();

                using var cmd2 = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId=@VchrTranId", cn, tx);
                cmd2.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                cmd2.ExecuteNonQuery();

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }

            MessageBox.Show("Deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetForm();
            LoadGrid();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e) => ResetForm();

        #endregion

        private void ResetForm()
        {
            _currentVchrTranId = null;
            VoucherDateBox.SelectedDate = DateTime.Today;
            AmountBox.Text = "0.00";
            DiscountBox.Text = "0.00";
            NarrationBox.Text = string.Empty;
            PaidByCombo.SelectedIndex = -1;
            AccountCombo.SelectedIndex = -1;
            BankAccountCombo.SelectedIndex = -1;
            SaveBtn.Content = "Save";
            VoucherNoBox.Value = GetNextVoucherNo();
            UpdateBankBalanceDisplay();
        }

        private int GetNextVoucherNo()
        {
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand(@"
                SELECT ISNULL(MAX(VchrNo),0) + 1 FROM VchrTranLog
                WHERE VchrType=@VchrType AND VchrName=@VchrName
            ", cn);
            cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
            cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
            cn.Open();
            var o = cmd.ExecuteScalar();
            return o != null ? Convert.ToInt32(o) : 1;
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e) => LoadGrid();
        private void VoucherDateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => LoadGrid();
        private void BankAccountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateBankBalanceDisplay();


        private void dgCashPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCashPayments.SelectedItem == null) return;

            var drv = dgCashPayments.SelectedItem as DataRowView;
            if (drv == null) return;

            _currentVchrTranId = drv["VchrTranId"] == DBNull.Value ? (int?)null : Convert.ToInt32(drv["VchrTranId"]);
            VoucherNoBox.Value = drv["VchrNo"] == DBNull.Value ? 0 : Convert.ToInt32(drv["VchrNo"]);
            VoucherDateBox.SelectedDate = drv["VchrDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["VchrDate"]);
            AmountBox.Text = drv["VchrAmount"] == DBNull.Value ? "0.00" : Convert.ToDecimal(drv["VchrAmount"]).ToString("N2", CultureInfo.InvariantCulture);
            NarrationBox.Text = drv["Narration"]?.ToString() ?? string.Empty;

            if (drv["FromAccountId"] != DBNull.Value) AccountCombo.SelectedValue = Convert.ToInt32(drv["FromAccountId"]);
            else AccountCombo.SelectedIndex = -1;

            if (drv["ToAccountId"] != DBNull.Value) BankAccountCombo.SelectedValue = Convert.ToInt32(drv["ToAccountId"]);
            else BankAccountCombo.SelectedIndex = -1;

            if (drv["SalesManId"] != DBNull.Value) PaidByCombo.SelectedValue = Convert.ToInt32(drv["SalesManId"]);
            else PaidByCombo.SelectedIndex = -1;

            SaveBtn.Content = "Update";
            UpdateBankBalanceDisplay();
        }
    }
}
