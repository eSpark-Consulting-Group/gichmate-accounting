using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class NumKeyBoardPOS : UserControl
	{
		private IContainer components = null;

		private ExButton btnDot;

		private ExButton btn9;

		private ExButton btn0;

		private ExButton btn8;

		private ExButton btn7;

		private ExButton btnBackSpace;

		private ExButton btn6;

		private ExButton btn1;

		private ExButton btn5;

		private ExButton btn2;

		private ExButton btn3;

		private ExButton btn4;

		private ExButton btnEnter;

		private ExButton btnQty;

		private ExButton btnRate;

		private ExButton btnClear;

		public ExButton QtyButton
		{
			get
			{
				return btnQty;
			}
			set
			{
				btnQty = value;
			}
		}

		public ExButton RateButton
		{
			get
			{
				return btnRate;
			}
			set
			{
				btnRate = value;
			}
		}

		public event QtyClickEventHandler atQtyClick;

		public event RateClickEventHandler atRateClick;

		public event NumberClickEventHandler atNumberClick;

		public event NumberClickEventHandler atEnterClick;

		public event NumberClickEventHandler atBackClick;

		public event NumberClickEventHandler atClearClick;

		public NumKeyBoardPOS()
		{
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, value: true);
			DoubleBuffered = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SendKeys.Send((sender as Button).Tag.ToString());
			if (atNumberClick != null)
			{
                atNumberClick(this);
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			SendKeys.Send("{BACKSPACE}");
			if (atBackClick != null)
			{
                atBackClick(this);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
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

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if (atEnterClick != null)
			{
                atEnterClick(this);
			}
			SendKeys.SendWait("{ENTER}");
		}

		private void btnPoint_Click(object sender, EventArgs e)
		{
			if (atNumberClick != null)
			{
                atNumberClick(this);
			}
			SendKeys.Send(".");
		}

		private void btnQty_Click(object sender, EventArgs e)
		{
			btnQty.BackColor = Color.FromArgb(245, 119, 35);
			btnQty.ForeColor = Color.White;
			btnRate.BackColor = Color.Gainsboro;
			btnRate.ForeColor = Color.FromArgb(61, 77, 125);
			if (atQtyClick != null)
			{
                atQtyClick(btnQty);
			}
		}

		private void btnRate_Click(object sender, EventArgs e)
		{
			if (atRateClick != null)
			{
                atRateClick(btnRate);
			}
			btnRate.BackColor = Color.FromArgb(245, 119, 35);
			btnRate.ForeColor = Color.White;
			btnQty.BackColor = Color.Gainsboro;
			btnQty.ForeColor = Color.FromArgb(61, 77, 125);
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(NumKeyBoardPOS));
			btnEnter = new AccountsUserControls.ExButton();
			btnQty = new AccountsUserControls.ExButton();
			btnRate = new AccountsUserControls.ExButton();
			btnClear = new AccountsUserControls.ExButton();
			btnDot = new AccountsUserControls.ExButton();
			btn9 = new AccountsUserControls.ExButton();
			btn0 = new AccountsUserControls.ExButton();
			btn8 = new AccountsUserControls.ExButton();
			btn7 = new AccountsUserControls.ExButton();
			btnBackSpace = new AccountsUserControls.ExButton();
			btn6 = new AccountsUserControls.ExButton();
			btn1 = new AccountsUserControls.ExButton();
			btn5 = new AccountsUserControls.ExButton();
			btn2 = new AccountsUserControls.ExButton();
			btn3 = new AccountsUserControls.ExButton();
			btn4 = new AccountsUserControls.ExButton();
			SuspendLayout();
			btnEnter.BackColor = Color.Gainsboro;
			btnEnter.FlatAppearance.BorderColor = Color.LightGray;
			btnEnter.FlatAppearance.BorderSize = 0;
			btnEnter.FlatStyle = FlatStyle.Flat;
			btnEnter.Font = new Font("Microsoft Sans Serif", 12.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnEnter.ForeColor = Color.FromArgb(61, 77, 125);
			btnEnter.ImeMode = ImeMode.NoControl;
			btnEnter.Location = new Point(232, 187);
			btnEnter.Name = "btnEnter";
			btnEnter.Size = new Size(75, 60);
			btnEnter.TabIndex = 29;
			btnEnter.Tag = "Enter";
			btnEnter.Text = "Enter";
			btnEnter.UseVisualStyleBackColor = false;
			btnEnter.Click += new EventHandler(btnEnter_Click);
			btnQty.BackColor = Color.Gainsboro;
			btnQty.FlatAppearance.BorderColor = Color.LightGray;
			btnQty.FlatAppearance.BorderSize = 0;
			btnQty.FlatStyle = FlatStyle.Flat;
			btnQty.Font = new Font("Microsoft Sans Serif", 12.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnQty.ForeColor = Color.FromArgb(61, 77, 125);
			btnQty.ImeMode = ImeMode.NoControl;
			btnQty.Location = new Point(232, 1);
			btnQty.Name = "btnQty";
			btnQty.Size = new Size(75, 60);
			btnQty.TabIndex = 26;
			btnQty.Text = "Qty";
			btnQty.UseVisualStyleBackColor = false;
			btnQty.Click += new EventHandler(btnQty_Click);
			btnRate.BackColor = Color.Gainsboro;
			btnRate.FlatAppearance.BorderColor = Color.LightGray;
			btnRate.FlatAppearance.BorderSize = 0;
			btnRate.FlatStyle = FlatStyle.Flat;
			btnRate.Font = new Font("Microsoft Sans Serif", 12.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnRate.ForeColor = Color.FromArgb(61, 77, 125);
			btnRate.ImeMode = ImeMode.NoControl;
			btnRate.Location = new Point(232, 63);
			btnRate.Name = "btnRate";
			btnRate.Size = new Size(75, 60);
			btnRate.TabIndex = 27;
			btnRate.Text = "Rate";
			btnRate.UseVisualStyleBackColor = false;
			btnRate.Click += new EventHandler(btnRate_Click);
			btnClear.BackColor = Color.Gainsboro;
			btnClear.FlatAppearance.BorderColor = Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = FlatStyle.Flat;
			btnClear.Font = new Font("Microsoft Sans Serif", 12.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnClear.ForeColor = Color.FromArgb(61, 77, 125);
			btnClear.ImeMode = ImeMode.NoControl;
			btnClear.Location = new Point(232, 125);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(75, 60);
			btnClear.TabIndex = 28;
			btnClear.Tag = "<";
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new EventHandler(btnClear_Click);
			btnDot.BackColor = Color.Gainsboro;
			btnDot.FlatAppearance.BorderColor = Color.LightGray;
			btnDot.FlatAppearance.BorderSize = 0;
			btnDot.FlatStyle = FlatStyle.Flat;
			btnDot.Font = new Font("Microsoft Sans Serif", 24f);
			btnDot.ForeColor = Color.FromArgb(61, 77, 125);
			btnDot.ImeMode = ImeMode.NoControl;
			btnDot.Location = new Point(1, 187);
			btnDot.Name = "btnDot";
			btnDot.Size = new Size(75, 60);
			btnDot.TabIndex = 23;
			btnDot.Tag = ".";
			btnDot.Text = ".";
			btnDot.UseVisualStyleBackColor = false;
			btnDot.Click += new EventHandler(btnPoint_Click);
			btn9.BackColor = Color.Gainsboro;
			btn9.FlatAppearance.BorderColor = Color.LightGray;
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = FlatStyle.Flat;
			btn9.Font = new Font("Microsoft Sans Serif", 24f);
			btn9.ForeColor = Color.FromArgb(61, 77, 125);
			btn9.ImeMode = ImeMode.NoControl;
			btn9.Location = new Point(155, 125);
			btn9.Name = "btn9";
			btn9.Size = new Size(75, 60);
			btn9.TabIndex = 22;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new EventHandler(button1_Click);
			btn0.BackColor = Color.Gainsboro;
			btn0.FlatAppearance.BorderColor = Color.LightGray;
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = FlatStyle.Flat;
			btn0.Font = new Font("Microsoft Sans Serif", 24f);
			btn0.ForeColor = Color.FromArgb(61, 77, 125);
			btn0.ImeMode = ImeMode.NoControl;
			btn0.Location = new Point(78, 187);
			btn0.Name = "btn0";
			btn0.Size = new Size(75, 60);
			btn0.TabIndex = 24;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new EventHandler(button1_Click);
			btn8.BackColor = Color.Gainsboro;
			btn8.FlatAppearance.BorderColor = Color.LightGray;
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = FlatStyle.Flat;
			btn8.Font = new Font("Microsoft Sans Serif", 24f);
			btn8.ForeColor = Color.FromArgb(61, 77, 125);
			btn8.ImeMode = ImeMode.NoControl;
			btn8.Location = new Point(78, 125);
			btn8.Name = "btn8";
			btn8.Size = new Size(75, 60);
			btn8.TabIndex = 21;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new EventHandler(button1_Click);
			btn7.BackColor = Color.Gainsboro;
			btn7.FlatAppearance.BorderColor = Color.LightGray;
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = FlatStyle.Flat;
			btn7.Font = new Font("Microsoft Sans Serif", 24f);
			btn7.ForeColor = Color.FromArgb(61, 77, 125);
			btn7.ImeMode = ImeMode.NoControl;
			btn7.Location = new Point(1, 125);
			btn7.Name = "btn7";
			btn7.Size = new Size(75, 60);
			btn7.TabIndex = 20;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new EventHandler(button1_Click);
			btnBackSpace.BackColor = Color.Gainsboro;
			btnBackSpace.FlatAppearance.BorderColor = Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = FlatStyle.Flat;
			btnBackSpace.Font = new Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = Color.FromArgb(61, 77, 125);
			btnBackSpace.Image = (Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = ImeMode.NoControl;
			btnBackSpace.Location = new Point(155, 187);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new Size(75, 60);
			btnBackSpace.TabIndex = 25;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new EventHandler(btnBack_Click);
			btn6.BackColor = Color.Gainsboro;
			btn6.FlatAppearance.BorderColor = Color.LightGray;
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = FlatStyle.Flat;
			btn6.Font = new Font("Microsoft Sans Serif", 24f);
			btn6.ForeColor = Color.FromArgb(61, 77, 125);
			btn6.ImeMode = ImeMode.NoControl;
			btn6.Location = new Point(155, 63);
			btn6.Name = "btn6";
			btn6.Size = new Size(75, 60);
			btn6.TabIndex = 19;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new EventHandler(button1_Click);
			btn1.BackColor = Color.Gainsboro;
			btn1.FlatAppearance.BorderColor = Color.LightGray;
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = FlatStyle.Flat;
			btn1.Font = new Font("Microsoft Sans Serif", 24f);
			btn1.ForeColor = Color.FromArgb(61, 77, 125);
			btn1.ImeMode = ImeMode.NoControl;
			btn1.Location = new Point(1, 1);
			btn1.Name = "btn1";
			btn1.Size = new Size(75, 60);
			btn1.TabIndex = 14;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new EventHandler(button1_Click);
			btn5.BackColor = Color.Gainsboro;
			btn5.FlatAppearance.BorderColor = Color.LightGray;
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = FlatStyle.Flat;
			btn5.Font = new Font("Microsoft Sans Serif", 24f);
			btn5.ForeColor = Color.FromArgb(61, 77, 125);
			btn5.ImeMode = ImeMode.NoControl;
			btn5.Location = new Point(78, 63);
			btn5.Name = "btn5";
			btn5.Size = new Size(75, 60);
			btn5.TabIndex = 18;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new EventHandler(button1_Click);
			btn2.BackColor = Color.Gainsboro;
			btn2.FlatAppearance.BorderColor = Color.LightGray;
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = FlatStyle.Flat;
			btn2.Font = new Font("Microsoft Sans Serif", 24f);
			btn2.ForeColor = Color.FromArgb(61, 77, 125);
			btn2.ImeMode = ImeMode.NoControl;
			btn2.Location = new Point(78, 1);
			btn2.Name = "btn2";
			btn2.Size = new Size(75, 60);
			btn2.TabIndex = 15;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new EventHandler(button1_Click);
			btn3.BackColor = Color.Gainsboro;
			btn3.FlatAppearance.BorderColor = Color.LightGray;
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = FlatStyle.Flat;
			btn3.Font = new Font("Microsoft Sans Serif", 24f);
			btn3.ForeColor = Color.FromArgb(61, 77, 125);
			btn3.ImeMode = ImeMode.NoControl;
			btn3.Location = new Point(155, 1);
			btn3.Name = "btn3";
			btn3.Size = new Size(75, 60);
			btn3.TabIndex = 16;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new EventHandler(button1_Click);
			btn4.BackColor = Color.Gainsboro;
			btn4.FlatAppearance.BorderColor = Color.LightGray;
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = FlatStyle.Flat;
			btn4.Font = new Font("Microsoft Sans Serif", 24f);
			btn4.ForeColor = Color.FromArgb(61, 77, 125);
			btn4.ImeMode = ImeMode.NoControl;
			btn4.Location = new Point(1, 63);
			btn4.Name = "btn4";
			btn4.Size = new Size(75, 60);
			btn4.TabIndex = 17;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new EventHandler(button1_Click);
            AutoScaleDimensions = new SizeF(8f, 16f);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEnter);
            Controls.Add(btnQty);
            Controls.Add(btnRate);
            Controls.Add(btnClear);
            Controls.Add(btnDot);
            Controls.Add(btn9);
            Controls.Add(btn0);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(btnBackSpace);
            Controls.Add(btn6);
            Controls.Add(btn1);
            Controls.Add(btn5);
            Controls.Add(btn2);
            Controls.Add(btn3);
            Controls.Add(btn4);
			DoubleBuffered = true;
			Font = new Font("Microsoft Sans Serif", 10f);
            Margin = new Padding(4);
            Name = "NumKeyBoardPOS";
            Size = new Size(308, 248);
			ResumeLayout(false);
		}
	}
}
