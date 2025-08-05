using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class StockTransferView : BaseForm
	{
		private string sTitle;

		private DateTime appDate;

		private DBContext db;

		private int iStockTransferMasterId;

		private List<ExStockTranSettings> exStocksTSettings;

		private DataGridViewTextBoxEditingControl textbox;

		private IContainer components = null;

		private Label lblHead;

		private Button btnDate;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private CheckBox cbPrntWlSaving;

		private Button button4;

		private AshComboBox cmbType;

		private Label label8;

		private Button btnSave;

		private Panel panel1;

		public AshDataGridview dgvDetails;

		private Label label3;

		private AshComboBox cmbToGdn;

		private AshComboBox cmbFromGdwn;

		private Label label4;

		private AshUpDown nudInvNo;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private CheckBox cbEnableInvNo;

		private Label label1;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private AshNumericLabel lblTotalQty;

		private AshNumericLabel lblTotalAmount;

		private BindingSource bdsDetails;

		private PrintDocument printDocument;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_UnitName;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_TotalAmount;

		public UserPrevilegeDtl plgDtls { get; set; }

		public StockTransferView(string _sTitle)
		{
			sTitle = _sTitle;
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
		}

		private void BindDgv()
		{
			dgvDetails.AutoGenerateColumns = false;
			dgvDetails.DataSource = null;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exStocksTSettings;
			dgvDetails.DataSource = bdsDetails;
		}

		private void TotalFindMethod()
		{
			lblTotalQty.Value = exStocksTSettings.Sum((ExStockTranSettings x) => x.ProQuantity);
			lblTotalAmount.Value = exStocksTSettings.Sum((ExStockTranSettings x) => x.Total);
		}

		private StockTransferMaster MasterEntityLoadMethod(int iCode)
		{
			return new StockTransferMaster
			{
				StockTransferId = iStockTransferMasterId,
				EntryDate = DateTime.Now.Date,
				TransferDate = dtmDate.Value.Date,
				StockTransferCode = iCode,
				Remarks = txtRemarks.Text,
				FromGodownId = cmbFromGdwn.SelectedValue.ToInt32(),
				FromGodownName = cmbFromGdwn.Text,
				ToGodownId = cmbToGdn.SelectedValue.ToInt32(),
				ToGodownName = cmbToGdn.Text,
				TranType = cmbType.Text,
				TotalQty = lblTotalQty.Value,
				TotalAmnt = lblTotalAmount.Value,
				CompanyName = db.Companies().FirstOrDefault().InstituteName,
				FK_BranchId = Settings.Default.iBranchId,
				UserId = Settings.Default.intLogOnId
			};
		}

		private void GdnStockTransMethod(StockTranSettings x, int iCode, ComboBox cmb, decimal InQty, decimal OutQty)
		{
			GdwnStckTranLog data = new GdwnStckTranLog
			{
				GSTranId = null,
				VchrId = iStockTransferMasterId,
				VchrNo = iCode,
				VchrType = lblHead.Text,
				GodwnId = cmb.SelectedValue.ToInt32(),
				GodownName = cmb.Text,
				ProductId = x.ProductId,
				ProductName = x.ProductName,
				InQuantity = InQty,
				OutQuantity = OutQty,
				UnitId = x.UnitId,
				VchrDate = dtmDate.Value.Date,
				TranDate = DateTime.Now,
				UserId = Settings.Default.intLogOnId,
				Narration = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
			AshproCommon.InsertToDatabaseObj(data);
		}

		private void SaveSubmethod(List<StockTranSettings> stockTranSettings, int iCode)
		{
			stockTranSettings.ForEach(delegate(StockTranSettings x)
			{
				x.StockTransferId = iStockTransferMasterId;
			});
			if (!AshproCommon.InsertToDatabase(stockTranSettings.Cast<object>().ToList(), "StockTranSettings"))
			{
				return;
			}
			foreach (StockTranSettings stockTranSetting in stockTranSettings)
			{
				GdnStockTransMethod(stockTranSetting, iCode, cmbFromGdwn, 0m, stockTranSetting.ProQuantity);
				GdnStockTransMethod(stockTranSetting, iCode, cmbToGdn, stockTranSetting.ProQuantity, 0m);
			}
		}

		private void GdnStockTranDeleteMethod()
		{
			List<GdwnStckTranLog> list = (from x in db.GdwnStckTranLogs()
				where x.VchrType == lblHead.Text && x.VchrId == iStockTransferMasterId
				select x).ToList();
			list.ForEach(delegate(GdwnStckTranLog x)
			{
				AshproCommon.DeleteFromDatabase("GdwnStckTranLog", "GSTranId", x.GSTranId.toInt32());
			});
		}

		private void ExStockTranSettingsLoadMethod(ExStockTranSettings t, ProductEntity productEntity, decimal iQty)
		{
			if (productEntity != null)
			{
				string unitName = db.Units(new { productEntity.UnitId }).FirstOrDefault().UnitName;
				t.StockTranSettingsId = null;
				t.Barcode = productEntity.Barcode;
				t.ProductId = productEntity.ProductId;
				t.ProCode = productEntity.ProductCode.toInt32();
				t.ProductName = productEntity.ProductName;
				t.ProQuantity = iQty;
				t.UnitId = productEntity.UnitId;
				t.UnitName = unitName;
				t.ProRate = productEntity.SellingPrice;
				t.Total = ((t.ProQuantity == 0m) ? 0m : (productEntity.SellingPrice * t.ProQuantity));
				t.SL = exStocksTSettings.Count;
			}
		}

		private void DisableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			nudInvNo.Enabled = true;
			cbEnableInvNo.Enabled = true;
			btnDelete.Enabled = true;
			btnEdit.Enabled = true;
			btnPrint.Enabled = true;
			btnClear.Enabled = true;
			btnEnd.Enabled = true;
		}

		private void EnableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			btnPrint.Enabled = false;
		}

		private void btnClearMethod()
		{
			EnableMethod();
			exStocksTSettings = new List<ExStockTranSettings>();
			BindDgv();
			AniHelper.ClearAshNumericLabels(this);
			txtRemarks.Text = string.Empty;
			iStockTransferMasterId = -1;
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			cmbType.Focus();
		}

		private void StockTransferView_Load(object sender, EventArgs e)
		{
			lblHead.Text = sTitle;
			exStocksTSettings = new List<ExStockTranSettings>();
			db = new DBContext();
			appDate = Settings.Default.appDate;
			dtmDate.Value = appDate;
			AniHelper.FillGodown(cmbFromGdwn);
			AniHelper.FillGodown(cmbToGdn);
			nudInvNo.Value = (db.StockTransferMasters().Max((StockTransferMaster x) => x.StockTransferCode) + 1) ?? 1;
			BindDgv();
			base.ActiveControl = cmbFromGdwn;
			cmbFromGdwn.Focus();
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			appDate = dtmDate.Value.Date;
		}

		private void cbEnableInvNo_CheckedChanged(object sender, EventArgs e)
		{
			nudInvNo.Enabled = cbEnableInvNo.Checked;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudInvNo.Value = (db.StockTransferMasters().Max((StockTransferMaster x) => x.StockTransferCode) + 1) ?? 1;
			btnClearMethod();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			EnableMethod();
			btnSave.Text = "Update";
			dgvDetails.Focus();
			dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 1].Cells[1];
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage())
				{
					GdnStockTranDeleteMethod();
					if (AshproCommon.DeleteFromDatabase("StockTranSettings", "StockTransferId", iStockTransferMasterId) && AshproCommon.DeleteFromDatabase("StockTransferMaster", "StockTransferId", iStockTransferMasterId))
					{
						btnClear_Click(null, null);
					}
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
				if (exStocksTSettings.Count == 0)
				{
					Messages.WarningMessage();
					return;
				}
				List<StockTranSettings> list = AniHelper.CopyListData<StockTranSettings>(exStocksTSettings.Cast<object>().ToList());
				list.ForEach(delegate(StockTranSettings x)
				{
					x.StockTranSettingsId = null;
				});
				if (btnSave.Text == "Save")
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
						return;
					}
					int iCode = (db.StockTransferMasters().Max((StockTransferMaster x) => x.StockTransferCode) + 1) ?? 1;
					StockTransferMaster stockTransferMaster = MasterEntityLoadMethod(iCode);
					stockTransferMaster.StockTransferId = null;
					if (AshproCommon.InsertToDatabaseObj(stockTransferMaster, "StockTransferMaster"))
					{
						iStockTransferMasterId = (from x in db.StockTransferMasters()
							where x.StockTransferCode == iCode
							select x).FirstOrDefault().StockTransferId.toInt32();
						SaveSubmethod(list, iCode);
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					GdnStockTranDeleteMethod();
					if (AshproCommon.DeleteFromDatabase("StockTranSettings", "StockTransferId", iStockTransferMasterId))
					{
						StockTransferMaster data = MasterEntityLoadMethod(nudInvNo.Value.ToInt32());
						if (AshproCommon.UpdateToDatabaseObj(data, "StockTransferMaster", "StockTransferId", iStockTransferMasterId))
						{
							SaveSubmethod(list, nudInvNo.Value.ToInt32());
						}
					}
				}
				if (cbPrntWlSaving.Checked)
				{
					printDocument.Print();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			btnClear_Click(null, null);
		}

		private void nudInvNo_ValueChanged(object sender, EventArgs e)
		{
			StockTransferMaster stockTransferMaster = (from x in db.StockTransferMasters()
				where x.StockTransferCode == nudInvNo.Value.ToInt32()
				select x).FirstOrDefault();
			if (stockTransferMaster != null)
			{
				iStockTransferMasterId = stockTransferMaster.StockTransferId.ToInt32();
				dtmDate.Value = stockTransferMaster.TransferDate;
				txtRemarks.Text = stockTransferMaster.Remarks;
				cmbType.Text = stockTransferMaster.TranType;
				cmbFromGdwn.SelectedValue = stockTransferMaster.FromGodownId.GetValueOrDefault();
				cmbToGdn.SelectedValue = stockTransferMaster.ToGodownId.GetValueOrDefault();
				List<StockTranSettings> source = (from x in db.StockTranSettings()
					where x.StockTransferId == iStockTransferMasterId
					select x).ToList();
				exStocksTSettings = AniHelper.CopyListData<ExStockTranSettings>(source.Cast<object>().ToList());
				int num = 1;
				foreach (ExStockTranSettings t in exStocksTSettings)
				{
					ProductEntity productEntity = db.Products(new { t.ProductId }).FirstOrDefault();
					_ = db.Units(new { productEntity.UnitId }).FirstOrDefault().UnitName;
					t.ProCode = productEntity.ProductCode.ToInt32();
					t.Barcode = productEntity.Barcode;
					t.ProductName = productEntity.ProductName;
					t.UnitName = (from y in db.Units()
						where y.UnitId == t.UnitId
						select y).FirstOrDefault().UnitName;
					t.SL = num;
					num++;
				}
				BindDgv();
				TotalFindMethod();
				DisableMethod();
			}
			else
			{
				btnClearMethod();
			}
		}

		private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				int iCount = 1;
				ExStockTranSettings item = (ExStockTranSettings)dgvDetails.CurrentRow.DataBoundItem;
				exStocksTSettings.Remove(item);
				exStocksTSettings.ForEach(delegate(ExStockTranSettings x)
				{
					x.SL = iCount;
					iCount++;
				});
				BindDgv();
				dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 1].Cells[1];
				TotalFindMethod();
			}
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
				ExStockTranSettings t = (ExStockTranSettings)dgvDetails.CurrentRow.DataBoundItem;
				int rowIndex = dgvDetails.CurrentCell.RowIndex;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
					{
						e.Handled = true;
						SelectCustomer selectCustomer = new SelectCustomer("");
						selectCustomer.sName = textbox.Text;
						if (selectCustomer.ShowDialog() == DialogResult.OK)
						{
							dgvDetails.CurrentCell.Value = selectCustomer.sName;
							ProductEntity productEntity = db.Products(new
							{
								ProductId = selectCustomer.iId
							}).FirstOrDefault();
							ExStockTranSettingsLoadMethod(t, productEntity, 1m);
							TotalFindMethod();
							dgvDetails.CurrentCell = dgvDetails.Rows[rowIndex].Cells[col_Qty.Name];
						}
					}
				}
				else
				{
					if (!(dgvDetails.CurrentCell.OwningColumn.Name == col_UnitName.Name))
					{
						return;
					}
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer frm = new SelectCustomer("Unit");
					frm.ProductId = t.ProductId.ToInt32();
					if (frm.ShowDialog() == DialogResult.OK)
					{
						dgvDetails.CurrentCell.Value = frm.sName;
						int iPCategoryId = db.PriceCategories().FirstOrDefault().PCategoryId.toInt32();
						ProductPriceCategory productPriceCategory = (from x in db.ProductPriceCategories()
							where x.ProductId == t.ProductId && x.PCategoryId == iPCategoryId && x.UnitId == frm.iId
							select x).FirstOrDefault();
						t.UnitId = frm.iId;
						t.UnitName = frm.sName;
						t.ProRate = productPriceCategory.SPrice;
						t.Total = ((t.ProQuantity == 0m) ? 0m : (t.ProQuantity * productPriceCategory.SPrice));
						dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex + 1].Cells[1];
						TotalFindMethod();
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
			if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && !(sender as TextBox).Text.Contains('.')) || (e.KeyChar == '.' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) || (e.KeyChar == '-' && !(sender as TextBox).Text.Contains('-')) || (e.KeyChar == '-' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength))
				{
					return;
				}
				e.Handled = true;
			}
			if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
			{
				dgvDetails.NotifyCurrentCellDirty(dirty: true);
			}
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExStockTranSettings exStockTranSettings = (ExStockTranSettings)dgvDetails.CurrentRow.DataBoundItem;
			if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProCode.Name)
			{
				ProductEntity productEntity = db.Products(new
				{
					ProductCode = exStockTranSettings.ProCode
				}).FirstOrDefault();
				ExStockTranSettingsLoadMethod(exStockTranSettings, productEntity, 1m);
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Barcode.Name)
			{
				ProductEntity productEntity2 = db.Products(new { exStockTranSettings.Barcode }).FirstOrDefault();
				ExStockTranSettingsLoadMethod(exStockTranSettings, productEntity2, 1m);
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				ProductEntity productEntity3 = db.Products(new { exStockTranSettings.ProductId }).FirstOrDefault();
				ExStockTranSettingsLoadMethod(exStockTranSettings, productEntity3, exStockTranSettings.ProQuantity);
			}
			TotalFindMethod();
		}

		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				int num = 0;
				bool flag = false;
				int num2 = 5;
				Company company = db.Companies().FirstOrDefault();
				GodownEntity godownEntity = (from t in db.Godowns()
					where t.GodwnId == cmbFromGdwn.SelectedValue.ToInt32()
					select t).FirstOrDefault();
				GodownEntity godownEntity2 = (from t in db.Godowns()
					where t.GodwnId == cmbToGdn.SelectedValue.ToInt32()
					select t).FirstOrDefault();
				_ = e.MarginBounds.Left;
				int num3 = e.MarginBounds.Top;
				int right = e.MarginBounds.Right;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				float[] dashPattern = new float[2] { 5f, 3f };
				Pen pen = new Pen(Color.Black, 1f);
				pen.DashPattern = dashPattern;
				Font font = new Font("Courier New", 10f, FontStyle.Regular);
				do
				{
					if (num < exStocksTSettings.Count)
					{
						if (!flag)
						{
							e.Graphics.DrawString("STOCK TRANSFER INVOICE", new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(410, num2), stringFormat2);
							num2 += 15;
							e.Graphics.DrawString(company.InstituteName, new Font("Courier New", 11f, FontStyle.Bold), Brushes.Black, new Point(410, num2), stringFormat2);
							num2 += 15;
							e.Graphics.DrawString(company.Address, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(410, num2), stringFormat2);
							num2 += 15;
							e.Graphics.DrawString(company.PhoneNo, new Font("Courier New", 9f, FontStyle.Regular), Brushes.Black, new Point(410, num2), stringFormat2);
							num2 += 30;
							e.Graphics.DrawString("Inv No: ", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(10, num2));
							e.Graphics.DrawString(nudInvNo.Value.ToString(), new Font("Courier New", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(136f, num2, 340f, 35f));
							e.Graphics.DrawString("Date: ", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(450, num2));
							e.Graphics.DrawString(dtmDate.Text, new Font("Courier New", 10f, FontStyle.Bold), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawString("From Godown: ", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(10, num2));
							e.Graphics.DrawString(cmbFromGdwn.Text, new Font("Courier New", 10f, FontStyle.Bold), Brushes.Black, new Point(136, num2));
							e.Graphics.DrawString("To Godown: ", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(450, num2));
							e.Graphics.DrawString(cmbToGdn.Text, new Font("Courier New", 10f, FontStyle.Bold), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawString(godownEntity.AddressOne, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(136, num2));
							e.Graphics.DrawString(godownEntity2.AddressOne, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawString(godownEntity.AddressTwo, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(136, num2));
							e.Graphics.DrawString(godownEntity2.AddressTwo, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawString(godownEntity.AddressThree, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(136, num2));
							e.Graphics.DrawString(godownEntity2.AddressThree, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawString(godownEntity.Phone, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(136, num2));
							e.Graphics.DrawString(godownEntity2.Phone, new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(556, num2));
							num2 += 15;
							e.Graphics.DrawLine(pen, new Point(5, num2 + 5), new Point(right + 37, num2 + 5));
							num2 += 15;
							e.Graphics.DrawString("SL", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(14, num2), stringFormat2);
							e.Graphics.DrawString("Product Name", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(40, num2));
							e.Graphics.DrawString("Qty", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(490, num2), stringFormat);
							e.Graphics.DrawString("Unit", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(500, num2));
							e.Graphics.DrawString("Rate", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(650, num2), stringFormat);
							e.Graphics.DrawString("Net Amount", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(right + 40, num2), stringFormat);
							num2 += 15;
							e.Graphics.DrawLine(pen, new Point(5, num2 + 5), new Point(right + 37, num2 + 5));
							num3 = (num2 += 15);
						}
						flag = true;
						e.Graphics.DrawString(exStocksTSettings[num].SL.ToString(), font, Brushes.Black, new Point(14, num3), stringFormat2);
						e.Graphics.DrawString(exStocksTSettings[num].ProductName, font, Brushes.Black, new RectangleF(40f, num3, 340f, 30f));
						if (exStocksTSettings[num].ProductName.Length > 40)
						{
							num3 += 15;
						}
						e.Graphics.DrawString(exStocksTSettings[num].ProQuantity.ToString(CommonSettings.strCurencyFormat), font, Brushes.Black, new Point(490, num3), stringFormat);
						e.Graphics.DrawString(exStocksTSettings[num].UnitName, font, Brushes.Black, new Point(500, num3));
						e.Graphics.DrawString(exStocksTSettings[num].ProRate.ToString(CommonSettings.strCurencyFormat), font, Brushes.Black, new Point(650, num3), stringFormat);
						e.Graphics.DrawString(exStocksTSettings[num].Total.ToString(CommonSettings.strCurencyFormat), font, Brushes.Black, new Point(right + 40, num3), stringFormat);
						num++;
						num3 += 15;
						continue;
					}
					e.Graphics.DrawLine(pen, new Point(5, num3 + 5), new Point(right + 37, num3 + 5));
					num3 += 15;
					e.Graphics.DrawString("   :Total Amount", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(410, num3), stringFormat);
					e.Graphics.DrawString(lblTotalAmount.Value.ToString(CommonSettings.strCurencyFormat), new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(510, num3), stringFormat);
					num3 += 15;
					e.Graphics.DrawLine(pen, new Point(5, num3 + 5), new Point(right + 37, num3 + 5));
					num = 0;
					e.HasMorePages = false;
					return;
				}
				while (num3 + 60 < e.MarginBounds.Bottom);
				num3 += 20;
				e.Graphics.DrawString("Cuntinue", new Font("Courier New", 10f, FontStyle.Regular), Brushes.Black, new Point(right, num3), stringFormat);
				num3 = 15;
				e.HasMorePages = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			printDocument.Print();
		}

		private void StockTransferView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint_Click(null, null);
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			lblHead = new System.Windows.Forms.Label();
			btnDate = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			cbPrntWlSaving = new System.Windows.Forms.CheckBox();
			button4 = new System.Windows.Forms.Button();
			cmbType = new AshUsercontrol.AshComboBox();
			label8 = new System.Windows.Forms.Label();
			btnSave = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label3 = new System.Windows.Forms.Label();
			cmbToGdn = new AshUsercontrol.AshComboBox();
			cmbFromGdwn = new AshUsercontrol.AshComboBox();
			label4 = new System.Windows.Forms.Label();
			nudInvNo = new AshUsercontrol.AshUpDown();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			cbEnableInvNo = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			toolTip = new System.Windows.Forms.ToolTip(components);
			lblTotalQty = new AshUsercontrol.AshNumericLabel();
			lblTotalAmount = new AshUsercontrol.AshNumericLabel();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			printDocument = new System.Drawing.Printing.PrintDocument();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudInvNo).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(745, -1);
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 6);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(144, 24);
			lblHead.TabIndex = 96;
			lblHead.Text = "Stock Transfer";
			btnDate.Location = new System.Drawing.Point(565, 12);
			btnDate.Name = "btnDate";
			btnDate.Size = new System.Drawing.Size(25, 23);
			btnDate.TabIndex = 82;
			btnDate.Text = "..";
			toolTip.SetToolTip(btnDate, "Press Alt + T");
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(622, 497);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 91;
			btnClear.Text = "Clear";
			toolTip.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.Enabled = false;
			btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Location = new System.Drawing.Point(700, 497);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(75, 30);
			btnPrint.TabIndex = 92;
			btnPrint.Text = "Print";
			toolTip.SetToolTip(btnPrint, "Press Alt + P");
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(544, 497);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 90;
			btnDelete.Text = "Delete";
			toolTip.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.Enabled = false;
			btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Location = new System.Drawing.Point(466, 497);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new System.Drawing.Size(75, 30);
			btnEdit.TabIndex = 89;
			btnEdit.Text = "Edit";
			toolTip.SetToolTip(btnEdit, "Press Alt + E");
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			cbPrntWlSaving.AutoSize = true;
			cbPrntWlSaving.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cbPrntWlSaving.Location = new System.Drawing.Point(15, 482);
			cbPrntWlSaving.Name = "cbPrntWlSaving";
			cbPrntWlSaving.Size = new System.Drawing.Size(142, 20);
			cbPrntWlSaving.TabIndex = 85;
			cbPrntWlSaving.Text = "Print while saving";
			cbPrntWlSaving.UseVisualStyleBackColor = true;
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(163, 1);
			button4.TabIndex = 97;
			button4.UseVisualStyleBackColor = true;
			cmbType.EnterColor = System.Drawing.Color.LightPink;
			cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbType.FormattingEnabled = true;
			cmbType.Items.AddRange(new object[1] { "Godown to Godown" });
			cmbType.LeaveColor = System.Drawing.Color.White;
			cmbType.Location = new System.Drawing.Point(161, 54);
			cmbType.Name = "cmbType";
			cmbType.Size = new System.Drawing.Size(208, 24);
			cmbType.TabIndex = 73;
			cmbType.Text = "Godown to Godown";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(42, 55);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(112, 16);
			label8.TabIndex = 72;
			label8.Text = "Transfer Type :";
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(388, 497);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 80;
			btnSave.Text = "Save";
			toolTip.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(14, 172);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(750, 282);
			panel1.TabIndex = 83;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_Barcode, col_ProCode, col_ProName, col_Qty, col_UnitName, col_Rate, col_TotalAmount);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.EnterKeyNavigation = false;
			dgvDetails.LastKey = System.Windows.Forms.Keys.None;
			dgvDetails.Location = new System.Drawing.Point(13, 12);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.Size = new System.Drawing.Size(722, 257);
			dgvDetails.TabIndex = 0;
			dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellEndEdit);
			dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetails_DataError);
			dgvDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetails_EditingControlShowing);
			dgvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetails_KeyDown);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 40;
			col_Barcode.DataPropertyName = "Barcode";
			col_Barcode.HeaderText = "Barcode";
			col_Barcode.Name = "col_Barcode";
			col_ProCode.DataPropertyName = "ProCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProCode.Visible = false;
			col_ProCode.Width = 80;
			col_ProName.DataPropertyName = "ProductName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.Width = 265;
			col_Qty.DataPropertyName = "ProQuantity";
			col_Qty.HeaderText = "Qty";
			col_Qty.Name = "col_Qty";
			col_Qty.Width = 80;
			col_UnitName.DataPropertyName = "UnitName";
			col_UnitName.HeaderText = "Unit";
			col_UnitName.Name = "col_UnitName";
			col_UnitName.Width = 60;
			col_Rate.DataPropertyName = "ProRate";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle3;
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_TotalAmount.DataPropertyName = "Total";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_TotalAmount.DefaultCellStyle = dataGridViewCellStyle4;
			col_TotalAmount.HeaderText = "Total Amount";
			col_TotalAmount.Name = "col_TotalAmount";
			col_TotalAmount.ReadOnly = true;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(61, 112);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(93, 16);
			label3.TabIndex = 76;
			label3.Text = "Godown To :";
			cmbToGdn.EnterColor = System.Drawing.Color.LightPink;
			cmbToGdn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbToGdn.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbToGdn.FormattingEnabled = true;
			cmbToGdn.LeaveColor = System.Drawing.Color.White;
			cmbToGdn.Location = new System.Drawing.Point(161, 110);
			cmbToGdn.Name = "cmbToGdn";
			cmbToGdn.Size = new System.Drawing.Size(208, 24);
			cmbToGdn.TabIndex = 77;
			cmbFromGdwn.EnterColor = System.Drawing.Color.LightPink;
			cmbFromGdwn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbFromGdwn.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbFromGdwn.FormattingEnabled = true;
			cmbFromGdwn.LeaveColor = System.Drawing.Color.White;
			cmbFromGdwn.Location = new System.Drawing.Point(161, 82);
			cmbFromGdwn.Name = "cmbFromGdwn";
			cmbFromGdwn.Size = new System.Drawing.Size(208, 24);
			cmbFromGdwn.TabIndex = 75;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(46, 86);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(108, 16);
			label4.TabIndex = 74;
			label4.Text = "Godown From :";
			nudInvNo.Enabled = false;
			nudInvNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			nudInvNo.Location = new System.Drawing.Point(271, 12);
			nudInvNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudInvNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudInvNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.Name = "nudInvNo";
			nudInvNo.Size = new System.Drawing.Size(91, 23);
			nudInvNo.TabIndex = 69;
			nudInvNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudInvNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.ValueChanged += new System.EventHandler(nudInvNo_ValueChanged);
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(442, 12);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 71;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(392, 19);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(45, 16);
			label2.TabIndex = 70;
			label2.Text = "Date:";
			cbEnableInvNo.AutoSize = true;
			cbEnableInvNo.Location = new System.Drawing.Point(369, 16);
			cbEnableInvNo.Name = "cbEnableInvNo";
			cbEnableInvNo.Size = new System.Drawing.Size(15, 14);
			cbEnableInvNo.TabIndex = 81;
			cbEnableInvNo.UseVisualStyleBackColor = true;
			cbEnableInvNo.CheckedChanged += new System.EventHandler(cbEnableInvNo_CheckedChanged);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(213, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 16);
			label1.TabIndex = 67;
			label1.Text = "Inv No:";
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtRemarks.Data = 0;
			txtRemarks.EnterColor = System.Drawing.Color.LightPink;
			txtRemarks.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtRemarks.Format = null;
			txtRemarks.isAllowNegative = false;
			txtRemarks.isAllowSpecialChar = false;
			txtRemarks.isNumberOnly = false;
			txtRemarks.isNumeric = false;
			txtRemarks.LeaveColor = System.Drawing.Color.White;
			txtRemarks.Location = new System.Drawing.Point(161, 141);
			txtRemarks.Margin = new System.Windows.Forms.Padding(4);
			txtRemarks.Name = "txtRemarks";
			txtRemarks.Size = new System.Drawing.Size(539, 16);
			txtRemarks.TabIndex = 79;
			txtRemarks.Value = new decimal(new int[4]);
			txtRemarks.WaterMark = null;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(81, 137);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(73, 16);
			label12.TabIndex = 78;
			label12.Text = "Remarks :";
			lblTotalQty.AutoSize = true;
			lblTotalQty.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTotalQty.Format = null;
			lblTotalQty.Location = new System.Drawing.Point(306, 461);
			lblTotalQty.Name = "lblTotalQty";
			lblTotalQty.Size = new System.Drawing.Size(17, 16);
			lblTotalQty.TabIndex = 99;
			lblTotalQty.Text = "0";
			lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblTotalQty.Value = new decimal(new int[4]);
			lblTotalAmount.AutoSize = true;
			lblTotalAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTotalAmount.Format = null;
			lblTotalAmount.Location = new System.Drawing.Point(421, 461);
			lblTotalAmount.Name = "lblTotalAmount";
			lblTotalAmount.Size = new System.Drawing.Size(17, 16);
			lblTotalAmount.TabIndex = 99;
			lblTotalAmount.Text = "0";
			lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblTotalAmount.Value = new decimal(new int[4]);
			printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(781, 530);
			base.Controls.Add(lblTotalAmount);
			base.Controls.Add(lblTotalQty);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnDate);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnPrint);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnEdit);
			base.Controls.Add(cbPrntWlSaving);
			base.Controls.Add(button4);
			base.Controls.Add(cmbType);
			base.Controls.Add(label8);
			base.Controls.Add(btnSave);
			base.Controls.Add(panel1);
			base.Controls.Add(label3);
			base.Controls.Add(cmbToGdn);
			base.Controls.Add(cmbFromGdwn);
			base.Controls.Add(label4);
			base.Controls.Add(nudInvNo);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label2);
			base.Controls.Add(cbEnableInvNo);
			base.Controls.Add(label1);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label12);
			base.Name = "StockTransferView";
			base.Load += new System.EventHandler(StockTransferView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(StockTransferView_KeyDown);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cbEnableInvNo, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(nudInvNo, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(cmbFromGdwn, 0);
			base.Controls.SetChildIndex(cmbToGdn, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(cmbType, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(cbPrntWlSaving, 0);
			base.Controls.SetChildIndex(btnEdit, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnPrint, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnDate, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(lblTotalQty, 0);
			base.Controls.SetChildIndex(lblTotalAmount, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)nudInvNo).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
