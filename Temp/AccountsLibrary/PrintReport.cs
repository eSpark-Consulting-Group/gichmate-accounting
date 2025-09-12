using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace AccountsLibrary
{
	public static class PrintReport
	{
		private static int m_currentPageIndex;

		private static IList<Stream> m_streams;

		public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
		{
			Stream stream = new MemoryStream();
			m_streams.Add(stream);
			return stream;
		}

		public static void Export(LocalReport report, bool print = true)
		{
			string deviceInfo = "<DeviceInfo>\r\n                <OutputFormat>EMF</OutputFormat>\r\n                <PageWidth>6.2in</PageWidth>\r\n                <PageHeight>8.3in</PageHeight>\r\n                <MarginTop>0.1in</MarginTop>\r\n                <MarginLeft>0.1in</MarginLeft>\r\n                <MarginRight>0.1in</MarginRight>\r\n                <MarginBottom>0.1in</MarginBottom>\r\n            </DeviceInfo>";
			m_streams = new List<Stream>();
			report.Render("Image", deviceInfo, CreateStream, out var _);
			foreach (Stream stream in m_streams)
			{
				stream.Position = 0L;
			}
			if (print)
			{
				Print();
			}
		}

		public static void PrintPage(object sender, PrintPageEventArgs ev)
		{
			Metafile image = new Metafile(m_streams[m_currentPageIndex]);
			Rectangle rect = new Rectangle(ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX, ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY, ev.PageBounds.Width, ev.PageBounds.Height);
			ev.Graphics.FillRectangle(Brushes.White, rect);
			ev.Graphics.DrawImage(image, rect);
			m_currentPageIndex++;
			ev.HasMorePages = m_currentPageIndex < m_streams.Count;
		}

		public static void Print()
		{
			if (m_streams != null && m_streams.Count != 0)
			{
				PrintDocument printDocument = new PrintDocument();
				if (!printDocument.PrinterSettings.IsValid)
				{
					throw new Exception("Error: cannot find the default printer.");
				}
				printDocument.PrintPage += PrintPage;
				m_currentPageIndex = 0;
				printDocument.Print();
				return;
			}
			throw new Exception("Error: no stream to print.");
		}

		public static void PrintToPrinter(this LocalReport report)
		{
			Export(report);
		}

		public static void DisposePrint()
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
