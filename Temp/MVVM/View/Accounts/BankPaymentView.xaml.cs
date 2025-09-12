using AccountsLibrary.Properties;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class BankPaymentView : UserControl
    {
        private int? _currentVchrTranId = null;
        private const string VCHR_TYPE = "Payment Voucher";
        private const string VCHR_NAME = "Bank Payment";

        public BankPaymentView()
        {
            InitializeComponent();           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            VoucherDateBox.SelectedDate = DateTime.Today;
            LoadAccounts();
            LoadEmployees();
            LoadFilterEmployees();
            ResetForm();
            LoadGrid();
           // LoadEmployees2();
        }

        #region Loading lookups & grid



        private void LoadAccounts()
        {
            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            using (var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                AccountCombo.ItemsSource = dt.DefaultView;
                BankAccountCombo.ItemsSource = dt.DefaultView;
            }

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
            PaidByCombo.SelectedIndex = 0;
        }

        private void LoadFilterEmployees()
        {
            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            using (var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                var dt2 = dt.Clone();
                var dr = dt2.NewRow();
                dr["EmpID"] = DBNull.Value;
                dr["EmpName"] = "All";
                dt2.Rows.Add(dr);
                foreach (DataRow r in dt.Rows) dt2.ImportRow(r);

                PaidByFilterCombo.SelectedIndex = 0;
            }
        }

        private void LoadGrid()
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            using (var cmd = new SqlCommand(@"
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
                                ", cn))

            {
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);

                if (PaidByFilterCombo.SelectedValue is int pid) cmd.Parameters.AddWithValue("@SalesManId", pid);
                else cmd.Parameters.AddWithValue("@SalesManId", DBNull.Value);

                if (VoucherDateBox.SelectedDate.HasValue) cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate.Value.Date);
                else cmd.Parameters.AddWithValue("@VchrDate", DBNull.Value);

                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

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

            dgBankPayments.ItemsSource = dtWithRow.DefaultView;

            decimal total = 0m;
            foreach (DataRow row in dtWithRow.Rows)
            {
                if (decimal.TryParse(row["VchrAmount"]?.ToString(), out decimal amt)) total += amt;
            }
            TotalText.Text = total.ToString("N2", CultureInfo.InvariantCulture);
        }

        #endregion

        #region Account balance

        private void BankAccountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBankBalanceDisplay();
        }

        private void VoucherDateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBankBalanceDisplay();
            if (AutoRefreshChk.IsChecked == true) LoadGrid();
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
            decimal balance = GetAccountBalance(accountId, asOf);
            BankBalanceText.Text = balance.ToString("N2", CultureInfo.InvariantCulture);
        }

        private decimal GetAccountBalance(int accountId, DateTime asOfDate)
        {
            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            using (var cmd = new SqlCommand(@"
                SELECT ISNULL(SUM(DebitAmount),0) AS DebitSum, ISNULL(SUM(CreditAmount),0) AS CreditSum
                FROM TransactionLog
                WHERE (AccountId = @AccountId OR OppAccountId = @AccountId) AND CONVERT(date, TranDate) <= @AsOfDate
                ", cn))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                cmd.Parameters.AddWithValue("@AsOfDate", asOfDate.Date);
                cn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        decimal debit = rdr.IsDBNull(0) ? 0m : rdr.GetDecimal(0);
                        decimal credit = rdr.IsDBNull(1) ? 0m : rdr.GetDecimal(1);
                        return debit - credit;
                    }
                }
            }
            return 0m;
        }

        #endregion

        #region Save / Update / Delete / Clear

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(AmountBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0m)
            {
                MessageBox.Show("Enter valid amount (>0).", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int fromAccountId = AccountCombo.SelectedValue != null ? Convert.ToInt32(AccountCombo.SelectedValue) : 0;
            int toAccountId = BankAccountCombo.SelectedValue != null ? Convert.ToInt32(BankAccountCombo.SelectedValue) : 0;
            int? salesManId = PaidByCombo.SelectedValue != null ? (int?)Convert.ToInt32(PaidByCombo.SelectedValue) : null;
          //int? salesManId = PaidByFilterCombo.SelectedValue != null ? (int?)Convert.ToInt32(PaidByFilterCombo.SelectedValue) : null;
            DateTime vDate = VoucherDateBox.SelectedDate ?? DateTime.Today;
            string narration = NarrationBox.Text ?? string.Empty;

            if ((string)SaveBtn.Content == "Save")
            {
                int next = GetNextVoucherNo();
                if (next <= 0) next = 1;

                using (var cn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    cn.Open();
                    using (var tx = cn.BeginTransaction())
                    {
                        try
                        {                           
                            int insertedVchrId;
                            using (var cmd = new SqlCommand(@"
                            INSERT INTO VchrTranLog (VchrNo, VchrDate, VchrAmount, VchrType, VchrName, SalesManId, FromAccountId, ToAccountId, TranDate, UserId, Narration, FK_BranchId, Terminal, SequenceNo)
                            VALUES (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @VchrName, @SalesManId, @FromAccountId, @ToAccountId, GETDATE(), @UserId, @Narration, @FK_BranchId, @Terminal, @SequenceNo);
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
                                cmd.Parameters.AddWithValue("@UserId", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Narration", narration);
                                cmd.Parameters.AddWithValue("@FK_BranchId", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Terminal", DBNull.Value);
                                cmd.Parameters.AddWithValue("@SequenceNo", DBNull.Value);

                                var idObj = cmd.ExecuteScalar();
                                insertedVchrId = idObj != null && idObj != DBNull.Value ? Convert.ToInt32(idObj) : 0;
                            }

                            if (insertedVchrId == 0) throw new Exception("Failed to insert voucher header.");

                            _currentVchrTranId = insertedVchrId;

                            using (var cmdMain = new SqlCommand(@"
                            INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal)
                            VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), NULL, NULL, @VchrName, NULL, NULL, NULL);
                            ", cn, tx))
                            {
                                cmdMain.Parameters.AddWithValue("@VchrId", insertedVchrId);
                                cmdMain.Parameters.AddWithValue("@VchrNo", next);
                                cmdMain.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                                cmdMain.Parameters.AddWithValue("@VchrDate", vDate);
                                cmdMain.Parameters.AddWithValue("@AccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmdMain.Parameters.AddWithValue("@OppAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmdMain.Parameters.AddWithValue("@DebitAmount", 0m);
                                cmdMain.Parameters.AddWithValue("@CreditAmount", amount);
                                cmdMain.Parameters.AddWithValue("@Narration", narration);
                                cmdMain.Parameters.AddWithValue("@UserID", DBNull.Value);
                                cmdMain.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                                cmdMain.ExecuteNonQuery();
                            }

                            using (var cmdSub = new SqlCommand(@"
                            INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, Status, RefNo, VchrName, FK_BranchId, SequenceNo, Terminal)
                            VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), NULL, NULL, @VchrName, NULL, NULL, NULL);
                            ", cn, tx))
                            {
                                cmdSub.Parameters.AddWithValue("@VchrId", insertedVchrId);
                                cmdSub.Parameters.AddWithValue("@VchrNo", next);
                                cmdSub.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                                cmdSub.Parameters.AddWithValue("@VchrDate", vDate);
                                cmdSub.Parameters.AddWithValue("@AccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmdSub.Parameters.AddWithValue("@OppAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmdSub.Parameters.AddWithValue("@DebitAmount", amount);
                                cmdSub.Parameters.AddWithValue("@CreditAmount", 0m);
                                cmdSub.Parameters.AddWithValue("@Narration", narration);
                                cmdSub.Parameters.AddWithValue("@UserID", DBNull.Value);
                                cmdSub.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                                cmdSub.ExecuteNonQuery();
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

                MessageBox.Show("Saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else // Update
            {
                if (!_currentVchrTranId.HasValue)
                {
                    MessageBox.Show("Select a voucher to update.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int vchrNo = Convert.ToInt32(VoucherNoBox.Value);

                using (var cn = new SqlConnection(Settings.Default.strConnetionString))
                {
                    cn.Open();
                    using (var tx = cn.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new SqlCommand(@"
                                    UPDATE VchrTranLog
                                    SET VchrNo = @VchrNo, VchrDate = @VchrDate, VchrAmount = @VchrAmount,
                                        SalesManId = @SalesManId, FromAccountId = @FromAccountId, ToAccountId = @ToAccountId,
                                        Narration = @Narration
                                    WHERE VchrTranId = @VchrTranId;
                                ", cn, tx))
                            {
                                cmd.Parameters.AddWithValue("@VchrTranId", _currentVchrTranId.Value);
                                cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                                cmd.Parameters.AddWithValue("@VchrDate", vDate);
                                cmd.Parameters.AddWithValue("@VchrAmount", amount);
                                cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmd.Parameters.AddWithValue("@ToAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmd.Parameters.AddWithValue("@Narration", narration);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmdDel = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId", cn, tx))
                            {
                                cmdDel.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmdDel.ExecuteNonQuery();
                            }

                             using (var cmdMain = new SqlCommand(@"
                            INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, VchrName)
                            VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), @VchrName);
                        ", cn, tx))
                            {
                                cmdMain.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmdMain.Parameters.AddWithValue("@VchrNo", vchrNo);
                                cmdMain.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                                cmdMain.Parameters.AddWithValue("@VchrDate", vDate);
                                cmdMain.Parameters.AddWithValue("@AccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmdMain.Parameters.AddWithValue("@OppAccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmdMain.Parameters.AddWithValue("@DebitAmount", 0m);
                                cmdMain.Parameters.AddWithValue("@CreditAmount", amount);
                                cmdMain.Parameters.AddWithValue("@Narration", narration);
                                cmdMain.Parameters.AddWithValue("@UserID", DBNull.Value);
                                cmdMain.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                                cmdMain.ExecuteNonQuery();
                            }

                            using (var cmdSub = new SqlCommand(@"
                                INSERT INTO TransactionLog (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, UserID, TranDate, VchrName)
                                VALUES (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, @UserID, GETDATE(), @VchrName);
                                ", cn, tx))
                            {
                                cmdSub.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                                cmdSub.Parameters.AddWithValue("@VchrNo", vchrNo);
                                cmdSub.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                                cmdSub.Parameters.AddWithValue("@VchrDate", vDate);
                                cmdSub.Parameters.AddWithValue("@AccountId", toAccountId == 0 ? (object)DBNull.Value : toAccountId);
                                cmdSub.Parameters.AddWithValue("@OppAccountId", fromAccountId == 0 ? (object)DBNull.Value : fromAccountId);
                                cmdSub.Parameters.AddWithValue("@DebitAmount", amount);
                                cmdSub.Parameters.AddWithValue("@CreditAmount", 0m);
                                cmdSub.Parameters.AddWithValue("@Narration", narration);
                                cmdSub.Parameters.AddWithValue("@UserID", DBNull.Value);
                                cmdSub.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                                cmdSub.ExecuteNonQuery();
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

                MessageBox.Show("Updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                SaveBtn.Content = "Save";
            }

            ResetForm();
            LoadGrid();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentVchrTranId.HasValue)
            {
                MessageBox.Show("Select a voucher to edit.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            SaveBtn.Content = "Update";
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

            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId", cn, tx))
                        {
                            cmd.Parameters.AddWithValue("@VchrId", _currentVchrTranId.Value);
                            cmd.ExecuteNonQuery();
                        }

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
            ResetForm();
            LoadGrid();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void ResetForm()
        {
            _currentVchrTranId = null;
            VoucherDateBox.SelectedDate = DateTime.Today;
            AmountBox.Text = "0.00";
            DiscountBox.Text = "0.00";
            NarrationBox.Text = string.Empty;
            PaidByCombo.SelectedIndex = -1;
          //PaidByFilterCombo.SelectedIndex = -1;
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
                using (var cn = new SqlConnection(Settings.Default.strConnetionString))
                using (var cmd = new SqlCommand(@"
                    SELECT ISNULL(MAX(VchrNo),0) + 1 FROM VchrTranLog WHERE VchrType = @VchrType AND VchrName = @VchrName
                    ", cn))
                {
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cn.Open();
                    var o = cmd.ExecuteScalar();
                    if (o != null && o != DBNull.Value) return Convert.ToInt32(o);
                }
            }
            catch { }
            return 1;
        }

        private void dgBankPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBankPayments.SelectedItem == null) return;
            var drv = dgBankPayments.SelectedItem as DataRowView;
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

            //remove below 2 lines in case of error in grid
           // if (drv["SalesManId"] != DBNull.Value) PaidByFilterCombo.SelectedValue = Convert.ToInt32(drv["SalesManId"]);
            //else PaidByFilterCombo.SelectedIndex = -1;

            SaveBtn.Content = "Update";
            UpdateBankBalanceDisplay();
        }

        #endregion

        private void BankPaymentAmount_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }


        private void DiscountChk_Checked(object sender, RoutedEventArgs e)
        {
            DiscountBox.IsEnabled = DiscountChk.IsChecked == true;
        }

        private void PaidByFilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadGrid();
        }

        private void ToggleFields_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DiscountBox_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }

    }
}
