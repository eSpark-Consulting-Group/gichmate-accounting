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
	public class InventorySalesRegisterView : BaseForm
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

		private string strFormType;

		private decimal dmlQty;

		private decimal dmlGross;

		private decimal dmlDis;

		private decimal dmlTax;

		private decimal dmlNet;

		private int DgvRowCount;

		private string checkedForms;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbGroup;

		private Label label1;

		private Label label4;

		private Label label3;

		private AshComboBox cmbSupplier;

		private Label label7;

		private AshComboBox cmbBrand;

		private Label label2;

		private AshComboBox cmbProduct;

		private AshComboBox cmbSMan;

		private Label label13;

		private AshComboBox cmbPCategory;

		private AshComboBox cmbAccount;

		private Label label10;

		private Label label9;

		private AshComboBox cmbGSTRate;

		private AshComboBox cmbRoute;

		private Label label12;

		private Label label8;

		private AshComboBox cmbSpecification;

		private AshDateTimePicker dtmTo;

		private Label label5;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private Label label11;

		private AshComboBox cmbGodown;

		private CheckedListBox clbForms;

		private RadioButton rdbDetails;

		private RadioButton rdbSummary;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string ReportFormTitle { get; set; }

		public InventorySalesRegisterView(string strForm)
		{
			strFormType = strForm;
			InitializeComponent();
			ColorChange.Color(this);
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
			Gridview = AniHelper.GetGridviewDatatable();
			string[] headers = new string[36]
			{
				"SL", "Voucher No", "Voucher Type", "Date", "Customer Name", "Product Name", "HSN", "Quantity", "Unit", "Rate",
				"Gross", "Disc(%)", "Discount Amount", "Tax(%)", "Tax Amount", "Net Amount", "Product Code", "Product Group", "Brand", "Supplier",
				"Depot", "Shelf", "Purchase Rate", "Sales Rate", "MRP", "Price Category", "Route", "Salesman", "Vehicle", "Address One",
				"Address Two", "Address Three", "Mobile No", "Email", "GST NO", "State Code"
			};
			string[] headers2 = new string[33]
			{
				"SL", "Voucher No", "Voucher Type", "Date", "Customer Name", "Product Name", "HSN", "Quantity", "Unit", "Rate",
				"Gross", "Disc(%)", "Discount Amount", "Tax(%)", "Tax Amount", "Net Amount", "Product Code", "Product Group", "Brand", "Supplier",
				"Depot", "Shelf", "Purchase Rate", "Sales Rate", "MRP", "Price Category", "Address One", "Address Two", "Address Three", "Mobile No",
				"Email", "GST NO", "State Code"
			};
			string[] headers3 = new string[16]
			{
				"SL", "Voucher No", "Voucher Type", "Date", "Customer Name", "Product Name", "HSN", "Quantity", "Unit", "Rate",
				"Gross", "Disc(%)", "Discount Amount", "Tax(%)", "Tax Amount", "Net Amount"
			};
			if (rdbDetails.Checked)
			{
				if (strFormType.Contains("Sales"))
				{
					Details = AniHelper.GetDataTable(headers);
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(300, "L");
					Gridview.Rows.Add(300, "L");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(70, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(80, "L");
				}
				else
				{
					Details = AniHelper.GetDataTable(headers2);
					Gridview.Rows.Add(50, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(300, "L");
					Gridview.Rows.Add(300, "L");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(60, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(80, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(150, "L");
					Gridview.Rows.Add(70, "L");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "R");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(120, "L");
					Gridview.Rows.Add(200, "L");
					Gridview.Rows.Add(80, "L");
				}
			}
			else
			{
				Details = AniHelper.GetDataTable(headers3);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(300, "L");
				Gridview.Rows.Add(80, "L");
				Gridview.Rows.Add(80, "L");
				Gridview.Rows.Add(60, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(60, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(60, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
			}
			dmlQty = (dmlGross = (dmlDis = (dmlTax = (dmlNet = 0m))));
			if (clbForms.CheckedItems.Count > 0)
			{
				foreach (string checkedItem in clbForms.CheckedItems)
				{
					checkedForms = checkedForms + "'" + checkedItem + "',";
				}
			}
			else if (strFormType != "Sales Return" && strFormType != "Purchase Return")
			{
				foreach (MasterValue item in CommonSettings.GetFormType())
				{
					checkedForms = checkedForms + "'" + item.Description + "',";
				}
			}
			checkedForms = checkedForms.Remove(checkedForms.Length - 1, 1);
			string text6 = "";
			if (strFormType == "Sales")
			{
				text6 = " and SalesEstimateMaster.InvType in (" + checkedForms + ") ";
			}
			else if (strFormType == "Purchase")
			{
				text6 = " and PurInvMaster.InvType in (" + checkedForms + ") ";
			}
			string text7 = "";
			if (cmbGroup.Text != "")
			{
				text7 = " and Product.PGroupId='" + cmbGroup.SelectedValue.ToInt32() + "' ";
			}
			string text8 = "";
			if (cmbBrand.Text != "")
			{
				text8 = " and Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "' ";
			}
			string text9 = "";
			if (cmbSupplier.Text != "")
			{
				text9 = " and Product.AccountId='" + cmbSupplier.SelectedValue.ToInt32() + "' ";
			}
			string text10 = "";
			if (cmbProduct.Text != "")
			{
				text10 = " and Product.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
			}
			string text11 = "";
			if (cmbSMan.Text != "")
			{
				text11 = " and SalesEstimateMaster.StafId='" + cmbSMan.SelectedValue.ToInt32() + "' ";
			}
			string text12 = "";
			if (cmbSpecification.Text != "")
			{
				text12 = " and Product.Specification='" + cmbSpecification.Text + "' ";
			}
			string text13 = "";
			if (cmbGodown.Text != "")
			{
				text13 = " and Product.GodwnId='" + cmbGodown.SelectedValue.ToInt32() + "'";
			}
			string text14 = "";
			if (cmbAccount.Text != "")
			{
				text14 = " and Account.AccountId='" + cmbAccount.SelectedValue.ToInt32() + "' ";
			}
			string text15 = "";
			if (cmbRoute.Text != "")
			{
				text15 = " and SalesEstimateMaster.RouteId='" + cmbRoute.SelectedValue.ToInt32() + "' ";
			}
			string text16 = "";
			if (cmbPCategory.Text != "")
			{
				text16 = " and SalesEstimateMaster.PriceCategoryId='" + cmbPCategory.SelectedValue.ToInt32() + "'";
			}
			if (rdbSummary.Checked)
			{
				if (Details.Columns.Count > 16)
				{
					for (int num = Details.Columns.Count - 1; num >= 16; num--)
					{
						Details.Columns.RemoveAt(num);
					}
				}
				if (strFormType == "Sales")
				{
					strQuery = "Select SalesEstimateMaster.SalesInvNo,Convert(nvarchar,SalesEstimateSettings.Date,103) as Date,Account.AccountName,Product.ProductName,SalesEstimateSettings.ProQty,SalesEstimateSettings.UnitId,SalesEstimateSettings.ProRate,SalesEstimateSettings.GrossAmnt,SalesEstimateSettings.ProDiscont,SalesEstimateSettings.DiscAmnt,SalesEstimateSettings.NetAmnt,SalesEstimateSettings.GST,SalesEstimateSettings.GSTAMOUNT,SalesEstimateMaster.InvType,Product.HSNNo,Account.TinNo,Account.CstNo From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId Join Product on SalesEstimateSettings.ProductId=Product.ProductId Join Account on SalesEstimateSettings.PartyAccountId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "'" + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text;
				}
				else if (strFormType == "Sales Return")
				{
					strQuery = "Select SalesRtnMaster.SlRtnInvNo,Convert(nvarchar,SlsRtnSettings.Date,103) as Date,Account.AccountName,Product.ProductName,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.NetAmnt,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,Product.HSNNo,Account.TinNo,Account.CstNo From SlsRtnSettings Join SalesRtnMaster on SlsRtnSettings.SlsRtnId=SalesRtnMaster.SlsRtrnId Join Product on SlsRtnSettings.ProductId=Product.ProductId Join Account on SlsRtnSettings.PartyAccountId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "'" + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text2;
				}
				else if (strFormType == "Purchase")
				{
					strQuery = "Select PurInvMaster.PurInvNo,Convert(nvarchar,PurInvSettings.Date,103) as Date,Account.AccountName,Product.ProductName,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.NetAmnt,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvMaster.InvType,Product.HSNNo,Account.TinNo,Account.CstNo From PurInvSettings Join PurInvMaster on PurInvSettings.PurInvId=PurInvMaster.PurInvId Join Product on PurInvSettings.ProId=Product.ProductId Join Account on PurInvSettings.PartyAccId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "'" + text7 + text8 + text10 + text9 + text12 + text13 + text14 + text16 + text6 + text3;
				}
				else
				{
					strQuery = "Select PurRtnInvMaster.PurRtnInvNo,Convert(nvarchar,PurRtnInvSttings.Date,103) as Date,Account.AccountName,Product.ProductName,PurRtnInvSttings.ProQty,PurRtnInvSttings.UnitId,PurRtnInvSttings.ProRate,PurRtnInvSttings.Gross,PurRtnInvSttings.Disc,PurRtnInvSttings.DiscAmnt,PurRtnInvSttings.NetAmnt,PurRtnInvSttings.GST,PurRtnInvSttings.GSTAMOUNT,Product.HSNNo,Account.TinNo,Account.CstNo From PurRtnInvSttings Join PurRtnInvMaster on PurRtnInvSttings.PurRtnInvId=PurRtnInvMaster.PurRtnInvId Join Product on PurRtnInvSttings.ProId=Product.ProductId Join Account on PurRtnInvSttings.PartyAccId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "'" + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text4;
				}
			}
			else if (strFormType == "Sales")
			{
				strQuery = "Select SalesEstimateMaster.SalesInvNo,Convert(nvarchar,SalesEstimateSettings.Date,103) as Date,Account.AccountName,Product.ProductName,SalesEstimateSettings.ProQty,SalesEstimateSettings.UnitId,SalesEstimateSettings.ProRate,SalesEstimateSettings.GrossAmnt,SalesEstimateSettings.ProDiscont,SalesEstimateSettings.DiscAmnt,SalesEstimateSettings.NetAmnt ,Product.ProductCode,Product.PGroupId,Product.BrandId,Product.AccountId,Product.GodwnId,Product.SelfId,Product.PurchasePrice,Product.SellingPrice,Product.MRP,SalesEstimateMaster.PriceCategoryId,SalesEstimateMaster.RouteId,SalesEstimateMaster.StafId,SalesEstimateMaster.VehId,Account.AddressOne,Account.AddressTwo,Account.AddressThree,Account.MobileNo,Account.EMailOne  ,SalesEstimateSettings.GST,SalesEstimateSettings.GSTAMOUNT,SalesEstimateMaster.InvType,Product.HSNNo,Account.TinNo,Account.CstNo From SalesEstimateSettings Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId=SalesEstimateMaster.SalesInvId Join Product on SalesEstimateSettings.ProductId=Product.ProductId Join Account on SalesEstimateSettings.PartyAccountId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' " + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text;
			}
			else if (strFormType == "Sales Return")
			{
				strQuery = "Select SalesRtnMaster.SlRtnInvNo,Convert(nvarchar,SlsRtnSettings.Date,103) as Date,Account.AccountName,Product.ProductName,SlsRtnSettings.ProQty,SlsRtnSettings.UnitId,SlsRtnSettings.ProRate,SlsRtnSettings.GrossAmnt,SlsRtnSettings.ProDis,SlsRtnSettings.DiscAmnt,SlsRtnSettings.NetAmnt ,Product.ProductCode,Product.PGroupId,Product.BrandId,Product.AccountId,Product.GodwnId,Product.SelfId,Product.PurchasePrice,Product.SellingPrice,Product.MRP,SalesRtnMaster.PriceCategoryId,SalesRtnMaster.RouteId,SalesRtnMaster.StaffId,SalesRtnMaster.VehId,Account.AddressOne,Account.AddressTwo,Account.AddressThree,Account.MobileNo,Account.EMailOne  ,SlsRtnSettings.GST,SlsRtnSettings.GSTAMOUNT,Product.HSNNo,Account.TinNo,Account.CstNo From SlsRtnSettings Join SalesRtnMaster on SlsRtnSettings.SlsRtnId=SalesRtnMaster.SlsRtrnId Join Product on SlsRtnSettings.ProductId=Product.ProductId Join Account on SlsRtnSettings.PartyAccountId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text2;
			}
			else if (strFormType == "Purchase")
			{
				strQuery = "Select PurInvMaster.PurInvNo,Convert(nvarchar,PurInvSettings.Date,103) as Date,Account.AccountName,Product.ProductName,PurInvSettings.ProQty,PurInvSettings.UnitId,PurInvSettings.ProRate,PurInvSettings.Gross,PurInvSettings.Disc,PurInvSettings.DiscAmnt,PurInvSettings.NetAmnt ,Product.ProductCode,Product.PGroupId,Product.BrandId,Product.AccountId,Product.GodwnId,Product.SelfId,Product.PurchasePrice,Product.SellingPrice,Product.MRP,PurInvMaster.PriceCategoryId,Account.AddressOne,Account.AddressTwo,Account.AddressThree,Account.MobileNo,Account.EMailOne  ,PurInvSettings.GST,PurInvSettings.GSTAMOUNT,PurInvMaster.InvType,Product.HSNNo,Account.TinNo,Account.CstNo From PurInvSettings Join PurInvMaster on PurInvSettings.PurInvId=PurInvMaster.PurInvId Join Product on PurInvSettings.ProId=Product.ProductId Join Account on PurInvSettings.PartyAccId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text7 + text8 + text10 + text9 + text12 + text13 + text14 + text16 + text6 + text3;
			}
			else
			{
				strQuery = "Select PurRtnInvMaster.PurRtnInvNo,Convert(nvarchar,PurRtnInvSttings.Date,103) as Date,Account.AccountName,Product.ProductName,PurRtnInvSttings.ProQty,PurRtnInvSttings.UnitId,PurRtnInvSttings.ProRate,PurRtnInvSttings.Gross,PurRtnInvSttings.Disc,PurRtnInvSttings.DiscAmnt,PurRtnInvSttings.NetAmnt ,Product.ProductCode,Product.PGroupId,Product.BrandId,Product.AccountId,Product.GodwnId,Product.SelfId,Product.PurchasePrice,Product.SellingPrice,Product.MRP,PurRtnInvMaster.PriceCategoryId,Account.AddressOne,Account.AddressTwo,Account.AddressThree,Account.MobileNo,Account.EMailOne  ,PurRtnInvSttings.GST,PurRtnInvSttings.GSTAMOUNT,Product.HSNNo,Account.TinNo,Account.CstNo From PurRtnInvSttings Join PurRtnInvMaster on PurRtnInvSttings.PurRtnInvId=PurRtnInvMaster.PurRtnInvId Join Product on PurRtnInvSttings.ProId=Product.ProductId Join Account on PurRtnInvSttings.PartyAccId = Account.AccountId where Date between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text7 + text8 + text10 + text9 + text11 + text12 + text13 + text14 + text15 + text16 + text6 + text4;
			}
			try
			{
				string strID = "";
				string strID2 = "";
				string strID3 = "";
				string strID4 = "";
				string strID5 = "";
				string strID6 = "";
				string strID7 = "";
				string strID8 = "";
				string strID9 = "";
				string strID10 = "";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[5].ToString(), out strID);
					DgvRowCount = Details.Rows.Count + 1;
					dmlQty += Convert.ToDecimal(row[4].ToString());
					dmlGross += Convert.ToDecimal(row[7].ToString());
					dmlDis += Convert.ToDecimal(row[9].ToString());
					dmlNet += Convert.ToDecimal(row[10].ToString());
					if (rdbSummary.Checked)
					{
						if (strFormType == "Sales")
						{
							Details.Rows.Add(DgvRowCount, row[0], row[13], row[1], row[2], row[3], row[14], row[4], strID, row[6], row[7], row[8], row[9], row[11], row[12], row[10]);
						}
						else if (strFormType == "Sales Return")
						{
							Details.Rows.Add(DgvRowCount, row[0], "Sales Return", row[1], row[2], row[3], row[13], row[4], strID, row[6], row[7], row[8], row[9], row[11], row[12], row[10]);
						}
						else if (strFormType == "Purchase")
						{
							Details.Rows.Add(DgvRowCount, row[0], row[13], row[1], row[2], row[3], row[14], row[4], strID, row[6], row[7], row[8], row[9], row[11], row[12], row[10]);
						}
						else
						{
							Details.Rows.Add(DgvRowCount, row[0], "Purchase Return", row[1], row[2], row[3], row[13], row[4], strID, row[6], row[7], row[8], row[9], row[11], row[12], row[10]);
						}
						if (row[12] != DBNull.Value)
						{
							dmlTax += row[12].ToString().ToDecimal();
						}
					}
					else
					{
						if (!rdbDetails.Checked)
						{
							continue;
						}
						AshproCommon.IdFindMethod("ProductGroup", "PGroupName", "PGroupId", row[12].ToString(), out strID2);
						AshproCommon.IdFindMethod("Brand", "BrandName", "BrandId", row[13].ToString(), out strID3);
						AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[14].ToString(), out strID4);
						AshproCommon.IdFindMethod("Godown", "GodownName", "GodwnId", row[15].ToString(), out strID5);
						AshproCommon.IdFindMethod("Self", "SelfName", "SelfId", row[16].ToString(), out strID6);
						AshproCommon.IdFindMethod("PriceCategory", "PCategoryName", "PCategoryId", row[20].ToString(), out strID7);
						if (strFormType.Contains("Sales"))
						{
							if (row[30] != DBNull.Value)
							{
								dmlTax += row[30].ToString().ToDecimal();
							}
							AshproCommon.IdFindMethod("Root", "RootName", "RootId", row[21].ToString(), out strID8);
							AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", row[22].ToString(), out strID9);
							AshproCommon.IdFindMethod("Vehicle", "RegNo", "VanId", row[23].ToString(), out strID10);
							Details.Rows.Add(DgvRowCount, row[0], row[31], row[1], row[2], row[3], row[32], row[4], strID, row[6], row[7], row[8], row[9], row[29], row[30], row[10], row[11], strID2, strID3, strID4, strID5, strID6, row[17], row[18], row[19], strID7, strID8, strID9, strID10, row[24], row[25], row[26], row[27], row[28], row[33], row[34]);
						}
						else
						{
							if (row[27] != DBNull.Value)
							{
								dmlTax += row[27].ToString().ToDecimal();
							}
							Details.Rows.Add(DgvRowCount, row[0], row[28], row[1], row[2], row[3], row[28], row[4], strID, row[6], row[7], row[8], row[9], row[26], row[27], row[10], row[11], strID2, strID3, strID4, strID5, strID6, row[17], row[18], row[19], strID7, row[21], row[22], row[23], row[24], row[25], row[29], row[30]);
						}
					}
				}
				dmlGross = Math.Round(dmlGross, 0);
				dmlDis = Math.Round(dmlDis, 0);
				dmlTax = Math.Round(dmlTax, 0);
				dmlNet = Math.Round(dmlNet, 0);
				if (rdbDetails.Checked)
				{
					if (strFormType.Contains("Sales"))
					{
						Details.Rows.Add("", "", "", "", "", "Total", "", dmlQty.ToString(CommonSettings.strCurencyFormat), "", "", dmlGross.ToString(CommonSettings.strCurencyFormat), "", dmlDis.ToString(CommonSettings.strCurencyFormat), "", dmlTax.ToString(CommonSettings.strCurencyFormat), dmlNet.ToString(CommonSettings.strCurencyFormat), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
					}
					else
					{
						Details.Rows.Add("", "", "", "", "", "Total", "", dmlQty.ToString(CommonSettings.strCurencyFormat), "", "", dmlGross.ToString(CommonSettings.strCurencyFormat), "", dmlDis.ToString(CommonSettings.strCurencyFormat), "", dmlTax.ToString(CommonSettings.strCurencyFormat), dmlNet.ToString(CommonSettings.strCurencyFormat), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
					}
				}
				else
				{
					Details.Rows.Add("", "", "", "", "", "Total", "", dmlQty.ToString(CommonSettings.strCurencyFormat), "", "", dmlGross.ToString(CommonSettings.strCurencyFormat), "", dmlDis.ToString(CommonSettings.strCurencyFormat), "", dmlTax.ToString(CommonSettings.strCurencyFormat), dmlNet.ToString(CommonSettings.strCurencyFormat));
				}
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
				if (!rdbSummary.Checked)
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(80, 80);
					LineData.Rows.Add(245, 245);
					LineData.Rows.Add(410, 410);
					LineData.Rows.Add(470, 470);
					LineData.Rows.Add(510, 510);
					LineData.Rows.Add(590, 590);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("Date", 8);
					Header.Rows.Add("Customer Name", 82);
					Header.Rows.Add("Product", 247);
					Header.Rows.Add("Qty", 412);
					Header.Rows.Add("Unit", 471);
					Header.Rows.Add("Rate", 511);
					Header.Rows.Add("Address", 591);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(3, 8, false, false, "", 0);
					data.Rows.Add(4, 85, false, true, "", 165);
					data.Rows.Add(5, 499, false, true, "", 165);
					data.Rows.Add(7, 505, true, false, "", 0);
					data.Rows.Add(8, 644, false, false, "", 0);
					data.Rows.Add(9, 798, false, false, "Right", 0);
					if (!(strFormType == "Sales") && !(strFormType == "Sales Return"))
					{
						data.Rows.Add(26, 798, true, true, "", 190);
					}
					else
					{
						data.Rows.Add(29, 798, true, true, "", 190);
					}
				}
				else
				{
					LineData = AniHelper.GetLineDatatable();
					LineData.Rows.Add(75, 75);
					LineData.Rows.Add(130, 130);
					LineData.Rows.Add(295, 295);
					LineData.Rows.Add(465, 465);
					LineData.Rows.Add(520, 520);
					LineData.Rows.Add(555, 555);
					LineData.Rows.Add(620, 620);
					LineData.Rows.Add(660, 660);
					LineData.Rows.Add(695, 695);
					Header = AniHelper.GetHeadDatatable();
					Header.Rows.Add("Date", 8);
					Header.Rows.Add("V No", 76);
					Header.Rows.Add("Customer Name", 132);
					Header.Rows.Add("Product", 297);
					Header.Rows.Add("Qty", 467);
					Header.Rows.Add("Unit", 520);
					Header.Rows.Add("Rate", 557);
					Header.Rows.Add("Dis", 621);
					Header.Rows.Add("GST", 661);
					Header.Rows.Add("Net Amnt", 696);
					data = AniHelper.GetDetailsDatatable();
					data.Rows.Add(3, 8, false, false, "", 0);
					data.Rows.Add(1, 76, true, false, "", 0);
					data.Rows.Add(4, 132, false, true, "", 165);
					data.Rows.Add(5, 297, false, true, "", 165);
					data.Rows.Add(7, 467, true, false, "", 0);
					data.Rows.Add(8, 521, false, false, "", 0);
					data.Rows.Add(9, 619, false, false, "Right", 0);
					data.Rows.Add(11, 659, false, false, "Right", 0);
					data.Rows.Add(13, 694, false, false, "Right", 0);
					data.Rows.Add(15, 798, true, false, "Right", 0);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			if (!rdbSummary.Checked)
			{
				LineDataPDF = AniHelper.GetLineDatatablePDF();
				LineDataPDF.Rows.Add(8, 50, 585, 50);
				LineDataPDF.Rows.Add(8, 50, 8, 800);
				LineDataPDF.Rows.Add(8, 800, 585, 800);
				LineDataPDF.Rows.Add(585, 50, 585, 800);
				LineDataPDF.Rows.Add(385, 50, 385, 800);
				LineDataPDF.Rows.Add(335, 50, 335, 800);
				LineDataPDF.Rows.Add(310, 50, 310, 800);
				LineDataPDF.Rows.Add(260, 50, 260, 800);
				LineDataPDF.Rows.Add(160, 50, 160, 800);
				LineDataPDF.Rows.Add(65, 50, 65, 800);
				HeaderPDF = AniHelper.GetHeadDatatable();
				HeaderPDF.Rows.Add("Date", 9);
				HeaderPDF.Rows.Add("Customer Name", 70);
				HeaderPDF.Rows.Add("Product Name", 165);
				HeaderPDF.Rows.Add("Qty", 265);
				HeaderPDF.Rows.Add("Unit", 315);
				HeaderPDF.Rows.Add("Rate", 340);
				HeaderPDF.Rows.Add("Address", 390);
				dataPDF = AniHelper.GetDetailsDatatable();
				dataPDF.Rows.Add(3, 10, false, false, "");
				dataPDF.Rows.Add(4, 33, true, false, "", 0);
				dataPDF.Rows.Add(5, 384, false, false, "Right", 0);
				dataPDF.Rows.Add(7, 386, true, false, "", 0);
				dataPDF.Rows.Add(8, 489, false, false, "Right", 0);
				dataPDF.Rows.Add(9, 583, false, false, "Right", 0);
				if (!(strFormType == "Sales") && !(strFormType == "Sales Return"))
				{
					dataPDF.Rows.Add(26, 583, false, false, "Right", 0);
				}
				else
				{
					dataPDF.Rows.Add(29, 583, false, false, "Right", 0);
				}
			}
			else
			{
				LineDataPDF = AniHelper.GetLineDatatablePDF();
				LineDataPDF.Rows.Add(8, 50, 585, 50);
				LineDataPDF.Rows.Add(8, 50, 8, 800);
				LineDataPDF.Rows.Add(8, 800, 585, 800);
				LineDataPDF.Rows.Add(585, 50, 585, 800);
				LineDataPDF.Rows.Add(525, 50, 525, 800);
				LineDataPDF.Rows.Add(497, 50, 497, 800);
				LineDataPDF.Rows.Add(469, 50, 469, 800);
				LineDataPDF.Rows.Add(414, 50, 414, 800);
				LineDataPDF.Rows.Add(390, 50, 390, 800);
				LineDataPDF.Rows.Add(335, 50, 335, 800);
				LineDataPDF.Rows.Add(225, 50, 225, 800);
				LineDataPDF.Rows.Add(115, 50, 115, 800);
				LineDataPDF.Rows.Add(65, 50, 65, 800);
				HeaderPDF = AniHelper.GetHeadDatatable();
				HeaderPDF.Rows.Add("Date", 10);
				HeaderPDF.Rows.Add("Vchr No", 67);
				HeaderPDF.Rows.Add("Customer Name", 117);
				HeaderPDF.Rows.Add("Product", 227);
				HeaderPDF.Rows.Add("Qty", 337);
				HeaderPDF.Rows.Add("Unit", 391);
				HeaderPDF.Rows.Add("Rate", 416);
				HeaderPDF.Rows.Add("Dis", 470);
				HeaderPDF.Rows.Add("Tax", 498);
				HeaderPDF.Rows.Add("Net Amnt", 527);
				dataPDF = AniHelper.GetDetailsDatatable();
				dataPDF.Rows.Add(3, 10, false, false, "", 0);
				dataPDF.Rows.Add(1, 66, false, false, "", 0);
				dataPDF.Rows.Add(4, 116, true, true, "", 20);
				dataPDF.Rows.Add(5, 226, false, true, "", 20);
				dataPDF.Rows.Add(7, 336, true, false, "", 0);
				dataPDF.Rows.Add(8, 391, false, false, "", 0);
				dataPDF.Rows.Add(9, 468, false, false, "Right", 0);
				dataPDF.Rows.Add(11, 496, false, false, "Right", 0);
				dataPDF.Rows.Add(13, 525, false, false, "Right", 0);
				dataPDF.Rows.Add(15, 583, false, false, "Right", 0);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			rdbSummary.Checked = true;
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cmbGroup.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (strFormType == "Sales")
			{
				sTitle = "Inventory Sales Register from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else if (strFormType == "Sales Return")
			{
				sTitle = "Inventory Sales Return Register from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else if (strFormType == "Purchase")
			{
				sTitle = "Inventory Purchase Register from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else
			{
				sTitle = "Inventory Purchase Return Register from " + dtmFrom.Text + " to " + dtmTo.Text;
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

		private void InventorySalesRegisterView_Load(object sender, EventArgs e)
		{
			lblTitle.Text = ReportFormTitle;
			if (strFormType == "Sales Return")
			{
				lblTitle.Text = ((ReportFormTitle == string.Empty) ? "Inventory Sales Return Register" : ReportFormTitle);
				clbForms.Visible = false;
			}
			else if (strFormType == "Purchase")
			{
				lblTitle.Text = ((ReportFormTitle == string.Empty) ? "Inventory Purchase Register" : ReportFormTitle);
				label7.Visible = false;
				cmbSMan.Visible = false;
				label8.Visible = false;
				cmbRoute.Visible = false;
				label11.Visible = false;
				cmbGodown.Visible = false;
			}
			else if (strFormType == "Purchase Return")
			{
				lblTitle.Text = ((ReportFormTitle == string.Empty) ? "Inventory Purchase Return Register" : ReportFormTitle);
				clbForms.Visible = false;
				label7.Visible = false;
				cmbSMan.Visible = false;
				label8.Visible = false;
				cmbRoute.Visible = false;
				label11.Visible = false;
				cmbGodown.Visible = false;
			}
			else
			{
				lblTitle.Text = ((ReportFormTitle == null) ? ("Inventory " + strFormType + " Register") : ReportFormTitle);
			}
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillEmployees(cmbSMan);
			AniHelper.FillPCategory(cmbPCategory);
			AniHelper.FillGodown(cmbGodown);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillTCategory(cmbGSTRate);
			AniHelper.FillAccount(cmbAccount, isCashAccntIncluded: false);
			AniHelper.FillSpecification(cmbSpecification);
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
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbGroup = new AshUsercontrol.AshComboBox();
			label1 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			cmbSupplier = new AshUsercontrol.AshComboBox();
			label7 = new System.Windows.Forms.Label();
			cmbBrand = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			cmbProduct = new AshUsercontrol.AshComboBox();
			cmbSMan = new AshUsercontrol.AshComboBox();
			label13 = new System.Windows.Forms.Label();
			cmbPCategory = new AshUsercontrol.AshComboBox();
			cmbAccount = new AshUsercontrol.AshComboBox();
			label10 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			cmbGSTRate = new AshUsercontrol.AshComboBox();
			cmbRoute = new AshUsercontrol.AshComboBox();
			label12 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			cmbSpecification = new AshUsercontrol.AshComboBox();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label5 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label6 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			cmbGodown = new AshUsercontrol.AshComboBox();
			clbForms = new System.Windows.Forms.CheckedListBox();
			rdbDetails = new System.Windows.Forms.RadioButton();
			rdbSummary = new System.Windows.Forms.RadioButton();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(664, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 2);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(235, 24);
			lblTitle.TabIndex = 99;
			lblTitle.Text = "Inventory Sales Register";
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(536, 339);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 96;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(622, 339);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 97;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGroup.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGroup.FormattingEnabled = true;
			cmbGroup.LeaveColor = System.Drawing.Color.White;
			cmbGroup.Location = new System.Drawing.Point(140, 58);
			cmbGroup.Name = "cmbGroup";
			cmbGroup.Size = new System.Drawing.Size(230, 24);
			cmbGroup.TabIndex = 102;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(79, 61);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 16);
			label1.TabIndex = 101;
			label1.Text = "Group :";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(65, 142);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 16);
			label4.TabIndex = 107;
			label4.Text = "Supplier :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(66, 115);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(70, 16);
			label3.TabIndex = 105;
			label3.Text = "Product :";
			cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
			cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSupplier.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSupplier.FormattingEnabled = true;
			cmbSupplier.LeaveColor = System.Drawing.Color.White;
			cmbSupplier.Location = new System.Drawing.Point(140, 139);
			cmbSupplier.Name = "cmbSupplier";
			cmbSupplier.Size = new System.Drawing.Size(230, 24);
			cmbSupplier.TabIndex = 108;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(31, 169);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(105, 16);
			label7.TabIndex = 109;
			label7.Text = "Salesman/Exi :";
			cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBrand.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBrand.FormattingEnabled = true;
			cmbBrand.LeaveColor = System.Drawing.Color.White;
			cmbBrand.Location = new System.Drawing.Point(140, 85);
			cmbBrand.Name = "cmbBrand";
			cmbBrand.Size = new System.Drawing.Size(230, 24);
			cmbBrand.TabIndex = 104;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(80, 88);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(56, 16);
			label2.TabIndex = 103;
			label2.Text = "Brand :";
			cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProduct.EnterColor = System.Drawing.Color.LightPink;
			cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProduct.FormattingEnabled = true;
			cmbProduct.LeaveColor = System.Drawing.Color.White;
			cmbProduct.Location = new System.Drawing.Point(140, 112);
			cmbProduct.Name = "cmbProduct";
			cmbProduct.Size = new System.Drawing.Size(230, 24);
			cmbProduct.TabIndex = 106;
			cmbSMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSMan.EnterColor = System.Drawing.Color.LightPink;
			cmbSMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSMan.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSMan.FormattingEnabled = true;
			cmbSMan.LeaveColor = System.Drawing.Color.White;
			cmbSMan.Location = new System.Drawing.Point(140, 166);
			cmbSMan.Name = "cmbSMan";
			cmbSMan.Size = new System.Drawing.Size(230, 24);
			cmbSMan.TabIndex = 110;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label13.Location = new System.Drawing.Point(20, 226);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(116, 16);
			label13.TabIndex = 113;
			label13.Text = "Price Category :";
			cmbPCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPCategory.EnterColor = System.Drawing.Color.LightPink;
			cmbPCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPCategory.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbPCategory.FormattingEnabled = true;
			cmbPCategory.LeaveColor = System.Drawing.Color.White;
			cmbPCategory.Location = new System.Drawing.Point(140, 223);
			cmbPCategory.Name = "cmbPCategory";
			cmbPCategory.Size = new System.Drawing.Size(230, 24);
			cmbPCategory.TabIndex = 114;
			cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAccount.EnterColor = System.Drawing.Color.LightPink;
			cmbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAccount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbAccount.FormattingEnabled = true;
			cmbAccount.LeaveColor = System.Drawing.Color.White;
			cmbAccount.Location = new System.Drawing.Point(140, 250);
			cmbAccount.Name = "cmbAccount";
			cmbAccount.Size = new System.Drawing.Size(230, 24);
			cmbAccount.TabIndex = 116;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label10.Location = new System.Drawing.Point(62, 255);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 16);
			label10.TabIndex = 115;
			label10.Text = "Account :";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(32, 199);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(104, 16);
			label9.TabIndex = 111;
			label9.Text = "Specification :";
			cmbGSTRate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGSTRate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGSTRate.EnterColor = System.Drawing.Color.LightPink;
			cmbGSTRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGSTRate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGSTRate.FormattingEnabled = true;
			cmbGSTRate.LeaveColor = System.Drawing.Color.White;
			cmbGSTRate.Location = new System.Drawing.Point(140, 277);
			cmbGSTRate.Name = "cmbGSTRate";
			cmbGSTRate.Size = new System.Drawing.Size(230, 24);
			cmbGSTRate.TabIndex = 118;
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Location = new System.Drawing.Point(452, 53);
			cmbRoute.Name = "cmbRoute";
			cmbRoute.Size = new System.Drawing.Size(230, 24);
			cmbRoute.TabIndex = 120;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(55, 283);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(81, 16);
			label12.TabIndex = 117;
			label12.Text = "GST Rate :";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(391, 60);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(57, 16);
			label8.TabIndex = 119;
			label8.Text = "Route :";
			cmbSpecification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSpecification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSpecification.EnterColor = System.Drawing.Color.LightPink;
			cmbSpecification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSpecification.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSpecification.FormattingEnabled = true;
			cmbSpecification.LeaveColor = System.Drawing.Color.White;
			cmbSpecification.Location = new System.Drawing.Point(140, 196);
			cmbSpecification.Name = "cmbSpecification";
			cmbSpecification.Size = new System.Drawing.Size(230, 24);
			cmbSpecification.TabIndex = 112;
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(452, 138);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 124;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(412, 142);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(36, 16);
			label5.TabIndex = 123;
			label5.Text = "To :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(452, 110);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 23);
			dtmFrom.TabIndex = 122;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(397, 114);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(51, 16);
			label6.TabIndex = 121;
			label6.Text = "From :";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label11.Location = new System.Drawing.Point(377, 86);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(71, 16);
			label11.TabIndex = 125;
			label11.Text = "Godown :";
			cmbGodown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGodown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGodown.EnterColor = System.Drawing.Color.LightPink;
			cmbGodown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGodown.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGodown.FormattingEnabled = true;
			cmbGodown.LeaveColor = System.Drawing.Color.White;
			cmbGodown.Location = new System.Drawing.Point(452, 83);
			cmbGodown.Name = "cmbGodown";
			cmbGodown.Size = new System.Drawing.Size(230, 24);
			cmbGodown.TabIndex = 126;
			clbForms.AllowDrop = true;
			clbForms.CheckOnClick = true;
			clbForms.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			clbForms.FormattingEnabled = true;
			clbForms.HorizontalExtent = 1;
			clbForms.HorizontalScrollbar = true;
			clbForms.Location = new System.Drawing.Point(452, 169);
			clbForms.Name = "clbForms";
			clbForms.Size = new System.Drawing.Size(230, 94);
			clbForms.Sorted = true;
			clbForms.TabIndex = 127;
			rdbDetails.AutoSize = true;
			rdbDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbDetails.Location = new System.Drawing.Point(559, 277);
			rdbDetails.Name = "rdbDetails";
			rdbDetails.Size = new System.Drawing.Size(70, 20);
			rdbDetails.TabIndex = 129;
			rdbDetails.Text = "Details";
			rdbDetails.UseVisualStyleBackColor = true;
			rdbSummary.AutoSize = true;
			rdbSummary.Checked = true;
			rdbSummary.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbSummary.Location = new System.Drawing.Point(465, 277);
			rdbSummary.Name = "rdbSummary";
			rdbSummary.Size = new System.Drawing.Size(86, 20);
			rdbSummary.TabIndex = 128;
			rdbSummary.TabStop = true;
			rdbSummary.Text = "Summary";
			rdbSummary.UseVisualStyleBackColor = true;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(452, 303);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(230, 24);
			cmbBranch.TabIndex = 131;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(380, 307);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 130;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(699, 373);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(rdbDetails);
			base.Controls.Add(rdbSummary);
			base.Controls.Add(clbForms);
			base.Controls.Add(label11);
			base.Controls.Add(cmbGodown);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label5);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label6);
			base.Controls.Add(label13);
			base.Controls.Add(cmbPCategory);
			base.Controls.Add(cmbAccount);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(cmbGSTRate);
			base.Controls.Add(cmbRoute);
			base.Controls.Add(label12);
			base.Controls.Add(label8);
			base.Controls.Add(cmbSpecification);
			base.Controls.Add(cmbGroup);
			base.Controls.Add(label1);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(cmbSupplier);
			base.Controls.Add(label7);
			base.Controls.Add(cmbBrand);
			base.Controls.Add(label2);
			base.Controls.Add(cmbProduct);
			base.Controls.Add(cmbSMan);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "InventorySalesRegisterView";
			base.Load += new System.EventHandler(InventorySalesRegisterView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(cmbSMan, 0);
			base.Controls.SetChildIndex(cmbProduct, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbBrand, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cmbSupplier, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cmbGroup, 0);
			base.Controls.SetChildIndex(cmbSpecification, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(cmbRoute, 0);
			base.Controls.SetChildIndex(cmbGSTRate, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(cmbAccount, 0);
			base.Controls.SetChildIndex(cmbPCategory, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(cmbGodown, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(clbForms, 0);
			base.Controls.SetChildIndex(rdbSummary, 0);
			base.Controls.SetChildIndex(rdbDetails, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
