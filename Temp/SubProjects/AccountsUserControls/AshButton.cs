using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
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
			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderSize = 0;
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
			components = new Container();
		}
	}
}
