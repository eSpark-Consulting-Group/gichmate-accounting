using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace Ashpro
{
	public class PrintRdlc : IDisposable
	{
		private int m_currentPageIndex;

		private IList<Stream> m_streams;

		private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
		{
			Stream stream = new MemoryStream();
			m_streams.Add(stream);
			return stream;
		}

		private void Export(LocalReport report)
		{
			string deviceInfo = "<DeviceInfo>\r\n                <OutputFormat>EMF</OutputFormat>\r\n                <PageWidth>8in</PageWidth>\r\n                <PageHeight>11in</PageHeight>\r\n                <MarginTop>0.0in</MarginTop>\r\n                <MarginLeft>0.0in</MarginLeft>\r\n                <MarginRight>0.0in</MarginRight>\r\n                <MarginBottom>0.0in</MarginBottom>\r\n            </DeviceInfo>";
			m_streams = new List<Stream>();
			report.Render("Image", deviceInfo, CreateStream, out var _);
			foreach (Stream stream in m_streams)
			{
				stream.Position = 0L;
			}
		}

		private void PrintPage(object sender, PrintPageEventArgs ev)
		{
			Metafile image = new Metafile(m_streams[m_currentPageIndex]);
			Rectangle rect = new Rectangle(ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX, ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY, ev.PageBounds.Width, ev.PageBounds.Height);
			ev.Graphics.FillRectangle(Brushes.White, rect);
			ev.Graphics.DrawImage(image, rect);
			m_currentPageIndex++;
			ev.HasMorePages = m_currentPageIndex < m_streams.Count;
		}

		private void Print(PrinterSettings sett)
		{
			if (m_streams != null && m_streams.Count != 0)
			{
				PrintDocument printDocument = new PrintDocument();
				if (!printDocument.PrinterSettings.IsValid)
				{
					throw new Exception("Error: cannot find the default printer.");
				}
				if (sett != null)
				{
					printDocument.PrinterSettings = sett;
				}
				printDocument.PrintPage += PrintPage;
				m_currentPageIndex = 0;
				printDocument.Print();
				return;
			}
			throw new Exception("Error: no stream to print.");
		}

		public void PrintReport(string sReportPath, PrinterSettings sett, DataSet dsPrint, List<atReportParameter> reportParameters)
		{
			if (dsPrint == null)
			{
				throw new Exception("Data Source Property Cannot be Null!");
			}
			if (sReportPath == "")
			{
				throw new Exception("Invalid Report Path!");
			}
			if (!File.Exists(sReportPath))
			{
				throw new Exception("Report File Not Found!");
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
				throw new Exception("Report Parameters Not Supplied!");
			}
			if (reportParameters != null && reportParameters.ToList().Count > parameters.Count + 6)
			{
				throw new Exception("Supplied Parameter Count is Greater Than Available Parameter in Report!");
			}
			if (reportParameters == null)
			{
				return;
			}
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
