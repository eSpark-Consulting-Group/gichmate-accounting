using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class POSLineEdit : BaseForm
	{
		private ProductEntity product;

		private ProductPriceCategory priceCategory;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private Button btnCancel;

		private Button btnOK;

		private Button btnUpdate;

		private DashboardPOSKeyboard nkPOS;

		private AshTBox txtunits;

		private Label label5;

		private AshTBox txtQty;

		private Label label2;

		private AshTBox txtitem;

		private Label label1;

		private AshTBox txtprice;

		private Label label3;

		private Label lblTitle;

		public CheckBox cbIncludeTax;

		public POSDetails pOSDetails { get; set; }

		public int PCategoryId { get; set; }

		public bool IsShowInclusive { get; set; } = true;


		public POSLineEdit()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void dashboardPOSKeyboard1_atStarClick(object source)
		{
			SendKeys.Send("{BACKSPACE}");
		}

		private void POSLineEdit_Load(object sender, EventArgs e)
		{
			foreach (Control control in nkPOS.Controls)
			{
				if (control is Button)
				{
					control.BackColor = Settings.Default.OtherB;
					control.ForeColor = Settings.Default.OtherF;
				}
			}
			cbIncludeTax.Visible = IsShowInclusive;
			nkPOS.StarButton.Text = "C";
			txtprice.Format = CommonSettings.strCurencyFormat;
			nkPOS.Font = new Font(nkPOS.StarButton.Font.FontFamily, 24f);
			if (pOSDetails != null)
			{
				txtitem.Text = pOSDetails.ProductName;
				txtunits.Text = pOSDetails.UnitName;
				txtQty.Value = pOSDetails.Qty;
				txtprice.Value = pOSDetails.Rate;
				product = db.Products(new
				{
					ProductId = pOSDetails.productId.ToInt32()
				}).FirstOrDefault();
				cbIncludeTax.Checked = product.isInclusive.toBool();
				base.ActiveControl = txtprice;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			pOSDetails.Rate = txtprice.Value;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (product.Barcode == pOSDetails.Barcode)
				{
					product.SellingPrice = txtprice.Value;
					product.isInclusive = cbIncludeTax.Checked;
					AshproCommon.UpdateToDatabaseObj(product, "Product", "ProductId", product.ProductId.ToInt32());
				}
				priceCategory = db.ProductPriceCategories(new { pOSDetails.Barcode, PCategoryId }).FirstOrDefault();
				if (priceCategory != null)
				{
					priceCategory.SPrice = txtprice.Value;
					AshproCommon.UpdateToDatabaseObj(priceCategory, "ProductPriceCategory", "ProPrceCatId", priceCategory.ProPrceCatId.toInt32());
				}
				ProductBarcodeEntity productBarcodeEntity = db.ProductBarcodes(new { pOSDetails.Barcode }).FirstOrDefault();
				if (productBarcodeEntity != null)
				{
					productBarcodeEntity.SellingPrice = txtprice.Value;
					AshproCommon.UpdateToDatabaseObj(productBarcodeEntity, "ProductBarcode", "Id", productBarcodeEntity.Id.toInt32());
				}
				btnOK.PerformClick();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtprice_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnOK.PerformClick();
			}
		}

		private void POSLineEdit_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.U && e.Control)
			{
				btnUpdate.PerformClick();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSLineEdit));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.nkPOS = new AshUsercontrol.DashboardPOSKeyboard();
            this.txtunits = new AshUsercontrol.AshTBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQty = new AshUsercontrol.AshTBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtitem = new AshUsercontrol.AshTBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIncludeTax = new System.Windows.Forms.CheckBox();
            this.txtprice = new AshUsercontrol.AshTBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // nkPOS
            // 
            resources.ApplyResources(this.nkPOS, "nkPOS");
            this.nkPOS.Name = "nkPOS";
            this.nkPOS.atStarClick += new AshUsercontrol.StarClickEventHandler(this.dashboardPOSKeyboard1_atStarClick);
            // 
            // txtunits
            // 
            resources.ApplyResources(this.txtunits, "txtunits");
            this.txtunits.Data = 0;
            this.txtunits.EnterColor = System.Drawing.Color.LightPink;
            this.txtunits.Format = null;
            this.txtunits.isAllowNegative = false;
            this.txtunits.isAllowSpecialChar = false;
            this.txtunits.isNumberOnly = false;
            this.txtunits.isNumeric = false;
            this.txtunits.LeaveColor = System.Drawing.Color.White;
            this.txtunits.Name = "txtunits";
            this.txtunits.ReadOnly = true;
            this.txtunits.TabStop = false;
            this.txtunits.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtQty
            // 
            resources.ApplyResources(this.txtQty, "txtQty");
            this.txtQty.Data = 0;
            this.txtQty.EnterColor = System.Drawing.Color.LightPink;
            this.txtQty.Format = null;
            this.txtQty.isAllowNegative = false;
            this.txtQty.isAllowSpecialChar = false;
            this.txtQty.isNumberOnly = false;
            this.txtQty.isNumeric = true;
            this.txtQty.LeaveColor = System.Drawing.Color.White;
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.TabStop = false;
            this.txtQty.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtitem
            // 
            resources.ApplyResources(this.txtitem, "txtitem");
            this.txtitem.Data = 0;
            this.txtitem.EnterColor = System.Drawing.Color.LightPink;
            this.txtitem.Format = null;
            this.txtitem.isAllowNegative = false;
            this.txtitem.isAllowSpecialChar = false;
            this.txtitem.isNumberOnly = false;
            this.txtitem.isNumeric = false;
            this.txtitem.LeaveColor = System.Drawing.Color.White;
            this.txtitem.Name = "txtitem";
            this.txtitem.ReadOnly = true;
            this.txtitem.TabStop = false;
            this.txtitem.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbIncludeTax
            // 
            resources.ApplyResources(this.cbIncludeTax, "cbIncludeTax");
            this.cbIncludeTax.Name = "cbIncludeTax";
            this.cbIncludeTax.UseVisualStyleBackColor = true;
            // 
            // txtprice
            // 
            resources.ApplyResources(this.txtprice, "txtprice");
            this.txtprice.Data = 0;
            this.txtprice.EnterColor = System.Drawing.Color.LightPink;
            this.txtprice.Format = null;
            this.txtprice.isAllowNegative = false;
            this.txtprice.isAllowSpecialChar = false;
            this.txtprice.isNumberOnly = false;
            this.txtprice.isNumeric = true;
            this.txtprice.LeaveColor = System.Drawing.Color.White;
            this.txtprice.Name = "txtprice";
            this.txtprice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtprice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtprice_KeyDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // POSLineEdit
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbIncludeTax);
            this.Controls.Add(this.txtprice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtunits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtitem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nkPOS);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnOK);
            this.Name = "POSLineEdit";
            this.Load += new System.EventHandler(this.POSLineEdit_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSLineEdit_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.nkPOS, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtitem, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtQty, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtunits, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtprice, 0);
            this.Controls.SetChildIndex(this.cbIncludeTax, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
