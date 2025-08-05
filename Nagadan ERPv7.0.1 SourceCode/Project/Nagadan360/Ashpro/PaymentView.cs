using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class PaymentView : BaseForm
	{
		private string sTitle;

		private DateTime InvoiceDate;

		private List<VchrTransaction> entVTransactions;

		private int? iVchrTranId;

		private DBContext db;

		private decimal dmlClosingBalance;

		private VchrTranLog vchrTranLog;

		private decimal dmlPaidAmount;

		private bool isEditing = false;

		private string sVType = "Payment Voucher";

		private int? iBranchId;

		private IContainer components = null;

		private Button button4;

		private Label lblCashAcctBal;

		private CheckBox cbRefresh;

		private DataGridView dgDetails;

		private AshComboBox cmbPaidBySrch;

		private PrintDocument printDocument1;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private GroupBox grpSearch;

		private Label nlblCashABalance;

		private Label lblTotal;

		private Label label10;

		private Label label2;

		private CheckBox cbDiscount;

		private AshTextBoxNormal txtDis;

		private Label lblEndBalance;

		private Label lblHead;

		private AshComboBox cmbAcct;

		private Label lblStartingBalance;

		private Label label3;

		private Label lblAmountInWords;

		private AshUpDown nudVoucherNo;

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

		private AshComboBox cmbPaidBy;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column10;
        private Label label9;

		public int? iVoucherNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public PaymentView(string _sTitle)
		{
			sTitle = _sTitle;
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
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
			isEditing = true;
			iVchrTranId = vchrTranLog.VchrTranId;
			dtmDate.Value = vchrTranLog.VchrDate;
			dmlPaidAmount = vchrTranLog.VchrAmount;
			txtAmount.Value = dmlPaidAmount;
			cmbAcct.SelectedValue = vchrTranLog.FromAccountId.GetValueOrDefault();
			cmbOppAcct.SelectedValue = vchrTranLog.ToAccountId.GetValueOrDefault();
			txtNarration.Text = vchrTranLog.Narration;
			cmbPaidBy.SelectedValue = vchrTranLog.SalesManId.GetValueOrDefault();
			AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			txtDis.Value = 0m;
			EnableMethod();
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo, decimal dmlAmnt, string sVchrType, string sVchrName, int iFromAcctId, int iToAcctId, string sNarration)
		{
			vchrTranLog.VchrTranId = iVchrTranId;
			vchrTranLog.VchrDate = dtmDate.Value;
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
			vchrTranLog.FK_BranchId = iBranchId;
			vchrTranLog.Terminal = Utility.uTerminal;
			vchrTranLog.SequenceNo = Utility.uSerialNumber;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			cmbPaidBySrch_SelectedIndexChanged(null, null);
			cbDiscount.Enabled = true;
			dmlClosingBalance = (dmlPaidAmount = 0m);
			btnSave.Text = "Save";
			dtmDate.Value = InvoiceDate;
			lblAmountInWords.Text = "Amount in Words:";
			Label label = lblStartingBalance;
			lblEndBalance.Visible = false;
			label.Visible = false;
			isEditing = false;
			DisableMethod();
		}

		private void PaymentView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			lblHead.Text = sTitle;
			iBranchId = Settings.Default.iBranchId;
			AniHelper.FillEmployees(cmbPaidBy);
			AniHelper.FillEmployees(cmbPaidBySrch);
			AniHelper.FillForPayment(cmbOppAcct);
			if (sTitle == "Cash Payment")
			{
				AniHelper.FillCashAccount(cmbAcct);
			}
			else
			{
				AniHelper.FillBankAccount(cmbAcct);
				nlblCashABalance.Text = "Bank Account Balance:";
				lblCashAccnt.Text = "Bank Account :";
			}
			InvoiceDate = Settings.Default.appDate;
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
				where x.VchrNo == nudVoucherNo.Value.ToInt32() && x.VchrType == sVType && x.VchrName == sTitle && x.FK_BranchId == Settings.Default.iBranchId
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
			else if (cmbOppAcct.SelectedValue.ToInt32() == vchrTranLog.ToAccountId)
			{
				AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(txtAmount.Value, dmlPaidAmount, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
			else
			{
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbOppAcct, lblStartingBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, txtAmount.Value, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
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
				where x.VchrType == sVType && x.VchrName == sTitle && x.FK_BranchId == Settings.Default.iBranchId
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
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DisableMethod();
			btnSave.Text = "Update";
			cbDiscount.Enabled = false;
			txtDis.Enabled = false;
			dtmDate.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtAmount.Value <= 0m)
			{
				Messages.WarningMessage();
				return;
			}
			vchrTranLog = new VchrTranLog();
			if (btnSave.Text == "Save")
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				int iVchrNo = ((from x in db.VchrTransaction()
					where x.VchrType == sVType && x.VchrName == sTitle && x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo, txtAmount.Value, sVType, sTitle, cmbAcct.SelectedValue.ToInt32(), cmbOppAcct.SelectedValue.ToInt32(), txtNarration.Text);
				vchrTranLog.VchrTranId = null;
				if (!AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					return;
				}
				if (!iBranchId.HasValue)
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.VchrName == vchrTranLog.VchrName
						select x).FirstOrDefault().VchrTranId.ToInt32();
				}
				else
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.VchrName == vchrTranLog.VchrName && x.FK_BranchId == iBranchId
						select x).FirstOrDefault().VchrTranId.ToInt32();
				}
				POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", sTitle, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", sTitle, AniHelper.GetDate(dtmDate.Value));
				if (cbDiscount.Checked && txtDis.Value > 0m)
				{
					if (dmlClosingBalance > 0m)
					{
						int iFromAcctId = AshproCommon.IdFindMethod("Select AccountId From Account  Where AccountName='OTHER DISCOUNT RECEIVED'");
						int iVchrNo2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Debit Note'", isVoucherNo: true);
						vchrTranLog = new VchrTranLog();
						EntityLoadMethod(vchrTranLog, iVchrNo2, txtDis.Value, "Debit Note", "Debit Note", iFromAcctId, cmbOppAcct.SelectedValue.ToInt32(), "Discount Received From Payment Voucher");
						vchrTranLog.VchrTranId = null;
						if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
						{
							iVchrTranId = (from x in db.VchrTransaction()
								where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.FK_BranchId == Settings.Default.iBranchId
								select x).FirstOrDefault().VchrTranId.ToInt32();
							POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
							POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
						}
					}
					else
					{
						int iToAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName ='OTHER DISCOUNT GIVEN'");
						int iVchrNo3 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Credit Note'", isVoucherNo: true);
						vchrTranLog = new VchrTranLog();
						EntityLoadMethod(vchrTranLog, iVchrNo3, txtDis.Value, "Credit Note", "Credit Note", cmbOppAcct.SelectedValue.ToInt32(), iToAcctId, "Discount Paid From Payment Voucher");
						vchrTranLog.VchrTranId = null;
						if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
						{
							iVchrTranId = (from x in db.VchrTransaction()
								where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.FK_BranchId == Settings.Default.iBranchId
								select x).FirstOrDefault().VchrTranId.ToInt32();
							POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
							POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
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
				EntityLoadMethod(vchrTranLog, nudVoucherNo.Value.ToInt32(), txtAmount.Value, sVType, sTitle, cmbAcct.SelectedValue.ToInt32(), cmbOppAcct.SelectedValue.ToInt32(), txtNarration.Text);
				DeleteSubMethod();
				if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iVchrTranId.ToString().ToInt32()))
				{
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value));
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

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
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
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbOppAcct, lblEndBalance, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
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

		private void PaymentView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.K && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDate_Click(null, null);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button4 = new System.Windows.Forms.Button();
            this.lblCashAcctBal = new System.Windows.Forms.Label();
            this.cbRefresh = new System.Windows.Forms.CheckBox();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbPaidBySrch = new AshUsercontrol.AshComboBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.nlblCashABalance = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDiscount = new System.Windows.Forms.CheckBox();
            this.txtDis = new AshUsercontrol.AshTextBoxNormal();
            this.lblEndBalance = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.cmbAcct = new AshUsercontrol.AshComboBox();
            this.lblStartingBalance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAmountInWords = new System.Windows.Forms.Label();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.lblCashAccnt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOppAcct = new AshUsercontrol.AshComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAmount = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.cmbPaidBy = new AshUsercontrol.AshComboBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.toolTip.SetToolTip(this.button4, resources.GetString("button4.ToolTip"));
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblCashAcctBal
            // 
            resources.ApplyResources(this.lblCashAcctBal, "lblCashAcctBal");
            this.lblCashAcctBal.ForeColor = System.Drawing.Color.Red;
            this.lblCashAcctBal.Name = "lblCashAcctBal";
            this.toolTip.SetToolTip(this.lblCashAcctBal, resources.GetString("lblCashAcctBal.ToolTip"));
            // 
            // cbRefresh
            // 
            resources.ApplyResources(this.cbRefresh, "cbRefresh");
            this.cbRefresh.Checked = true;
            this.cbRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRefresh.Name = "cbRefresh";
            this.toolTip.SetToolTip(this.cbRefresh, resources.GetString("cbRefresh.ToolTip"));
            this.cbRefresh.UseVisualStyleBackColor = true;
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column10});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip.SetToolTip(this.dgDetails, resources.GetString("dgDetails.ToolTip"));
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SL";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "VchrNo";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "VchrDate";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FromAccount";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ToAccount";
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "VchrAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Narration";
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "SalesMan";
            resources.ApplyResources(this.Column10, "Column10");
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // cmbPaidBySrch
            // 
            resources.ApplyResources(this.cmbPaidBySrch, "cmbPaidBySrch");
            this.cmbPaidBySrch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPaidBySrch.FormattingEnabled = true;
            this.cmbPaidBySrch.LeaveColor = System.Drawing.Color.White;
            this.cmbPaidBySrch.Name = "cmbPaidBySrch";
            this.toolTip.SetToolTip(this.cmbPaidBySrch, resources.GetString("cmbPaidBySrch.ToolTip"));
            this.cmbPaidBySrch.SelectedIndexChanged += new System.EventHandler(this.cmbPaidBySrch_SelectedIndexChanged);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Name = "btnEdit";
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.toolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.toolTip.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // grpSearch
            // 
            resources.ApplyResources(this.grpSearch, "grpSearch");
            this.grpSearch.Controls.Add(this.cbRefresh);
            this.grpSearch.Controls.Add(this.lblCashAcctBal);
            this.grpSearch.Controls.Add(this.dgDetails);
            this.grpSearch.Controls.Add(this.nlblCashABalance);
            this.grpSearch.Controls.Add(this.lblTotal);
            this.grpSearch.Controls.Add(this.cmbPaidBySrch);
            this.grpSearch.Controls.Add(this.label10);
            this.grpSearch.Controls.Add(this.label2);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.TabStop = false;
            this.toolTip.SetToolTip(this.grpSearch, resources.GetString("grpSearch.ToolTip"));
            // 
            // nlblCashABalance
            // 
            resources.ApplyResources(this.nlblCashABalance, "nlblCashABalance");
            this.nlblCashABalance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.nlblCashABalance.Name = "nlblCashABalance";
            this.toolTip.SetToolTip(this.nlblCashABalance, resources.GetString("nlblCashABalance.ToolTip"));
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Name = "lblTotal";
            this.toolTip.SetToolTip(this.lblTotal, resources.GetString("lblTotal.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // cbDiscount
            // 
            resources.ApplyResources(this.cbDiscount, "cbDiscount");
            this.cbDiscount.Name = "cbDiscount";
            this.toolTip.SetToolTip(this.cbDiscount, resources.GetString("cbDiscount.ToolTip"));
            this.cbDiscount.UseVisualStyleBackColor = true;
            this.cbDiscount.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // txtDis
            // 
            resources.ApplyResources(this.txtDis, "txtDis");
            this.txtDis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDis.Data = 0;
            this.txtDis.EnterColor = System.Drawing.Color.LightPink;
            this.txtDis.Format = null;
            this.txtDis.isAllowNegative = false;
            this.txtDis.isAllowSpecialChar = false;
            this.txtDis.isNumberOnly = false;
            this.txtDis.isNumeric = true;
            this.txtDis.LeaveColor = System.Drawing.Color.White;
            this.txtDis.Name = "txtDis";
            this.toolTip.SetToolTip(this.txtDis, resources.GetString("txtDis.ToolTip"));
            this.txtDis.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblEndBalance
            // 
            resources.ApplyResources(this.lblEndBalance, "lblEndBalance");
            this.lblEndBalance.ForeColor = System.Drawing.Color.Red;
            this.lblEndBalance.Name = "lblEndBalance";
            this.toolTip.SetToolTip(this.lblEndBalance, resources.GetString("lblEndBalance.ToolTip"));
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
            // 
            // cmbAcct
            // 
            resources.ApplyResources(this.cmbAcct, "cmbAcct");
            this.cmbAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAcct.FormattingEnabled = true;
            this.cmbAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbAcct.Name = "cmbAcct";
            this.toolTip.SetToolTip(this.cmbAcct, resources.GetString("cmbAcct.ToolTip"));
            this.cmbAcct.SelectedIndexChanged += new System.EventHandler(this.cmbAcct_SelectedIndexChanged);
            // 
            // lblStartingBalance
            // 
            resources.ApplyResources(this.lblStartingBalance, "lblStartingBalance");
            this.lblStartingBalance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblStartingBalance.Name = "lblStartingBalance";
            this.toolTip.SetToolTip(this.lblStartingBalance, resources.GetString("lblStartingBalance.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // lblAmountInWords
            // 
            resources.ApplyResources(this.lblAmountInWords, "lblAmountInWords");
            this.lblAmountInWords.ForeColor = System.Drawing.Color.Red;
            this.lblAmountInWords.Name = "lblAmountInWords";
            this.toolTip.SetToolTip(this.lblAmountInWords, resources.GetString("lblAmountInWords.ToolTip"));
            // 
            // nudVoucherNo
            // 
            resources.ApplyResources(this.nudVoucherNo, "nudVoucherNo");
            this.nudVoucherNo.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudVoucherNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVoucherNo.Name = "nudVoucherNo";
            this.toolTip.SetToolTip(this.nudVoucherNo, resources.GetString("nudVoucherNo.ToolTip"));
            this.nudVoucherNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVoucherNo.ValueChanged += new System.EventHandler(this.nudVoucherNo_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            this.dtmDate.ValueChanged += new System.EventHandler(this.dtmDate_ValueChanged);
            // 
            // lblCashAccnt
            // 
            resources.ApplyResources(this.lblCashAccnt, "lblCashAccnt");
            this.lblCashAccnt.Name = "lblCashAccnt";
            this.toolTip.SetToolTip(this.lblCashAccnt, resources.GetString("lblCashAccnt.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbOppAcct
            // 
            resources.ApplyResources(this.cmbOppAcct, "cmbOppAcct");
            this.cmbOppAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOppAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOppAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbOppAcct.FormattingEnabled = true;
            this.cmbOppAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbOppAcct.Name = "cmbOppAcct";
            this.toolTip.SetToolTip(this.cmbOppAcct, resources.GetString("cmbOppAcct.ToolTip"));
            this.cmbOppAcct.SelectedIndexChanged += new System.EventHandler(this.cmbOppAcct_SelectedIndexChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Data = 0;
            this.txtAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtAmount.Format = null;
            this.txtAmount.isAllowNegative = false;
            this.txtAmount.isAllowSpecialChar = false;
            this.txtAmount.isNumberOnly = false;
            this.txtAmount.isNumeric = true;
            this.txtAmount.LeaveColor = System.Drawing.Color.White;
            this.txtAmount.Name = "txtAmount";
            this.toolTip.SetToolTip(this.txtAmount, resources.GetString("txtAmount.ToolTip"));
            this.txtAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNarration.Data = 0;
            this.txtNarration.EnterColor = System.Drawing.Color.LightPink;
            this.txtNarration.Format = null;
            this.txtNarration.isAllowNegative = false;
            this.txtNarration.isAllowSpecialChar = false;
            this.txtNarration.isNumberOnly = false;
            this.txtNarration.isNumeric = false;
            this.txtNarration.LeaveColor = System.Drawing.Color.White;
            this.txtNarration.Name = "txtNarration";
            this.toolTip.SetToolTip(this.txtNarration, resources.GetString("txtNarration.ToolTip"));
            this.txtNarration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // cmbPaidBy
            // 
            resources.ApplyResources(this.cmbPaidBy, "cmbPaidBy");
            this.cmbPaidBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPaidBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPaidBy.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPaidBy.FormattingEnabled = true;
            this.cmbPaidBy.LeaveColor = System.Drawing.Color.White;
            this.cmbPaidBy.Name = "cmbPaidBy";
            this.toolTip.SetToolTip(this.cmbPaidBy, resources.GetString("cmbPaidBy.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // PaymentView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.cbDiscount);
            this.Controls.Add(this.txtDis);
            this.Controls.Add(this.lblEndBalance);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.cmbAcct);
            this.Controls.Add(this.lblStartingBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAmountInWords);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.lblCashAccnt);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbOppAcct);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.cmbPaidBy);
            this.Controls.Add(this.label9);
            this.Name = "PaymentView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.PaymentView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaymentView_KeyDown);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.cmbPaidBy, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.txtAmount, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbOppAcct, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.lblCashAccnt, 0);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.lblAmountInWords, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblStartingBalance, 0);
            this.Controls.SetChildIndex(this.cmbAcct, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.lblEndBalance, 0);
            this.Controls.SetChildIndex(this.txtDis, 0);
            this.Controls.SetChildIndex(this.cbDiscount, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
