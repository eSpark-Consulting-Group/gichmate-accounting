using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ashpro
{
	public class RotatingLabel : Label
	{
		private int m_RotateAngle = 0;

		private string m_NewText = string.Empty;

		private IContainer components = null;

		public int RotateAngle
		{
			get
			{
				return m_RotateAngle;
			}
			set
			{
				m_RotateAngle = value;
				Invalidate();
			}
		}

		public string NewText
		{
			get
			{
				return m_NewText;
			}
			set
			{
				m_NewText = value;
				Invalidate();
			}
		}

		public RotatingLabel()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Func<double, double> func = (double angle) => Math.PI * angle / 180.0;
			Brush brush = new SolidBrush(ForeColor);
			SizeF sizeF = e.Graphics.MeasureString(NewText, Font, base.Parent.Width);
			int num = (RotateAngle % 360 + 360) % 360;
			double num2 = func(num);
			int value = (int)Math.Ceiling((double)sizeF.Height * Math.Sin(num2));
			int value2 = (int)Math.Ceiling((double)sizeF.Width * Math.Cos(num2));
			int value3 = (int)Math.Ceiling((double)sizeF.Width * Math.Sin(num2));
			int value4 = (int)Math.Ceiling((double)sizeF.Height * Math.Cos(num2));
			int num3 = Math.Abs(value) + Math.Abs(value2);
			int num4 = Math.Abs(value3) + Math.Abs(value4);
			base.Width = num3;
			base.Height = num4;
			int num5 = ((num >= 0 && num < 90) ? 1 : ((num >= 90 && num < 180) ? 2 : ((num >= 180 && num < 270) ? 3 : ((num >= 270 && num < 360) ? 4 : 0))));
			int num6 = 0;
			int num7 = 0;
			switch (num5)
			{
			case 1:
				num6 = Math.Abs(value);
				break;
			case 2:
				num6 = num3;
				num7 = Math.Abs(value4);
				break;
			case 3:
				num6 = Math.Abs(value2);
				num7 = num4;
				break;
			case 4:
				num7 = Math.Abs(value3);
				break;
			}
			e.Graphics.TranslateTransform(num6, num7);
			e.Graphics.RotateTransform(RotateAngle);
			e.Graphics.DrawString(NewText, Font, brush, 0f, 0f);
			base.OnPaint(e);
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
