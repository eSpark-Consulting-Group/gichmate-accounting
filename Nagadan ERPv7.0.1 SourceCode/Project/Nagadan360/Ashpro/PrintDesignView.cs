using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class PrintDesignView : BaseForm
	{
		private Point firstPoint = default(Point);

		private Control ctrl;

		private AshTextBoxNormal tBox;

		private Point PanelMouseDownLocation;

		private TextBox txtControlBox;

		private bool mouseClickedOnCstmTBox = false;

		private int intY = 0;

		private int intSp = 1;

		private int intln = 1;

		private int intBk = 1;

		private int intSortIndex = 1;

		private bool isSortOn = false;
        private bool isFocused = false;
        private bool isDeleted = false;

        private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl = new PartyDetails();

		private TransactionMaster entTMaster;

		private SalesMaster entSMaster;

		private Transaction entTransaction;

		private InvoiceAdjustment entAdjustment;

		private List<InvoiceAdjustment> entAdjustments = new List<InvoiceAdjustment>();

		private List<SalesDetails> entSDetails = new List<SalesDetails>();

		private List<Transaction> entTransactions = new List<Transaction>();

		private int iTabindex;

		private int? iMasterID;

		private OpenFileDialog dlg;

		private DBContext db;

		private bool isEditable = false;

		private IContainer components = null;

		private Panel pnlWorkSpace;

		private TabControl tabSettings;

		private TabPage tabPage;

		private Button btnPreview;

		private Button btnPrint;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Label lblLinePerPage;

		private Label lblDown;

		private Label lblPitch;

		private Label lblBoldOff;

		private Label lblBoldOn;

		private Label lblReverse;

		private Label lblFilePath;

		private Label lblUp;

		private Label lblPrinters;

		private Label lblPageWidth;

		private Label label1;

		private AshComboBox cmbPrinter;

		private AshTextBoxNormal txtBackgroundImage;

		private AshTextBoxNormal txtBoldOff;

		private AshTextBoxNormal txtBoldOn;

		private AshTextBoxNormal txtPitch;

		private AshTextBoxNormal txtReverse;

		private AshTextBoxNormal txtLinesPerPage;

		private AshTextBoxNormal txtDownScoll;

		private AshTextBoxNormal txtUpScroll;

		private AshTextBoxNormal txtPageWidth;

		private AshComboBox cmbPrintType;

		private Label label3;

		private Label label2;

		private AshComboBox cmbDesignName;

		private AshComboBox cmbVoucher;

		private CheckBox cbDefault;

		private AshTextBoxNormal txtPageHeight;

		private Label label4;

		private AshTextBoxNormal txtSpaceInLines;

		private Label label5;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Button btnRemove;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label9;

		private Label label11;

		private Label label12;

		private GroupBox groupBox2;

		private Button btnAdd;

		private Label label13;

		private Label label14;

		private Label label15;

		private AshTextBoxNormal txtWidth;

		private AshTextBoxNormal txtText;

		private AshTextBoxNormal txtName;

		private AshComboBox cmbType;

		private AshComboBox cmbPosition;

		private AshComboBox cmbAlgmnt;

		private AshComboBox cmbControl;

		private AshComboBox cmbControlType;

		private AshComboBox cmbControlPosition;

		private AshTextBoxNormal txtY;

		private AshTextBoxNormal txtX;

		private Label label17;

		private Label label16;

		private Button button8;

		private Label lblTitle;

		private AshTextBoxNormal txtHeight;

		private Label label20;

		private Button btnBrowseImage;

		private Button btnOpenXML;

		private Button btnXMLSave;

		private AshComboBox cmbFormType;

		private Label label21;

		private AshTextBoxNormal txtSpec;

		private Label label22;

		private Button btnDeleteImage;

		private AshTextBoxNormal txtSpaceBtwnLines;

		private Button btnEdit;
        private Button btnUpdate;
        private CheckBox cbBold;
        private AshTextBoxNormal txtFontSize;
        private AshTextBoxNormal txtTabindex;
        private Label label10;
        private Label label18;
        private Button btnSort;
        private Button btnFont;

		public PrintDesignView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void PrintDesignView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			base.WindowState = FormWindowState.Maximized;
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cmbPrinter.Items.Add(installedPrinter);
			}
			if (cmbPrinter.Items.Count > 0)
			{
				cmbPrinter.SelectedIndex = 0;
			}
			cmbVoucher.SelectedIndex = 0;
			cmbPrintType.SelectedIndex = 0;
			base.ActiveControl = cmbVoucher;
		}

		private void tabSettings_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				firstPoint = Control.MousePosition;
			}
		}

		private void tabSettings_MouseMove(object sender, MouseEventArgs e)
		{
			ctrl = (Control)sender;
			if (e.Button == MouseButtons.Left)
			{
				Point mousePosition = Control.MousePosition;
				Point point = new Point(firstPoint.X - mousePosition.X, firstPoint.Y - mousePosition.Y);
				ctrl.Location = new Point(ctrl.Location.X - point.X, ctrl.Location.Y - point.Y);
				firstPoint = mousePosition;
			}
		}

		private void btnClear_Click_1(object sender, EventArgs e)
		{
			entDSettings = new List<DotmatrixSettings>();
			isEditable = false;
			btnEdit.BackColor = Settings.Default.BackColor;
			entDMaster = new DotmatrixMaster();
			pnlWorkSpace.BackgroundImage = null;
			isEditable = false;
			mouseClickedOnCstmTBox = false;
			pnlWorkSpace.Controls.Clear();
			TabPageClearMethod(tabPage);
			TabPageClearMethod(tabPage1);
			btnDelete.Enabled = false;
			iMasterID = null;
			Cursor = Cursors.Arrow;
		}

		private void txtControlBox_DoubleClick(object sender, EventArgs e)
		{
		}

		private void txtControlBox_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				ctrl = (Control)sender;
				if (mouseClickedOnCstmTBox)
				{
					ctrl.Left += e.X - PanelMouseDownLocation.X;
					ctrl.Top += e.Y - PanelMouseDownLocation.Y;
					DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
					if (dotmatrixSettings != null)
					{
						int num3 = (dotmatrixSettings.X = (txtX.Data = ctrl.Location.X));
						num3 = (dotmatrixSettings.Y = (txtY.Data = ctrl.Location.Y));
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 163");
			}
		}

		private void txtControlBox_Click(object sender, EventArgs e)
		{
			try
			{
				isEditable = false;
				btnEdit.BackColor = Settings.Default.BackColor;
				ctrl = (Control)sender;
				DotmatrixSettings dotmatrixSettings = entDSettings.FirstOrDefault((DotmatrixSettings x) => x.ControleName == ctrl.Name && x.Position == ctrl.Tag.ToString());
				if (dotmatrixSettings != null)
				{
					txtName.Text = dotmatrixSettings.ControleName;
					txtText.Text = dotmatrixSettings.FeildText;
					cmbType.Text = dotmatrixSettings.Type;
					txtX.Data = ctrl.Location.X;
					txtY.Data = ctrl.Location.Y;
					cmbAlgmnt.Text = dotmatrixSettings.Alaingmnet;
					cmbControlPosition.Text = dotmatrixSettings.Position;
					txtWidth.Data = ctrl.Width;
					txtHeight.Data = ctrl.Height;
					cmbPosition.Text = dotmatrixSettings.Position;
					txtSpec.Text = dotmatrixSettings.Spec;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "194");
			}
		}

		private void btnPrint_Click_1(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do You want to Print the Bill", Utility.uManufacturer, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				PrintBill();
			}
		}

		private void btnPreview_Click_1(object sender, EventArgs e)
		{
			PrintBill(isPrint: false);
		}

		private void cmbControlType_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			cmbControl.Text = string.Empty;
			cmbControl.Items.Clear();
			intBk = 1;
			intln = 1;
			intSp = 1;
			if (cmbControlType.Text == "SpaceControl")
			{
				foreach (Control control4 in pnlWorkSpace.Controls)
				{
					if (control4.BackColor == Color.LightBlue)
					{
						intSp++;
					}
				}
				cmbControl.Text = "Space" + intSp;
			}
			else if (cmbControlType.Text == "LineLabelControl")
			{
				foreach (Control control5 in pnlWorkSpace.Controls)
				{
					if (control5.BackColor == Color.Gold)
					{
						intln++;
					}
				}
				cmbControl.Text = "Line" + intln;
			}
			else if (cmbControlType.Text == "ValueControle")
			{
				if (cmbControlPosition.SelectedIndex == 0)
				{
					if (entcompany == null)
					{
						entcompany = new ExCompany();
					}
					AniHelper.PropertyFindMethod(entcompany, cmbControl);
				}
				else if (cmbControlPosition.SelectedIndex == 1)
				{
					if (entTMaster == null)
					{
						entTMaster = new TransactionMaster();
					}
					AniHelper.PropertyFindMethod(entTMaster, cmbControl);
					if (CommonSettings.blnGSTEnabled)
					{
						cmbControl.Items.Remove("VatAmount");
					}
					else
					{
						cmbControl.Items.Remove("SGSTAmount");
						cmbControl.Items.Remove("CGSTAmount");
						cmbControl.Items.Remove("IGSTAmount");
						cmbControl.Items.Remove("GSTAmount");
					}
					if (!CommonSettings.blnCessEnabled)
					{
						cmbControl.Items.Remove("CessAmount");
					}
				}
				else if (cmbControlPosition.SelectedIndex == 2)
				{
					if (entPartyDtl == null)
					{
						entPartyDtl = new PartyDetails();
					}
					AniHelper.PropertyFindMethod(entPartyDtl, cmbControl);
				}
				else if (cmbControlPosition.SelectedIndex == 4)
				{
					if (entTransaction == null)
					{
						entTransaction = new Transaction();
					}
					AniHelper.PropertyFindMethod(entTransaction, cmbControl);
					if (CommonSettings.blnGSTEnabled)
					{
						cmbControl.Items.Remove("Vat");
						cmbControl.Items.Remove("VatAmount");
					}
					else
					{
						cmbControl.Items.Remove("Gst");
						cmbControl.Items.Remove("GstAmount");
						cmbControl.Items.Remove("Sgst");
						cmbControl.Items.Remove("SGstAmount");
						cmbControl.Items.Remove("CGst");
						cmbControl.Items.Remove("CGstAmount");
						cmbControl.Items.Remove("IGst");
						cmbControl.Items.Remove("IGstAmount");
						cmbControl.Items.Remove("Hsn");
					}
				}
				else if (cmbControlPosition.SelectedIndex == 5)
				{
					if (entTMaster == null)
					{
						entTMaster = new TransactionMaster();
					}
					AniHelper.PropertyFindMethod(entTMaster, cmbControl);
				}
				else if (cmbControlPosition.SelectedIndex == 6)
				{
					if (entAdjustment == null)
					{
						entAdjustment = new InvoiceAdjustment();
					}
					AniHelper.PropertyFindMethod(entAdjustment, cmbControl);
				}
				else if (cmbControlPosition.SelectedIndex == 7)
				{
					if (entTMaster == null)
					{
						entTMaster = new TransactionMaster();
					}
					AniHelper.PropertyFindMethod(entTMaster, cmbControl);
				}
				if (cmbControl.Items.Count > 0)
				{
					cmbControl.SelectedIndex = 0;
				}
			}
			else
			{
				if (!(cmbControlType.Text == "BrakeControle"))
				{
					return;
				}
				foreach (Control control6 in pnlWorkSpace.Controls)
				{
					if (control6.BackColor == Color.Orchid)
					{
						intBk++;
					}
				}
				cmbControl.Text = "Break" + intBk;
			}
		}

		private void cmbVoucher_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbFormType.DataSource != null)
			{
				cmbFormType.DataSource = null;
			}
			else
			{
				cmbFormType.Items.Clear();
			}
			if (cmbVoucher.SelectedIndex == 0)
			{
				AniHelper.FillFormType(cmbFormType);
				return;
			}
			cmbFormType.Items.Add(cmbVoucher.Text);
			cmbFormType.SelectedIndex = 0;
		}

		private void txtText_TextChanged(object sender, EventArgs e)
		{
			foreach (Control control in pnlWorkSpace.Controls)
			{
				if (control.Name == txtName.Text && control.Tag.ToString() == cmbPosition.Text && isEditable)
				{
					control.Text = txtText.Text;
					DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
					if (dotmatrixSettings != null)
					{
						dotmatrixSettings.FeildText = control.Text;
					}
				}
			}
		}

		private void cmbAlgmnt_SelectionChangeCommitted(object sender, EventArgs e)
		{
		}

		private void btnSave_Click_1(object sender, EventArgs e)
		{
			if (entDSettings.Count == 0)
			{
				Messages.ErrorMessage("Design Not Completed");
				return;
			}
			Cursor = Cursors.WaitCursor;
			if (cmbDesignName.Text == string.Empty)
			{
				Messages.InformationMessage("Please put a name for the design");
				cmbDesignName.Focus();
				return;
			}
			if (AshproCommon.IdFindMethod("Select count(printMasterId) as Count From PrintDesignMaster where VochrType='" + cmbVoucher.Text + "' and FormType='" + cmbFormType.Text + "'") > 0 && cbDefault.Checked)
			{
				AshproCommon.DatabaseMethod("Update PrintDesignMaster set isDefault='" + false + "' where VochrType='" + cmbVoucher.Text + "' and FormType='" + cmbFormType.Text + "'");
			}
			entDMaster = DotmatrixMasterMethod();
			if (!iMasterID.HasValue)
			{
				try
				{
					if (db.DotmatrixMasters().Any((DotmatrixMaster x) => x.DesignName == cmbDesignName.Text))
					{
						Messages.ErrorMessage("Design Already Exist!");
						return;
					}
					entDMaster.printMasterId = null;
					if (AshproCommon.InsertToDatabaseObj(entDMaster, "PrintDesignMaster"))
					{
						int num = AshproCommon.IdFindMethod("Select printMasterId From PrintDesignMaster Where DesignName='" + cmbDesignName.Text + "' and VochrType='" + cmbVoucher.Text + "' and PrintType='" + cmbPrintType.Text + "'", isVoucherNo: false);
						if (num > 0)
						{
							foreach (DotmatrixSettings entDSetting in entDSettings)
							{
								entDSetting.printMasterId = num;
								entDSetting.PrintDesignSettingsID = null;
							}
							if (AshproCommon.InsertToDatabase(entDSettings.Cast<object>().ToList(), "PrintDesignSettings"))
							{
								Messages.SavedMessage();
							}
						}
					}
				}
				catch (Exception ex)
				{
					Messages.ErrorMessage(ex.Message);
				}
			}
			else
			{
				try
				{
					if (AshproCommon.DeleteFromDatabase("PrintDesignSettings", "printMasterId", iMasterID.ToInt32()) && AshproCommon.UpdateToDatabaseObj(entDMaster, "PrintDesignMaster", "printMasterId", iMasterID.ToInt32()))
					{
						foreach (DotmatrixSettings entDSetting2 in entDSettings)
						{
							entDSetting2.printMasterId = iMasterID.ToInt32();
							entDSetting2.PrintDesignSettingsID = null;
						}
						if (AshproCommon.InsertToDatabase(entDSettings.Cast<object>().ToList(), "PrintDesignSettings"))
						{
							Messages.UpdateMessage();
						}
					}
				}
				catch (Exception ex2)
				{
					Messages.ErrorMessage(ex2.Message);
				}
			}
			mouseClickedOnCstmTBox = false;
			btnDelete.Enabled = false;
			btnClear_Click_1(null, null);
			Cursor = Cursors.Arrow;
		}

		private void btnBrowseImage_Click(object sender, EventArgs e)
		{
			try
			{
				using (dlg = new OpenFileDialog())
				{
					dlg.Title = "Open Background Image";
					dlg.Filter = "All Picture Files(*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg;*.Jpeg)|*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg;*.Jpeg";
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						txtBackgroundImage.Text = dlg.FileName;
						pnlWorkSpace.BackgroundImage = Image.FromFile(dlg.FileName);
						pnlWorkSpace.BackgroundImageLayout = ImageLayout.Stretch;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "653");
			}
		}

		private void cmbDesignName_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (cmbDesignName.Text != string.Empty && cmbVoucher.Text != string.Empty)
				{
					ReloadMethod();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void cmbPrintType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				cmbDesignName.Text = string.Empty;
				AshproCommon.FillCombo(cmbDesignName, "Select printMasterId as Id,DesignName as Name From PrintDesignMaster Where VochrType='" + cmbVoucher.Text + "' and  FormType='" + cmbFormType.Text + "' and PrintType='" + cmbPrintType.Text + "'");
				if (cmbDesignName.Items.Count > 0)
				{
					cmbDesignName.SelectedIndex = 0;
				}
				if (cmbPrintType.SelectedIndex != 0)
				{
					if (cmbControlType.Items.Contains("BrakeControle"))
					{
						cmbControlType.Items.Remove("BrakeControle");
					}
					if (cmbControlType.Items.Contains("SpaceControl"))
					{
						cmbControlType.Items.Remove("SpaceControl");
					}
				}
				else
				{
					if (!cmbControlType.Items.Contains("BrakeControle"))
					{
						cmbControlType.Items.Add("BrakeControle");
					}
					if (!cmbControlType.Items.Contains("SpaceControl"))
					{
						cmbControlType.Items.Add("SpaceControl");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "705");
			}
		}

		private void TabPageClearMethod(TabPage tabPage)
		{
			try
			{
				foreach (object control in tabPage.Controls)
				{
					if (control is AshTextBoxNormal)
					{
						tBox = (AshTextBoxNormal)control;
						if (tBox.isNumberOnly)
						{
							tBox.Text = "0";
						}
						else
						{
							tBox.Text = string.Empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 760");
			}
		}

		private void RefreshMethod()
		{
			try
			{
				List<DotmatrixSettings> list = new List<DotmatrixSettings>();
				DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
				foreach (Control control in pnlWorkSpace.Controls)
				{
					if (control is TextBox)
					{
						dotmatrixSettings = entDSettings.FirstOrDefault((DotmatrixSettings x) => x.ControleName == ctrl.Name);
						dotmatrixSettings.TabIndex = control.TabIndex;
						list.Add(dotmatrixSettings);
					}
				}
				list = (entDSettings = RemoveDuplicate(list));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 749");
			}
		}

		private List<DotmatrixSettings> RemoveDuplicate(List<DotmatrixSettings> dSettings)
		{
			IEqualityComparer<DotmatrixSettings> comparer = EqualityComparer.Create((DotmatrixSettings c) => c.ControleName);
			return dSettings.Distinct(comparer).ToList();
		}

		private void AddMethod(string text, string Name, string sPosition)
		{
			if (cmbControlType.Text == "LabelControl")
			{
				AddMethodTypeWise(text, Name, 200, 20, Color.Aqua, 0, intY, "Left", 8, "Microsoft Sans Serif", isBold: false, sPosition);
			}
			else if (cmbControlType.Text == "ValueControle")
			{
				AddMethodTypeWise(text, Name, 200, 20, Color.Pink, 0, intY, "Left", 8, "Microsoft Sans Serif", isBold: false, sPosition);
			}
			else if (cmbControlType.Text == "LineLabelControl")
			{
				AddMethodTypeWise(text, Name, 200, 20, Color.Gold, 0, intY, "Left", 8, "Microsoft Sans Serif", isBold: false, sPosition);
			}
			else if (cmbControlType.Text == "SpaceControl")
			{
				AddMethodTypeWise(text, Name, 200, 20, Color.LightBlue, 0, intY, "Left", 8, "Microsoft Sans Serif", isBold: false, sPosition);
			}
			else
			{
				AddMethodTypeWise(text, Name, 200, 20, Color.Orchid, 0, intY, "Left", 8, "Microsoft Sans Serif", isBold: false, sPosition);
			}
		}

		private void AddMethodTypeWise(string text, string Name, int width, int hight, Color color, int X, int Y, string sAlignment, int fSize, string sFont, bool isBold, string sPosition)
		{
			if (fSize < 8)
			{
				fSize = 10;
			}
			Font font = ((!isBold) ? new Font(sFont, fSize, FontStyle.Regular) : new Font(sFont, fSize, FontStyle.Bold));
			txtControlBox = new TextBox();
			txtControlBox.AutoSize = false;
			txtControlBox.BorderStyle = BorderStyle.FixedSingle;
			txtControlBox.Tag = sPosition;
			txtControlBox.Name = Name;
			txtControlBox.Text = text;
			txtControlBox.Height = hight;
			txtControlBox.Width = width;
			txtControlBox.BackColor = color;
			txtControlBox.Font = font;
			if (sAlignment == "Left")
			{
				txtControlBox.TextAlign = HorizontalAlignment.Left;
			}
			else if (sAlignment == "Right")
			{
				txtControlBox.TextAlign = HorizontalAlignment.Right;
			}
			else
			{
				txtControlBox.TextAlign = HorizontalAlignment.Center;
			}
			txtControlBox.Location = new Point(X, Y);
			txtControlBox.MouseDown += delegate(object sndr, MouseEventArgs eArg)
			{
				mouseClickedOnCstmTBox = true;
				if (eArg.Button == MouseButtons.Left)
				{
					PanelMouseDownLocation = eArg.Location;
				}
			};
			txtControlBox.MouseUp += delegate
			{
				mouseClickedOnCstmTBox = false;
			};
			txtControlBox.MouseMove += txtControlBox_MouseMove;
			txtControlBox.Click += txtControlBox_Click;
			txtControlBox.TextChanged += delegate(object sndr, EventArgs eArg)
			{
				txtText.Text = ((Control)sndr).Text;
			};
			txtControlBox.DoubleClick += txtControlBox_DoubleClick;
			txtControlBox.KeyDown += TxtControlBox_KeyDown;
			if (!pnlWorkSpace.Controls.Contains(txtControlBox))
			{
				pnlWorkSpace.Controls.Add(txtControlBox);
			}
		}

		private void TxtControlBox_KeyDown(object sender, KeyEventArgs e)
		{
			isEditable = false;
			btnEdit.BackColor = Settings.Default.BackColor;
			int num = 4;
			ctrl = (Control)sender;
			if (e.KeyCode == Keys.Up)
			{
				ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - num);
			}
			else if (e.KeyCode == Keys.Down)
			{
				ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + num);
			}
			else if (e.KeyCode == Keys.Left)
			{
				ctrl.Location = new Point(ctrl.Location.X - num, ctrl.Location.Y);
			}
			else if (e.KeyCode == Keys.Right)
			{
				ctrl.Location = new Point(ctrl.Location.X + num, ctrl.Location.Y);
			}
			DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
			int num4 = (dotmatrixSettings.X = (txtX.Data = ctrl.Location.X));
			num4 = (dotmatrixSettings.Y = (txtY.Data = ctrl.Location.Y));
		}

		private void txtControlBox_TextChanged(object sender, EventArgs e)
		{
			ctrl = (Control)sender;
			txtText.Text = ctrl.Text;
		}

		public string PrintLine(string Length)
		{
			double value = Convert.ToDouble(Length);
			int num = Convert.ToInt32(value) / 10;
			string text = "  ";
			for (int i = 1; i <= num; i++)
			{
				text += " --";
			}
			return text;
		}

		private void PrintBill(bool isPrint = true)
		{
			entDMaster = DotmatrixMasterMethod();
			PrintHelper printHelper = new PrintHelper();
			entcompany = AshproCommon.GetObjectDetails<ExCompany>("select * from Institute");
			string strID = null;
			AshproCommon.IdFindMethod("AccountGroup", "AGroupId", "GroupName", "CUSTOMER/SUNDRY DEBTORS", out strID);
			AccountEntity accountEntity = new AccountEntity();
			accountEntity = AshproCommon.GetObjectDetails<AccountEntity>("select * from Account Where AGroupId='" + strID + "'");
			entPartyDtl = POSHelper.GetParty(accountEntity);
			entSMaster = AshproCommon.GetObjectDetails<SalesMaster>("select * from SalesEstimateMaster Where InvType='Local'");
			entTMaster = POSHelper.GetTMaster(entSMaster);
			entSDetails = AshproCommon.GetListMethod<SalesDetails>("select * from SalesEstimateSettings where SalesInvId = '" + 211 + "'");
			entTransactions = POSHelper.GetTransactionsFromSDetails(entSDetails, db);
			printHelper.PrintMethod(entDMaster, entDSettings, entcompany, entTMaster, entPartyDtl, entTransactions, entTMaster, null, null, isPrint);
			printHelper = null;
		}

		private DotmatrixMaster DotmatrixMasterMethod()
		{
			return new DotmatrixMaster
			{
				printMasterId = iMasterID,
				VochrType = cmbVoucher.Text,
				FormType = cmbFormType.Text,
				PrintType = cmbPrintType.Text,
				DesignName = cmbDesignName.Text,
				isDefault = cbDefault.Checked,
				isLandScape = false,
				PageWidth = txtPageWidth.Data,
				PageHieght = txtPageHeight.Data,
				SpaceBtwnLines = txtSpaceBtwnLines.Data,
				upScroll = txtUpScroll.Data,
				downScroll = txtDownScoll.Data,
				LinesPerPage = txtLinesPerPage.Data,
				Riverse = txtReverse.Text,
				Pitch = txtPitch.Text,
				Bold_On = txtBoldOn.Text,
				Bold_Off = txtBoldOff.Text,
				PrinterName = cmbPrinter.Text,
				BackgroundImgPath = txtBackgroundImage.Text,
				SpaceInLines = txtSpaceInLines.Data
			};
		}

		private void btnFont_Click(object sender, EventArgs e)
		{
			if (!isEditable)
			{
				return;
			}
			foreach (Control control in pnlWorkSpace.Controls)
			{
				if (!(control.Name == txtName.Text) || !(control.Tag.ToString() == cmbPosition.Text))
				{
					continue;
				}
				Font font = new Font("Arial", 10f, FontStyle.Regular);
				DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
				if (dotmatrixSettings != null)
				{
					font = ((!dotmatrixSettings.isBold) ? new Font(dotmatrixSettings.Font, dotmatrixSettings.FontSize, FontStyle.Regular) : new Font(dotmatrixSettings.Font, dotmatrixSettings.FontSize, FontStyle.Bold));
					FontDialog fontDialog = new FontDialog();
					fontDialog.Font = font;
					if (fontDialog.ShowDialog() == DialogResult.OK)
					{
						font = (control.Font = fontDialog.Font);
					}
					dotmatrixSettings.Font = font.FontFamily.Name.ToString();
					decimal d = font.Size.ToString().ToDecimal();
					dotmatrixSettings.FontSize = Math.Round(d).ToInt32();
					dotmatrixSettings.isBold = font.Style == FontStyle.Bold;
				}
			}
		}

		private void txtPageWidth_TextChanged(object sender, EventArgs e)
		{
			if (txtPageWidth.Text != "0" && txtPageHeight.Text != "0" && txtPageHeight.Text != string.Empty && txtPageWidth.Text != string.Empty)
			{
				pnlWorkSpace.Size = new Size(txtPageWidth.Data, txtPageHeight.Data);
			}
		}

		private void btnXMLSave_Click(object sender, EventArgs e)
		{
			if (entDSettings.Count == 0)
			{
				Messages.ErrorMessage("Design Not Completed");
				return;
			}
			entDSettings = entDSettings.OrderBy((DotmatrixSettings x) => x.TabIndex).ToList();
			string text = null;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Application.StartupPath;
			saveFileDialog.Title = "Save Printer Design";
			saveFileDialog.DefaultExt = "xml";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				text = saveFileDialog.FileName;
			}
			DotmatrixFormatClass dotmatrixFormatClass = new DotmatrixFormatClass();
			dotmatrixFormatClass._dotmatrixMaster = DotmatrixMasterMethod();
			dotmatrixFormatClass._dotmatrixSettings = entDSettings;
			string path = text;
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DotmatrixFormatClass));
			using (TextWriter textWriter = new StreamWriter(path))
			{
				xmlSerializer.Serialize(textWriter, dotmatrixFormatClass);
				textWriter.Close();
			}
			Messages.InformationMessage("Successfully Exported the Printer Design to XML");
		}

		private void btnOpenXML_Click(object sender, EventArgs e)
		{
			entDMaster = new DotmatrixMaster();
			entDSettings = new List<DotmatrixSettings>();
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
			if (!File.Exists(path))
			{
				return;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DotmatrixFormatClass));
			xmlSerializer.UnknownNode += Serializer_UnknownNode;
			xmlSerializer.UnknownAttribute += Serializer_UnknownAttribute;
			DotmatrixFormatClass dotmatrixFormatClass;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				dotmatrixFormatClass = (DotmatrixFormatClass)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
			}
			entDMaster = null;
			entDSettings.Clear();
			entDSettings = dotmatrixFormatClass._dotmatrixSettings.OrderBy((DotmatrixSettings x) => x.TabIndex).ToList();
			entDMaster = dotmatrixFormatClass._dotmatrixMaster;
			cbDefault.Checked = entDMaster.isDefault;
			txtPageWidth.Text = entDMaster.PageWidth.ToString();
			txtPageHeight.Text = entDMaster.PageHieght.ToString();
			txtSpaceInLines.Text = entDMaster.SpaceInLines.ToString();
			txtSpaceBtwnLines.Text = entDMaster.SpaceBtwnLines.ToString();
			txtUpScroll.Text = entDMaster.upScroll.ToString();
			txtDownScoll.Text = entDMaster.downScroll.ToString();
			txtLinesPerPage.Text = entDMaster.LinesPerPage.ToString();
			txtReverse.Text = entDMaster.Riverse;
			txtPitch.Text = entDMaster.Pitch;
			txtBoldOn.Text = entDMaster.Bold_On;
			txtBoldOff.Text = entDMaster.Bold_Off;
			cmbPrinter.Text = entDMaster.PrinterName;
			if (entDMaster.BackgroundImgPath != string.Empty)
			{
				string text = entDMaster.BackgroundImgPath;
				if (!File.Exists(text))
				{
					text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Form\\Form.png";
				}
				txtBackgroundImage.Text = text;
				pnlWorkSpace.BackgroundImage = Image.FromFile(text);
				pnlWorkSpace.BackgroundImageLayout = ImageLayout.Stretch;
			}
			foreach (DotmatrixSettings item in entDSettings.Distinct())
			{
				if (item.Type == "LabelControl")
				{
					AddMethodTypeWise(item.FeildText, item.ControleName, item.width, item.Height, Color.Aqua, item.X, item.Y, item.Alaingmnet, item.FontSize, item.Font, item.isBold, item.Position);
				}
				else if (item.Type == "ValueControle")
				{
					AddMethodTypeWise(item.FeildText, item.ControleName, item.width, item.Height, Color.Pink, item.X, item.Y, item.Alaingmnet, item.FontSize, item.Font, item.isBold, item.Position);
				}
				else if (item.Type == "LineLabelControl")
				{
					AddMethodTypeWise(item.FeildText, item.ControleName, item.width, item.Height, Color.Gold, item.X, item.Y, item.Alaingmnet, item.FontSize, item.Font, item.isBold, item.Position);
				}
				else if (item.Type == "SpaceControl")
				{
					AddMethodTypeWise(item.FeildText, item.ControleName, item.width, item.Height, Color.LightBlue, item.X, item.Y, item.Alaingmnet, item.FontSize, item.Font, item.isBold, item.Position);
				}
				else
				{
					AddMethodTypeWise(item.FeildText, item.ControleName, item.width, item.Height, Color.Orchid, item.X, item.Y, item.Alaingmnet, item.FontSize, item.Font, item.isBold, item.Position);
				}
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

		private void cmbControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAdd_Click(null, null);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (cmbControl.Text != string.Empty)
			{
				isEditable = false;
				btnEdit.BackColor = Settings.Default.BackColor;
				intY = 600;
				string text = null;
				DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
				text = (dotmatrixSettings.ControleName = ((cmbControlType.Text == "LabelControl") ? (cmbControl.Text + "_lbl") : ((!(cmbControlType.Text == "ValueControle")) ? cmbControl.Text : (cmbControl.Text + "_val"))));
				dotmatrixSettings.Type = cmbControlType.Text;
				if (cmbControlType.Text == "SpaceControl")
				{
					dotmatrixSettings.width = 50;
				}
				else if (cmbControlType.Text == "LabelControl")
				{
					dotmatrixSettings.width = 100;
				}
				else if (cmbControlType.Text == "LineLabelControl")
				{
					dotmatrixSettings.width = 700;
				}
				else if (cmbControlType.Text == "ValueControle")
				{
					dotmatrixSettings.width = 100;
				}
				else
				{
					dotmatrixSettings.width = 50;
				}
				dotmatrixSettings.isBold = false;
				dotmatrixSettings.Alaingmnet = "Left";
				dotmatrixSettings.iIndex = pnlWorkSpace.Controls.Count;
				dotmatrixSettings.FeildText = cmbControl.Text;
				dotmatrixSettings.Height = 20;
				dotmatrixSettings.FontSize = 10;
				dotmatrixSettings.Font = "Microsoft Sans Serif";
				dotmatrixSettings.Position = cmbControlPosition.Text;
				dotmatrixSettings.X = 0;
				dotmatrixSettings.Y = intY;
				iTabindex = pnlWorkSpace.Controls.Count;
				dotmatrixSettings.TabIndex = iTabindex;
				dotmatrixSettings.Spec = txtSpec.Text;
				entDSettings.Add(dotmatrixSettings);
				AddMethod(cmbControl.Text, text, cmbControlPosition.Text);
				txtName.Text = dotmatrixSettings.ControleName;
				txtText.Text = dotmatrixSettings.FeildText;
				cmbType.Text = dotmatrixSettings.Type;
				txtX.Data = dotmatrixSettings.X;
				txtY.Data = dotmatrixSettings.Y;
				txtHeight.Data = dotmatrixSettings.Height;
				cmbAlgmnt.Text = dotmatrixSettings.Alaingmnet;
				cmbControlPosition.Text = dotmatrixSettings.Position;
				txtWidth.Data = dotmatrixSettings.width;
				cmbControlType_SelectedIndexChanged_1(null, null);
				cmbControlPosition.Focus();
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			isEditable = false;
			btnEdit.BackColor = Settings.Default.BackColor;
			mouseClickedOnCstmTBox = false;
			DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
			if (dotmatrixSettings == null)
			{
				return;
			}
			entDSettings.Remove(dotmatrixSettings);
			foreach (Control control in pnlWorkSpace.Controls)
			{
				if (control.Name == txtName.Text && control.Tag.ToString() == cmbPosition.Text)
				{
					pnlWorkSpace.Controls.Remove(control);
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("PrintDesignSettings", "printMasterId", iMasterID.ToInt32()) && AshproCommon.DeleteFromDatabase("PrintDesignMaster", "printMasterId", iMasterID.ToInt32()))
				{
					Messages.DeleteMessage();
					btnClear_Click_1(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message + " 687");
			}
		}

		private void btnDeleteImage_Click(object sender, EventArgs e)
		{
			txtBackgroundImage.Text = string.Empty;
			pnlWorkSpace.BackgroundImage = null;
		}

		private void cmbControlPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmbControlType.Items.Clear();
			if (cmbPrintType.SelectedIndex == 1)
			{
				if (cmbControlPosition.SelectedIndex == 3)
				{
					string[] array = new string[4] { "SpaceControl", "LabelControl", "LineLabelControl", "BrakeControle" };
					ComboBox.ObjectCollection items = cmbControlType.Items;
					object[] items2 = array;
					items.AddRange(items2);
				}
				else if (cmbControlPosition.SelectedIndex == 4)
				{
					string[] array2 = new string[2] { "ValueControle", "BrakeControle" };
					ComboBox.ObjectCollection items3 = cmbControlType.Items;
					object[] items2 = array2;
					items3.AddRange(items2);
				}
				else
				{
					string[] array3 = new string[5] { "SpaceControl", "LabelControl", "LineLabelControl", "ValueControle", "BrakeControle" };
					ComboBox.ObjectCollection items4 = cmbControlType.Items;
					object[] items2 = array3;
					items4.AddRange(items2);
				}
			}
			else if (cmbControlPosition.SelectedIndex == 3)
			{
				string[] array4 = new string[2] { "LabelControl", "LineLabelControl" };
				ComboBox.ObjectCollection items5 = cmbControlType.Items;
				object[] items2 = array4;
				items5.AddRange(items2);
			}
			else if (cmbControlPosition.SelectedIndex != 4 && cmbControlPosition.SelectedIndex != 6)
			{
				string[] array5 = new string[3] { "LabelControl", "LineLabelControl", "ValueControle" };
				ComboBox.ObjectCollection items6 = cmbControlType.Items;
				object[] items2 = array5;
				items6.AddRange(items2);
			}
			else
			{
				cmbControlType.Items.Add("ValueControle");
			}
		}

		private void txtWidth_Validated(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in pnlWorkSpace.Controls)
				{
					if (control.Name == txtName.Text && control.Tag.ToString() == cmbPosition.Text && isEditable)
					{
						control.Size = new Size(txtWidth.Data, txtHeight.Data);
						DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
						if (dotmatrixSettings != null)
						{
							dotmatrixSettings.width = txtWidth.Data;
							dotmatrixSettings.Height = txtHeight.Data;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtSpec_Validated(object sender, EventArgs e)
		{
			DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
			if (dotmatrixSettings != null && isEditable)
			{
				dotmatrixSettings.Spec = txtSpec.Text;
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (isEditable)
			{
				isEditable = false;
				btnEdit.BackColor = Settings.Default.BackColor;
				DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
				if (dotmatrixSettings != null)
				{
					dotmatrixSettings.X = txtX.Data;
					dotmatrixSettings.Y = txtY.Data;
				}
			}
			else
			{
				isEditable = true;
				btnEdit.BackColor = Color.Red;
			}
		}

		private void txtX_TextChanged(object sender, EventArgs e)
		{
			try
			{
				foreach (Control control in pnlWorkSpace.Controls)
				{
					if (control.Name == txtName.Text && control.Tag.ToString() == cmbPosition.Text && isEditable)
					{
						control.Location = new Point(txtX.Data, txtY.Data);
						DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text).FirstOrDefault();
						if (dotmatrixSettings != null)
						{
							dotmatrixSettings.X = txtY.Data;
							dotmatrixSettings.Y = txtY.Data;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void ReloadMethod()
		{
			btnClear_Click_1(null, null);
			entDMaster = new DotmatrixMaster();
			entDMaster = db.DotmatrixMasters(new
			{
				DesignName = cmbDesignName.Text,
				VochrType = cmbVoucher.Text,
				FormType = cmbFormType.Text,
				PrintType = cmbPrintType.Text
			}).FirstOrDefault();
			if (entDMaster == null)
			{
				return;
			}
			iMasterID = entDMaster.printMasterId.ToInt32();
			entDSettings = new List<DotmatrixSettings>();
			entDSettings = db.DotmatrixSettings(new
			{
				printMasterId = iMasterID
			}).ToList();
			cbDefault.Checked = entDMaster.isDefault;
			txtPageWidth.Text = entDMaster.PageWidth.ToString();
			txtPageHeight.Text = entDMaster.PageHieght.ToString();
			txtSpaceInLines.Text = entDMaster.SpaceInLines.ToString();
			txtUpScroll.Text = entDMaster.upScroll.ToString();
			txtDownScoll.Text = entDMaster.downScroll.ToString();
			txtLinesPerPage.Text = entDMaster.LinesPerPage.ToString();
			txtReverse.Text = entDMaster.Riverse;
			txtPitch.Text = entDMaster.Pitch;
			txtBoldOn.Text = entDMaster.Bold_On;
			txtBoldOff.Text = entDMaster.Bold_Off;
			cmbPrinter.Text = entDMaster.PrinterName;
			txtSpaceBtwnLines.Text = entDMaster.SpaceBtwnLines.ToString();
			if (entDMaster.BackgroundImgPath != string.Empty)
			{
				string text = entDMaster.BackgroundImgPath;
				if (!File.Exists(text))
				{
					text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Form\\Form.png";
				}
				txtBackgroundImage.Text = text;
				pnlWorkSpace.BackgroundImage = Image.FromFile(text);
				pnlWorkSpace.BackgroundImageLayout = ImageLayout.Stretch;
			}
			foreach (DotmatrixSettings entDSetting in entDSettings)
			{
				if (entDSetting.Type == "LabelControl")
				{
					AddMethodTypeWise(entDSetting.FeildText, entDSetting.ControleName, entDSetting.width, entDSetting.Height, Color.Aqua, entDSetting.X, entDSetting.Y, entDSetting.Alaingmnet, entDSetting.FontSize, entDSetting.Font, entDSetting.isBold, entDSetting.Position);
				}
				else if (entDSetting.Type == "ValueControle")
				{
					AddMethodTypeWise(entDSetting.FeildText, entDSetting.ControleName, entDSetting.width, entDSetting.Height, Color.Pink, entDSetting.X, entDSetting.Y, entDSetting.Alaingmnet, entDSetting.FontSize, entDSetting.Font, entDSetting.isBold, entDSetting.Position);
				}
				else if (entDSetting.Type == "LineLabelControl")
				{
					AddMethodTypeWise(entDSetting.FeildText, entDSetting.ControleName, entDSetting.width, entDSetting.Height, Color.Gold, entDSetting.X, entDSetting.Y, entDSetting.Alaingmnet, entDSetting.FontSize, entDSetting.Font, entDSetting.isBold, entDSetting.Position);
				}
				else if (entDSetting.Type == "SpaceControl")
				{
					AddMethodTypeWise(entDSetting.FeildText, entDSetting.ControleName, entDSetting.width, entDSetting.Height, Color.LightBlue, entDSetting.X, entDSetting.Y, entDSetting.Alaingmnet, entDSetting.FontSize, entDSetting.Font, entDSetting.isBold, entDSetting.Position);
				}
				else
				{
					AddMethodTypeWise(entDSetting.FeildText, entDSetting.ControleName, entDSetting.width, entDSetting.Height, Color.Orchid, entDSetting.X, entDSetting.Y, entDSetting.Alaingmnet, entDSetting.FontSize, entDSetting.Font, entDSetting.isBold, entDSetting.Position);
				}
			}
			btnDelete.Enabled = true;
		}

		private void cmbAlgmnt_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (Control control in pnlWorkSpace.Controls)
			{
				if (control.Name == txtName.Text && control.Tag.ToString() == cmbPosition.Text && isEditable && control is TextBox)
				{
					TextBox textBox = (TextBox)control;
					if (cmbAlgmnt.SelectedIndex == 0)
					{
						textBox.TextAlign = HorizontalAlignment.Left;
					}
					else if (cmbAlgmnt.SelectedIndex == 1)
					{
						textBox.TextAlign = HorizontalAlignment.Right;
					}
					else
					{
						textBox.TextAlign = HorizontalAlignment.Center;
					}
					DotmatrixSettings dotmatrixSettings = entDSettings.Where((DotmatrixSettings x) => x.ControleName == txtName.Text && x.Position == cmbPosition.Text && x.Type == cmbType.Text).FirstOrDefault();
					if (dotmatrixSettings != null)
					{
						dotmatrixSettings.Alaingmnet = cmbAlgmnt.Text;
					}
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintDesignView));
            this.pnlWorkSpace = new System.Windows.Forms.Panel();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPage = new System.Windows.Forms.TabPage();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.cbDefault = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbPrinter = new AshUsercontrol.AshComboBox();
            this.txtBackgroundImage = new AshUsercontrol.AshTextBoxNormal();
            this.btnOpenXML = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnXMLSave = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtBoldOff = new AshUsercontrol.AshTextBoxNormal();
            this.txtBoldOn = new AshUsercontrol.AshTextBoxNormal();
            this.txtPitch = new AshUsercontrol.AshTextBoxNormal();
            this.txtReverse = new AshUsercontrol.AshTextBoxNormal();
            this.txtLinesPerPage = new AshUsercontrol.AshTextBoxNormal();
            this.txtDownScoll = new AshUsercontrol.AshTextBoxNormal();
            this.txtUpScroll = new AshUsercontrol.AshTextBoxNormal();
            this.txtSpaceBtwnLines = new AshUsercontrol.AshTextBoxNormal();
            this.txtSpaceInLines = new AshUsercontrol.AshTextBoxNormal();
            this.txtPageHeight = new AshUsercontrol.AshTextBoxNormal();
            this.txtPageWidth = new AshUsercontrol.AshTextBoxNormal();
            this.cmbDesignName = new AshUsercontrol.AshComboBox();
            this.cmbFormType = new AshUsercontrol.AshComboBox();
            this.cmbVoucher = new AshUsercontrol.AshComboBox();
            this.cmbPrintType = new AshUsercontrol.AshComboBox();
            this.lblLinePerPage = new System.Windows.Forms.Label();
            this.lblDown = new System.Windows.Forms.Label();
            this.lblPitch = new System.Windows.Forms.Label();
            this.lblBoldOff = new System.Windows.Forms.Label();
            this.lblBoldOn = new System.Windows.Forms.Label();
            this.lblReverse = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.lblPrinters = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPageWidth = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.cmbControl = new AshUsercontrol.AshComboBox();
            this.cmbControlType = new AshUsercontrol.AshComboBox();
            this.cmbControlPosition = new AshUsercontrol.AshComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtFontSize = new AshUsercontrol.AshTextBoxNormal();
            this.txtTabindex = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cbBold = new System.Windows.Forms.CheckBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtSpec = new AshUsercontrol.AshTextBoxNormal();
            this.txtY = new AshUsercontrol.AshTextBoxNormal();
            this.txtX = new AshUsercontrol.AshTextBoxNormal();
            this.txtHeight = new AshUsercontrol.AshTextBoxNormal();
            this.txtWidth = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.txtText = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbType = new AshUsercontrol.AshComboBox();
            this.cmbPosition = new AshUsercontrol.AshComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbAlgmnt = new AshUsercontrol.AshComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabSettings.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(642, 0);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            // 
            // pnlWorkSpace
            // 
            this.pnlWorkSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWorkSpace.Location = new System.Drawing.Point(270, 38);
            this.pnlWorkSpace.Name = "pnlWorkSpace";
            this.pnlWorkSpace.Size = new System.Drawing.Size(666, 718);
            this.pnlWorkSpace.TabIndex = 3;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPage);
            this.tabSettings.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSettings.ItemSize = new System.Drawing.Size(260, 21);
            this.tabSettings.Location = new System.Drawing.Point(6, 45);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(258, 575);
            this.tabSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSettings.TabIndex = 1;
            this.tabSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabSettings_MouseDown);
            this.tabSettings.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabSettings_MouseMove);
            // 
            // tabPage
            // 
            this.tabPage.BackColor = System.Drawing.Color.White;
            this.tabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage.Controls.Add(this.btnDeleteImage);
            this.tabPage.Controls.Add(this.btnBrowseImage);
            this.tabPage.Controls.Add(this.cbDefault);
            this.tabPage.Controls.Add(this.btnClear);
            this.tabPage.Controls.Add(this.cmbPrinter);
            this.tabPage.Controls.Add(this.txtBackgroundImage);
            this.tabPage.Controls.Add(this.btnOpenXML);
            this.tabPage.Controls.Add(this.btnDelete);
            this.tabPage.Controls.Add(this.btnXMLSave);
            this.tabPage.Controls.Add(this.btnSave);
            this.tabPage.Controls.Add(this.txtBoldOff);
            this.tabPage.Controls.Add(this.txtBoldOn);
            this.tabPage.Controls.Add(this.txtPitch);
            this.tabPage.Controls.Add(this.txtReverse);
            this.tabPage.Controls.Add(this.txtLinesPerPage);
            this.tabPage.Controls.Add(this.txtDownScoll);
            this.tabPage.Controls.Add(this.txtUpScroll);
            this.tabPage.Controls.Add(this.txtSpaceBtwnLines);
            this.tabPage.Controls.Add(this.txtSpaceInLines);
            this.tabPage.Controls.Add(this.txtPageHeight);
            this.tabPage.Controls.Add(this.txtPageWidth);
            this.tabPage.Controls.Add(this.cmbDesignName);
            this.tabPage.Controls.Add(this.cmbFormType);
            this.tabPage.Controls.Add(this.cmbVoucher);
            this.tabPage.Controls.Add(this.cmbPrintType);
            this.tabPage.Controls.Add(this.lblLinePerPage);
            this.tabPage.Controls.Add(this.lblDown);
            this.tabPage.Controls.Add(this.lblPitch);
            this.tabPage.Controls.Add(this.lblBoldOff);
            this.tabPage.Controls.Add(this.lblBoldOn);
            this.tabPage.Controls.Add(this.lblReverse);
            this.tabPage.Controls.Add(this.lblFilePath);
            this.tabPage.Controls.Add(this.lblUp);
            this.tabPage.Controls.Add(this.lblPrinters);
            this.tabPage.Controls.Add(this.label3);
            this.tabPage.Controls.Add(this.label21);
            this.tabPage.Controls.Add(this.label5);
            this.tabPage.Controls.Add(this.label2);
            this.tabPage.Controls.Add(this.label4);
            this.tabPage.Controls.Add(this.label1);
            this.tabPage.Controls.Add(this.lblPageWidth);
            this.tabPage.Location = new System.Drawing.Point(4, 25);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage.Size = new System.Drawing.Size(250, 546);
            this.tabPage.TabIndex = 0;
            this.tabPage.Text = "Header Properties";
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Location = new System.Drawing.Point(228, 414);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(18, 23);
            this.btnDeleteImage.TabIndex = 31;
            this.btnDeleteImage.Text = "...";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Location = new System.Drawing.Point(206, 414);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(18, 23);
            this.btnBrowseImage.TabIndex = 31;
            this.btnBrowseImage.Text = "...";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // cbDefault
            // 
            this.cbDefault.AutoSize = true;
            this.cbDefault.Location = new System.Drawing.Point(120, 120);
            this.cbDefault.Name = "cbDefault";
            this.cbDefault.Size = new System.Drawing.Size(74, 20);
            this.cbDefault.TabIndex = 6;
            this.cbDefault.Text = "Default";
            this.cbDefault.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(193, 445);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(53, 30);
            this.btnClear.TabIndex = 34;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.LeaveColor = System.Drawing.Color.White;
            this.cmbPrinter.Location = new System.Drawing.Point(120, 387);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(121, 24);
            this.cmbPrinter.TabIndex = 28;
            // 
            // txtBackgroundImage
            // 
            this.txtBackgroundImage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBackgroundImage.Data = 0;
            this.txtBackgroundImage.EnterColor = System.Drawing.Color.LightPink;
            this.txtBackgroundImage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackgroundImage.Format = null;
            this.txtBackgroundImage.isAllowNegative = false;
            this.txtBackgroundImage.isAllowSpecialChar = false;
            this.txtBackgroundImage.isNumberOnly = false;
            this.txtBackgroundImage.isNumeric = false;
            this.txtBackgroundImage.LeaveColor = System.Drawing.Color.White;
            this.txtBackgroundImage.Location = new System.Drawing.Point(120, 418);
            this.txtBackgroundImage.Margin = new System.Windows.Forms.Padding(4);
            this.txtBackgroundImage.Name = "txtBackgroundImage";
            this.txtBackgroundImage.Size = new System.Drawing.Size(83, 16);
            this.txtBackgroundImage.TabIndex = 30;
            this.txtBackgroundImage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBackgroundImage.WaterMark = null;
            // 
            // btnOpenXML
            // 
            this.btnOpenXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnOpenXML.FlatAppearance.BorderSize = 0;
            this.btnOpenXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOpenXML.ForeColor = System.Drawing.Color.White;
            this.btnOpenXML.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenXML.Image")));
            this.btnOpenXML.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenXML.Location = new System.Drawing.Point(132, 491);
            this.btnOpenXML.Name = "btnOpenXML";
            this.btnOpenXML.Size = new System.Drawing.Size(110, 30);
            this.btnOpenXML.TabIndex = 33;
            this.btnOpenXML.Text = "Open XML";
            this.btnOpenXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenXML.UseVisualStyleBackColor = false;
            this.btnOpenXML.Click += new System.EventHandler(this.btnOpenXML_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(105, 445);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 30);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = " &Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnXMLSave
            // 
            this.btnXMLSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnXMLSave.FlatAppearance.BorderSize = 0;
            this.btnXMLSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXMLSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnXMLSave.ForeColor = System.Drawing.Color.White;
            this.btnXMLSave.Image = ((System.Drawing.Image)(resources.GetObject("btnXMLSave.Image")));
            this.btnXMLSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnXMLSave.Location = new System.Drawing.Point(12, 491);
            this.btnXMLSave.Name = "btnXMLSave";
            this.btnXMLSave.Size = new System.Drawing.Size(117, 30);
            this.btnXMLSave.TabIndex = 32;
            this.btnXMLSave.Text = " XML Export";
            this.btnXMLSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXMLSave.UseVisualStyleBackColor = false;
            this.btnXMLSave.Click += new System.EventHandler(this.btnXMLSave_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(12, 445);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 30);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // txtBoldOff
            // 
            this.txtBoldOff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoldOff.Data = 0;
            this.txtBoldOff.EnterColor = System.Drawing.Color.LightPink;
            this.txtBoldOff.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoldOff.Format = null;
            this.txtBoldOff.isAllowNegative = false;
            this.txtBoldOff.isAllowSpecialChar = false;
            this.txtBoldOff.isNumberOnly = false;
            this.txtBoldOff.isNumeric = false;
            this.txtBoldOff.LeaveColor = System.Drawing.Color.White;
            this.txtBoldOff.Location = new System.Drawing.Point(120, 366);
            this.txtBoldOff.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoldOff.Name = "txtBoldOff";
            this.txtBoldOff.Size = new System.Drawing.Size(121, 16);
            this.txtBoldOff.TabIndex = 26;
            this.txtBoldOff.Text = "F";
            this.txtBoldOff.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBoldOff.WaterMark = null;
            // 
            // txtBoldOn
            // 
            this.txtBoldOn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoldOn.Data = 0;
            this.txtBoldOn.EnterColor = System.Drawing.Color.LightPink;
            this.txtBoldOn.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoldOn.Format = null;
            this.txtBoldOn.isAllowNegative = false;
            this.txtBoldOn.isAllowSpecialChar = false;
            this.txtBoldOn.isNumberOnly = false;
            this.txtBoldOn.isNumeric = false;
            this.txtBoldOn.LeaveColor = System.Drawing.Color.White;
            this.txtBoldOn.Location = new System.Drawing.Point(120, 342);
            this.txtBoldOn.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoldOn.Name = "txtBoldOn";
            this.txtBoldOn.Size = new System.Drawing.Size(121, 16);
            this.txtBoldOn.TabIndex = 24;
            this.txtBoldOn.Text = "E";
            this.txtBoldOn.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBoldOn.WaterMark = null;
            // 
            // txtPitch
            // 
            this.txtPitch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPitch.Data = 0;
            this.txtPitch.EnterColor = System.Drawing.Color.LightPink;
            this.txtPitch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPitch.Format = null;
            this.txtPitch.isAllowNegative = false;
            this.txtPitch.isAllowSpecialChar = false;
            this.txtPitch.isNumberOnly = false;
            this.txtPitch.isNumeric = false;
            this.txtPitch.LeaveColor = System.Drawing.Color.White;
            this.txtPitch.Location = new System.Drawing.Point(120, 318);
            this.txtPitch.Margin = new System.Windows.Forms.Padding(4);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(121, 16);
            this.txtPitch.TabIndex = 22;
            this.txtPitch.Text = "P";
            this.txtPitch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPitch.WaterMark = null;
            // 
            // txtReverse
            // 
            this.txtReverse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReverse.Data = 1;
            this.txtReverse.EnterColor = System.Drawing.Color.LightPink;
            this.txtReverse.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReverse.Format = null;
            this.txtReverse.isAllowNegative = false;
            this.txtReverse.isAllowSpecialChar = false;
            this.txtReverse.isNumberOnly = false;
            this.txtReverse.isNumeric = false;
            this.txtReverse.LeaveColor = System.Drawing.Color.White;
            this.txtReverse.Location = new System.Drawing.Point(120, 294);
            this.txtReverse.Margin = new System.Windows.Forms.Padding(4);
            this.txtReverse.Name = "txtReverse";
            this.txtReverse.Size = new System.Drawing.Size(121, 16);
            this.txtReverse.TabIndex = 20;
            this.txtReverse.Text = "j1";
            this.txtReverse.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtReverse.WaterMark = null;
            // 
            // txtLinesPerPage
            // 
            this.txtLinesPerPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLinesPerPage.Data = 40;
            this.txtLinesPerPage.EnterColor = System.Drawing.Color.LightPink;
            this.txtLinesPerPage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinesPerPage.Format = null;
            this.txtLinesPerPage.isAllowNegative = false;
            this.txtLinesPerPage.isAllowSpecialChar = false;
            this.txtLinesPerPage.isNumberOnly = true;
            this.txtLinesPerPage.isNumeric = false;
            this.txtLinesPerPage.LeaveColor = System.Drawing.Color.White;
            this.txtLinesPerPage.Location = new System.Drawing.Point(120, 270);
            this.txtLinesPerPage.Margin = new System.Windows.Forms.Padding(4);
            this.txtLinesPerPage.Name = "txtLinesPerPage";
            this.txtLinesPerPage.Size = new System.Drawing.Size(121, 16);
            this.txtLinesPerPage.TabIndex = 18;
            this.txtLinesPerPage.Text = "40";
            this.txtLinesPerPage.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtLinesPerPage.WaterMark = null;
            // 
            // txtDownScoll
            // 
            this.txtDownScoll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDownScoll.Data = 0;
            this.txtDownScoll.EnterColor = System.Drawing.Color.LightPink;
            this.txtDownScoll.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownScoll.Format = null;
            this.txtDownScoll.isAllowNegative = false;
            this.txtDownScoll.isAllowSpecialChar = false;
            this.txtDownScoll.isNumberOnly = true;
            this.txtDownScoll.isNumeric = false;
            this.txtDownScoll.LeaveColor = System.Drawing.Color.White;
            this.txtDownScoll.Location = new System.Drawing.Point(120, 246);
            this.txtDownScoll.Margin = new System.Windows.Forms.Padding(4);
            this.txtDownScoll.Name = "txtDownScoll";
            this.txtDownScoll.Size = new System.Drawing.Size(121, 16);
            this.txtDownScoll.TabIndex = 16;
            this.txtDownScoll.Text = "0";
            this.txtDownScoll.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDownScoll.WaterMark = null;
            // 
            // txtUpScroll
            // 
            this.txtUpScroll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUpScroll.Data = 0;
            this.txtUpScroll.EnterColor = System.Drawing.Color.LightPink;
            this.txtUpScroll.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpScroll.Format = null;
            this.txtUpScroll.isAllowNegative = false;
            this.txtUpScroll.isAllowSpecialChar = false;
            this.txtUpScroll.isNumberOnly = true;
            this.txtUpScroll.isNumeric = false;
            this.txtUpScroll.LeaveColor = System.Drawing.Color.White;
            this.txtUpScroll.Location = new System.Drawing.Point(120, 222);
            this.txtUpScroll.Margin = new System.Windows.Forms.Padding(4);
            this.txtUpScroll.Name = "txtUpScroll";
            this.txtUpScroll.Size = new System.Drawing.Size(121, 16);
            this.txtUpScroll.TabIndex = 14;
            this.txtUpScroll.Text = "0";
            this.txtUpScroll.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUpScroll.WaterMark = null;
            // 
            // txtSpaceBtwnLines
            // 
            this.txtSpaceBtwnLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpaceBtwnLines.Data = 10;
            this.txtSpaceBtwnLines.EnterColor = System.Drawing.Color.LightPink;
            this.txtSpaceBtwnLines.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpaceBtwnLines.Format = null;
            this.txtSpaceBtwnLines.isAllowNegative = false;
            this.txtSpaceBtwnLines.isAllowSpecialChar = false;
            this.txtSpaceBtwnLines.isNumberOnly = true;
            this.txtSpaceBtwnLines.isNumeric = false;
            this.txtSpaceBtwnLines.LeaveColor = System.Drawing.Color.White;
            this.txtSpaceBtwnLines.Location = new System.Drawing.Point(190, 195);
            this.txtSpaceBtwnLines.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpaceBtwnLines.Name = "txtSpaceBtwnLines";
            this.txtSpaceBtwnLines.Size = new System.Drawing.Size(43, 16);
            this.txtSpaceBtwnLines.TabIndex = 12;
            this.txtSpaceBtwnLines.Text = "10";
            this.txtSpaceBtwnLines.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtSpaceBtwnLines.WaterMark = null;
            // 
            // txtSpaceInLines
            // 
            this.txtSpaceInLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpaceInLines.Data = 15;
            this.txtSpaceInLines.EnterColor = System.Drawing.Color.LightPink;
            this.txtSpaceInLines.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpaceInLines.Format = null;
            this.txtSpaceInLines.isAllowNegative = false;
            this.txtSpaceInLines.isAllowSpecialChar = false;
            this.txtSpaceInLines.isNumberOnly = true;
            this.txtSpaceInLines.isNumeric = false;
            this.txtSpaceInLines.LeaveColor = System.Drawing.Color.White;
            this.txtSpaceInLines.Location = new System.Drawing.Point(120, 194);
            this.txtSpaceInLines.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpaceInLines.Name = "txtSpaceInLines";
            this.txtSpaceInLines.Size = new System.Drawing.Size(43, 16);
            this.txtSpaceInLines.TabIndex = 12;
            this.txtSpaceInLines.Text = "15";
            this.txtSpaceInLines.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.txtSpaceInLines.WaterMark = null;
            // 
            // txtPageHeight
            // 
            this.txtPageHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPageHeight.Data = 0;
            this.txtPageHeight.EnterColor = System.Drawing.Color.LightPink;
            this.txtPageHeight.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageHeight.Format = null;
            this.txtPageHeight.isAllowNegative = false;
            this.txtPageHeight.isAllowSpecialChar = false;
            this.txtPageHeight.isNumberOnly = true;
            this.txtPageHeight.isNumeric = false;
            this.txtPageHeight.LeaveColor = System.Drawing.Color.White;
            this.txtPageHeight.Location = new System.Drawing.Point(120, 170);
            this.txtPageHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.Size = new System.Drawing.Size(121, 16);
            this.txtPageHeight.TabIndex = 10;
            this.txtPageHeight.Text = "0";
            this.txtPageHeight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPageHeight.WaterMark = null;
            this.txtPageHeight.TextChanged += new System.EventHandler(this.txtPageWidth_TextChanged);
            // 
            // txtPageWidth
            // 
            this.txtPageWidth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPageWidth.Data = 0;
            this.txtPageWidth.EnterColor = System.Drawing.Color.LightPink;
            this.txtPageWidth.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageWidth.Format = null;
            this.txtPageWidth.isAllowNegative = false;
            this.txtPageWidth.isAllowSpecialChar = false;
            this.txtPageWidth.isNumberOnly = true;
            this.txtPageWidth.isNumeric = false;
            this.txtPageWidth.LeaveColor = System.Drawing.Color.White;
            this.txtPageWidth.Location = new System.Drawing.Point(120, 146);
            this.txtPageWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageWidth.Name = "txtPageWidth";
            this.txtPageWidth.Size = new System.Drawing.Size(121, 16);
            this.txtPageWidth.TabIndex = 8;
            this.txtPageWidth.Text = "0";
            this.txtPageWidth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPageWidth.WaterMark = null;
            this.txtPageWidth.TextChanged += new System.EventHandler(this.txtPageWidth_TextChanged);
            // 
            // cmbDesignName
            // 
            this.cmbDesignName.EnterColor = System.Drawing.Color.LightPink;
            this.cmbDesignName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDesignName.FormattingEnabled = true;
            this.cmbDesignName.LeaveColor = System.Drawing.Color.White;
            this.cmbDesignName.Location = new System.Drawing.Point(120, 90);
            this.cmbDesignName.Name = "cmbDesignName";
            this.cmbDesignName.Size = new System.Drawing.Size(121, 24);
            this.cmbDesignName.TabIndex = 5;
            this.cmbDesignName.SelectedIndexChanged += new System.EventHandler(this.cmbDesignName_SelectedIndexChanged);
            // 
            // cmbFormType
            // 
            this.cmbFormType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbFormType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFormType.FormattingEnabled = true;
            this.cmbFormType.Items.AddRange(new object[] {
            "Sales Invoice",
            "Sales Estimate",
            "Sales Order",
            "Sales Quotation",
            "Sales Return",
            "Purchase Invoice",
            "Purchase Return",
            "Purchase Order",
            "KOT"});
            this.cmbFormType.LeaveColor = System.Drawing.Color.White;
            this.cmbFormType.Location = new System.Drawing.Point(119, 33);
            this.cmbFormType.Name = "cmbFormType";
            this.cmbFormType.Size = new System.Drawing.Size(121, 24);
            this.cmbFormType.TabIndex = 1;
            // 
            // cmbVoucher
            // 
            this.cmbVoucher.EnterColor = System.Drawing.Color.LightPink;
            this.cmbVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVoucher.FormattingEnabled = true;
            this.cmbVoucher.Items.AddRange(new object[] {
            "Sales Invoice",
            "Sales Estimate",
            "Sales Order",
            "Sales Quotation",
            "Sales Return",
            "Purchase Invoice",
            "Purchase Return",
            "Purchase Order",
            "KOT"});
            this.cmbVoucher.LeaveColor = System.Drawing.Color.White;
            this.cmbVoucher.Location = new System.Drawing.Point(120, 6);
            this.cmbVoucher.Name = "cmbVoucher";
            this.cmbVoucher.Size = new System.Drawing.Size(121, 24);
            this.cmbVoucher.TabIndex = 1;
            this.cmbVoucher.SelectedIndexChanged += new System.EventHandler(this.cmbVoucher_SelectedIndexChanged);
            // 
            // cmbPrintType
            // 
            this.cmbPrintType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPrintType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPrintType.FormattingEnabled = true;
            this.cmbPrintType.Items.AddRange(new object[] {
            "Normal",
            "Dotmatrix",
            "Invoice "});
            this.cmbPrintType.LeaveColor = System.Drawing.Color.White;
            this.cmbPrintType.Location = new System.Drawing.Point(120, 62);
            this.cmbPrintType.Name = "cmbPrintType";
            this.cmbPrintType.Size = new System.Drawing.Size(121, 24);
            this.cmbPrintType.TabIndex = 3;
            this.cmbPrintType.SelectedIndexChanged += new System.EventHandler(this.cmbPrintType_SelectedIndexChanged);
            // 
            // lblLinePerPage
            // 
            this.lblLinePerPage.AutoSize = true;
            this.lblLinePerPage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLinePerPage.Location = new System.Drawing.Point(2, 264);
            this.lblLinePerPage.Name = "lblLinePerPage";
            this.lblLinePerPage.Size = new System.Drawing.Size(115, 16);
            this.lblLinePerPage.TabIndex = 17;
            this.lblLinePerPage.Text = "Lines Per Page :";
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDown.Location = new System.Drawing.Point(62, 241);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(55, 16);
            this.lblDown.TabIndex = 15;
            this.lblDown.Text = "Down :";
            // 
            // lblPitch
            // 
            this.lblPitch.AutoSize = true;
            this.lblPitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPitch.Location = new System.Drawing.Point(65, 313);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(52, 16);
            this.lblPitch.TabIndex = 21;
            this.lblPitch.Text = "Pitch :";
            // 
            // lblBoldOff
            // 
            this.lblBoldOff.AutoSize = true;
            this.lblBoldOff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBoldOff.Location = new System.Drawing.Point(78, 364);
            this.lblBoldOff.Name = "lblBoldOff";
            this.lblBoldOff.Size = new System.Drawing.Size(39, 16);
            this.lblBoldOff.TabIndex = 25;
            this.lblBoldOff.Text = "Off :";
            // 
            // lblBoldOn
            // 
            this.lblBoldOn.AutoSize = true;
            this.lblBoldOn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBoldOn.Location = new System.Drawing.Point(45, 338);
            this.lblBoldOn.Name = "lblBoldOn";
            this.lblBoldOn.Size = new System.Drawing.Size(72, 16);
            this.lblBoldOn.TabIndex = 23;
            this.lblBoldOn.Text = "Bold_On :";
            // 
            // lblReverse
            // 
            this.lblReverse.AutoSize = true;
            this.lblReverse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblReverse.Location = new System.Drawing.Point(51, 288);
            this.lblReverse.Name = "lblReverse";
            this.lblReverse.Size = new System.Drawing.Size(66, 16);
            this.lblReverse.TabIndex = 19;
            this.lblReverse.Text = "Riverse :";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFilePath.Location = new System.Drawing.Point(22, 417);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(95, 16);
            this.lblFilePath.TabIndex = 29;
            this.lblFilePath.Text = "Back Image :";
            // 
            // lblUp
            // 
            this.lblUp.AutoSize = true;
            this.lblUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUp.Location = new System.Drawing.Point(40, 216);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(77, 16);
            this.lblUp.TabIndex = 13;
            this.lblUp.Text = "UP Scroll :";
            // 
            // lblPrinters
            // 
            this.lblPrinters.AutoSize = true;
            this.lblPrinters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrinters.Location = new System.Drawing.Point(55, 393);
            this.lblPrinters.Name = "lblPrinters";
            this.lblPrinters.Size = new System.Drawing.Size(62, 16);
            this.lblPrinters.TabIndex = 27;
            this.lblPrinters.Text = "Printer :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(62, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(28, 37);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 16);
            this.label21.TabIndex = 0;
            this.label21.Text = "Form Type :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(1, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Space In Lines :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(44, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Voucher :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(19, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Page Height :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(30, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Print Type :";
            // 
            // lblPageWidth
            // 
            this.lblPageWidth.AutoSize = true;
            this.lblPageWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPageWidth.Location = new System.Drawing.Point(23, 139);
            this.lblPageWidth.Name = "lblPageWidth";
            this.lblPageWidth.Size = new System.Drawing.Size(94, 16);
            this.lblPageWidth.TabIndex = 7;
            this.lblPageWidth.Text = "Page Width :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSort);
            this.groupBox2.Controls.Add(this.cmbControl);
            this.groupBox2.Controls.Add(this.cmbControlType);
            this.groupBox2.Controls.Add(this.cmbControlPosition);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Location = new System.Drawing.Point(7, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 129);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Control";
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnSort.FlatAppearance.BorderSize = 0;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.ForeColor = System.Drawing.Color.White;
            this.btnSort.Location = new System.Drawing.Point(142, 99);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(60, 30);
            this.btnSort.TabIndex = 28;
            this.btnSort.Text = "Sor&t";
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // cmbControl
            // 
            this.cmbControl.EnterColor = System.Drawing.Color.LightPink;
            this.cmbControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbControl.FormattingEnabled = true;
            this.cmbControl.LeaveColor = System.Drawing.Color.White;
            this.cmbControl.Location = new System.Drawing.Point(91, 72);
            this.cmbControl.Name = "cmbControl";
            this.cmbControl.Size = new System.Drawing.Size(133, 24);
            this.cmbControl.TabIndex = 5;
            this.cmbControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbControl_KeyDown);
            // 
            // cmbControlType
            // 
            this.cmbControlType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbControlType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbControlType.FormattingEnabled = true;
            this.cmbControlType.LeaveColor = System.Drawing.Color.White;
            this.cmbControlType.Location = new System.Drawing.Point(92, 44);
            this.cmbControlType.Name = "cmbControlType";
            this.cmbControlType.Size = new System.Drawing.Size(133, 24);
            this.cmbControlType.TabIndex = 3;
            this.cmbControlType.SelectedIndexChanged += new System.EventHandler(this.cmbControlType_SelectedIndexChanged_1);
            // 
            // cmbControlPosition
            // 
            this.cmbControlPosition.EnterColor = System.Drawing.Color.LightPink;
            this.cmbControlPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbControlPosition.FormattingEnabled = true;
            this.cmbControlPosition.Items.AddRange(new object[] {
            "Company Section",
            "Invoice Section",
            "Party Section",
            "Invoice Details Header ",
            "Details Area",
            "Footer Section",
            "Footer Details",
            "Footer Bottom"});
            this.cmbControlPosition.LeaveColor = System.Drawing.Color.White;
            this.cmbControlPosition.Location = new System.Drawing.Point(92, 16);
            this.cmbControlPosition.Name = "cmbControlPosition";
            this.cmbControlPosition.Size = new System.Drawing.Size(133, 24);
            this.cmbControlPosition.TabIndex = 1;
            this.cmbControlPosition.SelectedIndexChanged += new System.EventHandler(this.cmbControlPosition_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(69, 100);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(31, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(5, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "Position :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(25, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 16);
            this.label14.TabIndex = 2;
            this.label14.Text = "Type :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(9, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 16);
            this.label15.TabIndex = 4;
            this.label15.Text = "Control :";
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(106, 100);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(30, 30);
            this.btnRemove.TabIndex = 26;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(160, 529);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(71, 30);
            this.btnPreview.TabIndex = 29;
            this.btnPreview.Text = "P&review";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click_1);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(91, 529);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 30);
            this.btnPrint.TabIndex = 28;
            this.btnPrint.Text = "&Print ";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(260, 21);
            this.tabControl1.Location = new System.Drawing.Point(925, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(258, 629);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabSettings_MouseDown);
            this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabSettings_MouseMove);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.txtFontSize);
            this.tabPage1.Controls.Add(this.txtTabindex);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.cbBold);
            this.tabPage1.Controls.Add(this.btnFont);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.txtSpec);
            this.tabPage1.Controls.Add(this.txtY);
            this.tabPage1.Controls.Add(this.btnPreview);
            this.tabPage1.Controls.Add(this.txtX);
            this.tabPage1.Controls.Add(this.btnPrint);
            this.tabPage1.Controls.Add(this.txtHeight);
            this.tabPage1.Controls.Add(this.txtWidth);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtText);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.cmbType);
            this.tabPage1.Controls.Add(this.cmbPosition);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.cmbAlgmnt);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(250, 600);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Item Properties";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(20, 529);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 30);
            this.btnUpdate.TabIndex = 27;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtFontSize
            // 
            this.txtFontSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFontSize.Data = 0;
            this.txtFontSize.EnterColor = System.Drawing.Color.LightPink;
            this.txtFontSize.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFontSize.Format = null;
            this.txtFontSize.isAllowNegative = false;
            this.txtFontSize.isAllowSpecialChar = false;
            this.txtFontSize.isNumberOnly = true;
            this.txtFontSize.isNumeric = false;
            this.txtFontSize.LeaveColor = System.Drawing.Color.White;
            this.txtFontSize.Location = new System.Drawing.Point(94, 295);
            this.txtFontSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtFontSize.Name = "txtFontSize";
            this.txtFontSize.Size = new System.Drawing.Size(127, 16);
            this.txtFontSize.TabIndex = 41;
            this.txtFontSize.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFontSize.WaterMark = null;
            this.txtFontSize.TextChanged += new System.EventHandler(this.txtFontSize_TextChanged);
            // 
            // txtTabindex
            // 
            this.txtTabindex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTabindex.Data = 0;
            this.txtTabindex.EnterColor = System.Drawing.Color.LightPink;
            this.txtTabindex.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTabindex.Format = null;
            this.txtTabindex.isAllowNegative = false;
            this.txtTabindex.isAllowSpecialChar = false;
            this.txtTabindex.isNumberOnly = true;
            this.txtTabindex.isNumeric = false;
            this.txtTabindex.LeaveColor = System.Drawing.Color.White;
            this.txtTabindex.Location = new System.Drawing.Point(94, 271);
            this.txtTabindex.Margin = new System.Windows.Forms.Padding(4);
            this.txtTabindex.Name = "txtTabindex";
            this.txtTabindex.Size = new System.Drawing.Size(127, 16);
            this.txtTabindex.TabIndex = 39;
            this.txtTabindex.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTabindex.WaterMark = null;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(32, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 16);
            this.label10.TabIndex = 38;
            this.label10.Text = "Index :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(28, 296);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 16);
            this.label18.TabIndex = 40;
            this.label18.Text = "F Size :";
            // 
            // cbBold
            // 
            this.cbBold.AutoSize = true;
            this.cbBold.Location = new System.Drawing.Point(38, 330);
            this.cbBold.Name = "cbBold";
            this.cbBold.Size = new System.Drawing.Size(54, 20);
            this.cbBold.TabIndex = 37;
            this.cbBold.Text = "Bold";
            this.cbBold.UseVisualStyleBackColor = true;
            this.cbBold.CheckedChanged += new System.EventHandler(this.cbBold_CheckedChanged);
            // 
            // btnFont
            // 
            this.btnFont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            this.btnFont.FlatAppearance.BorderSize = 0;
            this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFont.ForeColor = System.Drawing.Color.White;
            this.btnFont.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFont.Location = new System.Drawing.Point(98, 324);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(118, 31);
            this.btnFont.TabIndex = 36;
            this.btnFont.Text = "&Font";
            this.btnFont.UseVisualStyleBackColor = false;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Maroon;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(225, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(21, 23);
            this.btnEdit.TabIndex = 30;
            this.btnEdit.Text = ".";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtSpec
            // 
            this.txtSpec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpec.Data = 0;
            this.txtSpec.EnterColor = System.Drawing.Color.LightPink;
            this.txtSpec.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpec.Format = null;
            this.txtSpec.isAllowNegative = false;
            this.txtSpec.isAllowSpecialChar = false;
            this.txtSpec.isNumberOnly = false;
            this.txtSpec.isNumeric = false;
            this.txtSpec.LeaveColor = System.Drawing.Color.White;
            this.txtSpec.Location = new System.Drawing.Point(94, 246);
            this.txtSpec.Margin = new System.Windows.Forms.Padding(4);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(127, 16);
            this.txtSpec.TabIndex = 18;
            this.txtSpec.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSpec.WaterMark = null;
            this.txtSpec.Validated += new System.EventHandler(this.txtSpec_Validated);
            // 
            // txtY
            // 
            this.txtY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtY.Data = 0;
            this.txtY.EnterColor = System.Drawing.Color.LightPink;
            this.txtY.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY.Format = null;
            this.txtY.isAllowNegative = false;
            this.txtY.isAllowSpecialChar = false;
            this.txtY.isNumberOnly = true;
            this.txtY.isNumeric = false;
            this.txtY.LeaveColor = System.Drawing.Color.White;
            this.txtY.Location = new System.Drawing.Point(94, 111);
            this.txtY.Margin = new System.Windows.Forms.Padding(4);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(127, 16);
            this.txtY.TabIndex = 9;
            this.txtY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtY.WaterMark = null;
            this.txtY.TextChanged += new System.EventHandler(this.txtX_TextChanged);
            // 
            // txtX
            // 
            this.txtX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtX.Data = 0;
            this.txtX.EnterColor = System.Drawing.Color.LightPink;
            this.txtX.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX.Format = null;
            this.txtX.isAllowNegative = false;
            this.txtX.isAllowSpecialChar = false;
            this.txtX.isNumberOnly = true;
            this.txtX.isNumeric = false;
            this.txtX.LeaveColor = System.Drawing.Color.White;
            this.txtX.Location = new System.Drawing.Point(94, 87);
            this.txtX.Margin = new System.Windows.Forms.Padding(4);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(127, 16);
            this.txtX.TabIndex = 7;
            this.txtX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtX.WaterMark = null;
            this.txtX.TextChanged += new System.EventHandler(this.txtX_TextChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeight.Data = 0;
            this.txtHeight.EnterColor = System.Drawing.Color.LightPink;
            this.txtHeight.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeight.Format = null;
            this.txtHeight.isAllowNegative = false;
            this.txtHeight.isAllowSpecialChar = false;
            this.txtHeight.isNumberOnly = true;
            this.txtHeight.isNumeric = false;
            this.txtHeight.LeaveColor = System.Drawing.Color.White;
            this.txtHeight.Location = new System.Drawing.Point(94, 161);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(127, 16);
            this.txtHeight.TabIndex = 15;
            this.txtHeight.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtHeight.WaterMark = null;
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            this.txtHeight.Validated += new System.EventHandler(this.txtWidth_Validated);
            // 
            // txtWidth
            // 
            this.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWidth.Data = 0;
            this.txtWidth.EnterColor = System.Drawing.Color.LightPink;
            this.txtWidth.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.Format = null;
            this.txtWidth.isAllowNegative = false;
            this.txtWidth.isAllowSpecialChar = false;
            this.txtWidth.isNumberOnly = true;
            this.txtWidth.isNumeric = false;
            this.txtWidth.LeaveColor = System.Drawing.Color.White;
            this.txtWidth.Location = new System.Drawing.Point(94, 137);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(127, 16);
            this.txtWidth.TabIndex = 13;
            this.txtWidth.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWidth.WaterMark = null;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            this.txtWidth.Validated += new System.EventHandler(this.txtWidth_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(38, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Field :";
            // 
            // txtText
            // 
            this.txtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtText.Data = 0;
            this.txtText.EnterColor = System.Drawing.Color.LightPink;
            this.txtText.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtText.Format = null;
            this.txtText.isAllowNegative = false;
            this.txtText.isAllowSpecialChar = false;
            this.txtText.isNumberOnly = false;
            this.txtText.isNumeric = false;
            this.txtText.LeaveColor = System.Drawing.Color.White;
            this.txtText.Location = new System.Drawing.Point(94, 63);
            this.txtText.Margin = new System.Windows.Forms.Padding(4);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(127, 16);
            this.txtText.TabIndex = 5;
            this.txtText.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtText.WaterMark = null;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(17, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "Position :";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Data = 0;
            this.txtName.Enabled = false;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(94, 11);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(127, 16);
            this.txtName.TabIndex = 1;
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.WaterMark = null;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(4, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "Alignment :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label22.Location = new System.Drawing.Point(35, 247);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 16);
            this.label22.TabIndex = 17;
            this.label22.Text = "Spec :";
            // 
            // cmbType
            // 
            this.cmbType.Enabled = false;
            this.cmbType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "SpaceControl",
            "LabelControl",
            "LineLabelControl",
            "ValueControle",
            "BrakeControle"});
            this.cmbType.LeaveColor = System.Drawing.Color.White;
            this.cmbType.Location = new System.Drawing.Point(94, 32);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(122, 24);
            this.cmbType.TabIndex = 3;
            // 
            // cmbPosition
            // 
            this.cmbPosition.Enabled = false;
            this.cmbPosition.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Items.AddRange(new object[] {
            "Company Section",
            "Invoice Section",
            "Party Section",
            "Invoice Details Header ",
            "Details Area",
            "Footer Section",
            "Footer Details",
            "Footer Bottom"});
            this.cmbPosition.LeaveColor = System.Drawing.Color.White;
            this.cmbPosition.Location = new System.Drawing.Point(94, 215);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(127, 24);
            this.cmbPosition.TabIndex = 24;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(25, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(61, 16);
            this.label20.TabIndex = 14;
            this.label20.Text = "Height :";
            // 
            // cmbAlgmnt
            // 
            this.cmbAlgmnt.EnterColor = System.Drawing.Color.LightPink;
            this.cmbAlgmnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlgmnt.FormattingEnabled = true;
            this.cmbAlgmnt.Items.AddRange(new object[] {
            "Left",
            "Right",
            "Center"});
            this.cmbAlgmnt.LeaveColor = System.Drawing.Color.White;
            this.cmbAlgmnt.Location = new System.Drawing.Point(94, 187);
            this.cmbAlgmnt.Name = "cmbAlgmnt";
            this.cmbAlgmnt.Size = new System.Drawing.Size(127, 24);
            this.cmbAlgmnt.TabIndex = 22;
            this.cmbAlgmnt.SelectedIndexChanged += new System.EventHandler(this.cmbAlgmnt_SelectedIndexChanged);
            this.cmbAlgmnt.SelectionChangeCommitted += new System.EventHandler(this.cmbAlgmnt_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(30, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Width :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(38, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Text :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(59, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 16);
            this.label16.TabIndex = 6;
            this.label16.Text = "X :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(35, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Type :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(59, 112);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 16);
            this.label17.TabIndex = 8;
            this.label17.Text = "Y :";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(0, 38);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(132, 1);
            this.button8.TabIndex = 0;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(6, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(123, 24);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Print Design";
            // 
            // PrintDesignView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1329, 640);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlWorkSpace);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PrintDesignView";
            this.Load += new System.EventHandler(this.PrintDesignView_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.button8, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlWorkSpace, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.tabSettings, 0);
            this.tabSettings.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            this.tabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWidth.Text != string.Empty)
                {
                    DotmatrixSettings dsettings = entDSettings.Where(x => x.TabIndex == iTabindex).FirstOrDefault();
                    if (dsettings != null)
                    {
                        dsettings.ControleName = txtName.Text;
                        dsettings.isBold = cbBold.Checked;
                        dsettings.Alaingmnet = cmbAlgmnt.Text;
                        dsettings.Type = cmbType.Text;
                        dsettings.FeildText = txtText.Text;
                        dsettings.Position = cmbPosition.Text;
                        dsettings.width = txtWidth.Data;
                        if (txtTabindex.Text != string.Empty)
                        {
                            dsettings.iIndex = Convert.ToInt32(txtTabindex.Text);
                        }
                        dsettings.X = txtX.Data;
                        dsettings.Y = txtY.Data;
                        dsettings.Height = txtHeight.Data;
                        dsettings.FontSize = txtFontSize.Data;
                        dsettings.Font = btnFont.Text;
                    }
                }
                mouseClickedOnCstmTBox = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " 347");
            }
        }

        private void cbBold_CheckedChanged(object sender, EventArgs e)
        {
            if (txtFontSize.Text != string.Empty)
            {
                foreach (Control item in pnlWorkSpace.Controls)
                {
                    if (item.TabIndex == iTabindex && txtWidth.Text != string.Empty && isEditable == false && item is TextBox)
                    {
                        Font f;
                        if (cbBold.Checked)
                        {
                            f = new Font(btnFont.Text, txtFontSize.Data, FontStyle.Bold);
                        }
                        else
                        {
                            f = new Font(btnFont.Text, txtFontSize.Data, FontStyle.Regular);
                        }
                        item.Font = f;
                        DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
                        dotmatrixSettings = entDSettings.Where(x => x.TabIndex == iTabindex).FirstOrDefault();
                        dotmatrixSettings.FontSize = Convert.ToInt32(txtFontSize.Text);
                    }
                }
            }
        }

        private void txtFontSize_TextChanged(object sender, EventArgs e)
        {
            if (txtFontSize.Text != string.Empty)
            {
                foreach (Control item in pnlWorkSpace.Controls)
                {
                    if (item.TabIndex == iTabindex && txtWidth.Text != string.Empty && isEditable == false && item is TextBox)
                    {
                        Font f;
                        if (cbBold.Checked)
                        {
                            f = new Font(btnFont.Text, txtFontSize.Data, FontStyle.Bold);
                        }
                        else
                        {
                            f = new Font(btnFont.Text, txtFontSize.Data, FontStyle.Regular);
                        }
                        item.Font = f;
                        DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
                        dotmatrixSettings = entDSettings.Where(x => x.TabIndex == iTabindex).FirstOrDefault();
                        dotmatrixSettings.FontSize = Convert.ToInt32(txtFontSize.Text);
                    }
                }
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {

            foreach (Control item in pnlWorkSpace.Controls)
            {
                if (item.TabIndex == iTabindex && txtWidth.Text != string.Empty && isEditable == false && item is TextBox)
                {
                    item.Size = new Size(Convert.ToInt32(txtWidth.Text), Convert.ToInt32(txtHeight.Text));
                    DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
                    dotmatrixSettings = entDSettings.Where(x => x.TabIndex == iTabindex).FirstOrDefault();
                    dotmatrixSettings.width = Convert.ToInt32(txtWidth.Text);
                }
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

            foreach (Control item in pnlWorkSpace.Controls)
            {
                if (item.TabIndex == iTabindex && txtWidth.Text != string.Empty && isEditable == false && item is TextBox)
                {
                    item.Size = new Size(Convert.ToInt32(txtWidth.Text), Convert.ToInt32(txtHeight.Text));
                    DotmatrixSettings dotmatrixSettings = new DotmatrixSettings();
                    dotmatrixSettings = entDSettings.Where(x => x.TabIndex == iTabindex).FirstOrDefault();
                    dotmatrixSettings.width = Convert.ToInt32(txtWidth.Text);
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (isSortOn == false)
            {
                intSortIndex = 1;
                btnSort.BackColor = Color.Red;
                isSortOn = true;
            }
            else
            {
                btnSort.BackColor = Color.FromArgb(61, 77, 125);
                List<DotmatrixSettings> DMSettings = entDSettings.OrderBy(x => x.iIndex).ToList();
                entDSettings = DMSettings;
                isSortOn = false;
            }
        }
    }
}
