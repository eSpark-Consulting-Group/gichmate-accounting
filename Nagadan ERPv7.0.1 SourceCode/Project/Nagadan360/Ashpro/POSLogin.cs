using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;


namespace Ashpro
{
	public class POSLogin : BaseForm
	{
		private DBContext db = new DBContext();

		private User user;

		private bool isFromPrograms;

		private IContainer components = null;

		private AshTextBoxNormal txtPWord;

		private Label lblCompany;

		private NumKeyBoardLogin nkPOS;

		private Button btnLogin;

		private LinkLabel llCompanyView;

		private CheckBox cbArabic;

		private Label lblBranch;

		private AshComboBox cmbBranch;

		private Label lblLicense;

		private Label lblTitle;

		private CheckBox cbCounterSale;

		public POSLogin(bool _isFromPrograms = true)
		{
			isFromPrograms = _isFromPrograms;
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void POSLogin_Load(object sender, EventArgs e)
		{
			try
			{
				cbCounterSale.Checked = Settings.Default.IsSalesCounter;
				foreach (Control control in nkPOS.Controls)
				{
					if (control is Button)
					{
						control.BackColor = Settings.Default.LoginB;
						control.ForeColor = Settings.Default.LoginF;
					}
				}
				if (Settings.Default.Language == "ar")
				{
					cbArabic.Checked = true;
				}
				lblLicense.Text = Utility.uLicenceNumber;
				AniHelper.FillBranch(cmbBranch);
				if (cmbBranch.Items.Count > 0 && Settings.Default.iBranchId != 0)
				{
					cmbBranch.SelectedValue = Settings.Default.iBranchId;
				}
			}
			catch
			{
			}
		}

		private void llCompanyView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
			if (text == AniHelper.PWordGeneration())
			{
				Cursor = Cursors.WaitCursor;
				CompanyView companyView = new CompanyView();
				companyView.logForm = this;
				companyView.Show();
				Hide();
			}
		}

		private void POSLogin_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				LinkLabel linkLabel = llCompanyView;
				int visible;
				if (!llCompanyView.Visible)
				{
					llCompanyView.Visible = true;
					visible = 1;
				}
				else
				{
					llCompanyView.Visible = false;
					visible = 0;
				}
				linkLabel.Visible = (byte)visible != 0;
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			LoginMethod();
		}

