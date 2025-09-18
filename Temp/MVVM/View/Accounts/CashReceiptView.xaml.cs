using AccountsLibrary.Properties;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AccountsPRO.MVVM.View.Accounts
{
    public partial class CashReceiptView : UserControl
    {
        private int? _currentVchrTranId = null;
        private const string VCHR_TYPE = "Receipt Voucher";
        private const string VCHR_NAME = "Cash Receipt";

        public CashReceiptView()
        {
            InitializeComponent();

            Loaded += CashReceiptView_Loaded;
            dgBankPayments.SelectionChanged += dgBankPayments_SelectionChanged;
            BankAccountCombo.SelectionChanged += BankAccountCombo_SelectionChanged;
            VoucherDateBox.SelectedDateChanged += VoucherDateBox_SelectedDateChanged;
            PaidByCombo.SelectionChanged += PaidByCombo_SelectionChanged;
        }

        private void CashReceiptView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VoucherDateBox.SelectedDate = DateTime.Today;
                LoadAccounts();
                LoadCashAccounts();
                LoadEmployees();
                ResetForm();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Lookups & Grid

        private void SafeBindCombo(ComboBox cmb, DataTable dt, string displayMember, string valueMember, bool selectFirst = false)
        {
            cmb.ItemsSource = null;
            cmb.Items.Clear();
            cmb.ItemsSource = dt.DefaultView;
            cmb.DisplayMemberPath = displayMember;
            cmb.SelectedValuePath = valueMember;
            cmb.SelectedIndex = selectFirst ? 0 : -1;
        }

        private void LoadAccounts()
        {
            var dt = new DataTable();
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn);
            new SqlDataAdapter(cmd).Fill(dt);

            SafeBindCombo(AccountCombo, dt, "AccountName", "AccountId");
        }

        private void LoadCashAccounts()
        {
            var dt = new DataTable();
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn);
            new SqlDataAdapter(cmd).Fill(dt);

            SafeBindCombo(BankAccountCombo, dt, "AccountName", "AccountId");
        }

        private void LoadEmployees()
        {
            var dt = new DataTable();
            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            using var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn);
            new SqlDataAdapter(cmd).Fill(dt);

            // PaidByCombo with "All"
            var dtAll = dt.Clone();
            var row = dtAll.NewRow();
            row["EmpID"] = DBNull.Value;
            row["EmpName"] = "All";
            dtAll.Rows.Add(row);
            foreach (DataRow r in dt.Rows) dtAll.ImportRow(r);
            SafeBindCombo(PaidByCombo, dtAll, "EmpName", "EmpID", selectFirst: true);

            // PaidByCombo2 without "All"
            SafeBindCombo(PaidByCombo2, dt, "EmpName", "EmpID");
        }

        private void LoadGrid()
        {
            try
            {
                var dt = new DataTable();
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand(@"
                    SELECT v.VchrTranId, v.VchrNo, v.VchrDate, v.VchrAmount,
                           v.FromAccountId, fa.AccountName AS FromAccountName,
                           v.ToAccountId, ta.AccountName AS ToAccountName,
                           v.SalesManId, ed.EmpName AS PaidBy, v.Narration
                    FROM VchrTranLog v
                    LEFT JOIN Account fa ON fa.AccountId = v.FromAccountId
                    LEFT JOIN Account ta ON ta.AccountId = v.ToAccountId
                    LEFT JOIN EmployeeDetails ed ON ed.EmpID = v.SalesManId
                    WHERE v.VchrType = @VchrType
                      AND v.VchrName = @VchrName
                      AND (@SalesManId IS NULL OR v.SalesManId = @SalesManId OR (v.SalesManId IS NULL AND @SalesManId IS NULL))
                      AND (@VchrDate IS NULL OR CONVERT(date, v.VchrDate) = @VchrDate)
                    ORDER BY v.VchrDate DESC, v.VchrNo DESC
                ", cn);

                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);

                object sel = PaidByCombo.SelectedValue;
                cmd.Parameters.AddWithValue("@SalesManId", sel != null && sel != DBNull.Value && int.TryParse(sel.ToString(), out int pid) ? pid : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate.HasValue ? VoucherDateBox.SelectedDate.Value.Date : (object)DBNull.Value);

                new SqlDataAdapter(cmd).Fill(dt);

                // Add row numbers
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

                EnsureGridColumns();
                dgBankPayments.ItemsSource = dtWithRow.DefaultView;

                decimal total = 0m;
                foreach (DataRow row in dtWithRow.Rows)
                {
                    if (decimal.TryParse(row["VchrAmount"]?.ToString(), out decimal amt)) total += amt;
                }
                TotalText.Text = total.ToString("N2", CultureInfo.InvariantCulture);
                UpdateBankBalanceDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadGrid error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnsureGridColumns()
        {
            dgBankPayments.Columns.Clear();
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "SL", Binding = new Binding("RowNumber"), Width = 50 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Voucher No", Binding = new Binding("VchrNo"), Width = 150 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("VchrDate") { StringFormat = "d" }, Width = 150 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Cash Account", Binding = new Binding("FromAccountName"), Width = 180 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Party Account", Binding = new Binding("ToAccountName"), Width = 180 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Amount", Binding = new Binding("VchrAmount") { StringFormat = "N2" }, Width = 150 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Narration", Binding = new Binding("Narration"), Width = 200 });
            dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Collected By", Binding = new Binding("PaidBy"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });

            //dgBankPayments.Columns.Add(new DataGridTextColumn { Header = "Paid By", Binding = new Binding("PaidBy"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
        }

        #endregion

        #region Account balance helpers

        private void UpdateBankBalanceDisplay()
        {
            if (BankAccountCombo.SelectedValue == null) { BankBalanceText.Text = "0.00"; return; }
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
                WHERE (AccountId = @AccountId OR OppAccountId = @AccountId) AND CONVERT(date, TranDate) <= @AsOfDate
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

        #endregion

        #region Save / Update / Delete / Clear

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
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
                    MessageBox.Show("Collected By field is required");
                    return;
                }

                if (string.IsNullOrEmpty(BankAccountCombo.Text))
                {
                    MessageBox.Show("Cash Account Name is required");
                    return;
                }

                if (!decimal.TryParse(AmountBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0m)
                {
                    MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                else if (amount > 99999999.99m)
                {
                    MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                    return;
                }


                int fromAccountId = AccountCombo.SelectedValue != null ? Convert.ToInt32(AccountCombo.SelectedValue) : 0;
                int toAccountId = BankAccountCombo.SelectedValue != null ? Convert.ToInt32(BankAccountCombo.SelectedValue) : 0;
                int? salesManId = PaidByCombo2.SelectedValue != null && PaidByCombo2.SelectedValue != DBNull.Value
                    ? (int?)Convert.ToInt32(PaidByCombo2.SelectedValue)
                    : null;
                DateTime vDate = VoucherDateBox.SelectedDate ?? DateTime.Today;
                string narration = NarrationBox.Text ?? string.Empty;
                int voucherNo = GetNextVoucherNo();

                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                cn.Open();
                using var tx = cn.BeginTransaction();
                try
                {
                    int insertedVchrId;
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO VchrTranLog
                        (VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId, SalesManId, Narration, TranDate, UserId, VchrName, FK_BranchId, SequenceNo, Terminal)
                        VALUES (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @FromAccountId, @ToAccountId, @SalesManId, @Narration, GETDATE(), @UserId, @VchrName, NULL, NULL, @Terminal);
                        SELECT CAST(SCOPE_IDENTITY() AS int);
                    ", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@VchrNo", voucherNo);
                        cmd.Parameters.AddWithValue("@VchrDate", vDate);
                        cmd.Parameters.AddWithValue("@VchrAmount", amount);
                        cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                        cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                        cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                        cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Narration", narration);
                        cmd.Parameters.AddWithValue("@UserId", DBNull.Value);
                        cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                        cmd.Parameters.AddWithValue("@Terminal", "Fawad");

                        insertedVchrId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (insertedVchrId == 0) throw new Exception("Failed to insert voucher header.");
                    _currentVchrTranId = insertedVchrId;

                    // Transaction logs
                    using (var cmdMain = new SqlCommand(@"
                        INSERT INTO TransactionLog
                        (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal)
                        VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), NULL, NULL, @VchrName, NULL, NULL, @Terminal);
                    ", cn, tx))
                    {
                        cmdMain.Parameters.AddWithValue("@VchrId", insertedVchrId);
                        cmdMain.Parameters.AddWithValue("@VchrNo", voucherNo);
                        cmdMain.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                        cmdMain.Parameters.AddWithValue("@VchrDate", vDate);
                        cmdMain.Parameters.AddWithValue("@AccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                        cmdMain.Parameters.AddWithValue("@OppAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                        cmdMain.Parameters.AddWithValue("@DebitAmount", 0m);
                        cmdMain.Parameters.AddWithValue("@CreditAmount", amount);
                        cmdMain.Parameters.AddWithValue("@Narration", narration);
                        cmdMain.Parameters.AddWithValue("@UserID", DBNull.Value);
                        cmdMain.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                        cmdMain.Parameters.AddWithValue("@Terminal", "Fawad");
                        cmdMain.ExecuteNonQuery();
                    }

                    using (var cmdSub = new SqlCommand(@"
                        INSERT INTO TransactionLog
                        (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal)
                        VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), NULL, NULL, @VchrName, NULL, NULL, @Terminal);
                    ", cn, tx))
                    {
                        cmdSub.Parameters.AddWithValue("@VchrId", insertedVchrId);
                        cmdSub.Parameters.AddWithValue("@VchrNo", voucherNo);
                        cmdSub.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                        cmdSub.Parameters.AddWithValue("@VchrDate", vDate);
                        cmdSub.Parameters.AddWithValue("@AccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                        cmdSub.Parameters.AddWithValue("@OppAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                        cmdSub.Parameters.AddWithValue("@DebitAmount", amount);
                        cmdSub.Parameters.AddWithValue("@CreditAmount", 0m);
                        cmdSub.Parameters.AddWithValue("@Narration", narration);
                        cmdSub.Parameters.AddWithValue("@UserID", DBNull.Value);
                        cmdSub.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                        cmdSub.Parameters.AddWithValue("@Terminal", "Fawad");
                        cmdSub.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }

                MessageBox.Show("Saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dgBankPayments.SelectedItem == null)
            {
                MessageBox.Show("Select a voucher to edit.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var drv = dgBankPayments.SelectedItem as DataRowView;
            if (drv == null) return;

            _currentVchrTranId = Convert.ToInt32(drv["VchrTranId"]);
            VoucherNoBox.Value = Convert.ToInt32(drv["VchrNo"]);
            VoucherDateBox.SelectedDate = Convert.ToDateTime(drv["VchrDate"]);
            AmountBox.Text = Convert.ToDecimal(drv["VchrAmount"]).ToString("N2");
            NarrationBox.Text = drv["Narration"].ToString();
            PaidByCombo2.SelectedValue = drv["SalesManId"];
            AccountCombo.SelectedValue = drv["FromAccountId"];
            BankAccountCombo.SelectedValue = drv["ToAccountId"];

            SaveBtn.Content = "Update"; // Change Save button to Update mode
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentVchrTranId.HasValue)
            {
                MessageBox.Show("Select a voucher to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var res = MessageBox.Show("Are you sure you want to delete the selected voucher?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes) return;

            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                cn.Open();
                using var tx = cn.BeginTransaction();
                try
                {
                    using var cmd = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId", cn, tx);
                    cmd.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                    cmd.ExecuteNonQuery();

                    using var cmd2 = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId", cn, tx);
                    cmd2.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                    cmd2.ExecuteNonQuery();

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }

                MessageBox.Show("Deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e) => ResetForm();

        private void ResetForm()
        {
            _currentVchrTranId = null;
            VoucherDateBox.SelectedDate = DateTime.Today;
            AmountBox.Text = "0.00";
            DiscountBox.Text = "0.00";
            NarrationBox.Text = string.Empty;
            PaidByCombo2.SelectedIndex = -1;
            AccountCombo.SelectedIndex = -1;
            BankAccountCombo.SelectedIndex = -1;
            SaveBtn.Content = "Save";
            VoucherNoBox.Value = GetNextVoucherNo();
            UpdateBankBalanceDisplay();
        }

        #endregion

        #region Helpers & selection

        private int GetNextVoucherNo()
        {
            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand(@"
                    SELECT ISNULL(MAX(VchrNo), 0) + 1 
                    FROM VchrTranLog 
                    WHERE VchrType = @VchrType AND VchrName = @VchrName
                ", cn);
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                cn.Open();
                var o = cmd.ExecuteScalar();
                if (o != null && o != DBNull.Value) return Convert.ToInt32(o);
            }
            catch { }
            return 1;
        }

        private void dgBankPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBankPayments.SelectedItem == null) return;
            if (dgBankPayments.SelectedItem is not DataRowView drv) return;

            _currentVchrTranId = Convert.ToInt32(drv["VchrTranId"]);
            VoucherNoBox.Value = Convert.ToInt32(drv["VchrNo"]);
            VoucherDateBox.SelectedDate = Convert.ToDateTime(drv["VchrDate"]);
            AmountBox.Text = Convert.ToDecimal(drv["VchrAmount"]).ToString("N2");
            NarrationBox.Text = drv["Narration"].ToString();
            PaidByCombo2.SelectedValue = drv["SalesManId"];
            AccountCombo.SelectedValue = drv["FromAccountId"];
            BankAccountCombo.SelectedValue = drv["ToAccountId"];
            SaveBtn.Content = "Update";
        }

        private void BankAccountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateBankBalanceDisplay();
        private void VoucherDateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => UpdateBankBalanceDisplay();
        private void PaidByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AutoRefreshChk.IsChecked == true) LoadGrid();
        }

        private void AmountBox_TextPreviewInput(object sender, TextCompositionEventArgs e) => e.Handled = !decimal.TryParse(e.Text, out _);
        private void DiscountBox_TextPreviewInput(object sender, TextCompositionEventArgs e) => e.Handled = !decimal.TryParse(e.Text, out _);

        private void ToggleFields_Click(object sender, RoutedEventArgs e) =>
            FieldContainer.Visibility = FieldContainer.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        #endregion

        
    }
}


