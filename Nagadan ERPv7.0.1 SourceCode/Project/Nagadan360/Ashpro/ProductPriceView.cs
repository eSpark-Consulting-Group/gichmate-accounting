using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;

namespace Ashpro
{
	public class ProductPriceView : BaseForm
	{
		private int iPriceCatId;

		private DBContext db;

		private List<ExProductPrice> entExProductPrices;

		private IContainer components = null;

		private Panel panel1;

		private DataGridView dgvDetails;

		private Button btnSave;

		private Button button4;

		private Label lblHead;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_ActualRate;

		public ProductPriceView(int iPCId)
		{
			InitializeComponent();
			ColorChange.Color(this);
			iPriceCatId = iPCId;
		}

		private void ProductLoadMethod(int iPId, int iPCod, string sPName)
		{
			entExProductPrices = new List<ExProductPrice>();
			int sL = 1;
			foreach (ProductPriceCategory x2 in from x in db.ProductPriceCategories()
				where x.ProductId == iPCod && x.ProPrceCatId == iPriceCatId
				select x)
			{
				ExProductPrice exProductPrice = new ExProductPrice();
				GlobalFunctions.CopyData(x2, exProductPrice);
				exProductPrice.SL = sL;
				exProductPrice.ProductId = iPCod;
				exProductPrice.ProName = sPName;
				exProductPrice.UnitName = (from y in db.Units()
					where y.UnitId == x2.UnitId
					select y).FirstOrDefault().UnitName;
				entExProductPrices.Add(exProductPrice);
			}
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = entExProductPrices;
			dgvDetails.DataSource = bdsDetails;
		}

		private void ProductPriceView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			foreach (ProductEntity item in db.Products())
			{
				ProductLoadMethod(item.ProductId.ToInt32(), item.ProductCode.toInt32(), item.ProductName);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			foreach (ExProductPrice entExProductPrice in entExProductPrices)
			{
				entExProductPrice.SPrice = entExProductPrice.Rate;
			}
			List<ProductPriceCategory> list = new List<ProductPriceCategory>();
			list = AniHelper.CopyListData<ProductPriceCategory>(entExProductPrices.Cast<object>().ToList());
			if (AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "ProductPriceCategory"))
			{
				Messages.SavedMessage();
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
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new System.Windows.Forms.DataGridView();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ActualRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btnSave = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(644, -1);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Location = new System.Drawing.Point(11, 48);
			panel1.Margin = new System.Windows.Forms.Padding(5);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(654, 414);
			panel1.TabIndex = 104;
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dgvDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_ProCode, col_ProName, col_Unit, col_Rate, col_ActualRate);
			dgvDetails.Location = new System.Drawing.Point(9, 10);
			dgvDetails.Margin = new System.Windows.Forms.Padding(5);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.Size = new System.Drawing.Size(634, 395);
			dgvDetails.TabIndex = 1;
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 40;
			col_ProCode.DataPropertyName = "ProCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProCode.ReadOnly = true;
			col_ProCode.Width = 90;
			col_ProName.DataPropertyName = "ProName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.ReadOnly = true;
			col_ProName.Width = 230;
			col_Unit.DataPropertyName = "UnitName";
			col_Unit.HeaderText = "Unit";
			col_Unit.Name = "col_Unit";
			col_Unit.ReadOnly = true;
			col_Unit.Width = 70;
			col_Rate.DataPropertyName = "Rate";
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_ActualRate.DataPropertyName = "SPrice";
			col_ActualRate.HeaderText = "Actual Rate";
			col_ActualRate.Name = "col_ActualRate";
			col_ActualRate.ReadOnly = true;
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(565, 472);
			btnSave.Margin = new System.Windows.Forms.Padding(5);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(100, 37);
			btnSave.TabIndex = 102;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			button4.Location = new System.Drawing.Point(0, 41);
			button4.Margin = new System.Windows.Forms.Padding(4);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(179, 1);
			button4.TabIndex = 106;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(12, 11);
			lblHead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(136, 24);
			lblHead.TabIndex = 105;
			lblHead.Text = "Product Price";
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(680, 516);
			base.Controls.Add(panel1);
			base.Controls.Add(btnSave);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "ProductPriceView";
			base.Load += new System.EventHandler(ProductPriceView_Load);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
