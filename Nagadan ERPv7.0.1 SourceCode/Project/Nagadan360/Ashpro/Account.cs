using System;
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
	public class Account : MasterForm
	{
		private int? iAccountId;

		private string sVchrType = "Opening Voucher";

		private DBContext db;

		private IContainer components = null;

		private Label label9;

		private Label label7;

		private Label label8;

		private Label label14;

		private Label label13;

		private Label label1;

		private Label label6;

		private Label label5;

		private Label lblAddressOne;

		private Label label3;

		private Label label2;

		private Label label19;

		private Label lblAddressThree;

		private Label lblAddressTwo;

		private Button btnCreateAcctGrp;

		private Label label10;

		private Label label11;

		private Label label23;

		private Label label24;

		private Label label25;

		private CheckBox cbActive;

		private AshTextBoxNormal txtName;

		private AshTextBoxNormal txtOppBalance;

		private AshComboBox cmbDrOrCr;

		private AshDateTimePicker dtpDate;

		private AshTextBoxNormal txtDescription;

		private GroupBox grpBankDetails;

		private AshTextBoxNormal txtAcctNo;

		private AshTextBoxNormal txtBankAddress;

		private AshTextBoxNormal txtBankName;

		private GroupBox grpContactDetails;

		private AshTextBoxNormal txtStateCode;

		private AshTextBoxNormal txtGstNo;

		private AshTextBoxNormal txtEmail;

		private AshTextBoxNormal txtFax;

		private AshTextBoxNormal txtMobile;

		private AshTextBoxNormal txtPhoneNo;

		private AshTextBoxNormal txtAddressThree;

		private AshTextBoxNormal txtAddressTwo;

		private AshTextBoxNormal txtAddressOne;

		private AshComboBox cmbPrcCategory;

		private AshComboBox cmbRoute;

		private AshComboBox cmbAccountGrp;

		private Panel pnlMain;

		private Panel pnlSpecial;

		private Label lblLimit;

		private AshTextBoxNormal txtLimit;

		private AshTextBoxNormal txtAddFour;

		private Label lblAddressFour;

		public bool isOpenFromAnotherForm { get; set; }

		public int Id { get; set; }

		public string accountGroupName { get; set; }

		public string sFormName { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public Account()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
		}

		public void ReloadMethod(MasterSerchForm frm)
		{
			AccountEntity accountEntity = db.Accounts(new
			{
				AccountId = frm.id
			}).FirstOrDefault();
			if (accountEntity != null)
			{
				txtName.Text = accountEntity.AccountName;
				cmbAccountGrp.SelectedValue = accountEntity.AGroupId.GetValueOrDefault();
				cmbRoute.SelectedValue = accountEntity.RootId.GetValueOrDefault();
				cmbPrcCategory.SelectedValue = accountEntity.PCategoryId.GetValueOrDefault();
				txtOppBalance.Text = accountEntity.OpeningBalance.ToString(CommonSettings.strCurencyFormat);
				cmbDrOrCr.Text = accountEntity.DebitOrCridt;
				txtDescription.Text = accountEntity.Remarks;
				txtAddressOne.Text = accountEntity.AddressOne;
				txtAddressTwo.Text = accountEntity.AddressTwo;
				txtAddressThree.Text = accountEntity.AddressThree;
				txtAddFour.Text = accountEntity.AddressFour;
				txtPhoneNo.Text = accountEntity.PhoneOne;
				txtMobile.Text = accountEntity.MobileNo;
				txtFax.Text = accountEntity.FaxNo;
				txtEmail.Text = accountEntity.EMailOne;
				txtGstNo.Text = accountEntity.TinNo;
				txtStateCode.Text = accountEntity.CstNo;
				txtBankName.Text = accountEntity.BankName;
				txtBankAddress.Text = accountEntity.BankAddress;
				txtAcctNo.Text = accountEntity.AccountNo;
				cbActive.Checked = accountEntity.isActive;
				iAccountId = accountEntity.AccountId;
				cmbAccountGrp_SelectedIndexChanged(null, null);
				btnDelete.Enabled = true;
			}
			txtName.Focus();
		}

		private AccountEntity entityLoadMethod()
		{
			int? rootId = null;
			int? pCategoryId = null;
			if (cmbRoute.Text != string.Empty)
			{
				rootId = cmbRoute.SelectedValue.ToInt32();
			}
			if (cmbPrcCategory.Text != string.Empty)
			{
				pCategoryId = cmbPrcCategory.SelectedValue.ToInt32();
			}
			return new AccountEntity
			{
				AccountId = iAccountId,
				AGroupId = cmbAccountGrp.SelectedValue.ToInt32(),
				AccountName = txtName.Text,
				RootId = rootId,
				PCategoryId = pCategoryId,
				OpeningBalance = txtOppBalance.Value,
				DebitOrCridt = cmbDrOrCr.Text,
				AddressOne = txtAddressOne.Text,
				AddressTwo = txtAddressTwo.Text,
				AddressThree = txtAddressThree.Text,
				AddressFour = txtAddFour.Text,
				PhoneOne = txtPhoneNo.Text,
				MobileNo = txtMobile.Text,
				FaxNo = txtFax.Text,
				TinNo = txtGstNo.Text,
				CstNo = txtStateCode.Text,
				EMailOne = txtEmail.Text,
				Remarks = txtDescription.Text,
				BankName = txtBankName.Text,
				BankAddress = txtBankAddress.Text,
				AccountNo = txtAcctNo.Text,
				IsBuiltIn = false,
				isActive = true,
				DebitLimit = txtLimit.Value,
				IsLimitApplicable = (txtLimit.Visible ? true : false)
			};
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			foreach (Control control4 in pnlMain.Controls)
			{
				if (control4 is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)control4;
					if (ashTextBoxNormal.isNumeric)
					{
						ashTextBoxNormal.Text = "0";
					}
					else
					{
						ashTextBoxNormal.Text = string.Empty;
					}
				}
			}
			foreach (Control control5 in grpContactDetails.Controls)
			{
				if (control5 is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal2 = (AshTextBoxNormal)control5;
					ashTextBoxNormal2.Text = string.Empty;
				}
			}
			foreach (Control control6 in grpBankDetails.Controls)
			{
				if (control6 is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal3 = (AshTextBoxNormal)control6;
					ashTextBoxNormal3.Text = string.Empty;
				}
			}
			dtpDate.Value = DateTime.Now.Date;
			iAccountId = null;
			btnDelete.Enabled = false;
			pnlSpecial.Visible = false;
			if (accountGroupName == string.Empty)
			{
				grpContactDetails.Visible = false;
				grpBankDetails.Visible = false;
				pnlSpecial.Visible = false;
			}
			else if (accountGroupName == "CUSTOMER/SUNDRY DEBTORS")
			{
				pnlSpecial.Visible = true;
			}
			txtName.Focus();
		}

		private void Account_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillPCategory(cmbPrcCategory);
			AniHelper.FillAGroup(cmbAccountGrp);
			lblAddressOne.Text = CommonSettings.strAddressOneLabel + " :";
			lblAddressTwo.Text = CommonSettings.strAddressTwoLabel + " :";
			lblAddressThree.Text = CommonSettings.strAddressThreeLabel + " :";
			lblAddressFour.Text = CommonSettings.strAddressFourLabel + " :";
			if (isOpenFromAnotherForm)
			{
				cmbAccountGrp.Text = accountGroupName;
				btnSearch.Enabled = false;
				cmbAccountGrp.Enabled = false;
				cmbAccountGrp.TabStop = false;
				btnCreateAcctGrp.TabStop = false;
				btnCreateAcctGrp.Enabled = false;
				grpContactDetails.Enabled = true;
				grpBankDetails.Enabled = true;
				if (accountGroupName == "CUSTOMER/SUNDRY DEBTORS")
				{
					pnlSpecial.Visible = true;
					Label label = lblLimit;
					txtLimit.Visible = true;
					label.Visible = true;
				}
			}
			else if (accountGroupName != string.Empty)
			{
				cmbAccountGrp.Text = accountGroupName;
				cmbAccountGrp.Enabled = false;
				btnCreateAcctGrp.Enabled = false;
				cmbAccountGrp.TabStop = false;
				btnCreateAcctGrp.TabStop = false;
				if (accountGroupName != "INDIRECT EXPENSES")
				{
					grpContactDetails.Visible = true;
					grpBankDetails.Visible = true;
				}
				if (accountGroupName == "CUSTOMER/SUNDRY DEBTORS")
				{
					pnlSpecial.Visible = true;
					Label label2 = lblLimit;
					txtLimit.Visible = true;
					label2.Visible = true;
				}
			}
			lblMasterTitle.Text = sFormName;
			base.ActiveControl = txtName;
		}

		private void btnCreateAcctGrp_Click(object sender, EventArgs e)
		{
			AccountGroup accountGroup = new AccountGroup();
			accountGroup.isFromAccount = true;
			if (accountGroup.ShowDialog() == DialogResult.OK)
			{
				AshproCommon.FillCombo(cmbAccountGrp, "Select AGroupId as Id, GroupName as Name From AccountGroup");
				cmbAccountGrp.Text = accountGroup.sAccountGroup;
			}
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
				if (!iAccountId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
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
							Id = db.Accounts(new
							{
								AccountName = txtName.Text
							}).FirstOrDefault().AccountId.toInt32();
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
					VchrTranLog vchrTranLog = db.VchrTransaction().FirstOrDefault((VchrTranLog x) => x.FromAccountId == iAccountId.toInt32() && x.VchrType == sVchrType);
					if (vchrTranLog != null)
					{
						AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", vchrTranLog.VchrTranId.ToInt32());
						int num = (from x in db.TransactionLogs()
							where x.VchrId == vchrTranLog.VchrTranId.ToInt32() && x.VchrType == sVchrType
							select x).FirstOrDefault().TranId.ToInt32();
						AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", num.ToInt32());
					}
					if (AshproCommon.UpdateToDatabaseObj(entityLoadMethod(), "Account", "AccountId", iAccountId.toInt32()))
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

		private void cmbAccountGrp_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!(cmbAccountGrp.Text == "CUSTOMER/SUNDRY DEBTORS") && !(cmbAccountGrp.Text == "SUPPLIERS/SUNDRY CREDITORS") && !(cmbAccountGrp.Text == "CASH AT BANK"))
			{
				grpContactDetails.Visible = false;
				grpBankDetails.Visible = false;
			}
			else
			{
				grpContactDetails.Visible = true;
				grpBankDetails.Visible = true;
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				return;
			}
			if ((from x in db.Accounts()
				where x.AccountId == iAccountId.toInt32()
				select x).FirstOrDefault().IsBuiltIn)
			{
				Messages.BuiltinMessage();
				btnClear_Click(null, null);
				return;
			}
			if (Messages.DeleteConfirmationMessage())
			{
				VchrTranLog vchrTranLog = db.VchrTransaction().FirstOrDefault((VchrTranLog x) => x.FromAccountId == iAccountId.toInt32() && x.VchrType == sVchrType);
				if (vchrTranLog != null)
				{
					AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", vchrTranLog.VchrTranId.ToInt32());
					int num = (from x in db.TransactionLogs()
						where x.VchrId == vchrTranLog.VchrTranId.ToInt32() && x.VchrType == sVchrType
						select x).FirstOrDefault().TranId.ToInt32();
					AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", num.ToInt32());
				}
				AshproCommon.DeleteFromDatabase("Account", "AccountId", iAccountId.toInt32());
			}
			btnClear_Click(null, null);
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && sFormName == "Chart Of Account")
			{
				btnSave_Click(null, null);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				string empty = string.Empty;
				empty = ((!(accountGroupName == "")) ? (" GroupName = '" + accountGroupName + "'") : " GroupName <> ''");
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Account Name", "Account Group");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select AccountId as id,AccountName as name,GroupName as description From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where " + empty);
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.account = this;
				masterSerchForm.TopLevel = false;
				masterSerchForm.Location = new Point(base.Width + 50, 25);
				if (!base.IsMdiChild)
				{
					TabPage tabPage = (TabPage)base.Parent;
					tabPage.Controls.Add(masterSerchForm);
				}
				else
				{
					masterSerchForm.MdiParent = base.MdiParent;
				}
				masterSerchForm.Show();
				masterSerchForm.BringToFront();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Account));
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCreateAcctGrp = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblAddressThree = new System.Windows.Forms.Label();
            this.lblAddressTwo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAddressOne = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBankDetails = new System.Windows.Forms.GroupBox();
            this.txtAcctNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtBankAddress = new AshUsercontrol.AshTextBoxNormal();
            this.txtBankName = new AshUsercontrol.AshTextBoxNormal();
            this.grpContactDetails = new System.Windows.Forms.GroupBox();
            this.lblAddressFour = new System.Windows.Forms.Label();
            this.txtStateCode = new AshUsercontrol.AshTextBoxNormal();
            this.txtGstNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtEmail = new AshUsercontrol.AshTextBoxNormal();
            this.txtFax = new AshUsercontrol.AshTextBoxNormal();
            this.txtMobile = new AshUsercontrol.AshTextBoxNormal();
            this.txtPhoneNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddFour = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressThree = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressTwo = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressOne = new AshUsercontrol.AshTextBoxNormal();
            this.cmbPrcCategory = new AshUsercontrol.AshComboBox();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.dtpDate = new AshUsercontrol.AshDateTimePicker();
            this.cmbDrOrCr = new AshUsercontrol.AshComboBox();
            this.txtOppBalance = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.cmbAccountGrp = new AshUsercontrol.AshComboBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblLimit = new System.Windows.Forms.Label();
            this.txtLimit = new AshUsercontrol.AshTextBoxNormal();
            this.pnlSpecial = new System.Windows.Forms.Panel();
            this.pnlButton.SuspendLayout();
            this.grpBankDetails.SuspendLayout();
            this.grpContactDetails.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlSpecial.SuspendLayout();
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
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.cbActive.TabStop = false;
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
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
            this.btnCreateAcctGrp.TabStop = false;
            this.btnCreateAcctGrp.UseVisualStyleBackColor = false;
            this.btnCreateAcctGrp.Click += new System.EventHandler(this.btnCreateAcctGrp_Click);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // lblAddressThree
            // 
            resources.ApplyResources(this.lblAddressThree, "lblAddressThree");
            this.lblAddressThree.Name = "lblAddressThree";
            // 
            // lblAddressTwo
            // 
            resources.ApplyResources(this.lblAddressTwo, "lblAddressTwo");
            this.lblAddressTwo.Name = "lblAddressTwo";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblAddressOne
            // 
            resources.ApplyResources(this.lblAddressOne, "lblAddressOne");
            this.lblAddressOne.Name = "lblAddressOne";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // grpBankDetails
            // 
            resources.ApplyResources(this.grpBankDetails, "grpBankDetails");
            this.grpBankDetails.Controls.Add(this.txtAcctNo);
            this.grpBankDetails.Controls.Add(this.label9);
            this.grpBankDetails.Controls.Add(this.txtBankAddress);
            this.grpBankDetails.Controls.Add(this.txtBankName);
            this.grpBankDetails.Controls.Add(this.label7);
            this.grpBankDetails.Controls.Add(this.label8);
            this.grpBankDetails.Name = "grpBankDetails";
            this.grpBankDetails.TabStop = false;
            // 
            // txtAcctNo
            // 
            resources.ApplyResources(this.txtAcctNo, "txtAcctNo");
            this.txtAcctNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAcctNo.Data = 0;
            this.txtAcctNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtAcctNo.Format = null;
            this.txtAcctNo.isAllowNegative = false;
            this.txtAcctNo.isAllowSpecialChar = false;
            this.txtAcctNo.isNumberOnly = false;
            this.txtAcctNo.isNumeric = false;
            this.txtAcctNo.LeaveColor = System.Drawing.Color.White;
            this.txtAcctNo.Name = "txtAcctNo";
            this.txtAcctNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtBankAddress
            // 
            resources.ApplyResources(this.txtBankAddress, "txtBankAddress");
            this.txtBankAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBankAddress.Data = 0;
            this.txtBankAddress.EnterColor = System.Drawing.Color.LightPink;
            this.txtBankAddress.Format = null;
            this.txtBankAddress.isAllowNegative = false;
            this.txtBankAddress.isAllowSpecialChar = false;
            this.txtBankAddress.isNumberOnly = false;
            this.txtBankAddress.isNumeric = false;
            this.txtBankAddress.LeaveColor = System.Drawing.Color.White;
            this.txtBankAddress.Name = "txtBankAddress";
            this.txtBankAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtBankName
            // 
            resources.ApplyResources(this.txtBankName, "txtBankName");
            this.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBankName.Data = 0;
            this.txtBankName.EnterColor = System.Drawing.Color.LightPink;
            this.txtBankName.Format = null;
            this.txtBankName.isAllowNegative = false;
            this.txtBankName.isAllowSpecialChar = false;
            this.txtBankName.isNumberOnly = false;
            this.txtBankName.isNumeric = false;
            this.txtBankName.LeaveColor = System.Drawing.Color.White;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // grpContactDetails
            // 
            resources.ApplyResources(this.grpContactDetails, "grpContactDetails");
            this.grpContactDetails.Controls.Add(this.lblAddressFour);
            this.grpContactDetails.Controls.Add(this.txtStateCode);
            this.grpContactDetails.Controls.Add(this.lblAddressThree);
            this.grpContactDetails.Controls.Add(this.txtGstNo);
            this.grpContactDetails.Controls.Add(this.lblAddressTwo);
            this.grpContactDetails.Controls.Add(this.txtEmail);
            this.grpContactDetails.Controls.Add(this.label14);
            this.grpContactDetails.Controls.Add(this.txtFax);
            this.grpContactDetails.Controls.Add(this.label13);
            this.grpContactDetails.Controls.Add(this.txtMobile);
            this.grpContactDetails.Controls.Add(this.label1);
            this.grpContactDetails.Controls.Add(this.txtPhoneNo);
            this.grpContactDetails.Controls.Add(this.label6);
            this.grpContactDetails.Controls.Add(this.txtAddFour);
            this.grpContactDetails.Controls.Add(this.label5);
            this.grpContactDetails.Controls.Add(this.lblAddressOne);
            this.grpContactDetails.Controls.Add(this.txtAddressThree);
            this.grpContactDetails.Controls.Add(this.txtAddressTwo);
            this.grpContactDetails.Controls.Add(this.txtAddressOne);
            this.grpContactDetails.Controls.Add(this.label19);
            this.grpContactDetails.Name = "grpContactDetails";
            this.grpContactDetails.TabStop = false;
            // 
            // lblAddressFour
            // 
            resources.ApplyResources(this.lblAddressFour, "lblAddressFour");
            this.lblAddressFour.Name = "lblAddressFour";
            // 
            // txtStateCode
            // 
            resources.ApplyResources(this.txtStateCode, "txtStateCode");
            this.txtStateCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStateCode.Data = 0;
            this.txtStateCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtStateCode.Format = null;
            this.txtStateCode.isAllowNegative = false;
            this.txtStateCode.isAllowSpecialChar = false;
            this.txtStateCode.isNumberOnly = true;
            this.txtStateCode.isNumeric = false;
            this.txtStateCode.LeaveColor = System.Drawing.Color.White;
            this.txtStateCode.Name = "txtStateCode";
            this.txtStateCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGstNo
            // 
            resources.ApplyResources(this.txtGstNo, "txtGstNo");
            this.txtGstNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGstNo.Data = 0;
            this.txtGstNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtGstNo.Format = null;
            this.txtGstNo.isAllowNegative = false;
            this.txtGstNo.isAllowSpecialChar = false;
            this.txtGstNo.isNumberOnly = false;
            this.txtGstNo.isNumeric = false;
            this.txtGstNo.LeaveColor = System.Drawing.Color.White;
            this.txtGstNo.Name = "txtGstNo";
            this.txtGstNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Data = 0;
            this.txtEmail.EnterColor = System.Drawing.Color.LightPink;
            this.txtEmail.Format = null;
            this.txtEmail.isAllowNegative = false;
            this.txtEmail.isAllowSpecialChar = false;
            this.txtEmail.isNumberOnly = false;
            this.txtEmail.isNumeric = false;
            this.txtEmail.LeaveColor = System.Drawing.Color.White;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtFax
            // 
            resources.ApplyResources(this.txtFax, "txtFax");
            this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFax.Data = 0;
            this.txtFax.EnterColor = System.Drawing.Color.LightPink;
            this.txtFax.Format = null;
            this.txtFax.isAllowNegative = false;
            this.txtFax.isAllowSpecialChar = false;
            this.txtFax.isNumberOnly = false;
            this.txtFax.isNumeric = false;
            this.txtFax.LeaveColor = System.Drawing.Color.White;
            this.txtFax.Name = "txtFax";
            this.txtFax.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMobile
            // 
            resources.ApplyResources(this.txtMobile, "txtMobile");
            this.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMobile.Data = 0;
            this.txtMobile.EnterColor = System.Drawing.Color.LightPink;
            this.txtMobile.Format = null;
            this.txtMobile.isAllowNegative = false;
            this.txtMobile.isAllowSpecialChar = false;
            this.txtMobile.isNumberOnly = false;
            this.txtMobile.isNumeric = false;
            this.txtMobile.LeaveColor = System.Drawing.Color.White;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPhoneNo
            // 
            resources.ApplyResources(this.txtPhoneNo, "txtPhoneNo");
            this.txtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneNo.Data = 0;
            this.txtPhoneNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtPhoneNo.Format = null;
            this.txtPhoneNo.isAllowNegative = false;
            this.txtPhoneNo.isAllowSpecialChar = false;
            this.txtPhoneNo.isNumberOnly = false;
            this.txtPhoneNo.isNumeric = false;
            this.txtPhoneNo.LeaveColor = System.Drawing.Color.White;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddFour
            // 
            resources.ApplyResources(this.txtAddFour, "txtAddFour");
            this.txtAddFour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddFour.Data = 0;
            this.txtAddFour.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddFour.Format = null;
            this.txtAddFour.isAllowNegative = false;
            this.txtAddFour.isAllowSpecialChar = false;
            this.txtAddFour.isNumberOnly = false;
            this.txtAddFour.isNumeric = false;
            this.txtAddFour.LeaveColor = System.Drawing.Color.White;
            this.txtAddFour.Name = "txtAddFour";
            this.txtAddFour.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddressThree
            // 
            resources.ApplyResources(this.txtAddressThree, "txtAddressThree");
            this.txtAddressThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressThree.Data = 0;
            this.txtAddressThree.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressThree.Format = null;
            this.txtAddressThree.isAllowNegative = false;
            this.txtAddressThree.isAllowSpecialChar = false;
            this.txtAddressThree.isNumberOnly = false;
            this.txtAddressThree.isNumeric = false;
            this.txtAddressThree.LeaveColor = System.Drawing.Color.White;
            this.txtAddressThree.Name = "txtAddressThree";
            this.txtAddressThree.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddressTwo
            // 
            resources.ApplyResources(this.txtAddressTwo, "txtAddressTwo");
            this.txtAddressTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressTwo.Data = 0;
            this.txtAddressTwo.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressTwo.Format = null;
            this.txtAddressTwo.isAllowNegative = false;
            this.txtAddressTwo.isAllowSpecialChar = false;
            this.txtAddressTwo.isNumberOnly = false;
            this.txtAddressTwo.isNumeric = false;
            this.txtAddressTwo.LeaveColor = System.Drawing.Color.White;
            this.txtAddressTwo.Name = "txtAddressTwo";
            this.txtAddressTwo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddressOne
            // 
            resources.ApplyResources(this.txtAddressOne, "txtAddressOne");
            this.txtAddressOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressOne.Data = 0;
            this.txtAddressOne.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressOne.Format = null;
            this.txtAddressOne.isAllowNegative = false;
            this.txtAddressOne.isAllowSpecialChar = false;
            this.txtAddressOne.isNumberOnly = false;
            this.txtAddressOne.isNumeric = false;
            this.txtAddressOne.LeaveColor = System.Drawing.Color.White;
            this.txtAddressOne.Name = "txtAddressOne";
            this.txtAddressOne.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbPrcCategory
            // 
            resources.ApplyResources(this.cmbPrcCategory, "cmbPrcCategory");
            this.cmbPrcCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPrcCategory.FormattingEnabled = true;
            this.cmbPrcCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbPrcCategory.Name = "cmbPrcCategory";
            // 
            // cmbRoute
            // 
            resources.ApplyResources(this.cmbRoute, "cmbRoute");
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Name = "cmbRoute";
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
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.cmbAccountGrp.SelectedIndexChanged += new System.EventHandler(this.cmbAccountGrp_SelectedIndexChanged);
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnCreateAcctGrp);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.lblLimit);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.cbActive);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.label24);
            this.pnlMain.Controls.Add(this.cmbAccountGrp);
            this.pnlMain.Controls.Add(this.txtDescription);
            this.pnlMain.Controls.Add(this.txtLimit);
            this.pnlMain.Controls.Add(this.txtOppBalance);
            this.pnlMain.Controls.Add(this.dtpDate);
            this.pnlMain.Controls.Add(this.cmbDrOrCr);
            this.pnlMain.Name = "pnlMain";
            // 
            // lblLimit
            // 
            resources.ApplyResources(this.lblLimit, "lblLimit");
            this.lblLimit.Name = "lblLimit";
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
            // pnlSpecial
            // 
            resources.ApplyResources(this.pnlSpecial, "pnlSpecial");
            this.pnlSpecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSpecial.Controls.Add(this.cmbPrcCategory);
            this.pnlSpecial.Controls.Add(this.cmbRoute);
            this.pnlSpecial.Controls.Add(this.label23);
            this.pnlSpecial.Controls.Add(this.label25);
            this.pnlSpecial.Name = "pnlSpecial";
            // 
            // Account
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlSpecial);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.grpContactDetails);
            this.Controls.Add(this.grpBankDetails);
            this.MaximizeBox = false;
            this.Name = "Account";
            this.Load += new System.EventHandler(this.Account_Load);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.grpBankDetails, 0);
            this.Controls.SetChildIndex(this.grpContactDetails, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.pnlSpecial, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.pnlButton.ResumeLayout(false);
            this.grpBankDetails.ResumeLayout(false);
            this.grpBankDetails.PerformLayout();
            this.grpContactDetails.ResumeLayout(false);
            this.grpContactDetails.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlSpecial.ResumeLayout(false);
            this.pnlSpecial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
