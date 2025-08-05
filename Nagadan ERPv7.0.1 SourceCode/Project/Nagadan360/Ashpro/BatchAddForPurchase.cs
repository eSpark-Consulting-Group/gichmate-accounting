using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class BatchAddForPurchase : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private AshDateTimePicker dtmExpDate;

		private AshDateTimePicker dtmManufDate;

		private AshTextBoxNormal txtMRP;

		private AshTextBoxNormal txtSalesPrice;

		private AshTextBoxNormal txtProductName;

		private AshTextBoxNormal txtPurPrice;

		private AshTextBoxNormal txtBatchName;

		private Label label5;

		private Label label2;

		private Label label7;

		private Label label8;

		private Label label3;

		private Label label6;

		private Label label4;

		private Label label1;

		private Label label9;

		private Button btnSave;

		private Button btnCancel;

		private AshTextBoxNormal txtUnitName;

		private Label label10;

		private AshTextBoxNormal txtBarecode;

		public ExProductBarcodeEntity barcodeEntity { get; set; }

		public BatchAddForPurchase()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
        }

		private void EntityLoadMethod()
		{
			barcodeEntity.BatchName = txtBatchName.Text;
			barcodeEntity.FK_ProductId = db.Products().FirstOrDefault((ProductEntity x) => x.ProductName == txtProductName.Text).ProductId.ToInt32();
			barcodeEntity.FK_UnitId = db.Units().FirstOrDefault((UnitEntity x) => x.UnitName == txtUnitName.Text).UnitId;
			barcodeEntity.BarcodeType = "Batch Barcode";
			barcodeEntity.FK_BranchId = Settings.Default.iBranchId;
			barcodeEntity.ExpairyDate = dtmExpDate.Value;
			barcodeEntity.ManufDate = dtmManufDate.Value;
			barcodeEntity.PurchasePrice = txtPurPrice.Value;
			barcodeEntity.SellingPrice = txtSalesPrice.Value;
			barcodeEntity.MRP = txtMRP.Value;
		}

		private void BatchAddForPurchase_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
			if (barcodeEntity != null)
			{
				txtBatchName.Text = barcodeEntity.BatchName;
				txtProductName.Text = barcodeEntity.ProductName;
				txtUnitName.Text = barcodeEntity.UnitName;
				txtPurPrice.Value = barcodeEntity.PurchasePrice;
			}
			try
			{
				AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
				foreach (Batch item in db.Batches())
				{
					autoCompleteStringCollection.Add(item.Name);
				}
				txtBatchName.AutoCompleteMode = AutoCompleteMode.Suggest;
				txtBatchName.AutoCompleteSource = AutoCompleteSource.CustomSource;
				txtBatchName.AutoCompleteCustomSource = autoCompleteStringCollection;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (!(txtBatchName.Text == string.Empty) && !(txtBarecode.Text == string.Empty))
			{
				EntityLoadMethod();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				Messages.WarningMessage();
			}
		}

		private void txtMRP_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchAddForPurchase));
            this.dtmExpDate = new AshUsercontrol.AshDateTimePicker();
            this.dtmManufDate = new AshUsercontrol.AshDateTimePicker();
            this.txtMRP = new AshUsercontrol.AshTextBoxNormal();
            this.txtSalesPrice = new AshUsercontrol.AshTextBoxNormal();
            this.txtProductName = new AshUsercontrol.AshTextBoxNormal();
            this.txtPurPrice = new AshUsercontrol.AshTextBoxNormal();
            this.txtBatchName = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtUnitName = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBarecode = new AshUsercontrol.AshTextBoxNormal();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // dtmExpDate
            // 
            resources.ApplyResources(this.dtmExpDate, "dtmExpDate");
            this.dtmExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmExpDate.Name = "dtmExpDate";
            // 
            // dtmManufDate
            // 
            resources.ApplyResources(this.dtmManufDate, "dtmManufDate");
            this.dtmManufDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmManufDate.Name = "dtmManufDate";
            // 
            // txtMRP
            // 
            resources.ApplyResources(this.txtMRP, "txtMRP");
            this.txtMRP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMRP.Data = 0;
            this.txtMRP.EnterColor = System.Drawing.Color.LightPink;
            this.txtMRP.Format = "N2";
            this.txtMRP.isAllowNegative = false;
            this.txtMRP.isAllowSpecialChar = false;
            this.txtMRP.isNumberOnly = false;
            this.txtMRP.isNumeric = true;
            this.txtMRP.LeaveColor = System.Drawing.Color.White;
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtMRP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMRP_KeyDown);
            // 
            // txtSalesPrice
            // 
            resources.ApplyResources(this.txtSalesPrice, "txtSalesPrice");
            this.txtSalesPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSalesPrice.Data = 0;
            this.txtSalesPrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtSalesPrice.Format = "N2";
            this.txtSalesPrice.isAllowNegative = false;
            this.txtSalesPrice.isAllowSpecialChar = false;
            this.txtSalesPrice.isNumberOnly = false;
            this.txtSalesPrice.isNumeric = true;
            this.txtSalesPrice.LeaveColor = System.Drawing.Color.White;
            this.txtSalesPrice.Name = "txtSalesPrice";
            this.txtSalesPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtProductName
            // 
            resources.ApplyResources(this.txtProductName, "txtProductName");
            this.txtProductName.BackColor = System.Drawing.Color.White;
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductName.Data = 0;
            this.txtProductName.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductName.Format = null;
            this.txtProductName.isAllowNegative = false;
            this.txtProductName.isAllowSpecialChar = false;
            this.txtProductName.isNumberOnly = false;
            this.txtProductName.isNumeric = false;
            this.txtProductName.LeaveColor = System.Drawing.Color.White;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.TabStop = false;
            this.txtProductName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPurPrice
            // 
            resources.ApplyResources(this.txtPurPrice, "txtPurPrice");
            this.txtPurPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurPrice.Data = 0;
            this.txtPurPrice.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurPrice.Format = "N2";
            this.txtPurPrice.isAllowNegative = false;
            this.txtPurPrice.isAllowSpecialChar = false;
            this.txtPurPrice.isNumberOnly = false;
            this.txtPurPrice.isNumeric = true;
            this.txtPurPrice.LeaveColor = System.Drawing.Color.White;
            this.txtPurPrice.Name = "txtPurPrice";
            this.txtPurPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtBatchName
            // 
            resources.ApplyResources(this.txtBatchName, "txtBatchName");
            this.txtBatchName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBatchName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBatchName.Data = 0;
            this.txtBatchName.EnterColor = System.Drawing.Color.LightPink;
            this.txtBatchName.Format = null;
            this.txtBatchName.isAllowNegative = false;
            this.txtBatchName.isAllowSpecialChar = false;
            this.txtBatchName.isNumberOnly = false;
            this.txtBatchName.isNumeric = false;
            this.txtBatchName.LeaveColor = System.Drawing.Color.White;
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Value = new decimal(new int[] {
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtUnitName
            // 
            resources.ApplyResources(this.txtUnitName, "txtUnitName");
            this.txtUnitName.BackColor = System.Drawing.Color.White;
            this.txtUnitName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnitName.Data = 0;
            this.txtUnitName.EnterColor = System.Drawing.Color.LightPink;
            this.txtUnitName.Format = null;
            this.txtUnitName.isAllowNegative = false;
            this.txtUnitName.isAllowSpecialChar = false;
            this.txtUnitName.isNumberOnly = false;
            this.txtUnitName.isNumeric = false;
            this.txtUnitName.LeaveColor = System.Drawing.Color.White;
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.ReadOnly = true;
            this.txtUnitName.TabStop = false;
            this.txtUnitName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtBarecode
            // 
            resources.ApplyResources(this.txtBarecode, "txtBarecode");
            this.txtBarecode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarecode.Data = 0;
            this.txtBarecode.EnterColor = System.Drawing.Color.LightPink;
            this.txtBarecode.Format = null;
            this.txtBarecode.isAllowNegative = false;
            this.txtBarecode.isAllowSpecialChar = false;
            this.txtBarecode.isNumberOnly = false;
            this.txtBarecode.isNumeric = false;
            this.txtBarecode.LeaveColor = System.Drawing.Color.White;
            this.txtBarecode.Name = "txtBarecode";
            this.txtBarecode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // BatchAddForPurchase
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtmExpDate);
            this.Controls.Add(this.dtmManufDate);
            this.Controls.Add(this.txtMRP);
            this.Controls.Add(this.txtSalesPrice);
            this.Controls.Add(this.txtBarecode);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.txtPurPrice);
            this.Controls.Add(this.txtBatchName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "BatchAddForPurchase";
            this.Load += new System.EventHandler(this.BatchAddForPurchase_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtBatchName, 0);
            this.Controls.SetChildIndex(this.txtPurPrice, 0);
            this.Controls.SetChildIndex(this.txtProductName, 0);
            this.Controls.SetChildIndex(this.txtUnitName, 0);
            this.Controls.SetChildIndex(this.txtBarecode, 0);
            this.Controls.SetChildIndex(this.txtSalesPrice, 0);
            this.Controls.SetChildIndex(this.txtMRP, 0);
            this.Controls.SetChildIndex(this.dtmManufDate, 0);
            this.Controls.SetChildIndex(this.dtmExpDate, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
