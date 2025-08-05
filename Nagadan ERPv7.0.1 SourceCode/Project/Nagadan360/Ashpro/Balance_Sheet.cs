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
using excel = Microsoft.Office.Interop.Excel;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace Ashpro
{
	public class Balance_Sheet : Form
	{
		private string strMailClient;

		private string strPort;

		private string strUserId;

		private string strPassWord;

		private string pdfFilename;

		private string strConnection;

		private string strFirm;

		private string strEmailId;

		private string Query;

		private string strHead;

		private string strHeadAmount;

		private string strFirst;

		private string strSecond;

		private string strThird;

		private string strFour;

		private decimal dmlHeadAmnt;

		private decimal dmlBHeadTotal;

		private decimal dmlUTotal;

		private decimal dmlCloStckValue;

		private decimal dmlProAndLossAct;

		private decimal dmlCurLib;

		private decimal dmlLongLib;

		private decimal dmlCurAss;

		private decimal dmlFixAss;

		private decimal dmlAsstTotal;

		private decimal dmlLibTotal;

		private decimal dmlDiffAmnt;

		private List<string> listOne;

		private List<string> listTwo;

		private List<string> listThree;

		private List<string> listFour;

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

		private decimal dmlStock;

		private string strAccountName;

		private string strAccountId;

		private string strHeadTwo;

		private decimal dmlHeadAmntTwo;

		private decimal dmlUTotalTwo;

		private string strAccountBalance;

		private string strProId;

		private string strProName;

		private decimal dmlProRate;

		private string strFYearId;

		private string strFromDate;

		private string strPassingValue;

		private AccountGroupReport ACCgRPrPT;

		private StockReportView SReprt;

		private Profit_and_Lose_Account pfAcct;

		private LedgerReport ledgerReport;

		private Product_Flow_Report_Settings ProFLReport;

		private bool isPrinted;

		private bool isNotFirst;

		private int linesPrinted;

		private int y;

		private int? iBranchId = null;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private ToolTip toolTip1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader1;

		private ListView livDetails;

		private CheckBox cbDetailed;

		private Label label6;

		private PrintDocument printDocument1;

		private Panel panel1;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private CheckBox cbShowCloStck;

		private Label label1;

		private Label label2;

		private Button btnClose;

		private Button btnExcel;

		private Button btnClear;

		private Button btnMail;

		private Button btnShow;

		private Button btnPrint;

		private AshDateTimePicker dtmDate;

		private AshComboBox cmbClosingStock;

		private AshComboBox cmbOppStock;

		private Label lblBranch;

		private AshComboBox cmbBranch;

		public Balance_Sheet()
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
			PrintController printController = new StandardPrintController();
			printDocument1.PrintController = printController;
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

		private void ClearMethod()
		{
			livDetails.Items.Clear();
			panel1.Size = new Size(923, 526);
			livDetails.Parent = panel1;
			livDetails.Location = new Point(9, 12);
			livDetails.Size = new Size(906, 500);
			cbShowCloStck.Visible = false;
			cbDetailed.Checked = false;
			cbShowCloStck.Checked = false;
			cmbOppStock.SelectedIndex = 0;
			dtmDate.Value = DateTime.Now;
		}

		//private void ExportToExcelMethod()
		//{
		//	//Discarded unreachable code: IL_0057, IL_0066, IL_0076, IL_051d
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
		//	Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		//	/*Error near IL_0019: stloc 0 (out-of-bounds)*/;
		//	_ = ((_Application)/*Error near IL_001a: ldloc 0 (out-of-bounds)*/).Workbooks;
		//	((Workbooks)XlSheetType.xlWorksheet).Add();
		//	if ((int)_003C_003Eo__69._003C_003Ep__0 == 0)
		//	{
		//		Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(_003F), typeof(Balance_Sheet));
		//		/*Error near IL_0052: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//	}
		//	_ = _003C_003Eo__69._003C_003Ep__0;
		//	/*Error near IL_0061: Specified handle is not a TypeDefinitionHandle, TypeRefererenceHandle, or TypeSpecificationHandle.*/;
		//}

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
			string text = "Balance Sheet Report as on " + dtmDate.Text;
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
				pdfDocument.Info.Title = "Balance Sheet";
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
					xGraphics.DrawString("Balance Sheet Report as on " + dtmDate.Text, font, XBrushes.BlueViolet, new XRect(9.0, y, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
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
					}
					else
					{
						xGraphics.DrawString(s, font3, Brushes.Black, new Point(10, y));
						xGraphics.DrawString(s2, font3, Brushes.Black, new Point(291, y - 7), xStringFormat2);
					}
					if (livDetails.Items[linesPrinted].SubItems[2].Font.Style != FontStyle.Bold && !(livDetails.Items[linesPrinted].SubItems[2].Text == "TOTAL"))
					{
						xGraphics.DrawString(s3, font3, Brushes.Black, new Point(293, y));
						xGraphics.DrawString(s4, font3, Brushes.Black, new Point(584, y - 7), xStringFormat2);
					}
					else
					{
						xGraphics.DrawString(s3, font2, Brushes.Blue, new Point(293, y));
						xGraphics.DrawString(s4, font2, Brushes.Blue, new Point(584, y - 7), xStringFormat2);
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

		private void ReportShowMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				iBranchId = cmbBranch.SelectedValue.ToInt32();
				sBranchDetails = " and FK_BranchId='" + cmbBranch.SelectedValue.ToInt32() + "'";
			}
			livDetails.Items.Clear();
			try
			{
				listOne = new List<string>();
				listTwo = new List<string>();
				listThree = new List<string>();
				listFour = new List<string>();
				listOne.Clear();
				listTwo.Clear();
				listThree.Clear();
				listFour.Clear();
				listOne.Add("");
				listTwo.Add("");
				listThree.Add("");
				listFour.Add("");
				dmlCurLib = LiabilityMethod("CURRENT LIABILITY");
				dmlLongLib = LiabilityMethod("LONG TERM LIABILITY");
				dmlCurAss = LiabilityMethod("CURRENT ASSET");
				dmlFixAss = LiabilityMethod("FIXED ASSET");
				dmlProAndLossAct = ProfitAndLoseAccountMethod();
				listOne.Add("");
				listTwo.Add("");
				listThree.Add("");
				listFour.Add("");
				DisplayListInColumns(livDetails, listOne, 0);
				DisplayListInColumns(livDetails, listTwo, 1);
				DisplayListInColumns(livDetails, listThree, 2);
				DisplayListInColumns(livDetails, listFour, 3);
				if (dmlProAndLossAct >= 0m)
				{
					strFirst = "PROFIT & LOSS A/C";
					strSecond = dmlProAndLossAct.ToString(CommonSettings.strCurencyFormat);
					strThird = " ";
					strFour = " ";
					dmlLibTotal = dmlCurLib + dmlLongLib + dmlProAndLossAct;
					dmlAsstTotal = dmlCurAss + dmlFixAss;
				}
				else
				{
					dmlProAndLossAct = Math.Abs(dmlProAndLossAct);
					strFirst = " ";
					strSecond = " ";
					strThird = "PROFIT & LOSS A/C";
					strFour = dmlProAndLossAct.ToString(CommonSettings.strCurencyFormat);
					dmlLibTotal = dmlCurLib + dmlLongLib;
					dmlAsstTotal = dmlCurAss + dmlFixAss + dmlProAndLossAct;
				}
				ListViewAddMethod(strFirst, strSecond, strThird, strFour);
				ListViewAddMethod(" ", " ", " ", " ");
				if (dmlLibTotal > dmlAsstTotal)
				{
					dmlDiffAmnt = dmlLibTotal - dmlAsstTotal;
					dmlAsstTotal += dmlDiffAmnt;
					strFirst = " ";
					strSecond = " ";
					strThird = "DIFFERANCE IN OPPENING BALANCE";
					strFour = dmlDiffAmnt.ToString(CommonSettings.strCurencyFormat);
				}
				else if (dmlAsstTotal > dmlLibTotal)
				{
					dmlDiffAmnt = dmlAsstTotal - dmlLibTotal;
					dmlLibTotal += dmlDiffAmnt;
					strFirst = "DIFFERANCE IN OPPENING BALANCE";
					strSecond = dmlDiffAmnt.ToString(CommonSettings.strCurencyFormat);
					strThird = " ";
					strFour = " ";
				}
				else
				{
					strFirst = " ";
					strSecond = " ";
					strThird = " ";
					strFour = " ";
				}
				ListViewAddMethod(strFirst, strSecond, strThird, strFour);
				ListViewAddMethod(" ", " ", " ", " ");
				ListViewAddMethod("TOTAL", dmlLibTotal.ToString("F"), "TOTAL", dmlAsstTotal.ToString("F"));
				FontFamily family = new FontFamily(livDetails.Items[0].SubItems[1].Font.Name);
				int num = 0;
				for (int i = 0; i < livDetails.Items.Count; i++)
				{
					if (livDetails.Items[i].SubItems[0].Text == "CURRENT LIABILITY" || livDetails.Items[i].SubItems[0].Text == "LONG TERM LIABILITY")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[0].ForeColor = Color.DodgerBlue;
						livDetails.Items[i].SubItems[0].Font = new Font(family, 10f, FontStyle.Bold);
						livDetails.Items[i].SubItems[1].ForeColor = Color.DodgerBlue;
						livDetails.Items[i].SubItems[1].Font = new Font(family, 10f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[2].Text == "CURRENT ASSET" || livDetails.Items[i].SubItems[2].Text == "FIXED ASSET")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[2].ForeColor = Color.DodgerBlue;
						livDetails.Items[i].SubItems[2].Font = new Font(family, 10f, FontStyle.Bold);
						livDetails.Items[i].SubItems[3].ForeColor = Color.DodgerBlue;
						livDetails.Items[i].SubItems[3].Font = new Font(family, 10f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[0].Text == "PROFIT & LOSS A/C" || livDetails.Items[i].SubItems[0].Text == "DIFFERANCE IN OPPENING BALANCE")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[0].ForeColor = Color.Red;
						livDetails.Items[i].SubItems[0].Font = new Font(family, 10f, FontStyle.Bold);
						livDetails.Items[i].SubItems[1].ForeColor = Color.Red;
						livDetails.Items[i].SubItems[1].Font = new Font(family, 10f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[2].Text == "PROFIT & LOSS A/C" || livDetails.Items[i].SubItems[2].Text == "DIFFERANCE IN OPPENING BALANCE")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[2].ForeColor = Color.Red;
						livDetails.Items[i].SubItems[2].Font = new Font(family, 10f, FontStyle.Bold);
						livDetails.Items[i].SubItems[3].ForeColor = Color.Red;
						livDetails.Items[i].SubItems[3].Font = new Font(family, 10f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[0].Text == "TOTAL")
					{
						livDetails.Items[i].SubItems[0].BackColor = Color.Black;
						livDetails.Items[i].SubItems[0].ForeColor = Color.White;
						livDetails.Items[i].SubItems[0].Font = new Font(family, 10f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[0].Text != "" && livDetails.Items[i].SubItems[1].Text == " ")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[0].ForeColor = Color.Blue;
						livDetails.Items[i].SubItems[0].Font = new Font(family, 9f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[2].Text != "" && livDetails.Items[i].SubItems[3].Text == " ")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[2].ForeColor = Color.Blue;
						livDetails.Items[i].SubItems[2].Font = new Font(family, 9f, FontStyle.Bold);
					}
					if (livDetails.Items[i].SubItems[2].Text == "   CLOSING STOCK")
					{
						livDetails.Items[i].UseItemStyleForSubItems = false;
						livDetails.Items[i].SubItems[2].ForeColor = Color.Blue;
						livDetails.Items[i].SubItems[2].Font = new Font(family, 10f, FontStyle.Bold);
						livDetails.Items[i].SubItems[3].ForeColor = Color.Blue;
						livDetails.Items[i].SubItems[3].Font = new Font(family, 10f, FontStyle.Bold);
					}
				}
				if (!cbDetailed.Checked)
				{
					num = livDetails.Items.Count * 24;
					panel1.Size = new Size(780, num + 25);
					livDetails.Parent = panel1;
					livDetails.Location = new Point(9, 12);
					livDetails.Size = new Size(766, num);
				}
				else
				{
					panel1.Size = new Size(780, 444);
					livDetails.Parent = panel1;
					livDetails.Location = new Point(9, 12);
					livDetails.Size = new Size(766, 427);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "   398");
			}
		}

		private void ListViewAddMethod(string First, string Second, string Third, string Four)
		{
			ListViewItem listViewItem = new ListViewItem(First);
			listViewItem.SubItems.Add(Second);
			listViewItem.SubItems.Add(Third);
			listViewItem.SubItems.Add(Four);
			livDetails.Items.Add(listViewItem);
		}

		private decimal ProfitAndLoseAccountMethod()
		{
			decimal num = default(decimal);
			OppStockValue = OppStockValueFindMethodTwo();
			PurchaseValue = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "PURCHASE ACCOUNT");
			PurchaseRtnVal = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "PURCHASE RETURN ACCOUNT");
			PurchaseValue -= PurchaseRtnVal;
			SalesValue = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "SALES ACCOUNT");
			SalRtnValue = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "SALES RETURN ACCOUNT");
			SalesValue -= SalRtnValue;
			ClosStockValue = ClosingStckFindMethodTwo(isAddValue: false);
			DirectExp = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "DIRECT EXPENSES");
			DirectIncom = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "DIRECT INCOME");
			IndirectExp = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "INDIRECT EXPENSES");
			IndirectIncome = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, "INDIRECT INCOME");
			return SalesValue + ClosStockValue + DirectIncom + IndirectIncome - (OppStockValue + PurchaseValue + DirectExp + IndirectExp);
		}

		private decimal ValueFindMethod(string strQuery)
		{
			decimal? num = default(decimal);
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				num = ((row[0] == DBNull.Value) ? (num + (decimal?)default(decimal)) : (num + (decimal?)Convert.ToDecimal(row[0].ToString() ?? "0")));
			}
			return num.Value;
		}

		private decimal ClosingStckFindMethodTwo(bool isAddValue)
		{
			return AshproCommon.GetDecimalDataWithParameter_SP("spProfitAndLossOppeningStock", new
			{
				Validation = cmbOppStock.Text,
				BranchId = iBranchId,
				Date = dtmDate.Value.Date.AddHours(24.0)
			});
		}

		private decimal RateFindMethod(string strProId, ComboBox combow)
		{
			decimal? num = null;
			if (combow.SelectedIndex == 0)
			{
				Query = "Select SellingPrice From Product Where ProductId='" + strProId + "'";
			}
			else if (combow.SelectedIndex == 1)
			{
				Query = "Select PurchasePrice From Product Where ProductId='" + strProId + "'";
			}
			else if (combow.SelectedIndex == 2)
			{
				Query = "Select MRP From Product Where ProductId='" + strProId + "'";
			}
			else if (combow.SelectedIndex == 3)
			{
				Query = "Select TOP (1) ProRate From SalesEstimateSettings Where ProductId='" + strProId + "' ORDER BY ProductId DESC ";
			}
			else if (combow.SelectedIndex == 4)
			{
				Query = "Select TOP (1) ProRate From PurInvSettings Where ProId='" + strProId + "' ORDER BY ProId DESC ";
			}
			else if (combow.SelectedIndex == 5)
			{
				Query = "Select SUM(ProRate * ProQty) / SUM(ProQty) AS ProRate From SalesEstimateSettings Where ProductId='" + strProId + "'";
			}
			else if (combow.SelectedIndex == 6)
			{
				Query = "Select SUM(ProRate * ProQty) / SUM(ProQty) AS ProRate From PurInvSettings Where ProId='" + strProId + "'";
			}
			else if (combow.SelectedIndex == 7)
			{
				Query = "Select ProRate From SalesEstimateSettings Where SalesSettingsId = (Select min(SalesSettingsId) From SalesEstimateSettings where ProductId='" + strProId + "')";
			}
			else
			{
				Query = "Select ProRate From PurInvSettings Where PurSettingsId = (Select min(PurSettingsId) From PurInvSettings where ProId='" + strProId + "')";
			}
			num = AshproCommon.ValueFindMethod(Query);
			decimal? num2 = num;
			if ((num2.GetValueOrDefault() == default(decimal)) & num2.HasValue)
			{
				num = RateMethodTwo(strProId, combow);
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

		private decimal OppStockValueFindMethodTwo()
		{
			return AshproCommon.GetDecimalDataWithParameter_SP("spBalanceSheetOppeningStock", new
			{
				Validation = cmbOppStock.Text,
				BranchId = iBranchId,
				Date = dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0)
			});
		}

		private decimal LiabilityMethod(string strTitle)
		{
			dmlBHeadTotal = default(decimal);
			dmlUTotal = TotalFindMethod(strTitle);
			switch (strTitle)
			{
			case "CURRENT LIABILITY":
				if (dmlUTotal != 0m)
				{
					listOne.Add(strTitle);
					listTwo.Add(dmlUTotal.ToString("F"));
				}
				break;
			case "LONG TERM LIABILITY":
				dmlUTotalTwo = TotalFindMethod("CAPITAL ACCOUNT");
				dmlUTotal -= dmlUTotalTwo;
				if (dmlUTotal != 0m)
				{
					listOne.Add(strTitle);
					listTwo.Add(dmlUTotal.ToString("F"));
				}
				break;
			case "CURRENT ASSET":
				dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: false);
				dmlUTotal += dmlCloStckValue;
				goto default;
			default:
				if (dmlUTotal != 0m)
				{
					listThree.Add(strTitle);
					listFour.Add(dmlUTotal.ToString("F"));
				}
				break;
			}
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable("Select GroupName from AccountGroup where Under='" + strTitle + "'");
			foreach (DataRow row in dataTable.Rows)
			{
				strHead = row[0].ToString();
				if (!(strTitle == "CURRENT LIABILITY") && !(strTitle == "LONG TERM LIABILITY"))
				{
					dmlHeadAmnt = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);
					strHeadAmount = dmlHeadAmnt.ToString(CommonSettings.strCurencyFormat);
					if (!cbDetailed.Checked)
					{
						if (dmlHeadAmnt != 0m)
						{
							dmlBHeadTotal += dmlHeadAmnt;
							listThree.Add(strHead);
							listFour.Add(strHeadAmount);
						}
					}
					else if (dmlHeadAmnt != 0m)
					{
						listThree.Add(" ");
						listFour.Add(" ");
						listThree.Add(strHead);
						listFour.Add(" ");
					}
				}
				else
				{
					if (strHead == "CAPITAL ACCOUNT")
					{
						dmlHeadAmnt = CapitalAccountMethod();
					}
					else
					{
						dmlHeadAmnt = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);
					}
					strHeadAmount = dmlHeadAmnt.ToString(CommonSettings.strCurencyFormat);
					if (!cbDetailed.Checked)
					{
						if (dmlHeadAmnt != 0m)
						{
							dmlBHeadTotal += dmlHeadAmnt;
							listOne.Add(strHead);
							listTwo.Add(strHeadAmount);
						}
					}
					else if (dmlHeadAmnt != 0m)
					{
						listOne.Add(" ");
						listTwo.Add(" ");
						listOne.Add(strHead);
						listTwo.Add(" ");
					}
				}
				if (cbDetailed.Checked)
				{
					if (strHead == "CAPITAL ACCOUNT")
					{
						DetailedLedgerMethod(strTitle, strHead);
						DetailedLedgerMethod(strTitle, "DRAWINGS ACCOUNT");
					}
					else
					{
						DetailedLedgerMethod(strTitle, strHead);
					}
				}
			}
			if (strTitle == "CURRENT ASSET")
			{
				if (!cbShowCloStck.Checked)
				{
					dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: false);
					listThree.Add(" ");
					listFour.Add(" ");
					listThree.Add("   CLOSING STOCK");
					listFour.Add(dmlCloStckValue.ToString("F"));
				}
				else
				{
					dmlCloStckValue = ClosingStckFindMethodTwo(isAddValue: true);
					listThree.Add(" ");
					listFour.Add(" ");
					listThree.Add("   CLOSING STOCK");
					listFour.Add(dmlCloStckValue.ToString("F"));
				}
			}
			return dmlUTotal;
		}

		private decimal CapitalAccountMethod()
		{
			decimal num = default(decimal);
			dmlHeadAmntTwo = default(decimal);
			strHead = "CAPITAL ACCOUNT";
			num = AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHead);
			Query = "Select GroupName from AccountGroup where Under='" + strHead + "'";
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query);
			foreach (DataRow row in dataTable.Rows)
			{
				strHeadTwo = row[0].ToString();
				decimal num2 = AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, strHeadTwo);
				dmlHeadAmntTwo += num2;
			}
			return num - dmlHeadAmntTwo;
		}

		private void DetailedLedgerMethod(string Title, string Head)
		{
			try
			{
				Query = "Select AccountId,AccountName From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='" + Head + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					strAccountId = row[0].ToString();
					strAccountName = row[1].ToString();
					if (!(Title == "CURRENT LIABILITY") && !(Title == "LONG TERM LIABILITY"))
					{
						strAccountBalance = TotalBalanceFindMethod("D", strAccountId.ToInt32()).ToString();
						listThree.Add(strAccountName);
						listFour.Add(strAccountBalance);
					}
					else
					{
						strAccountBalance = TotalBalanceFindMethod("C", strAccountId.ToInt32()).ToString();
						listOne.Add(strAccountName);
						listTwo.Add(strAccountBalance);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "  836");
			}
		}

		private decimal TotalBalanceFindMethod(string sType, int iAcctId)
		{
			return AshproCommon.GetDecimalDataWithParameter_SP("spBalanceSheetBalance", new
			{
				AccountType = sType,
				AccountId = iAcctId,
				BranchId = iBranchId,
				Date = dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0)
			});
		}

		private decimal TotalFindMethod(string strUnder)
		{
			switch (strUnder)
			{
			default:
				return AniHelper.AccountBalanceMethod(1, "D", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, null, strUnder);
			case "CURRENT LIABILITY":
			case "LONG TERM LIABILITY":
			case "DIRECT INCOME":
			case "INDIRECT INCOME":
				return AniHelper.AccountBalanceMethod(1, "C", dtmDate.Value, dtmDate.Value.Date.AddHours(24.0).AddSeconds(-1.0), iBranchId, null, strUnder);
			}
		}

		private decimal StckFindMethod(string strProductId)
		{
			Query = "Select SUM(InQuantity-OutQuantity) as Stock From StockTranLog where ProductId='" + strProductId + "' and VchrDate<='" + AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)) + "'" + sBranchDetails;
			return AshproCommon.ValueFindMethod(Query);
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

		private void FinanceialYearDate()
		{
			try
			{
				strFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			AshproCommon.IdFindMethod("FinancialYear", "DateFrom", "FyearId", strFYearId, out strFromDate);
			DateTime dateTime = Convert.ToDateTime(strFromDate);
			int days = (DateTime.Now - dateTime).Days;
			if (days < 91)
			{
				dateTime = dtmDate.Value.AddDays(-(dtmDate.Value.DayOfYear + 274));
				dateTime = DateTime.Now.AddDays(-(DateTime.Now.DayOfYear + 274));
			}
			strFromDate = dateTime.ToString("dd-MMM-yyyy");
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

		private void Balance_Sheet_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			strConnection = Settings.Default.strConnetionString;
			FirmNameFindMethod();
			cmbOppStock.SelectedIndex = 1;
			cmbClosingStock.SelectedIndex = 0;
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
			iBranchId = null;
			ReportShowMethod();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = null;
			strPassingValue = null;
			if (livDetails.SelectedItems.Count == 0)
			{
				return;
			}
			string text2 = livDetails.SelectedItems[0].SubItems[0].Text;
			string text3 = livDetails.SelectedItems[0].SubItems[1].Text;
			string text4 = livDetails.SelectedItems[0].SubItems[2].Text;
			text = livDetails.SelectedItems[0].SubItems[3].Text;
			if (cbDetailed.Checked)
			{
				if (text2 != " " && text4 != " " && text2 != "" && text4 != "" && text3 != " " && text != " ")
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
							strPassingValue = text4;
						}
					}
				}
				else if (text2 != " " && text2 != "" && text3 != " ")
				{
					strPassingValue = text2;
				}
				else if (text4 != " " && text4 != "" && text != " ")
				{
					strPassingValue = text4;
				}
			}
			else if (text2 != " " && text4 != " " && text2 != "" && text4 != "")
			{
				if (!text2.Contains("TOTAL"))
				{
					DialogResult dialogResult2 = MessageBox.Show("If want to select '" + text2 + "' please press Yes. Or press No.", strFirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult2 == DialogResult.Yes)
					{
						strPassingValue = text2;
					}
					else
					{
						strPassingValue = text4;
					}
				}
			}
			else if (text2 != " " && text2 != "")
			{
				strPassingValue = text2;
			}
			else if (text4 != " " && text4 != "")
			{
				strPassingValue = text4;
			}
			if (strPassingValue == null)
			{
				return;
			}
			if (strPassingValue.Contains("CLOSING STOCK"))
			{
				if (!cbShowCloStck.Checked)
				{
					SReprt = new StockReportView();
					SReprt.MdiParent = base.MdiParent;
					SReprt.PassingValue = AniHelper.GetDate(dtmDate.Value);
					SReprt.Show();
				}
			}
			else if (strPassingValue.Contains("PROFIT & LOSS A/C"))
			{
				pfAcct = new Profit_and_Lose_Account();
				pfAcct.MdiParent = base.MdiParent;
				pfAcct.PassingValue = AniHelper.GetDate(dtmDate.Value);
				pfAcct.Show();
			}
			else if (cbDetailed.Checked)
			{
				if (!cbShowCloStck.Checked)
				{
					FinanceialYearDate();
					ledgerReport = new LedgerReport();
					ledgerReport.MdiParent = base.MdiParent;
					ledgerReport.ValueFromDate = strFromDate;
					ledgerReport.ValueToDate = AniHelper.GetDate(dtmDate.Value);
					ledgerReport.ValueLedger = strPassingValue;
					ledgerReport.Show();
					return;
				}
				FinanceialYearDate();
				if (!strPassingValue.Contains("     "))
				{
					ledgerReport = new LedgerReport();
					ledgerReport.MdiParent = base.MdiParent;
					ledgerReport.ValueFromDate = strFromDate;
					ledgerReport.ValueToDate = AniHelper.GetDate(dtmDate.Value);
					ledgerReport.ValueLedger = strPassingValue;
					ledgerReport.Show();
				}
				else
				{
					strPassingValue = strPassingValue.Replace("     ", null);
					ProFLReport = new Product_Flow_Report_Settings();
					ProFLReport.MdiParent = base.MdiParent;
					ProFLReport.Show();
				}
			}
			else
			{
				ACCgRPrPT = new AccountGroupReport();
				ACCgRPrPT.MdiParent = base.MdiParent;
				ACCgRPrPT.PassingValue = strPassingValue;
				ACCgRPrPT.PassingValueTwo = "";
				ACCgRPrPT.PassingValueThree = AniHelper.GetDate(dtmDate.Value);
				ACCgRPrPT.PassingValueFour = false;
				ACCgRPrPT.Show();
			}
		}

		private void cbDetailed_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
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
					e.Graphics.DrawString("Balance Sheet Report as on " + dtmDate.Text, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
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
						}
						else
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(8f, y, 230f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(395, y), stringFormat);
						}
						if (livDetails.Items[linesPrinted].SubItems[2].Font.Style != FontStyle.Bold && !(livDetails.Items[linesPrinted].SubItems[2].Text == "TOTAL"))
						{
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(775, y), stringFormat);
						}
						else
						{
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(402f, y, 230f, 15f));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(775, y), stringFormat);
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

		private void btnMail_Click(object sender, EventArgs e)
		{
			MailMethod();
		}

		private void Balance_Sheet_KeyDown(object sender, KeyEventArgs e)
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
				cmbOppStock.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Balance_Sheet));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.livDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbDetailed = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbShowCloStck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.cmbClosingStock = new AshUsercontrol.AshComboBox();
            this.cmbOppStock = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // livDetails
            // 
            resources.ApplyResources(this.livDetails, "livDetails");
            this.livDetails.BackColor = System.Drawing.Color.White;
            this.livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5});
            this.livDetails.FullRowSelect = true;
            this.livDetails.GridLines = true;
            this.livDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.livDetails.HideSelection = false;
            this.livDetails.Name = "livDetails";
            this.toolTip1.SetToolTip(this.livDetails, resources.GetString("livDetails.ToolTip"));
            this.livDetails.UseCompatibleStateImageBehavior = false;
            this.livDetails.View = System.Windows.Forms.View.Details;
            this.livDetails.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // cbDetailed
            // 
            resources.ApplyResources(this.cbDetailed, "cbDetailed");
            this.cbDetailed.Name = "cbDetailed";
            this.toolTip1.SetToolTip(this.cbDetailed, resources.GetString("cbDetailed.ToolTip"));
            this.cbDetailed.UseVisualStyleBackColor = true;
            this.cbDetailed.CheckedChanged += new System.EventHandler(this.cbDetailed_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.livDetails);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // cbShowCloStck
            // 
            resources.ApplyResources(this.cbShowCloStck, "cbShowCloStck");
            this.cbShowCloStck.Name = "cbShowCloStck";
            this.toolTip1.SetToolTip(this.cbShowCloStck, resources.GetString("cbShowCloStck.ToolTip"));
            this.cbShowCloStck.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExcel
            // 
            resources.ApplyResources(this.btnExcel, "btnExcel");
            this.btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Name = "btnExcel";
            this.toolTip1.SetToolTip(this.btnExcel, resources.GetString("btnExcel.ToolTip"));
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMail
            // 
            resources.ApplyResources(this.btnMail, "btnMail");
            this.btnMail.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMail.FlatAppearance.BorderSize = 0;
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.Name = "btnMail";
            this.toolTip1.SetToolTip(this.btnMail, resources.GetString("btnMail.ToolTip"));
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.FlatAppearance.BorderSize = 0;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.toolTip1.SetToolTip(this.btnShow, resources.GetString("btnShow.ToolTip"));
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.toolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.toolTip1.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // cmbClosingStock
            // 
            resources.ApplyResources(this.cmbClosingStock, "cmbClosingStock");
            this.cmbClosingStock.EnterColor = System.Drawing.Color.LightPink;
            this.cmbClosingStock.FormattingEnabled = true;
            this.cmbClosingStock.Items.AddRange(new object[] {
            resources.GetString("cmbClosingStock.Items"),
            resources.GetString("cmbClosingStock.Items1"),
            resources.GetString("cmbClosingStock.Items2"),
            resources.GetString("cmbClosingStock.Items3"),
            resources.GetString("cmbClosingStock.Items4"),
            resources.GetString("cmbClosingStock.Items5"),
            resources.GetString("cmbClosingStock.Items6"),
            resources.GetString("cmbClosingStock.Items7"),
            resources.GetString("cmbClosingStock.Items8")});
            this.cmbClosingStock.LeaveColor = System.Drawing.Color.White;
            this.cmbClosingStock.Name = "cmbClosingStock";
            this.toolTip1.SetToolTip(this.cmbClosingStock, resources.GetString("cmbClosingStock.ToolTip"));
            // 
            // cmbOppStock
            // 
            resources.ApplyResources(this.cmbOppStock, "cmbOppStock");
            this.cmbOppStock.EnterColor = System.Drawing.Color.LightPink;
            this.cmbOppStock.FormattingEnabled = true;
            this.cmbOppStock.Items.AddRange(new object[] {
            resources.GetString("cmbOppStock.Items"),
            resources.GetString("cmbOppStock.Items1"),
            resources.GetString("cmbOppStock.Items2"),
            resources.GetString("cmbOppStock.Items3"),
            resources.GetString("cmbOppStock.Items4"),
            resources.GetString("cmbOppStock.Items5"),
            resources.GetString("cmbOppStock.Items6"),
            resources.GetString("cmbOppStock.Items7"),
            resources.GetString("cmbOppStock.Items8")});
            this.cmbOppStock.LeaveColor = System.Drawing.Color.White;
            this.cmbOppStock.Name = "cmbOppStock";
            this.toolTip1.SetToolTip(this.cmbOppStock, resources.GetString("cmbOppStock.ToolTip"));
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            this.toolTip1.SetToolTip(this.lblBranch, resources.GetString("lblBranch.ToolTip"));
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Items.AddRange(new object[] {
            resources.GetString("cmbBranch.Items"),
            resources.GetString("cmbBranch.Items1"),
            resources.GetString("cmbBranch.Items2"),
            resources.GetString("cmbBranch.Items3"),
            resources.GetString("cmbBranch.Items4"),
            resources.GetString("cmbBranch.Items5"),
            resources.GetString("cmbBranch.Items6"),
            resources.GetString("cmbBranch.Items7"),
            resources.GetString("cmbBranch.Items8")});
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.toolTip1.SetToolTip(this.cmbBranch, resources.GetString("cmbBranch.ToolTip"));
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Balance_Sheet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ControlBox = false;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.cmbOppStock);
            this.Controls.Add(this.cmbClosingStock);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShowCloStck);
            this.Controls.Add(this.cbDetailed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Balance_Sheet";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.Balance_Sheet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Balance_Sheet_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
