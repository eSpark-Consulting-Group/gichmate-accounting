using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class CompanyDetails : BaseForm
	{
		private DBContext db;

		private int iCompany;

		private string sProductSerial;

		private Company company = new Company();

		private IContainer components = null;

		private AshTextBoxNormal txtCountry;

		private LinkLabel linkRemove;

		private LinkLabel linkAdd;

		private PictureBox pbLogo;

		private AshComboBox cmbType;

		private Label label13;

		private AshTextBoxNormal txtEmail;

		private Label label12;

		private AshTextBoxNormal txtWebsite;

		private Label label11;

		private AshTextBoxNormal txtFax;

		private Label label10;

		private AshTextBoxNormal txtMobbNo;

		private Label label9;

		private AshTextBoxNormal txtPhNo;

		private Label label8;

		private Label label7;

		private AshTextBoxNormal txtPinCode;

		private Label label6;

		private AshTextBoxNormal txtAddress;

		private Label label5;

		private AshTextBoxNormal txtPlace;

		private Label label4;

		private AshTextBoxNormal txtCompName;

		private Label label2;

		private Button btnClear;

		private Button btnSave;

		private ToolTip toolTip1;

		private Label label1;

		private AshTextBoxNormal txtState;

		private AshTextBoxNormal txtBank;

		private AshTextBoxNormal txtAccNo;

		private Label label18;

		private AshTextBoxNormal txtIFSC;

		private Label label17;

		private AshTextBoxNormal txtBranch;

		private Label label16;

		private Label label14;

		private Label label15;

		private AshTextBoxNormal txtCompanyCode;

		private Label label3;

		private AshTextBoxNormal txtGST;

		private Label label19;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem saveUpdateToolStripMenuItem;

		private ToolStripMenuItem clearToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem addImageToolStripMenuItem;

		private ToolStripMenuItem removeImageToolStripMenuItem;

		private ToolStripMenuItem companyNameToolStripMenuItem;

		private Label label21;

		private AshGradientPanel gpTest;

		private ErrorProvider errorProvider;

		private Label label20;

		private AshTextBoxNormal txtLocalName;

		private AshTextBoxNormal txtStateCode;

		private Label label22;

		public UserPrevilegeDtl plgDtls { get; set; }

		public CompanyDetails()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void EntityLoadMethod()
		{
			company.Logo = ((pbLogo.Image == null) ? null : AniHelper.imageToByteArray(pbLogo.Image));
			company.InstituteId = iCompany;
			company.InstituteName = txtCompName.Text;
			company.LocalName = txtLocalName.Text;
			company.Place = txtPlace.Text;
			company.Address = txtAddress.Text;
			company.Country = txtCountry.Text;
			company.Pincod = txtPinCode.Text;
			company.PhoneNo = txtPhNo.Text;
			company.MobNo = txtMobbNo.Text;
			company.FaxNo = txtFax.Text;
			company.website = txtWebsite.Text;
			company.EmailId = txtEmail.Text;
			company.Sector = cmbType.Text;
			company.State = txtState.Text;
			company.StateCode = txtStateCode.Data;
			company.GSTNo = txtGST.Text;
			company.BankName = txtBank.Text;
			company.BankBranch = txtBranch.Text;
			company.IFSC = txtIFSC.Text;
			company.Productserial = sProductSerial;
			company.AccNo = txtAccNo.Text;
			company.CompanyId = txtCompanyCode.Text;
		}

		private void AddImageMethode()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			try
			{
				openFileDialog.Title = "Open Image";
				openFileDialog.Filter = "All Picture Files(*.jpg;*.png;*.ico;*gif;*.bmp;*.jpeg)|*.jpg";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					pbLogo.Image = Image.FromFile(openFileDialog.FileName);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			finally
			{
				openFileDialog.Dispose();
			}
		}

		private void LoadTextBoxes()
		{
			try
			{
				company = db.Companies().FirstOrDefault();
				if (company != null)
				{
					iCompany = company.InstituteId.Value;
					txtCompName.Text = company.InstituteName;
					txtLocalName.Text = company.LocalName;
					txtPlace.Text = company.Place;
					txtAddress.Text = company.Address;
					txtCountry.Text = company.Country;
					txtPinCode.Text = company.Pincod;
					txtPhNo.Text = company.PhoneNo;
					txtMobbNo.Text = company.MobNo;
					txtFax.Text = company.FaxNo;
					txtWebsite.Text = company.website;
					sProductSerial = company.Productserial;
					txtEmail.Text = company.EmailId;
					cmbType.Text = company.Sector;
					if (company.Logo != null)
					{
						pbLogo.Image = AniHelper.byteArrayToImage(company.Logo);
					}
					txtGST.Text = company.GSTNo;
					txtState.Text = company.State;
					txtStateCode.Text = company.StateCode.ToString();
					txtBank.Text = company.BankName;
					txtBranch.Text = company.BankBranch;
					txtIFSC.Text = company.IFSC;
					txtAccNo.Text = company.AccNo;
					txtCompanyCode.Text = company.CompanyId;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void CompanyDetails_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			LoadTextBoxes();
			txtCompName.Focus();
		}

		private void CompanyDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				AddImageMethode();
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				pbLogo.Image = null;
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				txtCompName.Focus();
				if (txtCompName.Text != string.Empty)
				{
					txtCompName.SelectAll();
				}
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.F3 && e.Alt)
			{
				e.SuppressKeyPress = true;
				CompanyDetails_Load(null, null);
			}
			else if (e.KeyCode == Keys.F3)
			{
				e.SuppressKeyPress = true;
				company.LogoPath = AniHelper.InputBoxMethod("Connection", "Database Connection", "Connection :", isPassword: false, company.LogoPath);
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			pbLogo.Image = null;
			txtCompName.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtCompName.Text == string.Empty)
			{
				errorProvider.SetError(txtCompName, "Company Name Cannot be null");
				return;
			}
			try
			{
				EntityLoadMethod();
				if (db.Companies().Count == 0)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(company, "Institute"))
					{
						Messages.SavedMessage();
						btnSave.Text = "Update";
						txtCompName.Focus();
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(company, "Institute", "InstituteId", iCompany))
				{
					Messages.UpdateMessage();
					txtCompName.Focus();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void linkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			AddImageMethode();
		}

		private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			pbLogo.Image = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanyDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBank = new AshUsercontrol.AshTextBoxNormal();
            this.linkRemove = new System.Windows.Forms.LinkLabel();
            this.linkAdd = new System.Windows.Forms.LinkLabel();
            this.txtAccNo = new AshUsercontrol.AshTextBoxNormal();
            this.label18 = new System.Windows.Forms.Label();
            this.txtIFSC = new AshUsercontrol.AshTextBoxNormal();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBranch = new AshUsercontrol.AshTextBoxNormal();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtState = new AshUsercontrol.AshTextBoxNormal();
            this.txtCountry = new AshUsercontrol.AshTextBoxNormal();
            this.cmbType = new AshUsercontrol.AshComboBox();
            this.txtEmail = new AshUsercontrol.AshTextBoxNormal();
            this.txtWebsite = new AshUsercontrol.AshTextBoxNormal();
            this.txtGST = new AshUsercontrol.AshTextBoxNormal();
            this.txtFax = new AshUsercontrol.AshTextBoxNormal();
            this.txtMobbNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtPhNo = new AshUsercontrol.AshTextBoxNormal();
            this.txtCompanyCode = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPinCode = new AshUsercontrol.AshTextBoxNormal();
            this.txtAddress = new AshUsercontrol.AshTextBoxNormal();
            this.txtPlace = new AshUsercontrol.AshTextBoxNormal();
            this.txtCompName = new AshUsercontrol.AshTextBoxNormal();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label21 = new System.Windows.Forms.Label();
            this.gpTest = new AshUsercontrol.AshGradientPanel();
            this.txtLocalName = new AshUsercontrol.AshTextBoxNormal();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtStateCode = new AshUsercontrol.AshTextBoxNormal();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnEnd, "btnEnd");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Name = "label1";
            // 
            // txtBank
            // 
            this.txtBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBank.Data = 0;
            this.txtBank.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtBank, "txtBank");
            this.txtBank.Format = null;
            this.txtBank.isAllowNegative = false;
            this.txtBank.isAllowSpecialChar = false;
            this.txtBank.isNumberOnly = false;
            this.txtBank.isNumeric = false;
            this.txtBank.LeaveColor = System.Drawing.Color.White;
            this.txtBank.Name = "txtBank";
            this.txtBank.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // linkRemove
            // 
            resources.ApplyResources(this.linkRemove, "linkRemove");
            this.linkRemove.Name = "linkRemove";
            this.linkRemove.TabStop = true;
            this.toolTip1.SetToolTip(this.linkRemove, resources.GetString("linkRemove.ToolTip"));
            this.linkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemove_LinkClicked);
            // 
            // linkAdd
            // 
            resources.ApplyResources(this.linkAdd, "linkAdd");
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.TabStop = true;
            this.toolTip1.SetToolTip(this.linkAdd, resources.GetString("linkAdd.ToolTip"));
            this.linkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAdd_LinkClicked);
            // 
            // txtAccNo
            // 
            this.txtAccNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAccNo.Data = 0;
            this.txtAccNo.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtAccNo, "txtAccNo");
            this.txtAccNo.Format = null;
            this.txtAccNo.isAllowNegative = false;
            this.txtAccNo.isAllowSpecialChar = false;
            this.txtAccNo.isNumberOnly = false;
            this.txtAccNo.isNumeric = false;
            this.txtAccNo.LeaveColor = System.Drawing.Color.White;
            this.txtAccNo.Name = "txtAccNo";
            this.txtAccNo.Value = new decimal(new int[] {
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
            // txtIFSC
            // 
            this.txtIFSC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIFSC.Data = 0;
            this.txtIFSC.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtIFSC, "txtIFSC");
            this.txtIFSC.Format = null;
            this.txtIFSC.isAllowNegative = false;
            this.txtIFSC.isAllowSpecialChar = false;
            this.txtIFSC.isNumberOnly = false;
            this.txtIFSC.isNumeric = false;
            this.txtIFSC.LeaveColor = System.Drawing.Color.White;
            this.txtIFSC.Name = "txtIFSC";
            this.txtIFSC.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtBranch
            // 
            this.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBranch.Data = 0;
            this.txtBranch.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtBranch, "txtBranch");
            this.txtBranch.Format = null;
            this.txtBranch.isAllowNegative = false;
            this.txtBranch.isAllowSpecialChar = false;
            this.txtBranch.isNumberOnly = false;
            this.txtBranch.isNumeric = false;
            this.txtBranch.LeaveColor = System.Drawing.Color.White;
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtState
            // 
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtState.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtState.Data = 0;
            this.txtState.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.Format = null;
            this.txtState.isAllowNegative = false;
            this.txtState.isAllowSpecialChar = false;
            this.txtState.isNumberOnly = false;
            this.txtState.isNumeric = false;
            this.txtState.LeaveColor = System.Drawing.Color.White;
            this.txtState.Name = "txtState";
            this.txtState.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCountry
            // 
            this.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCountry.Data = 0;
            this.txtCountry.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Format = null;
            this.txtCountry.isAllowNegative = false;
            this.txtCountry.isAllowSpecialChar = false;
            this.txtCountry.isNumberOnly = false;
            this.txtCountry.isNumeric = false;
            this.txtCountry.LeaveColor = System.Drawing.Color.White;
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbType
            // 
            this.cmbType.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            resources.GetString("cmbType.Items"),
            resources.GetString("cmbType.Items1"),
            resources.GetString("cmbType.Items2"),
            resources.GetString("cmbType.Items3")});
            this.cmbType.LeaveColor = System.Drawing.Color.White;
            this.cmbType.Name = "cmbType";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Data = 0;
            this.txtEmail.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Format = null;
            this.txtEmail.isAllowNegative = false;
            this.txtEmail.isAllowSpecialChar = false;
            this.txtEmail.isNumberOnly = false;
            this.txtEmail.isNumeric = false;
            this.txtEmail.LeaveColor = System.Drawing.Color.White;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtWebsite
            // 
            this.txtWebsite.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWebsite.Data = 0;
            this.txtWebsite.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtWebsite, "txtWebsite");
            this.txtWebsite.Format = null;
            this.txtWebsite.isAllowNegative = false;
            this.txtWebsite.isAllowSpecialChar = false;
            this.txtWebsite.isNumberOnly = false;
            this.txtWebsite.isNumeric = false;
            this.txtWebsite.LeaveColor = System.Drawing.Color.White;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtGST
            // 
            this.txtGST.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGST.Data = 0;
            this.txtGST.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtGST, "txtGST");
            this.txtGST.Format = null;
            this.txtGST.isAllowNegative = false;
            this.txtGST.isAllowSpecialChar = false;
            this.txtGST.isNumberOnly = false;
            this.txtGST.isNumeric = false;
            this.txtGST.LeaveColor = System.Drawing.Color.White;
            this.txtGST.Name = "txtGST";
            this.txtGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtFax
            // 
            this.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFax.Data = 0;
            this.txtFax.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtFax, "txtFax");
            this.txtFax.Format = null;
            this.txtFax.isAllowNegative = false;
            this.txtFax.isAllowSpecialChar = false;
            this.txtFax.isNumberOnly = false;
            this.txtFax.isNumeric = false;
            this.txtFax.LeaveColor = System.Drawing.Color.White;
            this.txtFax.Name = "txtFax";
            this.txtFax.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtMobbNo
            // 
            this.txtMobbNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMobbNo.Data = 0;
            this.txtMobbNo.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtMobbNo, "txtMobbNo");
            this.txtMobbNo.Format = null;
            this.txtMobbNo.isAllowNegative = false;
            this.txtMobbNo.isAllowSpecialChar = false;
            this.txtMobbNo.isNumberOnly = false;
            this.txtMobbNo.isNumeric = false;
            this.txtMobbNo.LeaveColor = System.Drawing.Color.White;
            this.txtMobbNo.Name = "txtMobbNo";
            this.txtMobbNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPhNo
            // 
            this.txtPhNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhNo.Data = 0;
            this.txtPhNo.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtPhNo, "txtPhNo");
            this.txtPhNo.Format = null;
            this.txtPhNo.isAllowNegative = false;
            this.txtPhNo.isAllowSpecialChar = false;
            this.txtPhNo.isNumberOnly = false;
            this.txtPhNo.isNumeric = false;
            this.txtPhNo.LeaveColor = System.Drawing.Color.White;
            this.txtPhNo.Name = "txtPhNo";
            this.txtPhNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCompanyCode
            // 
            this.txtCompanyCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCompanyCode.Data = 0;
            this.txtCompanyCode.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtCompanyCode, "txtCompanyCode");
            this.txtCompanyCode.Format = null;
            this.txtCompanyCode.isAllowNegative = false;
            this.txtCompanyCode.isAllowSpecialChar = false;
            this.txtCompanyCode.isNumberOnly = false;
            this.txtCompanyCode.isNumeric = false;
            this.txtCompanyCode.LeaveColor = System.Drawing.Color.White;
            this.txtCompanyCode.Name = "txtCompanyCode";
            this.txtCompanyCode.Value = new decimal(new int[] {
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
            // txtPinCode
            // 
            this.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPinCode.Data = 0;
            this.txtPinCode.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtPinCode, "txtPinCode");
            this.txtPinCode.Format = null;
            this.txtPinCode.isAllowNegative = false;
            this.txtPinCode.isAllowSpecialChar = false;
            this.txtPinCode.isNumberOnly = false;
            this.txtPinCode.isNumeric = false;
            this.txtPinCode.LeaveColor = System.Drawing.Color.White;
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.Data = 0;
            this.txtAddress.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Format = null;
            this.txtAddress.isAllowNegative = false;
            this.txtAddress.isAllowSpecialChar = false;
            this.txtAddress.isNumberOnly = false;
            this.txtAddress.isNumeric = false;
            this.txtAddress.LeaveColor = System.Drawing.Color.White;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPlace
            // 
            this.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlace.Data = 0;
            this.txtPlace.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtPlace, "txtPlace");
            this.txtPlace.Format = null;
            this.txtPlace.isAllowNegative = false;
            this.txtPlace.isAllowSpecialChar = false;
            this.txtPlace.isNumberOnly = false;
            this.txtPlace.isNumeric = false;
            this.txtPlace.LeaveColor = System.Drawing.Color.White;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCompName
            // 
            this.txtCompName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCompName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCompName.Data = 0;
            this.txtCompName.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtCompName, "txtCompName");
            this.txtCompName.Format = null;
            this.txtCompName.isAllowNegative = false;
            this.txtCompName.isAllowSpecialChar = false;
            this.txtCompName.isNumberOnly = false;
            this.txtCompName.isNumeric = false;
            this.txtCompName.LeaveColor = System.Drawing.Color.White;
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.toolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveUpdateToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.addImageToolStripMenuItem,
            this.removeImageToolStripMenuItem,
            this.companyNameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // saveUpdateToolStripMenuItem
            // 
            this.saveUpdateToolStripMenuItem.Name = "saveUpdateToolStripMenuItem";
            resources.ApplyResources(this.saveUpdateToolStripMenuItem, "saveUpdateToolStripMenuItem");
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            resources.ApplyResources(this.addImageToolStripMenuItem, "addImageToolStripMenuItem");
            // 
            // removeImageToolStripMenuItem
            // 
            this.removeImageToolStripMenuItem.Name = "removeImageToolStripMenuItem";
            resources.ApplyResources(this.removeImageToolStripMenuItem, "removeImageToolStripMenuItem");
            // 
            // companyNameToolStripMenuItem
            // 
            this.companyNameToolStripMenuItem.Name = "companyNameToolStripMenuItem";
            resources.ApplyResources(this.companyNameToolStripMenuItem, "companyNameToolStripMenuItem");
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // gpTest
            // 
            this.gpTest.AllowMultiSelect = false;
            this.gpTest.Angle = 0F;
            this.gpTest.BackColor = System.Drawing.Color.DodgerBlue;
            this.gpTest.BottomColor = System.Drawing.Color.DodgerBlue;
            resources.ApplyResources(this.gpTest, "gpTest");
            this.gpTest.Name = "gpTest";
            this.gpTest.Selected = false;
            this.gpTest.TextAdjestmentHeight = 0;
            this.gpTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gpTest.TopColor = System.Drawing.Color.White;
            // 
            // txtLocalName
            // 
            this.txtLocalName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalName.Data = 0;
            this.txtLocalName.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtLocalName, "txtLocalName");
            this.txtLocalName.Format = null;
            this.txtLocalName.isAllowNegative = false;
            this.txtLocalName.isAllowSpecialChar = false;
            this.txtLocalName.isNumberOnly = false;
            this.txtLocalName.isNumeric = false;
            this.txtLocalName.LeaveColor = System.Drawing.Color.White;
            this.txtLocalName.Name = "txtLocalName";
            this.txtLocalName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // txtStateCode
            // 
            this.txtStateCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStateCode.Data = 0;
            this.txtStateCode.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtStateCode, "txtStateCode");
            this.txtStateCode.Format = null;
            this.txtStateCode.isAllowNegative = false;
            this.txtStateCode.isAllowSpecialChar = false;
            this.txtStateCode.isNumberOnly = false;
            this.txtStateCode.isNumeric = false;
            this.txtStateCode.LeaveColor = System.Drawing.Color.White;
            this.txtStateCode.Name = "txtStateCode";
            this.txtStateCode.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.TabStop = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CompanyDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.gpTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkRemove);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.linkAdd);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtCountry);
            this.Controls.Add(this.txtAccNo);
            this.Controls.Add(this.txtIFSC);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtStateCode);
            this.Controls.Add(this.txtCompanyCode);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGST);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMobbNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPhNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPinCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtLocalName);
            this.Controls.Add(this.txtCompName);
            this.Controls.Add(this.txtPlace);
            this.MaximizeBox = false;
            this.Name = "CompanyDetails";
            this.Load += new System.EventHandler(this.CompanyDetails_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompanyDetails_KeyDown);
            this.Controls.SetChildIndex(this.txtPlace, 0);
            this.Controls.SetChildIndex(this.txtCompName, 0);
            this.Controls.SetChildIndex(this.txtLocalName, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtPinCode, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtPhNo, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtMobbNo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.txtFax, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.txtGST, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.txtWebsite, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.txtCompanyCode, 0);
            this.Controls.SetChildIndex(this.txtStateCode, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.cmbType, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.txtBranch, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.txtIFSC, 0);
            this.Controls.SetChildIndex(this.txtAccNo, 0);
            this.Controls.SetChildIndex(this.txtCountry, 0);
            this.Controls.SetChildIndex(this.txtState, 0);
            this.Controls.SetChildIndex(this.pbLogo, 0);
            this.Controls.SetChildIndex(this.linkAdd, 0);
            this.Controls.SetChildIndex(this.txtBank, 0);
            this.Controls.SetChildIndex(this.linkRemove, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.gpTest, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
