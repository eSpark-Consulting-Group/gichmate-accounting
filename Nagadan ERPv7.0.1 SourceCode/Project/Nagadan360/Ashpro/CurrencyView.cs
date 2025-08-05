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
	public class CurrencyView : MasterForm
	{
		private DBContext db = new DBContext();

		private int? iMasterId;

		private IContainer components = null;

		private AshTextBoxNormal currencyCodeTextBox;

		private AshTextBoxNormal txtName;

		private Label label4;

		private Label label1;

		private Label label2;

		private Label label3;

		private AshTextBoxNormal englishPluralCurrencyNameTextBox;

		private AshTextBoxNormal englishCurrencyPartNameTextBox;

		private Label label5;

		private Label label6;

		private AshTextBoxNormal englishPluralCurrencyPartNameTextBox;

		private AshTextBoxNormal arabic1CurrencyNameTextBox;

		private Label label7;

		private Label label8;

		private AshTextBoxNormal arabic2CurrencyNameTextBox;

		private AshTextBoxNormal arabic310CurrencyNameTextBox;

		private Label label9;

		private Label label10;

		private AshTextBoxNormal arabic1199CurrencyNameTextBox;

		private AshTextBoxNormal arabic1CurrencyPartNameTextBox;

		private Label label11;

		private Label label12;

		private AshTextBoxNormal arabic2CurrencyPartNameTextBox;

		private AshTextBoxNormal arabic310CurrencyPartNameTextBox;

		private Label label13;

		private Label label14;

		private AshTextBoxNormal arabic1199CurrencyPartNameTextBox;

		private AshTextBoxNormal partPrecisionTextBox;

		private CheckBox isCurrencyNameFeminineCheckBox;

		private CheckBox isCurrencyPartNameFeminineCheckBox;

		public UserPrevilegeDtl plgDtls { get; set; }

		public CurrencyView()
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
			base.ActiveControl = txtName;
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				AniHelper.ClearMethod(this);
				btnDelete.Enabled = false;
				iMasterId = null;
				txtName.Focus();
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
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				CurrencyInfo data = new CurrencyInfo
				{
					CurrencyID = iMasterId,
					CurrencyCode = currencyCodeTextBox.Text,
					IsCurrencyNameFeminine = isCurrencyNameFeminineCheckBox.Checked,
					EnglishCurrencyName = txtName.Text,
					EnglishPluralCurrencyName = englishPluralCurrencyNameTextBox.Text,
					EnglishCurrencyPartName = englishCurrencyPartNameTextBox.Text,
					EnglishPluralCurrencyPartName = englishPluralCurrencyPartNameTextBox.Text,
					Arabic1CurrencyName = arabic1CurrencyNameTextBox.Text,
					Arabic2CurrencyName = arabic2CurrencyNameTextBox.Text,
					Arabic310CurrencyName = arabic310CurrencyNameTextBox.Text,
					Arabic1199CurrencyName = arabic1199CurrencyNameTextBox.Text,
					Arabic1CurrencyPartName = arabic1CurrencyPartNameTextBox.Text,
					Arabic2CurrencyPartName = arabic2CurrencyPartNameTextBox.Text,
					Arabic310CurrencyPartName = arabic310CurrencyPartNameTextBox.Text,
					Arabic1199CurrencyPartName = arabic1199CurrencyPartNameTextBox.Text,
					PartPrecision = partPrecisionTextBox.Data,
					IsCurrencyPartNameFeminine = isCurrencyPartNameFeminineCheckBox.Checked
				};
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(data))
					{
						Messages.SavedMessage();
						btnClear.PerformClick();
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(data, "CurrencyInfo", "CurrencyID", iMasterId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear.PerformClick();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("CurrencyInfo", "CurrencyID", iMasterId.toInt32()))
			{
				btnClear_Click(null, null);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Currency Name", "Currency Short Name");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select CurrencyID as id,EnglishCurrencyName as name,CurrencyCode as description From CurrencyInfo");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.currency = this;
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
			CurrencyInfo currencyInfo = db.CurrencyInfos(new
			{
				CurrencyID = frm.id
			}).FirstOrDefault();
			iMasterId = currencyInfo.CurrencyID;
			currencyCodeTextBox.Text = currencyInfo.CurrencyCode;
			isCurrencyNameFeminineCheckBox.Checked = currencyInfo.IsCurrencyNameFeminine;
			txtName.Text = currencyInfo.EnglishCurrencyName;
			englishPluralCurrencyNameTextBox.Text = currencyInfo.EnglishPluralCurrencyName;
			englishCurrencyPartNameTextBox.Text = currencyInfo.EnglishCurrencyPartName;
			englishPluralCurrencyPartNameTextBox.Text = currencyInfo.EnglishPluralCurrencyPartName;
			arabic1CurrencyNameTextBox.Text = currencyInfo.Arabic1CurrencyName;
			arabic2CurrencyNameTextBox.Text = currencyInfo.Arabic2CurrencyName;
			arabic310CurrencyNameTextBox.Text = currencyInfo.Arabic310CurrencyName;
			arabic1199CurrencyNameTextBox.Text = currencyInfo.Arabic1199CurrencyName;
			arabic1CurrencyPartNameTextBox.Text = currencyInfo.Arabic1CurrencyPartName;
			arabic2CurrencyPartNameTextBox.Text = currencyInfo.Arabic2CurrencyPartName;
			arabic310CurrencyPartNameTextBox.Text = currencyInfo.Arabic310CurrencyPartName;
			arabic1199CurrencyPartNameTextBox.Text = currencyInfo.Arabic1199CurrencyPartName;
			partPrecisionTextBox.Data = currencyInfo.PartPrecision;
			isCurrencyPartNameFeminineCheckBox.Checked = currencyInfo.IsCurrencyPartNameFeminine;
			btnDelete.Enabled = true;
			txtName.Focus();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencyView));
            this.currencyCodeTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.englishPluralCurrencyNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.englishCurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.englishPluralCurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.arabic1CurrencyNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.arabic2CurrencyNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.arabic310CurrencyNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.arabic1199CurrencyNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.arabic1CurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.arabic2CurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.arabic310CurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.arabic1199CurrencyPartNameTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.partPrecisionTextBox = new AshUsercontrol.AshTextBoxNormal();
            this.isCurrencyNameFeminineCheckBox = new System.Windows.Forms.CheckBox();
            this.isCurrencyPartNameFeminineCheckBox = new System.Windows.Forms.CheckBox();
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
            this.btnSave.TabStop = false;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.TabStop = false;
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // currencyCodeTextBox
            // 
            resources.ApplyResources(this.currencyCodeTextBox, "currencyCodeTextBox");
            this.currencyCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.currencyCodeTextBox.Data = 0;
            this.currencyCodeTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.currencyCodeTextBox.Format = null;
            this.currencyCodeTextBox.isAllowNegative = false;
            this.currencyCodeTextBox.isAllowSpecialChar = false;
            this.currencyCodeTextBox.isNumberOnly = false;
            this.currencyCodeTextBox.isNumeric = false;
            this.currencyCodeTextBox.LeaveColor = System.Drawing.Color.White;
            this.currencyCodeTextBox.Name = "currencyCodeTextBox";
            this.currencyCodeTextBox.Value = new decimal(new int[] {
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // englishPluralCurrencyNameTextBox
            // 
            resources.ApplyResources(this.englishPluralCurrencyNameTextBox, "englishPluralCurrencyNameTextBox");
            this.englishPluralCurrencyNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.englishPluralCurrencyNameTextBox.Data = 0;
            this.englishPluralCurrencyNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.englishPluralCurrencyNameTextBox.Format = null;
            this.englishPluralCurrencyNameTextBox.isAllowNegative = false;
            this.englishPluralCurrencyNameTextBox.isAllowSpecialChar = false;
            this.englishPluralCurrencyNameTextBox.isNumberOnly = false;
            this.englishPluralCurrencyNameTextBox.isNumeric = false;
            this.englishPluralCurrencyNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.englishPluralCurrencyNameTextBox.Name = "englishPluralCurrencyNameTextBox";
            this.englishPluralCurrencyNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // englishCurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.englishCurrencyPartNameTextBox, "englishCurrencyPartNameTextBox");
            this.englishCurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.englishCurrencyPartNameTextBox.Data = 0;
            this.englishCurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.englishCurrencyPartNameTextBox.Format = null;
            this.englishCurrencyPartNameTextBox.isAllowNegative = false;
            this.englishCurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.englishCurrencyPartNameTextBox.isNumberOnly = false;
            this.englishCurrencyPartNameTextBox.isNumeric = false;
            this.englishCurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.englishCurrencyPartNameTextBox.Name = "englishCurrencyPartNameTextBox";
            this.englishCurrencyPartNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // englishPluralCurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.englishPluralCurrencyPartNameTextBox, "englishPluralCurrencyPartNameTextBox");
            this.englishPluralCurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.englishPluralCurrencyPartNameTextBox.Data = 0;
            this.englishPluralCurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.englishPluralCurrencyPartNameTextBox.Format = null;
            this.englishPluralCurrencyPartNameTextBox.isAllowNegative = false;
            this.englishPluralCurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.englishPluralCurrencyPartNameTextBox.isNumberOnly = false;
            this.englishPluralCurrencyPartNameTextBox.isNumeric = false;
            this.englishPluralCurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.englishPluralCurrencyPartNameTextBox.Name = "englishPluralCurrencyPartNameTextBox";
            this.englishPluralCurrencyPartNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // arabic1CurrencyNameTextBox
            // 
            resources.ApplyResources(this.arabic1CurrencyNameTextBox, "arabic1CurrencyNameTextBox");
            this.arabic1CurrencyNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic1CurrencyNameTextBox.Data = 0;
            this.arabic1CurrencyNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic1CurrencyNameTextBox.Format = null;
            this.arabic1CurrencyNameTextBox.isAllowNegative = false;
            this.arabic1CurrencyNameTextBox.isAllowSpecialChar = false;
            this.arabic1CurrencyNameTextBox.isNumberOnly = false;
            this.arabic1CurrencyNameTextBox.isNumeric = false;
            this.arabic1CurrencyNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic1CurrencyNameTextBox.Name = "arabic1CurrencyNameTextBox";
            this.arabic1CurrencyNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // arabic2CurrencyNameTextBox
            // 
            resources.ApplyResources(this.arabic2CurrencyNameTextBox, "arabic2CurrencyNameTextBox");
            this.arabic2CurrencyNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic2CurrencyNameTextBox.Data = 0;
            this.arabic2CurrencyNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic2CurrencyNameTextBox.Format = null;
            this.arabic2CurrencyNameTextBox.isAllowNegative = false;
            this.arabic2CurrencyNameTextBox.isAllowSpecialChar = false;
            this.arabic2CurrencyNameTextBox.isNumberOnly = false;
            this.arabic2CurrencyNameTextBox.isNumeric = false;
            this.arabic2CurrencyNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic2CurrencyNameTextBox.Name = "arabic2CurrencyNameTextBox";
            this.arabic2CurrencyNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // arabic310CurrencyNameTextBox
            // 
            resources.ApplyResources(this.arabic310CurrencyNameTextBox, "arabic310CurrencyNameTextBox");
            this.arabic310CurrencyNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic310CurrencyNameTextBox.Data = 0;
            this.arabic310CurrencyNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic310CurrencyNameTextBox.Format = null;
            this.arabic310CurrencyNameTextBox.isAllowNegative = false;
            this.arabic310CurrencyNameTextBox.isAllowSpecialChar = false;
            this.arabic310CurrencyNameTextBox.isNumberOnly = false;
            this.arabic310CurrencyNameTextBox.isNumeric = false;
            this.arabic310CurrencyNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic310CurrencyNameTextBox.Name = "arabic310CurrencyNameTextBox";
            this.arabic310CurrencyNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // arabic1199CurrencyNameTextBox
            // 
            resources.ApplyResources(this.arabic1199CurrencyNameTextBox, "arabic1199CurrencyNameTextBox");
            this.arabic1199CurrencyNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic1199CurrencyNameTextBox.Data = 0;
            this.arabic1199CurrencyNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic1199CurrencyNameTextBox.Format = null;
            this.arabic1199CurrencyNameTextBox.isAllowNegative = false;
            this.arabic1199CurrencyNameTextBox.isAllowSpecialChar = false;
            this.arabic1199CurrencyNameTextBox.isNumberOnly = false;
            this.arabic1199CurrencyNameTextBox.isNumeric = false;
            this.arabic1199CurrencyNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic1199CurrencyNameTextBox.Name = "arabic1199CurrencyNameTextBox";
            this.arabic1199CurrencyNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // arabic1CurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.arabic1CurrencyPartNameTextBox, "arabic1CurrencyPartNameTextBox");
            this.arabic1CurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic1CurrencyPartNameTextBox.Data = 0;
            this.arabic1CurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic1CurrencyPartNameTextBox.Format = null;
            this.arabic1CurrencyPartNameTextBox.isAllowNegative = false;
            this.arabic1CurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.arabic1CurrencyPartNameTextBox.isNumberOnly = false;
            this.arabic1CurrencyPartNameTextBox.isNumeric = false;
            this.arabic1CurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic1CurrencyPartNameTextBox.Name = "arabic1CurrencyPartNameTextBox";
            this.arabic1CurrencyPartNameTextBox.Value = new decimal(new int[] {
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
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // arabic2CurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.arabic2CurrencyPartNameTextBox, "arabic2CurrencyPartNameTextBox");
            this.arabic2CurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic2CurrencyPartNameTextBox.Data = 0;
            this.arabic2CurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic2CurrencyPartNameTextBox.Format = null;
            this.arabic2CurrencyPartNameTextBox.isAllowNegative = false;
            this.arabic2CurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.arabic2CurrencyPartNameTextBox.isNumberOnly = false;
            this.arabic2CurrencyPartNameTextBox.isNumeric = false;
            this.arabic2CurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic2CurrencyPartNameTextBox.Name = "arabic2CurrencyPartNameTextBox";
            this.arabic2CurrencyPartNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // arabic310CurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.arabic310CurrencyPartNameTextBox, "arabic310CurrencyPartNameTextBox");
            this.arabic310CurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic310CurrencyPartNameTextBox.Data = 0;
            this.arabic310CurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic310CurrencyPartNameTextBox.Format = null;
            this.arabic310CurrencyPartNameTextBox.isAllowNegative = false;
            this.arabic310CurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.arabic310CurrencyPartNameTextBox.isNumberOnly = false;
            this.arabic310CurrencyPartNameTextBox.isNumeric = false;
            this.arabic310CurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic310CurrencyPartNameTextBox.Name = "arabic310CurrencyPartNameTextBox";
            this.arabic310CurrencyPartNameTextBox.Value = new decimal(new int[] {
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
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // arabic1199CurrencyPartNameTextBox
            // 
            resources.ApplyResources(this.arabic1199CurrencyPartNameTextBox, "arabic1199CurrencyPartNameTextBox");
            this.arabic1199CurrencyPartNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.arabic1199CurrencyPartNameTextBox.Data = 0;
            this.arabic1199CurrencyPartNameTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.arabic1199CurrencyPartNameTextBox.Format = null;
            this.arabic1199CurrencyPartNameTextBox.isAllowNegative = false;
            this.arabic1199CurrencyPartNameTextBox.isAllowSpecialChar = false;
            this.arabic1199CurrencyPartNameTextBox.isNumberOnly = false;
            this.arabic1199CurrencyPartNameTextBox.isNumeric = false;
            this.arabic1199CurrencyPartNameTextBox.LeaveColor = System.Drawing.Color.White;
            this.arabic1199CurrencyPartNameTextBox.Name = "arabic1199CurrencyPartNameTextBox";
            this.arabic1199CurrencyPartNameTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // partPrecisionTextBox
            // 
            resources.ApplyResources(this.partPrecisionTextBox, "partPrecisionTextBox");
            this.partPrecisionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.partPrecisionTextBox.Data = 0;
            this.partPrecisionTextBox.EnterColor = System.Drawing.Color.LightPink;
            this.partPrecisionTextBox.Format = null;
            this.partPrecisionTextBox.isAllowNegative = false;
            this.partPrecisionTextBox.isAllowSpecialChar = false;
            this.partPrecisionTextBox.isNumberOnly = true;
            this.partPrecisionTextBox.isNumeric = false;
            this.partPrecisionTextBox.LeaveColor = System.Drawing.Color.White;
            this.partPrecisionTextBox.Name = "partPrecisionTextBox";
            this.partPrecisionTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // isCurrencyNameFeminineCheckBox
            // 
            resources.ApplyResources(this.isCurrencyNameFeminineCheckBox, "isCurrencyNameFeminineCheckBox");
            this.isCurrencyNameFeminineCheckBox.Name = "isCurrencyNameFeminineCheckBox";
            this.isCurrencyNameFeminineCheckBox.UseVisualStyleBackColor = true;
            // 
            // isCurrencyPartNameFeminineCheckBox
            // 
            resources.ApplyResources(this.isCurrencyPartNameFeminineCheckBox, "isCurrencyPartNameFeminineCheckBox");
            this.isCurrencyPartNameFeminineCheckBox.Checked = true;
            this.isCurrencyPartNameFeminineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isCurrencyPartNameFeminineCheckBox.Name = "isCurrencyPartNameFeminineCheckBox";
            this.isCurrencyPartNameFeminineCheckBox.UseVisualStyleBackColor = true;
            // 
            // CurrencyView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.isCurrencyPartNameFeminineCheckBox);
            this.Controls.Add(this.isCurrencyNameFeminineCheckBox);
            this.Controls.Add(this.partPrecisionTextBox);
            this.Controls.Add(this.arabic310CurrencyPartNameTextBox);
            this.Controls.Add(this.arabic1CurrencyPartNameTextBox);
            this.Controls.Add(this.arabic310CurrencyNameTextBox);
            this.Controls.Add(this.arabic1CurrencyNameTextBox);
            this.Controls.Add(this.englishCurrencyPartNameTextBox);
            this.Controls.Add(this.currencyCodeTextBox);
            this.Controls.Add(this.arabic1199CurrencyPartNameTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.arabic2CurrencyPartNameTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.arabic1199CurrencyNameTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.arabic2CurrencyNameTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.englishPluralCurrencyPartNameTextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.englishPluralCurrencyNameTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "CurrencyView";
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.englishPluralCurrencyNameTextBox, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.englishPluralCurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.arabic2CurrencyNameTextBox, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.arabic1199CurrencyNameTextBox, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.arabic2CurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.arabic1199CurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.currencyCodeTextBox, 0);
            this.Controls.SetChildIndex(this.englishCurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.arabic1CurrencyNameTextBox, 0);
            this.Controls.SetChildIndex(this.arabic310CurrencyNameTextBox, 0);
            this.Controls.SetChildIndex(this.arabic1CurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.arabic310CurrencyPartNameTextBox, 0);
            this.Controls.SetChildIndex(this.partPrecisionTextBox, 0);
            this.Controls.SetChildIndex(this.isCurrencyNameFeminineCheckBox, 0);
            this.Controls.SetChildIndex(this.isCurrencyPartNameFeminineCheckBox, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
