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
	public class CustomerPrevilegeCard : MasterForm
	{
		private DBContext db = new DBContext();

		private int? iPrevilageCardId;

		private IContainer components = null;

		private AshDateTimePicker dtmDOB;

		private AshDateTimePicker dtmExpairy;

		private AshDateTimePicker dtmRenewal;

		private Label label15;

		private AshTextBoxNormal txtAge;

		private Label label13;

		private Label label12;

		private Label label3;

		private AshTextBoxNormal txtCode;

		private Label label11;

		private Label label4;

		private Label label10;

		private Label label5;

		private Label label9;

		private AshTextBoxNormal txtAddressOne;

		private Label label6;

		private AshTextBoxNormal txtPhone;

		private AshTextBoxNormal txtAddressTwo;

		private Label label8;

		private Label label7;

		private AshTextBoxNormal txtMobile;

		private AshComboBox cmbPCategory;

		private Label label1;

		private AshTextBoxNormal txtRemarks;

		private AshComboBox cmbName;

		private AshComboBox cmbNominee;

		public UserPrevilegeDtl plgDtls { get; set; }

		public CustomerPrevilegeCard()
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
			AniHelper.FillPCategory(cmbPCategory);
			AniHelper.FillAccount(cmbName, isCashAccntIncluded: false);
			AniHelper.FillAccount(cmbNominee, isCashAccntIncluded: false);
			base.ActiveControl = txtCode;
			btnSearch.Visible = true;
		}

		private void ReloadMethod(PrevilegeCard previlegeCard)
		{
			txtCode.Text = previlegeCard.ConsumerCode;
			cmbName.SelectedValue = previlegeCard.CustomerId.GetValueOrDefault();
			cmbPCategory.SelectedValue = previlegeCard.PriceCategoryId.GetValueOrDefault();
			cmbNominee.SelectedValue = previlegeCard.NomineeAccountId.GetValueOrDefault();
			dtmExpairy.Value = previlegeCard.ExpairyDate;
			dtmRenewal.Value = previlegeCard.RenewalDate;
			dtmDOB.Value = previlegeCard.DOBofCustomer;
			txtRemarks.Text = previlegeCard.Remarks;
			iPrevilageCardId = previlegeCard.PrevilageCardId.toInt32();
			LoadTextBoxMethod();
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtCode.Focus();
			txtCode.SelectAll();
		}

		private PrevilegeCard EntityLoadMethod()
		{
			return new PrevilegeCard
			{
				PrevilageCardId = iPrevilageCardId,
				ConsumerCode = txtCode.Text,
				CustomerId = cmbName.SelectedValue.ToInt32(),
				PriceCategoryId = cmbPCategory.SelectedValue.ToInt32(),
				NomineeAccountId = cmbNominee.SelectedValue.ToInt32(),
				ExpairyDate = dtmExpairy.Value.Date,
				RenewalDate = dtmRenewal.Value.Date,
				DOBofCustomer = dtmDOB.Value.Date,
				Remarks = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void LoadTextBoxMethod()
		{
			AccountEntity accountEntity = (from x in db.Accounts()
				where x.AccountId == cmbName.SelectedValue.ToInt32()
				select x).FirstOrDefault();
			if (accountEntity != null)
			{
				txtAddressOne.Text = accountEntity.AddressOne;
				txtAddressTwo.Text = accountEntity.AddressTwo;
				txtMobile.Text = accountEntity.MobileNo;
				txtPhone.Text = accountEntity.PhoneOne;
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			AniHelper.ClearDateTimeMethod(this);
			btnDelete.Enabled = false;
			btnDelete.ForeColor = Color.Black;
			iPrevilageCardId = null;
			txtCode.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("PrevilegeCard", "PrevilageCardId", iPrevilageCardId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtCode.Text == string.Empty && cmbName.Text != string.Empty)
				{
					Messages.WarningMessage();
				}
				else if (!iPrevilageCardId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					PrevilegeCard data = EntityLoadMethod();
					if (AshproCommon.InsertToDatabaseObj(data, "PrevilegeCard"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "PrevilegeCard", "PrevilageCardId", iPrevilageCardId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dtmDOB_ValueChanged(object sender, EventArgs e)
		{
			int num = 0;
			num = DateTime.Now.Year - dtmDOB.Value.Year;
			if (DateTime.Now.DayOfYear < dtmDOB.Value.DayOfYear)
			{
				num--;
			}
			txtAge.Text = num.ToString();
		}

		private void txtAge_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTextBoxMethod();
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Customer Name", "Card Number");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select PrevilageCardId as id,AccountName as name,ConsumerCode as description From PrevilegeCard Join Account on PrevilegeCard.CustomerId=Account.AccountId");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.previlegeCard = this;
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

		public void DataloadFromSearch(MasterSerchForm frm)
		{
			PrevilegeCard previlegeCard = db.PrevilegeCards(new
			{
				PrevilageCardId = frm.id
			}).FirstOrDefault();
			ReloadMethod(previlegeCard);
			dtmDOB_ValueChanged(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerPrevilegeCard));
            this.dtmDOB = new AshUsercontrol.AshDateTimePicker();
            this.dtmExpairy = new AshUsercontrol.AshDateTimePicker();
            this.dtmRenewal = new AshUsercontrol.AshDateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAge = new AshUsercontrol.AshTextBoxNormal();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddressOne = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPhone = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressTwo = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMobile = new AshUsercontrol.AshTextBoxNormal();
            this.cmbPCategory = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.cmbName = new AshUsercontrol.AshComboBox();
            this.cmbNominee = new AshUsercontrol.AshComboBox();
            this.pnlButton.SuspendLayout();
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
            // dtmDOB
            // 
            resources.ApplyResources(this.dtmDOB, "dtmDOB");
            this.dtmDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDOB.Name = "dtmDOB";
            this.dtmDOB.ValueChanged += new System.EventHandler(this.dtmDOB_ValueChanged);
            // 
            // dtmExpairy
            // 
            resources.ApplyResources(this.dtmExpairy, "dtmExpairy");
            this.dtmExpairy.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmExpairy.Name = "dtmExpairy";
            // 
            // dtmRenewal
            // 
            resources.ApplyResources(this.dtmRenewal, "dtmRenewal");
            this.dtmRenewal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmRenewal.Name = "dtmRenewal";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtAge
            // 
            resources.ApplyResources(this.txtAge, "txtAge");
            this.txtAge.BackColor = System.Drawing.Color.White;
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAge.Data = 0;
            this.txtAge.EnterColor = System.Drawing.Color.LightPink;
            this.txtAge.Format = null;
            this.txtAge.isAllowNegative = false;
            this.txtAge.isAllowSpecialChar = false;
            this.txtAge.isNumberOnly = true;
            this.txtAge.isNumeric = false;
            this.txtAge.LeaveColor = System.Drawing.Color.White;
            this.txtAge.Name = "txtAge";
            this.txtAge.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Data = 0;
            this.txtCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtCode.Format = null;
            this.txtCode.isAllowNegative = false;
            this.txtCode.isAllowSpecialChar = false;
            this.txtCode.isNumberOnly = false;
            this.txtCode.isNumeric = false;
            this.txtCode.LeaveColor = System.Drawing.Color.White;
            this.txtCode.Name = "txtCode";
            this.txtCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtAddressOne
            // 
            resources.ApplyResources(this.txtAddressOne, "txtAddressOne");
            this.txtAddressOne.BackColor = System.Drawing.Color.White;
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
            this.txtAddressOne.ReadOnly = true;
            this.txtAddressOne.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtPhone
            // 
            resources.ApplyResources(this.txtPhone, "txtPhone");
            this.txtPhone.BackColor = System.Drawing.Color.White;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Data = 0;
            this.txtPhone.EnterColor = System.Drawing.Color.LightPink;
            this.txtPhone.Format = null;
            this.txtPhone.isAllowNegative = false;
            this.txtPhone.isAllowSpecialChar = false;
            this.txtPhone.isNumberOnly = false;
            this.txtPhone.isNumeric = false;
            this.txtPhone.LeaveColor = System.Drawing.Color.White;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddressTwo
            // 
            resources.ApplyResources(this.txtAddressTwo, "txtAddressTwo");
            this.txtAddressTwo.BackColor = System.Drawing.Color.White;
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
            this.txtAddressTwo.ReadOnly = true;
            this.txtAddressTwo.Value = new decimal(new int[] {
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
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtMobile
            // 
            resources.ApplyResources(this.txtMobile, "txtMobile");
            this.txtMobile.BackColor = System.Drawing.Color.White;
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
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbPCategory
            // 
            resources.ApplyResources(this.cmbPCategory, "cmbPCategory");
            this.cmbPCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPCategory.FormattingEnabled = true;
            this.cmbPCategory.Items.AddRange(new object[] {
            resources.GetString("cmbPCategory.Items"),
            resources.GetString("cmbPCategory.Items1")});
            this.cmbPCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbPCategory.Name = "cmbPCategory";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.Data = 0;
            this.txtRemarks.EnterColor = System.Drawing.Color.LightPink;
            this.txtRemarks.Format = null;
            this.txtRemarks.isAllowNegative = false;
            this.txtRemarks.isAllowSpecialChar = false;
            this.txtRemarks.isNumberOnly = false;
            this.txtRemarks.isNumeric = false;
            this.txtRemarks.LeaveColor = System.Drawing.Color.White;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAge_KeyDown);
            // 
            // cmbName
            // 
            resources.ApplyResources(this.cmbName, "cmbName");
            this.cmbName.EnterColor = System.Drawing.Color.LightPink;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Items.AddRange(new object[] {
            resources.GetString("cmbName.Items"),
            resources.GetString("cmbName.Items1")});
            this.cmbName.LeaveColor = System.Drawing.Color.White;
            this.cmbName.Name = "cmbName";
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            // 
            // cmbNominee
            // 
            resources.ApplyResources(this.cmbNominee, "cmbNominee");
            this.cmbNominee.EnterColor = System.Drawing.Color.LightPink;
            this.cmbNominee.FormattingEnabled = true;
            this.cmbNominee.Items.AddRange(new object[] {
            resources.GetString("cmbNominee.Items"),
            resources.GetString("cmbNominee.Items1")});
            this.cmbNominee.LeaveColor = System.Drawing.Color.White;
            this.cmbNominee.Name = "cmbNominee";
            // 
            // CustomerPrevilegeCard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.cmbNominee);
            this.Controls.Add(this.cmbPCategory);
            this.Controls.Add(this.dtmDOB);
            this.Controls.Add(this.dtmExpairy);
            this.Controls.Add(this.dtmRenewal);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAddressOne);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAddressTwo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMobile);
            this.MaximizeBox = false;
            this.Name = "CustomerPrevilegeCard";
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.txtMobile, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtAddressTwo, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtPhone, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtAddressOne, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.txtCode, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.txtAge, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.dtmRenewal, 0);
            this.Controls.SetChildIndex(this.dtmExpairy, 0);
            this.Controls.SetChildIndex(this.dtmDOB, 0);
            this.Controls.SetChildIndex(this.cmbPCategory, 0);
            this.Controls.SetChildIndex(this.cmbNominee, 0);
            this.Controls.SetChildIndex(this.cmbName, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
