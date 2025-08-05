using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class SettingsView : BaseForm
	{
		private List<SettingsEntity> entSettings = new List<SettingsEntity>();

		private DBContext db = new DBContext();

		private string strConnection;

		private IContainer components = null;

		private DataGridView dgvSettings;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewCheckBoxColumn Column2;

		private Label lblTitle;

		private BindingSource bdsSettings;

		private Button btnSave;

		public SettingsView()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			ColorChange.Color(this);
		}

		private void SettingsView_Load(object sender, EventArgs e)
		{
			strConnection = Settings.Default.strConnetionString;
			entSettings = db.Settings(new
			{
				Remarks = "Importent"
			}).ToList();
			dgvSettings.AutoGenerateColumns = false;
			bdsSettings.DataSource = null;
			dgvSettings.DataSource = null;
			bdsSettings.DataSource = entSettings;
			dgvSettings.DataSource = bdsSettings;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (CommonSettings.UpdateSettings(entSettings, strConnection))
			{
				Messages.SavedMessage();
				Close();
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
			components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			dgvSettings = new System.Windows.Forms.DataGridView();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			lblTitle = new System.Windows.Forms.Label();
			bdsSettings = new System.Windows.Forms.BindingSource(components);
			btnSave = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)dgvSettings).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsSettings).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(408, 0);
			dgvSettings.AllowUserToAddRows = false;
			dgvSettings.AllowUserToDeleteRows = false;
			dgvSettings.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvSettings.Columns.AddRange(Column3, Column1, Column2);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvSettings.DefaultCellStyle = dataGridViewCellStyle2;
			dgvSettings.Location = new System.Drawing.Point(12, 41);
			dgvSettings.Name = "dgvSettings";
			dgvSettings.RowHeadersVisible = false;
			dgvSettings.Size = new System.Drawing.Size(415, 338);
			dgvSettings.TabIndex = 12;
			Column3.DataPropertyName = "Id";
			Column3.HeaderText = "Id";
			Column3.Name = "Column3";
			Column3.Visible = false;
			Column1.DataPropertyName = "Caption";
			Column1.HeaderText = "Name";
			Column1.Name = "Column1";
			Column1.Width = 300;
			Column2.DataPropertyName = "BitValue";
			Column2.HeaderText = "Value";
			Column2.Name = "Column2";
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			Column2.Width = 110;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 2);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(84, 24);
			lblTitle.TabIndex = 82;
			lblTitle.Text = "Settings";
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(367, 385);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(60, 25);
			btnSave.TabIndex = 83;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(442, 416);
			base.Controls.Add(btnSave);
			base.Controls.Add(lblTitle);
			base.Controls.Add(dgvSettings);
			base.Name = "SettingsView";
			Text = "SettingsView";
			base.Load += new System.EventHandler(SettingsView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(dgvSettings, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			((System.ComponentModel.ISupportInitialize)dgvSettings).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsSettings).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
