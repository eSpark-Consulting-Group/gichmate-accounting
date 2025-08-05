using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using Microsoft.CSharp.RuntimeBinder;
using excel= Microsoft.Office.Interop.Excel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace Ashpro
{
	public class Profit_and_Lose_Account : Form
	{
		private string strMailClient;

		private string strPort;

		private string strUserId;

		private string strPassWord;

		private string pdfFilename;

		private string strConnection;

		private string strPassingValue;

		private AccountGroupReport ACCgRPrPT;

		private StockReportView SReprt;

		private decimal OppStockValue;

		private decimal PurchaseValue;

		private decimal PurchaseRtnVal;

		private decimal SalRtnValue;

		private decimal DirectExp;

		private decimal SalesValue;

		private decimal DirectIncom;

		private decimal ClosStockValue;

		private decimal IndirectExp;

		private decimal IndirectIncome;

		private decimal SubTotalOne;

		private decimal SubTotalTwo;

		private decimal Gross;

		private decimal Trading;

		private decimal ExpTotal;

		private decimal IncmTotal;

		private decimal Net;

		private decimal ProfitAndLose;

		private decimal dmlProRate;

		private string strProId;

		private bool isPrinted;

		private bool isNotFirst;

		private int linesPrinted;

		private int y;

		private List<string> listOne;

		private List<string> listTwo;

		private List<string> listThree;

		private List<string> listFour;

		private string strFirm;

		private string strEmailId;

		private string Query;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private PrintDocument printDocument1;

		private AshDateTimePicker dtmFrom;

		private ToolTip toolTip1;

		private Button btnShow;

		private Button btnClear;

		private Button btnClose;

		private Button btnExcel;

		private Button btnMail;

		private Button btnPrint;

		private AshDateTimePicker dtmTo;

		private Panel panel1;

		private Label label5;

		private Label label6;

		private ListView livDetails;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private AshComboBox cmbClValidation;

		private Label label1;

		private AshComboBox cmbOpValidation;

		private Label label3;

		private AshComboBox cmbRType;

		private Label label2;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string PassingValue { get; set; }

		public Profit_and_Lose_Account()
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
			PrintController printController = new StandardPrintController();
			printDocument1.PrintController = printController;
		}

		private void ClearMethod()
		{
			livDetails.Items.Clear();
			panel1.Size = new Size(923, 526);
			livDetails.Parent = panel1;
			livDetails.Location = new Point(9, 12);
			livDetails.Size = new Size(906, 500);
			cmbOpValidation.SelectedIndex = 0;
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
		}

		//private void ExportToExcelMethod()
		//{
		//	//Discarded unreachable code: IL_0057, IL_0066, IL_0076, IL_0526
		//	//Error decoding local variables: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.
		//	//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0020: Expected O, but got Unknown
		//	//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0057: Incompatible stack heights: 1 vs 0
		//	//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0071: Expected O, but got Unknown
		//	//IL_0082: Expected O, but got Unknown
		//	//IL_0152: Expected O, but got Unknown
		//	//IL_0158: Expected I4, but got Unknown
		//	//IL_015f: Expected I4, but got Unknown
		//	//IL_0180: Expected O, but got Unknown
		//	//IL_0186: Expected I4, but got Unknown
		//	//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0190: Expected I4, but got Unknown
		//	//IL_01a2: Expected I4, but got Unknown
		//	//IL_0246: Expected O, but got Unknown
		//	//IL_024c: Expected I4, but got Unknown
		//	//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_0256: Expected I4, but got Unknown
		//	//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_027f: Invalid comparison between Unknown and I4
		//	//IL_02a7: Expected O, but got Unknown
		//	//IL_02b4: Expected O, but got Unknown
		//	//IL_02ba: Expected I4, but got Unknown
		//	//IL_02c1: Expected I4, but got Unknown
		//	//IL_02c8: Expected O, but got Unknown
		//	//IL_02d9: Expected O, but got Unknown
		//	//IL_02ed: Expected O, but got Unknown
		//	//IL_02fa: Expected O, but got Unknown
		//	//IL_0300: Expected I4, but got Unknown
		//	//IL_0307: Expected I4, but got Unknown
		//	//IL_030e: Expected O, but got Unknown
		//	//IL_031f: Expected O, but got Unknown
		//	//IL_03c6: Expected O, but got Unknown
		//	//IL_03cc: Expected I4, but got Unknown
		//	//IL_03d3: Expected I4, but got Unknown
		//	//IL_03eb: Expected O, but got Unknown
		//	//IL_0492: Expected O, but got Unknown
		//	//IL_0498: Expected I4, but got Unknown
		//	//IL_049f: Expected I4, but got Unknown
		//	//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_04bd: Expected O, but got Unknown
		//	//IL_04c7: Expected O, but got Unknown
		//	//IL_04d8: Expected O, but got Unknown
		//	//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//	//IL_04e7: Expected O, but got Unknown
		//	//IL_04f1: Expected O, but got Unknown
		//	//IL_0502: Expected O, but got Unknown
		//	//IL_050a: Expected O, but got Unknown
		//	//IL_0515: Expected O, but got Unknown
		//	//IL_0525: Expected O, but got Unknown
		//	Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		//	/*Error near IL_0019: stloc 0 (out-of-bounds)*/;
		//	_ = ((_Application)/*Error near IL_001a: ldloc 0 (out-of-bounds)*/).Workbooks;
		//	((Workbooks)XlSheetType.xlWorksheet).Add();
		//	if ((int)_003C_003Eo__47._003C_003Ep__0 == 0)
		//	{
		//		Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(_003F), typeof(Profit_and_Lose_Account));
		//		/*Error near IL_0052: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//	}
		//	_ = _003C_003Eo__47._003C_003Ep__0;
		//	/*Error near IL_0061: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//}

		private void ReportShowMethod()
		{
			int? iBranchId = null;
			if (cmbBranch.Text != "")
			{
				iBranchId = cmbBranch.SelectedValue.ToInt32();
			}
			livDetails.Items.Clear();
			listOne = new List<string>();
			listTwo = new List<string>();
			listThree = new List<string>();
			listFour = new List<string>();
			listOne.Clear();
			listTwo.Clear();
			listThree.Clear();
			listFour.Clear();
			listOne.Add(" ");
			listTwo.Add(" ");
			listThree.Add(" ");
			listFour.Add(" ");
			OppStockValue = StckFindMethodTwo(isOppeing: true);
			ClosStockValue = StckFindMethodTwo(isOppeing: false);
			int selectedIndex = cmbRType.SelectedIndex;
			DateTime date = dtmFrom.Value.Date;
			DateTime dtTo = dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0);
			PurchaseValue = AniHelper.AccountBalanceMethod(selectedIndex, "D", date, dtTo, iBranchId, "PURCHASE ACCOUNT");
			PurchaseRtnVal = AniHelper.AccountBalanceMethod(selectedIndex, "C", date, dtTo, iBranchId, "PURCHASE RETURN ACCOUNT");
			PurchaseValue -= PurchaseRtnVal;
			SalesValue = AniHelper.AccountBalanceMethod(selectedIndex, "C", date, dtTo, iBranchId, "SALES ACCOUNT");
			SalRtnValue = AniHelper.AccountBalanceMethod(selectedIndex, "D", date, dtTo, iBranchId, "SALES RETURN ACCOUNT");
			SalesValue -= SalRtnValue;
			DirectExp = AniHelper.AccountBalanceMethod(selectedIndex, "D", date, dtTo, iBranchId, null, "DIRECT EXPENSES");
			DirectIncom = AniHelper.AccountBalanceMethod(selectedIndex, "C", date, dtTo, iBranchId, null, "DIRECT INCOME");
			IndirectExp = AniHelper.AccountBalanceMethod(selectedIndex, "D", date, dtTo, iBranchId, null, "INDIRECT EXPENSES");
			IndirectIncome = AniHelper.AccountBalanceMethod(selectedIndex, "C", date, dtTo, iBranchId, null, "INDIRECT INCOME");
			SubTotalOne = OppStockValue + PurchaseValue + DirectExp;
			SubTotalTwo = SalesValue + DirectIncom + ClosStockValue;
			Gross = SubTotalTwo - SubTotalOne;
			if (OppStockValue != 0m)
			{
				listOne.Add("OPENING STOCK");
				listTwo.Add(OppStockValue.ToString("F"));
			}
			if (PurchaseValue != 0m)
			{
				listOne.Add("PURCHASE ACCOUNT");
				listTwo.Add(PurchaseValue.ToString("F"));
			}
			if (DirectExp != 0m)
			{
				listOne.Add("DIRECT EXPENSES");
				listTwo.Add(DirectExp.ToString("F"));
			}
			if (SalesValue != 0m)
			{
				listThree.Add("SALES ACCOUNT");
				listFour.Add(SalesValue.ToString("F"));
			}
			if (DirectIncom != 0m)
			{
				listThree.Add("DIRECT INCOME");
				listFour.Add(DirectIncom.ToString("F"));
			}
			if (ClosStockValue != 0m)
			{
				listThree.Add("CLOSING STOCK");
				listFour.Add(ClosStockValue.ToString("F"));
			}
			DisplayListInColumns(livDetails, listOne, 0);
			DisplayListInColumns(livDetails, listTwo, 1);
			DisplayListInColumns(livDetails, listThree, 2);
			DisplayListInColumns(livDetails, listFour, 3);
			ListViewAddMethod("SUB TOTAL", SubTotalOne.ToString("F"), "SUB TOTAL", SubTotalTwo.ToString("F"));
			if (Gross >= 0m)
			{
				Trading = SubTotalTwo;
				ListViewAddMethod(" ", " ", "GROSS PROFIT", Gross.ToString("F"));
			}
			else
			{
				Trading = SubTotalOne;
				ListViewAddMethod("GROSS LOSS", Gross.ToString("F"), " ", " ");
			}
			ListViewAddMethod("TRADING A/C TOTAL", Trading.ToString("F"), "TRADING A/C TOTAL", Trading.ToString("F"));
			ListViewAddMethod(" ", " ", " ", " ");
			if (Gross >= 0m)
			{
				ListViewAddMethod("GROSS PROFIT", Gross.ToString("F"), " ", " ");
				IncmTotal = IndirectIncome + Gross;
				ExpTotal = IndirectExp;
			}
			else
			{
				ListViewAddMethod(" ", " ", "GROSS LOSS", Gross.ToString("F"));
				IncmTotal = IndirectIncome;
				ExpTotal = IndirectExp + Gross;
			}
			ListViewAddMethod("INDIRECT INCOME", IndirectIncome.ToString("F"), "INDIRECT EXPENSES", IndirectExp.ToString("F"));
			ListViewAddMethod(" ", " ", " ", " ");
			ListViewAddMethod("INCOME TOTAL", IncmTotal.ToString("F"), "EXPENSES TOTAL", ExpTotal.ToString("F"));
			Net = IncmTotal - ExpTotal;
			if (Net >= 0m)
			{
				ProfitAndLose = IncmTotal;
				ListViewAddMethod(" ", " ", "NET PROFIT", Net.ToString("F"));
			}
			else
			{
				ProfitAndLose = ExpTotal;
				ListViewAddMethod("NET LOSS", Net.ToString("F"), " ", "");
			}
			ListViewAddMethod(" ", " ", " ", " ");
			FontFamily family = new FontFamily(livDetails.Items[0].SubItems[1].Font.Name);
			ListViewAddMethod("PROFIT AND LOSE A/C TOTAL", ProfitAndLose.ToString("F"), "PROFIT AND LOSE A/C TOTAL", ProfitAndLose.ToString("F"));
			for (int i = 0; i < livDetails.Items.Count; i++)
			{
				if (livDetails.Items[i].SubItems[0].Text == "PROFIT AND LOSE A/C " || livDetails.Items[i].SubItems[0].Text == "PROFIT AND LOSE A/C TOTAL" || livDetails.Items[i].SubItems[0].Text == "INCOME TOTAL" || livDetails.Items[i].SubItems[0].Text == "TRADING A/C TOTAL" || livDetails.Items[i].SubItems[0].Text == "SUB TOTAL")
				{
					livDetails.Items[i].SubItems[0].Font = new Font(family, 10f, FontStyle.Bold);
				}
			}
			livDetails.Size = new Size(906, livDetails.Items.Count * 20 + 18);
			panel1.Size = new Size(927, livDetails.Height + 21);
		}

		private decimal StckFindMethodTwo(bool isOppeing)
		{
			decimal num = default(decimal);
			if (isOppeing)
			{
				return AshproCommon.GetDecimalDataWithParameter_SP("spProfitAndLossOppeningStock", new
				{
					Validation = cmbOpValidation.Text,
					BranchId = Settings.Default.iBranchId,
					Date = AniHelper.GetDate(dtmFrom.Value)
				});
			}
			return AshproCommon.GetDecimalDataWithParameter_SP("spProfitAndLossClosingStock", new
			{
				Validation = cmbOpValidation.Text,
				BranchId = Settings.Default.iBranchId,
				Date = AniHelper.GetDate(dtmTo.Value)
			});
		}

		private decimal StckFindMethod(string strProductId, bool isOpp)
		{
			if (isOpp)
			{
				if (cmbRType.SelectedIndex == 0)
				{
					Query = "Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog where ProductId='" + strProductId + "' " + sBranchDetails + " and VchrDate < '" + AniHelper.GetDate(dtmFrom.Value) + "'";
				}
				else
				{
					Query = "Select OpeningStock From Product where ProductId='" + strProductId + "'";
				}
			}
			else
			{
				Query = "Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog where ProductId='" + strProductId + "' " + sBranchDetails + " and VchrDate <= '" + AniHelper.GetDate(dtmTo.Value) + "'";
			}
			return AshproCommon.ValueFindMethod(Query);
		}

		private decimal RateFindMethod(string strProId, bool isOpp)
		{
			decimal? num = 0m;
			ComboBox comboBox = ((!isOpp) ? cmbClValidation : cmbOpValidation);
			if (comboBox.SelectedIndex == 0)
			{
				Query = "Select SellingPrice From Product Where ProductId='" + strProId + "'";
			}
			else if (comboBox.SelectedIndex == 1)
			{
				Query = "Select PurchasePrice From Product Where ProductId='" + strProId + "'";
			}
			else if (comboBox.SelectedIndex == 2)
			{
				Query = "Select MRP From Product Where ProductId='" + strProId + "'";
			}
			else if (comboBox.SelectedIndex == 3)
			{
				Query = "Select TOP (1) ProRate From SalesEstimateSettings Where ProductId='" + strProId + "' ORDER BY ProductId DESC ";
			}
			else if (comboBox.SelectedIndex == 4)
			{
				Query = "Select TOP (1) ProRate From PurInvSettings Where ProId='" + strProId + "' ORDER BY ProId DESC ";
			}
			else if (comboBox.SelectedIndex == 5)
			{
				Query = "Select AVG(ProRate) AS ProRate From SalesEstimateSettings Where ProductId='" + strProId + "'";
			}
			else if (comboBox.SelectedIndex == 6)
			{
				Query = "Select AVG(ProRate) AS ProRate From PurInvSettings Where ProId='" + strProId + "'";
			}
			else if (comboBox.SelectedIndex == 7)
			{
				Query = "Select MAX(ProRate) AS ProRate From SalesEstimateSettings Where ProductId='" + strProId + "'";
			}
			else
			{
				Query = "Select MAX(ProRate) AS ProRate From PurInvSettings Where ProId='" + strProId + "'";
			}
			num = AshproCommon.ValueFindMethod(Query);
			decimal? num2 = num;
			if ((num2.GetValueOrDefault() == default(decimal)) & num2.HasValue)
			{
				num = RateMethodTwo(strProId, comboBox);
			}
			return num.Value;
		}

		private decimal RateMethodTwo(string strProId, ComboBox combow)
		{
			if (combow.SelectedIndex != 3 && combow.SelectedIndex != 5 && combow.SelectedIndex != 7)
			{
				if (combow.SelectedIndex == 4 || combow.SelectedIndex == 6 || combow.SelectedIndex == 8)
				{
					Query = "Select PurchasePrice From Product Where ProductId='" + strProId + "'";
				}
			}
			else
			{
				Query = "Select SellingPrice From Product Where ProductId='" + strProId + "'";
			}
			return AshproCommon.ValueFindMethod(Query);
		}

		private void ListViewAddMethod(string First, string Second, string Third, string Four)
		{
			ListViewItem listViewItem = new ListViewItem(First);
			listViewItem.SubItems.Add(Second);
			listViewItem.SubItems.Add(Third);
			listViewItem.SubItems.Add(Four);
			livDetails.Items.Add(listViewItem);
		}

		private void PrintMethod()
		{
			if (livDetails.Items.Count > 0)
			{
				FirmNameFindMethod();
				isPrinted = false;
				isNotFirst = false;
				linesPrinted = 0;
				printDocument1.Print();
			}
			else
			{
				MessageBox.Show("There is no data to print in the table");
			}
		}

		private void MailMethod()
		{
			if (livDetails.Items.Count > 0)
			{
				isNotFirst = false;
				isPrinted = false;
				linesPrinted = 0;
				MailSendMethod();
			}
			else
			{
				MessageBox.Show("There is no data in the table to send mail.");
			}
		}

		private void MailSendMethod()
		{
			pdfDocumentCurrent();
			if (strEmailId == null || strEmailId == "")
			{
				MyMsgBox myMsgBox = new MyMsgBox();
				myMsgBox.Passvalue = "Mail Address";
				myMsgBox.ShowDialog();
				strEmailId = myMsgBox.Passvalue;
			}
			int port = Convert.ToInt32(strPort);
			string from = strUserId;
			string to = strEmailId;
			string text = "Profit and Loss Report From " + dtmFrom.Text + " To " + dtmTo.Text;
			string host = strMailClient;
			string userName = strUserId;
			string password = strPassWord;
			string body = "Dear Sir/Madam,\n      \n" + text + "\n       \n        \nThanks and Regards\nAccounts Team\n" + strFirm;
			try
			{
				using MailMessage mailMessage = new MailMessage(from, to, text, body);
				mailMessage.Attachments.Add(new Attachment(pdfFilename));
				SmtpClient smtpClient = new SmtpClient(host);
				smtpClient.Port = port;
				smtpClient.Credentials = new NetworkCredential(userName, password);
				smtpClient.EnableSsl = true;
				smtpClient.Send(mailMessage);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void pdfDocumentCurrent()
		{
			FirmNameFindMethod();
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string text = folderPath + "\\" + Utility.uManufacturer + "\\";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			try
			{
				PdfDocument pdfDocument = new PdfDocument();
				pdfDocument.Info.Title = "Profit And Loss Account";
				PdfPage pdfPage = pdfDocument.AddPage();
				pdfPage.Size = PageSize.A4;
				Convert.ToInt32(pdfPage.Width.Value);
				Convert.ToInt32(pdfPage.Height.Value);
				XGraphics xGraphics = XGraphics.FromPdfPage(pdfPage);
				new XTextFormatter(xGraphics);
				XFont font = new XFont("Courier New", 9.0, XFontStyle.Bold);
				XFont font2 = new XFont("Courier New", 8.0, XFontStyle.Bold);
				XFont font3 = new XFont("Courier New", 9.0, XFontStyle.Regular);
				XFont font4 = new XFont("Courier New", 12.0, XFontStyle.Bold | XFontStyle.Underline);
				XPen pen = new XPen(XColors.Black, 0.75);
				XStringFormat xStringFormat = new XStringFormat();
				XStringFormat xStringFormat2 = new XStringFormat();
				xStringFormat.Alignment = XStringAlignment.Center;
				xStringFormat2.Alignment = XStringAlignment.Far;
				xStringFormat2.FormatFlags = XStringFormatFlags.MeasureTrailingSpaces;
				if (!isPrinted)
				{
					xGraphics.DrawLine(pen, 8, 50, 585, 50);
					xGraphics.DrawLine(pen, 8, 50, 8, 800);
					xGraphics.DrawLine(pen, 8, 800, 585, 800);
					xGraphics.DrawLine(pen, 8, 70, 585, 70);
					xGraphics.DrawLine(pen, 585, 50, 585, 800);
					xGraphics.DrawLine(pen, 502, 50, 502, 800);
					xGraphics.DrawLine(pen, 292, 50, 292, 800);
					xGraphics.DrawLine(pen, 200, 50, 200, 800);
					y = 5;
					xGraphics.DrawString(strFirm, font4, XBrushes.Black, new XRect(250.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					y += 35;
					xGraphics.DrawString("Profit and Loss Report From " + dtmFrom.Text + " To " + dtmTo.Text, font, XBrushes.BlueViolet, new XRect(9.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					y += 15;
					xGraphics.DrawString("LIABILITY", font, XBrushes.Black, new XRect(9.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					xGraphics.DrawString("AMOUNT", font, XBrushes.Black, new XRect(202.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					xGraphics.DrawString("ASSET", font, XBrushes.Black, new XRect(294.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					xGraphics.DrawString("AMOUNT", font, XBrushes.Black, new XRect(503.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
					y += 20;
					isPrinted = true;
				}
				while (linesPrinted < livDetails.Items.Count)
				{
					string s = livDetails.Items[linesPrinted].SubItems[0].Text;
					string s2 = livDetails.Items[linesPrinted].SubItems[1].Text;
					string s3 = livDetails.Items[linesPrinted].SubItems[2].Text;
					string s4 = livDetails.Items[linesPrinted].SubItems[3].Text;
					if (livDetails.Items[linesPrinted].SubItems[0].Font.Style == FontStyle.Bold)
					{
						xGraphics.DrawString(s, font2, Brushes.Blue, new Point(10, y));
						xGraphics.DrawString(s2, font2, Brushes.Blue, new Point(291, y - 7), xStringFormat2);
						xGraphics.DrawString(s3, font2, Brushes.Blue, new Point(293, y));
						xGraphics.DrawString(s4, font2, Brushes.Blue, new Point(584, y - 7), xStringFormat2);
					}
					else
					{
						xGraphics.DrawString(s, font3, Brushes.Black, new Point(10, y));
						xGraphics.DrawString(s2, font3, Brushes.Black, new Point(291, y - 7), xStringFormat2);
						xGraphics.DrawString(s3, font3, Brushes.Black, new Point(293, y));
						xGraphics.DrawString(s4, font3, Brushes.Black, new Point(584, y - 7), xStringFormat2);
					}
					linesPrinted++;
					y += 10;
					if (y + 10 > 800)
					{
						y = 55;
						xGraphics.Dispose();
						pdfPage = pdfDocument.AddPage();
						xGraphics = XGraphics.FromPdfPage(pdfPage);
						xGraphics.DrawLine(pen, 8, 50, 585, 50);
						xGraphics.DrawLine(pen, 8, 50, 8, 800);
						xGraphics.DrawLine(pen, 8, 800, 585, 800);
						xGraphics.DrawLine(pen, 585, 50, 585, 800);
						xGraphics.DrawLine(pen, 502, 50, 502, 800);
						xGraphics.DrawLine(pen, 292, 50, 292, 800);
						xGraphics.DrawLine(pen, 200, 50, 200, 800);
					}
				}
				isNotFirst = false;
				linesPrinted = 0;
				pdfFilename = text + "Report.pdf";
				pdfDocument.Save(pdfFilename);
				Process.Start(pdfFilename);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadMailDetailsMethod()
		{
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable("select EmailClaint,ClientPort,EmailAddress,EmailPassWord from PropertySettings");
			foreach (DataRow row in dataTable.Rows)
			{
				strMailClient = row[0].ToString();
				strPort = row[1].ToString();
				strUserId = row[2].ToString();
				strPassWord = row[3].ToString();
			}
		}

		private void FirmNameFindMethod()
		{
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable("SELECT InstituteName,EmailId FROM Institute");
			foreach (DataRow row in dataTable.Rows)
			{
				strFirm = row[0].ToString();
				strEmailId = row[1].ToString();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//ExportToExcelMethod();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			ReportShowMethod();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
			MailMethod();
		}

		private void Profit_and_Lose_Account_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				ReportShowMethod();
			}
			else if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				MailMethod();
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				//ExportToExcelMethod();
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				ClearMethod();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				PrintMethod();
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbOpValidation.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				_ = e.MarginBounds.Left;
				y = e.MarginBounds.Top;
				int num = e.MarginBounds.Bottom;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				if (!isPrinted)
				{
					y = 60;
					e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num - 20);
					num += 40;
					e.Graphics.DrawLine(Pens.Black, 244, y, 244, num);
					e.Graphics.DrawLine(Pens.Black, 395, y, 395, num);
					e.Graphics.DrawLine(Pens.Black, 630, y, 630, num);
					e.Graphics.DrawString(strFirm, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
					e.Graphics.DrawString("Profit and Loss Report From " + dtmFrom.Text + " To " + dtmTo.Text, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
					e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
					e.Graphics.DrawString("LIABILITY", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(9, 70));
					e.Graphics.DrawString("AMOUNT", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(249, 70));
					e.Graphics.DrawString("ASSET", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(400, 70));
					e.Graphics.DrawString("AMOUNT", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(630, 70));
					isPrinted = true;
					y = 100;
				}
				if (isNotFirst)
				{
					y = 15;
					e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num + 25);
					num += 40;
					e.Graphics.DrawLine(Pens.Black, 244, y, 244, num);
					e.Graphics.DrawLine(Pens.Black, 395, y, 395, num);
					e.Graphics.DrawLine(Pens.Black, 630, y, 630, num);
				}
				while (true)
				{
					if (linesPrinted < livDetails.Items.Count)
					{
						string s = livDetails.Items[linesPrinted].SubItems[0].Text;
						string s2 = livDetails.Items[linesPrinted].SubItems[1].Text;
						string s3 = livDetails.Items[linesPrinted].SubItems[2].Text;
						string s4 = livDetails.Items[linesPrinted].SubItems[3].Text;
						if (livDetails.Items[linesPrinted].SubItems[0].Font.Style == FontStyle.Bold)
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(8f, y, 230f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(395, y), stringFormat);
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(775, y), stringFormat);
						}
						else
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(8f, y, 230f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(395, y), stringFormat);
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(775, y), stringFormat);
						}
						linesPrinted++;
						y += 20;
						if (y >= e.MarginBounds.Bottom + 30)
						{
							break;
						}
						e.Graphics.DrawLine(Pens.Black, 8, y - 2, 776, y - 2);
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

		private static void DisplayListInColumns(ListView listView, List<string> values, int columnIndex)
		{
			for (int i = 0; i < values.Count; i++)
			{
				while (i >= listView.Items.Count)
				{
					listView.Items.Add(new ListViewItem());
				}
				ListViewItem listViewItem = listView.Items[i];
				while (listViewItem.SubItems.Count <= columnIndex)
				{
					listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem());
				}
				listViewItem.SubItems[columnIndex].Text = values[i];
			}
		}

		private void Profit_and_Lose_Account_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			strConnection = Settings.Default.strConnetionString;
			FirmNameFindMethod();
			cmbOpValidation.SelectedIndex = 1;
			cmbClValidation.SelectedIndex = 0;
			cmbRType.SelectedIndex = 0;
			if (PassingValue != null)
			{
				cmbRType.SelectedIndex = 1;
				dtmTo.Text = PassingValue;
				ReportShowMethod();
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = null;
			if (livDetails.SelectedItems.Count == 0)
			{
				return;
			}
			string text2 = livDetails.SelectedItems[0].SubItems[0].Text;
			text = livDetails.SelectedItems[0].SubItems[2].Text;
			if (text2 != " " && text != " " && text2 != "" && text != "")
			{
				if (!text2.Contains("TOTAL"))
				{
					DialogResult dialogResult = MessageBox.Show("If want to select '" + text2 + "' please press Yes. Or press No.", strFirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						strPassingValue = text2;
					}
					else
					{
						strPassingValue = text;
					}
				}
			}
			else if (!text2.Contains("TOTAL"))
			{
				if (text2 != " ")
				{
					strPassingValue = text2;
				}
				if (text != " ")
				{
					strPassingValue = text;
				}
			}
			if (strPassingValue == null || strPassingValue.Contains("PROFIT") || strPassingValue.Contains("LOSS"))
			{
				return;
			}
			if (strPassingValue.Contains("STOCK"))
			{
				if (strPassingValue == "OPENING STOCK" && cmbRType.SelectedIndex == 0)
				{
					SReprt = new StockReportView();
					SReprt.PassingValue = dtmFrom.Value.AddDays(-1.0).ToString();
					SReprt.MdiParent = base.MdiParent;
					SReprt.Show();
				}
				if (strPassingValue == "CLOSING STOCK")
				{
					SReprt = new StockReportView();
					SReprt.PassingValue = dtmTo.Text;
					SReprt.MdiParent = base.MdiParent;
					SReprt.Show();
				}
			}
			else
			{
				ACCgRPrPT = new AccountGroupReport();
				ACCgRPrPT.MdiParent = base.MdiParent;
				ACCgRPrPT.PassingValue = strPassingValue;
				ACCgRPrPT.PassingValueTwo = AniHelper.GetDate(dtmFrom.Value);
				ACCgRPrPT.PassingValueThree = dtmTo.Text;
				if (cmbRType.SelectedIndex == 0)
				{
					ACCgRPrPT.PassingValueFour = true;
				}
				else
				{
					ACCgRPrPT.PassingValueFour = false;
				}
				ACCgRPrPT.Show();
			}
		}

		private void cmbRType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbRType.SelectedIndex == 0)
			{
				label6.Visible = true;
				dtmFrom.Visible = true;
				label5.Text = "To:";
			}
			else
			{
				label6.Visible = false;
				dtmFrom.Visible = false;
				label5.Text = "As on:";
				dtmFrom.Value = dtmTo.Value.AddYears(-100);
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
            this.components = new System.ComponentModel.Container();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.livDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbClValidation = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOpValidation = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRType = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // dtmFrom
            // 
            this.dtmFrom.CustomFormat = "dd-MM-yyyy";
            this.dtmFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Location = new System.Drawing.Point(111, 90);
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.Size = new System.Drawing.Size(128, 22);
            this.dtmFrom.TabIndex = 83;
            this.toolTip1.SetToolTip(this.dtmFrom, "Press Alt + F");
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Location = new System.Drawing.Point(443, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 30);
            this.btnShow.TabIndex = 86;
            this.btnShow.Text = "Show";
            this.toolTip1.SetToolTip(this.btnShow, "Press Alt + S");
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(791, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 90;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Press Alt + C");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(878, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 91;
            this.btnClose.Text = "Close";
            this.toolTip1.SetToolTip(this.btnClose, "Press Alt + X");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(704, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 30);
            this.btnExcel.TabIndex = 89;
            this.btnExcel.Text = "Excel";
            this.toolTip1.SetToolTip(this.btnExcel, "Press Alt + E");
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnMail
            // 
            this.btnMail.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.Location = new System.Drawing.Point(617, 12);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(75, 30);
            this.btnMail.TabIndex = 88;
            this.btnMail.Text = "Mail";
            this.toolTip1.SetToolTip(this.btnMail, "Press Alt + M");
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(530, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 30);
            this.btnPrint.TabIndex = 87;
            this.btnPrint.Text = "Print";
            this.toolTip1.SetToolTip(this.btnPrint, "Press Alt + P");
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtmTo
            // 
            this.dtmTo.CustomFormat = "dd-MM-yyyy";
            this.dtmTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Location = new System.Drawing.Point(327, 95);
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.Size = new System.Drawing.Size(139, 22);
            this.dtmTo.TabIndex = 85;
            this.toolTip1.SetToolTip(this.dtmTo, "Press Alt + T");
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.livDetails);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(13, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 255);
            this.panel1.TabIndex = 92;
            // 
            // livDetails
            // 
            this.livDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.livDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.livDetails.FullRowSelect = true;
            this.livDetails.GridLines = true;
            this.livDetails.HideSelection = false;
            this.livDetails.Location = new System.Drawing.Point(12, 13);
            this.livDetails.Name = "livDetails";
            this.livDetails.Size = new System.Drawing.Size(906, 228);
            this.livDetails.TabIndex = 0;
            this.livDetails.UseCompatibleStateImageBehavior = false;
            this.livDetails.View = System.Windows.Forms.View.Details;
            this.livDetails.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Particulars";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amount";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Particulars";
            this.columnHeader3.Width = 300;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Amount";
            this.columnHeader4.Width = 150;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(274, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 16);
            this.label5.TabIndex = 84;
            this.label5.Text = "To :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 82;
            this.label6.Text = "From :";
            // 
            // cmbClValidation
            // 
            this.cmbClValidation.EnterColor = System.Drawing.Color.LightPink;
            this.cmbClValidation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClValidation.FormattingEnabled = true;
            this.cmbClValidation.Items.AddRange(new object[] {
            "Selling Price",
            "Purchase Price",
            "MRP",
            "Last Selling Price",
            "Last Purchase Price",
            "Average Selling Price",
            "Average Purchase Price",
            "Highest Selling Price",
            "Highest Purchase Price"});
            this.cmbClValidation.LeaveColor = System.Drawing.Color.White;
            this.cmbClValidation.Location = new System.Drawing.Point(758, 89);
            this.cmbClValidation.Name = "cmbClValidation";
            this.cmbClValidation.Size = new System.Drawing.Size(166, 24);
            this.cmbClValidation.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(540, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 16);
            this.label1.TabIndex = 98;
            this.label1.Text = "Closing Stock Validation Using :";
            // 
            // cmbOpValidation
            // 
            this.cmbOpValidation.EnterColor = System.Drawing.Color.LightPink;
            this.cmbOpValidation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOpValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOpValidation.FormattingEnabled = true;
            this.cmbOpValidation.Items.AddRange(new object[] {
            "Selling Price",
            "Purchase Price",
            "MRP",
            "Last Selling Price",
            "Last Purchase Price",
            "Average Selling Price",
            "Average Purchase Price",
            "Highest Selling Price",
            "Highest Purchase Price"});
            this.cmbOpValidation.LeaveColor = System.Drawing.Color.White;
            this.cmbOpValidation.Location = new System.Drawing.Point(758, 59);
            this.cmbOpValidation.Name = "cmbOpValidation";
            this.cmbOpValidation.Size = new System.Drawing.Size(166, 24);
            this.cmbOpValidation.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(560, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 16);
            this.label3.TabIndex = 98;
            this.label3.Text = "Opp Stock Validation Using :";
            // 
            // cmbRType
            // 
            this.cmbRType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRType.FormattingEnabled = true;
            this.cmbRType.Items.AddRange(new object[] {
            "Periodical Report",
            "Report as on Date"});
            this.cmbRType.LeaveColor = System.Drawing.Color.White;
            this.cmbRType.Location = new System.Drawing.Point(111, 59);
            this.cmbRType.Name = "cmbRType";
            this.cmbRType.Size = new System.Drawing.Size(126, 24);
            this.cmbRType.TabIndex = 97;
            this.cmbRType.SelectedIndexChanged += new System.EventHandler(this.cmbRType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 98;
            this.label2.Text = "Report Type :";
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Location = new System.Drawing.Point(314, 61);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(152, 24);
            this.cmbBranch.TabIndex = 104;
            this.cmbBranch.Visible = false;
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.Location = new System.Drawing.Point(246, 65);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(56, 16);
            this.lblBranch.TabIndex = 103;
            this.lblBranch.Text = "Branch :";
            this.lblBranch.Visible = false;
            // 
            // Profit_and_Lose_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1349, 387);
            this.ControlBox = false;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cmbClValidation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRType);
            this.Controls.Add(this.cmbOpValidation);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Profit_and_Lose_Account";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profit And Loss Account";
            this.Load += new System.EventHandler(this.Profit_and_Lose_Account_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Profit_and_Lose_Account_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
