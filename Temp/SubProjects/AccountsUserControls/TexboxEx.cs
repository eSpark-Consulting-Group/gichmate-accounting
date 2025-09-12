using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class TexboxEx : TextBox
	{
		private IContainer components = null;

		public TexboxEx()
		{
			BorderStyle = BorderStyle.None;
			BorderStyle = BorderStyle.None;
			Controls.Add(new Label
			{
				Height = 1,
				Dock = DockStyle.Bottom,
				BackColor = Color.DarkGray
			});
			Invalidate(invalidateChildren: true);
			InitializeComponent();
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
			SuspendLayout();
			Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4, 4, 4, 4);
			Name = "TexboxEx";
			Size = new Size(200, 185);
			ResumeLayout(false);
		}
	}
}
