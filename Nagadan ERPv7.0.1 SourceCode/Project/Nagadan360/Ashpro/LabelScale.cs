using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class LabelScale : BaseForm
	{
		private DBContext db = new DBContext();

		private List<ProductEntity> entProducts;

		private List<ExProductEntity> exProducts = new List<ExProductEntity>();

		private List<ProductEntity> entSelectedProduct = new List<ProductEntity>();

		private ProductEntity selectedProduct;

		private int? iMasterId;

		private BarcodeScale barcodeScale;

		private List<BarcodeScale> scales;

		private IContainer components = null;

		private Label lblHeader;

		private Label label1;

		private AshComboBox cmbProGroup;

		private Label label2;

		private CheckBox cbAll;

		private DataGridView dgSelectedProducts;

		private DataGridView dgProducts;

		private BindingSource bdsProduct;

		private Button btnAdd;

		private Button btnRemove;

		private BindingSource bdsSelectProduct;

		private Label label3;

		private AshTextBoxNormal txtScaleName;

		private Label label4;

		private Button btnLoadScaleProduct;

		private Button btnLoadFromPurchase;

		private AshTextBoxNormal txtIP;

		private Button btnSave;

		private Button btnRemoveAll;

		private Button btnDelete;

		private Button btnClear;

		private Button btnProductAllocationToScale;

		private Button btnUpload;

		private DataGridViewTextBoxColumn col_Name;

		private DataGridViewTextBoxColumn col_ProBarcode;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_Quantity;

		private DataGridViewCheckBoxColumn col_Select;

		private DataGridViewTextBoxColumn col_Selection;

		private DataGridViewTextBoxColumn col_Barcode;

		private Button btnClearAll;

		private Label label5;

		private AshComboBox cmbStarting;

		private Button btnAddLabel;

		private DataGridView dgLabelList;

		private DataGridViewTextBoxColumn col_LabelName;

		private DataGridViewTextBoxColumn col_IPAddress;

		private BindingSource bdsLabelList;

		private Label label6;

		private AshComboBox cmbPriceCategory;

		public LabelScale()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void CollDataAddMeethod(TextBox tBox, string sColumn)
		{
			AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
			List<string> list = new List<string>();
			list = AshproCommon.GetStringListMethod("Select " + sColumn + " From BarcodeScale");
			autoCompleteStringCollection.AddRange(Enumerable.ToArray(list));
			tBox.AutoCompleteMode = AutoCompleteMode.Suggest;
			tBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tBox.AutoCompleteCustomSource = autoCompleteStringCollection;
		}

		private void FillSelectedProducts()
		{
			dgSelectedProducts.DataSource = null;
			bdsSelectProduct.DataSource = null;
			bdsSelectProduct.DataSource = entSelectedProduct;
			dgSelectedProducts.DataSource = entSelectedProduct;
		}

		private void DgvLoadMethod(bool isSelected = false)
		{
			if (exProducts.Count > 0)
			{
				foreach (ExProductEntity exProduct in exProducts)
				{
					exProduct.IsSelected = isSelected;
				}
			}
			dgProducts.AutoGenerateColumns = false;
			bdsProduct.DataSource = null;
			dgProducts.DataSource = null;
			bdsProduct.DataSource = exProducts;
			dgProducts.DataSource = bdsProduct;
		}

		private void LabelScale_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbProGroup);
			AniHelper.FillPCategory(cmbPriceCategory);
			scales = new List<BarcodeScale>();
			CollDataAddMeethod(txtScaleName, "Name");
			CollDataAddMeethod(txtIP, "IPAddress");
			cmbStarting.Items.Add(CommonSettings.strRatebasedBarcode);
			cmbStarting.Items.Add(CommonSettings.strWeightbasedBarcode);
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Rate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			col_Quantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			AclasSDKHelper.AClasInitialze();
			barcodeScale = db.BarcodeScale().FirstOrDefault();
			if (barcodeScale != null)
			{
				txtIP.Text = barcodeScale.IPAddress;
				txtScaleName.Text = barcodeScale.Name;
				iMasterId = barcodeScale.Id;
			}
			exProducts = new List<ExProductEntity>();
			DgvLoadMethod();
		}

		private void cmbProGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			exProducts = new List<ExProductEntity>();
			entProducts = new List<ProductEntity>();
			entProducts = db.Products(new
			{
				PGroupId = cmbProGroup.SelectedValue.ToInt32()
			});
			List<ExProductEntity> list = new List<ExProductEntity>();
			list = AniHelper.CopyListData<ExProductEntity>(entProducts.Cast<object>().ToList());
			exProducts.AddRange(list);
			DgvLoadMethod();
		}

		private void cbAll_CheckedChanged(object sender, EventArgs e)
		{
			foreach (ExProductEntity exProduct in exProducts)
			{
				exProduct.IsSelected = cbAll.Checked;
			}
			dgProducts.Refresh();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			entSelectedProduct = new List<ProductEntity>();
			entSelectedProduct = AniHelper.CopyListData<ProductEntity>(exProducts.Where((ExProductEntity x) => x.IsSelected).Cast<object>().ToList());
			foreach (ProductEntity item in entSelectedProduct)
			{
				item.UnitId = db.Units(new { item.UnitId }).FirstOrDefault().UnitNo;
				ProductPriceCategory productPriceCategory = db.ProductPriceCategories(new
				{
					Barcode = item.Barcode,
					PCategoryId = cmbPriceCategory.SelectedValue.ToInt32()
				}).FirstOrDefault();
				if (productPriceCategory != null)
				{
					item.SellingPrice = productPriceCategory.SPrice;
				}
			}
			dgSelectedProducts.AutoGenerateColumns = false;
			FillSelectedProducts();
			cbAll.Checked = false;
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			entSelectedProduct = new List<ProductEntity>();
			bdsSelectProduct.DataSource = null;
			dgSelectedProducts.DataSource = null;
			dgSelectedProducts.Refresh();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (dgSelectedProducts.CurrentRow != null)
			{
				selectedProduct = (ProductEntity)dgSelectedProducts.CurrentRow.DataBoundItem;
			}
			entSelectedProduct.Remove(selectedProduct);
			FillSelectedProducts();
		}

		private void dgProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSelectedProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (!(txtIP.Text == string.Empty) && !(txtScaleName.Text == string.Empty))
			{
				BarcodeScale entity = new BarcodeScale
				{
					Id = iMasterId,
					Name = txtScaleName.Text,
					IPAddress = txtIP.Text,
					Remarks = ""
				};
				if (!iMasterId.HasValue)
				{
					if (AshproCommon.InsertMethod_SP(entity, "spInsertBarcodeScale"))
					{
						Messages.SavedMessage();
					}
				}
				else if (AshproCommon.UpdateMethod_SP(entity, "spUpdateBarcodeScale"))
				{
					Messages.UpdateMessage();
				}
				btnClear_Click(null, null);
			}
			else
			{
				Messages.ErrorMessage("Please fill all details");
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!iMasterId.HasValue)
			{
				Messages.WarningMessage("Please select one to delete");
				return;
			}
			if (Messages.DeleteConfirmationMessage())
			{
				AshproCommon.DeleteFromDatabase("BarcodeScale", "Id", iMasterId.toInt32());
			}
			btnClear_Click(null, null);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			string text2 = (txtScaleName.Text = (txtIP.Text = string.Empty));
			txtScaleName.Focus();
			iMasterId = null;
			CollDataAddMeethod(txtScaleName, "Name");
			CollDataAddMeethod(txtIP, "IPAddress");
			barcodeScale = new BarcodeScale();
		}

		private void txtScaleName_Validated(object sender, EventArgs e)
		{
			barcodeScale = db.BarcodeScale(new
			{
				Name = txtScaleName.Text
			}).FirstOrDefault();
			if (barcodeScale != null)
			{
				txtIP.Text = barcodeScale.IPAddress;
				iMasterId = barcodeScale.Id;
			}
		}

		private void txtIP_Validated(object sender, EventArgs e)
		{
			BarcodeScale barcodeScale = db.BarcodeScale(new
			{
				IPAddress = txtIP.Text
			}).FirstOrDefault();
			if (barcodeScale != null)
			{
				txtScaleName.Text = barcodeScale.Name;
				iMasterId = barcodeScale.Id;
			}
		}

		private void btnProductAllocationToScale_Click(object sender, EventArgs e)
		{
			try
			{
				if (iMasterId.HasValue && entSelectedProduct.Count != 0)
				{
					List<ProductBarcodeScale> list = new List<ProductBarcodeScale>();
					List<ProductBarcodeScale> list2 = new List<ProductBarcodeScale>();
					list2 = db.ProductBarcodeScales(new
					{
						FK_BarcodeScale = iMasterId
					}).ToList();
					foreach (ProductEntity item2 in entSelectedProduct)
					{
						ProductBarcodeScale item = new ProductBarcodeScale
						{
							FK_BarcodeScale = iMasterId,
							FK_ProductId = item2.ProductId
						};
						list.Add(item);
					}
					if (AshproCommon.UpdateDatabase(list.Cast<object>().ToList(), list2.Cast<object>().ToList(), "ProductBarcodeScale", "Id"))
					{
						Messages.SavedMessage();
					}
				}
				else
				{
					Messages.ErrorMessage("Please select Barcode Label Sale and Products. And try again later");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnLoadScaleProduct_Click(object sender, EventArgs e)
		{
			try
			{
				exProducts = new List<ExProductEntity>();
				DgvLoadMethod();
				List<ProductBarcodeScale> list = new List<ProductBarcodeScale>();
				list = db.ProductBarcodeScales(new
				{
					FK_BarcodeScale = iMasterId
				}).ToList();
				foreach (ProductBarcodeScale item2 in list)
				{
					ProductEntity item = db.Products(new
					{
						ProductId = item2.FK_ProductId
					}).FirstOrDefault();
					entProducts.Add(item);
				}
				exProducts = AniHelper.CopyListData<ExProductEntity>(entProducts.Cast<object>().ToList());
				DgvLoadMethod(isSelected: true);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnLoadFromPurchase_Click(object sender, EventArgs e)
		{
			try
			{
				List<PurchaseInvoiceDtl> list = new List<PurchaseInvoiceDtl>();
				FormSearch formSearch = new FormSearch();
				formSearch.sFormType = "";
				formSearch.sInvType = "Purchase Invoice";
				if (formSearch.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				list = db.PurInvSettings(new
				{
					PurInvId = formSearch.iInvoiceId
				}).ToList();
				foreach (PurchaseInvoiceDtl item in list)
				{
					if (db.ProductBarcodeScales(new
					{
						FK_BarcodeScale = iMasterId
					}).Any((ProductBarcodeScale x) => x.FK_ProductId == item.ProId))
					{
						ProductEntity item2 = db.Products(new
						{
							ProductId = item.ProId
						}).FirstOrDefault();
						entProducts.Add(item2);
					}
				}
				exProducts = AniHelper.CopyListData<ExProductEntity>(entProducts.Cast<object>().ToList());
				DgvLoadMethod(isSelected: true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnUpload_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "0";
				string text2 = "";
				string text3 = "22-09-14 15:06";
				int num = 0;
				int num2 = 1;
				if (entSelectedProduct.Count <= 0)
				{
					Messages.ErrorMessage("There is no Products selected to upload.");
					return;
				}
				List<BarcodeLabelScaleEntity> list = new List<BarcodeLabelScaleEntity>();
				foreach (ProductEntity item2 in entSelectedProduct)
				{
					string name = ((item2.LocalName == null) ? text2 : item2.LocalName.Trim());
					int num3 = item2.Barcode.Substring(0, 2).ToInt32();
					int num4 = item2.Barcode.Substring(2, item2.Barcode.Length - 2).ToInt32();
					int barcodeType = 0;
					if (CommonSettings.strWeightbasedBarcode != string.Empty)
					{
						barcodeType = ((num3 == CommonSettings.strWeightbasedBarcode.Substring(0, 2).ToInt32()) ? 7 : 2);
					}
					BarcodeLabelScaleEntity item = new BarcodeLabelScaleEntity
					{
						ID = num4,
						ItemCode = num4,
						DepartmentID = num3,
						GroupID = 0,
						Name1 = item2.ProductName.Trim(),
						Name2 = name,
						Name3 = text2,
						Price = item2.SellingPrice,
						UnitID = item2.UnitId,
						BarcodeType1 = barcodeType,
						BarcodeType2 = num,
						Label1ID = 1,
						Label2ID = num,
						ProducedDate = text3,
						FreshnessDate = text,
						ValidDate = text,
						PackageType = text,
						PackageWeight = text,
						PackagePrice = text,
						PackageRange = text,
						PackageDays = num,
						PackageHours = num,
						Text1ID = num,
						Text2ID = num,
						Text3ID = num,
						Text4ID = num,
						Text5ID = num,
						Text6ID = num,
						Text7ID = num,
						Text8ID = num,
						DiscountID = num,
						DiscountRate = num,
						HalfDiscount = num,
						QuarterDiscount = num,
						TareID = num,
						TareValue = num,
						IceID = num,
						ICEValue = num,
						OriginID = num,
						TraceabilityID = num2,
						LimitPrice = num,
						Tax1 = num,
						Tax2 = num,
						Tax3 = num,
						Tax4 = num,
						Flag1 = num,
						Flag2 = num,
						Flag3 = num,
						ProducedDateRule = text,
						FreshnessDateFrom = text,
						ValidDateFrom = text,
						PackageDateFrom = text,
						SpeedCode = text2,
						Position1 = text,
						SalesCategory = text2,
						DiscountBeginTime = text3,
						DiscountEndTime = text3,
						DiscountPrice = num,
						DiscountFlag = text
					};
					list.Add(item);
					num2++;
				}
				string text4 = Application.StartupPath + "\\PLU_0.txt";
				if (File.Exists(text4))
				{
					File.Delete(text4);
				}
				DataTable dataTable = new DataTable();
				dataTable = AniHelper.ListToDataTable(list);
				AniHelper.DataTabletoFile(dataTable, text4);
				if (scales.Count == 0 && txtIP.Text != string.Empty)
				{
					AclasSDKHelper.UploadDataToMachine("0000", txtIP.Text, text4);
				}
				else if (scales.Count > 0)
				{
					foreach (BarcodeScale scale in scales)
					{
						AclasSDKHelper.UploadDataToMachine("0000", scale.IPAddress, text4);
					}
				}
				Messages.InformationMessage("Successfully uploaded.");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			btnClearAll.PerformClick();
		}

		private void LabelScale_FormClosed(object sender, FormClosedEventArgs e)
		{
			AclasSDKHelper.AclasSDK_Finalize();
		}

		private void btnClearAll_Click(object sender, EventArgs e)
		{
			btnClear.PerformClick();
			btnRemoveAll.PerformClick();
			exProducts = new List<ExProductEntity>();
			scales = new List<BarcodeScale>();
			LoadDgScaleListMethod();
			dgLabelList.Visible = false;
			DgvLoadMethod();
		}

		private void cmbStarting_SelectedIndexChanged(object sender, EventArgs e)
		{
			exProducts = new List<ExProductEntity>();
			entProducts = new List<ProductEntity>();
			entProducts = (from x in db.Products()
				where x.Barcode.StartsWith(cmbStarting.Text.Trim())
				select x).ToList();
			List<ExProductEntity> list = new List<ExProductEntity>();
			list = AniHelper.CopyListData<ExProductEntity>(entProducts.Cast<object>().ToList());
			exProducts.AddRange(list);
			DgvLoadMethod();
		}

		private void btnAddLabel_Click(object sender, EventArgs e)
		{
			if (barcodeScale != null)
			{
				scales.Add(barcodeScale);
				btnClear.PerformClick();
			}
			if (!dgLabelList.Visible)
			{
				dgLabelList.Visible = true;
			}
			LoadDgScaleListMethod();
		}

		private void LoadDgScaleListMethod()
		{
			dgLabelList.AutoGenerateColumns = false;
			bdsLabelList.DataSource = null;
			dgLabelList.DataSource = null;
			bdsLabelList.DataSource = scales;
			dgLabelList.DataSource = bdsLabelList;
		}

		private void cmbPriceCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (ExProductEntity exProduct in exProducts)
			{
				ProductPriceCategory productPriceCategory = db.ProductPriceCategories(new { exProduct.Barcode }).FirstOrDefault();
				if (productPriceCategory != null)
				{
					exProduct.SellingPrice = productPriceCategory.SPrice;
				}
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			lblHeader = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			cmbProGroup = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			cbAll = new System.Windows.Forms.CheckBox();
			dgSelectedProducts = new System.Windows.Forms.DataGridView();
			col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dgProducts = new System.Windows.Forms.DataGridView();
			col_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Selection = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			bdsProduct = new System.Windows.Forms.BindingSource(components);
			btnAdd = new System.Windows.Forms.Button();
			btnRemove = new System.Windows.Forms.Button();
			bdsSelectProduct = new System.Windows.Forms.BindingSource(components);
			label3 = new System.Windows.Forms.Label();
			txtScaleName = new AshUsercontrol.AshTextBoxNormal();
			label4 = new System.Windows.Forms.Label();
			btnLoadScaleProduct = new System.Windows.Forms.Button();
			btnLoadFromPurchase = new System.Windows.Forms.Button();
			txtIP = new AshUsercontrol.AshTextBoxNormal();
			btnSave = new System.Windows.Forms.Button();
			btnRemoveAll = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnProductAllocationToScale = new System.Windows.Forms.Button();
			btnUpload = new System.Windows.Forms.Button();
			btnClearAll = new System.Windows.Forms.Button();
			label5 = new System.Windows.Forms.Label();
			cmbStarting = new AshUsercontrol.AshComboBox();
			btnAddLabel = new System.Windows.Forms.Button();
			dgLabelList = new System.Windows.Forms.DataGridView();
			col_LabelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_IPAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			bdsLabelList = new System.Windows.Forms.BindingSource(components);
			label6 = new System.Windows.Forms.Label();
			cmbPriceCategory = new AshUsercontrol.AshComboBox();
			((System.ComponentModel.ISupportInitialize)dgSelectedProducts).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgProducts).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsSelectProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgLabelList).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsLabelList).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(1005, 0);
			btnEnd.Margin = new System.Windows.Forms.Padding(4);
			btnEnd.Size = new System.Drawing.Size(47, 43);
			btnEnd.TabIndex = 20;
			lblHeader.AutoSize = true;
			lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			lblHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblHeader.Location = new System.Drawing.Point(-1, 0);
			lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new System.Drawing.Size(307, 25);
			lblHeader.TabIndex = 21;
			lblHeader.Text = "Barcode Scale Management";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(18, 35);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(113, 16);
			label1.TabIndex = 0;
			label1.Text = "Product Group :";
			cmbProGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbProGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProGroup.FormattingEnabled = true;
			cmbProGroup.LeaveColor = System.Drawing.Color.White;
			cmbProGroup.Location = new System.Drawing.Point(139, 31);
			cmbProGroup.Margin = new System.Windows.Forms.Padding(4);
			cmbProGroup.Name = "cmbProGroup";
			cmbProGroup.Size = new System.Drawing.Size(179, 24);
			cmbProGroup.TabIndex = 1;
			cmbProGroup.SelectedIndexChanged += new System.EventHandler(cmbProGroup_SelectedIndexChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(17, 124);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(106, 16);
			label2.TabIndex = 2;
			label2.Text = "Select Product";
			cbAll.AutoSize = true;
			cbAll.Location = new System.Drawing.Point(264, 122);
			cbAll.Margin = new System.Windows.Forms.Padding(4);
			cbAll.Name = "cbAll";
			cbAll.Size = new System.Drawing.Size(42, 20);
			cbAll.TabIndex = 4;
			cbAll.Text = "All";
			cbAll.UseVisualStyleBackColor = true;
			cbAll.CheckedChanged += new System.EventHandler(cbAll_CheckedChanged);
			dgSelectedProducts.AllowUserToAddRows = false;
			dgSelectedProducts.AllowUserToDeleteRows = false;
			dgSelectedProducts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			dgSelectedProducts.BackgroundColor = System.Drawing.Color.White;
			dgSelectedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgSelectedProducts.Columns.AddRange(col_Name, col_ProBarcode, col_Rate, col_Quantity);
			dgSelectedProducts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			dgSelectedProducts.Location = new System.Drawing.Point(648, 59);
			dgSelectedProducts.Margin = new System.Windows.Forms.Padding(4);
			dgSelectedProducts.Name = "dgSelectedProducts";
			dgSelectedProducts.RowHeadersVisible = false;
			dgSelectedProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgSelectedProducts.Size = new System.Drawing.Size(400, 585);
			dgSelectedProducts.TabIndex = 19;
			dgSelectedProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgSelectedProducts_DataError);
			col_Name.DataPropertyName = "ProductName";
			col_Name.HeaderText = "Name";
			col_Name.Name = "col_Name";
			col_Name.ReadOnly = true;
			col_Name.Width = 150;
			col_ProBarcode.DataPropertyName = "Barcode";
			col_ProBarcode.HeaderText = "Barcode";
			col_ProBarcode.Name = "col_ProBarcode";
			col_ProBarcode.ReadOnly = true;
			col_Rate.DataPropertyName = "SellingPrice";
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle;
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_Rate.Width = 80;
			col_Quantity.DataPropertyName = "UnitId";
			col_Quantity.HeaderText = "Unit";
			col_Quantity.Name = "col_Quantity";
			col_Quantity.Width = 70;
			dgProducts.AllowUserToAddRows = false;
			dgProducts.AllowUserToDeleteRows = false;
			dgProducts.BackgroundColor = System.Drawing.Color.White;
			dgProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgProducts.Columns.AddRange(col_Select, col_Selection, col_Barcode);
			dgProducts.Location = new System.Drawing.Point(9, 150);
			dgProducts.Margin = new System.Windows.Forms.Padding(4);
			dgProducts.Name = "dgProducts";
			dgProducts.RowHeadersVisible = false;
			dgProducts.Size = new System.Drawing.Size(324, 487);
			dgProducts.TabIndex = 3;
			dgProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgProducts_DataError);
			col_Select.DataPropertyName = "IsSelected";
			col_Select.HeaderText = "Select";
			col_Select.Name = "col_Select";
			col_Select.Width = 50;
			col_Selection.DataPropertyName = "ProductName";
			col_Selection.HeaderText = "Product Name";
			col_Selection.Name = "col_Selection";
			col_Selection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			col_Selection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			col_Selection.Width = 175;
			col_Barcode.DataPropertyName = "Barcode";
			col_Barcode.HeaderText = "Barcode";
			col_Barcode.Name = "col_Barcode";
			col_Barcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			col_Barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			col_Barcode.Width = 120;
			btnAdd.Location = new System.Drawing.Point(568, 378);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new System.Drawing.Size(75, 23);
			btnAdd.TabIndex = 12;
			btnAdd.Text = ">>";
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += new System.EventHandler(btnAdd_Click);
			btnRemove.Location = new System.Drawing.Point(568, 408);
			btnRemove.Name = "btnRemove";
			btnRemove.Size = new System.Drawing.Size(75, 23);
			btnRemove.TabIndex = 13;
			btnRemove.Text = "<<";
			btnRemove.UseVisualStyleBackColor = true;
			btnRemove.Click += new System.EventHandler(btnRemove_Click);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(339, 59);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(55, 16);
			label3.TabIndex = 5;
			label3.Text = "Scale :";
			txtScaleName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtScaleName.Data = 0;
			txtScaleName.EnterColor = System.Drawing.Color.LightPink;
			txtScaleName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtScaleName.Format = null;
			txtScaleName.isAllowNegative = false;
			txtScaleName.isAllowSpecialChar = false;
			txtScaleName.isNumberOnly = false;
			txtScaleName.isNumeric = false;
			txtScaleName.LeaveColor = System.Drawing.Color.White;
			txtScaleName.Location = new System.Drawing.Point(395, 59);
			txtScaleName.Margin = new System.Windows.Forms.Padding(4);
			txtScaleName.Name = "txtScaleName";
			txtScaleName.Size = new System.Drawing.Size(248, 16);
			txtScaleName.TabIndex = 6;
			txtScaleName.Value = new decimal(new int[4]);
			txtScaleName.WaterMark = null;
			txtScaleName.Validated += new System.EventHandler(txtScaleName_Validated);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(362, 92);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(32, 16);
			label4.TabIndex = 7;
			label4.Text = "IP :";
			btnLoadScaleProduct.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoadScaleProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoadScaleProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoadScaleProduct.ForeColor = System.Drawing.Color.White;
			btnLoadScaleProduct.Location = new System.Drawing.Point(379, 520);
			btnLoadScaleProduct.Name = "btnLoadScaleProduct";
			btnLoadScaleProduct.Size = new System.Drawing.Size(230, 30);
			btnLoadScaleProduct.TabIndex = 16;
			btnLoadScaleProduct.Text = "Load Scale Products";
			btnLoadScaleProduct.UseVisualStyleBackColor = false;
			btnLoadScaleProduct.Click += new System.EventHandler(btnLoadScaleProduct_Click);
			btnLoadFromPurchase.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoadFromPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoadFromPurchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoadFromPurchase.ForeColor = System.Drawing.Color.White;
			btnLoadFromPurchase.Location = new System.Drawing.Point(379, 556);
			btnLoadFromPurchase.Name = "btnLoadFromPurchase";
			btnLoadFromPurchase.Size = new System.Drawing.Size(230, 30);
			btnLoadFromPurchase.TabIndex = 17;
			btnLoadFromPurchase.Text = "Load From Purchase";
			btnLoadFromPurchase.UseVisualStyleBackColor = false;
			btnLoadFromPurchase.Click += new System.EventHandler(btnLoadFromPurchase_Click);
			txtIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtIP.Data = 0;
			txtIP.EnterColor = System.Drawing.Color.LightPink;
			txtIP.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtIP.Format = null;
			txtIP.isAllowNegative = false;
			txtIP.isAllowSpecialChar = false;
			txtIP.isNumberOnly = false;
			txtIP.isNumeric = false;
			txtIP.LeaveColor = System.Drawing.Color.White;
			txtIP.Location = new System.Drawing.Point(395, 92);
			txtIP.Margin = new System.Windows.Forms.Padding(4);
			txtIP.Name = "txtIP";
			txtIP.Size = new System.Drawing.Size(248, 16);
			txtIP.TabIndex = 8;
			txtIP.Value = new decimal(new int[4]);
			txtIP.WaterMark = null;
			txtIP.Validated += new System.EventHandler(txtIP_Validated);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(353, 115);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(66, 30);
			btnSave.TabIndex = 9;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnRemoveAll.Location = new System.Drawing.Point(568, 437);
			btnRemoveAll.Name = "btnRemoveAll";
			btnRemoveAll.Size = new System.Drawing.Size(75, 23);
			btnRemoveAll.TabIndex = 14;
			btnRemoveAll.Text = "<<<<";
			btnRemoveAll.UseVisualStyleBackColor = true;
			btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(424, 115);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(66, 30);
			btnDelete.TabIndex = 10;
			btnDelete.Text = "Delete";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(495, 115);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(66, 30);
			btnClear.TabIndex = 11;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnProductAllocationToScale.BackColor = System.Drawing.Color.DodgerBlue;
			btnProductAllocationToScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnProductAllocationToScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnProductAllocationToScale.ForeColor = System.Drawing.Color.White;
			btnProductAllocationToScale.Location = new System.Drawing.Point(379, 484);
			btnProductAllocationToScale.Name = "btnProductAllocationToScale";
			btnProductAllocationToScale.Size = new System.Drawing.Size(230, 30);
			btnProductAllocationToScale.TabIndex = 15;
			btnProductAllocationToScale.Text = "Allocate Product to Scale";
			btnProductAllocationToScale.UseVisualStyleBackColor = false;
			btnProductAllocationToScale.Click += new System.EventHandler(btnProductAllocationToScale_Click);
			btnUpload.BackColor = System.Drawing.Color.DodgerBlue;
			btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnUpload.ForeColor = System.Drawing.Color.White;
			btnUpload.Location = new System.Drawing.Point(397, 605);
			btnUpload.Name = "btnUpload";
			btnUpload.Size = new System.Drawing.Size(92, 30);
			btnUpload.TabIndex = 18;
			btnUpload.Text = "Upload ";
			btnUpload.UseVisualStyleBackColor = false;
			btnUpload.Click += new System.EventHandler(btnUpload_Click);
			btnClearAll.BackColor = System.Drawing.Color.DodgerBlue;
			btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClearAll.ForeColor = System.Drawing.Color.White;
			btnClearAll.Location = new System.Drawing.Point(495, 605);
			btnClearAll.Name = "btnClearAll";
			btnClearAll.Size = new System.Drawing.Size(92, 30);
			btnClearAll.TabIndex = 18;
			btnClearAll.Text = "Clear All";
			btnClearAll.UseVisualStyleBackColor = false;
			btnClearAll.Click += new System.EventHandler(btnClearAll_Click);
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(1, 63);
			label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(130, 16);
			label5.TabIndex = 0;
			label5.Text = "Barcode Starting :";
			cmbStarting.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbStarting.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbStarting.EnterColor = System.Drawing.Color.LightPink;
			cmbStarting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbStarting.FormattingEnabled = true;
			cmbStarting.LeaveColor = System.Drawing.Color.White;
			cmbStarting.Location = new System.Drawing.Point(139, 59);
			cmbStarting.Margin = new System.Windows.Forms.Padding(4);
			cmbStarting.Name = "cmbStarting";
			cmbStarting.Size = new System.Drawing.Size(179, 24);
			cmbStarting.TabIndex = 1;
			cmbStarting.SelectedIndexChanged += new System.EventHandler(cmbStarting_SelectedIndexChanged);
			btnAddLabel.BackColor = System.Drawing.Color.DodgerBlue;
			btnAddLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnAddLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnAddLabel.ForeColor = System.Drawing.Color.White;
			btnAddLabel.Location = new System.Drawing.Point(566, 115);
			btnAddLabel.Name = "btnAddLabel";
			btnAddLabel.Size = new System.Drawing.Size(66, 30);
			btnAddLabel.TabIndex = 11;
			btnAddLabel.Text = "Add";
			btnAddLabel.UseVisualStyleBackColor = false;
			btnAddLabel.Click += new System.EventHandler(btnAddLabel_Click);
			dgLabelList.AllowUserToAddRows = false;
			dgLabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgLabelList.Columns.AddRange(col_LabelName, col_IPAddress);
			dgLabelList.Location = new System.Drawing.Point(353, 168);
			dgLabelList.Name = "dgLabelList";
			dgLabelList.ReadOnly = true;
			dgLabelList.RowHeadersVisible = false;
			dgLabelList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgLabelList.Size = new System.Drawing.Size(279, 196);
			dgLabelList.TabIndex = 22;
			dgLabelList.Visible = false;
			col_LabelName.DataPropertyName = "Name";
			col_LabelName.HeaderText = "Label Name";
			col_LabelName.Name = "col_LabelName";
			col_LabelName.ReadOnly = true;
			col_LabelName.Width = 150;
			col_IPAddress.DataPropertyName = "IPAddress";
			col_IPAddress.HeaderText = "IP";
			col_IPAddress.Name = "col_IPAddress";
			col_IPAddress.ReadOnly = true;
			col_IPAddress.Width = 150;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(15, 92);
			label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(116, 16);
			label6.TabIndex = 0;
			label6.Text = "Price Category :";
			cmbPriceCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPriceCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPriceCategory.EnterColor = System.Drawing.Color.LightPink;
			cmbPriceCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPriceCategory.FormattingEnabled = true;
			cmbPriceCategory.LeaveColor = System.Drawing.Color.White;
			cmbPriceCategory.Location = new System.Drawing.Point(139, 88);
			cmbPriceCategory.Margin = new System.Windows.Forms.Padding(4);
			cmbPriceCategory.Name = "cmbPriceCategory";
			cmbPriceCategory.Size = new System.Drawing.Size(179, 24);
			cmbPriceCategory.TabIndex = 1;
			cmbPriceCategory.SelectedIndexChanged += new System.EventHandler(cmbPriceCategory_SelectedIndexChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1052, 647);
			base.Controls.Add(dgLabelList);
			base.Controls.Add(btnClearAll);
			base.Controls.Add(btnUpload);
			base.Controls.Add(btnLoadFromPurchase);
			base.Controls.Add(btnAddLabel);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(btnProductAllocationToScale);
			base.Controls.Add(btnLoadScaleProduct);
			base.Controls.Add(label4);
			base.Controls.Add(txtIP);
			base.Controls.Add(txtScaleName);
			base.Controls.Add(label3);
			base.Controls.Add(btnRemoveAll);
			base.Controls.Add(btnRemove);
			base.Controls.Add(btnAdd);
			base.Controls.Add(dgProducts);
			base.Controls.Add(dgSelectedProducts);
			base.Controls.Add(cbAll);
			base.Controls.Add(label2);
			base.Controls.Add(cmbPriceCategory);
			base.Controls.Add(label6);
			base.Controls.Add(cmbStarting);
			base.Controls.Add(label5);
			base.Controls.Add(cmbProGroup);
			base.Controls.Add(label1);
			base.Controls.Add(lblHeader);
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "LabelScale";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(LabelScale_FormClosed);
			base.Load += new System.EventHandler(LabelScale_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHeader, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cmbProGroup, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(cmbStarting, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(cmbPriceCategory, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cbAll, 0);
			base.Controls.SetChildIndex(dgSelectedProducts, 0);
			base.Controls.SetChildIndex(dgProducts, 0);
			base.Controls.SetChildIndex(btnAdd, 0);
			base.Controls.SetChildIndex(btnRemove, 0);
			base.Controls.SetChildIndex(btnRemoveAll, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtScaleName, 0);
			base.Controls.SetChildIndex(txtIP, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(btnLoadScaleProduct, 0);
			base.Controls.SetChildIndex(btnProductAllocationToScale, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnAddLabel, 0);
			base.Controls.SetChildIndex(btnLoadFromPurchase, 0);
			base.Controls.SetChildIndex(btnUpload, 0);
			base.Controls.SetChildIndex(btnClearAll, 0);
			base.Controls.SetChildIndex(dgLabelList, 0);
			((System.ComponentModel.ISupportInitialize)dgSelectedProducts).EndInit();
			((System.ComponentModel.ISupportInitialize)dgProducts).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsSelectProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)dgLabelList).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsLabelList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
