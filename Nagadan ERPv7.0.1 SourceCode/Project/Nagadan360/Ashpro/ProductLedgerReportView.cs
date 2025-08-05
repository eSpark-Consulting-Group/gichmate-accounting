using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class ProductLedgerReportView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string sTitle;

		private string Query;

		private string sBranchDetails = "";

		private IContainer components = null;

		private AshTextBoxNormal txtProCode;

		private Label label6;

		private Label label4;

		private Label label3;

		private AshComboBox cmbVchrType;

		private AshComboBox cmbProduct;

		private AshDateTimePicker dtmTo;

		private Label label2;

		private AshDateTimePicker dtmFrom;

		private Label label1;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string sFromDate { get; set; }

		public string sToDate { get; set; }

		public string sProduct { get; set; }

		public string sVoucherType { get; set; }

		public ProductLedgerReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			try
			{
				if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
				{
					sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
				}
				string[] headers = new string[12]
				{
					"SL", "Product Code", "Product Name", "Date", "Particulars", "Voucher Type", "Form", "Vchr No", "In Qty", "Out Qty",
					"Balance", "Unit"
				};
				Details = AniHelper.GetDataTable(headers);
				Gridview = AniHelper.GetGridviewDatatable();
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(50, "L");
				string text = "";
				if (cmbProduct.Text != "")
				{
					text = " and StockTranLog.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "'";
				}
				string text2 = "";
				if (cmbVchrType.Text != "")
				{
					text2 = " and StockTranLog.VchrType='" + cmbVchrType.Text + "' ";
				}
				string strID = "";
				if (txtProCode.Text != "")
				{
					AshproCommon.IdFindMethod("Product", "ProductId", "ProductCode", txtProCode.Text, out strID);
					text = " and StockTranLog.ProductId='" + strID + "'";
				}
				decimal num = default(decimal);
				int num2 = 0;
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				string strID2 = string.Empty;
				string strID3 = string.Empty;
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				num = OpeningStockMethod(strID);
				Query = "SELECT Product.ProductCode,Product.ProductName, Convert(nvarchar,StockTranLog.VchrDate,103) as VoucherDate ,StockTranLog.VchrNo,StockTranLog.VchrType,StockTranLog.VchrFormType,StockTranLog.AccountId,StockTranLog.InQuantity,StockTranLog.OutQuantity,StockTranLog.UnitId FROM StockTranLog join Product on  StockTranLog.ProductId=Product.ProductId where StockTranLog.VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0)) + "'" + text + text2 + sBranchDetails + " order by StockTranLog.VchrDate,StockTranLog.VchrNo";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num2++;
					empty = row[4].ToString();
					empty2 = row["InQuantity"].ToString();
					if (empty2 == "0.00")
					{
						empty2 = empty2.Replace("0.00", "");
					}
					empty3 = row["OutQuantity"].ToString();
					if (empty3 == "0.00")
					{
						empty3 = empty3.Replace("0.00", "");
					}
					empty = empty.Replace("Voucher", "");
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row["AccountId"].ToString(), out strID2);
					strID2 = strID2 ?? cmbProduct.Text;
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row["UnitId"].ToString(), out strID3);
					num3 = empty2.ToDecimal();
					num4 = empty3.ToDecimal();
					num = num + num3 - num4;
					Details.Rows.Add(num2, row[0], row[1], row[2], strID2, empty, row[5], row[3], empty2, empty3, num, strID3);
				}
				Details.Rows.Add("", "", "", "", "", "", "", "", "", "Clo Stock", num, "");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private decimal OpeningStockMethod(string strProductId)
		{
			decimal? num = AshproCommon.ValueFindMethod("Select sum(InQuantity) as Qty From StockTranLog where ProductId = '" + strProductId + "' " + sBranchDetails + " and VchrDate < '" + AniHelper.GetDate(dtmFrom.Value) + "'");
			decimal? num2 = AshproCommon.ValueFindMethod("SELECT SUM(InQuantity-OutQuantity) as Qty FROM StockTranLog where ProductId = '" + strProductId + "' " + sBranchDetails + " and VchrDate < '" + AniHelper.GetDate(dtmFrom.Value) + "'");
			decimal? num3 = AshproCommon.ValueFindMethod("Select sum(OutQuantity) as Qty From StockTranLog where ProductId = '" + strProductId + "' " + sBranchDetails + " and VchrDate < '" + AniHelper.GetDate(dtmFrom.Value) + "'");
			Details.Rows.Add(Details.Rows.Count + 1, "", "", dtmFrom.Value.ToString("dd/MM/yyyy"), "Opening Stock", "", "", "", num, num3, num2, "");
			return num2.ToDecimal();
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(220, 220);
				LineData.Rows.Add(304, 304);
				LineData.Rows.Add(419, 419);
				LineData.Rows.Add(479, 479);
				LineData.Rows.Add(559, 559);
				LineData.Rows.Add(639, 639);
				LineData.Rows.Add(719, 719);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Product Name", 9);
				Header.Rows.Add("Date", 225);
				Header.Rows.Add("Vchr Type", 305);
				Header.Rows.Add("Vchr No", 419);
				Header.Rows.Add("In Qty", 480);
				Header.Rows.Add("Out Qty", 560);
				Header.Rows.Add("Balance", 640);
				Header.Rows.Add("Unit", 720);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(2, 8, true, true, "", 235);
				data.Rows.Add(3, 225, false, false, "", 0);
				data.Rows.Add(5, 305, true, false, "", 0);
				data.Rows.Add(7, 420, true, false, "", 0);
				data.Rows.Add(8, 558, true, false, "Right", 0);
				data.Rows.Add(9, 638, true, false, "Right", 0);
				data.Rows.Add(10, 719, true, false, "Right", 0);
				data.Rows.Add(11, 723, false, false, "", 0);
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
			LineDataPDF.Rows.Add(315, 50, 315, 800);
			LineDataPDF.Rows.Add(230, 50, 230, 800);
			LineDataPDF.Rows.Add(160, 50, 160, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Product Name", 9);
			HeaderPDF.Rows.Add("Date", 163);
			HeaderPDF.Rows.Add("Vchr Type", 231);
			HeaderPDF.Rows.Add("Vchr No", 316);
			HeaderPDF.Rows.Add("In Qty", 361);
			HeaderPDF.Rows.Add("Out Qty", 426);
			HeaderPDF.Rows.Add("Balance", 491);
			HeaderPDF.Rows.Add("Unit", 556);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(2, 10, true, false, "");
			dataPDF.Rows.Add(3, 163, true, false, "", 0);
			dataPDF.Rows.Add(5, 231, true, false, "", 0);
			dataPDF.Rows.Add(7, 316, true, false, "", 0);
			dataPDF.Rows.Add(8, 424, true, false, "Right", 0);
			dataPDF.Rows.Add(9, 489, true, false, "Right", 0);
			dataPDF.Rows.Add(10, 554, true, false, "Right", 0);
			dataPDF.Rows.Add(11, 556, false, false, "", 0);
		}

		private void ProductLedgerReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillFormType(cmbVchrType);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			if (sProduct != string.Empty && sVoucherType != string.Empty)
			{
				dtmFrom.Text = sFromDate;
				dtmTo.Text = sToDate;
				cmbProduct.SelectedText = sProduct;
				cmbVchrType.SelectedText = sVoucherType;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtProCode.Text = string.Empty;
			dtmFrom.Value = DateTime.Now.Date;
			dtmTo.Value = DateTime.Now.Date;
			cmbProduct.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = "Product Ledger Report From " + AniHelper.GetDate(dtmFrom.Value) + " To " + dtmTo.Text;
			ReportGenerateMethod();
			PrintMethod();
			MailMethod();
			ReportView reportView = new ReportView();
			reportView = new ReportView();
			reportView.MdiParent = base.MdiParent;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Title = sTitle;
			reportView.data = data;
			reportView.dataPDF = dataPDF;
			reportView.LineData = LineData;
			reportView.LineDataPDF = LineDataPDF;
			reportView.Header = Header;
			reportView.HeaderPDF = HeaderPDF;
			reportView.Show();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductLedgerReportView));
            this.txtProCode = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbVchrType = new AshUsercontrol.AshComboBox();
            this.cmbProduct = new AshUsercontrol.AshComboBox();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // txtProCode
            // 
            resources.ApplyResources(this.txtProCode, "txtProCode");
            this.txtProCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProCode.Data = 0;
            this.txtProCode.EnterColor = System.Drawing.Color.LightPink;
            this.txtProCode.Format = null;
            this.txtProCode.isAllowNegative = false;
            this.txtProCode.isAllowSpecialChar = false;
            this.txtProCode.isNumberOnly = false;
            this.txtProCode.isNumeric = false;
            this.txtProCode.LeaveColor = System.Drawing.Color.White;
            this.txtProCode.Name = "txtProCode";
            this.txtProCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbVchrType
            // 
            resources.ApplyResources(this.cmbVchrType, "cmbVchrType");
            this.cmbVchrType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbVchrType.FormattingEnabled = true;
            this.cmbVchrType.Items.AddRange(new object[] {
            resources.GetString("cmbVchrType.Items"),
            resources.GetString("cmbVchrType.Items1"),
            resources.GetString("cmbVchrType.Items2"),
            resources.GetString("cmbVchrType.Items3"),
            resources.GetString("cmbVchrType.Items4")});
            this.cmbVchrType.LeaveColor = System.Drawing.Color.White;
            this.cmbVchrType.Name = "cmbVchrType";
            // 
            // cmbProduct
            // 
            resources.ApplyResources(this.cmbProduct, "cmbProduct");
            this.cmbProduct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.LeaveColor = System.Drawing.Color.White;
            this.cmbProduct.Name = "cmbProduct";
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
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
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // ProductLedgerReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbVchrType);
            this.Controls.Add(this.cmbProduct);
            this.Name = "ProductLedgerReportView";
            this.Load += new System.EventHandler(this.ProductLedgerReportView_Load);
            this.Controls.SetChildIndex(this.cmbProduct, 0);
            this.Controls.SetChildIndex(this.cmbVchrType, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtProCode, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
