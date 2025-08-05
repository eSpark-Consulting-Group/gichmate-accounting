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
	public class InventoryReport : BaseForm
	{
		private IContainer components = null;

		public Label lblHead;

		private GroupBox groupBox1;

		private Button btninvdiff;

		private Button btninvdiary;

		private Button btnproducts;

		private Button btnRptOpening;

		public DashboardPOS dashboard { get; set; }

		public Main mainForm { get; set; }

		public InventoryReport()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryReport));
            this.lblHead = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRptOpening = new System.Windows.Forms.Button();
            this.btninvdiff = new System.Windows.Forms.Button();
            this.btninvdiary = new System.Windows.Forms.Button();
            this.btnproducts = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnEnd, "btnEnd");
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRptOpening);
            this.groupBox1.Controls.Add(this.btninvdiff);
            this.groupBox1.Controls.Add(this.btninvdiary);
            this.groupBox1.Controls.Add(this.btnproducts);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnRptOpening
            // 
            resources.ApplyResources(this.btnRptOpening, "btnRptOpening");
            this.btnRptOpening.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnRptOpening.Name = "btnRptOpening";
            this.btnRptOpening.Tag = "33";
            this.btnRptOpening.UseVisualStyleBackColor = true;
            this.btnRptOpening.Click += new System.EventHandler(this.Button_Click);
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
            // InventoryReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHead);
            this.Name = "InventoryReport";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
