using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
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
				Point point2 = Location = new Point(Left + e.X - downPoint.X, Top + e.Y - downPoint.Y);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(BaseFormTwo));
			SuspendLayout();
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			ControlBox = false;
			FormBorderStyle = FormBorderStyle.None;
			KeyPreview = true;
			Name = "BaseFormTwo";
			ResumeLayout(false);
		}
	}
}
