using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;


namespace Ashpro
{
	public class ProductDetails : BaseForm
	{
		private string strParticulars;

		private DataGridViewRow row;

		private ProductLedgerReportView proLedgerRpt;

		private IContainer components = null;

		private Label label1;

		private Label lblProduct;

		private AshDateTimePicker dtmTo;

		private Label label5;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private Panel panel1;

		private DataGridView dgDetails;

		private Button button4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Label lblHead;

		public string ValueFromDate { get; set; }

		public string ValueToDate { get; set; }

		public string ValueProduct { get; set; }

		public string ValueOppening { get; set; }

		public string ValuePurchase { get; set; }

		public string ValuePurReturn { get; set; }

		public string ValueSales { get; set; }

		public string ValueSalReturn { get; set; }

		public string ValueClosing { get; set; }

		public ProductDetails()
		{

			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			panel1.BackColor = Settings.Default.BackColor;
		}

		private void ProductDetails_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Normal;
			if (ValueProduct != "")
			{
				lblProduct.Text = ValueProduct;
				dtmFrom.Text = ValueFromDate;
				dtmTo.Text = ValueToDate;
				dgDetails.Rows.Add(1, "Opening Stock", ValueOppening);
				dgDetails.Rows.Add(2, "Product Purchase", ValuePurchase);
				dgDetails.Rows.Add(3, "Product Purchase Return", ValuePurReturn);
				dgDetails.Rows.Add(4, "Product Sales", ValueSales);
				dgDetails.Rows.Add(5, "Product Sales Return", ValueSalReturn);
				dgDetails.Rows.Add(6, "Closing Stock", ValueClosing);
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			row = dgDetails.Rows[e.RowIndex];
			strParticulars = row.Cells[1].Value.ToString();
			if (proLedgerRpt != null)
			{
				return;
			}
			proLedgerRpt = new ProductLedgerReportView();
			proLedgerRpt.sFromDate = dtmFrom.Text;
			proLedgerRpt.sToDate = dtmTo.Text;
			proLedgerRpt.sProduct = ValueProduct;
			if (strParticulars == "Product Purchase")
			{
				proLedgerRpt.sVoucherType = "Purchase Voucher";
			}
			else if (strParticulars == "Product Purchase Return")
			{
				proLedgerRpt.sVoucherType = "";
			}
			else if (strParticulars == "Product Sales")
			{
				proLedgerRpt.sVoucherType = "";
			}
			else if (strParticulars == "Product Sales Return")
			{
				proLedgerRpt.sVoucherType = "";
			}
			else if (strParticulars == "Closing Stock")
			{
				proLedgerRpt.sVoucherType = "";
			}
			else
			{
				if (!(strParticulars == "Opening Stock"))
				{
					return;
				}
				proLedgerRpt.sVoucherType = "Opening Entry";
			}
			proLedgerRpt.MdiParent = base.MdiParent;
			proLedgerRpt.Show();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblProduct
            // 
            resources.ApplyResources(this.lblProduct, "lblProduct");
            this.lblProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblProduct.Name = "lblProduct";
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.dgDetails);
            this.panel1.Name = "panel1";
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // ProductDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "ProductDetails";
            this.Load += new System.EventHandler(this.ProductDetails_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblProduct, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
