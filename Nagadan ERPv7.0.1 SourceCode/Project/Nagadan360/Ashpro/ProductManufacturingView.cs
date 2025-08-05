using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ProductManufacturingView : BaseForm
	{
		private DBContext db;

		private DateTime DateValue;

		private List<ExManufactureDtls> entManufactureDtls;

		private int iMasterId;

		private DataGridViewTextBoxEditingControl textbox;

		private List<ExManufacturingExp> exManufacturingExps;

		private List<ExRawmaterialMapping> exRawmaterialMappings;

		private string sVoucherType = "Product Manufaturing";

		private IContainer components = null;

		private Button button4;

		private Label lblHead;

		private Button btnDateChange;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private CheckBox cbInvNo;

		private AshUpDown nudInvNo;

		private Label label1;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		public AshDataGridview dgvDetails;

		private Button button1;

		private Label label3;

		private AshTextBoxNormal txtRMCost;

		private Button btnRMCost;

		private Label label4;

		private AshTextBoxNormal txtManExpenses;

		private Button btnManExpenses;

		private Label label5;

		private AshTextBoxNormal txtTotalCost;

		private Label label6;

		private AshTextBoxNormal txtProfitP;

		private Label label7;

		private Label label8;

		private AshTextBoxNormal txtProfitAmount;

		private AshTextBoxNormal txtNetAmount;

		private BindingSource bdsDetails;

		private Label label9;

		private AshComboBox cmbGodown;

		private Label label10;

		private AshTextBoxNormal txtRemarks;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_UnitName;

		private DataGridViewTextBoxColumn col_PurRate;

		private DataGridViewTextBoxColumn col_SlsRate;

		private DataGridViewTextBoxColumn col_RawCost;

		private DataGridViewTextBoxColumn col_Expenses;

		private DataGridViewTextBoxColumn col_Profit;

		private DataGridViewTextBoxColumn col_FinalAmount;

		private DataGridViewTextBoxColumn col_NewPurRate;

		private DataGridViewTextBoxColumn col_NewSlsRate;

		private ToolTip toolTip;

		public UserPrevilegeDtl plgDtls { get; set; }

		public ProductManufacturingView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private ExManufactureDtls getCurrent()
		{
			try
			{
				if (dgvDetails.CurrentRow != null)
				{
					return (ExManufactureDtls)dgvDetails.CurrentRow.DataBoundItem;
				}
				return null;
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

		private void InitialLoadMethod()
		{
			nudInvNo.Value = ((from x in db.ManufactureHdrs()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((ManufactureHdr x) => x.InvNo) + 1) ?? 1;
			entManufactureDtls = new List<ExManufactureDtls>();
			exRawmaterialMappings = new List<ExRawmaterialMapping>();
			exManufacturingExps = new List<ExManufacturingExp>();
			BindDgv();
		}

		private void BindDgv()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entManufactureDtls;
			dgvDetails.DataSource = bdsDetails;
		}

		private void TotalCostFindMethod()
		{
			txtTotalCost.Value = txtRMCost.Value + txtManExpenses.Value;
			txtNetAmount.Value = txtTotalCost.Value + txtProfitAmount.Value;
		}

		private void ValuesAddMethod()
		{
			decimal a = entManufactureDtls.Sum((ExManufactureDtls x) => x.Quantity * (decimal?)x.PurchasePrice).ToDecimal();
			entManufactureDtls.ForEach(delegate(ExManufactureDtls x)
			{
				decimal? rawCost;
				if (!(txtRMCost.Value == 0m))
				{
					decimal value = txtRMCost.Value / a;
					decimal? num = x.Quantity * (decimal?)x.PurchasePrice;
					rawCost = (decimal?)value * num;
				}
				else
				{
					rawCost = default(decimal);
				}
				x.RawCost = rawCost;
				decimal? expenses;
				if (!(txtManExpenses.Value == 0m))
				{
					decimal value = txtManExpenses.Value / a;
					decimal? num2 = x.Quantity * (decimal?)x.PurchasePrice;
					expenses = (decimal?)value * num2;
				}
				else
				{
					expenses = default(decimal);
				}
				x.Expenses = expenses;
				decimal? profit;
				if (!(txtProfitAmount.Value == 0m))
				{
					decimal value = txtProfitAmount.Value / a;
					decimal? num3 = x.Quantity * (decimal?)x.PurchasePrice;
					profit = (decimal?)value * num3;
				}
				else
				{
					profit = default(decimal);
				}
				x.Profit = profit;
				x.FinalCost = x.RawCost + x.Expenses;
				x.FinalAmount = x.FinalCost + x.Profit;
				x.FinalCost = x.RawCost + x.Expenses;
				decimal? quantity = x.Quantity;
				if ((quantity.GetValueOrDefault() > default(decimal)) & quantity.HasValue)
				{
					quantity = x.FinalAmount;
					if ((quantity.GetValueOrDefault() > default(decimal)) & quantity.HasValue)
					{
						x.NewPurchaseRate = x.FinalCost / x.Quantity;
						x.NewSalesRate = x.FinalAmount / x.Quantity;
					}
				}
			});
			dgvDetails.Refresh();
		}

		private ManufactureHdr HdrEntityLoadMethod(int iInvNo)
		{
			return new ManufactureHdr
			{
				Id = iMasterId,
				InvNo = iInvNo,
				FK_GodownId = cmbGodown.SelectedValue.ToInt32(),
				FK_UserId = Settings.Default.intLogOnId,
				Date = dtmDate.Value.Date,
				EntryDate = DateTime.Now.Date,
				Remarks = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void SaveSubMethod(string sNarration, string sVType, List<ManufactureDtls> manufactureDtls, int iInvNo, int iMHdrId)
		{
			List<ManufacturingExp> list = new List<ManufacturingExp>();
			List<RawmaterialMappingEntity> list2 = new List<RawmaterialMappingEntity>();
			list2 = AniHelper.CopyListData<RawmaterialMappingEntity>(exRawmaterialMappings.Cast<object>().ToList());
			list = AniHelper.CopyListData<ManufacturingExp>(exManufacturingExps.Cast<object>().ToList());
			if (!AshproCommon.InsertToDatabase(manufactureDtls.Cast<object>().ToList()))
			{
				return;
			}
			list.ForEach(delegate(ManufacturingExp x)
			{
				x.Id = null;
				x.FK_ManufactureHdrId = iMHdrId;
			});
			AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "ManufacturingExp");
			list2.ForEach(delegate(RawmaterialMappingEntity x)
			{
				x.Id = null;
				x.FK_ManufactureHdrId = iMHdrId;
			});
			AshproCommon.InsertToDatabase(list2.Cast<object>().ToList(), "RawmaterialMapping");
			foreach (ManufactureDtls manufactureDtl in manufactureDtls)
			{
				POSHelper.STranLogMethod(iMHdrId, iInvNo, sVoucherType, sVoucherType, -1, sNarration, Settings.Default.intLogOnId, manufactureDtl.Quantity.ToDecimal(), 0m, manufactureDtl.FK_ProductId.toInt32(), manufactureDtl.FK_UnitId.toInt32(), cmbGodown.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value));
			}
			PriceCategoryEntity priceCategory = db.PriceCategories().FirstOrDefault();
			foreach (ManufactureDtls item2 in manufactureDtls)
			{
				ProductEntity productEntity = db.Products().Single((ProductEntity x) => x.ProductId == item2.FK_ProductId);
				productEntity.PurchasePrice = item2.NewPurchaseRate.ToDecimal();
				productEntity.SellingPrice = item2.NewSalesRate.ToDecimal();
				AshproCommon.UpdateToDatabaseObj(productEntity, "Product", "ProductId", item2.FK_ProductId.toInt32());
				ProductPriceCategory productPriceCategory = db.ProductPriceCategories().Single((ProductPriceCategory x) => x.ProductId == item2.FK_ProductId && x.UnitId == item2.FK_UnitId && x.PCategoryId == priceCategory.PCategoryId);
				productPriceCategory.PPrice = item2.NewPurchaseRate.ToDecimal();
				productPriceCategory.SPrice = item2.NewSalesRate.ToDecimal();
				AshproCommon.UpdateToDatabaseObj(productPriceCategory, "ProductPriceCategory", "ProPrceCatId", productPriceCategory.ProPrceCatId.toInt32());
			}
			foreach (ExRawmaterialMapping exRawmaterialMapping in exRawmaterialMappings)
			{
				POSHelper.STranLogMethod(iMHdrId, iInvNo, sVoucherType, sVoucherType, -1, sNarration, Settings.Default.intLogOnId, 0m, exRawmaterialMapping.Quantity, exRawmaterialMapping.FK_ProductId.toInt32(), exRawmaterialMapping.FK_UnitId.toInt32(), cmbGodown.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value));
			}
			int intLogOnId = Settings.Default.intLogOnId;
			foreach (ExManufacturingExp item in exManufacturingExps)
			{
				int iGrpId = db.Accounts().Single((AccountEntity x) => x.AccountId == item.FK_PaymentAccountId).AGroupId.ToInt32();
				string sVchrName = ((db.AccountGroup().Single((AccountGroupEntity x) => x.AGroupId == iGrpId).GroupName == "CASH IN HAND") ? "Cash Payment" : "Bank Payment");
				int num = ((from x in db.VchrTransaction()
					where x.VchrName == sVchrName && x.VchrType == sVType
					select x).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				POSHelper.VoucherTranLog(num, item.Date, item.Amount.ToDecimal(), sVType, sNarration, item.FK_PaymentAccountId.toInt32(), item.FK_ExpenseAccountId.toInt32(), intLogOnId, intLogOnId, sVchrName);
				int iVoucherID = db.VchrTransaction().Single((VchrTranLog x) => x.Narration == sNarration).VchrTranId.toInt32();
				POSHelper.TranLogMethod(0m, item.Amount.ToDecimal(), item.FK_PaymentAccountId.toInt32(), item.FK_ExpenseAccountId.toInt32(), iVoucherID, num, sVType, intLogOnId, "Main", null, AniHelper.GetDate(item.Date));
				POSHelper.TranLogMethod(item.Amount.ToDecimal(), 0m, item.FK_ExpenseAccountId.toInt32(), item.FK_PaymentAccountId.toInt32(), iVoucherID, num, sVType, intLogOnId, "Sub", null, AniHelper.GetDate(item.Date));
			}
		}

		private void DeleteSubMethod(string sNarration, string sVType, int iVchrId)
		{
			foreach (TransactionLog item in (from x in db.TransactionLogs()
				where x.VchrId == iVchrId && x.VchrType == sVType
				select x).ToList())
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item.TranId.toInt32());
			}
			AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", iVchrId);
			foreach (StockTranLog item2 in (from x in db.StockTranLogs()
				where x.Narration == sNarration
				select x).ToList())
			{
				AshproCommon.DeleteFromDatabase("StockTranLog", "STranId", item2.STranId.toInt32());
			}
			AshproCommon.DeleteFromDatabase("ManufactureDtls", "FK_ManufactureHdrId", iMasterId);
			AshproCommon.DeleteFromDatabase("RawmaterialMapping", "FK_ManufactureHdrId", iMasterId);
			AshproCommon.DeleteFromDatabase("ManufacturingExp", "FK_ManufactureHdrId", iMasterId);
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += Textbox_KeyPress;
					textbox.KeyUp += Textbox_KeyUp;
				}
			}
		}

		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				textbox = (DataGridViewTextBoxEditingControl)sender;
				ExManufactureDtls t = getCurrent();
				_ = dgvDetails.CurrentCell.RowIndex;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
					{
						e.Handled = true;
						SelectCustomer selectCustomer = new SelectCustomer("Finished_Good");
						selectCustomer.sName = textbox.Text;
						if (selectCustomer.ShowDialog() == DialogResult.OK)
						{
							dgvDetails.CurrentCell.Value = selectCustomer.sName;
							ProductEntity product = db.Products(new
							{
								ProductId = selectCustomer.iId
							}).FirstOrDefault();
							ExManufactureDtlsLoadMethod(t, product);
							ValuesAddMethod();
							dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[col_Qty.Name];
						}
					}
				}
				else
				{
					if (!(dgvDetails.CurrentCell.OwningColumn.Name == col_UnitName.Name))
					{
						return;
					}
					SelectCustomer frm = new SelectCustomer("Unit");
					frm.sName = textbox.Text;
					frm.ProductId = t.FK_ProductId.toInt32();
					if (frm.ShowDialog() == DialogResult.OK)
					{
						dgvDetails.CurrentCell.Value = frm.sName;
						t.PurchasePrice = (from x in db.Productunits()
							where x.UnitId == frm.iId && x.ProductId == t.FK_ProductId
							select x).FirstOrDefault().UnitQty * t.PurchasePrice;
						t.SalesePrice = (from x in db.Productunits()
							where x.UnitId == frm.iId && x.ProductId == t.FK_ProductId
							select x).FirstOrDefault().UnitQty * t.SalesePrice;
						ValuesAddMethod();
						dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[6];
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ExManufactureDtlsLoadMethod(ExManufactureDtls t, ProductEntity product)
		{
			if (product != null)
			{
				t.SL = dgvDetails.Rows.Count - 1;
				t.FK_ProductId = product.ProductId.ToInt32();
				t.Barcode = product.Barcode;
				t.ProductCode = product.ProductCode.toInt32();
				t.Quantity = t.Quantity ?? 1m;
				t.FK_UnitId = product.UnitId;
				t.UnitName = db.Units().Single((UnitEntity x) => x.UnitId == product.UnitId).UnitName;
				t.PurchasePrice = product.PurchasePrice;
				t.SalesePrice = product.SellingPrice;
			}
		}

		private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar.ToString();
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name || dgvDetails.CurrentCell.OwningColumn.Name == col_RawCost.Name || dgvDetails.CurrentCell.OwningColumn.Name == col_Expenses.Name || dgvDetails.CurrentCell.OwningColumn.Name == col_Profit.Name)
				{
					if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && !(sender as TextBox).Text.Contains('.')) || (e.KeyChar == '.' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) || (e.KeyChar == '-' && !(sender as TextBox).Text.Contains('-')) || (e.KeyChar == '-' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength))
					{
						return;
					}
					e.Handled = true;
				}
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProCode.Name)
				{
					dgvDetails.NotifyCurrentCellDirty(dirty: true);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ProductManufacturingView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			DateValue = Settings.Default.appDate;
			dtmDate.Value = DateValue;
			AniHelper.FillGodown(cmbGodown);
			InitialLoadMethod();
		}

		private void btnRMCost_Click(object sender, EventArgs e)
		{
			RawmaterialMapping rawmaterialMapping = new RawmaterialMapping(exRawmaterialMappings);
			if (rawmaterialMapping.ShowDialog() == DialogResult.OK)
			{
				txtRMCost.Value = Math.Round(exRawmaterialMappings.Sum((ExRawmaterialMapping x) => x.NetAmount).GetValueOrDefault(), 0);
				TotalCostFindMethod();
				ValuesAddMethod();
			}
		}

		private void btnManExpenses_Click(object sender, EventArgs e)
		{
			ExpenseMapping expenseMapping = new ExpenseMapping(exManufacturingExps);
			if (expenseMapping.ShowDialog() == DialogResult.OK)
			{
				txtManExpenses.Value = exManufacturingExps.Sum((ExManufacturingExp x) => x.Amount).GetValueOrDefault();
				TotalCostFindMethod();
				ValuesAddMethod();
			}
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExManufactureDtls t = (ExManufactureDtls)dgvDetails.CurrentRow.DataBoundItem;
			if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				decimal a4 = entManufactureDtls.Sum((ExManufactureDtls y) => y.Quantity * (decimal?)y.PurchasePrice).ToDecimal();
				entManufactureDtls.ForEach(delegate(ExManufactureDtls x)
				{
					decimal? rawCost3;
					if (!(txtRMCost.Value == 0m))
					{
						decimal value4 = txtRMCost.Value / a4;
						decimal? num = x.Quantity * (decimal?)x.PurchasePrice;
						rawCost3 = (decimal?)value4 * num;
					}
					else
					{
						rawCost3 = default(decimal);
					}
					x.RawCost = rawCost3;
					decimal? expenses3;
					if (!(txtManExpenses.Value == 0m))
					{
						decimal value4 = txtManExpenses.Value / a4;
						decimal? num2 = x.Quantity * (decimal?)x.PurchasePrice;
						expenses3 = (decimal?)value4 * num2;
					}
					else
					{
						expenses3 = default(decimal);
					}
					x.Expenses = expenses3;
					decimal? profit3;
					if (!(txtProfitAmount.Value == 0m))
					{
						decimal value4 = txtProfitAmount.Value / a4;
						decimal? num3 = x.Quantity * (decimal?)x.PurchasePrice;
						profit3 = (decimal?)value4 * num3;
					}
					else
					{
						profit3 = default(decimal);
					}
					x.Profit = profit3;
					x.FinalCost = x.RawCost + x.Expenses;
					x.FinalAmount = x.FinalCost + x.Profit;
					decimal? quantity4 = x.Quantity;
					if ((quantity4.GetValueOrDefault() > default(decimal)) & quantity4.HasValue)
					{
						quantity4 = x.FinalAmount;
						if ((quantity4.GetValueOrDefault() > default(decimal)) & quantity4.HasValue)
						{
							x.NewPurchaseRate = x.FinalCost / x.Quantity;
							x.NewSalesRate = x.FinalAmount / x.Quantity;
						}
					}
				});
				dgvDetails.Refresh();
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductEntity product = db.Products(new { t.Barcode }).FirstOrDefault();
				ExManufactureDtlsLoadMethod(t, product);
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_RawCost.Name)
			{
				decimal a3 = entManufactureDtls.Sum((ExManufactureDtls y) => y.Quantity * (decimal?)y.PurchasePrice).ToDecimal() - t.Quantity.ToDecimal() * t.PurchasePrice;
				entManufactureDtls.ForEach(delegate(ExManufactureDtls x)
				{
					if (x.FK_ProductId != t.FK_ProductId)
					{
						decimal? rawCost2;
						if (!(txtRMCost.Value == 0m))
						{
							decimal value3 = txtRMCost.Value;
							decimal? rawCost = t.RawCost;
							rawCost2 = ((decimal?)value3 - rawCost) / (decimal?)a3 * (x.Quantity * (decimal?)x.PurchasePrice);
						}
						else
						{
							rawCost2 = default(decimal);
						}
						x.RawCost = rawCost2;
					}
					x.FinalCost = x.RawCost + x.Expenses;
					x.FinalAmount = x.FinalCost + x.Profit;
					decimal? quantity3 = x.Quantity;
					if ((quantity3.GetValueOrDefault() > default(decimal)) & quantity3.HasValue)
					{
						quantity3 = x.FinalAmount;
						if ((quantity3.GetValueOrDefault() > default(decimal)) & quantity3.HasValue)
						{
							x.NewPurchaseRate = x.FinalCost / x.Quantity;
							x.NewSalesRate = x.FinalAmount / x.Quantity;
						}
					}
				});
				dgvDetails.Refresh();
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Expenses.Name)
			{
				decimal a2 = entManufactureDtls.Sum((ExManufactureDtls y) => y.Quantity * (decimal?)y.PurchasePrice).ToDecimal() - t.Quantity.ToDecimal() * t.PurchasePrice;
				entManufactureDtls.ForEach(delegate(ExManufactureDtls x)
				{
					if (x.FK_ProductId != t.FK_ProductId)
					{
						decimal? expenses2;
						if (!(txtManExpenses.Value == 0m))
						{
							decimal value2 = txtManExpenses.Value;
							decimal? expenses = t.Expenses;
							expenses2 = ((decimal?)value2 - expenses) / (decimal?)a2 * (x.Quantity * (decimal?)x.PurchasePrice);
						}
						else
						{
							expenses2 = default(decimal);
						}
						x.Expenses = expenses2;
					}
					x.FinalCost = x.RawCost + x.Expenses;
					x.FinalAmount = x.FinalCost + x.Profit;
					decimal? quantity2 = x.Quantity;
					if ((quantity2.GetValueOrDefault() > default(decimal)) & quantity2.HasValue)
					{
						quantity2 = x.FinalAmount;
						if ((quantity2.GetValueOrDefault() > default(decimal)) & quantity2.HasValue)
						{
							x.NewPurchaseRate = x.FinalCost / x.Quantity;
							x.NewSalesRate = x.FinalAmount / x.Quantity;
						}
					}
				});
				dgvDetails.Refresh();
			}
			else
			{
				if (!(dgvDetails.CurrentCell.OwningColumn.Name == col_Profit.Name))
				{
					return;
				}
				decimal a = entManufactureDtls.Sum((ExManufactureDtls y) => y.Quantity * (decimal?)y.PurchasePrice).ToDecimal() - t.Quantity.ToDecimal() * t.PurchasePrice;
				entManufactureDtls.ForEach(delegate(ExManufactureDtls x)
				{
					if (x.FK_ProductId != t.FK_ProductId)
					{
						decimal? profit2;
						if (!(txtProfitAmount.Value == 0m))
						{
							decimal value = txtProfitAmount.Value;
							decimal? profit = t.Profit;
							profit2 = ((decimal?)value - profit) / (decimal?)a * (x.Quantity * (decimal?)x.PurchasePrice);
						}
						else
						{
							profit2 = default(decimal);
						}
						x.Profit = profit2;
					}
					x.FinalCost = x.RawCost + x.Expenses;
					x.FinalAmount = x.FinalCost + x.Profit;
					decimal? quantity = x.Quantity;
					if ((quantity.GetValueOrDefault() > default(decimal)) & quantity.HasValue)
					{
						quantity = x.FinalAmount;
						if ((quantity.GetValueOrDefault() > default(decimal)) & quantity.HasValue)
						{
							x.NewPurchaseRate = x.FinalCost / x.Quantity;
							x.NewSalesRate = x.FinalAmount / x.Quantity;
						}
					}
				});
				dgvDetails.Refresh();
			}
		}

		private void txtProfitP_Validated(object sender, EventArgs e)
		{
			txtProfitAmount.Value = txtTotalCost.Value * (txtProfitP.Value / 100m);
			txtNetAmount.Value = txtProfitAmount.Value + txtTotalCost.Value;
			ValuesAddMethod();
		}

		private void txtProfitAmount_Validated(object sender, EventArgs e)
		{
			txtProfitP.Value = txtProfitAmount.Value / txtTotalCost.Value * 100m;
			txtNetAmount.Value = txtProfitAmount.Value + txtTotalCost.Value;
			ValuesAddMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			dtmDate.Value = DateValue;
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			InitialLoadMethod();
			dtmDate.Focus();
		}

		private void btnDateChange_Click(object sender, EventArgs e)
		{
			DateValue = dtmDate.Value;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (Messages.DeleteConfirmationMessage())
			{
				string sNarration = sVoucherType + " = " + nudInvNo.Value;
				int iVchrId = db.VchrTransaction().Single((VchrTranLog x) => x.Narration == sNarration).VchrTranId.toInt32();
				string sVType = "Payment Voucher";
				DeleteSubMethod(sNarration, sVType, iVchrId);
				AshproCommon.DeleteFromDatabase("ManufactureHdr", "Id", iMasterId);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (entManufactureDtls.Count == 0)
			{
				Messages.InformationMessage("Please enter product and try again.");
				return;
			}
			string sNarration = sVoucherType;
			string sVType = "Payment Voucher";
			List<ManufactureDtls> list = new List<ManufactureDtls>();
			list = AniHelper.CopyListData<ManufactureDtls>(entManufactureDtls.Cast<object>().ToList());
			if (btnSave.Text == "Save")
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				int iInvNo = ((from x in db.ManufactureHdrs()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((ManufactureHdr x) => x.InvNo) + 1) ?? 1;
				sNarration = sVoucherType + " = " + iInvNo;
				if (AshproCommon.InsertToDatabaseObj(HdrEntityLoadMethod(iInvNo)))
				{
					int iMHdrId = db.ManufactureHdrs().FirstOrDefault((ManufactureHdr y) => y.InvNo == iInvNo && y.FK_BranchId == Settings.Default.iBranchId).Id.toInt32();
					list.ForEach(delegate(ManufactureDtls x)
					{
						x.Id = null;
						x.FK_ManufactureHdrId = iMHdrId;
					});
					SaveSubMethod(sNarration, sVType, list, iInvNo, iMHdrId);
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
				sNarration = sVoucherType + " = " + nudInvNo.Value;
				int iVchrId = db.VchrTransaction().Single((VchrTranLog x) => x.Narration == sNarration).VchrTranId.toInt32();
				DeleteSubMethod(sNarration, sVType, iVchrId);
				if (AshproCommon.UpdateToDatabaseObj(HdrEntityLoadMethod(nudInvNo.Value.ToInt32()), "ManufactureHdr", "Id", iMasterId))
				{
					list.ForEach(delegate(ManufactureDtls x)
					{
						x.Id = null;
						x.FK_ManufactureHdrId = iMasterId;
					});
					SaveSubMethod(sNarration, sVType, list, nudInvNo.Value.ToInt32(), iMasterId);
					Messages.UpdateMessage();
				}
			}
			btnClear_Click(null, null);
		}

		private void nudInvNo_ValueChanged(object sender, EventArgs e)
		{
			ManufactureHdr manufactureHdr = (from x in db.ManufactureHdrs()
				where x.InvNo == nudInvNo.Value.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
				select x).FirstOrDefault();
			if (manufactureHdr != null)
			{
				LoadManufacturingDetails(manufactureHdr);
			}
			else
			{
				btnClear_Click(null, null);
			}
		}

		private void LoadManufacturingDetails(ManufactureHdr mHdr)
		{
			iMasterId = mHdr.Id.ToInt32();
			dtmDate.Value = mHdr.Date;
			cmbGodown.SelectedValue = mHdr.FK_GodownId.GetValueOrDefault();
			txtRemarks.Text = mHdr.Remarks;
			List<ManufactureDtls> list = new List<ManufactureDtls>();
			list = (from x in db.ManufactureDtls()
				where x.FK_ManufactureHdrId == iMasterId
				select x).ToList();
			entManufactureDtls = AniHelper.CopyListData<ExManufactureDtls>(list.Cast<object>().ToList());
			int num = 1;
			foreach (ExManufactureDtls x3 in entManufactureDtls)
			{
				x3.SL = num;
				ProductEntity productEntity = db.Products().Single((ProductEntity y) => y.ProductId == x3.FK_ProductId);
				x3.ProductCode = productEntity.ProductCode.toInt32();
				x3.ProductName = productEntity.ProductName;
				x3.UnitName = db.Units().Single((UnitEntity y) => y.UnitId == x3.FK_UnitId).UnitName;
				x3.FinalCost = x3.FinalAmount - x3.Profit;
				num++;
			}
			exManufacturingExps = AniHelper.CopyListData<ExManufacturingExp>((from x in db.ManufacturingExps()
				where x.FK_ManufactureHdrId == iMasterId
				select x).Cast<object>().ToList());
			num = 1;
			foreach (ExManufacturingExp exManufacturingExp in exManufacturingExps)
			{
				exManufacturingExp.SL = num;
				num++;
			}
			exRawmaterialMappings = AniHelper.CopyListData<ExRawmaterialMapping>((from x in db.RawmaterialMappings()
				where x.FK_ManufactureHdrId == iMasterId
				select x).Cast<object>().ToList());
			num = 1;
			foreach (ExRawmaterialMapping x2 in exRawmaterialMappings)
			{
				x2.SL = num;
				ProductEntity productEntity2 = db.Products().Single((ProductEntity y) => y.ProductId == x2.FK_ProductId);
				x2.ProCode = productEntity2.ProductCode.toInt32();
				x2.ProductName = productEntity2.ProductName;
				x2.UnitName = db.Units().Single((UnitEntity y) => y.UnitId == x2.FK_UnitId).UnitName;
				num++;
			}
			txtManExpenses.Value = exManufacturingExps.Sum((ExManufacturingExp x) => x.Amount).ToDecimal();
			txtRMCost.Value = exRawmaterialMappings.Sum((ExRawmaterialMapping x) => x.NetAmount).ToDecimal();
			txtTotalCost.Value = txtManExpenses.Value + txtRMCost.Value;
			txtProfitAmount.Value = entManufactureDtls.Sum((ExManufactureDtls x) => x.Profit).ToDecimal();
			txtProfitAmount_Validated(null, null);
			txtNetAmount.Value = txtTotalCost.Value + txtProfitAmount.Value;
			BindDgv();
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			btnEdit.Enabled = true;
			btnEnd.Enabled = true;
			btnClear.Enabled = true;
			cbInvNo.Enabled = true;
			nudInvNo.Enabled = cbInvNo.Checked;
			btnDelete.Enabled = true;
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			btnSave.Text = "Update";
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
		}

		private void cbInvNo_CheckedChanged(object sender, EventArgs e)
		{
			nudInvNo.Enabled = cbInvNo.Checked;
		}

		private void ProductManufacturingView_KeyDown(object sender, KeyEventArgs e)
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
				btnDateChange_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnRMCost_Click(null, null);
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnManExpenses_Click(null, null);
			}
			else
			{
				e.SuppressKeyPress = false;
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			btnDateChange = new System.Windows.Forms.Button();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			cbInvNo = new System.Windows.Forms.CheckBox();
			nudInvNo = new AshUsercontrol.AshUpDown();
			label1 = new System.Windows.Forms.Label();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			dgvDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SlsRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_RawCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Expenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_FinalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NewPurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NewSlsRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			button1 = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			txtRMCost = new AshUsercontrol.AshTextBoxNormal();
			btnRMCost = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			txtManExpenses = new AshUsercontrol.AshTextBoxNormal();
			btnManExpenses = new System.Windows.Forms.Button();
			label5 = new System.Windows.Forms.Label();
			txtTotalCost = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			txtProfitP = new AshUsercontrol.AshTextBoxNormal();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			txtProfitAmount = new AshUsercontrol.AshTextBoxNormal();
			txtNetAmount = new AshUsercontrol.AshTextBoxNormal();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			label9 = new System.Windows.Forms.Label();
			cmbGodown = new AshUsercontrol.AshComboBox();
			label10 = new System.Windows.Forms.Label();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			toolTip = new System.Windows.Forms.ToolTip(components);
			((System.ComponentModel.ISupportInitialize)nudInvNo).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(875, 1);
			btnEnd.TabIndex = 26;
			btnEnd.TabStop = true;
			button4.Location = new System.Drawing.Point(-3, 31);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(136, 1);
			button4.TabIndex = 28;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 7);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(142, 24);
			lblHead.TabIndex = 27;
			lblHead.Text = "Manufacturing";
			btnDateChange.Location = new System.Drawing.Point(660, 12);
			btnDateChange.Name = "btnDateChange";
			btnDateChange.Size = new System.Drawing.Size(22, 23);
			btnDateChange.TabIndex = 5;
			btnDateChange.Text = "...";
			toolTip.SetToolTip(btnDateChange, "Press Alt + T");
			btnDateChange.UseVisualStyleBackColor = true;
			btnDateChange.Click += new System.EventHandler(btnDateChange_Click);
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(530, 13);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 4;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(480, 19);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(50, 16);
			label2.TabIndex = 3;
			label2.Text = "Date :";
			cbInvNo.AutoSize = true;
			cbInvNo.Location = new System.Drawing.Point(453, 18);
			cbInvNo.Name = "cbInvNo";
			cbInvNo.Size = new System.Drawing.Size(15, 14);
			cbInvNo.TabIndex = 2;
			cbInvNo.UseVisualStyleBackColor = true;
			cbInvNo.CheckedChanged += new System.EventHandler(cbInvNo_CheckedChanged);
			nudInvNo.Enabled = false;
			nudInvNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			nudInvNo.Location = new System.Drawing.Point(340, 15);
			nudInvNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudInvNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudInvNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.Name = "nudInvNo";
			nudInvNo.Size = new System.Drawing.Size(106, 23);
			nudInvNo.TabIndex = 1;
			nudInvNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudInvNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.ValueChanged += new System.EventHandler(nudInvNo_ValueChanged);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(248, 17);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 16);
			label1.TabIndex = 0;
			label1.Text = "Invoice No :";
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(823, 398);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 25;
			btnClear.Text = "Clear";
			toolTip.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(746, 398);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 24;
			btnDelete.Text = "Delete";
			toolTip.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.Enabled = false;
			btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Location = new System.Drawing.Point(669, 398);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new System.Drawing.Size(75, 30);
			btnEdit.TabIndex = 23;
			btnEdit.Text = "Edit";
			toolTip.SetToolTip(btnEdit, "Press Alt + E");
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(592, 398);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 22;
			btnSave.Text = "Save";
			toolTip.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			dgvDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_Barcode, col_ProCode, col_ProName, col_Qty, col_UnitName, col_PurRate, col_SlsRate, col_RawCost, col_Expenses, col_Profit, col_FinalAmount, col_NewPurRate, col_NewSlsRate);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.EnterKeyNavigation = false;
			dgvDetails.LastKey = System.Windows.Forms.Keys.None;
			dgvDetails.Location = new System.Drawing.Point(17, 89);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.Size = new System.Drawing.Size(870, 187);
			dgvDetails.TabIndex = 0;
			dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellEndEdit);
			dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetails_DataError);
			dgvDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetails_EditingControlShowing);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 35;
			col_Barcode.DataPropertyName = "Barcode";
			col_Barcode.HeaderText = "Barcode";
			col_Barcode.Name = "col_Barcode";
			col_ProCode.DataPropertyName = "ProductCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProCode.Visible = false;
			col_ProCode.Width = 70;
			col_ProName.DataPropertyName = "ProductName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.Width = 200;
			col_Qty.DataPropertyName = "Quantity";
			col_Qty.HeaderText = "Qty";
			col_Qty.Name = "col_Qty";
			col_Qty.Width = 60;
			col_UnitName.DataPropertyName = "UnitName";
			col_UnitName.HeaderText = "Unit";
			col_UnitName.Name = "col_UnitName";
			col_UnitName.Width = 60;
			col_PurRate.DataPropertyName = "PurchasePrice";
			col_PurRate.HeaderText = "Pur Rate";
			col_PurRate.Name = "col_PurRate";
			col_PurRate.ReadOnly = true;
			col_PurRate.Width = 75;
			col_SlsRate.DataPropertyName = "SalesePrice";
			col_SlsRate.HeaderText = "Sales Rate";
			col_SlsRate.Name = "col_SlsRate";
			col_RawCost.DataPropertyName = "RawCost";
			col_RawCost.HeaderText = "Raw Cost";
			col_RawCost.Name = "col_RawCost";
			col_RawCost.Width = 80;
			col_Expenses.DataPropertyName = "Expenses";
			col_Expenses.HeaderText = "Expenses";
			col_Expenses.Name = "col_Expenses";
			col_Expenses.Width = 80;
			col_Profit.DataPropertyName = "Profit";
			col_Profit.HeaderText = "Profit";
			col_Profit.Name = "col_Profit";
			col_Profit.Width = 80;
			col_FinalAmount.DataPropertyName = "FinalAmount";
			col_FinalAmount.HeaderText = "Total Amount";
			col_FinalAmount.Name = "col_FinalAmount";
			col_FinalAmount.ReadOnly = true;
			col_FinalAmount.Width = 80;
			col_NewPurRate.DataPropertyName = "NewPurchaseRate";
			col_NewPurRate.HeaderText = "New Pur Rate";
			col_NewPurRate.Name = "col_NewPurRate";
			col_NewPurRate.ReadOnly = true;
			col_NewPurRate.Width = 80;
			col_NewSlsRate.DataPropertyName = "NewSalesRate";
			col_NewSlsRate.HeaderText = "New Sales Rate";
			col_NewSlsRate.Name = "col_NewSlsRate";
			button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			button1.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			button1.Location = new System.Drawing.Point(485, 320);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(2, 72);
			button1.TabIndex = 15;
			button1.UseVisualStyleBackColor = true;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(119, 323);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(138, 16);
			label3.TabIndex = 7;
			label3.Text = "Raw Material Cost :";
			txtRMCost.BackColor = System.Drawing.Color.White;
			txtRMCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRMCost.Data = 0;
			txtRMCost.EnterColor = System.Drawing.Color.LightPink;
			txtRMCost.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtRMCost.Format = null;
			txtRMCost.isAllowNegative = false;
			txtRMCost.isAllowSpecialChar = false;
			txtRMCost.isNumberOnly = false;
			txtRMCost.isNumeric = true;
			txtRMCost.LeaveColor = System.Drawing.Color.White;
			txtRMCost.Location = new System.Drawing.Point(263, 323);
			txtRMCost.Margin = new System.Windows.Forms.Padding(4);
			txtRMCost.Name = "txtRMCost";
			txtRMCost.ReadOnly = true;
			txtRMCost.Size = new System.Drawing.Size(155, 16);
			txtRMCost.TabIndex = 8;
			txtRMCost.TabStop = false;
			txtRMCost.Text = "0";
			txtRMCost.Value = new decimal(new int[4]);
			txtRMCost.WaterMark = null;
			btnRMCost.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnRMCost.Location = new System.Drawing.Point(428, 319);
			btnRMCost.Name = "btnRMCost";
			btnRMCost.Size = new System.Drawing.Size(43, 23);
			btnRMCost.TabIndex = 9;
			btnRMCost.TabStop = false;
			btnRMCost.Text = "..";
			toolTip.SetToolTip(btnRMCost, "Press Alt + R");
			btnRMCost.UseVisualStyleBackColor = true;
			btnRMCost.Click += new System.EventHandler(btnRMCost_Click);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(78, 347);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(179, 16);
			label4.TabIndex = 10;
			label4.Text = "Manufacturing Expenses :";
			txtManExpenses.BackColor = System.Drawing.Color.White;
			txtManExpenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtManExpenses.Data = 0;
			txtManExpenses.EnterColor = System.Drawing.Color.LightPink;
			txtManExpenses.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtManExpenses.Format = null;
			txtManExpenses.isAllowNegative = false;
			txtManExpenses.isAllowSpecialChar = false;
			txtManExpenses.isNumberOnly = false;
			txtManExpenses.isNumeric = true;
			txtManExpenses.LeaveColor = System.Drawing.Color.White;
			txtManExpenses.Location = new System.Drawing.Point(263, 347);
			txtManExpenses.Margin = new System.Windows.Forms.Padding(4);
			txtManExpenses.Name = "txtManExpenses";
			txtManExpenses.ReadOnly = true;
			txtManExpenses.Size = new System.Drawing.Size(155, 16);
			txtManExpenses.TabIndex = 11;
			txtManExpenses.TabStop = false;
			txtManExpenses.Text = "0";
			txtManExpenses.Value = new decimal(new int[4]);
			txtManExpenses.WaterMark = null;
			btnManExpenses.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnManExpenses.Location = new System.Drawing.Point(428, 343);
			btnManExpenses.Name = "btnManExpenses";
			btnManExpenses.Size = new System.Drawing.Size(43, 23);
			btnManExpenses.TabIndex = 12;
			btnManExpenses.TabStop = false;
			btnManExpenses.Text = "..";
			toolTip.SetToolTip(btnManExpenses, "Press Alt + M");
			btnManExpenses.UseVisualStyleBackColor = true;
			btnManExpenses.Click += new System.EventHandler(btnManExpenses_Click);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(169, 371);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(88, 16);
			label5.TabIndex = 13;
			label5.Text = "Total Cost :";
			txtTotalCost.BackColor = System.Drawing.Color.White;
			txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtTotalCost.Data = 0;
			txtTotalCost.EnterColor = System.Drawing.Color.LightPink;
			txtTotalCost.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtTotalCost.Format = null;
			txtTotalCost.isAllowNegative = false;
			txtTotalCost.isAllowSpecialChar = false;
			txtTotalCost.isNumberOnly = false;
			txtTotalCost.isNumeric = true;
			txtTotalCost.LeaveColor = System.Drawing.Color.White;
			txtTotalCost.Location = new System.Drawing.Point(263, 371);
			txtTotalCost.Margin = new System.Windows.Forms.Padding(4);
			txtTotalCost.Name = "txtTotalCost";
			txtTotalCost.ReadOnly = true;
			txtTotalCost.Size = new System.Drawing.Size(155, 16);
			txtTotalCost.TabIndex = 14;
			txtTotalCost.TabStop = false;
			txtTotalCost.Text = "0";
			txtTotalCost.Value = new decimal(new int[4]);
			txtTotalCost.WaterMark = null;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(652, 318);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(84, 16);
			label6.TabIndex = 16;
			label6.Text = "Profit (%) :";
			txtProfitP.BackColor = System.Drawing.Color.White;
			txtProfitP.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProfitP.Data = 0;
			txtProfitP.EnterColor = System.Drawing.Color.LightPink;
			txtProfitP.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProfitP.Format = null;
			txtProfitP.isAllowNegative = false;
			txtProfitP.isAllowSpecialChar = false;
			txtProfitP.isNumberOnly = false;
			txtProfitP.isNumeric = true;
			txtProfitP.LeaveColor = System.Drawing.Color.White;
			txtProfitP.Location = new System.Drawing.Point(741, 318);
			txtProfitP.Margin = new System.Windows.Forms.Padding(4);
			txtProfitP.Name = "txtProfitP";
			txtProfitP.Size = new System.Drawing.Size(155, 16);
			txtProfitP.TabIndex = 17;
			txtProfitP.Text = "0";
			txtProfitP.Value = new decimal(new int[4]);
			txtProfitP.WaterMark = null;
			txtProfitP.Validated += new System.EventHandler(txtProfitP_Validated);
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(627, 342);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(109, 16);
			label7.TabIndex = 18;
			label7.Text = "Profit Amount :";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(639, 366);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(97, 16);
			label8.TabIndex = 20;
			label8.Text = "Net Amount :";
			txtProfitAmount.BackColor = System.Drawing.Color.White;
			txtProfitAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProfitAmount.Data = 0;
			txtProfitAmount.EnterColor = System.Drawing.Color.LightPink;
			txtProfitAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProfitAmount.Format = null;
			txtProfitAmount.isAllowNegative = false;
			txtProfitAmount.isAllowSpecialChar = false;
			txtProfitAmount.isNumberOnly = false;
			txtProfitAmount.isNumeric = true;
			txtProfitAmount.LeaveColor = System.Drawing.Color.White;
			txtProfitAmount.Location = new System.Drawing.Point(741, 342);
			txtProfitAmount.Margin = new System.Windows.Forms.Padding(4);
			txtProfitAmount.Name = "txtProfitAmount";
			txtProfitAmount.Size = new System.Drawing.Size(155, 16);
			txtProfitAmount.TabIndex = 19;
			txtProfitAmount.Text = "0";
			txtProfitAmount.Value = new decimal(new int[4]);
			txtProfitAmount.WaterMark = null;
			txtProfitAmount.Validated += new System.EventHandler(txtProfitAmount_Validated);
			txtNetAmount.BackColor = System.Drawing.Color.White;
			txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNetAmount.Data = 0;
			txtNetAmount.EnterColor = System.Drawing.Color.LightPink;
			txtNetAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNetAmount.Format = null;
			txtNetAmount.isAllowNegative = false;
			txtNetAmount.isAllowSpecialChar = false;
			txtNetAmount.isNumberOnly = false;
			txtNetAmount.isNumeric = true;
			txtNetAmount.LeaveColor = System.Drawing.Color.White;
			txtNetAmount.Location = new System.Drawing.Point(741, 366);
			txtNetAmount.Margin = new System.Windows.Forms.Padding(4);
			txtNetAmount.Name = "txtNetAmount";
			txtNetAmount.ReadOnly = true;
			txtNetAmount.Size = new System.Drawing.Size(155, 16);
			txtNetAmount.TabIndex = 21;
			txtNetAmount.Text = "0";
			txtNetAmount.Value = new decimal(new int[4]);
			txtNetAmount.WaterMark = null;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(14, 62);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(76, 16);
			label9.TabIndex = 29;
			label9.Text = "Godown : ";
			cmbGodown.EnterColor = System.Drawing.Color.LightPink;
			cmbGodown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGodown.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGodown.FormattingEnabled = true;
			cmbGodown.LeaveColor = System.Drawing.Color.White;
			cmbGodown.Location = new System.Drawing.Point(91, 58);
			cmbGodown.Name = "cmbGodown";
			cmbGodown.Size = new System.Drawing.Size(121, 24);
			cmbGodown.TabIndex = 30;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label10.Location = new System.Drawing.Point(231, 62);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(73, 16);
			label10.TabIndex = 31;
			label10.Text = "Remarks :";
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.Data = 0;
			txtRemarks.EnterColor = System.Drawing.Color.LightPink;
			txtRemarks.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtRemarks.Format = null;
			txtRemarks.isAllowNegative = false;
			txtRemarks.isAllowSpecialChar = false;
			txtRemarks.isNumberOnly = false;
			txtRemarks.isNumeric = false;
			txtRemarks.LeaveColor = System.Drawing.Color.White;
			txtRemarks.Location = new System.Drawing.Point(309, 66);
			txtRemarks.Margin = new System.Windows.Forms.Padding(4);
			txtRemarks.Name = "txtRemarks";
			txtRemarks.Size = new System.Drawing.Size(587, 16);
			txtRemarks.TabIndex = 32;
			txtRemarks.Value = new decimal(new int[4]);
			txtRemarks.WaterMark = null;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(911, 431);
			base.Controls.Add(dgvDetails);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label10);
			base.Controls.Add(cmbGodown);
			base.Controls.Add(label9);
			base.Controls.Add(btnManExpenses);
			base.Controls.Add(txtNetAmount);
			base.Controls.Add(txtTotalCost);
			base.Controls.Add(txtProfitAmount);
			base.Controls.Add(txtManExpenses);
			base.Controls.Add(label8);
			base.Controls.Add(label5);
			base.Controls.Add(btnRMCost);
			base.Controls.Add(label7);
			base.Controls.Add(label4);
			base.Controls.Add(txtProfitP);
			base.Controls.Add(label6);
			base.Controls.Add(txtRMCost);
			base.Controls.Add(label3);
			base.Controls.Add(button1);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnEdit);
			base.Controls.Add(btnSave);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnDateChange);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label2);
			base.Controls.Add(cbInvNo);
			base.Controls.Add(nudInvNo);
			base.Controls.Add(label1);
			base.Name = "ProductManufacturingView";
			base.Load += new System.EventHandler(ProductManufacturingView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ProductManufacturingView_KeyDown);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(nudInvNo, 0);
			base.Controls.SetChildIndex(cbInvNo, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(btnDateChange, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnEdit, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtRMCost, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(txtProfitP, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(btnRMCost, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(txtManExpenses, 0);
			base.Controls.SetChildIndex(txtProfitAmount, 0);
			base.Controls.SetChildIndex(txtTotalCost, 0);
			base.Controls.SetChildIndex(txtNetAmount, 0);
			base.Controls.SetChildIndex(btnManExpenses, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(cmbGodown, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(dgvDetails, 0);
			((System.ComponentModel.ISupportInitialize)nudInvNo).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
