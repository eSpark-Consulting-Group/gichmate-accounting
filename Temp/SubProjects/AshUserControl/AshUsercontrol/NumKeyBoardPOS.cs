using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
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
			if (this.atNumberClick != null)
			{
				this.atNumberClick(this);
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			SendKeys.Send("{BACKSPACE}");
			if (this.atBackClick != null)
			{
				this.atBackClick(this);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
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

		private void btnEnter_Click(object sender, EventArgs e)
		{
			if (this.atEnterClick != null)
			{
				this.atEnterClick(this);
			}
			SendKeys.SendWait("{ENTER}");
		}

		private void btnPoint_Click(object sender, EventArgs e)
		{
			if (this.atNumberClick != null)
			{
				this.atNumberClick(this);
			}
			SendKeys.Send(".");
		}

		private void btnQty_Click(object sender, EventArgs e)
		{
			btnQty.BackColor = Color.FromArgb(245, 119, 35);
			btnQty.ForeColor = Color.White;
			btnRate.BackColor = Color.Gainsboro;
			btnRate.ForeColor = Color.FromArgb(61, 77, 125);
			if (this.atQtyClick != null)
			{
				this.atQtyClick(btnQty);
			}
		}

		private void btnRate_Click(object sender, EventArgs e)
		{
			if (this.atRateClick != null)
			{
				this.atRateClick(btnRate);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshUsercontrol.NumKeyBoardPOS));
			btnEnter = new AshUsercontrol.ExButton();
			btnQty = new AshUsercontrol.ExButton();
			btnRate = new AshUsercontrol.ExButton();
			btnClear = new AshUsercontrol.ExButton();
			btnDot = new AshUsercontrol.ExButton();
			btn9 = new AshUsercontrol.ExButton();
			btn0 = new AshUsercontrol.ExButton();
			btn8 = new AshUsercontrol.ExButton();
			btn7 = new AshUsercontrol.ExButton();
			btnBackSpace = new AshUsercontrol.ExButton();
			btn6 = new AshUsercontrol.ExButton();
			btn1 = new AshUsercontrol.ExButton();
			btn5 = new AshUsercontrol.ExButton();
			btn2 = new AshUsercontrol.ExButton();
			btn3 = new AshUsercontrol.ExButton();
			btn4 = new AshUsercontrol.ExButton();
			SuspendLayout();
			btnEnter.BackColor = System.Drawing.Color.Gainsboro;
			btnEnter.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnEnter.FlatAppearance.BorderSize = 0;
			btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEnter.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnEnter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnEnter.Location = new System.Drawing.Point(232, 187);
			btnEnter.Name = "btnEnter";
			btnEnter.Size = new System.Drawing.Size(75, 60);
			btnEnter.TabIndex = 29;
			btnEnter.Tag = "Enter";
			btnEnter.Text = "Enter";
			btnEnter.UseVisualStyleBackColor = false;
			btnEnter.Click += new System.EventHandler(btnEnter_Click);
			btnQty.BackColor = System.Drawing.Color.Gainsboro;
			btnQty.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnQty.FlatAppearance.BorderSize = 0;
			btnQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnQty.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnQty.Location = new System.Drawing.Point(232, 1);
			btnQty.Name = "btnQty";
			btnQty.Size = new System.Drawing.Size(75, 60);
			btnQty.TabIndex = 26;
			btnQty.Text = "Qty";
			btnQty.UseVisualStyleBackColor = false;
			btnQty.Click += new System.EventHandler(btnQty_Click);
			btnRate.BackColor = System.Drawing.Color.Gainsboro;
			btnRate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnRate.FlatAppearance.BorderSize = 0;
			btnRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnRate.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnRate.Location = new System.Drawing.Point(232, 63);
			btnRate.Name = "btnRate";
			btnRate.Size = new System.Drawing.Size(75, 60);
			btnRate.TabIndex = 27;
			btnRate.Text = "Rate";
			btnRate.UseVisualStyleBackColor = false;
			btnRate.Click += new System.EventHandler(btnRate_Click);
			btnClear.BackColor = System.Drawing.Color.Gainsboro;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnClear.Location = new System.Drawing.Point(232, 125);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 60);
			btnClear.TabIndex = 28;
			btnClear.Tag = "<";
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDot.BackColor = System.Drawing.Color.Gainsboro;
			btnDot.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDot.FlatAppearance.BorderSize = 0;
			btnDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btnDot.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnDot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnDot.Location = new System.Drawing.Point(1, 187);
			btnDot.Name = "btnDot";
			btnDot.Size = new System.Drawing.Size(75, 60);
			btnDot.TabIndex = 23;
			btnDot.Tag = ".";
			btnDot.Text = ".";
			btnDot.UseVisualStyleBackColor = false;
			btnDot.Click += new System.EventHandler(btnPoint_Click);
			btn9.BackColor = System.Drawing.Color.Gainsboro;
			btn9.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn9.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn9.Location = new System.Drawing.Point(155, 125);
			btn9.Name = "btn9";
			btn9.Size = new System.Drawing.Size(75, 60);
			btn9.TabIndex = 22;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new System.EventHandler(button1_Click);
			btn0.BackColor = System.Drawing.Color.Gainsboro;
			btn0.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn0.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn0.Location = new System.Drawing.Point(78, 187);
			btn0.Name = "btn0";
			btn0.Size = new System.Drawing.Size(75, 60);
			btn0.TabIndex = 24;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new System.EventHandler(button1_Click);
			btn8.BackColor = System.Drawing.Color.Gainsboro;
			btn8.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn8.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn8.Location = new System.Drawing.Point(78, 125);
			btn8.Name = "btn8";
			btn8.Size = new System.Drawing.Size(75, 60);
			btn8.TabIndex = 21;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new System.EventHandler(button1_Click);
			btn7.BackColor = System.Drawing.Color.Gainsboro;
			btn7.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn7.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn7.Location = new System.Drawing.Point(1, 125);
			btn7.Name = "btn7";
			btn7.Size = new System.Drawing.Size(75, 60);
			btn7.TabIndex = 20;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new System.EventHandler(button1_Click);
			btnBackSpace.BackColor = System.Drawing.Color.Gainsboro;
			btnBackSpace.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnBackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnBackSpace.Image = (System.Drawing.Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnBackSpace.Location = new System.Drawing.Point(155, 187);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new System.Drawing.Size(75, 60);
			btnBackSpace.TabIndex = 25;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new System.EventHandler(btnBack_Click);
			btn6.BackColor = System.Drawing.Color.Gainsboro;
			btn6.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn6.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn6.Location = new System.Drawing.Point(155, 63);
			btn6.Name = "btn6";
			btn6.Size = new System.Drawing.Size(75, 60);
			btn6.TabIndex = 19;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new System.EventHandler(button1_Click);
			btn1.BackColor = System.Drawing.Color.Gainsboro;
			btn1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn1.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn1.Location = new System.Drawing.Point(1, 1);
			btn1.Name = "btn1";
			btn1.Size = new System.Drawing.Size(75, 60);
			btn1.TabIndex = 14;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new System.EventHandler(button1_Click);
			btn5.BackColor = System.Drawing.Color.Gainsboro;
			btn5.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn5.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn5.Location = new System.Drawing.Point(78, 63);
			btn5.Name = "btn5";
			btn5.Size = new System.Drawing.Size(75, 60);
			btn5.TabIndex = 18;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new System.EventHandler(button1_Click);
			btn2.BackColor = System.Drawing.Color.Gainsboro;
			btn2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn2.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn2.Location = new System.Drawing.Point(78, 1);
			btn2.Name = "btn2";
			btn2.Size = new System.Drawing.Size(75, 60);
			btn2.TabIndex = 15;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new System.EventHandler(button1_Click);
			btn3.BackColor = System.Drawing.Color.Gainsboro;
			btn3.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn3.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn3.Location = new System.Drawing.Point(155, 1);
			btn3.Name = "btn3";
			btn3.Size = new System.Drawing.Size(75, 60);
			btn3.TabIndex = 16;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new System.EventHandler(button1_Click);
			btn4.BackColor = System.Drawing.Color.Gainsboro;
			btn4.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btn4.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn4.Location = new System.Drawing.Point(1, 63);
			btn4.Name = "btn4";
			btn4.Size = new System.Drawing.Size(75, 60);
			btn4.TabIndex = 17;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnEnter);
			base.Controls.Add(btnQty);
			base.Controls.Add(btnRate);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDot);
			base.Controls.Add(btn9);
			base.Controls.Add(btn0);
			base.Controls.Add(btn8);
			base.Controls.Add(btn7);
			base.Controls.Add(btnBackSpace);
			base.Controls.Add(btn6);
			base.Controls.Add(btn1);
			base.Controls.Add(btn5);
			base.Controls.Add(btn2);
			base.Controls.Add(btn3);
			base.Controls.Add(btn4);
			DoubleBuffered = true;
			Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "NumKeyBoardPOS";
			base.Size = new System.Drawing.Size(308, 248);
			ResumeLayout(false);
		}
	}
}
