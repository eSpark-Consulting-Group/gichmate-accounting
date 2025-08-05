using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SalesReturnReportView : BaseForm
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

		private Point firstPoint = default(Point);

		private decimal dmlTotalBalance;

		private IContainer components = null;

		private RadioButton rdbVchrDate;

		private Panel pnlMultiple;

		private AshComboBox cmbAcctWise;

		private AshDateTimePicker dtmFrom;

		private Panel pnlRoute;

		private AshComboBox cmbRoute;

		private AshComboBox cmbSM;

		private RadioButton rdbSlsManWise;

		private RadioButton rdbRouteWise;

		private RadioButton rdbEntryDate;

		private AshTextBoxNormal txtProCode;

		private Label rdbBrand;

		private AshComboBox cmbBrand;

		private AshComboBox cmbGroup;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private Label label4;

		private Panel pnlProduct;

		private RadioButton rdbProCodeWise;

		private AshComboBox cmbProduct;

		private RadioButton rdbProNameWise;

		private RadioButton rdbGroupWise;

		private RadioButton rdbAll;

		private AshDateTimePicker dtmTo;

		private Label label1;

		private CheckBox cbDate;

		private CheckBox cbNetTotal;

		private CheckBox cbDayTotal;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Label label2;

		public SalesReturnReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void MethodOfQuery(string strMethodQuery)
		{
			if (rdbAll.Checked)
			{
				QueryMethod(strMethodQuery, "");
			}
			else if (rdbGroupWise.Checked)
			{
				if (cmbBrand.Text == "")
				{
					if (cmbGroup.Text != "")
					{
						QueryMethod(strMethodQuery, "and SalesRtnMaster.SlsRtrnId in ( SELECT SlsRtnId FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId where ProductGroup.PGroupName='" + cmbGroup.Text + "' )");
					}
					else
					{
						QueryMethod(strMethodQuery, "");
					}
					return;
				}
				QueryMethod(strMethodQuery, "and SalesRtnMaster.SlsRtrnId in ( SELECT SlsRtnId FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName='" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' )");
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					QueryMethod(strMethodQuery, "and SalesRtnMaster.SlsRtrnId in ( SELECT SlsRtnId FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId=Product.ProductId where Product.ProductName='" + cmbProduct.Text + "' )");
				}
				else
				{
					QueryMethod(strMethodQuery, "");
				}
			}
			else if (txtProCode.Text != "")
			{
				QueryMethod(strMethodQuery, "and SalesRtnMaster.SlsRtrnId in ( SELECT SlsRtnId FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId=Product.ProductId where Product.ProductCode='" + txtProCode.Text + "' )");
			}
			else
			{
				QueryMethod(strMethodQuery, "");
			}
		}

		private void QueryMethod(string strQueryDate, string strQueryProduct)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			if (cmbRoute.Text != string.Empty)
			{
				text = " and StaffId = '" + cmbSM.SelectedValue.ToInt32() + "' ";
			}
			if (cmbSM.Text != string.Empty)
			{
				text2 = " and RouteId = '" + cmbRoute.SelectedValue.ToInt32() + "' ";
			}
			if (cmbAcctWise.Text != string.Empty)
			{
				text3 = " and PartyAccountId = '" + cmbAcctWise.SelectedValue.ToInt32() + "' ";
			}
			strQry = "SELECT SalesRtnMaster.SlsRtrnId,SalesRtnMaster.SlRtnInvNo,SalesRtnMaster.PartyAccountId,SalesRtnMaster.SlsRtnDate,SalesRtnMaster.Frieght,SalesRtnMaster.AditionalAmnt,SalesRtnMaster.TotalAmount,SalesRtnMaster.TotalDiscount,SalesRtnMaster.BillDiscount FROM SalesRtnMaster where  " + strQueryDate + "  between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'  " + text + text2 + text3 + sBranchDetails + strQueryProduct + "  order by SlsRtnDate,SlRtnInvNo";
		}

		private void ProductAddMethod(string strSalesRtnInvId)
		{
			dmlTotalBalance = default(decimal);
			if (rdbAll.Checked)
			{
				strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDis,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM SlsRtnSettings where SlsRtnId='" + strSalesRtnInvId + "'";
			}
			else if (rdbGroupWise.Checked)
			{
				if (cmbBrand.Text == "")
				{
					if (cmbGroup.Text != "")
					{
						strQry = "SELECT SlsRtnSettings.ProductId,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,SlsRtnSettings.NetAmnt FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and SlsRtnSettings.SlsRtnId='" + strSalesRtnInvId + "'";
					}
					else
					{
						strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDis,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM SlsRtnSettings where SlsRtnId='" + strSalesRtnInvId + "'";
					}
				}
				else
				{
					strQry = "SELECT SlsRtnSettings.ProductId,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,SlsRtnSettings.NetAmnt FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' and SlsRtnSettings.SlsRtnId='" + strSalesRtnInvId + "'";
				}
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					strQry = "SELECT SlsRtnSettings.ProductId,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,SlsRtnSettings.NetAmnt FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId= Product.ProductId where Product.ProductName = '" + cmbProduct.Text + "' and SlsRtnSettings.SlsRtnId='" + strSalesRtnInvId + "'";
				}
				else
				{
					strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDis,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM SlsRtnSettings where SlsRtnId='" + strSalesRtnInvId + "'";
				}
			}
			else if (txtProCode.Text != "")
			{
				strQry = "SELECT SlsRtnSettings.ProductId,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,SlsRtnSettings.NetAmnt FROM SlsRtnSettings Join Product on SlsRtnSettings.ProductId= Product.ProductId where Product.ProductCode = '" + txtProCode.Text + "' and SlsRtnSettings.SlsRtnId='" + strSalesRtnInvId + "'";
			}
			else
			{
				strQry = "SELECT ProductId,ProQty,UnitId,ProRate,GrossAmnt,ProDis,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM SlsRtnSettings where SlsRtnId='" + strSalesRtnInvId + "'";
			}
			string strID = "";
			string strID2 = "";
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQry);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Product", "ProductName", "ProductId", row["ProductId"].ToString(), out strID);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row["UnitId"].ToString(), out strID2);
					Details.Rows.Add(strID, row[1], strID2, row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
					if (!rdbAll.Checked && row[9] != DBNull.Value)
					{
						dmlTotalBalance += Convert.ToDecimal(row[9].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DGVFillMethod(string strQuery)
		{
			string text = "01-01-1899";
			string text2 = "";
			string strID = "";
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			DateTime now = DateTime.Now;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				if (row[3].ToString() != text2)
				{
					if (num == 0m)
					{
						num = num2;
					}
					if (cbDayTotal.Checked)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num);
					}
					num = default(decimal);
				}
				if (row[3].ToString() != text)
				{
					now = Convert.ToDateTime(row[3].ToString());
					text2 = now.ToString();
					if (!cbDate.Checked)
					{
						text = text2;
					}
					Details.Rows.Add("Inv Date - " + now.ToString("dd-MMM-yyyy"), " ", " ", "", "", " ", " ", " ", " ", " ");
				}
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row["PartyAccountId"].ToString(), out strID);
				Details.Rows.Add("Inv No- " + row[1].ToString() + "  [" + strID + "]", " ", " ", "", "", " ", " ", " ", " ", " ");
				ProductAddMethod(row["SlsRtrnId"].ToString());
				if (rdbAll.Checked)
				{
					if (row[7].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Disc Amnt", "", row[7]);
					}
					if (row[4].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Frieght", "", row[4]);
					}
					if (row[5].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Add Amnt", "", row[5]);
					}
					if (row[8].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Add Disc", "", row[8]);
					}
					Details.Rows.Add("", "", "", "", "", "", "", "Total", "", row[8]);
					num2 += Convert.ToDecimal(row[8].ToString());
					num += Convert.ToDecimal(row[8].ToString());
				}
				else
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Total", "", dmlTotalBalance.ToString());
					num2 += dmlTotalBalance;
				}
			}
			if (cbDayTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num);
			}
			if (cbNetTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Net Total", "", num2);
			}
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string[] headers = new string[10] { "Description", "Quantity", "Unit", "Rate", "Gross", "Disc(%)", "Dis Amount", "Tax(%)", "Tax Amount", "Net Amount" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			if (rdbVchrDate.Checked)
			{
				MethodOfQuery("SlsRtnDate");
			}
			else
			{
				MethodOfQuery("EntryDate");
			}
			DGVFillMethod(strQry);
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(340, 340);
				LineData.Rows.Add(405, 405);
				LineData.Rows.Add(460, 460);
				LineData.Rows.Add(545, 545);
				LineData.Rows.Add(600, 600);
				LineData.Rows.Add(670, 670);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Description", 8);
				Header.Rows.Add("Qty", 345);
				Header.Rows.Add("Unit", 410);
				Header.Rows.Add("Rate", 465);
				Header.Rows.Add("Dis", 560);
				Header.Rows.Add("Tax", 610);
				Header.Rows.Add("Net Amount", 675);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, true, true, "", 275);
				data.Rows.Add(1, 342, false, false, "", 0);
				data.Rows.Add(2, 406, false, false, "", 0);
				data.Rows.Add(3, 543, false, false, "Right", 0);
				data.Rows.Add(5, 598, false, false, "Right", 0);
				data.Rows.Add(7, 668, true, false, "Right", 0);
				data.Rows.Add(9, 775, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(285, 50, 285, 800);
			LineDataPDF.Rows.Add(333, 50, 333, 800);
			LineDataPDF.Rows.Add(360, 50, 360, 800);
			LineDataPDF.Rows.Add(425, 50, 425, 800);
			LineDataPDF.Rows.Add(460, 50, 460, 800);
			LineDataPDF.Rows.Add(510, 50, 510, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Description", 9);
			HeaderPDF.Rows.Add("Qty", 298);
			HeaderPDF.Rows.Add("Unit", 335);
			HeaderPDF.Rows.Add("Rate", 380);
			HeaderPDF.Rows.Add("Disc", 433);
			HeaderPDF.Rows.Add("Tax", 470);
			HeaderPDF.Rows.Add("Net Amount", 512);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, true, true, "");
			dataPDF.Rows.Add(1, 286, false, false, "", 0);
			dataPDF.Rows.Add(2, 334, true, false, "", 0);
			dataPDF.Rows.Add(3, 424, false, false, "Right", 0);
			dataPDF.Rows.Add(5, 459, false, false, "Right", 0);
			dataPDF.Rows.Add(7, 509, true, false, "Right", 0);
			dataPDF.Rows.Add(9, 583, true, false, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.PanelClearComboMethod(pnlRoute);
			AniHelper.PanelClearComboMethod(pnlProduct);
			AniHelper.PanelClearComboMethod(pnlMultiple);
			rdbAll.Checked = true;
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (dtmFrom.Text != dtmTo.Text)
			{
				sTitle = "Sales Return Report From " + dtmFrom.Text + " To " + dtmTo.Text;
			}
			else
			{
				sTitle = "Sales Return Report of the Date - " + dtmFrom.Text;
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

		private void pnlCustomers_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				firstPoint = Control.MousePosition;
			}
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

		private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbRouteWise.Checked = true;
		}

		private void cmbSM_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbSlsManWise.Checked = true;
		}

		private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbGroupWise.Checked = true;
		}

		private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbProNameWise.Checked = true;
		}

		private void SalesReturnReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillEmployees(cmbSM);
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbAcctWise);
			AniHelper.FillBranch(cmbBranch);
			AniHelper.ClearComboMethod(this);
			AniHelper.PanelClearComboMethod(pnlProduct);
			AniHelper.PanelClearComboMethod(pnlMultiple);
			AniHelper.PanelClearComboMethod(pnlRoute);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
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
			rdbVchrDate = new System.Windows.Forms.RadioButton();
			pnlMultiple = new System.Windows.Forms.Panel();
			cmbAcctWise = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			pnlRoute = new System.Windows.Forms.Panel();
			cmbRoute = new AshUsercontrol.AshComboBox();
			cmbSM = new AshUsercontrol.AshComboBox();
			rdbSlsManWise = new System.Windows.Forms.RadioButton();
			rdbRouteWise = new System.Windows.Forms.RadioButton();
			rdbEntryDate = new System.Windows.Forms.RadioButton();
			txtProCode = new AshUsercontrol.AshTextBoxNormal();
			rdbBrand = new System.Windows.Forms.Label();
			cmbBrand = new AshUsercontrol.AshComboBox();
			cmbGroup = new AshUsercontrol.AshComboBox();
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			pnlProduct = new System.Windows.Forms.Panel();
			rdbProCodeWise = new System.Windows.Forms.RadioButton();
			cmbProduct = new AshUsercontrol.AshComboBox();
			rdbProNameWise = new System.Windows.Forms.RadioButton();
			rdbGroupWise = new System.Windows.Forms.RadioButton();
			rdbAll = new System.Windows.Forms.RadioButton();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			cbDate = new System.Windows.Forms.CheckBox();
			cbNetTotal = new System.Windows.Forms.CheckBox();
			cbDayTotal = new System.Windows.Forms.CheckBox();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			pnlMultiple.SuspendLayout();
			pnlRoute.SuspendLayout();
			pnlProduct.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(419, -1);
			rdbVchrDate.AutoSize = true;
			rdbVchrDate.Checked = true;
			rdbVchrDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbVchrDate.Location = new System.Drawing.Point(44, 42);
			rdbVchrDate.Name = "rdbVchrDate";
			rdbVchrDate.Size = new System.Drawing.Size(108, 20);
			rdbVchrDate.TabIndex = 145;
			rdbVchrDate.TabStop = true;
			rdbVchrDate.Text = "Voucher Date";
			rdbVchrDate.UseVisualStyleBackColor = true;
			pnlMultiple.BackColor = System.Drawing.Color.White;
			pnlMultiple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlMultiple.Controls.Add(cmbAcctWise);
			pnlMultiple.Controls.Add(label2);
			pnlMultiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlMultiple.Location = new System.Drawing.Point(39, 312);
			pnlMultiple.Name = "pnlMultiple";
			pnlMultiple.Size = new System.Drawing.Size(392, 36);
			pnlMultiple.TabIndex = 132;
			cmbAcctWise.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAcctWise.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAcctWise.EnterColor = System.Drawing.Color.LightPink;
			cmbAcctWise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAcctWise.FormattingEnabled = true;
			cmbAcctWise.LeaveColor = System.Drawing.Color.White;
			cmbAcctWise.Location = new System.Drawing.Point(145, 7);
			cmbAcctWise.Name = "cmbAcctWise";
			cmbAcctWise.Size = new System.Drawing.Size(226, 24);
			cmbAcctWise.TabIndex = 3;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(65, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(71, 16);
			label2.TabIndex = 196;
			label2.Text = "Customer :";
			label2.Visible = false;
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(101, 69);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 22);
			dtmFrom.TabIndex = 136;
			pnlRoute.BackColor = System.Drawing.Color.White;
			pnlRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlRoute.Controls.Add(cmbRoute);
			pnlRoute.Controls.Add(cmbSM);
			pnlRoute.Controls.Add(rdbSlsManWise);
			pnlRoute.Controls.Add(rdbRouteWise);
			pnlRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlRoute.Location = new System.Drawing.Point(43, 99);
			pnlRoute.Name = "pnlRoute";
			pnlRoute.Size = new System.Drawing.Size(388, 63);
			pnlRoute.TabIndex = 133;
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Location = new System.Drawing.Point(142, 6);
			cmbRoute.Name = "cmbRoute";
			cmbRoute.Size = new System.Drawing.Size(226, 24);
			cmbRoute.TabIndex = 1;
			cmbRoute.SelectedIndexChanged += new System.EventHandler(cmbRoute_SelectedIndexChanged);
			cmbSM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSM.EnterColor = System.Drawing.Color.LightPink;
			cmbSM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSM.FormattingEnabled = true;
			cmbSM.LeaveColor = System.Drawing.Color.White;
			cmbSM.Location = new System.Drawing.Point(142, 33);
			cmbSM.Name = "cmbSM";
			cmbSM.Size = new System.Drawing.Size(226, 24);
			cmbSM.TabIndex = 3;
			cmbSM.SelectedIndexChanged += new System.EventHandler(cmbSM_SelectedIndexChanged);
			rdbSlsManWise.AutoSize = true;
			rdbSlsManWise.Location = new System.Drawing.Point(0, 31);
			rdbSlsManWise.Name = "rdbSlsManWise";
			rdbSlsManWise.Size = new System.Drawing.Size(127, 20);
			rdbSlsManWise.TabIndex = 2;
			rdbSlsManWise.TabStop = true;
			rdbSlsManWise.Text = "Sales Man Wise:";
			rdbSlsManWise.UseVisualStyleBackColor = true;
			rdbRouteWise.AutoSize = true;
			rdbRouteWise.Location = new System.Drawing.Point(29, 8);
			rdbRouteWise.Name = "rdbRouteWise";
			rdbRouteWise.Size = new System.Drawing.Size(99, 20);
			rdbRouteWise.TabIndex = 0;
			rdbRouteWise.TabStop = true;
			rdbRouteWise.Text = "Route Wise:";
			rdbRouteWise.UseVisualStyleBackColor = true;
			rdbEntryDate.AutoSize = true;
			rdbEntryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbEntryDate.Location = new System.Drawing.Point(220, 42);
			rdbEntryDate.Name = "rdbEntryDate";
			rdbEntryDate.Size = new System.Drawing.Size(88, 20);
			rdbEntryDate.TabIndex = 146;
			rdbEntryDate.Text = "Entry Date";
			rdbEntryDate.UseVisualStyleBackColor = true;
			txtProCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProCode.Data = 0;
			txtProCode.EnterColor = System.Drawing.Color.LightPink;
			txtProCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProCode.Format = null;
			txtProCode.isAllowNegative = false;
			txtProCode.isAllowSpecialChar = false;
			txtProCode.isNumberOnly = false;
			txtProCode.isNumeric = false;
			txtProCode.LeaveColor = System.Drawing.Color.White;
			txtProCode.Location = new System.Drawing.Point(142, 117);
			txtProCode.Margin = new System.Windows.Forms.Padding(4);
			txtProCode.Name = "txtProCode";
			txtProCode.Size = new System.Drawing.Size(228, 15);
			txtProCode.TabIndex = 8;
			txtProCode.Value = new decimal(new int[4]);
			txtProCode.WaterMark = null;
			rdbBrand.AutoSize = true;
			rdbBrand.Location = new System.Drawing.Point(92, 61);
			rdbBrand.Name = "rdbBrand";
			rdbBrand.Size = new System.Drawing.Size(47, 16);
			rdbBrand.TabIndex = 2;
			rdbBrand.Text = "Brand:";
			cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBrand.FormattingEnabled = true;
			cmbBrand.LeaveColor = System.Drawing.Color.White;
			cmbBrand.Location = new System.Drawing.Point(143, 57);
			cmbBrand.Name = "cmbBrand";
			cmbBrand.Size = new System.Drawing.Size(228, 24);
			cmbBrand.TabIndex = 4;
			cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGroup.FormattingEnabled = true;
			cmbGroup.LeaveColor = System.Drawing.Color.White;
			cmbGroup.Location = new System.Drawing.Point(143, 30);
			cmbGroup.Name = "cmbGroup";
			cmbGroup.Size = new System.Drawing.Size(228, 24);
			cmbGroup.TabIndex = 3;
			cmbGroup.SelectedIndexChanged += new System.EventHandler(cmbGroup_SelectedIndexChanged);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 4);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(197, 24);
			lblTitle.TabIndex = 144;
			lblTitle.Text = "Sales Return Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(300, 406);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 140;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(377, 406);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 141;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(243, 74);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(28, 16);
			label4.TabIndex = 137;
			label4.Text = "To:";
			pnlProduct.BackColor = System.Drawing.Color.White;
			pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlProduct.Controls.Add(txtProCode);
			pnlProduct.Controls.Add(rdbBrand);
			pnlProduct.Controls.Add(cmbBrand);
			pnlProduct.Controls.Add(cmbGroup);
			pnlProduct.Controls.Add(rdbProCodeWise);
			pnlProduct.Controls.Add(cmbProduct);
			pnlProduct.Controls.Add(rdbProNameWise);
			pnlProduct.Controls.Add(rdbGroupWise);
			pnlProduct.Controls.Add(rdbAll);
			pnlProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlProduct.Location = new System.Drawing.Point(41, 168);
			pnlProduct.Name = "pnlProduct";
			pnlProduct.Size = new System.Drawing.Size(390, 138);
			pnlProduct.TabIndex = 134;
			rdbProCodeWise.AutoSize = true;
			rdbProCodeWise.Location = new System.Drawing.Point(50, 115);
			rdbProCodeWise.Name = "rdbProCodeWise";
			rdbProCodeWise.Size = new System.Drawing.Size(89, 20);
			rdbProCodeWise.TabIndex = 7;
			rdbProCodeWise.Text = "Codewise:";
			rdbProCodeWise.UseVisualStyleBackColor = true;
			cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProduct.EnterColor = System.Drawing.Color.LightPink;
			cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProduct.FormattingEnabled = true;
			cmbProduct.LeaveColor = System.Drawing.Color.White;
			cmbProduct.Location = new System.Drawing.Point(142, 87);
			cmbProduct.Name = "cmbProduct";
			cmbProduct.Size = new System.Drawing.Size(228, 24);
			cmbProduct.TabIndex = 6;
			cmbProduct.SelectedIndexChanged += new System.EventHandler(cmbProduct_SelectedIndexChanged);
			rdbProNameWise.AutoSize = true;
			rdbProNameWise.Location = new System.Drawing.Point(37, 88);
			rdbProNameWise.Name = "rdbProNameWise";
			rdbProNameWise.Size = new System.Drawing.Size(102, 20);
			rdbProNameWise.TabIndex = 5;
			rdbProNameWise.Text = "Productwise:";
			rdbProNameWise.UseVisualStyleBackColor = true;
			rdbGroupWise.AutoSize = true;
			rdbGroupWise.Location = new System.Drawing.Point(46, 32);
			rdbGroupWise.Name = "rdbGroupWise";
			rdbGroupWise.Size = new System.Drawing.Size(93, 20);
			rdbGroupWise.TabIndex = 1;
			rdbGroupWise.Text = "Groupwise:";
			rdbGroupWise.UseVisualStyleBackColor = true;
			rdbAll.AutoSize = true;
			rdbAll.Checked = true;
			rdbAll.Location = new System.Drawing.Point(203, 0);
			rdbAll.Name = "rdbAll";
			rdbAll.Size = new System.Drawing.Size(90, 20);
			rdbAll.TabIndex = 0;
			rdbAll.TabStop = true;
			rdbAll.Text = "All Product";
			rdbAll.UseVisualStyleBackColor = true;
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(275, 68);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 22);
			dtmTo.TabIndex = 138;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(22, 75);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(74, 16);
			label1.TabIndex = 135;
			label1.Text = "Date From:";
			cbDate.AutoSize = true;
			cbDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDate.Location = new System.Drawing.Point(272, 352);
			cbDate.Name = "cbDate";
			cbDate.Size = new System.Drawing.Size(163, 20);
			cbDate.TabIndex = 147;
			cbDate.Text = "Date For Every Invoice";
			cbDate.UseVisualStyleBackColor = true;
			cbNetTotal.AutoSize = true;
			cbNetTotal.Checked = true;
			cbNetTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbNetTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbNetTotal.Location = new System.Drawing.Point(137, 352);
			cbNetTotal.Name = "cbNetTotal";
			cbNetTotal.Size = new System.Drawing.Size(118, 20);
			cbNetTotal.TabIndex = 148;
			cbNetTotal.Text = "Show Net Total";
			cbNetTotal.UseVisualStyleBackColor = true;
			cbDayTotal.AutoSize = true;
			cbDayTotal.Checked = true;
			cbDayTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbDayTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDayTotal.Location = new System.Drawing.Point(3, 352);
			cbDayTotal.Name = "cbDayTotal";
			cbDayTotal.Size = new System.Drawing.Size(122, 20);
			cbDayTotal.TabIndex = 149;
			cbDayTotal.Text = "Show Day Total";
			cbDayTotal.UseVisualStyleBackColor = true;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(101, 378);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(311, 24);
			cmbBranch.TabIndex = 197;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(36, 381);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(56, 16);
			lblBranch.TabIndex = 196;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(455, 439);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(cbDate);
			base.Controls.Add(cbNetTotal);
			base.Controls.Add(cbDayTotal);
			base.Controls.Add(rdbVchrDate);
			base.Controls.Add(pnlMultiple);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(pnlRoute);
			base.Controls.Add(rdbEntryDate);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(label4);
			base.Controls.Add(pnlProduct);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label1);
			base.Name = "SalesReturnReportView";
			base.Load += new System.EventHandler(SalesReturnReportView_Load);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(pnlProduct, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(rdbEntryDate, 0);
			base.Controls.SetChildIndex(pnlRoute, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(pnlMultiple, 0);
			base.Controls.SetChildIndex(rdbVchrDate, 0);
			base.Controls.SetChildIndex(cbDayTotal, 0);
			base.Controls.SetChildIndex(cbNetTotal, 0);
			base.Controls.SetChildIndex(cbDate, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			pnlMultiple.ResumeLayout(false);
			pnlMultiple.PerformLayout();
			pnlRoute.ResumeLayout(false);
			pnlRoute.PerformLayout();
			pnlProduct.ResumeLayout(false);
			pnlProduct.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
