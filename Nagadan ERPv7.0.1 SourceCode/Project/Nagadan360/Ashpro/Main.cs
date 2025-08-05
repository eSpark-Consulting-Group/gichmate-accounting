using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using Tulpep.NotificationWindow;
using System.Globalization;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace Ashpro
{
	public class Main : Form
	{
		private DBContext db;

		private SelectedFYear selectedFYear = new SelectedFYear();

		private FinancialYear financialYear = new FinancialYear();

		private string sFYear;

		private SpeechSynthesizer voice;

		private OpenFileDialog dlg;

		private string strLogged;

		private SolidBrush brush;

		private string imgPath;

		private int iPrevilegeId;

		private ClosingDialog frm;

		private JournalEntryReportView JrnlRegister = new JournalEntryReportView();

		private TrialBalanceView trailBalance = new TrialBalanceView();

		private ReminderView reminder = new ReminderView();

		private Calendar calendar = new Calendar();

		private ProductEditing proEditing = new ProductEditing();

		private SendMail sendMail = new SendMail();

		private VideoTutorial videoTutorial = new VideoTutorial();

		private StockAdjusterView stockAdjuster = new StockAdjusterView();

		private ProductWiseGSTReportView proGSTRpt = new ProductWiseGSTReportView();

		private AccountGroup accountGroup = new AccountGroup();

		private Admin admin = new Admin();

		private CompanyDetails CDetails = new CompanyDetails();

		private LockApplication lockapplication;

		private VehicleView vehicle = new VehicleView();

		private Shelfs self = new Shelfs();

		private Account account = new Account();

		private ProductGroup PGroup = new ProductGroup();

		private Product product = new Product();

		private Brand brand = new Brand();

		private Unit unit = new Unit();

		private Godown godown = new Godown();

		private TaxCategory tCategory = new TaxCategory();

		private PriceCategory priceCetegory = new PriceCategory();

		private Route root = new Route();

		private SalesInvoiceView salesInvoice = new SalesInvoiceView();

		private DatabaseAutoBackup dbAutoBackup = new DatabaseAutoBackup();

		private ChangePassword changePWord = new ChangePassword();

		private Calculator calcu = new Calculator();

		private About aboutus = new About();

		private ContactUs contactus = new ContactUs();

		private ReportBugs reportbuggs = new ReportBugs();

		private ChangeDate changeDate = new ChangeDate();

		private SalesEstimateView saleEstimate = new SalesEstimateView();

		private SalesReportView saleEstimateReport = new SalesReportView();

		private LedgerReport ledgerReport = new LedgerReport();

		private ProductLedgerReportView productLedgerReport = new ProductLedgerReportView();

		private SalesReturnView salesReturn = new SalesReturnView();

		private ChequeRecipt cheqReceipt = new ChequeRecipt("Receipt");

		private SalesView salesView = new SalesView();

		private BillPrint billPrint = new BillPrint();

		private FinancialPeriod fPeriod = new FinancialPeriod();

		private SelectedFyear SelectedFY = new SelectedFyear();

		private ReceiptView cashReceipt = new ReceiptView("Cash Receipt");

		private PurchaseInvoiceView Purchase = new PurchaseInvoiceView();

		private PurchaseReturnView purchaseReturn = new PurchaseReturnView();

		private PaymentView cashPayment = new PaymentView("Cash Payment");

		private JournalEntryView journalEntry = new JournalEntryView();

		private OpeningBalance openeningBalance = new OpeningBalance();

		private DebitNoteView debitNote = new DebitNoteView();

		private CreditNoteView creditNote = new CreditNoteView();

		private SalesAndSalesReturnReportView salesAndReturn = new SalesAndSalesReturnReportView();

		private PrintDesignView printDesign = new PrintDesignView();

		private FirstPurchase FPurchase = new FirstPurchase();

		private PartyListView partyList = new PartyListView();

		private PriceListView priceListReport = new PriceListView();

		private SalesReturnReportView saleReturnReport = new SalesReturnReportView();

		private PurchaseReportView purchaseReport = new PurchaseReportView();

		private PurchaseReturnReportView purchaseReturnReport = new PurchaseReturnReportView();

		private PaymentReceiptReportView payRecRpt = new PaymentReceiptReportView();

		private OutstandingReportView outStandingReport = new OutstandingReportView();

		private ItemWiseSalesReportView itemWiseSReport = new ItemWiseSalesReportView();

		private ItemWiseSalesReportView itemWiseSReportP = new ItemWiseSalesReportView();

		private InventorySalesRegisterView inventorySRegister = new InventorySalesRegisterView("PCategory Sales");

		private InventorySalesRegisterView inventorySRegisterSales = new InventorySalesRegisterView("Sales");

		private InventorySalesRegisterView inventorySRegisterSalesReturn = new InventorySalesRegisterView("Sales Return");

		private InventorySalesRegisterView inventorySRegisterPurchase = new InventorySalesRegisterView("Purchase");

		private InventorySalesRegisterView inventorySRegisterPurchaseReturn = new InventorySalesRegisterView("Purchase Return");

		private TransactionReportView transactionReport = new TransactionReportView();

		private Product_Flow_Report_Settings PFlowRpt = new Product_Flow_Report_Settings();

		private UnSoldProductReportView Unsoldproductreport = new UnSoldProductReportView();

		private ProductCombinationView proCombination = new ProductCombinationView("Product Combination");

		private ProductCombinationView Packing = new ProductCombinationView("Packing");

		private GodownStockReportView godownStkReport = new GodownStockReportView();

		private StockTransferView stockTransfer = new StockTransferView("Stock Trancfer");

		private StockTransferView DamageEntry = new StockTransferView("Damage Entry");

		private Cost_Centres costCentres = new Cost_Centres();

		private CustomerPrevilegeCard cpCard = new CustomerPrevilegeCard();

		private OrderFormView sOrder = new OrderFormView("Sales Order");

		private OrderFormView sQuotation = new OrderFormView("Sales Quotation");

		private OrderFormView pOrder = new OrderFormView("Purchase Order");

		private BankReconciliation bankReconciliation = new BankReconciliation();

		private UserManagement uManagement = new UserManagement();

		private SalesOrderReportView SlsOrderRpt = new SalesOrderReportView("Sales Order Report");

		private SalesOrderReportView SlsQuotationRpt = new SalesOrderReportView("Sales Quatation Report");

		private SalesOrderReportView pOrderRpt = new SalesOrderReportView("Purchase Order Report");

		private UserPrevilegeView uPrevilege = new UserPrevilegeView();

		private Application_Settings appSettings = new Application_Settings();

		private CustomTransReport cusTranRpt = new CustomTransReport();

		private BackupDatabase backupDB = new BackupDatabase("Backup");

		private BackupDatabase restoreDB = new BackupDatabase("Restore");

		private Balance_Sheet bSheet = new Balance_Sheet();

		private Profit_and_Lose_Account pfAccount = new Profit_and_Lose_Account();

		private DailyAccountView dayBook = new DailyAccountView();

		private EmployeeDetails empDetails = new EmployeeDetails();

		private GSTSalesRptView gstSaleRpt = new GSTSalesRptView("Sales");

		private GSTSalesRptView gstPurRpt = new GSTSalesRptView("Purchase");

		private ChequeRecipt chequePayment = new ChequeRecipt("Payment");

		private SalaryProcessView salProcess = new SalaryProcessView();

		private ResetDatabase resetDB = new ResetDatabase();

		private BillGeneration billGeneration = new BillGeneration();

		private CloseCash dayClose = new CloseCash();

		private PendingOrderReportView pendingOR = new PendingOrderReportView();

		private StockReportView stockRpt = new StockReportView();

		private SalesCounter slsCounter = new SalesCounter();

		private StockTrnasferReportView stockTranRpt = new StockTrnasferReportView("Stock Transfer Report");

		private StockTrnasferReportView DmgstockTranRpt = new StockTrnasferReportView("Damage Stock Report");

		private DebitCreditCardMaster dcCardMaster = new DebitCreditCardMaster();

		private SalesSummaryReportView slsSummaryRpt = new SalesSummaryReportView();

		private BarcodeDesign barcode = new BarcodeDesign();

		private PaymentView bankPayment = new PaymentView("Bank Payment");

		private ReceiptView bankReceipt = new ReceiptView("Bank Receipt");

		private ExportAndImport export = new ExportAndImport("Export Data");

		private ExportAndImport import = new ExportAndImport("Import Data");

		private StaffAttendanceView attendance = new StaffAttendanceView();

		private TransactionWithoutInventory tranWtotInv = new TransactionWithoutInventory();

		private ProductManufacturingView manufacturing = new ProductManufacturingView();

		private Product_Register_Settings proRRegisterSettings = new Product_Register_Settings();

		private PrintBarcode printBarcode = new PrintBarcode();

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem administrationToolStripMenuItem;

		private ToolStripMenuItem mastersToolStripMenuItem;

		private ToolStripMenuItem accountsGroupToolStripMenuItem;

		private ToolStripMenuItem adminSettingsToolStripMenuItem;

		private ToolStripMenuItem firmDetailsToolStripMenuItem;

		private ToolStripMenuItem lockApplicationToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private ToolStripMenuItem accountLedgerToolStripMenuItem;

		private ToolStripMenuItem productGroupToolStripMenuItem;

		private ToolStripMenuItem productToolStripMenuItem;

		private ToolStripMenuItem brandToolStripMenuItem;

		private ToolStripMenuItem unitToolStripMenuItem;

		private ToolStripMenuItem godownToolStripMenuItem;

		private ToolStripMenuItem selfToolStripMenuItem;

		private ToolStripMenuItem taxCategoryToolStripMenuItem;

		private ToolStripMenuItem priceCategoryToolStripMenuItem;

		private ToolStripMenuItem rootToolStripMenuItem;

		private ToolStripMenuItem vehicleToolStripMenuItem;

		private ToolStripMenuItem salesToolStripMenuItem;

		private ToolStripMenuItem invoiceToolStripMenuItem;

		private ToolStripMenuItem estimateToolStripMenuItem;

		private ToolStripMenuItem orderToolStripMenuItem;

		private ToolStripMenuItem quotationToolStripMenuItem;

		private ToolStripMenuItem returnToolStripMenuItem;

		private ToolStripMenuItem billPrintToolStripMenuItem;

		private ToolStripMenuItem purchaseToolStripMenuItem;

		private ToolStripMenuItem invoiceToolStripMenuItem1;

		private ToolStripMenuItem returnToolStripMenuItem1;

		private ToolStripMenuItem orderToolStripMenuItem1;

		private ToolStripMenuItem packingToolStripMenuItem;

		private ToolStripMenuItem packingToolStripMenuItem1;

		private ToolStripMenuItem stockTransferToolStripMenuItem;

		private ToolStripMenuItem damageEntryToolStripMenuItem;

		private ToolStripMenuItem stockAdjusterToolStripMenuItem;

		private ToolStripMenuItem itemsPriceSettingsToolStripMenuItem;

		private ToolStripMenuItem accountsToolStripMenuItem;

		private ToolStripMenuItem cashPaymentToolStripMenuItem;

		private ToolStripMenuItem cashReceiptsToolStripMenuItem;

		private ToolStripMenuItem bankPaymentToolStripMenuItem;

		private ToolStripMenuItem bankReceptsToolStripMenuItem;

		private ToolStripMenuItem journalEntriesToolStripMenuItem;

		private ToolStripMenuItem openingBalanceEntryToolStripMenuItem;

		private ToolStripMenuItem postDatedChequeReceivableToolStripMenuItem;

		private ToolStripMenuItem postDatedChequePayableToolStripMenuItem;

		private ToolStripMenuItem accountReportsToolStripMenuItem;

		private ToolStripMenuItem inventoryReportsToolStripMenuItem;

		private ToolStripMenuItem accountsReportsToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem staffToolStripMenuItem;

		private ToolStripMenuItem userToolStripMenuItem;

		private ToolStripMenuItem optionToolStripMenuItem;

		private ToolStripMenuItem toolsToolStripMenuItem;

		private ToolStripMenuItem aboutToolStripMenuItem;

		private ToolStrip toolStrip1;

		private ToolStripButton tsbSale;

		private ToolStripButton tsbPurchase;

		private ToolStripMenuItem salesAndSalesReturnReportToolStripMenuItem;

		private ToolStripMenuItem purchaseAndPurchaseReturnReportToolStripMenuItem;

		private ToolStripMenuItem salesEstimateReportToolStripMenuItem;

		private ToolStripMenuItem salesReportToolStripMenuItem;

		private ToolStripMenuItem salesReturnReportToolStripMenuItem;

		private ToolStripMenuItem purchaseReportToolStripMenuItem;

		private ToolStripMenuItem purchaseReturnReportToolStripMenuItem;

		private ToolStripMenuItem salesOrderReportToolStripMenuItem;

		private ToolStripMenuItem salesQuotationReportToolStripMenuItem;

		private ToolStripMenuItem purchaseOrderReportToolStripMenuItem;

		private ToolStripMenuItem partyListToolStripMenuItem;

		private ToolStripMenuItem accountPayableAndReceivableReportToolStripMenuItem;

		private ToolStripMenuItem stockReportToolStripMenuItem;

		private ToolStripMenuItem tsmItmWiseSlsRpt;

		private ToolStripMenuItem tsmItmWisePurRpt;

		private ToolStripMenuItem productLedgerToolStripMenuItem;

		private ToolStripMenuItem stockTransferEportToolStripMenuItem;

		private ToolStripMenuItem damageStockReportToolStripMenuItem;

		private ToolStripMenuItem accountBookToolStripMenuItem;

		private ToolStripMenuItem cashBookToolStripMenuItem;

		private ToolStripMenuItem bankBookToolStripMenuItem;

		private ToolStripMenuItem dayBookToolStripMenuItem;

		private ToolStripMenuItem paymentRegisterToolStripMenuItem;

		private ToolStripMenuItem receiptRegisterToolStripMenuItem;

		private ToolStripMenuItem journalRegisterToolStripMenuItem;

		private ToolStripMenuItem trialBalanceToolStripMenuItem;

		private ToolStripMenuItem incomeAndExpenditureReportToolStripMenuItem;

		private ToolStripMenuItem balanceSheetToolStripMenuItem;

		private ToolStripMenuItem outputVatReportToolStripMenuItem;

		private ToolStripMenuItem inputVatReportToolStripMenuItem;

		private ToolStripMenuItem staffRegistrationToolStripMenuItem;

		private ToolStripMenuItem roleAndPrevilageToolStripMenuItem;

		private ToolStripMenuItem createUserToolStripMenuItem;

		private ToolStripMenuItem changeDateToolStripMenuItem;

		private ToolStripMenuItem billGenetrationToolStripMenuItem;

		private ToolStripMenuItem applicationSettingsToolStripMenuItem;

		private ToolStripMenuItem calculatorToolStripMenuItem;

		private ToolStripMenuItem excelToolStripMenuItem;

		private ToolStripMenuItem notepadToolStripMenuItem;

		private ToolStripMenuItem paintToolStripMenuItem;

		private ToolStripMenuItem aboutUsToolStripMenuItem;

		private ToolStripMenuItem contactUsToolStripMenuItem;

		private ToolStripMenuItem videoTutorialToolStripMenuItem;

		private ToolStripButton tsbProduct;

		private ToolStripButton tsbPaymentAndReceipt;

		private ToolStripButton tsbExpense;

		private ToolStripButton tsbStock;

		private ToolStripButton tsbLedger;

		private ToolStripButton tsmCalculator;

		private ToolStripButton tsbCustomerReport;

		private ToolStripButton tsbSupplierReport;

		private ToolStripButton tsbVatReport;

		private ToolStripButton tsmDayClose;

		private ToolStripButton toolStripButton14;

		private ToolStripButton toolStripButton15;

		private ToolStripButton tsbLockApplication;

		private ToolStripButton tsbCalendar;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		//private Timer tmrClock;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private ToolStripStatusLabel toolStripStatusLabel3;

		private ToolStripMenuItem rootwiseCollectionReportToolStripMenuItem;

		private ToolStripMenuItem salesViewToolStripMenuItem;

		private ToolStripMenuItem reportBuggsToolStripMenuItem;

		private ToolStripMenuItem themeToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem4;

		private ToolStripMenuItem toolStripMenuItem5;

		private ToolStripMenuItem DebitNotetoolStripMenuItem;

		private ToolStripMenuItem CreditNotetoolStripMenuItem;

		private ToolStripMenuItem printDesignToolStripMenuItem;

		private ToolStripMenuItem tsmiFirstPurchase;

		private ToolStripMenuItem toolStripMenuItem7;

		private ToolStripMenuItem inventorySalesRegisterToolStripMenuItem;

		private ToolStripMenuItem colorThemeToolStripMenuItem;

		private ToolStripMenuItem customThemeToolStripMenuItem;

		private ToolStripMenuItem transactionReportToolStripMenuItem;

		private ToolStripMenuItem productFlowReportToolStripMenuItem;

		private ToolStripMenuItem unsoldProductToolStripMenuItem;

		private ToolStripMenuItem godownStockReportToolStripMenuItem;

		private ToolStripMenuItem productCombainToolStripMenuItem;

		private ToolStripMenuItem customTransactionAndReportToolStripMenuItem;

		private ToolStripMenuItem databaseBackupToolStripMenuItem;

		private ToolStripMenuItem summaryToolStripMenuItem;

		private ToolStripMenuItem detailedToolStripMenuItem;

		private ToolStripMenuItem salaryProcessToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem12;

		private ToolStripMenuItem toolStripMenuItem13;

		private ToolStripMenuItem dayCloseToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem8;

		private ToolStripMenuItem priceCategoryWiseSalesReportToolStripMenuItem;

		private ToolStripMenuItem rootwiseSalesReportToolStripMenuItem1;

		private ToolStripMenuItem salesmanWiseSalesReportToolStripMenuItem1;

		private ToolStripMenuItem partyWiseSalesReportToolStripMenuItem1;

		private ToolStripMenuItem groupWisePurchaseReportToolStripMenuItem1;

		private ToolStripMenuItem partywisePurhaseReportToolStripMenuItem1;

		private ToolStripMenuItem exicutiveWiseCollectionReportToolStripMenuItem1;

		private ToolStripMenuItem pendingOrderReportToolStripMenuItem;

		private ToolStripMenuItem productWiseGSTToolStripMenuItem;

		private ToolStripMenuItem calendarToolStripMenuItem;

		private ToolStripMenuItem summaryReportToolStripMenuItem;

		private ToolStripMenuItem tsmInvntrySlsRtnRgst;

		private ToolStripMenuItem tsmInvntryPurRgstr;

		private ToolStripMenuItem tsmInvntryPurRtnRgstr;

		private ToolStripMenuItem productBarcodeDesignToolStripMenuItem;

		private ToolTip toolTip1;

		private ToolStripMenuItem salesReturnSummaryReportToolStripMenuItem;

		private ToolStripMenuItem purchaseSummaryReportToolStripMenuItem;

		private ToolStripMenuItem purchaseReturnSummaryReportToolStripMenuItem;

		private ToolStripLabel lblPendingPeriod;

		private ToolStripMenuItem toolStripMenuItem9;

		private ToolStripMenuItem detailedToolStripMenuItem1;

		private ToolStripMenuItem summaryToolStripMenuItem1;

		private ToolStripMenuItem detailedToolStripMenuItem2;

		private ToolStripMenuItem summaryToolStripMenuItem2;

		private ToolStripMenuItem ToolMenuExportData;

		private ToolStripMenuItem ToolMenuImportData;

		private ToolStripMenuItem databaseRestoreToolStripMenuItem;

		private ToolStripMenuItem attendanceToolStripMenuItem;

		private ToolStripMenuItem transactionWithoutInventoryToolStripMenuItem;

		private ToolStripMenuItem manufacturingToolStripMenuItem;

		private ToolStripMenuItem productRegisterToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripMenuItem menuParty;

		private ToolStripMenuItem menuSupplier;

		private ToolStripMenuItem menuBank;

		private ToolStripMenuItem menuExpenseAccount;

		private ToolStripMenuItem dashboardThemeToolStripMenuItem;

		private ToolStripMenuItem bankReconciliationToolStripMenuItem;

		private ToolStripMenuItem changePasswordToolStripMenuItem1;

		private ToolStripMenuItem sendMailToolStripMenuItem1;

		private ToolStripMenuItem qRCodeToolStripMenuItem;

		private ToolStripMenuItem tsmBranch;

		private ToolStripMenuItem tsmBatch;

		private ToolStripMenuItem tsmRateFormat;

		private ToolStripMenuItem tsmLabelScale;

		private ToolStripMenuItem receiptAndPaymentToolStripMenuItem;

		private ToolStripMenuItem bulkReceiptToolStripMenuItem;

		private ToolStripMenuItem otherReportsToolStripMenuItem;

		private ToolStripMenuItem tsmSalesReport;

		private ToolStripMenuItem tsmInventoryReport;

		private ToolStripMenuItem tsmTaxReport;

		private ToolStripMenuItem tsmCustomerReport;

		private ToolStripMenuItem tsmSupplierReport;

		private ToolStripMenuItem tsmUpdate;
        private ToolStripMenuItem tsmCurrency;

		public Main()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
            AniHelper.IconChangeMethod(this);
			base.WindowState = FormWindowState.Maximized;
			//tmrClock.Start();
		}

        
        private void RemainderMethod()
		{
			List<ReminderTB> list = (from x in db.Reminders()
				where x.TargetUserId == Settings.Default.intLogOnId && x.ReminderDate == AniHelper.GetDate(DateTime.Now) && x.FK_BranchId == Settings.Default.iBranchId
				select x).ToList();
			if (list.Count > 0)
			{
				int iCount = 1;
				string strReminder = "Dear Sir you have a reminder today Please note";
				list.ForEach(delegate(ReminderTB x)
				{
					strReminder = strReminder + " " + iCount + "  " + x.Remainder + "\n";
					iCount++;
				});
				PopupNotifier popupNotifier = new PopupNotifier();
				popupNotifier.TitleText = "Reminder";
				popupNotifier.ContentText = strReminder;
				popupNotifier.Popup();
				voice = new SpeechSynthesizer();
				voice.SelectVoiceByHints(VoiceGender.Female);
				voice.SpeakAsync(strReminder);
				reminder = new ReminderView();
				reminder.blPassV = true;
				OpenForm(reminder);
			}
		}

		public void OpenForm(Form frm, bool isDistinct = true, string sForm = null)
		{
			try
			{
				MenuDetails menuDetails = new MenuDetails();
				UserPrevilegeDtl userPrevilegeDtl = new UserPrevilegeDtl();
				menuDetails = null;
				userPrevilegeDtl = null;
				menuDetails = ((sForm != null) ? db.MenuDetails(new
				{
					Name = sForm
				}).FirstOrDefault() : db.MenuDetails(new
				{
					ClassName = frm.Name
				}).FirstOrDefault());
				bool flag = true;
				if (menuDetails != null)
				{
					userPrevilegeDtl = db.UserPrevilegeDtls(new
					{
						FK_UserPrevilegeHdrId = iPrevilegeId,
						FK_MenuDetailsId = menuDetails.Id
					}).FirstOrDefault();
				}
				if (userPrevilegeDtl != null)
				{
					flag = userPrevilegeDtl.Enable;
				}
				if (!flag)
				{
					Messages.InformationMessage("You have no authorisation to open this Window!");
				}
				else if (frm.IsDisposed)
				{
					Type type = frm.GetType();
					Form form = (Form)Activator.CreateInstance(type);
					form.MdiParent = this;
					form.Show();
				}
				else if (isDistinct)
				{
					if (!base.MdiChildren.Contains(frm))
					{
						frm.MdiParent = this;
						frm.Show();
					}
					else
					{
						frm.Activate();
						frm.BringToFront();
					}
				}
				else
				{
					frm.MdiParent = this;
					frm.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void OpenFormSDF(Form frm)
		{
			try
			{
				Type type = frm.GetType();
				Form form = (Form)Activator.CreateInstance(type);
				form.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DisableControls()
		{
			for (int i = 0; i < base.Controls.Count; i++)
			{
				if (base.Controls[i].GetType() != typeof(MdiClient))
				{
					base.Controls[i].Enabled = false;
				}
			}
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form form in mdiChildren)
			{
				form.Enabled = false;
			}
			if (lockapplication != null)
			{
				lockapplication.Enabled = true;
			}
		}

		private void EnableControls()
		{
			for (int i = 0; i < base.Controls.Count; i++)
			{
				if (base.Controls[i].GetType() != typeof(MdiClient))
				{
					base.Controls[i].Enabled = true;
				}
			}
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form form in mdiChildren)
			{
				form.Enabled = true;
			}
		}

		private void ColorAddMethod()
		{
			int num = Screen.PrimaryScreen.Bounds.Width;
			int num2 = Screen.PrimaryScreen.Bounds.Height;
			brush = new SolidBrush(Color.DodgerBlue);
			Bitmap bitmap = new Bitmap(1, 1);
			Graphics graphics = Graphics.FromImage(bitmap);
			bitmap = new Bitmap(bitmap, num, num2);
			graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(brush, 0, 0, num, num2);
			BackgroundImage = bitmap;
		}

		private void FinancialPeriodMethod()
		{
			try
			{
				selectedFYear = AshproCommon.GetListMethod<SelectedFYear>("Select * From SelectedFYear").FirstOrDefault();
				if (selectedFYear != null)
				{
					sFYear = selectedFYear.FyearId.ToString();
					selectedFYear.SlectedFY.ToString();
					financialYear = AshproCommon.GetObjectDetails<FinancialYear>("Select * From FinancialYear Where FyearId='" + sFYear + "'");
					DateTime dateTime = financialYear.DateTo.toDateTime();
					if (dateTime < DateTime.Now)
					{
						OpenForm(fPeriod);
					}
				}
				else
				{
					OpenForm(fPeriod);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void AutoBackUpMethod()
		{
			try
			{
				dbAutoBackup = new DatabaseAutoBackup();
				if (dbAutoBackup.ShowDialog() == DialogResult.OK)
				{
					AppExitMethod();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private static void AppExitMethod()
		{
			try
			{
				foreach (Form openForm in Application.OpenForms)
				{
					if (openForm.Name != "Main")
					{
						openForm.Close();
					}
				}
			}
			catch (Exception)
			{
			}
			try
			{
				Application.Exit();
			}
			catch (Exception)
			{
				Environment.Exit(0);
			}
		}

		private void accountsGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(accountGroup);
		}

		private void vehicleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(vehicle);
		}

		private void selfToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(self);
		}

		private void accountLedgerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = "Chart Of Account";
			account.accountGroupName = string.Empty;
			OpenForm(account, isDistinct: false, "Account");
		}

		private void productGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(PGroup);
		}

		private void productToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(product);
		}

		private void brandToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(brand);
		}

		private void unitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(unit);
		}

		private void godownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(godown);
		}

		private void taxCategoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(tCategory);
		}

		private void priceCategoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(priceCetegory);
		}

		private void rootToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(root);
		}

		private void menuSupplier_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = "Supplier";
			account.accountGroupName = "SUPPLIERS/SUNDRY CREDITORS";
			OpenForm(account, isDistinct: false, "Supplier");
		}

		private void menuParty_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = "Customers";
			account.accountGroupName = "CUSTOMER/SUNDRY DEBTORS";
			OpenForm(account, isDistinct: false, "Customers");
		}

		private void menuBank_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = "Bank";
			account.accountGroupName = "CASH AT BANK";
			OpenForm(account, isDistinct: false, "Bank");
		}

		private void menuExpenseAccount_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = "Expense Account";
			account.accountGroupName = "INDIRECT EXPENSES";
			OpenForm(account, isDistinct: false, "Expense");
		}

		private void tsmBranch_Click(object sender, EventArgs e)
		{
			BranchView branchView = new BranchView();
			OpenForm(branchView);
		}

		private void tsmBatch_Click(object sender, EventArgs e)
		{
			BatchView batchView = new BatchView();
			OpenForm(batchView);
		}

		private void tsmCurrency_Click(object sender, EventArgs e)
		{
			CurrencyView currencyView = new CurrencyView();
			OpenForm(currencyView);
		}

		private void transactionWithoutInventoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(tranWtotInv);
		}

		private void bankPaymentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bankPayment = new PaymentView("Bank Payment");
			OpenForm(bankPayment, isDistinct: false, "BankPayment");
		}

		private void bankReceptsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bankReceipt = new ReceiptView("Bank Receipt");
			OpenForm(bankReceipt, isDistinct: false, "BankReceipt");
		}

		private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFormSDF(salesInvoice);
		}

		private void estimateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(saleEstimate, isDistinct: false);
		}

		private void returnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(salesReturn, isDistinct: false);
		}

		private void postDatedChequeReceivableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cheqReceipt = new ChequeRecipt("Receipt");
			OpenForm(cheqReceipt, isDistinct: false, "ChequeRecipt");
		}

		private void salesViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(salesView);
		}

		private void cashReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cashReceipt = new ReceiptView("Cash Receipt");
			OpenForm(cashReceipt, isDistinct: false, "CashReceipt");
		}

		private void invoiceToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OpenFormSDF(Purchase);
		}

		private void returnToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OpenForm(purchaseReturn, isDistinct: false);
		}

		private void cashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cashPayment = new PaymentView("Cash Payment");
			OpenForm(cashPayment, isDistinct: false, "CashPayment");
		}

		private void journalEntriesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(journalEntry);
		}

		private void openingBalanceEntryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(openeningBalance);
		}

		private void DebitNotetoolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(debitNote);
		}

		private void CreditNotetoolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(creditNote);
		}

		private void toolStripMenuItem6_Click(object sender, EventArgs e)
		{
			OpenForm(FPurchase);
		}

		private void orderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sOrder = new OrderFormView("Sales Order");
			OpenForm(sOrder, isDistinct: false, "SalesOrder");
		}

		private void quotationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sQuotation = new OrderFormView("Sales Quotation");
			OpenForm(sQuotation, isDistinct: false, "SalesQuotation");
		}

		private void orderToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			pOrder = new OrderFormView("Purchase Order");
			OpenForm(pOrder, isDistinct: false, "Purchase Order");
		}

		private void bankReconciliationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(bankReconciliation);
		}

		private void receiptAndPaymentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PaymentAndReceiptView paymentAndReceiptView = new PaymentAndReceiptView();
			OpenForm(paymentAndReceiptView);
		}

		private void bulkReceiptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BulkReceiptView bulkReceiptView = new BulkReceiptView();
			OpenForm(bulkReceiptView);
		}

		private void productCombainToolStripMenuItem_Click(object sender, EventArgs e)
		{
			proCombination = new ProductCombinationView("Product Combination");
			OpenForm(proCombination, isDistinct: false, "ProductCombination");
		}

		private void packingToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Packing = new ProductCombinationView("Packing");
			OpenForm(Packing, isDistinct: false, "Packing");
		}

		private void stockTransferToolStripMenuItem_Click(object sender, EventArgs e)
		{
			stockTransfer = new StockTransferView("Stock Trancfer");
			OpenForm(stockTransfer, isDistinct: false, "StockTrancfer");
		}

		private void damageEntryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DamageEntry = new StockTransferView("Damage Entry");
			OpenForm(DamageEntry, isDistinct: false, "DamageEntry");
		}

		private void stockAdjusterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(stockAdjuster);
		}

		private void manufacturingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(manufacturing);
		}

		private void productRegisterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(proRRegisterSettings);
		}

		private void rootwiseCollectionReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Routewise Collection Report";
			OpenForm(payRecRpt, isDistinct: false, "CollectionReport");
		}

		private void rootwiseSalesReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			OpenForm(slsSummaryRpt, isDistinct: false, "RouteWiseSalesReport");
		}

		private void partyWiseSalesReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			OpenForm(slsSummaryRpt, isDistinct: false, "PartyWiseSalesReport");
		}

		private void groupWisePurchaseReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			inventorySRegisterPurchase = new InventorySalesRegisterView("Purchase");
			OpenForm(inventorySRegisterPurchase, isDistinct: false, "GroupWisePurchaseReport");
		}

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			inventorySRegisterSales = new InventorySalesRegisterView("Sales");
			OpenForm(inventorySRegisterSales, isDistinct: false, "GroupWiseSalesReport");
		}

		private void partywisePurhaseReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Purchase";
			OpenForm(slsSummaryRpt, isDistinct: false, "SupplierWisePurchaseReport");
		}

		private void exicutiveWiseCollectionReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Exicutivewise Collection Report";
			OpenForm(payRecRpt, isDistinct: false, "ExicutiveWiseCollectionReport");
		}

		private void summaryToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Payment Report Summary";
			OpenForm(payRecRpt);
		}

		private void summaryToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Receipt Report Summary";
			OpenForm(payRecRpt);
		}

		private void summaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			OpenForm(slsSummaryRpt);
		}

		private void salesReturnSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales Return";
			OpenForm(slsSummaryRpt, isDistinct: false, "SalesReturnSummary");
		}

		private void purchaseSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Purchase";
			OpenForm(slsSummaryRpt, isDistinct: false, "PurchaseSummaryReport");
		}

		private void purchaseReturnSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Purchase Return";
			OpenForm(slsSummaryRpt, isDistinct: false, "PurchaseReturnSummaryReport");
		}

		private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(trailBalance);
		}

		private void journalRegisterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(JrnlRegister);
		}

		private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saleEstimateReport = new SalesReportView();
			saleEstimateReport.PassingValue = "Sales";
			OpenForm(saleEstimateReport, isDistinct: false, "SalesReport");
		}

		private void productWiseGSTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(proGSTRpt);
		}

		private void salesEstimateReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saleEstimateReport = new SalesReportView();
			saleEstimateReport.PassingValue = "Normal Sales";
			OpenForm(saleEstimateReport, isDistinct: false, "SalesEstimateReport");
		}

		private void accountBookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(ledgerReport);
		}

		private void productLedgerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(productLedgerReport);
		}

		private void salesAndSalesReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			salesAndReturn = new SalesAndSalesReturnReportView();
			salesAndReturn.ValueDetails = "Sales";
			OpenForm(salesAndReturn);
		}

		private void tsmItmWiseSlsRpt_Click(object sender, EventArgs e)
		{
			itemWiseSReport = new ItemWiseSalesReportView();
			itemWiseSReport.strFormType = "Sales";
			OpenForm(itemWiseSReport);
		}

		private void tsmItmWisePurRpt_Click(object sender, EventArgs e)
		{
			itemWiseSReportP = new ItemWiseSalesReportView();
			itemWiseSReportP.strFormType = "Purchase";
			OpenForm(itemWiseSReportP, isDistinct: false, "ItemWisePurchaseReport");
		}

		private void partyListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(partyList);
		}

		private void toolStripMenuItem7_Click(object sender, EventArgs e)
		{
			OpenForm(priceListReport);
		}

		private void purchaseAndPurchaseReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			salesAndReturn = new SalesAndSalesReturnReportView();
			salesAndReturn.ValueDetails = "Purchase";
			OpenForm(salesAndReturn, isDistinct: false, "PurchaseAndPurchaseReturnReport");
		}

		private void salesReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(saleReturnReport);
		}

		private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(purchaseReport);
		}

		private void purchaseReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(purchaseReturnReport);
		}

		private void accountPayableAndReceivableReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(outStandingReport);
		}

		private void outStandingReport_FormClosing(object sender, FormClosingEventArgs e)
		{
			outStandingReport = null;
		}

		private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ledgerReport = new LedgerReport();
			ledgerReport.ValueLedger = "CASH IN HAND";
			OpenForm(ledgerReport, isDistinct: false, "CashBook");
		}

		private void bankBookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ledgerReport = new LedgerReport();
			ledgerReport.ValueLedger = "CASH AT BANK";
			OpenForm(ledgerReport, isDistinct: false, "BankBook");
		}

		private void priceCategoryWiseSalesReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			inventorySRegister = new InventorySalesRegisterView("PCategory Sales");
			OpenForm(inventorySRegister, isDistinct: false, "PriceCategoryWiseSalesReport");
		}

		private void inventorySalesRegisterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			inventorySRegisterSales = new InventorySalesRegisterView("Sales");
			OpenForm(inventorySRegisterSales);
		}

		private void tsmInvntrySlsRtnRgst_Click(object sender, EventArgs e)
		{
			inventorySRegisterSalesReturn = new InventorySalesRegisterView("Sales Return");
			OpenForm(inventorySRegisterSalesReturn, isDistinct: false, "InventorySalesReturnRegister");
		}

		private void tsmInvntryPurRgstr_Click(object sender, EventArgs e)
		{
			inventorySRegisterPurchase = new InventorySalesRegisterView("Purchase");
			OpenForm(inventorySRegisterPurchase, isDistinct: false, "InventoryPurchaseRegister");
		}

		private void tsmInvntryPurRtnRgstr_Click(object sender, EventArgs e)
		{
			OpenForm(inventorySRegisterPurchaseReturn, isDistinct: false, "InventoryPurchaseReturnRegister");
		}

		private void transactionReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(transactionReport);
		}

		private void otherReportsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			POSReports pOSReports = new POSReports();
			pOSReports.mainForm = this;
			OpenForm(pOSReports);
		}

		private void productFlowReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(PFlowRpt);
		}

		private void unsoldProductToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(Unsoldproductreport);
		}

		private void godownStockReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(godownStkReport);
		}

		private void salesOrderReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SlsOrderRpt = new SalesOrderReportView("Sales Order Report");
			OpenForm(SlsOrderRpt);
		}

		private void salesQuotationReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SlsQuotationRpt = new SalesOrderReportView("Sales Quatation Report");
			OpenForm(SlsQuotationRpt, isDistinct: false, "SalesQuatationReport");
		}

		private void purchaseOrderReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pOrderRpt = new SalesOrderReportView("Purchase Order Report");
			OpenForm(pOrderRpt, isDistinct: false, "PurchaseOrderReport");
		}

		private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(bSheet);
		}

		private void incomeAndExpenditureReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(pfAccount);
		}

		private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dayBook = new DailyAccountView();
			dayBook.PassingValue = "Summary";
			OpenForm(dayBook);
		}

		private void detailedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dayBook = new DailyAccountView();
			dayBook.PassingValue = "Detailed";
			OpenForm(dayBook);
		}

		private void staffRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(empDetails);
		}

		private void outputVatReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gstSaleRpt = new GSTSalesRptView("Sales");
			OpenForm(gstSaleRpt, isDistinct: false);
		}

		private void inputVatReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gstPurRpt = new GSTSalesRptView("Purchase");
			OpenForm(gstPurRpt, isDistinct: false, "GSTPurchaseRpt");
		}

		private void receiptRegisterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Receipt Report";
			OpenForm(payRecRpt, isDistinct: false, "ReceiptRegister");
		}

		private void paymentRegisterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Payment Report";
			OpenForm(payRecRpt, isDistinct: false, "PaymentRegister");
		}

		private void salesmanWiseSalesReportToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			OpenForm(slsSummaryRpt, isDistinct: false, "SalesmanWiseSalesReport");
		}

		private void pendingOrderReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(pendingOR);
		}

		private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(stockRpt);
		}

		private void stockTransferEportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			stockTranRpt = new StockTrnasferReportView("Stock transfer Report");
			OpenForm(stockTranRpt, isDistinct: false, "StockTrancfer");
		}

		private void damageStockReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DmgstockTranRpt = new StockTrnasferReportView("Damage Stock Report");
			OpenForm(DmgstockTranRpt, isDistinct: false, "DamageEntry");
		}

		private void tsmLabelScale_Click(object sender, EventArgs e)
		{
			LabelScale labelScale = new LabelScale();
			OpenForm(labelScale);
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			OpenForm(printBarcode);
		}

		private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(attendance);
		}

		private void ToolMenuExportData_Click(object sender, EventArgs e)
		{
			export = new ExportAndImport("Export");
			OpenForm(export, isDistinct: false, "ExportData");
		}

		private void ToolMenuImportData_Click(object sender, EventArgs e)
		{
			import = new ExportAndImport("Import");
			OpenForm(import, isDistinct: false, "ImportData");
		}

		private void productBarcodeDesignToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(barcode);
		}

		private void toolStripButton14_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		private void sendMailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(sendMail);
		}

		private void itemsPriceSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(proEditing);
		}

		private void calendarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(calendar);
		}

		private void proEditing_FormClosing(object sender, FormClosingEventArgs e)
		{
			proEditing = null;
		}

		private void remainderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(reminder);
		}

		private void videoTutorialToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(videoTutorial);
		}

		private void adminSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(admin);
		}

		private void firmDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(CDetails);
		}

		private void lockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
            //LockApplication locApp = new LockApplication();
            //OpenForm(locApp);
        }

		private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(changePWord);
		}

		private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(calcu);
		}

		private void excelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("Excel.exe");
		}

		private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("Notepad.exe");
		}

		private void paintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("mspaint.exe");
		}

		private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(aboutus);
		}

		private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(contactus);
		}

		private void reportBuggsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(reportbuggs);
		}

		private void changeDateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(changeDate);
		}

		private void billPrintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(billPrint);
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			OpenForm(fPeriod);
		}

		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			OpenForm(SelectedFY);
		}

		private void printDesignToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFormSDF(printDesign);
		}

		private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(uManagement);
		}

		private void roleAndPrevilageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(uPrevilege);
		}

		private void applicationSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(appSettings);
		}

		private void customTransactionAndReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(cusTranRpt);
		}

		private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			backupDB = new BackupDatabase("Backup");
			OpenForm(backupDB);
		}

		private void databaseRestoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			restoreDB = new BackupDatabase("Restore");
			OpenForm(restoreDB, isDistinct: false, "RestoreDatabase");
		}

		private void postDatedChequePayableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			chequePayment = new ChequeRecipt("Payment");
			OpenForm(chequePayment, isDistinct: false, "ChequeRecipt");
		}

		private void salaryProcessToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(salProcess);
		}

		private void toolStripMenuItem12_Click(object sender, EventArgs e)
		{
			OpenForm(resetDB);
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			OpenForm(billGeneration);
		}

		private void dayCloseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenForm(dayClose);
		}

		private void qRCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
			OpenForm(qRCodeGenerator);
		}

		private void tsmRateFormat_Click(object sender, EventArgs e)
		{
			RateFormatView rateFormatView = new RateFormatView();
			OpenForm(rateFormatView);
		}

		private async void Main_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			base.Icon = ((Utility.uManufacturer == "NagadanPro") ? Resources.ico_logo1 : Resources.ico_logo);
			if (Settings.Default.strBackImageName != "" && File.Exists(Settings.Default.strBackImageName))
			{
				BackgroundImage = Image.FromFile(Settings.Default.strBackImageName);
			}
			else if (Utility.uManufacturer == "NagadanPro")
			{
                if (Settings.Default.IsActivated)
                {
                    //BackgroundImage = Resources.wall;
                    Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");
                    BackgroundImage = image;
                }
                else
                {
                    //BackgroundImage = Resources.wall_demo;
                    Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");
                    BackgroundImage = image;
                }
            }
            else
            {
                //BackgroundImage = Resources.wall_360;
                Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");
                BackgroundImage = image;
            }
            BackgroundImageLayout = ImageLayout.Stretch;
			db = new DBContext();
			if (CommonSettings.entSettings.Count == 0)
			{
				Task task = new Task(CommonSettings.LoadSettings);
				task.Start();
				await task;
			}
			iPrevilegeId = db.Users().Single((User x) => x.UserId == Settings.Default.intLogOnId).PrevilageId.ToInt32();
			Settings.Default.appDate = DateTime.Now;
			Settings.Default.Save();
			if (toolStripStatusLabel3.Text != "Admin" && toolStripStatusLabel3.Text != Utility.uManufacturer)
			{
				adminSettingsToolStripMenuItem.Visible = false;
			}
			try
			{
				taxCategoryToolStripMenuItem.Visible = CommonSettings.blnTaxEnabled;
				estimateToolStripMenuItem.Visible = CommonSettings.blnSalesEstimateEnable;
				tsmiFirstPurchase.Visible = CommonSettings.blnShowFtPurchase;
				transactionWithoutInventoryToolStripMenuItem.Visible = CommonSettings.blnShowTransWitInvtry;
				lockApplicationToolStripMenuItem.Visible = CommonSettings.blnEnableAppLock;
				tsmBatch.Visible = CommonSettings.blnEnableBatch;
				tsmBranch.Visible = CommonSettings.blnEnableMultiBranch;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			strLogged = Settings.Default.intLogOnId.ToString();
			toolStripStatusLabel3.Text = AniHelper.ChangeFirstLetterCase(AshproCommon.IdFindMethod("SELECT UserName FROM[User] where UserId = '" + strLogged + "'"));
			if (Settings.Default.isMain)
			{
				invoiceToolStripMenuItem.Visible = true;
				estimateToolStripMenuItem.Visible = false;
			}
			else
			{
				invoiceToolStripMenuItem.Visible = false;
				estimateToolStripMenuItem.Visible = true;
			}
			RemainderMethod();
			FinancialPeriodMethod();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			toolStripStatusLabel1.Text = now.ToString("dd-MMM-yyyy-HH:mm:ss");
		}

		private void Lockapplication_FormClosing(object sender, FormClosingEventArgs e)
		{
			EnableControls();
		}

		private void dashboardThemeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThemeChanger themeChanger = new ThemeChanger();
			themeChanger.ShowDialog();
		}

		private void toolStripButton15_Click(object sender, EventArgs e)
		{
			if (CommonSettings.blnAutoBackUpWhileClosing)
			{
				frm = new ClosingDialog();
				frm.FormClosing += Frm_FormClosing1;
				frm.ShowDialog();
				return;
			}
			MyMsgBox myMsgBox = new MyMsgBox();
			myMsgBox.Passvalue = "Closing";
			if (myMsgBox.ShowDialog() == DialogResult.OK)
			{
				AppExitMethod();
			}
		}

		private void Frm_FormClosing1(object sender, FormClosingEventArgs e)
		{
			if (frm.sValue == "Yes")
			{
				AutoBackUpMethod();
			}
			else if (frm.sValue == "No")
			{
				AppExitMethod();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void Main_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.N && e.Alt)
			{
				base.WindowState = FormWindowState.Minimized;
			}
			else if (e.KeyCode == Keys.F2 && CommonSettings.blnShowTransWitInvtry)
			{
				transactionWithoutInventoryToolStripMenuItem_Click(null, null);
			}
			else if (e.KeyCode == Keys.Z && e.Alt && e.Control)
			{
				dashboardThemeToolStripMenuItem_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt && e.Control)
			{
				string text = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
				if (text == AniHelper.PWordGeneration())
				{
					SettingsView settingsView = new SettingsView();
					OpenForm(settingsView);
				}
			}
		}

		private void customThemeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				dlg = new OpenFileDialog();
				dlg.Title = "Open Image";
				dlg.Filter = "All Picture Files(*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg)|*.jpg;*.png;*.ico;*.bmp;*.gif";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					BackgroundImage = Image.FromFile(dlg.FileName);
					BackgroundImageLayout = ImageLayout.Stretch;
					imgPath = dlg.FileName;
					Settings.Default.strBackImageName = imgPath;
					Settings.Default.Save();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				dlg.Dispose();
			}
		}

		private void tsbLockApplication_Click(object sender, EventArgs e)
		{
			DisableControls();
			lockapplication = new LockApplication();
			lockapplication.MdiParent = this;
			lockapplication.FormClosing += Lockapplication_FormClosing;
			lockapplication.Show();
		}

		private void tsmCustomerReport_Click(object sender, EventArgs e)
		{
			Customers customers = new Customers();
			customers.mainForm = this;
			OpenForm(customers);
		}

		private void tsmSalesReport_Click(object sender, EventArgs e)
		{
			Sales sales = new Sales();
			sales.mainForm = this;
			OpenForm(sales);
		}

		private void tsbCustomerReport_Click(object sender, EventArgs e)
		{
			POSReportView pOSReportView = new POSReportView();
			Utility.iReportFormNumber = 23;
			OpenForm(pOSReportView);
		}

		private void tsbSupplierReport_Click(object sender, EventArgs e)
		{
			POSReportView pOSReportView = new POSReportView();
			Utility.iReportFormNumber = 30;
			OpenForm(pOSReportView);
		}

		private void tsbVatReport_Click(object sender, EventArgs e)
		{
			POSReportView pOSReportView = new POSReportView();
			Utility.iReportFormNumber = 35;
			OpenForm(pOSReportView);
		}

		private void tsmInventoryReport_Click(object sender, EventArgs e)
		{
			InventoryReport inventoryReport = new InventoryReport();
			inventoryReport.mainForm = this;
			OpenForm(inventoryReport);
		}

		private void tsmSupplierReport_Click(object sender, EventArgs e)
		{
			Supplier supplier = new Supplier();
			supplier.mainForm = this;
			OpenForm(supplier);
		}

		private async void tsmUpdate_Click(object sender, EventArgs e)
		{
			await ActivationHelper.UpdationTask();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firmDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.lockApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuParty = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExpenseAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBank = new System.Windows.Forms.ToolStripMenuItem();
            this.productGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.godownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priceCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBranch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.salesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estimateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionWithoutInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFirstPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.packingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stockTransferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.damageEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockAdjusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsPriceSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productCombainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productBarcodeDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.manufacturingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashReceiptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankReceptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.journalEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openingBalanceEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebitNotetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreditNotetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postDatedChequeReceivableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postDatedChequePayableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankReconciliationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptAndPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.priceCategoryWiseSalesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootwiseSalesReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.salesmanWiseSalesReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.partyWiseSalesReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupWisePurchaseReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.partywisePurhaseReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exicutiveWiseCollectionReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pendingOrderReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReturnSummaryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseSummaryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReturnSummaryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesAndSalesReturnReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseAndPurchaseReturnReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesEstimateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReturnReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseReturnReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesOrderReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesQuotationReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrderReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.partyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountPayableAndReceivableReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootwiseCollectionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomerReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSupplierReport = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItmWiseSlsRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItmWisePurRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.productLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockTransferEportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvntrySlsRtnRgst = new System.Windows.Forms.ToolStripMenuItem();
            this.inventorySalesRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.damageStockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvntryPurRgstr = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvntryPurRtnRgstr = new System.Windows.Forms.ToolStripMenuItem();
            this.productFlowReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsoldProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.godownStockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInventoryReport = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.journalRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trialBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incomeAndExpenditureReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balanceSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputVatReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputVatReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productWiseGSTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTaxReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.staffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salaryProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attendanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleAndPrevilageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuExportData = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuImportData = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billGenetrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customTransactionAndReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.dayCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.qRCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRateFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLabelScale = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notepadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoTutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBuggsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSale = new System.Windows.Forms.ToolStripButton();
            this.tsbPurchase = new System.Windows.Forms.ToolStripButton();
            this.tsbProduct = new System.Windows.Forms.ToolStripButton();
            this.tsbPaymentAndReceipt = new System.Windows.Forms.ToolStripButton();
            this.tsbExpense = new System.Windows.Forms.ToolStripButton();
            this.tsbStock = new System.Windows.Forms.ToolStripButton();
            this.tsbLedger = new System.Windows.Forms.ToolStripButton();
            this.tsbCustomerReport = new System.Windows.Forms.ToolStripButton();
            this.tsbSupplierReport = new System.Windows.Forms.ToolStripButton();
            this.tsbVatReport = new System.Windows.Forms.ToolStripButton();
            this.tsmDayClose = new System.Windows.Forms.ToolStripButton();
            this.tsmCalculator = new System.Windows.Forms.ToolStripButton();
            this.tsbCalendar = new System.Windows.Forms.ToolStripButton();
            this.tsbLockApplication = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.lblPendingPeriod = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administrationToolStripMenuItem,
            this.mastersToolStripMenuItem,
            this.salesToolStripMenuItem,
            this.purchaseToolStripMenuItem,
            this.packingToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.accountReportsToolStripMenuItem,
            this.inventoryReportsToolStripMenuItem,
            this.accountsReportsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.staffToolStripMenuItem,
            this.userToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Name = "menuStrip1";
            this.toolTip1.SetToolTip(this.menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // administrationToolStripMenuItem
            // 
            resources.ApplyResources(this.administrationToolStripMenuItem, "administrationToolStripMenuItem");
            this.administrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminSettingsToolStripMenuItem,
            this.firmDetailsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.lockApplicationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            // 
            // adminSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.adminSettingsToolStripMenuItem, "adminSettingsToolStripMenuItem");
            this.adminSettingsToolStripMenuItem.Name = "adminSettingsToolStripMenuItem";
            this.adminSettingsToolStripMenuItem.Click += new System.EventHandler(this.adminSettingsToolStripMenuItem_Click);
            // 
            // firmDetailsToolStripMenuItem
            // 
            resources.ApplyResources(this.firmDetailsToolStripMenuItem, "firmDetailsToolStripMenuItem");
            this.firmDetailsToolStripMenuItem.Name = "firmDetailsToolStripMenuItem";
            this.firmDetailsToolStripMenuItem.Click += new System.EventHandler(this.firmDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // lockApplicationToolStripMenuItem
            // 
            resources.ApplyResources(this.lockApplicationToolStripMenuItem, "lockApplicationToolStripMenuItem");
            this.lockApplicationToolStripMenuItem.Name = "lockApplicationToolStripMenuItem";
            this.lockApplicationToolStripMenuItem.Click += new System.EventHandler(this.lockApplicationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mastersToolStripMenuItem
            // 
            resources.ApplyResources(this.mastersToolStripMenuItem, "mastersToolStripMenuItem");
            this.mastersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsGroupToolStripMenuItem,
            this.accountLedgerToolStripMenuItem,
            this.menuParty,
            this.menuSupplier,
            this.menuExpenseAccount,
            this.menuBank,
            this.productGroupToolStripMenuItem,
            this.productToolStripMenuItem,
            this.brandToolStripMenuItem,
            this.unitToolStripMenuItem,
            this.godownToolStripMenuItem,
            this.selfToolStripMenuItem,
            this.taxCategoryToolStripMenuItem,
            this.priceCategoryToolStripMenuItem,
            this.rootToolStripMenuItem,
            this.vehicleToolStripMenuItem,
            this.tsmBranch,
            this.tsmBatch,
            this.tsmCurrency});
            this.mastersToolStripMenuItem.Name = "mastersToolStripMenuItem";
            // 
            // accountsGroupToolStripMenuItem
            // 
            resources.ApplyResources(this.accountsGroupToolStripMenuItem, "accountsGroupToolStripMenuItem");
            this.accountsGroupToolStripMenuItem.Name = "accountsGroupToolStripMenuItem";
            this.accountsGroupToolStripMenuItem.Click += new System.EventHandler(this.accountsGroupToolStripMenuItem_Click);
            // 
            // accountLedgerToolStripMenuItem
            // 
            resources.ApplyResources(this.accountLedgerToolStripMenuItem, "accountLedgerToolStripMenuItem");
            this.accountLedgerToolStripMenuItem.Name = "accountLedgerToolStripMenuItem";
            this.accountLedgerToolStripMenuItem.Click += new System.EventHandler(this.accountLedgerToolStripMenuItem_Click);
            // 
            // menuParty
            // 
            resources.ApplyResources(this.menuParty, "menuParty");
            this.menuParty.Name = "menuParty";
            this.menuParty.Click += new System.EventHandler(this.menuParty_Click);
            // 
            // menuSupplier
            // 
            resources.ApplyResources(this.menuSupplier, "menuSupplier");
            this.menuSupplier.Name = "menuSupplier";
            this.menuSupplier.Click += new System.EventHandler(this.menuSupplier_Click);
            // 
            // menuExpenseAccount
            // 
            resources.ApplyResources(this.menuExpenseAccount, "menuExpenseAccount");
            this.menuExpenseAccount.Name = "menuExpenseAccount";
            this.menuExpenseAccount.Click += new System.EventHandler(this.menuExpenseAccount_Click);
            // 
            // menuBank
            // 
            resources.ApplyResources(this.menuBank, "menuBank");
            this.menuBank.Name = "menuBank";
            this.menuBank.Click += new System.EventHandler(this.menuBank_Click);
            // 
            // productGroupToolStripMenuItem
            // 
            resources.ApplyResources(this.productGroupToolStripMenuItem, "productGroupToolStripMenuItem");
            this.productGroupToolStripMenuItem.Name = "productGroupToolStripMenuItem";
            this.productGroupToolStripMenuItem.Click += new System.EventHandler(this.productGroupToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem
            // 
            resources.ApplyResources(this.productToolStripMenuItem, "productToolStripMenuItem");
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Click += new System.EventHandler(this.productToolStripMenuItem_Click);
            // 
            // brandToolStripMenuItem
            // 
            resources.ApplyResources(this.brandToolStripMenuItem, "brandToolStripMenuItem");
            this.brandToolStripMenuItem.Name = "brandToolStripMenuItem";
            this.brandToolStripMenuItem.Click += new System.EventHandler(this.brandToolStripMenuItem_Click);
            // 
            // unitToolStripMenuItem
            // 
            resources.ApplyResources(this.unitToolStripMenuItem, "unitToolStripMenuItem");
            this.unitToolStripMenuItem.Name = "unitToolStripMenuItem";
            this.unitToolStripMenuItem.Click += new System.EventHandler(this.unitToolStripMenuItem_Click);
            // 
            // godownToolStripMenuItem
            // 
            resources.ApplyResources(this.godownToolStripMenuItem, "godownToolStripMenuItem");
            this.godownToolStripMenuItem.Name = "godownToolStripMenuItem";
            this.godownToolStripMenuItem.Click += new System.EventHandler(this.godownToolStripMenuItem_Click);
            // 
            // selfToolStripMenuItem
            // 
            resources.ApplyResources(this.selfToolStripMenuItem, "selfToolStripMenuItem");
            this.selfToolStripMenuItem.Name = "selfToolStripMenuItem";
            this.selfToolStripMenuItem.Click += new System.EventHandler(this.selfToolStripMenuItem_Click);
            // 
            // taxCategoryToolStripMenuItem
            // 
            resources.ApplyResources(this.taxCategoryToolStripMenuItem, "taxCategoryToolStripMenuItem");
            this.taxCategoryToolStripMenuItem.Name = "taxCategoryToolStripMenuItem";
            this.taxCategoryToolStripMenuItem.Click += new System.EventHandler(this.taxCategoryToolStripMenuItem_Click);
            // 
            // priceCategoryToolStripMenuItem
            // 
            resources.ApplyResources(this.priceCategoryToolStripMenuItem, "priceCategoryToolStripMenuItem");
            this.priceCategoryToolStripMenuItem.Name = "priceCategoryToolStripMenuItem";
            this.priceCategoryToolStripMenuItem.Click += new System.EventHandler(this.priceCategoryToolStripMenuItem_Click);
            // 
            // rootToolStripMenuItem
            // 
            resources.ApplyResources(this.rootToolStripMenuItem, "rootToolStripMenuItem");
            this.rootToolStripMenuItem.Name = "rootToolStripMenuItem";
            this.rootToolStripMenuItem.Click += new System.EventHandler(this.rootToolStripMenuItem_Click);
            // 
            // vehicleToolStripMenuItem
            // 
            resources.ApplyResources(this.vehicleToolStripMenuItem, "vehicleToolStripMenuItem");
            this.vehicleToolStripMenuItem.Name = "vehicleToolStripMenuItem";
            this.vehicleToolStripMenuItem.Click += new System.EventHandler(this.vehicleToolStripMenuItem_Click);
            // 
            // tsmBranch
            // 
            resources.ApplyResources(this.tsmBranch, "tsmBranch");
            this.tsmBranch.Name = "tsmBranch";
            this.tsmBranch.Click += new System.EventHandler(this.tsmBranch_Click);
            // 
            // tsmBatch
            // 
            resources.ApplyResources(this.tsmBatch, "tsmBatch");
            this.tsmBatch.Name = "tsmBatch";
            this.tsmBatch.Click += new System.EventHandler(this.tsmBatch_Click);
            // 
            // tsmCurrency
            // 
            resources.ApplyResources(this.tsmCurrency, "tsmCurrency");
            this.tsmCurrency.Name = "tsmCurrency";
            this.tsmCurrency.Click += new System.EventHandler(this.tsmCurrency_Click);
            // 
            // salesToolStripMenuItem
            // 
            resources.ApplyResources(this.salesToolStripMenuItem, "salesToolStripMenuItem");
            this.salesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceToolStripMenuItem,
            this.estimateToolStripMenuItem,
            this.orderToolStripMenuItem,
            this.quotationToolStripMenuItem,
            this.returnToolStripMenuItem,
            this.billPrintToolStripMenuItem,
            this.salesViewToolStripMenuItem,
            this.transactionWithoutInventoryToolStripMenuItem,
            this.tsmSalesReport});
            this.salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            // 
            // invoiceToolStripMenuItem
            // 
            resources.ApplyResources(this.invoiceToolStripMenuItem, "invoiceToolStripMenuItem");
            this.invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            this.invoiceToolStripMenuItem.Click += new System.EventHandler(this.invoiceToolStripMenuItem_Click);
            // 
            // estimateToolStripMenuItem
            // 
            resources.ApplyResources(this.estimateToolStripMenuItem, "estimateToolStripMenuItem");
            this.estimateToolStripMenuItem.Name = "estimateToolStripMenuItem";
            this.estimateToolStripMenuItem.Click += new System.EventHandler(this.estimateToolStripMenuItem_Click);
            // 
            // orderToolStripMenuItem
            // 
            resources.ApplyResources(this.orderToolStripMenuItem, "orderToolStripMenuItem");
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            this.orderToolStripMenuItem.Click += new System.EventHandler(this.orderToolStripMenuItem_Click);
            // 
            // quotationToolStripMenuItem
            // 
            resources.ApplyResources(this.quotationToolStripMenuItem, "quotationToolStripMenuItem");
            this.quotationToolStripMenuItem.Name = "quotationToolStripMenuItem";
            this.quotationToolStripMenuItem.Click += new System.EventHandler(this.quotationToolStripMenuItem_Click);
            // 
            // returnToolStripMenuItem
            // 
            resources.ApplyResources(this.returnToolStripMenuItem, "returnToolStripMenuItem");
            this.returnToolStripMenuItem.Name = "returnToolStripMenuItem";
            this.returnToolStripMenuItem.Click += new System.EventHandler(this.returnToolStripMenuItem_Click);
            // 
            // billPrintToolStripMenuItem
            // 
            resources.ApplyResources(this.billPrintToolStripMenuItem, "billPrintToolStripMenuItem");
            this.billPrintToolStripMenuItem.Name = "billPrintToolStripMenuItem";
            this.billPrintToolStripMenuItem.Click += new System.EventHandler(this.billPrintToolStripMenuItem_Click);
            // 
            // salesViewToolStripMenuItem
            // 
            resources.ApplyResources(this.salesViewToolStripMenuItem, "salesViewToolStripMenuItem");
            this.salesViewToolStripMenuItem.Name = "salesViewToolStripMenuItem";
            this.salesViewToolStripMenuItem.Click += new System.EventHandler(this.salesViewToolStripMenuItem_Click);
            // 
            // transactionWithoutInventoryToolStripMenuItem
            // 
            resources.ApplyResources(this.transactionWithoutInventoryToolStripMenuItem, "transactionWithoutInventoryToolStripMenuItem");
            this.transactionWithoutInventoryToolStripMenuItem.Name = "transactionWithoutInventoryToolStripMenuItem";
            this.transactionWithoutInventoryToolStripMenuItem.Click += new System.EventHandler(this.transactionWithoutInventoryToolStripMenuItem_Click);
            // 
            // tsmSalesReport
            // 
            resources.ApplyResources(this.tsmSalesReport, "tsmSalesReport");
            this.tsmSalesReport.Name = "tsmSalesReport";
            this.tsmSalesReport.Click += new System.EventHandler(this.tsmSalesReport_Click);
            // 
            // purchaseToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseToolStripMenuItem, "purchaseToolStripMenuItem");
            this.purchaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceToolStripMenuItem1,
            this.tsmiFirstPurchase,
            this.returnToolStripMenuItem1,
            this.orderToolStripMenuItem1});
            this.purchaseToolStripMenuItem.Name = "purchaseToolStripMenuItem";
            // 
            // invoiceToolStripMenuItem1
            // 
            resources.ApplyResources(this.invoiceToolStripMenuItem1, "invoiceToolStripMenuItem1");
            this.invoiceToolStripMenuItem1.Name = "invoiceToolStripMenuItem1";
            this.invoiceToolStripMenuItem1.Click += new System.EventHandler(this.invoiceToolStripMenuItem1_Click);
            // 
            // tsmiFirstPurchase
            // 
            resources.ApplyResources(this.tsmiFirstPurchase, "tsmiFirstPurchase");
            this.tsmiFirstPurchase.Name = "tsmiFirstPurchase";
            this.tsmiFirstPurchase.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // returnToolStripMenuItem1
            // 
            resources.ApplyResources(this.returnToolStripMenuItem1, "returnToolStripMenuItem1");
            this.returnToolStripMenuItem1.Name = "returnToolStripMenuItem1";
            this.returnToolStripMenuItem1.Click += new System.EventHandler(this.returnToolStripMenuItem1_Click);
            // 
            // orderToolStripMenuItem1
            // 
            resources.ApplyResources(this.orderToolStripMenuItem1, "orderToolStripMenuItem1");
            this.orderToolStripMenuItem1.Name = "orderToolStripMenuItem1";
            this.orderToolStripMenuItem1.Click += new System.EventHandler(this.orderToolStripMenuItem1_Click);
            // 
            // packingToolStripMenuItem
            // 
            resources.ApplyResources(this.packingToolStripMenuItem, "packingToolStripMenuItem");
            this.packingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packingToolStripMenuItem1,
            this.stockTransferToolStripMenuItem,
            this.damageEntryToolStripMenuItem,
            this.stockAdjusterToolStripMenuItem,
            this.itemsPriceSettingsToolStripMenuItem,
            this.productCombainToolStripMenuItem,
            this.productBarcodeDesignToolStripMenuItem,
            this.toolStripMenuItem3,
            this.manufacturingToolStripMenuItem});
            this.packingToolStripMenuItem.Name = "packingToolStripMenuItem";
            // 
            // packingToolStripMenuItem1
            // 
            resources.ApplyResources(this.packingToolStripMenuItem1, "packingToolStripMenuItem1");
            this.packingToolStripMenuItem1.Name = "packingToolStripMenuItem1";
            this.packingToolStripMenuItem1.Click += new System.EventHandler(this.packingToolStripMenuItem1_Click);
            // 
            // stockTransferToolStripMenuItem
            // 
            resources.ApplyResources(this.stockTransferToolStripMenuItem, "stockTransferToolStripMenuItem");
            this.stockTransferToolStripMenuItem.Name = "stockTransferToolStripMenuItem";
            this.stockTransferToolStripMenuItem.Click += new System.EventHandler(this.stockTransferToolStripMenuItem_Click);
            // 
            // damageEntryToolStripMenuItem
            // 
            resources.ApplyResources(this.damageEntryToolStripMenuItem, "damageEntryToolStripMenuItem");
            this.damageEntryToolStripMenuItem.Name = "damageEntryToolStripMenuItem";
            this.damageEntryToolStripMenuItem.Click += new System.EventHandler(this.damageEntryToolStripMenuItem_Click);
            // 
            // stockAdjusterToolStripMenuItem
            // 
            resources.ApplyResources(this.stockAdjusterToolStripMenuItem, "stockAdjusterToolStripMenuItem");
            this.stockAdjusterToolStripMenuItem.Name = "stockAdjusterToolStripMenuItem";
            this.stockAdjusterToolStripMenuItem.Click += new System.EventHandler(this.stockAdjusterToolStripMenuItem_Click);
            // 
            // itemsPriceSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.itemsPriceSettingsToolStripMenuItem, "itemsPriceSettingsToolStripMenuItem");
            this.itemsPriceSettingsToolStripMenuItem.Name = "itemsPriceSettingsToolStripMenuItem";
            this.itemsPriceSettingsToolStripMenuItem.Click += new System.EventHandler(this.itemsPriceSettingsToolStripMenuItem_Click);
            // 
            // productCombainToolStripMenuItem
            // 
            resources.ApplyResources(this.productCombainToolStripMenuItem, "productCombainToolStripMenuItem");
            this.productCombainToolStripMenuItem.Name = "productCombainToolStripMenuItem";
            this.productCombainToolStripMenuItem.Click += new System.EventHandler(this.productCombainToolStripMenuItem_Click);
            // 
            // productBarcodeDesignToolStripMenuItem
            // 
            resources.ApplyResources(this.productBarcodeDesignToolStripMenuItem, "productBarcodeDesignToolStripMenuItem");
            this.productBarcodeDesignToolStripMenuItem.Name = "productBarcodeDesignToolStripMenuItem";
            this.productBarcodeDesignToolStripMenuItem.Click += new System.EventHandler(this.productBarcodeDesignToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // manufacturingToolStripMenuItem
            // 
            resources.ApplyResources(this.manufacturingToolStripMenuItem, "manufacturingToolStripMenuItem");
            this.manufacturingToolStripMenuItem.Name = "manufacturingToolStripMenuItem";
            this.manufacturingToolStripMenuItem.Click += new System.EventHandler(this.manufacturingToolStripMenuItem_Click);
            // 
            // accountsToolStripMenuItem
            // 
            resources.ApplyResources(this.accountsToolStripMenuItem, "accountsToolStripMenuItem");
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cashPaymentToolStripMenuItem,
            this.cashReceiptsToolStripMenuItem,
            this.bankPaymentToolStripMenuItem,
            this.bankReceptsToolStripMenuItem,
            this.journalEntriesToolStripMenuItem,
            this.openingBalanceEntryToolStripMenuItem,
            this.DebitNotetoolStripMenuItem,
            this.CreditNotetoolStripMenuItem,
            this.postDatedChequeReceivableToolStripMenuItem,
            this.postDatedChequePayableToolStripMenuItem,
            this.bankReconciliationToolStripMenuItem,
            this.receiptAndPaymentToolStripMenuItem,
            this.bulkReceiptToolStripMenuItem});
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            // 
            // cashPaymentToolStripMenuItem
            // 
            resources.ApplyResources(this.cashPaymentToolStripMenuItem, "cashPaymentToolStripMenuItem");
            this.cashPaymentToolStripMenuItem.Name = "cashPaymentToolStripMenuItem";
            this.cashPaymentToolStripMenuItem.Click += new System.EventHandler(this.cashPaymentToolStripMenuItem_Click);
            // 
            // cashReceiptsToolStripMenuItem
            // 
            resources.ApplyResources(this.cashReceiptsToolStripMenuItem, "cashReceiptsToolStripMenuItem");
            this.cashReceiptsToolStripMenuItem.Name = "cashReceiptsToolStripMenuItem";
            this.cashReceiptsToolStripMenuItem.Click += new System.EventHandler(this.cashReceiptsToolStripMenuItem_Click);
            // 
            // bankPaymentToolStripMenuItem
            // 
            resources.ApplyResources(this.bankPaymentToolStripMenuItem, "bankPaymentToolStripMenuItem");
            this.bankPaymentToolStripMenuItem.Name = "bankPaymentToolStripMenuItem";
            this.bankPaymentToolStripMenuItem.Click += new System.EventHandler(this.bankPaymentToolStripMenuItem_Click);
            // 
            // bankReceptsToolStripMenuItem
            // 
            resources.ApplyResources(this.bankReceptsToolStripMenuItem, "bankReceptsToolStripMenuItem");
            this.bankReceptsToolStripMenuItem.Name = "bankReceptsToolStripMenuItem";
            this.bankReceptsToolStripMenuItem.Click += new System.EventHandler(this.bankReceptsToolStripMenuItem_Click);
            // 
            // journalEntriesToolStripMenuItem
            // 
            resources.ApplyResources(this.journalEntriesToolStripMenuItem, "journalEntriesToolStripMenuItem");
            this.journalEntriesToolStripMenuItem.Name = "journalEntriesToolStripMenuItem";
            this.journalEntriesToolStripMenuItem.Click += new System.EventHandler(this.journalEntriesToolStripMenuItem_Click);
            // 
            // openingBalanceEntryToolStripMenuItem
            // 
            resources.ApplyResources(this.openingBalanceEntryToolStripMenuItem, "openingBalanceEntryToolStripMenuItem");
            this.openingBalanceEntryToolStripMenuItem.Name = "openingBalanceEntryToolStripMenuItem";
            this.openingBalanceEntryToolStripMenuItem.Click += new System.EventHandler(this.openingBalanceEntryToolStripMenuItem_Click);
            // 
            // DebitNotetoolStripMenuItem
            // 
            resources.ApplyResources(this.DebitNotetoolStripMenuItem, "DebitNotetoolStripMenuItem");
            this.DebitNotetoolStripMenuItem.Name = "DebitNotetoolStripMenuItem";
            this.DebitNotetoolStripMenuItem.Click += new System.EventHandler(this.DebitNotetoolStripMenuItem_Click);
            // 
            // CreditNotetoolStripMenuItem
            // 
            resources.ApplyResources(this.CreditNotetoolStripMenuItem, "CreditNotetoolStripMenuItem");
            this.CreditNotetoolStripMenuItem.Name = "CreditNotetoolStripMenuItem";
            this.CreditNotetoolStripMenuItem.Click += new System.EventHandler(this.CreditNotetoolStripMenuItem_Click);
            // 
            // postDatedChequeReceivableToolStripMenuItem
            // 
            resources.ApplyResources(this.postDatedChequeReceivableToolStripMenuItem, "postDatedChequeReceivableToolStripMenuItem");
            this.postDatedChequeReceivableToolStripMenuItem.Name = "postDatedChequeReceivableToolStripMenuItem";
            this.postDatedChequeReceivableToolStripMenuItem.Click += new System.EventHandler(this.postDatedChequeReceivableToolStripMenuItem_Click);
            // 
            // postDatedChequePayableToolStripMenuItem
            // 
            resources.ApplyResources(this.postDatedChequePayableToolStripMenuItem, "postDatedChequePayableToolStripMenuItem");
            this.postDatedChequePayableToolStripMenuItem.Name = "postDatedChequePayableToolStripMenuItem";
            this.postDatedChequePayableToolStripMenuItem.Click += new System.EventHandler(this.postDatedChequePayableToolStripMenuItem_Click);
            // 
            // bankReconciliationToolStripMenuItem
            // 
            resources.ApplyResources(this.bankReconciliationToolStripMenuItem, "bankReconciliationToolStripMenuItem");
            this.bankReconciliationToolStripMenuItem.Name = "bankReconciliationToolStripMenuItem";
            this.bankReconciliationToolStripMenuItem.Click += new System.EventHandler(this.bankReconciliationToolStripMenuItem_Click);
            // 
            // receiptAndPaymentToolStripMenuItem
            // 
            resources.ApplyResources(this.receiptAndPaymentToolStripMenuItem, "receiptAndPaymentToolStripMenuItem");
            this.receiptAndPaymentToolStripMenuItem.Name = "receiptAndPaymentToolStripMenuItem";
            this.receiptAndPaymentToolStripMenuItem.Click += new System.EventHandler(this.receiptAndPaymentToolStripMenuItem_Click);
            // 
            // bulkReceiptToolStripMenuItem
            // 
            resources.ApplyResources(this.bulkReceiptToolStripMenuItem, "bulkReceiptToolStripMenuItem");
            this.bulkReceiptToolStripMenuItem.Name = "bulkReceiptToolStripMenuItem";
            this.bulkReceiptToolStripMenuItem.Click += new System.EventHandler(this.bulkReceiptToolStripMenuItem_Click);
            // 
            // accountReportsToolStripMenuItem
            // 
            resources.ApplyResources(this.accountReportsToolStripMenuItem, "accountReportsToolStripMenuItem");
            this.accountReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem8,
            this.salesAndSalesReturnReportToolStripMenuItem,
            this.purchaseAndPurchaseReturnReportToolStripMenuItem,
            this.salesEstimateReportToolStripMenuItem,
            this.salesReportToolStripMenuItem,
            this.salesReturnReportToolStripMenuItem,
            this.purchaseReportToolStripMenuItem,
            this.purchaseReturnReportToolStripMenuItem,
            this.salesOrderReportToolStripMenuItem,
            this.salesQuotationReportToolStripMenuItem,
            this.purchaseOrderReportToolStripMenuItem,
            this.toolStripMenuItem7,
            this.partyListToolStripMenuItem,
            this.accountPayableAndReceivableReportToolStripMenuItem,
            this.rootwiseCollectionReportToolStripMenuItem,
            this.transactionReportToolStripMenuItem,
            this.otherReportsToolStripMenuItem,
            this.tsmCustomerReport,
            this.tsmSupplierReport});
            this.accountReportsToolStripMenuItem.Name = "accountReportsToolStripMenuItem";
            // 
            // toolStripMenuItem8
            // 
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.priceCategoryWiseSalesReportToolStripMenuItem,
            this.rootwiseSalesReportToolStripMenuItem1,
            this.salesmanWiseSalesReportToolStripMenuItem1,
            this.partyWiseSalesReportToolStripMenuItem1,
            this.groupWisePurchaseReportToolStripMenuItem1,
            this.partywisePurhaseReportToolStripMenuItem1,
            this.exicutiveWiseCollectionReportToolStripMenuItem1,
            this.pendingOrderReportToolStripMenuItem,
            this.summaryReportToolStripMenuItem,
            this.salesReturnSummaryReportToolStripMenuItem,
            this.purchaseSummaryReportToolStripMenuItem,
            this.purchaseReturnSummaryReportToolStripMenuItem});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            // 
            // toolStripMenuItem9
            // 
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // priceCategoryWiseSalesReportToolStripMenuItem
            // 
            resources.ApplyResources(this.priceCategoryWiseSalesReportToolStripMenuItem, "priceCategoryWiseSalesReportToolStripMenuItem");
            this.priceCategoryWiseSalesReportToolStripMenuItem.Name = "priceCategoryWiseSalesReportToolStripMenuItem";
            this.priceCategoryWiseSalesReportToolStripMenuItem.Click += new System.EventHandler(this.priceCategoryWiseSalesReportToolStripMenuItem_Click);
            // 
            // rootwiseSalesReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.rootwiseSalesReportToolStripMenuItem1, "rootwiseSalesReportToolStripMenuItem1");
            this.rootwiseSalesReportToolStripMenuItem1.Name = "rootwiseSalesReportToolStripMenuItem1";
            this.rootwiseSalesReportToolStripMenuItem1.Click += new System.EventHandler(this.rootwiseSalesReportToolStripMenuItem1_Click);
            // 
            // salesmanWiseSalesReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.salesmanWiseSalesReportToolStripMenuItem1, "salesmanWiseSalesReportToolStripMenuItem1");
            this.salesmanWiseSalesReportToolStripMenuItem1.Name = "salesmanWiseSalesReportToolStripMenuItem1";
            this.salesmanWiseSalesReportToolStripMenuItem1.Click += new System.EventHandler(this.salesmanWiseSalesReportToolStripMenuItem1_Click);
            // 
            // partyWiseSalesReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.partyWiseSalesReportToolStripMenuItem1, "partyWiseSalesReportToolStripMenuItem1");
            this.partyWiseSalesReportToolStripMenuItem1.Name = "partyWiseSalesReportToolStripMenuItem1";
            this.partyWiseSalesReportToolStripMenuItem1.Click += new System.EventHandler(this.partyWiseSalesReportToolStripMenuItem1_Click);
            // 
            // groupWisePurchaseReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.groupWisePurchaseReportToolStripMenuItem1, "groupWisePurchaseReportToolStripMenuItem1");
            this.groupWisePurchaseReportToolStripMenuItem1.Name = "groupWisePurchaseReportToolStripMenuItem1";
            this.groupWisePurchaseReportToolStripMenuItem1.Click += new System.EventHandler(this.groupWisePurchaseReportToolStripMenuItem1_Click);
            // 
            // partywisePurhaseReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.partywisePurhaseReportToolStripMenuItem1, "partywisePurhaseReportToolStripMenuItem1");
            this.partywisePurhaseReportToolStripMenuItem1.Name = "partywisePurhaseReportToolStripMenuItem1";
            this.partywisePurhaseReportToolStripMenuItem1.Click += new System.EventHandler(this.partywisePurhaseReportToolStripMenuItem1_Click);
            // 
            // exicutiveWiseCollectionReportToolStripMenuItem1
            // 
            resources.ApplyResources(this.exicutiveWiseCollectionReportToolStripMenuItem1, "exicutiveWiseCollectionReportToolStripMenuItem1");
            this.exicutiveWiseCollectionReportToolStripMenuItem1.Name = "exicutiveWiseCollectionReportToolStripMenuItem1";
            this.exicutiveWiseCollectionReportToolStripMenuItem1.Click += new System.EventHandler(this.exicutiveWiseCollectionReportToolStripMenuItem1_Click);
            // 
            // pendingOrderReportToolStripMenuItem
            // 
            resources.ApplyResources(this.pendingOrderReportToolStripMenuItem, "pendingOrderReportToolStripMenuItem");
            this.pendingOrderReportToolStripMenuItem.Name = "pendingOrderReportToolStripMenuItem";
            this.pendingOrderReportToolStripMenuItem.Click += new System.EventHandler(this.pendingOrderReportToolStripMenuItem_Click);
            // 
            // summaryReportToolStripMenuItem
            // 
            resources.ApplyResources(this.summaryReportToolStripMenuItem, "summaryReportToolStripMenuItem");
            this.summaryReportToolStripMenuItem.Name = "summaryReportToolStripMenuItem";
            this.summaryReportToolStripMenuItem.Click += new System.EventHandler(this.summaryReportToolStripMenuItem_Click);
            // 
            // salesReturnSummaryReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesReturnSummaryReportToolStripMenuItem, "salesReturnSummaryReportToolStripMenuItem");
            this.salesReturnSummaryReportToolStripMenuItem.Name = "salesReturnSummaryReportToolStripMenuItem";
            this.salesReturnSummaryReportToolStripMenuItem.Click += new System.EventHandler(this.salesReturnSummaryReportToolStripMenuItem_Click);
            // 
            // purchaseSummaryReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseSummaryReportToolStripMenuItem, "purchaseSummaryReportToolStripMenuItem");
            this.purchaseSummaryReportToolStripMenuItem.Name = "purchaseSummaryReportToolStripMenuItem";
            this.purchaseSummaryReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseSummaryReportToolStripMenuItem_Click);
            // 
            // purchaseReturnSummaryReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseReturnSummaryReportToolStripMenuItem, "purchaseReturnSummaryReportToolStripMenuItem");
            this.purchaseReturnSummaryReportToolStripMenuItem.Name = "purchaseReturnSummaryReportToolStripMenuItem";
            this.purchaseReturnSummaryReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReturnSummaryReportToolStripMenuItem_Click);
            // 
            // salesAndSalesReturnReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesAndSalesReturnReportToolStripMenuItem, "salesAndSalesReturnReportToolStripMenuItem");
            this.salesAndSalesReturnReportToolStripMenuItem.Name = "salesAndSalesReturnReportToolStripMenuItem";
            this.salesAndSalesReturnReportToolStripMenuItem.Click += new System.EventHandler(this.salesAndSalesReturnReportToolStripMenuItem_Click);
            // 
            // purchaseAndPurchaseReturnReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseAndPurchaseReturnReportToolStripMenuItem, "purchaseAndPurchaseReturnReportToolStripMenuItem");
            this.purchaseAndPurchaseReturnReportToolStripMenuItem.Name = "purchaseAndPurchaseReturnReportToolStripMenuItem";
            this.purchaseAndPurchaseReturnReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseAndPurchaseReturnReportToolStripMenuItem_Click);
            // 
            // salesEstimateReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesEstimateReportToolStripMenuItem, "salesEstimateReportToolStripMenuItem");
            this.salesEstimateReportToolStripMenuItem.Name = "salesEstimateReportToolStripMenuItem";
            this.salesEstimateReportToolStripMenuItem.Click += new System.EventHandler(this.salesEstimateReportToolStripMenuItem_Click);
            // 
            // salesReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesReportToolStripMenuItem, "salesReportToolStripMenuItem");
            this.salesReportToolStripMenuItem.Name = "salesReportToolStripMenuItem";
            this.salesReportToolStripMenuItem.Click += new System.EventHandler(this.salesReportToolStripMenuItem_Click);
            // 
            // salesReturnReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesReturnReportToolStripMenuItem, "salesReturnReportToolStripMenuItem");
            this.salesReturnReportToolStripMenuItem.Name = "salesReturnReportToolStripMenuItem";
            this.salesReturnReportToolStripMenuItem.Click += new System.EventHandler(this.salesReturnReportToolStripMenuItem_Click);
            // 
            // purchaseReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseReportToolStripMenuItem, "purchaseReportToolStripMenuItem");
            this.purchaseReportToolStripMenuItem.Name = "purchaseReportToolStripMenuItem";
            this.purchaseReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReportToolStripMenuItem_Click);
            // 
            // purchaseReturnReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseReturnReportToolStripMenuItem, "purchaseReturnReportToolStripMenuItem");
            this.purchaseReturnReportToolStripMenuItem.Name = "purchaseReturnReportToolStripMenuItem";
            this.purchaseReturnReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseReturnReportToolStripMenuItem_Click);
            // 
            // salesOrderReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesOrderReportToolStripMenuItem, "salesOrderReportToolStripMenuItem");
            this.salesOrderReportToolStripMenuItem.Name = "salesOrderReportToolStripMenuItem";
            this.salesOrderReportToolStripMenuItem.Click += new System.EventHandler(this.salesOrderReportToolStripMenuItem_Click);
            // 
            // salesQuotationReportToolStripMenuItem
            // 
            resources.ApplyResources(this.salesQuotationReportToolStripMenuItem, "salesQuotationReportToolStripMenuItem");
            this.salesQuotationReportToolStripMenuItem.Name = "salesQuotationReportToolStripMenuItem";
            this.salesQuotationReportToolStripMenuItem.Click += new System.EventHandler(this.salesQuotationReportToolStripMenuItem_Click);
            // 
            // purchaseOrderReportToolStripMenuItem
            // 
            resources.ApplyResources(this.purchaseOrderReportToolStripMenuItem, "purchaseOrderReportToolStripMenuItem");
            this.purchaseOrderReportToolStripMenuItem.Name = "purchaseOrderReportToolStripMenuItem";
            this.purchaseOrderReportToolStripMenuItem.Click += new System.EventHandler(this.purchaseOrderReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // partyListToolStripMenuItem
            // 
            resources.ApplyResources(this.partyListToolStripMenuItem, "partyListToolStripMenuItem");
            this.partyListToolStripMenuItem.Name = "partyListToolStripMenuItem";
            this.partyListToolStripMenuItem.Click += new System.EventHandler(this.partyListToolStripMenuItem_Click);
            // 
            // accountPayableAndReceivableReportToolStripMenuItem
            // 
            resources.ApplyResources(this.accountPayableAndReceivableReportToolStripMenuItem, "accountPayableAndReceivableReportToolStripMenuItem");
            this.accountPayableAndReceivableReportToolStripMenuItem.Name = "accountPayableAndReceivableReportToolStripMenuItem";
            this.accountPayableAndReceivableReportToolStripMenuItem.Click += new System.EventHandler(this.accountPayableAndReceivableReportToolStripMenuItem_Click);
            // 
            // rootwiseCollectionReportToolStripMenuItem
            // 
            resources.ApplyResources(this.rootwiseCollectionReportToolStripMenuItem, "rootwiseCollectionReportToolStripMenuItem");
            this.rootwiseCollectionReportToolStripMenuItem.Name = "rootwiseCollectionReportToolStripMenuItem";
            this.rootwiseCollectionReportToolStripMenuItem.Click += new System.EventHandler(this.rootwiseCollectionReportToolStripMenuItem_Click);
            // 
            // transactionReportToolStripMenuItem
            // 
            resources.ApplyResources(this.transactionReportToolStripMenuItem, "transactionReportToolStripMenuItem");
            this.transactionReportToolStripMenuItem.Name = "transactionReportToolStripMenuItem";
            this.transactionReportToolStripMenuItem.Click += new System.EventHandler(this.transactionReportToolStripMenuItem_Click);
            // 
            // otherReportsToolStripMenuItem
            // 
            resources.ApplyResources(this.otherReportsToolStripMenuItem, "otherReportsToolStripMenuItem");
            this.otherReportsToolStripMenuItem.Name = "otherReportsToolStripMenuItem";
            this.otherReportsToolStripMenuItem.Click += new System.EventHandler(this.otherReportsToolStripMenuItem_Click);
            // 
            // tsmCustomerReport
            // 
            resources.ApplyResources(this.tsmCustomerReport, "tsmCustomerReport");
            this.tsmCustomerReport.Name = "tsmCustomerReport";
            this.tsmCustomerReport.Click += new System.EventHandler(this.tsmCustomerReport_Click);
            // 
            // tsmSupplierReport
            // 
            resources.ApplyResources(this.tsmSupplierReport, "tsmSupplierReport");
            this.tsmSupplierReport.Name = "tsmSupplierReport";
            this.tsmSupplierReport.Click += new System.EventHandler(this.tsmSupplierReport_Click);
            // 
            // inventoryReportsToolStripMenuItem
            // 
            resources.ApplyResources(this.inventoryReportsToolStripMenuItem, "inventoryReportsToolStripMenuItem");
            this.inventoryReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockReportToolStripMenuItem,
            this.tsmItmWiseSlsRpt,
            this.tsmItmWisePurRpt,
            this.productLedgerToolStripMenuItem,
            this.stockTransferEportToolStripMenuItem,
            this.tsmInvntrySlsRtnRgst,
            this.inventorySalesRegisterToolStripMenuItem,
            this.damageStockReportToolStripMenuItem,
            this.tsmInvntryPurRgstr,
            this.tsmInvntryPurRtnRgstr,
            this.productFlowReportToolStripMenuItem,
            this.unsoldProductToolStripMenuItem,
            this.godownStockReportToolStripMenuItem,
            this.productRegisterToolStripMenuItem,
            this.tsmInventoryReport});
            this.inventoryReportsToolStripMenuItem.Name = "inventoryReportsToolStripMenuItem";
            // 
            // stockReportToolStripMenuItem
            // 
            resources.ApplyResources(this.stockReportToolStripMenuItem, "stockReportToolStripMenuItem");
            this.stockReportToolStripMenuItem.Name = "stockReportToolStripMenuItem";
            this.stockReportToolStripMenuItem.Click += new System.EventHandler(this.stockReportToolStripMenuItem_Click);
            // 
            // tsmItmWiseSlsRpt
            // 
            resources.ApplyResources(this.tsmItmWiseSlsRpt, "tsmItmWiseSlsRpt");
            this.tsmItmWiseSlsRpt.Name = "tsmItmWiseSlsRpt";
            this.tsmItmWiseSlsRpt.Click += new System.EventHandler(this.tsmItmWiseSlsRpt_Click);
            // 
            // tsmItmWisePurRpt
            // 
            resources.ApplyResources(this.tsmItmWisePurRpt, "tsmItmWisePurRpt");
            this.tsmItmWisePurRpt.Name = "tsmItmWisePurRpt";
            this.tsmItmWisePurRpt.Click += new System.EventHandler(this.tsmItmWisePurRpt_Click);
            // 
            // productLedgerToolStripMenuItem
            // 
            resources.ApplyResources(this.productLedgerToolStripMenuItem, "productLedgerToolStripMenuItem");
            this.productLedgerToolStripMenuItem.Name = "productLedgerToolStripMenuItem";
            this.productLedgerToolStripMenuItem.Click += new System.EventHandler(this.productLedgerToolStripMenuItem_Click);
            // 
            // stockTransferEportToolStripMenuItem
            // 
            resources.ApplyResources(this.stockTransferEportToolStripMenuItem, "stockTransferEportToolStripMenuItem");
            this.stockTransferEportToolStripMenuItem.Name = "stockTransferEportToolStripMenuItem";
            this.stockTransferEportToolStripMenuItem.Click += new System.EventHandler(this.stockTransferEportToolStripMenuItem_Click);
            // 
            // tsmInvntrySlsRtnRgst
            // 
            resources.ApplyResources(this.tsmInvntrySlsRtnRgst, "tsmInvntrySlsRtnRgst");
            this.tsmInvntrySlsRtnRgst.Name = "tsmInvntrySlsRtnRgst";
            this.tsmInvntrySlsRtnRgst.Click += new System.EventHandler(this.tsmInvntrySlsRtnRgst_Click);
            // 
            // inventorySalesRegisterToolStripMenuItem
            // 
            resources.ApplyResources(this.inventorySalesRegisterToolStripMenuItem, "inventorySalesRegisterToolStripMenuItem");
            this.inventorySalesRegisterToolStripMenuItem.Name = "inventorySalesRegisterToolStripMenuItem";
            this.inventorySalesRegisterToolStripMenuItem.Click += new System.EventHandler(this.inventorySalesRegisterToolStripMenuItem_Click);
            // 
            // damageStockReportToolStripMenuItem
            // 
            resources.ApplyResources(this.damageStockReportToolStripMenuItem, "damageStockReportToolStripMenuItem");
            this.damageStockReportToolStripMenuItem.Name = "damageStockReportToolStripMenuItem";
            this.damageStockReportToolStripMenuItem.Click += new System.EventHandler(this.damageStockReportToolStripMenuItem_Click);
            // 
            // tsmInvntryPurRgstr
            // 
            resources.ApplyResources(this.tsmInvntryPurRgstr, "tsmInvntryPurRgstr");
            this.tsmInvntryPurRgstr.Name = "tsmInvntryPurRgstr";
            this.tsmInvntryPurRgstr.Click += new System.EventHandler(this.tsmInvntryPurRgstr_Click);
            // 
            // tsmInvntryPurRtnRgstr
            // 
            resources.ApplyResources(this.tsmInvntryPurRtnRgstr, "tsmInvntryPurRtnRgstr");
            this.tsmInvntryPurRtnRgstr.Name = "tsmInvntryPurRtnRgstr";
            this.tsmInvntryPurRtnRgstr.Click += new System.EventHandler(this.tsmInvntryPurRtnRgstr_Click);
            // 
            // productFlowReportToolStripMenuItem
            // 
            resources.ApplyResources(this.productFlowReportToolStripMenuItem, "productFlowReportToolStripMenuItem");
            this.productFlowReportToolStripMenuItem.Name = "productFlowReportToolStripMenuItem";
            this.productFlowReportToolStripMenuItem.Click += new System.EventHandler(this.productFlowReportToolStripMenuItem_Click);
            // 
            // unsoldProductToolStripMenuItem
            // 
            resources.ApplyResources(this.unsoldProductToolStripMenuItem, "unsoldProductToolStripMenuItem");
            this.unsoldProductToolStripMenuItem.Name = "unsoldProductToolStripMenuItem";
            this.unsoldProductToolStripMenuItem.Click += new System.EventHandler(this.unsoldProductToolStripMenuItem_Click);
            // 
            // godownStockReportToolStripMenuItem
            // 
            resources.ApplyResources(this.godownStockReportToolStripMenuItem, "godownStockReportToolStripMenuItem");
            this.godownStockReportToolStripMenuItem.Name = "godownStockReportToolStripMenuItem";
            this.godownStockReportToolStripMenuItem.Click += new System.EventHandler(this.godownStockReportToolStripMenuItem_Click);
            // 
            // productRegisterToolStripMenuItem
            // 
            resources.ApplyResources(this.productRegisterToolStripMenuItem, "productRegisterToolStripMenuItem");
            this.productRegisterToolStripMenuItem.Name = "productRegisterToolStripMenuItem";
            this.productRegisterToolStripMenuItem.Click += new System.EventHandler(this.productRegisterToolStripMenuItem_Click);
            // 
            // tsmInventoryReport
            // 
            resources.ApplyResources(this.tsmInventoryReport, "tsmInventoryReport");
            this.tsmInventoryReport.Name = "tsmInventoryReport";
            this.tsmInventoryReport.Click += new System.EventHandler(this.tsmInventoryReport_Click);
            // 
            // accountsReportsToolStripMenuItem
            // 
            resources.ApplyResources(this.accountsReportsToolStripMenuItem, "accountsReportsToolStripMenuItem");
            this.accountsReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountBookToolStripMenuItem,
            this.cashBookToolStripMenuItem,
            this.bankBookToolStripMenuItem,
            this.dayBookToolStripMenuItem,
            this.paymentRegisterToolStripMenuItem,
            this.receiptRegisterToolStripMenuItem,
            this.journalRegisterToolStripMenuItem,
            this.trialBalanceToolStripMenuItem,
            this.incomeAndExpenditureReportToolStripMenuItem,
            this.balanceSheetToolStripMenuItem,
            this.outputVatReportToolStripMenuItem,
            this.inputVatReportToolStripMenuItem,
            this.productWiseGSTToolStripMenuItem,
            this.tsmTaxReport});
            this.accountsReportsToolStripMenuItem.Name = "accountsReportsToolStripMenuItem";
            // 
            // accountBookToolStripMenuItem
            // 
            resources.ApplyResources(this.accountBookToolStripMenuItem, "accountBookToolStripMenuItem");
            this.accountBookToolStripMenuItem.Name = "accountBookToolStripMenuItem";
            this.accountBookToolStripMenuItem.Click += new System.EventHandler(this.accountBookToolStripMenuItem_Click);
            // 
            // cashBookToolStripMenuItem
            // 
            resources.ApplyResources(this.cashBookToolStripMenuItem, "cashBookToolStripMenuItem");
            this.cashBookToolStripMenuItem.Name = "cashBookToolStripMenuItem";
            this.cashBookToolStripMenuItem.Click += new System.EventHandler(this.cashBookToolStripMenuItem_Click);
            // 
            // bankBookToolStripMenuItem
            // 
            resources.ApplyResources(this.bankBookToolStripMenuItem, "bankBookToolStripMenuItem");
            this.bankBookToolStripMenuItem.Name = "bankBookToolStripMenuItem";
            this.bankBookToolStripMenuItem.Click += new System.EventHandler(this.bankBookToolStripMenuItem_Click);
            // 
            // dayBookToolStripMenuItem
            // 
            resources.ApplyResources(this.dayBookToolStripMenuItem, "dayBookToolStripMenuItem");
            this.dayBookToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.summaryToolStripMenuItem,
            this.detailedToolStripMenuItem});
            this.dayBookToolStripMenuItem.Name = "dayBookToolStripMenuItem";
            // 
            // summaryToolStripMenuItem
            // 
            resources.ApplyResources(this.summaryToolStripMenuItem, "summaryToolStripMenuItem");
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Click += new System.EventHandler(this.summaryToolStripMenuItem_Click);
            // 
            // detailedToolStripMenuItem
            // 
            resources.ApplyResources(this.detailedToolStripMenuItem, "detailedToolStripMenuItem");
            this.detailedToolStripMenuItem.Name = "detailedToolStripMenuItem";
            this.detailedToolStripMenuItem.Click += new System.EventHandler(this.detailedToolStripMenuItem_Click);
            // 
            // paymentRegisterToolStripMenuItem
            // 
            resources.ApplyResources(this.paymentRegisterToolStripMenuItem, "paymentRegisterToolStripMenuItem");
            this.paymentRegisterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailedToolStripMenuItem1,
            this.summaryToolStripMenuItem1});
            this.paymentRegisterToolStripMenuItem.Name = "paymentRegisterToolStripMenuItem";
            // 
            // detailedToolStripMenuItem1
            // 
            resources.ApplyResources(this.detailedToolStripMenuItem1, "detailedToolStripMenuItem1");
            this.detailedToolStripMenuItem1.Name = "detailedToolStripMenuItem1";
            this.detailedToolStripMenuItem1.Click += new System.EventHandler(this.paymentRegisterToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem1
            // 
            resources.ApplyResources(this.summaryToolStripMenuItem1, "summaryToolStripMenuItem1");
            this.summaryToolStripMenuItem1.Name = "summaryToolStripMenuItem1";
            this.summaryToolStripMenuItem1.Click += new System.EventHandler(this.summaryToolStripMenuItem1_Click);
            // 
            // receiptRegisterToolStripMenuItem
            // 
            resources.ApplyResources(this.receiptRegisterToolStripMenuItem, "receiptRegisterToolStripMenuItem");
            this.receiptRegisterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailedToolStripMenuItem2,
            this.summaryToolStripMenuItem2});
            this.receiptRegisterToolStripMenuItem.Name = "receiptRegisterToolStripMenuItem";
            // 
            // detailedToolStripMenuItem2
            // 
            resources.ApplyResources(this.detailedToolStripMenuItem2, "detailedToolStripMenuItem2");
            this.detailedToolStripMenuItem2.Name = "detailedToolStripMenuItem2";
            this.detailedToolStripMenuItem2.Click += new System.EventHandler(this.receiptRegisterToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem2
            // 
            resources.ApplyResources(this.summaryToolStripMenuItem2, "summaryToolStripMenuItem2");
            this.summaryToolStripMenuItem2.Name = "summaryToolStripMenuItem2";
            this.summaryToolStripMenuItem2.Click += new System.EventHandler(this.summaryToolStripMenuItem2_Click);
            // 
            // journalRegisterToolStripMenuItem
            // 
            resources.ApplyResources(this.journalRegisterToolStripMenuItem, "journalRegisterToolStripMenuItem");
            this.journalRegisterToolStripMenuItem.Name = "journalRegisterToolStripMenuItem";
            this.journalRegisterToolStripMenuItem.Click += new System.EventHandler(this.journalRegisterToolStripMenuItem_Click);
            // 
            // trialBalanceToolStripMenuItem
            // 
            resources.ApplyResources(this.trialBalanceToolStripMenuItem, "trialBalanceToolStripMenuItem");
            this.trialBalanceToolStripMenuItem.Name = "trialBalanceToolStripMenuItem";
            this.trialBalanceToolStripMenuItem.Click += new System.EventHandler(this.trialBalanceToolStripMenuItem_Click);
            // 
            // incomeAndExpenditureReportToolStripMenuItem
            // 
            resources.ApplyResources(this.incomeAndExpenditureReportToolStripMenuItem, "incomeAndExpenditureReportToolStripMenuItem");
            this.incomeAndExpenditureReportToolStripMenuItem.Name = "incomeAndExpenditureReportToolStripMenuItem";
            this.incomeAndExpenditureReportToolStripMenuItem.Click += new System.EventHandler(this.incomeAndExpenditureReportToolStripMenuItem_Click);
            // 
            // balanceSheetToolStripMenuItem
            // 
            resources.ApplyResources(this.balanceSheetToolStripMenuItem, "balanceSheetToolStripMenuItem");
            this.balanceSheetToolStripMenuItem.Name = "balanceSheetToolStripMenuItem";
            this.balanceSheetToolStripMenuItem.Click += new System.EventHandler(this.balanceSheetToolStripMenuItem_Click);
            // 
            // outputVatReportToolStripMenuItem
            // 
            resources.ApplyResources(this.outputVatReportToolStripMenuItem, "outputVatReportToolStripMenuItem");
            this.outputVatReportToolStripMenuItem.Name = "outputVatReportToolStripMenuItem";
            this.outputVatReportToolStripMenuItem.Click += new System.EventHandler(this.outputVatReportToolStripMenuItem_Click);
            // 
            // inputVatReportToolStripMenuItem
            // 
            resources.ApplyResources(this.inputVatReportToolStripMenuItem, "inputVatReportToolStripMenuItem");
            this.inputVatReportToolStripMenuItem.Name = "inputVatReportToolStripMenuItem";
            this.inputVatReportToolStripMenuItem.Click += new System.EventHandler(this.inputVatReportToolStripMenuItem_Click);
            // 
            // productWiseGSTToolStripMenuItem
            // 
            resources.ApplyResources(this.productWiseGSTToolStripMenuItem, "productWiseGSTToolStripMenuItem");
            this.productWiseGSTToolStripMenuItem.Name = "productWiseGSTToolStripMenuItem";
            this.productWiseGSTToolStripMenuItem.Click += new System.EventHandler(this.productWiseGSTToolStripMenuItem_Click);
            // 
            // tsmTaxReport
            // 
            resources.ApplyResources(this.tsmTaxReport, "tsmTaxReport");
            this.tsmTaxReport.Name = "tsmTaxReport";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // staffToolStripMenuItem
            // 
            resources.ApplyResources(this.staffToolStripMenuItem, "staffToolStripMenuItem");
            this.staffToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staffRegistrationToolStripMenuItem,
            this.salaryProcessToolStripMenuItem,
            this.attendanceToolStripMenuItem});
            this.staffToolStripMenuItem.Name = "staffToolStripMenuItem";
            // 
            // staffRegistrationToolStripMenuItem
            // 
            resources.ApplyResources(this.staffRegistrationToolStripMenuItem, "staffRegistrationToolStripMenuItem");
            this.staffRegistrationToolStripMenuItem.Name = "staffRegistrationToolStripMenuItem";
            this.staffRegistrationToolStripMenuItem.Click += new System.EventHandler(this.staffRegistrationToolStripMenuItem_Click);
            // 
            // salaryProcessToolStripMenuItem
            // 
            resources.ApplyResources(this.salaryProcessToolStripMenuItem, "salaryProcessToolStripMenuItem");
            this.salaryProcessToolStripMenuItem.Name = "salaryProcessToolStripMenuItem";
            this.salaryProcessToolStripMenuItem.Click += new System.EventHandler(this.salaryProcessToolStripMenuItem_Click);
            // 
            // attendanceToolStripMenuItem
            // 
            resources.ApplyResources(this.attendanceToolStripMenuItem, "attendanceToolStripMenuItem");
            this.attendanceToolStripMenuItem.Name = "attendanceToolStripMenuItem";
            this.attendanceToolStripMenuItem.Click += new System.EventHandler(this.attendanceToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            resources.ApplyResources(this.userToolStripMenuItem, "userToolStripMenuItem");
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roleAndPrevilageToolStripMenuItem,
            this.createUserToolStripMenuItem,
            this.changePasswordToolStripMenuItem1});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            // 
            // roleAndPrevilageToolStripMenuItem
            // 
            resources.ApplyResources(this.roleAndPrevilageToolStripMenuItem, "roleAndPrevilageToolStripMenuItem");
            this.roleAndPrevilageToolStripMenuItem.Name = "roleAndPrevilageToolStripMenuItem";
            this.roleAndPrevilageToolStripMenuItem.Click += new System.EventHandler(this.roleAndPrevilageToolStripMenuItem_Click);
            // 
            // createUserToolStripMenuItem
            // 
            resources.ApplyResources(this.createUserToolStripMenuItem, "createUserToolStripMenuItem");
            this.createUserToolStripMenuItem.Name = "createUserToolStripMenuItem";
            this.createUserToolStripMenuItem.Click += new System.EventHandler(this.createUserToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem1
            // 
            resources.ApplyResources(this.changePasswordToolStripMenuItem1, "changePasswordToolStripMenuItem1");
            this.changePasswordToolStripMenuItem1.Name = "changePasswordToolStripMenuItem1";
            this.changePasswordToolStripMenuItem1.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            resources.ApplyResources(this.optionToolStripMenuItem, "optionToolStripMenuItem");
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolMenuExportData,
            this.ToolMenuImportData,
            this.changeDateToolStripMenuItem,
            this.billGenetrationToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem,
            this.themeToolStripMenuItem,
            this.printDesignToolStripMenuItem,
            this.customTransactionAndReportToolStripMenuItem,
            this.databaseBackupToolStripMenuItem,
            this.databaseRestoreToolStripMenuItem,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.dayCloseToolStripMenuItem,
            this.calendarToolStripMenuItem,
            this.sendMailToolStripMenuItem1,
            this.qRCodeToolStripMenuItem,
            this.tsmRateFormat,
            this.tsmLabelScale});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            // 
            // ToolMenuExportData
            // 
            resources.ApplyResources(this.ToolMenuExportData, "ToolMenuExportData");
            this.ToolMenuExportData.Name = "ToolMenuExportData";
            this.ToolMenuExportData.Click += new System.EventHandler(this.ToolMenuExportData_Click);
            // 
            // ToolMenuImportData
            // 
            resources.ApplyResources(this.ToolMenuImportData, "ToolMenuImportData");
            this.ToolMenuImportData.Name = "ToolMenuImportData";
            this.ToolMenuImportData.Click += new System.EventHandler(this.ToolMenuImportData_Click);
            // 
            // changeDateToolStripMenuItem
            // 
            resources.ApplyResources(this.changeDateToolStripMenuItem, "changeDateToolStripMenuItem");
            this.changeDateToolStripMenuItem.Name = "changeDateToolStripMenuItem";
            this.changeDateToolStripMenuItem.Click += new System.EventHandler(this.changeDateToolStripMenuItem_Click);
            // 
            // billGenetrationToolStripMenuItem
            // 
            resources.ApplyResources(this.billGenetrationToolStripMenuItem, "billGenetrationToolStripMenuItem");
            this.billGenetrationToolStripMenuItem.Name = "billGenetrationToolStripMenuItem";
            this.billGenetrationToolStripMenuItem.Click += new System.EventHandler(this.remainderToolStripMenuItem_Click);
            // 
            // applicationSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.applicationSettingsToolStripMenuItem, "applicationSettingsToolStripMenuItem");
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.applicationSettingsToolStripMenuItem_Click);
            // 
            // themeToolStripMenuItem
            // 
            resources.ApplyResources(this.themeToolStripMenuItem, "themeToolStripMenuItem");
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorThemeToolStripMenuItem,
            this.customThemeToolStripMenuItem,
            this.dashboardThemeToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            // 
            // colorThemeToolStripMenuItem
            // 
            resources.ApplyResources(this.colorThemeToolStripMenuItem, "colorThemeToolStripMenuItem");
            this.colorThemeToolStripMenuItem.Name = "colorThemeToolStripMenuItem";
            // 
            // customThemeToolStripMenuItem
            // 
            resources.ApplyResources(this.customThemeToolStripMenuItem, "customThemeToolStripMenuItem");
            this.customThemeToolStripMenuItem.Name = "customThemeToolStripMenuItem";
            this.customThemeToolStripMenuItem.Click += new System.EventHandler(this.customThemeToolStripMenuItem_Click);
            // 
            // dashboardThemeToolStripMenuItem
            // 
            resources.ApplyResources(this.dashboardThemeToolStripMenuItem, "dashboardThemeToolStripMenuItem");
            this.dashboardThemeToolStripMenuItem.Name = "dashboardThemeToolStripMenuItem";
            this.dashboardThemeToolStripMenuItem.Click += new System.EventHandler(this.dashboardThemeToolStripMenuItem_Click);
            // 
            // printDesignToolStripMenuItem
            // 
            resources.ApplyResources(this.printDesignToolStripMenuItem, "printDesignToolStripMenuItem");
            this.printDesignToolStripMenuItem.Name = "printDesignToolStripMenuItem";
            this.printDesignToolStripMenuItem.Click += new System.EventHandler(this.printDesignToolStripMenuItem_Click);
            // 
            // customTransactionAndReportToolStripMenuItem
            // 
            resources.ApplyResources(this.customTransactionAndReportToolStripMenuItem, "customTransactionAndReportToolStripMenuItem");
            this.customTransactionAndReportToolStripMenuItem.Name = "customTransactionAndReportToolStripMenuItem";
            this.customTransactionAndReportToolStripMenuItem.Click += new System.EventHandler(this.customTransactionAndReportToolStripMenuItem_Click);
            // 
            // databaseBackupToolStripMenuItem
            // 
            resources.ApplyResources(this.databaseBackupToolStripMenuItem, "databaseBackupToolStripMenuItem");
            this.databaseBackupToolStripMenuItem.Name = "databaseBackupToolStripMenuItem";
            this.databaseBackupToolStripMenuItem.Click += new System.EventHandler(this.databaseBackupToolStripMenuItem_Click);
            // 
            // databaseRestoreToolStripMenuItem
            // 
            resources.ApplyResources(this.databaseRestoreToolStripMenuItem, "databaseRestoreToolStripMenuItem");
            this.databaseRestoreToolStripMenuItem.Name = "databaseRestoreToolStripMenuItem";
            this.databaseRestoreToolStripMenuItem.Click += new System.EventHandler(this.databaseRestoreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            resources.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripMenuItem13
            // 
            resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // dayCloseToolStripMenuItem
            // 
            resources.ApplyResources(this.dayCloseToolStripMenuItem, "dayCloseToolStripMenuItem");
            this.dayCloseToolStripMenuItem.Name = "dayCloseToolStripMenuItem";
            this.dayCloseToolStripMenuItem.Click += new System.EventHandler(this.dayCloseToolStripMenuItem_Click);
            // 
            // calendarToolStripMenuItem
            // 
            resources.ApplyResources(this.calendarToolStripMenuItem, "calendarToolStripMenuItem");
            this.calendarToolStripMenuItem.Name = "calendarToolStripMenuItem";
            this.calendarToolStripMenuItem.Click += new System.EventHandler(this.calendarToolStripMenuItem_Click);
            // 
            // sendMailToolStripMenuItem1
            // 
            resources.ApplyResources(this.sendMailToolStripMenuItem1, "sendMailToolStripMenuItem1");
            this.sendMailToolStripMenuItem1.Name = "sendMailToolStripMenuItem1";
            this.sendMailToolStripMenuItem1.Click += new System.EventHandler(this.sendMailToolStripMenuItem_Click);
            // 
            // qRCodeToolStripMenuItem
            // 
            resources.ApplyResources(this.qRCodeToolStripMenuItem, "qRCodeToolStripMenuItem");
            this.qRCodeToolStripMenuItem.Name = "qRCodeToolStripMenuItem";
            this.qRCodeToolStripMenuItem.Click += new System.EventHandler(this.qRCodeToolStripMenuItem_Click);
            // 
            // tsmRateFormat
            // 
            resources.ApplyResources(this.tsmRateFormat, "tsmRateFormat");
            this.tsmRateFormat.Name = "tsmRateFormat";
            this.tsmRateFormat.Click += new System.EventHandler(this.tsmRateFormat_Click);
            // 
            // tsmLabelScale
            // 
            resources.ApplyResources(this.tsmLabelScale, "tsmLabelScale");
            this.tsmLabelScale.Name = "tsmLabelScale";
            this.tsmLabelScale.Click += new System.EventHandler(this.tsmLabelScale_Click);
            // 
            // toolsToolStripMenuItem
            // 
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calculatorToolStripMenuItem,
            this.excelToolStripMenuItem,
            this.notepadToolStripMenuItem,
            this.paintToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            // 
            // calculatorToolStripMenuItem
            // 
            resources.ApplyResources(this.calculatorToolStripMenuItem, "calculatorToolStripMenuItem");
            this.calculatorToolStripMenuItem.Name = "calculatorToolStripMenuItem";
            this.calculatorToolStripMenuItem.Click += new System.EventHandler(this.calculatorToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            resources.ApplyResources(this.excelToolStripMenuItem, "excelToolStripMenuItem");
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // notepadToolStripMenuItem
            // 
            resources.ApplyResources(this.notepadToolStripMenuItem, "notepadToolStripMenuItem");
            this.notepadToolStripMenuItem.Name = "notepadToolStripMenuItem";
            this.notepadToolStripMenuItem.Click += new System.EventHandler(this.notepadToolStripMenuItem_Click);
            // 
            // paintToolStripMenuItem
            // 
            resources.ApplyResources(this.paintToolStripMenuItem, "paintToolStripMenuItem");
            this.paintToolStripMenuItem.Name = "paintToolStripMenuItem";
            this.paintToolStripMenuItem.Click += new System.EventHandler(this.paintToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutUsToolStripMenuItem,
            this.contactUsToolStripMenuItem,
            this.videoTutorialToolStripMenuItem,
            this.reportBuggsToolStripMenuItem,
            this.tsmUpdate});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            // 
            // aboutUsToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutUsToolStripMenuItem, "aboutUsToolStripMenuItem");
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // contactUsToolStripMenuItem
            // 
            resources.ApplyResources(this.contactUsToolStripMenuItem, "contactUsToolStripMenuItem");
            this.contactUsToolStripMenuItem.Name = "contactUsToolStripMenuItem";
            this.contactUsToolStripMenuItem.Click += new System.EventHandler(this.contactUsToolStripMenuItem_Click);
            // 
            // videoTutorialToolStripMenuItem
            // 
            resources.ApplyResources(this.videoTutorialToolStripMenuItem, "videoTutorialToolStripMenuItem");
            this.videoTutorialToolStripMenuItem.Name = "videoTutorialToolStripMenuItem";
            this.videoTutorialToolStripMenuItem.Click += new System.EventHandler(this.videoTutorialToolStripMenuItem_Click);
            // 
            // reportBuggsToolStripMenuItem
            // 
            resources.ApplyResources(this.reportBuggsToolStripMenuItem, "reportBuggsToolStripMenuItem");
            this.reportBuggsToolStripMenuItem.Name = "reportBuggsToolStripMenuItem";
            this.reportBuggsToolStripMenuItem.Click += new System.EventHandler(this.reportBuggsToolStripMenuItem_Click);
            // 
            // tsmUpdate
            // 
            resources.ApplyResources(this.tsmUpdate, "tsmUpdate");
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Click += new System.EventHandler(this.tsmUpdate_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSale,
            this.tsbPurchase,
            this.tsbProduct,
            this.tsbPaymentAndReceipt,
            this.tsbExpense,
            this.tsbStock,
            this.tsbLedger,
            this.tsbCustomerReport,
            this.tsbSupplierReport,
            this.tsbVatReport,
            this.tsmDayClose,
            this.tsmCalculator,
            this.tsbCalendar,
            this.tsbLockApplication,
            this.toolStripButton14,
            this.toolStripButton15,
            this.lblPendingPeriod});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolTip1.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // tsbSale
            // 
            resources.ApplyResources(this.tsbSale, "tsbSale");
            this.tsbSale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSale.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbSale.Name = "tsbSale";
            this.tsbSale.Click += new System.EventHandler(this.invoiceToolStripMenuItem_Click);
            // 
            // tsbPurchase
            // 
            resources.ApplyResources(this.tsbPurchase, "tsbPurchase");
            this.tsbPurchase.BackColor = System.Drawing.Color.White;
            this.tsbPurchase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPurchase.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tsbPurchase.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tsbPurchase.Name = "tsbPurchase";
            this.tsbPurchase.Click += new System.EventHandler(this.invoiceToolStripMenuItem1_Click);
            // 
            // tsbProduct
            // 
            resources.ApplyResources(this.tsbProduct, "tsbProduct");
            this.tsbProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProduct.Margin = new System.Windows.Forms.Padding(2, 1, 3, 2);
            this.tsbProduct.Name = "tsbProduct";
            this.tsbProduct.Click += new System.EventHandler(this.productToolStripMenuItem_Click);
            // 
            // tsbPaymentAndReceipt
            // 
            resources.ApplyResources(this.tsbPaymentAndReceipt, "tsbPaymentAndReceipt");
            this.tsbPaymentAndReceipt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaymentAndReceipt.Margin = new System.Windows.Forms.Padding(2, 1, 3, 2);
            this.tsbPaymentAndReceipt.Name = "tsbPaymentAndReceipt";
            this.tsbPaymentAndReceipt.Click += new System.EventHandler(this.receiptAndPaymentToolStripMenuItem_Click);
            // 
            // tsbExpense
            // 
            resources.ApplyResources(this.tsbExpense, "tsbExpense");
            this.tsbExpense.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExpense.Margin = new System.Windows.Forms.Padding(2, 1, 3, 2);
            this.tsbExpense.Name = "tsbExpense";
            this.tsbExpense.Click += new System.EventHandler(this.cashPaymentToolStripMenuItem_Click);
            // 
            // tsbStock
            // 
            resources.ApplyResources(this.tsbStock, "tsbStock");
            this.tsbStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStock.Margin = new System.Windows.Forms.Padding(2, 1, 3, 2);
            this.tsbStock.Name = "tsbStock";
            this.tsbStock.Click += new System.EventHandler(this.stockReportToolStripMenuItem_Click);
            // 
            // tsbLedger
            // 
            resources.ApplyResources(this.tsbLedger, "tsbLedger");
            this.tsbLedger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLedger.Margin = new System.Windows.Forms.Padding(2, 1, 3, 2);
            this.tsbLedger.Name = "tsbLedger";
            this.tsbLedger.Click += new System.EventHandler(this.accountBookToolStripMenuItem_Click);
            // 
            // tsbCustomerReport
            // 
            resources.ApplyResources(this.tsbCustomerReport, "tsbCustomerReport");
            this.tsbCustomerReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCustomerReport.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsbCustomerReport.Name = "tsbCustomerReport";
            this.tsbCustomerReport.Click += new System.EventHandler(this.tsbCustomerReport_Click);
            // 
            // tsbSupplierReport
            // 
            resources.ApplyResources(this.tsbSupplierReport, "tsbSupplierReport");
            this.tsbSupplierReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSupplierReport.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsbSupplierReport.Name = "tsbSupplierReport";
            this.tsbSupplierReport.Click += new System.EventHandler(this.tsbSupplierReport_Click);
            // 
            // tsbVatReport
            // 
            resources.ApplyResources(this.tsbVatReport, "tsbVatReport");
            this.tsbVatReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVatReport.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsbVatReport.Name = "tsbVatReport";
            this.tsbVatReport.Click += new System.EventHandler(this.tsbVatReport_Click);
            // 
            // tsmDayClose
            // 
            resources.ApplyResources(this.tsmDayClose, "tsmDayClose");
            this.tsmDayClose.AutoToolTip = false;
            this.tsmDayClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmDayClose.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsmDayClose.Name = "tsmDayClose";
            this.tsmDayClose.Click += new System.EventHandler(this.dayCloseToolStripMenuItem_Click);
            // 
            // tsmCalculator
            // 
            resources.ApplyResources(this.tsmCalculator, "tsmCalculator");
            this.tsmCalculator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmCalculator.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsmCalculator.Name = "tsmCalculator";
            this.tsmCalculator.Click += new System.EventHandler(this.calculatorToolStripMenuItem_Click);
            // 
            // tsbCalendar
            // 
            resources.ApplyResources(this.tsbCalendar, "tsbCalendar");
            this.tsbCalendar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCalendar.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsbCalendar.Name = "tsbCalendar";
            this.tsbCalendar.Click += new System.EventHandler(this.calendarToolStripMenuItem_Click);
            // 
            // tsbLockApplication
            // 
            resources.ApplyResources(this.tsbLockApplication, "tsbLockApplication");
            this.tsbLockApplication.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLockApplication.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.tsbLockApplication.Name = "tsbLockApplication";
            this.tsbLockApplication.Click += new System.EventHandler(this.tsbLockApplication_Click);
            // 
            // toolStripButton14
            // 
            resources.ApplyResources(this.toolStripButton14, "toolStripButton14");
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripButton15
            // 
            resources.ApplyResources(this.toolStripButton15, "toolStripButton15");
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Margin = new System.Windows.Forms.Padding(1, 2, 3, 2);
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // lblPendingPeriod
            // 
            resources.ApplyResources(this.lblPendingPeriod, "lblPendingPeriod");
            this.lblPendingPeriod.ForeColor = System.Drawing.Color.Red;
            this.lblPendingPeriod.Margin = new System.Windows.Forms.Padding(250, 1, 0, 2);
            this.lblPendingPeriod.Name = "lblPendingPeriod";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackColor = System.Drawing.Color.Black;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(25, 3, 150, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(100, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

		}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ThemeChanger themeChanger = new ThemeChanger();
            themeChanger.ShowDialog();
        }
    }
}
