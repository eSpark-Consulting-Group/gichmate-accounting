using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
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
			if (this.atClearClick != null)
			{
				this.atClearClick(this);
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
			if (this.atEqualClick != null)
			{
				this.atEqualClick(this);
			}
		}

		private void btnpls_Click(object sender, EventArgs e)
		{
			if (this.atAddClick != null)
			{
				this.atAddClick(this);
			}
		}

		private void btnsub_Click(object sender, EventArgs e)
		{
			if (this.atSubClick != null)
			{
				this.atSubClick(this);
			}
		}

		private void btnmu_Click(object sender, EventArgs e)
		{
			if (this.atStarClick != null)
			{
				this.atStarClick(this);
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
			btnne = new AshUsercontrol.ExButton();
			btnsix = new AshUsercontrol.ExButton();
			btntree = new AshUsercontrol.ExButton();
			btnegt = new AshUsercontrol.ExButton();
			btnto = new AshUsercontrol.ExButton();
			btnfive = new AshUsercontrol.ExButton();
			btnequel = new AshUsercontrol.ExButton();
			btnpls = new AshUsercontrol.ExButton();
			btndot = new AshUsercontrol.ExButton();
			btnse = new AshUsercontrol.ExButton();
			btnsvn = new AshUsercontrol.ExButton();
			btnfor = new AshUsercontrol.ExButton();
			btnone = new AshUsercontrol.ExButton();
			btnsub = new AshUsercontrol.ExButton();
			btnmu = new AshUsercontrol.ExButton();
			btnce = new AshUsercontrol.ExButton();
			SuspendLayout();
			btnne.BackColor = System.Drawing.Color.Gainsboro;
			btnne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnne.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnne.FlatAppearance.BorderSize = 0;
			btnne.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnne.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnne.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnne.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnne.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnne.Location = new System.Drawing.Point(161, 60);
			btnne.Name = "btnne";
			btnne.Size = new System.Drawing.Size(74, 51);
			btnne.TabIndex = 14;
			btnne.Tag = "9";
			btnne.Text = "9";
			btnne.UseVisualStyleBackColor = false;
			btnne.Click += new System.EventHandler(btnone_Click);
			btnsix.BackColor = System.Drawing.Color.Gainsboro;
			btnsix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnsix.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsix.FlatAppearance.BorderSize = 0;
			btnsix.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsix.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnsix.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnsix.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnsix.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnsix.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnsix.Location = new System.Drawing.Point(161, 117);
			btnsix.Name = "btnsix";
			btnsix.Size = new System.Drawing.Size(74, 51);
			btnsix.TabIndex = 15;
			btnsix.Tag = "6";
			btnsix.Text = "6";
			btnsix.UseVisualStyleBackColor = false;
			btnsix.Click += new System.EventHandler(btnone_Click);
			btntree.BackColor = System.Drawing.Color.Gainsboro;
			btntree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btntree.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btntree.FlatAppearance.BorderSize = 0;
			btntree.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btntree.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btntree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btntree.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btntree.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btntree.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btntree.Location = new System.Drawing.Point(161, 174);
			btntree.Name = "btntree";
			btntree.Size = new System.Drawing.Size(74, 51);
			btntree.TabIndex = 16;
			btntree.Tag = "3";
			btntree.Text = "3";
			btntree.UseVisualStyleBackColor = false;
			btntree.Click += new System.EventHandler(btnone_Click);
			btnegt.BackColor = System.Drawing.Color.Gainsboro;
			btnegt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnegt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnegt.FlatAppearance.BorderSize = 0;
			btnegt.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnegt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnegt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnegt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnegt.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnegt.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnegt.Location = new System.Drawing.Point(81, 60);
			btnegt.Name = "btnegt";
			btnegt.Size = new System.Drawing.Size(74, 51);
			btnegt.TabIndex = 11;
			btnegt.Tag = "8";
			btnegt.Text = "8";
			btnegt.UseVisualStyleBackColor = false;
			btnegt.Click += new System.EventHandler(btnone_Click);
			btnto.BackColor = System.Drawing.Color.Gainsboro;
			btnto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnto.FlatAppearance.BorderSize = 0;
			btnto.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnto.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnto.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnto.Location = new System.Drawing.Point(82, 174);
			btnto.Name = "btnto";
			btnto.Size = new System.Drawing.Size(74, 51);
			btnto.TabIndex = 12;
			btnto.Tag = "2";
			btnto.Text = "2";
			btnto.UseVisualStyleBackColor = false;
			btnto.Click += new System.EventHandler(btnone_Click);
			btnfive.BackColor = System.Drawing.Color.Gainsboro;
			btnfive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnfive.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnfive.FlatAppearance.BorderSize = 0;
			btnfive.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnfive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnfive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnfive.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnfive.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnfive.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnfive.Location = new System.Drawing.Point(82, 117);
			btnfive.Name = "btnfive";
			btnfive.Size = new System.Drawing.Size(74, 51);
			btnfive.TabIndex = 13;
			btnfive.Tag = "5";
			btnfive.Text = "5";
			btnfive.UseVisualStyleBackColor = false;
			btnfive.Click += new System.EventHandler(btnone_Click);
			btnequel.BackColor = System.Drawing.Color.Gainsboro;
			btnequel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnequel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnequel.FlatAppearance.BorderSize = 0;
			btnequel.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnequel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnequel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnequel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnequel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnequel.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnequel.Location = new System.Drawing.Point(240, 174);
			btnequel.Name = "btnequel";
			btnequel.Size = new System.Drawing.Size(74, 108);
			btnequel.TabIndex = 8;
			btnequel.Tag = "";
			btnequel.Text = "=";
			btnequel.UseVisualStyleBackColor = false;
			btnequel.Click += new System.EventHandler(btnequel_Click);
			btnpls.BackColor = System.Drawing.Color.Gainsboro;
			btnpls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnpls.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnpls.FlatAppearance.BorderSize = 0;
			btnpls.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnpls.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnpls.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnpls.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnpls.Font = new System.Drawing.Font("MS Reference Sans Serif", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnpls.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnpls.Location = new System.Drawing.Point(240, 60);
			btnpls.Name = "btnpls";
			btnpls.Size = new System.Drawing.Size(74, 108);
			btnpls.TabIndex = 9;
			btnpls.Tag = "";
			btnpls.Text = "+";
			btnpls.UseVisualStyleBackColor = false;
			btnpls.Click += new System.EventHandler(btnpls_Click);
			btndot.BackColor = System.Drawing.Color.Gainsboro;
			btndot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btndot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btndot.FlatAppearance.BorderSize = 0;
			btndot.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btndot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btndot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btndot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btndot.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btndot.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btndot.Location = new System.Drawing.Point(161, 231);
			btndot.Name = "btndot";
			btndot.Size = new System.Drawing.Size(74, 51);
			btndot.TabIndex = 10;
			btndot.Tag = ".";
			btndot.Text = ".";
			btndot.UseVisualStyleBackColor = false;
			btndot.Click += new System.EventHandler(btnone_Click);
			btnse.BackColor = System.Drawing.Color.Gainsboro;
			btnse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnse.FlatAppearance.BorderSize = 0;
			btnse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnse.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnse.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnse.Location = new System.Drawing.Point(2, 231);
			btnse.Name = "btnse";
			btnse.Size = new System.Drawing.Size(154, 51);
			btnse.TabIndex = 7;
			btnse.Tag = "0";
			btnse.Text = "0";
			btnse.UseVisualStyleBackColor = false;
			btnse.Click += new System.EventHandler(btnone_Click);
			btnsvn.BackColor = System.Drawing.Color.Gainsboro;
			btnsvn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnsvn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsvn.FlatAppearance.BorderSize = 0;
			btnsvn.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsvn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnsvn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnsvn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnsvn.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnsvn.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnsvn.Location = new System.Drawing.Point(2, 60);
			btnsvn.Name = "btnsvn";
			btnsvn.Size = new System.Drawing.Size(74, 51);
			btnsvn.TabIndex = 4;
			btnsvn.Tag = "7";
			btnsvn.Text = "7";
			btnsvn.UseVisualStyleBackColor = false;
			btnsvn.Click += new System.EventHandler(btnone_Click);
			btnfor.BackColor = System.Drawing.Color.Gainsboro;
			btnfor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnfor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnfor.FlatAppearance.BorderSize = 0;
			btnfor.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnfor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnfor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnfor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnfor.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnfor.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnfor.Location = new System.Drawing.Point(2, 117);
			btnfor.Name = "btnfor";
			btnfor.Size = new System.Drawing.Size(74, 51);
			btnfor.TabIndex = 5;
			btnfor.Tag = "4";
			btnfor.Text = "4";
			btnfor.UseVisualStyleBackColor = false;
			btnfor.Click += new System.EventHandler(btnone_Click);
			btnone.BackColor = System.Drawing.Color.Gainsboro;
			btnone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnone.FlatAppearance.BorderSize = 0;
			btnone.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnone.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnone.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnone.Location = new System.Drawing.Point(2, 174);
			btnone.Name = "btnone";
			btnone.Size = new System.Drawing.Size(74, 51);
			btnone.TabIndex = 6;
			btnone.Tag = "1";
			btnone.Text = "1";
			btnone.UseVisualStyleBackColor = false;
			btnone.Click += new System.EventHandler(btnone_Click);
			btnsub.BackColor = System.Drawing.Color.Gainsboro;
			btnsub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnsub.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsub.FlatAppearance.BorderSize = 0;
			btnsub.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnsub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnsub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnsub.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnsub.Font = new System.Drawing.Font("MS Reference Sans Serif", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnsub.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnsub.Location = new System.Drawing.Point(240, 3);
			btnsub.Name = "btnsub";
			btnsub.Size = new System.Drawing.Size(74, 51);
			btnsub.TabIndex = 3;
			btnsub.Tag = "";
			btnsub.Text = "-";
			btnsub.UseVisualStyleBackColor = false;
			btnsub.Click += new System.EventHandler(btnsub_Click);
			btnmu.BackColor = System.Drawing.Color.Gainsboro;
			btnmu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnmu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnmu.FlatAppearance.BorderSize = 0;
			btnmu.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnmu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnmu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnmu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnmu.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnmu.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnmu.Location = new System.Drawing.Point(161, 3);
			btnmu.Name = "btnmu";
			btnmu.Size = new System.Drawing.Size(74, 51);
			btnmu.TabIndex = 2;
			btnmu.Tag = "";
			btnmu.Text = "*";
			btnmu.UseVisualStyleBackColor = false;
			btnmu.Click += new System.EventHandler(btnmu_Click);
			btnce.BackColor = System.Drawing.Color.Gainsboro;
			btnce.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			btnce.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnce.FlatAppearance.BorderSize = 0;
			btnce.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnce.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			btnce.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			btnce.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnce.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnce.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnce.Location = new System.Drawing.Point(2, 3);
			btnce.Name = "btnce";
			btnce.Size = new System.Drawing.Size(153, 51);
			btnce.TabIndex = 1;
			btnce.Tag = "<";
			btnce.Text = "CE";
			btnce.UseVisualStyleBackColor = false;
			btnce.Click += new System.EventHandler(btnce_Click);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			base.Controls.Add(btnne);
			base.Controls.Add(btnsix);
			base.Controls.Add(btntree);
			base.Controls.Add(btnegt);
			base.Controls.Add(btnto);
			base.Controls.Add(btnfive);
			base.Controls.Add(btnequel);
			base.Controls.Add(btnpls);
			base.Controls.Add(btndot);
			base.Controls.Add(btnse);
			base.Controls.Add(btnsvn);
			base.Controls.Add(btnfor);
			base.Controls.Add(btnone);
			base.Controls.Add(btnsub);
			base.Controls.Add(btnmu);
			base.Controls.Add(btnce);
			DoubleBuffered = true;
			base.Name = "DashboardPOSKeyboard";
			base.Size = new System.Drawing.Size(317, 284);
			ResumeLayout(false);
		}
	}
}
