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
	public class Sales : BaseForm
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

		public Label lblHead;

		private Button btnUserSale;

		private Button btnVatReport;

		public DashboardPOS dashboard { get; set; }

		public Main mainForm { get; set; }

		public Sales()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void btnClick_Event(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Sales));
			groupBox1 = new System.Windows.Forms.GroupBox();
			btnstckprofit = new System.Windows.Forms.Button();
			btnsalesbyprod = new System.Windows.Forms.Button();
			btncashClosed = new System.Windows.Forms.Button();
			btnVatReport = new System.Windows.Forms.Button();
			btnUserSale = new System.Windows.Forms.Button();
			btncashFlow = new System.Windows.Forms.Button();
			btnsalesumm = new System.Windows.Forms.Button();
			btnsalesrpof = new System.Windows.Forms.Button();
			btntaxCategory = new System.Windows.Forms.Button();
			btncatsum = new System.Windows.Forms.Button();
			btntaxes = new System.Windows.Forms.Button();
			btntrans = new System.Windows.Forms.Button();
			btnpayments = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			groupBox1.Controls.Add(btnstckprofit);
			groupBox1.Controls.Add(btnsalesbyprod);
			groupBox1.Controls.Add(btncashClosed);
			groupBox1.Controls.Add(btnVatReport);
			groupBox1.Controls.Add(btnUserSale);
			groupBox1.Controls.Add(btncashFlow);
			groupBox1.Controls.Add(btnsalesumm);
			groupBox1.Controls.Add(btnsalesrpof);
			groupBox1.Controls.Add(btntaxCategory);
			groupBox1.Controls.Add(btncatsum);
			groupBox1.Controls.Add(btntaxes);
			groupBox1.Controls.Add(btntrans);
			groupBox1.Controls.Add(btnpayments);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(btnstckprofit, "btnstckprofit");
			btnstckprofit.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnstckprofit.Name = "btnstckprofit";
			btnstckprofit.Tag = "16";
			btnstckprofit.UseVisualStyleBackColor = true;
			btnstckprofit.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnsalesbyprod, "btnsalesbyprod");
			btnsalesbyprod.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsalesbyprod.Name = "btnsalesbyprod";
			btnsalesbyprod.Tag = "19";
			btnsalesbyprod.UseVisualStyleBackColor = true;
			btnsalesbyprod.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btncashClosed, "btncashClosed");
			btncashClosed.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btncashClosed.Name = "btncashClosed";
			btncashClosed.Tag = "20";
			btncashClosed.UseVisualStyleBackColor = true;
			btncashClosed.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnVatReport, "btnVatReport");
			btnVatReport.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnVatReport.Name = "btnVatReport";
			btnVatReport.Tag = "35";
			btnVatReport.UseVisualStyleBackColor = true;
			btnVatReport.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnUserSale, "btnUserSale");
			btnUserSale.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnUserSale.Name = "btnUserSale";
			btnUserSale.Tag = "34";
			btnUserSale.UseVisualStyleBackColor = true;
			btnUserSale.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btncashFlow, "btncashFlow");
			btncashFlow.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btncashFlow.Name = "btncashFlow";
			btncashFlow.Tag = "21";
			btncashFlow.UseVisualStyleBackColor = true;
			btncashFlow.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnsalesumm, "btnsalesumm");
			btnsalesumm.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsalesumm.Name = "btnsalesumm";
			btnsalesumm.Tag = "13";
			btnsalesumm.UseVisualStyleBackColor = true;
			btnsalesumm.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnsalesrpof, "btnsalesrpof");
			btnsalesrpof.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsalesrpof.Name = "btnsalesrpof";
			btnsalesrpof.Tag = "18";
			btnsalesrpof.UseVisualStyleBackColor = true;
			btnsalesrpof.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btntaxCategory, "btntaxCategory");
			btntaxCategory.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btntaxCategory.Name = "btntaxCategory";
			btntaxCategory.Tag = "15";
			btntaxCategory.UseVisualStyleBackColor = true;
			btntaxCategory.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btncatsum, "btncatsum");
			btncatsum.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btncatsum.Name = "btncatsum";
			btncatsum.Tag = "17";
			btncatsum.UseVisualStyleBackColor = true;
			btncatsum.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btntaxes, "btntaxes");
			btntaxes.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btntaxes.Name = "btntaxes";
			btntaxes.Tag = "14";
			btntaxes.UseVisualStyleBackColor = true;
			btntaxes.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btntrans, "btntrans");
			btntrans.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btntrans.Name = "btntrans";
			btntrans.Tag = "12";
			btntrans.UseVisualStyleBackColor = true;
			btntrans.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(btnpayments, "btnpayments");
			btnpayments.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnpayments.Name = "btnpayments";
			btnpayments.Tag = "11";
			btnpayments.UseVisualStyleBackColor = true;
			btnpayments.Click += new System.EventHandler(btnClick_Event);
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(lblHead);
			base.Controls.Add(groupBox1);
			base.Name = "Sales";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
