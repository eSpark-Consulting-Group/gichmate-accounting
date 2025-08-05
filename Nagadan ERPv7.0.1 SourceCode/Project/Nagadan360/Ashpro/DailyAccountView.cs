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
	public class DailyAccountView : BaseForm
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

		private string Query;

		private IContainer components = null;

		private Button btnShow;

		private ToolTip toolTip1;

		private Button btnClear;

		private AshDateTimePicker dtmTo;

		private Label lblTo;

		private AshDateTimePicker dtmFrom;

		private Label lblFrom;

		private Label lblTitle;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public string PassingValue { get; set; }

		public DashboardPOS form { get; set; }

		public DailyAccountView()
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
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string strID = string.Empty;
			string[] headers = new string[5] { "SL", "Date", "Account Name", "Debit", "Credit" };
			string[] headers2 = new string[7] { "SL", "Date", "Inv No", "Vchr Type", "Account Name", "Debit", "Credit" };
			Gridview = AniHelper.GetGridviewDatatable();
			if (PassingValue == "Summary")
			{
				Details = AniHelper.GetDataTable(headers);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			else
			{
				Details = AniHelper.GetDataTable(headers2);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			try
			{
				int num3 = 1;
				if (PassingValue == "Summary")
				{
					Query = "Select Convert(nvarchar,VchrDate,103) as Date,VchrNo,VchrType,AccountId,DebitAmount,CreditAmount from TransactionLog where VchrDate between'" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text;
				}
				else
				{
					Query = "Select Convert(nvarchar,VchrDate,103) as Date,VchrNo,VchrType,AccountId,DebitAmount,CreditAmount from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'" + text;
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num += Convert.ToDecimal(row[4].ToString());
					num2 += Convert.ToDecimal(row[5].ToString());
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[3].ToString(), out strID);
					if (PassingValue == "Summary")
					{
						Details.Rows.Add(num3, row[0], strID, row[4], row[5]);
					}
					else
					{
						Details.Rows.Add(num3, row[0], row[1], row[2], strID, row[4], row[5]);
					}
					num3++;
				}
				if (PassingValue == "Summary")
				{
					Details.Rows.Add("", "", "", "", "");
					Details.Rows.Add("", "", "Total", num.ToString("F"), num2.ToString("F"));
				}
				else
				{
					Details.Rows.Add("", "", "", "", "", "", "");
					Details.Rows.Add("", "", "Total", "", "", num.ToString("F"), num2.ToString("F"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PrintMethod()
		{
			try
			{
				if (PassingValue == "Summary")
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(50, 50);
					LineData.Rows.Add(140, 140);
					LineData.Rows.Add(520, 520);
					LineData.Rows.Add(650, 650);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("SL", 8);
					Header.Rows.Add("Date", 53);
					Header.Rows.Add("Account Name", 142);
					Header.Rows.Add("Debit", 522);
					Header.Rows.Add("Credit", 652);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(0, 8, false, false, "");
					data.Rows.Add(1, 52, false, false, "");
					data.Rows.Add(2, 142, true, true, "", 500);
					data.Rows.Add(3, 649, true, false, "Right");
					data.Rows.Add(4, 769, true, false, "Right");
				}
				else
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(80, 80);
					LineData.Rows.Add(140, 140);
					LineData.Rows.Add(240, 240);
					LineData.Rows.Add(560, 560);
					LineData.Rows.Add(660, 660);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("Date", 8);
					Header.Rows.Add("Inv No", 81);
					Header.Rows.Add("Inv Type", 142);
					Header.Rows.Add("Account Name", 242);
					Header.Rows.Add("Debit", 562);
					Header.Rows.Add("Credit", 662);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(1, 8, false, false, "");
					data.Rows.Add(2, 81, false, false, "");
					data.Rows.Add(3, 141, false, true, "");
					data.Rows.Add(4, 241, true, true, "");
					data.Rows.Add(5, 659, true, false, "Right");
					data.Rows.Add(6, 769, true, false, "Right");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			if (PassingValue == "Summary")
			{
				LineDataPDF = AniHelper.GetLineDatatablePDF();
				LineDataPDF.Rows.Add(8, 50, 585, 50);
				LineDataPDF.Rows.Add(8, 50, 8, 800);
				LineDataPDF.Rows.Add(8, 800, 585, 800);
				LineDataPDF.Rows.Add(8, 70, 585, 70);
				LineDataPDF.Rows.Add(585, 50, 585, 800);
				LineDataPDF.Rows.Add(500, 50, 500, 800);
				LineDataPDF.Rows.Add(415, 50, 415, 800);
				LineDataPDF.Rows.Add(140, 50, 140, 800);
				LineDataPDF.Rows.Add(45, 50, 45, 800);
				HeaderPDF = AniHelper.GetHeadDatatable();
				HeaderPDF.Rows.Add("SL", 9);
				HeaderPDF.Rows.Add("Date", 47);
				HeaderPDF.Rows.Add("Account Name", 142);
				HeaderPDF.Rows.Add("Debit", 418);
				HeaderPDF.Rows.Add("Credit", 502);
				dataPDF = AniHelper.GetDetailsDatatable();
				dataPDF.Rows.Add(0, 10, false, true, "");
				dataPDF.Rows.Add(1, 47, false, true, "");
				dataPDF.Rows.Add(2, 142, true, true, "");
				dataPDF.Rows.Add(3, 499, true, false, "Right");
				dataPDF.Rows.Add(4, 584, true, false, "Right");
			}
			else
			{
				LineDataPDF = AniHelper.GetLineDatatablePDF();
				LineDataPDF.Rows.Add(8, 50, 585, 50);
				LineDataPDF.Rows.Add(8, 50, 8, 800);
				LineDataPDF.Rows.Add(8, 800, 585, 800);
				LineDataPDF.Rows.Add(8, 70, 585, 70);
				LineDataPDF.Rows.Add(585, 50, 585, 800);
				LineDataPDF.Rows.Add(510, 50, 510, 800);
				LineDataPDF.Rows.Add(450, 50, 450, 800);
				LineDataPDF.Rows.Add(210, 50, 210, 800);
				LineDataPDF.Rows.Add(110, 50, 110, 800);
				LineDataPDF.Rows.Add(65, 50, 65, 800);
				HeaderPDF = AniHelper.GetHeadDatatable();
				HeaderPDF.Rows.Add("Date", 9);
				HeaderPDF.Rows.Add("Inv. No", 70);
				HeaderPDF.Rows.Add("Inv Type", 115);
				HeaderPDF.Rows.Add("Account Name", 215);
				HeaderPDF.Rows.Add("Debit", 455);
				HeaderPDF.Rows.Add("Credit", 515);
				dataPDF = AniHelper.GetDetailsDatatable();
				dataPDF.Rows.Add(1, 10, false, true, "");
				dataPDF.Rows.Add(2, 66, false, true, "");
				dataPDF.Rows.Add(3, 111, false, true, "");
				dataPDF.Rows.Add(4, 211, true, true, "");
				dataPDF.Rows.Add(5, 509, true, false, "Right");
				dataPDF.Rows.Add(6, 584, true, false, "Right");
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			dtmFrom.Focus();
		}

		private void DailyAccountView_Load(object sender, EventArgs e)
		{
			AniHelper.FillBranch(cmbBranch);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			if (PassingValue == "Summary")
			{
				lblTo.Visible = false;
				dtmTo.Visible = false;
				lblFrom.Text = "Date";
			}
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				if (PassingValue == "Summary")
				{
					sTitle = "Daily Account Statement of the Date " + dtmFrom.Text;
				}
				else
				{
					sTitle = "Daily Account Statement from " + dtmFrom.Text + " to " + dtmTo.Text;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyAccountView));
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
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
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            this.toolTip1.SetToolTip(this.lblTo, resources.GetString("lblTo.ToolTip"));
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.toolTip1.SetToolTip(this.dtmFrom, resources.GetString("dtmFrom.ToolTip"));
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.Name = "lblFrom";
            this.toolTip1.SetToolTip(this.lblFrom, resources.GetString("lblFrom.ToolTip"));
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.toolTip1.SetToolTip(this.lblTitle, resources.GetString("lblTitle.ToolTip"));
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
            // DailyAccountView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTitle);
            this.Name = "DailyAccountView";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.DailyAccountView_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblFrom, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.lblTo, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
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
