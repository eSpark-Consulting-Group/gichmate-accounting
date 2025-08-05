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
	public class POSReports : BaseForm
	{
		private IContainer components = null;

		private GroupBox groupBox1;

		private Button btnstckprofit;

		private Button btnsalesbyprod;

		private Button btncashClosed;

		private Button btncashFlow;

		private Button btnsalesumm;

		private Button btnsalesrpof;

		private Button btntaxCategory;

		private Button btncatsum;

		private Button btntaxes;

		private Button btntrans;

		private Button btnpayments;

		private Button btninvdiff;

		private Button btninvdiary;

		private Button btnproducts;

		private Button btncustomerdiary;

		private Button btncustomers;

		private Button btnitemsales;

		private Button btncustmrsale;

		private Button btnsuppldiary;

		private Button btnsuppliers;

		private Button btnsupllSales;

		private Button button1;

		public Label lblHead;

		private Button btnUserSale;

		public DashboardPOS dashboard { get; set; }

		public Main mainForm { get; set; }

		public POSReports()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			Utility.iReportFormNumber = 101;
		}

		private void Button_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, button.Text);
			if (previlageMethod != null && !previlageMethod.Enable)
			{
				Messages.InformationMessage("You have no authorisation to open this Window!");
				return;
			}
			Utility.iReportFormNumber = button.Tag.ToInt32();
			POSReportView frm = new POSReportView();
			if (dashboard != null)
			{
				dashboard.OpenForm(frm, 25, 25, "Report");
			}
			else
			{
				mainForm.OpenForm(frm);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSReports));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUserSale = new System.Windows.Forms.Button();
            this.btnsupllSales = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnsuppldiary = new System.Windows.Forms.Button();
            this.btnsuppliers = new System.Windows.Forms.Button();
            this.btnitemsales = new System.Windows.Forms.Button();
            this.btncustmrsale = new System.Windows.Forms.Button();
            this.btncustomerdiary = new System.Windows.Forms.Button();
            this.btncustomers = new System.Windows.Forms.Button();
            this.btninvdiff = new System.Windows.Forms.Button();
            this.btninvdiary = new System.Windows.Forms.Button();
            this.btnproducts = new System.Windows.Forms.Button();
            this.btnstckprofit = new System.Windows.Forms.Button();
            this.btnsalesbyprod = new System.Windows.Forms.Button();
            this.btncashClosed = new System.Windows.Forms.Button();
            this.btncashFlow = new System.Windows.Forms.Button();
            this.btnsalesumm = new System.Windows.Forms.Button();
            this.btnsalesrpof = new System.Windows.Forms.Button();
            this.btntaxCategory = new System.Windows.Forms.Button();
            this.btncatsum = new System.Windows.Forms.Button();
            this.btntaxes = new System.Windows.Forms.Button();
            this.btntrans = new System.Windows.Forms.Button();
            this.btnpayments = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnUserSale);
            this.groupBox1.Controls.Add(this.btnsupllSales);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnsuppldiary);
            this.groupBox1.Controls.Add(this.btnsuppliers);
            this.groupBox1.Controls.Add(this.btnitemsales);
            this.groupBox1.Controls.Add(this.btncustmrsale);
            this.groupBox1.Controls.Add(this.btncustomerdiary);
            this.groupBox1.Controls.Add(this.btncustomers);
            this.groupBox1.Controls.Add(this.btninvdiff);
            this.groupBox1.Controls.Add(this.btninvdiary);
            this.groupBox1.Controls.Add(this.btnproducts);
            this.groupBox1.Controls.Add(this.btnstckprofit);
            this.groupBox1.Controls.Add(this.btnsalesbyprod);
            this.groupBox1.Controls.Add(this.btncashClosed);
            this.groupBox1.Controls.Add(this.btncashFlow);
            this.groupBox1.Controls.Add(this.btnsalesumm);
            this.groupBox1.Controls.Add(this.btnsalesrpof);
            this.groupBox1.Controls.Add(this.btntaxCategory);
            this.groupBox1.Controls.Add(this.btncatsum);
            this.groupBox1.Controls.Add(this.btntaxes);
            this.groupBox1.Controls.Add(this.btntrans);
            this.groupBox1.Controls.Add(this.btnpayments);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnUserSale
            // 
            resources.ApplyResources(this.btnUserSale, "btnUserSale");
            this.btnUserSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnUserSale.Name = "btnUserSale";
            this.btnUserSale.Tag = "34";
            this.btnUserSale.UseVisualStyleBackColor = true;
            this.btnUserSale.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsupllSales
            // 
            resources.ApplyResources(this.btnsupllSales, "btnsupllSales");
            this.btnsupllSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsupllSales.Name = "btnsupllSales";
            this.btnsupllSales.Tag = "32";
            this.btnsupllSales.UseVisualStyleBackColor = true;
            this.btnsupllSales.Click += new System.EventHandler(this.Button_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.button1.Name = "button1";
            this.button1.Tag = "31";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsuppldiary
            // 
            resources.ApplyResources(this.btnsuppldiary, "btnsuppldiary");
            this.btnsuppldiary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsuppldiary.Name = "btnsuppldiary";
            this.btnsuppldiary.Tag = "30";
            this.btnsuppldiary.UseVisualStyleBackColor = true;
            this.btnsuppldiary.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsuppliers
            // 
            resources.ApplyResources(this.btnsuppliers, "btnsuppliers");
            this.btnsuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsuppliers.Name = "btnsuppliers";
            this.btnsuppliers.Tag = "29";
            this.btnsuppliers.UseVisualStyleBackColor = true;
            this.btnsuppliers.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnitemsales
            // 
            resources.ApplyResources(this.btnitemsales, "btnitemsales");
            this.btnitemsales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnitemsales.Name = "btnitemsales";
            this.btnitemsales.Tag = "25";
            this.btnitemsales.UseVisualStyleBackColor = true;
            this.btnitemsales.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncustmrsale
            // 
            resources.ApplyResources(this.btncustmrsale, "btncustmrsale");
            this.btncustmrsale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustmrsale.Name = "btncustmrsale";
            this.btncustmrsale.Tag = "24";
            this.btncustmrsale.UseVisualStyleBackColor = true;
            this.btncustmrsale.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncustomerdiary
            // 
            resources.ApplyResources(this.btncustomerdiary, "btncustomerdiary");
            this.btncustomerdiary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustomerdiary.Name = "btncustomerdiary";
            this.btncustomerdiary.Tag = "23";
            this.btncustomerdiary.UseVisualStyleBackColor = true;
            this.btncustomerdiary.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncustomers
            // 
            resources.ApplyResources(this.btncustomers, "btncustomers");
            this.btncustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustomers.Name = "btncustomers";
            this.btncustomers.Tag = "22";
            this.btncustomers.UseVisualStyleBackColor = true;
            this.btncustomers.Click += new System.EventHandler(this.Button_Click);
            // 
            // btninvdiff
            // 
            resources.ApplyResources(this.btninvdiff, "btninvdiff");
            this.btninvdiff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btninvdiff.Name = "btninvdiff";
            this.btninvdiff.Tag = "28";
            this.btninvdiff.UseVisualStyleBackColor = true;
            this.btninvdiff.Click += new System.EventHandler(this.Button_Click);
            // 
            // btninvdiary
            // 
            resources.ApplyResources(this.btninvdiary, "btninvdiary");
            this.btninvdiary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btninvdiary.Name = "btninvdiary";
            this.btninvdiary.Tag = "27";
            this.btninvdiary.UseVisualStyleBackColor = true;
            this.btninvdiary.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnproducts
            // 
            resources.ApplyResources(this.btnproducts, "btnproducts");
            this.btnproducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnproducts.Name = "btnproducts";
            this.btnproducts.Tag = "26";
            this.btnproducts.UseVisualStyleBackColor = true;
            this.btnproducts.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnstckprofit
            // 
            resources.ApplyResources(this.btnstckprofit, "btnstckprofit");
            this.btnstckprofit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnstckprofit.Name = "btnstckprofit";
            this.btnstckprofit.Tag = "16";
            this.btnstckprofit.UseVisualStyleBackColor = true;
            this.btnstckprofit.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsalesbyprod
            // 
            resources.ApplyResources(this.btnsalesbyprod, "btnsalesbyprod");
            this.btnsalesbyprod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsalesbyprod.Name = "btnsalesbyprod";
            this.btnsalesbyprod.Tag = "19";
            this.btnsalesbyprod.UseVisualStyleBackColor = true;
            this.btnsalesbyprod.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncashClosed
            // 
            resources.ApplyResources(this.btncashClosed, "btncashClosed");
            this.btncashClosed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncashClosed.Name = "btncashClosed";
            this.btncashClosed.Tag = "20";
            this.btncashClosed.UseVisualStyleBackColor = true;
            this.btncashClosed.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncashFlow
            // 
            resources.ApplyResources(this.btncashFlow, "btncashFlow");
            this.btncashFlow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncashFlow.Name = "btncashFlow";
            this.btncashFlow.Tag = "21";
            this.btncashFlow.UseVisualStyleBackColor = true;
            this.btncashFlow.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsalesumm
            // 
            resources.ApplyResources(this.btnsalesumm, "btnsalesumm");
            this.btnsalesumm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsalesumm.Name = "btnsalesumm";
            this.btnsalesumm.Tag = "13";
            this.btnsalesumm.UseVisualStyleBackColor = true;
            this.btnsalesumm.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnsalesrpof
            // 
            resources.ApplyResources(this.btnsalesrpof, "btnsalesrpof");
            this.btnsalesrpof.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsalesrpof.Name = "btnsalesrpof";
            this.btnsalesrpof.Tag = "18";
            this.btnsalesrpof.UseVisualStyleBackColor = true;
            this.btnsalesrpof.Click += new System.EventHandler(this.Button_Click);
            // 
            // btntaxCategory
            // 
            resources.ApplyResources(this.btntaxCategory, "btntaxCategory");
            this.btntaxCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btntaxCategory.Name = "btntaxCategory";
            this.btntaxCategory.Tag = "15";
            this.btntaxCategory.UseVisualStyleBackColor = true;
            this.btntaxCategory.Click += new System.EventHandler(this.Button_Click);
            // 
            // btncatsum
            // 
            resources.ApplyResources(this.btncatsum, "btncatsum");
            this.btncatsum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncatsum.Name = "btncatsum";
            this.btncatsum.Tag = "17";
            this.btncatsum.UseVisualStyleBackColor = true;
            this.btncatsum.Click += new System.EventHandler(this.Button_Click);
            // 
            // btntaxes
            // 
            resources.ApplyResources(this.btntaxes, "btntaxes");
            this.btntaxes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btntaxes.Name = "btntaxes";
            this.btntaxes.Tag = "14";
            this.btntaxes.UseVisualStyleBackColor = true;
            this.btntaxes.Click += new System.EventHandler(this.Button_Click);
            // 
            // btntrans
            // 
            resources.ApplyResources(this.btntrans, "btntrans");
            this.btntrans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btntrans.Name = "btntrans";
            this.btntrans.Tag = "12";
            this.btntrans.UseVisualStyleBackColor = true;
            this.btntrans.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnpayments
            // 
            resources.ApplyResources(this.btnpayments, "btnpayments");
            this.btnpayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnpayments.Name = "btnpayments";
            this.btnpayments.Tag = "11";
            this.btnpayments.UseVisualStyleBackColor = true;
            this.btnpayments.Click += new System.EventHandler(this.Button_Click);
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // POSReports
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.groupBox1);
            this.Name = "POSReports";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
