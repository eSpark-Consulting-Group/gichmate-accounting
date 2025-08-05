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
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class ChequeRecipt : BaseForm
	{
		private string strTitleHead;

		private string sVoucherName;

		private string sVoucherType;

		private int? iVchrTranId;

		private int? iChequeDtlsId;

		private DBContext db;

		private List<ChequeDetails> entChequeDetails;

		private ChequeDetails chequeDetails;

		private VchrTranLog vchrTranLog;

		private IContainer components = null;

		private GroupBox grpSearch;

		private AshTextBoxNormal txtChqNoSrch;

		private Label label2;

		private Label label1;

		private Label label5;

		private Label label6;

		private AshDateTimePicker dtmDate;

		private Label label7;

		private AshComboBox cmbBankAcct;

		private Label label8;

		private AshComboBox cmbPartyAcct;

		private Label label9;

		private AshTextBoxNormal txtAmnt;

		private AshTextBoxNormal txtChqNo;

		private Label label10;

		private AshTextBoxNormal txtBank;

		private Label label11;

		private AshTextBoxNormal txtBranch;

		private Label label12;

		private AshDateTimePicker dtmChqDate;

		private AshTextBoxNormal txtNarration;

		private Label label15;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private AshComboBox cmbPartySrch;

		private AshUpDown nudVoucherNo;

		private Button btnExport;

		private ToolTip toolTip;

		private AshDateTimePicker dtmTo;

		private Label label14;

		private Label label4;

		private AshComboBox cmbStatus;

		private Label label16;

		private DataGridView dgDetails;

		private Label label13;

		private Label lblCollDate;

		private AshDateTimePicker dtmCollDate;

		private AshDateTimePicker dtmFrom;

		private Label lblChqRtnAmnt;

		private AshTextBoxNormal txtChqRtnAmnt;

		private Button button4;

		private Label lblHead;

		private Button btnSave;

		private Label label3;

		private AshComboBox cmbExi;

		private Label label17;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column13;
        private AshTextBoxNormal txtRefNo;

		public string ReturnValue { get; set; }

		public string PassingValue { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public ChequeRecipt(string strTitle)
		{
			strTitleHead = strTitle;
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void DGVLoadMethod(string Query)
		{
			entChequeDetails = new List<ChequeDetails>();
			entChequeDetails = AshproCommon.GetListMethod<ChequeDetails>(Query);
			if (entChequeDetails != null)
			{
				dgDetails.AutoGenerateColumns = false;
				LoadChequeDetailsEntity(entChequeDetails);
				dgDetails.DataSource = entChequeDetails;
			}
		}

		private void LoadChequeDetailsEntity(List<ChequeDetails> chequeDetails)
		{
			int iRowCount = 1;
			chequeDetails.ForEach(delegate(ChequeDetails x)
			{
				x.SL = iRowCount;
				x.VchrNo = AshproCommon.IdFindMethod("Select VchrNo From VchrTranLog Where VchrTranId='" + x.VchrId + "' and VchrType='" + sVoucherType + "' and VchrName='" + sVoucherName + "'");
				x.BankAccount = AshproCommon.IdFindMethod("Select AccountName From Account Where AccountId='" + x.BankAcctId + "'");
				x.PartyAccount = AshproCommon.IdFindMethod("Select AccountName From Account Where AccountId='" + x.AccountId + "'");
				iRowCount++;
			});
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
			obj[3] = sVoucherType;
			obj[4] = "'and VchrName='";
			obj[5] = sVoucherName;
			obj[6] = "'";
			dataTable = AshproCommon.GetDataTable(string.Concat(obj));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
			AshproCommon.DeleteFromDatabase("ChequeDetails", "CheqDtlsId", iChequeDtlsId.ToInt32());
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo, decimal dmlAmnt, string sVchrType, string sVchrName, int iFromAcctId, int iToAcctId, string sNarration)
		{
			vchrTranLog.VchrTranId = iVchrTranId;
			vchrTranLog.VchrDate = dtmDate.Value.Date;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = dmlAmnt;
			vchrTranLog.VchrType = sVchrType;
			vchrTranLog.VchrName = sVchrName;
			vchrTranLog.SalesManId = cmbExi.SelectedValue.ToInt32();
			vchrTranLog.FromAccountId = iFromAcctId;
			vchrTranLog.ToAccountId = iToAcctId;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = sNarration;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
		}

		private void ChequeDetailsLoadMethod()
		{
			chequeDetails.CheqDtlsId = iChequeDtlsId;
			chequeDetails.SL = null;
			chequeDetails.VchrNo = null;
			chequeDetails.BankAccount = null;
			chequeDetails.PartyAccount = null;
			chequeDetails.VchrId = iVchrTranId.ToInt32();
			chequeDetails.VchrType = sVoucherType;
			chequeDetails.AccountId = cmbPartyAcct.SelectedValue.ToInt32();
			chequeDetails.BankAcctId = cmbBankAcct.SelectedValue.ToInt32();
			chequeDetails.Bank = txtBank.Text;
			chequeDetails.Branch = txtBranch.Text;
			chequeDetails.CheqNo = txtChqNo.Text;
			chequeDetails.CheqDate = AniHelper.GetDate(dtmChqDate.Value.Date);
			chequeDetails.Amount = txtAmnt.Value;
			chequeDetails.Status = cmbStatus.Text;
			chequeDetails.CollectedDate = dtmCollDate.Value.Date;
			chequeDetails.RefNo = txtRefNo.Text;
			chequeDetails.Remarks = txtNarration.Text;
			chequeDetails.TranDate = DateTime.Now.Date;
			chequeDetails.JeId = Settings.Default.intLogOnId;
			chequeDetails.Type = sVoucherName;
			chequeDetails.Date = dtmDate.Value.Date;
			chequeDetails.ChqRtnAmount = txtChqRtnAmnt.Value;
		}

		private void SaveSubMethod()
		{
			ChequeDetailsLoadMethod();
			chequeDetails.CheqDtlsId = null;
			if (AshproCommon.InsertToDatabaseObj(chequeDetails, "ChequeDetails"))
			{
				if (strTitleHead == "Receipt")
				{
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					int iFromAcctId = AshproCommon.IdFindMethod("Select AccountId From Account  Where AccountName='CHEQUE RETURN INCOME'");
					int iVchrNo = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Debit Note'", isVoucherNo: true);
					vchrTranLog = new VchrTranLog();
					EntityLoadMethod(vchrTranLog, iVchrNo, txtChqRtnAmnt.Value, "Debit Note", "Debit Note", iFromAcctId, cmbPartyAcct.SelectedValue.ToInt32(), "Discount Received From Payment Voucher");
					vchrTranLog.VchrTranId = null;
					if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
					{
						iVchrTranId = (from x in db.VchrTransaction()
							where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.FK_BranchId == Settings.Default.iBranchId
							select x).FirstOrDefault().VchrTranId.ToInt32();
						POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
						POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					}
				}
				else
				{
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					int iToAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName ='CHEQUE RETURN EXPENSES'");
					int iVchrNo2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Credit Note'", isVoucherNo: true);
					vchrTranLog = new VchrTranLog();
					EntityLoadMethod(vchrTranLog, iVchrNo2, txtChqRtnAmnt.Value, "Credit Note", "Credit Note", cmbPartyAcct.SelectedValue.ToInt32(), iToAcctId, "Discount Paid From Payment Voucher");
					vchrTranLog.VchrTranId = null;
					if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
					{
						iVchrTranId = (from x in db.VchrTransaction()
							where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.FK_BranchId == Settings.Default.iBranchId
							select x).FirstOrDefault().VchrTranId.ToInt32();
						POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
						POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
					}
				}
			}
			btnClear_Click(null, null);
		}

		private void LoadDataFromDB(ChequeDetails cDetails)
		{
			iChequeDtlsId = cDetails.CheqDtlsId;
			dtmDate.Value = cDetails.Date;
			cmbPartyAcct.SelectedValue = cDetails.AccountId.GetValueOrDefault();
			cmbBankAcct.SelectedValue = cDetails.BankAcctId.GetValueOrDefault();
			txtAmnt.Value = cDetails.Amount;
			txtNarration.Text = cDetails.Remarks;
			txtBank.Text = cDetails.Bank;
			txtBranch.Text = cDetails.Branch;
			txtChqNo.Text = cDetails.CheqNo;
			dtmChqDate.Value = cDetails.CheqDate.toDateTime();
			txtRefNo.Text = cDetails.RefNo;
			cmbStatus.Text = cDetails.Status;
			dtmCollDate.Value = cDetails.CollectedDate;
			txtChqRtnAmnt.Value = cDetails.ChqRtnAmount;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			dtmDate.Value = DateTime.Now.Date;
			DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
			txtChqRtnAmnt.Visible = false;
			lblChqRtnAmnt.Visible = false;
			lblCollDate.Visible = false;
			dtmCollDate.Visible = false;
			btnEdit.Text = "Save";
			DisableMethod();
			cmbPartyAcct.Focus();
		}

		private void ChequeRecipt_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			lblHead.Text = "Post Dated Cheque " + strTitleHead;
			sVoucherName = "Cheque " + strTitleHead;
			sVoucherType = strTitleHead + " Voucher";
			AniHelper.FillPartyAndSupplier(cmbPartySrch);
			AniHelper.FillPartyAndSupplier(cmbPartyAcct);
			AniHelper.FillBankAccount(cmbBankAcct);
			AniHelper.FillEmployees(cmbExi);
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == sVoucherType && x.VchrName == sVoucherName && x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtAmnt.Value < 1m)
			{
				MessageBox.Show("Amount must be greater than zero.", Utility.uManufacturer);
				return;
			}
			chequeDetails = new ChequeDetails();
			vchrTranLog = new VchrTranLog();
			if (btnSave.Text == "Save")
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				int iVchrNo = ((from x in db.VchrTransaction()
					where x.VchrType == sVoucherType && x.VchrName == sVoucherName && x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo, txtAmnt.Value, sVoucherType, sVoucherName, cmbPartyAcct.SelectedValue.ToInt32(), cmbBankAcct.SelectedValue.ToInt32(), txtNarration.Text);
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrTranId = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrType='" + sVoucherType + "' and VchrName='" + sVoucherName + "' and VchrNo='" + iVchrNo + "'");
					SaveSubMethod();
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else
			{
				DeleteSubMethod();
				EntityLoadMethod(vchrTranLog, nudVoucherNo.Value.ToInt32(), txtAmnt.Value, sVoucherType, sVoucherName, cmbPartyAcct.SelectedValue.ToInt32(), cmbBankAcct.SelectedValue.ToInt32(), txtNarration.Text);
				if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranId", "VchrTranId", iVchrTranId.ToInt32()))
				{
					SaveSubMethod();
				}
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == sVoucherType && x.VchrName == sVoucherName && x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			btnClearMethod();
		}

		private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbStatus.SelectedIndex == 1)
			{
				lblCollDate.Visible = true;
				dtmCollDate.Visible = true;
				txtChqRtnAmnt.Visible = false;
				lblChqRtnAmnt.Visible = false;
			}
			if (cmbStatus.SelectedIndex == 2)
			{
				lblCollDate.Visible = true;
				dtmCollDate.Visible = true;
				txtChqRtnAmnt.Visible = true;
				lblChqRtnAmnt.Visible = true;
			}
			else
			{
				txtChqRtnAmnt.Visible = false;
				lblChqRtnAmnt.Visible = false;
				lblCollDate.Visible = false;
				dtmCollDate.Visible = false;
			}
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

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (dgDetails.Rows.Count > 0)
			{
				//AniHelper.ExportToExcelMethod(dgDetails, "Post dated cheque " + strTitleHead + " Report of from - " + dtmFrom.Text + " to " + dtmTo.Text);
			}
		}

		private void cmbPartySrch_SelectedIndexChanged(object sender, EventArgs e)
		{
			DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and AccountId = '" + cmbPartySrch.SelectedValue.ToInt32() + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
		}

		private void txtChqNoSrch_TextChanged(object sender, EventArgs e)
		{
			DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and CheqNo = '" + txtChqNoSrch.Text + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
		}

		private void ChequeRecipt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				if (btnDelete.Enabled)
				{
					e.SuppressKeyPress = true;
					btnDelete_Click(null, null);
				}
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnExport_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (dgDetails.Rows.Count > 0)
				{
					dgDetails.Focus();
					dgDetails.CurrentCell = dgDetails.Rows[0].Cells[0];
				}
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void dateChanged(object sender, EventArgs e)
		{
			if (dtmTo.Value > dtmFrom.Value)
			{
				DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
			}
			else
			{
				MessageBox.Show("From date must be lessthan the To date.", Utility.uManufacturer);
			}
		}

		private void dtmDate_ValueChanged(object sender, EventArgs e)
		{
			DGVLoadMethod("Select * From ChequeDetails Where Type='" + sVoucherName + "' and Date = '" + AniHelper.GetDate(dtmDate.Value.Date) + "' and FK_BranchId='" + Settings.Default.iBranchId + "'");
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DisableMethod();
			btnSave.Text = "Update";
			dtmDate.Focus();
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				ChequeDetails chequeDetails = (ChequeDetails)dgDetails.CurrentRow.DataBoundItem;
				iVchrTranId = chequeDetails.VchrId;
				cmbExi.SelectedValue = (from x in db.VchrTransaction()
					where x.VchrType == sVoucherType && x.VchrName == sVoucherName && x.VchrTranId == iVchrTranId && x.FK_BranchId == Settings.Default.iBranchId
					select x).FirstOrDefault().SalesManId;
				nudVoucherNo.Value = chequeDetails.VchrNo.ToInt32();
				LoadDataFromDB(chequeDetails);
			}
		}

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			vchrTranLog = (from x in db.VchrTransaction()
				where x.VchrType == sVoucherType && x.VchrName == sVoucherName && x.VchrNo == nudVoucherNo.Value.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
				select x).FirstOrDefault();
			if (vchrTranLog != null)
			{
				iVchrTranId = vchrTranLog.VchrTranId;
				chequeDetails = new ChequeDetails();
				string[] obj = new string[7] { "Select * From ChequeDetails Where VchrId='", null, null, null, null, null, null };
				int? num = iVchrTranId;
				obj[1] = num.ToString();
				obj[2] = "' and VchrType='";
				obj[3] = sVoucherType;
				obj[4] = "' and Type='";
				obj[5] = sVoucherName;
				obj[6] = "'";
				chequeDetails = AshproCommon.GetObjectDetails<ChequeDetails>(string.Concat(obj));
				cmbExi.SelectedValue = vchrTranLog.SalesManId;
				LoadDataFromDB(chequeDetails);
			}
			else
			{
				btnClearMethod();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChequeRecipt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtChqNoSrch = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPartySrch = new AshUsercontrol.AshComboBox();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBankAcct = new AshUsercontrol.AshComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPartyAcct = new AshUsercontrol.AshComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.txtChqNo = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBank = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBranch = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.dtmChqDate = new AshUsercontrol.AshDateTimePicker();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmbStatus = new AshUsercontrol.AshComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCollDate = new System.Windows.Forms.Label();
            this.dtmCollDate = new AshUsercontrol.AshDateTimePicker();
            this.lblChqRtnAmnt = new System.Windows.Forms.Label();
            this.txtChqRtnAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbExi = new AshUsercontrol.AshComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRefNo = new AshUsercontrol.AshTextBoxNormal();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // grpSearch
            // 
            resources.ApplyResources(this.grpSearch, "grpSearch");
            this.grpSearch.BackColor = System.Drawing.Color.White;
            this.grpSearch.Controls.Add(this.dgDetails);
            this.grpSearch.Controls.Add(this.btnExport);
            this.grpSearch.Controls.Add(this.txtChqNoSrch);
            this.grpSearch.Controls.Add(this.label2);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Controls.Add(this.cmbPartySrch);
            this.grpSearch.Controls.Add(this.dtmTo);
            this.grpSearch.Controls.Add(this.label14);
            this.grpSearch.Controls.Add(this.label4);
            this.grpSearch.Controls.Add(this.dtmFrom);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.TabStop = false;
            this.toolTip.SetToolTip(this.grpSearch, resources.GetString("grpSearch.ToolTip"));
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
            this.Column11,
            this.Column3,
            this.Column4,
            this.Column7,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column12,
            this.Column9,
            this.Column10,
            this.Column13});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
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
            this.Column2.DataPropertyName = "Date";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "VchrNo";
            resources.ApplyResources(this.Column11, "Column11");
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "BankAccount";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "PartyAccount";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Bank";
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Branch";
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CheqNo";
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "CheqDate";
            resources.ApplyResources(this.Column12, "Column12");
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Status";
            resources.ApplyResources(this.Column9, "Column9");
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Remarks";
            resources.ApplyResources(this.Column10, "Column10");
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "CollectedDate";
            resources.ApplyResources(this.Column13, "Column13");
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Name = "btnExport";
            this.toolTip.SetToolTip(this.btnExport, resources.GetString("btnExport.ToolTip"));
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtChqNoSrch
            // 
            resources.ApplyResources(this.txtChqNoSrch, "txtChqNoSrch");
            this.txtChqNoSrch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChqNoSrch.Data = 0;
            this.txtChqNoSrch.EnterColor = System.Drawing.Color.LightPink;
            this.txtChqNoSrch.Format = null;
            this.txtChqNoSrch.isAllowNegative = false;
            this.txtChqNoSrch.isAllowSpecialChar = false;
            this.txtChqNoSrch.isNumberOnly = false;
            this.txtChqNoSrch.isNumeric = false;
            this.txtChqNoSrch.LeaveColor = System.Drawing.Color.White;
            this.txtChqNoSrch.Name = "txtChqNoSrch";
            this.toolTip.SetToolTip(this.txtChqNoSrch, resources.GetString("txtChqNoSrch.ToolTip"));
            this.txtChqNoSrch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtChqNoSrch.TextChanged += new System.EventHandler(this.txtChqNoSrch_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // cmbPartySrch
            // 
            resources.ApplyResources(this.cmbPartySrch, "cmbPartySrch");
            this.cmbPartySrch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartySrch.FormattingEnabled = true;
            this.cmbPartySrch.LeaveColor = System.Drawing.Color.White;
            this.cmbPartySrch.Name = "cmbPartySrch";
            this.toolTip.SetToolTip(this.cmbPartySrch, resources.GetString("cmbPartySrch.ToolTip"));
            this.cmbPartySrch.SelectedIndexChanged += new System.EventHandler(this.cmbPartySrch_SelectedIndexChanged);
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            this.toolTip.SetToolTip(this.dtmTo, resources.GetString("dtmTo.ToolTip"));
            this.dtmTo.ValueChanged += new System.EventHandler(this.dateChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.toolTip.SetToolTip(this.dtmFrom, resources.GetString("dtmFrom.ToolTip"));
            this.dtmFrom.ValueChanged += new System.EventHandler(this.dateChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            this.dtmDate.ValueChanged += new System.EventHandler(this.dtmDate_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // cmbBankAcct
            // 
            resources.ApplyResources(this.cmbBankAcct, "cmbBankAcct");
            this.cmbBankAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBankAcct.FormattingEnabled = true;
            this.cmbBankAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbBankAcct.Name = "cmbBankAcct";
            this.toolTip.SetToolTip(this.cmbBankAcct, resources.GetString("cmbBankAcct.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // cmbPartyAcct
            // 
            resources.ApplyResources(this.cmbPartyAcct, "cmbPartyAcct");
            this.cmbPartyAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAcct.FormattingEnabled = true;
            this.cmbPartyAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAcct.Name = "cmbPartyAcct";
            this.toolTip.SetToolTip(this.cmbPartyAcct, resources.GetString("cmbPartyAcct.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // txtAmnt
            // 
            resources.ApplyResources(this.txtAmnt, "txtAmnt");
            this.txtAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmnt.Data = 0;
            this.txtAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtAmnt.Format = null;
            this.txtAmnt.isAllowNegative = false;
            this.txtAmnt.isAllowSpecialChar = false;
            this.txtAmnt.isNumberOnly = false;
            this.txtAmnt.isNumeric = true;
            this.txtAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtAmnt.Name = "txtAmnt";
            this.toolTip.SetToolTip(this.txtAmnt, resources.GetString("txtAmnt.ToolTip"));
            this.txtAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtChqNo
            // 
            resources.ApplyResources(this.txtChqNo, "txtChqNo");
            this.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChqNo.Data = 0;
            this.txtChqNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtChqNo.Format = null;
            this.txtChqNo.isAllowNegative = false;
            this.txtChqNo.isAllowSpecialChar = false;
            this.txtChqNo.isNumberOnly = false;
            this.txtChqNo.isNumeric = false;
            this.txtChqNo.LeaveColor = System.Drawing.Color.White;
            this.txtChqNo.Name = "txtChqNo";
            this.toolTip.SetToolTip(this.txtChqNo, resources.GetString("txtChqNo.ToolTip"));
            this.txtChqNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // txtBank
            // 
            resources.ApplyResources(this.txtBank, "txtBank");
            this.txtBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBank.Data = 0;
            this.txtBank.EnterColor = System.Drawing.Color.LightPink;
            this.txtBank.Format = null;
            this.txtBank.isAllowNegative = false;
            this.txtBank.isAllowSpecialChar = false;
            this.txtBank.isNumberOnly = false;
            this.txtBank.isNumeric = false;
            this.txtBank.LeaveColor = System.Drawing.Color.White;
            this.txtBank.Name = "txtBank";
            this.toolTip.SetToolTip(this.txtBank, resources.GetString("txtBank.ToolTip"));
            this.txtBank.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // txtBranch
            // 
            resources.ApplyResources(this.txtBranch, "txtBranch");
            this.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBranch.Data = 0;
            this.txtBranch.EnterColor = System.Drawing.Color.LightPink;
            this.txtBranch.Format = null;
            this.txtBranch.isAllowNegative = false;
            this.txtBranch.isAllowSpecialChar = false;
            this.txtBranch.isNumberOnly = false;
            this.txtBranch.isNumeric = false;
            this.txtBranch.LeaveColor = System.Drawing.Color.White;
            this.txtBranch.Name = "txtBranch";
            this.toolTip.SetToolTip(this.txtBranch, resources.GetString("txtBranch.ToolTip"));
            this.txtBranch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // dtmChqDate
            // 
            resources.ApplyResources(this.dtmChqDate, "dtmChqDate");
            this.dtmChqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmChqDate.Name = "dtmChqDate";
            this.toolTip.SetToolTip(this.dtmChqDate, resources.GetString("dtmChqDate.ToolTip"));
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
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            this.toolTip.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
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
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
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
            // cmbStatus
            // 
            resources.ApplyResources(this.cmbStatus, "cmbStatus");
            this.cmbStatus.EnterColor = System.Drawing.Color.LightPink;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            resources.GetString("cmbStatus.Items"),
            resources.GetString("cmbStatus.Items1"),
            resources.GetString("cmbStatus.Items2")});
            this.cmbStatus.LeaveColor = System.Drawing.Color.White;
            this.cmbStatus.Name = "cmbStatus";
            this.toolTip.SetToolTip(this.cmbStatus, resources.GetString("cmbStatus.ToolTip"));
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // lblCollDate
            // 
            resources.ApplyResources(this.lblCollDate, "lblCollDate");
            this.lblCollDate.Name = "lblCollDate";
            this.toolTip.SetToolTip(this.lblCollDate, resources.GetString("lblCollDate.ToolTip"));
            // 
            // dtmCollDate
            // 
            resources.ApplyResources(this.dtmCollDate, "dtmCollDate");
            this.dtmCollDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmCollDate.Name = "dtmCollDate";
            this.toolTip.SetToolTip(this.dtmCollDate, resources.GetString("dtmCollDate.ToolTip"));
            // 
            // lblChqRtnAmnt
            // 
            resources.ApplyResources(this.lblChqRtnAmnt, "lblChqRtnAmnt");
            this.lblChqRtnAmnt.Name = "lblChqRtnAmnt";
            this.toolTip.SetToolTip(this.lblChqRtnAmnt, resources.GetString("lblChqRtnAmnt.ToolTip"));
            // 
            // txtChqRtnAmnt
            // 
            resources.ApplyResources(this.txtChqRtnAmnt, "txtChqRtnAmnt");
            this.txtChqRtnAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChqRtnAmnt.Data = 0;
            this.txtChqRtnAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtChqRtnAmnt.Format = null;
            this.txtChqRtnAmnt.isAllowNegative = false;
            this.txtChqRtnAmnt.isAllowSpecialChar = false;
            this.txtChqRtnAmnt.isNumberOnly = false;
            this.txtChqRtnAmnt.isNumeric = true;
            this.txtChqRtnAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtChqRtnAmnt.Name = "txtChqRtnAmnt";
            this.toolTip.SetToolTip(this.txtChqRtnAmnt, resources.GetString("txtChqRtnAmnt.ToolTip"));
            this.txtChqRtnAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.toolTip.SetToolTip(this.button4, resources.GetString("button4.ToolTip"));
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // cmbExi
            // 
            resources.ApplyResources(this.cmbExi, "cmbExi");
            this.cmbExi.EnterColor = System.Drawing.Color.LightPink;
            this.cmbExi.FormattingEnabled = true;
            this.cmbExi.LeaveColor = System.Drawing.Color.White;
            this.cmbExi.Name = "cmbExi";
            this.toolTip.SetToolTip(this.cmbExi, resources.GetString("cmbExi.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            this.toolTip.SetToolTip(this.label17, resources.GetString("label17.ToolTip"));
            // 
            // txtRefNo
            // 
            resources.ApplyResources(this.txtRefNo, "txtRefNo");
            this.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRefNo.Data = 0;
            this.txtRefNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtRefNo.Format = null;
            this.txtRefNo.isAllowNegative = false;
            this.txtRefNo.isAllowSpecialChar = false;
            this.txtRefNo.isNumberOnly = false;
            this.txtRefNo.isNumeric = false;
            this.txtRefNo.LeaveColor = System.Drawing.Color.White;
            this.txtRefNo.Name = "txtRefNo";
            this.toolTip.SetToolTip(this.txtRefNo, resources.GetString("txtRefNo.ToolTip"));
            this.txtRefNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ChequeRecipt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.cmbExi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dtmCollDate);
            this.Controls.Add(this.dtmChqDate);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblCollDate);
            this.Controls.Add(this.txtRefNo);
            this.Controls.Add(this.txtChqNo);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtChqRtnAmnt);
            this.Controls.Add(this.lblChqRtnAmnt);
            this.Controls.Add(this.txtAmnt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbPartyAcct);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBankAcct);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grpSearch);
            this.MaximizeBox = false;
            this.Name = "ChequeRecipt";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ChequeRecipt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChequeRecipt_KeyDown);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbBankAcct, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cmbPartyAcct, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtAmnt, 0);
            this.Controls.SetChildIndex(this.lblChqRtnAmnt, 0);
            this.Controls.SetChildIndex(this.txtChqRtnAmnt, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.txtChqNo, 0);
            this.Controls.SetChildIndex(this.txtRefNo, 0);
            this.Controls.SetChildIndex(this.lblCollDate, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.txtBank, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtBranch, 0);
            this.Controls.SetChildIndex(this.dtmChqDate, 0);
            this.Controls.SetChildIndex(this.dtmCollDate, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.cmbStatus, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbExi, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
