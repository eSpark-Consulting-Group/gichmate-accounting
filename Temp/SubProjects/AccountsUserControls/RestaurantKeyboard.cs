using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class RestaurantKeyboard : UserControl
	{
		private IContainer components = null;

		public ExButton btn5;

		public ExButton btn4;

		public ExButton btn3;

		public ExButton btn2;

		public ExButton btn1;

		public ExButton btn6;

		public ExButton btn7;

		public ExButton btn8;

		public ExButton btn9;

		public ExButton btn0;

		public ExButton btnClear;

		public ExButton btnBackSpace;

		public event NumberClickEventHandler atNumberClick;

		public RestaurantKeyboard()
		{
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, value: true);
			DoubleBuffered = true;
		}

		private void btnone_Click(object sender, EventArgs e)
		{
			SendKeys.Send((sender as Button).Tag.ToString());
			if (atNumberClick != null)
			{
				atNumberClick(this);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Form form = FindForm();
			if (form != null && form.ActiveControl != null && (form.ActiveControl.GetType() == typeof(TextBox) || form.ActiveControl.GetType().BaseType == typeof(TextBox)))
			{
				(form.ActiveControl as TextBox).Text = "";
			}
		}

		private void btnBackSpace_Click(object sender, EventArgs e)
		{
			SendKeys.Send("{BACKSPACE}");
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(RestaurantKeyboard));
			btn5 = new AccountsUserControls.ExButton();
			btn4 = new AccountsUserControls.ExButton();
			btn3 = new AccountsUserControls.ExButton();
			btn2 = new AccountsUserControls.ExButton();
			btn1 = new AccountsUserControls.ExButton();
			btn6 = new AccountsUserControls.ExButton();
			btn7 = new AccountsUserControls.ExButton();
			btn8 = new AccountsUserControls.ExButton();
			btn9 = new AccountsUserControls.ExButton();
			btn0 = new AccountsUserControls.ExButton();
			btnClear = new AccountsUserControls.ExButton();
			btnBackSpace = new AccountsUserControls.ExButton();
			SuspendLayout();
			btn5.BackColor = Color.FromArgb(14, 72, 119);
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = FlatStyle.Flat;
			btn5.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn5.ForeColor = Color.White;
			btn5.Location = new Point(234, 3);
			btn5.Name = "btn5";
			btn5.Size = new Size(55, 30);
			btn5.TabIndex = 2;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new EventHandler(btnone_Click);
			btn4.BackColor = Color.FromArgb(14, 72, 119);
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = FlatStyle.Flat;
			btn4.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn4.ForeColor = Color.White;
			btn4.Location = new Point(176, 3);
			btn4.Name = "btn4";
			btn4.Size = new Size(55, 30);
			btn4.TabIndex = 3;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new EventHandler(btnone_Click);
			btn3.BackColor = Color.FromArgb(14, 72, 119);
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = FlatStyle.Flat;
			btn3.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn3.ForeColor = Color.White;
			btn3.Location = new Point(118, 3);
			btn3.Name = "btn3";
			btn3.Size = new Size(55, 30);
			btn3.TabIndex = 4;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new EventHandler(btnone_Click);
			btn2.BackColor = Color.FromArgb(14, 72, 119);
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = FlatStyle.Flat;
			btn2.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn2.ForeColor = Color.White;
			btn2.Location = new Point(60, 3);
			btn2.Name = "btn2";
			btn2.Size = new Size(55, 30);
			btn2.TabIndex = 5;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new EventHandler(btnone_Click);
			btn1.BackColor = Color.FromArgb(14, 72, 119);
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = FlatStyle.Flat;
			btn1.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn1.ForeColor = Color.White;
			btn1.Location = new Point(2, 3);
			btn1.Name = "btn1";
			btn1.Size = new Size(55, 30);
			btn1.TabIndex = 6;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new EventHandler(btnone_Click);
			btn6.BackColor = Color.FromArgb(14, 72, 119);
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = FlatStyle.Flat;
			btn6.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn6.ForeColor = Color.White;
			btn6.Location = new Point(2, 35);
			btn6.Name = "btn6";
			btn6.Size = new Size(55, 30);
			btn6.TabIndex = 6;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new EventHandler(btnone_Click);
			btn7.BackColor = Color.FromArgb(14, 72, 119);
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = FlatStyle.Flat;
			btn7.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn7.ForeColor = Color.White;
			btn7.Location = new Point(60, 35);
			btn7.Name = "btn7";
			btn7.Size = new Size(55, 30);
			btn7.TabIndex = 5;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new EventHandler(btnone_Click);
			btn8.BackColor = Color.FromArgb(14, 72, 119);
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = FlatStyle.Flat;
			btn8.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn8.ForeColor = Color.White;
			btn8.Location = new Point(118, 35);
			btn8.Name = "btn8";
			btn8.Size = new Size(55, 30);
			btn8.TabIndex = 4;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new EventHandler(btnone_Click);
			btn9.BackColor = Color.FromArgb(14, 72, 119);
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = FlatStyle.Flat;
			btn9.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn9.ForeColor = Color.White;
			btn9.Location = new Point(176, 35);
			btn9.Name = "btn9";
			btn9.Size = new Size(55, 30);
			btn9.TabIndex = 3;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new EventHandler(btnone_Click);
			btn0.BackColor = Color.FromArgb(14, 72, 119);
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = FlatStyle.Flat;
			btn0.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btn0.ForeColor = Color.White;
			btn0.Location = new Point(234, 35);
			btn0.Name = "btn0";
			btn0.Size = new Size(55, 30);
			btn0.TabIndex = 2;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new EventHandler(btnone_Click);
			btnClear.BackColor = Color.FromArgb(14, 72, 119);
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = FlatStyle.Flat;
			btnClear.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			btnClear.ForeColor = Color.White;
			btnClear.Location = new Point(292, 3);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(55, 30);
			btnClear.TabIndex = 2;
			btnClear.Tag = "CE";
			btnClear.Text = "CE";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new EventHandler(btnClear_Click);
			btnBackSpace.BackColor = Color.FromArgb(14, 72, 119);
			btnBackSpace.BackgroundImageLayout = ImageLayout.Stretch;
			btnBackSpace.FlatAppearance.BorderColor = Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = FlatStyle.Flat;
			btnBackSpace.Font = new Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = Color.FromArgb(61, 77, 125);
			btnBackSpace.Image = (Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = ImeMode.NoControl;
			btnBackSpace.Location = new Point(292, 35);
			btnBackSpace.Margin = new Padding(4);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new Size(55, 30);
			btnBackSpace.TabIndex = 38;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new EventHandler(btnBackSpace_Click);
			AutoScaleDimensions = new SizeF(6f, 13f);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnBackSpace);
			Controls.Add(btn0);
			Controls.Add(btnClear);
			Controls.Add(btn5);
			Controls.Add(btn9);
			Controls.Add(btn8);
			Controls.Add(btn4);
			Controls.Add(btn7);
			Controls.Add(btn3);
			Controls.Add(btn6);
			Controls.Add(btn2);
			Controls.Add(btn1);
			DoubleBuffered = true;
			Name = "RestaurantKeyboard";
			Size = new Size(350, 68);
			ResumeLayout(false);
		}
	}
}
