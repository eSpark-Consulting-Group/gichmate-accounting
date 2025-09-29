using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using Microsoft.CSharp.RuntimeBinder;
using AccountsPro.SubProjects.AccountsLibraryFramework;
using AccountsPro.SubProjects.AccountsUserControls;
//using excel = Microsoft.Office.Interop.Excel;
//using OfficeOpenXml;
//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Drawing.Layout;
//using PdfSharp.Pdf;

namespace AccountsLibrary
{
	public class AniHelper
	{
		public static string LoggedInUser = "";
		private static Company company;

		private PrintDocument pDocu = new PrintDocument();

		private PrintDocument pDocument = new PrintDocument();

		private DataTable LineData = new DataTable();

		private DataTable Header = new DataTable();

		private DataTable data = new DataTable();

		private DataTable details = new DataTable();

		private string Title;

		private bool isPrinted = false;

		private bool isNotFirst = false;

		private int linesPrinted;

		private DataGridView dgReport;

		private static string[] ary = new string[3] { "Date", "Purchase", "Sales" };

		public static Func<decimal, decimal, decimal> GetInclusiveAmount = (decimal Taxable, decimal Vat) => Taxable - Taxable * 100m / (100m + Vat);

		public static Func<decimal, decimal, decimal> GetInclusiveBaseAmount = (decimal Taxable, decimal Vat) => Taxable * 100m / (100m + Vat);

		public AniHelper()
		{
			company = GetCompany();
			PrintController printController = new StandardPrintController();
			pDocu.PrintController = printController;
			pDocument.PrintController = printController;
			pDocument.PrintPage += PDocument_PrintPage;
			pDocu.PrintPage += PDocu_PrintPage;
		}

		public static void FillAGroup(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select distinct(AGroupId) as Id,GroupName as Name From AccountGroup Where Under <> '" + string.Empty + "'");
		}

		public static void FillExpenseGroup(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select distinct(AGroupId) as Id,GroupName as Name From AccountGroup Where Under = 'INDIRECT EXPENSES'");
		}

		public static void FillBarcode(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select Id,name as Name From BarcodeHDR");
		}

		public static void FillPrevilege(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select Id,Name From UserPrevilegeHdr");
		}

		public static void FillPCategory(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select PCategoryId as Id,PCategoryName as Name From PriceCategory");
		}

