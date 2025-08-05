using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using DarrenLee.Translator;

namespace Ashpro
{
	public class DashboardPOS : Form
	{
		private Rectangle resolution;

		private DBContext db = new DBContext();

		private ClosingDialog frm;

		private int iMod = 0;

		private int xLocation = 170;

		private string arwMenuShow = "<";

		private string arwMenuHide = ">";

		private string arwReportShow = ">";

		private string arwReportHide = "<";

		private DashboardPOSView dpOSView;

		private POSView pOSView;

		private JournalEntryReportView JrnlRegister;

		private TrialBalanceView trailBalance;

		private ReminderView reminder;

		private Calendar calendar;

		private ProductEditing proEditing;

		private SendMail sendMail;

		private VideoTutorial videoTutorial;

		private StockAdjusterView stockAdjuster;

		private ProductWiseGSTReportView proGSTRpt;

		private AccountGroup accountGroup;

		private Admin admin;

		private CompanyDetails CDetails;

		private VehicleView vehicle;

		private Shelfs self;

		private Account account;

		private ProductGroup PGroup;

		private Product product;

		private Brand brand;

		private Unit unit;

		private Godown godown;

		private TaxCategory tCategory;

		private PriceCategory priceCetegory;

		private Route route;

		private SalesInvoiceView salesInvoice;

		private DatabaseAutoBackup dbAutoBackup;

		private ChangePassword changePWord;

		private Calculator calcu;

		private About aboutus;

		private ContactUs contactus;

		private ReportBugs reportbuggs;

		private ChangeDate changeDate;

		private SalesEstimateView saleEstimate;

		private SalesReportView saleEstimateReport;

		private LedgerReport ledgerReport;

		private ProductLedgerReportView productLedgerReport;

		private SalesReturnView salesReturn;

		private ChequeRecipt cheqReceipt;

		private SalesView salesView;

		private BillPrint billPrint;

		private FinancialPeriod fPeriod;

		private SelectedFyear SelectedFY;

		private ReceiptView cashReceipt;

		private PurchaseInvoiceView Purchase;

		private PurchaseReturnView purchaseReturn;

		private PaymentView cashPayment;

		private JournalEntryView journalEntry;

		private OpeningBalance openeningBalance;

		private DebitNoteView debitNote;

		private CreditNoteView creditNote;

		private SalesAndSalesReturnReportView salesAndReturn;

		private PrintDesignView printDesign;

		private FirstPurchase FPurchase;

		private PartyListView partyList;

		private PriceListView priceListReport;

		private SalesReturnReportView saleReturnReport;

		private PurchaseReportView purchaseReport;

		private PurchaseReturnReportView purchaseReturnReport;

		private PaymentReceiptReportView payRecRpt;

		private OutstandingReportView outStandingReport;

		private ItemWiseSalesReportView itemWiseSReport;

		private ItemWiseSalesReportView itemWiseSReportP;

		private InventorySalesRegisterView inventorySRegister;

		private InventorySalesRegisterView inventorySRegisterSalesReturn;

		private InventorySalesRegisterView inventorySRegisterPurchase;

		private InventorySalesRegisterView inventorySRegisterPurchaseReturn;

		private TransactionReportView transactionReport;

		private Product_Flow_Report_Settings PFlowRpt;

		private UnSoldProductReportView Unsoldproductreport;

		private ProductCombinationView proCombination;

		private ProductCombinationView Packing;

		private GodownStockReportView godownStkReport;

		private StockTransferView stockTransfer;

		private StockTransferView DamageEntry;

		private Cost_Centres costCentres;

		private CustomerPrevilegeCard cpCard;

		private OrderFormView sOrder;

		private OrderFormView sQuotation;

		private OrderFormView pOrder;

		private UserManagement uManagement;

		private SalesOrderReportView SlsOrderRpt;

		private SalesOrderReportView SlsQuotationRpt;

		private SalesOrderReportView pOrderRpt;

		private UserPrevilegeView uPrevilege;

		private Application_Settings appSettings;

		private CustomTransReport cusTranRpt;

		private BackupDatabase backupDB;

		private BackupDatabase restoreDB;

		private Balance_Sheet bSheet;

		private Profit_and_Lose_Account pfAccount;

		private DailyAccountView dayBook;

		private EmployeeDetails empDetails;

		private GSTSalesRptView gstSaleRpt;

		private GSTSalesRptView gstPurRpt;

		private ChequeRecipt chequePayment;

		private BankReconciliation bankReconciliation;

		private SalaryProcessView salProcess;

		private BillGeneration billGeneration;

		private DayClose dayClose;

		private PendingOrderReportView pendingOR;

		private StockReportView stockRpt;

		private SalesCounter slsCounter;

		private StockTrnasferReportView stockTranRpt;

		private DebitCreditCardMaster dcCardMaster;

		private SalesSummaryReportView slsSummaryRpt;

		private BarcodeDesign barcode;

		private PaymentView bankPayment;

		private ReceiptView bankReceipt;

		private ExportAndImport export;

		private ExportAndImport import;

		private StaffAttendanceView attendance;

		private TransactionWithoutInventory tranWtotInv;

		private ProductManufacturingView manufacturing;

		private Product_Register_Settings proRRegisterSettings;

		private PrintBarcode printBarcode;

		private IContainer components = null;

		private Label lblManufacturer;

		private Label lblDateTime;

		private Label lblsystem;

		private FlowLayoutPanel pnlNavigation;

		private Button btnFirm;

		private Button btnMasters;

		private Button btnTransaction;

		private Button btnReports;

		private Button btnStaff;

		private Button btnOptions;

		private Button btnAbout;

		private ContextMenuStrip cmsMasters;

		private ToolStripMenuItem tsmAGroup;

		private ToolStripMenuItem tsmAccount;

		private ToolStripMenuItem tsmSupplier;

		private ToolStripMenuItem tsmExpense;

		private ToolStripMenuItem tsmBank;

		private ToolStripMenuItem tsmPGroup;

		private ToolStripMenuItem tsmProduct;

		private ToolStripMenuItem tsmBrand;

		private ToolStripMenuItem tsmUnit;

		private ToolStripMenuItem tsmGodown;

		private ToolStripMenuItem tsmShelf;

		private ToolStripMenuItem tsmPCategory;

		private ToolStripMenuItem tsmTCategory;

		private ToolStripMenuItem tsmRoute;

		private ToolStripMenuItem tsmVehicle;

		private ToolStripMenuItem tsmSCounter;

		private ToolStripMenuItem tsmCCenters;

		private ToolStripMenuItem tsmCPCard;

		private ToolStripMenuItem tsmCardMaster;

		private ToolStripMenuItem tsmBranch;

		private ToolStripMenuItem tsmBatch;

		private ContextMenuStrip cmsMainTrans;

		private ToolStripMenuItem toolStripMenuItem7;

		private ToolStripMenuItem salesInvoiceToolStripMenuItem;

		private ToolStripMenuItem tsmSalesEstimate;

		private ToolStripMenuItem salesReturnToolStripMenuItem;

		private ToolStripMenuItem salesOrderToolStripMenuItem;

		private ToolStripMenuItem salesQuotationToolStripMenuItem;

		private ToolStripMenuItem salesViewToolStripMenuItem;

		private ToolStripMenuItem billPrintToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem8;

		private ToolStripMenuItem tsmPurchase;

		private ToolStripMenuItem tsmPurReturn;

		private ToolStripMenuItem tsmPurOrder;

		private ToolStripMenuItem tsmFirstPurchase;

		private ToolStripMenuItem toolStripMenuItem9;

		private ToolStripMenuItem tsmPacking;

		private ToolStripMenuItem tsmStockTransfer;

		private ToolStripMenuItem tsmDamageEntry;

		private ToolStripMenuItem tsmStockAdjuster;

		private ToolStripMenuItem tsmProductEditing;

		private ToolStripMenuItem tsmProductCombination;

		private ToolStripMenuItem tsmBCodeDesign;

		private ToolStripMenuItem tsmBCodePrinting;

		private ToolStripMenuItem tsmManufacturing;

		private ToolStripMenuItem toolStripMenuItem10;

		private ToolStripMenuItem tsmCashPayment;

		private ToolStripMenuItem tsmCashReceipt;

		private ToolStripMenuItem tsmBankPayment;

		private ToolStripMenuItem tsmBankreceipt;

		private ToolStripMenuItem tsmJournalEntry;

		private ToolStripMenuItem tsmOpeningBalance;

		private ToolStripMenuItem tsmDebitNote;

		private ToolStripMenuItem tsmCreditNote;

		private ToolStripMenuItem tsmPDCReceipt;

		private ToolStripMenuItem tsmPDCPayment;

		private ToolStripMenuItem bankReconciliationToolStripMenuItem;

		private ContextMenuStrip cmsReport;

		private ToolStripMenuItem toolStripMenuItem11;

		private ToolStripMenuItem tsmAdvanceReport;

		private ToolStripMenuItem tsmGrpWiseSlsReport;

		private ToolStripMenuItem tsmPCategoryWiseSlsReport;

		private ToolStripMenuItem tsmRtwiseSlsReport;

		private ToolStripMenuItem tsmSMWiseSlsReport;

		private ToolStripMenuItem tsmPartyWiseSlsReport;

		private ToolStripMenuItem tsmGrpWisePurRpt;

		private ToolStripMenuItem tsmSplrWsPurRpt;

		private ToolStripMenuItem tsmExiCollRpt;

		private ToolStripMenuItem tsmPendingOrdrRpt;

		private ToolStripMenuItem tsmSlsSummary;

		private ToolStripMenuItem tsmSlsRtnSummary;

		private ToolStripMenuItem tsmPurSummary;

		private ToolStripMenuItem tsmPurRtnSummary;

		private ToolStripMenuItem tsmRptSales;

		private ToolStripMenuItem tsmRptSalesEstimate;

		private ToolStripMenuItem tsmRptSalesReturn;

		private ToolStripMenuItem tsmRptSalesOrder;

		private ToolStripMenuItem tsmRptSalesQuotation;

		private ToolStripMenuItem tsmRptPurchase;

		private ToolStripMenuItem tsmRptPurchaseReturn;

		private ToolStripMenuItem tsmRptPurchaseOrder;

		private ToolStripMenuItem tsmRptSalesAndSalesReturn;

		private ToolStripMenuItem tsmRptPurchaseAndPurchaseReturn;

		private ToolStripMenuItem tsmRptCollection;

		private ToolStripMenuItem tsmRptTaxSales;

		private ToolStripMenuItem tsmRptTaxPurchase;

		private ToolStripMenuItem toolStripMenuItem13;

		private ToolStripMenuItem tsmRptStock;

		private ToolStripMenuItem tsmItemWiseSalesReport;

		private ToolStripMenuItem tsmItemWisePurchaseReport;

		private ToolStripMenuItem tsmProductLedger;

		private ToolStripMenuItem tsmStockTransferReport;

		private ToolStripMenuItem tmsInvSlsRegister;

		private ToolStripMenuItem tsmInvtSlsReturnReport;

		private ToolStripMenuItem tsmInventoryPurRegister;

		private ToolStripMenuItem tsmInventoryPurRtnRegister;

		private ToolStripMenuItem tsmProFlowReport;

		private ToolStripMenuItem tsmUnsoldProdReport;

		private ToolStripMenuItem tsmGodownStockRpt;

		private ToolStripMenuItem tsmProRegister;

		private ToolStripMenuItem tsmPriceList;

		private ToolStripMenuItem toolStripMenuItem14;

		private ToolStripMenuItem tsmAccntBook;

		private ToolStripMenuItem tsmCashBook;

		private ToolStripMenuItem tsmBankBook;

		private ToolStripMenuItem tsmDayBook;

		private ToolStripMenuItem tsmPaymentRegister;

		private ToolStripMenuItem tsmReceiptRegister;

		private ToolStripMenuItem tsmJournalRegister;

		private ToolStripMenuItem tsmTransaction;

		private ToolStripMenuItem tsmPartyList;

		private ToolStripMenuItem tsmOutstandingReport;

		private ToolStripMenuItem tsmTrialBalance;

		private ToolStripMenuItem tsmProfitAndLossAccount;

		private ToolStripMenuItem tsmBalanceSheet;

		private ContextMenuStrip cmsStaff;

		private ToolStripMenuItem tsmChangePassword;

		private ToolStripMenuItem tsmRoleAndPrevilage;

		private ToolStripMenuItem tsmCreateUser;

		private ToolStripMenuItem tsmStaffRegistration;

		private ToolStripMenuItem tsmSalaryProcess;

		private ToolStripMenuItem tsmAtte;

		private ContextMenuStrip cmsOption;

		private ToolStripMenuItem tsmExport;

		private ToolStripMenuItem tsmImport;

		private ToolStripMenuItem tsmChangeDate;

		private ToolStripMenuItem tsmReminder;

		private ToolStripMenuItem tsmSettings;

		private ToolStripMenuItem tsmPrintDesigner;

		private ToolStripMenuItem tsmCustomTransaction;

		private ToolStripMenuItem tsmDatabaseBackup;

		private ToolStripMenuItem tsmDatabaseRestore;

		private ToolStripMenuItem tsmDatasetRestore;

		private ToolStripMenuItem tsmDataTransfer;

		private ToolStripMenuItem tsmDayClose;

		private ToolStripMenuItem tsmCalender;

		private ToolStripMenuItem tsmSendMail;

		private ToolStripMenuItem otherOptionToolStripMenuItem;

		private ToolStripMenuItem tsmCalculator;

		private ToolStripMenuItem tsmExcel;

		private ToolStripMenuItem tsmNotepad;

		private ToolStripMenuItem tsmPaint;

		private ToolStripMenuItem tsmVideoTutorial;

		private ToolStripMenuItem qRCodeToolStripMenuItem;

		private ToolStripMenuItem tsmRateFormat;

		private ContextMenuStrip cmsAbout;

		private ToolStripMenuItem tsmAboutUs;

		private ToolStripMenuItem tsmContactUs;

		private ToolStripMenuItem tsmReportBuggs;

		private ContextMenuStrip cmsFirm;

		private ToolStripMenuItem tsmAdminSettings;

		private ToolStripMenuItem tsmCompany;

		private ToolStripMenuItem tsmFYear;

		private ToolStripMenuItem tsmFYSelection;

		private System.Windows.Forms.Timer tmrDateTime;

		private Button btnMinimize;

		private Button btnClose;

		private TabControl tabMdi;

		private FlowLayoutPanel pnlReport;

		private Button btnShowReportPanel;

		private Button btnDashBoard;

		private Button btnCloseCash;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		private Button btnStock;

		private Button btnSalesReport;

		private Button btnInventory;

		private Button btnSupplierReport;

		private Button btnCustomerReport;

		private Button btnConfiguration;

		private ToolTip ttDashboardPOS;

		private Button btnSalesSummary;

		private Button btnReceiptReport;

		private Button btnPaymentReport;

		private Button btnStockReport;

		private Button btnLedgerReport;

		private Button btnPurchaseReport;

		private Button btnProfitReport;

		private Button btnSalesReturnReport;

		private Button btnProductSale;

		private Button btnCategorySale;

		private Button btnBalanceSheet;

		private Button btnProfitAndLoseAccount;

		private Button btnTrialBalance;

		private Button btnOutstandingReport;

		private Button btnSwithOff;

		private Button btnPOSReports;

		private ToolStripMenuItem tsmTestView;

		private ToolStripMenuItem tsmLabelScale;

		private ToolStripMenuItem tsmProductUnitCombination;

		private ToolStripMenuItem tsmBulkReceipt;

		private ToolStripMenuItem tsmReceiptAndPayment;

		private Button btnSalesInvoice;

		private Label lblCompany;

		private TabPage tabPOS;

		private Button btnPaymentAndReceipt;

		public ToolStripMenuItem tsmCustomer;

		private ToolStripMenuItem tsmUpdate;

		public Panel pnlTitle;

		public Button btnPanelShow;

		private ToolStripMenuItem tsmCurrency;

		private ToolStripMenuItem tsmKOTPrinter;

		private ToolStripMenuItem tsmProductPrinter;

		private ToolStripMenuItem tsmFavorite;

		public ToolStripMenuItem tsmChangeTheme;

		private ToolStripMenuItem tsmNote;

		private ToolStripMenuItem tsmRemoteSync;

		private ToolStripMenuItem tsmKeyTransfer;

		private Button btnKeyboard;

		public DashboardPOS(int _iMod = 0)
		{
			
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			
			AniHelper.IconChangeMethod(this);
			iMod = _iMod;
			tabMdi.ItemSize = new Size(100, 30);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			resolution = Screen.PrimaryScreen.Bounds;
			Utility.uResolutionWidth = resolution.Width;
			Utility.uResolutionHeight = resolution.Height;
			int num = (Utility.uResolutionHeight - pnlTitle.Height) / 17;
			foreach (Button control in pnlReport.Controls)
			{
				control.Height = num;
				control.Width = 163;
			}
			int num2 = (Utility.uResolutionHeight - pnlTitle.Height) / 15;
			foreach (Button control2 in pnlNavigation.Controls)
			{
				control2.Height = num2;
				control2.Width = 169;
			}
			MDILoadMethod();
			if (Settings.Default.Language == "ar")
			{
				arwMenuShow = ">";
				arwMenuHide = "<";
				arwReportShow = "<";
				arwReportHide = ">";
			}
		}

		private void FocusMethod()
		{
			if (dpOSView != null)
			{
				if (CommonSettings.blnPosBarcodeBased)
				{
					dpOSView.txtBarcode.Focus();
				}
				else
				{
					dpOSView.txtsearchitemname.Focus();
				}
			}
			else if (pOSView != null)
			{
				pOSView.txtBarcode.Focus();
			}
		}

