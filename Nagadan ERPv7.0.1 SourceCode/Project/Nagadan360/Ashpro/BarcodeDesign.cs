using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;


namespace Ashpro
{
	public class BarcodeDesign : BaseForm
	{
		private DBContext db;

		private ToolTip toolTip;

		private bool mouseClickedOnBCode = false;

		private bool mouseClickedOnPbBarcode = false;

		private bool mouseClickedOnBLabel = false;

		private bool mouseClickedOnCstmTBox = false;

		private bool mouseClickedOnCstmLogo = false;

		private bool isEditable = false;

		private Point PanelMouseDownLocation;

		private BarcodeData proBarcode;

		private List<BarcodeData> barcodeList;

		private List<BarcodeDTL> entBarcodeDTLs = new List<BarcodeDTL>();

		private double PaperWidth;

		private double PaperHieght;

		private double intTtlnoPerPage;

		private double lw;

		private double lh;

		private int intDown;

		private int intAcross;

		private RotatingLabel txtBox;

		private ProductBarcode pBarcode;

		private Font font;

		private int intMsr = 0;

		private BarcodeHDR bhdr;

		private FontStyle fStyle = FontStyle.Regular;

		private int intLabelWidth;

		private int intLabelHeight;

		private int intRtMgn;

		private int intBtmMgn;

		private double dblRtMargin;

		private double dblBtmMargin;

		private bool isMrgnKeyPressed = false;

		private bool isLblKeyPressed = false;

		private string strFileName = string.Empty;

		private PictureBox pbLogo;

		private TextBox tBox;

		private int iBarcodeHDRID;

		private BarcodeDTL barcodeDTL;

		private int intCount = 1;

		private IContainer components = null;

		private Label lblParameter;

		private AshTextBoxNormal txtLabelHt;

		private Label lblPrintPerPage;

		private AshTextBoxNormal txtLabelWth;

		private Label lblTotalPerPage;

		private Label lblDownCount;

		private Label lblAcrossCount;

		private CheckBox cbBorder;

		private AshTextBoxNormal txtPSWdth;

		private AshTextBoxNormal txtDown;

		private AshTextBoxNormal txtTop;

		private Label lblBottomMarging;

		private AshTextBoxNormal txtRight;

		private Label lblTopMargin;

		private Button btnPrint;

		private GroupBox grpBarcode;

		private AshComboBox cmbPrinter;

		private AshComboBox cmbPaperSize;

		private Label lblLabelHeight;

		private AshTextBoxNormal txtBottom;

		private Label lblLabelWidth;

		private AshTextBoxNormal txtPSHt;

		private RadioButton rdbSingle;

		private Panel pnlTesting;

		private Button btnExternal;

		private AshTextBoxNormal txtStarting;

		private Label lblStartingPoint;

		private RadioButton rdbBulk;

		private Button btnRefresh;

		private Button btnDelete;

		private GroupBox grpPaperSetup;

		private Label lblPrinter;

		private Label lblPSHeight;

		private Label lblPaperSize;

		private Label lblPSWidth;

		private Button btnEncode;

		private Button btnAdd;

		private Label lblHeader;

		private RadioButton rdbPx;

		private RadioButton rdbIn;

		private RadioButton rdbCM;

		private GroupBox grpMeasurement;

		private ErrorProvider errProvider;

		private Button btnPreview;

		private GroupBox grpSave;

		private Button btnOpen;

		private Button btnSave;

		private GroupBox grpControleSettings;

		private AshTextBoxNormal txtPrefixVal;

		private Label lblAlignment;

		private Button btnLogo;

		private Button btnFont;

		private AshTextBoxNormal txtBarcodeY;

		private Label lblControlPrefix;

		private Label lblFormat;

		private AshTextBoxNormal txtHeight;

		private Label lblControlLocation_Y;

		private AshComboBox cmbAlignment;

		private AshComboBox cmbBarcodeValue;

		private AshComboBox cbEncodeType;

		private Label lblControlLocation_X;

		private AshTextBoxNormal txtBarcodeX;

		private AshTextBoxNormal txtControl;

		private AshTextBoxNormal txtWidth;

		private Label lblControl;

		private Label lblControlHeight;

		private Label lblControlWidth;

		private Panel pnlWorkSpace;

		private Panel pnlMargin;

		private PictureBox pbBarcodeLabel;

		private Panel pnlLabel;

		private Panel Barcode_Panel;

		private PictureBox pbBarcode;

		private GroupBox grpLabelSettings;

		private AshTextBoxNormal txtLeft;

		private Label lblRightMargin;

		private AshTextBoxNormal txtAcross;

		private Label lblLeftMargin;

		private FontDialog fontDialog1;

		private PrintDialog printDialog1;

		private AshComboBox cmbParameter;

		private RadioButton rbQRCode;

		private RadioButton rbBarcode;

		private AshComboBox cmbDesign;

		private Label label9;

		private Button btnDeleteBarcode;

		private Button btnEdit;

		private Button btnExport;

		private Button btnImport;

		private RadioButton rbMRPSlip;

