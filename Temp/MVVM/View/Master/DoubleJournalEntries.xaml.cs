using AccountsLibrary;
using AccountsLibrary.Properties;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountsPRO.MVVM.View.Master
{
    public partial class DoubleJournalEntries : Window
    {
        private int? editingVchrId = null;
        private string VoucherType = "Journal Voucher";
        private int? iVchrTranId;
        private bool blCredit = false;
        private decimal dmlClosingBalance;
        private decimal dmlPaidAmount;

        public DoubleJournalEntries()
        {
            InitializeComponent();
            LoadAccounts();
            LoadVouchers();
            DatePickBox.SelectedDate = DateTime.Now;
        }

        #region Models
        public class JournalEntry
        {
            public int SL { get; set; }
            public int VoucherNo { get; set; }
            public DateTime Date { get; set; }
            public string FromAccount { get; set; }
            public string ToAccount { get; set; }
            public double Amount { get; set; }
            public string Narration { get; set; }
            public int VchrId { get; set; }
        }

        public class Account
        {
            public int AccountId { get; set; }
            public string AccountName { get; set; }
        }
        #endregion

        #region Load Data
        private void LoadAccounts()
        {
            if (AniHelper.vchrTranLog == null)
                AniHelper.vchrTranLog = new VchrTranLog();

            iVchrTranId = AniHelper.vchrTranLog.VchrTranId;
            var accounts = new List<Account>();
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountId, AccountName FROM Account", con);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        accounts.Add(new Account
                        {
                            AccountId = rdr.GetInt32(0),
                            AccountName = rdr.GetString(1)
                        });
                    }
                }
            }
            FromAccCreditBox.ItemsSource = accounts;
            FromAccCreditBox.DisplayMemberPath = "AccountName";
            FromAccCreditBox.SelectedValuePath = "AccountId";

            ToAccDebitBox.ItemsSource = accounts;
            ToAccDebitBox.DisplayMemberPath = "AccountName";
            ToAccDebitBox.SelectedValuePath = "AccountId";
        }

        private void LoadVouchers()
        {
            var entries = new List<JournalEntry>();
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand(@"SELECT VchrTranId, VchrNo, VchrDate, 
                                          FromAccountId, ToAccountId, VchrAmount, Narration 
                                          FROM VchrTranLog ORDER BY VchrNo", con);
                using (var rdr = cmd.ExecuteReader())
                {
                    int sl = 1;
                    while (rdr.Read())
                    {
                        entries.Add(new JournalEntry
                        {
                            SL = sl++,
                            VchrId = !rdr.IsDBNull(0) ? rdr.GetInt32(0) : 0,
                            VoucherNo = !rdr.IsDBNull(1) ? rdr.GetInt32(1) : 0,
                            Date = !rdr.IsDBNull(2) ? rdr.GetDateTime(2) : DateTime.MinValue,
                            FromAccount = !rdr.IsDBNull(3) ? GetAccountName(rdr.GetInt32(3)) : "",
                            ToAccount = !rdr.IsDBNull(4) ? GetAccountName(rdr.GetInt32(4)) : "",
                            Amount = !rdr.IsDBNull(5) ? Convert.ToDouble(rdr.GetDecimal(5)) : 0,
                            Narration = !rdr.IsDBNull(6) ? rdr.GetString(6) : ""
                        });

                    }
                }
            }
            JournalDataGrid.ItemsSource = entries;
        }

        private string GetAccountName(int accountId)
        {
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountName FROM Account WHERE AccountId=@id", con);
                cmd.Parameters.AddWithValue("@id", accountId);
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? "";
            }
        }
        #endregion

        #region Button Events
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (UpDownBox.Value == null || UpDownBox.Value == 0)
            {
                MessageBox.Show("Voucher number is required.");
                return;
            }

            if (string.IsNullOrEmpty(FromAccCreditBox.Text))
            {
                MessageBox.Show("Account Credit Selection is required");
                return;
            }

            if (string.IsNullOrEmpty(ToAccDebitBox.Text))
            {
                MessageBox.Show("Account Debit selection is required");
                return;
            }


            if (!decimal.TryParse(AmountBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount2) || amount2 <= 0m)
            {
                MessageBox.Show("Enter a valid Amount greater than zero.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (amount2 > 99999999.99m)
            {
                MessageBox.Show("Limit Exceeded. Please Enter Valid Amount.");
                return;
            }

            int fromId = (int)FromAccCreditBox.SelectedValue;
            int toId = (int)ToAccDebitBox.SelectedValue;
            int vNo = (int)UpDownBox.Value;
            DateTime vDate = DatePickBox.SelectedDate ?? DateTime.Now;
            decimal amount = decimal.TryParse(AmountBox.Text, out decimal a) ? a : 0;
            string narration = NarrationBox.Text;

            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                SqlCommand cmd;

                if (editingVchrId.HasValue)
                {
                    // Update existing
                    cmd = new SqlCommand(@"UPDATE VchrTranLog 
                                           SET VchrNo=@vNo, VchrDate=@vDate, 
                                               FromAccountId=@fromId, ToAccountId=@toId, 
                                               VchrAmount=@amount, Narration=@narr
                                           WHERE VchrTranId=@id", con);
                    cmd.Parameters.AddWithValue("@id", editingVchrId.Value);

                    cmd.Parameters.AddWithValue("@vNo", vNo);
                    cmd.Parameters.AddWithValue("@vDate", vDate);
                    cmd.Parameters.AddWithValue("@fromId", fromId);
                    cmd.Parameters.AddWithValue("@toId", toId);
                    cmd.Parameters.AddWithValue("@amount", (decimal)amount);
                    cmd.Parameters.AddWithValue("@narr", narration);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    // Insert new
                    //cmd = new SqlCommand(@"INSERT INTO VchrTranLog 
                    //                       (VchrNo, VchrDate, FromAccountId, ToAccountId, VchrAmount, Narration) 
                    //                       VALUES(@vNo,@vDate,@fromId,@toId,@amount,@narr)", con);

                    if (AniHelper.vchrTranLog == null)
                        AniHelper.vchrTranLog = new VchrTranLog();
                    //if (btnSave.Text == "Save")
                    //{
                    //    if (plgDtls != null && !plgDtls.SaveData)
                    //    {
                    //        Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
                    //        return;
                    //    }


                    if (AniHelper.dBContext == null)
                        AniHelper.dBContext = new DBContext();


                    int iVchrNo = (AniHelper.dBContext.VchrTransaction(new
                    {
                        VchrType = VoucherType,
                        FK_BranchId = Settings.Default.iBranchId
                    }).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;

                    EntityLoadMethod(AniHelper.vchrTranLog, iVchrNo);
                    AniHelper.vchrTranLog.VchrTranId = null;
                    if (AshproCommon.InsertToDatabaseObj(AniHelper.vchrTranLog, "VchrTranLog"))
                    {
                        iVchrTranId = (from x in AniHelper.dBContext.VchrTransaction()
                                       where x.VchrNo == AniHelper.vchrTranLog.VchrNo && x.VchrType == AniHelper.vchrTranLog.VchrType
                                       select x).FirstOrDefault().VchrTranId;
                        if (blCredit)
                        {
                            POSHelper.TranLogMethod(@amount, 0m, @fromId, @toId, iVchrTranId, @vNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Main", AniHelper.vchrTranLog.VchrType, DatePickBox.SelectedDate.ToString());
                            POSHelper.TranLogMethod(0m, @amount, @toId, @fromId, iVchrTranId, @vNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Sub", AniHelper.vchrTranLog.VchrType, DatePickBox.SelectedDate.ToString());
                        }
                        else
                        {
                            POSHelper.TranLogMethod(0m, @amount, @fromId, @toId, iVchrTranId, @vNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Main", AniHelper.vchrTranLog.VchrType, DatePickBox.SelectedDate.ToString());
                            POSHelper.TranLogMethod(@amount, 0m, @toId, @fromId, iVchrTranId, @vNo, AniHelper.vchrTranLog.VchrType, AniHelper.vchrTranLog.UserId, "Sub", AniHelper.vchrTranLog.VchrType, DatePickBox.SelectedDate.ToString());
                        }
                        //btnClear_Click(null, null);
                    }
                    //    return;
                    //}

                }

            }

            ClearForm();
            MessageBox.Show("Entry Successfully saved!");
            LoadVouchers();
        }

        private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo)
        {
            vchrTranLog.VchrDate = (DateTime)DatePickBox.SelectedDate;
            vchrTranLog.VchrTranId = iVchrTranId;
            vchrTranLog.VchrNo = iVchrNo;
            vchrTranLog.VchrAmount = Decimal.Parse(AmountBox.Text);
            vchrTranLog.VchrType = VoucherType;
            vchrTranLog.VchrName = VoucherType;
            vchrTranLog.SalesManId = Settings.Default.intLogOnId;
            vchrTranLog.FromAccountId = (int?)FromAccCreditBox.SelectedValue;
            vchrTranLog.ToAccountId = (int?)ToAccDebitBox.SelectedValue;
            vchrTranLog.TranDate = DateTime.Now.Date;
            vchrTranLog.UserId = Settings.Default.intLogOnId;
            vchrTranLog.Narration = NarrationBox.Text;
            vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                editingVchrId = selected.VchrId;
                UpDownBox.Value = selected.VoucherNo;
                DatePickBox.SelectedDate = selected.Date;

                FromAccCreditBox.SelectedValue = GetAccountIdByName(selected.FromAccount);
                ToAccDebitBox.SelectedValue = GetAccountIdByName(selected.ToAccount);

                AmountBox.Text = selected.Amount.ToString("N2");
                NarrationBox.Text = selected.Narration;
            }
            else
            {
               // MessageBox.Show("Select a row to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using (var con = new SqlConnection(Settings.Default.strConnetionString))
                    {
                        con.Open();
                        var cmd = new SqlCommand("DELETE FROM VchrTranLog WHERE VchrTranId=@id", con);
                        cmd.Parameters.AddWithValue("@id", selected.VchrId);
                        cmd.ExecuteNonQuery();
                    }
                    ClearForm();
                    LoadVouchers();
                }
            }
            else
            {
                //MessageBox.Show("Select a row to delete.");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            editingVchrId = null;
            UpDownBox.Value = 0;
            DatePickBox.SelectedDate = DateTime.Now;
            FromAccCreditBox.SelectedIndex = -1;
            ToAccDebitBox.SelectedIndex = -1;
            AmountBox.Text = "0.00";
            NarrationBox.Text = "";
        }
        #endregion

        #region Helpers
        private int GetAccountIdByName(string name)
        {
            using (var con = new SqlConnection(Settings.Default.strConnetionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT AccountId FROM Account WHERE AccountName=@name", con);
                cmd.Parameters.AddWithValue("@name", name);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        private void Amount_TextPreviewInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !e.Text.All(char.IsDigit); // this is here because it will now only allow numeric data to be enterered.

            // Allow only digits and optionally one dot for decimal
            //if (e.Text.All(char.IsDigit))
            //{
            //    // Input is valid
            //    e.Handled = false; // allow input
            //}
            //else
            //{
            //    // Input is invalid
            //    e.Handled = true; // block input
            //    MessageBox.Show("Please Enter Valid Numeric Data");
            //}


        }
        #endregion

        private void JournalDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JournalDataGrid.SelectedItem is JournalEntry selected)
            {
                editingVchrId = selected.VchrId;
                UpDownBox.Value = selected.VoucherNo;
                DatePickBox.SelectedDate = selected.Date;

                FromAccCreditBox.SelectedValue = GetAccountIdByName(selected.FromAccount);
                ToAccDebitBox.SelectedValue = GetAccountIdByName(selected.ToAccount);

                AmountBox.Text = selected.Amount.ToString("N2");
                NarrationBox.Text = selected.Narration;


                //VchrTransaction vchrTransaction = selected;//(VchrTransaction)dgDetails.CurrentRow.DataBoundItem;
                iVchrTranId = selected.VchrId;
                //UpDownBox.Value = vchrTransaction.VchrNo;
                LoadSingleObjectMethod();

            }
            else
            {
               // MessageBox.Show("Select a row to edit.");
            }
        }

        private void FromAccCreditBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            //LabelsVisibleMethod(isToVisible: true);
            String lblTransactionType = "";
            //lblBeforeFrom//UpDownBox
            if (FromAccCreditBox.SelectedValue == null)
            {
                if (AniHelper.dBContext == null)
                    AniHelper.dBContext = new DBContext();


                int iVchrNo = (AniHelper.dBContext.VchrTransaction(new
                {
                    VchrType = VoucherType,
                    FK_BranchId = Settings.Default.iBranchId
                }).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;

                UpDownBox.Value = iVchrNo;

                LoadSingleObjectMethod();
            }
            try
            {
                (dmlClosingBalance, lblTransactionType) = AniHelper.AccountBalanceFindMethod(0m, 0m, FromAccCreditBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
                lblBeforeFrom.Content = lblTransactionType;
                //lblAfterFrom
                (dmlClosingBalance, lblTransactionType) = AniHelper.AccountBalanceFindMethod(0m, 0m, FromAccCreditBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
                lblAfterFrom.Content = lblTransactionType;

                if (dmlClosingBalance < 0m)
                {
                    blCredit = true;
                }
                else
                {
                    blCredit = false;
                }
            }
            catch { }
        }

        private void LoadSingleObjectMethod()
        {
            //LabelsVisibleMethod(isToVisible: true);
            //isEditing = true;
            iVchrTranId = AniHelper.vchrTranLog.VchrTranId;
            DatePickBox.SelectedDate = AniHelper.vchrTranLog.VchrDate;
            dmlPaidAmount = AniHelper.vchrTranLog.VchrAmount;
            AmountBox.Text = dmlPaidAmount.ToString();
            ToAccDebitBox.SelectedValue = AniHelper.vchrTranLog.ToAccountId.GetValueOrDefault();
            FromAccCreditBox.SelectedValue = AniHelper.vchrTranLog.FromAccountId.GetValueOrDefault();
            NarrationBox.Text = AniHelper.vchrTranLog.Narration;
            //(dmlClosingBalance, lblTransactionType) = AniHelper.AccountBalanceFindMethod(0m, 0m, FromAccCreditBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
            decimal tempdec;
            (tempdec, lblBeforeFrom.Content) = AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, FromAccCreditBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
            (tempdec, lblAfterFrom.Content) = AniHelper.AccountBalanceFindMethod(0m, 0m, FromAccCreditBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
            (tempdec, lblBeforeTo.Content) = AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, ToAccDebitBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
            (tempdec, lblAfterTo.Content) = AniHelper.AccountBalanceFindMethod(0m, 0m, ToAccDebitBox.SelectedValue.ToString(), DatePickBox.SelectedDate.Value.AddHours(24.0).AddSeconds(-1.0));
            //EnableMethod();
        }

        private void UpDownBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (AniHelper.dBContext == null)
                AniHelper.dBContext = new DBContext();

            AniHelper.vchrTranLog = (from x in AniHelper.dBContext.VchrTransaction()
                                     where x.VchrType == VoucherType && x.VchrNo == UpDownBox.Value
                                     select x).FirstOrDefault();
            if (AniHelper.vchrTranLog != null)
            {
                LoadSingleObjectMethod();
            }
            else
            {
                //btnClearMethod();
            }
        }
    }
}
