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
	public class AddOns : BaseForm
	{
		private AshButton btnProduct;

		private List<ProductEntity> _products;

		private IContainer components = null;

		private FlowLayoutPanel flpProducts;

		private Label lblTitle;

		public int Id { get; set; }

		public RestaurantPOSView restaurantPOS { get; set; }

		public AddOns(List<ProductEntity> products)
		{
			InitializeComponent();
			_products = products;
			foreach (ProductEntity product in products)
			{
				btnProduct = new AshButton();
				btnProduct.Name = product.ProductId.ToString();
				btnProduct.Text = product.ProductName;
				btnProduct.BackColor = Settings.Default.ProductBB;
				btnProduct.ForeColor = Settings.Default.ProductBF;
				btnProduct.Size = new Size(Utility.pbValues.Name.ToInt32(), Utility.pbValues.Description.ToInt32());
				if (product.image != null)
				{
					btnProduct.BackgroundImage = AniHelper.byteArrayToImage(product.image);
					btnProduct.BackgroundImageLayout = ImageLayout.Zoom;
					btnProduct.TextAlign = ContentAlignment.BottomCenter;
				}
				btnProduct.Click += BtnProduct_Click;
				flpProducts.Controls.Add(btnProduct);
			}
		}

		private void BtnProduct_Click(object sender, EventArgs e)
		{
			try
			{
				btnProduct = (AshButton)sender;
				ProductEntity product = _products.FirstOrDefault((ProductEntity x) => x.ProductId == btnProduct.Name.ToInt32());
				restaurantPOS.DGVLoadFromProClick(product);
				Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.AddOns));
			flpProducts = new System.Windows.Forms.FlowLayoutPanel();
			lblTitle = new System.Windows.Forms.Label();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(flpProducts, "flpProducts");
			flpProducts.Name = "flpProducts";
			resources.ApplyResources(lblTitle, "lblTitle");
			lblTitle.Name = "lblTitle";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(lblTitle);
			base.Controls.Add(flpProducts);
			base.Name = "AddOns";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(flpProducts, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
