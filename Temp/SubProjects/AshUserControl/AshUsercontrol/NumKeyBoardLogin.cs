using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class NumKeyBoardLogin : UserControl
	{
		private IContainer components = null;

		public ExButton btnClear;

		public ExButton btn9;

		public ExButton btn0;

		public ExButton btn8;

		public ExButton btn7;

		public ExButton btnBackSpace;

		public ExButton btn6;

		public ExButton btn1;

		public ExButton btn5;

		public ExButton btn2;

		public ExButton btn3;

		public ExButton btn4;

		public NumKeyBoardLogin()
		{
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, value: true);
			DoubleBuffered = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SendKeys.Send((sender as Button).Tag.ToString());
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			SendKeys.Send("{BACKSPACE}");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Form form = FindForm();
			if (form != null && form.ActiveControl != null && (form.ActiveControl.GetType() == typeof(TextBox) || form.ActiveControl.GetType().BaseType == typeof(TextBox)))
			{
				(form.ActiveControl as TextBox).Text = "";
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshUsercontrol.NumKeyBoardLogin));
			btnClear = new AshUsercontrol.ExButton();
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
			btnClear.BackColor = System.Drawing.Color.Gainsboro;
			btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnClear.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btnClear.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnClear.Location = new System.Drawing.Point(1, 187);
			btnClear.Margin = new System.Windows.Forms.Padding(4);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 60);
			btnClear.TabIndex = 35;
			btnClear.Tag = "CE";
			btnClear.Text = "CE";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btn9.BackColor = System.Drawing.Color.Gainsboro;
			btn9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn9.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn9.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn9.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn9.Location = new System.Drawing.Point(155, 125);
			btn9.Margin = new System.Windows.Forms.Padding(4);
			btn9.Name = "btn9";
			btn9.Size = new System.Drawing.Size(75, 60);
			btn9.TabIndex = 34;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new System.EventHandler(button1_Click);
			btn0.BackColor = System.Drawing.Color.Gainsboro;
			btn0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn0.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn0.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn0.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn0.Location = new System.Drawing.Point(79, 187);
			btn0.Margin = new System.Windows.Forms.Padding(4);
			btn0.Name = "btn0";
			btn0.Size = new System.Drawing.Size(75, 60);
			btn0.TabIndex = 36;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new System.EventHandler(button1_Click);
			btn8.BackColor = System.Drawing.Color.Gainsboro;
			btn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn8.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn8.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn8.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn8.Location = new System.Drawing.Point(79, 125);
			btn8.Margin = new System.Windows.Forms.Padding(4);
			btn8.Name = "btn8";
			btn8.Size = new System.Drawing.Size(75, 60);
			btn8.TabIndex = 33;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new System.EventHandler(button1_Click);
			btn7.BackColor = System.Drawing.Color.Gainsboro;
			btn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn7.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn7.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn7.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn7.Location = new System.Drawing.Point(1, 125);
			btn7.Margin = new System.Windows.Forms.Padding(4);
			btn7.Name = "btn7";
			btn7.Size = new System.Drawing.Size(75, 60);
			btn7.TabIndex = 32;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new System.EventHandler(button1_Click);
			btnBackSpace.BackColor = System.Drawing.Color.Gainsboro;
			btnBackSpace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnBackSpace.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnBackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			//btnBackSpace.Image = (System.Drawing.Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnBackSpace.Location = new System.Drawing.Point(155, 187);
			btnBackSpace.Margin = new System.Windows.Forms.Padding(4);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new System.Drawing.Size(75, 60);
			btnBackSpace.TabIndex = 37;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new System.EventHandler(btnBack_Click);
			btn6.BackColor = System.Drawing.Color.Gainsboro;
			btn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn6.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn6.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn6.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn6.Location = new System.Drawing.Point(155, 63);
			btn6.Margin = new System.Windows.Forms.Padding(4);
			btn6.Name = "btn6";
			btn6.Size = new System.Drawing.Size(75, 60);
			btn6.TabIndex = 31;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new System.EventHandler(button1_Click);
			btn1.BackColor = System.Drawing.Color.Gainsboro;
			btn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn1.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn1.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn1.Location = new System.Drawing.Point(1, 1);
			btn1.Margin = new System.Windows.Forms.Padding(4);
			btn1.Name = "btn1";
			btn1.Size = new System.Drawing.Size(75, 60);
			btn1.TabIndex = 26;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new System.EventHandler(button1_Click);
			btn5.BackColor = System.Drawing.Color.Gainsboro;
			btn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn5.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn5.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn5.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn5.Location = new System.Drawing.Point(79, 63);
			btn5.Margin = new System.Windows.Forms.Padding(4);
			btn5.Name = "btn5";
			btn5.Size = new System.Drawing.Size(75, 60);
			btn5.TabIndex = 30;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new System.EventHandler(button1_Click);
			btn2.BackColor = System.Drawing.Color.Gainsboro;
			btn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn2.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn2.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn2.Location = new System.Drawing.Point(79, 1);
			btn2.Margin = new System.Windows.Forms.Padding(4);
			btn2.Name = "btn2";
			btn2.Size = new System.Drawing.Size(75, 60);
			btn2.TabIndex = 27;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new System.EventHandler(button1_Click);
			btn3.BackColor = System.Drawing.Color.Gainsboro;
			btn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn3.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn3.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn3.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn3.Location = new System.Drawing.Point(155, 1);
			btn3.Margin = new System.Windows.Forms.Padding(4);
			btn3.Name = "btn3";
			btn3.Size = new System.Drawing.Size(75, 60);
			btn3.TabIndex = 28;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new System.EventHandler(button1_Click);
			btn4.BackColor = System.Drawing.Color.Gainsboro;
			btn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btn4.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btn4.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75f, System.Drawing.FontStyle.Bold);
			btn4.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btn4.Location = new System.Drawing.Point(1, 63);
			btn4.Margin = new System.Windows.Forms.Padding(4);
			btn4.Name = "btn4";
			btn4.Size = new System.Drawing.Size(75, 60);
			btn4.TabIndex = 29;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnClear);
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
			base.Name = "NumKeyBoardLogin";
			base.Size = new System.Drawing.Size(231, 249);
			ResumeLayout(false);
		}
	}
}
