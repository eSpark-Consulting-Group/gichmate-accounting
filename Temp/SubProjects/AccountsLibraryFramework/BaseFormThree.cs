using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
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
			WindowState = FormWindowState.Maximized;
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
			btnEnd = new Button();
			SuspendLayout();
			btnEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnEnd.BackColor = Color.White;
			btnEnd.DialogResult = DialogResult.Cancel;
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.FlatStyle = FlatStyle.Flat;
			btnEnd.ForeColor = Color.DarkGray;
			btnEnd.ImeMode = ImeMode.NoControl;
			btnEnd.Location = new Point(456, 0);
			btnEnd.Name = "btnEnd";
			btnEnd.Size = new Size(35, 35);
			btnEnd.TabIndex = 12;
			btnEnd.TabStop = false;
			btnEnd.Text = "X";
			btnEnd.UseVisualStyleBackColor = false;
			btnEnd.Click += new EventHandler(btnEnd_Click);
			btnEnd.MouseEnter += new EventHandler(btnEnd_MouseEnter);
			btnEnd.MouseLeave += new EventHandler(btnEnd_MouseLeave);
			AutoScaleDimensions = new SizeF(6f, 13f);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			CancelButton = btnEnd;
			ClientSize = new Size(491, 317);
			Controls.Add(btnEnd);
			FormBorderStyle = FormBorderStyle.None;
			KeyPreview = true;
			Name = "BaseFormThree";
			Load += new EventHandler(BaseFormThree_Load);
			ResumeLayout(false);
		}
	}
}
