using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshFramework
{
	public class BaseFormThree : Form
	{
		private IContainer components = null;

		public Button btnEnd;

		public BaseFormThree()
		{
			InitializeComponent();
		}

		private void BaseFormThree_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
		}

		private void btnEnd_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnEnd_MouseEnter(object sender, EventArgs e)
		{
			btnEnd.ForeColor = Color.White;
			btnEnd.BackColor = Color.Red;
		}

		private void btnEnd_MouseLeave(object sender, EventArgs e)
		{
			btnEnd.ForeColor = Color.DarkGray;
			btnEnd.BackColor = Color.White;
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
			btnEnd = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnEnd.BackColor = System.Drawing.Color.White;
			btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEnd.ForeColor = System.Drawing.Color.DarkGray;
			btnEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnEnd.Location = new System.Drawing.Point(456, 0);
			btnEnd.Name = "btnEnd";
			btnEnd.Size = new System.Drawing.Size(35, 35);
			btnEnd.TabIndex = 12;
			btnEnd.TabStop = false;
			btnEnd.Text = "X";
			btnEnd.UseVisualStyleBackColor = false;
			btnEnd.Click += new System.EventHandler(btnEnd_Click);
			btnEnd.MouseEnter += new System.EventHandler(btnEnd_MouseEnter);
			btnEnd.MouseLeave += new System.EventHandler(btnEnd_MouseLeave);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.CancelButton = btnEnd;
			base.ClientSize = new System.Drawing.Size(491, 317);
			base.Controls.Add(btnEnd);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.Name = "BaseFormThree";
			base.Load += new System.EventHandler(BaseFormThree_Load);
			ResumeLayout(false);
		}
	}
}
