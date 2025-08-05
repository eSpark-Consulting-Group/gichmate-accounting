using System.ComponentModel;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class POSButton : Button
	{
		private IContainer components = null;

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
