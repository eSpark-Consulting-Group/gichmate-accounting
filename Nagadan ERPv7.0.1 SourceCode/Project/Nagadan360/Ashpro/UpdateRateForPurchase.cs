using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class UpdateRateForPurchase : BaseForm
	{
		private IContainer components = null;

		private Label label5;

		private Label label1;

		private Label label2;

		private Button btnOK;

		private Button btnCancel;

		public AshTextBoxNormal txtProductName;

		public AshTextBoxNormal txtBarcode;

		private DataGridView dgDetails;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_SLNo;

		private DataGridViewTextBoxColumn col_PriceCategoryName;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_UnitName;

		private DataGridViewTextBoxColumn col_SaleRate;

		private DataGridViewTextBoxColumn col_SaleDiscount;

		private DataGridViewTextBoxColumn col_PurchaseRate;

		private DataGridViewTextBoxColumn col_PurchaseDiscount;

		public List<ExProductPrice> productPrices { get; set; }

		public UpdateRateForPurchase()
		{
			InitializeComponent();
			ColorChange.Color(this);
			btnEnd.Visible = false;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void txtNewSalesPrice_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnCancel_Click(null, null);
			}
		}

		private void UpdateRateForPurchase_Load(object sender, EventArgs e)
		{
			dgDetails.AutoGenerateColumns = false;
			dgDetails.DataSource = null;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = productPrices;
			dgDetails.DataSource = bdsDetails;
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
			label5 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtProductName = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			txtBarcode = new AshUsercontrol.AshTextBoxNormal();
			btnOK = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			dgDetails = new System.Windows.Forms.DataGridView();
			col_SLNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PriceCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SaleDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PurchaseRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PurchaseDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(595, 0);
			btnEnd.Margin = new System.Windows.Forms.Padding(4);
			btnEnd.Size = new System.Drawing.Size(47, 43);
			btnEnd.TabIndex = 14;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label5.Location = new System.Drawing.Point(-1, 0);
			label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(147, 24);
			label5.TabIndex = 15;
			label5.Text = "Price Updation";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 41);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(111, 16);
			label1.TabIndex = 0;
			label1.Text = "Product Name :";
			txtProductName.BackColor = System.Drawing.Color.Gainsboro;
			txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProductName.Data = 0;
			txtProductName.EnterColor = System.Drawing.Color.LightPink;
			txtProductName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProductName.Format = null;
			txtProductName.isAllowNegative = false;
			txtProductName.isAllowSpecialChar = false;
			txtProductName.isNumberOnly = false;
			txtProductName.isNumeric = false;
			txtProductName.LeaveColor = System.Drawing.Color.White;
			txtProductName.Location = new System.Drawing.Point(124, 41);
			txtProductName.Margin = new System.Windows.Forms.Padding(4);
			txtProductName.Name = "txtProductName";
			txtProductName.ReadOnly = true;
			txtProductName.Size = new System.Drawing.Size(279, 16);
			txtProductName.TabIndex = 1;
			txtProductName.TabStop = false;
			txtProductName.Value = new decimal(new int[4]);
			txtProductName.WaterMark = null;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(410, 41);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(72, 16);
			label2.TabIndex = 2;
			label2.Text = "Barcode :";
			txtBarcode.BackColor = System.Drawing.Color.Gainsboro;
			txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBarcode.Data = 0;
			txtBarcode.EnterColor = System.Drawing.Color.LightPink;
			txtBarcode.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtBarcode.Format = null;
			txtBarcode.isAllowNegative = false;
			txtBarcode.isAllowSpecialChar = false;
			txtBarcode.isNumberOnly = false;
			txtBarcode.isNumeric = false;
			txtBarcode.LeaveColor = System.Drawing.Color.White;
			txtBarcode.Location = new System.Drawing.Point(480, 41);
			txtBarcode.Margin = new System.Windows.Forms.Padding(4);
			txtBarcode.Name = "txtBarcode";
			txtBarcode.ReadOnly = true;
			txtBarcode.Size = new System.Drawing.Size(149, 16);
			txtBarcode.TabIndex = 3;
			txtBarcode.TabStop = false;
			txtBarcode.Value = new decimal(new int[4]);
			txtBarcode.WaterMark = null;
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatAppearance.BorderSize = 0;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnOK.Location = new System.Drawing.Point(478, 227);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 30);
			btnOK.TabIndex = 12;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.FlatAppearance.BorderSize = 0;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnCancel.Location = new System.Drawing.Point(559, 227);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 30);
			btnCancel.TabIndex = 13;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.BackgroundColor = System.Drawing.Color.Gainsboro;
			dgDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(col_SLNo, col_PriceCategoryName, col_ProName, col_UnitName, col_SaleRate, col_SaleDiscount, col_PurchaseRate, col_PurchaseDiscount);
			dgDetails.Location = new System.Drawing.Point(18, 65);
			dgDetails.Margin = new System.Windows.Forms.Padding(4);
			dgDetails.Name = "dgDetails";
			dgDetails.RowHeadersVisible = false;
			dgDetails.RowHeadersWidth = 51;
			dgDetails.Size = new System.Drawing.Size(613, 140);
			dgDetails.TabIndex = 16;
			col_SLNo.DataPropertyName = "SL";
			col_SLNo.HeaderText = "SL";
			col_SLNo.MinimumWidth = 6;
			col_SLNo.Name = "col_SLNo";
			col_SLNo.Width = 30;
			col_PriceCategoryName.DataPropertyName = "PCategoryName";
			col_PriceCategoryName.HeaderText = "Price Category";
			col_PriceCategoryName.MinimumWidth = 6;
			col_PriceCategoryName.Name = "col_PriceCategoryName";
			col_PriceCategoryName.Width = 125;
			col_ProName.DataPropertyName = "ProName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_UnitName.DataPropertyName = "UnitName";
			col_UnitName.HeaderText = "Unit";
			col_UnitName.MinimumWidth = 6;
			col_UnitName.Name = "col_UnitName";
			col_UnitName.Width = 50;
			col_SaleRate.DataPropertyName = "SPrice";
			col_SaleRate.HeaderText = "Sale Rate";
			col_SaleRate.MinimumWidth = 6;
			col_SaleRate.Name = "col_SaleRate";
			col_SaleRate.Width = 80;
			col_SaleDiscount.DataPropertyName = "SaleDiscount";
			col_SaleDiscount.HeaderText = "Sale Dis";
			col_SaleDiscount.MinimumWidth = 6;
			col_SaleDiscount.Name = "col_SaleDiscount";
			col_SaleDiscount.Width = 80;
			col_PurchaseRate.DataPropertyName = "PPrice";
			col_PurchaseRate.HeaderText = "Pur Rate";
			col_PurchaseRate.MinimumWidth = 6;
			col_PurchaseRate.Name = "col_PurchaseRate";
			col_PurchaseRate.Width = 80;
			col_PurchaseDiscount.DataPropertyName = "PurchaseDiscount";
			col_PurchaseDiscount.HeaderText = "Pur Dis";
			col_PurchaseDiscount.MinimumWidth = 6;
			col_PurchaseDiscount.Name = "col_PurchaseDiscount";
			col_PurchaseDiscount.Width = 80;
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Gainsboro;
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(642, 260);
			base.Controls.Add(dgDetails);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.Controls.Add(txtBarcode);
			base.Controls.Add(label2);
			base.Controls.Add(txtProductName);
			base.Controls.Add(label1);
			base.Controls.Add(label5);
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "UpdateRateForPurchase";
			base.Load += new System.EventHandler(UpdateRateForPurchase_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtProductName, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtBarcode, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
