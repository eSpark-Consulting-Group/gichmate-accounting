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
	public class Godown : MasterForm
	{
		private int? iGodwnId;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private AshTextBoxNormal txtAddressOne;

		private Label label1;

		private AshTextBoxNormal txtName;

		private Label label4;

		private AshTextBoxNormal txtDescription;

		private AshTextBoxNormal txtAddressThree;

		private Label label6;

		private Label label3;

		private AshTextBoxNormal txtPhoneNo;

		private AshTextBoxNormal txtAddressTwo;

		private Label label5;

		private Label label2;

		public bool isOpenFromAnotherForm { get; set; }

		public string godownName { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public Godown()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
			base.ActiveControl = txtName;
		}

		private GodownEntity EntityMethod()
		{
			return new GodownEntity
			{
				GodwnId = iGodwnId,
				GodownName = txtName.Text,
				AddressOne = txtAddressOne.Text,
				AddressTwo = txtAddressTwo.Text,
				AddressThree = txtAddressThree.Text,
				Phone = txtPhoneNo.Text,
				Description = txtDescription.Text
			};
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
				}
				else if (!iGodwnId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(EntityMethod(), "Godown"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntityMethod(), "Godown", "GodwnId", iGodwnId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Godown", "GodwnId", iGodwnId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			txtName.Focus();
			btnDelete.Enabled = false;
			iGodwnId = null;
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Depot Name", "Address");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select GodwnId as id,GodownName as name,AddressOne as description From Godown");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.godown = this;
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

		public void DataLoadFromSearch(MasterSerchForm frm)
		{
			GodownEntity godownEntity = db.Godowns(new
			{
				GodwnId = frm.id
			}).FirstOrDefault();
			txtName.Text = godownEntity.GodownName;
			txtAddressOne.Text = godownEntity.AddressOne;
			txtAddressTwo.Text = godownEntity.AddressTwo;
			txtAddressThree.Text = godownEntity.AddressThree;
			txtPhoneNo.Text = godownEntity.Phone;
			txtDescription.Text = godownEntity.Description;
			iGodwnId = godownEntity.GodwnId;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Godown));
            this.txtAddressOne = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressThree = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhoneNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddressTwo = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // txtAddressOne
            // 
            resources.ApplyResources(this.txtAddressOne, "txtAddressOne");
            this.txtAddressOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressOne.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // txtAddressThree
            // 
            resources.ApplyResources(this.txtAddressThree, "txtAddressThree");
            this.txtAddressThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressThree.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // txtAddressTwo
            // 
            resources.ApplyResources(this.txtAddressTwo, "txtAddressTwo");
            this.txtAddressTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressTwo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Godown
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtAddressThree);
            this.Controls.Add(this.txtAddressOne);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddressTwo);
            this.Controls.Add(this.txtPhoneNo);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Godown";
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtPhoneNo, 0);
            this.Controls.SetChildIndex(this.txtAddressTwo, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtAddressOne, 0);
            this.Controls.SetChildIndex(this.txtAddressThree, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
