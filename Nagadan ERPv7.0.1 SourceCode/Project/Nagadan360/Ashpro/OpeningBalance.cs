using System;
using System.ComponentModel;
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
	public class OpeningBalance : BaseForm
	{
		private string strVoucherType = "Opening Voucher";

		private int iVchrId;

		private VchrTranLog vchrTranLog;

		private DBContext db;

		private IContainer components = null;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private AshTextBoxNormal txtNarration;

		private Label label8;

		private AshTextBoxNormal txtDbtAmnt;

		private Label label7;

		private AshComboBox cmbAccount;

		private Label label5;

		private AshDateTimePicker dtmDate;

		private Label label4;

		private AshUpDown nudVoucherNo;

		private Label label3;

		private AshTextBoxNormal txtCrtAmnt;

		private Label label1;

		private Label label2;

		private ToolTip toolTip;

		private Button button4;

		private Label lblHead;

		public string PassingValueOne { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public OpeningBalance()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void EnableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			nudVoucherNo.Enabled = true;
			btnEdit.Enabled = true;
			btnDelete.Enabled = true;
			btnClear.Enabled = true;
			btnEnd.Enabled = true;
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

		private void EntityMethod(int iVoucherNo)
		{
			vchrTranLog.VchrTranId = iVchrId;
			vchrTranLog.VchrNo = iVoucherNo;
			vchrTranLog.VchrDate = dtmDate.Value.Date;
			vchrTranLog.VchrType = strVoucherType;
			if (txtDbtAmnt.Value > 0m)
			{
				vchrTranLog.VchrAmount = txtDbtAmnt.Value;
				vchrTranLog.VchrName = "Debit Voucher";
			}
			else
			{
				vchrTranLog.VchrAmount = txtCrtAmnt.Value;
				vchrTranLog.VchrName = "Credit Voucher";
			}
			vchrTranLog.FromAccountId = cmbAccount.SelectedValue.ToInt32();
			vchrTranLog.Narration = txtNarration.Text;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.SalesManId = Settings.Default.intLogOnId;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			cmbAccount.SelectedIndex = 0;
			dtmDate.Value = DateTime.Now;
			btnSave.Text = "Save";
			DisableMethod();
			dtmDate.Focus();
		}

		private void OpeningBalance_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillAllAccount(cmbAccount);
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == strVoucherType
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			if (PassingValueOne != null)
			{
				nudVoucherNo.Value = Convert.ToDecimal(PassingValueOne);
			}
		}

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			vchrTranLog = (from x in db.VchrTransaction()
				where x.VchrType == strVoucherType && x.VchrNo == nudVoucherNo.Value.ToInt32()
				select x).FirstOrDefault();
			if (vchrTranLog != null)
			{
				iVchrId = vchrTranLog.VchrTranId.ToInt32();
				cmbAccount.SelectedValue = vchrTranLog.FromAccountId;
				if (vchrTranLog.VchrName == "Debit Voucher")
				{
					txtDbtAmnt.Value = vchrTranLog.VchrAmount;
				}
				else
				{
					txtCrtAmnt.Value = vchrTranLog.VchrAmount;
				}
				dtmDate.Value = vchrTranLog.VchrDate;
				txtNarration.Text = vchrTranLog.Narration;
				btnSave.Text = "Update";
				EnableMethod();
			}
			else
			{
				btnClearMethod();
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
					int iValue = AshproCommon.IdFindMethod("Select TranId From TransactionLog Where VchrType='" + vchrTranLog.VchrType + "' and VchrName='" + vchrTranLog.VchrName + "' and VchrId='" + iVchrId + "'");
					AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", iValue);
					AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", iVchrId);
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnSave.Text = "Update";
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
		}

		private void OpeningBalance_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.C && e.Control)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.S && e.Control)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Control)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Control)
			{
				e.SuppressKeyPress = true;
				btnEdit_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Control)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtDbtAmnt.Value <= 0m && txtCrtAmnt.Value <= 0m)
			{
				MessageBox.Show("You have to enter the amount.", Utility.uManufacturer);
				cmbAccount.Focus();
				return;
			}
			int iVoucherNo = 1;
			vchrTranLog = new VchrTranLog();
			if (btnSave.Text == "Save")
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				iVoucherNo = ((from x in db.VchrTransaction()
					where x.VchrType == strVoucherType
					select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityMethod(iVoucherNo);
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrId = (from x in db.VchrTransaction()
						where x.VchrType == strVoucherType && x.VchrNo == iVoucherNo
						select x).FirstOrDefault().VchrTranId.ToInt32();
					if (txtDbtAmnt.Value > 0m)
					{
						POSHelper.TranLogMethod(txtDbtAmnt.Value, 0m, cmbAccount.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVoucherNo, strVoucherType, Settings.Default.intLogOnId, "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
						AshproCommon.DatabaseMethod("Update Account set OpeningBalance='" + txtDbtAmnt.Value + "',DebitOrCridt='DR'");
					}
					else
					{
						POSHelper.TranLogMethod(0m, txtCrtAmnt.Value, cmbAccount.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVoucherNo, strVoucherType, Settings.Default.intLogOnId, "Main", vchrTranLog.VchrName, AniHelper.GetDate(dtmDate.Value));
						AshproCommon.DatabaseMethod("Update Account set OpeningBalance='" + txtCrtAmnt.Value + "',DebitOrCridt='CR'");
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
			iVoucherNo = nudVoucherNo.Value.ToInt32();
			EntityMethod(iVoucherNo);
			int iValue = AshproCommon.IdFindMethod("Select TranId From TransactionLog Where VchrType='" + vchrTranLog.VchrType + "' and VchrName='" + vchrTranLog.VchrName + "' and VchrId='" + iVchrId + "'");
			AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", iValue);
			if (AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iVchrId))
			{
				if (txtDbtAmnt.Value > 0m)
				{
					POSHelper.TranLogMethod(txtDbtAmnt.Value, 0m, cmbAccount.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVoucherNo, strVoucherType, Settings.Default.intLogOnId, "Main", vchrTranLog.VchrName);
					AshproCommon.DatabaseMethod("Update Account set OpeningBalance='" + txtDbtAmnt.Value + "',DebitOrCridt='DR'");
				}
				else
				{
					POSHelper.TranLogMethod(0m, txtCrtAmnt.Value, cmbAccount.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVoucherNo, strVoucherType, Settings.Default.intLogOnId, "Main", vchrTranLog.VchrName);
					AshproCommon.DatabaseMethod("Update Account set OpeningBalance='" + txtCrtAmnt.Value + "',DebitOrCridt='CR'");
				}
				btnClear_Click(null, null);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudVoucherNo.Value = ((from x in db.VchrTransaction()
				where x.VchrType == strVoucherType
				select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			btnClearMethod();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpeningBalance));
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDbtAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAccount = new AshUsercontrol.AshComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCrtAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnEnd, "btnEnd");
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Name = "btnEdit";
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.toolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNarration
            // 
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNarration.Data = 0;
            this.txtNarration.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.Format = null;
            this.txtNarration.isAllowNegative = false;
            this.txtNarration.isAllowSpecialChar = false;
            this.txtNarration.isNumberOnly = false;
            this.txtNarration.isNumeric = false;
            this.txtNarration.LeaveColor = System.Drawing.Color.White;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtDbtAmnt
            // 
            this.txtDbtAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDbtAmnt.Data = 0;
            this.txtDbtAmnt.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtDbtAmnt, "txtDbtAmnt");
            this.txtDbtAmnt.Format = null;
            this.txtDbtAmnt.isAllowNegative = false;
            this.txtDbtAmnt.isAllowSpecialChar = false;
            this.txtDbtAmnt.isNumberOnly = false;
            this.txtDbtAmnt.isNumeric = true;
            this.txtDbtAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtDbtAmnt.Name = "txtDbtAmnt";
            this.txtDbtAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbAccount
            // 
            this.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccount.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.cmbAccount, "cmbAccount");
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbAccount.Name = "cmbAccount";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // nudVoucherNo
            // 
            resources.ApplyResources(this.nudVoucherNo, "nudVoucherNo");
            this.nudVoucherNo.Name = "nudVoucherNo";
            this.nudVoucherNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVoucherNo.ValueChanged += new System.EventHandler(this.nudVoucherNo_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtCrtAmnt
            // 
            this.txtCrtAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCrtAmnt.Data = 0;
            this.txtCrtAmnt.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtCrtAmnt, "txtCrtAmnt");
            this.txtCrtAmnt.Format = null;
            this.txtCrtAmnt.isAllowNegative = false;
            this.txtCrtAmnt.isAllowSpecialChar = false;
            this.txtCrtAmnt.isNumberOnly = false;
            this.txtCrtAmnt.isNumeric = true;
            this.txtCrtAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtCrtAmnt.Name = "txtCrtAmnt";
            this.txtCrtAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // OpeningBalance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCrtAmnt);
            this.Controls.Add(this.txtDbtAmnt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.Name = "OpeningBalance";
            this.Load += new System.EventHandler(this.OpeningBalance_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpeningBalance_KeyDown);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbAccount, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtDbtAmnt, 0);
            this.Controls.SetChildIndex(this.txtCrtAmnt, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
