using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ReceiptView : BaseForm
	{
		private string sTitle;

		private DateTime InvoiceDate;

		private List<VchrTransaction> entVTransactions;

		private int? iVchrTranId;

		private DBContext db;

		private decimal dmlClosingBalance;

		private VchrTranLog vchrTranLog;

		private bool isEditing = false;

		private decimal dmlPaidAmount;

		private string sVType = "Receipt Voucher";

		private IContainer components = null;

		private CheckBox cbDiscount;

		private AshTextBoxNormal txtDis;

		private CheckBox cbMail;

		private Label lblEndBalance;

		private Label lblHead;

		private Label lblStartingBalance;

		private Label label3;

		private Label lblAmountInWords;

		private AshUpDown nudVoucherNo;

		private CheckBox cbPrint;

		private Label label4;

		private AshDateTimePicker dtmDate;

		private Button btnDate;

		private Label lblCashAccnt;

		private Label label6;

		private AshComboBox cmbOppAcct;

		private Label label7;

		private AshTextBoxNormal txtAmount;

		private Label label8;

		private AshTextBoxNormal txtNarration;

		private CheckBox cbRefresh;

		private Label lblCashAcctBal;

		private DataGridView dgDetails;

		private Label nlblCashABalance;

		private Label label9;

		private Label lblTotal;

		private AshComboBox cmbPaidBySrch;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private GroupBox grpSearch;

		private Label label10;

		private Label label2;

		private AshComboBox cmbPaidBy;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_VchrNo;

		private DataGridViewTextBoxColumn col_Date;

		private DataGridViewTextBoxColumn col_CashAccount;

		private DataGridViewTextBoxColumn col_PartyAccount;

		private DataGridViewTextBoxColumn col_Amount;

		private DataGridViewTextBoxColumn col_Narration;

		private DataGridViewTextBoxColumn col_PaidBy;

		private AshComboBox cmbAcct;

		public int? iVoucherNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public ReceiptView(string _sTitle)
		{
			sTitle = _sTitle;
			InitializeComponent();
			ColorChange.Color(this);
			col_Amount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
		}

		private void DGVLoadMethod(List<VchrTranLog> entVTransac)
		{
			if (!cbRefresh.Checked)
			{
				return;
			}
			dgDetails.AutoGenerateColumns = false;
			dgDetails.AutoGenerateColumns = false;
			if (entVTransac.Count > 0)
			{
				entVTransactions = POSHelper.GetVTransactionsFromVDetails(entVTransac, db);
				dgDetails.DataSource = entVTransactions;
				lblTotal.Text = entVTransactions.Sum((VchrTransaction x) => x.VchrAmount).ToString(CommonSettings.strCurencyFormat);
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbAcct, lblCashAcctBal, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
		}

		private void DisableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnDelete.Enabled = false;
		}

		private void EnableMethod()
		{
			foreach (Control control in base.Controls)
			{
				if (control.GetType() != typeof(Label))
				{
					control.Enabled = false;
				}
			}
			grpSearch.Enabled = true;
			nudVoucherNo.Enabled = true;
			btnEdit.Enabled = true;
			btnPrint.Enabled = true;
			btnMail.Enabled = true;
			btnDelete.Enabled = true;
			btnClear.Enabled = true;
			btnEnd.Enabled = true;
		}

		private void DeleteSubMethod()
		{
			DataTable dataTable = new DataTable();
			string[] obj = new string[7] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null, null };
			int? num = iVchrTranId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType='";
			obj[3] = sVType;
			obj[4] = "'and VchrName='";
			obj[5] = sTitle;
			obj[6] = "'";
			dataTable = AshproCommon.GetDataTable(string.Concat(obj));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
		}

		private void LoadSingleObjectMethod()
		{
			try
			{
				isEditing = true;
				iVchrTranId = vchrTranLog.VchrTranId;
				dtmDate.Value = vchrTranLog.VchrDate;
				dmlPaidAmount = vchrTranLog.VchrAmount;
				txtAmount.Value = dmlPaidAmount;
				if (vchrTranLog.ToAccountId.HasValue)
				{
					cmbAcct.SelectedValue = vchrTranLog.ToAccountId;
				}
				if (vchrTranLog.FromAccountId.HasValue)
				{
					cmbOppAcct.SelectedValue = vchrTranLog.FromAccountId;
				}
				txtNarration.Text = vchrTranLog.Narration;
				cmbPaidBy.SelectedValue = vchrTranLog.SalesManId;
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				txtDis.Value = 0m;
				EnableMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo, decimal dmlAmnt, string sVchrType, string sVchrName, int iFromAcctId, int iToAcctId, string sNarration)
		{
			vchrTranLog.VchrDate = dtmDate.Value;
			vchrTranLog.VchrTranId = iVchrTranId;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = dmlAmnt;
			vchrTranLog.VchrType = sVchrType;
			vchrTranLog.VchrName = sVchrName;
			vchrTranLog.SalesManId = cmbPaidBy.SelectedValue.ToInt32();
			vchrTranLog.FromAccountId = iFromAcctId;
			vchrTranLog.ToAccountId = iToAcctId;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = sNarration;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
			vchrTranLog.SequenceNo = Utility.uSerialNumber;
			vchrTranLog.Terminal = Utility.uTerminal;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			cmbPaidBySrch_SelectedIndexChanged(null, null);
			cbDiscount.Enabled = true;
			dmlClosingBalance = (dmlPaidAmount = 0m);
			iVchrTranId = null;
			dtmDate.Value = InvoiceDate;
			lblAmountInWords.Text = "Amount in Words:";
			Label label = lblStartingBalance;
			lblEndBalance.Visible = false;
			label.Visible = false;
			isEditing = false;
			DisableMethod();
		}

		private void ReceiptView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			lblHead.Text = sTitle;
			AniHelper.FillEmployees(cmbPaidBy);
			AniHelper.FillEmployees(cmbPaidBySrch);
			AniHelper.FillForReceipt(cmbOppAcct);
			if (sTitle == "Cash Receipt")
			{
				AniHelper.FillCashAccount(cmbAcct);
			}
			else
			{
				AniHelper.FillBankAccount(cmbAcct);
				nlblCashABalance.Text = "Bank Account Balance:";
				lblCashAccnt.Text = "Bank Account :";
			}
			InvoiceDate = Settings.Default.appDate.Date;
			dtmDate.Value = InvoiceDate;
			cmbPaidBySrch.Text = "ALL";
			nudVoucherNo.Value = AshproCommon.IdFindMethod("Select Max(VchrNo) as VchrNo from VchrTranLog where VchrType='" + sVType + "' and VchrName='" + sTitle + "' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: true);
			if (iVoucherNo.HasValue)
			{
				nudVoucherNo.Value = iVoucherNo.ToInt32();
				LoadSingleObjectMethod();
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			InvoiceDate = dtmDate.Value;
		}

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			vchrTranLog = (from x in db.VchrTransaction()
				where x.VchrNo == nudVoucherNo.Value.ToInt32() && x.VchrType == sVType && x.VchrName == sTitle
				select x).FirstOrDefault();
			if (vchrTranLog != null)
			{
				LoadSingleObjectMethod();
			}
			else
			{
				btnClearMethod();
			}
		}

		private void cmbOppAcct_SelectedIndexChanged(object sender, EventArgs e)
		{
			dmlClosingBalance = default(decimal);
			if (!isEditing)
			{
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
			else if (cmbOppAcct.SelectedValue.ToInt32() == vchrTranLog.FromAccountId)
			{
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, txtAmount.Value, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
			else
			{
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
			Label label = lblStartingBalance;
			lblEndBalance.Visible = true;
			label.Visible = true;
		}

		private void cmbAcct_SelectedIndexChanged(object sender, EventArgs e)
		{
			List<VchrTranLog> list = new List<VchrTranLog>();
			list = db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sTitle,
				FK_BranchId = Settings.Default.iBranchId,
				FromAccountId = cmbAcct.SelectedValue.ToInt32()
			});
			DGVLoadMethod(list);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == sVType && x.VchrName == sTitle
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			btnClearMethod();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage())
				{
					DeleteSubMethod();
					if (AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", iVchrTranId.ToInt32()))
					{
						btnClear_Click(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DisableMethod();
			cbDiscount.Enabled = false;
			txtDis.Enabled = false;
			dtmDate.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtAmount.Value <= 0m)
			{
				MessageBox.Show("The amount paid must be greater than zero", Utility.uManufacturer);
				return;
			}
			vchrTranLog = new VchrTranLog();
			if (!iVchrTranId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				int iVchrNo = (db.VchrTransaction(new
				{
					VchrType = sVType,
					VchrName = sTitle
				}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo, txtAmount.Value, sVType, sTitle, cmbOppAcct.SelectedValue.ToInt32(), cmbAcct.SelectedValue.ToInt32(), txtNarration.Text);
				vchrTranLog.VchrTranId = null;
				if (!AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					return;
				}
				vchrTranLog = (from x in db.VchrTransaction()
					where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.VchrName == vchrTranLog.VchrName
					select x).FirstOrDefault();
				POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
				POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
				if (cbDiscount.Checked && txtDis.Value > 0m)
				{
					if (dmlClosingBalance > 0m)
					{
						int iFromAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName='OTHER DISCOUNT GIVEN'");
						int iVchrNo2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Credit Note'", isVoucherNo: true);
						EntityLoadMethod(vchrTranLog, iVchrNo2, txtDis.Value, "Credit Note", "Credit Note", iFromAcctId, cmbOppAcct.SelectedValue.ToInt32(), "Discount Paid From Receipt Voucher");
						vchrTranLog.VchrTranId = null;
						if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
						{
							iVchrTranId = (from x in db.VchrTransaction()
								where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
								select x).FirstOrDefault().VchrTranId.ToInt32();
							POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
							POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
						}
					}
					else
					{
						int iToAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName='OTHER DISCOUNT RECEIVED'");
						int iVchrNo3 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Debit Note'", isVoucherNo: true);
						EntityLoadMethod(vchrTranLog, iVchrNo3, txtDis.Value, "Debit Note", "Debit Note", cmbOppAcct.SelectedValue.ToInt32(), iToAcctId, "Discount Received From Receipt Voucher");
						vchrTranLog.VchrTranId = null;
						if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
						{
							iVchrTranId = (from x in db.VchrTransaction()
								where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
								select x).FirstOrDefault().VchrTranId.ToInt32();
							POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
							POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
						}
					}
				}
				btnClear_Click(null, null);
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else
			{
				DeleteSubMethod();
				EntityLoadMethod(vchrTranLog, nudVoucherNo.Value.ToInt32(), txtAmount.Value, sVType, sTitle, cmbOppAcct.SelectedValue.ToInt32(), cmbAcct.SelectedValue.ToInt32(), txtNarration.Text);
				if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iVchrTranId.ToInt32()))
				{
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
					btnClear_Click(null, null);
				}
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				VchrTransaction vchrTransaction = (VchrTransaction)dgDetails.CurrentRow.DataBoundItem;
				iVchrTranId = vchrTransaction.VchrTranId;
				nudVoucherNo.Value = vchrTransaction.VchrNo;
				LoadSingleObjectMethod();
			}
		}

		private void cbDiscount_CheckedChanged(object sender, EventArgs e)
		{
			txtDis.Enabled = cbDiscount.Checked;
		}

		private void dtmDate_ValueChanged(object sender, EventArgs e)
		{
			cmbPaidBySrch_SelectedIndexChanged(null, null);
		}

		private void cmbPaidBySrch_SelectedIndexChanged(object sender, EventArgs e)
		{
			List<VchrTranLog> list = new List<VchrTranLog>();
			list = ((!(cmbPaidBySrch.Text == "All")) ? db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sTitle,
				FK_BranchId = Settings.Default.iBranchId,
				VchrDate = AniHelper.GetDate(dtmDate.Value.Date),
				SalesManId = cmbPaidBySrch.SelectedValue.ToInt32()
			}) : db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sTitle,
				FK_BranchId = Settings.Default.iBranchId,
				VchrDate = AniHelper.GetDate(dtmDate.Value.Date)
			}));
			DGVLoadMethod(list);
		}

		private void txtAmount_TextChanged(object sender, EventArgs e)
		{
			lblStartingBalance.Visible = true;
			lblEndBalance.Visible = true;
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(dmlPaidAmount, txtAmount.Value, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			if (!isEditing)
			{
				txtDis.Value = Math.Abs(dmlClosingBalance);
			}
			if (txtAmount.Value > 0m)
			{
				lblAmountInWords.Text = "Amount in Words: " + AniHelper.GetAmountInWords(Math.Round(txtAmount.Value).ToInt32()) + " Only.";
			}
			else
			{
				lblAmountInWords.Text = "Amount in Words: ";
			}
		}

		private void txtNarration_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
		}

		private void ReceiptView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnEdit_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDate_Click(null, null);
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnMail_Click(null, null);
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			cbDiscount = new System.Windows.Forms.CheckBox();
			txtDis = new AshUsercontrol.AshTextBoxNormal();
			cbMail = new System.Windows.Forms.CheckBox();
			lblEndBalance = new System.Windows.Forms.Label();
			lblHead = new System.Windows.Forms.Label();
			lblStartingBalance = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			lblAmountInWords = new System.Windows.Forms.Label();
			nudVoucherNo = new AshUsercontrol.AshUpDown();
			cbPrint = new System.Windows.Forms.CheckBox();
			label4 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			btnDate = new System.Windows.Forms.Button();
			lblCashAccnt = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			cmbOppAcct = new AshUsercontrol.AshComboBox();
			label7 = new System.Windows.Forms.Label();
			txtAmount = new AshUsercontrol.AshTextBoxNormal();
			label8 = new System.Windows.Forms.Label();
			txtNarration = new AshUsercontrol.AshTextBoxNormal();
			cbRefresh = new System.Windows.Forms.CheckBox();
			lblCashAcctBal = new System.Windows.Forms.Label();
			dgDetails = new System.Windows.Forms.DataGridView();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_VchrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CashAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PartyAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Narration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PaidBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
			nlblCashABalance = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			lblTotal = new System.Windows.Forms.Label();
			cmbPaidBySrch = new AshUsercontrol.AshComboBox();
			toolTip = new System.Windows.Forms.ToolTip(components);
			btnClear = new System.Windows.Forms.Button();
			btnMail = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			grpSearch = new System.Windows.Forms.GroupBox();
			label10 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			cmbPaidBy = new AshUsercontrol.AshComboBox();
			cmbAcct = new AshUsercontrol.AshComboBox();
			((System.ComponentModel.ISupportInitialize)nudVoucherNo).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			grpSearch.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(885, 0);
			cbDiscount.AutoSize = true;
			cbDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDiscount.Location = new System.Drawing.Point(431, 384);
			cbDiscount.Name = "cbDiscount";
			cbDiscount.Size = new System.Drawing.Size(95, 20);
			cbDiscount.TabIndex = 43;
			cbDiscount.Text = "Discount :";
			cbDiscount.UseVisualStyleBackColor = true;
			cbDiscount.CheckedChanged += new System.EventHandler(cbDiscount_CheckedChanged);
			txtDis.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtDis.Data = 0;
			txtDis.Enabled = false;
			txtDis.EnterColor = System.Drawing.Color.LightPink;
			txtDis.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtDis.Format = null;
			txtDis.isAllowNegative = false;
			txtDis.isAllowSpecialChar = false;
			txtDis.isNumberOnly = false;
			txtDis.isNumeric = true;
			txtDis.LeaveColor = System.Drawing.Color.White;
			txtDis.Location = new System.Drawing.Point(534, 388);
			txtDis.Margin = new System.Windows.Forms.Padding(4);
			txtDis.Name = "txtDis";
			txtDis.Size = new System.Drawing.Size(198, 16);
			txtDis.TabIndex = 44;
			txtDis.Text = "0.00";
			txtDis.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtDis.WaterMark = null;
			cbMail.AutoSize = true;
			cbMail.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbMail.Location = new System.Drawing.Point(151, 498);
			cbMail.Name = "cbMail";
			cbMail.Size = new System.Drawing.Size(141, 20);
			cbMail.TabIndex = 64;
			cbMail.Text = "Mail While Saving";
			cbMail.UseVisualStyleBackColor = true;
			lblEndBalance.AutoSize = true;
			lblEndBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblEndBalance.ForeColor = System.Drawing.Color.Red;
			lblEndBalance.Location = new System.Drawing.Point(798, 416);
			lblEndBalance.Name = "lblEndBalance";
			lblEndBalance.Size = new System.Drawing.Size(40, 17);
			lblEndBalance.TabIndex = 61;
			lblEndBalance.Text = "0.00";
			lblEndBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblEndBalance.Visible = false;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 8);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(134, 24);
			lblHead.TabIndex = 65;
			lblHead.Text = "Cash Receipt";
			lblStartingBalance.AutoSize = true;
			lblStartingBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblStartingBalance.ForeColor = System.Drawing.Color.DodgerBlue;
			lblStartingBalance.Location = new System.Drawing.Point(798, 329);
			lblStartingBalance.Name = "lblStartingBalance";
			lblStartingBalance.Size = new System.Drawing.Size(40, 17);
			lblStartingBalance.TabIndex = 60;
			lblStartingBalance.Text = "0.00";
			lblStartingBalance.Visible = false;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(35, 331);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(87, 16);
			label3.TabIndex = 33;
			label3.Text = "Vouchr No :";
			lblAmountInWords.AutoSize = true;
			lblAmountInWords.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblAmountInWords.ForeColor = System.Drawing.Color.Red;
			lblAmountInWords.Location = new System.Drawing.Point(15, 468);
			lblAmountInWords.Name = "lblAmountInWords";
			lblAmountInWords.Size = new System.Drawing.Size(126, 16);
			lblAmountInWords.TabIndex = 62;
			lblAmountInWords.Text = "Amount in Words:";
			nudVoucherNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			nudVoucherNo.Location = new System.Drawing.Point(125, 329);
			nudVoucherNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudVoucherNo.Maximum = new decimal(new int[4] { -727379968, 232, 0, 0 });
			nudVoucherNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVoucherNo.Name = "nudVoucherNo";
			nudVoucherNo.Size = new System.Drawing.Size(161, 23);
			nudVoucherNo.TabIndex = 34;
			nudVoucherNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudVoucherNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVoucherNo.ValueChanged += new System.EventHandler(nudVoucherNo_ValueChanged);
			cbPrint.AutoSize = true;
			cbPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbPrint.Location = new System.Drawing.Point(3, 497);
			cbPrint.Name = "cbPrint";
			cbPrint.Size = new System.Drawing.Size(146, 20);
			cbPrint.TabIndex = 63;
			cbPrint.Text = "Print While Saving";
			cbPrint.UseVisualStyleBackColor = true;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(473, 335);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(50, 16);
			label4.TabIndex = 35;
			label4.Text = "Date :";
			dtmDate.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(536, 329);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(102, 23);
			dtmDate.TabIndex = 36;
			dtmDate.ValueChanged += new System.EventHandler(dtmDate_ValueChanged);
			btnDate.Location = new System.Drawing.Point(743, 328);
			btnDate.Name = "btnDate";
			btnDate.Size = new System.Drawing.Size(26, 23);
			btnDate.TabIndex = 56;
			btnDate.Text = "...";
			toolTip.SetToolTip(btnDate, "Press Alt + T");
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			lblCashAccnt.AutoSize = true;
			lblCashAccnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblCashAccnt.Location = new System.Drawing.Point(412, 361);
			lblCashAccnt.Name = "lblCashAccnt";
			lblCashAccnt.Size = new System.Drawing.Size(111, 16);
			lblCashAccnt.TabIndex = 39;
			lblCashAccnt.Text = "Cash Account :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(48, 360);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 16);
			label6.TabIndex = 37;
			label6.Text = "Account :";
			cmbOppAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbOppAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbOppAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbOppAcct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbOppAcct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbOppAcct.FormattingEnabled = true;
			cmbOppAcct.LeaveColor = System.Drawing.Color.White;
			cmbOppAcct.Location = new System.Drawing.Point(125, 357);
			cmbOppAcct.Name = "cmbOppAcct";
			cmbOppAcct.Size = new System.Drawing.Size(244, 24);
			cmbOppAcct.TabIndex = 38;
			cmbOppAcct.SelectedIndexChanged += new System.EventHandler(cmbOppAcct_SelectedIndexChanged);
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(53, 385);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(69, 16);
			label7.TabIndex = 41;
			label7.Text = "Amount :";
			txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAmount.Data = 0;
			txtAmount.EnterColor = System.Drawing.Color.LightPink;
			txtAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAmount.Format = null;
			txtAmount.isAllowNegative = false;
			txtAmount.isAllowSpecialChar = false;
			txtAmount.isNumberOnly = false;
			txtAmount.isNumeric = true;
			txtAmount.LeaveColor = System.Drawing.Color.White;
			txtAmount.Location = new System.Drawing.Point(125, 388);
			txtAmount.Margin = new System.Windows.Forms.Padding(4);
			txtAmount.Name = "txtAmount";
			txtAmount.Size = new System.Drawing.Size(244, 16);
			txtAmount.TabIndex = 42;
			txtAmount.Text = "0.00";
			txtAmount.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtAmount.WaterMark = null;
			txtAmount.TextChanged += new System.EventHandler(txtAmount_TextChanged);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(43, 438);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(79, 16);
			label8.TabIndex = 47;
			label8.Text = "Narration :";
			txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNarration.Data = 0;
			txtNarration.EnterColor = System.Drawing.Color.LightPink;
			txtNarration.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNarration.Format = null;
			txtNarration.isAllowNegative = false;
			txtNarration.isAllowSpecialChar = false;
			txtNarration.isNumberOnly = false;
			txtNarration.isNumeric = false;
			txtNarration.LeaveColor = System.Drawing.Color.White;
			txtNarration.Location = new System.Drawing.Point(124, 440);
			txtNarration.Margin = new System.Windows.Forms.Padding(4);
			txtNarration.Name = "txtNarration";
			txtNarration.Size = new System.Drawing.Size(622, 16);
			txtNarration.TabIndex = 48;
			txtNarration.Value = new decimal(new int[4]);
			txtNarration.WaterMark = null;
			txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNarration_KeyDown);
			cbRefresh.AutoSize = true;
			cbRefresh.Checked = true;
			cbRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
			cbRefresh.Location = new System.Drawing.Point(481, 17);
			cbRefresh.Name = "cbRefresh";
			cbRefresh.Size = new System.Drawing.Size(112, 20);
			cbRefresh.TabIndex = 2;
			cbRefresh.Text = "Auto Refresh";
			cbRefresh.UseVisualStyleBackColor = true;
			lblCashAcctBal.AutoSize = true;
			lblCashAcctBal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblCashAcctBal.ForeColor = System.Drawing.Color.Red;
			lblCashAcctBal.Location = new System.Drawing.Point(638, 249);
			lblCashAcctBal.Name = "lblCashAcctBal";
			lblCashAcctBal.Size = new System.Drawing.Size(40, 17);
			lblCashAcctBal.TabIndex = 7;
			lblCashAcctBal.Text = "0.00";
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(col_SL, col_VchrNo, col_Date, col_CashAccount, col_PartyAccount, col_Amount, col_Narration, col_PaidBy);
			dgDetails.Location = new System.Drawing.Point(14, 44);
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgDetails.Size = new System.Drawing.Size(858, 193);
			dgDetails.TabIndex = 3;
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellContentClick);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 50;
			col_VchrNo.DataPropertyName = "VchrNo";
			col_VchrNo.HeaderText = "Voucher No";
			col_VchrNo.Name = "col_VchrNo";
			col_VchrNo.ReadOnly = true;
			col_VchrNo.Width = 80;
			col_Date.DataPropertyName = "VchrDate";
			col_Date.HeaderText = "Date";
			col_Date.Name = "col_Date";
			col_Date.ReadOnly = true;
			col_Date.Width = 90;
			col_CashAccount.DataPropertyName = "ToAccount";
			col_CashAccount.HeaderText = "Cash Account";
			col_CashAccount.Name = "col_CashAccount";
			col_CashAccount.ReadOnly = true;
			col_CashAccount.Width = 150;
			col_PartyAccount.DataPropertyName = "FromAccount";
			col_PartyAccount.HeaderText = "Party Account";
			col_PartyAccount.Name = "col_PartyAccount";
			col_PartyAccount.ReadOnly = true;
			col_PartyAccount.Width = 180;
			col_Amount.DataPropertyName = "VchrAmount";
			col_Amount.HeaderText = "Amount";
			col_Amount.Name = "col_Amount";
			col_Amount.ReadOnly = true;
			col_Amount.Width = 80;
			col_Narration.DataPropertyName = "Narration";
			col_Narration.HeaderText = "Narration";
			col_Narration.Name = "col_Narration";
			col_Narration.ReadOnly = true;
			col_Narration.Width = 200;
			col_PaidBy.DataPropertyName = "SalesMan";
			col_PaidBy.HeaderText = "Paid By";
			col_PaidBy.Name = "col_PaidBy";
			col_PaidBy.ReadOnly = true;
			nlblCashABalance.AutoSize = true;
			nlblCashABalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			nlblCashABalance.ForeColor = System.Drawing.Color.DodgerBlue;
			nlblCashABalance.Location = new System.Drawing.Point(419, 249);
			nlblCashABalance.Name = "nlblCashABalance";
			nlblCashABalance.Size = new System.Drawing.Size(175, 17);
			nlblCashABalance.TabIndex = 6;
			nlblCashABalance.Text = "Cash Account Balance:";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(21, 411);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(101, 16);
			label9.TabIndex = 45;
			label9.Text = "Collected By :";
			lblTotal.AutoSize = true;
			lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTotal.ForeColor = System.Drawing.Color.Red;
			lblTotal.Location = new System.Drawing.Point(301, 249);
			lblTotal.Name = "lblTotal";
			lblTotal.Size = new System.Drawing.Size(40, 17);
			lblTotal.TabIndex = 5;
			lblTotal.Text = "0.00";
			cmbPaidBySrch.EnterColor = System.Drawing.Color.LightPink;
			cmbPaidBySrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPaidBySrch.FormattingEnabled = true;
			cmbPaidBySrch.LeaveColor = System.Drawing.Color.White;
			cmbPaidBySrch.Location = new System.Drawing.Point(206, 15);
			cmbPaidBySrch.Name = "cmbPaidBySrch";
			cmbPaidBySrch.Size = new System.Drawing.Size(228, 24);
			cmbPaidBySrch.TabIndex = 1;
			cmbPaidBySrch.SelectedIndexChanged += new System.EventHandler(cmbPaidBySrch_SelectedIndexChanged);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(685, 491);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 52;
			btnClear.Text = "Clear";
			toolTip.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnMail.BackColor = System.Drawing.Color.DodgerBlue;
			btnMail.Enabled = false;
			btnMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnMail.ForeColor = System.Drawing.Color.White;
			btnMail.Location = new System.Drawing.Point(841, 491);
			btnMail.Name = "btnMail";
			btnMail.Size = new System.Drawing.Size(75, 30);
			btnMail.TabIndex = 54;
			btnMail.Text = "Mail";
			toolTip.SetToolTip(btnMail, "Press Alt + M");
			btnMail.UseVisualStyleBackColor = false;
			btnMail.Click += new System.EventHandler(btnMail_Click);
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.Enabled = false;
			btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Location = new System.Drawing.Point(763, 491);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(75, 30);
			btnPrint.TabIndex = 53;
			btnPrint.Text = "Print";
			toolTip.SetToolTip(btnPrint, "Press Alt + P");
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(607, 491);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 51;
			btnDelete.Text = "Delete";
			toolTip.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.Enabled = false;
			btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Location = new System.Drawing.Point(529, 491);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new System.Drawing.Size(75, 30);
			btnEdit.TabIndex = 50;
			btnEdit.Text = "Edit";
			toolTip.SetToolTip(btnEdit, "Press Alt + E");
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(451, 491);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 49;
			btnSave.Text = "Save";
			toolTip.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			grpSearch.Controls.Add(cbRefresh);
			grpSearch.Controls.Add(lblCashAcctBal);
			grpSearch.Controls.Add(dgDetails);
			grpSearch.Controls.Add(nlblCashABalance);
			grpSearch.Controls.Add(lblTotal);
			grpSearch.Controls.Add(cmbPaidBySrch);
			grpSearch.Controls.Add(label10);
			grpSearch.Controls.Add(label2);
			grpSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			grpSearch.Location = new System.Drawing.Point(20, 41);
			grpSearch.Name = "grpSearch";
			grpSearch.Size = new System.Drawing.Size(888, 279);
			grpSearch.TabIndex = 59;
			grpSearch.TabStop = false;
			grpSearch.Text = "Search";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label10.ForeColor = System.Drawing.Color.DodgerBlue;
			label10.Location = new System.Drawing.Point(81, 249);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(50, 17);
			label10.TabIndex = 4;
			label10.Text = "Total:";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(107, 17);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(96, 16);
			label2.TabIndex = 0;
			label2.Text = "Collected By:";
			cmbPaidBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPaidBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPaidBy.EnterColor = System.Drawing.Color.LightPink;
			cmbPaidBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPaidBy.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbPaidBy.FormattingEnabled = true;
			cmbPaidBy.LeaveColor = System.Drawing.Color.White;
			cmbPaidBy.Location = new System.Drawing.Point(124, 409);
			cmbPaidBy.Name = "cmbPaidBy";
			cmbPaidBy.Size = new System.Drawing.Size(244, 24);
			cmbPaidBy.TabIndex = 46;
			cmbAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbAcct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAcct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbAcct.FormattingEnabled = true;
			cmbAcct.LeaveColor = System.Drawing.Color.White;
			cmbAcct.Location = new System.Drawing.Point(534, 357);
			cmbAcct.Name = "cmbAcct";
			cmbAcct.Size = new System.Drawing.Size(198, 24);
			cmbAcct.TabIndex = 38;
			cmbAcct.SelectedIndexChanged += new System.EventHandler(cmbOppAcct_SelectedIndexChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(920, 523);
			base.Controls.Add(cbDiscount);
			base.Controls.Add(txtDis);
			base.Controls.Add(cbMail);
			base.Controls.Add(lblEndBalance);
			base.Controls.Add(lblHead);
			base.Controls.Add(lblStartingBalance);
			base.Controls.Add(label3);
			base.Controls.Add(lblAmountInWords);
			base.Controls.Add(nudVoucherNo);
			base.Controls.Add(cbPrint);
			base.Controls.Add(label4);
			base.Controls.Add(dtmDate);
			base.Controls.Add(btnDate);
			base.Controls.Add(lblCashAccnt);
			base.Controls.Add(label6);
			base.Controls.Add(cmbAcct);
			base.Controls.Add(cmbOppAcct);
			base.Controls.Add(label7);
			base.Controls.Add(txtAmount);
			base.Controls.Add(label8);
			base.Controls.Add(txtNarration);
			base.Controls.Add(label9);
			base.Controls.Add(grpSearch);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnMail);
			base.Controls.Add(btnPrint);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnEdit);
			base.Controls.Add(btnSave);
			base.Controls.Add(cmbPaidBy);
			base.Name = "ReceiptView";
			base.Load += new System.EventHandler(ReceiptView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ReceiptView_KeyDown);
			base.Controls.SetChildIndex(cmbPaidBy, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnEdit, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnPrint, 0);
			base.Controls.SetChildIndex(btnMail, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(grpSearch, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(txtNarration, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(txtAmount, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cmbOppAcct, 0);
			base.Controls.SetChildIndex(cmbAcct, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(lblCashAccnt, 0);
			base.Controls.SetChildIndex(btnDate, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(cbPrint, 0);
			base.Controls.SetChildIndex(nudVoucherNo, 0);
			base.Controls.SetChildIndex(lblAmountInWords, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(lblStartingBalance, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(lblEndBalance, 0);
			base.Controls.SetChildIndex(cbMail, 0);
			base.Controls.SetChildIndex(txtDis, 0);
			base.Controls.SetChildIndex(cbDiscount, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			((System.ComponentModel.ISupportInitialize)nudVoucherNo).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			grpSearch.ResumeLayout(false);
			grpSearch.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
