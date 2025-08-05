using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class DiscountChangerPOS : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private Label label2;

		private Button btnCancel;

		private Button btnOK;

		public Label lblProductName;

		public AshTextBoxNormal txtDiscount;

		public AshTextBoxNormal txtDiscAmount;

		private Label label3;

		private Label label5;

		private Label label7;

		public AshNumericLabel lblQuantity;

		public AshNumericLabel lblRate;

		public AshNumericLabel lblGross;

		public DiscountChangerPOS()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			btnEnd.Visible = false;
			lblQuantity.Format = CommonSettings.strQuantityFormat;
			lblRate.Format = CommonSettings.strCurencyFormat;
			lblGross.Format = CommonSettings.strCurencyFormat;
			txtDiscount.Format = CommonSettings.strCurencyFormat;
			txtDiscAmount.Format = CommonSettings.strCurencyFormat;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void txtDiscount_Validated(object sender, EventArgs e)
		{
			if (txtDiscount.Value > 0m)
			{
				txtDiscAmount.Value = lblGross.Value * txtDiscount.Value / 100m;
			}
			else
			{
				txtDiscAmount.Value = 0m;
			}
		}

		private void txtDiscAmount_Validated(object sender, EventArgs e)
		{
			if (txtDiscAmount.Value > 0m)
			{
				txtDiscount.Value = txtDiscAmount.Value / lblGross.Value * 100m;
			}
			else
			{
				txtDiscount.Value = 0m;
			}
		}

		private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (txtDiscount.Value > 0m)
				{
					txtDiscAmount.Value = lblGross.Value * txtDiscount.Value / 100m;
				}
				else
				{
					txtDiscAmount.Value = 0m;
				}
				btnOK_Click(null, null);
			}
		}

		private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (txtDiscAmount.Value > 0m)
				{
					txtDiscount.Value = txtDiscAmount.Value / lblGross.Value * 100m;
				}
				else
				{
					txtDiscount.Value = 0m;
				}
				btnOK_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscountChangerPOS));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDiscAmount = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblQuantity = new AshUsercontrol.AshNumericLabel();
            this.lblRate = new AshUsercontrol.AshNumericLabel();
            this.lblGross = new AshUsercontrol.AshNumericLabel();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblProductName
            // 
            resources.ApplyResources(this.lblProductName, "lblProductName");
            this.lblProductName.Name = "lblProductName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtDiscount
            // 
            resources.ApplyResources(this.txtDiscount, "txtDiscount");
            this.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscount.Data = 0;
            this.txtDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscount.Format = null;
            this.txtDiscount.isAllowNegative = false;
            this.txtDiscount.isAllowSpecialChar = false;
            this.txtDiscount.isNumberOnly = false;
            this.txtDiscount.isNumeric = true;
            this.txtDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscount_KeyDown);
            this.txtDiscount.Validated += new System.EventHandler(this.txtDiscount_Validated);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabStop = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.TabStop = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDiscAmount
            // 
            resources.ApplyResources(this.txtDiscAmount, "txtDiscAmount");
            this.txtDiscAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscAmount.Data = 0;
            this.txtDiscAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscAmount.Format = null;
            this.txtDiscAmount.isAllowNegative = false;
            this.txtDiscAmount.isAllowSpecialChar = false;
            this.txtDiscAmount.isNumberOnly = false;
            this.txtDiscAmount.isNumeric = true;
            this.txtDiscAmount.LeaveColor = System.Drawing.Color.White;
            this.txtDiscAmount.Name = "txtDiscAmount";
            this.txtDiscAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDiscAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscAmount_KeyDown);
            this.txtDiscAmount.Validated += new System.EventHandler(this.txtDiscAmount_Validated);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblQuantity
            // 
            resources.ApplyResources(this.lblQuantity, "lblQuantity");
            this.lblQuantity.Format = null;
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblRate
            // 
            resources.ApplyResources(this.lblRate, "lblRate");
            this.lblRate.Format = null;
            this.lblRate.Name = "lblRate";
            this.lblRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblGross
            // 
            resources.ApplyResources(this.lblGross, "lblGross");
            this.lblGross.Format = null;
            this.lblGross.Name = "lblGross";
            this.lblGross.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // DiscountChangerPOS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDiscAmount);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Name = "DiscountChangerPOS";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblProductName, 0);
            this.Controls.SetChildIndex(this.txtDiscount, 0);
            this.Controls.SetChildIndex(this.txtDiscAmount, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblQuantity, 0);
            this.Controls.SetChildIndex(this.lblRate, 0);
            this.Controls.SetChildIndex(this.lblGross, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
