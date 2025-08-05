using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class WeighScaleView : BaseForm
	{
		public string hex;

		private string sValue;

		private SerialPort _serialPort;

		private const int BaudRate = 9600;

		private System.Windows.Forms.Timer timer;

		private IContainer components = null;

		public Label lblHead;

		public AshTextBoxNormal txtInput;

		public Label lblInput;

		public Label label1;

		public AshTextBoxNormal txtRate;

		private Button btnCancel;

		private Button btnOK;

		private NumKeyBoardPOS nkPOS;

		public CheckBox cbInclusive;

		public decimal Rate { get; set; }

		public WeighScaleView(bool isRateIncluded = false)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			txtRate.Format = CommonSettings.strCurencyFormat;
			if (Settings.Default.ScalePort != string.Empty)
			{
				_serialPort = new SerialPort(Settings.Default.ScalePort, 9600, Parity.None, 8, StopBits.One);
				timer = new System.Windows.Forms.Timer();
				timer.Tick += Timer_Tick;
			}
			if (isRateIncluded)
			{
				cbInclusive.Visible = true;
				txtRate.ReadOnly = false;
			}
			txtInput.Font = new Font("Keania One", 48f, FontStyle.Regular);
			txtRate.Font = new Font("Keania One", 48f, FontStyle.Regular);
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			try
			{
				hex = string.Empty;
				hex = $"{_serialPort.ReadExisting():X2} ";
				if (hex != "")
				{
					string[] array = hex.Split(new string[1] { "+" }, StringSplitOptions.None);
					hex = array[1].Trim().Replace("kg", "");
					hex = hex.ToDecimal().ToString();
					if (sValue != hex)
					{
						sValue = hex;
						txtInput.Text = sValue;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (Settings.Default.ScalePort != string.Empty)
			{
				timer.Stop();
			}
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (Settings.Default.ScalePort != string.Empty)
			{
				timer.Stop();
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnOK_Click(null, null);
			}
		}

		private void WeighScaleView_Load(object sender, EventArgs e)
		{
			if (!(Settings.Default.ScalePort != string.Empty))
			{
				return;
			}
			try
			{
				if (!_serialPort.IsOpen)
				{
					_serialPort.Open();
				}
				timer.Interval = 1000;
				timer.Start();
			}
			catch (Exception)
			{
			}
		}

		private void WeighScaleView_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Settings.Default.ScalePort != string.Empty)
			{
				timer.Stop();
				if (_serialPort.IsOpen)
				{
					_serialPort.Close();
					_serialPort.Dispose();
				}
			}
		}

		private void txtInput_TextChanged(object sender, EventArgs e)
		{
			if (txtInput.Text != string.Empty)
			{
				txtRate.Value = txtInput.Text.ToDecimal() * Rate;
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
			lblHead = new System.Windows.Forms.Label();
			txtInput = new AshUsercontrol.AshTextBoxNormal();
			lblInput = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtRate = new AshUsercontrol.AshTextBoxNormal();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			nkPOS = new AshUsercontrol.NumKeyBoardPOS();
			cbInclusive = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(761, 0);
			btnEnd.TabIndex = 7;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			lblHead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblHead.Location = new System.Drawing.Point(-1, 1);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(137, 24);
			lblHead.TabIndex = 8;
			lblHead.Text = "Measurement";
			txtInput.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
			txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtInput.Data = 0;
			txtInput.EnterColor = System.Drawing.Color.LightPink;
			txtInput.Font = new System.Drawing.Font("Ticking Timebomb BB", 48f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			txtInput.ForeColor = System.Drawing.Color.Red;
			txtInput.Format = null;
			txtInput.isAllowNegative = false;
			txtInput.isAllowSpecialChar = false;
			txtInput.isNumberOnly = false;
			txtInput.isNumeric = true;
			txtInput.LeaveColor = System.Drawing.Color.White;
			txtInput.Location = new System.Drawing.Point(191, 48);
			txtInput.Margin = new System.Windows.Forms.Padding(4);
			txtInput.Name = "txtInput";
			txtInput.Size = new System.Drawing.Size(297, 64);
			txtInput.TabIndex = 1;
			txtInput.Text = "0.00";
			txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtInput.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtInput.WaterMark = null;
			txtInput.TextChanged += new System.EventHandler(txtInput_TextChanged);
			txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(txtInput_KeyDown);
			lblInput.AutoSize = true;
			lblInput.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblInput.Location = new System.Drawing.Point(5, 66);
			lblInput.Name = "lblInput";
			lblInput.Size = new System.Drawing.Size(172, 24);
			lblInput.TabIndex = 0;
			lblInput.Text = "Measurement :";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(102, 133);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(75, 24);
			label1.TabIndex = 2;
			label1.Text = "Rate :";
			txtRate.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
			txtRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRate.Data = 0;
			txtRate.EnterColor = System.Drawing.Color.LightPink;
			txtRate.Font = new System.Drawing.Font("Ticking Timebomb BB", 48f, System.Drawing.FontStyle.Bold);
			txtRate.ForeColor = System.Drawing.Color.Red;
			txtRate.Format = null;
			txtRate.isAllowNegative = false;
			txtRate.isAllowSpecialChar = false;
			txtRate.isNumberOnly = false;
			txtRate.isNumeric = true;
			txtRate.LeaveColor = System.Drawing.Color.White;
			txtRate.Location = new System.Drawing.Point(191, 114);
			txtRate.Margin = new System.Windows.Forms.Padding(4);
			txtRate.Name = "txtRate";
			txtRate.ReadOnly = true;
			txtRate.Size = new System.Drawing.Size(297, 64);
			txtRate.TabIndex = 3;
			txtRate.TabStop = false;
			txtRate.Text = "0.00";
			txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtRate.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtRate.WaterMark = null;
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnCancel.Location = new System.Drawing.Point(457, 298);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(70, 35);
			btnCancel.TabIndex = 5;
			btnCancel.TabStop = false;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnOK.Location = new System.Drawing.Point(382, 298);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(70, 35);
			btnOK.TabIndex = 4;
			btnOK.TabStop = false;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			nkPOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
			nkPOS.Location = new System.Drawing.Point(540, 42);
			nkPOS.Margin = new System.Windows.Forms.Padding(4);
			nkPOS.Name = "nkPOS";
			nkPOS.Size = new System.Drawing.Size(232, 248);
			nkPOS.TabIndex = 6;
			nkPOS.TabStop = false;
			cbInclusive.AutoSize = true;
			cbInclusive.Checked = true;
			cbInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
			cbInclusive.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbInclusive.Location = new System.Drawing.Point(191, 198);
			cbInclusive.Name = "cbInclusive";
			cbInclusive.Size = new System.Drawing.Size(115, 28);
			cbInclusive.TabIndex = 9;
			cbInclusive.Text = "Inclusive";
			cbInclusive.UseVisualStyleBackColor = true;
			cbInclusive.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(795, 345);
			base.Controls.Add(cbInclusive);
			base.Controls.Add(nkPOS);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnOK);
			base.Controls.Add(txtRate);
			base.Controls.Add(label1);
			base.Controls.Add(txtInput);
			base.Controls.Add(lblInput);
			base.Controls.Add(lblHead);
			base.Name = "WeighScaleView";
			Text = "WeighScaleView";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(WeighScaleView_FormClosing);
			base.Load += new System.EventHandler(WeighScaleView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(lblInput, 0);
			base.Controls.SetChildIndex(txtInput, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtRate, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(nkPOS, 0);
			base.Controls.SetChildIndex(cbInclusive, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
