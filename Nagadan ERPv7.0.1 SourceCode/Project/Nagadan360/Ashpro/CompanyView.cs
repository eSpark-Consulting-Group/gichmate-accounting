using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class CompanyView : BaseForm
	{
		private string sPath;

		private int iDbVersionSum = 0;

		private bool isSPConString = false;

		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private AshComboBox cmbServer;

		private Button btnNewCompany;

		private CheckBox cbWAutentication;

		private Button btnDbConfiguration;

		private LinkLabel llAdvanceSettings;

		private AshComboBox cbCompany;

		private Label label2;

		private Button btnSelect;

		private Button btnDegrade;

		private Button btnPatch;

		public Form logForm { get; set; }

		public CompanyView()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			ColorChange.Color(this);
		}

		private void LoginFormMethod()
		{
			Cursor = Cursors.Arrow;
			if (logForm == null)
			{
				ThemeChanger themeChanger = (ThemeChanger)(logForm = new ThemeChanger(_isFromActivation: true));
			}
			logForm.Show();
			Hide();
		}

		private async Task LoadListMethod()
		{
			try
			{
				cbCompany.Items.Clear();
				string constr = "Server = " + cmbServer.Text + ";Integrated Security = True";
				if (!cbWAutentication.Checked)
				{
					constr = "Server = " + cmbServer.Text + "; Integrated Security = false;User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword;
				}
				new DataTable();
				foreach (DataRow dr in (await AshproCommon.GetDataTableAsync("SELECT name from sys.databases order by name asc ", constr)).Rows)
				{
					try
					{
						string db = dr[0].ToString();
						if (await DBHelper.isValidCompany(db) && await DBHelper.isApplicationCompany(db, cmbServer.Text, cbWAutentication.Checked))
						{
							cbCompany.Items.Add(db);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void GetSDFFiles()
		{
			cbCompany.Items.Clear();
			sPath = Path.GetDirectoryName(Application.ExecutablePath);
			DirectoryInfo directoryInfo = new DirectoryInfo(sPath);
			FileInfo[] files = directoryInfo.GetFiles("*.sdf");
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				string item = fileInfo.ToString().Replace(".sdf", "");
				cbCompany.Items.Add(item);
			}
			if (Settings.Default.strSQLCon != string.Empty)
			{
				string strSQLCon = Settings.Default.strSQLCon;
				string text = strSQLCon.Substring(strSQLCon.LastIndexOf("\\") + 1);
				cbCompany.Text = text.Replace(".sdf", "");
			}
		}

		private async Task DBModeMethod()
		{
			label1.Visible = true;
			cmbServer.Visible = true;
			cbWAutentication.Visible = true;
			btnDbConfiguration.Visible = true;
			if (Settings.Default.strSQLCon != string.Empty)
			{
				cbCompany.Text = Settings.Default.strSQLCon;
			}
			if (Settings.Default.strServer != string.Empty)
			{
				cmbServer.Text = Settings.Default.strServer;
			}
			else
			{
				cmbServer.Text = Utility.uTerminal + "\\UNIPOSPRO";
			}
			if (cmbServer.Text != string.Empty)
			{
				await LoadListMethod();
			}
		}

		private void SelectCompany()
		{
			if (DBHelper.isUpgradeRequired())
			{
				try
				{
					Process process = new Process();
					process.StartInfo.FileName = "UpgradeHelper.exe";
					string arguments = Settings.Default.strServer + " \"" + Settings.Default.strSQLCon + " \" " + Settings.Default.sUserName + " \"" + Settings.Default.sPassword + " \"" + iDbVersionSum;
					process.StartInfo.Arguments = arguments;
					process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
					process.Start();
					process.WaitForExit();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + " 282");
				}
			}
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private async void CompanyView_Load(object sender, EventArgs e)
		{
			if (!Settings.Default.isServer)
			{
				cbWAutentication.Checked = false;
			}
			await DBModeMethod();
			Cursor = Cursors.Arrow;
		}

		private void cmbServer_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		public void btnSelect_Click(object sender, EventArgs e)
		{
			if (!isSPConString)
			{
				lbCompany_MouseDoubleClick(null, null);
			}
			LoginFormMethod();
		}

		private async void btnNewCompany_Click(object sender, EventArgs e)
		{
			NewCompany frm = new NewCompany
			{
				sServer = cmbServer.Text,
				isServer = (cbWAutentication.Checked ? true : false)
			};
			if (frm.ShowDialog() == DialogResult.OK)
			{
				await LoadListMethod();
				cbCompany.Text = frm.sCompany;
			}
		}

		private void btnDbConfiguration_Click(object sender, EventArgs e)
		{
			DBConnectionSettings dBConnectionSettings = new DBConnectionSettings();
			if (dBConnectionSettings.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.sUserName = dBConnectionSettings.sUserName;
				Settings.Default.sPassword = dBConnectionSettings.sPassword;
				Settings.Default.Save();
			}
		}

		private void lbCompany_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				Cursor = Cursors.WaitCursor;
				if (cbCompany.Text != string.Empty)
				{
					if (cmbServer.Text == string.Empty)
					{
						MessageBox.Show("Please Select a Server.", Utility.uManufacturer);
						return;
					}
					DBHelper.DatabaseAddMethod(cbCompany.Text, cmbServer.Text, cbWAutentication.Checked);
				}
				DBHelper.DBUpgradInMethod();
				DBContext dBContext = new DBContext();
				if (dBContext.DotmatrixMasters().Count() == 0)
				{
					FileInfo fileInfo = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\InitialData.sql");
					DatabaseInitialCreation.ExecuteScriptsFromFile(fileInfo.FullName);
				}
				Cursor = Cursors.Arrow;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 261");
			}
		}

		private void llAdvanceSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DatabaseConnection databaseConnection = new DatabaseConnection();
			if (databaseConnection.ShowDialog() == DialogResult.OK)
			{
				isSPConString = true;
			}
		}

		private void btnDegrade_Click(object sender, EventArgs e)
		{
			try
			{
				AshproCommon.DatabaseMethod("update Institute set Productserial='20.0.1001'");
				Messages.InformationMessage("Successfully degraded the Database");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async void cmbServer_Validated(object sender, EventArgs e)
		{
			AshUsercontrol.ProgressBar frm = new AshUsercontrol.ProgressBar(this);
			await LoadListMethod();
			frm.Visible = false;
		}

		private void btnPatch_Click(object sender, EventArgs e)
		{
			ApplicationPatch applicationPatch = new ApplicationPatch();
			applicationPatch.ShowDialog();
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
			lblTitle = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			cmbServer = new AshUsercontrol.AshComboBox();
			btnSelect = new System.Windows.Forms.Button();
			btnNewCompany = new System.Windows.Forms.Button();
			cbWAutentication = new System.Windows.Forms.CheckBox();
			btnDbConfiguration = new System.Windows.Forms.Button();
			llAdvanceSettings = new System.Windows.Forms.LinkLabel();
			cbCompany = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			btnDegrade = new System.Windows.Forms.Button();
			btnPatch = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(305, 1);
			btnEnd.Margin = new System.Windows.Forms.Padding(4);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 4);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(183, 24);
			lblTitle.TabIndex = 82;
			lblTitle.Text = "Select Company";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(30, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(62, 16);
			label1.TabIndex = 85;
			label1.Text = "Server :";
			label1.Visible = false;
			cmbServer.EnterColor = System.Drawing.Color.LightPink;
			cmbServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbServer.FormattingEnabled = true;
			cmbServer.LeaveColor = System.Drawing.Color.White;
			cmbServer.Location = new System.Drawing.Point(99, 49);
			cmbServer.Name = "cmbServer";
			cmbServer.Size = new System.Drawing.Size(229, 21);
			cmbServer.TabIndex = 86;
			cmbServer.Visible = false;
			cmbServer.Validated += new System.EventHandler(cmbServer_Validated);
			btnSelect.BackColor = System.Drawing.Color.DodgerBlue;
			btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSelect.ForeColor = System.Drawing.Color.White;
			btnSelect.Location = new System.Drawing.Point(175, 152);
			btnSelect.Name = "btnSelect";
			btnSelect.Size = new System.Drawing.Size(120, 25);
			btnSelect.TabIndex = 88;
			btnSelect.Text = "Select Company";
			btnSelect.UseVisualStyleBackColor = false;
			btnSelect.Click += new System.EventHandler(btnSelect_Click);
			btnNewCompany.BackColor = System.Drawing.Color.DodgerBlue;
			btnNewCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnNewCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnNewCompany.ForeColor = System.Drawing.Color.White;
			btnNewCompany.Location = new System.Drawing.Point(7, 103);
			btnNewCompany.Name = "btnNewCompany";
			btnNewCompany.Size = new System.Drawing.Size(121, 25);
			btnNewCompany.TabIndex = 88;
			btnNewCompany.Text = "New Company";
			btnNewCompany.UseVisualStyleBackColor = false;
			btnNewCompany.Click += new System.EventHandler(btnNewCompany_Click);
			cbWAutentication.AutoSize = true;
			cbWAutentication.Checked = true;
			cbWAutentication.CheckState = System.Windows.Forms.CheckState.Checked;
			cbWAutentication.Location = new System.Drawing.Point(165, 130);
			cbWAutentication.Name = "cbWAutentication";
			cbWAutentication.Size = new System.Drawing.Size(141, 17);
			cbWAutentication.TabIndex = 90;
			cbWAutentication.Text = "Windows Authentication";
			cbWAutentication.UseVisualStyleBackColor = true;
			cbWAutentication.Visible = false;
			btnDbConfiguration.BackColor = System.Drawing.Color.DodgerBlue;
			btnDbConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDbConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDbConfiguration.ForeColor = System.Drawing.Color.White;
			btnDbConfiguration.Location = new System.Drawing.Point(7, 135);
			btnDbConfiguration.Name = "btnDbConfiguration";
			btnDbConfiguration.Size = new System.Drawing.Size(121, 25);
			btnDbConfiguration.TabIndex = 88;
			btnDbConfiguration.Text = "Configuration";
			btnDbConfiguration.UseVisualStyleBackColor = false;
			btnDbConfiguration.Visible = false;
			btnDbConfiguration.Click += new System.EventHandler(btnDbConfiguration_Click);
			llAdvanceSettings.AutoSize = true;
			llAdvanceSettings.Location = new System.Drawing.Point(30, 200);
			llAdvanceSettings.Name = "llAdvanceSettings";
			llAdvanceSettings.Size = new System.Drawing.Size(91, 13);
			llAdvanceSettings.TabIndex = 91;
			llAdvanceSettings.TabStop = true;
			llAdvanceSettings.Text = "Advance Settings";
			llAdvanceSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llAdvanceSettings_LinkClicked);
			cbCompany.EnterColor = System.Drawing.Color.LightPink;
			cbCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cbCompany.FormattingEnabled = true;
			cbCompany.LeaveColor = System.Drawing.Color.White;
			cbCompany.Location = new System.Drawing.Point(98, 76);
			cbCompany.Name = "cbCompany";
			cbCompany.Size = new System.Drawing.Size(229, 21);
			cbCompany.TabIndex = 92;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(13, 77);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 16);
			label2.TabIndex = 85;
			label2.Text = "Company :";
			btnDegrade.BackColor = System.Drawing.Color.DodgerBlue;
			btnDegrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDegrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDegrade.ForeColor = System.Drawing.Color.White;
			btnDegrade.Location = new System.Drawing.Point(7, 166);
			btnDegrade.Name = "btnDegrade";
			btnDegrade.Size = new System.Drawing.Size(121, 25);
			btnDegrade.TabIndex = 93;
			btnDegrade.Text = "Degrade DB";
			btnDegrade.UseVisualStyleBackColor = false;
			btnDegrade.Click += new System.EventHandler(btnDegrade_Click);
			btnPatch.BackColor = System.Drawing.Color.DodgerBlue;
			btnPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPatch.ForeColor = System.Drawing.Color.White;
			btnPatch.Location = new System.Drawing.Point(175, 188);
			btnPatch.Name = "btnPatch";
			btnPatch.Size = new System.Drawing.Size(120, 25);
			btnPatch.TabIndex = 88;
			btnPatch.Text = "Activate Patch";
			btnPatch.UseVisualStyleBackColor = false;
			btnPatch.Click += new System.EventHandler(btnPatch_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(340, 224);
			base.Controls.Add(btnDegrade);
			base.Controls.Add(cbCompany);
			base.Controls.Add(llAdvanceSettings);
			base.Controls.Add(cbWAutentication);
			base.Controls.Add(btnDbConfiguration);
			base.Controls.Add(btnNewCompany);
			base.Controls.Add(btnPatch);
			base.Controls.Add(btnSelect);
			base.Controls.Add(cmbServer);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(lblTitle);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "CompanyView";
			base.Load += new System.EventHandler(CompanyView_Load);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbServer, 0);
			base.Controls.SetChildIndex(btnSelect, 0);
			base.Controls.SetChildIndex(btnPatch, 0);
			base.Controls.SetChildIndex(btnNewCompany, 0);
			base.Controls.SetChildIndex(btnDbConfiguration, 0);
			base.Controls.SetChildIndex(cbWAutentication, 0);
			base.Controls.SetChildIndex(llAdvanceSettings, 0);
			base.Controls.SetChildIndex(cbCompany, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(btnDegrade, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
