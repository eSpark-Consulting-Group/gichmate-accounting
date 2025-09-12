using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AshFramework;
using BarcodeLib;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace Ashpro
{
	public class ProductBarcode
	{
		private string NumberFormat;

		private PrintDocument pDocu = new PrintDocument();

		private List<BarcodeData> barcodeList = new List<BarcodeData>();

		private StringFormat fNear = new StringFormat();

		private StringFormat fCenter = new StringFormat();

		private StringFormat fFar = new StringFormat();

		private StringFormat fVertical;

		private StringFormat fFormat = new StringFormat();

		private int intPrintedLabelCount;

		private int intRowIndex;

		private int intNoOFClm;

		private int intItemPrinted;

		private int intAcsNo;

		private int intTotal;

		private bool isFirst = false;

		private BarcodeHDR bHdr = new BarcodeHDR();

		private List<BarcodeDTL> _dtls = new List<BarcodeDTL>();

		private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

		private int _intStartingPoint;

		private int intPrintableWth;

		private int intColmWdth;

		private int intColmHt;

		private FontStyle fStyle;

		private DBContext db = new DBContext();

		public ProductBarcode()
		{
			NumberFormat = CommonSettings.strCurencyFormat;
			PrintController printController = new StandardPrintController();
			pDocu.PrintController = printController;
			pDocu.PrintPage += pDocu_PrintPage;
		}

		public static bool CheckQRCodeContent(string content)
		{
			return !string.IsNullOrEmpty(content) && Encoding.UTF8.GetByteCount(content) <= 2952;
		}

		public static Image CreateQRCode(string text, int width, int height)
		{
			if (CheckQRCodeContent(text))
			{
				try
				{
					BarcodeWriter barcodeWriter = new BarcodeWriter
					{
						Format = BarcodeFormat.QR_CODE,
						Options = new QrCodeEncodingOptions
						{
							Width = width,
							Height = height,
							CharacterSet = "UTF-8"
						},
						Renderer = new BitmapRenderer()
					};
					return barcodeWriter.Write(text);
				}
				catch (Exception ex)
				{
					Messages.ErrorMessage(ex.Message);
				}
			}
			return null;
		}

		public static Image CreateBarCode(string text, int width, int height, string sFormat)
		{
			if (CheckQRCodeContent(text))
			{
				try
				{
					BarcodeWriter barcodeWriter = new BarcodeWriter
					{
						Format = GetBarcodeFormat(sFormat),
						Options = new QrCodeEncodingOptions
						{
							Width = width,
							Height = height,
							CharacterSet = sFormat
						},
						Renderer = new BitmapRenderer()
					};
					return barcodeWriter.Write(text);
				}
				catch (Exception ex)
				{
					Messages.ErrorMessage(ex.Message);
				}
			}
			return null;
		}

		public void printBarcode(List<BarcodeData> proBarcode, int intStartingPoint, int? iHdrId = null)
		{
			if (iHdrId.HasValue)
			{
				bHdr = (from x in db.BarcodeHDR()
					where x.Id == iHdrId
					select x).FirstOrDefault();
			}
			else
			{
				bHdr = db.BarcodeHDR().FirstOrDefault();
			}
			if (intStartingPoint > 0)
			{
				_intStartingPoint = intStartingPoint - 1;
			}
			else
			{
				_intStartingPoint = 0;
			}
			barcodeList = proBarcode;
			_dtls = db.BarcodeDTL(new
			{
				FK_BarcodeHDR = bHdr.Id
			}).ToList();
			intTotal = 0;
			foreach (BarcodeData barcode in barcodeList)
			{
				intTotal += barcode.Quantity;
			}
			intPrintedLabelCount = 0;
			isFirst = true;
			intRowIndex = 0;
			intNoOFClm = 0;
			intItemPrinted = 0;
			pDocu.PrinterSettings.PrinterName = bHdr.PrinterName;
			pDocu.DefaultPageSettings.PaperSize = new PaperSize(bHdr.PaperName, bHdr.PSWidth, bHdr.PSHieght);
			pDocu.Print();
		}

		private void getStringFormat(string sfName, string strFStylName)
		{
			fNear.Alignment = StringAlignment.Near;
			fCenter.Alignment = StringAlignment.Center;
			fFar.Alignment = StringAlignment.Near;
			fFar.FormatFlags = StringFormatFlags.DirectionRightToLeft;
			fVertical = new StringFormat(StringFormatFlags.DirectionVertical);
			fFormat = null;
			switch (sfName)
			{
			case "Left":
				fFormat = fNear;
				break;
			case "Center":
				fFormat = fCenter;
				break;
			case "Right":
				fFormat = fFar;
				break;
			default:
				fFormat = fVertical;
				break;
			}
			if (strFStylName == "Regular")
			{
				fStyle = FontStyle.Regular;
			}
			else if (strFStylName == "Bold")
			{
				fStyle = FontStyle.Bold;
			}
			else
			{
				fStyle = FontStyle.Italic;
			}
		}

		public void printBarcodePreview(List<BarcodeData> proBarcode, List<BarcodeDTL> dtls, BarcodeHDR _bHdr, int intStartingPoint)
		{
			_intStartingPoint = intStartingPoint;
			barcodeList = proBarcode;
			bHdr = _bHdr;
			_dtls = dtls;
			intTotal = 0;
			foreach (BarcodeData barcode in barcodeList)
			{
				intTotal += barcode.Quantity;
			}
			intPrintedLabelCount = 0;
			isFirst = true;
			intRowIndex = 0;
			intNoOFClm = 0;
			intItemPrinted = 0;
			pDocu.PrinterSettings.PrinterName = bHdr.PrinterName;
			pDocu.DefaultPageSettings.PaperSize = new PaperSize(bHdr.PaperName, bHdr.PSWidth, bHdr.PSHieght);
			printPreviewDialog1.Document = pDocu;
			printPreviewDialog1.ShowDialog();
		}

		public void printBarcodePreview(List<BarcodeData> proBarcode, int intStartingPoint, int? iHdrId = null)
		{
			try
			{
				_intStartingPoint = intStartingPoint;
				barcodeList = proBarcode;
				if (iHdrId.HasValue)
				{
					bHdr = db.BarcodeHDR().FirstOrDefault((BarcodeHDR x) => x.Id == iHdrId);
				}
				else
				{
					bHdr = db.BarcodeHDR().FirstOrDefault();
				}
				if (bHdr != null)
				{
					_dtls = (from x in db.BarcodeDTL()
						where x.FK_BarcodeHDR == bHdr.Id
						select x).ToList();
					intTotal = 0;
					foreach (BarcodeData barcode in barcodeList)
					{
						intTotal += barcode.Quantity;
					}
					intPrintedLabelCount = 0;
					isFirst = true;
					intRowIndex = 0;
					intNoOFClm = 0;
					intItemPrinted = 0;
					pDocu.PrinterSettings.PrinterName = bHdr.PrinterName;
					pDocu.DefaultPageSettings.PaperSize = new PaperSize(bHdr.PaperName, bHdr.PSWidth, bHdr.PSHieght);
					printPreviewDialog1 = new PrintPreviewDialog();
					printPreviewDialog1.Document = pDocu;
					printPreviewDialog1.ShowDialog();
				}
				else
				{
					Messages.ErrorMessage("There is no barcode design. Please design one and try later.");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void PrintRectangle(PrintPageEventArgs e, int xPoint, int yPoint, int wt, int ht, BarcodeData data, List<BarcodeDTL> _dtls, int bField, BarcodeHDR hdr = null)
		{
			try
			{
				if (hdr != null)
				{
					bHdr = hdr;
				}
				new Bitmap(wt, ht);
				Image image = null;
				string text = ((bField == 1) ? data.Product_Code : data.Barcode);
				foreach (BarcodeDTL _dtl in _dtls)
				{
					string text2 = _dtl.Text.Replace(" ", "_");
					getStringFormat(_dtl.Alignment, _dtl.fStyle);
					if (_dtl.Type == "Logo")
					{
						Image image2 = Image.FromFile(_dtl.Value);
						e.Graphics.DrawImage(image2, xPoint + _dtl.Position_x, yPoint + _dtl.Position_y, _dtl.Width, _dtl.Height);
						continue;
					}
					if (_dtl.Type == "Label")
					{
						e.Graphics.DrawString(_dtl.Value, new Font(_dtl.fontFamily, _dtl.fontSize, fStyle), new SolidBrush(Color.Black), new RectangleF(xPoint + _dtl.Position_x, yPoint + _dtl.Position_y, _dtl.Width, _dtl.Height), fFormat);
						continue;
					}
					if (_dtl.Type == "Barcode")
					{
						image = ((!bHdr.isBarcode) ? CreateQRCode(text, _dtl.Width, _dtl.Height) : BarcodeImageEncodeMethod(_dtl.Width, _dtl.Height, _dtl.Format, _dtl.RotationPoint, text));
						e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						e.Graphics.DrawImage(image, xPoint + _dtl.Position_x, yPoint + _dtl.Position_y, _dtl.Width, _dtl.Height);
						continue;
					}
					PropertyInfo[] properties = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.Name == text2)
						{
							string empty = string.Empty;
							empty = ((!(propertyInfo.PropertyType.Name != "Decimal")) ? propertyInfo.GetValue(data, null).ToString().ToDecimal()
								.ToString(CommonSettings.strCurencyFormat) : propertyInfo.GetValue(data, null).ToString());
							if (empty != null)
							{
								e.Graphics.DrawString(_dtl.Value + empty, new Font(_dtl.fontFamily, _dtl.fontSize, fStyle), new SolidBrush(Color.Black), new RectangleF(xPoint + _dtl.Position_x, yPoint + _dtl.Position_y, _dtl.Width, _dtl.Height), fFormat);
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		public Image GetBarcodeImage(BarcodeData data, List<BarcodeDTL> _dtls, int wt, int ht, Color forcolor, Color backcolor, int bField, BarcodeHDR hdr = null)
		{
			if (hdr != null)
			{
				bHdr = hdr;
			}
			Bitmap bitmap = new Bitmap(wt, ht);
			Image image = null;
			string text = "";
			try
			{
				switch (bField)
				{
				case 0:
					text = data.Product_Code;
					break;
				case 1:
					text = data.Barcode;
					break;
				}
				using Graphics graphics = Graphics.FromImage(bitmap);
				graphics.Clear(backcolor);
				foreach (BarcodeDTL _dtl in _dtls)
				{
					string text2 = _dtl.Text.Replace(" ", "_");
					getStringFormat(_dtl.Alignment, _dtl.fStyle);
					PropertyInfo[] properties = data.GetType().GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (_dtl.Type == null)
						{
							continue;
						}
						if (_dtl.Type == "string")
						{
							if (!(propertyInfo.Name == text2))
							{
								continue;
							}
							string text3 = propertyInfo.GetValue(data, null).ToString();
							if (text3 != null)
							{
								if (_dtl.fontSize == 0)
								{
									_dtl.fontSize = 8;
								}
								graphics.DrawString(_dtl.Value + text3, new Font(_dtl.fontFamily, _dtl.fontSize, fStyle), new SolidBrush(Color.Black), new RectangleF(_dtl.Position_x, _dtl.Position_y, _dtl.Width, _dtl.Height), fFormat);
								break;
							}
						}
						else if (_dtl.Type == "Logo")
						{
							Image image2 = Image.FromFile(_dtl.Value);
							graphics.DrawImage(image2, _dtl.Position_x, _dtl.Position_y, _dtl.Width, _dtl.Height);
						}
						else if (_dtl.Type == "Label")
						{
							graphics.DrawString(_dtl.Value, new Font(_dtl.fontFamily, _dtl.fontSize, fStyle), new SolidBrush(Color.Black), new RectangleF(_dtl.Position_x, _dtl.Position_y, _dtl.Width, _dtl.Height), fFormat);
						}
						else
						{
							image = ((!bHdr.isBarcode) ? CreateQRCode(text, _dtl.Width, _dtl.Height) : BarcodeImageEncodeMethod(_dtl.Width, _dtl.Height, _dtl.Format, _dtl.RotationPoint, text));
							graphics.DrawImageUnscaled(image, _dtl.Position_x, _dtl.Position_y, _dtl.Width, _dtl.Height);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return bitmap;
		}

		private static BarcodeFormat GetBarcodeFormat(string bEncodeType)
		{
			BarcodeFormat barcodeFormat = (BarcodeFormat)0;
			return bEncodeType switch
			{
				"UPC-E" => BarcodeFormat.UPC_E, 
				"MSI" => BarcodeFormat.MSI, 
				"Code 128" => BarcodeFormat.CODE_128, 
				"Code 93" => BarcodeFormat.CODE_93, 
				"UPC-A" => BarcodeFormat.UPC_A, 
				"EAN-8" => BarcodeFormat.EAN_8, 
				"EAN-13" => BarcodeFormat.EAN_13, 
				"Codabar" => BarcodeFormat.CODABAR, 
				"Code 39" => BarcodeFormat.CODE_39, 
				"ITF-14" => BarcodeFormat.ITF, 
				_ => BarcodeFormat.All_1D, 
			};
		}

		public static Image BarcodeImageEncodeMethod(int bWidth, int bHeight, string bEncodeType, string strRotateType, string strData)
		{
			if (bWidth == 0)
			{
				bWidth = 125;
			}
			Barcode barcode = new Barcode();
			barcode.Alignment = AlignmentPositions.CENTER;
			Image result = null;
			TYPE tYPE = TYPE.UNSPECIFIED;
			tYPE = bEncodeType switch
			{
				"Code 39 Extended" => TYPE.CODE39Extended, 
				"FIM" => TYPE.FIM, 
				"Bookland/ISBN" => TYPE.BOOKLAND, 
				"Telepen" => TYPE.TELEPEN, 
				"MSI" => TYPE.MSI_Mod10, 
				"LOGMARS" => TYPE.LOGMARS, 
				"Code 93" => TYPE.CODE93, 
				"UPC-A" => TYPE.UPCA, 
				"UPC-E" => TYPE.UPCE, 
				"Code 39 Mod 43" => TYPE.CODE39_Mod43, 
				"Code 128" => TYPE.CODE128, 
				"UPC 2 Digit Ext." => TYPE.UPC_SUPPLEMENTAL_2DIGIT, 
				"PostNet" => TYPE.PostNet, 
				"Code 128-C" => TYPE.CODE128C, 
				"Code 128-B" => TYPE.CODE128B, 
				"Code 128-A" => TYPE.CODE128A, 
				"EAN-8" => TYPE.EAN8, 
				"EAN-13" => TYPE.EAN13, 
				"UPC 5 Digit Ext." => TYPE.UPC_SUPPLEMENTAL_5DIGIT, 
				"Code 39" => TYPE.CODE39, 
				"Code 11" => TYPE.CODE11, 
				"ITF-14" => TYPE.ITF14, 
				"Codabar" => TYPE.Codabar, 
				"Interleaved 2 of 5" => TYPE.Interleaved2of5, 
				"Standard 2 of 5" => TYPE.Standard2of5, 
				"JAN-13" => TYPE.JAN13, 
				_ => TYPE.PHARMACODE, 
			};
			try
			{
				if (tYPE != 0)
				{
					barcode.IncludeLabel = true;
					barcode.LabelPosition = LabelPositions.BOTTOMCENTER;
					result = barcode.Encode(tYPE, strData, Color.Black, Color.White, bWidth, bHeight);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		private void pDocu_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				double num4 = 0.0;
				int num5 = 0;
				int num6 = 0;
				intAcsNo = 0;
				intColmWdth = bHdr.Width + bHdr.RightMargin;
				intColmHt = bHdr.Height + bHdr.BottomMargin;
				intPrintableWth = intColmWdth * bHdr.Accross - bHdr.FirstMargin;
				num5 = bHdr.FirstMargin;
				num6 = bHdr.TopMargin;
				while (true)
				{
					if (intPrintedLabelCount < intTotal)
					{
						if (isFirst)
						{
							if (_intStartingPoint != 0)
							{
								num4 = _intStartingPoint / bHdr.Accross;
								num2 = Convert.ToInt32(Math.Truncate(num4)) * intColmHt + bHdr.TopMargin;
								num3 = _intStartingPoint % bHdr.Accross;
								if (num3 != 0)
								{
									num = num3 * intColmWdth + bHdr.FirstMargin;
									intAcsNo = num3;
								}
								else
								{
									num = bHdr.FirstMargin;
								}
								num5 = num;
								num6 = num2;
								intItemPrinted = _intStartingPoint;
							}
							intNoOFClm = barcodeList[intRowIndex].Quantity;
							isFirst = false;
						}
						if (intPrintedLabelCount == intNoOFClm && intRowIndex < barcodeList.Count - 1)
						{
							intRowIndex++;
							intNoOFClm += barcodeList[intRowIndex].Quantity;
						}
						if (bHdr.isBorderIncluded)
						{
							e.Graphics.DrawRectangle(Pens.Black, num5, num6, bHdr.Width, bHdr.Height);
						}
						PrintRectangle(e, num5, num6, bHdr.Width, bHdr.Height, barcodeList[intRowIndex], _dtls, bHdr.BarcodeField);
						if (intAcsNo < bHdr.Accross - 1)
						{
							intAcsNo++;
							num5 += intColmWdth;
						}
						else
						{
							intAcsNo = 0;
							num5 = bHdr.FirstMargin;
							num6 += intColmHt;
						}
						intItemPrinted++;
						intPrintedLabelCount++;
						if (intItemPrinted >= bHdr.TotalPerPage)
						{
							break;
						}
						e.HasMorePages = false;
						continue;
					}
					intPrintedLabelCount = 0;
					return;
				}
				if (intPrintedLabelCount != intTotal)
				{
					intItemPrinted = 0;
					e.HasMorePages = true;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
