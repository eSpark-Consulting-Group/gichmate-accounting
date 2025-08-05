using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class AshDateTimePicker : DateTimePicker
	{
		internal class ComboInfoHelper
		{
			private struct RECT
			{
				public int Left;

				public int Top;

				public int Right;

				public int Bottom;
			}

			private struct ComboBoxInfo
			{
				public int cbSize;

				public RECT rcItem;

				public RECT rcButton;

				public IntPtr stateButton;

				public IntPtr hwndCombo;

				public IntPtr hwndEdit;

				public IntPtr hwndList;
			}

			[DllImport("user32")]
			private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

			public static int GetComboDropDownWidth()
			{
				ComboBox comboBox = new ComboBox();
				int comboDropDownWidth = GetComboDropDownWidth(comboBox.Handle);
				comboBox.Dispose();
				return comboDropDownWidth;
			}

			public static int GetComboDropDownWidth(IntPtr handle)
			{
				ComboBoxInfo info = default(ComboBoxInfo);
				info.cbSize = Marshal.SizeOf(info);
				GetComboBoxInfo(handle, ref info);
				return info.rcButton.Right - info.rcButton.Left;
			}
		}

		private const int WM_ERASEBKGND = 20;

		private const int WM_PAINT = 15;

		private const int WM_NC_HITTEST = 132;

		private const int WM_NC_PAINT = 133;

		private const int WM_PRINTCLIENT = 792;

		private const int WM_SETCURSOR = 32;

		private Pen BorderPen = new Pen(Color.LightGray, 2f);

		private Pen BorderPenControl = new Pen(SystemColors.ControlDark, 2f);

		private bool DroppedDown = false;

		private static int DropDownButtonWidth = 17;

		private IContainer components = null;

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

		[DllImport("user32")]
		private static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		private static void FlatDateTimePicker()
		{
			DropDownButtonWidth = ComboInfoHelper.GetComboDropDownWidth();
		}

		public AshDateTimePicker()
		{
			SetStyle(ControlStyles.DoubleBuffer, value: true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
			InitializeComponent();
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			base.OnValueChanged(eventargs);
			Invalidate();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (!e.Handled && e.KeyCode == Keys.Return)
			{
				SendKeys.Send("{TAB}");
			}
		}

		protected override void WndProc(ref Message m)
		{
			try
			{
				IntPtr windowDC = GetWindowDC(m.HWnd);
				Graphics graphics = Graphics.FromHdc(windowDC);
				switch (m.Msg)
				{
				case 133:
					SendPrintClientMsg();
					OverrideControlBorder(graphics);
					m.Result = (IntPtr)1;
					break;
				case 15:
					base.WndProc(ref m);
					OverrideControlBorder(graphics);
					OverrideDropDown(graphics);
					break;
				case 132:
					base.WndProc(ref m);
					if (DroppedDown)
					{
						Invalidate(base.ClientRectangle, invalidateChildren: false);
					}
					break;
				default:
					base.WndProc(ref m);
					break;
				}
				ReleaseDC(m.HWnd, windowDC);
				graphics.Dispose();
			}
			catch (Exception)
			{
			}
		}

		private void SendPrintClientMsg()
		{
			Graphics graphics = CreateGraphics();
			IntPtr hdc = graphics.GetHdc();
			graphics.ReleaseHdc(hdc);
			graphics.Dispose();
		}

		private void OverrideDropDown(Graphics g)
		{
			if (!base.ShowUpDown)
			{
				Rectangle rectangle = new Rectangle(base.Width - DropDownButtonWidth, 0, DropDownButtonWidth, base.Height);
			}
		}

		private void OverrideControlBorder(Graphics g)
		{
			g.DrawRectangle(new Pen(Color.White, 2f), new Rectangle(0, 0, base.Width, base.Height));
			g.DrawLine(BorderPen, new Point(0, base.Height), new Point(base.Width - 33, base.Height));
		}

		protected override void OnDropDown(EventArgs eventargs)
		{
			DroppedDown = true;
			base.OnDropDown(eventargs);
		}

		protected override void OnCloseUp(EventArgs eventargs)
		{
			DroppedDown = false;
			base.OnCloseUp(eventargs);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Invalidate();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
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
			base.Name = "AshDateTimePicker";
			base.Size = new System.Drawing.Size(331, 150);
			ResumeLayout(false);
		}
	}
}
