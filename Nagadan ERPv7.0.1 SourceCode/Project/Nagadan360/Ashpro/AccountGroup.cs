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
	public class AccountGroup : MasterForm
	{
		private int? iAGroupId;

		private DBContext db;

		private IContainer components = null;

		private GroupBox grpSearch;

		private ListView lvDetails;

		private ColumnHeader SrchAccountGroup;

		private ColumnHeader Under;

		private Label label3;

		private Label label2;

		private ColumnHeader columnHeader4;

		private AshTextBoxNormal txtName;

		private AshComboBox cmbUnder;

		private Panel panel1;

		public string sAccountGroup { get; set; }

		public bool isFromAccount { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public AccountGroup()
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
				foreach (AccountGroupEntity item in db.AccountGroup())
				{
					ListViewItem listViewItem = new ListViewItem((lvDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.GroupName);
					listViewItem.SubItems.Add(item.Under);
					lvDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private AccountGroupEntity EntityLoadMethod()
		{
			return new AccountGroupEntity
			{
				AGroupId = iAGroupId,
				GroupName = txtName.Text,
				Under = cmbUnder.Text,
				Remarks = string.Empty,
				isBuilIn = false
			};
		}

		private void AccountGroup_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillAGroup(cmbUnder);
			LoadListViewMethod();
			if (isFromAccount)
			{
				lvDetails.Enabled = false;
				cmbUnder.Text = sAccountGroup;
			}
		}

		private void lvDetails_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvDetails.SelectedItems.Count != 0)
			{
				AccountGroupEntity accountGroupEntity = (from x in db.AccountGroup()
					where x.GroupName == lvDetails.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtName.Text = accountGroupEntity.GroupName;
				cmbUnder.Text = accountGroupEntity.Under;
				iAGroupId = accountGroupEntity.AGroupId;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			txtName.Text = string.Empty;
			LoadListViewMethod();
			iAGroupId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
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
				if (!iAGroupId.HasValue)
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
						MessageBox.Show("Already Exist", Utility.uManufacturer);
						txtName.Focus();
						txtName.SelectAll();
						return;
					}
					if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod(), "AccountGroup"))
					{
						Messages.SavedMessage();
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "AccountGroup", "AGroupId", iAGroupId.toInt32()))
					{
						Messages.UpdateMessage();
					}
				}
				if (isFromAccount)
				{
					sAccountGroup = txtName.Text;
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
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
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if ((from x in db.AccountGroup()
				where x.AGroupId == iAGroupId
				select x).FirstOrDefault().isBuilIn)
			{
				Messages.BuiltinMessage();
				btnClear_Click(null, null);
			}
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("AccountGroup", "AGroupId", iAGroupId.toInt32()))
			{
				btnClear_Click(null, null);
			}
		}

		private void cmbUnder_KeyDown(object sender, KeyEventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountGroup));
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SrchAccountGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Under = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.cmbUnder = new AshUsercontrol.AshComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlButton.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // grpSearch
            // 
            resources.ApplyResources(this.grpSearch, "grpSearch");
            this.grpSearch.BackColor = System.Drawing.Color.White;
            this.grpSearch.Controls.Add(this.lvDetails);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.TabStop = false;
            // 
            // lvDetails
            // 
            resources.ApplyResources(this.lvDetails, "lvDetails");
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.SrchAccountGroup,
            this.Under});
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.GridLines = true;
            this.lvDetails.HideSelection = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.SelectedIndexChanged += new System.EventHandler(this.lvDetails_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // SrchAccountGroup
            // 
            resources.ApplyResources(this.SrchAccountGroup, "SrchAccountGroup");
            // 
            // Under
            // 
            resources.ApplyResources(this.Under, "Under");
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
            // cmbUnder
            // 
            resources.ApplyResources(this.cmbUnder, "cmbUnder");
            this.cmbUnder.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnder.FormattingEnabled = true;
            this.cmbUnder.LeaveColor = System.Drawing.Color.White;
            this.cmbUnder.Name = "cmbUnder";
            this.cmbUnder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnder_KeyDown);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbUnder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.grpSearch);
            this.panel1.Name = "panel1";
            // 
            // AccountGroup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "AccountGroup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AccountGroup_Load);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.pnlButton.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
