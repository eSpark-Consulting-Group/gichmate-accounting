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
	public class ProductPrinterAllocation : BaseForm
	{
		private DBContext db = new DBContext();

		private List<ExProductEntity> exProducts = new List<ExProductEntity>();

		private List<ExProductPrinter> entSelectedProduct = new List<ExProductPrinter>();

		private List<ProductPrinter> entOldProductPrinters = new List<ProductPrinter>();

		private IContainer components = null;

		private Label label1;

		private AshComboBox cmbProGroup;

		private Label label2;

		private Label label5;

		private DataGridView dgProducts;

		private CheckBox cbAll;

		private Label label3;

		private DataGridView dgSelectedProducts;

		private Button btnRemoveAll;

		private Button btnRemove;

		private Button btnAdd;

		private BindingSource bdsProduct;

		private BindingSource bdsSelectProduct;

		private Button btnDelete;

		private Button btnSave;

		private AshComboBox cmbKotPrinter;

		private DataGridViewCheckBoxColumn col_Select;

		private DataGridViewTextBoxColumn col_Selection;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_SelectedProduct;

		public UserPrevilegeDtl plgDtls { get; set; }

		public ProductPrinterAllocation()
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

		private void ProductPrinterAllocation_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbProGroup);
			AniHelper.FillKOTPrinter(cmbKotPrinter);
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
					ExProductPrinter x = new ExProductPrinter();
					x.FK_ProductId = exProduct.ProductId;
					x.ProductName = exProduct.ProductName;
					x.FK_KOTPrinterId = cmbKotPrinter.SelectedValue.ToInt32();
					x.FK_BranchId = Settings.Default.iBranchId;
					if (!entSelectedProduct.Any((ExProductPrinter y) => y.FK_ProductId == x.FK_ProductId))
					{
						entSelectedProduct.Add(x);
					}
				}
			}
			FillSelectedProducts();
			cbAll.Checked = false;
		}

		private void dgProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgSelectedProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			ExProductPrinter item = new ExProductPrinter();
			if (dgSelectedProducts.CurrentRow != null)
			{
				item = (ExProductPrinter)dgSelectedProducts.CurrentRow.DataBoundItem;
			}
			entSelectedProduct.Remove(item);
			dgSelectedProducts.Refresh();
		}

		private void btnRemoveAll_Click(object sender, EventArgs e)
		{
			entSelectedProduct = new List<ExProductPrinter>();
			FillSelectedProducts();
		}

		private void cmbKotPrinter_SelectedIndexChanged(object sender, EventArgs e)
		{
			entSelectedProduct = new List<ExProductPrinter>();
			List<ProductPrinter> list = db.ProductPrinters(new
			{
				FK_BranchId = Settings.Default.iBranchId,
				FK_KOTPrinterId = cmbKotPrinter.SelectedValue.ToInt32()
			}).ToList();
			entOldProductPrinters = new List<ProductPrinter>();
			entOldProductPrinters = db.ProductPrinters(new
			{
				FK_BranchId = Settings.Default.iBranchId,
				FK_KOTPrinterId = cmbKotPrinter.SelectedValue.ToInt32()
			}).ToList();
			foreach (ProductPrinter item in list)
			{
				ExProductPrinter exProductPrinter = new ExProductPrinter();
				exProductPrinter = item.CopyTo<ExProductPrinter>();
				exProductPrinter.ProductName = db.Products(new
				{
					ProductId = item.FK_ProductId
				}).FirstOrDefault().ProductName;
				entSelectedProduct.Add(exProductPrinter);
			}
			FillSelectedProducts();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (entSelectedProduct.Count == 0)
				{
					Messages.WarningMessage();
				}
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					return;
				}
				List<ProductPrinter> list = new List<ProductPrinter>();
				list = AniHelper.CopyListData<ProductPrinter>(entSelectedProduct.Cast<object>().ToList());
				if (AshproCommon.UpdateDatabase(list.Cast<object>().ToList(), entOldProductPrinters.Cast<object>().ToList(), "ProductPrinter", "Id"))
				{
					Messages.SavedMessage();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (entSelectedProduct.Count > 0 && Messages.DeleteConfirmationMessage() && AshproCommon.DeleteMethod("Delete from ProductPrinter where FK_BranchId='" + Settings.Default.iBranchId + "' and FK_KOTPrinterId='" + cmbKotPrinter.SelectedValue.ToInt32() + "'"))
			{
				btnRemoveAll_Click(null, null);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.ProductPrinterAllocation));
			label1 = new System.Windows.Forms.Label();
			cmbProGroup = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			dgProducts = new System.Windows.Forms.DataGridView();
			cbAll = new System.Windows.Forms.CheckBox();
			label3 = new System.Windows.Forms.Label();
			dgSelectedProducts = new System.Windows.Forms.DataGridView();
			btnRemoveAll = new System.Windows.Forms.Button();
			btnRemove = new System.Windows.Forms.Button();
			btnAdd = new System.Windows.Forms.Button();
			bdsProduct = new System.Windows.Forms.BindingSource(components);
			bdsSelectProduct = new System.Windows.Forms.BindingSource(components);
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			cmbKotPrinter = new AshUsercontrol.AshComboBox();
			col_SelectedProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Selection = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dgProducts).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgSelectedProducts).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsSelectProduct).BeginInit();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			resources.ApplyResources(cmbProGroup, "cmbProGroup");
			cmbProGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbProGroup.FormattingEnabled = true;
			cmbProGroup.LeaveColor = System.Drawing.Color.White;
			cmbProGroup.Name = "cmbProGroup";
			cmbProGroup.SelectedIndexChanged += new System.EventHandler(cmbProGroup_SelectedIndexChanged);
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			resources.ApplyResources(dgProducts, "dgProducts");
			dgProducts.AllowUserToAddRows = false;
			dgProducts.AllowUserToDeleteRows = false;
			dgProducts.BackgroundColor = System.Drawing.Color.White;
			dgProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgProducts.Columns.AddRange(col_Select, col_Selection, col_Barcode);
			dgProducts.Name = "dgProducts";
			dgProducts.RowHeadersVisible = false;
			dgProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgProducts_DataError);
			resources.ApplyResources(cbAll, "cbAll");
			cbAll.Name = "cbAll";
			cbAll.UseVisualStyleBackColor = true;
			cbAll.CheckedChanged += new System.EventHandler(cbAll_CheckedChanged);
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			resources.ApplyResources(dgSelectedProducts, "dgSelectedProducts");
			dgSelectedProducts.AllowUserToAddRows = false;
			dgSelectedProducts.AllowUserToDeleteRows = false;
			dgSelectedProducts.BackgroundColor = System.Drawing.Color.White;
			dgSelectedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgSelectedProducts.Columns.AddRange(col_SelectedProduct);
			dgSelectedProducts.Name = "dgSelectedProducts";
			dgSelectedProducts.RowHeadersVisible = false;
			dgSelectedProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgSelectedProducts_DataError);
			resources.ApplyResources(btnRemoveAll, "btnRemoveAll");
			btnRemoveAll.Name = "btnRemoveAll";
			btnRemoveAll.UseVisualStyleBackColor = true;
			btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
			resources.ApplyResources(btnRemove, "btnRemove");
			btnRemove.Name = "btnRemove";
			btnRemove.UseVisualStyleBackColor = true;
			btnRemove.Click += new System.EventHandler(btnRemove_Click);
			resources.ApplyResources(btnAdd, "btnAdd");
			btnAdd.Name = "btnAdd";
			btnAdd.UseVisualStyleBackColor = true;
			btnAdd.Click += new System.EventHandler(btnAdd_Click);
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Name = "btnDelete";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Name = "btnSave";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			resources.ApplyResources(cmbKotPrinter, "cmbKotPrinter");
			cmbKotPrinter.EnterColor = System.Drawing.Color.LightPink;
			cmbKotPrinter.FormattingEnabled = true;
			cmbKotPrinter.LeaveColor = System.Drawing.Color.White;
			cmbKotPrinter.Name = "cmbKotPrinter";
			cmbKotPrinter.SelectedIndexChanged += new System.EventHandler(cmbKotPrinter_SelectedIndexChanged);
			col_SelectedProduct.DataPropertyName = "ProductName";
			resources.ApplyResources(col_SelectedProduct, "col_SelectedProduct");
			col_SelectedProduct.Name = "col_SelectedProduct";
			col_SelectedProduct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			col_SelectedProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			col_Select.DataPropertyName = "IsSelected";
			resources.ApplyResources(col_Select, "col_Select");
			col_Select.Name = "col_Select";
			col_Selection.DataPropertyName = "ProductName";
			resources.ApplyResources(col_Selection, "col_Selection");
			col_Selection.Name = "col_Selection";
			col_Selection.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			col_Selection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			col_Barcode.DataPropertyName = "Barcode";
			resources.ApplyResources(col_Barcode, "col_Barcode");
			col_Barcode.Name = "col_Barcode";
			col_Barcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			col_Barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(cmbKotPrinter);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(btnRemoveAll);
			base.Controls.Add(btnRemove);
			base.Controls.Add(btnAdd);
			base.Controls.Add(dgSelectedProducts);
			base.Controls.Add(dgProducts);
			base.Controls.Add(cbAll);
			base.Controls.Add(label3);
			base.Controls.Add(label5);
			base.Controls.Add(cmbProGroup);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Name = "ProductPrinterAllocation";
			base.Load += new System.EventHandler(ProductPrinterAllocation_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbProGroup, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cbAll, 0);
			base.Controls.SetChildIndex(dgProducts, 0);
			base.Controls.SetChildIndex(dgSelectedProducts, 0);
			base.Controls.SetChildIndex(btnAdd, 0);
			base.Controls.SetChildIndex(btnRemove, 0);
			base.Controls.SetChildIndex(btnRemoveAll, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(cmbKotPrinter, 0);
			((System.ComponentModel.ISupportInitialize)dgProducts).EndInit();
			((System.ComponentModel.ISupportInitialize)dgSelectedProducts).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsSelectProduct).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
