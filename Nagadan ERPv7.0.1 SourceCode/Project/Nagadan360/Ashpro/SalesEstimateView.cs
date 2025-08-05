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
using AshUsercontrol;

namespace Ashpro
{
	public class SalesEstimateView : BaseForm
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

		private List<POSDetails> entPOSDetails;

		private DataGridViewTextBoxEditingControl textbox;

		private SalesReturnView salesReturn;

		private AccountEntity account;

		private bool isFromOrder = false;

		private int iSOrderId;

		private bool isReturn;

		private DBContext db;

		private int iNormalCashAccountId;

		private string sVType = "Normal";

		private string sVoucherType = "Sales Voucher";

		private string sBranchDetails = string.Empty;

		private decimal dTendered;

		private decimal dChanges;

		private SalesOrderMaster salesOrderMaster;

		private List<ComboFill> comboFills = new List<ComboFill>();

		private IContainer components = null;

		private Button btnClear;

		private Button button1;

		private AshTextBoxNormal txtPrintCopy;

		private AshNumericLabel lblBalance;

		private AshNumericLabel lblSReturnAmnt;

		private AshNumericLabel lblCashPaid;

		private Label label20;

		private Label label16;

		private Label label18;

		private Button btnSearch;

		private Button btnPayment;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private AshNumericLabel lblGrandTotal;

		private Label label36;

		private AshTextBoxNormal txtAddDiscAmnt;

		private AshTextBoxNormal txtAddDisc;

		private Label label15;

		private BindingSource bdsSales;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private AshTextBoxNormal txtAddAmnt;

		private AshNumericLabel lblNetTotal;

		private Label label34;

		private Label label33;

		private CheckBox cbMail;

		private CheckBox cbPrint;

		private AshNumericLabel lblDisTotal;

		private AshNumericLabel lblGrsTotal;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblUntQty;

		private AshNumericLabel lblLastSalesRate;

		private AshNumericLabel lblCStock;

		private Button btnSRtn;

		public AshDataGridview dgDetails;

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

		private AshComboBox cmbDepot;

		private AshComboBox cmbSlsAcct;

		private Label label7;

		private Label label3;

		private AshUpDown nudInvoiceNo;

		private Label label25;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private Label label2;

		private CheckBox chkEnableInvNo;

		private Button btnDate;

		private Label label8;

		private AshNumericLabel lblCardPaid;

		private Button btnLast;

		private Button btnNext;

		private Button btnBack;

		private Button btnFirst;

		private Button btnPDF;

		private Label label9;

		private AshNumericLabel lblOnline;

		private CheckBox cbMappingBarcodeBased;

		private Label lblRunBal;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_Gross;

		private DataGridViewTextBoxColumn col_Dis;

		private DataGridViewTextBoxColumn col_Discount;

		private DataGridViewTextBoxColumn col_NetAmnt;

		private ToolTip toolTip;

		private Button btnCustomerSearch;

		public int iInvNo { get; set; } = 0;


		public UserPrevilegeDtl plgDtls { get; set; }

