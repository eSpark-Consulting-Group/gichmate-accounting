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

namespace Ashpro
{
	public class StockAdjusterView : BaseForm
	{
		private DateTime estimateDate;

		private int iStockAdjusterMaster;

		private DBContext db;

		private List<ExStockAdjustSettings> exStockAdjustSettings;

		private IContainer components = null;

		private Button button4;

		private ToolTip toolTip;

		private Button btnClear;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnExcel;

		private Button btnSave;

		private Button btnLoad;

		private Label lblHead;

		public DataGridView dgvDetails;

		private Panel panel1;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private Label label3;

		private AshComboBox cmbProBrand;

		private AshComboBox cmbProGroup;

		private Label label4;

		private AshUpDown nudVchrNo;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private CheckBox cbEnableVchrNo;

		private Label label1;

		private Button btnDate;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_SysQty;

		private DataGridViewTextBoxColumn col_UnitName;

		private DataGridViewTextBoxColumn col_ActQty;

		private DataGridViewTextBoxColumn col_Excess;

		private DataGridViewTextBoxColumn col_Short;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_TotalAmount;

		public UserPrevilegeDtl plgDtls { get; set; }

		public StockAdjusterView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void BindDGV(List<ExStockAdjustSettings> exStockAdjustSettings)
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exStockAdjustSettings;
			dgvDetails.DataSource = bdsDetails;
		}

		private void DeleteGodownStockTranLoag()
		{
			List<GdwnStckTranLog> list = (from x in db.GdwnStckTranLogs()
				where x.VchrId == iStockAdjusterMaster && x.VchrType == "Stock Adjustment"
				select x).ToList();
			foreach (GdwnStckTranLog item in list)
			{
				AshproCommon.DeleteFromDatabase("GdwnStckTranLog", "GSTranId", item.GSTranId.toInt32());
			}
		}

		private void DeleteSTranLogMethod()
		{
			List<StockTranLog> list = (from x in db.StockTranLogs()
				where x.VchrId == iStockAdjusterMaster && x.VchrType == "Stock Adjustment"
				select x).ToList();
			foreach (StockTranLog item in list)
			{
				AshproCommon.DeleteFromDatabase("StockTranLog", "STranId", item.STranId.toInt32());
			}
		}

