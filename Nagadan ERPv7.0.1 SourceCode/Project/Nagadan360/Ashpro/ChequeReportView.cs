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
	public class ChequeReportView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string Query;

		private string strPartyDetails;

		private string strChqStatusDtls;

		private string strChqNoDtls;

		private string strRefNoDtls;

		private string strFormType;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbCheqStatus;

		private Label label17;

		private AshTextBoxNormal txtRefNo;

		private AshTextBoxNormal txtCheqNo;

		private AshComboBox cmbPartyAccnt;

		private Label label3;

		private Label label5;

		private Label label4;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label label2;

		private Label label1;

		private Label lblBranch;

		private AshComboBox cmbBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public ChequeReportView(string sFormType)
		{
			strFormType = sFormType;
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
			string[] headers = new string[10] { "SL", "Date", "Inv No", "Party Name", "Bank", "Branch", "Cheque No", "Amount", "Ref No", "Status" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(150, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(120, "L");
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			if (cmbPartyAccnt.Text != string.Empty)
			{
				strPartyDetails = " and AccountId='" + cmbPartyAccnt.SelectedValue.ToInt32() + "'";
			}
			else
			{
				strPartyDetails = "";
			}
			if (cmbCheqStatus.Text != string.Empty)
			{
				strChqStatusDtls = " and Status='" + cmbCheqStatus.Text + "'";
			}
			else
			{
				strChqStatusDtls = "";
			}
			if (txtCheqNo.Text != string.Empty)
			{
				strChqNoDtls = "and CheqNo='" + txtCheqNo.Text + "'";
			}
			else
			{
				strChqNoDtls = "";
			}
			if (txtRefNo.Text != string.Empty)
			{
				strRefNoDtls = " and RefNo='" + txtRefNo.Text + "'";
			}
			else
			{
				strRefNoDtls = "";
			}
			int num3 = 1;
			string strID = string.Empty;
			try
			{
				Query = "Select * From ChequeDetails Where CheqDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' and VchrType='" + strFormType + "'" + strPartyDetails + strChqStatusDtls + strChqNoDtls + strRefNoDtls + text;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					DateTime dateTime = Convert.ToDateTime(row[9].ToString());
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[3].ToString(), out strID);
					num2 = Convert.ToDecimal(row[5].ToString());
					num += num2;
					Details.Rows.Add(num3, dateTime.ToString("dd-MMM-yy"), row[1], strID, row[6], row[7], row[8], num2.ToString(CommonSettings.strCurencyFormat), row[11], row[10]);
					num3++;
				}
				Details.Rows.Add("", "", "", "Total", "", "", "", num.ToString(CommonSettings.strCurencyFormat), "", "");
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
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(65, 65);
				LineData.Rows.Add(110, 110);
				LineData.Rows.Add(300, 300);
				LineData.Rows.Add(450, 450);
				LineData.Rows.Add(600, 600);
				LineData.Rows.Add(660, 660);
				LineData.Rows.Add(720, 720);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Date", 9);
				Header.Rows.Add("Inv No", 66);
				Header.Rows.Add("Party Name", 112);
				Header.Rows.Add("Bank", 302);
				Header.Rows.Add("Branch", 452);
				Header.Rows.Add("Chg No", 601);
				Header.Rows.Add("Amount", 661);
				Header.Rows.Add("Status", 721);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(1, 8, false, false, "");
				data.Rows.Add(2, 66, true, false, "");
				data.Rows.Add(3, 111, true, true, "", 185);
				data.Rows.Add(4, 301, true, true, "", 145);
				data.Rows.Add(5, 451, true, true, "", 145);
				data.Rows.Add(6, 601, true, false, "");
				data.Rows.Add(7, 720, true, false, "Right");
				data.Rows.Add(9, 721, true, false, "");
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
			LineDataPDF.Rows.Add(525, 50, 525, 800);
			LineDataPDF.Rows.Add(475, 50, 475, 800);
			LineDataPDF.Rows.Add(415, 50, 415, 800);
			LineDataPDF.Rows.Add(345, 50, 345, 800);
			LineDataPDF.Rows.Add(215, 50, 215, 800);
			LineDataPDF.Rows.Add(85, 50, 85, 800);
			LineDataPDF.Rows.Add(45, 50, 45, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Date", 10);
			HeaderPDF.Rows.Add("Inv. No", 50);
			HeaderPDF.Rows.Add("Party Name", 90);
			HeaderPDF.Rows.Add("Bank", 220);
			HeaderPDF.Rows.Add("Branch", 350);
			HeaderPDF.Rows.Add("Cheque No", 420);
			HeaderPDF.Rows.Add("Amount", 480);
			HeaderPDF.Rows.Add("Status", 530);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(1, 10, false, false, "");
			dataPDF.Rows.Add(2, 47, true, false, "");
			dataPDF.Rows.Add(3, 87, true, true, "");
			dataPDF.Rows.Add(4, 217, true, true, "");
			dataPDF.Rows.Add(5, 347, true, true, "");
			dataPDF.Rows.Add(6, 417, true, false, "");
			dataPDF.Rows.Add(7, 524, true, false, "Right");
			dataPDF.Rows.Add(9, 525, true, false, "");
			dataPDF.Rows.Add(0, 10, true, true, "");
			dataPDF.Rows.Add(1, 105, true, false, "");
			dataPDF.Rows.Add(4, 147, true, false, "");
			dataPDF.Rows.Add(5, 414, true, false, "Right");
			dataPDF.Rows.Add(6, 484, true, false, "Right");
			dataPDF.Rows.Add(7, 583, true, false, "Right");
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				ReportGenerateMethod();
				PrintMethod();
				MailMethod();
				ReportView reportView = new ReportView();
				reportView = new ReportView();
				reportView.MdiParent = base.MdiParent;
				reportView.Details = Details;
				reportView.Gridview = Gridview;
				reportView.Title = lblTitle.Text + " from " + dtmFrom.Text + " to " + dtmTo.Text;
				reportView.data = data;
				reportView.dataPDF = dataPDF;
				reportView.LineData = LineData;
				reportView.LineDataPDF = LineDataPDF;
				reportView.Header = Header;
				reportView.HeaderPDF = HeaderPDF;
				reportView.Show();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			cmbCheqStatus.Text = string.Empty;
			cmbPartyAccnt.Focus();
		}

		private void ChequeReportView_Load(object sender, EventArgs e)
		{
			lblTitle.Text = strFormType + " Report";
			AniHelper.FillPartyAndSupplier(cmbPartyAccnt);
			AniHelper.FillBranch(cmbBranch);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChequeReportView));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbCheqStatus = new AshUsercontrol.AshComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRefNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtCheqNo = new AshUsercontrol.AshTextBoxNormal();
            this.cmbPartyAccnt = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.rdbSummary = new System.Windows.Forms.RadioButton();
            this.rdbDetails = new System.Windows.Forms.RadioButton();
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
            // cmbCheqStatus
            // 
            resources.ApplyResources(this.cmbCheqStatus, "cmbCheqStatus");
            this.cmbCheqStatus.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCheqStatus.FormattingEnabled = true;
            this.cmbCheqStatus.Items.AddRange(new object[] {
            resources.GetString("cmbCheqStatus.Items"),
            resources.GetString("cmbCheqStatus.Items1"),
            resources.GetString("cmbCheqStatus.Items2")});
            this.cmbCheqStatus.LeaveColor = System.Drawing.Color.White;
            this.cmbCheqStatus.Name = "cmbCheqStatus";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtRefNo
            // 
            resources.ApplyResources(this.txtRefNo, "txtRefNo");
            this.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRefNo.Data = 0;
            this.txtRefNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtRefNo.Format = null;
            this.txtRefNo.isAllowNegative = false;
            this.txtRefNo.isAllowSpecialChar = false;
            this.txtRefNo.isNumberOnly = false;
            this.txtRefNo.isNumeric = false;
            this.txtRefNo.LeaveColor = System.Drawing.Color.White;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCheqNo
            // 
            resources.ApplyResources(this.txtCheqNo, "txtCheqNo");
            this.txtCheqNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheqNo.Data = 0;
            this.txtCheqNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtCheqNo.Format = null;
            this.txtCheqNo.isAllowNegative = false;
            this.txtCheqNo.isAllowSpecialChar = false;
            this.txtCheqNo.isNumberOnly = false;
            this.txtCheqNo.isNumeric = false;
            this.txtCheqNo.LeaveColor = System.Drawing.Color.White;
            this.txtCheqNo.Name = "txtCheqNo";
            this.txtCheqNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbPartyAccnt
            // 
            resources.ApplyResources(this.cmbPartyAccnt, "cmbPartyAccnt");
            this.cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAccnt.FormattingEnabled = true;
            this.cmbPartyAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAccnt.Name = "cmbPartyAccnt";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
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
            // ChequeReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cmbCheqStatus);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtRefNo);
            this.Controls.Add(this.txtCheqNo);
            this.Controls.Add(this.cmbPartyAccnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Name = "ChequeReportView";
            this.Load += new System.EventHandler(this.ChequeReportView_Load);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbPartyAccnt, 0);
            this.Controls.SetChildIndex(this.txtCheqNo, 0);
            this.Controls.SetChildIndex(this.txtRefNo, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.cmbCheqStatus, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.rdbDetails, 0);
            this.Controls.SetChildIndex(this.rdbSummary, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