		public SalesEstimateView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			lblCashPaid.Format = CommonSettings.strCurencyFormat;
			lblCardPaid.Format = CommonSettings.strCurencyFormat;
			lblSReturnAmnt.Format = CommonSettings.strCurencyFormat;
			txtAddAmnt.Format = CommonSettings.strCurencyFormat;
			txtFreight.Format = CommonSettings.strCurencyFormat;
			txtAddDisc.Format = CommonSettings.strCurencyFormat;
			txtAddDiscAmnt.Format = CommonSettings.strCurencyFormat;
			col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Dis.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void PrintMethod(bool isPDF = false)
		{
			try
			{
				if (entsalesdtls.Count == 0)
				{
					Messages.ErrorMessage("No records to Print!");
					return;
				}
				List<InvoiceAdjustment> list = new List<InvoiceAdjustment>();
				if (CommonSettings.blnInvoicePrintingByRDLC)
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
						AniHelper.AddPaymentPrint(list, "Cash Payment", pOSMaster.CashPayment.ToString(), "دفع نقدا");
					}
					cashPayment = pOSMaster.CardPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(list, "Card Payment", pOSMaster.CardPayment.ToString(), "بطاقه ائتمان");
					}
					cashPayment = pOSMaster.OtherPayment;
					if ((cashPayment.GetValueOrDefault() > default(decimal)) & cashPayment.HasValue)
					{
						AniHelper.AddPaymentPrint(list, "Online Payment", pOSMaster.OtherPayment.ToString(), "مدفوعات أخرى");
					}
					dataTable4 = AniHelper.ListToDataTable(list);
					dataTable4.TableName = "dtPayment";
					dataSet.Tables.Add(dataTable4);
					decimal num = entsalesdtls.Sum((SalesDetails x) => x.ProQty);
					string sReportPath = Application.StartupPath + "\\ReportForms\\rptPOSPrintWithInvoiceNo.rdlc";
					List<atReportParameter> list2 = new List<atReportParameter>();
					list2.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
					CultureInfo cultureInfo = new CultureInfo("en-US");
					string parameterValue = pOSMaster.SalesDate.ToString(cultureInfo);
					list2.Add(new atReportParameter("QuantityParameter", CommonSettings.strQuantityFormat));
					list2.Add(new atReportParameter("InvoiceDate", parameterValue));
					list2.Add(new atReportParameter("Tendered", dTendered.ToString(CommonSettings.strCurencyFormat)));
					list2.Add(new atReportParameter("Changes", dChanges.ToString(CommonSettings.strCurencyFormat)));
					list2.Add(new atReportParameter("TotalQuantity", num.ToString(CommonSettings.strQuantityFormat)));
					printerSettings.PrinterName = printerSettings.PrinterName;
					DirectReportPrint directReportPrint = new DirectReportPrint();
					directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list2, isPDF);
					return;
				}
				entDMaster = db.DotmatrixMasters(new
				{
					VochrType = "Sales Estimate",
					FormType = "Sales Estimate",
					isDefault = true
				}).FirstOrDefault();
				if (entDMaster != null)
				{
					entDSettings = new List<DotmatrixSettings>();
					entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
					Company company2 = new Company();
					company2 = db.Companies().FirstOrDefault();
					entcompany = company2.CopyTo<ExCompany>();
					entcompany.CompanyName = company2.InstituteName;
					entcompany.TaxId = company2.GSTNo;
					entcompany.Logo = company2.Logo;
					entPartyDtl = new PartyDetails();
					account = db.Accounts(new
					{
						AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
					}).FirstOrDefault();
					entPartyDtl = POSHelper.GetParty(account);
					int num2 = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
					if (account.AGroupId == num2)
					{
						entPartyDtl.Name = "Cash Customer";
					}
					iSalesInvId = AshproCommon.IdFindMethod("Select SalesInvId From SalesEstimateMaster Where InvType='" + sVType + "'and SalesInvNo='" + nudInvoiceNo.Value + "' and FYearID='" + iFYearId + "'" + sBranchDetails);
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
					entTMaster.OtherPayment = 0m;
					entTMaster.Tendered = dTendered;
					entTMaster.Changes = dChanges;
					entTMaster.PriceCategory = cmbProCategory.Text;
					PrintHelper printHelper = new PrintHelper();
					printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, list, entTMaster, isPrint: true, txtPrintCopy.Data, isPDF);
					printHelper = null;
				}
				else
				{
					MessageBox.Show("There is no default print design. Please make your own design", Utility.uManufacturer);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void LoadComboMethod()
		{
			AniHelper.FillAccount(cmbPartyAccnt);
			AniHelper.FillSalesAccount(cmbSlsAcct);
			AniHelper.FillEmployees(cmbSlsExi);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillPCategory(cmbProCategory);
			AniHelper.FillGodown(cmbDepot);
			AniHelper.FillVehicle(cmbVehicle);
		}

		private void LoadSettingsMethod()
		{
			DataGridViewTextBoxColumn dataGridViewTextBoxColumn = col_Dis;
			bool visible = (col_Discount.Visible = CommonSettings.blnShowDiscountInInvoice);
			dataGridViewTextBoxColumn.Visible = visible;
			entBooleanSettings = new List<SettingsEntity>();
			entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == "Sales Estimate" && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
			chkEnableInvNo.Checked = CommonSettings.blnEditInvoiceNumber;
			cbPrint.Checked = CommonSettings.blnPntInvWlSaving;
			cbMail.Checked = CommonSettings.blnSndMlWlSaving;
			if (CommonSettings.blnEnableMultiBranch)
			{
				sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
		}

		private void NameAndBalanceFindMethod()
		{
			try
			{
				cmbProCategory.SelectedValue = db.Accounts(new
				{
					AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
				}).FirstOrDefault().PCategoryId ?? 1;
				lblRunBal.Text = AniHelper.AccountBalanceFindMethod(cmbPartyAccnt.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)));
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void TotalFindMethod()
		{
			lblTotalQty.Text = entTransactions.Sum((Transaction x) => x.Quantity).ToString(CommonSettings.strQuantityFormat);
			lblGrsTotal.Text = entTransactions.Sum((Transaction x) => x.GrossAmount).ToString(CommonSettings.strCurencyFormat);
			lblDisTotal.Text = entTransactions.Sum((Transaction x) => x.DiscountAmount).ToString(CommonSettings.strCurencyFormat);
			lblNetTotal.Text = entTransactions.Sum((Transaction x) => x.NETAmount).ToString(CommonSettings.strCurencyFormat);
			lblGrandTotal.Text = (lblNetTotal.Text.ToDecimal() + txtAddAmnt.Text.ToDecimal() + txtFreight.Text.ToDecimal() - txtAddDiscAmnt.Text.ToDecimal()).ToString(CommonSettings.strCurencyFormat);
		}

		private void SalesOrderLoadMethod(int iIvoiceId)
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
			BindDgv();
			TotalFindMethod();
		}

		private void LoadSalesInvoiceFromID()
		{
			iMasterId = entsaleshdr.SalesInvId;
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
				cmbSlsAcct.SelectedValue = entsaleshdr.SalesAccountId.GetValueOrDefault();
				cmbPartyAccnt.SelectedValue = entsaleshdr.PartyAccountId.GetValueOrDefault();
				cmbRoute.SelectedValue = entsaleshdr.RouteId.GetValueOrDefault();
				cmbVehicle.SelectedValue = entsaleshdr.VehId.GetValueOrDefault();
				cmbProCategory.SelectedValue = entsaleshdr.PriceCategoryId.GetValueOrDefault();
				txtAddAmnt.Value = entsaleshdr.AditionalAmount;
				txtFreight.Value = entsaleshdr.Frieght;
				lblCashPaid.Value = entsaleshdr.CashPayment;
				lblCardPaid.Value = entsaleshdr.CardPayment;
				lblSReturnAmnt.Value = entsaleshdr.ReturnAmount;
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
				iCashAcctId = entsaleshdr.CashAccountId;
				iCardAccount = entsaleshdr.CardAccountId;
				iOtherAccount = entsaleshdr.OnlineAccountId;
				BindDgv();
				TotalFindMethod();
				foreach (Control control in base.Controls)
				{
					control.Enabled = false;
				}
				btnFirst.Enabled = true;
				btnLast.Enabled = true;
				btnNext.Enabled = true;
				btnBack.Enabled = true;
				btnEdit.Enabled = true;
				btnEnd.Enabled = true;
				btnPrint.Enabled = true;
				btnMail.Enabled = true;
				btnClear.Enabled = true;
				nudInvoiceNo.Enabled = true;
				chkEnableInvNo.Enabled = true;
				txtPrintCopy.Enabled = true;
				btnDelete.Enabled = true;
			}
			else
			{
				btnClearMethod();
			}
		}

		private void SalesHeaderLoadMethod(SalesMaster sMaster, int InvNo)
		{
			sMaster.SalesInvId = iMasterId;
			sMaster.SalesInvNo = InvNo;
			sMaster.InvType = sVType;
			sMaster.SalesDate = dtmDate.Value.Date;
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
			sMaster.TotalDiscount = lblDisTotal.Value;
			sMaster.FK_BranchId = Settings.Default.iBranchId;
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
			sMaster.IsSettiled = ((!(sMaster.Credit > 0m)) ? true : false);
			sMaster.ReturnAmount = lblSReturnAmnt.Value;
			sMaster.FreeAmount = FreeAmountTotalFindMethod();
			sMaster.IsClosed = false;
			sMaster.CardAccountId = iCashAcctId;
			sMaster.CardAccountId = iCardAccount;
			sMaster.OnlineAccountId = iOtherAccount;
			sMaster.SequenceNo = Utility.uSerialNumber;
			sMaster.Terminal = Utility.uTerminal;
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

		private void DeleteSubMethod()
		{
			string[] obj = new string[8] { "Delete From GdwnStckTranLog Where VchrId='", null, null, null, null, null, null, null };
			int? num = iMasterId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType ='";
			obj[3] = sVoucherType;
			obj[4] = "' and Narration='";
			obj[5] = sVoucherType;
			obj[6] = "'";
			obj[7] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[8] { "Delete From StockTranLog Where VchrId='", null, null, null, null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType ='";
			obj2[3] = sVoucherType;
			obj2[4] = "' and Narration='";
			obj2[5] = sVoucherType;
			obj2[6] = "'";
			obj2[7] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj2));
			DataTable dataTable = new DataTable();
			string[] obj3 = new string[8] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null, null, null };
			num = iMasterId;
			obj3[1] = num.ToString();
			obj3[2] = "' and VchrType='";
			obj3[3] = sVoucherType;
			obj3[4] = "' and VchrName ='";
			obj3[5] = sVType;
			obj3[6] = "'";
			obj3[7] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj3));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
			AshproCommon.DeleteFromDatabase("SalesEstimateSettings", "SalesInvId", iMasterId.ToInt32());
		}

		private void ClearFieldMethod()
		{
			entTransactions = new List<Transaction>();
			BindDgv();
			cmbPartyAccnt.SelectedIndex = 0;
			cmbProCategory.SelectedIndex = 0;
		}

		private void SaveSubMethod()
		{
			if (AshproCommon.InsertToDatabase(entsalesdtls.Cast<object>().ToList(), "SalesEstimateSettings"))
			{
				POSHelper.TranLogMethod(0m, entsaleshdr.GrandDiscount, cmbSlsAcct.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, sVoucherType, Settings.Default.intLogOnId, "Main", sVType, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(entsaleshdr.GrandDiscount, 0m, cmbPartyAccnt.SelectedValue.ToInt32(), cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entsaleshdr.SalesInvNo, sVoucherType, Settings.Default.intLogOnId, "Sub", sVType, AniHelper.GetDate(dtmDate.Value));
				entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
				POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), entsaleshdr.SalesInvNo.toInt32(), sVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), sVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
				if (lblCashPaid.Value > 0m)
				{
					int num = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num, dtmDate.Value.Date, lblCashPaid.Value, "Receipt Voucher", "Cash Received On Sale For Pos", cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId.ToInt32(), cmbSlsExi.SelectedValue.ToInt32(), Settings.Default.intLogOnId, "Cash Receipt");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(lblCashPaid.Value, 0m, iCashAcctId.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVoucherID, num, "Receipt Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, lblCashPaid.Value, cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId.ToInt32(), iVoucherID, num, "Receipt Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
				if (lblCardPaid.Value > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num2, dtmDate.Value.Date, lblCardPaid.Value, "Receipt Voucher", "Cash Received On Sale For Pos", cmbPartyAccnt.SelectedValue.ToInt32(), iCardAccount.ToInt32(), cmbSlsExi.SelectedValue.ToInt32(), Settings.Default.intLogOnId, "Cash Receipt");
					int iVoucherID2 = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num2 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(lblCardPaid.Value, 0m, iCardAccount.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVoucherID2, num2, "Receipt Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, lblCardPaid.Value, cmbPartyAccnt.SelectedValue.ToInt32(), iCardAccount.ToInt32(), iVoucherID2, num2, "Receipt Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
				if (isFromOrder)
				{
					AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
				}
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
			catch (Exception ex2)
			{
				throw ex2;
			}
		}

		private void BindDgv()
		{
			dgDetails.AutoGenerateColumns = false;
			bdsSales.DataSource = null;
			bdsSales.DataSource = entTransactions;
			dgDetails.DataSource = bdsSales;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			isFromOrder = false;
			ClearFieldMethod();
			iMasterId = null;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			cmbPartyAccnt.Focus();
		}

		private void NudValueFindMethod()
		{
			nudInvoiceNo.Value = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = sVType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
		}

		private async void SalesEstimateView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
			iNormalCashAccountId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'CASH ACCOUNT'");
			entsalesdtls = new List<SalesDetails>();
			entTransactions = new List<Transaction>();
			BindDgv();
			estimateDate = Settings.Default.appDate;
			LoadComboMethod();
			dtmDate.Value = estimateDate;
			LoadSettingsMethod();
			iFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
			if (iInvNo != 0)
			{
				nudInvoiceNo.Value = iInvNo;
			}
			else
			{
				NudValueFindMethod();
			}
			cbMappingBarcodeBased.Checked = CommonSettings.blnDefaultCellIsBarcode.toBool();
			if (CommonSettings.blnDefaultCellIsBarcode.toBool())
			{
				col_ProName.ReadOnly = true;
			}
			else
			{
				col_Barcode.ReadOnly = true;
			}
		}

		private void chkEnableInvNo_CheckedChanged(object sender, EventArgs e)
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
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSE").FirstOrDefault();
			settingsEntity.BitValue = chkEnableInvNo.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private void btnAccCreate_Click(object sender, EventArgs e)
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

		private void cmbPartyAccnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			NameAndBalanceFindMethod();
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
			catch (Exception)
			{
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
			pOSCashPayment.iCashAccountId = iCashAcctId;
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
			decimal num4 = default(decimal);
			decimal num5 = Math.Abs(lblGrandTotal.Value);
			num4 = num5 - dTendered;
			if (num4 < 0m)
			{
				lblCashPaid.Value = num5 - (lblCardPaid.Value + lblOnline.Value);
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
		}

		private void btnSRtn_Click(object sender, EventArgs e)
		{
			salesReturn = new SalesReturnView();
			salesReturn.SalesInvId = iMasterId;
			salesReturn.FormClosed += SalesReturn_FormClosed;
			salesReturn.MdiParent = base.MdiParent;
			salesReturn.Show();
		}

		private void SalesReturn_FormClosed(object sender, FormClosedEventArgs e)
		{
			lblSReturnAmnt.Value = salesReturn.dmlReturnAmount;
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			ClearFieldMethod();
			entsaleshdr = db.SalesMasters(new
			{
				InvType = sVType,
				SalesInvNo = nudInvoiceNo.Value.ToInt32(),
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

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = sVType;
				formSearch.sInvType = "Sales Invoice";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					iMasterId = formSearch.iInvoiceId;
					LoadSalesInvoiceFromID();
				}
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

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
			{
				InvoiceType = sVType,
				FinYearId = iFYearId,
				BranchId = Settings.Default.iBranchId
			});
			btnClearMethod();
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
				DialogResult dialogResult = MessageBox.Show("Do you really want to delete?", Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					DeleteSubMethod();
					AshproCommon.DeleteFromDatabase("SalesEstimateMaster", "SalesInvId", iMasterId.ToInt32());
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			dgDetails.Focus();
			dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[2];
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			lblPreBill.Text = lblGrandTotal.Value.ToString();
			if (entTransactions.Count == 0)
			{
				MessageBox.Show("No records to save!", Utility.uManufacturer);
				return;
			}
			entsalesdtls = new List<SalesDetails>();
			entsalesdtls = POSHelper.GetSDetailsFrmTransaction(entTransactions, cmbPartyAccnt.SelectedValue.ToInt32(), dtmDate.Value.Date, isEstimate: true);
			if (entsalesdtls == null)
			{
				return;
			}
			try
			{
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					entsaleshdr = new SalesMaster();
					int num = 0;
					SalesHeaderLoadMethod(InvNo: (!CommonSettings.blnEnableMultiBranch) ? AshproCommon.GetIntDataWithParameter_SP("spGetLastSalesInvoiceNo", new
					{
						InvoiceType = sVType,
						FinYearId = iFYearId,
						BranchId = Settings.Default.iBranchId
					}) : ((db.SalesMasters(new
					{
						InvType = sVType,
						FK_BranchId = Settings.Default.iBranchId
					}).Max((SalesMaster x) => x.SalesInvNo) + 1) ?? 1), sMaster: entsaleshdr);
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
					POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), num, sVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), sVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
					if (isFromOrder)
					{
						AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
					}
					entsaleshdr.SalesInvNo = num;
					entsaleshdr.CntrInvNo += num;
				}
				else
				{
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
					int iVchrNo = entsaleshdr.SalesInvNo.toInt32();
					entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
					POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), iVchrNo, sVoucherType, entsaleshdr.InvType, cmbPartyAccnt.SelectedValue.ToInt32(), sVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
					if (isFromOrder)
					{
						AshproCommon.DatabaseMethod("Update SalesOrderMaster set Status='" + true + "' Where SalesOrderMasterId='" + iSOrderId + "'");
					}
					entsaleshdr.CntrInvNo += iVchrNo;
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dgSales_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Transaction current = getCurrent();
			int rowIndex = dgDetails.CurrentCell.RowIndex;
			if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
				productBarcodeEntity = db.ProductBarcodes(new { current.Barcode }).FirstOrDefault();
				POSHelper.GetTransactionsFromBarcode(current, productBarcodeEntity, cmbProCategory.SelectedValue.ToInt32(), rowIndex, sVType, "Sales Invoice", isEstimate: true);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				POSHelper.GetTransactionsAfterEdit(current, isEstimate: true);
				if (current.Quantity > lblCStock.Value && CommonSettings.blnShowMsgWlNStockSE)
				{
					Messages.InformationMessage("The quantity you entered is more than the current stock value!");
				}
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Rate.Name)
			{
				POSHelper.GetTransactionsAfterEdit(current, isEstimate: true);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Dis.Name)
			{
				POSHelper.GetTransactionsAfterEdit(current, isEstimate: true);
			}
			TotalFindMethod();
		}

		private void dgSales_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyUp += Textbox_KeyUp;
				}
			}
		}

		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			textbox = (DataGridViewTextBoxEditingControl)sender;
			try
			{
				Transaction current = getCurrent();
				int rowIndex = dgDetails.CurrentCell.RowIndex;
				if (dgDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
					{
						e.Handled = true;
						ShowItemList showItemList = new ShowItemList();
						showItemList.ProName = textbox.Text;
						showItemList.productList = comboFills;
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
							StockFindMethod(current);
							lblLastSalesRate.Value = AshproCommon.ValueFindMethod("Select MAX(ProRate) as LastPrice From SalesEstimateSettings Where PartyAccountId ='" + cmbPartyAccnt.SelectedValue.ToInt32() + "' and ProductId ='" + proSearch.ProductId + "'");
							POSHelper.GetTransactions(current, rowIndex, sVType, isEstimate: true);
							dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[col_Qty.Name];
							TotalFindMethod();
						}
					}
				}
				else
				{
					if (!(dgDetails.CurrentCell.OwningColumn.Name == col_Unit.Name) || e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer selectCustomer = new SelectCustomer("Unit");
					int num2 = (selectCustomer.ProductId = AshproCommon.IdFindMethod("Select ProductId From Product Where ProductCode = '" + current.ProductCode + "'"));
					selectCustomer.Location = new Point(dgDetails.Location.X + 200, dgDetails.Location.Y + 100 + textbox.Height + dgDetails.GetCellDisplayRectangle(2, rowIndex, cutOverflow: true).Location.Y);
					if (selectCustomer.ShowDialog() != DialogResult.OK)
					{
						return;
					}
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
							current.VatAmount = current.Taxable * (current.Vat / 100m);
						}
						else
						{
							current.VatAmount = 0m;
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
						current.NETAmount = current.Taxable + current.VatAmount;
						dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[col_Rate.Name];
						TotalFindMethod();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void StockFindMethod(Transaction t)
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
		}

		private void dgSales_KeyDown(object sender, KeyEventArgs e)
		{
			Transaction current = getCurrent();
			if (e.KeyCode == Keys.Delete)
			{
				entTransactions.Remove(current);
				int iCount = 1;
				entTransactions.ForEach(delegate(Transaction x)
				{
					x.SL = iCount;
					iCount++;
				});
				BindDgv();
				dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[1];
				TotalFindMethod();
			}
			else if (e.KeyCode == Keys.F6)
			{
				ProductTracing productTracing = new ProductTracing();
				productTracing.TypeVoucher = "Sales";
				productTracing.PartyAccntId = cmbPartyAccnt.SelectedValue.ToInt32();
				productTracing.ProductId = current.ProductId.ToInt32();
				productTracing.ShowDialog();
			}
			else if (e.KeyCode == Keys.F8)
			{
				AniHelper.ItemCreationMethod(current.ProductId.toInt32());
			}
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSE").FirstOrDefault();
			settingsEntity.BitValue = cbPrint.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void cbMail_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSE").FirstOrDefault();
			settingsEntity.BitValue = cbMail.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void txtAddAmnt_TextChanged(object sender, EventArgs e)
		{
			TotalFindMethod();
		}

		private void txtAddDisc_Validated(object sender, EventArgs e)
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

		private void txtAddDiscAmnt_Validated(object sender, EventArgs e)
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

		private void SalesEstimateView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnMail_Click(null, null);
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
			else
			{
				e.SuppressKeyPress = false;
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
			NudValueFindMethod();
		}

		private void btnPDF_Click(object sender, EventArgs e)
		{
			PrintMethod(isPDF: true);
		}

		private void cbMappingBarcodeBased_CheckedChanged(object sender, EventArgs e)
		{
			if (cbMappingBarcodeBased.Checked)
			{
				col_ProName.ReadOnly = true;
				col_Barcode.ReadOnly = false;
			}
			else
			{
				col_Barcode.ReadOnly = true;
				col_ProName.ReadOnly = false;
			}
		}

		private void cmbPartyAccnt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				dgDetails.Focus();
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SalesEstimateView));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			btnClear = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
			lblBalance = new AshUsercontrol.AshNumericLabel();
			lblSReturnAmnt = new AshUsercontrol.AshNumericLabel();
			lblCashPaid = new AshUsercontrol.AshNumericLabel();
			label20 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			btnSearch = new System.Windows.Forms.Button();
			btnPayment = new System.Windows.Forms.Button();
			btnMail = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			lblGrandTotal = new AshUsercontrol.AshNumericLabel();
			label36 = new System.Windows.Forms.Label();
			txtAddDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
			txtAddDisc = new AshUsercontrol.AshTextBoxNormal();
			label15 = new System.Windows.Forms.Label();
			bdsSales = new System.Windows.Forms.BindingSource(components);
			label35 = new System.Windows.Forms.Label();
			txtFreight = new AshUsercontrol.AshTextBoxNormal();
			txtAddAmnt = new AshUsercontrol.AshTextBoxNormal();
			lblNetTotal = new AshUsercontrol.AshNumericLabel();
			label34 = new System.Windows.Forms.Label();
			label33 = new System.Windows.Forms.Label();
			cbMail = new System.Windows.Forms.CheckBox();
			cbPrint = new System.Windows.Forms.CheckBox();
			lblDisTotal = new AshUsercontrol.AshNumericLabel();
			lblGrsTotal = new AshUsercontrol.AshNumericLabel();
			lblTotalQty = new AshUsercontrol.AshNumericLabel();
			lblUntQty = new AshUsercontrol.AshNumericLabel();
			lblLastSalesRate = new AshUsercontrol.AshNumericLabel();
			lblCStock = new AshUsercontrol.AshNumericLabel();
			btnSRtn = new System.Windows.Forms.Button();
			dgDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Dis = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NetAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label32 = new System.Windows.Forms.Label();
			btnBalance = new System.Windows.Forms.Button();
			lblPreBill = new System.Windows.Forms.Label();
			label19 = new System.Windows.Forms.Label();
			cmbProCategory = new AshUsercontrol.AshComboBox();
			label13 = new System.Windows.Forms.Label();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			cmbSlsExi = new AshUsercontrol.AshComboBox();
			label11 = new System.Windows.Forms.Label();
			cmbPartyAccnt = new AshUsercontrol.AshComboBox();
			label6 = new System.Windows.Forms.Label();
			cmbVehicle = new AshUsercontrol.AshComboBox();
			label5 = new System.Windows.Forms.Label();
			btnAccCreate = new System.Windows.Forms.Button();
			btnOrder = new System.Windows.Forms.Button();
			cmbRoute = new AshUsercontrol.AshComboBox();
			label4 = new System.Windows.Forms.Label();
			cmbDepot = new AshUsercontrol.AshComboBox();
			cmbSlsAcct = new AshUsercontrol.AshComboBox();
			label7 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			nudInvoiceNo = new AshUsercontrol.AshUpDown();
			label25 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			chkEnableInvNo = new System.Windows.Forms.CheckBox();
			btnDate = new System.Windows.Forms.Button();
			label8 = new System.Windows.Forms.Label();
			lblCardPaid = new AshUsercontrol.AshNumericLabel();
			btnLast = new System.Windows.Forms.Button();
			btnNext = new System.Windows.Forms.Button();
			btnBack = new System.Windows.Forms.Button();
			btnFirst = new System.Windows.Forms.Button();
			btnPDF = new System.Windows.Forms.Button();
			label9 = new System.Windows.Forms.Label();
			lblOnline = new AshUsercontrol.AshNumericLabel();
			cbMappingBarcodeBased = new System.Windows.Forms.CheckBox();
			lblRunBal = new System.Windows.Forms.Label();
			toolTip = new System.Windows.Forms.ToolTip(components);
			btnCustomerSearch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)bdsSales).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).BeginInit();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			toolTip.SetToolTip(btnEnd, resources.GetString("btnEnd.ToolTip"));
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Name = "btnClear";
			btnClear.TabStop = false;
			toolTip.SetToolTip(btnClear, resources.GetString("btnClear.ToolTip"));
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			resources.ApplyResources(button1, "button1");
			button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button1.Name = "button1";
			button1.TabStop = false;
			toolTip.SetToolTip(button1, resources.GetString("button1.ToolTip"));
			button1.UseVisualStyleBackColor = true;
			resources.ApplyResources(txtPrintCopy, "txtPrintCopy");
			txtPrintCopy.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtPrintCopy.Data = 1;
			txtPrintCopy.EnterColor = System.Drawing.Color.LightPink;
			txtPrintCopy.Format = null;
			txtPrintCopy.isAllowNegative = false;
			txtPrintCopy.isAllowSpecialChar = false;
			txtPrintCopy.isNumberOnly = true;
			txtPrintCopy.isNumeric = false;
			txtPrintCopy.LeaveColor = System.Drawing.Color.White;
			txtPrintCopy.Name = "txtPrintCopy";
			txtPrintCopy.TabStop = false;
			toolTip.SetToolTip(txtPrintCopy, resources.GetString("txtPrintCopy.ToolTip"));
			txtPrintCopy.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			resources.ApplyResources(lblBalance, "lblBalance");
			lblBalance.Format = null;
			lblBalance.Name = "lblBalance";
			toolTip.SetToolTip(lblBalance, resources.GetString("lblBalance.ToolTip"));
			lblBalance.Value = new decimal(new int[4]);
			resources.ApplyResources(lblSReturnAmnt, "lblSReturnAmnt");
			lblSReturnAmnt.Format = null;
			lblSReturnAmnt.Name = "lblSReturnAmnt";
			toolTip.SetToolTip(lblSReturnAmnt, resources.GetString("lblSReturnAmnt.ToolTip"));
			lblSReturnAmnt.Value = new decimal(new int[4]);
			resources.ApplyResources(lblCashPaid, "lblCashPaid");
			lblCashPaid.Format = null;
			lblCashPaid.Name = "lblCashPaid";
			toolTip.SetToolTip(lblCashPaid, resources.GetString("lblCashPaid.ToolTip"));
			lblCashPaid.Value = new decimal(new int[4]);
			resources.ApplyResources(label20, "label20");
			label20.Name = "label20";
			toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip"));
			resources.ApplyResources(label16, "label16");
			label16.Name = "label16";
			toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip"));
			resources.ApplyResources(label18, "label18");
			label18.Name = "label18";
			toolTip.SetToolTip(label18, resources.GetString("label18.ToolTip"));
			resources.ApplyResources(btnSearch, "btnSearch");
			btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
			btnSearch.ForeColor = System.Drawing.Color.White;
			btnSearch.Name = "btnSearch";
			btnSearch.TabStop = false;
			toolTip.SetToolTip(btnSearch, resources.GetString("btnSearch.ToolTip"));
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
			resources.ApplyResources(btnPayment, "btnPayment");
			btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
			btnPayment.ForeColor = System.Drawing.Color.White;
			btnPayment.Name = "btnPayment";
			btnPayment.TabStop = false;
			toolTip.SetToolTip(btnPayment, resources.GetString("btnPayment.ToolTip"));
			btnPayment.UseVisualStyleBackColor = false;
			btnPayment.Click += new System.EventHandler(btnPayment_Click);
			resources.ApplyResources(btnMail, "btnMail");
			btnMail.BackColor = System.Drawing.Color.DodgerBlue;
			btnMail.ForeColor = System.Drawing.Color.White;
			btnMail.Name = "btnMail";
			btnMail.TabStop = false;
			toolTip.SetToolTip(btnMail, resources.GetString("btnMail.ToolTip"));
			btnMail.UseVisualStyleBackColor = false;
			btnMail.Click += new System.EventHandler(btnMail_Click);
			resources.ApplyResources(btnPrint, "btnPrint");
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Name = "btnPrint";
			btnPrint.TabStop = false;
			toolTip.SetToolTip(btnPrint, resources.GetString("btnPrint.ToolTip"));
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Name = "btnDelete";
			btnDelete.TabStop = false;
			toolTip.SetToolTip(btnDelete, resources.GetString("btnDelete.ToolTip"));
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			resources.ApplyResources(btnEdit, "btnEdit");
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Name = "btnEdit";
			btnEdit.TabStop = false;
			toolTip.SetToolTip(btnEdit, resources.GetString("btnEdit.ToolTip"));
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Name = "btnSave";
			btnSave.TabStop = false;
			toolTip.SetToolTip(btnSave, resources.GetString("btnSave.ToolTip"));
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			resources.ApplyResources(lblGrandTotal, "lblGrandTotal");
			lblGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblGrandTotal.ForeColor = System.Drawing.Color.Red;
			lblGrandTotal.Format = null;
			lblGrandTotal.Name = "lblGrandTotal";
			toolTip.SetToolTip(lblGrandTotal, resources.GetString("lblGrandTotal.ToolTip"));
			lblGrandTotal.Value = new decimal(new int[4]);
			resources.ApplyResources(label36, "label36");
			label36.Name = "label36";
			toolTip.SetToolTip(label36, resources.GetString("label36.ToolTip"));
			resources.ApplyResources(txtAddDiscAmnt, "txtAddDiscAmnt");
			txtAddDiscAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAddDiscAmnt.Data = 0;
			txtAddDiscAmnt.EnterColor = System.Drawing.Color.LightPink;
			txtAddDiscAmnt.Format = null;
			txtAddDiscAmnt.isAllowNegative = false;
			txtAddDiscAmnt.isAllowSpecialChar = false;
			txtAddDiscAmnt.isNumberOnly = false;
			txtAddDiscAmnt.isNumeric = true;
			txtAddDiscAmnt.LeaveColor = System.Drawing.Color.White;
			txtAddDiscAmnt.Name = "txtAddDiscAmnt";
			toolTip.SetToolTip(txtAddDiscAmnt, resources.GetString("txtAddDiscAmnt.ToolTip"));
			txtAddDiscAmnt.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtAddDiscAmnt.Validated += new System.EventHandler(txtAddDiscAmnt_Validated);
			resources.ApplyResources(txtAddDisc, "txtAddDisc");
			txtAddDisc.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAddDisc.Data = 0;
			txtAddDisc.EnterColor = System.Drawing.Color.LightPink;
			txtAddDisc.Format = null;
			txtAddDisc.isAllowNegative = false;
			txtAddDisc.isAllowSpecialChar = false;
			txtAddDisc.isNumberOnly = false;
			txtAddDisc.isNumeric = true;
			txtAddDisc.LeaveColor = System.Drawing.Color.White;
			txtAddDisc.Name = "txtAddDisc";
			toolTip.SetToolTip(txtAddDisc, resources.GetString("txtAddDisc.ToolTip"));
			txtAddDisc.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtAddDisc.Validated += new System.EventHandler(txtAddDisc_Validated);
			resources.ApplyResources(label15, "label15");
			label15.Name = "label15";
			toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip"));
			resources.ApplyResources(label35, "label35");
			label35.Name = "label35";
			toolTip.SetToolTip(label35, resources.GetString("label35.ToolTip"));
			resources.ApplyResources(txtFreight, "txtFreight");
			txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFreight.Data = 0;
			txtFreight.EnterColor = System.Drawing.Color.LightPink;
			txtFreight.Format = null;
			txtFreight.isAllowNegative = false;
			txtFreight.isAllowSpecialChar = false;
			txtFreight.isNumberOnly = false;
			txtFreight.isNumeric = true;
			txtFreight.LeaveColor = System.Drawing.Color.White;
			txtFreight.Name = "txtFreight";
			toolTip.SetToolTip(txtFreight, resources.GetString("txtFreight.ToolTip"));
			txtFreight.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtFreight.TextChanged += new System.EventHandler(txtAddAmnt_TextChanged);
			resources.ApplyResources(txtAddAmnt, "txtAddAmnt");
			txtAddAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAddAmnt.Data = 0;
			txtAddAmnt.EnterColor = System.Drawing.Color.LightPink;
			txtAddAmnt.Format = null;
			txtAddAmnt.isAllowNegative = false;
			txtAddAmnt.isAllowSpecialChar = false;
			txtAddAmnt.isNumberOnly = false;
			txtAddAmnt.isNumeric = true;
			txtAddAmnt.LeaveColor = System.Drawing.Color.White;
			txtAddAmnt.Name = "txtAddAmnt";
			toolTip.SetToolTip(txtAddAmnt, resources.GetString("txtAddAmnt.ToolTip"));
			txtAddAmnt.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtAddAmnt.TextChanged += new System.EventHandler(txtAddAmnt_TextChanged);
			resources.ApplyResources(lblNetTotal, "lblNetTotal");
			lblNetTotal.ForeColor = System.Drawing.Color.DodgerBlue;
			lblNetTotal.Format = null;
			lblNetTotal.Name = "lblNetTotal";
			toolTip.SetToolTip(lblNetTotal, resources.GetString("lblNetTotal.ToolTip"));
			lblNetTotal.Value = new decimal(new int[4]);
			resources.ApplyResources(label34, "label34");
			label34.Name = "label34";
			toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip"));
			resources.ApplyResources(label33, "label33");
			label33.Name = "label33";
			toolTip.SetToolTip(label33, resources.GetString("label33.ToolTip"));
			resources.ApplyResources(cbMail, "cbMail");
			cbMail.Name = "cbMail";
			cbMail.TabStop = false;
			toolTip.SetToolTip(cbMail, resources.GetString("cbMail.ToolTip"));
			cbMail.UseVisualStyleBackColor = true;
			cbMail.CheckedChanged += new System.EventHandler(cbMail_CheckedChanged);
			resources.ApplyResources(cbPrint, "cbPrint");
			cbPrint.Name = "cbPrint";
			cbPrint.TabStop = false;
			toolTip.SetToolTip(cbPrint, resources.GetString("cbPrint.ToolTip"));
			cbPrint.UseVisualStyleBackColor = true;
			cbPrint.CheckedChanged += new System.EventHandler(cbPrint_CheckedChanged);
			resources.ApplyResources(lblDisTotal, "lblDisTotal");
			lblDisTotal.ForeColor = System.Drawing.Color.DodgerBlue;
			lblDisTotal.Format = null;
			lblDisTotal.Name = "lblDisTotal";
			toolTip.SetToolTip(lblDisTotal, resources.GetString("lblDisTotal.ToolTip"));
			lblDisTotal.Value = new decimal(new int[4]);
			resources.ApplyResources(lblGrsTotal, "lblGrsTotal");
			lblGrsTotal.ForeColor = System.Drawing.Color.DodgerBlue;
			lblGrsTotal.Format = null;
			lblGrsTotal.Name = "lblGrsTotal";
			toolTip.SetToolTip(lblGrsTotal, resources.GetString("lblGrsTotal.ToolTip"));
			lblGrsTotal.Value = new decimal(new int[4]);
			resources.ApplyResources(lblTotalQty, "lblTotalQty");
			lblTotalQty.ForeColor = System.Drawing.Color.DodgerBlue;
			lblTotalQty.Format = null;
			lblTotalQty.Name = "lblTotalQty";
			toolTip.SetToolTip(lblTotalQty, resources.GetString("lblTotalQty.ToolTip"));
			lblTotalQty.Value = new decimal(new int[4]);
			resources.ApplyResources(lblUntQty, "lblUntQty");
			lblUntQty.ForeColor = System.Drawing.Color.Magenta;
			lblUntQty.Format = null;
			lblUntQty.Name = "lblUntQty";
			toolTip.SetToolTip(lblUntQty, resources.GetString("lblUntQty.ToolTip"));
			lblUntQty.Value = new decimal(new int[4]);
			resources.ApplyResources(lblLastSalesRate, "lblLastSalesRate");
			lblLastSalesRate.ForeColor = System.Drawing.Color.Red;
			lblLastSalesRate.Format = null;
			lblLastSalesRate.Name = "lblLastSalesRate";
			toolTip.SetToolTip(lblLastSalesRate, resources.GetString("lblLastSalesRate.ToolTip"));
			lblLastSalesRate.Value = new decimal(new int[4]);
			resources.ApplyResources(lblCStock, "lblCStock");
			lblCStock.ForeColor = System.Drawing.Color.Red;
			lblCStock.Format = null;
			lblCStock.Name = "lblCStock";
			toolTip.SetToolTip(lblCStock, resources.GetString("lblCStock.ToolTip"));
			lblCStock.Value = new decimal(new int[4]);
			resources.ApplyResources(btnSRtn, "btnSRtn");
			btnSRtn.BackColor = System.Drawing.Color.DodgerBlue;
			btnSRtn.ForeColor = System.Drawing.Color.White;
			btnSRtn.Name = "btnSRtn";
			btnSRtn.TabStop = false;
			toolTip.SetToolTip(btnSRtn, resources.GetString("btnSRtn.ToolTip"));
			btnSRtn.UseVisualStyleBackColor = false;
			btnSRtn.Click += new System.EventHandler(btnSRtn_Click);
			resources.ApplyResources(dgDetails, "dgDetails");
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgDetails.Columns.AddRange(col_SL, col_ProCode, col_ProName, col_Barcode, col_Qty, col_Unit, col_Rate, col_Gross, col_Dis, col_Discount, col_NetAmnt);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgDetails.DefaultCellStyle = dataGridViewCellStyle3;
			dgDetails.EnableHeadersVisualStyles = false;
			dgDetails.EnterKeyNavigation = false;
			dgDetails.LastKey = System.Windows.Forms.Keys.None;
			dgDetails.Name = "dgDetails";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			dgDetails.RowHeadersVisible = false;
			dgDetails.RowTemplate.Height = 25;
			toolTip.SetToolTip(dgDetails, resources.GetString("dgDetails.ToolTip"));
			dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgSales_CellEndEdit);
			dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgSales_DataError);
			dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgSales_EditingControlShowing);
			dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgSales_KeyDown);
			col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SL.DataPropertyName = "SL";
			resources.ApplyResources(col_SL, "col_SL");
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_ProCode.DataPropertyName = "ProductCode";
			resources.ApplyResources(col_ProCode, "col_ProCode");
			col_ProCode.Name = "col_ProCode";
			col_ProName.DataPropertyName = "ProductName";
			resources.ApplyResources(col_ProName, "col_ProName");
			col_ProName.Name = "col_ProName";
			col_Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Barcode.DataPropertyName = "Barcode";
			resources.ApplyResources(col_Barcode, "col_Barcode");
			col_Barcode.Name = "col_Barcode";
			col_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Qty.DataPropertyName = "Quantity";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Qty.DefaultCellStyle = dataGridViewCellStyle5;
			resources.ApplyResources(col_Qty, "col_Qty");
			col_Qty.Name = "col_Qty";
			col_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Unit.DataPropertyName = "Unit";
			resources.ApplyResources(col_Unit, "col_Unit");
			col_Unit.Name = "col_Unit";
			col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle6;
			resources.ApplyResources(col_Rate, "col_Rate");
			col_Rate.Name = "col_Rate";
			col_Gross.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Gross.DataPropertyName = "GrossAmount";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Gross.DefaultCellStyle = dataGridViewCellStyle7;
			resources.ApplyResources(col_Gross, "col_Gross");
			col_Gross.Name = "col_Gross";
			col_Gross.ReadOnly = true;
			col_Dis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Dis.DataPropertyName = "Discount";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Dis.DefaultCellStyle = dataGridViewCellStyle8;
			resources.ApplyResources(col_Dis, "col_Dis");
			col_Dis.Name = "col_Dis";
			col_Discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Discount.DataPropertyName = "DiscountAmount";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Discount.DefaultCellStyle = dataGridViewCellStyle9;
			resources.ApplyResources(col_Discount, "col_Discount");
			col_Discount.Name = "col_Discount";
			col_Discount.ReadOnly = true;
			col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_NetAmnt.DataPropertyName = "NETAmount";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle10;
			resources.ApplyResources(col_NetAmnt, "col_NetAmnt");
			col_NetAmnt.Name = "col_NetAmnt";
			col_NetAmnt.ReadOnly = true;
			resources.ApplyResources(label32, "label32");
			label32.Name = "label32";
			toolTip.SetToolTip(label32, resources.GetString("label32.ToolTip"));
			resources.ApplyResources(btnBalance, "btnBalance");
			btnBalance.Name = "btnBalance";
			btnBalance.TabStop = false;
			toolTip.SetToolTip(btnBalance, resources.GetString("btnBalance.ToolTip"));
			btnBalance.UseVisualStyleBackColor = true;
			btnBalance.Click += new System.EventHandler(btnBalance_Click);
			resources.ApplyResources(lblPreBill, "lblPreBill");
			lblPreBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblPreBill.ForeColor = System.Drawing.Color.Red;
			lblPreBill.Name = "lblPreBill";
			toolTip.SetToolTip(lblPreBill, resources.GetString("lblPreBill.ToolTip"));
			resources.ApplyResources(label19, "label19");
			label19.Name = "label19";
			toolTip.SetToolTip(label19, resources.GetString("label19.ToolTip"));
			resources.ApplyResources(cmbProCategory, "cmbProCategory");
			cmbProCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProCategory.EnterColor = System.Drawing.Color.LightPink;
			cmbProCategory.FormattingEnabled = true;
			cmbProCategory.LeaveColor = System.Drawing.Color.White;
			cmbProCategory.Name = "cmbProCategory";
			toolTip.SetToolTip(cmbProCategory, resources.GetString("cmbProCategory.ToolTip"));
			resources.ApplyResources(label13, "label13");
			label13.Name = "label13";
			toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
			resources.ApplyResources(txtRemarks, "txtRemarks");
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtRemarks.Data = 0;
			txtRemarks.EnterColor = System.Drawing.Color.LightPink;
			txtRemarks.Format = null;
			txtRemarks.isAllowNegative = false;
			txtRemarks.isAllowSpecialChar = false;
			txtRemarks.isNumberOnly = false;
			txtRemarks.isNumeric = false;
			txtRemarks.LeaveColor = System.Drawing.Color.White;
			txtRemarks.Name = "txtRemarks";
			toolTip.SetToolTip(txtRemarks, resources.GetString("txtRemarks.ToolTip"));
			txtRemarks.Value = new decimal(new int[4]);
			resources.ApplyResources(label12, "label12");
			label12.Name = "label12";
			toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip"));
			resources.ApplyResources(cmbSlsExi, "cmbSlsExi");
			cmbSlsExi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSlsExi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSlsExi.EnterColor = System.Drawing.Color.LightPink;
			cmbSlsExi.FormattingEnabled = true;
			cmbSlsExi.LeaveColor = System.Drawing.Color.White;
			cmbSlsExi.Name = "cmbSlsExi";
			toolTip.SetToolTip(cmbSlsExi, resources.GetString("cmbSlsExi.ToolTip"));
			resources.ApplyResources(label11, "label11");
			label11.Name = "label11";
			toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
			resources.ApplyResources(cmbPartyAccnt, "cmbPartyAccnt");
			cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPartyAccnt.EnterColor = System.Drawing.Color.LightPink;
			cmbPartyAccnt.FormattingEnabled = true;
			cmbPartyAccnt.LeaveColor = System.Drawing.Color.White;
			cmbPartyAccnt.Name = "cmbPartyAccnt";
			toolTip.SetToolTip(cmbPartyAccnt, resources.GetString("cmbPartyAccnt.ToolTip"));
			cmbPartyAccnt.SelectedIndexChanged += new System.EventHandler(cmbPartyAccnt_SelectedIndexChanged);
			cmbPartyAccnt.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbPartyAccnt_KeyDown);
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
			resources.ApplyResources(cmbVehicle, "cmbVehicle");
			cmbVehicle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbVehicle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
			cmbVehicle.FormattingEnabled = true;
			cmbVehicle.LeaveColor = System.Drawing.Color.White;
			cmbVehicle.Name = "cmbVehicle";
			toolTip.SetToolTip(cmbVehicle, resources.GetString("cmbVehicle.ToolTip"));
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
			resources.ApplyResources(btnAccCreate, "btnAccCreate");
			btnAccCreate.BackColor = System.Drawing.Color.DodgerBlue;
			btnAccCreate.ForeColor = System.Drawing.Color.White;
			btnAccCreate.Name = "btnAccCreate";
			btnAccCreate.TabStop = false;
			toolTip.SetToolTip(btnAccCreate, resources.GetString("btnAccCreate.ToolTip"));
			btnAccCreate.UseVisualStyleBackColor = false;
			btnAccCreate.Click += new System.EventHandler(btnAccCreate_Click);
			resources.ApplyResources(btnOrder, "btnOrder");
			btnOrder.BackColor = System.Drawing.Color.DodgerBlue;
			btnOrder.FlatAppearance.BorderSize = 0;
			btnOrder.ForeColor = System.Drawing.Color.White;
			btnOrder.Name = "btnOrder";
			btnOrder.TabStop = false;
			toolTip.SetToolTip(btnOrder, resources.GetString("btnOrder.ToolTip"));
			btnOrder.UseVisualStyleBackColor = false;
			btnOrder.Click += new System.EventHandler(btnOrder_Click);
			resources.ApplyResources(cmbRoute, "cmbRoute");
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Name = "cmbRoute";
			toolTip.SetToolTip(cmbRoute, resources.GetString("cmbRoute.ToolTip"));
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
			resources.ApplyResources(cmbDepot, "cmbDepot");
			cmbDepot.EnterColor = System.Drawing.Color.LightPink;
			cmbDepot.FormattingEnabled = true;
			cmbDepot.LeaveColor = System.Drawing.Color.White;
			cmbDepot.Name = "cmbDepot";
			toolTip.SetToolTip(cmbDepot, resources.GetString("cmbDepot.ToolTip"));
			resources.ApplyResources(cmbSlsAcct, "cmbSlsAcct");
			cmbSlsAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbSlsAcct.FormattingEnabled = true;
			cmbSlsAcct.LeaveColor = System.Drawing.Color.White;
			cmbSlsAcct.Name = "cmbSlsAcct";
			cmbSlsAcct.TabStop = false;
			toolTip.SetToolTip(cmbSlsAcct, resources.GetString("cmbSlsAcct.ToolTip"));
			resources.ApplyResources(label7, "label7");
			label7.Name = "label7";
			toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
			resources.ApplyResources(nudInvoiceNo, "nudInvoiceNo");
			nudInvoiceNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudInvoiceNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.Name = "nudInvoiceNo";
			nudInvoiceNo.TabStop = false;
			toolTip.SetToolTip(nudInvoiceNo, resources.GetString("nudInvoiceNo.ToolTip"));
			nudInvoiceNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.ValueChanged += new System.EventHandler(nudInvoiceNo_ValueChanged);
			resources.ApplyResources(label25, "label25");
			label25.Name = "label25";
			toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
			resources.ApplyResources(dtmDate, "dtmDate");
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Name = "dtmDate";
			dtmDate.TabStop = false;
			toolTip.SetToolTip(dtmDate, resources.GetString("dtmDate.ToolTip"));
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
			resources.ApplyResources(chkEnableInvNo, "chkEnableInvNo");
			chkEnableInvNo.Name = "chkEnableInvNo";
			chkEnableInvNo.TabStop = false;
			toolTip.SetToolTip(chkEnableInvNo, resources.GetString("chkEnableInvNo.ToolTip"));
			chkEnableInvNo.UseVisualStyleBackColor = true;
			chkEnableInvNo.CheckedChanged += new System.EventHandler(chkEnableInvNo_CheckedChanged);
			resources.ApplyResources(btnDate, "btnDate");
			btnDate.Name = "btnDate";
			btnDate.TabStop = false;
			toolTip.SetToolTip(btnDate, resources.GetString("btnDate.ToolTip"));
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			resources.ApplyResources(label8, "label8");
			label8.Name = "label8";
			toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
			resources.ApplyResources(lblCardPaid, "lblCardPaid");
			lblCardPaid.Format = null;
			lblCardPaid.Name = "lblCardPaid";
			toolTip.SetToolTip(lblCardPaid, resources.GetString("lblCardPaid.ToolTip"));
			lblCardPaid.Value = new decimal(new int[4]);
			resources.ApplyResources(btnLast, "btnLast");
			btnLast.BackColor = System.Drawing.Color.DodgerBlue;
			btnLast.ForeColor = System.Drawing.Color.White;
			btnLast.Name = "btnLast";
			btnLast.TabStop = false;
			toolTip.SetToolTip(btnLast, resources.GetString("btnLast.ToolTip"));
			btnLast.UseVisualStyleBackColor = false;
			btnLast.Click += new System.EventHandler(btnLast_Click);
			resources.ApplyResources(btnNext, "btnNext");
			btnNext.BackColor = System.Drawing.Color.DodgerBlue;
			btnNext.ForeColor = System.Drawing.Color.White;
			btnNext.Name = "btnNext";
			btnNext.TabStop = false;
			toolTip.SetToolTip(btnNext, resources.GetString("btnNext.ToolTip"));
			btnNext.UseVisualStyleBackColor = false;
			btnNext.Click += new System.EventHandler(btnNext_Click);
			resources.ApplyResources(btnBack, "btnBack");
			btnBack.BackColor = System.Drawing.Color.DodgerBlue;
			btnBack.ForeColor = System.Drawing.Color.White;
			btnBack.Name = "btnBack";
			btnBack.TabStop = false;
			toolTip.SetToolTip(btnBack, resources.GetString("btnBack.ToolTip"));
			btnBack.UseVisualStyleBackColor = false;
			btnBack.Click += new System.EventHandler(btnBack_Click);
			resources.ApplyResources(btnFirst, "btnFirst");
			btnFirst.BackColor = System.Drawing.Color.DodgerBlue;
			btnFirst.ForeColor = System.Drawing.Color.White;
			btnFirst.Name = "btnFirst";
			btnFirst.TabStop = false;
			toolTip.SetToolTip(btnFirst, resources.GetString("btnFirst.ToolTip"));
			btnFirst.UseVisualStyleBackColor = false;
			btnFirst.Click += new System.EventHandler(btnFirst_Click);
			resources.ApplyResources(btnPDF, "btnPDF");
			btnPDF.BackColor = System.Drawing.Color.DodgerBlue;
			btnPDF.ForeColor = System.Drawing.Color.White;
			btnPDF.Name = "btnPDF";
			btnPDF.TabStop = false;
			toolTip.SetToolTip(btnPDF, resources.GetString("btnPDF.ToolTip"));
			btnPDF.UseVisualStyleBackColor = false;
			btnPDF.Click += new System.EventHandler(btnPDF_Click);
			resources.ApplyResources(label9, "label9");
			label9.Name = "label9";
			toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip"));
			resources.ApplyResources(lblOnline, "lblOnline");
			lblOnline.Format = null;
			lblOnline.Name = "lblOnline";
			toolTip.SetToolTip(lblOnline, resources.GetString("lblOnline.ToolTip"));
			lblOnline.Value = new decimal(new int[4]);
			resources.ApplyResources(cbMappingBarcodeBased, "cbMappingBarcodeBased");
			cbMappingBarcodeBased.Name = "cbMappingBarcodeBased";
			cbMappingBarcodeBased.TabStop = false;
			toolTip.SetToolTip(cbMappingBarcodeBased, resources.GetString("cbMappingBarcodeBased.ToolTip"));
			cbMappingBarcodeBased.UseVisualStyleBackColor = true;
			cbMappingBarcodeBased.CheckedChanged += new System.EventHandler(cbMappingBarcodeBased_CheckedChanged);
			resources.ApplyResources(lblRunBal, "lblRunBal");
			lblRunBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblRunBal.ForeColor = System.Drawing.Color.Red;
			lblRunBal.Name = "lblRunBal";
			toolTip.SetToolTip(lblRunBal, resources.GetString("lblRunBal.ToolTip"));
			resources.ApplyResources(btnCustomerSearch, "btnCustomerSearch");
			btnCustomerSearch.BackColor = System.Drawing.Color.DodgerBlue;
			btnCustomerSearch.ForeColor = System.Drawing.Color.White;
			btnCustomerSearch.Name = "btnCustomerSearch";
			btnCustomerSearch.TabStop = false;
			toolTip.SetToolTip(btnCustomerSearch, resources.GetString("btnCustomerSearch.ToolTip"));
			btnCustomerSearch.UseVisualStyleBackColor = false;
			btnCustomerSearch.Click += new System.EventHandler(btnCustomerSearch_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnCustomerSearch);
			base.Controls.Add(lblRunBal);
			base.Controls.Add(cbMappingBarcodeBased);
			base.Controls.Add(btnLast);
			base.Controls.Add(btnNext);
			base.Controls.Add(btnBack);
			base.Controls.Add(btnFirst);
			base.Controls.Add(btnClear);
			base.Controls.Add(button1);
			base.Controls.Add(txtPrintCopy);
			base.Controls.Add(lblBalance);
			base.Controls.Add(lblSReturnAmnt);
			base.Controls.Add(lblOnline);
			base.Controls.Add(lblCardPaid);
			base.Controls.Add(lblCashPaid);
			base.Controls.Add(label20);
			base.Controls.Add(label16);
			base.Controls.Add(label9);
			base.Controls.Add(label8);
			base.Controls.Add(label18);
			base.Controls.Add(btnSearch);
			base.Controls.Add(btnPayment);
			base.Controls.Add(btnMail);
			base.Controls.Add(btnPrint);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnEdit);
			base.Controls.Add(btnPDF);
			base.Controls.Add(btnSave);
			base.Controls.Add(lblGrandTotal);
			base.Controls.Add(label36);
			base.Controls.Add(txtAddDiscAmnt);
			base.Controls.Add(txtAddDisc);
			base.Controls.Add(label15);
			base.Controls.Add(label35);
			base.Controls.Add(txtFreight);
			base.Controls.Add(txtAddAmnt);
			base.Controls.Add(lblNetTotal);
			base.Controls.Add(label34);
			base.Controls.Add(label33);
			base.Controls.Add(cbMail);
			base.Controls.Add(cbPrint);
			base.Controls.Add(lblDisTotal);
			base.Controls.Add(lblGrsTotal);
			base.Controls.Add(lblTotalQty);
			base.Controls.Add(lblUntQty);
			base.Controls.Add(lblLastSalesRate);
			base.Controls.Add(lblCStock);
			base.Controls.Add(btnSRtn);
			base.Controls.Add(dgDetails);
			base.Controls.Add(label32);
			base.Controls.Add(btnBalance);
			base.Controls.Add(lblPreBill);
			base.Controls.Add(label19);
			base.Controls.Add(cmbProCategory);
			base.Controls.Add(label13);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label12);
			base.Controls.Add(cmbSlsExi);
			base.Controls.Add(label11);
			base.Controls.Add(cmbPartyAccnt);
			base.Controls.Add(label6);
			base.Controls.Add(cmbVehicle);
			base.Controls.Add(label5);
			base.Controls.Add(btnAccCreate);
			base.Controls.Add(btnOrder);
			base.Controls.Add(cmbRoute);
			base.Controls.Add(label4);
			base.Controls.Add(cmbDepot);
			base.Controls.Add(cmbSlsAcct);
			base.Controls.Add(label7);
			base.Controls.Add(label3);
			base.Controls.Add(nudInvoiceNo);
			base.Controls.Add(label25);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label1);
			base.Controls.Add(label2);
			base.Controls.Add(chkEnableInvNo);
			base.Controls.Add(btnDate);
			base.Name = "SalesEstimateView";
			toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			base.Load += new System.EventHandler(SalesEstimateView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SalesEstimateView_KeyDown);
			base.Controls.SetChildIndex(btnDate, 0);
			base.Controls.SetChildIndex(chkEnableInvNo, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(label25, 0);
			base.Controls.SetChildIndex(nudInvoiceNo, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cmbSlsAcct, 0);
			base.Controls.SetChildIndex(cmbDepot, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(cmbRoute, 0);
			base.Controls.SetChildIndex(btnOrder, 0);
			base.Controls.SetChildIndex(btnAccCreate, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(cmbVehicle, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(cmbPartyAccnt, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(cmbSlsExi, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(cmbProCategory, 0);
			base.Controls.SetChildIndex(label19, 0);
			base.Controls.SetChildIndex(lblPreBill, 0);
			base.Controls.SetChildIndex(btnBalance, 0);
			base.Controls.SetChildIndex(label32, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			base.Controls.SetChildIndex(btnSRtn, 0);
			base.Controls.SetChildIndex(lblCStock, 0);
			base.Controls.SetChildIndex(lblLastSalesRate, 0);
			base.Controls.SetChildIndex(lblUntQty, 0);
			base.Controls.SetChildIndex(lblTotalQty, 0);
			base.Controls.SetChildIndex(lblGrsTotal, 0);
			base.Controls.SetChildIndex(lblDisTotal, 0);
			base.Controls.SetChildIndex(cbPrint, 0);
			base.Controls.SetChildIndex(cbMail, 0);
			base.Controls.SetChildIndex(label33, 0);
			base.Controls.SetChildIndex(label34, 0);
			base.Controls.SetChildIndex(lblNetTotal, 0);
			base.Controls.SetChildIndex(txtAddAmnt, 0);
			base.Controls.SetChildIndex(txtFreight, 0);
			base.Controls.SetChildIndex(label35, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(txtAddDisc, 0);
			base.Controls.SetChildIndex(txtAddDiscAmnt, 0);
			base.Controls.SetChildIndex(label36, 0);
			base.Controls.SetChildIndex(lblGrandTotal, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnPDF, 0);
			base.Controls.SetChildIndex(btnEdit, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnPrint, 0);
			base.Controls.SetChildIndex(btnMail, 0);
			base.Controls.SetChildIndex(btnPayment, 0);
			base.Controls.SetChildIndex(btnSearch, 0);
			base.Controls.SetChildIndex(label18, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label16, 0);
			base.Controls.SetChildIndex(label20, 0);
			base.Controls.SetChildIndex(lblCashPaid, 0);
			base.Controls.SetChildIndex(lblCardPaid, 0);
			base.Controls.SetChildIndex(lblOnline, 0);
			base.Controls.SetChildIndex(lblSReturnAmnt, 0);
			base.Controls.SetChildIndex(lblBalance, 0);
			base.Controls.SetChildIndex(txtPrintCopy, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(btnFirst, 0);
			base.Controls.SetChildIndex(btnBack, 0);
			base.Controls.SetChildIndex(btnNext, 0);
			base.Controls.SetChildIndex(btnLast, 0);
			base.Controls.SetChildIndex(cbMappingBarcodeBased, 0);
			base.Controls.SetChildIndex(lblRunBal, 0);
			base.Controls.SetChildIndex(btnCustomerSearch, 0);
			((System.ComponentModel.ISupportInitialize)bdsSales).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
