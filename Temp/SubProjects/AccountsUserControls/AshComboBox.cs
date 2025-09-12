using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class AshComboBox : ComboBox
	{
		private Label lblLine;

		private IContainer components = null;

		public Color EnterColor { get; set; } = Color.LightPink;


		public Color LeaveColor { get; set; } = Color.White;


		public AshComboBox()
		{
			FlatStyle = FlatStyle.Flat;
			lblLine = new Label
			{
				Height = 1,
				Dock = DockStyle.None,
				Left = Left,
				Top = Height - 2,
				Width = Width - 20,
				BackColor = Color.DarkGray
			};
			Controls.Add(lblLine);
			Invalidate(invalidateChildren: true);
			InitializeComponent();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			lblLine.Width = Width - 20;
			lblLine.Top = Height - 2;
		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			BackColor = EnterColor;
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			BackColor = LeaveColor;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (!e.Handled && e.KeyCode == Keys.Return)
			{
				SendKeys.Send("{TAB}");
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
			components = new Container();
		}
	}
}
