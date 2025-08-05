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
	public class ProductSubView : BaseFormTwo
	{
		private AshButton btnProduct;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private FlowLayoutPanel flpProducts;

		private Button btnCancel;

		public int Id { get; set; }

		public ProductSubView(List<ProductBarcodeEntity> productBarcodes)
		{
			InitializeComponent();
			CurrencyInfo currencyInfo = db.CurrencyInfos(new
			{
				CurrencyID = CommonSettings.strDefaultCurrency
			}).FirstOrDefault();
			string text = " ";
			if (currencyInfo != null)
			{
				text = currencyInfo.CurrencyCode + ". ";
			}
			foreach (ProductBarcodeEntity productBarcode in productBarcodes)
			{
				btnProduct = new AshButton();
				btnProduct.Name = productBarcode.Id.ToString();
				ProductPriceCategory productPriceCategory = db.ProductPriceCategories(new { productBarcode.Barcode }).FirstOrDefault();
				if (productPriceCategory != null)
				{
					btnProduct.Text = productBarcode.ProductBarcodeName + " \n" + text + productPriceCategory.SPrice.ToString(CommonSettings.strCurencyFormat);
				}
				else
				{
					btnProduct.Text = productBarcode.ProductBarcodeName;
				}
				btnProduct.BackColor = Settings.Default.ProductBB;
				btnProduct.ForeColor = Settings.Default.ProductBF;
				btnProduct.Size = new Size(Utility.pbValues.Name.ToInt32(), Utility.pbValues.Description.ToInt32());
				if (productBarcode.image != null)
				{
					btnProduct.BackgroundImage = AniHelper.byteArrayToImage(productBarcode.image);
					btnProduct.BackgroundImageLayout = ImageLayout.Zoom;
					btnProduct.TextAlign = ContentAlignment.BottomCenter;
				}
				btnProduct.Click += delegate(object s, EventArgs e)
				{
					btnProduct = (AshButton)s;
					Id = btnProduct.Name.ToInt32();
					base.DialogResult = DialogResult.OK;
					Close();
				};
				flpProducts.Controls.Add(btnProduct);
			}
			int num = Utility.pbValues.Name.ToInt32() * 3 + 70;
			int num2 = Utility.pbValues.Description.ToInt32() * 2 + 20;
			base.Size = new Size(num, num2);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
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
			flpProducts = new System.Windows.Forms.FlowLayoutPanel();
			btnCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			flpProducts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			flpProducts.AutoScroll = true;
			flpProducts.Location = new System.Drawing.Point(0, 0);
			flpProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			flpProducts.Name = "flpProducts";
			flpProducts.Size = new System.Drawing.Size(657, 286);
			flpProducts.TabIndex = 0;
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnCancel.Location = new System.Drawing.Point(545, 297);
			btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(93, 43);
			btnCancel.TabIndex = 5;
			btnCancel.TabStop = false;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(657, 352);
			base.Controls.Add(btnCancel);
			base.Controls.Add(flpProducts);
			base.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			base.Name = "ProductSubView";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "ProductSubView";
			ResumeLayout(false);
		}
	}
}
