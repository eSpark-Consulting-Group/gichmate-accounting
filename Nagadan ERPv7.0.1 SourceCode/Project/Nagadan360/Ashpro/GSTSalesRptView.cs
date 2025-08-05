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
	public class GSTSalesRptView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private string strFormName;

		private string Query;

		private string sTitle;

		private string sBranchDetails = "";

		private string checkedForms = null;

		private IContainer components = null;

		private Button btnShow;

		private Button btnClear;

		private AshDateTimePicker dtmTo;

		private Label lblTo;

		private AshDateTimePicker dtmFrom;

		private Label lblFrom;

		private Label lblTitle;

		private CheckedListBox clForms;

		private Label label1;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public DashboardPOS form { get; set; }

		public GSTSalesRptView(string sFName)
		{
			strFormName = sFName;
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private decimal MRPAmntFindMethod(string strSalesInvId)
		{
			decimal result = default(decimal);
			decimal num = default(decimal);
			try
			{
				if (strFormName == "Sales")
				{
					Query = "Select SalesEstimateSettings.ProQty,Product.MRP From SalesEstimateSettings Join Product on SalesEstimateSettings.ProductId=Product.ProductId Where SalesEstimateSettings.SalesInvId='" + strSalesInvId + "'";
				}
				else if (strFormName == "Purchase")
				{
					Query = "Select PurInvSettings.ProQty,Product.MRP From PurInvSettings Join Product on PurInvSettings.ProId=Product.ProductId Where PurInvSettings.PurInvId='" + strSalesInvId + "'";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num = Convert.ToDecimal(row[0].ToString()) * Convert.ToDecimal(row[1].ToString());
					result += num;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 464");
			}
			return result;
		}

		private string ColumnsAddMethod(string strSalesVat)
		{
			string result = "";
			try
			{
				if (strFormName == "Sales")
				{
					Query = "Select TCategoryName from TaxCategory where SalesVat='" + strSalesVat + "' ";
				}
				else if (strFormName == "Purchase")
				{
					Query = "Select TCategoryName from TaxCategory where PurVat='" + strSalesVat + "' ";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					result = row[0].ToString();
					Details.Columns.Add(row[0].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 1396");
			}
			return result;
		}

		private void AddGSTAmntMethod(string strId, string Colm, decimal dmlAmt)
		{
			int num = 0;
			try
			{
				if (strFormName == "Sales")
				{
					Query = "Select SalesEstimateMaster.SalesInvId From SalesEstimateMaster where SalesDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + sBranchDetails + " and InvType in (" + checkedForms + ") order by SalesDate,SalesInvNo";
				}
				else if (strFormName == "Purchase")
				{
					Query = "Select PurInvMaster.PurInvId From PurInvMaster where PurDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + sBranchDetails + " and InvType in (" + checkedForms + ") order by PurDate,PurInvNo";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					GSTAmntFindMethod(num, strId, Colm, row[0].ToString());
					num++;
				}
				Details.Rows[num][Colm] = "";
				Details.Rows[num + 1][Colm] = dmlAmt;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 608");
			}
		}

		private void GSTAmntFindMethod(int i, string strId, string Colom, string strSalesInvId)
		{
			try
			{
				if (strFormName == "Sales")
				{
					Query = "Select SUM(GSTAMOUNT) as TaxAmount From SalesEstimateSettings where SalesEstimateSettings.GST='" + strId + "' and SalesEstimateSettings.SalesInvId = '" + strSalesInvId + "'";
				}
				else if (strFormName == "Purchase")
				{
					Query = "Select SUM(GSTAMOUNT) as TaxAmount From PurInvSettings where PurInvSettings.GST='" + strId + "' and PurInvSettings.PurInvId = '" + strSalesInvId + "'";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows[i][Colom] = row[0].ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 661");
			}
		}

		private void TaxDetailsAddMethod()
		{
			string text = null;
			decimal num = default(decimal);
			try
			{
				if (strFormName == "Sales")
				{
					Query = "Select distinct(SalesEstimateSettings.GST) From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateMaster.SalesDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' and SalesEstimateMaster.InvType in (" + checkedForms + ")";
				}
				else if (strFormName == "Purchase")
				{
					Query = "Select distinct(PurInvSettings.GST) From PurInvSettings Join PurInvMaster on PurInvSettings.PurInvId=PurInvMaster.PurInvId where PurInvMaster.PurDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' and PurInvMaster.InvType in (" + checkedForms + ")";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num = GSTDetailsAddMethod(row[0].ToString());
					if (num > 0m)
					{
						text = ColumnsAddMethod(row[0].ToString());
						AddGSTAmntMethod(row[0].ToString(), text, num);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 553");
			}
		}

		private decimal GSTDetailsAddMethod(string strTaxId)
		{
			string text = string.Empty;
			decimal? num = 0m;
			try
			{
				if (strFormName == "Sales")
				{
					if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
					{
						text = " and SalesEstimateMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
					}
					Query = "Select SUM(GSTAMOUNT) as TaxAmount From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.GST='" + strTaxId + "' and SalesEstimateMaster.SalesDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + " and SalesEstimateMaster.InvType in (" + checkedForms + ")";
				}
				else if (strFormName == "Purchase")
				{
					if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
					{
						text = " and PurInvMaster.FK_BranchId='" + Settings.Default.iBranchId + "'";
					}
					Query = "Select SUM(GSTAMOUNT) as TaxAmount From PurInvSettings Join PurInvMaster on PurInvSettings.PurInvId=PurInvMaster.PurInvId where PurInvSettings.GST='" + strTaxId + "' and PurInvMaster.PurDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + " and PurInvMaster.InvType in (" + checkedForms + ")";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num += (decimal?)row[0].ToString().ToDecimal();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 713");
			}
			return num.Value;
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string[] headers = new string[16]
			{
				"SL", "Vchr No", "Vchr Type", "Date", "Party Name", "Tax No", "Address", "Phone No", "TaxableAmount", "SGST",
				"CGST", "IGST", "Total GST", "Cess", "Net Total", "MRP total"
			};
			string[] headers2 = new string[15]
			{
				"SL", "Vchr No", "Vchr Type", "Date", "Party Name", "Tax No", "Address", "Phone No", "TaxableAmount", "SGST",
				"CGST", "IGST", "GST Amount", "Net Total", "MRP total"
			};
			string[] headers3 = new string[12]
			{
				"SL", "Vchr No", "Vchr Type", "Date", "Party Name", "Tax No", "Address", "Phone No", "TaxableAmount", "Vat Amount",
				"Net Total", "MRP total"
			};
			string[] headers4 = new string[13]
			{
				"SL", "Vchr No", "Vchr Type", "Date", "Party Name", "Tax No", "Address", "Phone No", "TaxableAmount", "Vat Amount",
				"Cess", "Net Total", "MRP total"
			};
			Gridview = AniHelper.GetGridviewDatatable();
			if (CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
			{
				Details = AniHelper.GetDataTable(headers);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(110, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			else if (CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
			{
				Details = AniHelper.GetDataTable(headers2);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(110, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			else if (!CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
			{
				Details = AniHelper.GetDataTable(headers3);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(110, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			else if (!CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
			{
				Details = AniHelper.GetDataTable(headers4);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(110, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			if (clForms.CheckedItems.Count > 0)
			{
				foreach (string checkedItem in clForms.CheckedItems)
				{
					checkedForms = checkedForms + "'" + checkedItem + "',";
				}
			}
			else
			{
				foreach (MasterValue item in CommonSettings.GetFormType())
				{
					checkedForms = checkedForms + "'" + item.Description + "',";
				}
			}
			checkedForms = checkedForms.Remove(checkedForms.Length - 1, 1);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			decimal num8 = default(decimal);
			decimal num9 = default(decimal);
			decimal num10 = default(decimal);
			decimal num11 = default(decimal);
			int num12 = 1;
			string empty = string.Empty;
			try
			{
				Query = "Select SalesEstimateMaster.SalesInvNo,SalesEstimateMaster.InvType,Convert(nvarchar,SalesEstimateMaster.SalesDate,103) as Date,Account.AccountName,Account.TinNo,Account.AddressOne,Account.AddressTwo,Account.MobileNo,SalesEstimateMaster.GrandDiscount,SalesEstimateMaster.SGSTAmount,SalesEstimateMaster.CGSTAmount,SalesEstimateMaster.IGSTAmount,SalesEstimateMaster.FloodCess,SalesEstimateMaster.SalesInvId,SalesEstimateMaster.TotalTaxAmount From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId = Account.AccountId where SalesDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + sBranchDetails + " and InvType in (" + checkedForms + ") order by SalesDate,SalesInvNo";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					num10 = MRPAmntFindMethod(row[13].ToString());
					num9 = row[14].ToString().ToDecimal();
					num11 = row[8].ToString().ToDecimal() - num9;
					empty = ((!(row[5].ToString() != string.Empty) || !(row[6].ToString() != string.Empty)) ? (row[5].ToString() + row[6].ToString()) : (row[5].ToString() + ", " + row[6].ToString()));
					if (CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
					{
						Details.Rows.Add(num12, row[0], row[1], row[2], row[3], row[4], empty, row[7], num11.ToString(CommonSettings.strCurencyFormat), row[9].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[10].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[11].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num9.ToString(CommonSettings.strCurencyFormat), row[12].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[8].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num10.ToString(CommonSettings.strCurencyFormat));
					}
					else if (CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
					{
						Details.Rows.Add(num12, row[0], row[1], row[2], row[3], row[4], empty, row[7], num11.ToString(CommonSettings.strCurencyFormat), row[9].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[10].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[11].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num9.ToString(CommonSettings.strCurencyFormat), row[8].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num10.ToString(CommonSettings.strCurencyFormat));
					}
					else if (!CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
					{
						Details.Rows.Add(num12, row[0], row[1], row[2], row[3], row[4], empty, row[7], num11.ToString(CommonSettings.strCurencyFormat), num9.ToString(CommonSettings.strCurencyFormat), row[8].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num10.ToString(CommonSettings.strCurencyFormat));
					}
					else if (!CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
					{
						Details.Rows.Add(num12, row[0], row[1], row[2], row[3], row[4], empty, row[7], num11.ToString(CommonSettings.strCurencyFormat), num9.ToString(CommonSettings.strCurencyFormat), row[12].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), row[8].ToString().ToDecimal().ToString(CommonSettings.strCurencyFormat), num10.ToString(CommonSettings.strCurencyFormat));
					}
					num += num11;
					num2 += row[8].ToString().ToDecimal();
					num3 += num10;
					num4 += num9;
					if (CommonSettings.blnGSTEnabled)
					{
						num5 += row[9].ToString().ToDecimal();
						num6 += row[10].ToString().ToDecimal();
						num7 += row[11].ToString().ToDecimal();
					}
					if (CommonSettings.blnCessEnabled)
					{
						num8 += row[12].ToString().ToDecimal();
					}
					num12++;
				}
				if (CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
					Details.Rows.Add("", "", "", "", "Total ", "", "", "", num.ToString("F"), num5.ToString("F"), num6.ToString("F"), num7.ToString("F"), num4.ToString("F"), num8.ToString("F"), num2.ToString("F"), num3.ToString("F"));
				}
				else if (CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
					Details.Rows.Add("", "", "", "", "Total ", "", "", "", num.ToString("F"), num5.ToString("F"), num6.ToString("F"), num7.ToString("F"), num4.ToString("F"), num2.ToString("F"), num3.ToString("F"));
				}
				else if (!CommonSettings.blnGSTEnabled && !CommonSettings.blnCessEnabled)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "");
					Details.Rows.Add("", "", "", "", "Total ", "", "", "", num.ToString("F"), num4.ToString("F"), num2.ToString("F"), num3.ToString("F"));
				}
				else if (!CommonSettings.blnGSTEnabled && CommonSettings.blnCessEnabled)
				{
					Details.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "");
					Details.Rows.Add("", "", "", "", "Total ", "", "", "", num.ToString("F"), num4.ToString("F"), num8.ToString("F"), num2.ToString("F"), num3.ToString("F"));
				}
				TaxDetailsAddMethod();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 411");
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			for (int i = 0; i < clForms.Items.Count; i++)
			{
				clForms.SetItemChecked(i, value: true);
			}
		}

		private void GSTSalesRptView_Load(object sender, EventArgs e)
		{
			AniHelper.FillFormType(clForms);
			AniHelper.FillBranch(cmbBranch);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			if (strFormName == "Purchase")
			{
				lblTitle.Text = "Tax Purchase Report";
			}
			for (int i = 0; i < clForms.Items.Count; i++)
			{
				clForms.SetItemChecked(i, value: true);
			}
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				if (CommonSettings.blnGSTEnabled)
				{
					sTitle = "GST Sales Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
				else
				{
					sTitle = "VAT Sales Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
				ReportGenerateMethod();
				ReportView reportView = new ReportView();
				reportView = new ReportView();
				reportView.MdiParent = base.MdiParent;
				reportView.Details = Details;
				reportView.Gridview = Gridview;
				reportView.Title = sTitle;
				reportView.blPrint = false;
				reportView.blMail = false;
				if (form == null)
				{
					reportView.Show();
				}
				else
				{
					form.OpenForm(reportView, 0, 0);
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GSTSalesRptView));
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.clForms = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.rdbSummary = new System.Windows.Forms.RadioButton();
            this.rdbDetails = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
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
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.Name = "lblFrom";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // clForms
            // 
            resources.ApplyResources(this.clForms, "clForms");
            this.clForms.CheckOnClick = true;
            this.clForms.FormattingEnabled = true;
            this.clForms.Name = "clForms";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // rdbSummary
            // 
            resources.ApplyResources(this.rdbSummary, "rdbSummary");
            this.rdbSummary.Name = "rdbSummary";
            this.rdbSummary.UseVisualStyleBackColor = true;
            // 
            // rdbDetails
            // 
            resources.ApplyResources(this.rdbDetails, "rdbDetails");
            this.rdbDetails.Checked = true;
            this.rdbDetails.Name = "rdbDetails";
            this.rdbDetails.TabStop = true;
            this.rdbDetails.UseVisualStyleBackColor = true;
            // 
            // GSTSalesRptView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdbSummary);
            this.Controls.Add(this.rdbDetails);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.clForms);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTitle);
            this.Name = "GSTSalesRptView";
            this.Load += new System.EventHandler(this.GSTSalesRptView_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblFrom, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.lblTo, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.clForms, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.rdbDetails, 0);
            this.Controls.SetChildIndex(this.rdbSummary, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
