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
	public class ProductWiseGSTReportView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private string sTitle;

		private string Query;

		private string sBranchDetails = "";

		private int intDGVCount;

		private string checkedForms;

		private IContainer components = null;

		private CheckedListBox clbForms;

		private AshDateTimePicker dtmFrom;

		private AshComboBox cmbReportType;

		private Label label8;

		private Label label5;

		private Label label6;

		private AshDateTimePicker dtmTo;

		private RadioButton rdbHSN;

		private RadioButton rdbProduct;

		private RadioButton rdbDate;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public ProductWiseGSTReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And SalesEstimateMaster.FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			Gridview = AniHelper.GetGridviewDatatable();
			string[] headers;
			if (cmbReportType.SelectedIndex != 0 && cmbReportType.SelectedIndex != 1 && cmbReportType.SelectedIndex != 2)
			{
				if (cmbReportType.SelectedIndex == 3)
				{
					headers = new string[9] { "SL", "Type", "Place of Supply", "Rate", "Taxable Value", "Output GST", "Invoice Amount", "Cess Amount", "E-COMMERCE GSTIN" };
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(100, "L");
					Gridview.Rows.Add(250, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(200, "L");
				}
				else if (cmbReportType.SelectedIndex == 4)
				{
					if (rdbDate.Checked)
					{
						headers = new string[9] { "SL", "DATE", "HSN CODE", "TAXABLE AMOUNT", "IGST AMNT", "CGST AMOUNT", "SGST AMOUNT", "CESS AMOUNT", "TOTAL AMOUNT" };
						Gridview.Rows.Add(50, "L");
						Gridview.Rows.Add(100, "L");
						Gridview.Rows.Add(100, "L");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
					}
					else if (rdbProduct.Checked)
					{
						headers = new string[11]
						{
							"SL", "HSN", "PRODUCT", "UQC", "TOTAL QTY", "TAXABLE VALUE", "IGST AMNT", "CGST AMOUNT", "SGST AMOUNT", "CESS AMOUNT",
							"TOTAL AMOUNT"
						};
						Gridview.Rows.Add(50, "L");
						Gridview.Rows.Add(100, "L");
						Gridview.Rows.Add(250, "L");
						Gridview.Rows.Add(100, "L");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
					}
					else
					{
						headers = new string[8] { "SL", "HSN", "TAXABLE VALUE", "IGST AMNT", "CGST AMOUNT", "SGST AMOUNT", "CESS AMOUNT", "TOTAL AMOUNT" };
						Gridview.Rows.Add(50, "L");
						Gridview.Rows.Add(100, "L");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
						Gridview.Rows.Add(120, "R");
					}
				}
				else if (cmbReportType.SelectedIndex == 5)
				{
					headers = new string[14]
					{
						"SL", "GSTIN OF Recipient", "Invoice/Advance Reciept No", "Invoice/Advance Reciept Date", "Note/Refund Voucher No", "Note/Refund Voucher Date", "Document Type", "Reason for Issue", "Place of Supply", "Note/Refund Voucher Value",
						"Rate", "Taxable Value", "Cess Amount", "Pre GST"
					};
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(250, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
				}
				else if (cmbReportType.SelectedIndex == 6)
				{
					headers = new string[14]
					{
						"SL", "UR Type", "Invoice/Advance Reciept No", "Invoice/Advance Reciept Date", "Note/Refund Voucher No", "Note/Refund Voucher Date", "Document Type", "Reason for Issue", "Place of Supply", "Note/Refund Voucher Value",
						"Rate", "Taxable Value", "Cess Amount", "Pre GST"
					};
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(250, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
				}
				else
				{
					headers = new string[12]
					{
						"SL", "GST No", "Inv No", "Date", "Inv Amnt", "Place of Supply", "Reverse Charge", "Invoice Type", "E-COMMERCE GSTIN", "GST Rate",
						"Taxable Value", "Cess Amount"
					};
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(250, "L");
					Gridview.Rows.Add(100, "L");
					Gridview.Rows.Add(100, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(250, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
				}
			}
			else
			{
				headers = new string[12]
				{
					"SL", "GST No", "Inv No", "Date", "Inv Amnt", "Place of Supply", "Reverse Charge", "Invoice Type", "E-COMMERCE GSTIN", "GST Rate",
					"Taxable Value", "Cess Amount"
				};
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			Details = AniHelper.GetDataTable(headers);
			if (clbForms.CheckedItems.Count > 0)
			{
				foreach (string checkedItem in clbForms.CheckedItems)
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
			intDGVCount = 1;
			if (cmbReportType.SelectedIndex == 0)
			{
				B2bMethod();
			}
			else
			{
				if (cmbReportType.SelectedIndex == 1 || cmbReportType.SelectedIndex == 2)
				{
					return;
				}
				if (cmbReportType.SelectedIndex == 3)
				{
					try
					{
						Query = "Select distinct(Account.CstNo) From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId = Account.AccountId where SalesDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' " + sBranchDetails + " and InvType in (" + checkedForms + ") and Account.TinNo = '" + DBNull.Value?.ToString() + "' AND Account.TinNo = ''";
						DataTable dataTable = new DataTable();
						dataTable = AshproCommon.GetDataTable(Query);
						foreach (DataRow row in dataTable.Rows)
						{
							GstRateTotalFindMethod(row[0].ToString());
						}
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						return;
					}
				}
				if (cmbReportType.SelectedIndex == 4)
				{
					if (rdbDate.Checked)
					{
						try
						{
							Query = "Select distinct(SalesEstimateMaster.SalesDate) From SalesEstimateMaster where SalesDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' " + sBranchDetails + " and InvType in (" + checkedForms + ")";
							DataTable dataTable2 = new DataTable();
							dataTable2 = AshproCommon.GetDataTable(Query);
							foreach (DataRow row2 in dataTable2.Rows)
							{
								string strDate = Convert.ToDateTime(row2[0].ToString()).ToString("dd-MMM-yyyy");
								HSNCodeFindMethod(strDate);
							}
							return;
						}
						catch (Exception ex2)
						{
							MessageBox.Show(ex2.Message);
							return;
						}
					}
					if (rdbProduct.Checked)
					{
						try
						{
							Query = "Select distinct SalesEstimateSettings.ProductId,SalesEstimateSettings.HSN From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ")";
							DataTable dataTable3 = new DataTable();
							dataTable3 = AshproCommon.GetDataTable(Query);
							foreach (DataRow row3 in dataTable3.Rows)
							{
								ProductWiseHSN(row3[0].ToString(), row3[1].ToString());
							}
							return;
						}
						catch (Exception ex3)
						{
							MessageBox.Show(ex3.Message);
							return;
						}
					}
					try
					{
						Query = "Select distinct SalesEstimateSettings.HSN From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ")";
						DataTable dataTable4 = new DataTable();
						dataTable4 = AshproCommon.GetDataTable(Query);
						foreach (DataRow row4 in dataTable4.Rows)
						{
							HSNCodeWiseTotalFindMethod(row4[0].ToString());
						}
						return;
					}
					catch (Exception ex4)
					{
						MessageBox.Show(ex4.Message);
						return;
					}
				}
				B2bMethod();
			}
		}

		private void B2bMethod()
		{
			try
			{
				Query = "Select SalesEstimateMaster.SalesInvId From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId = Account.AccountId where SalesDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and InvType in (" + checkedForms + ") and Account.TinNo <> '" + DBNull.Value?.ToString() + "' " + sBranchDetails + " AND Account.TinNo <> '' order by SalesDate,SalesInvNo";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					B2BDetailsFindMethod(row[0].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "634");
			}
		}

		private void B2BDetailsFindMethod(string strId)
		{
			try
			{
				Query = "Select Account.TinNo,Account.CstNo,SalesEstimateMaster.SalesInvNo,SalesEstimateMaster.SalesDate,SalesEstimateMaster.InvType From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId = Account.AccountId where SalesEstimateMaster.SalesInvId='" + strId + "' ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					GSTRateFindMethod(strId, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString().toDateTime().ToString("dd-MMM-yyyy"), row[4].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "817");
			}
		}

		private void HSNCodeWiseTotalFindMethod(string strHSNCode)
		{
			try
			{
				Query = "Select SUM(SalesEstimateSettings.NetAmnt) as TaxableAmount,SUM(SalesEstimateSettings.IGSTAMOUNT) as igstAmount,SUM(SalesEstimateSettings.CGSTAmount) as cgstAmount,SUM(SalesEstimateSettings.SGSTAmount) as sgstAmount,SUM(SalesEstimateSettings.NetAmnt+SalesEstimateSettings.GSTAMOUNT) as InvoiceTotal From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.HSN='" + strHSNCode + "' " + sBranchDetails + " AND SalesEstimateMaster.SalesDate between'" + dtmFrom.Text + "' and '" + dtmTo.Text + "' AND  SalesEstimateMaster.InvType in (" + checkedForms + ") ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows.Add(intDGVCount, strHSNCode, row[0].ToString(), row[1], row[2], row[3], "0.00", row[4]);
					intDGVCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "785");
			}
		}

		private void ProductWiseHSN(string ProId, string HSN)
		{
			try
			{
				Query = "Select distinct(SalesEstimateSettings.UnitId) From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.ProductId = '" + ProId + "' " + sBranchDetails + " and SalesEstimateMaster.SalesDate between  '" + dtmFrom.Text + "' AND '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ")";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					ProwiseHSNTotalFindMethod(ProId, HSN, row[0].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "760");
			}
		}

		private void ProwiseHSNTotalFindMethod(string strProId, string strHSN, string strUnit)
		{
			string strID = null;
			string strID2 = null;
			try
			{
				Query = "Select SUM(SalesEstimateSettings.ProQty) as TotalQty,SUM(SalesEstimateSettings.NetAmnt) as NetTotal,SUM(SalesEstimateSettings.IGSTAMOUNT) as igstAmount,SUM(SalesEstimateSettings.CGSTAmount) as cgstAmount,SUM(SalesEstimateSettings.SGSTAmount) as sgstAmount,SUM(SalesEstimateSettings.NetAmnt+SalesEstimateSettings.GSTAMOUNT) as InvoiceTotal From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.ProductId='" + strProId + "' " + sBranchDetails + " AND SalesEstimateSettings.UnitId='" + strUnit + "' AND SalesEstimateMaster.SalesDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ") ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Product", "ProductName", "ProductId", strProId, out strID);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", strUnit, out strID2);
					Details.Rows.Add(intDGVCount, strHSN, strID, strID2, row[0].ToString(), row[1], row[2], row[3], row[4], "0.00", row[5]);
					intDGVCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "790");
			}
		}

		private void HSNCodeFindMethod(string strDate)
		{
			try
			{
				Query = "Select distinct(SalesEstimateSettings.HSN) From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateMaster.SalesDate = '" + strDate + "' " + sBranchDetails + " and SalesEstimateMaster.InvType in (" + checkedForms + ")";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					HSNTotalFindMethod(row[0].ToString(), strDate);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void HSNTotalFindMethod(string strHSNCode, string strDate)
		{
			try
			{
				Query = "Select SUM(SalesEstimateSettings.NetAmnt) as TaxableAmount,SUM(SalesEstimateSettings.IGSTAMOUNT) as igstAmount,SUM(SalesEstimateSettings.CGSTAmount) as cgstAmount,SUM(SalesEstimateSettings.SGSTAmount) as sgstAmount,SUM(SalesEstimateSettings.NetAmnt+SalesEstimateSettings.GSTAMOUNT) as InvoiceTotal From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.HSN='" + strHSNCode + "' " + sBranchDetails + " AND SalesEstimateMaster.SalesDate='" + strDate + "' and SalesEstimateMaster.InvType in (" + checkedForms + ") ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows.Add(intDGVCount, strDate, strHSNCode, row[0].ToString(), row[1], row[2], row[3], "0.00", row[4]);
					intDGVCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "753");
			}
		}

		private void GstRateTotalFindMethod(string strCCode)
		{
			try
			{
				Query = "Select distinct(SalesEstimateSettings.GST) From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId Join Account on SalesEstimateSettings.PartyAccountId = Account.AccountId where Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' " + sBranchDetails + " and SalesEstimateMaster.InvType in (" + checkedForms + ") and Account.TinNo = '" + DBNull.Value?.ToString() + "' AND Account.TinNo = '' AND Account.CstNo='" + strCCode + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					TotalFindMethod(row[0].ToString(), strCCode);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void TotalFindMethod(string strGSTRate, string strStateCode)
		{
			try
			{
				Query = "Select SUM(SalesEstimateSettings.NetAmnt) as TaxableAmount,SUM(SalesEstimateSettings.GSTAMOUNT) as gstAmount,SUM(SalesEstimateSettings.NetAmnt+SalesEstimateSettings.GSTAMOUNT) as InvoiceTotal From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId Join Account on SalesEstimateSettings.PartyAccountId = Account.AccountId where SalesEstimateSettings.GST='" + strGSTRate + "' " + sBranchDetails + " AND Account.CstNo='" + strStateCode + "' AND Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ") AND Account.TinNo = '" + DBNull.Value?.ToString() + "' AND Account.TinNo = ''";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows.Add(intDGVCount, "OE", strStateCode, strGSTRate, row[0].ToString(), row[1], row[2], "0.00", "");
					intDGVCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "715");
			}
		}

		private void GSTRateFindMethod(string strId, string strGSTNo, string strSCode, string strInvNo, string strInvDt, string strInvType)
		{
			try
			{
				Query = "Select distinct SalesEstimateSettings.GST From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateMaster.SalesInvId='" + strId + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					TotalFindMethod(strId, strGSTNo, strSCode, row[0].ToString(), strInvNo, strInvDt, strInvType);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "686");
			}
		}

		private void TotalFindMethod(string strId, string strGSTNo, string strSCode, string strGSTRate, string strInvNo, string strInvDt, string strInvType)
		{
			try
			{
				Query = "Select SUM(SalesEstimateSettings.NetAmnt+SalesEstimateSettings.GSTAMOUNT) as InvoiceTotal,SUM(SalesEstimateSettings.NetAmnt)as TaxableVal From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId where SalesEstimateSettings.SalesInvId='" + strId + "' and SalesEstimateSettings.GST='" + strGSTRate + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows.Add(intDGVCount, strGSTNo, strInvNo, strInvDt, row[0].ToString(), strSCode, "N", strInvType, "", strGSTRate, row[1], "0.00");
					intDGVCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = "GST R-1 Report from " + dtmFrom.Text + " to " + dtmTo.Text;
			ReportGenerateMethod();
			ReportView reportView = new ReportView();
			reportView = new ReportView();
			reportView.MdiParent = base.MdiParent;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Title = sTitle;
			reportView.blPrint = false;
			reportView.blMail = false;
			reportView.Show();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			rdbDate.Checked = true;
		}

		private void ProductWiseGSTReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillFormType(clbForms);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
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
			clbForms = new System.Windows.Forms.CheckedListBox();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			cmbReportType = new AshUsercontrol.AshComboBox();
			label8 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			rdbHSN = new System.Windows.Forms.RadioButton();
			rdbProduct = new System.Windows.Forms.RadioButton();
			rdbDate = new System.Windows.Forms.RadioButton();
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(337, 0);
			clbForms.AllowDrop = true;
			clbForms.CheckOnClick = true;
			clbForms.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			clbForms.FormattingEnabled = true;
			clbForms.HorizontalExtent = 1;
			clbForms.HorizontalScrollbar = true;
			clbForms.Location = new System.Drawing.Point(112, 171);
			clbForms.Name = "clbForms";
			clbForms.Size = new System.Drawing.Size(238, 76);
			clbForms.Sorted = true;
			clbForms.TabIndex = 99;
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(144, 50);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(146, 23);
			dtmFrom.TabIndex = 96;
			cmbReportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbReportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbReportType.EnterColor = System.Drawing.Color.LightPink;
			cmbReportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbReportType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbReportType.FormattingEnabled = true;
			cmbReportType.Items.AddRange(new object[5] { "B2B Supplies", "Export", "B2C Large", "B2C Small", "HSN Report" });
			cmbReportType.LeaveColor = System.Drawing.Color.White;
			cmbReportType.Location = new System.Drawing.Point(139, 103);
			cmbReportType.Name = "cmbReportType";
			cmbReportType.Size = new System.Drawing.Size(221, 24);
			cmbReportType.TabIndex = 94;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(39, 106);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(100, 16);
			label8.TabIndex = 93;
			label8.Text = "Report Type :";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(67, 74);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 16);
			label5.TabIndex = 97;
			label5.Text = "Date To :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(52, 54);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(87, 16);
			label6.TabIndex = 95;
			label6.Text = "Date From :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(144, 74);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(146, 23);
			dtmTo.TabIndex = 98;
			rdbHSN.AutoSize = true;
			rdbHSN.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbHSN.Location = new System.Drawing.Point(225, 148);
			rdbHSN.Name = "rdbHSN";
			rdbHSN.Size = new System.Drawing.Size(125, 20);
			rdbHSN.TabIndex = 102;
			rdbHSN.TabStop = true;
			rdbHSN.Text = "HSN Code wise";
			rdbHSN.UseVisualStyleBackColor = true;
			rdbHSN.Visible = false;
			rdbProduct.AutoSize = true;
			rdbProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbProduct.Location = new System.Drawing.Point(112, 148);
			rdbProduct.Name = "rdbProduct";
			rdbProduct.Size = new System.Drawing.Size(111, 20);
			rdbProduct.TabIndex = 101;
			rdbProduct.Text = "Product wise";
			rdbProduct.UseVisualStyleBackColor = true;
			rdbProduct.Visible = false;
			rdbDate.AutoSize = true;
			rdbDate.Checked = true;
			rdbDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbDate.Location = new System.Drawing.Point(15, 148);
			rdbDate.Name = "rdbDate";
			rdbDate.Size = new System.Drawing.Size(91, 20);
			rdbDate.TabIndex = 100;
			rdbDate.TabStop = true;
			rdbDate.Text = "Date wise";
			rdbDate.UseVisualStyleBackColor = true;
			rdbDate.Visible = false;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(157, 24);
			lblTitle.TabIndex = 107;
			lblTitle.Text = "GST R-1 Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(210, 285);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 103;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(291, 285);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 104;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(139, 255);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(221, 24);
			cmbBranch.TabIndex = 177;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(72, 255);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 176;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(372, 319);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(rdbHSN);
			base.Controls.Add(rdbProduct);
			base.Controls.Add(rdbDate);
			base.Controls.Add(clbForms);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(cmbReportType);
			base.Controls.Add(label8);
			base.Controls.Add(label5);
			base.Controls.Add(label6);
			base.Controls.Add(dtmTo);
			base.Name = "ProductWiseGSTReportView";
			base.Load += new System.EventHandler(ProductWiseGSTReportView_Load);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(cmbReportType, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(clbForms, 0);
			base.Controls.SetChildIndex(rdbDate, 0);
			base.Controls.SetChildIndex(rdbProduct, 0);
			base.Controls.SetChildIndex(rdbHSN, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
