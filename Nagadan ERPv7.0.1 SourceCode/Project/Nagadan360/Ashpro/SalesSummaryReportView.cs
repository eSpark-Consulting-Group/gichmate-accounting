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
	public class SalesSummaryReportView : BaseForm
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

		private string sBranchDetails = "";

		private string checkedForms;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbAccount;

		private Label label10;

		private Label label9;

		private Label label3;

		private Label label11;

		private AshComboBox cmbJE;

		private AshComboBox cmbPCategory;

		private AshComboBox cmbProduct;

		private AshComboBox cmbVehicle;

		private Label label7;

		private AshComboBox cmbSMan;

		private AshComboBox cmbRoute;

		private Label label8;

		private Label label12;

		private Label label6;

		private AshDateTimePicker dtmFrom;

		private Label label5;

		private AshDateTimePicker dtmTo;

		private CheckedListBox clForms;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string VoucherType { get; set; }

		public SalesSummaryReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void ReportShowMethod()
		{
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
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			int num11 = 0;
			if (VoucherType == "Sales")
			{
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
			}
			if (cmbAccount.Text != "")
			{
				text = " and Account.AccountName='" + cmbAccount.Text + "' ";
			}
			if (VoucherType == "Sales" || VoucherType == "Sales Return")
			{
				if (cmbRoute.Text != "")
				{
					text2 = ((!(VoucherType == "Sales")) ? (" and SalesRtnMaster.RouteId='" + cmbRoute.SelectedValue.ToInt32() + "' ") : (" and SalesEstimateMaster.RouteId='" + cmbRoute.SelectedValue.ToInt32() + "' "));
				}
				if (cmbSMan.Text != "")
				{
					text3 = ((!(VoucherType == "Sales")) ? (" and SalesRtnMaster.StaffId='" + cmbSMan.SelectedValue.ToInt32() + "' ") : (" and SalesEstimateMaster.StafId='" + cmbSMan.SelectedValue.ToInt32() + "' "));
				}
				if (cmbVehicle.Text != "")
				{
					text4 = ((!(VoucherType == "Sales")) ? (" and SalesRtnMaster.VehId='" + cmbVehicle.SelectedValue.ToInt32() + "'") : (" and SalesEstimateMaster.VehId='" + cmbVehicle.SelectedValue.ToInt32() + "'"));
				}
			}
			if (cmbPCategory.Text != "")
			{
				text5 = ((VoucherType == "Sales") ? (" and SalesEstimateMaster.PriceCategoryId='" + cmbPCategory.SelectedValue.ToInt32() + "' ") : ((VoucherType == "Sales Return") ? (" and SalesRtnMaster.PriceCategoryId='" + cmbPCategory.SelectedValue.ToInt32() + "' ") : ((!(VoucherType == "Purchase")) ? (" and PurRtnInvMaster.PriceCategoryId='" + cmbPCategory.SelectedValue.ToInt32() + "' ") : (" and PurInvMaster.PriceCategoryId='" + cmbPCategory.SelectedValue.ToInt32() + "' "))));
			}
			if (cmbJE.Text != "")
			{
				text6 = ((VoucherType == "Sales") ? (" and SalesEstimateMaster.JEId='" + cmbJE.SelectedValue.ToInt32() + "' ") : ((VoucherType == "Sales Return") ? (" and SalesRtnMaster.JeId='" + cmbJE.SelectedValue.ToInt32() + "' ") : ((!(VoucherType == "Purchase")) ? (" and PurRtnInvMaster.JEId='" + cmbJE.SelectedValue.ToInt32() + "' ") : (" and PurInvMaster.JEId='" + cmbJE.SelectedValue.ToInt32() + "' "))));
			}
			if (cmbProduct.Text != "")
			{
				text7 = ((VoucherType == "Sales") ? (" and SalesEstimateSettings.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ") : ((VoucherType == "Sales Return") ? (" and SlsRtnSettings.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ") : ((!(VoucherType == "Purchase")) ? (" and PurRtnInvSttings.ProId='" + cmbProduct.SelectedValue.ToInt32() + "' ") : (" and PurInvSettings.ProId='" + cmbProduct.SelectedValue.ToInt32() + "' "))));
			}
			num11 = 1;
			if (VoucherType == "Sales")
			{
				strQuery = "Select SalesEstimateMaster.SalesInvNo,SalesEstimateMaster.InvType,Convert(nvarchar,SalesEstimateMaster.SalesDate,103) as Date,Account.AccountName,Account.TinNo,Account.AddressOne,Account.CstNo,Account.MobileNo,Account.EMailOne,SalesEstimateMaster.PriceCategoryId,SalesEstimateMaster.RouteId,SalesEstimateMaster.VehId,SalesEstimateMaster.StafId,SalesEstimateMaster.SalesInvId, SalesEstimateMaster.TotalDiscount,SalesEstimateMaster.BillDiscount,SalesEstimateMaster.Frieght,SalesEstimateMaster.AditionalAmount,SalesEstimateMaster.IGSTAmount,SalesEstimateMaster.CGSTAmount,SalesEstimateMaster.SGSTAmount,SalesEstimateMaster.GrandDiscount,SalesEstimateMaster.JEId,Convert(nvarchar,SalesEstimateMaster.EntryDate,103) as EDate,SalesEstimateMaster.Remarks,Account.AddressTwo,Account.AddressThree From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId=Account.AccountId Where SalesEstimateMaster.SalesDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' and SalesEstimateMaster.InvType in (" + checkedForms + ")" + text + text2 + text3 + text4 + text5 + text6 + text7 + sBranchDetails + " order by SalesEstimateMaster.SalesDate,SalesEstimateMaster.SalesInvNo";
			}
			else if (VoucherType == "Sales Return")
			{
				strQuery = "Select SalesRtnMaster.SlRtnInvNo,SalesRtnMaster.SlRtnInvNo,Convert(nvarchar,SalesRtnMaster.SlsRtnDate,103) as Date,Account.AccountName,Account.TinNo,Account.AddressOne,Account.CstNo,Account.MobileNo,Account.EMailOne,SalesRtnMaster.PriceCategoryId,SalesRtnMaster.RouteId,SalesRtnMaster.VehId,SalesRtnMaster.StaffId,SalesRtnMaster.SlsRtrnId, SalesRtnMaster.TotalDiscount,SalesRtnMaster.BillDiscount,SalesRtnMaster.Frieght,SalesRtnMaster.AditionalAmnt,SalesRtnMaster.IGSTAmount,SalesRtnMaster.CGSTAmount,SalesRtnMaster.SGSTAmount,SalesRtnMaster.TotalAmount,SalesRtnMaster.JeId,Convert(nvarchar,SalesRtnMaster.EntryDate,103) as EDate,SalesRtnMaster.Remarks,Account.AddressTwo,Account.AddressThree From SalesRtnMaster Join Account on SalesRtnMaster.PartyAccountId=Account.AccountId Where SalesRtnMaster.SlsRtnDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' " + text + text2 + text3 + text4 + text5 + text6 + text7 + sBranchDetails + " order by SalesRtnMaster.SlsRtnDate,SalesRtnMaster.SlRtnInvNo";
			}
			else if (VoucherType == "Purchase")
			{
				strQuery = "Select PurInvMaster.PurInvNo,PurInvMaster.InvType,Convert(nvarchar,PurInvMaster.PurDate,103) as Date,Account.AccountName,Account.TinNo,Account.AddressOne,Account.CstNo,Account.MobileNo,Account.EMailOne,PurInvMaster.PriceCategoryId,PurInvMaster.PriceCategoryId,PurInvMaster.PriceCategoryId,PurInvMaster.PriceCategoryId,PurInvMaster.PurInvId, PurInvMaster.TotalDiscount,PurInvMaster.BillDisc,PurInvMaster.Frieght,PurInvMaster.AdditionalAmnt,PurInvMaster.IGSTAmount,PurInvMaster.CGSTAmount,PurInvMaster.SGSTAmount,PurInvMaster.TotalAmnt,PurInvMaster.JEId,Convert(nvarchar,PurInvMaster.EntryDate,103) as EDate,PurInvMaster.Remarks,Account.AddressTwo,Account.AddressThree From PurInvMaster Join Account on PurInvMaster.PartyAccId=Account.AccountId Where PurInvMaster.PurDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'" + text + text2 + text3 + text4 + text5 + text6 + text7 + sBranchDetails + " order by PurInvMaster.PurDate,PurInvMaster.PurInvNo";
			}
			else
			{
				strQuery = "Select PurRtnInvMaster.PurRtnInvNo,PurRtnInvMaster.PurRtnInvNo,Convert(nvarchar,PurRtnInvMaster.PurRtnDate,103) as Date,Account.AccountName,Account.TinNo,Account.AddressOne,Account.CstNo,Account.MobileNo,Account.EMailOne,PurRtnInvMaster.PriceCategoryId,PurRtnInvMaster.PriceCategoryId,PurRtnInvMaster.PriceCategoryId,PurRtnInvMaster.PriceCategoryId,PurRtnInvMaster.PurRtnInvId, PurRtnInvMaster.TotalDiscount,PurRtnInvMaster.BillDisc,PurRtnInvMaster.Frieght,PurRtnInvMaster.AdditionalAmnt,PurRtnInvMaster.IGSTAmount,PurRtnInvMaster.CGSTAmount,PurRtnInvMaster.SGSTAmount,PurRtnInvMaster.TotalAmnt,PurRtnInvMaster.JEId,Convert(nvarchar,PurRtnInvMaster.EntryDate,103) as EDate,PurRtnInvMaster.Remarks,Account.AddressTwo,Account.AddressThree From PurRtnInvMaster Join Account on PurRtnInvMaster.PartyAcId=Account.AccountId Where PurRtnInvMaster.PurRtnDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'" + text + text2 + text3 + text4 + text5 + text6 + text7 + sBranchDetails + " order by PurRtnInvMaster.PurRtnDate,PurRtnInvMaster.PurRtnInvNo";
			}
			try
			{
				string strID = "";
				string strID2 = "";
				string strID3 = "";
				string strID4 = "";
				string text9 = "";
				string text10 = "";
				string text11 = "";
				string strID5 = "";
				decimal num12 = default(decimal);
				decimal num13 = default(decimal);
				decimal num14 = default(decimal);
				decimal num15 = default(decimal);
				decimal num16 = default(decimal);
				decimal num17 = default(decimal);
				decimal num18 = default(decimal);
				decimal num19 = default(decimal);
				decimal num20 = default(decimal);
				decimal num21 = default(decimal);
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("PriceCategory", "PCategoryName", "PCategoryId", row[9].ToString(), out strID);
					if (VoucherType == "Sales" || VoucherType == "Sales Return")
					{
						AshproCommon.IdFindMethod("Root", "RootName", "RootId", row[10].ToString(), out strID2);
						AshproCommon.IdFindMethod("Vehicle", "VehName", "VanId", row[11].ToString(), out strID3);
						AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", row[12].ToString(), out strID4);
					}
					text9 = ((VoucherType == "Sales Return") ? "S Return" : ((!(VoucherType == "Purchase Return")) ? row[1].ToString() : "P Return"));
					text10 = row[13].ToString();
					text11 = ((VoucherType == "Sales") ? AshproCommon.ValueFindMethod("Select SUM(GrossAmnt)as Gross From SalesEstimateSettings Where SalesInvId='" + text10 + "'").ToString(CommonSettings.strCurencyFormat) : ((VoucherType == "Sales Return") ? AshproCommon.ValueFindMethod("Select SUM(GrossAmnt)as Gross From SlsRtnSettings Where SlsRtnId='" + text10 + "'").ToString(CommonSettings.strCurencyFormat) : ((!(VoucherType == "Purchase")) ? AshproCommon.ValueFindMethod("Select SUM(Gross)as Gross From PurRtnInvSttings Where PurRtnInvId='" + text10 + "'").ToString(CommonSettings.strCurencyFormat) : AshproCommon.ValueFindMethod("Select SUM(Gross)as Gross From PurInvSettings Where PurInvId='" + text10 + "'").ToString(CommonSettings.strCurencyFormat))));
					num12 = Convert.ToDecimal(text11);
					num += num12;
					if (row[14] != DBNull.Value)
					{
						num13 = Convert.ToDecimal(row[14].ToString());
					}
					num2 += num13;
					if (row[15] != DBNull.Value)
					{
						num14 = Convert.ToDecimal(row[15].ToString());
					}
					num3 += num14;
					if (row[16] != DBNull.Value)
					{
						num15 = Convert.ToDecimal(row[16].ToString());
					}
					num5 += num15;
					if (row[17] != DBNull.Value)
					{
						num16 = Convert.ToDecimal(row[17].ToString());
					}
					num4 += num16;
					if (!row[18].Equals(DBNull.Value))
					{
						num17 = Convert.ToDecimal(row[18].ToString());
						num6 += num17;
					}
					if (!row[19].Equals(DBNull.Value))
					{
						num18 = Convert.ToDecimal(row[19].ToString());
						num7 += num18;
					}
					if (!row[20].Equals(DBNull.Value))
					{
						num19 = Convert.ToDecimal(row[20].ToString());
						num8 += num19;
					}
					num20 = num17 + num18 + num19;
					num9 += num20;
					if (row[21] != DBNull.Value)
					{
						num21 = Convert.ToDecimal(row[21].ToString());
					}
					num10 += num21;
					AshproCommon.IdFindMethod("[User]", "UserName", "UserId", row[22].ToString(), out strID5);
					if (!(VoucherType == "Sales") && !(VoucherType == "Sales Return"))
					{
						Details.Rows.Add(num11, row[0], text9, row[2], row[3], row[4], row[5].ToString() + "\n" + row[25].ToString() + "\n" + row[26].ToString(), row[6], row[7], row[8], strID, num12.ToString("F"), num13.ToString("F"), num14.ToString("F"), num15.ToString("F"), num16.ToString("F"), num17.ToString("F"), num18.ToString("F"), num19.ToString("F"), num20.ToString("F"), "0.00", num21.ToString("F"), strID5, row[23], row[24]);
					}
					else
					{
						Details.Rows.Add(num11, row[0], text9, row[2], row[3], row[4], row[5].ToString() + "\n" + row[25].ToString() + "\n" + row[26].ToString(), row[6], row[7], row[8], strID, strID2, strID3, strID4, num12.ToString("F"), num13.ToString("F"), num14.ToString("F"), num15.ToString("F"), num16.ToString("F"), num17.ToString("F"), num18.ToString("F"), num19.ToString("F"), num20.ToString("F"), "0.00", num21.ToString("F"), strID5, row[23], row[24]);
					}
					num11++;
				}
				if (!(VoucherType == "Sales") && !(VoucherType == "Sales Return"))
				{
					Details.Rows.Add("", " ", " ", " ", "Total", " ", " ", " ", " ", " ", " ", num.ToString("F"), num2.ToString("F"), num3.ToString("F"), num5.ToString("F"), num4.ToString("F"), num6.ToString("F"), num7.ToString(), num8.ToString(), num9.ToString(), "0.00", num10.ToString(), "", "", "");
				}
				else
				{
					Details.Rows.Add("", " ", " ", " ", "Total", " ", " ", " ", " ", " ", " ", " ", " ", " ", num.ToString("F"), num2.ToString("F"), num3.ToString("F"), num5.ToString("F"), num4.ToString("F"), num6.ToString("F"), num7.ToString(), num8.ToString(), num9.ToString(), "0.00", num10.ToString(), "", "", "");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			Gridview = AniHelper.GetGridviewDatatable();
			if (VoucherType.Contains("Purchase"))
			{
				string[] headers = new string[25]
				{
					"SL", "Vchr No", "Vchr Type", "Date", "Customer Name", "Tax No", "Address", "State Code", "Mobile No", "Email",
					"P Category", "Gross", "Bill Discount", "Aditional Dis", "Freight", "Add Amount", "IGST", "CGST", "SGST", "GST Amont",
					"Cess Amnt", "Net Amount", "JE Name", "Entry Date", "Remarks"
				};
				Details = AniHelper.GetDataTable(headers);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(80, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(300, "L");
			}
			else
			{
				string[] headers2 = new string[28]
				{
					"SL", "Vchr No", "Vchr Type", "Date", "Customer Name", "Tax No", "Address", "State Code", "Mobile No", "Email",
					"P Category", "Route", "Vehicle", "Sales Man", "Gross", "Bill Discount", "Aditional Dis", "Freight", "Add Amount", "IGST",
					"CGST", "SGST", "GST Amont", "Cess Amnt", "Net Amount", "JE Name", "Entry Date", "Remarks"
				};
				Details = AniHelper.GetDataTable(headers2);
				Gridview.Rows.Add(50, "L");
				Gridview.Rows.Add(80, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(250, "L");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(200, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "L");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(120, "R");
				Gridview.Rows.Add(150, "L");
				Gridview.Rows.Add(100, "L");
				Gridview.Rows.Add(300, "L");
			}
			ReportShowMethod();
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(60, 60);
				LineData.Rows.Add(120, 120);
				LineData.Rows.Add(190, 190);
				LineData.Rows.Add(270, 270);
				LineData.Rows.Add(470, 470);
				LineData.Rows.Add(550, 550);
				LineData.Rows.Add(630, 630);
				LineData.Rows.Add(710, 710);
				LineData.Rows.Add(790, 790);
				LineData.Rows.Add(870, 870);
				LineData.Rows.Add(950, 950);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 8);
				Header.Rows.Add("Inv", 61);
				Header.Rows.Add("Type", 121);
				Header.Rows.Add("Date", 191);
				Header.Rows.Add("Customer", 271);
				Header.Rows.Add("Gross", 471);
				Header.Rows.Add("Bill Dis", 551);
				Header.Rows.Add("Add Dis", 631);
				Header.Rows.Add("Add Amnt", 711);
				Header.Rows.Add("Frieght", 791);
				Header.Rows.Add("Tax Amnt", 871);
				Header.Rows.Add("Net", 951);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "", 0);
				data.Rows.Add(1, 61, false, true, "", 165);
				data.Rows.Add(2, 121, false, true, "", 165);
				data.Rows.Add(3, 191, false, true, "", 165);
				data.Rows.Add(4, 271, true, true, "", 200);
				data.Rows.Add(14, 550, true, false, "Right", 0);
				data.Rows.Add(15, 630, true, false, "Right", 0);
				data.Rows.Add(16, 710, true, false, "Right", 0);
				data.Rows.Add(17, 790, true, false, "Right", 0);
				data.Rows.Add(18, 870, true, false, "Right", 0);
				data.Rows.Add(22, 950, true, false, "Right", 0);
				data.Rows.Add(24, 1040, true, false, "Right", 0);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			LineDataPDF = AniHelper.GetLineDatatablePDF();
			LineDataPDF.Rows.Add(8, 50, 830, 50);
			LineDataPDF.Rows.Add(8, 50, 8, 585);
			LineDataPDF.Rows.Add(8, 585, 830, 585);
			LineDataPDF.Rows.Add(830, 50, 830, 585);
			LineDataPDF.Rows.Add(35, 50, 35, 585);
			LineDataPDF.Rows.Add(70, 50, 70, 585);
			LineDataPDF.Rows.Add(120, 50, 120, 585);
			LineDataPDF.Rows.Add(180, 50, 180, 585);
			LineDataPDF.Rows.Add(360, 50, 360, 585);
			LineDataPDF.Rows.Add(430, 50, 430, 585);
			LineDataPDF.Rows.Add(495, 50, 495, 585);
			LineDataPDF.Rows.Add(560, 50, 560, 585);
			LineDataPDF.Rows.Add(625, 50, 625, 585);
			LineDataPDF.Rows.Add(690, 50, 690, 585);
			LineDataPDF.Rows.Add(760, 50, 760, 585);
			LineDataPDF.Rows.Add(830, 50, 830, 585);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("Inv No", 36);
			HeaderPDF.Rows.Add("Type", 71);
			HeaderPDF.Rows.Add("Date", 121);
			HeaderPDF.Rows.Add("Customer", 181);
			HeaderPDF.Rows.Add("Gross", 361);
			HeaderPDF.Rows.Add("Bill Dis", 431);
			HeaderPDF.Rows.Add("Add Dis", 496);
			HeaderPDF.Rows.Add("Frieght", 561);
			HeaderPDF.Rows.Add("Add Amnt", 626);
			HeaderPDF.Rows.Add("Tax Amnt", 691);
			HeaderPDF.Rows.Add("Net", 761);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 36, false, false, "", 0);
			dataPDF.Rows.Add(2, 71, false, false, "", 0);
			dataPDF.Rows.Add(3, 121, false, false, "", 0);
			dataPDF.Rows.Add(4, 181, true, false, "", 0);
			dataPDF.Rows.Add(14, 429, true, false, "Right", 0);
			dataPDF.Rows.Add(15, 494, true, false, "Right", 0);
			dataPDF.Rows.Add(16, 559, true, false, "Right", 0);
			dataPDF.Rows.Add(17, 624, true, false, "Right", 0);
			dataPDF.Rows.Add(18, 689, true, false, "Right", 0);
			dataPDF.Rows.Add(22, 759, true, false, "Right", 0);
			dataPDF.Rows.Add(24, 829, true, false, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cmbAccount.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = VoucherType + " Summary Report From " + dtmFrom.Text + " To " + dtmTo.Text;
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
			reportView.blLandscape = true;
			reportView.Show();
		}

		private void SalesSummaryReportView_Load(object sender, EventArgs e)
		{
			dtmFrom.Value = DateTime.Now.Date;
			AniHelper.FillAccount(cmbAccount);
			AniHelper.FillPCategory(cmbPCategory);
			AniHelper.FillEmployees(cmbJE);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillEmployees(cmbSMan);
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillVehicle(cmbVehicle);
			AniHelper.FillFormType(clForms);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			if (VoucherType == "Sales Return")
			{
				clForms.Visible = false;
				lblTitle.Text = "Sales Return Summary Report";
			}
			else if (VoucherType == "Purchase")
			{
				clForms.Visible = false;
				label8.Visible = false;
				cmbRoute.Visible = false;
				label7.Visible = false;
				cmbVehicle.Visible = false;
				label12.Visible = false;
				cmbSMan.Visible = false;
				lblTitle.Text = "Purchase Summary Report";
			}
			else if (VoucherType == "Purchase Return")
			{
				clForms.Visible = false;
				label8.Visible = false;
				cmbRoute.Visible = false;
				label7.Visible = false;
				cmbVehicle.Visible = false;
				label12.Visible = false;
				cmbSMan.Visible = false;
				lblTitle.Text = "Purchase Return Summary Report";
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
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbAccount = new AshUsercontrol.AshComboBox();
			label10 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			cmbJE = new AshUsercontrol.AshComboBox();
			cmbPCategory = new AshUsercontrol.AshComboBox();
			cmbProduct = new AshUsercontrol.AshComboBox();
			cmbVehicle = new AshUsercontrol.AshComboBox();
			label7 = new System.Windows.Forms.Label();
			cmbSMan = new AshUsercontrol.AshComboBox();
			cmbRoute = new AshUsercontrol.AshComboBox();
			label8 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label5 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			clForms = new System.Windows.Forms.CheckedListBox();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(665, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(222, 24);
			lblTitle.TabIndex = 132;
			lblTitle.Text = "Sales Summary Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(544, 237);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 128;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(622, 237);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 129;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAccount.EnterColor = System.Drawing.Color.LightPink;
			cmbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAccount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbAccount.FormattingEnabled = true;
			cmbAccount.LeaveColor = System.Drawing.Color.White;
			cmbAccount.Location = new System.Drawing.Point(145, 52);
			cmbAccount.Name = "cmbAccount";
			cmbAccount.Size = new System.Drawing.Size(230, 24);
			cmbAccount.TabIndex = 134;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label10.Location = new System.Drawing.Point(69, 55);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 16);
			label10.TabIndex = 133;
			label10.Text = "Account :";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(27, 82);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(116, 16);
			label9.TabIndex = 135;
			label9.Text = "Price Category :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(73, 136);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(70, 16);
			label3.TabIndex = 139;
			label3.Text = "Product :";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label11.Location = new System.Drawing.Point(52, 109);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(91, 16);
			label11.TabIndex = 137;
			label11.Text = "Entered By :";
			cmbJE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbJE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbJE.EnterColor = System.Drawing.Color.LightPink;
			cmbJE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbJE.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbJE.FormattingEnabled = true;
			cmbJE.LeaveColor = System.Drawing.Color.White;
			cmbJE.Location = new System.Drawing.Point(145, 106);
			cmbJE.Name = "cmbJE";
			cmbJE.Size = new System.Drawing.Size(230, 24);
			cmbJE.TabIndex = 138;
			cmbPCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPCategory.EnterColor = System.Drawing.Color.LightPink;
			cmbPCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPCategory.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbPCategory.FormattingEnabled = true;
			cmbPCategory.LeaveColor = System.Drawing.Color.White;
			cmbPCategory.Location = new System.Drawing.Point(145, 79);
			cmbPCategory.Name = "cmbPCategory";
			cmbPCategory.Size = new System.Drawing.Size(230, 24);
			cmbPCategory.TabIndex = 136;
			cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProduct.EnterColor = System.Drawing.Color.LightPink;
			cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProduct.FormattingEnabled = true;
			cmbProduct.LeaveColor = System.Drawing.Color.White;
			cmbProduct.Location = new System.Drawing.Point(145, 133);
			cmbProduct.Name = "cmbProduct";
			cmbProduct.Size = new System.Drawing.Size(230, 24);
			cmbProduct.TabIndex = 140;
			cmbVehicle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbVehicle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
			cmbVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbVehicle.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbVehicle.FormattingEnabled = true;
			cmbVehicle.LeaveColor = System.Drawing.Color.White;
			cmbVehicle.Location = new System.Drawing.Point(146, 214);
			cmbVehicle.Name = "cmbVehicle";
			cmbVehicle.Size = new System.Drawing.Size(229, 24);
			cmbVehicle.TabIndex = 146;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(39, 191);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(105, 16);
			label7.TabIndex = 143;
			label7.Text = "Salesman/Exi :";
			cmbSMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSMan.EnterColor = System.Drawing.Color.LightPink;
			cmbSMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSMan.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSMan.FormattingEnabled = true;
			cmbSMan.LeaveColor = System.Drawing.Color.White;
			cmbSMan.Location = new System.Drawing.Point(146, 188);
			cmbSMan.Name = "cmbSMan";
			cmbSMan.Size = new System.Drawing.Size(229, 24);
			cmbSMan.TabIndex = 144;
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Location = new System.Drawing.Point(146, 161);
			cmbRoute.Name = "cmbRoute";
			cmbRoute.Size = new System.Drawing.Size(229, 24);
			cmbRoute.TabIndex = 142;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(87, 164);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(57, 16);
			label8.TabIndex = 141;
			label8.Text = "Route :";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(78, 217);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(66, 16);
			label12.TabIndex = 145;
			label12.Text = "Vehicle :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(393, 57);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(87, 16);
			label6.TabIndex = 147;
			label6.Text = "Date From :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(482, 53);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(207, 23);
			dtmFrom.TabIndex = 148;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(408, 86);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 16);
			label5.TabIndex = 149;
			label5.Text = "Date To :";
			dtmTo.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(482, 82);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(207, 23);
			dtmTo.TabIndex = 150;
			clForms.AllowDrop = true;
			clForms.CheckOnClick = true;
			clForms.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			clForms.FormattingEnabled = true;
			clForms.HorizontalExtent = 1;
			clForms.HorizontalScrollbar = true;
			clForms.Location = new System.Drawing.Point(441, 118);
			clForms.Name = "clForms";
			clForms.Size = new System.Drawing.Size(230, 94);
			clForms.Sorted = true;
			clForms.TabIndex = 151;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(145, 242);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(230, 24);
			cmbBranch.TabIndex = 199;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(80, 245);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 198;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(701, 270);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(clForms);
			base.Controls.Add(label5);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label6);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(cmbVehicle);
			base.Controls.Add(label7);
			base.Controls.Add(cmbSMan);
			base.Controls.Add(cmbRoute);
			base.Controls.Add(label8);
			base.Controls.Add(label12);
			base.Controls.Add(cmbAccount);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(label3);
			base.Controls.Add(label11);
			base.Controls.Add(cmbJE);
			base.Controls.Add(cmbPCategory);
			base.Controls.Add(cmbProduct);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "SalesSummaryReportView";
			base.Load += new System.EventHandler(SalesSummaryReportView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(cmbProduct, 0);
			base.Controls.SetChildIndex(cmbPCategory, 0);
			base.Controls.SetChildIndex(cmbJE, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(cmbAccount, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(cmbRoute, 0);
			base.Controls.SetChildIndex(cmbSMan, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cmbVehicle, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(clForms, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
