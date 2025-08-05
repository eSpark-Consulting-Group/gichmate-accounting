using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class Login : BaseForm
	{
		private DBContext db;

		private User user;

		private IContainer components = null;

		private Label label2;

		private Label label1;

		private PictureBox pictureBox1;

		private Label lblCompany;

		private ToolTip toolTip1;

		private Button btnLogin;

		private Button btnCancel;

		private AshTextBoxNormal txtUserName;

		private AshTextBoxNormal txtPWord;

		private Label lblStatus;

		private LinkLabel llCompanyView;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private CheckBox cbArabic;

		public Login()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
            AniHelper.IconChangeMethod(this);
            
        }

		private void btnLoggin_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			try
			{
				if ((txtUserName.Text != "") & (txtPWord.Text != ""))
				{
					user = (from x in db.Users()
						where x.UserName == txtUserName.Text.ToUpper() && x.PassWord == txtPWord.Text
						select x).FirstOrDefault();
					if (user != null)
					{
						Utility.iBranchId = ((cmbBranch.SelectedValue == null) ? 1 : cmbBranch.SelectedValue.ToInt32());
						Settings.Default.iBranchId = Utility.iBranchId;
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
						if (Settings.Default.MainForm == "MDI")
						{
							Main main = new Main();
							main.Show();
							Hide();
						}
						else
						{
							DashboardPOS dashboardPOS = new DashboardPOS(2);
							dashboardPOS.Show();
							Hide();
						}
					}
					else
					{
						Messages.ErrorMessage("Please check your Username and Password");
						AniHelper.ClearMethod(this);
						txtUserName.Focus();
					}
				}
				else
				{
					Messages.ErrorMessage("Please enter full details");
					AniHelper.ClearMethod(this);
					txtUserName.Focus();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			Cursor = Cursors.Arrow;
		}

		private void Login_Load(object sender, EventArgs e)
		{
			try
			{
				cbArabic.Checked = ((Settings.Default.Language == "ar") ? true : false);
				lblCompany.Text = Utility.uManufacturer + " - Login";
				btnEnd.Visible = false;
				db = new DBContext();
				base.ActiveControl = txtUserName;
				AniHelper.FillBranch(cmbBranch);
				if (cmbBranch.Items.Count > 0)
				{
					cmbBranch.SelectedValue = Settings.Default.iBranchId;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void llCompanyView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            //string text = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
            //if (text == AniHelper.PWordGeneration())
            //{
            //	Cursor = Cursors.WaitCursor;
            //	CompanyView companyView = new CompanyView();
            //	companyView.logForm = this;
            //	companyView.Show();
            //	Hide();
            //}
            this.Cursor = Cursors.WaitCursor;
            CompanyView frm = new CompanyView();
            frm.Show();
            this.Hide();
        }

		private void Login_KeyDown(object sender, KeyEventArgs e)
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void txtPWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnLoggin_Click(null, null);
			}
		}

		private async void Login_Activated(object sender, EventArgs e)
		{
			txtUserName.Focus();
			Task task = new Task(CommonSettings.LoadSettings);
			task.Start();
			await task;
			Cursor = Cursors.Arrow;
		}

		private void cbArabic_Click(object sender, EventArgs e)
		{
			if (cbArabic.Checked)
			{
				Settings.Default.Language = "ar";
                RightToLeftLayout = true;
			}
			else
			{
				Settings.Default.Language = "en";
                RightToLeftLayout = false;
            }
            Settings.Default.Save();
            //InitializeComponent();
            RightToLeftLayout = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtUserName = new AshUsercontrol.AshTextBoxNormal();
            this.txtPWord = new AshUsercontrol.AshTextBoxNormal();
            this.lblStatus = new System.Windows.Forms.Label();
            this.llCompanyView = new System.Windows.Forms.LinkLabel();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbArabic = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // lblCompany
            // 
            resources.ApplyResources(this.lblCompany, "lblCompany");
            this.lblCompany.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCompany.Name = "lblCompany";
            this.toolTip1.SetToolTip(this.lblCompany, resources.GetString("lblCompany.ToolTip"));
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Name = "btnLogin";
            this.toolTip1.SetToolTip(this.btnLogin, resources.GetString("btnLogin.ToolTip"));
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLoggin_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.toolTip1.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.BackColor = System.Drawing.Color.White;
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
            this.toolTip1.SetToolTip(this.txtUserName, resources.GetString("txtUserName.ToolTip"));
            this.txtUserName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.txtPWord.isNumberOnly = false;
            this.txtPWord.isNumeric = false;
            this.txtPWord.LeaveColor = System.Drawing.Color.White;
            this.txtPWord.Name = "txtPWord";
            this.toolTip1.SetToolTip(this.txtPWord, resources.GetString("txtPWord.ToolTip"));
            this.txtPWord.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPWord_KeyDown);
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Name = "lblStatus";
            this.toolTip1.SetToolTip(this.lblStatus, resources.GetString("lblStatus.ToolTip"));
            // 
            // llCompanyView
            // 
            resources.ApplyResources(this.llCompanyView, "llCompanyView");
            this.llCompanyView.Name = "llCompanyView";
            this.llCompanyView.TabStop = true;
            this.toolTip1.SetToolTip(this.llCompanyView, resources.GetString("llCompanyView.ToolTip"));
            this.llCompanyView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCompanyView_LinkClicked);
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.toolTip1.SetToolTip(this.cmbBranch, resources.GetString("cmbBranch.ToolTip"));
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            this.toolTip1.SetToolTip(this.lblBranch, resources.GetString("lblBranch.ToolTip"));
            // 
            // cbArabic
            // 
            resources.ApplyResources(this.cbArabic, "cbArabic");
            this.cbArabic.Name = "cbArabic";
            this.cbArabic.TabStop = false;
            this.toolTip1.SetToolTip(this.cbArabic, resources.GetString("cbArabic.ToolTip"));
            this.cbArabic.UseVisualStyleBackColor = true;
            this.cbArabic.Click += new System.EventHandler(this.cbArabic_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.cbArabic);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.llCompanyView);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtPWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCompany);
            this.Name = "Login";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Activated += new System.EventHandler(this.Login_Activated);
            this.Load += new System.EventHandler(this.Login_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.Controls.SetChildIndex(this.lblCompany, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btnLogin, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtPWord, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.llCompanyView, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.Controls.SetChildIndex(this.cbArabic, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
