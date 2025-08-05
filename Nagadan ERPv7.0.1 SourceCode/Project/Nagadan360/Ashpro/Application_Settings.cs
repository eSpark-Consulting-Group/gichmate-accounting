using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class Application_Settings : BaseForm
	{
		private string strConnection;

		private List<SettingsEntity> entBooleanSettings;

		private List<SettingsEntity> entStringSettings;

		private DBContext db;

		private MasterValue defaultSalesForm;

		private IContainer components = null;

		private Button btnASSave;

		private TreeView trvSettings;

		private TabPage strSettings;

		private TabPage blnSettings;

		private TabControl tabSettings;

		private Button btnSave;

		private Panel panel1;

		private DataGridView dgvSettings;

		private BindingSource bdsSettings;

		private Label label8;

		private Label label9;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private AshComboBox cmbSalesType;

		private AshTextBoxNormal txtCurrencyFormat;

		private AshTextBoxNormal txtQuantityFormat;

		private AshTextBoxNormal txtDefaultCompany;

		private GroupBox groupBox1;

		private AshTextBoxNormal txtEmailProvider;

		private AshTextBoxNormal txtPassword;

		private AshTextBoxNormal txtUserName;

		private AshTextBoxNormal txtEmailAddress;

		private AshTextBoxNormal txtClientPort;

		private AshTextBoxNormal txtClientName;

		private Label label10;

		private Label label7;

		private Label label6;

		private Label label1;

		private AshComboBox cmbPurchaseType;

		private Label label11;

		private Button button5;

		private Label lblTitle;

		private Button btnFormType;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewCheckBoxColumn Column2;

		private Button btnBrowseFolder;

		private AshTextBoxNormal txtBackupPath;

		private AshTextBoxNormal txtCessPercentage;

		private Label label13;

		private Label label12;

		private AshTextBoxNormal txtDateFormat;

		private Label label14;

		private AshTextBoxNormal txtRateBarcode;

		private Label label15;

		private AshTextBoxNormal txtWeightBarcode;

		private Label label16;

		private AshComboBox cmbOnlineAccount;

		private AshComboBox cmbBankAccount;

		private AshComboBox cmbCashAccount;

		private Label label19;

		private Label label18;

		private Label label17;

		private TabPage tpColor;

		private Button btnForeColor;

		private Button btnBackColor;

		private Button btnMini;

		private Button btnCD;

		private Button btnRdlc;

		private Button btnPrinter;

		private AshComboBox cmbMini;

		private AshComboBox cmbComPort;

		private AshComboBox cmbCD;

		private Label label25;

		private AshComboBox cmbRDLC;

		private Label label24;

		private AshComboBox cmbTerminalPrinter;

		private Label label23;

		private Label label21;

		private Label label20;

		private AshTextBoxNormal txtSeqNo;

		private Label label22;

		private Button btnSeqNo;

		private AshTextBoxNormal txtCounterName;

		private Label label26;

		private Button btnCounter;

		private TabPage tabPOS;

		private Button btnGeneralFore;

		private Button btnSideIconFore;

		private Button btnProductFore;

		private Button btnNumberpadFore;

		private Button btnTopButtonFore;

		private Button btnCategoryFore;

		private Button btnTitleFore;

		private Button btnGeneralBack;

		private Button btnSideIconBack;

		private Button btnProductBack;

		private Button btnNumberpadBack;

		private Button btnTopButtonBack;

		private Button btnCategoryBack;

		private Button btnTitleBack;

		private GroupBox groupBox3;

		private Button btnDetailsPanelFore;

		private GroupBox groupBox2;

		private Button btnDetailsPanelBack;

		private GroupBox groupBox4;

		private Button btnTop;

		private Button btnDgvHeaderBack;

		private Button btnSide;

		private Button btnDgvHeaderFore;

		private Button btnDgvBack;

		private GroupBox groupBox5;

		private Button btnLoginFore;

		private Button btnLoginBack;

		private Button btnProductPB;

		private Button btnCategoryPB;

		private Button btnNumberpadPBC;

		private GroupBox groupBox7;

		private Button btnOtherF;

		private Button btnOtherB;

		private Button btnDefault;

		private GroupBox grpButtonSize;

		private Label label28;

		private Label label27;

		private AshTextBoxNormal txtProductY;

		private AshTextBoxNormal txtProductX;

		private AshTextBoxNormal txtGroupY;

		private AshTextBoxNormal txtGroupX;

		private Button btnSaveSize;

		private Label label29;

		private AshTextBoxNormal txtOnlinePaymentLabel;

		private Label label30;

		private AshComboBox cmbCurrency;

		private AshTextBoxNormal txtGroupCount;

		private AshTextBoxNormal txtProductCount;

		private Label label32;

		private Label label31;

		private Button btnGroupCount;

		private Button btnProCount;

		private AshComboBox cmbWghScalePort;

		private Label label33;

		private Button btnWghScale;

		private Button btnCusDisplay;

		private Label label37;

		private Label label36;

		private Label label35;

		private Label label34;

		private AshTextBoxNormal txtAddFour;

		private AshTextBoxNormal txtAddThree;

		private AshTextBoxNormal txtAddTwo;

		private AshTextBoxNormal txtAddOne;

		private AshTextBoxNormal txtReportPath;

		private Label label38;

		private Button btnCloseCashPath;

		private Button btn130;

		private Button btn12;

		private Button btnSalesForm;

		private AshComboBox cmbSalesForm;

		private Label label39;

		public Application_Settings()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
		}

		private void Application_Settings_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			if (Settings.Default.MainForm == "Restaurant")
			{
				tabPOS.Text = "Restaurant";
				btnTopButtonBack.Text = "Main Icons Button";
				btnTopButtonFore.Text = "Main Icons Button";
				btnSideIconBack.Text = "KOT Icons Button";
				btnSideIconFore.Text = "KOT Icons Button";
				btnTitleBack.Text = "customer Panel";
				btnTitleFore.Text = "customer Panel";
				btnTop.Text = "Main Panel Back Color";
				btnSide.Text = "KOT Panel Back Color";
			}
			txtBackupPath.Text = Settings.Default.BackupPath;
			txtProductCount.Data = Settings.Default.iProductsPerPage;
			txtGroupCount.Data = Settings.Default.iGroupsPerPage;
			if (Utility.gbValues != null)
			{
				txtGroupX.Data = Utility.gbValues.Name.ToInt32();
				txtGroupY.Data = Utility.gbValues.Description.ToInt32();
			}
			if (Utility.pbValues != null)
			{
				txtProductX.Data = Utility.pbValues.Name.ToInt32();
				txtProductY.Data = Utility.pbValues.Description.ToInt32();
			}
			btnBackColor.BackColor = Settings.Default.BackColor;
			btnForeColor.ForeColor = Settings.Default.ForeColor;
			txtSeqNo.Data = Utility.uSerialNumber;
			strConnection = Settings.Default.strConnetionString;
			List<SettingsEntity> list = new List<SettingsEntity>();
			list = db.Settings().ToList();
			entBooleanSettings = new List<SettingsEntity>();
			entStringSettings = new List<SettingsEntity>();
			dgvSettings.AutoGenerateColumns = false;
			entBooleanSettings = (from x in list
				where x.Remarks == "General" && (x.StringValue == null || x.StringValue == string.Empty)
				select x into y
				orderby y.Caption
				select y).ToList();
			bdsSettings.DataSource = entBooleanSettings;
			dgvSettings.DataSource = bdsSettings;
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cmbTerminalPrinter.Items.Add(installedPrinter);
				cmbRDLC.Items.Add(installedPrinter);
				cmbCD.Items.Add(installedPrinter);
				cmbMini.Items.Add(installedPrinter);
			}
			string[] portNames = SerialPort.GetPortNames();
			string[] array = portNames;
			foreach (string item2 in array)
			{
				cmbComPort.Items.Add(item2);
				cmbWghScalePort.Items.Add(item2);
			}
			cmbComPort.Text = Settings.Default.sCom;
			cmbWghScalePort.Text = Settings.Default.ScalePort;
			cmbTerminalPrinter.Text = Settings.Default.TerminalPrinter;
			cmbRDLC.Text = Settings.Default.RDLCPrinter;
			cmbCD.Text = Settings.Default.CDPrinter;
			cmbMini.Text = Settings.Default.MiniReportPrinter;
			txtCounterName.Text = Settings.Default.CounterName;
			ColorApplyMethod();
			AniHelper.FillSalesForms(cmbSalesForm);
			defaultSalesForm = db.MasterValues(new
			{
				MasterType = "DefaultSalesFormType"
			}).FirstOrDefault();
			cmbSalesForm.SelectedValue = defaultSalesForm.Name;
		}

		private void ColorApplyMethod()
		{
			btnNumberpadBack.BackColor = Settings.Default.NumberBB;
			btnNumberpadFore.BackColor = Settings.Default.NumberBF;
			btnCategoryBack.BackColor = Settings.Default.CategoryBB;
			btnCategoryFore.BackColor = Settings.Default.CategoryBF;
			btnProductBack.BackColor = Settings.Default.ProductBB;
			btnProductFore.BackColor = Settings.Default.ProductBF;
			btnTopButtonBack.BackColor = Settings.Default.TopIBB;
			btnTopButtonFore.BackColor = Settings.Default.TopIBF;
			btnSideIconBack.BackColor = Settings.Default.SideIBB;
			btnSideIconFore.BackColor = Settings.Default.SideIBF;
			btnGeneralBack.BackColor = Settings.Default.GeneralBB;
			btnGeneralFore.BackColor = Settings.Default.GeneralBF;
			btnTitleBack.BackColor = Settings.Default.TitlebarPB;
			btnTitleFore.BackColor = Settings.Default.TitlebarPF;
			btnDetailsPanelBack.BackColor = Settings.Default.DetailPB;
			btnDetailsPanelFore.BackColor = Settings.Default.DetailPF;
			btnLoginBack.BackColor = Settings.Default.LoginB;
			btnLoginFore.BackColor = Settings.Default.LoginF;
			btnOtherB.BackColor = Settings.Default.OtherB;
			btnOtherF.BackColor = Settings.Default.OtherF;
			btnNumberpadPBC.BackColor = Settings.Default.NumberpadPB;
			btnCategoryPB.BackColor = Settings.Default.CategoryPB;
			btnProductPB.BackColor = Settings.Default.ProductPB;
			btnTop.BackColor = Settings.Default.TopPB;
			btnSide.BackColor = Settings.Default.SidePB;
			btnDgvHeaderBack.BackColor = Settings.Default.DgvheaderB;
			btnDgvHeaderFore.BackColor = Settings.Default.DgvheaderF;
			btnDgvBack.BackColor = Settings.Default.DgvB;
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (trvSettings.SelectedNode.Text != "Sales" && trvSettings.SelectedNode.Text != "Purchase")
			{
				entBooleanSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == trvSettings.SelectedNode.Text && (x.StringValue == null || x.StringValue == string.Empty)).ToList();
				bdsSettings.DataSource = entBooleanSettings;
				dgvSettings.DataSource = bdsSettings;
			}
		}

		private void tabSettings_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (CommonSettings.entSettings != null && tabSettings.SelectedTab == strSettings)
				{
					entStringSettings = CommonSettings.entSettings.Where((SettingsEntity x) => x.Remarks == "General" && x.StringValue != null && x.StringValue != string.Empty).ToList();
					txtEmailProvider.Text = CommonSettings.strEmailProvider;
					txtClientName.Text = CommonSettings.strClientName;
					txtClientPort.Text = CommonSettings.strClientPort;
					txtEmailAddress.Text = CommonSettings.strEmailAddress;
					txtUserName.Text = CommonSettings.strUserName;
					txtPassword.Text = CommonSettings.strPassword;
					txtDefaultCompany.Text = CommonSettings.strDefaultCompany;
					txtQuantityFormat.Text = CommonSettings.strQuantityFormat;
					txtCurrencyFormat.Text = CommonSettings.strCurencyFormat;
					txtDateFormat.Text = CommonSettings.strDateFormat;
					txtRateBarcode.Text = CommonSettings.strRatebasedBarcode;
					txtWeightBarcode.Text = CommonSettings.strWeightbasedBarcode;
					txtOnlinePaymentLabel.Text = CommonSettings.strOnlinePayment;
					txtAddOne.Text = CommonSettings.strAddressOneLabel;
					txtAddTwo.Text = CommonSettings.strAddressTwoLabel;
					txtAddThree.Text = CommonSettings.strAddressThreeLabel;
					txtAddFour.Text = CommonSettings.strAddressFourLabel;
					AniHelper.FillFormType(cmbSalesType);
					AniHelper.FillFormType(cmbPurchaseType);
					AniHelper.FillCashAccount(cmbCashAccount);
					AniHelper.FillBankAccount(cmbBankAccount);
					AniHelper.FillBankAccount(cmbOnlineAccount);
					AniHelper.FillCurrency(cmbCurrency);
					if (cmbCashAccount.Items.Count > 0)
					{
						cmbCashAccount.SelectedValue = CommonSettings.strDefaultCashAccountId;
					}
					if (cmbBankAccount.Items.Count > 0)
					{
						cmbBankAccount.SelectedValue = CommonSettings.strDefaultBankAccountId;
					}
					if (cmbOnlineAccount.Items.Count > 0)
					{
						cmbOnlineAccount.SelectedValue = CommonSettings.strDefaultOnlineAccountId;
					}
					cmbSalesType.Text = CommonSettings.strDefaultSalesType;
					cmbPurchaseType.Text = CommonSettings.strDefaultPurchaseType;
					if (cmbCurrency.Items.Count > 0)
					{
						cmbCurrency.SelectedValue = CommonSettings.strDefaultCurrency.ToInt32();
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnASSave_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (SettingsEntity entStringSetting in entStringSettings)
				{
					try
					{
						if (entStringSetting.Name == "strEmailProvider")
						{
							entStringSetting.StringValue = txtEmailProvider.Text;
						}
						if (entStringSetting.Name == "strClientName")
						{
							entStringSetting.StringValue = txtClientName.Text;
						}
						if (entStringSetting.Name == "strClientPort")
						{
							entStringSetting.StringValue = txtClientPort.Text;
						}
						if (entStringSetting.Name == "strEmailAddress")
						{
							entStringSetting.StringValue = txtEmailAddress.Text;
						}
						if (entStringSetting.Name == "strUserName")
						{
							entStringSetting.StringValue = txtUserName.Text;
						}
						if (entStringSetting.Name == "strPassword")
						{
							entStringSetting.StringValue = txtPassword.Text;
						}
						if (entStringSetting.Name == "strDefaultCompany")
						{
							entStringSetting.StringValue = txtDefaultCompany.Text;
						}
						if (entStringSetting.Name == "strQuantityFormat")
						{
							entStringSetting.StringValue = txtQuantityFormat.Text;
						}
						if (entStringSetting.Name == "strCurencyFormat")
						{
							entStringSetting.StringValue = txtCurrencyFormat.Text;
						}
						if (entStringSetting.Name == "strDateFormat")
						{
							entStringSetting.StringValue = txtDateFormat.Text;
						}
						if (entStringSetting.Name == "strDefaultSalesType")
						{
							entStringSetting.StringValue = cmbSalesType.Text;
						}
						if (entStringSetting.Name == "strDefaultPurchaseType")
						{
							entStringSetting.StringValue = cmbPurchaseType.Text;
						}
						if (entStringSetting.Name == "strRatebasedBarcode")
						{
							entStringSetting.StringValue = txtRateBarcode.Text;
						}
						if (entStringSetting.Name == "strWeightbasedBarcode")
						{
							entStringSetting.StringValue = txtWeightBarcode.Text;
						}
						if (entStringSetting.Name == "strDefaultCashAccountId")
						{
							entStringSetting.StringValue = cmbCashAccount.SelectedValue.ToString();
						}
						if (entStringSetting.Name == "strDefaultBankAccountId")
						{
							entStringSetting.StringValue = cmbBankAccount.SelectedValue.ToString();
						}
						if (entStringSetting.Name == "strDefaultOnlineAccountId")
						{
							entStringSetting.StringValue = cmbOnlineAccount.SelectedValue.ToString();
						}
						if (entStringSetting.Name == "strOnlinePayment")
						{
							entStringSetting.StringValue = txtOnlinePaymentLabel.Text;
						}
						if (entStringSetting.Name == "strDefaultCurrency")
						{
							entStringSetting.StringValue = cmbCurrency.SelectedValue.ToString();
						}
						if (entStringSetting.Name == "strAddressOneLabel")
						{
							entStringSetting.StringValue = txtAddOne.Text;
						}
						if (entStringSetting.Name == "strAddressTwoLabel")
						{
							entStringSetting.StringValue = txtAddTwo.Text;
						}
						if (entStringSetting.Name == "strAddressThreeLabel")
						{
							entStringSetting.StringValue = txtAddThree.Text;
						}
						if (entStringSetting.Name == "strAddressFourLabel")
						{
							entStringSetting.StringValue = txtAddFour.Text;
						}
					}
					catch (Exception)
					{
						_ = entStringSetting.Name;
					}
				}
				if (CommonSettings.UpdateSettings(entStringSettings, strConnection))
				{
					CommonSettings.LoadSettings();
					MessageBox.Show("Settings Updated", Utility.uManufacturer);
				}
				Close();
			}
			catch (Exception)
			{
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (CommonSettings.UpdateSettings(entBooleanSettings, strConnection))
			{
				AniHelper.FillFormType(cmbPurchaseType);
				AniHelper.FillFormType(cmbSalesType);
				if (cmbSalesType.Items.Count > 0)
				{
					cmbSalesType.SelectedIndex = 0;
				}
				if (cmbPurchaseType.Items.Count > 0)
				{
					cmbPurchaseType.SelectedIndex = 0;
				}
				foreach (SettingsEntity entStringSetting in entStringSettings)
				{
					if (entStringSetting.Name == "strDefaultSalesType")
					{
						entStringSetting.StringValue = cmbSalesType.Text;
					}
					if (entStringSetting.Name == "strDefaultPurchaseType")
					{
						entStringSetting.StringValue = cmbPurchaseType.Text;
					}
				}
				if (CommonSettings.UpdateSettings(entStringSettings, strConnection))
				{
					MessageBox.Show("Settings Updated", Utility.uManufacturer);
				}
			}
			Close();
		}

		private void btnFormType_Click(object sender, EventArgs e)
		{
			FormTypes formTypes = new FormTypes();
			if (formTypes.ShowDialog() == DialogResult.OK)
			{
				AniHelper.FillFormType(cmbSalesType);
				AniHelper.FillFormType(cmbPurchaseType);
			}
		}

		private void btnBrowseFolder_Click(object sender, EventArgs e)
		{
			using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			DialogResult dialogResult = folderBrowserDialog.ShowDialog();
			if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
			{
				string text2 = (txtBackupPath.Text = (Settings.Default.BackupPath = folderBrowserDialog.SelectedPath));
				Settings.Default.Save();
			}
		}

		private void btnPrinter_Click(object sender, EventArgs e)
		{
			Settings.Default.TerminalPrinter = cmbTerminalPrinter.Text;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnBackColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				Color color3 = (btnBackColor.BackColor = (Settings.Default.BackColor = colorDialog.Color));
				Settings.Default.Save();
			}
		}

		private void btnForeColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				Color color3 = (btnForeColor.BackColor = (Settings.Default.ForeColor = colorDialog.Color));
				Settings.Default.Save();
			}
		}

		private void btnSeqNo_Click(object sender, EventArgs e)
		{
			Utility.uSerialNumber = txtSeqNo.Data;
			AniHelper.SerialNumberMethod(txtSeqNo.Data);
			Messages.SavedMessage();
		}

		private void btnRdlc_Click(object sender, EventArgs e)
		{
			Settings.Default.RDLCPrinter = cmbRDLC.Text;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnCD_Click(object sender, EventArgs e)
		{
			Settings.Default.CDPrinter = cmbCD.Text;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnMini_Click(object sender, EventArgs e)
		{
			Settings.Default.MiniReportPrinter = cmbMini.Text;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnCounter_Click(object sender, EventArgs e)
		{
			Settings.Default.CounterName = txtCounterName.Text;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				button.BackColor = colorDialog.Color;
			}
			object tag = button.Tag;
			object obj = tag;
			switch (obj as string)
			{
			case "SPBC":
				Settings.Default.SidePB = colorDialog.Color;
				break;
			case "TPF":
				Settings.Default.TitlebarPF = colorDialog.Color;
				break;
			case "TPB":
				Settings.Default.TitlebarPB = colorDialog.Color;
				break;
			case "SBF":
				Settings.Default.SideIBF = colorDialog.Color;
				break;
			case "SBB":
				Settings.Default.SideIBB = colorDialog.Color;
				break;
			case "OBB":
				Settings.Default.OtherB = colorDialog.Color;
				break;
			case "OBF":
				Settings.Default.OtherF = colorDialog.Color;
				break;
			case "CBF":
				Settings.Default.CategoryBF = colorDialog.Color;
				break;
			case "CBB":
				Settings.Default.CategoryBB = colorDialog.Color;
				break;
			case "TPBC":
				Settings.Default.TopPB = colorDialog.Color;
				break;
			case "CPBC":
				Settings.Default.CategoryPB = colorDialog.Color;
				break;
			case "LHBC":
				Settings.Default.DgvheaderB = colorDialog.Color;
				break;
			case "DPB":
				Settings.Default.DetailPB = colorDialog.Color;
				break;
			case "LHFC":
				Settings.Default.DgvheaderF = colorDialog.Color;
				break;
			case "LBC":
				Settings.Default.DgvB = colorDialog.Color;
				break;
			case "DPF":
				Settings.Default.DetailPF = colorDialog.Color;
				break;
			case "PPBC":
				Settings.Default.ProductPB = colorDialog.Color;
				break;
			case "LBF":
				Settings.Default.LoginF = colorDialog.Color;
				break;
			case "LBB":
				Settings.Default.LoginB = colorDialog.Color;
				break;
			case "NBF":
				Settings.Default.NumberBF = colorDialog.Color;
				break;
			case "NBB":
				Settings.Default.NumberBB = colorDialog.Color;
				break;
			case "NPBC":
				Settings.Default.NumberpadPB = colorDialog.Color;
				break;
			case "GBB":
				Settings.Default.GeneralBB = colorDialog.Color;
				break;
			case "GBF":
				Settings.Default.GeneralBF = colorDialog.Color;
				break;
			case "PBB":
				Settings.Default.ProductBB = colorDialog.Color;
				break;
			case "PBF":
				Settings.Default.ProductBF = colorDialog.Color;
				break;
			case "TBF":
				Settings.Default.TopIBF = colorDialog.Color;
				break;
			case "TBB":
				Settings.Default.TopIBB = colorDialog.Color;
				break;
			}
			Settings.Default.Save();
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			Color color = Color.FromArgb(128, 255, 255);
			Color black = Color.Black;
			Color red = Color.Red;
			Color lightGray = Color.LightGray;
			Color black2 = Color.Black;
			Color lgnBackColor = Color.FromArgb(128, 255, 255);
			Color black3 = Color.Black;
			Color othrBackColor = Color.FromArgb(128, 255, 255);
			Color black4 = Color.Black;
			ColorSaveMethod(color, black, red, lightGray, black2, lgnBackColor, black3, othrBackColor, black4);
			ColorApplyMethod();
		}

		private static void ColorSaveMethod(Color btnBackColor, Color btnForeColor, Color btnForeColor2, Color pnlBackColor, Color pnlForeColor, Color lgnBackColor, Color lgnForeColor, Color othrBackColor, Color othrForeColor)
		{
			Settings.Default.NumberBB = btnBackColor;
			Settings.Default.NumberBF = btnForeColor;
			Settings.Default.CategoryBB = btnBackColor;
			Settings.Default.CategoryBF = btnForeColor;
			Settings.Default.ProductBB = btnBackColor;
			Settings.Default.ProductBF = btnForeColor;
			Settings.Default.TopIBB = btnBackColor;
			Settings.Default.TopIBF = btnForeColor2;
			Settings.Default.SideIBB = btnBackColor;
			Settings.Default.SideIBF = btnForeColor2;
			Settings.Default.GeneralBB = btnBackColor;
			Settings.Default.GeneralBF = btnForeColor;
			Settings.Default.TitlebarPB = Color.Black;
			Settings.Default.TitlebarPF = Color.White;
			Settings.Default.DetailPB = pnlBackColor;
			Settings.Default.DetailPF = pnlForeColor;
			Settings.Default.LoginB = lgnBackColor;
			Settings.Default.LoginF = lgnForeColor;
			Settings.Default.OtherB = othrBackColor;
			Settings.Default.OtherF = othrForeColor;
			Settings.Default.NumberpadPB = pnlBackColor;
			Settings.Default.CategoryPB = pnlBackColor;
			Settings.Default.ProductPB = pnlBackColor;
			Settings.Default.TopPB = pnlBackColor;
			Settings.Default.SidePB = pnlBackColor;
			Settings.Default.DgvheaderB = btnBackColor;
			Settings.Default.DgvheaderF = pnlForeColor;
			Settings.Default.DgvB = pnlBackColor;
			Settings.Default.Save();
		}

		private void btnSaveSize_Click(object sender, EventArgs e)
		{
			if (txtGroupX.Data == 0 || txtGroupY.Data == 0 || txtProductX.Data == 0 || txtProductY.Data == 0)
			{
				Messages.WarningMessage();
			}
			Utility.gbValues.Name = txtGroupX.Data.ToString();
			Utility.gbValues.Description = txtGroupY.Data.ToString();
			Utility.pbValues.Name = txtProductX.Data.ToString();
			Utility.pbValues.Description = txtProductY.Data.ToString();
			AshproCommon.UpdateToDatabaseObj(Utility.gbValues, "MasterValue", "Id", Utility.gbValues.Id.toInt32());
			AshproCommon.UpdateToDatabaseObj(Utility.pbValues, "MasterValue", "Id", Utility.pbValues.Id.toInt32());
			Messages.UpdateMessage();
		}

		private void btnProCount_Click(object sender, EventArgs e)
		{
			Settings.Default.iProductsPerPage = txtProductCount.Data;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnGroupCount_Click(object sender, EventArgs e)
		{
			Settings.Default.iGroupsPerPage = txtGroupCount.Data;
			Settings.Default.Save();
			Messages.SavedMessage();
		}

		private void btnCusDisplay_Click(object sender, EventArgs e)
		{
			Settings.Default.sCom = cmbComPort.Text;
			Settings.Default.Save();
			Messages.UpdateMessage();
		}

		private void btnWghScale_Click(object sender, EventArgs e)
		{
			Settings.Default.ScalePort = cmbWghScalePort.Text;
			Settings.Default.Save();
			Messages.UpdateMessage();
		}

		private void btnCloseCashPath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtReportPath.Text = folderBrowserDialog.SelectedPath;
				Settings.Default.CloseCashPDF = txtReportPath.Text;
				Settings.Default.Save();
			}
		}

		private void btn12_Click(object sender, EventArgs e)
		{
			if (Messages.DialogMessage("Are you want to change the report Format?", isYesisDefault: false))
			{
				Button button = (Button)sender;
				string sValue = button.Text;
				AshUsercontrol.ProgressBar progressBar = new AshUsercontrol.ProgressBar(this);
				DirectoryCopyMethod(sValue, "ReportForms");
				DirectoryCopyMethod(sValue, "MiniReportForm");
				progressBar.Visible = false;
				Messages.SavedMessage();
			}
		}

		private static void DirectoryCopyMethod(string sValue, string sFolder)
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			string path = directoryName + "\\" + sValue + "\\" + sFolder;
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			FileInfo[] files = directoryInfo.GetFiles();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				string sourceFileName = directoryName + "\\" + sValue + "\\" + sFolder + "\\" + fileInfo.ToString();
				string destFileName = directoryName + "\\" + sFolder + "\\" + fileInfo.ToString();
				try
				{
					File.Copy(sourceFileName, destFileName, overwrite: true);
				}
				catch (Exception)
				{
				}
			}
		}

		private void btnSalesForm_Click(object sender, EventArgs e)
		{
			defaultSalesForm.Name = cmbSalesForm.SelectedValue.ToString();
			defaultSalesForm.Description = cmbSalesForm.SelectedValue.ToString();
			if (AshproCommon.UpdateToDatabaseObj(defaultSalesForm, "MasterValue", "Id", defaultSalesForm.Id.toInt32()))
			{
				Messages.UpdateMessage();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application_Settings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnASSave = new System.Windows.Forms.Button();
            this.trvSettings = new System.Windows.Forms.TreeView();
            this.strSettings = new System.Windows.Forms.TabPage();
            this.label29 = new System.Windows.Forms.Label();
            this.grpButtonSize = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.btnSaveSize = new System.Windows.Forms.Button();
            this.txtProductY = new AshUsercontrol.AshTextBoxNormal();
            this.txtProductX = new AshUsercontrol.AshTextBoxNormal();
            this.txtGroupY = new AshUsercontrol.AshTextBoxNormal();
            this.txtGroupX = new AshUsercontrol.AshTextBoxNormal();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnFormType = new System.Windows.Forms.Button();
            this.cmbPurchaseType = new AshUsercontrol.AshComboBox();
            this.txtOnlinePaymentLabel = new AshUsercontrol.AshTextBoxNormal();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbOnlineAccount = new AshUsercontrol.AshComboBox();
            this.txtAddFour = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddThree = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddTwo = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddOne = new AshUsercontrol.AshTextBoxNormal();
            this.cmbBankAccount = new AshUsercontrol.AshComboBox();
            this.cmbCashAccount = new AshUsercontrol.AshComboBox();
            this.cmbCurrency = new AshUsercontrol.AshComboBox();
            this.cmbSalesType = new AshUsercontrol.AshComboBox();
            this.txtCurrencyFormat = new AshUsercontrol.AshTextBoxNormal();
            this.txtBackupPath = new AshUsercontrol.AshTextBoxNormal();
            this.txtQuantityFormat = new AshUsercontrol.AshTextBoxNormal();
            this.txtWeightBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.txtRateBarcode = new AshUsercontrol.AshTextBoxNormal();
            this.txtCessPercentage = new AshUsercontrol.AshTextBoxNormal();
            this.txtDateFormat = new AshUsercontrol.AshTextBoxNormal();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDefaultCompany = new AshUsercontrol.AshTextBoxNormal();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEmailProvider = new AshUsercontrol.AshTextBoxNormal();
            this.txtPassword = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClientPort = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClientName = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.blnSettings = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tpColor = new System.Windows.Forms.TabPage();
            this.cmbWghScalePort = new AshUsercontrol.AshComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtReportPath = new AshUsercontrol.AshTextBoxNormal();
            this.txtGroupCount = new AshUsercontrol.AshTextBoxNormal();
            this.txtCounterName = new AshUsercontrol.AshTextBoxNormal();
            this.txtProductCount = new AshUsercontrol.AshTextBoxNormal();
            this.txtSeqNo = new AshUsercontrol.AshTextBoxNormal();
            this.label38 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnCloseCashPath = new System.Windows.Forms.Button();
            this.btn130 = new System.Windows.Forms.Button();
            this.btnGroupCount = new System.Windows.Forms.Button();
            this.btnCounter = new System.Windows.Forms.Button();
            this.btn12 = new System.Windows.Forms.Button();
            this.btnProCount = new System.Windows.Forms.Button();
            this.btnSalesForm = new System.Windows.Forms.Button();
            this.btnSeqNo = new System.Windows.Forms.Button();
            this.btnWghScale = new System.Windows.Forms.Button();
            this.btnCusDisplay = new System.Windows.Forms.Button();
            this.btnMini = new System.Windows.Forms.Button();
            this.btnCD = new System.Windows.Forms.Button();
            this.btnRdlc = new System.Windows.Forms.Button();
            this.btnPrinter = new System.Windows.Forms.Button();
            this.cmbMini = new AshUsercontrol.AshComboBox();
            this.cmbComPort = new AshUsercontrol.AshComboBox();
            this.cmbCD = new AshUsercontrol.AshComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbRDLC = new AshUsercontrol.AshComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbSalesForm = new AshUsercontrol.AshComboBox();
            this.cmbTerminalPrinter = new AshUsercontrol.AshComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.tabPOS = new System.Windows.Forms.TabPage();
            this.btnDefault = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnOtherF = new System.Windows.Forms.Button();
            this.btnOtherB = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnLoginFore = new System.Windows.Forms.Button();
            this.btnLoginBack = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnNumberpadPBC = new System.Windows.Forms.Button();
            this.btnTop = new System.Windows.Forms.Button();
            this.btnDgvHeaderBack = new System.Windows.Forms.Button();
            this.btnSide = new System.Windows.Forms.Button();
            this.btnDgvHeaderFore = new System.Windows.Forms.Button();
            this.btnProductPB = new System.Windows.Forms.Button();
            this.btnCategoryPB = new System.Windows.Forms.Button();
            this.btnDgvBack = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNumberpadFore = new System.Windows.Forms.Button();
            this.btnTitleFore = new System.Windows.Forms.Button();
            this.btnGeneralFore = new System.Windows.Forms.Button();
            this.btnDetailsPanelFore = new System.Windows.Forms.Button();
            this.btnSideIconFore = new System.Windows.Forms.Button();
            this.btnCategoryFore = new System.Windows.Forms.Button();
            this.btnProductFore = new System.Windows.Forms.Button();
            this.btnTopButtonFore = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnNumberpadBack = new System.Windows.Forms.Button();
            this.btnTitleBack = new System.Windows.Forms.Button();
            this.btnDetailsPanelBack = new System.Windows.Forms.Button();
            this.btnCategoryBack = new System.Windows.Forms.Button();
            this.btnTopButtonBack = new System.Windows.Forms.Button();
            this.btnProductBack = new System.Windows.Forms.Button();
            this.btnSideIconBack = new System.Windows.Forms.Button();
            this.btnGeneralBack = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.bdsSettings = new System.Windows.Forms.BindingSource(this.components);
            this.strSettings.SuspendLayout();
            this.grpButtonSize.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.blnSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.tpColor.SuspendLayout();
            this.tabPOS.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // btnASSave
            // 
            resources.ApplyResources(this.btnASSave, "btnASSave");
            this.btnASSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnASSave.ForeColor = System.Drawing.Color.White;
            this.btnASSave.Name = "btnASSave";
            this.btnASSave.TabStop = false;
            this.btnASSave.UseVisualStyleBackColor = false;
            this.btnASSave.Click += new System.EventHandler(this.btnASSave_Click);
            // 
            // trvSettings
            // 
            resources.ApplyResources(this.trvSettings, "trvSettings");
            this.trvSettings.AllowDrop = true;
            this.trvSettings.HotTracking = true;
            this.trvSettings.Name = "trvSettings";
            this.trvSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvSettings.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvSettings.Nodes1"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("trvSettings.Nodes2")))});
            this.trvSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // strSettings
            // 
            resources.ApplyResources(this.strSettings, "strSettings");
            this.strSettings.Controls.Add(this.label29);
            this.strSettings.Controls.Add(this.grpButtonSize);
            this.strSettings.Controls.Add(this.btnBrowseFolder);
            this.strSettings.Controls.Add(this.btnFormType);
            this.strSettings.Controls.Add(this.cmbPurchaseType);
            this.strSettings.Controls.Add(this.txtOnlinePaymentLabel);
            this.strSettings.Controls.Add(this.label37);
            this.strSettings.Controls.Add(this.label36);
            this.strSettings.Controls.Add(this.label35);
            this.strSettings.Controls.Add(this.label34);
            this.strSettings.Controls.Add(this.cmbOnlineAccount);
            this.strSettings.Controls.Add(this.txtAddFour);
            this.strSettings.Controls.Add(this.txtAddThree);
            this.strSettings.Controls.Add(this.txtAddTwo);
            this.strSettings.Controls.Add(this.txtAddOne);
            this.strSettings.Controls.Add(this.cmbBankAccount);
            this.strSettings.Controls.Add(this.cmbCashAccount);
            this.strSettings.Controls.Add(this.cmbCurrency);
            this.strSettings.Controls.Add(this.cmbSalesType);
            this.strSettings.Controls.Add(this.txtCurrencyFormat);
            this.strSettings.Controls.Add(this.txtBackupPath);
            this.strSettings.Controls.Add(this.txtQuantityFormat);
            this.strSettings.Controls.Add(this.txtWeightBarcode);
            this.strSettings.Controls.Add(this.txtRateBarcode);
            this.strSettings.Controls.Add(this.txtCessPercentage);
            this.strSettings.Controls.Add(this.txtDateFormat);
            this.strSettings.Controls.Add(this.label19);
            this.strSettings.Controls.Add(this.txtDefaultCompany);
            this.strSettings.Controls.Add(this.label18);
            this.strSettings.Controls.Add(this.label11);
            this.strSettings.Controls.Add(this.label17);
            this.strSettings.Controls.Add(this.groupBox1);
            this.strSettings.Controls.Add(this.label10);
            this.strSettings.Controls.Add(this.label13);
            this.strSettings.Controls.Add(this.label30);
            this.strSettings.Controls.Add(this.label16);
            this.strSettings.Controls.Add(this.label7);
            this.strSettings.Controls.Add(this.label15);
            this.strSettings.Controls.Add(this.label12);
            this.strSettings.Controls.Add(this.label6);
            this.strSettings.Controls.Add(this.label14);
            this.strSettings.Controls.Add(this.label1);
            this.strSettings.Controls.Add(this.btnASSave);
            this.strSettings.Name = "strSettings";
            this.strSettings.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // grpButtonSize
            // 
            resources.ApplyResources(this.grpButtonSize, "grpButtonSize");
            this.grpButtonSize.Controls.Add(this.label28);
            this.grpButtonSize.Controls.Add(this.label27);
            this.grpButtonSize.Controls.Add(this.btnSaveSize);
            this.grpButtonSize.Controls.Add(this.txtProductY);
            this.grpButtonSize.Controls.Add(this.txtProductX);
            this.grpButtonSize.Controls.Add(this.txtGroupY);
            this.grpButtonSize.Controls.Add(this.txtGroupX);
            this.grpButtonSize.Name = "grpButtonSize";
            this.grpButtonSize.TabStop = false;
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // btnSaveSize
            // 
            resources.ApplyResources(this.btnSaveSize, "btnSaveSize");
            this.btnSaveSize.Name = "btnSaveSize";
            this.btnSaveSize.TabStop = false;
            this.btnSaveSize.UseVisualStyleBackColor = true;
            this.btnSaveSize.Click += new System.EventHandler(this.btnSaveSize_Click);
            // 
            // txtProductY
            // 
            resources.ApplyResources(this.txtProductY, "txtProductY");
            this.txtProductY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductY.Data = 0;
            this.txtProductY.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductY.Format = null;
            this.txtProductY.isAllowNegative = false;
            this.txtProductY.isAllowSpecialChar = false;
            this.txtProductY.isNumberOnly = true;
            this.txtProductY.isNumeric = false;
            this.txtProductY.LeaveColor = System.Drawing.Color.White;
            this.txtProductY.Name = "txtProductY";
            this.txtProductY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtProductX
            // 
            resources.ApplyResources(this.txtProductX, "txtProductX");
            this.txtProductX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductX.Data = 0;
            this.txtProductX.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductX.Format = null;
            this.txtProductX.isAllowNegative = false;
            this.txtProductX.isAllowSpecialChar = false;
            this.txtProductX.isNumberOnly = true;
            this.txtProductX.isNumeric = false;
            this.txtProductX.LeaveColor = System.Drawing.Color.White;
            this.txtProductX.Name = "txtProductX";
            this.txtProductX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGroupY
            // 
            resources.ApplyResources(this.txtGroupY, "txtGroupY");
            this.txtGroupY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGroupY.Data = 0;
            this.txtGroupY.EnterColor = System.Drawing.Color.LightPink;
            this.txtGroupY.Format = null;
            this.txtGroupY.isAllowNegative = false;
            this.txtGroupY.isAllowSpecialChar = false;
            this.txtGroupY.isNumberOnly = true;
            this.txtGroupY.isNumeric = false;
            this.txtGroupY.LeaveColor = System.Drawing.Color.White;
            this.txtGroupY.Name = "txtGroupY";
            this.txtGroupY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGroupX
            // 
            resources.ApplyResources(this.txtGroupX, "txtGroupX");
            this.txtGroupX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGroupX.Data = 0;
            this.txtGroupX.EnterColor = System.Drawing.Color.LightPink;
            this.txtGroupX.Format = null;
            this.txtGroupX.isAllowNegative = false;
            this.txtGroupX.isAllowSpecialChar = false;
            this.txtGroupX.isNumberOnly = true;
            this.txtGroupX.isNumeric = false;
            this.txtGroupX.LeaveColor = System.Drawing.Color.White;
            this.txtGroupX.Name = "txtGroupX";
            this.txtGroupX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnBrowseFolder
            // 
            resources.ApplyResources(this.btnBrowseFolder, "btnBrowseFolder");
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.TabStop = false;
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // btnFormType
            // 
            resources.ApplyResources(this.btnFormType, "btnFormType");
            this.btnFormType.Name = "btnFormType";
            this.btnFormType.TabStop = false;
            this.btnFormType.UseVisualStyleBackColor = true;
            this.btnFormType.Click += new System.EventHandler(this.btnFormType_Click);
            // 
            // cmbPurchaseType
            // 
            resources.ApplyResources(this.cmbPurchaseType, "cmbPurchaseType");
            this.cmbPurchaseType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPurchaseType.FormattingEnabled = true;
            this.cmbPurchaseType.LeaveColor = System.Drawing.Color.White;
            this.cmbPurchaseType.Name = "cmbPurchaseType";
            // 
            // txtOnlinePaymentLabel
            // 
            resources.ApplyResources(this.txtOnlinePaymentLabel, "txtOnlinePaymentLabel");
            this.txtOnlinePaymentLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOnlinePaymentLabel.Data = 0;
            this.txtOnlinePaymentLabel.EnterColor = System.Drawing.Color.LightPink;
            this.txtOnlinePaymentLabel.Format = null;
            this.txtOnlinePaymentLabel.isAllowNegative = false;
            this.txtOnlinePaymentLabel.isAllowSpecialChar = false;
            this.txtOnlinePaymentLabel.isNumberOnly = false;
            this.txtOnlinePaymentLabel.isNumeric = false;
            this.txtOnlinePaymentLabel.LeaveColor = System.Drawing.Color.White;
            this.txtOnlinePaymentLabel.Name = "txtOnlinePaymentLabel";
            this.txtOnlinePaymentLabel.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // cmbOnlineAccount
            // 
            resources.ApplyResources(this.cmbOnlineAccount, "cmbOnlineAccount");
            this.cmbOnlineAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbOnlineAccount.FormattingEnabled = true;
            this.cmbOnlineAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbOnlineAccount.Name = "cmbOnlineAccount";
            // 
            // txtAddFour
            // 
            resources.ApplyResources(this.txtAddFour, "txtAddFour");
            this.txtAddFour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddFour.Data = 0;
            this.txtAddFour.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddFour.Format = null;
            this.txtAddFour.isAllowNegative = false;
            this.txtAddFour.isAllowSpecialChar = false;
            this.txtAddFour.isNumberOnly = false;
            this.txtAddFour.isNumeric = false;
            this.txtAddFour.LeaveColor = System.Drawing.Color.White;
            this.txtAddFour.Name = "txtAddFour";
            this.txtAddFour.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddThree
            // 
            resources.ApplyResources(this.txtAddThree, "txtAddThree");
            this.txtAddThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddThree.Data = 0;
            this.txtAddThree.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddThree.Format = null;
            this.txtAddThree.isAllowNegative = false;
            this.txtAddThree.isAllowSpecialChar = false;
            this.txtAddThree.isNumberOnly = false;
            this.txtAddThree.isNumeric = false;
            this.txtAddThree.LeaveColor = System.Drawing.Color.White;
            this.txtAddThree.Name = "txtAddThree";
            this.txtAddThree.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddTwo
            // 
            resources.ApplyResources(this.txtAddTwo, "txtAddTwo");
            this.txtAddTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddTwo.Data = 0;
            this.txtAddTwo.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddTwo.Format = null;
            this.txtAddTwo.isAllowNegative = false;
            this.txtAddTwo.isAllowSpecialChar = false;
            this.txtAddTwo.isNumberOnly = false;
            this.txtAddTwo.isNumeric = false;
            this.txtAddTwo.LeaveColor = System.Drawing.Color.White;
            this.txtAddTwo.Name = "txtAddTwo";
            this.txtAddTwo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddOne
            // 
            resources.ApplyResources(this.txtAddOne, "txtAddOne");
            this.txtAddOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddOne.Data = 0;
            this.txtAddOne.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddOne.Format = null;
            this.txtAddOne.isAllowNegative = false;
            this.txtAddOne.isAllowSpecialChar = false;
            this.txtAddOne.isNumberOnly = false;
            this.txtAddOne.isNumeric = false;
            this.txtAddOne.LeaveColor = System.Drawing.Color.White;
            this.txtAddOne.Name = "txtAddOne";
            this.txtAddOne.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbBankAccount
            // 
            resources.ApplyResources(this.cmbBankAccount, "cmbBankAccount");
            this.cmbBankAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbBankAccount.Name = "cmbBankAccount";
            // 
            // cmbCashAccount
            // 
            resources.ApplyResources(this.cmbCashAccount, "cmbCashAccount");
            this.cmbCashAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCashAccount.FormattingEnabled = true;
            this.cmbCashAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbCashAccount.Name = "cmbCashAccount";
            // 
            // cmbCurrency
            // 
            resources.ApplyResources(this.cmbCurrency, "cmbCurrency");
            this.cmbCurrency.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.LeaveColor = System.Drawing.Color.White;
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // cmbSalesType
            // 
            resources.ApplyResources(this.cmbSalesType, "cmbSalesType");
            this.cmbSalesType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSalesType.FormattingEnabled = true;
            this.cmbSalesType.LeaveColor = System.Drawing.Color.White;
            this.cmbSalesType.Name = "cmbSalesType";
            // 
            // txtCurrencyFormat
            // 
            resources.ApplyResources(this.txtCurrencyFormat, "txtCurrencyFormat");
            this.txtCurrencyFormat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrencyFormat.Data = 0;
            this.txtCurrencyFormat.EnterColor = System.Drawing.Color.LightPink;
            this.txtCurrencyFormat.Format = null;
            this.txtCurrencyFormat.isAllowNegative = false;
            this.txtCurrencyFormat.isAllowSpecialChar = false;
            this.txtCurrencyFormat.isNumberOnly = false;
            this.txtCurrencyFormat.isNumeric = false;
            this.txtCurrencyFormat.LeaveColor = System.Drawing.Color.White;
            this.txtCurrencyFormat.Name = "txtCurrencyFormat";
            this.txtCurrencyFormat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtBackupPath
            // 
            resources.ApplyResources(this.txtBackupPath, "txtBackupPath");
            this.txtBackupPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBackupPath.Data = 0;
            this.txtBackupPath.EnterColor = System.Drawing.Color.LightPink;
            this.txtBackupPath.Format = null;
            this.txtBackupPath.isAllowNegative = false;
            this.txtBackupPath.isAllowSpecialChar = false;
            this.txtBackupPath.isNumberOnly = false;
            this.txtBackupPath.isNumeric = false;
            this.txtBackupPath.LeaveColor = System.Drawing.Color.White;
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtQuantityFormat
            // 
            resources.ApplyResources(this.txtQuantityFormat, "txtQuantityFormat");
            this.txtQuantityFormat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantityFormat.Data = 0;
            this.txtQuantityFormat.EnterColor = System.Drawing.Color.LightPink;
            this.txtQuantityFormat.Format = null;
            this.txtQuantityFormat.isAllowNegative = false;
            this.txtQuantityFormat.isAllowSpecialChar = false;
            this.txtQuantityFormat.isNumberOnly = false;
            this.txtQuantityFormat.isNumeric = false;
            this.txtQuantityFormat.LeaveColor = System.Drawing.Color.White;
            this.txtQuantityFormat.Name = "txtQuantityFormat";
            this.txtQuantityFormat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtWeightBarcode
            // 
            resources.ApplyResources(this.txtWeightBarcode, "txtWeightBarcode");
            this.txtWeightBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWeightBarcode.Data = 0;
            this.txtWeightBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtWeightBarcode.Format = null;
            this.txtWeightBarcode.isAllowNegative = false;
            this.txtWeightBarcode.isAllowSpecialChar = false;
            this.txtWeightBarcode.isNumberOnly = false;
            this.txtWeightBarcode.isNumeric = false;
            this.txtWeightBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtWeightBarcode.Name = "txtWeightBarcode";
            this.txtWeightBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtRateBarcode
            // 
            resources.ApplyResources(this.txtRateBarcode, "txtRateBarcode");
            this.txtRateBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRateBarcode.Data = 0;
            this.txtRateBarcode.EnterColor = System.Drawing.Color.LightPink;
            this.txtRateBarcode.Format = null;
            this.txtRateBarcode.isAllowNegative = false;
            this.txtRateBarcode.isAllowSpecialChar = false;
            this.txtRateBarcode.isNumberOnly = false;
            this.txtRateBarcode.isNumeric = false;
            this.txtRateBarcode.LeaveColor = System.Drawing.Color.White;
            this.txtRateBarcode.Name = "txtRateBarcode";
            this.txtRateBarcode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCessPercentage
            // 
            resources.ApplyResources(this.txtCessPercentage, "txtCessPercentage");
            this.txtCessPercentage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCessPercentage.Data = 0;
            this.txtCessPercentage.EnterColor = System.Drawing.Color.LightPink;
            this.txtCessPercentage.Format = null;
            this.txtCessPercentage.isAllowNegative = false;
            this.txtCessPercentage.isAllowSpecialChar = false;
            this.txtCessPercentage.isNumberOnly = false;
            this.txtCessPercentage.isNumeric = false;
            this.txtCessPercentage.LeaveColor = System.Drawing.Color.White;
            this.txtCessPercentage.Name = "txtCessPercentage";
            this.txtCessPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtDateFormat
            // 
            resources.ApplyResources(this.txtDateFormat, "txtDateFormat");
            this.txtDateFormat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDateFormat.Data = 0;
            this.txtDateFormat.EnterColor = System.Drawing.Color.LightPink;
            this.txtDateFormat.Format = null;
            this.txtDateFormat.isAllowNegative = false;
            this.txtDateFormat.isAllowSpecialChar = false;
            this.txtDateFormat.isNumberOnly = false;
            this.txtDateFormat.isNumeric = false;
            this.txtDateFormat.LeaveColor = System.Drawing.Color.White;
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // txtDefaultCompany
            // 
            resources.ApplyResources(this.txtDefaultCompany, "txtDefaultCompany");
            this.txtDefaultCompany.BackColor = System.Drawing.Color.White;
            this.txtDefaultCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDefaultCompany.Data = 0;
            this.txtDefaultCompany.EnterColor = System.Drawing.Color.LightPink;
            this.txtDefaultCompany.Format = null;
            this.txtDefaultCompany.isAllowNegative = false;
            this.txtDefaultCompany.isAllowSpecialChar = false;
            this.txtDefaultCompany.isNumberOnly = false;
            this.txtDefaultCompany.isNumeric = false;
            this.txtDefaultCompany.LeaveColor = System.Drawing.Color.White;
            this.txtDefaultCompany.Name = "txtDefaultCompany";
            this.txtDefaultCompany.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtEmailProvider);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEmailAddress);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtClientPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtEmailProvider
            // 
            resources.ApplyResources(this.txtEmailProvider, "txtEmailProvider");
            this.txtEmailProvider.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmailProvider.Data = 0;
            this.txtEmailProvider.EnterColor = System.Drawing.Color.LightPink;
            this.txtEmailProvider.Format = null;
            this.txtEmailProvider.isAllowNegative = false;
            this.txtEmailProvider.isAllowSpecialChar = false;
            this.txtEmailProvider.isNumberOnly = false;
            this.txtEmailProvider.isNumeric = false;
            this.txtEmailProvider.LeaveColor = System.Drawing.Color.White;
            this.txtEmailProvider.Name = "txtEmailProvider";
            this.txtEmailProvider.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Data = 0;
            this.txtPassword.EnterColor = System.Drawing.Color.LightPink;
            this.txtPassword.Format = null;
            this.txtPassword.isAllowNegative = false;
            this.txtPassword.isAllowSpecialChar = false;
            this.txtPassword.isNumberOnly = false;
            this.txtPassword.isNumeric = false;
            this.txtPassword.LeaveColor = System.Drawing.Color.White;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Data = 0;
            this.txtUserName.EnterColor = System.Drawing.Color.LightPink;
            this.txtUserName.Format = null;
            this.txtUserName.isAllowNegative = false;
            this.txtUserName.isAllowSpecialChar = false;
            this.txtUserName.isNumberOnly = false;
            this.txtUserName.isNumeric = false;
            this.txtUserName.LeaveColor = System.Drawing.Color.White;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtEmailAddress
            // 
            resources.ApplyResources(this.txtEmailAddress, "txtEmailAddress");
            this.txtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmailAddress.Data = 0;
            this.txtEmailAddress.EnterColor = System.Drawing.Color.LightPink;
            this.txtEmailAddress.Format = null;
            this.txtEmailAddress.isAllowNegative = false;
            this.txtEmailAddress.isAllowSpecialChar = false;
            this.txtEmailAddress.isNumberOnly = false;
            this.txtEmailAddress.isNumeric = false;
            this.txtEmailAddress.LeaveColor = System.Drawing.Color.White;
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtClientPort
            // 
            resources.ApplyResources(this.txtClientPort, "txtClientPort");
            this.txtClientPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClientPort.Data = 0;
            this.txtClientPort.EnterColor = System.Drawing.Color.LightPink;
            this.txtClientPort.Format = null;
            this.txtClientPort.isAllowNegative = false;
            this.txtClientPort.isAllowSpecialChar = false;
            this.txtClientPort.isNumberOnly = false;
            this.txtClientPort.isNumeric = false;
            this.txtClientPort.LeaveColor = System.Drawing.Color.White;
            this.txtClientPort.Name = "txtClientPort";
            this.txtClientPort.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtClientName
            // 
            resources.ApplyResources(this.txtClientName, "txtClientName");
            this.txtClientName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClientName.Data = 0;
            this.txtClientName.EnterColor = System.Drawing.Color.LightPink;
            this.txtClientName.Format = null;
            this.txtClientName.isAllowNegative = false;
            this.txtClientName.isAllowSpecialChar = false;
            this.txtClientName.isNumberOnly = false;
            this.txtClientName.isNumeric = false;
            this.txtClientName.LeaveColor = System.Drawing.Color.White;
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // blnSettings
            // 
            resources.ApplyResources(this.blnSettings, "blnSettings");
            this.blnSettings.Controls.Add(this.panel1);
            this.blnSettings.Controls.Add(this.trvSettings);
            this.blnSettings.Controls.Add(this.btnSave);
            this.blnSettings.Name = "blnSettings";
            this.blnSettings.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.dgvSettings);
            this.panel1.Name = "panel1";
            // 
            // dgvSettings
            // 
            resources.ApplyResources(this.dgvSettings, "dgvSettings");
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToDeleteRows = false;
            this.dgvSettings.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSettings.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.RowHeadersVisible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Id";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Caption";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "BitValue";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabSettings
            // 
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Controls.Add(this.blnSettings);
            this.tabSettings.Controls.Add(this.strSettings);
            this.tabSettings.Controls.Add(this.tpColor);
            this.tabSettings.Controls.Add(this.tabPOS);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabSettings.SelectedIndexChanged += new System.EventHandler(this.tabSettings_SelectedIndexChanged);
            // 
            // tpColor
            // 
            resources.ApplyResources(this.tpColor, "tpColor");
            this.tpColor.Controls.Add(this.cmbWghScalePort);
            this.tpColor.Controls.Add(this.label33);
            this.tpColor.Controls.Add(this.txtReportPath);
            this.tpColor.Controls.Add(this.txtGroupCount);
            this.tpColor.Controls.Add(this.txtCounterName);
            this.tpColor.Controls.Add(this.txtProductCount);
            this.tpColor.Controls.Add(this.txtSeqNo);
            this.tpColor.Controls.Add(this.label38);
            this.tpColor.Controls.Add(this.label32);
            this.tpColor.Controls.Add(this.label26);
            this.tpColor.Controls.Add(this.label31);
            this.tpColor.Controls.Add(this.label22);
            this.tpColor.Controls.Add(this.btnCloseCashPath);
            this.tpColor.Controls.Add(this.btn130);
            this.tpColor.Controls.Add(this.btnGroupCount);
            this.tpColor.Controls.Add(this.btnCounter);
            this.tpColor.Controls.Add(this.btn12);
            this.tpColor.Controls.Add(this.btnProCount);
            this.tpColor.Controls.Add(this.btnSalesForm);
            this.tpColor.Controls.Add(this.btnSeqNo);
            this.tpColor.Controls.Add(this.btnWghScale);
            this.tpColor.Controls.Add(this.btnCusDisplay);
            this.tpColor.Controls.Add(this.btnMini);
            this.tpColor.Controls.Add(this.btnCD);
            this.tpColor.Controls.Add(this.btnRdlc);
            this.tpColor.Controls.Add(this.btnPrinter);
            this.tpColor.Controls.Add(this.cmbMini);
            this.tpColor.Controls.Add(this.cmbComPort);
            this.tpColor.Controls.Add(this.cmbCD);
            this.tpColor.Controls.Add(this.label25);
            this.tpColor.Controls.Add(this.cmbRDLC);
            this.tpColor.Controls.Add(this.label24);
            this.tpColor.Controls.Add(this.cmbSalesForm);
            this.tpColor.Controls.Add(this.cmbTerminalPrinter);
            this.tpColor.Controls.Add(this.label23);
            this.tpColor.Controls.Add(this.label21);
            this.tpColor.Controls.Add(this.label39);
            this.tpColor.Controls.Add(this.label20);
            this.tpColor.Controls.Add(this.btnForeColor);
            this.tpColor.Controls.Add(this.btnBackColor);
            this.tpColor.Name = "tpColor";
            this.tpColor.UseVisualStyleBackColor = true;
            // 
            // cmbWghScalePort
            // 
            resources.ApplyResources(this.cmbWghScalePort, "cmbWghScalePort");
            this.cmbWghScalePort.EnterColor = System.Drawing.Color.LightPink;
            this.cmbWghScalePort.FormattingEnabled = true;
            this.cmbWghScalePort.LeaveColor = System.Drawing.Color.White;
            this.cmbWghScalePort.Name = "cmbWghScalePort";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // txtReportPath
            // 
            resources.ApplyResources(this.txtReportPath, "txtReportPath");
            this.txtReportPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReportPath.Data = 0;
            this.txtReportPath.EnterColor = System.Drawing.Color.LightPink;
            this.txtReportPath.Format = null;
            this.txtReportPath.isAllowNegative = false;
            this.txtReportPath.isAllowSpecialChar = false;
            this.txtReportPath.isNumberOnly = false;
            this.txtReportPath.isNumeric = false;
            this.txtReportPath.LeaveColor = System.Drawing.Color.White;
            this.txtReportPath.Name = "txtReportPath";
            this.txtReportPath.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGroupCount
            // 
            resources.ApplyResources(this.txtGroupCount, "txtGroupCount");
            this.txtGroupCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGroupCount.Data = 9;
            this.txtGroupCount.EnterColor = System.Drawing.Color.LightPink;
            this.txtGroupCount.Format = null;
            this.txtGroupCount.isAllowNegative = false;
            this.txtGroupCount.isAllowSpecialChar = false;
            this.txtGroupCount.isNumberOnly = true;
            this.txtGroupCount.isNumeric = false;
            this.txtGroupCount.LeaveColor = System.Drawing.Color.White;
            this.txtGroupCount.Name = "txtGroupCount";
            this.txtGroupCount.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // txtCounterName
            // 
            resources.ApplyResources(this.txtCounterName, "txtCounterName");
            this.txtCounterName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCounterName.Data = 0;
            this.txtCounterName.EnterColor = System.Drawing.Color.LightPink;
            this.txtCounterName.Format = null;
            this.txtCounterName.isAllowNegative = false;
            this.txtCounterName.isAllowSpecialChar = false;
            this.txtCounterName.isNumberOnly = false;
            this.txtCounterName.isNumeric = false;
            this.txtCounterName.LeaveColor = System.Drawing.Color.White;
            this.txtCounterName.Name = "txtCounterName";
            this.txtCounterName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtProductCount
            // 
            resources.ApplyResources(this.txtProductCount, "txtProductCount");
            this.txtProductCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductCount.Data = 56;
            this.txtProductCount.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductCount.Format = null;
            this.txtProductCount.isAllowNegative = false;
            this.txtProductCount.isAllowSpecialChar = false;
            this.txtProductCount.isNumberOnly = true;
            this.txtProductCount.isNumeric = false;
            this.txtProductCount.LeaveColor = System.Drawing.Color.White;
            this.txtProductCount.Name = "txtProductCount";
            this.txtProductCount.Value = new decimal(new int[] {
            56,
            0,
            0,
            0});
            // 
            // txtSeqNo
            // 
            resources.ApplyResources(this.txtSeqNo, "txtSeqNo");
            this.txtSeqNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeqNo.Data = 1;
            this.txtSeqNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtSeqNo.Format = null;
            this.txtSeqNo.isAllowNegative = false;
            this.txtSeqNo.isAllowSpecialChar = false;
            this.txtSeqNo.isNumberOnly = true;
            this.txtSeqNo.isNumeric = false;
            this.txtSeqNo.LeaveColor = System.Drawing.Color.White;
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // btnCloseCashPath
            // 
            resources.ApplyResources(this.btnCloseCashPath, "btnCloseCashPath");
            this.btnCloseCashPath.Name = "btnCloseCashPath";
            this.btnCloseCashPath.TabStop = false;
            this.btnCloseCashPath.UseVisualStyleBackColor = true;
            this.btnCloseCashPath.Click += new System.EventHandler(this.btnCloseCashPath_Click);
            // 
            // btn130
            // 
            resources.ApplyResources(this.btn130, "btn130");
            this.btn130.BackColor = System.Drawing.Color.Blue;
            this.btn130.FlatAppearance.BorderSize = 0;
            this.btn130.ForeColor = System.Drawing.Color.White;
            this.btn130.Name = "btn130";
            this.btn130.TabStop = false;
            this.btn130.UseVisualStyleBackColor = false;
            this.btn130.Click += new System.EventHandler(this.btn12_Click);
            // 
            // btnGroupCount
            // 
            resources.ApplyResources(this.btnGroupCount, "btnGroupCount");
            this.btnGroupCount.Name = "btnGroupCount";
            this.btnGroupCount.TabStop = false;
            this.btnGroupCount.UseVisualStyleBackColor = true;
            this.btnGroupCount.Click += new System.EventHandler(this.btnGroupCount_Click);
            // 
            // btnCounter
            // 
            resources.ApplyResources(this.btnCounter, "btnCounter");
            this.btnCounter.Name = "btnCounter";
            this.btnCounter.TabStop = false;
            this.btnCounter.UseVisualStyleBackColor = true;
            this.btnCounter.Click += new System.EventHandler(this.btnCounter_Click);
            // 
            // btn12
            // 
            resources.ApplyResources(this.btn12, "btn12");
            this.btn12.BackColor = System.Drawing.Color.Blue;
            this.btn12.FlatAppearance.BorderSize = 0;
            this.btn12.ForeColor = System.Drawing.Color.White;
            this.btn12.Name = "btn12";
            this.btn12.TabStop = false;
            this.btn12.UseVisualStyleBackColor = false;
            this.btn12.Click += new System.EventHandler(this.btn12_Click);
            // 
            // btnProCount
            // 
            resources.ApplyResources(this.btnProCount, "btnProCount");
            this.btnProCount.Name = "btnProCount";
            this.btnProCount.TabStop = false;
            this.btnProCount.UseVisualStyleBackColor = true;
            this.btnProCount.Click += new System.EventHandler(this.btnProCount_Click);
            // 
            // btnSalesForm
            // 
            resources.ApplyResources(this.btnSalesForm, "btnSalesForm");
            this.btnSalesForm.Name = "btnSalesForm";
            this.btnSalesForm.TabStop = false;
            this.btnSalesForm.UseVisualStyleBackColor = true;
            this.btnSalesForm.Click += new System.EventHandler(this.btnSalesForm_Click);
            // 
            // btnSeqNo
            // 
            resources.ApplyResources(this.btnSeqNo, "btnSeqNo");
            this.btnSeqNo.Name = "btnSeqNo";
            this.btnSeqNo.TabStop = false;
            this.btnSeqNo.UseVisualStyleBackColor = true;
            this.btnSeqNo.Click += new System.EventHandler(this.btnSeqNo_Click);
            // 
            // btnWghScale
            // 
            resources.ApplyResources(this.btnWghScale, "btnWghScale");
            this.btnWghScale.Name = "btnWghScale";
            this.btnWghScale.TabStop = false;
            this.btnWghScale.UseVisualStyleBackColor = true;
            this.btnWghScale.Click += new System.EventHandler(this.btnWghScale_Click);
            // 
            // btnCusDisplay
            // 
            resources.ApplyResources(this.btnCusDisplay, "btnCusDisplay");
            this.btnCusDisplay.Name = "btnCusDisplay";
            this.btnCusDisplay.TabStop = false;
            this.btnCusDisplay.UseVisualStyleBackColor = true;
            this.btnCusDisplay.Click += new System.EventHandler(this.btnCusDisplay_Click);
            // 
            // btnMini
            // 
            resources.ApplyResources(this.btnMini, "btnMini");
            this.btnMini.Name = "btnMini";
            this.btnMini.TabStop = false;
            this.btnMini.UseVisualStyleBackColor = true;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // btnCD
            // 
            resources.ApplyResources(this.btnCD, "btnCD");
            this.btnCD.Name = "btnCD";
            this.btnCD.TabStop = false;
            this.btnCD.UseVisualStyleBackColor = true;
            this.btnCD.Click += new System.EventHandler(this.btnCD_Click);
            // 
            // btnRdlc
            // 
            resources.ApplyResources(this.btnRdlc, "btnRdlc");
            this.btnRdlc.Name = "btnRdlc";
            this.btnRdlc.TabStop = false;
            this.btnRdlc.UseVisualStyleBackColor = true;
            this.btnRdlc.Click += new System.EventHandler(this.btnRdlc_Click);
            // 
            // btnPrinter
            // 
            resources.ApplyResources(this.btnPrinter, "btnPrinter");
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.TabStop = false;
            this.btnPrinter.UseVisualStyleBackColor = true;
            this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click);
            // 
            // cmbMini
            // 
            resources.ApplyResources(this.cmbMini, "cmbMini");
            this.cmbMini.EnterColor = System.Drawing.Color.LightPink;
            this.cmbMini.FormattingEnabled = true;
            this.cmbMini.LeaveColor = System.Drawing.Color.White;
            this.cmbMini.Name = "cmbMini";
            // 
            // cmbComPort
            // 
            resources.ApplyResources(this.cmbComPort, "cmbComPort");
            this.cmbComPort.EnterColor = System.Drawing.Color.LightPink;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.LeaveColor = System.Drawing.Color.White;
            this.cmbComPort.Name = "cmbComPort";
            // 
            // cmbCD
            // 
            resources.ApplyResources(this.cmbCD, "cmbCD");
            this.cmbCD.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCD.FormattingEnabled = true;
            this.cmbCD.LeaveColor = System.Drawing.Color.White;
            this.cmbCD.Name = "cmbCD";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // cmbRDLC
            // 
            resources.ApplyResources(this.cmbRDLC, "cmbRDLC");
            this.cmbRDLC.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRDLC.FormattingEnabled = true;
            this.cmbRDLC.LeaveColor = System.Drawing.Color.White;
            this.cmbRDLC.Name = "cmbRDLC";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // cmbSalesForm
            // 
            resources.ApplyResources(this.cmbSalesForm, "cmbSalesForm");
            this.cmbSalesForm.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSalesForm.FormattingEnabled = true;
            this.cmbSalesForm.LeaveColor = System.Drawing.Color.White;
            this.cmbSalesForm.Name = "cmbSalesForm";
            // 
            // cmbTerminalPrinter
            // 
            resources.ApplyResources(this.cmbTerminalPrinter, "cmbTerminalPrinter");
            this.cmbTerminalPrinter.EnterColor = System.Drawing.Color.LightPink;
            this.cmbTerminalPrinter.FormattingEnabled = true;
            this.cmbTerminalPrinter.LeaveColor = System.Drawing.Color.White;
            this.cmbTerminalPrinter.Name = "cmbTerminalPrinter";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // btnForeColor
            // 
            resources.ApplyResources(this.btnForeColor, "btnForeColor");
            this.btnForeColor.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnForeColor.FlatAppearance.BorderSize = 0;
            this.btnForeColor.ForeColor = System.Drawing.Color.White;
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.UseVisualStyleBackColor = false;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // btnBackColor
            // 
            resources.ApplyResources(this.btnBackColor, "btnBackColor");
            this.btnBackColor.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBackColor.FlatAppearance.BorderSize = 0;
            this.btnBackColor.ForeColor = System.Drawing.Color.White;
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.UseVisualStyleBackColor = false;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // tabPOS
            // 
            resources.ApplyResources(this.tabPOS, "tabPOS");
            this.tabPOS.Controls.Add(this.btnDefault);
            this.tabPOS.Controls.Add(this.groupBox7);
            this.tabPOS.Controls.Add(this.groupBox5);
            this.tabPOS.Controls.Add(this.groupBox4);
            this.tabPOS.Controls.Add(this.groupBox3);
            this.tabPOS.Controls.Add(this.groupBox2);
            this.tabPOS.Name = "tabPOS";
            this.tabPOS.UseVisualStyleBackColor = true;
            // 
            // btnDefault
            // 
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.BackColor = System.Drawing.Color.LightGray;
            this.groupBox7.Controls.Add(this.btnOtherF);
            this.groupBox7.Controls.Add(this.btnOtherB);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // btnOtherF
            // 
            resources.ApplyResources(this.btnOtherF, "btnOtherF");
            this.btnOtherF.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOtherF.FlatAppearance.BorderSize = 0;
            this.btnOtherF.ForeColor = System.Drawing.Color.White;
            this.btnOtherF.Name = "btnOtherF";
            this.btnOtherF.Tag = "OBF";
            this.btnOtherF.UseVisualStyleBackColor = false;
            this.btnOtherF.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnOtherB
            // 
            resources.ApplyResources(this.btnOtherB, "btnOtherB");
            this.btnOtherB.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOtherB.FlatAppearance.BorderSize = 0;
            this.btnOtherB.ForeColor = System.Drawing.Color.White;
            this.btnOtherB.Name = "btnOtherB";
            this.btnOtherB.Tag = "OBB";
            this.btnOtherB.UseVisualStyleBackColor = false;
            this.btnOtherB.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.BackColor = System.Drawing.Color.LightGray;
            this.groupBox5.Controls.Add(this.btnLoginFore);
            this.groupBox5.Controls.Add(this.btnLoginBack);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // btnLoginFore
            // 
            resources.ApplyResources(this.btnLoginFore, "btnLoginFore");
            this.btnLoginFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoginFore.FlatAppearance.BorderSize = 0;
            this.btnLoginFore.ForeColor = System.Drawing.Color.White;
            this.btnLoginFore.Name = "btnLoginFore";
            this.btnLoginFore.Tag = "LBF";
            this.btnLoginFore.UseVisualStyleBackColor = false;
            this.btnLoginFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnLoginBack
            // 
            resources.ApplyResources(this.btnLoginBack, "btnLoginBack");
            this.btnLoginBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoginBack.FlatAppearance.BorderSize = 0;
            this.btnLoginBack.ForeColor = System.Drawing.Color.White;
            this.btnLoginBack.Name = "btnLoginBack";
            this.btnLoginBack.Tag = "LBB";
            this.btnLoginBack.UseVisualStyleBackColor = false;
            this.btnLoginBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.BackColor = System.Drawing.Color.LightGray;
            this.groupBox4.Controls.Add(this.btnNumberpadPBC);
            this.groupBox4.Controls.Add(this.btnTop);
            this.groupBox4.Controls.Add(this.btnDgvHeaderBack);
            this.groupBox4.Controls.Add(this.btnSide);
            this.groupBox4.Controls.Add(this.btnDgvHeaderFore);
            this.groupBox4.Controls.Add(this.btnProductPB);
            this.groupBox4.Controls.Add(this.btnCategoryPB);
            this.groupBox4.Controls.Add(this.btnDgvBack);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // btnNumberpadPBC
            // 
            resources.ApplyResources(this.btnNumberpadPBC, "btnNumberpadPBC");
            this.btnNumberpadPBC.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNumberpadPBC.FlatAppearance.BorderSize = 0;
            this.btnNumberpadPBC.ForeColor = System.Drawing.Color.White;
            this.btnNumberpadPBC.Name = "btnNumberpadPBC";
            this.btnNumberpadPBC.Tag = "NPBC";
            this.btnNumberpadPBC.UseVisualStyleBackColor = false;
            this.btnNumberpadPBC.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnTop
            // 
            resources.ApplyResources(this.btnTop, "btnTop");
            this.btnTop.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTop.FlatAppearance.BorderSize = 0;
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Name = "btnTop";
            this.btnTop.Tag = "TPBC";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDgvHeaderBack
            // 
            resources.ApplyResources(this.btnDgvHeaderBack, "btnDgvHeaderBack");
            this.btnDgvHeaderBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDgvHeaderBack.FlatAppearance.BorderSize = 0;
            this.btnDgvHeaderBack.ForeColor = System.Drawing.Color.White;
            this.btnDgvHeaderBack.Name = "btnDgvHeaderBack";
            this.btnDgvHeaderBack.Tag = "LHBC";
            this.btnDgvHeaderBack.UseVisualStyleBackColor = false;
            this.btnDgvHeaderBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSide
            // 
            resources.ApplyResources(this.btnSide, "btnSide");
            this.btnSide.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSide.FlatAppearance.BorderSize = 0;
            this.btnSide.ForeColor = System.Drawing.Color.White;
            this.btnSide.Name = "btnSide";
            this.btnSide.Tag = "SPBC";
            this.btnSide.UseVisualStyleBackColor = false;
            this.btnSide.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDgvHeaderFore
            // 
            resources.ApplyResources(this.btnDgvHeaderFore, "btnDgvHeaderFore");
            this.btnDgvHeaderFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDgvHeaderFore.FlatAppearance.BorderSize = 0;
            this.btnDgvHeaderFore.ForeColor = System.Drawing.Color.White;
            this.btnDgvHeaderFore.Name = "btnDgvHeaderFore";
            this.btnDgvHeaderFore.Tag = "LHFC";
            this.btnDgvHeaderFore.UseVisualStyleBackColor = false;
            this.btnDgvHeaderFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnProductPB
            // 
            resources.ApplyResources(this.btnProductPB, "btnProductPB");
            this.btnProductPB.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProductPB.FlatAppearance.BorderSize = 0;
            this.btnProductPB.ForeColor = System.Drawing.Color.White;
            this.btnProductPB.Name = "btnProductPB";
            this.btnProductPB.Tag = "PPBC";
            this.btnProductPB.UseVisualStyleBackColor = false;
            this.btnProductPB.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnCategoryPB
            // 
            resources.ApplyResources(this.btnCategoryPB, "btnCategoryPB");
            this.btnCategoryPB.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCategoryPB.FlatAppearance.BorderSize = 0;
            this.btnCategoryPB.ForeColor = System.Drawing.Color.White;
            this.btnCategoryPB.Name = "btnCategoryPB";
            this.btnCategoryPB.Tag = "CPBC";
            this.btnCategoryPB.UseVisualStyleBackColor = false;
            this.btnCategoryPB.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDgvBack
            // 
            resources.ApplyResources(this.btnDgvBack, "btnDgvBack");
            this.btnDgvBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDgvBack.FlatAppearance.BorderSize = 0;
            this.btnDgvBack.ForeColor = System.Drawing.Color.White;
            this.btnDgvBack.Name = "btnDgvBack";
            this.btnDgvBack.Tag = "LBC";
            this.btnDgvBack.UseVisualStyleBackColor = false;
            this.btnDgvBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackColor = System.Drawing.Color.LightGray;
            this.groupBox3.Controls.Add(this.btnNumberpadFore);
            this.groupBox3.Controls.Add(this.btnTitleFore);
            this.groupBox3.Controls.Add(this.btnGeneralFore);
            this.groupBox3.Controls.Add(this.btnDetailsPanelFore);
            this.groupBox3.Controls.Add(this.btnSideIconFore);
            this.groupBox3.Controls.Add(this.btnCategoryFore);
            this.groupBox3.Controls.Add(this.btnProductFore);
            this.groupBox3.Controls.Add(this.btnTopButtonFore);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnNumberpadFore
            // 
            resources.ApplyResources(this.btnNumberpadFore, "btnNumberpadFore");
            this.btnNumberpadFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNumberpadFore.FlatAppearance.BorderSize = 0;
            this.btnNumberpadFore.ForeColor = System.Drawing.Color.White;
            this.btnNumberpadFore.Name = "btnNumberpadFore";
            this.btnNumberpadFore.Tag = "NBF";
            this.btnNumberpadFore.UseVisualStyleBackColor = false;
            this.btnNumberpadFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnTitleFore
            // 
            resources.ApplyResources(this.btnTitleFore, "btnTitleFore");
            this.btnTitleFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTitleFore.FlatAppearance.BorderSize = 0;
            this.btnTitleFore.ForeColor = System.Drawing.Color.White;
            this.btnTitleFore.Name = "btnTitleFore";
            this.btnTitleFore.Tag = "TPF";
            this.btnTitleFore.UseVisualStyleBackColor = false;
            this.btnTitleFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnGeneralFore
            // 
            resources.ApplyResources(this.btnGeneralFore, "btnGeneralFore");
            this.btnGeneralFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGeneralFore.FlatAppearance.BorderSize = 0;
            this.btnGeneralFore.ForeColor = System.Drawing.Color.White;
            this.btnGeneralFore.Name = "btnGeneralFore";
            this.btnGeneralFore.Tag = "GBF";
            this.btnGeneralFore.UseVisualStyleBackColor = false;
            this.btnGeneralFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDetailsPanelFore
            // 
            resources.ApplyResources(this.btnDetailsPanelFore, "btnDetailsPanelFore");
            this.btnDetailsPanelFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDetailsPanelFore.FlatAppearance.BorderSize = 0;
            this.btnDetailsPanelFore.ForeColor = System.Drawing.Color.White;
            this.btnDetailsPanelFore.Name = "btnDetailsPanelFore";
            this.btnDetailsPanelFore.Tag = "DPF";
            this.btnDetailsPanelFore.UseVisualStyleBackColor = false;
            this.btnDetailsPanelFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSideIconFore
            // 
            resources.ApplyResources(this.btnSideIconFore, "btnSideIconFore");
            this.btnSideIconFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSideIconFore.FlatAppearance.BorderSize = 0;
            this.btnSideIconFore.ForeColor = System.Drawing.Color.White;
            this.btnSideIconFore.Name = "btnSideIconFore";
            this.btnSideIconFore.Tag = "SBF";
            this.btnSideIconFore.UseVisualStyleBackColor = false;
            this.btnSideIconFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnCategoryFore
            // 
            resources.ApplyResources(this.btnCategoryFore, "btnCategoryFore");
            this.btnCategoryFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCategoryFore.FlatAppearance.BorderSize = 0;
            this.btnCategoryFore.ForeColor = System.Drawing.Color.White;
            this.btnCategoryFore.Name = "btnCategoryFore";
            this.btnCategoryFore.Tag = "CBF";
            this.btnCategoryFore.UseVisualStyleBackColor = false;
            this.btnCategoryFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnProductFore
            // 
            resources.ApplyResources(this.btnProductFore, "btnProductFore");
            this.btnProductFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProductFore.FlatAppearance.BorderSize = 0;
            this.btnProductFore.ForeColor = System.Drawing.Color.White;
            this.btnProductFore.Name = "btnProductFore";
            this.btnProductFore.Tag = "PBF";
            this.btnProductFore.UseVisualStyleBackColor = false;
            this.btnProductFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnTopButtonFore
            // 
            resources.ApplyResources(this.btnTopButtonFore, "btnTopButtonFore");
            this.btnTopButtonFore.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTopButtonFore.FlatAppearance.BorderSize = 0;
            this.btnTopButtonFore.ForeColor = System.Drawing.Color.White;
            this.btnTopButtonFore.Name = "btnTopButtonFore";
            this.btnTopButtonFore.Tag = "TBF";
            this.btnTopButtonFore.UseVisualStyleBackColor = false;
            this.btnTopButtonFore.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.btnNumberpadBack);
            this.groupBox2.Controls.Add(this.btnTitleBack);
            this.groupBox2.Controls.Add(this.btnDetailsPanelBack);
            this.groupBox2.Controls.Add(this.btnCategoryBack);
            this.groupBox2.Controls.Add(this.btnTopButtonBack);
            this.groupBox2.Controls.Add(this.btnProductBack);
            this.groupBox2.Controls.Add(this.btnSideIconBack);
            this.groupBox2.Controls.Add(this.btnGeneralBack);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnNumberpadBack
            // 
            resources.ApplyResources(this.btnNumberpadBack, "btnNumberpadBack");
            this.btnNumberpadBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNumberpadBack.FlatAppearance.BorderSize = 0;
            this.btnNumberpadBack.ForeColor = System.Drawing.Color.White;
            this.btnNumberpadBack.Name = "btnNumberpadBack";
            this.btnNumberpadBack.Tag = "NBB";
            this.btnNumberpadBack.UseVisualStyleBackColor = false;
            this.btnNumberpadBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnTitleBack
            // 
            resources.ApplyResources(this.btnTitleBack, "btnTitleBack");
            this.btnTitleBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTitleBack.FlatAppearance.BorderSize = 0;
            this.btnTitleBack.ForeColor = System.Drawing.Color.White;
            this.btnTitleBack.Name = "btnTitleBack";
            this.btnTitleBack.Tag = "TPB";
            this.btnTitleBack.UseVisualStyleBackColor = false;
            this.btnTitleBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnDetailsPanelBack
            // 
            resources.ApplyResources(this.btnDetailsPanelBack, "btnDetailsPanelBack");
            this.btnDetailsPanelBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDetailsPanelBack.FlatAppearance.BorderSize = 0;
            this.btnDetailsPanelBack.ForeColor = System.Drawing.Color.White;
            this.btnDetailsPanelBack.Name = "btnDetailsPanelBack";
            this.btnDetailsPanelBack.Tag = "DPB";
            this.btnDetailsPanelBack.UseVisualStyleBackColor = false;
            this.btnDetailsPanelBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnCategoryBack
            // 
            resources.ApplyResources(this.btnCategoryBack, "btnCategoryBack");
            this.btnCategoryBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCategoryBack.FlatAppearance.BorderSize = 0;
            this.btnCategoryBack.ForeColor = System.Drawing.Color.White;
            this.btnCategoryBack.Name = "btnCategoryBack";
            this.btnCategoryBack.Tag = "CBB";
            this.btnCategoryBack.UseVisualStyleBackColor = false;
            this.btnCategoryBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnTopButtonBack
            // 
            resources.ApplyResources(this.btnTopButtonBack, "btnTopButtonBack");
            this.btnTopButtonBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTopButtonBack.FlatAppearance.BorderSize = 0;
            this.btnTopButtonBack.ForeColor = System.Drawing.Color.White;
            this.btnTopButtonBack.Name = "btnTopButtonBack";
            this.btnTopButtonBack.Tag = "TBB";
            this.btnTopButtonBack.UseVisualStyleBackColor = false;
            this.btnTopButtonBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnProductBack
            // 
            resources.ApplyResources(this.btnProductBack, "btnProductBack");
            this.btnProductBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnProductBack.FlatAppearance.BorderSize = 0;
            this.btnProductBack.ForeColor = System.Drawing.Color.White;
            this.btnProductBack.Name = "btnProductBack";
            this.btnProductBack.Tag = "PBB";
            this.btnProductBack.UseVisualStyleBackColor = false;
            this.btnProductBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSideIconBack
            // 
            resources.ApplyResources(this.btnSideIconBack, "btnSideIconBack");
            this.btnSideIconBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSideIconBack.FlatAppearance.BorderSize = 0;
            this.btnSideIconBack.ForeColor = System.Drawing.Color.White;
            this.btnSideIconBack.Name = "btnSideIconBack";
            this.btnSideIconBack.Tag = "SBB";
            this.btnSideIconBack.UseVisualStyleBackColor = false;
            this.btnSideIconBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnGeneralBack
            // 
            resources.ApplyResources(this.btnGeneralBack, "btnGeneralBack");
            this.btnGeneralBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGeneralBack.FlatAppearance.BorderSize = 0;
            this.btnGeneralBack.ForeColor = System.Drawing.Color.White;
            this.btnGeneralBack.Name = "btnGeneralBack";
            this.btnGeneralBack.Tag = "GBB";
            this.btnGeneralBack.UseVisualStyleBackColor = false;
            this.btnGeneralBack.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // Application_Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabSettings);
            this.Name = "Application_Settings";
            this.Load += new System.EventHandler(this.Application_Settings_Load);
            this.Controls.SetChildIndex(this.tabSettings, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.button5, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.strSettings.ResumeLayout(false);
            this.strSettings.PerformLayout();
            this.grpButtonSize.ResumeLayout(false);
            this.grpButtonSize.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.blnSettings.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tpColor.ResumeLayout(false);
            this.tpColor.PerformLayout();
            this.tabPOS.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
