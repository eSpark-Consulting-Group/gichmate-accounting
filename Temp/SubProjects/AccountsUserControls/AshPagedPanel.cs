using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class AshPagedPanel : UserControl
	{
		public enum flowDirection
		{
			LeftToRight,
			TopDown
		}

		private int _gapLeft = 5;

		private int _gapTop = 5;

		private Panel _LastPageShown;

		private List<Control> _Controls;

		private List<Control> _AllPages;

		private IContainer components = null;

		private Panel pnlMain;

		private FlowLayoutPanel pnlButtons;

		public flowDirection FlowDirection { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Control> ChildControls
		{
			get
			{
				return _Controls;
			}
			set
			{
				_Controls = value;
				AddControls();
			}
		}

		private void AddControls()
		{
			try
			{
				pnlButtons.Controls.Clear();
				pnlMain.Controls.Clear();
				if (_Controls.Count <= 0)
				{
					return;
				}
				int num = 1;
				Panel panel = CreatePage(num);
				int num2 = _gapLeft;
				int num3 = _gapTop;
				pnlMain.Controls.Add(panel);
				foreach (Control control in _Controls)
				{
					if (FlowDirection == flowDirection.LeftToRight)
					{
						if (num2 + control.Width > pnlMain.Width)
						{
							num3 += control.Height + _gapTop;
							num2 = _gapLeft;
						}
						if (num3 + control.Height > pnlMain.Height)
						{
							panel = CreatePage(++num);
							pnlMain.Controls.Add(panel);
							num2 = _gapLeft;
							num3 = _gapTop;
						}
						panel.Controls.Add(control);
						num2 += control.Width + _gapLeft;
					}
				}
				AshGradientPanel btn = pnlButtons.Controls.Find("btnPAge_1", searchAllChildren: false)[0] as AshGradientPanel;
				ShowPage(btn, 1);
			}
			catch (Exception)
			{
			}
		}

		public AshPagedPanel()
		{
			InitializeComponent();
			ChildControls = new List<Control>();
			_AllPages = new List<Control>();
			FlowDirection = flowDirection.LeftToRight;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		}

		private Panel CreatePage(int pageNo)
		{
			AshGradientPanel btnPage = new AshGradientPanel();
			btnPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnPage.Cursor = Cursors.Hand;
			btnPage.Location = new Point(3, 3);
			btnPage.Name = "btnPAge_" + pageNo;
			btnPage.Selected = true;
			btnPage.Tag = pageNo;
			btnPage.AllowMultiSelect = false;
			btnPage.Angle = 110f;
			btnPage.Font = new Font("Open Sans", 13f);
			btnPage.BackColor = Color.SteelBlue;
			btnPage.BottomColor = Color.DodgerBlue;
			btnPage.ForeColor = Color.White;
			btnPage.TextAdjestmentHeight = 0;
			btnPage.TextAlign = ContentAlignment.MiddleCenter;
			btnPage.TopColor = Color.FromArgb(61, 77, 125);
			btnPage.Size = new Size(45, 45);
			btnPage.TabIndex = 1;
			btnPage.Text = pageNo.ToString();
			pnlButtons.Controls.Add(btnPage);
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			if (FlowDirection == flowDirection.LeftToRight)
			{
				flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
			}
			else
			{
				flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			}
			flowLayoutPanel.BackColor = Color.White;
			flowLayoutPanel.Dock = DockStyle.Fill;
			flowLayoutPanel.Tag = pageNo;
			flowLayoutPanel.Name = "Page_" + pageNo;
			btnPage.Click += delegate
			{
				ShowPage(btnPage, Convert.ToInt32(btnPage.Tag));
			};
			pnlMain.Controls.Add(flowLayoutPanel);
			flowLayoutPanel.Visible = false;
			return flowLayoutPanel;
		}

		private void ShowPage(AshGradientPanel btn, int pageNo)
		{
			if (_LastPageShown != null)
			{
				_LastPageShown.Visible = false;
			}
			Panel panel = pnlMain.Controls.Find("Page_" + pageNo, searchAllChildren: false)[0] as Panel;
			panel.BringToFront();
			panel.Visible = true;
			_LastPageShown = panel;
			btn.Selected = true;
		}

		private void pnlMain_Resize(object sender, EventArgs e)
		{
			AddControls();
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
			pnlMain = new Panel();
			pnlButtons = new FlowLayoutPanel();
			SuspendLayout();
			pnlMain.BackColor = Color.White;
			pnlMain.Dock = DockStyle.Fill;
			pnlMain.Location = new Point(0, 0);
			pnlMain.Name = "pnlMain";
			pnlMain.Size = new Size(331, 327);
			pnlMain.TabIndex = 0;
			pnlMain.Resize += new EventHandler(pnlMain_Resize);
			pnlButtons.BackColor = Color.FromArgb(224, 224, 224);
			pnlButtons.Dock = DockStyle.Right;
			pnlButtons.Location = new Point(331, 0);
			pnlButtons.Name = "pnlButtons";
			pnlButtons.Size = new Size(44, 327);
			pnlButtons.TabIndex = 1;
			AutoScaleDimensions = new SizeF(6f, 13f);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlMain);
			Controls.Add(pnlButtons);
			DoubleBuffered = true;
			Name = "AshPagedPanel";
			Size = new Size(375, 327);
			ResumeLayout(false);
		}
	}
}
