using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
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
			if (this.atNumberClick != null)
			{
				this.atNumberClick(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshUsercontrol.RestaurantKeyboard));
			btn5 = new AshUsercontrol.ExButton();
			btn4 = new AshUsercontrol.ExButton();
			btn3 = new AshUsercontrol.ExButton();
			btn2 = new AshUsercontrol.ExButton();
			btn1 = new AshUsercontrol.ExButton();
			btn6 = new AshUsercontrol.ExButton();
			btn7 = new AshUsercontrol.ExButton();
			btn8 = new AshUsercontrol.ExButton();
			btn9 = new AshUsercontrol.ExButton();
			btn0 = new AshUsercontrol.ExButton();
			btnClear = new AshUsercontrol.ExButton();
			btnBackSpace = new AshUsercontrol.ExButton();
			SuspendLayout();
			btn5.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn5.ForeColor = System.Drawing.Color.White;
			btn5.Location = new System.Drawing.Point(234, 3);
			btn5.Name = "btn5";
			btn5.Size = new System.Drawing.Size(55, 30);
			btn5.TabIndex = 2;
			btn5.Tag = "5";
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new System.EventHandler(btnone_Click);
			btn4.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn4.ForeColor = System.Drawing.Color.White;
			btn4.Location = new System.Drawing.Point(176, 3);
			btn4.Name = "btn4";
			btn4.Size = new System.Drawing.Size(55, 30);
			btn4.TabIndex = 3;
			btn4.Tag = "4";
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new System.EventHandler(btnone_Click);
			btn3.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn3.ForeColor = System.Drawing.Color.White;
			btn3.Location = new System.Drawing.Point(118, 3);
			btn3.Name = "btn3";
			btn3.Size = new System.Drawing.Size(55, 30);
			btn3.TabIndex = 4;
			btn3.Tag = "3";
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new System.EventHandler(btnone_Click);
			btn2.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn2.ForeColor = System.Drawing.Color.White;
			btn2.Location = new System.Drawing.Point(60, 3);
			btn2.Name = "btn2";
			btn2.Size = new System.Drawing.Size(55, 30);
			btn2.TabIndex = 5;
			btn2.Tag = "2";
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new System.EventHandler(btnone_Click);
			btn1.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn1.ForeColor = System.Drawing.Color.White;
			btn1.Location = new System.Drawing.Point(2, 3);
			btn1.Name = "btn1";
			btn1.Size = new System.Drawing.Size(55, 30);
			btn1.TabIndex = 6;
			btn1.Tag = "1";
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new System.EventHandler(btnone_Click);
			btn6.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn6.ForeColor = System.Drawing.Color.White;
			btn6.Location = new System.Drawing.Point(2, 35);
			btn6.Name = "btn6";
			btn6.Size = new System.Drawing.Size(55, 30);
			btn6.TabIndex = 6;
			btn6.Tag = "6";
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new System.EventHandler(btnone_Click);
			btn7.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn7.ForeColor = System.Drawing.Color.White;
			btn7.Location = new System.Drawing.Point(60, 35);
			btn7.Name = "btn7";
			btn7.Size = new System.Drawing.Size(55, 30);
			btn7.TabIndex = 5;
			btn7.Tag = "7";
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new System.EventHandler(btnone_Click);
			btn8.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn8.ForeColor = System.Drawing.Color.White;
			btn8.Location = new System.Drawing.Point(118, 35);
			btn8.Name = "btn8";
			btn8.Size = new System.Drawing.Size(55, 30);
			btn8.TabIndex = 4;
			btn8.Tag = "8";
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new System.EventHandler(btnone_Click);
			btn9.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn9.ForeColor = System.Drawing.Color.White;
			btn9.Location = new System.Drawing.Point(176, 35);
			btn9.Name = "btn9";
			btn9.Size = new System.Drawing.Size(55, 30);
			btn9.TabIndex = 3;
			btn9.Tag = "9";
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new System.EventHandler(btnone_Click);
			btn0.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn0.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn0.ForeColor = System.Drawing.Color.White;
			btn0.Location = new System.Drawing.Point(234, 35);
			btn0.Name = "btn0";
			btn0.Size = new System.Drawing.Size(55, 30);
			btn0.TabIndex = 2;
			btn0.Tag = "0";
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new System.EventHandler(btnone_Click);
			btnClear.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(292, 3);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(55, 30);
			btnClear.TabIndex = 2;
			btnClear.Tag = "CE";
			btnClear.Text = "CE";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnBackSpace.BackColor = System.Drawing.Color.FromArgb(14, 72, 119);
			btnBackSpace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			btnBackSpace.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnBackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnBackSpace.Image = (System.Drawing.Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnBackSpace.Location = new System.Drawing.Point(292, 35);
			btnBackSpace.Margin = new System.Windows.Forms.Padding(4);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new System.Drawing.Size(55, 30);
			btnBackSpace.TabIndex = 38;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new System.EventHandler(btnBackSpace_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnBackSpace);
			base.Controls.Add(btn0);
			base.Controls.Add(btnClear);
			base.Controls.Add(btn5);
			base.Controls.Add(btn9);
			base.Controls.Add(btn8);
			base.Controls.Add(btn4);
			base.Controls.Add(btn7);
			base.Controls.Add(btn3);
			base.Controls.Add(btn6);
			base.Controls.Add(btn2);
			base.Controls.Add(btn1);
			DoubleBuffered = true;
			base.Name = "RestaurantKeyboard";
			base.Size = new System.Drawing.Size(350, 68);
			ResumeLayout(false);
		}
	}
}
