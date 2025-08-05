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
	public class Supplier : BaseForm
	{
		private IContainer components = null;

		public Label lblHead;

		private GroupBox groupBox1;

		private Button btnsupllSales;

		private Button btnsuppldiary;

		private Button btnproducts;

		private Button btnsuppliers;

		public DashboardPOS dashboard { get; set; }

		public Main mainForm { get; set; }

		public Supplier()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Supplier));
			lblHead = new System.Windows.Forms.Label();
			groupBox1 = new System.Windows.Forms.GroupBox();
			btnsupllSales = new System.Windows.Forms.Button();
			btnsuppldiary = new System.Windows.Forms.Button();
			btnproducts = new System.Windows.Forms.Button();
			btnsuppliers = new System.Windows.Forms.Button();
			groupBox1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			groupBox1.Controls.Add(btnsupllSales);
			groupBox1.Controls.Add(btnsuppldiary);
			groupBox1.Controls.Add(btnproducts);
			groupBox1.Controls.Add(btnsuppliers);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(btnsupllSales, "btnsupllSales");
			btnsupllSales.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsupllSales.Name = "btnsupllSales";
			btnsupllSales.Tag = "32";
			btnsupllSales.UseVisualStyleBackColor = true;
			btnsupllSales.Click += new System.EventHandler(Button_Click);
			resources.ApplyResources(btnsuppldiary, "btnsuppldiary");
			btnsuppldiary.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsuppldiary.Name = "btnsuppldiary";
			btnsuppldiary.Tag = "30";
			btnsuppldiary.UseVisualStyleBackColor = true;
			btnsuppldiary.Click += new System.EventHandler(Button_Click);
			resources.ApplyResources(btnproducts, "btnproducts");
			btnproducts.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnproducts.Name = "btnproducts";
			btnproducts.Tag = "31";
			btnproducts.UseVisualStyleBackColor = true;
			btnproducts.Click += new System.EventHandler(Button_Click);
			resources.ApplyResources(btnsuppliers, "btnsuppliers");
			btnsuppliers.ForeColor = System.Drawing.Color.FromArgb(6, 123, 162);
			btnsuppliers.Name = "btnsuppliers";
			btnsuppliers.Tag = "29";
			btnsuppliers.UseVisualStyleBackColor = true;
			btnsuppliers.Click += new System.EventHandler(Button_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(groupBox1);
			base.Controls.Add(lblHead);
			base.Name = "Supplier";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
