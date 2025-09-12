using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class AshUpDown : NumericUpDown
	{
		private const int WM_ERASEBKGND = 20;

		private const int WM_PAINT = 15;

		private const int WM_NC_HITTEST = 132;

		private const int WM_NC_PAINT = 133;

		private const int WM_PRINTCLIENT = 792;

		private const int WM_SETCURSOR = 32;

		private Pen BorderPen = new Pen(Color.DarkGray, 2f);

		private IContainer components = null;

		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		private static extern int SendMessage(nint hwnd, int wMsg, nint wParam, object lParam);

		[DllImport("user32")]
		private static extern nint GetWindowDC(nint hWnd);

		[DllImport("user32")]
		private static extern int ReleaseDC(nint hWnd, nint hDC);

		public AshUpDown()
		{
			InitializeComponent();
			SetStyle(ControlStyles.DoubleBuffer, value: true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		}

		private void SendPrintClientMsg()
		{
			Graphics graphics = CreateGraphics();
			nint hdc = graphics.GetHdc();
			SendMessage(Handle, 792, hdc, 0);
			graphics.ReleaseHdc(hdc);
			graphics.Dispose();
		}

		private void OverrideControlBorder(Graphics g)
		{
			g.DrawRectangle(new Pen(Color.White, 2f), new Rectangle(0, 0, Width, Height));
			g.DrawLine(BorderPen, new Point(0, Height), new Point(Width - 18, Height));
			g.DrawLine(BorderPen, new Point(0, Height), new Point(Width - 17, Height));
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
			AutoScaleMode = AutoScaleMode.Font;
		}
	}
}
