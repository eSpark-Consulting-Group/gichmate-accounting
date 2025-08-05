using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class NewCompany : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private AshTextBoxNormal txtCompany;

		private Button btnSave;

		private Button btnCancel;

		private Button btnBrowse;

		public string sServer { get; set; }

		public string sCompany { get; set; }

		public bool isServer { get; set; } = true;


		public NewCompany()
		{
			InitializeComponent();
			ColorChange.Color(this);
			AniHelper.IconChangeMethod(this);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			base.UseWaitCursor = true;
			sCompany = DBHelper.NewDatabaseMethodSQL(txtCompany.Text, sServer, isShowMessage: true, isServer);
			base.UseWaitCursor = false;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			try
			{
				saveFileDialog.Title = "Save DataBase File";
				saveFileDialog.Filter = "All database Files(*.mdf)|*.mdf";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					txtCompany.Text = saveFileDialog.FileName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				saveFileDialog.Dispose();
			}
		}

		private void NewCompany_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
			if (isServer)
			{
				txtCompany.Enabled = false;
			}
			else
			{
				btnBrowse.Visible = false;
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
			lblTitle = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtCompany = new AshUsercontrol.AshTextBoxNormal();
			btnSave = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			btnBrowse = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(528, 1);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(4, 4);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(147, 24);
			lblTitle.TabIndex = 71;
			lblTitle.Text = "New Company";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(9, 56);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(116, 16);
			label1.TabIndex = 73;
			label1.Text = "Database Path :";
			txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtCompany.Data = 0;
			txtCompany.EnterColor = System.Drawing.Color.LightPink;
			txtCompany.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtCompany.Format = null;
			txtCompany.isAllowNegative = false;
			txtCompany.isAllowSpecialChar = false;
			txtCompany.isNumberOnly = false;
			txtCompany.isNumeric = false;
			txtCompany.LeaveColor = System.Drawing.Color.White;
			txtCompany.Location = new System.Drawing.Point(127, 59);
			txtCompany.Margin = new System.Windows.Forms.Padding(4);
			txtCompany.Name = "txtCompany";
			txtCompany.Size = new System.Drawing.Size(320, 16);
			txtCompany.TabIndex = 74;
			txtCompany.Value = new decimal(new int[4]);
			txtCompany.WaterMark = null;
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(195, 82);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 23);
			btnSave.TabIndex = 76;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.FlatAppearance.BorderSize = 0;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(291, 82);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 23);
			btnCancel.TabIndex = 77;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnBrowse.BackColor = System.Drawing.Color.DodgerBlue;
			btnBrowse.FlatAppearance.BorderSize = 0;
			btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnBrowse.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnBrowse.ForeColor = System.Drawing.Color.White;
			btnBrowse.Location = new System.Drawing.Point(454, 54);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new System.Drawing.Size(81, 26);
			btnBrowse.TabIndex = 76;
			btnBrowse.Text = "Browse";
			btnBrowse.UseVisualStyleBackColor = false;
			btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(564, 123);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnBrowse);
			base.Controls.Add(btnSave);
			base.Controls.Add(txtCompany);
			base.Controls.Add(label1);
			base.Controls.Add(lblTitle);
			base.Name = "NewCompany";
			base.Load += new System.EventHandler(NewCompany_Load);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtCompany, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnBrowse, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
