using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class Product : MasterForm
	{
		private Calculator calcu;

		private int? iProductId;

		private string sVoucherType = "Opening Entry";

		private DBContext db = new DBContext();

		private List<ExProductUnit> entProductUnits;

		private List<ProductUnit> entOldProductUnits;

		private List<ExProductBarcodeEntity> entProductBarcodes;

		private List<ProductBarcodeEntity> entOldProductBarcodes;

		private List<ExProductPrice> entProductPrices;

		private List<ProductPriceCategory> entoldProductPrices;

		private ProductEntity productEntity;

		private bool isUnitInEditingMode = false;

		private bool isBarcodeInEditingMode = false;

		private ExProductUnit OldproductUnit;

		private List<ProductBarcodeEntity> entSizes = new List<ProductBarcodeEntity>();

		private List<ProductBarcodeEntity> entOldSizes = new List<ProductBarcodeEntity>();

		private ProductBarcodeEntity productBarcode = new ProductBarcodeEntity();

		private bool isSizeUpadate = false;

		private string sizeProName;

		private int? iSizeId;

		private IContainer components = null;

		private TabControl tabProduct;

		private TabPage tabProductDetails;

		private Label lblShelf;

		private AshComboBox cmbGodown;

		private Button btnSupplier;

		private Button btnGodown;

		private Label lblGodown;

		private AshTextBoxNormal txtReOrderStck;

		private Label lblReOrder;

		private AshDateTimePicker dtmOppStk;

		private Label lblOpDat;

		private AshTextBoxNormal txtOppStock;

		private Label lblOppStck;

		private AshTextBoxNormal txtMRP;

		private Label lblMRP;

		private AshTextBoxNormal txtSlsPrice;

		private Label lblSalePrice;

		private AshTextBoxNormal txtPurPrice;

		private Label lblPurPrice;

		private AshComboBox cmbSupplier;

		private Label lblSupplier;

		private AshComboBox cmbShelf;

		private AshComboBox cmbTaxCtgry;

		private Label lblTCategory;

		private Button btnShelf;

		private Button btnTaxCategory;

		private AshComboBox cmbBrand;

		private Label lblBrand;

		private Button btnBaseUnit;

		private Button btnBrand;

		private Button btnProductGroup;

		private AshComboBox cmbBUnit;

		private Label lblBUnit;

		private AshComboBox cmbProGrp;

		private Label lblProGrp;

		private AshTextBoxNormal txtSpecification;

		private Label lblSpec;

		private AshTextBoxNormal txtName;

		private Label lblName;

		private CheckBox cbEditProCode;

		private AshTextBoxNormal txtProCode;

		private Label lblProCode;

		private TabPage tabMultiUnit;

		private GroupBox grpUnit;

		private Panel panel2;

		private AshComboBox cmbRptUnit;

		private Label label60;

		private AshComboBox cmbPurUnit;

		private Label label59;

		private AshComboBox cmbSalesUnit;

		private Label label58;

		private Label label57;

		private Panel pnlUnit;

		private TabPage tabMultiRates;

		private Panel panel4;

		private DataGridView dgMultiRates;

		private TabPage taImage;

		private Panel panel3;

		private Button btnSaveImage;

		private Button btnRemove;

		private Button btnBrouse;

		private PictureBox pbProImage;

		private Label lblHSN;

		private AshTextBoxNormal txtHSNCode;

		private Panel panel5;

		private AshGradientPanel ashGradientPanel1;

		private Button btnProDetails;

		private Button btnProImage;

		private Button btnMultiRates;

		private Button btnMultiUnit;

		private AshGradientPanel btnSelected;

		private Button button1;

		private Button button3;

		private Button button4;

		private Button button5;

		private AshGradientPanel ashGradientPanel2;

		private BindingSource bsPCategory;

		private TabPage tabAddDetails;

		private TabPage tabMultiBarcode;

		private Button btnMultiBarcode;

		private Button btnAddDetails;

		private AshComboBox cmbProType;

		private Label lblType;

		private AshTextBoxNormal txtRemarks;

		private AshTextBoxNormal txtProInLocal;

		private AshTextBoxNormal txtAliasName;

		private AshTextBoxNormal txtAliasCode;

		private Label label26;

		private Label label25;

		private Label label24;

		private Label label23;

		public AshTextBoxNormal txtBarcode;

		private Label lblBarcode;

		private AshTextBoxNormal txtSlsDiscP;

		private Label label21;

		private AshTextBoxNormal txtSlsDiscAmnt;

		private Label label22;

		private AshTextBoxNormal txtPurDiscP;

		private Label label20;

		private AshTextBoxNormal txtPurDisAmnt;

		private Label label19;

		private CheckBox cbInclusive;

		private CheckBox cbActive;

		private CheckBox cbScale;

		private CheckBox cbMultiple;

		public AshTextBoxNormal txtLoyality;

		private Label lblLoyality;

		public AshTextBoxNormal txtOrderInList;

		private Label lblOrdrInList;

		private AshComboBox cmbUnitMulti;

		private CheckBox cbMultiUnitInclusive;

		private Label label27;

		private Button btnAddMultiUnit;

		private Label label28;

		public AshTextBoxNormal txtMultiUnitOppeningStock;

		public AshTextBoxNormal txtMultiUnitProductName;

		private Label label31;

		private Label label32;

		public AshTextBoxNormal txtMultiUnitBarcode;

		public AshTextBoxNormal txtUnitQty;

		private Label label34;

		private Label label35;

		public AshTextBoxNormal txtMultiUnitprice;

		private DataGridView dgMultiUnit;

		private AshComboBox cmbUnitMultiBarcode;

		private Label label36;

		public AshTextBoxNormal txtMultiBarcode;

		private Button btnAddMultiBarcode;

		private Label label37;

		private DataGridView dgMultiBarcode;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_MultiBarcode;

		private DataGridViewTextBoxColumn col_BarcodeType;

		private DataGridViewImageColumn col_Close;

		private Panel panel1;

		private RadioButton rbNone;

		private RadioButton rbTranslate;

		private RadioButton rbTransliterate;

		private BindingSource bsMultUnit;

		private BindingSource bsMultiBarcode;

		private BindingSource bsProduct;

		private CheckBox cbRateBasedWeight;

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

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

		private CheckBox cbDirectSearch;

		private CheckBox cbAutoGenerated;

		private CheckBox cbMultiUnitTransliterate;

		private Label label1;

		public AshTextBoxNormal txtLocalMultiUnit;

		private CheckBox cbMultiUnitTranslate;

		private AshComboBox cmbRestaurantType;

		private AshComboBox cmbPriority;

		private CheckBox cbFavaurite;

		private AshTextBoxNormal txtCookingTime;

		private Label label3;

		private Label label4;

		private Label label2;

		private TabPage tabSize;

		private Button btnSizeRemoveImage;

		private Panel panel6;

		private PictureBox pbSizeImage;

		private Button btnSizeAddImg;

		private CheckBox cbSizeTraslate;

		private CheckBox cbSizeTrasliterate;

		private Label label5;

		public AshTextBoxNormal txtSizeLocalName;

		private DataGridView dgSize;

		private Button btnAddSize;

		private Label label6;

		public AshTextBoxNormal txtSizeName;

		public AshTextBoxNormal txtSizeBarcode;

		private Label label9;

		private Label label10;

		public AshTextBoxNormal txtSizePrice;

		private Button btnSize;

		private Label label7;

		private BindingSource bdsSize;

		private DataGridViewTextBoxColumn col_Name;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Price;

		private DataGridViewImageColumn col_Close1;

		private Panel pnlMultiSize;

		private CheckBox cbPercentageBased;

		private AshTextBoxNormal txtExiCommission;

		private Label label8;

		private DataGridViewTextBoxColumn Col_MultiSize_Name;

		private DataGridViewTextBoxColumn Col_MultiSize_Barcode;

		private DataGridViewTextBoxColumn Col_MultiSize_Price;

		private DataGridViewImageColumn col_SizeClose;

		private CheckBox cbSerialNumber;

		private AshTextBoxNormal txtItemCalory;

		private Label label11;

		private DataGridViewTextBoxColumn col_MultiRate_SL;

		private DataGridViewTextBoxColumn col_MultiRates_PCategory;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_MultiRates_Unit;

		private DataGridViewTextBoxColumn col_MultiRates_SaleRate;

		private DataGridViewTextBoxColumn col_MultiRates_SalesDiscount;

		private DataGridViewTextBoxColumn col_MultiRates_PurRate;

		private DataGridViewTextBoxColumn col_MultiRates_PurDiscount;

		private DataGridViewTextBoxColumn col_MultiRatesBarcode;

		private DataGridViewImageColumn col_Delete;

		public string ValueLedger { get; set; }

		public string ValueCode { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public Product()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			FormatHelperClass.Format(this);
			ColorChange.Color(this);
			col_Price.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_MultiRates_PurDiscount.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_MultiRates_PurRate.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_MultiRates_SaleRate.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_MultiRates_SalesDiscount.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			panel3.BackColor = Settings.Default.BackColor;
			panel4.BackColor = Settings.Default.BackColor;
			grpUnit.BackColor = Settings.Default.BackColor;
			btnAddMultiUnit.BackColor = Settings.Default.BackColor;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
			base.ActiveControl = txtName;
		}

		public void DataLoadFromSearch(MasterSerchForm frm)
		{
			ProductEntity product = db.Products(new
			{
				ProductCode = frm.id
			}).FirstOrDefault();
			ProductLoadMethod(product);
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtName.Focus();
		}

		private void DeleteTransactunsMethods()
		{
			string[] obj = new string[5] { "delete from StockTranLog where ProductId='", null, null, null, null };
			int? num = iProductId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType='";
			obj[3] = sVoucherType;
			obj[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[5] { "Delete from GdwnStckTranLog where VchrId='", null, null, null, null };
			num = iProductId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType='";
			obj2[3] = sVoucherType;
			obj2[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj2));
			num = iProductId;
			AshproCommon.DeleteMethod("delete from ProductUnit where ProductId='" + num + "'");
			num = iProductId;
			AshproCommon.DeleteMethod("delete from ProductPriceCategory where ProductId='" + num + "'");
			string[] obj3 = new string[5] { "delete from ProductBarcode where FK_ProductId='", null, null, null, null };
			num = iProductId;
			obj3[1] = num.ToString();
			obj3[2] = "' and FK_BranchId='";
			obj3[3] = Settings.Default.iBranchId.ToString();
			obj3[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj3));
		}

		private void ProductTextLoadMethod(ProductEntity productEntity)
		{
			try
			{
				iProductId = productEntity.ProductId.toInt32();
				txtProCode.Data = productEntity.ProductCode.ToInt32();
				txtName.Text = productEntity.ProductName;
				txtSpecification.Text = productEntity.Specification;
				cmbProGrp.SelectedValue = productEntity.PGroupId ?? 1;
				cmbBrand.SelectedValue = productEntity.BrandId ?? 1;
				cmbBUnit.SelectedValue = productEntity.UnitId ?? 1;
				txtHSNCode.Text = productEntity.HSNNo;
				cmbTaxCtgry.SelectedValue = productEntity.TCategoryId ?? 1;
				if (productEntity.AccountId.HasValue)
				{
					int? accountId = productEntity.AccountId;
					if (!((accountId.GetValueOrDefault() == 0) & accountId.HasValue))
					{
						cmbSupplier.SelectedValue = productEntity.AccountId;
					}
				}
				cmbGodown.SelectedValue = productEntity.GodwnId ?? 1;
				cmbShelf.SelectedValue = productEntity.SelfId ?? 1;
				txtPurPrice.Value = productEntity.PurchasePrice;
				txtSlsPrice.Value = productEntity.SellingPrice;
				txtMRP.Value = productEntity.MRP;
				txtOppStock.Value = productEntity.OpeningStock;
				dtmOppStk.Value = productEntity.OppStockDate;
				txtReOrderStck.Value = productEntity.ReOrderStock;
				txtSlsDiscP.Value = productEntity.SaleDis;
				SaleDiscount();
				txtPurDiscP.Value = productEntity.PurchaseDis;
				PurDiscount();
				txtAliasCode.Text = productEntity.AliasCode;
				txtAliasName.Text = productEntity.AliasName;
				txtProInLocal.Text = productEntity.LocalName;
				cmbProType.Text = productEntity.ProductType;
				cbActive.Checked = productEntity.isActive;
				txtRemarks.Text = productEntity.Remarks;
				cmbSalesUnit.SelectedValue = productEntity.SalesUnit ?? 1;
				cmbPurUnit.SelectedValue = productEntity.PurchaseUnit ?? 1;
				cmbRptUnit.SelectedValue = productEntity.ReportUnit ?? 1;
				cbInclusive.Checked = productEntity.isInclusive.GetValueOrDefault();
				txtBarcode.Text = productEntity.Barcode;
				cbScale.Checked = productEntity.IsScale.GetValueOrDefault();
				cbMultiple.Checked = productEntity.IsAllowMultiple.GetValueOrDefault();
				txtOrderInList.Data = productEntity.ListOrder;
				txtLoyality.Data = productEntity.LoyalityPont;
				cbRateBasedWeight.Checked = productEntity.isRateBasedWeight.GetValueOrDefault();
				if (productEntity.image != null)
				{
					pbProImage.Image = AniHelper.byteArrayToImage(productEntity.image);
				}
				if (productEntity.PriorityStatus != null)
				{
					cmbPriority.Text = productEntity.PriorityStatus;
				}
				if (productEntity.RestaurantItemType != null)
				{
					cmbRestaurantType.Text = productEntity.RestaurantItemType;
				}
				txtCookingTime.Data = productEntity.CookingTime.GetValueOrDefault();
				cbFavaurite.Checked = productEntity.IsFavorite.GetValueOrDefault();
				cbPercentageBased.Checked = productEntity.IsCommisionBasedPercentage.GetValueOrDefault();
				txtExiCommission.Value = productEntity.SalesmanCommision.ToDecimal();
				cbSerialNumber.Checked = productEntity.FieldOne.ToBool();
				txtItemCalory.Data = productEntity.FieldTwo.ToInt32();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
		}

		private void EntityLoadMethod(int iPCode)
		{
			productEntity.ProductId = iProductId;
			productEntity.ProductCode = iPCode;
			productEntity.ProductName = txtName.Text;
			if (cmbRestaurantType.SelectedIndex == 1 && !productEntity.ProductName.Contains("#"))
			{
				productEntity.ProductName = "#" + txtName.Text;
			}
			productEntity.Specification = txtSpecification.Text;
			productEntity.PGroupId = cmbProGrp.SelectedValue.ToInt32();
			productEntity.UnitId = cmbBUnit.SelectedValue.ToInt32();
			productEntity.BrandId = cmbBrand.SelectedValue.ToInt32();
			productEntity.TCategoryId = cmbTaxCtgry.SelectedValue.ToInt32();
			productEntity.GodwnId = cmbGodown.SelectedValue.ToInt32();
			productEntity.SelfId = cmbShelf.SelectedValue.ToInt32();
			productEntity.AccountId = cmbSupplier.SelectedValue.ToInt32();
			productEntity.SellingPrice = txtSlsPrice.Value;
			productEntity.PurchasePrice = txtPurPrice.Value;
			productEntity.MRP = txtMRP.Value;
			productEntity.OpeningStock = txtOppStock.Value;
			productEntity.OppStockDate = dtmOppStk.Value.Date;
			productEntity.ReOrderStock = txtReOrderStck.Value;
			productEntity.HSNNo = txtHSNCode.Text;
			productEntity.PurchaseDis = txtPurDiscP.Value;
			productEntity.SaleDis = txtSlsDiscP.Value;
			productEntity.isActive = cbActive.Checked;
			productEntity.AliasCode = txtAliasCode.Text;
			productEntity.AliasName = txtAliasName.Text;
			productEntity.Remarks = txtRemarks.Text;
			productEntity.PurchaseUnit = cmbPurUnit.SelectedValue.ToInt32();
			productEntity.SalesUnit = cmbSalesUnit.SelectedValue.ToInt32();
			productEntity.ReportUnit = cmbRptUnit.SelectedValue.ToInt32();
			productEntity.image = ((pbProImage.Image == null) ? null : AniHelper.imageToByteArray(pbProImage.Image));
			productEntity.filePath = null;
			productEntity.LocalName = txtProInLocal.Text;
			productEntity.ProductType = cmbProType.Text;
			productEntity.description = string.Empty;
			productEntity.isInclusive = cbInclusive.Checked;
			productEntity.IsAllowMultiple = cbMultiple.Checked;
			productEntity.ListOrder = txtOrderInList.Data;
			productEntity.LoyalityPont = txtLoyality.Data;
			productEntity.IsScale = cbScale.Checked;
			productEntity.isRateBasedWeight = cbRateBasedWeight.Checked;
			productEntity.Barcode = ((txtBarcode.Text == string.Empty) ? txtProCode.Text : txtBarcode.Text);
			productEntity.PriorityStatus = cmbPriority.Text;
			productEntity.RestaurantItemType = cmbRestaurantType.Text;
			productEntity.CookingTime = txtCookingTime.Data;
			productEntity.IsFavorite = cbFavaurite.Checked;
			productEntity.IsCommisionBasedPercentage = cbPercentageBased.Checked;
			productEntity.SalesmanCommision = txtExiCommission.Value;
			productEntity.FieldOne = cbSerialNumber.CheckState.ToString();
			productEntity.FieldTwo = txtItemCalory.Data.ToString();
		}

		private void loadPriceCategory(string sBarcode = null, decimal? sPrice = null, bool isMultiSize = false)
		{
			try
			{
				if (!isMultiSize && !iProductId.HasValue)
				{
					entProductPrices = new List<ExProductPrice>();
				}
				foreach (PriceCategoryEntity item in db.PriceCategories())
				{
					decimal percentage = item.Percentage;
					percentage = ((percentage < 1m) ? 1m : (percentage / 100m));
					if (sPrice.HasValue)
					{
						sPrice *= (decimal?)percentage;
					}
					sBarcode = sBarcode ?? txtBarcode.Text;
					if (!entProductPrices.Any((ExProductPrice x) => x.PCategoryId == item.PCategoryId && x.Barcode == sBarcode))
					{
						ExProductPrice item2 = new ExProductPrice
						{
							PCategoryId = item.PCategoryId,
							PCategoryName = item.PCategoryName,
							SL = entProductPrices.Count + 1,
							SPrice = (sPrice ?? (txtSlsPrice.Value * percentage)),
							SPDisc = txtSlsDiscP.Value,
							PDisc = txtPurDiscP.Value,
							PPrice = txtPurPrice.Value,
							ProName = txtName.Text,
							UnitId = cmbBUnit.SelectedValue.ToInt32(),
							UnitName = cmbBUnit.Text,
							PGroupId = cmbProGrp.SelectedValue.ToInt32(),
							Barcode = sBarcode
						};
						entProductPrices.Add(item2);
					}
					else
					{
						ExProductPrice exProductPrice = entProductPrices.FirstOrDefault((ExProductPrice x) => x.PCategoryId == item.PCategoryId && x.Barcode == sBarcode);
						exProductPrice.PCategoryId = item.PCategoryId;
						exProductPrice.PCategoryName = item.PCategoryName;
						exProductPrice.SL = entProductPrices.Count + 1;
						exProductPrice.SPrice = sPrice ?? (txtSlsPrice.Value * percentage);
						exProductPrice.SPDisc = txtSlsDiscP.Value;
						exProductPrice.PPrice = txtPurPrice.Value;
						exProductPrice.PDisc = txtPurDiscP.Value;
						exProductPrice.ProName = txtName.Text;
						exProductPrice.UnitId = cmbBUnit.SelectedValue.ToInt32();
						exProductPrice.UnitName = cmbBUnit.Text;
						exProductPrice.PGroupId = cmbProGrp.SelectedValue.ToInt32();
						exProductPrice.Barcode = sBarcode;
					}
				}
				dgLoadMethodMultiRates();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dgLoadMethodMultiRates()
		{
			dgMultiRates.AutoGenerateColumns = false;
			bsPCategory.DataSource = null;
			dgMultiRates.DataSource = null;
			bsPCategory.DataSource = entProductPrices;
			dgMultiRates.DataSource = bsPCategory;
		}

		private void dgLoadMethodMultiSize()
		{
			dgSize.AutoGenerateColumns = false;
			bdsSize.DataSource = null;
			dgSize.DataSource = null;
			bdsSize.DataSource = entSizes;
			dgSize.DataSource = bdsSize;
		}

		private void ClearSize()
		{
			AshTextBoxNormal ashTextBoxNormal = txtSizeName;
			AshTextBoxNormal ashTextBoxNormal2 = txtSizeLocalName;
			string text2 = (txtSizeBarcode.Text = (sizeProName = string.Empty));
			string text5 = (ashTextBoxNormal.Text = (ashTextBoxNormal2.Text = text2));
			txtSizePrice.Value = 0m;
			pbSizeImage.Image = null;
			productBarcode = new ProductBarcodeEntity();
			iSizeId = null;
			txtSizeName.Focus();
		}

		private void AddbarcodeToList(string sBarcode, string sType, int iUnitId, decimal dSalePrice, decimal dPurPrice, string sName, string lName)
		{
			if (sBarcode == string.Empty)
			{
				return;
			}
			if (!entProductBarcodes.Any((ExProductBarcodeEntity x) => x.Barcode == sBarcode))
			{
				ExProductBarcodeEntity exProductBarcodeEntity = new ExProductBarcodeEntity();
				exProductBarcodeEntity.SL = entProductBarcodes.Count + 1;
				exProductBarcodeEntity.Barcode = sBarcode;
				exProductBarcodeEntity.FK_ProductId = iProductId;
				exProductBarcodeEntity.FK_UnitId = iUnitId;
				exProductBarcodeEntity.ProductName = txtName.Text;
				exProductBarcodeEntity.PurchasePrice = dPurPrice;
				exProductBarcodeEntity.SellingPrice = dSalePrice;
				exProductBarcodeEntity.MRP = txtMRP.Value;
				exProductBarcodeEntity.BatchName = string.Empty;
				exProductBarcodeEntity.FK_BatchId = -1;
				exProductBarcodeEntity.FK_BranchId = Settings.Default.iBranchId;
				exProductBarcodeEntity.BarcodeType = sType;
				exProductBarcodeEntity.ProductCost = dPurPrice;
				exProductBarcodeEntity.MinimuPrice = dSalePrice;
				exProductBarcodeEntity.WholesalePrice = dSalePrice;
				exProductBarcodeEntity.RetailPrice = dSalePrice;
				exProductBarcodeEntity.ProductBarcodeName = sName;
				exProductBarcodeEntity.LocalName = lName;
				entProductBarcodes.Add(exProductBarcodeEntity);
			}
			else
			{
				ExProductBarcodeEntity exProductBarcodeEntity2 = entProductBarcodes.FirstOrDefault((ExProductBarcodeEntity x) => x.Barcode == sBarcode);
				exProductBarcodeEntity2.SL = entProductBarcodes.Count + 1;
				exProductBarcodeEntity2.Barcode = sBarcode;
				exProductBarcodeEntity2.FK_ProductId = iProductId;
				exProductBarcodeEntity2.FK_UnitId = iUnitId;
				exProductBarcodeEntity2.ProductName = txtName.Text;
				exProductBarcodeEntity2.PurchasePrice = dPurPrice;
				exProductBarcodeEntity2.SellingPrice = dSalePrice;
				exProductBarcodeEntity2.MRP = txtMRP.Value;
				exProductBarcodeEntity2.BatchName = string.Empty;
				exProductBarcodeEntity2.FK_BatchId = -1;
				exProductBarcodeEntity2.FK_BranchId = Settings.Default.iBranchId;
				exProductBarcodeEntity2.BarcodeType = sType;
				exProductBarcodeEntity2.ProductCost = dPurPrice;
				exProductBarcodeEntity2.MinimuPrice = dSalePrice;
				exProductBarcodeEntity2.WholesalePrice = dSalePrice;
				exProductBarcodeEntity2.RetailPrice = dSalePrice;
				exProductBarcodeEntity2.ProductBarcodeName = sName;
				exProductBarcodeEntity2.LocalName = lName;
			}
			dgLoadMethodBarcode();
		}

		private void dgLoadMethodBarcode()
		{
			dgMultiBarcode.AutoGenerateColumns = false;
			bsMultiBarcode.DataSource = null;
			dgMultiBarcode.DataSource = null;
			bsMultiBarcode.DataSource = entProductBarcodes;
			dgMultiBarcode.DataSource = bsMultiBarcode;
		}

		private void dgLoadMethodUnit()
		{
			dgMultiUnit.AutoGenerateColumns = false;
			bsMultUnit.DataSource = null;
			dgMultiUnit.DataSource = null;
			bsMultUnit.DataSource = entProductUnits;
			dgMultiUnit.DataSource = bsMultUnit;
		}

		private void CalculatorMethod()
		{
			if (calcu == null)
			{
				calcu = new Calculator();
				calcu.MdiParent = base.MdiParent;
				calcu.FormClosing += delegate
				{
					calcu = null;
				};
				calcu.Show();
			}
			else
			{
				calcu.Activate();
				calcu.BringToFront();
			}
		}

		private void PurDiscount()
		{
			try
			{
				if (txtPurDiscP.Value == 0m)
				{
					txtPurDisAmnt.Value = 0m;
				}
				else
				{
					txtPurDisAmnt.Value = txtPurDiscP.Value / 100m * txtPurPrice.Value;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SaleDiscount()
		{
			try
			{
				if (txtSlsDiscP.Value == 0m)
				{
					txtSlsDiscAmnt.Value = 0m;
				}
				else
				{
					txtSlsDiscAmnt.Value = txtSlsDiscP.Value / 100m * txtSlsPrice.Value;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FillComboMethod()
		{
			try
			{
				AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
				AniHelper.FillProGroup(cmbProGrp);
				AniHelper.FillUnit(cmbBUnit);
				AniHelper.FillUnit(cmbUnitMulti);
				AniHelper.FillUnit(cmbUnitMultiBarcode);
				AniHelper.FillUnit(cmbPurUnit);
				AniHelper.FillUnit(cmbSalesUnit);
				AniHelper.FillUnit(cmbRptUnit);
				AniHelper.FillBrand(cmbBrand);
				AniHelper.FillTCategory(cmbTaxCtgry);
				AniHelper.FillGodown(cmbGodown);
				AshproCommon.FillCombo(cmbShelf, "Select SelfId as Id,SelfName as Name From Self Where GodownId='" + cmbGodown.SelectedValue.ToInt32() + "'");
				cmbPriority.SelectedIndex = 0;
				cmbRestaurantType.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadMethodEntity()
		{
			entProductPrices = new List<ExProductPrice>();
			entProductUnits = new List<ExProductUnit>();
			entProductBarcodes = new List<ExProductBarcodeEntity>();
			entOldProductUnits = new List<ProductUnit>();
			entOldProductBarcodes = new List<ProductBarcodeEntity>();
			entoldProductPrices = new List<ProductPriceCategory>();
		}

		private void ProductLoadMethod(ProductEntity product)
		{
			try
			{
				pbProImage.Image = null;
				ProductTextLoadMethod(product);
				List<ProductBarcodeEntity> list = new List<ProductBarcodeEntity>();
				entOldProductUnits = db.Productunits(new { product.ProductId }).ToList();
				entProductUnits = AniHelper.CopyListData<ExProductUnit>(db.Productunits(new { product.ProductId }).ToList().Cast<object>()
					.ToList());
				OldproductUnit = entProductUnits.FirstOrDefault((ExProductUnit x) => x.UnitType == "Base Unit");
				entProductUnits.Remove(OldproductUnit);
				entOldProductBarcodes = (from x in db.ProductBarcodes(new
					{
						FK_ProductId = product.ProductId,
						FK_BranchId = Settings.Default.iBranchId
					})
					where x.BarcodeType != "Multi Size"
					select x).ToList();
				list = (from x in db.ProductBarcodes(new
					{
						FK_ProductId = product.ProductId,
						FK_BranchId = Settings.Default.iBranchId
					})
					where x.BarcodeType != "Multi Size"
					select x).ToList();
				entOldSizes = db.ProductBarcodes(new
				{
					FK_ProductId = product.ProductId,
					BarcodeType = "Multi Size"
				}).ToList();
				entSizes = db.ProductBarcodes(new
				{
					FK_ProductId = product.ProductId,
					BarcodeType = "Multi Size"
				}).ToList();
				entoldProductPrices = db.ProductPriceCategories(new { product.ProductId }).ToList();
				entProductPrices = AniHelper.CopyListData<ExProductPrice>(db.ProductPriceCategories(new { product.ProductId }).Cast<object>().ToList()).ToList();
				foreach (ExProductUnit item in entProductUnits)
				{
					item.ProductName = product.ProductName;
					item.UnitName = db.Units().FirstOrDefault((UnitEntity x) => x.UnitId == item.UnitId).UnitName;
				}
				entProductBarcodes = AniHelper.CopyListData<ExProductBarcodeEntity>(list.Cast<object>().ToList());
				int num = 1;
				foreach (ExProductBarcodeEntity entProductBarcode in entProductBarcodes)
				{
					entProductBarcode.SL = num;
					num++;
				}
				num = 1;
				foreach (ExProductPrice x2 in entProductPrices)
				{
					x2.SL = num;
					x2.PCategoryName = db.PriceCategories().FirstOrDefault((PriceCategoryEntity y) => y.PCategoryId == x2.PCategoryId).PCategoryName;
					ProductBarcodeEntity productBarcodeEntity = db.ProductBarcodes(new { x2.Barcode }).FirstOrDefault();
					if (productBarcodeEntity.ProductBarcodeName != null && !(productBarcodeEntity.ProductBarcodeName == string.Empty))
					{
						x2.ProName = productBarcodeEntity.ProductBarcodeName;
					}
					else
					{
						x2.ProName = db.Productunits(new { x2.Barcode }).FirstOrDefault().ProductUnitName;
					}
					x2.Rate = x2.SPrice;
					x2.UnitName = db.Units().FirstOrDefault((UnitEntity y) => y.UnitId == x2.UnitId).UnitName;
					num++;
				}
				dgLoadMethodUnit();
				dgLoadMethodBarcode();
				dgLoadMethodMultiRates();
				dgLoadMethodMultiSize();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			if (!(txtName.Text == string.Empty) && !(txtBarcode.Text == string.Empty))
			{
				Cursor = Cursors.WaitCursor;
				try
				{
					if (!iProductId.HasValue)
					{
						if (plgDtls != null && !plgDtls.SaveData)
						{
							Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
							return;
						}
						int num = 0;
						num = AshproCommon.GetIntData_SP("spGetProductLastProductCode");
						this.productEntity = new ProductEntity();
						EntityLoadMethod(num);
						if (AshproCommon.InsertToDatabaseObj(this.productEntity, "Product"))
						{
							ProductEntity productEntity = new ProductEntity();
							productEntity = db.Products(new
							{
								ProductCode = num,
								ProductName = txtName.Text
							}).FirstOrDefault();
							List<ProductUnit> list = AniHelper.CopyListData<ProductUnit>(entProductUnits.Cast<object>().ToList());
							if (!list.Any((ProductUnit x) => x.UnitId == cmbBUnit.SelectedValue.ToInt32()))
							{
								ProductUnit item = new ProductUnit
								{
									UnitId = cmbBUnit.SelectedValue.ToInt32(),
									Barcode = txtBarcode.Text,
									IsInclusive = cbInclusive.Checked,
									OpeningStock = txtOppStock.Value,
									ProductUnitName = txtName.Text,
									SalesPrice = txtSlsPrice.Value,
									UnitQty = 1m,
									UnitType = "Base Unit"
								};
								list.Add(item);
							}
							decimal num2 = default(decimal);
							foreach (ProductUnit item4 in list)
							{
								item4.ProductId = productEntity.ProductId;
								AshproCommon.InsertToDatabaseObj(item4);
								num2 += item4.OpeningStock * item4.UnitQty;
							}
							if (num2 > 0m && productEntity.ProductType != "Service")
							{
								POSHelper.STranLogMethod(productEntity.ProductId.ToInt32(), productEntity.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, num2, 0m, productEntity.ProductId.ToInt32(), productEntity.UnitId.ToInt32(), db.Godowns().FirstOrDefault().GodwnId.toInt32(), dtmOppStk.Value.ToString(), productEntity.Barcode);
							}
							foreach (ExProductPrice entProductPrice in entProductPrices)
							{
								entProductPrice.ProductId = productEntity.ProductId;
							}
							List<ProductPriceCategory> list2 = new List<ProductPriceCategory>();
							list2 = AniHelper.CopyListData<ProductPriceCategory>(entProductPrices.Cast<object>().ToList());
							AshproCommon.InsertToDatabase(list2.Cast<object>().ToList());
							List<ProductBarcodeEntity> list3 = new List<ProductBarcodeEntity>();
							list3 = AniHelper.CopyListData<ProductBarcodeEntity>(entProductBarcodes.Cast<object>().ToList());
							foreach (ProductBarcodeEntity item5 in list3)
							{
								item5.FK_ProductId = productEntity.ProductId;
							}
							AshproCommon.InsertToDatabase(list3.Cast<object>().ToList(), "ProductBarcode");
							if (entSizes.Count > 0)
							{
								foreach (ProductBarcodeEntity entSize in entSizes)
								{
									entSize.FK_ProductId = productEntity.ProductId;
								}
								AshproCommon.InsertToDatabase(entSizes.Cast<object>().ToList(), "ProductBarcode");
							}
							Messages.SavedMessage();
						}
					}
					else
					{
						if (plgDtls != null && !plgDtls.EditData)
						{
							Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
							return;
						}
						this.productEntity = new ProductEntity();
						EntityLoadMethod(txtProCode.Data);
						if (AshproCommon.UpdateToDatabaseObj(this.productEntity, "Product", "ProductId", iProductId.ToInt32()))
						{
							List<ProductUnit> list4 = AniHelper.CopyListData<ProductUnit>(entProductUnits.Cast<object>().ToList());
							if (!list4.Any((ProductUnit x) => x.UnitId == cmbBUnit.SelectedValue.ToInt32()))
							{
								ProductUnit item2 = new ProductUnit
								{
									UnitId = cmbBUnit.SelectedValue.ToInt32(),
									Barcode = txtBarcode.Text,
									IsInclusive = cbInclusive.Checked,
									OpeningStock = txtOppStock.Value,
									ProductUnitName = txtName.Text,
									SalesPrice = txtSlsPrice.Value,
									UnitQty = 1m,
									UnitType = "Base Unit"
								};
								list4.Add(item2);
							}
							decimal num3 = default(decimal);
							foreach (ProductUnit item6 in list4)
							{
								item6.ProductId = this.productEntity.ProductId;
								num3 += item6.OpeningStock * item6.UnitQty;
							}
							AshproCommon.UpdateDatabase(list4.Cast<object>().ToList(), entOldProductUnits.Cast<object>().ToList(), "ProductUnit", "ProUnitId");
							string[] obj = new string[5] { "delete from StockTranLog where ProductId='", null, null, null, null };
							int? num4 = iProductId;
							obj[1] = num4.ToString();
							obj[2] = "' and VchrType='";
							obj[3] = sVoucherType;
							obj[4] = "'";
							AshproCommon.DeleteMethod(string.Concat(obj));
							string[] obj2 = new string[5] { "Delete from GdwnStckTranLog where VchrId='", null, null, null, null };
							num4 = iProductId;
							obj2[1] = num4.ToString();
							obj2[2] = "' and VchrType='";
							obj2[3] = sVoucherType;
							obj2[4] = "'";
							AshproCommon.DeleteMethod(string.Concat(obj2));
							if (num3 > 0m && this.productEntity.ProductType != "Service")
							{
								POSHelper.STranLogMethod(this.productEntity.ProductId.ToInt32(), this.productEntity.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, num3, 0m, this.productEntity.ProductId.ToInt32(), this.productEntity.UnitId.ToInt32(), 1, null, this.productEntity.Barcode);
							}
							foreach (ExProductPrice entProductPrice2 in entProductPrices)
							{
								entProductPrice2.ProductId = this.productEntity.ProductId;
							}
							List<ProductPriceCategory> list5 = new List<ProductPriceCategory>();
							list5 = AniHelper.CopyListData<ProductPriceCategory>(entProductPrices.Cast<object>().ToList());
							AshproCommon.UpdateDatabase(list5.Cast<object>().ToList(), entoldProductPrices.Cast<object>().ToList(), "ProductPriceCategory", "ProPrceCatId");
							List<ProductBarcodeEntity> list6 = new List<ProductBarcodeEntity>();
							list6 = AniHelper.CopyListData<ProductBarcodeEntity>(entProductBarcodes.Cast<object>().ToList());
							if (!list6.Any((ProductBarcodeEntity x) => x.Barcode == txtBarcode.Text))
							{
								ProductBarcodeEntity item3 = new ProductBarcodeEntity
								{
									FK_BranchId = Settings.Default.iBranchId,
									FK_UnitId = this.productEntity.UnitId.toInt32(),
									FK_BatchId = null,
									Barcode = txtBarcode.Text,
									BarcodeType = "Base Barcode",
									PurchasePrice = txtPurPrice.Value,
									SellingPrice = txtSlsPrice.Value,
									MRP = txtMRP.Value,
									MinimuPrice = txtSlsPrice.Value,
									RetailPrice = txtSlsPrice.Value,
									WholesalePrice = txtSlsPrice.Value,
									ProductCost = txtPurPrice.Value,
									ProductBarcodeName = txtName.Text,
									LocalName = txtProInLocal.Text
								};
								list6.Add(item3);
							}
							foreach (ProductBarcodeEntity item7 in list6)
							{
								item7.FK_ProductId = this.productEntity.ProductId;
							}
							AshproCommon.UpdateDatabase(list6.Cast<object>().ToList(), entOldProductBarcodes.Cast<object>().ToList(), "ProductBarcode", "Id");
							if (entSizes.Count > 0 || entOldSizes.Count > 0)
							{
								foreach (ProductBarcodeEntity entSize2 in entSizes)
								{
									entSize2.FK_ProductId = this.productEntity.ProductId;
								}
								AshproCommon.UpdateDatabase(entSizes.Cast<object>().ToList(), entOldSizes.Cast<object>().ToList(), "ProductBarcode", "Id");
							}
							Messages.UpdateMessage();
						}
					}
					btnClear.PerformClick();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + " 269");
				}
				Cursor = Cursors.Arrow;
			}
			else
			{
				Messages.WarningMessage();
				txtName.Focus();
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				entProductBarcodes = new List<ExProductBarcodeEntity>();
				entoldProductPrices = new List<ProductPriceCategory>();
				entOldProductBarcodes = new List<ProductBarcodeEntity>();
				entOldProductUnits = new List<ProductUnit>();
				entProductPrices = new List<ExProductPrice>();
				entProductUnits = new List<ExProductUnit>();
				entOldSizes = new List<ProductBarcodeEntity>();
				entSizes = new List<ProductBarcodeEntity>();
				AniHelper.PanelClearmethod(pnlMultiSize);
				AniHelper.PanelClearmethod(tabProductDetails);
				AniHelper.PanelClearmethod(tabAddDetails);
				AniHelper.PanelClearmethod(pnlUnit);
				dgMultiRates.DataSource = null;
				dgMultiBarcode.DataSource = null;
				dgMultiUnit.DataSource = null;
				pbProImage.Image = null;
				pbSizeImage.Image = null;
				dgSize.DataSource = null;
				txtProCode.Data = AshproCommon.GetIntData_SP("spGetProductLastProductCode");
				btnDelete.Enabled = false;
				iProductId = 0;
				iProductId = null;
				CheckBox checkBox = cbFavaurite;
				CheckBox checkBox2 = cbRateBasedWeight;
				CheckBox checkBox3 = cbScale;
				cbMultiple.Checked = false;
				checkBox3.Checked = false;
				checkBox2.Checked = false;
				checkBox.Checked = false;
				txtCookingTime.Data = 0;
				cmbRestaurantType.SelectedIndex = 0;
				cmbPriority.SelectedIndex = 2;
				cbInclusive.Checked = CommonSettings.blnTaxInclusive;
				txtName.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage())
				{
					DeleteTransactunsMethods();
					AshproCommon.DeleteFromDatabase("Product", "ProductId", iProductId.ToInt32());
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("Code", "Product Name", "Barcode");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select ProductCode as id,ProductName as name,Barcode as description From Product");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.product = this;
				masterSerchForm.TopLevel = false;
				masterSerchForm.Location = new Point(base.Width + 50, 25);
				if (!base.IsMdiChild)
				{
					TabPage tabPage = (TabPage)base.Parent;
					tabPage.Controls.Add(masterSerchForm);
				}
				else
				{
					masterSerchForm.MdiParent = base.MdiParent;
				}
				masterSerchForm.Show();
				masterSerchForm.BringToFront();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void Product_Load(object sender, EventArgs e)
		{
			try
			{
				LoadMethodEntity();
				txtProCode.Data = AshproCommon.GetIntData_SP("spGetProductLastProductCode");
				if (ValueLedger != null)
				{
					txtProCode.Text = ValueCode;
					txtName.Text = ValueLedger;
					ProductEntity productEntity = db.Products(new
					{
						ProductCode = txtProCode.Data
					}).FirstOrDefault();
					if (productEntity != null)
					{
						ProductLoadMethod(productEntity);
					}
				}
				FillComboMethod();
				cbInclusive.Checked = CommonSettings.blnTaxInclusive;
				base.ActiveControl = txtName;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnNewObjectCreate(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			switch (button.Name)
			{
			case "btnBrand":
			{
				Brand brand = new Brand();
				brand.isOpenFromAnotherForm = true;
				if (brand.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillBrand(cmbBrand);
					cmbBrand.Text = brand.brandName;
				}
				break;
			}
			case "btnSupplier":
			{
				Account account = new Account();
				account.isOpenFromAnotherForm = true;
				account.accountGroupName = "SUPPLIERS/SUNDRY CREDITORS";
				if (account.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
					cmbSupplier.SelectedValue = account.Id;
				}
				break;
			}
			case "btnShelf":
			{
				Shelfs shelfs = new Shelfs();
				shelfs.godwonName = cmbGodown.Text;
				shelfs.isOpenFromAnotherForm = true;
				if (shelfs.ShowDialog() == DialogResult.OK)
				{
					AshproCommon.FillCombo(cmbShelf, "Select SelfId as Id,SelfName as Name From Self Where GodownId='" + cmbGodown.SelectedValue.ToInt32() + "'");
					cmbShelf.Text = shelfs.shelfName;
				}
				break;
			}
			case "btnProductGroup":
			{
				ProductGroup productGroup = new ProductGroup();
				productGroup.isOpenFromAnotherForm = true;
				if (productGroup.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillProGroup(cmbProGrp);
					cmbProGrp.Text = productGroup.ProductGroupName;
				}
				break;
			}
			case "btnBaseUnit":
			{
				Unit unit = new Unit();
				unit.isOpenFromAnotherForm = true;
				if (unit.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillUnit(cmbBUnit);
					cmbBUnit.Text = unit.UnitName;
				}
				break;
			}
			case "btnTaxCategory":
			{
				TaxCategory taxCategory = new TaxCategory();
				taxCategory.isOpenFromAnotherForm = true;
				if (taxCategory.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillTCategory(cmbTaxCtgry);
					cmbTaxCtgry.Text = taxCategory.taxCategoryName;
				}
				break;
			}
			case "btnGodown":
			{
				Godown godown = new Godown();
				godown.isOpenFromAnotherForm = true;
				if (godown.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillGodown(cmbGodown);
					cmbGodown.Text = godown.godownName;
				}
				break;
			}
			}
		}

		private void btnProDetails_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabProductDetails;
			btnProDetails.BackColor = Color.White;
			btnProDetails.ForeColor = Color.Black;
			Button button = btnMultiUnit;
			Button button2 = btnMultiRates;
			Button button3 = btnProImage;
			Button button4 = btnAddDetails;
			Button button5 = btnMultiBarcode;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnMultiUnit;
			Button button7 = btnMultiRates;
			Button button8 = btnProImage;
			Button button9 = btnAddDetails;
			Button button10 = btnMultiBarcode;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnProDetails.Top;
		}

		private void btnAddUnits_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabMultiUnit;
			btnMultiUnit.BackColor = Color.White;
			btnMultiUnit.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiRates;
			Button button3 = btnProImage;
			Button button4 = btnAddDetails;
			Button button5 = btnMultiBarcode;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiRates;
			Button button8 = btnProImage;
			Button button9 = btnAddDetails;
			Button button10 = btnMultiBarcode;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnMultiUnit.Top;
		}

		private void btnPriceSettings_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabMultiRates;
			btnMultiRates.BackColor = Color.White;
			btnMultiRates.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiUnit;
			Button button3 = btnProImage;
			Button button4 = btnAddDetails;
			Button button5 = btnMultiBarcode;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiUnit;
			Button button8 = btnProImage;
			Button button9 = btnAddDetails;
			Button button10 = btnMultiBarcode;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnMultiRates.Top;
		}

		private void btnProImage_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = taImage;
			btnProImage.BackColor = Color.White;
			btnProImage.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiUnit;
			Button button3 = btnMultiRates;
			Button button4 = btnAddDetails;
			Button button5 = btnMultiBarcode;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiUnit;
			Button button8 = btnMultiRates;
			Button button9 = btnAddDetails;
			Button button10 = btnMultiBarcode;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnProImage.Top;
		}

		private void btnAddDetails_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabAddDetails;
			btnAddDetails.BackColor = Color.White;
			btnAddDetails.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiUnit;
			Button button3 = btnMultiRates;
			Button button4 = btnProImage;
			Button button5 = btnMultiBarcode;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiUnit;
			Button button8 = btnMultiRates;
			Button button9 = btnProImage;
			Button button10 = btnMultiBarcode;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnAddDetails.Top;
		}

		private void btnMultiBarcode_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabMultiBarcode;
			btnMultiBarcode.BackColor = Color.White;
			btnMultiBarcode.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiUnit;
			Button button3 = btnMultiRates;
			Button button4 = btnProImage;
			Button button5 = btnAddDetails;
			Color color = (btnSize.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiUnit;
			Button button8 = btnMultiRates;
			Button button9 = btnProImage;
			Button button10 = btnAddDetails;
			color = (btnSize.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnMultiBarcode.Top;
		}

		private void btnSize_Click(object sender, EventArgs e)
		{
			tabProduct.SelectedTab = tabSize;
			btnSize.BackColor = Color.White;
			btnSize.ForeColor = Color.Black;
			Button button = btnProDetails;
			Button button2 = btnMultiUnit;
			Button button3 = btnMultiRates;
			Button button4 = btnProImage;
			Button button5 = btnAddDetails;
			Color color = (btnMultiBarcode.BackColor = Color.Transparent);
			Color color3 = (button5.BackColor = color);
			Color color5 = (button4.BackColor = color3);
			Color color7 = (button3.BackColor = color5);
			Color color10 = (button.BackColor = (button2.BackColor = color7));
			Button button6 = btnProDetails;
			Button button7 = btnMultiUnit;
			Button button8 = btnMultiRates;
			Button button9 = btnProImage;
			Button button10 = btnAddDetails;
			color = (btnMultiBarcode.ForeColor = Color.Black);
			color3 = (button10.ForeColor = color);
			color5 = (button9.ForeColor = color3);
			color7 = (button8.ForeColor = color5);
			color10 = (button6.ForeColor = (button7.ForeColor = color7));
			btnSelected.Top = btnSize.Top;
		}

		private void btnBrouse_Click(object sender, EventArgs e)
		{
			AniHelper.ImageLoadMethod(pbProImage);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			pbProImage.Image = null;
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = null;
			try
			{
				saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save Image";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					pbProImage.Image.Save(saveFileDialog.FileName + ".jpg");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				saveFileDialog.Dispose();
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			txtAliasCode.Text = txtProCode.Text;
		}

		private void cmbUnit_DropDown(object sender, EventArgs e)
		{
			ComboBox ctrl = (ComboBox)sender;
			AshproCommon.FillCombo(ctrl, "Select UnitId as Id,UnitName as Name From Unit");
		}

		private void cmbUnit2_SelectedIndexChanged(object sender, EventArgs e)
		{
			loadPriceCategory();
		}

		private void cbEditProCode_CheckedChanged(object sender, EventArgs e)
		{
			txtProCode.Enabled = (cbEditProCode.Checked ? true : false);
		}

		private void cmbGodown_SelectedIndexChanged(object sender, EventArgs e)
		{
			AshproCommon.FillCombo(cmbShelf, "Select SelfId as Id,SelfName as Name From Self Where GodownId='" + cmbGodown.SelectedValue.ToInt32() + "'");
		}

		private void txtPurPrice_Validated(object sender, EventArgs e)
		{
			AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbBUnit.SelectedValue.ToInt32(), txtSlsPrice.Value, txtPurPrice.Value, txtName.Text, txtProInLocal.Text);
			loadPriceCategory();
		}

		private void txtSlsDiscAmnt_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtSlsDiscAmnt.Value == 0m)
				{
					txtSlsDiscP.Value = 0m;
				}
				else
				{
					txtSlsDiscP.Value = txtSlsDiscAmnt.Value * 100m / txtSlsPrice.Value;
				}
				loadPriceCategory();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtSlsDiscP_Validated(object sender, EventArgs e)
		{
			SaleDiscount();
			loadPriceCategory();
		}

		private void txtPurDisAmnt_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtPurDisAmnt.Value == 0m)
				{
					txtPurDiscP.Value = 0m;
				}
				else
				{
					txtPurDiscP.Value = txtPurDisAmnt.Value * 100m / txtPurPrice.Value;
				}
				loadPriceCategory();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtPurDiscP_Validated(object sender, EventArgs e)
		{
			PurDiscount();
			loadPriceCategory();
		}

		private void txtProName_Validated(object sender, EventArgs e)
		{
			if (txtName.Text != string.Empty)
			{
				if (!iProductId.HasValue)
				{
					loadPriceCategory();
				}
				txtAliasName.Text = txtName.Text;
				if (rbTranslate.Checked)
				{
					GoogleTrans googleTrans = new GoogleTrans();
					txtProInLocal.Text = googleTrans.Translate(txtName.Text, "en", "ar");
				}
				else if (rbTransliterate.Checked)
				{
					txtProInLocal.Text = ClsArabic.Transliterated2Arabic(txtName.Text);
				}
				AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbBUnit.SelectedValue.ToInt32(), txtSlsPrice.Value, txtPurPrice.Value, txtName.Text, txtProInLocal.Text);
				if (!iProductId.HasValue && db.Products().Any((ProductEntity x) => x.ProductName == txtName.Text))
				{
					Messages.ErrorMessage("Product already exist.");
					txtName.Focus();
					txtName.SelectAll();
				}
			}
		}

		private void Product_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				CalculatorMethod();
			}
		}

		private void txtBarcode_Validated(object sender, EventArgs e)
		{
			if (!(txtBarcode.Text != string.Empty))
			{
				return;
			}
			if (!cbDirectSearch.Checked)
			{
				if (!iProductId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
				{
					Barcode = txtBarcode.Text
				}) > 0)
				{
					Messages.ErrorMessage("Barcode already exist.");
					txtBarcode.Focus();
					txtBarcode.SelectAll();
				}
				else
				{
					AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbBUnit.SelectedValue.ToInt32(), txtSlsPrice.Value, txtPurPrice.Value, txtName.Text, txtProInLocal.Text);
				}
			}
			else
			{
				ProductEntity objectWithParameter_SP = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
				{
					Barcode = txtBarcode.Text
				});
				if (objectWithParameter_SP != null && objectWithParameter_SP.ProductName != null)
				{
					ProductLoadMethod(objectWithParameter_SP);
				}
			}
		}

		private void cmbUnitMulti_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtMultiUnitProductName.Text = ((txtName.Text != string.Empty) ? (txtName.Text + " " + cmbUnitMulti.Text) : "");
			if (cbMultiUnitTranslate.Checked)
			{
				GoogleTrans googleTrans = new GoogleTrans();
				txtLocalMultiUnit.Text = googleTrans.Translate(txtMultiUnitProductName.Text, "en", "ar");
			}
			if (cbMultiUnitTransliterate.Checked)
			{
				txtLocalMultiUnit.Text = ClsArabic.Transliterated2Arabic(txtMultiUnitProductName.Text);
			}
		}

		private void txtMultiUnitBarcode_Validated(object sender, EventArgs e)
		{
			if (txtMultiUnitBarcode.Text != string.Empty && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
			{
				Barcode = txtMultiUnitBarcode.Text
			}) > 0)
			{
				Messages.ErrorMessage("Barcode already exist.");
				txtMultiUnitBarcode.Focus();
				txtMultiUnitBarcode.SelectAll();
			}
		}

		private void btnAddMultiUnit_Click(object sender, EventArgs e)
		{
			if (!(txtMultiUnitProductName.Text == string.Empty) && !(txtMultiUnitBarcode.Text == string.Empty))
			{
				ExProductUnit exProductUnit = new ExProductUnit();
				if (!isUnitInEditingMode)
				{
					exProductUnit.UnitId = cmbUnitMulti.SelectedValue.ToInt32();
					exProductUnit.ProductUnitName = txtMultiUnitProductName.Text;
					if (cmbUnitMulti.Text != string.Empty && txtUnitQty.Value > 0m)
					{
						exProductUnit.UnitQty = txtUnitQty.Value;
					}
					exProductUnit.UnitType = entProductUnits.Count + 2 + " Unit";
					exProductUnit.Barcode = txtMultiUnitBarcode.Text;
					exProductUnit.SalesPrice = txtMultiUnitprice.Value;
					exProductUnit.OpeningStock = txtMultiUnitOppeningStock.Value;
					exProductUnit.IsInclusive = cbMultiUnitInclusive.Checked;
					exProductUnit.ProductId = iProductId;
					exProductUnit.ProductName = txtName.Text;
					exProductUnit.UnitName = cmbUnitMulti.Text;
					entProductUnits.Add(exProductUnit);
					dgLoadMethodUnit();
				}
				else
				{
					exProductUnit = entProductUnits.FirstOrDefault((ExProductUnit x) => x.UnitId == cmbUnitMulti.SelectedValue.ToInt32());
					exProductUnit.ProductUnitName = txtMultiUnitProductName.Text;
					if (cmbUnitMulti.Text != string.Empty && txtUnitQty.Value > 0m)
					{
						exProductUnit.UnitQty = txtUnitQty.Value;
					}
					exProductUnit.UnitType = entProductUnits.Count + 2 + " Unit";
					exProductUnit.Barcode = txtMultiUnitBarcode.Text;
					exProductUnit.SalesPrice = txtMultiUnitprice.Value;
					exProductUnit.OpeningStock = txtMultiUnitOppeningStock.Value;
					exProductUnit.IsInclusive = cbMultiUnitInclusive.Checked;
					exProductUnit.ProductId = iProductId;
					exProductUnit.ProductName = txtName.Text;
					exProductUnit.UnitName = cmbUnitMulti.Text;
					dgMultiUnit.Refresh();
				}
				foreach (PriceCategoryEntity item in db.PriceCategories())
				{
					if (!entProductPrices.Any((ExProductPrice x) => x.Barcode == txtMultiUnitBarcode.Text && x.PCategoryId == item.PCategoryId))
					{
						ExProductPrice item2 = new ExProductPrice
						{
							PCategoryId = item.PCategoryId,
							PCategoryName = item.PCategoryName,
							SL = entProductPrices.Count + 1,
							SPrice = txtMultiUnitprice.Value,
							PPrice = 0m,
							ProName = txtName.Text,
							UnitId = cmbUnitMulti.SelectedValue.ToInt32(),
							UnitName = cmbUnitMulti.Text,
							PGroupId = cmbProGrp.SelectedValue.ToInt32(),
							Barcode = txtMultiUnitBarcode.Text
						};
						entProductPrices.Add(item2);
					}
					else
					{
						ExProductPrice exProductPrice = entProductPrices.FirstOrDefault((ExProductPrice y) => y.Barcode == txtMultiUnitBarcode.Text && y.PCategoryId == item.PCategoryId);
						exProductPrice.PCategoryId = item.PCategoryId;
						exProductPrice.PCategoryName = item.PCategoryName;
						exProductPrice.SL = entProductPrices.Count + 1;
						exProductPrice.SPrice = txtMultiUnitprice.Value;
						exProductPrice.PPrice = 0m;
						exProductPrice.ProName = txtName.Text;
						exProductPrice.UnitId = cmbUnitMulti.SelectedValue.ToInt32();
						exProductPrice.UnitName = cmbUnitMulti.Text;
						exProductPrice.PGroupId = cmbProGrp.SelectedValue.ToInt32();
						exProductPrice.Barcode = txtMultiUnitBarcode.Text;
					}
				}
				dgLoadMethodMultiRates();
				AddbarcodeToList(txtMultiUnitBarcode.Text, "Unit Barcode", cmbUnitMulti.SelectedValue.ToInt32(), txtMultiUnitprice.Value, txtPurPrice.Value * txtUnitQty.Value, txtMultiUnitProductName.Text, txtLocalMultiUnit.Text);
				isUnitInEditingMode = false;
				AniHelper.PanelClearmethod(pnlUnit);
				cmbUnitMulti.Focus();
			}
			else
			{
				Messages.WarningMessage();
				cmbUnitMulti.Focus();
			}
		}

		private void dgMultiUnit_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex == dgMultiUnit.Columns[col_Close1.Name].Index)
				{
					ExProductUnit exProductUnit = (ExProductUnit)dgMultiUnit.CurrentRow.DataBoundItem;
					entProductUnits.Remove(exProductUnit);
					ExProductPrice item = entProductPrices.FirstOrDefault((ExProductPrice x) => x.ProductId == exProductUnit.ProductId && x.UnitId == exProductUnit.UnitId);
					entProductPrices.Remove(item);
					ExProductBarcodeEntity item2 = entProductBarcodes.FirstOrDefault((ExProductBarcodeEntity x) => x.Barcode == exProductUnit.Barcode);
					entProductBarcodes.Remove(item2);
					dgLoadMethodUnit();
					dgLoadMethodBarcode();
					dgLoadMethodMultiRates();
					dgLoadMethodMultiSize();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgMultiUnit_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgMultiUnit_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					ExProductUnit exProductUnit = (ExProductUnit)dgMultiUnit.CurrentRow.DataBoundItem;
					cmbUnitMulti.SelectedValue = exProductUnit.UnitId;
					txtMultiUnitProductName.Text = exProductUnit.ProductUnitName;
					txtMultiUnitOppeningStock.Value = exProductUnit.OpeningStock;
					txtMultiUnitprice.Value = exProductUnit.SalesPrice;
					cbMultiUnitInclusive.Checked = exProductUnit.IsInclusive;
					txtUnitQty.Value = exProductUnit.UnitQty;
					txtMultiUnitBarcode.Text = exProductUnit.Barcode;
					isUnitInEditingMode = true;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnAddMultiBarcode_Click(object sender, EventArgs e)
		{
			if (txtMultiBarcode.Text != string.Empty)
			{
				if (!isBarcodeInEditingMode)
				{
					AddbarcodeToList(txtMultiBarcode.Text, "Multi Barcode", cmbUnitMultiBarcode.SelectedValue.ToInt32(), txtSlsPrice.Value, txtPurPrice.Value, txtName.Text, txtProInLocal.Text);
				}
				else
				{
					dgMultiBarcode.Refresh();
				}
			}
			txtMultiBarcode.Text = string.Empty;
			isBarcodeInEditingMode = false;
			txtMultiBarcode.Focus();
		}

		private void txtMultiBarcode_KeyDown(object sender, KeyEventArgs e)
		{
			if (AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
			{
				Barcode = txtMultiBarcode.Text
			}) > 0)
			{
				Messages.ErrorMessage("Barcode already exist.");
				txtMultiBarcode.Focus();
				txtMultiBarcode.SelectAll();
			}
			else if (e.KeyCode == Keys.Return)
			{
				btnAddMultiBarcode.PerformClick();
			}
		}

		private void dgMultiBarcode_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgMultiBarcode_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == dgMultiBarcode.Columns[col_Close.Name].Index)
			{
				ExProductBarcodeEntity item = (ExProductBarcodeEntity)dgMultiBarcode.CurrentRow.DataBoundItem;
				entProductBarcodes.Remove(item);
				int iSL = 1;
				entProductBarcodes.ForEach(delegate(ExProductBarcodeEntity x)
				{
					x.SL = iSL;
					iSL++;
				});
				dgLoadMethodBarcode();
			}
		}

		private void dgMultiBarcode_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == dgMultiBarcode.Columns[col_Close.Name].Index)
			{
				ExProductBarcodeEntity exProductBarcodeEntity = (ExProductBarcodeEntity)dgMultiBarcode.CurrentRow.DataBoundItem;
				txtMultiBarcode.Text = exProductBarcodeEntity.Barcode;
				cmbUnitMultiBarcode.SelectedValue = exProductBarcodeEntity.FK_UnitId;
				isBarcodeInEditingMode = true;
			}
		}

		private void txtMultiBarcode_Validated(object sender, EventArgs e)
		{
			if (txtMultiBarcode.Text != string.Empty && !iProductId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
			{
				Barcode = txtMultiBarcode.Text
			}) > 0)
			{
				Messages.ErrorMessage("Barcode already exist.");
				txtMultiBarcode.Focus();
				txtMultiBarcode.SelectAll();
			}
		}

		private void dgProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void txtUnitQty_Validated(object sender, EventArgs e)
		{
			txtMultiUnitprice.Value = txtSlsPrice.Value * txtUnitQty.Value;
			if (cbAutoGenerated.Checked)
			{
				txtMultiUnitBarcode.Text = txtBarcode.Text + txtUnitQty.Value;
			}
		}

		private void txtMultiUnitProductName_Validated(object sender, EventArgs e)
		{
			if (cbMultiUnitTranslate.Checked)
			{
				GoogleTrans googleTrans = new GoogleTrans();
				txtLocalMultiUnit.Text = googleTrans.Translate(txtMultiUnitProductName.Text, "en", "ar");
			}
			if (cbMultiUnitTransliterate.Checked)
			{
				txtLocalMultiUnit.Text = ClsArabic.Transliterated2Arabic(txtMultiUnitProductName.Text);
			}
		}

		private void btnSizeRemoveImage_Click(object sender, EventArgs e)
		{
			pbSizeImage.Image = null;
		}

		private void btnSizeAddImg_Click(object sender, EventArgs e)
		{
			AniHelper.ImageLoadMethod(pbSizeImage);
		}

		private void btnAddSize_Click(object sender, EventArgs e)
		{
			if (!(txtSizeName.Text == string.Empty) && !(txtSizeBarcode.Text == string.Empty))
			{
				productBarcode.FK_ProductId = iProductId;
				productBarcode.ProductBarcodeName = sizeProName;
				productBarcode.LocalName = txtSizeLocalName.Text;
				productBarcode.Barcode = txtSizeBarcode.Text;
				productBarcode.SellingPrice = txtSizePrice.Value;
				productBarcode.image = ((pbSizeImage.Image == null) ? null : AniHelper.imageToByteArray(pbSizeImage.Image));
				productBarcode.FK_BranchId = Settings.Default.iBranchId;
				productBarcode.FK_UnitId = cmbBUnit.SelectedValue.ToInt32();
				productBarcode.BarcodeType = "Multi Size";
				if (!isSizeUpadate)
				{
					entSizes.Add(productBarcode);
					dgLoadMethodMultiSize();
				}
				else
				{
					dgSize.Refresh();
				}
				isSizeUpadate = false;
				loadPriceCategory(txtSizeBarcode.Text, txtSizePrice.Value, isMultiSize: true);
				ClearSize();
			}
			else
			{
				Messages.WarningMessage();
			}
		}

		private void txtSizeName_Validated(object sender, EventArgs e)
		{
			sizeProName = txtName.Text + "_" + txtSizeName.Text;
			txtSizeLocalName.Text = AniHelper.GetLocalName(cbSizeTrasliterate.Checked, cbSizeTraslate.Checked, sizeProName, txtSizeLocalName.Text);
		}

		private void txtSizePrice_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAddSize.PerformClick();
			}
		}

		private void dgSize_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				productBarcode = (ProductBarcodeEntity)dgSize.CurrentRow.DataBoundItem;
				iSizeId = productBarcode.Id;
				string oldValue = txtName.Text.Replace("#", "") + "_";
				string text = productBarcode.ProductBarcodeName.Replace("#", "");
				txtSizeName.Text = text.Replace(oldValue, "");
				sizeProName = txtName.Text + "_" + txtSizeName.Text;
				txtSizeLocalName.Text = productBarcode.LocalName;
				txtSizeBarcode.Text = productBarcode.Barcode;
				txtSizePrice.Value = productBarcode.SellingPrice;
				pbSizeImage.Image = AniHelper.byteArrayToImage(productBarcode.image);
				isSizeUpadate = true;
			}
		}

		private void tabProduct_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (tabProduct.SelectedIndex)
			{
			case 0:
				txtName.Focus();
				break;
			case 1:
				cbRateBasedWeight.Focus();
				break;
			case 2:
				cmbUnitMulti.Focus();
				break;
			case 4:
				txtMultiBarcode.Focus();
				break;
			case 6:
				txtSizeName.Focus();
				break;
			case 3:
			case 5:
				break;
			}
		}

		private void dgSize_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSize_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex == dgSize.Columns[col_SizeClose.Name].Index)
				{
					productBarcode = (ProductBarcodeEntity)dgSize.CurrentRow.DataBoundItem;
					entSizes.Remove(productBarcode);
					ExProductPrice exProductPrice = entProductPrices.FirstOrDefault((ExProductPrice x) => x.Barcode == productBarcode.Barcode);
					if (exProductPrice != null)
					{
						MultirateDeleteMethod(exProductPrice);
					}
					dgLoadMethodMultiSize();
					txtSizeName.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSizeBarcode_Validated(object sender, EventArgs e)
		{
			if (txtSizeBarcode.Text != string.Empty && !iSizeId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
			{
				Barcode = txtSizeBarcode.Text
			}) > 0)
			{
				Messages.ErrorMessage("Barcode already exist.");
				txtSizeBarcode.Focus();
				txtSizeBarcode.SelectAll();
			}
		}

		private void dgMultiRates_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == dgMultiRates.Columns[col_Delete.Name].Index)
			{
				ExProductPrice exProductPrice = (ExProductPrice)dgMultiRates.CurrentRow.DataBoundItem;
				MultirateDeleteMethod(exProductPrice);
			}
		}

		private void MultirateDeleteMethod(ExProductPrice exProductPrice)
		{
			entProductPrices.Remove(exProductPrice);
			int num = 1;
			foreach (ExProductPrice entProductPrice in entProductPrices)
			{
				entProductPrice.SL = num;
				num++;
			}
			dgLoadMethodMultiRates();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabProduct = new System.Windows.Forms.TabControl();
            this.tabProductDetails = new System.Windows.Forms.TabPage();
            this.cbDirectSearch = new System.Windows.Forms.CheckBox();
            this.txtLoyality = new AshUsercontrol.AshTextBoxNormal();
            this.lblLoyality = new System.Windows.Forms.Label();
            this.txtOrderInList = new AshUsercontrol.AshTextBoxNormal();
            this.lblOrdrInList = new System.Windows.Forms.Label();
            this.cbSerialNumber = new System.Windows.Forms.CheckBox();
            this.cbInclusive = new System.Windows.Forms.CheckBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.txtBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblHSN = new System.Windows.Forms.Label();
            this.lblShelf = new System.Windows.Forms.Label();
            this.cmbGodown = new AshUsercontrol.AshComboBox();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.btnGodown = new System.Windows.Forms.Button();
            this.lblGodown = new System.Windows.Forms.Label();
            this.txtReOrderStck = new AshUsercontrol.AshTextBoxNormal();
            this.lblReOrder = new System.Windows.Forms.Label();
            this.dtmOppStk = new AshUsercontrol.AshDateTimePicker();
            this.lblOpDat = new System.Windows.Forms.Label();
            this.txtOppStock = new AshUsercontrol.AshTextBoxNormal();
            this.lblOppStck = new System.Windows.Forms.Label();
            this.txtHSNCode = new AshUsercontrol.AshTextBoxNormal();
            this.txtMRP = new AshUsercontrol.AshTextBoxNormal();
            this.lblMRP = new System.Windows.Forms.Label();
            this.txtSlsPrice = new AshUsercontrol.AshTextBoxNormal();
            this.lblSalePrice = new System.Windows.Forms.Label();
            this.txtPurPrice = new AshUsercontrol.AshTextBoxNormal();
            this.lblPurPrice = new System.Windows.Forms.Label();
            this.cmbSupplier = new AshUsercontrol.AshComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbShelf = new AshUsercontrol.AshComboBox();
            this.cmbTaxCtgry = new AshUsercontrol.AshComboBox();
            this.lblTCategory = new System.Windows.Forms.Label();
            this.btnShelf = new System.Windows.Forms.Button();
            this.btnTaxCategory = new System.Windows.Forms.Button();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.btnBaseUnit = new System.Windows.Forms.Button();
            this.btnBrand = new System.Windows.Forms.Button();
            this.btnProductGroup = new System.Windows.Forms.Button();
            this.cmbBUnit = new AshUsercontrol.AshComboBox();
            this.lblBUnit = new System.Windows.Forms.Label();
            this.cmbProGrp = new AshUsercontrol.AshComboBox();
            this.lblProGrp = new System.Windows.Forms.Label();
            this.txtSpecification = new AshUsercontrol.AshTextBoxNormal();
            this.lblSpec = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.lblName = new System.Windows.Forms.Label();
            this.cbEditProCode = new System.Windows.Forms.CheckBox();
            this.txtProCode = new AshUsercontrol.AshTextBoxNormal();
            this.lblProCode = new System.Windows.Forms.Label();
            this.cmbProType = new AshUsercontrol.AshComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.tabAddDetails = new System.Windows.Forms.TabPage();
            this.cmbRestaurantType = new AshUsercontrol.AshComboBox();
            this.cmbPriority = new AshUsercontrol.AshComboBox();
            this.cbRateBasedWeight = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbTranslate = new System.Windows.Forms.RadioButton();
            this.rbTransliterate = new System.Windows.Forms.RadioButton();
            this.cbScale = new System.Windows.Forms.CheckBox();
            this.cbPercentageBased = new System.Windows.Forms.CheckBox();
            this.cbFavaurite = new System.Windows.Forms.CheckBox();
            this.cbMultiple = new System.Windows.Forms.CheckBox();
            this.txtSlsDiscP = new AshUsercontrol.AshTextBoxNormal();
            this.label21 = new System.Windows.Forms.Label();
            this.txtSlsDiscAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCookingTime = new AshUsercontrol.AshTextBoxNormal();
            this.txtPurDiscP = new AshUsercontrol.AshTextBoxNormal();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPurDisAmnt = new AshUsercontrol.AshTextBoxNormal();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.txtItemCalory = new AshUsercontrol.AshTextBoxNormal();
            this.txtExiCommission = new AshUsercontrol.AshTextBoxNormal();
            this.txtProInLocal = new AshUsercontrol.AshTextBoxNormal();
            this.txtAliasName = new AshUsercontrol.AshTextBoxNormal();
            this.txtAliasCode = new AshUsercontrol.AshTextBoxNormal();
            this.label26 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabMultiUnit = new System.Windows.Forms.TabPage();
            this.grpUnit = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbRptUnit = new AshUsercontrol.AshComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.cmbPurUnit = new AshUsercontrol.AshComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.cmbSalesUnit = new AshUsercontrol.AshComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.pnlUnit = new System.Windows.Forms.Panel();
            this.cbMultiUnitTranslate = new System.Windows.Forms.CheckBox();
            this.cbAutoGenerated = new System.Windows.Forms.CheckBox();
            this.cbMultiUnitTransliterate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocalMultiUnit = new AshUsercontrol.AshTextBoxNormal();
            this.dgMultiUnit = new System.Windows.Forms.DataGridView();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Close1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbUnitMulti = new AshUsercontrol.AshComboBox();
            this.cbMultiUnitInclusive = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnAddMultiUnit = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.txtMultiUnitOppeningStock = new AshUsercontrol.AshTextBoxNormal();
            this.txtMultiUnitProductName = new AshUsercontrol.AshTextBoxNormal();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtMultiUnitBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.txtUnitQty = new AshUsercontrol.AshTextBoxNormal();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtMultiUnitprice = new AshUsercontrol.AshTextBoxNormal();
            this.tabMultiRates = new System.Windows.Forms.TabPage();
            this.dgMultiRates = new System.Windows.Forms.DataGridView();
            this.col_MultiRate_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_PCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_SalesDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_PurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRates_PurDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRatesBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabMultiBarcode = new System.Windows.Forms.TabPage();
            this.dgMultiBarcode = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BarcodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Close = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbUnitMultiBarcode = new AshUsercontrol.AshComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtMultiBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.btnAddMultiBarcode = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.taImage = new System.Windows.Forms.TabPage();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbProImage = new System.Windows.Forms.PictureBox();
            this.btnBrouse = new System.Windows.Forms.Button();
            this.tabSize = new System.Windows.Forms.TabPage();
            this.pnlMultiSize = new System.Windows.Forms.Panel();
            this.txtSizeName = new AshUsercontrol.AshTextBoxNormal();
            this.btnSizeRemoveImage = new System.Windows.Forms.Button();
            this.txtSizePrice = new AshUsercontrol.AshTextBoxNormal();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pbSizeImage = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSizeAddImg = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSizeTraslate = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbSizeTrasliterate = new System.Windows.Forms.CheckBox();
            this.txtSizeBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSizeLocalName = new AshUsercontrol.AshTextBoxNormal();
            this.btnAddSize = new System.Windows.Forms.Button();
            this.dgSize = new System.Windows.Forms.DataGridView();
            this.Col_MultiSize_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_MultiSize_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_MultiSize_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SizeClose = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.bsPCategory = new System.Windows.Forms.BindingSource(this.components);
            this.bsMultUnit = new System.Windows.Forms.BindingSource(this.components);
            this.bsMultiBarcode = new System.Windows.Forms.BindingSource(this.components);
            this.bsProduct = new System.Windows.Forms.BindingSource(this.components);
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
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ashGradientPanel1 = new AshUsercontrol.AshGradientPanel();
            this.btnSelected = new AshUsercontrol.AshGradientPanel();
            this.btnSize = new System.Windows.Forms.Button();
            this.btnMultiBarcode = new System.Windows.Forms.Button();
            this.btnAddDetails = new System.Windows.Forms.Button();
            this.btnProImage = new System.Windows.Forms.Button();
            this.btnMultiRates = new System.Windows.Forms.Button();
            this.btnMultiUnit = new System.Windows.Forms.Button();
            this.btnProDetails = new System.Windows.Forms.Button();
            this.ashGradientPanel2 = new AshUsercontrol.AshGradientPanel();
            this.bdsSize = new System.Windows.Forms.BindingSource(this.components);
            this.pnlButton.SuspendLayout();
            this.tabProduct.SuspendLayout();
            this.tabProductDetails.SuspendLayout();
            this.tabAddDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabMultiUnit.SuspendLayout();
            this.grpUnit.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiUnit)).BeginInit();
            this.tabMultiRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiRates)).BeginInit();
            this.tabMultiBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiBarcode)).BeginInit();
            this.taImage.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProImage)).BeginInit();
            this.tabSize.SuspendLayout();
            this.pnlMultiSize.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSizeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultiBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).BeginInit();
            this.ashGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            resources.ApplyResources(this.pnlButton, "pnlButton");
            // 
            // pnlMaster
            // 
            resources.ApplyResources(this.pnlMaster, "pnlMaster");
            // 
            // lblMasterTitle
            // 
            resources.ApplyResources(this.lblMasterTitle, "lblMasterTitle");
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // tabProduct
            // 
            resources.ApplyResources(this.tabProduct, "tabProduct");
            this.tabProduct.Controls.Add(this.tabProductDetails);
            this.tabProduct.Controls.Add(this.tabAddDetails);
            this.tabProduct.Controls.Add(this.tabMultiUnit);
            this.tabProduct.Controls.Add(this.tabMultiRates);
            this.tabProduct.Controls.Add(this.tabMultiBarcode);
            this.tabProduct.Controls.Add(this.taImage);
            this.tabProduct.Controls.Add(this.tabSize);
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.SelectedIndex = 0;
            this.tabProduct.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabProduct.TabStop = false;
            this.tabProduct.SelectedIndexChanged += new System.EventHandler(this.tabProduct_SelectedIndexChanged);
            // 
            // tabProductDetails
            // 
            resources.ApplyResources(this.tabProductDetails, "tabProductDetails");
            this.tabProductDetails.BackColor = System.Drawing.Color.White;
            this.tabProductDetails.Controls.Add(this.cbDirectSearch);
            this.tabProductDetails.Controls.Add(this.txtLoyality);
            this.tabProductDetails.Controls.Add(this.lblLoyality);
            this.tabProductDetails.Controls.Add(this.txtOrderInList);
            this.tabProductDetails.Controls.Add(this.lblOrdrInList);
            this.tabProductDetails.Controls.Add(this.cbSerialNumber);
            this.tabProductDetails.Controls.Add(this.cbInclusive);
            this.tabProductDetails.Controls.Add(this.cbActive);
            this.tabProductDetails.Controls.Add(this.txtBarcode);
            this.tabProductDetails.Controls.Add(this.lblBarcode);
            this.tabProductDetails.Controls.Add(this.lblHSN);
            this.tabProductDetails.Controls.Add(this.lblShelf);
            this.tabProductDetails.Controls.Add(this.cmbGodown);
            this.tabProductDetails.Controls.Add(this.btnSupplier);
            this.tabProductDetails.Controls.Add(this.btnGodown);
            this.tabProductDetails.Controls.Add(this.lblGodown);
            this.tabProductDetails.Controls.Add(this.txtReOrderStck);
            this.tabProductDetails.Controls.Add(this.lblReOrder);
            this.tabProductDetails.Controls.Add(this.dtmOppStk);
            this.tabProductDetails.Controls.Add(this.lblOpDat);
            this.tabProductDetails.Controls.Add(this.txtOppStock);
            this.tabProductDetails.Controls.Add(this.lblOppStck);
            this.tabProductDetails.Controls.Add(this.txtHSNCode);
            this.tabProductDetails.Controls.Add(this.txtMRP);
            this.tabProductDetails.Controls.Add(this.lblMRP);
            this.tabProductDetails.Controls.Add(this.txtSlsPrice);
            this.tabProductDetails.Controls.Add(this.lblSalePrice);
            this.tabProductDetails.Controls.Add(this.txtPurPrice);
            this.tabProductDetails.Controls.Add(this.lblPurPrice);
            this.tabProductDetails.Controls.Add(this.cmbSupplier);
            this.tabProductDetails.Controls.Add(this.lblSupplier);
            this.tabProductDetails.Controls.Add(this.cmbShelf);
            this.tabProductDetails.Controls.Add(this.cmbTaxCtgry);
            this.tabProductDetails.Controls.Add(this.lblTCategory);
            this.tabProductDetails.Controls.Add(this.btnShelf);
            this.tabProductDetails.Controls.Add(this.btnTaxCategory);
            this.tabProductDetails.Controls.Add(this.cmbBrand);
            this.tabProductDetails.Controls.Add(this.lblBrand);
            this.tabProductDetails.Controls.Add(this.btnBaseUnit);
            this.tabProductDetails.Controls.Add(this.btnBrand);
            this.tabProductDetails.Controls.Add(this.btnProductGroup);
            this.tabProductDetails.Controls.Add(this.cmbBUnit);
            this.tabProductDetails.Controls.Add(this.lblBUnit);
            this.tabProductDetails.Controls.Add(this.cmbProGrp);
            this.tabProductDetails.Controls.Add(this.lblProGrp);
            this.tabProductDetails.Controls.Add(this.txtSpecification);
            this.tabProductDetails.Controls.Add(this.lblSpec);
            this.tabProductDetails.Controls.Add(this.txtName);
            this.tabProductDetails.Controls.Add(this.lblName);
            this.tabProductDetails.Controls.Add(this.cbEditProCode);
            this.tabProductDetails.Controls.Add(this.txtProCode);
            this.tabProductDetails.Controls.Add(this.lblProCode);
            this.tabProductDetails.Controls.Add(this.cmbProType);
            this.tabProductDetails.Controls.Add(this.lblType);
            this.tabProductDetails.Name = "tabProductDetails";
            // 
            // cbDirectSearch
            // 
            resources.ApplyResources(this.cbDirectSearch, "cbDirectSearch");
            this.cbDirectSearch.Name = "cbDirectSearch";
            this.cbDirectSearch.UseVisualStyleBackColor = true;
            // 
            // txtLoyality
            // 
            resources.ApplyResources(this.txtLoyality, "txtLoyality");
            this.txtLoyality.BackColor = System.Drawing.Color.White;
            this.txtLoyality.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoyality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoyality.Data = 0;
            this.txtLoyality.EnterColor = System.Drawing.Color.LightPink;
            this.txtLoyality.Format = null;
            this.txtLoyality.isAllowNegative = false;
            this.txtLoyality.isAllowSpecialChar = false;
            this.txtLoyality.isNumberOnly = true;
            this.txtLoyality.isNumeric = false;
            this.txtLoyality.LeaveColor = System.Drawing.Color.White;
            this.txtLoyality.Name = "txtLoyality";
            this.txtLoyality.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblLoyality
            // 
            resources.ApplyResources(this.lblLoyality, "lblLoyality");
            this.lblLoyality.Name = "lblLoyality";
            // 
            // txtOrderInList
            // 
            resources.ApplyResources(this.txtOrderInList, "txtOrderInList");
            this.txtOrderInList.BackColor = System.Drawing.Color.White;
            this.txtOrderInList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOrderInList.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrderInList.Data = 0;
            this.txtOrderInList.EnterColor = System.Drawing.Color.LightPink;
            this.txtOrderInList.Format = null;
            this.txtOrderInList.isAllowNegative = false;
            this.txtOrderInList.isAllowSpecialChar = false;
            this.txtOrderInList.isNumberOnly = true;
            this.txtOrderInList.isNumeric = false;
            this.txtOrderInList.LeaveColor = System.Drawing.Color.White;
            this.txtOrderInList.Name = "txtOrderInList";
            this.txtOrderInList.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblOrdrInList
            // 
            resources.ApplyResources(this.lblOrdrInList, "lblOrdrInList");
            this.lblOrdrInList.Name = "lblOrdrInList";
            // 
            // cbSerialNumber
            // 
            resources.ApplyResources(this.cbSerialNumber, "cbSerialNumber");
            this.cbSerialNumber.Name = "cbSerialNumber";
            this.cbSerialNumber.UseVisualStyleBackColor = true;
            // 
            // cbInclusive
            // 
            resources.ApplyResources(this.cbInclusive, "cbInclusive");
            this.cbInclusive.Checked = true;
            this.cbInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInclusive.Name = "cbInclusive";
            this.cbInclusive.UseVisualStyleBackColor = true;
            // 
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // txtBarcode
            // 
            resources.ApplyResources(this.txtBarcode, "txtBarcode");
            this.txtBarcode.BackColor = System.Drawing.Color.White;
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBarcode.Data = 0;
            this.txtBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtBarcode.Format = null;
            this.txtBarcode.isAllowNegative = false;
            this.txtBarcode.isAllowSpecialChar = false;
            this.txtBarcode.isNumberOnly = false;
            this.txtBarcode.isNumeric = false;
            this.txtBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBarcode.Validated += new System.EventHandler(this.txtBarcode_Validated);
            // 
            // lblBarcode
            // 
            resources.ApplyResources(this.lblBarcode, "lblBarcode");
            this.lblBarcode.Name = "lblBarcode";
            // 
            // lblHSN
            // 
            resources.ApplyResources(this.lblHSN, "lblHSN");
            this.lblHSN.Name = "lblHSN";
            // 
            // lblShelf
            // 
            resources.ApplyResources(this.lblShelf, "lblShelf");
            this.lblShelf.Name = "lblShelf";
            // 
            // cmbGodown
            // 
            resources.ApplyResources(this.cmbGodown, "cmbGodown");
            this.cmbGodown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGodown.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGodown.FormattingEnabled = true;
            this.cmbGodown.LeaveColor = System.Drawing.Color.White;
            this.cmbGodown.Name = "cmbGodown";
            this.cmbGodown.SelectedIndexChanged += new System.EventHandler(this.cmbGodown_SelectedIndexChanged);
            // 
            // btnSupplier
            // 
            resources.ApplyResources(this.btnSupplier, "btnSupplier");
            this.btnSupplier.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSupplier.ForeColor = System.Drawing.Color.White;
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.TabStop = false;
            this.btnSupplier.UseVisualStyleBackColor = false;
            this.btnSupplier.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // btnGodown
            // 
            resources.ApplyResources(this.btnGodown, "btnGodown");
            this.btnGodown.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGodown.ForeColor = System.Drawing.Color.White;
            this.btnGodown.Name = "btnGodown";
            this.btnGodown.TabStop = false;
            this.btnGodown.UseVisualStyleBackColor = false;
            this.btnGodown.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // lblGodown
            // 
            resources.ApplyResources(this.lblGodown, "lblGodown");
            this.lblGodown.Name = "lblGodown";
            // 
            // txtReOrderStck
            // 
            resources.ApplyResources(this.txtReOrderStck, "txtReOrderStck");
            this.txtReOrderStck.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReOrderStck.Data = 0;
            this.txtReOrderStck.EnterColor = System.Drawing.Color.LightPink;
            this.txtReOrderStck.Format = null;
            this.txtReOrderStck.isAllowNegative = false;
            this.txtReOrderStck.isAllowSpecialChar = false;
            this.txtReOrderStck.isNumberOnly = false;
            this.txtReOrderStck.isNumeric = true;
            this.txtReOrderStck.LeaveColor = System.Drawing.Color.White;
            this.txtReOrderStck.Name = "txtReOrderStck";
            this.txtReOrderStck.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblReOrder
            // 
            resources.ApplyResources(this.lblReOrder, "lblReOrder");
            this.lblReOrder.Name = "lblReOrder";
            // 
            // dtmOppStk
            // 
            resources.ApplyResources(this.dtmOppStk, "dtmOppStk");
            this.dtmOppStk.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmOppStk.Name = "dtmOppStk";
            // 
            // lblOpDat
            // 
            resources.ApplyResources(this.lblOpDat, "lblOpDat");
            this.lblOpDat.Name = "lblOpDat";
            // 
            // txtOppStock
            // 
            resources.ApplyResources(this.txtOppStock, "txtOppStock");
            this.txtOppStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOppStock.Data = 0;
            this.txtOppStock.EnterColor = System.Drawing.Color.LightPink;
            this.txtOppStock.Format = null;
            this.txtOppStock.isAllowNegative = false;
            this.txtOppStock.isAllowSpecialChar = false;
            this.txtOppStock.isNumberOnly = false;
            this.txtOppStock.isNumeric = true;
            this.txtOppStock.LeaveColor = System.Drawing.Color.White;
            this.txtOppStock.Name = "txtOppStock";
            this.txtOppStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblOppStck
            // 
            resources.ApplyResources(this.lblOppStck, "lblOppStck");
            this.lblOppStck.Name = "lblOppStck";
            // 
            // txtHSNCode
            // 
            resources.ApplyResources(this.txtHSNCode, "txtHSNCode");
            this.txtHSNCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHSNCode.Data = 0;
            this.txtHSNCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtHSNCode.Format = null;
            this.txtHSNCode.isAllowNegative = false;
            this.txtHSNCode.isAllowSpecialChar = false;
            this.txtHSNCode.isNumberOnly = true;
            this.txtHSNCode.isNumeric = false;
            this.txtHSNCode.LeaveColor = System.Drawing.Color.White;
            this.txtHSNCode.Name = "txtHSNCode";
            this.txtHSNCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMRP
            // 
            resources.ApplyResources(this.txtMRP, "txtMRP");
            this.txtMRP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMRP.Data = 0;
            this.txtMRP.EnterColor = System.Drawing.Color.LightPink;
            this.txtMRP.Format = null;
            this.txtMRP.isAllowNegative = false;
            this.txtMRP.isAllowSpecialChar = false;
            this.txtMRP.isNumberOnly = false;
            this.txtMRP.isNumeric = true;
            this.txtMRP.LeaveColor = System.Drawing.Color.White;
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblMRP
            // 
            resources.ApplyResources(this.lblMRP, "lblMRP");
            this.lblMRP.Name = "lblMRP";
            // 
            // txtSlsPrice
            // 
            resources.ApplyResources(this.txtSlsPrice, "txtSlsPrice");
            this.txtSlsPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsPrice.Data = 0;
            this.txtSlsPrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsPrice.Format = null;
            this.txtSlsPrice.isAllowNegative = false;
            this.txtSlsPrice.isAllowSpecialChar = false;
            this.txtSlsPrice.isNumberOnly = false;
            this.txtSlsPrice.isNumeric = true;
            this.txtSlsPrice.LeaveColor = System.Drawing.Color.White;
            this.txtSlsPrice.Name = "txtSlsPrice";
            this.txtSlsPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSlsPrice.Validated += new System.EventHandler(this.txtPurPrice_Validated);
            // 
            // lblSalePrice
            // 
            resources.ApplyResources(this.lblSalePrice, "lblSalePrice");
            this.lblSalePrice.Name = "lblSalePrice";
            // 
            // txtPurPrice
            // 
            resources.ApplyResources(this.txtPurPrice, "txtPurPrice");
            this.txtPurPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurPrice.Data = 0;
            this.txtPurPrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurPrice.Format = null;
            this.txtPurPrice.isAllowNegative = false;
            this.txtPurPrice.isAllowSpecialChar = false;
            this.txtPurPrice.isNumberOnly = false;
            this.txtPurPrice.isNumeric = true;
            this.txtPurPrice.LeaveColor = System.Drawing.Color.White;
            this.txtPurPrice.Name = "txtPurPrice";
            this.txtPurPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurPrice.Validated += new System.EventHandler(this.txtPurPrice_Validated);
            // 
            // lblPurPrice
            // 
            resources.ApplyResources(this.lblPurPrice, "lblPurPrice");
            this.lblPurPrice.Name = "lblPurPrice";
            // 
            // cmbSupplier
            // 
            resources.ApplyResources(this.cmbSupplier, "cmbSupplier");
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.LeaveColor = System.Drawing.Color.White;
            this.cmbSupplier.Name = "cmbSupplier";
            // 
            // lblSupplier
            // 
            resources.ApplyResources(this.lblSupplier, "lblSupplier");
            this.lblSupplier.Name = "lblSupplier";
            // 
            // cmbShelf
            // 
            resources.ApplyResources(this.cmbShelf, "cmbShelf");
            this.cmbShelf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShelf.EnterColor = System.Drawing.Color.LightPink;
            this.cmbShelf.FormattingEnabled = true;
            this.cmbShelf.LeaveColor = System.Drawing.Color.White;
            this.cmbShelf.Name = "cmbShelf";
            // 
            // cmbTaxCtgry
            // 
            resources.ApplyResources(this.cmbTaxCtgry, "cmbTaxCtgry");
            this.cmbTaxCtgry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxCtgry.EnterColor = System.Drawing.Color.LightPink;
            this.cmbTaxCtgry.FormattingEnabled = true;
            this.cmbTaxCtgry.LeaveColor = System.Drawing.Color.White;
            this.cmbTaxCtgry.Name = "cmbTaxCtgry";
            // 
            // lblTCategory
            // 
            resources.ApplyResources(this.lblTCategory, "lblTCategory");
            this.lblTCategory.Name = "lblTCategory";
            // 
            // btnShelf
            // 
            resources.ApplyResources(this.btnShelf, "btnShelf");
            this.btnShelf.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShelf.ForeColor = System.Drawing.Color.White;
            this.btnShelf.Name = "btnShelf";
            this.btnShelf.TabStop = false;
            this.btnShelf.UseVisualStyleBackColor = false;
            this.btnShelf.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // btnTaxCategory
            // 
            resources.ApplyResources(this.btnTaxCategory, "btnTaxCategory");
            this.btnTaxCategory.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTaxCategory.ForeColor = System.Drawing.Color.White;
            this.btnTaxCategory.Name = "btnTaxCategory";
            this.btnTaxCategory.TabStop = false;
            this.btnTaxCategory.UseVisualStyleBackColor = false;
            this.btnTaxCategory.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // cmbBrand
            // 
            resources.ApplyResources(this.cmbBrand, "cmbBrand");
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.LeaveColor = System.Drawing.Color.White;
            this.cmbBrand.Name = "cmbBrand";
            // 
            // lblBrand
            // 
            resources.ApplyResources(this.lblBrand, "lblBrand");
            this.lblBrand.Name = "lblBrand";
            // 
            // btnBaseUnit
            // 
            resources.ApplyResources(this.btnBaseUnit, "btnBaseUnit");
            this.btnBaseUnit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBaseUnit.ForeColor = System.Drawing.Color.White;
            this.btnBaseUnit.Name = "btnBaseUnit";
            this.btnBaseUnit.TabStop = false;
            this.btnBaseUnit.UseVisualStyleBackColor = false;
            this.btnBaseUnit.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // btnBrand
            // 
            resources.ApplyResources(this.btnBrand, "btnBrand");
            this.btnBrand.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBrand.ForeColor = System.Drawing.Color.White;
            this.btnBrand.Name = "btnBrand";
            this.btnBrand.TabStop = false;
            this.btnBrand.UseVisualStyleBackColor = false;
            this.btnBrand.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // btnProductGroup
            // 
            resources.ApplyResources(this.btnProductGroup, "btnProductGroup");
            this.btnProductGroup.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProductGroup.ForeColor = System.Drawing.Color.White;
            this.btnProductGroup.Name = "btnProductGroup";
            this.btnProductGroup.TabStop = false;
            this.btnProductGroup.UseVisualStyleBackColor = false;
            this.btnProductGroup.Click += new System.EventHandler(this.btnNewObjectCreate);
            // 
            // cmbBUnit
            // 
            resources.ApplyResources(this.cmbBUnit, "cmbBUnit");
            this.cmbBUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBUnit.FormattingEnabled = true;
            this.cmbBUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbBUnit.Name = "cmbBUnit";
            // 
            // lblBUnit
            // 
            resources.ApplyResources(this.lblBUnit, "lblBUnit");
            this.lblBUnit.Name = "lblBUnit";
            // 
            // cmbProGrp
            // 
            resources.ApplyResources(this.cmbProGrp, "cmbProGrp");
            this.cmbProGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProGrp.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProGrp.FormattingEnabled = true;
            this.cmbProGrp.LeaveColor = System.Drawing.Color.White;
            this.cmbProGrp.Name = "cmbProGrp";
            // 
            // lblProGrp
            // 
            resources.ApplyResources(this.lblProGrp, "lblProGrp");
            this.lblProGrp.Name = "lblProGrp";
            // 
            // txtSpecification
            // 
            resources.ApplyResources(this.txtSpecification, "txtSpecification");
            this.txtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpecification.Data = 0;
            this.txtSpecification.EnterColor = System.Drawing.Color.LightPink;
            this.txtSpecification.Format = null;
            this.txtSpecification.isAllowNegative = false;
            this.txtSpecification.isAllowSpecialChar = false;
            this.txtSpecification.isNumberOnly = false;
            this.txtSpecification.isNumeric = false;
            this.txtSpecification.LeaveColor = System.Drawing.Color.White;
            this.txtSpecification.Name = "txtSpecification";
            this.txtSpecification.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSpec
            // 
            resources.ApplyResources(this.lblSpec, "lblSpec");
            this.lblSpec.Name = "lblSpec";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BackColor = System.Drawing.Color.LightPink;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.Validated += new System.EventHandler(this.txtProName_Validated);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // cbEditProCode
            // 
            resources.ApplyResources(this.cbEditProCode, "cbEditProCode");
            this.cbEditProCode.Name = "cbEditProCode";
            this.cbEditProCode.TabStop = false;
            this.cbEditProCode.UseVisualStyleBackColor = true;
            this.cbEditProCode.CheckedChanged += new System.EventHandler(this.cbEditProCode_CheckedChanged);
            // 
            // txtProCode
            // 
            resources.ApplyResources(this.txtProCode, "txtProCode");
            this.txtProCode.BackColor = System.Drawing.Color.White;
            this.txtProCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProCode.Data = 0;
            this.txtProCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtProCode.Format = null;
            this.txtProCode.isAllowNegative = false;
            this.txtProCode.isAllowSpecialChar = false;
            this.txtProCode.isNumberOnly = true;
            this.txtProCode.isNumeric = false;
            this.txtProCode.LeaveColor = System.Drawing.Color.White;
            this.txtProCode.Name = "txtProCode";
            this.txtProCode.TabStop = false;
            this.txtProCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProCode.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblProCode
            // 
            resources.ApplyResources(this.lblProCode, "lblProCode");
            this.lblProCode.Name = "lblProCode";
            // 
            // cmbProType
            // 
            resources.ApplyResources(this.cmbProType, "cmbProType");
            this.cmbProType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProType.FormattingEnabled = true;
            this.cmbProType.Items.AddRange(new object[] {
            resources.GetString("cmbProType.Items"),
            resources.GetString("cmbProType.Items1"),
            resources.GetString("cmbProType.Items2"),
            resources.GetString("cmbProType.Items3")});
            this.cmbProType.LeaveColor = System.Drawing.Color.White;
            this.cmbProType.Name = "cmbProType";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // tabAddDetails
            // 
            resources.ApplyResources(this.tabAddDetails, "tabAddDetails");
            this.tabAddDetails.Controls.Add(this.cmbRestaurantType);
            this.tabAddDetails.Controls.Add(this.cmbPriority);
            this.tabAddDetails.Controls.Add(this.cbRateBasedWeight);
            this.tabAddDetails.Controls.Add(this.panel1);
            this.tabAddDetails.Controls.Add(this.cbScale);
            this.tabAddDetails.Controls.Add(this.cbPercentageBased);
            this.tabAddDetails.Controls.Add(this.cbFavaurite);
            this.tabAddDetails.Controls.Add(this.cbMultiple);
            this.tabAddDetails.Controls.Add(this.txtSlsDiscP);
            this.tabAddDetails.Controls.Add(this.label21);
            this.tabAddDetails.Controls.Add(this.txtSlsDiscAmnt);
            this.tabAddDetails.Controls.Add(this.label22);
            this.tabAddDetails.Controls.Add(this.txtCookingTime);
            this.tabAddDetails.Controls.Add(this.txtPurDiscP);
            this.tabAddDetails.Controls.Add(this.label20);
            this.tabAddDetails.Controls.Add(this.txtPurDisAmnt);
            this.tabAddDetails.Controls.Add(this.label19);
            this.tabAddDetails.Controls.Add(this.txtRemarks);
            this.tabAddDetails.Controls.Add(this.txtItemCalory);
            this.tabAddDetails.Controls.Add(this.txtExiCommission);
            this.tabAddDetails.Controls.Add(this.txtProInLocal);
            this.tabAddDetails.Controls.Add(this.txtAliasName);
            this.tabAddDetails.Controls.Add(this.txtAliasCode);
            this.tabAddDetails.Controls.Add(this.label26);
            this.tabAddDetails.Controls.Add(this.label3);
            this.tabAddDetails.Controls.Add(this.label4);
            this.tabAddDetails.Controls.Add(this.label11);
            this.tabAddDetails.Controls.Add(this.label8);
            this.tabAddDetails.Controls.Add(this.label2);
            this.tabAddDetails.Controls.Add(this.label25);
            this.tabAddDetails.Controls.Add(this.label24);
            this.tabAddDetails.Controls.Add(this.label23);
            this.tabAddDetails.Name = "tabAddDetails";
            this.tabAddDetails.UseVisualStyleBackColor = true;
            // 
            // cmbRestaurantType
            // 
            resources.ApplyResources(this.cmbRestaurantType, "cmbRestaurantType");
            this.cmbRestaurantType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRestaurantType.FormattingEnabled = true;
            this.cmbRestaurantType.Items.AddRange(new object[] {
            resources.GetString("cmbRestaurantType.Items"),
            resources.GetString("cmbRestaurantType.Items1"),
            resources.GetString("cmbRestaurantType.Items2")});
            this.cmbRestaurantType.LeaveColor = System.Drawing.Color.White;
            this.cmbRestaurantType.Name = "cmbRestaurantType";
            // 
            // cmbPriority
            // 
            resources.ApplyResources(this.cmbPriority, "cmbPriority");
            this.cmbPriority.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            resources.GetString("cmbPriority.Items"),
            resources.GetString("cmbPriority.Items1"),
            resources.GetString("cmbPriority.Items2")});
            this.cmbPriority.LeaveColor = System.Drawing.Color.White;
            this.cmbPriority.Name = "cmbPriority";
            // 
            // cbRateBasedWeight
            // 
            resources.ApplyResources(this.cbRateBasedWeight, "cbRateBasedWeight");
            this.cbRateBasedWeight.Name = "cbRateBasedWeight";
            this.cbRateBasedWeight.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rbNone);
            this.panel1.Controls.Add(this.rbTranslate);
            this.panel1.Controls.Add(this.rbTransliterate);
            this.panel1.Name = "panel1";
            // 
            // rbNone
            // 
            resources.ApplyResources(this.rbNone, "rbNone");
            this.rbNone.Checked = true;
            this.rbNone.Name = "rbNone";
            this.rbNone.TabStop = true;
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // rbTranslate
            // 
            resources.ApplyResources(this.rbTranslate, "rbTranslate");
            this.rbTranslate.Name = "rbTranslate";
            this.rbTranslate.UseVisualStyleBackColor = true;
            // 
            // rbTransliterate
            // 
            resources.ApplyResources(this.rbTransliterate, "rbTransliterate");
            this.rbTransliterate.Name = "rbTransliterate";
            this.rbTransliterate.UseVisualStyleBackColor = true;
            // 
            // cbScale
            // 
            resources.ApplyResources(this.cbScale, "cbScale");
            this.cbScale.Name = "cbScale";
            this.cbScale.UseVisualStyleBackColor = true;
            // 
            // cbPercentageBased
            // 
            resources.ApplyResources(this.cbPercentageBased, "cbPercentageBased");
            this.cbPercentageBased.Checked = true;
            this.cbPercentageBased.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPercentageBased.Name = "cbPercentageBased";
            this.cbPercentageBased.UseVisualStyleBackColor = true;
            // 
            // cbFavaurite
            // 
            resources.ApplyResources(this.cbFavaurite, "cbFavaurite");
            this.cbFavaurite.Name = "cbFavaurite";
            this.cbFavaurite.UseVisualStyleBackColor = true;
            // 
            // cbMultiple
            // 
            resources.ApplyResources(this.cbMultiple, "cbMultiple");
            this.cbMultiple.Name = "cbMultiple";
            this.cbMultiple.UseVisualStyleBackColor = true;
            // 
            // txtSlsDiscP
            // 
            resources.ApplyResources(this.txtSlsDiscP, "txtSlsDiscP");
            this.txtSlsDiscP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsDiscP.Data = 0;
            this.txtSlsDiscP.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsDiscP.Format = null;
            this.txtSlsDiscP.isAllowNegative = false;
            this.txtSlsDiscP.isAllowSpecialChar = false;
            this.txtSlsDiscP.isNumberOnly = false;
            this.txtSlsDiscP.isNumeric = true;
            this.txtSlsDiscP.LeaveColor = System.Drawing.Color.White;
            this.txtSlsDiscP.Name = "txtSlsDiscP";
            this.txtSlsDiscP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSlsDiscP.Validated += new System.EventHandler(this.txtSlsDiscP_Validated);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // txtSlsDiscAmnt
            // 
            resources.ApplyResources(this.txtSlsDiscAmnt, "txtSlsDiscAmnt");
            this.txtSlsDiscAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsDiscAmnt.Data = 0;
            this.txtSlsDiscAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsDiscAmnt.Format = null;
            this.txtSlsDiscAmnt.isAllowNegative = false;
            this.txtSlsDiscAmnt.isAllowSpecialChar = false;
            this.txtSlsDiscAmnt.isNumberOnly = false;
            this.txtSlsDiscAmnt.isNumeric = true;
            this.txtSlsDiscAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtSlsDiscAmnt.Name = "txtSlsDiscAmnt";
            this.txtSlsDiscAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSlsDiscAmnt.Validated += new System.EventHandler(this.txtSlsDiscAmnt_Validated);
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // txtCookingTime
            // 
            resources.ApplyResources(this.txtCookingTime, "txtCookingTime");
            this.txtCookingTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCookingTime.Data = 0;
            this.txtCookingTime.EnterColor = System.Drawing.Color.LightPink;
            this.txtCookingTime.Format = null;
            this.txtCookingTime.isAllowNegative = false;
            this.txtCookingTime.isAllowSpecialChar = false;
            this.txtCookingTime.isNumberOnly = true;
            this.txtCookingTime.isNumeric = false;
            this.txtCookingTime.LeaveColor = System.Drawing.Color.White;
            this.txtCookingTime.Name = "txtCookingTime";
            this.txtCookingTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCookingTime.Validated += new System.EventHandler(this.txtPurDiscP_Validated);
            // 
            // txtPurDiscP
            // 
            resources.ApplyResources(this.txtPurDiscP, "txtPurDiscP");
            this.txtPurDiscP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurDiscP.Data = 0;
            this.txtPurDiscP.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurDiscP.Format = null;
            this.txtPurDiscP.isAllowNegative = false;
            this.txtPurDiscP.isAllowSpecialChar = false;
            this.txtPurDiscP.isNumberOnly = false;
            this.txtPurDiscP.isNumeric = true;
            this.txtPurDiscP.LeaveColor = System.Drawing.Color.White;
            this.txtPurDiscP.Name = "txtPurDiscP";
            this.txtPurDiscP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurDiscP.Validated += new System.EventHandler(this.txtPurDiscP_Validated);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // txtPurDisAmnt
            // 
            resources.ApplyResources(this.txtPurDisAmnt, "txtPurDisAmnt");
            this.txtPurDisAmnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurDisAmnt.Data = 0;
            this.txtPurDisAmnt.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurDisAmnt.Format = null;
            this.txtPurDisAmnt.isAllowNegative = false;
            this.txtPurDisAmnt.isAllowSpecialChar = false;
            this.txtPurDisAmnt.isNumberOnly = false;
            this.txtPurDisAmnt.isNumeric = true;
            this.txtPurDisAmnt.LeaveColor = System.Drawing.Color.White;
            this.txtPurDisAmnt.Name = "txtPurDisAmnt";
            this.txtPurDisAmnt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurDisAmnt.Validated += new System.EventHandler(this.txtPurDisAmnt_Validated);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.Data = 0;
            this.txtRemarks.EnterColor = System.Drawing.Color.LightPink;
            this.txtRemarks.Format = null;
            this.txtRemarks.isAllowNegative = false;
            this.txtRemarks.isAllowSpecialChar = false;
            this.txtRemarks.isNumberOnly = false;
            this.txtRemarks.isNumeric = false;
            this.txtRemarks.LeaveColor = System.Drawing.Color.White;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtItemCalory
            // 
            resources.ApplyResources(this.txtItemCalory, "txtItemCalory");
            this.txtItemCalory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemCalory.Data = 0;
            this.txtItemCalory.EnterColor = System.Drawing.Color.LightPink;
            this.txtItemCalory.Format = null;
            this.txtItemCalory.isAllowNegative = false;
            this.txtItemCalory.isAllowSpecialChar = false;
            this.txtItemCalory.isNumberOnly = true;
            this.txtItemCalory.isNumeric = false;
            this.txtItemCalory.LeaveColor = System.Drawing.Color.White;
            this.txtItemCalory.Name = "txtItemCalory";
            this.txtItemCalory.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtExiCommission
            // 
            resources.ApplyResources(this.txtExiCommission, "txtExiCommission");
            this.txtExiCommission.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExiCommission.Data = 0;
            this.txtExiCommission.EnterColor = System.Drawing.Color.LightPink;
            this.txtExiCommission.Format = null;
            this.txtExiCommission.isAllowNegative = false;
            this.txtExiCommission.isAllowSpecialChar = false;
            this.txtExiCommission.isNumberOnly = false;
            this.txtExiCommission.isNumeric = true;
            this.txtExiCommission.LeaveColor = System.Drawing.Color.White;
            this.txtExiCommission.Name = "txtExiCommission";
            this.txtExiCommission.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtProInLocal
            // 
            resources.ApplyResources(this.txtProInLocal, "txtProInLocal");
            this.txtProInLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProInLocal.Data = 0;
            this.txtProInLocal.EnterColor = System.Drawing.Color.LightPink;
            this.txtProInLocal.Format = null;
            this.txtProInLocal.isAllowNegative = false;
            this.txtProInLocal.isAllowSpecialChar = false;
            this.txtProInLocal.isNumberOnly = false;
            this.txtProInLocal.isNumeric = false;
            this.txtProInLocal.LeaveColor = System.Drawing.Color.White;
            this.txtProInLocal.Name = "txtProInLocal";
            this.txtProInLocal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAliasName
            // 
            resources.ApplyResources(this.txtAliasName, "txtAliasName");
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAliasName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAliasName.Data = 0;
            this.txtAliasName.EnterColor = System.Drawing.Color.LightPink;
            this.txtAliasName.Format = null;
            this.txtAliasName.isAllowNegative = false;
            this.txtAliasName.isAllowSpecialChar = false;
            this.txtAliasName.isNumberOnly = false;
            this.txtAliasName.isNumeric = false;
            this.txtAliasName.LeaveColor = System.Drawing.Color.White;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAliasCode
            // 
            resources.ApplyResources(this.txtAliasCode, "txtAliasCode");
            this.txtAliasCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAliasCode.Data = 0;
            this.txtAliasCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtAliasCode.Format = null;
            this.txtAliasCode.isAllowNegative = false;
            this.txtAliasCode.isAllowSpecialChar = false;
            this.txtAliasCode.isNumberOnly = true;
            this.txtAliasCode.isNumeric = false;
            this.txtAliasCode.LeaveColor = System.Drawing.Color.White;
            this.txtAliasCode.Name = "txtAliasCode";
            this.txtAliasCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // tabMultiUnit
            // 
            resources.ApplyResources(this.tabMultiUnit, "tabMultiUnit");
            this.tabMultiUnit.BackColor = System.Drawing.Color.White;
            this.tabMultiUnit.Controls.Add(this.grpUnit);
            this.tabMultiUnit.Name = "tabMultiUnit";
            // 
            // grpUnit
            // 
            resources.ApplyResources(this.grpUnit, "grpUnit");
            this.grpUnit.BackColor = System.Drawing.Color.DodgerBlue;
            this.grpUnit.Controls.Add(this.panel2);
            this.grpUnit.Controls.Add(this.pnlUnit);
            this.grpUnit.Name = "grpUnit";
            this.grpUnit.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cmbRptUnit);
            this.panel2.Controls.Add(this.label60);
            this.panel2.Controls.Add(this.cmbPurUnit);
            this.panel2.Controls.Add(this.label59);
            this.panel2.Controls.Add(this.cmbSalesUnit);
            this.panel2.Controls.Add(this.label58);
            this.panel2.Controls.Add(this.label57);
            this.panel2.Name = "panel2";
            // 
            // cmbRptUnit
            // 
            resources.ApplyResources(this.cmbRptUnit, "cmbRptUnit");
            this.cmbRptUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRptUnit.FormattingEnabled = true;
            this.cmbRptUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbRptUnit.Name = "cmbRptUnit";
            // 
            // label60
            // 
            resources.ApplyResources(this.label60, "label60");
            this.label60.Name = "label60";
            // 
            // cmbPurUnit
            // 
            resources.ApplyResources(this.cmbPurUnit, "cmbPurUnit");
            this.cmbPurUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPurUnit.FormattingEnabled = true;
            this.cmbPurUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbPurUnit.Name = "cmbPurUnit";
            // 
            // label59
            // 
            resources.ApplyResources(this.label59, "label59");
            this.label59.Name = "label59";
            // 
            // cmbSalesUnit
            // 
            resources.ApplyResources(this.cmbSalesUnit, "cmbSalesUnit");
            this.cmbSalesUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSalesUnit.FormattingEnabled = true;
            this.cmbSalesUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbSalesUnit.Name = "cmbSalesUnit";
            // 
            // label58
            // 
            resources.ApplyResources(this.label58, "label58");
            this.label58.Name = "label58";
            // 
            // label57
            // 
            resources.ApplyResources(this.label57, "label57");
            this.label57.Name = "label57";
            // 
            // pnlUnit
            // 
            resources.ApplyResources(this.pnlUnit, "pnlUnit");
            this.pnlUnit.BackColor = System.Drawing.Color.White;
            this.pnlUnit.Controls.Add(this.cbMultiUnitTranslate);
            this.pnlUnit.Controls.Add(this.cbAutoGenerated);
            this.pnlUnit.Controls.Add(this.cbMultiUnitTransliterate);
            this.pnlUnit.Controls.Add(this.label1);
            this.pnlUnit.Controls.Add(this.txtLocalMultiUnit);
            this.pnlUnit.Controls.Add(this.dgMultiUnit);
            this.pnlUnit.Controls.Add(this.cmbUnitMulti);
            this.pnlUnit.Controls.Add(this.cbMultiUnitInclusive);
            this.pnlUnit.Controls.Add(this.label27);
            this.pnlUnit.Controls.Add(this.btnAddMultiUnit);
            this.pnlUnit.Controls.Add(this.label28);
            this.pnlUnit.Controls.Add(this.txtMultiUnitOppeningStock);
            this.pnlUnit.Controls.Add(this.txtMultiUnitProductName);
            this.pnlUnit.Controls.Add(this.label31);
            this.pnlUnit.Controls.Add(this.label32);
            this.pnlUnit.Controls.Add(this.txtMultiUnitBarcode);
            this.pnlUnit.Controls.Add(this.txtUnitQty);
            this.pnlUnit.Controls.Add(this.label34);
            this.pnlUnit.Controls.Add(this.label35);
            this.pnlUnit.Controls.Add(this.txtMultiUnitprice);
            this.pnlUnit.Name = "pnlUnit";
            // 
            // cbMultiUnitTranslate
            // 
            resources.ApplyResources(this.cbMultiUnitTranslate, "cbMultiUnitTranslate");
            this.cbMultiUnitTranslate.Name = "cbMultiUnitTranslate";
            this.cbMultiUnitTranslate.TabStop = false;
            this.cbMultiUnitTranslate.UseVisualStyleBackColor = true;
            // 
            // cbAutoGenerated
            // 
            resources.ApplyResources(this.cbAutoGenerated, "cbAutoGenerated");
            this.cbAutoGenerated.Checked = true;
            this.cbAutoGenerated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoGenerated.Name = "cbAutoGenerated";
            this.cbAutoGenerated.UseVisualStyleBackColor = true;
            // 
            // cbMultiUnitTransliterate
            // 
            resources.ApplyResources(this.cbMultiUnitTransliterate, "cbMultiUnitTransliterate");
            this.cbMultiUnitTransliterate.Checked = true;
            this.cbMultiUnitTransliterate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMultiUnitTransliterate.Name = "cbMultiUnitTransliterate";
            this.cbMultiUnitTransliterate.TabStop = false;
            this.cbMultiUnitTransliterate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtLocalMultiUnit
            // 
            resources.ApplyResources(this.txtLocalMultiUnit, "txtLocalMultiUnit");
            this.txtLocalMultiUnit.BackColor = System.Drawing.Color.White;
            this.txtLocalMultiUnit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalMultiUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalMultiUnit.Data = 0;
            this.txtLocalMultiUnit.EnterColor = System.Drawing.Color.LightPink;
            this.txtLocalMultiUnit.Format = null;
            this.txtLocalMultiUnit.isAllowNegative = false;
            this.txtLocalMultiUnit.isAllowSpecialChar = false;
            this.txtLocalMultiUnit.isNumberOnly = false;
            this.txtLocalMultiUnit.isNumeric = false;
            this.txtLocalMultiUnit.LeaveColor = System.Drawing.Color.White;
            this.txtLocalMultiUnit.Name = "txtLocalMultiUnit";
            this.txtLocalMultiUnit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dgMultiUnit
            // 
            resources.ApplyResources(this.dgMultiUnit, "dgMultiUnit");
            this.dgMultiUnit.AllowUserToAddRows = false;
            this.dgMultiUnit.AllowUserToDeleteRows = false;
            this.dgMultiUnit.BackgroundColor = System.Drawing.Color.White;
            this.dgMultiUnit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiUnit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Name,
            this.col_Barcode,
            this.col_Qty,
            this.col_Price,
            this.col_Close1});
            this.dgMultiUnit.Name = "dgMultiUnit";
            this.dgMultiUnit.ReadOnly = true;
            this.dgMultiUnit.RowHeadersVisible = false;
            this.dgMultiUnit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiUnit_CellClick);
            this.dgMultiUnit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiUnit_CellContentClick);
            this.dgMultiUnit.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMultiUnit_DataError);
            // 
            // col_Name
            // 
            this.col_Name.DataPropertyName = "ProductUnitName";
            resources.ApplyResources(this.col_Name, "col_Name");
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            // 
            // col_Barcode
            // 
            this.col_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_Barcode, "col_Barcode");
            this.col_Barcode.Name = "col_Barcode";
            this.col_Barcode.ReadOnly = true;
            // 
            // col_Qty
            // 
            this.col_Qty.DataPropertyName = "UnitQty";
            resources.ApplyResources(this.col_Qty, "col_Qty");
            this.col_Qty.Name = "col_Qty";
            this.col_Qty.ReadOnly = true;
            // 
            // col_Price
            // 
            this.col_Price.DataPropertyName = "SalesPrice";
            resources.ApplyResources(this.col_Price, "col_Price");
            this.col_Price.Name = "col_Price";
            this.col_Price.ReadOnly = true;
            // 
            // col_Close1
            // 
            resources.ApplyResources(this.col_Close1, "col_Close1");
            this.col_Close1.Name = "col_Close1";
            this.col_Close1.ReadOnly = true;
            // 
            // cmbUnitMulti
            // 
            resources.ApplyResources(this.cmbUnitMulti, "cmbUnitMulti");
            this.cmbUnitMulti.BackColor = System.Drawing.Color.White;
            this.cmbUnitMulti.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnitMulti.FormattingEnabled = true;
            this.cmbUnitMulti.LeaveColor = System.Drawing.Color.White;
            this.cmbUnitMulti.Name = "cmbUnitMulti";
            this.cmbUnitMulti.SelectedIndexChanged += new System.EventHandler(this.cmbUnitMulti_SelectedIndexChanged);
            // 
            // cbMultiUnitInclusive
            // 
            resources.ApplyResources(this.cbMultiUnitInclusive, "cbMultiUnitInclusive");
            this.cbMultiUnitInclusive.Checked = true;
            this.cbMultiUnitInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMultiUnitInclusive.Name = "cbMultiUnitInclusive";
            this.cbMultiUnitInclusive.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // btnAddMultiUnit
            // 
            resources.ApplyResources(this.btnAddMultiUnit, "btnAddMultiUnit");
            this.btnAddMultiUnit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddMultiUnit.ForeColor = System.Drawing.Color.White;
            this.btnAddMultiUnit.Name = "btnAddMultiUnit";
            this.btnAddMultiUnit.UseVisualStyleBackColor = false;
            this.btnAddMultiUnit.Click += new System.EventHandler(this.btnAddMultiUnit_Click);
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // txtMultiUnitOppeningStock
            // 
            resources.ApplyResources(this.txtMultiUnitOppeningStock, "txtMultiUnitOppeningStock");
            this.txtMultiUnitOppeningStock.BackColor = System.Drawing.Color.White;
            this.txtMultiUnitOppeningStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiUnitOppeningStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMultiUnitOppeningStock.Data = 0;
            this.txtMultiUnitOppeningStock.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiUnitOppeningStock.Format = null;
            this.txtMultiUnitOppeningStock.isAllowNegative = false;
            this.txtMultiUnitOppeningStock.isAllowSpecialChar = false;
            this.txtMultiUnitOppeningStock.isNumberOnly = false;
            this.txtMultiUnitOppeningStock.isNumeric = true;
            this.txtMultiUnitOppeningStock.LeaveColor = System.Drawing.Color.White;
            this.txtMultiUnitOppeningStock.Name = "txtMultiUnitOppeningStock";
            this.txtMultiUnitOppeningStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMultiUnitProductName
            // 
            resources.ApplyResources(this.txtMultiUnitProductName, "txtMultiUnitProductName");
            this.txtMultiUnitProductName.BackColor = System.Drawing.Color.White;
            this.txtMultiUnitProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiUnitProductName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMultiUnitProductName.Data = 0;
            this.txtMultiUnitProductName.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiUnitProductName.Format = null;
            this.txtMultiUnitProductName.isAllowNegative = false;
            this.txtMultiUnitProductName.isAllowSpecialChar = false;
            this.txtMultiUnitProductName.isNumberOnly = false;
            this.txtMultiUnitProductName.isNumeric = false;
            this.txtMultiUnitProductName.LeaveColor = System.Drawing.Color.White;
            this.txtMultiUnitProductName.Name = "txtMultiUnitProductName";
            this.txtMultiUnitProductName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMultiUnitProductName.Validated += new System.EventHandler(this.txtMultiUnitProductName_Validated);
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // txtMultiUnitBarcode
            // 
            resources.ApplyResources(this.txtMultiUnitBarcode, "txtMultiUnitBarcode");
            this.txtMultiUnitBarcode.BackColor = System.Drawing.Color.White;
            this.txtMultiUnitBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiUnitBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMultiUnitBarcode.Data = 0;
            this.txtMultiUnitBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiUnitBarcode.Format = null;
            this.txtMultiUnitBarcode.isAllowNegative = false;
            this.txtMultiUnitBarcode.isAllowSpecialChar = false;
            this.txtMultiUnitBarcode.isNumberOnly = false;
            this.txtMultiUnitBarcode.isNumeric = false;
            this.txtMultiUnitBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtMultiUnitBarcode.Name = "txtMultiUnitBarcode";
            this.txtMultiUnitBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMultiUnitBarcode.Validated += new System.EventHandler(this.txtMultiUnitBarcode_Validated);
            // 
            // txtUnitQty
            // 
            resources.ApplyResources(this.txtUnitQty, "txtUnitQty");
            this.txtUnitQty.BackColor = System.Drawing.Color.White;
            this.txtUnitQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnitQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnitQty.Data = 0;
            this.txtUnitQty.EnterColor = System.Drawing.Color.LightPink;
            this.txtUnitQty.Format = null;
            this.txtUnitQty.isAllowNegative = false;
            this.txtUnitQty.isAllowSpecialChar = false;
            this.txtUnitQty.isNumberOnly = false;
            this.txtUnitQty.isNumeric = true;
            this.txtUnitQty.LeaveColor = System.Drawing.Color.White;
            this.txtUnitQty.Name = "txtUnitQty";
            this.txtUnitQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitQty.Validated += new System.EventHandler(this.txtUnitQty_Validated);
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // txtMultiUnitprice
            // 
            resources.ApplyResources(this.txtMultiUnitprice, "txtMultiUnitprice");
            this.txtMultiUnitprice.BackColor = System.Drawing.Color.White;
            this.txtMultiUnitprice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiUnitprice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMultiUnitprice.Data = 0;
            this.txtMultiUnitprice.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiUnitprice.Format = null;
            this.txtMultiUnitprice.isAllowNegative = false;
            this.txtMultiUnitprice.isAllowSpecialChar = false;
            this.txtMultiUnitprice.isNumberOnly = false;
            this.txtMultiUnitprice.isNumeric = true;
            this.txtMultiUnitprice.LeaveColor = System.Drawing.Color.White;
            this.txtMultiUnitprice.Name = "txtMultiUnitprice";
            this.txtMultiUnitprice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // tabMultiRates
            // 
            resources.ApplyResources(this.tabMultiRates, "tabMultiRates");
            this.tabMultiRates.BackColor = System.Drawing.Color.White;
            this.tabMultiRates.Controls.Add(this.dgMultiRates);
            this.tabMultiRates.Controls.Add(this.panel4);
            this.tabMultiRates.Name = "tabMultiRates";
            // 
            // dgMultiRates
            // 
            resources.ApplyResources(this.dgMultiRates, "dgMultiRates");
            this.dgMultiRates.AllowUserToAddRows = false;
            this.dgMultiRates.AllowUserToDeleteRows = false;
            this.dgMultiRates.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMultiRates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMultiRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_MultiRate_SL,
            this.col_MultiRates_PCategory,
            this.col_ProName,
            this.col_MultiRates_Unit,
            this.col_MultiRates_SaleRate,
            this.col_MultiRates_SalesDiscount,
            this.col_MultiRates_PurRate,
            this.col_MultiRates_PurDiscount,
            this.col_MultiRatesBarcode,
            this.col_Delete});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMultiRates.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgMultiRates.Name = "dgMultiRates";
            this.dgMultiRates.RowHeadersVisible = false;
            this.dgMultiRates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiRates_CellClick);
            // 
            // col_MultiRate_SL
            // 
            this.col_MultiRate_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_MultiRate_SL, "col_MultiRate_SL");
            this.col_MultiRate_SL.Name = "col_MultiRate_SL";
            // 
            // col_MultiRates_PCategory
            // 
            this.col_MultiRates_PCategory.DataPropertyName = "PCategoryName";
            resources.ApplyResources(this.col_MultiRates_PCategory, "col_MultiRates_PCategory");
            this.col_MultiRates_PCategory.Name = "col_MultiRates_PCategory";
            // 
            // col_ProName
            // 
            this.col_ProName.DataPropertyName = "ProName";
            resources.ApplyResources(this.col_ProName, "col_ProName");
            this.col_ProName.Name = "col_ProName";
            // 
            // col_MultiRates_Unit
            // 
            this.col_MultiRates_Unit.DataPropertyName = "UnitName";
            resources.ApplyResources(this.col_MultiRates_Unit, "col_MultiRates_Unit");
            this.col_MultiRates_Unit.Name = "col_MultiRates_Unit";
            // 
            // col_MultiRates_SaleRate
            // 
            this.col_MultiRates_SaleRate.DataPropertyName = "SPrice";
            resources.ApplyResources(this.col_MultiRates_SaleRate, "col_MultiRates_SaleRate");
            this.col_MultiRates_SaleRate.Name = "col_MultiRates_SaleRate";
            // 
            // col_MultiRates_SalesDiscount
            // 
            this.col_MultiRates_SalesDiscount.DataPropertyName = "SPDisc";
            resources.ApplyResources(this.col_MultiRates_SalesDiscount, "col_MultiRates_SalesDiscount");
            this.col_MultiRates_SalesDiscount.Name = "col_MultiRates_SalesDiscount";
            // 
            // col_MultiRates_PurRate
            // 
            this.col_MultiRates_PurRate.DataPropertyName = "PPrice";
            resources.ApplyResources(this.col_MultiRates_PurRate, "col_MultiRates_PurRate");
            this.col_MultiRates_PurRate.Name = "col_MultiRates_PurRate";
            // 
            // col_MultiRates_PurDiscount
            // 
            this.col_MultiRates_PurDiscount.DataPropertyName = "PDisc";
            resources.ApplyResources(this.col_MultiRates_PurDiscount, "col_MultiRates_PurDiscount");
            this.col_MultiRates_PurDiscount.Name = "col_MultiRates_PurDiscount";
            // 
            // col_MultiRatesBarcode
            // 
            this.col_MultiRatesBarcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_MultiRatesBarcode, "col_MultiRatesBarcode");
            this.col_MultiRatesBarcode.Name = "col_MultiRatesBarcode";
            // 
            // col_Delete
            // 
            resources.ApplyResources(this.col_Delete, "col_Delete");
            this.col_Delete.Name = "col_Delete";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel4.Name = "panel4";
            // 
            // tabMultiBarcode
            // 
            resources.ApplyResources(this.tabMultiBarcode, "tabMultiBarcode");
            this.tabMultiBarcode.Controls.Add(this.dgMultiBarcode);
            this.tabMultiBarcode.Controls.Add(this.cmbUnitMultiBarcode);
            this.tabMultiBarcode.Controls.Add(this.label36);
            this.tabMultiBarcode.Controls.Add(this.txtMultiBarcode);
            this.tabMultiBarcode.Controls.Add(this.btnAddMultiBarcode);
            this.tabMultiBarcode.Controls.Add(this.label37);
            this.tabMultiBarcode.Name = "tabMultiBarcode";
            this.tabMultiBarcode.UseVisualStyleBackColor = true;
            // 
            // dgMultiBarcode
            // 
            resources.ApplyResources(this.dgMultiBarcode, "dgMultiBarcode");
            this.dgMultiBarcode.AllowUserToAddRows = false;
            this.dgMultiBarcode.AllowUserToDeleteRows = false;
            this.dgMultiBarcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgMultiBarcode.BackgroundColor = System.Drawing.Color.White;
            this.dgMultiBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMultiBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiBarcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_MultiBarcode,
            this.col_BarcodeType,
            this.col_Close});
            this.dgMultiBarcode.Name = "dgMultiBarcode";
            this.dgMultiBarcode.ReadOnly = true;
            this.dgMultiBarcode.RowHeadersVisible = false;
            this.dgMultiBarcode.TabStop = false;
            this.dgMultiBarcode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiBarcode_CellClick);
            this.dgMultiBarcode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiBarcode_CellContentClick);
            this.dgMultiBarcode.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMultiBarcode_DataError);
            // 
            // col_SL
            // 
            this.col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_MultiBarcode
            // 
            this.col_MultiBarcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_MultiBarcode, "col_MultiBarcode");
            this.col_MultiBarcode.Name = "col_MultiBarcode";
            this.col_MultiBarcode.ReadOnly = true;
            // 
            // col_BarcodeType
            // 
            this.col_BarcodeType.DataPropertyName = "BarcodeType";
            resources.ApplyResources(this.col_BarcodeType, "col_BarcodeType");
            this.col_BarcodeType.Name = "col_BarcodeType";
            this.col_BarcodeType.ReadOnly = true;
            // 
            // col_Close
            // 
            this.col_Close.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.col_Close, "col_Close");
            this.col_Close.Name = "col_Close";
            this.col_Close.ReadOnly = true;
            // 
            // cmbUnitMultiBarcode
            // 
            resources.ApplyResources(this.cmbUnitMultiBarcode, "cmbUnitMultiBarcode");
            this.cmbUnitMultiBarcode.BackColor = System.Drawing.Color.White;
            this.cmbUnitMultiBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnitMultiBarcode.FormattingEnabled = true;
            this.cmbUnitMultiBarcode.LeaveColor = System.Drawing.Color.White;
            this.cmbUnitMultiBarcode.Name = "cmbUnitMultiBarcode";
            this.cmbUnitMultiBarcode.TabStop = false;
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // txtMultiBarcode
            // 
            resources.ApplyResources(this.txtMultiBarcode, "txtMultiBarcode");
            this.txtMultiBarcode.BackColor = System.Drawing.Color.White;
            this.txtMultiBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMultiBarcode.Data = 0;
            this.txtMultiBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiBarcode.Format = null;
            this.txtMultiBarcode.isAllowNegative = false;
            this.txtMultiBarcode.isAllowSpecialChar = false;
            this.txtMultiBarcode.isNumberOnly = false;
            this.txtMultiBarcode.isNumeric = false;
            this.txtMultiBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtMultiBarcode.Name = "txtMultiBarcode";
            this.txtMultiBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMultiBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMultiBarcode_KeyDown);
            this.txtMultiBarcode.Validated += new System.EventHandler(this.txtMultiBarcode_Validated);
            // 
            // btnAddMultiBarcode
            // 
            resources.ApplyResources(this.btnAddMultiBarcode, "btnAddMultiBarcode");
            this.btnAddMultiBarcode.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddMultiBarcode.ForeColor = System.Drawing.Color.White;
            this.btnAddMultiBarcode.Name = "btnAddMultiBarcode";
            this.btnAddMultiBarcode.TabStop = false;
            this.btnAddMultiBarcode.UseVisualStyleBackColor = false;
            this.btnAddMultiBarcode.Click += new System.EventHandler(this.btnAddMultiBarcode_Click);
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // taImage
            // 
            resources.ApplyResources(this.taImage, "taImage");
            this.taImage.BackColor = System.Drawing.Color.White;
            this.taImage.Controls.Add(this.btnSaveImage);
            this.taImage.Controls.Add(this.btnRemove);
            this.taImage.Controls.Add(this.panel3);
            this.taImage.Controls.Add(this.btnBrouse);
            this.taImage.Name = "taImage";
            // 
            // btnSaveImage
            // 
            resources.ApplyResources(this.btnSaveImage, "btnSaveImage");
            this.btnSaveImage.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveImage.ForeColor = System.Drawing.Color.White;
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.UseVisualStyleBackColor = false;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel3.Controls.Add(this.pbProImage);
            this.panel3.Name = "panel3";
            // 
            // pbProImage
            // 
            resources.ApplyResources(this.pbProImage, "pbProImage");
            this.pbProImage.BackColor = System.Drawing.Color.Snow;
            this.pbProImage.Name = "pbProImage";
            this.pbProImage.TabStop = false;
            // 
            // btnBrouse
            // 
            resources.ApplyResources(this.btnBrouse, "btnBrouse");
            this.btnBrouse.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBrouse.ForeColor = System.Drawing.Color.White;
            this.btnBrouse.Name = "btnBrouse";
            this.btnBrouse.UseVisualStyleBackColor = false;
            this.btnBrouse.Click += new System.EventHandler(this.btnBrouse_Click);
            // 
            // tabSize
            // 
            resources.ApplyResources(this.tabSize, "tabSize");
            this.tabSize.Controls.Add(this.pnlMultiSize);
            this.tabSize.Controls.Add(this.dgSize);
            this.tabSize.Name = "tabSize";
            this.tabSize.UseVisualStyleBackColor = true;
            // 
            // pnlMultiSize
            // 
            resources.ApplyResources(this.pnlMultiSize, "pnlMultiSize");
            this.pnlMultiSize.Controls.Add(this.txtSizeName);
            this.pnlMultiSize.Controls.Add(this.btnSizeRemoveImage);
            this.pnlMultiSize.Controls.Add(this.txtSizePrice);
            this.pnlMultiSize.Controls.Add(this.panel6);
            this.pnlMultiSize.Controls.Add(this.label10);
            this.pnlMultiSize.Controls.Add(this.btnSizeAddImg);
            this.pnlMultiSize.Controls.Add(this.label7);
            this.pnlMultiSize.Controls.Add(this.cbSizeTraslate);
            this.pnlMultiSize.Controls.Add(this.label9);
            this.pnlMultiSize.Controls.Add(this.cbSizeTrasliterate);
            this.pnlMultiSize.Controls.Add(this.txtSizeBarcode);
            this.pnlMultiSize.Controls.Add(this.label5);
            this.pnlMultiSize.Controls.Add(this.label6);
            this.pnlMultiSize.Controls.Add(this.txtSizeLocalName);
            this.pnlMultiSize.Controls.Add(this.btnAddSize);
            this.pnlMultiSize.Name = "pnlMultiSize";
            // 
            // txtSizeName
            // 
            resources.ApplyResources(this.txtSizeName, "txtSizeName");
            this.txtSizeName.BackColor = System.Drawing.Color.White;
            this.txtSizeName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSizeName.Data = 0;
            this.txtSizeName.EnterColor = System.Drawing.Color.LightPink;
            this.txtSizeName.Format = null;
            this.txtSizeName.isAllowNegative = false;
            this.txtSizeName.isAllowSpecialChar = false;
            this.txtSizeName.isNumberOnly = false;
            this.txtSizeName.isNumeric = false;
            this.txtSizeName.LeaveColor = System.Drawing.Color.White;
            this.txtSizeName.Name = "txtSizeName";
            this.txtSizeName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizeName.Validated += new System.EventHandler(this.txtSizeName_Validated);
            // 
            // btnSizeRemoveImage
            // 
            resources.ApplyResources(this.btnSizeRemoveImage, "btnSizeRemoveImage");
            this.btnSizeRemoveImage.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSizeRemoveImage.ForeColor = System.Drawing.Color.White;
            this.btnSizeRemoveImage.Name = "btnSizeRemoveImage";
            this.btnSizeRemoveImage.UseVisualStyleBackColor = false;
            this.btnSizeRemoveImage.Click += new System.EventHandler(this.btnSizeRemoveImage_Click);
            // 
            // txtSizePrice
            // 
            resources.ApplyResources(this.txtSizePrice, "txtSizePrice");
            this.txtSizePrice.BackColor = System.Drawing.Color.White;
            this.txtSizePrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizePrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSizePrice.Data = 0;
            this.txtSizePrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtSizePrice.Format = null;
            this.txtSizePrice.isAllowNegative = false;
            this.txtSizePrice.isAllowSpecialChar = false;
            this.txtSizePrice.isNumberOnly = false;
            this.txtSizePrice.isNumeric = true;
            this.txtSizePrice.LeaveColor = System.Drawing.Color.White;
            this.txtSizePrice.Name = "txtSizePrice";
            this.txtSizePrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSizePrice_KeyDown);
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel6.Controls.Add(this.pbSizeImage);
            this.panel6.Name = "panel6";
            // 
            // pbSizeImage
            // 
            resources.ApplyResources(this.pbSizeImage, "pbSizeImage");
            this.pbSizeImage.BackColor = System.Drawing.Color.Snow;
            this.pbSizeImage.Name = "pbSizeImage";
            this.pbSizeImage.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // btnSizeAddImg
            // 
            resources.ApplyResources(this.btnSizeAddImg, "btnSizeAddImg");
            this.btnSizeAddImg.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSizeAddImg.ForeColor = System.Drawing.Color.White;
            this.btnSizeAddImg.Name = "btnSizeAddImg";
            this.btnSizeAddImg.UseVisualStyleBackColor = false;
            this.btnSizeAddImg.Click += new System.EventHandler(this.btnSizeAddImg_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cbSizeTraslate
            // 
            resources.ApplyResources(this.cbSizeTraslate, "cbSizeTraslate");
            this.cbSizeTraslate.Name = "cbSizeTraslate";
            this.cbSizeTraslate.TabStop = false;
            this.cbSizeTraslate.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cbSizeTrasliterate
            // 
            resources.ApplyResources(this.cbSizeTrasliterate, "cbSizeTrasliterate");
            this.cbSizeTrasliterate.Checked = true;
            this.cbSizeTrasliterate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSizeTrasliterate.Name = "cbSizeTrasliterate";
            this.cbSizeTrasliterate.TabStop = false;
            this.cbSizeTrasliterate.UseVisualStyleBackColor = true;
            // 
            // txtSizeBarcode
            // 
            resources.ApplyResources(this.txtSizeBarcode, "txtSizeBarcode");
            this.txtSizeBarcode.BackColor = System.Drawing.Color.White;
            this.txtSizeBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSizeBarcode.Data = 0;
            this.txtSizeBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtSizeBarcode.Format = null;
            this.txtSizeBarcode.isAllowNegative = false;
            this.txtSizeBarcode.isAllowSpecialChar = false;
            this.txtSizeBarcode.isNumberOnly = false;
            this.txtSizeBarcode.isNumeric = false;
            this.txtSizeBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtSizeBarcode.Name = "txtSizeBarcode";
            this.txtSizeBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizeBarcode.Validated += new System.EventHandler(this.txtSizeBarcode_Validated);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtSizeLocalName
            // 
            resources.ApplyResources(this.txtSizeLocalName, "txtSizeLocalName");
            this.txtSizeLocalName.BackColor = System.Drawing.Color.White;
            this.txtSizeLocalName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeLocalName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSizeLocalName.Data = 0;
            this.txtSizeLocalName.EnterColor = System.Drawing.Color.LightPink;
            this.txtSizeLocalName.Format = null;
            this.txtSizeLocalName.isAllowNegative = false;
            this.txtSizeLocalName.isAllowSpecialChar = false;
            this.txtSizeLocalName.isNumberOnly = false;
            this.txtSizeLocalName.isNumeric = false;
            this.txtSizeLocalName.LeaveColor = System.Drawing.Color.White;
            this.txtSizeLocalName.Name = "txtSizeLocalName";
            this.txtSizeLocalName.TabStop = false;
            this.txtSizeLocalName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnAddSize
            // 
            resources.ApplyResources(this.btnAddSize, "btnAddSize");
            this.btnAddSize.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddSize.ForeColor = System.Drawing.Color.White;
            this.btnAddSize.Name = "btnAddSize";
            this.btnAddSize.UseVisualStyleBackColor = false;
            this.btnAddSize.Click += new System.EventHandler(this.btnAddSize_Click);
            // 
            // dgSize
            // 
            resources.ApplyResources(this.dgSize, "dgSize");
            this.dgSize.AllowUserToAddRows = false;
            this.dgSize.AllowUserToDeleteRows = false;
            this.dgSize.BackgroundColor = System.Drawing.Color.White;
            this.dgSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSize.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_MultiSize_Name,
            this.Col_MultiSize_Barcode,
            this.Col_MultiSize_Price,
            this.col_SizeClose});
            this.dgSize.Name = "dgSize";
            this.dgSize.ReadOnly = true;
            this.dgSize.RowHeadersVisible = false;
            this.dgSize.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSize_CellClick);
            this.dgSize.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSize_CellContentClick);
            this.dgSize.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgSize_DataError);
            // 
            // Col_MultiSize_Name
            // 
            this.Col_MultiSize_Name.DataPropertyName = "ProductBarcodeName";
            resources.ApplyResources(this.Col_MultiSize_Name, "Col_MultiSize_Name");
            this.Col_MultiSize_Name.Name = "Col_MultiSize_Name";
            this.Col_MultiSize_Name.ReadOnly = true;
            // 
            // Col_MultiSize_Barcode
            // 
            this.Col_MultiSize_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.Col_MultiSize_Barcode, "Col_MultiSize_Barcode");
            this.Col_MultiSize_Barcode.Name = "Col_MultiSize_Barcode";
            this.Col_MultiSize_Barcode.ReadOnly = true;
            // 
            // Col_MultiSize_Price
            // 
            this.Col_MultiSize_Price.DataPropertyName = "SellingPrice";
            resources.ApplyResources(this.Col_MultiSize_Price, "Col_MultiSize_Price");
            this.Col_MultiSize_Price.Name = "Col_MultiSize_Price";
            this.Col_MultiSize_Price.ReadOnly = true;
            // 
            // col_SizeClose
            // 
            resources.ApplyResources(this.col_SizeClose, "col_SizeClose");
            this.col_SizeClose.Name = "col_SizeClose";
            this.col_SizeClose.ReadOnly = true;
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProductUnitName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Barcode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UnitQty";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SalesPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SL";
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "PCategoryName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "UnitName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "SPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "SPDisc";
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "PPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "PDisc";
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "SL";
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Barcode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "BarcodeType";
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "ProductCode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "ProductName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Barcode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn17, "dataGridViewTextBoxColumn17");
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // ashGradientPanel1
            // 
            resources.ApplyResources(this.ashGradientPanel1, "ashGradientPanel1");
            this.ashGradientPanel1.AllowMultiSelect = false;
            this.ashGradientPanel1.Angle = 0F;
            this.ashGradientPanel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.ashGradientPanel1.BottomColor = System.Drawing.Color.DodgerBlue;
            this.ashGradientPanel1.Controls.Add(this.btnSelected);
            this.ashGradientPanel1.Controls.Add(this.btnSize);
            this.ashGradientPanel1.Controls.Add(this.btnMultiBarcode);
            this.ashGradientPanel1.Controls.Add(this.btnAddDetails);
            this.ashGradientPanel1.Controls.Add(this.btnProImage);
            this.ashGradientPanel1.Controls.Add(this.btnMultiRates);
            this.ashGradientPanel1.Controls.Add(this.btnMultiUnit);
            this.ashGradientPanel1.Controls.Add(this.btnProDetails);
            this.ashGradientPanel1.Name = "ashGradientPanel1";
            this.ashGradientPanel1.Selected = false;
            this.ashGradientPanel1.TextAdjestmentHeight = 0;
            this.ashGradientPanel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ashGradientPanel1.TopColor = System.Drawing.Color.White;
            // 
            // btnSelected
            // 
            resources.ApplyResources(this.btnSelected, "btnSelected");
            this.btnSelected.AllowMultiSelect = false;
            this.btnSelected.Angle = 0F;
            this.btnSelected.BottomColor = System.Drawing.Color.Empty;
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Selected = false;
            this.btnSelected.TextAdjestmentHeight = 0;
            this.btnSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSelected.TopColor = System.Drawing.Color.Empty;
            // 
            // btnSize
            // 
            resources.ApplyResources(this.btnSize, "btnSize");
            this.btnSize.BackColor = System.Drawing.Color.Transparent;
            this.btnSize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSize.FlatAppearance.BorderSize = 0;
            this.btnSize.ForeColor = System.Drawing.Color.Black;
            this.btnSize.Name = "btnSize";
            this.btnSize.UseVisualStyleBackColor = false;
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // btnMultiBarcode
            // 
            resources.ApplyResources(this.btnMultiBarcode, "btnMultiBarcode");
            this.btnMultiBarcode.BackColor = System.Drawing.Color.Transparent;
            this.btnMultiBarcode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMultiBarcode.FlatAppearance.BorderSize = 0;
            this.btnMultiBarcode.ForeColor = System.Drawing.Color.Black;
            this.btnMultiBarcode.Name = "btnMultiBarcode";
            this.btnMultiBarcode.UseVisualStyleBackColor = false;
            this.btnMultiBarcode.Click += new System.EventHandler(this.btnMultiBarcode_Click);
            // 
            // btnAddDetails
            // 
            resources.ApplyResources(this.btnAddDetails, "btnAddDetails");
            this.btnAddDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnAddDetails.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAddDetails.FlatAppearance.BorderSize = 0;
            this.btnAddDetails.ForeColor = System.Drawing.Color.Black;
            this.btnAddDetails.Name = "btnAddDetails";
            this.btnAddDetails.UseVisualStyleBackColor = false;
            this.btnAddDetails.Click += new System.EventHandler(this.btnAddDetails_Click);
            // 
            // btnProImage
            // 
            resources.ApplyResources(this.btnProImage, "btnProImage");
            this.btnProImage.BackColor = System.Drawing.Color.Transparent;
            this.btnProImage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProImage.FlatAppearance.BorderSize = 0;
            this.btnProImage.ForeColor = System.Drawing.Color.Black;
            this.btnProImage.Name = "btnProImage";
            this.btnProImage.UseVisualStyleBackColor = false;
            this.btnProImage.Click += new System.EventHandler(this.btnProImage_Click);
            // 
            // btnMultiRates
            // 
            resources.ApplyResources(this.btnMultiRates, "btnMultiRates");
            this.btnMultiRates.BackColor = System.Drawing.Color.Transparent;
            this.btnMultiRates.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMultiRates.FlatAppearance.BorderSize = 0;
            this.btnMultiRates.ForeColor = System.Drawing.Color.Black;
            this.btnMultiRates.Name = "btnMultiRates";
            this.btnMultiRates.UseVisualStyleBackColor = false;
            this.btnMultiRates.Click += new System.EventHandler(this.btnPriceSettings_Click);
            // 
            // btnMultiUnit
            // 
            resources.ApplyResources(this.btnMultiUnit, "btnMultiUnit");
            this.btnMultiUnit.BackColor = System.Drawing.Color.Transparent;
            this.btnMultiUnit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnMultiUnit.FlatAppearance.BorderSize = 0;
            this.btnMultiUnit.ForeColor = System.Drawing.Color.Black;
            this.btnMultiUnit.Name = "btnMultiUnit";
            this.btnMultiUnit.UseVisualStyleBackColor = false;
            this.btnMultiUnit.Click += new System.EventHandler(this.btnAddUnits_Click);
            // 
            // btnProDetails
            // 
            resources.ApplyResources(this.btnProDetails, "btnProDetails");
            this.btnProDetails.BackColor = System.Drawing.Color.White;
            this.btnProDetails.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProDetails.FlatAppearance.BorderSize = 0;
            this.btnProDetails.ForeColor = System.Drawing.Color.Black;
            this.btnProDetails.Name = "btnProDetails";
            this.btnProDetails.UseVisualStyleBackColor = false;
            this.btnProDetails.Click += new System.EventHandler(this.btnProDetails_Click);
            // 
            // ashGradientPanel2
            // 
            resources.ApplyResources(this.ashGradientPanel2, "ashGradientPanel2");
            this.ashGradientPanel2.AllowMultiSelect = false;
            this.ashGradientPanel2.Angle = 0F;
            this.ashGradientPanel2.BottomColor = System.Drawing.Color.Empty;
            this.ashGradientPanel2.Name = "ashGradientPanel2";
            this.ashGradientPanel2.Selected = false;
            this.ashGradientPanel2.TextAdjestmentHeight = 0;
            this.ashGradientPanel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ashGradientPanel2.TopColor = System.Drawing.Color.Empty;
            // 
            // Product
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ashGradientPanel1);
            this.Controls.Add(this.tabProduct);
            this.MaximizeBox = false;
            this.Name = "Product";
            this.Load += new System.EventHandler(this.Product_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Product_KeyDown);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.tabProduct, 0);
            this.Controls.SetChildIndex(this.ashGradientPanel1, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.pnlButton.ResumeLayout(false);
            this.tabProduct.ResumeLayout(false);
            this.tabProductDetails.ResumeLayout(false);
            this.tabProductDetails.PerformLayout();
            this.tabAddDetails.ResumeLayout(false);
            this.tabAddDetails.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabMultiUnit.ResumeLayout(false);
            this.grpUnit.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlUnit.ResumeLayout(false);
            this.pnlUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiUnit)).EndInit();
            this.tabMultiRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiRates)).EndInit();
            this.tabMultiBarcode.ResumeLayout(false);
            this.tabMultiBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiBarcode)).EndInit();
            this.taImage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProImage)).EndInit();
            this.tabSize.ResumeLayout(false);
            this.pnlMultiSize.ResumeLayout(false);
            this.pnlMultiSize.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSizeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultiBarcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).EndInit();
            this.ashGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }
    }
}
