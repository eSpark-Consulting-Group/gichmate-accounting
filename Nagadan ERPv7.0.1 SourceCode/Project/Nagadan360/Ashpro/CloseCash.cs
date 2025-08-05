using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class CloseCash : BaseForm
	{
		private DBContext db = new DBContext();

		private string VoucherType = "Journal Voucher";

		private Dictionary<string, decimal> salesPayments;

		private List<SalesMaster> entSalesMasters = new List<SalesMaster>();

		private decimal dmlCashPaymentTotal;

		private decimal dmlCardPaymentTotal;

		private decimal dmlFreeTotal;

		private decimal dmlReturnTotal;

		private decimal dmlCreditTotal;

		private decimal dmlOtherPaymentTotal;

		private decimal dmlOpeningCash;

		private decimal dmlClosingCash;

		private decimal dmlCashReceived;

		private decimal dmlCashPaid;

		private int iManinCashAccountId;

		private decimal dmlTotalSale;

		private int? iBranchId = null;

		private IContainer components = null;

		public Label lblHead;

		private Label label1;

		private AshTBox txtSequenseNo;

		private Label label2;

		private Label label3;

		private Panel panel1;

		private Panel panel2;

		private AshDataGridview dgPayments;

		private Panel panel3;

		private AshDataGridview dgTaxes;

		private Panel panel4;

		private Button btnPrintReport;

		private Button btnCloseCash;

		private Button btnPrint;

		private AshTBox txtTransactions;

		private Label label4;

		private AshTBox txtSales;

		private Label label5;

		private AshTBox txtNet;

		private Label label6;

		private AshTBox txtTaxes;

		private AshTBox txtTotal;

		private AshTBox txtMoney;

		private Label label7;

		private Label label8;

		private Label label9;

		private Button btnSearch;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Button btnClear;
        private DataGridViewTextBoxColumn col_Payment;
        private DataGridViewTextBoxColumn col_Money;
        private DataGridViewTextBoxColumn col_TaxCategory;
        private DataGridViewTextBoxColumn col_Taxes;
        private DataGridViewTextBoxColumn col_Net;
        private Button btnProWiseReport;

		public DashboardPOS DashboardPOS { get; set; }

		public CloseCash()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			txtMoney.Format = CommonSettings.strCurencyFormat;
			txtNet.Format = CommonSettings.strCurencyFormat;
			txtTaxes.Format = CommonSettings.strCurencyFormat;
			txtTotal.Format = CommonSettings.strCurencyFormat;
			col_Money.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxes.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Net.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void PrintCloseCash(bool isCloseCash = true)
		{
			try
			{
				PrinterSettings printerSettings = new PrinterSettings();
				DataSet dataSet = new DataSet();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.ProCategoryForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), txtSequenseNo.Text, Utility.uTerminal, iBranchId);
				dataTable.TableName = "dtGroupSale";
				dataSet.Tables.Add(dataTable);
				DataTable dataTable2 = new DataTable();
				dataTable2 = ReportHelperForPOS.SalesReturnItemsForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
				dataTable2.TableName = "dtSalesReturnItems";
				dataSet.Tables.Add(dataTable2);
				DataTable dataTable3 = new DataTable();
				dataTable3 = ReportHelperForPOS.SuppliersPurchaseForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
				dataTable3.TableName = "dtSupplierPurchase";
				dataSet.Tables.Add(dataTable3);
				DataTable dataTable4 = new DataTable();
				dataTable4 = ReportHelperForPOS.CashReceiptForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
				dataTable4.TableName = "dtCashReceipt";
				dataSet.Tables.Add(dataTable4);
				DataTable dataTable5 = new DataTable();
				dataTable5 = ReportHelperForPOS.CashPaymentForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
				dataTable5.TableName = "dtCashPayment";
				dataSet.Tables.Add(dataTable5);
				DataTable dataTable6 = new DataTable();
				dataTable6 = ReportHelperForPOS.GetTaxForCloseCash(txtSequenseNo.Data, Utility.uTerminal, iBranchId);
				dataTable6.TableName = "dtTaxSales";
				dataSet.Tables.Add(dataTable6);
				string sReportPath = Application.StartupPath + "\\ReportForms\\rptCloseCashNew.rdlc";
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				List<atReportParameter> list = new List<atReportParameter>();
				list.Add(new atReportParameter("ShopName", company.InstituteName));
				list.Add(new atReportParameter("ShopAddress", company.Address));
				list.Add(new atReportParameter("ShopTaxNumber", company.GSTNo));
				list.Add(new atReportParameter("MachineName", Utility.uTerminal));
				list.Add(new atReportParameter("SequenceNo", Settings.Default.CounterName + txtSequenseNo.Data));
				list.Add(new atReportParameter("StartDate", dtmFrom.Text));
				list.Add(new atReportParameter("EndDate", dtmTo.Text));
				list.Add(new atReportParameter("SalesMan", db.Users().FirstOrDefault((User x) => x.UserId == Settings.Default.intLogOnId).UserName));
				list.Add(new atReportParameter("CashTotal", dmlCashPaymentTotal.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("CardTotal", dmlCardPaymentTotal.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("OtherPayment", dmlOtherPaymentTotal.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("CreditTotal", dmlCreditTotal.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("ReturnTotal", dmlReturnTotal.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("OpeningCash", dmlOpeningCash.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("ClosingCash", dmlClosingCash.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("CashReceived", dmlCashReceived.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("CashPaid", dmlCashPaid.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
				list.Add(new atReportParameter("CompanyNameInArabic", company.LocalName));
				list.Add(new atReportParameter("TotalSales", dmlTotalSale.ToString(CommonSettings.strCurencyFormat)));
				printerSettings.PrinterName = printerSettings.PrinterName;
				DirectReportPrint directReportPrint = new DirectReportPrint();
				directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list);
				if (isCloseCash)
				{
					string text = Settings.Default.CloseCashPDF;
					if (text == string.Empty)
					{
						text = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
						text = Path.Combine(text, Utility.uManufacturer);
						text = Path.Combine(text, "Close Cash");
					}
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					string path = "Close_Cash_" + Utility.uSerialNumber + "_" + DateTime.Now.Date.ToString(CommonSettings.strDateFormat) + ".pdf";
					path = Path.Combine(text, path);
					directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list, generatePdf: true, 1, path, isStart: false);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo)
		{
			vchrTranLog.VchrDate = DateTime.Now.Date;
			vchrTranLog.VchrTranId = null;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = txtMoney.Value;
			vchrTranLog.VchrType = VoucherType;
			vchrTranLog.VchrName = VoucherType;
			vchrTranLog.SalesManId = Settings.Default.intLogOnId;
			vchrTranLog.FromAccountId = Utility.uCounterCashAccountId;
			vchrTranLog.ToAccountId = iManinCashAccountId;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = "OPening Cash Entry";
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
		}

		private void CashSettilementMethod()
		{
			iManinCashAccountId = db.Accounts(new
			{
				AccountId = 1
			}).FirstOrDefault().AccountId.ToInt32();
			VchrTranLog vchrTranLog = new VchrTranLog();
			int iVchrNo = (db.VchrTransaction(new
			{
				VchrType = VoucherType,
				FK_BranchId = Settings.Default.iBranchId
			}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			EntityLoadMethod(vchrTranLog, iVchrNo);
			if (AshproCommon.InsertToDatabaseObj(vchrTranLog))
			{
				int num = (from x in db.VchrTransaction()
					where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
					select x).FirstOrDefault().VchrTranId.ToInt32();
				POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), num.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(DateTime.Now));
				POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), num.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(DateTime.Now));
			}
			Settings.Default.IsCashClosed = true;
			Settings.Default.Save();
			if (DashboardPOS != null)
			{
				POSLogin pOSLogin = new POSLogin();
				pOSLogin.Show();
				DashboardPOS.Hide();
			}
		}

		private void SearchMethod()
		{
			try
			{
				List<TransactionLog> list = new List<TransactionLog>();
				list = db.TransactionLogs(new
				{
					SequenceNo = txtSequenseNo.Data,
					Terminal = Utility.uTerminal
				}).ToList();
				dgTaxes.DataSource = null;
				MasterValue masterValue = db.MasterValues(new
				{
					MasterType = "CounterOppeningCash",
					Description = Utility.uTerminal
				}).FirstOrDefault();
				entSalesMasters = db.SalesMasters(new
				{
					SequenceNo = txtSequenseNo.Data,
					Terminal = Utility.uTerminal
				}).ToList();
				if (list.Count > 0)
				{
					dtmFrom.Value = list.Min((TransactionLog x) => x.VchrDate);
					dtmTo.Value = list.Max((TransactionLog x) => x.VchrDate);
					int num2 = (txtTransactions.Data = (txtSales.Data = entSalesMasters.Count));
					txtTaxes.Value = entSalesMasters.Sum((SalesMaster x) => x.TotalTaxAmount);
					txtNet.Value = entSalesMasters.Sum((SalesMaster x) => x.TaxableAmount);
					txtTotal.Value = entSalesMasters.Sum((SalesMaster x) => x.GrandDiscount);
					dmlCashPaymentTotal = entSalesMasters.Where((SalesMaster x) => x.CashPayment > 0m).Sum((SalesMaster x) => x.CashPayment);
					dmlCardPaymentTotal = entSalesMasters.Where((SalesMaster x) => x.CardPayment > 0m).Sum((SalesMaster x) => x.CardPayment);
					dmlOtherPaymentTotal = entSalesMasters.Where((SalesMaster x) => x.OtherPayment > 0m).Sum((SalesMaster x) => x.OtherPayment);
					dmlFreeTotal = entSalesMasters.Sum((SalesMaster x) => x.FreeAmount);
					dmlReturnTotal = entSalesMasters.Sum((SalesMaster x) => x.ReturnAmount);
					dmlCreditTotal = entSalesMasters.Sum((SalesMaster x) => x.Credit);
					dmlTotalSale = dmlCardPaymentTotal + dmlCashPaymentTotal + dmlOtherPaymentTotal + dmlCreditTotal - Math.Abs(dmlReturnTotal);
					if (Settings.Default.IsSalesCounter && masterValue != null)
					{
						dmlOpeningCash = masterValue.Name.ToDecimal();
					}
					else
					{
						dmlOpeningCash = ReportHelperForPOS.OppeningCashForCloseCash(AniHelper.GetDate(dtmFrom.Value.AddSeconds(-1.0)), iBranchId);
					}
					dmlClosingCash = ReportHelperForPOS.ClosingCashForCloseCash(AniHelper.GetDate(dtmFrom.Value.AddSeconds(-1.0)), AniHelper.GetDate(dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0)), iBranchId);
					dmlCashReceived = ReportHelperForPOS.CashReceiptTotalForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					dmlCashPaid = ReportHelperForPOS.CashPaymentTotalForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0)), iBranchId);
					txtMoney.Value = dmlClosingCash;
					salesPayments = new Dictionary<string, decimal>();
					if (dmlCashPaymentTotal > 0m)
					{
						salesPayments.Add("Cash Sale", dmlCashPaymentTotal);
					}
					if (dmlCardPaymentTotal > 0m)
					{
						salesPayments.Add("Card Sale", dmlCardPaymentTotal);
					}
					if (dmlOtherPaymentTotal > 0m)
					{
						salesPayments.Add("Online Sale", dmlOtherPaymentTotal);
					}
					if (dmlFreeTotal > 0m)
					{
						salesPayments.Add("Free Item Sale", dmlFreeTotal);
					}
					if (dmlReturnTotal < 0m)
					{
						salesPayments.Add("Return Sale", dmlReturnTotal);
					}
					if (dmlCreditTotal > 0m)
					{
						salesPayments.Add("Credit Sales", dmlCreditTotal);
					}
					dgPayments.DataSource = salesPayments.ToList();
					DataTable taxForCloseCash = ReportHelperForPOS.GetTaxForCloseCash(txtSequenseNo.Data, Utility.uTerminal, iBranchId);
					dgTaxes.DataSource = taxForCloseCash;
				}
				else
				{
					dtmFrom.Value = DateTime.Now;
					dtmTo.Value = DateTime.Now;
					AshTBox ashTBox = txtTransactions;
					AshTBox ashTBox2 = txtSales;
					int num2 = 0;
					ashTBox2.Data = 0;
					ashTBox.Data = 0;
					txtTaxes.Value = 0m;
					txtNet.Value = 0m;
					decimal num5 = (txtTotal.Value = (txtMoney.Value = default(decimal)));
					dgPayments.DataSource = null;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void CloseCash_Load(object sender, EventArgs e)
		{
			txtSequenseNo.Data = Utility.uSerialNumber;
			SearchMethod();
			if (CommonSettings.blnEnableMultiBranch)
			{
				iBranchId = Settings.Default.iBranchId;
			}
		}

		private void btnProWiseReport_Click(object sender, EventArgs e)
		{
			PrinterSettings printerSettings = new PrinterSettings();
			DataTable dataTable = new DataTable();
			dataTable = ReportHelperForPOS.ProductSaleCloseCash(txtSequenseNo.Data, iBranchId);
			DataSet dataSet = new DataSet();
			dataTable.TableName = "dtSaleByProductPOS";
			dataSet.Tables.Add(dataTable);
			string sReportPath = Application.StartupPath + "\\MiniReportForm\\rptProductSaleCloseCash.rdlc";
			List<atReportParameter> list = new List<atReportParameter>();
			list.Add(new atReportParameter("FromDate", dtmFrom.Text));
			list.Add(new atReportParameter("ToDate", dtmTo.Text));
			list.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
			list.Add(new atReportParameter("DateFormat", CommonSettings.strDateFormat));
			printerSettings.PrinterName = printerSettings.PrinterName;
			DirectReportPrint directReportPrint = new DirectReportPrint();
			directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list);
		}

		private void btnCloseCash_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgPayments.Rows.Count == 0)
				{
					Messages.WarningMessage("There is no sales in this serial!");
					return;
				}
				AshproCommon.DatabaseMethod("Update SalesEstimateMaster set IsClosed='True' Where SequenceNo='" + txtSequenseNo.Data + "' and FK_BranchId='" + Settings.Default.iBranchId + "' and Terminal='" + Utility.uTerminal + "'");
				PrintCloseCash();
				CashSettilementMethod();
				txtSequenseNo.Data++;
				Utility.uSerialNumber = txtSequenseNo.Data;
				AniHelper.SerialNumberMethod(txtSequenseNo.Data);
				Settings.Default.iTockenNo = 1;
				Settings.Default.Save();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			SearchMethod();
		}

		private void btnPrintReport_Click(object sender, EventArgs e)
		{
			try
			{
				PrinterSettings printerSettings = new PrinterSettings();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.ProCategoryForCloseCash(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), txtSequenseNo.Text, Utility.uTerminal, iBranchId);
				DataSet dataSet = new DataSet();
				dataTable.TableName = "dtProCategoryForCloseCash";
				dataSet.Tables.Add(dataTable);
				decimal num = default(decimal);
				foreach (DataRow row in dataTable.Rows)
				{
					num += row[4].ToString().ToDecimal();
				}
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				string sReportPath = Application.StartupPath + "\\ReportForms\\rptProGroupForCashClose.rdlc";
				List<atReportParameter> list = new List<atReportParameter>();
				list.Add(new atReportParameter("MachineName", Utility.uTerminal));
				list.Add(new atReportParameter("StartDate", dtmFrom.Text));
				list.Add(new atReportParameter("EndDate", dtmTo.Text));
				list.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
				list.Add(new atReportParameter("TotalAmount", num.ToString(CommonSettings.strCurencyFormat)));
				list.Add(new atReportParameter("ShopName", company.InstituteName));
				list.Add(new atReportParameter("ShopAddress", company.Address));
				list.Add(new atReportParameter("ShopTaxNumber", company.GSTNo));
				list.Add(new atReportParameter("CompanyNameInArabic", company.LocalName));
				printerSettings.PrinterName = printerSettings.PrinterName;
				DirectReportPrint directReportPrint = new DirectReportPrint();
				directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintCloseCash(isCloseCash: false);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			CloseCash_Load(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseCash));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHead = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.txtSequenseNo = new AshUsercontrol.AshTBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgPayments = new AshUsercontrol.AshDataGridview();
            this.col_Payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgTaxes = new AshUsercontrol.AshDataGridview();
            this.col_TaxCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Taxes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Net = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnProWiseReport = new System.Windows.Forms.Button();
            this.btnCloseCash = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMoney = new AshUsercontrol.AshTBox();
            this.txtTotal = new AshUsercontrol.AshTBox();
            this.txtNet = new AshUsercontrol.AshTBox();
            this.txtTaxes = new AshUsercontrol.AshTBox();
            this.txtSales = new AshUsercontrol.AshTBox();
            this.txtTransactions = new AshUsercontrol.AshTBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTaxes)).BeginInit();
            this.panel4.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.dtmTo);
            this.panel1.Controls.Add(this.dtmFrom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnPrintReport);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSequenseNo);
            this.panel1.Name = "panel1";
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
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrintReport
            // 
            resources.ApplyResources(this.btnPrintReport, "btnPrintReport");
            this.btnPrintReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrintReport.ForeColor = System.Drawing.Color.White;
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.UseVisualStyleBackColor = false;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // txtSequenseNo
            // 
            resources.ApplyResources(this.txtSequenseNo, "txtSequenseNo");
            this.txtSequenseNo.Data = 0;
            this.txtSequenseNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtSequenseNo.Format = null;
            this.txtSequenseNo.isAllowNegative = false;
            this.txtSequenseNo.isAllowSpecialChar = false;
            this.txtSequenseNo.isNumberOnly = true;
            this.txtSequenseNo.isNumeric = false;
            this.txtSequenseNo.LeaveColor = System.Drawing.Color.White;
            this.txtSequenseNo.Name = "txtSequenseNo";
            this.txtSequenseNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.dgPayments);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dgTaxes);
            this.panel2.Name = "panel2";
            // 
            // dgPayments
            // 
            resources.ApplyResources(this.dgPayments, "dgPayments");
            this.dgPayments.AllowUserToAddRows = false;
            this.dgPayments.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPayments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Payment,
            this.col_Money});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPayments.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgPayments.EnableHeadersVisualStyles = false;
            this.dgPayments.EnterKeyNavigation = false;
            this.dgPayments.LastKey = System.Windows.Forms.Keys.None;
            this.dgPayments.Name = "dgPayments";
            this.dgPayments.ReadOnly = true;
            // 
            // col_Payment
            // 
            this.col_Payment.DataPropertyName = "Key";
            resources.ApplyResources(this.col_Payment, "col_Payment");
            this.col_Payment.Name = "col_Payment";
            this.col_Payment.ReadOnly = true;
            // 
            // col_Money
            // 
            this.col_Money.DataPropertyName = "Value";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            this.col_Money.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.col_Money, "col_Money");
            this.col_Money.Name = "col_Money";
            this.col_Money.ReadOnly = true;
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // dgTaxes
            // 
            resources.ApplyResources(this.dgTaxes, "dgTaxes");
            this.dgTaxes.AllowUserToAddRows = false;
            this.dgTaxes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgTaxes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgTaxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTaxes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_TaxCategory,
            this.col_Taxes,
            this.col_Net});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTaxes.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgTaxes.EnableHeadersVisualStyles = false;
            this.dgTaxes.EnterKeyNavigation = false;
            this.dgTaxes.LastKey = System.Windows.Forms.Keys.None;
            this.dgTaxes.Name = "dgTaxes";
            this.dgTaxes.ReadOnly = true;
            // 
            // col_TaxCategory
            // 
            this.col_TaxCategory.DataPropertyName = "TaxCategory";
            resources.ApplyResources(this.col_TaxCategory, "col_TaxCategory");
            this.col_TaxCategory.Name = "col_TaxCategory";
            this.col_TaxCategory.ReadOnly = true;
            // 
            // col_Taxes
            // 
            this.col_Taxes.DataPropertyName = "Tax";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Taxes.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_Taxes, "col_Taxes");
            this.col_Taxes.Name = "col_Taxes";
            this.col_Taxes.ReadOnly = true;
            // 
            // col_Net
            // 
            this.col_Net.DataPropertyName = "Net";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Net.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.col_Net, "col_Net");
            this.col_Net.Name = "col_Net";
            this.col_Net.ReadOnly = true;
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.btnProWiseReport);
            this.panel4.Controls.Add(this.btnCloseCash);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Controls.Add(this.btnPrint);
            this.panel4.Name = "panel4";
            // 
            // btnProWiseReport
            // 
            resources.ApplyResources(this.btnProWiseReport, "btnProWiseReport");
            this.btnProWiseReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProWiseReport.ForeColor = System.Drawing.Color.White;
            this.btnProWiseReport.Name = "btnProWiseReport";
            this.btnProWiseReport.UseVisualStyleBackColor = false;
            this.btnProWiseReport.Click += new System.EventHandler(this.btnProWiseReport_Click);
            // 
            // btnCloseCash
            // 
            resources.ApplyResources(this.btnCloseCash, "btnCloseCash");
            this.btnCloseCash.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCloseCash.ForeColor = System.Drawing.Color.White;
            this.btnCloseCash.Name = "btnCloseCash";
            this.btnCloseCash.UseVisualStyleBackColor = false;
            this.btnCloseCash.Click += new System.EventHandler(this.btnCloseCash_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtMoney
            // 
            resources.ApplyResources(this.txtMoney, "txtMoney");
            this.txtMoney.BackColor = System.Drawing.Color.White;
            this.txtMoney.Data = 0;
            this.txtMoney.EnterColor = System.Drawing.Color.LightPink;
            this.txtMoney.Format = null;
            this.txtMoney.isAllowNegative = false;
            this.txtMoney.isAllowSpecialChar = false;
            this.txtMoney.isNumberOnly = false;
            this.txtMoney.isNumeric = true;
            this.txtMoney.LeaveColor = System.Drawing.Color.White;
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Data = 0;
            this.txtTotal.EnterColor = System.Drawing.Color.LightPink;
            this.txtTotal.Format = null;
            this.txtTotal.isAllowNegative = false;
            this.txtTotal.isAllowSpecialChar = false;
            this.txtTotal.isNumberOnly = false;
            this.txtTotal.isNumeric = true;
            this.txtTotal.LeaveColor = System.Drawing.Color.White;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtNet
            // 
            resources.ApplyResources(this.txtNet, "txtNet");
            this.txtNet.BackColor = System.Drawing.Color.White;
            this.txtNet.Data = 0;
            this.txtNet.EnterColor = System.Drawing.Color.LightPink;
            this.txtNet.Format = null;
            this.txtNet.isAllowNegative = false;
            this.txtNet.isAllowSpecialChar = false;
            this.txtNet.isNumberOnly = false;
            this.txtNet.isNumeric = true;
            this.txtNet.LeaveColor = System.Drawing.Color.White;
            this.txtNet.Name = "txtNet";
            this.txtNet.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTaxes
            // 
            resources.ApplyResources(this.txtTaxes, "txtTaxes");
            this.txtTaxes.BackColor = System.Drawing.Color.White;
            this.txtTaxes.Data = 0;
            this.txtTaxes.EnterColor = System.Drawing.Color.LightPink;
            this.txtTaxes.Format = null;
            this.txtTaxes.isAllowNegative = false;
            this.txtTaxes.isAllowSpecialChar = false;
            this.txtTaxes.isNumberOnly = false;
            this.txtTaxes.isNumeric = true;
            this.txtTaxes.LeaveColor = System.Drawing.Color.White;
            this.txtTaxes.Name = "txtTaxes";
            this.txtTaxes.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSales
            // 
            resources.ApplyResources(this.txtSales, "txtSales");
            this.txtSales.BackColor = System.Drawing.Color.White;
            this.txtSales.Data = 0;
            this.txtSales.EnterColor = System.Drawing.Color.LightPink;
            this.txtSales.Format = null;
            this.txtSales.isAllowNegative = false;
            this.txtSales.isAllowSpecialChar = false;
            this.txtSales.isNumberOnly = true;
            this.txtSales.isNumeric = false;
            this.txtSales.LeaveColor = System.Drawing.Color.White;
            this.txtSales.Name = "txtSales";
            this.txtSales.ReadOnly = true;
            this.txtSales.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtTransactions
            // 
            resources.ApplyResources(this.txtTransactions, "txtTransactions");
            this.txtTransactions.BackColor = System.Drawing.Color.White;
            this.txtTransactions.Data = 0;
            this.txtTransactions.EnterColor = System.Drawing.Color.LightPink;
            this.txtTransactions.Format = null;
            this.txtTransactions.isAllowNegative = false;
            this.txtTransactions.isAllowSpecialChar = false;
            this.txtTransactions.isNumberOnly = true;
            this.txtTransactions.isNumeric = false;
            this.txtTransactions.LeaveColor = System.Drawing.Color.White;
            this.txtTransactions.Name = "txtTransactions";
            this.txtTransactions.ReadOnly = true;
            this.txtTransactions.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // CloseCash
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtNet);
            this.Controls.Add(this.txtTaxes);
            this.Controls.Add(this.txtSales);
            this.Controls.Add(this.txtTransactions);
            this.Name = "CloseCash";
            this.Load += new System.EventHandler(this.CloseCash_Load);
            this.Controls.SetChildIndex(this.txtTransactions, 0);
            this.Controls.SetChildIndex(this.txtSales, 0);
            this.Controls.SetChildIndex(this.txtTaxes, 0);
            this.Controls.SetChildIndex(this.txtNet, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.txtMoney, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTaxes)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
