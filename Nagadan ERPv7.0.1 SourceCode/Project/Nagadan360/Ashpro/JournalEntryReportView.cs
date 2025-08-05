using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class JournalEntryReportView : BaseForm
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

		private string strFromDetails;

		private string strToDetails;

		private string Query;

		private string strFromAccName;

		private string strToAccName;

		private IContainer components = null;

		private AshComboBox cmbToAccnt;

		private Label lblLedger;

		private Button btnShow;

		private ToolTip toolTip1;

		private Button btnClear;

		private AshDateTimePicker dtmTo;

		private Label label2;

		private AshDateTimePicker dtmFrom;

		private Label label1;

		private Label lblTitle;

		private AshComboBox cmbFromAccnt;

		private Label label3;

		private Label label6;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public DashboardPOS form { get; set; }

		public JournalEntryReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string[] headers = new string[7] { "SL", "Date", "Vchr No", "From Account(Credit)", "To Account(Debit)", "Amount", "Narration" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(400, "L");
			int num = 1;
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			if (cmbFromAccnt.Text != "")
			{
				strFromDetails = " and FromAccountId='" + cmbFromAccnt.SelectedValue.ToInt32() + "' ";
			}
			else
			{
				strFromDetails = "";
			}
			if (cmbToAccnt.Text != "")
			{
				strToDetails = " and ToAccountId='" + cmbToAccnt.SelectedValue.ToInt32() + "' ";
			}
			else
			{
				strToDetails = "";
			}
			Query = "Select VchrDate,VchrNo,FromAccountId,ToAccountId,VchrAmount,Narration From VchrTranLog Where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' and VchrType='Journal Voucher'" + strFromDetails + strToDetails + text + " Order by VchrDate,VchrNo";
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query);
			foreach (DataRow row in dataTable.Rows)
			{
				DateTime dateTime = Convert.ToDateTime(row[0]);
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[2].ToString(), out strFromAccName);
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[3].ToString(), out strToAccName);
				num3 = Convert.ToDecimal(row[4].ToString());
				num2 += num3;
				Details.Rows.Add(num, dateTime.ToString("dd-MMM-yyyy"), row[1], strFromAccName, strToAccName, num3.ToString("F"), row[5]);
				num++;
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(45, 45);
				LineData.Rows.Add(130, 130);
				LineData.Rows.Add(210, 210);
				LineData.Rows.Add(435, 435);
				LineData.Rows.Add(660, 660);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 8);
				Header.Rows.Add("Date", 48);
				Header.Rows.Add("Vchr No", 133);
				Header.Rows.Add("From Account", 212);
				Header.Rows.Add("To Account", 438);
				Header.Rows.Add("Amount", 662);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "");
				data.Rows.Add(1, 48, false, false, "");
				data.Rows.Add(2, 132, false, false, "");
				data.Rows.Add(3, 212, true, true, "", 220);
				data.Rows.Add(4, 438, false, true, "", 220);
				data.Rows.Add(5, 775, true, false, "Right");
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
			LineDataPDF.Rows.Add(340, 50, 340, 800);
			LineDataPDF.Rows.Add(170, 50, 170, 800);
			LineDataPDF.Rows.Add(100, 50, 100, 800);
			LineDataPDF.Rows.Add(30, 50, 30, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 10);
			HeaderPDF.Rows.Add("Date", 32);
			HeaderPDF.Rows.Add("Vchr No", 102);
			HeaderPDF.Rows.Add("From Account", 172);
			HeaderPDF.Rows.Add("To Account", 342);
			HeaderPDF.Rows.Add("Amount", 512);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, true, "");
			dataPDF.Rows.Add(1, 32, false, true, "");
			dataPDF.Rows.Add(2, 102, false, true, "");
			dataPDF.Rows.Add(3, 172, true, true, "");
			dataPDF.Rows.Add(4, 342, false, true, "");
			dataPDF.Rows.Add(5, 583, true, false, "Right");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			AshComboBox ashComboBox = cmbFromAccnt;
			AshComboBox ashComboBox2 = cmbToAccnt;
			string text = (cmbBranch.Text = string.Empty);
			string text4 = (ashComboBox.Text = (ashComboBox2.Text = text));
			cmbFromAccnt.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				sTitle = "Journal Entry Report From " + dtmFrom.Text + " To " + dtmTo.Text;
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
		}

		private void JournalEntryReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillPartyAndSupplier(cmbFromAccnt);
			AniHelper.FillPartyAndSupplier(cmbToAccnt);
			AniHelper.FillBranch(cmbBranch);
			AshComboBox ashComboBox = cmbFromAccnt;
			AshComboBox ashComboBox2 = cmbToAccnt;
			string text = (cmbBranch.Text = string.Empty);
			string text4 = (ashComboBox.Text = (ashComboBox2.Text = text));
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalEntryReportView));
            this.cmbToAccnt = new AshUsercontrol.AshComboBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbFromAccnt = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.rdbSummary = new System.Windows.Forms.RadioButton();
            this.rdbDetails = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // cmbToAccnt
            // 
            resources.ApplyResources(this.cmbToAccnt, "cmbToAccnt");
            this.cmbToAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbToAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbToAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbToAccnt.FormattingEnabled = true;
            this.cmbToAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbToAccnt.Name = "cmbToAccnt";
            this.toolTip1.SetToolTip(this.cmbToAccnt, resources.GetString("cmbToAccnt.ToolTip"));
            // 
            // lblLedger
            // 
            resources.ApplyResources(this.lblLedger, "lblLedger");
            this.lblLedger.Name = "lblLedger";
            this.toolTip1.SetToolTip(this.lblLedger, resources.GetString("lblLedger.ToolTip"));
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.toolTip1.SetToolTip(this.btnShow, resources.GetString("btnShow.ToolTip"));
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
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            this.toolTip1.SetToolTip(this.dtmTo, resources.GetString("dtmTo.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.toolTip1.SetToolTip(this.dtmFrom, resources.GetString("dtmFrom.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.toolTip1.SetToolTip(this.lblTitle, resources.GetString("lblTitle.ToolTip"));
            // 
            // cmbFromAccnt
            // 
            resources.ApplyResources(this.cmbFromAccnt, "cmbFromAccnt");
            this.cmbFromAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFromAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFromAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbFromAccnt.FormattingEnabled = true;
            this.cmbFromAccnt.Items.AddRange(new object[] {
            resources.GetString("cmbFromAccnt.Items"),
            resources.GetString("cmbFromAccnt.Items1"),
            resources.GetString("cmbFromAccnt.Items2"),
            resources.GetString("cmbFromAccnt.Items3"),
            resources.GetString("cmbFromAccnt.Items4"),
            resources.GetString("cmbFromAccnt.Items5"),
            resources.GetString("cmbFromAccnt.Items6"),
            resources.GetString("cmbFromAccnt.Items7")});
            this.cmbFromAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbFromAccnt.Name = "cmbFromAccnt";
            this.toolTip1.SetToolTip(this.cmbFromAccnt, resources.GetString("cmbFromAccnt.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Items.AddRange(new object[] {
            resources.GetString("cmbBranch.Items"),
            resources.GetString("cmbBranch.Items1"),
            resources.GetString("cmbBranch.Items2")});
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.toolTip1.SetToolTip(this.cmbBranch, resources.GetString("cmbBranch.ToolTip"));
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            this.toolTip1.SetToolTip(this.lblBranch, resources.GetString("lblBranch.ToolTip"));
            // 
            // rdbSummary
            // 
            resources.ApplyResources(this.rdbSummary, "rdbSummary");
            this.rdbSummary.Name = "rdbSummary";
            this.toolTip1.SetToolTip(this.rdbSummary, resources.GetString("rdbSummary.ToolTip"));
            this.rdbSummary.UseVisualStyleBackColor = true;
            // 
            // rdbDetails
            // 
            resources.ApplyResources(this.rdbDetails, "rdbDetails");
            this.rdbDetails.Checked = true;
            this.rdbDetails.Name = "rdbDetails";
            this.rdbDetails.TabStop = true;
            this.toolTip1.SetToolTip(this.rdbDetails, resources.GetString("rdbDetails.ToolTip"));
            this.rdbDetails.UseVisualStyleBackColor = true;
            // 
            // JournalEntryReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cmbToAccnt);
            this.Controls.Add(this.lblLedger);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbFromAccnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Name = "JournalEntryReportView";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.JournalEntryReportView_Load);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbFromAccnt, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblLedger, 0);
            this.Controls.SetChildIndex(this.cmbToAccnt, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.rdbDetails, 0);
            this.Controls.SetChildIndex(this.rdbSummary, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
