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
	public class NoteMasterView : MasterForm
	{
		private int? iMasterId;

		private DBContext db = new DBContext();

		private ItemNote entItemNote;

		private IContainer components = null;

		private Label label1;

		private AshTextBoxNormal txtNotes;

		private CheckBox cbMain;

		private DataGridView dgDetails;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn col_Note;
        private DataGridViewCheckBoxColumn col_Main;
        private BindingSource bsDetails;

		public string Notes { get; set; }

		public bool isOpenFromAnotherForm { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public NoteMasterView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			btnSearch.Visible = false;
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
		}

		private void LoadDGV(List<ItemNote> entNotes)
		{
			List<ExItemNote> list = new List<ExItemNote>();
			list = AniHelper.CopyListData<ExItemNote>(entNotes.Cast<object>().ToList());
			int iSL = 1;
			list.ForEach(delegate(ExItemNote x)
			{
				x.SL = iSL;
				iSL++;
			});
			dgDetails.AutoGenerateColumns = false;
			bsDetails.DataSource = null;
			dgDetails.DataSource = null;
			bsDetails.DataSource = list;
			dgDetails.DataSource = bsDetails;
		}

		private void NoteMasterView_Load(object sender, EventArgs e)
		{
			try
			{
				LoadDGV(db.ItemNotes());
				base.ActiveControl = txtNotes;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				entItemNote = (ItemNote)dgDetails.CurrentRow.DataBoundItem;
				txtNotes.Text = entItemNote.Note;
				cbMain.Checked = entItemNote.IsMain;
				iMasterId = entItemNote.Id;
				btnDelete.Enabled = true;
				txtNotes.Focus();
			}
		}

		private void cbMain_KeyDown(object sender, KeyEventArgs e)
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
				if (txtNotes.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				ItemNote data = new ItemNote
				{
					Id = iMasterId,
					Note = txtNotes.Text,
					IsMain = cbMain.Checked
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
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(data, "ItemNote", "Id", iMasterId.ToInt32()))
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

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			LoadDGV(db.ItemNotes().ToList());
			iMasterId = null;
			txtNotes.Text = string.Empty;
			btnDelete.Enabled = false;
			txtNotes.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("ItemNote", "Id", iMasterId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteMasterView));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNotes = new AshUsercontrol.AshTextBoxNormal();
            this.cbMain = new System.Windows.Forms.CheckBox();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Main = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetails)).BeginInit();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes.Data = 0;
            this.txtNotes.EnterColor = System.Drawing.Color.LightPink;
            this.txtNotes.Format = null;
            this.txtNotes.isAllowNegative = false;
            this.txtNotes.isAllowSpecialChar = false;
            this.txtNotes.isNumberOnly = false;
            this.txtNotes.isNumeric = false;
            this.txtNotes.LeaveColor = System.Drawing.Color.White;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cbMain
            // 
            resources.ApplyResources(this.cbMain, "cbMain");
            this.cbMain.Name = "cbMain";
            this.cbMain.UseVisualStyleBackColor = true;
            this.cbMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbMain_KeyDown);
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_Note,
            this.col_Main});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.RowTemplate.Height = 24;
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_Note
            // 
            this.col_Note.DataPropertyName = "Note";
            resources.ApplyResources(this.col_Note, "col_Note");
            this.col_Note.Name = "col_Note";
            this.col_Note.ReadOnly = true;
            // 
            // col_Main
            // 
            this.col_Main.DataPropertyName = "IsMain";
            resources.ApplyResources(this.col_Main, "col_Main");
            this.col_Main.Name = "col_Main";
            this.col_Main.ReadOnly = true;
            // 
            // NoteMasterView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.cbMain);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label1);
            this.Name = "NoteMasterView";
            this.Load += new System.EventHandler(this.NoteMasterView_Load);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtNotes, 0);
            this.Controls.SetChildIndex(this.cbMain, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
