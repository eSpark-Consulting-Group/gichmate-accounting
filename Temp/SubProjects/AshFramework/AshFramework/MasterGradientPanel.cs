using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AshFramework
{
	public class MasterGradientPanel : Panel
	{
		private string _Text = "";

		private IContainer components = null;

		public Color TopColor { get; set; }

		public Color BottomColor { get; set; }

		public float Angle { get; set; }

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Bindable(true)]
		public bool Selected { get; set; }

		public bool AllowMultiSelect { get; set; }

		public override string Text
		{
			get
			{
				return _Text;
			}
			set
			{
				_Text = value;
			}
		}

		public ContentAlignment TextAlign { get; set; }

		public int TextAdjestmentHeight { get; set; }

		public MasterGradientPanel()
		{
			InitializeComponent();
			TextAdjestmentHeight = 0;
			TextAlign = ContentAlignment.MiddleCenter;
			_Text = "";
		}

		private void DrawText(Graphics g)
		{
			Brush brush = new SolidBrush(ForeColor);
			StringFormat stringFormat = new StringFormat();
			int num = (int)Math.Log((double)TextAlign, 2.0);
			stringFormat.LineAlignment = (StringAlignment)(num / 4);
			stringFormat.Alignment = (StringAlignment)(num % 4);
			g.DrawString(_Text, Font, brush, new Rectangle(TextAdjestmentHeight / 2, TextAdjestmentHeight / 2, base.ClientRectangle.Width - TextAdjestmentHeight, base.ClientRectangle.Height - TextAdjestmentHeight), stringFormat);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			LinearGradientBrush brush = new LinearGradientBrush(base.ClientRectangle, TopColor, BottomColor, Angle);
			Graphics graphics = e.Graphics;
			graphics.FillRectangle(brush, base.ClientRectangle);
			if (Text != string.Empty)
			{
				DrawText(e.Graphics);
			}
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
