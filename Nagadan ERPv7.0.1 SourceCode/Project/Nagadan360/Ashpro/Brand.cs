using System;
using System.ComponentModel;
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
	public class Brand : MasterForm
	{
		private int? iBrandId;

		private DBContext db;

		private IContainer components = null;

		private AshTextBoxNormal txtDescription;

		private ListView lvDetails;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Label label1;

		private AshTextBoxNormal txtName;

		private Label label4;

		private ColumnHeader columnHeader5;

		public bool isOpenFromAnotherForm { get; set; }

		public string brandName { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public Brand()
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
			base.ActiveControl = txtName;
		}

		private void LoadListViewMethod()
		{
			lvDetails.Items.Clear();
			try
			{
				foreach (BrandEntity item in db.Brands().ToList())
				{
					ListViewItem listViewItem = new ListViewItem((lvDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.BrandName);
					listViewItem.SubItems.Add(item.Description);
					lvDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private BrandEntity EntityLoadMethod()
		{
			return new BrandEntity
			{
				BrandId = iBrandId,
				BrandName = txtName.Text,
				Description = txtDescription.Text
			};
		}

		private void livBrand_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvDetails.SelectedItems.Count != 0)
			{
				BrandEntity brandEntity = (from x in db.Brands()
					where x.BrandName == lvDetails.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtName.Text = brandEntity.BrandName;
				txtDescription.Text = brandEntity.Description;
				iBrandId = brandEntity.BrandId;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
				txtName.Focus();
				txtName.SelectAll();
			}
		}

		private void Brand_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			LoadListViewMethod();
			if (isOpenFromAnotherForm)
			{
				lvDetails.Enabled = false;
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			txtName.Text = string.Empty;
			txtDescription.Text = string.Empty;
			LoadListViewMethod();
			iBrandId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Brand", "BrandId", iBrandId.ToInt32()))
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
			else if (!iBrandId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					return;
				}
				string text = txtName.Text;
				ListViewItem listViewItem = lvDetails.FindItemWithText(text);
				if (listViewItem != null)
				{
					Messages.ReferenceExistsMessage();
					txtName.Focus();
					txtName.SelectAll();
					return;
				}
				if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod(), "Brand"))
				{
					Messages.SavedMessage();
					btnClear_Click(null, null);
				}
				if (isOpenFromAnotherForm)
				{
					base.DialogResult = DialogResult.OK;
					brandName = txtName.Text;
					Close();
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "Brand", "BrandId", iBrandId.ToInt32()))
			{
				Messages.UpdateMessage();
				btnClear_Click(null, null);
			}
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Brand));
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
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
            // lvDetails
            // 
            resources.ApplyResources(this.lvDetails, "lvDetails");
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2});
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.GridLines = true;
            this.lvDetails.HideSelection = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.SelectedIndexChanged += new System.EventHandler(this.livBrand_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
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
            // Brand
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lvDetails);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Brand";
            this.Load += new System.EventHandler(this.Brand_Load);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.lvDetails, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