		private void MDILoadMethod()
		{
			try
			{
				if (iMod == 0)
				{
					tabPOS.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
					pOSView = new POSView(this);
					pOSView.TopLevel = false;
					tabPOS.Controls.Add(pOSView);
					pOSView.Show();
					pOSView.Focus();
					pOSView.BringToFront();
				}
				else if (iMod == 1)
				{
					tabPOS.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
					dpOSView = new DashboardPOSView();
					dpOSView.dashboardPOS = this;
					dpOSView.previlegeDtlPOS = AniHelper.GetPrevilageMethod(null, "POS");
					dpOSView.TopLevel = false;
					tabPOS.Controls.Add(dpOSView);
					dpOSView.Show();
					dpOSView.Focus();
					dpOSView.BringToFront();
				}
				else if (iMod == 2)
				{
					tabPOS.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
					DashboardView dashboardView = new DashboardView(this);
					dashboardView.TopLevel = false;
					tabPOS.Controls.Add(dashboardView);
					dashboardView.Show();
					dashboardView.Focus();
					dashboardView.BringToFront();
				}
				else if (iMod == 3)
				{
					CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
					btnStock.Text = MessageKeys.Restaurant;
					tabPOS.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
					RestaurantPOSView restaurantPOSView = new RestaurantPOSView(this);
					restaurantPOSView.TopLevel = false;
					restaurantPOSView.dashboardPOS = this;
					restaurantPOSView.previlegeDtlPOS = AniHelper.GetPrevilageMethod(null, "POS");
					tabPOS.Controls.Add(restaurantPOSView);
					restaurantPOSView.Show();
					restaurantPOSView.Focus();
					restaurantPOSView.BringToFront();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ColorChangeMethod()
		{
			pnlTitle.BackColor = Settings.Default.TitlebarPB;
			pnlTitle.ForeColor = Settings.Default.TitlebarPF;
		}

		public void OpenForm(Form frm, int X = 25, int Y = 25, string sForm = null, bool isEnabled = true)
		{
			try
			{
				btnShowReportPanel.Text = "<";
				pnlReport.Visible = false;
				btnPanelShow.Text = ">";
				pnlNavigation.Visible = false;
				if (!isEnabled)
				{
					Messages.InformationMessage("You have no authorisation to open this Window!");
					return;
				}
				if (frm.IsDisposed)
				{
					Type type = frm.GetType();
					Form form = (Form)Activator.CreateInstance(type);
					form.TopLevel = false;
					TabPage tabPage = new TabPage();
					tabPage.Name = frm.Name;
					tabPage.Text = sForm;
					tabPage.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
					if (Utility.uManufacturer == "DeskCloud ERP")
					{
						if (Settings.Default.IsActivated)
						{
							//tabPage.BackgroundImage = Resources.wall;
							Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");

							tabPage.BackgroundImage = image;
						}
						else
						{
							Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");
							tabPage.BackgroundImage = image;
							//tabPage.BackgroundImage = Resources.wall_demo;
						}
					}
					else
					{
						Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");

						tabPage.BackgroundImage = image;
						//tabPage.BackgroundImage = Resources.wall_360_1;
					}
					tabPage.BackgroundImageLayout = ImageLayout.Stretch;
					tabPage.BackColor = Color.FromArgb(32, 30, 45);
					tabMdi.TabPages.Add(tabPage);
					tabMdi.SelectedTab = tabPage;
					tabPage.Controls.Add(form);
					form.Location = new Point(X, Y);
					frm.FormClosing += Frm_FormClosing;
					form.Show();
					form.Focus();
					form.BringToFront();
					return;
				}
				frm.TopLevel = false;
				TabPage tabPage2 = new TabPage();
				tabPage2.Name = frm.Name;
				tabPage2.Text = sForm;
				tabPage2.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
				if (Utility.uManufacturer == "DeskCloud ERP")
				{
					if (Settings.Default.IsActivated)
					{
						Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");

						tabPage2.BackgroundImage = image;
						//tabPage2.BackgroundImage = Resources.wall;
					}
					else
					{
						Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");
						tabPage2.BackgroundImage = image;
						//tabPage2.BackgroundImage = Resources.wall_demo;
					}
				}
				else
				{
					Image image = Image.FromFile(Application.StartupPath + "\\Images\\wall_demo.jpg");

					tabPage2.BackgroundImage = image;
					//tabPage2.BackgroundImage = Resources.wall_360_1;
				}
				tabPage2.BackgroundImageLayout = ImageLayout.Stretch;
				tabPage2.BackColor = Color.FromArgb(32, 30, 45);
				tabMdi.TabPages.Add(tabPage2);
				tabMdi.SelectedTab = tabPage2;
				tabPage2.Controls.Add(frm);
				frm.Location = new Point(X, Y);
				frm.FormClosing += Frm_FormClosing;
				frm.Show();
				frm.Focus();
				frm.BringToFront();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Frm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Form form = (Form)sender;
			if (form.Parent is TabPage)
			{
				TabPage value = (TabPage)form.Parent;
				int selectedIndex = tabMdi.SelectedIndex;
				tabMdi.TabPages.Remove(value);
				tabMdi.SelectedIndex = selectedIndex - 1;
			}
			FocusMethod();
		}

		private void OpenFormSDF(Form frm)
		{
			try
			{
				btnPanelShow.Text = ">";
				pnlNavigation.Visible = false;
				Type type = frm.GetType();
				Form form = (Form)Activator.CreateInstance(type);
				form.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FinancialPeriodMethod()
		{
			try
			{
				SelectedFYear selectedFYear = db.SelectedFYears().FirstOrDefault();
				if (selectedFYear != null)
				{
					int fyearId = selectedFYear.SlectedFY.ToInt32();
					FinancialYear financialYear = db.FinancialYears(new
					{
						FyearId = fyearId
					}).FirstOrDefault();
					DateTime dateTime = financialYear.DateTo.toDateTime();
					if (dateTime < DateTime.Now)
					{
						fPeriod = new FinancialPeriod();
						OpenForm(fPeriod, 180, 80);
					}
				}
				else
				{
					int year = DateTime.Now.Year;
					int num = (DateTime.Now.DayOfYear - 1) * -1;
					int num2 = new DateTime(year, 12, 31).DayOfYear - 1;
					DateTime value = DateTime.Now.AddDays(num);
					DateTime value2 = value.AddDays(num2);
					fPeriod = new FinancialPeriod();
					fPeriod.dtmFrom.Value = value;
					fPeriod.dtmTo.Value = value2;
					fPeriod.txtFYName.Text = year.ToString();
					fPeriod.isFirstOpening = true;
					fPeriod.ActiveControl = fPeriod.dtmFrom;
					OpenForm(fPeriod, 180, 80);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void NavPanelMaxMethod()
		{
			pnlNavigation.Size = new Size(173, pnlNavigation.Height);
		}

		private void AppExitMethod()
		{
			Hide();
			try
			{
				foreach (TabPage tabPage in tabMdi.TabPages)
				{
					if (!(tabPage.Name != tabPOS.Name))
					{
						continue;
					}
					foreach (Control control in tabPage.Controls)
					{
						if (control is Form)
						{
							Form form = (Form)control;
							form.Close();
						}
					}
				}
				foreach (Form openForm in Application.OpenForms)
				{
					if (openForm.Name != "DashboardPOS")
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

		private void AutoBackUpMethod()
		{
			try
			{
				Hide();
				if (Settings.Default.isServer)
				{
					dbAutoBackup = new DatabaseAutoBackup();
					if (dbAutoBackup.ShowDialog() == DialogResult.OK)
					{
						AppExitMethod();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void tsmAboutUs_Click(object sender, EventArgs e)
		{
			aboutus = new About();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(aboutus.Name);
			if (previlageMethod != null)
			{
				OpenForm(aboutus, 25, 25, "About", previlageMethod.Enable);
			}
			else
			{
				OpenForm(aboutus, 25, 25, "About");
			}
		}

		private void tsmContactUs_Click(object sender, EventArgs e)
		{
			contactus = new ContactUs();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(contactus.Name);
			if (previlageMethod != null)
			{
				OpenForm(contactus, 25, 25, "Contact Us", previlageMethod.Enable);
			}
			else
			{
				OpenForm(contactus);
			}
		}

		private void tsmReportBuggs_Click(object sender, EventArgs e)
		{
			reportbuggs = new ReportBugs();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(reportbuggs.Name);
			if (previlageMethod != null)
			{
				OpenForm(reportbuggs, 25, 25, "Report Bugs", previlageMethod.Enable);
			}
			else
			{
				OpenForm(reportbuggs, 25, 25, "Report Bugs");
			}
		}

		private void tsmKeyTransfer_Click(object sender, EventArgs e)
		{
			ActivationTransfer activationTransfer = new ActivationTransfer();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(activationTransfer.Name);
			if (previlageMethod != null)
			{
				OpenForm(activationTransfer, 25, 25, "Activation Transfer", previlageMethod.Enable);
			}
			else
			{
				OpenForm(activationTransfer, 25, 25, "Activation Transfer");
			}
		}

		private void tsmLabelScale_Click(object sender, EventArgs e)
		{
			LabelScale labelScale = new LabelScale();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(labelScale.Name);
			if (previlageMethod != null)
			{
				OpenForm(labelScale, 25, 25, "Label Scale", previlageMethod.Enable);
			}
			else
			{
				OpenForm(labelScale, 25, 25, "Label Scale");
			}
		}

		private void tsmTestView_Click(object sender, EventArgs e)
		{
		}

		private void tsmCustomTransaction_Click(object sender, EventArgs e)
		{
			cusTranRpt = new CustomTransReport();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(cusTranRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(cusTranRpt, 25, 25, "Custom Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(cusTranRpt);
			}
		}

		private void tsmExport_Click(object sender, EventArgs e)
		{
			export = new ExportAndImport("Export");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ExportData");
			if (previlageMethod != null)
			{
				OpenForm(export, 25, 25, "ExportData", previlageMethod.Enable);
			}
			else
			{
				OpenForm(export, 25, 25, "ExportData");
			}
		}

		private void tsmImport_Click(object sender, EventArgs e)
		{
			import = new ExportAndImport("Import");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ImportData");
			if (previlageMethod != null)
			{
				OpenForm(import, 25, 25, "ImportData", previlageMethod.Enable);
			}
			else
			{
				OpenForm(import, 25, 25, "ImportData");
			}
		}

		private void tsmChangeDate_Click(object sender, EventArgs e)
		{
			changeDate = new ChangeDate();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(changeDate.Name);
			if (previlageMethod != null)
			{
				OpenForm(changeDate, 25, 25, "Change Date", previlageMethod.Enable);
			}
			else
			{
				OpenForm(changeDate, 25, 25, "Change Date");
			}
		}

		private void tsmSettings_Click(object sender, EventArgs e)
		{
			appSettings = new Application_Settings();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(appSettings.Name);
			if (previlageMethod != null)
			{
				OpenForm(appSettings, 25, 25, "Settings", previlageMethod.Enable);
			}
			else
			{
				OpenForm(appSettings, 25, 25, "Settings");
			}
		}

		private void tsmReminder_Click(object sender, EventArgs e)
		{
			reminder = new ReminderView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(reminder.Name);
			if (previlageMethod != null)
			{
				OpenForm(reminder, 25, 25, "Reminder", previlageMethod.Enable);
			}
			else
			{
				OpenForm(reminder, 25, 25, "Reminder");
			}
		}

		private void tsmPrintDesigner_Click(object sender, EventArgs e)
		{
			printDesign = new PrintDesignView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(printDesign.Name);
			if (previlageMethod != null)
			{
				OpenForm(printDesign, 25, 25, "Print Designer", previlageMethod.Enable);
			}
			else
			{
				OpenForm(printDesign, 25, 25, "Print Designer");
			}
		}

		private void tsmDatabaseBackup_Click(object sender, EventArgs e)
		{
			backupDB = new BackupDatabase("Backup");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(backupDB.Name);
			if (previlageMethod != null)
			{
				OpenForm(backupDB, 25, 25, "Backup", previlageMethod.Enable);
			}
			else
			{
				OpenForm(backupDB, 25, 25, "Backup");
			}
		}

		private void tsmDatabaseRestore_Click(object sender, EventArgs e)
		{
			restoreDB = new BackupDatabase("Restore");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "RestoreDatabase");
			if (previlageMethod != null)
			{
				OpenForm(restoreDB, 25, 25, "RestoreDatabase", previlageMethod.Enable);
			}
			else
			{
				OpenForm(restoreDB, 25, 25, "RestoreDatabase");
			}
		}

		private void tsmDatasetRestore_Click(object sender, EventArgs e)
		{
			ResetDatabase resetDatabase = new ResetDatabase();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "DatabaseReset");
			if (previlageMethod != null)
			{
				OpenForm(resetDatabase, 25, 25, "Database Reset", previlageMethod.Enable);
			}
			else
			{
				OpenForm(resetDatabase, 25, 25, "Database Reset");
			}
		}

		private void tsmDataTransfer_Click(object sender, EventArgs e)
		{
			billGeneration = new BillGeneration();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(billGeneration.Name);
			if (previlageMethod != null)
			{
				OpenForm(billGeneration, 25, 25, "Bill Generation", previlageMethod.Enable);
			}
			else
			{
				OpenForm(billGeneration, 25, 25, "Bill Generation");
			}
		}

		private void tsmDayClose_Click(object sender, EventArgs e)
		{
			dayClose = new DayClose();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(dayClose.Name);
			if (previlageMethod != null)
			{
				OpenForm(dayClose, 25, 25, "Day Close", previlageMethod.Enable);
			}
			else
			{
				OpenForm(dayClose, 25, 25, "Day Close");
			}
		}

		private void tsmCalender_Click(object sender, EventArgs e)
		{
			calendar = new Calendar();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(calendar.Name);
			if (previlageMethod != null)
			{
				OpenForm(calendar, 25, 25, "Calendar", previlageMethod.Enable);
			}
			else
			{
				OpenForm(calendar, 25, 25, "Calendar");
			}
		}

		private void tsmSendMail_Click(object sender, EventArgs e)
		{
			sendMail = new SendMail();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(sendMail.Name);
			if (previlageMethod != null)
			{
				OpenForm(sendMail, 25, 25, "Mail", previlageMethod.Enable);
			}
			else
			{
				OpenForm(sendMail, 25, 25, "Mail");
			}
		}

		private void tsmVideoTutorial_Click(object sender, EventArgs e)
		{
			videoTutorial = new VideoTutorial();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(videoTutorial.Name);
			if (previlageMethod != null)
			{
				OpenForm(videoTutorial, 25, 25, "Video Tutorial", previlageMethod.Enable);
			}
			else
			{
				OpenForm(videoTutorial, 25, 25, "Video Tutorial");
			}
		}

		private void tsmCalculator_Click(object sender, EventArgs e)
		{
			calcu = new Calculator();
			OpenFormSDF(calcu);
		}

		private void tsmExcel_Click(object sender, EventArgs e)
		{
			Process.Start("Excel.exe");
		}

		private void tsmNotepad_Click(object sender, EventArgs e)
		{
			Process.Start("Notepad.exe");
		}

		private void tsmPaint_Click(object sender, EventArgs e)
		{
			Process.Start("mspaint.exe");
		}

		private void qRCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(qRCodeGenerator.Name);
			if (previlageMethod != null)
			{
				OpenForm(qRCodeGenerator, 25, 25, "QRCode", previlageMethod.Enable);
			}
			else
			{
				OpenForm(qRCodeGenerator, 25, 25, "QRCode");
			}
		}

		private void tsmRateFormat_Click(object sender, EventArgs e)
		{
			RateFormatView rateFormatView = new RateFormatView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(rateFormatView.Name);
			if (previlageMethod != null)
			{
				OpenForm(rateFormatView, 25, 25, "Rate Format", previlageMethod.Enable);
			}
			else
			{
				OpenForm(rateFormatView, 25, 25, "Rate Format");
			}
		}

		public void tsmProductPrinter_Click(object sender, EventArgs e)
		{
			ProductPrinterAllocation productPrinterAllocation = new ProductPrinterAllocation();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(productPrinterAllocation.Name);
			if (previlageMethod != null)
			{
				OpenForm(productPrinterAllocation, 25, 25, "Printer Allocation", previlageMethod.Enable);
			}
			else
			{
				OpenForm(productPrinterAllocation, 25, 25, "Printer Allocation");
			}
		}

		public void tsmChangeTheme_Click(object sender, EventArgs e)
		{
			btnShowReportPanel.Text = "<";
			pnlReport.Visible = false;
			btnPanelShow.Text = ">";
			pnlNavigation.Visible = false;
			ThemeChanger themeChanger = new ThemeChanger();
			themeChanger.ShowDialog();
		}

		public void tsmFavorite_Click(object sender, EventArgs e)
		{
			FavouriteProduct favouriteProduct = new FavouriteProduct();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(favouriteProduct.Name);
			if (previlageMethod != null)
			{
				OpenForm(favouriteProduct, 25, 25, "Favourite", previlageMethod.Enable);
			}
			else
			{
				OpenForm(favouriteProduct, 25, 25, "Favourite");
			}
		}

		private void tsmRemoteSync_Click(object sender, EventArgs e)
		{
			RemoteSyncView remoteSyncView = new RemoteSyncView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(remoteSyncView.Name);
			if (previlageMethod != null)
			{
				OpenForm(remoteSyncView, 25, 25, "Remote Sync", previlageMethod.Enable);
			}
			else
			{
				OpenForm(remoteSyncView);
			}
		}

		private void tsmChangePassword_Click(object sender, EventArgs e)
		{
			changePWord = new ChangePassword();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(changePWord.Name);
			if (previlageMethod != null)
			{
				OpenForm(changePWord, 25, 25, "Password Change", previlageMethod.Enable);
			}
			else
			{
				OpenForm(changePWord, 25, 25, "Password Change");
			}
		}

		private void tsmRoleAndPrevilage_Click(object sender, EventArgs e)
		{
			uPrevilege = new UserPrevilegeView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(uPrevilege.Name);
			if (previlageMethod != null)
			{
				OpenForm(uPrevilege, 25, 25, "User Previlage", previlageMethod.Enable);
			}
			else
			{
				OpenForm(uPrevilege, 25, 25, "User Previlage");
			}
		}

		private void tsmCreateUser_Click(object sender, EventArgs e)
		{
			uManagement = new UserManagement();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(uManagement.Name);
			if (previlageMethod != null)
			{
				OpenForm(uManagement, 25, 25, "User", previlageMethod.Enable);
			}
			else
			{
				OpenForm(uManagement, 25, 25, "User");
			}
		}

		private void tsmStaffRegistration_Click(object sender, EventArgs e)
		{
			empDetails = new EmployeeDetails();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(empDetails.Name);
			if (previlageMethod != null)
			{
				OpenForm(empDetails, 25, 25, "Employee", previlageMethod.Enable);
			}
			else
			{
				OpenForm(empDetails, 25, 25, "Employee");
			}
		}

		private void tsmSalaryProcess_Click(object sender, EventArgs e)
		{
			salProcess = new SalaryProcessView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(salProcess.Name);
			if (previlageMethod != null)
			{
				OpenForm(salProcess, 25, 25, "Salary", previlageMethod.Enable);
			}
			else
			{
				OpenForm(salProcess, 25, 25, "Salary");
			}
		}

		private void tsmAtte_Click(object sender, EventArgs e)
		{
			attendance = new StaffAttendanceView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(attendance.Name);
			if (previlageMethod != null)
			{
				OpenForm(attendance, 25, 25, "Attendance", previlageMethod.Enable);
			}
			else
			{
				OpenForm(attendance, 25, 25, "Attendance");
			}
		}

		private void tsmGrpWiseSlsReport_Click(object sender, EventArgs e)
		{
			Utility.iReportFormNumber = 17;
			POSReportView pOSReportView = new POSReportView();
			OpenForm(pOSReportView);
		}

		private void tsmPCategoryWiseSlsReport_Click(object sender, EventArgs e)
		{
			inventorySRegister = new InventorySalesRegisterView("PCategory Sales");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PriceCategoryWiseSalesReport");
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegister, 25, 25, "PriceCategoryWiseSalesReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegister, 25, 25, "PriceCategoryWiseSalesReport");
			}
		}

		private void tsmRtwiseSlsReport_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "RouteWiseSalesReport");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "RouteWiseSalesReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "RouteWiseSalesReport");
			}
		}

		private void tsmSMWiseSlsReport_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesmanWiseSalesReport");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "SalesmanWiseSalesReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "SalesmanWiseSalesReport");
			}
		}

		private void tsmPartyWiseSlsReport_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PartyWiseSalesReport");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "PartyWiseSalesReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "PartyWiseSalesReport");
			}
		}

		private void tsmGrpWisePurRpt_Click(object sender, EventArgs e)
		{
			inventorySRegisterPurchase = new InventorySalesRegisterView("Purchase");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "GroupWisePurchaseReport");
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegisterPurchase, 25, 25, "GroupWisePurchaseReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegisterPurchase, 25, 25, "GroupWisePurchaseReport");
			}
		}

		private void tsmSplrWsPurRpt_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SupplierWisePurchaseReport");
			slsSummaryRpt.VoucherType = "Purchase";
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "SupplierWisePurchaseReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "SupplierWisePurchaseReport");
			}
		}

		private void tsmExiCollRpt_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Exicutivewise Collection Report";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ExicutiveWiseCollectionReport");
			if (previlageMethod != null)
			{
				OpenForm(payRecRpt, 25, 25, "ExicutiveWiseCollectionReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(payRecRpt, 25, 25, "ExicutiveWiseCollectionReport");
			}
		}

		private void tsmPendingOrdrRpt_Click(object sender, EventArgs e)
		{
			pendingOR = new PendingOrderReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(pendingOR.Name);
			if (previlageMethod != null)
			{
				OpenForm(pendingOR, 25, 25, "Pending Order", previlageMethod.Enable);
			}
			else
			{
				OpenForm(pendingOR, 25, 25, "Pending Order");
			}
		}

