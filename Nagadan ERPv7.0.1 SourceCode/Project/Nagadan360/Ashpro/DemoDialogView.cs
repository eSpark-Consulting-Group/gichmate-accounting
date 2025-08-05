using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;

namespace Ashpro
{
	public class DemoDialogView : BaseFormTwo
	{
		private IContainer components = null;

		private Button btnDemo;

		private Button btnRegister;

		private Button btnCancel;

		private Label lblDialog;

		private Label lblTitle;

		private ToolTip toolTip;

		public DemoDialogView(DateTime endDate, DateTime curDate)
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			lblTitle.Text = Utility.uManufacturer;
			string dateDifferenceDays = AniHelper.GetDateDifferenceDays(endDate, curDate);
			int dateDifferences = AniHelper.GetDateDifferences(endDate, curDate);
			if (dateDifferences == 0)
			{
				btnDemo.Visible = false;
				lblDialog.Text = "\"The demo subscription ended today. \n Please contact the dealer to purchase The Application.\nIf you have already purchased the application,\n kindly press the Register button and enter the Activation Key.\"";
			}
			else if (dateDifferences < 0)
			{
				btnDemo.Visible = false;
				lblDialog.Text = "\"The demo subscription ended on " + endDate.ToString("dd-MM-yyyy") + ".\n Please contact the dealer to purchase The Application.\n If you have already purchased the application, \nkindly press the Register button and enter the Activation Key.\"";
			}
			else
			{
				lblDialog.Text = "\"The demo subscription will end within " + dateDifferenceDays + ".\n Please contact the dealer to purchase The Application.\nIf you have already purchased the application,\n kindly press the Register button and enter the Activation Key.\"";
			}
		}

		private void btnDemo_Click(object sender, EventArgs e)
		{
			Hide();
			POSActivation.FormLoadMethodTwo();
		}

		private void btnRegister_Click(object sender, EventArgs e)
		{
			Activation activation = new Activation();
			Hide();
			if (activation.ShowDialog() == DialogResult.OK)
			{
				POSActivation.FormLoadMethodTwo();
			}
			else
			{
				Application.Exit();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void DemoDialogView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				btnRegister.PerformClick();
			}
			else if (e.KeyCode == Keys.Return)
			{
				if (btnDemo.Visible)
				{
					btnDemo.PerformClick();
				}
				else
				{
					btnRegister.PerformClick();
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
			components = new System.ComponentModel.Container();
			btnDemo = new System.Windows.Forms.Button();
			btnRegister = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			lblDialog = new System.Windows.Forms.Label();
			lblTitle = new System.Windows.Forms.Label();
			toolTip = new System.Windows.Forms.ToolTip(components);
			SuspendLayout();
			btnDemo.BackColor = System.Drawing.Color.MidnightBlue;
			btnDemo.FlatAppearance.BorderSize = 0;
			btnDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDemo.ForeColor = System.Drawing.Color.White;
			btnDemo.Location = new System.Drawing.Point(111, 160);
			btnDemo.Name = "btnDemo";
			btnDemo.Size = new System.Drawing.Size(148, 48);
			btnDemo.TabIndex = 0;
			btnDemo.Text = "Continue Demo";
			toolTip.SetToolTip(btnDemo, "Press Enter");
			btnDemo.UseVisualStyleBackColor = false;
			btnDemo.Click += new System.EventHandler(btnDemo_Click);
			btnRegister.BackColor = System.Drawing.Color.FromArgb(0, 64, 64);
			btnRegister.FlatAppearance.BorderSize = 0;
			btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnRegister.ForeColor = System.Drawing.Color.White;
			btnRegister.Location = new System.Drawing.Point(265, 160);
			btnRegister.Name = "btnRegister";
			btnRegister.Size = new System.Drawing.Size(148, 48);
			btnRegister.TabIndex = 1;
			btnRegister.Text = "Register With Key";
			toolTip.SetToolTip(btnRegister, "Press F3");
			btnRegister.UseVisualStyleBackColor = false;
			btnRegister.Click += new System.EventHandler(btnRegister_Click);
			btnCancel.BackColor = System.Drawing.Color.Maroon;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.FlatAppearance.BorderSize = 0;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(419, 160);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(148, 48);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Cancel";
			toolTip.SetToolTip(btnCancel, "Press Esc");
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			lblDialog.AutoSize = true;
			lblDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblDialog.ForeColor = System.Drawing.Color.Navy;
			lblDialog.Location = new System.Drawing.Point(30, 62);
			lblDialog.Name = "lblDialog";
			lblDialog.Size = new System.Drawing.Size(52, 17);
			lblDialog.TabIndex = 3;
			lblDialog.Text = "label1";
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(0, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(77, 24);
			lblTitle.TabIndex = 4;
			lblTitle.Text = "Ashpro";
			base.AcceptButton = btnDemo;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(671, 239);
			base.Controls.Add(lblTitle);
			base.Controls.Add(lblDialog);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnRegister);
			base.Controls.Add(btnDemo);
			base.Name = "DemoDialogView";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "DemoDialogView";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(DemoDialogView_KeyDown);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
