using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
	public class SalesReturnView : BaseForm
	{
		private DateTime estimateDate;

		private List<SettingsEntity> entBooleanSettings;

		private List<Transaction> entTransactions;

		private SalesRtnMaster entSalesRtnMaster;

		private List<SalesRtnDetails> entSalesrtnDetails;

		private int iFYearId;

		private int iCashAcctId;

		private int? iMasterId;

		private ExCompany entcompany;

		private TransactionMaster entTMaster = new TransactionMaster();

		private PartyDetails entPartyDtl;

		private List<POSDetails> entPOSDetails;

		private DataGridViewTextBoxEditingControl textbox;

		private DBContext db;

		private string sVoucherType = "Sales Return";

		private List<ComboFill> comboFills = new List<ComboFill>();

		private decimal dSubDiscount;

		private IContainer components = null;

		private Button btnClear;

		private Button button1;

		private AshTextBoxNormal txtPrintCopy;

		private Button btnSeperator2;

		private AshNumericLabel lblBalance;

		private AshNumericLabel lblPaidAmount;

		private Label label16;

		private Label label18;

		private Button btnSearch;

		private Button btnPayment;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private Label label23;

		private Label label93;

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

		private BindingSource bdsSalesReturn;

		private Label label90;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private AshTextBoxNormal txtAddAmnt;

		private AshNumericLabel lblNetTotal;

		private Label label34;

		private Label label33;

		private CheckBox cbPrint;

		private AshNumericLabel lblTaxable;

		private AshNumericLabel lblDisTotal;

		private AshNumericLabel lblGrsTotal;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblUntQty;

		private AshNumericLabel lblLastSalesRate;

		private AshNumericLabel lblCStock;

		private Button btnEstimate;

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

		private AshComboBox cmbSalesType;

		private Label label39;

		private CheckBox cbMail;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProName;

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

		private Label lblRunBal;

		private ToolTip toolTip;

		private FlowLayoutPanel flpButtons;

		private Button btnDiscount;

		private Button btnCustomerSearch;

		public int? iInvNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public decimal dmlReturnAmount { get; set; }

		public int? SalesInvId { get; set; }

		public SalesReturnView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			lblPaidAmount.Format = CommonSettings.strCurencyFormat;
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
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Dis.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Vat.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			if (Utility.uManufacturer != "Ashpro")
			{
				btnDelete.Visible = false;
			}
		}

		private void LoadComboMethod()
		{
			AniHelper.FillAccount(cmbPartyAccnt);
			AniHelper.FillSalesRtnAccount(cmbSlsAcct);
			AniHelper.FillEmployees(cmbSlsExi);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillPCategory(cmbProCategory);
			AniHelper.FillGodown(cmbDepot);
			AniHelper.FillVehicle(cmbVehicle);
			AniHelper.FillFormType(cmbSalesType);
		}

		private void LoadSettingsMethod()
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
			entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == sVoucherType && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
			chkEnableInvNo.Checked = CommonSettings.blnEditInvoiceNumber;
			cbPrint.Checked = CommonSettings.blnPntInvWlSaving;
			cbMail.Checked = CommonSettings.blnSndMlWlSaving;
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
				col_Vat.Visible = false;
				col_VatAmnt.Visible = false;
				if (CommonSettings.blnShowGSTInInvoice)
				{
					col_GST.Visible = true;
				}
				if (CommonSettings.blnShowGSTAmountInInvoice)
				{
					col_GSTAmnt.Visible = true;
				}
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
				if (CommonSettings.blnShowVatInInvoice)
				{
					col_GST.Visible = true;
				}
				if (CommonSettings.blnShowVatAmountInInvoice)
				{
					col_GSTAmnt.Visible = true;
				}
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
			}
			catch (Exception)
			{
			}
			try
			{
				lblRunBal.Text = AniHelper.AccountBalanceFindMethod(cmbPartyAccnt.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)));
			}
			catch (Exception ex2)
			{
				Messages.ErrorMessage(ex2.Message);
			}
		}

		private void SalesInvoiceLoadMethod(int iInvId)
		{
			SalesMaster salesMaster = db.SalesMasters(new
			{
				SalesInvId = iInvId
			}).FirstOrDefault();
			if (salesMaster != null)
			{
				List<SalesDetails> salesDetails = db.SalesDetails(new
				{
					SalesInvId = iInvId
				}).ToList();
				entTransactions = POSHelper.GetTransactionsFromSDetails(salesDetails, db);
				cmbPartyAccnt.SelectedValue = salesMaster.PartyAccountId.GetValueOrDefault();
				cmbRoute.SelectedValue = salesMaster.RouteId.GetValueOrDefault();
				cmbVehicle.SelectedValue = salesMaster.VehId.GetValueOrDefault();
				cmbProCategory.SelectedValue = salesMaster.PriceCategoryId.GetValueOrDefault();
				txtAddAmnt.Value = salesMaster.AditionalAmount;
				txtFreight.Value = salesMaster.Frieght;
				decimal billDiscount = salesMaster.BillDiscount;
				decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
				decimal num2 = default(decimal);
				if (billDiscount > 0m && num > 0m)
				{
					num2 = billDiscount / num * 100m;
				}
				txtAddDisc.Text = num2.ToString(CommonSettings.strCurencyFormat);
				txtAddDiscAmnt.Value = billDiscount;
				txtRemarks.Text = salesMaster.Remarks;
				BindDgv();
				TotalFindMethod();
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

		private void TotalFindMethod()
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
			lblGrandTotal.Text = (lblNetTotal.Text.ToDecimal() + txtAddAmnt.Text.ToDecimal() + txtFreight.Text.ToDecimal() - txtAddDiscAmnt.Text.ToDecimal()).ToString(CommonSettings.strCurencyFormat);
		}

		private void LoadFromSales(string sFormType)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = sFormType;
				formSearch.sInvType = "Sales Invoice";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					SalesInvoiceLoadMethod(formSearch.iInvoiceId);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void LoadSalesReturnFromID()
		{
			nudInvoiceNo.Value = entSalesRtnMaster.SlRtnInvNo.toInt32();
			entSalesrtnDetails = db.SalesRtnDetails(new
			{
				SlsRtnId = entSalesRtnMaster.SlsRtrnId
			}).ToList();
			entTransactions = POSHelper.GetTransactionsFromSRDetails(entSalesrtnDetails, db);
			iMasterId = entSalesRtnMaster.SlsRtrnId;
			dtmDate.Value = entSalesRtnMaster.SlsRtnDate;
			cmbSlsAcct.SelectedValue = entSalesRtnMaster.SlsRtnAccountId.GetValueOrDefault();
			cmbPartyAccnt.SelectedValue = entSalesRtnMaster.PartyAccountId.GetValueOrDefault();
			cmbRoute.SelectedValue = entSalesRtnMaster.RouteId.GetValueOrDefault();
			cmbVehicle.SelectedValue = entSalesRtnMaster.VehId.GetValueOrDefault();
			cmbProCategory.SelectedValue = entSalesRtnMaster.PriceCategoryId.GetValueOrDefault();
			txtAddAmnt.Value = entSalesRtnMaster.AditionalAmnt;
			txtFreight.Value = entSalesRtnMaster.Frieght;
			decimal billDiscount = entSalesRtnMaster.BillDiscount;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			decimal num2 = default(decimal);
			if (billDiscount > 0m && num > 0m)
			{
				num2 = billDiscount / num * 100m;
			}
			txtAddDisc.Text = num2.ToString(CommonSettings.strCurencyFormat);
			txtAddDiscAmnt.Value = billDiscount;
			txtRemarks.Text = entSalesRtnMaster.Remarks;
			BindDgv();
			TotalFindMethod();
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			flpButtons.Enabled = true;
			btnEnd.Enabled = true;
			nudInvoiceNo.Enabled = true;
			chkEnableInvNo.Enabled = true;
			txtPrintCopy.Enabled = true;
			Button button = btnSave;
			btnSearch.Enabled = false;
			button.Enabled = false;
			Button button2 = btnEdit;
			Button button3 = btnDelete;
			Button button4 = btnMail;
			btnPrint.Enabled = true;
			button4.Enabled = true;
			button3.Enabled = true;
			button2.Enabled = true;
		}

		private void PrintMethod()
		{
			DotmatrixMaster dotmatrixMaster = db.DotmatrixMasters(new
			{
				VochrType = sVoucherType,
				isDefault = true
			}).FirstOrDefault();
			if (dotmatrixMaster != null)
			{
				List<DotmatrixSettings> list = new List<DotmatrixSettings>();
				list = db.DotmatrixSettings(new { dotmatrixMaster.printMasterId }).ToList();
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entcompany.TaxId = company.GSTNo;
				entcompany.Logo = company.Logo;
				entPartyDtl = new PartyDetails();
				AccountEntity accountEntity = db.Accounts(new
				{
					AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
				}).FirstOrDefault();
				entPartyDtl = POSHelper.GetParty(accountEntity);
				int num = db.AccountGroup(new { }).FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CASH IN HAND").AGroupId.ToInt32();
				if (accountEntity.AGroupId == num)
				{
					entPartyDtl.Name = "Cash Customer";
				}
				entSalesRtnMaster = db.SalesRtnMasters(new
				{
					SlRtnInvNo = nudInvoiceNo.Value.ToInt32(),
					FK_BranchId = Settings.Default.iBranchId
				}).FirstOrDefault();
				entSalesrtnDetails = db.SalesRtnDetails(new
				{
					SlsRtnId = entSalesRtnMaster.SlsRtrnId
				}).ToList();
				entTMaster = new TransactionMaster();
				entTMaster = POSHelper.GetTMaster(entSalesRtnMaster);
				entTMaster.TotalQuantity = entTransactions.Sum((Transaction x) => x.Quantity);
				entTMaster.OrderNo = 0;
				entTMaster.OtherPayment = 0m;
				entTMaster.Tendered = default(decimal);
				entTMaster.Changes = default(decimal);
				entTMaster.InvoiceBarcode = entTMaster.InvoiceNo.ToString();
				entTransactions = new List<Transaction>();
				entTransactions = POSHelper.GetTransactionsFromSRDetails(entSalesrtnDetails, db);
				List<InvoiceAdjustment> list2 = new List<InvoiceAdjustment>();
				if (entTMaster.CashPayment > 0m)
				{
					AniHelper.AddPaymentPrint(list2, "Cash Payment", entTMaster.CashPayment.ToString(CommonSettings.strCurencyFormat), "دفع نقدا");
				}
				if (entTMaster.CardPayment > 0m)
				{
					AniHelper.AddPaymentPrint(list2, "Card Payment", entTMaster.CardPayment.ToString(CommonSettings.strCurencyFormat), "بطاقه ائتمان");
				}
				if (entTMaster.OtherPayment > 0m)
				{
					AniHelper.AddPaymentPrint(list2, "Online Payment", entTMaster.OtherPayment.ToString(CommonSettings.strCurencyFormat), "مدفوعات أخرى");
				}
				if (entTMaster.BillDiscount > 0m)
				{
					AniHelper.AddPaymentPrint(list2, "Discount", entTMaster.BillDiscount.ToString(CommonSettings.strCurencyFormat), "خصم");
				}
				AniHelper.AddPaymentPrint(list2, "Tax (%)", "Tax Amount", "Total");
				foreach (Transaction item in entTransactions)
				{
					if (!list2.Any((InvoiceAdjustment x) => x.Name == item.Vat.ToString("N0")))
					{
						decimal nm = item.Vat;
						decimal num2 = entTransactions.Where((Transaction x) => x.Vat == nm).Sum((Transaction y) => y.VatAmount);
						AniHelper.AddPaymentPrint(sRLabel: entTransactions.Where((Transaction x) => x.Vat == nm).Sum((Transaction y) => y.NETAmount).ToString(CommonSettings.strCurencyFormat), paymentPrint: list2, sName: nm.ToString("N0"), dValue: num2.ToString(CommonSettings.strCurencyFormat));
					}
				}
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(dotmatrixMaster, list, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, list2, entTMaster, isPrint: true, txtPrintCopy.Data);
				printHelper = null;
			}
			else
			{
				Messages.InformationMessage("There is no default print design. Please make your own design");
			}
		}

		private void DeleteSubMethod()
		{
			string[] obj = new string[9] { "Delete From GdwnStckTranLog Where VchrId='", null, null, null, null, null, null, null, null };
			int? num = iMasterId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType ='";
			obj[3] = sVoucherType;
			obj[4] = "' and Narration='";
			obj[5] = sVoucherType;
			obj[6] = "' and FK_BranchId='";
			obj[7] = Settings.Default.iBranchId.ToString();
			obj[8] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[9] { "Delete From StockTranLog Where VchrId='", null, null, null, null, null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType ='";
			obj2[3] = sVoucherType;
			obj2[4] = "' and Narration='";
			obj2[5] = sVoucherType;
			obj2[6] = "'  and FK_BranchId='";
			obj2[7] = Settings.Default.iBranchId.ToString();
			obj2[8] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj2));
			List<TransactionLog> list = new List<TransactionLog>();
			list = db.TransactionLogs(new
			{
				VchrId = iMasterId,
				VchrType = "Sales Return Voucher",
				FK_BranchId = Settings.Default.iBranchId
			}).ToList();
			foreach (TransactionLog item in list)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item.TranId.toInt32());
			}
			list = db.TransactionLogs(new
			{
				VchrId = iMasterId,
				VchrNo = nudInvoiceNo.Value.ToInt32(),
				VchrType = "Journal Voucher",
				VchrName = "TAX RECEIVABLE",
				FK_BranchId = Settings.Default.iBranchId
			}).ToList();
			foreach (TransactionLog item2 in list)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item2.TranId.toInt32());
			}
			AshproCommon.DeleteFromDatabase("SlsRtnSettings", "SlsRtnId", iMasterId.toInt32());
		}

		private void SalesReturnHeaderLoadMethod(SalesRtnMaster srMaster, int InvNo)
		{
			srMaster.SlsRtrnId = iMasterId;
			srMaster.SlRtnInvNo = InvNo;
			srMaster.SlsRtnDate = dtmDate.Value.Date;
			srMaster.SlsRtnAccountId = cmbSlsAcct.SelectedValue.ToInt32();
			srMaster.PartyAccountId = cmbPartyAccnt.SelectedValue.ToInt32();
			srMaster.StaffId = cmbSlsExi.SelectedValue.ToInt32();
			srMaster.RouteId = cmbRoute.SelectedValue.ToInt32();
			srMaster.VehId = cmbVehicle.SelectedValue.ToInt32();
			srMaster.PriceCategoryId = cmbProCategory.SelectedValue.ToInt32();
			srMaster.EntryDate = DateTime.Now.Date;
			srMaster.JeId = Settings.Default.intLogOnId;
			srMaster.Remarks = txtRemarks.Text;
			srMaster.AditionalAmnt = txtAddAmnt.Value;
			srMaster.Frieght = txtFreight.Value;
			srMaster.BillDiscount = txtAddDiscAmnt.Value;
			srMaster.TotalAmount = lblGrandTotal.Value;
			srMaster.Taxable = lblTaxable.Value;
			srMaster.TotalDiscount = lblDisTotal.Value;
			srMaster.TotalTax = lblTaxTotal.Value;
			srMaster.CGSTAmount = lblTotalCGST.Value;
			srMaster.SGSTAmount = lblTotalSGST.Value;
			srMaster.IGSTAmount = lblTotalIGST.Value;
			srMaster.FYearID = iFYearId;
			srMaster.FK_BranchId = Settings.Default.iBranchId;
			srMaster.Terminal = Utility.uTerminal;
			srMaster.SequenceNo = Utility.uSerialNumber;
			int? salesInvNo = db.SalesMasters(new { SalesInvId }).FirstOrDefault().SalesInvNo;
			srMaster.RefInvNo = ((!salesInvNo.HasValue) ? string.Empty : salesInvNo.ToString());
		}

		private void SaveSubMethod()
		{
			if (AshproCommon.InsertToDatabase(entSalesrtnDetails.Cast<object>().ToList(), "SlsRtnSettings"))
			{
				POSHelper.TranLogMethod(entSalesRtnMaster.TotalAmount, 0m, cmbSlsAcct.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iMasterId.ToInt32(), entSalesRtnMaster.SlRtnInvNo.toInt32(), "Sales Return Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(0m, entSalesRtnMaster.TotalAmount, cmbPartyAccnt.SelectedValue.ToInt32(), cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entSalesRtnMaster.SlRtnInvNo.toInt32(), "Sales Return Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				if (CommonSettings.blnTaxEnabled)
				{
					int num = db.Accounts(new
					{
						AccountName = "TAX RECEIVABLE"
					}).FirstOrDefault().AccountId.ToInt32();
					POSHelper.TranLogMethod(0m, entSalesRtnMaster.TotalTax, cmbSlsAcct.SelectedValue.ToInt32(), num, iMasterId.ToInt32(), entSalesRtnMaster.SlRtnInvNo.toInt32(), "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX RECEIVABLE", AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(entSalesRtnMaster.TotalTax, 0m, num, cmbSlsAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entSalesRtnMaster.SlRtnInvNo.toInt32(), "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX RECEIVABLE", AniHelper.GetDate(dtmDate.Value));
				}
				entPOSDetails = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
				POSHelper.SaveStockTranLoagMethod(entPOSDetails, iMasterId.ToInt32(), entSalesRtnMaster.SlRtnInvNo.toInt32(), sVoucherType, sVoucherType, cmbPartyAccnt.SelectedValue.ToInt32(), sVoucherType, Settings.Default.intLogOnId, cmbDepot.SelectedValue.ToInt32(), isIn: true, AniHelper.GetDate(dtmDate.Value));
				decimal value = lblPaidAmount.Value;
				if (value > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Payment Voucher' and VchrName='Cash Payment'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num2, dtmDate.Value.Date, value, "Payment Voucher", "Cash Paid On Sale Return", cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId, cmbSlsExi.SelectedValue.ToInt32(), Settings.Default.intLogOnId, "Cash Payment");
					int iVoucherID = db.VchrTransaction(new
					{
						VchrNo = num2,
						VchrType = "Payment Voucher"
					}).FirstOrDefault().VchrTranId.toInt32();
					POSHelper.TranLogMethod(0m, value, iCashAcctId, cmbPartyAccnt.SelectedValue.ToInt32(), iVoucherID, num2, "Payment Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(value, 0m, cmbPartyAccnt.SelectedValue.ToInt32(), iCashAcctId, iVoucherID, num2, "Payment Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
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
			bdsSalesReturn.DataSource = null;
			bdsSalesReturn.DataSource = entTransactions;
			dgDetails.DataSource = bdsSalesReturn;
		}

		private void FormClearMethod()
		{
			entTransactions = new List<Transaction>();
			BindDgv();
			cmbPartyAccnt.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
			cmbProCategory.SelectedIndex = 0;
		}

		private void btnClearMethod()
		{
			FormClearMethod();
			AniHelper.ClearMethod(this);
			flpButtons.Enabled = true;
			Button button = btnSave;
			btnSearch.Enabled = true;
			button.Enabled = true;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			iMasterId = null;
			cmbPartyAccnt.Focus();
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private async void SalesReturnView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
			entSalesrtnDetails = new List<SalesRtnDetails>();
			entTransactions = new List<Transaction>();
			BindDgv();
			estimateDate = Settings.Default.appDate;
			dtmDate.Value = estimateDate;
			LoadComboMethod();
			LoadSettingsMethod();
			iFYearId = db.SelectedFYears().FirstOrDefault().FyearId.ToInt32();
			if (iInvNo.HasValue)
			{
				nudInvoiceNo.Value = iInvNo.ToInt32();
			}
			else
			{
				nudInvoiceNo.Value = (db.SalesRtnMasters(new
				{
					FYearID = iFYearId,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((SalesRtnMaster y) => y.SlRtnInvNo) + 1) ?? 1;
			}
			if (SalesInvId.HasValue)
			{
				SalesInvoiceLoadMethod(SalesInvId.toInt32());
			}
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
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSR").FirstOrDefault();
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
			LedgerReport ledgerReport = new LedgerReport
			{
				MdiParent = base.MdiParent,
				ValueFromDate = AniHelper.GetFYearFromDate().ToString(),
				ValueToDate = dtmDate.Text,
				ValueLedger = cmbPartyAccnt.Text
			};
			ledgerReport.Show();
		}

		private void cmbPartyAccnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			NameAndBalanceFindMethod();
		}

		private void btnOrder_Click(object sender, EventArgs e)
		{
			LoadFromSales(cmbSalesType.Text);
		}

		private void btnEstimate_Click(object sender, EventArgs e)
		{
			LoadFromSales("Normal");
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			FormClearMethod();
			entSalesRtnMaster = db.SalesRtnMasters(new
			{
				SlRtnInvNo = nudInvoiceNo.Value.ToInt32(),
				FYearID = iFYearId
			}).FirstOrDefault();
			if (entSalesRtnMaster != null)
			{
				LoadSalesReturnFromID();
			}
			else
			{
				btnClearMethod();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = (db.SalesRtnMasters(new
			{
				FYearID = iFYearId,
				FK_BranchId = Settings.Default.iBranchId
			}).Max((SalesRtnMaster y) => y.SlRtnInvNo) + 1) ?? 1;
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
				if (Messages.DeleteConfirmationMessage())
				{
					DeleteSubMethod();
					AshproCommon.DeleteFromDatabase("SalesRtnMaster", "SlsRtrnId", iMasterId.toInt32());
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
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnSave.Enabled = true;
			btnEdit.Enabled = false;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			dgDetails.Focus();
			dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[col_ProName.Name];
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (entTransactions.Count == 0)
				{
					Messages.ErrorMessage("No records to save!");
					return;
				}
				entSalesrtnDetails = new List<SalesRtnDetails>();
				entSalesrtnDetails = POSHelper.GetSRDetailsFrmTransaction(entTransactions, cmbPartyAccnt.SelectedValue.ToInt32(), dtmDate.Value.Date);
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					entSalesRtnMaster = new SalesRtnMaster();
					int num = AshproCommon.IdFindMethod("select max(SlRtnInvNo) as maxValue from SalesRtnMaster Where FYearID='" + iFYearId + "' and FK_BranchId='" + Settings.Default.iBranchId + "'", isVoucherNo: true);
					SalesReturnHeaderLoadMethod(entSalesRtnMaster, num);
					if (entSalesrtnDetails == null)
					{
						return;
					}
					entSalesRtnMaster.SlsRtrnId = null;
					if (AshproCommon.InsertToDatabaseObj(entSalesRtnMaster, "SalesRtnMaster"))
					{
						entSalesRtnMaster = db.SalesRtnMasters(new
						{
							SlRtnInvNo = num,
							FYearID = iFYearId,
							FK_BranchId = Settings.Default.iBranchId
						}).FirstOrDefault();
						if (entSalesRtnMaster != null)
						{
							iMasterId = entSalesRtnMaster.SlsRtrnId;
							foreach (SalesRtnDetails entSalesrtnDetail in entSalesrtnDetails)
							{
								entSalesrtnDetail.SlsrtnSettingsID = null;
								entSalesrtnDetail.SlsRtnId = iMasterId.ToInt32();
							}
							SaveSubMethod();
							if (SalesInvId.HasValue)
							{
								dmlReturnAmount = lblGrandTotal.Value;
								Close();
							}
						}
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					foreach (SalesRtnDetails entSalesrtnDetail2 in entSalesrtnDetails)
					{
						entSalesrtnDetail2.SlsrtnSettingsID = null;
						entSalesrtnDetail2.SlsRtnId = iMasterId.ToInt32();
					}
					DeleteSubMethod();
					SalesReturnHeaderLoadMethod(entSalesRtnMaster, nudInvoiceNo.Value.ToInt32());
					if (AshproCommon.UpdateToDatabaseObj(entSalesRtnMaster, "SalesRtnMaster", "SlsRtrnId", iMasterId.toInt32()))
					{
						SaveSubMethod();
					}
				}
				if (cbPrint.Checked)
				{
					PrintMethod();
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgSalesReturn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Transaction current = getCurrent();
			int rowIndex = dgDetails.CurrentCell.RowIndex;
			if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
				productBarcodeEntity = AshproCommon.GetObjectWithParameter_SP<ProductBarcodeEntity>("spGetProductBarcode", new { current.Barcode });
				POSHelper.GetTransactionsFromBarcode(current, productBarcodeEntity, cmbProCategory.SelectedValue.ToInt32(), rowIndex, cmbSalesType.Text);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				POSHelper.GetTransactionsAfterEdit(current);
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

		private void dgSalesReturn_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSalesReturn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
							lblLastSalesRate.Value = AshproCommon.ValueFindMethod("Select MAX(ProRate) as LastPrice From PurRtnInvSttings Where PartyAccId  ='" + cmbPartyAccnt.SelectedValue.ToInt32() + "' and ProId ='" + proSearch.ProductId + "'");
							POSHelper.GetTransactions(current, rowIndex, cmbSalesType.Text);
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
					selectCustomer.ProductId = current.ProductId.ToInt32();
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

		private void dgSalesReturn_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Transaction current = getCurrent();
				if (e.KeyCode == Keys.Delete)
				{
					int iCount = 1;
					entTransactions.Remove(current);
					entTransactions.ForEach(delegate(Transaction x)
					{
						x.SL = iCount;
						iCount++;
					});
					BindDgv();
					TotalFindMethod();
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[col_ProName.Name];
				}
				else if (e.KeyCode == Keys.F6)
				{
					ProductTracing productTracing = new ProductTracing();
					productTracing.TypeVoucher = "Sales Return";
					productTracing.PartyAccntId = cmbPartyAccnt.SelectedValue.ToInt32();
					productTracing.ProductId = current.ProductId.ToInt32();
					productTracing.ShowDialog();
				}
				else if (e.KeyCode == Keys.F8)
				{
					AniHelper.ItemCreationMethod(current.ProductId.toInt32());
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = sVoucherType;
				formSearch.sInvType = sVoucherType;
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					entSalesRtnMaster = db.SalesRtnMasters(new
					{
						SlsRtrnId = formSearch.iInvoiceId
					}).FirstOrDefault();
					if (entSalesRtnMaster != null)
					{
						LoadSalesReturnFromID();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void SalesReturnView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint_Click(null, null);
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnMail_Click(null, null);
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSearch_Click(null, null);
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnOrder_Click(null, null);
			}
			else if (e.KeyCode == Keys.Y && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPayment_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnEstimate_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBalance_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDate_Click(null, null);
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
				lblCStock.Value = AshproCommon.ValueFindMethod("Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog Where ProductId='" + current.ProductId + "'and VchrDate <='" + AniHelper.GetDate(dtmDate.Value) + "' ");
				lblLastSalesRate.Value = AshproCommon.ValueFindMethod("Select MAX(ProRate) as LastPrice From PurRtnInvSttings Where PartyAccId  ='" + cmbPartyAccnt.SelectedValue.ToInt32() + "' and ProId ='" + current.ProductId + "'");
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
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

		private void btnPayment_Click(object sender, EventArgs e)
		{
			int num = db.Accounts(new
			{
				AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
			}).FirstOrDefault().AGroupId.ToInt32();
			int num2 = db.AccountGroup(new
			{
				GroupName = "CASH IN HAND"
			}).FirstOrDefault().AGroupId.ToInt32();
			if (num == num2)
			{
				Messages.ErrorMessage("You don't want to make payment in the case of cash sales");
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			pOSCashPayment.lblInvoiceAmount.Value = entTransactions.Sum((Transaction x) => x.NETAmount);
			pOSCashPayment.iPartyAccountId = cmbPartyAccnt.SelectedValue.ToInt32();
			if (pOSCashPayment.ShowDialog() == DialogResult.OK)
			{
				lblPaidAmount.Text = pOSCashPayment.lblPaidAmount.Value.ToString(CommonSettings.strCurencyFormat);
				lblBalance.Text = pOSCashPayment.lblBalanceAmount.Value.ToString(CommonSettings.strCurencyFormat);
				iCashAcctId = pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32();
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SalesReturnView));
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			btnClear = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
			btnSeperator2 = new System.Windows.Forms.Button();
			lblBalance = new AshUsercontrol.AshNumericLabel();
			lblPaidAmount = new AshUsercontrol.AshNumericLabel();
			label16 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			btnSearch = new System.Windows.Forms.Button();
			btnPayment = new System.Windows.Forms.Button();
			btnMail = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			label23 = new System.Windows.Forms.Label();
			label93 = new System.Windows.Forms.Label();
			lblTotalIGST = new AshUsercontrol.AshNumericLabel();
			lblTotalCGST = new AshUsercontrol.AshNumericLabel();
			lblTotalSGST = new AshUsercontrol.AshNumericLabel();
			lblTaxTotal = new AshUsercontrol.AshNumericLabel();
			label21 = new System.Windows.Forms.Label();
			lblGrandTotal = new AshUsercontrol.AshNumericLabel();
			label36 = new System.Windows.Forms.Label();
			txtAddDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
			txtAddDisc = new AshUsercontrol.AshTextBoxNormal();
			label15 = new System.Windows.Forms.Label();
			bdsSalesReturn = new System.Windows.Forms.BindingSource(components);
			label90 = new System.Windows.Forms.Label();
			label35 = new System.Windows.Forms.Label();
			txtFreight = new AshUsercontrol.AshTextBoxNormal();
			txtAddAmnt = new AshUsercontrol.AshTextBoxNormal();
			lblNetTotal = new AshUsercontrol.AshNumericLabel();
			label34 = new System.Windows.Forms.Label();
			label33 = new System.Windows.Forms.Label();
			cbPrint = new System.Windows.Forms.CheckBox();
			lblTaxable = new AshUsercontrol.AshNumericLabel();
			lblDisTotal = new AshUsercontrol.AshNumericLabel();
			lblGrsTotal = new AshUsercontrol.AshNumericLabel();
			lblTotalQty = new AshUsercontrol.AshNumericLabel();
			lblUntQty = new AshUsercontrol.AshNumericLabel();
			lblLastSalesRate = new AshUsercontrol.AshNumericLabel();
			lblCStock = new AshUsercontrol.AshNumericLabel();
			btnEstimate = new System.Windows.Forms.Button();
			dgDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_HSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Dis = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_VatAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_GST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_GSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_IGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_IGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			cmbSalesType = new AshUsercontrol.AshComboBox();
			label39 = new System.Windows.Forms.Label();
			cbMail = new System.Windows.Forms.CheckBox();
			lblRunBal = new System.Windows.Forms.Label();
			toolTip = new System.Windows.Forms.ToolTip(components);
			btnCustomerSearch = new System.Windows.Forms.Button();
			flpButtons = new System.Windows.Forms.FlowLayoutPanel();
			btnDiscount = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)bdsSalesReturn).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).BeginInit();
			flpButtons.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			toolTip.SetToolTip(btnEnd, resources.GetString("btnEnd.ToolTip"));
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Name = "btnClear";
			toolTip.SetToolTip(btnClear, resources.GetString("btnClear.ToolTip"));
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			resources.ApplyResources(button1, "button1");
			button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button1.Name = "button1";
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
			toolTip.SetToolTip(txtPrintCopy, resources.GetString("txtPrintCopy.ToolTip"));
			txtPrintCopy.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			resources.ApplyResources(btnSeperator2, "btnSeperator2");
			btnSeperator2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSeperator2.Name = "btnSeperator2";
			toolTip.SetToolTip(btnSeperator2, resources.GetString("btnSeperator2.ToolTip"));
			btnSeperator2.UseVisualStyleBackColor = true;
			resources.ApplyResources(lblBalance, "lblBalance");
			lblBalance.Format = null;
			lblBalance.Name = "lblBalance";
			toolTip.SetToolTip(lblBalance, resources.GetString("lblBalance.ToolTip"));
			lblBalance.Value = new decimal(new int[4]);
			resources.ApplyResources(lblPaidAmount, "lblPaidAmount");
			lblPaidAmount.Format = null;
			lblPaidAmount.Name = "lblPaidAmount";
			toolTip.SetToolTip(lblPaidAmount, resources.GetString("lblPaidAmount.ToolTip"));
			lblPaidAmount.Value = new decimal(new int[4]);
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
			toolTip.SetToolTip(btnSearch, resources.GetString("btnSearch.ToolTip"));
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
			resources.ApplyResources(btnPayment, "btnPayment");
			btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
			btnPayment.ForeColor = System.Drawing.Color.White;
			btnPayment.Name = "btnPayment";
			toolTip.SetToolTip(btnPayment, resources.GetString("btnPayment.ToolTip"));
			btnPayment.UseVisualStyleBackColor = false;
			btnPayment.Click += new System.EventHandler(btnPayment_Click);
			resources.ApplyResources(btnMail, "btnMail");
			btnMail.BackColor = System.Drawing.Color.DodgerBlue;
			btnMail.ForeColor = System.Drawing.Color.White;
			btnMail.Name = "btnMail";
			toolTip.SetToolTip(btnMail, resources.GetString("btnMail.ToolTip"));
			btnMail.UseVisualStyleBackColor = false;
			btnMail.Click += new System.EventHandler(btnMail_Click);
			resources.ApplyResources(btnPrint, "btnPrint");
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Name = "btnPrint";
			toolTip.SetToolTip(btnPrint, resources.GetString("btnPrint.ToolTip"));
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Name = "btnDelete";
			toolTip.SetToolTip(btnDelete, resources.GetString("btnDelete.ToolTip"));
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			resources.ApplyResources(btnEdit, "btnEdit");
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Name = "btnEdit";
			toolTip.SetToolTip(btnEdit, resources.GetString("btnEdit.ToolTip"));
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Name = "btnSave";
			toolTip.SetToolTip(btnSave, resources.GetString("btnSave.ToolTip"));
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			resources.ApplyResources(label23, "label23");
			label23.Name = "label23";
			toolTip.SetToolTip(label23, resources.GetString("label23.ToolTip"));
			resources.ApplyResources(label93, "label93");
			label93.Name = "label93";
			toolTip.SetToolTip(label93, resources.GetString("label93.ToolTip"));
			resources.ApplyResources(lblTotalIGST, "lblTotalIGST");
			lblTotalIGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblTotalIGST.ForeColor = System.Drawing.Color.DodgerBlue;
			lblTotalIGST.Format = null;
			lblTotalIGST.Name = "lblTotalIGST";
			toolTip.SetToolTip(lblTotalIGST, resources.GetString("lblTotalIGST.ToolTip"));
			lblTotalIGST.Value = new decimal(new int[4]);
			resources.ApplyResources(lblTotalCGST, "lblTotalCGST");
			lblTotalCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblTotalCGST.ForeColor = System.Drawing.Color.DodgerBlue;
			lblTotalCGST.Format = null;
			lblTotalCGST.Name = "lblTotalCGST";
			toolTip.SetToolTip(lblTotalCGST, resources.GetString("lblTotalCGST.ToolTip"));
			lblTotalCGST.Value = new decimal(new int[4]);
			resources.ApplyResources(lblTotalSGST, "lblTotalSGST");
			lblTotalSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblTotalSGST.ForeColor = System.Drawing.Color.DodgerBlue;
			lblTotalSGST.Format = null;
			lblTotalSGST.Name = "lblTotalSGST";
			toolTip.SetToolTip(lblTotalSGST, resources.GetString("lblTotalSGST.ToolTip"));
			lblTotalSGST.Value = new decimal(new int[4]);
			resources.ApplyResources(lblTaxTotal, "lblTaxTotal");
			lblTaxTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			lblTaxTotal.ForeColor = System.Drawing.Color.Red;
			lblTaxTotal.Format = null;
			lblTaxTotal.Name = "lblTaxTotal";
			toolTip.SetToolTip(lblTaxTotal, resources.GetString("lblTaxTotal.ToolTip"));
			lblTaxTotal.Value = new decimal(new int[4]);
			resources.ApplyResources(label21, "label21");
			label21.Name = "label21";
			toolTip.SetToolTip(label21, resources.GetString("label21.ToolTip"));
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
			resources.ApplyResources(label90, "label90");
			label90.Name = "label90";
			toolTip.SetToolTip(label90, resources.GetString("label90.ToolTip"));
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
			resources.ApplyResources(cbPrint, "cbPrint");
			cbPrint.Name = "cbPrint";
			toolTip.SetToolTip(cbPrint, resources.GetString("cbPrint.ToolTip"));
			cbPrint.UseVisualStyleBackColor = true;
			resources.ApplyResources(lblTaxable, "lblTaxable");
			lblTaxable.ForeColor = System.Drawing.Color.DodgerBlue;
			lblTaxable.Format = null;
			lblTaxable.Name = "lblTaxable";
			toolTip.SetToolTip(lblTaxable, resources.GetString("lblTaxable.ToolTip"));
			lblTaxable.Value = new decimal(new int[4]);
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
			resources.ApplyResources(btnEstimate, "btnEstimate");
			btnEstimate.BackColor = System.Drawing.Color.DodgerBlue;
			btnEstimate.ForeColor = System.Drawing.Color.White;
			btnEstimate.Name = "btnEstimate";
			toolTip.SetToolTip(btnEstimate, resources.GetString("btnEstimate.ToolTip"));
			btnEstimate.UseVisualStyleBackColor = false;
			btnEstimate.Click += new System.EventHandler(btnEstimate_Click);
			resources.ApplyResources(dgDetails, "dgDetails");
			dgDetails.AllowUserToDeleteRows = false;
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
			dgDetails.Columns.AddRange(col_SL, col_ProCode, col_Barcode, col_ProName, col_HSN, col_Qty, col_Unit, col_Rate, col_Gross, col_Dis, col_Discount, col_Taxable, col_Vat, col_VatAmnt, col_GST, col_GSTAmnt, col_SGST, col_SGSTAmnt, col_CGST, col_CGSTAmnt, col_IGST, col_IGSTAmnt, col_NetAmnt);
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
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellContentClick);
			dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgSalesReturn_CellEndEdit);
			dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgSalesReturn_DataError);
			dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgSalesReturn_EditingControlShowing);
			dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgSalesReturn_KeyDown);
			col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SL.DataPropertyName = "SL";
			resources.ApplyResources(col_SL, "col_SL");
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_ProCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_ProCode.DataPropertyName = "ProductCode";
			resources.ApplyResources(col_ProCode, "col_ProCode");
			col_ProCode.Name = "col_ProCode";
			col_Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Barcode.DataPropertyName = "Barcode";
			resources.ApplyResources(col_Barcode, "col_Barcode");
			col_Barcode.Name = "col_Barcode";
			col_ProName.DataPropertyName = "ProductName";
			resources.ApplyResources(col_ProName, "col_ProName");
			col_ProName.Name = "col_ProName";
			col_HSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_HSN.DataPropertyName = "Hsn";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_HSN.DefaultCellStyle = dataGridViewCellStyle5;
			resources.ApplyResources(col_HSN, "col_HSN");
			col_HSN.Name = "col_HSN";
			col_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Qty.DataPropertyName = "Quantity";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Qty.DefaultCellStyle = dataGridViewCellStyle6;
			resources.ApplyResources(col_Qty, "col_Qty");
			col_Qty.Name = "col_Qty";
			col_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Unit.DataPropertyName = "Unit";
			resources.ApplyResources(col_Unit, "col_Unit");
			col_Unit.Name = "col_Unit";
			col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle7;
			resources.ApplyResources(col_Rate, "col_Rate");
			col_Rate.Name = "col_Rate";
			col_Gross.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Gross.DataPropertyName = "GrossAmount";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Gross.DefaultCellStyle = dataGridViewCellStyle8;
			resources.ApplyResources(col_Gross, "col_Gross");
			col_Gross.Name = "col_Gross";
			col_Gross.ReadOnly = true;
			col_Dis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Dis.DataPropertyName = "Discount";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Dis.DefaultCellStyle = dataGridViewCellStyle9;
			resources.ApplyResources(col_Dis, "col_Dis");
			col_Dis.Name = "col_Dis";
			col_Discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Discount.DataPropertyName = "DiscountAmount";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Discount.DefaultCellStyle = dataGridViewCellStyle10;
			resources.ApplyResources(col_Discount, "col_Discount");
			col_Discount.Name = "col_Discount";
			col_Discount.ReadOnly = true;
			col_Taxable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Taxable.DataPropertyName = "Taxable";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Taxable.DefaultCellStyle = dataGridViewCellStyle11;
			resources.ApplyResources(col_Taxable, "col_Taxable");
			col_Taxable.Name = "col_Taxable";
			col_Taxable.ReadOnly = true;
			col_Vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Vat.DataPropertyName = "Vat";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Vat.DefaultCellStyle = dataGridViewCellStyle12;
			resources.ApplyResources(col_Vat, "col_Vat");
			col_Vat.Name = "col_Vat";
			col_VatAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_VatAmnt.DataPropertyName = "VatAmount";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle13;
			resources.ApplyResources(col_VatAmnt, "col_VatAmnt");
			col_VatAmnt.Name = "col_VatAmnt";
			col_VatAmnt.ReadOnly = true;
			col_GST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_GST.DataPropertyName = "Gst";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_GST.DefaultCellStyle = dataGridViewCellStyle14;
			resources.ApplyResources(col_GST, "col_GST");
			col_GST.Name = "col_GST";
			col_GSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_GSTAmnt.DataPropertyName = "GstAmount";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle15;
			resources.ApplyResources(col_GSTAmnt, "col_GSTAmnt");
			col_GSTAmnt.Name = "col_GSTAmnt";
			col_GSTAmnt.ReadOnly = true;
			col_SGST.DataPropertyName = "Sgst";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_SGST.DefaultCellStyle = dataGridViewCellStyle16;
			resources.ApplyResources(col_SGST, "col_SGST");
			col_SGST.Name = "col_SGST";
			col_SGST.ReadOnly = true;
			col_SGSTAmnt.DataPropertyName = "SGstAmount";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle17;
			resources.ApplyResources(col_SGSTAmnt, "col_SGSTAmnt");
			col_SGSTAmnt.Name = "col_SGSTAmnt";
			col_SGSTAmnt.ReadOnly = true;
			col_CGST.DataPropertyName = "CGst";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_CGST.DefaultCellStyle = dataGridViewCellStyle18;
			resources.ApplyResources(col_CGST, "col_CGST");
			col_CGST.Name = "col_CGST";
			col_CGST.ReadOnly = true;
			col_CGSTAmnt.DataPropertyName = "CGstAmount";
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle19;
			resources.ApplyResources(col_CGSTAmnt, "col_CGSTAmnt");
			col_CGSTAmnt.Name = "col_CGSTAmnt";
			col_CGSTAmnt.ReadOnly = true;
			col_IGST.DataPropertyName = "IGst";
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_IGST.DefaultCellStyle = dataGridViewCellStyle20;
			resources.ApplyResources(col_IGST, "col_IGST");
			col_IGST.Name = "col_IGST";
			col_IGST.ReadOnly = true;
			col_IGSTAmnt.DataPropertyName = "IGstAmount";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle21;
			resources.ApplyResources(col_IGSTAmnt, "col_IGSTAmnt");
			col_IGSTAmnt.Name = "col_IGSTAmnt";
			col_IGSTAmnt.ReadOnly = true;
			col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_NetAmnt.DataPropertyName = "NETAmount";
			dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle22;
			resources.ApplyResources(col_NetAmnt, "col_NetAmnt");
			col_NetAmnt.Name = "col_NetAmnt";
			col_NetAmnt.ReadOnly = true;
			resources.ApplyResources(label32, "label32");
			label32.Name = "label32";
			toolTip.SetToolTip(label32, resources.GetString("label32.ToolTip"));
			resources.ApplyResources(btnBalance, "btnBalance");
			btnBalance.Name = "btnBalance";
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
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
			resources.ApplyResources(cmbVehicle, "cmbVehicle");
			cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
			cmbVehicle.FormattingEnabled = true;
			cmbVehicle.LeaveColor = System.Drawing.Color.White;
			cmbVehicle.Name = "cmbVehicle";
			toolTip.SetToolTip(cmbVehicle, resources.GetString("cmbVehicle.ToolTip"));
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
			resources.ApplyResources(btnOrder, "btnOrder");
			btnOrder.BackColor = System.Drawing.Color.DodgerBlue;
			btnOrder.FlatAppearance.BorderSize = 0;
			btnOrder.ForeColor = System.Drawing.Color.White;
			btnOrder.Name = "btnOrder";
			toolTip.SetToolTip(btnOrder, resources.GetString("btnOrder.ToolTip"));
			btnOrder.UseVisualStyleBackColor = false;
			btnOrder.Click += new System.EventHandler(btnOrder_Click);
			resources.ApplyResources(cmbRoute, "cmbRoute");
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
			toolTip.SetToolTip(nudInvoiceNo, resources.GetString("nudInvoiceNo.ToolTip"));
			nudInvoiceNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.ValueChanged += new System.EventHandler(nudInvoiceNo_ValueChanged);
			resources.ApplyResources(label25, "label25");
			label25.Name = "label25";
			toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
			resources.ApplyResources(dtmDate, "dtmDate");
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Name = "dtmDate";
			toolTip.SetToolTip(dtmDate, resources.GetString("dtmDate.ToolTip"));
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
			resources.ApplyResources(chkEnableInvNo, "chkEnableInvNo");
			chkEnableInvNo.Name = "chkEnableInvNo";
			toolTip.SetToolTip(chkEnableInvNo, resources.GetString("chkEnableInvNo.ToolTip"));
			chkEnableInvNo.UseVisualStyleBackColor = true;
			chkEnableInvNo.CheckedChanged += new System.EventHandler(chkEnableInvNo_CheckedChanged);
			resources.ApplyResources(btnDate, "btnDate");
			btnDate.Name = "btnDate";
			toolTip.SetToolTip(btnDate, resources.GetString("btnDate.ToolTip"));
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			resources.ApplyResources(cmbSalesType, "cmbSalesType");
			cmbSalesType.EnterColor = System.Drawing.Color.LightPink;
			cmbSalesType.FormattingEnabled = true;
			cmbSalesType.LeaveColor = System.Drawing.Color.White;
			cmbSalesType.Name = "cmbSalesType";
			toolTip.SetToolTip(cmbSalesType, resources.GetString("cmbSalesType.ToolTip"));
			resources.ApplyResources(label39, "label39");
			label39.Name = "label39";
			toolTip.SetToolTip(label39, resources.GetString("label39.ToolTip"));
			resources.ApplyResources(cbMail, "cbMail");
			cbMail.Name = "cbMail";
			toolTip.SetToolTip(cbMail, resources.GetString("cbMail.ToolTip"));
			cbMail.UseVisualStyleBackColor = true;
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
			resources.ApplyResources(flpButtons, "flpButtons");
			flpButtons.Controls.Add(btnSearch);
			flpButtons.Controls.Add(btnPrint);
			flpButtons.Controls.Add(btnMail);
			flpButtons.Controls.Add(btnClear);
			flpButtons.Controls.Add(btnDelete);
			flpButtons.Controls.Add(btnEdit);
			flpButtons.Controls.Add(btnSave);
			flpButtons.Name = "flpButtons";
			toolTip.SetToolTip(flpButtons, resources.GetString("flpButtons.ToolTip"));
			resources.ApplyResources(btnDiscount, "btnDiscount");
			btnDiscount.Name = "btnDiscount";
			toolTip.SetToolTip(btnDiscount, resources.GetString("btnDiscount.ToolTip"));
			btnDiscount.UseVisualStyleBackColor = true;
			btnDiscount.Click += new System.EventHandler(btnDiscount_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnCustomerSearch);
			base.Controls.Add(btnDiscount);
			base.Controls.Add(flpButtons);
			base.Controls.Add(lblRunBal);
			base.Controls.Add(cmbSalesType);
			base.Controls.Add(label39);
			base.Controls.Add(button1);
			base.Controls.Add(txtPrintCopy);
			base.Controls.Add(btnSeperator2);
			base.Controls.Add(lblBalance);
			base.Controls.Add(lblPaidAmount);
			base.Controls.Add(label16);
			base.Controls.Add(label18);
			base.Controls.Add(btnPayment);
			base.Controls.Add(label23);
			base.Controls.Add(label93);
			base.Controls.Add(lblTotalIGST);
			base.Controls.Add(lblTotalCGST);
			base.Controls.Add(lblTotalSGST);
			base.Controls.Add(lblTaxTotal);
			base.Controls.Add(label21);
			base.Controls.Add(lblGrandTotal);
			base.Controls.Add(label36);
			base.Controls.Add(txtAddDiscAmnt);
			base.Controls.Add(txtAddDisc);
			base.Controls.Add(label15);
			base.Controls.Add(label90);
			base.Controls.Add(label35);
			base.Controls.Add(txtFreight);
			base.Controls.Add(txtAddAmnt);
			base.Controls.Add(lblNetTotal);
			base.Controls.Add(label34);
			base.Controls.Add(label33);
			base.Controls.Add(cbMail);
			base.Controls.Add(cbPrint);
			base.Controls.Add(lblTaxable);
			base.Controls.Add(lblDisTotal);
			base.Controls.Add(lblGrsTotal);
			base.Controls.Add(lblTotalQty);
			base.Controls.Add(lblUntQty);
			base.Controls.Add(lblLastSalesRate);
			base.Controls.Add(lblCStock);
			base.Controls.Add(btnEstimate);
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
			base.Name = "SalesReturnView";
			toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			base.Load += new System.EventHandler(SalesReturnView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SalesReturnView_KeyDown);
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
			base.Controls.SetChildIndex(btnEstimate, 0);
			base.Controls.SetChildIndex(lblCStock, 0);
			base.Controls.SetChildIndex(lblLastSalesRate, 0);
			base.Controls.SetChildIndex(lblUntQty, 0);
			base.Controls.SetChildIndex(lblTotalQty, 0);
			base.Controls.SetChildIndex(lblGrsTotal, 0);
			base.Controls.SetChildIndex(lblDisTotal, 0);
			base.Controls.SetChildIndex(lblTaxable, 0);
			base.Controls.SetChildIndex(cbPrint, 0);
			base.Controls.SetChildIndex(cbMail, 0);
			base.Controls.SetChildIndex(label33, 0);
			base.Controls.SetChildIndex(label34, 0);
			base.Controls.SetChildIndex(lblNetTotal, 0);
			base.Controls.SetChildIndex(txtAddAmnt, 0);
			base.Controls.SetChildIndex(txtFreight, 0);
			base.Controls.SetChildIndex(label35, 0);
			base.Controls.SetChildIndex(label90, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(txtAddDisc, 0);
			base.Controls.SetChildIndex(txtAddDiscAmnt, 0);
			base.Controls.SetChildIndex(label36, 0);
			base.Controls.SetChildIndex(lblGrandTotal, 0);
			base.Controls.SetChildIndex(label21, 0);
			base.Controls.SetChildIndex(lblTaxTotal, 0);
			base.Controls.SetChildIndex(lblTotalSGST, 0);
			base.Controls.SetChildIndex(lblTotalCGST, 0);
			base.Controls.SetChildIndex(lblTotalIGST, 0);
			base.Controls.SetChildIndex(label93, 0);
			base.Controls.SetChildIndex(label23, 0);
			base.Controls.SetChildIndex(btnPayment, 0);
			base.Controls.SetChildIndex(label18, 0);
			base.Controls.SetChildIndex(label16, 0);
			base.Controls.SetChildIndex(lblPaidAmount, 0);
			base.Controls.SetChildIndex(lblBalance, 0);
			base.Controls.SetChildIndex(btnSeperator2, 0);
			base.Controls.SetChildIndex(txtPrintCopy, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(label39, 0);
			base.Controls.SetChildIndex(cmbSalesType, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblRunBal, 0);
			base.Controls.SetChildIndex(flpButtons, 0);
			base.Controls.SetChildIndex(btnDiscount, 0);
			base.Controls.SetChildIndex(btnCustomerSearch, 0);
			((System.ComponentModel.ISupportInitialize)bdsSalesReturn).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).EndInit();
			flpButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