		private void tsmSlsSummary_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(slsSummaryRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "Sales Summary", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "Sales Summary");
			}
		}

		private void tsmSlsRtnSummary_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Sales Return";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesReturnSummary");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "SalesReturnSummary", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "SalesReturnSummary");
			}
		}

		private void tsmPurSummary_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Purchase";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PurchaseSummaryReport");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "PurchaseSummaryReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "PurchaseSummaryReport");
			}
		}

		private void tsmPurRtnSummary_Click(object sender, EventArgs e)
		{
			slsSummaryRpt = new SalesSummaryReportView();
			slsSummaryRpt.VoucherType = "Purchase Return";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PurchaseReturnSummaryReport");
			if (previlageMethod != null)
			{
				OpenForm(slsSummaryRpt, 25, 25, "PurchaseReturnSummaryReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsSummaryRpt, 25, 25, "PurchaseReturnSummaryReport");
			}
		}

		private void tsmSalesReport_Click(object sender, EventArgs e)
		{
			saleEstimateReport = new SalesReportView();
			saleEstimateReport.PassingValue = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesReport");
			if (previlageMethod != null)
			{
				OpenForm(saleEstimateReport, 10, 10, "SalesReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(saleEstimateReport, 10, 10, "SalesReport");
			}
		}

		private void tsmRptSalesEstimate_Click(object sender, EventArgs e)
		{
			saleEstimateReport = new SalesReportView();
			saleEstimateReport.PassingValue = "Normal Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesEstimateReport");
			if (previlageMethod != null)
			{
				OpenForm(saleEstimateReport, 25, 25, "SalesEstimateReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(saleEstimateReport, 25, 25, "SalesEstimateReport");
			}
		}

		private void tsmRptSalesReturn_Click(object sender, EventArgs e)
		{
			saleReturnReport = new SalesReturnReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(saleReturnReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(saleReturnReport, 25, 25, "Sales Return", previlageMethod.Enable);
			}
			else
			{
				OpenForm(saleReturnReport, 25, 25, "Sales Return");
			}
		}

		private void tsmRptSalesOrder_Click(object sender, EventArgs e)
		{
			SlsOrderRpt = new SalesOrderReportView("Sales Order Report");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(SlsOrderRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(SlsOrderRpt, 25, 25, "Sales Order", previlageMethod.Enable);
			}
			else
			{
				OpenForm(SlsOrderRpt, 25, 25, "Sales Order");
			}
		}

		private void tsmRptSalesQuotation_Click(object sender, EventArgs e)
		{
			SlsQuotationRpt = new SalesOrderReportView("Sales Quatation Report");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesQuatationReport");
			if (previlageMethod != null)
			{
				OpenForm(SlsQuotationRpt, 25, 25, "SalesQuatationReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(SlsQuotationRpt, 25, 25, "SalesQuatationReport");
			}
		}

		private void tsmRptPurchase_Click(object sender, EventArgs e)
		{
			purchaseReport = new PurchaseReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(purchaseReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(purchaseReport, 25, 25, "Purchase Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(purchaseReport, 25, 25, "Purchase Report");
			}
		}

		private void tsmRptPurchaseReturn_Click(object sender, EventArgs e)
		{
			purchaseReturnReport = new PurchaseReturnReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(purchaseReturnReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(purchaseReturnReport, 25, 25, "Purchase Return Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(purchaseReturnReport, 25, 25, "Purchase Return Report");
			}
		}

		private void tsmRptPurchaseOrder_Click(object sender, EventArgs e)
		{
			pOrderRpt = new SalesOrderReportView("Purchase Order Report");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PurchaseOrderReport");
			if (previlageMethod != null)
			{
				OpenForm(pOrderRpt, 25, 25, "PurchaseOrderReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(pOrderRpt, 25, 25, "PurchaseOrderReport");
			}
		}

		private void tsmRptSalesAndSalesReturn_Click(object sender, EventArgs e)
		{
			salesAndReturn = new SalesAndSalesReturnReportView();
			salesAndReturn.ValueDetails = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(salesAndReturn.Name);
			if (previlageMethod != null)
			{
				OpenForm(salesAndReturn, 25, 25, "Sales & Return Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(salesAndReturn, 25, 25, "Sales & Return Report");
			}
		}

		private void tsmRptPurchaseAndPurchaseReturn_Click(object sender, EventArgs e)
		{
			salesAndReturn = new SalesAndSalesReturnReportView();
			salesAndReturn.ValueDetails = "Purchase";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PurchaseAndPurchaseReturnReport");
			if (previlageMethod != null)
			{
				OpenForm(salesAndReturn, 25, 25, "PurchaseAndPurchaseReturnReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(salesAndReturn, 25, 25, "PurchaseAndPurchaseReturnReport");
			}
		}

		private void tsmRptCollection_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.ValueDetails = "Receipt Report";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "CollectionReport");
			if (previlageMethod != null)
			{
				OpenForm(payRecRpt, 25, 25, "CollectionReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(payRecRpt, 25, 25, "CollectionReport");
			}
		}

		private void tsmRptTaxSales_Click(object sender, EventArgs e)
		{
			gstSaleRpt = new GSTSalesRptView("Sales");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(gstSaleRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(gstSaleRpt, 25, 25, "Tax Sales Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(gstSaleRpt, 25, 25, "Tax Sales Report");
			}
		}

		private void tsmRptTaxPurchase_Click(object sender, EventArgs e)
		{
			gstPurRpt = new GSTSalesRptView("Purchase");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "GSTPurchaseRpt");
			if (previlageMethod != null)
			{
				OpenForm(gstPurRpt, 25, 25, "GSTPurchaseRpt", previlageMethod.Enable);
				OpenForm(stockRpt, 25, 25, null, previlageMethod.Enable);
			}
			else
			{
				OpenForm(gstPurRpt, 25, 25, "GSTPurchaseRpt");
			}
		}

		private void tsmRptStock_Click(object sender, EventArgs e)
		{
			stockRpt = new StockReportView(this);
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(stockRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(stockRpt, 25, 25, "Stock Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(stockRpt, 25, 25, "Stock Report");
			}
		}

		private void tsmItemWiseSalesReport_Click(object sender, EventArgs e)
		{
			itemWiseSReport = new ItemWiseSalesReportView();
			itemWiseSReport.strFormType = "Sales";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(itemWiseSReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(itemWiseSReport, 25, 25, "Item Wise Sales", previlageMethod.Enable);
			}
			else
			{
				OpenForm(itemWiseSReport, 25, 25, "Item Wise Sales");
			}
		}

		private void tsmItemWisePurchaseReport_Click(object sender, EventArgs e)
		{
			itemWiseSReportP = new ItemWiseSalesReportView();
			itemWiseSReportP.strFormType = "Purchase";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ItemWisePurchaseReport");
			if (previlageMethod != null)
			{
				OpenForm(itemWiseSReportP, 25, 25, "ItemWisePurchaseReport", previlageMethod.Enable);
			}
			else
			{
				OpenForm(itemWiseSReportP, 25, 25, "ItemWisePurchaseReport");
			}
		}

		private void tsmProductLedger_Click(object sender, EventArgs e)
		{
			productLedgerReport = new ProductLedgerReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(productLedgerReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(productLedgerReport, 25, 25, "Product Ledger", previlageMethod.Enable);
			}
			else
			{
				OpenForm(productLedgerReport, 25, 25, "Product Ledger");
			}
		}

		private void tsmStockTransferReport_Click(object sender, EventArgs e)
		{
			stockTranRpt = new StockTrnasferReportView("Stock Transfer Report");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(stockTranRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(stockTranRpt, 25, 25, "Stock Transfer Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(stockTranRpt, 25, 25, "Stock Transfer Report");
			}
		}

		private void cmsInvSlsRegister_Click(object sender, EventArgs e)
		{
			inventorySRegister = new InventorySalesRegisterView("Sales");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(inventorySRegister.Name);
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegister, 25, 25, "Sales Register", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegister, 25, 25, "Sales Register");
			}
		}

		private void tsmInvtSlsReturnReport_Click(object sender, EventArgs e)
		{
			inventorySRegisterSalesReturn = new InventorySalesRegisterView("Sales Return");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "InventorySalesReturnRegister");
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegisterSalesReturn, 25, 25, "InventorySalesReturnRegister", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegisterSalesReturn, 25, 25, "InventorySalesReturnRegister");
			}
		}

		private void tsmInventoryPurRegister_Click(object sender, EventArgs e)
		{
			inventorySRegisterPurchase = new InventorySalesRegisterView("Purchase");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "InventoryPurchaseRegister");
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegisterPurchase, 25, 25, "InventoryPurchaseRegister", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegisterPurchase, 25, 25, "InventoryPurchaseRegister");
			}
		}

		private void tsmInventoryPurRtnRegister_Click(object sender, EventArgs e)
		{
			inventorySRegisterPurchaseReturn = new InventorySalesRegisterView("Purchase Return");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "InventoryPurchaseReturnRegister");
			if (previlageMethod != null)
			{
				OpenForm(inventorySRegisterPurchaseReturn, 25, 25, "InventoryPurchaseReturnRegister", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventorySRegisterPurchaseReturn, 25, 25, "InventoryPurchaseReturnRegister");
			}
		}

		private void tsmProFlowReport_Click(object sender, EventArgs e)
		{
			PFlowRpt = new Product_Flow_Report_Settings();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(PFlowRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(PFlowRpt, 25, 25, "Flow Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(PFlowRpt, 25, 25, "Flow Report");
			}
		}

		private void tsmUnsoldProdReport_Click(object sender, EventArgs e)
		{
			Unsoldproductreport = new UnSoldProductReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(Unsoldproductreport.Name);
			if (previlageMethod != null)
			{
				OpenForm(Unsoldproductreport, 25, 25, "Unsold Product", previlageMethod.Enable);
			}
			else
			{
				OpenForm(Unsoldproductreport, 25, 25, "Unsold Product");
			}
		}

		private void tsmGodownStockRpt_Click(object sender, EventArgs e)
		{
			godownStkReport = new GodownStockReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(godownStkReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(godownStkReport, 25, 25, "Deport Stock", previlageMethod.Enable);
			}
			else
			{
				OpenForm(godownStkReport, 25, 25, "Deport Stock");
			}
		}

		private void tsmPriceList_Click(object sender, EventArgs e)
		{
			priceListReport = new PriceListView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(godownStkReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(priceListReport, 25, 25, "Price List", previlageMethod.Enable);
			}
			else
			{
				OpenForm(priceListReport, 25, 25, "Price List");
			}
		}

		private void tsmProRegister_Click(object sender, EventArgs e)
		{
			proRRegisterSettings = new Product_Register_Settings();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(proRRegisterSettings.Name);
			if (previlageMethod != null)
			{
				OpenForm(proRRegisterSettings, 25, 25, "Product Register", previlageMethod.Enable);
			}
			else
			{
				OpenForm(proRRegisterSettings, 25, 25, "Product Register");
			}
		}

		private void tsmTransaction_Click(object sender, EventArgs e)
		{
			transactionReport = new TransactionReportView();
			transactionReport.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(transactionReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(transactionReport, 25, 25, "Transaction Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(transactionReport, 25, 25, "Transaction Report");
			}
		}

		private void tsmAccntBook_Click(object sender, EventArgs e)
		{
			ledgerReport = new LedgerReport();
			ledgerReport.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(ledgerReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(ledgerReport, 25, 25, "Ledger Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(ledgerReport, 25, 25, "Ledger Report");
			}
		}

		private void tsmCashBook_Click(object sender, EventArgs e)
		{
			ledgerReport = new LedgerReport();
			ledgerReport.form = this;
			ledgerReport.ValueLedger = "CASH IN HAND";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "CashBook");
			if (previlageMethod != null)
			{
				OpenForm(ledgerReport, 25, 25, "CashBook", previlageMethod.Enable);
			}
			else
			{
				OpenForm(ledgerReport, 25, 25, "CashBook");
			}
		}

		private void tsmBankBook_Click(object sender, EventArgs e)
		{
			ledgerReport = new LedgerReport();
			ledgerReport.form = this;
			ledgerReport.ValueLedger = "CASH AT BANK";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "BankBook");
			if (previlageMethod != null)
			{
				OpenForm(ledgerReport, 25, 25, "BankBook", previlageMethod.Enable);
			}
			else
			{
				OpenForm(ledgerReport, 25, 25, "BankBook");
			}
		}

		private void tsmDayBook_Click(object sender, EventArgs e)
		{
			dayBook = new DailyAccountView();
			dayBook.PassingValue = "Summary";
			dayBook.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(dayBook.Name);
			if (previlageMethod != null)
			{
				OpenForm(dayBook, 25, 25, "Day Book", previlageMethod.Enable);
			}
			else
			{
				OpenForm(dayBook, 25, 25, "Day Book");
			}
		}

		private void tsmPaymentRegister_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.form = this;
			payRecRpt.ValueDetails = "Payment Report";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PaymentRegister");
			if (previlageMethod != null)
			{
				OpenForm(payRecRpt, 25, 25, "PaymentRegister", previlageMethod.Enable);
			}
			else
			{
				OpenForm(payRecRpt, 25, 25, "PaymentRegister");
			}
		}

		private void tsmReceiptRegister_Click(object sender, EventArgs e)
		{
			payRecRpt = new PaymentReceiptReportView();
			payRecRpt.form = this;
			payRecRpt.ValueDetails = "Receipt Report";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ReceiptRegister");
			if (previlageMethod != null)
			{
				OpenForm(payRecRpt, 25, 25, "ReceiptRegister", previlageMethod.Enable);
			}
			else
			{
				OpenForm(payRecRpt, 25, 25, "ReceiptRegister");
			}
		}

		private void tsmJournalRegister_Click(object sender, EventArgs e)
		{
			JrnlRegister = new JournalEntryReportView();
			JrnlRegister.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(JrnlRegister.Name);
			if (previlageMethod != null)
			{
				OpenForm(JrnlRegister, 25, 25, "Journal Register", previlageMethod.Enable);
			}
			else
			{
				OpenForm(JrnlRegister, 25, 25, "Journal Register");
			}
		}

		private void tsmPartyList_Click(object sender, EventArgs e)
		{
			partyList = new PartyListView();
			partyList.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(partyList.Name);
			if (previlageMethod != null)
			{
				OpenForm(partyList, 25, 25, "Party List", previlageMethod.Enable);
			}
			else
			{
				OpenForm(partyList, 25, 25, "Party List");
			}
		}

		private void tsmOutstandingReport_Click(object sender, EventArgs e)
		{
			outStandingReport = new OutstandingReportView();
			outStandingReport.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(outStandingReport.Name);
			if (previlageMethod != null)
			{
				OpenForm(outStandingReport, 25, 25, "Outstanding Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(outStandingReport, 25, 25, "Outstanding Report");
			}
		}

		private void tsmTrialBalance_Click(object sender, EventArgs e)
		{
			trailBalance = new TrialBalanceView();
			trailBalance.form = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(trailBalance.Name);
			if (previlageMethod != null)
			{
				OpenForm(trailBalance, 25, 25, "Trial Balance", previlageMethod.Enable);
			}
			else
			{
				OpenForm(trailBalance, 25, 25, "Trial Balance");
			}
		}

		private void tsmProfitAndLossAccount_Click(object sender, EventArgs e)
		{
			pfAccount = new Profit_and_Lose_Account();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(pfAccount.Name);
			if (previlageMethod != null)
			{
				OpenForm(pfAccount, 25, 25, "Profit And Loss", previlageMethod.Enable);
			}
			else
			{
				OpenForm(pfAccount, 25, 25, "Profit And Loss");
			}
		}

		private void tsmBalanceSheet_Click(object sender, EventArgs e)
		{
			bSheet = new Balance_Sheet();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(bSheet.Name);
			if (previlageMethod != null)
			{
				OpenForm(bSheet, 25, 25, "Balance Sheet", previlageMethod.Enable);
			}
			else
			{
				OpenForm(bSheet, 25, 25, "Balance Sheet");
			}
		}

		private void tsmGSTRReport_Click(object sender, EventArgs e)
		{
			proGSTRpt = new ProductWiseGSTReportView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(proGSTRpt.Name);
			if (previlageMethod != null)
			{
				OpenForm(proGSTRpt, 25, 25, "GSTR Report", previlageMethod.Enable);
			}
			else
			{
				OpenForm(proGSTRpt, 25, 25, "GSTR Report");
			}
		}

		private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			salesInvoice = new SalesInvoiceView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(salesInvoice.Name);
			salesInvoice.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(salesInvoice, 25, 25, "Sales Invoice", previlageMethod.Enable);
			}
			else
			{
				OpenForm(salesInvoice, 25, 25, "Sales Invoice");
			}
		}

		private void salesEstimateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saleEstimate = new SalesEstimateView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(saleEstimate.Name);
			saleEstimate.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(saleEstimate, 10, 10, "Sales Estimate", previlageMethod.Enable);
			}
			else
			{
				OpenForm(saleEstimate, 10, 10, "Sales Estimate");
			}
		}

		private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			salesReturn = new SalesReturnView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(salesReturn.Name);
			salesReturn.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(salesReturn, 10, 10, "Sales Return", salesReturn.plgDtls.Enable);
			}
			else
			{
				OpenForm(salesReturn, 10, 10, "Sales Return");
			}
		}

		private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sOrder = new OrderFormView("Sales Order");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "SalesOrder");
			sOrder.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(sOrder, 10, 10, "SalesOrder", previlageMethod.Enable);
			}
			else
			{
				OpenForm(sOrder, 10, 10, "SalesOrder");
			}
		}

		private void salesQuotationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sQuotation = new OrderFormView("Sales Quotation");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Sales Quotation");
			sQuotation.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(sQuotation, 10, 10, "SalesQuotation", previlageMethod.Enable);
			}
			else
			{
				OpenForm(sQuotation, 10, 10, "SalesQuotation");
			}
		}

		private void salesViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			salesView = new SalesView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(salesView.Name);
			if (previlageMethod != null)
			{
				OpenForm(salesView, 10, 10, "Sales View", previlageMethod.Enable);
			}
			else
			{
				OpenForm(salesView, 10, 10, "Sales View");
			}
		}

		private void billPrintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			billPrint = new BillPrint();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(billPrint.Name);
			if (previlageMethod != null)
			{
				OpenForm(billPrint, 10, 10, "Bill Print", previlageMethod.Enable);
			}
			else
			{
				OpenForm(billPrint, 10, 10, "Bill Print");
			}
		}

		private void transactionWithoutInventoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tranWtotInv = new TransactionWithoutInventory();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(tranWtotInv.Name);
			tranWtotInv.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(tranWtotInv, 10, 10, "Transaction", previlageMethod.Enable);
			}
			else
			{
				OpenForm(tranWtotInv, 10, 10, "Transaction");
			}
		}

		public void tsmPurchase_Click(object sender, EventArgs e)
		{
			Purchase = new PurchaseInvoiceView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(Purchase.Name);
			Purchase.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(Purchase, 10, 10, "Purchase", previlageMethod.Enable);
			}
			else
			{
				OpenForm(Purchase, 10, 10, "Purchase");
			}
		}

		private void tsmPurReturn_Click(object sender, EventArgs e)
		{
			purchaseReturn = new PurchaseReturnView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(purchaseReturn.Name);
			purchaseReturn.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(purchaseReturn, 10, 10, "Purchase Return", previlageMethod.Enable);
			}
			else
			{
				OpenForm(purchaseReturn, 10, 10, "Purchase Return");
			}
		}

		private void tsmPurOrder_Click(object sender, EventArgs e)
		{
			pOrder = new OrderFormView("Purchase Order");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "PurchaseOrder");
			pOrder.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(pOrder, 10, 10, "PurchaseOrder", previlageMethod.Enable);
			}
			else
			{
				OpenForm(pOrder, 10, 10, "PurchaseOrder");
			}
		}

		private void tsmFirstPurchase_Click(object sender, EventArgs e)
		{
			FPurchase = new FirstPurchase();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(FPurchase.Name);
			FPurchase.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(FPurchase, 10, 10, "Purchase", previlageMethod.Enable);
			}
			else
			{
				OpenForm(FPurchase, 10, 10, "Purchase");
			}
		}

		private void tsmPacking_Click(object sender, EventArgs e)
		{
			Packing = new ProductCombinationView("Packing");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Packing");
			Packing.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(Packing, 10, 10, "Packing", previlageMethod.Enable);
			}
			else
			{
				OpenForm(Packing, 10, 10, "Packing");
			}
		}

		private void tsmStockTransfer_Click(object sender, EventArgs e)
		{
			stockTransfer = new StockTransferView("Stock Trancfer");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "StockTrancfer");
			stockTransfer.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(stockTransfer, 10, 10, "StockTrancfer", previlageMethod.Enable);
			}
			else
			{
				OpenForm(stockTransfer, 10, 10, "StockTrancfer");
			}
		}

		private void tsmDamageEntry_Click(object sender, EventArgs e)
		{
			DamageEntry = new StockTransferView("Damage Entry");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "DamageEntry");
			DamageEntry.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(DamageEntry, 10, 10, "DamageEntry", previlageMethod.Enable);
			}
			else
			{
				OpenForm(DamageEntry, 10, 10, "DamageEntry");
			}
		}

		public void tsmStockAdjuster_Click(object sender, EventArgs e)
		{
			stockAdjuster = new StockAdjusterView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(stockAdjuster.Name);
			stockAdjuster.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(stockAdjuster, 10, 10, "Stock Adjustment", previlageMethod.Enable);
			}
			else
			{
				OpenForm(stockAdjuster, 10, 10, "Stock Adjustment");
			}
		}

		private void tsmProductEditing_Click(object sender, EventArgs e)
		{
			proEditing = new ProductEditing();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(proEditing.Name);
			if (previlageMethod != null)
			{
				OpenForm(proEditing, 10, 10, "Product Editing", previlageMethod.Enable);
			}
			else
			{
				OpenForm(proEditing, 10, 10, "Product Editing");
			}
		}

		private void tsmProductCombination_Click(object sender, EventArgs e)
		{
			proCombination = new ProductCombinationView("Product Combination");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ProductCombination");
			proCombination.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(proCombination, 10, 10, "ProductCombination", previlageMethod.Enable);
			}
			else
			{
				OpenForm(proCombination, 10, 10, "ProductCombination");
			}
		}

		public void tsmBCodeDesign_Click(object sender, EventArgs e)
		{
			barcode = new BarcodeDesign();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(barcode.Name);
			if (previlageMethod != null)
			{
				OpenForm(barcode, 10, 10, "Barcode Design", previlageMethod.Enable);
			}
			else
			{
				OpenForm(barcode, 10, 10, "Barcode Design");
			}
		}

		public void tsmBCodePrinting_Click(object sender, EventArgs e)
		{
			printBarcode = new PrintBarcode();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(printBarcode.Name);
			if (previlageMethod != null)
			{
				OpenForm(printBarcode, 10, 10, "Barcode Print", previlageMethod.Enable);
			}
			else
			{
				OpenForm(printBarcode, 10, 10, "Barcode Print");
			}
		}

		private void tsmManufacturing_Click(object sender, EventArgs e)
		{
			manufacturing = new ProductManufacturingView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(manufacturing.Name);
			manufacturing.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(manufacturing, 10, 10, "Manufacturing", previlageMethod.Enable);
			}
			else
			{
				OpenForm(manufacturing, 10, 10, "Manufacturing");
			}
		}

		private void tsmProductUnitCombination_Click(object sender, EventArgs e)
		{
			proCombination = new ProductCombinationView("Product Unit Combination");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ProductUnitCombination");
			proCombination.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(proCombination, 10, 10, "ProductUnitCombination", previlageMethod.Enable);
			}
			else
			{
				OpenForm(proCombination, 10, 10, "ProductUnitCombination");
			}
		}

		private void psmCashPayment_Click(object sender, EventArgs e)
		{
			cashPayment = new PaymentView("Cash Payment");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "cashPayment");
			cashPayment.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(cashPayment, 25, 25, "CashPayment", previlageMethod.Enable);
			}
			else
			{
				OpenForm(cashPayment, 25, 25, "CashPayment");
			}
		}

		private void psmCashReceipt_Click(object sender, EventArgs e)
		{
			cashReceipt = new ReceiptView("Cash Receipt");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "CashReceipt");
			cashReceipt.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(cashReceipt, 25, 25, "CashReceipt", previlageMethod.Enable);
			}
			else
			{
				OpenForm(cashReceipt, 25, 25, "CashReceipt");
			}
		}

		private void psmBankPayment_Click(object sender, EventArgs e)
		{
			bankPayment = new PaymentView("Bank Payment");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "BankPayment");
			bankPayment.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(bankPayment, 25, 25, "BankPayment", previlageMethod.Enable);
			}
			else
			{
				OpenForm(bankPayment, 25, 25, "BankPayment");
			}
		}

		private void psmBankreceipt_Click(object sender, EventArgs e)
		{
			bankReceipt = new ReceiptView("Bank Receipt");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "BankReceipt");
			bankReceipt.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(bankReceipt, 25, 25, "BankReceipt", previlageMethod.Enable);
			}
			else
			{
				OpenForm(bankReceipt, 25, 25, "BankReceipt");
			}
		}

		private void psmJournalEntry_Click(object sender, EventArgs e)
		{
			journalEntry = new JournalEntryView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(journalEntry.Name);
			journalEntry.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(journalEntry, 25, 25, "Journal Entry", previlageMethod.Enable);
			}
			else
			{
				OpenForm(journalEntry, 25, 25, "Journal Entry");
			}
		}

		private void tsmOpeningBalance_Click(object sender, EventArgs e)
		{
			openeningBalance = new OpeningBalance();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(openeningBalance.Name);
			openeningBalance.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(openeningBalance, 25, 25, "Opening Balance", previlageMethod.Enable);
			}
			else
			{
				OpenForm(openeningBalance, 25, 25, "Opening Balance");
			}
		}

		private void tsmDebitNote_Click(object sender, EventArgs e)
		{
			debitNote = new DebitNoteView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(debitNote.Name);
			debitNote.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(debitNote, 25, 25, "Debit Note", previlageMethod.Enable);
			}
			else
			{
				OpenForm(debitNote, 25, 25, "Debit Note");
			}
		}

		private void tsmCreditNote_Click(object sender, EventArgs e)
		{
			creditNote = new CreditNoteView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(creditNote.Name);
			creditNote.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(creditNote, 25, 25, "Credit Note", previlageMethod.Enable);
			}
			else
			{
				OpenForm(creditNote, 25, 25, "Credit Note");
			}
		}

		private void tsmPDCReceipt_Click(object sender, EventArgs e)
		{
			cheqReceipt = new ChequeRecipt("Receipt");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ChequeRecipt");
			cheqReceipt.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(cheqReceipt, 10, 10, "ChequeRecipt", previlageMethod.Enable);
			}
			else
			{
				OpenForm(cheqReceipt, 10, 10, "ChequeRecipt");
			}
		}

		private void tsmPDCPayment_Click(object sender, EventArgs e)
		{
			chequePayment = new ChequeRecipt("Payment");
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "ChequePayment");
			chequePayment.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(chequePayment, 10, 10, "ChequePayment", previlageMethod.Enable);
			}
			else
			{
				OpenForm(chequePayment, 10, 10, "ChequePayment");
			}
		}

		private void bankReconciliationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bankReconciliation = new BankReconciliation();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(bankReconciliation.Name);
			if (previlageMethod != null)
			{
				OpenForm(bankReconciliation, 25, 25, "Bank Reconciliation", previlageMethod.Enable);
			}
			else
			{
				OpenForm(bankReconciliation, 25, 25, "Bank Reconciliation");
			}
		}

		private void tsmBulkReceipt_Click(object sender, EventArgs e)
		{
			BulkReceiptView bulkReceiptView = new BulkReceiptView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(bulkReceiptView.Name);
			if (previlageMethod != null)
			{
				OpenForm(bulkReceiptView, 25, 25, "Cash Receipt", previlageMethod.Enable);
			}
			else
			{
				OpenForm(bulkReceiptView, 25, 25, "Cash Receipt");
			}
		}

		private void tsmReceiptAndPayment_Click_1(object sender, EventArgs e)
		{
			PaymentAndReceiptView paymentAndReceiptView = new PaymentAndReceiptView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(paymentAndReceiptView.Name);
			if (previlageMethod != null)
			{
				OpenForm(paymentAndReceiptView, 25, 25, "Payment & Receipt", previlageMethod.Enable);
			}
			else
			{
				OpenForm(paymentAndReceiptView, 25, 25, "Payment & Receipt");
			}
		}

		private void tsmAGroup_Click(object sender, EventArgs e)
		{
			accountGroup = new AccountGroup();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(accountGroup.Name);
			accountGroup.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(accountGroup, 25, 25, "Account Group", previlageMethod.Enable);
			}
			else
			{
				OpenForm(accountGroup, 25, 25, "Account Group");
			}
		}

		private void tsmAccount_Click(object sender, EventArgs e)
		{
			account = new Account();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Account");
			account.plgDtls = previlageMethod;
			account.sFormName = MessageKeys.Chart_Of_Account;
			account.accountGroupName = string.Empty;
			if (previlageMethod != null)
			{
				OpenForm(account, 25, 25, "Account", previlageMethod.Enable);
			}
			else
			{
				OpenForm(account, 25, 25, "Account");
			}
		}

		private void tsmCustomer_Click(object sender, EventArgs e)
		{
			account = new Account();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Customer");
			account.sFormName = MessageKeys.Customers;
			account.accountGroupName = "CUSTOMER/SUNDRY DEBTORS";
			account.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(account, 25, 25, "Customer", previlageMethod.Enable);
			}
			else
			{
				OpenForm(account, 25, 25, "Customer");
			}
		}

		public void tsmSupplier_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = MessageKeys.Supplier;
			account.accountGroupName = "SUPPLIERS/SUNDRY CREDITORS";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Supplier");
			account.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(account, 25, 25, "Supplier", previlageMethod.Enable);
			}
			else
			{
				OpenForm(account, 25, 25, "Supplier");
			}
		}

		private void tsmExpense_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = MessageKeys.Expense_Account;
			account.accountGroupName = "INDIRECT EXPENSES";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Expense");
			account.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(account, 25, 25, "Expense", previlageMethod.Enable);
			}
			else
			{
				OpenForm(account, 25, 25, "Expense");
			}
		}

		private void tsmBank_Click(object sender, EventArgs e)
		{
			account = new Account();
			account.sFormName = MessageKeys.Bank;
			account.accountGroupName = "CASH AT BANK";
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Bank");
			account.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(account, 25, 25, "Bank", previlageMethod.Enable);
			}
			else
			{
				OpenForm(account, 25, 25, "Bank");
			}
		}

		public void tsmPGroup_Click(object sender, EventArgs e)
		{
			PGroup = new ProductGroup();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(PGroup.Name);
			PGroup.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(PGroup, 25, 25, "Product Group", previlageMethod.Enable);
			}
			else
			{
				OpenForm(PGroup, 25, 25, "Product Group");
			}
		}

		public void tsmProduct_Click(object sender, EventArgs e)
		{
			product = new Product();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(product.Name);
			product.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(product, 25, 25, "Product", previlageMethod.Enable);
			}
			else
			{
				OpenForm(product, 25, 25, "Product");
			}
		}

		private void tsmBrand_Click(object sender, EventArgs e)
		{
			brand = new Brand();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(brand.Name);
			brand.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(brand, 25, 25, "Brand", previlageMethod.Enable);
			}
			else
			{
				OpenForm(brand, 25, 25, "Brand");
			}
		}

		private void tsmUnit_Click(object sender, EventArgs e)
		{
			unit = new Unit();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(unit.Name);
			unit.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(unit, 25, 25, "Unit", previlageMethod.Enable);
			}
			else
			{
				OpenForm(unit, 25, 25, "Unit");
			}
		}

		public void tsmGodown_Click(object sender, EventArgs e)
		{
			godown = new Godown();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(godown.Name);
			godown.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(godown, 25, 25, "Depot", previlageMethod.Enable);
			}
			else
			{
				OpenForm(godown, 25, 25, "Depot");
			}
		}

		private void tsmShelf_Click(object sender, EventArgs e)
		{
			self = new Shelfs();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(self.Name);
			self.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(self, 25, 25, "Shelf", previlageMethod.Enable);
			}
			else
			{
				OpenForm(self, 25, 25, "Shelf");
			}
		}

		private void tsmPCategory_Click(object sender, EventArgs e)
		{
			priceCetegory = new PriceCategory();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(priceCetegory.Name);
			priceCetegory.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(priceCetegory, 25, 25, "Price Category", previlageMethod.Enable);
			}
			else
			{
				OpenForm(priceCetegory, 25, 25, "Price Category");
			}
		}

		public void tsmTCategory_Click(object sender, EventArgs e)
		{
			tCategory = new TaxCategory();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(tCategory.Name);
			tCategory.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(tCategory, 25, 25, "Tax Category", previlageMethod.Enable);
			}
			else
			{
				OpenForm(tCategory, 25, 25, "Tax Category");
			}
		}

		private void tsmRoute_Click(object sender, EventArgs e)
		{
			route = new Route();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(route.Name);
			route.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(route, 25, 25, "Route", previlageMethod.Enable);
			}
			else
			{
				OpenForm(route, 25, 25, "Route");
			}
		}

		private void tsmVehicle_Click(object sender, EventArgs e)
		{
			vehicle = new VehicleView();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(vehicle.Name);
			vehicle.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(vehicle, 25, 25, "Vehicle", previlageMethod.Enable);
			}
			else
			{
				OpenForm(vehicle, 25, 25, "Vehicle");
			}
		}

		private void tsmSCounter_Click(object sender, EventArgs e)
		{
			slsCounter = new SalesCounter();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(slsCounter.Name);
			slsCounter.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(slsCounter, 25, 25, "Sales Counter", previlageMethod.Enable);
			}
			else
			{
				OpenForm(slsCounter, 25, 25, "Sales Counter");
			}
		}

		private void tsmCCenters_Click(object sender, EventArgs e)
		{
			costCentres = new Cost_Centres();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(costCentres.Name);
			costCentres.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(costCentres, 25, 25, "Cost Center", previlageMethod.Enable);
			}
			else
			{
				OpenForm(costCentres, 25, 25, "Cost Center");
			}
		}

		private void tsmCPCard_Click(object sender, EventArgs e)
		{
			cpCard = new CustomerPrevilegeCard();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(cpCard.Name);
			cpCard.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(cpCard, 25, 25, "Previlage Card", previlageMethod.Enable);
			}
			else
			{
				OpenForm(cpCard, 25, 25, "Previlage Card");
			}
		}

		private void tsmCardMaster_Click(object sender, EventArgs e)
		{
			dcCardMaster = new DebitCreditCardMaster();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(dcCardMaster.Name);
			dcCardMaster.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(dcCardMaster, 25, 25, "Card Master", previlageMethod.Enable);
			}
			else
			{
				OpenForm(dcCardMaster, 25, 25, "Card Master");
			}
		}

		public void tsmBranch_Click(object sender, EventArgs e)
		{
			BranchView branchView = new BranchView();
			UserPrevilegeDtl userPrevilegeDtl = (branchView.plgDtls = AniHelper.GetPrevilageMethod(branchView.Name));
			if (userPrevilegeDtl != null)
			{
				OpenForm(branchView, 25, 25, "Branch", userPrevilegeDtl.Enable);
			}
			else
			{
				OpenForm(branchView, 25, 25, "Branch");
			}
		}

		private void tsmBatch_Click(object sender, EventArgs e)
		{
			BatchView batchView = new BatchView();
			UserPrevilegeDtl userPrevilegeDtl = (batchView.plgDtls = AniHelper.GetPrevilageMethod(batchView.Name));
			if (userPrevilegeDtl != null)
			{
				OpenForm(batchView, 25, 25, "Batch", userPrevilegeDtl.Enable);
			}
			else
			{
				OpenForm(batchView, 25, 25, "Batch");
			}
		}

		private void tsmCurrency_Click(object sender, EventArgs e)
		{
			CurrencyView currencyView = new CurrencyView();
			UserPrevilegeDtl userPrevilegeDtl = (currencyView.plgDtls = AniHelper.GetPrevilageMethod(currencyView.Name));
			if (userPrevilegeDtl != null)
			{
				OpenForm(currencyView, 25, 25, "Currency", userPrevilegeDtl.Enable);
			}
			else
			{
				OpenForm(currencyView, 25, 25, "Currency");
			}
		}

		public void tsmKOTPrinter_Click(object sender, EventArgs e)
		{
			KOTPrinterView kOTPrinterView = new KOTPrinterView();
			UserPrevilegeDtl userPrevilegeDtl = (kOTPrinterView.plgDtls = AniHelper.GetPrevilageMethod(kOTPrinterView.Name));
			if (userPrevilegeDtl != null)
			{
				OpenForm(kOTPrinterView, 25, 25, "KOT", userPrevilegeDtl.Enable);
			}
			else
			{
				OpenForm(kOTPrinterView, 25, 25, "KOT");
			}
		}

		private void tsmNote_Click(object sender, EventArgs e)
		{
			NoteMasterView noteMasterView = new NoteMasterView();
			UserPrevilegeDtl userPrevilegeDtl = (noteMasterView.plgDtls = AniHelper.GetPrevilageMethod(noteMasterView.Name));
			if (userPrevilegeDtl != null)
			{
				OpenForm(noteMasterView, 25, 25, "Item Note", userPrevilegeDtl.Enable);
			}
			else
			{
				OpenForm(noteMasterView, 25, 25, "Item Note");
			}
		}

		private void tsmAdminSettings_Click(object sender, EventArgs e)
		{
			admin = new Admin();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(admin.Name);
			if (previlageMethod != null)
			{
				OpenForm(admin, 200, 100, "Admin", previlageMethod.Enable);
			}
			else
			{
				OpenForm(admin, 200, 100, "Admin");
			}
		}

		private void tsmCompany_Click(object sender, EventArgs e)
		{
			CDetails = new CompanyDetails();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(CDetails.Name);
			CDetails.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(CDetails, 100, 80, "Company", previlageMethod.Enable);
			}
			else
			{
				OpenForm(CDetails, 100, 80, "Company");
			}
		}

		private void tsmFYear_Click(object sender, EventArgs e)
		{
			fPeriod = new FinancialPeriod();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(fPeriod.Name);
			fPeriod.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(fPeriod, 180, 100, "Financial Year", previlageMethod.Enable);
			}
			else
			{
				OpenForm(fPeriod, 180, 100, "Financial Year");
			}
		}

		private void tsmFYSelection_Click(object sender, EventArgs e)
		{
			SelectedFY = new SelectedFyear();
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(SelectedFY.Name);
			SelectedFY.plgDtls = previlageMethod;
			if (previlageMethod != null)
			{
				OpenForm(SelectedFY, 180, 80, "Selected Financial Year", previlageMethod.Enable);
			}
			else
			{
				OpenForm(SelectedFY, 180, 80, "Selected Financial Year");
			}
		}

		private void btnCloseCash_Click(object sender, EventArgs e)
		{
			CloseCash closeCash = new CloseCash();
			closeCash.DashboardPOS = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(closeCash.Name);
			if (previlageMethod != null)
			{
				OpenForm(closeCash, 50, 50, "Close Cash", previlageMethod.Enable);
			}
			else
			{
				OpenForm(closeCash, 50, 50, "Close Cash");
			}
		}

		private void btnStock_Click(object sender, EventArgs e)
		{
			if (iMod == 3)
			{
				RestaurantForm restaurantForm = new RestaurantForm();
				restaurantForm.pOS = this;
				UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(restaurantForm.Name);
				if (previlageMethod != null)
				{
					OpenForm(restaurantForm, 50, 50, "Restaurant", previlageMethod.Enable);
				}
				else
				{
					OpenForm(restaurantForm, 50, 50, "Restaurant");
				}
			}
			else
			{
				Stock stock = new Stock();
				stock.pOS = this;
				UserPrevilegeDtl previlageMethod2 = AniHelper.GetPrevilageMethod(stock.Name);
				if (previlageMethod2 != null)
				{
					OpenForm(stock, 50, 50, "Stock", previlageMethod2.Enable);
				}
				else
				{
					OpenForm(stock, 50, 50, "Stock");
				}
			}
		}

		private void btnSalesReport_Click(object sender, EventArgs e)
		{
			Sales sales = new Sales();
			sales.dashboard = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "POSSales");
			if (previlageMethod != null)
			{
				OpenForm(sales, 50, 50, "Sales", previlageMethod.Enable);
			}
			else
			{
				OpenForm(sales, 50, 50, "Sales");
			}
		}

		private void btnInventory_Click(object sender, EventArgs e)
		{
			InventoryReport inventoryReport = new InventoryReport();
			inventoryReport.dashboard = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "InventoryReport");
			if (previlageMethod != null)
			{
				OpenForm(inventoryReport, 50, 50, "Inventory", previlageMethod.Enable);
			}
			else
			{
				OpenForm(inventoryReport, 50, 50, "Inventory");
			}
		}

		private void btnSupplierReport_Click(object sender, EventArgs e)
		{
			Supplier supplier = new Supplier();
			supplier.dashboard = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "POSSupplier");
			if (previlageMethod != null)
			{
				OpenForm(supplier, 50, 50, "Supplier", previlageMethod.Enable);
			}
			else
			{
				OpenForm(supplier, 50, 50, "Supplier");
			}
		}

		private void btnCustomerReport_Click(object sender, EventArgs e)
		{
			Customers customers = new Customers();
			customers.dashboard = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(null, "Customers");
			if (previlageMethod != null)
			{
				OpenForm(customers, 50, 50, "Customers", previlageMethod.Enable);
			}
			else
			{
				OpenForm(customers, 50, 50, "Customers");
			}
		}

		private void btnDashBoard_Click(object sender, EventArgs e)
		{
			btnPanelShow.Text = ">";
			pnlNavigation.Visible = false;
			tabMdi.TabIndex = 0;
		}

		private void btnStaff_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnStaff.ContextMenuStrip = cmsStaff;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnStaff.ContextMenuStrip.Width);
			}
			btnStaff.ContextMenuStrip.Show(new Point(xLocation, btnStaff.Location.Y));
		}

		private void btnOptions_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnOptions.ContextMenuStrip = cmsOption;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnOptions.ContextMenuStrip.Width);
			}
			btnOptions.ContextMenuStrip.Show(new Point(xLocation, btnOptions.Location.Y));
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnAbout.ContextMenuStrip = cmsAbout;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnAbout.ContextMenuStrip.Width);
			}
			btnAbout.ContextMenuStrip.Show(new Point(xLocation, btnAbout.Location.Y));
		}

		private void btnReports_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnReports.ContextMenuStrip = cmsReport;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnReports.ContextMenuStrip.Width);
			}
			btnReports.ContextMenuStrip.Show(new Point(xLocation, btnReports.Location.Y));
		}

		private void btnTransaction_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnTransaction.ContextMenuStrip = cmsMainTrans;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnTransaction.ContextMenuStrip.Width);
			}
			btnTransaction.ContextMenuStrip.Show(new Point(xLocation, btnTransaction.Location.Y));
		}

		private void btnMasters_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnMasters.ContextMenuStrip = cmsMasters;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnMasters.ContextMenuStrip.Width);
			}
			btnMasters.ContextMenuStrip.Show(new Point(xLocation, btnMasters.Location.Y));
		}

		private void btnFirm_Click(object sender, EventArgs e)
		{
			NavPanelMaxMethod();
			btnFirm.ContextMenuStrip = cmsFirm;
			if (Settings.Default.Language == "ar")
			{
				xLocation = resolution.Width - (170 + btnFirm.ContextMenuStrip.Width);
			}
			btnFirm.ContextMenuStrip.Show(new Point(xLocation, btnFirm.Location.Y));
		}

		private void btnSwithOff_Click(object sender, EventArgs e)
		{
			if (!(Settings.Default.MainForm == "Dashboard") && !(Settings.Default.MainForm == "MDI"))
			{
				POSLogin pOSLogin = new POSLogin(_isFromPrograms: false);
				pOSLogin.Show();
			}
			else
			{
				Login login = new Login();
				login.Show();
			}
			Hide();
		}

		private void btnMinimize_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		private void btnPanelShow_Click(object sender, EventArgs e)
		{
			if (pnlNavigation.Visible)
			{
				btnPanelShow.Text = arwMenuHide;
				pnlNavigation.Visible = false;
				return;
			}
			btnPanelShow.Text = arwMenuShow;
			pnlNavigation.Visible = true;
			btnShowReportPanel.Text = arwReportHide;
			pnlReport.Visible = false;
		}

		private void btnShowReportPanel_Click(object sender, EventArgs e)
		{
			if (pnlReport.Visible)
			{
				btnShowReportPanel.Text = arwReportHide;
				pnlReport.Visible = false;
				return;
			}
			btnShowReportPanel.Text = arwReportShow;
			pnlReport.Visible = true;
			btnPanelShow.Text = arwMenuHide;
			pnlNavigation.Visible = false;
		}

		private void btnPOSReports_Click(object sender, EventArgs e)
		{
			POSReports pOSReports = new POSReports();
			pOSReports.dashboard = this;
			UserPrevilegeDtl previlageMethod = AniHelper.GetPrevilageMethod(pOSReports.Name);
			if (previlageMethod != null)
			{
				OpenForm(frm, 50, 50, null, previlageMethod.Enable);
			}
			else
			{
				OpenForm(pOSReports);
			}
		}

		private void DashboardPOS_Activated(object sender, EventArgs e)
		{
			ColorChangeMethod();
		}

		private void DashboardPOS_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Z && e.Alt && e.Control)
			{
				tsmChangeTheme_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt && e.Control)
			{
				string text = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
				if (text == AniHelper.PWordGeneration())
				{
					SettingsView settingsView = new SettingsView();
					settingsView.ShowDialog();
				}
			}
			else if (e.KeyCode == Keys.L && e.Alt && !e.Control)
			{
			}
		}

		private void DashboardPOS_Load(object sender, EventArgs e)
		{
			try
			{
				lblManufacturer.Text = Utility.uManufacturer;
				base.WindowState = FormWindowState.Maximized;
				lblCompany.Text = Settings.Default.strSQLCon;
				lblsystem.Text = Environment.MachineName;
				tmrDateTime.Start();
				Settings.Default.appDate = DateTime.Now.Date;
				Settings.Default.Save();
				tsmTestView.Visible = false;
				Utility.uPrevilageId = db.Users(new
				{
					UserId = Settings.Default.intLogOnId
				}).FirstOrDefault().PrevilageId.ToInt32();
				FinancialPeriodMethod();
				tmrDateTime.Start();
				tsmTCategory.Visible = CommonSettings.blnTaxEnabled;
				tsmSalesEstimate.Visible = CommonSettings.blnSalesEstimateEnable;
				tsmRptSalesEstimate.Visible = CommonSettings.blnSalesEstimateEnable;
				tsmFirstPurchase.Visible = CommonSettings.blnShowFtPurchase;
				tsmBatch.Visible = CommonSettings.blnEnableBatch;
				tsmBranch.Visible = CommonSettings.blnEnableMultiBranch;
				tsmRemoteSync.Visible = CommonSettings.blnEnableOnlineReport;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void tsmUpdate_Click(object sender, EventArgs e)
		{
			await ActivationHelper.UpdationTask();
		}

		private void btnKeyboard_Click(object sender, EventArgs e)
		{
			AniHelper.OSKMethod();
		}

		private void tmrDateTime_Tick(object sender, EventArgs e)
		{
			CultureInfo cultureInfo = new CultureInfo("en-US");
			lblDateTime.Text = DateTime.Now.ToString(cultureInfo);
		}

		private void btnClose_Click(object sender, EventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardPOS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.btnSwithOff = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblsystem = new System.Windows.Forms.Label();
            this.pnlNavigation = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDashBoard = new System.Windows.Forms.Button();
            this.btnCloseCash = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnSalesReport = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnCustomerReport = new System.Windows.Forms.Button();
            this.btnSupplierReport = new System.Windows.Forms.Button();
            this.btnConfiguration = new System.Windows.Forms.Button();
            this.btnFirm = new System.Windows.Forms.Button();
            this.btnMasters = new System.Windows.Forms.Button();
            this.btnTransaction = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnPanelShow = new System.Windows.Forms.Button();
            this.tabMdi = new System.Windows.Forms.TabControl();
            this.tabPOS = new System.Windows.Forms.TabPage();
            this.cmsMasters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExpense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBank = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBrand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGodown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShelf = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVehicle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSCounter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCCenters = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCPCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCardMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBranch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCurrency = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmKOTPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNote = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsMainTrans = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.salesInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalesEstimate = new System.Windows.Forms.ToolStripMenuItem();
            this.salesReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesQuotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPurReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPurOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFirstPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPacking = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStockTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDamageEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStockAdjuster = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProductEditing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProductCombination = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBCodeDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBCodePrinting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmManufacturing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProductUnitCombination = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCashPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCashReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBankPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBankreceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmJournalEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpeningBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDebitNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPDCReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPDCPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.bankReconciliationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBulkReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReceiptAndPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAdvanceReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGrpWiseSlsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPCategoryWiseSlsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRtwiseSlsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSMWiseSlsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPartyWiseSlsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGrpWisePurRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSplrWsPurRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExiCollRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPendingOrdrRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSlsSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSlsRtnSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPurSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPurRtnSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSales = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSalesEstimate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSalesReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSalesOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSalesQuotation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptPurchaseReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptPurchaseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptSalesAndSalesReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptPurchaseAndPurchaseReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptTaxSales = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptTaxPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRptStock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemWiseSalesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemWisePurchaseReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProductLedger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStockTransferReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsInvSlsRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvtSlsReturnReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInventoryPurRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInventoryPurRtnRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProFlowReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUnsoldProdReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGodownStockRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPriceList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAccntBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCashBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBankBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDayBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaymentRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReceiptRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmJournalRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPartyList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOutstandingReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTrialBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProfitAndLossAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBalanceSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsStaff = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRoleAndPrevilage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreateUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStaffRegistration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalaryProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAtte = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChangeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPrintDesigner = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDatabaseBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDatabaseRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDatasetRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDataTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDayClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCalender = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSendMail = new System.Windows.Forms.ToolStripMenuItem();
            this.otherOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCalculator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTestView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVideoTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.qRCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRateFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChangeTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLabelScale = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProductPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFavorite = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRemoteSync = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAbout = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmContactUs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReportBuggs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmKeyTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsFirm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAdminSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFYear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFYSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.pnlReport = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalesInvoice = new System.Windows.Forms.Button();
            this.btnPOSReports = new System.Windows.Forms.Button();
            this.btnSalesSummary = new System.Windows.Forms.Button();
            this.btnPaymentAndReceipt = new System.Windows.Forms.Button();
            this.btnBalanceSheet = new System.Windows.Forms.Button();
            this.btnProfitAndLoseAccount = new System.Windows.Forms.Button();
            this.btnTrialBalance = new System.Windows.Forms.Button();
            this.btnOutstandingReport = new System.Windows.Forms.Button();
            this.btnReceiptReport = new System.Windows.Forms.Button();
            this.btnPaymentReport = new System.Windows.Forms.Button();
            this.btnStockReport = new System.Windows.Forms.Button();
            this.btnLedgerReport = new System.Windows.Forms.Button();
            this.btnPurchaseReport = new System.Windows.Forms.Button();
            this.btnProfitReport = new System.Windows.Forms.Button();
            this.btnSalesReturnReport = new System.Windows.Forms.Button();
            this.btnProductSale = new System.Windows.Forms.Button();
            this.btnCategorySale = new System.Windows.Forms.Button();
            this.btnShowReportPanel = new System.Windows.Forms.Button();
            this.ttDashboardPOS = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTitle.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.tabMdi.SuspendLayout();
            this.cmsMasters.SuspendLayout();
            this.cmsMainTrans.SuspendLayout();
            this.cmsReport.SuspendLayout();
            this.cmsStaff.SuspendLayout();
            this.cmsOption.SuspendLayout();
            this.cmsAbout.SuspendLayout();
            this.cmsFirm.SuspendLayout();
            this.pnlReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            resources.ApplyResources(this.pnlTitle, "pnlTitle");
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.pnlTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTitle.Controls.Add(this.btnKeyboard);
            this.pnlTitle.Controls.Add(this.btnSwithOff);
            this.pnlTitle.Controls.Add(this.btnMinimize);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Controls.Add(this.lblCompany);
            this.pnlTitle.Controls.Add(this.lblManufacturer);
            this.pnlTitle.Controls.Add(this.lblDateTime);
            this.pnlTitle.Controls.Add(this.lblsystem);
            this.pnlTitle.ForeColor = System.Drawing.Color.White;
            this.pnlTitle.Name = "pnlTitle";
            this.ttDashboardPOS.SetToolTip(this.pnlTitle, resources.GetString("pnlTitle.ToolTip"));
            // 
            // btnKeyboard
            // 
            resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
            this.btnKeyboard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnKeyboard.FlatAppearance.BorderSize = 0;
            this.btnKeyboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnKeyboard.ForeColor = System.Drawing.Color.DarkGray;
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.TabStop = false;
            this.ttDashboardPOS.SetToolTip(this.btnKeyboard, resources.GetString("btnKeyboard.ToolTip"));
            this.btnKeyboard.UseVisualStyleBackColor = true;
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // btnSwithOff
            // 
            resources.ApplyResources(this.btnSwithOff, "btnSwithOff");
            this.btnSwithOff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSwithOff.FlatAppearance.BorderSize = 0;
            this.btnSwithOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSwithOff.ForeColor = System.Drawing.Color.DarkGray;
            this.btnSwithOff.Name = "btnSwithOff";
            this.btnSwithOff.TabStop = false;
            this.ttDashboardPOS.SetToolTip(this.btnSwithOff, resources.GetString("btnSwithOff.ToolTip"));
            this.btnSwithOff.UseVisualStyleBackColor = true;
            this.btnSwithOff.Click += new System.EventHandler(this.btnSwithOff_Click);
            // 
            // btnMinimize
            // 
            resources.ApplyResources(this.btnMinimize, "btnMinimize");
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.ForeColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.TabStop = false;
            this.ttDashboardPOS.SetToolTip(this.btnMinimize, resources.GetString("btnMinimize.ToolTip"));
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.ttDashboardPOS.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCompany
            // 
            resources.ApplyResources(this.lblCompany, "lblCompany");
            this.lblCompany.ForeColor = System.Drawing.Color.White;
            this.lblCompany.Name = "lblCompany";
            this.ttDashboardPOS.SetToolTip(this.lblCompany, resources.GetString("lblCompany.ToolTip"));
            // 
            // lblManufacturer
            // 
            resources.ApplyResources(this.lblManufacturer, "lblManufacturer");
            this.lblManufacturer.ForeColor = System.Drawing.Color.White;
            this.lblManufacturer.Name = "lblManufacturer";
            this.ttDashboardPOS.SetToolTip(this.lblManufacturer, resources.GetString("lblManufacturer.ToolTip"));
            // 
            // lblDateTime
            // 
            resources.ApplyResources(this.lblDateTime, "lblDateTime");
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Name = "lblDateTime";
            this.ttDashboardPOS.SetToolTip(this.lblDateTime, resources.GetString("lblDateTime.ToolTip"));
            // 
            // lblsystem
            // 
            resources.ApplyResources(this.lblsystem, "lblsystem");
            this.lblsystem.ForeColor = System.Drawing.Color.White;
            this.lblsystem.Name = "lblsystem";
            this.ttDashboardPOS.SetToolTip(this.lblsystem, resources.GetString("lblsystem.ToolTip"));
            // 
            // pnlNavigation
            // 
            resources.ApplyResources(this.pnlNavigation, "pnlNavigation");
            this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.pnlNavigation.Controls.Add(this.btnDashBoard);
            this.pnlNavigation.Controls.Add(this.btnCloseCash);
            this.pnlNavigation.Controls.Add(this.btnStock);
            this.pnlNavigation.Controls.Add(this.btnSalesReport);
            this.pnlNavigation.Controls.Add(this.btnInventory);
            this.pnlNavigation.Controls.Add(this.btnCustomerReport);
            this.pnlNavigation.Controls.Add(this.btnSupplierReport);
            this.pnlNavigation.Controls.Add(this.btnConfiguration);
            this.pnlNavigation.Controls.Add(this.btnFirm);
            this.pnlNavigation.Controls.Add(this.btnMasters);
            this.pnlNavigation.Controls.Add(this.btnTransaction);
            this.pnlNavigation.Controls.Add(this.btnReports);
            this.pnlNavigation.Controls.Add(this.btnStaff);
            this.pnlNavigation.Controls.Add(this.btnOptions);
            this.pnlNavigation.Controls.Add(this.btnAbout);
            this.pnlNavigation.Name = "pnlNavigation";
            this.ttDashboardPOS.SetToolTip(this.pnlNavigation, resources.GetString("pnlNavigation.ToolTip"));
            // 
            // btnDashBoard
            // 
            resources.ApplyResources(this.btnDashBoard, "btnDashBoard");
            this.btnDashBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnDashBoard.FlatAppearance.BorderSize = 0;
            this.btnDashBoard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnDashBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnDashBoard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDashBoard.Name = "btnDashBoard";
            this.ttDashboardPOS.SetToolTip(this.btnDashBoard, resources.GetString("btnDashBoard.ToolTip"));
            this.btnDashBoard.UseVisualStyleBackColor = false;
            this.btnDashBoard.Click += new System.EventHandler(this.btnDashBoard_Click);
            // 
            // btnCloseCash
            // 
            resources.ApplyResources(this.btnCloseCash, "btnCloseCash");
            this.btnCloseCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnCloseCash.FlatAppearance.BorderSize = 0;
            this.btnCloseCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnCloseCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnCloseCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCloseCash.Name = "btnCloseCash";
            this.ttDashboardPOS.SetToolTip(this.btnCloseCash, resources.GetString("btnCloseCash.ToolTip"));
            this.btnCloseCash.UseVisualStyleBackColor = false;
            this.btnCloseCash.Click += new System.EventHandler(this.btnCloseCash_Click);
            // 
            // btnStock
            // 
            resources.ApplyResources(this.btnStock, "btnStock");
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnStock.FlatAppearance.BorderSize = 0;
            this.btnStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStock.Name = "btnStock";
            this.ttDashboardPOS.SetToolTip(this.btnStock, resources.GetString("btnStock.ToolTip"));
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnSalesReport
            // 
            resources.ApplyResources(this.btnSalesReport, "btnSalesReport");
            this.btnSalesReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnSalesReport.FlatAppearance.BorderSize = 0;
            this.btnSalesReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnSalesReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSalesReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalesReport.Name = "btnSalesReport";
            this.ttDashboardPOS.SetToolTip(this.btnSalesReport, resources.GetString("btnSalesReport.ToolTip"));
            this.btnSalesReport.UseVisualStyleBackColor = false;
            this.btnSalesReport.Click += new System.EventHandler(this.btnSalesReport_Click);
            // 
            // btnInventory
            // 
            resources.ApplyResources(this.btnInventory, "btnInventory");
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnInventory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInventory.Name = "btnInventory";
            this.ttDashboardPOS.SetToolTip(this.btnInventory, resources.GetString("btnInventory.ToolTip"));
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnCustomerReport
            // 
            resources.ApplyResources(this.btnCustomerReport, "btnCustomerReport");
            this.btnCustomerReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnCustomerReport.FlatAppearance.BorderSize = 0;
            this.btnCustomerReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnCustomerReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnCustomerReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCustomerReport.Name = "btnCustomerReport";
            this.ttDashboardPOS.SetToolTip(this.btnCustomerReport, resources.GetString("btnCustomerReport.ToolTip"));
            this.btnCustomerReport.UseVisualStyleBackColor = false;
            this.btnCustomerReport.Click += new System.EventHandler(this.btnCustomerReport_Click);
            // 
            // btnSupplierReport
            // 
            resources.ApplyResources(this.btnSupplierReport, "btnSupplierReport");
            this.btnSupplierReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnSupplierReport.FlatAppearance.BorderSize = 0;
            this.btnSupplierReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnSupplierReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSupplierReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSupplierReport.Name = "btnSupplierReport";
            this.ttDashboardPOS.SetToolTip(this.btnSupplierReport, resources.GetString("btnSupplierReport.ToolTip"));
            this.btnSupplierReport.UseVisualStyleBackColor = false;
            this.btnSupplierReport.Click += new System.EventHandler(this.btnSupplierReport_Click);
            // 
            // btnConfiguration
            // 
            resources.ApplyResources(this.btnConfiguration, "btnConfiguration");
            this.btnConfiguration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnConfiguration.FlatAppearance.BorderSize = 0;
            this.btnConfiguration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnConfiguration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnConfiguration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConfiguration.Name = "btnConfiguration";
            this.ttDashboardPOS.SetToolTip(this.btnConfiguration, resources.GetString("btnConfiguration.ToolTip"));
            this.btnConfiguration.UseVisualStyleBackColor = false;
            this.btnConfiguration.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // btnFirm
            // 
            resources.ApplyResources(this.btnFirm, "btnFirm");
            this.btnFirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnFirm.FlatAppearance.BorderSize = 0;
            this.btnFirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnFirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnFirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFirm.Name = "btnFirm";
            this.ttDashboardPOS.SetToolTip(this.btnFirm, resources.GetString("btnFirm.ToolTip"));
            this.btnFirm.UseVisualStyleBackColor = false;
            this.btnFirm.Click += new System.EventHandler(this.btnFirm_Click);
            // 
            // btnMasters
            // 
            resources.ApplyResources(this.btnMasters, "btnMasters");
            this.btnMasters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnMasters.FlatAppearance.BorderSize = 0;
            this.btnMasters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMasters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnMasters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMasters.Name = "btnMasters";
            this.ttDashboardPOS.SetToolTip(this.btnMasters, resources.GetString("btnMasters.ToolTip"));
            this.btnMasters.UseVisualStyleBackColor = false;
            this.btnMasters.Click += new System.EventHandler(this.btnMasters_Click);
            // 
            // btnTransaction
            // 
            resources.ApplyResources(this.btnTransaction, "btnTransaction");
            this.btnTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnTransaction.FlatAppearance.BorderSize = 0;
            this.btnTransaction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnTransaction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnTransaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTransaction.Name = "btnTransaction";
            this.ttDashboardPOS.SetToolTip(this.btnTransaction, resources.GetString("btnTransaction.ToolTip"));
            this.btnTransaction.UseVisualStyleBackColor = false;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // btnReports
            // 
            resources.ApplyResources(this.btnReports, "btnReports");
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReports.Name = "btnReports";
            this.ttDashboardPOS.SetToolTip(this.btnReports, resources.GetString("btnReports.ToolTip"));
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnStaff
            // 
            resources.ApplyResources(this.btnStaff, "btnStaff");
            this.btnStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnStaff.FlatAppearance.BorderSize = 0;
            this.btnStaff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnStaff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStaff.Name = "btnStaff";
            this.ttDashboardPOS.SetToolTip(this.btnStaff, resources.GetString("btnStaff.ToolTip"));
            this.btnStaff.UseVisualStyleBackColor = false;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnOptions
            // 
            resources.ApplyResources(this.btnOptions, "btnOptions");
            this.btnOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnOptions.FlatAppearance.BorderSize = 0;
            this.btnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOptions.Name = "btnOptions";
            this.ttDashboardPOS.SetToolTip(this.btnOptions, resources.GetString("btnOptions.ToolTip"));
            this.btnOptions.UseVisualStyleBackColor = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnAbout
            // 
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAbout.Name = "btnAbout";
            this.ttDashboardPOS.SetToolTip(this.btnAbout, resources.GetString("btnAbout.ToolTip"));
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnPanelShow
            // 
            resources.ApplyResources(this.btnPanelShow, "btnPanelShow");
            this.btnPanelShow.BackColor = System.Drawing.Color.Crimson;
            this.btnPanelShow.ForeColor = System.Drawing.Color.White;
            this.btnPanelShow.Name = "btnPanelShow";
            this.ttDashboardPOS.SetToolTip(this.btnPanelShow, resources.GetString("btnPanelShow.ToolTip"));
            this.btnPanelShow.UseVisualStyleBackColor = false;
            this.btnPanelShow.Click += new System.EventHandler(this.btnPanelShow_Click);
            // 
            // tabMdi
            // 
            resources.ApplyResources(this.tabMdi, "tabMdi");
            this.tabMdi.Controls.Add(this.tabPOS);
            this.tabMdi.Multiline = true;
            this.tabMdi.Name = "tabMdi";
            this.tabMdi.SelectedIndex = 0;
            this.tabMdi.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ttDashboardPOS.SetToolTip(this.tabMdi, resources.GetString("tabMdi.ToolTip"));
            // 
            // tabPOS
            // 
            resources.ApplyResources(this.tabPOS, "tabPOS");
            this.tabPOS.Name = "tabPOS";
            this.ttDashboardPOS.SetToolTip(this.tabPOS, resources.GetString("tabPOS.ToolTip"));
            this.tabPOS.UseVisualStyleBackColor = true;
            // 
            // cmsMasters
            // 
            resources.ApplyResources(this.cmsMasters, "cmsMasters");
            this.cmsMasters.BackColor = System.Drawing.Color.Gray;
            this.cmsMasters.DropShadowEnabled = false;
            this.cmsMasters.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMasters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAGroup,
            this.tsmAccount,
            this.tsmCustomer,
            this.tsmSupplier,
            this.tsmExpense,
            this.tsmBank,
            this.tsmPGroup,
            this.tsmProduct,
            this.tsmBrand,
            this.tsmUnit,
            this.tsmGodown,
            this.tsmShelf,
            this.tsmPCategory,
            this.tsmTCategory,
            this.tsmRoute,
            this.tsmVehicle,
            this.tsmSCounter,
            this.tsmCCenters,
            this.tsmCPCard,
            this.tsmCardMaster,
            this.tsmBranch,
            this.tsmBatch,
            this.tsmCurrency,
            this.tsmKOTPrinter,
            this.tsmNote});
            this.cmsMasters.Name = "cmsFirm";
            this.ttDashboardPOS.SetToolTip(this.cmsMasters, resources.GetString("cmsMasters.ToolTip"));
            // 
            // tsmAGroup
            // 
            resources.ApplyResources(this.tsmAGroup, "tsmAGroup");
            this.tsmAGroup.BackColor = System.Drawing.Color.White;
            this.tsmAGroup.ForeColor = System.Drawing.Color.Black;
            this.tsmAGroup.Name = "tsmAGroup";
            this.tsmAGroup.Click += new System.EventHandler(this.tsmAGroup_Click);
            // 
            // tsmAccount
            // 
            resources.ApplyResources(this.tsmAccount, "tsmAccount");
            this.tsmAccount.BackColor = System.Drawing.Color.White;
            this.tsmAccount.ForeColor = System.Drawing.Color.Black;
            this.tsmAccount.Name = "tsmAccount";
            this.tsmAccount.Click += new System.EventHandler(this.tsmAccount_Click);
            // 
            // tsmCustomer
            // 
            resources.ApplyResources(this.tsmCustomer, "tsmCustomer");
            this.tsmCustomer.BackColor = System.Drawing.Color.White;
            this.tsmCustomer.ForeColor = System.Drawing.Color.Black;
            this.tsmCustomer.Name = "tsmCustomer";
            this.tsmCustomer.Click += new System.EventHandler(this.tsmCustomer_Click);
            // 
            // tsmSupplier
            // 
            resources.ApplyResources(this.tsmSupplier, "tsmSupplier");
            this.tsmSupplier.BackColor = System.Drawing.Color.White;
            this.tsmSupplier.ForeColor = System.Drawing.Color.Black;
            this.tsmSupplier.Name = "tsmSupplier";
            this.tsmSupplier.Click += new System.EventHandler(this.tsmSupplier_Click);
            // 
            // tsmExpense
            // 
            resources.ApplyResources(this.tsmExpense, "tsmExpense");
            this.tsmExpense.BackColor = System.Drawing.Color.White;
            this.tsmExpense.ForeColor = System.Drawing.Color.Black;
            this.tsmExpense.Name = "tsmExpense";
            this.tsmExpense.Click += new System.EventHandler(this.tsmExpense_Click);
            // 
            // tsmBank
            // 
            resources.ApplyResources(this.tsmBank, "tsmBank");
            this.tsmBank.BackColor = System.Drawing.Color.White;
            this.tsmBank.Name = "tsmBank";
            this.tsmBank.Click += new System.EventHandler(this.tsmBank_Click);
            // 
            // tsmPGroup
            // 
            resources.ApplyResources(this.tsmPGroup, "tsmPGroup");
            this.tsmPGroup.BackColor = System.Drawing.Color.White;
            this.tsmPGroup.Name = "tsmPGroup";
            this.tsmPGroup.Click += new System.EventHandler(this.tsmPGroup_Click);
            // 
            // tsmProduct
            // 
            resources.ApplyResources(this.tsmProduct, "tsmProduct");
            this.tsmProduct.BackColor = System.Drawing.Color.White;
            this.tsmProduct.Name = "tsmProduct";
            this.tsmProduct.Click += new System.EventHandler(this.tsmProduct_Click);
            // 
            // tsmBrand
            // 
            resources.ApplyResources(this.tsmBrand, "tsmBrand");
            this.tsmBrand.BackColor = System.Drawing.Color.White;
            this.tsmBrand.Name = "tsmBrand";
            this.tsmBrand.Click += new System.EventHandler(this.tsmBrand_Click);
            // 
            // tsmUnit
            // 
            resources.ApplyResources(this.tsmUnit, "tsmUnit");
            this.tsmUnit.BackColor = System.Drawing.Color.White;
            this.tsmUnit.Name = "tsmUnit";
            this.tsmUnit.Click += new System.EventHandler(this.tsmUnit_Click);
            // 
            // tsmGodown
            // 
            resources.ApplyResources(this.tsmGodown, "tsmGodown");
            this.tsmGodown.BackColor = System.Drawing.Color.White;
            this.tsmGodown.Name = "tsmGodown";
            this.tsmGodown.Click += new System.EventHandler(this.tsmGodown_Click);
            // 
            // tsmShelf
            // 
            resources.ApplyResources(this.tsmShelf, "tsmShelf");
            this.tsmShelf.BackColor = System.Drawing.Color.White;
            this.tsmShelf.Name = "tsmShelf";
            this.tsmShelf.Click += new System.EventHandler(this.tsmShelf_Click);
            // 
            // tsmPCategory
            // 
            resources.ApplyResources(this.tsmPCategory, "tsmPCategory");
            this.tsmPCategory.BackColor = System.Drawing.Color.White;
            this.tsmPCategory.Name = "tsmPCategory";
            this.tsmPCategory.Click += new System.EventHandler(this.tsmPCategory_Click);
            // 
            // tsmTCategory
            // 
            resources.ApplyResources(this.tsmTCategory, "tsmTCategory");
            this.tsmTCategory.BackColor = System.Drawing.Color.White;
            this.tsmTCategory.Name = "tsmTCategory";
            this.tsmTCategory.Click += new System.EventHandler(this.tsmTCategory_Click);
            // 
            // tsmRoute
            // 
            resources.ApplyResources(this.tsmRoute, "tsmRoute");
            this.tsmRoute.BackColor = System.Drawing.Color.White;
            this.tsmRoute.Name = "tsmRoute";
            this.tsmRoute.Click += new System.EventHandler(this.tsmRoute_Click);
            // 
            // tsmVehicle
            // 
            resources.ApplyResources(this.tsmVehicle, "tsmVehicle");
            this.tsmVehicle.BackColor = System.Drawing.Color.White;
            this.tsmVehicle.Name = "tsmVehicle";
            this.tsmVehicle.Click += new System.EventHandler(this.tsmVehicle_Click);
            // 
            // tsmSCounter
            // 
            resources.ApplyResources(this.tsmSCounter, "tsmSCounter");
            this.tsmSCounter.BackColor = System.Drawing.Color.White;
            this.tsmSCounter.Name = "tsmSCounter";
            this.tsmSCounter.Click += new System.EventHandler(this.tsmSCounter_Click);
            // 
            // tsmCCenters
            // 
            resources.ApplyResources(this.tsmCCenters, "tsmCCenters");
            this.tsmCCenters.BackColor = System.Drawing.Color.White;
            this.tsmCCenters.Name = "tsmCCenters";
            this.tsmCCenters.Click += new System.EventHandler(this.tsmCCenters_Click);
            // 
            // tsmCPCard
            // 
            resources.ApplyResources(this.tsmCPCard, "tsmCPCard");
            this.tsmCPCard.BackColor = System.Drawing.Color.White;
            this.tsmCPCard.Name = "tsmCPCard";
            this.tsmCPCard.Click += new System.EventHandler(this.tsmCPCard_Click);
            // 
            // tsmCardMaster
            // 
            resources.ApplyResources(this.tsmCardMaster, "tsmCardMaster");
            this.tsmCardMaster.BackColor = System.Drawing.Color.White;
            this.tsmCardMaster.Name = "tsmCardMaster";
            this.tsmCardMaster.Click += new System.EventHandler(this.tsmCardMaster_Click);
            // 
            // tsmBranch
            // 
            resources.ApplyResources(this.tsmBranch, "tsmBranch");
            this.tsmBranch.BackColor = System.Drawing.Color.White;
            this.tsmBranch.Name = "tsmBranch";
            this.tsmBranch.Click += new System.EventHandler(this.tsmBranch_Click);
            // 
            // tsmBatch
            // 
            resources.ApplyResources(this.tsmBatch, "tsmBatch");
            this.tsmBatch.BackColor = System.Drawing.Color.White;
            this.tsmBatch.Name = "tsmBatch";
            this.tsmBatch.Click += new System.EventHandler(this.tsmBatch_Click);
            // 
            // tsmCurrency
            // 
            resources.ApplyResources(this.tsmCurrency, "tsmCurrency");
            this.tsmCurrency.BackColor = System.Drawing.Color.White;
            this.tsmCurrency.Name = "tsmCurrency";
            this.tsmCurrency.Click += new System.EventHandler(this.tsmCurrency_Click);
            // 
            // tsmKOTPrinter
            // 
            resources.ApplyResources(this.tsmKOTPrinter, "tsmKOTPrinter");
            this.tsmKOTPrinter.BackColor = System.Drawing.Color.White;
            this.tsmKOTPrinter.Name = "tsmKOTPrinter";
            this.tsmKOTPrinter.Click += new System.EventHandler(this.tsmKOTPrinter_Click);
            // 
            // tsmNote
            // 
            resources.ApplyResources(this.tsmNote, "tsmNote");
            this.tsmNote.BackColor = System.Drawing.Color.White;
            this.tsmNote.Name = "tsmNote";
            this.tsmNote.Click += new System.EventHandler(this.tsmNote_Click);
            // 
            // cmsMainTrans
            // 
            resources.ApplyResources(this.cmsMainTrans, "cmsMainTrans");
            this.cmsMainTrans.BackColor = System.Drawing.Color.Gray;
            this.cmsMainTrans.DropShadowEnabled = false;
            this.cmsMainTrans.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMainTrans.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.cmsMainTrans.Name = "cmsFirm";
            this.ttDashboardPOS.SetToolTip(this.cmsMainTrans, resources.GetString("cmsMainTrans.ToolTip"));
            // 
            // toolStripMenuItem7
            // 
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            this.toolStripMenuItem7.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salesInvoiceToolStripMenuItem,
            this.tsmSalesEstimate,
            this.salesReturnToolStripMenuItem,
            this.salesOrderToolStripMenuItem,
            this.salesQuotationToolStripMenuItem,
            this.salesViewToolStripMenuItem,
            this.billPrintToolStripMenuItem});
            this.toolStripMenuItem7.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            // 
            // salesInvoiceToolStripMenuItem
            // 
            resources.ApplyResources(this.salesInvoiceToolStripMenuItem, "salesInvoiceToolStripMenuItem");
            this.salesInvoiceToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesInvoiceToolStripMenuItem.Name = "salesInvoiceToolStripMenuItem";
            this.salesInvoiceToolStripMenuItem.Click += new System.EventHandler(this.salesInvoiceToolStripMenuItem_Click);
            // 
            // tsmSalesEstimate
            // 
            resources.ApplyResources(this.tsmSalesEstimate, "tsmSalesEstimate");
            this.tsmSalesEstimate.BackColor = System.Drawing.Color.White;
            this.tsmSalesEstimate.Name = "tsmSalesEstimate";
            this.tsmSalesEstimate.Click += new System.EventHandler(this.salesEstimateToolStripMenuItem_Click);
            // 
            // salesReturnToolStripMenuItem
            // 
            resources.ApplyResources(this.salesReturnToolStripMenuItem, "salesReturnToolStripMenuItem");
            this.salesReturnToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesReturnToolStripMenuItem.Name = "salesReturnToolStripMenuItem";
            this.salesReturnToolStripMenuItem.Click += new System.EventHandler(this.salesReturnToolStripMenuItem_Click);
            // 
            // salesOrderToolStripMenuItem
            // 
            resources.ApplyResources(this.salesOrderToolStripMenuItem, "salesOrderToolStripMenuItem");
            this.salesOrderToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesOrderToolStripMenuItem.Name = "salesOrderToolStripMenuItem";
            this.salesOrderToolStripMenuItem.Click += new System.EventHandler(this.salesOrderToolStripMenuItem_Click);
            // 
            // salesQuotationToolStripMenuItem
            // 
            resources.ApplyResources(this.salesQuotationToolStripMenuItem, "salesQuotationToolStripMenuItem");
            this.salesQuotationToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesQuotationToolStripMenuItem.Name = "salesQuotationToolStripMenuItem";
            this.salesQuotationToolStripMenuItem.Click += new System.EventHandler(this.salesQuotationToolStripMenuItem_Click);
            // 
            // salesViewToolStripMenuItem
            // 
            resources.ApplyResources(this.salesViewToolStripMenuItem, "salesViewToolStripMenuItem");
            this.salesViewToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.salesViewToolStripMenuItem.Name = "salesViewToolStripMenuItem";
            this.salesViewToolStripMenuItem.Click += new System.EventHandler(this.salesViewToolStripMenuItem_Click);
            // 
            // billPrintToolStripMenuItem
            // 
            resources.ApplyResources(this.billPrintToolStripMenuItem, "billPrintToolStripMenuItem");
            this.billPrintToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.billPrintToolStripMenuItem.Name = "billPrintToolStripMenuItem";
            this.billPrintToolStripMenuItem.Click += new System.EventHandler(this.billPrintToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            this.toolStripMenuItem8.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPurchase,
            this.tsmPurReturn,
            this.tsmPurOrder,
            this.tsmFirstPurchase});
            this.toolStripMenuItem8.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            // 
            // tsmPurchase
            // 
            resources.ApplyResources(this.tsmPurchase, "tsmPurchase");
            this.tsmPurchase.BackColor = System.Drawing.Color.White;
            this.tsmPurchase.Name = "tsmPurchase";
            this.tsmPurchase.Click += new System.EventHandler(this.tsmPurchase_Click);
            // 
            // tsmPurReturn
            // 
            resources.ApplyResources(this.tsmPurReturn, "tsmPurReturn");
            this.tsmPurReturn.BackColor = System.Drawing.Color.White;
            this.tsmPurReturn.Name = "tsmPurReturn";
            this.tsmPurReturn.Click += new System.EventHandler(this.tsmPurReturn_Click);
            // 
            // tsmPurOrder
            // 
            resources.ApplyResources(this.tsmPurOrder, "tsmPurOrder");
            this.tsmPurOrder.BackColor = System.Drawing.Color.White;
            this.tsmPurOrder.Name = "tsmPurOrder";
            this.tsmPurOrder.Click += new System.EventHandler(this.tsmPurOrder_Click);
            // 
            // tsmFirstPurchase
            // 
            resources.ApplyResources(this.tsmFirstPurchase, "tsmFirstPurchase");
            this.tsmFirstPurchase.BackColor = System.Drawing.Color.White;
            this.tsmFirstPurchase.Name = "tsmFirstPurchase";
            this.tsmFirstPurchase.Click += new System.EventHandler(this.tsmFirstPurchase_Click);
            // 
            // toolStripMenuItem9
            // 
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            this.toolStripMenuItem9.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPacking,
            this.tsmStockTransfer,
            this.tsmDamageEntry,
            this.tsmStockAdjuster,
            this.tsmProductEditing,
            this.tsmProductCombination,
            this.tsmBCodeDesign,
            this.tsmBCodePrinting,
            this.tsmManufacturing,
            this.tsmProductUnitCombination});
            this.toolStripMenuItem9.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            // 
            // tsmPacking
            // 
            resources.ApplyResources(this.tsmPacking, "tsmPacking");
            this.tsmPacking.BackColor = System.Drawing.Color.White;
            this.tsmPacking.Name = "tsmPacking";
            this.tsmPacking.Click += new System.EventHandler(this.tsmPacking_Click);
            // 
            // tsmStockTransfer
            // 
            resources.ApplyResources(this.tsmStockTransfer, "tsmStockTransfer");
            this.tsmStockTransfer.BackColor = System.Drawing.Color.White;
            this.tsmStockTransfer.Name = "tsmStockTransfer";
            this.tsmStockTransfer.Click += new System.EventHandler(this.tsmStockTransfer_Click);
            // 
            // tsmDamageEntry
            // 
            resources.ApplyResources(this.tsmDamageEntry, "tsmDamageEntry");
            this.tsmDamageEntry.BackColor = System.Drawing.Color.White;
            this.tsmDamageEntry.Name = "tsmDamageEntry";
            this.tsmDamageEntry.Click += new System.EventHandler(this.tsmDamageEntry_Click);
            // 
            // tsmStockAdjuster
            // 
            resources.ApplyResources(this.tsmStockAdjuster, "tsmStockAdjuster");
            this.tsmStockAdjuster.BackColor = System.Drawing.Color.White;
            this.tsmStockAdjuster.Name = "tsmStockAdjuster";
            this.tsmStockAdjuster.Click += new System.EventHandler(this.tsmStockAdjuster_Click);
            // 
            // tsmProductEditing
            // 
            resources.ApplyResources(this.tsmProductEditing, "tsmProductEditing");
            this.tsmProductEditing.BackColor = System.Drawing.Color.White;
            this.tsmProductEditing.Name = "tsmProductEditing";
            this.tsmProductEditing.Click += new System.EventHandler(this.tsmProductEditing_Click);
            // 
            // tsmProductCombination
            // 
            resources.ApplyResources(this.tsmProductCombination, "tsmProductCombination");
            this.tsmProductCombination.BackColor = System.Drawing.Color.White;
            this.tsmProductCombination.Name = "tsmProductCombination";
            this.tsmProductCombination.Click += new System.EventHandler(this.tsmProductCombination_Click);
            // 
            // tsmBCodeDesign
            // 
            resources.ApplyResources(this.tsmBCodeDesign, "tsmBCodeDesign");
            this.tsmBCodeDesign.BackColor = System.Drawing.Color.White;
            this.tsmBCodeDesign.Name = "tsmBCodeDesign";
            this.tsmBCodeDesign.Click += new System.EventHandler(this.tsmBCodeDesign_Click);
            // 
            // tsmBCodePrinting
            // 
            resources.ApplyResources(this.tsmBCodePrinting, "tsmBCodePrinting");
            this.tsmBCodePrinting.BackColor = System.Drawing.Color.White;
            this.tsmBCodePrinting.Name = "tsmBCodePrinting";
            this.tsmBCodePrinting.Click += new System.EventHandler(this.tsmBCodePrinting_Click);
            // 
            // tsmManufacturing
            // 
            resources.ApplyResources(this.tsmManufacturing, "tsmManufacturing");
            this.tsmManufacturing.BackColor = System.Drawing.Color.White;
            this.tsmManufacturing.Name = "tsmManufacturing";
            this.tsmManufacturing.Click += new System.EventHandler(this.tsmManufacturing_Click);
            // 
            // tsmProductUnitCombination
            // 
            resources.ApplyResources(this.tsmProductUnitCombination, "tsmProductUnitCombination");
            this.tsmProductUnitCombination.Name = "tsmProductUnitCombination";
            this.tsmProductUnitCombination.Click += new System.EventHandler(this.tsmProductUnitCombination_Click);
            // 
            // toolStripMenuItem10
            // 
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            this.toolStripMenuItem10.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCashPayment,
            this.tsmCashReceipt,
            this.tsmBankPayment,
            this.tsmBankreceipt,
            this.tsmJournalEntry,
            this.tsmOpeningBalance,
            this.tsmDebitNote,
            this.tsmCreditNote,
            this.tsmPDCReceipt,
            this.tsmPDCPayment,
            this.bankReconciliationToolStripMenuItem,
            this.tsmBulkReceipt,
            this.tsmReceiptAndPayment});
            this.toolStripMenuItem10.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            // 
            // tsmCashPayment
            // 
            resources.ApplyResources(this.tsmCashPayment, "tsmCashPayment");
            this.tsmCashPayment.BackColor = System.Drawing.Color.White;
            this.tsmCashPayment.Name = "tsmCashPayment";
            this.tsmCashPayment.Click += new System.EventHandler(this.psmCashPayment_Click);
            // 
            // tsmCashReceipt
            // 
            resources.ApplyResources(this.tsmCashReceipt, "tsmCashReceipt");
            this.tsmCashReceipt.BackColor = System.Drawing.Color.White;
            this.tsmCashReceipt.Name = "tsmCashReceipt";
            this.tsmCashReceipt.Click += new System.EventHandler(this.psmCashReceipt_Click);
            // 
            // tsmBankPayment
            // 
            resources.ApplyResources(this.tsmBankPayment, "tsmBankPayment");
            this.tsmBankPayment.BackColor = System.Drawing.Color.White;
            this.tsmBankPayment.Name = "tsmBankPayment";
            this.tsmBankPayment.Click += new System.EventHandler(this.psmBankPayment_Click);
            // 
            // tsmBankreceipt
            // 
            resources.ApplyResources(this.tsmBankreceipt, "tsmBankreceipt");
            this.tsmBankreceipt.BackColor = System.Drawing.Color.White;
            this.tsmBankreceipt.Name = "tsmBankreceipt";
            this.tsmBankreceipt.Click += new System.EventHandler(this.psmBankreceipt_Click);
            // 
            // tsmJournalEntry
            // 
            resources.ApplyResources(this.tsmJournalEntry, "tsmJournalEntry");
            this.tsmJournalEntry.BackColor = System.Drawing.Color.White;
            this.tsmJournalEntry.Name = "tsmJournalEntry";
            this.tsmJournalEntry.Click += new System.EventHandler(this.psmJournalEntry_Click);
            // 
            // tsmOpeningBalance
            // 
            resources.ApplyResources(this.tsmOpeningBalance, "tsmOpeningBalance");
            this.tsmOpeningBalance.BackColor = System.Drawing.Color.White;
            this.tsmOpeningBalance.Name = "tsmOpeningBalance";
            this.tsmOpeningBalance.Click += new System.EventHandler(this.tsmOpeningBalance_Click);
            // 
            // tsmDebitNote
            // 
            resources.ApplyResources(this.tsmDebitNote, "tsmDebitNote");
            this.tsmDebitNote.BackColor = System.Drawing.Color.White;
            this.tsmDebitNote.Name = "tsmDebitNote";
            this.tsmDebitNote.Click += new System.EventHandler(this.tsmDebitNote_Click);
            // 
            // tsmCreditNote
            // 
            resources.ApplyResources(this.tsmCreditNote, "tsmCreditNote");
            this.tsmCreditNote.BackColor = System.Drawing.Color.White;
            this.tsmCreditNote.Name = "tsmCreditNote";
            this.tsmCreditNote.Click += new System.EventHandler(this.tsmCreditNote_Click);
            // 
            // tsmPDCReceipt
            // 
            resources.ApplyResources(this.tsmPDCReceipt, "tsmPDCReceipt");
            this.tsmPDCReceipt.BackColor = System.Drawing.Color.White;
            this.tsmPDCReceipt.Name = "tsmPDCReceipt";
            this.tsmPDCReceipt.Click += new System.EventHandler(this.tsmPDCReceipt_Click);
            // 
            // tsmPDCPayment
            // 
            resources.ApplyResources(this.tsmPDCPayment, "tsmPDCPayment");
            this.tsmPDCPayment.BackColor = System.Drawing.Color.White;
            this.tsmPDCPayment.Name = "tsmPDCPayment";
            this.tsmPDCPayment.Click += new System.EventHandler(this.tsmPDCPayment_Click);
            // 
            // bankReconciliationToolStripMenuItem
            // 
            resources.ApplyResources(this.bankReconciliationToolStripMenuItem, "bankReconciliationToolStripMenuItem");
            this.bankReconciliationToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.bankReconciliationToolStripMenuItem.Name = "bankReconciliationToolStripMenuItem";
            this.bankReconciliationToolStripMenuItem.Click += new System.EventHandler(this.bankReconciliationToolStripMenuItem_Click);
            // 
            // tsmBulkReceipt
            // 
            resources.ApplyResources(this.tsmBulkReceipt, "tsmBulkReceipt");
            this.tsmBulkReceipt.Name = "tsmBulkReceipt";
            this.tsmBulkReceipt.Click += new System.EventHandler(this.tsmBulkReceipt_Click);
            // 
            // tsmReceiptAndPayment
            // 
            resources.ApplyResources(this.tsmReceiptAndPayment, "tsmReceiptAndPayment");
            this.tsmReceiptAndPayment.Name = "tsmReceiptAndPayment";
            this.tsmReceiptAndPayment.Click += new System.EventHandler(this.tsmReceiptAndPayment_Click_1);
            // 
            // cmsReport
            // 
            resources.ApplyResources(this.cmsReport, "cmsReport");
            this.cmsReport.BackColor = System.Drawing.Color.Gray;
            this.cmsReport.DropShadowEnabled = false;
            this.cmsReport.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14});
            this.cmsReport.Name = "cmsFirm";
            this.ttDashboardPOS.SetToolTip(this.cmsReport, resources.GetString("cmsReport.ToolTip"));
            // 
            // toolStripMenuItem11
            // 
            resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
            this.toolStripMenuItem11.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdvanceReport,
            this.tsmRptSales,
            this.tsmRptSalesEstimate,
            this.tsmRptSalesReturn,
            this.tsmRptSalesOrder,
            this.tsmRptSalesQuotation,
            this.tsmRptPurchase,
            this.tsmRptPurchaseReturn,
            this.tsmRptPurchaseOrder,
            this.tsmRptSalesAndSalesReturn,
            this.tsmRptPurchaseAndPurchaseReturn,
            this.tsmRptCollection,
            this.tsmRptTaxSales,
            this.tsmRptTaxPurchase});
            this.toolStripMenuItem11.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            // 
            // tsmAdvanceReport
            // 
            resources.ApplyResources(this.tsmAdvanceReport, "tsmAdvanceReport");
            this.tsmAdvanceReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmGrpWiseSlsReport,
            this.tsmPCategoryWiseSlsReport,
            this.tsmRtwiseSlsReport,
            this.tsmSMWiseSlsReport,
            this.tsmPartyWiseSlsReport,
            this.tsmGrpWisePurRpt,
            this.tsmSplrWsPurRpt,
            this.tsmExiCollRpt,
            this.tsmPendingOrdrRpt,
            this.tsmSlsSummary,
            this.tsmSlsRtnSummary,
            this.tsmPurSummary,
            this.tsmPurRtnSummary});
            this.tsmAdvanceReport.Name = "tsmAdvanceReport";
            // 
            // tsmGrpWiseSlsReport
            // 
            resources.ApplyResources(this.tsmGrpWiseSlsReport, "tsmGrpWiseSlsReport");
            this.tsmGrpWiseSlsReport.Name = "tsmGrpWiseSlsReport";
            this.tsmGrpWiseSlsReport.Click += new System.EventHandler(this.tsmGrpWiseSlsReport_Click);
            // 
            // tsmPCategoryWiseSlsReport
            // 
            resources.ApplyResources(this.tsmPCategoryWiseSlsReport, "tsmPCategoryWiseSlsReport");
            this.tsmPCategoryWiseSlsReport.Name = "tsmPCategoryWiseSlsReport";
            this.tsmPCategoryWiseSlsReport.Click += new System.EventHandler(this.tsmPCategoryWiseSlsReport_Click);
            // 
            // tsmRtwiseSlsReport
            // 
            resources.ApplyResources(this.tsmRtwiseSlsReport, "tsmRtwiseSlsReport");
            this.tsmRtwiseSlsReport.Name = "tsmRtwiseSlsReport";
            this.tsmRtwiseSlsReport.Click += new System.EventHandler(this.tsmRtwiseSlsReport_Click);
            // 
            // tsmSMWiseSlsReport
            // 
            resources.ApplyResources(this.tsmSMWiseSlsReport, "tsmSMWiseSlsReport");
            this.tsmSMWiseSlsReport.Name = "tsmSMWiseSlsReport";
            this.tsmSMWiseSlsReport.Click += new System.EventHandler(this.tsmSMWiseSlsReport_Click);
            // 
            // tsmPartyWiseSlsReport
            // 
            resources.ApplyResources(this.tsmPartyWiseSlsReport, "tsmPartyWiseSlsReport");
            this.tsmPartyWiseSlsReport.Name = "tsmPartyWiseSlsReport";
            this.tsmPartyWiseSlsReport.Click += new System.EventHandler(this.tsmPartyWiseSlsReport_Click);
            // 
            // tsmGrpWisePurRpt
            // 
            resources.ApplyResources(this.tsmGrpWisePurRpt, "tsmGrpWisePurRpt");
            this.tsmGrpWisePurRpt.ForeColor = System.Drawing.Color.Black;
            this.tsmGrpWisePurRpt.Name = "tsmGrpWisePurRpt";
            this.tsmGrpWisePurRpt.Click += new System.EventHandler(this.tsmGrpWisePurRpt_Click);
            // 
            // tsmSplrWsPurRpt
            // 
            resources.ApplyResources(this.tsmSplrWsPurRpt, "tsmSplrWsPurRpt");
            this.tsmSplrWsPurRpt.Name = "tsmSplrWsPurRpt";
            this.tsmSplrWsPurRpt.Click += new System.EventHandler(this.tsmSplrWsPurRpt_Click);
            // 
            // tsmExiCollRpt
            // 
            resources.ApplyResources(this.tsmExiCollRpt, "tsmExiCollRpt");
            this.tsmExiCollRpt.Name = "tsmExiCollRpt";
            this.tsmExiCollRpt.Click += new System.EventHandler(this.tsmExiCollRpt_Click);
            // 
            // tsmPendingOrdrRpt
            // 
            resources.ApplyResources(this.tsmPendingOrdrRpt, "tsmPendingOrdrRpt");
            this.tsmPendingOrdrRpt.Name = "tsmPendingOrdrRpt";
            this.tsmPendingOrdrRpt.Click += new System.EventHandler(this.tsmPendingOrdrRpt_Click);
            // 
            // tsmSlsSummary
            // 
            resources.ApplyResources(this.tsmSlsSummary, "tsmSlsSummary");
            this.tsmSlsSummary.Name = "tsmSlsSummary";
            this.tsmSlsSummary.Click += new System.EventHandler(this.tsmSlsSummary_Click);
            // 
            // tsmSlsRtnSummary
            // 
            resources.ApplyResources(this.tsmSlsRtnSummary, "tsmSlsRtnSummary");
            this.tsmSlsRtnSummary.Name = "tsmSlsRtnSummary";
            this.tsmSlsRtnSummary.Click += new System.EventHandler(this.tsmSlsRtnSummary_Click);
            // 
            // tsmPurSummary
            // 
            resources.ApplyResources(this.tsmPurSummary, "tsmPurSummary");
            this.tsmPurSummary.Name = "tsmPurSummary";
            this.tsmPurSummary.Click += new System.EventHandler(this.tsmPurSummary_Click);
            // 
            // tsmPurRtnSummary
            // 
            resources.ApplyResources(this.tsmPurRtnSummary, "tsmPurRtnSummary");
            this.tsmPurRtnSummary.Name = "tsmPurRtnSummary";
            this.tsmPurRtnSummary.Click += new System.EventHandler(this.tsmPurRtnSummary_Click);
            // 
            // tsmRptSales
            // 
            resources.ApplyResources(this.tsmRptSales, "tsmRptSales");
            this.tsmRptSales.Name = "tsmRptSales";
            this.tsmRptSales.Click += new System.EventHandler(this.tsmSalesReport_Click);
            // 
            // tsmRptSalesEstimate
            // 
            resources.ApplyResources(this.tsmRptSalesEstimate, "tsmRptSalesEstimate");
            this.tsmRptSalesEstimate.Name = "tsmRptSalesEstimate";
            this.tsmRptSalesEstimate.Click += new System.EventHandler(this.tsmRptSalesEstimate_Click);
            // 
            // tsmRptSalesReturn
            // 
            resources.ApplyResources(this.tsmRptSalesReturn, "tsmRptSalesReturn");
            this.tsmRptSalesReturn.Name = "tsmRptSalesReturn";
            this.tsmRptSalesReturn.Click += new System.EventHandler(this.tsmRptSalesReturn_Click);
            // 
            // tsmRptSalesOrder
            // 
            resources.ApplyResources(this.tsmRptSalesOrder, "tsmRptSalesOrder");
            this.tsmRptSalesOrder.Name = "tsmRptSalesOrder";
            this.tsmRptSalesOrder.Click += new System.EventHandler(this.tsmRptSalesOrder_Click);
            // 
            // tsmRptSalesQuotation
            // 
            resources.ApplyResources(this.tsmRptSalesQuotation, "tsmRptSalesQuotation");
            this.tsmRptSalesQuotation.Name = "tsmRptSalesQuotation";
            this.tsmRptSalesQuotation.Click += new System.EventHandler(this.tsmRptSalesQuotation_Click);
            // 
            // tsmRptPurchase
            // 
            resources.ApplyResources(this.tsmRptPurchase, "tsmRptPurchase");
            this.tsmRptPurchase.Name = "tsmRptPurchase";
            this.tsmRptPurchase.Click += new System.EventHandler(this.tsmRptPurchase_Click);
            // 
            // tsmRptPurchaseReturn
            // 
            resources.ApplyResources(this.tsmRptPurchaseReturn, "tsmRptPurchaseReturn");
            this.tsmRptPurchaseReturn.Name = "tsmRptPurchaseReturn";
            this.tsmRptPurchaseReturn.Click += new System.EventHandler(this.tsmRptPurchaseReturn_Click);
            // 
            // tsmRptPurchaseOrder
            // 
            resources.ApplyResources(this.tsmRptPurchaseOrder, "tsmRptPurchaseOrder");
            this.tsmRptPurchaseOrder.Name = "tsmRptPurchaseOrder";
            this.tsmRptPurchaseOrder.Click += new System.EventHandler(this.tsmRptPurchaseOrder_Click);
            // 
            // tsmRptSalesAndSalesReturn
            // 
            resources.ApplyResources(this.tsmRptSalesAndSalesReturn, "tsmRptSalesAndSalesReturn");
            this.tsmRptSalesAndSalesReturn.Name = "tsmRptSalesAndSalesReturn";
            this.tsmRptSalesAndSalesReturn.Click += new System.EventHandler(this.tsmRptSalesAndSalesReturn_Click);
            // 
            // tsmRptPurchaseAndPurchaseReturn
            // 
            resources.ApplyResources(this.tsmRptPurchaseAndPurchaseReturn, "tsmRptPurchaseAndPurchaseReturn");
            this.tsmRptPurchaseAndPurchaseReturn.Name = "tsmRptPurchaseAndPurchaseReturn";
            this.tsmRptPurchaseAndPurchaseReturn.Click += new System.EventHandler(this.tsmRptPurchaseAndPurchaseReturn_Click);
            // 
            // tsmRptCollection
            // 
            resources.ApplyResources(this.tsmRptCollection, "tsmRptCollection");
            this.tsmRptCollection.Name = "tsmRptCollection";
            this.tsmRptCollection.Click += new System.EventHandler(this.tsmRptCollection_Click);
            // 
            // tsmRptTaxSales
            // 
            resources.ApplyResources(this.tsmRptTaxSales, "tsmRptTaxSales");
            this.tsmRptTaxSales.Name = "tsmRptTaxSales";
            this.tsmRptTaxSales.Click += new System.EventHandler(this.tsmRptTaxSales_Click);
            // 
            // tsmRptTaxPurchase
            // 
            resources.ApplyResources(this.tsmRptTaxPurchase, "tsmRptTaxPurchase");
            this.tsmRptTaxPurchase.Name = "tsmRptTaxPurchase";
            this.tsmRptTaxPurchase.Click += new System.EventHandler(this.tsmRptTaxPurchase_Click);
            // 
            // toolStripMenuItem13
            // 
            resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
            this.toolStripMenuItem13.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem13.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRptStock,
            this.tsmItemWiseSalesReport,
            this.tsmItemWisePurchaseReport,
            this.tsmProductLedger,
            this.tsmStockTransferReport,
            this.tmsInvSlsRegister,
            this.tsmInvtSlsReturnReport,
            this.tsmInventoryPurRegister,
            this.tsmInventoryPurRtnRegister,
            this.tsmProFlowReport,
            this.tsmUnsoldProdReport,
            this.tsmGodownStockRpt,
            this.tsmProRegister,
            this.tsmPriceList});
            this.toolStripMenuItem13.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            // 
            // tsmRptStock
            // 
            resources.ApplyResources(this.tsmRptStock, "tsmRptStock");
            this.tsmRptStock.Name = "tsmRptStock";
            this.tsmRptStock.Click += new System.EventHandler(this.tsmRptStock_Click);
            // 
            // tsmItemWiseSalesReport
            // 
            resources.ApplyResources(this.tsmItemWiseSalesReport, "tsmItemWiseSalesReport");
            this.tsmItemWiseSalesReport.Name = "tsmItemWiseSalesReport";
            this.tsmItemWiseSalesReport.Click += new System.EventHandler(this.tsmItemWiseSalesReport_Click);
            // 
            // tsmItemWisePurchaseReport
            // 
            resources.ApplyResources(this.tsmItemWisePurchaseReport, "tsmItemWisePurchaseReport");
            this.tsmItemWisePurchaseReport.Name = "tsmItemWisePurchaseReport";
            this.tsmItemWisePurchaseReport.Click += new System.EventHandler(this.tsmItemWisePurchaseReport_Click);
            // 
            // tsmProductLedger
            // 
            resources.ApplyResources(this.tsmProductLedger, "tsmProductLedger");
            this.tsmProductLedger.Name = "tsmProductLedger";
            this.tsmProductLedger.Click += new System.EventHandler(this.tsmProductLedger_Click);
            // 
            // tsmStockTransferReport
            // 
            resources.ApplyResources(this.tsmStockTransferReport, "tsmStockTransferReport");
            this.tsmStockTransferReport.Name = "tsmStockTransferReport";
            this.tsmStockTransferReport.Click += new System.EventHandler(this.tsmStockTransferReport_Click);
            // 
            // tmsInvSlsRegister
            // 
            resources.ApplyResources(this.tmsInvSlsRegister, "tmsInvSlsRegister");
            this.tmsInvSlsRegister.Name = "tmsInvSlsRegister";
            this.tmsInvSlsRegister.Click += new System.EventHandler(this.cmsInvSlsRegister_Click);
            // 
            // tsmInvtSlsReturnReport
            // 
            resources.ApplyResources(this.tsmInvtSlsReturnReport, "tsmInvtSlsReturnReport");
            this.tsmInvtSlsReturnReport.Name = "tsmInvtSlsReturnReport";
            this.tsmInvtSlsReturnReport.Click += new System.EventHandler(this.tsmInvtSlsReturnReport_Click);
            // 
            // tsmInventoryPurRegister
            // 
            resources.ApplyResources(this.tsmInventoryPurRegister, "tsmInventoryPurRegister");
            this.tsmInventoryPurRegister.Name = "tsmInventoryPurRegister";
            this.tsmInventoryPurRegister.Click += new System.EventHandler(this.tsmInventoryPurRegister_Click);
            // 
            // tsmInventoryPurRtnRegister
            // 
            resources.ApplyResources(this.tsmInventoryPurRtnRegister, "tsmInventoryPurRtnRegister");
            this.tsmInventoryPurRtnRegister.Name = "tsmInventoryPurRtnRegister";
            this.tsmInventoryPurRtnRegister.Click += new System.EventHandler(this.tsmInventoryPurRtnRegister_Click);
            // 
            // tsmProFlowReport
            // 
            resources.ApplyResources(this.tsmProFlowReport, "tsmProFlowReport");
            this.tsmProFlowReport.Name = "tsmProFlowReport";
            this.tsmProFlowReport.Click += new System.EventHandler(this.tsmProFlowReport_Click);
            // 
            // tsmUnsoldProdReport
            // 
            resources.ApplyResources(this.tsmUnsoldProdReport, "tsmUnsoldProdReport");
            this.tsmUnsoldProdReport.Name = "tsmUnsoldProdReport";
            this.tsmUnsoldProdReport.Click += new System.EventHandler(this.tsmUnsoldProdReport_Click);
            // 
            // tsmGodownStockRpt
            // 
            resources.ApplyResources(this.tsmGodownStockRpt, "tsmGodownStockRpt");
            this.tsmGodownStockRpt.Name = "tsmGodownStockRpt";
            this.tsmGodownStockRpt.Click += new System.EventHandler(this.tsmGodownStockRpt_Click);
            // 
            // tsmProRegister
            // 
            resources.ApplyResources(this.tsmProRegister, "tsmProRegister");
            this.tsmProRegister.Name = "tsmProRegister";
            this.tsmProRegister.Click += new System.EventHandler(this.tsmProRegister_Click);
            // 
            // tsmPriceList
            // 
            resources.ApplyResources(this.tsmPriceList, "tsmPriceList");
            this.tsmPriceList.Name = "tsmPriceList";
            this.tsmPriceList.Click += new System.EventHandler(this.tsmPriceList_Click);
            // 
            // toolStripMenuItem14
            // 
            resources.ApplyResources(this.toolStripMenuItem14, "toolStripMenuItem14");
            this.toolStripMenuItem14.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem14.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAccntBook,
            this.tsmCashBook,
            this.tsmBankBook,
            this.tsmDayBook,
            this.tsmPaymentRegister,
            this.tsmReceiptRegister,
            this.tsmJournalRegister,
            this.tsmTransaction,
            this.tsmPartyList,
            this.tsmOutstandingReport,
            this.tsmTrialBalance,
            this.tsmProfitAndLossAccount,
            this.tsmBalanceSheet});
            this.toolStripMenuItem14.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            // 
            // tsmAccntBook
            // 
            resources.ApplyResources(this.tsmAccntBook, "tsmAccntBook");
            this.tsmAccntBook.Name = "tsmAccntBook";
            this.tsmAccntBook.Click += new System.EventHandler(this.tsmAccntBook_Click);
            // 
            // tsmCashBook
            // 
            resources.ApplyResources(this.tsmCashBook, "tsmCashBook");
            this.tsmCashBook.Name = "tsmCashBook";
            this.tsmCashBook.Click += new System.EventHandler(this.tsmCashBook_Click);
            // 
            // tsmBankBook
            // 
            resources.ApplyResources(this.tsmBankBook, "tsmBankBook");
            this.tsmBankBook.Name = "tsmBankBook";
            this.tsmBankBook.Click += new System.EventHandler(this.tsmBankBook_Click);
            // 
            // tsmDayBook
            // 
            resources.ApplyResources(this.tsmDayBook, "tsmDayBook");
            this.tsmDayBook.Name = "tsmDayBook";
            this.tsmDayBook.Click += new System.EventHandler(this.tsmDayBook_Click);
            // 
            // tsmPaymentRegister
            // 
            resources.ApplyResources(this.tsmPaymentRegister, "tsmPaymentRegister");
            this.tsmPaymentRegister.Name = "tsmPaymentRegister";
            this.tsmPaymentRegister.Click += new System.EventHandler(this.tsmPaymentRegister_Click);
            // 
            // tsmReceiptRegister
            // 
            resources.ApplyResources(this.tsmReceiptRegister, "tsmReceiptRegister");
            this.tsmReceiptRegister.Name = "tsmReceiptRegister";
            this.tsmReceiptRegister.Click += new System.EventHandler(this.tsmReceiptRegister_Click);
            // 
            // tsmJournalRegister
            // 
            resources.ApplyResources(this.tsmJournalRegister, "tsmJournalRegister");
            this.tsmJournalRegister.Name = "tsmJournalRegister";
            this.tsmJournalRegister.Click += new System.EventHandler(this.tsmJournalRegister_Click);
            // 
            // tsmTransaction
            // 
            resources.ApplyResources(this.tsmTransaction, "tsmTransaction");
            this.tsmTransaction.Name = "tsmTransaction";
            this.tsmTransaction.Click += new System.EventHandler(this.tsmTransaction_Click);
            // 
            // tsmPartyList
            // 
            resources.ApplyResources(this.tsmPartyList, "tsmPartyList");
            this.tsmPartyList.Name = "tsmPartyList";
            this.tsmPartyList.Click += new System.EventHandler(this.tsmPartyList_Click);
            // 
            // tsmOutstandingReport
            // 
            resources.ApplyResources(this.tsmOutstandingReport, "tsmOutstandingReport");
            this.tsmOutstandingReport.Name = "tsmOutstandingReport";
            this.tsmOutstandingReport.Click += new System.EventHandler(this.tsmOutstandingReport_Click);
            // 
            // tsmTrialBalance
            // 
            resources.ApplyResources(this.tsmTrialBalance, "tsmTrialBalance");
            this.tsmTrialBalance.Name = "tsmTrialBalance";
            this.tsmTrialBalance.Click += new System.EventHandler(this.tsmTrialBalance_Click);
            // 
            // tsmProfitAndLossAccount
            // 
            resources.ApplyResources(this.tsmProfitAndLossAccount, "tsmProfitAndLossAccount");
            this.tsmProfitAndLossAccount.Name = "tsmProfitAndLossAccount";
            this.tsmProfitAndLossAccount.Click += new System.EventHandler(this.tsmProfitAndLossAccount_Click);
            // 
            // tsmBalanceSheet
            // 
            resources.ApplyResources(this.tsmBalanceSheet, "tsmBalanceSheet");
            this.tsmBalanceSheet.Name = "tsmBalanceSheet";
            this.tsmBalanceSheet.Click += new System.EventHandler(this.tsmBalanceSheet_Click);
            // 
            // cmsStaff
            // 
            resources.ApplyResources(this.cmsStaff, "cmsStaff");
            this.cmsStaff.BackColor = System.Drawing.Color.White;
            this.cmsStaff.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsStaff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmChangePassword,
            this.tsmRoleAndPrevilage,
            this.tsmCreateUser,
            this.tsmStaffRegistration,
            this.tsmSalaryProcess,
            this.tsmAtte});
            this.cmsStaff.Name = "cmsStaff";
            this.ttDashboardPOS.SetToolTip(this.cmsStaff, resources.GetString("cmsStaff.ToolTip"));
            // 
            // tsmChangePassword
            // 
            resources.ApplyResources(this.tsmChangePassword, "tsmChangePassword");
            this.tsmChangePassword.Name = "tsmChangePassword";
            this.tsmChangePassword.Click += new System.EventHandler(this.tsmChangePassword_Click);
            // 
            // tsmRoleAndPrevilage
            // 
            resources.ApplyResources(this.tsmRoleAndPrevilage, "tsmRoleAndPrevilage");
            this.tsmRoleAndPrevilage.Name = "tsmRoleAndPrevilage";
            this.tsmRoleAndPrevilage.Click += new System.EventHandler(this.tsmRoleAndPrevilage_Click);
            // 
            // tsmCreateUser
            // 
            resources.ApplyResources(this.tsmCreateUser, "tsmCreateUser");
            this.tsmCreateUser.Name = "tsmCreateUser";
            this.tsmCreateUser.Click += new System.EventHandler(this.tsmCreateUser_Click);
            // 
            // tsmStaffRegistration
            // 
            resources.ApplyResources(this.tsmStaffRegistration, "tsmStaffRegistration");
            this.tsmStaffRegistration.Name = "tsmStaffRegistration";
            this.tsmStaffRegistration.Click += new System.EventHandler(this.tsmStaffRegistration_Click);
            // 
            // tsmSalaryProcess
            // 
            resources.ApplyResources(this.tsmSalaryProcess, "tsmSalaryProcess");
            this.tsmSalaryProcess.Name = "tsmSalaryProcess";
            this.tsmSalaryProcess.Click += new System.EventHandler(this.tsmSalaryProcess_Click);
            // 
            // tsmAtte
            // 
            resources.ApplyResources(this.tsmAtte, "tsmAtte");
            this.tsmAtte.Name = "tsmAtte";
            this.tsmAtte.Click += new System.EventHandler(this.tsmSalaryProcess_Click);
            // 
            // cmsOption
            // 
            resources.ApplyResources(this.cmsOption, "cmsOption");
            this.cmsOption.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmExport,
            this.tsmImport,
            this.tsmChangeDate,
            this.tsmReminder,
            this.tsmSettings,
            this.tsmPrintDesigner,
            this.tsmCustomTransaction,
            this.tsmDatabaseBackup,
            this.tsmDatabaseRestore,
            this.tsmDatasetRestore,
            this.tsmDataTransfer,
            this.tsmDayClose,
            this.tsmCalender,
            this.tsmSendMail,
            this.otherOptionToolStripMenuItem,
            this.tsmVideoTutorial,
            this.qRCodeToolStripMenuItem,
            this.tsmRateFormat,
            this.tsmChangeTheme,
            this.tsmLabelScale,
            this.tsmProductPrinter,
            this.tsmFavorite,
            this.tsmRemoteSync});
            this.cmsOption.Name = "contextMenuStrip1";
            this.ttDashboardPOS.SetToolTip(this.cmsOption, resources.GetString("cmsOption.ToolTip"));
            // 
            // tsmExport
            // 
            resources.ApplyResources(this.tsmExport, "tsmExport");
            this.tsmExport.Name = "tsmExport";
            this.tsmExport.Click += new System.EventHandler(this.tsmExport_Click);
            // 
            // tsmImport
            // 
            resources.ApplyResources(this.tsmImport, "tsmImport");
            this.tsmImport.Name = "tsmImport";
            this.tsmImport.Click += new System.EventHandler(this.tsmImport_Click);
            // 
            // tsmChangeDate
            // 
            resources.ApplyResources(this.tsmChangeDate, "tsmChangeDate");
            this.tsmChangeDate.Name = "tsmChangeDate";
            this.tsmChangeDate.Click += new System.EventHandler(this.tsmChangeDate_Click);
            // 
            // tsmReminder
            // 
            resources.ApplyResources(this.tsmReminder, "tsmReminder");
            this.tsmReminder.Name = "tsmReminder";
            this.tsmReminder.Click += new System.EventHandler(this.tsmReminder_Click);
            // 
            // tsmSettings
            // 
            resources.ApplyResources(this.tsmSettings, "tsmSettings");
            this.tsmSettings.Name = "tsmSettings";
            this.tsmSettings.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // tsmPrintDesigner
            // 
            resources.ApplyResources(this.tsmPrintDesigner, "tsmPrintDesigner");
            this.tsmPrintDesigner.Name = "tsmPrintDesigner";
            this.tsmPrintDesigner.Click += new System.EventHandler(this.tsmPrintDesigner_Click);
            // 
            // tsmCustomTransaction
            // 
            resources.ApplyResources(this.tsmCustomTransaction, "tsmCustomTransaction");
            this.tsmCustomTransaction.Name = "tsmCustomTransaction";
            this.tsmCustomTransaction.Click += new System.EventHandler(this.tsmCustomTransaction_Click);
            // 
            // tsmDatabaseBackup
            // 
            resources.ApplyResources(this.tsmDatabaseBackup, "tsmDatabaseBackup");
            this.tsmDatabaseBackup.Name = "tsmDatabaseBackup";
            this.tsmDatabaseBackup.Click += new System.EventHandler(this.tsmDatabaseBackup_Click);
            // 
            // tsmDatabaseRestore
            // 
            resources.ApplyResources(this.tsmDatabaseRestore, "tsmDatabaseRestore");
            this.tsmDatabaseRestore.Name = "tsmDatabaseRestore";
            this.tsmDatabaseRestore.Click += new System.EventHandler(this.tsmDatabaseRestore_Click);
            // 
            // tsmDatasetRestore
            // 
            resources.ApplyResources(this.tsmDatasetRestore, "tsmDatasetRestore");
            this.tsmDatasetRestore.Name = "tsmDatasetRestore";
            this.tsmDatasetRestore.Click += new System.EventHandler(this.tsmDatasetRestore_Click);
            // 
            // tsmDataTransfer
            // 
            resources.ApplyResources(this.tsmDataTransfer, "tsmDataTransfer");
            this.tsmDataTransfer.Name = "tsmDataTransfer";
            this.tsmDataTransfer.Click += new System.EventHandler(this.tsmDataTransfer_Click);
            // 
            // tsmDayClose
            // 
            resources.ApplyResources(this.tsmDayClose, "tsmDayClose");
            this.tsmDayClose.Name = "tsmDayClose";
            this.tsmDayClose.Click += new System.EventHandler(this.tsmDayClose_Click);
            // 
            // tsmCalender
            // 
            resources.ApplyResources(this.tsmCalender, "tsmCalender");
            this.tsmCalender.Name = "tsmCalender";
            this.tsmCalender.Click += new System.EventHandler(this.tsmCalender_Click);
            // 
            // tsmSendMail
            // 
            resources.ApplyResources(this.tsmSendMail, "tsmSendMail");
            this.tsmSendMail.Name = "tsmSendMail";
            this.tsmSendMail.Click += new System.EventHandler(this.tsmSendMail_Click);
            // 
            // otherOptionToolStripMenuItem
            // 
            resources.ApplyResources(this.otherOptionToolStripMenuItem, "otherOptionToolStripMenuItem");
            this.otherOptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCalculator,
            this.tsmExcel,
            this.tsmNotepad,
            this.tsmPaint,
            this.tsmTestView});
            this.otherOptionToolStripMenuItem.Name = "otherOptionToolStripMenuItem";
            // 
            // tsmCalculator
            // 
            resources.ApplyResources(this.tsmCalculator, "tsmCalculator");
            this.tsmCalculator.Name = "tsmCalculator";
            this.tsmCalculator.Click += new System.EventHandler(this.tsmCalculator_Click);
            // 
            // tsmExcel
            // 
            resources.ApplyResources(this.tsmExcel, "tsmExcel");
            this.tsmExcel.Name = "tsmExcel";
            this.tsmExcel.Click += new System.EventHandler(this.tsmExcel_Click);
            // 
            // tsmNotepad
            // 
            resources.ApplyResources(this.tsmNotepad, "tsmNotepad");
            this.tsmNotepad.Name = "tsmNotepad";
            this.tsmNotepad.Click += new System.EventHandler(this.tsmNotepad_Click);
            // 
            // tsmPaint
            // 
            resources.ApplyResources(this.tsmPaint, "tsmPaint");
            this.tsmPaint.Name = "tsmPaint";
            this.tsmPaint.Click += new System.EventHandler(this.tsmPaint_Click);
            // 
            // tsmTestView
            // 
            resources.ApplyResources(this.tsmTestView, "tsmTestView");
            this.tsmTestView.Name = "tsmTestView";
            this.tsmTestView.Click += new System.EventHandler(this.tsmTestView_Click);
            // 
            // tsmVideoTutorial
            // 
            resources.ApplyResources(this.tsmVideoTutorial, "tsmVideoTutorial");
            this.tsmVideoTutorial.Name = "tsmVideoTutorial";
            this.tsmVideoTutorial.Click += new System.EventHandler(this.tsmVideoTutorial_Click);
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
            // tsmChangeTheme
            // 
            resources.ApplyResources(this.tsmChangeTheme, "tsmChangeTheme");
            this.tsmChangeTheme.Name = "tsmChangeTheme";
            this.tsmChangeTheme.Click += new System.EventHandler(this.tsmChangeTheme_Click);
            // 
            // tsmLabelScale
            // 
            resources.ApplyResources(this.tsmLabelScale, "tsmLabelScale");
            this.tsmLabelScale.Name = "tsmLabelScale";
            this.tsmLabelScale.Click += new System.EventHandler(this.tsmLabelScale_Click);
            // 
            // tsmProductPrinter
            // 
            resources.ApplyResources(this.tsmProductPrinter, "tsmProductPrinter");
            this.tsmProductPrinter.Name = "tsmProductPrinter";
            this.tsmProductPrinter.Click += new System.EventHandler(this.tsmProductPrinter_Click);
            // 
            // tsmFavorite
            // 
            resources.ApplyResources(this.tsmFavorite, "tsmFavorite");
            this.tsmFavorite.Name = "tsmFavorite";
            this.tsmFavorite.Click += new System.EventHandler(this.tsmFavorite_Click);
            // 
            // tsmRemoteSync
            // 
            resources.ApplyResources(this.tsmRemoteSync, "tsmRemoteSync");
            this.tsmRemoteSync.Name = "tsmRemoteSync";
            this.tsmRemoteSync.Click += new System.EventHandler(this.tsmRemoteSync_Click);
            // 
            // cmsAbout
            // 
            resources.ApplyResources(this.cmsAbout, "cmsAbout");
            this.cmsAbout.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsAbout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAboutUs,
            this.tsmContactUs,
            this.tsmReportBuggs,
            this.tsmUpdate,
            this.tsmKeyTransfer});
            this.cmsAbout.Name = "contextMenuStrip1";
            this.ttDashboardPOS.SetToolTip(this.cmsAbout, resources.GetString("cmsAbout.ToolTip"));
            // 
            // tsmAboutUs
            // 
            resources.ApplyResources(this.tsmAboutUs, "tsmAboutUs");
            this.tsmAboutUs.Name = "tsmAboutUs";
            this.tsmAboutUs.Click += new System.EventHandler(this.tsmAboutUs_Click);
            // 
            // tsmContactUs
            // 
            resources.ApplyResources(this.tsmContactUs, "tsmContactUs");
            this.tsmContactUs.Name = "tsmContactUs";
            this.tsmContactUs.Click += new System.EventHandler(this.tsmContactUs_Click);
            // 
            // tsmReportBuggs
            // 
            resources.ApplyResources(this.tsmReportBuggs, "tsmReportBuggs");
            this.tsmReportBuggs.Name = "tsmReportBuggs";
            this.tsmReportBuggs.Click += new System.EventHandler(this.tsmReportBuggs_Click);
            // 
            // tsmUpdate
            // 
            resources.ApplyResources(this.tsmUpdate, "tsmUpdate");
            this.tsmUpdate.Name = "tsmUpdate";
            this.tsmUpdate.Click += new System.EventHandler(this.tsmUpdate_Click);
            // 
            // tsmKeyTransfer
            // 
            resources.ApplyResources(this.tsmKeyTransfer, "tsmKeyTransfer");
            this.tsmKeyTransfer.Name = "tsmKeyTransfer";
            this.tsmKeyTransfer.Click += new System.EventHandler(this.tsmKeyTransfer_Click);
            // 
            // cmsFirm
            // 
            resources.ApplyResources(this.cmsFirm, "cmsFirm");
            this.cmsFirm.BackColor = System.Drawing.Color.Gray;
            this.cmsFirm.DropShadowEnabled = false;
            this.cmsFirm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsFirm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdminSettings,
            this.tsmCompany,
            this.tsmFYear,
            this.tsmFYSelection});
            this.cmsFirm.Name = "cmsFirm";
            this.ttDashboardPOS.SetToolTip(this.cmsFirm, resources.GetString("cmsFirm.ToolTip"));
            // 
            // tsmAdminSettings
            // 
            resources.ApplyResources(this.tsmAdminSettings, "tsmAdminSettings");
            this.tsmAdminSettings.BackColor = System.Drawing.Color.White;
            this.tsmAdminSettings.ForeColor = System.Drawing.Color.Black;
            this.tsmAdminSettings.Name = "tsmAdminSettings";
            this.tsmAdminSettings.Click += new System.EventHandler(this.tsmAdminSettings_Click);
            // 
            // tsmCompany
            // 
            resources.ApplyResources(this.tsmCompany, "tsmCompany");
            this.tsmCompany.BackColor = System.Drawing.Color.White;
            this.tsmCompany.ForeColor = System.Drawing.Color.Black;
            this.tsmCompany.Name = "tsmCompany";
            this.tsmCompany.Click += new System.EventHandler(this.tsmCompany_Click);
            // 
            // tsmFYear
            // 
            resources.ApplyResources(this.tsmFYear, "tsmFYear");
            this.tsmFYear.BackColor = System.Drawing.Color.White;
            this.tsmFYear.ForeColor = System.Drawing.Color.Black;
            this.tsmFYear.Name = "tsmFYear";
            this.tsmFYear.Click += new System.EventHandler(this.tsmFYear_Click);
            // 
            // tsmFYSelection
            // 
            resources.ApplyResources(this.tsmFYSelection, "tsmFYSelection");
            this.tsmFYSelection.BackColor = System.Drawing.Color.White;
            this.tsmFYSelection.ForeColor = System.Drawing.Color.Black;
            this.tsmFYSelection.Name = "tsmFYSelection";
            this.tsmFYSelection.Click += new System.EventHandler(this.tsmFYSelection_Click);
            // 
            // tmrDateTime
            // 
            this.tmrDateTime.Tick += new System.EventHandler(this.tmrDateTime_Tick);
            // 
            // pnlReport
            // 
            resources.ApplyResources(this.pnlReport, "pnlReport");
            this.pnlReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.pnlReport.Controls.Add(this.btnSalesInvoice);
            this.pnlReport.Controls.Add(this.btnPOSReports);
            this.pnlReport.Controls.Add(this.btnSalesSummary);
            this.pnlReport.Controls.Add(this.btnPaymentAndReceipt);
            this.pnlReport.Controls.Add(this.btnBalanceSheet);
            this.pnlReport.Controls.Add(this.btnProfitAndLoseAccount);
            this.pnlReport.Controls.Add(this.btnTrialBalance);
            this.pnlReport.Controls.Add(this.btnOutstandingReport);
            this.pnlReport.Controls.Add(this.btnReceiptReport);
            this.pnlReport.Controls.Add(this.btnPaymentReport);
            this.pnlReport.Controls.Add(this.btnStockReport);
            this.pnlReport.Controls.Add(this.btnLedgerReport);
            this.pnlReport.Controls.Add(this.btnPurchaseReport);
            this.pnlReport.Controls.Add(this.btnProfitReport);
            this.pnlReport.Controls.Add(this.btnSalesReturnReport);
            this.pnlReport.Controls.Add(this.btnProductSale);
            this.pnlReport.Controls.Add(this.btnCategorySale);
            this.pnlReport.Name = "pnlReport";
            this.ttDashboardPOS.SetToolTip(this.pnlReport, resources.GetString("pnlReport.ToolTip"));
            // 
            // btnSalesInvoice
            // 
            resources.ApplyResources(this.btnSalesInvoice, "btnSalesInvoice");
            this.btnSalesInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnSalesInvoice.FlatAppearance.BorderSize = 0;
            this.btnSalesInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnSalesInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSalesInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalesInvoice.Name = "btnSalesInvoice";
            this.ttDashboardPOS.SetToolTip(this.btnSalesInvoice, resources.GetString("btnSalesInvoice.ToolTip"));
            this.btnSalesInvoice.UseVisualStyleBackColor = false;
            this.btnSalesInvoice.Click += new System.EventHandler(this.salesInvoiceToolStripMenuItem_Click);
            // 
            // btnPOSReports
            // 
            resources.ApplyResources(this.btnPOSReports, "btnPOSReports");
            this.btnPOSReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnPOSReports.FlatAppearance.BorderSize = 0;
            this.btnPOSReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnPOSReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPOSReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPOSReports.Name = "btnPOSReports";
            this.ttDashboardPOS.SetToolTip(this.btnPOSReports, resources.GetString("btnPOSReports.ToolTip"));
            this.btnPOSReports.UseVisualStyleBackColor = false;
            this.btnPOSReports.Click += new System.EventHandler(this.btnPOSReports_Click);
            // 
            // btnSalesSummary
            // 
            resources.ApplyResources(this.btnSalesSummary, "btnSalesSummary");
            this.btnSalesSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnSalesSummary.FlatAppearance.BorderSize = 0;
            this.btnSalesSummary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnSalesSummary.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSalesSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalesSummary.Name = "btnSalesSummary";
            this.ttDashboardPOS.SetToolTip(this.btnSalesSummary, resources.GetString("btnSalesSummary.ToolTip"));
            this.btnSalesSummary.UseVisualStyleBackColor = false;
            this.btnSalesSummary.Click += new System.EventHandler(this.tsmSlsSummary_Click);
            // 
            // btnPaymentAndReceipt
            // 
            resources.ApplyResources(this.btnPaymentAndReceipt, "btnPaymentAndReceipt");
            this.btnPaymentAndReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnPaymentAndReceipt.FlatAppearance.BorderSize = 0;
            this.btnPaymentAndReceipt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnPaymentAndReceipt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPaymentAndReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPaymentAndReceipt.Name = "btnPaymentAndReceipt";
            this.ttDashboardPOS.SetToolTip(this.btnPaymentAndReceipt, resources.GetString("btnPaymentAndReceipt.ToolTip"));
            this.btnPaymentAndReceipt.UseVisualStyleBackColor = false;
            this.btnPaymentAndReceipt.Click += new System.EventHandler(this.tsmReceiptAndPayment_Click_1);
            // 
            // btnBalanceSheet
            // 
            resources.ApplyResources(this.btnBalanceSheet, "btnBalanceSheet");
            this.btnBalanceSheet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnBalanceSheet.FlatAppearance.BorderSize = 0;
            this.btnBalanceSheet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnBalanceSheet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnBalanceSheet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBalanceSheet.Name = "btnBalanceSheet";
            this.ttDashboardPOS.SetToolTip(this.btnBalanceSheet, resources.GetString("btnBalanceSheet.ToolTip"));
            this.btnBalanceSheet.UseVisualStyleBackColor = false;
            this.btnBalanceSheet.Click += new System.EventHandler(this.tsmBalanceSheet_Click);
            // 
            // btnProfitAndLoseAccount
            // 
            resources.ApplyResources(this.btnProfitAndLoseAccount, "btnProfitAndLoseAccount");
            this.btnProfitAndLoseAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnProfitAndLoseAccount.FlatAppearance.BorderSize = 0;
            this.btnProfitAndLoseAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnProfitAndLoseAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnProfitAndLoseAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnProfitAndLoseAccount.Name = "btnProfitAndLoseAccount";
            this.ttDashboardPOS.SetToolTip(this.btnProfitAndLoseAccount, resources.GetString("btnProfitAndLoseAccount.ToolTip"));
            this.btnProfitAndLoseAccount.UseVisualStyleBackColor = false;
            this.btnProfitAndLoseAccount.Click += new System.EventHandler(this.tsmProfitAndLossAccount_Click);
            // 
            // btnTrialBalance
            // 
            resources.ApplyResources(this.btnTrialBalance, "btnTrialBalance");
            this.btnTrialBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnTrialBalance.FlatAppearance.BorderSize = 0;
            this.btnTrialBalance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnTrialBalance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnTrialBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTrialBalance.Name = "btnTrialBalance";
            this.ttDashboardPOS.SetToolTip(this.btnTrialBalance, resources.GetString("btnTrialBalance.ToolTip"));
            this.btnTrialBalance.UseVisualStyleBackColor = false;
            this.btnTrialBalance.Click += new System.EventHandler(this.tsmTrialBalance_Click);
            // 
            // btnOutstandingReport
            // 
            resources.ApplyResources(this.btnOutstandingReport, "btnOutstandingReport");
            this.btnOutstandingReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnOutstandingReport.FlatAppearance.BorderSize = 0;
            this.btnOutstandingReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnOutstandingReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnOutstandingReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOutstandingReport.Name = "btnOutstandingReport";
            this.ttDashboardPOS.SetToolTip(this.btnOutstandingReport, resources.GetString("btnOutstandingReport.ToolTip"));
            this.btnOutstandingReport.UseVisualStyleBackColor = false;
            this.btnOutstandingReport.Click += new System.EventHandler(this.tsmOutstandingReport_Click);
            // 
            // btnReceiptReport
            // 
            resources.ApplyResources(this.btnReceiptReport, "btnReceiptReport");
            this.btnReceiptReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnReceiptReport.FlatAppearance.BorderSize = 0;
            this.btnReceiptReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnReceiptReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnReceiptReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReceiptReport.Name = "btnReceiptReport";
            this.ttDashboardPOS.SetToolTip(this.btnReceiptReport, resources.GetString("btnReceiptReport.ToolTip"));
            this.btnReceiptReport.UseVisualStyleBackColor = false;
            this.btnReceiptReport.Click += new System.EventHandler(this.tsmReceiptRegister_Click);
            // 
            // btnPaymentReport
            // 
            resources.ApplyResources(this.btnPaymentReport, "btnPaymentReport");
            this.btnPaymentReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnPaymentReport.FlatAppearance.BorderSize = 0;
            this.btnPaymentReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnPaymentReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPaymentReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPaymentReport.Name = "btnPaymentReport";
            this.ttDashboardPOS.SetToolTip(this.btnPaymentReport, resources.GetString("btnPaymentReport.ToolTip"));
            this.btnPaymentReport.UseVisualStyleBackColor = false;
            this.btnPaymentReport.Click += new System.EventHandler(this.tsmPaymentRegister_Click);
            // 
            // btnStockReport
            // 
            resources.ApplyResources(this.btnStockReport, "btnStockReport");
            this.btnStockReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnStockReport.FlatAppearance.BorderSize = 0;
            this.btnStockReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnStockReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnStockReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStockReport.Name = "btnStockReport";
            this.ttDashboardPOS.SetToolTip(this.btnStockReport, resources.GetString("btnStockReport.ToolTip"));
            this.btnStockReport.UseVisualStyleBackColor = false;
            this.btnStockReport.Click += new System.EventHandler(this.tsmRptStock_Click);
            // 
            // btnLedgerReport
            // 
            resources.ApplyResources(this.btnLedgerReport, "btnLedgerReport");
            this.btnLedgerReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnLedgerReport.FlatAppearance.BorderSize = 0;
            this.btnLedgerReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnLedgerReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnLedgerReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLedgerReport.Name = "btnLedgerReport";
            this.ttDashboardPOS.SetToolTip(this.btnLedgerReport, resources.GetString("btnLedgerReport.ToolTip"));
            this.btnLedgerReport.UseVisualStyleBackColor = false;
            this.btnLedgerReport.Click += new System.EventHandler(this.tsmAccntBook_Click);
            // 
            // btnPurchaseReport
            // 
            resources.ApplyResources(this.btnPurchaseReport, "btnPurchaseReport");
            this.btnPurchaseReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnPurchaseReport.FlatAppearance.BorderSize = 0;
            this.btnPurchaseReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnPurchaseReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnPurchaseReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPurchaseReport.Name = "btnPurchaseReport";
            this.ttDashboardPOS.SetToolTip(this.btnPurchaseReport, resources.GetString("btnPurchaseReport.ToolTip"));
            this.btnPurchaseReport.UseVisualStyleBackColor = false;
            this.btnPurchaseReport.Click += new System.EventHandler(this.tsmRptPurchase_Click);
            // 
            // btnProfitReport
            // 
            resources.ApplyResources(this.btnProfitReport, "btnProfitReport");
            this.btnProfitReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnProfitReport.FlatAppearance.BorderSize = 0;
            this.btnProfitReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnProfitReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnProfitReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnProfitReport.Name = "btnProfitReport";
            this.ttDashboardPOS.SetToolTip(this.btnProfitReport, resources.GetString("btnProfitReport.ToolTip"));
            this.btnProfitReport.UseVisualStyleBackColor = false;
            // 
            // btnSalesReturnReport
            // 
            resources.ApplyResources(this.btnSalesReturnReport, "btnSalesReturnReport");
            this.btnSalesReturnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnSalesReturnReport.FlatAppearance.BorderSize = 0;
            this.btnSalesReturnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnSalesReturnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSalesReturnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSalesReturnReport.Name = "btnSalesReturnReport";
            this.ttDashboardPOS.SetToolTip(this.btnSalesReturnReport, resources.GetString("btnSalesReturnReport.ToolTip"));
            this.btnSalesReturnReport.UseVisualStyleBackColor = false;
            this.btnSalesReturnReport.Click += new System.EventHandler(this.tsmRptSalesReturn_Click);
            // 
            // btnProductSale
            // 
            resources.ApplyResources(this.btnProductSale, "btnProductSale");
            this.btnProductSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnProductSale.FlatAppearance.BorderSize = 0;
            this.btnProductSale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnProductSale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnProductSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnProductSale.Name = "btnProductSale";
            this.ttDashboardPOS.SetToolTip(this.btnProductSale, resources.GetString("btnProductSale.ToolTip"));
            this.btnProductSale.UseVisualStyleBackColor = false;
            this.btnProductSale.Click += new System.EventHandler(this.tsmItemWiseSalesReport_Click);
            // 
            // btnCategorySale
            // 
            resources.ApplyResources(this.btnCategorySale, "btnCategorySale");
            this.btnCategorySale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.btnCategorySale.FlatAppearance.BorderSize = 0;
            this.btnCategorySale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnCategorySale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnCategorySale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCategorySale.Name = "btnCategorySale";
            this.ttDashboardPOS.SetToolTip(this.btnCategorySale, resources.GetString("btnCategorySale.ToolTip"));
            this.btnCategorySale.UseVisualStyleBackColor = false;
            this.btnCategorySale.Click += new System.EventHandler(this.tsmGrpWiseSlsReport_Click);
            // 
            // btnShowReportPanel
            // 
            resources.ApplyResources(this.btnShowReportPanel, "btnShowReportPanel");
            this.btnShowReportPanel.BackColor = System.Drawing.Color.Crimson;
            this.btnShowReportPanel.ForeColor = System.Drawing.Color.White;
            this.btnShowReportPanel.Name = "btnShowReportPanel";
            this.ttDashboardPOS.SetToolTip(this.btnShowReportPanel, resources.GetString("btnShowReportPanel.ToolTip"));
            this.btnShowReportPanel.UseVisualStyleBackColor = false;
            this.btnShowReportPanel.Click += new System.EventHandler(this.btnShowReportPanel_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SL";
            this.dataGridViewTextBoxColumn1.FillWeight = 30.18542F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ProductName";
            this.dataGridViewTextBoxColumn3.FillWeight = 174.3004F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn4.FillWeight = 71.59125F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "UnitName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "FreeQty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Rate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn7.FillWeight = 97.0194F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Gross";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Discount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "DisAmount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Taxable";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Tax";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "TaxAmount";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "NetAmount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn14.FillWeight = 126.9036F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // DashboardPOS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMdi);
            this.Controls.Add(this.btnShowReportPanel);
            this.Controls.Add(this.pnlReport);
            this.Controls.Add(this.btnPanelShow);
            this.Controls.Add(this.pnlNavigation);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "DashboardPOS";
            this.ttDashboardPOS.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Activated += new System.EventHandler(this.DashboardPOS_Activated);
            this.Load += new System.EventHandler(this.DashboardPOS_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DashboardPOS_KeyDown);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlNavigation.ResumeLayout(false);
            this.tabMdi.ResumeLayout(false);
            this.cmsMasters.ResumeLayout(false);
            this.cmsMainTrans.ResumeLayout(false);
            this.cmsReport.ResumeLayout(false);
            this.cmsStaff.ResumeLayout(false);
            this.cmsOption.ResumeLayout(false);
            this.cmsAbout.ResumeLayout(false);
            this.cmsFirm.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
