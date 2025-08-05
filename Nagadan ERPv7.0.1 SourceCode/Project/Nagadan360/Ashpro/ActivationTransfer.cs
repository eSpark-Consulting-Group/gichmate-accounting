using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ActivationTransfer : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private Button btnTransfer;

		private Button btnCancel;

		public ActivationTransfer()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnTransfer_Click(object sender, EventArgs e)
		{
			string activationKey = Settings.Default.ActivationKey;
			Settings @default = Settings.Default;
			Settings default2 = Settings.Default;
			Settings default3 = Settings.Default;
			Settings default4 = Settings.Default;
			string text2 = (Settings.Default.ActivationKey = null);
			string text4 = (default4.ActivationMacID = text2);
			string text6 = (default3.ActivationEndDate = text4);
			string text9 = (@default.Activationlicense = (default2.ActivationStartDate = text6));
			Settings.Default.Save();
			string url = "http://license.riolabz.com/license-repo/public/api/v1/checkMac";
			_ = (List<RootObject>)Activation.VerifyLicenceAsync(activationKey, url, isActivation: false).GetAwaiter().GetResult();
			Application.Exit();
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
			btnTransfer = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(548, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(1, 1);
			lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(256, 29);
			lblTitle.TabIndex = 13;
			lblTitle.Text = "Activation Transfer";
			btnTransfer.BackColor = System.Drawing.Color.Maroon;
			btnTransfer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnTransfer.FlatAppearance.BorderSize = 0;
			btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnTransfer.ForeColor = System.Drawing.Color.White;
			btnTransfer.Location = new System.Drawing.Point(131, 99);
			btnTransfer.Margin = new System.Windows.Forms.Padding(4);
			btnTransfer.Name = "btnTransfer";
			btnTransfer.Size = new System.Drawing.Size(197, 59);
			btnTransfer.TabIndex = 15;
			btnTransfer.Text = "Transfer";
			btnTransfer.UseVisualStyleBackColor = false;
			btnTransfer.Click += new System.EventHandler(btnTransfer_Click);
			btnCancel.BackColor = System.Drawing.Color.MidnightBlue;
			btnCancel.FlatAppearance.BorderSize = 0;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(359, 99);
			btnCancel.Margin = new System.Windows.Forms.Padding(4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(197, 59);
			btnCancel.TabIndex = 14;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(593, 192);
			base.Controls.Add(btnTransfer);
			base.Controls.Add(btnCancel);
			base.Controls.Add(lblTitle);
			base.Name = "ActivationTransfer";
			Text = "ActivationTransfer";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(btnTransfer, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