		public BarcodeDesign()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void EntityEditMethod()
		{
			try
			{
				barcodeDTL.Format = cbEncodeType.Text;
				barcodeDTL.Value = txtPrefixVal.Text;
				barcodeDTL.Alignment = cmbAlignment.Text;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ShowToolTip()
		{
			try
			{
				toolTip = new ToolTip();
				toolTip.SetToolTip(btnSave, "Save  [F8]");
				toolTip.SetToolTip(btnOpen, "Open  [F11]");
				toolTip.SetToolTip(btnEncode, "Generate  [F2]");
				toolTip.SetToolTip(btnAdd, "Add");
				toolTip.SetToolTip(btnDelete, "Delete");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AddBarcodeMethod()
		{
			barcodeDTL = new BarcodeDTL();
			barcodeDTL.Name = "29";
			barcodeDTL.Text = "Barcode_Image";
			barcodeDTL.Position_x = 0;
			barcodeDTL.Position_y = 18;
			barcodeDTL.Width = 140;
			barcodeDTL.Height = 70;
			barcodeDTL.Alignment = "Left";
			barcodeDTL.RotationPoint = "360";
			barcodeDTL.fontFamily = "Open Sans";
			barcodeDTL.fontSize = 9;
			barcodeDTL.fStyle = "Regular";
			barcodeDTL.Format = "Code 128";
			barcodeDTL.bAlignment = "Rotate180FlipXY";
			barcodeDTL.Type = "Barcode";
			entBarcodeDTLs.Add(barcodeDTL);
		}

		private bool ValidationMwthod()
		{
			try
			{
				errProvider.Clear();
				if (lblPrintPerPage.Text.Trim() == "0")
				{
					errProvider.SetError(lblPrintPerPage, "Value Must be greater than zero");
					return false;
				}
				if (txtLabelWth.Text == "0" || txtLabelWth.Text == string.Empty)
				{
					errProvider.SetError(txtLabelWth, "Value Must be greater than zero");
					txtLabelWth.Focus();
					return false;
				}
				if (txtLabelHt.Text == "0" || txtLabelHt.Text == string.Empty)
				{
					errProvider.SetError(txtLabelHt, "Value Must be greater than zero");
					txtLabelHt.Focus();
					return false;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return true;
		}

		private void AddMethod(string text, string Type, string sName, string value, int intX, int intY, int Wdth, int Htt)
		{
			try
			{
				if (Type != "Logo")
				{
					txtBox = new RotatingLabel();
					txtBox.AutoSize = false;
					txtBox.TextAlign = ContentAlignment.TopCenter;
					txtBox.Name = sName;
					txtBox.NewText = text;
					txtWidth.Text = txtBox.Width.ToString();
					txtHeight.Text = txtBox.Font.Height.ToString();
					txtBarcodeX.Text = intX.ToString();
					txtBarcodeY.Text = intY.ToString();
					txtBox.Location = new Point(intX, intY);
					txtBox.MouseDown += txtBox_MouseDown;
					txtBox.MouseUp += txtBox_MouseUp;
					txtBox.MouseMove += txtBox_MouseMove;
					pnlLabel.Controls.Add(txtBox);
					return;
				}
				pbLogo = new PictureBox();
				if (value != null && !(value == string.Empty))
				{
					pbLogo.BackgroundImage = Image.FromFile(value);
				}
				else
				{
					pbLogo.BackgroundImage = Resources.Background_Image;
				}
				pbLogo.BackgroundImageLayout = ImageLayout.Stretch;
				pbLogo.Name = base.Name;
				pbLogo.BorderStyle = BorderStyle.FixedSingle;
				pbLogo.Location = new Point(intX, intY);
				pbLogo.Size = new Size(Wdth, Htt);
				pbLogo.MouseDown += pbLogo_MouseDown;
				pbLogo.MouseUp += pbLogo_MouseUp;
				pbLogo.MouseMove += pbLogo_MouseMove;
				txtWidth.Text = Wdth.ToString();
				txtHeight.Text = Htt.ToString();
				txtBarcodeX.Text = intX.ToString();
				txtBarcodeY.Text = intY.ToString();
				pnlLabel.Controls.Add(pbLogo);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void BarcodeHeaderMethod()
		{
			try
			{
				bhdr = new BarcodeHDR();
				bhdr.Id = iBarcodeHDRID;
				bhdr.name = cmbDesign.Text;
				bhdr.Accross = Convert.ToInt32(txtAcross.Text);
				bhdr.Down = Convert.ToInt32(txtDown.Text);
				bhdr.TotalPerPage = Convert.ToInt32(lblPrintPerPage.Text);
				bhdr.PrinterName = cmbPrinter.Text;
				bhdr.PaperName = cmbPaperSize.Text;
				if (rdbPx.Checked)
				{
					bhdr.FirstMargin = Convert.ToInt32(txtLeft.Text);
					bhdr.TopMargin = Convert.ToInt32(txtTop.Text);
					bhdr.RightMargin = Convert.ToInt32(txtRight.Text);
					bhdr.BottomMargin = Convert.ToInt32(txtBottom.Text);
					bhdr.Width = Convert.ToInt32(txtLabelWth.Text);
					bhdr.Height = Convert.ToInt32(txtLabelHt.Text);
					bhdr.PSWidth = Convert.ToInt32(txtPSWdth.Text);
					bhdr.PSHieght = Convert.ToInt32(txtPSHt.Text);
				}
				else if (rdbCM.Checked)
				{
					bhdr.FirstMargin = Convert.ToInt32(Convert.ToDouble(txtLeft.Text) * 39.3701);
					bhdr.TopMargin = Convert.ToInt32(Convert.ToDouble(txtTop.Text) * 39.3701);
					bhdr.RightMargin = Convert.ToInt32(Convert.ToDouble(txtRight.Text) * 39.3701);
					bhdr.BottomMargin = Convert.ToInt32(Convert.ToDouble(txtBottom.Text) * 39.3701);
					bhdr.Width = Convert.ToInt32(Convert.ToDouble(txtLabelWth.Text) * 39.3701);
					bhdr.Height = Convert.ToInt32(Convert.ToDouble(txtLabelHt.Text) * 39.3701);
					bhdr.PSWidth = Convert.ToInt32(Convert.ToDouble(txtPSWdth.Text) * 39.3701);
					bhdr.PSHieght = Convert.ToInt32(Convert.ToDouble(txtPSHt.Text) * 39.3701);
				}
				else
				{
					bhdr.FirstMargin = Convert.ToInt32(Convert.ToDouble(txtLeft.Text) * 100.0);
					bhdr.TopMargin = Convert.ToInt32(Convert.ToDouble(txtTop.Text) * 100.0);
					bhdr.RightMargin = Convert.ToInt32(Convert.ToDouble(txtRight.Text) * 100.0);
					bhdr.BottomMargin = Convert.ToInt32(Convert.ToDouble(txtBottom.Text) * 100.0);
					bhdr.Width = Convert.ToInt32(Convert.ToDouble(txtLabelWth.Text) * 100.0);
					bhdr.Height = Convert.ToInt32(Convert.ToDouble(txtLabelHt.Text) * 100.0);
					bhdr.PSWidth = Convert.ToInt32(Convert.ToDouble(txtPSWdth.Text) * 100.0);
					bhdr.PSHieght = Convert.ToInt32(Convert.ToDouble(txtPSHt.Text) * 100.0);
				}
				bhdr.BarcodeField = cmbBarcodeValue.SelectedIndex;
				bhdr.isBarcode = rbBarcode.Checked;
				bhdr.isBorderIncluded = cbBorder.Checked;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void LoadControlsMwthod()
		{
			try
			{
				entBarcodeDTLs = new List<BarcodeDTL>();
				foreach (Label item in pnlLabel.Controls.OfType<Label>())
				{
					pnlLabel.Controls.Remove(item);
				}
				foreach (PictureBox item2 in pnlLabel.Controls.OfType<PictureBox>())
				{
					pnlLabel.Controls.Remove(item2);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AddPrintDataList()
		{
			try
			{
				for (int i = 0; i < 3; i++)
				{
					barcodeList = new List<BarcodeData>();
					barcodeList.Clear();
					proBarcode = new BarcodeData();
					proBarcode.Sl_No = 1;
					proBarcode.Product_Code = (1000 + i).ToString();
					proBarcode.Product_Name = "Sample Product";
					proBarcode.Specification = "Specification";
					proBarcode.Group_Name = "Group";
					proBarcode.Brand = "Brand";
					proBarcode.Unit = "Unit";
					proBarcode.Purchase_Rate = 800m;
					proBarcode.Sales_Rate = 1000m;
					proBarcode.MRP = 1100m;
					proBarcode.Description = "Description";
					proBarcode.Company_Name = "U Field-2";
					proBarcode.Place = "U Field-3";
					proBarcode.Address = "U Field-4";
					proBarcode.Pincode = "PC-1234";
					proBarcode.PhoneNumber = "POS1234548";
					proBarcode.Mobile = "BB-1234";
					proBarcode.Website = "UB-1234";
					proBarcode.Country = "LC-1234";
					proBarcode.Barcode = "1234567" + i;
					if (rdbSingle.Checked)
					{
						proBarcode.Quantity = i + 1;
					}
					else
					{
						proBarcode.Quantity = (i + 1) * 4;
					}
					proBarcode.ExpairyDate = "01-01-2021";
					proBarcode.ManufacturingDate = "01-01-2021";
					proBarcode.PriceCode = "HAA";
					proBarcode.Rate_In_Arabic = proBarcode.Sales_Rate.ToString(CommonSettings.strCurencyFormat).ToArabic();
					barcodeList.Add(proBarcode);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private string MsrConverting(string strTBText, double dmlMSRValue, bool isMultiple)
		{
			try
			{
				if (rdbPx.Checked)
				{
					if (isMultiple)
					{
						return Math.Truncate(Convert.ToDouble(strTBText) * dmlMSRValue).ToString();
					}
					return Math.Truncate(Convert.ToDouble(strTBText) / dmlMSRValue).ToString();
				}
				if (isMultiple)
				{
					return (Convert.ToDouble(strTBText) * dmlMSRValue).ToString("N2");
				}
				return (Convert.ToDouble(strTBText) / dmlMSRValue).ToString("N2");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return null;
		}

		private void WidthAndHightFindMethod()
		{
			try
			{
				double d = 0.0;
				double d2 = 0.0;
				if (txtLabelWth.Text != string.Empty && txtLabelHt.Text != string.Empty && txtPSHt.Text != string.Empty && txtPSWdth.Text != string.Empty && txtBottom.Text != string.Empty && txtRight.Text != string.Empty)
				{
					PaperHieght = Convert.ToDouble(txtPSHt.Text) - Convert.ToDouble(txtTop.Text);
					PaperWidth = Convert.ToDouble(txtPSWdth.Text) - Convert.ToDouble(txtLeft.Text);
					lh = Convert.ToDouble(txtLabelHt.Text) + Convert.ToDouble(txtBottom.Text);
					lw = Convert.ToDouble(txtLabelWth.Text) + Convert.ToDouble(txtRight.Text);
					intAcross = 0;
					intDown = 0;
					intTtlnoPerPage = 0.0;
					if (PaperWidth > 0.0 && lw > 0.0)
					{
						d = PaperWidth / lw;
					}
					if (PaperHieght > 0.0 && lh > 0.0)
					{
						d2 = PaperHieght / lh;
					}
					intAcross = Convert.ToInt32(Math.Truncate(d));
					intDown = Convert.ToInt32(Math.Truncate(d2));
					intTtlnoPerPage = intAcross * intDown;
					lblPrintPerPage.Text = intTtlnoPerPage.ToString();
					txtAcross.Text = intAcross.ToString();
					txtDown.Text = intDown.ToString();
				}
			}
			catch (Exception)
			{
			}
		}

		private void pbLogo_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (mouseClickedOnCstmLogo)
				{
					pbLogo.Left += e.X - PanelMouseDownLocation.X;
					pbLogo.Top += e.Y - PanelMouseDownLocation.Y;
					if (intMsr == 0)
					{
						txtBarcodeX.Text = pbLogo.Location.X.ToString();
						txtBarcodeY.Text = pbLogo.Location.Y.ToString();
					}
					else if (intMsr == 1)
					{
						txtBarcodeX.Text = ((double)pbLogo.Location.X / 39.3701).ToString("N2");
						txtBarcodeY.Text = ((double)pbLogo.Location.Y / 39.3701).ToString("N2");
					}
					else
					{
						txtBarcodeX.Text = (pbLogo.Location.X / 100).ToString();
						txtBarcodeY.Text = (pbLogo.Location.Y / 100).ToString();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void pbLogo_MouseUp(object sender, MouseEventArgs e)
		{
			mouseClickedOnCstmLogo = false;
		}

		private void pbLogo_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				btnLogo.Visible = true;
				btnFont.Visible = false;
				lblControlPrefix.Text = "File Path";
				mouseClickedOnCstmLogo = true;
				pbLogo = (PictureBox)sender;
				btnLogo.Visible = true;
				txtControl.Text = barcodeDTL.Text;
				cmbBarcodeValue.Visible = false;
				txtPrefixVal.Visible = true;
				if (e.Button == MouseButtons.Left)
				{
					PanelMouseDownLocation = e.Location;
				}
				if (barcodeDTL.Value != string.Empty)
				{
					txtPrefixVal.Text = barcodeDTL.Value;
				}
				if (intMsr == 0)
				{
					txtBarcodeX.Text = barcodeDTL.Position_x.ToString();
					txtBarcodeY.Text = barcodeDTL.Position_y.ToString();
					txtHeight.Text = barcodeDTL.Height.ToString();
					txtWidth.Text = barcodeDTL.Width.ToString();
				}
				else if (intMsr == 1)
				{
					txtBarcodeX.Text = ((double)barcodeDTL.Position_x / 39.3701).ToString("F");
					txtBarcodeY.Text = ((double)barcodeDTL.Position_y / 39.3701).ToString("F");
					txtHeight.Text = ((double)barcodeDTL.Height / 39.3701).ToString("F");
					txtWidth.Text = ((double)barcodeDTL.Width / 39.3701).ToString("F");
				}
				else
				{
					txtBarcodeX.Text = (barcodeDTL.Position_x * 100).ToString("F");
					txtBarcodeY.Text = (barcodeDTL.Position_y * 100).ToString("F");
					txtHeight.Text = (barcodeDTL.Height * 100).ToString("F");
					txtWidth.Text = (barcodeDTL.Width * 100).ToString("F");
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void pbBarcode_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (entBarcodeDTLs.Count != 0)
				{
					mouseClickedOnPbBarcode = true;
					barcodeDTL = entBarcodeDTLs.FirstOrDefault((BarcodeDTL x) => x.Name == "29");
					txtControl.Text = barcodeDTL.Text;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void pbBarcode_MouseUp(object sender, MouseEventArgs e)
		{
			mouseClickedOnPbBarcode = false;
		}

		private void pbBarcode_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (mouseClickedOnPbBarcode)
				{
					Barcode_Panel.Height = pbBarcode.Top + e.Y;
					Barcode_Panel.Width = pbBarcode.Left + e.X;
					if (intMsr == 0)
					{
						txtHeight.Text = Barcode_Panel.Height.ToString();
						txtWidth.Text = Barcode_Panel.Width.ToString();
					}
					else if (intMsr == 1)
					{
						txtHeight.Text = ((double)Barcode_Panel.Height / 39.3701).ToString("F");
						txtWidth.Text = ((double)Barcode_Panel.Width / 39.3701).ToString("F");
					}
					else
					{
						txtHeight.Text = (Barcode_Panel.Height / 100).ToString("F");
						txtWidth.Text = (Barcode_Panel.Width / 100).ToString("F");
					}
					barcodeDTL.Width = Barcode_Panel.Width;
					barcodeDTL.Height = Barcode_Panel.Height;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 1160");
			}
		}

		private void Barcode_Panel_MouseUp(object sender, MouseEventArgs e)
		{
			mouseClickedOnBLabel = false;
			isEditable = false;
			btnEdit.BackColor = Settings.Default.BackColor;
		}

		private void Barcode_Panel_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (entBarcodeDTLs.Count != 0)
				{
					barcodeDTL = entBarcodeDTLs[0];
					mouseClickedOnBLabel = true;
					lblControlPrefix.Text = "Value :";
					cmbBarcodeValue.Visible = true;
					txtPrefixVal.Visible = false;
					barcodeDTL = entBarcodeDTLs.FirstOrDefault((BarcodeDTL x) => x.Name == "29");
					txtControl.Text = barcodeDTL.Text;
					txtPrefixVal.Text = string.Empty;
					if (e.Button == MouseButtons.Left)
					{
						PanelMouseDownLocation = e.Location;
					}
					if (intMsr == 0)
					{
						txtWidth.Text = Barcode_Panel.Width.ToString();
						txtHeight.Text = Barcode_Panel.Height.ToString();
						txtBarcodeX.Text = Barcode_Panel.Location.X.ToString();
						txtBarcodeY.Text = Barcode_Panel.Location.Y.ToString();
					}
					else if (intMsr == 1)
					{
						txtWidth.Text = ((double)Barcode_Panel.Width / 39.3701).ToString("F");
						txtHeight.Text = ((double)Barcode_Panel.Height / 39.3701).ToString("F");
						txtBarcodeX.Text = ((double)Barcode_Panel.Location.X / 39.3701).ToString("F");
						txtBarcodeY.Text = ((double)Barcode_Panel.Location.Y / 39.3701).ToString("F");
					}
					else
					{
						txtWidth.Text = (Barcode_Panel.Width.ToString().ToDecimal() / 100m).ToString();
						txtHeight.Text = (Barcode_Panel.Height.ToString().ToDecimal() / 100m).ToString();
						txtBarcodeX.Text = (Barcode_Panel.Location.X.ToString().ToDecimal() / 100m).ToString();
						txtBarcodeY.Text = (Barcode_Panel.Location.Y.ToString().ToDecimal() / 100m).ToString();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void rdbPx_Click(object sender, EventArgs e)
		{
			try
			{
				isEditable = false;
				btnEdit.BackColor = Settings.Default.BackColor;
				double num = 0.0;
				if (rdbPx.Checked)
				{
					if (intMsr == 1)
					{
						num = 39.3701;
						txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: true);
						txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: true);
						txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: true);
						txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: true);
						txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: true);
						txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: true);
						txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: true);
						txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: true);
						txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: true);
						txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: true);
						txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: true);
						txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: true);
					}
					else
					{
						if (intMsr != 2)
						{
							return;
						}
						num = 100.0;
						txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: true);
						txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: true);
						txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: true);
						txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: true);
						txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: true);
						txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: true);
						txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: true);
						txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: true);
						txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: true);
						txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: true);
						txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: true);
						txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: true);
					}
					intMsr = 0;
					return;
				}
				if (rdbCM.Checked)
				{
					if (intMsr == 0)
					{
						num = 39.3701;
						txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: false);
						txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: false);
						txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: false);
						txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: false);
						txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: false);
						txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: false);
						txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: false);
						txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: false);
						txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: false);
						txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: false);
						txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: false);
						txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: false);
					}
					else
					{
						if (intMsr != 2)
						{
							return;
						}
						num = 2.54;
						txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: true);
						txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: true);
						txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: true);
						txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: true);
						txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: true);
						txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: true);
						txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: true);
						txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: true);
						txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: true);
						txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: true);
						txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: true);
						txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: true);
					}
					intMsr = 1;
					return;
				}
				if (intMsr == 0)
				{
					num = 100.0;
					txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: false);
					txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: false);
					txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: false);
					txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: false);
					txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: false);
					txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: false);
					txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: false);
					txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: false);
					txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: false);
					txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: false);
					txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: false);
					txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: false);
				}
				else
				{
					if (intMsr != 1)
					{
						return;
					}
					num = 2.54;
					txtLabelWth.Text = MsrConverting(txtLabelWth.Text, num, isMultiple: false);
					txtLabelHt.Text = MsrConverting(txtLabelHt.Text, num, isMultiple: false);
					txtHeight.Text = MsrConverting(txtHeight.Text, num, isMultiple: false);
					txtWidth.Text = MsrConverting(txtWidth.Text, num, isMultiple: false);
					txtBarcodeX.Text = MsrConverting(txtBarcodeX.Text, num, isMultiple: false);
					txtBarcodeY.Text = MsrConverting(txtBarcodeY.Text, num, isMultiple: false);
					txtLeft.Text = MsrConverting(txtLeft.Text, num, isMultiple: false);
					txtRight.Text = MsrConverting(txtRight.Text, num, isMultiple: false);
					txtTop.Text = MsrConverting(txtTop.Text, num, isMultiple: false);
					txtBottom.Text = MsrConverting(txtBottom.Text, num, isMultiple: false);
					txtPSWdth.Text = MsrConverting(txtPSWdth.Text, num, isMultiple: false);
					txtPSHt.Text = MsrConverting(txtPSHt.Text, num, isMultiple: false);
				}
				intMsr = 2;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtBarcodeX_TextChanged(object sender, EventArgs e)
		{
		}

		private void cmbAlignment_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (barcodeDTL == null || !(barcodeDTL.Name != string.Empty) || !(barcodeDTL.Name != "29"))
				{
					return;
				}
				double num = 100.0;
				if (cmbAlignment.SelectedIndex == 3)
				{
					if (rdbPx.Checked)
					{
						txtHeight.Text = 100.0.ToString();
					}
					else if (rdbCM.Checked)
					{
						txtHeight.Text = 2.5399986284007405.ToString("F");
					}
					else
					{
						txtHeight.Text = 1.0.ToString("F");
					}
					txtBox.AutoSize = false;
					txtBox.Text = "";
					txtBox.RotateAngle = 90;
					txtBox.Refresh();
				}
				else
				{
					if (rdbPx.Checked)
					{
						txtHeight.Text = 15.0.ToString();
					}
					else if (rdbCM.Checked)
					{
						txtHeight.Text = 0.3809997942601111.ToString("F");
					}
					else
					{
						txtHeight.Text = 0.15.ToString("F");
					}
					txtBox.AutoSize = false;
					txtBox.Text = "";
					txtBox.RotateAngle = 360;
					txtBox.Refresh();
					EntityEditMethod();
				}
				barcodeDTL.Alignment = cmbAlignment.Text;
				barcodeDTL.RotationPoint = txtBox.RotateAngle.ToString();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnLogo_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				strFileName = openFileDialog.FileName;
				txtPrefixVal.Text = strFileName;
				foreach (PictureBox item in pnlLabel.Controls.OfType<PictureBox>())
				{
					item.BackgroundImage = Image.FromFile(strFileName);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 1537");
			}
		}

		private void cmbPrinter_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				cmbPaperSize.Items.Clear();
				PrinterSettings printerSettings = new PrinterSettings();
				printerSettings.PrinterName = cmbPrinter.Text;
				foreach (PaperSize paperSize in printerSettings.PaperSizes)
				{
					cmbPaperSize.Items.Add(paperSize.PaperName);
				}
				if (cmbPaperSize.Items.Contains("A4"))
				{
					cmbPaperSize.Text = "A4";
					txtPSWdth.Text = "827";
					txtPSHt.Text = "1169";
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void cmbPaperSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				PrinterSettings printerSettings = new PrinterSettings();
				foreach (PaperSize paperSize in printerSettings.PaperSizes)
				{
					if (cmbPaperSize.Text == paperSize.PaperName)
					{
						if (rdbPx.Checked)
						{
							txtPSWdth.Text = paperSize.Width.ToString();
							txtPSHt.Text = paperSize.Height.ToString();
						}
						else if (rdbCM.Checked)
						{
							txtPSWdth.Text = ((double)paperSize.Width / 39.3701).ToString("F");
							txtPSHt.Text = ((double)paperSize.Height / 39.3701).ToString("F");
						}
						else
						{
							txtPSWdth.Text = (paperSize.Width / 100).ToString("F");
							txtPSHt.Text = (paperSize.Height / 100).ToString("F");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEncode_Click(object sender, EventArgs e)
		{
			btnLogo.Visible = false;
			btnFont.Visible = true;
			try
			{
				if (ValidationMwthod())
				{
					BarcodeHeaderMethod();
					pBarcode = new ProductBarcode();
					grpBarcode.BackgroundImage = pBarcode.GetBarcodeImage(barcodeList.FirstOrDefault(), entBarcodeDTLs, bhdr.Width, bhdr.Height, Color.Black, Color.White, bhdr.BarcodeField, bhdr);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "1602");
			}
		}

		private void btnPreview_Click(object sender, EventArgs e)
		{
			btnLogo.Visible = false;
			btnFont.Visible = true;
			try
			{
				if (ValidationMwthod())
				{
					BarcodeHeaderMethod();
					pBarcode = new ProductBarcode();
					pBarcode.printBarcodePreview(barcodeList, entBarcodeDTLs, bhdr, Convert.ToInt32(txtStarting.Text));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "1622");
			}
		}

		private void txtAcross_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtDown.Text != string.Empty && txtAcross.Text != string.Empty)
				{
					intAcross = Convert.ToInt32(txtAcross.Text);
					intDown = Convert.ToInt32(txtDown.Text);
					lblPrintPerPage.Text = (intAcross * intDown).ToString();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtLeft_TextChanged(object sender, EventArgs e)
		{
			tBox = (TextBox)sender;
			try
			{
				if (isMrgnKeyPressed)
				{
					if (rdbPx.Checked)
					{
						intBtmMgn = Convert.ToInt32(txtBottom.Text);
						intRtMgn = Convert.ToInt32(txtRight.Text);
					}
					else if (rdbCM.Checked)
					{
						dblRtMargin = Convert.ToDouble(txtRight.Text) * 39.3701;
						intRtMgn = Convert.ToInt32(Math.Round(dblRtMargin, 0));
						dblBtmMargin = Convert.ToDouble(txtBottom.Text) * 39.3701;
						intBtmMgn = Convert.ToInt32(Math.Round(dblBtmMargin, 0));
					}
					else
					{
						dblRtMargin = Convert.ToDouble(txtRight.Text) * 100.0;
						intRtMgn = Convert.ToInt32(Math.Round(dblRtMargin, 0));
						dblBtmMargin = Convert.ToDouble(txtBottom.Text) * 100.0;
						intBtmMgn = Convert.ToInt32(Math.Round(dblBtmMargin, 0));
					}
					intLabelWidth = pnlLabel.Width;
					intLabelHeight = pnlLabel.Height;
					if (tBox == txtRight || tBox == txtBottom)
					{
						pnlMargin.Size = new Size(intRtMgn + intLabelWidth + pnlLabel.Location.X, intBtmMgn + intLabelHeight + pnlLabel.Location.Y);
						pnlLabel.Height = intLabelHeight;
						pnlLabel.Width = intLabelWidth;
					}
					isMrgnKeyPressed = false;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				WidthAndHightFindMethod();
			}
		}

		private void txtLabelWth_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (txtLabelHt.Text != string.Empty && txtLabelWth.Text != string.Empty && !mouseClickedOnBCode && isLblKeyPressed)
				{
					if (intMsr == 0)
					{
						pnlMargin.Height = Convert.ToInt32(txtLabelHt.Text) + 30;
						pnlMargin.Width = Convert.ToInt32(txtLabelWth.Text) + 30;
					}
					else if (intMsr == 1)
					{
						pnlMargin.Height = Convert.ToInt32(Convert.ToDouble(txtLabelHt.Text) * 39.3701) + 30;
						pnlMargin.Width = Convert.ToInt32(Convert.ToDouble(txtLabelWth.Text) * 39.3701) + 30;
					}
					else if (rdbIn.Checked)
					{
						pnlMargin.Height = Convert.ToInt32(Convert.ToDouble(txtLabelHt.Text) * 100.0) + 30;
						pnlMargin.Width = Convert.ToInt32(Convert.ToDouble(txtLabelWth.Text) * 100.0) + 30;
					}
					isLblKeyPressed = false;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				WidthAndHightFindMethod();
			}
		}

		private void txtPSWdth_TextChanged(object sender, EventArgs e)
		{
			WidthAndHightFindMethod();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				btnLogo.Visible = false;
				btnFont.Visible = true;
				if (!ValidationMwthod())
				{
					return;
				}
				BarcodeHeaderMethod();
				if (!db.BarcodeHDR().Any((BarcodeHDR x) => x.name == cmbDesign.Text))
				{
					bhdr.Id = null;
					if (AshproCommon.InsertToDatabaseObj(bhdr, "BarcodeHDR"))
					{
						foreach (BarcodeDTL entBarcodeDTL in entBarcodeDTLs)
						{
							entBarcodeDTL.id = null;
							entBarcodeDTL.FK_BarcodeHDR = (from x in db.BarcodeHDR()
								where x.name == bhdr.name
								select x).FirstOrDefault().Id.ToInt32();
						}
						if (AshproCommon.InsertToDatabase(entBarcodeDTLs.Cast<object>().ToList(), "BarcodeDTL"))
						{
							Messages.SavedMessage();
						}
					}
				}
				else if (AshproCommon.DeleteFromDatabase("BarcodeDTL", "FK_BarcodeHDR", iBarcodeHDRID) && AshproCommon.UpdateToDatabaseObj(bhdr, "BarcodeHDR", "Id", iBarcodeHDRID))
				{
					foreach (BarcodeDTL entBarcodeDTL2 in entBarcodeDTLs)
					{
						entBarcodeDTL2.id = null;
						entBarcodeDTL2.FK_BarcodeHDR = (from x in db.BarcodeHDR()
							where x.name == bhdr.name
							select x).FirstOrDefault().Id.ToInt32();
					}
					if (AshproCommon.InsertToDatabase(entBarcodeDTLs.Cast<object>().ToList(), "BarcodeDTL"))
					{
						Messages.UpdateMessage();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			btnRefresh_Click(null, null);
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			try
			{
				bhdr = new BarcodeHDR();
				entBarcodeDTLs = new List<BarcodeDTL>();
				rdbPx.Checked = true;
				bhdr = db.BarcodeHDR().FirstOrDefault((BarcodeHDR x) => x.Id == cmbDesign.SelectedValue.ToInt32());
				iBarcodeHDRID = bhdr.Id.ToInt32();
				entBarcodeDTLs = (from x in db.BarcodeDTL()
					where x.FK_BarcodeHDR == bhdr.Id
					select x).ToList();
				BarcodeLoadMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void BarcodeLoadMethod()
		{
			foreach (BarcodeDTL entBarcodeDTL in entBarcodeDTLs)
			{
				if (entBarcodeDTL.Type != "Barcode")
				{
					AddMethod(entBarcodeDTL.Text, entBarcodeDTL.Type, entBarcodeDTL.Name, string.Empty, entBarcodeDTL.Position_x, entBarcodeDTL.Position_y, entBarcodeDTL.Width, entBarcodeDTL.Height);
				}
			}
			barcodeDTL = entBarcodeDTLs.FirstOrDefault((BarcodeDTL x) => x.Name == "29");
			if (barcodeDTL != null)
			{
				Barcode_Panel.Size = new Size(barcodeDTL.Width, barcodeDTL.Height);
				Barcode_Panel.Location = new Point(barcodeDTL.Position_x, barcodeDTL.Position_y);
			}
			else
			{
				Barcode_Panel.Visible = false;
			}
			txtLeft.Text = bhdr.FirstMargin.ToString();
			txtTop.Text = bhdr.TopMargin.ToString();
			txtRight.Text = bhdr.RightMargin.ToString();
			txtBottom.Text = bhdr.BottomMargin.ToString();
			txtLabelWth.Text = bhdr.Width.ToString();
			txtLabelHt.Text = bhdr.Height.ToString();
			txtAcross.Text = bhdr.Accross.ToString();
			txtDown.Text = bhdr.Down.ToString();
			lblPrintPerPage.Text = bhdr.TotalPerPage.ToString();
			cmbPrinter.Text = bhdr.PrinterName.ToString();
			cmbPaperSize.Text = bhdr.PaperName.ToString();
			txtPSWdth.Text = bhdr.PSWidth.ToString();
			txtPSHt.Text = bhdr.PSHieght.ToString();
			cbBorder.Checked = bhdr.isBorderIncluded;
			rbBarcode.Checked = bhdr.isBarcode;
			cmbBarcodeValue.SelectedIndex = bhdr.BarcodeField;
			pnlMargin.Width = bhdr.Width + 20 + bhdr.RightMargin;
			pnlMargin.Height = bhdr.Height + 20 + bhdr.BottomMargin;
			pnlLabel.Location = new Point(10, 10);
		}

		private void BarcodeDesign_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control && e.Alt && e.KeyCode == Keys.T)
				{
					if (pnlTesting.Visible)
					{
						pnlTesting.Visible = false;
						pnlTesting.Location = new Point(315, 150);
					}
					else
					{
						pnlTesting.Visible = true;
					}
				}
				else if (e.KeyCode == Keys.F8)
				{
					btnSave_Click(null, null);
				}
				else if (e.KeyCode == Keys.F11)
				{
					btnOpen_Click(null, null);
				}
				else if (e.KeyCode == Keys.F2)
				{
					btnEncode_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void BarcodeDesign_Load(object sender, EventArgs e)
		{
			try
			{
				db = new DBContext();
				entBarcodeDTLs = new List<BarcodeDTL>();
				ShowToolTip();
				AddPrintDataList();
				cbEncodeType.SelectedIndex = 8;
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					cmbPrinter.Items.Add(installedPrinter);
				}
				if (cmbPrinter.Items.Count > 0)
				{
					cmbPrinter.SelectedIndex = 0;
				}
				cmbBarcodeValue.SelectedIndex = 0;
				PrinterSettings printerSettings = new PrinterSettings();
				printerSettings.PrinterName = cmbPrinter.Text;
				foreach (PaperSize paperSize in printerSettings.PaperSizes)
				{
					cmbPaperSize.Items.Add(paperSize.PaperName);
				}
				if (cmbPaperSize.Items.Contains("A4"))
				{
					cmbPaperSize.Text = "A4";
					txtPSWdth.Text = "827";
					txtPSHt.Text = "1169";
				}
				cmbParameter.Items.Clear();
				BarcodeData myObject = new BarcodeData();
				AniHelper.PropertyFindMethod(myObject, cmbParameter);
				AniHelper.FillBarcode(cmbDesign);
				cmbDesign.Text = string.Empty;
				btnRefresh_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void rbBarcode_Click(object sender, EventArgs e)
		{
			try
			{
				if (rbMRPSlip.Checked)
				{
					if (entBarcodeDTLs.Any((BarcodeDTL x) => x.Type == "Barcode"))
					{
						entBarcodeDTLs.Remove(entBarcodeDTLs.FirstOrDefault((BarcodeDTL x) => x.Type == "Barcode"));
					}
					Barcode_Panel.Visible = false;
					return;
				}
				if (!entBarcodeDTLs.Any((BarcodeDTL x) => x.Type == "Barcode"))
				{
					AddBarcodeMethod();
				}
				Barcode_Panel.Visible = true;
				Barcode_Panel.BackgroundImage = (rbBarcode.Checked ? Resources.Barcode : Resources.QRCode);
				Barcode_Panel.Size = (rbBarcode.Checked ? new Size(200, 90) : new Size(100, 90));
				Barcode_Panel.BackgroundImageLayout = ImageLayout.Stretch;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnFont_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtBox != null)
				{
					fontDialog1 = new FontDialog();
					fontDialog1.Font = txtBox.Font;
					if (fontDialog1.ShowDialog() == DialogResult.OK)
					{
						font = fontDialog1.Font;
						txtBox.Font = font;
					}
					barcodeDTL.fontFamily = font.FontFamily.Name.ToString();
					decimal d = font.Size.ToString().ToDecimal();
					barcodeDTL.fontSize = Math.Round(d).ToInt32();
					barcodeDTL.fStyle = font.Style.ToString();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnDeleteBarcode_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("BarcodeDTL", "FK_BarcodeHDR", iBarcodeHDRID) && AshproCommon.DeleteFromDatabase("BarcodeHDR", "Id", iBarcodeHDRID))
				{
					Messages.DeleteMessage();
					AniHelper.FillBarcode(cmbDesign);
					btnRefresh_Click(null, null);
					cmbDesign.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void Barcode_Panel_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (mouseClickedOnBLabel)
				{
					Barcode_Panel.Left += e.X - PanelMouseDownLocation.X;
					Barcode_Panel.Top += e.Y - PanelMouseDownLocation.Y;
					if (intMsr == 0)
					{
						txtBarcodeX.Text = Barcode_Panel.Location.X.ToString();
						txtBarcodeY.Text = Barcode_Panel.Location.Y.ToString();
					}
					else if (intMsr == 1)
					{
						txtBarcodeX.Text = ((double)Barcode_Panel.Location.X / 39.3701).ToString("N2");
						txtBarcodeY.Text = ((double)Barcode_Panel.Location.Y / 39.3701).ToString("N2");
					}
					else
					{
						txtBarcodeX.Text = (Barcode_Panel.Location.X.ToString().ToDecimal() / 100m).ToString();
						txtBarcodeY.Text = (Barcode_Panel.Location.Y.ToString().ToDecimal() / 100m).ToString();
					}
					barcodeDTL.Position_x = Barcode_Panel.Location.X;
					barcodeDTL.Position_y = Barcode_Panel.Location.Y;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtPrefixVal_Validated(object sender, EventArgs e)
		{
			try
			{
				if (barcodeDTL != null)
				{
					barcodeDTL.Value = txtPrefixVal.Text;
				}
				foreach (RotatingLabel item in pnlLabel.Controls.OfType<RotatingLabel>())
				{
					if (item.Name == barcodeDTL.Name)
					{
						txtBox = item;
						if (barcodeDTL.Type != "Label")
						{
							txtBox.NewText += txtPrefixVal.Text;
							continue;
						}
						txtBox.NewText = txtPrefixVal.Text;
						barcodeDTL.Text = barcodeDTL.Value;
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			isEditable = ((!isEditable) ? true : false);
			btnEdit.BackColor = (isEditable ? Color.Red : Settings.Default.BackColor);
		}

		private void txtWidth_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!isEditable || !(txtWidth.Text.ToDecimal() > 0m) || !(txtHeight.Text.ToDecimal() > 0m))
				{
					return;
				}
				if (barcodeDTL.Type == "Barcode")
				{
					mouseClickedOnBLabel = false;
					if (txtHeight.Text != string.Empty && txtHeight.Text != "0" && txtWidth.Text != string.Empty && txtWidth.Text != "0" && !mouseClickedOnPbBarcode)
					{
						if (rdbPx.Checked)
						{
							Barcode_Panel.Height = txtHeight.Data;
							Barcode_Panel.Width = txtWidth.Data;
						}
						else if (rdbCM.Checked)
						{
							Barcode_Panel.Height = ((double)txtHeight.Data * 39.3701).ToInt32();
							Barcode_Panel.Width = ((double)txtWidth.Data * 39.3701).ToInt32();
						}
						else
						{
							Barcode_Panel.Height = txtHeight.Data * 100;
							Barcode_Panel.Width = txtWidth.Data * 100;
						}
					}
					barcodeDTL.Width = Barcode_Panel.Width;
					barcodeDTL.Height = Barcode_Panel.Height;
				}
				else if (barcodeDTL.Type == "Logo")
				{
					if (txtHeight.Text != string.Empty && txtHeight.Text != "0" && txtWidth.Text != string.Empty && txtWidth.Text != "0" && !mouseClickedOnPbBarcode)
					{
						if (rdbPx.Checked)
						{
							pbLogo.Height = txtHeight.Data;
							pbLogo.Width = txtWidth.Data;
						}
						else if (rdbCM.Checked)
						{
							pbLogo.Height = ((double)txtHeight.Data * 39.3701).ToInt32();
							pbLogo.Width = ((double)txtWidth.Data * 39.3701).ToInt32();
						}
						else
						{
							pbLogo.Height = txtHeight.Data * 100;
							pbLogo.Width = txtWidth.Data * 100;
						}
						barcodeDTL.Width = pbLogo.Width;
						barcodeDTL.Height = pbLogo.Height;
					}
				}
				else if (rdbPx.Checked)
				{
					barcodeDTL.Height = txtHeight.Data;
					barcodeDTL.Width = txtWidth.Data;
				}
				else if (rdbCM.Checked)
				{
					barcodeDTL.Height = ((double)txtHeight.Data * 39.3701).ToInt32();
					barcodeDTL.Width = ((double)txtWidth.Data * 39.3701).ToInt32();
				}
				else
				{
					barcodeDTL.Height = txtHeight.Data * 100;
					barcodeDTL.Width = txtWidth.Data * 100;
				}
			}
			catch (Exception)
			{
			}
		}

		private void txtBarcodeX_TextChanged_1(object sender, EventArgs e)
		{
			try
			{
				if (barcodeDTL == null || !(txtBarcodeX.Text != string.Empty) || !(txtBarcodeY.Text != string.Empty) || !isEditable)
				{
					return;
				}
				if (barcodeDTL.Type == "Barcode")
				{
					if (intMsr == 0)
					{
						Barcode_Panel.Location = new Point(Convert.ToInt32(txtBarcodeX.Text), Convert.ToInt32(txtBarcodeY.Text));
					}
					else if (intMsr == 1)
					{
						Barcode_Panel.Location = new Point(Convert.ToInt32(Convert.ToDouble(txtBarcodeX.Text) * 39.3701), Convert.ToInt32(Convert.ToDouble(txtBarcodeY.Text) * 39.3701));
					}
					else
					{
						Barcode_Panel.Location = new Point(Convert.ToInt32(txtBarcodeX.Text) * 100, Convert.ToInt32(txtBarcodeY.Text) * 100);
					}
				}
				else if (barcodeDTL.Type == "Logo")
				{
					if (intMsr == 0)
					{
						pbLogo.Location = new Point(Convert.ToInt32(txtBarcodeX.Text), Convert.ToInt32(txtBarcodeY.Text));
					}
					else if (intMsr == 1)
					{
						pbLogo.Location = new Point(Convert.ToInt32(Convert.ToDouble(txtBarcodeX.Text) * 39.3701), Convert.ToInt32(Convert.ToDouble(txtBarcodeY.Text) * 39.3701));
					}
					else
					{
						pbLogo.Location = new Point(Convert.ToInt32(txtBarcodeX.Text) * 100, Convert.ToInt32(txtBarcodeY.Text) * 100);
					}
				}
				else if (intMsr == 0)
				{
					txtBox.Location = new Point(Convert.ToInt32(txtBarcodeX.Text), Convert.ToInt32(txtBarcodeY.Text));
				}
				else if (intMsr == 1)
				{
					txtBox.Location = new Point(Convert.ToInt32(Convert.ToDouble(txtBarcodeX.Text) * 39.3701), Convert.ToInt32(Convert.ToDouble(txtBarcodeY.Text) * 39.3701));
				}
				else
				{
					txtBox.Location = new Point(Convert.ToInt32(txtBarcodeX.Text) * 100, Convert.ToInt32(txtBarcodeY.Text) * 100);
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				if (entBarcodeDTLs.Count == 0)
				{
					Messages.ErrorMessage("Design Not Completed");
					return;
				}
				string text = null;
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.InitialDirectory = Application.StartupPath;
				saveFileDialog.Title = "Save Barcode Design";
				saveFileDialog.DefaultExt = "xml";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					text = saveFileDialog.FileName;
				}
				BarcodeFormatClass barcodeFormatClass = new BarcodeFormatClass();
				barcodeFormatClass._barcodeDTLs = entBarcodeDTLs;
				barcodeFormatClass._barcodeHDR = bhdr;
				string path = text;
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(BarcodeFormatClass));
				using (TextWriter textWriter = new StreamWriter(path))
				{
					xmlSerializer.Serialize(textWriter, barcodeFormatClass);
					textWriter.Close();
				}
				Messages.InformationMessage("Successfully Exported the Barcode Design to XML");
				btnRefresh_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			bhdr = new BarcodeHDR();
			entBarcodeDTLs = new List<BarcodeDTL>();
			string path = null;
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					path = openFileDialog.FileName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			if (File.Exists(path))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(BarcodeFormatClass));
				xmlSerializer.UnknownNode += Serializer_UnknownNode;
				xmlSerializer.UnknownAttribute += Serializer_UnknownAttribute;
				BarcodeFormatClass barcodeFormatClass;
				using (FileStream fileStream = new FileStream(path, FileMode.Open))
				{
					barcodeFormatClass = (BarcodeFormatClass)xmlSerializer.Deserialize(fileStream);
					fileStream.Close();
				}
				entBarcodeDTLs = barcodeFormatClass._barcodeDTLs;
				bhdr = barcodeFormatClass._barcodeHDR;
				BarcodeLoadMethod();
			}
		}

		private void Serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Serializer_UnknownNode(object sender, XmlNodeEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void pbBarcodeLabel_MouseDown(object sender, MouseEventArgs e)
		{
			mouseClickedOnBCode = true;
		}

		private void pbBarcodeLabel_MouseUp(object sender, MouseEventArgs e)
		{
			mouseClickedOnBCode = false;
		}

		private void pbBarcodeLabel_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (mouseClickedOnBCode)
				{
					pnlMargin.Height = pbBarcodeLabel.Top + e.Y;
					pnlMargin.Width = pbBarcodeLabel.Left + e.X;
					if (intMsr == 0)
					{
						txtLabelHt.Text = pnlLabel.Height.ToString();
						txtLabelWth.Text = pnlLabel.Width.ToString();
					}
					else if (intMsr == 1)
					{
						txtLabelHt.Text = ((double)pnlLabel.Height / 39.3701).ToString("F");
						txtLabelWth.Text = ((double)pnlLabel.Width / 39.3701).ToString("F");
					}
					else
					{
						txtLabelHt.Text = (pnlLabel.Height / 100).ToString("F");
						txtLabelWth.Text = (pnlLabel.Width / 100).ToString("F");
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(cmbParameter.Text != string.Empty))
				{
					return;
				}
				foreach (Label item in pnlLabel.Controls.OfType<Label>())
				{
					item.ForeColor = Color.Black;
				}
				isEditable = false;
				btnEdit.BackColor = Settings.Default.BackColor;
				cmbAlignment.SelectedIndex = 0;
				txtControl.Text = cmbParameter.Text;
				barcodeDTL = new BarcodeDTL();
				barcodeDTL.Name = cmbParameter.SelectedIndex.ToString();
				barcodeDTL.Text = cmbParameter.Text;
				barcodeDTL.Value = "";
				barcodeDTL.Position_x = 100;
				barcodeDTL.Position_y = 2;
				barcodeDTL.Width = 40;
				barcodeDTL.Height = 40;
				barcodeDTL.fontSize = 10;
				barcodeDTL.Alignment = "Left";
				barcodeDTL.RotationPoint = "360";
				barcodeDTL.fontFamily = "Open Sans";
				barcodeDTL.fontSize = 9;
				barcodeDTL.fStyle = "Regular";
				barcodeDTL.Format = "Code 128";
				barcodeDTL.bAlignment = "Rotate180FlipXY";
				if (barcodeDTL.Text == "Logo")
				{
					barcodeDTL.Type = "Logo";
				}
				else if (barcodeDTL.Text == "Custom String")
				{
					string text3 = (barcodeDTL.Text = (barcodeDTL.Name = cmbParameter.Text + intCount));
					intCount++;
					barcodeDTL.Type = "Label";
				}
				else
				{
					barcodeDTL.Type = "string";
				}
				entBarcodeDTLs.Add(barcodeDTL);
				if (barcodeDTL.Type == "Logo")
				{
					btnLogo.Visible = true;
					btnFont.Visible = false;
					lblControlPrefix.Text = "File Path";
					AddMethod(barcodeDTL.Text, barcodeDTL.Type, barcodeDTL.Name, string.Empty, 100, 2, 40, 40);
				}
				else
				{
					btnFont.Visible = true;
					btnLogo.Visible = false;
					lblControlPrefix.Text = "Prefix Value";
					AddMethod(barcodeDTL.Text, barcodeDTL.Type, barcodeDTL.Name, string.Empty, 1, 1, 15, 100);
					txtPrefixVal.Text = barcodeDTL.Value;
					txtBox.ForeColor = Color.Crimson;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (barcodeDTL == null)
				{
					return;
				}
				if (barcodeDTL.Type == "string")
				{
					foreach (Label item in pnlLabel.Controls.OfType<Label>())
					{
						if (item.Name == barcodeDTL.Name)
						{
							pnlLabel.Controls.Remove(item);
							entBarcodeDTLs.Remove(barcodeDTL);
						}
					}
					return;
				}
				if (!(barcodeDTL.Type == "Logo"))
				{
					return;
				}
				foreach (PictureBox item2 in pnlLabel.Controls.OfType<PictureBox>())
				{
					if (item2.Name == barcodeDTL.Name)
					{
						pnlLabel.Controls.Remove(item2);
						entBarcodeDTLs.Remove(barcodeDTL);
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < 3; i++)
				{
					LoadControlsMwthod();
				}
				AddBarcodeMethod();
				rdbPx.Checked = true;
				intMsr = 0;
				txtPrefixVal.Text = string.Empty;
				cmbAlignment.SelectedIndex = 0;
				txtControl.Text = string.Empty;
				txtLabelWth.Text = "250";
				txtLabelHt.Text = "150";
				grpBarcode.BackgroundImage = null;
				txtRight.Text = "10";
				txtBottom.Text = "10";
				txtLeft.Text = "0";
				txtTop.Text = "0";
				txtWidth.Text = "200";
				txtHeight.Text = "90";
				txtBarcodeX.Text = "0";
				txtBarcodeY.Text = "0";
				pnlMargin.Height = Convert.ToInt32(txtLabelHt.Text) + 30;
				pnlMargin.Width = Convert.ToInt32(txtLabelWth.Text) + 30;
				if (cmbPrinter.Items.Count > 0)
				{
					cmbPrinter.SelectedIndex = 0;
				}
				if (cmbPaperSize.Items.Contains("A4"))
				{
					cmbPaperSize.Text = "A4";
					txtPSWdth.Text = "827";
					txtPSHt.Text = "1169";
				}
				cmbParameter.SelectedIndex = -1;
				cbBorder.Checked = false;
				btnLogo.Visible = false;
				cmbParameter.Focus();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtBox_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (mouseClickedOnCstmTBox)
				{
					txtBox.Left += e.X - PanelMouseDownLocation.X;
					txtBox.Top += e.Y - PanelMouseDownLocation.Y;
					if (intMsr == 0)
					{
						txtBarcodeX.Text = txtBox.Location.X.ToString();
						txtBarcodeY.Text = txtBox.Location.Y.ToString();
					}
					else
					{
						int num = txtBox.Location.X;
						double num2 = 0.0;
						int num3 = txtBox.Location.Y;
						double num4 = 0.0;
						double num5 = 0.0;
						num5 = ((intMsr != 1) ? 100.0 : 39.3701);
						num2 = (double)num / num5;
						num4 = (double)num3 / num5;
						txtBarcodeX.Text = num2.ToString("N2");
						txtBarcodeY.Text = num4.ToString("N2");
					}
					if (barcodeDTL != null)
					{
						barcodeDTL.Position_x = txtBox.Location.X;
						barcodeDTL.Position_y = txtBox.Location.Y;
						barcodeDTL.Width = txtWidth.Data;
						barcodeDTL.Height = txtHeight.Data;
						barcodeDTL.Format = cbEncodeType.Text;
						barcodeDTL.Value = txtPrefixVal.Text;
						barcodeDTL.Alignment = cmbAlignment.Text;
						barcodeDTL.fontFamily = font.FontFamily.ToString();
						barcodeDTL.fontSize = font.Size.ToInt32();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtBox_MouseUp(object sender, MouseEventArgs e)
		{
			mouseClickedOnCstmTBox = false;
			isEditable = false;
			btnEdit.BackColor = Settings.Default.BackColor;
		}

		private void txtBox_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				btnFont.Visible = true;
				btnLogo.Visible = false;
				lblControlPrefix.Text = "Prefix Value";
				foreach (Label item in pnlLabel.Controls.OfType<Label>())
				{
					item.ForeColor = Color.Black;
				}
				txtBox = (RotatingLabel)sender;
				txtBox.ForeColor = Color.Crimson;
				txtControl.Text = txtBox.NewText;
				barcodeDTL = entBarcodeDTLs.FirstOrDefault((BarcodeDTL x) => x.Name == txtBox.Name);
				lblControlPrefix.Text = "Prefix Value";
				cmbBarcodeValue.Visible = false;
				txtPrefixVal.Visible = true;
				mouseClickedOnCstmTBox = true;
				if (e.Button == MouseButtons.Left)
				{
					PanelMouseDownLocation = e.Location;
				}
				txtPrefixVal.Text = barcodeDTL.Value;
				if (barcodeDTL.fStyle == "Regular")
				{
					fStyle = FontStyle.Regular;
				}
				else if (barcodeDTL.fStyle == "Bold")
				{
					fStyle = FontStyle.Bold;
				}
				else
				{
					fStyle = FontStyle.Italic;
				}
				font = new Font(barcodeDTL.fontFamily, barcodeDTL.fontSize, fStyle);
				if (barcodeDTL.Alignment == "Left")
				{
					cmbAlignment.SelectedIndex = 0;
				}
				else if (barcodeDTL.Alignment == "Center")
				{
					cmbAlignment.SelectedIndex = 1;
				}
				else if (barcodeDTL.Alignment == "Right")
				{
					cmbAlignment.SelectedIndex = 2;
				}
				else
				{
					cmbAlignment.SelectedIndex = 3;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarcodeDesign));
            this.lblParameter = new System.Windows.Forms.Label();
            this.txtLabelHt = new AshUsercontrol.AshTextBoxNormal();
            this.lblPrintPerPage = new System.Windows.Forms.Label();
            this.txtLabelWth = new AshUsercontrol.AshTextBoxNormal();
            this.lblTotalPerPage = new System.Windows.Forms.Label();
            this.lblDownCount = new System.Windows.Forms.Label();
            this.lblAcrossCount = new System.Windows.Forms.Label();
            this.cbBorder = new System.Windows.Forms.CheckBox();
            this.txtPSWdth = new AshUsercontrol.AshTextBoxNormal();
            this.txtDown = new AshUsercontrol.AshTextBoxNormal();
            this.txtTop = new AshUsercontrol.AshTextBoxNormal();
            this.lblBottomMarging = new System.Windows.Forms.Label();
            this.txtRight = new AshUsercontrol.AshTextBoxNormal();
            this.lblTopMargin = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.grpBarcode = new System.Windows.Forms.GroupBox();
            this.cmbPrinter = new AshUsercontrol.AshComboBox();
            this.cmbPaperSize = new AshUsercontrol.AshComboBox();
            this.lblLabelHeight = new System.Windows.Forms.Label();
            this.txtBottom = new AshUsercontrol.AshTextBoxNormal();
            this.lblLabelWidth = new System.Windows.Forms.Label();
            this.txtPSHt = new AshUsercontrol.AshTextBoxNormal();
            this.rdbSingle = new System.Windows.Forms.RadioButton();
            this.pnlTesting = new System.Windows.Forms.Panel();
            this.btnExternal = new System.Windows.Forms.Button();
            this.txtStarting = new AshUsercontrol.AshTextBoxNormal();
            this.lblStartingPoint = new System.Windows.Forms.Label();
            this.rdbBulk = new System.Windows.Forms.RadioButton();
            this.grpPaperSetup = new System.Windows.Forms.GroupBox();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.lblPSHeight = new System.Windows.Forms.Label();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.lblPSWidth = new System.Windows.Forms.Label();
            this.btnEncode = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.rdbPx = new System.Windows.Forms.RadioButton();
            this.rdbIn = new System.Windows.Forms.RadioButton();
            this.rdbCM = new System.Windows.Forms.RadioButton();
            this.grpMeasurement = new System.Windows.Forms.GroupBox();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnPreview = new System.Windows.Forms.Button();
            this.grpSave = new System.Windows.Forms.GroupBox();
            this.cmbDesign = new AshUsercontrol.AshComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDeleteBarcode = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtLeft = new AshUsercontrol.AshTextBoxNormal();
            this.txtPrefixVal = new AshUsercontrol.AshTextBoxNormal();
            this.grpControleSettings = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.btnLogo = new System.Windows.Forms.Button();
            this.btnFont = new System.Windows.Forms.Button();
            this.txtBarcodeY = new AshUsercontrol.AshTextBoxNormal();
            this.lblControlPrefix = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.txtHeight = new AshUsercontrol.AshTextBoxNormal();
            this.lblControlLocation_Y = new System.Windows.Forms.Label();
            this.cmbAlignment = new AshUsercontrol.AshComboBox();
            this.cmbBarcodeValue = new AshUsercontrol.AshComboBox();
            this.cbEncodeType = new AshUsercontrol.AshComboBox();
            this.lblControlLocation_X = new System.Windows.Forms.Label();
            this.txtBarcodeX = new AshUsercontrol.AshTextBoxNormal();
            this.txtControl = new AshUsercontrol.AshTextBoxNormal();
            this.txtWidth = new AshUsercontrol.AshTextBoxNormal();
            this.lblControl = new System.Windows.Forms.Label();
            this.lblControlHeight = new System.Windows.Forms.Label();
            this.lblControlWidth = new System.Windows.Forms.Label();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.Barcode_Panel = new System.Windows.Forms.Panel();
            this.pbBarcode = new System.Windows.Forms.PictureBox();
            this.pnlMargin = new System.Windows.Forms.Panel();
            this.pbBarcodeLabel = new System.Windows.Forms.PictureBox();
            this.pnlWorkSpace = new System.Windows.Forms.Panel();
            this.rbMRPSlip = new System.Windows.Forms.RadioButton();
            this.rbQRCode = new System.Windows.Forms.RadioButton();
            this.rbBarcode = new System.Windows.Forms.RadioButton();
            this.lblRightMargin = new System.Windows.Forms.Label();
            this.txtAcross = new AshUsercontrol.AshTextBoxNormal();
            this.lblLeftMargin = new System.Windows.Forms.Label();
            this.grpLabelSettings = new System.Windows.Forms.GroupBox();
            this.cmbParameter = new AshUsercontrol.AshComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pnlTesting.SuspendLayout();
            this.grpPaperSetup.SuspendLayout();
            this.grpMeasurement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.grpSave.SuspendLayout();
            this.grpControleSettings.SuspendLayout();
            this.pnlLabel.SuspendLayout();
            this.Barcode_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcode)).BeginInit();
            this.pnlMargin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcodeLabel)).BeginInit();
            this.pnlWorkSpace.SuspendLayout();
            this.grpLabelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.errProvider.SetError(this.btnEnd, resources.GetString("btnEnd.Error"));
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.errProvider.SetIconAlignment(this.btnEnd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnEnd.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnEnd, ((int)(resources.GetObject("btnEnd.IconPadding"))));
            // 
            // lblParameter
            // 
            resources.ApplyResources(this.lblParameter, "lblParameter");
            this.errProvider.SetError(this.lblParameter, resources.GetString("lblParameter.Error"));
            this.errProvider.SetIconAlignment(this.lblParameter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblParameter.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblParameter, ((int)(resources.GetObject("lblParameter.IconPadding"))));
            this.lblParameter.Name = "lblParameter";
            // 
            // txtLabelHt
            // 
            resources.ApplyResources(this.txtLabelHt, "txtLabelHt");
            this.txtLabelHt.BackColor = System.Drawing.SystemColors.Window;
            this.txtLabelHt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLabelHt.Data = 0;
            this.txtLabelHt.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtLabelHt, resources.GetString("txtLabelHt.Error"));
            this.txtLabelHt.Format = null;
            this.errProvider.SetIconAlignment(this.txtLabelHt, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLabelHt.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtLabelHt, ((int)(resources.GetObject("txtLabelHt.IconPadding"))));
            this.txtLabelHt.isAllowNegative = false;
            this.txtLabelHt.isAllowSpecialChar = false;
            this.txtLabelHt.isNumberOnly = false;
            this.txtLabelHt.isNumeric = false;
            this.txtLabelHt.LeaveColor = System.Drawing.Color.White;
            this.txtLabelHt.Name = "txtLabelHt";
            this.txtLabelHt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLabelHt.TextChanged += new System.EventHandler(this.txtLabelWth_TextChanged);
            // 
            // lblPrintPerPage
            // 
            resources.ApplyResources(this.lblPrintPerPage, "lblPrintPerPage");
            this.errProvider.SetError(this.lblPrintPerPage, resources.GetString("lblPrintPerPage.Error"));
            this.errProvider.SetIconAlignment(this.lblPrintPerPage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrintPerPage.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblPrintPerPage, ((int)(resources.GetObject("lblPrintPerPage.IconPadding"))));
            this.lblPrintPerPage.Name = "lblPrintPerPage";
            // 
            // txtLabelWth
            // 
            resources.ApplyResources(this.txtLabelWth, "txtLabelWth");
            this.txtLabelWth.BackColor = System.Drawing.SystemColors.Window;
            this.txtLabelWth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLabelWth.Data = 0;
            this.txtLabelWth.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtLabelWth, resources.GetString("txtLabelWth.Error"));
            this.txtLabelWth.Format = null;
            this.errProvider.SetIconAlignment(this.txtLabelWth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLabelWth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtLabelWth, ((int)(resources.GetObject("txtLabelWth.IconPadding"))));
            this.txtLabelWth.isAllowNegative = false;
            this.txtLabelWth.isAllowSpecialChar = false;
            this.txtLabelWth.isNumberOnly = false;
            this.txtLabelWth.isNumeric = false;
            this.txtLabelWth.LeaveColor = System.Drawing.Color.White;
            this.txtLabelWth.Name = "txtLabelWth";
            this.txtLabelWth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLabelWth.TextChanged += new System.EventHandler(this.txtLabelWth_TextChanged);
            // 
            // lblTotalPerPage
            // 
            resources.ApplyResources(this.lblTotalPerPage, "lblTotalPerPage");
            this.errProvider.SetError(this.lblTotalPerPage, resources.GetString("lblTotalPerPage.Error"));
            this.errProvider.SetIconAlignment(this.lblTotalPerPage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblTotalPerPage.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblTotalPerPage, ((int)(resources.GetObject("lblTotalPerPage.IconPadding"))));
            this.lblTotalPerPage.Name = "lblTotalPerPage";
            // 
            // lblDownCount
            // 
            resources.ApplyResources(this.lblDownCount, "lblDownCount");
            this.errProvider.SetError(this.lblDownCount, resources.GetString("lblDownCount.Error"));
            this.errProvider.SetIconAlignment(this.lblDownCount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblDownCount.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblDownCount, ((int)(resources.GetObject("lblDownCount.IconPadding"))));
            this.lblDownCount.Name = "lblDownCount";
            // 
            // lblAcrossCount
            // 
            resources.ApplyResources(this.lblAcrossCount, "lblAcrossCount");
            this.errProvider.SetError(this.lblAcrossCount, resources.GetString("lblAcrossCount.Error"));
            this.errProvider.SetIconAlignment(this.lblAcrossCount, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblAcrossCount.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblAcrossCount, ((int)(resources.GetObject("lblAcrossCount.IconPadding"))));
            this.lblAcrossCount.Name = "lblAcrossCount";
            // 
            // cbBorder
            // 
            resources.ApplyResources(this.cbBorder, "cbBorder");
            this.errProvider.SetError(this.cbBorder, resources.GetString("cbBorder.Error"));
            this.errProvider.SetIconAlignment(this.cbBorder, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbBorder.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cbBorder, ((int)(resources.GetObject("cbBorder.IconPadding"))));
            this.cbBorder.Name = "cbBorder";
            this.cbBorder.UseVisualStyleBackColor = true;
            // 
            // txtPSWdth
            // 
            resources.ApplyResources(this.txtPSWdth, "txtPSWdth");
            this.txtPSWdth.BackColor = System.Drawing.SystemColors.Window;
            this.txtPSWdth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPSWdth.Data = 0;
            this.txtPSWdth.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtPSWdth, resources.GetString("txtPSWdth.Error"));
            this.txtPSWdth.Format = null;
            this.errProvider.SetIconAlignment(this.txtPSWdth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPSWdth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtPSWdth, ((int)(resources.GetObject("txtPSWdth.IconPadding"))));
            this.txtPSWdth.isAllowNegative = false;
            this.txtPSWdth.isAllowSpecialChar = false;
            this.txtPSWdth.isNumberOnly = true;
            this.txtPSWdth.isNumeric = false;
            this.txtPSWdth.LeaveColor = System.Drawing.Color.White;
            this.txtPSWdth.Name = "txtPSWdth";
            this.txtPSWdth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPSWdth.TextChanged += new System.EventHandler(this.txtPSWdth_TextChanged);
            // 
            // txtDown
            // 
            resources.ApplyResources(this.txtDown, "txtDown");
            this.txtDown.BackColor = System.Drawing.SystemColors.Window;
            this.txtDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDown.Data = 0;
            this.txtDown.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtDown, resources.GetString("txtDown.Error"));
            this.txtDown.Format = null;
            this.errProvider.SetIconAlignment(this.txtDown, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDown.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtDown, ((int)(resources.GetObject("txtDown.IconPadding"))));
            this.txtDown.isAllowNegative = false;
            this.txtDown.isAllowSpecialChar = false;
            this.txtDown.isNumberOnly = false;
            this.txtDown.isNumeric = false;
            this.txtDown.LeaveColor = System.Drawing.Color.White;
            this.txtDown.Name = "txtDown";
            this.txtDown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDown.TextChanged += new System.EventHandler(this.txtAcross_TextChanged);
            // 
            // txtTop
            // 
            resources.ApplyResources(this.txtTop, "txtTop");
            this.txtTop.BackColor = System.Drawing.SystemColors.Window;
            this.txtTop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTop.Data = 0;
            this.txtTop.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtTop, resources.GetString("txtTop.Error"));
            this.txtTop.Format = null;
            this.errProvider.SetIconAlignment(this.txtTop, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtTop.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtTop, ((int)(resources.GetObject("txtTop.IconPadding"))));
            this.txtTop.isAllowNegative = false;
            this.txtTop.isAllowSpecialChar = false;
            this.txtTop.isNumberOnly = false;
            this.txtTop.isNumeric = false;
            this.txtTop.LeaveColor = System.Drawing.Color.White;
            this.txtTop.Name = "txtTop";
            this.txtTop.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTop.TextChanged += new System.EventHandler(this.txtLeft_TextChanged);
            // 
            // lblBottomMarging
            // 
            resources.ApplyResources(this.lblBottomMarging, "lblBottomMarging");
            this.errProvider.SetError(this.lblBottomMarging, resources.GetString("lblBottomMarging.Error"));
            this.errProvider.SetIconAlignment(this.lblBottomMarging, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblBottomMarging.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblBottomMarging, ((int)(resources.GetObject("lblBottomMarging.IconPadding"))));
            this.lblBottomMarging.Name = "lblBottomMarging";
            // 
            // txtRight
            // 
            resources.ApplyResources(this.txtRight, "txtRight");
            this.txtRight.BackColor = System.Drawing.SystemColors.Window;
            this.txtRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRight.Data = 0;
            this.txtRight.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtRight, resources.GetString("txtRight.Error"));
            this.txtRight.Format = null;
            this.errProvider.SetIconAlignment(this.txtRight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtRight.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtRight, ((int)(resources.GetObject("txtRight.IconPadding"))));
            this.txtRight.isAllowNegative = false;
            this.txtRight.isAllowSpecialChar = false;
            this.txtRight.isNumberOnly = false;
            this.txtRight.isNumeric = false;
            this.txtRight.LeaveColor = System.Drawing.Color.White;
            this.txtRight.Name = "txtRight";
            this.txtRight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRight.TextChanged += new System.EventHandler(this.txtLeft_TextChanged);
            // 
            // lblTopMargin
            // 
            resources.ApplyResources(this.lblTopMargin, "lblTopMargin");
            this.errProvider.SetError(this.lblTopMargin, resources.GetString("lblTopMargin.Error"));
            this.errProvider.SetIconAlignment(this.lblTopMargin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblTopMargin.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblTopMargin, ((int)(resources.GetObject("lblTopMargin.IconPadding"))));
            this.lblTopMargin.Name = "lblTopMargin";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnPrint, resources.GetString("btnPrint.Error"));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnPrint, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnPrint.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnPrint, ((int)(resources.GetObject("btnPrint.IconPadding"))));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // grpBarcode
            // 
            resources.ApplyResources(this.grpBarcode, "grpBarcode");
            this.grpBarcode.BackColor = System.Drawing.Color.Gray;
            this.errProvider.SetError(this.grpBarcode, resources.GetString("grpBarcode.Error"));
            this.grpBarcode.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.grpBarcode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpBarcode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpBarcode, ((int)(resources.GetObject("grpBarcode.IconPadding"))));
            this.grpBarcode.Name = "grpBarcode";
            this.grpBarcode.TabStop = false;
            // 
            // cmbPrinter
            // 
            resources.ApplyResources(this.cmbPrinter, "cmbPrinter");
            this.cmbPrinter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPrinter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrinter.DropDownHeight = 300;
            this.cmbPrinter.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbPrinter, resources.GetString("cmbPrinter.Error"));
            this.cmbPrinter.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbPrinter.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbPrinter, ((int)(resources.GetObject("cmbPrinter.IconPadding"))));
            this.cmbPrinter.LeaveColor = System.Drawing.Color.White;
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.SelectedIndexChanged += new System.EventHandler(this.cmbPrinter_SelectedIndexChanged);
            // 
            // cmbPaperSize
            // 
            resources.ApplyResources(this.cmbPaperSize, "cmbPaperSize");
            this.cmbPaperSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPaperSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPaperSize.DropDownHeight = 300;
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbPaperSize, resources.GetString("cmbPaperSize.Error"));
            this.cmbPaperSize.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbPaperSize, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbPaperSize.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbPaperSize, ((int)(resources.GetObject("cmbPaperSize.IconPadding"))));
            this.cmbPaperSize.LeaveColor = System.Drawing.Color.White;
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.SelectedIndexChanged += new System.EventHandler(this.cmbPaperSize_SelectedIndexChanged);
            // 
            // lblLabelHeight
            // 
            resources.ApplyResources(this.lblLabelHeight, "lblLabelHeight");
            this.errProvider.SetError(this.lblLabelHeight, resources.GetString("lblLabelHeight.Error"));
            this.errProvider.SetIconAlignment(this.lblLabelHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLabelHeight.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblLabelHeight, ((int)(resources.GetObject("lblLabelHeight.IconPadding"))));
            this.lblLabelHeight.Name = "lblLabelHeight";
            // 
            // txtBottom
            // 
            resources.ApplyResources(this.txtBottom, "txtBottom");
            this.txtBottom.BackColor = System.Drawing.SystemColors.Window;
            this.txtBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBottom.Data = 0;
            this.txtBottom.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtBottom, resources.GetString("txtBottom.Error"));
            this.txtBottom.Format = null;
            this.errProvider.SetIconAlignment(this.txtBottom, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtBottom.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtBottom, ((int)(resources.GetObject("txtBottom.IconPadding"))));
            this.txtBottom.isAllowNegative = false;
            this.txtBottom.isAllowSpecialChar = false;
            this.txtBottom.isNumberOnly = false;
            this.txtBottom.isNumeric = false;
            this.txtBottom.LeaveColor = System.Drawing.Color.White;
            this.txtBottom.Name = "txtBottom";
            this.txtBottom.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBottom.TextChanged += new System.EventHandler(this.txtLeft_TextChanged);
            // 
            // lblLabelWidth
            // 
            resources.ApplyResources(this.lblLabelWidth, "lblLabelWidth");
            this.errProvider.SetError(this.lblLabelWidth, resources.GetString("lblLabelWidth.Error"));
            this.errProvider.SetIconAlignment(this.lblLabelWidth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLabelWidth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblLabelWidth, ((int)(resources.GetObject("lblLabelWidth.IconPadding"))));
            this.lblLabelWidth.Name = "lblLabelWidth";
            // 
            // txtPSHt
            // 
            resources.ApplyResources(this.txtPSHt, "txtPSHt");
            this.txtPSHt.BackColor = System.Drawing.SystemColors.Window;
            this.txtPSHt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPSHt.Data = 0;
            this.txtPSHt.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtPSHt, resources.GetString("txtPSHt.Error"));
            this.txtPSHt.Format = null;
            this.errProvider.SetIconAlignment(this.txtPSHt, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPSHt.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtPSHt, ((int)(resources.GetObject("txtPSHt.IconPadding"))));
            this.txtPSHt.isAllowNegative = false;
            this.txtPSHt.isAllowSpecialChar = false;
            this.txtPSHt.isNumberOnly = true;
            this.txtPSHt.isNumeric = false;
            this.txtPSHt.LeaveColor = System.Drawing.Color.White;
            this.txtPSHt.Name = "txtPSHt";
            this.txtPSHt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPSHt.TextChanged += new System.EventHandler(this.txtPSWdth_TextChanged);
            // 
            // rdbSingle
            // 
            resources.ApplyResources(this.rdbSingle, "rdbSingle");
            this.rdbSingle.Checked = true;
            this.errProvider.SetError(this.rdbSingle, resources.GetString("rdbSingle.Error"));
            this.rdbSingle.ForeColor = System.Drawing.Color.Black;
            this.errProvider.SetIconAlignment(this.rdbSingle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rdbSingle.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rdbSingle, ((int)(resources.GetObject("rdbSingle.IconPadding"))));
            this.rdbSingle.Name = "rdbSingle";
            this.rdbSingle.TabStop = true;
            this.rdbSingle.UseVisualStyleBackColor = true;
            // 
            // pnlTesting
            // 
            resources.ApplyResources(this.pnlTesting, "pnlTesting");
            this.pnlTesting.BackColor = System.Drawing.Color.White;
            this.pnlTesting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTesting.Controls.Add(this.btnPrint);
            this.pnlTesting.Controls.Add(this.rdbSingle);
            this.pnlTesting.Controls.Add(this.btnExternal);
            this.pnlTesting.Controls.Add(this.txtStarting);
            this.pnlTesting.Controls.Add(this.lblStartingPoint);
            this.pnlTesting.Controls.Add(this.rdbBulk);
            this.errProvider.SetError(this.pnlTesting, resources.GetString("pnlTesting.Error"));
            this.errProvider.SetIconAlignment(this.pnlTesting, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlTesting.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pnlTesting, ((int)(resources.GetObject("pnlTesting.IconPadding"))));
            this.pnlTesting.Name = "pnlTesting";
            // 
            // btnExternal
            // 
            resources.ApplyResources(this.btnExternal, "btnExternal");
            this.btnExternal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnExternal, resources.GetString("btnExternal.Error"));
            this.btnExternal.FlatAppearance.BorderSize = 0;
            this.btnExternal.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnExternal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnExternal.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnExternal, ((int)(resources.GetObject("btnExternal.IconPadding"))));
            this.btnExternal.Name = "btnExternal";
            this.btnExternal.UseVisualStyleBackColor = false;
            // 
            // txtStarting
            // 
            resources.ApplyResources(this.txtStarting, "txtStarting");
            this.txtStarting.BackColor = System.Drawing.SystemColors.Window;
            this.txtStarting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStarting.Data = 0;
            this.txtStarting.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtStarting, resources.GetString("txtStarting.Error"));
            this.txtStarting.Format = null;
            this.errProvider.SetIconAlignment(this.txtStarting, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtStarting.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtStarting, ((int)(resources.GetObject("txtStarting.IconPadding"))));
            this.txtStarting.isAllowNegative = false;
            this.txtStarting.isAllowSpecialChar = false;
            this.txtStarting.isNumberOnly = false;
            this.txtStarting.isNumeric = false;
            this.txtStarting.LeaveColor = System.Drawing.Color.White;
            this.txtStarting.Name = "txtStarting";
            this.txtStarting.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblStartingPoint
            // 
            resources.ApplyResources(this.lblStartingPoint, "lblStartingPoint");
            this.errProvider.SetError(this.lblStartingPoint, resources.GetString("lblStartingPoint.Error"));
            this.lblStartingPoint.ForeColor = System.Drawing.Color.Black;
            this.errProvider.SetIconAlignment(this.lblStartingPoint, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblStartingPoint.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblStartingPoint, ((int)(resources.GetObject("lblStartingPoint.IconPadding"))));
            this.lblStartingPoint.Name = "lblStartingPoint";
            // 
            // rdbBulk
            // 
            resources.ApplyResources(this.rdbBulk, "rdbBulk");
            this.errProvider.SetError(this.rdbBulk, resources.GetString("rdbBulk.Error"));
            this.rdbBulk.ForeColor = System.Drawing.Color.Black;
            this.errProvider.SetIconAlignment(this.rdbBulk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rdbBulk.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rdbBulk, ((int)(resources.GetObject("rdbBulk.IconPadding"))));
            this.rdbBulk.Name = "rdbBulk";
            this.rdbBulk.UseVisualStyleBackColor = true;
            // 
            // grpPaperSetup
            // 
            resources.ApplyResources(this.grpPaperSetup, "grpPaperSetup");
            this.grpPaperSetup.Controls.Add(this.txtPSHt);
            this.grpPaperSetup.Controls.Add(this.txtPSWdth);
            this.grpPaperSetup.Controls.Add(this.lblPrinter);
            this.grpPaperSetup.Controls.Add(this.lblPSHeight);
            this.grpPaperSetup.Controls.Add(this.cmbPrinter);
            this.grpPaperSetup.Controls.Add(this.cmbPaperSize);
            this.grpPaperSetup.Controls.Add(this.lblPaperSize);
            this.grpPaperSetup.Controls.Add(this.lblPSWidth);
            this.errProvider.SetError(this.grpPaperSetup, resources.GetString("grpPaperSetup.Error"));
            this.errProvider.SetIconAlignment(this.grpPaperSetup, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpPaperSetup.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpPaperSetup, ((int)(resources.GetObject("grpPaperSetup.IconPadding"))));
            this.grpPaperSetup.Name = "grpPaperSetup";
            this.grpPaperSetup.TabStop = false;
            // 
            // lblPrinter
            // 
            resources.ApplyResources(this.lblPrinter, "lblPrinter");
            this.errProvider.SetError(this.lblPrinter, resources.GetString("lblPrinter.Error"));
            this.errProvider.SetIconAlignment(this.lblPrinter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPrinter.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblPrinter, ((int)(resources.GetObject("lblPrinter.IconPadding"))));
            this.lblPrinter.Name = "lblPrinter";
            // 
            // lblPSHeight
            // 
            resources.ApplyResources(this.lblPSHeight, "lblPSHeight");
            this.errProvider.SetError(this.lblPSHeight, resources.GetString("lblPSHeight.Error"));
            this.errProvider.SetIconAlignment(this.lblPSHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPSHeight.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblPSHeight, ((int)(resources.GetObject("lblPSHeight.IconPadding"))));
            this.lblPSHeight.Name = "lblPSHeight";
            // 
            // lblPaperSize
            // 
            resources.ApplyResources(this.lblPaperSize, "lblPaperSize");
            this.errProvider.SetError(this.lblPaperSize, resources.GetString("lblPaperSize.Error"));
            this.errProvider.SetIconAlignment(this.lblPaperSize, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPaperSize.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblPaperSize, ((int)(resources.GetObject("lblPaperSize.IconPadding"))));
            this.lblPaperSize.Name = "lblPaperSize";
            // 
            // lblPSWidth
            // 
            resources.ApplyResources(this.lblPSWidth, "lblPSWidth");
            this.errProvider.SetError(this.lblPSWidth, resources.GetString("lblPSWidth.Error"));
            this.errProvider.SetIconAlignment(this.lblPSWidth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblPSWidth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblPSWidth, ((int)(resources.GetObject("lblPSWidth.IconPadding"))));
            this.lblPSWidth.Name = "lblPSWidth";
            // 
            // btnEncode
            // 
            resources.ApplyResources(this.btnEncode, "btnEncode");
            this.btnEncode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnEncode, resources.GetString("btnEncode.Error"));
            this.btnEncode.FlatAppearance.BorderSize = 0;
            this.btnEncode.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnEncode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnEncode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnEncode, ((int)(resources.GetObject("btnEncode.IconPadding"))));
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.UseVisualStyleBackColor = false;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.errProvider.SetError(this.lblHeader, resources.GetString("lblHeader.Error"));
            this.errProvider.SetIconAlignment(this.lblHeader, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblHeader.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblHeader, ((int)(resources.GetObject("lblHeader.IconPadding"))));
            this.lblHeader.Name = "lblHeader";
            // 
            // rdbPx
            // 
            resources.ApplyResources(this.rdbPx, "rdbPx");
            this.rdbPx.Checked = true;
            this.errProvider.SetError(this.rdbPx, resources.GetString("rdbPx.Error"));
            this.errProvider.SetIconAlignment(this.rdbPx, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rdbPx.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rdbPx, ((int)(resources.GetObject("rdbPx.IconPadding"))));
            this.rdbPx.Name = "rdbPx";
            this.rdbPx.TabStop = true;
            this.rdbPx.UseVisualStyleBackColor = true;
            this.rdbPx.Click += new System.EventHandler(this.rdbPx_Click);
            // 
            // rdbIn
            // 
            resources.ApplyResources(this.rdbIn, "rdbIn");
            this.errProvider.SetError(this.rdbIn, resources.GetString("rdbIn.Error"));
            this.errProvider.SetIconAlignment(this.rdbIn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rdbIn.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rdbIn, ((int)(resources.GetObject("rdbIn.IconPadding"))));
            this.rdbIn.Name = "rdbIn";
            this.rdbIn.UseVisualStyleBackColor = true;
            this.rdbIn.Click += new System.EventHandler(this.rdbPx_Click);
            // 
            // rdbCM
            // 
            resources.ApplyResources(this.rdbCM, "rdbCM");
            this.errProvider.SetError(this.rdbCM, resources.GetString("rdbCM.Error"));
            this.errProvider.SetIconAlignment(this.rdbCM, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rdbCM.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rdbCM, ((int)(resources.GetObject("rdbCM.IconPadding"))));
            this.rdbCM.Name = "rdbCM";
            this.rdbCM.UseVisualStyleBackColor = true;
            this.rdbCM.Click += new System.EventHandler(this.rdbPx_Click);
            // 
            // grpMeasurement
            // 
            resources.ApplyResources(this.grpMeasurement, "grpMeasurement");
            this.grpMeasurement.BackColor = System.Drawing.Color.White;
            this.grpMeasurement.Controls.Add(this.rdbPx);
            this.grpMeasurement.Controls.Add(this.rdbIn);
            this.grpMeasurement.Controls.Add(this.rdbCM);
            this.errProvider.SetError(this.grpMeasurement, resources.GetString("grpMeasurement.Error"));
            this.errProvider.SetIconAlignment(this.grpMeasurement, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpMeasurement.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpMeasurement, ((int)(resources.GetObject("grpMeasurement.IconPadding"))));
            this.grpMeasurement.Name = "grpMeasurement";
            this.grpMeasurement.TabStop = false;
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            resources.ApplyResources(this.errProvider, "errProvider");
            // 
            // btnPreview
            // 
            resources.ApplyResources(this.btnPreview, "btnPreview");
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnPreview, resources.GetString("btnPreview.Error"));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnPreview, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnPreview.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnPreview, ((int)(resources.GetObject("btnPreview.IconPadding"))));
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // grpSave
            // 
            resources.ApplyResources(this.grpSave, "grpSave");
            this.grpSave.BackColor = System.Drawing.Color.White;
            this.grpSave.Controls.Add(this.cmbDesign);
            this.grpSave.Controls.Add(this.label9);
            this.grpSave.Controls.Add(this.btnOpen);
            this.grpSave.Controls.Add(this.btnSave);
            this.grpSave.Controls.Add(this.btnDeleteBarcode);
            this.grpSave.Controls.Add(this.btnExport);
            this.grpSave.Controls.Add(this.btnImport);
            this.errProvider.SetError(this.grpSave, resources.GetString("grpSave.Error"));
            this.errProvider.SetIconAlignment(this.grpSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpSave.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpSave, ((int)(resources.GetObject("grpSave.IconPadding"))));
            this.grpSave.Name = "grpSave";
            this.grpSave.TabStop = false;
            // 
            // cmbDesign
            // 
            resources.ApplyResources(this.cmbDesign, "cmbDesign");
            this.cmbDesign.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbDesign, resources.GetString("cmbDesign.Error"));
            this.cmbDesign.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbDesign, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbDesign.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbDesign, ((int)(resources.GetObject("cmbDesign.IconPadding"))));
            this.cmbDesign.LeaveColor = System.Drawing.Color.White;
            this.cmbDesign.Name = "cmbDesign";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errProvider.SetError(this.label9, resources.GetString("label9.Error"));
            this.errProvider.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errProvider.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnOpen, resources.GetString("btnOpen.Error"));
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnOpen, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnOpen.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnOpen, ((int)(resources.GetObject("btnOpen.IconPadding"))));
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnSave, resources.GetString("btnSave.Error"));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnSave, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnSave.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnSave, ((int)(resources.GetObject("btnSave.IconPadding"))));
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDeleteBarcode
            // 
            resources.ApplyResources(this.btnDeleteBarcode, "btnDeleteBarcode");
            this.btnDeleteBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnDeleteBarcode, resources.GetString("btnDeleteBarcode.Error"));
            this.btnDeleteBarcode.FlatAppearance.BorderSize = 0;
            this.btnDeleteBarcode.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnDeleteBarcode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnDeleteBarcode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnDeleteBarcode, ((int)(resources.GetObject("btnDeleteBarcode.IconPadding"))));
            this.btnDeleteBarcode.Name = "btnDeleteBarcode";
            this.btnDeleteBarcode.UseVisualStyleBackColor = false;
            this.btnDeleteBarcode.Click += new System.EventHandler(this.btnDeleteBarcode_Click);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnExport, resources.GetString("btnExport.Error"));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnExport, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnExport.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnExport, ((int)(resources.GetObject("btnExport.IconPadding"))));
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnImport, resources.GetString("btnImport.Error"));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnImport, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnImport.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnImport, ((int)(resources.GetObject("btnImport.IconPadding"))));
            this.btnImport.Name = "btnImport";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtLeft
            // 
            resources.ApplyResources(this.txtLeft, "txtLeft");
            this.txtLeft.BackColor = System.Drawing.SystemColors.Window;
            this.txtLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLeft.Data = 0;
            this.txtLeft.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtLeft, resources.GetString("txtLeft.Error"));
            this.txtLeft.Format = null;
            this.errProvider.SetIconAlignment(this.txtLeft, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLeft.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtLeft, ((int)(resources.GetObject("txtLeft.IconPadding"))));
            this.txtLeft.isAllowNegative = false;
            this.txtLeft.isAllowSpecialChar = false;
            this.txtLeft.isNumberOnly = false;
            this.txtLeft.isNumeric = false;
            this.txtLeft.LeaveColor = System.Drawing.Color.White;
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLeft.TextChanged += new System.EventHandler(this.txtLeft_TextChanged);
            // 
            // txtPrefixVal
            // 
            resources.ApplyResources(this.txtPrefixVal, "txtPrefixVal");
            this.txtPrefixVal.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrefixVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrefixVal.Data = 0;
            this.txtPrefixVal.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtPrefixVal, resources.GetString("txtPrefixVal.Error"));
            this.txtPrefixVal.Format = null;
            this.errProvider.SetIconAlignment(this.txtPrefixVal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtPrefixVal.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtPrefixVal, ((int)(resources.GetObject("txtPrefixVal.IconPadding"))));
            this.txtPrefixVal.isAllowNegative = false;
            this.txtPrefixVal.isAllowSpecialChar = false;
            this.txtPrefixVal.isNumberOnly = false;
            this.txtPrefixVal.isNumeric = false;
            this.txtPrefixVal.LeaveColor = System.Drawing.Color.White;
            this.txtPrefixVal.Name = "txtPrefixVal";
            this.txtPrefixVal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPrefixVal.Validated += new System.EventHandler(this.txtPrefixVal_Validated);
            // 
            // grpControleSettings
            // 
            resources.ApplyResources(this.grpControleSettings, "grpControleSettings");
            this.grpControleSettings.BackColor = System.Drawing.Color.White;
            this.grpControleSettings.Controls.Add(this.btnEdit);
            this.grpControleSettings.Controls.Add(this.txtPrefixVal);
            this.grpControleSettings.Controls.Add(this.lblAlignment);
            this.grpControleSettings.Controls.Add(this.btnLogo);
            this.grpControleSettings.Controls.Add(this.btnFont);
            this.grpControleSettings.Controls.Add(this.txtBarcodeY);
            this.grpControleSettings.Controls.Add(this.lblControlPrefix);
            this.grpControleSettings.Controls.Add(this.lblFormat);
            this.grpControleSettings.Controls.Add(this.txtHeight);
            this.grpControleSettings.Controls.Add(this.lblControlLocation_Y);
            this.grpControleSettings.Controls.Add(this.cmbAlignment);
            this.grpControleSettings.Controls.Add(this.cmbBarcodeValue);
            this.grpControleSettings.Controls.Add(this.cbEncodeType);
            this.grpControleSettings.Controls.Add(this.lblControlLocation_X);
            this.grpControleSettings.Controls.Add(this.txtBarcodeX);
            this.grpControleSettings.Controls.Add(this.txtControl);
            this.grpControleSettings.Controls.Add(this.txtWidth);
            this.grpControleSettings.Controls.Add(this.lblControl);
            this.grpControleSettings.Controls.Add(this.lblControlHeight);
            this.grpControleSettings.Controls.Add(this.lblControlWidth);
            this.errProvider.SetError(this.grpControleSettings, resources.GetString("grpControleSettings.Error"));
            this.errProvider.SetIconAlignment(this.grpControleSettings, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpControleSettings.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpControleSettings, ((int)(resources.GetObject("grpControleSettings.IconPadding"))));
            this.grpControleSettings.Name = "grpControleSettings";
            this.grpControleSettings.TabStop = false;
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.errProvider.SetError(this.btnEdit, resources.GetString("btnEdit.Error"));
            this.errProvider.SetIconAlignment(this.btnEdit, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnEdit.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnEdit, ((int)(resources.GetObject("btnEdit.IconPadding"))));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblAlignment
            // 
            resources.ApplyResources(this.lblAlignment, "lblAlignment");
            this.errProvider.SetError(this.lblAlignment, resources.GetString("lblAlignment.Error"));
            this.errProvider.SetIconAlignment(this.lblAlignment, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblAlignment.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblAlignment, ((int)(resources.GetObject("lblAlignment.IconPadding"))));
            this.lblAlignment.Name = "lblAlignment";
            // 
            // btnLogo
            // 
            resources.ApplyResources(this.btnLogo, "btnLogo");
            this.btnLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnLogo, resources.GetString("btnLogo.Error"));
            this.btnLogo.FlatAppearance.BorderSize = 0;
            this.btnLogo.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnLogo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnLogo.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnLogo, ((int)(resources.GetObject("btnLogo.IconPadding"))));
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.UseVisualStyleBackColor = false;
            this.btnLogo.Click += new System.EventHandler(this.btnLogo_Click);
            // 
            // btnFont
            // 
            resources.ApplyResources(this.btnFont, "btnFont");
            this.btnFont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnFont, resources.GetString("btnFont.Error"));
            this.btnFont.FlatAppearance.BorderSize = 0;
            this.btnFont.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnFont, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnFont.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnFont, ((int)(resources.GetObject("btnFont.IconPadding"))));
            this.btnFont.Name = "btnFont";
            this.btnFont.UseVisualStyleBackColor = false;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // txtBarcodeY
            // 
            resources.ApplyResources(this.txtBarcodeY, "txtBarcodeY");
            this.txtBarcodeY.BackColor = System.Drawing.SystemColors.Window;
            this.txtBarcodeY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcodeY.Data = 0;
            this.txtBarcodeY.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtBarcodeY, resources.GetString("txtBarcodeY.Error"));
            this.txtBarcodeY.Format = null;
            this.errProvider.SetIconAlignment(this.txtBarcodeY, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtBarcodeY.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtBarcodeY, ((int)(resources.GetObject("txtBarcodeY.IconPadding"))));
            this.txtBarcodeY.isAllowNegative = false;
            this.txtBarcodeY.isAllowSpecialChar = false;
            this.txtBarcodeY.isNumberOnly = false;
            this.txtBarcodeY.isNumeric = false;
            this.txtBarcodeY.LeaveColor = System.Drawing.Color.White;
            this.txtBarcodeY.Name = "txtBarcodeY";
            this.txtBarcodeY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBarcodeY.TextChanged += new System.EventHandler(this.txtBarcodeX_TextChanged_1);
            // 
            // lblControlPrefix
            // 
            resources.ApplyResources(this.lblControlPrefix, "lblControlPrefix");
            this.errProvider.SetError(this.lblControlPrefix, resources.GetString("lblControlPrefix.Error"));
            this.errProvider.SetIconAlignment(this.lblControlPrefix, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControlPrefix.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControlPrefix, ((int)(resources.GetObject("lblControlPrefix.IconPadding"))));
            this.lblControlPrefix.Name = "lblControlPrefix";
            // 
            // lblFormat
            // 
            resources.ApplyResources(this.lblFormat, "lblFormat");
            this.errProvider.SetError(this.lblFormat, resources.GetString("lblFormat.Error"));
            this.errProvider.SetIconAlignment(this.lblFormat, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblFormat.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblFormat, ((int)(resources.GetObject("lblFormat.IconPadding"))));
            this.lblFormat.Name = "lblFormat";
            // 
            // txtHeight
            // 
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.BackColor = System.Drawing.SystemColors.Window;
            this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeight.Data = 0;
            this.txtHeight.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtHeight, resources.GetString("txtHeight.Error"));
            this.txtHeight.Format = null;
            this.errProvider.SetIconAlignment(this.txtHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtHeight.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtHeight, ((int)(resources.GetObject("txtHeight.IconPadding"))));
            this.txtHeight.isAllowNegative = false;
            this.txtHeight.isAllowSpecialChar = false;
            this.txtHeight.isNumberOnly = false;
            this.txtHeight.isNumeric = false;
            this.txtHeight.LeaveColor = System.Drawing.Color.White;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtHeight.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // lblControlLocation_Y
            // 
            resources.ApplyResources(this.lblControlLocation_Y, "lblControlLocation_Y");
            this.errProvider.SetError(this.lblControlLocation_Y, resources.GetString("lblControlLocation_Y.Error"));
            this.errProvider.SetIconAlignment(this.lblControlLocation_Y, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControlLocation_Y.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControlLocation_Y, ((int)(resources.GetObject("lblControlLocation_Y.IconPadding"))));
            this.lblControlLocation_Y.Name = "lblControlLocation_Y";
            // 
            // cmbAlignment
            // 
            resources.ApplyResources(this.cmbAlignment, "cmbAlignment");
            this.cmbAlignment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAlignment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAlignment.DropDownHeight = 300;
            this.cmbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlignment.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbAlignment, resources.GetString("cmbAlignment.Error"));
            this.cmbAlignment.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbAlignment, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbAlignment.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbAlignment, ((int)(resources.GetObject("cmbAlignment.IconPadding"))));
            this.cmbAlignment.Items.AddRange(new object[] {
            resources.GetString("cmbAlignment.Items"),
            resources.GetString("cmbAlignment.Items1"),
            resources.GetString("cmbAlignment.Items2"),
            resources.GetString("cmbAlignment.Items3")});
            this.cmbAlignment.LeaveColor = System.Drawing.Color.White;
            this.cmbAlignment.Name = "cmbAlignment";
            this.cmbAlignment.SelectedIndexChanged += new System.EventHandler(this.cmbAlignment_SelectedIndexChanged);
            // 
            // cmbBarcodeValue
            // 
            resources.ApplyResources(this.cmbBarcodeValue, "cmbBarcodeValue");
            this.cmbBarcodeValue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBarcodeValue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBarcodeValue.DropDownHeight = 300;
            this.cmbBarcodeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeValue.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbBarcodeValue, resources.GetString("cmbBarcodeValue.Error"));
            this.cmbBarcodeValue.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbBarcodeValue, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbBarcodeValue.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbBarcodeValue, ((int)(resources.GetObject("cmbBarcodeValue.IconPadding"))));
            this.cmbBarcodeValue.Items.AddRange(new object[] {
            resources.GetString("cmbBarcodeValue.Items"),
            resources.GetString("cmbBarcodeValue.Items1")});
            this.cmbBarcodeValue.LeaveColor = System.Drawing.Color.White;
            this.cmbBarcodeValue.Name = "cmbBarcodeValue";
            // 
            // cbEncodeType
            // 
            resources.ApplyResources(this.cbEncodeType, "cbEncodeType");
            this.cbEncodeType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEncodeType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEncodeType.DropDownHeight = 300;
            this.cbEncodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEncodeType.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cbEncodeType, resources.GetString("cbEncodeType.Error"));
            this.cbEncodeType.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cbEncodeType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cbEncodeType.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cbEncodeType, ((int)(resources.GetObject("cbEncodeType.IconPadding"))));
            this.cbEncodeType.Items.AddRange(new object[] {
            resources.GetString("cbEncodeType.Items"),
            resources.GetString("cbEncodeType.Items1"),
            resources.GetString("cbEncodeType.Items2"),
            resources.GetString("cbEncodeType.Items3"),
            resources.GetString("cbEncodeType.Items4"),
            resources.GetString("cbEncodeType.Items5"),
            resources.GetString("cbEncodeType.Items6"),
            resources.GetString("cbEncodeType.Items7"),
            resources.GetString("cbEncodeType.Items8"),
            resources.GetString("cbEncodeType.Items9")});
            this.cbEncodeType.LeaveColor = System.Drawing.Color.White;
            this.cbEncodeType.Name = "cbEncodeType";
            // 
            // lblControlLocation_X
            // 
            resources.ApplyResources(this.lblControlLocation_X, "lblControlLocation_X");
            this.errProvider.SetError(this.lblControlLocation_X, resources.GetString("lblControlLocation_X.Error"));
            this.errProvider.SetIconAlignment(this.lblControlLocation_X, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControlLocation_X.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControlLocation_X, ((int)(resources.GetObject("lblControlLocation_X.IconPadding"))));
            this.lblControlLocation_X.Name = "lblControlLocation_X";
            // 
            // txtBarcodeX
            // 
            resources.ApplyResources(this.txtBarcodeX, "txtBarcodeX");
            this.txtBarcodeX.BackColor = System.Drawing.SystemColors.Window;
            this.txtBarcodeX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcodeX.Data = 0;
            this.txtBarcodeX.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtBarcodeX, resources.GetString("txtBarcodeX.Error"));
            this.txtBarcodeX.Format = null;
            this.errProvider.SetIconAlignment(this.txtBarcodeX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtBarcodeX.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtBarcodeX, ((int)(resources.GetObject("txtBarcodeX.IconPadding"))));
            this.txtBarcodeX.isAllowNegative = false;
            this.txtBarcodeX.isAllowSpecialChar = false;
            this.txtBarcodeX.isNumberOnly = false;
            this.txtBarcodeX.isNumeric = false;
            this.txtBarcodeX.LeaveColor = System.Drawing.Color.White;
            this.txtBarcodeX.Name = "txtBarcodeX";
            this.txtBarcodeX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBarcodeX.TextChanged += new System.EventHandler(this.txtBarcodeX_TextChanged_1);
            // 
            // txtControl
            // 
            resources.ApplyResources(this.txtControl, "txtControl");
            this.txtControl.BackColor = System.Drawing.SystemColors.Window;
            this.txtControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtControl.Data = 0;
            this.txtControl.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtControl, resources.GetString("txtControl.Error"));
            this.txtControl.ForeColor = System.Drawing.Color.Crimson;
            this.txtControl.Format = null;
            this.errProvider.SetIconAlignment(this.txtControl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtControl.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtControl, ((int)(resources.GetObject("txtControl.IconPadding"))));
            this.txtControl.isAllowNegative = false;
            this.txtControl.isAllowSpecialChar = false;
            this.txtControl.isNumberOnly = false;
            this.txtControl.isNumeric = false;
            this.txtControl.LeaveColor = System.Drawing.Color.White;
            this.txtControl.Name = "txtControl";
            this.txtControl.ReadOnly = true;
            this.txtControl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtWidth
            // 
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.BackColor = System.Drawing.SystemColors.Window;
            this.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWidth.Data = 0;
            this.txtWidth.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtWidth, resources.GetString("txtWidth.Error"));
            this.txtWidth.Format = null;
            this.errProvider.SetIconAlignment(this.txtWidth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWidth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtWidth, ((int)(resources.GetObject("txtWidth.IconPadding"))));
            this.txtWidth.isAllowNegative = false;
            this.txtWidth.isAllowSpecialChar = false;
            this.txtWidth.isNumberOnly = false;
            this.txtWidth.isNumeric = false;
            this.txtWidth.LeaveColor = System.Drawing.Color.White;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // lblControl
            // 
            resources.ApplyResources(this.lblControl, "lblControl");
            this.errProvider.SetError(this.lblControl, resources.GetString("lblControl.Error"));
            this.errProvider.SetIconAlignment(this.lblControl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControl.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControl, ((int)(resources.GetObject("lblControl.IconPadding"))));
            this.lblControl.Name = "lblControl";
            // 
            // lblControlHeight
            // 
            resources.ApplyResources(this.lblControlHeight, "lblControlHeight");
            this.errProvider.SetError(this.lblControlHeight, resources.GetString("lblControlHeight.Error"));
            this.errProvider.SetIconAlignment(this.lblControlHeight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControlHeight.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControlHeight, ((int)(resources.GetObject("lblControlHeight.IconPadding"))));
            this.lblControlHeight.Name = "lblControlHeight";
            // 
            // lblControlWidth
            // 
            resources.ApplyResources(this.lblControlWidth, "lblControlWidth");
            this.errProvider.SetError(this.lblControlWidth, resources.GetString("lblControlWidth.Error"));
            this.errProvider.SetIconAlignment(this.lblControlWidth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblControlWidth.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblControlWidth, ((int)(resources.GetObject("lblControlWidth.IconPadding"))));
            this.lblControlWidth.Name = "lblControlWidth";
            // 
            // pnlLabel
            // 
            resources.ApplyResources(this.pnlLabel, "pnlLabel");
            this.pnlLabel.AllowDrop = true;
            this.pnlLabel.BackColor = System.Drawing.Color.White;
            this.pnlLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLabel.Controls.Add(this.Barcode_Panel);
            this.errProvider.SetError(this.pnlLabel, resources.GetString("pnlLabel.Error"));
            this.errProvider.SetIconAlignment(this.pnlLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlLabel.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pnlLabel, ((int)(resources.GetObject("pnlLabel.IconPadding"))));
            this.pnlLabel.Name = "pnlLabel";
            // 
            // Barcode_Panel
            // 
            resources.ApplyResources(this.Barcode_Panel, "Barcode_Panel");
            this.Barcode_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barcode_Panel.Controls.Add(this.pbBarcode);
            this.errProvider.SetError(this.Barcode_Panel, resources.GetString("Barcode_Panel.Error"));
            this.errProvider.SetIconAlignment(this.Barcode_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Barcode_Panel.IconAlignment"))));
            this.errProvider.SetIconPadding(this.Barcode_Panel, ((int)(resources.GetObject("Barcode_Panel.IconPadding"))));
            this.Barcode_Panel.Name = "Barcode_Panel";
            this.Barcode_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Barcode_Panel_MouseDown);
            this.Barcode_Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Barcode_Panel_MouseMove);
            this.Barcode_Panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Barcode_Panel_MouseUp);
            // 
            // pbBarcode
            // 
            resources.ApplyResources(this.pbBarcode, "pbBarcode");
            this.pbBarcode.BackColor = System.Drawing.Color.DarkGray;
            this.errProvider.SetError(this.pbBarcode, resources.GetString("pbBarcode.Error"));
            this.errProvider.SetIconAlignment(this.pbBarcode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pbBarcode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pbBarcode, ((int)(resources.GetObject("pbBarcode.IconPadding"))));
            this.pbBarcode.Name = "pbBarcode";
            this.pbBarcode.TabStop = false;
            this.pbBarcode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBarcode_MouseDown);
            this.pbBarcode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbBarcode_MouseMove);
            this.pbBarcode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBarcode_MouseUp);
            // 
            // pnlMargin
            // 
            resources.ApplyResources(this.pnlMargin, "pnlMargin");
            this.pnlMargin.BackColor = System.Drawing.Color.LightGray;
            this.pnlMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMargin.Controls.Add(this.pbBarcodeLabel);
            this.pnlMargin.Controls.Add(this.pnlLabel);
            this.errProvider.SetError(this.pnlMargin, resources.GetString("pnlMargin.Error"));
            this.errProvider.SetIconAlignment(this.pnlMargin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlMargin.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pnlMargin, ((int)(resources.GetObject("pnlMargin.IconPadding"))));
            this.pnlMargin.Name = "pnlMargin";
            // 
            // pbBarcodeLabel
            // 
            resources.ApplyResources(this.pbBarcodeLabel, "pbBarcodeLabel");
            this.pbBarcodeLabel.BackColor = System.Drawing.Color.White;
            this.pbBarcodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errProvider.SetError(this.pbBarcodeLabel, resources.GetString("pbBarcodeLabel.Error"));
            this.errProvider.SetIconAlignment(this.pbBarcodeLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pbBarcodeLabel.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pbBarcodeLabel, ((int)(resources.GetObject("pbBarcodeLabel.IconPadding"))));
            this.pbBarcodeLabel.Name = "pbBarcodeLabel";
            this.pbBarcodeLabel.TabStop = false;
            this.pbBarcodeLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBarcodeLabel_MouseDown);
            this.pbBarcodeLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbBarcodeLabel_MouseMove);
            this.pbBarcodeLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBarcodeLabel_MouseUp);
            // 
            // pnlWorkSpace
            // 
            resources.ApplyResources(this.pnlWorkSpace, "pnlWorkSpace");
            this.pnlWorkSpace.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlWorkSpace.Controls.Add(this.rbMRPSlip);
            this.pnlWorkSpace.Controls.Add(this.rbQRCode);
            this.pnlWorkSpace.Controls.Add(this.rbBarcode);
            this.pnlWorkSpace.Controls.Add(this.pnlMargin);
            this.errProvider.SetError(this.pnlWorkSpace, resources.GetString("pnlWorkSpace.Error"));
            this.errProvider.SetIconAlignment(this.pnlWorkSpace, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pnlWorkSpace.IconAlignment"))));
            this.errProvider.SetIconPadding(this.pnlWorkSpace, ((int)(resources.GetObject("pnlWorkSpace.IconPadding"))));
            this.pnlWorkSpace.Name = "pnlWorkSpace";
            // 
            // rbMRPSlip
            // 
            resources.ApplyResources(this.rbMRPSlip, "rbMRPSlip");
            this.errProvider.SetError(this.rbMRPSlip, resources.GetString("rbMRPSlip.Error"));
            this.errProvider.SetIconAlignment(this.rbMRPSlip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbMRPSlip.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rbMRPSlip, ((int)(resources.GetObject("rbMRPSlip.IconPadding"))));
            this.rbMRPSlip.Name = "rbMRPSlip";
            this.rbMRPSlip.UseVisualStyleBackColor = true;
            this.rbMRPSlip.Click += new System.EventHandler(this.rbBarcode_Click);
            // 
            // rbQRCode
            // 
            resources.ApplyResources(this.rbQRCode, "rbQRCode");
            this.errProvider.SetError(this.rbQRCode, resources.GetString("rbQRCode.Error"));
            this.errProvider.SetIconAlignment(this.rbQRCode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbQRCode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rbQRCode, ((int)(resources.GetObject("rbQRCode.IconPadding"))));
            this.rbQRCode.Name = "rbQRCode";
            this.rbQRCode.UseVisualStyleBackColor = true;
            this.rbQRCode.Click += new System.EventHandler(this.rbBarcode_Click);
            // 
            // rbBarcode
            // 
            resources.ApplyResources(this.rbBarcode, "rbBarcode");
            this.rbBarcode.Checked = true;
            this.errProvider.SetError(this.rbBarcode, resources.GetString("rbBarcode.Error"));
            this.errProvider.SetIconAlignment(this.rbBarcode, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("rbBarcode.IconAlignment"))));
            this.errProvider.SetIconPadding(this.rbBarcode, ((int)(resources.GetObject("rbBarcode.IconPadding"))));
            this.rbBarcode.Name = "rbBarcode";
            this.rbBarcode.TabStop = true;
            this.rbBarcode.UseVisualStyleBackColor = true;
            this.rbBarcode.Click += new System.EventHandler(this.rbBarcode_Click);
            // 
            // lblRightMargin
            // 
            resources.ApplyResources(this.lblRightMargin, "lblRightMargin");
            this.errProvider.SetError(this.lblRightMargin, resources.GetString("lblRightMargin.Error"));
            this.errProvider.SetIconAlignment(this.lblRightMargin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblRightMargin.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblRightMargin, ((int)(resources.GetObject("lblRightMargin.IconPadding"))));
            this.lblRightMargin.Name = "lblRightMargin";
            // 
            // txtAcross
            // 
            resources.ApplyResources(this.txtAcross, "txtAcross");
            this.txtAcross.BackColor = System.Drawing.SystemColors.Window;
            this.txtAcross.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAcross.Data = 0;
            this.txtAcross.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.txtAcross, resources.GetString("txtAcross.Error"));
            this.txtAcross.Format = null;
            this.errProvider.SetIconAlignment(this.txtAcross, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtAcross.IconAlignment"))));
            this.errProvider.SetIconPadding(this.txtAcross, ((int)(resources.GetObject("txtAcross.IconPadding"))));
            this.txtAcross.isAllowNegative = false;
            this.txtAcross.isAllowSpecialChar = false;
            this.txtAcross.isNumberOnly = false;
            this.txtAcross.isNumeric = false;
            this.txtAcross.LeaveColor = System.Drawing.Color.White;
            this.txtAcross.Name = "txtAcross";
            this.txtAcross.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAcross.TextChanged += new System.EventHandler(this.txtAcross_TextChanged);
            // 
            // lblLeftMargin
            // 
            resources.ApplyResources(this.lblLeftMargin, "lblLeftMargin");
            this.errProvider.SetError(this.lblLeftMargin, resources.GetString("lblLeftMargin.Error"));
            this.errProvider.SetIconAlignment(this.lblLeftMargin, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lblLeftMargin.IconAlignment"))));
            this.errProvider.SetIconPadding(this.lblLeftMargin, ((int)(resources.GetObject("lblLeftMargin.IconPadding"))));
            this.lblLeftMargin.Name = "lblLeftMargin";
            // 
            // grpLabelSettings
            // 
            resources.ApplyResources(this.grpLabelSettings, "grpLabelSettings");
            this.grpLabelSettings.BackColor = System.Drawing.Color.White;
            this.grpLabelSettings.Controls.Add(this.txtLabelHt);
            this.grpLabelSettings.Controls.Add(this.lblPrintPerPage);
            this.grpLabelSettings.Controls.Add(this.txtLabelWth);
            this.grpLabelSettings.Controls.Add(this.lblTotalPerPage);
            this.grpLabelSettings.Controls.Add(this.lblDownCount);
            this.grpLabelSettings.Controls.Add(this.lblAcrossCount);
            this.grpLabelSettings.Controls.Add(this.cbBorder);
            this.grpLabelSettings.Controls.Add(this.txtDown);
            this.grpLabelSettings.Controls.Add(this.lblLabelHeight);
            this.grpLabelSettings.Controls.Add(this.txtBottom);
            this.grpLabelSettings.Controls.Add(this.txtTop);
            this.grpLabelSettings.Controls.Add(this.lblBottomMarging);
            this.grpLabelSettings.Controls.Add(this.txtRight);
            this.grpLabelSettings.Controls.Add(this.lblLabelWidth);
            this.grpLabelSettings.Controls.Add(this.lblTopMargin);
            this.grpLabelSettings.Controls.Add(this.txtLeft);
            this.grpLabelSettings.Controls.Add(this.lblRightMargin);
            this.grpLabelSettings.Controls.Add(this.txtAcross);
            this.grpLabelSettings.Controls.Add(this.lblLeftMargin);
            this.errProvider.SetError(this.grpLabelSettings, resources.GetString("grpLabelSettings.Error"));
            this.errProvider.SetIconAlignment(this.grpLabelSettings, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpLabelSettings.IconAlignment"))));
            this.errProvider.SetIconPadding(this.grpLabelSettings, ((int)(resources.GetObject("grpLabelSettings.IconPadding"))));
            this.grpLabelSettings.Name = "grpLabelSettings";
            this.grpLabelSettings.TabStop = false;
            // 
            // cmbParameter
            // 
            resources.ApplyResources(this.cmbParameter, "cmbParameter");
            this.cmbParameter.AllowDrop = true;
            this.cmbParameter.EnterColor = System.Drawing.Color.LightPink;
            this.errProvider.SetError(this.cmbParameter, resources.GetString("cmbParameter.Error"));
            this.cmbParameter.FormattingEnabled = true;
            this.errProvider.SetIconAlignment(this.cmbParameter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbParameter.IconAlignment"))));
            this.errProvider.SetIconPadding(this.cmbParameter, ((int)(resources.GetObject("cmbParameter.IconPadding"))));
            this.cmbParameter.Items.AddRange(new object[] {
            resources.GetString("cmbParameter.Items"),
            resources.GetString("cmbParameter.Items1"),
            resources.GetString("cmbParameter.Items2"),
            resources.GetString("cmbParameter.Items3"),
            resources.GetString("cmbParameter.Items4"),
            resources.GetString("cmbParameter.Items5"),
            resources.GetString("cmbParameter.Items6"),
            resources.GetString("cmbParameter.Items7"),
            resources.GetString("cmbParameter.Items8"),
            resources.GetString("cmbParameter.Items9"),
            resources.GetString("cmbParameter.Items10"),
            resources.GetString("cmbParameter.Items11"),
            resources.GetString("cmbParameter.Items12"),
            resources.GetString("cmbParameter.Items13"),
            resources.GetString("cmbParameter.Items14"),
            resources.GetString("cmbParameter.Items15"),
            resources.GetString("cmbParameter.Items16"),
            resources.GetString("cmbParameter.Items17"),
            resources.GetString("cmbParameter.Items18"),
            resources.GetString("cmbParameter.Items19"),
            resources.GetString("cmbParameter.Items20"),
            resources.GetString("cmbParameter.Items21"),
            resources.GetString("cmbParameter.Items22"),
            resources.GetString("cmbParameter.Items23"),
            resources.GetString("cmbParameter.Items24"),
            resources.GetString("cmbParameter.Items25"),
            resources.GetString("cmbParameter.Items26")});
            this.cmbParameter.LeaveColor = System.Drawing.Color.White;
            this.cmbParameter.Name = "cmbParameter";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnRefresh, resources.GetString("btnRefresh.Error"));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnRefresh, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnRefresh.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnRefresh, ((int)(resources.GetObject("btnRefresh.IconPadding"))));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnDelete, resources.GetString("btnDelete.Error"));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnDelete, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnDelete.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnDelete, ((int)(resources.GetObject("btnDelete.IconPadding"))));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.errProvider.SetError(this.btnAdd, resources.GetString("btnAdd.Error"));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.errProvider.SetIconAlignment(this.btnAdd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnAdd.IconAlignment"))));
            this.errProvider.SetIconPadding(this.btnAdd, ((int)(resources.GetObject("btnAdd.IconPadding"))));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // BarcodeDesign
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbParameter);
            this.Controls.Add(this.lblParameter);
            this.Controls.Add(this.grpBarcode);
            this.Controls.Add(this.pnlTesting);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grpPaperSetup);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.grpMeasurement);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.grpSave);
            this.Controls.Add(this.grpControleSettings);
            this.Controls.Add(this.pnlWorkSpace);
            this.Controls.Add(this.grpLabelSettings);
            this.Name = "BarcodeDesign";
            this.Load += new System.EventHandler(this.BarcodeDesign_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarcodeDesign_KeyDown);
            this.Controls.SetChildIndex(this.grpLabelSettings, 0);
            this.Controls.SetChildIndex(this.pnlWorkSpace, 0);
            this.Controls.SetChildIndex(this.grpControleSettings, 0);
            this.Controls.SetChildIndex(this.grpSave, 0);
            this.Controls.SetChildIndex(this.btnPreview, 0);
            this.Controls.SetChildIndex(this.grpMeasurement, 0);
            this.Controls.SetChildIndex(this.lblHeader, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnEncode, 0);
            this.Controls.SetChildIndex(this.grpPaperSetup, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.pnlTesting, 0);
            this.Controls.SetChildIndex(this.grpBarcode, 0);
            this.Controls.SetChildIndex(this.lblParameter, 0);
            this.Controls.SetChildIndex(this.cmbParameter, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.pnlTesting.ResumeLayout(false);
            this.pnlTesting.PerformLayout();
            this.grpPaperSetup.ResumeLayout(false);
            this.grpPaperSetup.PerformLayout();
            this.grpMeasurement.ResumeLayout(false);
            this.grpMeasurement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.grpSave.ResumeLayout(false);
            this.grpSave.PerformLayout();
            this.grpControleSettings.ResumeLayout(false);
            this.grpControleSettings.PerformLayout();
            this.pnlLabel.ResumeLayout(false);
            this.Barcode_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcode)).EndInit();
            this.pnlMargin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcodeLabel)).EndInit();
            this.pnlWorkSpace.ResumeLayout(false);
            this.pnlWorkSpace.PerformLayout();
            this.grpLabelSettings.ResumeLayout(false);
            this.grpLabelSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
