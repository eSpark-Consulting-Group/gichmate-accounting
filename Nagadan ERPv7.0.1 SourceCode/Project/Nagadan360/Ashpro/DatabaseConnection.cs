using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class DatabaseConnection : BaseForm
	{
		private IContainer components = null;

		private TextBox lblConnectionString;

		private Label label3;

		private Button btnConfigur;

		private Label lblTitle;

		private Button btnUpgrade;

		private Button btnOK;

		private Button btnFullReset;

		public DatabaseConnection()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void DatabaseConnection_Load(object sender, EventArgs e)
		{
			lblConnectionString.Text = Settings.Default.strConnetionString;
		}

		private void btnConfigur_Click(object sender, EventArgs e)
		{
			Settings.Default.strConnetionString = lblConnectionString.Text;
			Settings.Default.Save();
			MessageBox.Show("Successfully Configured");
		}

		private void btnUpgrade_Click(object sender, EventArgs e)
		{
			UpgradeHelper upgradeHelper = new UpgradeHelper();
			if (upgradeHelper.ShowDialog() == DialogResult.OK)
			{
				MessageBox.Show("Upgraded successfully.", Utility.uManufacturer);
			}
		}

		private void btnFullReset_Click(object sender, EventArgs e)
		{
			if (Messages.DialogMessage("This will remove all your data. Are you sure to reset the database?"))
			{
				string text = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
				if (text == AniHelper.PWordGeneration())
				{
					ResetDatabase.FullResetMethod();
				}
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
			lblConnectionString = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			btnConfigur = new System.Windows.Forms.Button();
			lblTitle = new System.Windows.Forms.Label();
			btnUpgrade = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			btnFullReset = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(845, 0);
			btnEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			lblConnectionString.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			lblConnectionString.Location = new System.Drawing.Point(131, 55);
			lblConnectionString.Multiline = true;
			lblConnectionString.Name = "lblConnectionString";
			lblConnectionString.Size = new System.Drawing.Size(734, 60);
			lblConnectionString.TabIndex = 36;
			label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(17, 53);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(94, 13);
			label3.TabIndex = 35;
			label3.Text = "Connection String:";
			btnConfigur.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnConfigur.BackColor = System.Drawing.Color.DodgerBlue;
			btnConfigur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnConfigur.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnConfigur.ForeColor = System.Drawing.Color.White;
			btnConfigur.Location = new System.Drawing.Point(330, 122);
			btnConfigur.Name = "btnConfigur";
			btnConfigur.Size = new System.Drawing.Size(75, 30);
			btnConfigur.TabIndex = 34;
			btnConfigur.Text = "Save";
			btnConfigur.UseVisualStyleBackColor = false;
			btnConfigur.Click += new System.EventHandler(btnConfigur_Click);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(4, 6);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(240, 24);
			lblTitle.TabIndex = 83;
			lblTitle.Text = "Database Connection";
			btnUpgrade.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnUpgrade.BackColor = System.Drawing.Color.DodgerBlue;
			btnUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnUpgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnUpgrade.ForeColor = System.Drawing.Color.White;
			btnUpgrade.Location = new System.Drawing.Point(411, 122);
			btnUpgrade.Name = "btnUpgrade";
			btnUpgrade.Size = new System.Drawing.Size(75, 30);
			btnUpgrade.TabIndex = 34;
			btnUpgrade.Text = "Upgrade";
			btnUpgrade.UseVisualStyleBackColor = false;
			btnUpgrade.Click += new System.EventHandler(btnUpgrade_Click);
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Location = new System.Drawing.Point(492, 122);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 30);
			btnOK.TabIndex = 34;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			btnFullReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnFullReset.BackColor = System.Drawing.Color.DodgerBlue;
			btnFullReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnFullReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnFullReset.ForeColor = System.Drawing.Color.White;
			btnFullReset.Location = new System.Drawing.Point(573, 122);
			btnFullReset.Name = "btnFullReset";
			btnFullReset.Size = new System.Drawing.Size(97, 30);
			btnFullReset.TabIndex = 34;
			btnFullReset.Text = "Full Reset";
			btnFullReset.UseVisualStyleBackColor = false;
			btnFullReset.Click += new System.EventHandler(btnFullReset_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(881, 161);
			base.Controls.Add(lblTitle);
			base.Controls.Add(lblConnectionString);
			base.Controls.Add(label3);
			base.Controls.Add(btnUpgrade);
			base.Controls.Add(btnFullReset);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnConfigur);
			base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			base.Name = "DatabaseConnection";
			base.Load += new System.EventHandler(DatabaseConnection_Load);
			base.Controls.SetChildIndex(btnConfigur, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(btnFullReset, 0);
			base.Controls.SetChildIndex(btnUpgrade, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(lblConnectionString, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
