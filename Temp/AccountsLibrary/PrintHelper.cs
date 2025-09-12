using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AccountsLibrary.Properties;
using AccountsPro.SubProjects.AccountsLibraryFramework;

namespace AccountsLibrary
{
	public class PrintHelper
	{
		private List<DotmatrixSettings> entDSettings;

		private List<InvoiceAdjustment> entAdjustments;

		private PrintDocument printDocument;

		private DotmatrixMaster entDMaster;

		private ExCompany entCompany;

		private TransactionMaster entInvoice;

		private TransactionMaster tMaster;

		private TransactionMaster entAdjustment;

		private List<Transaction> entDetails;

		private PartyDetails entParty;

		private int iTopMargin;

		private int intWidth;

		private int linesPrinted;

		private int iIndex;

		private StringFormat stringFormat;

		private bool isPrinted;

		private bool isLast = true;

		private Pen blackPen;

		private Font font;

		private PrintPreviewDialog printPreviewDialog;

		private string strImagepath;

		private int Ypos;

		public PrintHelper()
		{
			entDMaster = new DotmatrixMaster();
			entDSettings = new List<DotmatrixSettings>();
			entCompany = new ExCompany();
			entInvoice = new TransactionMaster();
			entParty = new PartyDetails();
			entDetails = new List<Transaction>();
			entAdjustment = new TransactionMaster();
			printDocument = new PrintDocument();
			entAdjustments = new List<InvoiceAdjustment>();
			PrintController printController = new StandardPrintController();
			printDocument.PrintController = printController;
			printDocument.PrintPage += PrintDocument_PrintPage;
			printDocument.BeginPrint += PrintDocument_BeginPrint;
		}

		private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
		{
			iIndex = 0;
			linesPrinted = 0;
			isPrinted = false;
		}

		private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			iTopMargin = e.MarginBounds.Top;
			float[] dashPattern = new float[2] { 5f, 3f };
			blackPen = new Pen(Color.Black, 1f);
			blackPen.DashPattern = dashPattern;
			if (entDMaster.PrintType != "Dotmatrix")
			{
				if (entDMaster.PrintType == "Invoice ")
				{
					InvoiceMethod(e);
				}
				else
				{
					NormalPrintingMethod(e);
				}
			}
		}

