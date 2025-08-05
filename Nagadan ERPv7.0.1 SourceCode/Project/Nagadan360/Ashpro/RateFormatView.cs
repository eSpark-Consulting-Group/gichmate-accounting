using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class RateFormatView : BaseForm
	{
		private DBContext db = new DBContext();

		private List<RateFormat> formats = new List<RateFormat>();

		private IContainer components = null;

		private Label lblTitle;

		private AshButton btnClear;

		private AshButton btnSave;

		private AshTextBoxNormal txtZero;

		private AshTextBoxNormal txtFive;

		private Label label10;

		private Label label5;

		private AshTextBoxNormal txtNine;

		private Label label9;

		private AshTextBoxNormal txtFour;

		private AshTextBoxNormal txtEieght;

		private Label label3;

		private Label label8;

		private AshTextBoxNormal txtThree;

		private AshTextBoxNormal txtSeven;

		private Label label2;

		private Label label7;

		private AshTextBoxNormal txtTwo;

		private AshTextBoxNormal txtSix;

		private Label label1;

		private Label label6;

		private AshTextBoxNormal txtOne;

		private Label label4;

		public RateFormatView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			txtOne.Focus();
		}

		private void RateFormatView_Load(object sender, EventArgs e)
		{
			formats = db.RateFormats();
			if (formats.Count <= 0)
			{
				return;
			}
			foreach (Control control in base.Controls)
			{
				if (control is AshTextBoxNormal)
				{
					AshTextBoxNormal ashText = (AshTextBoxNormal)control;
					ashText.Text = db.RateFormats().FirstOrDefault((RateFormat x) => x.Name == ashText.Tag.ToInt32()).Value;
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (formats.Count > 0)
			{
				foreach (RateFormat format in formats)
				{
					foreach (Control control3 in base.Controls)
					{
						if (control3 is AshTextBoxNormal)
						{
							AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)control3;
							if (ashTextBoxNormal.Tag.ToInt32() == format.Name)
							{
								format.Value = ashTextBoxNormal.Text;
								AshproCommon.UpdateToDatabaseObj(format, "RateFormat", "Name", format.Name);
							}
						}
					}
				}
				Messages.UpdateMessage();
				return;
			}
			foreach (Control control4 in base.Controls)
			{
				if (control4 is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal2 = (AshTextBoxNormal)control4;
					RateFormat rateFormat = new RateFormat();
					rateFormat.Name = ashTextBoxNormal2.Tag.ToInt32();
					rateFormat.Value = ashTextBoxNormal2.Text;
					AshproCommon.InsertToDatabaseObj(rateFormat);
				}
			}
			Messages.SavedMessage();
		}

		private void txtZero_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
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
			lblTitle = new System.Windows.Forms.Label();
			btnClear = new AshUsercontrol.AshButton();
			btnSave = new AshUsercontrol.AshButton();
			txtZero = new AshUsercontrol.AshTextBoxNormal();
			txtFive = new AshUsercontrol.AshTextBoxNormal();
			label10 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			txtNine = new AshUsercontrol.AshTextBoxNormal();
			label9 = new System.Windows.Forms.Label();
			txtFour = new AshUsercontrol.AshTextBoxNormal();
			txtEieght = new AshUsercontrol.AshTextBoxNormal();
			label3 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			txtThree = new AshUsercontrol.AshTextBoxNormal();
			txtSeven = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			txtTwo = new AshUsercontrol.AshTextBoxNormal();
			txtSix = new AshUsercontrol.AshTextBoxNormal();
			label1 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			txtOne = new AshUsercontrol.AshTextBoxNormal();
			label4 = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(419, 0);
			btnEnd.TabIndex = 23;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 2);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(123, 24);
			lblTitle.TabIndex = 22;
			lblTitle.Text = "Rate Format";
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(256, 134);
			btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 21;
			btnClear.Text = "&Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(125, 134);
			btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 20;
			btnSave.Text = "&Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			txtZero.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtZero.Data = 0;
			txtZero.EnterColor = System.Drawing.Color.LightPink;
			txtZero.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtZero.Format = null;
			txtZero.isAllowNegative = false;
			txtZero.isAllowSpecialChar = false;
			txtZero.isNumberOnly = false;
			txtZero.isNumeric = false;
			txtZero.LeaveColor = System.Drawing.Color.White;
			txtZero.Location = new System.Drawing.Point(375, 94);
			txtZero.Margin = new System.Windows.Forms.Padding(4);
			txtZero.MaxLength = 1;
			txtZero.Name = "txtZero";
			txtZero.Size = new System.Drawing.Size(35, 16);
			txtZero.TabIndex = 19;
			txtZero.Tag = "0";
			txtZero.Value = new decimal(new int[4]);
			txtZero.WaterMark = null;
			txtZero.KeyDown += new System.Windows.Forms.KeyEventHandler(txtZero_KeyDown);
			txtFive.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFive.Data = 0;
			txtFive.EnterColor = System.Drawing.Color.LightPink;
			txtFive.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtFive.Format = null;
			txtFive.isAllowNegative = false;
			txtFive.isAllowSpecialChar = false;
			txtFive.isNumberOnly = false;
			txtFive.isNumeric = false;
			txtFive.LeaveColor = System.Drawing.Color.White;
			txtFive.Location = new System.Drawing.Point(375, 61);
			txtFive.Margin = new System.Windows.Forms.Padding(4);
			txtFive.MaxLength = 1;
			txtFive.Name = "txtFive";
			txtFive.Size = new System.Drawing.Size(35, 16);
			txtFive.TabIndex = 9;
			txtFive.Tag = "5";
			txtFive.Value = new decimal(new int[4]);
			txtFive.WaterMark = null;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(351, 95);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(19, 13);
			label10.TabIndex = 18;
			label10.Text = "0 :";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(351, 62);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(19, 13);
			label5.TabIndex = 8;
			label5.Text = "5 :";
			txtNine.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNine.Data = 0;
			txtNine.EnterColor = System.Drawing.Color.LightPink;
			txtNine.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNine.Format = null;
			txtNine.isAllowNegative = false;
			txtNine.isAllowSpecialChar = false;
			txtNine.isNumberOnly = false;
			txtNine.isNumeric = false;
			txtNine.LeaveColor = System.Drawing.Color.White;
			txtNine.Location = new System.Drawing.Point(296, 94);
			txtNine.Margin = new System.Windows.Forms.Padding(4);
			txtNine.MaxLength = 1;
			txtNine.Name = "txtNine";
			txtNine.Size = new System.Drawing.Size(35, 16);
			txtNine.TabIndex = 17;
			txtNine.Tag = "9";
			txtNine.Value = new decimal(new int[4]);
			txtNine.WaterMark = null;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(272, 95);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(19, 13);
			label9.TabIndex = 16;
			label9.Text = "9 :";
			txtFour.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFour.Data = 0;
			txtFour.EnterColor = System.Drawing.Color.LightPink;
			txtFour.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtFour.Format = null;
			txtFour.isAllowNegative = false;
			txtFour.isAllowSpecialChar = false;
			txtFour.isNumberOnly = false;
			txtFour.isNumeric = false;
			txtFour.LeaveColor = System.Drawing.Color.White;
			txtFour.Location = new System.Drawing.Point(296, 61);
			txtFour.Margin = new System.Windows.Forms.Padding(4);
			txtFour.MaxLength = 1;
			txtFour.Name = "txtFour";
			txtFour.Size = new System.Drawing.Size(35, 16);
			txtFour.TabIndex = 7;
			txtFour.Tag = "4";
			txtFour.Value = new decimal(new int[4]);
			txtFour.WaterMark = null;
			txtEieght.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtEieght.Data = 0;
			txtEieght.EnterColor = System.Drawing.Color.LightPink;
			txtEieght.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtEieght.Format = null;
			txtEieght.isAllowNegative = false;
			txtEieght.isAllowSpecialChar = false;
			txtEieght.isNumberOnly = false;
			txtEieght.isNumeric = false;
			txtEieght.LeaveColor = System.Drawing.Color.White;
			txtEieght.Location = new System.Drawing.Point(221, 93);
			txtEieght.Margin = new System.Windows.Forms.Padding(4);
			txtEieght.MaxLength = 1;
			txtEieght.Name = "txtEieght";
			txtEieght.Size = new System.Drawing.Size(35, 16);
			txtEieght.TabIndex = 15;
			txtEieght.Tag = "8";
			txtEieght.Value = new decimal(new int[4]);
			txtEieght.WaterMark = null;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(272, 62);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(19, 13);
			label3.TabIndex = 6;
			label3.Text = "4 :";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(197, 94);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(19, 13);
			label8.TabIndex = 14;
			label8.Text = "8 :";
			txtThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtThree.Data = 0;
			txtThree.EnterColor = System.Drawing.Color.LightPink;
			txtThree.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtThree.Format = null;
			txtThree.isAllowNegative = false;
			txtThree.isAllowSpecialChar = false;
			txtThree.isNumberOnly = false;
			txtThree.isNumeric = false;
			txtThree.LeaveColor = System.Drawing.Color.White;
			txtThree.Location = new System.Drawing.Point(221, 60);
			txtThree.Margin = new System.Windows.Forms.Padding(4);
			txtThree.MaxLength = 1;
			txtThree.Name = "txtThree";
			txtThree.Size = new System.Drawing.Size(35, 16);
			txtThree.TabIndex = 5;
			txtThree.Tag = "3";
			txtThree.Value = new decimal(new int[4]);
			txtThree.WaterMark = null;
			txtSeven.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSeven.Data = 0;
			txtSeven.EnterColor = System.Drawing.Color.LightPink;
			txtSeven.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSeven.Format = null;
			txtSeven.isAllowNegative = false;
			txtSeven.isAllowSpecialChar = false;
			txtSeven.isNumberOnly = false;
			txtSeven.isNumeric = false;
			txtSeven.LeaveColor = System.Drawing.Color.White;
			txtSeven.Location = new System.Drawing.Point(146, 93);
			txtSeven.Margin = new System.Windows.Forms.Padding(4);
			txtSeven.MaxLength = 1;
			txtSeven.Name = "txtSeven";
			txtSeven.Size = new System.Drawing.Size(35, 16);
			txtSeven.TabIndex = 13;
			txtSeven.Tag = "7";
			txtSeven.Value = new decimal(new int[4]);
			txtSeven.WaterMark = null;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(197, 61);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(19, 13);
			label2.TabIndex = 4;
			label2.Text = "3 :";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(122, 94);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(19, 13);
			label7.TabIndex = 12;
			label7.Text = "7 :";
			txtTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtTwo.Data = 0;
			txtTwo.EnterColor = System.Drawing.Color.LightPink;
			txtTwo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtTwo.Format = null;
			txtTwo.isAllowNegative = false;
			txtTwo.isAllowSpecialChar = false;
			txtTwo.isNumberOnly = false;
			txtTwo.isNumeric = false;
			txtTwo.LeaveColor = System.Drawing.Color.White;
			txtTwo.Location = new System.Drawing.Point(146, 60);
			txtTwo.Margin = new System.Windows.Forms.Padding(4);
			txtTwo.MaxLength = 1;
			txtTwo.Name = "txtTwo";
			txtTwo.Size = new System.Drawing.Size(35, 16);
			txtTwo.TabIndex = 3;
			txtTwo.Tag = "2";
			txtTwo.Value = new decimal(new int[4]);
			txtTwo.WaterMark = null;
			txtSix.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSix.Data = 0;
			txtSix.EnterColor = System.Drawing.Color.LightPink;
			txtSix.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSix.Format = null;
			txtSix.isAllowNegative = false;
			txtSix.isAllowSpecialChar = false;
			txtSix.isNumberOnly = false;
			txtSix.isNumeric = false;
			txtSix.LeaveColor = System.Drawing.Color.White;
			txtSix.Location = new System.Drawing.Point(72, 92);
			txtSix.Margin = new System.Windows.Forms.Padding(4);
			txtSix.MaxLength = 1;
			txtSix.Name = "txtSix";
			txtSix.Size = new System.Drawing.Size(35, 16);
			txtSix.TabIndex = 11;
			txtSix.Tag = "6";
			txtSix.Value = new decimal(new int[4]);
			txtSix.WaterMark = null;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(122, 61);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(19, 13);
			label1.TabIndex = 2;
			label1.Text = "2 :";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(48, 93);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(19, 13);
			label6.TabIndex = 10;
			label6.Text = "6 :";
			txtOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtOne.Data = 0;
			txtOne.EnterColor = System.Drawing.Color.LightPink;
			txtOne.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtOne.Format = null;
			txtOne.isAllowNegative = false;
			txtOne.isAllowSpecialChar = false;
			txtOne.isNumberOnly = false;
			txtOne.isNumeric = false;
			txtOne.LeaveColor = System.Drawing.Color.White;
			txtOne.Location = new System.Drawing.Point(72, 59);
			txtOne.Margin = new System.Windows.Forms.Padding(4);
			txtOne.MaxLength = 1;
			txtOne.Name = "txtOne";
			txtOne.Size = new System.Drawing.Size(35, 16);
			txtOne.TabIndex = 1;
			txtOne.Tag = "1";
			txtOne.Value = new decimal(new int[4]);
			txtOne.WaterMark = null;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(48, 60);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(19, 13);
			label4.TabIndex = 0;
			label4.Text = "1 :";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(454, 184);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnSave);
			base.Controls.Add(txtZero);
			base.Controls.Add(txtFive);
			base.Controls.Add(label10);
			base.Controls.Add(label5);
			base.Controls.Add(txtNine);
			base.Controls.Add(label9);
			base.Controls.Add(txtFour);
			base.Controls.Add(txtEieght);
			base.Controls.Add(label3);
			base.Controls.Add(label8);
			base.Controls.Add(txtThree);
			base.Controls.Add(txtSeven);
			base.Controls.Add(label2);
			base.Controls.Add(label7);
			base.Controls.Add(txtTwo);
			base.Controls.Add(txtSix);
			base.Controls.Add(label1);
			base.Controls.Add(label6);
			base.Controls.Add(txtOne);
			base.Controls.Add(label4);
			base.Name = "RateFormatView";
			base.Load += new System.EventHandler(RateFormatView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(txtOne, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtSix, 0);
			base.Controls.SetChildIndex(txtTwo, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtSeven, 0);
			base.Controls.SetChildIndex(txtThree, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtEieght, 0);
			base.Controls.SetChildIndex(txtFour, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(txtNine, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(txtFive, 0);
			base.Controls.SetChildIndex(txtZero, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
