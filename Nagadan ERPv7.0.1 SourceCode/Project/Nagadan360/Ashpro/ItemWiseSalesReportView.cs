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
	public class ItemWiseSalesReportView : BaseForm
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

		private IContainer components = null;

		private AshDateTimePicker dtmTo;

		private Label label5;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private AshComboBox cmbRoute;

		private Label label8;

		private AshComboBox cmbGroup;

		private Label label1;

		private Label label4;

		private AshComboBox cmbSupplier;

		private Label label7;

		private AshComboBox cmbBrand;

		private Label label2;

		private AshComboBox cmbSMan;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbProduct;

		private Label label3;

		private CheckBox cbSalesReturn;

		private CheckBox cbSales;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string strFormType { get; set; }

		public ItemWiseSalesReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " and SalesEstimateMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
				text2 = " and SalesRtnMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
				text3 = " and PurInvMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
				text4 = " and PurRtnInvMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string[] headers = new string[10] { "SL", "Type", "Product Name", "Qty", "Unit", "Rate", "Gross Amount", "Discount Amount", "Tax Amount", "Net Total" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(150, "L");
			if (Settings.Default.Language == "ar")
			{
				Gridview.Rows.Add(300, "R");
			}
			else
			{
				Gridview.Rows.Add(300, "L");
			}
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			string text9 = string.Empty;
			string text10 = string.Empty;
			string text11 = "";
			string text12 = "";
			string text13 = "";
			string text14 = "";
			string text15 = "";
			string text16 = "";
			string text17 = "";
			string text18 = "";
			string text19;
			string text20;
			if (strFormType == "Sales")
			{
				text19 = " Join Product on SalesEstimateSettings.ProductId=Product.ProductId";
				text20 = " Join Product on SlsRtnSettings.ProductId=Product.ProductId";
			}
			else
			{
				text19 = " Join Product on PurInvSettings.ProId=Product.ProductId ";
				text20 = " Join Product on PurRtnInvSttings.ProId=Product.ProductId";
			}
			if (cmbGroup.Text != "")
			{
				text5 = " and Product.PGroupId='" + cmbGroup.SelectedValue.ToInt32() + "' ";
			}
			if (cmbBrand.Text != "")
			{
				text6 = " and Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "' ";
			}
			if (cmbProduct.Text != "")
			{
				text7 = " and Product.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
			}
			if (cmbSupplier.Text != "")
			{
				text8 = "and Product.AccountId='" + cmbSupplier.SelectedValue.ToInt32() + "' ";
			}
			if (cmbSMan.Text != "" || cmbRoute.Text != "")
			{
				text9 = " Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId ";
				text10 = " Join SalesRtnMaster on SlsRtnSettings.SlsRtnId=SalesRtnMaster.SlsRtrnId ";
			}
			if (cmbSMan.Text != "")
			{
				text11 = " Join EmployeeDetails on SalesEstimateMaster.StafId = EmployeeDetails.EmpID ";
				text12 = " Join EmployeeDetails on SalesRtnMaster.StaffId = EmployeeDetails.EmpID ";
				text13 = " and EmployeeDetails.EmpID='" + cmbSMan.SelectedValue.ToInt32() + "' ";
			}
			if (cmbRoute.Text != "")
			{
				text14 = "Join Root on SalesEstimateMaster.RouteId=Root.RootId ";
				text15 = " Join Root on SalesRtnMaster.RouteId=Root.RootId ";
				text16 = " and Root.RootName='" + cmbRoute.Text + "'";
			}
			try
			{
				if (cbSales.Checked && cbSalesReturn.Checked)
				{
					if (strFormType == "Sales")
					{
						strQuery = "Select SalesEstimateSettings.ProductId,SalesEstimateSettings.UnitId,SUM(SalesEstimateSettings.ProQty) as Qty,sum(SalesEstimateSettings.GrossAmnt)/sum(SalesEstimateSettings.ProQty) as Rate,sum(SalesEstimateSettings.GrossAmnt) as grossAmount,SUM(SalesEstimateSettings.DiscAmnt) as DisAmount,SUM(SalesEstimateSettings.NetAmnt) as Netamount,SUM(SalesEstimateSettings.GSTAMOUNT) as TotalTax From SalesEstimateSettings " + text19 + text9 + text11 + text14 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text18 + text + " Group by SalesEstimateSettings.ProductId,SalesEstimateSettings.UnitId Union All Select SlsRtnSettings.ProductId,SlsRtnSettings.UnitId,SUM(0-SlsRtnSettings.ProQty) as Qty,SUM(SlsRtnSettings.GrossAmnt)/SUM(SlsRtnSettings.ProQty) as Rate,sum(0-SlsRtnSettings.GrossAmnt) as grossAmount,SUM(0-SlsRtnSettings.DiscAmnt) as DisAmount,SUM(0-SlsRtnSettings.NetAmnt) as Netamount,SUM(0-SlsRtnSettings.GSTAMOUNT) as TotalTax From SlsRtnSettings" + text20 + text10 + text12 + text15 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text17 + text2 + " Group by SlsRtnSettings.ProductId,SlsRtnSettings.UnitId";
					}
					else
					{
						strQuery = "Select PurInvSettings.ProId,PurInvSettings.UnitId,SUM(PurInvSettings.ProQty) as Qty,sum(PurInvSettings.Gross)/sum(PurInvSettings.ProQty) as Rate,sum(PurInvSettings.Gross) as grossAmount,SUM(PurInvSettings.DiscAmnt) as DisAmount,SUM(PurInvSettings.NetAmnt) as Netamount,SUM(PurInvSettings.GSTAMOUNT) as TotalTax From PurInvSettings " + text19 + text9 + text11 + text14 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text18 + text3 + " Group by PurInvSettings.ProId,PurInvSettings.UnitId Union All Select PurRtnInvSttings.ProId,PurRtnInvSttings.UnitId,SUM(0-PurRtnInvSttings.ProQty) as Qty,SUM(PurRtnInvSttings.Gross)/SUM(PurRtnInvSttings.ProQty) as Rate,sum(0-PurRtnInvSttings.Gross) as grossAmount,SUM(0-PurRtnInvSttings.DiscAmnt) as DisAmount,SUM(0-PurRtnInvSttings.NetAmnt) as Netamount,SUM(0-PurRtnInvSttings.GSTAMOUNT) as TotalTax From PurRtnInvSttings" + text20 + text10 + text12 + text15 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text17 + text4 + " Group by PurRtnInvSttings.ProId,PurRtnInvSttings.UnitId";
					}
				}
				else if (cbSales.Checked && !cbSalesReturn.Checked)
				{
					if (strFormType == "Sales")
					{
						strQuery = "Select SalesEstimateSettings.ProductId,SalesEstimateSettings.UnitId,SUM(ProQty) as Qty,sum(SalesEstimateSettings.GrossAmnt)/sum(SalesEstimateSettings.ProQty) as Rate,sum(GrossAmnt) as grossAmount,SUM(DiscAmnt) as DisAmount,SUM(NetAmnt) as Netamount,SUM(SalesEstimateSettings.GSTAMOUNT) as TotalTax From SalesEstimateSettings " + text19 + text9 + text11 + text14 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text18 + text + " Group by SalesEstimateSettings.ProductId,SalesEstimateSettings.UnitId ";
					}
					else
					{
						strQuery = "Select PurInvSettings.ProId,PurInvSettings.UnitId,SUM(ProQty) as Qty,sum(PurInvSettings.Gross)/sum(PurInvSettings.ProQty) as Rate,sum(Gross) as grossAmount,SUM(DiscAmnt) as DisAmount,SUM(NetAmnt) as Netamount,SUM(PurInvSettings.GSTAMOUNT) as TotalTax From PurInvSettings " + text19 + text9 + text11 + text14 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text18 + text3 + " Group by PurInvSettings.ProId,PurInvSettings.UnitId ";
					}
				}
				else if (!cbSales.Checked && cbSalesReturn.Checked)
				{
					if (strFormType == "Sales")
					{
						strQuery = "Select SlsRtnSettings.ProductId,SlsRtnSettings.UnitId,SUM(ProQty) as Qty,SUM(GrossAmnt)/SUM(ProQty) as Rate,sum(GrossAmnt) as grossAmount,SUM(DiscAmnt) as DisAmount,SUM(NetAmnt) as Netamount,SUM(SlsRtnSettings.GSTAMOUNT) as TotalTax From SlsRtnSettings" + text20 + text10 + text12 + text15 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text17 + text2 + " Group by SlsRtnSettings.ProductId,SlsRtnSettings.UnitId";
					}
					else
					{
						strQuery = "Select PurRtnInvSttings.ProId,PurRtnInvSttings.UnitId,SUM(ProQty) as Qty,SUM(Gross)/SUM(ProQty) as Rate,sum(Gross) as grossAmount,SUM(DiscAmnt) as DisAmount,SUM(NetAmnt) as Netamount,,SUM(PurRtnInvSttings.GSTAMOUNT) as TotalTax From PurRtnInvSttings" + text20 + text10 + text12 + text15 + " where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text5 + text6 + text7 + text8 + text13 + text16 + text17 + text4 + " Group by PurRtnInvSttings.ProId,PurRtnInvSttings.UnitId";
					}
				}
				string strID = string.Empty;
				string strID2 = string.Empty;
				string text21 = string.Empty;
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					num += row[2].ToString().ToDecimal();
					num2 += row[4].ToString().ToDecimal();
					num3 += row[5].ToString().ToDecimal();
					if (row[7] != DBNull.Value)
					{
						num4 += row[7].ToString().ToDecimal();
					}
					num5 += Convert.ToDecimal(row[6].ToString());
					num6 = row[3].ToString().ToDecimal();
					AshproCommon.IdFindMethod("Product", "ProductName", "ProductId", row[0].ToString(), out strID);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[1].ToString(), out strID2);
					if (strFormType == "Sales")
					{
						if (cbSales.Checked && cbSalesReturn.Checked)
						{
							text21 = ((!(Convert.ToDecimal(row[6].ToString()) < 0m)) ? "Sales" : "Sales Return");
						}
						else if (cbSales.Checked && !cbSalesReturn.Checked)
						{
							text21 = "Sales";
						}
						else if (!cbSales.Checked && cbSalesReturn.Checked)
						{
							text21 = "Sales Return";
						}
					}
					else if (cbSales.Checked && cbSalesReturn.Checked)
					{
						text21 = ((!(Convert.ToDecimal(row[6].ToString()) < 0m)) ? "Purchase" : "Purchase Return");
					}
					else if (cbSales.Checked && !cbSalesReturn.Checked)
					{
						text21 = "Purchase";
					}
					else if (!cbSales.Checked && cbSalesReturn.Checked)
					{
						text21 = "Purchase Return";
					}
					Details.Rows.Add(Details.Rows.Count + 1, text21, strID, row[2].ToString().ToDecimal().ToString(CommonSettings.strQuantityFormat), strID2, num6.ToString(CommonSettings.strCurencyFormat), row[4].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[5].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[7].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[6].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat));
				}
				Details.Rows.Add("", "", "Total", num.ToString(CommonSettings.strQuantityFormat), "", "", num2.ToString(CommonSettings.strCurencyFormat), num3.ToString(CommonSettings.strCurencyFormat), num4.ToString(CommonSettings.strCurencyFormat), num5.ToString(CommonSettings.strCurencyFormat));
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
				LineData.Rows.Add(58, 58);
				LineData.Rows.Add(93, 93);
				LineData.Rows.Add(315, 315);
				LineData.Rows.Add(385, 385);
				LineData.Rows.Add(435, 435);
				LineData.Rows.Add(515, 515);
				LineData.Rows.Add(595, 595);
				LineData.Rows.Add(675, 675);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 10);
				Header.Rows.Add("Type", 60);
				Header.Rows.Add("Product", 95);
				Header.Rows.Add("Qty", 317);
				Header.Rows.Add("Unit", 387);
				Header.Rows.Add("Rate", 437);
				Header.Rows.Add("Disc", 517);
				Header.Rows.Add("Tax", 597);
				Header.Rows.Add("Net Amt", 677);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 10, false, false, "", 0);
				data.Rows.Add(1, 60, false, false, "", 0);
				if (Settings.Default.Language == "ar")
				{
					data.Rows.Add(2, 95, true, true, "Right", 220);
				}
				else
				{
					data.Rows.Add(2, 95, true, true, "", 220);
				}
				data.Rows.Add(3, 317, true, false, "", 0);
				data.Rows.Add(4, 387, true, false, "", 0);
				data.Rows.Add(5, 515, true, false, "Right", 0);
				data.Rows.Add(7, 595, true, false, "Right", 0);
				data.Rows.Add(8, 675, true, false, "Right", 0);
				data.Rows.Add(9, 798, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(508, 50, 508, 800);
			LineDataPDF.Rows.Add(438, 50, 438, 800);
			LineDataPDF.Rows.Add(368, 50, 368, 800);
			LineDataPDF.Rows.Add(298, 50, 298, 800);
			LineDataPDF.Rows.Add(268, 50, 268, 800);
			LineDataPDF.Rows.Add(223, 50, 223, 800);
			LineDataPDF.Rows.Add(73, 50, 73, 800);
			LineDataPDF.Rows.Add(43, 50, 43, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 10);
			HeaderPDF.Rows.Add("Type", 45);
			HeaderPDF.Rows.Add("Product", 75);
			HeaderPDF.Rows.Add("Qty", 225);
			HeaderPDF.Rows.Add("Unit", 270);
			HeaderPDF.Rows.Add("Rate", 300);
			HeaderPDF.Rows.Add("Disc", 370);
			HeaderPDF.Rows.Add("Tax", 440);
			HeaderPDF.Rows.Add("Net Amnt", 510);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 9, false, false, "");
			dataPDF.Rows.Add(1, 44, false, false, "", 0);
			dataPDF.Rows.Add(2, 74, true, false, "", 0);
			dataPDF.Rows.Add(3, 223, true, false, "", 0);
			dataPDF.Rows.Add(4, 269, true, false, "", 0);
			dataPDF.Rows.Add(5, 367, true, false, "Right", 0);
			dataPDF.Rows.Add(7, 437, true, false, "Right", 0);
			dataPDF.Rows.Add(8, 507, true, false, "Right", 0);
			dataPDF.Rows.Add(9, 584, true, false, "Right", 0);
		}

		private void ItemWiseSalesReportView_Load(object sender, EventArgs e)
		{
			dtmFrom.Value = DateTime.Now.Date;
			dtmTo.Value = dtmFrom.Value.AddHours(24.0).AddSeconds(-1.0);
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillEmployees(cmbSMan);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cmbGroup.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (strFormType == "Sales")
			{
				if (cbSales.Checked && cbSalesReturn.Checked)
				{
					sTitle = "Item wise Sales and Sales Return Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
				else if (cbSales.Checked && !cbSalesReturn.Checked)
				{
					sTitle = "Item wise Sales Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
				else
				{
					sTitle = "Item wise Sales Return Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
			}
			else if (cbSales.Checked && cbSalesReturn.Checked)
			{
				sTitle = "Item wise Purchase and Purchase Return Report from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else if (cbSales.Checked && !cbSalesReturn.Checked)
			{
				sTitle = "Item wise Purchase Report from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else
			{
				sTitle = "Item wise Purchase Return Report from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			ReportGenerateMethod();
			PrintMethod();
			MailMethod();
			ReportView reportView = new ReportView();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemWiseSalesReportView));
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSupplier = new AshUsercontrol.AshComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSMan = new AshUsercontrol.AshComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbProduct = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSalesReturn = new System.Windows.Forms.CheckBox();
            this.cbSales = new System.Windows.Forms.CheckBox();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
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
            // cmbRoute
            // 
            resources.ApplyResources(this.cmbRoute, "cmbRoute");
            this.cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Name = "cmbRoute";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cmbGroup
            // 
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbGroup.Name = "cmbGroup";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmbSupplier
            // 
            resources.ApplyResources(this.cmbSupplier, "cmbSupplier");
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.LeaveColor = System.Drawing.Color.White;
            this.cmbSupplier.Name = "cmbSupplier";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbBrand
            // 
            resources.ApplyResources(this.cmbBrand, "cmbBrand");
            this.cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBrand.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.LeaveColor = System.Drawing.Color.White;
            this.cmbBrand.Name = "cmbBrand";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbSMan
            // 
            resources.ApplyResources(this.cmbSMan, "cmbSMan");
            this.cmbSMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSMan.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSMan.FormattingEnabled = true;
            this.cmbSMan.LeaveColor = System.Drawing.Color.White;
            this.cmbSMan.Name = "cmbSMan";
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
            // cmbProduct
            // 
            resources.ApplyResources(this.cmbProduct, "cmbProduct");
            this.cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProduct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.LeaveColor = System.Drawing.Color.White;
            this.cmbProduct.Name = "cmbProduct";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbSalesReturn
            // 
            resources.ApplyResources(this.cbSalesReturn, "cbSalesReturn");
            this.cbSalesReturn.Checked = true;
            this.cbSalesReturn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSalesReturn.Name = "cbSalesReturn";
            this.cbSalesReturn.UseVisualStyleBackColor = true;
            // 
            // cbSales
            // 
            resources.ApplyResources(this.cbSales, "cbSales");
            this.cbSales.Checked = true;
            this.cbSales.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSales.Name = "cbSales";
            this.cbSales.UseVisualStyleBackColor = true;
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
            // ItemWiseSalesReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cbSalesReturn);
            this.Controls.Add(this.cbSales);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbRoute);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.cmbSMan);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Name = "ItemWiseSalesReportView";
            this.Load += new System.EventHandler(this.ItemWiseSalesReportView_Load);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.cmbSMan, 0);
            this.Controls.SetChildIndex(this.cmbProduct, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbBrand, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cmbSupplier, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbGroup, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cmbRoute, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.cbSales, 0);
            this.Controls.SetChildIndex(this.cbSalesReturn, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
