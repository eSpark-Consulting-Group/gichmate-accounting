using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace Ashpro
{
	public class DirectReportPrint : IDisposable
	{
		private PrintDocument printDoc = new PrintDocument();

		private double[] PageBounds = new double[6];

		private int m_currentPageIndex;

		private IList<Stream> m_streams;

		private DataTable _table = new DataTable();

		public DataTable ReportTable
		{
			get
			{
				return _table;
			}
			set
			{
				_table = value;
			}
		}

		public DirectReportPrint()
		{
			PrintController printController = new StandardPrintController();
			printDoc.PrintController = printController;
		}

		private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
		{
			Stream stream = new MemoryStream();
			m_streams.Add(stream);
			return stream;
		}

		private string ReadEmbeddedResource(string ResourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using Stream stream = executingAssembly.GetManifestResourceStream(ResourceName);
			using StreamReader streamReader = new StreamReader(stream);
			string text = streamReader.ReadToEnd();
			return text.Replace('\r', ' ');
		}

		private string ReadReportXML(string ReportName)
		{
			try
			{
				string text = " ";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				using (StreamReader streamReader = new StreamReader(ReportName))
				{
					text4 = streamReader.ReadToEnd();
				}
				if (text4.Contains("<PageHeight>") && text4.Contains("</PageHeight>"))
				{
					int num = text4.IndexOf("<PageHeight>");
					int num2 = text4.IndexOf("</PageHeight>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</PageHeight>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[0] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				if (text4.Contains("<PageWidth>") && text4.Contains("</PageWidth>"))
				{
					int num = text4.IndexOf("<PageWidth>");
					int num2 = text4.IndexOf("</PageWidth>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</PageWidth>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[1] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				if (text4.Contains("<LeftMargin>") && text4.Contains("</LeftMargin>"))
				{
					int num = text4.IndexOf("<LeftMargin>");
					int num2 = text4.IndexOf("</LeftMargin>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</LeftMargin>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[2] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				if (text4.Contains("<RightMargin>") && text4.Contains("</RightMargin>"))
				{
					int num = text4.IndexOf("<RightMargin>");
					int num2 = text4.IndexOf("</RightMargin>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</RightMargin>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[3] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				if (text4.Contains("<TopMargin>") && text4.Contains("</TopMargin>"))
				{
					int num = text4.IndexOf("<TopMargin>");
					int num2 = text4.IndexOf("</TopMargin>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</TopMargin>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[4] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				if (text4.Contains("<BottomMargin>") && text4.Contains("</BottomMargin>"))
				{
					int num = text4.IndexOf("<BottomMargin>");
					int num2 = text4.IndexOf("</BottomMargin>");
					text2 = text4.Substring(num, num2 - num);
					text = text + text2 + "</BottomMargin>";
					int num3 = text2.IndexOf('>') + 1;
					text3 = text2.Substring(num3, text2.Length - num3);
					PageBounds[5] = Math.Round(Convert.ToDouble(text3.Substring(0, text3.Length - 2)), 2);
				}
				return text;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void Export(LocalReport report, string ReportName)
		{
			try
			{
				string text = ReadReportXML(ReportName);
				if (text != null)
				{
					string deviceInfo = "<DeviceInfo>  <OutputFormat>EMF</OutputFormat>" + text + "</DeviceInfo>";
					m_streams = new List<Stream>();
					report.Render("Image", deviceInfo, CreateStream, out var _);
					{
						foreach (Stream stream in m_streams)
						{
							stream.Position = 0L;
						}
						return;
					}
				}
				throw new Exception("Something Went Wrong Unable To Print Report");
			}
			catch (Exception)
			{
			}
		}

		private string ConvertReportToPDF(LocalReport rep, string ReportName, string sFileName)
		{
			string format = "PDF";
			string text = ReadReportXML(ReportName);
			string deviceInfo = "<DeviceInfo>  <OutputFormat>EMF</OutputFormat>" + text + "  <EmbedFonts>None</EmbedFonts></DeviceInfo>";
			byte[] array = rep.Render(format, deviceInfo);
			if (sFileName == null)
			{
				sFileName = AppDomain.CurrentDomain.BaseDirectory;
				string text2 = "Invoice.pdf";
				sFileName += text2;
			}
			using (FileStream fileStream = new FileStream(sFileName, FileMode.Create))
			{
				fileStream.Write(array, 0, array.Length);
			}
			return sFileName;
		}

		private void PrintPage(object sender, PrintPageEventArgs ev)
		{
			try
			{
				Metafile image = new Metafile(m_streams[m_currentPageIndex]);
				Rectangle rect = new Rectangle(ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX, ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY, ev.PageBounds.Width, ev.PageBounds.Height);
				ev.Graphics.FillRectangle(Brushes.White, rect);
				ev.Graphics.DrawImage(image, rect);
				m_currentPageIndex++;
				ev.HasMorePages = m_currentPageIndex < m_streams.Count;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
			}
		}

		private void Print(PrinterSettings sett)
		{
			try
			{
				if (m_streams != null && m_streams.Count != 0)
				{
					if (!printDoc.PrinterSettings.IsValid)
					{
						throw new Exception("Error: cannot find the default printer.");
					}
					if (sett != null)
					{
						printDoc.PrinterSettings = sett;
					}
					printDoc.PrintPage += PrintPage;
					m_currentPageIndex = 0;
					printDoc.Print();
					return;
				}
				throw new Exception("Error: no stream to print.");
			}
			catch (Exception ex)
			{
				_ = ex.Message;
			}
		}

		public void PrintReport(string sReportPath, PrinterSettings sett, DataSet dsPrint, List<atReportParameter> reportParameters, bool generatePdf = false, int iCopy = 1, string sFileName = null, bool isStart = true)
		{
			if (dsPrint == null)
			{
				throw new Exception("Data Source Property Can not Be Null");
			}
			if (sReportPath == "")
			{
				throw new Exception("Invalid Report Path");
			}
			if (!File.Exists(sReportPath))
			{
				throw new Exception("Report File Not Found");
			}
			LocalReport localReport = new LocalReport();
			localReport.ReportPath = sReportPath;
			localReport.EnableHyperlinks = true;
			foreach (DataTable table in dsPrint.Tables)
			{
				localReport.DataSources.Add(new ReportDataSource(table.TableName, table));
			}
			ReportParameterInfoCollection parameters = localReport.GetParameters();
			if (reportParameters == null && parameters.Count > 6)
			{
				throw new Exception("Report Parameters Not Supplied");
			}
			if (reportParameters != null && reportParameters.ToList().Count > parameters.Count + 6)
			{
				throw new Exception("Supplied Parameter Count Is Greater Than Available Parameter In Report");
			}
			if (reportParameters != null)
			{
				foreach (ReportParameterInfo rptParam in localReport.GetParameters())
				{
					if (reportParameters.Where((atReportParameter x) => x.ParameterName == rptParam.Name).ToList().Count > 0)
					{
						atReportParameter atReportParameter2 = new atReportParameter();
						atReportParameter2 = reportParameters.Where((atReportParameter x) => x.ParameterName == rptParam.Name).Single();
						ReportParameter parameters2 = new ReportParameter(atReportParameter2.ParameterName, (atReportParameter2.ParameterValue == "") ? "--" : atReportParameter2.ParameterValue);
						localReport.SetParameters(parameters2);
					}
				}
			}
			if (!generatePdf)
			{
				for (int i = 0; i < iCopy; i++)
				{
					Export(localReport, sReportPath);
					m_currentPageIndex = 0;
					Print(sett);
					Dispose();
				}
			}
			else
			{
				string fileName = ConvertReportToPDF(localReport, sReportPath, sFileName);
				if (isStart)
				{
					Process.Start(fileName);
				}
				Dispose();
			}
		}

		public void Dispose()
		{
			if (m_streams == null)
			{
				return;
			}
			foreach (Stream stream in m_streams)
			{
				stream.Close();
			}
			m_streams = null;
		}
	}
}
