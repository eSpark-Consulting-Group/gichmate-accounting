using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class AshTextBoxNormal : TextBox
	{
		private string mCue;

		private IContainer components = null;

		public bool isNumeric { get; set; }

		public bool isAllowNegative { get; set; }

		public bool isAllowSpecialChar { get; set; }

		public bool isNumberOnly { get; set; }

		public Color EnterColor { get; set; } = Color.LightPink;


		public Color LeaveColor { get; set; } = Color.White;


		public string Format { get; set; }

		[Localizable(true)]
		public string WaterMark
		{
			get
			{
				return mCue;
			}
			set
			{
				mCue = value;
				updateCue();
			}
		}

		public decimal Value
		{
			get
			{
				decimal result = default;
				decimal.TryParse(Text, out result);
				return result;
			}
			set
			{
				if (isNumeric)
				{
					Text = GlobalFunctions.getFormattedValue(Format, value);
				}
			}
		}

		public int Data
		{
			get
			{
				int result = 0;
				if (Text.Length > 0 && !Text.Substring(0, 1).isNumeric())
				{
					int.TryParse(Text.Substring(1), out result);
				}
				else
				{
					int.TryParse(Text, out result);
				}
				return result;
			}
			set
			{
				if (isNumberOnly)
				{
					Text = GlobalFunctions.getFormattedValue(Format, value);
				}
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern nint SendMessage(nint hWnd, int msg, nint wp, string lp);

		private void updateCue()
		{
			if (IsHandleCreated && mCue != null)
			{
				SendMessage(Handle, 5377, 1, mCue);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			updateCue();
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
			if (e.KeyCode == Keys.Return && !e.Handled)
			{
				SendKeys.Send("{TAB}");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down && !e.Handled)
			{
				SendKeys.Send("{TAB}");
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Up && !e.Handled)
			{
				SendKeys.Send("+{TAB}");
				e.Handled = true;
			}
		}

		public AshTextBoxNormal()
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

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			bool flag = false;
			if (isNumeric)
			{
				if (isAllowNegative && e.KeyChar == '-')
				{
					if (Text.Contains('-'))
					{
						e.Handled = true;
					}
					return;
				}
				if (isAllowSpecialChar && e.KeyChar == '+' || e.KeyChar == '\b' || e.KeyChar == '.')
				{
					return;
				}
				if (!int.TryParse(char.ConvertFromUtf32(e.KeyChar), out var _))
				{
					e.Handled = true;
				}
			}
			if (isNumberOnly && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
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
			SuspendLayout();
			Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4, 4, 4, 4);
			Name = "AshTextBoxNormal";
			Size = new Size(200, 185);
			ResumeLayout(false);
		}
	}
}
