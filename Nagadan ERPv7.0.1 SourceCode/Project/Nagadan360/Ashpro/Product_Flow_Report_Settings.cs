using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class Product_Flow_Report_Settings : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label label3;

		private AshDateTimePicker dtmTo;

		private Label label5;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private Button btnShow;

		private Button btnClear;

		private ToolTip toolTip1;

		private Button button4;

		private Label lblHead;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Panel pnlBottom;

		private Panel pnlMain;

		private AshTextBoxNormal txtProduct;

		private AshTextBoxNormal txtBarcode;

		private Label label1;

		public Product_Flow_Report_Settings()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ClearMethood()
		{
			txtProduct.Text = string.Empty;
			txtBarcode.Text = string.Empty;
			dtmFrom.Value = DateTime.Now.Date;
			dtmTo.Value = DateTime.Now.Date;
		}

		private void ReportGenerateMethod()
		{
			string barcode = null;
			int? productId = null;
			string[] headers = new string[15]
			{
				"Barcode", "Product Name", "Group", "Brand", "Opp Stock", "Purchase", "Purchase Return", "Sales", "Sales Return", "Free In",
				"Free Out", "Stock", "Unit", "Rate", "Stock Value"
			};
			Details = AniHelper.GetDataTable(headers);
			int? branchId = null;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				branchId = cmbBranch.SelectedValue.ToInt32();
			}
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(150, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			if (txtProduct.Text != "")
			{
				ProductEntity productEntity = db.Products(new
				{
					ProductName = txtProduct.Text
				}).FirstOrDefault();
				if (productEntity != null)
				{
					productId = productEntity.ProductId;
				}
			}
			if (txtBarcode.Text != "")
			{
				barcode = txtBarcode.Text;
			}
			try
			{
				string date = AniHelper.GetDate(dtmFrom.Value.Date);
				string date2 = AniHelper.GetDate(dtmTo.Value);
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spProductFlowReport", new
				{
					FromDate = date,
					ToDate = date2,
					Barcode = barcode,
					ProductId = productId,
					BranchId = branchId
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(239, 239);
				LineData.Rows.Add(319, 319);
				LineData.Rows.Add(399, 399);
				LineData.Rows.Add(479, 479);
				LineData.Rows.Add(559, 559);
				LineData.Rows.Add(639, 639);
				LineData.Rows.Add(719, 719);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Product Name", 9);
				Header.Rows.Add("Opp Stock", 240);
				Header.Rows.Add("Purchase", 320);
				Header.Rows.Add("Pur Rtn", 400);
				Header.Rows.Add("Sales", 480);
				Header.Rows.Add("Sales Rtn", 560);
				Header.Rows.Add("Clo Stock", 640);
				Header.Rows.Add("Unit", 720);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(1, 8, true, true, "", 230);
				data.Rows.Add(4, 240, true, false, "");
				data.Rows.Add(5, 320, true, false, "");
				data.Rows.Add(6, 400, true, false, "");
				data.Rows.Add(7, 480, true, false, "");
				data.Rows.Add(8, 560, true, false, "");
				data.Rows.Add(9, 640, true, false, "");
				data.Rows.Add(10, 720, false, false, "");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			LineDataPDF = AniHelper.GetLineDatatablePDF();
			LineDataPDF.Rows.Add(8, 50, 585, 50);
			LineDataPDF.Rows.Add(8, 50, 8, 800);
			LineDataPDF.Rows.Add(8, 800, 585, 800);
			LineDataPDF.Rows.Add(585, 50, 585, 800);
			LineDataPDF.Rows.Add(555, 50, 555, 800);
			LineDataPDF.Rows.Add(490, 50, 490, 800);
			LineDataPDF.Rows.Add(425, 50, 425, 800);
			LineDataPDF.Rows.Add(360, 50, 360, 800);
			LineDataPDF.Rows.Add(295, 50, 295, 800);
			LineDataPDF.Rows.Add(230, 50, 230, 800);
			LineDataPDF.Rows.Add(165, 50, 165, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Product Name", 9);
			HeaderPDF.Rows.Add("Opp Stock", 168);
			HeaderPDF.Rows.Add("Purchase", 231);
			HeaderPDF.Rows.Add("Pur Rtn", 296);
			HeaderPDF.Rows.Add("Sales", 361);
			HeaderPDF.Rows.Add("Sales Rtn", 426);
			HeaderPDF.Rows.Add("Clo Stock", 491);
			HeaderPDF.Rows.Add("Unit", 556);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(1, 10, true, true, "");
			dataPDF.Rows.Add(4, 166, true, false, "");
			dataPDF.Rows.Add(5, 231, true, false, "");
			dataPDF.Rows.Add(6, 296, true, false, "");
			dataPDF.Rows.Add(7, 361, true, false, "");
			dataPDF.Rows.Add(8, 426, true, false, "");
			dataPDF.Rows.Add(9, 491, true, false, "");
			dataPDF.Rows.Add(10, 556, false, false, "");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethood();
		}

		private async void Product_Flow_Report_Settings_Load(object sender, EventArgs e)
		{
			await AniHelper.AutoCompleteMethod(txtProduct);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			dtmFrom.Value = AniHelper.GetFYearFromDate();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			ReportGenerateMethod();
			PrintMethod();
			MailMethod();
			ReportView reportView = new ReportView();
			reportView = new ReportView();
			reportView.MdiParent = base.MdiParent;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Title = "Product Flow Report From " + dtmFrom.Text + " To " + dtmTo.Text;
			reportView.data = data;
			reportView.dataPDF = dataPDF;
			reportView.LineData = LineData;
			reportView.LineDataPDF = LineDataPDF;
			reportView.Header = Header;
			reportView.HeaderPDF = HeaderPDF;
			reportView.Show();
		}

		private void Product_Flow_Report_Settings_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnShow_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				ClearMethood();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else
			{
				e.SuppressKeyPress = false;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product_Flow_Report_Settings));
            this.label3 = new System.Windows.Forms.Label();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.txtProduct = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            this.toolTip1.SetToolTip(this.dtmTo, resources.GetString("dtmTo.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.toolTip1.SetToolTip(this.dtmFrom, resources.GetString("dtmFrom.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.toolTip1.SetToolTip(this.btnShow, resources.GetString("btnShow.ToolTip"));
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.toolTip1.SetToolTip(this.button4, resources.GetString("button4.ToolTip"));
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip1.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Items.AddRange(new object[] {
            resources.GetString("cmbBranch.Items"),
            resources.GetString("cmbBranch.Items1"),
            resources.GetString("cmbBranch.Items2")});
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.toolTip1.SetToolTip(this.cmbBranch, resources.GetString("cmbBranch.ToolTip"));
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            this.toolTip1.SetToolTip(this.lblBranch, resources.GetString("lblBranch.ToolTip"));
            // 
            // pnlBottom
            // 
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Controls.Add(this.btnShow);
            this.pnlBottom.Name = "pnlBottom";
            this.toolTip1.SetToolTip(this.pnlBottom, resources.GetString("pnlBottom.ToolTip"));
            // 
            // pnlMain
            // 
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Controls.Add(this.txtBarcode);
            this.pnlMain.Controls.Add(this.txtProduct);
            this.pnlMain.Controls.Add(this.lblBranch);
            this.pnlMain.Controls.Add(this.cmbBranch);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.dtmFrom);
            this.pnlMain.Controls.Add(this.dtmTo);
            this.pnlMain.Name = "pnlMain";
            this.toolTip1.SetToolTip(this.pnlMain, resources.GetString("pnlMain.ToolTip"));
            // 
            // txtBarcode
            // 
            resources.ApplyResources(this.txtBarcode, "txtBarcode");
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcode.Data = 0;
            this.txtBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtBarcode.Format = null;
            this.txtBarcode.isAllowNegative = false;
            this.txtBarcode.isAllowSpecialChar = false;
            this.txtBarcode.isNumberOnly = false;
            this.txtBarcode.isNumeric = false;
            this.txtBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtBarcode.Name = "txtBarcode";
            this.toolTip1.SetToolTip(this.txtBarcode, resources.GetString("txtBarcode.ToolTip"));
            this.txtBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtProduct
            // 
            resources.ApplyResources(this.txtProduct, "txtProduct");
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProduct.Data = 0;
            this.txtProduct.EnterColor = System.Drawing.Color.LightPink;
            this.txtProduct.Format = null;
            this.txtProduct.isAllowNegative = false;
            this.txtProduct.isAllowSpecialChar = false;
            this.txtProduct.isNumberOnly = false;
            this.txtProduct.isNumeric = false;
            this.txtProduct.LeaveColor = System.Drawing.Color.White;
            this.txtProduct.Name = "txtProduct";
            this.toolTip1.SetToolTip(this.txtProduct, resources.GetString("txtProduct.ToolTip"));
            this.txtProduct.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // Product_Flow_Report_Settings
            // 
            this.AcceptButton = this.btnShow;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.MaximizeBox = false;
            this.Name = "Product_Flow_Report_Settings";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.Product_Flow_Report_Settings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Product_Flow_Report_Settings_KeyDown);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.pnlBottom, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.pnlBottom.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