		public static void FillAccount(ComboBox cmb, bool isCashAccntIncluded = true)
		{
			if (isCashAccntIncluded)
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'  or AccountGroup.GroupName ='CASH IN HAND'");
			}
			else
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'");
			}
			cmb.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
		}

		public static void FillAccountFromParameter(ComboBox cmb, string sAGroup)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='" + sAGroup + "'");
		}

		public static void FillPartyAndSupplier(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'  or AccountGroup.GroupName ='SUPPLIERS/SUNDRY CREDITORS'");
		}

		public static void FillNotPartyAndSupplier(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName not in ('CUSTOMER/SUNDRY DEBTORS','SUPPLIERS/SUNDRY CREDITORS')");
		}

		public static void FillForPayment(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'  or AccountGroup.GroupName ='SUPPLIERS/SUNDRY CREDITORS' or AccountGroup.GroupName ='INDIRECT EXPENSES' or AccountGroup.GroupName ='CAPITAL ACCOUNT'");
		}

		public static void FillForReceipt(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'  or AccountGroup.GroupName ='SUPPLIERS/SUNDRY CREDITORS' or AccountGroup.GroupName ='INDIRECT INCOME' or AccountGroup.GroupName ='CAPITAL ACCOUNT' ");
		}

		public static void FillAccountForDebitNote(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName in('DISCOUNT RECEIVED','INDIRECT INCOME','DIRECT INCOME')");
		}

		public static void FillAccountForCreditNote(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName in('DISCOUNT GIVEN','INDIRECT EXPENSES','DIRECT EXPENSES')");
		}

		public static void FillAccountForPurchase(ComboBox cmb, bool isCashAccntIncluded = true)
		{
			if (isCashAccntIncluded)
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS'  or AccountGroup.GroupName ='CASH IN HAND'");
			}
			else
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS'");
			}
			if (isCashAccntIncluded)
			{
				cmb.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
			}
			else
			{
				cmb.SelectedIndex = 0;
			}
		}

		public static void FillProGroup(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select PGroupId as Id,PGroupName as Name From ProductGroup order by PGroupName");
		}

		public static void FillUnit(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select UnitId as Id,UnitName as Name From Unit");
		}

		public static void FillUser(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select UserId as Id,UserName as Name From [User]");
		}

		public static void FillBrand(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select BrandId as Id,BrandName as Name From Brand");
		}

		public static void FillCashAccount(ComboBox cmb, bool isLoadFull = false)
		{
			if (Utility.uIsSalesCounter && !Settings.Default.IsCashClosed && !isLoadFull)
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account where AccountId = '" + Utility.uCounterCashAccountId + "'");
			}
			else
			{
				AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account where AGroupId = 18");
			}
		}

		public static void FillCashAndBankAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account where AGroupId = 19 or AGroupId= 18");
			cmb.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
		}

		public static void FillTCategory(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select TCategoryId as Id,TCategoryName as Name From TaxCategory");
		}

		public static void FillBankAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CASH AT BANK'");
		}

		public static void FillCurrency(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select CurrencyID as Id,EnglishCurrencyName as Name From CurrencyInfo");
		}

		public static void FillSalesAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName = 'SALES ACCOUNT'");
		}

		public static void FillSalesRtnAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName = 'SALES RETURN ACCOUNT'");
		}

		public static void FillPurchaseAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName = 'PURCHASE ACCOUNT'");
		}

		public static void FillPurchaseRtnAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName = 'PURCHASE RETURN ACCOUNT'");
		}

		public static void FillSalesForms(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select Name as Id,Description as Name From MasterValue  Where MasterType = 'SalesFormType'");
		}

		public static void FillFormType(Control clForms)
		{
			if (clForms is ComboBox)
			{
				ComboBox comboBox = (ComboBox)clForms;
				comboBox.Items.Clear();
			}
			else
			{
				CheckedListBox checkedListBox = (CheckedListBox)clForms;
				checkedListBox.Items.Clear();
			}
			string text = "";
			text = ((!CommonSettings.blnGSTEnabled) ? "Select Description as Name From MasterValue Where MasterType='FormType'" : "Select Description as Name From MasterValue Where MasterType='GSTFormTypes'");
			foreach (DataRow row in AshproCommon.GetDataTable(text).Rows)
			{
				if (clForms is ComboBox)
				{
					ComboBox comboBox2 = (ComboBox)clForms;
					comboBox2.Items.Add(row[0]);
					comboBox2.SelectedIndex = 0;
				}
				else
				{
					CheckedListBox checkedListBox2 = (CheckedListBox)clForms;
					checkedListBox2.Items.Add(row[0]);
				}
			}
		}

		public static void FillEmployees(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select EmpID as Id,EmpName as Name From EmployeeDetails");
		}

		public static void FillRoute(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select RootId as Id,RootName as Name From Root");
		}

		public static void FillGodown(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select GodwnId as Id,GodownName as Name From Godown");
		}

		public static void FillVehicle(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select VanId as Id,VehName as Name From Vehicle");
		}

		public static void FillAllAccount(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select AccountId as Id,AccountName as Name From Account");
		}

		public static void FillProduct(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select ProductId as Id,ProductName as Name From Product");
		}

		public static void FillSpecification(ComboBox cmb)
		{
			cmb.ValueMember = "spec";
			cmb.DisplayMember = "spec";
			cmb.DataSource = AshproCommon.GetDataTable("Select distinct(Specification) as spec From Product");
		}

		public static void FillHSN(ComboBox cmb)
		{
			cmb.ValueMember = "hsn";
			cmb.DisplayMember = "hsn";
			cmb.DataSource = AshproCommon.GetDataTable("Select distinct(HSNNo) as hsn From Product");
		}

		public static void FillBranch(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select Id,Name From Branch");
		}

		public static void FillKOTPrinter(ComboBox cmb)
		{
			AshproCommon.FillCombo(cmb, "Select Id as Id,KOTName as Name From KOTPrinter");
		}

		public static async Task<string> GetValueForChart(string sAGroup, string dtm)
		{
			decimal dValue = default(decimal);
			DataTable dt = new DataTable();
			DBContext db = new DBContext();
			return await Task.Run(delegate
			{
				var entity = new
				{
					GroupId = db.AccountGroup().FirstOrDefault((AccountGroupEntity x) => x.GroupName == sAGroup).AGroupId,
					Date = DateTime.Now.Date
				};
				dt = AshproCommon.GetDataTableWithIdParameter_SP("spGetValueForChart", entity);
				foreach (DataRow row in dt.Rows)
				{
					dValue += row[0].ToString2().ToDecimal();
				}
				return (dValue >= 0m) ? (dValue.ToString(CommonSettings.strCurencyFormat) + "  Dr") : (Math.Abs(dValue).ToString(CommonSettings.strCurencyFormat) + "  Cr");
			});
		}

		public static async Task<string> GetValueForChart(string sAGroup, string dFrom, string dTo, string DebitOrCredit)
		{
			return await Task.Run(delegate
			{
				decimal num = default(decimal);
				string text = null;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select AccountId From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where GroupName='" + sAGroup + "'");
				foreach (DataRow row in dataTable.Rows)
				{
					num += AshproCommon.ValueFindMethod("Select SUM(" + DebitOrCredit + ") as Balance From TransactionLog Where AccountId='" + row[0].ToInt32() + "' and VchrDate between '" + dFrom + "' and '" + dTo + "'");
				}
				return (num >= 0m) ? (num.ToString(CommonSettings.strCurencyFormat) + "  Dr") : (Math.Abs(num).ToString(CommonSettings.strCurencyFormat) + "  Cr");
			});
		}

		public static async Task<DataTable> GetChartValuesForSalesAndPurchase()
		{
			DataTable sDetails = GetDataTable(ary);
			await Task.Run(delegate
			{
				var entity = new
				{
					FromDate = DateTime.Now.Date.AddDays(-30.0),
					ToDate = DateTime.Now.Date
				};
				sDetails = AshproCommon.GetDataTableWithIdParameter_SP("spGetValuesForSalesAndPurchase", entity);
				return sDetails;
			});
			return sDetails;
		}

		public static async Task<DataTable> GetChartValuesForIncome()
		{
			DataTable Details = GetDataTable(ary);
			int? iBranchId = null;
			if (CommonSettings.blnEnableMultiBranch)
			{
				iBranchId = Settings.Default.iBranchId;
			}
			await Task.Run(delegate
			{
				var entity = new
				{
					BranchId = iBranchId,
					FromDate = DateTime.Now.Date.AddDays(-30.0),
					ToDate = DateTime.Now.Date
				};
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spGetValuesForIncome", entity);
				return Details;
			});
			return Details;
		}

		public static async Task<DataTable> GetChartValuesForExpense()
		{
			DataTable Details = GetDataTable(ary);
			int? iBranchId = null;
			if (CommonSettings.blnEnableMultiBranch)
			{
				iBranchId = Settings.Default.iBranchId;
			}
			await Task.Run(delegate
			{
				var entity = new
				{
					BranchId = iBranchId,
					FromDate = DateTime.Now.Date.AddDays(-30.0),
					ToDate = DateTime.Now.Date
				};
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spGetValuesForExpenses", entity);
				return Details;
			});
			return Details;
		}

		public static async Task<DataTable> GetChartValuesForStock()
		{
			DataTable Details = GetDataTable(ary);
			await Task.Run(delegate
			{
				var entity = new { DateTime.Now.Date };
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spGetGroupWiseStockValue", entity);
				return Details;
			});
			return Details;
		}

		private static decimal SotckValueFindMethod(DataRow dr)
		{
			decimal result = default(decimal);
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable("Select ProductId,SellingPrice From Product Where PGroupId='" + dr[0].ToInt32() + "'");
			foreach (DataRow row in dataTable.Rows)
			{
				decimal num = row[1].ToString().ToDecimal();
				decimal num2 = AshproCommon.ValueFindMethod("Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog Where ProductId='" + row[0].ToInt32() + "'and VchrDate <='" + GetDate(DateTime.Now.Date) + "' ");
				result += num2 * num;
			}
			return result;
		}

		public static async Task<DataTable> GetChartValuesForGroupWiseSales()
		{
			DataTable Details = GetDataTable(ary);
			await Task.Run(delegate
			{
				var entity = new
				{
					FromDate = DateTime.Now.Date.AddDays(-7.0),
					ToDate = DateTime.Now.Date
				};
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spGetValuesForGroupWiseSales", entity);
				return Details;
			});
			return Details;
		}

		public static void ClearDateTimeMethod(Form form)
		{
			foreach (Control control in form.Controls)
			{
				if (control is AshDateTimePicker)
				{
					AshDateTimePicker ashDateTimePicker = (AshDateTimePicker)control;
					ashDateTimePicker.Value = DateTime.Now;
				}
			}
		}

		public static void ClearAshNumericLabels(Form form)
		{
			foreach (Control control in form.Controls)
			{
				if (control is AshNumericLabel)
				{
					AshNumericLabel ashNumericLabel = (AshNumericLabel)control;
					ashNumericLabel.Value = 0m;
				}
			}
		}

		public static void ClearMethod(Form form)
		{
			foreach (Control control in form.Controls)
			{
				control.Enabled = true;
				if (control is AshNumericLabel)
				{
					AshNumericLabel ashNumericLabel = (AshNumericLabel)control;
					ashNumericLabel.Value = 0m;
				}
				else if (control is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)control;
					if (ashTextBoxNormal.isNumberOnly)
					{
						ashTextBoxNormal.Text = "1";
					}
					else if (ashTextBoxNormal.isNumeric)
					{
						ashTextBoxNormal.Text = "0.00";
					}
					else
					{
						ashTextBoxNormal.Text = string.Empty;
					}
				}
				else if (control is Panel)
				{
					Panel panel = (Panel)control;
					PanelClearmethod(panel);
				}
			}
		}

		public static void ClearComboMethod(Form form)
		{
			foreach (Control control in form.Controls)
			{
				if (control is AshComboBox)
				{
					AshComboBox ashComboBox = (AshComboBox)control;
					ashComboBox.SelectedIndex = -1;
				}
			}
		}

		public static void PanelClearComboMethod(Control form)
		{
			foreach (Control control in form.Controls)
			{
				if (control is AshComboBox)
				{
					AshComboBox ashComboBox = (AshComboBox)control;
					ashComboBox.SelectedIndex = -1;
				}
			}
		}

		public static void PanelClearmethod(Control panel)
		{
			foreach (Control control in panel.Controls)
			{
				if (control is AshNumericLabel)
				{
					AshNumericLabel ashNumericLabel = (AshNumericLabel)control;
					ashNumericLabel.Value = 0m;
				}
				else if (control is AshTextBoxNormal)
				{
					AshTextBoxNormal ashTextBoxNormal = (AshTextBoxNormal)control;
					if (ashTextBoxNormal.isNumberOnly)
					{
						ashTextBoxNormal.Text = "1";
					}
					else if (ashTextBoxNormal.isNumeric)
					{
						ashTextBoxNormal.Value = 0m;
					}
					else
					{
						ashTextBoxNormal.Text = string.Empty;
					}
				}
				else if (control is Panel)
				{
					PanelClearmethod(control);
				}
			}
		}

		public void PrintMethod(DataTable _data, DataTable _Header, DataTable _LineData, string sTitle, DataGridView _dgReport, bool blLanscape)
		{
			data = _data;
			Header = _Header;
			LineData = _LineData;
			Title = sTitle;
			dgReport = _dgReport;
			if (data.Rows.Count > 0)
			{
				isPrinted = false;
				isNotFirst = false;
				linesPrinted = 0;
				if (blLanscape)
				{
					pDocu.DefaultPageSettings.Landscape = true;
				}
				pDocu.Print();
			}
			else
			{
				//Messages.ErrorMessage("There is no data to print in the table");
				MessageBox.Show("There is no data to print in the table");
			}
		}

		public void PrintMethod(DataTable _data, DataTable _Header, DataTable _LineData, string sTitle, DataTable _Details, bool blLanscape)
		{
			data = _data;
			Header = _Header;
			LineData = _LineData;
			Title = sTitle;
			details = _Details;
			if (data.Rows.Count > 0)
			{
				isPrinted = false;
				isNotFirst = false;
				linesPrinted = 0;
				if (blLanscape)
				{
					pDocument.DefaultPageSettings.Landscape = true;
				}
				pDocument.Print();
			}
			else
			{
                MessageBox.Show("There is no data to print in the table");
			}
		}

		public static DataTable GetLineDatatable()
		{
			string[] headers = new string[2] { "X", "Y" };
			return GetDataTable(headers);
		}

		public static DataTable GetGridviewDatatable()
		{
			string[] headers = new string[2] { "Width", "Alignment" };
			return GetDataTable(headers);
		}

		public static DataTable GetHeadDatatable()
		{
			string[] headers = new string[2] { "Header", "X" };
			return GetDataTable(headers);
		}

		public static DataTable GetDetailsDatatable()
		{
			string[] headers = new string[6] { "Data", "X", "Bold", "RectancleF", "Alignment", "Length" };
			return GetDataTable(headers);
		}

		public static DataTable GetLineDatatablePDF()
		{
			string[] headers = new string[4] { "A", "B", "C", "D" };
			return GetDataTable(headers);
		}

		public void MailSendMethod(DataTable _data, DataTable _Header, DataTable _LineData, string sTitle, DataGridView _dgReport, bool blLandscape = false)
		{
		//	string empty = string.Empty;
		//	data = _data;
		//	Header = _Header;
		//	LineData = _LineData;
		//	Title = sTitle;
		//	dgReport = _dgReport;
		//	if (dgReport.Rows.Count > 0)
		//	{
		//		isNotFirst = false;
		//		isPrinted = false;
		//		linesPrinted = 0;
		//		empty = PDFGenerateMethod(blLandscape);
		//		string text = company.EmailId;
		//		if (text == null || text == "")
		//		{
		//			MyMsgBox myMsgBox = new MyMsgBox();
		//			myMsgBox.Passvalue = "Mail Address";
		//			myMsgBox.ShowDialog();
		//			text = myMsgBox.Passvalue;
		//		}
		//		string body = "Dear Sir/Madam,\n      \n" + Title + "\n       \n        \nThanks and Regards\nAccounts Team\n" + company.InstituteName;
		//		try
		//		{
		//			using MailMessage mailMessage = new MailMessage(CommonSettings.strEmailAddress, text, Title, body);
		//			mailMessage.Attachments.Add(new Attachment(empty));
		//			SmtpClient smtpClient = new SmtpClient(CommonSettings.strClientName);
		//			smtpClient.Port = CommonSettings.strClientPort.ToInt32();
		//			smtpClient.Credentials = new NetworkCredential(CommonSettings.strUserName, CommonSettings.strPassword);
		//			smtpClient.EnableSsl = true;
		//			smtpClient.Send(mailMessage);
		//			return;
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show(ex.Message);
		//			return;
		//		}
		//	}
			MessageBox.Show("There is no data in the table to send mail.");
		}

		//private string PDFGenerateMethod(bool blLandscape)
		//{
		//	string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + Utility.uManufacturer + "\\";
		//	if (!Directory.Exists(text))
		//	{
		//		Directory.CreateDirectory(text);
		//	}
		//	try
		//	{
		//		PdfDocument pdfDocument = new PdfDocument();
		//		pdfDocument.Info.Title = "Report";
		//		PdfPage pdfPage = pdfDocument.AddPage();
		//		if (blLandscape)
		//		{
		//			pdfPage.Orientation = PageOrientation.Landscape;
		//		}
		//		pdfPage.Size = PageSize.A4;
		//		Convert.ToInt32(pdfPage.Width.Value);
		//		Convert.ToInt32(pdfPage.Height.Value);
		//		XGraphics xGraphics = XGraphics.FromPdfPage(pdfPage);
		//		new XTextFormatter(xGraphics);
		//		XFont font = new XFont("Courier New", 9.0, XFontStyleEx.Bold);
		//		XFont font2 = new XFont("Courier New", 8.0, XFontStyleEx.Bold);
		//		XFont font3 = new XFont("Courier New", 9.0, XFontStyleEx.Regular);
		//		XFont font4 = new XFont("Courier New", 12.0, XFontStyleEx.Bold | XFontStyleEx.Underline);
		//		XPen pen = new XPen(XColors.Black, 0.75);
		//		XStringFormat xStringFormat = new XStringFormat();
		//		XStringFormat xStringFormat2 = new XStringFormat();
		//		xStringFormat.Alignment = XStringAlignment.Center;
		//		xStringFormat2.Alignment = XStringAlignment.Far;
		//		//xStringFormat2.FormatFlags = XStringFormatFlags.MeasureTrailingSpaces;
		//		int num = 5;
		//		if (!isPrinted)
		//		{
		//			if (blLandscape)
		//			{
		//				xGraphics.DrawLine(pen, 8, 70, 830, 70);
		//			}
		//			else
		//			{
		//				xGraphics.DrawLine(pen, 8, 70, 585, 70);
		//			}
		//			foreach (DataRow row in LineData.Rows)
		//			{
		//				xGraphics.DrawLine(pen, row[0].ToInt32(), row[1].ToInt32(), row[2].ToInt32(), row[3].ToInt32());
		//			}
		//			xGraphics.DrawString(company.InstituteName, font4, XBrushes.Black, new XRect(250.0, num, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			num += 35;
		//			xGraphics.DrawString(Title, font, XBrushes.BlueViolet, new XRect(5.0, num, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			num += 15;
		//			foreach (DataRow row2 in Header.Rows)
		//			{
		//				xGraphics.DrawString(row2[0].ToString(), font, XBrushes.Black, new XRect(row2[1].ToInt32(), num, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			}
		//			num += 20;
		//			isPrinted = true;
		//		}
		//		while (linesPrinted < dgReport.Rows.Count)
		//		{
		//			foreach (DataRow row3 in data.Rows)
		//			{
		//				string text2 = dgReport.Rows[linesPrinted].Cells[row3[0].ToInt32()].Value.ToString();
		//				if (row3[3].ToString().ToBool() && row3[5].ToInt32() > 0 && text2.Length > row3[5].ToInt32())
		//				{
		//					text2 = text2.Remove(row3[5].ToInt32());
		//				}
		//				if (dgReport.Rows[linesPrinted].Cells[0].Value.ToString() == "" && row3[2].ToString().ToBool())
		//				{
		//					if (row3[4].ToString() == "Right")
		//					{
		//						xGraphics.DrawString(text2, font2, XBrushes.Red, row3[1].ToInt32(), num, xStringFormat2);
		//					}
		//					else
		//					{
		//						xGraphics.DrawString(text2, font, XBrushes.Red, new XRect(row3[1].ToInt32(), num, row3[5].ToInt32(), 15.0), XStringFormats.TopLeft);
		//					}
		//				}
		//				else if (row3[4].ToString() == "Right")
		//				{
		//					xGraphics.DrawString(text2, font2, XBrushes.Black, row3[1].ToInt32(), num, xStringFormat2);
		//				}
		//				else
		//				{
		//					xGraphics.DrawString(text2, font3, XBrushes.Black, new XRect(row3[1].ToInt32(), num, row3[5].ToInt32(), 15.0), XStringFormats.TopLeft);
		//				}
		//			}
		//			linesPrinted++;
		//			num += 10;
		//			if (blLandscape)
		//			{
		//				if (num + 10 <= 585)
		//				{
		//					continue;
		//				}
		//				num = 55;
		//				xGraphics.Dispose();
		//				pdfPage = pdfDocument.AddPage();
		//				pdfPage.Orientation = PageOrientation.Landscape;
		//				pdfPage.Size = PageSize.A4;
		//				xGraphics = XGraphics.FromPdfPage(pdfPage);
		//				foreach (DataRow row4 in LineData.Rows)
		//				{
		//					xGraphics.DrawLine(pen, row4[0].ToInt32(), row4[1].ToInt32(), row4[2].ToInt32(), row4[3].ToInt32());
		//				}
		//			}
		//			else
		//			{
		//				if (num + 10 <= 800)
		//				{
		//					continue;
		//				}
		//				num = 55;
		//				xGraphics.Dispose();
		//				pdfPage = pdfDocument.AddPage();
		//				xGraphics = XGraphics.FromPdfPage(pdfPage);
		//				foreach (DataRow row5 in LineData.Rows)
		//				{
		//					xGraphics.DrawLine(pen, row5[0].ToInt32(), row5[1].ToInt32(), row5[2].ToInt32(), row5[3].ToInt32());
		//				}
		//			}
		//		}
		//		isNotFirst = false;
		//		linesPrinted = 0;
		//		text += "Report.pdf";
		//		pdfDocument.Save(text);
		//		Process.Start(text);
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//	return text;
		//}

		//public static void ExportToExcelMethod(DataGridView entDetails, string sTitle)
		//{
		//	//Discarded unreachable code: IL_0061, IL_0070, IL_0080, IL_00ae, IL_00e7, IL_00fd, IL_012c, IL_0183, IL_01bc, IL_01d2, IL_0201, IL_0240, IL_0274, IL_02ad, IL_02c3, IL_02f2, IL_032e, IL_0342, IL_040b, IL_0417
		//	//Error decoding local variables: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.
		//	//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_002a: Expected O, but got Unknown
		//	//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0061: Incompatible stack heights: 1 vs 0
		//	//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_007b: Expected O, but got Unknown
		//	//IL_008c: Expected O, but got Unknown
		//	//IL_0141: Expected O, but got Unknown
		//	//IL_0147: Expected I4, but got Unknown
		//	//IL_014e: Expected I4, but got Unknown
		//	//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0169: Expected O, but got Unknown
		//	//IL_016f: Expected I4, but got Unknown
		//	//IL_0176: Expected I4, but got Unknown
		//	//IL_0216: Expected O, but got Unknown
		//	//IL_021c: Expected I4, but got Unknown
		//	//IL_0223: Expected I4, but got Unknown
		//	//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0246: Expected O, but got Unknown
		//	//IL_024c: Expected I4, but got Unknown
		//	//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0256: Expected I4, but got Unknown
		//	//IL_0263: Expected I4, but got Unknown
		//	//IL_0307: Expected O, but got Unknown
		//	//IL_030d: Expected I4, but got Unknown
		//	//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0317: Expected I4, but got Unknown
		//	//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_033b: Invalid comparison between Unknown and I4
		//	//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0360: Expected O, but got Unknown
		//	//IL_036e: Expected O, but got Unknown
		//	//IL_037f: Expected O, but got Unknown
		//	//IL_038c: Expected O, but got Unknown
		//	//IL_0392: Expected I4, but got Unknown
		//	//IL_0399: Expected I4, but got Unknown
		//	//IL_03a0: Expected O, but got Unknown
		//	//IL_03a7: Expected O, but got Unknown
		//	//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_03c8: Expected O, but got Unknown
		//	//IL_03cf: Expected O, but got Unknown
		//	//IL_03e0: Expected O, but got Unknown
		//	//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_03ef: Expected O, but got Unknown
		//	//IL_03f9: Expected O, but got Unknown
		//	//IL_040a: Expected O, but got Unknown
		//	//IL_0412: Expected O, but got Unknown
		//	company = GetCompany();
		//	try
		//	{
		//		//Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		//		///*Error near IL_0023: stloc 0 (out-of-bounds)*/;
		//		//_ = ((_Application)/*Error near IL_0024: ldloc 0 (out-of-bounds)*/).Workbooks;
		//		//((Workbooks)XlSheetType.xlWorksheet).Add();
		//		//if ((int)_003C_003Eo__78._003C_003Ep__0 == 0)
		//		//{
		//		//	Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(_003F), typeof(AniHelper));
		//		//	/*Error near IL_005c: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//		//}
		//		//_ = _003C_003Eo__78._003C_003Ep__0;
		//		///*Error near IL_006b: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
			
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		//public static void ExportToExcelMethodLiv(ListView livDetails, string sTitle)
		//{
		//	//Discarded unreachable code: IL_0061, IL_0070, IL_0080, IL_00ae, IL_00e7, IL_00fd, IL_012c, IL_0183, IL_01bc, IL_01d2, IL_0201, IL_0240, IL_0274, IL_02ad, IL_02c3, IL_02f2, IL_032e, IL_0342, IL_041e
		//	//Error decoding local variables: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.
		//	//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_002a: Expected O, but got Unknown
		//	//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0061: Incompatible stack heights: 1 vs 0
		//	//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_007b: Expected O, but got Unknown
		//	//IL_008c: Expected O, but got Unknown
		//	//IL_0141: Expected O, but got Unknown
		//	//IL_0147: Expected I4, but got Unknown
		//	//IL_014e: Expected I4, but got Unknown
		//	//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0169: Expected O, but got Unknown
		//	//IL_016f: Expected I4, but got Unknown
		//	//IL_0176: Expected I4, but got Unknown
		//	//IL_0216: Expected O, but got Unknown
		//	//IL_021c: Expected I4, but got Unknown
		//	//IL_0223: Expected I4, but got Unknown
		//	//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0246: Expected O, but got Unknown
		//	//IL_024c: Expected I4, but got Unknown
		//	//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0256: Expected I4, but got Unknown
		//	//IL_0263: Expected I4, but got Unknown
		//	//IL_0307: Expected O, but got Unknown
		//	//IL_030d: Expected I4, but got Unknown
		//	//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0317: Expected I4, but got Unknown
		//	//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_033b: Invalid comparison between Unknown and I4
		//	//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0360: Expected O, but got Unknown
		//	//IL_036d: Expected O, but got Unknown
		//	//IL_0373: Expected I4, but got Unknown
		//	//IL_037a: Expected I4, but got Unknown
		//	//IL_0381: Expected O, but got Unknown
		//	//IL_0392: Expected O, but got Unknown
		//	//IL_03a3: Expected O, but got Unknown
		//	//IL_03b0: Expected O, but got Unknown
		//	//IL_03b6: Expected I4, but got Unknown
		//	//IL_03bd: Expected I4, but got Unknown
		//	//IL_03c4: Expected O, but got Unknown
		//	//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_03db: Expected O, but got Unknown
		//	//IL_03e2: Expected O, but got Unknown
		//	//IL_03f3: Expected O, but got Unknown
		//	//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0402: Expected O, but got Unknown
		//	//IL_040c: Expected O, but got Unknown
		//	//IL_041d: Expected O, but got Unknown
		//	//IL_0425: Expected O, but got Unknown
		//	//IL_0430: Expected O, but got Unknown
		//	company = GetCompany();
		//	try
		//	{
		//		Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		//		/*Error near IL_0023: stloc 0 (out-of-bounds)*/;
		//		_ = ((_Application)/*Error near IL_0024: ldloc 0 (out-of-bounds)*/).Workbooks;
		//		((Workbooks)XlSheetType.xlWorksheet).Add();
		//		if ((int)_003C_003Eo__79._003C_003Ep__0 == 0)
		//		{
		//			Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(_003F), typeof(AniHelper));
		//			/*Error near IL_005c: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//		}
		//		_ = _003C_003Eo__79._003C_003Ep__0;
		//		/*Error near IL_006b: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//	}
		//	catch (Exception)
		//	{
		//		/*Error near IL_0427: stloc 12 (out-of-bounds)*/;
		//		MessageBox.Show(((Exception)/*Error near IL_0429: ldloc 12 (out-of-bounds)*/).Message);
		//	}
		//}

		//public static void DataSetToExcel(DataSet dataSet, string sFileName, bool isExport = true)
		//{
		//	try
		//	{
		//		using (ExcelPackage excelPackage = new ExcelPackage())
		//		{
		//			foreach (DataTable table in dataSet.Tables)
		//			{
		//				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
		//				excelWorksheet.Cells["A1"].LoadFromDataTable(table, PrintHeaders: true);
		//			}
		//			excelPackage.SaveAs(new FileInfo(sFileName));
		//		}
		//		if (isExport)
		//		{
		//			MessageBox.Show("Successfully Exported.", Utility.uManufacturer);
		//		}
		//		Process.Start(sFileName);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		private static string GetString(object obj)
		{
			try
			{
				if (obj != null)
				{
					return obj.ToString();
				}
				return string.Empty;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		//public static DataTable WorksheetToTable(string fullFielPath)
		//{
		//	try
		//	{
		//		FileInfo newFile = new FileInfo(fullFielPath);
		//		ExcelPackage excelPackage = new ExcelPackage(newFile);
		//		ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
		//		return WorksheetToTable(worksheet);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		//public static DataTable WorksheetToTable(ExcelWorksheet worksheet)
		//{
		//	int row = worksheet.Dimension.End.Row;
		//	int column = worksheet.Dimension.End.Column;
		//	DataTable dataTable = new DataTable(worksheet.Name);
		//	DataRow dataRow = null;
		//	for (int i = 1; i <= row; i++)
		//	{
		//		if (i > 1)
		//		{
		//			dataRow = dataTable.Rows.Add();
		//		}
		//		for (int j = 1; j <= column; j++)
		//		{
		//			if (i == 1)
		//			{
		//				dataTable.Columns.Add(GetString(worksheet.Cells[i, j].Value));
		//			}
		//			else
		//			{
		//				dataRow[j - 1] = GetString(worksheet.Cells[i, j].Value);
		//			}
		//		}
		//	}
		//	return dataTable;
		//}

		//public static DataTable ImportFromExelMethod(string sFilepath)
		//{
		//	try
		//	{
		//		string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sFilepath + ";Extended Properties=Excel 12.0;";
		//		OleDbConnection selectConnection = new OleDbConnection(connectionString);
		//		OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("Select * from [Sheet1$]", selectConnection);
		//		DataTable dataTable = new DataTable();
		//		oleDbDataAdapter.Fill(dataTable);
		//		return dataTable;
		//	}
		//	catch (Exception ex)
		//	{
		//		Messages.ErrorMessage(ex.Message);
		//		return null;
		//	}
		//}

		//public static void CustomerSerchMethod(ComboBox comb, bool isCustomer = true)
		//{
		//	DBContext dBContext = new DBContext();
		//	string sName = "Customer Name";
		//	string text = "CUSTOMER/SUNDRY DEBTORS";
		//	if (!isCustomer)
		//	{
		//		sName = "Supplier Name";
		//		text = "SUPPLIERS/SUNDRY CREDITORS";
		//	}
		//	MasterSerchForm masterSerchForm = new MasterSerchForm("ID", sName, "Phone Number", _isFromTransForm: true);
		//	DataTable dataTable = new DataTable();
		//	dataTable = AshproCommon.GetDataTable("select AccountId as id,AccountName as name,PhoneOne as description From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where  GroupName = '" + text + "'");
		//	masterSerchForm.Masters = dBContext.Masters(dataTable);
		//	if (masterSerchForm.ShowDialog() == DialogResult.OK)
		//	{
		//		comb.SelectedValue = masterSerchForm.id;
		//	}
		//}

		public static void PropertyFindMethod(object myObject, ComboBox cmb)
		{
			Type type = myObject.GetType();
			PropertyInfo[] properties = type.GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				try
				{
					if (!cmb.Items.Contains(propertyInfo.Name))
					{
						cmb.Items.Add(propertyInfo.Name);
					}
				}
				catch
				{
				}
			}
		}

		public static string GenerateBase64(string sellerName, string taxNumber, string invoiceTimeStamp, string invoiceTotal, string taxAmount)
		{
			List<byte[]> list = new List<byte[]>
			{
				Encoding.UTF8.GetBytes(sellerName),
				Encoding.UTF8.GetBytes(taxNumber),
				Encoding.UTF8.GetBytes(invoiceTimeStamp),
				Encoding.UTF8.GetBytes(invoiceTotal),
				Encoding.UTF8.GetBytes(taxAmount)
			};
			byte[] inArray;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					uint num = 1u;
					for (int i = 0; i < list.Count; i++)
					{
						binaryWriter.Write(Convert.ToByte(num++));
						binaryWriter.Write(Convert.ToByte(list[i].Length));
						binaryWriter.Write(list[i]);
					}
					binaryWriter.Flush();
				}
				inArray = memoryStream.ToArray();
			}
			return Convert.ToBase64String(inArray);
		}

		public static void Display(string firstValue, string secodValue)
		{
			try
			{
				using SerialPort serialPort = new SerialPort(Settings.Default.sCom, 9600, Parity.None, 8, StopBits.One);
				serialPort.Open();
				if (firstValue != string.Empty)
				{
					serialPort.Write(new byte[1] { 12 }, 0, 1);
					serialPort.Write(firstValue);
				}
				if (secodValue != string.Empty)
				{
					serialPort.Write(new byte[2] { 10, 13 }, 0, 2);
					serialPort.Write(secodValue);
				}
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				//Messages.WarningMessage("Port Not Connected!");
				MessageBox.Show("Port Not Connected!");

            }
		}

		public static string PWordGeneration()
		{
			string result = "0";
			try
			{
				int year = DateTime.Now.Year;
				int month = DateTime.Now.Month;
				int day = DateTime.Now.Day;
				result = (year + day + month).ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return result;
		}

		public static string GetLocalName(bool isTransilitrate, bool isTranslate, string sName, string sValue)
		{
			string result = sValue;
			if (isTranslate)
			{
				GoogleTrans googleTrans = new GoogleTrans();
				result = googleTrans.Translate(sName, "en", "ar");
			}
			if (isTransilitrate)
			{
				result = ClsArabic.Transliterated2Arabic(sName);
			}
			return result;
		}

		public static void ImageLoadMethod(PictureBox pictureBox)
		{
			try
			{
				using OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Open Image";
				openFileDialog.Filter = "All Picture Files(*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg;*.Jpeg)|*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg;*.Jpeg";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					pictureBox.Image = Image.FromFile(openFileDialog.FileName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static string GetDateDifferenceDays(DateTime fDate, DateTime sDate)
		{
			string text = (fDate.Date - sDate.Date).ToString();
			int length = text.Length;
			if (length > 9)
			{
				length -= 9;
				text = text.Remove(length);
			}
			else
			{
				text = "0";
			}
			if (!(text == "1") && !(text == "0"))
			{
				return text + " Days";
			}
			return text + " Day";
		}

		public static int GetDateDifferences(DateTime fDate, DateTime sDate)
		{
			string text = (fDate.Date - sDate.Date).ToString();
			int length = text.Length;
			if (length > 9)
			{
				length -= 9;
				text = text.Remove(length);
			}
			else
			{
				text = "0";
			}
			return text.ToInt32();
		}

		public static decimal ValueFindMethodForFinancialStatement(string strQuery)
		{
			decimal? num = default(decimal);
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				if (row[0] != DBNull.Value)
				{
					num += (decimal?)Convert.ToDecimal(row[0].ToString());
				}
			}
			return num.Value;
		}

		public static decimal AccountBalanceMethod(int iTypeIndex, string sType, DateTime dtFrom, DateTime dtTo, int? iBranchId, string sGroup = null, string sUnder = null)
		{
			if (iBranchId.HasValue)
			{
				_ = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			return AshproCommon.GetDecimalDataWithParameter_SP("spProfitAndLossAccountBalance", new
			{
				Type = iTypeIndex,
				AccountType = sType,
				GroupName = sGroup,
				BranchId = iBranchId,
				FromDate = GetDate(dtFrom),
				ToDate = GetDate(dtTo),
				Under = sUnder
			});
		}

		private static AutoCompleteStringCollection CollDataAddMeethod(bool iscustomer = false)
		{
			string query = "Select ProductName From Product";
			if (iscustomer)
			{
				query = "Select AccountName From Account Where AGroupId=21";
			}
			AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
			List<string> list = new List<string>();
			list = AshproCommon.GetStringListMethod(query);
			autoCompleteStringCollection.AddRange(Enumerable.ToArray(list));
			return autoCompleteStringCollection;
		}

		public static async Task AutoCompleteMethod(TextBox txtsearchitemname, bool iscustomer = false)
		{
			try
			{
				AutoCompleteStringCollection coll = await Task.Run(() => CollDataAddMeethod(iscustomer));
				txtsearchitemname.AutoCompleteMode = AutoCompleteMode.Suggest;
				txtsearchitemname.AutoCompleteSource = AutoCompleteSource.CustomSource;
				txtsearchitemname.AutoCompleteCustomSource = coll;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
                //Messages.ErrorMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }
		}

		public static void SerialNumberMethod(int iSN)
		{
			DBContext dBContext = new DBContext();
			MasterValue masterValue = dBContext.MasterValues(new
			{
				MasterType = "SN",
				Name = Utility.uTerminal
			}).FirstOrDefault();
			if (masterValue == null)
			{
				AshproCommon.InsertToDatabaseObj(new MasterValue
				{
					MasterType = "SN",
					Name = Utility.uTerminal,
					Description = iSN.ToString()
				});
			}
			else
			{
				masterValue.Description = iSN.ToString();
				AshproCommon.UpdateToDatabaseObj(masterValue, "MasterValue", "Id", masterValue.Id.toInt32());
			}
		}

		public static void IconChangeMethod(Form frm)
		{
			if (Utility.uManufacturer == "AccountsLibrary")
			{
				frm.Icon = Resources.ico_logo1;
			}
			else
			{
				frm.Icon = Resources.ico_logo;
			}
		}

		public static void AddPaymentPrint(List<InvoiceAdjustment> paymentPrint, string sName, string dValue, string sRLabel)
		{
			InvoiceAdjustment item = new InvoiceAdjustment
			{
				Name = sName,
				Value = dValue,
				RightLabel = sRLabel
			};
			paymentPrint.Add(item);
		}

		public static string GetPriceCode(string dValue)
		{
			string text = null;
			DBContext dBContext = new DBContext();
			char[] array = dValue.ToDecimal().ToString("G29").ToCharArray();
			char[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				char c = array2[i];
				text = ((!(c.ToString() != ".")) ? (text + ".") : (text + dBContext.RateFormats(new
				{
					Name = c.ToString()
				}).FirstOrDefault().Value));
			}
			return text;
		}

		public static int GetApplicationVersion()
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			_ = versionInfo.FileMajorPart + "." + versionInfo.FileMinorPart + "." + versionInfo.FileBuildPart;
			string text = versionInfo.FileMajorPart.ToString();
			string text2 = versionInfo.FileMinorPart.ToString();
			string text3 = versionInfo.FileBuildPart.ToString();
			return (text + text2 + text3).ToInt32();
		}

		public static string GetAmountInWords(int inputNumber)
		{
			int num = inputNumber;
			if (num == 0)
			{
				return "Zero";
			}
			int[] array = new int[4];
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (num < 0)
			{
				stringBuilder.Append("Minus ");
				num = -num;
			}
			string[] array2 = new string[10] { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
			string[] array3 = new string[10] { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
			string[] array4 = new string[8] { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
			string[] array5 = new string[3] { "Thousand ", "Lakh ", "Crore " };
			array[0] = num % 1000;
			array[1] = num / 1000;
			array[2] = num / 100000;
			array[1] -= 100 * array[2];
			array[3] = num / 10000000;
			array[2] -= 100 * array[3];
			int num3 = 3;
			while (num3 > 0)
			{
				if (array[num3] == 0)
				{
					num3--;
					continue;
				}
				num2 = num3;
				break;
			}
			for (int num4 = num2; num4 >= 0; num4--)
			{
				if (array[num4] != 0)
				{
					int num5 = array[num4] % 10;
					int num6 = array[num4] / 10;
					int num7 = array[num4] / 100;
					num6 -= 10 * num7;
					if (num7 > 0)
					{
						stringBuilder.Append(array2[num7] + "Hundred ");
					}
					if (num5 > 0 || num6 > 0)
					{
						if (num7 > 0 || num4 == 0)
						{
							stringBuilder.Append("and ");
						}
						switch (num6)
						{
						case 0:
							stringBuilder.Append(array2[num5]);
							break;
						case 1:
							stringBuilder.Append(array3[num5]);
							break;
						default:
							stringBuilder.Append(array4[num6 - 2] + array2[num5]);
							break;
						}
					}
					if (num4 != 0)
					{
						stringBuilder.Append(array5[num4 - 1]);
					}
				}
			}
			return stringBuilder.ToString().TrimEnd();
		}

		public static bool CheckInternet()
		{
			try
			{
				using WebClient webClient = new WebClient();
				using (webClient.OpenRead("http://google.com/generate_204"))
				{
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		public static async Task<string> GetStockValue()
		{
			_ = string.Empty;
			decimal dValue = default(decimal);
			return await Task.Run(delegate
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTableWithIdParameter_SP("spGetStockValueForChart", new
				{
					BranchId = Settings.Default.iBranchId,
					Date = GetDate(DateTime.Now)
				});
				foreach (DataRow row in dataTable.Rows)
				{
					dValue += row[0].ToString2().ToDecimal();
				}
				return dValue.ToString(CommonSettings.strCurencyFormat);
			});
		}

		public static void OpeningBalanceVoucherMethod(decimal dmlOppening, string sDrOrCr, DBContext db, string sAccount, DateTime date)
		{
			string sVchrType = "Opening Voucher";
			string text = null;
			int intVchrNo = ((from x in db.VchrTransaction()
				where x.VchrType == sVchrType
				select x).Max((VchrTranLog y) => y.VchrNo) + 1) ?? 1;
			if (sDrOrCr == "CR")
			{
				dmlOppening *= -1m;
				text = "Credit Voucher";
			}
			else
			{
				dmlOppening = Math.Abs(dmlOppening);
				text = "Debit Voucher";
			}
			int num = (from x in db.Accounts()
				where x.AccountName == sAccount
				select x).FirstOrDefault().AccountId.ToInt32();
			POSHelper.VoucherTranLog(intVchrNo, date, dmlOppening, sVchrType, "Opening Account Entry", num.ToInt32(), 0, 0, Settings.Default.intLogOnId, text);
			int iVoucherID = (from x in db.VchrTransaction()
				where x.VchrNo == intVchrNo && x.VchrType == sVchrType
				select x).FirstOrDefault().VchrTranId.ToInt32();
			if (sDrOrCr == "DR")
			{
				POSHelper.TranLogMethod(Math.Abs(dmlOppening), 0m, num.ToInt32(), num.ToInt32(), iVoucherID, intVchrNo, sVchrType, Settings.Default.intLogOnId, "Main", text, GetDate(date));
			}
			else
			{
				POSHelper.TranLogMethod(0m, Math.Abs(dmlOppening), num.ToInt32(), num.ToInt32(), iVoucherID, intVchrNo, sVchrType, Settings.Default.intLogOnId, "Main", text, GetDate(date));
			}
		}

		public static void InsertToProPriceCategory(List<ProductPriceCategory> entpCategory, int iProductId, int iProGroupId)
		{
			List<ProductPriceCategory> list = new List<ProductPriceCategory>();
			foreach (ProductPriceCategory item2 in entpCategory)
			{
				_ = item2;
				ProductPriceCategory item = new ProductPriceCategory
				{
					ProPrceCatId = null,
					ProductId = iProductId,
					PGroupId = iProGroupId
				};
				list.Add(item);
			}
			AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "ProductPriceCategory");
		}

		public static string ChangeFirstLetterCase(string inputString)
		{
			inputString = inputString.ToLower().Trim();
			if (inputString.Length > 0)
			{
				char[] array = inputString.ToCharArray();
				array[0] = char.ToUpper(array[0]);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ToString() == " ")
					{
						array[i + 1] = char.ToUpper(array[i + 1]);
					}
				}
				return new string(array);
			}
			return inputString;
		}

		//public static void PDFGenerateMethod(DataGridView dgvReport, string sTitle)
		//{
		//	company = AshproCommon.GetObjectDetails<Company>("Select * From Institute");
		//	bool flag = false;
		//	int num = 0;
		//	string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		//	string text = folderPath + "\\" + Utility.uManufacturer + "\\";
		//	if (!Directory.Exists(text))
		//	{
		//		Directory.CreateDirectory(text);
		//	}
		//	try
		//	{
		//		PdfDocument pdfDocument = new PdfDocument();
		//		pdfDocument.Info.Title = "Account Group Report";
		//		PdfPage pdfPage = pdfDocument.AddPage();
		//		pdfPage.Size = PageSize.A4;
		//		Convert.ToInt32(pdfPage.Width.Value);
		//		Convert.ToInt32(pdfPage.Height.Value);
		//		int num2 = 0;
		//		XGraphics xGraphics = XGraphics.FromPdfPage(pdfPage);
		//		new XTextFormatter(xGraphics);
		//		XFont font = new XFont("Courier New", 9.0, XFontStyleEx.Bold);
		//		new XFont("Courier New", 8.0, XFontStyleEx.Bold);
		//		XFont font2 = new XFont("Courier New", 9.0, XFontStyleEx.Regular);
		//		XFont font3 = new XFont("Courier New", 12.0, XFontStyleEx.Bold | XFontStyleEx.Underline);
		//		XPen pen = new XPen(XColors.Black, 0.75);
		//		XStringFormat xStringFormat = new XStringFormat();
		//		XStringFormat xStringFormat2 = new XStringFormat();
		//		xStringFormat.Alignment = XStringAlignment.Center;
		//		xStringFormat2.Alignment = XStringAlignment.Far;
		//		//xStringFormat2.FormatFlags = XStringFormatFlags.MeasureTrailingSpaces;
		//		if (!flag)
		//		{
		//			xGraphics.DrawLine(pen, 8, 50, 585, 50);
		//			xGraphics.DrawLine(pen, 8, 50, 8, 800);
		//			xGraphics.DrawLine(pen, 8, 800, 585, 800);
		//			xGraphics.DrawLine(pen, 8, 70, 585, 70);
		//			xGraphics.DrawLine(pen, 585, 50, 585, 800);
		//			xGraphics.DrawLine(pen, 440, 50, 440, 800);
		//			xGraphics.DrawLine(pen, 370, 50, 370, 800);
		//			xGraphics.DrawLine(pen, 300, 50, 300, 800);
		//			xGraphics.DrawLine(pen, 45, 50, 45, 800);
		//			num2 = 5;
		//			xGraphics.DrawString(company.InstituteName, font3, XBrushes.Black, new XRect(250.0, 5.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			num2 = 40;
		//			xGraphics.DrawString(sTitle, font, XBrushes.BlueViolet, new XRect(5.0, 40.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			num2 = 55;
		//			xGraphics.DrawString("SL", font, XBrushes.Black, new XRect(9.0, 55.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			xGraphics.DrawString("Name", font, XBrushes.Black, new XRect(46.0, 55.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			xGraphics.DrawString("Debit", font, XBrushes.Black, new XRect(301.0, 55.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			xGraphics.DrawString("Credit", font, XBrushes.Black, new XRect(371.0, 55.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			xGraphics.DrawString("Balance", font, XBrushes.Black, new XRect(441.0, 55.0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			num2 = 75;
		//			flag = true;
		//		}
		//		while (num < dgvReport.Rows.Count)
		//		{
		//			string text2 = dgvReport.Rows[num].Cells[0].Value.ToString();
		//			string text3 = dgvReport.Rows[num].Cells[1].Value.ToString();
		//			string s = dgvReport.Rows[num].Cells[2].Value.ToString();
		//			string s2 = dgvReport.Rows[num].Cells[3].Value.ToString();
		//			string s3 = dgvReport.Rows[num].Cells[4].Value.ToString();
		//			xGraphics.DrawString(text2, font2, XBrushes.Black, new XRect(10.0, num2, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//			xGraphics.DrawLine(pen, 440, 50, 440, 800);
		//			xGraphics.DrawLine(pen, 370, 50, 370, 800);
		//			xGraphics.DrawLine(pen, 300, 50, 300, 800);
		//			xGraphics.DrawLine(pen, 45, 50, 45, 800);
		//			if (text2 == "")
		//			{
		//				xGraphics.DrawString(text3, font, XBrushes.Red, new XRect(46.0, num2, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//				xGraphics.DrawString(s, font, XBrushes.Red, 370.0, num2, xStringFormat2);
		//				xGraphics.DrawString(s2, font, XBrushes.Red, 440.0, num2, xStringFormat2);
		//				xGraphics.DrawString(s3, font, XBrushes.Red, 584.0, num2, xStringFormat2);
		//			}
		//			else
		//			{
		//				xGraphics.DrawString(text3, font2, XBrushes.Black, new XRect(46.0, num2, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
		//				xGraphics.DrawString(s, font2, XBrushes.Black, 370.0, num2, xStringFormat2);
		//				xGraphics.DrawString(s2, font2, XBrushes.Black, 440.0, num2, xStringFormat2);
		//				xGraphics.DrawString(s3, font2, XBrushes.Black, 584.0, num2, xStringFormat2);
		//			}
		//			num++;
		//			num2 += 10;
		//			if (num2 + 10 > 800)
		//			{
		//				num2 = 55;
		//				xGraphics.Dispose();
		//				pdfPage = pdfDocument.AddPage();
		//				xGraphics = XGraphics.FromPdfPage(pdfPage);
		//				xGraphics.DrawLine(pen, 8, 50, 585, 50);
		//				xGraphics.DrawLine(pen, 8, 50, 8, 800);
		//				xGraphics.DrawLine(pen, 8, 800, 585, 800);
		//				xGraphics.DrawLine(pen, 585, 50, 585, 800);
		//				xGraphics.DrawLine(pen, 440, 50, 440, 800);
		//				xGraphics.DrawLine(pen, 370, 50, 370, 800);
		//				xGraphics.DrawLine(pen, 300, 50, 300, 800);
		//				xGraphics.DrawLine(pen, 45, 50, 45, 800);
		//			}
		//		}
		//		num = 0;
		//		pdfDocument.Save(text + "Report.pdf");
		//		Process.Start(text + "Report.pdf");
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		public static byte[] imageToByteArray(Image imageIn)
		{
			MemoryStream memoryStream = new MemoryStream();
			imageIn.Save(memoryStream, ImageFormat.Png);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, array.Length);
			return array;
		}

		public static Image byteArrayToImage(byte[] byteArrayIn)
		{
			if (byteArrayIn == null)
			{
				return null;
			}
			MemoryStream stream = new MemoryStream(byteArrayIn);
			return Image.FromStream(stream);
		}

		public static string GetDateForsearch(DateTime dateTime)
		{
			try
			{
				CultureInfo cultureInfo = new CultureInfo("en-US");
				return "N'" + dateTime.ToString("yyyy-MM-ddTHH:mm:ss", cultureInfo) + "'";
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static string GetDate(DateTime dateTime)
		{
			try
			{
				CultureInfo cultureInfo = new CultureInfo("en-US");
				return dateTime.ToString("yyyy-MM-ddTHH:mm:ss", cultureInfo);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static string GetTime(DateTime dateTime)
		{
			try
			{
				return dateTime.ToString("HH:mm:ss");
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static DateTime GetFYearFromDate()
		{
			int num = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
			return Convert.ToDateTime(AshproCommon.IdFindMethod("Select DateFrom From FinancialYear Where FyearId='" + num + "'"));
		}

		public static Company GetCompany()
		{
			return AshproCommon.GetListMethod<Company>("Select * From Institute").FirstOrDefault();
		}

		public static DataTable GetDataTable(string[] headers)
		{
			DataTable dataTable = new DataTable();
			foreach (string text in headers)
			{
				if (text.Contains("dm_"))
				{
					string columnName = text.Replace("dm_", "");
					dataTable.Columns.Add(columnName, typeof(decimal));
				}
				else
				{
					dataTable.Columns.Add(text);
				}
			}
			return dataTable;
		}

		public decimal GetLastRate(int iProdId, int iUnitId, string sForm, string sType = "")
		{
			decimal? obj = null;
			DateTime? dateTime = null;
			if (sForm == "Purchase")
			{
				dateTime = AshproCommon.IdFindMethod("Select max(Date) as lastdate From PurInvSettings Where ProId='" + iProdId + "'");
				obj = ((!dateTime.HasValue) ? new decimal?(default(decimal)) : new decimal?(AshproCommon.ValueFindMethod("Select ProRate From PurInvSettings Where ProId='" + iProdId + "'and Date='" + GetDate(dateTime.ToDateTime()) + "' and UnitId='" + iUnitId + "'")));
			}
			else if (sForm == "Sales")
			{
				dateTime = AshproCommon.IdFindMethod("Select max(Date) as lastdate From SalesEstimateSettings Where ProductId='" + iProdId + "'");
				obj = ((!dateTime.HasValue) ? new decimal?(default(decimal)) : new decimal?(AshproCommon.ValueFindMethod("Select ProRate From SalesEstimateSettings Where ProductId='" + iProdId + "'and Date='" + GetDate(dateTime.ToDateTime()) + "' and UnitId='" + iUnitId + "'")));
			}
			return obj.ToDecimal();
		}

		public static List<T> CopyListData<T>(List<object> listSource)
		{
			List<T> list = new List<T>();
			foreach (object item in listSource)
			{
				T val = Activator.CreateInstance<T>();
				GlobalFunctions.CopyData(item, val);
				list.Add(val);
			}
			return list;
		}

		public static string GetIpAddress()
		{
			string text = "1234";
			try
			{
				WebRequest webRequest = WebRequest.Create("http://checkip.dyndns.org/");
				using (WebResponse webResponse = webRequest.GetResponse())
				{
					using StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
					text = streamReader.ReadToEnd();
				}
				int num = text.IndexOf("Address: ") + 9;
				int num2 = text.LastIndexOf("</body>");
				text = text.Substring(num, num2 - num);
				return text;
			}
			catch (Exception)
			{
			}
			return text;
		}

		public static string GetLocalIPAddress()
		{
			string hostName = Dns.GetHostName();
			return Dns.GetHostByName(hostName).AddressList[0].ToString();
		}

		public static int GetRandomNumber()
		{
			Random random = new Random();
			return random.Next(12345, 98765);
		}

		public static void OSKMethod()
		{
			try
			{
				string text = Path.Combine(Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
				string text2 = "C:\\windows\\system32\\osk.exe";
				string text3 = (Environment.Is64BitOperatingSystem ? text : text2);
				if (File.Exists(text3))
				{
					Process.Start(text3);
				}
			}
			catch (Exception)
			{
                //Messages.InformationMessage("On-Screen Keyboard is not installed in your Machine or not in currect path");
                MessageBox.Show("On-Screen Keyboard is not installed in your Machine or not in currect path");
            }
		}

		public static string InputBoxMethod(string sTitle, string sCaption, string sLabel, bool isPassword = false, string inputTex = null)
		{
			InputBox inputBox = new InputBox();
			inputBox.lblHead.Text = ((sTitle == string.Empty) ? "AccountsLibrary" : sTitle);
			inputBox.lblCaption.Text = ((sCaption == string.Empty) ? "Enter the details" : sCaption);
			inputBox.lblInput.Text = ((sLabel == string.Empty) ? "Data :" : sLabel);
			inputBox.txtInput.Text = inputTex;
			if (isPassword)
			{
				inputBox.txtInput.PasswordChar = '*';
			}
			if (inputBox.ShowDialog() == DialogResult.OK)
			{
				return inputBox.txtInput.Text;
			}
			return string.Empty;
		}

		public static decimal AccountBalanceFindMethod(decimal AddAmnt, decimal MnsAmnt, ComboBox cmb, Control lblResult, DateTime dtmDate)
		{
			decimal num = AshproCommon.ValueFindMethod("SELECT SUM(DebitAmount-CreditAmount) as Balance FROM TransactionLog where AccountId='" + cmb.SelectedValue.ToInt32() + "' and VchrDate <= '" + GetDate(dtmDate) + "'");
			num = num + AddAmnt - MnsAmnt;
			if (num >= 0m)
			{
				lblResult.Text = num.ToString(CommonSettings.strCurencyFormat) + " Dr";
			}
			else
			{
				lblResult.Text = Math.Abs(num).ToString(CommonSettings.strCurencyFormat) + " Cr";
			}
			return num;
		}

		public static string AccountBalanceFindMethod(int iAccountId, string sDate, decimal? dAdd = null, decimal? dMinus = null)
		{
			decimal num = default(decimal);
			num = AshproCommon.GetDecimalDataWithParameter_SP("spGetAccountBalance", new
			{
				AccountId = iAccountId,
				VchrDate = sDate
			});
			num = num + dAdd.ToDecimal() - dMinus.ToDecimal();
			string empty = string.Empty;
			if (num >= 0m)
			{
				return num.ToString(CommonSettings.strCurencyFormat) + " Dr";
			}
			return Math.Abs(num).ToString(CommonSettings.strCurencyFormat) + " Cr";
		}

		public static UserPrevilegeDtl GetPrevilageMethod(string sForm = null, string FomName = null)
		{
			MenuDetails menuDetails = new MenuDetails();
			DBContext dBContext = new DBContext();
			menuDetails = ((sForm == null) ? dBContext.MenuDetails(new
			{
				Name = FomName
			}).FirstOrDefault() : dBContext.MenuDetails(new
			{
				ClassName = sForm
			}).FirstOrDefault());
			if (menuDetails != null)
			{
				return dBContext.UserPrevilegeDtls(new
				{
					FK_UserPrevilegeHdrId = Utility.uPrevilageId.ToInt32(),
					FK_MenuDetailsId = menuDetails.Id
				}).FirstOrDefault();
			}
			return null;
		}

		//public static void ItemCreationMethod(int? iProductId = null)
		//{
		//	DBContext dBContext = new DBContext();
		//	POSItemCreation pOSItemCreation = new POSItemCreation();
		//	if (iProductId.HasValue)
		//	{
		//		ProductEntity productEntity2 = (pOSItemCreation.entProduct = dBContext.Products(new
		//		{
		//			ProductId = iProductId
		//		}).FirstOrDefault());
		//	}
		//	pOSItemCreation.ShowDialog();
		//}

		public static string RandomString(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
				select s[random.Next(s.Length)]).ToArray());
		}

		private static string identifier(string wmiClass, string wmiProperty)
		{
			string text = "";
			ManagementClass managementClass = new ManagementClass(wmiClass);
			ManagementObjectCollection instances = managementClass.GetInstances();
			foreach (ManagementObject item in instances)
			{
				if (text == "")
				{
					try
					{
						text = item[wmiProperty].ToString();
					}
					catch
					{
						continue;
					}
					break;
				}
			}
			return text;
		}

		public static string getCPUID()
		{
			return identifier("Win32_BaseBoard", "SerialNumber") + identifier("win32_processor", "processorID");
		}

		public static string Encrypt(string clearText)
		{
			string password = "MAKV2SPBNI99212";
			byte[] bytes = Encoding.Unicode.GetBytes(clearText);
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
				{
					73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
					100, 101, 118
				});
				rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(32);
				rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(16);
				using MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytes, 0, bytes.Length);
					cryptoStream.Close();
				}
				clearText = Convert.ToBase64String(memoryStream.ToArray());
			}
			return clearText;
		}

		public static string Decrypt(string cipherText)
		{
			try
			{
				string password = "MAKV2SPBNI99212";
				new UTF8Encoding();
				byte[] array = Convert.FromBase64String(cipherText);
				using RijndaelManaged rijndaelManaged = new RijndaelManaged();
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
				{
					73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
					100, 101, 118
				});
				rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(32);
				rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(16);
				using MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.Close();
				}
				cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
			}
			catch (Exception)
			{
			}
			return cipherText;
		}

		public static DataTable ListToDataTable<T>(List<T> items)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				dataTable.Columns.Add(propertyInfo.Name);
			}
			foreach (T item in items)
			{
				object[] array2 = new object[properties.Length];
				for (int j = 0; j < properties.Length; j++)
				{
					if (properties[j].PropertyType.Name == "Decimal")
					{
						array2[j] = properties[j].GetValue(item, null).ToString().ToDecimal()
							.ToString(CommonSettings.strCurencyFormat)
							.ToDecimal();
					}
					else
					{
						array2[j] = properties[j].GetValue(item, null);
					}
				}
				dataTable.Rows.Add(array2);
			}
			return dataTable;
		}

		public static DataTable ToDataTable<T>(T item)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				dataTable.Columns.Add(propertyInfo.Name);
			}
			object[] array2 = new object[properties.Length];
			for (int j = 0; j < properties.Length; j++)
			{
				if (properties[j].PropertyType.Name == "Decimal")
				{
					array2[j] = properties[j].GetValue(item, null).ToString().ToDecimal()
						.ToString(CommonSettings.strCurencyFormat);
				}
				else
				{
					array2[j] = properties[j].GetValue(item, null);
				}
			}
			dataTable.Rows.Add(array2);
			return dataTable;
		}

		public static List<T> DataTableToList<T>(DataTable data)
		{
			List<T> list = new List<T>();
			_ = typeof(T);
			T val = Activator.CreateInstance<T>();
			foreach (DataRow row in data.Rows)
			{
				val = GetItem<T>(row);
				list.Add(val);
			}
			return list;
		}

		public static T GetItem<T>(DataRow dr)
		{
			try
			{
				Type typeFromHandle = typeof(T);
				T val = Activator.CreateInstance<T>();
				foreach (DataColumn column in dr.Table.Columns)
				{
					PropertyInfo[] properties = typeFromHandle.GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (!(propertyInfo.Name == column.ColumnName))
						{
							continue;
						}
						if (dr[column.ColumnName] != DBNull.Value)
						{
							if (propertyInfo.PropertyType.Name == "Boolean")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToBool(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Int32")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToInt32(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Decimal")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToDecimal(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "Nullable`1")
							{
								if (propertyInfo.PropertyType.FullName.Contains("System.Int32"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, dr[column.ColumnName].ToInt32(), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.Boolean"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToBool(), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
								{
									if (dr[column.ColumnName].ToString() != string.Empty)
									{
										propertyInfo.SetValue(val, Convert.ToDateTime(dr[column.ColumnName].ToString()), null);
									}
								}
								else if (propertyInfo.PropertyType.FullName.Contains("System.Decimal") && dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, dr[column.ColumnName].ToString().ToDecimal(), null);
								}
							}
							else if (propertyInfo.PropertyType.Name == "DateTime")
							{
								if (dr[column.ColumnName].ToString() != string.Empty)
								{
									propertyInfo.SetValue(val, Convert.ToDateTime(dr[column.ColumnName].ToString()), null);
								}
							}
							else
							{
								propertyInfo.SetValue(val, dr[column.ColumnName].ToString(), null);
							}
						}
						else
						{
							propertyInfo.SetValue(val, null, null);
						}
					}
				}
				return val;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				throw;
			}
		}

		public static List<string> GetMACAddress()
		{
			List<string> list = new List<string>();
			string empty = string.Empty;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (networkInterface.OperationalStatus == OperationalStatus.Up && !networkInterface.Description.Contains("Virtual") && !networkInterface.Description.Contains("Pseudo") && networkInterface.GetPhysicalAddress().ToString() != "")
				{
					empty = networkInterface.GetPhysicalAddress().ToString();
					if (!list.Contains(empty))
					{
						list.Add(empty);
					}
				}
			}
			return list;
		}

		public static List<string> GetMaccID()
		{
			List<string> list = new List<string>();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			string empty = string.Empty;
			NetworkInterface[] array = allNetworkInterfaces;
			foreach (NetworkInterface networkInterface in array)
			{
				networkInterface.GetIPProperties();
				empty = networkInterface.GetPhysicalAddress().ToString();
				if (empty != string.Empty && !list.Contains(empty))
				{
					list.Add(empty);
				}
			}
			return list;
		}

		public static void DataTableToTextFile(DataTable dt, string outputFilePath)
		{
			int[] array = new int[dt.Columns.Count];
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				array[i] = dt.Columns[i].ColumnName.Length;
				foreach (DataRow row in dt.Rows)
				{
					if (!row.IsNull(i))
					{
						int length = row[i].ToString().Length;
						if (length > array[i])
						{
							array[i] = length;
						}
					}
				}
			}
			using StreamWriter streamWriter = new StreamWriter(outputFilePath, append: false, Encoding.Unicode);
			for (int j = 0; j < dt.Columns.Count; j++)
			{
				streamWriter.Write(dt.Columns[j].ColumnName.PadRight(array[j] + 2));
				streamWriter.Write("\t");
			}
			streamWriter.WriteLine();
			foreach (DataRow row2 in dt.Rows)
			{
				for (int k = 0; k < dt.Columns.Count; k++)
				{
					if (!row2.IsNull(k))
					{
						streamWriter.Write(row2[k].ToString().PadRight(array[k] + 2));
						streamWriter.Write("\t");
					}
					else
					{
						streamWriter.Write(new string(' ', array[k] + 2));
						streamWriter.Write("\t");
					}
				}
				streamWriter.WriteLine();
			}
			streamWriter.Close();
		}

		public static void ExportDataTabletoFile(DataTable datatable, string file)
		{
			try
			{
				StreamWriter streamWriter = new StreamWriter(file, append: true, Encoding.Unicode);
				string text = string.Empty;
				foreach (DataColumn column in datatable.Columns)
				{
					text += column.ColumnName;
				}
				streamWriter.WriteLine(text.Remove(text.Length - 1, 1));
				foreach (DataRow row in datatable.Rows)
				{
					string text2 = string.Empty;
					object[] itemArray = row.ItemArray;
					foreach (object obj in itemArray)
					{
						text2 += obj.ToString();
					}
					streamWriter.WriteLine(text2.Remove(text2.Length - 1, 1), 0);
				}
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				//Messages.ErrorMessage(ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		public static void DataTabletoFile(DataTable dt, string sfile)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DataColumn column in dt.Columns)
			{
				if (column.ColumnName != "DiscountFlag")
				{
					stringBuilder.Append(column.ColumnName.Trim());
					stringBuilder.Append("\t");
				}
				else
				{
					stringBuilder.Append(column.ColumnName.Trim());
					stringBuilder.Append("\t");
					stringBuilder.Append(Environment.NewLine);
				}
			}
			foreach (DataRow row in dt.Rows)
			{
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					if (!row.IsNull(i))
					{
						stringBuilder.Append(row[i].ToString());
						stringBuilder.Append("\t");
					}
					else
					{
						stringBuilder.Append("0");
						stringBuilder.Append("\t");
					}
				}
				stringBuilder.Append(Environment.NewLine);
			}
			FileStream stream = new FileStream(sfile, FileMode.CreateNew);
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(stringBuilder.ToString());
			streamWriter.Flush();
			streamWriter.Close();
		}

		private void PDocu_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				_ = e.MarginBounds.Left;
				int num = e.MarginBounds.Top;
				int num2 = e.MarginBounds.Bottom;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				if (!isPrinted)
				{
					num = 60;
					e.Graphics.DrawRectangle(Pens.Black, 8, 60, e.MarginBounds.Right + 40, num2 - 20);
					num2 += 40;
					foreach (DataRow row in LineData.Rows)
					{
						e.Graphics.DrawLine(Pens.Black, row[0].ToInt32(), num, row[1].ToInt32(), num2);
					}
					e.Graphics.DrawString(company.InstituteName, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
					e.Graphics.DrawString(Title, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
					e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
					foreach (DataRow row2 in Header.Rows)
					{
						e.Graphics.DrawString(row2[0].ToString(), new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(row2[1].ToInt32(), 70));
					}
					isPrinted = true;
					num = 100;
				}
				if (isNotFirst)
				{
					num = 15;
					e.Graphics.DrawRectangle(Pens.Black, 8, 15, e.MarginBounds.Right + 40, num2 + 25);
					num2 += 40;
					foreach (DataRow row3 in LineData.Rows)
					{
						e.Graphics.DrawLine(Pens.Black, row3[0].ToInt32(), num, row3[1].ToInt32(), num2);
					}
				}
				while (true)
				{
					if (linesPrinted < dgReport.Rows.Count)
					{
						foreach (DataRow row4 in data.Rows)
						{
							if (dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString() == "" && row4[2].ToString().ToBool())
							{
								if (row4[3].ToString().ToBool())
								{
									e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(row4[1].ToInt32(), num, row4[5].ToInt32(), 15f));
								}
								else if (row4[4].ToString() == "Right")
								{
									e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat);
								}
								else if (row4[4].ToString() == "Center")
								{
									e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat2);
								}
								else
								{
									e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num));
								}
							}
							else if (row4[3].ToString().ToBool())
							{
								e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(row4[1].ToInt32(), num, row4[5].ToInt32(), 15f));
							}
							else if (row4[4].ToString() == "Right")
							{
								e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat);
							}
							else if (row4[4].ToString() == "Right")
							{
								e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat2);
							}
							else
							{
								e.Graphics.DrawString(dgReport.Rows[linesPrinted].Cells[row4[0].ToInt32()].Value.ToString(), new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num));
							}
						}
						linesPrinted++;
						num += 20;
						if (num >= e.MarginBounds.Bottom + 30)
						{
							break;
						}
						e.Graphics.DrawLine(Pens.Black, 8, num - 2, e.MarginBounds.Right + 47, num - 2);
						continue;
					}
					linesPrinted = 0;
					isNotFirst = false;
					return;
				}
				isNotFirst = true;
				e.HasMorePages = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				_ = e.MarginBounds.Left;
				int num = e.MarginBounds.Top;
				int num2 = e.MarginBounds.Bottom;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				if (!isPrinted)
				{
					num = 60;
					e.Graphics.DrawRectangle(Pens.Black, 8, 60, e.MarginBounds.Right + 40, num2 - 20);
					num2 += 40;
					foreach (DataRow row in LineData.Rows)
					{
						e.Graphics.DrawLine(Pens.Black, row[0].ToInt32(), num, row[1].ToInt32(), num2);
					}
					e.Graphics.DrawString(company.InstituteName, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
					e.Graphics.DrawString(Title, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
					e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
					foreach (DataRow row2 in Header.Rows)
					{
						e.Graphics.DrawString(row2[0].ToString(), new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(row2[1].ToInt32(), 70));
					}
					isPrinted = true;
					num = 100;
				}
				if (isNotFirst)
				{
					num = 15;
					e.Graphics.DrawRectangle(Pens.Black, 8, 15, e.MarginBounds.Right + 40, num2 + 25);
					num2 += 40;
					foreach (DataRow row3 in LineData.Rows)
					{
						e.Graphics.DrawLine(Pens.Black, row3[0].ToInt32(), num, row3[1].ToInt32(), num2);
					}
				}
				while (true)
				{
					if (linesPrinted < details.Rows.Count)
					{
						foreach (DataRow row4 in data.Rows)
						{
							string text = details.Rows[linesPrinted][row4[0].ToInt32()].ToString();
							string familyName = "Times New Roman";
							if (text == "" && row4[2].ToString().ToBool())
							{
								if (row4[3].ToString().ToBool())
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Bold), Brushes.Black, new RectangleF(row4[1].ToInt32(), num, row4[5].ToInt32(), 15f));
								}
								else if (row4[4].ToString() == "Right")
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat);
								}
								else if (row4[4].ToString() == "Center")
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat2);
								}
								else
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Bold), Brushes.Black, new Point(row4[1].ToInt32(), num));
								}
							}
							else if (row4[3].ToString().ToBool())
							{
								if (row4[4].ToString() == "Right")
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Regular), Brushes.Black, new RectangleF(row4[1].ToInt32(), num, row4[5].ToInt32(), 15f), stringFormat);
								}
								else
								{
									e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Regular), Brushes.Black, new RectangleF(row4[1].ToInt32(), num, row4[5].ToInt32(), 15f));
								}
							}
							else if (row4[4].ToString() == "Right")
							{
								e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat);
							}
							else if (row4[4].ToString() == "Center")
							{
								e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num), stringFormat2);
							}
							else
							{
								e.Graphics.DrawString(text, new Font(familyName, 10f, FontStyle.Regular), Brushes.Black, new Point(row4[1].ToInt32(), num));
							}
						}
						linesPrinted++;
						num += 20;
						if (num >= e.MarginBounds.Bottom + 30)
						{
							break;
						}
						e.Graphics.DrawLine(Pens.Black, 8, num - 2, e.MarginBounds.Right + 47, num - 2);
						continue;
					}
					linesPrinted = 0;
					isNotFirst = false;
					return;
				}
				isNotFirst = true;
				e.HasMorePages = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
