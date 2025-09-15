using AccountsLibrary.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class PaymentAndReceiptView : UserControl
    {
        
        private readonly string _conn = Settings.Default.strConnetionString;


        private int? _currentVchrTranId = null;

        private string sVType = "Payment/Receipt Voucher";
        private string sVchrName = "Cash Receipt";

        public PaymentAndReceiptView()
        {
            InitializeComponent();

            Loaded += PaymentAndReceiptView_Loaded;
            Loaded += (s, e) => WireButtonsByContent();

            dgVouchers.SelectionChanged += DgVouchers_SelectionChanged;
        }

        private void PaymentAndReceiptView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dpVoucherDate.SelectedDate = DateTime.Today;

                LoadAccounts();
                LoadEmployees();
                LoadVoucherTypes();
                LoadVouchers();
                
                TrySetNextVoucherNo();

                UpdateLedgerBalanceDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing control: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Load data for dropdowns & grid

        private void LoadAccounts()
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            cmbParty.ItemsSource = dt.DefaultView;
            cmbParty.DisplayMemberPath = "AccountName";
            cmbParty.SelectedValuePath = "AccountId";

            CashBankAccount.ItemsSource = dt.DefaultView;
            CashBankAccount.DisplayMemberPath = "AccountName";
            CashBankAccount.SelectedValuePath = "AccountId";
        }

        private void LoadEmployees()
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            SelectedValue.ItemsSource = dt.DefaultView;
            SelectedValue.DisplayMemberPath = "EmpName";
            SelectedValue.SelectedValuePath = "EmpID";
        }

        private void LoadVoucherTypes()
        {
            cmbVoucherType.Items.Clear();
            cmbVoucherType.Items.Add("Receipt Voucher");
            cmbVoucherType.Items.Add("Payment Voucher");
            cmbVoucherType.Items.Add("Debit Note");
            cmbVoucherType.Items.Add("Credit Note");
            cmbVoucherType.Items.Add("Journal Voucher");
            cmbVoucherType.SelectedIndex = 0;
        }

        private void LoadVouchers()
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(@"
                SELECT v.VchrTranId,
                       v.VchrNo,
                       v.VchrDate,
                       v.VchrType,
                       v.VchrName,
                       fa.AccountName,
                       ta.AccountName,
                       v.VchrAmount,
                       v.Narration,
                       v.SalesManId
                FROM VchrTranLog v
                LEFT JOIN Account fa ON fa.AccountId = v.FromAccountId
                LEFT JOIN Account ta ON ta.AccountId = v.ToAccountId
                ORDER BY v.VchrDate DESC, v.VchrNo DESC
            ", cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            dgVouchers.ItemsSource = dt.DefaultView;
                     
            var total = dt.AsEnumerable().Sum(r => r.Field<decimal?>("VchrAmount") ?? 0m);
            TxtAmountInWords.Text = total.ToString("N2", CultureInfo.InvariantCulture); 
        }

        #endregion

        #region Helper GetNext / Balances

        private int GetNextVoucherNo(string vchType = null, string vchName = null)
        {
            vchType ??= sVType;
            vchName ??= sVchrName;

            using (var cn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(@"
                SELECT ISNULL(MAX(VchrNo),0) + 1
                FROM VchrTranLog
                WHERE VchrType = @VchrType AND VchrName = @VchrName
            ", cn))
            {
                cmd.Parameters.AddWithValue("@VchrType", vchType);
                cmd.Parameters.AddWithValue("@VchrName", vchName);
                cn.Open();
                var o = cmd.ExecuteScalar();
                return o != null ? Convert.ToInt32(o) : 1;
            }
        }

        private void TrySetNextVoucherNo()
        {
            try
            {
                var next = GetNextVoucherNo();
                if (txtVoucherNo != null)
                {
                    txtVoucherNo.Value = next;
                }
            }
            catch
            {
                // ignore
            }
        }

        private decimal GetAccountBalance(int accountId, DateTime asOf)
        {
            using (var cn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand(@"
                SELECT ISNULL(SUM(DebitAmount),0) AS DebitSum, ISNULL(SUM(CreditAmount),0) AS CreditSum
                FROM TransactionLog
                WHERE (AccountId = @AccountId OR OppAccountId = @AccountId)
                  AND CONVERT(date, TranDate) <= @AsOfDate
            ", cn))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                cmd.Parameters.AddWithValue("@AsOfDate", asOf.Date);
                cn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        decimal debit = rdr.IsDBNull(0) ? 0m : Convert.ToDecimal(rdr[0]);
                        decimal credit = rdr.IsDBNull(1) ? 0m : Convert.ToDecimal(rdr[1]);
                        return debit - credit;
                    }
                }
            }
            return 0m;
        }

        private void UpdateLedgerBalanceDisplay()
        {
            if (cmbParty.SelectedValue == null)
            {
                LedgerAndBalance.Text = "0.00";
                return;
            }

            if (!int.TryParse(cmbParty.SelectedValue.ToString(), out int acctId))
            {
                LedgerAndBalance.Text = "0.00";
                return;
            }

            var asOf = dpVoucherDate.SelectedDate ?? DateTime.Today;
            var bal = GetAccountBalance(acctId, asOf);
            LedgerAndBalance.Text = bal.ToString("N2", CultureInfo.InvariantCulture);
        }

        #endregion

        #region 

        private void SaveVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveOrUpdateVoucher(isUpdate: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving voucher: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_currentVchrTranId.HasValue)
                {
                    MessageBox.Show("Select a voucher to edit.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                SaveOrUpdateVoucher(isUpdate: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating voucher: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveOrUpdateVoucher(bool isUpdate)
        {
            // validation
            if (!decimal.TryParse(txtPaidAmount.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal paidAmount) || paidAmount <= 0m)
            {
                MessageBox.Show("The amount paid must be greater than zero", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int vchrNo = (txtVoucherNo.Value != null) ? Convert.ToInt32(txtVoucherNo.Value) : GetNextVoucherNo();
            DateTime vDate = dpVoucherDate.SelectedDate ?? DateTime.Today;
            string vType = cmbVoucherType.Text ?? sVType;
            string narration = txtNarration.Text ?? string.Empty;

            int fromAccountId = 0, toAccountId = 0;
            if (cmbParty.SelectedValue != null) int.TryParse(cmbParty.SelectedValue.ToString(), out fromAccountId);
            if (CashBankAccount.SelectedValue != null) int.TryParse(CashBankAccount.SelectedValue.ToString(), out toAccountId);

            int? salesManId = null;
            if (SelectedValue.SelectedValue != null && int.TryParse(SelectedValue.SelectedValue.ToString(), out int sm)) salesManId = sm;

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        int insertedVchrId;

                        if (!isUpdate)
                        {
                            using (var cmd = new SqlCommand(@"
                                INSERT INTO VchrTranLog
                                (VchrNo, VchrDate, VchrAmount, VchrType, VchrName, SalesManId, FromAccountId, ToAccountId, TranDate, UserId, Narration)
                                VALUES (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @VchrName, @SalesManId, @FromAccountId, @ToAccountId, GETDATE(), NULL, @Narration);
                                SELECT CAST(SCOPE_IDENTITY() AS int);
                            ", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                                cmd.Parameters.AddWithValue("@VchrAmount", paidAmount);
                                cmd.Parameters.AddWithValue("@VchrType", vType);
                                cmd.Parameters.AddWithValue("@VchrName", sVchrName);
                                cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmd.Parameters.AddWithValue("@Narration", narration);
                                insertedVchrId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            _currentVchrTranId = insertedVchrId;
                        }
                        else
                        {
                            if (!_currentVchrTranId.HasValue) throw new InvalidOperationException("No voucher selected to update.");

                            using (var cmd = new SqlCommand(@"
                                UPDATE VchrTranLog
                                SET VchrNo=@VchrNo, VchrDate=@VchrDate, VchrAmount=@VchrAmount, SalesManId=@SalesManId,
                                    FromAccountId=@FromAccountId, ToAccountId=@ToAccountId, Narration=@Narration
                                WHERE VchrTranId=@VchrTranId
                            ", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                                cmd.Parameters.AddWithValue("@VchrAmount", paidAmount);
                                cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmd.Parameters.AddWithValue("@Narration", narration);
                                cmd.ExecuteNonQuery();
                            }

                            // remove old TransactionLog entries
                            using (var cmdDel = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId", cn, tx))
                            {
                                cmdDel.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmdDel.ExecuteNonQuery();
                            }

                            insertedVchrId = _currentVchrTranId.Value;
                        }

                        // Insert TransactionLog entries: one credit (from account), one debit (to account)
                        InsertTransactionLogRows(cn, tx, insertedVchrId, vchrNo, vType, vDate, fromAccountId, toAccountId, paidAmount, narration);

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }

            MessageBox.Show(isUpdate ? "Voucher updated." : "Voucher saved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

           LoadVouchers();
            TrySetNextVoucherNo();
            ClearFormFields(keepVoucherNumber: false);
        }

        private void InsertTransactionLogRows(SqlConnection cn, SqlTransaction tx, int vchrId, int vchrNo, string vType, DateTime vDate, int fromAccountId, int toAccountId, decimal amount, string narration)
        {
            // Credit entry (AccountId = fromAccountId, OppAccountId = toAccountId)
            using (var cmd = new SqlCommand(@"
                INSERT INTO TransactionLog
                    (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                VALUES (@VchrId,@VchrNo,@VchrType,@VchrDate,@AccountId,@OppAccountId,@DebitAmount,@CreditAmount,@Narration,GETDATE(),@VchrName)
            ", cn, tx))
            {
                cmd.Parameters.AddWithValue("@VchrId", vchrId);
                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                cmd.Parameters.AddWithValue("@VchrType", vType);
                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                cmd.Parameters.AddWithValue("@AccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                cmd.Parameters.AddWithValue("@OppAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                cmd.Parameters.AddWithValue("@DebitAmount", 0m);
                cmd.Parameters.AddWithValue("@CreditAmount", amount);
                cmd.Parameters.AddWithValue("@Narration", narration);
                cmd.Parameters.AddWithValue("@VchrName", sVchrName);
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SqlCommand(@"
                INSERT INTO TransactionLog
                    (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                VALUES (@VchrId,@VchrNo,@VchrType,@VchrDate,@AccountId,@OppAccountId,@DebitAmount,@CreditAmount,@Narration,GETDATE(),@VchrName)
            ", cn, tx))
            {
                cmd.Parameters.AddWithValue("@VchrId", vchrId);
                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                cmd.Parameters.AddWithValue("@VchrType", vType);
                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                cmd.Parameters.AddWithValue("@AccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                cmd.Parameters.AddWithValue("@OppAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                cmd.Parameters.AddWithValue("@DebitAmount", amount);
                cmd.Parameters.AddWithValue("@CreditAmount", 0m);
                cmd.Parameters.AddWithValue("@Narration", narration);
                cmd.Parameters.AddWithValue("@VchrName", sVchrName);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_currentVchrTranId.HasValue)
                {
                    MessageBox.Show("Select a voucher to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var res = MessageBox.Show("Confirm delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.Yes) return;

                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var tx = cn.BeginTransaction())
                    {
                        try
                        {
                            // delete transaction logs
                            using (var cmd = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId=@VchrId", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = new SqlCommand("DELETE FROM ReceiptAndPayment WHERE VchrId = @VchrId", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmd.ExecuteNonQuery();
                            }

                            // delete main vchr log
                            using (var cmd = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                                cmd.ExecuteNonQuery();
                            }

                            tx.Commit();
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show("Deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFormFields(keepVoucherNumber: false);
                LoadVouchers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting voucher: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields(keepVoucherNumber: true);
        }

        private void ClearFormFields(bool keepVoucherNumber)
        {
            _currentVchrTranId = null;
            dpVoucherDate.SelectedDate = DateTime.Today;
            txtPaidAmount.Text = "0.00";
            Discount.Text = "0.00";
            txtNarration.Text = string.Empty;
            cmbParty.SelectedIndex = -1;
            CashBankAccount.SelectedIndex = -1;
            SelectedValue.SelectedIndex = -1;
            if (!keepVoucherNumber) txtVoucherNo.Value = GetNextVoucherNo();
            UpdateLedgerBalanceDisplay();
        }

        #endregion

        #region DataGrid selection and UI wiring

        private void DgVouchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgVouchers.SelectedItem == null) return;

                var drv = dgVouchers.SelectedItem as DataRowView;
                if (drv == null) return;

                _currentVchrTranId = drv["VchrTranId"] == DBNull.Value ? (int?)null : Convert.ToInt32(drv["VchrTranId"]);
                txtVoucherNo.Value = drv["VchrNo"] == DBNull.Value ? 0 : Convert.ToInt32(drv["VchrNo"]);
                dpVoucherDate.SelectedDate = drv["VchrDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["VchrDate"]);
                txtPaidAmount.Text = drv["VchrAmount"] == DBNull.Value ? "0.00" : Convert.ToDecimal(drv["VchrAmount"]).ToString("N2", CultureInfo.InvariantCulture);
                txtNarration.Text = drv["Narration"]?.ToString() ?? string.Empty;

                
                if (drv.Row.Table.Columns.Contains("FromAccountName") && drv["FromAccountName"] != DBNull.Value)
                {
               
                    SelectComboBoxItemByText(cmbParty, drv["FromAccountName"].ToString());
                }
                else
                {
                    cmbParty.SelectedIndex = -1;
                }

                if (drv.Row.Table.Columns.Contains("ToAccountName") && drv["ToAccountName"] != DBNull.Value)
                {
                    SelectComboBoxItemByText(CashBankAccount, drv["ToAccountName"].ToString());
                }
                else
                {
                    CashBankAccount.SelectedIndex = -1;
                }

                if (drv.Row.Table.Columns.Contains("SalesManId") && drv["SalesManId"] != DBNull.Value)
                {
                    SelectedValue.SelectedValue = drv["SalesManId"];
                }
                else
                {
                    SelectedValue.SelectedIndex = -1;
                }

                UpdateLedgerBalanceDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting voucher: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectComboBoxItemByText(ComboBox cb, string text)
        {
            if (cb == null || string.IsNullOrEmpty(text)) return;
            foreach (var item in cb.Items)
            {
                if (item is DataRowView drv)
                {
                    if (drv.Row.ItemArray.Any(v => v != null && v.ToString() == text))
                    {
                        cb.SelectedItem = item;
                        return;
                    }
                }
            }

            for (int i = 0; i < cb.Items.Count; i++)
            {
                var it = cb.Items[i];
                if (it is DataRowView r && r.Row.Table.Columns.Contains("AccountName"))
                {
                    if ((r["AccountName"]?.ToString() ?? string.Empty).Equals(text, StringComparison.OrdinalIgnoreCase))
                    {
                        cb.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        #endregion

        #region Auto-wire buttons in XAML (by Content text)

        private void WireButtonsByContent()
        {
           
            var buttons = FindVisualChildren<Button>(this);
            foreach (var b in buttons)
            {
                if (b.Content == null) continue;
                var txt = b.Content.ToString().Trim();
                switch (txt)
                {
                    case "Save":
                        b.Click -= SaveVoucher_Click;
                        b.Click += SaveVoucher_Click;
                        break;
                    case "Edit":
                        b.Click -= EditVoucher_Click;
                        b.Click += EditVoucher_Click;
                        break;
                    case "Delete":
                        b.Click -= DeleteVoucher_Click;
                        b.Click += DeleteVoucher_Click;
                        break;
                    case "Clear":
                        b.Click -= Clear_Click;
                        b.Click += Clear_Click;
                        break;
                    case "Print":
                        b.Click -= BtnPrint_Click;
                        b.Click += BtnPrint_Click;
                        break;
                    case "Mail":
                        b.Click -= BtnMail_Click;
                        b.Click += BtnMail_Click;
                        break;
                }
            }
        }

        private static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child == null) continue;
                if (child is T t) yield return t;
                foreach (var childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        #endregion

        #region

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Print requested - not implemented here.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mail requested - not implemented here.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
