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
	public class GodownStockReportView : BaseForm
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

		private string strQuery;

		private string strValidation;

		private decimal dmlTotalQty;

		private string strGroupName;

		private string strBrandName;

		private string strUnitName;

		private decimal dmlRate;

		private decimal dmlStockValue;

		private decimal dmlTotalValue;

		private decimal dmlBalanceQty;

		private int intRowCount;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private AshComboBox cmbValidation;

		private AshComboBox cmbGdwn;

		private Label label6;

		private Label label2;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public GodownStockReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ProDetailsFindMethod(string strProductId)
		{
			try
			{
				strQuery = "Select Product.ProductId,Product.ProductName,Godown.GodownName,Product.PGroupId,Product.BrandId,Product.UnitId,Product." + strValidation + " From Product Join Godown on Product.GodwnId=Godown.GodwnId where Product.ProductId='" + strProductId + "' ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					strProductId = row[0].ToString();
					intRowCount = Details.Rows.Count + 1;
					AshproCommon.IdFindMethod("ProductGroup", "PGroupName", "PGroupId", row[3].ToString(), out strGroupName);
					AshproCommon.IdFindMethod("Brand", "BrandName", "BrandId", row[4].ToString(), out strBrandName);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[5].ToString(), out strUnitName);
					if (row[6] != DBNull.Value)
					{
						dmlRate = Convert.ToDecimal(row[6].ToString());
					}
					else
					{
						dmlRate = default(decimal);
					}
					dmlStockValue = dmlRate * dmlBalanceQty;
					dmlTotalQty += dmlBalanceQty;
					dmlTotalValue += dmlStockValue;
					Details.Rows.Add(intRowCount, row[1], row[2], strGroupName, strBrandName, dmlBalanceQty, strUnitName, row[6], dmlStockValue.ToString("F"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private decimal ProStockAddMethod(string sdtrPId)
		{
			strQuery = "SELECT SUM(InQuantity-OutQuantity) as outQty FROM GdwnStckTranLog where ProductId = '" + sdtrPId + "' " + sBranchDetails + " and VchrDate <= '" + AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
			return AshproCommon.ValueFindMethod(strQuery);
		}

		private void ReportGenerateMethod()
		{
			try
			{
				string text = string.Empty;
				if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
				{
					sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
					text = " and StockTransferMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
				}
				string[] headers = new string[9] { "SL", "Product Name", "Godown", "Group Name", "Brand", "Stock", "Unit", "Rate", "Stock Value" };
				Details = AniHelper.GetDataTable(headers);
				Gridview = AniHelper.GetGridviewDatatable();
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(60, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				dmlTotalQty = default(decimal);
				dmlTotalValue = default(decimal);
				if (cmbValidation.SelectedIndex == 0)
				{
					strValidation = "SellingPrice";
				}
				else if (cmbValidation.SelectedIndex == 1)
				{
					strValidation = "PurchasePrice";
				}
				else
				{
					strValidation = "MRP";
				}
				strQuery = "Select distinct GdwnStckTranLog.ProductId,GdwnStckTranLog.ProductName,GdwnStckTranLog.GodownName,Product.PGroupId,Product.BrandId,Product.UnitId,Product." + strValidation + " From GdwnStckTranLog Join Product on GdwnStckTranLog.ProductId=Product.ProductId where GdwnStckTranLog.GodownName='" + cmbGdwn.Text + "' " + sBranchDetails;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					dmlBalanceQty = ProStockAddMethod(row[0].ToString());
					intRowCount = Details.Rows.Count + 1;
					AshproCommon.IdFindMethod("ProductGroup", "PGroupName", "PGroupId", row[3].ToString(), out strGroupName);
					AshproCommon.IdFindMethod("Brand", "BrandName", "BrandId", row[4].ToString(), out strBrandName);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[5].ToString(), out strUnitName);
					dmlRate = Convert.ToDecimal(row[6].ToString());
					dmlStockValue = dmlRate * dmlBalanceQty;
					dmlTotalQty += dmlBalanceQty;
					dmlTotalValue += dmlStockValue;
					Details.Rows.Add(intRowCount, row[1], row[2], strGroupName, strBrandName, dmlBalanceQty, strUnitName, row[6], dmlStockValue.ToString("F"));
				}
				strQuery = "select ProductId from StockTranSettings Join StockTransferMaster on StockTranSettings.StockTransferId=StockTransferMaster.StockTransferId where StockTransferMaster.ToGodownName = '" + cmbGdwn.Text + "' " + text + " and StockTransferMaster.TransferDate <= '" + AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
				dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row2 in dataTable.Rows)
				{
					ProDetailsFindMethod(row2[0].ToString());
				}
				Details.Rows.Add("", "Total", "", "", "", dmlTotalQty.ToString("F"), "", "", dmlTotalValue.ToString("F"));
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(84, 84);
				LineData.Rows.Add(400, 400);
				LineData.Rows.Add(500, 500);
				LineData.Rows.Add(540, 540);
				LineData.Rows.Add(650, 650);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 9);
				Header.Rows.Add("Product Group", 95);
				Header.Rows.Add("Quatity", 405);
				Header.Rows.Add("Unit", 502);
				Header.Rows.Add("Rate", 545);
				Header.Rows.Add("Stock Value", 655);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "", 0);
				data.Rows.Add(1, 85, true, true, "", 410);
				data.Rows.Add(5, 499, true, false, "Right", 0);
				data.Rows.Add(6, 505, false, false, "", 0);
				data.Rows.Add(7, 644, false, false, "Right", 0);
				data.Rows.Add(8, 798, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(490, 50, 490, 800);
			LineDataPDF.Rows.Add(415, 50, 415, 800);
			LineDataPDF.Rows.Add(385, 50, 385, 800);
			LineDataPDF.Rows.Add(305, 50, 305, 800);
			LineDataPDF.Rows.Add(30, 50, 30, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("Product Name", 33);
			HeaderPDF.Rows.Add("Quatity", 308);
			HeaderPDF.Rows.Add("Unit", 386);
			HeaderPDF.Rows.Add("Rate", 416);
			HeaderPDF.Rows.Add("Stock Value", 491);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 33, true, false, "", 0);
			dataPDF.Rows.Add(5, 384, true, false, "Right", 0);
			dataPDF.Rows.Add(6, 386, false, false, "", 0);
			dataPDF.Rows.Add(7, 489, false, false, "Right", 0);
			dataPDF.Rows.Add(8, 583, true, false, "Right", 0);
		}

		private void GodownStockReportView_Load(object sender, EventArgs e)
		{
			dtmDate.Value = DateTime.Now.Date;
			AniHelper.FillGodown(cmbGdwn);
			cmbValidation.SelectedIndex = 0;
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			cmbValidation.SelectedIndex = 0;
			dtmDate.Value = DateTime.Now.Date;
			cmbGdwn.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			sTitle = "Stock Report of the godown " + cmbGdwn.Text + " on " + dtmDate.Text;
			ReportGenerateMethod();
			Cursor = Cursors.Arrow;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GodownStockReportView));
            this.cmbValidation = new AshUsercontrol.AshComboBox();
            this.cmbGdwn = new AshUsercontrol.AshComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
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
            // cmbValidation
            // 
            resources.ApplyResources(this.cmbValidation, "cmbValidation");
            this.cmbValidation.EnterColor = System.Drawing.Color.LightPink;
            this.cmbValidation.FormattingEnabled = true;
            this.cmbValidation.Items.AddRange(new object[] {
            resources.GetString("cmbValidation.Items"),
            resources.GetString("cmbValidation.Items1"),
            resources.GetString("cmbValidation.Items2")});
            this.cmbValidation.LeaveColor = System.Drawing.Color.White;
            this.cmbValidation.Name = "cmbValidation";
            // 
            // cmbGdwn
            // 
            resources.ApplyResources(this.cmbGdwn, "cmbGdwn");
            this.cmbGdwn.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGdwn.FormattingEnabled = true;
            this.cmbGdwn.LeaveColor = System.Drawing.Color.White;
            this.cmbGdwn.Name = "cmbGdwn";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
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
            // GodownStockReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cmbValidation);
            this.Controls.Add(this.cmbGdwn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.label1);
            this.Name = "GodownStockReportView";
            this.Load += new System.EventHandler(this.GodownStockReportView_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbGdwn, 0);
            this.Controls.SetChildIndex(this.cmbValidation, 0);
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
