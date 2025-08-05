using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class POSStockView : BaseForm
	{
		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private Label lblProductName;

		private Label label3;

		private Label lblStock;

		private Label LabelPurchasePrice;

		private Label lblPurchasePrice;

		private Label LabelHPurchasePrice;

		private Label lblHPurchasePrice;

		private Label LabelAPurchasePrice;

		private Label lblAPurchasePrice;

		private Label LabelLPurchasePrice;

		private Label lblLPurchasePrice;

		public int ProductId { get; set; }

		public decimal dStock { get; set; }

		public int UnitId { get; set; }

		public int PCategoryId { get; set; }

		public POSStockView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void POSStockView_Load(object sender, EventArgs e)
		{
			try
			{
				DBContext dBContext = new DBContext();
				ProductEntity productEntity = dBContext.Products(new { ProductId }).ToList().FirstOrDefault();
				UnitEntity unitEntity = dBContext.Units(new { UnitId }).FirstOrDefault();
				ProductUnit productUnit = dBContext.Productunits(new { ProductId, UnitId }).FirstOrDefault();
				ProductPriceCategory productPriceCategory = dBContext.ProductPriceCategories(new { ProductId, UnitId, PCategoryId }).FirstOrDefault();
				lblProductName.Text = productEntity.ProductName;
				if (!CommonSettings.blnShowPurchasePrice)
				{
					return;
				}
				base.Size = new Size(530, 260);
				Label labelPurchasePrice = LabelPurchasePrice;
				Label label = lblPurchasePrice;
				Label labelHPurchasePrice = LabelHPurchasePrice;
				lblHPurchasePrice.Visible = true;
				labelHPurchasePrice.Visible = true;
				label.Visible = true;
				labelPurchasePrice.Visible = true;
				Label labelAPurchasePrice = LabelAPurchasePrice;
				Label label2 = lblAPurchasePrice;
				Label labelLPurchasePrice = LabelLPurchasePrice;
				lblLPurchasePrice.Visible = true;
				labelLPurchasePrice.Visible = true;
				label2.Visible = true;
				labelAPurchasePrice.Visible = true;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTableWithIdParameter_SP("spProductPurchasePrice", new
				{
					ProductId = productEntity.ProductId.toInt32()
				});
				foreach (DataRow row in dataTable.Rows)
				{
					decimal num = row["Highest"].ToString().ToDecimal() * productUnit.UnitQty;
					lblHPurchasePrice.Text = num.ToString(CommonSettings.strCurencyFormat);
					decimal num2 = row["Average"].ToString().ToDecimal() * productUnit.UnitQty;
					lblAPurchasePrice.Text = num2.ToString(CommonSettings.strCurencyFormat);
					decimal num3 = row["Last"].ToString().ToDecimal() * productUnit.UnitQty;
					lblLPurchasePrice.Text = num3.ToString(CommonSettings.strCurencyFormat);
				}
				if (productPriceCategory != null && !(productPriceCategory.PPrice == 0m))
				{
					lblPurchasePrice.Text = productPriceCategory.PPrice.ToString(CommonSettings.strCurencyFormat);
				}
				else
				{
					decimal num4 = productEntity.PurchasePrice * productUnit.UnitQty;
					lblPurchasePrice.Text = num4.ToString(CommonSettings.strCurencyFormat);
				}
				lblStock.Text = dStock.ToString(CommonSettings.strQuantityFormat) + " " + unitEntity.UnitName;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSStockView));
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.LabelPurchasePrice = new System.Windows.Forms.Label();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.LabelHPurchasePrice = new System.Windows.Forms.Label();
            this.lblHPurchasePrice = new System.Windows.Forms.Label();
            this.LabelAPurchasePrice = new System.Windows.Forms.Label();
            this.lblAPurchasePrice = new System.Windows.Forms.Label();
            this.LabelLPurchasePrice = new System.Windows.Forms.Label();
            this.lblLPurchasePrice = new System.Windows.Forms.Label();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblProductName
            // 
            resources.ApplyResources(this.lblProductName, "lblProductName");
            this.lblProductName.Name = "lblProductName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblStock
            // 
            resources.ApplyResources(this.lblStock, "lblStock");
            this.lblStock.Name = "lblStock";
            // 
            // LabelPurchasePrice
            // 
            resources.ApplyResources(this.LabelPurchasePrice, "LabelPurchasePrice");
            this.LabelPurchasePrice.Name = "LabelPurchasePrice";
            // 
            // lblPurchasePrice
            // 
            resources.ApplyResources(this.lblPurchasePrice, "lblPurchasePrice");
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            // 
            // LabelHPurchasePrice
            // 
            resources.ApplyResources(this.LabelHPurchasePrice, "LabelHPurchasePrice");
            this.LabelHPurchasePrice.Name = "LabelHPurchasePrice";
            // 
            // lblHPurchasePrice
            // 
            resources.ApplyResources(this.lblHPurchasePrice, "lblHPurchasePrice");
            this.lblHPurchasePrice.Name = "lblHPurchasePrice";
            // 
            // LabelAPurchasePrice
            // 
            resources.ApplyResources(this.LabelAPurchasePrice, "LabelAPurchasePrice");
            this.LabelAPurchasePrice.Name = "LabelAPurchasePrice";
            // 
            // lblAPurchasePrice
            // 
            resources.ApplyResources(this.lblAPurchasePrice, "lblAPurchasePrice");
            this.lblAPurchasePrice.Name = "lblAPurchasePrice";
            // 
            // LabelLPurchasePrice
            // 
            resources.ApplyResources(this.LabelLPurchasePrice, "LabelLPurchasePrice");
            this.LabelLPurchasePrice.Name = "LabelLPurchasePrice";
            // 
            // lblLPurchasePrice
            // 
            resources.ApplyResources(this.lblLPurchasePrice, "lblLPurchasePrice");
            this.lblLPurchasePrice.Name = "lblLPurchasePrice";
            // 
            // POSStockView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLPurchasePrice);
            this.Controls.Add(this.LabelLPurchasePrice);
            this.Controls.Add(this.lblAPurchasePrice);
            this.Controls.Add(this.LabelAPurchasePrice);
            this.Controls.Add(this.lblHPurchasePrice);
            this.Controls.Add(this.LabelHPurchasePrice);
            this.Controls.Add(this.lblPurchasePrice);
            this.Controls.Add(this.LabelPurchasePrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "POSStockView";
            this.Load += new System.EventHandler(this.POSStockView_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblProductName, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblStock, 0);
            this.Controls.SetChildIndex(this.LabelPurchasePrice, 0);
            this.Controls.SetChildIndex(this.lblPurchasePrice, 0);
            this.Controls.SetChildIndex(this.LabelHPurchasePrice, 0);
            this.Controls.SetChildIndex(this.lblHPurchasePrice, 0);
            this.Controls.SetChildIndex(this.LabelAPurchasePrice, 0);
            this.Controls.SetChildIndex(this.lblAPurchasePrice, 0);
            this.Controls.SetChildIndex(this.LabelLPurchasePrice, 0);
            this.Controls.SetChildIndex(this.lblLPurchasePrice, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
