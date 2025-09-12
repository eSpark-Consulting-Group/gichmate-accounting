using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class DashboardPOSKeyboard : UserControl
	{
		private IContainer components = null;

		public ExButton btnce;

		public ExButton btnmu;

		public ExButton btnsub;

		public ExButton btnsvn;

		public ExButton btnfor;

		public ExButton btnone;

		public ExButton btnse;

		public ExButton btnequel;

		public ExButton btnpls;

		public ExButton btndot;

		public ExButton btnegt;

		public ExButton btnto;

		public ExButton btnfive;

		public ExButton btnne;

		public ExButton btnsix;

		public ExButton btntree;

		public ExButton StarButton
		{
			get
			{
				return btnmu;
			}
			set
			{
				btnmu = value;
			}
		}

		public event NumberClickEventHandler atClearClick;

		public event NumberClickEventHandler atEqualClick;

		public event StarClickEventHandler atStarClick;

		public event AddClickEventHandler atAddClick;

		public event SubClickEventHandler atSubClick;

		public DashboardPOSKeyboard()
		{
			InitializeComponent();
		}

		private void btnce_Click(object sender, EventArgs e)
		{
			if (atClearClick != null)
			{
                atClearClick(this);
			}
			Form form = FindForm();
			if (form != null && form.ActiveControl != null && (form.ActiveControl.GetType() == typeof(TextBox) || form.ActiveControl.GetType().BaseType == typeof(TextBox)))
			{
				(form.ActiveControl as TextBox).Text = "";
			}
		}

		private void btnone_Click(object sender, EventArgs e)
		{
			SendKeys.Send((sender as Button).Tag.ToString());
		}

		private void btnequel_Click(object sender, EventArgs e)
		{
			if (atEqualClick != null)
			{
                atEqualClick(this);
			}
		}

		private void btnpls_Click(object sender, EventArgs e)
		{
			if (atAddClick != null)
			{
                atAddClick(this);
			}
		}

		private void btnsub_Click(object sender, EventArgs e)
		{
			if (atSubClick != null)
			{
                atSubClick(this);
			}
		}

		private void btnmu_Click(object sender, EventArgs e)
		{
			if (atStarClick != null)
			{
                atStarClick(this);
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
			btnne = new ExButton();
			btnsix = new ExButton();
			btntree = new ExButton();
			btnegt = new ExButton();
			btnto = new ExButton();
			btnfive = new ExButton();
			btnequel = new ExButton();
			btnpls = new ExButton();
			btndot = new ExButton();
			btnse = new ExButton();
			btnsvn = new ExButton();
			btnfor = new ExButton();
			btnone = new ExButton();
			btnsub = new ExButton();
			btnmu = new ExButton();
			btnce = new ExButton();
			SuspendLayout();
			btnne.BackColor = Color.Gainsboro;
			btnne.BackgroundImageLayout = ImageLayout.Stretch;
			btnne.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnne.FlatAppearance.BorderSize = 0;
			btnne.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnne.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnne.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnne.FlatStyle = FlatStyle.Popup;
			btnne.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnne.ForeColor = Color.FromArgb(61, 77, 125);
			btnne.Location = new Point(161, 60);
			btnne.Name = "btnne";
			btnne.Size = new Size(74, 51);
			btnne.TabIndex = 14;
			btnne.Tag = "9";
			btnne.Text = "9";
			btnne.UseVisualStyleBackColor = false;
			btnne.Click += new EventHandler(btnone_Click);
			btnsix.BackColor = Color.Gainsboro;
			btnsix.BackgroundImageLayout = ImageLayout.Stretch;
			btnsix.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnsix.FlatAppearance.BorderSize = 0;
			btnsix.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnsix.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnsix.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnsix.FlatStyle = FlatStyle.Popup;
			btnsix.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnsix.ForeColor = Color.FromArgb(61, 77, 125);
			btnsix.Location = new Point(161, 117);
			btnsix.Name = "btnsix";
			btnsix.Size = new Size(74, 51);
			btnsix.TabIndex = 15;
			btnsix.Tag = "6";
			btnsix.Text = "6";
			btnsix.UseVisualStyleBackColor = false;
			btnsix.Click += new EventHandler(btnone_Click);
			btntree.BackColor = Color.Gainsboro;
			btntree.BackgroundImageLayout = ImageLayout.Stretch;
			btntree.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btntree.FlatAppearance.BorderSize = 0;
			btntree.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btntree.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btntree.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btntree.FlatStyle = FlatStyle.Popup;
			btntree.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btntree.ForeColor = Color.FromArgb(61, 77, 125);
			btntree.Location = new Point(161, 174);
			btntree.Name = "btntree";
			btntree.Size = new Size(74, 51);
			btntree.TabIndex = 16;
			btntree.Tag = "3";
			btntree.Text = "3";
			btntree.UseVisualStyleBackColor = false;
			btntree.Click += new EventHandler(btnone_Click);
			btnegt.BackColor = Color.Gainsboro;
			btnegt.BackgroundImageLayout = ImageLayout.Stretch;
			btnegt.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnegt.FlatAppearance.BorderSize = 0;
			btnegt.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnegt.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnegt.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnegt.FlatStyle = FlatStyle.Popup;
			btnegt.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnegt.ForeColor = Color.FromArgb(61, 77, 125);
			btnegt.Location = new Point(81, 60);
			btnegt.Name = "btnegt";
			btnegt.Size = new Size(74, 51);
			btnegt.TabIndex = 11;
			btnegt.Tag = "8";
			btnegt.Text = "8";
			btnegt.UseVisualStyleBackColor = false;
			btnegt.Click += new EventHandler(btnone_Click);
			btnto.BackColor = Color.Gainsboro;
			btnto.BackgroundImageLayout = ImageLayout.Stretch;
			btnto.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnto.FlatAppearance.BorderSize = 0;
			btnto.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnto.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnto.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnto.FlatStyle = FlatStyle.Popup;
			btnto.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnto.ForeColor = Color.FromArgb(61, 77, 125);
			btnto.Location = new Point(82, 174);
			btnto.Name = "btnto";
			btnto.Size = new Size(74, 51);
			btnto.TabIndex = 12;
			btnto.Tag = "2";
			btnto.Text = "2";
			btnto.UseVisualStyleBackColor = false;
			btnto.Click += new EventHandler(btnone_Click);
			btnfive.BackColor = Color.Gainsboro;
			btnfive.BackgroundImageLayout = ImageLayout.Stretch;
			btnfive.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnfive.FlatAppearance.BorderSize = 0;
			btnfive.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnfive.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnfive.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnfive.FlatStyle = FlatStyle.Popup;
			btnfive.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnfive.ForeColor = Color.FromArgb(61, 77, 125);
			btnfive.Location = new Point(82, 117);
			btnfive.Name = "btnfive";
			btnfive.Size = new Size(74, 51);
			btnfive.TabIndex = 13;
			btnfive.Tag = "5";
			btnfive.Text = "5";
			btnfive.UseVisualStyleBackColor = false;
			btnfive.Click += new EventHandler(btnone_Click);
			btnequel.BackColor = Color.Gainsboro;
			btnequel.BackgroundImageLayout = ImageLayout.Stretch;
			btnequel.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnequel.FlatAppearance.BorderSize = 0;
			btnequel.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnequel.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnequel.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnequel.FlatStyle = FlatStyle.Popup;
			btnequel.Font = new Font("Microsoft Sans Serif", 24f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnequel.ForeColor = Color.FromArgb(61, 77, 125);
			btnequel.Location = new Point(240, 174);
			btnequel.Name = "btnequel";
			btnequel.Size = new Size(74, 108);
			btnequel.TabIndex = 8;
			btnequel.Tag = "";
			btnequel.Text = "=";
			btnequel.UseVisualStyleBackColor = false;
			btnequel.Click += new EventHandler(btnequel_Click);
			btnpls.BackColor = Color.Gainsboro;
			btnpls.BackgroundImageLayout = ImageLayout.Stretch;
			btnpls.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnpls.FlatAppearance.BorderSize = 0;
			btnpls.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnpls.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnpls.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnpls.FlatStyle = FlatStyle.Popup;
			btnpls.Font = new Font("MS Reference Sans Serif", 24f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnpls.ForeColor = Color.FromArgb(61, 77, 125);
			btnpls.Location = new Point(240, 60);
			btnpls.Name = "btnpls";
			btnpls.Size = new Size(74, 108);
			btnpls.TabIndex = 9;
			btnpls.Tag = "";
			btnpls.Text = "+";
			btnpls.UseVisualStyleBackColor = false;
			btnpls.Click += new EventHandler(btnpls_Click);
			btndot.BackColor = Color.Gainsboro;
			btndot.BackgroundImageLayout = ImageLayout.Stretch;
			btndot.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btndot.FlatAppearance.BorderSize = 0;
			btndot.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btndot.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btndot.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btndot.FlatStyle = FlatStyle.Popup;
			btndot.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btndot.ForeColor = Color.FromArgb(61, 77, 125);
			btndot.Location = new Point(161, 231);
			btndot.Name = "btndot";
			btndot.Size = new Size(74, 51);
			btndot.TabIndex = 10;
			btndot.Tag = ".";
			btndot.Text = ".";
			btndot.UseVisualStyleBackColor = false;
			btndot.Click += new EventHandler(btnone_Click);
			btnse.BackColor = Color.Gainsboro;
			btnse.BackgroundImageLayout = ImageLayout.Stretch;
			btnse.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnse.FlatAppearance.BorderSize = 0;
			btnse.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnse.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnse.FlatStyle = FlatStyle.Popup;
			btnse.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnse.ForeColor = Color.FromArgb(61, 77, 125);
			btnse.Location = new Point(2, 231);
			btnse.Name = "btnse";
			btnse.Size = new Size(154, 51);
			btnse.TabIndex = 7;
			btnse.Tag = "0";
			btnse.Text = "0";
			btnse.UseVisualStyleBackColor = false;
			btnse.Click += new EventHandler(btnone_Click);
			btnsvn.BackColor = Color.Gainsboro;
			btnsvn.BackgroundImageLayout = ImageLayout.Stretch;
			btnsvn.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnsvn.FlatAppearance.BorderSize = 0;
			btnsvn.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnsvn.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnsvn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnsvn.FlatStyle = FlatStyle.Popup;
			btnsvn.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnsvn.ForeColor = Color.FromArgb(61, 77, 125);
			btnsvn.Location = new Point(2, 60);
			btnsvn.Name = "btnsvn";
			btnsvn.Size = new Size(74, 51);
			btnsvn.TabIndex = 4;
			btnsvn.Tag = "7";
			btnsvn.Text = "7";
			btnsvn.UseVisualStyleBackColor = false;
			btnsvn.Click += new EventHandler(btnone_Click);
			btnfor.BackColor = Color.Gainsboro;
			btnfor.BackgroundImageLayout = ImageLayout.Stretch;
			btnfor.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnfor.FlatAppearance.BorderSize = 0;
			btnfor.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnfor.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnfor.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnfor.FlatStyle = FlatStyle.Popup;
			btnfor.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnfor.ForeColor = Color.FromArgb(61, 77, 125);
			btnfor.Location = new Point(2, 117);
			btnfor.Name = "btnfor";
			btnfor.Size = new Size(74, 51);
			btnfor.TabIndex = 5;
			btnfor.Tag = "4";
			btnfor.Text = "4";
			btnfor.UseVisualStyleBackColor = false;
			btnfor.Click += new EventHandler(btnone_Click);
			btnone.BackColor = Color.Gainsboro;
			btnone.BackgroundImageLayout = ImageLayout.Stretch;
			btnone.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnone.FlatAppearance.BorderSize = 0;
			btnone.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnone.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnone.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnone.FlatStyle = FlatStyle.Popup;
			btnone.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnone.ForeColor = Color.FromArgb(61, 77, 125);
			btnone.Location = new Point(2, 174);
			btnone.Name = "btnone";
			btnone.Size = new Size(74, 51);
			btnone.TabIndex = 6;
			btnone.Tag = "1";
			btnone.Text = "1";
			btnone.UseVisualStyleBackColor = false;
			btnone.Click += new EventHandler(btnone_Click);
			btnsub.BackColor = Color.Gainsboro;
			btnsub.BackgroundImageLayout = ImageLayout.Stretch;
			btnsub.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnsub.FlatAppearance.BorderSize = 0;
			btnsub.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnsub.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnsub.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnsub.FlatStyle = FlatStyle.Popup;
			btnsub.Font = new Font("MS Reference Sans Serif", 24f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnsub.ForeColor = Color.FromArgb(61, 77, 125);
			btnsub.Location = new Point(240, 3);
			btnsub.Name = "btnsub";
			btnsub.Size = new Size(74, 51);
			btnsub.TabIndex = 3;
			btnsub.Tag = "";
			btnsub.Text = "-";
			btnsub.UseVisualStyleBackColor = false;
			btnsub.Click += new EventHandler(btnsub_Click);
			btnmu.BackColor = Color.Gainsboro;
			btnmu.BackgroundImageLayout = ImageLayout.Stretch;
			btnmu.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnmu.FlatAppearance.BorderSize = 0;
			btnmu.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnmu.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnmu.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnmu.FlatStyle = FlatStyle.Popup;
			btnmu.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnmu.ForeColor = Color.FromArgb(61, 77, 125);
			btnmu.Location = new Point(161, 3);
			btnmu.Name = "btnmu";
			btnmu.Size = new Size(74, 51);
			btnmu.TabIndex = 2;
			btnmu.Tag = "";
			btnmu.Text = "*";
			btnmu.UseVisualStyleBackColor = false;
			btnmu.Click += new EventHandler(btnmu_Click);
			btnce.BackColor = Color.Gainsboro;
			btnce.BackgroundImageLayout = ImageLayout.Zoom;
			btnce.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
			btnce.FlatAppearance.BorderSize = 0;
			btnce.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 192, 192);
			btnce.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 224, 192);
			btnce.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192);
			btnce.FlatStyle = FlatStyle.Popup;
			btnce.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnce.ForeColor = Color.FromArgb(61, 77, 125);
			btnce.Location = new Point(2, 3);
			btnce.Name = "btnce";
			btnce.Size = new Size(153, 51);
			btnce.TabIndex = 1;
			btnce.Tag = "<";
			btnce.Text = "CE";
			btnce.UseVisualStyleBackColor = false;
			btnce.Click += new EventHandler(btnce_Click);
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(btnne);
            Controls.Add(btnsix);
            Controls.Add(btntree);
            Controls.Add(btnegt);
            Controls.Add(btnto);
            Controls.Add(btnfive);
            Controls.Add(btnequel);
            Controls.Add(btnpls);
            Controls.Add(btndot);
            Controls.Add(btnse);
            Controls.Add(btnsvn);
            Controls.Add(btnfor);
            Controls.Add(btnone);
            Controls.Add(btnsub);
            Controls.Add(btnmu);
            Controls.Add(btnce);
			DoubleBuffered = true;
            Name = "DashboardPOSKeyboard";
            Size = new Size(317, 284);
			ResumeLayout(false);
		}
	}
}