		private void DisableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			nudVchrNo.Enabled = true;
			cbEnableVchrNo.Enabled = true;
			btnDelete.Enabled = true;
			btnEdit.Enabled = true;
			btnClear.Enabled = true;
			btnEnd.Enabled = true;
			btnExcel.Enabled = true;
		}

		private void EnableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			btnExcel.Enabled = false;
		}

		private void btnClearMethod()
		{
			dtmDate.Value = estimateDate;
			AniHelper.ClearComboMethod(this);
			txtRemarks.Text = string.Empty;
			exStockAdjustSettings = new List<ExStockAdjustSettings>();
			BindDGV(exStockAdjustSettings);
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			cmbProGroup.Focus();
		}

		private async Task<ExStockAdjustSettings> ItemFoundMethod(int iSL, int iProId)
		{
			ProductEntity productEntity = (from x in db.Products()
				where x.ProductId == iProId
				select x).FirstOrDefault();
			return await Task.Run(delegate
			{
				ExStockAdjustSettings exStockAdjustSettings = new ExStockAdjustSettings();
				exStockAdjustSettings.productId = productEntity.ProductId.ToInt32();
				exStockAdjustSettings.ProductCode = productEntity.ProductCode.ToInt32();
				exStockAdjustSettings.SL = iSL;
				exStockAdjustSettings.ProductName = productEntity.ProductName;
				exStockAdjustSettings.unitId = productEntity.UnitId;
				exStockAdjustSettings.UnitName = (from x in db.Units()
					where x.UnitId == productEntity.UnitId
					select x).FirstOrDefault().UnitName;
				exStockAdjustSettings.Rate = productEntity.SellingPrice;
				exStockAdjustSettings.curActualStock = (from y in db.StockTranLogs()
					where y.ProductId == productEntity.ProductId
					select y).Sum((StockTranLog x) => x.InQuantity - x.OutQuantity);
				exStockAdjustSettings.currSystemStock = exStockAdjustSettings.curActualStock;
				exStockAdjustSettings.TotalAmount = exStockAdjustSettings.curActualStock * productEntity.SellingPrice;
				exStockAdjustSettings.Short = 0m;
				exStockAdjustSettings.Excess = 0m;
				return exStockAdjustSettings;
			});
		}

		private void StockAdjusterView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			estimateDate = Settings.Default.appDate;
			dtmDate.Value = estimateDate;
			nudVchrNo.Value = (db.StockAdjusterMasters().Max((StockAdjusterMaster x) => x.StockAdjusterMasterCode) + 1) ?? 1;
			AniHelper.FillProGroup(cmbProGroup);
			AniHelper.FillBrand(cmbProBrand);
			AniHelper.ClearComboMethod(this);
			base.ActiveControl = cmbProGroup;
			cmbProGroup.Focus();
		}

		private async void btnLoad_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			exStockAdjustSettings = new List<ExStockAdjustSettings>();
			int iSL = 1;
			var sEntity = new
			{
				BrandId = ((cmbProBrand.Text == string.Empty) ? null : cmbProBrand.SelectedValue),
				ProGroupId = ((cmbProGroup.Text == string.Empty) ? null : cmbProGroup.SelectedValue)
			};
			exStockAdjustSettings = AshproCommon.GetList_SP<ExStockAdjustSettings>("spStockAdjustProductLoading", sEntity);
			foreach (ExStockAdjustSettings x in exStockAdjustSettings)
			{
				x.SL = iSL;
				x.TotalAmount = x.currSystemStock * x.Rate;
				iSL++;
			}
			BindDGV(exStockAdjustSettings);
			Cursor = Cursors.Arrow;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nudVchrNo.Value = (db.StockAdjusterMasters().Max((StockAdjusterMaster x) => x.StockAdjusterMasterCode) + 1) ?? 1;
			btnClearMethod();
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//AniHelper.ExportToExcelMethod(dgvDetails, "Stock Adjustment Report as on " + dtmDate.Text);
		}

		private void cbEnableVchrNo_CheckedChanged(object sender, EventArgs e)
		{
			nudVchrNo.Enabled = cbEnableVchrNo.Checked;
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExStockAdjustSettings exStockAdjustSettings = (ExStockAdjustSettings)dgvDetails.CurrentRow.DataBoundItem;
			if (exStockAdjustSettings != null)
			{
				if (exStockAdjustSettings.currSystemStock > exStockAdjustSettings.curActualStock)
				{
					exStockAdjustSettings.Excess = 0m;
					exStockAdjustSettings.Short = exStockAdjustSettings.currSystemStock - exStockAdjustSettings.curActualStock;
				}
				else
				{
					exStockAdjustSettings.Excess = exStockAdjustSettings.curActualStock - exStockAdjustSettings.currSystemStock;
					exStockAdjustSettings.Short = 0m;
				}
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			estimateDate = dtmDate.Value.Date;
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
					DeleteGodownStockTranLoag();
					DeleteSTranLogMethod();
					AshproCommon.DeleteFromDatabase("StockAdjustSettings", "stockAdjusterMasterId", iStockAdjusterMaster);
					AshproCommon.DeleteFromDatabase("StockAdjusterMaster", "stockAdjusterMasterId", iStockAdjusterMaster);
					btnClear_Click(null, null);
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
				if (exStockAdjustSettings.Count == 0)
				{
					MessageBox.Show("No data to save.", Utility.uManufacturer);
					return;
				}
				List<StockAdjustSettings> stockAdjustSettings = AniHelper.CopyListData<StockAdjustSettings>(exStockAdjustSettings.Cast<object>().ToList());
				stockAdjustSettings.ForEach(delegate(StockAdjustSettings x)
				{
					if (x.curActualStock == x.currSystemStock)
					{
						stockAdjustSettings.Remove(x);
					}
				});
				int? proBrand = null;
				int? protGroup = null;
				if (cmbProBrand.Text != string.Empty)
				{
					proBrand = cmbProBrand.SelectedValue.ToInt32();
				}
				if (cmbProGroup.Text != string.Empty)
				{
					protGroup = cmbProGroup.SelectedValue.ToInt32();
				}
				if (btnSave.Text == "Save")
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
						return;
					}
					int iCode = (db.StockAdjusterMasters().Max((StockAdjusterMaster x) => x.StockAdjusterMasterCode) + 1) ?? 1;
					StockAdjusterMaster data = new StockAdjusterMaster
					{
						stockAdjusterMasterId = null,
						ProBrand = proBrand,
						ProtGroup = protGroup,
						Remarks = txtRemarks.Text,
						Date = dtmDate.Value.Date,
						StockAdjusterMasterCode = iCode
					};
					if (AshproCommon.InsertToDatabaseObj(data, "StockAdjusterMaster"))
					{
						iStockAdjusterMaster = (from x in db.StockAdjusterMasters()
							where x.StockAdjusterMasterCode == iCode
							select x).FirstOrDefault().stockAdjusterMasterId.toInt32();
						stockAdjustSettings.ForEach(delegate(StockAdjustSettings x)
						{
							x.stockAdjusterMasterId = iStockAdjusterMaster;
						});
						if (AshproCommon.InsertToDatabase(stockAdjustSettings.Cast<object>().ToList(), "StockAdjustSettings"))
						{
							foreach (ExStockAdjustSettings x3 in exStockAdjustSettings)
							{
								int iGdwnId = (from y in db.Products()
									where y.ProductId == x3.productId
									select y).FirstOrDefault().GodwnId.ToInt32();
								POSHelper.STranLogMethod(iStockAdjusterMaster, iCode, "Stock Adjustment", "Stock Adjustment", -1, "Stock Adjustment", Settings.Default.intLogOnId, x3.Excess, x3.Short, x3.productId.toInt32(), x3.unitId.toInt32(), iGdwnId);
							}
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
					StockAdjusterMaster data2 = new StockAdjusterMaster
					{
						stockAdjusterMasterId = iStockAdjusterMaster,
						ProBrand = proBrand,
						ProtGroup = protGroup,
						Remarks = txtRemarks.Text,
						Date = dtmDate.Value.Date,
						StockAdjusterMasterCode = nudVchrNo.Value.ToInt32()
					};
					stockAdjustSettings.ForEach(delegate(StockAdjustSettings x)
					{
						x.stockAdjustSettingsId = null;
					});
					DeleteGodownStockTranLoag();
					DeleteSTranLogMethod();
					AshproCommon.DeleteFromDatabase("StockAdjustSettings", "stockAdjusterMasterId", iStockAdjusterMaster);
					if (AshproCommon.UpdateToDatabaseObj(data2, "StockAdjusterMaster", "stockAdjusterMasterId", iStockAdjusterMaster) && AshproCommon.InsertToDatabase(stockAdjustSettings.Cast<object>().ToList(), "StockAdjustSettings"))
					{
						foreach (ExStockAdjustSettings x2 in exStockAdjustSettings)
						{
							int iGdwnId2 = (from y in db.Products()
								where y.PGroupId == x2.productId
								select y).FirstOrDefault().GodwnId.toInt32();
							POSHelper.STranLogMethod(iStockAdjusterMaster, nudVchrNo.Value.ToInt32(), "Stock Adjustment", "Stock Adjustment", -1, "Stock Adjustment", Settings.Default.intLogOnId, x2.Excess, x2.Short, x2.productId.toInt32(), x2.unitId.toInt32(), iGdwnId2);
						}
					}
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void nudVchrNo_ValueChanged(object sender, EventArgs e)
		{
			StockAdjusterMaster stockAdjusterMaster = (from x in db.StockAdjusterMasters()
				where x.StockAdjusterMasterCode == nudVchrNo.Value.ToInt32()
				select x).FirstOrDefault();
			if (stockAdjusterMaster != null)
			{
				dtmDate.Value = stockAdjusterMaster.Date;
				txtRemarks.Text = stockAdjusterMaster.Remarks;
				cmbProGroup.SelectedValue = stockAdjusterMaster.ProtGroup.GetValueOrDefault();
				cmbProBrand.SelectedValue = stockAdjusterMaster.ProBrand.GetValueOrDefault();
				iStockAdjusterMaster = stockAdjusterMaster.stockAdjusterMasterId.toInt32();
				List<StockAdjustSettings> source = (from x in db.StockAdjustSettings()
					where x.stockAdjusterMasterId == iStockAdjusterMaster
					select x).ToList();
				exStockAdjustSettings = AniHelper.CopyListData<ExStockAdjustSettings>(source.Cast<object>().ToList());
				int num = 1;
				foreach (ExStockAdjustSettings x2 in exStockAdjustSettings)
				{
					ProductEntity productEntity = (from y in db.Products()
						where y.ProductId == x2.productId
						select y).FirstOrDefault();
					x2.SL = num;
					x2.ProductCode = productEntity.ProductCode.ToInt32();
					x2.ProductName = productEntity.ProductName;
					x2.UnitName = (from y in db.Units()
						where y.UnitId == x2.unitId
						select y).FirstOrDefault().UnitName;
					x2.Excess = ((x2.currSystemStock < x2.curActualStock) ? (x2.curActualStock - x2.currSystemStock) : 0m);
					x2.Short = ((x2.currSystemStock > x2.curActualStock) ? (x2.currSystemStock - x2.curActualStock) : 0m);
					x2.Rate = productEntity.SellingPrice;
					x2.TotalAmount = x2.currSystemStock * x2.Rate;
					num++;
				}
				BindDGV(exStockAdjustSettings);
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
				ExStockAdjustSettings item = (ExStockAdjustSettings)dgvDetails.CurrentRow.DataBoundItem;
				exStockAdjustSettings.Remove(item);
				exStockAdjustSettings.ForEach(delegate(ExStockAdjustSettings x)
				{
					x.SL = iCount;
					iCount++;
				});
				BindDGV(exStockAdjustSettings);
			}
		}

		private void StockAdjusterView_KeyDown(object sender, KeyEventArgs e)
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
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnLoad_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnExcel_Click(null, null);
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
			button4 = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnEdit = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			toolTip = new System.Windows.Forms.ToolTip(components);
			btnSave = new System.Windows.Forms.Button();
			btnLoad = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			dgvDetails = new System.Windows.Forms.DataGridView();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SysQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ActQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Excess = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Short = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			panel1 = new System.Windows.Forms.Panel();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			cmbProBrand = new AshUsercontrol.AshComboBox();
			cmbProGroup = new AshUsercontrol.AshComboBox();
			label4 = new System.Windows.Forms.Label();
			nudVchrNo = new AshUsercontrol.AshUpDown();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			cbEnableVchrNo = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			btnDate = new System.Windows.Forms.Button();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudVchrNo).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(735, 1);
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(163, 1);
			button4.TabIndex = 92;
			button4.UseVisualStyleBackColor = true;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(687, 416);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 86;
			btnClear.Text = "Clear";
			toolTip.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(534, 416);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 83;
			btnDelete.Text = "Delete";
			toolTip.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
			btnEdit.Enabled = false;
			btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnEdit.ForeColor = System.Drawing.Color.White;
			btnEdit.Location = new System.Drawing.Point(458, 416);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new System.Drawing.Size(75, 30);
			btnEdit.TabIndex = 82;
			btnEdit.Text = "Edit";
			toolTip.SetToolTip(btnEdit, "Press Alt + E");
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += new System.EventHandler(btnEdit_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.Enabled = false;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(611, 416);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(75, 30);
			btnExcel.TabIndex = 85;
			btnExcel.Text = "Excel";
			toolTip.SetToolTip(btnExcel, "Press Alt + X");
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(382, 416);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 81;
			btnSave.Text = "Save";
			toolTip.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoad.ForeColor = System.Drawing.Color.White;
			btnLoad.Location = new System.Drawing.Point(684, 92);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new System.Drawing.Size(75, 30);
			btnLoad.TabIndex = 80;
			btnLoad.Text = "Load";
			toolTip.SetToolTip(btnLoad, "Press Alt + L");
			btnLoad.UseVisualStyleBackColor = false;
			btnLoad.Click += new System.EventHandler(btnLoad_Click);
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 6);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(144, 24);
			lblHead.TabIndex = 91;
			lblHead.Text = "Stock Adjuster";
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_ProCode, col_ProName, col_SysQty, col_UnitName, col_ActQty, col_Excess, col_Short, col_Rate, col_TotalAmount);
			dgvDetails.Location = new System.Drawing.Point(13, 12);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.Size = new System.Drawing.Size(722, 257);
			dgvDetails.TabIndex = 0;
			dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellEndEdit);
			dgvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetails_KeyDown);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 40;
			col_ProCode.DataPropertyName = "ProductCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProCode.ReadOnly = true;
			col_ProCode.Width = 80;
			col_ProName.DataPropertyName = "ProductName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.ReadOnly = true;
			col_ProName.Width = 250;
			col_SysQty.DataPropertyName = "currSystemStock";
			col_SysQty.HeaderText = "Sys Qty";
			col_SysQty.Name = "col_SysQty";
			col_SysQty.ReadOnly = true;
			col_SysQty.Width = 80;
			col_UnitName.DataPropertyName = "UnitName";
			col_UnitName.HeaderText = "Unit";
			col_UnitName.Name = "col_UnitName";
			col_UnitName.ReadOnly = true;
			col_UnitName.Width = 60;
			col_ActQty.DataPropertyName = "curActualStock";
			col_ActQty.HeaderText = "Act Qty";
			col_ActQty.Name = "col_ActQty";
			col_ActQty.Width = 60;
			col_Excess.DataPropertyName = "Excess";
			col_Excess.HeaderText = "Excess";
			col_Excess.Name = "col_Excess";
			col_Excess.ReadOnly = true;
			col_Excess.Width = 50;
			col_Short.DataPropertyName = "Short";
			col_Short.HeaderText = "Short";
			col_Short.Name = "col_Short";
			col_Short.ReadOnly = true;
			col_Short.Width = 50;
			col_Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle;
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_Rate.ReadOnly = true;
			col_TotalAmount.DataPropertyName = "TotalAmount";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			col_TotalAmount.DefaultCellStyle = dataGridViewCellStyle2;
			col_TotalAmount.HeaderText = "Total Amount";
			col_TotalAmount.Name = "col_TotalAmount";
			col_TotalAmount.ReadOnly = true;
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(11, 128);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(750, 282);
			panel1.TabIndex = 90;
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
			txtRemarks.Location = new System.Drawing.Point(136, 106);
			txtRemarks.Margin = new System.Windows.Forms.Padding(4);
			txtRemarks.Name = "txtRemarks";
			txtRemarks.Size = new System.Drawing.Size(539, 16);
			txtRemarks.TabIndex = 79;
			txtRemarks.Value = new decimal(new int[4]);
			txtRemarks.WaterMark = null;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(60, 102);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(73, 16);
			label12.TabIndex = 78;
			label12.Text = "Remarks :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(77, 77);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(56, 16);
			label3.TabIndex = 76;
			label3.Text = "Brand :";
			cmbProBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbProBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProBrand.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProBrand.FormattingEnabled = true;
			cmbProBrand.LeaveColor = System.Drawing.Color.White;
			cmbProBrand.Location = new System.Drawing.Point(136, 77);
			cmbProBrand.Name = "cmbProBrand";
			cmbProBrand.Size = new System.Drawing.Size(208, 24);
			cmbProBrand.TabIndex = 77;
			cmbProGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbProGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProGroup.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProGroup.FormattingEnabled = true;
			cmbProGroup.LeaveColor = System.Drawing.Color.White;
			cmbProGroup.Location = new System.Drawing.Point(136, 48);
			cmbProGroup.Name = "cmbProGroup";
			cmbProGroup.Size = new System.Drawing.Size(208, 24);
			cmbProGroup.TabIndex = 75;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(20, 52);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(113, 16);
			label4.TabIndex = 74;
			label4.Text = "Product Group :";
			nudVchrNo.Enabled = false;
			nudVchrNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			nudVchrNo.Location = new System.Drawing.Point(307, 8);
			nudVchrNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudVchrNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudVchrNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVchrNo.Name = "nudVchrNo";
			nudVchrNo.Size = new System.Drawing.Size(91, 23);
			nudVchrNo.TabIndex = 71;
			nudVchrNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudVchrNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVchrNo.ValueChanged += new System.EventHandler(nudVchrNo_ValueChanged);
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(475, 3);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 73;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(426, 11);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(50, 16);
			label2.TabIndex = 72;
			label2.Text = "Date :";
			cbEnableVchrNo.AutoSize = true;
			cbEnableVchrNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbEnableVchrNo.Location = new System.Drawing.Point(405, 10);
			cbEnableVchrNo.Name = "cbEnableVchrNo";
			cbEnableVchrNo.Size = new System.Drawing.Size(15, 14);
			cbEnableVchrNo.TabIndex = 88;
			cbEnableVchrNo.UseVisualStyleBackColor = true;
			cbEnableVchrNo.CheckedChanged += new System.EventHandler(cbEnableVchrNo_CheckedChanged);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(210, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 16);
			label1.TabIndex = 70;
			label1.Text = "Invoice No :";
			btnDate.Location = new System.Drawing.Point(601, 3);
			btnDate.Name = "btnDate";
			btnDate.Size = new System.Drawing.Size(25, 23);
			btnDate.TabIndex = 89;
			btnDate.Text = "..";
			toolTip.SetToolTip(btnDate, "Press Alt + T");
			btnDate.UseVisualStyleBackColor = true;
			btnDate.Click += new System.EventHandler(btnDate_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(771, 452);
			base.Controls.Add(button4);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnEdit);
			base.Controls.Add(btnExcel);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnSave);
			base.Controls.Add(panel1);
			base.Controls.Add(btnLoad);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label12);
			base.Controls.Add(label3);
			base.Controls.Add(cmbProBrand);
			base.Controls.Add(cmbProGroup);
			base.Controls.Add(label4);
			base.Controls.Add(nudVchrNo);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label2);
			base.Controls.Add(cbEnableVchrNo);
			base.Controls.Add(label1);
			base.Controls.Add(btnDate);
			base.Name = "StockAdjusterView";
			base.Load += new System.EventHandler(StockAdjusterView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(StockAdjusterView_KeyDown);
			base.Controls.SetChildIndex(btnDate, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cbEnableVchrNo, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(nudVchrNo, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(cmbProGroup, 0);
			base.Controls.SetChildIndex(cmbProBrand, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(btnLoad, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(btnExcel, 0);
			base.Controls.SetChildIndex(btnEdit, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)nudVchrNo).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
