using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshFramework
{
	public class BaseFormTwo : Form
	{
		public Point downPoint = Point.Empty;

		private IContainer components = null;

		public BaseFormTwo()
		{
			InitializeComponent();
		}

		private void BaseForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				downPoint = new Point(e.X, e.Y);
			}
		}

		private void BaseForm_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.White, 2f), DisplayRectangle);
		}

		private void BaseForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (!(downPoint == Point.Empty))
			{
				Point point2 = (base.Location = new Point(base.Left + e.X - downPoint.X, base.Top + e.Y - downPoint.Y));
			}
		}

		private void BaseForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				downPoint = Point.Empty;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshFramework.BaseFormTwo));
			SuspendLayout();
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ControlBox = false;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.Name = "BaseFormTwo";
			ResumeLayout(false);
		}
	}
}
