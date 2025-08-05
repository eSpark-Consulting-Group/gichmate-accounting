using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class PartyListView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string sTitle;

		private string strQuery;

		private string strBalance;

		private decimal dmlDebit;

		private decimal dmlCredit;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private RadioButton rdbCreditors;

		private RadioButton rdbDebtors;

		private AshComboBox cmbArea;

		private Label label1;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		private Panel panel1;

		public DashboardPOS form { get; set; }

		public PartyListView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			decimal num = default(decimal);
			int num2 = 1;
			string[] headers = new string[15]
			{
				"SL", "ID", "Name", "Address One", "Address Two", "Address Three", "Phone No", "Mobile No", "Fax", "TAX No",
				"State Code", "Email", "Debit", "Credit", "Balance"
			};
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(60, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			if (rdbDebtors.Checked)
			{
				if (cmbArea.Text == "ALL")
				{
					strQuery = "Select AccountId,AccountName,AddressOne,AddressTwo,AddressThree,PhoneOne,MobileNo,FaxNo,TinNo,CstNo,EMailOne from Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId where GroupName='CUSTOMER/SUNDRY DEBTORS' order by AccountName";
				}
				else
				{
					strQuery = "Select AccountId,AccountName,AddressOne,AddressTwo,AddressThree,PhoneOne,MobileNo,FaxNo,TinNo,CstNo,EMailOne from Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Join Root on Account.RootId=Root.RootId where GroupName='CUSTOMER/SUNDRY DEBTORS' and RootName='" + cmbArea.Text + "' order by AccountName";
				}
			}
			else
			{
				strQuery = "Select AccountId,AccountName,AddressOne,AddressTwo,AddressThree,PhoneOne,MobileNo,FaxNo,TinNo,CstNo,EMailOne from Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId where AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS' order by AccountName";
			}
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				num = BalanceFindMethod(row[0].ToString());
				if (num >= 0m)
				{
					strBalance = num.ToString("F") + " DR";
				}
				else
				{
					strBalance = Math.Abs(num).ToString("F") + " CR";
				}
				Details.Rows.Add(num2, row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], dmlDebit.ToString("F"), dmlCredit.ToString("F"), strBalance);
				num2++;
			}
		}

		private decimal BalanceFindMethod(string strAId)
		{
			dmlDebit = default(decimal);
			dmlCredit = default(decimal);
			try
			{
				strQuery = "Select sum(DebitAmount), sum(CreditAmount) From TransactionLog where AccountId='" + strAId + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					if (row[0] != DBNull.Value)
					{
						dmlDebit = Convert.ToDecimal(row[0].ToString());
					}
					if (row[1] != DBNull.Value)
					{
						dmlCredit = Convert.ToDecimal(row[1].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return dmlDebit - dmlCredit;
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(50, 50);
				LineData.Rows.Add(250, 250);
				LineData.Rows.Add(400, 400);
				LineData.Rows.Add(550, 550);
				LineData.Rows.Add(650, 650);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 14);
				Header.Rows.Add("Party Name", 55);
				Header.Rows.Add("Address One", 255);
				Header.Rows.Add("Address Two", 405);
				Header.Rows.Add("Phone No", 555);
				Header.Rows.Add("Balance", 670);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "");
				data.Rows.Add(2, 51, false, true, "", 190);
				data.Rows.Add(3, 251, false, true, "", 145);
				data.Rows.Add(4, 401, false, true, "", 145);
				data.Rows.Add(7, 551, false, true, "", 100);
				data.Rows.Add(14, 651, false, false, "");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			LineDataPDF = AniHelper.GetLineDatatablePDF();
			LineDataPDF.Rows.Add(8, 50, 585, 50);
			LineDataPDF.Rows.Add(8, 50, 8, 800);
			LineDataPDF.Rows.Add(8, 800, 585, 800);
			LineDataPDF.Rows.Add(585, 50, 585, 800);
			LineDataPDF.Rows.Add(510, 50, 510, 800);
			LineDataPDF.Rows.Add(440, 50, 440, 800);
			LineDataPDF.Rows.Add(320, 50, 320, 800);
			LineDataPDF.Rows.Add(200, 50, 200, 800);
			LineDataPDF.Rows.Add(45, 50, 45, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 10);
			HeaderPDF.Rows.Add("Party Name", 50);
			HeaderPDF.Rows.Add("Address One", 205);
			HeaderPDF.Rows.Add("Address Two", 325);
			HeaderPDF.Rows.Add("Mobile", 445);
			HeaderPDF.Rows.Add("Balance", 515);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(2, 50, false, true, "", 150);
			dataPDF.Rows.Add(3, 205, false, true, "", 120);
			dataPDF.Rows.Add(4, 325, false, true, "", 105);
			dataPDF.Rows.Add(7, 445, false, true, "", 65);
			dataPDF.Rows.Add(14, 515, false, false, "");
		}

		private void PartyListView_Load(object sender, EventArgs e)
		{
			AniHelper.FillRoute(cmbArea);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			rdbDebtors.Checked = true;
			cmbArea.SelectedIndex = 0;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (!rdbDetails.Checked)
			{
				return;
			}
			if (rdbDebtors.Checked)
			{
				if (cmbArea.Text != "ALL")
				{
					sTitle = "List of Customer belong to the route " + cmbArea.Text;
				}
				else
				{
					sTitle = "List of Customer";
				}
			}
			else
			{
				sTitle = "List of Suppliers";
			}
			ReportGenerateMethod();
			PrintMethod();
			MailMethod();
			ReportView reportView = new ReportView();
			reportView = new ReportView();
			reportView.MdiParent = base.MdiParent;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Title = sTitle;
			reportView.data = data;
			reportView.dataPDF = dataPDF;
			reportView.LineData = LineData;
			reportView.LineDataPDF = LineDataPDF;
			reportView.Header = Header;
			reportView.HeaderPDF = HeaderPDF;
			if (form != null)
			{
				form.OpenForm(reportView, 0, 0);
			}
			else
			{
				reportView.Show();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartyListView));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rdbCreditors = new System.Windows.Forms.RadioButton();
            this.rdbDebtors = new System.Windows.Forms.RadioButton();
            this.cmbArea = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbSummary = new System.Windows.Forms.RadioButton();
            this.rdbDetails = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
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
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rdbCreditors
            // 
            resources.ApplyResources(this.rdbCreditors, "rdbCreditors");
            this.rdbCreditors.Name = "rdbCreditors";
            this.rdbCreditors.TabStop = true;
            this.rdbCreditors.UseVisualStyleBackColor = true;
            // 
            // rdbDebtors
            // 
            resources.ApplyResources(this.rdbDebtors, "rdbDebtors");
            this.rdbDebtors.Checked = true;
            this.rdbDebtors.Name = "rdbDebtors";
            this.rdbDebtors.TabStop = true;
            this.rdbDebtors.UseVisualStyleBackColor = true;
            // 
            // cmbArea
            // 
            resources.ApplyResources(this.cmbArea, "cmbArea");
            this.cmbArea.EnterColor = System.Drawing.Color.LightPink;
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.LeaveColor = System.Drawing.Color.White;
            this.cmbArea.Name = "cmbArea";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // rdbSummary
            // 
            resources.ApplyResources(this.rdbSummary, "rdbSummary");
            this.rdbSummary.Name = "rdbSummary";
            this.rdbSummary.UseVisualStyleBackColor = true;
            // 
            // rdbDetails
            // 
            resources.ApplyResources(this.rdbDetails, "rdbDetails");
            this.rdbDetails.Checked = true;
            this.rdbDetails.Name = "rdbDetails";
            this.rdbDetails.TabStop = true;
            this.rdbDetails.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rdbCreditors);
            this.panel1.Controls.Add(this.rdbDebtors);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbArea);
            this.panel1.Name = "panel1";
            // 
            // PartyListView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Name = "PartyListView";
            this.Load += new System.EventHandler(this.PartyListView_Load);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.rdbDetails, 0);
            this.Controls.SetChildIndex(this.rdbSummary, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
