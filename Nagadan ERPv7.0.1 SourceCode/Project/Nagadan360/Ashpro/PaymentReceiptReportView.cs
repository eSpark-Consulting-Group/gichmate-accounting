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
	public class PaymentReceiptReportView : BaseForm
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

		private string strVoucherType;

		private int Index;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private RadioButton rdbAll;

		private RadioButton rdbBank;

		private RadioButton rdbCash;

		private AshComboBox cmbSM;

		private AshComboBox cmbRoute;

		private Label label7;

		private AshComboBox cmbAccGroup;

		private Label label4;

		private Label label2;

		private AshComboBox cmbAccount;

		private Label label1;

		private RadioButton rdbEntryDate;

		private RadioButton rdbVoucherDate;

		private AshDateTimePicker dtmTo;

		private Label label5;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private Panel panel3;

		private AshComboBox cmbAccWise;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Label label3;

		private Panel panel1;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public string ValueDetails { get; set; }

		public DashboardPOS form { get; set; }

		public PaymentReceiptReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private decimal SumofAmountFindMethod(string strAccId)
		{
			if (ValueDetails.Contains("Payment"))
			{
				strQuery = "Select sum(VchrAmount) as Amount From VchrTranLog where ToAccountId='" + strAccId + "' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'" + sBranchDetails;
			}
			else
			{
				strQuery = "Select sum(VchrAmount) as Amount From VchrTranLog where FromAccountId='" + strAccId + "' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'" + sBranchDetails;
			}
			return AshproCommon.ValueFindMethod(strQuery);
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string empty = string.Empty;
			string text = string.Empty;
			string empty2 = string.Empty;
			string[] headers = new string[9] { "Date", "Vchr No", "Vchr Type", "Particulars", "Amount", "Narration", "Exicutive", "User", "Entry Date" };
			string[] headers2 = new string[4] { "SL", "Particulars", "Account Group", "Amount" };
			Gridview = AniHelper.GetGridviewDatatable();
			if (ValueDetails.Contains("Summary"))
			{
				Details = AniHelper.GetDataTable(headers2);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(120, "R");
			}
			else
			{
				Details = AniHelper.GetDataTable(headers);
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(350, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(100, "L");
			}
			decimal num = default(decimal);
			empty = ((ValueDetails == "Receipt Report" || ValueDetails == "Receipt Report Summary") ? " Join Account on VchrTranLog.ToAccountId=Account.AccountId " : " Join Account on VchrTranLog.FromAccountId=Account.AccountId ");
			string text2 = " Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId ";
			if (cmbAccount.Text != "")
			{
				text = "";
				empty2 = " and Account.AccountName = '" + cmbAccount.Text + "' ";
			}
			else
			{
				empty2 = "";
				if (rdbCash.Checked)
				{
					text = " and AccountGroup.GroupName= 'CASH IN HAND' ";
				}
				else if (rdbBank.Checked)
				{
					text = " and AccountGroup.GroupName= 'CASH AT BANK' ";
				}
				else if (rdbAll.Checked)
				{
					text = " and AccountGroup.GroupName IN ('CASH IN HAND' , 'CASH AT BANK') ";
				}
			}
			string text3 = "VchrDate";
			if (!rdbVoucherDate.Checked)
			{
				text3 = "TranDate";
			}
			string text4 = "";
			string text5 = "";
			if (cmbSM.Text != "")
			{
				text4 = " and EmployeeDetails.EmpName='" + cmbSM.Text + "' ";
				text5 = " Join EmployeeDetails on VchrTranLog.SalesManId=EmployeeDetails.EmpID ";
			}
			string text6 = "";
			if (cmbAccWise.Text != "")
			{
				text6 = ((ValueDetails == "Receipt Report" || ValueDetails == "Receipt Report Summary") ? (" and VchrTranLog.FromAccountId='" + cmbAccWise.SelectedValue.ToInt32() + "' ") : (" and VchrTranLog.ToAccountId='" + cmbAccWise.SelectedValue.ToInt32() + "' "));
			}
			string text7 = "";
			if (cmbAccGroup.Text != "" && cmbRoute.Text == "")
			{
				text7 = ((ValueDetails == "Receipt Report" || ValueDetails == "Receipt Report Summary") ? (" and VchrTranLog.FromAccountId in (select Account.AccountId from Account where Account.AGroupId='" + cmbAccGroup.SelectedValue.ToInt32() + "') ") : (" and VchrTranLog.ToAccountId in (select Account.AccountId from Account where Account.AGroupId='" + cmbAccGroup.SelectedValue.ToInt32() + "') "));
			}
			else if (cmbAccGroup.Text != "" && cmbRoute.Text != "")
			{
				text7 = ((ValueDetails == "Receipt Report" || ValueDetails == "Receipt Report Summary") ? (" and VchrTranLog.FromAccountId in (select Account.AccountId from Account where Account.AGroupId='" + cmbAccGroup.SelectedValue.ToInt32() + "' and Account.RootId='" + cmbRoute.SelectedValue.ToInt32() + "') ") : (" and VchrTranLog.ToAccountId in (select Account.AccountId from Account where Account.AGroupId='" + cmbAccGroup.SelectedValue.ToInt32() + "' and Account.RootId='" + cmbRoute.SelectedValue.ToInt32() + "') "));
			}
			else if (cmbAccGroup.Text == "" && cmbRoute.Text != "")
			{
				text7 = ((ValueDetails == "Receipt Report" || ValueDetails == "Receipt Report Summary") ? (" and VchrTranLog.FromAccountId in (select Account.AccountId from Account where Account.RootId='" + cmbRoute.SelectedValue.ToInt32() + "') ") : (" and VchrTranLog.ToAccountId in (select Account.AccountId from Account where Account.RootId='" + cmbRoute.SelectedValue.ToInt32() + "') "));
			}
			string strID = "";
			string strID2 = "";
			string strID3 = "";
			try
			{
				if (!ValueDetails.Contains("Summary"))
				{
					strQuery = "Select * from VchrTranLog " + empty + text2 + text5 + " where VchrType='" + strVoucherType + "' and " + text3 + " between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + empty2 + text + text4 + text6 + text7 + sBranchDetails + " order by VchrDate,VchrNo ";
					DataTable dataTable = new DataTable();
					dataTable = AshproCommon.GetDataTable(strQuery);
					foreach (DataRow row in dataTable.Rows)
					{
						DateTime dateTime = Convert.ToDateTime(row[2].ToString());
						DateTime dateTime2 = Convert.ToDateTime(row[10].ToString());
						num += Convert.ToDecimal(row[3].ToString());
						AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[Index].ToString(), out strID);
						AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", row[9].ToString(), out strID2);
						AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", row[8].ToString(), out strID3);
						Details.Rows.Add(dateTime.ToString("dd-MMM-yyyy"), row[1], row[4], strID, row[3], row[7], strID3, strID2, dateTime2.ToString("dd-MMM-yyyy"));
					}
				}
				else
				{
					if (ValueDetails.Contains("Payment"))
					{
						strQuery = "Select distinct(ToAccountId) as Account from VchrTranLog " + empty + text2 + " where VchrType='" + strVoucherType + "' and " + text3 + " between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + text6 + text7 + sBranchDetails;
					}
					else
					{
						strQuery = "Select distinct(FromAccountId) as Account from VchrTranLog " + empty + text2 + " where VchrType='" + strVoucherType + "' and " + text3 + " between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + text6 + text7 + sBranchDetails;
					}
					string strID4 = "";
					string strID5 = "";
					decimal num2 = default(decimal);
					DataTable dataTable2 = new DataTable();
					dataTable2 = AshproCommon.GetDataTable(strQuery);
					foreach (DataRow row2 in dataTable2.Rows)
					{
						int num3 = Details.Rows.Count + 1;
						AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row2[0].ToString(), out strID);
						AshproCommon.IdFindMethod("Account", "AGroupId", "AccountId", row2[0].ToString(), out strID4);
						AshproCommon.IdFindMethod("AccountGroup", "GroupName", "AGroupId", strID4, out strID5);
						num2 = SumofAmountFindMethod(row2[0].ToString());
						num += num2;
						Details.Rows.Add(num3, strID, strID5, num2);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			if (!ValueDetails.Contains("Summary"))
			{
				Details.Rows.Add("", "", "", "Total", num.ToString("F"), "", "", "", "");
			}
			else
			{
				Details.Rows.Add("", "Total", "", num.ToString("F"));
			}
		}

		private void PrintMethod()
		{
			try
			{
				if (ValueDetails == "Receipt Report")
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(90, 90);
					LineData.Rows.Add(160, 160);
					LineData.Rows.Add(300, 300);
					LineData.Rows.Add(500, 500);
					LineData.Rows.Add(610, 610);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("Vchr Date", 10);
					Header.Rows.Add("Vchr No", 92);
					Header.Rows.Add("Vchr Type", 162);
					Header.Rows.Add("Particulars", 302);
					Header.Rows.Add("Amount", 502);
					Header.Rows.Add("Exicutive", 612);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(0, 10, false, false, "", 0);
					data.Rows.Add(1, 92, false, false, "", 0);
					data.Rows.Add(2, 162, false, true, "", 135);
					data.Rows.Add(3, 302, true, true, "", 300);
					data.Rows.Add(4, 608, true, false, "Right", 0);
					data.Rows.Add(6, 612, false, false, "", 0);
				}
				else if (ValueDetails == "Payment Report")
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(90, 90);
					LineData.Rows.Add(160, 160);
					LineData.Rows.Add(360, 360);
					LineData.Rows.Add(470, 470);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("Vchr Date", 10);
					Header.Rows.Add("Vchr No", 92);
					Header.Rows.Add("Particulars", 161);
					Header.Rows.Add("Amount", 362);
					Header.Rows.Add("Remarks", 472);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(0, 10, false, false, "", 0);
					data.Rows.Add(1, 92, false, false, "", 0);
					data.Rows.Add(3, 162, true, true, "", 300);
					data.Rows.Add(4, 468, true, false, "Right", 0);
					data.Rows.Add(5, 472, false, false, "", 0);
				}
				else
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(40, 40);
					LineData.Rows.Add(350, 350);
					LineData.Rows.Add(600, 600);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("SL", 10);
					Header.Rows.Add("Account Name", 42);
					Header.Rows.Add("Account Group", 352);
					Header.Rows.Add("Balance", 602);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(0, 10, false, false, "", 0);
					data.Rows.Add(1, 42, true, true, "", 305);
					data.Rows.Add(3, 401, false, true, "", 245);
					data.Rows.Add(2, 775, true, false, "Right", 0);
				}
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

		private void PaymentReceiptReportView_Load(object sender, EventArgs e)
		{
			lblTitle.Text = ValueDetails;
			AniHelper.FillAGroup(cmbAccGroup);
			AniHelper.FillCashAndBankAccount(cmbAccount);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillEmployees(cmbSM);
			AniHelper.FillPartyAndSupplier(cmbAccWise);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			if (ValueDetails.Contains("Receipt Report"))
			{
				Index = 5;
				strVoucherType = "Receipt Voucher";
			}
			else
			{
				Index = 6;
				strVoucherType = "Payment Voucher";
			}
			AniHelper.FillBranch(cmbBranch);
			AniHelper.ClearComboMethod(this);
			AshComboBox ashComboBox = cmbAccGroup;
			AshComboBox ashComboBox2 = cmbAccount;
			AshComboBox ashComboBox3 = cmbRoute;
			AshComboBox ashComboBox4 = cmbSM;
			AshComboBox ashComboBox5 = cmbAccWise;
			string text = (cmbBranch.Text = string.Empty);
			string text3 = (ashComboBox5.Text = text);
			string text5 = (ashComboBox4.Text = text3);
			string text7 = (ashComboBox3.Text = text5);
			string text10 = (ashComboBox.Text = (ashComboBox2.Text = text7));
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now));
			AshComboBox ashComboBox = cmbAccGroup;
			AshComboBox ashComboBox2 = cmbAccount;
			AshComboBox ashComboBox3 = cmbRoute;
			AshComboBox ashComboBox4 = cmbSM;
			AshComboBox ashComboBox5 = cmbAccWise;
			string text = (cmbBranch.Text = string.Empty);
			string text3 = (ashComboBox5.Text = text);
			string text5 = (ashComboBox4.Text = text3);
			string text7 = (ashComboBox3.Text = text5);
			string text10 = (ashComboBox.Text = (ashComboBox2.Text = text7));
			cmbAccGroup.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				sTitle = ValueDetails + " from " + dtmFrom.Text + " to " + dtmTo.Text;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentReceiptReportView));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rdbAll = new System.Windows.Forms.RadioButton();
            this.rdbBank = new System.Windows.Forms.RadioButton();
            this.rdbCash = new System.Windows.Forms.RadioButton();
            this.cmbSM = new AshUsercontrol.AshComboBox();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAccGroup = new AshUsercontrol.AshComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAccount = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbEntryDate = new System.Windows.Forms.RadioButton();
            this.rdbVoucherDate = new System.Windows.Forms.RadioButton();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbAccWise = new AshUsercontrol.AshComboBox();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbSummary = new System.Windows.Forms.RadioButton();
            this.rdbDetails = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
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
            // rdbAll
            // 
            resources.ApplyResources(this.rdbAll, "rdbAll");
            this.rdbAll.Checked = true;
            this.rdbAll.Name = "rdbAll";
            this.rdbAll.TabStop = true;
            this.rdbAll.UseVisualStyleBackColor = true;
            // 
            // rdbBank
            // 
            resources.ApplyResources(this.rdbBank, "rdbBank");
            this.rdbBank.Name = "rdbBank";
            this.rdbBank.UseVisualStyleBackColor = true;
            // 
            // rdbCash
            // 
            resources.ApplyResources(this.rdbCash, "rdbCash");
            this.rdbCash.Name = "rdbCash";
            this.rdbCash.UseVisualStyleBackColor = true;
            // 
            // cmbSM
            // 
            resources.ApplyResources(this.cmbSM, "cmbSM");
            this.cmbSM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSM.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSM.FormattingEnabled = true;
            this.cmbSM.LeaveColor = System.Drawing.Color.White;
            this.cmbSM.Name = "cmbSM";
            // 
            // cmbRoute
            // 
            resources.ApplyResources(this.cmbRoute, "cmbRoute");
            this.cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Name = "cmbRoute";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbAccGroup
            // 
            resources.ApplyResources(this.cmbAccGroup, "cmbAccGroup");
            this.cmbAccGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccGroup.FormattingEnabled = true;
            this.cmbAccGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbAccGroup.Name = "cmbAccGroup";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbAccount
            // 
            resources.ApplyResources(this.cmbAccount, "cmbAccount");
            this.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbAccount.Name = "cmbAccount";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // rdbEntryDate
            // 
            resources.ApplyResources(this.rdbEntryDate, "rdbEntryDate");
            this.rdbEntryDate.Name = "rdbEntryDate";
            this.rdbEntryDate.UseVisualStyleBackColor = true;
            // 
            // rdbVoucherDate
            // 
            resources.ApplyResources(this.rdbVoucherDate, "rdbVoucherDate");
            this.rdbVoucherDate.Checked = true;
            this.rdbVoucherDate.Name = "rdbVoucherDate";
            this.rdbVoucherDate.TabStop = true;
            this.rdbVoucherDate.UseVisualStyleBackColor = true;
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cmbAccWise);
            this.panel3.Controls.Add(this.cmbBranch);
            this.panel3.Controls.Add(this.rdbEntryDate);
            this.panel3.Controls.Add(this.rdbVoucherDate);
            this.panel3.Controls.Add(this.dtmTo);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblBranch);
            this.panel3.Controls.Add(this.dtmFrom);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Name = "panel3";
            // 
            // cmbAccWise
            // 
            resources.ApplyResources(this.cmbAccWise, "cmbAccWise");
            this.cmbAccWise.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccWise.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccWise.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccWise.FormattingEnabled = true;
            this.cmbAccWise.LeaveColor = System.Drawing.Color.White;
            this.cmbAccWise.Name = "cmbAccWise";
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
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rdbAll);
            this.panel1.Controls.Add(this.cmbSM);
            this.panel1.Controls.Add(this.rdbBank);
            this.panel1.Controls.Add(this.rdbCash);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbRoute);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbAccGroup);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbAccount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
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
            // PaymentReceiptReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Name = "PaymentReceiptReportView";
            this.Load += new System.EventHandler(this.PaymentReceiptReportView_Load);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.rdbDetails, 0);
            this.Controls.SetChildIndex(this.rdbSummary, 0);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
