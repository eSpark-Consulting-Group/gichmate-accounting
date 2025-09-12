using System.ComponentModel;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class ExButton : POSButton
	{
		private IContainer components = null;

		public ExButton()
		{
			SetStyle(ControlStyles.Selectable, value: false);
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
