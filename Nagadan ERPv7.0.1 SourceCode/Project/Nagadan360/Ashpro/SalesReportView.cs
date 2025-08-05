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
	public class SalesReportView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string strQry;

		private string sTitle;

		private Point firstPoint = default(Point);

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbSalesman;

		private Label label4;

		private AshDateTimePicker dtmTo;

		private Label label1;

		private Panel pnlProduct;

		private Label rdbBrand;

		private AshComboBox cmbBrand;

		private AshComboBox cmbGroup;

		private AshComboBox cmbProduct;

		private AshComboBox cmbRoute;

		private AshDateTimePicker dtmFrom;

		private RadioButton rdbEntryDate;

		private RadioButton rdbVchrDate;

		private CheckBox cbDate;

		private CheckBox cbNetTotal;

		private CheckBox cbDayTotal;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Label label6;

		private Label label5;

		private Label label3;

		private Label label2;

		private AshComboBox cmbParty;

		private Label label7;

		private Panel pnlRoute;

		public string PassingValue { get; set; }

		public SalesReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private string MasterLoadMethod()
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string text2 = string.Empty;
			string text3 = " InvType <>'Normal Sales' ";
			if (PassingValue == "Normal Sales")
			{
				text3 = " InvType ='Normal Sales' ";
			}
			string text4 = string.Empty;
			string text5 = " And SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and ' " + AniHelper.GetDate(dtmTo.Value) + "' ";
			if (rdbEntryDate.Checked)
			{
				text5 = " And EntryDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' ";
			}
			string text6 = string.Empty;
			if (cmbRoute.Text != string.Empty)
			{
				text6 = " and RouteId='" + cmbRoute.SelectedValue.ToInt32() + "'";
			}
			string text7 = string.Empty;
			if (cmbSalesman.Text != string.Empty)
			{
				text7 = " and StafId ='" + cmbSalesman.SelectedValue.ToInt32() + "' ";
			}
			string text8 = string.Empty;
			if (cmbParty.Text != string.Empty)
			{
				text8 = " And PartyAccountId='" + cmbParty.SelectedValue.ToInt32() + "' ";
			}
			string text9 = string.Empty;
			if (cmbGroup.Text != string.Empty)
			{
				text2 = " left Join SalesEstimateSettings on SalesEstimateSettings.SalesInvId = SalesEstimateMaster.SalesInvId ";
				text4 = " Left Join Product  on SalesEstimateSettings.ProductId = Product.ProductId ";
				text9 = " And Product.PGroupId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
			}
			string text10 = string.Empty;
			if (cmbBrand.Text != string.Empty)
			{
				text2 = " left Join SalesEstimateSettings on SalesEstimateSettings.SalesInvId = SalesEstimateMaster.SalesInvId ";
				text4 = " Left Join Product  on SalesEstimateSettings.ProductId = Product.ProductId ";
				text10 = " And Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "' ";
			}
			string text11 = string.Empty;
			if (cmbProduct.Text != string.Empty)
			{
				text2 = " left Join SalesEstimateSettings  on SalesEstimateSettings.SalesInvId = SalesEstimateMaster.SalesInvId ";
				text11 = " And SalesEstimateSettings.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
			}
			return "SELECT SalesEstimateMaster.SalesInvId,SalesInvNo,SalesEstimateMaster.PartyAccountId,SalesDate,Frieght,AditionalAmount,GrandDiscount,BillDiscount,TotalDiscount,InvType,TotalTaxAmount  FROM SalesEstimateMaster " + text2 + text4 + " where  " + text3 + text5 + text6 + text7 + text8 + text9 + text10 + text11 + text + " order by SalesDate,SalesInvNo";
		}

		private void DGVFillMethod()
		{
			string text = "01-01-1899";
			string text2 = "";
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			DateTime now = DateTime.Now;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(MasterLoadMethod());
			foreach (DataRow row in dataTable.Rows)
			{
				if (text2 != string.Empty && row[3].ToString().toDateTime().Date.ToString() != text2 && num != 0m)
				{
					if (cbDayTotal.Checked)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num.ToString(CommonSettings.strCurencyFormat));
					}
					num = default(decimal);
					text2 = string.Empty;
				}
				if (row[3].ToString().toDateTime().Date.ToString() != text)
				{
					now = Convert.ToDateTime(row[3].ToString());
					text2 = now.Date.ToString();
					if (!cbDate.Checked)
					{
						text = text2;
					}
					Details.Rows.Add("Inv Date - " + now.ToString("dd-MMM-yyyy"), " ", " ", " ", " ", " ", " ", " ", " ", " ");
				}
				string strID = string.Empty;
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row["PartyAccountId"].ToString(), out strID);
				if (PassingValue == "Normal Sales")
				{
					Details.Rows.Add("Inv No- " + row[1].ToString() + "  [" + strID + "]", " ", " ", " ", " ", " ", " ", " ", " ", " ");
				}
				else
				{
					Details.Rows.Add("Inv No (" + row[9].ToString() + ") - " + row[1].ToString() + "  [" + strID + "]", " ", " ", " ", " ", " ", " ", " ", " ", " ");
				}
				ProductAddMethod(row["SalesInvId"].ToInt32());
				decimal num3 = row[4].ToString().ToDecimal();
				decimal num4 = row[8].ToString().ToDecimal();
				if (num4 > 0m)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Disc Amnt", "", num4.ToString(CommonSettings.strCurencyFormat));
				}
				if (num3 > 0m)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Frieght", "", num3.ToString(CommonSettings.strCurencyFormat));
				}
				decimal num5 = row[5].ToString().ToDecimal();
				if (num5 > 0m)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Add Amnt", "", num5.ToString(CommonSettings.strCurencyFormat));
				}
				decimal num6 = row[7].ToString().ToDecimal();
				if (num6 > 0m)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Add Disc", "", num6.ToString(CommonSettings.strCurencyFormat));
				}
				decimal num7 = row[10].ToString().ToDecimal();
				if (num7 > 0m)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Tax", "", num7.ToString(CommonSettings.strCurencyFormat));
				}
				Details.Rows.Add("", "", "", "", "", "", "", "Total", "", row[6].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat));
				num2 += row[6].ToString().ToDecimal();
				num += Convert.ToDecimal(row[6].ToString());
			}
			if (cbDayTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num.ToString(CommonSettings.strCurencyFormat));
			}
			if (cbNetTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Net Total", "", num2.ToString(CommonSettings.strCurencyFormat));
			}
		}

		private void ProductAddMethod(int iSalesInvId)
		{
			string text = string.Empty;
			if (cmbBrand.Text != string.Empty)
			{
				text = " And Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "'";
			}
			string text2 = string.Empty;
			if (cmbGroup.Text != string.Empty)
			{
				text2 = " And Product.PGroupId='" + cmbGroup.SelectedValue.ToInt32() + "' ";
			}
			string text3 = string.Empty;
			if (cmbProduct.Text != string.Empty)
			{
				text3 = " And SalesEstimateSettings.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "'";
			}
			strQry = "SELECT Product.ProductName,ProQty,UnitName,ProRate,GrossAmnt,ProDiscont,DiscAmnt,GST,GSTAMOUNT,NetAmnt  FROM SalesEstimateSettings  Left Join Product on SalesEstimateSettings.ProductId = Product.ProductId  Left Join Unit on SalesEstimateSettings.UnitId = Unit.UnitId  where SalesInvId='" + iSalesInvId + "' " + text + text2 + text3 + " order by ProductName ";
			string text4 = "0.00";
			string text5 = "0.00";
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQry);
				foreach (DataRow row in dataTable.Rows)
				{
					text4 = ((row[8] == DBNull.Value) ? "0.00" : row[8].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat));
					text5 = ((row[9] == DBNull.Value) ? "0.00" : row[9].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat));
					Details.Rows.Add(row[0], row[1].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[2].ToString(), row[3].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[4].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[5].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[6].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[7].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), text4, text5);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ReportGenerateMethod()
		{
			string[] headers = new string[10] { "Description", "Quantity", "Unit", "Rate", "Gross", "Disc(%)", "Dis Amount", "Tax(%)", "Tax Amount", "Net Amount" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(80, "R");
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(80, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(80, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			DGVFillMethod();
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
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (dtmFrom.Value.Date != dtmTo.Value.Date)
			{
				if (PassingValue == "Normal Sales")
				{
					sTitle = "Sales Estimate Report From " + dtmFrom.Text + " To " + dtmTo.Text;
				}
				else
				{
					sTitle = "Sales Report From " + dtmFrom.Text + " To " + dtmTo.Text;
				}
			}
			else if (PassingValue == "Normal Sales")
			{
				sTitle = "Sales Estimate Report of the Date - " + dtmFrom.Text;
			}
			else
			{
				sTitle = "Sales Report of the Date - " + dtmFrom.Text;
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

		private void SalesReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbParty);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			AniHelper.PanelClearComboMethod(pnlProduct);
			AniHelper.PanelClearComboMethod(pnlRoute);
			AniHelper.FillEmployees(cmbSalesman);
			dtmFrom.Value = DateTime.Now.Date;
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
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbSalesman = new AshUsercontrol.AshComboBox();
			label4 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			pnlProduct = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			cmbRoute = new AshUsercontrol.AshComboBox();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			cmbParty = new AshUsercontrol.AshComboBox();
			cbNetTotal = new System.Windows.Forms.CheckBox();
			cbDayTotal = new System.Windows.Forms.CheckBox();
			label7 = new System.Windows.Forms.Label();
			rdbBrand = new System.Windows.Forms.Label();
			cmbBrand = new AshUsercontrol.AshComboBox();
			cmbGroup = new AshUsercontrol.AshComboBox();
			cmbProduct = new AshUsercontrol.AshComboBox();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			rdbEntryDate = new System.Windows.Forms.RadioButton();
			rdbVchrDate = new System.Windows.Forms.RadioButton();
			cbDate = new System.Windows.Forms.CheckBox();
			pnlRoute = new System.Windows.Forms.Panel();
			pnlProduct.SuspendLayout();
			pnlRoute.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(421, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 4);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(129, 24);
			lblTitle.TabIndex = 127;
			lblTitle.Text = "Sales Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(246, 435);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 28);
			btnShow.TabIndex = 123;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(324, 435);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 28);
			btnClear.TabIndex = 124;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbSalesman.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSalesman.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSalesman.EnterColor = System.Drawing.Color.LightPink;
			cmbSalesman.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSalesman.FormattingEnabled = true;
			cmbSalesman.LeaveColor = System.Drawing.Color.White;
			cmbSalesman.Location = new System.Drawing.Point(145, 57);
			cmbSalesman.Name = "cmbSalesman";
			cmbSalesman.Size = new System.Drawing.Size(225, 24);
			cmbSalesman.TabIndex = 3;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(72, 59);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(31, 16);
			label4.TabIndex = 120;
			label4.Text = "To:";
			dtmTo.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(109, 55);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(206, 23);
			dtmTo.TabIndex = 121;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(21, 38);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(82, 16);
			label1.TabIndex = 118;
			label1.Text = "Date From:";
			pnlProduct.BackColor = System.Drawing.Color.White;
			pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlProduct.Controls.Add(label6);
			pnlProduct.Controls.Add(label5);
			pnlProduct.Controls.Add(label3);
			pnlProduct.Controls.Add(label2);
			pnlProduct.Controls.Add(cmbRoute);
			pnlProduct.Controls.Add(cmbBranch);
			pnlProduct.Controls.Add(lblBranch);
			pnlProduct.Controls.Add(cmbSalesman);
			pnlProduct.Controls.Add(cmbParty);
			pnlProduct.Controls.Add(cbNetTotal);
			pnlProduct.Controls.Add(cbDayTotal);
			pnlProduct.Controls.Add(label7);
			pnlProduct.Controls.Add(rdbBrand);
			pnlProduct.Controls.Add(cmbBrand);
			pnlProduct.Controls.Add(cmbGroup);
			pnlProduct.Controls.Add(cmbProduct);
			pnlProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlProduct.Location = new System.Drawing.Point(28, 192);
			pnlProduct.Name = "pnlProduct";
			pnlProduct.Size = new System.Drawing.Size(402, 233);
			pnlProduct.TabIndex = 115;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(22, 118);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(113, 16);
			label6.TabIndex = 196;
			label6.Text = "Product Group :";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(81, 90);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(54, 16);
			label5.TabIndex = 196;
			label5.Text = "Party :";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(49, 62);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(86, 16);
			label3.TabIndex = 196;
			label3.Text = "Sales Man :";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(78, 34);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 16);
			label2.TabIndex = 196;
			label2.Text = "Route :";
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Location = new System.Drawing.Point(145, 30);
			cmbRoute.Name = "cmbRoute";
			cmbRoute.Size = new System.Drawing.Size(225, 24);
			cmbRoute.TabIndex = 1;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(145, 193);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(225, 24);
			cmbBranch.TabIndex = 195;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(71, 194);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 194;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			cmbParty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbParty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbParty.EnterColor = System.Drawing.Color.LightPink;
			cmbParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbParty.FormattingEnabled = true;
			cmbParty.LeaveColor = System.Drawing.Color.White;
			cmbParty.Location = new System.Drawing.Point(145, 85);
			cmbParty.Name = "cmbParty";
			cmbParty.Size = new System.Drawing.Size(225, 24);
			cmbParty.TabIndex = 10;
			cbNetTotal.AutoSize = true;
			cbNetTotal.Checked = true;
			cbNetTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbNetTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbNetTotal.Location = new System.Drawing.Point(198, 8);
			cbNetTotal.Name = "cbNetTotal";
			cbNetTotal.Size = new System.Drawing.Size(130, 20);
			cbNetTotal.TabIndex = 133;
			cbNetTotal.Text = "Show Net Total";
			cbNetTotal.UseVisualStyleBackColor = true;
			cbDayTotal.AutoSize = true;
			cbDayTotal.Checked = true;
			cbDayTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbDayTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDayTotal.Location = new System.Drawing.Point(60, 8);
			cbDayTotal.Name = "cbDayTotal";
			cbDayTotal.Size = new System.Drawing.Size(132, 20);
			cbDayTotal.TabIndex = 134;
			cbDayTotal.Text = "Show Day Total";
			cbDayTotal.UseVisualStyleBackColor = true;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(24, 174);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(111, 16);
			label7.TabIndex = 2;
			label7.Text = "Product Name :";
			rdbBrand.AutoSize = true;
			rdbBrand.Location = new System.Drawing.Point(79, 146);
			rdbBrand.Name = "rdbBrand";
			rdbBrand.Size = new System.Drawing.Size(56, 16);
			rdbBrand.TabIndex = 2;
			rdbBrand.Text = "Brand :";
			cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBrand.FormattingEnabled = true;
			cmbBrand.LeaveColor = System.Drawing.Color.White;
			cmbBrand.Location = new System.Drawing.Point(145, 141);
			cmbBrand.Name = "cmbBrand";
			cmbBrand.Size = new System.Drawing.Size(225, 24);
			cmbBrand.TabIndex = 4;
			cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGroup.FormattingEnabled = true;
			cmbGroup.LeaveColor = System.Drawing.Color.White;
			cmbGroup.Location = new System.Drawing.Point(145, 113);
			cmbGroup.Name = "cmbGroup";
			cmbGroup.Size = new System.Drawing.Size(225, 24);
			cmbGroup.TabIndex = 3;
			cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProduct.EnterColor = System.Drawing.Color.LightPink;
			cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProduct.FormattingEnabled = true;
			cmbProduct.LeaveColor = System.Drawing.Color.White;
			cmbProduct.Location = new System.Drawing.Point(145, 169);
			cmbProduct.Name = "cmbProduct";
			cmbProduct.Size = new System.Drawing.Size(225, 24);
			cmbProduct.TabIndex = 6;
			dtmFrom.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(109, 31);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(206, 23);
			dtmFrom.TabIndex = 119;
			rdbEntryDate.AutoSize = true;
			rdbEntryDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbEntryDate.Location = new System.Drawing.Point(218, 5);
			rdbEntryDate.Name = "rdbEntryDate";
			rdbEntryDate.Size = new System.Drawing.Size(97, 20);
			rdbEntryDate.TabIndex = 131;
			rdbEntryDate.Text = "Entry Date";
			rdbEntryDate.UseVisualStyleBackColor = true;
			rdbVchrDate.AutoSize = true;
			rdbVchrDate.Checked = true;
			rdbVchrDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbVchrDate.Location = new System.Drawing.Point(42, 5);
			rdbVchrDate.Name = "rdbVchrDate";
			rdbVchrDate.Size = new System.Drawing.Size(116, 20);
			rdbVchrDate.TabIndex = 130;
			rdbVchrDate.TabStop = true;
			rdbVchrDate.Text = "Voucher Date";
			rdbVchrDate.UseVisualStyleBackColor = true;
			cbDate.AutoSize = true;
			cbDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDate.Location = new System.Drawing.Point(103, 84);
			cbDate.Name = "cbDate";
			cbDate.Size = new System.Drawing.Size(179, 20);
			cbDate.TabIndex = 132;
			cbDate.Text = "Date For Every Invoice";
			cbDate.UseVisualStyleBackColor = true;
			pnlRoute.BackColor = System.Drawing.Color.White;
			pnlRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlRoute.Controls.Add(dtmFrom);
			pnlRoute.Controls.Add(label1);
			pnlRoute.Controls.Add(dtmTo);
			pnlRoute.Controls.Add(cbDate);
			pnlRoute.Controls.Add(label4);
			pnlRoute.Controls.Add(rdbEntryDate);
			pnlRoute.Controls.Add(rdbVchrDate);
			pnlRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlRoute.Location = new System.Drawing.Point(30, 75);
			pnlRoute.Name = "pnlRoute";
			pnlRoute.Size = new System.Drawing.Size(400, 109);
			pnlRoute.TabIndex = 114;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(455, 475);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(pnlProduct);
			base.Controls.Add(pnlRoute);
			base.Name = "SalesReportView";
			base.Load += new System.EventHandler(SalesReportView_Load);
			base.Controls.SetChildIndex(pnlRoute, 0);
			base.Controls.SetChildIndex(pnlProduct, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlProduct.ResumeLayout(false);
			pnlProduct.PerformLayout();
			pnlRoute.ResumeLayout(false);
			pnlRoute.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
