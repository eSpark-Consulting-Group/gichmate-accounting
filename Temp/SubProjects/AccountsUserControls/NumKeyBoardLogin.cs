using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(NumKeyBoardLogin));
			btnClear = new AccountsUserControls.ExButton();
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
			btnClear.BackColor = Color.Gainsboro;
			btnClear.BackgroundImageLayout = ImageLayout.Stretch;
			btnClear.FlatAppearance.BorderColor = Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = FlatStyle.Popup;
			btnClear.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btnClear.ForeColor = Color.FromArgb(61, 77, 125);
			btnClear.ImeMode = ImeMode.NoControl;
			btnClear.Location = new Point(1, 187);
			btnClear.Margin = new Padding(4);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(75, 60);
			btnClear.TabIndex = 35;
			btnClear.Tag = "CE";
			btnClear.Text = "CE";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new EventHandler(btnClear_Click);
			btn9.BackColor = Color.Gainsboro;
			btn9.BackgroundImageLayout = ImageLayout.Stretch;
			btn9.FlatAppearance.BorderColor = Color.LightGray;
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = FlatStyle.Popup;
			btn9.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn9.ForeColor = Color.FromArgb(61, 77, 125);
			btn9.ImeMode = ImeMode.NoControl;
			btn9.Location = new Point(155, 125);
			btn9.Margin = new Padding(4);
			btn9.Name = "btn9";
			btn9.Size = new Size(75, 60);
			btn9.TabIndex = 34;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new EventHandler(button1_Click);
			btn0.BackColor = Color.Gainsboro;
			btn0.BackgroundImageLayout = ImageLayout.Stretch;
			btn0.FlatAppearance.BorderColor = Color.LightGray;
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = FlatStyle.Popup;
			btn0.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn0.ForeColor = Color.FromArgb(61, 77, 125);
			btn0.ImeMode = ImeMode.NoControl;
			btn0.Location = new Point(79, 187);
			btn0.Margin = new Padding(4);
			btn0.Name = "btn0";
			btn0.Size = new Size(75, 60);
			btn0.TabIndex = 36;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new EventHandler(button1_Click);
			btn8.BackColor = Color.Gainsboro;
			btn8.BackgroundImageLayout = ImageLayout.Stretch;
			btn8.FlatAppearance.BorderColor = Color.LightGray;
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = FlatStyle.Popup;
			btn8.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn8.ForeColor = Color.FromArgb(61, 77, 125);
			btn8.ImeMode = ImeMode.NoControl;
			btn8.Location = new Point(79, 125);
			btn8.Margin = new Padding(4);
			btn8.Name = "btn8";
			btn8.Size = new Size(75, 60);
			btn8.TabIndex = 33;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new EventHandler(button1_Click);
			btn7.BackColor = Color.Gainsboro;
			btn7.BackgroundImageLayout = ImageLayout.Stretch;
			btn7.FlatAppearance.BorderColor = Color.LightGray;
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = FlatStyle.Popup;
			btn7.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn7.ForeColor = Color.FromArgb(61, 77, 125);
			btn7.ImeMode = ImeMode.NoControl;
			btn7.Location = new Point(1, 125);
			btn7.Margin = new Padding(4);
			btn7.Name = "btn7";
			btn7.Size = new Size(75, 60);
			btn7.TabIndex = 32;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new EventHandler(button1_Click);
			btnBackSpace.BackColor = Color.Gainsboro;
			btnBackSpace.BackgroundImageLayout = ImageLayout.Stretch;
			btnBackSpace.FlatAppearance.BorderColor = Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = FlatStyle.Popup;
			btnBackSpace.Font = new Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = Color.FromArgb(61, 77, 125);
			//btnBackSpace.Image = (System.Drawing.Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = ImeMode.NoControl;
			btnBackSpace.Location = new Point(155, 187);
			btnBackSpace.Margin = new Padding(4);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new Size(75, 60);
			btnBackSpace.TabIndex = 37;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new EventHandler(btnBack_Click);
			btn6.BackColor = Color.Gainsboro;
			btn6.BackgroundImageLayout = ImageLayout.Stretch;
			btn6.FlatAppearance.BorderColor = Color.LightGray;
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = FlatStyle.Popup;
			btn6.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn6.ForeColor = Color.FromArgb(61, 77, 125);
			btn6.ImeMode = ImeMode.NoControl;
			btn6.Location = new Point(155, 63);
			btn6.Margin = new Padding(4);
			btn6.Name = "btn6";
			btn6.Size = new Size(75, 60);
			btn6.TabIndex = 31;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new EventHandler(button1_Click);
			btn1.BackColor = Color.Gainsboro;
			btn1.BackgroundImageLayout = ImageLayout.Stretch;
			btn1.FlatAppearance.BorderColor = Color.LightGray;
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = FlatStyle.Popup;
			btn1.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn1.ForeColor = Color.FromArgb(61, 77, 125);
			btn1.ImeMode = ImeMode.NoControl;
			btn1.Location = new Point(1, 1);
			btn1.Margin = new Padding(4);
			btn1.Name = "btn1";
			btn1.Size = new Size(75, 60);
			btn1.TabIndex = 26;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new EventHandler(button1_Click);
			btn5.BackColor = Color.Gainsboro;
			btn5.BackgroundImageLayout = ImageLayout.Stretch;
			btn5.FlatAppearance.BorderColor = Color.LightGray;
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = FlatStyle.Popup;
			btn5.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn5.ForeColor = Color.FromArgb(61, 77, 125);
			btn5.ImeMode = ImeMode.NoControl;
			btn5.Location = new Point(79, 63);
			btn5.Margin = new Padding(4);
			btn5.Name = "btn5";
			btn5.Size = new Size(75, 60);
			btn5.TabIndex = 30;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new EventHandler(button1_Click);
			btn2.BackColor = Color.Gainsboro;
			btn2.BackgroundImageLayout = ImageLayout.Stretch;
			btn2.FlatAppearance.BorderColor = Color.LightGray;
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = FlatStyle.Popup;
			btn2.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn2.ForeColor = Color.FromArgb(61, 77, 125);
			btn2.ImeMode = ImeMode.NoControl;
			btn2.Location = new Point(79, 1);
			btn2.Margin = new Padding(4);
			btn2.Name = "btn2";
			btn2.Size = new Size(75, 60);
			btn2.TabIndex = 27;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new EventHandler(button1_Click);
			btn3.BackColor = Color.Gainsboro;
			btn3.BackgroundImageLayout = ImageLayout.Stretch;
			btn3.FlatAppearance.BorderColor = Color.LightGray;
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = FlatStyle.Popup;
			btn3.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn3.ForeColor = Color.FromArgb(61, 77, 125);
			btn3.ImeMode = ImeMode.NoControl;
			btn3.Location = new Point(155, 1);
			btn3.Margin = new Padding(4);
			btn3.Name = "btn3";
			btn3.Size = new Size(75, 60);
			btn3.TabIndex = 28;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new EventHandler(button1_Click);
			btn4.BackColor = Color.Gainsboro;
			btn4.BackgroundImageLayout = ImageLayout.Stretch;
			btn4.FlatAppearance.BorderColor = Color.LightGray;
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = FlatStyle.Popup;
			btn4.Font = new Font("MS Reference Sans Serif", 21.75f, FontStyle.Bold);
			btn4.ForeColor = Color.FromArgb(61, 77, 125);
			btn4.ImeMode = ImeMode.NoControl;
			btn4.Location = new Point(1, 63);
			btn4.Margin = new Padding(4);
			btn4.Name = "btn4";
			btn4.Size = new Size(75, 60);
			btn4.TabIndex = 29;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new EventHandler(button1_Click);
			AutoScaleDimensions = new SizeF(8f, 16f);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnClear);
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
			Name = "NumKeyBoardLogin";
			Size = new Size(231, 249);
			ResumeLayout(false);
		}
	}
}
