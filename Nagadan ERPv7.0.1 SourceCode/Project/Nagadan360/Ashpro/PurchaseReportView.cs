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
	public class PurchaseReportView : BaseForm
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

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private Label label4;

		private AshDateTimePicker dtmTo;

		private Label label1;

		private Panel pnlProduct;

		private AshTextBoxNormal txtProCode;

		private Label rdbBrand;

		private AshComboBox cmbBrand;

		private AshComboBox cmbGroup;

		private RadioButton rdbProCodeWise;

		private AshComboBox cmbProduct;

		private RadioButton rdbProNameWise;

		private RadioButton rdbGroupWise;

		private RadioButton rdbAll;

		private AshDateTimePicker dtmFrom;

		private AshComboBox cmbAcctWise;

		private RadioButton rdbPartyAcc;

		private Panel pnlMultiple;

		private RadioButton rdbEntryDate;

		private RadioButton rdbVchrDate;

		private CheckBox cbDate;

		private CheckBox cbNetTotal;

		private CheckBox cbDayTotal;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public PurchaseReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void QueryMethod(string strQueryDate, string strQueryProduct)
		{
			if (cmbAcctWise.Text != string.Empty)
			{
				strQry = "SELECT PurInvId,PurInvNo,PartyAccId,PurDate,Frieght,AdditionalAmnt,TotalAmnt,BillDisc,TotalDiscount,TotalTax FROM PurInvMaster where  PartyAccId = '" + cmbAcctWise.SelectedValue.ToInt32() + "' " + sBranchDetails + " and  " + strQueryDate + "  between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + strQueryProduct + " order by PurDate,PurInvNo";
			}
			else
			{
				strQry = "SELECT PurInvId,PurInvNo,PartyAccId,PurDate,Frieght,AdditionalAmnt,TotalAmnt,BillDisc,TotalDiscount,TotalTax FROM PurInvMaster where  " + strQueryDate + "  between '" + AniHelper.GetDate(dtmFrom.Value) + "' " + sBranchDetails + " and '" + AniHelper.GetDate(dtmTo.Value) + "' " + strQueryProduct + " order by PurDate,PurInvNo";
			}
		}

		private void DGVFillMethod(string strQuery)
		{
			string text = string.Empty;
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			DateTime now = DateTime.Now;
			string strID = string.Empty;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				if (row[3].ToString() != text && num2 != 0m)
				{
					if (cbDayTotal.Checked)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num2);
					}
					num2 = default(decimal);
				}
				now = row[3].ToString().toDateTime();
				text = now.ToString();
				if (!cbDate.Checked)
				{
				}
				Details.Rows.Add("Inv Date - " + now.ToString("dd-MMM-yyyy"), " ", " ", " ", " ", " ", " ", " ", " ", " ");
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row["PartyAccId"].ToString(), out strID);
				Details.Rows.Add("Inv No- " + row[1].ToString() + "  [" + strID + "]", " ", " ", " ", " ", " ", " ", " ", " ", " ");
				ProductAddMethod(row["PurInvId"].ToString());
				if (rdbAll.Checked)
				{
					if (row[8].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Dis Amnt", "", row[8]);
					}
					if (row[4].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Frieght", "", row[4]);
					}
					if (row[5].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Add Amnt", "", row[5]);
					}
					if (row[7].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Add Disc", "", row[7]);
					}
					if (row[9].ToString().ToDecimal() > 0m)
					{
						Details.Rows.Add("", "", "", "", "", "", "", "Tax Amnt", "", row[9]);
					}
					Details.Rows.Add("", "", "", "", "", "", "", "Total", "", row[6]);
					num += Convert.ToDecimal(row[6].ToString());
					num2 += Convert.ToDecimal(row[6].ToString());
				}
				else
				{
					Details.Rows.Add("", "", "", "", "", "", "", "Total", "", dmlTotalBalance.ToString(CommonSettings.strCurencyFormat));
					num += dmlTotalBalance;
				}
			}
			if (cbDayTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Day Total", "", num2);
			}
			if (cbNetTotal.Checked)
			{
				Details.Rows.Add("", "", "", "", "", "", "", "Net Total", "", num);
			}
		}

		private void ProductAddMethod(string strPurInvId)
		{
			dmlTotalBalance = default(decimal);
			if (rdbAll.Checked)
			{
				strQry = "SELECT ProId,ProQty,UnitId,ProRate,Gross,Disc,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM PurInvSettings where PurInvId='" + strPurInvId + "'";
			}
			else if (rdbGroupWise.Checked)
			{
				if (cmbBrand.Text == "")
				{
					if (cmbGroup.Text != "")
					{
						strQry = "SELECT PurInvSettings.ProId,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvSettings.NetAmnt FROM PurInvSettings Join Product on PurInvSettings.ProId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and PurInvSettings.PurInvId='" + strPurInvId + "'";
					}
					else
					{
						strQry = "SELECT ProId,ProQty,UnitId,ProRate,Gross,Disc,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM PurInvSettings where PurInvId='" + strPurInvId + "'";
					}
				}
				else
				{
					strQry = "SELECT PurInvSettings.ProId,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvSettings.NetAmnt FROM PurInvSettings Join Product on PurInvSettings.ProId= Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName = '" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' and PurInvSettings.PurInvId='" + strPurInvId + "'";
				}
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					strQry = "SELECT PurInvSettings.ProId,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvSettings.NetAmnt FROM PurInvSettings Join Product on PurInvSettings.ProId= Product.ProductId where Product.ProductName = '" + cmbProduct.Text + "' and PurInvSettings.PurInvId='" + strPurInvId + "'";
				}
				else
				{
					strQry = "SELECT ProId,ProQty,UnitId,ProRate,Gross,Disc,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM PurInvSettings where PurInvId='" + strPurInvId + "'";
				}
			}
			else if (txtProCode.Text != "")
			{
				strQry = "SELECT PurInvSettings.ProId,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvSettings.NetAmnt FROM PurInvSettings Join Product on PurInvSettings.ProId= Product.ProductId where Product.ProductCode = '" + txtProCode.Text + "' and PurInvSettings.PurInvId='" + strPurInvId + "'";
			}
			else
			{
				strQry = "SELECT ProId,ProQty,UnitId,ProRate,Gross,Disc,DiscAmnt,GST,GSTAMOUNT,NetAmnt FROM PurInvSettings where PurInvId='" + strPurInvId + "'";
			}
			string strID = string.Empty;
			string strID2 = string.Empty;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQry);
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.IdFindMethod("Product", "ProductName", "ProductId", row["ProId"].ToString(), out strID);
				AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row["UnitId"].ToString(), out strID2);
				Details.Rows.Add(strID, row[1], strID2, row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
				if (!rdbAll.Checked)
				{
					if (row[9] != DBNull.Value)
					{
						dmlTotalBalance += Convert.ToDecimal(row[9].ToString());
					}
					else
					{
						dmlTotalBalance += 0m;
					}
				}
			}
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
						QueryMethod(strMethodQuery, "and PurInvId in ( SELECT PurInvId FROM PurInvSettings Join Product on PurInvSettings.ProId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId where ProductGroup.PGroupName='" + cmbGroup.Text + "' )");
					}
					else
					{
						QueryMethod(strMethodQuery, "");
					}
					return;
				}
				QueryMethod(strMethodQuery, "and PurInvId in ( SELECT PurInvId FROM PurInvSettings Join Product on PurInvSettings.ProId=Product.ProductId Join ProductGroup on Product.PGroupId= ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId where ProductGroup.PGroupName='" + cmbGroup.Text + "' and Brand.BrandName='" + cmbBrand.Text + "' )");
			}
			else if (rdbProNameWise.Checked)
			{
				if (cmbProduct.Text != "")
				{
					QueryMethod(strMethodQuery, "and PurInvId in ( SELECT PurInvId FROM PurInvSettings Join Product on PurInvSettings.ProId=Product.ProductId where Product.ProductName='" + cmbProduct.Text + "' )");
				}
				else
				{
					QueryMethod(strMethodQuery, "");
				}
			}
			else if (txtProCode.Text != "")
			{
				QueryMethod(strMethodQuery, "and PurInvId in ( SELECT PurInvId FROM PurInvSettings Join Product on PurInvSettings.ProId=Product.ProductId where Product.ProductCode='" + txtProCode.Text + "' )");
			}
			else
			{
				QueryMethod(strMethodQuery, "");
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
				MethodOfQuery("PurDate");
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

		private void PurchaseReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbAcctWise);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			AniHelper.PanelClearComboMethod(pnlProduct);
			AniHelper.PanelClearComboMethod(pnlMultiple);
			rdbAll.Checked = true;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.PanelClearComboMethod(pnlProduct);
			AniHelper.PanelClearComboMethod(pnlMultiple);
			rdbAll.Checked = true;
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (dtmFrom.Text != dtmTo.Text)
			{
				sTitle = "Purchase Report From " + dtmFrom.Text + " To " + dtmTo.Text;
			}
			else
			{
				sTitle = "Purchase Report of the Date - " + dtmFrom.Text;
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

		private void cmbAcctWise_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbPartyAcc.Checked = true;
		}

		private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			rdbGroupWise.Checked = true;
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
			label4 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			pnlProduct = new System.Windows.Forms.Panel();
			txtProCode = new AshUsercontrol.AshTextBoxNormal();
			rdbBrand = new System.Windows.Forms.Label();
			cmbBrand = new AshUsercontrol.AshComboBox();
			cmbGroup = new AshUsercontrol.AshComboBox();
			rdbProCodeWise = new System.Windows.Forms.RadioButton();
			cmbProduct = new AshUsercontrol.AshComboBox();
			rdbProNameWise = new System.Windows.Forms.RadioButton();
			rdbGroupWise = new System.Windows.Forms.RadioButton();
			rdbAll = new System.Windows.Forms.RadioButton();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			cmbAcctWise = new AshUsercontrol.AshComboBox();
			rdbPartyAcc = new System.Windows.Forms.RadioButton();
			pnlMultiple = new System.Windows.Forms.Panel();
			rdbEntryDate = new System.Windows.Forms.RadioButton();
			rdbVchrDate = new System.Windows.Forms.RadioButton();
			cbDate = new System.Windows.Forms.CheckBox();
			cbNetTotal = new System.Windows.Forms.CheckBox();
			cbDayTotal = new System.Windows.Forms.CheckBox();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			pnlProduct.SuspendLayout();
			pnlMultiple.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(399, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(166, 24);
			lblTitle.TabIndex = 127;
			lblTitle.Text = "Purchase Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(278, 388);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
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
			btnClear.Location = new System.Drawing.Point(355, 388);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 124;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(244, 86);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(28, 16);
			label4.TabIndex = 120;
			label4.Text = "To:";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(276, 80);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 22);
			dtmTo.TabIndex = 121;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(23, 87);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(74, 16);
			label1.TabIndex = 118;
			label1.Text = "Date From:";
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
			pnlProduct.Location = new System.Drawing.Point(42, 112);
			pnlProduct.Name = "pnlProduct";
			pnlProduct.Size = new System.Drawing.Size(348, 138);
			pnlProduct.TabIndex = 115;
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
			txtProCode.Size = new System.Drawing.Size(187, 15);
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
			cmbBrand.Size = new System.Drawing.Size(187, 24);
			cmbBrand.TabIndex = 4;
			cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGroup.FormattingEnabled = true;
			cmbGroup.LeaveColor = System.Drawing.Color.White;
			cmbGroup.Location = new System.Drawing.Point(143, 30);
			cmbGroup.Name = "cmbGroup";
			cmbGroup.Size = new System.Drawing.Size(187, 24);
			cmbGroup.TabIndex = 3;
			cmbGroup.SelectedIndexChanged += new System.EventHandler(cmbGroup_SelectedIndexChanged);
			rdbProCodeWise.AutoSize = true;
			rdbProCodeWise.Location = new System.Drawing.Point(47, 115);
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
			cmbProduct.Size = new System.Drawing.Size(187, 24);
			cmbProduct.TabIndex = 6;
			rdbProNameWise.AutoSize = true;
			rdbProNameWise.Location = new System.Drawing.Point(29, 88);
			rdbProNameWise.Name = "rdbProNameWise";
			rdbProNameWise.Size = new System.Drawing.Size(102, 20);
			rdbProNameWise.TabIndex = 5;
			rdbProNameWise.Text = "Productwise:";
			rdbProNameWise.UseVisualStyleBackColor = true;
			rdbGroupWise.AutoSize = true;
			rdbGroupWise.Location = new System.Drawing.Point(44, 32);
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
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(102, 81);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 22);
			dtmFrom.TabIndex = 119;
			cmbAcctWise.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAcctWise.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAcctWise.EnterColor = System.Drawing.Color.LightPink;
			cmbAcctWise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAcctWise.FormattingEnabled = true;
			cmbAcctWise.LeaveColor = System.Drawing.Color.White;
			cmbAcctWise.Location = new System.Drawing.Point(164, 8);
			cmbAcctWise.Name = "cmbAcctWise";
			cmbAcctWise.Size = new System.Drawing.Size(168, 24);
			cmbAcctWise.TabIndex = 3;
			cmbAcctWise.SelectedIndexChanged += new System.EventHandler(cmbAcctWise_SelectedIndexChanged);
			rdbPartyAcc.AutoSize = true;
			rdbPartyAcc.Location = new System.Drawing.Point(0, 8);
			rdbPartyAcc.Name = "rdbPartyAcc";
			rdbPartyAcc.Size = new System.Drawing.Size(145, 20);
			rdbPartyAcc.TabIndex = 2;
			rdbPartyAcc.Text = "Party Account Wise:";
			rdbPartyAcc.UseVisualStyleBackColor = true;
			pnlMultiple.BackColor = System.Drawing.Color.White;
			pnlMultiple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pnlMultiple.Controls.Add(cmbAcctWise);
			pnlMultiple.Controls.Add(rdbPartyAcc);
			pnlMultiple.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			pnlMultiple.Location = new System.Drawing.Point(40, 267);
			pnlMultiple.Name = "pnlMultiple";
			pnlMultiple.Size = new System.Drawing.Size(350, 49);
			pnlMultiple.TabIndex = 113;
			rdbEntryDate.AutoSize = true;
			rdbEntryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbEntryDate.Location = new System.Drawing.Point(237, 55);
			rdbEntryDate.Name = "rdbEntryDate";
			rdbEntryDate.Size = new System.Drawing.Size(88, 20);
			rdbEntryDate.TabIndex = 129;
			rdbEntryDate.Text = "Entry Date";
			rdbEntryDate.UseVisualStyleBackColor = true;
			rdbVchrDate.AutoSize = true;
			rdbVchrDate.Checked = true;
			rdbVchrDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbVchrDate.Location = new System.Drawing.Point(61, 55);
			rdbVchrDate.Name = "rdbVchrDate";
			rdbVchrDate.Size = new System.Drawing.Size(108, 20);
			rdbVchrDate.TabIndex = 128;
			rdbVchrDate.TabStop = true;
			rdbVchrDate.Text = "Voucher Date";
			rdbVchrDate.UseVisualStyleBackColor = true;
			cbDate.AutoSize = true;
			cbDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDate.Location = new System.Drawing.Point(217, 327);
			cbDate.Name = "cbDate";
			cbDate.Size = new System.Drawing.Size(163, 20);
			cbDate.TabIndex = 130;
			cbDate.Text = "Date For Every Invoice";
			cbDate.UseVisualStyleBackColor = true;
			cbNetTotal.AutoSize = true;
			cbNetTotal.Checked = true;
			cbNetTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbNetTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbNetTotal.Location = new System.Drawing.Point(129, 327);
			cbNetTotal.Name = "cbNetTotal";
			cbNetTotal.Size = new System.Drawing.Size(82, 20);
			cbNetTotal.TabIndex = 131;
			cbNetTotal.Text = "Net Total";
			cbNetTotal.UseVisualStyleBackColor = true;
			cbDayTotal.AutoSize = true;
			cbDayTotal.Checked = true;
			cbDayTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			cbDayTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbDayTotal.Location = new System.Drawing.Point(32, 327);
			cbDayTotal.Name = "cbDayTotal";
			cbDayTotal.Size = new System.Drawing.Size(86, 20);
			cbDayTotal.TabIndex = 132;
			cbDayTotal.Text = "Day Total";
			cbDayTotal.UseVisualStyleBackColor = true;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(119, 353);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(257, 24);
			cmbBranch.TabIndex = 187;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(52, 353);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(56, 16);
			lblBranch.TabIndex = 186;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(434, 424);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(cbDate);
			base.Controls.Add(cbNetTotal);
			base.Controls.Add(cbDayTotal);
			base.Controls.Add(rdbEntryDate);
			base.Controls.Add(rdbVchrDate);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(label4);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label1);
			base.Controls.Add(pnlProduct);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(pnlMultiple);
			base.Name = "PurchaseReportView";
			base.Load += new System.EventHandler(PurchaseReportView_Load);
			base.Controls.SetChildIndex(pnlMultiple, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(pnlProduct, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(rdbVchrDate, 0);
			base.Controls.SetChildIndex(rdbEntryDate, 0);
			base.Controls.SetChildIndex(cbDayTotal, 0);
			base.Controls.SetChildIndex(cbNetTotal, 0);
			base.Controls.SetChildIndex(cbDate, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			pnlProduct.ResumeLayout(false);
			pnlProduct.PerformLayout();
			pnlMultiple.ResumeLayout(false);
			pnlMultiple.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
