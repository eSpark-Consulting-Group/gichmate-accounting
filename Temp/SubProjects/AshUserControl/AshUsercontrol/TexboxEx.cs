using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class TexboxEx : TextBox
	{
		private IContainer components = null;

		public TexboxEx()
		{
			base.BorderStyle = BorderStyle.None;
			base.BorderStyle = BorderStyle.None;
			base.Controls.Add(new Label
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
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			base.Name = "TexboxEx";
			base.Size = new System.Drawing.Size(200, 185);
			ResumeLayout(false);
		}
	}
}
