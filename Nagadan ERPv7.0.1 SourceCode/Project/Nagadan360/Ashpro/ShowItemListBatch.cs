using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class ShowItemListBatch : BaseForm
	{
		private List<ComboFill> entProList = new List<ComboFill>();

		private IContainer components = null;

		private AshTextBoxNormal txtSearch;

		private PictureBox pcSearch;

		private DataGridView dgProduct;

		private DataGridViewTextBoxColumn col_Id;

		private DataGridViewTextBoxColumn col_Name;

		private DataGridView dgvDetails;

		private DataGridViewTextBoxColumn col_ProductCode;

		private DataGridViewTextBoxColumn col_BatchId;

		private DataGridViewTextBoxColumn col_BatchCode;

		private DataGridViewTextBoxColumn col_BatchName;

		private DataGridViewTextBoxColumn col_SellingPrice;

		private DataGridViewTextBoxColumn col_PurPrice;

		private DataGridViewTextBoxColumn col_MRP;

		private DataGridViewTextBoxColumn col_ExpDate;

		private DataGridViewTextBoxColumn col_Stock;

		private DataGridViewTextBoxColumn col_Godown;

		private DataGridViewTextBoxColumn col_Shelf;

		public int iId { get; set; }

		public string sName { get; set; }

		public string ProductId { get; set; }

		public string ProName { get; set; }

		public ShowItemListBatch()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void LoadDgProducts()
		{
			entProList = AshproCommon.GetListMethod<ComboFill>("Select ProductId as Id, ProductName as Name From Product Where  ProductName like'%" + txtSearch.Text + "%'");
			dgProduct.DataSource = entProList;
			if (entProList.Count > 0)
			{
				int num = dgProduct.Rows[0].Cells[0].Value.ToInt32();
				DGDetailsLoadMethod(num);
			}
		}

		private void DGDetailsLoadMethod(int iId)
		{
			dgvDetails.DataSource = AshproCommon.GetDataTable("SELECT Product.ProductCode,Batch.Code,Batch.Name,Batch.SalesRate,Batch.PurchaseRate,Batch.MRP,Batch.ExpairyDate,sum(StockTranLog.InQuantity-StockTranLog.OutQuantity) as Stock,Godown.GodownName,Self.SelfName FROM StockTranLog Join Product on StockTranLog.ProductId=Product.ProductId Join Batch on Batch.FK_ProductId = Product.ProductId Join Godown on Product.GodwnId= Godown.GodwnId Join Self on Product.SelfId= Self.SelfId WHERE Product.ProductId = '" + iId + "' and StockTranLog.VchrDate <= '" + AniHelper.GetDate(DateTime.Now) + "' Group by Product.ProductCode,Batch.Code,Batch.Name,Batch.SalesRate,Batch.PurchaseRate,Batch.MRP,Batch.ExpairyDate,Godown.GodownName,Self.SelfName");
		}

		private void ShowItemListBatch_Load(object sender, EventArgs e)
		{
			txtSearch.Text = ProName;
			txtSearch.SelectionStart = txtSearch.Text.Length;
			LoadDgProducts();
		}

		private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dgProduct.Rows.Count > 0)
			{
				ComboFill comboFill = (ComboFill)dgProduct.CurrentRow.DataBoundItem;
				iId = comboFill.Id;
				sName = comboFill.Name;
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			LoadDgProducts();
		}

		private void dgProduct_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (dgProduct.Rows.Count > 0)
				{
					ComboFill comboFill = (ComboFill)dgProduct.CurrentRow.DataBoundItem;
					iId = comboFill.Id;
					sName = comboFill.Name;
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
				ComboFill comboFill = (ComboFill)dgProduct.CurrentRow.DataBoundItem;
				if (e.KeyCode == Keys.Return)
				{
					try
					{
						if (dgProduct.Rows.Count > 0)
						{
							iId = comboFill.Id;
							sName = comboFill.Name;
							base.DialogResult = DialogResult.OK;
							Close();
						}
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						return;
					}
				}
				if (e.KeyCode == Keys.Left)
				{
					dgvDetails.Focus();
				}
				else if (e.KeyCode == Keys.Up)
				{
					int num = dgProduct.Rows[dgProduct.CurrentCell.RowIndex - 1].Cells[0].Value.ToInt32();
					DGDetailsLoadMethod(num);
				}
				else if (e.KeyCode == Keys.Down)
				{
					int num2 = dgProduct.CurrentCell.RowIndex + 1;
					if (num2 < dgProduct.Rows.Count)
					{
						int num3 = dgProduct.Rows[num2].Cells[0].Value.ToInt32();
						DGDetailsLoadMethod(num3);
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
				ComboFill comboFill = (ComboFill)dgProduct.CurrentRow.DataBoundItem;
				iId = comboFill.Id;
				sName = comboFill.Name;
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && dgProduct.Rows.Count > 0)
			{
				ComboFill comboFill = (ComboFill)dgProduct.CurrentRow.DataBoundItem;
				iId = comboFill.Id;
				sName = comboFill.Name;
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void ShowItemListBatch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.F3)
			{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.ShowItemListBatch));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			txtSearch = new AshUsercontrol.AshTextBoxNormal();
			pcSearch = new System.Windows.Forms.PictureBox();
			dgProduct = new System.Windows.Forms.DataGridView();
			col_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dgvDetails = new System.Windows.Forms.DataGridView();
			col_ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_BatchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_BatchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_BatchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PurPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ExpDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Godown = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Shelf = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pcSearch).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgProduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(1012, 0);
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
			txtSearch.Location = new System.Drawing.Point(72, 9);
			txtSearch.Margin = new System.Windows.Forms.Padding(4);
			txtSearch.Name = "txtSearch";
			txtSearch.Size = new System.Drawing.Size(660, 16);
			txtSearch.TabIndex = 44;
			txtSearch.Value = new decimal(new int[4]);
			txtSearch.WaterMark = "Search";
			txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
			pcSearch.BackgroundImage = (System.Drawing.Image)resources.GetObject("pcSearch.BackgroundImage");
			pcSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			pcSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			pcSearch.Location = new System.Drawing.Point(21, 6);
			pcSearch.Name = "pcSearch";
			pcSearch.Size = new System.Drawing.Size(17, 20);
			pcSearch.TabIndex = 47;
			pcSearch.TabStop = false;
			dgProduct.AllowUserToAddRows = false;
			dgProduct.AllowUserToDeleteRows = false;
			dgProduct.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgProduct.ColumnHeadersHeight = 40;
			dgProduct.Columns.AddRange(col_Id, col_Name);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgProduct.DefaultCellStyle = dataGridViewCellStyle2;
			dgProduct.EnableHeadersVisualStyles = false;
			dgProduct.Location = new System.Drawing.Point(1, 33);
			dgProduct.Name = "dgProduct";
			dgProduct.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dgProduct.RowHeadersVisible = false;
			dgProduct.Size = new System.Drawing.Size(270, 221);
			dgProduct.TabIndex = 45;
			dgProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgProduct_CellContentClick);
			dgProduct.DoubleClick += new System.EventHandler(dgProduct_DoubleClick);
			dgProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(dgProduct_KeyDown);
			col_Id.DataPropertyName = "Id";
			col_Id.HeaderText = "Id";
			col_Id.Name = "col_Id";
			col_Id.ReadOnly = true;
			col_Id.Visible = false;
			col_Name.DataPropertyName = "Name";
			col_Name.HeaderText = "Name";
			col_Name.Name = "col_Name";
			col_Name.ReadOnly = true;
			col_Name.Width = 265;
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			dgvDetails.ColumnHeadersHeight = 40;
			dgvDetails.Columns.AddRange(col_ProductCode, col_BatchId, col_BatchCode, col_BatchName, col_SellingPrice, col_PurPrice, col_MRP, col_ExpDate, col_Stock, col_Godown, col_Shelf);
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle6;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.Location = new System.Drawing.Point(273, 36);
			dgvDetails.Margin = new System.Windows.Forms.Padding(4);
			dgvDetails.Name = "dgvDetails";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
			dgvDetails.RowHeadersVisible = false;
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
			dgvDetails.RowsDefaultCellStyle = dataGridViewCellStyle8;
			dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvDetails.Size = new System.Drawing.Size(773, 218);
			dgvDetails.TabIndex = 46;
			dgvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellContentClick);
			dgvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetails_KeyDown);
			col_ProductCode.HeaderText = "Pro Code";
			col_ProductCode.Name = "col_ProductCode";
			col_ProductCode.ReadOnly = true;
			col_ProductCode.Width = 60;
			col_BatchId.HeaderText = "BatchId";
			col_BatchId.Name = "col_BatchId";
			col_BatchId.Visible = false;
			col_BatchCode.HeaderText = "Batch Code";
			col_BatchCode.Name = "col_BatchCode";
			col_BatchCode.ReadOnly = true;
			col_BatchCode.Width = 60;
			col_BatchName.HeaderText = "Batch Name";
			col_BatchName.Name = "col_BatchName";
			col_BatchName.ReadOnly = true;
			col_SellingPrice.HeaderText = "Selling Price";
			col_SellingPrice.Name = "col_SellingPrice";
			col_SellingPrice.ReadOnly = true;
			col_SellingPrice.Width = 80;
			col_PurPrice.HeaderText = "Pur Price";
			col_PurPrice.Name = "col_PurPrice";
			col_PurPrice.ReadOnly = true;
			col_PurPrice.Width = 80;
			col_MRP.HeaderText = "MRP";
			col_MRP.Name = "col_MRP";
			col_MRP.ReadOnly = true;
			col_MRP.Width = 80;
			col_ExpDate.HeaderText = "Exp Date";
			col_ExpDate.Name = "col_ExpDate";
			col_ExpDate.ReadOnly = true;
			col_Stock.HeaderText = "Stock";
			col_Stock.Name = "col_Stock";
			col_Stock.ReadOnly = true;
			col_Stock.Width = 80;
			col_Godown.HeaderText = "Godown";
			col_Godown.Name = "col_Godown";
			col_Godown.ReadOnly = true;
			col_Godown.Width = 80;
			col_Shelf.HeaderText = "Shelf";
			col_Shelf.Name = "col_Shelf";
			col_Shelf.ReadOnly = true;
			col_Shelf.Width = 60;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnEnd;
			base.ClientSize = new System.Drawing.Size(1047, 261);
			base.Controls.Add(txtSearch);
			base.Controls.Add(pcSearch);
			base.Controls.Add(dgProduct);
			base.Controls.Add(dgvDetails);
			base.Name = "ShowItemListBatch";
			base.Load += new System.EventHandler(ShowItemListBatch_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ShowItemListBatch_KeyDown);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(dgvDetails, 0);
			base.Controls.SetChildIndex(dgProduct, 0);
			base.Controls.SetChildIndex(pcSearch, 0);
			base.Controls.SetChildIndex(txtSearch, 0);
			((System.ComponentModel.ISupportInitialize)pcSearch).EndInit();
			((System.ComponentModel.ISupportInitialize)dgProduct).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