		private void WritingMethod(DotmatrixSettings item, object obj, PrintPageEventArgs e)
		{
			try
			{
				if (item.Type == "ValueControle")
				{
					ItemFontAndStringFormatFindMethod(item);
					if (item.Position != "Details Area" && item.Position != "Footer Section" && item.Position != "Footer Details" && item.Position != "Footer Bottom")
					{
						if (item.FeildText == "Logo")
						{
							Image imageValueFromEntity = getImageValueFromEntity(obj, item.FeildText);
							if (imageValueFromEntity != null)
							{
								e.Graphics.DrawImage(imageValueFromEntity, new RectangleF(item.X, item.Y, item.width, item.Height));
							}
						}
						else if (item.FeildText == "InvoiceQRCode")
						{
							int num = Math.Min(item.width, item.Height);
							string text = getValueFromEntity(obj, item).ToString();
							//Image image = ProductBarcode.CreateQRCode(text, num, num);
							//if (image != null)
							//{
							//	e.Graphics.DrawImage(image, new RectangleF(item.X, item.Y, item.width, item.Height));
							//}
						}
						else
						{
							object valueFromEntity = getValueFromEntity(obj, item);
							if (valueFromEntity != null)
							{
								e.Graphics.DrawString(valueFromEntity.ToString(), font, Brushes.Black, new RectangleF(item.X, item.Y, item.width, item.Height), stringFormat);
							}
						}
						return;
					}
					if ((entDMaster.PrintType == "Invoice " && item.Position == "Footer Section") || (entDMaster.PrintType == "Invoice " && item.Position == "Footer Bottom"))
					{
						if (item.FeildText == "InvoiceBarcode")
						{
							string strData = getValueFromEntity(obj, item).ToString();
							//Image image2 = ProductBarcode.BarcodeImageEncodeMethod(item.width, item.Height, "Code 128", "360", strData);
							//if (image2 != null)
							//{
							//	e.Graphics.DrawImage(image2, new RectangleF(item.X, iTopMargin, item.width, item.Height));
							//	iTopMargin = iTopMargin + entDMaster.SpaceBtwnLines.Value + item.Height;
							//}
						}
						else
						{
							e.Graphics.DrawString(getValueFromEntity(obj, item).ToString(), font, Brushes.Black, new RectangleF(item.X, item.Y, item.width, item.Height), stringFormat);
						}
						return;
					}
					if (item.FeildText == "InvoiceBarcode")
					{
						string text2 = getValueFromEntity(obj, item).ToString();
						if (text2 != string.Empty)
						{
							//Image image3 = ProductBarcode.BarcodeImageEncodeMethod(item.width, item.Height, "Code 128", "360", text2);
							//if (image3 != null)
							//{
							//	e.Graphics.DrawImage(image3, new RectangleF(item.X, iTopMargin, item.width, item.Height));
							//	iTopMargin = iTopMargin + entDMaster.SpaceBtwnLines.Value + item.Height;
							//}
						}
						return;
					}
					if (item.FeildText == "ItemQRCode")
					{
						int num2 = Math.Min(item.width + 40, item.Height + 40);
						string text3 = getValueFromEntity(obj, item).ToString();
						//Image image4 = ProductBarcode.CreateQRCode(text3, num2, num2);
						//if (image4 != null)
						//{
						//	e.Graphics.DrawImage(image4, new RectangleF(item.X, iTopMargin, item.width, item.Height));
						//}
					}
					else if (item.FeildText == "ItemImage")
					{
						DBContext dBContext = new DBContext();
						int productId = getValueFromEntity(obj, item).ToString().ToInt32();
						byte[] image5 = dBContext.Products(new
						{
							ProductId = productId
						}).FirstOrDefault().image;
						if (image5 != null)
						{
							Image image6 = AniHelper.byteArrayToImage(image5);
							if (image6 != null)
							{
								e.Graphics.DrawImage(image6, new RectangleF(item.X, iTopMargin, item.width, item.Height));
							}
						}
					}
					else
					{
						e.Graphics.DrawString(getValueFromEntity(obj, item).ToString(), font, Brushes.Black, new RectangleF(item.X, iTopMargin, item.width, item.Height), stringFormat);
					}
					if (item.Position == "Footer Section" || item.Position == "Footer Bottom")
					{
						iTopMargin += entDMaster.SpaceBtwnLines.Value;
					}
				}
				else
				{
					if (!(item.Type == "LabelControl"))
					{
						return;
					}
					ItemFontAndStringFormatFindMethod(item);
					if (item.Position != "Details Area" && item.Position != "Footer Section" && item.Position != "Footer Details" && item.Position != "Footer Bottom")
					{
						e.Graphics.DrawString(item.FeildText, font, Brushes.Black, new RectangleF(item.X, item.Y, item.width, item.Height), stringFormat);
						return;
					}
					if ((entDMaster.PrintType == "Invoice " && item.Position == "Footer Section") || (entDMaster.PrintType == "Invoice " && item.Position == "Footer Botom"))
					{
						e.Graphics.DrawString(item.FeildText, font, Brushes.Black, new RectangleF(item.X, item.Y, item.width, item.Height), stringFormat);
						return;
					}
					if ((item.Position == "Footer Section" || item.Position == "Footer Bottom") && item.Spec == "ar")
					{
						iTopMargin -= entDMaster.SpaceBtwnLines.Value;
					}
					e.Graphics.DrawString(item.FeildText, font, Brushes.Black, new RectangleF(item.X, iTopMargin, item.width, item.Height), stringFormat);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void InvoiceMethod(PrintPageEventArgs e)
		{
			List<DotmatrixSettings> list = new List<DotmatrixSettings>();
			list = entDSettings.Where((DotmatrixSettings x) => x.Position == "Details Area").ToList();
			while (true)
			{
				if (linesPrinted >= entDetails.Count)
				{
					return;
				}
				if (!isPrinted)
				{
					strImagepath = entDMaster.BackgroundImgPath;
					if (strImagepath != string.Empty && File.Exists(strImagepath))
					{
						Image image = Image.FromFile(strImagepath);
						if (image != null)
						{
							e.Graphics.DrawImage(image, new RectangleF(0f, 0f, entDMaster.PageWidth.Value, entDMaster.PageHieght.Value));
						}
					}
					if (entDSettings.Count((DotmatrixSettings x) => x.Position == "Company Section" || x.Position == "Invoice Section" || x.Position == "Party Section" || x.Position == "Invoice Details Header ") > 0)
					{
						foreach (DotmatrixSettings item in entDSettings.Where((DotmatrixSettings x) => x.Position == "Company Section" || x.Position == "Invoice Section" || x.Position == "Party Section" || x.Position == "Invoice Details Header ").ToList())
						{
							if (item.Position == "Company Section")
							{
								WritingMethod(item, entCompany, e);
							}
							else if (item.Position == "Invoice Section")
							{
								WritingMethod(item, entInvoice, e);
							}
							else if (item.Position == "Party Section")
							{
								WritingMethod(item, entParty, e);
							}
							if (item.Position == "Invoice Details Header ")
							{
								WritingMethod(item, null, e);
								iTopMargin = Ypos + entDMaster.SpaceBtwnLines.Value + 15;
							}
						}
					}
					isPrinted = true;
					iTopMargin = entDMaster.SpaceInLines.Value;
				}
				foreach (DotmatrixSettings item2 in list)
				{
					if (!CommonSettings.blnGSTEnabled)
					{
						if (item2.ControleName == "Vat")
						{
							item2.ControleName = "Gst";
						}
						if (item2.ControleName == "VatAmount")
						{
							item2.ControleName = "GstAmount";
						}
					}
					if (item2.Spec.ToUpper() == "M")
					{
						string text = getValueFromEntity(entDetails[linesPrinted], item2).ToString();
						if (text != null && text != string.Empty)
						{
							iTopMargin += entDMaster.SpaceBtwnLines.Value;
							WritingMethod(item2, entDetails[linesPrinted], e);
						}
					}
					else
					{
						WritingMethod(item2, entDetails[linesPrinted], e);
					}
				}
				iTopMargin += entDMaster.SpaceBtwnLines.Value;
				iIndex++;
				linesPrinted++;
				if (iIndex >= entDMaster.LinesPerPage)
				{
					break;
				}
				iTopMargin += entDMaster.SpaceBtwnLines.Value;
				if (!isLast)
				{
					continue;
				}
				isLast = false;
				if (entDSettings.Count((DotmatrixSettings x) => x.Position == "Footer Section") <= 0)
				{
					continue;
				}
				foreach (DotmatrixSettings item3 in entDSettings.Where((DotmatrixSettings x) => x.Position == "Footer Section").ToList())
				{
					WritingMethod(item3, entAdjustment, e);
				}
			}
			iIndex = 0;
			isLast = true;
			isPrinted = false;
			if (linesPrinted != entDetails.Count)
			{
				e.HasMorePages = true;
			}
		}

		private void NormalPrintingMethod(PrintPageEventArgs e)
		{
			try
			{
				List<DotmatrixSettings> list = new List<DotmatrixSettings>();
				list = entDSettings.Where((DotmatrixSettings x) => x.Position == "Details Area").ToList();
				Ypos = 0;
				while (true)
				{
					if (linesPrinted < entDetails.Count)
					{
						if (!isPrinted)
						{
							List<DotmatrixSettings> list2 = (from x in entDSettings
								where x.Position == "Company Section"
								orderby x.Y
								select x).ToList();
							if (list2.Count > 0)
							{
								foreach (DotmatrixSettings item2 in list2)
								{
									if (item2.Type == "LabelControl")
									{
										DotmatrixSettings dotmatrixSettings = list2.FirstOrDefault((DotmatrixSettings x) => x.Type == "ValueControle" && x.Y == item2.Y);
										if (dotmatrixSettings != null)
										{
											string text = getValueFromEntity(entCompany, dotmatrixSettings).ToString();
											if (text != null && text != string.Empty)
											{
												NormalWritingMethod(item2, entCompany, e);
											}
										}
										else
										{
											NormalWritingMethod(item2, entCompany, e);
										}
									}
									else if (item2.Type == "ValueControle")
									{
										string text2 = getValueFromEntity(entCompany, item2).ToString();
										if (text2 != null && text2 != string.Empty)
										{
											NormalWritingMethod(item2, entCompany, e);
										}
									}
								}
							}
							list2 = (from x in entDSettings
								where x.Position == "Invoice Section"
								orderby x.Y
								select x).ToList();
							if (list2.Count > 0)
							{
								foreach (DotmatrixSettings item in list2)
								{
									if (item.Type == "LabelControl")
									{
										DotmatrixSettings dotmatrixSettings2 = list2.FirstOrDefault((DotmatrixSettings x) => x.Type == "ValueControle" && x.Y == item.Y);
										if (dotmatrixSettings2 != null)
										{
											string text3 = getValueFromEntity(entInvoice, dotmatrixSettings2).ToString();
											if (text3 != null && text3 != string.Empty)
											{
												NormalWritingMethod(item, entInvoice, e);
											}
										}
										else
										{
											NormalWritingMethod(item, entInvoice, e);
										}
									}
									else if (item.Type == "ValueControle")
									{
										string text4 = getValueFromEntity(entInvoice, item).ToString();
										if (text4 != null && text4 != string.Empty)
										{
											NormalWritingMethod(item, entInvoice, e);
										}
									}
								}
							}
							list2 = (from x in entDSettings
								where x.Position == "Party Section"
								orderby x.Y
								select x).ToList();
							if (list2.Count > 0)
							{
								foreach (DotmatrixSettings item4 in list2)
								{
									NormalWritingMethod(item4, entParty, e);
								}
							}
							list2 = (from x in entDSettings
								where x.Position == "Invoice Details Header "
								orderby x.Y
								select x).ToList();
							if (list2.Count > 0)
							{
								foreach (DotmatrixSettings item5 in list2)
								{
									NormalWritingMethod(item5, null, e);
									iTopMargin = Ypos + entDMaster.SpaceInLines.Value;
								}
							}
						}
						isPrinted = true;
						foreach (DotmatrixSettings item6 in list)
						{
							if (!CommonSettings.blnGSTEnabled)
							{
								if (item6.ControleName == "Vat")
								{
									item6.ControleName = "Gst";
								}
								if (item6.ControleName == "VatAmount")
								{
									item6.ControleName = "GstAmount";
								}
							}
							if (item6.Spec != "M")
							{
								NormalWritingMethod(item6, entDetails[linesPrinted], e);
								continue;
							}
							string text5 = getValueFromEntity(entDetails[linesPrinted], item6).ToString();
							if (text5 != null && text5 != string.Empty)
							{
								iTopMargin = iTopMargin + item6.FontSize + entDMaster.SpaceInLines.Value;
								NormalWritingMethod(item6, entDetails[linesPrinted], e);
							}
						}
						iTopMargin += entDMaster.SpaceInLines.Value;
						iIndex++;
						linesPrinted++;
						if (iTopMargin >= entDMaster.PageHieght && entDMaster.LinesPerPage < 1000)
						{
							break;
						}
						isLast = true;
						iTopMargin += entDMaster.SpaceInLines.Value;
						continue;
					}
					if (!isLast)
					{
						return;
					}
					iTopMargin -= entDMaster.SpaceBtwnLines.Value;
					if (entDSettings.Count((DotmatrixSettings x) => x.Position == "Footer Section") > 0)
					{
						List<DotmatrixSettings> list3 = new List<DotmatrixSettings>();
						list3 = (from x in entDSettings
							where x.Position == "Footer Section"
							orderby x.Y
							select x).ToList();
						foreach (DotmatrixSettings item3 in list3)
						{
							if (item3.Type == "LabelControl")
							{
								DotmatrixSettings dotmatrixSettings3 = list3.FirstOrDefault((DotmatrixSettings x) => x.Type == "ValueControle" && x.Y == item3.Y);
								if (dotmatrixSettings3 != null)
								{
									if (getValueFromEntity(entAdjustment, dotmatrixSettings3).ToString().ToDecimal() != 0m)
									{
										NormalWritingMethod(item3, entAdjustment, e);
									}
								}
								else
								{
									NormalWritingMethod(item3, entAdjustment, e);
								}
							}
							else if (item3.Type == "ValueControle")
							{
								string s = getValueFromEntity(entAdjustment, item3).ToString();
								decimal result = default(decimal);
								if (decimal.TryParse(s, out result))
								{
									if (getValueFromEntity(entAdjustment, item3).ToString().ToDecimal() != 0m)
									{
										NormalWritingMethod(item3, entAdjustment, e);
									}
								}
								else
								{
									NormalWritingMethod(item3, entAdjustment, e);
								}
							}
							else
							{
								NormalWritingMethod(item3, entAdjustment, e);
							}
						}
					}
					if (entDSettings.Count((DotmatrixSettings x) => x.Position == "Footer Details") > 0)
					{
						foreach (InvoiceAdjustment entAdjustment in entAdjustments)
						{
							List<DotmatrixSettings> list4 = new List<DotmatrixSettings>();
							list4 = (from x in entDSettings
								where x.Position == "Footer Details"
								orderby x.Y
								select x).ToList();
							foreach (DotmatrixSettings item7 in list4)
							{
								NormalWritingMethod(item7, entAdjustment, e);
							}
							iTopMargin += entDMaster.SpaceBtwnLines.Value;
						}
					}
					iTopMargin += entDMaster.SpaceBtwnLines.Value;
					if (entDSettings.Count((DotmatrixSettings x) => x.Position == "Footer Bottom") <= 0)
					{
						return;
					}
					List<DotmatrixSettings> list5 = new List<DotmatrixSettings>();
					list5 = (from x in entDSettings
						where x.Position == "Footer Bottom"
						orderby x.Y
						select x).ToList();
					foreach (DotmatrixSettings item8 in list5)
					{
						NormalWritingMethod(item8, tMaster, e);
					}
					return;
				}
				iIndex = 0;
				e.HasMorePages = true;
				iTopMargin = 0;
				isLast = false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void NormalWritingMethod(DotmatrixSettings item, object obj, PrintPageEventArgs e)
		{
			try
			{
				if (item.Type == "ValueControle")
				{
					ItemFontAndStringFormatFindMethod(item);
					if (item.Position != "Details Area" && item.Position != "Footer Section" && item.Position != "Footer Details" && item.Position != "Footer Bottom")
					{
						if (item.FeildText == "Logo")
						{
							Image imageValueFromEntity = getImageValueFromEntity(obj, item.FeildText);
							if (imageValueFromEntity != null)
							{
								e.Graphics.DrawImage(imageValueFromEntity, new RectangleF(item.X, Ypos, item.width, item.Height));
								Ypos += item.Height;
							}
							return;
						}
						if (item.FeildText == "InvoiceQRCode")
						{
							int num = Math.Min(item.width, item.Height);
							string text = getValueFromEntity(obj, item).ToString();
							//Image image = ProductBarcode.CreateQRCode(text, num, num);
							//if (image != null)
							//{
							//	e.Graphics.DrawImage(image, new RectangleF(item.X, Ypos, item.width, item.Height));
							//	Ypos += item.Height;
							//}
							return;
						}
						object valueFromEntity = getValueFromEntity(obj, item);
						if (valueFromEntity != null)
						{
							e.Graphics.DrawString(valueFromEntity.ToString(), font, Brushes.Black, new RectangleF(item.X, Ypos, item.width, item.Height), stringFormat);
							DotmatrixSettings dotmatrixSettings = entDSettings.FirstOrDefault((DotmatrixSettings x) => x.Y == item.Y && x.Spec == "M");
							if (dotmatrixSettings == null)
							{
								Ypos += item.Height;
							}
						}
						return;
					}
					if (item.FeildText == "InvoiceBarcode")
					{
						string text2 = getValueFromEntity(obj, item).ToString();
						if (text2 != string.Empty)
						{
							//Image image2 = ProductBarcode.BarcodeImageEncodeMethod(item.width, item.Height, "Code 128", "360", text2);
							//if (image2 != null)
							//{
							//	e.Graphics.DrawImage(image2, new RectangleF(item.X, iTopMargin, item.width, item.Height));
							//	iTopMargin = iTopMargin + entDMaster.SpaceBtwnLines.Value + item.Height;
							//}
						}
					}
					else if (item.FeildText == "InvoiceQRCode")
					{
						int num2 = Math.Min(item.width, item.Height);
						string text3 = getValueFromEntity(obj, item).ToString();
						//Image image3 = ProductBarcode.CreateQRCode(text3, num2, num2);
						//if (image3 != null)
						//{
						//	e.Graphics.DrawImage(image3, new RectangleF(item.X, iTopMargin, item.width, item.Height));
						//	iTopMargin = iTopMargin + entDMaster.SpaceBtwnLines.Value + item.Height;
						//}
					}
					else if (item.FeildText == "ItemQRCode")
					{
						int num3 = Math.Min(item.width + 40, item.Height + 40);
						string text4 = getValueFromEntity(obj, item).ToString();
						//Image image4 = ProductBarcode.CreateQRCode(text4, num3, num3);
						//if (image4 != null)
						//{
						//	e.Graphics.DrawImage(image4, new RectangleF(item.X, iTopMargin, item.width, item.Height));
						//}
					}
					else if (item.FeildText == "ItemImage")
					{
						DBContext dBContext = new DBContext();
						int productId = getValueFromEntity(obj, item).ToString().ToInt32();
						byte[] image5 = dBContext.Products(new
						{
							ProductId = productId
						}).FirstOrDefault().image;
						if (image5 != null)
						{
							Image image6 = AniHelper.byteArrayToImage(image5);
							if (image6 != null)
							{
								e.Graphics.DrawImage(image6, new RectangleF(item.X, iTopMargin, item.width, item.Height));
							}
						}
					}
					else
					{
						e.Graphics.DrawString(getValueFromEntity(obj, item).ToString(), font, Brushes.Black, new RectangleF(item.X, iTopMargin, item.width, item.Height), stringFormat);
					}
					if (item.Position == "Footer Section" || item.Position == "Footer Bottom")
					{
						iTopMargin += entDMaster.SpaceBtwnLines.Value;
					}
					return;
				}
				if (item.Type == "LabelControl")
				{
					ItemFontAndStringFormatFindMethod(item);
					if (item.Position != "Details Area" && item.Position != "Footer Section" && item.Position != "Footer Details" && item.Position != "Footer Bottom")
					{
						e.Graphics.DrawString(item.FeildText, font, Brushes.Black, new RectangleF(item.X, Ypos, item.width, item.Height), stringFormat);
						if (item.Spec == "M")
						{
							Ypos += item.Height;
						}
					}
					else
					{
						if ((item.Position == "Footer Section" || item.Position == "Footer Bottom") && item.Spec == "ar")
						{
							iTopMargin -= entDMaster.SpaceBtwnLines.Value;
						}
						e.Graphics.DrawString(item.FeildText, font, Brushes.Black, new RectangleF(item.X, iTopMargin, item.width, item.Height), stringFormat);
					}
					return;
				}
				ItemFontAndStringFormatFindMethod(item);
				if (item.Position != "Details Area" && item.Position != "Footer Section" && item.Position != "Footer Details" && item.Position != "Footer Bottom")
				{
					e.Graphics.DrawLine(blackPen, new Point(item.X, Ypos), new Point(item.X + item.width, Ypos));
					Ypos += item.Height;
					return;
				}
				if (item.Position == "Footer Section")
				{
					iTopMargin += entDMaster.SpaceBtwnLines.toInt32();
					e.Graphics.DrawLine(blackPen, new Point(item.X, iTopMargin), new Point(item.X + item.width, iTopMargin));
				}
				else
				{
					e.Graphics.DrawLine(blackPen, new Point(item.X, iTopMargin), new Point(item.X + item.width, iTopMargin));
				}
				iTopMargin += entDMaster.SpaceBtwnLines.toInt32();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void ItemFontAndStringFormatFindMethod(DotmatrixSettings item)
		{
			try
			{
				if (item.isBold)
				{
					font = new Font(item.Font, item.FontSize, FontStyle.Bold);
				}
				else
				{
					font = new Font(item.Font, item.FontSize, FontStyle.Regular);
				}
				stringFormat = new StringFormat();
				if (item.Alaingmnet == "Right")
				{
					stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				}
				else if (item.Alaingmnet == "Center")
				{
					stringFormat.Alignment = StringAlignment.Center;
				}
				else
				{
					stringFormat.Alignment = StringAlignment.Near;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void PrintMethod(DotmatrixMaster _entDMaster, List<DotmatrixSettings> _entDSettings, ExCompany _entCompany, TransactionMaster _entInvoice, PartyDetails _entParty, List<Transaction> _entDetails, TransactionMaster _entAdjustment, List<InvoiceAdjustment> _entAdjustments = null, TransactionMaster _tMaster = null, bool isPrint = true, int iCopyToPrint = 1, bool isPDF = false, bool isKOT = false)
		{
			try
			{
				entDMaster = _entDMaster;
				entDSettings = _entDSettings;
				entCompany = _entCompany;
				entInvoice = _entInvoice;
				entParty = _entParty;
				entDetails = _entDetails;
				entAdjustment = _entAdjustment;
				entAdjustments = _entAdjustments;
				tMaster = _tMaster;
				if (entDMaster.PrintType != "Dotmatrix")
				{
					if (!isPDF)
					{
						if (isKOT)
						{
							printDocument.PrinterSettings.PrinterName = entDMaster.PrinterName;
						}
						else if (Settings.Default.TerminalPrinter != string.Empty)
						{
							printDocument.PrinterSettings.PrinterName = Settings.Default.TerminalPrinter;
						}
						else if (entDMaster.PrinterName != string.Empty)
						{
							printDocument.PrinterSettings.PrinterName = entDMaster.PrinterName;
						}
						if (isPrint)
						{
							for (int i = 0; i < iCopyToPrint; i++)
							{
								isLast = true;
								printDocument.Print();
							}
						}
						else
						{
							printPreviewDialog = new PrintPreviewDialog();
							printPreviewDialog.Document = printDocument;
							printPreviewDialog.ShowDialog();
						}
					}
					else
					{
						string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
						string text = Path.Combine(folderPath, "Invoice.pdf");
						printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
						printDocument.PrinterSettings.PrintToFile = true;
						printDocument.PrinterSettings.PrintFileName = text;
						printDocument.Print();
						Process.Start(text);
					}
				}
				else
				{
					DotmatrixPrintMethod(isPrint);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private Image getImageValueFromEntity(object data, string strProperty)
		{
			Image result = null;
			PropertyInfo[] properties = data.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.Name == strProperty && propertyInfo.GetValue(data, null) != null)
				{
					byte[] byteArrayIn = (byte[])propertyInfo.GetValue(data, null);
					result = AniHelper.byteArrayToImage(byteArrayIn);
					break;
				}
			}
			return result;
		}

		private object getValueFromEntity(object data, DotmatrixSettings dSettings)
		{
			string feildText = dSettings.FeildText;
			object obj = null;
			PropertyInfo[] properties = data.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!(propertyInfo.Name == feildText))
				{
					continue;
				}
				if (propertyInfo.PropertyType.Name == "Decimal")
				{
					obj = ((!(propertyInfo.Name != "Quantity")) ? propertyInfo.GetValue(data, null).ToString().ToDecimal()
						.ToString("G29") : propertyInfo.GetValue(data, null).ToString().ToDecimal()
						.ToString(CommonSettings.strCurencyFormat));
					if (obj == null)
					{
						obj = 0;
					}
				}
				else if (propertyInfo.PropertyType.Name == "DateTime")
				{
					DateTime result = DateTime.Now;
					if (DateTime.TryParse(propertyInfo.GetValue(data, null).ToString(), out result))
					{
						string spec = dSettings.Spec;
						obj = ((!(spec == string.Empty)) ? result.ToString(spec) : result.ToString("dd-MM-yyyy HH:mm:ss"));
					}
				}
				else if (propertyInfo.PropertyType.Name == "Byte[]")
				{
					byte[] array = (byte[])propertyInfo.GetValue(data, null);
					obj = array;
				}
				else
				{
					obj = propertyInfo.GetValue(data, null);
					if (obj == null)
					{
						obj = string.Empty;
					}
				}
				break;
			}
			return obj;
		}

		private void DotmatrixPrintMethod(bool isPrint)
		{
			foreach (DotmatrixSettings entDSetting in entDSettings)
			{
				if (entDSetting.width > 0)
				{
					decimal value = Convert.ToDecimal(entDSetting.width);
					value /= 10m;
					entDSetting.width = Convert.ToInt32(value);
				}
			}
			entDMaster.Riverse = Convert.ToChar(27) + entDMaster.Riverse;
			entDMaster.Pitch = Convert.ToChar(27) + entDMaster.Pitch + Convert.ToChar(18);
			entDMaster.Bold_On = Convert.ToChar(27) + entDMaster.Bold_On;
			entDMaster.Bold_Off = Convert.ToChar(27) + entDMaster.Bold_Off;
			int? linesPerPage = entDMaster.LinesPerPage;
			if ((linesPerPage.GetValueOrDefault() == 0) & linesPerPage.HasValue)
			{
				using (StreamWriter rdr = new StreamWriter("bill.prn"))
				{
					ReverseSkip(entDMaster.upScroll.Value, rdr);
					PrintTitle(rdr);
					PrintHeaderDetails(entCompany, entInvoice, entParty, rdr);
					PrintDetailsHeader(rdr);
					PrintDetails(entDetails.Cast<object>().ToList(), rdr);
					PrintFooter(entInvoice, rdr);
					SkipLine(entDMaster.downScroll.Value, rdr);
				}
				if (isPrint)
				{
					TextPrinter.SendFileToPrinter(entDMaster.PrinterName, "bill.prn");
				}
				else
				{
					Process.Start("bill.prn");
				}
				return;
			}
			if (entDetails.Count <= entDMaster.LinesPerPage)
			{
				using (StreamWriter streamWriter = new StreamWriter("bill.prn"))
				{
					ReverseSkip(entDMaster.upScroll.Value, streamWriter);
					PrintTitle(streamWriter);
					PrintHeaderDetails(entCompany, entInvoice, entParty, streamWriter);
					PrintDetailsHeader(streamWriter);
					PrintDetails(entDetails.Cast<object>().ToList(), streamWriter);
					for (int i = 0; i < entDMaster.LinesPerPage.Value - entDetails.Count; i++)
					{
						streamWriter.WriteLine("");
					}
					PrintFooter(entInvoice, streamWriter);
					SkipLine(entDMaster.downScroll.Value, streamWriter);
				}
				if (isPrint)
				{
					TextPrinter.SendFileToPrinter(entDMaster.PrinterName, "bill.prn");
				}
				return;
			}
			int num = entDetails.Count / entDMaster.LinesPerPage.Value;
			int num2 = entDetails.Count % entDMaster.LinesPerPage.Value;
			int num3 = entDMaster.LinesPerPage.Value - num2;
			if (num2 > 0)
			{
				num++;
			}
			for (int j = 1; j <= num; j++)
			{
				if (j == 1)
				{
					using (StreamWriter rdr2 = new StreamWriter("bill.prn"))
					{
						ReverseSkip(entDMaster.upScroll.Value, rdr2);
						PrintTitle(rdr2);
						PrintHeaderDetails(entCompany, entInvoice, entParty, rdr2);
						PrintDetailsHeader(rdr2);
						iIndex = PrintDetails(entDetails.Cast<object>().ToList(), 0, rdr2);
						SkipLine(entDMaster.downScroll.Value, rdr2);
					}
					TextPrinter.SendFileToPrinter(entDMaster.PrinterName, "bill.prn");
					Thread.Sleep(5000);
				}
				else if (j == num)
				{
					using (StreamWriter streamWriter2 = new StreamWriter("bill.prn"))
					{
						ReverseSkip(entDMaster.upScroll.Value, streamWriter2);
						PrintTitle(streamWriter2);
						PrintHeaderDetails(entCompany, entInvoice, entParty, streamWriter2);
						PrintDetailsHeader(streamWriter2);
						iIndex = PrintDetails(entDetails.Cast<object>().ToList(), iIndex, streamWriter2);
						if (num3 > 0)
						{
							for (int k = 0; k < num3; k++)
							{
								streamWriter2.WriteLine("");
							}
						}
						PrintFooter(entInvoice, streamWriter2);
						SkipLine(entDMaster.downScroll.Value, streamWriter2);
					}
					TextPrinter.SendFileToPrinter(entDMaster.PrinterName, "bill.prn");
				}
				else
				{
					using (StreamWriter rdr3 = new StreamWriter("bill.prn"))
					{
						ReverseSkip(entDMaster.upScroll.Value, rdr3);
						PrintTitle(rdr3);
						PrintHeaderDetails(entCompany, entInvoice, entParty, rdr3);
						PrintDetailsHeader(rdr3);
						iIndex = PrintDetails(entDetails.Cast<object>().ToList(), iIndex, rdr3);
						SkipLine(entDMaster.downScroll.Value, rdr3);
					}
					TextPrinter.SendFileToPrinter(entDMaster.PrinterName, "bill.prn");
					Thread.Sleep(5000);
				}
			}
		}

		private void PrintHeaderDetails(object company, object Invoice, object Party, StreamWriter rdr)
		{
			int num = entDSettings.Count((DotmatrixSettings x) => x.Position == "Company Section" || x.Position == "Invoice Section" || x.Position == "Party Section");
			if (num <= 0)
			{
				return;
			}
			foreach (DotmatrixSettings entDSetting in entDSettings)
			{
				if (entDSetting.Position == "Company Section")
				{
					WritingMethod(entDSetting, company, rdr);
				}
				else if (entDSetting.Position == "Invoice Section")
				{
					WritingMethod(entDSetting, Invoice, rdr);
				}
				else if (entDSetting.Position == "Party Section")
				{
					WritingMethod(entDSetting, Party, rdr);
				}
			}
		}

		private void PrintDetailsHeader(StreamWriter rdr)
		{
			int num = entDSettings.Count((DotmatrixSettings x) => x.Position == "Invoice Details Header ");
			if (num <= 0)
			{
				return;
			}
			foreach (DotmatrixSettings entDSetting in entDSettings)
			{
				if (entDSetting.Position == "Invoice Details Header ")
				{
					WritingMethod(entDSetting, null, rdr);
				}
			}
		}

		private void PrintDetails(List<object> dtlsObj, StreamWriter rdr)
		{
			List<DotmatrixSettings> list = new List<DotmatrixSettings>();
			list = entDSettings.Where((DotmatrixSettings x) => x.Position == "Details Area").ToList();
			int count = list.Count;
			if (count <= 0)
			{
				return;
			}
			foreach (object item in dtlsObj)
			{
				foreach (DotmatrixSettings item2 in list)
				{
					if (!CommonSettings.blnGSTEnabled)
					{
						if (item2.ControleName == "Vat")
						{
							item2.ControleName = "Gst";
						}
						if (item2.ControleName == "VatAmount")
						{
							item2.ControleName = "GstAmount";
						}
					}
					WritingMethod(item2, item, rdr);
				}
			}
		}

		private int PrintDetails(List<object> dtlsObj, int Index, StreamWriter rdr)
		{
			int num = 0;
			int num2 = dtlsObj.Count - Index;
			List<DotmatrixSettings> list = new List<DotmatrixSettings>();
			list = entDSettings.Where((DotmatrixSettings x) => x.Position == "Details Area").ToList();
			int count = list.Count;
			if (count > 0)
			{
				num = ((!(num2 > entDMaster.LinesPerPage)) ? num2 : entDMaster.LinesPerPage.Value);
				for (int i = Index; i < num + Index; i++)
				{
					foreach (DotmatrixSettings item in list)
					{
						WritingMethod(item, dtlsObj[i], rdr);
					}
				}
			}
			return num + Index;
		}

		private void PrintFooter(object Invoice, StreamWriter rdr)
		{
			int num = entDSettings.Count((DotmatrixSettings x) => x.Position == "Footer Section");
			if (num <= 0)
			{
				return;
			}
			foreach (DotmatrixSettings entDSetting in entDSettings)
			{
				if (entDSetting.Position == "Footer Section")
				{
					WritingMethod(entDSetting, Invoice, rdr);
				}
			}
		}

		private void WritingMethod(DotmatrixSettings item, object obj, StreamWriter rdr)
		{
			if (item.Type == "LabelControl")
			{
				intWidth = item.width;
				if (item.Alaingmnet == "Left")
				{
					intWidth *= -1;
				}
				else
				{
					intWidth = intWidth;
				}
				string text = item.FeildText;
				if (item.isBold)
				{
					text = entDMaster.Bold_On + text + entDMaster.Bold_Off;
				}
				rdr.Write("{0," + intWidth + "}", text);
			}
			else if (item.Type == "ValueControle")
			{
				intWidth = item.width;
				if (item.Alaingmnet == "Left")
				{
					intWidth *= -1;
				}
				else
				{
					intWidth = intWidth;
				}
				string arg = getValueFromEntity(obj, item).ToString();
				if (item.isBold)
				{
					arg = entDMaster.Bold_On + getValueFromEntity(obj, item)?.ToString() + entDMaster.Bold_Off;
				}
				rdr.Write("{0," + intWidth + "}", arg);
			}
			else if (item.Type == "BrakeControle")
			{
				rdr.WriteLine("");
			}
			else if (item.Type == "LineLabelControl")
			{
				PrintLine(item.width, rdr);
			}
			else if (item.Type == "SpaceControl")
			{
				PrintSpace(item.width, rdr);
			}
		}

		private void PrintLine(int wdth, StreamWriter rdr)
		{
			string text = string.Empty;
			for (int i = 1; i <= wdth; i++)
			{
				text += "-";
			}
			rdr.WriteLine(text);
		}

		private void PrintSpace(int wdth, StreamWriter rdr)
		{
			string text = string.Empty;
			for (int i = 1; i <= wdth; i++)
			{
				text += " ";
			}
			rdr.Write(text);
		}

		private void SkipLine(int LineNos, StreamWriter rdr)
		{
			for (int i = 1; i <= LineNos; i++)
			{
				rdr.WriteLine("");
			}
		}

		private void PrintTitle(StreamWriter rdr)
		{
			rdr.WriteLine("{0,0}", entDMaster.Pitch);
		}

		private void ReverseSkip(int LineNos, StreamWriter rdr)
		{
			for (int i = 1; i <= LineNos; i++)
			{
				rdr.WriteLine(entDMaster.Riverse);
			}
		}
	}
}
