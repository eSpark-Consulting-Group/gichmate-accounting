using AccountsLibrary.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Accounts
{
    /// <summary>
    /// Interaction logic for PostDatedChequeReceiptView.xaml
    /// </summary>
    public partial class PostDatedChequeReceiptView : UserControl
    {

        private const string VCHR_TYPE = "Receipt Voucher";
        private const string VCHR_NAME = "Cheque Receipt";
        private readonly int currentBranchId = Settings.Default.iBranchId;


        public PostDatedChequeReceiptView()
        {
            InitializeComponent();
            Loaded += PostDatedChequeReceiptView_Loaded;

           
            FromDatePickerBox.SelectedDateChanged += Filters_Changed;
            ToDatePickerBox.SelectedDateChanged += Filters_Changed;
            txtPartyBox.SelectionChanged += Filters_Changed;
            txtCheque.TextChanged += (s, e) => LoadGrid();
            collectionStatusComboBox.SelectionChanged += Filters_Changed;

            postDatedGrid.MouseDoubleClick += postDatedGrid_MouseDoubleClick;



            txtChqDate.SelectedDate = DateTime.Today;
        }

        private void PostDatedChequeReceiptView_Loaded(object sender, RoutedEventArgs e)
        {
            // default dates
            FromDatePickerBox.SelectedDate = DateTime.Today;
            ToDatePickerBox.SelectedDate = DateTime.Today;
            DatePickerBox.SelectedDate = DateTime.Today;
            txtChqDate.SelectedDate = DateTime.Today;

            FillAccounts(txtPartyBox);
            FillAccounts(txtPartyBox2);
            FillAccounts(txtBankAccountBox);
            FillEmployees(txtExi);    
            FillStatusCombo();

            voucherNoBox.Value = GetNextVoucherNo();



            LoadGrid();
        }

        #region Fill helpers

        private void FillAccounts(ComboBox combo)
        {
            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand("SELECT AccountId AS AccountId, AccountName AS AccountName FROM Account ORDER BY AccountName", cn);
                var dt = new DataTable();
                using var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                combo.ItemsSource = dt.DefaultView;
                combo.DisplayMemberPath = "AccountName";
                combo.SelectedValuePath = "AccountId";
                combo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillEmployees(ComboBox combo)
        {
            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand("SELECT EmpID AS EmpID, EmpName AS EmpName FROM EmployeeDetails ORDER BY EmpName", cn);
                var dt = new DataTable();
                using var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                combo.ItemsSource = dt.DefaultView;
                combo.DisplayMemberPath = "EmpName";
                combo.SelectedValuePath = "EmpID";
                combo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillStatusCombo()
        {
            collectionStatusComboBox.ItemsSource = new[] { "All", "Pending", "Collected", "Returned" };
            collectionStatusComboBox.SelectedIndex = 0;
        }

        #endregion


        #region Grid Loading & Filters

        private void Filters_Changed(object sender, SelectionChangedEventArgs e) => LoadGrid();
        private void Filters_Changed(object sender, RoutedEventArgs e) => LoadGrid();

        private void LoadGrid()
        {
            DateTime from = FromDatePickerBox.SelectedDate ?? DateTime.Today;
            DateTime to = ToDatePickerBox.SelectedDate ?? DateTime.Today;

            if (to < from)
            {
                MessageBox.Show("From date must be less than or equal to To date.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string sql = @"
                            SELECT 
                                cd.CheqDtlsId       AS CheqDtlsId,
                                cd.VchrId           AS VchrId,
                                cd.Date             AS Date,
                                cd.Type             AS Type,
                                cd.CheqNo           AS CheqNo,
                                cd.CheqDate         AS CheqDate,
                                cd.Amount           AS Amount,
                                a.AccountName       AS PartyAccountName,
                                b.AccountName       AS BankAccountName,
                                cd.Status           AS Status,
                                cd.RefNo            AS RefNo,
                                cd.Remarks          AS Remarks,
                                cd.CollectedDate    AS CollectedDate,
                                cd.Bank             AS Bank,
                                cd.Branch           AS Branch,
                                cd.ChqRtnAmount     AS ChqRtnAmount
                            FROM ChequeDetails AS cd
                            LEFT JOIN Account AS a ON a.AccountId = cd.AccountId
                            LEFT JOIN Account AS b ON b.AccountId = cd.BankAcctId
                            WHERE cd.Type = @Type
                              AND cd.Date BETWEEN @FromDate AND @ToDate
                              AND cd.FK_BranchId = @BranchId
                            ";

            if (txtPartyBox.SelectedValue != null)
                sql += " AND cd.AccountId = @PartyId";
            if (!string.IsNullOrWhiteSpace(txtCheque.Text))
                sql += " AND cd.CheqNo LIKE @CheqNo";
            if (collectionStatusComboBox.SelectedIndex > 0)
                sql += " AND cd.Status = @Status";

            sql += " ORDER BY cd.Date DESC, cd.CheqDate DESC";

            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Type", VCHR_NAME);
                cmd.Parameters.AddWithValue("@FromDate", from.Date);
                cmd.Parameters.AddWithValue("@ToDate", to.Date);
                cmd.Parameters.AddWithValue("@BranchId", currentBranchId);

                if (txtPartyBox.SelectedValue != null)
                    cmd.Parameters.AddWithValue("@PartyId", Convert.ToInt32(txtPartyBox.SelectedValue));
                if (!string.IsNullOrWhiteSpace(txtCheque.Text))
                    cmd.Parameters.AddWithValue("@CheqNo", "%" + txtCheque.Text.Trim() + "%");
                if (collectionStatusComboBox.SelectedIndex > 0)
                    cmd.Parameters.AddWithValue("@Status", (string)collectionStatusComboBox.SelectedItem);

                var dt = new DataTable();
                using var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                postDatedGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cheques: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion


        #region Save/Edit/Delete/Clear

        private void Savebtn_Click(object sender, RoutedEventArgs e) => SaveOrUpdate();
        private void Editbtn_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Select a row then press Save to update.", "Edit", MessageBoxButton.OK, MessageBoxImage.Information);
        private void Deletebtn_Click(object sender, RoutedEventArgs e) => DeleteSelected();
        private void Clearbtn_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void ClearForm()
        {
            FromDatePickerBox.SelectedDate = DateTime.Today;
            ToDatePickerBox.SelectedDate = DateTime.Today;
            DatePickerBox.SelectedDate = DateTime.Today;
            txtChqDate.SelectedDate = DateTime.Today;

            txtBank.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtChqNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtReferenceNo.Text = string.Empty;
            txtAmount.Text = "0.00";

            txtPartyBox2.SelectedIndex = -1;
            txtBankAccountBox.SelectedIndex = -1;
            txtExi.SelectedIndex = -1;
            collectionStatusComboBox.SelectedIndex = 0;

            voucherNoBox.Value = GetNextVoucherNo();

            LoadGrid();
        }

        private void SaveOrUpdate()
        {
            if (voucherNoBox.Value == null || voucherNoBox.Value == 0)
            {
                MessageBox.Show("Voucher number is required.");
                return;
            }

            if (string.IsNullOrEmpty(txtPartyBox2.Text))
            {
                MessageBox.Show("Party Account name is required");
                return;
            }

            if (string.IsNullOrEmpty(txtBankAccountBox.Text))
            {
                MessageBox.Show("Bank Account name is required");
                return;
            }

            if (string.IsNullOrEmpty(collectionStatusComboBox.Text))
            {
                MessageBox.Show("Collection Status should be selected");
                return;
            }

            if (txtChqDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a date to proceed");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount) || amount <= 0m)
            {
                MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (amount > 99999999.99m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }


            int? partyId = txtPartyBox2.SelectedValue != null ? (int?)Convert.ToInt32(txtPartyBox2.SelectedValue) : null;
            int? bankAcctId = txtBankAccountBox.SelectedValue != null ? (int?)Convert.ToInt32(txtBankAccountBox.SelectedValue) : null;
            int? salesManId = txtExi.SelectedValue != null ? (int?)Convert.ToInt32(txtExi.SelectedValue) : null;
            int vchrNo = Convert.ToInt32(voucherNoBox.Value ?? 0);
            DateTime vchrDate = DatePickerBox.SelectedDate ?? DateTime.Today;
            string narration = txtRemarks.Text ?? string.Empty;
            string refNo = txtReferenceNo.Text ?? string.Empty;
            string status = collectionStatusComboBox.SelectedItem != null ? (string)collectionStatusComboBox.SelectedItem : string.Empty;
            DateTime chqDate = txtChqDate.SelectedDate ?? DateTime.Today;
            DateTime date = DatePickerBox.SelectedDate ?? DateTime.Today;
            string bank = txtBank.Text ?? string.Empty;
            string branch = txtBranch.Text ?? string.Empty;
            string cheqNo = txtChqNo.Text ?? string.Empty;

            int? jeIdForChequeDetails = null;

            int? loginUserId = Settings.Default.intLogOnId;
            bool userExists = false;
            try
            {
                using var cnCheck = new SqlConnection(Settings.Default.strConnetionString);
                using var cmdCheck = new SqlCommand("SELECT COUNT(1) FROM [User] AS u WHERE u.UserId = @UserId", cnCheck);
                cmdCheck.Parameters.AddWithValue("@UserId", (object)loginUserId ?? DBNull.Value);
                cnCheck.Open();
                object oCount = cmdCheck.ExecuteScalar();
                userExists = oCount != null && Convert.ToInt32(oCount) > 0;
            }
            catch
            {
                userExists = false;
            }

            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            cn.Open();
            using var tx = cn.BeginTransaction();
            try
            {
                int? currentVchrId = null;
                int? currentCheqDtlsId = null;

                if (postDatedGrid.SelectedItem is DataRowView drv)
                {
                    if (int.TryParse(drv["VchrId"]?.ToString(), out int parsedVchr)) currentVchrId = parsedVchr;
                    if (int.TryParse(drv["CheqDtlsId"]?.ToString(), out int parsedCheq)) currentCheqDtlsId = parsedCheq;
                }

                // Insert or update VchrTranLog
                if (currentVchrId == null)
                {
                    using var cmd = new SqlCommand(@"
                                                    INSERT INTO VchrTranLog
                                                        (VchrNo, VchrDate, VchrAmount, VchrType, VchrName, SalesManId, FromAccountId, ToAccountId, TranDate, UserId, Narration, FK_BranchId, SequenceNo, Terminal)
                                                    VALUES
                                                        (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @VchrName, @SalesManId, @FromAccountId, @ToAccountId, GETDATE(), @UserId, @Narration, @FK_BranchId, @SequenceNo, @Terminal);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int);
                                                    ", cn, tx);

                    cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                    cmd.Parameters.AddWithValue("@VchrDate", vchrDate.Date);
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromAccountId", (object)partyId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToAccountId", (object)bankAcctId ?? DBNull.Value);

                    if (userExists && loginUserId.HasValue && loginUserId > 0)
                        cmd.Parameters.AddWithValue("@UserId", loginUserId.Value);
                    else
                        cmd.Parameters.AddWithValue("@UserId", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Narration", narration);
                    cmd.Parameters.AddWithValue("@FK_BranchId", currentBranchId);
                    cmd.Parameters.AddWithValue("@SequenceNo", 1);
                    cmd.Parameters.AddWithValue("@Terminal", DBNull.Value);

                    object o = cmd.ExecuteScalar();
                    currentVchrId = o != null ? Convert.ToInt32(o) : (int?)null;
                }
                else
                {
                    using var cmd = new SqlCommand(@"
                                                    UPDATE VchrTranLog
                                                    SET VchrNo = @VchrNo,
                                                        VchrDate = @VchrDate,
                                                        VchrAmount = @VchrAmount,
                                                        SalesManId = @SalesManId,
                                                        FromAccountId = @FromAccountId,
                                                        ToAccountId = @ToAccountId,
                                                        Narration = @Narration
                                                    WHERE VchrTranId = @VchrTranId
                                                    ", cn, tx);

                    cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                    cmd.Parameters.AddWithValue("@VchrDate", vchrDate.Date);
                    cmd.Parameters.AddWithValue("@VchrAmount", amount);
                    cmd.Parameters.AddWithValue("@SalesManId", (object)salesManId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromAccountId", (object)partyId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToAccountId", (object)bankAcctId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Narration", narration);
                    cmd.Parameters.AddWithValue("@VchrTranId", currentVchrId.Value);
                    cmd.ExecuteNonQuery();

                    using var cmdDel = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId AND VchrType = @VchrType AND VchrName = @VchrName", cn, tx);
                    cmdDel.Parameters.AddWithValue("@VchrId", currentVchrId.Value);
                    cmdDel.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmdDel.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cmdDel.ExecuteNonQuery();
                }

                if (currentVchrId == null)
                    throw new Exception("Failed to create or identify voucher record (VchrTranId).");

                if (currentCheqDtlsId == null)
                {
                    using var cmd = new SqlCommand(@"
                                                    INSERT INTO ChequeDetails
                                                        (VchrId, VchrType, Type, AccountId, BankAcctId, Bank, Branch, CheqNo, CheqDate, Amount, Status, CollectedDate, RefNo, Remarks, TranDate, JeId, Date, ChqRtnAmount, FK_BranchId)
                                                    VALUES
                                                        (@VchrId, @VchrType, @Type, @AccountId, @BankAcctId, @Bank, @Branch, @CheqNo, @CheqDate, @Amount, @Status, @CollectedDate, @RefNo, @Remarks, GETDATE(), @JeId, @Date, @ChqRtnAmount, @FK_BranchId);
                                                    SELECT CAST(SCOPE_IDENTITY() AS int);
                                                    ", cn, tx);

                    cmd.Parameters.AddWithValue("@VchrId", currentVchrId.Value);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@Type", VCHR_NAME);
                    cmd.Parameters.AddWithValue("@AccountId", (object)partyId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankAcctId", (object)bankAcctId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Bank", bank);
                    cmd.Parameters.AddWithValue("@Branch", branch);
                    cmd.Parameters.AddWithValue("@CheqNo", cheqNo);
                    cmd.Parameters.AddWithValue("@CheqDate", chqDate.Date);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@CollectedDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RefNo", refNo);
                    cmd.Parameters.AddWithValue("@Remarks", narration);
                    cmd.Parameters.AddWithValue("@JeId", DBNull.Value); 
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    cmd.Parameters.AddWithValue("@ChqRtnAmount", 0m);
                    cmd.Parameters.AddWithValue("@FK_BranchId", currentBranchId);

                    object o = cmd.ExecuteScalar();
                    currentCheqDtlsId = o != null ? Convert.ToInt32(o) : (int?)null;
                }
                else
                {
                    using var cmd = new SqlCommand(@"
                                                    UPDATE ChequeDetails
                                                    SET VchrId=@VchrId, VchrType=@VchrType, Type=@Type, AccountId=@AccountId, BankAcctId=@BankAcctId,
                                                        Bank=@Bank, Branch=@Branch, CheqNo=@CheqNo, CheqDate=@CheqDate, Amount=@Amount, Status=@Status,
                                                        CollectedDate=@CollectedDate, RefNo=@RefNo, Remarks=@Remarks, JeId=@JeId, Date=@Date, ChqRtnAmount=@ChqRtnAmount
                                                    WHERE CheqDtlsId = @CheqDtlsId
                                                    ", cn, tx);

                    cmd.Parameters.AddWithValue("@VchrId", currentVchrId.Value);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@Type", VCHR_NAME);
                    cmd.Parameters.AddWithValue("@AccountId", (object)partyId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BankAcctId", (object)bankAcctId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Bank", bank);
                    cmd.Parameters.AddWithValue("@Branch", branch);
                    cmd.Parameters.AddWithValue("@CheqNo", cheqNo);
                    cmd.Parameters.AddWithValue("@CheqDate", chqDate.Date);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@CollectedDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@RefNo", refNo);
                    cmd.Parameters.AddWithValue("@Remarks", narration);
                    cmd.Parameters.AddWithValue("@JeId", DBNull.Value); // intentionally NULL
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    cmd.Parameters.AddWithValue("@ChqRtnAmount", 0m);
                    cmd.Parameters.AddWithValue("@CheqDtlsId", currentCheqDtlsId.Value);

                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(@"
                                                INSERT INTO TransactionLog
                                                    (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                                                VALUES
                                                    (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, GETDATE(), @VchrName)
                                                ", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@VchrId", currentVchrId.Value);
                    cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrDate", vchrDate.Date);
                    cmd.Parameters.AddWithValue("@AccountId", (object)partyId ?? DBNull.Value); // party (credit)
                    cmd.Parameters.AddWithValue("@OppAccountId", (object)bankAcctId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DebitAmount", 0m);
                    cmd.Parameters.AddWithValue("@CreditAmount", amount);
                    cmd.Parameters.AddWithValue("@Narration", narration);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(@"
                                                INSERT INTO TransactionLog
                                                    (VchrId, VchrNo, VchrType, VchrDate, AccountId, OppAccountId, DebitAmount, CreditAmount, Narration, TranDate, VchrName)
                                                VALUES
                                                    (@VchrId, @VchrNo, @VchrType, @VchrDate, @AccountId, @OppAccountId, @DebitAmount, @CreditAmount, @Narration, GETDATE(), @VchrName)
                                                ", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@VchrId", currentVchrId.Value);
                    cmd.Parameters.AddWithValue("@VchrNo", vchrNo);
                    cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                    cmd.Parameters.AddWithValue("@VchrDate", vchrDate.Date);
                    cmd.Parameters.AddWithValue("@AccountId", (object)bankAcctId ?? DBNull.Value); // bank (debit)
                    cmd.Parameters.AddWithValue("@OppAccountId", (object)partyId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DebitAmount", amount);
                    cmd.Parameters.AddWithValue("@CreditAmount", 0m);
                    cmd.Parameters.AddWithValue("@Narration", narration);
                    cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                MessageBox.Show("Saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                MessageBox.Show($"Error saving: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSelected()
        {
            if (!(postDatedGrid.SelectedItem is DataRowView drv))
            {
                MessageBox.Show("Select a record to delete.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(drv["VchrId"]?.ToString(), out int vchrId))
            {
                MessageBox.Show("Selected row has no voucher id.", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Confirm delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            using var cn = new SqlConnection(Settings.Default.strConnetionString);
            cn.Open();
            using var tx = cn.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand("DELETE FROM TransactionLog WHERE VchrId = @VchrId AND VchrType = @VchrType AND VchrName = @VchrName", cn, tx);
                cmd.Parameters.AddWithValue("@VchrId", vchrId);
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                cmd.ExecuteNonQuery();

                if (int.TryParse(drv["CheqDtlsId"]?.ToString(), out int cheqId))
                {
                    using var cmd2 = new SqlCommand("DELETE FROM ChequeDetails WHERE CheqDtlsId = @CheqDtlsId", cn, tx);
                    cmd2.Parameters.AddWithValue("@CheqDtlsId", cheqId);
                    cmd2.ExecuteNonQuery();
                }

                using var cmd3 = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId = @VchrTranId", cn, tx);
                cmd3.Parameters.AddWithValue("@VchrTranId", vchrId);
                cmd3.ExecuteNonQuery();

                tx.Commit();
                MessageBox.Show("Deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                MessageBox.Show($"Error deleting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion



        #region Utilities

        private int GetNextVoucherNo()
        {
            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand(@"
                                                SELECT ISNULL(MAX(VchrNo), 0) + 1
                                                FROM VchrTranLog AS v
                                                WHERE v.VchrType = @VchrType AND v.VchrName = @VchrName AND v.FK_BranchId = @BranchId
                                                ", cn);
                cmd.Parameters.AddWithValue("@VchrType", VCHR_TYPE);
                cmd.Parameters.AddWithValue("@VchrName", VCHR_NAME);
                cmd.Parameters.AddWithValue("@BranchId", currentBranchId);
                cn.Open();
                object o = cmd.ExecuteScalar();
                return o != null ? Convert.ToInt32(o) : 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion



        #region Grid selection -> load form fields

        private void postDatedGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!(postDatedGrid.SelectedItem is DataRowView drv)) return;
            LoadSelectedRowToForm(drv);
        }

        private void LoadSelectedRowToForm(DataRowView drv)
        {
            try
            {
                if (drv == null) return;

                if (int.TryParse(drv["VchrId"]?.ToString(), out int vchrId))
                {
                    voucherNoBox.Value = GetVchrNoById(vchrId);
                    DatePickerBox.SelectedDate = drv["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["Date"]);
                }

                txtBank.Text = drv["Bank"]?.ToString();
                txtBranch.Text = drv["Branch"]?.ToString();
                txtChqNo.Text = drv["CheqNo"]?.ToString();
                txtAmount.Text = drv["Amount"] == DBNull.Value ? "0.00" : Convert.ToDecimal(drv["Amount"]).ToString("N2");
                txtReferenceNo.Text = drv["RefNo"]?.ToString();
                txtRemarks.Text = drv["Remarks"]?.ToString();
                txtChqDate.SelectedDate = drv["CheqDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["CheqDate"]);

                if (drv.Row.Table.Columns.Contains("PartyAccountName") && drv["PartyAccountName"] != DBNull.Value)
                {
                    var partyName = drv["PartyAccountName"].ToString();
                    foreach (var item in txtPartyBox2.Items)
                    {
                        if (item is DataRowView r && r["AccountName"].ToString() == partyName)
                        {
                            txtPartyBox2.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (drv.Row.Table.Columns.Contains("BankAccountName") && drv["BankAccountName"] != DBNull.Value)
                {
                    var bankName = drv["BankAccountName"].ToString();
                    foreach (var item in txtBankAccountBox.Items)
                    {
                        if (item is DataRowView r && r["AccountName"].ToString() == bankName)
                        {
                            txtBankAccountBox.SelectedItem = item;
                            break;
                        }
                    }
                }

                collectionStatusComboBox.SelectedItem = drv["Status"]?.ToString() ?? "Pending";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading item into form: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GetVchrNoById(int vchrId)
        {
            try
            {
                using var cn = new SqlConnection(Settings.Default.strConnetionString);
                using var cmd = new SqlCommand("SELECT v.VchrNo FROM VchrTranLog AS v WHERE v.VchrTranId = @VchrTranId", cn);
                cmd.Parameters.AddWithValue("@VchrTranId", vchrId);
                cn.Open();
                object o = cmd.ExecuteScalar();
                return o != null ? Convert.ToInt32(o) : 0;
            }
            catch
            {
                return 0;
            }
        }


        private void postDatedGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (postDatedGrid.SelectedItem is not DataRowView drv) return;

            try
            {
                voucherNoBox.Value = int.TryParse(drv["VchrId"]?.ToString(), out int vchrId) ? vchrId : 0;
                DatePickerBox.SelectedDate = drv["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["Date"]);

                txtBank.Text = drv["Bank"]?.ToString() ?? string.Empty;
                txtBranch.Text = drv["Branch"]?.ToString() ?? string.Empty;
                txtChqNo.Text = drv["CheqNo"]?.ToString() ?? string.Empty;
                txtCheque.Text = drv["CheqNo"]?.ToString() ?? string.Empty; 
                txtAmount.Text = drv["Amount"] == DBNull.Value
                    ? "0.00"
                    : Convert.ToDecimal(drv["Amount"]).ToString("N2", CultureInfo.InvariantCulture);
                txtReferenceNo.Text = drv["RefNo"]?.ToString() ?? string.Empty;
                txtRemarks.Text = drv["Remarks"]?.ToString() ?? string.Empty;
                txtChqDate.SelectedDate = drv["CheqDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(drv["CheqDate"]);

                if (drv.Row.Table.Columns.Contains("PartyAccountName") && drv["PartyAccountName"] != DBNull.Value)
                {
                    SelectComboBoxItemByName(txtPartyBox, drv["PartyAccountName"].ToString());
                    SelectComboBoxItemByName(txtPartyBox2, drv["PartyAccountName"].ToString());
                }
                else
                {
                    txtPartyBox.SelectedIndex = -1;
                    txtPartyBox2.SelectedIndex = -1;
                }

                if (drv.Row.Table.Columns.Contains("BankAccountName") && drv["BankAccountName"] != DBNull.Value)
                {
                    SelectComboBoxItemByName(txtBankAccountBox, drv["BankAccountName"].ToString());
                }
                else
                {
                    txtBankAccountBox.SelectedIndex = -1;
                }

                collectionStatusComboBox.SelectedItem = drv["Status"]?.ToString() ?? "Pending";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading selection: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectComboBoxItemByName(ComboBox combo, string accountName)
        {
            foreach (var item in combo.Items)
            {
                if (item is DataRowView r && r.Row.Table.Columns.Contains("AccountName")
                    && r["AccountName"].ToString() == accountName)
                {
                    combo.SelectedItem = item;
                    break;
                }
            }
        }

        #endregion
    }
}
