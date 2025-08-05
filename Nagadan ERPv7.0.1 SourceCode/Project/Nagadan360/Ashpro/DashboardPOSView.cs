using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class DashboardPOSView : BaseFormTwo
	{
		private DBContext db = new DBContext();

		private Control focusedControl;

		private decimal dCashPayment;

		private decimal dCardPayment;

		private decimal dOtherPayment;

		private decimal? dTendered = null;

		private decimal? dChanges = null;

		private bool isReturn = false;

		private Company company;

		private Button btnProduct;

		private List<POSDetails> entPOSDetails;

		private POSDetails entPosDetail;

		private SalesMaster salesMaster;

		private List<SalesDetails> salesDetails;

		private int? iFYearId;

		private int iTmr = 0;

		private int? iMasterId;

		private DataGridViewTextBoxEditingControl textbox;

		private int iRouteId;

		private int iVehicleId;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private List<Transaction> entsalesdtls = new List<Transaction>();

		private string strVoucherType = "Sales Voucher";

		private string sBranchDetails = string.Empty;

		private DataGridViewCell dgvCell;

		private Button btnGroup;

		private byte[] imageBt = null;

		private int iIndex = 0;

		private int iSalesManID;

		private int iPartyAccountID;

		private int iSalesAccountId;

		private int iUserId;

		private int iNormalCashAccountId;

		private decimal dmlDiscountAmount;

		private bool isReturnAllowed = false;

		private int? iCashAcctId;

		private int? iCardAccount;

		private int? iOtherAccount;

		private InputLanguage defaultLang;

		private POSItemCreation pOSItemCreation;

		private DotmatrixMaster entDMaster;

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private bool isEditing = false;

		private int? rtnInvNumber;

		private List<ExProductPOS> productPOs;

		private int iNoofPages;

		private string sPagedetails;

		private int iPageNumber;

		private decimal dSubDiscount;

		private SecondDisplayView SecondDisplay = new SecondDisplayView();

		private System.Windows.Forms.Timer idleTimer = new System.Windows.Forms.Timer();

		private string remarks;

		private IContainer components = null;

		private Panel pnlNumberPad;

		private DashboardPOSKeyboard dashboardPOSKeyboard1;

		private AshUpDown nudInvoiceNo;

		private Button btnbarcode;

		private Panel pnlGroupBar;

		private FlowLayoutPanel pnlGroup;

		private Button btnTop;

		private Button btnDwn;

		private Button btnLast;

		private Button btnNext;

		private Button btnBack;

		private Button btnFirst;

		private Button btnpush;

		private ComboBox cmbwarehouse;

		public AshTBox txtsearchitemname;

		private ComboBox cmbwhole;

		public TextBox txtBarcode;

		public AshTBox txtqty;

		private CheckBox checkBox1;

		private FlowLayoutPanel flpTopMenu;

		private Button btnSearchBill;

		public Button btnfastSale;

		private Button btnpro;

		private Button btndrawer;

		private Button btnprint;

		public Button btnRetrieveBill;

		private Button btncancelsale;

		public Button btnnewsale;

		private Button btnsearchcustom;

		private Button btnReturn;

		private FlowLayoutPanel flpProduct;

		private Panel pnlSidebar;

		private Button btneditline;

		public Button btnstck;

		private Button btnsearch;

		private Button btnlindisc;

		private Button btnup;

		private Button btndltline;

		private Button btndown;

		private Panel pnlDetails;

		private AshNumericLabel lblChanges;

		private AshNumericLabel lblLastBill;

		private Label lChanges;

		private Label lLastBill;

		private Label lblCustomer;

		private Label lblUser;

		private CheckBox cbPrint;

		private AshNumericLabel lblGrandTotal;

		private Label lblqty;

		public Label lblcust;

		private Label lblsubtotal;

		public Label lblloginname;

		public AshTBox lblTaxTotal;

		public AshTBox lblBalance;

		public AshTBox txtAddDiscountAmnt;

		private Label lbltotalLabel;

		private Label label1;

		private Label lbldis;

		public AshTBox lblTotalQty;

		private Label lbltax;

		public AshTBox lblGross;

		public AshTBox lblTotal;

		private Panel pnlGrid;

		private DataGridView dgItems;

		private BindingSource bdsItems;

		private System.Windows.Forms.Timer tmrGrandTotal;

		private ToolTip ttDashboardPOS;

		private Panel panel1;

		private Panel panel2;

		private AshTextBoxNormal txtPrintCopy;

		private Label lblCount;

		private Button btnDiscount;

		private AshTextBoxNormal txtPCopy2;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn col_ProductCode;
        private DataGridViewTextBoxColumn col_ProductName;
        private DataGridViewTextBoxColumn col_Qty;
        private DataGridViewTextBoxColumn col_Unit;
        private DataGridViewTextBoxColumn col_FreeQty;
        private DataGridViewTextBoxColumn col_Rate;
        private DataGridViewTextBoxColumn col_Gross;
        private DataGridViewTextBoxColumn col_Discount;
        private DataGridViewTextBoxColumn col_DisAmount;
        private DataGridViewTextBoxColumn col_Taxable;
        private DataGridViewTextBoxColumn col_Tax;
        private DataGridViewTextBoxColumn col_TaxAmount;
        private DataGridViewTextBoxColumn col_NetAmount;
        private CheckBox cbPrint2;

		public UserPrevilegeDtl previlegeDtlPOS { get; set; }

		public DashboardPOS dashboardPOS { get; set; }

		public DashboardPOSView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			txtBarcode.Enter += TxtBarcode_Enter;
			txtBarcode.LostFocus += delegate
			{
				InputLanguage.CurrentInputLanguage = defaultLang;
			};
			defaultLang = InputLanguage.FromCulture(Thread.CurrentThread.CurrentCulture);
			if (Settings.Default.Language == "ar")
			{
				flpTopMenu.FlowDirection = FlowDirection.RightToLeft;
			}
			idleTimer.Interval = 1000;
			idleTimer.Tick += IdleTimer_Tick;
			idleTimer.Enabled = true;
		}

		private void IdleTimer_Tick(object sender, EventArgs e)
		{
			if (CommonSettings.blnShowSecondDisplay && Win32.GetIdleTime() > Settings.Default.IdleTime)
			{
				SecondDisplay.ShowImageSlider();
			}
			try
			{
				if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty && Win32.GetIdleTime() > 30000)
				{
					AniHelper.Display(entcompany.CompanyName, "");
				}
			}
			catch (NullReferenceException)
			{
				CommonSettings.blnEnableCutomerDisplay = false;
			}
		}

		private void TxtBarcode_Enter(object sender, EventArgs e)
		{
			if (InputLanguage.InstalledInputLanguages.Count != 1)
			{
				CultureInfo culture = new CultureInfo("en-US");
				InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
			}
		}

		private void PrintMethod(DotmatrixMaster entDMaster, int iCopy = 1)
		{
			if (entDMaster != null)
			{
				List<InvoiceAdjustment> list = new List<InvoiceAdjustment>();
				entDSettings = new List<DotmatrixSettings>();
				entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entcompany.TaxId = company.GSTNo;
				entcompany.Logo = company.Logo;
				entPartyDtl = new PartyDetails();
				AccountEntity accountEntity = db.Accounts(new
				{
					AccountId = iPartyAccountID
				}).FirstOrDefault();
				entPartyDtl = POSHelper.GetParty(accountEntity);
				if (!iMasterId.HasValue)
				{
					SalesMasterMethod(null, cmbwhole.SelectedIndex.ToInt32(), txtAddDiscountAmnt.Value, lblGrandTotal.Value, lblBalance.Value);
					salesMaster.CntrInvNo = string.Empty;
				}
				int num = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
				if (accountEntity.AGroupId == num)
				{
					entPartyDtl.Name = "Cash Customer";
				}
				TransactionMaster transactionMaster = new TransactionMaster();
				bool isCashAccount = ((iPartyAccountID == iNormalCashAccountId) ? true : false);
				transactionMaster = POSHelper.GetTMaster(salesMaster, isCashAccount);
				transactionMaster.TotalQuantity = entPOSDetails.Sum((POSDetails x) => x.Qty);
				transactionMaster.GrossAmount = entPOSDetails.Sum((POSDetails x) => x.Gross);
				transactionMaster.OrderNo = 0;
				transactionMaster.Tendered = dTendered;
				transactionMaster.Changes = dChanges;
				transactionMaster.SalesMan = lblUser.Text;
				transactionMaster.PriceCategory = cmbwhole.Text;
				if (lblGrandTotal.Value > 0m)
				{
					if (iPartyAccountID == iNormalCashAccountId)
					{
						transactionMaster.InvoiceMode = "Cash Sale";
					}
					else if (lblBalance.Value == 0m)
					{
						transactionMaster.InvoiceMode = "Cash Sale";
					}
					else
					{
						transactionMaster.InvoiceMode = "Credit Sale";
					}
				}
				else
				{
					transactionMaster.InvoiceMode = "Sales Return";
				}
				foreach (POSDetails item in entPOSDetails)
				{
					string nm = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item.Tax).TCategoryName;
					if (!list.Any((InvoiceAdjustment x) => x.Name == nm))
					{
						decimal num2 = entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.TaxAmount);
						AniHelper.AddPaymentPrint(sRLabel: entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.NetAmount).ToString(CommonSettings.strCurencyFormat), paymentPrint: list, sName: nm, dValue: num2.ToString(CommonSettings.strCurencyFormat));
					}
				}
				entsalesdtls = new List<Transaction>();
				entsalesdtls = POSHelper.GetTransactionFromPOSDetails(entPOSDetails, db, CommonSettings.strDefaultSalesType);
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, transactionMaster, entPartyDtl, entsalesdtls, transactionMaster, list, transactionMaster, isPrint: true, iCopy);
				printHelper = null;
			}
			else
			{
				Messages.InformationMessage("There is no default print design. Please make your own design");
			}
		}

		private void ShowSecondDisplay()
		{
			if (CommonSettings.blnShowSecondDisplay)
			{
				if (Screen.AllScreens.Length > 1)
				{
					SecondDisplay.Show();
					return;
				}
				Messages.ErrorMessage("Second Display not Connected!");
				CommonSettings.blnShowSecondDisplay = false;
			}
		}

		private void CellContentMethod()
		{
			if (dgItems.Rows.Count == 0)
			{
				return;
			}
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			if (dgItems.CurrentCell.OwningColumn.Name == col_Unit.Name)
			{
				SelectCustomer selectCustomer = new SelectCustomer("Unit");
				int num = (selectCustomer.ProductId = entPosDetail.productId);
				if (selectCustomer.ShowDialog() == DialogResult.OK)
				{
					entPosDetail.UnitName = selectCustomer.sName.Trim();
					entPosDetail.UnitId = selectCustomer.iId;
					ProductUnit productUnit = db.Productunits(new
					{
						ProductId = entPosDetail.productId,
						UnitId = selectCustomer.iId
					}).FirstOrDefault();
					entPosDetail.ProductName = productUnit.ProductUnitName;
					entPosDetail.Barcode = productUnit.Barcode;
					POSHelper.SellingPriceAndDiscFindMethod(cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", productUnit.Barcode);
					entPosDetail.Rate = POSHelper.dmlRate;
					entPosDetail.Discount = POSHelper.dmlDiscount;
					bool isInclusive = productUnit.IsInclusive;
					entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
					entPosDetail.DisAmount = entPosDetail.Rate * (entPosDetail.Discount / 100m);
					entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
					int? tCategoryId = db.Products(new
					{
						ProductId = entPosDetail.productId
					}).FirstOrDefault().TCategoryId;
					entPosDetail.Tax = db.TaxCategories(new
					{
						TCategoryId = tCategoryId
					}).FirstOrDefault().SalesVat;
					if (!isInclusive)
					{
						entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
						entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
					}
					else
					{
						entPosDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPosDetail.Taxable, entPosDetail.Tax);
						entPosDetail.NetAmount = entPosDetail.Taxable;
						entPosDetail.Taxable = entPosDetail.NetAmount - entPosDetail.TaxAmount;
					}
					TotalFindMethod();
					dgItems.Refresh();
				}
			}
			else
			{
				entPosDetail = new POSDetails();
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			}
			ProductStockFindMethod();
		}

		private void ProductStockFindMethod()
		{
			try
			{
				string barcode = null;
				ProductEntity productEntity = db.Products(new
				{
					ProductId = entPosDetail.productId
				}).ToList().FirstOrDefault();
				if (productEntity.UnitId == entPosDetail.UnitId)
				{
					barcode = entPosDetail.Barcode;
				}
				btnstck.Text = AshproCommon.GetDecimalDataWithParameter_SP("spProductUnitWiseStock", new
				{
					GodwnId = cmbwarehouse.SelectedValue.ToInt32(),
					ProductId = entPosDetail.productId,
					BranchId = Settings.Default.iBranchId,
					UnitId = entPosDetail.UnitId,
					Barcode = barcode
				}).ToString("G29");
			}
			catch (Exception)
			{
				btnstck.Text = "0";
			}
		}

		private bool LoadInitialSettings()
		{
			try
			{
				iUserId = Settings.Default.intLogOnId;
				AniHelper.FillPCategory(cmbwhole);
				AniHelper.FillGodown(cmbwarehouse);
				iSalesAccountId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'SALES ACCOUNT'");
				iRouteId = AshproCommon.IdFindMethod("Select RootId From Root Where RootName='ALL' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: false);
				iVehicleId = AshproCommon.IdFindMethod("Select VanId From Vehicle Where VehName='ALL' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: false);
				iFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear", isVoucherNo: false);
				if (CommonSettings.blnEnableMultiBranch)
				{
					iSalesManID = iUserId;
				}
				else
				{
					iSalesManID = db.EmployeeDetails().FirstOrDefault().EmpID.ToInt32();
				}
				lblUser.Text = db.Users().FirstOrDefault((User x) => x.UserId == iUserId).UserName;
				return true;
			}
			catch (Exception)
			{
				return true;
			}
		}

		private void TotalFindMethod()
		{
			try
			{
				decimal num = txtAddDiscountAmnt.Text.ToDecimal();
				decimal num2 = entPOSDetails.Sum((POSDetails x) => x.NetAmount) - num;
				decimal num3 = Math.Round(num2, 0);
				_ = num2 - num3;
				lblTotal.Text = entPOSDetails.Sum((POSDetails x) => x.NetAmount).ToString(CommonSettings.strCurencyFormat);
				lblTotalQty.Text = entPOSDetails.Sum((POSDetails x) => x.Qty).ToString(CommonSettings.strCurencyFormat);
				lblGross.Text = entPOSDetails.Sum((POSDetails x) => x.Taxable).ToString(CommonSettings.strCurencyFormat);
				dmlDiscountAmount = entPOSDetails.Sum((POSDetails x) => x.DisAmount);
				lblTaxTotal.Text = entPOSDetails.Sum((POSDetails x) => x.TaxAmount).ToString(CommonSettings.strCurencyFormat);
				lblGrandTotal.Text = num2.ToString(CommonSettings.strCurencyFormat);
				lblBalance.Value = num2 - (dCashPayment + dCardPayment + dOtherPayment);
				tmrGrandTotal.Enabled = true;
				if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty && dgItems.Rows.Count > 0 && entPosDetail != null)
				{
					if (entPosDetail.ProductName == null)
					{
						return;
					}
					string text = entPosDetail.ProductName;
					if (text.Length > 12)
					{
						text = text.Remove(11);
					}
					AniHelper.Display(text + " " + (entPosDetail.NetAmount / entPosDetail.Qty).ToString(CommonSettings.strCurencyFormat), "Total : " + lblGrandTotal.Value.ToString(CommonSettings.strCurencyFormat));
				}
				if (CommonSettings.blnShowSecondDisplay && lblGrandTotal.Value > 0m && entPosDetail.productId != 0)
				{
					Image imgProduct = null;
					string sMessage = entPosDetail.ProductName + " \n" + entPosDetail.Qty + " " + entPosDetail.UnitName + " \n" + entPosDetail.Rate.ToString(CommonSettings.strCurencyFormat) + " ";
					byte[] image = db.Products(new
					{
						ProductId = entPosDetail.productId
					}).FirstOrDefault().image;
					if (image != null)
					{
						imgProduct = AniHelper.byteArrayToImage(image);
					}
					SecondDisplay.ShowProductDetails(sMessage, lblGrandTotal.Value.ToString(), imgProduct, entPosDetail.ProductName);
				}
			}
			catch (Exception)
			{
			}
		}

		private void SalesMasterMethod(int? InvoiceNo, int iPCategory, decimal AddDisc, decimal GrandTotal, decimal dBalance, bool isPOSHold = false)
		{
			salesMaster = new SalesMaster();
			salesMaster.SalesInvId = iMasterId;
			salesMaster.SalesInvNo = InvoiceNo;
			salesMaster.InvType = CommonSettings.strDefaultSalesType;
			salesMaster.SalesDate = DateTime.Now;
			salesMaster.SalesAccountId = iSalesAccountId;
			salesMaster.PartyAccountId = iPartyAccountID;
			salesMaster.RouteId = iRouteId;
			salesMaster.VehId = iVehicleId;
			salesMaster.StafId = iSalesManID;
			salesMaster.JEId = Settings.Default.intLogOnId;
			salesMaster.PriceCategoryId = iPCategory;
			salesMaster.AditionalAmount = 0m;
			salesMaster.Frieght = 0m;
			salesMaster.BillDiscount = AddDisc;
			salesMaster.TotalDiscount = dmlDiscountAmount;
			salesMaster.GrandDiscount = GrandTotal;
			salesMaster.EntryDate = DateTime.Now.Date;
			salesMaster.Remarks = remarks;
			salesMaster.TaxableAmount = entPOSDetails.Sum((POSDetails x) => x.Taxable);
			salesMaster.FYearID = iFYearId;
			salesMaster.TotalTaxAmount = entPOSDetails.Sum((POSDetails x) => x.TaxAmount);
			salesMaster.FloodCess = 0m;
			if (CommonSettings.blnGSTEnabled)
			{
				if (CommonSettings.strDefaultSalesType.Contains("Interstate"))
				{
					salesMaster.IGSTAmount = salesMaster.TotalTaxAmount;
					salesMaster.SGSTAmount = 0m;
					salesMaster.CGSTAmount = 0m;
				}
				else
				{
					salesMaster.IGSTAmount = 0m;
					salesMaster.SGSTAmount = salesMaster.TotalTaxAmount / 2m;
					salesMaster.CGSTAmount = salesMaster.TotalTaxAmount / 2m;
				}
			}
			salesMaster.FK_BranchId = Settings.Default.iBranchId;
			salesMaster.CardPayment = dCardPayment;
			if (!isPOSHold && dCashPayment == 0m && dBalance == GrandTotal && iPartyAccountID == iNormalCashAccountId)
			{
				if (!isReturn)
				{
					salesMaster.CashPayment = dBalance;
				}
				else
				{
					salesMaster.CashPayment = 0m;
				}
			}
			else if (!isReturn)
			{
				salesMaster.CashPayment = dCashPayment;
			}
			else
			{
				salesMaster.CashPayment = dCashPayment * -1m;
			}
			if (iPartyAccountID != iNormalCashAccountId)
			{
				if (!isReturn)
				{
					salesMaster.Credit = dBalance;
				}
				else
				{
					salesMaster.Credit = dBalance * -1m;
				}
			}
			if (!iMasterId.HasValue)
			{
				salesMaster.CntrInvNo = Settings.Default.CounterName;
			}
			else
			{
				SalesMaster obj = salesMaster;
				string counterName = Settings.Default.CounterName;
				int? num = InvoiceNo;
				obj.CntrInvNo = counterName + num;
			}
			salesMaster.SequenceNo = Utility.uSerialNumber;
			salesMaster.Terminal = Utility.uTerminal;
			salesMaster.IsClosed = false;
			salesMaster.FreeAmount = FreeAmountTotalFindMethod();
			if (iPartyAccountID == iNormalCashAccountId)
			{
				salesMaster.ReturnAmount = ((GrandTotal > 0m) ? 0m : GrandTotal);
			}
			else
			{
				salesMaster.ReturnAmount = 0m;
			}
			salesMaster.FielOne = rtnInvNumber.ToString();
			salesMaster.OtherPayment = dOtherPayment;
			salesMaster.CashAccountId = iCashAcctId;
			salesMaster.CardAccountId = iCardAccount;
			salesMaster.OnlineAccountId = iOtherAccount;
			salesMaster.IsSettiled = ((!(salesMaster.Credit > 0m)) ? true : false);
		}

		private decimal FreeAmountTotalFindMethod()
		{
			decimal result = default(decimal);
			try
			{
				foreach (POSDetails item in entPOSDetails.Where((POSDetails x) => x.Rate == 0m).ToList())
				{
					result += AshproCommon.ValueFindMethod("SELECT SPrice FROM ProductPriceCategory where ProductId='" + item.productId + "' and PCategoryId='" + cmbwhole.SelectedValue.ToInt32() + "' and UnitId='" + item.UnitId + "'");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return result;
		}

		private void ReloadMethod(SalesMaster salesMaster, bool isSales = false)
		{
			try
			{
				if (salesMaster == null)
				{
					return;
				}
				salesDetails = new List<SalesDetails>();
				if (!isSales)
				{
					SalesMaster salesMaster2 = db.POSHoldMasters(new
					{
						InvType = salesMaster.InvType,
						SalesInvNo = salesMaster.SalesInvNo,
						FYearID = iFYearId,
						FK_BranchId = Settings.Default.iBranchId
					}).FirstOrDefault();
					if (salesMaster2 == null)
					{
						return;
					}
					iMasterId = salesMaster2.SalesInvId.toInt32();
					salesDetails = db.POSHoldSettings(new
					{
						SalesInvId = iMasterId
					}).ToList();
				}
				else
				{
					nudInvoiceNo.Value = salesMaster.SalesInvNo.toInt32();
					iMasterId = db.SalesMasters(new
					{
						InvType = salesMaster.InvType,
						SalesInvNo = salesMaster.SalesInvNo,
						FYearID = iFYearId,
						FK_BranchId = Settings.Default.iBranchId
					}).FirstOrDefault().SalesInvId.toInt32();
					salesDetails = db.SalesDetails(new
					{
						SalesInvId = iMasterId
					}).ToList();
				}
				LoadMethod(salesMaster, salesDetails);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BindDgv()
		{
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			txtBarcode.Text = string.Empty;
		}

		private void LoadMethod(SalesMaster salesMaster, List<SalesDetails> salesDetails)
		{
			entPOSDetails = new List<POSDetails>();
			entPOSDetails = POSHelper.GetPOSDetailsFromSDetails(salesDetails);
			for (int i = 0; i < entPOSDetails.Count; i++)
			{
				entPOSDetails[i].SL = i + 1;
			}
			decimal num = default(decimal);
			bdsItems.DataSource = null;
			dgItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			decimal num2 = salesMaster.GrandDiscount + salesMaster.BillDiscount;
			decimal billDiscount = salesMaster.BillDiscount;
			if (billDiscount > 0m)
			{
				num = billDiscount * 100m / num2;
			}
			txtAddDiscountAmnt.Text = billDiscount.ToString(CommonSettings.strCurencyFormat);
			cmbwhole.SelectedValue = salesMaster.PriceCategoryId;
			iPartyAccountID = salesMaster.PartyAccountId.ToInt32();
			lblCustomer.Text = db.Accounts(new
			{
				AccountId = iPartyAccountID
			}).FirstOrDefault().AccountName;
			iSalesManID = salesMaster.StafId.ToInt32();
			iSalesAccountId = salesMaster.SalesAccountId.ToInt32();
			dCashPayment = salesMaster.CashPayment;
			dCardPayment = salesMaster.CardPayment;
			dOtherPayment = salesMaster.OtherPayment;
			iCashAcctId = salesMaster.CashAccountId;
			iCardAccount = salesMaster.CardAccountId;
			iOtherAccount = salesMaster.OnlineAccountId;
			TotalFindMethod();
			FocusMethod();
		}

		private void RowDeleteMethod()
		{
			if (dgItems.CurrentRow == null)
			{
				return;
			}
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			entPOSDetails.Remove(entPosDetail);
			for (int i = 0; i < entPOSDetails.Count; i++)
			{
				entPOSDetails[i].SL = i + 1;
			}
			BindDgv();
			TotalFindMethod();
			if (entPOSDetails.Count == 0)
			{
				if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
				{
					AniHelper.Display(company.InstituteName, "");
				}
				btnstck.Text = "0";
			}
			FocusMethod();
		}

		private void DeleteSubMethod()
		{
			string[] obj = new string[6] { "Delete From GdwnStckTranLog Where VchrId='", null, null, null, null, null };
			int? num = iMasterId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType ='";
			obj[3] = CommonSettings.strDefaultSalesType;
			obj[4] = "' ";
			obj[5] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[8] { "Delete From StockTranLog Where VchrId='", null, null, null, null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType ='";
			obj2[3] = strVoucherType;
			obj2[4] = "' and VchrFormType = '";
			obj2[5] = CommonSettings.strDefaultSalesType;
			obj2[6] = "'";
			obj2[7] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj2));
			DataTable dataTable = new DataTable();
			string[] obj3 = new string[6] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj3[1] = num.ToString();
			obj3[2] = "' and VchrType='";
			obj3[3] = strVoucherType;
			obj3[4] = "'";
			obj3[5] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj3));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
			string[] obj4 = new string[6] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj4[1] = num.ToString();
			obj4[2] = "'and VchrNo='";
			obj4[3] = nudInvoiceNo.Value.ToInt32().ToString();
			obj4[4] = "' and VchrType='Journal Voucher' and VchrName='TAX PAYABLE'";
			obj4[5] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj4));
			foreach (DataRow row2 in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row2[0].ToString().ToInt32());
			}
			AshproCommon.DeleteMethod("Delete From SalesEstimateSettings Where SalesInvId ='" + iMasterId.ToInt32() + "'");
			AshproCommon.DeleteMethod("Delete From TransactionLog Where Narration = 'POS" + iMasterId + "' " + sBranchDetails);
			AshproCommon.DeleteMethod("Delete From VchrTranLog Where Narration = 'POS" + iMasterId + "' " + sBranchDetails);
		}

		private void SaveMethodSub(int InvoiceNo, int iDepotId)
		{
			if (!AshproCommon.InsertToDatabase(salesDetails.Cast<object>().ToList(), "SalesEstimateSettings"))
			{
				return;
			}
			POSHelper.TranLogMethod(0m, salesMaster.GrandDiscount, iSalesAccountId, iPartyAccountID, iMasterId.toInt32(), InvoiceNo, strVoucherType, iUserId, "Main", CommonSettings.strDefaultSalesType);
			POSHelper.TranLogMethod(salesMaster.GrandDiscount, 0m, iPartyAccountID, iSalesAccountId, iMasterId.toInt32(), InvoiceNo, strVoucherType, iUserId, "Sub", CommonSettings.strDefaultSalesType);
			if (CommonSettings.blnTaxEnabled && lblTaxTotal.Text.ToDecimal() > 0m)
			{
				int num = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
				POSHelper.TranLogMethod(salesMaster.TotalTaxAmount, 0m, iSalesAccountId, num, iMasterId.toInt32(), InvoiceNo, "Journal Voucher", iUserId, "Main", "TAX PAYABLE");
				POSHelper.TranLogMethod(0m, salesMaster.TotalTaxAmount, num, iSalesAccountId, iMasterId.toInt32(), InvoiceNo, "Journal Voucher", iUserId, "Sub", "TAX PAYABLE");
			}
			if (CommonSettings.blnCessEnabled && salesMaster.FloodCess > 0m)
			{
				int num2 = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
				POSHelper.TranLogMethod(salesMaster.FloodCess, 0m, iSalesAccountId, num2, iMasterId.toInt32(), salesMaster.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX PAYABLE");
				POSHelper.TranLogMethod(0m, salesMaster.FloodCess, num2, iSalesAccountId, iMasterId.toInt32(), salesMaster.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX PAYABLE");
			}
			POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.toInt32(), InvoiceNo, strVoucherType, CommonSettings.strDefaultSalesType, iPartyAccountID, "POS Entry", iUserId, iDepotId);
			if (dCashPayment > 0m)
			{
				if (!isReturn)
				{
					int num3 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num3, DateTime.Now.Date, dCashPayment, "Receipt Voucher", "POS" + iMasterId, iPartyAccountID, iCashAcctId.ToInt32(), iSalesManID, iUserId, "Cash Receipt");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num3 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dCashPayment, 0m, iCashAcctId.ToInt32(), iPartyAccountID, iVoucherID, num3, "Receipt Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dCashPayment, iPartyAccountID, iCashAcctId.ToInt32(), iVoucherID, num3, "Receipt Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
				else
				{
					int num4 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Payment Voucher' and VchrName='Cash Payment'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num4, DateTime.Now.Date, dCashPayment, "Payment Voucher", "POS" + iMasterId, iCashAcctId.ToInt32(), iPartyAccountID, iSalesManID, iUserId, "Cash Payment");
					int iVoucherID2 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num4 + "' and VchrType ='Payment Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dCashPayment, 0m, iPartyAccountID, iCashAcctId.ToInt32(), iVoucherID2, num4, "Payment Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dCashPayment, iCashAcctId.ToInt32(), iPartyAccountID, iVoucherID2, num4, "Payment Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
			}
			if (dCardPayment > 0m)
			{
				if (!isReturn)
				{
					int num5 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num5, DateTime.Now.Date, dCardPayment, "Receipt Voucher", "POS" + iMasterId, iPartyAccountID, iCardAccount.ToInt32(), iSalesManID, iUserId, "Bank Receipt");
					int iVoucherID3 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num5 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dCardPayment, 0m, iCardAccount.ToInt32(), iPartyAccountID, iVoucherID3, num5, "Receipt Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dCardPayment, iPartyAccountID, iCardAccount.ToInt32(), iVoucherID3, num5, "Receipt Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
				else
				{
					int num6 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num6, DateTime.Now.Date, dCardPayment, "Payment Voucher", "POS" + iMasterId, iCardAccount.ToInt32(), iPartyAccountID, iSalesManID, iUserId, "Bank Payment");
					int iVoucherID4 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num6 + "' and VchrType ='Payment Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dCardPayment, 0m, iPartyAccountID, iCardAccount.ToInt32(), iVoucherID4, num6, "Payment Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dCardPayment, iCardAccount.ToInt32(), iPartyAccountID, iVoucherID4, num6, "Payment Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
			}
			if (dOtherPayment > 0m)
			{
				if (!isReturn)
				{
					int num7 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Bank Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num7, DateTime.Now.Date, dOtherPayment, "Receipt Voucher", "POS" + iMasterId, iPartyAccountID, iOtherAccount.ToInt32(), iSalesManID, iUserId, "Bank Receipt");
					int iVoucherID5 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num7 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dOtherPayment, 0m, iOtherAccount.ToInt32(), iPartyAccountID, iVoucherID5, num7, "Receipt Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dOtherPayment, iPartyAccountID, iOtherAccount.ToInt32(), iVoucherID5, num7, "Receipt Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
				else
				{
					int num8 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Payment Voucher' and VchrName='Bank Payment'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num8, DateTime.Now.Date, dOtherPayment, "Payment Voucher", "POS" + iMasterId, iOtherAccount.ToInt32(), iPartyAccountID, iSalesManID, iUserId, "Bank Payment");
					int iVoucherID6 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num8 + "' and VchrType ='Payment Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(dOtherPayment, 0m, iPartyAccountID, iOtherAccount.ToInt32(), iVoucherID6, num8, "Payment Voucher", iUserId, "Main", null, null, "POS" + iMasterId);
					POSHelper.TranLogMethod(0m, dOtherPayment, iOtherAccount.ToInt32(), iPartyAccountID, iVoucherID6, num8, "Payment Voucher", iUserId, "Sub", null, null, "POS" + iMasterId);
				}
			}
		}

		private void btnClearMethod()
		{
			entPOSDetails.Clear();
			entPOSDetails = new List<POSDetails>();
			bdsItems.DataSource = null;
			dgItems.DataSource = null;
			entPosDetail = new POSDetails();
			AniHelper.ClearMethod(this);
			txtAddDiscountAmnt.Value = 0m;
			decimal num2 = (lblBalance.Value = default(decimal));
			dCardPayment = (dCashPayment = (dOtherPayment = num2));
			iMasterId = null;
			btnstck.Text = "0";
			iPartyAccountID = iNormalCashAccountId;
			AccountEntity accountEntity = db.Accounts(new
			{
				AccountId = iPartyAccountID
			}).FirstOrDefault();
			lblCustomer.Text = accountEntity.AccountName;
			lblCustomer.Tag = accountEntity.AccountId;
			dmlDiscountAmount = default(decimal);
			isReturn = false;
			isReturnAllowed = false;
			dTendered = (dChanges = default(decimal));
			TotalFindMethod();
			txtBarcode.Select();
			iCashAcctId = CommonSettings.strDefaultCashAccountId.ToInt32();
			iCardAccount = CommonSettings.strDefaultBankAccountId.ToInt32();
			iOtherAccount = CommonSettings.strDefaultOnlineAccountId.ToInt32();
			txtPrintCopy.Data = Settings.Default.iPrintCopy;
			isEditing = false;
			rtnInvNumber = null;
			dSubDiscount = default(decimal);
			remarks = string.Empty;
			FocusMethod();
		}

		private void DeleteHoldedBill()
		{
			try
			{
				AshproCommon.DeleteMethod("Delete From POSHoldSettings where SalesInvId = '" + iMasterId.ToInt32() + "'");
				AshproCommon.DeleteMethod("Delete From POSHoldMaster Where Id = '" + iMasterId.ToInt32() + "'");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			iMasterId = null;
		}

		private void HoldBillCountMethod()
		{
			try
			{
				btnRetrieveBill.Text = db.POSHoldMasters(new
				{
					InvType = CommonSettings.strDefaultSalesType
				}).Count.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BarcodeMethodWithName()
		{
			if (txtsearchitemname.Text == string.Empty)
			{
				return;
			}
			if (salesMaster != null && salesMaster.IsClosed)
			{
				Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
				return;
			}
			ProductEntity product = new ProductEntity();
			decimal? dmlRate = null;
			int? iUnitId = null;
			product = db.Products(new
			{
				ProductName = txtsearchitemname.Text
			}).FirstOrDefault();
			if (product != null)
			{
				decimal? dmlQty = null;
				ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
				int pOSDetails = POSHelper.GetPOSDetails(product, entPOSDetails, cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", dmlQty, product.Barcode, iUnitId, dmlRate);
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.productId == product.ProductId).FirstOrDefault();
				BindDgv();
				TotalFindMethod();
				ProductStockFindMethod();
				dgItems.ClearSelection();
				dgItems.FirstDisplayedScrollingRowIndex = pOSDetails;
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgItems.CurrentCell = dgItems.Rows[pOSDetails].Cells[col_ProductName.Name];
				txtsearchitemname.Text = string.Empty;
				FocusMethod();
			}
		}

		private async void BarcodeAddmethod()
		{
			try
			{
				if (txtBarcode.Text == string.Empty)
				{
					FocusMethod();
					return;
				}
				if (salesMaster != null && salesMaster.IsClosed)
				{
					Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
					return;
				}
				ProductEntity product = new ProductEntity();
				product = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
				{
					Barcode = txtBarcode.Text
				});
				if (product != null && product.ProductName != null)
				{
					decimal? dmlRate2 = null;
					decimal? dmlQty2 = null;
					int? iUnitId = null;
					if (product != null)
					{
						ScaleWithNoRateMethod(product, ref dmlRate2, ref dmlQty2, ref iUnitId);
					}
					if (product != null)
					{
						int? num = iUnitId;
						int iRowIndex2 = POSHelper.GetPOSDetails(iUnitId: num.HasValue ? ((object)num.GetValueOrDefault()) : AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + txtBarcode.Text + "'"), sProName: AshproCommon.IdFindMethod("Select ProductUnitName From ProductUnit Where Barcode='" + txtBarcode.Text + "'"), productEntity: product, pDetails: entPOSDetails, sPCategoryID: cmbwhole.SelectedValue.ToInt32(), fType: "Sales Invoice", Qty: dmlQty2, Barcode: txtBarcode.Text, dRate: dmlRate2);
						entPosDetail = entPOSDetails.Where((POSDetails x) => x.Barcode == txtBarcode.Text).FirstOrDefault();
						BindDgv();
						TotalFindMethod();
						ProductStockFindMethod();
						dgItems.ClearSelection();
						dgItems.FirstDisplayedScrollingRowIndex = iRowIndex2;
						dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
						dgItems.CurrentCell = dgItems.Rows[iRowIndex2].Cells[col_ProductName.Name];
						FocusMethod();
					}
				}
				else if (txtBarcode.Text.Length != 13)
				{
					NewProductCreatmethod();
					await AniHelper.AutoCompleteMethod(txtsearchitemname);
				}
				else
				{
					int iDPlace = 0;
					if (CommonSettings.strRatebasedBarcode.ToInt32() != 0)
					{
						iDPlace = CommonSettings.strRatebasedBarcode.Length;
					}
					_ = string.Empty;
					_ = string.Empty;
					string Barcode;
					string sRateBlock;
					if (iDPlace == 2)
					{
						Barcode = txtBarcode.Text.Substring(0, 6);
						sRateBlock = txtBarcode.Text.Substring(6, 6);
					}
					else
					{
						Barcode = txtBarcode.Text.Substring(0, 7);
						sRateBlock = txtBarcode.Text.Substring(7, 5);
					}
					product = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new { Barcode });
					if (product != null)
					{
						if (product.ProductName != null)
						{
							string sCode = txtBarcode.Text.Substring(0, iDPlace);
							decimal? dmlRate = null;
							decimal? dmlQty = null;
							if (sCode == CommonSettings.strRatebasedBarcode)
							{
								_ = string.Empty;
								_ = string.Empty;
								int iDecimal2 = 2;
								if (CommonSettings.strCurencyFormat != string.Empty)
								{
									iDecimal2 = CommonSettings.strCurencyFormat.Substring(1, 1).ToInt32();
								}
								string mRate;
								string sRate;
								if (iDecimal2 == 2)
								{
									if (sRateBlock.Length == 6)
									{
										mRate = sRateBlock.Substring(0, 4);
										sRate = sRateBlock.Substring(4, 2);
									}
									else
									{
										mRate = sRateBlock.Substring(0, 3);
										sRate = sRateBlock.Substring(3, 2);
									}
								}
								else if (sRateBlock.Length == 6)
								{
									mRate = sRateBlock.Substring(0, 3);
									sRate = sRateBlock.Substring(3, 3);
								}
								else
								{
									mRate = sRateBlock.Substring(0, 2);
									sRate = sRateBlock.Substring(2, 3);
								}
								dmlRate = (mRate + "." + sRate).ToDecimal();
							}
							else if (sCode == CommonSettings.strWeightbasedBarcode)
							{
								_ = string.Empty;
								_ = string.Empty;
								int iDecimal = 2;
								if (CommonSettings.strQuantityFormat != string.Empty)
								{
									iDecimal = CommonSettings.strQuantityFormat.Substring(1, 1).ToInt32();
								}
								string mQty;
								string sQty;
								if (iDecimal == 2)
								{
									if (sRateBlock.Length == 6)
									{
										mQty = sRateBlock.Substring(0, 4);
										sQty = sRateBlock.Substring(4, 2);
									}
									else
									{
										mQty = sRateBlock.Substring(0, 3);
										sQty = sRateBlock.Substring(3, 2);
									}
								}
								else if (sRateBlock.Length == 6)
								{
									mQty = sRateBlock.Substring(0, 3);
									sQty = sRateBlock.Substring(3, 3);
								}
								else
								{
									mQty = sRateBlock.Substring(0, 2);
									sQty = sRateBlock.Substring(2, 3);
								}
								dmlQty = (mQty + "." + sQty).ToDecimal();
							}
							if (product != null)
							{
								int iRowIndex = POSHelper.GetPOSDetails(iUnitId: AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + Barcode + "'"), productEntity: product, pDetails: entPOSDetails, sPCategoryID: cmbwhole.SelectedValue.ToInt32(), fType: "Sales Invoice", Qty: dmlQty, Barcode: Barcode, dRate: dmlRate);
								entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == product.ProductCode.ToString()).FirstOrDefault();
								BindDgv();
								TotalFindMethod();
								ProductStockFindMethod();
								dgItems.ClearSelection();
								dgItems.FirstDisplayedScrollingRowIndex = iRowIndex;
								dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
								dgItems.CurrentCell = dgItems.Rows[iRowIndex].Cells[col_ProductName.Name];
							}
						}
						else
						{
							NewProductCreatmethod();
						}
					}
					else
					{
						NewProductCreatmethod();
					}
				}
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				MessageBox.Show(ex.Message);
			}
			FocusMethod();
		}

		private void SaveMetod(int iPcatId, decimal addADisc, decimal dGrandTotal, decimal dBalance, int iDepotId, string GdnName, int Rtn = 0)
		{
			try
			{
				int num = 0;
				salesDetails = POSHelper.SalesDetailsMethod(entPOSDetails, iPartyAccountID, CommonSettings.strDefaultSalesType);
				if (!iMasterId.HasValue)
				{
					if (previlegeDtlPOS != null && !previlegeDtlPOS.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					num = nudInvoiceNo.Value.ToInt32();
					SalesMasterMethod(num, iPcatId, addADisc, dGrandTotal, dBalance);
					DataSet dataSet = new DataSet();
					DataTable dataTable = new DataTable();
					dataTable = AniHelper.ListToDataTable(salesDetails);
					dataTable.TableName = "type";
					dataSet.Tables.Add(dataTable);
					iMasterId = AshproCommon.InsertMethod_SP(salesMaster, dataSet, "spSalesInvoice", new
					{
						TransactionType = "insert",
						GodownId = iDepotId,
						GodownName = GdnName,
						IsReturn = Rtn
					});
					num = db.SalesMasters(new
					{
						SalesInvId = iMasterId
					}).FirstOrDefault().SalesInvNo.toInt32();
					POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.toInt32(), num, strVoucherType, CommonSettings.strDefaultSalesType, iPartyAccountID, "POS Entry", iUserId, iDepotId);
					salesMaster.SalesInvNo = num;
					salesMaster.CntrInvNo += num;
					return;
				}
				if (Utility.uManufacturer != "Ashpro" && !CommonSettings.blnEnableToEditInvoice)
				{
					Messages.WarningMessage("You can't edit the invoice!");
					isEditing = true;
					return;
				}
				if (previlegeDtlPOS != null && !previlegeDtlPOS.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to edit the record. Please Contact the Admin.");
					return;
				}
				foreach (SalesDetails salesDetail in salesDetails)
				{
					salesDetail.SalesInvId = iMasterId.toInt32();
				}
				int? num2 = iMasterId;
				num = AshproCommon.IdFindMethod("Select SalesInvNo From SalesEstimateMaster Where SalesInvId='" + num2 + "'", isVoucherNo: false);
				SalesMasterMethod(num, iPcatId, addADisc, dGrandTotal, dBalance);
				DataSet dataSet2 = new DataSet();
				DataTable dataTable2 = new DataTable();
				dataTable2 = AniHelper.ListToDataTable(salesDetails);
				dataTable2.TableName = "type";
				dataSet2.Tables.Add(dataTable2);
				iMasterId = AshproCommon.InsertMethod_SP(salesMaster, dataSet2, "spSalesInvoice", new
				{
					TransactionType = "update",
					GodownId = iDepotId,
					GodownName = GdnName,
					IsReturn = Rtn
				});
				num = db.SalesMasters(new
				{
					SalesInvId = iMasterId
				}).FirstOrDefault().SalesInvNo.toInt32();
				POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.toInt32(), num, strVoucherType, CommonSettings.strDefaultSalesType, iPartyAccountID, "POS Entry", iUserId, iDepotId);
				salesMaster.SalesInvNo = num;
				salesMaster.CntrInvNo += num;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ScaleWithNoRateMethod(ProductEntity product, ref decimal? dmlRate, ref decimal? dmlQty, ref int? iUnitId)
		{
			if (!product.IsScale.toBool())
			{
				return;
			}
			if (!product.isRateBasedWeight.toBool())
			{
				string text = ((txtBarcode.Text == string.Empty) ? product.Barcode : txtBarcode.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", text);
				decimal? num = dmlRate;
				if ((num.GetValueOrDefault() == default(decimal)) & num.HasValue)
				{
					dmlRate = product.SellingPrice;
				}
				num = dmlRate;
				if ((num.GetValueOrDefault() == default(decimal)) & num.HasValue)
				{
					CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
					WeighScaleView weighScaleView = new WeighScaleView(isRateIncluded: true);
					weighScaleView.Rate = dmlRate.ToDecimal();
					if (weighScaleView.ShowDialog() == DialogResult.OK)
					{
						dmlQty = weighScaleView.txtInput.Text.ToDecimal();
						dmlRate = weighScaleView.txtRate.Value;
						product.isInclusive = weighScaleView.cbInclusive.Checked;
					}
				}
				else
				{
					CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
					WeighScaleView weighScaleView2 = new WeighScaleView();
					weighScaleView2.Rate = dmlRate.ToDecimal();
					if (weighScaleView2.ShowDialog() == DialogResult.OK)
					{
						dmlQty = weighScaleView2.txtInput.Text.ToDecimal();
					}
				}
				return;
			}
			POSInputBox pOSInputBox = new POSInputBox();
			pOSInputBox.isScaleWithNoRate = true;
			pOSInputBox.isNameVisible = true;
			if (pOSInputBox.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (pOSInputBox.txtInput.Value > 0m && pOSInputBox.txtName.Text.ToDecimal() > 0m)
			{
				dmlRate = pOSInputBox.txtInput.Value;
				dmlQty = pOSInputBox.txtName.Text.ToDecimal();
			}
			else if (pOSInputBox.txtInput.Value > 0m)
			{
				string text2 = ((txtBarcode.Text == string.Empty) ? product.Barcode : txtBarcode.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text2 + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", text2);
				decimal? num2;
				if (!(pOSInputBox.txtInput.Value == 0m))
				{
					decimal value = pOSInputBox.txtInput.Value;
					decimal? num = dmlRate;
					num2 = (decimal?)value / num;
				}
				else
				{
					num2 = default(decimal);
				}
				dmlQty = num2;
			}
			else
			{
				dmlQty = pOSInputBox.txtName.Text.ToDecimal();
			}
		}

		private void FocusMethod()
		{
			if (CommonSettings.blnPosBarcodeBased)
			{
				base.ActiveControl = txtBarcode;
				return;
			}
			base.ActiveControl = txtsearchitemname;
			txtsearchitemname.Focus();
			txtsearchitemname.SelectAll();
		}

		private async void NewProductCreatmethod()
		{
			if (new CustomeMessageBox(isMsg: true)
			{
				lblMessage = 
				{
					Text = "There is no product in this Barcode. Are you want to create new product?"
				}
			}.ShowDialog() == DialogResult.OK)
			{
				List<ProductPriceCategory> entProPrices = new List<ProductPriceCategory>();
				new List<ProductUnit>();
				new List<ProductBarcodeEntity>();
				_ = string.Empty;
				POSInputBox frm = new POSInputBox
				{
					isNotInput = true
				};
				if (frm.ShowDialog() != DialogResult.OK || frm.txtName.Text == string.Empty)
				{
					return;
				}
				decimal dRate = frm.txtInput.Value;
				string sName2 = frm.txtName.Text;
				bool isInclusive = frm.cbInclusive.Checked;
				int iTCategoryId = frm.cmbTCategory.SelectedValue.ToInt32();
				if (!db.ProductGroups().Any((ProductGroupEntity x) => x.PGroupName.ToLower() == "star"))
				{
					ProductGroupEntity Group = new ProductGroupEntity
					{
						PGroupId = null,
						PGroupName = "STAR",
						Under = "GENERAL",
						ListOrder = 1
					};
					AshproCommon.InsertToDatabaseObj(Group, "ProductGroup");
				}
				ProductGroupEntity productGroup = db.ProductGroups(new
				{
					PGroupName = "STAR"
				}).FirstOrDefault();
				sName2 = ((sName2 == string.Empty) ? ("STAR " + txtBarcode.Text) : sName2);
				int iNo = AshproCommon.GetIntData_SP("spGetProductLastProductCode");
				ProductEntity newProduct = new ProductEntity
				{
					ProductId = null,
					ProductName = sName2,
					AliasName = sName2,
					PGroupId = productGroup.PGroupId,
					ListOrder = 1,
					ProductCode = iNo,
					AliasCode = iNo.ToString(),
					Barcode = txtBarcode.Text,
					UnitId = db.Units().FirstOrDefault().UnitId.ToInt32(),
					BrandId = db.Brands().FirstOrDefault().BrandId.ToInt32(),
					TCategoryId = iTCategoryId,
					GodwnId = cmbwarehouse.SelectedValue.ToInt32(),
					isActive = true,
					IsScale = false,
					IsAllowMultiple = false,
					IsExpairing = false,
					IsCommisionBasedPercentage = false,
					IsMarginBasedPercentage = false,
					MRPMargin = default(decimal),
					MinimumMargin = default(decimal),
					NormalMargin = default(decimal),
					OppStockDate = DateTime.Now,
					ProductType = "Finished Good",
					SellingPrice = dRate,
					MRP = dRate,
					isInclusive = isInclusive
				};
				if (!AshproCommon.InsertToDatabaseObj(newProduct, "Product"))
				{
					return;
				}
				new ProductEntity();
				ProductEntity product = db.Products(new
				{
					ProductName = sName2
				}).FirstOrDefault();
				foreach (PriceCategoryEntity item in db.PriceCategories())
				{
					ProductPriceCategory pPrice = new ProductPriceCategory
					{
						PCategoryId = item.PCategoryId,
						ProductId = product.ProductId,
						SPrice = product.SellingPrice,
						PPrice = 0m,
						UnitId = product.UnitId,
						PGroupId = productGroup.PGroupId,
						Barcode = product.Barcode
					};
					entProPrices.Add(pPrice);
				}
				AshproCommon.InsertToDatabase(entProPrices.Cast<object>().ToList());
				ProductUnit pUnit = new ProductUnit
				{
					ProductId = product.ProductId,
					UnitId = product.UnitId,
					UnitQty = 1m,
					Barcode = txtBarcode.Text,
					IsInclusive = false,
					OpeningStock = 0m,
					ProductUnitName = product.ProductName,
					SalesPrice = product.SellingPrice,
					UnitType = "Base Unit"
				};
				AshproCommon.InsertToDatabaseObj(pUnit);
				ProductBarcodeEntity proBarcode = new ProductBarcodeEntity
				{
					FK_BranchId = Settings.Default.iBranchId,
					FK_ProductId = product.ProductId.toInt32(),
					FK_UnitId = product.UnitId.toInt32(),
					FK_BatchId = null,
					Barcode = txtBarcode.Text,
					BarcodeType = "Base Barcode",
					PurchasePrice = 0m,
					SellingPrice = product.SellingPrice,
					MRP = product.SellingPrice,
					MinimuPrice = product.SellingPrice,
					RetailPrice = product.SellingPrice,
					WholesalePrice = product.SellingPrice,
					ProductCost = product.SellingPrice
				};
				AshproCommon.InsertToDatabaseObj(proBarcode, "ProductBarcode");
				int iRowIndex = POSHelper.GetPOSDetails(product, entPOSDetails, cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", null, txtBarcode.Text);
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == txtBarcode.Text).FirstOrDefault();
				BindDgv();
				TotalFindMethod();
				await LoadProductMethod();
				btnstck.Text = "0";
				dgItems.ClearSelection();
				dgItems.FirstDisplayedScrollingRowIndex = iRowIndex;
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgItems.CurrentCell = dgItems.Rows[iRowIndex].Cells[col_ProductName.Name];
			}
			else
			{
				txtBarcode.SelectAll();
			}
		}

		private void DgvLoadMethod(List<POSDetails> pOSDetails)
		{
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			TotalFindMethod();
		}

		private void ColorChangeMethod()
		{
			dashboardPOSKeyboard1.btnone.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnone.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnto.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnto.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btntree.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btntree.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnfor.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnfor.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnfive.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnfive.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnsix.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnsix.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnsvn.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnsvn.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnegt.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnegt.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnne.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnne.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnse.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btnse.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btndot.BackColor = Settings.Default.NumberBB;
			dashboardPOSKeyboard1.btndot.ForeColor = Settings.Default.NumberBF;
			dashboardPOSKeyboard1.btnce.BackColor = Settings.Default.GeneralBB;
			dashboardPOSKeyboard1.btnce.ForeColor = Settings.Default.GeneralBF;
			dashboardPOSKeyboard1.btnmu.BackColor = Settings.Default.GeneralBB;
			dashboardPOSKeyboard1.btnmu.ForeColor = Settings.Default.GeneralBF;
			dashboardPOSKeyboard1.btnsub.BackColor = Settings.Default.GeneralBB;
			dashboardPOSKeyboard1.btnsub.ForeColor = Settings.Default.GeneralBF;
			dashboardPOSKeyboard1.btnpls.BackColor = Settings.Default.GeneralBB;
			dashboardPOSKeyboard1.btnpls.ForeColor = Settings.Default.GeneralBF;
			dashboardPOSKeyboard1.btnequel.BackColor = Settings.Default.GeneralBB;
			dashboardPOSKeyboard1.btnequel.ForeColor = Settings.Default.GeneralBF;
			btnbarcode.BackColor = Settings.Default.GeneralBB;
			btnbarcode.ForeColor = Settings.Default.GeneralBF;
			btnpush.BackColor = Settings.Default.GeneralBB;
			btnpush.ForeColor = Settings.Default.GeneralBF;
			btnFirst.BackColor = Settings.Default.GeneralBB;
			btnFirst.ForeColor = Settings.Default.GeneralBF;
			btnBack.BackColor = Settings.Default.GeneralBB;
			btnBack.ForeColor = Settings.Default.GeneralBF;
			btnNext.BackColor = Settings.Default.GeneralBB;
			btnNext.ForeColor = Settings.Default.GeneralBF;
			btnLast.BackColor = Settings.Default.GeneralBB;
			btnLast.ForeColor = Settings.Default.GeneralBF;
			btnTop.BackColor = Settings.Default.GeneralBB;
			btnTop.ForeColor = Settings.Default.GeneralBF;
			btnDwn.BackColor = Settings.Default.GeneralBB;
			btnDwn.ForeColor = Settings.Default.GeneralBF;
			foreach (Control control6 in flpTopMenu.Controls)
			{
				if (control6 is Button)
				{
					Button button = (Button)control6;
					button.BackColor = Settings.Default.TopIBB;
					button.ForeColor = Settings.Default.TopIBF;
				}
			}
			flpTopMenu.BackColor = Settings.Default.TopPB;
			pnlSidebar.BackColor = Settings.Default.SidePB;
			foreach (Control control7 in pnlSidebar.Controls)
			{
				if (control7 is Button)
				{
					Button button2 = (Button)control7;
					button2.BackColor = Settings.Default.SideIBB;
					button2.ForeColor = Settings.Default.SideIBF;
				}
			}
			Color color2 = (pnlNumberPad.BackColor = (dashboardPOSKeyboard1.BackColor = Settings.Default.NumberpadPB));
			color2 = (pnlGroup.BackColor = (pnlGroupBar.BackColor = Settings.Default.CategoryPB));
			foreach (Control control8 in pnlGroup.Controls)
			{
				if (control8 is Button)
				{
					Button button3 = (Button)control8;
					button3.BackColor = Settings.Default.CategoryBB;
					button3.ForeColor = Settings.Default.CategoryBF;
				}
			}
			flpProduct.BackColor = Settings.Default.ProductPB;
			foreach (Control control9 in flpProduct.Controls)
			{
				if (control9 is Button)
				{
					Button button4 = (Button)control9;
					button4.BackColor = Settings.Default.ProductBB;
					button4.ForeColor = Settings.Default.ProductBF;
				}
			}
			pnlDetails.BackColor = Settings.Default.DetailPB;
			foreach (Control control10 in pnlDetails.Controls)
			{
				if (control10 is Label)
				{
					Label label = (Label)control10;
					label.ForeColor = Settings.Default.DetailPF;
				}
			}
			dgItems.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.DgvheaderB;
			dgItems.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.DgvheaderF;
			dgItems.BackgroundColor = Settings.Default.DgvB;
			AshNumericLabel ashNumericLabel = lblLastBill;
			Label label2 = lLastBill;
			AshNumericLabel ashNumericLabel2 = lblChanges;
			Color color4 = (lChanges.ForeColor = Color.Red);
			Color color6 = (ashNumericLabel2.ForeColor = color4);
			color2 = (ashNumericLabel.ForeColor = (label2.ForeColor = color6));
		}

		private async Task LoadProductMethod()
		{
			try
			{
				pnlGroup.Controls.Clear();
				bool isFirst = true;
				int iPGrpId = 0;
				new List<ProductGroupEntity>();
				List<ProductGroupEntity> productGroups = (from x in db.ProductGroups(new
					{
						isShowInGrid = true
					})
					orderby x.ListOrder descending
					select x).ToList();
				foreach (ProductGroupEntity x2 in productGroups)
				{
					if (isFirst)
					{
						iPGrpId = x2.PGroupId.toInt32();
						isFirst = false;
					}
					btnGroup = new Button();
					btnGroup.Name = x2.PGroupName;
					btnGroup.Text = x2.PGroupName;
					btnGroup.Tag = x2.PGroupId;
					btnGroup.BackColor = Settings.Default.CategoryBB;
					btnGroup.FlatStyle = FlatStyle.Flat;
					btnGroup.ForeColor = Settings.Default.CategoryBF;
					btnGroup.Size = new Size(Utility.gbValues.Name.ToInt32(), Utility.gbValues.Description.ToInt32());
					btnGroup.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
					btnGroup.Click += BtnGroup_Click;
					pnlGroup.Controls.Add(btnGroup);
				}
				await ProductLoadMethod(iPGrpId);
				lblCount.Text = sPagedetails;
				iPageNumber = 1;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgItemRateColumnEditMethod(decimal dRate, bool isInclusive = false)
		{
			if (entPosDetail == null)
			{
				entPosDetail = entPOSDetails.FirstOrDefault();
			}
			entPosDetail.Rate = dRate;
			entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
			entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
			entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
			if (!isInclusive)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else
			{
				entPosDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPosDetail.Taxable, entPosDetail.Tax);
				entPosDetail.NetAmount = entPosDetail.Taxable;
				entPosDetail.Taxable = entPosDetail.NetAmount - entPosDetail.TaxAmount;
			}
		}

		private void dgItemQtyColumnEditMethod(bool isInclusive = false)
		{
			entPosDetail.Gross = entPosDetail.Rate * entPosDetail.Qty;
			entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
			entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
			if (!isInclusive)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else
			{
				entPosDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPosDetail.Taxable, entPosDetail.Tax);
				entPosDetail.NetAmount = entPosDetail.Taxable;
				entPosDetail.Taxable = entPosDetail.NetAmount - entPosDetail.TaxAmount;
			}
		}

		private async Task ProductLoadMethod(int iPGrpId)
		{
			try
			{
				flpProduct.Controls.Clear();
				List<Button> controls = await GetProductControls(iPGrpId);
				Control.ControlCollection controls2 = flpProduct.Controls;
				Control[] controls3 = controls.ToArray();
				controls2.AddRange(controls3);
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async Task<List<Button>> GetProductControls(int iPGrpId)
		{
			productPOs = new List<ExProductPOS>();
			productPOs = await db.POSProducts(new
			{
				PGroupId = iPGrpId
			});
			int itemCount = productPOs.Count;
			decimal dcmNoofPages = (decimal)itemCount / (decimal)Settings.Default.iProductsPerPage;
			decimal dcrounded = dcmNoofPages - Math.Round(dcmNoofPages, 0);
			if (dcrounded != 0m)
			{
				dcmNoofPages += 1m;
			}
			iNoofPages = (int)dcmNoofPages;
			sPagedetails = "1/" + iNoofPages;
			iPageNumber = 1;
			return LoadPages(1);
		}

		private List<Button> LoadPages(int iPageNumber)
		{
			List<Button> list = new List<Button>();
			List<ExProductPOS> list2 = new List<ExProductPOS>();
			list2 = productPOs.Skip((iPageNumber - 1) * Settings.Default.iProductsPerPage).Take(Settings.Default.iProductsPerPage).ToList();
			foreach (ExProductPOS item in list2)
			{
				ProButtonAddMethod(list, item);
			}
			return list;
		}

		private void ProButtonAddMethod(List<Button> controls, ExProductPOS x)
		{
			btnProduct = new Button();
			if (CommonSettings.blnShowPriceOnProduct)
			{
				CurrencyInfo currencyInfo = db.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency
				}).FirstOrDefault();
				string text = " ";
				if (currencyInfo != null)
				{
					text = currencyInfo.CurrencyCode + ". ";
				}
				btnProduct.Text = x.ProductName + "\n" + text + x.Rate.ToString(CommonSettings.strCurencyFormat);
			}
			else
			{
				btnProduct.Text = x.ProductName;
			}
			btnProduct.Name = x.ProductId.ToString();
			btnProduct.BackColor = Settings.Default.ProductBB;
			btnProduct.ForeColor = Settings.Default.ProductBF;
			btnProduct.FlatStyle = FlatStyle.Flat;
			btnProduct.FlatAppearance.BorderSize = 0;
			if (x.ProductName.IsArabic())
			{
				btnProduct.Font = new Font("MS Reference Sans Serif", 12f, FontStyle.Bold);
			}
			else
			{
				btnProduct.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
			}
			btnProduct.Size = new Size(Utility.pbValues.Name.ToInt32(), Utility.pbValues.Description.ToInt32());
			if (x.image != null)
			{
				btnProduct.BackgroundImage = AniHelper.byteArrayToImage(x.image);
				btnProduct.BackgroundImageLayout = ImageLayout.Zoom;
				btnProduct.TextAlign = ContentAlignment.BottomCenter;
			}
			btnProduct.Click += BtnProduct_Click;
			controls.Add(btnProduct);
		}

		private void DgItems_GotFocus(object sender, EventArgs e)
		{
			focusedControl = (DataGridView)sender;
			focusedControl.Focus();
		}

		private void BtnLookup_GotFocus(object sender, EventArgs e)
		{
			FocusMethod();
		}

		private void TxtBarcode_GotFocus(object sender, EventArgs e)
		{
			focusedControl = (TextBox)sender;
			focusedControl.Focus();
		}

		private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			CellContentMethod();
		}

		private void dgItems_CellContentClick(object sender, MouseEventArgs e)
		{
			CellContentMethod();
		}

		private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += textbox_KeyPress;
				}
			}
		}

		private void dgItems_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F6)
			{
				entPosDetail = new POSDetails();
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				ProductTracing productTracing = new ProductTracing();
				productTracing.TypeVoucher = "Sales";
				productTracing.PartyAccntId = iPartyAccountID;
				productTracing.ProductId = entPosDetail.productId;
				productTracing.ShowDialog();
			}
		}

		private void dgItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
			if (dgItems.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				dgItemQtyColumnEditMethod(isInclusive);
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Rate.Name)
			{
				dgItemRateColumnEditMethod(entPosDetail.Rate, isInclusive);
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Discount.Name)
			{
				entPosDetail.DisAmount = entPosDetail.Gross * (entPosDetail.Discount / 100m);
				entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_DisAmount.Name)
			{
				if (entPosDetail.DisAmount > 0m)
				{
					entPosDetail.Discount = entPosDetail.DisAmount / entPosDetail.Gross * 100m;
					entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
					entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
					entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
				}
			}
			else if (dgItems.CurrentCell.OwningColumn.Name == col_Tax.Name)
			{
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
			}
			TotalFindMethod();
			dgItems.Refresh();
		}

		private void textbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((dgItems.CurrentCell.OwningColumn.Name == col_Qty.Name || dgItems.CurrentCell.OwningColumn.Name == col_Rate.Name) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || (sender as TextBox).Text.Contains('.')) && (e.KeyChar != '.' || (sender as TextBox).SelectionLength != (sender as TextBox).TextLength) && (e.KeyChar != '-' || (sender as TextBox).Text.Contains('-')) && (e.KeyChar != '-' || (sender as TextBox).SelectionLength != (sender as TextBox).TextLength))
			{
				e.Handled = true;
			}
		}

		private void txtsearchitemname_Validated(object sender, EventArgs e)
		{
			BarcodeMethodWithName();
		}

		private void txtAddDiscountAmnt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				FocusMethod();
			}
		}

		private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void btnTax_Click(object sender, EventArgs e)
		{
			if (dgItems.CurrentRow != null)
			{
				dgItems.Focus();
				dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Tax.Name];
				dgvCell = dgItems.CurrentCell;
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgItems.CurrentRow.Index == dgItems.Rows.Count - 1 && dgItems.CurrentRow.Index != -1)
				{
					dgItems.CurrentCell = dgItems.Rows[0].Cells[col_ProductName.Name];
				}
				else
				{
					dgItems.CurrentCell = dgItems.Rows[dgItems.CurrentRow.Index + 1].Cells[col_ProductName.Name];
				}
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				CellContentMethod();
				FocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (dgItems.CurrentRow.Index != -1)
			{
				if (dgItems.CurrentRow.Index != 0)
				{
					dgItems.CurrentCell = dgItems.Rows[dgItems.CurrentRow.Index - 1].Cells[col_ProductName.Name];
				}
				else
				{
					dgItems.CurrentCell = dgItems.Rows[dgItems.Rows.Count - 1].Cells[col_ProductName.Name];
				}
			}
			else
			{
				dgItems.CurrentCell = dgItems.Rows[0].Cells[col_ProductName.Name];
			}
			dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			CellContentMethod();
			FocusMethod();
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			if (!iMasterId.HasValue)
			{
				isReturnAllowed = true;
			}
			else
			{
				if (!Messages.DialogMessage("Are you want mark the invoice as Sales Return ?"))
				{
					return;
				}
				rtnInvNumber = salesMaster.SalesInvNo;
				iMasterId = null;
				if (entPOSDetails.Count <= 0)
				{
					return;
				}
				foreach (POSDetails entPOSDetail in entPOSDetails)
				{
					entPOSDetail.Qty *= -1m;
					entPOSDetail.Gross *= -1m;
					entPOSDetail.DisAmount *= -1m;
					entPOSDetail.Taxable *= -1m;
					entPOSDetail.TaxAmount *= -1m;
					entPOSDetail.NetAmount *= -1m;
				}
				TotalFindMethod();
				dgItems.Refresh();
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
			string sBarcode = AniHelper.InputBoxMethod(MessageKeys.POSBarcodeSelection, MessageKeys.POSBarcodeSelectionCaption, MessageKeys.POSBarcodeSelectionLabel);
			if (sBarcode != string.Empty)
			{
				entPosDetail = entPOSDetails.FirstOrDefault((POSDetails x) => x.Barcode == sBarcode);
				if (entPosDetail != null)
				{
					int index = entPOSDetails.IndexOf(entPosDetail);
					dgItems.CurrentCell = dgItems.Rows[index].Cells[col_ProductName.Name];
					dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					dgItems.Rows[index].Selected = true;
					CellContentMethod();
				}
				FocusMethod();
			}
		}

		private void tmrGrandTotal_Tick(object sender, EventArgs e)
		{
			if (lblGrandTotal.ForeColor == Color.Green)
			{
				lblGrandTotal.ForeColor = Color.Red;
			}
			else
			{
				lblGrandTotal.ForeColor = Color.Green;
			}
			iTmr++;
			if (iTmr > 10)
			{
				iTmr = 0;
				tmrGrandTotal.Enabled = false;
			}
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				salesMaster = db.SalesMasters(new
				{
					InvType = CommonSettings.strDefaultSalesType,
					SalesInvNo = nudInvoiceNo.Value.ToInt32(),
					FYearID = iFYearId,
					FK_BranchId = Settings.Default.iBranchId
				}).FirstOrDefault();
				if (salesMaster != null)
				{
					iMasterId = salesMaster.SalesInvId.ToInt32();
					int? num = iMasterId;
					if (!((num.GetValueOrDefault() == 0) & num.HasValue))
					{
						salesDetails = db.SalesDetails(new
						{
							SalesInvId = iMasterId
						}).ToList();
						LoadMethod(salesMaster, salesDetails);
					}
					else
					{
						btnClearMethod();
					}
				}
				else
				{
					btnClearMethod();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				BarcodeAddmethod();
			}
		}

		private void btnPlus_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count != 0)
			{
				if (entPOSDetails.Count > 0 || entPosDetail == null || entPosDetail.productId == 0)
				{
					entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				}
				if (entPosDetail.Qty == -1m)
				{
					entPosDetail.Qty = 1m;
				}
				else
				{
					entPosDetail.Qty += 1m;
				}
				bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
				dgItemQtyColumnEditMethod(isInclusive);
				TotalFindMethod();
				dgItems.Refresh();
			}
		}

		private void btnMinus_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				return;
			}
			if (entPOSDetails.Count > 0 && entPosDetail == null)
			{
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			}
			if (isReturnAllowed || !(entPosDetail.Qty == 1m))
			{
				if (entPosDetail.Qty == 1m)
				{
					entPosDetail.Qty = -1m;
				}
				else
				{
					entPosDetail.Qty -= 1m;
				}
				bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
				dgItemQtyColumnEditMethod(isInclusive);
				TotalFindMethod();
				dgItems.Refresh();
			}
		}

		private void btnRemoveRow_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count != 0)
			{
				if (salesMaster != null && salesMaster.IsClosed && iMasterId.HasValue)
				{
					Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
				}
				else
				{
					RowDeleteMethod();
				}
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = CommonSettings.strDefaultSalesType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
			btnClearMethod();
		}

		private void btnTouch_Click(object sender, EventArgs e)
		{
			POSProductView pOSProductView = new POSProductView(this, _isPosDashboard: true);
			pOSProductView.entPOSDetails = entPOSDetails;
			pOSProductView.iPCategoryID = cmbwhole.SelectedValue.ToInt32();
			if (pOSProductView.ShowDialog() == DialogResult.OK)
			{
				txtBarcode.Text = string.Empty;
				FocusMethod();
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					Messages.ErrorMessage("No records to Print!");
					return;
				}
				if (CommonSettings.blnInvoicePrintingByRDLC)
				{
					List<InvoiceAdjustment> list = new List<InvoiceAdjustment>();
					Shop shop = new Shop();
					if (company != null)
					{
						shop = company.CopyTo<Shop>();
						shop.ShopName = company.InstituteName;
						shop.TaxNumber = company.GSTNo;
					}
					PrinterSettings printerSettings = new PrinterSettings();
					DataTable dataTable = new DataTable();
					dataTable = AniHelper.ToDataTable(shop);
					DataSet dataSet = new DataSet();
					dataTable.TableName = "dtCompany";
					dataSet.Tables.Add(dataTable);
					int? invoiceNo = null;
					if (iMasterId.HasValue)
					{
						invoiceNo = nudInvoiceNo.Value.ToInt32();
					}
					else
					{
						SalesMasterMethod(invoiceNo, cmbwhole.SelectedIndex.ToInt32(), txtAddDiscountAmnt.Value, lblGrandTotal.Value, lblBalance.Value);
					}
					POSMaster pOSMaster = salesMaster.CopyTo<POSMaster>();
					pOSMaster.GrandTotal = salesMaster.GrandDiscount;
					pOSMaster.SalesAccount = db.Accounts(new
					{
						AccountId = salesMaster.SalesAccountId
					}).FirstOrDefault().AccountName;
					pOSMaster.PartyAccount = db.Accounts(new
					{
						AccountId = salesMaster.PartyAccountId
					}).FirstOrDefault().AccountName;
					pOSMaster.Financialyear = db.FinancialYears(new
					{
						FyearId = salesMaster.FYearID
					}).FirstOrDefault().FyearName;
					pOSMaster.BranchName = db.Branches(new
					{
						Id = salesMaster.FK_BranchId
					}).FirstOrDefault().Name;
					pOSMaster.SalesMan = db.EmployeeDetails(new
					{
						EmpID = salesMaster.StafId
					}).FirstOrDefault().EmpName;
					pOSMaster.JEName = db.EmployeeDetails(new
					{
						EmpID = salesMaster.JEId
					}).FirstOrDefault().EmpName;
					DataTable dataTable2 = new DataTable();
					dataTable2 = AniHelper.ToDataTable(pOSMaster);
					dataTable2.TableName = "dtPOSMaster";
					dataSet.Tables.Add(dataTable2);
					DataTable dataTable3 = new DataTable();
					dataTable3 = AniHelper.ListToDataTable(entPOSDetails);
					dataTable3.TableName = "dtPOSDetails";
					dataSet.Tables.Add(dataTable3);
					DataTable dataTable4 = new DataTable();
					decimal? cashPayment = pOSMaster.CashPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(list, "Cash Payment", pOSMaster.CashPayment.ToString(CommonSettings.strCurencyFormat), "دفع نقدا");
					}
					cashPayment = pOSMaster.CardPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(list, "Card Payment", pOSMaster.CardPayment.ToString(CommonSettings.strCurencyFormat), "بطاقه ائتمان");
					}
					cashPayment = pOSMaster.OtherPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(list, "Online Payment", pOSMaster.OtherPayment.ToString(CommonSettings.strCurencyFormat), "مدفوعات أخرى");
					}
					if (pOSMaster.BillDiscount > 0m)
					{
						AniHelper.AddPaymentPrint(list, "Discount", pOSMaster.BillDiscount.ToString(CommonSettings.strCurencyFormat), "خصم");
					}
					AniHelper.AddPaymentPrint(list, "Tax (%)", "Tax Amount", "Total");
					foreach (POSDetails item in entPOSDetails)
					{
						string nm = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item.Tax).TCategoryName;
						if (!list.Any((InvoiceAdjustment x) => x.Name == nm))
						{
							decimal num = entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.TaxAmount);
							AniHelper.AddPaymentPrint(sRLabel: entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.NetAmount).ToString(CommonSettings.strCurencyFormat), paymentPrint: list, sName: nm, dValue: num.ToString(CommonSettings.strCurencyFormat));
						}
					}
					dataTable4 = AniHelper.ListToDataTable(list);
					dataTable4.TableName = "dtPayment";
					dataSet.Tables.Add(dataTable4);
					decimal num2 = entPOSDetails.Sum((POSDetails x) => x.Qty);
					string sReportPath = Application.StartupPath + "\\ReportForms\\rptPOSPrintWithInvoiceNo.rdlc";
					List<atReportParameter> list2 = new List<atReportParameter>();
					list2.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
					CultureInfo cultureInfo = new CultureInfo("en-US");
					string parameterValue = pOSMaster.SalesDate.ToString(cultureInfo);
					list2.Add(new atReportParameter("QuantityParameter", CommonSettings.strQuantityFormat));
					list2.Add(new atReportParameter("InvoiceDate", parameterValue));
					list2.Add(new atReportParameter("Tendered", dTendered.ToString(CommonSettings.strCurencyFormat)));
					list2.Add(new atReportParameter("Changes", dChanges.ToString(CommonSettings.strCurencyFormat)));
					list2.Add(new atReportParameter("TotalQuantity", num2.ToString(CommonSettings.strQuantityFormat)));
					string text = Settings.Default.RDLCPrinter;
					if (text == string.Empty)
					{
						text = printerSettings.PrinterName;
					}
					printerSettings.PrinterName = text;
					DirectReportPrint directReportPrint = new DirectReportPrint();
					directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list2, generatePdf: false, txtPrintCopy.Data);
				}
				else
				{
					entDMaster = db.DotmatrixMasters(new
					{
						VochrType = "Sales Invoice",
						FormType = CommonSettings.strDefaultSalesType,
						isDefault = true
					}).FirstOrDefault();
					PrintMethod(entDMaster, txtPrintCopy.Data);
				}
				FocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if ((iMasterId.HasValue && Utility.uManufacturer == "Ashpro") || (iMasterId.HasValue && CommonSettings.blnEnableToDeleteInvoice))
			{
				if (!salesMaster.IsClosed)
				{
					try
					{
						if (Messages.DeleteConfirmationMessage())
						{
							if (previlegeDtlPOS != null && !previlegeDtlPOS.DeleteData)
							{
								Messages.ErrorMessage("You have no authorisation to delete the record. Please Contact the Admin.");
								return;
							}
							DeleteSubMethod();
							AshproCommon.DeleteMethod("Delete From SalesEstimateMaster Where SalesInvId ='" + iMasterId.ToInt32() + "'");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
				else
				{
					Messages.ErrorMessage("The Invoice is closed. So you can't delete the same.");
				}
			}
			if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
			{
				AniHelper.Display(company.InstituteName, "");
			}
			btnNew_Click(null, null);
		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			if (CommonSettings.blnEnableCutomerDisplay)
			{
				AniHelper.Display("Total : " + lblGrandTotal.Value.ToString(CommonSettings.strCurencyFormat), "");
			}
			if (CommonSettings.blnShowSecondDisplay)
			{
				SecondDisplay.ShowTotalAmountOnly("Total : " + lblGrandTotal.Value.ToString(CommonSettings.strCurencyFormat));
			}
			if (entPOSDetails.Count == 0)
			{
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			pOSCashPayment.isShowNumberPad = true;
			decimal value = lblGrandTotal.Value;
			isReturn = ((value < 0m) ? true : false);
			pOSCashPayment.isReturn = isReturn;
			pOSCashPayment.lblTitle.Text = (isReturn ? "Refund" : "Payment");
			pOSCashPayment.lblInvoiceAmount.Value = Math.Abs(value);
			pOSCashPayment.txtCashPaid.Value = ((dCashPayment + dCardPayment + dOtherPayment == 0m) ? Math.Abs(value) : dCashPayment);
			pOSCashPayment.dCashPayment = dCashPayment;
			decimal num3 = (pOSCashPayment.txtCardPaid.Value = (pOSCashPayment.dCardPayment = dCardPayment));
			num3 = (pOSCashPayment.txtOtherPaid.Value = (pOSCashPayment.dOtherPayment = dOtherPayment));
			pOSCashPayment.lblPaidAmount.Value = dCashPayment + dCardPayment + dOtherPayment;
			pOSCashPayment.iCashAccountId = iCashAcctId;
			pOSCashPayment.iCardAccountId = iCardAccount;
			pOSCashPayment.iOthersAccountId = iOtherAccount;
			pOSCashPayment.iPartyAccountId = lblCustomer.Tag.ToInt32();
			if (pOSCashPayment.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			dCashPayment = pOSCashPayment.dCashPayment;
			dCardPayment = pOSCashPayment.dCardPayment;
			dOtherPayment = pOSCashPayment.dOtherPayment;
			dTendered = dCashPayment + dCardPayment + dOtherPayment;
			decimal num6 = default(decimal);
			decimal num7 = Math.Abs(lblGrandTotal.Value);
			num6 = num7 - dTendered.ToDecimal();
			if (num6 < 0m)
			{
				dCashPayment = num7 - (dCardPayment + dOtherPayment);
				if (iPartyAccountID == iNormalCashAccountId)
				{
					dChanges = Math.Abs(num6);
					lblBalance.Value = 0m;
				}
				else
				{
					dChanges = default(decimal);
					lblBalance.Value = num6;
				}
			}
			else
			{
				lblBalance.Value = num6;
			}
			iCashAcctId = ((dCashPayment > 0m) ? pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32() : 0);
			iCardAccount = ((dCardPayment > 0m) ? pOSCashPayment.cmbBankAccount.SelectedValue.ToInt32() : 0);
			iOtherAccount = ((dOtherPayment > 0m) ? pOSCashPayment.cmbBankOthers.SelectedValue.ToInt32() : 0);
			if (isReturn)
			{
				dCashPayment *= -1m;
				dCardPayment *= -1m;
				dOtherPayment *= -1m;
			}
			btnfastSale.PerformClick();
		}

		private void btnSearchBill_Click(object sender, EventArgs e)
		{
			POSFormSearch pOSFormSearch = new POSFormSearch();
			pOSFormSearch.searchType = "BILL";
			pOSFormSearch.FormType = CommonSettings.strDefaultSalesType;
			if (pOSFormSearch.ShowDialog() == DialogResult.OK)
			{
				salesMaster = pOSFormSearch.SalesMaster;
				ReloadMethod(salesMaster, isSales: true);
			}
			FocusMethod();
		}

		private void btnHoldBill_Click(object sender, EventArgs e)
		{
			try
			{
				if (db.POSHoldMasters().Any((SalesMaster x) => x.SalesInvNo == nudInvoiceNo.Value.ToInt32()))
				{
					SalesMasterMethod(db.POSHoldMasters().Max((SalesMaster x) => x.SalesInvNo + 1), cmbwhole.SelectedValue.ToInt32(), txtAddDiscountAmnt.Value, lblGrandTotal.Value, lblBalance.Value, isPOSHold: true);
				}
				else
				{
					SalesMasterMethod(nudInvoiceNo.Value.ToInt32(), cmbwhole.SelectedValue.ToInt32(), txtAddDiscountAmnt.Value, lblGrandTotal.Value, lblBalance.Value, isPOSHold: true);
				}
				POSHelper.InvoiceHoldMethod(salesMaster, entPOSDetails);
				HoldBillCountMethod();
				btnNew_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnRetrieveBill_Click(object sender, EventArgs e)
		{
			POSFormSearch pOSFormSearch = new POSFormSearch();
			pOSFormSearch.searchType = "Retrieve";
			pOSFormSearch.FormType = CommonSettings.strDefaultSalesType;
			if (pOSFormSearch.ShowDialog() == DialogResult.OK && pOSFormSearch.SalesMaster != null)
			{
				ReloadMethod(pOSFormSearch.SalesMaster);
				DeleteHoldedBill();
			}
			HoldBillCountMethod();
			FocusMethod();
		}

		private void btnCashDrawer_Click(object sender, EventArgs e)
		{
			PrinterSettings printerSettings = new PrinterSettings();
			string text = Settings.Default.CDPrinter;
			if (text == string.Empty)
			{
				text = printerSettings.PrinterName;
			}
			VbPrint vbPrint = new VbPrint(text)
			{
				RTL = false,
				Bold = true
			};
			vbPrint.CutPaper();
			vbPrint.EndDoc();
			FocusMethod();
		}

		private void dashboardPOSKeyboard1_atEqualClick(object source)
		{
			try
			{
				btnPayment_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dashboardPOSKeyboard1_atSubClick(object source)
		{
			try
			{
				if (dgItems.CurrentRow != null)
				{
					if (salesMaster != null && salesMaster.IsClosed)
					{
						Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
						return;
					}
					if (txtqty.Text != string.Empty)
					{
						dgItems.Focus();
						dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Qty.Name];
						dgvCell = dgItems.CurrentCell;
						dgvCell.Value = txtqty.Text;
						txtqty.Text = string.Empty;
						entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
						ProductEntity productEntity = db.Products(new
						{
							ProductId = entPosDetail.productId
						}).FirstOrDefault();
						dgItemQtyColumnEditMethod(productEntity.isInclusive.toBool());
						TotalFindMethod();
						dgItems.Refresh();
						dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Rate.Name];
					}
					else
					{
						btnMinus_Click(null, null);
					}
				}
				FocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dashboardPOSKeyboard1_atStarClick(object source)
		{
			try
			{
				if (salesMaster != null && salesMaster.IsClosed)
				{
					Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
					return;
				}
				txtqty.Enabled = true;
				txtqty.Focus();
				txtqty.Text = string.Empty;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btneditline_Click(object sender, EventArgs e)
		{
			if (dgItems.CurrentRow != null)
			{
				POSDetails pOSDetails = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				POSLineEdit pOSLineEdit = new POSLineEdit();
				pOSLineEdit.pOSDetails = pOSDetails;
				pOSLineEdit.PCategoryId = cmbwhole.SelectedValue.ToInt32();
				if (pOSLineEdit.ShowDialog() == DialogResult.OK)
				{
					dgItemRateColumnEditMethod(pOSLineEdit.pOSDetails.Rate, pOSLineEdit.cbIncludeTax.Checked);
					TotalFindMethod();
					dgItems.Refresh();
				}
			}
			FocusMethod();
		}

		private void btnstck_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgItems.CurrentRow != null)
				{
					POSDetails pOSDetails = (POSDetails)dgItems.CurrentRow.DataBoundItem;
					POSStockView pOSStockView = new POSStockView();
					pOSStockView.dStock = btnstck.Text.ToDecimal();
					pOSStockView.ProductId = pOSDetails.productId;
					pOSStockView.UnitId = pOSDetails.UnitId;
					pOSStockView.PCategoryId = cmbwhole.SelectedValue.ToInt32();
					pOSStockView.ShowDialog();
					FocusMethod();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dashboardPOSKeyboard1_atAddClick(object source)
		{
			try
			{
				if (dgItems.CurrentRow != null)
				{
					if (salesMaster != null && salesMaster.IsClosed)
					{
						Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
						return;
					}
					if (txtqty.Text != string.Empty)
					{
						dgItems.Focus();
						dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Rate.Name];
						dgvCell = dgItems.CurrentCell;
						dgvCell.Value = txtqty.Text;
						entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
						ProductEntity productEntity = db.Products(new
						{
							ProductId = entPosDetail.productId
						}).FirstOrDefault();
						dgItemRateColumnEditMethod(txtqty.Text.ToDecimal(), productEntity.isInclusive.toBool());
						txtqty.Text = string.Empty;
						TotalFindMethod();
						dgItems.Refresh();
						dgItems.CurrentCell = dgItems.CurrentRow.Cells[col_Rate.Name];
					}
					else
					{
						btnPlus_Click(null, null);
					}
				}
				FocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnbarcode_Click(object sender, EventArgs e)
		{
			BarcodeAddmethod();
		}

		private void btnpush_Click(object sender, EventArgs e)
		{
			cmbwhole.Enabled = false;
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					FocusMethod();
					return;
				}
				int? num = iFYearId;
				if ((num.GetValueOrDefault() == 0) & num.HasValue)
				{
					iFYearId = db.SelectedFYears().FirstOrDefault().FyearId.toInt32();
				}
				isReturn = ((lblGrandTotal.Value < 0m) ? true : false);
				int pCatId = cmbwhole.SelectedValue.ToInt32();
				decimal dAddDisc = txtAddDiscountAmnt.Value;
				decimal dGrand = lblGrandTotal.Value;
				decimal dBalance = lblBalance.Value;
				string sDepotName = cmbwarehouse.Text;
				int iDepotId = cmbwarehouse.SelectedValue.ToInt32();
				int Rtn = (isReturn ? 1 : 0);
				await Task.Run(delegate
				{
					SaveMetod(pCatId, dAddDisc, dGrand, dBalance, iDepotId, sDepotName, Rtn);
				});
				if (cbPrint.Checked && !isEditing)
				{
					btnPrint_Click(null, null);
				}
				if (cbPrint2.Checked && !isEditing)
				{
					entDMaster = db.DotmatrixMasters(new
					{
						VochrType = "Sales Invoice",
						FormType = CommonSettings.strDefaultSalesType,
						PrintType = "Invoice"
					}).FirstOrDefault();
					PrintMethod(entDMaster, txtPCopy2.Data);
				}
				decimal gTotal = lblGrandTotal.Value;
				decimal changes = dChanges.ToDecimal();
				btnNew_Click(null, null);
				lblLastBill.Value = gTotal;
				lblChanges.Value = changes;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnsearchcustom_Click(object sender, EventArgs e)
		{
			CustomerSearchPOS customerSearchPOS = new CustomerSearchPOS();
			if (customerSearchPOS.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (customerSearchPOS.isNewCustomer)
			{
				dashboardPOS.tsmCustomer.PerformClick();
				return;
			}
			AccountEntity accountEntity = db.Accounts(new
			{
				AccountId = customerSearchPOS.txtName.Tag.ToInt32()
			}).FirstOrDefault();
			if (accountEntity != null)
			{
				iPartyAccountID = customerSearchPOS.txtName.Tag.ToInt32();
				lblCustomer.Tag = iPartyAccountID;
				lblCustomer.Text = customerSearchPOS.txtName.Text;
			}
			FocusMethod();
		}

		private void txtAddDiscountAmnt_Validated(object sender, EventArgs e)
		{
			lblGrandTotal.Value = lblTotal.Value - txtAddDiscountAmnt.Value;
			lblBalance.Value = lblGrandTotal.Value - (dCashPayment + dCardPayment);
			FocusMethod();
		}

		private void btnpro_Click(object sender, EventArgs e)
		{
			try
			{
				pOSItemCreation = new POSItemCreation();
				if (entPosDetail != null)
				{
					pOSItemCreation.entProduct = db.Products(new
					{
						ProductId = entPosDetail.productId
					}).FirstOrDefault();
				}
				pOSItemCreation.FormClosing += POSItemCreation_FormClosing;
				pOSItemCreation.ShowDialog();
				FocusMethod();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void POSItemCreation_FormClosing(object sender, FormClosingEventArgs e)
		{
			FocusMethod();
		}

		private void btnlastbill_Click(object sender, EventArgs e)
		{
			btnfastSale.PerformClick();
			btnprint.PerformClick();
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = 1m;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			if (!(nudInvoiceNo.Value == 1m))
			{
				nudInvoiceNo.Value--;
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			int num = 1;
			num = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = CommonSettings.strDefaultSalesType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
			if (nudInvoiceNo.Value < (decimal)num)
			{
				nudInvoiceNo.Value++;
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = CommonSettings.strDefaultSalesType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
		}

		private void DashboardPOSView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				if (CommonSettings.blnPosBarcodeBased)
				{
					txtsearchitemname.Focus();
				}
				else
				{
					txtBarcode.Focus();
				}
			}
			else if (e.KeyCode == Keys.F2)
			{
				btnnewsale.PerformClick();
			}
			else if (e.KeyCode == Keys.F3)
			{
				btnRetrieveBill.PerformClick();
			}
			else if (e.KeyCode == Keys.F4)
			{
				btncancelsale.PerformClick();
			}
			else if (e.KeyCode == Keys.F5)
			{
				btnDiscount.PerformClick();
			}
			else if (e.KeyCode == Keys.F6)
			{
				btnprint.PerformClick();
			}
			else if (e.KeyCode == Keys.F7)
			{
				btnprint.PerformClick();
			}
			else if (e.KeyCode == Keys.F8)
			{
				btnpro.PerformClick();
			}
			else if (e.KeyCode == Keys.F9)
			{
				btndltline.PerformClick();
			}
			else if (e.KeyCode == Keys.F10)
			{
				btnReturn.PerformClick();
			}
			else if (e.KeyCode == Keys.F11)
			{
				btneditline.PerformClick();
			}
			else if (e.KeyCode == Keys.F12)
			{
				btnfastSale.PerformClick();
			}
			else if (e.KeyCode == Keys.N && e.Control)
			{
				remarks = AniHelper.InputBoxMethod("Remarks", "Please enter remarks", "Remarks :", isPassword: false, remarks);
			}
			else if (e.KeyCode == Keys.Space)
			{
				if (base.ActiveControl != txtsearchitemname)
				{
					btnPayment_Click(null, null);
				}
			}
			else if (e.KeyCode == Keys.Multiply)
			{
				txtqty.Enabled = true;
				txtqty.Text = string.Empty;
				txtqty.Focus();
			}
			else if (e.KeyCode == Keys.Add)
			{
				dashboardPOSKeyboard1_atAddClick(null);
			}
			else if (e.KeyCode == Keys.Subtract)
			{
				dashboardPOSKeyboard1_atSubClick(null);
			}
			else if (e.KeyCode == Keys.C && e.Control)
			{
				btnsearchcustom.PerformClick();
			}
			else if (e.KeyCode == Keys.H && e.Control)
			{
				btnSearchBill.PerformClick();
			}
			else if (e.KeyCode == Keys.S && e.Control)
			{
				btnsearch.PerformClick();
			}
			else if (e.KeyCode == Keys.D && e.Control)
			{
				btnlindisc.PerformClick();
			}
			else if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Delete)
			{
				if (e.KeyCode == Keys.Up)
				{
					btnup.PerformClick();
				}
				else if (e.KeyCode == Keys.Down)
				{
					btndown.PerformClick();
				}
				else if (e.KeyCode == Keys.Left)
				{
					btnDwn_Click(null, null);
				}
				else if (e.KeyCode == Keys.Right)
				{
					btnTop_Click(null, null);
				}
				else if (e.KeyCode == Keys.K && e.Control && e.Alt)
				{
					CommonSettings.blnShowSecondDisplay = true;
					ShowSecondDisplay();
					FocusMethod();
				}
			}
			else
			{
				FocusMethod();
			}
		}

		private void DashboardPOSView_VisibleChanged(object sender, EventArgs e)
		{
			ColorChangeMethod();
			FocusMethod();
		}

		private void txtPrintCopy_Validated(object sender, EventArgs e)
		{
			if (txtPrintCopy.Data > 0)
			{
				Settings.Default.iPrintCopy = txtPrintCopy.Data;
				Settings.Default.Save();
			}
		}

		private void BtnProduct_Click(object sender, EventArgs e)
		{
			try
			{
				if (salesMaster != null && salesMaster.IsClosed)
				{
					Messages.ErrorMessage("This Invoice is closed. So you can't edit the same.");
					return;
				}
				Button button = (Button)sender;
				ProductEntity productEntity = new ProductEntity();
				productEntity = db.Products(new
				{
					ProductId = button.Name.ToInt32()
				}).ToList().FirstOrDefault();
				if (productEntity != null)
				{
					List<ProductBarcodeEntity> list = new List<ProductBarcodeEntity>();
					list = db.ProductBarcodes(new
					{
						FK_ProductId = productEntity.ProductId,
						BarcodeType = "Multi Size"
					}).ToList();
					if (list.Count > 0)
					{
						ProductSubView frm = new ProductSubView(list);
						if (frm.ShowDialog() == DialogResult.OK)
						{
							int num = 0;
							decimal? dmlQty = null;
							int? iUnitId = null;
							decimal? dmlRate = null;
							ScaleWithNoRateMethod(productEntity, ref dmlRate, ref dmlQty, ref iUnitId);
							ProductBarcodeEntity productBarcode = new ProductBarcodeEntity();
							productBarcode = list.FirstOrDefault((ProductBarcodeEntity x) => x.Id == frm.Id);
							num = POSHelper.GetPOSDetails(productEntity, entPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", dmlQty, productBarcode.Barcode, iUnitId, productBarcode.SellingPrice, productBarcode.ProductBarcodeName);
							entPosDetail = entPOSDetails.FirstOrDefault((POSDetails x) => x.Barcode == productBarcode.Barcode);
							DgvLoadMethod(entPOSDetails);
							ProductStockFindMethod();
							dgItems.ClearSelection();
							dgItems.FirstDisplayedScrollingRowIndex = num;
							dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
							dgItems.CurrentCell = dgItems.Rows[num].Cells[col_ProductName.Name];
						}
					}
					else
					{
						DGVLoadFromProClick(productEntity);
					}
				}
				FocusMethod();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnlindisc_Click(object sender, EventArgs e)
		{
			if (dgItems.CurrentRow == null)
			{
				return;
			}
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			DiscountChangerPOS discountChangerPOS = new DiscountChangerPOS();
			discountChangerPOS.lblProductName.Text = entPosDetail.ProductName;
			discountChangerPOS.lblQuantity.Value = entPosDetail.Qty;
			discountChangerPOS.lblRate.Value = entPosDetail.Rate;
			discountChangerPOS.lblGross.Value = entPosDetail.Gross;
			discountChangerPOS.txtDiscount.Value = entPosDetail.Discount;
			discountChangerPOS.txtDiscAmount.Value = entPosDetail.DisAmount;
			bool flag = db.Products(new
			{
				ProductId = entPosDetail.productId
			}).FirstOrDefault().isInclusive.toBool();
			if (discountChangerPOS.ShowDialog() == DialogResult.OK)
			{
				entPosDetail.Discount = discountChangerPOS.txtDiscount.Value;
				entPosDetail.DisAmount = discountChangerPOS.txtDiscAmount.Value;
				entPosDetail.Taxable = entPosDetail.Gross - entPosDetail.DisAmount;
				entPosDetail.TaxAmount = entPosDetail.Taxable * (entPosDetail.Tax / 100m);
				if (flag)
				{
					entPosDetail.NetAmount = entPosDetail.Taxable;
				}
				else
				{
					entPosDetail.NetAmount = entPosDetail.Taxable + entPosDetail.TaxAmount;
				}
				dgItems.Refresh();
				TotalFindMethod();
			}
		}

		private void btnDiscount_Click(object sender, EventArgs e)
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					return;
				}
				dSubDiscount = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Discount Percentage", "Discount :", isPassword: false, dSubDiscount.ToString()).ToDecimal();
				foreach (POSDetails entPOSDetail in entPOSDetails)
				{
					entPOSDetail.Discount = dSubDiscount;
					entPOSDetail.DisAmount = entPOSDetail.Rate * entPOSDetail.Qty * (entPOSDetail.Discount / 100m);
					entPOSDetail.Taxable = entPOSDetail.Gross - entPOSDetail.DisAmount;
					if (!db.Productunits(new
					{
						ProductId = entPOSDetail.productId,
						UnitId = entPOSDetail.UnitId
					}).FirstOrDefault().IsInclusive)
					{
						entPOSDetail.TaxAmount = entPOSDetail.Taxable * (entPOSDetail.Tax / 100m);
						entPOSDetail.NetAmount = entPOSDetail.Taxable + entPOSDetail.TaxAmount;
					}
					else
					{
						entPOSDetail.TaxAmount = AniHelper.GetInclusiveAmount(entPOSDetail.Taxable, entPOSDetail.Tax);
						entPOSDetail.NetAmount = entPOSDetail.Taxable;
						entPOSDetail.Taxable = entPOSDetail.NetAmount - entPOSDetail.TaxAmount;
					}
				}
				dgItems.Refresh();
				TotalFindMethod();
				FocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		public void DGVLoadFromProClick(ProductEntity product)
		{
			decimal? dmlRate = null;
			int? iUnitId = null;
			decimal? dmlQty = null;
			ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
			int pOSDetails = POSHelper.GetPOSDetails(product, entPOSDetails, cmbwhole.SelectedValue.ToInt32(), "Sales Invoice", dmlQty, product.Barcode, iUnitId, dmlRate);
			entPosDetail = entPOSDetails.FirstOrDefault((POSDetails x) => x.ProductCode == product.ProductCode.ToString());
			DgvLoadMethod(entPOSDetails);
			ProductStockFindMethod();
			dgItems.ClearSelection();
			dgItems.FirstDisplayedScrollingRowIndex = pOSDetails;
			dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgItems.CurrentCell = dgItems.Rows[pOSDetails].Cells[col_ProductName.Name];
		}

		private async void btnDwn_Click(object sender, EventArgs e)
		{
			if (iPageNumber > 1)
			{
				iPageNumber--;
			}
			sPagedetails = iPageNumber + "/" + iNoofPages;
			lblCount.Text = sPagedetails;
			try
			{
				flpProduct.Controls.Clear();
				List<Button> controls = await Task.Run(() => LoadPages(iPageNumber));
				Control.ControlCollection controls2 = flpProduct.Controls;
				Control[] controls3 = controls.ToArray();
				controls2.AddRange(controls3);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void btnTop_Click(object sender, EventArgs e)
		{
			if (iPageNumber < iNoofPages)
			{
				iPageNumber++;
			}
			sPagedetails = iPageNumber + "/" + iNoofPages;
			lblCount.Text = sPagedetails;
			try
			{
				flpProduct.Controls.Clear();
				List<Button> controls = await Task.Run(() => LoadPages(iPageNumber));
				Control.ControlCollection controls2 = flpProduct.Controls;
				Control[] controls3 = controls.ToArray();
				controls2.AddRange(controls3);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void BtnGroup_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			await ProductLoadMethod(btn.Tag.ToInt32());
			lblCount.Text = sPagedetails;
			iIndex = btn.TabIndex;
			base.ActiveControl = txtBarcode;
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.isPrintOn = cbPrint.Checked;
			Settings.Default.Save();
		}

		private async void DashboardPOSView_Load(object sender, EventArgs e)
		{
			try
			{
				ShowSecondDisplay();
				SalesCounterEntity salesCounter = new SalesCounterEntity();
				if (Utility.uIsSalesCounter)
				{
					salesCounter = db.SalesCounter(new
					{
						Remarks = Utility.uTerminal,
						FK_BranchId = Settings.Default.iBranchId
					}).FirstOrDefault();
					if (salesCounter == null)
					{
						Settings.Default.IsCashClosed = true;
						Settings.Default.Save();
					}
				}
				if (Utility.uIsSalesCounter && Settings.Default.IsCashClosed)
				{
					iCashAcctId = CommonSettings.strDefaultCashAccountId.ToInt32();
					CounterOpeningCashEntry frm = new CounterOpeningCashEntry();
					frm.ShowDialog();
					salesCounter = db.SalesCounter(new
					{
						Remarks = Utility.uTerminal,
						FK_BranchId = Settings.Default.iBranchId
					}).FirstOrDefault();
				}
				if (Utility.uIsSalesCounter && salesCounter != null)
				{
					iCashAcctId = (iPartyAccountID = (iNormalCashAccountId = (Utility.uCounterCashAccountId = salesCounter.CashAccountId.ToInt32())));
					CommonSettings.strDefaultCashAccountId = iNormalCashAccountId.ToString();
				}
				else if (CommonSettings.strDefaultCashAccountId != string.Empty && CommonSettings.strDefaultCashAccountId != "1")
				{
					iCashAcctId = (iPartyAccountID = (iNormalCashAccountId = (Utility.uCounterCashAccountId = CommonSettings.strDefaultCashAccountId.ToInt32())));
				}
				else
				{
					iCashAcctId = (iPartyAccountID = (iNormalCashAccountId = (Utility.uCounterCashAccountId = db.Accounts(new
					{
						AccountId = 1
					}).FirstOrDefault().AccountId.ToInt32())));
					CommonSettings.strDefaultCashAccountId = iNormalCashAccountId.ToString();
				}
				await AniHelper.AutoCompleteMethod(txtsearchitemname);
				base.WindowState = FormWindowState.Maximized;
				company = db.Companies().FirstOrDefault();
				previlegeDtlPOS = AniHelper.GetPrevilageMethod(null, "POS");
				cbPrint.Checked = (Settings.Default.isPrintOn ? true : false);
				txtPrintCopy.Data = Settings.Default.iPrintCopy;
				iCardAccount = CommonSettings.strDefaultBankAccountId.ToInt32();
				iOtherAccount = CommonSettings.strDefaultOnlineAccountId.ToInt32();
				col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
				col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_DisAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Tax.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_TaxAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_NetAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				if (Utility.uResolutionWidth <= 1024)
				{
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = col_SL;
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = col_Discount;
					col_Tax.Visible = false;
					dataGridViewTextBoxColumn2.Visible = false;
					dataGridViewTextBoxColumn.Visible = false;
					foreach (Control item in flpTopMenu.Controls)
					{
						item.Width = 55;
					}
				}
				lblBalance.Format = CommonSettings.strCurencyFormat;
				entPOSDetails = new List<POSDetails>();
				dgItems.AutoGenerateColumns = false;
				dgItems.DataSource = null;
				bdsItems.DataSource = null;
				bdsItems.DataSource = entPOSDetails;
				dgItems.DataSource = bdsItems;
				if (LoadInitialSettings())
				{
					nudInvoiceNo.Value = (db.SalesMasters(new
					{
						InvType = CommonSettings.strDefaultSalesType,
						FYearID = iFYearId,
						FK_BranchId = Settings.Default.iBranchId
					}).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				}
				else
				{
					Close();
				}
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
				HoldBillCountMethod();
				txtqty.Text = string.Empty;
				FocusMethod();
				if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
				{
					AniHelper.Display(company.InstituteName, "");
				}
				await LoadProductMethod();
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				MessageBox.Show(ex.Message ?? "");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardPOSView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlNumberPad = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dashboardPOSKeyboard1 = new AshUsercontrol.DashboardPOSKeyboard();
            this.btnbarcode = new System.Windows.Forms.Button();
            this.txtqty = new AshUsercontrol.AshTBox();
            this.nudInvoiceNo = new AshUsercontrol.AshUpDown();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbwhole = new System.Windows.Forms.ComboBox();
            this.cmbwarehouse = new System.Windows.Forms.ComboBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnpush = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlGroupBar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnTop = new System.Windows.Forms.Button();
            this.btnDwn = new System.Windows.Forms.Button();
            this.pnlGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.txtsearchitemname = new AshUsercontrol.AshTBox();
            this.flpTopMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearchBill = new System.Windows.Forms.Button();
            this.btnfastSale = new System.Windows.Forms.Button();
            this.btnpro = new System.Windows.Forms.Button();
            this.btndrawer = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnRetrieveBill = new System.Windows.Forms.Button();
            this.btncancelsale = new System.Windows.Forms.Button();
            this.btnnewsale = new System.Windows.Forms.Button();
            this.btnsearchcustom = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.flpProduct = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btneditline = new System.Windows.Forms.Button();
            this.btnstck = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnlindisc = new System.Windows.Forms.Button();
            this.btnup = new System.Windows.Forms.Button();
            this.btndltline = new System.Windows.Forms.Button();
            this.btndown = new System.Windows.Forms.Button();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.txtPCopy2 = new AshUsercontrol.AshTextBoxNormal();
            this.txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
            this.lblChanges = new AshUsercontrol.AshNumericLabel();
            this.lblLastBill = new AshUsercontrol.AshNumericLabel();
            this.lChanges = new System.Windows.Forms.Label();
            this.lLastBill = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cbPrint2 = new System.Windows.Forms.CheckBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cbPrint = new System.Windows.Forms.CheckBox();
            this.lblGrandTotal = new AshUsercontrol.AshNumericLabel();
            this.lblqty = new System.Windows.Forms.Label();
            this.lblcust = new System.Windows.Forms.Label();
            this.lblsubtotal = new System.Windows.Forms.Label();
            this.lblloginname = new System.Windows.Forms.Label();
            this.lblTaxTotal = new AshUsercontrol.AshTBox();
            this.lblBalance = new AshUsercontrol.AshTBox();
            this.txtAddDiscountAmnt = new AshUsercontrol.AshTBox();
            this.lbltotalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbldis = new System.Windows.Forms.Label();
            this.lblTotalQty = new AshUsercontrol.AshTBox();
            this.lbltax = new System.Windows.Forms.Label();
            this.lblGross = new AshUsercontrol.AshTBox();
            this.lblTotal = new AshUsercontrol.AshTBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_FreeQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DisAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrGrandTotal = new System.Windows.Forms.Timer(this.components);
            this.ttDashboardPOS = new System.Windows.Forms.ToolTip(this.components);
            this.bdsItems = new System.Windows.Forms.BindingSource(this.components);
            this.pnlNumberPad.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).BeginInit();
            this.pnlGroupBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flpTopMenu.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNumberPad
            // 
            resources.ApplyResources(this.pnlNumberPad, "pnlNumberPad");
            this.pnlNumberPad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlNumberPad.Controls.Add(this.panel1);
            this.pnlNumberPad.Controls.Add(this.pnlGroupBar);
            this.pnlNumberPad.Controls.Add(this.txtsearchitemname);
            this.pnlNumberPad.Name = "pnlNumberPad";
            this.ttDashboardPOS.SetToolTip(this.pnlNumberPad, resources.GetString("pnlNumberPad.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.dashboardPOSKeyboard1);
            this.panel1.Controls.Add(this.btnbarcode);
            this.panel1.Controls.Add(this.txtqty);
            this.panel1.Controls.Add(this.nudInvoiceNo);
            this.panel1.Controls.Add(this.txtBarcode);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.cmbwhole);
            this.panel1.Controls.Add(this.cmbwarehouse);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnpush);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Name = "panel1";
            this.ttDashboardPOS.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // dashboardPOSKeyboard1
            // 
            resources.ApplyResources(this.dashboardPOSKeyboard1, "dashboardPOSKeyboard1");
            this.dashboardPOSKeyboard1.Name = "dashboardPOSKeyboard1";
            this.ttDashboardPOS.SetToolTip(this.dashboardPOSKeyboard1, resources.GetString("dashboardPOSKeyboard1.ToolTip"));
            this.dashboardPOSKeyboard1.atEqualClick += new AshUsercontrol.NumberClickEventHandler(this.dashboardPOSKeyboard1_atEqualClick);
            this.dashboardPOSKeyboard1.atStarClick += new AshUsercontrol.StarClickEventHandler(this.dashboardPOSKeyboard1_atStarClick);
            this.dashboardPOSKeyboard1.atAddClick += new AshUsercontrol.AddClickEventHandler(this.dashboardPOSKeyboard1_atAddClick);
            this.dashboardPOSKeyboard1.atSubClick += new AshUsercontrol.SubClickEventHandler(this.dashboardPOSKeyboard1_atSubClick);
            // 
            // btnbarcode
            // 
            resources.ApplyResources(this.btnbarcode, "btnbarcode");
            this.btnbarcode.BackColor = System.Drawing.Color.Gainsboro;
            this.btnbarcode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnbarcode.FlatAppearance.BorderSize = 0;
            this.btnbarcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnbarcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnbarcode.Name = "btnbarcode";
            this.ttDashboardPOS.SetToolTip(this.btnbarcode, resources.GetString("btnbarcode.ToolTip"));
            this.btnbarcode.UseVisualStyleBackColor = false;
            this.btnbarcode.Click += new System.EventHandler(this.btnbarcode_Click);
            // 
            // txtqty
            // 
            resources.ApplyResources(this.txtqty, "txtqty");
            this.txtqty.Data = 0;
            this.txtqty.EnterColor = System.Drawing.Color.LightPink;
            this.txtqty.Format = null;
            this.txtqty.isAllowNegative = false;
            this.txtqty.isAllowSpecialChar = false;
            this.txtqty.isNumberOnly = false;
            this.txtqty.isNumeric = true;
            this.txtqty.LeaveColor = System.Drawing.Color.White;
            this.txtqty.Name = "txtqty";
            this.ttDashboardPOS.SetToolTip(this.txtqty, resources.GetString("txtqty.ToolTip"));
            this.txtqty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudInvoiceNo
            // 
            resources.ApplyResources(this.nudInvoiceNo, "nudInvoiceNo");
            this.nudInvoiceNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nudInvoiceNo.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudInvoiceNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInvoiceNo.Name = "nudInvoiceNo";
            this.ttDashboardPOS.SetToolTip(this.nudInvoiceNo, resources.GetString("nudInvoiceNo.ToolTip"));
            this.nudInvoiceNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInvoiceNo.ValueChanged += new System.EventHandler(this.nudInvoiceNo_ValueChanged);
            // 
            // txtBarcode
            // 
            resources.ApplyResources(this.txtBarcode, "txtBarcode");
            this.txtBarcode.Name = "txtBarcode";
            this.ttDashboardPOS.SetToolTip(this.txtBarcode, resources.GetString("txtBarcode.ToolTip"));
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.ttDashboardPOS.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cmbwhole
            // 
            resources.ApplyResources(this.cmbwhole, "cmbwhole");
            this.cmbwhole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbwhole.FormattingEnabled = true;
            this.cmbwhole.Name = "cmbwhole";
            this.ttDashboardPOS.SetToolTip(this.cmbwhole, resources.GetString("cmbwhole.ToolTip"));
            // 
            // cmbwarehouse
            // 
            resources.ApplyResources(this.cmbwarehouse, "cmbwarehouse");
            this.cmbwarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbwarehouse.FormattingEnabled = true;
            this.cmbwarehouse.Name = "cmbwarehouse";
            this.ttDashboardPOS.SetToolTip(this.cmbwarehouse, resources.GetString("cmbwarehouse.ToolTip"));
            // 
            // btnLast
            // 
            resources.ApplyResources(this.btnLast, "btnLast");
            this.btnLast.Name = "btnLast";
            this.ttDashboardPOS.SetToolTip(this.btnLast, resources.GetString("btnLast.ToolTip"));
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnpush
            // 
            resources.ApplyResources(this.btnpush, "btnpush");
            this.btnpush.Name = "btnpush";
            this.ttDashboardPOS.SetToolTip(this.btnpush, resources.GetString("btnpush.ToolTip"));
            this.btnpush.UseVisualStyleBackColor = true;
            this.btnpush.Click += new System.EventHandler(this.btnpush_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.ttDashboardPOS.SetToolTip(this.btnNext, resources.GetString("btnNext.ToolTip"));
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFirst
            // 
            resources.ApplyResources(this.btnFirst, "btnFirst");
            this.btnFirst.Name = "btnFirst";
            this.ttDashboardPOS.SetToolTip(this.btnFirst, resources.GetString("btnFirst.ToolTip"));
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.ttDashboardPOS.SetToolTip(this.btnBack, resources.GetString("btnBack.ToolTip"));
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnlGroupBar
            // 
            resources.ApplyResources(this.pnlGroupBar, "pnlGroupBar");
            this.pnlGroupBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGroupBar.Controls.Add(this.panel2);
            this.pnlGroupBar.Controls.Add(this.pnlGroup);
            this.pnlGroupBar.Name = "pnlGroupBar";
            this.ttDashboardPOS.SetToolTip(this.pnlGroupBar, resources.GetString("pnlGroupBar.ToolTip"));
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.btnTop);
            this.panel2.Controls.Add(this.btnDwn);
            this.panel2.Name = "panel2";
            this.ttDashboardPOS.SetToolTip(this.panel2, resources.GetString("panel2.ToolTip"));
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.ttDashboardPOS.SetToolTip(this.lblCount, resources.GetString("lblCount.ToolTip"));
            // 
            // btnTop
            // 
            resources.ApplyResources(this.btnTop, "btnTop");
            this.btnTop.BackColor = System.Drawing.Color.Gainsboro;
            this.btnTop.Name = "btnTop";
            this.btnTop.Tag = "1";
            this.ttDashboardPOS.SetToolTip(this.btnTop, resources.GetString("btnTop.ToolTip"));
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnDwn
            // 
            resources.ApplyResources(this.btnDwn, "btnDwn");
            this.btnDwn.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDwn.Name = "btnDwn";
            this.btnDwn.Tag = "1";
            this.ttDashboardPOS.SetToolTip(this.btnDwn, resources.GetString("btnDwn.ToolTip"));
            this.btnDwn.UseVisualStyleBackColor = false;
            this.btnDwn.Click += new System.EventHandler(this.btnDwn_Click);
            // 
            // pnlGroup
            // 
            resources.ApplyResources(this.pnlGroup, "pnlGroup");
            this.pnlGroup.Name = "pnlGroup";
            this.ttDashboardPOS.SetToolTip(this.pnlGroup, resources.GetString("pnlGroup.ToolTip"));
            // 
            // txtsearchitemname
            // 
            resources.ApplyResources(this.txtsearchitemname, "txtsearchitemname");
            this.txtsearchitemname.Data = 0;
            this.txtsearchitemname.EnterColor = System.Drawing.Color.LightPink;
            this.txtsearchitemname.Format = null;
            this.txtsearchitemname.isAllowNegative = false;
            this.txtsearchitemname.isAllowSpecialChar = false;
            this.txtsearchitemname.isNumberOnly = false;
            this.txtsearchitemname.isNumeric = false;
            this.txtsearchitemname.LeaveColor = System.Drawing.Color.White;
            this.txtsearchitemname.Name = "txtsearchitemname";
            this.ttDashboardPOS.SetToolTip(this.txtsearchitemname, resources.GetString("txtsearchitemname.ToolTip"));
            this.txtsearchitemname.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtsearchitemname.Validated += new System.EventHandler(this.txtsearchitemname_Validated);
            // 
            // flpTopMenu
            // 
            resources.ApplyResources(this.flpTopMenu, "flpTopMenu");
            this.flpTopMenu.Controls.Add(this.btnSearchBill);
            this.flpTopMenu.Controls.Add(this.btnfastSale);
            this.flpTopMenu.Controls.Add(this.btnpro);
            this.flpTopMenu.Controls.Add(this.btndrawer);
            this.flpTopMenu.Controls.Add(this.btnprint);
            this.flpTopMenu.Controls.Add(this.btnRetrieveBill);
            this.flpTopMenu.Controls.Add(this.btncancelsale);
            this.flpTopMenu.Controls.Add(this.btnnewsale);
            this.flpTopMenu.Controls.Add(this.btnsearchcustom);
            this.flpTopMenu.Controls.Add(this.btnReturn);
            this.flpTopMenu.Name = "flpTopMenu";
            this.ttDashboardPOS.SetToolTip(this.flpTopMenu, resources.GetString("flpTopMenu.ToolTip"));
            // 
            // btnSearchBill
            // 
            resources.ApplyResources(this.btnSearchBill, "btnSearchBill");
            this.btnSearchBill.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSearchBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnSearchBill.Name = "btnSearchBill";
            this.ttDashboardPOS.SetToolTip(this.btnSearchBill, resources.GetString("btnSearchBill.ToolTip"));
            this.btnSearchBill.UseVisualStyleBackColor = false;
            this.btnSearchBill.Click += new System.EventHandler(this.btnSearchBill_Click);
            // 
            // btnfastSale
            // 
            resources.ApplyResources(this.btnfastSale, "btnfastSale");
            this.btnfastSale.BackColor = System.Drawing.Color.Gainsboro;
            this.btnfastSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnfastSale.Name = "btnfastSale";
            this.ttDashboardPOS.SetToolTip(this.btnfastSale, resources.GetString("btnfastSale.ToolTip"));
            this.btnfastSale.UseVisualStyleBackColor = false;
            this.btnfastSale.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnpro
            // 
            resources.ApplyResources(this.btnpro, "btnpro");
            this.btnpro.BackColor = System.Drawing.Color.Gainsboro;
            this.btnpro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnpro.Name = "btnpro";
            this.ttDashboardPOS.SetToolTip(this.btnpro, resources.GetString("btnpro.ToolTip"));
            this.btnpro.UseVisualStyleBackColor = false;
            this.btnpro.Click += new System.EventHandler(this.btnpro_Click);
            // 
            // btndrawer
            // 
            resources.ApplyResources(this.btndrawer, "btndrawer");
            this.btndrawer.BackColor = System.Drawing.Color.Gainsboro;
            this.btndrawer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btndrawer.Name = "btndrawer";
            this.ttDashboardPOS.SetToolTip(this.btndrawer, resources.GetString("btndrawer.ToolTip"));
            this.btndrawer.UseVisualStyleBackColor = false;
            this.btndrawer.Click += new System.EventHandler(this.btnCashDrawer_Click);
            // 
            // btnprint
            // 
            resources.ApplyResources(this.btnprint, "btnprint");
            this.btnprint.BackColor = System.Drawing.Color.Gainsboro;
            this.btnprint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnprint.Name = "btnprint";
            this.ttDashboardPOS.SetToolTip(this.btnprint, resources.GetString("btnprint.ToolTip"));
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnRetrieveBill
            // 
            resources.ApplyResources(this.btnRetrieveBill, "btnRetrieveBill");
            this.btnRetrieveBill.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRetrieveBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnRetrieveBill.Name = "btnRetrieveBill";
            this.ttDashboardPOS.SetToolTip(this.btnRetrieveBill, resources.GetString("btnRetrieveBill.ToolTip"));
            this.btnRetrieveBill.UseVisualStyleBackColor = false;
            this.btnRetrieveBill.Click += new System.EventHandler(this.btnRetrieveBill_Click);
            // 
            // btncancelsale
            // 
            resources.ApplyResources(this.btncancelsale, "btncancelsale");
            this.btncancelsale.BackColor = System.Drawing.Color.Gainsboro;
            this.btncancelsale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btncancelsale.Name = "btncancelsale";
            this.ttDashboardPOS.SetToolTip(this.btncancelsale, resources.GetString("btncancelsale.ToolTip"));
            this.btncancelsale.UseVisualStyleBackColor = false;
            this.btncancelsale.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnnewsale
            // 
            resources.ApplyResources(this.btnnewsale, "btnnewsale");
            this.btnnewsale.BackColor = System.Drawing.Color.Gainsboro;
            this.btnnewsale.FlatAppearance.BorderSize = 0;
            this.btnnewsale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnnewsale.Name = "btnnewsale";
            this.ttDashboardPOS.SetToolTip(this.btnnewsale, resources.GetString("btnnewsale.ToolTip"));
            this.btnnewsale.UseVisualStyleBackColor = false;
            this.btnnewsale.Click += new System.EventHandler(this.btnHoldBill_Click);
            // 
            // btnsearchcustom
            // 
            resources.ApplyResources(this.btnsearchcustom, "btnsearchcustom");
            this.btnsearchcustom.BackColor = System.Drawing.Color.Gainsboro;
            this.btnsearchcustom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsearchcustom.Name = "btnsearchcustom";
            this.ttDashboardPOS.SetToolTip(this.btnsearchcustom, resources.GetString("btnsearchcustom.ToolTip"));
            this.btnsearchcustom.UseVisualStyleBackColor = false;
            this.btnsearchcustom.Click += new System.EventHandler(this.btnsearchcustom_Click);
            // 
            // btnReturn
            // 
            resources.ApplyResources(this.btnReturn, "btnReturn");
            this.btnReturn.BackColor = System.Drawing.Color.Gainsboro;
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnReturn.Name = "btnReturn";
            this.ttDashboardPOS.SetToolTip(this.btnReturn, resources.GetString("btnReturn.ToolTip"));
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // flpProduct
            // 
            resources.ApplyResources(this.flpProduct, "flpProduct");
            this.flpProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.flpProduct.Name = "flpProduct";
            this.ttDashboardPOS.SetToolTip(this.flpProduct, resources.GetString("flpProduct.ToolTip"));
            // 
            // pnlSidebar
            // 
            resources.ApplyResources(this.pnlSidebar, "pnlSidebar");
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlSidebar.Controls.Add(this.btneditline);
            this.pnlSidebar.Controls.Add(this.btnstck);
            this.pnlSidebar.Controls.Add(this.btnsearch);
            this.pnlSidebar.Controls.Add(this.btnlindisc);
            this.pnlSidebar.Controls.Add(this.btnup);
            this.pnlSidebar.Controls.Add(this.btndltline);
            this.pnlSidebar.Controls.Add(this.btndown);
            this.pnlSidebar.Name = "pnlSidebar";
            this.ttDashboardPOS.SetToolTip(this.pnlSidebar, resources.GetString("pnlSidebar.ToolTip"));
            // 
            // btneditline
            // 
            resources.ApplyResources(this.btneditline, "btneditline");
            this.btneditline.BackColor = System.Drawing.Color.Gainsboro;
            this.btneditline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btneditline.Name = "btneditline";
            this.ttDashboardPOS.SetToolTip(this.btneditline, resources.GetString("btneditline.ToolTip"));
            this.btneditline.UseVisualStyleBackColor = false;
            this.btneditline.Click += new System.EventHandler(this.btneditline_Click);
            // 
            // btnstck
            // 
            resources.ApplyResources(this.btnstck, "btnstck");
            this.btnstck.BackColor = System.Drawing.Color.Gainsboro;
            this.btnstck.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnstck.Name = "btnstck";
            this.ttDashboardPOS.SetToolTip(this.btnstck, resources.GetString("btnstck.ToolTip"));
            this.btnstck.UseVisualStyleBackColor = false;
            this.btnstck.Click += new System.EventHandler(this.btnstck_Click);
            // 
            // btnsearch
            // 
            resources.ApplyResources(this.btnsearch, "btnsearch");
            this.btnsearch.BackColor = System.Drawing.Color.Gainsboro;
            this.btnsearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnsearch.Name = "btnsearch";
            this.ttDashboardPOS.SetToolTip(this.btnsearch, resources.GetString("btnsearch.ToolTip"));
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnlindisc
            // 
            resources.ApplyResources(this.btnlindisc, "btnlindisc");
            this.btnlindisc.BackColor = System.Drawing.Color.Gainsboro;
            this.btnlindisc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnlindisc.Name = "btnlindisc";
            this.ttDashboardPOS.SetToolTip(this.btnlindisc, resources.GetString("btnlindisc.ToolTip"));
            this.btnlindisc.UseVisualStyleBackColor = false;
            this.btnlindisc.Click += new System.EventHandler(this.btnlindisc_Click);
            // 
            // btnup
            // 
            resources.ApplyResources(this.btnup, "btnup");
            this.btnup.BackColor = System.Drawing.Color.Gainsboro;
            this.btnup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btnup.Name = "btnup";
            this.ttDashboardPOS.SetToolTip(this.btnup, resources.GetString("btnup.ToolTip"));
            this.btnup.UseVisualStyleBackColor = false;
            this.btnup.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btndltline
            // 
            resources.ApplyResources(this.btndltline, "btndltline");
            this.btndltline.BackColor = System.Drawing.Color.Gainsboro;
            this.btndltline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(123)))), ((int)(((byte)(162)))));
            this.btndltline.Name = "btndltline";
            this.ttDashboardPOS.SetToolTip(this.btndltline, resources.GetString("btndltline.ToolTip"));
            this.btndltline.UseVisualStyleBackColor = false;
            this.btndltline.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btndown
            // 
            resources.ApplyResources(this.btndown, "btndown");
            this.btndown.BackColor = System.Drawing.Color.Gainsboro;
            this.btndown.ForeColor = System.Drawing.Color.ForestGreen;
            this.btndown.Name = "btndown";
            this.ttDashboardPOS.SetToolTip(this.btndown, resources.GetString("btndown.ToolTip"));
            this.btndown.UseVisualStyleBackColor = false;
            this.btndown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // pnlDetails
            // 
            resources.ApplyResources(this.pnlDetails, "pnlDetails");
            this.pnlDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlDetails.Controls.Add(this.btnDiscount);
            this.pnlDetails.Controls.Add(this.txtPCopy2);
            this.pnlDetails.Controls.Add(this.txtPrintCopy);
            this.pnlDetails.Controls.Add(this.lblChanges);
            this.pnlDetails.Controls.Add(this.lblLastBill);
            this.pnlDetails.Controls.Add(this.lChanges);
            this.pnlDetails.Controls.Add(this.lLastBill);
            this.pnlDetails.Controls.Add(this.lblCustomer);
            this.pnlDetails.Controls.Add(this.cbPrint2);
            this.pnlDetails.Controls.Add(this.lblUser);
            this.pnlDetails.Controls.Add(this.cbPrint);
            this.pnlDetails.Controls.Add(this.lblGrandTotal);
            this.pnlDetails.Controls.Add(this.lblqty);
            this.pnlDetails.Controls.Add(this.lblcust);
            this.pnlDetails.Controls.Add(this.lblsubtotal);
            this.pnlDetails.Controls.Add(this.lblloginname);
            this.pnlDetails.Controls.Add(this.lblTaxTotal);
            this.pnlDetails.Controls.Add(this.lblBalance);
            this.pnlDetails.Controls.Add(this.txtAddDiscountAmnt);
            this.pnlDetails.Controls.Add(this.lbltotalLabel);
            this.pnlDetails.Controls.Add(this.label1);
            this.pnlDetails.Controls.Add(this.lbldis);
            this.pnlDetails.Controls.Add(this.lblTotalQty);
            this.pnlDetails.Controls.Add(this.lbltax);
            this.pnlDetails.Controls.Add(this.lblGross);
            this.pnlDetails.Controls.Add(this.lblTotal);
            this.pnlDetails.Name = "pnlDetails";
            this.ttDashboardPOS.SetToolTip(this.pnlDetails, resources.GetString("pnlDetails.ToolTip"));
            // 
            // btnDiscount
            // 
            resources.ApplyResources(this.btnDiscount, "btnDiscount");
            this.btnDiscount.Name = "btnDiscount";
            this.ttDashboardPOS.SetToolTip(this.btnDiscount, resources.GetString("btnDiscount.ToolTip"));
            this.btnDiscount.UseVisualStyleBackColor = true;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // txtPCopy2
            // 
            resources.ApplyResources(this.txtPCopy2, "txtPCopy2");
            this.txtPCopy2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPCopy2.Data = 1;
            this.txtPCopy2.EnterColor = System.Drawing.Color.LightPink;
            this.txtPCopy2.Format = null;
            this.txtPCopy2.isAllowNegative = false;
            this.txtPCopy2.isAllowSpecialChar = false;
            this.txtPCopy2.isNumberOnly = true;
            this.txtPCopy2.isNumeric = false;
            this.txtPCopy2.LeaveColor = System.Drawing.Color.White;
            this.txtPCopy2.Name = "txtPCopy2";
            this.txtPCopy2.TabStop = false;
            this.ttDashboardPOS.SetToolTip(this.txtPCopy2, resources.GetString("txtPCopy2.ToolTip"));
            this.txtPCopy2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPCopy2.Validated += new System.EventHandler(this.txtPrintCopy_Validated);
            // 
            // txtPrintCopy
            // 
            resources.ApplyResources(this.txtPrintCopy, "txtPrintCopy");
            this.txtPrintCopy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrintCopy.Data = 1;
            this.txtPrintCopy.EnterColor = System.Drawing.Color.LightPink;
            this.txtPrintCopy.Format = null;
            this.txtPrintCopy.isAllowNegative = false;
            this.txtPrintCopy.isAllowSpecialChar = false;
            this.txtPrintCopy.isNumberOnly = true;
            this.txtPrintCopy.isNumeric = false;
            this.txtPrintCopy.LeaveColor = System.Drawing.Color.White;
            this.txtPrintCopy.Name = "txtPrintCopy";
            this.txtPrintCopy.TabStop = false;
            this.ttDashboardPOS.SetToolTip(this.txtPrintCopy, resources.GetString("txtPrintCopy.ToolTip"));
            this.txtPrintCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPrintCopy.Validated += new System.EventHandler(this.txtPrintCopy_Validated);
            // 
            // lblChanges
            // 
            resources.ApplyResources(this.lblChanges, "lblChanges");
            this.lblChanges.ForeColor = System.Drawing.Color.Red;
            this.lblChanges.Format = null;
            this.lblChanges.Name = "lblChanges";
            this.ttDashboardPOS.SetToolTip(this.lblChanges, resources.GetString("lblChanges.ToolTip"));
            this.lblChanges.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblLastBill
            // 
            resources.ApplyResources(this.lblLastBill, "lblLastBill");
            this.lblLastBill.ForeColor = System.Drawing.Color.Red;
            this.lblLastBill.Format = null;
            this.lblLastBill.Name = "lblLastBill";
            this.ttDashboardPOS.SetToolTip(this.lblLastBill, resources.GetString("lblLastBill.ToolTip"));
            this.lblLastBill.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lChanges
            // 
            resources.ApplyResources(this.lChanges, "lChanges");
            this.lChanges.ForeColor = System.Drawing.Color.Red;
            this.lChanges.Name = "lChanges";
            this.ttDashboardPOS.SetToolTip(this.lChanges, resources.GetString("lChanges.ToolTip"));
            // 
            // lLastBill
            // 
            resources.ApplyResources(this.lLastBill, "lLastBill");
            this.lLastBill.ForeColor = System.Drawing.Color.Red;
            this.lLastBill.Name = "lLastBill";
            this.ttDashboardPOS.SetToolTip(this.lLastBill, resources.GetString("lLastBill.ToolTip"));
            // 
            // lblCustomer
            // 
            resources.ApplyResources(this.lblCustomer, "lblCustomer");
            this.lblCustomer.Name = "lblCustomer";
            this.ttDashboardPOS.SetToolTip(this.lblCustomer, resources.GetString("lblCustomer.ToolTip"));
            // 
            // cbPrint2
            // 
            resources.ApplyResources(this.cbPrint2, "cbPrint2");
            this.cbPrint2.Name = "cbPrint2";
            this.ttDashboardPOS.SetToolTip(this.cbPrint2, resources.GetString("cbPrint2.ToolTip"));
            this.cbPrint2.UseVisualStyleBackColor = true;
            this.cbPrint2.Click += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // lblUser
            // 
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.Name = "lblUser";
            this.ttDashboardPOS.SetToolTip(this.lblUser, resources.GetString("lblUser.ToolTip"));
            // 
            // cbPrint
            // 
            resources.ApplyResources(this.cbPrint, "cbPrint");
            this.cbPrint.Name = "cbPrint";
            this.ttDashboardPOS.SetToolTip(this.cbPrint, resources.GetString("cbPrint.ToolTip"));
            this.cbPrint.UseVisualStyleBackColor = true;
            this.cbPrint.Click += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // lblGrandTotal
            // 
            resources.ApplyResources(this.lblGrandTotal, "lblGrandTotal");
            this.lblGrandTotal.BackColor = System.Drawing.Color.DarkGray;
            this.lblGrandTotal.Format = "N2";
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.ttDashboardPOS.SetToolTip(this.lblGrandTotal, resources.GetString("lblGrandTotal.ToolTip"));
            this.lblGrandTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblqty
            // 
            resources.ApplyResources(this.lblqty, "lblqty");
            this.lblqty.ForeColor = System.Drawing.Color.Black;
            this.lblqty.Name = "lblqty";
            this.ttDashboardPOS.SetToolTip(this.lblqty, resources.GetString("lblqty.ToolTip"));
            // 
            // lblcust
            // 
            resources.ApplyResources(this.lblcust, "lblcust");
            this.lblcust.Name = "lblcust";
            this.ttDashboardPOS.SetToolTip(this.lblcust, resources.GetString("lblcust.ToolTip"));
            // 
            // lblsubtotal
            // 
            resources.ApplyResources(this.lblsubtotal, "lblsubtotal");
            this.lblsubtotal.ForeColor = System.Drawing.Color.Black;
            this.lblsubtotal.Name = "lblsubtotal";
            this.ttDashboardPOS.SetToolTip(this.lblsubtotal, resources.GetString("lblsubtotal.ToolTip"));
            // 
            // lblloginname
            // 
            resources.ApplyResources(this.lblloginname, "lblloginname");
            this.lblloginname.Name = "lblloginname";
            this.ttDashboardPOS.SetToolTip(this.lblloginname, resources.GetString("lblloginname.ToolTip"));
            // 
            // lblTaxTotal
            // 
            resources.ApplyResources(this.lblTaxTotal, "lblTaxTotal");
            this.lblTaxTotal.Data = 0;
            this.lblTaxTotal.EnterColor = System.Drawing.Color.LightPink;
            this.lblTaxTotal.Format = null;
            this.lblTaxTotal.isAllowNegative = false;
            this.lblTaxTotal.isAllowSpecialChar = false;
            this.lblTaxTotal.isNumberOnly = false;
            this.lblTaxTotal.isNumeric = true;
            this.lblTaxTotal.LeaveColor = System.Drawing.Color.White;
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.lblTaxTotal.ReadOnly = true;
            this.ttDashboardPOS.SetToolTip(this.lblTaxTotal, resources.GetString("lblTaxTotal.ToolTip"));
            this.lblTaxTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblBalance
            // 
            resources.ApplyResources(this.lblBalance, "lblBalance");
            this.lblBalance.Data = 0;
            this.lblBalance.EnterColor = System.Drawing.Color.LightPink;
            this.lblBalance.Format = null;
            this.lblBalance.isAllowNegative = false;
            this.lblBalance.isAllowSpecialChar = false;
            this.lblBalance.isNumberOnly = false;
            this.lblBalance.isNumeric = true;
            this.lblBalance.LeaveColor = System.Drawing.Color.White;
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.ReadOnly = true;
            this.ttDashboardPOS.SetToolTip(this.lblBalance, resources.GetString("lblBalance.ToolTip"));
            this.lblBalance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddDiscountAmnt
            // 
            resources.ApplyResources(this.txtAddDiscountAmnt, "txtAddDiscountAmnt");
            this.txtAddDiscountAmnt.Data = 0;
            this.txtAddDiscountAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddDiscountAmnt.Format = null;
            this.txtAddDiscountAmnt.isAllowNegative = false;
            this.txtAddDiscountAmnt.isAllowSpecialChar = false;
            this.txtAddDiscountAmnt.isNumberOnly = false;
            this.txtAddDiscountAmnt.isNumeric = true;
            this.txtAddDiscountAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtAddDiscountAmnt.Name = "txtAddDiscountAmnt";
            this.ttDashboardPOS.SetToolTip(this.txtAddDiscountAmnt, resources.GetString("txtAddDiscountAmnt.ToolTip"));
            this.txtAddDiscountAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAddDiscountAmnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddDiscountAmnt_KeyDown);
            this.txtAddDiscountAmnt.Validated += new System.EventHandler(this.txtAddDiscountAmnt_Validated);
            // 
            // lbltotalLabel
            // 
            resources.ApplyResources(this.lbltotalLabel, "lbltotalLabel");
            this.lbltotalLabel.ForeColor = System.Drawing.Color.Black;
            this.lbltotalLabel.Name = "lbltotalLabel";
            this.ttDashboardPOS.SetToolTip(this.lbltotalLabel, resources.GetString("lbltotalLabel.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            this.ttDashboardPOS.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // lbldis
            // 
            resources.ApplyResources(this.lbldis, "lbldis");
            this.lbldis.ForeColor = System.Drawing.Color.Black;
            this.lbldis.Name = "lbldis";
            this.ttDashboardPOS.SetToolTip(this.lbldis, resources.GetString("lbldis.ToolTip"));
            // 
            // lblTotalQty
            // 
            resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
            this.lblTotalQty.Data = 0;
            this.lblTotalQty.EnterColor = System.Drawing.Color.LightPink;
            this.lblTotalQty.Format = null;
            this.lblTotalQty.isAllowNegative = false;
            this.lblTotalQty.isAllowSpecialChar = false;
            this.lblTotalQty.isNumberOnly = false;
            this.lblTotalQty.isNumeric = true;
            this.lblTotalQty.LeaveColor = System.Drawing.Color.White;
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.ReadOnly = true;
            this.ttDashboardPOS.SetToolTip(this.lblTotalQty, resources.GetString("lblTotalQty.ToolTip"));
            this.lblTotalQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lbltax
            // 
            resources.ApplyResources(this.lbltax, "lbltax");
            this.lbltax.ForeColor = System.Drawing.Color.Black;
            this.lbltax.Name = "lbltax";
            this.ttDashboardPOS.SetToolTip(this.lbltax, resources.GetString("lbltax.ToolTip"));
            // 
            // lblGross
            // 
            resources.ApplyResources(this.lblGross, "lblGross");
            this.lblGross.Data = 0;
            this.lblGross.EnterColor = System.Drawing.Color.LightPink;
            this.lblGross.Format = null;
            this.lblGross.isAllowNegative = false;
            this.lblGross.isAllowSpecialChar = false;
            this.lblGross.isNumberOnly = false;
            this.lblGross.isNumeric = true;
            this.lblGross.LeaveColor = System.Drawing.Color.White;
            this.lblGross.Name = "lblGross";
            this.lblGross.ReadOnly = true;
            this.ttDashboardPOS.SetToolTip(this.lblGross, resources.GetString("lblGross.ToolTip"));
            this.lblGross.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Data = 0;
            this.lblTotal.EnterColor = System.Drawing.Color.LightPink;
            this.lblTotal.Format = null;
            this.lblTotal.isAllowNegative = false;
            this.lblTotal.isAllowSpecialChar = false;
            this.lblTotal.isNumberOnly = false;
            this.lblTotal.isNumeric = true;
            this.lblTotal.LeaveColor = System.Drawing.Color.White;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.ReadOnly = true;
            this.ttDashboardPOS.SetToolTip(this.lblTotal, resources.GetString("lblTotal.ToolTip"));
            this.lblTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // pnlGrid
            // 
            resources.ApplyResources(this.pnlGrid, "pnlGrid");
            this.pnlGrid.Controls.Add(this.dgItems);
            this.pnlGrid.Controls.Add(this.pnlSidebar);
            this.pnlGrid.Controls.Add(this.flpTopMenu);
            this.pnlGrid.Controls.Add(this.pnlDetails);
            this.pnlGrid.Name = "pnlGrid";
            this.ttDashboardPOS.SetToolTip(this.pnlGrid, resources.GetString("pnlGrid.ToolTip"));
            // 
            // dgItems
            // 
            resources.ApplyResources(this.dgItems, "dgItems");
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ProductCode,
            this.col_ProductName,
            this.col_Qty,
            this.col_Unit,
            this.col_FreeQty,
            this.col_Rate,
            this.col_Gross,
            this.col_Discount,
            this.col_DisAmount,
            this.col_Taxable,
            this.col_Tax,
            this.col_TaxAmount,
            this.col_NetAmount});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgItems.EnableHeadersVisualStyles = false;
            this.dgItems.GridColor = System.Drawing.SystemColors.Control;
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.RowTemplate.Height = 35;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ttDashboardPOS.SetToolTip(this.dgItems, resources.GetString("dgItems.ToolTip"));
            this.dgItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellContentClick);
            this.dgItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgItems_DataError);
            this.dgItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItems_EditingControlShowing);
            this.dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyDown);
            this.dgItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgItems_CellContentClick);
            // 
            // col_SL
            // 
            this.col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SL.DataPropertyName = "SL";
            this.col_SL.FillWeight = 30.18542F;
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_ProductCode
            // 
            this.col_ProductCode.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.col_ProductCode, "col_ProductCode");
            this.col_ProductCode.Name = "col_ProductCode";
            this.col_ProductCode.ReadOnly = true;
            // 
            // col_ProductName
            // 
            this.col_ProductName.DataPropertyName = "ProductName";
            this.col_ProductName.FillWeight = 174.3004F;
            resources.ApplyResources(this.col_ProductName, "col_ProductName");
            this.col_ProductName.Name = "col_ProductName";
            this.col_ProductName.ReadOnly = true;
            // 
            // col_Qty
            // 
            this.col_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_Qty.FillWeight = 71.59125F;
            resources.ApplyResources(this.col_Qty, "col_Qty");
            this.col_Qty.Name = "col_Qty";
            // 
            // col_Unit
            // 
            this.col_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Unit.DataPropertyName = "UnitName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Unit.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.col_Unit, "col_Unit");
            this.col_Unit.Name = "col_Unit";
            this.col_Unit.ReadOnly = true;
            // 
            // col_FreeQty
            // 
            this.col_FreeQty.DataPropertyName = "FreeQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_FreeQty.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_FreeQty, "col_FreeQty");
            this.col_FreeQty.Name = "col_FreeQty";
            // 
            // col_Rate
            // 
            this.col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Rate.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_Rate.FillWeight = 97.0194F;
            resources.ApplyResources(this.col_Rate, "col_Rate");
            this.col_Rate.Name = "col_Rate";
            // 
            // col_Gross
            // 
            this.col_Gross.DataPropertyName = "Gross";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Gross.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.col_Gross, "col_Gross");
            this.col_Gross.Name = "col_Gross";
            this.col_Gross.ReadOnly = true;
            // 
            // col_Discount
            // 
            this.col_Discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Discount.DataPropertyName = "Discount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Discount.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.col_Discount, "col_Discount");
            this.col_Discount.Name = "col_Discount";
            // 
            // col_DisAmount
            // 
            this.col_DisAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_DisAmount.DataPropertyName = "DisAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_DisAmount.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.col_DisAmount, "col_DisAmount");
            this.col_DisAmount.Name = "col_DisAmount";
            // 
            // col_Taxable
            // 
            this.col_Taxable.DataPropertyName = "Taxable";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Taxable.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.col_Taxable, "col_Taxable");
            this.col_Taxable.Name = "col_Taxable";
            // 
            // col_Tax
            // 
            this.col_Tax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Tax.DataPropertyName = "Tax";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Tax.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.col_Tax, "col_Tax");
            this.col_Tax.Name = "col_Tax";
            // 
            // col_TaxAmount
            // 
            this.col_TaxAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_TaxAmount.DataPropertyName = "TaxAmount";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_TaxAmount.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.col_TaxAmount, "col_TaxAmount");
            this.col_TaxAmount.Name = "col_TaxAmount";
            // 
            // col_NetAmount
            // 
            this.col_NetAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_NetAmount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            this.col_NetAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.col_NetAmount.FillWeight = 126.9036F;
            resources.ApplyResources(this.col_NetAmount, "col_NetAmount");
            this.col_NetAmount.Name = "col_NetAmount";
            this.col_NetAmount.ReadOnly = true;
            // 
            // tmrGrandTotal
            // 
            this.tmrGrandTotal.Interval = 500;
            this.tmrGrandTotal.Tick += new System.EventHandler(this.tmrGrandTotal_Tick);
            // 
            // DashboardPOSView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpProduct);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlNumberPad);
            this.Name = "DashboardPOSView";
            this.ttDashboardPOS.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.DashboardPOSView_Load);
            this.VisibleChanged += new System.EventHandler(this.DashboardPOSView_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DashboardPOSView_KeyDown);
            this.pnlNumberPad.ResumeLayout(false);
            this.pnlNumberPad.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).EndInit();
            this.pnlGroupBar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flpTopMenu.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsItems)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
