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
	public class Customers : BaseForm
	{
		private IContainer components = null;

		public Label lblHead;

		private GroupBox groupBox1;

		private Button btncustomerdiary;

		private Button btncustmrsale;

		private Button btncustomers;

		private Button btnitemsales;

		public DashboardPOS dashboard { get; set; }

		public Main mainForm { get; set; }

		public Customers()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void button_Click(object sender, EventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customers));
            this.lblHead = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btncustomerdiary = new System.Windows.Forms.Button();
            this.btnitemsales = new System.Windows.Forms.Button();
            this.btncustmrsale = new System.Windows.Forms.Button();
            this.btncustomers = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btncustomerdiary);
            this.groupBox1.Controls.Add(this.btnitemsales);
            this.groupBox1.Controls.Add(this.btncustmrsale);
            this.groupBox1.Controls.Add(this.btncustomers);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btncustomerdiary
            // 
            resources.ApplyResources(this.btncustomerdiary, "btncustomerdiary");
            this.btncustomerdiary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustomerdiary.Name = "btncustomerdiary";
            this.btncustomerdiary.Tag = "23";
            this.btncustomerdiary.UseVisualStyleBackColor = true;
            this.btncustomerdiary.Click += new System.EventHandler(this.button_Click);
            // 
            // btnitemsales
            // 
            resources.ApplyResources(this.btnitemsales, "btnitemsales");
            this.btnitemsales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnitemsales.Name = "btnitemsales";
            this.btnitemsales.Tag = "25";
            this.btnitemsales.UseVisualStyleBackColor = true;
            this.btnitemsales.Click += new System.EventHandler(this.button_Click);
            // 
            // btncustmrsale
            // 
            resources.ApplyResources(this.btncustmrsale, "btncustmrsale");
            this.btncustmrsale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustmrsale.Name = "btncustmrsale";
            this.btncustmrsale.Tag = "24";
            this.btncustmrsale.UseVisualStyleBackColor = true;
            this.btncustmrsale.Click += new System.EventHandler(this.button_Click);
            // 
            // btncustomers
            // 
            resources.ApplyResources(this.btncustomers, "btncustomers");
            this.btncustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncustomers.Name = "btncustomers";
            this.btncustomers.Tag = "22";
            this.btncustomers.UseVisualStyleBackColor = true;
            this.btncustomers.Click += new System.EventHandler(this.button_Click);
            // 
            // Customers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHead);
            this.Name = "Customers";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
