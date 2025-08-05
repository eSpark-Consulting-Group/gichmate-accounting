using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
	public class PurchaseReturnView : BaseForm
	{
		private DateTime estimateDate;

		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private TransactionMaster entTMaster = new TransactionMaster();

		private List<Transaction> entTransactions = new List<Transaction>();

		private List<SettingsEntity> entBooleanSettings;

		private int iCashAcctId;

		private int iPurRtnInvId;

		private int? iMasterId;

		private DataGridViewTextBoxEditingControl textbox;

		private string sKeyChar = "";

		private PurchaseInvoiceHdr entPurHdr;

		private List<PurchaseInvoiceDtl> entPurDtls = new List<PurchaseInvoiceDtl>();

		private PurRtnInvMaster entPurRtn;

		private List<PurRtnInvSttings> entPurRtnDtls = new List<PurRtnInvSttings>();

		private AccountEntity account;

		private DBContext db;

		private string sVoucherType = "Purchase Return Voucher";

		private string sBranchDetails = string.Empty;

		private List<ComboFill> comboFills = new List<ComboFill>();

		private IContainer components = null;

		private Label lblTitle;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private CheckBox chkEnableInvNo;

		private Label label2;

		private Button btnDate;

		private AshUpDown nudInvoiceNo;

		private AshComboBox cmbPriCategory;

		private Label label13;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private AshComboBox cmbPartyAcct;

		private Label label6;

		private AshComboBox cmbPurAcct;

		private Label label3;

		private Label label32;

		private Button btnLedgerReport;

		private Button btnPayment;

		private Button btnInvoice;

		public AshDataGridview dgDetails;

		private AshNumericLabel lblNetTotal;

		private AshNumericLabel lblTaxable;

		private AshNumericLabel lblDisTotal;

		private AshNumericLabel lblGrsTotal;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblLastRate;

		private AshNumericLabel lblUnitQty;

		private AshNumericLabel lblCStock;

		private Button btnSeperator2;

		private AshNumericLabel lblGrandTotal;

		private Label label36;

		private AshTextBoxNormal txtAddDiscAmnt;

		private AshTextBoxNormal txtAddDisc;

		private Label label22;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private Label label34;

		private AshTextBoxNormal txtAddAmnt;

		private Label label33;

		private AshTextBoxNormal txtPrintCopy;

		private CheckBox cbPrint;

		private Label label15;

		private Label label93;

		private Label label90;

		private AshNumericLabel lblTotalIGST;

		private AshNumericLabel lblTotalCGST;

		private AshNumericLabel lblTotalSGST;

		private AshNumericLabel lblTaxTotal;

		private Label label19;

		private Button button6;

		private AshNumericLabel lblBalance;

		private AshNumericLabel lblPaidAmount;

		private Label label16;

		private Label label18;

		private Button btnSearch;

		private Button btnClear;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private AshComboBox cmbPurType;

		private Label label11;

		private BindingSource bdsDetails;

		private CheckBox cbMail;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProCode;

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

		private Button btnCustomerSearch;

		public int? iPurInvoiceNo { get; set; }

		public int? iPartyAccount { get; set; }

		public decimal ReturnAmount { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public PurchaseReturnView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			col_Qty.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Dis.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Vat.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_VatAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			if (Utility.uManufacturer != "Ashpro")
			{
				btnDelete.Visible = false;
			}
		}

		private void LoadComboMethod()
		{
			AniHelper.FillAccountForPurchase(cmbPartyAcct);
			AniHelper.FillPurchaseRtnAccount(cmbPurAcct);
			AniHelper.FillPCategory(cmbPriCategory);
			AniHelper.FillFormType(cmbPurType);
		}

		private void LoadSettingsMethod()
		{
			DataGridViewTextBoxColumn dataGridViewTextBoxColumn = col_Dis;
			bool visible = (col_Discount.Visible = CommonSettings.blnShowDiscountInInvoice);
			dataGridViewTextBoxColumn.Visible = visible;
			entBooleanSettings = new List<SettingsEntity>();
			entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == "Purchase Return" && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
			chkEnableInvNo.Checked = CommonSettings.blnEditInvoiceNumber;
			cbPrint.Checked = CommonSettings.blnPntInvWlSaving;
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
				label15.Visible = true;
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
				cmbPriCategory.SelectedValue = db.Accounts(new
				{
					AccountId = cmbPartyAcct.SelectedValue.ToInt32()
				}).FirstOrDefault().PCategoryId ?? 1;
				lblRunBal.Text = AniHelper.AccountBalanceFindMethod(cmbPartyAcct.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)));
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void PurchaseInvoiceLoadMethod(int iInvId)
		{
			entPurHdr = AshproCommon.GetObjectDetails<PurchaseInvoiceHdr>("Select * From PurInvMaster Where PurInvId='" + iInvId + "'");
			entPurDtls = AshproCommon.GetListMethod<PurchaseInvoiceDtl>("Select * From PurInvSettings where PurInvId='" + iInvId + "'");
			entTransactions = POSHelper.GetTransactionsFromPDetails(entPurDtls, db);
			dtmDate.Value = entPurHdr.PurDate;
			nudInvoiceNo.Value = entPurHdr.PurInvNo.toInt32();
			cmbPartyAcct.SelectedValue = entPurHdr.PartyAccId.GetValueOrDefault();
			cmbPurAcct.SelectedValue = entPurHdr.PurActId.GetValueOrDefault();
			cmbPriCategory.SelectedValue = entPurHdr.PriceCategoryId.GetValueOrDefault();
			txtRemarks.Text = entPurHdr.Remarks;
			txtAddAmnt.Text = entPurHdr.AdditionalAmnt.ToString(CommonSettings.strCurencyFormat);
			txtAddDiscAmnt.Text = entPurHdr.BillDisc.ToString(CommonSettings.strCurencyFormat);
			txtFreight.Text = entPurHdr.Frieght.ToString(CommonSettings.strCurencyFormat);
			decimal billDisc = entPurHdr.BillDisc;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			if (billDisc > 0m && num > 0m)
			{
				txtAddDisc.Text = (billDisc / num * 100m).ToString(CommonSettings.strCurencyFormat);
			}
			BindDgv();
			TotalFindMethod();
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
				GodwnId = db.Godowns().FirstOrDefault().GodwnId.toInt32(),
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

		private void LoadPurchaseReturnFromID()
		{
			iMasterId = entPurRtn.PurRtnInvId.ToInt32();
			entPurRtnDtls = db.PurRtnInvSttings(new
			{
				PurRtnInvId = iMasterId
			}).ToList();
			entTransactions = POSHelper.GetTransactionsFromPRDetails(entPurRtnDtls, db);
			dtmDate.Value = entPurRtn.PurRtnDate;
			nudInvoiceNo.Value = entPurRtn.PurRtnInvNo.toInt32();
			cmbPartyAcct.SelectedValue = entPurRtn.PartyAcId.GetValueOrDefault();
			cmbPurAcct.SelectedValue = entPurRtn.PurRtnAcId.GetValueOrDefault();
			cmbPriCategory.SelectedValue = entPurRtn.PriceCategoryId.GetValueOrDefault();
			txtRemarks.Text = entPurRtn.Remarks;
			txtAddAmnt.Text = entPurRtn.AdditionalAmnt.ToString(CommonSettings.strCurencyFormat);
			txtAddDiscAmnt.Text = entPurRtn.BillDisc.ToString(CommonSettings.strCurencyFormat);
			txtFreight.Text = entPurRtn.Frieght.ToString(CommonSettings.strCurencyFormat);
			decimal billDisc = entPurRtn.BillDisc;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			if (billDisc > 0m && num > 0m)
			{
				txtAddDisc.Text = (billDisc / num * 100m).ToString(CommonSettings.strCurencyFormat);
			}
			BindDgv();
			TotalFindMethod();
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			flpButtons.Enabled = true;
			btnSave.Enabled = false;
			btnEdit.Enabled = true;
			btnEnd.Enabled = true;
			btnPrint.Enabled = true;
			btnClear.Enabled = true;
			nudInvoiceNo.Enabled = true;
			chkEnableInvNo.Enabled = true;
			txtPrintCopy.Enabled = true;
			btnDelete.Enabled = true;
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
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entTransactions;
			dgDetails.DataSource = bdsDetails;
		}

		private void PrintMethod()
		{
			try
			{
				entDMaster = db.DotmatrixMasters(new
				{
					VochrType = "Purchase Return",
					isDefault = true
				}).FirstOrDefault();
				if (entDMaster != null)
				{
					entDSettings = new List<DotmatrixSettings>();
					entDSettings = db.DotmatrixSettings(new { entDMaster.printMasterId }).ToList();
					Company company = new Company();
					company = db.Companies().FirstOrDefault();
					entcompany = company.CopyTo<ExCompany>();
					entcompany.CompanyName = company.InstituteName;
					entPartyDtl = new PartyDetails();
					account = db.Accounts(new
					{
						AccountId = cmbPartyAcct.SelectedValue.ToInt32()
					}).FirstOrDefault();
					entPartyDtl = POSHelper.GetParty(account);
					iPurRtnInvId = AshproCommon.IdFindMethod("Select PurRtnInvId From PurRtnInvMaster Where PurRtnInvNo='" + nudInvoiceNo.Value + "'" + sBranchDetails);
					entTMaster = new TransactionMaster();
					entTransactions = new List<Transaction>();
					entPurRtn = db.PurRtnInvMasters(new
					{
						PurRtnInvId = iPurRtnInvId
					}).FirstOrDefault();
					entPurRtnDtls = db.PurRtnInvSttings(new
					{
						PurRtnInvId = iPurRtnInvId
					}).ToList();
					entTMaster = POSHelper.GetTMaster(entPurRtn);
					entTransactions = POSHelper.GetTransactionsFromPRDetails(entPurRtnDtls, db);
					PrintHelper printHelper = new PrintHelper();
					printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, null, entTMaster, isPrint: true, txtPrintCopy.Data);
					printHelper = null;
				}
				else
				{
					Messages.ErrorMessage("There is no default print design. Please make your own design");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
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
			obj[6] = "' ";
			obj[7] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[8] { "Delete From StockTranLog Where VchrId='", null, null, null, null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType ='";
			obj2[3] = sVoucherType;
			obj2[4] = "' and Narration='";
			obj2[5] = sVoucherType;
			obj2[6] = "' ";
			obj2[7] = sBranchDetails;
			AshproCommon.DeleteMethod(string.Concat(obj2));
			DataTable dataTable = new DataTable();
			string[] obj3 = new string[6] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null };
			num = iMasterId;
			obj3[1] = num.ToString();
			obj3[2] = "' and VchrType='";
			obj3[3] = sVoucherType;
			obj3[4] = "' ";
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
			obj4[4] = "' and VchrType='Journal Voucher' and VchrName='TAX PAYABLE' ";
			obj4[5] = sBranchDetails;
			dataTable = AshproCommon.GetDataTable(string.Concat(obj4));
			foreach (DataRow row2 in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row2[0].ToString().ToInt32());
			}
			AshproCommon.DeleteFromDatabase("PurRtnInvSttings", "PurRtnInvId", iMasterId.ToInt32());
		}

		private void GenerateEntityMethod(PurRtnInvMaster puRtnHdr, int iInvNo)
		{
			puRtnHdr.PurRtnInvId = iMasterId;
			puRtnHdr.PurRtnInvNo = iInvNo;
			puRtnHdr.PurRtnDate = dtmDate.Value.Date;
			puRtnHdr.PurRtnAcId = cmbPurAcct.SelectedValue.ToInt32();
			puRtnHdr.PartyAcId = cmbPartyAcct.SelectedValue.ToInt32();
			puRtnHdr.PriceCategoryId = cmbPriCategory.SelectedValue.ToInt32();
			puRtnHdr.AdditionalAmnt = txtAddAmnt.Value;
			puRtnHdr.Frieght = txtFreight.Value;
			puRtnHdr.TotalDiscount = lblDisTotal.Text.ToDecimal();
			puRtnHdr.BillDisc = txtAddDiscAmnt.Value;
			puRtnHdr.TotalTax = lblTaxTotal.Value;
			puRtnHdr.TotalAmnt = lblGrandTotal.Value;
			puRtnHdr.JEId = Settings.Default.intLogOnId;
			puRtnHdr.EntryDate = DateTime.Now.Date;
			puRtnHdr.SGSTAmount = lblTotalSGST.Value;
			puRtnHdr.CGSTAmount = lblTotalCGST.Value;
			puRtnHdr.IGSTAmount = lblTotalIGST.Value;
			puRtnHdr.Remarks = txtRemarks.Text;
			puRtnHdr.Taxable = lblTaxable.Text.ToDecimal();
			puRtnHdr.FK_BranchId = Settings.Default.iBranchId;
			puRtnHdr.Terminal = Utility.uTerminal;
			puRtnHdr.SequenceNo = Utility.uSerialNumber;
		}

		private void SaveSubMethod()
		{
			if (AshproCommon.InsertToDatabase(entPurRtnDtls.Cast<object>().ToList(), "PurRtnInvSttings"))
			{
				POSHelper.TranLogMethod(0m, entPurRtn.TotalAmnt, cmbPurAcct.SelectedValue.ToInt32(), cmbPartyAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurRtn.PurRtnInvNo, sVoucherType, Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(entPurRtn.TotalAmnt, 0m, cmbPartyAcct.SelectedValue.ToInt32(), cmbPurAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurRtn.PurRtnInvNo, sVoucherType, Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				if (CommonSettings.blnTaxEnabled)
				{
					int num = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX PAYABLE'", isVoucherNo: false);
					POSHelper.TranLogMethod(entPurRtn.TotalTax, 0m, cmbPurAcct.SelectedValue.ToInt32(), num, iMasterId.ToInt32(), entPurRtn.PurRtnInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, entPurRtn.TotalTax, num, cmbPurAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurRtn.PurRtnInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX PAYABLE", AniHelper.GetDate(dtmDate.Value));
				}
				List<POSDetails> pOSDetailsFrmTrans = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
				POSHelper.SaveStockTranLoagMethod(pOSDetailsFrmTrans, iMasterId.ToInt32(), entPurRtn.PurRtnInvNo.toInt32(), sVoucherType, "Purchase Return", cmbPartyAcct.SelectedValue.ToInt32(), sVoucherType, Settings.Default.intLogOnId, null, isIn: false, AniHelper.GetDate(dtmDate.Value));
				decimal value = lblPaidAmount.Value;
				if (value > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Receipt Voucher' and VchrName='Cash Receipt'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num2, dtmDate.Value.Date, value, "Payment Voucher", "Cash Received On Purchase Return", iCashAcctId, cmbPartyAcct.SelectedValue.ToInt32(), Settings.Default.intLogOnId, Settings.Default.intLogOnId, "Cash Receipt");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num2 + "' and VchrType ='Receipt Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(value, 0m, iCashAcctId, cmbPartyAcct.SelectedValue.ToInt32(), iVoucherID, num2, "Receipt Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(0m, value, cmbPartyAcct.SelectedValue.ToInt32(), iCashAcctId, iVoucherID, num2, "Receipt Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
			}
		}

		private void FormClearMethod()
		{
			entTransactions = new List<Transaction>();
			BindDgv();
			cmbPartyAcct.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
			cmbPriCategory.SelectedIndex = 0;
		}

		private void btnClearMethod()
		{
			AniHelper.ClearMethod(this);
			FormClearMethod();
			iMasterId = null;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			btnSearch.Enabled = true;
			cmbPartyAcct.Focus();
		}

		private async void PurchaseReturnView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
			estimateDate = Settings.Default.appDate;
			dtmDate.Value = estimateDate;
			LoadComboMethod();
			LoadSettingsMethod();
			entPurRtnDtls = new List<PurRtnInvSttings>();
			entTransactions = new List<Transaction>();
			BindDgv();
			if (iPurInvoiceNo.HasValue)
			{
				nudInvoiceNo.Value = iPurInvoiceNo.ToInt32();
			}
			else
			{
				nudInvoiceNo.Value = (db.PurRtnInvMasters(new
				{
					FK_BranchId = Settings.Default.iBranchId
				}).Max((PurRtnInvMaster x) => x.PurRtnInvNo) + 1) ?? 1;
			}
			if (CommonSettings.blnEnableMultiBranch)
			{
				sBranchDetails = "and FK_BranchId = '" + Settings.Default.iBranchId + "'";
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
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberPR").FirstOrDefault();
			settingsEntity.BitValue = chkEnableInvNo.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void btnLedgerReport_Click(object sender, EventArgs e)
		{
			LedgerReport ledgerReport = new LedgerReport();
			ledgerReport.MdiParent = base.MdiParent;
			ledgerReport.ValueFromDate = AniHelper.GetFYearFromDate().ToString();
			ledgerReport.ValueToDate = dtmDate.Text;
			ledgerReport.ValueLedger = cmbPartyAcct.Text;
			ledgerReport.Show();
		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			int num = AshproCommon.IdFindMethod("Select AGroupId From Account Where AccountId = '" + cmbPartyAcct.SelectedValue?.ToString() + "'", isVoucherNo: false);
			int num2 = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName ='CASH IN HAND'", isVoucherNo: false);
			if (num == num2)
			{
				Messages.ErrorMessage("You don't want to make payment in the case of Cash Account");
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			decimal num5 = (pOSCashPayment.lblInvoiceAmount.Value = (pOSCashPayment.lblBalanceAmount.Value = entTransactions.Sum((Transaction x) => x.NETAmount)));
			pOSCashPayment.iPartyAccountId = cmbPartyAcct.SelectedValue.ToInt32();
			if (pOSCashPayment.ShowDialog() == DialogResult.OK)
			{
				lblPaidAmount.Text = (pOSCashPayment.txtCashPaid.Value + pOSCashPayment.txtCardPaid.Value).ToString(CommonSettings.strCurencyFormat);
				lblBalance.Text = pOSCashPayment.lblBalanceAmount.Value.ToString(CommonSettings.strCurencyFormat);
				iCashAcctId = pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32();
			}
		}

		private void cmbPartyAcct_SelectedIndexChanged(object sender, EventArgs e)
		{
			NameAndBalanceFindMethod();
		}

		private void btnInvoice_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = cmbPurType.Text;
				formSearch.sInvType = "Purchase Invoice";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					PurchaseInvoiceLoadMethod(formSearch.iInvoiceId);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			FormClearMethod();
			entPurRtn = db.PurRtnInvMasters(new
			{
				PurRtnInvNo = nudInvoiceNo.Value.ToInt32(),
				FK_BranchId = Settings.Default.iBranchId
			}).FirstOrDefault();
			if (entPurRtn != null)
			{
				LoadPurchaseReturnFromID();
			}
			else
			{
				btnClearMethod();
			}
		}

		private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Transaction current = getCurrent();
			int rowIndex = dgDetails.CurrentCell.RowIndex;
			if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
				productBarcodeEntity = db.ProductBarcodes(new { current.Barcode }).FirstOrDefault();
				POSHelper.GetPurchaseTransactionsFromBarcode(current, productBarcodeEntity, cmbPriCategory.SelectedValue.ToInt32(), rowIndex, cmbPurType.Text);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				POSHelper.GetPurchaseTransactionsAfterEdit(current);
				if (current.Quantity > lblCStock.Value && CommonSettings.blnShowMsgWlNStockPR)
				{
					Messages.InformationMessage("The quantity you entered is more than the current stock value!");
				}
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Rate.Name)
			{
				POSHelper.GetPurchaseTransactionsAfterEdit(current);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Dis.Name)
			{
				POSHelper.GetPurchaseTransactionsAfterEdit(current);
			}
			TotalFindMethod();
		}

		private void dgDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += textbox_KeyPress;
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
						ShowItemList showItemList = new ShowItemList(isPurchase: true);
						showItemList.ProName = textbox.Text;
						showItemList.productList = comboFills;
						showItemList.ProPriceCatId = cmbPriCategory.SelectedValue.ToInt32();
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
							current.Rate = proSearch.PPrice;
							current.Discount = proSearch.PPDisc;
							StockFindMethod(current);
							lblLastRate.Value = AshproCommon.ValueFindMethod("Select MAX(ProRate) as LastPrice From PurInvSettings Where PartyAccId  ='" + cmbPartyAcct.SelectedValue.ToInt32() + "' and ProId  ='" + proSearch.ProductId + "'");
							POSHelper.GetPurchaseTransaction(current, rowIndex, cmbPurType.Text);
							dgDetails.CurrentCell = dgDetails.Rows[rowIndex].Cells[col_Qty.Name];
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
						POSHelper.SellingPriceAndDiscFindMethod(cmbPriCategory.SelectedValue.ToInt32(), "Purchase Invoice", productUnit.Barcode);
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

		private void textbox_KeyPress(object sender, KeyPressEventArgs e)
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

		private void dgDetails_KeyDown(object sender, KeyEventArgs e)
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
				productTracing.TypeVoucher = "Purchase Return";
				productTracing.PartyAccntId = cmbPartyAcct.SelectedValue.ToInt32();
				productTracing.ProductId = current.ProductId.ToInt32();
				productTracing.ShowDialog();
			}
			else if (e.KeyCode == Keys.F8)
			{
				AniHelper.ItemCreationMethod(current.ProductId.toInt32());
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

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = cmbPurType.Text;
				formSearch.sInvType = "Purchase Return";
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					iMasterId = formSearch.iInvoiceId;
					LoadPurchaseReturnFromID();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudInvoiceNo.Value = (db.PurRtnInvMasters(new
			{
				FK_BranchId = Settings.Default.iBranchId
			}).Max((PurRtnInvMaster x) => x.PurRtnInvNo) + 1) ?? 1;
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
					AshproCommon.DeleteFromDatabase("PurRtnInvMaster", "PurRtnInvId", iMasterId.ToInt32());
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
			btnSearch.Enabled = false;
			btnSave.Enabled = true;
			dgDetails.Focus();
			dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[col_ProName.Name];
		}

		private void cbMail_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingPR").FirstOrDefault();
			settingsEntity.BitValue = cbMail.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingPR").FirstOrDefault();
			settingsEntity.BitValue = cbPrint.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (entTransactions.Count == 0)
				{
					Messages.WarningMessage("No records to save!");
					return;
				}
				entPurRtnDtls = new List<PurRtnInvSttings>();
				entPurRtnDtls = POSHelper.GetPRDetailsFrmTransaction(db, entTransactions, cmbPartyAcct.SelectedValue.ToInt32(), dtmDate.Value.Date);
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					int num = 0;
					num = (db.PurRtnInvMasters(new
					{
						FK_BranchId = Settings.Default.iBranchId
					}).Max((PurRtnInvMaster x) => x.PurRtnInvNo) + 1) ?? 1;
					entPurRtn = new PurRtnInvMaster();
					GenerateEntityMethod(entPurRtn, num);
					entPurRtn.PurRtnInvId = null;
					if (AshproCommon.InsertToDatabaseObj(entPurRtn, "PurRtnInvMaster"))
					{
						entPurRtn = db.PurRtnInvMasters(new
						{
							PurRtnInvNo = num,
							FK_BranchId = Settings.Default.iBranchId
						}).FirstOrDefault();
						if (entPurRtn != null)
						{
							iMasterId = entPurRtn.PurRtnInvId;
							foreach (PurRtnInvSttings entPurRtnDtl in entPurRtnDtls)
							{
								entPurRtnDtl.PurRtnInvId = iMasterId.ToInt32();
							}
							SaveSubMethod();
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
					foreach (PurRtnInvSttings entPurRtnDtl2 in entPurRtnDtls)
					{
						entPurRtnDtl2.PurRtnInvId = iMasterId.ToInt32();
					}
					DeleteSubMethod();
					GenerateEntityMethod(entPurRtn, nudInvoiceNo.Value.ToInt32());
					if (AshproCommon.UpdateToDatabaseObj(entPurRtn, "PurRtnInvMaster", "PurRtnInvId", iMasterId.ToInt32()))
					{
						SaveSubMethod();
					}
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PurchaseReturnView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnEdit_Click(null, null);
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnMail_Click(null, null);
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint_Click(null, null);
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSearch_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnLedgerReport_Click(null, null);
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnInvoice_Click(null, null);
			}
			else if (e.KeyCode == Keys.Y && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPayment_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
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
				lblCStock.Value = AshproCommon.ValueFindMethod("Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog Where ProductId='" + current.ProductId + "'and VchrDate <='" + AniHelper.GetDate(dtmDate.Value) + "' " + sBranchDetails);
				lblLastRate.Value = AshproCommon.ValueFindMethod("Select MAX(ProRate) as LastPrice From PurInvSettings Where PartyAccId  ='" + cmbPartyAcct.SelectedValue.ToInt32() + "' and ProId  ='" + current.ProductId + "'");
			}
		}

		private void cmbPartyAcct_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				dgDetails.Focus();
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private void btnCustomerSearch_Click(object sender, EventArgs e)
		{
			AniHelper.CustomerSerchMethod(cmbPartyAcct);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.PurchaseReturnView));
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
			lblTitle = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			chkEnableInvNo = new System.Windows.Forms.CheckBox();
			label2 = new System.Windows.Forms.Label();
			btnDate = new System.Windows.Forms.Button();
			nudInvoiceNo = new AshUsercontrol.AshUpDown();
			cmbPriCategory = new AshUsercontrol.AshComboBox();
			label13 = new System.Windows.Forms.Label();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			cmbPartyAcct = new AshUsercontrol.AshComboBox();
			label6 = new System.Windows.Forms.Label();
			cmbPurAcct = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			label32 = new System.Windows.Forms.Label();
			btnLedgerReport = new System.Windows.Forms.Button();
			btnPayment = new System.Windows.Forms.Button();
			btnInvoice = new System.Windows.Forms.Button();
			dgDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			lblNetTotal = new AshUsercontrol.AshNumericLabel();
			lblTaxable = new AshUsercontrol.AshNumericLabel();
			lblDisTotal = new AshUsercontrol.AshNumericLabel();
			lblGrsTotal = new AshUsercontrol.AshNumericLabel();
			lblTotalQty = new AshUsercontrol.AshNumericLabel();
			lblLastRate = new AshUsercontrol.AshNumericLabel();
			lblUnitQty = new AshUsercontrol.AshNumericLabel();
			lblCStock = new AshUsercontrol.AshNumericLabel();
			btnSeperator2 = new System.Windows.Forms.Button();
			lblGrandTotal = new AshUsercontrol.AshNumericLabel();
			label36 = new System.Windows.Forms.Label();
			txtAddDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
			txtAddDisc = new AshUsercontrol.AshTextBoxNormal();
			label22 = new System.Windows.Forms.Label();
			label35 = new System.Windows.Forms.Label();
			txtFreight = new AshUsercontrol.AshTextBoxNormal();
			label34 = new System.Windows.Forms.Label();
			txtAddAmnt = new AshUsercontrol.AshTextBoxNormal();
			label33 = new System.Windows.Forms.Label();
			txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
			cbPrint = new System.Windows.Forms.CheckBox();
			label15 = new System.Windows.Forms.Label();
			label93 = new System.Windows.Forms.Label();
			label90 = new System.Windows.Forms.Label();
			lblTotalIGST = new AshUsercontrol.AshNumericLabel();
			lblTotalCGST = new AshUsercontrol.AshNumericLabel();
			lblTotalSGST = new AshUsercontrol.AshNumericLabel();
			lblTaxTotal = new AshUsercontrol.AshNumericLabel();
			label19 = new System.Windows.Forms.Label();
			button6 = new System.Windows.Forms.Button();
			lblBalance = new AshUsercontrol.AshNumericLabel();
			lblPaidAmount = new AshUsercontrol.AshNumericLabel();
			label16 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			btnSearch = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnMail = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			cmbPurType = new AshUsercontrol.AshComboBox();
			label11 = new System.Windows.Forms.Label();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			cbMail = new System.Windows.Forms.CheckBox();
			lblRunBal = new System.Windows.Forms.Label();
			toolTip = new System.Windows.Forms.ToolTip(components);
			btnCustomerSearch = new System.Windows.Forms.Button();
			flpButtons = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			flpButtons.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			toolTip.SetToolTip(btnEnd, resources.GetString("btnEnd.ToolTip"));
			resources.ApplyResources(lblTitle, "lblTitle");
			lblTitle.Name = "lblTitle";
			toolTip.SetToolTip(lblTitle, resources.GetString("lblTitle.ToolTip"));
			resources.ApplyResources(dtmDate, "dtmDate");
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Name = "dtmDate";
			dtmDate.TabStop = false;
			toolTip.SetToolTip(dtmDate, resources.GetString("dtmDate.ToolTip"));
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
			resources.ApplyResources(chkEnableInvNo, "chkEnableInvNo");
			chkEnableInvNo.Name = "chkEnableInvNo";
			chkEnableInvNo.TabStop = false;
			toolTip.SetToolTip(chkEnableInvNo, resources.GetString("chkEnableInvNo.ToolTip"));
			chkEnableInvNo.UseVisualStyleBackColor = true;
			chkEnableInvNo.CheckedChanged += new System.EventHandler(chkEnableInvNo_CheckedChanged);
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
			resources.ApplyResources(btnDate, "btnDate");
			btnDate.Name = "btnDate";
			btnDate.TabStop = false;
			toolTip.SetToolTip(btnDate, resources.GetString("btnDate.ToolTip"));
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			resources.ApplyResources(nudInvoiceNo, "nudInvoiceNo");
			nudInvoiceNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudInvoiceNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.Name = "nudInvoiceNo";
			nudInvoiceNo.TabStop = false;
			toolTip.SetToolTip(nudInvoiceNo, resources.GetString("nudInvoiceNo.ToolTip"));
			nudInvoiceNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvoiceNo.ValueChanged += new System.EventHandler(nudInvoiceNo_ValueChanged);
			resources.ApplyResources(cmbPriCategory, "cmbPriCategory");
			cmbPriCategory.EnterColor = System.Drawing.Color.LightPink;
			cmbPriCategory.FormattingEnabled = true;
			cmbPriCategory.LeaveColor = System.Drawing.Color.White;
			cmbPriCategory.Name = "cmbPriCategory";
			toolTip.SetToolTip(cmbPriCategory, resources.GetString("cmbPriCategory.ToolTip"));
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
			resources.ApplyResources(cmbPartyAcct, "cmbPartyAcct");
			cmbPartyAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPartyAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPartyAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbPartyAcct.FormattingEnabled = true;
			cmbPartyAcct.LeaveColor = System.Drawing.Color.White;
			cmbPartyAcct.Name = "cmbPartyAcct";
			toolTip.SetToolTip(cmbPartyAcct, resources.GetString("cmbPartyAcct.ToolTip"));
			cmbPartyAcct.SelectedIndexChanged += new System.EventHandler(cmbPartyAcct_SelectedIndexChanged);
			cmbPartyAcct.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbPartyAcct_KeyDown);
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
			resources.ApplyResources(cmbPurAcct, "cmbPurAcct");
			cmbPurAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbPurAcct.FormattingEnabled = true;
			cmbPurAcct.LeaveColor = System.Drawing.Color.White;
			cmbPurAcct.Name = "cmbPurAcct";
			toolTip.SetToolTip(cmbPurAcct, resources.GetString("cmbPurAcct.ToolTip"));
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
			resources.ApplyResources(label32, "label32");
			label32.Name = "label32";
			toolTip.SetToolTip(label32, resources.GetString("label32.ToolTip"));
			resources.ApplyResources(btnLedgerReport, "btnLedgerReport");
			btnLedgerReport.Name = "btnLedgerReport";
			btnLedgerReport.TabStop = false;
			toolTip.SetToolTip(btnLedgerReport, resources.GetString("btnLedgerReport.ToolTip"));
			btnLedgerReport.UseVisualStyleBackColor = true;
			btnLedgerReport.Click += new System.EventHandler(btnLedgerReport_Click);
			resources.ApplyResources(btnPayment, "btnPayment");
			btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
			btnPayment.ForeColor = System.Drawing.Color.White;
			btnPayment.Name = "btnPayment";
			btnPayment.TabStop = false;
			toolTip.SetToolTip(btnPayment, resources.GetString("btnPayment.ToolTip"));
			btnPayment.UseVisualStyleBackColor = false;
			btnPayment.Click += new System.EventHandler(btnPayment_Click);
			resources.ApplyResources(btnInvoice, "btnInvoice");
			btnInvoice.BackColor = System.Drawing.Color.DodgerBlue;
			btnInvoice.FlatAppearance.BorderSize = 0;
			btnInvoice.ForeColor = System.Drawing.Color.White;
			btnInvoice.Name = "btnInvoice";
			btnInvoice.TabStop = false;
			toolTip.SetToolTip(btnInvoice, resources.GetString("btnInvoice.ToolTip"));
			btnInvoice.UseVisualStyleBackColor = false;
			btnInvoice.Click += new System.EventHandler(btnInvoice_Click);
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
			dgDetails.Columns.AddRange(col_SL, col_Barcode, col_ProCode, col_ProName, col_HSN, col_Qty, col_Unit, col_Rate, col_Gross, col_Dis, col_Discount, col_Taxable, col_Vat, col_VatAmnt, col_GST, col_GSTAmnt, col_SGST, col_SGSTAmnt, col_CGST, col_CGSTAmnt, col_IGST, col_IGSTAmnt, col_NetAmnt);
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
			dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellEndEdit);
			dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgDetails_DataError);
			dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgDetails_EditingControlShowing);
			dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgDetails_KeyDown);
			col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SL.DataPropertyName = "SL";
			resources.ApplyResources(col_SL, "col_SL");
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Barcode.DataPropertyName = "Barcode";
			resources.ApplyResources(col_Barcode, "col_Barcode");
			col_Barcode.Name = "col_Barcode";
			col_ProCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_ProCode.DataPropertyName = "ProductCode";
			resources.ApplyResources(col_ProCode, "col_ProCode");
			col_ProCode.Name = "col_ProCode";
			col_ProName.DataPropertyName = "ProductName";
			resources.ApplyResources(col_ProName, "col_ProName");
			col_ProName.Name = "col_ProName";
			col_HSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_HSN.DataPropertyName = "Hsn";
			resources.ApplyResources(col_HSN, "col_HSN");
			col_HSN.Name = "col_HSN";
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
			col_Taxable.DataPropertyName = "Taxable";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Taxable.DefaultCellStyle = dataGridViewCellStyle10;
			resources.ApplyResources(col_Taxable, "col_Taxable");
			col_Taxable.Name = "col_Taxable";
			col_Taxable.ReadOnly = true;
			col_Vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_Vat.DataPropertyName = "Vat";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Vat.DefaultCellStyle = dataGridViewCellStyle11;
			resources.ApplyResources(col_Vat, "col_Vat");
			col_Vat.Name = "col_Vat";
			col_VatAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_VatAmnt.DataPropertyName = "VatAmount";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle12;
			resources.ApplyResources(col_VatAmnt, "col_VatAmnt");
			col_VatAmnt.Name = "col_VatAmnt";
			col_VatAmnt.ReadOnly = true;
			col_GST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_GST.DataPropertyName = "Gst";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_GST.DefaultCellStyle = dataGridViewCellStyle13;
			resources.ApplyResources(col_GST, "col_GST");
			col_GST.Name = "col_GST";
			col_GSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_GSTAmnt.DataPropertyName = "GstAmount";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle14;
			resources.ApplyResources(col_GSTAmnt, "col_GSTAmnt");
			col_GSTAmnt.Name = "col_GSTAmnt";
			col_GSTAmnt.ReadOnly = true;
			col_SGST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SGST.DataPropertyName = "Sgst";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_SGST.DefaultCellStyle = dataGridViewCellStyle15;
			resources.ApplyResources(col_SGST, "col_SGST");
			col_SGST.Name = "col_SGST";
			col_SGST.ReadOnly = true;
			col_SGSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SGSTAmnt.DataPropertyName = "SGstAmount";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle16;
			resources.ApplyResources(col_SGSTAmnt, "col_SGSTAmnt");
			col_SGSTAmnt.Name = "col_SGSTAmnt";
			col_SGSTAmnt.ReadOnly = true;
			col_CGST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_CGST.DataPropertyName = "CGst";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_CGST.DefaultCellStyle = dataGridViewCellStyle17;
			resources.ApplyResources(col_CGST, "col_CGST");
			col_CGST.Name = "col_CGST";
			col_CGST.ReadOnly = true;
			col_CGSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_CGSTAmnt.DataPropertyName = "CGstAmount";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle18;
			resources.ApplyResources(col_CGSTAmnt, "col_CGSTAmnt");
			col_CGSTAmnt.Name = "col_CGSTAmnt";
			col_CGSTAmnt.ReadOnly = true;
			col_IGST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_IGST.DataPropertyName = "IGst";
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_IGST.DefaultCellStyle = dataGridViewCellStyle19;
			resources.ApplyResources(col_IGST, "col_IGST");
			col_IGST.Name = "col_IGST";
			col_IGST.ReadOnly = true;
			col_IGSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_IGSTAmnt.DataPropertyName = "IGstAmount";
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle20;
			resources.ApplyResources(col_IGSTAmnt, "col_IGSTAmnt");
			col_IGSTAmnt.Name = "col_IGSTAmnt";
			col_IGSTAmnt.ReadOnly = true;
			col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_NetAmnt.DataPropertyName = "NETAmount";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle21;
			resources.ApplyResources(col_NetAmnt, "col_NetAmnt");
			col_NetAmnt.Name = "col_NetAmnt";
			col_NetAmnt.ReadOnly = true;
			resources.ApplyResources(lblNetTotal, "lblNetTotal");
			lblNetTotal.ForeColor = System.Drawing.Color.DodgerBlue;
			lblNetTotal.Format = null;
			lblNetTotal.Name = "lblNetTotal";
			toolTip.SetToolTip(lblNetTotal, resources.GetString("lblNetTotal.ToolTip"));
			lblNetTotal.Value = new decimal(new int[4]);
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
			resources.ApplyResources(lblLastRate, "lblLastRate");
			lblLastRate.ForeColor = System.Drawing.Color.Magenta;
			lblLastRate.Format = null;
			lblLastRate.Name = "lblLastRate";
			toolTip.SetToolTip(lblLastRate, resources.GetString("lblLastRate.ToolTip"));
			lblLastRate.Value = new decimal(new int[4]);
			resources.ApplyResources(lblUnitQty, "lblUnitQty");
			lblUnitQty.ForeColor = System.Drawing.Color.Red;
			lblUnitQty.Format = null;
			lblUnitQty.Name = "lblUnitQty";
			toolTip.SetToolTip(lblUnitQty, resources.GetString("lblUnitQty.ToolTip"));
			lblUnitQty.Value = new decimal(new int[4]);
			resources.ApplyResources(lblCStock, "lblCStock");
			lblCStock.ForeColor = System.Drawing.Color.Red;
			lblCStock.Format = null;
			lblCStock.Name = "lblCStock";
			toolTip.SetToolTip(lblCStock, resources.GetString("lblCStock.ToolTip"));
			lblCStock.Value = new decimal(new int[4]);
			resources.ApplyResources(btnSeperator2, "btnSeperator2");
			btnSeperator2.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSeperator2.Name = "btnSeperator2";
			btnSeperator2.TabStop = false;
			toolTip.SetToolTip(btnSeperator2, resources.GetString("btnSeperator2.ToolTip"));
			btnSeperator2.UseVisualStyleBackColor = true;
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
			resources.ApplyResources(label22, "label22");
			label22.Name = "label22";
			toolTip.SetToolTip(label22, resources.GetString("label22.ToolTip"));
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
			resources.ApplyResources(label34, "label34");
			label34.Name = "label34";
			toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip"));
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
			resources.ApplyResources(label33, "label33");
			label33.Name = "label33";
			toolTip.SetToolTip(label33, resources.GetString("label33.ToolTip"));
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
			resources.ApplyResources(cbPrint, "cbPrint");
			cbPrint.Name = "cbPrint";
			cbPrint.TabStop = false;
			toolTip.SetToolTip(cbPrint, resources.GetString("cbPrint.ToolTip"));
			cbPrint.UseVisualStyleBackColor = true;
			cbPrint.CheckedChanged += new System.EventHandler(cbPrint_CheckedChanged);
			resources.ApplyResources(label15, "label15");
			label15.Name = "label15";
			toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip"));
			resources.ApplyResources(label93, "label93");
			label93.Name = "label93";
			toolTip.SetToolTip(label93, resources.GetString("label93.ToolTip"));
			resources.ApplyResources(label90, "label90");
			label90.Name = "label90";
			toolTip.SetToolTip(label90, resources.GetString("label90.ToolTip"));
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
			resources.ApplyResources(label19, "label19");
			label19.Name = "label19";
			toolTip.SetToolTip(label19, resources.GetString("label19.ToolTip"));
			resources.ApplyResources(button6, "button6");
			button6.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button6.Name = "button6";
			button6.TabStop = false;
			toolTip.SetToolTip(button6, resources.GetString("button6.ToolTip"));
			button6.UseVisualStyleBackColor = true;
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
			btnSearch.TabStop = false;
			toolTip.SetToolTip(btnSearch, resources.GetString("btnSearch.ToolTip"));
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Name = "btnClear";
			btnClear.TabStop = false;
			toolTip.SetToolTip(btnClear, resources.GetString("btnClear.ToolTip"));
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
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
			resources.ApplyResources(cmbPurType, "cmbPurType");
			cmbPurType.EnterColor = System.Drawing.Color.LightPink;
			cmbPurType.FormattingEnabled = true;
			cmbPurType.LeaveColor = System.Drawing.Color.White;
			cmbPurType.Name = "cmbPurType";
			toolTip.SetToolTip(cmbPurType, resources.GetString("cmbPurType.ToolTip"));
			resources.ApplyResources(label11, "label11");
			label11.Name = "label11";
			toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
			resources.ApplyResources(cbMail, "cbMail");
			cbMail.Name = "cbMail";
			cbMail.TabStop = false;
			toolTip.SetToolTip(cbMail, resources.GetString("cbMail.ToolTip"));
			cbMail.UseVisualStyleBackColor = true;
			cbMail.CheckedChanged += new System.EventHandler(cbMail_CheckedChanged);
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
			flpButtons.Controls.Add(btnClear);
			flpButtons.Controls.Add(btnMail);
			flpButtons.Controls.Add(btnDelete);
			flpButtons.Controls.Add(btnEdit);
			flpButtons.Controls.Add(btnSave);
			flpButtons.Name = "flpButtons";
			toolTip.SetToolTip(flpButtons, resources.GetString("flpButtons.ToolTip"));
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(btnCustomerSearch);
			base.Controls.Add(flpButtons);
			base.Controls.Add(lblRunBal);
			base.Controls.Add(cbMail);
			base.Controls.Add(cmbPurType);
			base.Controls.Add(label11);
			base.Controls.Add(button6);
			base.Controls.Add(lblBalance);
			base.Controls.Add(lblPaidAmount);
			base.Controls.Add(label16);
			base.Controls.Add(label18);
			base.Controls.Add(label15);
			base.Controls.Add(label93);
			base.Controls.Add(label90);
			base.Controls.Add(lblTotalIGST);
			base.Controls.Add(lblTotalCGST);
			base.Controls.Add(lblTotalSGST);
			base.Controls.Add(lblTaxTotal);
			base.Controls.Add(label19);
			base.Controls.Add(btnSeperator2);
			base.Controls.Add(lblGrandTotal);
			base.Controls.Add(label36);
			base.Controls.Add(txtAddDiscAmnt);
			base.Controls.Add(txtAddDisc);
			base.Controls.Add(label22);
			base.Controls.Add(label35);
			base.Controls.Add(txtFreight);
			base.Controls.Add(label34);
			base.Controls.Add(txtAddAmnt);
			base.Controls.Add(label33);
			base.Controls.Add(txtPrintCopy);
			base.Controls.Add(cbPrint);
			base.Controls.Add(lblNetTotal);
			base.Controls.Add(lblTaxable);
			base.Controls.Add(lblDisTotal);
			base.Controls.Add(lblGrsTotal);
			base.Controls.Add(lblTotalQty);
			base.Controls.Add(lblLastRate);
			base.Controls.Add(lblUnitQty);
			base.Controls.Add(lblCStock);
			base.Controls.Add(dgDetails);
			base.Controls.Add(btnPayment);
			base.Controls.Add(btnInvoice);
			base.Controls.Add(label32);
			base.Controls.Add(btnLedgerReport);
			base.Controls.Add(cmbPriCategory);
			base.Controls.Add(label13);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label12);
			base.Controls.Add(cmbPartyAcct);
			base.Controls.Add(label6);
			base.Controls.Add(cmbPurAcct);
			base.Controls.Add(label3);
			base.Controls.Add(lblTitle);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label1);
			base.Controls.Add(chkEnableInvNo);
			base.Controls.Add(label2);
			base.Controls.Add(btnDate);
			base.Controls.Add(nudInvoiceNo);
			base.Name = "PurchaseReturnView";
			toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			base.Load += new System.EventHandler(PurchaseReturnView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(PurchaseReturnView_KeyDown);
			base.Controls.SetChildIndex(nudInvoiceNo, 0);
			base.Controls.SetChildIndex(btnDate, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(chkEnableInvNo, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbPurAcct, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(cmbPartyAcct, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(cmbPriCategory, 0);
			base.Controls.SetChildIndex(btnLedgerReport, 0);
			base.Controls.SetChildIndex(label32, 0);
			base.Controls.SetChildIndex(btnInvoice, 0);
			base.Controls.SetChildIndex(btnPayment, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			base.Controls.SetChildIndex(lblCStock, 0);
			base.Controls.SetChildIndex(lblUnitQty, 0);
			base.Controls.SetChildIndex(lblLastRate, 0);
			base.Controls.SetChildIndex(lblTotalQty, 0);
			base.Controls.SetChildIndex(lblGrsTotal, 0);
			base.Controls.SetChildIndex(lblDisTotal, 0);
			base.Controls.SetChildIndex(lblTaxable, 0);
			base.Controls.SetChildIndex(lblNetTotal, 0);
			base.Controls.SetChildIndex(cbPrint, 0);
			base.Controls.SetChildIndex(txtPrintCopy, 0);
			base.Controls.SetChildIndex(label33, 0);
			base.Controls.SetChildIndex(txtAddAmnt, 0);
			base.Controls.SetChildIndex(label34, 0);
			base.Controls.SetChildIndex(txtFreight, 0);
			base.Controls.SetChildIndex(label35, 0);
			base.Controls.SetChildIndex(label22, 0);
			base.Controls.SetChildIndex(txtAddDisc, 0);
			base.Controls.SetChildIndex(txtAddDiscAmnt, 0);
			base.Controls.SetChildIndex(label36, 0);
			base.Controls.SetChildIndex(lblGrandTotal, 0);
			base.Controls.SetChildIndex(btnSeperator2, 0);
			base.Controls.SetChildIndex(label19, 0);
			base.Controls.SetChildIndex(lblTaxTotal, 0);
			base.Controls.SetChildIndex(lblTotalSGST, 0);
			base.Controls.SetChildIndex(lblTotalCGST, 0);
			base.Controls.SetChildIndex(lblTotalIGST, 0);
			base.Controls.SetChildIndex(label90, 0);
			base.Controls.SetChildIndex(label93, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(label18, 0);
			base.Controls.SetChildIndex(label16, 0);
			base.Controls.SetChildIndex(lblPaidAmount, 0);
			base.Controls.SetChildIndex(lblBalance, 0);
			base.Controls.SetChildIndex(button6, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(cmbPurType, 0);
			base.Controls.SetChildIndex(cbMail, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblRunBal, 0);
			base.Controls.SetChildIndex(flpButtons, 0);
			base.Controls.SetChildIndex(btnCustomerSearch, 0);
			((System.ComponentModel.ISupportInitialize)nudInvoiceNo).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			flpButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