		private void LoginMethod()
		{
			try
			{
				if (txtPWord.Text != "")
				{
					user = db.Users(new
					{
						PassWord = txtPWord.Text
					}).FirstOrDefault();
					if (user != null)
					{
						bool uIsSalesCounter = (Settings.Default.IsSalesCounter = cbCounterSale.Checked);
						Utility.uIsSalesCounter = uIsSalesCounter;
						Settings.Default.iBranchId = cmbBranch.SelectedValue.ToInt32();
						Settings.Default.intLogOnId = user.UserId.ToInt32();
						Settings.Default.Save();
						if (Utility.uManufacturer != "DeskCloud ERP")
						{
							DBHelper.DBUpgradInMethod();
						}
						MasterValue masterValue = db.MasterValues(new
						{
							MasterType = "SN",
							Name = Utility.uTerminal
						}).FirstOrDefault();
						if (masterValue != null)
						{
							Utility.uSerialNumber = masterValue.Description.ToInt32();
						}
						else
						{
							Utility.uSerialNumber = 1;
						}
						SettingsLoad settingsLoad = new SettingsLoad();
						if (settingsLoad.ShowDialog() == DialogResult.OK)
						{
							int iMod = 1;
							if (Settings.Default.MainForm != "POS")
							{
								iMod = 3;
							}
							DashboardPOS dashboardPOS = new DashboardPOS(iMod);
							dashboardPOS.Show();
							Hide();
						}
					}
					else
					{
						Messages.ErrorMessage("Please check your Password");
						txtPWord.Text = string.Empty;
						base.ActiveControl = txtPWord;
					}
				}
				else
				{
					Messages.ErrorMessage("Please enter full details");
					txtPWord.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			Cursor = Cursors.Arrow;
		}

		private void cbArabic_Click(object sender, EventArgs e)
		{
			if (cbArabic.Checked)
			{
				Settings.Default.Language = "ar";
			}
			else
			{
				Settings.Default.Language = "en";
			}
			Settings.Default.Save();
			txtPWord.Focus();
			txtPWord.SelectAll();
		}

		private void btnEnd_Click_1(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void POSLogin_VisibleChanged(object sender, EventArgs e)
		{
			txtPWord.Focus();
			Cursor = Cursors.Arrow;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSLogin));
            this.txtPWord = new AshUsercontrol.AshTextBoxNormal();
            this.lblCompany = new System.Windows.Forms.Label();
            this.nkPOS = new AshUsercontrol.NumKeyBoardLogin();
            this.btnLogin = new System.Windows.Forms.Button();
            this.llCompanyView = new System.Windows.Forms.LinkLabel();
            this.cbArabic = new System.Windows.Forms.CheckBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbCounterSale = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click_1);
            // 
            // txtPWord
            // 
            resources.ApplyResources(this.txtPWord, "txtPWord");
            this.txtPWord.BackColor = System.Drawing.Color.White;
            this.txtPWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPWord.Data = 0;
            this.txtPWord.EnterColor = System.Drawing.Color.LightPink;
            this.txtPWord.Format = null;
            this.txtPWord.isAllowNegative = false;
            this.txtPWord.isAllowSpecialChar = false;
            this.txtPWord.isNumberOnly = true;
            this.txtPWord.isNumeric = false;
            this.txtPWord.LeaveColor = System.Drawing.Color.White;
            this.txtPWord.Name = "txtPWord";
            this.txtPWord.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblCompany
            // 
            resources.ApplyResources(this.lblCompany, "lblCompany");
            this.lblCompany.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCompany.Name = "lblCompany";
            // 
            // nkPOS
            // 
            resources.ApplyResources(this.nkPOS, "nkPOS");
            this.nkPOS.Name = "nkPOS";
            this.nkPOS.TabStop = false;
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.BackColor = System.Drawing.Color.ForestGreen;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.TabStop = false;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // llCompanyView
            // 
            resources.ApplyResources(this.llCompanyView, "llCompanyView");
            this.llCompanyView.Name = "llCompanyView";
            this.llCompanyView.TabStop = true;
            this.llCompanyView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCompanyView_LinkClicked);
            // 
            // cbArabic
            // 
            resources.ApplyResources(this.cbArabic, "cbArabic");
            this.cbArabic.Name = "cbArabic";
            this.cbArabic.TabStop = false;
            this.cbArabic.UseVisualStyleBackColor = true;
            this.cbArabic.Click += new System.EventHandler(this.cbArabic_Click);
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            // 
            // lblLicense
            // 
            resources.ApplyResources(this.lblLicense, "lblLicense");
            this.lblLicense.Name = "lblLicense";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Name = "lblTitle";
            // 
            // cbCounterSale
            // 
            resources.ApplyResources(this.cbCounterSale, "cbCounterSale");
            this.cbCounterSale.Name = "cbCounterSale";
            this.cbCounterSale.TabStop = false;
            this.cbCounterSale.UseVisualStyleBackColor = true;
            this.cbCounterSale.Click += new System.EventHandler(this.cbArabic_Click);
            // 
            // POSLogin
            // 
            this.AcceptButton = this.btnLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblLicense);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cbCounterSale);
            this.Controls.Add(this.cbArabic);
            this.Controls.Add(this.llCompanyView);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.nkPOS);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.txtPWord);
            this.Name = "POSLogin";
            this.Load += new System.EventHandler(this.POSLogin_Load);
            this.VisibleChanged += new System.EventHandler(this.POSLogin_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSLogin_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.txtPWord, 0);
            this.Controls.SetChildIndex(this.lblCompany, 0);
            this.Controls.SetChildIndex(this.nkPOS, 0);
            this.Controls.SetChildIndex(this.btnLogin, 0);
            this.Controls.SetChildIndex(this.llCompanyView, 0);
            this.Controls.SetChildIndex(this.cbArabic, 0);
            this.Controls.SetChildIndex(this.cbCounterSale, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.lblLicense, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
