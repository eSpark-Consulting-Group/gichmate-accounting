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
	public class JournalEntryView : BaseForm
	{
		private DateTime InvoiceDate;

		private List<VchrTransaction> entVTransactions;

		private int? iVchrTranId;

		private DBContext db;

		private decimal dmlClosingBalance;

		private VchrTranLog vchrTranLog;

		private decimal dmlPaidAmount;

		private bool isEditing = false;

		private bool blCredit = false;

		private string VoucherType = "Journal Voucher";

		private IContainer components = null;

		private Label lblHead;

		private GroupBox grpSearch;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private Button btnTo;

		private Button btnFrom;

		private Button btnDate;

		private Label lblAfter;

		private Label lblBefore;

		private Button button4;

		private Label lblAfterTo;

		private Label lblBeforeTo;

		private Label lblAfterFrom;

		private Label lblBeforeFrom;

		private AshTextBoxNormal txtNarration;

		private Label label8;

		private AshTextBoxNormal txtAmount;

		private Label label7;

		private AshComboBox cmbToAccnt;

		private Label label6;

		private AshComboBox cmbFromAccnt;

		private Label label5;

		private AshDateTimePicker dtmDate;

		private Label label4;

		private AshUpDown nudVoucherNo;

		private Label label3;

		private DataGridView dgDetails;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private RadioButton rbCustomer;

		private RadioButton rbSupplier;

		private RadioButton rbOthers;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private RadioButton rbAll;

		public int? iVoucherNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public JournalEntryView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
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

		private void DGVLoadMethod(string Query)
		{
			dgDetails.AutoGenerateColumns = false;
			entVTransactions = new List<VchrTransaction>();
			entVTransactions = POSHelper.GetVTransactionsFromVDetails(AshproCommon.GetListMethod<VchrTranLog>(Query), db);
			dgDetails.DataSource = entVTransactions;
		}

		private void OppenAccountViewMethod(ComboBox cmb)
		{
			LedgerReport ledgerReport = new LedgerReport();
			ledgerReport.MdiParent = base.MdiParent;
			ledgerReport.ValueFromDate = AniHelper.GetFYearFromDate().ToString();
			ledgerReport.ValueToDate = dtmDate.Text;
			ledgerReport.ValueLedger = cmb.Text;
			ledgerReport.Show();
		}

		private void LabelsVisibleMethod(bool isToVisible)
		{
			lblBeforeFrom.Visible = isToVisible;
			btnFrom.Visible = isToVisible;
			lblAfterFrom.Visible = isToVisible;
			lblAfterTo.Visible = isToVisible;
			btnTo.Visible = isToVisible;
			lblBeforeTo.Visible = isToVisible;
			lblBefore.Visible = isToVisible;
			lblAfter.Visible = isToVisible;
		}

		private void DeleteSubMethod()
		{
			DataTable dataTable = new DataTable();
			string[] obj = new string[5] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null };
			int? num = iVchrTranId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType='";
			obj[3] = VoucherType;
			obj[4] = "'";
			dataTable = AshproCommon.GetDataTable(string.Concat(obj));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo)
		{
			vchrTranLog.VchrDate = dtmDate.Value;
			vchrTranLog.VchrTranId = iVchrTranId;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = txtAmount.Value;
			vchrTranLog.VchrType = VoucherType;
			vchrTranLog.VchrName = VoucherType;
			vchrTranLog.SalesManId = Settings.Default.intLogOnId;
			vchrTranLog.FromAccountId = cmbFromAccnt.SelectedValue.ToInt32();
			vchrTranLog.ToAccountId = cmbToAccnt.SelectedValue.ToInt32();
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = txtNarration.Text;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
		}

		private void LoadSingleObjectMethod()
		{
			LabelsVisibleMethod(isToVisible: true);
			isEditing = true;
			iVchrTranId = vchrTranLog.VchrTranId;
			dtmDate.Value = vchrTranLog.VchrDate;
			dmlPaidAmount = vchrTranLog.VchrAmount;
			txtAmount.Value = dmlPaidAmount;
			cmbToAccnt.SelectedValue = vchrTranLog.ToAccountId.GetValueOrDefault();
			cmbFromAccnt.SelectedValue = vchrTranLog.FromAccountId.GetValueOrDefault();
			txtNarration.Text = vchrTranLog.Narration;
			AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbFromAccnt, lblBeforeFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbToAccnt, lblBeforeTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			EnableMethod();
		}

		private void btnClearMethod()
		{
			DGVLoadMethod("Select * From VchrTranLog Where VchrType='" + VoucherType + "' and VchrDate='" + AniHelper.GetDate(dtmDate.Value.Date) + "'");
			dmlClosingBalance = (dmlPaidAmount = 0m);
			btnSave.Text = "Save";
			dtmDate.Value = InvoiceDate;
			LabelsVisibleMethod(isToVisible: false);
			isEditing = false;
			DisableMethod();
			AniHelper.ClearMethod(this);
		}

		private void JournalEntryView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			InvoiceDate = Settings.Default.appDate;
			dtmDate.Value = InvoiceDate;
			AniHelper.FillAllAccount(cmbFromAccnt);
			AniHelper.FillAllAccount(cmbToAccnt);
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == VoucherType
				select x).Sum((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			DGVLoadMethod("Select * From VchrTranLog Where VchrType='" + VoucherType + "' and VchrDate='" + AniHelper.GetDate(dtmDate.Value.Date) + "'");
			LabelsVisibleMethod(isToVisible: false);
			if (iVoucherNo.HasValue)
			{
				nudVoucherNo.Value = iVoucherNo.ToInt32();
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			InvoiceDate = dtmDate.Value;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == VoucherType
				select x).Sum((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			btnClearMethod();
		}

		private void btnFrom_Click(object sender, EventArgs e)
		{
			OppenAccountViewMethod(cmbFromAccnt);
		}

		private void btnTo_Click(object sender, EventArgs e)
		{
			OppenAccountViewMethod(cmbFromAccnt);
		}

		private void txtAmount_Validated(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			if (isEditing)
			{
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbFromAccnt, lblBeforeFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbToAccnt, lblBeforeTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				if (blCredit)
				{
					AniHelper.AccountBalanceFindMethod(txtAmount.Value, dmlPaidAmount, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
					AniHelper.AccountBalanceFindMethod(dmlPaidAmount, txtAmount.Value, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				}
				else
				{
					AniHelper.AccountBalanceFindMethod(dmlPaidAmount, txtAmount.Value, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
					AniHelper.AccountBalanceFindMethod(txtAmount.Value, dmlPaidAmount, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				}
			}
			else
			{
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbFromAccnt, lblBeforeFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbToAccnt, lblBeforeTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				if (blCredit)
				{
					AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
					AniHelper.AccountBalanceFindMethod(0m, txtAmount.Value, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				}
				else
				{
					AniHelper.AccountBalanceFindMethod(0m, txtAmount.Value, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
					AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				}
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
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DisableMethod();
			btnSave.Text = "Update";
			dtmDate.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtAmount.Value <= 0m)
			{
				MessageBox.Show("The amount must be greater than zero", Utility.uManufacturer);
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
				int iVchrNo = (db.VchrTransaction(new
				{
					VchrType = VoucherType,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo);
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
						select x).FirstOrDefault().VchrTranId.ToInt32();
					if (blCredit)
					{
						POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
						POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					}
					else
					{
						POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
						POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					}
					btnClear_Click(null, null);
				}
				return;
			}
			if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				return;
			}
			DeleteSubMethod();
			EntityLoadMethod(vchrTranLog, nudVoucherNo.Value.ToInt32());
			if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iVchrTranId.ToInt32()))
			{
				if (blCredit)
				{
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
				}
				else
				{
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
				}
				btnClear_Click(null, null);
			}
		}

		private void cmbFromAccnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(0m, 0m, cmbFromAccnt, lblBeforeFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbFromAccnt, lblAfterFrom, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			if (dmlClosingBalance < 0m)
			{
				blCredit = true;
			}
			else
			{
				blCredit = false;
			}
		}

		private void cmbToAccnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(0m, 0m, cmbToAccnt, lblBeforeTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbToAccnt, lblAfterTo, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
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

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			vchrTranLog = (from x in db.VchrTransaction()
				where x.VchrType == VoucherType && x.VchrNo == nudVoucherNo.Value.ToInt32()
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

		private void txtNarration_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void JournalEntryView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.F && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnFrom_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnTo_Click(null, null);
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

		private void rbOthers_CheckedChanged(object sender, EventArgs e)
		{
			AniHelper.FillNotPartyAndSupplier(cmbFromAccnt);
			AniHelper.FillNotPartyAndSupplier(cmbToAccnt);
		}

		private void rbAll_CheckedChanged(object sender, EventArgs e)
		{
			AniHelper.FillAllAccount(cmbFromAccnt);
			AniHelper.FillAllAccount(cmbToAccnt);
		}

		private void rbSupplier_CheckedChanged(object sender, EventArgs e)
		{
			AniHelper.FillAccountForPurchase(cmbFromAccnt, isCashAccntIncluded: false);
			AniHelper.FillAccountForPurchase(cmbToAccnt, isCashAccntIncluded: false);
			if (cmbFromAccnt.Items.Count > 0)
			{
				cmbFromAccnt.SelectedIndex = 0;
			}
			if (cmbToAccnt.Items.Count > 0)
			{
				cmbToAccnt.SelectedIndex = 0;
			}
		}

		private void rbCustomer_CheckedChanged(object sender, EventArgs e)
		{
			AniHelper.FillAccount(cmbFromAccnt, isCashAccntIncluded: false);
			AniHelper.FillAccount(cmbToAccnt, isCashAccntIncluded: false);
			if (cmbFromAccnt.Items.Count > 0)
			{
				cmbFromAccnt.SelectedIndex = 0;
			}
			if (cmbToAccnt.Items.Count > 0)
			{
				cmbToAccnt.SelectedIndex = 0;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalEntryView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHead = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTo = new System.Windows.Forms.Button();
            this.btnFrom = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblBefore = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.lblAfterTo = new System.Windows.Forms.Label();
            this.lblBeforeTo = new System.Windows.Forms.Label();
            this.lblAfterFrom = new System.Windows.Forms.Label();
            this.lblBeforeFrom = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.txtAmount = new AshUsercontrol.AshTextBoxNormal();
            this.cmbToAccnt = new AshUsercontrol.AshComboBox();
            this.cmbFromAccnt = new AshUsercontrol.AshComboBox();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.rbCustomer = new System.Windows.Forms.RadioButton();
            this.rbSupplier = new System.Windows.Forms.RadioButton();
            this.rbOthers = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
            // 
            // grpSearch
            // 
            resources.ApplyResources(this.grpSearch, "grpSearch");
            this.grpSearch.Controls.Add(this.dgDetails);
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
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
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
            // btnTo
            // 
            resources.ApplyResources(this.btnTo, "btnTo");
            this.btnTo.Name = "btnTo";
            this.toolTip.SetToolTip(this.btnTo, resources.GetString("btnTo.ToolTip"));
            this.btnTo.UseVisualStyleBackColor = true;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnFrom
            // 
            resources.ApplyResources(this.btnFrom, "btnFrom");
            this.btnFrom.Name = "btnFrom";
            this.toolTip.SetToolTip(this.btnFrom, resources.GetString("btnFrom.ToolTip"));
            this.btnFrom.UseVisualStyleBackColor = true;
            this.btnFrom.Click += new System.EventHandler(this.btnFrom_Click);
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.toolTip.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // lblAfter
            // 
            resources.ApplyResources(this.lblAfter, "lblAfter");
            this.lblAfter.Name = "lblAfter";
            this.toolTip.SetToolTip(this.lblAfter, resources.GetString("lblAfter.ToolTip"));
            // 
            // lblBefore
            // 
            resources.ApplyResources(this.lblBefore, "lblBefore");
            this.lblBefore.Name = "lblBefore";
            this.toolTip.SetToolTip(this.lblBefore, resources.GetString("lblBefore.ToolTip"));
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.toolTip.SetToolTip(this.button4, resources.GetString("button4.ToolTip"));
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblAfterTo
            // 
            resources.ApplyResources(this.lblAfterTo, "lblAfterTo");
            this.lblAfterTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAfterTo.ForeColor = System.Drawing.Color.Red;
            this.lblAfterTo.Name = "lblAfterTo";
            this.toolTip.SetToolTip(this.lblAfterTo, resources.GetString("lblAfterTo.ToolTip"));
            // 
            // lblBeforeTo
            // 
            resources.ApplyResources(this.lblBeforeTo, "lblBeforeTo");
            this.lblBeforeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBeforeTo.ForeColor = System.Drawing.Color.Red;
            this.lblBeforeTo.Name = "lblBeforeTo";
            this.toolTip.SetToolTip(this.lblBeforeTo, resources.GetString("lblBeforeTo.ToolTip"));
            // 
            // lblAfterFrom
            // 
            resources.ApplyResources(this.lblAfterFrom, "lblAfterFrom");
            this.lblAfterFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAfterFrom.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblAfterFrom.Name = "lblAfterFrom";
            this.toolTip.SetToolTip(this.lblAfterFrom, resources.GetString("lblAfterFrom.ToolTip"));
            // 
            // lblBeforeFrom
            // 
            resources.ApplyResources(this.lblBeforeFrom, "lblBeforeFrom");
            this.lblBeforeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBeforeFrom.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblBeforeFrom.Name = "lblBeforeFrom";
            this.toolTip.SetToolTip(this.lblBeforeFrom, resources.GetString("lblBeforeFrom.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            0});
            this.txtAmount.Validated += new System.EventHandler(this.txtAmount_Validated);
            // 
            // cmbToAccnt
            // 
            resources.ApplyResources(this.cmbToAccnt, "cmbToAccnt");
            this.cmbToAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbToAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbToAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbToAccnt.FormattingEnabled = true;
            this.cmbToAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbToAccnt.Name = "cmbToAccnt";
            this.toolTip.SetToolTip(this.cmbToAccnt, resources.GetString("cmbToAccnt.ToolTip"));
            this.cmbToAccnt.SelectedIndexChanged += new System.EventHandler(this.cmbToAccnt_SelectedIndexChanged);
            // 
            // cmbFromAccnt
            // 
            resources.ApplyResources(this.cmbFromAccnt, "cmbFromAccnt");
            this.cmbFromAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFromAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFromAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbFromAccnt.FormattingEnabled = true;
            this.cmbFromAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbFromAccnt.Name = "cmbFromAccnt";
            this.toolTip.SetToolTip(this.cmbFromAccnt, resources.GetString("cmbFromAccnt.ToolTip"));
            this.cmbFromAccnt.SelectedIndexChanged += new System.EventHandler(this.cmbFromAccnt_SelectedIndexChanged);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // nudVoucherNo
            // 
            resources.ApplyResources(this.nudVoucherNo, "nudVoucherNo");
            this.nudVoucherNo.Maximum = new decimal(new int[] {
            276447232,
            23283,
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
            // rbCustomer
            // 
            resources.ApplyResources(this.rbCustomer, "rbCustomer");
            this.rbCustomer.Name = "rbCustomer";
            this.toolTip.SetToolTip(this.rbCustomer, resources.GetString("rbCustomer.ToolTip"));
            this.rbCustomer.UseVisualStyleBackColor = true;
            this.rbCustomer.CheckedChanged += new System.EventHandler(this.rbCustomer_CheckedChanged);
            // 
            // rbSupplier
            // 
            resources.ApplyResources(this.rbSupplier, "rbSupplier");
            this.rbSupplier.Name = "rbSupplier";
            this.toolTip.SetToolTip(this.rbSupplier, resources.GetString("rbSupplier.ToolTip"));
            this.rbSupplier.UseVisualStyleBackColor = true;
            this.rbSupplier.CheckedChanged += new System.EventHandler(this.rbSupplier_CheckedChanged);
            // 
            // rbOthers
            // 
            resources.ApplyResources(this.rbOthers, "rbOthers");
            this.rbOthers.Name = "rbOthers";
            this.toolTip.SetToolTip(this.rbOthers, resources.GetString("rbOthers.ToolTip"));
            this.rbOthers.UseVisualStyleBackColor = true;
            this.rbOthers.CheckedChanged += new System.EventHandler(this.rbOthers_CheckedChanged);
            // 
            // rbAll
            // 
            resources.ApplyResources(this.rbAll, "rbAll");
            this.rbAll.Checked = true;
            this.rbAll.Name = "rbAll";
            this.rbAll.TabStop = true;
            this.toolTip.SetToolTip(this.rbAll, resources.GetString("rbAll.ToolTip"));
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SL";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VchrNo";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "VchrDate";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "FromAccount";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ToAccount";
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "VchrAmount";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Narration";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // JournalEntryView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.rbOthers);
            this.Controls.Add(this.rbSupplier);
            this.Controls.Add(this.rbCustomer);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.lblAfter);
            this.Controls.Add(this.lblBefore);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblAfterTo);
            this.Controls.Add(this.lblBeforeTo);
            this.Controls.Add(this.lblAfterFrom);
            this.Controls.Add(this.lblBeforeFrom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbToAccnt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFromAccnt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.btnFrom);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.label3);
            this.Name = "JournalEntryView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.JournalEntryView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JournalEntryView_KeyDown);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.btnFrom, 0);
            this.Controls.SetChildIndex(this.btnTo, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbFromAccnt, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbToAccnt, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtAmount, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.lblBeforeFrom, 0);
            this.Controls.SetChildIndex(this.lblAfterFrom, 0);
            this.Controls.SetChildIndex(this.lblBeforeTo, 0);
            this.Controls.SetChildIndex(this.lblAfterTo, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.lblBefore, 0);
            this.Controls.SetChildIndex(this.lblAfter, 0);
            this.Controls.SetChildIndex(this.grpSearch, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.rbCustomer, 0);
            this.Controls.SetChildIndex(this.rbSupplier, 0);
            this.Controls.SetChildIndex(this.rbOthers, 0);
            this.Controls.SetChildIndex(this.rbAll, 0);
            this.grpSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
