using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshFramework
{
	public class BaseForm : Form
	{
		public Point downPoint = Point.Empty;

		private IContainer components = null;

		public Button btnEnd;

		public BaseForm()
		{
			InitializeComponent();
		}

		protected virtual void btnEnd_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BaseFormThree_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				downPoint = new Point(e.X, e.Y);
			}
		}

		private void BaseFormThree_MouseMove(object sender, MouseEventArgs e)
		{
			if (!(downPoint == Point.Empty))
			{
				Point point2 = (base.Location = new Point(base.Left + e.X - downPoint.X, base.Top + e.Y - downPoint.Y));
			}
		}

		private void BaseFormThree_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				downPoint = Point.Empty;
			}
		}

		private void BaseFormThree_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Normal;
		}

		private void BaseFormThree_Activated(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Normal;
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

		private void BaseFormThree_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.White, 2f), DisplayRectangle);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshFramework.BaseForm));
			btnEnd = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnEnd.BackColor = System.Drawing.Color.White;
			btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEnd.ForeColor = System.Drawing.Color.DarkGray;
			btnEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnEnd.Location = new System.Drawing.Point(239, 0);
			btnEnd.Name = "btnEnd";
			btnEnd.Size = new System.Drawing.Size(35, 35);
			btnEnd.TabIndex = 11;
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
			base.ClientSize = new System.Drawing.Size(273, 136);
			base.ControlBox = false;
			base.Controls.Add(btnEnd);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "BaseForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Activated += new System.EventHandler(BaseFormThree_Activated);
			base.Load += new System.EventHandler(BaseFormThree_Load);
			base.Paint += new System.Windows.Forms.PaintEventHandler(BaseFormThree_Paint);
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(BaseFormThree_MouseDown);
			base.MouseMove += new System.Windows.Forms.MouseEventHandler(BaseFormThree_MouseMove);
			base.MouseUp += new System.Windows.Forms.MouseEventHandler(BaseFormThree_MouseUp);
			ResumeLayout(false);
		}
	}
}
