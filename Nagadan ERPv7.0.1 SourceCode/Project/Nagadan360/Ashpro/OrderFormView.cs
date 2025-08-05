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
	public class OrderFormView : BaseForm
	{
		private DateTime estimateDate;

		private string sForm;

		private List<SettingsEntity> entBooleanSettings;

		private SalesOrderMaster entSalesOrder;

		private List<SalesOrderSettings> entSOrderDtls;

		private int iInvoiceId;

		private int? iMasterId;

		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private TransactionMaster entTMaster = new TransactionMaster();

		private List<Transaction> entTransactions = new List<Transaction>();

		private DataGridViewTextBoxEditingControl textbox;

		private string sKeyChar = "";

		private AccountEntity account;

		private DBContext db;

		private bool isPurchase = false;

		private List<ComboFill> comboFills = new List<ComboFill>();

		private decimal dSubDiscount;

		private IContainer components = null;

		private Label lblHeader;

		private AshUpDown nudInvoiceNo;

		private Button btnDate;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private CheckBox chkEnableInvNo;

		private Label label1;

		private AshComboBox cmbProCategory;

		private Label label13;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private AshComboBox cmbSlsExi;

		private Label label11;

		private AshComboBox cmbPartyAccnt;

		private Label label6;

		private Button btnAccCreate;

		private AshComboBox cmbRoute;

		private Label label4;

		private Label label32;

		private Button btnBalance;

		private AshComboBox cmbSlsAcct;

		private Label label3;

		private CheckBox cbOCompltd;

		public AshDataGridview dgDetails;

		private AshNumericLabel lblTaxable;

		private AshNumericLabel lblNetTotal;

		private AshNumericLabel lblDisTotal;

		private AshNumericLabel lblGrsTotal;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblUntQty;

		private AshNumericLabel lblLastSalesRate;

		private AshNumericLabel lblCStock;

		private Label label36;

		private AshTextBoxNormal txtDisAmnt;

		private AshTextBoxNormal txtDis;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private Label label34;

		private AshTextBoxNormal txtAddAmnt;

		private Label label33;

		private AshTextBoxNormal txtPrintCopy;

		private Button btnClear;

		private Button btnSearch;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private CheckBox cbMail;

		private CheckBox cbPrint;

		private AshNumericLabel lblTaxTotal;

		private Label label7;

		private BindingSource bdsDetails;

		private AshNumericLabel lblGrandTotal;

		private Label lblRunBal;

		private ToolTip toolTip;

		private Button btnDiscount;

		private Button btnPayment;
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
        private Button btnCustomerSearch;

		public int? iInvNo { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public OrderFormView(string _sForm)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			sForm = _sForm;
			InitializeComponent();
			ColorChange.Color(this);
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
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void LoadComboMethod()
		{
			if (sForm == "Purchase Order")
			{
				AniHelper.FillPurchaseAccount(cmbSlsAcct);
				AniHelper.FillAccountForPurchase(cmbPartyAccnt);
			}
			else
			{
				AniHelper.FillAccount(cmbPartyAccnt);
				AniHelper.FillSalesAccount(cmbSlsAcct);
			}
			AniHelper.FillEmployees(cmbSlsExi);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillPCategory(cmbProCategory);
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
			entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == sForm && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
			chkEnableInvNo.Checked = CommonSettings.blnEditInvoiceNumber;
			cbPrint.Checked = CommonSettings.blnPntInvWlSaving;
			cbMail.Checked = CommonSettings.blnSndMlWlSaving;
			if (CommonSettings.blnTaxEnabled)
			{
				if (CommonSettings.blnGSTEnabled)
				{
					col_Vat.Visible = false;
					col_VatAmnt.Visible = false;
					col_GST.Visible = true;
					col_GSTAmnt.Visible = true;
				}
				else
				{
					col_GST.Visible = true;
					col_GSTAmnt.Visible = true;
				}
			}
		}

		private void LoadSalesOrderFromID()
		{
			try
			{
				iMasterId = entSalesOrder.SalesOrderMasterId.ToInt32();
				entSOrderDtls = db.SalesOrderSettings(new
				{
					SalesOrderMasterId = iMasterId
				}).ToList();
				entTransactions = POSHelper.GetTransactionsFromSODetails(entSOrderDtls, db);
				nudInvoiceNo.Value = entSalesOrder.VchrNo.toInt32();
				dtmDate.Value = entSalesOrder.InvDate;
				cmbSlsAcct.SelectedValue = entSalesOrder.SalesAccountId.GetValueOrDefault();
				cmbPartyAccnt.SelectedValue = entSalesOrder.CustomerId.GetValueOrDefault();
				cmbRoute.SelectedValue = entSalesOrder.RouteId.GetValueOrDefault();
				cmbProCategory.SelectedValue = entSalesOrder.PriceCategoryId.GetValueOrDefault();
				cmbSlsExi.SelectedValue = entSalesOrder.SalesManId.GetValueOrDefault();
				txtAddAmnt.Value = entSalesOrder.AdditionalAmnt;
				txtFreight.Value = entSalesOrder.Freight;
				cbOCompltd.Checked = entSalesOrder.Status;
				decimal billDiscount = entSalesOrder.BillDiscount;
				decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
				decimal num2 = default(decimal);
				if (billDiscount > 0m && num > 0m)
				{
					num2 = billDiscount / num * 100m;
				}
				txtDis.Text = num2.ToString(CommonSettings.strCurencyFormat);
				txtDisAmnt.Value = billDiscount;
				txtRemarks.Text = entSalesOrder.Remarks;
				dgDetails.AutoGenerateColumns = false;
				bdsDetails.DataSource = null;
				dgDetails.DataSource = null;
				bdsDetails.DataSource = entTransactions;
				dgDetails.DataSource = bdsDetails;
				TotalFindMethod();
				foreach (Control control in base.Controls)
				{
					control.Enabled = false;
				}
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
				GodwnId = db.Godowns().FirstOrDefault().GodwnId.ToInt32(),
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
			lblTaxTotal.Text = entTransactions.Sum((Transaction x) => x.VatAmount).ToString(CommonSettings.strCurencyFormat);
			lblGrandTotal.Text = Math.Round(lblNetTotal.Text.ToDecimal() + txtAddAmnt.Text.ToDecimal() + txtFreight.Text.ToDecimal() - txtDisAmnt.Text.ToDecimal(), 0).ToString(CommonSettings.strCurencyFormat);
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
			entDMaster = db.DotmatrixMasters(new
			{
				FormType = sForm,
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
				entcompany.TaxId = company.GSTNo;
				entPartyDtl = new PartyDetails();
				account = db.Accounts(new
				{
					AccountId = cmbPartyAccnt.SelectedValue.ToInt32()
				}).FirstOrDefault();
				entPartyDtl = POSHelper.GetParty(account);
				iInvoiceId = AshproCommon.IdFindMethod("Select SalesOrderMasterId From SalesOrderMaster Where InvType='" + sForm + "'and VchrNo='" + nudInvoiceNo.Value + "'");
				entTMaster = new TransactionMaster();
				entSOrderDtls = new List<SalesOrderSettings>();
				entTransactions = new List<Transaction>();
				entSalesOrder = db.SalesOrderMasters(new
				{
					SalesOrderMasterId = iInvoiceId
				}).FirstOrDefault();
				entSOrderDtls = db.SalesOrderSettings(new
				{
					SalesOrderMasterId = iInvoiceId
				}).ToList();
				entTMaster = POSHelper.GetTMaster(entSalesOrder);
				entTMaster.GrossAmount = entTransactions.Sum((Transaction x) => x.GrossAmount);
				entTMaster.TotalQuantity = entTransactions.Sum((Transaction x) => x.Quantity);
				entTMaster.SalesMan = cmbSlsExi.Text;
				entTMaster.PriceCategory = cmbProCategory.Text;
				entTransactions = POSHelper.GetTransactionsFromSODetails(entSOrderDtls, db);
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, null, entTMaster, isPrint: true, txtPrintCopy.Data);
				printHelper = null;
			}
			else
			{
				Messages.ErrorMessage("There is no default print design. Please make your own design");
			}
		}

		private void SalesOrderLoadMethod(SalesOrderMaster sMaster, int InvNo)
		{
			sMaster.SalesOrderMasterId = iMasterId;
			sMaster.VchrNo = InvNo;
			sMaster.InvType = sForm;
			sMaster.InvDate = dtmDate.Value.Date;
			sMaster.SalesAccountId = cmbSlsAcct.SelectedValue.ToInt32();
			sMaster.CustomerId = cmbPartyAccnt.SelectedValue.ToInt32();
			sMaster.SalesManId = cmbSlsExi.SelectedValue.ToInt32();
			sMaster.RouteId = cmbRoute.SelectedValue.ToInt32();
			sMaster.PriceCategoryId = cmbProCategory.SelectedValue.ToInt32();
			sMaster.EntryDate = DateTime.Now.Date;
			sMaster.JEid = Settings.Default.intLogOnId;
			sMaster.Remarks = txtRemarks.Text;
			sMaster.AdditionalAmnt = txtAddAmnt.Value;
			sMaster.Freight = txtFreight.Value;
			sMaster.AdditionalAmnt = txtDisAmnt.Value;
			sMaster.GrandTotal = lblGrandTotal.Value;
			sMaster.Taxable = lblTaxable.Value;
			sMaster.BillDiscount = lblDisTotal.Value;
			sMaster.TotalTax = lblTaxTotal.Value;
			sMaster.FK_BranchId = Settings.Default.iBranchId;
		}

		private void FormClearMethod()
		{
			entTransactions = new List<Transaction>();
			cmbPartyAccnt.SelectedIndex = 0;
			cmbProCategory.SelectedIndex = 0;
			BindDgv();
		}

		private void btnClearmethod()
		{
			AniHelper.ClearMethod(this);
			FormClearMethod();
			iMasterId = null;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			cmbPartyAccnt.Focus();
		}

		private async void OrderFormView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			comboFills = await Task.Run(() => AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product"));
			lblHeader.Text = sForm;
			estimateDate = Settings.Default.appDate;
			LoadComboMethod();
			entSOrderDtls = new List<SalesOrderSettings>();
			entTransactions = new List<Transaction>();
			BindDgv();
			if (iInvNo.HasValue)
			{
				nudInvoiceNo.Value = iInvNo.ToInt32();
			}
			else
			{
				nudInvoiceNo.Value = (db.SalesOrderMasters(new
				{
					InvType = sForm,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((SalesOrderMaster x) => x.VchrNo) + 1) ?? 1;
			}
			dtmDate.Value = estimateDate;
			LoadSettingsMethod();
			if (sForm == "Purchase Order")
			{
				label6.Text = "Cash/Supplier :";
				label3.Text = "Purchase A/C :";
				label4.Visible = false;
				label11.Visible = false;
				label13.Visible = false;
				cmbProCategory.Visible = false;
				cmbSlsExi.Visible = false;
				cmbRoute.Visible = false;
			}
			else if (sForm == "Sales Quotation")
			{
				cbOCompltd.Visible = false;
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
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
			if (sForm == "Sales Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSO").FirstOrDefault();
			}
			else if (sForm == "Purchase Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberPO").FirstOrDefault();
			}
			else if (sForm == "Sales Quotation")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnEditInvoiceNumberSQ").FirstOrDefault();
			}
			settingsEntity.BitValue = chkEnableInvNo.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void btnAccCreate_Click(object sender, EventArgs e)
		{
			Account account = new Account();
			account.isOpenFromAnotherForm = true;
			if (!(sForm == "Sales Order") && !(sForm == "Sales Quotation"))
			{
				account.sFormName = "Supplier";
				account.accountGroupName = "SUPPLIERS/SUNDRY CREDITORS";
			}
			else
			{
				account.sFormName = "Party";
				account.accountGroupName = "CUSTOMER/SUNDRY DEBTORS";
			}
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

		private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Transaction current = getCurrent();
			int rowIndex = dgDetails.CurrentCell.RowIndex;
			if (dgDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
				productBarcodeEntity = db.ProductBarcodes(new { current.Barcode }).FirstOrDefault();
				POSHelper.GetTransactionsFromBarcode(current, productBarcodeEntity, cmbProCategory.SelectedValue.ToInt32(), rowIndex, string.Empty);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				POSHelper.GetTransactionsAfterEdit(current);
				if (sForm == "Sales Quotation" && current.Quantity > lblCStock.Value && CommonSettings.blnShowMsgWlNStockSQ)
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

		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			textbox = (DataGridViewTextBoxEditingControl)sender;
			try
			{
				Transaction current = getCurrent();
				int rowIndex = dgDetails.CurrentCell.RowIndex;
				if (dgDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					if (sForm == "Purchase Order")
					{
						isPurchase = true;
					}
					ShowItemList showItemList = new ShowItemList(isPurchase);
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
						current.UnitId = proSearch.UnitId;
						current.Unit = proSearch.UnitName;
						if (sForm == "Purchase Order")
						{
							current.Rate = proSearch.PPrice;
							current.Discount = proSearch.PPDisc;
						}
						else
						{
							current.Rate = proSearch.SPrice;
							current.Discount = proSearch.SPDisc;
						}
						StockFindMethod(current);
						POSHelper.GetTransactions(current, rowIndex, string.Empty);
						dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells["col_Qty"];
						TotalFindMethod();
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
					if (productUnit == null)
					{
						return;
					}
					dgDetails.CurrentCell.Value = selectCustomer.sName;
					current.UnitId = selectCustomer.iId;
					current.ProductName = productUnit.ProductUnitName;
					current.Barcode = productUnit.Barcode;
					if (sForm == "Purchase Order")
					{
						POSHelper.SellingPriceAndDiscFindMethod(cmbProCategory.SelectedValue.ToInt32(), "Purchase Invoice", productUnit.Barcode);
					}
					else
					{
						POSHelper.SellingPriceAndDiscFindMethod(cmbProCategory.SelectedValue.ToInt32(), "Sales Invoice", productUnit.Barcode);
					}
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
					if (CommonSettings.blnTaxEnabled)
					{
						current.Taxable = current.GrossAmount - current.DiscountAmount;
						if (current.Vat > 0m)
						{
							current.VatAmount = current.Taxable * (current.Vat / 100m);
						}
						else
						{
							current.VatAmount = 0m;
						}
						if (CommonSettings.blnGSTEnabled)
						{
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
						}
						current.NETAmount = current.Taxable + current.VatAmount;
					}
					else
					{
						current.NETAmount = current.GrossAmount - current.DiscountAmount;
					}
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells["col_Rate"];
					TotalFindMethod();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			else if (e.KeyCode == Keys.F8)
			{
				AniHelper.ItemCreationMethod(current.ProductId.toInt32());
			}
		}

		private void txtAddAmnt_TextChanged(object sender, EventArgs e)
		{
			TotalFindMethod();
		}

		private void txtDis_Validated(object sender, EventArgs e)
		{
			if (txtDis.Text == string.Empty)
			{
				txtDis.Value = 0m;
				return;
			}
			decimal value = txtDis.Value;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			decimal num2 = default(decimal);
			if (value > 0m)
			{
				num2 = value / 100m * num;
			}
			txtDisAmnt.Text = num2.ToString(CommonSettings.strCurencyFormat);
			TotalFindMethod();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = sForm;
				formSearch.sInvType = sForm;
				if (formSearch.ShowDialog() == DialogResult.OK)
				{
					iMasterId = formSearch.iInvoiceId;
					LoadSalesOrderFromID();
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				nudInvoiceNo.Value = (db.SalesOrderMasters(new
				{
					InvType = sForm,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((SalesOrderMaster x) => x.VchrNo) + 1) ?? 1;
				btnClearmethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
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
				if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("SalesOrderSettings", "SalesOrderMasterId", iMasterId.ToInt32()))
				{
					AshproCommon.DeleteFromDatabase("SalesOrderMaster", "SalesOrderMasterId", iMasterId.ToInt32());
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
			if (entTransactions.Count == 0)
			{
				Messages.WarningMessage("No records to save!");
				return;
			}
			entSOrderDtls = new List<SalesOrderSettings>();
			entSOrderDtls = POSHelper.GetSODetailsFrmTransaction(db, entTransactions, cmbPartyAccnt.SelectedValue.ToInt32(), dtmDate.Value.Date);
			try
			{
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
						return;
					}
					entSalesOrder = new SalesOrderMaster();
					int num = 0;
					num = (db.SalesOrderMasters(new
					{
						InvType = sForm,
						FK_BranchId = Settings.Default.iBranchId
					}).Max((SalesOrderMaster x) => x.VchrNo) + 1) ?? 1;
					SalesOrderLoadMethod(entSalesOrder, num);
					if (entSOrderDtls == null)
					{
						return;
					}
					entSalesOrder.SalesOrderMasterId = null;
					if (AshproCommon.InsertToDatabaseObj(entSalesOrder, "SalesOrderMaster"))
					{
						entSalesOrder = db.SalesOrderMasters(new
						{
							InvType = sForm,
							VchrNo = entSalesOrder.VchrNo,
							FK_BranchId = Settings.Default.iBranchId
						}).FirstOrDefault();
						if (entSalesOrder != null)
						{
							iMasterId = entSalesOrder.SalesOrderMasterId.toInt32();
							foreach (SalesOrderSettings entSOrderDtl in entSOrderDtls)
							{
								entSOrderDtl.SalesOrderMasterId = iMasterId.ToInt32();
							}
							AshproCommon.InsertToDatabase(entSOrderDtls.Cast<object>().ToList(), "SalesOrderSettings");
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
					foreach (SalesOrderSettings entSOrderDtl2 in entSOrderDtls)
					{
						entSOrderDtl2.SalesOrderMasterId = iMasterId.ToInt32();
					}
					AshproCommon.DeleteFromDatabase("SalesOrderSettings", "SalesOrderMasterId", iMasterId.ToInt32());
					SalesOrderLoadMethod(entSalesOrder, nudInvoiceNo.Value.ToInt32());
					if (AshproCommon.UpdateToDatabaseObj(entSalesOrder, "SalesOrderMaster", "SalesOrderMasterId", iMasterId.ToInt32()))
					{
						AshproCommon.InsertToDatabase(entSOrderDtls.Cast<object>().ToList(), "SalesOrderSettings");
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
				MessageBox.Show(ex.Message);
			}
		}

		private void cbPrint_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			if (sForm == "Sales Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSO").FirstOrDefault();
			}
			else if (sForm == "Purchase Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingPO").FirstOrDefault();
			}
			else if (sForm == "Sales Quotation")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnPntInvWlSavingSQ").FirstOrDefault();
			}
			settingsEntity.BitValue = cbPrint.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void cbMail_CheckedChanged(object sender, EventArgs e)
		{
			SettingsEntity settingsEntity = new SettingsEntity();
			if (sForm == "Sales Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSO").FirstOrDefault();
			}
			else if (sForm == "Purchase Order")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingPO").FirstOrDefault();
			}
			else if (sForm == "Sales Quotation")
			{
				settingsEntity = entBooleanSettings.Where((SettingsEntity x) => x.Name == "blnSndMlWlSavingSQ").FirstOrDefault();
			}
			settingsEntity.BitValue = cbMail.Checked;
			CommonSettings.UpdateSettings(entBooleanSettings, Settings.Default.strConnetionString);
		}

		private void nudInvoiceNo_ValueChanged(object sender, EventArgs e)
		{
			FormClearMethod();
			entSalesOrder = db.SalesOrderMasters(new
			{
				InvType = sForm,
				VchrNo = nudInvoiceNo.Value.ToInt32(),
				FK_BranchId = Settings.Default.iBranchId
			}).FirstOrDefault();
			if (entSalesOrder != null)
			{
				LoadSalesOrderFromID();
			}
			else
			{
				btnClearmethod();
			}
		}

		private void OrderFormView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSearch_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBalance_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnAccCreate_Click(null, null);
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

		private void btnPayment_Click(object sender, EventArgs e)
		{
		}

		private void btnCustomerSearch_Click(object sender, EventArgs e)
		{
			if (sForm == "Purchase Order")
			{
				AniHelper.CustomerSerchMethod(cmbPartyAccnt, isCustomer: false);
			}
			else
			{
				AniHelper.CustomerSerchMethod(cmbPartyAccnt);
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderFormView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.nudInvoiceNo = new AshUsercontrol.AshUpDown();
            this.btnDate = new System.Windows.Forms.Button();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnableInvNo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProCategory = new AshUsercontrol.AshComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbSlsExi = new AshUsercontrol.AshComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbPartyAccnt = new AshUsercontrol.AshComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAccCreate = new System.Windows.Forms.Button();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.btnBalance = new System.Windows.Forms.Button();
            this.cmbSlsAcct = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOCompltd = new System.Windows.Forms.CheckBox();
            this.dgDetails = new AshUsercontrol.AshDataGridview();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblTaxable = new AshUsercontrol.AshNumericLabel();
            this.lblNetTotal = new AshUsercontrol.AshNumericLabel();
            this.lblDisTotal = new AshUsercontrol.AshNumericLabel();
            this.lblGrsTotal = new AshUsercontrol.AshNumericLabel();
            this.lblTotalQty = new AshUsercontrol.AshNumericLabel();
            this.lblUntQty = new AshUsercontrol.AshNumericLabel();
            this.lblLastSalesRate = new AshUsercontrol.AshNumericLabel();
            this.lblCStock = new AshUsercontrol.AshNumericLabel();
            this.label36 = new System.Windows.Forms.Label();
            this.txtDisAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.txtDis = new AshUsercontrol.AshTextBoxNormal();
            this.label35 = new System.Windows.Forms.Label();
            this.txtFreight = new AshUsercontrol.AshTextBoxNormal();
            this.label34 = new System.Windows.Forms.Label();
            this.txtAddAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label33 = new System.Windows.Forms.Label();
            this.txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbMail = new System.Windows.Forms.CheckBox();
            this.cbPrint = new System.Windows.Forms.CheckBox();
            this.lblTaxTotal = new AshUsercontrol.AshNumericLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblGrandTotal = new AshUsercontrol.AshNumericLabel();
            this.lblRunBal = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.btnCustomerSearch = new System.Windows.Forms.Button();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            this.toolTip.SetToolTip(this.lblHeader, resources.GetString("lblHeader.ToolTip"));
            // 
            // nudInvoiceNo
            // 
            resources.ApplyResources(this.nudInvoiceNo, "nudInvoiceNo");
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
            this.nudInvoiceNo.TabStop = false;
            this.toolTip.SetToolTip(this.nudInvoiceNo, resources.GetString("nudInvoiceNo.ToolTip"));
            this.nudInvoiceNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInvoiceNo.ValueChanged += new System.EventHandler(this.nudInvoiceNo_ValueChanged);
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.btnDate.TabStop = false;
            this.toolTip.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.TabStop = false;
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // chkEnableInvNo
            // 
            resources.ApplyResources(this.chkEnableInvNo, "chkEnableInvNo");
            this.chkEnableInvNo.Name = "chkEnableInvNo";
            this.chkEnableInvNo.TabStop = false;
            this.toolTip.SetToolTip(this.chkEnableInvNo, resources.GetString("chkEnableInvNo.ToolTip"));
            this.chkEnableInvNo.UseVisualStyleBackColor = true;
            this.chkEnableInvNo.CheckedChanged += new System.EventHandler(this.chkEnableInvNo_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // cmbProCategory
            // 
            resources.ApplyResources(this.cmbProCategory, "cmbProCategory");
            this.cmbProCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProCategory.FormattingEnabled = true;
            this.cmbProCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbProCategory.Name = "cmbProCategory";
            this.toolTip.SetToolTip(this.cmbProCategory, resources.GetString("cmbProCategory.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Data = 0;
            this.txtRemarks.EnterColor = System.Drawing.Color.LightPink;
            this.txtRemarks.Format = null;
            this.txtRemarks.isAllowNegative = false;
            this.txtRemarks.isAllowSpecialChar = false;
            this.txtRemarks.isNumberOnly = false;
            this.txtRemarks.isNumeric = false;
            this.txtRemarks.LeaveColor = System.Drawing.Color.White;
            this.txtRemarks.Name = "txtRemarks";
            this.toolTip.SetToolTip(this.txtRemarks, resources.GetString("txtRemarks.ToolTip"));
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // cmbSlsExi
            // 
            resources.ApplyResources(this.cmbSlsExi, "cmbSlsExi");
            this.cmbSlsExi.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSlsExi.FormattingEnabled = true;
            this.cmbSlsExi.LeaveColor = System.Drawing.Color.White;
            this.cmbSlsExi.Name = "cmbSlsExi";
            this.toolTip.SetToolTip(this.cmbSlsExi, resources.GetString("cmbSlsExi.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // cmbPartyAccnt
            // 
            resources.ApplyResources(this.cmbPartyAccnt, "cmbPartyAccnt");
            this.cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyAccnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAccnt.FormattingEnabled = true;
            this.cmbPartyAccnt.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAccnt.Name = "cmbPartyAccnt";
            this.toolTip.SetToolTip(this.cmbPartyAccnt, resources.GetString("cmbPartyAccnt.ToolTip"));
            this.cmbPartyAccnt.SelectedIndexChanged += new System.EventHandler(this.cmbPartyAccnt_SelectedIndexChanged);
            this.cmbPartyAccnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPartyAccnt_KeyDown);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // btnAccCreate
            // 
            resources.ApplyResources(this.btnAccCreate, "btnAccCreate");
            this.btnAccCreate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAccCreate.ForeColor = System.Drawing.Color.White;
            this.btnAccCreate.Name = "btnAccCreate";
            this.btnAccCreate.TabStop = false;
            this.toolTip.SetToolTip(this.btnAccCreate, resources.GetString("btnAccCreate.ToolTip"));
            this.btnAccCreate.UseVisualStyleBackColor = false;
            this.btnAccCreate.Click += new System.EventHandler(this.btnAccCreate_Click);
            // 
            // cmbRoute
            // 
            resources.ApplyResources(this.cmbRoute, "cmbRoute");
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Name = "cmbRoute";
            this.toolTip.SetToolTip(this.cmbRoute, resources.GetString("cmbRoute.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            this.toolTip.SetToolTip(this.label32, resources.GetString("label32.ToolTip"));
            // 
            // btnBalance
            // 
            resources.ApplyResources(this.btnBalance, "btnBalance");
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.TabStop = false;
            this.toolTip.SetToolTip(this.btnBalance, resources.GetString("btnBalance.ToolTip"));
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // cmbSlsAcct
            // 
            resources.ApplyResources(this.cmbSlsAcct, "cmbSlsAcct");
            this.cmbSlsAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSlsAcct.FormattingEnabled = true;
            this.cmbSlsAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbSlsAcct.Name = "cmbSlsAcct";
            this.toolTip.SetToolTip(this.cmbSlsAcct, resources.GetString("cmbSlsAcct.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // cbOCompltd
            // 
            resources.ApplyResources(this.cbOCompltd, "cbOCompltd");
            this.cbOCompltd.Name = "cbOCompltd";
            this.cbOCompltd.TabStop = false;
            this.toolTip.SetToolTip(this.cbOCompltd, resources.GetString("cbOCompltd.ToolTip"));
            this.cbOCompltd.UseVisualStyleBackColor = true;
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ProCode,
            this.col_Barcode,
            this.col_ProName,
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
            dataGridViewCellStyle21.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.EnterKeyNavigation = false;
            this.dgDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgDetails.Name = "dgDetails";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.RowTemplate.Height = 25;
            this.toolTip.SetToolTip(this.dgDetails, resources.GetString("dgDetails.ToolTip"));
            this.dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellEndEdit);
            this.dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgDetails_DataError);
            this.dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgDetails_EditingControlShowing);
            this.dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgDetails_KeyDown);
            // 
            // col_SL
            // 
            this.col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_ProCode
            // 
            this.col_ProCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ProCode.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.col_ProCode, "col_ProCode");
            this.col_ProCode.Name = "col_ProCode";
            // 
            // col_Barcode
            // 
            this.col_Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_Barcode, "col_Barcode");
            this.col_Barcode.Name = "col_Barcode";
            // 
            // col_ProName
            // 
            this.col_ProName.DataPropertyName = "ProductName";
            resources.ApplyResources(this.col_ProName, "col_ProName");
            this.col_ProName.Name = "col_ProName";
            // 
            // col_HSN
            // 
            this.col_HSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_HSN.DataPropertyName = "Hsn";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_HSN.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.col_HSN, "col_HSN");
            this.col_HSN.Name = "col_HSN";
            // 
            // col_Qty
            // 
            this.col_Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Qty.DataPropertyName = "Quantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Qty.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.col_Qty, "col_Qty");
            this.col_Qty.Name = "col_Qty";
            // 
            // col_Unit
            // 
            this.col_Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Unit.DataPropertyName = "Unit";
            resources.ApplyResources(this.col_Unit, "col_Unit");
            this.col_Unit.Name = "col_Unit";
            // 
            // col_Rate
            // 
            this.col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Rate.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_Rate, "col_Rate");
            this.col_Rate.Name = "col_Rate";
            // 
            // col_Gross
            // 
            this.col_Gross.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Gross.DataPropertyName = "GrossAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Gross.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.col_Gross, "col_Gross");
            this.col_Gross.Name = "col_Gross";
            this.col_Gross.ReadOnly = true;
            // 
            // col_Dis
            // 
            this.col_Dis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Dis.DataPropertyName = "Discount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Dis.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.col_Dis, "col_Dis");
            this.col_Dis.Name = "col_Dis";
            // 
            // col_Discount
            // 
            this.col_Discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Discount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Discount.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.col_Discount, "col_Discount");
            this.col_Discount.Name = "col_Discount";
            this.col_Discount.ReadOnly = true;
            // 
            // col_Taxable
            // 
            this.col_Taxable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Taxable.DataPropertyName = "Taxable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Taxable.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.col_Taxable, "col_Taxable");
            this.col_Taxable.Name = "col_Taxable";
            this.col_Taxable.ReadOnly = true;
            // 
            // col_Vat
            // 
            this.col_Vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Vat.DataPropertyName = "Vat";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Vat.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.col_Vat, "col_Vat");
            this.col_Vat.Name = "col_Vat";
            // 
            // col_VatAmnt
            // 
            this.col_VatAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_VatAmnt.DataPropertyName = "VatAmount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.col_VatAmnt, "col_VatAmnt");
            this.col_VatAmnt.Name = "col_VatAmnt";
            this.col_VatAmnt.ReadOnly = true;
            // 
            // col_GST
            // 
            this.col_GST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_GST.DataPropertyName = "Gst";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_GST.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.col_GST, "col_GST");
            this.col_GST.Name = "col_GST";
            // 
            // col_GSTAmnt
            // 
            this.col_GSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_GSTAmnt.DataPropertyName = "GstAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.col_GSTAmnt, "col_GSTAmnt");
            this.col_GSTAmnt.Name = "col_GSTAmnt";
            this.col_GSTAmnt.ReadOnly = true;
            // 
            // col_SGST
            // 
            this.col_SGST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SGST.DataPropertyName = "Sgst";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_SGST.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.col_SGST, "col_SGST");
            this.col_SGST.Name = "col_SGST";
            this.col_SGST.ReadOnly = true;
            // 
            // col_SGSTAmnt
            // 
            this.col_SGSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SGSTAmnt.DataPropertyName = "SGstAmount";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.col_SGSTAmnt, "col_SGSTAmnt");
            this.col_SGSTAmnt.Name = "col_SGSTAmnt";
            this.col_SGSTAmnt.ReadOnly = true;
            // 
            // col_CGST
            // 
            this.col_CGST.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_CGST.DataPropertyName = "CGst";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_CGST.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.col_CGST, "col_CGST");
            this.col_CGST.Name = "col_CGST";
            this.col_CGST.ReadOnly = true;
            // 
            // col_CGSTAmnt
            // 
            this.col_CGSTAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_CGSTAmnt.DataPropertyName = "CGstAmount";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.col_CGSTAmnt, "col_CGSTAmnt");
            this.col_CGSTAmnt.Name = "col_CGSTAmnt";
            this.col_CGSTAmnt.ReadOnly = true;
            // 
            // col_IGST
            // 
            this.col_IGST.DataPropertyName = "IGst";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_IGST.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.col_IGST, "col_IGST");
            this.col_IGST.Name = "col_IGST";
            this.col_IGST.ReadOnly = true;
            // 
            // col_IGSTAmnt
            // 
            this.col_IGSTAmnt.DataPropertyName = "IGstAmount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.col_IGSTAmnt, "col_IGSTAmnt");
            this.col_IGSTAmnt.Name = "col_IGSTAmnt";
            this.col_IGSTAmnt.ReadOnly = true;
            // 
            // col_NetAmnt
            // 
            this.col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_NetAmnt.DataPropertyName = "NETAmount";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle20;
            resources.ApplyResources(this.col_NetAmnt, "col_NetAmnt");
            this.col_NetAmnt.Name = "col_NetAmnt";
            this.col_NetAmnt.ReadOnly = true;
            // 
            // lblTaxable
            // 
            resources.ApplyResources(this.lblTaxable, "lblTaxable");
            this.lblTaxable.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTaxable.Format = null;
            this.lblTaxable.Name = "lblTaxable";
            this.toolTip.SetToolTip(this.lblTaxable, resources.GetString("lblTaxable.ToolTip"));
            this.lblTaxable.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblNetTotal
            // 
            resources.ApplyResources(this.lblNetTotal, "lblNetTotal");
            this.lblNetTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNetTotal.Format = null;
            this.lblNetTotal.Name = "lblNetTotal";
            this.toolTip.SetToolTip(this.lblNetTotal, resources.GetString("lblNetTotal.ToolTip"));
            this.lblNetTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblDisTotal
            // 
            resources.ApplyResources(this.lblDisTotal, "lblDisTotal");
            this.lblDisTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDisTotal.Format = null;
            this.lblDisTotal.Name = "lblDisTotal";
            this.toolTip.SetToolTip(this.lblDisTotal, resources.GetString("lblDisTotal.ToolTip"));
            this.lblDisTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblGrsTotal
            // 
            resources.ApplyResources(this.lblGrsTotal, "lblGrsTotal");
            this.lblGrsTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblGrsTotal.Format = null;
            this.lblGrsTotal.Name = "lblGrsTotal";
            this.toolTip.SetToolTip(this.lblGrsTotal, resources.GetString("lblGrsTotal.ToolTip"));
            this.lblGrsTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblTotalQty
            // 
            resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
            this.lblTotalQty.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalQty.Format = null;
            this.lblTotalQty.Name = "lblTotalQty";
            this.toolTip.SetToolTip(this.lblTotalQty, resources.GetString("lblTotalQty.ToolTip"));
            this.lblTotalQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblUntQty
            // 
            resources.ApplyResources(this.lblUntQty, "lblUntQty");
            this.lblUntQty.ForeColor = System.Drawing.Color.Magenta;
            this.lblUntQty.Format = null;
            this.lblUntQty.Name = "lblUntQty";
            this.toolTip.SetToolTip(this.lblUntQty, resources.GetString("lblUntQty.ToolTip"));
            this.lblUntQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblLastSalesRate
            // 
            resources.ApplyResources(this.lblLastSalesRate, "lblLastSalesRate");
            this.lblLastSalesRate.ForeColor = System.Drawing.Color.Red;
            this.lblLastSalesRate.Format = null;
            this.lblLastSalesRate.Name = "lblLastSalesRate";
            this.toolTip.SetToolTip(this.lblLastSalesRate, resources.GetString("lblLastSalesRate.ToolTip"));
            this.lblLastSalesRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblCStock
            // 
            resources.ApplyResources(this.lblCStock, "lblCStock");
            this.lblCStock.ForeColor = System.Drawing.Color.Red;
            this.lblCStock.Format = null;
            this.lblCStock.Name = "lblCStock";
            this.toolTip.SetToolTip(this.lblCStock, resources.GetString("lblCStock.ToolTip"));
            this.lblCStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            this.toolTip.SetToolTip(this.label36, resources.GetString("label36.ToolTip"));
            // 
            // txtDisAmnt
            // 
            resources.ApplyResources(this.txtDisAmnt, "txtDisAmnt");
            this.txtDisAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDisAmnt.Data = 0;
            this.txtDisAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtDisAmnt.Format = null;
            this.txtDisAmnt.isAllowNegative = false;
            this.txtDisAmnt.isAllowSpecialChar = false;
            this.txtDisAmnt.isNumberOnly = false;
            this.txtDisAmnt.isNumeric = false;
            this.txtDisAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtDisAmnt.Name = "txtDisAmnt";
            this.toolTip.SetToolTip(this.txtDisAmnt, resources.GetString("txtDisAmnt.ToolTip"));
            this.txtDisAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtDis
            // 
            resources.ApplyResources(this.txtDis, "txtDis");
            this.txtDis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDis.Data = 0;
            this.txtDis.EnterColor = System.Drawing.Color.LightPink;
            this.txtDis.Format = null;
            this.txtDis.isAllowNegative = false;
            this.txtDis.isAllowSpecialChar = false;
            this.txtDis.isNumberOnly = false;
            this.txtDis.isNumeric = false;
            this.txtDis.LeaveColor = System.Drawing.Color.White;
            this.txtDis.Name = "txtDis";
            this.toolTip.SetToolTip(this.txtDis, resources.GetString("txtDis.ToolTip"));
            this.txtDis.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDis.Validated += new System.EventHandler(this.txtDis_Validated);
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            this.toolTip.SetToolTip(this.label35, resources.GetString("label35.ToolTip"));
            // 
            // txtFreight
            // 
            resources.ApplyResources(this.txtFreight, "txtFreight");
            this.txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFreight.Data = 0;
            this.txtFreight.EnterColor = System.Drawing.Color.LightPink;
            this.txtFreight.Format = null;
            this.txtFreight.isAllowNegative = false;
            this.txtFreight.isAllowSpecialChar = false;
            this.txtFreight.isNumberOnly = false;
            this.txtFreight.isNumeric = false;
            this.txtFreight.LeaveColor = System.Drawing.Color.White;
            this.txtFreight.Name = "txtFreight";
            this.toolTip.SetToolTip(this.txtFreight, resources.GetString("txtFreight.ToolTip"));
            this.txtFreight.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtFreight.TextChanged += new System.EventHandler(this.txtAddAmnt_TextChanged);
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            this.toolTip.SetToolTip(this.label34, resources.GetString("label34.ToolTip"));
            // 
            // txtAddAmnt
            // 
            resources.ApplyResources(this.txtAddAmnt, "txtAddAmnt");
            this.txtAddAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddAmnt.Data = 0;
            this.txtAddAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddAmnt.Format = null;
            this.txtAddAmnt.isAllowNegative = false;
            this.txtAddAmnt.isAllowSpecialChar = false;
            this.txtAddAmnt.isNumberOnly = false;
            this.txtAddAmnt.isNumeric = false;
            this.txtAddAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtAddAmnt.Name = "txtAddAmnt";
            this.toolTip.SetToolTip(this.txtAddAmnt, resources.GetString("txtAddAmnt.ToolTip"));
            this.txtAddAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAddAmnt.TextChanged += new System.EventHandler(this.txtAddAmnt_TextChanged);
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            this.toolTip.SetToolTip(this.label33, resources.GetString("label33.ToolTip"));
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
            this.toolTip.SetToolTip(this.txtPrintCopy, resources.GetString("txtPrintCopy.ToolTip"));
            this.txtPrintCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.TabStop = false;
            this.toolTip.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.TabStop = false;
            this.toolTip.SetToolTip(this.btnSearch, resources.GetString("btnSearch.ToolTip"));
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMail
            // 
            resources.ApplyResources(this.btnMail, "btnMail");
            this.btnMail.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.Name = "btnMail";
            this.btnMail.TabStop = false;
            this.toolTip.SetToolTip(this.btnMail, resources.GetString("btnMail.ToolTip"));
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.TabStop = false;
            this.toolTip.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabStop = false;
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TabStop = false;
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabStop = false;
            this.toolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbMail
            // 
            resources.ApplyResources(this.cbMail, "cbMail");
            this.cbMail.Name = "cbMail";
            this.cbMail.TabStop = false;
            this.toolTip.SetToolTip(this.cbMail, resources.GetString("cbMail.ToolTip"));
            this.cbMail.UseVisualStyleBackColor = true;
            this.cbMail.CheckedChanged += new System.EventHandler(this.cbMail_CheckedChanged);
            // 
            // cbPrint
            // 
            resources.ApplyResources(this.cbPrint, "cbPrint");
            this.cbPrint.Name = "cbPrint";
            this.cbPrint.TabStop = false;
            this.toolTip.SetToolTip(this.cbPrint, resources.GetString("cbPrint.ToolTip"));
            this.cbPrint.UseVisualStyleBackColor = true;
            this.cbPrint.CheckedChanged += new System.EventHandler(this.cbPrint_CheckedChanged);
            // 
            // lblTaxTotal
            // 
            resources.ApplyResources(this.lblTaxTotal, "lblTaxTotal");
            this.lblTaxTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaxTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTaxTotal.Format = null;
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.toolTip.SetToolTip(this.lblTaxTotal, resources.GetString("lblTaxTotal.ToolTip"));
            this.lblTaxTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // lblGrandTotal
            // 
            resources.ApplyResources(this.lblGrandTotal, "lblGrandTotal");
            this.lblGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrandTotal.ForeColor = System.Drawing.Color.Red;
            this.lblGrandTotal.Format = null;
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.toolTip.SetToolTip(this.lblGrandTotal, resources.GetString("lblGrandTotal.ToolTip"));
            this.lblGrandTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblRunBal
            // 
            resources.ApplyResources(this.lblRunBal, "lblRunBal");
            this.lblRunBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRunBal.ForeColor = System.Drawing.Color.Red;
            this.lblRunBal.Name = "lblRunBal";
            this.toolTip.SetToolTip(this.lblRunBal, resources.GetString("lblRunBal.ToolTip"));
            // 
            // btnPayment
            // 
            resources.ApplyResources(this.btnPayment, "btnPayment");
            this.btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPayment.ForeColor = System.Drawing.Color.White;
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.TabStop = false;
            this.toolTip.SetToolTip(this.btnPayment, resources.GetString("btnPayment.ToolTip"));
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnDiscount
            // 
            resources.ApplyResources(this.btnDiscount, "btnDiscount");
            this.btnDiscount.Name = "btnDiscount";
            this.toolTip.SetToolTip(this.btnDiscount, resources.GetString("btnDiscount.ToolTip"));
            this.btnDiscount.UseVisualStyleBackColor = true;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btnCustomerSearch
            // 
            resources.ApplyResources(this.btnCustomerSearch, "btnCustomerSearch");
            this.btnCustomerSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCustomerSearch.ForeColor = System.Drawing.Color.White;
            this.btnCustomerSearch.Name = "btnCustomerSearch";
            this.btnCustomerSearch.TabStop = false;
            this.toolTip.SetToolTip(this.btnCustomerSearch, resources.GetString("btnCustomerSearch.ToolTip"));
            this.btnCustomerSearch.UseVisualStyleBackColor = false;
            this.btnCustomerSearch.Click += new System.EventHandler(this.btnCustomerSearch_Click);
            // 
            // OrderFormView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnDiscount);
            this.Controls.Add(this.lblRunBal);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.lblTaxTotal);
            this.Controls.Add(this.txtPrintCopy);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbMail);
            this.Controls.Add(this.cbPrint);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txtDisAmnt);
            this.Controls.Add(this.txtDis);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtFreight);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtAddAmnt);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblTaxable);
            this.Controls.Add(this.lblNetTotal);
            this.Controls.Add(this.lblDisTotal);
            this.Controls.Add(this.lblGrsTotal);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.lblUntQty);
            this.Controls.Add(this.lblLastSalesRate);
            this.Controls.Add(this.lblCStock);
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.cbOCompltd);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.cmbSlsAcct);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbProCategory);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbSlsExi);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbPartyAccnt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCustomerSearch);
            this.Controls.Add(this.btnAccCreate);
            this.Controls.Add(this.cmbRoute);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.nudInvoiceNo);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkEnableInvNo);
            this.Controls.Add(this.label1);
            this.Name = "OrderFormView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.OrderFormView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OrderFormView_KeyDown);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chkEnableInvNo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.nudInvoiceNo, 0);
            this.Controls.SetChildIndex(this.lblHeader, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbRoute, 0);
            this.Controls.SetChildIndex(this.btnAccCreate, 0);
            this.Controls.SetChildIndex(this.btnCustomerSearch, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbPartyAccnt, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cmbSlsExi, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.cmbProCategory, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbSlsAcct, 0);
            this.Controls.SetChildIndex(this.btnBalance, 0);
            this.Controls.SetChildIndex(this.label32, 0);
            this.Controls.SetChildIndex(this.cbOCompltd, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.Controls.SetChildIndex(this.lblCStock, 0);
            this.Controls.SetChildIndex(this.lblLastSalesRate, 0);
            this.Controls.SetChildIndex(this.lblUntQty, 0);
            this.Controls.SetChildIndex(this.lblTotalQty, 0);
            this.Controls.SetChildIndex(this.lblGrsTotal, 0);
            this.Controls.SetChildIndex(this.lblDisTotal, 0);
            this.Controls.SetChildIndex(this.lblNetTotal, 0);
            this.Controls.SetChildIndex(this.lblTaxable, 0);
            this.Controls.SetChildIndex(this.label33, 0);
            this.Controls.SetChildIndex(this.txtAddAmnt, 0);
            this.Controls.SetChildIndex(this.label34, 0);
            this.Controls.SetChildIndex(this.txtFreight, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtDis, 0);
            this.Controls.SetChildIndex(this.txtDisAmnt, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.cbPrint, 0);
            this.Controls.SetChildIndex(this.cbMail, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnMail, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.txtPrintCopy, 0);
            this.Controls.SetChildIndex(this.lblTaxTotal, 0);
            this.Controls.SetChildIndex(this.lblGrandTotal, 0);
            this.Controls.SetChildIndex(this.lblRunBal, 0);
            this.Controls.SetChildIndex(this.btnDiscount, 0);
            this.Controls.SetChildIndex(this.btnPayment, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudInvoiceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
