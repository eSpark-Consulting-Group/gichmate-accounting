using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class DBConnectionSettings : BaseForm
	{
		private IContainer components = null;

		private Button btnConfigur;

		private AshTextBoxNormal txtPassword;

		private Label label7;

		private AshTextBoxNormal txtUserName;

		private Label label8;

		private Button button4;

		private Label lblHead;

		public string sUserName { get; set; }

		public string sPassword { get; set; }

		public DBConnectionSettings()
		{
			InitializeComponent();
			ColorChange.Color(this);
			AniHelper.IconChangeMethod(this);
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnConfigur_Click(object sender, EventArgs e)
		{
			sUserName = txtUserName.Text;
			sPassword = txtPassword.Text;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void DBConnectionSettings_Load(object sender, EventArgs e)
		{
			if (!(Settings.Default.sUserName == string.Empty) && !(Settings.Default.sPassword == string.Empty))
			{
				txtUserName.Text = Settings.Default.sUserName;
				txtPassword.Text = Settings.Default.sPassword;
			}
			else
			{
				txtUserName.Text = "atUser";
				txtPassword.Text = "at123";
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
			btnConfigur = new System.Windows.Forms.Button();
			txtPassword = new AshUsercontrol.AshTextBoxNormal();
			label7 = new System.Windows.Forms.Label();
			txtUserName = new AshUsercontrol.AshTextBoxNormal();
			label8 = new System.Windows.Forms.Label();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(389, 2);
			btnConfigur.BackColor = System.Drawing.Color.DodgerBlue;
			btnConfigur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnConfigur.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnConfigur.ForeColor = System.Drawing.Color.White;
			btnConfigur.Location = new System.Drawing.Point(329, 103);
			btnConfigur.Name = "btnConfigur";
			btnConfigur.Size = new System.Drawing.Size(75, 30);
			btnConfigur.TabIndex = 25;
			btnConfigur.Text = "Save";
			btnConfigur.UseVisualStyleBackColor = false;
			btnConfigur.Click += new System.EventHandler(btnConfigur_Click);
			txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtPassword.Data = 0;
			txtPassword.EnterColor = System.Drawing.Color.LightPink;
			txtPassword.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtPassword.Format = null;
			txtPassword.isAllowNegative = false;
			txtPassword.isAllowSpecialChar = false;
			txtPassword.isNumberOnly = false;
			txtPassword.isNumeric = false;
			txtPassword.LeaveColor = System.Drawing.Color.White;
			txtPassword.Location = new System.Drawing.Point(111, 80);
			txtPassword.Margin = new System.Windows.Forms.Padding(4);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = 'x';
			txtPassword.Size = new System.Drawing.Size(293, 16);
			txtPassword.TabIndex = 27;
			txtPassword.Value = new decimal(new int[4]);
			txtPassword.WaterMark = null;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(31, 82);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(76, 16);
			label7.TabIndex = 26;
			label7.Text = "Password:";
			txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtUserName.Data = 0;
			txtUserName.EnterColor = System.Drawing.Color.LightPink;
			txtUserName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtUserName.Format = null;
			txtUserName.isAllowNegative = false;
			txtUserName.isAllowSpecialChar = false;
			txtUserName.isNumberOnly = false;
			txtUserName.isNumeric = false;
			txtUserName.LeaveColor = System.Drawing.Color.White;
			txtUserName.Location = new System.Drawing.Point(111, 54);
			txtUserName.Margin = new System.Windows.Forms.Padding(4);
			txtUserName.Name = "txtUserName";
			txtUserName.Size = new System.Drawing.Size(293, 16);
			txtUserName.TabIndex = 27;
			txtUserName.Value = new decimal(new int[4]);
			txtUserName.WaterMark = null;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(23, 57);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(84, 16);
			label8.TabIndex = 26;
			label8.Text = "User Name:";
			button4.Location = new System.Drawing.Point(1, 31);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(240, 1);
			button4.TabIndex = 102;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 7);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(226, 24);
			lblHead.TabIndex = 101;
			lblHead.Text = "Database Configuration";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(427, 150);
			base.Controls.Add(txtPassword);
			base.Controls.Add(button4);
			base.Controls.Add(label8);
			base.Controls.Add(label7);
			base.Controls.Add(lblHead);
			base.Controls.Add(txtUserName);
			base.Controls.Add(btnConfigur);
			base.MaximizeBox = false;
			base.Name = "DBConnectionSettings";
			base.Load += new System.EventHandler(DBConnectionSettings_Load);
			base.Controls.SetChildIndex(btnConfigur, 0);
			base.Controls.SetChildIndex(txtUserName, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(txtPassword, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
