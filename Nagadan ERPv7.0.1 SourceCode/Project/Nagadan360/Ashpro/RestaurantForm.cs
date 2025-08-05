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
	public class RestaurantForm : BaseForm
	{
		private IContainer components = null;

		private Label lblHead;

		private GroupBox grpstock;

		private Button btnFavoriteProduct;

		private Button btnPrinterAllocation;

		private Button btnKOTPrinter;

		private Button btncatagory;

		private Button btnproduct;

		private Button button3;

		private Button button1;

		private Button button2;

		public DashboardPOS pOS { get; set; }

		public RestaurantForm()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void btnbutton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			switch (button.Tag.ToInt32())
			{
			case 1:
			{
				POSItemCreation frm = new POSItemCreation();
				pOS.OpenForm(frm);
				return;
			}
			case 2:
				pOS.tsmPGroup_Click(null, null);
				return;
			case 3:
				pOS.tsmKOTPrinter_Click(null, null);
				return;
			case 4:
				pOS.tsmProductPrinter_Click(null, null);
				return;
			case 5:
				pOS.tsmFavorite_Click(null, null);
				return;
			case 6:
			{
				POSFormSearch pOSFormSearch = new POSFormSearch();
				pOSFormSearch.searchType = "KOT";
				pOSFormSearch.isShowDeleteAll = true;
				pOS.OpenForm(pOSFormSearch);
				return;
			}
			}
			Utility.iReportFormNumber = button.Tag.ToInt32();
			POSReportView frm2 = new POSReportView();
			if (pOS != null)
			{
				pOS.OpenForm(frm2);
			}
			else
			{
				pOS.OpenForm(frm2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.RestaurantForm));
			lblHead = new System.Windows.Forms.Label();
			grpstock = new System.Windows.Forms.GroupBox();
			button3 = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			btnFavoriteProduct = new System.Windows.Forms.Button();
			btnPrinterAllocation = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			btnKOTPrinter = new System.Windows.Forms.Button();
			btncatagory = new System.Windows.Forms.Button();
			btnproduct = new System.Windows.Forms.Button();
			grpstock.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			grpstock.Controls.Add(button3);
			grpstock.Controls.Add(button1);
			grpstock.Controls.Add(btnFavoriteProduct);
			grpstock.Controls.Add(btnPrinterAllocation);
			grpstock.Controls.Add(button2);
			grpstock.Controls.Add(btnKOTPrinter);
			grpstock.Controls.Add(btncatagory);
			grpstock.Controls.Add(btnproduct);
			resources.ApplyResources(grpstock, "grpstock");
			grpstock.Name = "grpstock";
			grpstock.TabStop = false;
			button3.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(button3, "button3");
			button3.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			button3.Name = "button3";
			button3.Tag = "37";
			button3.UseVisualStyleBackColor = false;
			button3.Click += new System.EventHandler(btnbutton_Click);
			button1.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(button1, "button1");
			button1.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			button1.Name = "button1";
			button1.Tag = "36";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(btnbutton_Click);
			btnFavoriteProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnFavoriteProduct, "btnFavoriteProduct");
			btnFavoriteProduct.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnFavoriteProduct.Name = "btnFavoriteProduct";
			btnFavoriteProduct.Tag = "5";
			btnFavoriteProduct.UseVisualStyleBackColor = false;
			btnFavoriteProduct.Click += new System.EventHandler(btnbutton_Click);
			btnPrinterAllocation.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnPrinterAllocation, "btnPrinterAllocation");
			btnPrinterAllocation.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnPrinterAllocation.Name = "btnPrinterAllocation";
			btnPrinterAllocation.Tag = "4";
			btnPrinterAllocation.UseVisualStyleBackColor = false;
			btnPrinterAllocation.Click += new System.EventHandler(btnbutton_Click);
			button2.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(button2, "button2");
			button2.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			button2.Name = "button2";
			button2.Tag = "6";
			button2.UseVisualStyleBackColor = false;
			button2.Click += new System.EventHandler(btnbutton_Click);
			btnKOTPrinter.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnKOTPrinter, "btnKOTPrinter");
			btnKOTPrinter.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnKOTPrinter.Name = "btnKOTPrinter";
			btnKOTPrinter.Tag = "3";
			btnKOTPrinter.UseVisualStyleBackColor = false;
			btnKOTPrinter.Click += new System.EventHandler(btnbutton_Click);
			btncatagory.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btncatagory, "btncatagory");
			btncatagory.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btncatagory.Name = "btncatagory";
			btncatagory.Tag = "2";
			btncatagory.UseVisualStyleBackColor = false;
			btncatagory.Click += new System.EventHandler(btnbutton_Click);
			btnproduct.BackColor = System.Drawing.SystemColors.ButtonFace;
			resources.ApplyResources(btnproduct, "btnproduct");
			btnproduct.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnproduct.Name = "btnproduct";
			btnproduct.Tag = "1";
			btnproduct.UseVisualStyleBackColor = false;
			btnproduct.Click += new System.EventHandler(btnbutton_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(grpstock);
			base.Controls.Add(lblHead);
			base.Name = "RestaurantForm";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(grpstock, 0);
			grpstock.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
