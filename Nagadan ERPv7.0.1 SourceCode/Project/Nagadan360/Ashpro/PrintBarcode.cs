using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class PrintBarcode : BaseForm
	{
		private Company entCompany = new Company();

		private List<BarcodeData> barcodeDatas = new List<BarcodeData>();

		private ProductBarcode productBarcode;

		private BarcodeData barcodeData;

		private List<ProductEntity> entProducts = new List<ProductEntity>();

		private bool isEdit = false;

		private DBContext db = new DBContext();

		private ProductEntity product;

		private bool isSearchByBarcode = false;

		private IContainer components = null;

		private GroupBox grpSrch;

		private AshTextBoxNormal txtSrch;

		private Label label14;

		private AshTextBoxNormal txtProductCode;

		private Label label1;

		private Button btnAdd;

		private Button btnRmv;

		private Button btnClr;

		private Button btnClear;

		private Button btnPrint;

		private Button btnPreview;

		private Label label2;

		private AshTextBoxNormal txtProductName;

		private Label label3;

		private AshTextBoxNormal txtQuantity;

		private Button button6;

		private Label label4;

		private Label label5;

		private AshTextBoxNormal txtStarting;

		private DataGridView dgProduct;

		private BindingSource bdsProduct;

		private AshNumericLabel lblTotalQty;

		private BindingSource bdsDetails;

		private Button btnOppStock;

		private Label label6;

		private Label label7;

		private Label label8;

		private AshDateTimePicker dtmManufacturing;

		private AshDateTimePicker dtmExpairy;

		private DataGridView dgBarcode;

		private Label label9;
        private DataGridViewTextBoxColumn col_Barcode;
        private DataGridViewTextBoxColumn col_Product;
        private DataGridViewTextBoxColumn col_ProductCode;
        private DataGridViewTextBoxColumn col_ProductName;
        private DataGridViewTextBoxColumn col_Quantity;
        private AshComboBox cmbDesign;

		public PrintBarcode()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void LoadBarcodeData(int Qty)
		{
			if (!isEdit)
			{
				barcodeData = new BarcodeData();
			}
			ProductBarcodeEntity productBarcodeEntity = new ProductBarcodeEntity();
			productBarcodeEntity = db.ProductBarcodes(new
			{
				Barcode = txtProductCode.Text
			}).FirstOrDefault();
			if (productBarcodeEntity != null)
			{
				barcodeData.Sl_No = barcodeDatas.Count + 1;
				barcodeData.Product_Code = product.ProductCode.ToString();
				barcodeData.Product_Name = txtProductName.Text;
				barcodeData.Unit = db.Units(new
				{
					UnitId = productBarcodeEntity.FK_UnitId
				}).FirstOrDefault().UnitName;
				barcodeData.Brand = db.Brands(new { product.BrandId }).FirstOrDefault().BrandName;
				barcodeData.Group_Name = db.ProductGroups(new { product.PGroupId }).FirstOrDefault().PGroupName;
				barcodeData.Specification = product.Specification;
				barcodeData.Description = product.description;
				barcodeData.Purchase_Rate = productBarcodeEntity.PurchasePrice;
				barcodeData.Sales_Rate = productBarcodeEntity.SellingPrice;
				barcodeData.MRP = productBarcodeEntity.MRP;
				barcodeData.Barcode = txtProductCode.Text;
				barcodeData.Company_Name = entCompany.InstituteName;
				barcodeData.Address = entCompany.Address;
				barcodeData.Place = entCompany.Place;
				barcodeData.Country = entCompany.Country;
				barcodeData.Pincode = entCompany.Pincod;
				barcodeData.PhoneNumber = entCompany.PhoneNo;
				barcodeData.Mobile = entCompany.MobNo;
				barcodeData.Website = entCompany.website;
				barcodeData.EmailId = entCompany.EmailId;
				barcodeData.Quantity = Qty;
				barcodeData.ExpairyDate = dtmExpairy.Value.ToString(CommonSettings.strDateFormat);
				barcodeData.ManufacturingDate = dtmManufacturing.Value.ToString(CommonSettings.strDateFormat);
				barcodeData.PriceCode = AniHelper.GetPriceCode(productBarcodeEntity.PurchasePrice.ToString(CommonSettings.strCurencyFormat));
				barcodeData.Rate_In_Arabic = productBarcodeEntity.SellingPrice.ToString(CommonSettings.strCurencyFormat).ToArabic();
				barcodeData.ItemCalory = product.FieldTwo.ToInt32();
				if (!isEdit)
				{
					barcodeDatas.Add(barcodeData);
				}
			}
		}

		private void dgBarcodeLoada()
		{
			dgBarcode.AutoGenerateColumns = false;
			dgBarcode.DataSource = null;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entProducts;
			dgBarcode.DataSource = bdsDetails;
		}

		private async Task LoadData()
		{
			Task task = new Task(LoadProductFromDB);
			task.Start();
			await task;
			dgBarcodeLoada();
		}

		private void LoadProductFromDB()
		{
			entProducts = new List<ProductEntity>();
			entProducts = db.Products();
		}

		private void ClearMethod()
		{
			txtProductCode.Text = string.Empty;
			txtProductName.Text = string.Empty;
			txtQuantity.Data = 0;
			isEdit = false;
			txtSrch.Text = string.Empty;
			txtSrch.Focus();
			txtSrch.SelectAll();
		}

		private async void PrintBarcode_Load(object sender, EventArgs e)
		{
			await LoadData();
			productBarcode = new ProductBarcode();
			barcodeDatas = new List<BarcodeData>();
			entCompany = db.Companies().FirstOrDefault();
			AniHelper.FillBarcode(cmbDesign);
			dgProduct.AutoGenerateColumns = false;
			dgProduct.DataSource = null;
			bdsProduct.DataSource = null;
			bdsProduct.DataSource = barcodeDatas;
			dgProduct.DataSource = bdsProduct;
			base.ActiveControl = txtSrch;
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (product != null && txtQuantity.Data != 0)
				{
					await Task.Run(delegate
					{
						LoadBarcodeData(txtQuantity.Data);
					});
					dgProduct.AutoGenerateColumns = false;
					bdsProduct.DataSource = null;
					bdsProduct.DataSource = barcodeDatas;
					dgProduct.DataSource = bdsProduct;
					lblTotalQty.Value = barcodeDatas.Sum((BarcodeData x) => x.Quantity);
					ClearMethod();
					if (isSearchByBarcode)
					{
						await LoadData();
					}
				}
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgProduct_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && dgProduct.SelectedRows.Count > 0)
			{
				BarcodeData item = (BarcodeData)dgProduct.CurrentRow.DataBoundItem;
				barcodeDatas.Remove(item);
				dgProduct.Refresh();
			}
		}

		private void dgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				barcodeData = new BarcodeData();
				barcodeData = (BarcodeData)dgProduct.CurrentRow.DataBoundItem;
				txtProductCode.Text = barcodeData.Barcode;
				txtProductName.Text = barcodeData.Product_Name;
				txtQuantity.Text = barcodeData.Quantity.ToString();
				isEdit = true;
				txtQuantity.Focus();
				txtQuantity.SelectAll();
			}
		}

		private void btnClrFromRpt_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void btnRmv_Click(object sender, EventArgs e)
		{
			if (dgProduct.SelectedRows.Count > 0)
			{
				BarcodeData item = (BarcodeData)dgProduct.CurrentRow.DataBoundItem;
				barcodeDatas.Remove(item);
				dgProduct.Refresh();
			}
			ClearMethod();
		}

		private async void btnClear_Click(object sender, EventArgs e)
		{
			await LoadData();
			ClearMethod();
			lblTotalQty.Value = 0m;
			dgProduct.DataSource = null;
			barcodeDatas.Clear();
		}

		private void btnPreview_Click(object sender, EventArgs e)
		{
			productBarcode.printBarcodePreview(barcodeDatas, Convert.ToInt32(txtStarting.Text), cmbDesign.SelectedValue.ToInt32());
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			productBarcode.printBarcode(barcodeDatas, Convert.ToInt32(txtStarting.Text), cmbDesign.SelectedValue.ToInt32());
		}

		private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAdd_Click(null, null);
			}
		}

		private void btnOppStock_Click(object sender, EventArgs e)
		{
			barcodeDatas = new List<BarcodeData>();
			foreach (ProductEntity product in entProducts.Where((ProductEntity x) => x.OpeningStock > 0m).ToList())
			{
				barcodeData = new BarcodeData();
				barcodeData.Sl_No = barcodeDatas.Count + 1;
				barcodeData.Product_Code = product.ProductCode.ToString();
				barcodeData.Product_Name = product.ProductName;
				barcodeData.Unit = db.Units().Find((UnitEntity x) => x.UnitId == product.UnitId).UnitName;
				barcodeData.Brand = db.Brands().Find((BrandEntity x) => x.BrandId == product.BrandId).BrandName;
				barcodeData.Group_Name = db.ProductGroups().Find((ProductGroupEntity x) => x.PGroupId == product.PGroupId).PGroupName;
				barcodeData.Specification = product.Specification;
				barcodeData.Description = product.description;
				barcodeData.Purchase_Rate = product.PurchasePrice;
				barcodeData.Sales_Rate = product.SellingPrice;
				barcodeData.MRP = product.MRP;
				barcodeData.Barcode = product.Barcode;
				barcodeData.Company_Name = entCompany.InstituteName;
				barcodeData.Address = entCompany.Address;
				barcodeData.Place = entCompany.Place;
				barcodeData.Country = entCompany.Country;
				barcodeData.Pincode = entCompany.Pincod;
				barcodeData.PhoneNumber = entCompany.PhoneNo;
				barcodeData.Mobile = entCompany.MobNo;
				barcodeData.Website = entCompany.website;
				barcodeData.EmailId = entCompany.EmailId;
				barcodeData.Quantity = product.OpeningStock.ToInt32();
				barcodeDatas.Add(barcodeData);
			}
			dgProduct.AutoGenerateColumns = false;
			bdsProduct.DataSource = null;
			bdsProduct.DataSource = barcodeDatas;
			dgProduct.DataSource = bdsProduct;
			lblTotalQty.Value = barcodeDatas.Sum((BarcodeData x) => x.Quantity);
		}

		private void dgBarcode_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dgBarcode.CurrentRow.Index >= 0)
			{
				product = (ProductEntity)dgBarcode.CurrentRow.DataBoundItem;
				txtProductCode.Text = product.Barcode;
				txtProductName.Text = product.ProductName;
				txtQuantity.Focus();
				txtQuantity.SelectAll();
			}
		}

		private void txtSrch_Validated(object sender, EventArgs e)
		{
			if (txtSrch.Text == string.Empty)
			{
				return;
			}
			isSearchByBarcode = true;
			entProducts = new List<ProductEntity>();
			if (txtSrch.Text != string.Empty)
			{
				product = AshproCommon.GetObjectWithParameter_SP<ProductEntity>("spGetProductWithBarcode", new
				{
					Barcode = txtSrch.Text
				});
				if (product.ProductName != null)
				{
					product.Barcode = txtSrch.Text;
					entProducts.Add(product);
					dgBarcodeLoada();
					txtProductCode.Text = product.Barcode;
					txtProductName.Text = product.ProductName;
					txtQuantity.Focus();
					txtQuantity.SelectAll();
				}
				else
				{
					txtSrch.Focus();
					txtSrch.SelectAll();
				}
			}
			else
			{
				entProducts = db.Products();
				dgBarcodeLoada();
				txtProductCode.Text = product.Barcode;
				txtProductName.Text = product.ProductName;
				txtQuantity.Focus();
				txtQuantity.SelectAll();
			}
		}

		private void dgBarcode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				dgBarcode_CellContentClick(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintBarcode));
            this.grpSrch = new System.Windows.Forms.GroupBox();
            this.dgBarcode = new System.Windows.Forms.DataGridView();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSrch = new AshUsercontrol.AshTextBoxNormal();
            this.label14 = new System.Windows.Forms.Label();
            this.txtProductCode = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRmv = new System.Windows.Forms.Button();
            this.btnClr = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductName = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQuantity = new AshUsercontrol.AshTextBoxNormal();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStarting = new AshUsercontrol.AshTextBoxNormal();
            this.dgProduct = new System.Windows.Forms.DataGridView();
            this.col_ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalQty = new AshUsercontrol.AshNumericLabel();
            this.btnOppStock = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtmManufacturing = new AshUsercontrol.AshDateTimePicker();
            this.dtmExpairy = new AshUsercontrol.AshDateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDesign = new AshUsercontrol.AshComboBox();
            this.bdsProduct = new System.Windows.Forms.BindingSource(this.components);
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.grpSrch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // grpSrch
            // 
            resources.ApplyResources(this.grpSrch, "grpSrch");
            this.grpSrch.BackColor = System.Drawing.Color.White;
            this.grpSrch.Controls.Add(this.dgBarcode);
            this.grpSrch.Controls.Add(this.txtSrch);
            this.grpSrch.Controls.Add(this.label14);
            this.grpSrch.Name = "grpSrch";
            this.grpSrch.TabStop = false;
            // 
            // dgBarcode
            // 
            resources.ApplyResources(this.dgBarcode, "dgBarcode");
            this.dgBarcode.AllowUserToAddRows = false;
            this.dgBarcode.AllowUserToDeleteRows = false;
            this.dgBarcode.BackgroundColor = System.Drawing.Color.White;
            this.dgBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBarcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Barcode,
            this.col_Product});
            this.dgBarcode.Name = "dgBarcode";
            this.dgBarcode.ReadOnly = true;
            this.dgBarcode.RowHeadersVisible = false;
            this.dgBarcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBarcode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBarcode_CellContentClick);
            this.dgBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBarcode_KeyDown);
            // 
            // col_Barcode
            // 
            this.col_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_Barcode, "col_Barcode");
            this.col_Barcode.Name = "col_Barcode";
            this.col_Barcode.ReadOnly = true;
            // 
            // col_Product
            // 
            this.col_Product.DataPropertyName = "ProductName";
            resources.ApplyResources(this.col_Product, "col_Product");
            this.col_Product.Name = "col_Product";
            this.col_Product.ReadOnly = true;
            // 
            // txtSrch
            // 
            resources.ApplyResources(this.txtSrch, "txtSrch");
            this.txtSrch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSrch.Data = 0;
            this.txtSrch.EnterColor = System.Drawing.Color.LightPink;
            this.txtSrch.Format = null;
            this.txtSrch.isAllowNegative = false;
            this.txtSrch.isAllowSpecialChar = false;
            this.txtSrch.isNumberOnly = false;
            this.txtSrch.isNumeric = false;
            this.txtSrch.LeaveColor = System.Drawing.Color.White;
            this.txtSrch.Name = "txtSrch";
            this.txtSrch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSrch.Validated += new System.EventHandler(this.txtSrch_Validated);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtProductCode
            // 
            resources.ApplyResources(this.txtProductCode, "txtProductCode");
            this.txtProductCode.BackColor = System.Drawing.Color.White;
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductCode.Data = 10001;
            this.txtProductCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductCode.Format = null;
            this.txtProductCode.isAllowNegative = false;
            this.txtProductCode.isAllowSpecialChar = false;
            this.txtProductCode.isNumberOnly = false;
            this.txtProductCode.isNumeric = false;
            this.txtProductCode.LeaveColor = System.Drawing.Color.White;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.ReadOnly = true;
            this.txtProductCode.Value = new decimal(new int[] {
            10001,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRmv
            // 
            resources.ApplyResources(this.btnRmv, "btnRmv");
            this.btnRmv.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRmv.ForeColor = System.Drawing.Color.White;
            this.btnRmv.Name = "btnRmv";
            this.btnRmv.UseVisualStyleBackColor = false;
            this.btnRmv.Click += new System.EventHandler(this.btnRmv_Click);
            // 
            // btnClr
            // 
            resources.ApplyResources(this.btnClr, "btnClr");
            this.btnClr.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClr.ForeColor = System.Drawing.Color.White;
            this.btnClr.Name = "btnClr";
            this.btnClr.UseVisualStyleBackColor = false;
            this.btnClr.Click += new System.EventHandler(this.btnClrFromRpt_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPreview
            // 
            resources.ApplyResources(this.btnPreview, "btnPreview");
            this.btnPreview.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtProductName
            // 
            resources.ApplyResources(this.txtProductName, "txtProductName");
            this.txtProductName.BackColor = System.Drawing.Color.White;
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductName.Data = 0;
            this.txtProductName.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductName.Format = null;
            this.txtProductName.isAllowNegative = false;
            this.txtProductName.isAllowSpecialChar = false;
            this.txtProductName.isNumberOnly = false;
            this.txtProductName.isNumeric = false;
            this.txtProductName.LeaveColor = System.Drawing.Color.White;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtQuantity
            // 
            resources.ApplyResources(this.txtQuantity, "txtQuantity");
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantity.Data = 0;
            this.txtQuantity.EnterColor = System.Drawing.Color.LightPink;
            this.txtQuantity.Format = null;
            this.txtQuantity.isAllowNegative = false;
            this.txtQuantity.isAllowSpecialChar = false;
            this.txtQuantity.isNumberOnly = true;
            this.txtQuantity.isNumeric = false;
            this.txtQuantity.LeaveColor = System.Drawing.Color.White;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtStarting
            // 
            resources.ApplyResources(this.txtStarting, "txtStarting");
            this.txtStarting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStarting.Data = 0;
            this.txtStarting.EnterColor = System.Drawing.Color.LightPink;
            this.txtStarting.Format = null;
            this.txtStarting.isAllowNegative = false;
            this.txtStarting.isAllowSpecialChar = false;
            this.txtStarting.isNumberOnly = true;
            this.txtStarting.isNumeric = false;
            this.txtStarting.LeaveColor = System.Drawing.Color.White;
            this.txtStarting.Name = "txtStarting";
            this.txtStarting.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // dgProduct
            // 
            resources.ApplyResources(this.dgProduct, "dgProduct");
            this.dgProduct.AllowUserToAddRows = false;
            this.dgProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ProductCode,
            this.col_ProductName,
            this.col_Quantity});
            this.dgProduct.Name = "dgProduct";
            this.dgProduct.ReadOnly = true;
            this.dgProduct.RowHeadersVisible = false;
            this.dgProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProduct_CellContentClick);
            this.dgProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgProduct_KeyDown);
            // 
            // col_ProductCode
            // 
            this.col_ProductCode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_ProductCode, "col_ProductCode");
            this.col_ProductCode.Name = "col_ProductCode";
            this.col_ProductCode.ReadOnly = true;
            // 
            // col_ProductName
            // 
            this.col_ProductName.DataPropertyName = "Product_Name";
            resources.ApplyResources(this.col_ProductName, "col_ProductName");
            this.col_ProductName.Name = "col_ProductName";
            this.col_ProductName.ReadOnly = true;
            // 
            // col_Quantity
            // 
            this.col_Quantity.DataPropertyName = "Quantity";
            resources.ApplyResources(this.col_Quantity, "col_Quantity");
            this.col_Quantity.Name = "col_Quantity";
            this.col_Quantity.ReadOnly = true;
            // 
            // lblTotalQty
            // 
            resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
            this.lblTotalQty.Format = null;
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnOppStock
            // 
            resources.ApplyResources(this.btnOppStock, "btnOppStock");
            this.btnOppStock.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOppStock.ForeColor = System.Drawing.Color.White;
            this.btnOppStock.Name = "btnOppStock";
            this.btnOppStock.UseVisualStyleBackColor = false;
            this.btnOppStock.Click += new System.EventHandler(this.btnOppStock_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // dtmManufacturing
            // 
            resources.ApplyResources(this.dtmManufacturing, "dtmManufacturing");
            this.dtmManufacturing.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmManufacturing.Name = "dtmManufacturing";
            // 
            // dtmExpairy
            // 
            resources.ApplyResources(this.dtmExpairy, "dtmExpairy");
            this.dtmExpairy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmExpairy.Name = "dtmExpairy";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cmbDesign
            // 
            resources.ApplyResources(this.cmbDesign, "cmbDesign");
            this.cmbDesign.EnterColor = System.Drawing.Color.LightPink;
            this.cmbDesign.FormattingEnabled = true;
            this.cmbDesign.LeaveColor = System.Drawing.Color.White;
            this.cmbDesign.Name = "cmbDesign";
            // 
            // PrintBarcode
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbDesign);
            this.Controls.Add(this.dtmExpairy);
            this.Controls.Add(this.dtmManufacturing);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.dgProduct);
            this.Controls.Add(this.txtStarting);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnRmv);
            this.Controls.Add(this.btnClr);
            this.Controls.Add(this.btnOppStock);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpSrch);
            this.MaximizeBox = false;
            this.Name = "PrintBarcode";
            this.Load += new System.EventHandler(this.PrintBarcode_Load);
            this.Controls.SetChildIndex(this.grpSrch, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtProductCode, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtProductName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtQuantity, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnOppStock, 0);
            this.Controls.SetChildIndex(this.btnClr, 0);
            this.Controls.SetChildIndex(this.btnRmv, 0);
            this.Controls.SetChildIndex(this.btnPreview, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.button6, 0);
            this.Controls.SetChildIndex(this.txtStarting, 0);
            this.Controls.SetChildIndex(this.dgProduct, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTotalQty, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.dtmManufacturing, 0);
            this.Controls.SetChildIndex(this.dtmExpairy, 0);
            this.Controls.SetChildIndex(this.cmbDesign, 0);
            this.grpSrch.ResumeLayout(false);
            this.grpSrch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBarcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
