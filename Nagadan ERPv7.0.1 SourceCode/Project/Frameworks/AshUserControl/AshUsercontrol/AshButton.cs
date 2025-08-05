using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class AshButton : Button
	{
		private IContainer components = null;

		public AshButton()
		{
			InitializeComponent();
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			base.FlatStyle = FlatStyle.Flat;
			base.FlatAppearance.BorderSize = 0;
			Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
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
		}
	}
}
