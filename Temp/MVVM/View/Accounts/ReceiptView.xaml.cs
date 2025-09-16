using AccountsLibrary.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FlexMold.MVVM.View.Accounts
{
    public partial class ReceiptView : UserControl
    {
        private DataTable _gridTable;

        public ReceiptView()
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            InitializeComponent();

            if (string.IsNullOrWhiteSpace(Settings.Default.strConnetionString))
            {
                throw new InvalidOperationException("Connection string not configured: Properties.Settings.Default.strConnetionString");
            }

            var today = DateTime.Now.Date;
            ReceiptDateBox.SelectedDate = today;
            routeDateBox.SelectedDate = today.AddDays(-7);
            vehicleDateBox.SelectedDate = today;

            BuildGridTable();

            receiptGrid.CellEditEnding += ReceiptGrid_CellEditEnding;
            routeDateBox.SelectedDateChanged += DatePicker_SelectedDateChanged_AutoLoad;
            vehicleDateBox.SelectedDateChanged += DatePicker_SelectedDateChanged_AutoLoad;
            routeComboBox.SelectionChanged += ComboOrFilter_Changed_AutoLoad;
            vehicleComboBox.SelectionChanged += ComboOrFilter_Changed_AutoLoad;
            txtSalesExi.TextChanged += TxtSalesExi_TextChanged_AutoLoad;
        }

        private void BuildGridTable()
        {
            _gridTable = new DataTable();

            _gridTable.Columns.Add("SalesInvId", typeof(int));
            _gridTable.Columns.Add("SL", typeof(int));
            _gridTable.Columns.Add("SalesDate", typeof(DateTime));
            _gridTable.Columns.Add("InvoiceNo", typeof(string));
            _gridTable.Columns.Add("Party", typeof(string));
            _gridTable.Columns.Add("GrandDiscount", typeof(decimal));
            _gridTable.Columns.Add("ExistingTotalPayment", typeof(decimal));
            _gridTable.Columns.Add("RemainingCredit", typeof(decimal));
            _gridTable.Columns.Add("CashPayment", typeof(decimal));
            _gridTable.Columns.Add("CashAccountId", typeof(int));
            _gridTable.Columns.Add("CardPayment", typeof(decimal));
            _gridTable.Columns.Add("CardAccountId", typeof(int));
            _gridTable.Columns.Add("OtherPayment", typeof(decimal));
            _gridTable.Columns.Add("OtherAccountId", typeof(int));
            _gridTable.Columns.Add("IsSettled", typeof(bool));
            _gridTable.Columns.Add("PartyAccountId", typeof(int));

            receiptGrid.ItemsSource = _gridTable.DefaultView;
        }

        private void DatePicker_SelectedDateChanged_AutoLoad(object sender, SelectionChangedEventArgs e)
        {
            LoadSalesIntoGrid();
        }

        private void ComboOrFilter_Changed_AutoLoad(object sender, SelectionChangedEventArgs e)
        {
            LoadSalesIntoGrid();
        }

        private void TxtSalesExi_TextChanged_AutoLoad(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoadSalesIntoGrid();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadSalesIntoGrid();
        }

        private void LoadSalesIntoGrid()
        {
            DateTime from = (routeDateBox.SelectedDate ?? DateTime.Now.Date);
            DateTime to = (vehicleDateBox.SelectedDate ?? DateTime.Now.Date);
            if (to < from)
            {
                MessageBox.Show("From date must be less than or equal to To date.", "Date range error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int? routeId = null, vehicleId = null;
            if (routeComboBox.SelectedValue != null && int.TryParse(routeComboBox.SelectedValue.ToString(), out int routeIdVal))
                routeId = routeIdVal;
            if (vehicleComboBox.SelectedValue != null && int.TryParse(vehicleComboBox.SelectedValue.ToString(), out int vehicleIdVal))
                vehicleId = vehicleIdVal;

            int salesExecId = 0;
            if (!string.IsNullOrWhiteSpace(txtSalesExi.Text))
                int.TryParse(txtSalesExi.Text, out salesExecId);

            var sql = @"
                        SELECT 
                            s.SalesInvId,
                            s.SalesDate,
                            ISNULL(a.AccountName, '') AS PartyName,
                            ISNULL(s.GrandDiscount, 0) AS GrandDiscount,
                            ISNULL(s.TotalPayment, 0) AS TotalPayment,
                            ISNULL(s.Credit, 0) AS Credit,
                            ISNULL(s.PartyAccountId, 0) AS PartyAccountId
                        FROM SalesEstimateMaster s
                        LEFT JOIN Account a ON s.PartyAccountId = a.AccountId
                        WHERE s.FK_BranchId = @branch
                          AND s.SalesDate BETWEEN @from AND @to
                    ";

            if (routeId.HasValue) sql += " AND s.RouteId = @routeId";
            if (vehicleId.HasValue) sql += " AND s.VehId = @vehId";
            if (salesExecId > 0) sql += " AND s.StafId = @stafId";

            sql += " AND (ISNULL(s.Credit,0) - ISNULL(s.TotalPayment,0)) > 0";

            var dt = new DataTable();
            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@branch", GetBranchId());
                cmd.Parameters.AddWithValue("@from", from.Date);
                // include full day of `to` by adding end-of-day
                cmd.Parameters.AddWithValue("@to", to.Date.AddDays(1).AddTicks(-1));
                if (routeId.HasValue) cmd.Parameters.AddWithValue("@routeId", routeId.Value);
                if (vehicleId.HasValue) cmd.Parameters.AddWithValue("@vehId", vehicleId.Value);
                if (salesExecId > 0) cmd.Parameters.AddWithValue("@stafId", salesExecId);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading invoices: {ex.Message}", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            _gridTable.Rows.Clear();
            int sl = 1;
            foreach (DataRow row in dt.Rows)
            {
                decimal grand = row.Field<decimal?>("GrandDiscount") ?? 0m;
                decimal totalPayment = row.Field<decimal?>("TotalPayment") ?? 0m;
                decimal credit = row.Field<decimal?>("Credit") ?? grand;
                decimal remaining = credit - totalPayment;
                if (remaining < 0) remaining = 0m;

                var dr = _gridTable.NewRow();
                dr["SalesInvId"] = row.Field<int>("SalesInvId");
                dr["SL"] = sl++;
                dr["SalesDate"] = row.Field<DateTime>("SalesDate");
                dr["InvoiceNo"] = row.Field<int>("SalesInvId").ToString();
                dr["Party"] = row.Field<string>("PartyName");
                dr["GrandDiscount"] = grand;
                dr["ExistingTotalPayment"] = totalPayment;
                dr["RemainingCredit"] = remaining;
                dr["CashPayment"] = 0m;
                dr["CashAccountId"] = DBNull.Value;
                dr["CardPayment"] = 0m;
                dr["CardAccountId"] = DBNull.Value;
                dr["OtherPayment"] = 0m;
                dr["OtherAccountId"] = DBNull.Value;
                dr["IsSettled"] = false;
                dr["PartyAccountId"] = row.Field<int?>("PartyAccountId") ?? 0;
                _gridTable.Rows.Add(dr);
            }

            receiptGrid.ItemsSource = _gridTable.DefaultView;
            RecalculateTotals();
        }

        private int GetBranchId()
        {
            try
            {
                return Convert.ToInt32(Settings.Default.iBranchId);
            }
            catch
            {
                return 1;
            }
        }

        private void ReceiptGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                receiptGrid.CommitEdit(DataGridEditingUnit.Row, true);

                if (receiptGrid.ItemsSource is DataView dv)
                {
                    var rowView = (DataRowView)dv[e.Row.GetIndex()];
                    RecalculateRow(rowView.Row);
                    RecalculateTotals();
                }
            }
        }

        private void RecalculateRow(DataRow row)
        {
            decimal grand = row.Field<decimal?>("GrandDiscount") ?? 0m;
            decimal existing = row.Field<decimal?>("ExistingTotalPayment") ?? 0m;
            decimal cash = row.Field<decimal?>("CashPayment") ?? 0m;
            decimal card = row.Field<decimal?>("CardPayment") ?? 0m;
            decimal other = row.Field<decimal?>("OtherPayment") ?? 0m;

            decimal remaining = (grand - existing) - (cash + card + other);
            if (remaining < 0m) remaining = 0m;

            row["RemainingCredit"] = remaining;
            row["IsSettled"] = remaining == 0m;
        }

        private void RecalculateTotals()
        {
            decimal totalCash = 0m, totalCard = 0m, totalOther = 0m, totalCredit = 0m, totalAmount = 0m;

            foreach (DataRow r in _gridTable.Rows)
            {
                totalCash += Convert.ToDecimal(r["CashPayment"] ?? 0m);
                totalCard += Convert.ToDecimal(r["CardPayment"] ?? 0m);
                totalOther += Convert.ToDecimal(r["OtherPayment"] ?? 0m);
                totalAmount += Convert.ToDecimal(r["GrandDiscount"] ?? 0m);
                totalCredit += Convert.ToDecimal(r["RemainingCredit"] ?? 0m);
            }

            txtTotalCash.Text = totalCash.ToString("N2");
            txtTotalCard.Text = totalCard.ToString("N2");
            txtOnlineTotal.Text = totalOther.ToString("N2");
            txtCreditTotal.Text = totalCredit.ToString("N2");
            txtTotalAmount.Text = totalAmount.ToString("N2");
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveAllPayments();
        }

        private void SaveAllPayments()
        {
            if (_gridTable.Rows.Count == 0)
            {
                MessageBox.Show("No invoices loaded.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int salesExecId = 0;
            int.TryParse(txtSalesExi.Text, out salesExecId);
            string remarks = txtRemarks.Text ?? string.Empty;

            using (var cn = new SqlConnection(Settings.Default.strConnetionString))
            {
                cn.Open();
                using (var tran = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow r in _gridTable.Rows)
                        {
                            int salesInvId = Convert.ToInt32(r["SalesInvId"]);
                            int partyAccountId = Convert.ToInt32(r["PartyAccountId"] ?? 0);
                            decimal cash = Convert.ToDecimal(r["CashPayment"] ?? 0m);
                            object cashAccountObj = r["CashAccountId"];
                            int? cashAccountId = (cashAccountObj == DBNull.Value ? (int?)null : Convert.ToInt32(cashAccountObj));
                            decimal card = Convert.ToDecimal(r["CardPayment"] ?? 0m);
                            object cardAccountObj = r["CardAccountId"];
                            int? cardAccountId = (cardAccountObj == DBNull.Value ? (int?)null : Convert.ToInt32(cardAccountObj));
                            decimal other = Convert.ToDecimal(r["OtherPayment"] ?? 0m);
                            object otherAccountObj = r["OtherAccountId"];
                            int? otherAccountId = (otherAccountObj == DBNull.Value ? (int?)null : Convert.ToInt32(otherAccountObj));
                            decimal existingTotal = Convert.ToDecimal(r["ExistingTotalPayment"] ?? 0m);
                            decimal grand = Convert.ToDecimal(r["GrandDiscount"] ?? 0m);

                            decimal sumNewPayments = cash + card + other;
                            if (sumNewPayments <= 0m) continue;

                            if (cash > 0m && cashAccountId.HasValue)
                                InsertVchrTranLog(cn, tran, "Cash Receipt", cash, partyAccountId, cashAccountId.Value, salesExecId, remarks);

                            if (card > 0m && cardAccountId.HasValue)
                                InsertVchrTranLog(cn, tran, "Bank Receipt", card, partyAccountId, cardAccountId.Value, salesExecId, remarks);

                            if (other > 0m && otherAccountId.HasValue)
                                InsertVchrTranLog(cn, tran, "Bank Receipt", other, partyAccountId, otherAccountId.Value, salesExecId, remarks);

                            decimal newTotalPayment = existingTotal + sumNewPayments;
                            bool isSettled = newTotalPayment >= grand;

                            var updateSql = @"
                                UPDATE SalesEstimateMaster
                                SET TotalPayment = @newTotalPayment,
                                    IsSettiled = @isSettled
                                WHERE SalesInvId = @salesInvId
                            ";
                            using (var upd = new SqlCommand(updateSql, cn, tran))
                            {
                                upd.Parameters.AddWithValue("@newTotalPayment", newTotalPayment);
                                upd.Parameters.AddWithValue("@isSettled", isSettled ? 1 : 0);
                                upd.Parameters.AddWithValue("@salesInvId", salesInvId);
                                upd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadSalesIntoGrid();
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { }
                        MessageBox.Show($"Error saving payments: {ex.Message}", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void InsertVchrTranLog(SqlConnection cn, SqlTransaction tran, string vchrName, decimal amount, int fromAccountId, int toAccountId, int salesManId, string narration)
        {
            int branchId = GetBranchId();

            var nextVchrNoSql = @"
                                SELECT ISNULL(MAX(VchrNo), 0) + 1
                                FROM VchrTranLog
                                WHERE VchrType = @vchrType AND VchrName = @vchrName AND FK_BranchId = @branch
                            ";
            int nextVchrNo;
            using (var cmd = new SqlCommand(nextVchrNoSql, cn, tran))
            {
                cmd.Parameters.AddWithValue("@vchrType", "Receipt Voucher");
                cmd.Parameters.AddWithValue("@vchrName", vchrName);
                cmd.Parameters.AddWithValue("@branch", branchId);
                nextVchrNo = Convert.ToInt32(cmd.ExecuteScalar());
            }

            var insertSql = @"
                            INSERT INTO VchrTranLog
                            (VchrNo, VchrDate, VchrAmount, VchrType, FromAccountId, ToAccountId,
                             Narration, SalesManId, UserId, TranDate, VchrName, FK_BranchId, SequenceNo, Terminal, TerminalName)
                            VALUES
                            (@VchrNo, @VchrDate, @VchrAmount, @VchrType, @FromAccountId, @ToAccountId,
                             @Narration, @SalesManId, @UserId, @TranDate, @VchrName, @FK_BranchId, @SequenceNo, @Terminal, @TerminalName);
                          ";

            int userId = GetCurrentUserIdOrDefault();

            using (var cmd = new SqlCommand(insertSql, cn, tran))
            {
                cmd.Parameters.AddWithValue("@VchrNo", nextVchrNo);
                cmd.Parameters.AddWithValue("@VchrDate", (ReceiptDateBox.SelectedDate ?? DateTime.Now.Date));
                cmd.Parameters.AddWithValue("@VchrAmount", amount);
                cmd.Parameters.AddWithValue("@VchrType", "Receipt Voucher");
                cmd.Parameters.AddWithValue("@FromAccountId", fromAccountId);
                cmd.Parameters.AddWithValue("@ToAccountId", toAccountId);
                cmd.Parameters.AddWithValue("@Narration", narration ?? string.Empty);
                cmd.Parameters.AddWithValue("@SalesManId", (object)(salesManId > 0 ? salesManId : (object)DBNull.Value));
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@TranDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@VchrName", vchrName);
                cmd.Parameters.AddWithValue("@FK_BranchId", branchId);
                cmd.Parameters.AddWithValue("@SequenceNo", DBNull.Value);
                cmd.Parameters.AddWithValue("@Terminal", DBNull.Value);
                cmd.Parameters.AddWithValue("@TerminalName", DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        private int GetCurrentUserIdOrDefault()
        {
            try
            {
                return Convert.ToInt32(Settings.Default.intLogOnId);
            }
            catch
            {
                return 1;
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            txtSalesExi.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTotalCash.Text = "0.00";
            txtTotalCard.Text = "0.00";
            txtOnlineTotal.Text = "0.00";
            txtCreditTotal.Text = "0.00";
            txtTotalAmount.Text = "0.00";
            _gridTable.Rows.Clear();
            receiptGrid.ItemsSource = _gridTable.DefaultView;
        }
    }
}
