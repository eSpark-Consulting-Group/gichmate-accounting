using System;
using System.Collections.Generic;
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
	public class BatchView : MasterForm
	{
		private DBContext db = new DBContext();

		private int? iMasterId;

		private IContainer components = null;

		private GroupBox groupBox1;

		private ListView lvDetails;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader3;

		private AshTextBoxNormal txtRemarks;

		private AshTextBoxNormal txtName;

		private Label label4;

		private Label label1;

		private AshTextBoxNormal txtSearchBatch;

		private Label label9;

		public UserPrevilegeDtl plgDtls { get; set; }

		public BatchView()
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

		private void LoadListViewMethod(int? iProId = null)
		{
			lvDetails.Items.Clear();
			try
			{
				List<Batch> list = new List<Batch>();
				list = ((!(txtSearchBatch.Text != string.Empty)) ? (from x in db.Batches()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList() : (from x in db.Batches()
					where x.Name.StartsWith(txtSearchBatch.Text) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList());
				foreach (Batch item in list)
				{
					ListViewItem listViewItem = new ListViewItem((lvDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.Name);
					lvDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Batch EntityLoadMethod()
		{
			return new Batch
			{
				Id = iMasterId,
				Name = txtName.Text,
				Remarks = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void BatchView_Load(object sender, EventArgs e)
		{
			LoadListViewMethod();
		}

		private void lvDetails_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvDetails.SelectedItems.Count != 0)
			{
				Batch batch = db.Batches().FirstOrDefault((Batch x) => x.Name == lvDetails.SelectedItems[0].SubItems[1].Text);
				txtRemarks.Text = batch.Remarks;
				txtName.Text = batch.Name;
				iMasterId = batch.Id;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
				txtName.Focus();
				txtName.SelectAll();
			}
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			LoadListViewMethod();
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
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Batch", "Id", iMasterId.toInt32()))
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
					return;
				}
				string text = txtName.Text;
				ListViewItem listViewItem = lvDetails.FindItemWithText(text);
				if (listViewItem != null)
				{
					Messages.ReferenceExistsMessage();
					txtName.Focus();
					txtName.SelectAll();
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
			else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "Batch", "Id", iMasterId.toInt32()))
			{
				Messages.UpdateMessage();
				btnClear_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearchBatch = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lvDetails);
            this.groupBox1.Controls.Add(this.txtSearchBatch);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lvDetails
            // 
            resources.ApplyResources(this.lvDetails, "lvDetails");
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.GridLines = true;
            this.lvDetails.HideSelection = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.SelectedIndexChanged += new System.EventHandler(this.lvDetails_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // txtSearchBatch
            // 
            resources.ApplyResources(this.txtSearchBatch, "txtSearchBatch");
            this.txtSearchBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchBatch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchBatch.Data = 0;
            this.txtSearchBatch.EnterColor = System.Drawing.Color.LightPink;
            this.txtSearchBatch.Format = null;
            this.txtSearchBatch.isAllowNegative = false;
            this.txtSearchBatch.isAllowSpecialChar = false;
            this.txtSearchBatch.isNumberOnly = false;
            this.txtSearchBatch.isNumeric = false;
            this.txtSearchBatch.LeaveColor = System.Drawing.Color.White;
            this.txtSearchBatch.Name = "txtSearchBatch";
            this.txtSearchBatch.Value = new decimal(new int[] {
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
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
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
            // BatchView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "BatchView";
            this.Load += new System.EventHandler(this.BatchView_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
