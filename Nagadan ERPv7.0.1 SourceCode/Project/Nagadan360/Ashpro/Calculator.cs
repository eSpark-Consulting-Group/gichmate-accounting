using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class Calculator : BaseForm
	{
		private decimal dmlRslt;

		private decimal dmlFrstAmnt;

		private decimal dmlScndAmnt;

		private bool isFrstAmntEntrd = false;

		private bool isOperator = false;

		private bool isResultFound = false;

		private string strValueToAdd;

		private string strValue;

		private string strSign;

		private IContainer components = null;

		private Label lblResult;

		private Button btnClear;

		private Button btnDel;

		private Button btnSubstraction;

		private Button btn7;

		private Button btn8;

		private Button btn9;

		private Button btnMultiple;

		private Button btn4;

		private Button btn5;

		private Button btn6;

		private Button btnMinus;

		private Button btn1;

		private Button btn2;

		private Button btn3;

		private Button btnPlus;

		private Button btnNegative;

		private Button btn0;

		private Button btnPoint;

		private Button btnEqual;

		private Label lblReport;

		private Label lblHead;

		private ExButton btnBackSpace;

		public Calculator()
		{
			InitializeComponent();
		}

		private void KeyPressMethod()
		{
			if (isResultFound)
			{
				lblReport.Text = string.Empty;
				isResultFound = false;
			}
			if (!(strValueToAdd == "0") && !(strValueToAdd == "1") && !(strValueToAdd == "2") && !(strValueToAdd == "3") && !(strValueToAdd == "4") && !(strValueToAdd == "5") && !(strValueToAdd == "6") && !(strValueToAdd == "7") && !(strValueToAdd == "8") && !(strValueToAdd == "9"))
			{
				if (strValueToAdd == "/")
				{
					strSign = strValueToAdd;
					isOperator = true;
					if (!isFrstAmntEntrd)
					{
						dmlFrstAmnt = Convert.ToDecimal(lblResult.Text);
						isFrstAmntEntrd = true;
					}
					else
					{
						dmlFrstAmnt /= Convert.ToDecimal(lblResult.Text);
					}
					lblReport.Text = lblReport.Text + " " + dmlFrstAmnt + " " + strValueToAdd;
				}
				else if (strValueToAdd == "+")
				{
					strSign = strValueToAdd;
					isOperator = true;
					if (!isFrstAmntEntrd)
					{
						dmlFrstAmnt = Convert.ToDecimal(lblResult.Text);
						isFrstAmntEntrd = true;
					}
					else
					{
						dmlFrstAmnt += Convert.ToDecimal(lblResult.Text);
					}
					lblReport.Text = lblReport.Text + " " + lblResult.Text + " " + strValueToAdd;
				}
				else if (strValueToAdd == "-")
				{
					strSign = strValueToAdd;
					isOperator = true;
					if (!isFrstAmntEntrd)
					{
						dmlFrstAmnt = Convert.ToDecimal(lblResult.Text);
						isFrstAmntEntrd = true;
					}
					else
					{
						dmlFrstAmnt -= Convert.ToDecimal(lblResult.Text);
					}
					lblReport.Text = lblReport.Text + " " + lblResult.Text + " " + strValueToAdd;
				}
				else if (strValueToAdd == "*")
				{
					strSign = strValueToAdd;
					isOperator = true;
					if (!isFrstAmntEntrd)
					{
						dmlFrstAmnt = Convert.ToDecimal(lblResult.Text);
						isFrstAmntEntrd = true;
					}
					else
					{
						dmlFrstAmnt *= Convert.ToDecimal(lblResult.Text);
					}
					lblReport.Text = lblReport.Text + " " + lblResult.Text + " " + strValueToAdd;
				}
				else if (strValueToAdd == "=")
				{
					ResultGenerationMethod();
					isOperator = true;
					isResultFound = true;
				}
				else if (strValueToAdd == ".")
				{
					if (!lblResult.Text.Contains("."))
					{
						ValAddMethod();
					}
					else
					{
						lblResult.Text = lblResult.Text;
					}
					isOperator = false;
				}
			}
			else
			{
				ValAddMethod();
				isOperator = false;
			}
		}

		private void ResultGenerationMethod()
		{
			lblReport.Text = lblReport.Text + " " + lblResult.Text;
			dmlScndAmnt = Convert.ToDecimal(lblResult.Text);
			if (strSign == "+")
			{
				dmlRslt = dmlFrstAmnt + dmlScndAmnt;
			}
			else if (strSign == "-")
			{
				dmlRslt = dmlFrstAmnt - dmlScndAmnt;
			}
			else if (strSign == "*")
			{
				dmlRslt = dmlFrstAmnt * dmlScndAmnt;
			}
			else if (strSign == "/")
			{
				dmlRslt = dmlFrstAmnt / dmlScndAmnt;
			}
			lblResult.Text = dmlRslt.ToString();
			dmlRslt = default(decimal);
			dmlFrstAmnt = default(decimal);
			dmlScndAmnt = default(decimal);
			isFrstAmntEntrd = false;
		}

		private void ValAddMethod()
		{
			strValue = lblResult.Text;
			if (!(strValue == "0") && !isOperator)
			{
				lblResult.Text = strValue + strValueToAdd;
			}
			else
			{
				lblResult.Text = strValueToAdd;
			}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			lblReport.Text = "";
			btnClear_Click(null, null);
		}

		private void btnBackSpace_Click(object sender, EventArgs e)
		{
			lblResult.Text = lblResult.Text.Remove(lblResult.Text.Length - 1, 1);
			if (lblResult.Text == string.Empty)
			{
				lblResult.Text = "0";
			}
		}

		private void btnNegative_Click(object sender, EventArgs e)
		{
			lblResult.Text = (lblResult.Text.ToDecimal() * -1m).ToString();
		}

		private void button_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			strValueToAdd = button.Text;
			KeyPressMethod();
		}

		private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
		{
			strValueToAdd = e.KeyChar.ToString();
			KeyPressMethod();
		}

		private void Calculator_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			lblResult.Text = "0";
			dmlRslt = default(decimal);
			dmlFrstAmnt = default(decimal);
			dmlScndAmnt = default(decimal);
			isFrstAmntEntrd = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Calculator));
			lblResult = new System.Windows.Forms.Label();
			btnClear = new System.Windows.Forms.Button();
			btnDel = new System.Windows.Forms.Button();
			btnSubstraction = new System.Windows.Forms.Button();
			btn7 = new System.Windows.Forms.Button();
			btn8 = new System.Windows.Forms.Button();
			btn9 = new System.Windows.Forms.Button();
			btnMultiple = new System.Windows.Forms.Button();
			btn4 = new System.Windows.Forms.Button();
			btn5 = new System.Windows.Forms.Button();
			btn6 = new System.Windows.Forms.Button();
			btnMinus = new System.Windows.Forms.Button();
			btn1 = new System.Windows.Forms.Button();
			btn2 = new System.Windows.Forms.Button();
			btn3 = new System.Windows.Forms.Button();
			btnPlus = new System.Windows.Forms.Button();
			btnNegative = new System.Windows.Forms.Button();
			btn0 = new System.Windows.Forms.Button();
			btnPoint = new System.Windows.Forms.Button();
			btnEqual = new System.Windows.Forms.Button();
			lblReport = new System.Windows.Forms.Label();
			lblHead = new System.Windows.Forms.Label();
			btnBackSpace = new AshUsercontrol.ExButton();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(294, 0);
			lblResult.BackColor = System.Drawing.Color.White;
			lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblResult.ForeColor = System.Drawing.Color.Black;
			lblResult.Location = new System.Drawing.Point(4, 69);
			lblResult.Name = "lblResult";
			lblResult.Size = new System.Drawing.Size(318, 20);
			lblResult.TabIndex = 0;
			lblResult.Text = "0";
			lblResult.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			btnClear.BackColor = System.Drawing.Color.Gainsboro;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnClear.Location = new System.Drawing.Point(10, 106);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 60);
			btnClear.TabIndex = 1;
			btnClear.Text = "CE";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDel.BackColor = System.Drawing.Color.Gainsboro;
			btnDel.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnDel.FlatAppearance.BorderSize = 0;
			btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDel.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnDel.Location = new System.Drawing.Point(166, 106);
			btnDel.Name = "btnDel";
			btnDel.Size = new System.Drawing.Size(75, 60);
			btnDel.TabIndex = 1;
			btnDel.Text = "DEL";
			btnDel.UseVisualStyleBackColor = false;
			btnDel.Click += new System.EventHandler(btnDel_Click);
			btnSubstraction.BackColor = System.Drawing.Color.Gainsboro;
			btnSubstraction.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnSubstraction.FlatAppearance.BorderSize = 0;
			btnSubstraction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSubstraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSubstraction.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnSubstraction.Location = new System.Drawing.Point(245, 106);
			btnSubstraction.Name = "btnSubstraction";
			btnSubstraction.Size = new System.Drawing.Size(75, 60);
			btnSubstraction.TabIndex = 1;
			btnSubstraction.Text = "/";
			btnSubstraction.UseVisualStyleBackColor = false;
			btnSubstraction.Click += new System.EventHandler(button_Click);
			btn7.BackColor = System.Drawing.Color.Gainsboro;
			btn7.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn7.FlatAppearance.BorderSize = 0;
			btn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn7.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn7.Location = new System.Drawing.Point(10, 169);
			btn7.Name = "btn7";
			btn7.Size = new System.Drawing.Size(75, 60);
			btn7.TabIndex = 1;
			btn7.Text = "7";
			btn7.UseVisualStyleBackColor = false;
			btn7.Click += new System.EventHandler(button_Click);
			btn8.BackColor = System.Drawing.Color.Gainsboro;
			btn8.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn8.FlatAppearance.BorderSize = 0;
			btn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn8.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn8.Location = new System.Drawing.Point(88, 169);
			btn8.Name = "btn8";
			btn8.Size = new System.Drawing.Size(75, 60);
			btn8.TabIndex = 1;
			btn8.Text = "8";
			btn8.UseVisualStyleBackColor = false;
			btn8.Click += new System.EventHandler(button_Click);
			btn9.BackColor = System.Drawing.Color.Gainsboro;
			btn9.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn9.FlatAppearance.BorderSize = 0;
			btn9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn9.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn9.Location = new System.Drawing.Point(166, 169);
			btn9.Name = "btn9";
			btn9.Size = new System.Drawing.Size(75, 60);
			btn9.TabIndex = 1;
			btn9.Text = "9";
			btn9.UseVisualStyleBackColor = false;
			btn9.Click += new System.EventHandler(button_Click);
			btnMultiple.BackColor = System.Drawing.Color.Gainsboro;
			btnMultiple.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnMultiple.FlatAppearance.BorderSize = 0;
			btnMultiple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMultiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnMultiple.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnMultiple.Location = new System.Drawing.Point(245, 169);
			btnMultiple.Name = "btnMultiple";
			btnMultiple.Size = new System.Drawing.Size(75, 60);
			btnMultiple.TabIndex = 1;
			btnMultiple.Text = "*";
			btnMultiple.UseVisualStyleBackColor = false;
			btnMultiple.Click += new System.EventHandler(button_Click);
			btn4.BackColor = System.Drawing.Color.Gainsboro;
			btn4.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn4.FlatAppearance.BorderSize = 0;
			btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn4.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn4.Location = new System.Drawing.Point(10, 233);
			btn4.Name = "btn4";
			btn4.Size = new System.Drawing.Size(75, 60);
			btn4.TabIndex = 1;
			btn4.Text = "4";
			btn4.UseVisualStyleBackColor = false;
			btn4.Click += new System.EventHandler(button_Click);
			btn5.BackColor = System.Drawing.Color.Gainsboro;
			btn5.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn5.FlatAppearance.BorderSize = 0;
			btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn5.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn5.Location = new System.Drawing.Point(88, 233);
			btn5.Name = "btn5";
			btn5.Size = new System.Drawing.Size(75, 60);
			btn5.TabIndex = 1;
			btn5.Text = "5";
			btn5.UseVisualStyleBackColor = false;
			btn5.Click += new System.EventHandler(button_Click);
			btn6.BackColor = System.Drawing.Color.Gainsboro;
			btn6.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn6.FlatAppearance.BorderSize = 0;
			btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn6.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn6.Location = new System.Drawing.Point(166, 233);
			btn6.Name = "btn6";
			btn6.Size = new System.Drawing.Size(75, 60);
			btn6.TabIndex = 1;
			btn6.Text = "6";
			btn6.UseVisualStyleBackColor = false;
			btn6.Click += new System.EventHandler(button_Click);
			btnMinus.BackColor = System.Drawing.Color.Gainsboro;
			btnMinus.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnMinus.FlatAppearance.BorderSize = 0;
			btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnMinus.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnMinus.Location = new System.Drawing.Point(245, 233);
			btnMinus.Name = "btnMinus";
			btnMinus.Size = new System.Drawing.Size(75, 60);
			btnMinus.TabIndex = 1;
			btnMinus.Text = "-";
			btnMinus.UseVisualStyleBackColor = false;
			btnMinus.Click += new System.EventHandler(button_Click);
			btn1.BackColor = System.Drawing.Color.Gainsboro;
			btn1.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn1.FlatAppearance.BorderSize = 0;
			btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn1.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn1.Location = new System.Drawing.Point(10, 297);
			btn1.Name = "btn1";
			btn1.Size = new System.Drawing.Size(75, 60);
			btn1.TabIndex = 1;
			btn1.Text = "1";
			btn1.UseVisualStyleBackColor = false;
			btn1.Click += new System.EventHandler(button_Click);
			btn2.BackColor = System.Drawing.Color.Gainsboro;
			btn2.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn2.FlatAppearance.BorderSize = 0;
			btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn2.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn2.Location = new System.Drawing.Point(88, 297);
			btn2.Name = "btn2";
			btn2.Size = new System.Drawing.Size(75, 60);
			btn2.TabIndex = 1;
			btn2.Text = "2";
			btn2.UseVisualStyleBackColor = false;
			btn2.Click += new System.EventHandler(button_Click);
			btn3.BackColor = System.Drawing.Color.Gainsboro;
			btn3.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn3.FlatAppearance.BorderSize = 0;
			btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn3.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn3.Location = new System.Drawing.Point(166, 297);
			btn3.Name = "btn3";
			btn3.Size = new System.Drawing.Size(75, 60);
			btn3.TabIndex = 1;
			btn3.Text = "3";
			btn3.UseVisualStyleBackColor = false;
			btn3.Click += new System.EventHandler(button_Click);
			btnPlus.BackColor = System.Drawing.Color.Gainsboro;
			btnPlus.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnPlus.FlatAppearance.BorderSize = 0;
			btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPlus.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnPlus.Location = new System.Drawing.Point(245, 297);
			btnPlus.Name = "btnPlus";
			btnPlus.Size = new System.Drawing.Size(75, 60);
			btnPlus.TabIndex = 1;
			btnPlus.Text = "+";
			btnPlus.UseVisualStyleBackColor = false;
			btnPlus.Click += new System.EventHandler(button_Click);
			btnNegative.BackColor = System.Drawing.Color.Gainsboro;
			btnNegative.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnNegative.FlatAppearance.BorderSize = 0;
			btnNegative.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnNegative.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnNegative.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnNegative.Location = new System.Drawing.Point(10, 361);
			btnNegative.Name = "btnNegative";
			btnNegative.Size = new System.Drawing.Size(75, 60);
			btnNegative.TabIndex = 1;
			btnNegative.Text = "+/-";
			btnNegative.UseVisualStyleBackColor = false;
			btnNegative.Click += new System.EventHandler(btnNegative_Click);
			btn0.BackColor = System.Drawing.Color.Gainsboro;
			btn0.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btn0.FlatAppearance.BorderSize = 0;
			btn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btn0.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btn0.Location = new System.Drawing.Point(88, 361);
			btn0.Name = "btn0";
			btn0.Size = new System.Drawing.Size(75, 60);
			btn0.TabIndex = 1;
			btn0.Text = "0";
			btn0.UseVisualStyleBackColor = false;
			btn0.Click += new System.EventHandler(button_Click);
			btnPoint.BackColor = System.Drawing.Color.Gainsboro;
			btnPoint.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnPoint.FlatAppearance.BorderSize = 0;
			btnPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPoint.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnPoint.Location = new System.Drawing.Point(166, 361);
			btnPoint.Name = "btnPoint";
			btnPoint.Size = new System.Drawing.Size(75, 60);
			btnPoint.TabIndex = 1;
			btnPoint.Text = ".";
			btnPoint.UseVisualStyleBackColor = false;
			btnPoint.Click += new System.EventHandler(button_Click);
			btnEqual.BackColor = System.Drawing.Color.Gainsboro;
			btnEqual.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnEqual.FlatAppearance.BorderSize = 0;
			btnEqual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEqual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEqual.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnEqual.Location = new System.Drawing.Point(245, 361);
			btnEqual.Name = "btnEqual";
			btnEqual.Size = new System.Drawing.Size(75, 60);
			btnEqual.TabIndex = 1;
			btnEqual.Text = "=";
			btnEqual.UseVisualStyleBackColor = false;
			btnEqual.Click += new System.EventHandler(button_Click);
			lblReport.BackColor = System.Drawing.Color.White;
			lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblReport.ForeColor = System.Drawing.Color.Black;
			lblReport.Location = new System.Drawing.Point(4, 36);
			lblReport.Name = "lblReport";
			lblReport.Size = new System.Drawing.Size(318, 20);
			lblReport.TabIndex = 0;
			lblReport.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 3);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(103, 24);
			lblHead.TabIndex = 94;
			lblHead.Text = "Calculator";
			btnBackSpace.BackColor = System.Drawing.Color.Gainsboro;
			btnBackSpace.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnBackSpace.FlatAppearance.BorderSize = 0;
			btnBackSpace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnBackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f);
			btnBackSpace.ForeColor = System.Drawing.Color.FromArgb(61, 77, 125);
			btnBackSpace.Image = (System.Drawing.Image)resources.GetObject("btnBackSpace.Image");
			btnBackSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnBackSpace.Location = new System.Drawing.Point(88, 106);
			btnBackSpace.Name = "btnBackSpace";
			btnBackSpace.Size = new System.Drawing.Size(75, 60);
			btnBackSpace.TabIndex = 95;
			btnBackSpace.Tag = "C";
			btnBackSpace.UseVisualStyleBackColor = false;
			btnBackSpace.Click += new System.EventHandler(btnBackSpace_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(330, 428);
			base.Controls.Add(btnBackSpace);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnEqual);
			base.Controls.Add(btnPoint);
			base.Controls.Add(btnPlus);
			base.Controls.Add(btn3);
			base.Controls.Add(btnMinus);
			base.Controls.Add(btn6);
			base.Controls.Add(btn0);
			base.Controls.Add(btnMultiple);
			base.Controls.Add(btn2);
			base.Controls.Add(btn9);
			base.Controls.Add(btn5);
			base.Controls.Add(btnNegative);
			base.Controls.Add(btnSubstraction);
			base.Controls.Add(btn1);
			base.Controls.Add(btn8);
			base.Controls.Add(btn4);
			base.Controls.Add(btnDel);
			base.Controls.Add(btn7);
			base.Controls.Add(btnClear);
			base.Controls.Add(lblReport);
			base.Controls.Add(lblResult);
			ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = "Calculator";
			Text = "";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(Calculator_KeyDown);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Calculator_KeyPress);
			base.Controls.SetChildIndex(lblResult, 0);
			base.Controls.SetChildIndex(lblReport, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btn7, 0);
			base.Controls.SetChildIndex(btnDel, 0);
			base.Controls.SetChildIndex(btn4, 0);
			base.Controls.SetChildIndex(btn8, 0);
			base.Controls.SetChildIndex(btn1, 0);
			base.Controls.SetChildIndex(btnSubstraction, 0);
			base.Controls.SetChildIndex(btnNegative, 0);
			base.Controls.SetChildIndex(btn5, 0);
			base.Controls.SetChildIndex(btn9, 0);
			base.Controls.SetChildIndex(btn2, 0);
			base.Controls.SetChildIndex(btnMultiple, 0);
			base.Controls.SetChildIndex(btn0, 0);
			base.Controls.SetChildIndex(btn6, 0);
			base.Controls.SetChildIndex(btnMinus, 0);
			base.Controls.SetChildIndex(btn3, 0);
			base.Controls.SetChildIndex(btnPlus, 0);
			base.Controls.SetChildIndex(btnPoint, 0);
			base.Controls.SetChildIndex(btnEqual, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(btnBackSpace, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
