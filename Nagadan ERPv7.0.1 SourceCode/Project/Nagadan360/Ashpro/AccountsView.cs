using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class AccountsView : MasterForm
	{
		private DBContext db = new DBContext();

		private string sVchrType = "Opening Voucher";

		private List<AccountEntity> accounts;

		private int? iMasterId;

		private IContainer components = null;

		private Panel pnlMain;

		private Label label2;

		private Label label11;

		private Button btnCreateAcctGrp;

		private Label lblLimit;

		private Label label24;

		private Label label10;

		private Label label3;

		private CheckBox cbActive;

		private AshTextBoxNormal txtName;

		private AshComboBox cmbAccountGrp;

		private AshTextBoxNormal txtDescription;

		private AshTextBoxNormal txtLimit;

		private AshTextBoxNormal txtOppBalance;

		private AshDateTimePicker dtpDate;

		private AshComboBox cmbDrOrCr;

		private BindingSource bdsDetails;

		public bool isOpenFromAnotherForm { get; set; }

		public string accountName { get; set; }

		public string accountGroupName { get; set; }

		public string sFormName { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public AccountsView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			txtOppBalance.Format = CommonSettings.strCurencyFormat;
			txtLimit.Format = CommonSettings.strCurencyFormat;
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
			base.ActiveControl = txtName;
		}

		private void ReloadMethod(AccountEntity accountEntity)
		{
			if (accountEntity != null)
			{
				txtName.Text = accountEntity.AccountName;
				cmbAccountGrp.SelectedValue = accountEntity.AGroupId;
				txtOppBalance.Value = accountEntity.OpeningBalance;
				cmbDrOrCr.Text = accountEntity.DebitOrCridt;
				txtDescription.Text = accountEntity.Remarks;
				cbActive.Checked = accountEntity.isActive;
				txtLimit.Value = accountEntity.DebitLimit;
				iMasterId = accountEntity.AccountId;
				btnDelete.Enabled = true;
			}
		}

		private AccountEntity entityLoadMethod()
		{
			return new AccountEntity
			{
				AccountId = iMasterId,
				AGroupId = cmbAccountGrp.SelectedValue.ToInt32(),
				AccountName = txtName.Text,
				OpeningBalance = txtOppBalance.Value,
				DebitOrCridt = cmbDrOrCr.Text,
				Remarks = txtDescription.Text,
				IsBuiltIn = false,
				isActive = true,
				DebitLimit = txtLimit.Value,
				IsLimitApplicable = (txtLimit.Visible ? true : false)
			};
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void btnCreateAcctGrp_Click(object sender, EventArgs e)
		{
			AccountGroup accountGroup = new AccountGroup();
			accountGroup.isFromAccount = true;
			if (accountGroup.ShowDialog() == DialogResult.OK)
			{
				AniHelper.FillAGroup(cmbAccountGrp);
				cmbAccountGrp.Text = accountGroup.sAccountGroup;
			}
		}

		private void AccountsView_Load(object sender, EventArgs e)
		{
			AniHelper.FillAGroup(cmbAccountGrp);
			if (isOpenFromAnotherForm)
			{
				cmbAccountGrp.Text = accountGroupName;
				btnSearch.Enabled = false;
				cmbAccountGrp.Enabled = false;
				cmbAccountGrp.TabStop = false;
				btnCreateAcctGrp.TabStop = false;
				btnCreateAcctGrp.Enabled = false;
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.PanelClearmethod(this);
			iMasterId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				return;
			}
			if ((from x in db.Accounts()
				where x.AccountId == iMasterId.toInt32()
				select x).FirstOrDefault().IsBuiltIn)
			{
				Messages.BuiltinMessage();
				btnClear_Click(null, null);
				return;
			}
			if (Messages.DeleteConfirmationMessage())
			{
				VchrTranLog vchrTranLog = db.VchrTransaction().FirstOrDefault((VchrTranLog x) => x.FromAccountId == iMasterId.toInt32() && x.VchrType == sVchrType);
				if (vchrTranLog != null)
				{
					AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", vchrTranLog.VchrTranId.ToInt32());
					int num = (from x in db.TransactionLogs()
						where x.VchrId == vchrTranLog.VchrTranId.ToInt32() && x.VchrType == sVchrType
						select x).FirstOrDefault().TranId.ToInt32();
					AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", num.ToInt32());
				}
				AshproCommon.DeleteFromDatabase("Account", "AccountId", iMasterId.toInt32());
			}
			btnClear_Click(null, null);
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					if (accounts.Any((AccountEntity x) => x.AccountName == txtName.Text))
					{
						Messages.ReferenceExistsMessage();
						return;
					}
					if (AshproCommon.InsertToDatabaseObj(entityLoadMethod(), "Account"))
					{
						if (txtOppBalance.Value > 0m)
						{
							AniHelper.OpeningBalanceVoucherMethod(txtOppBalance.Value, cmbDrOrCr.Text, db, txtName.Text, dtpDate.Value);
						}
						Messages.SavedMessage();
						if (isOpenFromAnotherForm)
						{
							accountName = txtName.Text;
							base.DialogResult = DialogResult.OK;
							Close();
						}
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					VchrTranLog vchrTranLog = db.VchrTransaction().FirstOrDefault((VchrTranLog x) => x.FromAccountId == iMasterId.toInt32() && x.VchrType == sVchrType);
					if (vchrTranLog != null)
					{
						AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", vchrTranLog.VchrTranId.ToInt32());
						int num = (from x in db.TransactionLogs()
							where x.VchrId == vchrTranLog.VchrTranId.ToInt32() && x.VchrType == sVchrType
							select x).FirstOrDefault().TranId.ToInt32();
						AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", num.ToInt32());
					}
					if (AshproCommon.UpdateToDatabaseObj(entityLoadMethod(), "Account", "AccountId", iMasterId.toInt32()))
					{
						if (txtOppBalance.Value > 0m)
						{
							AniHelper.OpeningBalanceVoucherMethod(txtOppBalance.Value, cmbDrOrCr.Text, db, txtName.Text, dtpDate.Value);
						}
						Messages.UpdateMessage();
					}
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Account Name", "Group Name");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select AccountId as id,AccountName as name,GroupName as description From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where GroupName <> '' ");
				masterSerchForm.Masters = db.Masters(dataTable);
				if (masterSerchForm.ShowDialog() == DialogResult.OK)
				{
					AccountEntity accountEntity = db.Accounts(new
					{
						AccountId = masterSerchForm.id
					}).FirstOrDefault();
					ReloadMethod(accountEntity);
					btnDelete.Enabled = true;
					btnDelete.ForeColor = Color.White;
					txtName.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsView));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCreateAcctGrp = new System.Windows.Forms.Button();
            this.lblLimit = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.cmbAccountGrp = new AshUsercontrol.AshComboBox();
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.txtLimit = new AshUsercontrol.AshTextBoxNormal();
            this.txtOppBalance = new AshUsercontrol.AshTextBoxNormal();
            this.dtpDate = new AshUsercontrol.AshDateTimePicker();
            this.cmbDrOrCr = new AshUsercontrol.AshComboBox();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.pnlButton.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            resources.ApplyResources(this.pnlButton, "pnlButton");
            // 
            // pnlMaster
            // 
            resources.ApplyResources(this.pnlMaster, "pnlMaster");
            // 
            // lblMasterTitle
            // 
            resources.ApplyResources(this.lblMasterTitle, "lblMasterTitle");
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.btnCreateAcctGrp);
            this.pnlMain.Controls.Add(this.lblLimit);
            this.pnlMain.Controls.Add(this.label24);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.cbActive);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.cmbAccountGrp);
            this.pnlMain.Controls.Add(this.txtDescription);
            this.pnlMain.Controls.Add(this.txtLimit);
            this.pnlMain.Controls.Add(this.txtOppBalance);
            this.pnlMain.Controls.Add(this.dtpDate);
            this.pnlMain.Controls.Add(this.cmbDrOrCr);
            this.pnlMain.Name = "pnlMain";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // btnCreateAcctGrp
            // 
            resources.ApplyResources(this.btnCreateAcctGrp, "btnCreateAcctGrp");
            this.btnCreateAcctGrp.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCreateAcctGrp.ForeColor = System.Drawing.Color.White;
            this.btnCreateAcctGrp.Name = "btnCreateAcctGrp";
            this.btnCreateAcctGrp.UseVisualStyleBackColor = false;
            this.btnCreateAcctGrp.Click += new System.EventHandler(this.btnCreateAcctGrp_Click);
            // 
            // lblLimit
            // 
            resources.ApplyResources(this.lblLimit, "lblLimit");
            this.lblLimit.Name = "lblLimit";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbAccountGrp
            // 
            resources.ApplyResources(this.cmbAccountGrp, "cmbAccountGrp");
            this.cmbAccountGrp.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccountGrp.FormattingEnabled = true;
            this.cmbAccountGrp.LeaveColor = System.Drawing.Color.White;
            this.cmbAccountGrp.Name = "cmbAccountGrp";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Data = 0;
            this.txtDescription.EnterColor = System.Drawing.Color.LightPink;
            this.txtDescription.Format = null;
            this.txtDescription.isAllowNegative = false;
            this.txtDescription.isAllowSpecialChar = false;
            this.txtDescription.isNumberOnly = false;
            this.txtDescription.isNumeric = false;
            this.txtDescription.LeaveColor = System.Drawing.Color.White;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // txtLimit
            // 
            resources.ApplyResources(this.txtLimit, "txtLimit");
            this.txtLimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLimit.Data = 0;
            this.txtLimit.EnterColor = System.Drawing.Color.LightPink;
            this.txtLimit.Format = null;
            this.txtLimit.isAllowNegative = false;
            this.txtLimit.isAllowSpecialChar = false;
            this.txtLimit.isNumberOnly = false;
            this.txtLimit.isNumeric = true;
            this.txtLimit.LeaveColor = System.Drawing.Color.White;
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtOppBalance
            // 
            resources.ApplyResources(this.txtOppBalance, "txtOppBalance");
            this.txtOppBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOppBalance.Data = 0;
            this.txtOppBalance.EnterColor = System.Drawing.Color.LightPink;
            this.txtOppBalance.Format = null;
            this.txtOppBalance.isAllowNegative = false;
            this.txtOppBalance.isAllowSpecialChar = false;
            this.txtOppBalance.isNumberOnly = false;
            this.txtOppBalance.isNumeric = true;
            this.txtOppBalance.LeaveColor = System.Drawing.Color.White;
            this.txtOppBalance.Name = "txtOppBalance";
            this.txtOppBalance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dtpDate
            // 
            resources.ApplyResources(this.dtpDate, "dtpDate");
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Name = "dtpDate";
            // 
            // cmbDrOrCr
            // 
            resources.ApplyResources(this.cmbDrOrCr, "cmbDrOrCr");
            this.cmbDrOrCr.EnterColor = System.Drawing.Color.LightPink;
            this.cmbDrOrCr.FormattingEnabled = true;
            this.cmbDrOrCr.Items.AddRange(new object[] {
            resources.GetString("cmbDrOrCr.Items"),
            resources.GetString("cmbDrOrCr.Items1")});
            this.cmbDrOrCr.LeaveColor = System.Drawing.Color.White;
            this.cmbDrOrCr.Name = "cmbDrOrCr";
            // 
            // AccountsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "AccountsView";
            this.Load += new System.EventHandler(this.AccountsView_Load);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlButton.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
