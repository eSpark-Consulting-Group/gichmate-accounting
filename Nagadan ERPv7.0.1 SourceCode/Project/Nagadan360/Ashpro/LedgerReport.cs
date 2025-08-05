using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class LedgerReport : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string strAccountId;

		private string strAccountName;

		private decimal dmlDebit;

		private decimal dmlCredit;

		private decimal dmlBalace;

		private decimal dmlDebitTotal;

		private decimal dmlCreditTotal;

		private decimal dmlODebit;

		private decimal dmlOCredit;

		private string strQuery;

		private string strNetTotal;

		private string sTitle;

		private ReportView frm;

		private DataGridViewRow row;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private AshComboBox cmbLedgerAcct;

		private Label lblLedger;

		private Button btnShow;

		private Button btnClear;

		private AshDateTimePicker dtmTo;

		private Label label2;

		private AshDateTimePicker dtmFrom;

		private Label label1;

		private ToolTip toolTip1;

		private Label label6;

		private AshComboBox cmbAccGrp;

		private CheckBox cbShowOppBalance;

		private Label label3;

		private Label lblTitle;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public string ValueFromDate { get; set; }

		public string ValueToDate { get; set; }

		public string ValueLedger { get; set; }

		public DashboardPOS form { get; set; }

		public LedgerReport()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string[] headers = new string[10] { "Date", "Inv No", "Vchr Type", "Ref No", "Particulars", "Debit", "Credit", "Balance", "Remarks", "Transaction Date" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(350, "L");
			Gridview.Rows.Add(120, "L");
			dmlBalace = default(decimal);
			dmlDebitTotal = default(decimal);
			dmlCreditTotal = default(decimal);
			strNetTotal = "0.00";
			if (cbShowOppBalance.Checked)
			{
				OpeningBalanceFindMethod();
				dmlDebitTotal = dmlODebit;
				dmlCreditTotal = dmlOCredit;
			}
			try
			{
				strQuery = "SELECT VchrDate,VchrNo,VchrType,RefNo,OppAccountId,DebitAmount,CreditAmount,Narration,TranDate FROM TransactionLog  where TransactionLog.AccountId = '" + cmbLedgerAcct.SelectedValue.ToInt32() + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' order by VchrDate";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					strAccountId = row["OppAccountId"].ToString();
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", strAccountId, out strAccountName);
					dmlDebit = Convert.ToDecimal(row["DebitAmount"].ToString());
					dmlCredit = Convert.ToDecimal(row["CreditAmount"].ToString());
					dmlBalace = dmlBalace + dmlDebit - dmlCredit;
					dmlDebitTotal += dmlDebit;
					dmlCreditTotal += dmlCredit;
					if (dmlBalace < 0m)
					{
						strNetTotal = Math.Abs(dmlBalace).ToString(CommonSettings.strCurencyFormat) + " Cr";
					}
					else
					{
						strNetTotal = dmlBalace.ToString(CommonSettings.strCurencyFormat) + " Dr";
					}
					DateTime dateTime = Convert.ToDateTime(row[0].ToString());
					DateTime dateTime2 = Convert.ToDateTime(row[8].ToString());
					Details.Rows.Add(dateTime.ToString("dd-MMM-yyyy"), row[1], row[2], row[3], strAccountName, dmlDebit.ToString(CommonSettings.strCurencyFormat), dmlCredit.ToString(CommonSettings.strCurencyFormat), strNetTotal, row[7], dateTime2.ToString("dd-MMM-yyyy"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 120");
			}
			Details.Rows.Add("", "", "", "", "", "", "", "", "", "");
			Details.Rows.Add("", "", "", "", "", dmlDebitTotal.ToString(CommonSettings.strCurencyFormat), dmlCreditTotal.ToString(CommonSettings.strCurencyFormat), "", "", "");
			Details.Rows.Add("", "", "", "", "", "", "", "", "", "");
			Details.Rows.Add("", "", "", "", "", "Closing Balance", strNetTotal, "", "", "");
		}

		private void OpeningBalanceFindMethod()
		{
			dmlOCredit = default(decimal);
			dmlODebit = default(decimal);
			dmlCredit = default(decimal);
			dmlDebit = default(decimal);
			dmlBalace = default(decimal);
			dmlBalace = AshproCommon.GetDecimalDataWithParameter_SP("spBalanceSheetBalance", new
			{
				AccountType = "D",
				AccountId = cmbLedgerAcct.SelectedValue.ToInt32(),
				BranchId = Settings.Default.iBranchId,
				Date = AniHelper.GetDate(dtmFrom.Value)
			});
			if (dmlBalace < 0m)
			{
				dmlOCredit = dmlBalace;
				strNetTotal = Math.Abs(dmlBalace).ToString("F") + " Cr";
				dmlCredit = Convert.ToDecimal(Math.Abs(dmlBalace));
				dmlDebit = default(decimal);
			}
			else
			{
				dmlODebit = dmlBalace;
				strNetTotal = dmlBalace.ToString("F") + " Dr";
				dmlDebit = Convert.ToDecimal(Math.Abs(dmlBalace));
				dmlCredit = default(decimal);
			}
			Details.Rows.Add(dtmFrom.Text, "", "", "", "Opening Balance", dmlDebit, dmlCredit, strNetTotal, "", "");
		}

		private void LoadReportOpeningMethod()
		{
			try
			{
				AniHelper.FillBranch(cmbBranch);
				if (ValueLedger != null)
				{
					if (ValueLedger == "CASH IN HAND")
					{
						AniHelper.FillCashAccount(cmbLedgerAcct);
						sTitle = "Cash Book from " + dtmFrom.Text + " to " + dtmTo.Text;
						lblTitle.Text = "Cash Book";
					}
					else if (ValueLedger == "CASH AT BANK")
					{
						AniHelper.FillBankAccount(cmbLedgerAcct);
						sTitle = "Bank Book from " + dtmFrom.Text + " to " + dtmTo.Text;
						lblTitle.Text = "Bank Book";
					}
					else
					{
						DBContext dBContext = new DBContext();
						AniHelper.FillPartyAndSupplier(cmbLedgerAcct);
						cmbLedgerAcct.SelectedValue = dBContext.Accounts(new
						{
							AccountName = ValueLedger
						}).FirstOrDefault().AccountId;
						dtmFrom.Text = ValueFromDate;
						dtmFrom.Visible = false;
						label1.Visible = false;
						dtmTo.Text = ValueToDate;
						label2.Text = "As on Date:";
						sTitle = "Ledger Report of " + cmbLedgerAcct.Text + " as on Date " + dtmTo.Text;
					}
					label6.Visible = false;
					cmbAccGrp.Visible = false;
					cbShowOppBalance.Visible = false;
				}
				else
				{
					sTitle = "Ledger Report of " + cmbLedgerAcct.Text + " From " + dtmFrom.Text + " To " + dtmTo.Text;
					dtmFrom.Value = AniHelper.GetFYearFromDate();
					AniHelper.FillAGroup(cmbAccGrp);
					cmbAccGrp.Text = "CUSTOMER/SUNDRY DEBTORS";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearMethod()
		{
			AshComboBox ashComboBox = cmbAccGrp;
			AshComboBox ashComboBox2 = cmbLedgerAcct;
			string text = (cmbBranch.Text = string.Empty);
			string text4 = (ashComboBox.Text = (ashComboBox2.Text = text));
			dtmFrom.Value = DateTime.Now;
			dtmFrom.Visible = true;
			label1.Visible = true;
			btnShow.Enabled = true;
			label6.Visible = true;
			cmbAccGrp.Visible = true;
			cbShowOppBalance.Visible = true;
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(110, 110);
				LineData.Rows.Add(180, 180);
				LineData.Rows.Add(420, 420);
				LineData.Rows.Add(540, 540);
				LineData.Rows.Add(660, 660);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Date", 8);
				Header.Rows.Add("Inv No", 115);
				Header.Rows.Add("Particulars", 185);
				Header.Rows.Add("Debit", 485);
				Header.Rows.Add("Credit", 585);
				Header.Rows.Add("Balance", 685);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, true, false, "");
				data.Rows.Add(1, 145, true, false, "Center");
				data.Rows.Add(4, 185, true, true, "", 350);
				data.Rows.Add(5, 540, true, false, "Right");
				data.Rows.Add(6, 610, true, false, "Right");
				data.Rows.Add(7, 665, true, false, "Right");
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
			LineDataPDF.Rows.Add(485, 50, 485, 800);
			LineDataPDF.Rows.Add(415, 50, 415, 800);
			LineDataPDF.Rows.Add(342, 50, 342, 800);
			LineDataPDF.Rows.Add(145, 50, 145, 800);
			LineDataPDF.Rows.Add(85, 50, 85, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Date", 10);
			HeaderPDF.Rows.Add("Inv. No", 90);
			HeaderPDF.Rows.Add("Particulars", 150);
			HeaderPDF.Rows.Add("Debit", 350);
			HeaderPDF.Rows.Add("Credit", 420);
			HeaderPDF.Rows.Add("Balance", 490);
			dataPDF = AniHelper.GetDetailsDatatable();
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
				if (ValueLedger == null)
				{
					sTitle = "Ledger Report of " + cmbLedgerAcct.Text + " From " + dtmFrom.Text + " To " + dtmTo.Text;
				}
				ReportGenerateMethod();
				PrintMethod();
				MailMethod();
				frm = new ReportView();
				frm = new ReportView();
				frm.MdiParent = base.MdiParent;
				frm.Details = Details;
				frm.Gridview = Gridview;
				frm.Title = sTitle;
				frm.data = data;
				frm.dataPDF = dataPDF;
				frm.LineData = LineData;
				frm.LineDataPDF = LineDataPDF;
				frm.Header = Header;
				frm.HeaderPDF = HeaderPDF;
				frm.dgvReport.CellContentClick += DgvReport_CellContentClick;
				if (form != null)
				{
					form.OpenForm(frm, 0, 0);
				}
				else
				{
					frm.Show();
				}
			}
		}

		private void DgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				row = frm.dgvReport.SelectedRows[0];
				string text = row.Cells[2].Value.ToString();
				int num = row.Cells[1].Value.ToInt32();
				switch (text)
				{
				case "Sales Voucher":
				{
					SalesEstimateView salesEstimateView = new SalesEstimateView();
					salesEstimateView.iInvNo = num;
					salesEstimateView.ShowDialog();
					break;
				}
				case "Receipt Voucher":
				{
					ReceiptView receiptView = new ReceiptView("Cash Receipt");
					receiptView.iVoucherNo = num;
					receiptView.ShowDialog();
					break;
				}
				case "Payment Voucher":
				{
					PaymentView paymentView = new PaymentView("Cash Payment");
					paymentView.iVoucherNo = num;
					paymentView.ShowDialog();
					break;
				}
				case "Journal Voucher":
				{
					JournalEntryView journalEntryView = new JournalEntryView();
					journalEntryView.iVoucherNo = num;
					journalEntryView.ShowDialog();
					break;
				}
				case "Sales Return Voucher":
				{
					SalesReturnView salesReturnView = new SalesReturnView();
					salesReturnView.iInvNo = num;
					salesReturnView.ShowDialog();
					break;
				}
				case "Credit Note":
				{
					CreditNoteView creditNoteView = new CreditNoteView();
					creditNoteView.iVoucherNo = num;
					creditNoteView.ShowDialog();
					break;
				}
				case "Debit Note":
				{
					DebitNoteView debitNoteView = new DebitNoteView();
					debitNoteView.iVoucherNo = num;
					debitNoteView.ShowDialog();
					break;
				}
				case "Purchase Voucher":
				{
					PurchaseInvoiceView purchaseInvoiceView = new PurchaseInvoiceView();
					purchaseInvoiceView.iPurInvoiceNo = num;
					purchaseInvoiceView.ShowDialog();
					break;
				}
				case "Purchase Return Voucher":
				{
					PurchaseReturnView purchaseReturnView = new PurchaseReturnView();
					purchaseReturnView.iPurInvoiceNo = num;
					purchaseReturnView.ShowDialog();
					break;
				}
				case "Opening Voucher":
				{
					OpeningBalance openingBalance = new OpeningBalance();
					openingBalance.PassingValueOne = num.ToString();
					openingBalance.ShowDialog();
					break;
				}
				default:
				{
					SalesInvoiceView salesInvoiceView = new SalesInvoiceView();
					salesInvoiceView.iInvoiceNo = num;
					salesInvoiceView.sVoucherType = text;
					salesInvoiceView.ShowDialog();
					break;
				}
				}
			}
		}

		private void LedgerReport_Load(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			LoadReportOpeningMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void LedgerReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnShow_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				ClearMethod();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.F && e.Alt)
			{
				e.SuppressKeyPress = true;
				dtmFrom.Focus();
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				dtmTo.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void cmbAccGrp_SelectedIndexChanged(object sender, EventArgs e)
		{
			AshproCommon.FillCombo(cmbLedgerAcct, "Select AccountId as Id,AccountName as Name From Account  where AGroupId='" + cmbAccGrp.SelectedValue.ToInt32() + "'");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LedgerReport));
            this.cmbLedgerAcct = new AshUsercontrol.AshComboBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.cmbAccGrp = new AshUsercontrol.AshComboBox();
            this.cbShowOppBalance = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
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
            // cmbLedgerAcct
            // 
            resources.ApplyResources(this.cmbLedgerAcct, "cmbLedgerAcct");
            this.cmbLedgerAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLedgerAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLedgerAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbLedgerAcct.FormattingEnabled = true;
            this.cmbLedgerAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbLedgerAcct.Name = "cmbLedgerAcct";
            this.toolTip1.SetToolTip(this.cmbLedgerAcct, resources.GetString("cmbLedgerAcct.ToolTip"));
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbAccGrp
            // 
            resources.ApplyResources(this.cmbAccGrp, "cmbAccGrp");
            this.cmbAccGrp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAccGrp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAccGrp.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAccGrp.FormattingEnabled = true;
            this.cmbAccGrp.Items.AddRange(new object[] {
            resources.GetString("cmbAccGrp.Items"),
            resources.GetString("cmbAccGrp.Items1"),
            resources.GetString("cmbAccGrp.Items2"),
            resources.GetString("cmbAccGrp.Items3"),
            resources.GetString("cmbAccGrp.Items4"),
            resources.GetString("cmbAccGrp.Items5"),
            resources.GetString("cmbAccGrp.Items6"),
            resources.GetString("cmbAccGrp.Items7")});
            this.cmbAccGrp.LeaveColor = System.Drawing.Color.White;
            this.cmbAccGrp.Name = "cmbAccGrp";
            this.toolTip1.SetToolTip(this.cmbAccGrp, resources.GetString("cmbAccGrp.ToolTip"));
            this.cmbAccGrp.SelectedIndexChanged += new System.EventHandler(this.cmbAccGrp_SelectedIndexChanged);
            // 
            // cbShowOppBalance
            // 
            resources.ApplyResources(this.cbShowOppBalance, "cbShowOppBalance");
            this.cbShowOppBalance.Checked = true;
            this.cbShowOppBalance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowOppBalance.Name = "cbShowOppBalance";
            this.toolTip1.SetToolTip(this.cbShowOppBalance, resources.GetString("cbShowOppBalance.ToolTip"));
            this.cbShowOppBalance.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            // LedgerReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbShowOppBalance);
            this.Controls.Add(this.cmbAccGrp);
            this.Controls.Add(this.cmbLedgerAcct);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblLedger);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.Name = "LedgerReport";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.LedgerReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LedgerReport_KeyDown);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblLedger, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbLedgerAcct, 0);
            this.Controls.SetChildIndex(this.cmbAccGrp, 0);
            this.Controls.SetChildIndex(this.cbShowOppBalance, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
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
