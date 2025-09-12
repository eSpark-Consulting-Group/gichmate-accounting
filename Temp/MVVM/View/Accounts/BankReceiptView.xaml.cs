using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class BankReceiptView : UserControl
    {
        private readonly string _connStr;
        private int? _currentVchrTranId = null;
        private const string VCHR_TYPE = "Receipt Voucher";
        private const string VCHR_NAME = "Bank Receipt";

        public BankReceiptView()
        {
            InitializeComponent();

            _connStr = ConfigurationManager.ConnectionStrings["PSVAT5"]?.ConnectionString
                       ?? throw new InvalidOperationException("Connection string 'PSVAT5' not found in app.config.");

            // wire events
            Loaded += BankReceiptView_Loaded;
            dgBankPayments.SelectionChanged += dgBankPayments_SelectionChanged;
            BankAccountCombo.SelectionChanged += BankAccountCombo_SelectionChanged;
            VoucherDateBox.SelectedDateChanged += VoucherDateBox_SelectedDateChanged;
            PaidByCombo.SelectionChanged += PaidByCombo_SelectionChanged;
        }

        private void BankReceiptView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VoucherDateBox.SelectedDate = DateTime.Today;
                LoadAccounts();
                LoadBankAccounts();
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

        private void LoadAccounts()
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                AccountCombo.ItemsSource = dt.DefaultView;
                AccountCombo.DisplayMemberPath = "AccountName";
                AccountCombo.SelectedValuePath = "AccountId";
            }
        }

        private void LoadBankAccounts()
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account ORDER BY AccountName", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                BankAccountCombo.ItemsSource = dt.DefaultView;
                BankAccountCombo.DisplayMemberPath = "AccountName";
                BankAccountCombo.SelectedValuePath = "AccountId";
            }
        }

        /// <summary>
        /// Populate both header filter PaidByCombo and the entry PaidByCombo2
        /// </summary>
        private void LoadEmployees()
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("SELECT EmpID, EmpName FROM EmployeeDetails ORDER BY EmpName", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                // header filter: add "All"
                var dtFilter = dt.Clone();
                var row = dtFilter.NewRow();
                row["EmpID"] = DBNull.Value;
                row["EmpName"] = "All";
                dtFilter.Rows.Add(row);
                foreach (DataRow r in dt.Rows) dtFilter.ImportRow(r);

                PaidByCombo.ItemsSource = dtFilter.DefaultView;
                PaidByCombo.DisplayMemberPath = "EmpName";
                PaidByCombo.SelectedValuePath = "EmpID";
                PaidByCombo.SelectedIndex = 0;

                // entry combobox (the actual PaidBy on the form)
                PaidByCombo2.ItemsSource = dt.DefaultView;
                PaidByCombo2.DisplayMemberPath = "EmpName";
                PaidByCombo2.SelectedValuePath = "EmpID";
                PaidByCombo2.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Load grid data and ensure DataGrid columns are bound to the returned columns.
        /// </summary>
        private void LoadGrid()
        {
            try
            {
                var dt = new DataTable();
                using (var cn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(@"
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
                      AND (@SalesManId IS NULL OR v.SalesManId = @SalesManId)
                      AND (@VchrDate IS NULL OR CONVERT(date, v.VchrDate) = @VchrDate)
                    ORDER BY v.VchrDate DESC, v.VchrNo DESC
                ", cn))
                {
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);

                    // SalesManId filter: safe check for DBNull/null
                    object sel = PaidByCombo.SelectedValue;
                    if (sel != null && sel != DBNull.Value && int.TryParse(sel.ToString(), out int pid))
                        cmd.Parameters.AddWithValue("@SalesManId", pid);
                    else
                        cmd.Parameters.AddWithValue("@SalesManId", DBNull.Value);

                    if (VoucherDateBox.SelectedDate.HasValue)
                        cmd.Parameters.AddWithValue("@VchrDate", VoucherDateBox.SelectedDate.Value.Date);
                    else
                        cmd.Parameters.AddWithValue("@VchrDate", DBNull.Value);

                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

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

                // ensure DataGrid columns are properly bound (we preserve visual layout but create bindings programmatically)
                EnsureGridColumns();

                dgBankPayments.ItemsSource = dtWithRow.DefaultView;

                // compute total
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

        /// <summary>
        /// Create/ensure the DataGrid has columns bound to the DataTable column names.
        /// This keeps the XAML visual layout (we create the same number/order of columns programmatically).
        /// </summary>
        private void EnsureGridColumns()
        {
            // If columns are already created and have bindings, do nothing.
            // But to be safe, re-create them so binding names match our query.
            dgBankPayments.Columns.Clear();

            // SL
            var colSL = new DataGridTextColumn
            {
                Header = "SL",
                Binding = new Binding("RowNumber"),
                Width = new DataGridLength(50)
            };
            dgBankPayments.Columns.Add(colSL);

            // Voucher No
            var colVNo = new DataGridTextColumn
            {
                Header = "Voucher No",
                Binding = new Binding("VchrNo"),
                Width = new DataGridLength(150)
            };
            dgBankPayments.Columns.Add(colVNo);

            // Date
            var colDate = new DataGridTextColumn
            {
                Header = "Date",
                Binding = new Binding("VchrDate") { StringFormat = "d" },
                Width = new DataGridLength(150)
            };
            dgBankPayments.Columns.Add(colDate);

            // Cash Account (From)
            var colFrom = new DataGridTextColumn
            {
                Header = "Cash Account",
                Binding = new Binding("FromAccountName"),
                Width = new DataGridLength(180)
            };
            dgBankPayments.Columns.Add(colFrom);

            // Party Account (To)
            var colTo = new DataGridTextColumn
            {
                Header = "Party Account",
                Binding = new Binding("ToAccountName"),
                Width = new DataGridLength(180)
            };
            dgBankPayments.Columns.Add(colTo);

            // Amount
            var colAmt = new DataGridTextColumn
            {
                Header = "Amount",
                Binding = new Binding("VchrAmount") { StringFormat = "N2" },
                Width = new DataGridLength(150)
            };
            dgBankPayments.Columns.Add(colAmt);

            // Narration
            var colNarr = new DataGridTextColumn
            {
                Header = "Narration",
                Binding = new Binding("Narration"),
                Width = new DataGridLength(200)
            };
            dgBankPayments.Columns.Add(colNarr);

            // Paid By (Employee)
            var colPaidBy = new DataGridTextColumn
            {
                Header = "Paid By",
                Binding = new Binding("PaidBy"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            dgBankPayments.Columns.Add(colPaidBy);
        }

        #endregion

        #region Account balance helpers

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

        /// <summary>
        /// Balance = SUM(DebitAmount) - SUM(CreditAmount) up to asOfDate
        /// </summary>
        private decimal GetAccountBalance(int accountId, DateTime asOfDate)
        {
            using (var cn = new SqlConnection(_connStr))
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
            try
            {
                if (!decimal.TryParse(AmountBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0m)
                {
                    MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int fromAccountId = AccountCombo.SelectedValue != null ? Convert.ToInt32(AccountCombo.SelectedValue) : 0; // party / opposite account
                int toAccountId = BankAccountCombo.SelectedValue != null ? Convert.ToInt32(BankAccountCombo.SelectedValue) : 0; // bank account
                int? salesManId = PaidByCombo2.SelectedValue != null && PaidByCombo2.SelectedValue != DBNull.Value
                    ? (int?)Convert.ToInt32(PaidByCombo2.SelectedValue)
                    : null;
                DateTime vDate = VoucherDateBox.SelectedDate ?? DateTime.Today;
                string narration = NarrationBox.Text ?? string.Empty;
                int voucherNo = GetNextVoucherNo();

                using (var cn = new SqlConnection(_connStr))
                {
                    cn.Open();
                    using (var tx = cn.BeginTransaction())
                    {
                        try
                        {
                            // Insert VchrTranLog (header)
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

                                // preserve Terminal if you want a literal name, set here:
                                cmd.Parameters.AddWithValue("@Terminal", "Fawad");

                                var idObj = cmd.ExecuteScalar();
                                insertedVchrId = idObj != null && idObj != DBNull.Value ? Convert.ToInt32(idObj) : 0;
                            }

                            if (insertedVchrId == 0) throw new Exception("Failed to insert voucher header.");

                            _currentVchrTranId = insertedVchrId;

                            // Insert TransactionLog main (Credit on FromAccount)
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

                            // Insert TransactionLog sub (Debit on ToAccount)
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
                    }
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
                using (var cn = new SqlConnection(_connStr))
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

                MessageBox.Show("Deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

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
                using (var cn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(@"
                    SELECT ISNULL(MAX(VchrNo), 0) + 1 FROM VchrTranLog WHERE VchrType = @VchrType AND VchrName = @VchrName
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

            try
            {
                _currentVchrTranId = drv["VchrTranId"] == DBNull.Value ? (int?)null : Convert.ToInt32(drv["VchrTranId"]);
                VoucherNoBox.Value = drv["VchrNo"] == DBNull.Value ? 0 : Convert.ToInt32(drv["VchrNo"]);
                VoucherDateBox.SelectedDate = drv["VchrDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["VchrDate"]);
                AmountBox.Text = drv["VchrAmount"] == DBNull.Value ? "0.00" : Convert.ToDecimal(drv["VchrAmount"]).ToString("N2", CultureInfo.InvariantCulture);
                NarrationBox.Text = drv["Narration"]?.ToString() ?? string.Empty;

                if (drv["FromAccountId"] != DBNull.Value) AccountCombo.SelectedValue = Convert.ToInt32(drv["FromAccountId"]);
                else AccountCombo.SelectedIndex = -1;

                if (drv["ToAccountId"] != DBNull.Value) BankAccountCombo.SelectedValue = Convert.ToInt32(drv["ToAccountId"]);
                else BankAccountCombo.SelectedIndex = -1;

                if (drv["SalesManId"] != DBNull.Value) PaidByCombo2.SelectedValue = Convert.ToInt32(drv["SalesManId"]);
                else PaidByCombo2.SelectedIndex = -1;

                SaveBtn.Content = "Update";
                UpdateBankBalanceDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Selection load error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region UI events

        private void AmountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // keep simple
        }

        private void BankAccountCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBankBalanceDisplay();
        }

        private void VoucherDateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBankBalanceDisplay();
            if (AutoRefreshChk.IsChecked == true) LoadGrid();
        }

        private void PaidByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // refresh grid when header filter changes
            LoadGrid();
        }

        private void ToggleFields_Click(object sender, RoutedEventArgs e)
        {
            FieldContainer.Visibility = FieldContainer.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        #endregion
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_currentVchrTranId.HasValue)
            {
                MessageBox.Show("Select a voucher to edit.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveBtn.Content = "Update";
        }

        private void AmountBox_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }


        private void DiscountBox_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
            // this is here because it will now only allow numeric data to be enterered.
        }

    }
}
