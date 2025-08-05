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
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SalesInvoiceView : BaseFormThree
	{
		private DateTime estimateDate;

		private int iFYearId;

		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private SalesMaster entsaleshdr;

		private List<SalesDetails> entsalesdtls = new List<SalesDetails>();

		private TransactionMaster entTMaster = new TransactionMaster();

		private List<Transaction> entTransactions = new List<Transaction>();

		private List<SettingsEntity> entBooleanSettings;

		private int? iCashAcctId;

		private int? iCardAccount;

		private int? iOtherAccount;

		private int iSalesInvId;

		private int? iMasterId;

		private SalesOrderMaster salesOrderMaster;

		private List<POSDetails> entPOSDetails;

		private DataGridViewTextBoxEditingControl textbox;

		private SalesReturnView salesReturn;

		private AccountEntity account;

		private string sKeyChar = "";

		private bool isFromOrder = false;

		private bool isReturn = false;

		private int iSOrderId;

		private DBContext db;

		private string strVoucherType = "Sales Voucher";

		private string sBranchDetails = "";

		private decimal dTendered;

		private decimal dChanges;

		private int iNormalCashAccountId;

		private List<ComboFill> comboFills = new List<ComboFill>();

		private decimal dSubDiscount;

		private List<InvoiceAdjustment> printEntities = new List<InvoiceAdjustment>();

		private int? rtnInvRefNumber;

		private IContainer components = null;

		private AshUpDown nudInvoiceNo;

		private Label label25;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private Label label2;

		private CheckBox chkEnableInvNo;

		private Button btnDate;

		private AshComboBox cmbSalesType;

		private Label label32;

		private Button btnBalance;

		private Label lblPreBill;

		private Label label19;

		private AshComboBox cmbProCategory;

		private Label label13;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private AshComboBox cmbSlsExi;

		private Label label11;

		private AshComboBox cmbPartyAccnt;

		private Label label6;

		private AshComboBox cmbVehicle;

		private Label label5;

		private Button btnAccCreate;

		private Button btnOrder;

		private AshComboBox cmbRoute;

		private Label label4;

		private AshComboBox cmbSlsAcct;

		private Label label39;

		private Label label3;

		private AshTextBoxNormal txtPrintCopy;

		private Button btnClear;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private Label label37;

		private Label label23;

		private Label label93;

		private Label label90;

		private AshNumericLabel lblCessAmount;

		private AshNumericLabel lblTotalIGST;

		private AshNumericLabel lblTotalCGST;

		private AshNumericLabel lblTotalSGST;

		private AshNumericLabel lblTaxTotal;

		private Label label21;

		private AshNumericLabel lblGrandTotal;

		private Label label36;

		private AshTextBoxNormal txtAddDiscAmnt;

		private AshTextBoxNormal txtAddDisc;

		private Label label15;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private Label label34;

		private AshTextBoxNormal txtAddAmnt;

		private Label label33;

		private CheckBox cbPrint;

		private AshNumericLabel lblTaxable;

		private AshNumericLabel lblDisTotal;

		private AshNumericLabel lblGrsTotal;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblUntQty;

		private AshNumericLabel lblLastPurRate;

		private AshNumericLabel lblCStock;

		private Button btnSRtn;

		public AshDataGridview dgDetails;

		private BindingSource bdsDetails;

		private Button btnPayment;

		private AshNumericLabel lblBalance;

		private AshNumericLabel lblCashPaid;

		private Label label16;

		private Label label18;

		private Label label20;

		private AshNumericLabel lblSReturnAmnt;

		private Label label7;

		private AshComboBox cmbDepot;

		private AshNumericLabel lblNetTotal;

		private Button btnSeperator2;

		private Button button1;

		private Button btnSearch;

		private Label label8;

		private AshNumericLabel lblCardPaid;

		private Button btnFirst;

		private Button btnBack;

		private Button btnNext;

		private Button btnLast;

		private Button btnPDF;

		private Label label9;

		private AshNumericLabel lblOnline;

		private Label lblRunBal;

		private Button btnPrint2;

		private CheckBox cbPrint2;

		private ToolTip toolTip;

		private Button btnReceiptAndPayment;

		private FlowLayoutPanel flpButtons;

		private Button btnDiscount;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn col_ProCode;
        private DataGridViewTextBoxColumn col_ProName;
        private DataGridViewTextBoxColumn col_Barcode;
        private DataGridViewTextBoxColumn col_HSN;
        private DataGridViewTextBoxColumn col_Qty;
        private DataGridViewTextBoxColumn col_Unit;
        private DataGridViewTextBoxColumn col_Rate;
        private DataGridViewTextBoxColumn col_Gross;
        private DataGridViewTextBoxColumn col_Dis;
        private DataGridViewTextBoxColumn col_Discount;
        private DataGridViewTextBoxColumn col_Taxable;
        private DataGridViewTextBoxColumn col_Vat;
        private DataGridViewTextBoxColumn col_VatAmnt;
        private DataGridViewTextBoxColumn col_GST;
        private DataGridViewTextBoxColumn col_GSTAmnt;
        private DataGridViewTextBoxColumn col_SGST;
        private DataGridViewTextBoxColumn col_SGSTAmnt;
        private DataGridViewTextBoxColumn col_CGST;
        private DataGridViewTextBoxColumn col_CGSTAmnt;
        private DataGridViewTextBoxColumn col_IGST;
        private DataGridViewTextBoxColumn col_IGSTAmnt;
        private DataGridViewTextBoxColumn col_NetAmnt;
        private Button btnCustomerSearch;

		public int? iInvoiceNo { get; set; }

		public string sVoucherType { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public SalesInvoiceView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			ColorChange.Color(this);
			if (CommonSettings.blnDefaultCellIsBarcode.toBool())
			{
				col_Barcode.DisplayIndex = 2;
				col_ProName.DisplayIndex = 3;
			}
			lblCashPaid.Format = CommonSettings.strCurencyFormat;
			lblCardPaid.Format = CommonSettings.strCurencyFormat;
			lblSReturnAmnt.Format = CommonSettings.strCurencyFormat;
			txtAddAmnt.Format = CommonSettings.strCurencyFormat;
			txtFreight.Format = CommonSettings.strCurencyFormat;
			txtAddDisc.Format = CommonSettings.strCurencyFormat;
			txtAddDiscAmnt.Format = CommonSettings.strCurencyFormat;
			lblTotalSGST.Format = CommonSettings.strCurencyFormat;
			lblTotalCGST.Format = CommonSettings.strCurencyFormat;
			lblTotalIGST.Format = CommonSettings.strCurencyFormat;
			lblTaxTotal.Format = CommonSettings.strCurencyFormat;
			col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Dis.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Vat.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_VatAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			lblLastPurRate.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			if (Utility.uManufacturer != "Ashpro")
			{
				btnDelete.Visible = false;
			}
		}

		private decimal FreeAmountTotalFindMethod()
		{
			decimal result = default(decimal);
			try
			{
				foreach (SalesDetails item in entsalesdtls.Where((SalesDetails x) => x.ProRate == 0m).ToList())
				{
					result += AshproCommon.ValueFindMethod("SELECT SPrice FROM ProductPriceCategory where ProductId='" + item.ProductId + "' and PCategoryId='" + cmbProCategory.SelectedValue.ToInt32() + "' and UnitId='" + item.UnitId + "'");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return result;
		}

		private void SaveSubMethod()
		{
			if (AshproCommon.InsertToDatabase(entsalesdtls.Cast<object>().ToList(), "SalesEstimateSettings"))
			{
				POSHelper.TranLogMethod(0m, entsaleshdr.GrandDiscount, cmbSlsAcct.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, strVoucherType, Settings.Default.intLogOnId, "Main", cmbSalesType.Text, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(entsaleshdr.GrandDiscount, 0m, cmbPartyAccnt.SelectedValue.ToInt32(), cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, strVoucherType, Settings.Default.intLogOnId, "Sub", cmbSalesType.Text, AniHelper.GetDate(dtmDate.Value));
				if (CommonSettings.blnTaxEnabled && lblTaxTotal.Value > 0m)
				{
					int num = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
					POSHelper.TranLogMethod(entsaleshdr.TotalTaxAmount, 0m, cmbSlsAcct.SelectedValue.ToInt32(), num, iMasterId.ToInt32(), entsaleshdr.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, entsaleshdr.TotalTaxAmount, num, cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
				}
				if (CommonSettings.blnCessEnabled && lblCessAmount.Value > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
					POSHelper.TranLogMethod(entsaleshdr.FloodCess, 0m, cmbSlsAcct.SelectedValue.ToInt32(), num2, iMasterId.ToInt32(), entsaleshdr.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, entsaleshdr.FloodCess, num2, cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
				}
				entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
				POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), entsaleshdr.SalesInvNo.toInt32(), strVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), strVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
				if (lblCashPaid.Value > 0m)
				{
					int num3 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num3, dtmDate.Value.Date, lblCashPaid.Value, "Receipt Voucher", "Cash Received On Sale", cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId.toInt32(), cmbSlsExi.SelectedValue.ToInt32(), Settings.Default.intLogOnId, "Cash Receipt");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num3 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(lblCashPaid.Value, 0m, iCashAcctId.toInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVoucherID, num3, "Receipt Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, lblCashPaid.Value, cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId.toInt32(), iVoucherID, num3, "Receipt Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
				if (lblCardPaid.Value > 0m)
				{
					int num4 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num4, dtmDate.Value.Date, lblCardPaid.Value, "Receipt Voucher", "Cash Received On Sale", cmbPartyAccnt.SelectedValue.ToInt32(), iCardAccount.toInt32(), cmbSlsExi.SelectedValue.ToInt32(), Settings.Default.intLogOnId, "Cash Receipt");
					int iVoucherID2 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num4 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(lblCardPaid.Value, 0m, iCardAccount.toInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVoucherID2, num4, "Receipt Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, lblCardPaid.Value, cmbPartyAccnt.SelectedValue.ToInt32(), iCardAccount.toInt32(), iVoucherID2, num4, "Receipt Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
				if (isFromOrder)
				{
					AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
				}
			}
		}

		private void NameAndBalanceFindMethod()
		{
			cmbProCategory.SelectedValue = db.Accounts(new
			{
				AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
			}).FirstOrDefault().PCategoryId;
			if (cmbProCategory.SelectedValue == null)
			{
				cmbProCategory.SelectedIndex = 0;
			}
			lblRunBal.Text = AniHelper.AccountBalanceFindMethod(cmbPartyAccnt.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)));
		}

		private void LoadComboMethod()
		{
			try
			{
				AniHelper.FillPCategory(cmbProCategory);
				AniHelper.FillAccount(cmbPartyAccnt);
				AniHelper.FillSalesAccount(cmbSlsAcct);
				AniHelper.FillFormType(cmbSalesType);
				AniHelper.FillEmployees(cmbSlsExi);
				AniHelper.FillRoute(cmbRoute);
				AniHelper.FillGodown(cmbDepot);
				AniHelper.FillVehicle(cmbVehicle);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void LoadSettingsMethod()
		{
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = col_Dis;
				bool visible = (col_Discount.Visible = CommonSettings.blnShowDiscountInInvoice);
				dataGridViewTextBoxColumn.Visible = visible;
				col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_VatAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_GSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_IGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_SGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_CGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
				entBooleanSettings = new List<SettingsEntity>();
				entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == "Sales Invoice" && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
				chkEnableInvNo.Checked = CommonSettings.blnEditInvoiceNumber;
				cbPrint.Checked = CommonSettings.blnPntInvWlSaving;
				if (cmbSalesType.SelectedIndex == 0 && CommonSettings.blnCessEnabled)
				{
					label37.Visible = true;
					lblCessAmount.Visible = true;
				}
				else
				{
					label37.Visible = false;
					lblCessAmount.Visible = false;
				}
				cmbSalesType.Text = CommonSettings.strDefaultSalesType;
				if (!CommonSettings.blnTaxEnabled)
				{
					return;
				}
				if (CommonSettings.blnShowHSNInInv)
				{
					col_HSN.Visible = true;
				}
				if (CommonSettings.blnGSTEnabled)
				{
					col_GST.Visible = CommonSettings.blnShowGSTInInvoice;
					col_GSTAmnt.Visible = CommonSettings.blnShowGSTAmountInInvoice;
					if (CommonSettings.blnShowSGSTInInv)
					{
						col_SGST.Visible = true;
					}
					if (CommonSettings.blnShowSGSTAmntInInv)
					{
						col_SGSTAmnt.Visible = true;
					}
					if (CommonSettings.blnShowCGSTInInv)
					{
						col_CGST.Visible = true;
					}
					if (CommonSettings.blnShowCGSTAmntInInv)
					{
						col_CGSTAmnt.Visible = true;
					}
					if (CommonSettings.blnShowIGSTInInv)
					{
						col_IGST.Visible = true;
					}
					if (CommonSettings.blnShowIGSTAmntInInv)
					{
						col_IGSTAmnt.Visible = true;
					}
					label90.Visible = true;
					label93.Visible = true;
					label23.Visible = true;
					lblTotalSGST.Visible = true;
					lblTotalCGST.Visible = true;
					lblTotalIGST.Visible = true;
				}
				else
				{
					col_Vat.Visible = CommonSettings.blnShowVatInInvoice;
					col_VatAmnt.Visible = CommonSettings.blnShowVatAmountInInvoice;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void PrintMethod(bool isPDF = false, bool isFirstPrint = true)
		{
			try
			{
				if (entsalesdtls.Count == 0)
				{
					Messages.ErrorMessage("No records to Print!");
				}
				else if (CommonSettings.blnInvoicePrintingByRDLC)
				{
					Shop shop = new Shop();
					Company company = db.Companies().FirstOrDefault();
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
					POSMaster pOSMaster = entsaleshdr.CopyTo<POSMaster>();
					pOSMaster.GrandTotal = entsaleshdr.GrandDiscount;
					pOSMaster.SalesAccount = db.Accounts(new
					{
						AccountId = entsaleshdr.SalesAccountId
					}).FirstOrDefault().AccountName;
					pOSMaster.PartyAccount = db.Accounts(new
					{
						AccountId = entsaleshdr.PartyAccountId
					}).FirstOrDefault().AccountName;
					pOSMaster.Financialyear = db.FinancialYears(new
					{
						FyearId = entsaleshdr.FYearID
					}).FirstOrDefault().FyearName;
					pOSMaster.BranchName = db.Branches(new
					{
						Id = entsaleshdr.FK_BranchId
					}).FirstOrDefault().Name;
					pOSMaster.SalesMan = db.EmployeeDetails(new
					{
						EmpID = entsaleshdr.StafId
					}).FirstOrDefault().EmpName;
					pOSMaster.JEName = db.EmployeeDetails(new
					{
						EmpID = entsaleshdr.JEId
					}).FirstOrDefault().EmpName;
					DataTable dataTable2 = new DataTable();
					dataTable2 = AniHelper.ToDataTable(pOSMaster);
					dataTable2.TableName = "dtPOSMaster";
					dataSet.Tables.Add(dataTable2);
					DataTable dataTable3 = new DataTable();
					entPOSDetails = POSHelper.GetPOSDetailsFromSDetails(entsalesdtls);
					dataTable3 = AniHelper.ListToDataTable(entPOSDetails);
					dataTable3.TableName = "dtPOSDetails";
					dataSet.Tables.Add(dataTable3);
					DataTable dataTable4 = new DataTable();
					decimal? cashPayment = pOSMaster.CashPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(printEntities, "Cash Payment", pOSMaster.CashPayment.ToString(CommonSettings.strCurencyFormat), "دفع نقدا");
					}
					cashPayment = pOSMaster.CardPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(printEntities, "Card Payment", pOSMaster.CardPayment.ToString(CommonSettings.strCurencyFormat), "بطاقه ائتمان");
					}
					cashPayment = pOSMaster.OtherPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(printEntities, "Online Payment", pOSMaster.OtherPayment.ToString(CommonSettings.strCurencyFormat), "مدفوعات أخرى");
					}
					if (pOSMaster.BillDiscount > 0m)
					{
						AniHelper.AddPaymentPrint(printEntities, "Discount", pOSMaster.BillDiscount.ToString(CommonSettings.strCurencyFormat), "خصم");
					}
					AniHelper.AddPaymentPrint(printEntities, "Tax (%)", "Tax Amount", "Total");
					foreach (SalesDetails item in entsalesdtls)
					{
						string nm = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item.GST).TCategoryName;
						if (!printEntities.Any((InvoiceAdjustment x) => x.Name == nm))
						{
							decimal num = entsalesdtls.Where((SalesDetails x) => x.GST == item.GST).Sum((SalesDetails y) => y.GSTAMOUNT);
							AniHelper.AddPaymentPrint(sRLabel: entsalesdtls.Where((SalesDetails x) => x.GST == item.GST).Sum((SalesDetails y) => y.NetAmnt).ToString(CommonSettings.strCurencyFormat), paymentPrint: printEntities, sName: nm, dValue: num.ToString(CommonSettings.strCurencyFormat));
						}
					}
					dataTable4 = AniHelper.ListToDataTable(printEntities);
					dataTable4.TableName = "dtPayment";
					dataSet.Tables.Add(dataTable4);
					decimal num2 = entsalesdtls.Sum((SalesDetails x) => x.ProQty);
					string sReportPath = Application.StartupPath + "\\ReportForms\\rptPOSPrintWithInvoiceNo.rdlc";
					List<atReportParameter> list = new List<atReportParameter>();
					list.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
					CultureInfo cultureInfo = new CultureInfo("en-US");
					string parameterValue = pOSMaster.SalesDate.ToString(cultureInfo);
					list.Add(new atReportParameter("QuantityParameter", CommonSettings.strQuantityFormat));
					list.Add(new atReportParameter("InvoiceDate", parameterValue));
					list.Add(new atReportParameter("Tendered", dTendered.ToString(CommonSettings.strCurencyFormat)));
					list.Add(new atReportParameter("Changes", dChanges.ToString(CommonSettings.strCurencyFormat)));
					list.Add(new atReportParameter("TotalQuantity", num2.ToString(CommonSettings.strQuantityFormat)));
					printerSettings.PrinterName = printerSettings.PrinterName;
					DirectReportPrint directReportPrint = new DirectReportPrint();
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					folderPath = Path.Combine(folderPath, Utility.uManufacturer);
					folderPath = Path.Combine(folderPath, "Invoice");
					if (!Directory.Exists(folderPath))
					{
						Directory.CreateDirectory(folderPath);
					}
					string path = "Invoice_" + pOSMaster.CntrInvNo + "_" + DateTime.Now.Date.ToString(CommonSettings.strDateFormat) + ".pdf";
					path = Path.Combine(folderPath, path);
					directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list, isPDF, txtPrintCopy.Data, path);
				}
				else
				{
					entDMaster = db.DotmatrixMasters(new
					{
						VochrType = "Sales Invoice",
						FormType = cmbSalesType.Text,
						isDefault = true
					}).FirstOrDefault();
					AppPrintingMethod(isPDF);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AppPrintingMethod(bool isPDF)
		{
			if (entDMaster != null)
			{
				entDSettings = new List<DotmatrixSettings>();
				entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entcompany.TaxId = company.GSTNo;
				entcompany.Logo = company.Logo;
				entPartyDtl = new PartyDetails();
				account = db.Accounts(new
				{
					AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
				}).FirstOrDefault();
				entPartyDtl = POSHelper.GetParty(account);
				int num = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
				if (account.AGroupId == num)
				{
					entPartyDtl.Name = "Cash Customer";
				}
				iSalesInvId = iMasterId.ToInt32();
				entTMaster = new TransactionMaster();
				entsaleshdr = new SalesMaster();
				entsaleshdr = db.SalesMasters(new
				{
					SalesInvId = iSalesInvId
				}).FirstOrDefault();
				bool isCashAccount = ((cmbPartyAccnt.SelectedValue.ToInt32() == iNormalCashAccountId) ? true : false);
				entTMaster = POSHelper.GetTMaster(entsaleshdr, isCashAccount);
				entTMaster.TotalQuantity = entTransactions.Sum((Transaction x) => x.Quantity);
				entTMaster.GrossAmount = entTransactions.Sum((Transaction x) => x.GrossAmount);
				entTMaster.OrderNo = ((salesOrderMaster != null) ? salesOrderMaster.VchrNo.ToInt32() : 0);
				entTMaster.Tendered = dTendered;
				entTMaster.Changes = dChanges;
				entTMaster.SalesMan = cmbSlsExi.Text;
				entTMaster.PriceCategory = cmbProCategory.Text;
				if (lblGrandTotal.Value > 0m)
				{
					if (cmbPartyAccnt.SelectedValue.ToInt32() == iNormalCashAccountId)
					{
						entTMaster.InvoiceMode = "Cash Sale";
					}
					else if (lblBalance.Value == 0m)
					{
						entTMaster.InvoiceMode = "Cash Sale";
					}
					else
					{
						entTMaster.InvoiceMode = "Credit Sale";
					}
				}
				else
				{
					entTMaster.InvoiceMode = "Sales Return";
				}
				foreach (Transaction item in entTransactions)
				{
					string nm = db.TaxCategories().FirstOrDefault((TaxCategoryEntity x) => x.SalesVat == item.Vat).TCategoryName;
					if (!printEntities.Any((InvoiceAdjustment x) => x.Name == nm))
					{
						decimal num2 = entsalesdtls.Where((SalesDetails x) => x.GST == item.Vat).Sum((SalesDetails y) => y.GSTAMOUNT);
						AniHelper.AddPaymentPrint(sRLabel: entsalesdtls.Where((SalesDetails x) => x.GST == item.Vat).Sum((SalesDetails y) => y.NetAmnt).ToString(CommonSettings.strCurencyFormat), paymentPrint: printEntities, sName: nm, dValue: num2.ToString(CommonSettings.strCurencyFormat));
					}
				}
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, printEntities, entTMaster, isPrint: true, txtPrintCopy.Data, isPDF);
				printHelper = null;
			}
			else
			{
				Messages.WarningMessage("There is no default print design. Please make your own design");
			}
		}

		private void SalesHeaderLoadMethod(SalesMaster sMaster, int InvNo)
		{
			try
			{
				sMaster.SalesInvId = iMasterId;
				sMaster.SalesInvNo = InvNo;
				sMaster.InvType = cmbSalesType.Text;
				sMaster.SalesDate = dtmDate.Value;
				sMaster.SalesAccountId = cmbSlsAcct.SelectedValue.ToInt32();
				sMaster.PartyAccountId = cmbPartyAccnt.SelectedValue.ToInt32();
				sMaster.StafId = cmbSlsExi.SelectedValue.ToInt32();
				sMaster.RouteId = cmbRoute.SelectedValue.ToInt32();
				sMaster.VehId = cmbVehicle.SelectedValue.ToInt32();
				sMaster.PriceCategoryId = cmbProCategory.SelectedValue.ToInt32();
				sMaster.EntryDate = DateTime.Now.Date;
				sMaster.JEId = Settings.Default.intLogOnId;
				sMaster.Remarks = txtRemarks.Text;
				sMaster.FYearID = iFYearId;
				sMaster.AditionalAmount = txtAddAmnt.Value;
				sMaster.Frieght = txtFreight.Value;
				sMaster.BillDiscount = txtAddDiscAmnt.Value;
				sMaster.GrandDiscount = lblGrandTotal.Value;
				sMaster.TaxableAmount = lblTaxable.Value;
				sMaster.TotalDiscount = lblDisTotal.Value;
				sMaster.TotalTaxAmount = lblTaxTotal.Value;
				sMaster.FloodCess = lblCessAmount.Value;
				sMaster.CGSTAmount = lblTotalCGST.Value;
				sMaster.SGSTAmount = lblTotalSGST.Value;
				sMaster.IGSTAmount = lblTotalIGST.Value;
				sMaster.FK_BranchId = Settings.Default.iBranchId;
				sMaster.FielOne = rtnInvRefNumber.ToString();
				if (lblGrandTotal.Value == lblBalance.Value && cmbPartyAccnt.SelectedValue.ToInt32() == iNormalCashAccountId)
				{
					sMaster.CashPayment = lblGrandTotal.Value;
					sMaster.Credit = 0m;
				}
				else
				{
					sMaster.CashPayment = lblCashPaid.Value;
					sMaster.Credit = lblBalance.Value;
				}
				sMaster.CardPayment = lblCardPaid.Value;
				sMaster.OtherPayment = lblOnline.Value;
				sMaster.ReturnAmount = lblSReturnAmnt.Value;
				sMaster.FreeAmount = FreeAmountTotalFindMethod();
				sMaster.IsClosed = false;
				sMaster.CashAccountId = iCashAcctId;
				sMaster.CardAccountId = iCardAccount;
				sMaster.OnlineAccountId = iOtherAccount;
				sMaster.IsSettiled = ((!(sMaster.Credit > 0m)) ? true : false);
				sMaster.SequenceNo = Utility.uSerialNumber;
				sMaster.Terminal = Utility.uTerminal;
				sMaster.CntrInvNo = Settings.Default.CounterName;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private DataGridViewCell MoveForward(DataGridViewCell _currentCell)
		{
			try
			{
				int index = _currentCell.OwningRow.Index;
				int index2 = _currentCell.OwningColumn.Index;
				DataGridViewCell dataGridViewCell = dgDetails.Rows[index].Cells[index2 + 1];
				if (dataGridViewCell.Visible && !dataGridViewCell.ReadOnly)
				{
					return dataGridViewCell;
				}
				return MoveForward(dataGridViewCell);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}
		}

		private Transaction getCurrent()
		{
			try
			{
				if (dgDetails.CurrentRow != null)
				{
					return (Transaction)dgDetails.CurrentRow.DataBoundItem;
				}
				return null;
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

		private void BindDgv()
		{
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entTransactions;
			dgDetails.DataSource = bdsDetails;
		}

		private void TotalFindMethod()
		{
			decimal num = CommonSettings.strCessPercentage.ToDecimal() / 100m;
			try
			{
				lblTotalQty.Text = entTransactions.Sum((Transaction x) => x.Quantity).ToString(CommonSettings.strQuantityFormat);
				lblGrsTotal.Text = entTransactions.Sum((Transaction x) => x.GrossAmount).ToString(CommonSettings.strCurencyFormat);
				lblDisTotal.Text = entTransactions.Sum((Transaction x) => x.DiscountAmount).ToString(CommonSettings.strCurencyFormat);
				lblTaxable.Text = entTransactions.Sum((Transaction x) => x.Taxable).ToString(CommonSettings.strCurencyFormat);
				lblNetTotal.Text = entTransactions.Sum((Transaction x) => x.NETAmount).ToString(CommonSettings.strCurencyFormat);
				lblTotalSGST.Text = entTransactions.Sum((Transaction x) => x.SGstAmount).ToString(CommonSettings.strCurencyFormat);
				lblTotalCGST.Text = entTransactions.Sum((Transaction x) => x.CGstAmount).ToString(CommonSettings.strCurencyFormat);
				lblTotalIGST.Text = entTransactions.Sum((Transaction x) => x.IGstAmount).ToString(CommonSettings.strCurencyFormat);
				lblTaxTotal.Text = entTransactions.Sum((Transaction x) => x.VatAmount).ToString(CommonSettings.strCurencyFormat);
				if (CommonSettings.blnCessEnabled && cmbSalesType.Text == "Local")
				{
					lblCessAmount.Text = (lblTaxable.Value * num).ToString(CommonSettings.strCurencyFormat);
					lblGrandTotal.Text = (lblNetTotal.Value + txtAddAmnt.Value + lblCessAmount.Value + txtFreight.Value - txtAddDiscAmnt.Value).ToString(CommonSettings.strCurencyFormat);
				}
				else
				{
					lblGrandTotal.Text = (lblNetTotal.Value + txtAddAmnt.Value + txtFreight.Value - txtAddDiscAmnt.Value).ToString(CommonSettings.strCurencyFormat);
				}
				lblBalance.Value = lblGrandTotal.Text.ToDecimal();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void SalesOrderLoadMethod(int iIvoiceId)
		{
			try
			{
				salesOrderMaster = new SalesOrderMaster();
				salesOrderMaster = AshproCommon.GetObjectDetails<SalesOrderMaster>("Select * From SalesOrderMaster Where SalesOrderMasterId='" + iIvoiceId + "'");
				List<SalesOrderSettings> list = new List<SalesOrderSettings>();
				list = AshproCommon.GetListMethod<SalesOrderSettings>("Select * From SalesOrderSettings Where SalesOrderMasterId='" + iIvoiceId + "' ");
				txtRemarks.Text = salesOrderMaster.Remarks;
				txtAddAmnt.Text = salesOrderMaster.AdditionalAmnt.ToString(CommonSettings.strCurencyFormat);
				txtFreight.Text = salesOrderMaster.Freight.ToString(CommonSettings.strCurencyFormat);
				txtAddDiscAmnt.Text = salesOrderMaster.AddDiscount.ToString(CommonSettings.strCurencyFormat);
				dtmDate.Value = salesOrderMaster.InvDate;
				cmbPartyAccnt.SelectedValue = salesOrderMaster.CustomerId.GetValueOrDefault();
				cmbSlsAcct.SelectedValue = salesOrderMaster.SalesAccountId.GetValueOrDefault();
				cmbSlsExi.SelectedValue = salesOrderMaster.SalesManId.GetValueOrDefault();
				cmbRoute.SelectedValue = salesOrderMaster.RouteId.GetValueOrDefault();
				cmbProCategory.SelectedValue = salesOrderMaster.PriceCategoryId.GetValueOrDefault();
				entTransactions = POSHelper.GetTransactionFromSOrder(list);
				dgDetails.AutoGenerateColumns = false;
				dgDetails.DataSource = null;
				bdsDetails.DataSource = null;
				bdsDetails.DataSource = entTransactions;
				dgDetails.DataSource = bdsDetails;
				TotalFindMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void LoadSalesInvoiceFromID()
		{
			try
			{
				iMasterId = entsaleshdr.SalesInvId.ToInt32();
				int? num = iMasterId;
				if (!((num.GetValueOrDefault() == 0) & num.HasValue))
				{
					entsalesdtls = db.SalesDetails(new
					{
						SalesInvId = iMasterId
					}).ToList();
					entTransactions = POSHelper.GetTransactionsFromSDetails(entsalesdtls, db);
					nudInvoiceNo.Value = entsaleshdr.SalesInvNo.toInt32();
					dtmDate.Value = entsaleshdr.SalesDate;
					cmbSlsAcct.SelectedValue = entsaleshdr.SalesAccountId;
					cmbPartyAccnt.SelectedValue = entsaleshdr.PartyAccountId;
					lblCashPaid.Value = entsaleshdr.CashPayment;
					lblCardPaid.Value = entsaleshdr.CardPayment;
					lblSReturnAmnt.Value = entsaleshdr.ReturnAmount;
					cmbRoute.SelectedValue = entsaleshdr.RouteId.GetValueOrDefault();
					cmbVehicle.SelectedValue = entsaleshdr.VehId.GetValueOrDefault();
					cmbProCategory.SelectedValue = entsaleshdr.PriceCategoryId.GetValueOrDefault();
					txtAddAmnt.Value = entsaleshdr.AditionalAmount;
					txtFreight.Value = entsaleshdr.Frieght;
					decimal billDiscount = entsaleshdr.BillDiscount;
					decimal num2 = entTransactions.Sum((Transaction x) => x.NETAmount);
					decimal num3 = default(decimal);
					if (billDiscount > 0m && num2 > 0m)
					{
						num3 = billDiscount / num2 * 100m;
					}
					txtAddDisc.Text = num3.ToString(CommonSettings.strCurencyFormat);
					txtAddDiscAmnt.Value = billDiscount;
					txtRemarks.Text = entsaleshdr.Remarks;
					lblCessAmount.Value = entsaleshdr.FloodCess;
					iCashAcctId = entsaleshdr.CashAccountId;
					iCardAccount = entsaleshdr.CardAccountId;
					iOtherAccount = entsaleshdr.OnlineAccountId;
					BindDgv();
					TotalFindMethod();
					foreach (Control control in base.Controls)
					{
						control.Enabled = false;
					}
					flpButtons.Enabled = true;
					Button button = btnSave;
					btnSearch.Enabled = false;
					button.Enabled = false;
					btnSRtn.Enabled = true;
					btnEdit.Enabled = true;
					btnEnd.Enabled = true;
					btnPrint.Enabled = true;
					btnPrint2.Enabled = true;
					btnClear.Enabled = true;
					nudInvoiceNo.Enabled = true;
					chkEnableInvNo.Enabled = true;
					txtPrintCopy.Enabled = true;
					btnDelete.Enabled = true;
					btnFirst.Enabled = true;
					btnBack.Enabled = true;
					btnNext.Enabled = true;
					btnLast.Enabled = true;
					btnPDF.Enabled = true;
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

		private void DeleteSubMethod()
		{
			try
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
				string[] obj3 = new string[8] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null, null, null };
				num = iMasterId;
				obj3[1] = num.ToString();
				obj3[2] = "' and VchrType='";
				obj3[3] = strVoucherType;
				obj3[4] = "' and VchrName ='";
				obj3[5] = cmbSalesType.Text;
				obj3[6] = "'";
				obj3[7] = sBranchDetails;
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
				AshproCommon.DeleteFromDatabase("SalesEstimateSettings", "SalesInvId", iMasterId.ToInt32());
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ClearFieldMethod()
		{
			entTransactions = new List<Transaction>();
			BindDgv();
			cmbPartyAccnt.SelectedValue = iNormalCashAccountId;
			cmbProCategory.SelectedIndex = 0;
		}

		private void btnClearMethod()
		{
			dtmDate.Value = estimateDate;
			AniHelper.ClearMethod(this);
			isFromOrder = false;
			ClearFieldMethod();
			txtPrintCopy.Data = Settings.Default.iPrintCopy;
			iMasterId = null;
			rtnInvRefNumber = null;
			btnPrint.Enabled = false;
			btnPrint2.Enabled = false;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			btnSearch.Enabled = true;
			cmbPartyAccnt.Focus();
		}

		private void StockAndPurRateFindMethod(Transaction t)
		{
			string barcode = null;
			ProductEntity productEntity = db.Products(new { t.ProductId }).ToList().FirstOrDefault();
			if (productEntity.UnitId == t.UnitId)
			{
				barcode = t.Barcode;
			}
			lblCStock.Text = AshproCommon.GetDecimalDataWithParameter_SP("spProductUnitWiseStock", new
			{
				GodwnId = cmbDepot.SelectedValue.ToInt32(),
				ProductId = t.ProductId,
				BranchId = Settings.Default.iBranchId,
				UnitId = t.UnitId,
				Barcode = barcode
			}).ToString("G29");
			lblLastPurRate.Value = productEntity.PurchasePrice;
		}

		private async void SalesInvoiceView_Load(object sender, EventArgs e)
		{
			try
			{
				base.WindowState = FormWindowState.Maximized;
				db = new DBContext();
				comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
				iNormalCashAccountId = CommonSettings.strDefaultCashAccountId.ToInt32();
				LoadSettingsMethod();
				estimateDate = Settings.Default.appDate;
				LoadComboMethod();
				dtmDate.Value = estimateDate;
				entsalesdtls = new List<SalesDetails>();
				entTransactions = new List<Transaction>();
				BindDgv();
				iFYearId = db.SelectedFYears().FirstOrDefault().FyearId.toInt32();
				if (iInvoiceNo.HasValue)
				{
					nudInvoiceNo.Value = iInvoiceNo.ToInt32();
					cmbSalesType.Text = sVoucherType;
				}
				else
				{
					cmbSalesType.Text = CommonSettings.strDefaultSalesType;
					cmbSalesType_SelectedIndexChanged(null, null);
				}
				txtPrintCopy.Data = Settings.Default.iPrintCopy;
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void cmbPartyAccnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			NameAndBalanceFindMethod();
		}

		private void btnAccCreate_Click(object sender, EventArgs e)
		{
			try
			{
				Account account = new Account();
				account.isOpenFromAnotherForm = true;
				account.sFormName = "Party";
				account.accountGroupName = "CUSTOMER/SUNDRY DEBTORS";
				if (account.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillAccount(cmbPartyAccnt);
					cmbPartyAccnt.SelectedValue = account.Id;
					NameAndBalanceFindMethod();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private void chkEnableInvNo_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (chkEnableInvNo.Checked)
				{
					nudInvoiceNo.Enabled = true;
				}
				else
				{
					nudInvoiceNo.Enabled = false;
				}
				SettingsEntity settingsEntity = new SettingsEntity();
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumber").FirstOrDefault();
				settingsEntity.BitValue = chkEnableInvNo.Checked;
				CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnBalance_Click(object sender, EventArgs e)
		{
			LedgerReport ledgerReport = new LedgerReport();
			ledgerReport.MdiParent = base.MdiParent;
			ledgerReport.ValueFromDate = AniHelper.GetFYearFromDate().ToString();
			ledgerReport.ValueToDate = dtmDate.Text;
			ledgerReport.ValueLedger = cmbPartyAccnt.Text;
			ledgerReport.Show();
		}

		private void btnOrder_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = "Sales Order";
				formSearch.sInvType = "Sales Order";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					iSOrderId = formSearch.iInvoiceId;
					isFromOrder = true;
					SalesOrderLoadMethod(formSearch.iInvoiceId);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSRtn_Click(object sender, EventArgs e)
		{
			if (!CommonSettings.blnEnableSRFromInvoice)
			{
				salesReturn = new SalesReturnView();
				salesReturn.SalesInvId = iMasterId;
				salesReturn.FormClosed += SalesReturn_FormClosed;
				salesReturn.MdiParent = base.MdiParent;
				salesReturn.ShowDialog();
			}
			else
			{
				if (!Messages.DialogMessage("Are you want mark the invoice as Sales Return ?"))
				{
					return;
				}
				AshNumericLabel ashNumericLabel = lblCashPaid;
				AshNumericLabel ashNumericLabel2 = lblCardPaid;
				decimal num2 = (lblOnline.Value = default(decimal));
				decimal num5 = (ashNumericLabel.Value = (ashNumericLabel2.Value = num2));
				btnEdit_Click(null, null);
				rtnInvRefNumber = entsaleshdr.SalesInvNo;
				iMasterId = null;
				if (entTransactions.Count <= 0)
				{
					return;
				}
				foreach (Transaction entTransaction in entTransactions)
				{
					entTransaction.Quantity *= -1m;
					entTransaction.GrossAmount *= -1m;
					entTransaction.DiscountAmount *= -1m;
					entTransaction.Taxable *= -1m;
					entTransaction.GstAmount *= -1m;
					entTransaction.VatAmount *= -1m;
					entTransaction.NETAmount *= -1m;
				}
				TotalFindMethod();
				dgDetails.Refresh();
			}
		}

		private void SalesReturn_FormClosed(object sender, FormClosedEventArgs e)
		{
			lblSReturnAmnt.Value = salesReturn.dmlReturnAmount;
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				SettingsEntity settingsEntity = new SettingsEntity();
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSaving").FirstOrDefault();
				settingsEntity.BitValue = cbPrint.Checked;
				CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		public void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				lblPreBill.Text = lblGrandTotal.Value.ToString();
				if (entTransactions.Count == 0)
				{
					MessageBox.Show("No records to save!", Utility.uManufacturer);
					return;
				}
				entsalesdtls = new List<SalesDetails>();
				entsalesdtls = POSHelper.GetSDetailsFrmTransaction(entTransactions, cmbPartyAccnt.SelectedValue.ToInt32(), dtmDate.Value.Date);
				if (entsalesdtls == null)
				{
					return;
				}
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					entsaleshdr = new SalesMaster();
					int num = 0;
					num = nudInvoiceNo.Value.ToInt32();
					SalesHeaderLoadMethod(entsaleshdr, num);
					DataSet dataSet = new DataSet();
					DataTable dataTable = new DataTable();
					dataTable = AniHelper.ListToDataTable(entsalesdtls);
					dataTable.TableName = "type";
					dataSet.Tables.Add(dataTable);
					iMasterId = AshproCommon.InsertMethod_SP(entsaleshdr, dataSet, "spSalesInvoice", new
					{
						TransactionType = "insert",
						GodownId = cmbDepot.SelectedValue.ToInt32(),
						GodownName = cmbDepot.Text,
						IsReturn = 0
					});
					num = db.SalesMasters(new
					{
						SalesInvId = iMasterId
					}).FirstOrDefault().SalesInvNo.toInt32();
					entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
					POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), num, strVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), strVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
					if (isFromOrder)
					{
						AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
					}
					entsaleshdr.SalesInvNo = num;
					entsaleshdr.CntrInvNo += num;
				}
				else
				{
					if (Utility.uManufacturer != "DeskCloud ERP" && !CommonSettings.blnEnableToEditInvoice)
					{
						Messages.WarningMessage("You can't edit the invoice!");
						return;
					}
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					foreach (SalesDetails entsalesdtl in entsalesdtls)
					{
						entsalesdtl.SalesSettingsId = null;
						entsalesdtl.SalesInvId = iMasterId.ToInt32();
					}
					SalesHeaderLoadMethod(entsaleshdr, nudInvoiceNo.Value.ToInt32());
					DataSet dataSet2 = new DataSet();
					DataTable dataTable2 = new DataTable();
					dataTable2 = AniHelper.ListToDataTable(entsalesdtls);
					dataTable2.TableName = "type";
					dataSet2.Tables.Add(dataTable2);
					iMasterId = AshproCommon.InsertMethod_SP(entsaleshdr, dataSet2, "spSalesInvoice", new
					{
						TransactionType = "update",
						GodownId = cmbDepot.SelectedValue.ToInt32(),
						GodownName = cmbDepot.Text,
						IsReturn = 0
					});
					iMasterId = db.SalesMasters(new
					{
						SalesInvId = iMasterId
					}).FirstOrDefault().SalesInvNo.toInt32();
					int iVchrNo = entsaleshdr.SalesInvNo.toInt32();
					entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
					POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), iVchrNo, strVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), strVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
					if (isFromOrder)
					{
						AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
					}
					entsaleshdr.CntrInvNo += iVchrNo;
				}
				if (cbPrint.Checked)
				{
					PrintMethod();
				}
				if (cbPrint2.Checked)
				{
					btnPrint2_Click(null, null);
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				cmbSalesType.Text = CommonSettings.strDefaultSalesType;
				cmbSalesType_SelectedIndexChanged(null, null);
				btnClearMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
					return;
				}
				if (Messages.DeleteConfirmationMessage())
				{
					DeleteSubMethod();
					AshproCommon.DeleteFromDatabase("SalesEstimateMaster", "SalesInvId", iMasterId.ToInt32());
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in base.Controls)
				{
					control.Enabled = true;
				}
				btnEdit.Enabled = false;
				btnPrint.Enabled = false;
				btnPrint2.Enabled = false;
				btnPDF.Enabled = false;
				btnDelete.Enabled = false;
				btnSave.Enabled = true;
				dgDetails.Focus();
				CellFocusMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void CellFocusMethod()
		{
			if (CommonSettings.blnDefaultCellIsBarcode.toBool())
			{
				dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[col_Barcode.Name];
			}
			else
			{
				dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[col_ProName.Name];
			}
		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			if (lblGrandTotal.Value == 0m)
			{
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			pOSCashPayment.isShowNumberPad = false;
			decimal value = lblGrandTotal.Value;
			isReturn = ((value < 0m) ? true : false);
			pOSCashPayment.isReturn = isReturn;
			pOSCashPayment.lblTitle.Text = (isReturn ? "Refund" : "Payment");
			pOSCashPayment.lblInvoiceAmount.Value = Math.Abs(value);
			pOSCashPayment.txtCashPaid.Value = ((lblCashPaid.Value + lblCardPaid.Value + lblOnline.Value == 0m) ? Math.Abs(value) : lblCashPaid.Value);
			pOSCashPayment.dCashPayment = lblCashPaid.Value;
			decimal num2 = (pOSCashPayment.txtCardPaid.Value = (pOSCashPayment.dCardPayment = lblCardPaid.Value));
			num2 = (pOSCashPayment.txtOtherPaid.Value = (pOSCashPayment.dOtherPayment = lblOnline.Value));
			pOSCashPayment.lblPaidAmount.Value = lblCashPaid.Value + lblCardPaid.Value + lblOnline.Value;
			pOSCashPayment.iCashAccountId = iCashAcctId ?? iNormalCashAccountId;
			pOSCashPayment.iCardAccountId = iCardAccount;
			pOSCashPayment.iOthersAccountId = iOtherAccount;
			pOSCashPayment.iPartyAccountId = cmbPartyAccnt.SelectedValue.ToInt32();
			if (pOSCashPayment.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			lblCashPaid.Value = pOSCashPayment.dCashPayment;
			lblCardPaid.Value = pOSCashPayment.dCardPayment;
			lblOnline.Value = pOSCashPayment.dOtherPayment;
			dTendered = lblCashPaid.Value + lblCardPaid.Value + lblOnline.Value;
			decimal value4 = lblGrandTotal.Value;
			if (isReturn)
			{
				dTendered *= -1m;
			}
			decimal num4 = default(decimal);
			num4 = value4 - dTendered;
			if (num4 < 0m && !isReturn)
			{
				lblCashPaid.Value = value4 - (lblCardPaid.Value + lblOnline.Value);
				if (cmbPartyAccnt.SelectedValue.ToInt32() == iNormalCashAccountId)
				{
					dChanges = Math.Abs(num4);
					lblBalance.Value = 0m;
				}
				else
				{
					dChanges = default(decimal);
					lblBalance.Value = num4;
				}
			}
			else
			{
				lblBalance.Value = num4;
			}
			iCashAcctId = ((lblCashPaid.Value > 0m) ? pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32() : 0);
			iCardAccount = ((lblCardPaid.Value > 0m) ? pOSCashPayment.cmbBankAccount.SelectedValue.ToInt32() : 0);
			iOtherAccount = ((lblOnline.Value > 0m) ? pOSCashPayment.cmbBankOthers.SelectedValue.ToInt32() : 0);
			if (isReturn)
			{
				lblCashPaid.Value *= -1m;
				lblCardPaid.Value *= -1m;
				lblOnline.Value *= -1m;
			}
		}

		private void dgSalesInvoice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += textbox_KeyPress;
					textbox.KeyUp += Textbox_KeyUp;
					textbox.KeyDown += Textbox_KeyDown;
				}
			}
		}

		private void Textbox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name && CommonSettings.blnDefaultCellIsBarcode.toBool())
			{
				dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentRow.Index + 1].Cells[col_Barcode.Name];
			}
		}

		private void textbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				sKeyChar = e.KeyChar.ToString();
				if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name || dgDetails.CurrentCell.OwningColumn.Name == col_Rate.Name)
				{
					if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && !(sender as TextBox).Text.Contains('.')) || (e.KeyChar == '.' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) || (e.KeyChar == '-' && !(sender as TextBox).Text.Contains('-')) || (e.KeyChar == '-' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength))
					{
						return;
					}
					e.Handled = true;
				}
				if (dgDetails.CurrentCell.OwningColumn.Name == col_ProCode.Name)
				{
					dgDetails.NotifyCurrentCellDirty(dirty: true);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				textbox = (DataGridViewTextBoxEditingControl)sender;
				Transaction current = getCurrent();
				int rowIndex = dgDetails.CurrentCell.RowIndex;
				if (dgDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					ShowItemList showItemList = new ShowItemList();
					showItemList.productList = comboFills;
					showItemList.ProName = textbox.Text;
					showItemList.ProPriceCatId = cmbProCategory.SelectedValue.ToInt32();
					showItemList.Location = new Point(dgDetails.Location.X + 200, dgDetails.Location.Y + 100 + textbox.Height + dgDetails.GetCellDisplayRectangle(2, rowIndex, cutOverflow: true).Location.Y);
					if (showItemList.ShowDialog() == DialogResult.OK)
					{
						ExProductSearch proSearch = showItemList.proSearch;
						current.Barcode = proSearch.Barcode;
						dgDetails.CurrentCell.Value = proSearch.ProductName;
						current.ProductId = proSearch.ProductId;
						current.ProductName = proSearch.ProductName;
						current.UnitId = proSearch.UnitId;
						current.Unit = proSearch.UnitName;
						current.Rate = proSearch.SPrice;
						current.Discount = proSearch.SPDisc;
						POSHelper.GetTransactions(current, rowIndex, cmbSalesType.Text);
						StockAndPurRateFindMethod(current);
						dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells["col_Qty"];
						TotalFindMethod();
					}
				}
				else if (dgDetails.CurrentCell.OwningColumn.Name == col_Unit.Name)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer selectCustomer = new SelectCustomer("Unit");
					selectCustomer.ProductId = current.ProductId.toInt32();
					selectCustomer.Location = new Point(dgDetails.Location.X + 200, dgDetails.Location.Y + 100 + textbox.Height + dgDetails.GetCellDisplayRectangle(2, rowIndex, cutOverflow: true).Location.Y);
					if (selectCustomer.ShowDialog() == DialogResult.OK)
					{
						ProductUnit productUnit = db.Productunits(new
						{
							ProductId = current.ProductId,
							UnitId = selectCustomer.iId
						}).FirstOrDefault();
						if (productUnit != null)
						{
							dgDetails.CurrentCell.Value = selectCustomer.sName;
							current.UnitId = selectCustomer.iId;
							current.ProductName = productUnit.ProductUnitName;
							current.Barcode = productUnit.Barcode;
							POSHelper.SellingPriceAndDiscFindMethod(cmbProCategory.SelectedValue.ToInt32(), "Sales Invoice", productUnit.Barcode);
							current.Rate = POSHelper.dmlRate;
							current.Discount = POSHelper.dmlDiscount;
							current.GrossAmount = current.Rate * current.Quantity;
							if (current.Discount > 0m)
							{
								current.DiscountAmount = current.GrossAmount * (current.Discount / 100m);
							}
							else
							{
								current.DiscountAmount = 0m;
							}
							current.Taxable = current.GrossAmount - current.DiscountAmount;
							if (current.Vat > 0m)
							{
								if (!productUnit.IsInclusive)
								{
									current.VatAmount = current.Taxable * (current.Vat / 100m);
									current.NETAmount = current.Taxable + current.VatAmount;
								}
								else
								{
									current.VatAmount = AniHelper.GetInclusiveAmount(current.Taxable, current.Vat);
									current.NETAmount = current.Taxable;
									current.Taxable = current.NETAmount - current.VatAmount;
								}
							}
							else
							{
								current.VatAmount = 0m;
								current.NETAmount = current.Taxable + current.VatAmount;
							}
							current.Gst = current.Vat;
							current.GstAmount = current.VatAmount;
							if (current.Sgst > 0m)
							{
								current.SGstAmount = current.Taxable * (current.Sgst / 100m);
							}
							else
							{
								current.SGstAmount = 0m;
							}
							if (current.CGst > 0m)
							{
								current.CGstAmount = current.Taxable * (current.CGst / 100m);
							}
							else
							{
								current.CGstAmount = 0m;
							}
							if (current.IGst > 0m)
							{
								current.IGstAmount = current.Taxable * (current.IGst / 100m);
							}
							else
							{
								current.IGstAmount = 0m;
							}
							dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells["col_Rate"];
							TotalFindMethod();
						}
					}
				}
				if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name && e.KeyCode == Keys.Return && CommonSettings.blnDefaultCellIsBarcode.toBool())
				{
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[col_Barcode.Name];
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgSalesInvoice_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSalesInvoice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				Transaction current = getCurrent();
				int rowIndex = dgDetails.CurrentCell.RowIndex;
				if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
				{
					ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
					if (current != null)
					{
						productBarcodeEntity = db.ProductBarcodes(new { current.Barcode }).FirstOrDefault();
						if (productBarcodeEntity == null)
						{
							return;
						}
						POSHelper.GetTransactionsFromBarcode(current, productBarcodeEntity, cmbProCategory.SelectedValue.ToInt32(), rowIndex, cmbSalesType.Text);
					}
				}
				else if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
				{
					POSHelper.GetTransactionsAfterEdit(current);
					if (current.Quantity > lblCStock.Value && CommonSettings.blnShowMsgWlNegativeStock)
					{
						Messages.InformationMessage("The quantity you entered is more than the current stock value!");
					}
				}
				else if (dgDetails.CurrentCell.OwningColumn.Name == col_Rate.Name)
				{
					POSHelper.GetTransactionsAfterEdit(current);
				}
				else if (dgDetails.CurrentCell.OwningColumn.Name == col_Dis.Name)
				{
					POSHelper.GetTransactionsAfterEdit(current);
				}
				TotalFindMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtAddDiscAmnt_TextChanged(object sender, EventArgs e)
		{
			TotalFindMethod();
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			ClearFieldMethod();
			entsaleshdr = db.SalesMasters(new
			{
				InvType = cmbSalesType.Text,
				SalesInvNo = nudInvoiceNo.Value.ToInt32(),
				FYearID = iFYearId,
				FK_BranchId = Settings.Default.iBranchId
			}).FirstOrDefault();
			if (entsaleshdr != null)
			{
				LoadSalesInvoiceFromID();
			}
			else
			{
				btnClearMethod();
			}
		}

		private void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = cmbSalesType.Text,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = cmbSalesType.Text;
				formSearch.sInvType = "Sales Invoice";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					entsaleshdr = db.SalesMasters(new
					{
						SalesInvId = formSearch.iInvoiceId
					}).FirstOrDefault();
					LoadSalesInvoiceFromID();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void dgSalesInvoice_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Transaction t = getCurrent();
				if (e.KeyCode == Keys.Delete)
				{
					int iCount = 1;
					entTransactions.Remove(t);
					entTransactions.ForEach(delegate(Transaction x)
					{
						x.SL = iCount;
						iCount++;
					});
					BindDgv();
					CellFocusMethod();
					TotalFindMethod();
				}
				else if (e.KeyCode == Keys.F6)
				{
					ProductTracing frm = new ProductTracing();
					frm.TypeVoucher = "Sales";
					frm.PartyAccntId = cmbPartyAccnt.SelectedValue.ToInt32();
					frm.ProductId = t.ProductId.ToInt32();
					frm.ShowDialog();
				}
				else if (e.KeyCode == Keys.F8)
				{
					if (t != null)
					{
						AniHelper.ItemCreationMethod(t.ProductId);
					}
					else
					{
						AniHelper.ItemCreationMethod();
					}
					comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
				}
				else if (e.KeyCode == Keys.Return && dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name && CommonSettings.blnDefaultCellIsBarcode.toBool())
				{
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentRow.Index + 1].Cells[col_Barcode.Name];
				}
			}
			catch (Exception)
			{
			}
		}

		private void txtAddDisc_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtAddDisc.Text == string.Empty)
				{
					txtAddDiscAmnt.Value = 0m;
					return;
				}
				decimal value = txtAddDisc.Value;
				decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
				decimal num2 = default(decimal);
				if (value > 0m)
				{
					num2 = value / 100m * num;
				}
				txtAddDiscAmnt.Text = num2.ToString(CommonSettings.strCurencyFormat);
				TotalFindMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtAddDiscAmnt_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtAddDiscAmnt.Text == string.Empty)
				{
					txtAddDisc.Value = 0m;
					return;
				}
				decimal value = txtAddDiscAmnt.Value;
				decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
				decimal num2 = default(decimal);
				if (value > 0m && num > 0m)
				{
					num2 = value / num * 100m;
				}
				txtAddDisc.Text = num2.ToString(CommonSettings.strCurencyFormat);
				TotalFindMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
		}

		private void SalesInvoiceView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnEdit_Click(null, null);
			}
			else if (e.KeyCode == Keys.I && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnFirst_Click(null, null);
			}
			else if (e.KeyCode == Keys.K && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBack_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnNext_Click(null, null);
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnLast_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDate_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnAccCreate_Click(null, null);
			}
			else if (e.KeyCode == Keys.O && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnOrder_Click(null, null);
			}
			else if (e.KeyCode == Keys.U && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSRtn_Click(null, null);
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBalance_Click(null, null);
			}
			else if (e.KeyCode == Keys.F && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPDF_Click(null, null);
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSearch_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint2_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnAccCreate_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.F11)
			{
				e.SuppressKeyPress = true;
				if (dgDetails.CurrentRow != null)
				{
					Transaction transaction = (Transaction)dgDetails.CurrentRow.DataBoundItem;
					POSLineEdit pOSLineEdit = new POSLineEdit();
					pOSLineEdit.IsShowInclusive = false;
					pOSLineEdit.pOSDetails = POSHelper.POSDetailsFrmTrans(transaction);
					pOSLineEdit.PCategoryId = cmbProCategory.SelectedValue.ToInt32();
					if (pOSLineEdit.ShowDialog() == DialogResult.OK)
					{
						transaction.Rate = pOSLineEdit.pOSDetails.Rate;
						POSHelper.GetTransactionsAfterEdit(transaction);
						TotalFindMethod();
						dgDetails.Refresh();
					}
				}
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				Transaction current = getCurrent();
				StockAndPurRateFindMethod(current);
			}
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
			nudInvoiceNo.Value++;
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			cmbSalesType_SelectedIndexChanged(null, null);
		}

		private void btnPDF_Click(object sender, EventArgs e)
		{
			PrintMethod(isPDF: true);
		}

		private void cmbPartyAccnt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (CommonSettings.blnDefaultCellIsBarcode.toBool())
				{
					dgDetails.CurrentCell = dgDetails.Rows[0].Cells[col_ProName.Name];
				}
				dgDetails.Focus();
			}
		}

		private void btnPrint2_Click(object sender, EventArgs e)
		{
			entDMaster = db.DotmatrixMasters(new
			{
				VochrType = "Sales Invoice",
				FormType = cmbSalesType.Text,
				PrintType = "Invoice"
			}).FirstOrDefault();
			AppPrintingMethod(isPDF: false);
		}

		private void btnReceiptAndPayment_Click(object sender, EventArgs e)
		{
			PaymentAndReceiptView paymentAndReceiptView = new PaymentAndReceiptView();
			paymentAndReceiptView.ShowDialog();
		}

		private void txtPrintCopy_Validated(object sender, EventArgs e)
		{
			if (txtPrintCopy.Data > 0)
			{
				Settings.Default.iPrintCopy = txtPrintCopy.Data;
				Settings.Default.Save();
			}
		}

		private void btnDiscount_Click(object sender, EventArgs e)
		{
			if (entTransactions.Count == 0)
			{
				return;
			}
			dSubDiscount = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Discount Percentage", "Discount :", isPassword: false, dSubDiscount.ToString()).ToDecimal();
			foreach (Transaction entTransaction in entTransactions)
			{
				entTransaction.Discount = dSubDiscount;
				POSHelper.GetTransactionsAfterEdit(entTransaction);
			}
			dgDetails.Refresh();
			TotalFindMethod();
		}

		private void btnCustomerSearch_Click(object sender, EventArgs e)
		{
			AniHelper.CustomerSerchMethod(cmbPartyAccnt);
			base.ActiveControl = dgDetails;
			dgDetails.CurrentCell = dgDetails.Rows[0].Cells[col_ProName.Name];
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesInvoiceView));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label25 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnableInvNo = new System.Windows.Forms.CheckBox();
            this.btnDate = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.btnBalance = new System.Windows.Forms.Button();
            this.lblPreBill = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAccCreate = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lblCessAmount = new AshUsercontrol.AshNumericLabel();
            this.lblTotalIGST = new AshUsercontrol.AshNumericLabel();
            this.lblTotalCGST = new AshUsercontrol.AshNumericLabel();
            this.lblTotalSGST = new AshUsercontrol.AshNumericLabel();
            this.lblTaxTotal = new AshUsercontrol.AshNumericLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.lblGrandTotal = new AshUsercontrol.AshNumericLabel();
            this.label36 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cbPrint = new System.Windows.Forms.CheckBox();
            this.lblTaxable = new AshUsercontrol.AshNumericLabel();
            this.lblNetTotal = new AshUsercontrol.AshNumericLabel();
            this.lblDisTotal = new AshUsercontrol.AshNumericLabel();
            this.lblGrsTotal = new AshUsercontrol.AshNumericLabel();
            this.lblTotalQty = new AshUsercontrol.AshNumericLabel();
            this.lblUntQty = new AshUsercontrol.AshNumericLabel();
            this.lblLastPurRate = new AshUsercontrol.AshNumericLabel();
            this.lblCStock = new AshUsercontrol.AshNumericLabel();
            this.btnSRtn = new System.Windows.Forms.Button();
            this.txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddDisc = new AshUsercontrol.AshTextBoxNormal();
            this.txtFreight = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.cmbSalesType = new AshUsercontrol.AshComboBox();
            this.cmbProCategory = new AshUsercontrol.AshComboBox();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.cmbSlsExi = new AshUsercontrol.AshComboBox();
            this.cmbPartyAccnt = new AshUsercontrol.AshComboBox();
            this.cmbVehicle = new AshUsercontrol.AshComboBox();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.cmbSlsAcct = new AshUsercontrol.AshComboBox();
            this.nudInvoiceNo = new AshUsercontrol.AshUpDown();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.dgDetails = new AshUsercontrol.AshDataGridview();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.btnPayment = new System.Windows.Forms.Button();
            this.lblBalance = new AshUsercontrol.AshNumericLabel();
            this.lblCashPaid = new AshUsercontrol.AshNumericLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblSReturnAmnt = new AshUsercontrol.AshNumericLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDepot = new AshUsercontrol.AshComboBox();
            this.btnSeperator2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCardPaid = new AshUsercontrol.AshNumericLabel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblOnline = new AshUsercontrol.AshNumericLabel();
            this.lblRunBal = new System.Windows.Forms.Label();
            this.btnPrint2 = new System.Windows.Forms.Button();
            this.cbPrint2 = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnReceiptAndPayment = new System.Windows.Forms.Button();
            this.btnCustomerSearch = new System.Windows.Forms.Button();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_HSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Dis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_VatAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_GST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_GSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_IGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_IGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NetAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.flpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(1038, 1);
            this.btnEnd.Size = new System.Drawing.Size(41, 35);
            this.btnEnd.TabIndex = 43;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label25.Location = new System.Drawing.Point(2, 6);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 24);
            this.label25.TabIndex = 31;
            this.label25.Text = "Sales Invoice";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(141, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Inv No:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(694, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Date:";
            // 
            // chkEnableInvNo
            // 
            this.chkEnableInvNo.AutoSize = true;
            this.chkEnableInvNo.Location = new System.Drawing.Point(323, 11);
            this.chkEnableInvNo.Name = "chkEnableInvNo";
            this.chkEnableInvNo.Size = new System.Drawing.Size(15, 14);
            this.chkEnableInvNo.TabIndex = 34;
            this.chkEnableInvNo.TabStop = false;
            this.chkEnableInvNo.UseVisualStyleBackColor = true;
            this.chkEnableInvNo.CheckedChanged += new System.EventHandler(this.chkEnableInvNo_CheckedChanged);
            // 
            // btnDate
            // 
            this.btnDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDate.Location = new System.Drawing.Point(879, 8);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(22, 20);
            this.btnDate.TabIndex = 42;
            this.btnDate.TabStop = false;
            this.btnDate.Text = "...";
            this.toolTip.SetToolTip(this.btnDate, "Press Alt + A");
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label32.Location = new System.Drawing.Point(607, 169);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(94, 16);
            this.label32.TabIndex = 20;
            this.label32.Text = "Running Bal :";
            // 
            // btnBalance
            // 
            this.btnBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBalance.Location = new System.Drawing.Point(932, 170);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(26, 23);
            this.btnBalance.TabIndex = 47;
            this.btnBalance.TabStop = false;
            this.btnBalance.Text = "...";
            this.toolTip.SetToolTip(this.btnBalance, "Press Alt + B");
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // lblPreBill
            // 
            this.lblPreBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblPreBill.ForeColor = System.Drawing.Color.Red;
            this.lblPreBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPreBill.Location = new System.Drawing.Point(720, 137);
            this.lblPreBill.Name = "lblPreBill";
            this.lblPreBill.Size = new System.Drawing.Size(207, 23);
            this.lblPreBill.TabIndex = 19;
            this.lblPreBill.Text = "0.00";
            this.lblPreBill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label19.Location = new System.Drawing.Point(605, 141);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 16);
            this.label19.TabIndex = 18;
            this.label19.Text = "Previous Bill :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label13.Location = new System.Drawing.Point(42, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Price Category :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label12.Location = new System.Drawing.Point(85, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 16);
            this.label12.TabIndex = 10;
            this.label12.Text = "Remarks :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label11.Location = new System.Drawing.Point(81, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "Sales Exi :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label6.Location = new System.Drawing.Point(66, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cash/Party :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label5.Location = new System.Drawing.Point(92, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Veh No :";
            // 
            // btnAccCreate
            // 
            this.btnAccCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccCreate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAccCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccCreate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAccCreate.ForeColor = System.Drawing.Color.White;
            this.btnAccCreate.Location = new System.Drawing.Point(545, 47);
            this.btnAccCreate.Name = "btnAccCreate";
            this.btnAccCreate.Size = new System.Drawing.Size(30, 26);
            this.btnAccCreate.TabIndex = 39;
            this.btnAccCreate.TabStop = false;
            this.btnAccCreate.Text = "+";
            this.toolTip.SetToolTip(this.btnAccCreate, "Press Alt + L");
            this.btnAccCreate.UseVisualStyleBackColor = false;
            this.btnAccCreate.Click += new System.EventHandler(this.btnAccCreate_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOrder.FlatAppearance.BorderSize = 0;
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(966, 55);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(97, 30);
            this.btnOrder.TabIndex = 44;
            this.btnOrder.TabStop = false;
            this.btnOrder.Text = "Order";
            this.toolTip.SetToolTip(this.btnOrder, "Press Alt + O");
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(101, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Route :";
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label39.Location = new System.Drawing.Point(609, 50);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(92, 16);
            this.label39.TabIndex = 12;
            this.label39.Text = "Sales Type :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(618, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sales A/C :";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(282, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 30);
            this.btnClear.TabIndex = 82;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.toolTip.SetToolTip(this.btnClear, "Press Alt + C");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(375, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 30);
            this.btnPrint.TabIndex = 81;
            this.btnPrint.TabStop = false;
            this.btnPrint.Tag = "1";
            this.btnPrint.Text = "Print";
            this.toolTip.SetToolTip(this.btnPrint, "Press Alt + P");
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(189, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 30);
            this.btnDelete.TabIndex = 83;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Delete";
            this.toolTip.SetToolTip(this.btnDelete, "Press Alt + D");
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(96, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 30);
            this.btnEdit.TabIndex = 84;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "Edit";
            this.toolTip.SetToolTip(this.btnEdit, "Press Alt + E");
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 30);
            this.btnSave.TabIndex = 85;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.toolTip.SetToolTip(this.btnSave, "Press Alt + S");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label37.Location = new System.Drawing.Point(855, 523);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(90, 16);
            this.label37.TabIndex = 76;
            this.label37.Text = "Flood Cess :";
            this.label37.Visible = false;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label23.Location = new System.Drawing.Point(855, 499);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(90, 16);
            this.label23.TabIndex = 74;
            this.label23.Text = "Total IGST :";
            this.label23.Visible = false;
            // 
            // label93
            // 
            this.label93.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label93.Location = new System.Drawing.Point(851, 474);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(94, 16);
            this.label93.TabIndex = 72;
            this.label93.Text = "Total CGST :";
            this.label93.Visible = false;
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label90.Location = new System.Drawing.Point(851, 452);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(94, 16);
            this.label90.TabIndex = 70;
            this.label90.Text = "Total SGST :";
            this.label90.Visible = false;
            // 
            // lblCessAmount
            // 
            this.lblCessAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCessAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCessAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCessAmount.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCessAmount.Format = null;
            this.lblCessAmount.Location = new System.Drawing.Point(959, 523);
            this.lblCessAmount.Name = "lblCessAmount";
            this.lblCessAmount.Size = new System.Drawing.Size(106, 20);
            this.lblCessAmount.TabIndex = 77;
            this.lblCessAmount.Text = "0";
            this.lblCessAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCessAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lblCessAmount.Visible = false;
            // 
            // lblTotalIGST
            // 
            this.lblTotalIGST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalIGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalIGST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalIGST.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalIGST.Format = null;
            this.lblTotalIGST.Location = new System.Drawing.Point(959, 499);
            this.lblTotalIGST.Name = "lblTotalIGST";
            this.lblTotalIGST.Size = new System.Drawing.Size(106, 20);
            this.lblTotalIGST.TabIndex = 75;
            this.lblTotalIGST.Text = "0";
            this.lblTotalIGST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalIGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lblTotalIGST.Visible = false;
            // 
            // lblTotalCGST
            // 
            this.lblTotalCGST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalCGST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalCGST.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalCGST.Format = null;
            this.lblTotalCGST.Location = new System.Drawing.Point(959, 475);
            this.lblTotalCGST.Name = "lblTotalCGST";
            this.lblTotalCGST.Size = new System.Drawing.Size(106, 20);
            this.lblTotalCGST.TabIndex = 73;
            this.lblTotalCGST.Text = "0";
            this.lblTotalCGST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalCGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lblTotalCGST.Visible = false;
            // 
            // lblTotalSGST
            // 
            this.lblTotalSGST.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalSGST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalSGST.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalSGST.Format = null;
            this.lblTotalSGST.Location = new System.Drawing.Point(959, 448);
            this.lblTotalSGST.Name = "lblTotalSGST";
            this.lblTotalSGST.Size = new System.Drawing.Size(106, 23);
            this.lblTotalSGST.TabIndex = 71;
            this.lblTotalSGST.Text = "0";
            this.lblTotalSGST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalSGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lblTotalSGST.Visible = false;
            // 
            // lblTaxTotal
            // 
            this.lblTaxTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTaxTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTaxTotal.Format = null;
            this.lblTaxTotal.Location = new System.Drawing.Point(959, 549);
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.lblTaxTotal.Size = new System.Drawing.Size(106, 23);
            this.lblTaxTotal.TabIndex = 79;
            this.lblTaxTotal.Text = "0";
            this.lblTaxTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTaxTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F);
            this.label21.Location = new System.Drawing.Point(846, 548);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 20);
            this.label21.TabIndex = 78;
            this.label21.Text = "Total Tax :";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.ForeColor = System.Drawing.Color.Red;
            this.lblGrandTotal.Format = null;
            this.lblGrandTotal.Location = new System.Drawing.Point(174, 549);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(121, 23);
            this.lblGrandTotal.TabIndex = 57;
            this.lblGrandTotal.Text = "0";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrandTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F);
            this.label36.Location = new System.Drawing.Point(47, 548);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(118, 20);
            this.label36.TabIndex = 56;
            this.label36.Text = "Grand Total :";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label15.Location = new System.Drawing.Point(50, 523);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 16);
            this.label15.TabIndex = 54;
            this.label15.Text = "Discount Amnt :";
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label35.Location = new System.Drawing.Point(64, 499);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(101, 16);
            this.label35.TabIndex = 52;
            this.label35.Text = "Discount(%) :";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label34.Location = new System.Drawing.Point(100, 474);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 16);
            this.label34.TabIndex = 50;
            this.label34.Text = "Freight :";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label33.Location = new System.Drawing.Point(51, 452);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(114, 16);
            this.label33.TabIndex = 48;
            this.label33.Text = "Aditional Amnt :";
            // 
            // cbPrint
            // 
            this.cbPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPrint.AutoSize = true;
            this.cbPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cbPrint.Location = new System.Drawing.Point(7, 582);
            this.cbPrint.Name = "cbPrint";
            this.cbPrint.Size = new System.Drawing.Size(57, 20);
            this.cbPrint.TabIndex = 87;
            this.cbPrint.TabStop = false;
            this.cbPrint.Text = "Print";
            this.cbPrint.UseVisualStyleBackColor = true;
            this.cbPrint.CheckedChanged += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // lblTaxable
            // 
            this.lblTaxable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxable.AutoSize = true;
            this.lblTaxable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTaxable.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTaxable.Format = null;
            this.lblTaxable.Location = new System.Drawing.Point(895, 427);
            this.lblTaxable.Name = "lblTaxable";
            this.lblTaxable.Size = new System.Drawing.Size(14, 13);
            this.lblTaxable.TabIndex = 29;
            this.lblTaxable.Text = "0";
            this.lblTaxable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTaxable.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblNetTotal
            // 
            this.lblNetTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNetTotal.AutoSize = true;
            this.lblNetTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNetTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNetTotal.Format = null;
            this.lblNetTotal.Location = new System.Drawing.Point(993, 427);
            this.lblNetTotal.Name = "lblNetTotal";
            this.lblNetTotal.Size = new System.Drawing.Size(14, 13);
            this.lblNetTotal.TabIndex = 30;
            this.lblNetTotal.Text = "0";
            this.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNetTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblDisTotal
            // 
            this.lblDisTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDisTotal.AutoSize = true;
            this.lblDisTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDisTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDisTotal.Format = null;
            this.lblDisTotal.Location = new System.Drawing.Point(795, 427);
            this.lblDisTotal.Name = "lblDisTotal";
            this.lblDisTotal.Size = new System.Drawing.Size(14, 13);
            this.lblDisTotal.TabIndex = 28;
            this.lblDisTotal.Text = "0";
            this.lblDisTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDisTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblGrsTotal
            // 
            this.lblGrsTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGrsTotal.AutoSize = true;
            this.lblGrsTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGrsTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblGrsTotal.Format = null;
            this.lblGrsTotal.Location = new System.Drawing.Point(674, 427);
            this.lblGrsTotal.Name = "lblGrsTotal";
            this.lblGrsTotal.Size = new System.Drawing.Size(14, 13);
            this.lblGrsTotal.TabIndex = 27;
            this.lblGrsTotal.Text = "0";
            this.lblGrsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGrsTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalQty.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalQty.Format = null;
            this.lblTotalQty.Location = new System.Drawing.Point(573, 427);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(14, 13);
            this.lblTotalQty.TabIndex = 26;
            this.lblTotalQty.Text = "0";
            this.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotalQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblUntQty
            // 
            this.lblUntQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUntQty.AutoSize = true;
            this.lblUntQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblUntQty.ForeColor = System.Drawing.Color.Magenta;
            this.lblUntQty.Format = null;
            this.lblUntQty.Location = new System.Drawing.Point(154, 427);
            this.lblUntQty.Name = "lblUntQty";
            this.lblUntQty.Size = new System.Drawing.Size(14, 13);
            this.lblUntQty.TabIndex = 24;
            this.lblUntQty.Text = "0";
            this.lblUntQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUntQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.lblUntQty.Visible = false;
            // 
            // lblLastPurRate
            // 
            this.lblLastPurRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastPurRate.AutoSize = true;
            this.lblLastPurRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLastPurRate.ForeColor = System.Drawing.Color.Red;
            this.lblLastPurRate.Format = null;
            this.lblLastPurRate.Location = new System.Drawing.Point(274, 427);
            this.lblLastPurRate.Name = "lblLastPurRate";
            this.lblLastPurRate.Size = new System.Drawing.Size(14, 13);
            this.lblLastPurRate.TabIndex = 25;
            this.lblLastPurRate.Text = "0";
            this.lblLastPurRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLastPurRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblCStock
            // 
            this.lblCStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCStock.AutoSize = true;
            this.lblCStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCStock.ForeColor = System.Drawing.Color.Red;
            this.lblCStock.Format = null;
            this.lblCStock.Location = new System.Drawing.Point(57, 427);
            this.lblCStock.Name = "lblCStock";
            this.lblCStock.Size = new System.Drawing.Size(14, 13);
            this.lblCStock.TabIndex = 23;
            this.lblCStock.Text = "0";
            this.lblCStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnSRtn
            // 
            this.btnSRtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSRtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSRtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSRtn.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSRtn.ForeColor = System.Drawing.Color.White;
            this.btnSRtn.Location = new System.Drawing.Point(966, 137);
            this.btnSRtn.Name = "btnSRtn";
            this.btnSRtn.Size = new System.Drawing.Size(97, 30);
            this.btnSRtn.TabIndex = 46;
            this.btnSRtn.TabStop = false;
            this.btnSRtn.Text = "Return";
            this.toolTip.SetToolTip(this.btnSRtn, "Press Alt + U");
            this.btnSRtn.UseVisualStyleBackColor = false;
            this.btnSRtn.Click += new System.EventHandler(this.btnSRtn_Click);
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
            this.txtPrintCopy.Location = new System.Drawing.Point(153, 585);
            this.txtPrintCopy.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtPrintCopy.Name = "txtPrintCopy";
            this.txtPrintCopy.Size = new System.Drawing.Size(51, 16);
            this.txtPrintCopy.TabIndex = 88;
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
            // txtAddDiscAmnt
            // 
            this.txtAddDiscAmnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAddDiscAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddDiscAmnt.Data = 0;
            this.txtAddDiscAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddDiscAmnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtAddDiscAmnt.Format = null;
            this.txtAddDiscAmnt.isAllowNegative = false;
            this.txtAddDiscAmnt.isAllowSpecialChar = false;
            this.txtAddDiscAmnt.isNumberOnly = false;
            this.txtAddDiscAmnt.isNumeric = true;
            this.txtAddDiscAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtAddDiscAmnt.Location = new System.Drawing.Point(174, 527);
            this.txtAddDiscAmnt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtAddDiscAmnt.Name = "txtAddDiscAmnt";
            this.txtAddDiscAmnt.Size = new System.Drawing.Size(122, 16);
            this.txtAddDiscAmnt.TabIndex = 55;
            this.txtAddDiscAmnt.Text = "0.00";
            this.txtAddDiscAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAddDiscAmnt.WaterMark = null;
            this.txtAddDiscAmnt.Validated += new System.EventHandler(this.txtAddDiscAmnt_Validated);
            // 
            // txtAddDisc
            // 
            this.txtAddDisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAddDisc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddDisc.Data = 0;
            this.txtAddDisc.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddDisc.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtAddDisc.Format = null;
            this.txtAddDisc.isAllowNegative = false;
            this.txtAddDisc.isAllowSpecialChar = false;
            this.txtAddDisc.isNumberOnly = false;
            this.txtAddDisc.isNumeric = true;
            this.txtAddDisc.LeaveColor = System.Drawing.Color.White;
            this.txtAddDisc.Location = new System.Drawing.Point(174, 503);
            this.txtAddDisc.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtAddDisc.Name = "txtAddDisc";
            this.txtAddDisc.Size = new System.Drawing.Size(92, 16);
            this.txtAddDisc.TabIndex = 53;
            this.txtAddDisc.Text = "0.00";
            this.txtAddDisc.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAddDisc.WaterMark = null;
            this.txtAddDisc.Validated += new System.EventHandler(this.txtAddDisc_Validated);
            // 
            // txtFreight
            // 
            this.txtFreight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFreight.Data = 0;
            this.txtFreight.EnterColor = System.Drawing.Color.LightPink;
            this.txtFreight.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtFreight.Format = null;
            this.txtFreight.isAllowNegative = false;
            this.txtFreight.isAllowSpecialChar = false;
            this.txtFreight.isNumberOnly = false;
            this.txtFreight.isNumeric = true;
            this.txtFreight.LeaveColor = System.Drawing.Color.White;
            this.txtFreight.Location = new System.Drawing.Point(174, 479);
            this.txtFreight.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtFreight.Name = "txtFreight";
            this.txtFreight.Size = new System.Drawing.Size(121, 16);
            this.txtFreight.TabIndex = 51;
            this.txtFreight.Text = "0.00";
            this.txtFreight.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtFreight.WaterMark = null;
            this.txtFreight.TextChanged += new System.EventHandler(this.txtAddDiscAmnt_TextChanged);
            // 
            // txtAddAmnt
            // 
            this.txtAddAmnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAddAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddAmnt.Data = 0;
            this.txtAddAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddAmnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtAddAmnt.Format = null;
            this.txtAddAmnt.isAllowNegative = false;
            this.txtAddAmnt.isAllowSpecialChar = false;
            this.txtAddAmnt.isNumberOnly = false;
            this.txtAddAmnt.isNumeric = true;
            this.txtAddAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtAddAmnt.Location = new System.Drawing.Point(174, 455);
            this.txtAddAmnt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtAddAmnt.Name = "txtAddAmnt";
            this.txtAddAmnt.Size = new System.Drawing.Size(124, 16);
            this.txtAddAmnt.TabIndex = 49;
            this.txtAddAmnt.Text = "0.00";
            this.txtAddAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAddAmnt.WaterMark = null;
            this.txtAddAmnt.TextChanged += new System.EventHandler(this.txtAddDiscAmnt_TextChanged);
            // 
            // cmbSalesType
            // 
            this.cmbSalesType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSalesType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSalesType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSalesType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbSalesType.FormattingEnabled = true;
            this.cmbSalesType.LeaveColor = System.Drawing.Color.White;
            this.cmbSalesType.Location = new System.Drawing.Point(720, 46);
            this.cmbSalesType.Name = "cmbSalesType";
            this.cmbSalesType.Size = new System.Drawing.Size(238, 24);
            this.cmbSalesType.TabIndex = 13;
            this.cmbSalesType.SelectedIndexChanged += new System.EventHandler(this.cmbSalesType_SelectedIndexChanged);
            // 
            // cmbProCategory
            // 
            this.cmbProCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProCategory.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbProCategory.FormattingEnabled = true;
            this.cmbProCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbProCategory.Location = new System.Drawing.Point(170, 130);
            this.cmbProCategory.Name = "cmbProCategory";
            this.cmbProCategory.Size = new System.Drawing.Size(371, 24);
            this.cmbProCategory.TabIndex = 7;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Data = 0;
            this.txtRemarks.EnterColor = System.Drawing.Color.LightPink;
            this.txtRemarks.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtRemarks.Format = null;
            this.txtRemarks.isAllowNegative = false;
            this.txtRemarks.isAllowSpecialChar = false;
            this.txtRemarks.isNumberOnly = false;
            this.txtRemarks.isNumeric = false;
            this.txtRemarks.LeaveColor = System.Drawing.Color.White;
            this.txtRemarks.Location = new System.Drawing.Point(170, 194);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(372, 16);
            this.txtRemarks.TabIndex = 11;
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRemarks.WaterMark = null;
            // 
            // cmbSlsExi
            // 
            this.cmbSlsExi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSlsExi.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSlsExi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSlsExi.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbSlsExi.FormattingEnabled = true;
            this.cmbSlsExi.LeaveColor = System.Drawing.Color.White;
            this.cmbSlsExi.Location = new System.Drawing.Point(170, 75);
            this.cmbSlsExi.Name = "cmbSlsExi";
            this.cmbSlsExi.Size = new System.Drawing.Size(371, 24);
            this.cmbSlsExi.TabIndex = 3;
            // 
            // cmbPartyAccnt
            // 
            this.cmbPartyAccnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAccnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPartyAccnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbPartyAccnt.FormattingEnabled = true;
            this.cmbPartyAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAccnt.Location = new System.Drawing.Point(169, 47);
            this.cmbPartyAccnt.Name = "cmbPartyAccnt";
            this.cmbPartyAccnt.Size = new System.Drawing.Size(373, 24);
            this.cmbPartyAccnt.TabIndex = 1;
            this.cmbPartyAccnt.SelectedIndexChanged += new System.EventHandler(this.cmbPartyAccnt_SelectedIndexChanged);
            this.cmbPartyAccnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPartyAccnt_KeyDown);
            // 
            // cmbVehicle
            // 
            this.cmbVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
            this.cmbVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVehicle.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbVehicle.FormattingEnabled = true;
            this.cmbVehicle.LeaveColor = System.Drawing.Color.White;
            this.cmbVehicle.Location = new System.Drawing.Point(170, 158);
            this.cmbVehicle.Name = "cmbVehicle";
            this.cmbVehicle.Size = new System.Drawing.Size(371, 24);
            this.cmbVehicle.TabIndex = 9;
            // 
            // cmbRoute
            // 
            this.cmbRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Location = new System.Drawing.Point(170, 103);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(371, 24);
            this.cmbRoute.TabIndex = 5;
            // 
            // cmbSlsAcct
            // 
            this.cmbSlsAcct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSlsAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSlsAcct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSlsAcct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbSlsAcct.FormattingEnabled = true;
            this.cmbSlsAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbSlsAcct.Location = new System.Drawing.Point(720, 76);
            this.cmbSlsAcct.Name = "cmbSlsAcct";
            this.cmbSlsAcct.Size = new System.Drawing.Size(238, 24);
            this.cmbSlsAcct.TabIndex = 15;
            this.cmbSlsAcct.TabStop = false;
            // 
            // nudInvoiceNo
            // 
            this.nudInvoiceNo.Enabled = false;
            this.nudInvoiceNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.nudInvoiceNo.Location = new System.Drawing.Point(200, 7);
            this.nudInvoiceNo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.nudInvoiceNo.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nudInvoiceNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInvoiceNo.Name = "nudInvoiceNo";
            this.nudInvoiceNo.Size = new System.Drawing.Size(119, 23);
            this.nudInvoiceNo.TabIndex = 33;
            this.nudInvoiceNo.TabStop = false;
            this.nudInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudInvoiceNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInvoiceNo.ValueChanged += new System.EventHandler(this.nudInvoiceNo_ValueChanged);
            // 
            // dtmDate
            // 
            this.dtmDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtmDate.CustomFormat = "dd-MMM-yyyy";
            this.dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Location = new System.Drawing.Point(747, 6);
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.Size = new System.Drawing.Size(129, 23);
            this.dtmDate.TabIndex = 41;
            this.dtmDate.TabStop = false;
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.ColumnHeadersHeight = 40;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ProCode,
            this.col_ProName,
            this.col_Barcode,
            this.col_HSN,
            this.col_Qty,
            this.col_Unit,
            this.col_Rate,
            this.col_Gross,
            this.col_Dis,
            this.col_Discount,
            this.col_Taxable,
            this.col_Vat,
            this.col_VatAmnt,
            this.col_GST,
            this.col_GSTAmnt,
            this.col_SGST,
            this.col_SGSTAmnt,
            this.col_CGST,
            this.col_CGSTAmnt,
            this.col_IGST,
            this.col_IGSTAmnt,
            this.col_NetAmnt});
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.EnterKeyNavigation = false;
            this.dgDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgDetails.Location = new System.Drawing.Point(6, 217);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.RowTemplate.Height = 25;
            this.dgDetails.Size = new System.Drawing.Size(1064, 207);
            this.dgDetails.TabIndex = 22;
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            this.dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSalesInvoice_CellEndEdit);
            this.dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgSalesInvoice_DataError);
            this.dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgSalesInvoice_EditingControlShowing);
            this.dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgSalesInvoice_KeyDown);
            // 
            // btnPayment
            // 
            this.btnPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPayment.ForeColor = System.Drawing.Color.White;
            this.btnPayment.Location = new System.Drawing.Point(966, 96);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(97, 30);
            this.btnPayment.TabIndex = 45;
            this.btnPayment.TabStop = false;
            this.btnPayment.Text = "Payment";
            this.toolTip.SetToolTip(this.btnPayment, "Press Alt + Y");
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // lblBalance
            // 
            this.lblBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalance.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblBalance.Format = null;
            this.lblBalance.Location = new System.Drawing.Point(699, 544);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(131, 16);
            this.lblBalance.TabIndex = 68;
            this.lblBalance.Text = "0";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBalance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblCashPaid
            // 
            this.lblCashPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCashPaid.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblCashPaid.Format = null;
            this.lblCashPaid.Location = new System.Drawing.Point(699, 479);
            this.lblCashPaid.Name = "lblCashPaid";
            this.lblCashPaid.Size = new System.Drawing.Size(131, 16);
            this.lblCashPaid.TabIndex = 62;
            this.lblCashPaid.Text = "0";
            this.lblCashPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCashPaid.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label16.Location = new System.Drawing.Point(550, 544);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 16);
            this.label16.TabIndex = 67;
            this.label16.Text = "Balance Amount :";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label18.Location = new System.Drawing.Point(562, 479);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 16);
            this.label18.TabIndex = 61;
            this.label18.Text = "Cash Payment :";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label20.Location = new System.Drawing.Point(573, 454);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(102, 16);
            this.label20.TabIndex = 59;
            this.label20.Text = "Sales Return :";
            // 
            // lblSReturnAmnt
            // 
            this.lblSReturnAmnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSReturnAmnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblSReturnAmnt.Format = null;
            this.lblSReturnAmnt.Location = new System.Drawing.Point(699, 454);
            this.lblSReturnAmnt.Name = "lblSReturnAmnt";
            this.lblSReturnAmnt.Size = new System.Drawing.Size(131, 16);
            this.lblSReturnAmnt.TabIndex = 60;
            this.lblSReturnAmnt.Text = "0";
            this.lblSReturnAmnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSReturnAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label7.Location = new System.Drawing.Point(643, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Depot :";
            // 
            // cmbDepot
            // 
            this.cmbDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDepot.EnterColor = System.Drawing.Color.LightPink;
            this.cmbDepot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDepot.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.LeaveColor = System.Drawing.Color.White;
            this.cmbDepot.Location = new System.Drawing.Point(720, 102);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(238, 24);
            this.cmbDepot.TabIndex = 17;
            // 
            // btnSeperator2
            // 
            this.btnSeperator2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSeperator2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSeperator2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeperator2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSeperator2.Location = new System.Drawing.Point(317, 454);
            this.btnSeperator2.Name = "btnSeperator2";
            this.btnSeperator2.Size = new System.Drawing.Size(2, 114);
            this.btnSeperator2.TabIndex = 58;
            this.btnSeperator2.TabStop = false;
            this.btnSeperator2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(836, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(2, 114);
            this.button1.TabIndex = 69;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(654, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 30);
            this.btnSearch.TabIndex = 80;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search";
            this.toolTip.SetToolTip(this.btnSearch, "Press Alt + R");
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label8.Location = new System.Drawing.Point(564, 503);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 63;
            this.label8.Text = "Card Payment :";
            // 
            // lblCardPaid
            // 
            this.lblCardPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCardPaid.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblCardPaid.Format = null;
            this.lblCardPaid.Location = new System.Drawing.Point(699, 503);
            this.lblCardPaid.Name = "lblCardPaid";
            this.lblCardPaid.Size = new System.Drawing.Size(131, 16);
            this.lblCardPaid.TabIndex = 64;
            this.lblCardPaid.Text = "0";
            this.lblCardPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardPaid.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnFirst.ForeColor = System.Drawing.Color.White;
            this.btnFirst.Location = new System.Drawing.Point(364, 3);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(35, 26);
            this.btnFirst.TabIndex = 35;
            this.btnFirst.TabStop = false;
            this.btnFirst.Text = "<<";
            this.toolTip.SetToolTip(this.btnFirst, "Press Alt + I");
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(398, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 26);
            this.btnBack.TabIndex = 36;
            this.btnBack.TabStop = false;
            this.btnBack.Text = "<";
            this.toolTip.SetToolTip(this.btnBack, "Press Alt + K");
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(427, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 26);
            this.btnNext.TabIndex = 37;
            this.btnNext.TabStop = false;
            this.btnNext.Text = ">";
            this.toolTip.SetToolTip(this.btnNext, "Press Alt + X");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnLast.ForeColor = System.Drawing.Color.White;
            this.btnLast.Location = new System.Drawing.Point(456, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(35, 26);
            this.btnLast.TabIndex = 38;
            this.btnLast.TabStop = false;
            this.btnLast.Text = ">>";
            this.toolTip.SetToolTip(this.btnLast, "Press Alt + N");
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPDF.Enabled = false;
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPDF.ForeColor = System.Drawing.Color.White;
            this.btnPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPDF.Location = new System.Drawing.Point(561, 3);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(87, 30);
            this.btnPDF.TabIndex = 86;
            this.btnPDF.TabStop = false;
            this.btnPDF.Text = "PDF";
            this.toolTip.SetToolTip(this.btnPDF, "Press Alt + F");
            this.btnPDF.UseVisualStyleBackColor = false;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(554, 525);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 16);
            this.label9.TabIndex = 65;
            this.label9.Text = "Online Payment :";
            // 
            // lblOnline
            // 
            this.lblOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnline.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.lblOnline.Format = null;
            this.lblOnline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOnline.Location = new System.Drawing.Point(699, 525);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(131, 16);
            this.lblOnline.TabIndex = 66;
            this.lblOnline.Text = "0";
            this.lblOnline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOnline.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblRunBal
            // 
            this.lblRunBal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRunBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunBal.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblRunBal.ForeColor = System.Drawing.Color.Red;
            this.lblRunBal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRunBal.Location = new System.Drawing.Point(720, 169);
            this.lblRunBal.Name = "lblRunBal";
            this.lblRunBal.Size = new System.Drawing.Size(207, 23);
            this.lblRunBal.TabIndex = 90;
            this.lblRunBal.Text = "0.00";
            this.lblRunBal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint2
            // 
            this.btnPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint2.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint2.Enabled = false;
            this.btnPrint2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint2.ForeColor = System.Drawing.Color.White;
            this.btnPrint2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint2.Location = new System.Drawing.Point(468, 3);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.Size = new System.Drawing.Size(87, 30);
            this.btnPrint2.TabIndex = 81;
            this.btnPrint2.TabStop = false;
            this.btnPrint2.Tag = "2";
            this.btnPrint2.Text = "Print-2";
            this.toolTip.SetToolTip(this.btnPrint2, "Press Alt + T");
            this.btnPrint2.UseVisualStyleBackColor = false;
            this.btnPrint2.Click += new System.EventHandler(this.btnPrint2_Click);
            // 
            // cbPrint2
            // 
            this.cbPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPrint2.AutoSize = true;
            this.cbPrint2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.cbPrint2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbPrint2.Location = new System.Drawing.Point(70, 581);
            this.cbPrint2.Name = "cbPrint2";
            this.cbPrint2.Size = new System.Drawing.Size(82, 20);
            this.cbPrint2.TabIndex = 87;
            this.cbPrint2.TabStop = false;
            this.cbPrint2.Text = "Print - 2";
            this.cbPrint2.UseVisualStyleBackColor = true;
            this.cbPrint2.CheckedChanged += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // btnReceiptAndPayment
            // 
            this.btnReceiptAndPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceiptAndPayment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReceiptAndPayment.Location = new System.Drawing.Point(964, 170);
            this.btnReceiptAndPayment.Name = "btnReceiptAndPayment";
            this.btnReceiptAndPayment.Size = new System.Drawing.Size(26, 23);
            this.btnReceiptAndPayment.TabIndex = 47;
            this.btnReceiptAndPayment.TabStop = false;
            this.btnReceiptAndPayment.Text = "...";
            this.toolTip.SetToolTip(this.btnReceiptAndPayment, "Press Alt + B");
            this.btnReceiptAndPayment.UseVisualStyleBackColor = true;
            this.btnReceiptAndPayment.Click += new System.EventHandler(this.btnReceiptAndPayment_Click);
            // 
            // btnCustomerSearch
            // 
            this.btnCustomerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomerSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCustomerSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCustomerSearch.ForeColor = System.Drawing.Color.White;
            this.btnCustomerSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomerSearch.Image")));
            this.btnCustomerSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCustomerSearch.Location = new System.Drawing.Point(576, 47);
            this.btnCustomerSearch.Name = "btnCustomerSearch";
            this.btnCustomerSearch.Size = new System.Drawing.Size(26, 26);
            this.btnCustomerSearch.TabIndex = 93;
            this.btnCustomerSearch.TabStop = false;
            this.toolTip.SetToolTip(this.btnCustomerSearch, "Press Alt + A");
            this.btnCustomerSearch.UseVisualStyleBackColor = false;
            this.btnCustomerSearch.Click += new System.EventHandler(this.btnCustomerSearch_Click);
            // 
            // flpButtons
            // 
            this.flpButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flpButtons.Controls.Add(this.btnSearch);
            this.flpButtons.Controls.Add(this.btnPDF);
            this.flpButtons.Controls.Add(this.btnPrint2);
            this.flpButtons.Controls.Add(this.btnPrint);
            this.flpButtons.Controls.Add(this.btnClear);
            this.flpButtons.Controls.Add(this.btnDelete);
            this.flpButtons.Controls.Add(this.btnEdit);
            this.flpButtons.Controls.Add(this.btnSave);
            this.flpButtons.Location = new System.Drawing.Point(335, 573);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flpButtons.Size = new System.Drawing.Size(744, 37);
            this.flpButtons.TabIndex = 91;
            // 
            // btnDiscount
            // 
            this.btnDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDiscount.Location = new System.Drawing.Point(277, 503);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(21, 23);
            this.btnDiscount.TabIndex = 92;
            this.btnDiscount.Text = "..";
            this.btnDiscount.UseVisualStyleBackColor = true;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // col_SL
            // 
            this.col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SL.DataPropertyName = "SL";
            this.col_SL.HeaderText = "SL";
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            this.col_SL.Width = 40;
            // 
            // col_ProCode
            // 
            this.col_ProCode.DataPropertyName = "ProductCode";
            this.col_ProCode.HeaderText = "Pro Code";
            this.col_ProCode.Name = "col_ProCode";
            this.col_ProCode.Visible = false;
            // 
            // col_ProName
            // 
            this.col_ProName.DataPropertyName = "ProductName";
            this.col_ProName.HeaderText = "Product Name";
            this.col_ProName.Name = "col_ProName";
            // 
            // col_Barcode
            // 
            this.col_Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Barcode.DataPropertyName = "Barcode";
            this.col_Barcode.HeaderText = "Barcode";
            this.col_Barcode.Name = "col_Barcode";
            this.col_Barcode.Width = 80;
            // 
            // col_HSN
            // 
            this.col_HSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_HSN.DataPropertyName = "Hsn";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_HSN.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_HSN.HeaderText = "HSN";
            this.col_HSN.Name = "col_HSN";
            this.col_HSN.Visible = false;
            this.col_HSN.Width = 80;
            // 
            // col_Qty
            // 
            this.col_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Qty.DataPropertyName = "Quantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_Qty.HeaderText = "Qty";
            this.col_Qty.Name = "col_Qty";
            this.col_Qty.Width = 80;
            // 
            // col_Unit
            // 
            this.col_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Unit.DataPropertyName = "Unit";
            this.col_Unit.HeaderText = "Unit";
            this.col_Unit.Name = "col_Unit";
            this.col_Unit.Width = 50;
            // 
            // col_Rate
            // 
            this.col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Rate.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_Rate.HeaderText = "Rate";
            this.col_Rate.Name = "col_Rate";
            this.col_Rate.Width = 90;
            // 
            // col_Gross
            // 
            this.col_Gross.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Gross.DataPropertyName = "GrossAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Gross.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_Gross.HeaderText = "Gross";
            this.col_Gross.Name = "col_Gross";
            this.col_Gross.ReadOnly = true;
            this.col_Gross.Width = 90;
            // 
            // col_Dis
            // 
            this.col_Dis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Dis.DataPropertyName = "Discount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Dis.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_Dis.HeaderText = "Dis%";
            this.col_Dis.Name = "col_Dis";
            this.col_Dis.Width = 50;
            // 
            // col_Discount
            // 
            this.col_Discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Discount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Discount.DefaultCellStyle = dataGridViewCellStyle8;
            this.col_Discount.HeaderText = "Discount";
            this.col_Discount.Name = "col_Discount";
            this.col_Discount.ReadOnly = true;
            this.col_Discount.Width = 90;
            // 
            // col_Taxable
            // 
            this.col_Taxable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Taxable.DataPropertyName = "Taxable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Taxable.DefaultCellStyle = dataGridViewCellStyle9;
            this.col_Taxable.HeaderText = "Taxable";
            this.col_Taxable.Name = "col_Taxable";
            this.col_Taxable.ReadOnly = true;
            this.col_Taxable.Width = 90;
            // 
            // col_Vat
            // 
            this.col_Vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Vat.DataPropertyName = "Vat";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Vat.DefaultCellStyle = dataGridViewCellStyle10;
            this.col_Vat.HeaderText = "Tax";
            this.col_Vat.Name = "col_Vat";
            this.col_Vat.Visible = false;
            this.col_Vat.Width = 50;
            // 
            // col_VatAmnt
            // 
            this.col_VatAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_VatAmnt.DataPropertyName = "VatAmount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle11;
            this.col_VatAmnt.HeaderText = "Tax Amount";
            this.col_VatAmnt.Name = "col_VatAmnt";
            this.col_VatAmnt.ReadOnly = true;
            this.col_VatAmnt.Visible = false;
            this.col_VatAmnt.Width = 90;
            // 
            // col_GST
            // 
            this.col_GST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_GST.DataPropertyName = "Gst";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_GST.DefaultCellStyle = dataGridViewCellStyle12;
            this.col_GST.HeaderText = "GST(%)";
            this.col_GST.Name = "col_GST";
            this.col_GST.Visible = false;
            this.col_GST.Width = 50;
            // 
            // col_GSTAmnt
            // 
            this.col_GSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_GSTAmnt.DataPropertyName = "GstAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle13;
            this.col_GSTAmnt.HeaderText = "GST Amount";
            this.col_GSTAmnt.Name = "col_GSTAmnt";
            this.col_GSTAmnt.ReadOnly = true;
            this.col_GSTAmnt.Visible = false;
            this.col_GSTAmnt.Width = 90;
            // 
            // col_SGST
            // 
            this.col_SGST.DataPropertyName = "Sgst";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_SGST.DefaultCellStyle = dataGridViewCellStyle14;
            this.col_SGST.HeaderText = "SGST(%)";
            this.col_SGST.Name = "col_SGST";
            this.col_SGST.ReadOnly = true;
            this.col_SGST.Visible = false;
            // 
            // col_SGSTAmnt
            // 
            this.col_SGSTAmnt.DataPropertyName = "SGstAmount";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle15;
            this.col_SGSTAmnt.HeaderText = "SGST(Rs)";
            this.col_SGSTAmnt.Name = "col_SGSTAmnt";
            this.col_SGSTAmnt.ReadOnly = true;
            this.col_SGSTAmnt.Visible = false;
            // 
            // col_CGST
            // 
            this.col_CGST.DataPropertyName = "CGst";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_CGST.DefaultCellStyle = dataGridViewCellStyle16;
            this.col_CGST.HeaderText = "CGST(%)";
            this.col_CGST.Name = "col_CGST";
            this.col_CGST.ReadOnly = true;
            this.col_CGST.Visible = false;
            // 
            // col_CGSTAmnt
            // 
            this.col_CGSTAmnt.DataPropertyName = "CGstAmount";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle17;
            this.col_CGSTAmnt.HeaderText = "CGST(Rs)";
            this.col_CGSTAmnt.Name = "col_CGSTAmnt";
            this.col_CGSTAmnt.ReadOnly = true;
            this.col_CGSTAmnt.Visible = false;
            // 
            // col_IGST
            // 
            this.col_IGST.DataPropertyName = "IGst";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_IGST.DefaultCellStyle = dataGridViewCellStyle18;
            this.col_IGST.HeaderText = "IGST(%)";
            this.col_IGST.Name = "col_IGST";
            this.col_IGST.ReadOnly = true;
            this.col_IGST.Visible = false;
            // 
            // col_IGSTAmnt
            // 
            this.col_IGSTAmnt.DataPropertyName = "IGstAmount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle19;
            this.col_IGSTAmnt.HeaderText = "IGST(Rs)";
            this.col_IGSTAmnt.Name = "col_IGSTAmnt";
            this.col_IGSTAmnt.ReadOnly = true;
            this.col_IGSTAmnt.Visible = false;
            // 
            // col_NetAmnt
            // 
            this.col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_NetAmnt.DataPropertyName = "NETAmount";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle20;
            this.col_NetAmnt.HeaderText = "Net Amount";
            this.col_NetAmnt.Name = "col_NetAmnt";
            this.col_NetAmnt.ReadOnly = true;
            this.col_NetAmnt.Width = 90;
            // 
            // SalesInvoiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1078, 608);
            this.Controls.Add(this.btnCustomerSearch);
            this.Controls.Add(this.btnDiscount);
            this.Controls.Add(this.flpButtons);
            this.Controls.Add(this.lblRunBal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSeperator2);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblSReturnAmnt);
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.lblCardPaid);
            this.Controls.Add(this.lblCashPaid);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtPrintCopy);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label93);
            this.Controls.Add(this.label90);
            this.Controls.Add(this.lblCessAmount);
            this.Controls.Add(this.lblTotalIGST);
            this.Controls.Add(this.lblTotalCGST);
            this.Controls.Add(this.lblTotalSGST);
            this.Controls.Add(this.lblTaxTotal);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txtAddDiscAmnt);
            this.Controls.Add(this.txtAddDisc);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtFreight);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtAddAmnt);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.cbPrint2);
            this.Controls.Add(this.cbPrint);
            this.Controls.Add(this.lblTaxable);
            this.Controls.Add(this.lblNetTotal);
            this.Controls.Add(this.lblDisTotal);
            this.Controls.Add(this.lblGrsTotal);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.lblUntQty);
            this.Controls.Add(this.lblLastPurRate);
            this.Controls.Add(this.lblCStock);
            this.Controls.Add(this.btnSRtn);
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.cmbSalesType);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btnReceiptAndPayment);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.lblPreBill);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cmbProCategory);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbSlsExi);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbPartyAccnt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbVehicle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnAccCreate);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.cmbRoute);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDepot);
            this.Controls.Add(this.cmbSlsAcct);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudInvoiceNo);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkEnableInvNo);
            this.Controls.Add(this.btnDate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.Name = "SalesInvoiceView";
            this.Load += new System.EventHandler(this.SalesInvoiceView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalesInvoiceView_KeyDown);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.chkEnableInvNo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.nudInvoiceNo, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label39, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbSlsAcct, 0);
            this.Controls.SetChildIndex(this.cmbDepot, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbRoute, 0);
            this.Controls.SetChildIndex(this.btnOrder, 0);
            this.Controls.SetChildIndex(this.btnAccCreate, 0);
            this.Controls.SetChildIndex(this.btnFirst, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnLast, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbVehicle, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbPartyAccnt, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cmbSlsExi, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.cmbProCategory, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.lblPreBill, 0);
            this.Controls.SetChildIndex(this.btnBalance, 0);
            this.Controls.SetChildIndex(this.btnReceiptAndPayment, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.cmbSalesType, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.Controls.SetChildIndex(this.btnSRtn, 0);
            this.Controls.SetChildIndex(this.lblCStock, 0);
            this.Controls.SetChildIndex(this.lblLastPurRate, 0);
            this.Controls.SetChildIndex(this.lblUntQty, 0);
            this.Controls.SetChildIndex(this.lblTotalQty, 0);
            this.Controls.SetChildIndex(this.lblGrsTotal, 0);
            this.Controls.SetChildIndex(this.lblDisTotal, 0);
            this.Controls.SetChildIndex(this.lblNetTotal, 0);
            this.Controls.SetChildIndex(this.lblTaxable, 0);
            this.Controls.SetChildIndex(this.cbPrint, 0);
            this.Controls.SetChildIndex(this.cbPrint2, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.txtAddAmnt, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.txtFreight, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.txtAddDisc, 0);
            this.Controls.SetChildIndex(this.txtAddDiscAmnt, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.lblGrandTotal, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.lblTaxTotal, 0);
            this.Controls.SetChildIndex(this.lblTotalSGST, 0);
            this.Controls.SetChildIndex(this.lblTotalCGST, 0);
            this.Controls.SetChildIndex(this.lblTotalIGST, 0);
            this.Controls.SetChildIndex(this.lblCessAmount, 0);
            this.Controls.SetChildIndex(this.label90, 0);
            this.Controls.SetChildIndex(this.label93, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.label37, 0);
            this.Controls.SetChildIndex(this.btnPayment, 0);
            this.Controls.SetChildIndex(this.txtPrintCopy, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.lblCashPaid, 0);
            this.Controls.SetChildIndex(this.lblCardPaid, 0);
            this.Controls.SetChildIndex(this.lblOnline, 0);
            this.Controls.SetChildIndex(this.lblSReturnAmnt, 0);
            this.Controls.SetChildIndex(this.lblBalance, 0);
            this.Controls.SetChildIndex(this.btnSeperator2, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblRunBal, 0);
            this.Controls.SetChildIndex(this.flpButtons, 0);
            this.Controls.SetChildIndex(this.btnDiscount, 0);
            this.Controls.SetChildIndex(this.btnCustomerSearch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.flpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
