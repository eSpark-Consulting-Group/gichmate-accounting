using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class Stock : BaseForm
	{
		private IContainer components = null;

		private GroupBox grpstock;

		private Button btnbarcode;

		private Button btnlocation;

		private Button btnprodLoca;

		private Button btnstockmanage;

		private Button btnpurchase;

		private Button btnBarcodePrint;

		private Button btnsuppl;

		private Button btntax;

		private Button btncatagory;

		private Button btnproduct;

		private Label lblHead;

		public DashboardPOS pOS { get; set; }

		public Stock()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void btnproduct_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			switch (button.Tag.ToInt32())
			{
			case 1:
				pOS.tsmProduct_Click(null, null);
				break;
			case 2:
				pOS.tsmPGroup_Click(null, null);
				break;
			case 3:
				pOS.tsmSupplier_Click(null, null);
				break;
			case 4:
				pOS.tsmPurchase_Click(null, null);
				break;
			case 5:
				pOS.tsmBranch_Click(null, null);
				break;
			case 6:
				pOS.tsmStockAdjuster_Click(null, null);
				break;
			case 7:
				pOS.tsmGodown_Click(null, null);
				break;
			case 8:
				pOS.tsmTCategory_Click(null, null);
				break;
			case 10:
				pOS.tsmBCodePrinting_Click(null, null);
				break;
			case 9:
				pOS.tsmBCodeDesign_Click(null, null);
				break;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Stock));
			grpstock = new System.Windows.Forms.GroupBox();
			btnbarcode = new System.Windows.Forms.Button();
			btnlocation = new System.Windows.Forms.Button();
			btnprodLoca = new System.Windows.Forms.Button();
			btnstockmanage = new System.Windows.Forms.Button();
			btnpurchase = new System.Windows.Forms.Button();
			btnBarcodePrint = new System.Windows.Forms.Button();
			btnsuppl = new System.Windows.Forms.Button();
			btntax = new System.Windows.Forms.Button();
			btncatagory = new System.Windows.Forms.Button();
			btnproduct = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			grpstock.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			grpstock.Controls.Add(btnbarcode);
			grpstock.Controls.Add(btnlocation);
			grpstock.Controls.Add(btnprodLoca);
			grpstock.Controls.Add(btnstockmanage);
			grpstock.Controls.Add(btnpurchase);
			grpstock.Controls.Add(btnBarcodePrint);
			grpstock.Controls.Add(btnsuppl);
			grpstock.Controls.Add(btntax);
			grpstock.Controls.Add(btncatagory);
			grpstock.Controls.Add(btnproduct);
			resources.ApplyResources(grpstock, "grpstock");
			grpstock.Name = "grpstock";
			grpstock.TabStop = false;
			btnbarcode.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnbarcode, "btnbarcode");
			btnbarcode.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnbarcode.Name = "btnbarcode";
			btnbarcode.Tag = "10";
			btnbarcode.UseVisualStyleBackColor = false;
			btnbarcode.Click += new System.EventHandler(btnproduct_Click);
			btnlocation.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnlocation, "btnlocation");
			btnlocation.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnlocation.Name = "btnlocation";
			btnlocation.Tag = "5";
			btnlocation.UseVisualStyleBackColor = false;
			btnlocation.Click += new System.EventHandler(btnproduct_Click);
			btnprodLoca.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnprodLoca, "btnprodLoca");
			btnprodLoca.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnprodLoca.Name = "btnprodLoca";
			btnprodLoca.Tag = "7";
			btnprodLoca.UseVisualStyleBackColor = false;
			btnprodLoca.Click += new System.EventHandler(btnproduct_Click);
			btnstockmanage.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnstockmanage, "btnstockmanage");
			btnstockmanage.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnstockmanage.Name = "btnstockmanage";
			btnstockmanage.Tag = "6";
			btnstockmanage.UseVisualStyleBackColor = false;
			btnstockmanage.Click += new System.EventHandler(btnproduct_Click);
			btnpurchase.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnpurchase, "btnpurchase");
			btnpurchase.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnpurchase.Name = "btnpurchase";
			btnpurchase.Tag = "4";
			btnpurchase.UseVisualStyleBackColor = false;
			btnpurchase.Click += new System.EventHandler(btnproduct_Click);
			btnBarcodePrint.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnBarcodePrint, "btnBarcodePrint");
			btnBarcodePrint.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnBarcodePrint.Name = "btnBarcodePrint";
			btnBarcodePrint.Tag = "9";
			btnBarcodePrint.UseVisualStyleBackColor = false;
			btnBarcodePrint.Click += new System.EventHandler(btnproduct_Click);
			btnsuppl.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnsuppl, "btnsuppl");
			btnsuppl.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsuppl.Name = "btnsuppl";
			btnsuppl.Tag = "3";
			btnsuppl.UseVisualStyleBackColor = false;
			btnsuppl.Click += new System.EventHandler(btnproduct_Click);
			btntax.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btntax, "btntax");
			btntax.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btntax.Name = "btntax";
			btntax.Tag = "8";
			btntax.UseVisualStyleBackColor = false;
			btntax.Click += new System.EventHandler(btnproduct_Click);
			btncatagory.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btncatagory, "btncatagory");
			btncatagory.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btncatagory.Name = "btncatagory";
			btncatagory.Tag = "2";
			btncatagory.UseVisualStyleBackColor = false;
			btncatagory.Click += new System.EventHandler(btnproduct_Click);
			btnproduct.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnproduct, "btnproduct");
			btnproduct.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnproduct.Name = "btnproduct";
			btnproduct.Tag = "1";
			btnproduct.UseVisualStyleBackColor = false;
			btnproduct.Click += new System.EventHandler(btnproduct_Click);
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(lblHead);
			base.Controls.Add(grpstock);
			base.Name = "Stock";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(grpstock, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			grpstock.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
