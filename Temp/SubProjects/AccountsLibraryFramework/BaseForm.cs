using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
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
				Point point2 = Location = new Point(Left + e.X - downPoint.X, Top + e.Y - downPoint.Y);
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
			WindowState = FormWindowState.Normal;
		}

		private void BaseFormThree_Activated(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(BaseForm));
			btnEnd = new Button();
			SuspendLayout();
			btnEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnEnd.BackColor = Color.White;
			btnEnd.DialogResult = DialogResult.Cancel;
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.FlatStyle = FlatStyle.Flat;
			btnEnd.ForeColor = Color.DarkGray;
			btnEnd.ImeMode = ImeMode.NoControl;
			btnEnd.Location = new Point(239, 0);
			btnEnd.Name = "btnEnd";
			btnEnd.Size = new Size(35, 35);
			btnEnd.TabIndex = 11;
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
			ClientSize = new Size(273, 136);
			ControlBox = false;
			Controls.Add(btnEnd);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			Name = "BaseForm";
			StartPosition = FormStartPosition.CenterScreen;
			Activated += new EventHandler(BaseFormThree_Activated);
			Load += new EventHandler(BaseFormThree_Load);
			Paint += new PaintEventHandler(BaseFormThree_Paint);
			MouseDown += new MouseEventHandler(BaseFormThree_MouseDown);
			MouseMove += new MouseEventHandler(BaseFormThree_MouseMove);
			MouseUp += new MouseEventHandler(BaseFormThree_MouseUp);
			ResumeLayout(false);
		}
	}
}
