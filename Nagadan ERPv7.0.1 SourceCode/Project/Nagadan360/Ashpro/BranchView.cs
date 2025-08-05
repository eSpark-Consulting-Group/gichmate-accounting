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
	public class BranchView : MasterForm
	{
		private DBContext db = new DBContext();

		private int? iMasterId;

		private IContainer components = null;

		private AshTextBoxNormal txtAddress;

		private AshTextBoxNormal txtName;

		private Label label4;

		private Label label1;

		private AshTextBoxNormal txtState;

		private AshTextBoxNormal txtCity;

		private Label label2;

		private Label label3;

		private AshTextBoxNormal txtCountry;

		private Label label5;

		private AshTextBoxNormal txtDescription;

		private Label label6;

		private AshTextBoxNormal txtContactPerson;

		private AshTextBoxNormal txtMobileNo;

		private Label label7;

		private Label label8;

		private AshTextBoxNormal txtPhoneNo;

		private AshTextBoxNormal txtPinCode;

		private Label label9;

		private Label label10;

		private CheckBox cbActive;

		public UserPrevilegeDtl plgDtls { get; set; }

		public BranchView()
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

		private Branch EntityLoadMethod()
		{
			return new Branch
			{
				Id = iMasterId,
				Name = txtName.Text,
				Address = txtAddress.Text,
				City = txtCity.Text,
				State = txtState.Text,
				Country = txtCountry.Text,
				PostalCode = txtPinCode.Text,
				PhoneNumber = txtPhoneNo.Text,
				MobileNumber = txtMobileNo.Text,
				ContactPerson = txtContactPerson.Text,
				Description = txtDescription.Text,
				Active = cbActive.Checked
			};
		}

		public void ReloadMethod(MasterSerchForm frm)
		{
			Branch branch = db.Branches(new
			{
				Id = frm.id
			}).FirstOrDefault();
			txtName.Text = branch.Name;
			txtAddress.Text = branch.Address;
			txtCity.Text = branch.City;
			txtState.Text = branch.State;
			txtCountry.Text = branch.Country;
			txtPinCode.Text = branch.PostalCode;
			txtPhoneNo.Text = branch.PhoneNumber;
			txtMobileNo.Text = branch.MobileNumber;
			txtContactPerson.Text = branch.ContactPerson;
			txtDescription.Text = branch.Description;
			cbActive.Checked = branch.Active;
			iMasterId = branch.Id;
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtName.Focus();
			txtName.SelectAll();
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			iMasterId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Branch", "Id", iMasterId.toInt32()))
			{
				btnClear_Click(null, null);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text == string.Empty)
			{
				Messages.WarningMessage();
			}
			else if (!iMasterId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
				}
				else if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod()))
				{
					Messages.SavedMessage();
					btnClear_Click(null, null);
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "Branch", "Id", iMasterId.toInt32()))
			{
				Messages.UpdateMessage();
				btnClear_Click(null, null);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Branch Name", "Address");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select Id as id,Name as name,Address as description From Branch");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.branch = this;
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

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave.PerformClick();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchView));
            this.txtAddress = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtState = new AshUsercontrol.AshTextBoxNormal();
            this.txtCity = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCountry = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.txtContactPerson = new AshUsercontrol.AshTextBoxNormal();
            this.txtMobileNo = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhoneNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtPinCode = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.CheckBox();
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
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Data = 0;
            this.txtAddress.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddress.Format = null;
            this.txtAddress.isAllowNegative = false;
            this.txtAddress.isAllowSpecialChar = false;
            this.txtAddress.isNumberOnly = false;
            this.txtAddress.isNumeric = false;
            this.txtAddress.LeaveColor = System.Drawing.Color.White;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // txtState
            // 
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtState.Data = 0;
            this.txtState.EnterColor = System.Drawing.Color.LightPink;
            this.txtState.Format = null;
            this.txtState.isAllowNegative = false;
            this.txtState.isAllowSpecialChar = false;
            this.txtState.isNumberOnly = false;
            this.txtState.isNumeric = false;
            this.txtState.LeaveColor = System.Drawing.Color.White;
            this.txtState.Name = "txtState";
            this.txtState.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCity.Data = 0;
            this.txtCity.EnterColor = System.Drawing.Color.LightPink;
            this.txtCity.Format = null;
            this.txtCity.isAllowNegative = false;
            this.txtCity.isAllowSpecialChar = false;
            this.txtCity.isNumberOnly = false;
            this.txtCity.isNumeric = false;
            this.txtCity.LeaveColor = System.Drawing.Color.White;
            this.txtCity.Name = "txtCity";
            this.txtCity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCountry.Data = 0;
            this.txtCountry.EnterColor = System.Drawing.Color.LightPink;
            this.txtCountry.Format = null;
            this.txtCountry.isAllowNegative = false;
            this.txtCountry.isAllowSpecialChar = false;
            this.txtCountry.isNumberOnly = false;
            this.txtCountry.isNumeric = false;
            this.txtCountry.LeaveColor = System.Drawing.Color.White;
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Value = new decimal(new int[] {
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
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtContactPerson
            // 
            resources.ApplyResources(this.txtContactPerson, "txtContactPerson");
            this.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContactPerson.Data = 0;
            this.txtContactPerson.EnterColor = System.Drawing.Color.LightPink;
            this.txtContactPerson.Format = null;
            this.txtContactPerson.isAllowNegative = false;
            this.txtContactPerson.isAllowSpecialChar = false;
            this.txtContactPerson.isNumberOnly = false;
            this.txtContactPerson.isNumeric = false;
            this.txtContactPerson.LeaveColor = System.Drawing.Color.White;
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMobileNo
            // 
            resources.ApplyResources(this.txtMobileNo, "txtMobileNo");
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMobileNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobileNo.Data = 0;
            this.txtMobileNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtMobileNo.Format = null;
            this.txtMobileNo.isAllowNegative = false;
            this.txtMobileNo.isAllowSpecialChar = false;
            this.txtMobileNo.isNumberOnly = false;
            this.txtMobileNo.isNumeric = false;
            this.txtMobileNo.LeaveColor = System.Drawing.Color.White;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Value = new decimal(new int[] {
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
            // txtPinCode
            // 
            resources.ApplyResources(this.txtPinCode, "txtPinCode");
            this.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPinCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPinCode.Data = 0;
            this.txtPinCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtPinCode.Format = null;
            this.txtPinCode.isAllowNegative = false;
            this.txtPinCode.isAllowSpecialChar = false;
            this.txtPinCode.isNumberOnly = false;
            this.txtPinCode.isNumeric = false;
            this.txtPinCode.LeaveColor = System.Drawing.Color.White;
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.Value = new decimal(new int[] {
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
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // BranchView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtMobileNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPhoneNo);
            this.Controls.Add(this.txtPinCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCountry);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "BranchView";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtCity, 0);
            this.Controls.SetChildIndex(this.txtState, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtCountry, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtPinCode, 0);
            this.Controls.SetChildIndex(this.txtPhoneNo, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtMobileNo, 0);
            this.Controls.SetChildIndex(this.txtContactPerson, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.cbActive, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
