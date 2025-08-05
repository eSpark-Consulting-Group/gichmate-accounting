using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ShowItemList : BaseForm
	{
		private List<ComboFill> entProList = new List<ComboFill>();

		private List<ExProductSearch> proList;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private DataGridView dgvDetails;

		private DataGridView dgProduct;

		private AshTextBoxNormal txtSearch;

		private PictureBox pcSearch;

		private DataGridViewTextBoxColumn col_Id;

		private DataGridViewTextBoxColumn col_Name;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProductName;

		private DataGridViewTextBoxColumn col_Type;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_SalePrice;

		private DataGridViewTextBoxColumn col_PPrice;

		private DataGridViewTextBoxColumn col_Discount;

		private DataGridViewTextBoxColumn col_PurDisc;

		public string ProName { get; set; }

		public int ProPriceCatId { get; set; }

		public ExProductSearch proSearch { get; set; }

		public List<ComboFill> productList { get; set; }

		public ShowItemList(bool isPurchase = false)
		{
			InitializeComponent();
			ColorChange.Color(this);
			if (isPurchase)
			{
				col_Discount.Visible = false;
			}
			else
			{
				col_PurDisc.Visible = false;
			}
			col_PPrice.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SalePrice.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_PurDisc.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
		}

		private async void LoadDgProducts(string sText)
		{
			entProList = await Task.Run(() => productList.Where((ComboFill x) => x.Name.ToUpper().Contains(sText.ToUpper())).ToList());
			dgProduct.DataSource = entProList;
			if (entProList.Count > 0)
			{
				int pId = dgProduct.Rows[0].Cells[0].Value.ToInt32();
				DGDetailsLoadMethod(pId);
			}
		}

		private void DGDetailsLoadMethod(int iId)
		{
			proList = new List<ExProductSearch>();
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch)
			{
				text = " and ProductBarcode.FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			DataTable dataTable = AshproCommon.GetDataTable("SELECT ProductBarcode.Barcode,ProductBarcode.BarcodeType,ProductBarcode.FK_UnitId, Unit.UnitName,ProductPriceCategory.SPrice,ProductPriceCategory.PPrice,ProductPriceCategory.SPDisc,ProductBarcodeName,FK_ProductId,ProductPriceCategory.PDisc FROM ProductBarcode Left Join ProductPriceCategory on ProductBarcode.Barcode = ProductPriceCategory.Barcode Left Join Unit on ProductBarcode.FK_UnitId = Unit.UnitId  WHERE ProductBarcode.FK_ProductId = '" + iId + "' and ProductPriceCategory.PCategoryId='" + ProPriceCatId + "'" + text);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					ExProductSearch exProductSearch = new ExProductSearch();
					exProductSearch.Barcode = row["Barcode"].ToString();
					exProductSearch.BarcodeType = row["BarcodeType"].ToString();
					int unitId = (exProductSearch.UnitId = row["FK_UnitId"].ToInt32());
					exProductSearch.UnitName = row["UnitName"].ToString();
					exProductSearch.SPrice = row["SPrice"].ToString().ToDecimal();
					exProductSearch.PPrice = row["PPrice"].ToString().ToDecimal();
					exProductSearch.SPDisc = row["SPDisc"].ToString().ToDecimal();
					string text2 = row["ProductBarcodeName"].ToString();
					exProductSearch.ProductName = ((text2 == string.Empty) ? db.Productunits(new
					{
						ProductId = iId,
						UnitId = unitId
					}).FirstOrDefault().ProductUnitName : text2.ToString2());
					exProductSearch.ProductId = row["FK_ProductId"].ToInt32();
					exProductSearch.PPDisc = row["PDisc"].ToString().ToDecimal();
					proList.Add(exProductSearch);
				}
			}
			dgvDetails.AutoGenerateColumns = false;
			dgvDetails.DataSource = null;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = proList;
			dgvDetails.DataSource = bdsDetails;
		}

		private void ShowItemList_Load(object sender, EventArgs e)
		{
			txtSearch.Text = ProName;
			txtSearch.SelectionStart = txtSearch.Text.Length;
			LoadDgProducts(ProName);
		}

		private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				proSearch = (ExProductSearch)dgvDetails.CurrentRow.DataBoundItem;
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			string sText = txtSearch.Text;
			LoadDgProducts(sText);
		}

		private void dgProduct_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgProduct.Rows.Count > 0)
				{
					proSearch = proList.FirstOrDefault();
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dgProduct_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					try
					{
						proSearch = proList.FirstOrDefault();
						base.DialogResult = DialogResult.OK;
						Close();
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						return;
					}
				}
				if (e.KeyCode == Keys.Right)
				{
					dgvDetails.Focus();
				}
				else if (e.KeyCode == Keys.Up)
				{
					int iId = dgProduct.Rows[dgProduct.CurrentCell.RowIndex - 1].Cells[0].Value.ToInt32();
					DGDetailsLoadMethod(iId);
				}
				else if (e.KeyCode == Keys.Down)
				{
					int num = dgProduct.CurrentCell.RowIndex + 1;
					if (num < dgProduct.Rows.Count)
					{
						int iId2 = dgProduct.Rows[num].Cells[0].Value.ToInt32();
						DGDetailsLoadMethod(iId2);
					}
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
			}
		}

		private void dgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dgProduct.Rows.Count > 0)
			{
				int iId = dgProduct.Rows[dgProduct.CurrentCell.RowIndex].Cells[0].Value.ToInt32();
				DGDetailsLoadMethod(iId);
				proSearch = proList.FirstOrDefault();
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				proSearch = (ExProductSearch)dgvDetails.CurrentRow.DataBoundItem;
				base.DialogResult = DialogResult.OK;
				Close();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.ShowItemList));
			dgvDetails = new System.Windows.Forms.DataGridView();
			dgProduct = new System.Windows.Forms.DataGridView();
			col_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			txtSearch = new AshUsercontrol.AshTextBoxNormal();
			pcSearch = new System.Windows.Forms.PictureBox();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PurDisc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)pcSearch).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(974, 1);
			btnEnd.Margin = new System.Windows.Forms.Padding(5);
			btnEnd.Size = new System.Drawing.Size(48, 35);
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.ColumnHeadersHeight = 50;
			dgvDetails.Columns.AddRange(col_Barcode, col_ProductName, col_Type, col_Unit, col_SalePrice, col_PPrice, col_Discount, col_PurDisc);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle3;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.Location = new System.Drawing.Point(278, 39);
			dgvDetails.Margin = new System.Windows.Forms.Padding(4);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.RowHeadersWidth = 51;
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dgvDetails.RowsDefaultCellStyle = dataGridViewCellStyle4;
			dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvDetails.Size = new System.Drawing.Size(743, 243);
			dgvDetails.TabIndex = 2;
			dgvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellContentClick);
			dgvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetails_KeyDown);
			dgProduct.AllowUserToAddRows = false;
			dgProduct.AllowUserToDeleteRows = false;
			dgProduct.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			dgProduct.ColumnHeadersHeight = 40;
			dgProduct.Columns.AddRange(col_Id, col_Name);
			dgProduct.EnableHeadersVisualStyles = false;
			dgProduct.Location = new System.Drawing.Point(1, 39);
			dgProduct.Name = "dgProduct";
			dgProduct.ReadOnly = true;
			dgProduct.RowHeadersVisible = false;
			dgProduct.RowHeadersWidth = 51;
			dgProduct.Size = new System.Drawing.Size(270, 244);
			dgProduct.TabIndex = 1;
			dgProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgProduct_CellContentClick);
			dgProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(dgProduct_KeyDown);
			dgProduct.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(dgProduct_MouseDoubleClick);
			col_Id.DataPropertyName = "Id";
			col_Id.HeaderText = "Id";
			col_Id.MinimumWidth = 6;
			col_Id.Name = "col_Id";
			col_Id.ReadOnly = true;
			col_Id.Visible = false;
			col_Id.Width = 125;
			col_Name.DataPropertyName = "Name";
			col_Name.HeaderText = "Name";
			col_Name.MinimumWidth = 6;
			col_Name.Name = "col_Name";
			col_Name.ReadOnly = true;
			col_Name.Width = 265;
			txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSearch.Data = 0;
			txtSearch.EnterColor = System.Drawing.Color.LightPink;
			txtSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSearch.Format = null;
			txtSearch.isAllowNegative = false;
			txtSearch.isAllowSpecialChar = false;
			txtSearch.isNumberOnly = false;
			txtSearch.isNumeric = false;
			txtSearch.LeaveColor = System.Drawing.Color.White;
			txtSearch.Location = new System.Drawing.Point(72, 15);
			txtSearch.Margin = new System.Windows.Forms.Padding(4);
			txtSearch.Name = "txtSearch";
			txtSearch.Size = new System.Drawing.Size(660, 20);
			txtSearch.TabIndex = 0;
			txtSearch.Value = new decimal(new int[4]);
			txtSearch.WaterMark = "Search";
			txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
			pcSearch.BackgroundImage = (System.Drawing.Image)resources.GetObject("pcSearch.BackgroundImage");
			pcSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			pcSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			pcSearch.Location = new System.Drawing.Point(21, 12);
			pcSearch.Name = "pcSearch";
			pcSearch.Size = new System.Drawing.Size(17, 20);
			pcSearch.TabIndex = 43;
			pcSearch.TabStop = false;
			col_Barcode.DataPropertyName = "Barcode";
			col_Barcode.HeaderText = "Barcode";
			col_Barcode.MinimumWidth = 6;
			col_Barcode.Name = "col_Barcode";
			col_Barcode.Width = 110;
			col_ProductName.DataPropertyName = "ProductName";
			col_ProductName.HeaderText = "Product Name";
			col_ProductName.MinimumWidth = 6;
			col_ProductName.Name = "col_ProductName";
			col_ProductName.Width = 225;
			col_Type.DataPropertyName = "BarcodeType";
			col_Type.HeaderText = "Barcode Type";
			col_Type.MinimumWidth = 6;
			col_Type.Name = "col_Type";
			col_Type.Width = 125;
			col_Unit.DataPropertyName = "UnitName";
			col_Unit.HeaderText = "Unit";
			col_Unit.MinimumWidth = 6;
			col_Unit.Name = "col_Unit";
			col_Unit.Width = 50;
			col_SalePrice.DataPropertyName = "SPrice";
			col_SalePrice.HeaderText = "Sale Price";
			col_SalePrice.MinimumWidth = 6;
			col_SalePrice.Name = "col_SalePrice";
			col_SalePrice.Width = 80;
			col_PPrice.DataPropertyName = "PPrice";
			col_PPrice.HeaderText = "Purchase Price";
			col_PPrice.MinimumWidth = 6;
			col_PPrice.Name = "col_PPrice";
			col_PPrice.Width = 80;
			col_Discount.DataPropertyName = "SPDisc";
			col_Discount.HeaderText = "Disc";
			col_Discount.MinimumWidth = 6;
			col_Discount.Name = "col_Discount";
			col_Discount.Width = 80;
			col_PurDisc.DataPropertyName = "PPDisc";
			col_PurDisc.HeaderText = "Disc";
			col_PurDisc.MinimumWidth = 6;
			col_PurDisc.Name = "col_PurDisc";
			col_PurDisc.Width = 125;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 20f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1024, 287);
			base.Controls.Add(txtSearch);
			base.Controls.Add(pcSearch);
			base.Controls.Add(dgProduct);
			base.Controls.Add(dgvDetails);
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Name = "ShowItemList";
			base.Load += new System.EventHandler(ShowItemList_Load);
			base.Controls.SetChildIndex(dgvDetails, 0);
			base.Controls.SetChildIndex(dgProduct, 0);
			base.Controls.SetChildIndex(pcSearch, 0);
			base.Controls.SetChildIndex(txtSearch, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)dgProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)pcSearch).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
