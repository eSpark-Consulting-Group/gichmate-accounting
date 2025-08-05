using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ServerOrClient : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private RadioButton rbServer;

		private RadioButton rbClient;

		private Button btnOK;

		public ServerOrClient()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (rbServer.Checked)
			{
				Settings.Default.isServer = true;
				Settings.Default.Save();
				DatabaseInitialCreation databaseInitialCreation = new DatabaseInitialCreation();
				databaseInitialCreation.Show();
			}
			else
			{
				Settings.Default.isServer = false;
				Settings.Default.Save();
				CompanyView companyView = new CompanyView();
				companyView.Show();
			}
			base.DialogResult = DialogResult.OK;
			Close();
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
			rbServer = new System.Windows.Forms.RadioButton();
			rbClient = new System.Windows.Forms.RadioButton();
			btnOK = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(225, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 15.75f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(-1, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(183, 26);
			lblTitle.TabIndex = 83;
			lblTitle.Text = "Server Or Client";
			rbServer.AutoSize = true;
			rbServer.Checked = true;
			rbServer.Location = new System.Drawing.Point(66, 67);
			rbServer.Name = "rbServer";
			rbServer.Size = new System.Drawing.Size(56, 17);
			rbServer.TabIndex = 84;
			rbServer.TabStop = true;
			rbServer.Text = "Server";
			rbServer.UseVisualStyleBackColor = true;
			rbClient.AutoSize = true;
			rbClient.Location = new System.Drawing.Point(164, 67);
			rbClient.Name = "rbClient";
			rbClient.Size = new System.Drawing.Size(51, 17);
			rbClient.TabIndex = 84;
			rbClient.Text = "Client";
			rbClient.UseVisualStyleBackColor = true;
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatAppearance.BorderSize = 0;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Location = new System.Drawing.Point(180, 105);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 23);
			btnOK.TabIndex = 85;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(260, 134);
			base.Controls.Add(btnOK);
			base.Controls.Add(rbClient);
			base.Controls.Add(rbServer);
			base.Controls.Add(lblTitle);
			base.Name = "ServerOrClient";
			Text = "ServerOrClient";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(rbServer, 0);
			base.Controls.SetChildIndex(rbClient, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
