using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class POSItemCreation : BaseForm
	{
		private DBContext db = new DBContext();

		private List<ExProductUnit> entProductUnits;

		private List<ProductUnit> entOldProductUnits;

		private List<ExProductBarcodeEntity> entProductBarcodes;

		private List<ProductBarcodeEntity> entOldProductBarcodes;

		private List<ExProductPrice> entProductPrices;

		private List<ProductPriceCategory> entoldProductPrices;

		private int? iMasterId;

		private bool isUnitInEditingMode = false;

		private bool isBarcodeInEditingMode = false;

		private string sVoucherType = "Opening Entry";

		private InputLanguage defaultLang;

		private ExProductUnit OldproductUnit;

		private List<ProductBarcodeEntity> entSizes;

		private List<ProductBarcodeEntity> entOldSizes;

		private ProductBarcodeEntity productBarcode = new ProductBarcodeEntity();

		private bool isSizeUpadate = false;

		private string sizeProName;

		private int? iSizeId;

		private IContainer components = null;

		private Label lblTitle;

		private Label lblSearch;

		public AshTextBoxNormal txtSearch;

		private TabControl tabProduct;

		private TabPage tpGeneral;

		private TabPage tpMultiunit;

		public AshTextBoxNormal txtBarcode;

		private Label label2;

		public AshTextBoxNormal txtName;

		private Label label1;

		private TabPage tpMultiBarcode;

		public AshTextBoxNormal txtLocalName;

		private Label label3;

		private CheckBox cbTranslate;

		private CheckBox cbTransliterate;

		private Button btnAddGroup;

		private AshComboBox cmbTaxCategory;

		private AshComboBox cmbGroup;

		private Label label5;

		private Label label4;

		public AshTextBoxNormal txtGrossProfit;

		public AshTextBoxNormal txtMRP;

		private Label label9;

		public AshTextBoxNormal txtGrossMargin;

		public AshTextBoxNormal txtPurchasePrice;

		public AshTextBoxNormal txtSellingPrice;

		private Label label10;

		private Label label7;

		private Label label8;

		public AshTextBoxNormal txtOppeningStock;

		private Label label6;

		private Button btnSave;

		private Button btnClear;

		private CheckBox cbActive;

		private CheckBox cbScale;

		private CheckBox cbInclusive;

		public AshTextBoxNormal txtOrderInList;

		private Label label12;

		private CheckBox cbMultiUnitInclusive;

		private Button btnAddMultiUnit;

		public AshTextBoxNormal txtMultiUnitOppeningStock;

		private Label label18;

		public AshTextBoxNormal txtMultiUnitBarcode;

		private Label label17;

		public AshTextBoxNormal txtMultiUnitprice;

		private Label label16;

		public AshTextBoxNormal txtUnitQty;

		private Label label15;

		public AshTextBoxNormal txtMultiUnitProductName;

		private Label label14;

		private AshComboBox cmbUnitMulti;

		private Label label13;

		private Panel pnlMultiUnitTwo;

		private DataGridView dgMultiUnit;

		private Panel pnlMultiunitOne;

		private Panel panel4;

		private DataGridView dgMultiBarcode;

		private Panel pnlMultiBarcode;

		public AshTextBoxNormal txtMultiBarcode;

		private Button btnAddMultiBarcode;

		private Label label19;

		private AshComboBox cmbUnit;

		private Label label20;

		private Panel pnlGeneralOne;

		private Panel pnlGeneralTwo;

		private Panel pnlFooter;

		private AshComboBox cmbUnitMultiBarcode;

		private Label label21;

		private BindingSource bsMultUnit;

		private BindingSource bsMultiBarcode;

		private TabPage tpMultiRate;

		private Panel pnlMultiRates;

		private DataGridView dgMultiRates;

		private BindingSource bsPCategory;

		private AshComboBox cmbDepot;

		private AshComboBox cmbSupplier;

		private Label label24;

		private Label label22;

		private AshComboBox cmbBrand;

		private Label label11;

		private AshComboBox cmbProType;

		private Label label48;

		private CheckBox cbMultiple;

		public AshTextBoxNormal txtLoyality;

		private Label label23;

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

		private Label label25;

		public AshTextBoxNormal txtLocalMultiUnit;

		private CheckBox cbMultiUnitTranslate;

		private CheckBox cbMultiUnitTransliterate;

		private Button btnClose;

		private DataGridViewTextBoxColumn col_Name;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Price;

		private DataGridViewImageColumn col_Close1;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_MultiBarcode;

		private DataGridViewTextBoxColumn col_BarcodeType;

		private DataGridViewImageColumn col_Close;

		private ToolTip toolTip1;

		private CheckBox cbRateBasedWeight;

		private CheckBox cbAutoGenerated;

		private Button btnBarcode;

		private Button btnDelete;

		private TabPage tabSize;

		private DataGridView dgSize;

		private Button btnSizeRemoveImage;

		private Panel panel6;

		private PictureBox pbSizeImage;

		private Button btnSizeAddImg;

		private CheckBox cbSizeTraslate;

		private CheckBox cbSizeTrasliterate;

		private Label label26;

		public AshTextBoxNormal txtSizeLocalName;

		private Button btnAddSize;

		private Label lblMultiSize;

		public AshTextBoxNormal txtSizeName;

		public AshTextBoxNormal txtSizeBarcode;

		private Label label28;

		private Label label30;

		public AshTextBoxNormal txtSizePrice;

		private BindingSource bdsSize;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

		private DataGridViewImageColumn col_SizeClose;

		private Panel pnlMultiSize;

		private TabPage tabAdditional;

		private Panel pnlAdditional;

		private AshComboBox cmbPriority;

		private AshComboBox cmbRestaurantType;

		private Label label32;

		private Label label31;

		private CheckBox cbPercentageBased;

		private CheckBox cbFavaurite;

		private Label label33;

		private Label label29;

		private AshTextBoxNormal txtExiCommission;

		private AshTextBoxNormal txtCookingTime;

		private CheckBox cbSerialNumber;

		private Label label34;

		private AshTextBoxNormal txtItemCalory;

		private DataGridViewTextBoxColumn col_SLNo;

		private DataGridViewTextBoxColumn col_PriceCategoryName;

		private DataGridViewTextBoxColumn col_UnitName;

		private DataGridViewTextBoxColumn col_SaleRate;

		private DataGridViewTextBoxColumn col_SaleDiscount;

		private DataGridViewTextBoxColumn col_PurchaseRate;

		private DataGridViewTextBoxColumn col_PurchaseDiscount;

		private DataGridViewTextBoxColumn col_MultiPriceBarcode;

		private DataGridViewImageColumn col_MultiRateDelete;

		public ProductEntity entProduct { get; set; }

		public List<ProductEntity> entProducts { get; set; } = new List<ProductEntity>();


		public POSItemCreation()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			AniHelper.IconChangeMethod(this);
			btnEnd.Visible = false;
			txtUnitQty.Format = (txtSellingPrice.Format = (txtPurchasePrice.Format = (txtMRP.Format = (txtGrossProfit.Format = (txtGrossMargin.Format = (txtMultiUnitprice.Format = (txtOppeningStock.Format = (txtMultiUnitOppeningStock.Format = CommonSettings.strCurencyFormat))))))));
			col_Price.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Qty.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			txtBarcode.Enter += TxtBarcode_Enter;
			txtMultiBarcode.Enter += TxtBarcode_Enter;
			txtMultiUnitBarcode.Enter += TxtBarcode_Enter;
			txtBarcode.LostFocus += delegate
			{
				InputLanguage.CurrentInputLanguage = defaultLang;
			};
			txtMultiBarcode.LostFocus += delegate
			{
				InputLanguage.CurrentInputLanguage = defaultLang;
			};
			txtMultiUnitBarcode.LostFocus += delegate
			{
				InputLanguage.CurrentInputLanguage = defaultLang;
			};
			defaultLang = InputLanguage.FromCulture(Thread.CurrentThread.CurrentCulture);
		}

		private void TxtBarcode_Enter(object sender, EventArgs e)
		{
			if (InputLanguage.InstalledInputLanguages.Count != 1)
			{
				CultureInfo culture = new CultureInfo("en-US");
				InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
			}
		}

		private void DeleteTransactunsMethods()
		{
			string[] obj = new string[5] { "delete from StockTranLog where ProductId='", null, null, null, null };
			int? num = iMasterId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType='";
			obj[3] = sVoucherType;
			obj[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj));
			string[] obj2 = new string[5] { "Delete from GdwnStckTranLog where VchrId='", null, null, null, null };
			num = iMasterId;
			obj2[1] = num.ToString();
			obj2[2] = "' and VchrType='";
			obj2[3] = sVoucherType;
			obj2[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj2));
			num = iMasterId;
			AshproCommon.DeleteMethod("delete from ProductUnit where ProductId='" + num + "'");
			num = iMasterId;
			AshproCommon.DeleteMethod("delete from ProductPriceCategory where ProductId='" + num + "'");
			string[] obj3 = new string[5] { "delete from ProductBarcode where FK_ProductId='", null, null, null, null };
			num = iMasterId;
			obj3[1] = num.ToString();
			obj3[2] = "' and FK_BranchId='";
			obj3[3] = Settings.Default.iBranchId.ToString();
			obj3[4] = "'";
			AshproCommon.DeleteMethod(string.Concat(obj3));
		}

		private void LoadPriceCategory(string sBarcode = null, decimal? sPrice = null, bool isMultiSize = false)
		{
			try
			{
				if (!isMultiSize && !iMasterId.HasValue)
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
							SPrice = (sPrice ?? (txtSellingPrice.Value * percentage)),
							PPrice = txtPurchasePrice.Value,
							ProName = txtName.Text,
							UnitId = cmbUnit.SelectedValue.ToInt32(),
							UnitName = cmbUnit.Text,
							PGroupId = cmbGroup.SelectedValue.ToInt32(),
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
						exProductPrice.SPrice = sPrice ?? (txtSellingPrice.Value * percentage);
						exProductPrice.PPrice = txtPurchasePrice.Value;
						exProductPrice.ProName = txtName.Text;
						exProductPrice.UnitId = cmbUnit.SelectedValue.ToInt32();
						exProductPrice.UnitName = cmbUnit.Text;
						exProductPrice.PGroupId = cmbGroup.SelectedValue.ToInt32();
						exProductPrice.Barcode = sBarcode;
					}
				}
				dgLoadMethodMultiRates();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private List<BarcodeData> GetBarcodeData()
		{
			try
			{
				List<BarcodeData> list = new List<BarcodeData>();
				BarcodeData barcodeData = new BarcodeData();
				Company company = new Company();
				company = db.Companies().FirstOrDefault();
				barcodeData.Sl_No = 1;
				barcodeData.Product_Code = AshproCommon.IdFindMethod("Select Max(ProductCode) as PCode from Product", isVoucherNo: true).ToString();
				barcodeData.Product_Name = txtName.Text;
				barcodeData.Unit = cmbUnit.Text;
				barcodeData.Brand = cmbBrand.Text;
				barcodeData.Group_Name = cmbGroup.Text;
				barcodeData.Specification = string.Empty;
				barcodeData.Description = string.Empty;
				barcodeData.Purchase_Rate = txtPurchasePrice.Value;
				barcodeData.Sales_Rate = txtSellingPrice.Value;
				barcodeData.MRP = txtMRP.Value;
				barcodeData.Barcode = txtBarcode.Text;
				barcodeData.Quantity = Math.Round(txtOppeningStock.Value).ToInt32();
				barcodeData.Company_Name = company.InstituteName;
				barcodeData.Address = company.Address;
				barcodeData.Place = company.Place;
				barcodeData.Country = company.Country;
				barcodeData.Pincode = company.Pincod;
				barcodeData.PhoneNumber = company.PhoneNo;
				barcodeData.Mobile = company.MobNo;
				barcodeData.Website = company.website;
				barcodeData.EmailId = company.EmailId;
				barcodeData.ExpairyDate = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity y) => y.Barcode == barcodeData.Barcode).ExpairyDate.ToString();
				barcodeData.ManufacturingDate = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity y) => y.Barcode == barcodeData.Barcode).ManufDate.ToString();
				barcodeData.PriceCode = AniHelper.GetPriceCode(txtPurchasePrice.Value.ToString(CommonSettings.strCurencyFormat));
				barcodeData.Rate_In_Arabic = barcodeData.Sales_Rate.ToString(CommonSettings.strCurencyFormat).ToArabic();
				barcodeData.ItemCalory = txtItemCalory.Data;
				list.Add(barcodeData);
				return list;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
				return null;
			}
		}

		private void dgLoadMethodUnit()
		{
			try
			{
				dgMultiUnit.AutoGenerateColumns = false;
				bsMultUnit.DataSource = null;
				dgMultiUnit.DataSource = null;
				bsMultUnit.DataSource = entProductUnits;
				dgMultiUnit.DataSource = bsMultUnit;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgLoadMethodBarcode()
		{
			try
			{
				dgMultiBarcode.AutoGenerateColumns = false;
				bsMultiBarcode.DataSource = null;
				dgMultiBarcode.DataSource = null;
				bsMultiBarcode.DataSource = entProductBarcodes;
				dgMultiBarcode.DataSource = bsMultiBarcode;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgLoadMethodMultiRates()
		{
			try
			{
				dgMultiRates.AutoGenerateColumns = false;
				bsPCategory.DataSource = null;
				dgMultiRates.DataSource = null;
				bsPCategory.DataSource = entProductPrices;
				dgMultiRates.DataSource = bsPCategory;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgLoadMethodMultiSize()
		{
			try
			{
				dgSize.AutoGenerateColumns = false;
				bdsSize.DataSource = null;
				dgSize.DataSource = null;
				bdsSize.DataSource = entSizes;
				dgSize.DataSource = bdsSize;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ClearSize()
		{
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AddbarcodeToList(string sBarcode, string sType, int iUnitId, decimal dSalePrice, decimal dPurPrice, string sName, string lName)
		{
			try
			{
				if (sBarcode == string.Empty)
				{
					return;
				}
				if (!entProductBarcodes.Any((ExProductBarcodeEntity x) => x.Barcode == sBarcode && x.BarcodeType == "Base Barcode"))
				{
					ExProductBarcodeEntity exProductBarcodeEntity = new ExProductBarcodeEntity();
					exProductBarcodeEntity.SL = entProductBarcodes.Count + 1;
					exProductBarcodeEntity.Barcode = sBarcode;
					exProductBarcodeEntity.FK_ProductId = iMasterId;
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
					exProductBarcodeEntity2.FK_ProductId = iMasterId;
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void EntityLoadMethod(int iProductCode)
		{
			try
			{
				entProduct.ProductId = iMasterId;
				entProduct.ProductCode = iProductCode;
				entProduct.ProductName = txtName.Text;
				if (cmbRestaurantType.SelectedIndex == 1 && !entProduct.ProductName.Contains("#"))
				{
					entProduct.ProductName = "#" + txtName.Text;
				}
				entProduct.ProductName = txtName.Text;
				entProduct.LocalName = txtLocalName.Text;
				entProduct.PurchasePrice = txtPurchasePrice.Value;
				entProduct.SellingPrice = txtSellingPrice.Value;
				entProduct.MRP = txtMRP.Value;
				entProduct.OpeningStock = txtOppeningStock.Value;
				entProduct.OppStockDate = DateTime.Now.Date;
				entProduct.isInclusive = cbInclusive.Checked;
				entProduct.isActive = cbActive.Checked;
				entProduct.IsScale = cbScale.Checked;
				entProduct.ProductType = cmbProType.Text;
				entProduct.ListOrder = ((txtOrderInList.Data == 0) ? iProductCode : txtOrderInList.Data);
				entProduct.UnitId = cmbUnit.SelectedValue.ToInt32();
				entProduct.PGroupId = cmbGroup.SelectedValue.ToInt32();
				entProduct.TCategoryId = cmbTaxCategory.SelectedValue.ToInt32();
				entProduct.Barcode = txtBarcode.Text;
				entProduct.AccountId = cmbSupplier.SelectedValue.ToInt32();
				entProduct.BrandId = cmbBrand.SelectedValue.ToInt32();
				entProduct.GodwnId = cmbDepot.SelectedValue.ToInt32();
				entProduct.description = " ";
				entProduct.IsAllowMultiple = cbMultiple.Checked;
				entProduct.LoyalityPont = txtLoyality.Data;
				entProduct.isRateBasedWeight = cbRateBasedWeight.Checked;
				entProduct.CookingTime = txtCookingTime.Data;
				entProduct.PriorityStatus = cmbPriority.Text;
				entProduct.RestaurantItemType = cmbRestaurantType.Text;
				entProduct.IsFavorite = cbFavaurite.Checked;
				entProduct.IsCommisionBasedPercentage = cbPercentageBased.Checked;
				entProduct.SalesmanCommision = txtExiCommission.Value;
				entProduct.FieldOne = cbSerialNumber.Checked.ToString();
				entProduct.FieldTwo = txtItemCalory.Value.ToString();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnAddGroup_Click(object sender, EventArgs e)
		{
			try
			{
				ProductGroup productGroup = new ProductGroup();
				productGroup.isOpenFromAnotherForm = true;
				if (productGroup.ShowDialog() == DialogResult.OK)
				{
					AniHelper.FillProGroup(cmbGroup);
					cmbGroup.Text = productGroup.ProductGroupName;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtName_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text != string.Empty)
				{
					if (!iMasterId.HasValue && AshproCommon.GetListMethod<ProductEntity>("Product", new
					{
						ProductName = txtName.Text
					}).ToList().Count > 0)
					{
						Messages.ErrorMessage("Product already exist.");
						txtName.Text = string.Empty;
						txtName.Focus();
					}
					else
					{
						txtLocalName.Text = AniHelper.GetLocalName(cbTransliterate.Checked, cbTranslate.Checked, txtName.Text, txtLocalName.Text);
						LoadPriceCategory();
						AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbUnit.SelectedValue.ToInt32(), txtSellingPrice.Value, txtPurchasePrice.Value, txtName.Text, txtLocalName.Text);
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void POSItemCreation_Load(object sender, EventArgs e)
		{
			try
			{
				await AniHelper.AutoCompleteMethod(txtSearch);
				AniHelper.FillProGroup(cmbGroup);
				AniHelper.FillUnit(cmbUnit);
				AniHelper.FillUnit(cmbUnitMulti);
				AniHelper.FillTCategory(cmbTaxCategory);
				AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
				AniHelper.FillBrand(cmbBrand);
				AniHelper.FillGodown(cmbDepot);
				AniHelper.FillUnit(cmbUnitMultiBarcode);
				cbAutoGenerated.Checked = Settings.Default.UnitBarcodeAuto;
				entProductBarcodes = new List<ExProductBarcodeEntity>();
				entProductUnits = new List<ExProductUnit>();
				entProductPrices = new List<ExProductPrice>();
				entSizes = new List<ProductBarcodeEntity>();
				dgLoadMethodUnit();
				dgLoadMethodBarcode();
				if (entProduct != null)
				{
					txtSearch.Text = entProduct.Barcode;
					txtSearch_Validated(null, null);
				}
				cbInclusive.Checked = CommonSettings.blnTaxInclusive;
				base.ActiveControl = txtSearch;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSellingPrice_Validated(object sender, EventArgs e)
		{
			try
			{
				AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbUnit.SelectedValue.ToInt32(), txtSellingPrice.Value, txtPurchasePrice.Value, txtName.Text, txtLocalName.Text);
				LoadPriceCategory();
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
				tabProduct.SelectedIndex = 0;
				isUnitInEditingMode = false;
				isBarcodeInEditingMode = false;
				AniHelper.PanelClearmethod(pnlGeneralOne);
				AniHelper.PanelClearmethod(pnlGeneralTwo);
				AniHelper.PanelClearmethod(pnlMultiunitOne);
				AniHelper.PanelClearmethod(pnlMultiBarcode);
				AniHelper.PanelClearmethod(pnlMultiSize);
				pbSizeImage.Image = null;
				entProductBarcodes = new List<ExProductBarcodeEntity>();
				entProductUnits = new List<ExProductUnit>();
				entProductPrices = new List<ExProductPrice>();
				entSizes = new List<ProductBarcodeEntity>();
				dgLoadMethodUnit();
				dgLoadMethodBarcode();
				dgLoadMethodMultiRates();
				dgLoadMethodMultiSize();
				iMasterId = null;
				cbInclusive.Checked = CommonSettings.blnTaxInclusive;
				txtBarcode.Focus();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnAddMultiUnit_Click(object sender, EventArgs e)
		{
			try
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
						exProductUnit.ProductId = iMasterId;
						exProductUnit.ProductName = txtName.Text;
						exProductUnit.UnitName = cmbUnitMulti.Text;
						entProductUnits.Add(exProductUnit);
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
						exProductUnit.ProductId = iMasterId;
						exProductUnit.ProductName = txtName.Text;
						exProductUnit.UnitName = cmbUnitMulti.Text;
						dgMultiUnit.Refresh();
					}
					dgLoadMethodUnit();
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
								PGroupId = cmbGroup.SelectedValue.ToInt32(),
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
							exProductPrice.PGroupId = cmbGroup.SelectedValue.ToInt32();
							exProductPrice.Barcode = txtMultiUnitBarcode.Text;
						}
					}
					dgLoadMethodMultiRates();
					AddbarcodeToList(txtMultiUnitBarcode.Text, "Unit Barcode", cmbUnitMulti.SelectedValue.ToInt32(), txtMultiUnitprice.Value, txtPurchasePrice.Value * txtUnitQty.Value, txtMultiUnitProductName.Text, txtLocalMultiUnit.Text);
					isUnitInEditingMode = false;
					AniHelper.PanelClearmethod(pnlMultiunitOne);
					cmbUnitMulti.Focus();
				}
				else
				{
					Messages.WarningMessage();
					cmbUnitMulti.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnAddMultiBarcode_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtMultiBarcode.Text != string.Empty)
				{
					if (!isBarcodeInEditingMode)
					{
						AddbarcodeToList(txtMultiBarcode.Text, "Multi Barcode", cmbUnitMultiBarcode.SelectedValue.ToInt32(), txtSellingPrice.Value, txtPurchasePrice.Value, txtName.Text, txtLocalName.Text);
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSearch_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtSearch.Text == string.Empty)
				{
					return;
				}
				entProduct = new ProductEntity();
				tabProduct.SelectedIndex = 0;
				entProduct = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
				{
					Barcode = txtSearch.Text
				});
				if (entProduct.ProductName == null)
				{
					entProduct = db.Products(new
					{
						ProductName = txtSearch.Text
					}).FirstOrDefault();
				}
				if (entProduct != null)
				{
					if (entProduct.ProductId.HasValue)
					{
						iMasterId = entProduct.ProductId;
						txtName.Text = entProduct.ProductName;
						txtLocalName.Text = entProduct.LocalName;
						cmbUnit.SelectedValue = entProduct.UnitId;
						cmbGroup.SelectedValue = entProduct.PGroupId;
						cmbTaxCategory.SelectedValue = entProduct.TCategoryId;
						txtPurchasePrice.Value = entProduct.PurchasePrice;
						txtSellingPrice.Value = entProduct.SellingPrice;
						txtMRP.Value = entProduct.MRP;
						txtOppeningStock.Value = entProduct.OpeningStock;
						cbActive.Checked = entProduct.isActive;
						cbInclusive.Checked = entProduct.isInclusive.toBool();
						cbScale.Checked = entProduct.IsScale.GetValueOrDefault();
						cbRateBasedWeight.Checked = entProduct.isRateBasedWeight.GetValueOrDefault();
						txtBarcode.Text = entProduct.Barcode;
						cmbProType.Text = entProduct.ProductType;
						if (entProduct.AccountId.HasValue)
						{
							cmbSupplier.SelectedValue = entProduct.AccountId;
						}
						cmbBrand.SelectedValue = entProduct.BrandId;
						cmbDepot.SelectedValue = entProduct.GodwnId.toInt32();
						txtOrderInList.Data = entProduct.ListOrder;
						cbMultiple.Checked = entProduct.IsAllowMultiple.GetValueOrDefault();
						txtLoyality.Data = entProduct.LoyalityPont;
						cbFavaurite.Checked = entProduct.IsFavorite.toBool();
						cmbRestaurantType.Text = entProduct.RestaurantItemType;
						cmbPriority.Text = entProduct.PriorityStatus;
						txtCookingTime.Data = entProduct.CookingTime.toInt32();
						cbPercentageBased.Checked = entProduct.IsCommisionBasedPercentage.GetValueOrDefault();
						txtExiCommission.Value = entProduct.SalesmanCommision.ToDecimal();
						cbSerialNumber.Checked = entProduct.FieldOne.ToBool();
						txtItemCalory.Data = entProduct.FieldTwo.ToInt32();
						List<ProductBarcodeEntity> list = new List<ProductBarcodeEntity>();
						entOldProductUnits = db.Productunits(new { entProduct.ProductId }).ToList();
						entProductUnits = AniHelper.CopyListData<ExProductUnit>(db.Productunits(new { entProduct.ProductId }).Cast<object>().ToList());
						OldproductUnit = entProductUnits.FirstOrDefault((ExProductUnit x) => x.UnitType == "Base Unit");
						entProductUnits.Remove(OldproductUnit);
						entOldProductBarcodes = (from x in db.ProductBarcodes(new
							{
								FK_ProductId = entProduct.ProductId,
								FK_BranchId = Settings.Default.iBranchId
							})
							where x.BarcodeType != "Multi Size"
							select x).ToList();
						list = (from x in db.ProductBarcodes(new
							{
								FK_ProductId = entProduct.ProductId,
								FK_BranchId = Settings.Default.iBranchId
							})
							where x.BarcodeType != "Multi Size"
							select x).ToList();
						entOldSizes = (from x in db.ProductBarcodes(new
							{
								FK_ProductId = entProduct.ProductId,
								BarcodeType = "Multi Size"
							})
							where x.ProductBarcodeName != string.Empty
							select x).ToList();
						entSizes = (from x in db.ProductBarcodes(new
							{
								FK_ProductId = entProduct.ProductId,
								BarcodeType = "Multi Size"
							})
							where x.ProductBarcodeName != string.Empty
							select x).ToList();
						entoldProductPrices = db.ProductPriceCategories(new { entProduct.ProductId }).ToList();
						entProductPrices = AniHelper.CopyListData<ExProductPrice>(db.ProductPriceCategories(new { entProduct.ProductId }).Cast<object>().ToList());
						foreach (ExProductUnit item in entProductUnits)
						{
							item.ProductName = entProduct.ProductName;
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
							if (productBarcodeEntity != null && productBarcodeEntity.ProductBarcodeName != string.Empty)
							{
								x2.ProName = productBarcodeEntity.ProductBarcodeName;
							}
							else
							{
								x2.ProName = entProduct.ProductName;
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
					else
					{
						Messages.ErrorMessage("Product Not Found");
					}
					txtSearch.Text = string.Empty;
					txtBarcode.Focus();
					txtBarcode.SelectAll();
				}
				else
				{
					Messages.ErrorMessage("Product Not Found");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
					cmbUnitMulti_SelectedIndexChanged(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgMultiBarcode_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(txtName.Text == string.Empty) && !(txtBarcode.Text == string.Empty))
				{
					if (!iMasterId.HasValue)
					{
						int iProductCode = (db.Products().Max((ProductEntity x) => x.ProductCode) + 1) ?? 1;
						entProduct = new ProductEntity();
						EntityLoadMethod(iProductCode);
						if (AshproCommon.InsertToDatabaseObj(entProduct, "Product"))
						{
							entProduct = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProduct", new
							{
								Barcode = txtBarcode.Text,
								ProductName = txtName.Text
							});
							List<ProductUnit> list = AniHelper.CopyListData<ProductUnit>(entProductUnits.Cast<object>().ToList());
							if (!list.Any((ProductUnit x) => x.UnitId == cmbUnit.SelectedValue.ToInt32()))
							{
								ProductUnit item = new ProductUnit
								{
									UnitId = cmbUnit.SelectedValue.ToInt32(),
									Barcode = txtBarcode.Text,
									IsInclusive = cbInclusive.Checked,
									OpeningStock = txtOppeningStock.Value,
									ProductUnitName = txtName.Text,
									SalesPrice = txtSellingPrice.Value,
									UnitQty = 1m,
									UnitType = "Base Unit"
								};
								list.Add(item);
							}
							decimal num = default(decimal);
							foreach (ProductUnit item4 in list)
							{
								item4.ProductId = entProduct.ProductId;
								AshproCommon.InsertToDatabaseObj(item4);
								num += item4.OpeningStock * item4.UnitQty;
							}
							if (num > 0m && entProduct.ProductType != "Service")
							{
								POSHelper.STranLogMethod(entProduct.ProductId.ToInt32(), entProduct.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, num, 0m, entProduct.ProductId.ToInt32(), entProduct.UnitId.ToInt32(), 1, null, entProduct.Barcode);
							}
							foreach (ExProductPrice entProductPrice in entProductPrices)
							{
								entProductPrice.ProductId = entProduct.ProductId;
							}
							List<ProductPriceCategory> list2 = new List<ProductPriceCategory>();
							list2 = AniHelper.CopyListData<ProductPriceCategory>(entProductPrices.Cast<object>().ToList());
							AshproCommon.InsertToDatabase(list2.Cast<object>().ToList());
							List<ProductBarcodeEntity> list3 = new List<ProductBarcodeEntity>();
							list3 = AniHelper.CopyListData<ProductBarcodeEntity>(entProductBarcodes.Cast<object>().ToList());
							foreach (ProductBarcodeEntity item5 in list3)
							{
								item5.FK_ProductId = entProduct.ProductId;
							}
							AshproCommon.InsertToDatabase(list3.Cast<object>().ToList(), "ProductBarcode");
							foreach (ProductBarcodeEntity entSize in entSizes)
							{
								entSize.FK_ProductId = entProduct.ProductId;
							}
							AshproCommon.InsertToDatabase(entSizes.Cast<object>().ToList(), "ProductBarcode");
						}
					}
					else
					{
						EntityLoadMethod(db.Products(new
						{
							ProductId = iMasterId
						}).FirstOrDefault().ProductCode.ToInt32());
						if (AshproCommon.UpdateToDatabaseObj(entProduct, "Product", "ProductId", iMasterId.ToInt32()))
						{
							List<ProductUnit> list4 = AniHelper.CopyListData<ProductUnit>(entProductUnits.Cast<object>().ToList());
							if (!list4.Any((ProductUnit x) => x.UnitId == cmbUnit.SelectedValue.ToInt32()))
							{
								ProductUnit item2 = new ProductUnit
								{
									UnitId = cmbUnit.SelectedValue.ToInt32(),
									Barcode = txtBarcode.Text,
									IsInclusive = cbInclusive.Checked,
									OpeningStock = txtOppeningStock.Value,
									ProductUnitName = txtName.Text + " " + cmbUnit.Text,
									SalesPrice = txtSellingPrice.Value,
									UnitQty = 1m,
									UnitType = "Base Unit"
								};
								list4.Add(item2);
							}
							decimal num2 = default(decimal);
							foreach (ProductUnit item6 in list4)
							{
								item6.ProductId = entProduct.ProductId;
								num2 += item6.OpeningStock * item6.UnitQty;
							}
							AshproCommon.UpdateDatabase(list4.Cast<object>().ToList(), entOldProductUnits.Cast<object>().ToList(), "ProductUnit", "ProUnitId");
							string[] obj = new string[5] { "delete from StockTranLog where ProductId='", null, null, null, null };
							int? num3 = iMasterId;
							obj[1] = num3.ToString();
							obj[2] = "' and VchrType='";
							obj[3] = sVoucherType;
							obj[4] = "'";
							AshproCommon.DeleteMethod(string.Concat(obj));
							string[] obj2 = new string[5] { "Delete from GdwnStckTranLog where VchrId='", null, null, null, null };
							num3 = iMasterId;
							obj2[1] = num3.ToString();
							obj2[2] = "' and VchrType='";
							obj2[3] = sVoucherType;
							obj2[4] = "'";
							AshproCommon.DeleteMethod(string.Concat(obj2));
							if (num2 > 0m && entProduct.ProductType != "Service")
							{
								POSHelper.STranLogMethod(entProduct.ProductId.ToInt32(), entProduct.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, num2, 0m, entProduct.ProductId.ToInt32(), entProduct.UnitId.ToInt32(), 1, null, entProduct.Barcode);
							}
							foreach (ExProductPrice entProductPrice2 in entProductPrices)
							{
								entProductPrice2.ProductId = entProduct.ProductId;
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
									FK_ProductId = entProduct.ProductId.toInt32(),
									FK_UnitId = entProduct.UnitId.toInt32(),
									FK_BatchId = null,
									Barcode = txtBarcode.Text,
									BarcodeType = "Base Barcode",
									PurchasePrice = txtPurchasePrice.Value,
									SellingPrice = txtSellingPrice.Value,
									MRP = txtMRP.Value,
									MinimuPrice = txtSellingPrice.Value,
									RetailPrice = txtSellingPrice.Value,
									WholesalePrice = txtSellingPrice.Value,
									ProductCost = txtPurchasePrice.Value
								};
								list6.Add(item3);
							}
							foreach (ProductBarcodeEntity item7 in list6)
							{
								item7.FK_ProductId = entProduct.ProductId;
							}
							AshproCommon.UpdateDatabase(list6.Cast<object>().ToList(), entOldProductBarcodes.Cast<object>().ToList(), "ProductBarcode", "Id");
							foreach (ProductBarcodeEntity entSize2 in entSizes)
							{
								entSize2.FK_ProductId = entProduct.ProductId;
							}
							AshproCommon.UpdateDatabase(entSizes.Cast<object>().ToList(), entOldSizes.Cast<object>().ToList(), "ProductBarcode", "Id");
						}
					}
					btnClear.PerformClick();
				}
				else
				{
					Messages.WarningMessage();
					txtBarcode.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void cmbUnitMulti_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtMultiUnitOppeningStock_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAddMultiUnit.PerformClick();
			}
		}

		private void dgMultiUnit_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgMultiBarcode_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgMultiBarcode_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					ExProductBarcodeEntity exProductBarcodeEntity = (ExProductBarcodeEntity)dgMultiBarcode.CurrentRow.DataBoundItem;
					txtMultiBarcode.Text = exProductBarcodeEntity.Barcode;
					cmbUnitMultiBarcode.SelectedValue = exProductBarcodeEntity.FK_UnitId;
					isBarcodeInEditingMode = true;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtUnitQty_Validated(object sender, EventArgs e)
		{
			try
			{
				txtMultiUnitprice.Value = txtSellingPrice.Value * txtUnitQty.Value;
				if (cbAutoGenerated.Checked)
				{
					txtMultiUnitBarcode.Text = txtBarcode.Text + txtUnitQty.Value;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtMultiBarcode_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					btnAddMultiBarcode.PerformClick();
					txtMultiBarcode.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void tabProduct_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				switch (tabProduct.SelectedIndex)
				{
				case 0:
					txtBarcode.Focus();
					break;
				case 1:
					cmbUnitMulti.Focus();
					break;
				case 2:
					txtMultiBarcode.Focus();
					break;
				case 4:
					txtSizeName.Focus();
					break;
				case 3:
					break;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtBarcode_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtBarcode.Text != string.Empty)
				{
					if (!iMasterId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
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
						AddbarcodeToList(txtBarcode.Text, "Base Barcode", cmbUnit.SelectedValue.ToInt32(), txtSellingPrice.Value, txtPurchasePrice.Value, txtName.Text, txtLocalName.Text);
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtMultiUnitBarcode_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtMultiUnitBarcode.Text != string.Empty && !iMasterId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
				{
					Barcode = txtMultiUnitBarcode.Text
				}) > 0)
				{
					Messages.ErrorMessage("Barcode already exist.");
					txtMultiUnitBarcode.Focus();
					txtMultiUnitBarcode.SelectAll();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtPurchasePrice_Validated(object sender, EventArgs e)
		{
			try
			{
				LoadPriceCategory();
				if (txtPurchasePrice.Value > 0m && txtSellingPrice.Value > 0m)
				{
					txtGrossMargin.Value = txtSellingPrice.Value - txtPurchasePrice.Value;
					txtGrossProfit.Value = txtGrossMargin.Value / txtPurchasePrice.Value * 100m;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void POSItemCreation_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control)
				{
					cbTransliterate.Checked = ((!cbTransliterate.Checked) ? true : false);
				}
				else if (e.KeyCode == Keys.F1)
				{
					btnSave.PerformClick();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtMultiBarcode_Validated(object sender, EventArgs e)
		{
			try
			{
				if (txtMultiBarcode.Text != string.Empty && !iMasterId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
				{
					Barcode = txtMultiBarcode.Text
				}) > 0)
				{
					Messages.ErrorMessage("Barcode already exist.");
					txtMultiBarcode.Focus();
					txtMultiBarcode.SelectAll();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cbAutoGenerated_Click(object sender, EventArgs e)
		{
			Settings.Default.UnitBarcodeAuto = cbAutoGenerated.Checked;
			Settings.Default.Save();
		}

		private void btnBarcode_Click(object sender, EventArgs e)
		{
			try
			{
				int num = Math.Round(txtOppeningStock.Value).ToInt32();
				if (!(txtName.Text == string.Empty) && num != 0)
				{
					ProductBarcode productBarcode = new ProductBarcode();
					productBarcode.printBarcode(GetBarcodeData(), 1, db.BarcodeHDR().FirstOrDefault().Id);
				}
				else
				{
					Messages.ErrorMessage("No Record to Print!");
				}
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
				DeleteTransactunsMethods();
				AshproCommon.DeleteFromDatabase("Product", "ProductId", iMasterId.ToInt32());
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtMultiUnitProductName_Validated(object sender, EventArgs e)
		{
			try
			{
				txtLocalMultiUnit.Text = AniHelper.GetLocalName(cbMultiUnitTransliterate.Checked, cbMultiUnitTranslate.Checked, txtMultiUnitProductName.Text, txtLocalMultiUnit.Text);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSizeAddImg_Click(object sender, EventArgs e)
		{
			try
			{
				AniHelper.ImageLoadMethod(pbSizeImage);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSizeRemoveImage_Click(object sender, EventArgs e)
		{
			pbSizeImage.Image = null;
		}

		private void btnAddSize_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(txtSizeName.Text == string.Empty) && !(txtSizeBarcode.Text == string.Empty))
				{
					productBarcode.FK_ProductId = iMasterId;
					productBarcode.ProductBarcodeName = sizeProName;
					productBarcode.LocalName = txtSizeLocalName.Text;
					productBarcode.Barcode = txtSizeBarcode.Text;
					productBarcode.SellingPrice = txtSizePrice.Value;
					productBarcode.image = ((pbSizeImage.Image == null) ? null : AniHelper.imageToByteArray(pbSizeImage.Image));
					productBarcode.FK_BranchId = Settings.Default.iBranchId;
					productBarcode.FK_UnitId = cmbUnit.SelectedValue.ToInt32();
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
					LoadPriceCategory(txtSizeBarcode.Text, txtSizePrice.Value, isMultiSize: true);
					ClearSize();
				}
				else
				{
					Messages.WarningMessage();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSizeName_Validated(object sender, EventArgs e)
		{
			try
			{
				sizeProName = txtName.Text + "_" + txtSizeName.Text;
				txtSizeLocalName.Text = AniHelper.GetLocalName(cbSizeTrasliterate.Checked, cbSizeTraslate.Checked, sizeProName, txtSizeLocalName.Text);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSizePrice_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					btnAddSize.PerformClick();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgSize_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgMultiRates_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex == dgMultiRates.Columns[col_MultiRateDelete.Name].Index)
				{
					ExProductPrice exProductPrice = (ExProductPrice)dgMultiRates.CurrentRow.DataBoundItem;
					MultirateDeleteMethod(exProductPrice);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void MultirateDeleteMethod(ExProductPrice exProductPrice)
		{
			try
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
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSItemCreation));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.tabProduct = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.pnlGeneralOne = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMRP = new AshUsercontrol.AshTextBoxNormal();
            this.txtBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.cmbTaxCategory = new AshUsercontrol.AshComboBox();
            this.txtPurchasePrice = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbUnit = new AshUsercontrol.AshComboBox();
            this.txtOppeningStock = new AshUsercontrol.AshTextBoxNormal();
            this.label20 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.txtSellingPrice = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlGeneralTwo = new System.Windows.Forms.Panel();
            this.cmbProType = new AshUsercontrol.AshComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtGrossProfit = new AshUsercontrol.AshTextBoxNormal();
            this.cbTranslate = new System.Windows.Forms.CheckBox();
            this.cbTransliterate = new System.Windows.Forms.CheckBox();
            this.txtLocalName = new AshUsercontrol.AshTextBoxNormal();
            this.cmbDepot = new AshUsercontrol.AshComboBox();
            this.cmbSupplier = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGrossMargin = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.txtLoyality = new AshUsercontrol.AshTextBoxNormal();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOrderInList = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.cbInclusive = new System.Windows.Forms.CheckBox();
            this.cbScale = new System.Windows.Forms.CheckBox();
            this.cbRateBasedWeight = new System.Windows.Forms.CheckBox();
            this.cbMultiple = new System.Windows.Forms.CheckBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.tpMultiunit = new System.Windows.Forms.TabPage();
            this.pnlMultiUnitTwo = new System.Windows.Forms.Panel();
            this.dgMultiUnit = new System.Windows.Forms.DataGridView();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Close1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlMultiunitOne = new System.Windows.Forms.Panel();
            this.cbAutoGenerated = new System.Windows.Forms.CheckBox();
            this.cbMultiUnitTranslate = new System.Windows.Forms.CheckBox();
            this.cbMultiUnitTransliterate = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtLocalMultiUnit = new AshUsercontrol.AshTextBoxNormal();
            this.cmbUnitMulti = new AshUsercontrol.AshComboBox();
            this.cbMultiUnitInclusive = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnAddMultiUnit = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMultiUnitOppeningStock = new AshUsercontrol.AshTextBoxNormal();
            this.txtMultiUnitProductName = new AshUsercontrol.AshTextBoxNormal();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMultiUnitBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.txtUnitQty = new AshUsercontrol.AshTextBoxNormal();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMultiUnitprice = new AshUsercontrol.AshTextBoxNormal();
            this.tpMultiBarcode = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgMultiBarcode = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BarcodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Close = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlMultiBarcode = new System.Windows.Forms.Panel();
            this.cmbUnitMultiBarcode = new AshUsercontrol.AshComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMultiBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.btnAddMultiBarcode = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.tpMultiRate = new System.Windows.Forms.TabPage();
            this.pnlMultiRates = new System.Windows.Forms.Panel();
            this.dgMultiRates = new System.Windows.Forms.DataGridView();
            this.col_SLNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PriceCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SaleDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PurchaseRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PurchaseDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiPriceBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MultiRateDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabSize = new System.Windows.Forms.TabPage();
            this.pnlMultiSize = new System.Windows.Forms.Panel();
            this.txtSizeName = new AshUsercontrol.AshTextBoxNormal();
            this.txtSizePrice = new AshUsercontrol.AshTextBoxNormal();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cbSizeTraslate = new System.Windows.Forms.CheckBox();
            this.txtSizeBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.cbSizeTrasliterate = new System.Windows.Forms.CheckBox();
            this.lblMultiSize = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtSizeLocalName = new AshUsercontrol.AshTextBoxNormal();
            this.btnSizeRemoveImage = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pbSizeImage = new System.Windows.Forms.PictureBox();
            this.btnSizeAddImg = new System.Windows.Forms.Button();
            this.btnAddSize = new System.Windows.Forms.Button();
            this.dgSize = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SizeClose = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabAdditional = new System.Windows.Forms.TabPage();
            this.pnlAdditional = new System.Windows.Forms.Panel();
            this.cmbPriority = new AshUsercontrol.AshComboBox();
            this.cmbRestaurantType = new AshUsercontrol.AshComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.cbSerialNumber = new System.Windows.Forms.CheckBox();
            this.cbPercentageBased = new System.Windows.Forms.CheckBox();
            this.cbFavaurite = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtItemCalory = new AshUsercontrol.AshTextBoxNormal();
            this.txtExiCommission = new AshUsercontrol.AshTextBoxNormal();
            this.txtCookingTime = new AshUsercontrol.AshTextBoxNormal();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
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
            this.txtSearch = new AshUsercontrol.AshTextBoxNormal();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.bsMultUnit = new System.Windows.Forms.BindingSource(this.components);
            this.bsMultiBarcode = new System.Windows.Forms.BindingSource(this.components);
            this.bsPCategory = new System.Windows.Forms.BindingSource(this.components);
            this.bdsSize = new System.Windows.Forms.BindingSource(this.components);
            this.tabProduct.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.pnlGeneralOne.SuspendLayout();
            this.pnlGeneralTwo.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.tpMultiunit.SuspendLayout();
            this.pnlMultiUnitTwo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiUnit)).BeginInit();
            this.pnlMultiunitOne.SuspendLayout();
            this.tpMultiBarcode.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiBarcode)).BeginInit();
            this.pnlMultiBarcode.SuspendLayout();
            this.tpMultiRate.SuspendLayout();
            this.pnlMultiRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiRates)).BeginInit();
            this.tabSize.SuspendLayout();
            this.pnlMultiSize.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSizeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSize)).BeginInit();
            this.tabAdditional.SuspendLayout();
            this.pnlAdditional.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultiBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.toolTip1.SetToolTip(this.lblTitle, resources.GetString("lblTitle.ToolTip"));
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.Name = "lblSearch";
            this.toolTip1.SetToolTip(this.lblSearch, resources.GetString("lblSearch.ToolTip"));
            // 
            // tabProduct
            // 
            resources.ApplyResources(this.tabProduct, "tabProduct");
            this.tabProduct.Controls.Add(this.tpGeneral);
            this.tabProduct.Controls.Add(this.tpMultiunit);
            this.tabProduct.Controls.Add(this.tpMultiBarcode);
            this.tabProduct.Controls.Add(this.tpMultiRate);
            this.tabProduct.Controls.Add(this.tabSize);
            this.tabProduct.Controls.Add(this.tabAdditional);
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.SelectedIndex = 0;
            this.tabProduct.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabProduct.TabStop = false;
            this.toolTip1.SetToolTip(this.tabProduct, resources.GetString("tabProduct.ToolTip"));
            this.tabProduct.SelectedIndexChanged += new System.EventHandler(this.tabProduct_SelectedIndexChanged);
            // 
            // tpGeneral
            // 
            resources.ApplyResources(this.tpGeneral, "tpGeneral");
            this.tpGeneral.BackColor = System.Drawing.Color.White;
            this.tpGeneral.Controls.Add(this.pnlGeneralOne);
            this.tpGeneral.Controls.Add(this.pnlGeneralTwo);
            this.tpGeneral.Controls.Add(this.pnlFooter);
            this.tpGeneral.Name = "tpGeneral";
            this.toolTip1.SetToolTip(this.tpGeneral, resources.GetString("tpGeneral.ToolTip"));
            // 
            // pnlGeneralOne
            // 
            resources.ApplyResources(this.pnlGeneralOne, "pnlGeneralOne");
            this.pnlGeneralOne.Controls.Add(this.label9);
            this.pnlGeneralOne.Controls.Add(this.label7);
            this.pnlGeneralOne.Controls.Add(this.txtMRP);
            this.pnlGeneralOne.Controls.Add(this.txtBarcode);
            this.pnlGeneralOne.Controls.Add(this.cmbTaxCategory);
            this.pnlGeneralOne.Controls.Add(this.txtPurchasePrice);
            this.pnlGeneralOne.Controls.Add(this.txtName);
            this.pnlGeneralOne.Controls.Add(this.label5);
            this.pnlGeneralOne.Controls.Add(this.label1);
            this.pnlGeneralOne.Controls.Add(this.label6);
            this.pnlGeneralOne.Controls.Add(this.cmbUnit);
            this.pnlGeneralOne.Controls.Add(this.txtOppeningStock);
            this.pnlGeneralOne.Controls.Add(this.label20);
            this.pnlGeneralOne.Controls.Add(this.label8);
            this.pnlGeneralOne.Controls.Add(this.btnAddGroup);
            this.pnlGeneralOne.Controls.Add(this.cmbGroup);
            this.pnlGeneralOne.Controls.Add(this.txtSellingPrice);
            this.pnlGeneralOne.Controls.Add(this.label2);
            this.pnlGeneralOne.Controls.Add(this.label4);
            this.pnlGeneralOne.Name = "pnlGeneralOne";
            this.toolTip1.SetToolTip(this.pnlGeneralOne, resources.GetString("pnlGeneralOne.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txtMRP
            // 
            resources.ApplyResources(this.txtMRP, "txtMRP");
            this.txtMRP.BackColor = System.Drawing.Color.White;
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
            this.toolTip1.SetToolTip(this.txtBarcode, resources.GetString("txtBarcode.ToolTip"));
            this.txtBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBarcode.Validated += new System.EventHandler(this.txtBarcode_Validated);
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
            // 
            // txtPurchasePrice
            // 
            resources.ApplyResources(this.txtPurchasePrice, "txtPurchasePrice");
            this.txtPurchasePrice.BackColor = System.Drawing.Color.White;
            this.txtPurchasePrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurchasePrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurchasePrice.Data = 0;
            this.txtPurchasePrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurchasePrice.Format = null;
            this.txtPurchasePrice.isAllowNegative = false;
            this.txtPurchasePrice.isAllowSpecialChar = false;
            this.txtPurchasePrice.isNumberOnly = false;
            this.txtPurchasePrice.isNumeric = true;
            this.txtPurchasePrice.LeaveColor = System.Drawing.Color.White;
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.toolTip1.SetToolTip(this.txtPurchasePrice, resources.GetString("txtPurchasePrice.ToolTip"));
            this.txtPurchasePrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurchasePrice.Validated += new System.EventHandler(this.txtPurchasePrice_Validated);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BackColor = System.Drawing.Color.White;
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
            this.toolTip1.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbUnit
            // 
            resources.ApplyResources(this.cmbUnit, "cmbUnit");
            this.cmbUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnit.BackColor = System.Drawing.Color.White;
            this.cmbUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbUnit.Name = "cmbUnit";
            this.toolTip1.SetToolTip(this.cmbUnit, resources.GetString("cmbUnit.ToolTip"));
            // 
            // txtOppeningStock
            // 
            resources.ApplyResources(this.txtOppeningStock, "txtOppeningStock");
            this.txtOppeningStock.BackColor = System.Drawing.Color.White;
            this.txtOppeningStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOppeningStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOppeningStock.Data = 0;
            this.txtOppeningStock.EnterColor = System.Drawing.Color.LightPink;
            this.txtOppeningStock.Format = null;
            this.txtOppeningStock.isAllowNegative = false;
            this.txtOppeningStock.isAllowSpecialChar = false;
            this.txtOppeningStock.isNumberOnly = false;
            this.txtOppeningStock.isNumeric = true;
            this.txtOppeningStock.LeaveColor = System.Drawing.Color.White;
            this.txtOppeningStock.Name = "txtOppeningStock";
            this.toolTip1.SetToolTip(this.txtOppeningStock, resources.GetString("txtOppeningStock.ToolTip"));
            this.txtOppeningStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            this.toolTip1.SetToolTip(this.label20, resources.GetString("label20.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // btnAddGroup
            // 
            resources.ApplyResources(this.btnAddGroup, "btnAddGroup");
            this.btnAddGroup.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddGroup.ForeColor = System.Drawing.Color.White;
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.TabStop = false;
            this.toolTip1.SetToolTip(this.btnAddGroup, resources.GetString("btnAddGroup.ToolTip"));
            this.btnAddGroup.UseVisualStyleBackColor = false;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // cmbGroup
            // 
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroup.BackColor = System.Drawing.Color.White;
            this.cmbGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbGroup.Name = "cmbGroup";
            this.toolTip1.SetToolTip(this.cmbGroup, resources.GetString("cmbGroup.ToolTip"));
            // 
            // txtSellingPrice
            // 
            resources.ApplyResources(this.txtSellingPrice, "txtSellingPrice");
            this.txtSellingPrice.BackColor = System.Drawing.Color.White;
            this.txtSellingPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSellingPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSellingPrice.Data = 0;
            this.txtSellingPrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtSellingPrice.Format = null;
            this.txtSellingPrice.isAllowNegative = false;
            this.txtSellingPrice.isAllowSpecialChar = false;
            this.txtSellingPrice.isNumberOnly = false;
            this.txtSellingPrice.isNumeric = true;
            this.txtSellingPrice.LeaveColor = System.Drawing.Color.White;
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.toolTip1.SetToolTip(this.txtSellingPrice, resources.GetString("txtSellingPrice.ToolTip"));
            this.txtSellingPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSellingPrice.Validated += new System.EventHandler(this.txtSellingPrice_Validated);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // pnlGeneralTwo
            // 
            resources.ApplyResources(this.pnlGeneralTwo, "pnlGeneralTwo");
            this.pnlGeneralTwo.Controls.Add(this.cmbProType);
            this.pnlGeneralTwo.Controls.Add(this.label48);
            this.pnlGeneralTwo.Controls.Add(this.txtGrossProfit);
            this.pnlGeneralTwo.Controls.Add(this.cbTranslate);
            this.pnlGeneralTwo.Controls.Add(this.cbTransliterate);
            this.pnlGeneralTwo.Controls.Add(this.txtLocalName);
            this.pnlGeneralTwo.Controls.Add(this.cmbDepot);
            this.pnlGeneralTwo.Controls.Add(this.cmbSupplier);
            this.pnlGeneralTwo.Controls.Add(this.label3);
            this.pnlGeneralTwo.Controls.Add(this.txtGrossMargin);
            this.pnlGeneralTwo.Controls.Add(this.label10);
            this.pnlGeneralTwo.Controls.Add(this.label24);
            this.pnlGeneralTwo.Controls.Add(this.label22);
            this.pnlGeneralTwo.Controls.Add(this.cmbBrand);
            this.pnlGeneralTwo.Controls.Add(this.label11);
            this.pnlGeneralTwo.Name = "pnlGeneralTwo";
            this.toolTip1.SetToolTip(this.pnlGeneralTwo, resources.GetString("pnlGeneralTwo.ToolTip"));
            // 
            // cmbProType
            // 
            resources.ApplyResources(this.cmbProType, "cmbProType");
            this.cmbProType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProType.FormattingEnabled = true;
            this.cmbProType.Items.AddRange(new object[] {
            resources.GetString("cmbProType.Items"),
            resources.GetString("cmbProType.Items1")});
            this.cmbProType.LeaveColor = System.Drawing.Color.White;
            this.cmbProType.Name = "cmbProType";
            this.toolTip1.SetToolTip(this.cmbProType, resources.GetString("cmbProType.ToolTip"));
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.Name = "label48";
            this.toolTip1.SetToolTip(this.label48, resources.GetString("label48.ToolTip"));
            // 
            // txtGrossProfit
            // 
            resources.ApplyResources(this.txtGrossProfit, "txtGrossProfit");
            this.txtGrossProfit.BackColor = System.Drawing.Color.White;
            this.txtGrossProfit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGrossProfit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrossProfit.Data = 0;
            this.txtGrossProfit.EnterColor = System.Drawing.Color.LightPink;
            this.txtGrossProfit.Format = null;
            this.txtGrossProfit.isAllowNegative = false;
            this.txtGrossProfit.isAllowSpecialChar = false;
            this.txtGrossProfit.isNumberOnly = false;
            this.txtGrossProfit.isNumeric = true;
            this.txtGrossProfit.LeaveColor = System.Drawing.Color.White;
            this.txtGrossProfit.Name = "txtGrossProfit";
            this.txtGrossProfit.TabStop = false;
            this.toolTip1.SetToolTip(this.txtGrossProfit, resources.GetString("txtGrossProfit.ToolTip"));
            this.txtGrossProfit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cbTranslate
            // 
            resources.ApplyResources(this.cbTranslate, "cbTranslate");
            this.cbTranslate.Name = "cbTranslate";
            this.cbTranslate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbTranslate, resources.GetString("cbTranslate.ToolTip"));
            this.cbTranslate.UseVisualStyleBackColor = true;
            // 
            // cbTransliterate
            // 
            resources.ApplyResources(this.cbTransliterate, "cbTransliterate");
            this.cbTransliterate.Name = "cbTransliterate";
            this.cbTransliterate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbTransliterate, resources.GetString("cbTransliterate.ToolTip"));
            this.cbTransliterate.UseVisualStyleBackColor = true;
            // 
            // txtLocalName
            // 
            resources.ApplyResources(this.txtLocalName, "txtLocalName");
            this.txtLocalName.BackColor = System.Drawing.Color.White;
            this.txtLocalName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalName.Data = 0;
            this.txtLocalName.EnterColor = System.Drawing.Color.LightPink;
            this.txtLocalName.Format = null;
            this.txtLocalName.isAllowNegative = false;
            this.txtLocalName.isAllowSpecialChar = false;
            this.txtLocalName.isNumberOnly = false;
            this.txtLocalName.isNumeric = false;
            this.txtLocalName.LeaveColor = System.Drawing.Color.White;
            this.txtLocalName.Name = "txtLocalName";
            this.toolTip1.SetToolTip(this.txtLocalName, resources.GetString("txtLocalName.ToolTip"));
            this.txtLocalName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbDepot
            // 
            resources.ApplyResources(this.cmbDepot, "cmbDepot");
            this.cmbDepot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDepot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDepot.EnterColor = System.Drawing.Color.LightPink;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.LeaveColor = System.Drawing.Color.White;
            this.cmbDepot.Name = "cmbDepot";
            this.toolTip1.SetToolTip(this.cmbDepot, resources.GetString("cmbDepot.ToolTip"));
            // 
            // cmbSupplier
            // 
            resources.ApplyResources(this.cmbSupplier, "cmbSupplier");
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.LeaveColor = System.Drawing.Color.White;
            this.cmbSupplier.Name = "cmbSupplier";
            this.toolTip1.SetToolTip(this.cmbSupplier, resources.GetString("cmbSupplier.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // txtGrossMargin
            // 
            resources.ApplyResources(this.txtGrossMargin, "txtGrossMargin");
            this.txtGrossMargin.BackColor = System.Drawing.Color.White;
            this.txtGrossMargin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGrossMargin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGrossMargin.Data = 0;
            this.txtGrossMargin.EnterColor = System.Drawing.Color.LightPink;
            this.txtGrossMargin.Format = null;
            this.txtGrossMargin.isAllowNegative = false;
            this.txtGrossMargin.isAllowSpecialChar = false;
            this.txtGrossMargin.isNumberOnly = false;
            this.txtGrossMargin.isNumeric = true;
            this.txtGrossMargin.LeaveColor = System.Drawing.Color.White;
            this.txtGrossMargin.Name = "txtGrossMargin";
            this.txtGrossMargin.TabStop = false;
            this.toolTip1.SetToolTip(this.txtGrossMargin, resources.GetString("txtGrossMargin.ToolTip"));
            this.txtGrossMargin.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            this.toolTip1.SetToolTip(this.label24, resources.GetString("label24.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            this.toolTip1.SetToolTip(this.label22, resources.GetString("label22.ToolTip"));
            // 
            // cmbBrand
            // 
            resources.ApplyResources(this.cmbBrand, "cmbBrand");
            this.cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBrand.BackColor = System.Drawing.Color.White;
            this.cmbBrand.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.LeaveColor = System.Drawing.Color.White;
            this.cmbBrand.Name = "cmbBrand";
            this.toolTip1.SetToolTip(this.cmbBrand, resources.GetString("cmbBrand.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // pnlFooter
            // 
            resources.ApplyResources(this.pnlFooter, "pnlFooter");
            this.pnlFooter.Controls.Add(this.txtLoyality);
            this.pnlFooter.Controls.Add(this.label23);
            this.pnlFooter.Controls.Add(this.txtOrderInList);
            this.pnlFooter.Controls.Add(this.label12);
            this.pnlFooter.Controls.Add(this.cbInclusive);
            this.pnlFooter.Controls.Add(this.cbScale);
            this.pnlFooter.Controls.Add(this.cbRateBasedWeight);
            this.pnlFooter.Controls.Add(this.cbMultiple);
            this.pnlFooter.Controls.Add(this.cbActive);
            this.pnlFooter.Name = "pnlFooter";
            this.toolTip1.SetToolTip(this.pnlFooter, resources.GetString("pnlFooter.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtLoyality, resources.GetString("txtLoyality.ToolTip"));
            this.txtLoyality.Value = new decimal(new int[] {
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
            this.toolTip1.SetToolTip(this.txtOrderInList, resources.GetString("txtOrderInList.ToolTip"));
            this.txtOrderInList.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // cbInclusive
            // 
            resources.ApplyResources(this.cbInclusive, "cbInclusive");
            this.cbInclusive.Checked = true;
            this.cbInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInclusive.Name = "cbInclusive";
            this.toolTip1.SetToolTip(this.cbInclusive, resources.GetString("cbInclusive.ToolTip"));
            this.cbInclusive.UseVisualStyleBackColor = true;
            // 
            // cbScale
            // 
            resources.ApplyResources(this.cbScale, "cbScale");
            this.cbScale.Name = "cbScale";
            this.toolTip1.SetToolTip(this.cbScale, resources.GetString("cbScale.ToolTip"));
            this.cbScale.UseVisualStyleBackColor = true;
            // 
            // cbRateBasedWeight
            // 
            resources.ApplyResources(this.cbRateBasedWeight, "cbRateBasedWeight");
            this.cbRateBasedWeight.Name = "cbRateBasedWeight";
            this.toolTip1.SetToolTip(this.cbRateBasedWeight, resources.GetString("cbRateBasedWeight.ToolTip"));
            this.cbRateBasedWeight.UseVisualStyleBackColor = true;
            // 
            // cbMultiple
            // 
            resources.ApplyResources(this.cbMultiple, "cbMultiple");
            this.cbMultiple.Checked = true;
            this.cbMultiple.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMultiple.Name = "cbMultiple";
            this.toolTip1.SetToolTip(this.cbMultiple, resources.GetString("cbMultiple.ToolTip"));
            this.cbMultiple.UseVisualStyleBackColor = true;
            // 
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.toolTip1.SetToolTip(this.cbActive, resources.GetString("cbActive.ToolTip"));
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // tpMultiunit
            // 
            resources.ApplyResources(this.tpMultiunit, "tpMultiunit");
            this.tpMultiunit.Controls.Add(this.pnlMultiUnitTwo);
            this.tpMultiunit.Controls.Add(this.pnlMultiunitOne);
            this.tpMultiunit.Name = "tpMultiunit";
            this.toolTip1.SetToolTip(this.tpMultiunit, resources.GetString("tpMultiunit.ToolTip"));
            this.tpMultiunit.UseVisualStyleBackColor = true;
            // 
            // pnlMultiUnitTwo
            // 
            resources.ApplyResources(this.pnlMultiUnitTwo, "pnlMultiUnitTwo");
            this.pnlMultiUnitTwo.Controls.Add(this.dgMultiUnit);
            this.pnlMultiUnitTwo.Name = "pnlMultiUnitTwo";
            this.toolTip1.SetToolTip(this.pnlMultiUnitTwo, resources.GetString("pnlMultiUnitTwo.ToolTip"));
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
            this.toolTip1.SetToolTip(this.dgMultiUnit, resources.GetString("dgMultiUnit.ToolTip"));
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
            // pnlMultiunitOne
            // 
            resources.ApplyResources(this.pnlMultiunitOne, "pnlMultiunitOne");
            this.pnlMultiunitOne.Controls.Add(this.cbAutoGenerated);
            this.pnlMultiunitOne.Controls.Add(this.cbMultiUnitTranslate);
            this.pnlMultiunitOne.Controls.Add(this.cbMultiUnitTransliterate);
            this.pnlMultiunitOne.Controls.Add(this.label25);
            this.pnlMultiunitOne.Controls.Add(this.txtLocalMultiUnit);
            this.pnlMultiunitOne.Controls.Add(this.cmbUnitMulti);
            this.pnlMultiunitOne.Controls.Add(this.cbMultiUnitInclusive);
            this.pnlMultiunitOne.Controls.Add(this.label13);
            this.pnlMultiunitOne.Controls.Add(this.btnAddMultiUnit);
            this.pnlMultiunitOne.Controls.Add(this.label14);
            this.pnlMultiunitOne.Controls.Add(this.txtMultiUnitOppeningStock);
            this.pnlMultiunitOne.Controls.Add(this.txtMultiUnitProductName);
            this.pnlMultiunitOne.Controls.Add(this.label18);
            this.pnlMultiunitOne.Controls.Add(this.label15);
            this.pnlMultiunitOne.Controls.Add(this.txtMultiUnitBarcode);
            this.pnlMultiunitOne.Controls.Add(this.txtUnitQty);
            this.pnlMultiunitOne.Controls.Add(this.label17);
            this.pnlMultiunitOne.Controls.Add(this.label16);
            this.pnlMultiunitOne.Controls.Add(this.txtMultiUnitprice);
            this.pnlMultiunitOne.Name = "pnlMultiunitOne";
            this.toolTip1.SetToolTip(this.pnlMultiunitOne, resources.GetString("pnlMultiunitOne.ToolTip"));
            // 
            // cbAutoGenerated
            // 
            resources.ApplyResources(this.cbAutoGenerated, "cbAutoGenerated");
            this.cbAutoGenerated.Checked = true;
            this.cbAutoGenerated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoGenerated.Name = "cbAutoGenerated";
            this.toolTip1.SetToolTip(this.cbAutoGenerated, resources.GetString("cbAutoGenerated.ToolTip"));
            this.cbAutoGenerated.UseVisualStyleBackColor = true;
            this.cbAutoGenerated.Click += new System.EventHandler(this.cbAutoGenerated_Click);
            // 
            // cbMultiUnitTranslate
            // 
            resources.ApplyResources(this.cbMultiUnitTranslate, "cbMultiUnitTranslate");
            this.cbMultiUnitTranslate.Name = "cbMultiUnitTranslate";
            this.cbMultiUnitTranslate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbMultiUnitTranslate, resources.GetString("cbMultiUnitTranslate.ToolTip"));
            this.cbMultiUnitTranslate.UseVisualStyleBackColor = true;
            // 
            // cbMultiUnitTransliterate
            // 
            resources.ApplyResources(this.cbMultiUnitTransliterate, "cbMultiUnitTransliterate");
            this.cbMultiUnitTransliterate.Checked = true;
            this.cbMultiUnitTransliterate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMultiUnitTransliterate.Name = "cbMultiUnitTransliterate";
            this.cbMultiUnitTransliterate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbMultiUnitTransliterate, resources.GetString("cbMultiUnitTransliterate.ToolTip"));
            this.cbMultiUnitTransliterate.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            this.toolTip1.SetToolTip(this.label25, resources.GetString("label25.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtLocalMultiUnit, resources.GetString("txtLocalMultiUnit.ToolTip"));
            this.txtLocalMultiUnit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbUnitMulti
            // 
            resources.ApplyResources(this.cmbUnitMulti, "cmbUnitMulti");
            this.cmbUnitMulti.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUnitMulti.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnitMulti.BackColor = System.Drawing.Color.White;
            this.cmbUnitMulti.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnitMulti.FormattingEnabled = true;
            this.cmbUnitMulti.LeaveColor = System.Drawing.Color.White;
            this.cmbUnitMulti.Name = "cmbUnitMulti";
            this.toolTip1.SetToolTip(this.cmbUnitMulti, resources.GetString("cmbUnitMulti.ToolTip"));
            this.cmbUnitMulti.SelectedIndexChanged += new System.EventHandler(this.cmbUnitMulti_SelectedIndexChanged);
            // 
            // cbMultiUnitInclusive
            // 
            resources.ApplyResources(this.cbMultiUnitInclusive, "cbMultiUnitInclusive");
            this.cbMultiUnitInclusive.Checked = true;
            this.cbMultiUnitInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMultiUnitInclusive.Name = "cbMultiUnitInclusive";
            this.toolTip1.SetToolTip(this.cbMultiUnitInclusive, resources.GetString("cbMultiUnitInclusive.ToolTip"));
            this.cbMultiUnitInclusive.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // btnAddMultiUnit
            // 
            resources.ApplyResources(this.btnAddMultiUnit, "btnAddMultiUnit");
            this.btnAddMultiUnit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddMultiUnit.ForeColor = System.Drawing.Color.White;
            this.btnAddMultiUnit.Name = "btnAddMultiUnit";
            this.toolTip1.SetToolTip(this.btnAddMultiUnit, resources.GetString("btnAddMultiUnit.ToolTip"));
            this.btnAddMultiUnit.UseVisualStyleBackColor = false;
            this.btnAddMultiUnit.Click += new System.EventHandler(this.btnAddMultiUnit_Click);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip1.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtMultiUnitOppeningStock, resources.GetString("txtMultiUnitOppeningStock.ToolTip"));
            this.txtMultiUnitOppeningStock.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMultiUnitOppeningStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMultiUnitOppeningStock_KeyDown);
            // 
            // txtMultiUnitProductName
            // 
            resources.ApplyResources(this.txtMultiUnitProductName, "txtMultiUnitProductName");
            this.txtMultiUnitProductName.BackColor = System.Drawing.Color.White;
            this.txtMultiUnitProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMultiUnitProductName.Data = 0;
            this.txtMultiUnitProductName.EnterColor = System.Drawing.Color.LightPink;
            this.txtMultiUnitProductName.Format = null;
            this.txtMultiUnitProductName.isAllowNegative = false;
            this.txtMultiUnitProductName.isAllowSpecialChar = false;
            this.txtMultiUnitProductName.isNumberOnly = false;
            this.txtMultiUnitProductName.isNumeric = false;
            this.txtMultiUnitProductName.LeaveColor = System.Drawing.Color.White;
            this.txtMultiUnitProductName.Name = "txtMultiUnitProductName";
            this.toolTip1.SetToolTip(this.txtMultiUnitProductName, resources.GetString("txtMultiUnitProductName.ToolTip"));
            this.txtMultiUnitProductName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMultiUnitProductName.Validated += new System.EventHandler(this.txtMultiUnitProductName_Validated);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            this.toolTip1.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            this.toolTip1.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtMultiUnitBarcode, resources.GetString("txtMultiUnitBarcode.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtUnitQty, resources.GetString("txtUnitQty.ToolTip"));
            this.txtUnitQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitQty.Validated += new System.EventHandler(this.txtUnitQty_Validated);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            this.toolTip1.SetToolTip(this.label17, resources.GetString("label17.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip1.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtMultiUnitprice, resources.GetString("txtMultiUnitprice.ToolTip"));
            this.txtMultiUnitprice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // tpMultiBarcode
            // 
            resources.ApplyResources(this.tpMultiBarcode, "tpMultiBarcode");
            this.tpMultiBarcode.Controls.Add(this.panel4);
            this.tpMultiBarcode.Controls.Add(this.pnlMultiBarcode);
            this.tpMultiBarcode.Name = "tpMultiBarcode";
            this.toolTip1.SetToolTip(this.tpMultiBarcode, resources.GetString("tpMultiBarcode.ToolTip"));
            this.tpMultiBarcode.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.dgMultiBarcode);
            this.panel4.Name = "panel4";
            this.toolTip1.SetToolTip(this.panel4, resources.GetString("panel4.ToolTip"));
            // 
            // dgMultiBarcode
            // 
            resources.ApplyResources(this.dgMultiBarcode, "dgMultiBarcode");
            this.dgMultiBarcode.AllowUserToAddRows = false;
            this.dgMultiBarcode.AllowUserToDeleteRows = false;
            this.dgMultiBarcode.BackgroundColor = System.Drawing.Color.White;
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
            this.toolTip1.SetToolTip(this.dgMultiBarcode, resources.GetString("dgMultiBarcode.ToolTip"));
            this.dgMultiBarcode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiBarcode_CellClick);
            this.dgMultiBarcode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiBarcode_CellContentClick);
            this.dgMultiBarcode.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgMultiBarcode_DataError);
            // 
            // col_SL
            // 
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
            resources.ApplyResources(this.col_Close, "col_Close");
            this.col_Close.Name = "col_Close";
            this.col_Close.ReadOnly = true;
            // 
            // pnlMultiBarcode
            // 
            resources.ApplyResources(this.pnlMultiBarcode, "pnlMultiBarcode");
            this.pnlMultiBarcode.Controls.Add(this.cmbUnitMultiBarcode);
            this.pnlMultiBarcode.Controls.Add(this.label21);
            this.pnlMultiBarcode.Controls.Add(this.txtMultiBarcode);
            this.pnlMultiBarcode.Controls.Add(this.btnAddMultiBarcode);
            this.pnlMultiBarcode.Controls.Add(this.label19);
            this.pnlMultiBarcode.Name = "pnlMultiBarcode";
            this.toolTip1.SetToolTip(this.pnlMultiBarcode, resources.GetString("pnlMultiBarcode.ToolTip"));
            // 
            // cmbUnitMultiBarcode
            // 
            resources.ApplyResources(this.cmbUnitMultiBarcode, "cmbUnitMultiBarcode");
            this.cmbUnitMultiBarcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUnitMultiBarcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnitMultiBarcode.BackColor = System.Drawing.Color.White;
            this.cmbUnitMultiBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnitMultiBarcode.FormattingEnabled = true;
            this.cmbUnitMultiBarcode.LeaveColor = System.Drawing.Color.White;
            this.cmbUnitMultiBarcode.Name = "cmbUnitMultiBarcode";
            this.cmbUnitMultiBarcode.TabStop = false;
            this.toolTip1.SetToolTip(this.cmbUnitMultiBarcode, resources.GetString("cmbUnitMultiBarcode.ToolTip"));
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            this.toolTip1.SetToolTip(this.label21, resources.GetString("label21.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtMultiBarcode, resources.GetString("txtMultiBarcode.ToolTip"));
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
            this.toolTip1.SetToolTip(this.btnAddMultiBarcode, resources.GetString("btnAddMultiBarcode.ToolTip"));
            this.btnAddMultiBarcode.UseVisualStyleBackColor = false;
            this.btnAddMultiBarcode.Click += new System.EventHandler(this.btnAddMultiBarcode_Click);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            this.toolTip1.SetToolTip(this.label19, resources.GetString("label19.ToolTip"));
            // 
            // tpMultiRate
            // 
            resources.ApplyResources(this.tpMultiRate, "tpMultiRate");
            this.tpMultiRate.Controls.Add(this.pnlMultiRates);
            this.tpMultiRate.Name = "tpMultiRate";
            this.toolTip1.SetToolTip(this.tpMultiRate, resources.GetString("tpMultiRate.ToolTip"));
            this.tpMultiRate.UseVisualStyleBackColor = true;
            // 
            // pnlMultiRates
            // 
            resources.ApplyResources(this.pnlMultiRates, "pnlMultiRates");
            this.pnlMultiRates.Controls.Add(this.dgMultiRates);
            this.pnlMultiRates.Name = "pnlMultiRates";
            this.toolTip1.SetToolTip(this.pnlMultiRates, resources.GetString("pnlMultiRates.ToolTip"));
            // 
            // dgMultiRates
            // 
            resources.ApplyResources(this.dgMultiRates, "dgMultiRates");
            this.dgMultiRates.AllowUserToAddRows = false;
            this.dgMultiRates.AllowUserToDeleteRows = false;
            this.dgMultiRates.BackgroundColor = System.Drawing.Color.White;
            this.dgMultiRates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMultiRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMultiRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SLNo,
            this.col_PriceCategoryName,
            this.col_UnitName,
            this.col_SaleRate,
            this.col_SaleDiscount,
            this.col_PurchaseRate,
            this.col_PurchaseDiscount,
            this.col_MultiPriceBarcode,
            this.col_MultiRateDelete});
            this.dgMultiRates.Name = "dgMultiRates";
            this.dgMultiRates.RowHeadersVisible = false;
            this.toolTip1.SetToolTip(this.dgMultiRates, resources.GetString("dgMultiRates.ToolTip"));
            this.dgMultiRates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMultiRates_CellClick);
            // 
            // col_SLNo
            // 
            this.col_SLNo.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SLNo, "col_SLNo");
            this.col_SLNo.Name = "col_SLNo";
            // 
            // col_PriceCategoryName
            // 
            this.col_PriceCategoryName.DataPropertyName = "PCategoryName";
            resources.ApplyResources(this.col_PriceCategoryName, "col_PriceCategoryName");
            this.col_PriceCategoryName.Name = "col_PriceCategoryName";
            // 
            // col_UnitName
            // 
            this.col_UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(this.col_UnitName, "col_UnitName");
            this.col_UnitName.Name = "col_UnitName";
            // 
            // col_SaleRate
            // 
            this.col_SaleRate.DataPropertyName = "SPrice";
            resources.ApplyResources(this.col_SaleRate, "col_SaleRate");
            this.col_SaleRate.Name = "col_SaleRate";
            // 
            // col_SaleDiscount
            // 
            this.col_SaleDiscount.DataPropertyName = "SaleDiscount";
            resources.ApplyResources(this.col_SaleDiscount, "col_SaleDiscount");
            this.col_SaleDiscount.Name = "col_SaleDiscount";
            // 
            // col_PurchaseRate
            // 
            this.col_PurchaseRate.DataPropertyName = "PPrice";
            resources.ApplyResources(this.col_PurchaseRate, "col_PurchaseRate");
            this.col_PurchaseRate.Name = "col_PurchaseRate";
            // 
            // col_PurchaseDiscount
            // 
            this.col_PurchaseDiscount.DataPropertyName = "PurchaseDiscount";
            resources.ApplyResources(this.col_PurchaseDiscount, "col_PurchaseDiscount");
            this.col_PurchaseDiscount.Name = "col_PurchaseDiscount";
            // 
            // col_MultiPriceBarcode
            // 
            this.col_MultiPriceBarcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_MultiPriceBarcode, "col_MultiPriceBarcode");
            this.col_MultiPriceBarcode.Name = "col_MultiPriceBarcode";
            // 
            // col_MultiRateDelete
            // 
            resources.ApplyResources(this.col_MultiRateDelete, "col_MultiRateDelete");
            this.col_MultiRateDelete.Name = "col_MultiRateDelete";
            // 
            // tabSize
            // 
            resources.ApplyResources(this.tabSize, "tabSize");
            this.tabSize.Controls.Add(this.pnlMultiSize);
            this.tabSize.Controls.Add(this.btnSizeRemoveImage);
            this.tabSize.Controls.Add(this.panel6);
            this.tabSize.Controls.Add(this.btnSizeAddImg);
            this.tabSize.Controls.Add(this.btnAddSize);
            this.tabSize.Controls.Add(this.dgSize);
            this.tabSize.Name = "tabSize";
            this.toolTip1.SetToolTip(this.tabSize, resources.GetString("tabSize.ToolTip"));
            this.tabSize.UseVisualStyleBackColor = true;
            // 
            // pnlMultiSize
            // 
            resources.ApplyResources(this.pnlMultiSize, "pnlMultiSize");
            this.pnlMultiSize.Controls.Add(this.txtSizeName);
            this.pnlMultiSize.Controls.Add(this.txtSizePrice);
            this.pnlMultiSize.Controls.Add(this.label30);
            this.pnlMultiSize.Controls.Add(this.label28);
            this.pnlMultiSize.Controls.Add(this.cbSizeTraslate);
            this.pnlMultiSize.Controls.Add(this.txtSizeBarcode);
            this.pnlMultiSize.Controls.Add(this.cbSizeTrasliterate);
            this.pnlMultiSize.Controls.Add(this.lblMultiSize);
            this.pnlMultiSize.Controls.Add(this.label26);
            this.pnlMultiSize.Controls.Add(this.txtSizeLocalName);
            this.pnlMultiSize.Name = "pnlMultiSize";
            this.toolTip1.SetToolTip(this.pnlMultiSize, resources.GetString("pnlMultiSize.ToolTip"));
            // 
            // txtSizeName
            // 
            resources.ApplyResources(this.txtSizeName, "txtSizeName");
            this.txtSizeName.BackColor = System.Drawing.Color.White;
            this.txtSizeName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSizeName.Data = 0;
            this.txtSizeName.EnterColor = System.Drawing.Color.LightPink;
            this.txtSizeName.Format = null;
            this.txtSizeName.isAllowNegative = false;
            this.txtSizeName.isAllowSpecialChar = false;
            this.txtSizeName.isNumberOnly = false;
            this.txtSizeName.isNumeric = false;
            this.txtSizeName.LeaveColor = System.Drawing.Color.White;
            this.txtSizeName.Name = "txtSizeName";
            this.toolTip1.SetToolTip(this.txtSizeName, resources.GetString("txtSizeName.ToolTip"));
            this.txtSizeName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizeName.Validated += new System.EventHandler(this.txtSizeName_Validated);
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
            this.toolTip1.SetToolTip(this.txtSizePrice, resources.GetString("txtSizePrice.ToolTip"));
            this.txtSizePrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSizePrice_KeyDown);
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            this.toolTip1.SetToolTip(this.label30, resources.GetString("label30.ToolTip"));
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            this.toolTip1.SetToolTip(this.label28, resources.GetString("label28.ToolTip"));
            // 
            // cbSizeTraslate
            // 
            resources.ApplyResources(this.cbSizeTraslate, "cbSizeTraslate");
            this.cbSizeTraslate.Name = "cbSizeTraslate";
            this.cbSizeTraslate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbSizeTraslate, resources.GetString("cbSizeTraslate.ToolTip"));
            this.cbSizeTraslate.UseVisualStyleBackColor = true;
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
            this.toolTip1.SetToolTip(this.txtSizeBarcode, resources.GetString("txtSizeBarcode.ToolTip"));
            this.txtSizeBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSizeBarcode.Validated += new System.EventHandler(this.txtSizeBarcode_Validated);
            // 
            // cbSizeTrasliterate
            // 
            resources.ApplyResources(this.cbSizeTrasliterate, "cbSizeTrasliterate");
            this.cbSizeTrasliterate.Checked = true;
            this.cbSizeTrasliterate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSizeTrasliterate.Name = "cbSizeTrasliterate";
            this.cbSizeTrasliterate.TabStop = false;
            this.toolTip1.SetToolTip(this.cbSizeTrasliterate, resources.GetString("cbSizeTrasliterate.ToolTip"));
            this.cbSizeTrasliterate.UseVisualStyleBackColor = true;
            // 
            // lblMultiSize
            // 
            resources.ApplyResources(this.lblMultiSize, "lblMultiSize");
            this.lblMultiSize.Name = "lblMultiSize";
            this.toolTip1.SetToolTip(this.lblMultiSize, resources.GetString("lblMultiSize.ToolTip"));
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            this.toolTip1.SetToolTip(this.label26, resources.GetString("label26.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtSizeLocalName, resources.GetString("txtSizeLocalName.ToolTip"));
            this.txtSizeLocalName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnSizeRemoveImage
            // 
            resources.ApplyResources(this.btnSizeRemoveImage, "btnSizeRemoveImage");
            this.btnSizeRemoveImage.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSizeRemoveImage.ForeColor = System.Drawing.Color.White;
            this.btnSizeRemoveImage.Name = "btnSizeRemoveImage";
            this.toolTip1.SetToolTip(this.btnSizeRemoveImage, resources.GetString("btnSizeRemoveImage.ToolTip"));
            this.btnSizeRemoveImage.UseVisualStyleBackColor = false;
            this.btnSizeRemoveImage.Click += new System.EventHandler(this.btnSizeRemoveImage_Click);
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel6.Controls.Add(this.pbSizeImage);
            this.panel6.Name = "panel6";
            this.toolTip1.SetToolTip(this.panel6, resources.GetString("panel6.ToolTip"));
            // 
            // pbSizeImage
            // 
            resources.ApplyResources(this.pbSizeImage, "pbSizeImage");
            this.pbSizeImage.BackColor = System.Drawing.Color.Snow;
            this.pbSizeImage.Name = "pbSizeImage";
            this.pbSizeImage.TabStop = false;
            this.toolTip1.SetToolTip(this.pbSizeImage, resources.GetString("pbSizeImage.ToolTip"));
            // 
            // btnSizeAddImg
            // 
            resources.ApplyResources(this.btnSizeAddImg, "btnSizeAddImg");
            this.btnSizeAddImg.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSizeAddImg.ForeColor = System.Drawing.Color.White;
            this.btnSizeAddImg.Name = "btnSizeAddImg";
            this.toolTip1.SetToolTip(this.btnSizeAddImg, resources.GetString("btnSizeAddImg.ToolTip"));
            this.btnSizeAddImg.UseVisualStyleBackColor = false;
            this.btnSizeAddImg.Click += new System.EventHandler(this.btnSizeAddImg_Click);
            // 
            // btnAddSize
            // 
            resources.ApplyResources(this.btnAddSize, "btnAddSize");
            this.btnAddSize.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddSize.ForeColor = System.Drawing.Color.White;
            this.btnAddSize.Name = "btnAddSize";
            this.toolTip1.SetToolTip(this.btnAddSize, resources.GetString("btnAddSize.ToolTip"));
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
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn18,
            this.col_SizeClose});
            this.dgSize.Name = "dgSize";
            this.dgSize.ReadOnly = true;
            this.dgSize.RowHeadersVisible = false;
            this.toolTip1.SetToolTip(this.dgSize, resources.GetString("dgSize.ToolTip"));
            this.dgSize.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSize_CellClick);
            this.dgSize.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSize_CellContentClick);
            this.dgSize.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgSize_DataError);
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "ProductBarcodeName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Barcode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "SellingPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn18, "dataGridViewTextBoxColumn18");
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            // 
            // col_SizeClose
            // 
            resources.ApplyResources(this.col_SizeClose, "col_SizeClose");
            this.col_SizeClose.Name = "col_SizeClose";
            this.col_SizeClose.ReadOnly = true;
            // 
            // tabAdditional
            // 
            resources.ApplyResources(this.tabAdditional, "tabAdditional");
            this.tabAdditional.Controls.Add(this.pnlAdditional);
            this.tabAdditional.Name = "tabAdditional";
            this.toolTip1.SetToolTip(this.tabAdditional, resources.GetString("tabAdditional.ToolTip"));
            this.tabAdditional.UseVisualStyleBackColor = true;
            // 
            // pnlAdditional
            // 
            resources.ApplyResources(this.pnlAdditional, "pnlAdditional");
            this.pnlAdditional.Controls.Add(this.cmbPriority);
            this.pnlAdditional.Controls.Add(this.cmbRestaurantType);
            this.pnlAdditional.Controls.Add(this.label32);
            this.pnlAdditional.Controls.Add(this.label31);
            this.pnlAdditional.Controls.Add(this.cbSerialNumber);
            this.pnlAdditional.Controls.Add(this.cbPercentageBased);
            this.pnlAdditional.Controls.Add(this.cbFavaurite);
            this.pnlAdditional.Controls.Add(this.label34);
            this.pnlAdditional.Controls.Add(this.label33);
            this.pnlAdditional.Controls.Add(this.label29);
            this.pnlAdditional.Controls.Add(this.txtItemCalory);
            this.pnlAdditional.Controls.Add(this.txtExiCommission);
            this.pnlAdditional.Controls.Add(this.txtCookingTime);
            this.pnlAdditional.Name = "pnlAdditional";
            this.toolTip1.SetToolTip(this.pnlAdditional, resources.GetString("pnlAdditional.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbPriority, resources.GetString("cmbPriority.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbRestaurantType, resources.GetString("cmbRestaurantType.ToolTip"));
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            this.toolTip1.SetToolTip(this.label32, resources.GetString("label32.ToolTip"));
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            this.toolTip1.SetToolTip(this.label31, resources.GetString("label31.ToolTip"));
            // 
            // cbSerialNumber
            // 
            resources.ApplyResources(this.cbSerialNumber, "cbSerialNumber");
            this.cbSerialNumber.Name = "cbSerialNumber";
            this.toolTip1.SetToolTip(this.cbSerialNumber, resources.GetString("cbSerialNumber.ToolTip"));
            this.cbSerialNumber.UseVisualStyleBackColor = true;
            // 
            // cbPercentageBased
            // 
            resources.ApplyResources(this.cbPercentageBased, "cbPercentageBased");
            this.cbPercentageBased.Checked = true;
            this.cbPercentageBased.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPercentageBased.Name = "cbPercentageBased";
            this.toolTip1.SetToolTip(this.cbPercentageBased, resources.GetString("cbPercentageBased.ToolTip"));
            this.cbPercentageBased.UseVisualStyleBackColor = true;
            // 
            // cbFavaurite
            // 
            resources.ApplyResources(this.cbFavaurite, "cbFavaurite");
            this.cbFavaurite.Name = "cbFavaurite";
            this.toolTip1.SetToolTip(this.cbFavaurite, resources.GetString("cbFavaurite.ToolTip"));
            this.cbFavaurite.UseVisualStyleBackColor = true;
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
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            this.toolTip1.SetToolTip(this.label29, resources.GetString("label29.ToolTip"));
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
            this.toolTip1.SetToolTip(this.txtItemCalory, resources.GetString("txtItemCalory.ToolTip"));
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
            this.txtExiCommission.isNumberOnly = true;
            this.txtExiCommission.isNumeric = false;
            this.txtExiCommission.LeaveColor = System.Drawing.Color.White;
            this.txtExiCommission.Name = "txtExiCommission";
            this.toolTip1.SetToolTip(this.txtExiCommission, resources.GetString("txtExiCommission.ToolTip"));
            this.txtExiCommission.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.toolTip1.SetToolTip(this.txtCookingTime, resources.GetString("txtCookingTime.ToolTip"));
            this.txtCookingTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Barcode";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "BarcodeType";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "SL";
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "PCategoryName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "UnitName";
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "SPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "SaleDiscount";
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "PPrice";
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "PurchaseDiscount";
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.BackColor = System.Drawing.Color.LightPink;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Data = 0;
            this.txtSearch.EnterColor = System.Drawing.Color.LightPink;
            this.txtSearch.Format = null;
            this.txtSearch.isAllowNegative = false;
            this.txtSearch.isAllowSpecialChar = false;
            this.txtSearch.isNumberOnly = false;
            this.txtSearch.isNumeric = false;
            this.txtSearch.LeaveColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.Name = "txtSearch";
            this.toolTip1.SetToolTip(this.txtSearch, resources.GetString("txtSearch.ToolTip"));
            this.txtSearch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSearch.Validated += new System.EventHandler(this.txtSearch_Validated);
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
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.toolTip1.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            // POSItemCreation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnBarcode);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tabProduct);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "POSItemCreation";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.POSItemCreation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSItemCreation_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            this.Controls.SetChildIndex(this.lblSearch, 0);
            this.Controls.SetChildIndex(this.tabProduct, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnBarcode, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.tabProduct.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.pnlGeneralOne.ResumeLayout(false);
            this.pnlGeneralOne.PerformLayout();
            this.pnlGeneralTwo.ResumeLayout(false);
            this.pnlGeneralTwo.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.tpMultiunit.ResumeLayout(false);
            this.pnlMultiUnitTwo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiUnit)).EndInit();
            this.pnlMultiunitOne.ResumeLayout(false);
            this.pnlMultiunitOne.PerformLayout();
            this.tpMultiBarcode.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiBarcode)).EndInit();
            this.pnlMultiBarcode.ResumeLayout(false);
            this.pnlMultiBarcode.PerformLayout();
            this.tpMultiRate.ResumeLayout(false);
            this.pnlMultiRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMultiRates)).EndInit();
            this.tabSize.ResumeLayout(false);
            this.pnlMultiSize.ResumeLayout(false);
            this.pnlMultiSize.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSizeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSize)).EndInit();
            this.tabAdditional.ResumeLayout(false);
            this.pnlAdditional.ResumeLayout(false);
            this.pnlAdditional.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMultiBarcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
