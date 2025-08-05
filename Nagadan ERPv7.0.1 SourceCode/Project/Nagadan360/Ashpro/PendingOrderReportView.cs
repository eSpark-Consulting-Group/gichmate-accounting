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
	public class PendingOrderReportView : BaseForm
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

		private string strQry;

		private string sBranchDetails = "";

		private string strFormName = "Sales Order";

		private decimal dmlTotalBalance;

		private Point firstPoint = default(Point);

		private IContainer components = null;

		private Panel pnlProduct;

		private AshTextBoxNormal txtProCode;

		private AshComboBox cmbBrand;

		private AshComboBox cmbGroup;

		private AshComboBox cmbProduct;

		private Panel pnlRoute;

		private AshComboBox cmbRoute;

		private AshComboBox cmbSM;

		private Label label4;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label label1;

		private RadioButton rdbSls;

		private RadioButton rdbPur;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Label rdbBrand;

		private RadioButton rdbProCodeWise;

		private RadioButton rdbProNameWise;

		private RadioButton rdbGroupWise;

		private RadioButton rdbAll;

		private RadioButton rdbSlsManWise;

		private RadioButton rdbRouteWise;

		public PendingOrderReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string[] headers = new string[8] { "Description", "Quantity", "Unit", "Rate", "Gross", "Disc", "Dis Amount", "Net Amount" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			try
			{
				MethodOfQuery("InvDate");
				DGVFillMethod(strQry);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ProductAddMethod(string strSalesOrderMasterId)
		{
			dmlTotalBalance = default(decimal);
			if (rdbAll.Checked)
			{
				strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDiscont,DiscAmnt,NetAmnt FROM SalesOrderSettings where SalesOrderMasterId='" + strSalesOrderMasterId + "'";
			}
			else if (rdbGroupWise.Checked)
			{
				if (cmbBrand.Text == "")
				{
					if (cmbGroup.Text != "")
					{
						strQry = "SELECT SalesOrderSettings.ProductId,SalesOrderSettings.ProQty,SalesOrderSettings.UnitId,SalesOrderSettings.ProRate,SalesOrderSettings.GrossAmnt,SalesOrderSettings.ProDiscont,SalesOrderSettings.DiscAmnt,SalesOrderSettings.NetAmnt FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and SalesOrderSettings.SalesOrderMasterId='" + strSalesOrderMasterId + "'";
					}
					else
					{
						strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDiscont,DiscAmnt,NetAmnt FROM SalesOrderSettings where SalesOrderMasterId='" + strSalesOrderMasterId + "'";
					}
				}
				else
				{
					strQry = "SELECT SalesOrderSettings.ProductId,SalesOrderSettings.ProQty,SalesOrderSettings.UnitId,SalesOrderSettings.ProRate,SalesOrderSettings.GrossAmnt,SalesOrderSettings.ProDiscont,SalesOrderSettings.DiscAmnt,SalesOrderSettings.NetAmnt FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' and SalesOrderSettings.SalesOrderMasterId='" + strSalesOrderMasterId + "'";
				}
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					strQry = "SELECT SalesOrderSettings.ProductId,SalesOrderSettings.ProQty,SalesOrderSettings.UnitId,SalesOrderSettings.ProRate,SalesOrderSettings.GrossAmnt,SalesOrderSettings.ProDiscont,SalesOrderSettings.DiscAmnt,SalesOrderSettings.NetAmnt FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId= Product.ProductId where Product.ProductName = '" + cmbProduct.Text + "' and SalesOrderSettings.SalesOrderMasterId='" + strSalesOrderMasterId + "'";
				}
				else
				{
					strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDiscont,DiscAmnt,NetAmnt FROM SalesOrderSettings where SalesOrderMasterId='" + strSalesOrderMasterId + "'";
				}
			}
			else if (txtProCode.Text != "")
			{
				strQry = "SELECT SalesOrderSettings.ProductId,SalesOrderSettings.ProQty,SalesOrderSettings.UnitId,SalesOrderSettings.ProRate,SalesOrderSettings.GrossAmnt,SalesOrderSettings.ProDiscont,SalesOrderSettings.DiscAmnt,SalesOrderSettings.NetAmnt FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId= Product.ProductId where Product.ProductCode = '" + txtProCode.Text + "' and SalesOrderSettings.SalesOrderMasterId='" + strSalesOrderMasterId + "'";
			}
			else
			{
				strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDiscont,DiscAmnt,NetAmnt FROM SalesOrderSettings where SalesOrderMasterId='" + strSalesOrderMasterId + "'";
			}
			try
			{
				string strID = string.Empty;
				string strID2 = string.Empty;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQry);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Product", "ProductName", "ProductId", row["ProductId"].ToString(), out strID);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row["UnitId"].ToString(), out strID2);
					Details.Rows.Add(strID, row[1], strID2, row[3], row[4], row[5], row[6], row[7]);
					if (!rdbAll.Checked)
					{
						dmlTotalBalance += row[7].ToString().ToDecimal();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MethodOfQuery(string strMethodQuery)
		{
			if (rdbAll.Checked)
			{
				QuerMethod(strMethodQuery, "");
			}
			else if (rdbGroupWise.Checked)
			{
				if (cmbBrand.Text == "")
				{
					if (cmbGroup.Text != "")
					{
						QuerMethod(strMethodQuery, "and SalesOrderMasterId in ( SELECT SalesOrderMasterId FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId where ProductGroup.PGroupName='" + cmbGroup.Text + "' )");
					}
					else
					{
						QuerMethod(strMethodQuery, "");
					}
					return;
				}
				QuerMethod(strMethodQuery, "and SalesOrderMasterId in ( SELECT SalesOrderMasterId FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName='" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' )");
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					QuerMethod(strMethodQuery, "and SalesOrderMasterId in ( SELECT SalesOrderMasterId FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId=Product.ProductId where Product.ProductName='" + cmbProduct.Text + "' )");
				}
				else
				{
					QuerMethod(strMethodQuery, "");
				}
			}
			else if (txtProCode.Text != "")
			{
				QuerMethod(strMethodQuery, "and SalesOrderMasterId in ( SELECT SalesOrderMasterId FROM SalesOrderSettings Join Product on SalesOrderSettings.ProductId=Product.ProductId where Product.ProductCode='" + txtProCode.Text + "' )");
			}
			else
			{
				QuerMethod(strMethodQuery, "");
			}
		}

		private void QuerMethod(string strQueryDate, string strQueryProduct)
		{
			string text = "";
			string text2 = "";
			if (cmbRoute.Text != string.Empty)
			{
				text = " and RouteId = '" + cmbRoute.SelectedValue.ToInt32() + "' ";
			}
			if (cmbSM.Text != string.Empty)
			{
				text2 = " and SalesManId = '" + cmbSM.SelectedValue.ToInt32() + "' ";
			}
			strQry = "SELECT SalesOrderMasterId,VchrNo,CustomerId,InvDate,Freight,AdditionalAmnt,GrandTotal,BillDiscount,AddDiscount FROM SalesOrderMaster where  " + strQueryDate + " between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and Status='" + false + "' and InvType='" + strFormName + "' " + text + text2 + strQueryProduct + sBranchDetails + " order by InvDate,VchrNo";
		}

		private void DGVFillMethod(string strQuery)
		{
			decimal num = default(decimal);
			string empty = string.Empty;
			string text = "0.00";
			string strID = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				Details.Rows.Add("Inv Date - " + row[3].ToString().toDateTime().ToString("dd-MMM-yyyy"), " ", " ", " ", " ", " ", " ", " ");
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row["CustomerId"].ToString(), out strID);
				Details.Rows.Add("Inv No- " + row[1].ToString() + "  [" + strID + "]", " ", " ", " ", " ", " ", " ", " ");
				ProductAddMethod(row["SalesOrderMasterId"].ToString());
				empty = row[4].ToString();
				empty2 = row[5].ToString();
				empty3 = row[7].ToString();
				text = row[8].ToString();
				if (rdbAll.Checked)
				{
					if (text != "0.00")
					{
						Details.Rows.Add("", "", "", "", "", "Disc Amnt", "", row[7]);
					}
					if (empty != "0.00")
					{
						Details.Rows.Add("", "", "", "", "", "Freight", "", row[4]);
					}
					if (empty2 != "0.00")
					{
						Details.Rows.Add("", "", "", "", "", "Add Amnt", "", row[5]);
					}
					if (empty3 != "0.00")
					{
						Details.Rows.Add("", "", "", "", "", "Add Disc", "", row[8]);
					}
					Details.Rows.Add("", "", "", "", "", "Total", "", row[6]);
					num += Convert.ToDecimal(row[6].ToString());
				}
				else
				{
					Details.Rows.Add("", "", "", "", "", "Total", "", dmlTotalBalance.ToString());
					num += dmlTotalBalance;
				}
			}
			Details.Rows.Add("", "", "", "", "", "Net Total", "", num);
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(320, 320);
				LineData.Rows.Add(400, 400);
				LineData.Rows.Add(470, 470);
				LineData.Rows.Add(570, 570);
				LineData.Rows.Add(665, 665);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Description", 9);
				Header.Rows.Add("Qty", 325);
				Header.Rows.Add("Unit", 405);
				Header.Rows.Add("Rate", 475);
				Header.Rows.Add("Dis(%)", 575);
				Header.Rows.Add("Net Amount", 670);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, true, true, "", 275);
				data.Rows.Add(1, 327, false, false, "", 0);
				data.Rows.Add(2, 407, false, false, "", 0);
				data.Rows.Add(3, 568, false, false, "Right", 0);
				data.Rows.Add(5, 572, true, false, "", 0);
				data.Rows.Add(7, 798, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(315, 50, 315, 800);
			LineDataPDF.Rows.Add(361, 50, 361, 800);
			LineDataPDF.Rows.Add(402, 50, 402, 800);
			LineDataPDF.Rows.Add(455, 50, 455, 800);
			LineDataPDF.Rows.Add(508, 50, 508, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Description", 10);
			HeaderPDF.Rows.Add("Qty", 319);
			HeaderPDF.Rows.Add("Unit", 365);
			HeaderPDF.Rows.Add("Rate", 406);
			HeaderPDF.Rows.Add("Dis(%)", 460);
			HeaderPDF.Rows.Add("Net Amount", 512);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, true, true, "");
			dataPDF.Rows.Add(1, 318, false, false, "", 0);
			dataPDF.Rows.Add(2, 363, false, false, "", 0);
			dataPDF.Rows.Add(3, 454, false, false, "Right", 0);
			dataPDF.Rows.Add(5, 456, true, false, "", 0);
			dataPDF.Rows.Add(7, 585, true, false, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.PanelClearComboMethod(pnlRoute);
			AniHelper.PanelClearComboMethod(pnlProduct);
			rdbAll.Checked = true;
			rdbSls.Checked = true;
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (dtmFrom.Value == dtmTo.Value)
			{
				sTitle = "Pending " + strFormName + " Report as on the date " + dtmFrom.Text;
			}
			else
			{
				sTitle = "Pending " + strFormName + " Report From " + dtmFrom.Text + " To " + dtmTo.Text;
			}
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

		private void rdbSls_CheckedChanged(object sender, EventArgs e)
		{
			pnlRoute.Enabled = rdbSls.Checked;
			strFormName = (rdbSls.Checked ? "Sales Order" : "Purchase Order");
		}

		private void pnlCustomers_MouseMove(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			if (e.Button == MouseButtons.Left)
			{
				Point mousePosition = Control.MousePosition;
				Point point = new Point(firstPoint.X - mousePosition.X, firstPoint.Y - mousePosition.Y);
				control.Location = new Point(control.Location.X - point.X, control.Location.Y - point.Y);
				firstPoint = mousePosition;
			}
		}

		private void pnlCustomers_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				firstPoint = Control.MousePosition;
			}
		}

		private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbGroupWise.Checked = true;
		}

		private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbRouteWise.Checked = true;
			cmbSM.Text = string.Empty;
		}

		private void cmbSM_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbSlsManWise.Checked = true;
			cmbRoute.Text = string.Empty;
		}

		private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbProNameWise.Checked = true;
		}

		private void PendingOrderReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillEmployees(cmbSM);
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			AniHelper.PanelClearComboMethod(pnlRoute);
			AniHelper.PanelClearComboMethod(pnlProduct);
			rdbAll.Checked = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingOrderReportView));
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.txtProCode = new AshUsercontrol.AshTextBoxNormal();
            this.rdbBrand = new System.Windows.Forms.Label();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.rdbProCodeWise = new System.Windows.Forms.RadioButton();
            this.cmbProduct = new AshUsercontrol.AshComboBox();
            this.rdbProNameWise = new System.Windows.Forms.RadioButton();
            this.rdbGroupWise = new System.Windows.Forms.RadioButton();
            this.rdbAll = new System.Windows.Forms.RadioButton();
            this.pnlRoute = new System.Windows.Forms.Panel();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.cmbSM = new AshUsercontrol.AshComboBox();
            this.rdbSlsManWise = new System.Windows.Forms.RadioButton();
            this.rdbRouteWise = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbSls = new System.Windows.Forms.RadioButton();
            this.rdbPur = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.pnlProduct.SuspendLayout();
            this.pnlRoute.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // pnlProduct
            // 
            resources.ApplyResources(this.pnlProduct, "pnlProduct");
            this.pnlProduct.BackColor = System.Drawing.Color.White;
            this.pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProduct.Controls.Add(this.txtProCode);
            this.pnlProduct.Controls.Add(this.rdbBrand);
            this.pnlProduct.Controls.Add(this.cmbBrand);
            this.pnlProduct.Controls.Add(this.cmbGroup);
            this.pnlProduct.Controls.Add(this.rdbProCodeWise);
            this.pnlProduct.Controls.Add(this.cmbProduct);
            this.pnlProduct.Controls.Add(this.rdbProNameWise);
            this.pnlProduct.Controls.Add(this.rdbGroupWise);
            this.pnlProduct.Controls.Add(this.rdbAll);
            this.pnlProduct.Name = "pnlProduct";
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
            // rdbBrand
            // 
            resources.ApplyResources(this.rdbBrand, "rdbBrand");
            this.rdbBrand.Name = "rdbBrand";
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
            // cmbGroup
            // 
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // rdbProCodeWise
            // 
            resources.ApplyResources(this.rdbProCodeWise, "rdbProCodeWise");
            this.rdbProCodeWise.Name = "rdbProCodeWise";
            this.rdbProCodeWise.UseVisualStyleBackColor = true;
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
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // rdbProNameWise
            // 
            resources.ApplyResources(this.rdbProNameWise, "rdbProNameWise");
            this.rdbProNameWise.Name = "rdbProNameWise";
            this.rdbProNameWise.UseVisualStyleBackColor = true;
            // 
            // rdbGroupWise
            // 
            resources.ApplyResources(this.rdbGroupWise, "rdbGroupWise");
            this.rdbGroupWise.Name = "rdbGroupWise";
            this.rdbGroupWise.UseVisualStyleBackColor = true;
            // 
            // rdbAll
            // 
            resources.ApplyResources(this.rdbAll, "rdbAll");
            this.rdbAll.Checked = true;
            this.rdbAll.Name = "rdbAll";
            this.rdbAll.TabStop = true;
            this.rdbAll.UseVisualStyleBackColor = true;
            // 
            // pnlRoute
            // 
            resources.ApplyResources(this.pnlRoute, "pnlRoute");
            this.pnlRoute.BackColor = System.Drawing.Color.White;
            this.pnlRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRoute.Controls.Add(this.cmbRoute);
            this.pnlRoute.Controls.Add(this.cmbSM);
            this.pnlRoute.Controls.Add(this.rdbSlsManWise);
            this.pnlRoute.Controls.Add(this.rdbRouteWise);
            this.pnlRoute.Name = "pnlRoute";
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
            this.cmbRoute.SelectedIndexChanged += new System.EventHandler(this.cmbRoute_SelectedIndexChanged);
            // 
            // cmbSM
            // 
            resources.ApplyResources(this.cmbSM, "cmbSM");
            this.cmbSM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSM.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSM.FormattingEnabled = true;
            this.cmbSM.LeaveColor = System.Drawing.Color.White;
            this.cmbSM.Name = "cmbSM";
            this.cmbSM.SelectedIndexChanged += new System.EventHandler(this.cmbSM_SelectedIndexChanged);
            // 
            // rdbSlsManWise
            // 
            resources.ApplyResources(this.rdbSlsManWise, "rdbSlsManWise");
            this.rdbSlsManWise.Name = "rdbSlsManWise";
            this.rdbSlsManWise.TabStop = true;
            this.rdbSlsManWise.UseVisualStyleBackColor = true;
            // 
            // rdbRouteWise
            // 
            resources.ApplyResources(this.rdbRouteWise, "rdbRouteWise");
            this.rdbRouteWise.Name = "rdbRouteWise";
            this.rdbRouteWise.TabStop = true;
            this.rdbRouteWise.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
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
            // rdbSls
            // 
            resources.ApplyResources(this.rdbSls, "rdbSls");
            this.rdbSls.Checked = true;
            this.rdbSls.Name = "rdbSls";
            this.rdbSls.TabStop = true;
            this.rdbSls.UseVisualStyleBackColor = true;
            this.rdbSls.CheckedChanged += new System.EventHandler(this.rdbSls_CheckedChanged);
            // 
            // rdbPur
            // 
            resources.ApplyResources(this.rdbPur, "rdbPur");
            this.rdbPur.Name = "rdbPur";
            this.rdbPur.UseVisualStyleBackColor = true;
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
            // PendingOrderReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdbSls);
            this.Controls.Add(this.rdbPur);
            this.Controls.Add(this.pnlProduct);
            this.Controls.Add(this.pnlRoute);
            this.Name = "PendingOrderReportView";
            this.Load += new System.EventHandler(this.PendingOrderReportView_Load);
            this.Controls.SetChildIndex(this.pnlRoute, 0);
            this.Controls.SetChildIndex(this.pnlProduct, 0);
            this.Controls.SetChildIndex(this.rdbPur, 0);
            this.Controls.SetChildIndex(this.rdbSls, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.pnlProduct.ResumeLayout(false);
            this.pnlProduct.PerformLayout();
            this.pnlRoute.ResumeLayout(false);
            this.pnlRoute.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
