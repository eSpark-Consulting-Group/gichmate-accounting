using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class ProductCombinationView : BaseForm
	{
		private string strTitleHead;

		private string strProType;

		private int? iProCombMasterId;

		private int iProductId;

		private DateTime estimateDate;

		private List<ProCombSettings> entProCombSettings;

		private List<ProCombSettings> oldProCombSettings;

		private List<ExProCombSettings> entExProCombSettings;

		private DataGridViewTextBoxEditingControl textbox;

		private DBContext db;

		private bool isProductUnitCombination = false;

		private AutoCompleteStringCollection coll;

		private List<string> products;

		private ProductEntity product;

		private ProductEntity productEntity;

		private ProCombMaster oldProCMaster;

		private IContainer components = null;

		private Label label10;

		private Label label9;

		private AshTextBoxNormal txtPurRate;

		private Label label4;

		private AshTextBoxNormal txtQty;

		private AshTextBoxNormal txtMRP;

		private Label label6;

		private ToolTip toolTip;

		private Button btnSave;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private ContextMenuStrip ContextMenustrip;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripMenuItem clearToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem toFocusListViewToolStripMenuItem;

		private Label lblHead;

		private AshComboBox cmbUnit;

		private AshTextBoxNormal txtProSaleRate;

		private Panel panel1;

		public AshDataGridview dgProduct;

		private Button btnDate;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private AshUpDown nudVchrNo;

		private Label label1;

		private GroupBox grpSrch;

		private AshTextBoxNormal txtNameSrch;

		private Label label3;

		private AshTextBoxNormal txtName;

		private Label label7;

		private Label label8;

		private DataGridView dgvDetails;

		private BindingSource bdsProComb;

		private AshNumericLabel lblQty;

		private AshTextBoxNormal txtBarcode;

		private Label label5;

		private CheckBox cbInclusive;

		private CheckBox cbTranslate;

		private CheckBox cbTransliterate;

		private AshTextBoxNormal txtLocalName;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn col_ProName;
        private DataGridViewTextBoxColumn col_Barcode;
        private DataGridViewTextBoxColumn cl_ProName;
        private DataGridViewTextBoxColumn col_Qty;
        private DataGridViewTextBoxColumn col_Unit;
        private DataGridViewTextBoxColumn col_SRate;
        private DataGridViewTextBoxColumn col_SValue;
        private DataGridViewTextBoxColumn col_PRate;
        private DataGridViewTextBoxColumn col_PValue;
        private DataGridViewTextBoxColumn col_MRate;
        private DataGridViewTextBoxColumn col_MValue;
        private Label label11;

		public UserPrevilegeDtl plgDtls { get; set; }

		public ProductCombinationView(string strTitle)
		{
			strTitleHead = strTitle;
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			col_MRate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_MValue.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_PRate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_PValue.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_SRate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SValue.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			txtMRP.Format = CommonSettings.strCurencyFormat;
			txtProSaleRate.Format = CommonSettings.strCurencyFormat;
			txtPurRate.Format = CommonSettings.strCurencyFormat;
			txtQty.Format = CommonSettings.strQuantityFormat;
		}

		private async Task AutoCompleteMethod(string sColumn, TextBox textBox)
		{
			try
			{
				await Task.Run(delegate
				{
					CollDataAddMeethod(sColumn);
				});
				textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
				textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
				textBox.AutoCompleteCustomSource = coll;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void CollDataAddMeethod(string sColumn)
		{
			coll = new AutoCompleteStringCollection();
			products = new List<string>();
			products = AshproCommon.GetStringListMethod("Select " + sColumn + " From Product");
			coll.AddRange(Enumerable.ToArray(products));
		}

		private void LoadDGVDetailsStarting()
		{
			dgvDetails.DataSource = null;
			List<ProCombMaster> list = db.ProCombMasters(new
			{
				CombType = strTitleHead
			}).ToList();
			if (list.Count > 0)
			{
				LoadDgvDetails(list);
			}
			nudVchrNo.Value = (list.Max((ProCombMaster y) => y.ProCombVchrNo) + 1) ?? 1;
		}

		private ProCombMaster EntityLoadMethod(int iVchrNo)
		{
			return new ProCombMaster
			{
				ProCombMasterId = iProCombMasterId,
				CombProName = txtName.Text,
				ProCombVchrNo = iVchrNo,
				Date = dtmDate.Value.Date,
				ProQty = txtQty.Value,
				UnitId = cmbUnit.SelectedValue.ToInt32(),
				ProSaleRate = txtProSaleRate.Value,
				ProPurRate = txtPurRate.Value,
				MRP = txtMRP.Value,
				CombType = strTitleHead,
				Barcode = txtBarcode.Text
			};
		}

		private ExProCombSettings getCurrent()
		{
			try
			{
				if (dgProduct.CurrentRow != null)
				{
					return (ExProCombSettings)dgProduct.CurrentRow.DataBoundItem;
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

		private void BinDgv()
		{
			dgProduct.AutoGenerateColumns = false;
			bdsProComb.DataSource = null;
			dgProduct.DataSource = null;
			bdsProComb.DataSource = entExProCombSettings;
			dgProduct.DataSource = bdsProComb;
			lblQty.Value = entExProCombSettings.Sum((ExProCombSettings x) => x.ProQty);
		}

		private void TotalFindMethod()
		{
			lblQty.Value = entExProCombSettings.Sum((ExProCombSettings x) => x.ProQty);
			txtPurRate.Value = entExProCombSettings.Sum((ExProCombSettings x) => x.PValue);
			txtProSaleRate.Value = entExProCombSettings.Sum((ExProCombSettings x) => x.SValue);
			txtMRP.Value = entExProCombSettings.Sum((ExProCombSettings x) => x.MValue);
			txtQty.Value = 1m;
		}

		private void ExProCompStngsEntityLoadMethod(ExProCombSettings t, ProductEntity productEntity, decimal iQty)
		{
			if (productEntity != null)
			{
				string unitName = (from x in db.Units()
					where x.UnitId == productEntity.UnitId
					select x).FirstOrDefault().UnitName;
				t.ProCombSettingsId = null;
				t.ProductId = productEntity.ProductId.toInt32();
				t.Barcode = productEntity.Barcode;
				t.ProName = productEntity.ProductName;
				t.ProQty = iQty;
				t.UnitId = productEntity.UnitId;
				t.UnitName = unitName;
				t.PurRate = productEntity.PurchasePrice;
				t.PValue = ((t.ProQty == 0m) ? 0m : (productEntity.PurchasePrice * t.ProQty));
				t.SaleRate = productEntity.SellingPrice;
				t.SValue = ((t.ProQty == 0m) ? 0m : (productEntity.SellingPrice * t.ProQty));
				t.MRP = productEntity.MRP;
				t.MValue = ((t.ProQty == 0m) ? 0m : (productEntity.MRP * t.ProQty));
			}
		}

		private void LoadDgvDetails(List<ProCombMaster> proCombMasters)
		{
			int num = 1;
			List<ExProCombMaster> list = AniHelper.CopyListData<ExProCombMaster>(proCombMasters.Cast<object>().ToList());
			foreach (ExProCombMaster item in list)
			{
				item.SL = num;
				num++;
			}
			dgvDetails.AutoGenerateColumns = false;
			dgvDetails.DataSource = list;
		}

		private void LoadFromDB(ProCombMaster proCMaster, bool isNudChanged = false)
		{
			iProCombMasterId = proCMaster.ProCombMasterId.toInt32();
			dtmDate.Value = proCMaster.Date;
			txtName.Text = proCMaster.CombProName;
			ProductEntity productEntity = db.Products(new
			{
				ProductName = proCMaster.CombProName
			}).FirstOrDefault();
			iProductId = productEntity.ProductId.toInt32();
			cmbUnit.SelectedValue = proCMaster.UnitId;
			txtQty.Value = proCMaster.ProQty;
			txtPurRate.Value = proCMaster.ProPurRate;
			txtProSaleRate.Value = proCMaster.ProSaleRate;
			txtMRP.Value = proCMaster.MRP;
			txtBarcode.Text = proCMaster.Barcode;
			txtLocalName.Text = productEntity.LocalName;
			if (!isNudChanged)
			{
				nudVchrNo.Value = proCMaster.ProCombVchrNo.ToInt32();
			}
			entProCombSettings = new List<ProCombSettings>();
			entProCombSettings = (oldProCombSettings = db.ProCombSettings(new { proCMaster.ProCombMasterId }).ToList());
			entExProCombSettings = new List<ExProCombSettings>();
			entExProCombSettings = AniHelper.CopyListData<ExProCombSettings>(entProCombSettings.Cast<object>().ToList());
			entExProCombSettings.ForEach(delegate(ExProCombSettings x)
			{
				x.ProName = db.Products(new { x.ProductId }).FirstOrDefault().ProductName;
				x.UnitName = db.Units(new { x.UnitId }).FirstOrDefault().UnitName;
				x.PValue = ((x.ProQty == 0m) ? 0m : (x.PurRate * x.ProQty));
				x.SValue = ((x.ProQty == 0m) ? 0m : (x.SaleRate * x.ProQty));
				x.MValue = ((x.ProQty == 0m) ? 0m : (x.MRP * x.ProQty));
			});
			BinDgv();
			DisableMethod();
		}

		private void DisableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			grpSrch.Enabled = true;
			btnEdit.Enabled = true;
			btnEnd.Enabled = true;
			btnClear.Enabled = true;
			nudVchrNo.Enabled = true;
			btnDelete.Enabled = true;
		}

		private ProductEntity ProductEntityLoadMethod()
		{
			return new ProductEntity
			{
				ProductId = null,
				ProductCode = db.Products().Max((ProductEntity x) => x.ProductCode) + 1,
				ProductName = txtName.Text,
				ProductType = strProType,
				PGroupId = db.ProductGroups().FirstOrDefault().PGroupId.toInt32(),
				BrandId = db.Brands().FirstOrDefault().BrandId.ToInt32(),
				TCategoryId = db.TaxCategories().FirstOrDefault().TCategoryId.ToInt32(),
				AccountId = db.Accounts().FirstOrDefault().AccountId.ToInt32(),
				GodwnId = db.Godowns().FirstOrDefault().GodwnId.ToInt32(),
				SelfId = db.Shelfs().FirstOrDefault().SelfId.ToInt32(),
				UnitId = cmbUnit.SelectedValue.ToInt32(),
				PurchasePrice = txtPurRate.Value,
				SellingPrice = txtProSaleRate.Value,
				MRP = txtMRP.Value,
				OpeningStock = 0m,
				OppStockDate = DateTime.Now.Date,
				ReOrderStock = 0m,
				PurchaseDis = 0m,
				SaleDis = 0m,
				AliasCode = (db.Products().Max((ProductEntity x) => x.ProductCode) + 1).ToString(),
				AliasName = txtName.Text,
				Remarks = strProType,
				LocalName = txtLocalName.Text,
				PurchaseUnit = cmbUnit.SelectedValue.ToInt32(),
				SalesUnit = cmbUnit.SelectedValue.ToInt32(),
				ReportUnit = cmbUnit.SelectedValue.ToInt32(),
				isActive = true,
				Barcode = txtBarcode.Text
			};
		}

		private List<ProductPriceCategory> ProPCategoryEntityLoadMethod()
		{
			List<ProductPriceCategory> list = new List<ProductPriceCategory>();
			foreach (PriceCategoryEntity item2 in db.PriceCategories().ToList())
			{
				ProductPriceCategory item = new ProductPriceCategory
				{
					ProPrceCatId = null,
					ProductId = productEntity.ProductId.toInt32(),
					PCategoryId = item2.PCategoryId.toInt32(),
					UnitId = cmbUnit.SelectedValue.ToInt32(),
					PGroupId = productEntity.PGroupId,
					SPrice = productEntity.SellingPrice,
					SPDisc = 0m,
					PPrice = productEntity.PurchasePrice,
					PDisc = 0m
				};
				list.Add(item);
			}
			return list;
		}

		private void SaveSubMethod(string sVoucherType)
		{
			if (!isProductUnitCombination)
			{
				if (AshproCommon.InsertToDatabaseObj(ProductEntityLoadMethod(), "Product"))
				{
					productEntity = (from x in db.Products()
						where x.ProductName == txtName.Text
						select x).FirstOrDefault();
					AshproCommon.InsertToDatabase(ProPCategoryEntityLoadMethod().Cast<object>().ToList(), "ProductPriceCategory");
					ProductUnit data = ProductUnitEntityMethod();
					AshproCommon.InsertToDatabaseObj(data);
					ProductBarcodeEntity data2 = ProductBarcodeEntityMethod();
					AshproCommon.InsertToDatabaseObj(data2, "ProductBarcode");
				}
			}
			else
			{
				UnitProductCombinationMethod(sVoucherType);
			}
		}

		private void DeleteParMethod(string sVoucherType)
		{
			if (!isProductUnitCombination)
			{
				if (AshproCommon.DeleteMethod("Delete from ProductPriceCategory where ProductId='" + iProductId + "'") && AshproCommon.DeleteMethod("Delete from ProductUnit where ProductId='" + iProductId + "'") && AshproCommon.DeleteMethod("Delete from ProductBarcode where FK_ProductId='" + iProductId + "'"))
				{
					AshproCommon.DeleteFromDatabase("Product", "ProductId", iProductId);
				}
			}
			else
			{
				UnitProductCombinationMethodForDeleteEntry(sVoucherType);
			}
		}

		private ProductBarcodeEntity ProductBarcodeEntityMethod()
		{
			return new ProductBarcodeEntity
			{
				Barcode = txtBarcode.Text,
				BarcodeType = "Base Barcode",
				FK_BranchId = Settings.Default.iBranchId,
				FK_ProductId = productEntity.ProductId,
				FK_UnitId = cmbUnit.SelectedValue.ToInt32(),
				MinimuPrice = txtProSaleRate.Value,
				MRP = txtMRP.Value,
				RetailPrice = txtProSaleRate.Value,
				SellingPrice = txtProSaleRate.Value,
				WholesalePrice = txtProSaleRate.Value,
				PurchasePrice = txtPurRate.Value,
				ProductCost = txtPurRate.Value,
				FK_BatchId = 1
			};
		}

		private ProductUnit ProductUnitEntityMethod()
		{
			return new ProductUnit
			{
				ProductId = productEntity.ProductId,
				UnitId = cmbUnit.SelectedValue.ToInt32(),
				Barcode = txtBarcode.Text,
				IsInclusive = cbInclusive.Checked,
				OpeningStock = 0m,
				ProductUnitName = txtName.Text,
				SalesPrice = txtProSaleRate.Value,
				UnitQty = 1m,
				UnitType = "Base Unit"
			};
		}

		private void UnitProductCombinationMethod(string sVoucherType)
		{
			AshproCommon.DeleteMethod("delete from StockTranLog where ProductId='" + product.ProductId + "' and VchrType='" + sVoucherType + "'");
			AshproCommon.DeleteMethod("Delete from GdwnStckTranLog where VchrId='" + product.ProductId + "' and VchrType='" + sVoucherType + "'");
			product.ProductType = strProType;
			AshproCommon.UpdateMethod_SP(product, "spUpdateProduct");
			if (product.OpeningStock > 0m && product.ProductType != "Service")
			{
				ProCombSettings proCombSettings = entProCombSettings.FirstOrDefault();
				ProductEntity productEntity = db.Products(new { proCombSettings.ProductId }).FirstOrDefault();
				decimal dmlInQty = proCombSettings.ProQty * product.OpeningStock;
				POSHelper.STranLogMethod(proCombSettings.ProductId.ToInt32(), productEntity.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, dmlInQty, 0m, productEntity.ProductId.ToInt32(), productEntity.UnitId.ToInt32(), db.Godowns().FirstOrDefault().GodwnId.toInt32());
			}
		}

		private void UnitProductCombinationMethodForDeleteEntry(string sVoucherType)
		{
			ProCombSettings proCombSettings = oldProCombSettings.FirstOrDefault();
			ProductEntity productEntity = db.Products(new
			{
				ProductName = oldProCMaster.CombProName
			}).FirstOrDefault();
			decimal num = proCombSettings.ProQty * productEntity.OpeningStock;
			ProductEntity productEntity2 = db.Products(new { proCombSettings.ProductId }).FirstOrDefault();
			AshproCommon.DeleteMethod("delete from StockTranLog where ProductId='" + productEntity2.ProductId + "' and VchrType='" + sVoucherType + "' and InQuantity='" + num + "'");
			AshproCommon.DeleteMethod("Delete from GdwnStckTranLog where VchrId='" + productEntity2.ProductId + "' and VchrType='" + sVoucherType + "'  and InQuantity='" + num + "'");
			productEntity.ProductType = "Finished Good";
			AshproCommon.UpdateMethod_SP(productEntity, "spUpdateProduct");
			sVoucherType = "Opening Entry";
			if (productEntity.OpeningStock > 0m && productEntity.ProductType != "Service")
			{
				POSHelper.STranLogMethod(productEntity.ProductId.ToInt32(), productEntity.ProductCode.ToInt32(), sVoucherType, sVoucherType, 0, "Opening Stock Entry", Settings.Default.intLogOnId, productEntity.OpeningStock, 0m, productEntity.ProductId.ToInt32(), productEntity.UnitId.ToInt32(), db.Godowns().FirstOrDefault().GodwnId.toInt32());
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			iProductId = -1;
			iProCombMasterId = -1;
			entExProCombSettings = new List<ExProCombSettings>();
			entProCombSettings = new List<ProCombSettings>();
			BinDgv();
			AniHelper.ClearMethod(this);
			iProCombMasterId = null;
			LoadDGVDetailsStarting();
			btnDelete.Enabled = false;
			dgProduct.Focus();
			dgProduct.CurrentCell = dgProduct.Rows[dgProduct.Rows.Count - 1].Cells[1];
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string sVoucherType = "Opening Entry";
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage())
				{
					DeleteParMethod(sVoucherType);
					if (AshproCommon.DeleteFromDatabase("ProCombSettings", "ProCombMasterId", iProCombMasterId.ToInt32()) && AshproCommon.DeleteFromDatabase("ProCombMaster", "ProCombMasterId", iProCombMasterId.ToInt32()))
					{
						btnClear_Click(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "  717");
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			dgProduct.Focus();
			dgProduct.CurrentCell = dgProduct.Rows[dgProduct.Rows.Count - 1].Cells[1];
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string sVoucherType = "Opening Entry";
			if (txtName.Text == string.Empty)
			{
				Messages.WarningMessage("Please Enter all details.");
				return;
			}
			entProCombSettings = AniHelper.CopyListData<ProCombSettings>(entExProCombSettings.Cast<object>().ToList());
			if (!iProCombMasterId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				int num = (db.ProCombMasters(new
				{
					CombType = strTitleHead
				}).Max((ProCombMaster y) => y.ProCombVchrNo) + 1) ?? 1;
				ProCombMaster data = EntityLoadMethod(num);
				if (AshproCommon.InsertToDatabaseObj(data))
				{
					iProCombMasterId = db.ProCombMasters(new
					{
						CombType = strTitleHead,
						ProCombVchrNo = num
					}).FirstOrDefault().ProCombMasterId.toInt32();
					entProCombSettings.ForEach(delegate(ProCombSettings x)
					{
						x.ProCombMasterId = iProCombMasterId;
					});
					if (AshproCommon.InsertToDatabase(entProCombSettings.Cast<object>().ToList(), "ProCombSettings"))
					{
						SaveSubMethod(sVoucherType);
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
				ProCombMaster data2 = EntityLoadMethod(nudVchrNo.Value.ToInt32());
				DeleteParMethod(sVoucherType);
				if (AshproCommon.DeleteFromDatabase("ProCombSettings", "ProCombMasterId", iProCombMasterId.ToInt32()) && AshproCommon.UpdateToDatabaseObj(data2, "ProCombMaster", "ProCombMasterId", iProCombMasterId.ToInt32()))
				{
					entProCombSettings.ForEach(delegate(ProCombSettings x)
					{
						x.ProCombMasterId = iProCombMasterId;
						x.ProCombSettingsId = null;
					});
					if (AshproCommon.InsertToDatabase(entProCombSettings.Cast<object>().ToList(), "ProCombSettings"))
					{
						SaveSubMethod(sVoucherType);
					}
				}
			}
			btnClear_Click(null, null);
		}

		private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					ExProCombMaster source = (ExProCombMaster)dgvDetails.CurrentRow.DataBoundItem;
					ProCombMaster proCombMaster = new ProCombMaster();
					oldProCMaster = new ProCombMaster();
					LoadFromDB(oldProCMaster = source.CopyTo<ProCombMaster>(), isNudChanged: true);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void nudVchrNo_ValueChanged(object sender, EventArgs e)
		{
			ProCombMaster proCombMaster = (from x in db.ProCombMasters()
				where x.ProCombVchrNo == nudVchrNo.Value.ToInt32()
				select x).FirstOrDefault();
			if (proCombMaster != null)
			{
				LoadFromDB(proCombMaster, isNudChanged: true);
			}
			else
			{
				btnClear_Click(null, null);
			}
		}

		private void dgProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExProCombSettings current = getCurrent();
			if (dgProduct.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductEntity productEntity = db.Products(new { current.Barcode }).FirstOrDefault();
				ExProCompStngsEntityLoadMethod(current, productEntity, 1m);
				entExProCombSettings.ForEach(delegate(ExProCombSettings x)
				{
					if (!txtName.Text.Contains(x.ProName))
					{
						txtName.Text = ((txtName.Text == string.Empty) ? x.ProName : (txtName.Text + "-" + x.ProName));
					}
				});
				LocalNameGenerateMethod();
			}
			else if (dgProduct.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				ProductEntity productEntity2 = db.Products(new { current.ProductId }).FirstOrDefault();
				ExProCompStngsEntityLoadMethod(current, productEntity2, current.ProQty);
			}
			else if (dgProduct.CurrentCell.OwningColumn.Name == col_SRate.Name)
			{
				current.SValue = ((current.ProQty == 0m) ? 0m : (current.SaleRate * current.ProQty));
			}
			if (!isProductUnitCombination)
			{
				TotalFindMethod();
			}
		}

		private void dgProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
				ExProCombSettings current = getCurrent();
				int rowIndex = dgProduct.CurrentCell.RowIndex;
				if (dgProduct.CurrentCell.OwningColumn.Name == cl_ProName.Name)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer selectCustomer = new SelectCustomer("");
					selectCustomer.sName = textbox.Text;
					if (selectCustomer.ShowDialog() != DialogResult.OK)
					{
						return;
					}
					dgProduct.CurrentCell.Value = selectCustomer.sName;
					ProductEntity productEntity = db.Products(new
					{
						ProductId = selectCustomer.iId
					}).FirstOrDefault();
					ExProCompStngsEntityLoadMethod(current, productEntity, 1m);
					if (!isProductUnitCombination)
					{
						TotalFindMethod();
						entExProCombSettings.ForEach(delegate(ExProCombSettings x)
						{
							if (!txtName.Text.Contains(x.ProName))
							{
								txtName.Text = ((txtName.Text == string.Empty) ? x.ProName : (txtName.Text + "-" + x.ProName));
							}
						});
						LocalNameGenerateMethod();
					}
					dgProduct.CurrentCell = dgProduct.Rows[rowIndex].Cells[col_Qty.Name];
				}
				else
				{
					if (!(dgProduct.CurrentCell.OwningColumn.Name == col_Unit.Name) || e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer selectCustomer2 = new SelectCustomer("Unit");
					selectCustomer2.ProductId = current.ProductId.ToInt32();
					if (selectCustomer2.ShowDialog() == DialogResult.OK)
					{
						dgProduct.CurrentCell.Value = selectCustomer2.sName;
						int pCategoryId = db.PriceCategories().FirstOrDefault().PCategoryId.toInt32();
						ProductPriceCategory productPriceCategory = db.ProductPriceCategories(new
						{
							ProductId = current.ProductId,
							PCategoryId = pCategoryId,
							UnitId = selectCustomer2.iId
						}).FirstOrDefault();
						current.UnitId = selectCustomer2.iId;
						current.UnitName = selectCustomer2.sName;
						current.SaleRate = productPriceCategory.SPrice;
						current.SValue = ((current.ProQty == 0m) ? 0m : (current.ProQty * productPriceCategory.SPrice));
						current.PurRate = productPriceCategory.PPrice;
						current.PValue = ((current.ProQty == 0m) ? 0m : (current.ProQty * productPriceCategory.PPrice));
						current.MRP *= db.Productunits(new
						{
							ProductId = current.ProductId,
							UnitId = selectCustomer2.iId
						}).FirstOrDefault().UnitQty;
						current.MValue = ((current.ProQty == 0m) ? 0m : (current.MRP * current.ProQty));
						dgProduct.CurrentCell = dgProduct.Rows[dgProduct.CurrentCell.RowIndex + 1].Cells[1];
						if (!isProductUnitCombination)
						{
							TotalFindMethod();
						}
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
			e.KeyChar.ToString();
			if (dgProduct.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && !(sender as TextBox).Text.Contains('.')) || (e.KeyChar == '.' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) || (e.KeyChar == '-' && !(sender as TextBox).Text.Contains('-')) || (e.KeyChar == '-' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength))
				{
					return;
				}
				e.Handled = true;
			}
			if (dgProduct.CurrentCell.OwningColumn.Name == cl_ProName.Name)
			{
				dgProduct.NotifyCurrentCellDirty(dirty: true);
			}
		}

		private void txtNameSrch_TextChanged(object sender, EventArgs e)
		{
			List<ProCombMaster> proCombMasters = (from x in db.ProCombMasters()
				where x.CombType == strTitleHead && x.CombProName.StartsWith(txtNameSrch.Text)
				select x).ToList();
			LoadDgvDetails(proCombMasters);
		}

		private void dgProduct_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ExProCombSettings current = getCurrent();
				entExProCombSettings.Remove(current);
				BinDgv();
			}
		}

		private void txtName_Validated(object sender, EventArgs e)
		{
			if (isProductUnitCombination)
			{
				product = db.Products(new
				{
					ProductName = txtName.Text
				}).FirstOrDefault();
				if (product != null)
				{
					txtBarcode.Text = product.Barcode;
					cmbUnit.SelectedValue = product.UnitId;
					txtProSaleRate.Value = product.SellingPrice;
					txtPurRate.Value = product.PurchasePrice;
					txtMRP.Value = product.MRP;
					txtLocalName.Text = product.LocalName;
				}
			}
		}

		private void LocalNameGenerateMethod()
		{
			if (cbTranslate.Checked)
			{
				GoogleTrans googleTrans = new GoogleTrans();
				txtLocalName.Text = googleTrans.Translate(txtName.Text, "en", "ar");
			}
			if (cbTransliterate.Checked)
			{
				txtLocalName.Text = ClsArabic.Transliterated2Arabic(txtName.Text);
			}
		}

		private void txtBarcode_Validated(object sender, EventArgs e)
		{
			if (txtBarcode.Text != string.Empty && !iProCombMasterId.HasValue && AshproCommon.GetIntDataWithParameter_SP("spBarcodeExist", new
			{
				Barcode = txtBarcode.Text
			}) > 0)
			{
				Messages.ErrorMessage("Barcode already exist.");
				txtBarcode.Focus();
				txtBarcode.SelectAll();
			}
			else if (isProductUnitCombination)
			{
				product = db.Products(new
				{
					Barcode = txtBarcode.Text
				}).FirstOrDefault();
				if (product != null)
				{
					txtName.Text = product.ProductName;
					cmbUnit.SelectedValue = product.UnitId;
					txtProSaleRate.Value = product.SellingPrice;
					txtPurRate.Value = product.PurchasePrice;
					txtMRP.Value = product.MRP;
				}
			}
		}

		private void ProductCombinationView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDate_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private async void ProductCombinationView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			entExProCombSettings = new List<ExProCombSettings>();
			entProCombSettings = new List<ProCombSettings>();
			lblHead.Text = strTitleHead;
			if (strTitleHead == "Packing")
			{
				strProType = "Finished Good";
			}
			else if (strTitleHead == "Product Combination")
			{
				strProType = "Combained Product";
			}
			else
			{
				strProType = "Combained Product";
				isProductUnitCombination = true;
				await AutoCompleteMethod("ProductName", txtName);
				await AutoCompleteMethod("Barcode", txtBarcode);
			}
			estimateDate = Settings.Default.appDate;
			dtmDate.Value = estimateDate;
			AniHelper.FillUnit(cmbUnit);
			LoadDGVDetailsStarting();
			BinDgv();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCombinationView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPurRate = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQty = new AshUsercontrol.AshTextBoxNormal();
            this.txtMRP = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.ContextMenustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toFocusListViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHead = new System.Windows.Forms.Label();
            this.cmbUnit = new AshUsercontrol.AshComboBox();
            this.txtProSaleRate = new AshUsercontrol.AshTextBoxNormal();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgProduct = new AshUsercontrol.AshDataGridview();
            this.col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.nudVchrNo = new AshUsercontrol.AshUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.grpSrch = new System.Windows.Forms.GroupBox();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNameSrch = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblQty = new AshUsercontrol.AshNumericLabel();
            this.txtBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.cbInclusive = new System.Windows.Forms.CheckBox();
            this.cbTranslate = new System.Windows.Forms.CheckBox();
            this.cbTransliterate = new System.Windows.Forms.CheckBox();
            this.txtLocalName = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.bdsProComb = new System.Windows.Forms.BindingSource(this.components);
            this.ContextMenustrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVchrNo)).BeginInit();
            this.grpSrch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProComb)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
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
            this.toolTip.SetToolTip(this.txtPurRate, resources.GetString("txtPurRate.ToolTip"));
            this.txtPurRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // txtQty
            // 
            resources.ApplyResources(this.txtQty, "txtQty");
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Data = 1;
            this.txtQty.EnterColor = System.Drawing.Color.LightPink;
            this.txtQty.Format = null;
            this.txtQty.isAllowNegative = false;
            this.txtQty.isAllowSpecialChar = false;
            this.txtQty.isNumberOnly = false;
            this.txtQty.isNumeric = true;
            this.txtQty.LeaveColor = System.Drawing.Color.White;
            this.txtQty.Name = "txtQty";
            this.toolTip.SetToolTip(this.txtQty, resources.GetString("txtQty.ToolTip"));
            this.txtQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.toolTip.SetToolTip(this.txtMRP, resources.GetString("txtMRP.ToolTip"));
            this.txtMRP.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.toolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
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
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.toolTip.SetToolTip(this.btnDate, resources.GetString("btnDate.ToolTip"));
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // ContextMenustrip
            // 
            resources.ApplyResources(this.ContextMenustrip, "ContextMenustrip");
            this.ContextMenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toFocusListViewToolStripMenuItem});
            this.ContextMenustrip.Name = "contextMenuStrip1";
            this.toolTip.SetToolTip(this.ContextMenustrip, resources.GetString("ContextMenustrip.ToolTip"));
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // editToolStripMenuItem
            // 
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
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
            // toFocusListViewToolStripMenuItem
            // 
            resources.ApplyResources(this.toFocusListViewToolStripMenuItem, "toFocusListViewToolStripMenuItem");
            this.toFocusListViewToolStripMenuItem.Name = "toFocusListViewToolStripMenuItem";
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
            // 
            // cmbUnit
            // 
            resources.ApplyResources(this.cmbUnit, "cmbUnit");
            this.cmbUnit.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.LeaveColor = System.Drawing.Color.White;
            this.cmbUnit.Name = "cmbUnit";
            this.toolTip.SetToolTip(this.cmbUnit, resources.GetString("cmbUnit.ToolTip"));
            // 
            // txtProSaleRate
            // 
            resources.ApplyResources(this.txtProSaleRate, "txtProSaleRate");
            this.txtProSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProSaleRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProSaleRate.Data = 0;
            this.txtProSaleRate.EnterColor = System.Drawing.Color.LightPink;
            this.txtProSaleRate.Format = null;
            this.txtProSaleRate.isAllowNegative = false;
            this.txtProSaleRate.isAllowSpecialChar = false;
            this.txtProSaleRate.isNumberOnly = false;
            this.txtProSaleRate.isNumeric = true;
            this.txtProSaleRate.LeaveColor = System.Drawing.Color.White;
            this.txtProSaleRate.Name = "txtProSaleRate";
            this.toolTip.SetToolTip(this.txtProSaleRate, resources.GetString("txtProSaleRate.ToolTip"));
            this.txtProSaleRate.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.dgProduct);
            this.panel1.Name = "panel1";
            this.toolTip.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // dgProduct
            // 
            resources.ApplyResources(this.dgProduct, "dgProduct");
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgProduct.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Barcode,
            this.cl_ProName,
            this.col_Qty,
            this.col_Unit,
            this.col_SRate,
            this.col_SValue,
            this.col_PRate,
            this.col_PValue,
            this.col_MRate,
            this.col_MValue});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgProduct.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgProduct.EnableHeadersVisualStyles = false;
            this.dgProduct.EnterKeyNavigation = false;
            this.dgProduct.LastKey = System.Windows.Forms.Keys.None;
            this.dgProduct.Name = "dgProduct";
            this.dgProduct.RowHeadersVisible = false;
            this.toolTip.SetToolTip(this.dgProduct, resources.GetString("dgProduct.ToolTip"));
            this.dgProduct.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProduct_CellEndEdit);
            this.dgProduct.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgProduct_DataError);
            this.dgProduct.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgProduct_EditingControlShowing);
            this.dgProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgProduct_KeyDown);
            // 
            // col_Barcode
            // 
            this.col_Barcode.DataPropertyName = "Barcode";
            resources.ApplyResources(this.col_Barcode, "col_Barcode");
            this.col_Barcode.Name = "col_Barcode";
            // 
            // cl_ProName
            // 
            this.cl_ProName.DataPropertyName = "ProName";
            resources.ApplyResources(this.cl_ProName, "cl_ProName");
            this.cl_ProName.Name = "cl_ProName";
            // 
            // col_Qty
            // 
            this.col_Qty.DataPropertyName = "ProQty";
            resources.ApplyResources(this.col_Qty, "col_Qty");
            this.col_Qty.Name = "col_Qty";
            // 
            // col_Unit
            // 
            this.col_Unit.DataPropertyName = "UnitName";
            resources.ApplyResources(this.col_Unit, "col_Unit");
            this.col_Unit.Name = "col_Unit";
            // 
            // col_SRate
            // 
            this.col_SRate.DataPropertyName = "SaleRate";
            resources.ApplyResources(this.col_SRate, "col_SRate");
            this.col_SRate.Name = "col_SRate";
            this.col_SRate.ReadOnly = true;
            // 
            // col_SValue
            // 
            this.col_SValue.DataPropertyName = "SValue";
            resources.ApplyResources(this.col_SValue, "col_SValue");
            this.col_SValue.Name = "col_SValue";
            this.col_SValue.ReadOnly = true;
            // 
            // col_PRate
            // 
            this.col_PRate.DataPropertyName = "PurRate";
            resources.ApplyResources(this.col_PRate, "col_PRate");
            this.col_PRate.Name = "col_PRate";
            this.col_PRate.ReadOnly = true;
            // 
            // col_PValue
            // 
            this.col_PValue.DataPropertyName = "PValue";
            resources.ApplyResources(this.col_PValue, "col_PValue");
            this.col_PValue.Name = "col_PValue";
            this.col_PValue.ReadOnly = true;
            // 
            // col_MRate
            // 
            this.col_MRate.DataPropertyName = "MRP";
            resources.ApplyResources(this.col_MRate, "col_MRate");
            this.col_MRate.Name = "col_MRate";
            this.col_MRate.ReadOnly = true;
            // 
            // col_MValue
            // 
            this.col_MValue.DataPropertyName = "MValue";
            resources.ApplyResources(this.col_MValue, "col_MValue");
            this.col_MValue.Name = "col_MValue";
            this.col_MValue.ReadOnly = true;
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // nudVchrNo
            // 
            resources.ApplyResources(this.nudVchrNo, "nudVchrNo");
            this.nudVchrNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVchrNo.Name = "nudVchrNo";
            this.toolTip.SetToolTip(this.nudVchrNo, resources.GetString("nudVchrNo.ToolTip"));
            this.nudVchrNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVchrNo.ValueChanged += new System.EventHandler(this.nudVchrNo_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // grpSrch
            // 
            resources.ApplyResources(this.grpSrch, "grpSrch");
            this.grpSrch.BackColor = System.Drawing.Color.White;
            this.grpSrch.Controls.Add(this.dgvDetails);
            this.grpSrch.Controls.Add(this.txtNameSrch);
            this.grpSrch.Controls.Add(this.label3);
            this.grpSrch.ForeColor = System.Drawing.Color.Black;
            this.grpSrch.Name = "grpSrch";
            this.grpSrch.TabStop = false;
            this.toolTip.SetToolTip(this.grpSrch, resources.GetString("grpSrch.ToolTip"));
            // 
            // dgvDetails
            // 
            resources.ApplyResources(this.dgvDetails, "dgvDetails");
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ProName});
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.toolTip.SetToolTip(this.dgvDetails, resources.GetString("dgvDetails.ToolTip"));
            this.dgvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetails_CellContentClick);
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_ProName
            // 
            this.col_ProName.DataPropertyName = "CombProName";
            resources.ApplyResources(this.col_ProName, "col_ProName");
            this.col_ProName.Name = "col_ProName";
            this.col_ProName.ReadOnly = true;
            // 
            // txtNameSrch
            // 
            resources.ApplyResources(this.txtNameSrch, "txtNameSrch");
            this.txtNameSrch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameSrch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNameSrch.Data = 0;
            this.txtNameSrch.EnterColor = System.Drawing.Color.LightPink;
            this.txtNameSrch.Format = null;
            this.txtNameSrch.isAllowNegative = false;
            this.txtNameSrch.isAllowSpecialChar = false;
            this.txtNameSrch.isNumberOnly = false;
            this.txtNameSrch.isNumeric = false;
            this.txtNameSrch.LeaveColor = System.Drawing.Color.White;
            this.txtNameSrch.Name = "txtNameSrch";
            this.toolTip.SetToolTip(this.txtNameSrch, resources.GetString("txtNameSrch.ToolTip"));
            this.txtNameSrch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNameSrch.TextChanged += new System.EventHandler(this.txtNameSrch_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            this.toolTip.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // lblQty
            // 
            resources.ApplyResources(this.lblQty, "lblQty");
            this.lblQty.Format = null;
            this.lblQty.Name = "lblQty";
            this.toolTip.SetToolTip(this.lblQty, resources.GetString("lblQty.ToolTip"));
            this.lblQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtBarcode
            // 
            resources.ApplyResources(this.txtBarcode, "txtBarcode");
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
            this.toolTip.SetToolTip(this.txtBarcode, resources.GetString("txtBarcode.ToolTip"));
            this.txtBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBarcode.Validated += new System.EventHandler(this.txtBarcode_Validated);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // cbInclusive
            // 
            resources.ApplyResources(this.cbInclusive, "cbInclusive");
            this.cbInclusive.Checked = true;
            this.cbInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInclusive.Name = "cbInclusive";
            this.toolTip.SetToolTip(this.cbInclusive, resources.GetString("cbInclusive.ToolTip"));
            this.cbInclusive.UseVisualStyleBackColor = true;
            // 
            // cbTranslate
            // 
            resources.ApplyResources(this.cbTranslate, "cbTranslate");
            this.cbTranslate.Name = "cbTranslate";
            this.cbTranslate.TabStop = false;
            this.toolTip.SetToolTip(this.cbTranslate, resources.GetString("cbTranslate.ToolTip"));
            this.cbTranslate.UseVisualStyleBackColor = true;
            // 
            // cbTransliterate
            // 
            resources.ApplyResources(this.cbTransliterate, "cbTransliterate");
            this.cbTransliterate.Checked = true;
            this.cbTransliterate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTransliterate.Name = "cbTransliterate";
            this.cbTransliterate.TabStop = false;
            this.toolTip.SetToolTip(this.cbTransliterate, resources.GetString("cbTransliterate.ToolTip"));
            this.cbTransliterate.UseVisualStyleBackColor = true;
            // 
            // txtLocalName
            // 
            resources.ApplyResources(this.txtLocalName, "txtLocalName");
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
            this.toolTip.SetToolTip(this.txtLocalName, resources.GetString("txtLocalName.ToolTip"));
            this.txtLocalName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLocalName.Validated += new System.EventHandler(this.txtBarcode_Validated);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // ProductCombinationView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbTranslate);
            this.Controls.Add(this.cbTransliterate);
            this.Controls.Add(this.cbInclusive);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPurRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtMRP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.txtProSaleRate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDate);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudVchrNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtLocalName);
            this.Controls.Add(this.grpSrch);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label8);
            this.Name = "ProductCombinationView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ProductCombinationView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductCombinationView_KeyDown);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtBarcode, 0);
            this.Controls.SetChildIndex(this.grpSrch, 0);
            this.Controls.SetChildIndex(this.txtLocalName, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.nudVchrNo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.btnDate, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.txtProSaleRate, 0);
            this.Controls.SetChildIndex(this.cmbUnit, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtMRP, 0);
            this.Controls.SetChildIndex(this.txtQty, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtPurRate, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.lblQty, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.cbInclusive, 0);
            this.Controls.SetChildIndex(this.cbTransliterate, 0);
            this.Controls.SetChildIndex(this.cbTranslate, 0);
            this.ContextMenustrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVchrNo)).EndInit();
            this.grpSrch.ResumeLayout(false);
            this.grpSrch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsProComb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
