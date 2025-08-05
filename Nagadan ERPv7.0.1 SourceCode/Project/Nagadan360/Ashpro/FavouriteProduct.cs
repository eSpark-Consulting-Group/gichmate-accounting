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
	public class FavouriteProduct : BaseForm
	{
		private DBContext db = new DBContext();

		private List<ExProductEntity> exProducts = new List<ExProductEntity>();

		private List<ProductEntity> entSelectedProduct = new List<ProductEntity>();

		private List<ProductEntity> entOldSelectedProduct = new List<ProductEntity>();

		private IContainer components = null;

		private Label lblTitle;

		private Button btnSave;

		private BindingSource bdsSelectProduct;

		private BindingSource bdsProduct;

		private Button btnRemoveAll;

		private Button btnRemove;

		private Button btnAdd;

		private DataGridView dgSelectedProducts;

		private DataGridView dgProducts;

		private CheckBox cbAll;

		private Label label3;

		private AshComboBox cmbProGroup;
        private DataGridViewTextBoxColumn col_SelectedProduct;
        private DataGridViewCheckBoxColumn col_Select;
        private DataGridViewTextBoxColumn col_Selection;
        private DataGridViewTextBoxColumn col_Barcode;
        private Label label2;

		public UserPrevilegeDtl plgDtls { get; set; }

		public FavouriteProduct()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
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

		private void FillSelectedProducts()
		{
			dgSelectedProducts.AutoGenerateColumns = false;
			dgSelectedProducts.DataSource = null;
			bdsSelectProduct.DataSource = null;
			bdsSelectProduct.DataSource = entSelectedProduct;
			dgSelectedProducts.DataSource = entSelectedProduct;
		}

		private void FavouriteProduct_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbProGroup);
			entSelectedProduct = db.Products(new
			{
				IsFavorite = true
			}).ToList();
			entOldSelectedProduct = db.Products(new
			{
				IsFavorite = true
			}).ToList();
			FillSelectedProducts();
		}

		private void cmbProGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			exProducts = new List<ExProductEntity>();
			List<ProductEntity> source = db.Products(new
			{
				PGroupId = cmbProGroup.SelectedValue.ToInt32()
			});
			exProducts = AniHelper.CopyListData<ExProductEntity>(source.Cast<object>().ToList());
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
			foreach (ExProductEntity exProduct in exProducts)
			{
				if (exProduct.IsSelected)
				{
					ProductEntity productEntity = new ProductEntity();
					productEntity = exProduct.CopyTo<ProductEntity>();
					entSelectedProduct.Add(productEntity);
				}
			}
			FillSelectedProducts();
			cbAll.Checked = false;
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			ProductEntity item = new ProductEntity();
			if (dgSelectedProducts.CurrentRow != null)
			{
				item = (ProductEntity)dgSelectedProducts.CurrentRow.DataBoundItem;
			}
			entSelectedProduct.Remove(item);
			dgSelectedProducts.Refresh();
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			entSelectedProduct = new List<ProductEntity>();
			FillSelectedProducts();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (entOldSelectedProduct.Count > 0)
			{
				foreach (ProductEntity item in entOldSelectedProduct)
				{
					if (!entSelectedProduct.Any((ProductEntity x) => x.ProductId == item.ProductId))
					{
						item.IsFavorite = false;
						AshproCommon.UpdateToDatabaseObj(item, "Product", "ProductId", item.ProductId.toInt32());
					}
				}
			}
			if (entSelectedProduct.Count > 0)
			{
				foreach (ProductEntity item2 in entSelectedProduct)
				{
					item2.IsFavorite = true;
					AshproCommon.UpdateToDatabaseObj(item2, "Product", "ProductId", item2.ProductId.toInt32());
				}
			}
			Messages.UpdateMessage();
			Close();
		}

		private void dgProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSelectedProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavouriteProduct));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgSelectedProducts = new System.Windows.Forms.DataGridView();
            this.col_SelectedProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgProducts = new System.Windows.Forms.DataGridView();
            this.col_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_Selection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProGroup = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bdsSelectProduct = new System.Windows.Forms.BindingSource(this.components);
            this.bdsProduct = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgSelectedProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSelectProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemoveAll
            // 
            resources.ApplyResources(this.btnRemoveAll, "btnRemoveAll");
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgSelectedProducts
            // 
            resources.ApplyResources(this.dgSelectedProducts, "dgSelectedProducts");
            this.dgSelectedProducts.AllowUserToAddRows = false;
            this.dgSelectedProducts.AllowUserToDeleteRows = false;
            this.dgSelectedProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgSelectedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSelectedProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SelectedProduct});
            this.dgSelectedProducts.Name = "dgSelectedProducts";
            this.dgSelectedProducts.RowHeadersVisible = false;
            this.dgSelectedProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgSelectedProducts_DataError);
            // 
            // col_SelectedProduct
            // 
            this.col_SelectedProduct.DataPropertyName = "ProductName";
            resources.ApplyResources(this.col_SelectedProduct, "col_SelectedProduct");
            this.col_SelectedProduct.Name = "col_SelectedProduct";
            this.col_SelectedProduct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_SelectedProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgProducts
            // 
            resources.ApplyResources(this.dgProducts, "dgProducts");
            this.dgProducts.AllowUserToAddRows = false;
            this.dgProducts.AllowUserToDeleteRows = false;
            this.dgProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Select,
            this.col_Selection,
            this.col_Barcode});
            this.dgProducts.Name = "dgProducts";
            this.dgProducts.RowHeadersVisible = false;
            this.dgProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgProducts_DataError);
            // 
            // col_Select
            // 
            this.col_Select.DataPropertyName = "IsSelected";
            resources.ApplyResources(this.col_Select, "col_Select");
            this.col_Select.Name = "col_Select";
            // 
            // col_Selection
            // 
            this.col_Selection.DataPropertyName = "ProductName";
            resources.ApplyResources(this.col_Selection, "col_Selection");
            this.col_Selection.Name = "col_Selection";
            this.col_Selection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Selection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Barcode
            // 
            this.col_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_Barcode, "col_Barcode");
            this.col_Barcode.Name = "col_Barcode";
            this.col_Barcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbAll
            // 
            resources.ApplyResources(this.cbAll, "cbAll");
            this.cbAll.Name = "cbAll";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbProGroup
            // 
            resources.ApplyResources(this.cmbProGroup, "cmbProGroup");
            this.cmbProGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProGroup.FormattingEnabled = true;
            this.cmbProGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbProGroup.Name = "cmbProGroup";
            this.cmbProGroup.SelectedIndexChanged += new System.EventHandler(this.cmbProGroup_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // FavouriteProduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgSelectedProducts);
            this.Controls.Add(this.dgProducts);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbProGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Name = "FavouriteProduct";
            this.Load += new System.EventHandler(this.FavouriteProduct_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbProGroup, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbAll, 0);
            this.Controls.SetChildIndex(this.dgProducts, 0);
            this.Controls.SetChildIndex(this.dgSelectedProducts, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.btnRemoveAll, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgSelectedProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSelectProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
