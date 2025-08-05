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
	public class CreditNoteView : BaseForm
	{
		private DateTime InvoiceDate;

		private List<VchrTransaction> entVTransactions;

		private int? iVchrTranId;

		private DBContext db;

		private decimal dmlClosingBalance;

		private VchrTranLog vchrTranLog;

		private decimal dmlPaidAmount;

		private bool isEditing = false;

		private string VoucherType = "Credit Note";

		private IContainer components = null;

		private Button button4;

		private Label lblHead;

		private Label lblAfter;

		private Label lblBefore;

		private Label lblAccntAfter;

		private Label lblAccntBefore;

		private Label lblPartyAfter;

		private Label lblPartyBefore;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private AshTextBoxNormal txtNarration;

		private Label label8;

		private AshTextBoxNormal txtAmount;

		private Label label7;

		private AshComboBox cmbAccount;

		private Label label6;

		private AshComboBox cmbPartyAccount;

		private Label label5;

		private Button btnParty;

		private Button btnDate;

		private AshUpDown nudVoucherNo;

		private Button btnAccnt;

		private PrintDocument printDocument1;

		private AshDateTimePicker dtmDate;

		private Label label4;

		private Label label3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridView dgDetails;

		public int? iVoucherNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public CreditNoteView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void LabelsVisibleMethod(bool isToVisible)
		{
			lblPartyBefore.Visible = isToVisible;
			lblPartyAfter.Visible = isToVisible;
			lblAccntBefore.Visible = isToVisible;
			lblAccntAfter.Visible = isToVisible;
			btnParty.Visible = isToVisible;
			btnAccnt.Visible = isToVisible;
			lblBefore.Visible = isToVisible;
			lblAfter.Visible = isToVisible;
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
			vchrTranLog.FromAccountId = cmbPartyAccount.SelectedValue.ToInt32();
			vchrTranLog.ToAccountId = cmbAccount.SelectedValue.ToInt32();
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = txtNarration.Text;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
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

		private void LoadSingleObjectMethod()
		{
			LabelsVisibleMethod(isToVisible: true);
			isEditing = true;
			iVchrTranId = vchrTranLog.VchrTranId;
			dtmDate.Value = vchrTranLog.VchrDate;
			dmlPaidAmount = vchrTranLog.VchrAmount;
			txtAmount.Value = dmlPaidAmount;
			cmbAccount.SelectedValue = vchrTranLog.ToAccountId.GetValueOrDefault();
			cmbPartyAccount.SelectedValue = vchrTranLog.FromAccountId.GetValueOrDefault();
			txtNarration.Text = vchrTranLog.Narration;
			AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbPartyAccount, lblPartyBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbPartyAccount, lblPartyAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbAccount, lblAccntBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbAccount, lblAccntAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			EnableMethod();
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			DGVLoadMethod("Select * From VchrTranLog Where VchrType='" + VoucherType + "' and VchrDate='" + AniHelper.GetDate(dtmDate.Value.Date) + "'");
			dmlClosingBalance = (dmlPaidAmount = 0m);
			btnSave.Text = "Save";
			dtmDate.Value = InvoiceDate;
			LabelsVisibleMethod(isToVisible: false);
			isEditing = false;
			DisableMethod();
		}

		private void CreditNoteView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			InvoiceDate = Settings.Default.appDate;
			dtmDate.Value = InvoiceDate;
			AniHelper.FillPartyAndSupplier(cmbPartyAccount);
			AniHelper.FillAccountForCreditNote(cmbAccount);
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == VoucherType
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
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
			dtmDate.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtAmount.Value <= 0m)
			{
				Messages.ErrorMessage("The amount must be greater than zero");
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
					where x.VchrType == VoucherType
					select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo);
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
						select x).FirstOrDefault().VchrTranId.ToInt32();
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					btnClear_Click(null, null);
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else
			{
				DeleteSubMethod();
				EntityLoadMethod(vchrTranLog, nudVoucherNo.Value.ToInt32());
				if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iVchrTranId.ToInt32()))
				{
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(dtmDate.Value));
					btnClear_Click(null, null);
				}
			}
		}

		private void btnParty_Click(object sender, EventArgs e)
		{
			OppenAccountViewMethod(cmbPartyAccount);
		}

		private void btnAccnt_Click(object sender, EventArgs e)
		{
			OppenAccountViewMethod(cmbAccount);
		}

		private void txtAmount_Validated(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			if (isEditing)
			{
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, 0m, cmbPartyAccount, lblPartyBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, dmlPaidAmount, cmbAccount, lblAccntBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(dmlPaidAmount, txtAmount.Value, cmbPartyAccount, lblPartyAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(txtAmount.Value, dmlPaidAmount, cmbAccount, lblAccntAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
			else
			{
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbPartyAccount, lblPartyBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, 0m, cmbAccount, lblAccntBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(0m, txtAmount.Value, cmbPartyAccount, lblPartyAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
				AniHelper.AccountBalanceFindMethod(txtAmount.Value, 0m, cmbAccount, lblAccntAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			}
		}

		private void cmbPartyAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(0m, 0m, cmbPartyAccount, lblPartyBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbPartyAccount, lblPartyAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
		}

		private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			LabelsVisibleMethod(isToVisible: true);
			dmlClosingBalance = AniHelper.AccountBalanceFindMethod(0m, 0m, cmbAccount, lblAccntBefore, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
			AniHelper.AccountBalanceFindMethod(0m, 0m, cmbAccount, lblAccntAfter, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0));
		}

		private void txtNarration_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			vchrTranLog = (from x in db.VchrTransaction()
				where x.VchrNo == nudVoucherNo.Value.ToInt32() && x.VchrType == VoucherType
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

		private void CreditNoteView_KeyDown(object sender, KeyEventArgs e)
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
				btnParty_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnAccnt_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditNoteView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblBefore = new System.Windows.Forms.Label();
            this.lblAccntAfter = new System.Windows.Forms.Label();
            this.lblAccntBefore = new System.Windows.Forms.Label();
            this.lblPartyAfter = new System.Windows.Forms.Label();
            this.lblPartyBefore = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAmount = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAccount = new AshUsercontrol.AshComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPartyAccount = new AshUsercontrol.AshComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnParty = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnAccnt = new System.Windows.Forms.Button();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
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
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
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
            // lblAccntAfter
            // 
            resources.ApplyResources(this.lblAccntAfter, "lblAccntAfter");
            this.lblAccntAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAccntAfter.ForeColor = System.Drawing.Color.Red;
            this.lblAccntAfter.Name = "lblAccntAfter";
            this.toolTip.SetToolTip(this.lblAccntAfter, resources.GetString("lblAccntAfter.ToolTip"));
            // 
            // lblAccntBefore
            // 
            resources.ApplyResources(this.lblAccntBefore, "lblAccntBefore");
            this.lblAccntBefore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAccntBefore.ForeColor = System.Drawing.Color.Red;
            this.lblAccntBefore.Name = "lblAccntBefore";
            this.toolTip.SetToolTip(this.lblAccntBefore, resources.GetString("lblAccntBefore.ToolTip"));
            // 
            // lblPartyAfter
            // 
            resources.ApplyResources(this.lblPartyAfter, "lblPartyAfter");
            this.lblPartyAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPartyAfter.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblPartyAfter.Name = "lblPartyAfter";
            this.toolTip.SetToolTip(this.lblPartyAfter, resources.GetString("lblPartyAfter.ToolTip"));
            // 
            // lblPartyBefore
            // 
            resources.ApplyResources(this.lblPartyBefore, "lblPartyBefore");
            this.lblPartyBefore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPartyBefore.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblPartyBefore.Name = "lblPartyBefore";
            this.toolTip.SetToolTip(this.lblPartyBefore, resources.GetString("lblPartyBefore.ToolTip"));
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
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
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
            this.txtAmount.Validated += new System.EventHandler(this.txtAmount_Validated);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // cmbAccount
            // 
            resources.ApplyResources(this.cmbAccount, "cmbAccount");
            this.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbAccount.Name = "cmbAccount";
            this.toolTip.SetToolTip(this.cmbAccount, resources.GetString("cmbAccount.ToolTip"));
            this.cmbAccount.SelectedIndexChanged += new System.EventHandler(this.cmbAccount_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbPartyAccount
            // 
            resources.ApplyResources(this.cmbPartyAccount, "cmbPartyAccount");
            this.cmbPartyAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAccount.FormattingEnabled = true;
            this.cmbPartyAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAccount.Name = "cmbPartyAccount";
            this.toolTip.SetToolTip(this.cmbPartyAccount, resources.GetString("cmbPartyAccount.ToolTip"));
            this.cmbPartyAccount.SelectedIndexChanged += new System.EventHandler(this.cmbPartyAccount_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // btnParty
            // 
            resources.ApplyResources(this.btnParty, "btnParty");
            this.btnParty.Name = "btnParty";
            this.toolTip.SetToolTip(this.btnParty, resources.GetString("btnParty.ToolTip"));
            this.btnParty.UseVisualStyleBackColor = true;
            this.btnParty.Click += new System.EventHandler(this.btnParty_Click);
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.toolTip.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
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
            // btnAccnt
            // 
            resources.ApplyResources(this.btnAccnt, "btnAccnt");
            this.btnAccnt.Name = "btnAccnt";
            this.toolTip.SetToolTip(this.btnAccnt, resources.GetString("btnAccnt.ToolTip"));
            this.btnAccnt.UseVisualStyleBackColor = true;
            this.btnAccnt.Click += new System.EventHandler(this.btnAccnt_Click);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
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
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDetails.EnableHeadersVisualStyles = false;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle2;
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
            // CreditNoteView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.lblAfter);
            this.Controls.Add(this.lblBefore);
            this.Controls.Add(this.lblAccntAfter);
            this.Controls.Add(this.lblAccntBefore);
            this.Controls.Add(this.lblPartyAfter);
            this.Controls.Add(this.lblPartyBefore);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbPartyAccount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnParty);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAccnt);
            this.Name = "CreditNoteView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.CreditNoteView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditNoteView_KeyDown);
            this.Controls.SetChildIndex(this.btnAccnt, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.btnParty, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbPartyAccount, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbAccount, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtAmount, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.lblPartyBefore, 0);
            this.Controls.SetChildIndex(this.lblPartyAfter, 0);
            this.Controls.SetChildIndex(this.lblAccntBefore, 0);
            this.Controls.SetChildIndex(this.lblAccntAfter, 0);
            this.Controls.SetChildIndex(this.lblBefore, 0);
            this.Controls.SetChildIndex(this.lblAfter, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
