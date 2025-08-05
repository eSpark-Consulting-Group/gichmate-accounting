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
	public class RestaurantPOSView : BaseFormTwo
	{
		private DBContext db = new DBContext();

		private int iFYearId;

		private int? iMasterId;

		private SalesMaster salesMaster;

		private List<POSDetails> entPOSDetails;

		private List<POSDetails> entOldPOSDetails;

		private POSDetails entPosDetail;

		private List<SalesDetails> salesDetails;

		private string strVoucherType = "Sales Voucher";

		private string sBranchDetails = string.Empty;

		private Button btnProduct;

		private Button btnGroup;

		private int iIndex = 0;

		private int iNoofPages;

		private List<ExProductPOS> productPOs;

		private bool isToUp;

		private int iGrpPageNo = 0;

		private int iGrpNoofPages;

		private List<ProductGroupEntity> entProGroups;

		private string sPagedetails;

		private int iSalesManID;

		private int iPartyAccountID;

		private int iSalesAccountId;

		private int iNormalCashAccountId;

		private decimal dmlDiscountAmount;

		private int? iCashAcctId;

		private int? iCardAccount;

		private int? iOtherAccount;

		private int iRouteId;

		private int iVehicleId;

		private int iPricecategoryId;

		private decimal dCashPayment;

		private decimal dCardPayment;

		private decimal dOtherPayment;

		private decimal dBalance;

		private decimal dChanges;

		private decimal? dTendered;

		private bool isReturn = false;

		private int iUserId;

		private KOTOrderMaster kOTOrderMaster = new KOTOrderMaster();

		private int? iKotMasterId;

		private DotmatrixMaster entDMaster;

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private List<Transaction> entsalesdtls = new List<Transaction>();

		private Company company;

		private int? rtnInvNumber;

		private decimal dSubDiscount;

		private SecondDisplayView SecondDisplay = new SecondDisplayView();

		private System.Windows.Forms.Timer idleTimer = new System.Windows.Forms.Timer();

		private IContainer components = null;

		private Panel pnlSide;

		private DataGridView dgItems;

		private Panel pnlNumberpad;

		private Panel pnlInvoice;

		private Panel pnlMain;

		private TabControl tabMain;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private FlowLayoutPanel pnlMainMenu;

		private Button btnLast;

		private Button btnNext;

		private Button btnPrevious;

		private Button btnFirst;

		private Panel pnlDetails;

		private Button btnReprint;

		private Button btnNotes;

		private Button btnPayment;

		private Button btnModify;

		private Button btnAddons;

		private Button btnKOT;

		private Button btnVoid;

		private Button btnExtra;

		private Button btnClear;

		private Label label3;

		private Label label2;

		private AshNumericLabel lblGrandTotal;

		private Label lblTaxTotal;

		private Label lblSubTotal;

		private Label label7;

		private Label label1;

		private AshTextBoxNormal txtDiscount;

		private Button btnBarcode;

		private AshTextBoxNormal txtSearchItem;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private RestaurantKeyboard restaurantKeyboard1;

		private AshTextBoxNormal txtInvoiceNo;

		private FlowLayoutPanel flpProduct;

		private FlowLayoutPanel flpGroup;

		private Button btnKotLookUp;

		private Button btnMinus;

		private Button btnReturn;

		private Button btnFormType;

		private Button btnPlus;

		private Button btnHold;

		private Button btnfastSale;

		private Button btnDown;

		private Button btnCustomerHistory;

		private Button btnRetrieveBill;

		private Button btnUp;

		private Button btnCashDrawer;

		private BindingSource bdsItems;

		private RadioButton rbName;

		private RadioButton rbBarcode;

		private Panel pnlKOT;

		private Button btnShowNumberpad;

		private Panel panel2;

		private Panel panel3;

		private Button btnShowToppanel;

		private Button btnUpGroup;

		private Panel pnlProductTop;

		private Panel panel4;

		private Button btnDownGroup;

		private Button btnSelectedGroup;

		private Panel panel5;

		private Panel panel6;

		private Button btnScroll;

		private Button btnPage;

		private Button btnFavourite;

		private AshTextBoxNormal txtCharges;

		private Panel pnlTop;

		private Panel pnlCustomer;

		private Label lblPhoneNumber;

		private Button btnCustomer;

		private Label label4;

		private AshTextBoxNormal txtCustomerName;

		private Panel pnlBuilding;

		private AshTextBoxNormal txtBuilding;

		private AshTextBoxNormal txtRoad;

		private Label lblAddressTwo;

		private Label lblAddressOne;

		private Panel pnlBlock;

		private AshTextBoxNormal txtBlock;

		private Label lblAddressThree;

		private AshTextBoxNormal txtFlat;

		private Label lblAddressFour;

		private Panel pnlExtra;

		private Label label9;

		private CheckBox cbPrint;

		private AshTextBoxNormal txtPrintCopy;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProductName;

		private DataGridViewTextBoxColumn col_Quantity;

		private DataGridViewTextBoxColumn col_Price;

		private DataGridViewTextBoxColumn col_Amount;

		private Button btnDiscount;

		public UserPrevilegeDtl previlegeDtlPOS { get; set; }

		public DashboardPOS dashboardPOS { get; set; }

		public RestaurantPOSView(DashboardPOS dashboardPOS)
		{
			InitializeComponent();
			tabMain.ItemSize = new Size(1, 1);
			base.Width = Utility.uResolutionWidth - dashboardPOS.btnPanelShow.Width * 2;
			base.Height = Utility.uResolutionHeight - dashboardPOS.pnlTitle.Height;
			lblGrandTotal.Format = CommonSettings.strCurencyFormat;
			col_Amount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Price.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			if (Utility.uResolutionWidth < 1366)
			{
				pnlExtra.Visible = false;
				Panel panel = pnlBlock;
				pnlBuilding.Width = 145;
				panel.Width = 145;
			}
			else if (Utility.uResolutionWidth >= 1920)
			{
				pnlExtra.Width = 360;
			}
			int num = base.Height / 13;
			int num2 = pnlMainMenu.Width - 6;
			foreach (Button control in pnlMainMenu.Controls)
			{
				control.Height = num - 7;
				control.Width = num2;
			}
			bool isNumPanelShow = Settings.Default.IsNumPanelShow;
			pnlNumberpad.Visible = isNumPanelShow;
			btnShowNumberpad.Text = (isNumPanelShow ? "<<" : ">>");
			isNumPanelShow = Settings.Default.IsTopPanelShow;
			pnlTop.Visible = isNumPanelShow;
			btnShowToppanel.Text = (isNumPanelShow ? "Hide" : "Show");
			idleTimer.Interval = 1000;
			idleTimer.Tick += IdleTimer_Tick;
			idleTimer.Enabled = true;
		}

		private void ColorChangeMethod()
		{
			restaurantKeyboard1.btn1.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn1.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn2.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn2.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn3.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn3.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn4.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn4.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn5.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn5.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn6.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn6.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn7.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn7.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn8.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn8.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn9.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn9.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btn0.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btn0.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btnClear.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btnClear.ForeColor = Settings.Default.NumberBF;
			restaurantKeyboard1.btnBackSpace.BackColor = Settings.Default.NumberBB;
			restaurantKeyboard1.btnBackSpace.ForeColor = Settings.Default.NumberBF;
			btnScroll.BackColor = Settings.Default.GeneralBB;
			btnScroll.ForeColor = Settings.Default.GeneralBF;
			btnSelectedGroup.BackColor = Settings.Default.GeneralBB;
			btnSelectedGroup.ForeColor = Settings.Default.GeneralBF;
			btnFavourite.BackColor = Settings.Default.GeneralBB;
			btnFavourite.ForeColor = Settings.Default.GeneralBF;
			btnPage.BackColor = Settings.Default.GeneralBB;
			btnPage.ForeColor = Settings.Default.GeneralBF;
			btnShowToppanel.BackColor = Settings.Default.GeneralBB;
			btnShowToppanel.ForeColor = Settings.Default.GeneralBF;
			btnUpGroup.BackColor = Settings.Default.GeneralBB;
			btnUpGroup.ForeColor = Settings.Default.GeneralBF;
			btnDownGroup.BackColor = Settings.Default.GeneralBB;
			btnDownGroup.ForeColor = Settings.Default.GeneralBF;
			foreach (Control control10 in pnlMainMenu.Controls)
			{
				if (control10 is Button)
				{
					Button button = (Button)control10;
					button.BackColor = Settings.Default.TopIBB;
					button.ForeColor = Settings.Default.TopIBF;
				}
			}
			pnlMainMenu.BackColor = Settings.Default.TopPB;
			pnlKOT.BackColor = Settings.Default.SidePB;
			foreach (Control control11 in pnlKOT.Controls)
			{
				if (control11 is Button)
				{
					Button button2 = (Button)control11;
					button2.BackColor = Settings.Default.SideIBB;
					button2.ForeColor = Settings.Default.SideIBF;
				}
			}
			Color color2 = (pnlNumberpad.BackColor = (restaurantKeyboard1.BackColor = Settings.Default.NumberpadPB));
			flpGroup.BackColor = Settings.Default.CategoryPB;
			foreach (Control control12 in flpGroup.Controls)
			{
				if (control12 is Button)
				{
					Button button3 = (Button)control12;
					button3.BackColor = Settings.Default.CategoryBB;
					button3.ForeColor = Settings.Default.CategoryBF;
				}
			}
			flpProduct.BackColor = Settings.Default.ProductPB;
			foreach (Control control13 in flpProduct.Controls)
			{
				if (control13 is Button)
				{
					Button button4 = (Button)control13;
					button4.BackColor = Settings.Default.ProductBB;
					button4.ForeColor = Settings.Default.ProductBF;
				}
			}
			foreach (Control control14 in pnlDetails.Controls)
			{
				control14.BackColor = Settings.Default.DetailPB;
			}
			foreach (Control control15 in pnlCustomer.Controls)
			{
				control15.BackColor = Settings.Default.DetailPB;
			}
			foreach (Control control16 in pnlBuilding.Controls)
			{
				control16.BackColor = Settings.Default.DetailPB;
			}
			foreach (Control control17 in pnlBlock.Controls)
			{
				control17.BackColor = Settings.Default.DetailPB;
			}
			foreach (Control control18 in pnlDetails.Controls)
			{
				if (control18 is Label)
				{
					Label label = (Label)control18;
					label.ForeColor = Settings.Default.DetailPF;
				}
			}
			dgItems.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.DgvheaderB;
			dgItems.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.DgvheaderF;
			dgItems.BackgroundColor = Settings.Default.DgvB;
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
					num = txtInvoiceNo.Data;
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
			obj4[3] = txtInvoiceNo.Data.ToString();
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

		public static bool InvoiceHoldMethod(SalesMaster salesMaster, List<POSDetails> pOSDetails)
		{
			try
			{
				List<SalesDetails> items = POSHelper.SalesDetailsMethod(pOSDetails, salesMaster.PartyAccountId.ToInt32(), salesMaster.InvType);
				DataSet dataSet = new DataSet();
				DataTable dataTable = new DataTable();
				dataTable = AniHelper.ListToDataTable(items);
				dataTable.TableName = "type";
				dataSet.Tables.Add(dataTable);
				salesMaster.CntrInvNo += salesMaster.SalesInvNo;
				AshproCommon.InsertMethod_SP(salesMaster, dataSet, "spInsertPOSHold");
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return false;
			}
			return true;
		}

		private void HoldBillCountMethod()
		{
			try
			{
				btnHold.Text = "Hold \n" + db.POSHoldMasters(new
				{
					InvType = CommonSettings.strDefaultSalesType
				}).Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private decimal FreeAmountTotalFindMethod()
		{
			decimal result = default(decimal);
			try
			{
				foreach (POSDetails item in entPOSDetails.Where((POSDetails x) => x.Rate == 0m).ToList())
				{
					result += AshproCommon.ValueFindMethod("SELECT SPrice FROM ProductPriceCategory where ProductId='" + item.productId + "' and PCategoryId='" + db.PriceCategories().FirstOrDefault().PCategoryId + "' and UnitId='" + item.UnitId + "'");
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
				if (salesMaster != null)
				{
					List<SalesDetails> list = new List<SalesDetails>();
					if (!isSales)
					{
						iMasterId = db.POSHoldMasters(new
						{
							InvType = salesMaster.InvType,
							SalesInvNo = salesMaster.SalesInvNo,
							FYearID = iFYearId,
							FK_BranchId = Settings.Default.iBranchId
						}).FirstOrDefault().SalesInvId.toInt32();
						list = db.POSHoldSettings(new
						{
							SalesInvId = iMasterId
						}).ToList();
					}
					else
					{
						txtInvoiceNo.Data = salesMaster.SalesInvNo.toInt32();
						iMasterId = db.SalesMasters(new
						{
							InvType = salesMaster.InvType,
							SalesInvNo = salesMaster.SalesInvNo,
							FYearID = iFYearId,
							FK_BranchId = Settings.Default.iBranchId
						}).FirstOrDefault().SalesInvId.toInt32();
						list = db.SalesDetails(new
						{
							SalesInvId = iMasterId
						}).ToList();
					}
					LoadMethod(salesMaster, list);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			salesMaster.Remarks = "POS Entry";
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
			salesMaster.FieldTwo = btnFormType.Text;
			salesMaster.OtherPayment = dOtherPayment;
			salesMaster.CashAccountId = iCashAcctId;
			salesMaster.CardAccountId = iCardAccount;
			salesMaster.OnlineAccountId = iOtherAccount;
			salesMaster.IsSettiled = ((!(salesMaster.Credit > 0m)) ? true : false);
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

		private void LoadMethod(SalesMaster salesMaster, List<SalesDetails> salesDetails)
		{
			entPOSDetails = new List<POSDetails>();
			entPOSDetails = POSHelper.GetPOSDetailsFromSDetails(salesDetails);
			for (int i = 0; i < entPOSDetails.Count; i++)
			{
				entPOSDetails[i].SL = i + 1;
			}
			decimal num = default(decimal);
			DgvLoadMethod(entPOSDetails);
			decimal num2 = salesMaster.GrandDiscount + salesMaster.BillDiscount;
			decimal billDiscount = salesMaster.BillDiscount;
			if (billDiscount > 0m)
			{
				num = billDiscount * 100m / num2;
			}
			TotalFindMethod();
			txtSearchItem.Focus();
		}

		private void BarcodeMethodWithName()
		{
			if (txtSearchItem.Text == string.Empty)
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
				ProductName = txtSearchItem.Text
			}).FirstOrDefault();
			if (product != null)
			{
				decimal? dmlQty = null;
				ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
				int pOSDetails = POSHelper.GetPOSDetails(product, entPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", dmlQty, product.Barcode, iUnitId, dmlRate);
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.productId == product.ProductId).FirstOrDefault();
				DgvLoadMethod(entPOSDetails);
				TotalFindMethod();
				dgItems.ClearSelection();
				dgItems.FirstDisplayedScrollingRowIndex = pOSDetails;
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgItems.CurrentCell = dgItems.Rows[pOSDetails].Cells[col_ProductName.Name];
				txtSearchItem.Text = string.Empty;
				txtSearchItem.Focus();
			}
		}

		public void DGVLoadFromProClick(ProductEntity product)
		{
			int num = 0;
			decimal? dmlQty = null;
			decimal? dmlRate = null;
			int? iUnitId = null;
			ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
			num = POSHelper.GetPOSDetails(product, entPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", dmlQty, product.Barcode, iUnitId, dmlRate);
			entPosDetail = entPOSDetails.FirstOrDefault((POSDetails x) => x.Barcode == product.Barcode);
			DgvLoadMethod(entPOSDetails);
			dgItems.ClearSelection();
			dgItems.FirstDisplayedScrollingRowIndex = num;
			dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgItems.CurrentCell = dgItems.Rows[num].Cells[col_ProductName.Name];
		}

		private void ScaleWithNoRateMethod(ProductEntity product, ref decimal? dmlRate, ref decimal? dmlQty, ref int? iUnitId)
		{
			if (!product.IsScale.toBool())
			{
				return;
			}
			if (!product.isRateBasedWeight.toBool())
			{
				string text = ((txtSearchItem.Text == string.Empty) ? product.Barcode : txtSearchItem.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(db.PriceCategories().FirstOrDefault().PCategoryId.ToInt32(), "Sales Invoice", text);
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
				string text2 = ((txtSearchItem.Text == string.Empty) ? product.Barcode : txtSearchItem.Text);
				iUnitId = AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + text2 + "'");
				dmlRate = POSHelper.SellingPriceAndDiscFindMethod(db.PriceCategories().FirstOrDefault().PCategoryId.toInt32(), "Sales Invoice", text2);
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

		private void PrintInvoiceMethod()
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					Messages.ErrorMessage("No records to Print!");
					return;
				}
				List<InvoiceAdjustment> list = new List<InvoiceAdjustment>();
				if (CommonSettings.blnInvoicePrintingByRDLC)
				{
					Shop shop = new Shop();
					if (entcompany != null)
					{
						shop = entcompany.CopyTo<Shop>();
						shop.ShopName = entcompany.CompanyName;
						shop.TaxNumber = entcompany.TaxId;
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
						invoiceNo = txtInvoiceNo.Data;
					}
					else
					{
						SalesMasterMethod(invoiceNo, db.Godowns().FirstOrDefault().GodwnId.toInt32(), txtDiscount.Value, lblGrandTotal.Value, dBalance);
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
					foreach (POSDetails item2 in entPOSDetails)
					{
						string nm2 = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item2.Tax).TCategoryName;
						if (!list.Any((InvoiceAdjustment x) => x.Name == nm2))
						{
							decimal num = entPOSDetails.Where((POSDetails x) => x.Tax == item2.Tax).Sum((POSDetails y) => y.TaxAmount);
							AniHelper.AddPaymentPrint(sRLabel: entPOSDetails.Where((POSDetails x) => x.Tax == item2.Tax).Sum((POSDetails y) => y.NetAmount).ToString(CommonSettings.strCurencyFormat), paymentPrint: list, sName: nm2, dValue: num.ToString(CommonSettings.strCurencyFormat));
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
					if (entDMaster == null)
					{
						Messages.InformationMessage("There is no default print design. Please make your own design");
						return;
					}
					entDSettings = new List<DotmatrixSettings>();
					entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
					entPartyDtl = new PartyDetails();
					AccountEntity accountEntity = db.Accounts(new
					{
						AccountId = iPartyAccountID
					}).FirstOrDefault();
					entPartyDtl = POSHelper.GetParty(accountEntity);
					if (!iMasterId.HasValue)
					{
						SalesMasterMethod(null, db.Godowns().FirstOrDefault().GodwnId.toInt32(), txtDiscount.Value, lblGrandTotal.Value, dBalance);
						salesMaster.CntrInvNo = string.Empty;
					}
					int num3 = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
					if (accountEntity.AGroupId == num3)
					{
						entPartyDtl.Name = "Cash Customer";
					}
					TransactionMaster transactionMaster = new TransactionMaster();
					transactionMaster = POSHelper.GetTMaster(salesMaster);
					transactionMaster.TotalQuantity = entPOSDetails.Sum((POSDetails x) => x.Qty);
					transactionMaster.GrossAmount = entPOSDetails.Sum((POSDetails x) => x.Gross);
					transactionMaster.OrderNo = 0;
					transactionMaster.Tendered = dTendered;
					transactionMaster.Changes = dChanges;
					transactionMaster.SalesMan = db.EmployeeDetails(new
					{
						EmpID = iSalesManID
					}).FirstOrDefault().EmpName;
					transactionMaster.PriceCategory = string.Empty;
					transactionMaster.SalesFormType = btnFormType.Text;
					if (lblGrandTotal.Value > 0m)
					{
						if (iPartyAccountID == iNormalCashAccountId)
						{
							transactionMaster.InvoiceMode = "Cash Sale";
						}
						else if (dBalance == 0m)
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
					transactionMaster.TockenNo = ((!iKotMasterId.HasValue) ? Settings.Default.iTockenNo : (Settings.Default.iTockenNo - 1));
					foreach (POSDetails item in entPOSDetails)
					{
						ProductBarcodeEntity productBarcodeEntity = db.ProductBarcodes(new { item.Barcode }).FirstOrDefault();
						if (productBarcodeEntity != null && productBarcodeEntity.ProductBarcodeName != null && productBarcodeEntity.LocalName != string.Empty)
						{
							item.LocalName = productBarcodeEntity.LocalName;
						}
						string nm = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item.Tax).TCategoryName;
						if (!list.Any((InvoiceAdjustment x) => x.Name == nm))
						{
							decimal num4 = entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.TaxAmount);
							AniHelper.AddPaymentPrint(sRLabel: entPOSDetails.Where((POSDetails x) => x.Tax == item.Tax).Sum((POSDetails y) => y.NetAmount).ToString(CommonSettings.strCurencyFormat), paymentPrint: list, sName: nm, dValue: num4.ToString(CommonSettings.strCurencyFormat));
						}
					}
					entsalesdtls = new List<Transaction>();
					entsalesdtls = POSHelper.GetTransactionFromPOSDetails(entPOSDetails, db, CommonSettings.strDefaultSalesType);
					PrintHelper printHelper = new PrintHelper();
					printHelper.PrintMethod(entDMaster, entDSettings, entcompany, transactionMaster, entPartyDtl, entsalesdtls, transactionMaster, list, transactionMaster, isPrint: true, txtPrintCopy.Data);
					printHelper = null;
				}
				txtSearchItem.Focus();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void BarcodeAddmethod()
		{
			try
			{
				if (txtSearchItem.Text == string.Empty)
				{
					txtSearchItem.Focus();
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
					Barcode = txtSearchItem.Text
				});
				if (product != null && product.ProductName != null)
				{
					decimal? dmlRate = null;
					decimal? dmlQty = null;
					int? iUnitId = null;
					if (product != null)
					{
						ScaleWithNoRateMethod(product, ref dmlRate, ref dmlQty, ref iUnitId);
					}
					if (product != null)
					{
						int? num = iUnitId;
						int iRowIndex = POSHelper.GetPOSDetails(iUnitId: num.HasValue ? ((object)num.GetValueOrDefault()) : AshproCommon.IdFindMethod("Select FK_UnitId From ProductBarcode Where Barcode='" + txtSearchItem.Text + "'"), sProName: AshproCommon.IdFindMethod("Select ProductUnitName From ProductUnit Where Barcode='" + txtSearchItem.Text + "'"), productEntity: product, pDetails: entPOSDetails, sPCategoryID: db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), fType: "Sales Invoice", Qty: dmlQty, Barcode: txtSearchItem.Text, dRate: dmlRate);
						entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == product.ProductCode.ToString()).FirstOrDefault();
						DgvLoadMethod(entPOSDetails);
						TotalFindMethod();
						dgItems.ClearSelection();
						dgItems.FirstDisplayedScrollingRowIndex = iRowIndex;
						dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
						dgItems.CurrentCell = dgItems.Rows[iRowIndex].Cells[col_ProductName.Name];
						txtSearchItem.Focus();
					}
				}
				else
				{
					NewProductCreatmethod();
					await AniHelper.AutoCompleteMethod(txtSearchItem);
				}
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				MessageBox.Show(ex.Message);
			}
		}

		private async void NewProductCreatmethod()
		{
			if (Messages.DialogMessage("There is no product in this Barcode. Are you want to create new product?", isYesisDefault: false))
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
				sName2 = ((sName2 == string.Empty) ? ("STAR " + txtSearchItem.Text) : sName2);
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
					Barcode = txtSearchItem.Text,
					UnitId = db.Units().FirstOrDefault().UnitId.ToInt32(),
					BrandId = db.Brands().FirstOrDefault().BrandId.ToInt32(),
					TCategoryId = iTCategoryId,
					GodwnId = db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(),
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
						PGroupId = productGroup.PGroupId
					};
					entProPrices.Add(pPrice);
				}
				AshproCommon.InsertToDatabase(entProPrices.Cast<object>().ToList());
				ProductUnit pUnit = new ProductUnit
				{
					ProductId = product.ProductId,
					UnitId = product.UnitId,
					UnitQty = 1m,
					Barcode = txtSearchItem.Text,
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
					Barcode = txtSearchItem.Text,
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
				int iRowIndex = POSHelper.GetPOSDetails(product, entPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", null, txtSearchItem.Text);
				entPosDetail = entPOSDetails.Where((POSDetails x) => x.ProductCode == txtSearchItem.Text).FirstOrDefault();
				DgvLoadMethod(entPOSDetails);
				TotalFindMethod();
				await LoadProductMethod();
				dgItems.ClearSelection();
				dgItems.FirstDisplayedScrollingRowIndex = iRowIndex;
				dgItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgItems.CurrentCell = dgItems.Rows[iRowIndex].Cells[col_ProductName.Name];
			}
			else
			{
				txtSearchItem.SelectAll();
			}
		}

		private void TotalFindMethod()
		{
			decimal num = entPOSDetails.Sum((POSDetails x) => x.NetAmount) - txtDiscount.Text.ToDecimal();
			decimal num2 = Math.Round(num, 0);
			_ = num - num2;
			dmlDiscountAmount = entPOSDetails.Sum((POSDetails x) => x.DisAmount);
			lblSubTotal.Text = entPOSDetails.Sum((POSDetails x) => x.NetAmount).ToString(CommonSettings.strCurencyFormat);
			lblTaxTotal.Text = entPOSDetails.Sum((POSDetails x) => x.TaxAmount).ToString(CommonSettings.strCurencyFormat);
			lblGrandTotal.Value = num;
			dBalance = num - (dCashPayment + dCardPayment + dOtherPayment);
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
			if (CommonSettings.blnShowSecondDisplay && lblGrandTotal.Value > 0m)
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

		private int GetProPriority(string sPriority)
		{
			int num = 3;
			return sPriority switch
			{
				"low" => 3, 
				"Medium" => 2, 
				"High" => 1, 
				_ => 3, 
			};
		}

		private void KOTPrintMethod(List<POSDetails> pOSDetails)
		{
			try
			{
				entDMaster = db.DotmatrixMasters(new
				{
					VochrType = "KOT",
					FormType = "KOT",
					isDefault = true
				}).FirstOrDefault();
				if (entDMaster != null)
				{
					List<KOTPrinter> list = new List<KOTPrinter>();
					list = db.KOTPrinters(new
					{
						FK_BranchId = Settings.Default.iBranchId
					}).ToList();
					TransactionMaster kOTMaster = GetKOTMaster(kOTOrderMaster);
					entDSettings = new List<DotmatrixSettings>();
					entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
					entPartyDtl = new PartyDetails();
					AccountEntity accountEntity = db.Accounts(new
					{
						AccountId = iPartyAccountID
					}).FirstOrDefault();
					entPartyDtl = POSHelper.GetParty(accountEntity);
					int num = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
					if (accountEntity.AGroupId == num)
					{
						entPartyDtl.Name = "Cash Customer";
					}
					{
						foreach (KOTPrinter item in list)
						{
							List<POSDetails> list2 = new List<POSDetails>();
							foreach (POSDetails x2 in pOSDetails)
							{
								if (db.ProductPrinters(new
								{
									FK_KOTPrinterId = item.Id,
									FK_BranchId = Settings.Default.iBranchId
								}).ToList().Any((ProductPrinter y) => y.FK_ProductId == x2.productId))
								{
									list2.Add(x2);
								}
							}
							if (list2.Count > 0)
							{
								PrintKOT(item, kOTMaster, list2);
							}
						}
						return;
					}
				}
				Messages.ErrorMessage("No designer created for KOT.");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void SaveKotOrder(List<KOTOrderSettings> kOTOrders)
		{
			try
			{
				if (!iKotMasterId.HasValue)
				{
					DataSet dataSet = new DataSet();
					DataTable dataTable = new DataTable();
					dataTable = AniHelper.ListToDataTable(kOTOrders);
					dataTable.TableName = "type";
					dataSet.Tables.Add(dataTable);
					iKotMasterId = AshproCommon.InsertMethod_SP(kOTOrderMaster, dataSet, "spInsertKOTOrderMaster", new
					{
						TransactionType = "insert"
					});
				}
				else
				{
					DataSet dataSet2 = new DataSet();
					DataTable dataTable2 = new DataTable();
					dataTable2 = AniHelper.ListToDataTable(kOTOrders);
					dataTable2.TableName = "type";
					dataSet2.Tables.Add(dataTable2);
					iKotMasterId = AshproCommon.InsertMethod_SP(kOTOrderMaster, dataSet2, "spInsertKOTOrderMaster", new
					{
						TransactionType = "update"
					});
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void GetKOTOrderMaster(List<KOTOrderSettings> kOTOrders, string sRemarks = "KOT")
		{
			kOTOrderMaster.FK_FYearId = iFYearId;
			kOTOrderMaster.FK_BranchId = Settings.Default.iBranchId;
			kOTOrderMaster.FK_CustomerId = iPartyAccountID;
			kOTOrderMaster.FK_StaffId = iUserId;
			kOTOrderMaster.FK_UserId = iUserId;
			kOTOrderMaster.Terminal = Utility.uTerminal;
			kOTOrderMaster.TotalQuantity = kOTOrders.Sum((KOTOrderSettings x) => x.Quantity);
			kOTOrderMaster.TotalAmount = kOTOrders.Sum((KOTOrderSettings x) => x.NETAmount);
			if (!iKotMasterId.HasValue && sRemarks != "Cancel KOT")
			{
				kOTOrderMaster.TockenNo = Settings.Default.iTockenNo;
				kOTOrderMaster.DateAndTime = DateTime.Now;
				Settings.Default.iTockenNo++;
				Settings.Default.Save();
			}
			kOTOrderMaster.CookingTime = kOTOrders.Max((KOTOrderSettings x) => x.CookingTime);
			kOTOrderMaster.SerialNo = Utility.uSerialNumber;
			kOTOrderMaster.TableId = 1;
			kOTOrderMaster.OrderType = btnFormType.Text;
			kOTOrderMaster.Remarks = sRemarks;
		}

		private List<KOTOrderSettings> GetKOTOrdersettings(List<POSDetails> pOSDetails)
		{
			List<KOTOrderSettings> list = new List<KOTOrderSettings>();
			foreach (POSDetails pOSDetail in pOSDetails)
			{
				KOTOrderSettings kOTOrderSettings = new KOTOrderSettings();
				ProductEntity productEntity = db.Products(new
				{
					ProductId = pOSDetail.productId
				}).FirstOrDefault();
				kOTOrderSettings.Barcode = pOSDetail.Barcode;
				kOTOrderSettings.ProductId = pOSDetail.productId;
				kOTOrderSettings.Quantity = pOSDetail.Qty;
				kOTOrderSettings.ItemNote = pOSDetail.ItemNote;
				kOTOrderSettings.UnitId = pOSDetail.UnitId;
				kOTOrderSettings.Rate = pOSDetail.Rate;
				kOTOrderSettings.NETAmount = pOSDetail.NetAmount;
				kOTOrderSettings.CookingTime = productEntity.CookingTime.ToInt32();
				kOTOrderSettings.ProPriority = GetProPriority(productEntity.PriorityStatus);
				list.Add(kOTOrderSettings);
			}
			return list.OrderBy((KOTOrderSettings x) => x.ProPriority).ToList();
		}

		private void PrintKOT(KOTPrinter kOTPrinter, TransactionMaster entsalesMaster, List<POSDetails> kOTOrders)
		{
			entDMaster.PrinterName = kOTPrinter.PrinterName;
			entsalesMaster.TotalQuantity = kOTOrders.Sum((POSDetails x) => x.Qty);
			entsalesMaster.KOTPrinterName = kOTPrinter.KOTName;
			if (entDMaster != null)
			{
				entsalesdtls = new List<Transaction>();
				entsalesdtls = POSHelper.GetTransactionFromPOSDetails(kOTOrders, db, "KOT", isRestuaren: true);
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entsalesMaster, entPartyDtl, entsalesdtls, entsalesMaster, null, entsalesMaster, isPrint: true, txtPrintCopy.Data, isPDF: false, isKOT: true);
				printHelper = null;
			}
			else
			{
				Messages.InformationMessage("There is no default print design. Please make your own design");
			}
		}

		private TransactionMaster GetKOTMaster(KOTOrderMaster kOTMaster)
		{
			TransactionMaster transactionMaster = new TransactionMaster();
			transactionMaster.AdditionalAmount = txtCharges.Value;
			transactionMaster.BillDiscount = entPOSDetails.Sum((POSDetails x) => x.DisAmount);
			transactionMaster.CardPayment = 0m;
			transactionMaster.CashPayment = 0m;
			transactionMaster.CessAmount = 0m;
			transactionMaster.CGSTAmount = 0m;
			transactionMaster.Changes = default(decimal);
			transactionMaster.CntrInvNo = "";
			transactionMaster.CookingTime = kOTMaster.CookingTime;
			transactionMaster.Credit = 0m;
			transactionMaster.Date = kOTMaster.DateAndTime;
			transactionMaster.FK_BranchId = kOTMaster.FK_BranchId;
			transactionMaster.FreeAmount = 0m;
			transactionMaster.Frieght = 0m;
			transactionMaster.GrandTotal = kOTMaster.TotalAmount;
			if (CommonSettings.strDefaultCurrency != string.Empty)
			{
				ToWord toWord = new ToWord(Math.Abs(transactionMaster.NetAmount), db.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency
				}).FirstOrDefault());
				transactionMaster.GrandTotalToWords = toWord.ConvertToEnglish();
				transactionMaster.GrandTotalToArabicWords = toWord.ConvertToArabic();
			}
			transactionMaster.TotalQuantity = entPOSDetails.Sum((POSDetails x) => x.Qty);
			transactionMaster.OrderNo = 0;
			transactionMaster.Tendered = default(decimal);
			transactionMaster.Changes = default(decimal);
			transactionMaster.SalesMan = db.EmployeeDetails().FirstOrDefault().EmpName;
			transactionMaster.InvoiceMode = "KOT";
			transactionMaster.Notes = kOTMaster.Notes;
			transactionMaster.TockenNo = kOTMaster.TockenNo;
			transactionMaster.KOTTitle = kOTMaster.Remarks;
			transactionMaster.SalesFormType = "Take Away";
			return transactionMaster;
		}

		private void DgvLoadMethod(List<POSDetails> pOSDetails)
		{
			dgItems.AutoGenerateColumns = false;
			dgItems.DataSource = null;
			bdsItems.DataSource = null;
			bdsItems.DataSource = entPOSDetails;
			dgItems.DataSource = bdsItems;
			TotalFindMethod();
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

		private async Task LoadProductMethod()
		{
			try
			{
				entProGroups = (from x in db.ProductGroups(new
					{
						isShowInGrid = true
					})
					orderby x.ListOrder descending
					select x).ToList();
				int itemCount = entProGroups.Count;
				decimal dcmNoofPages = (decimal)itemCount / (decimal)Settings.Default.iGroupsPerPage;
				decimal dcrounded = dcmNoofPages - Math.Round(dcmNoofPages, 0);
				if (dcrounded != 0m)
				{
					dcmNoofPages += 1m;
				}
				iGrpNoofPages = (int)dcmNoofPages;
				LoadGroupButtons();
				await ProductLoadMethod(null, true);
				btnPage.Text = sPagedetails;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void LoadGroupButtons()
		{
			flpGroup.Controls.Clear();
			foreach (ProductGroupEntity item in entProGroups.Skip((iGrpPageNo - 1) * Settings.Default.iGroupsPerPage).Take(Settings.Default.iGroupsPerPage).ToList())
			{
				GroupButtonAddMethod(item.PGroupId.toInt32(), item.PGroupName);
			}
		}

		private void GroupButtonAddMethod(int iPGrpId, string sName)
		{
			btnGroup = new Button();
			btnGroup.Name = sName;
			int num = flpGroup.Width - 5;
			if (Utility.gbValues.Name.ToInt32() < num)
			{
				num = Utility.gbValues.Name.ToInt32();
			}
			btnGroup.Text = btnGroup.Name;
			btnGroup.Tag = iPGrpId;
			btnGroup.BackColor = Settings.Default.CategoryBB;
			btnGroup.FlatStyle = FlatStyle.Flat;
			btnGroup.ForeColor = Settings.Default.CategoryBF;
			btnGroup.Size = new Size(num, Utility.gbValues.Description.ToInt32());
			btnGroup.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
			btnGroup.Click += BtnGroup_Click;
			flpGroup.Controls.Add(btnGroup);
		}

		private async Task ProductLoadMethod(int? iPGrpId, bool? isFavorate)
		{
			try
			{
				flpProduct.Controls.Clear();
				btnScroll.Tag = 1;
				List<Button> controls = await GetProductControls(iPGrpId, isFavorate);
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

		private async void BtnGroup_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			btnSelectedGroup.Text = btn.Text;
			int? iGroupId = null;
			bool? isFav = true;
			if (btn.Tag.ToInt32() > 0)
			{
				iGroupId = btn.Tag.ToInt32();
				isFav = null;
			}
			await ProductLoadMethod(iGroupId, isFav);
			iIndex = btn.TabIndex;
			btnPage.Text = sPagedetails;
			base.ActiveControl = txtSearchItem;
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

		private async Task<List<Button>> GetProductControls(int? iPGrpId, bool? isFavorate)
		{
			productPOs = new List<ExProductPOS>();
			productPOs = await db.POSProducts(new
			{
				PGroupId = iPGrpId,
				IsFavorite = isFavorate
			});
			isToUp = true;
			int itemCount = productPOs.Count;
			decimal dcmNoofPages = (decimal)itemCount / (decimal)Settings.Default.iProductsPerPage;
			decimal dcrounded = dcmNoofPages - Math.Round(dcmNoofPages, 0);
			if (dcrounded != 0m)
			{
				dcmNoofPages += 1m;
			}
			iNoofPages = (int)dcmNoofPages;
			sPagedetails = "1/" + iNoofPages;
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

		private void KOTdeletemethod()
		{
			if (AshproCommon.DeleteFromDatabase("KOTOrderSettings", "FK_KOTMasterId", iKotMasterId.toInt32()))
			{
				AshproCommon.DeleteFromDatabase("KOTOrderMaster", "KOTMasterId", iKotMasterId.toInt32());
			}
		}

		private string ItemNoteShowMethod(string notes, bool isMani = false)
		{
			try
			{
				ItemNoteView itemNoteView = new ItemNoteView(isMani);
				itemNoteView.cmbNotes.Text = notes;
				if (itemNoteView.ShowDialog() == DialogResult.OK)
				{
					return itemNoteView.cmbNotes.Text;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return string.Empty;
		}

		private void IdleTimer_Tick(object sender, EventArgs e)
		{
			if (CommonSettings.blnShowSecondDisplay && Win32.GetIdleTime() > Settings.Default.IdleTime)
			{
				SecondDisplay.ShowImageSlider();
			}
			if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty && Win32.GetIdleTime() > 30000)
			{
				AniHelper.Display(entcompany.CompanyName, "");
			}
		}

		private async void btnScroll_Click(object sender, EventArgs e)
		{
			int iPageNumber = btnScroll.Tag.ToInt32();
			if (isToUp)
			{
				if (iPageNumber < iNoofPages)
				{
					iPageNumber++;
				}
				if (iPageNumber >= iNoofPages)
				{
					isToUp = false;
				}
			}
			else
			{
				if (iPageNumber > 1)
				{
					iPageNumber--;
				}
				if (iPageNumber == 1)
				{
					isToUp = true;
				}
			}
			btnScroll.Tag = iPageNumber;
			sPagedetails = iPageNumber + "/" + iNoofPages;
			btnPage.Text = sPagedetails;
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
				txtSearchItem.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async void RestaurantPOSView_Load(object sender, EventArgs e)
		{
			try
			{
				if (CommonSettings.blnShowSecondDisplay)
				{
					if (Screen.AllScreens.Length > 1)
					{
						SecondDisplay.Show();
					}
					else
					{
						Messages.ErrorMessage("No Second Display Connected");
						CommonSettings.blnShowSecondDisplay = false;
					}
				}
				cbPrint.Checked = Settings.Default.isPrintOn;
				lblAddressOne.Text = CommonSettings.strAddressOneLabel;
				lblAddressTwo.Text = CommonSettings.strAddressTwoLabel;
				lblAddressThree.Text = CommonSettings.strAddressThreeLabel;
				lblAddressFour.Text = CommonSettings.strAddressFourLabel;
				company = db.Companies().FirstOrDefault();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entcompany.TaxId = company.GSTNo;
				entcompany.Logo = company.Logo;
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
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
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
				iPricecategoryId = db.PriceCategories().FirstOrDefault().PCategoryId.toInt32();
				entPOSDetails = new List<POSDetails>();
				await AniHelper.AutoCompleteMethod(txtCustomerName, iscustomer: true);
				SelectedFYear selectedF = db.SelectedFYears().FirstOrDefault();
				if (selectedF != null)
				{
					iFYearId = selectedF.FyearId.ToInt32();
				}
				else
				{
					iFYearId = 1;
				}
				txtInvoiceNo.Data = (db.SalesMasters(new
				{
					InvType = CommonSettings.strDefaultSalesType,
					FYearID = iFYearId,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
				HoldBillCountMethod();
				if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
				{
					AniHelper.Display(company.InstituteName, "");
				}
				await LoadProductMethod();
				MasterValue defaultSalesForm = db.MasterValues(new
				{
					MasterType = "DefaultSalesFormType"
				}).FirstOrDefault();
				if (defaultSalesForm != null)
				{
					btnFormType.Text = defaultSalesForm.Name;
				}
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnPlus_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				return;
			}
			if (entPOSDetails.Count > 0 || entPosDetail == null || entPosDetail.productId == 0)
			{
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			}
			if (txtSearchItem.Text == string.Empty)
			{
				if (entPosDetail.Qty == -1m)
				{
					entPosDetail.Qty = 1m;
				}
				else
				{
					entPosDetail.Qty += 1m;
				}
			}
			else
			{
				entPosDetail.Qty = txtSearchItem.Text.ToInt32();
				txtSearchItem.Text = string.Empty;
			}
			bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
			dgItemQtyColumnEditMethod(isInclusive);
			TotalFindMethod();
			dgItems.Refresh();
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
			if (txtSearchItem.Text == string.Empty)
			{
				if (entPosDetail.Qty > 1m)
				{
					entPosDetail.Qty--;
				}
			}
			else
			{
				entPosDetail.Rate = txtSearchItem.Text.ToDecimal();
				txtSearchItem.Text = string.Empty;
			}
			bool isInclusive = AshproCommon.IdFindMethod("Select isInclusive From Product Where ProductId = '" + entPosDetail.productId + "'");
			dgItemQtyColumnEditMethod(isInclusive);
			TotalFindMethod();
			dgItems.Refresh();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			entPOSDetails = new List<POSDetails>();
			entOldPOSDetails = new List<POSDetails>();
			kOTOrderMaster = new KOTOrderMaster();
			iKotMasterId = (iMasterId = null);
			dBalance = (POSHelper.dmlDiscount = (dmlDiscountAmount = (POSHelper.dmlRate = 0m)));
			dCashPayment = (dCardPayment = (dOtherPayment = (dChanges = 0m)));
			dgItems.DataSource = null;
			txtDiscount.Value = 0m;
			TotalFindMethod();
			txtInvoiceNo.Data = (db.SalesMasters(new
			{
				InvType = CommonSettings.strDefaultSalesType,
				FYearID = iFYearId,
				FK_BranchId = Settings.Default.iBranchId
			}).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1;
			AniHelper.PanelClearmethod(pnlTop);
			lblPhoneNumber.Text = "--";
			iPartyAccountID = iNormalCashAccountId;
			rtnInvNumber = null;
			dSubDiscount = default(decimal);
			MasterValue masterValue = db.MasterValues(new
			{
				MasterType = "DefaultSalesFormType"
			}).FirstOrDefault();
			if (masterValue != null)
			{
				btnFormType.Text = masterValue.Name;
			}
		}

		private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
		{
			new List<SalesDetails>();
			try
			{
				salesMaster = db.SalesMasters(new
				{
					InvType = CommonSettings.strDefaultSalesType,
					SalesInvNo = txtInvoiceNo.Data,
					FYearID = iFYearId,
					FK_BranchId = Settings.Default.iBranchId
				}).FirstOrDefault();
				if (salesMaster != null)
				{
					ReloadMethod(salesMaster, isSales: true);
				}
				else
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			txtInvoiceNo.Data = 1;
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			if (txtInvoiceNo.Data != 1)
			{
				txtInvoiceNo.Data--;
			}
		}

		private void btnHold_Click(object sender, EventArgs e)
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					return;
				}
				if (db.POSHoldMasters().Any((SalesMaster x) => x.SalesInvNo == txtInvoiceNo.Data))
				{
					SalesMasterMethod(db.POSHoldMasters().Max((SalesMaster x) => x.SalesInvNo + 1), iPricecategoryId, txtDiscount.Value, lblGrandTotal.Value, 0m, isPOSHold: true);
				}
				else
				{
					SalesMasterMethod(txtInvoiceNo.Data, iPricecategoryId, txtDiscount.Value, lblGrandTotal.Value, 0m, isPOSHold: true);
				}
				InvoiceHoldMethod(salesMaster, entPOSDetails);
				HoldBillCountMethod();
				btnClear_Click(null, null);
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
			txtSearchItem.Focus();
		}

		private void btnExtra_Click(object sender, EventArgs e)
		{
			List<ProductEntity> list = db.Products(new
			{
				RestaurantItemType = "Extra"
			}).ToList();
			if (list.Count > 0)
			{
				AddOns addOns = new AddOns(list);
				addOns.restaurantPOS = this;
				addOns.ShowDialog();
			}
		}

		private void btnAddons_Click(object sender, EventArgs e)
		{
			List<ProductEntity> list = db.Products(new
			{
				RestaurantItemType = "AddOns"
			}).ToList();
			if (list.Count > 0)
			{
				AddOns addOns = new AddOns(list);
				addOns.restaurantPOS = this;
				addOns.ShowDialog();
			}
		}

		private void btnKOT_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				Messages.WarningMessage();
				return;
			}
			if (!iKotMasterId.HasValue)
			{
				List<KOTOrderSettings> kOTOrdersettings = GetKOTOrdersettings(entPOSDetails);
				GetKOTOrderMaster(kOTOrdersettings);
				SaveKotOrder(kOTOrdersettings);
				KOTPrintMethod(entPOSDetails);
			}
			else
			{
				List<POSDetails> list = new List<POSDetails>();
				List<POSDetails> list2 = new List<POSDetails>();
				foreach (POSDetails item in entOldPOSDetails)
				{
					if (!entPOSDetails.Any((POSDetails x) => x.Barcode == item.Barcode))
					{
						item.ProductName += "-Cancelled";
						list.Add(item);
						continue;
					}
					POSDetails pOSDetails = entPOSDetails.FirstOrDefault((POSDetails x) => x.Barcode == item.Barcode);
					if (item.Qty > pOSDetails.Qty)
					{
						decimal num2 = (pOSDetails.Qty = item.Qty - pOSDetails.Qty);
						pOSDetails.ProductName += "-Cancelled";
						list.Add(pOSDetails);
					}
				}
				foreach (POSDetails item2 in entPOSDetails)
				{
					if (!entOldPOSDetails.Any((POSDetails x) => x.Barcode == item2.Barcode))
					{
						list2.Add(item2);
						continue;
					}
					POSDetails pOSDetails2 = entOldPOSDetails.FirstOrDefault((POSDetails x) => x.Barcode == item2.Barcode);
					if (item2.Qty > pOSDetails2.Qty)
					{
						decimal num4 = (pOSDetails2.Qty = item2.Qty - pOSDetails2.Qty);
						list2.Add(pOSDetails2);
					}
				}
				List<KOTOrderSettings> kOTOrdersettings2 = GetKOTOrdersettings(entPOSDetails);
				GetKOTOrderMaster(kOTOrdersettings2);
				SaveKotOrder(kOTOrdersettings2);
				KOTPrintMethod(list2);
				iKotMasterId = null;
				List<KOTOrderSettings> kOTOrdersettings3 = GetKOTOrdersettings(list);
				GetKOTOrderMaster(kOTOrdersettings3, "Cancel KOT");
				SaveKotOrder(kOTOrdersettings3);
				KOTPrintMethod(list);
			}
			btnClear.PerformClick();
		}

		private void btnNotes_Click(object sender, EventArgs e)
		{
			string notes = ItemNoteShowMethod(kOTOrderMaster.Notes, isMani: true);
			kOTOrderMaster.Notes = notes;
		}

		private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					POSDetails pOSDetails = (POSDetails)dgItems.CurrentRow.DataBoundItem;
					pOSDetails.ItemNote = ItemNoteShowMethod(pOSDetails.ItemNote);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnKotLookUp_Click(object sender, EventArgs e)
		{
			List<KOTOrderSettings> list = new List<KOTOrderSettings>();
			entPOSDetails = new List<POSDetails>();
			entOldPOSDetails = new List<POSDetails>();
			POSFormSearch pOSFormSearch = new POSFormSearch();
			pOSFormSearch.searchType = "KOT";
			pOSFormSearch.FormType = CommonSettings.strDefaultSalesType;
			if (pOSFormSearch.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			iKotMasterId = pOSFormSearch.SalesMaster.SalesInvId;
			if (!iKotMasterId.HasValue)
			{
				return;
			}
			kOTOrderMaster = db.KOTOrderMasters(new
			{
				KOTMasterId = iKotMasterId
			}).FirstOrDefault();
			list = db.KOTOrderSettingss(new
			{
				FK_KOTMasterId = iKotMasterId
			}).ToList();
			foreach (KOTOrderSettings item in list)
			{
				ProductBarcodeEntity productBarcodeEntity = db.ProductBarcodes(new { item.Barcode }).FirstOrDefault();
				ProductEntity productEntity = db.Products(new
				{
					ProductId = item.ProductId.toInt32()
				}).FirstOrDefault();
				string text = productBarcodeEntity.ProductBarcodeName;
				if (text == string.Empty)
				{
					text = productEntity.ProductName;
				}
				POSHelper.GetPOSDetails(productEntity, entPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", item.Quantity, item.Barcode, item.UnitId, item.Rate, text);
				POSHelper.GetPOSDetails(productEntity, entOldPOSDetails, db.TaxCategories().FirstOrDefault().TCategoryId.toInt32(), "Sales Invoice", item.Quantity, item.Barcode, item.UnitId, item.Rate, text);
			}
			DgvLoadMethod(entPOSDetails);
			TotalFindMethod();
		}

		private void btnReprint_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				Messages.WarningMessage();
				return;
			}
			List<KOTOrderSettings> kOTOrdersettings = GetKOTOrdersettings(entPOSDetails);
			GetKOTOrderMaster(kOTOrdersettings);
			KOTPrintMethod(entPOSDetails);
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
			entPosDetail = new POSDetails();
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			txtSearchItem.Focus();
		}

		private void btnDown_Click(object sender, EventArgs e)
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
			entPosDetail = new POSDetails();
			entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
			txtSearchItem.Focus();
		}

		private void txtCustomerName_Validated(object sender, EventArgs e)
		{
			if (!(txtCustomerName.Text == string.Empty))
			{
				AccountEntity account = db.Accounts(new
				{
					AccountName = txtCustomerName.Text
				}).FirstOrDefault();
				CustomerLoadMethod(account);
			}
		}

		private void CustomerLoadMethod(AccountEntity account)
		{
			if (account != null)
			{
				iPartyAccountID = account.AccountId.toInt32();
				lblPhoneNumber.Text = account.PhoneOne;
				txtRoad.Text = account.AddressOne;
				txtBuilding.Text = account.AddressTwo;
				txtBlock.Text = account.AddressThree;
				txtFlat.Text = account.AddressFour;
			}
		}

		private async void btnCustomer_Click(object sender, EventArgs e)
		{
			Account account = new Account();
			UserPrevilegeDtl previlegeDtl = AniHelper.GetPrevilageMethod(null, "Customer");
			account.sFormName = MessageKeys.Customers;
			account.accountGroupName = "CUSTOMER/SUNDRY DEBTORS";
			account.plgDtls = previlegeDtl;
			account.isOpenFromAnotherForm = true;
			if (account.ShowDialog() == DialogResult.OK)
			{
				AccountEntity accountEntity = db.Accounts(new
				{
					AccountId = account.Id
				}).FirstOrDefault();
				if (accountEntity != null)
				{
					lblPhoneNumber.Text = accountEntity.PhoneOne;
					txtCustomerName.Text = accountEntity.AccountName;
					txtRoad.Text = accountEntity.AddressOne;
					txtBuilding.Text = accountEntity.AddressTwo;
					txtBlock.Text = accountEntity.AddressThree;
					txtFlat.Text = accountEntity.AddressFour;
					iPartyAccountID = account.Id;
				}
			}
			await AniHelper.AutoCompleteMethod(txtCustomerName, iscustomer: true);
		}

		private async void btnfastSale_Click(object sender, EventArgs e)
		{
			try
			{
				if (entPOSDetails.Count == 0)
				{
					txtSearchItem.Focus();
					return;
				}
				if (iFYearId == 0)
				{
					iFYearId = db.SelectedFYears().FirstOrDefault().FyearId.toInt32();
				}
				isReturn = ((lblGrandTotal.Value < 0m) ? true : false);
				int pCatId = db.PriceCategories().FirstOrDefault().PCategoryId.toInt32();
				decimal dAddDisc = txtDiscount.Value;
				decimal dGrand = lblGrandTotal.Value;
				GodownEntity godown = db.Godowns().FirstOrDefault();
				string sDepotName = godown.GodownName;
				int iDepotId = godown.GodwnId.toInt32();
				int Rtn = (isReturn ? 1 : 0);
				await Task.Run(delegate
				{
					SaveMetod(pCatId, dAddDisc, dGrand, dBalance, iDepotId, sDepotName, Rtn);
				});
				if (cbPrint.Checked)
				{
					PrintInvoiceMethod();
				}
				if (!iKotMasterId.HasValue)
				{
					List<KOTOrderSettings> kOTOrders = GetKOTOrdersettings(entPOSDetails);
					GetKOTOrderMaster(kOTOrders);
					KOTPrintMethod(entPOSDetails);
				}
				else
				{
					KOTdeletemethod();
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
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
		}

		private void btnFormType_Click(object sender, EventArgs e)
		{
			SalesFormType salesFormType = new SalesFormType();
			if (salesFormType.ShowDialog() == DialogResult.OK)
			{
				btnFormType.Text = salesFormType.sFormType;
			}
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.isPrintOn = cbPrint.Checked;
			Settings.Default.Save();
		}

		private void txtPrintCopy_Validated(object sender, EventArgs e)
		{
			if (txtPrintCopy.Data > 0)
			{
				Settings.Default.iPrintCopy = txtPrintCopy.Data;
				Settings.Default.Save();
			}
		}

		private void btnCustomerHistory_Click(object sender, EventArgs e)
		{
			CustomerHistoryView customerHistoryView = new CustomerHistoryView();
			customerHistoryView.CustomerId = iPartyAccountID;
			customerHistoryView.ShowDialog();
		}

		private void RestaurantPOSView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Z && e.Alt && e.Control)
			{
				dashboardPOS.tsmChangeTheme_Click(null, null);
			}
			else if (e.KeyCode == Keys.F5)
			{
				btnDiscount.PerformClick();
			}
			else if (e.KeyCode == Keys.Space)
			{
				if (txtSearchItem.Text == string.Empty && base.ActiveControl == txtSearchItem)
				{
					btnPayment_Click(null, null);
				}
			}
			else if (e.KeyCode == Keys.F8)
			{
				if (entPosDetail != null)
				{
					AniHelper.ItemCreationMethod(entPosDetail.productId);
				}
				else
				{
					AniHelper.ItemCreationMethod();
				}
			}
		}

		private void btnModify_Click(object sender, EventArgs e)
		{
			if (!iKotMasterId.HasValue)
			{
				return;
			}
			if (entOldPOSDetails.Count == 0)
			{
				Messages.WarningMessage();
				return;
			}
			foreach (POSDetails entOldPOSDetail in entOldPOSDetails)
			{
				entOldPOSDetail.ProductName += "-Cancelled";
			}
			kOTOrderMaster.Remarks = "Cancel KOT";
			KOTPrintMethod(entOldPOSDetails);
			KOTdeletemethod();
			btnClear.PerformClick();
		}

		private void btnVoid_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count != 0 && dgItems.CurrentRow != null)
			{
				entPosDetail = (POSDetails)dgItems.CurrentRow.DataBoundItem;
				entPOSDetails.Remove(entPosDetail);
				for (int i = 0; i < entPOSDetails.Count; i++)
				{
					entPOSDetails[i].SL = i + 1;
				}
				DgvLoadMethod(entPOSDetails);
				if (entPOSDetails.Count == 0 && CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
				{
					AniHelper.Display(company.InstituteName, "");
				}
				txtSearchItem.Focus();
			}
		}

		private void RestaurantPOSView_VisibleChanged(object sender, EventArgs e)
		{
			ColorChangeMethod();
		}

		private void btnBarcode_Click(object sender, EventArgs e)
		{
			if (rbBarcode.Checked)
			{
				BarcodeAddmethod();
			}
			else
			{
				BarcodeMethodWithName();
			}
			txtSearchItem.Text = string.Empty;
		}

		private void txtSearchItem_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnBarcode_Click(null, null);
			}
			else if (e.KeyCode == Keys.Add)
			{
				btnPlus_Click(null, null);
			}
			else if (e.KeyCode == Keys.Subtract)
			{
				btnMinus_Click(null, null);
			}
		}

		private async void rbName_CheckedChanged(object sender, EventArgs e)
		{
			txtSearchItem.isNumberOnly = rbBarcode.Checked;
			if (rbName.Checked)
			{
				await AniHelper.AutoCompleteMethod(txtSearchItem);
			}
			else
			{
				txtSearchItem.AutoCompleteMode = AutoCompleteMode.None;
			}
		}

		private void txtDiscount_Validated(object sender, EventArgs e)
		{
			lblGrandTotal.Value = lblSubTotal.Text.ToDecimal() - txtDiscount.Value;
			dBalance = lblGrandTotal.Value - (dCashPayment + dCardPayment + dOtherPayment);
			txtSearchItem.Focus();
		}

		private void rbBarcode_CheckedChanged(object sender, EventArgs e)
		{
			txtSearchItem.isNumberOnly = rbBarcode.Checked;
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
				txtSearchItem.Focus();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0 || !Messages.DialogMessage("Are you want mark the invoice as Sales Return ?"))
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

		private void btnPayment_Click(object sender, EventArgs e)
		{
			if (entPOSDetails.Count == 0)
			{
				return;
			}
			if (CommonSettings.blnEnableCutomerDisplay && Settings.Default.sCom != string.Empty)
			{
				AniHelper.Display("Total : " + lblGrandTotal.Value.ToString(CommonSettings.strCurencyFormat), "");
			}
			if (CommonSettings.blnShowSecondDisplay)
			{
				SecondDisplay.ShowTotalAmountOnly("Total : " + lblGrandTotal.Value.ToString(CommonSettings.strCurencyFormat));
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
			pOSCashPayment.iPartyAccountId = iPartyAccountID;
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
				}
				else
				{
					dChanges = default(decimal);
					dBalance = num6;
				}
			}
			else
			{
				dBalance = num6;
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

		private void btnNext_Click(object sender, EventArgs e)
		{
			int num = 1;
			num = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = CommonSettings.strDefaultSalesType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
			if (txtInvoiceNo.Data < num)
			{
				txtInvoiceNo.Data++;
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			txtInvoiceNo.Data = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = CommonSettings.strDefaultSalesType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
		}

		private void btnShowToppanel_Click(object sender, EventArgs e)
		{
			bool flag = !pnlTop.Visible;
			Panel panel = pnlTop;
			bool visible = (Settings.Default.IsTopPanelShow = flag);
			panel.Visible = visible;
			btnShowToppanel.Text = (flag ? "Hide" : "Show");
			Settings.Default.Save();
		}

		private void btnShowNumberpad_Click(object sender, EventArgs e)
		{
			bool flag = !pnlNumberpad.Visible;
			Panel panel = pnlNumberpad;
			bool visible = (Settings.Default.IsNumPanelShow = flag);
			panel.Visible = visible;
			btnShowNumberpad.Text = (flag ? "<<" : ">>");
			Settings.Default.Save();
		}

		private void btnUpGroup_Click(object sender, EventArgs e)
		{
			if (iGrpPageNo > 1)
			{
				iGrpPageNo--;
			}
			LoadGroupButtons();
		}

		private void btnDownGroup_Click(object sender, EventArgs e)
		{
			if (iGrpPageNo < iGrpNoofPages)
			{
				iGrpPageNo++;
			}
			LoadGroupButtons();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestaurantPOSView));
            this.pnlSide = new System.Windows.Forms.Panel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.txtCharges = new AshUsercontrol.AshTextBoxNormal();
            this.txtDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGrandTotal = new AshUsercontrol.AshNumericLabel();
            this.lblTaxTotal = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlKOT = new System.Windows.Forms.Panel();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnKOT = new System.Windows.Forms.Button();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnExtra = new System.Windows.Forms.Button();
            this.btnVoid = new System.Windows.Forms.Button();
            this.btnAddons = new System.Windows.Forms.Button();
            this.pnlNumberpad = new System.Windows.Forms.Panel();
            this.txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
            this.cbPrint = new System.Windows.Forms.CheckBox();
            this.rbName = new System.Windows.Forms.RadioButton();
            this.rbBarcode = new System.Windows.Forms.RadioButton();
            this.restaurantKeyboard1 = new AshUsercontrol.RestaurantKeyboard();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.txtSearchItem = new AshUsercontrol.AshTextBoxNormal();
            this.pnlInvoice = new System.Windows.Forms.Panel();
            this.txtInvoiceNo = new AshUsercontrol.AshTextBoxNormal();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flpProduct = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlProductTop = new System.Windows.Forms.Panel();
            this.btnSelectedGroup = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnPage = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnScroll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flpGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDownGroup = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnShowToppanel = new System.Windows.Forms.Button();
            this.btnFavourite = new System.Windows.Forms.Button();
            this.btnUpGroup = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustomerName = new AshUsercontrol.AshTextBoxNormal();
            this.pnlBuilding = new System.Windows.Forms.Panel();
            this.txtBuilding = new AshUsercontrol.AshTextBoxNormal();
            this.txtRoad = new AshUsercontrol.AshTextBoxNormal();
            this.lblAddressTwo = new System.Windows.Forms.Label();
            this.lblAddressOne = new System.Windows.Forms.Label();
            this.pnlBlock = new System.Windows.Forms.Panel();
            this.txtBlock = new AshUsercontrol.AshTextBoxNormal();
            this.lblAddressThree = new System.Windows.Forms.Label();
            this.txtFlat = new AshUsercontrol.AshTextBoxNormal();
            this.lblAddressFour = new System.Windows.Forms.Label();
            this.pnlExtra = new System.Windows.Forms.Panel();
            this.pnlMainMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCustomerHistory = new System.Windows.Forms.Button();
            this.btnCashDrawer = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnRetrieveBill = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnKotLookUp = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnFormType = new System.Windows.Forms.Button();
            this.btnfastSale = new System.Windows.Forms.Button();
            this.btnShowNumberpad = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bdsItems = new System.Windows.Forms.BindingSource(this.components);
            this.pnlSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.pnlKOT.SuspendLayout();
            this.pnlNumberpad.SuspendLayout();
            this.pnlInvoice.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlProductTop.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlBuilding.SuspendLayout();
            this.pnlBlock.SuspendLayout();
            this.pnlMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSide
            // 
            this.pnlSide.Controls.Add(this.dgItems);
            this.pnlSide.Controls.Add(this.pnlDetails);
            this.pnlSide.Controls.Add(this.pnlKOT);
            this.pnlSide.Controls.Add(this.pnlNumberpad);
            this.pnlSide.Controls.Add(this.pnlInvoice);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(353, 721);
            this.pnlSide.TabIndex = 0;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgItems.ColumnHeadersHeight = 40;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ProductName,
            this.col_Quantity,
            this.col_Price,
            this.col_Amount});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.EnableHeadersVisualStyles = false;
            this.dgItems.Location = new System.Drawing.Point(0, 37);
            this.dgItems.Name = "dgItems";
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeadersWidth = 51;
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItems.Size = new System.Drawing.Size(353, 302);
            this.dgItems.TabIndex = 2;
            this.dgItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellContentClick);
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            this.col_SL.HeaderText = "#";
            this.col_SL.MinimumWidth = 6;
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            this.col_SL.Width = 30;
            // 
            // col_ProductName
            // 
            this.col_ProductName.DataPropertyName = "ProductName";
            this.col_ProductName.HeaderText = "Description";
            this.col_ProductName.MinimumWidth = 6;
            this.col_ProductName.Name = "col_ProductName";
            this.col_ProductName.ReadOnly = true;
            this.col_ProductName.Width = 140;
            // 
            // col_Quantity
            // 
            this.col_Quantity.DataPropertyName = "Qty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Quantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_Quantity.HeaderText = "Quantity";
            this.col_Quantity.MinimumWidth = 6;
            this.col_Quantity.Name = "col_Quantity";
            this.col_Quantity.ReadOnly = true;
            this.col_Quantity.Width = 60;
            // 
            // col_Price
            // 
            this.col_Price.DataPropertyName = "Rate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Price.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_Price.HeaderText = "Price";
            this.col_Price.MinimumWidth = 6;
            this.col_Price.Name = "col_Price";
            this.col_Price.ReadOnly = true;
            this.col_Price.Width = 60;
            // 
            // col_Amount
            // 
            this.col_Amount.DataPropertyName = "NetAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Amount.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_Amount.HeaderText = "Amount";
            this.col_Amount.MinimumWidth = 6;
            this.col_Amount.Name = "col_Amount";
            this.col_Amount.ReadOnly = true;
            this.col_Amount.Width = 60;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.btnDiscount);
            this.pnlDetails.Controls.Add(this.txtCharges);
            this.pnlDetails.Controls.Add(this.txtDiscount);
            this.pnlDetails.Controls.Add(this.label3);
            this.pnlDetails.Controls.Add(this.label2);
            this.pnlDetails.Controls.Add(this.lblGrandTotal);
            this.pnlDetails.Controls.Add(this.lblTaxTotal);
            this.pnlDetails.Controls.Add(this.lblSubTotal);
            this.pnlDetails.Controls.Add(this.label7);
            this.pnlDetails.Controls.Add(this.label1);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetails.Location = new System.Drawing.Point(0, 339);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(353, 81);
            this.pnlDetails.TabIndex = 3;
            // 
            // btnDiscount
            // 
            this.btnDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.btnDiscount.FlatAppearance.BorderSize = 0;
            this.btnDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscount.ForeColor = System.Drawing.Color.White;
            this.btnDiscount.Location = new System.Drawing.Point(327, 3);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(22, 23);
            this.btnDiscount.TabIndex = 2;
            this.btnDiscount.Text = "..";
            this.btnDiscount.UseVisualStyleBackColor = false;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // txtCharges
            // 
            this.txtCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtCharges.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges.Data = 0;
            this.txtCharges.EnterColor = System.Drawing.Color.LightPink;
            this.txtCharges.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtCharges.ForeColor = System.Drawing.Color.White;
            this.txtCharges.Format = null;
            this.txtCharges.isAllowNegative = false;
            this.txtCharges.isAllowSpecialChar = false;
            this.txtCharges.isNumberOnly = false;
            this.txtCharges.isNumeric = true;
            this.txtCharges.LeaveColor = System.Drawing.Color.White;
            this.txtCharges.Location = new System.Drawing.Point(92, 56);
            this.txtCharges.Margin = new System.Windows.Forms.Padding(4);
            this.txtCharges.Multiline = true;
            this.txtCharges.Name = "txtCharges";
            this.txtCharges.Size = new System.Drawing.Size(85, 21);
            this.txtCharges.TabIndex = 1;
            this.txtCharges.Text = "0";
            this.txtCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCharges.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCharges.WaterMark = "0.000";
            // 
            // txtDiscount
            // 
            this.txtDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscount.Data = 0;
            this.txtDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtDiscount.ForeColor = System.Drawing.Color.White;
            this.txtDiscount.Format = null;
            this.txtDiscount.isAllowNegative = false;
            this.txtDiscount.isAllowSpecialChar = false;
            this.txtDiscount.isNumberOnly = false;
            this.txtDiscount.isNumeric = true;
            this.txtDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtDiscount.Location = new System.Drawing.Point(234, 5);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscount.Multiline = true;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(89, 21);
            this.txtDiscount.TabIndex = 1;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDiscount.WaterMark = "0.000";
            this.txtDiscount.Validated += new System.EventHandler(this.txtDiscount_Validated);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Charges";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Vat";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblGrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.White;
            this.lblGrandTotal.Format = null;
            this.lblGrandTotal.Location = new System.Drawing.Point(180, 29);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(171, 50);
            this.lblGrandTotal.TabIndex = 0;
            this.lblGrandTotal.Text = "0.000";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrandTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            196608});
            // 
            // lblTaxTotal
            // 
            this.lblTaxTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblTaxTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblTaxTotal.ForeColor = System.Drawing.Color.White;
            this.lblTaxTotal.Location = new System.Drawing.Point(91, 29);
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.lblTaxTotal.Size = new System.Drawing.Size(87, 23);
            this.lblTaxTotal.TabIndex = 0;
            this.lblTaxTotal.Text = "0.000";
            this.lblTaxTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblSubTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblSubTotal.ForeColor = System.Drawing.Color.White;
            this.lblSubTotal.Location = new System.Drawing.Point(91, 3);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(87, 23);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "0.000";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(180, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Disc";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sub Total";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKOT
            // 
            this.pnlKOT.Controls.Add(this.btnPayment);
            this.pnlKOT.Controls.Add(this.btnReprint);
            this.pnlKOT.Controls.Add(this.btnClear);
            this.pnlKOT.Controls.Add(this.btnKOT);
            this.pnlKOT.Controls.Add(this.btnNotes);
            this.pnlKOT.Controls.Add(this.btnModify);
            this.pnlKOT.Controls.Add(this.btnExtra);
            this.pnlKOT.Controls.Add(this.btnVoid);
            this.pnlKOT.Controls.Add(this.btnAddons);
            this.pnlKOT.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlKOT.Location = new System.Drawing.Point(0, 420);
            this.pnlKOT.Name = "pnlKOT";
            this.pnlKOT.Size = new System.Drawing.Size(353, 182);
            this.pnlKOT.TabIndex = 4;
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.Maroon;
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnPayment.ForeColor = System.Drawing.Color.White;
            this.btnPayment.Location = new System.Drawing.Point(238, 4);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(112, 55);
            this.btnPayment.TabIndex = 3;
            this.btnPayment.Text = "Pay";
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.BackColor = System.Drawing.Color.Maroon;
            this.btnReprint.FlatAppearance.BorderSize = 0;
            this.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnReprint.ForeColor = System.Drawing.Color.White;
            this.btnReprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReprint.Location = new System.Drawing.Point(238, 122);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(112, 55);
            this.btnReprint.TabIndex = 3;
            this.btnReprint.Text = "Re-Print";
            this.btnReprint.UseVisualStyleBackColor = false;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Maroon;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(2, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 55);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnKOT
            // 
            this.btnKOT.BackColor = System.Drawing.Color.Maroon;
            this.btnKOT.FlatAppearance.BorderSize = 0;
            this.btnKOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnKOT.ForeColor = System.Drawing.Color.White;
            this.btnKOT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKOT.Location = new System.Drawing.Point(120, 4);
            this.btnKOT.Name = "btnKOT";
            this.btnKOT.Size = new System.Drawing.Size(112, 55);
            this.btnKOT.TabIndex = 3;
            this.btnKOT.Text = "KOT";
            this.btnKOT.UseVisualStyleBackColor = false;
            this.btnKOT.Click += new System.EventHandler(this.btnKOT_Click);
            // 
            // btnNotes
            // 
            this.btnNotes.BackColor = System.Drawing.Color.Maroon;
            this.btnNotes.FlatAppearance.BorderSize = 0;
            this.btnNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNotes.ForeColor = System.Drawing.Color.White;
            this.btnNotes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNotes.Location = new System.Drawing.Point(238, 62);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(112, 55);
            this.btnNotes.TabIndex = 3;
            this.btnNotes.Text = "Notes";
            this.btnNotes.UseVisualStyleBackColor = false;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackColor = System.Drawing.Color.Maroon;
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModify.Location = new System.Drawing.Point(120, 122);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(112, 55);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "Cancel KOT";
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnExtra
            // 
            this.btnExtra.BackColor = System.Drawing.Color.Maroon;
            this.btnExtra.FlatAppearance.BorderSize = 0;
            this.btnExtra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExtra.ForeColor = System.Drawing.Color.White;
            this.btnExtra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExtra.Location = new System.Drawing.Point(2, 62);
            this.btnExtra.Name = "btnExtra";
            this.btnExtra.Size = new System.Drawing.Size(112, 55);
            this.btnExtra.TabIndex = 3;
            this.btnExtra.Text = "Extra";
            this.btnExtra.UseVisualStyleBackColor = false;
            this.btnExtra.Click += new System.EventHandler(this.btnExtra_Click);
            // 
            // btnVoid
            // 
            this.btnVoid.BackColor = System.Drawing.Color.Maroon;
            this.btnVoid.FlatAppearance.BorderSize = 0;
            this.btnVoid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnVoid.ForeColor = System.Drawing.Color.White;
            this.btnVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoid.Location = new System.Drawing.Point(2, 122);
            this.btnVoid.Name = "btnVoid";
            this.btnVoid.Size = new System.Drawing.Size(112, 55);
            this.btnVoid.TabIndex = 3;
            this.btnVoid.Text = "Void Item";
            this.btnVoid.UseVisualStyleBackColor = false;
            this.btnVoid.Click += new System.EventHandler(this.btnVoid_Click);
            // 
            // btnAddons
            // 
            this.btnAddons.BackColor = System.Drawing.Color.Maroon;
            this.btnAddons.FlatAppearance.BorderSize = 0;
            this.btnAddons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddons.ForeColor = System.Drawing.Color.White;
            this.btnAddons.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddons.Location = new System.Drawing.Point(120, 62);
            this.btnAddons.Name = "btnAddons";
            this.btnAddons.Size = new System.Drawing.Size(112, 55);
            this.btnAddons.TabIndex = 3;
            this.btnAddons.Text = "Add Ons";
            this.btnAddons.UseVisualStyleBackColor = false;
            this.btnAddons.Click += new System.EventHandler(this.btnAddons_Click);
            // 
            // pnlNumberpad
            // 
            this.pnlNumberpad.BackColor = System.Drawing.SystemColors.Control;
            this.pnlNumberpad.Controls.Add(this.txtPrintCopy);
            this.pnlNumberpad.Controls.Add(this.cbPrint);
            this.pnlNumberpad.Controls.Add(this.rbName);
            this.pnlNumberpad.Controls.Add(this.rbBarcode);
            this.pnlNumberpad.Controls.Add(this.restaurantKeyboard1);
            this.pnlNumberpad.Controls.Add(this.btnBarcode);
            this.pnlNumberpad.Controls.Add(this.txtSearchItem);
            this.pnlNumberpad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNumberpad.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.pnlNumberpad.Location = new System.Drawing.Point(0, 602);
            this.pnlNumberpad.Name = "pnlNumberpad";
            this.pnlNumberpad.Size = new System.Drawing.Size(353, 119);
            this.pnlNumberpad.TabIndex = 1;
            this.pnlNumberpad.Visible = false;
            // 
            // txtPrintCopy
            // 
            this.txtPrintCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPrintCopy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrintCopy.Data = 1;
            this.txtPrintCopy.EnterColor = System.Drawing.Color.LightPink;
            this.txtPrintCopy.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPrintCopy.Format = null;
            this.txtPrintCopy.isAllowNegative = false;
            this.txtPrintCopy.isAllowSpecialChar = false;
            this.txtPrintCopy.isNumberOnly = true;
            this.txtPrintCopy.isNumeric = false;
            this.txtPrintCopy.LeaveColor = System.Drawing.Color.White;
            this.txtPrintCopy.Location = new System.Drawing.Point(292, 30);
            this.txtPrintCopy.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtPrintCopy.Name = "txtPrintCopy";
            this.txtPrintCopy.Size = new System.Drawing.Size(51, 16);
            this.txtPrintCopy.TabIndex = 121;
            this.txtPrintCopy.TabStop = false;
            this.txtPrintCopy.Text = "1";
            this.txtPrintCopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrintCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPrintCopy.WaterMark = null;
            this.txtPrintCopy.Validated += new System.EventHandler(this.txtPrintCopy_Validated);
            // 
            // cbPrint
            // 
            this.cbPrint.AutoSize = true;
            this.cbPrint.Checked = true;
            this.cbPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbPrint.Location = new System.Drawing.Point(292, 7);
            this.cbPrint.Name = "cbPrint";
            this.cbPrint.Size = new System.Drawing.Size(57, 20);
            this.cbPrint.TabIndex = 115;
            this.cbPrint.Text = "Print";
            this.cbPrint.UseVisualStyleBackColor = true;
            this.cbPrint.CheckedChanged += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // rbName
            // 
            this.rbName.AutoSize = true;
            this.rbName.Location = new System.Drawing.Point(94, 6);
            this.rbName.Name = "rbName";
            this.rbName.Size = new System.Drawing.Size(62, 20);
            this.rbName.TabIndex = 3;
            this.rbName.Text = "Name";
            this.rbName.UseVisualStyleBackColor = true;
            this.rbName.CheckedChanged += new System.EventHandler(this.rbName_CheckedChanged);
            // 
            // rbBarcode
            // 
            this.rbBarcode.AutoSize = true;
            this.rbBarcode.Checked = true;
            this.rbBarcode.Location = new System.Drawing.Point(8, 7);
            this.rbBarcode.Name = "rbBarcode";
            this.rbBarcode.Size = new System.Drawing.Size(79, 20);
            this.rbBarcode.TabIndex = 3;
            this.rbBarcode.TabStop = true;
            this.rbBarcode.Text = "Barcode";
            this.rbBarcode.UseVisualStyleBackColor = true;
            this.rbBarcode.CheckedChanged += new System.EventHandler(this.rbBarcode_CheckedChanged);
            // 
            // restaurantKeyboard1
            // 
            this.restaurantKeyboard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.restaurantKeyboard1.Location = new System.Drawing.Point(4, 47);
            this.restaurantKeyboard1.Margin = new System.Windows.Forms.Padding(4);
            this.restaurantKeyboard1.Name = "restaurantKeyboard1";
            this.restaurantKeyboard1.Size = new System.Drawing.Size(345, 68);
            this.restaurantKeyboard1.TabIndex = 2;
            // 
            // btnBarcode
            // 
            this.btnBarcode.Location = new System.Drawing.Point(224, 4);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(62, 43);
            this.btnBarcode.TabIndex = 122;
            this.btnBarcode.Text = "..";
            this.btnBarcode.UseVisualStyleBackColor = true;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchItem.Data = 0;
            this.txtSearchItem.EnterColor = System.Drawing.Color.LightPink;
            this.txtSearchItem.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtSearchItem.Format = null;
            this.txtSearchItem.isAllowNegative = false;
            this.txtSearchItem.isAllowSpecialChar = false;
            this.txtSearchItem.isNumberOnly = true;
            this.txtSearchItem.isNumeric = false;
            this.txtSearchItem.LeaveColor = System.Drawing.Color.White;
            this.txtSearchItem.Location = new System.Drawing.Point(12, 28);
            this.txtSearchItem.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.Size = new System.Drawing.Size(208, 16);
            this.txtSearchItem.TabIndex = 0;
            this.txtSearchItem.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSearchItem.WaterMark = "Barcode/Product Name";
            this.txtSearchItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchItem_KeyDown);
            // 
            // pnlInvoice
            // 
            this.pnlInvoice.BackColor = System.Drawing.SystemColors.Control;
            this.pnlInvoice.Controls.Add(this.txtInvoiceNo);
            this.pnlInvoice.Controls.Add(this.btnLast);
            this.pnlInvoice.Controls.Add(this.btnNext);
            this.pnlInvoice.Controls.Add(this.btnPrevious);
            this.pnlInvoice.Controls.Add(this.btnFirst);
            this.pnlInvoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInvoice.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.pnlInvoice.Location = new System.Drawing.Point(0, 0);
            this.pnlInvoice.Name = "pnlInvoice";
            this.pnlInvoice.Size = new System.Drawing.Size(353, 37);
            this.pnlInvoice.TabIndex = 0;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvoiceNo.Data = 1;
            this.txtInvoiceNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtInvoiceNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtInvoiceNo.Format = null;
            this.txtInvoiceNo.isAllowNegative = false;
            this.txtInvoiceNo.isAllowSpecialChar = false;
            this.txtInvoiceNo.isNumberOnly = true;
            this.txtInvoiceNo.isNumeric = false;
            this.txtInvoiceNo.LeaveColor = System.Drawing.Color.White;
            this.txtInvoiceNo.Location = new System.Drawing.Point(83, 12);
            this.txtInvoiceNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(179, 16);
            this.txtInvoiceNo.TabIndex = 4;
            this.txtInvoiceNo.Text = "1";
            this.txtInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInvoiceNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInvoiceNo.WaterMark = null;
            this.txtInvoiceNo.TextChanged += new System.EventHandler(this.txtInvoiceNo_TextChanged);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(303, 4);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(41, 30);
            this.btnLast.TabIndex = 1;
            this.btnLast.Text = ">>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(269, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 30);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(46, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(30, 30);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(8, 4);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(34, 30);
            this.btnFirst.TabIndex = 1;
            this.btnFirst.Text = "<<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(353, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(947, 721);
            this.pnlMain.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(947, 721);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flpProduct);
            this.tabPage1.Controls.Add(this.pnlProductTop);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.pnlTop);
            this.tabPage1.Controls.Add(this.pnlMainMenu);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(939, 712);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flpProduct
            // 
            this.flpProduct.AutoScroll = true;
            this.flpProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProduct.Location = new System.Drawing.Point(89, 106);
            this.flpProduct.Name = "flpProduct";
            this.flpProduct.Size = new System.Drawing.Size(756, 603);
            this.flpProduct.TabIndex = 3;
            // 
            // pnlProductTop
            // 
            this.pnlProductTop.Controls.Add(this.btnSelectedGroup);
            this.pnlProductTop.Controls.Add(this.panel6);
            this.pnlProductTop.Controls.Add(this.panel5);
            this.pnlProductTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductTop.Location = new System.Drawing.Point(89, 63);
            this.pnlProductTop.Margin = new System.Windows.Forms.Padding(2);
            this.pnlProductTop.Name = "pnlProductTop";
            this.pnlProductTop.Size = new System.Drawing.Size(756, 43);
            this.pnlProductTop.TabIndex = 5;
            // 
            // btnSelectedGroup
            // 
            this.btnSelectedGroup.BackColor = System.Drawing.Color.Maroon;
            this.btnSelectedGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectedGroup.FlatAppearance.BorderSize = 0;
            this.btnSelectedGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectedGroup.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSelectedGroup.ForeColor = System.Drawing.Color.White;
            this.btnSelectedGroup.Location = new System.Drawing.Point(117, 0);
            this.btnSelectedGroup.Name = "btnSelectedGroup";
            this.btnSelectedGroup.Size = new System.Drawing.Size(571, 43);
            this.btnSelectedGroup.TabIndex = 3;
            this.btnSelectedGroup.Text = "Favaorite";
            this.btnSelectedGroup.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnPage);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(688, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(68, 43);
            this.panel6.TabIndex = 4;
            // 
            // btnPage
            // 
            this.btnPage.BackColor = System.Drawing.Color.Maroon;
            this.btnPage.FlatAppearance.BorderSize = 0;
            this.btnPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPage.ForeColor = System.Drawing.Color.White;
            this.btnPage.Location = new System.Drawing.Point(3, 0);
            this.btnPage.Name = "btnPage";
            this.btnPage.Size = new System.Drawing.Size(59, 42);
            this.btnPage.TabIndex = 3;
            this.btnPage.Text = "1/1";
            this.btnPage.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnScroll);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(117, 43);
            this.panel5.TabIndex = 0;
            // 
            // btnScroll
            // 
            this.btnScroll.BackColor = System.Drawing.Color.Maroon;
            this.btnScroll.FlatAppearance.BorderSize = 0;
            this.btnScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScroll.ForeColor = System.Drawing.Color.White;
            this.btnScroll.Image = ((System.Drawing.Image)(resources.GetObject("btnScroll.Image")));
            this.btnScroll.Location = new System.Drawing.Point(3, 0);
            this.btnScroll.Name = "btnScroll";
            this.btnScroll.Size = new System.Drawing.Size(110, 40);
            this.btnScroll.TabIndex = 3;
            this.btnScroll.Tag = "1";
            this.btnScroll.UseVisualStyleBackColor = false;
            this.btnScroll.Click += new System.EventHandler(this.btnScroll_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flpGroup);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(845, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(91, 646);
            this.panel2.TabIndex = 4;
            // 
            // flpGroup
            // 
            this.flpGroup.BackColor = System.Drawing.SystemColors.Control;
            this.flpGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpGroup.Location = new System.Drawing.Point(0, 149);
            this.flpGroup.Name = "flpGroup";
            this.flpGroup.Size = new System.Drawing.Size(91, 443);
            this.flpGroup.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDownGroup);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 592);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(91, 54);
            this.panel4.TabIndex = 1;
            // 
            // btnDownGroup
            // 
            this.btnDownGroup.BackColor = System.Drawing.Color.Maroon;
            this.btnDownGroup.FlatAppearance.BorderSize = 0;
            this.btnDownGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownGroup.ForeColor = System.Drawing.Color.White;
            this.btnDownGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDownGroup.Image")));
            this.btnDownGroup.Location = new System.Drawing.Point(3, 6);
            this.btnDownGroup.Name = "btnDownGroup";
            this.btnDownGroup.Size = new System.Drawing.Size(90, 51);
            this.btnDownGroup.TabIndex = 3;
            this.btnDownGroup.UseVisualStyleBackColor = false;
            this.btnDownGroup.Click += new System.EventHandler(this.btnDownGroup_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnShowToppanel);
            this.panel3.Controls.Add(this.btnFavourite);
            this.panel3.Controls.Add(this.btnUpGroup);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(91, 149);
            this.panel3.TabIndex = 0;
            // 
            // btnShowToppanel
            // 
            this.btnShowToppanel.BackColor = System.Drawing.Color.Maroon;
            this.btnShowToppanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnShowToppanel.FlatAppearance.BorderSize = 0;
            this.btnShowToppanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowToppanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShowToppanel.ForeColor = System.Drawing.Color.White;
            this.btnShowToppanel.Location = new System.Drawing.Point(0, 0);
            this.btnShowToppanel.Name = "btnShowToppanel";
            this.btnShowToppanel.Size = new System.Drawing.Size(91, 22);
            this.btnShowToppanel.TabIndex = 3;
            this.btnShowToppanel.Text = "Show";
            this.btnShowToppanel.UseVisualStyleBackColor = false;
            this.btnShowToppanel.Click += new System.EventHandler(this.btnShowToppanel_Click);
            // 
            // btnFavourite
            // 
            this.btnFavourite.BackColor = System.Drawing.Color.Maroon;
            this.btnFavourite.FlatAppearance.BorderSize = 0;
            this.btnFavourite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFavourite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnFavourite.ForeColor = System.Drawing.Color.White;
            this.btnFavourite.Location = new System.Drawing.Point(1, 82);
            this.btnFavourite.Name = "btnFavourite";
            this.btnFavourite.Size = new System.Drawing.Size(90, 63);
            this.btnFavourite.TabIndex = 3;
            this.btnFavourite.Tag = "0";
            this.btnFavourite.Text = "Favaorite";
            this.btnFavourite.UseVisualStyleBackColor = false;
            this.btnFavourite.Click += new System.EventHandler(this.BtnGroup_Click);
            // 
            // btnUpGroup
            // 
            this.btnUpGroup.BackColor = System.Drawing.Color.Maroon;
            this.btnUpGroup.FlatAppearance.BorderSize = 0;
            this.btnUpGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpGroup.ForeColor = System.Drawing.Color.White;
            this.btnUpGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnUpGroup.Image")));
            this.btnUpGroup.Location = new System.Drawing.Point(3, 27);
            this.btnUpGroup.Name = "btnUpGroup";
            this.btnUpGroup.Size = new System.Drawing.Size(90, 51);
            this.btnUpGroup.TabIndex = 3;
            this.btnUpGroup.UseVisualStyleBackColor = false;
            this.btnUpGroup.Click += new System.EventHandler(this.btnUpGroup_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTop.Controls.Add(this.pnlCustomer);
            this.pnlTop.Controls.Add(this.pnlBuilding);
            this.pnlTop.Controls.Add(this.pnlBlock);
            this.pnlTop.Controls.Add(this.pnlExtra);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(89, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlTop.Size = new System.Drawing.Size(847, 60);
            this.pnlTop.TabIndex = 6;
            this.pnlTop.Visible = false;
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCustomer.Controls.Add(this.lblPhoneNumber);
            this.pnlCustomer.Controls.Add(this.btnCustomer);
            this.pnlCustomer.Controls.Add(this.label9);
            this.pnlCustomer.Controls.Add(this.label4);
            this.pnlCustomer.Controls.Add(this.txtCustomerName);
            this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomer.Location = new System.Drawing.Point(0, 3);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(302, 57);
            this.pnlCustomer.TabIndex = 10;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhoneNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.White;
            this.lblPhoneNumber.Location = new System.Drawing.Point(70, 28);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(174, 23);
            this.lblPhoneNumber.TabIndex = 4;
            this.lblPhoneNumber.Text = "--";
            this.lblPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCustomer
            // 
            this.btnCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomer.Image")));
            this.btnCustomer.Location = new System.Drawing.Point(269, 25);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(26, 25);
            this.btnCustomer.TabIndex = 3;
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Phone";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerName.Data = 0;
            this.txtCustomerName.EnterColor = System.Drawing.Color.LightPink;
            this.txtCustomerName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtCustomerName.ForeColor = System.Drawing.Color.White;
            this.txtCustomerName.Format = null;
            this.txtCustomerName.isAllowNegative = false;
            this.txtCustomerName.isAllowSpecialChar = false;
            this.txtCustomerName.isNumberOnly = false;
            this.txtCustomerName.isNumeric = false;
            this.txtCustomerName.LeaveColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtCustomerName.Location = new System.Drawing.Point(70, 5);
            this.txtCustomerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(226, 16);
            this.txtCustomerName.TabIndex = 1;
            this.txtCustomerName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCustomerName.WaterMark = "";
            this.txtCustomerName.Validated += new System.EventHandler(this.txtCustomerName_Validated);
            // 
            // pnlBuilding
            // 
            this.pnlBuilding.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBuilding.Controls.Add(this.txtBuilding);
            this.pnlBuilding.Controls.Add(this.txtRoad);
            this.pnlBuilding.Controls.Add(this.lblAddressTwo);
            this.pnlBuilding.Controls.Add(this.lblAddressOne);
            this.pnlBuilding.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBuilding.Location = new System.Drawing.Point(302, 3);
            this.pnlBuilding.Name = "pnlBuilding";
            this.pnlBuilding.Size = new System.Drawing.Size(205, 57);
            this.pnlBuilding.TabIndex = 11;
            // 
            // txtBuilding
            // 
            this.txtBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuilding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtBuilding.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuilding.Data = 0;
            this.txtBuilding.EnterColor = System.Drawing.Color.LightPink;
            this.txtBuilding.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtBuilding.ForeColor = System.Drawing.Color.White;
            this.txtBuilding.Format = null;
            this.txtBuilding.isAllowNegative = false;
            this.txtBuilding.isAllowSpecialChar = false;
            this.txtBuilding.isNumberOnly = false;
            this.txtBuilding.isNumeric = false;
            this.txtBuilding.LeaveColor = System.Drawing.Color.White;
            this.txtBuilding.Location = new System.Drawing.Point(61, 30);
            this.txtBuilding.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuilding.Multiline = true;
            this.txtBuilding.Name = "txtBuilding";
            this.txtBuilding.Size = new System.Drawing.Size(139, 21);
            this.txtBuilding.TabIndex = 9;
            this.txtBuilding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuilding.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBuilding.WaterMark = "0.000";
            // 
            // txtRoad
            // 
            this.txtRoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtRoad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRoad.Data = 0;
            this.txtRoad.EnterColor = System.Drawing.Color.LightPink;
            this.txtRoad.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtRoad.ForeColor = System.Drawing.Color.White;
            this.txtRoad.Format = null;
            this.txtRoad.isAllowNegative = false;
            this.txtRoad.isAllowSpecialChar = false;
            this.txtRoad.isNumberOnly = false;
            this.txtRoad.isNumeric = false;
            this.txtRoad.LeaveColor = System.Drawing.Color.White;
            this.txtRoad.Location = new System.Drawing.Point(61, 4);
            this.txtRoad.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoad.Multiline = true;
            this.txtRoad.Name = "txtRoad";
            this.txtRoad.Size = new System.Drawing.Size(139, 21);
            this.txtRoad.TabIndex = 3;
            this.txtRoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRoad.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRoad.WaterMark = "0.000";
            // 
            // lblAddressTwo
            // 
            this.lblAddressTwo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblAddressTwo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblAddressTwo.ForeColor = System.Drawing.Color.White;
            this.lblAddressTwo.Location = new System.Drawing.Point(2, 29);
            this.lblAddressTwo.Name = "lblAddressTwo";
            this.lblAddressTwo.Size = new System.Drawing.Size(57, 23);
            this.lblAddressTwo.TabIndex = 8;
            this.lblAddressTwo.Text = "Building";
            this.lblAddressTwo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddressOne
            // 
            this.lblAddressOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblAddressOne.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblAddressOne.ForeColor = System.Drawing.Color.White;
            this.lblAddressOne.Location = new System.Drawing.Point(2, 3);
            this.lblAddressOne.Name = "lblAddressOne";
            this.lblAddressOne.Size = new System.Drawing.Size(57, 23);
            this.lblAddressOne.TabIndex = 2;
            this.lblAddressOne.Text = "Road";
            this.lblAddressOne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBlock
            // 
            this.pnlBlock.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBlock.Controls.Add(this.txtBlock);
            this.pnlBlock.Controls.Add(this.lblAddressThree);
            this.pnlBlock.Controls.Add(this.txtFlat);
            this.pnlBlock.Controls.Add(this.lblAddressFour);
            this.pnlBlock.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBlock.Location = new System.Drawing.Point(507, 3);
            this.pnlBlock.Name = "pnlBlock";
            this.pnlBlock.Size = new System.Drawing.Size(205, 57);
            this.pnlBlock.TabIndex = 12;
            // 
            // txtBlock
            // 
            this.txtBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlock.Data = 0;
            this.txtBlock.EnterColor = System.Drawing.Color.LightPink;
            this.txtBlock.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtBlock.ForeColor = System.Drawing.Color.White;
            this.txtBlock.Format = null;
            this.txtBlock.isAllowNegative = false;
            this.txtBlock.isAllowSpecialChar = false;
            this.txtBlock.isNumberOnly = false;
            this.txtBlock.isNumeric = false;
            this.txtBlock.LeaveColor = System.Drawing.Color.White;
            this.txtBlock.Location = new System.Drawing.Point(63, 7);
            this.txtBlock.Margin = new System.Windows.Forms.Padding(4);
            this.txtBlock.Multiline = true;
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(140, 21);
            this.txtBlock.TabIndex = 6;
            this.txtBlock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBlock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBlock.WaterMark = "0.000";
            // 
            // lblAddressThree
            // 
            this.lblAddressThree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblAddressThree.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblAddressThree.ForeColor = System.Drawing.Color.White;
            this.lblAddressThree.Location = new System.Drawing.Point(5, 3);
            this.lblAddressThree.Name = "lblAddressThree";
            this.lblAddressThree.Size = new System.Drawing.Size(54, 23);
            this.lblAddressThree.TabIndex = 4;
            this.lblAddressThree.Text = "Block";
            this.lblAddressThree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFlat
            // 
            this.txtFlat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFlat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.txtFlat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFlat.Data = 0;
            this.txtFlat.EnterColor = System.Drawing.Color.LightPink;
            this.txtFlat.Font = new System.Drawing.Font("MS Reference Sans Serif", 11F);
            this.txtFlat.ForeColor = System.Drawing.Color.White;
            this.txtFlat.Format = null;
            this.txtFlat.isAllowNegative = false;
            this.txtFlat.isAllowSpecialChar = false;
            this.txtFlat.isNumberOnly = false;
            this.txtFlat.isNumeric = false;
            this.txtFlat.LeaveColor = System.Drawing.Color.White;
            this.txtFlat.Location = new System.Drawing.Point(63, 30);
            this.txtFlat.Margin = new System.Windows.Forms.Padding(4);
            this.txtFlat.Multiline = true;
            this.txtFlat.Name = "txtFlat";
            this.txtFlat.Size = new System.Drawing.Size(140, 21);
            this.txtFlat.TabIndex = 7;
            this.txtFlat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFlat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFlat.WaterMark = "0.000";
            // 
            // lblAddressFour
            // 
            this.lblAddressFour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(71)))), ((int)(((byte)(121)))));
            this.lblAddressFour.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblAddressFour.ForeColor = System.Drawing.Color.White;
            this.lblAddressFour.Location = new System.Drawing.Point(5, 28);
            this.lblAddressFour.Name = "lblAddressFour";
            this.lblAddressFour.Size = new System.Drawing.Size(54, 23);
            this.lblAddressFour.TabIndex = 5;
            this.lblAddressFour.Text = "Flat";
            this.lblAddressFour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlExtra
            // 
            this.pnlExtra.BackColor = System.Drawing.SystemColors.Control;
            this.pnlExtra.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlExtra.Location = new System.Drawing.Point(712, 3);
            this.pnlExtra.Name = "pnlExtra";
            this.pnlExtra.Size = new System.Drawing.Size(135, 57);
            this.pnlExtra.TabIndex = 13;
            // 
            // pnlMainMenu
            // 
            this.pnlMainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMainMenu.Controls.Add(this.btnCustomerHistory);
            this.pnlMainMenu.Controls.Add(this.btnCashDrawer);
            this.pnlMainMenu.Controls.Add(this.btnHold);
            this.pnlMainMenu.Controls.Add(this.btnRetrieveBill);
            this.pnlMainMenu.Controls.Add(this.btnPlus);
            this.pnlMainMenu.Controls.Add(this.btnMinus);
            this.pnlMainMenu.Controls.Add(this.btnUp);
            this.pnlMainMenu.Controls.Add(this.btnDown);
            this.pnlMainMenu.Controls.Add(this.btnKotLookUp);
            this.pnlMainMenu.Controls.Add(this.btnReturn);
            this.pnlMainMenu.Controls.Add(this.btnFormType);
            this.pnlMainMenu.Controls.Add(this.btnfastSale);
            this.pnlMainMenu.Controls.Add(this.btnShowNumberpad);
            this.pnlMainMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMainMenu.Location = new System.Drawing.Point(3, 3);
            this.pnlMainMenu.Name = "pnlMainMenu";
            this.pnlMainMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.pnlMainMenu.Size = new System.Drawing.Size(86, 706);
            this.pnlMainMenu.TabIndex = 0;
            // 
            // btnCustomerHistory
            // 
            this.btnCustomerHistory.BackColor = System.Drawing.Color.Maroon;
            this.btnCustomerHistory.FlatAppearance.BorderSize = 0;
            this.btnCustomerHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCustomerHistory.ForeColor = System.Drawing.Color.White;
            this.btnCustomerHistory.Location = new System.Drawing.Point(3, 3);
            this.btnCustomerHistory.Name = "btnCustomerHistory";
            this.btnCustomerHistory.Size = new System.Drawing.Size(86, 48);
            this.btnCustomerHistory.TabIndex = 3;
            this.btnCustomerHistory.Text = "Customer History";
            this.btnCustomerHistory.UseVisualStyleBackColor = false;
            this.btnCustomerHistory.Click += new System.EventHandler(this.btnCustomerHistory_Click);
            // 
            // btnCashDrawer
            // 
            this.btnCashDrawer.BackColor = System.Drawing.Color.Maroon;
            this.btnCashDrawer.FlatAppearance.BorderSize = 0;
            this.btnCashDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashDrawer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCashDrawer.ForeColor = System.Drawing.Color.White;
            this.btnCashDrawer.Location = new System.Drawing.Point(3, 57);
            this.btnCashDrawer.Name = "btnCashDrawer";
            this.btnCashDrawer.Size = new System.Drawing.Size(86, 48);
            this.btnCashDrawer.TabIndex = 3;
            this.btnCashDrawer.Text = "Cash Drawer";
            this.btnCashDrawer.UseVisualStyleBackColor = false;
            this.btnCashDrawer.Click += new System.EventHandler(this.btnCashDrawer_Click);
            // 
            // btnHold
            // 
            this.btnHold.BackColor = System.Drawing.Color.Maroon;
            this.btnHold.FlatAppearance.BorderSize = 0;
            this.btnHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHold.ForeColor = System.Drawing.Color.White;
            this.btnHold.Location = new System.Drawing.Point(3, 111);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(86, 48);
            this.btnHold.TabIndex = 3;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = false;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // btnRetrieveBill
            // 
            this.btnRetrieveBill.BackColor = System.Drawing.Color.Maroon;
            this.btnRetrieveBill.FlatAppearance.BorderSize = 0;
            this.btnRetrieveBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetrieveBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRetrieveBill.ForeColor = System.Drawing.Color.White;
            this.btnRetrieveBill.Location = new System.Drawing.Point(3, 165);
            this.btnRetrieveBill.Name = "btnRetrieveBill";
            this.btnRetrieveBill.Size = new System.Drawing.Size(86, 48);
            this.btnRetrieveBill.TabIndex = 3;
            this.btnRetrieveBill.Text = "Retrive";
            this.btnRetrieveBill.UseVisualStyleBackColor = false;
            this.btnRetrieveBill.Click += new System.EventHandler(this.btnRetrieveBill_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = System.Drawing.Color.Maroon;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPlus.ForeColor = System.Drawing.Color.White;
            this.btnPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnPlus.Image")));
            this.btnPlus.Location = new System.Drawing.Point(3, 219);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(86, 48);
            this.btnPlus.TabIndex = 3;
            this.btnPlus.Text = "Quantity";
            this.btnPlus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = System.Drawing.Color.Maroon;
            this.btnMinus.FlatAppearance.BorderSize = 0;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnMinus.ForeColor = System.Drawing.Color.White;
            this.btnMinus.Image = ((System.Drawing.Image)(resources.GetObject("btnMinus.Image")));
            this.btnMinus.Location = new System.Drawing.Point(3, 273);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(86, 48);
            this.btnMinus.TabIndex = 3;
            this.btnMinus.Text = "Quantity";
            this.btnMinus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Maroon;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.ForeColor = System.Drawing.Color.White;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(3, 327);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(86, 48);
            this.btnUp.TabIndex = 3;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Maroon;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.ForeColor = System.Drawing.Color.White;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(3, 381);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(86, 48);
            this.btnDown.TabIndex = 3;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnKotLookUp
            // 
            this.btnKotLookUp.BackColor = System.Drawing.Color.Maroon;
            this.btnKotLookUp.FlatAppearance.BorderSize = 0;
            this.btnKotLookUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKotLookUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnKotLookUp.ForeColor = System.Drawing.Color.White;
            this.btnKotLookUp.Location = new System.Drawing.Point(3, 435);
            this.btnKotLookUp.Name = "btnKotLookUp";
            this.btnKotLookUp.Size = new System.Drawing.Size(86, 48);
            this.btnKotLookUp.TabIndex = 3;
            this.btnKotLookUp.Text = "Order\r\nLook Up";
            this.btnKotLookUp.UseVisualStyleBackColor = false;
            this.btnKotLookUp.Click += new System.EventHandler(this.btnKotLookUp_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Maroon;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(3, 489);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(86, 48);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnFormType
            // 
            this.btnFormType.BackColor = System.Drawing.Color.Maroon;
            this.btnFormType.FlatAppearance.BorderSize = 0;
            this.btnFormType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFormType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFormType.ForeColor = System.Drawing.Color.White;
            this.btnFormType.Location = new System.Drawing.Point(3, 543);
            this.btnFormType.Name = "btnFormType";
            this.btnFormType.Size = new System.Drawing.Size(86, 48);
            this.btnFormType.TabIndex = 4;
            this.btnFormType.Text = "Take Away";
            this.btnFormType.UseVisualStyleBackColor = false;
            this.btnFormType.Click += new System.EventHandler(this.btnFormType_Click);
            // 
            // btnfastSale
            // 
            this.btnfastSale.BackColor = System.Drawing.Color.Maroon;
            this.btnfastSale.FlatAppearance.BorderSize = 0;
            this.btnfastSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfastSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnfastSale.ForeColor = System.Drawing.Color.White;
            this.btnfastSale.Location = new System.Drawing.Point(3, 597);
            this.btnfastSale.Name = "btnfastSale";
            this.btnfastSale.Size = new System.Drawing.Size(86, 48);
            this.btnfastSale.TabIndex = 3;
            this.btnfastSale.Text = "Fast Sale";
            this.btnfastSale.UseVisualStyleBackColor = false;
            this.btnfastSale.Click += new System.EventHandler(this.btnfastSale_Click);
            // 
            // btnShowNumberpad
            // 
            this.btnShowNumberpad.BackColor = System.Drawing.Color.Maroon;
            this.btnShowNumberpad.FlatAppearance.BorderSize = 0;
            this.btnShowNumberpad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowNumberpad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShowNumberpad.ForeColor = System.Drawing.Color.White;
            this.btnShowNumberpad.Location = new System.Drawing.Point(3, 651);
            this.btnShowNumberpad.Name = "btnShowNumberpad";
            this.btnShowNumberpad.Size = new System.Drawing.Size(86, 48);
            this.btnShowNumberpad.TabIndex = 3;
            this.btnShowNumberpad.Text = ">>";
            this.btnShowNumberpad.UseVisualStyleBackColor = false;
            this.btnShowNumberpad.Click += new System.EventHandler(this.btnShowNumberpad_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(839, 712);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "SL";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Price";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // RestaurantPOSView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 721);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSide);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RestaurantPOSView";
            this.Text = "RestaurantPOSView";
            this.Load += new System.EventHandler(this.RestaurantPOSView_Load);
            this.VisibleChanged += new System.EventHandler(this.RestaurantPOSView_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RestaurantPOSView_KeyDown);
            this.pnlSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlKOT.ResumeLayout(false);
            this.pnlNumberpad.ResumeLayout(false);
            this.pnlNumberpad.PerformLayout();
            this.pnlInvoice.ResumeLayout(false);
            this.pnlInvoice.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlProductTop.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlBuilding.ResumeLayout(false);
            this.pnlBuilding.PerformLayout();
            this.pnlBlock.ResumeLayout(false);
            this.pnlBlock.PerformLayout();
            this.pnlMainMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsItems)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
