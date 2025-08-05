using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class CustomeMessageBox : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private AshButton btnCopy;

		private AshButton btnOK;

		public Label lblMessage;

		public CustomeMessageBox(bool isMsg = false)
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			if (isMsg)
			{
				btnCopy.Visible = false;
				btnOK.Text = "Yes";
			}
		}

		private void CustomeMessageBox_Load(object sender, EventArgs e)
		{
			lblTitle.Text = Utility.uManufacturer;
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			string text = null;
			foreach (string item in Utility.uMacId)
			{
				text = text + " " + item;
			}
			Clipboard.SetText(text);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
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
			btnCopy = new AshUsercontrol.AshButton();
			btnOK = new AshUsercontrol.AshButton();
			lblMessage = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(827, 0);
			btnEnd.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 18f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(4, 4);
			lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(129, 38);
			lblTitle.TabIndex = 12;
			lblTitle.Text = "Ashpro";
			btnCopy.BackColor = System.Drawing.Color.DodgerBlue;
			btnCopy.FlatAppearance.BorderSize = 0;
			btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCopy.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnCopy.ForeColor = System.Drawing.Color.White;
			btnCopy.Location = new System.Drawing.Point(515, 201);
			btnCopy.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			btnCopy.Name = "btnCopy";
			btnCopy.Size = new System.Drawing.Size(152, 43);
			btnCopy.TabIndex = 42;
			btnCopy.TabStop = false;
			btnCopy.Text = "Copy";
			btnCopy.UseVisualStyleBackColor = false;
			btnCopy.Click += new System.EventHandler(btnCopy_Click);
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatAppearance.BorderSize = 0;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Location = new System.Drawing.Point(703, 201);
			btnOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(152, 43);
			btnOK.TabIndex = 42;
			btnOK.TabStop = false;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			lblMessage.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblMessage.Location = new System.Drawing.Point(43, 79);
			lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblMessage.Name = "lblMessage";
			lblMessage.Size = new System.Drawing.Size(780, 106);
			lblMessage.TabIndex = 43;
			lblMessage.Text = "The Registration is filed for the ID Test123456789. Please contact the dealer.";
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(872, 258);
			base.Controls.Add(lblMessage);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCopy);
			base.Controls.Add(lblTitle);
			base.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			base.Name = "CustomeMessageBox";
			base.Load += new System.EventHandler(CustomeMessageBox_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnCopy, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(lblMessage, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
