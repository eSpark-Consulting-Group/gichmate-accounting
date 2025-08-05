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
	public class Unit : MasterForm
	{
		private int? intUnitId;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private GroupBox groupBox1;

		private AshTextBoxNormal txtName;

		private Label label2;

		private DataGridView dgDetails;

		private DataGridViewTextBoxColumn Col_UnitName;

		private DataGridViewTextBoxColumn col_UnitNo;

		private AshTextBoxNormal txtUnitNo;

		public string UnitName { get; set; }

		public bool isOpenFromAnotherForm { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public Unit()
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
		}

		private void loadItems()
		{
			dgDetails.AutoGenerateColumns = false;
			dgDetails.DataSource = db.Units().ToList();
		}

		private void Unit_Load(object sender, EventArgs e)
		{
			loadItems();
			base.ActiveControl = txtName;
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
				UnitEntity data = new UnitEntity
				{
					UnitId = intUnitId,
					UnitName = txtName.Text,
					UnitNo = txtUnitNo.Data
				};
				if (!intUnitId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(data, "Unit"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(data, "Unit", "UnitId", intUnitId.ToInt32()))
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
			loadItems();
			intUnitId = null;
			txtName.Text = string.Empty;
			txtUnitNo.Data = 0;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Unit", "UnitId", intUnitId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				UnitEntity unitEntity = (UnitEntity)dgDetails.CurrentRow.DataBoundItem;
				txtName.Text = unitEntity.UnitName;
				txtUnitNo.Data = unitEntity.UnitNo.toInt32();
				intUnitId = unitEntity.UnitId.toInt32();
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
				txtName.Focus();
				txtName.SelectAll();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Unit));
			groupBox1 = new System.Windows.Forms.GroupBox();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			txtUnitNo = new AshUsercontrol.AshTextBoxNormal();
			dgDetails = new System.Windows.Forms.DataGridView();
			Col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_UnitNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pnlButton.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			SuspendLayout();
			resources.ApplyResources(pnlButton, "pnlButton");
			resources.ApplyResources(pnlMaster, "pnlMaster");
			resources.ApplyResources(lblMasterTitle, "lblMasterTitle");
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.TabStop = false;
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.TabStop = false;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.TabStop = false;
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnSearch, "btnSearch");
			btnSearch.TabStop = false;
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			groupBox1.BackColor = System.Drawing.Color.White;
			groupBox1.Controls.Add(dgDetails);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtName, "txtName");
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Name = "txtName";
			txtName.Value = new decimal(new int[4]);
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			txtUnitNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtUnitNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtUnitNo.Data = 0;
			txtUnitNo.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtUnitNo, "txtUnitNo");
			txtUnitNo.Format = null;
			txtUnitNo.isAllowNegative = false;
			txtUnitNo.isAllowSpecialChar = false;
			txtUnitNo.isNumberOnly = true;
			txtUnitNo.isNumeric = false;
			txtUnitNo.LeaveColor = System.Drawing.Color.White;
			txtUnitNo.Name = "txtUnitNo";
			txtUnitNo.Value = new decimal(new int[4]);
			txtUnitNo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtName_KeyDown);
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(Col_UnitName, col_UnitNo);
			resources.ApplyResources(dgDetails, "dgDetails");
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellContentClick);
			Col_UnitName.DataPropertyName = "UnitName";
			resources.ApplyResources(Col_UnitName, "Col_UnitName");
			Col_UnitName.Name = "Col_UnitName";
			Col_UnitName.ReadOnly = true;
			col_UnitNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_UnitNo.DataPropertyName = "UnitNo";
			resources.ApplyResources(col_UnitNo, "col_UnitNo");
			col_UnitNo.Name = "col_UnitNo";
			col_UnitNo.ReadOnly = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(groupBox1);
			base.Controls.Add(txtUnitNo);
			base.Controls.Add(txtName);
			base.Controls.Add(label2);
			base.MaximizeBox = false;
			base.Name = "Unit";
			base.Load += new System.EventHandler(Unit_Load);
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(txtUnitNo, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
