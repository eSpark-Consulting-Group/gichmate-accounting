using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class FirstPurchase : Form
	{
		private DateTime estimateDate;

		private List<Transaction> entTransactions = new List<Transaction>();

		private Transaction entTransaction = new Transaction();

		private AccountEntity account;

		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private TransactionMaster entTMaster = new TransactionMaster();

		private List<PurchaseInvoiceDtl> entPurDtls = new List<PurchaseInvoiceDtl>();

		private int iCashAcctId;

		private int iPurInvId;

		private int? iMasterId;

		private int? iProId;

		private DBContext db;

		private Calculator calcu;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private Panel pnlDetails;

		private Button btnClose;

		private Button btnClearAll;

		private Button btnSave;

		private AshNumericLabel lblTotalTax;

		private Label label4;

		private AshNumericLabel lblGrandTotal;

		private Label label36;

		private AshTextBoxNormal txtDisAmount;

		private AshTextBoxNormal txtDis;

		private Label label35;

		private AshTextBoxNormal txtFreight;

		private Label label34;

		private AshTextBoxNormal txtAddAmount;

		private Label label33;

		private CheckBox chkPrntWlSave;

		private AshNumericLabel lblNetTotal;

		private AshNumericLabel lblDisTotal;

		private Label label46;

		private Label label29;

		private AshNumericLabel lblGrsTotal;

		private Label label27;

		private AshNumericLabel lblTotalQty;

		private Label label25;

		private Button btnDate;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private Label label1;

		private Label label10;

		private Button btnAccount;

		private AshComboBox cmbPurAcct;

		private Label label3;

		private Panel pnlCustomer;

		private Panel pnlProduct;

		private AshTextBoxNormal txtPhoneNo;

		private Label label6;

		private AshTextBoxNormal txtAddressTwo;

		private Label label9;

		private AshTextBoxNormal txtAddressOne;

		private Label label8;

		private AshTextBoxNormal txtName;

		private Label label7;

		private Button btnCreateAccount;

		private AshTextBoxNormal txtSaleRate;

		private Label label15;

		private AshTextBoxNormal txtPurRate;

		private Label label13;

		private Label label14;

		private AshTextBoxNormal txtQty;

		private Label label12;

		private AshTextBoxNormal txtProduct;

		private Label label11;

		private AshTextBoxNormal txtMRP;

		private Label label19;

		private Label label17;

		private Label label18;

		private Label label16;

		private AshTextBoxNormal txtTaxAmnt;

		private AshTextBoxNormal txtDiscAmnt;

		private Label label23;

		private Label label22;

		private AshTextBoxNormal txtDiscP;

		private Label label21;

		private Label label20;

		private AshNumericLabel lblLastDisc;

		private AshNumericLabel lblUnitQty;

		private AshNumericLabel lblLastRate;

		private Label label85;

		private Label label31;

		private AshNumericLabel lblStock;

		private Label label38;

		private Label label39;

		private AshTextBoxNormal txtRemarks;

		private Label label48;

		private Button btnAdd;

		private AshTextBoxNormal txtGross;

		private Label label50;

		private Label label49;

		private Label label51;

		private AshNumericLabel lblNet;

		private AshTextBoxNormal txtAddressThree;

		private Label label53;

		private Button btnClear;

		private AshComboBox cmbPartyAcct;

		private AshComboBox cmbShelf;

		private AshComboBox cmbGroup;

		private AshComboBox cmbGodown;

		private AshComboBox cmbBrand;

		private AshComboBox cmbUnit;

		private ToolTip toolTip1;

		private AshTextBoxNormal txtPrintCopy;

		private ContextMenuStrip ctxtMenuFrstpur;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripMenuItem clearToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem printToolStripMenuItem;

		private ToolStripMenuItem mailToolStripMenuItem;

		private ToolStripMenuItem settingsToolStripMenuItem;

		private ToolStripMenuItem backToolStripMenuItem;

		private ToolStripMenuItem defaultSettingsToolStripMenuItem;

		private ToolStripMenuItem updateSettingsToolStripMenuItem;

		private ToolStripMenuItem goToSalesReturnToolStripMenuItem;

		private ToolStripMenuItem calculatorToolStripMenuItem;

		private ToolStripMenuItem openNewAccountToolStripMenuItem;

		private ToolStripMenuItem createNewAccountToolStripMenuItem;

		private AshTextBoxNormal txtStateCode;

		private Label label32;

		private AshTextBoxNormal txtGstNo;

		private Label label24;

		public AshDataGridview dgDetails;

		private AshTextBoxNormal txtInvNo;

		private AshTextBoxNormal txtRefNo;

		private Label label5;

		private Button btnPayment;

		private Button btnBarcode;

		private AshNumericLabel lblPaidAmount;

		private Label label26;

		private AshNumericLabel lblBalance;

		private Label label28;

		private AshComboBox cmbTaxCategory;
        private DataGridViewTextBoxColumn col_SL;
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
        private BindingSource bdsDetails;

		public UserPrevilegeDtl plgDtls { get; set; }

		public FirstPurchase()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
            AutoCompleteText();
		}

		private void AutoCompleteText()
		{
			try
			{
				db = new DBContext();
				txtProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				txtProduct.AutoCompleteSource = AutoCompleteSource.CustomSource;
				AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
				autoCompleteStringCollection.Clear();
				foreach (ProductEntity item in db.Products())
				{
					autoCompleteStringCollection.Add(item.ProductName);
				}
				txtProduct.AutoCompleteCustomSource = autoCompleteStringCollection;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void TotalFindMethod()
		{
			lblTotalQty.Text = entTransactions.Sum((Transaction x) => x.Quantity).ToString(CommonSettings.strQuantityFormat);
			lblGrsTotal.Text = entTransactions.Sum((Transaction x) => x.GrossAmount).ToString(CommonSettings.strCurencyFormat);
			lblDisTotal.Text = entTransactions.Sum((Transaction x) => x.DiscountAmount).ToString(CommonSettings.strCurencyFormat);
			lblNetTotal.Text = entTransactions.Sum((Transaction x) => x.NETAmount).ToString(CommonSettings.strCurencyFormat);
			lblTotalTax.Text = entTransactions.Sum((Transaction x) => x.VatAmount).ToString(CommonSettings.strCurencyFormat);
			lblGrandTotal.Text = (lblNetTotal.Text.ToDecimal() + txtAddAmount.Value + txtFreight.Value - txtDisAmount.Value).ToString(CommonSettings.strCurencyFormat);
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

		private void SaveSubMethod(PurchaseInvoiceHdr entPurHdr)
		{
			if (AshproCommon.InsertToDatabase(entPurDtls.Cast<object>().ToList(), "PurInvSettings"))
			{
				POSHelper.TranLogMethod(entPurHdr.TotalAmnt, 0m, cmbPurAcct.SelectedValue.ToInt32(), cmbPartyAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurHdr.PurInvNo, "Purchase Voucher", Settings.Default.intLogOnId, "Main", entPurHdr.InvType, AniHelper.GetDate(dtmDate.Value));
				POSHelper.TranLogMethod(0m, entPurHdr.TotalAmnt, cmbPartyAcct.SelectedValue.ToInt32(), cmbPurAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurHdr.PurInvNo, "Purchase Voucher", Settings.Default.intLogOnId, "Sub", entPurHdr.InvType, AniHelper.GetDate(dtmDate.Value));
				if (CommonSettings.blnTaxEnabled && entPurHdr.TotalTax > 0m)
				{
					int num = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName = 'TAX RECEIVABLE'");
					POSHelper.TranLogMethod(0m, entPurHdr.TotalTax, cmbPurAcct.SelectedValue.ToInt32(), num, iMasterId.ToInt32(), entPurHdr.PurInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Main", "TAX RECEIVABLE", AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(entPurHdr.TotalTax, 0m, num, cmbPurAcct.SelectedValue.ToInt32(), iMasterId.ToInt32(), entPurHdr.PurInvNo, "Journal Voucher", Settings.Default.intLogOnId, "Sub", "TAX RECEIVABLE", AniHelper.GetDate(dtmDate.Value));
				}
				List<POSDetails> pOSDetailsFrmTrans = POSHelper.GetPOSDetailsFrmTrans(entTransactions);
				POSHelper.SaveStockTranLoagMethod(pOSDetailsFrmTrans, iMasterId.ToInt32(), entPurHdr.PurInvNo.ToInt32(), "Purchase Voucher", entPurHdr.InvType, cmbPartyAcct.SelectedValue.ToInt32(), "Purchase Voucher", Settings.Default.intLogOnId, null, isIn: true, AniHelper.GetDate(dtmDate.Value));
				decimal value = lblPaidAmount.Value;
				if (value > 0m)
				{
					int num2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Payment Voucher' and VchrName='Cash Payment'", isVoucherNo: true);
					POSHelper.VoucherTranLog(num2, dtmDate.Value.Date, value, "Payment Voucher", "Cash Paid On Purchase", iCashAcctId, cmbPartyAcct.SelectedValue.ToInt32(), Settings.Default.intLogOnId, Settings.Default.intLogOnId, "Cash Payment");
					int iVoucherID = AshproCommon.IdFindMethod("Select VchrTranId From VchrTranLog Where VchrNo='" + num2 + "' and VchrType ='Payment Voucher'", isVoucherNo: false);
					POSHelper.TranLogMethod(0m, value, iCashAcctId, cmbPartyAcct.SelectedValue.ToInt32(), iVoucherID, num2, "Payment Voucher", Settings.Default.intLogOnId, "Main", null, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(value, 0m, cmbPartyAcct.SelectedValue.ToInt32(), iCashAcctId, iVoucherID, num2, "Payment Voucher", Settings.Default.intLogOnId, "Sub", null, AniHelper.GetDate(dtmDate.Value));
				}
			}
		}

		private void PrintMethod()
		{
			entDMaster = db.DotmatrixMasters().FirstOrDefault((DotmatrixMaster x) => x.VochrType == "Purchase Invoice" && x.isDefault);
			if (entDMaster != null)
			{
				entDSettings = new List<DotmatrixSettings>();
				entDSettings = (from x in db.DotmatrixSettings()
					where x.printMasterId == entDMaster.printMasterId
					select x).ToList();
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				entcompany = company.CopyTo<ExCompany>();
				entcompany.CompanyName = company.InstituteName;
				entcompany.TaxId = company.GSTNo;
				entPartyDtl = new PartyDetails();
				account = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountId == cmbPartyAcct.SelectedValue.ToInt32());
				entPartyDtl = POSHelper.GetParty(account);
				iPurInvId = AshproCommon.IdFindMethod("Select PurInvId From PurInvMaster Where PurInvNo='" + txtInvNo.Data + "'" + sBranchDetails);
				entTMaster = new TransactionMaster();
				entTransactions = new List<Transaction>();
				PurchaseInvoiceHdr purchaseInvoiceHdr = new PurchaseInvoiceHdr();
				purchaseInvoiceHdr = db.PurchaseInvoiceHdrs().FirstOrDefault((PurchaseInvoiceHdr x) => x.PurInvId == iPurInvId);
				entPurDtls = (from x in db.PurInvSettings()
					where x.PurInvId == iPurInvId
					select x).ToList();
				entTMaster = POSHelper.GetTMaster(purchaseInvoiceHdr);
				entTransactions = POSHelper.GetTransactionsFromPDetails(entPurDtls, db);
				PrintHelper printHelper = new PrintHelper();
				printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, null, entTMaster, isPrint: true, txtPrintCopy.Data);
				printHelper = null;
			}
			else
			{
				MessageBox.Show("There is no default print design. Please make your own design", Utility.uManufacturer);
			}
		}

		private void proLastRateandDiscFindMethod(int iPId)
		{
			lblLastRate.Value = (((from x in db.PurInvSettings()
				where x.ProId == iPId
				select x).LastOrDefault() == null) ? 0m : (from x in db.PurInvSettings()
				where x.ProId == iPId
				select x).LastOrDefault().ProRate);
			lblLastDisc.Value = (((from x in db.PurInvSettings()
				where x.ProId == iPId
				select x).LastOrDefault() == null) ? 0m : (from x in db.PurInvSettings()
				where x.ProId == iPId
				select x).LastOrDefault().Disc);
		}

		private void CalculatorMethod()
		{
			if (calcu == null)
			{
				calcu = new Calculator();
				calcu.MdiParent = base.MdiParent;
				calcu.FormClosing += Calcu_FormClosing;
				calcu.Show();
			}
			else
			{
				calcu.Activate();
				calcu.BringToFront();
			}
		}

		private void GrossAmdNetMethod()
		{
			txtGross.Text = (txtQty.Value * txtPurRate.Value).ToString(CommonSettings.strCurencyFormat);
			txtDiscAmnt.Text = ((txtDiscP.Value == 0m) ? "0" : (txtGross.Value * (txtDiscP.Value / 100m)).ToString(CommonSettings.strCurencyFormat));
			decimal purVat = (from x in db.TaxCategories()
				where x.TCategoryId == cmbTaxCategory.SelectedValue.ToInt32()
				select x).FirstOrDefault().PurVat;
			txtTaxAmnt.Text = ((purVat > 0m) ? ((txtGross.Value - txtDiscAmnt.Value) * purVat / 100m).ToString(CommonSettings.strCurencyFormat) : "0");
			lblNet.Text = (txtGross.Value - txtDiscAmnt.Value + txtTaxAmnt.Value).ToString(CommonSettings.strCurencyFormat);
		}

		private void ProductAndProPriceCategoryUpdateMethod()
		{
			ProductEntity product = ProductEntityLoadMethod();
			iProId = (from x in db.Products()
				where x.ProductName == product.ProductName
				select x).FirstOrDefault().ProductId;
			AshproCommon.DeleteFromDatabase("ProductPriceCategory", "ProductId", iProId.ToInt32());
			AshproCommon.UpdateToDatabaseObj(product, "Product", "ProductId", iProId.ToInt32());
			List<ProductPriceCategory> entpCategory = PriceCategoryLoadMethod(product);
			AniHelper.InsertToProPriceCategory(entpCategory, product.ProductId.ToInt32(), cmbGroup.SelectedValue.ToInt32());
		}

		private List<ProductPriceCategory> PriceCategoryLoadMethod(ProductEntity product)
		{
			List<ProductPriceCategory> entpCategory = new List<ProductPriceCategory>();
			db.PriceCategories().ForEach(delegate(PriceCategoryEntity x)
			{
				ProductPriceCategory item = new ProductPriceCategory
				{
					ProductId = product.ProductId.ToInt32(),
					PCategoryId = x.PCategoryId.ToInt32(),
					UnitId = cmbUnit.SelectedValue.ToInt32(),
					SPrice = txtSaleRate.Value,
					SPDisc = 0m,
					PPrice = txtPurRate.Value,
					PDisc = txtDiscP.Value
				};
				entpCategory.Add(item);
			});
			return entpCategory;
		}

		private ProductEntity ProductEntityLoadMethod()
		{
			int num = 0;
			num = AshproCommon.GetIntData_SP("spGetProductLastProductCode");
			return new ProductEntity
			{
				ProductCode = num,
				ProductName = txtProduct.Text,
				PGroupId = (from x in db.ProductGroups()
					where x.PGroupName == cmbGroup.Text
					select x).FirstOrDefault().PGroupId.ToInt32(),
				UnitId = (from x in db.Units()
					where x.UnitName == cmbUnit.Text
					select x).FirstOrDefault().UnitId.ToInt32(),
				BrandId = (from x in db.Brands()
					where x.BrandName == cmbBrand.Text
					select x).FirstOrDefault().BrandId.ToInt32(),
				PurchasePrice = txtPurRate.Value,
				SellingPrice = txtSaleRate.Value,
				MRP = txtMRP.Value,
				OpeningStock = 0m,
				OppStockDate = dtmDate.Value.Date,
				AccountId = cmbPartyAcct.SelectedValue.ToInt32(),
				GodwnId = (from x in db.Godowns()
					where x.GodownName == cmbGodown.Text
					select x).FirstOrDefault().GodwnId.toInt32(),
				SelfId = (from x in db.Shelfs()
					where x.SelfName == cmbShelf.Text
					select x).FirstOrDefault().SelfId.ToInt32(),
				TCategoryId = cmbTaxCategory.SelectedValue.ToInt32(),
				PurchaseDis = txtDiscP.Value,
				AliasCode = num.ToString(),
				AliasName = txtProduct.Text
			};
		}

		private void BindDgv()
		{
			dgDetails.DataSource = null;
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entTransactions;
			dgDetails.DataSource = bdsDetails;
		}

		private void FirstPurchase_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			estimateDate = Settings.Default.appDate;
			dtmDate.Value = estimateDate;
			db = new DBContext();
			if (CommonSettings.blnEnableMultiBranch)
			{
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
			}
			if (CommonSettings.blnEnableMultiBranch)
			{
				txtInvNo.Data = ((from x in db.PurchaseInvoiceHdrs()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1;
			}
			else
			{
				txtInvNo.Data = (db.PurchaseInvoiceHdrs().Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1;
			}
			AniHelper.FillAccountForPurchase(cmbPartyAcct);
			AniHelper.FillPurchaseAccount(cmbPurAcct);
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillGodown(cmbGodown);
			AniHelper.FillUnit(cmbUnit);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillTCategory(cmbTaxCategory);
		}

		private void FirstPurchase_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				PrintMethod();
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				base.WindowState = FormWindowState.Minimized;
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClearAll_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnAdd_Click(null, null);
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.K && e.Alt)
			{
				e.SuppressKeyPress = true;
				estimateDate = dtmDate.Value;
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnCreateAccount_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				CalculatorMethod();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void Calcu_FormClosing(object sender, FormClosingEventArgs e)
		{
			calcu = null;
		}

		private decimal ProStockFindingMethod(int pId)
		{
			return AshproCommon.ValueFindMethod("select sum(InQuantity-OutQuantity) as sumOutQty from StockTranLog where ProductId='" + pId + "' and VchrDate <='" + dtmDate.Text + "' ");
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private void txtQty_TextChanged(object sender, EventArgs e)
		{
			GrossAmdNetMethod();
		}

		private void cmbGodown_SelectedIndexChanged(object sender, EventArgs e)
		{
			AshproCommon.FillCombo(cmbShelf, "Select SelfId as Id,SelfName as Name From Self Where GodownId='" + cmbGodown.SelectedValue.ToInt32() + "'");
		}

		private void btnCreateAccount_Click(object sender, EventArgs e)
		{
			try
			{
				AccountEntity accountEntity = new AccountEntity
				{
					AGroupId = (from X in db.AccountGroup()
						where X.GroupName == "SUPPLIERS/SUNDRY CREDITORS"
						select X).FirstOrDefault().AGroupId.toInt32(),
					AccountName = txtName.Text,
					AddressOne = txtAddressOne.Text,
					AddressTwo = txtAddressTwo.Text,
					AddressThree = txtAddressThree.Text,
					PhoneOne = txtPhoneNo.Text,
					TinNo = txtGstNo.Text,
					CstNo = txtStateCode.Text,
					isActive = true,
					IsBuiltIn = false
				};
				if (AshproCommon.InsertToDatabaseObj(accountEntity, "Account"))
				{
					AniHelper.FillAccountForPurchase(cmbPartyAcct);
					cmbPartyAcct.SelectedText = accountEntity.AccountName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAccount_Click(object sender, EventArgs e)
		{
			Account account = new Account();
			account.isOpenFromAnotherForm = true;
			account.sFormName = "Supplier";
			account.accountGroupName = "SUPPLIERS/SUNDRY CREDITORS";
			if (account.ShowDialog() == DialogResult.OK)
			{
				AniHelper.FillAccountForPurchase(cmbPartyAcct);
				cmbPartyAcct.SelectedValue = account.Id;
				cmbGodown_SelectedIndexChanged(null, null);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (cmbUnit.Text != "" && !db.Units().Any((UnitEntity x) => x.UnitName == cmbUnit.Text))
			{
				UnitEntity data = new UnitEntity
				{
					UnitName = cmbUnit.Text
				};
				AshproCommon.InsertToDatabaseObj(data, "unit");
			}
			if (cmbBrand.Text != "" && !db.Brands().Any((BrandEntity x) => x.BrandName == cmbBrand.Text))
			{
				BrandEntity data2 = new BrandEntity
				{
					BrandName = cmbBrand.Text
				};
				AshproCommon.InsertToDatabaseObj(data2, "Brand");
			}
			if (cmbGroup.Text != "" && !db.ProductGroups().Any((ProductGroupEntity x) => x.PGroupName == cmbGroup.Text))
			{
				ProductGroupEntity data3 = new ProductGroupEntity
				{
					PGroupName = cmbGroup.Text,
					Under = "GENERAL"
				};
				AshproCommon.InsertToDatabaseObj(data3, "ProductGroup");
			}
			if (cmbGodown.Text != "" && !db.Godowns().Any((GodownEntity x) => x.GodownName == cmbGodown.Text))
			{
				GodownEntity data4 = new GodownEntity
				{
					GodownName = cmbGodown.Text
				};
				AshproCommon.InsertToDatabaseObj(data4, "Godown");
			}
			if (cmbShelf.Text != "" && !db.Shelfs().Any((SelfEntity x) => x.SelfName == cmbShelf.Text && x.GodownId == cmbGodown.SelectedValue.ToInt32()))
			{
				SelfEntity selfEntity = new SelfEntity();
				selfEntity.GodownId = AshproCommon.IdFindMethod("Select GodwnId From Godown Where GodownName='" + cmbGodown.Text + "'");
				selfEntity.SelfName = cmbShelf.Text;
				SelfEntity data5 = selfEntity;
				AshproCommon.InsertToDatabaseObj(data5, "Self");
			}
			bool flag;
			if ((!(flag = txtProduct.Text == "")) ? ((object)flag) : (flag & (AshproCommon.IdFindMethod("Select Count(*) From Product where ProductName ='" + txtProduct.Text.ToUpper() + "' ") == 1)))
			{
				return;
			}
			if (!iProId.HasValue)
			{
				if (db.Products().Count((ProductEntity x) => x.ProductName == txtProduct.Text) == 0)
				{
					ProductEntity productEntity = ProductEntityLoadMethod();
					AshproCommon.InsertToDatabaseObj(productEntity, "Product");
					List<ProductPriceCategory> entpCategory = PriceCategoryLoadMethod(productEntity);
					AniHelper.InsertToProPriceCategory(entpCategory, productEntity.ProductId.ToInt32(), cmbGroup.SelectedValue.ToInt32());
				}
				else
				{
					ProductAndProPriceCategoryUpdateMethod();
				}
				entTransaction = new Transaction
				{
					SL = dgDetails.Rows.Count + 1,
					ProductCode = (from x in db.Products()
						where x.ProductName == txtProduct.Text
						select x).FirstOrDefault().ProductCode.ToInt32(),
					ProductName = txtProduct.Text,
					Quantity = txtQty.Value,
					Unit = cmbUnit.Text,
					Rate = txtPurRate.Value,
					Discount = txtDiscP.Value,
					Vat = (from x in db.TaxCategories()
						where x.TCategoryId == cmbTaxCategory.SelectedValue.ToInt32()
						select x).FirstOrDefault().PurVat
				};
				POSHelper.GetTransactionsAfterEdit(entTransaction);
				entTransactions.Add(entTransaction);
				BindDgv();
			}
			else
			{
				ProductAndProPriceCategoryUpdateMethod();
				entTransaction.ProductCode = (from x in db.Products()
					where x.ProductName == txtProduct.Text
					select x).FirstOrDefault().ProductCode.ToInt32();
				entTransaction.ProductName = txtProduct.Text;
				entTransaction.Quantity = txtQty.Value;
				entTransaction.Unit = cmbUnit.Text;
				entTransaction.Rate = txtPurRate.Value;
				entTransaction.Discount = txtDiscP.Value;
				POSHelper.GetTransactionsAfterEdit(entTransaction);
				dgDetails.Refresh();
			}
			TotalFindMethod();
			btnClear_Click(null, null);
		}

		private void btnClearAll_Click(object sender, EventArgs e)
		{
			dgDetails.DataSource = null;
			AniHelper.ClearMethod(this);
			if (CommonSettings.blnEnableMultiBranch)
			{
				txtInvNo.Data = ((from x in db.PurchaseInvoiceHdrs()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1;
			}
			else
			{
				txtInvNo.Data = (db.PurchaseInvoiceHdrs().Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1;
			}
			cmbPartyAcct.Focus();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.PanelClearmethod(pnlProduct);
			iProId = null;
			txtProduct.Focus();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				return;
			}
			int num = 1;
			if (entTransactions.Count == 0)
			{
				MessageBox.Show("No records to save!", Utility.uManufacturer);
				return;
			}
			entPurDtls = new List<PurchaseInvoiceDtl>();
			entPurDtls = POSHelper.GetPDetailsFrmTransaction(entTransactions, cmbPartyAcct.SelectedValue.ToInt32(), dtmDate.Value.Date);
			num = ((!CommonSettings.blnEnableMultiBranch) ? ((db.PurchaseInvoiceHdrs().Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1) : (((from x in db.PurchaseInvoiceHdrs()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((PurchaseInvoiceHdr x) => x.PurInvNo) + 1) ?? 1));
			PurchaseInvoiceHdr entPurHdr = new PurchaseInvoiceHdr
			{
				PurInvId = null,
				PurInvNo = num,
				PurDate = dtmDate.Value.Date,
				PurActId = cmbPurAcct.SelectedValue.ToInt32(),
				PartyAccId = cmbPartyAcct.SelectedValue.ToInt32(),
				InvType = CommonSettings.strDefaultPurchaseType,
				PriceCategoryId = db.PriceCategories().FirstOrDefault().PCategoryId.toInt32(),
				AdditionalAmnt = txtAddAmount.Value,
				Frieght = txtFreight.Value,
				TotalDiscount = lblDisTotal.Text.ToDecimal(),
				BillDisc = txtDisAmount.Value,
				TotalTax = lblTotalTax.Value,
				TotalAmnt = lblGrandTotal.Value,
				JEId = Settings.Default.intLogOnId,
				EntryDate = DateTime.Now.Date,
				SGSTAmount = entTransactions.Sum((Transaction x) => x.SGstAmount),
				CGSTAmount = entTransactions.Sum((Transaction x) => x.CGstAmount),
				IGSTAmount = entTransactions.Sum((Transaction x) => x.IGstAmount),
				Remarks = txtRemarks.Text,
				RefInvNo = txtRefNo.Text,
				Taxable = entTransactions.Sum((Transaction x) => x.Taxable)
			};
			if (AshproCommon.InsertToDatabaseObj(entPurHdr, "PurInvMaster"))
			{
				iMasterId = db.PurchaseInvoiceHdrs().FirstOrDefault((PurchaseInvoiceHdr x) => x.PurInvNo == entPurHdr.PurInvNo).PurInvId;
				entPurDtls.ForEach(delegate(PurchaseInvoiceDtl x)
				{
					x.PurInvId = iMasterId;
				});
				SaveSubMethod(entPurHdr);
				if (chkPrntWlSave.Checked)
				{
					PrintMethod();
				}
			}
		}

		private void txtPurRate_TextChanged(object sender, EventArgs e)
		{
			GrossAmdNetMethod();
		}

		private void txtDiscP_TextChanged(object sender, EventArgs e)
		{
			GrossAmdNetMethod();
		}

		private void txtTaxP_TextChanged(object sender, EventArgs e)
		{
			GrossAmdNetMethod();
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbPartyAcct.Text != "CASH ACCOUNT")
			{
				AccountEntity objectDetails = AshproCommon.GetObjectDetails<AccountEntity>("Select * From Account Where AccountId='" + cmbPartyAcct.SelectedValue.ToInt32() + "'");
				txtName.Text = objectDetails.AccountName;
				txtAddressOne.Text = objectDetails.AddressOne;
				txtAddressTwo.Text = objectDetails.AddressTwo;
				txtAddressThree.Text = objectDetails.AddressThree;
				txtPhoneNo.Text = objectDetails.PhoneOne;
				txtGstNo.Text = objectDetails.TinNo;
				txtStateCode.Text = objectDetails.CstNo;
			}
		}

		private void txtDis_Validated(object sender, EventArgs e)
		{
			if (txtDis.Value == 0m)
			{
				txtDisAmount.Value = 0m;
				return;
			}
			decimal value = txtDis.Value;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			decimal num2 = default(decimal);
			if (value > 0m)
			{
				num2 = value / 100m * num;
			}
			txtDisAmount.Text = num2.ToString(CommonSettings.strCurencyFormat);
			TotalFindMethod();
		}

		private void txtDisAmount_Validated(object sender, EventArgs e)
		{
			if (txtDisAmount.Text == string.Empty)
			{
				txtDis.Value = 0m;
				return;
			}
			decimal value = txtDisAmount.Value;
			decimal num = entTransactions.Sum((Transaction x) => x.NETAmount);
			decimal num2 = default(decimal);
			if (value > 0m && num > 0m)
			{
				num2 = value / num * 100m;
			}
			txtDis.Text = num2.ToString(CommonSettings.strCurencyFormat);
			TotalFindMethod();
		}

		private void txtAddAmount_Validated(object sender, EventArgs e)
		{
			TotalFindMethod();
		}

		private void btnPayment_Click(object sender, EventArgs e)
		{
			int num = AshproCommon.IdFindMethod("Select AGroupId From Account Where AccountId = '" + cmbPartyAcct.SelectedValue?.ToString() + "'", isVoucherNo: false);
			int num2 = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName ='CASH IN HAND'", isVoucherNo: false);
			if (num == num2)
			{
				MessageBox.Show("You don't want to make payment in the case of Cash Purchase", Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			POSCashPayment pOSCashPayment = new POSCashPayment();
			pOSCashPayment.lblInvoiceAmount.Value = entTransactions.Sum((Transaction x) => x.NETAmount);
			if (pOSCashPayment.ShowDialog() == DialogResult.OK)
			{
				lblPaidAmount.Text = (pOSCashPayment.txtCashPaid.Value + pOSCashPayment.txtCardPaid.Value).ToString(CommonSettings.strCurencyFormat);
				lblBalance.Text = pOSCashPayment.lblBalanceAmount.Value.ToString(CommonSettings.strCurencyFormat);
				iCashAcctId = pOSCashPayment.cmbCashAccount.SelectedValue.ToInt32();
			}
		}

		private void btnBarcode_Click(object sender, EventArgs e)
		{
			ProductBarcode productBarcode = new ProductBarcode();
			productBarcode.printBarcode(POSHelper.GetBarcodeDataFromTransaction(entTransactions), txtPrintCopy.Data);
		}

		private void txtProduct_Validated(object sender, EventArgs e)
		{
			try
			{
				ProductEntity objectWithParameter_SP = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProduct", new
				{
					ProductName = txtName.Text
				});
				if (objectWithParameter_SP != null)
				{
					txtPurRate.Value = objectWithParameter_SP.PurchasePrice;
					txtSaleRate.Value = objectWithParameter_SP.SellingPrice;
					txtMRP.Value = objectWithParameter_SP.MRP;
					cmbGroup.SelectedValue = objectWithParameter_SP.PGroupId;
					cmbBrand.SelectedValue = objectWithParameter_SP.BrandId;
					cmbUnit.SelectedValue = objectWithParameter_SP.UnitId;
					cmbGodown.SelectedValue = objectWithParameter_SP.GodwnId;
					cmbShelf.SelectedValue = objectWithParameter_SP.SelfId;
					cmbTaxCategory.SelectedValue = objectWithParameter_SP.TCategoryId;
					txtDiscP.Value = objectWithParameter_SP.PurchaseDis;
					lblStock.Text = ProStockFindingMethod(objectWithParameter_SP.ProductId.ToInt32()).ToString(CommonSettings.strCurencyFormat);
					proLastRateandDiscFindMethod(objectWithParameter_SP.ProductId.ToInt32());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 2698");
			}
		}

		private void cmbTaxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			GrossAmdNetMethod();
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				entTransaction = getCurrent();
				txtProduct.Text = entTransaction.ProductName;
				ProductEntity objectWithParameter_SP = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProduct", new
				{
					ProductName = txtName.Text
				});
				txtQty.Text = entTransaction.Quantity.ToString(CommonSettings.strCurencyFormat);
				cmbUnit.Text = entTransaction.Unit;
				txtPurRate.Text = entTransaction.Rate.ToString(CommonSettings.strCurencyFormat);
				txtSaleRate.Text = objectWithParameter_SP.SellingPrice.ToString(CommonSettings.strCurencyFormat);
				txtMRP.Text = objectWithParameter_SP.MRP.ToString(CommonSettings.strCurencyFormat);
				cmbGroup.SelectedValue = objectWithParameter_SP.PGroupId;
				cmbBrand.SelectedValue = objectWithParameter_SP.BrandId;
				cmbUnit.SelectedValue = objectWithParameter_SP.UnitId;
				cmbGodown.SelectedValue = objectWithParameter_SP.GodwnId;
				cmbShelf.SelectedValue = objectWithParameter_SP.SelfId;
				cmbTaxCategory.SelectedValue = objectWithParameter_SP.TCategoryId;
				GrossAmdNetMethod();
			}
		}

		private void dgDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				int iCount = 1;
				Transaction current = getCurrent();
				entTransactions.Remove(current);
				entTransactions.ForEach(delegate(Transaction x)
				{
					x.SL = iCount;
					iCount++;
				});
				BindDgv();
				TotalFindMethod();
			}
		}

		private void cmbShelf_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAdd_Click(null, null);
			}
		}

		private void dgDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstPurchase));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.dgDetails = new AshUsercontrol.AshDataGridview();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.chkPrntWlSave = new System.Windows.Forms.CheckBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnDate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAccount = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label48 = new System.Windows.Forms.Label();
            this.txtInvNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtStateCode = new AshUsercontrol.AshTextBoxNormal();
            this.label32 = new System.Windows.Forms.Label();
            this.txtRefNo = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGstNo = new AshUsercontrol.AshTextBoxNormal();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPhoneNo = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddressThree = new AshUsercontrol.AshTextBoxNormal();
            this.label53 = new System.Windows.Forms.Label();
            this.txtAddressTwo = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddressOne = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.cmbPartyAcct = new AshUsercontrol.AshComboBox();
            this.cmbPurAcct = new AshUsercontrol.AshComboBox();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.cmbShelf = new AshUsercontrol.AshComboBox();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.cmbGodown = new AshUsercontrol.AshComboBox();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.cmbTaxCategory = new AshUsercontrol.AshComboBox();
            this.cmbUnit = new AshUsercontrol.AshComboBox();
            this.txtMRP = new AshUsercontrol.AshTextBoxNormal();
            this.txtGross = new AshUsercontrol.AshTextBoxNormal();
            this.label50 = new System.Windows.Forms.Label();
            this.txtSaleRate = new AshUsercontrol.AshTextBoxNormal();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTaxAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.txtDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPurRate = new AshUsercontrol.AshTextBoxNormal();
            this.txtDiscP = new AshUsercontrol.AshTextBoxNormal();
            this.label13 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtQty = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProduct = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxtMenuFrstpur = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToSalesReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblBalance = new AshUsercontrol.AshNumericLabel();
            this.lblPaidAmount = new AshUsercontrol.AshNumericLabel();
            this.txtPrintCopy = new AshUsercontrol.AshTextBoxNormal();
            this.lblLastDisc = new AshUsercontrol.AshNumericLabel();
            this.lblUnitQty = new AshUsercontrol.AshNumericLabel();
            this.lblLastRate = new AshUsercontrol.AshNumericLabel();
            this.lblNet = new AshUsercontrol.AshNumericLabel();
            this.lblStock = new AshUsercontrol.AshNumericLabel();
            this.lblTotalQty = new AshUsercontrol.AshNumericLabel();
            this.lblGrsTotal = new AshUsercontrol.AshNumericLabel();
            this.lblDisTotal = new AshUsercontrol.AshNumericLabel();
            this.lblNetTotal = new AshUsercontrol.AshNumericLabel();
            this.lblTotalTax = new AshUsercontrol.AshNumericLabel();
            this.lblGrandTotal = new AshUsercontrol.AshNumericLabel();
            this.txtAddAmount = new AshUsercontrol.AshTextBoxNormal();
            this.txtDisAmount = new AshUsercontrol.AshTextBoxNormal();
            this.txtFreight = new AshUsercontrol.AshTextBoxNormal();
            this.txtDis = new AshUsercontrol.AshTextBoxNormal();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.pnlCustomer.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.ctxtMenuFrstpur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            resources.ApplyResources(this.pnlDetails, "pnlDetails");
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.dgDetails);
            this.pnlDetails.Name = "pnlDetails";
            this.toolTip1.SetToolTip(this.pnlDetails, resources.GetString("pnlDetails.ToolTip"));
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
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
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.EnterKeyNavigation = false;
            this.dgDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.RowTemplate.Height = 25;
            this.toolTip1.SetToolTip(this.dgDetails, resources.GetString("dgDetails.ToolTip"));
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            this.dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgDetails_DataError);
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
            this.col_ProCode.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.col_ProCode, "col_ProCode");
            this.col_ProCode.Name = "col_ProCode";
            this.col_ProCode.ReadOnly = true;
            // 
            // col_ProName
            // 
            this.col_ProName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ProName.DataPropertyName = "ProductName";
            resources.ApplyResources(this.col_ProName, "col_ProName");
            this.col_ProName.Name = "col_ProName";
            this.col_ProName.ReadOnly = true;
            // 
            // col_HSN
            // 
            this.col_HSN.DataPropertyName = "Hsn";
            resources.ApplyResources(this.col_HSN, "col_HSN");
            this.col_HSN.Name = "col_HSN";
            this.col_HSN.ReadOnly = true;
            // 
            // col_Qty
            // 
            this.col_Qty.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Qty.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.col_Qty, "col_Qty");
            this.col_Qty.Name = "col_Qty";
            this.col_Qty.ReadOnly = true;
            // 
            // col_Unit
            // 
            this.col_Unit.DataPropertyName = "Unit";
            resources.ApplyResources(this.col_Unit, "col_Unit");
            this.col_Unit.Name = "col_Unit";
            this.col_Unit.ReadOnly = true;
            // 
            // col_Rate
            // 
            this.col_Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Rate.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.col_Rate, "col_Rate");
            this.col_Rate.Name = "col_Rate";
            this.col_Rate.ReadOnly = true;
            // 
            // col_Gross
            // 
            this.col_Gross.DataPropertyName = "GrossAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Gross.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_Gross, "col_Gross");
            this.col_Gross.Name = "col_Gross";
            this.col_Gross.ReadOnly = true;
            // 
            // col_Dis
            // 
            this.col_Dis.DataPropertyName = "Discount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Dis.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.col_Dis, "col_Dis");
            this.col_Dis.Name = "col_Dis";
            this.col_Dis.ReadOnly = true;
            // 
            // col_Discount
            // 
            this.col_Discount.DataPropertyName = "DiscountAmount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Discount.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.col_Discount, "col_Discount");
            this.col_Discount.Name = "col_Discount";
            this.col_Discount.ReadOnly = true;
            // 
            // col_Taxable
            // 
            this.col_Taxable.DataPropertyName = "Taxable";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Taxable.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.col_Taxable, "col_Taxable");
            this.col_Taxable.Name = "col_Taxable";
            this.col_Taxable.ReadOnly = true;
            // 
            // col_Vat
            // 
            this.col_Vat.DataPropertyName = "Vat";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_Vat.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.col_Vat, "col_Vat");
            this.col_Vat.Name = "col_Vat";
            this.col_Vat.ReadOnly = true;
            // 
            // col_VatAmnt
            // 
            this.col_VatAmnt.DataPropertyName = "VatAmount";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.col_VatAmnt, "col_VatAmnt");
            this.col_VatAmnt.Name = "col_VatAmnt";
            this.col_VatAmnt.ReadOnly = true;
            // 
            // col_GST
            // 
            this.col_GST.DataPropertyName = "Gst";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_GST.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.col_GST, "col_GST");
            this.col_GST.Name = "col_GST";
            this.col_GST.ReadOnly = true;
            // 
            // col_GSTAmnt
            // 
            this.col_GSTAmnt.DataPropertyName = "GstAmount";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.col_GSTAmnt, "col_GSTAmnt");
            this.col_GSTAmnt.Name = "col_GSTAmnt";
            this.col_GSTAmnt.ReadOnly = true;
            // 
            // col_SGST
            // 
            this.col_SGST.DataPropertyName = "Sgst";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_SGST.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.col_SGST, "col_SGST");
            this.col_SGST.Name = "col_SGST";
            this.col_SGST.ReadOnly = true;
            // 
            // col_SGSTAmnt
            // 
            this.col_SGSTAmnt.DataPropertyName = "SGstAmount";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.col_SGSTAmnt, "col_SGSTAmnt");
            this.col_SGSTAmnt.Name = "col_SGSTAmnt";
            this.col_SGSTAmnt.ReadOnly = true;
            // 
            // col_CGST
            // 
            this.col_CGST.DataPropertyName = "CGst";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CGST.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.col_CGST, "col_CGST");
            this.col_CGST.Name = "col_CGST";
            this.col_CGST.ReadOnly = true;
            // 
            // col_CGSTAmnt
            // 
            this.col_CGSTAmnt.DataPropertyName = "CGstAmount";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.col_CGSTAmnt, "col_CGSTAmnt");
            this.col_CGSTAmnt.Name = "col_CGSTAmnt";
            this.col_CGSTAmnt.ReadOnly = true;
            // 
            // col_IGST
            // 
            this.col_IGST.DataPropertyName = "IGst";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_IGST.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.col_IGST, "col_IGST");
            this.col_IGST.Name = "col_IGST";
            this.col_IGST.ReadOnly = true;
            // 
            // col_IGSTAmnt
            // 
            this.col_IGSTAmnt.DataPropertyName = "IGstAmount";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.col_IGSTAmnt, "col_IGSTAmnt");
            this.col_IGSTAmnt.Name = "col_IGSTAmnt";
            this.col_IGSTAmnt.ReadOnly = true;
            // 
            // col_NetAmnt
            // 
            this.col_NetAmnt.DataPropertyName = "NETAmount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.col_NetAmnt, "col_NetAmnt");
            this.col_NetAmnt.Name = "col_NetAmnt";
            this.col_NetAmnt.ReadOnly = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClearAll
            // 
            resources.ApplyResources(this.btnClearAll, "btnClearAll");
            this.btnClearAll.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClearAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClearAll.ForeColor = System.Drawing.Color.White;
            this.btnClearAll.Name = "btnClearAll";
            this.toolTip1.SetToolTip(this.btnClearAll, resources.GetString("btnClearAll.ToolTip"));
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.toolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            this.toolTip1.SetToolTip(this.label36, resources.GetString("label36.ToolTip"));
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            this.toolTip1.SetToolTip(this.label35, resources.GetString("label35.ToolTip"));
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            this.toolTip1.SetToolTip(this.label34, resources.GetString("label34.ToolTip"));
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            this.toolTip1.SetToolTip(this.label33, resources.GetString("label33.ToolTip"));
            // 
            // chkPrntWlSave
            // 
            resources.ApplyResources(this.chkPrntWlSave, "chkPrntWlSave");
            this.chkPrntWlSave.Checked = true;
            this.chkPrntWlSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrntWlSave.Name = "chkPrntWlSave";
            this.toolTip1.SetToolTip(this.chkPrntWlSave, resources.GetString("chkPrntWlSave.ToolTip"));
            this.chkPrntWlSave.UseVisualStyleBackColor = true;
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.Name = "label46";
            this.toolTip1.SetToolTip(this.label46, resources.GetString("label46.ToolTip"));
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            this.toolTip1.SetToolTip(this.label29, resources.GetString("label29.ToolTip"));
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            this.toolTip1.SetToolTip(this.label27, resources.GetString("label27.ToolTip"));
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            this.toolTip1.SetToolTip(this.label25, resources.GetString("label25.ToolTip"));
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.btnDate.TabStop = false;
            this.toolTip1.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // btnAccount
            // 
            resources.ApplyResources(this.btnAccount, "btnAccount");
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.TabStop = false;
            this.toolTip1.SetToolTip(this.btnAccount, resources.GetString("btnAccount.ToolTip"));
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // pnlCustomer
            // 
            resources.ApplyResources(this.pnlCustomer, "pnlCustomer");
            this.pnlCustomer.BackColor = System.Drawing.Color.White;
            this.pnlCustomer.Controls.Add(this.txtRemarks);
            this.pnlCustomer.Controls.Add(this.label48);
            this.pnlCustomer.Controls.Add(this.txtInvNo);
            this.pnlCustomer.Controls.Add(this.txtStateCode);
            this.pnlCustomer.Controls.Add(this.label32);
            this.pnlCustomer.Controls.Add(this.txtRefNo);
            this.pnlCustomer.Controls.Add(this.label5);
            this.pnlCustomer.Controls.Add(this.txtGstNo);
            this.pnlCustomer.Controls.Add(this.label24);
            this.pnlCustomer.Controls.Add(this.txtPhoneNo);
            this.pnlCustomer.Controls.Add(this.label6);
            this.pnlCustomer.Controls.Add(this.txtAddressThree);
            this.pnlCustomer.Controls.Add(this.label53);
            this.pnlCustomer.Controls.Add(this.txtAddressTwo);
            this.pnlCustomer.Controls.Add(this.label9);
            this.pnlCustomer.Controls.Add(this.txtAddressOne);
            this.pnlCustomer.Controls.Add(this.label8);
            this.pnlCustomer.Controls.Add(this.txtName);
            this.pnlCustomer.Controls.Add(this.label7);
            this.pnlCustomer.Controls.Add(this.btnCreateAccount);
            this.pnlCustomer.Controls.Add(this.dtmDate);
            this.pnlCustomer.Controls.Add(this.cmbPartyAcct);
            this.pnlCustomer.Controls.Add(this.cmbPurAcct);
            this.pnlCustomer.Controls.Add(this.label1);
            this.pnlCustomer.Controls.Add(this.label3);
            this.pnlCustomer.Controls.Add(this.label2);
            this.pnlCustomer.Controls.Add(this.label10);
            this.pnlCustomer.Controls.Add(this.btnDate);
            this.pnlCustomer.Controls.Add(this.btnAccount);
            this.pnlCustomer.Name = "pnlCustomer";
            this.toolTip1.SetToolTip(this.pnlCustomer, resources.GetString("pnlCustomer.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtRemarks, resources.GetString("txtRemarks.ToolTip"));
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.Name = "label48";
            this.toolTip1.SetToolTip(this.label48, resources.GetString("label48.ToolTip"));
            // 
            // txtInvNo
            // 
            resources.ApplyResources(this.txtInvNo, "txtInvNo");
            this.txtInvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvNo.Data = 0;
            this.txtInvNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtInvNo.Format = null;
            this.txtInvNo.isAllowNegative = false;
            this.txtInvNo.isAllowSpecialChar = false;
            this.txtInvNo.isNumberOnly = true;
            this.txtInvNo.isNumeric = false;
            this.txtInvNo.LeaveColor = System.Drawing.Color.White;
            this.txtInvNo.Name = "txtInvNo";
            this.toolTip1.SetToolTip(this.txtInvNo, resources.GetString("txtInvNo.ToolTip"));
            this.txtInvNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtStateCode
            // 
            resources.ApplyResources(this.txtStateCode, "txtStateCode");
            this.txtStateCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStateCode.Data = 0;
            this.txtStateCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtStateCode.Format = null;
            this.txtStateCode.isAllowNegative = false;
            this.txtStateCode.isAllowSpecialChar = false;
            this.txtStateCode.isNumberOnly = false;
            this.txtStateCode.isNumeric = false;
            this.txtStateCode.LeaveColor = System.Drawing.Color.White;
            this.txtStateCode.Name = "txtStateCode";
            this.toolTip1.SetToolTip(this.txtStateCode, resources.GetString("txtStateCode.ToolTip"));
            this.txtStateCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            this.toolTip1.SetToolTip(this.label32, resources.GetString("label32.ToolTip"));
            // 
            // txtRefNo
            // 
            resources.ApplyResources(this.txtRefNo, "txtRefNo");
            this.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRefNo.Data = 0;
            this.txtRefNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtRefNo.Format = null;
            this.txtRefNo.isAllowNegative = false;
            this.txtRefNo.isAllowSpecialChar = false;
            this.txtRefNo.isNumberOnly = false;
            this.txtRefNo.isNumeric = false;
            this.txtRefNo.LeaveColor = System.Drawing.Color.White;
            this.txtRefNo.Name = "txtRefNo";
            this.toolTip1.SetToolTip(this.txtRefNo, resources.GetString("txtRefNo.ToolTip"));
            this.txtRefNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // txtGstNo
            // 
            resources.ApplyResources(this.txtGstNo, "txtGstNo");
            this.txtGstNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGstNo.Data = 0;
            this.txtGstNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtGstNo.Format = null;
            this.txtGstNo.isAllowNegative = false;
            this.txtGstNo.isAllowSpecialChar = false;
            this.txtGstNo.isNumberOnly = false;
            this.txtGstNo.isNumeric = false;
            this.txtGstNo.LeaveColor = System.Drawing.Color.White;
            this.txtGstNo.Name = "txtGstNo";
            this.toolTip1.SetToolTip(this.txtGstNo, resources.GetString("txtGstNo.ToolTip"));
            this.txtGstNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            this.toolTip1.SetToolTip(this.label24, resources.GetString("label24.ToolTip"));
            // 
            // txtPhoneNo
            // 
            resources.ApplyResources(this.txtPhoneNo, "txtPhoneNo");
            this.txtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneNo.Data = 0;
            this.txtPhoneNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtPhoneNo.Format = null;
            this.txtPhoneNo.isAllowNegative = false;
            this.txtPhoneNo.isAllowSpecialChar = false;
            this.txtPhoneNo.isNumberOnly = false;
            this.txtPhoneNo.isNumeric = false;
            this.txtPhoneNo.LeaveColor = System.Drawing.Color.White;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.toolTip1.SetToolTip(this.txtPhoneNo, resources.GetString("txtPhoneNo.ToolTip"));
            this.txtPhoneNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // txtAddressThree
            // 
            resources.ApplyResources(this.txtAddressThree, "txtAddressThree");
            this.txtAddressThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressThree.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddressThree.Data = 0;
            this.txtAddressThree.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressThree.Format = null;
            this.txtAddressThree.isAllowNegative = false;
            this.txtAddressThree.isAllowSpecialChar = false;
            this.txtAddressThree.isNumberOnly = false;
            this.txtAddressThree.isNumeric = false;
            this.txtAddressThree.LeaveColor = System.Drawing.Color.White;
            this.txtAddressThree.Name = "txtAddressThree";
            this.toolTip1.SetToolTip(this.txtAddressThree, resources.GetString("txtAddressThree.ToolTip"));
            this.txtAddressThree.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            this.toolTip1.SetToolTip(this.label53, resources.GetString("label53.ToolTip"));
            // 
            // txtAddressTwo
            // 
            resources.ApplyResources(this.txtAddressTwo, "txtAddressTwo");
            this.txtAddressTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressTwo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddressTwo.Data = 0;
            this.txtAddressTwo.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressTwo.Format = null;
            this.txtAddressTwo.isAllowNegative = false;
            this.txtAddressTwo.isAllowSpecialChar = false;
            this.txtAddressTwo.isNumberOnly = false;
            this.txtAddressTwo.isNumeric = false;
            this.txtAddressTwo.LeaveColor = System.Drawing.Color.White;
            this.txtAddressTwo.Name = "txtAddressTwo";
            this.toolTip1.SetToolTip(this.txtAddressTwo, resources.GetString("txtAddressTwo.ToolTip"));
            this.txtAddressTwo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // txtAddressOne
            // 
            resources.ApplyResources(this.txtAddressOne, "txtAddressOne");
            this.txtAddressOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddressOne.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddressOne.Data = 0;
            this.txtAddressOne.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddressOne.Format = null;
            this.txtAddressOne.isAllowNegative = false;
            this.txtAddressOne.isAllowSpecialChar = false;
            this.txtAddressOne.isNumberOnly = false;
            this.txtAddressOne.isNumeric = false;
            this.txtAddressOne.LeaveColor = System.Drawing.Color.White;
            this.txtAddressOne.Name = "txtAddressOne";
            this.toolTip1.SetToolTip(this.txtAddressOne, resources.GetString("txtAddressOne.ToolTip"));
            this.txtAddressOne.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.toolTip1.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // btnCreateAccount
            // 
            resources.ApplyResources(this.btnCreateAccount, "btnCreateAccount");
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.TabStop = false;
            this.toolTip1.SetToolTip(this.btnCreateAccount, resources.GetString("btnCreateAccount.ToolTip"));
            this.btnCreateAccount.UseVisualStyleBackColor = true;
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip1.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // cmbPartyAcct
            // 
            resources.ApplyResources(this.cmbPartyAcct, "cmbPartyAcct");
            this.cmbPartyAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPartyAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPartyAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPartyAcct.FormattingEnabled = true;
            this.cmbPartyAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbPartyAcct.Name = "cmbPartyAcct";
            this.toolTip1.SetToolTip(this.cmbPartyAcct, resources.GetString("cmbPartyAcct.ToolTip"));
            this.cmbPartyAcct.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // cmbPurAcct
            // 
            resources.ApplyResources(this.cmbPurAcct, "cmbPurAcct");
            this.cmbPurAcct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPurAcct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPurAcct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPurAcct.FormattingEnabled = true;
            this.cmbPurAcct.LeaveColor = System.Drawing.Color.White;
            this.cmbPurAcct.Name = "cmbPurAcct";
            this.toolTip1.SetToolTip(this.cmbPurAcct, resources.GetString("cmbPurAcct.ToolTip"));
            // 
            // pnlProduct
            // 
            resources.ApplyResources(this.pnlProduct, "pnlProduct");
            this.pnlProduct.BackColor = System.Drawing.Color.White;
            this.pnlProduct.Controls.Add(this.cmbShelf);
            this.pnlProduct.Controls.Add(this.cmbGroup);
            this.pnlProduct.Controls.Add(this.cmbGodown);
            this.pnlProduct.Controls.Add(this.cmbBrand);
            this.pnlProduct.Controls.Add(this.cmbTaxCategory);
            this.pnlProduct.Controls.Add(this.cmbUnit);
            this.pnlProduct.Controls.Add(this.txtMRP);
            this.pnlProduct.Controls.Add(this.txtGross);
            this.pnlProduct.Controls.Add(this.label50);
            this.pnlProduct.Controls.Add(this.txtSaleRate);
            this.pnlProduct.Controls.Add(this.label15);
            this.pnlProduct.Controls.Add(this.txtTaxAmnt);
            this.pnlProduct.Controls.Add(this.txtDiscAmnt);
            this.pnlProduct.Controls.Add(this.label23);
            this.pnlProduct.Controls.Add(this.label19);
            this.pnlProduct.Controls.Add(this.label22);
            this.pnlProduct.Controls.Add(this.label17);
            this.pnlProduct.Controls.Add(this.txtPurRate);
            this.pnlProduct.Controls.Add(this.txtDiscP);
            this.pnlProduct.Controls.Add(this.label13);
            this.pnlProduct.Controls.Add(this.label21);
            this.pnlProduct.Controls.Add(this.label18);
            this.pnlProduct.Controls.Add(this.label20);
            this.pnlProduct.Controls.Add(this.label49);
            this.pnlProduct.Controls.Add(this.label14);
            this.pnlProduct.Controls.Add(this.label16);
            this.pnlProduct.Controls.Add(this.txtQty);
            this.pnlProduct.Controls.Add(this.label12);
            this.pnlProduct.Controls.Add(this.txtProduct);
            this.pnlProduct.Controls.Add(this.label11);
            this.pnlProduct.Name = "pnlProduct";
            this.toolTip1.SetToolTip(this.pnlProduct, resources.GetString("pnlProduct.ToolTip"));
            // 
            // cmbShelf
            // 
            resources.ApplyResources(this.cmbShelf, "cmbShelf");
            this.cmbShelf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShelf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShelf.EnterColor = System.Drawing.Color.LightPink;
            this.cmbShelf.FormattingEnabled = true;
            this.cmbShelf.LeaveColor = System.Drawing.Color.White;
            this.cmbShelf.Name = "cmbShelf";
            this.toolTip1.SetToolTip(this.cmbShelf, resources.GetString("cmbShelf.ToolTip"));
            this.cmbShelf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbShelf_KeyDown);
            // 
            // cmbGroup
            // 
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbGroup.Name = "cmbGroup";
            this.toolTip1.SetToolTip(this.cmbGroup, resources.GetString("cmbGroup.ToolTip"));
            // 
            // cmbGodown
            // 
            resources.ApplyResources(this.cmbGodown, "cmbGodown");
            this.cmbGodown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGodown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGodown.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGodown.FormattingEnabled = true;
            this.cmbGodown.LeaveColor = System.Drawing.Color.White;
            this.cmbGodown.Name = "cmbGodown";
            this.toolTip1.SetToolTip(this.cmbGodown, resources.GetString("cmbGodown.ToolTip"));
            this.cmbGodown.SelectedIndexChanged += new System.EventHandler(this.cmbGodown_SelectedIndexChanged);
            // 
            // cmbBrand
            // 
            resources.ApplyResources(this.cmbBrand, "cmbBrand");
            this.cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBrand.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.LeaveColor = System.Drawing.Color.White;
            this.cmbBrand.Name = "cmbBrand";
            this.toolTip1.SetToolTip(this.cmbBrand, resources.GetString("cmbBrand.ToolTip"));
            // 
            // cmbTaxCategory
            // 
            resources.ApplyResources(this.cmbTaxCategory, "cmbTaxCategory");
            this.cmbTaxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTaxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTaxCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbTaxCategory.FormattingEnabled = true;
            this.cmbTaxCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbTaxCategory.Name = "cmbTaxCategory";
            this.toolTip1.SetToolTip(this.cmbTaxCategory, resources.GetString("cmbTaxCategory.ToolTip"));
            this.cmbTaxCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTaxCategory_SelectedIndexChanged);
            // 
            // cmbUnit
            // 
            resources.ApplyResources(this.cmbUnit, "cmbUnit");
            this.cmbUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbUnit.Name = "cmbUnit";
            this.toolTip1.SetToolTip(this.cmbUnit, resources.GetString("cmbUnit.ToolTip"));
            // 
            // txtMRP
            // 
            resources.ApplyResources(this.txtMRP, "txtMRP");
            this.txtMRP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMRP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMRP.Data = 0;
            this.txtMRP.EnterColor = System.Drawing.Color.LightPink;
            this.txtMRP.Format = null;
            this.txtMRP.isAllowNegative = false;
            this.txtMRP.isAllowSpecialChar = false;
            this.txtMRP.isNumberOnly = false;
            this.txtMRP.isNumeric = true;
            this.txtMRP.LeaveColor = System.Drawing.Color.White;
            this.txtMRP.Name = "txtMRP";
            this.toolTip1.SetToolTip(this.txtMRP, resources.GetString("txtMRP.ToolTip"));
            this.txtMRP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGross
            // 
            resources.ApplyResources(this.txtGross, "txtGross");
            this.txtGross.BackColor = System.Drawing.Color.White;
            this.txtGross.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGross.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGross.Data = 0;
            this.txtGross.EnterColor = System.Drawing.Color.LightPink;
            this.txtGross.Format = null;
            this.txtGross.isAllowNegative = false;
            this.txtGross.isAllowSpecialChar = false;
            this.txtGross.isNumberOnly = false;
            this.txtGross.isNumeric = true;
            this.txtGross.LeaveColor = System.Drawing.Color.White;
            this.txtGross.Name = "txtGross";
            this.txtGross.ReadOnly = true;
            this.txtGross.TabStop = false;
            this.toolTip1.SetToolTip(this.txtGross, resources.GetString("txtGross.ToolTip"));
            this.txtGross.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            this.toolTip1.SetToolTip(this.label50, resources.GetString("label50.ToolTip"));
            // 
            // txtSaleRate
            // 
            resources.ApplyResources(this.txtSaleRate, "txtSaleRate");
            this.txtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaleRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaleRate.Data = 0;
            this.txtSaleRate.EnterColor = System.Drawing.Color.LightPink;
            this.txtSaleRate.Format = null;
            this.txtSaleRate.isAllowNegative = false;
            this.txtSaleRate.isAllowSpecialChar = false;
            this.txtSaleRate.isNumberOnly = false;
            this.txtSaleRate.isNumeric = true;
            this.txtSaleRate.LeaveColor = System.Drawing.Color.White;
            this.txtSaleRate.Name = "txtSaleRate";
            this.toolTip1.SetToolTip(this.txtSaleRate, resources.GetString("txtSaleRate.ToolTip"));
            this.txtSaleRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            this.toolTip1.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // txtTaxAmnt
            // 
            resources.ApplyResources(this.txtTaxAmnt, "txtTaxAmnt");
            this.txtTaxAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaxAmnt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTaxAmnt.Data = 0;
            this.txtTaxAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtTaxAmnt.Format = null;
            this.txtTaxAmnt.isAllowNegative = false;
            this.txtTaxAmnt.isAllowSpecialChar = false;
            this.txtTaxAmnt.isNumberOnly = false;
            this.txtTaxAmnt.isNumeric = true;
            this.txtTaxAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtTaxAmnt.Name = "txtTaxAmnt";
            this.txtTaxAmnt.TabStop = false;
            this.toolTip1.SetToolTip(this.txtTaxAmnt, resources.GetString("txtTaxAmnt.ToolTip"));
            this.txtTaxAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtDiscAmnt
            // 
            resources.ApplyResources(this.txtDiscAmnt, "txtDiscAmnt");
            this.txtDiscAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscAmnt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDiscAmnt.Data = 0;
            this.txtDiscAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscAmnt.Format = null;
            this.txtDiscAmnt.isAllowNegative = false;
            this.txtDiscAmnt.isAllowSpecialChar = false;
            this.txtDiscAmnt.isNumberOnly = false;
            this.txtDiscAmnt.isNumeric = true;
            this.txtDiscAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtDiscAmnt.Name = "txtDiscAmnt";
            this.txtDiscAmnt.TabStop = false;
            this.toolTip1.SetToolTip(this.txtDiscAmnt, resources.GetString("txtDiscAmnt.ToolTip"));
            this.txtDiscAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            this.toolTip1.SetToolTip(this.label23, resources.GetString("label23.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            this.toolTip1.SetToolTip(this.label19, resources.GetString("label19.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            this.toolTip1.SetToolTip(this.label22, resources.GetString("label22.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            this.toolTip1.SetToolTip(this.label17, resources.GetString("label17.ToolTip"));
            // 
            // txtPurRate
            // 
            resources.ApplyResources(this.txtPurRate, "txtPurRate");
            this.txtPurRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurRate.Data = 0;
            this.txtPurRate.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurRate.Format = null;
            this.txtPurRate.isAllowNegative = false;
            this.txtPurRate.isAllowSpecialChar = false;
            this.txtPurRate.isNumberOnly = false;
            this.txtPurRate.isNumeric = true;
            this.txtPurRate.LeaveColor = System.Drawing.Color.White;
            this.txtPurRate.Name = "txtPurRate";
            this.toolTip1.SetToolTip(this.txtPurRate, resources.GetString("txtPurRate.ToolTip"));
            this.txtPurRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurRate.TextChanged += new System.EventHandler(this.txtPurRate_TextChanged);
            // 
            // txtDiscP
            // 
            resources.ApplyResources(this.txtDiscP, "txtDiscP");
            this.txtDiscP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDiscP.Data = 0;
            this.txtDiscP.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscP.Format = null;
            this.txtDiscP.isAllowNegative = false;
            this.txtDiscP.isAllowSpecialChar = false;
            this.txtDiscP.isNumberOnly = false;
            this.txtDiscP.isNumeric = true;
            this.txtDiscP.LeaveColor = System.Drawing.Color.White;
            this.txtDiscP.Name = "txtDiscP";
            this.toolTip1.SetToolTip(this.txtDiscP, resources.GetString("txtDiscP.ToolTip"));
            this.txtDiscP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDiscP.TextChanged += new System.EventHandler(this.txtDiscP_TextChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            this.toolTip1.SetToolTip(this.label21, resources.GetString("label21.ToolTip"));
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            this.toolTip1.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            this.toolTip1.SetToolTip(this.label20, resources.GetString("label20.ToolTip"));
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            this.toolTip1.SetToolTip(this.label49, resources.GetString("label49.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip1.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip1.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // txtQty
            // 
            resources.ApplyResources(this.txtQty, "txtQty");
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Data = 0;
            this.txtQty.EnterColor = System.Drawing.Color.LightPink;
            this.txtQty.Format = null;
            this.txtQty.isAllowNegative = false;
            this.txtQty.isAllowSpecialChar = false;
            this.txtQty.isNumberOnly = false;
            this.txtQty.isNumeric = true;
            this.txtQty.LeaveColor = System.Drawing.Color.White;
            this.txtQty.Name = "txtQty";
            this.toolTip1.SetToolTip(this.txtQty, resources.GetString("txtQty.ToolTip"));
            this.txtQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // txtProduct
            // 
            resources.ApplyResources(this.txtProduct, "txtProduct");
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Data = 0;
            this.txtProduct.EnterColor = System.Drawing.Color.LightPink;
            this.txtProduct.Format = null;
            this.txtProduct.isAllowNegative = false;
            this.txtProduct.isAllowSpecialChar = false;
            this.txtProduct.isNumberOnly = false;
            this.txtProduct.isNumeric = false;
            this.txtProduct.LeaveColor = System.Drawing.Color.White;
            this.txtProduct.Name = "txtProduct";
            this.toolTip1.SetToolTip(this.txtProduct, resources.GetString("txtProduct.ToolTip"));
            this.txtProduct.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProduct.Validated += new System.EventHandler(this.txtProduct_Validated);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // label85
            // 
            resources.ApplyResources(this.label85, "label85");
            this.label85.Name = "label85";
            this.toolTip1.SetToolTip(this.label85, resources.GetString("label85.ToolTip"));
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            this.toolTip1.SetToolTip(this.label31, resources.GetString("label31.ToolTip"));
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            this.toolTip1.SetToolTip(this.label38, resources.GetString("label38.ToolTip"));
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            this.toolTip1.SetToolTip(this.label39, resources.GetString("label39.ToolTip"));
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Name = "btnAdd";
            this.toolTip1.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            this.toolTip1.SetToolTip(this.label51, resources.GetString("label51.ToolTip"));
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ctxtMenuFrstpur
            // 
            resources.ApplyResources(this.ctxtMenuFrstpur, "ctxtMenuFrstpur");
            this.ctxtMenuFrstpur.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.printToolStripMenuItem,
            this.mailToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.backToolStripMenuItem,
            this.defaultSettingsToolStripMenuItem,
            this.updateSettingsToolStripMenuItem,
            this.goToSalesReturnToolStripMenuItem,
            this.calculatorToolStripMenuItem,
            this.openNewAccountToolStripMenuItem,
            this.createNewAccountToolStripMenuItem});
            this.ctxtMenuFrstpur.Name = "ctxtMenuSlsInv";
            this.toolTip1.SetToolTip(this.ctxtMenuFrstpur, resources.GetString("ctxtMenuFrstpur.ToolTip"));
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // deleteToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            // 
            // clearToolStripMenuItem
            // 
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            // 
            // closeToolStripMenuItem
            // 
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            // 
            // mailToolStripMenuItem
            // 
            resources.ApplyResources(this.mailToolStripMenuItem, "mailToolStripMenuItem");
            this.mailToolStripMenuItem.Name = "mailToolStripMenuItem";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            // 
            // backToolStripMenuItem
            // 
            resources.ApplyResources(this.backToolStripMenuItem, "backToolStripMenuItem");
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            // 
            // defaultSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.defaultSettingsToolStripMenuItem, "defaultSettingsToolStripMenuItem");
            this.defaultSettingsToolStripMenuItem.Name = "defaultSettingsToolStripMenuItem";
            // 
            // updateSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.updateSettingsToolStripMenuItem, "updateSettingsToolStripMenuItem");
            this.updateSettingsToolStripMenuItem.Name = "updateSettingsToolStripMenuItem";
            // 
            // goToSalesReturnToolStripMenuItem
            // 
            resources.ApplyResources(this.goToSalesReturnToolStripMenuItem, "goToSalesReturnToolStripMenuItem");
            this.goToSalesReturnToolStripMenuItem.Name = "goToSalesReturnToolStripMenuItem";
            // 
            // calculatorToolStripMenuItem
            // 
            resources.ApplyResources(this.calculatorToolStripMenuItem, "calculatorToolStripMenuItem");
            this.calculatorToolStripMenuItem.Name = "calculatorToolStripMenuItem";
            // 
            // openNewAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.openNewAccountToolStripMenuItem, "openNewAccountToolStripMenuItem");
            this.openNewAccountToolStripMenuItem.Name = "openNewAccountToolStripMenuItem";
            // 
            // createNewAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.createNewAccountToolStripMenuItem, "createNewAccountToolStripMenuItem");
            this.createNewAccountToolStripMenuItem.Name = "createNewAccountToolStripMenuItem";
            // 
            // btnPayment
            // 
            resources.ApplyResources(this.btnPayment, "btnPayment");
            this.btnPayment.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPayment.ForeColor = System.Drawing.Color.White;
            this.btnPayment.Name = "btnPayment";
            this.toolTip1.SetToolTip(this.btnPayment, resources.GetString("btnPayment.ToolTip"));
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnBarcode
            // 
            resources.ApplyResources(this.btnBarcode, "btnBarcode");
            this.btnBarcode.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBarcode.ForeColor = System.Drawing.Color.White;
            this.btnBarcode.Name = "btnBarcode";
            this.toolTip1.SetToolTip(this.btnBarcode, resources.GetString("btnBarcode.ToolTip"));
            this.btnBarcode.UseVisualStyleBackColor = false;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            this.toolTip1.SetToolTip(this.label26, resources.GetString("label26.ToolTip"));
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            this.toolTip1.SetToolTip(this.label28, resources.GetString("label28.ToolTip"));
            // 
            // lblBalance
            // 
            resources.ApplyResources(this.lblBalance, "lblBalance");
            this.lblBalance.Format = null;
            this.lblBalance.Name = "lblBalance";
            this.toolTip1.SetToolTip(this.lblBalance, resources.GetString("lblBalance.ToolTip"));
            this.lblBalance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblPaidAmount
            // 
            resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
            this.lblPaidAmount.Format = null;
            this.lblPaidAmount.Name = "lblPaidAmount";
            this.toolTip1.SetToolTip(this.lblPaidAmount, resources.GetString("lblPaidAmount.ToolTip"));
            this.lblPaidAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.toolTip1.SetToolTip(this.txtPrintCopy, resources.GetString("txtPrintCopy.ToolTip"));
            this.txtPrintCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLastDisc
            // 
            resources.ApplyResources(this.lblLastDisc, "lblLastDisc");
            this.lblLastDisc.ForeColor = System.Drawing.Color.Red;
            this.lblLastDisc.Format = null;
            this.lblLastDisc.Name = "lblLastDisc";
            this.toolTip1.SetToolTip(this.lblLastDisc, resources.GetString("lblLastDisc.ToolTip"));
            this.lblLastDisc.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblUnitQty
            // 
            resources.ApplyResources(this.lblUnitQty, "lblUnitQty");
            this.lblUnitQty.ForeColor = System.Drawing.Color.Magenta;
            this.lblUnitQty.Format = null;
            this.lblUnitQty.Name = "lblUnitQty";
            this.toolTip1.SetToolTip(this.lblUnitQty, resources.GetString("lblUnitQty.ToolTip"));
            this.lblUnitQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblLastRate
            // 
            resources.ApplyResources(this.lblLastRate, "lblLastRate");
            this.lblLastRate.ForeColor = System.Drawing.Color.Red;
            this.lblLastRate.Format = null;
            this.lblLastRate.Name = "lblLastRate";
            this.toolTip1.SetToolTip(this.lblLastRate, resources.GetString("lblLastRate.ToolTip"));
            this.lblLastRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblNet
            // 
            resources.ApplyResources(this.lblNet, "lblNet");
            this.lblNet.ForeColor = System.Drawing.Color.Red;
            this.lblNet.Format = null;
            this.lblNet.Name = "lblNet";
            this.toolTip1.SetToolTip(this.lblNet, resources.GetString("lblNet.ToolTip"));
            this.lblNet.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblStock
            // 
            resources.ApplyResources(this.lblStock, "lblStock");
            this.lblStock.ForeColor = System.Drawing.Color.Red;
            this.lblStock.Format = null;
            this.lblStock.Name = "lblStock";
            this.toolTip1.SetToolTip(this.lblStock, resources.GetString("lblStock.ToolTip"));
            this.lblStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblTotalQty
            // 
            resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
            this.lblTotalQty.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalQty.Format = null;
            this.lblTotalQty.Name = "lblTotalQty";
            this.toolTip1.SetToolTip(this.lblTotalQty, resources.GetString("lblTotalQty.ToolTip"));
            this.lblTotalQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblGrsTotal
            // 
            resources.ApplyResources(this.lblGrsTotal, "lblGrsTotal");
            this.lblGrsTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblGrsTotal.Format = null;
            this.lblGrsTotal.Name = "lblGrsTotal";
            this.toolTip1.SetToolTip(this.lblGrsTotal, resources.GetString("lblGrsTotal.ToolTip"));
            this.lblGrsTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblDisTotal
            // 
            resources.ApplyResources(this.lblDisTotal, "lblDisTotal");
            this.lblDisTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDisTotal.Format = null;
            this.lblDisTotal.Name = "lblDisTotal";
            this.toolTip1.SetToolTip(this.lblDisTotal, resources.GetString("lblDisTotal.ToolTip"));
            this.lblDisTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblNetTotal
            // 
            resources.ApplyResources(this.lblNetTotal, "lblNetTotal");
            this.lblNetTotal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNetTotal.Format = null;
            this.lblNetTotal.Name = "lblNetTotal";
            this.toolTip1.SetToolTip(this.lblNetTotal, resources.GetString("lblNetTotal.ToolTip"));
            this.lblNetTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblTotalTax
            // 
            resources.ApplyResources(this.lblTotalTax, "lblTotalTax");
            this.lblTotalTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalTax.ForeColor = System.Drawing.Color.Red;
            this.lblTotalTax.Format = null;
            this.lblTotalTax.Name = "lblTotalTax";
            this.toolTip1.SetToolTip(this.lblTotalTax, resources.GetString("lblTotalTax.ToolTip"));
            this.lblTotalTax.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblGrandTotal
            // 
            resources.ApplyResources(this.lblGrandTotal, "lblGrandTotal");
            this.lblGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGrandTotal.ForeColor = System.Drawing.Color.Red;
            this.lblGrandTotal.Format = null;
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.toolTip1.SetToolTip(this.lblGrandTotal, resources.GetString("lblGrandTotal.ToolTip"));
            this.lblGrandTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtAddAmount
            // 
            resources.ApplyResources(this.txtAddAmount, "txtAddAmount");
            this.txtAddAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddAmount.Data = 0;
            this.txtAddAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddAmount.Format = null;
            this.txtAddAmount.isAllowNegative = false;
            this.txtAddAmount.isAllowSpecialChar = false;
            this.txtAddAmount.isNumberOnly = false;
            this.txtAddAmount.isNumeric = true;
            this.txtAddAmount.LeaveColor = System.Drawing.Color.White;
            this.txtAddAmount.Name = "txtAddAmount";
            this.toolTip1.SetToolTip(this.txtAddAmount, resources.GetString("txtAddAmount.ToolTip"));
            this.txtAddAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtAddAmount.Validated += new System.EventHandler(this.txtAddAmount_Validated);
            // 
            // txtDisAmount
            // 
            resources.ApplyResources(this.txtDisAmount, "txtDisAmount");
            this.txtDisAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDisAmount.Data = 0;
            this.txtDisAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtDisAmount.Format = null;
            this.txtDisAmount.isAllowNegative = false;
            this.txtDisAmount.isAllowSpecialChar = false;
            this.txtDisAmount.isNumberOnly = false;
            this.txtDisAmount.isNumeric = true;
            this.txtDisAmount.LeaveColor = System.Drawing.Color.White;
            this.txtDisAmount.Name = "txtDisAmount";
            this.toolTip1.SetToolTip(this.txtDisAmount, resources.GetString("txtDisAmount.ToolTip"));
            this.txtDisAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDisAmount.Validated += new System.EventHandler(this.txtDisAmount_Validated);
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
            this.txtFreight.isNumeric = true;
            this.txtFreight.LeaveColor = System.Drawing.Color.White;
            this.txtFreight.Name = "txtFreight";
            this.toolTip1.SetToolTip(this.txtFreight, resources.GetString("txtFreight.ToolTip"));
            this.txtFreight.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtFreight.Validated += new System.EventHandler(this.txtAddAmount_Validated);
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
            this.txtDis.isNumeric = true;
            this.txtDis.LeaveColor = System.Drawing.Color.White;
            this.txtDis.Name = "txtDis";
            this.toolTip1.SetToolTip(this.txtDis, resources.GetString("txtDis.ToolTip"));
            this.txtDis.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDis.Validated += new System.EventHandler(this.txtDis_Validated);
            // 
            // FirstPurchase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.ctxtMenuFrstpur;
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.lblPaidAmount);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.txtPrintCopy);
            this.Controls.Add(this.lblLastDisc);
            this.Controls.Add(this.lblUnitQty);
            this.Controls.Add(this.lblLastRate);
            this.Controls.Add(this.label85);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.pnlProduct);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.lblGrsTotal);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.lblDisTotal);
            this.Controls.Add(this.lblNetTotal);
            this.Controls.Add(this.chkPrntWlSave);
            this.Controls.Add(this.lblTotalTax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.txtAddAmount);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtDisAmount);
            this.Controls.Add(this.txtFreight);
            this.Controls.Add(this.txtDis);
            this.Controls.Add(this.label35);
            this.KeyPreview = true;
            this.Name = "FirstPurchase";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FirstPurchase_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FirstPurchase_KeyDown);
            this.pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            this.ctxtMenuFrstpur.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
