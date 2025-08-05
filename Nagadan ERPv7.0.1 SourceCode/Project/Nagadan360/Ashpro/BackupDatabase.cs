using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class BackupDatabase : BaseForm
	{
		private string strFormType;

		private string strConnection;

		private string strQuery;

		private IContainer components = null;

		private Label label19;

		private Button btnBrowse;

		private Label label16;

		private BackgroundWorker backgroundWorker;

		private Label lblStatus;

		private System.Windows.Forms.ProgressBar progressBar;

		private ToolTip toolTip1;

		private AshTextBoxNormal txtPath;

		private Button btnOK;

		private Label lblTitle;

		private AshTextBoxNormal txtDatabase;

		public BackupDatabase(string strFrm)
		{
			InitializeComponent();
			ColorChange.Color(this);
			strFormType = strFrm;
		}

		private void OpenFileMethod()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = folderPath;
			openFileDialog.Title = "Database Restore";
			openFileDialog.Filter = "Backup File(*.bak) |*.bak|All Files(*.*) | *.*";
			openFileDialog.FilterIndex = 0;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = openFileDialog.FileName;
			}
		}

		private void SaveFileMethod()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Database Backup";
			saveFileDialog.Filter = "Backup File(*.bak) |*.bak|All Files(*.*) | *.*";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = saveFileDialog.FileName;
			}
		}

		private string GetSqlServerConnectionString(string address, string db)
		{
			return "Data Source=" + address.Trim() + ";Initial Catalog=" + db.Trim() + ";Integrated Security=SSPI;";
		}

		private void GenerateMethod()
		{
			if (txtDatabase.Text != string.Empty && txtPath.Text != string.Empty)
			{
				if (strFormType == "Backup")
				{
					MethodBackup();
				}
				else
				{
					RestoreMethod();
				}
			}
		}

		private void RestoreMethod()
		{
			if (txtDatabase.Text != string.Empty && txtPath.Text != string.Empty)
			{
				backgroundWorker.RunWorkerAsync();
				MethodRestore();
			}
			else
			{
				MessageBox.Show("Please fill the field");
			}
		}

		private void MethodRestore()
		{
			MessageBox.Show("Database Restoring started");
			try
			{
				strQuery = "ALTER DATABASE [" + txtDatabase.Text + "] Set SINGLE_USER WITH ROLLBACK IMMEDIATE";
				AshproCommon.DatabaseMethod(strQuery, strConnection);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 355");
			}
			try
			{
				strQuery = "USE MASTER RESTORE DATABASE [" + txtDatabase.Text + "] FROM DISK='" + txtPath.Text + "' WITH REPLACE;";
				AshproCommon.DatabaseMethod(strQuery, strConnection);
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message + "  373");
			}
			try
			{
				strQuery = "Alter Database [" + txtDatabase.Text + "] Set MULTI_USER";
				AshproCommon.DatabaseMethod(strQuery, strConnection);
				MessageBox.Show("Successfully restored");
			}
			catch (Exception ex3)
			{
				MessageBox.Show(ex3.Message + "  391");
			}
		}

		private void MethodBackup()
		{
			if (txtDatabase.Text != string.Empty && txtPath.Text != string.Empty)
			{
				backgroundWorker.RunWorkerAsync();
				BackupMethod();
			}
			else
			{
				MessageBox.Show("Please fill the field");
			}
		}

		private void BackupMethod()
		{
			if (File.Exists(txtPath.Text))
			{
				File.Delete(txtPath.Text);
			}
			try
			{
				strQuery = "BACKUP DATABASE [" + txtDatabase.Text + "] TO DISK='" + txtPath.Text + "'";
				AshproCommon.DatabaseMethod(strQuery);
				Messages.InformationMessage("Successfully completed the process!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnBrwsTwo_Click(object sender, EventArgs e)
		{
			if (strFormType == "Backup")
			{
				SaveFileMethod();
			}
			else
			{
				OpenFileMethod();
			}
		}

		private void BackupDatabase_Load(object sender, EventArgs e)
		{
			txtDatabase.Text = Settings.Default.strSQLCon;
			strConnection = "Server = " + Settings.Default.strServer + "; Database = master;Integrated Security = false;User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword;
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			GenerateMethod();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
			lblStatus.Text = "Processing ...." + e.ProgressPercentage + " %";
		}

		private void BackupDatabase_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.O && e.Alt)
			{
				e.SuppressKeyPress = true;
				GenerateMethod();
			}
			else if (e.KeyCode == Keys.O && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBrwsTwo_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else
			{
				e.SuppressKeyPress = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupDatabase));
            this.label19 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtPath = new AshUsercontrol.AshTextBoxNormal();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDatabase = new AshUsercontrol.AshTextBoxNormal();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            this.toolTip1.SetToolTip(this.label19, resources.GetString("label19.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip1.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrwsTwo_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip1.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            this.toolTip1.SetToolTip(this.lblStatus, resources.GetString("lblStatus.ToolTip"));
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.toolTip1.SetToolTip(this.progressBar, resources.GetString("progressBar.ToolTip"));
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPath.Data = 0;
            this.txtPath.EnterColor = System.Drawing.Color.LightPink;
            this.txtPath.Format = null;
            this.txtPath.isAllowNegative = false;
            this.txtPath.isAllowSpecialChar = false;
            this.txtPath.isNumberOnly = false;
            this.txtPath.isNumeric = false;
            this.txtPath.LeaveColor = System.Drawing.Color.White;
            this.txtPath.Name = "txtPath";
            this.toolTip1.SetToolTip(this.txtPath, resources.GetString("txtPath.ToolTip"));
            this.txtPath.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.toolTip1.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.toolTip1.SetToolTip(this.lblTitle, resources.GetString("lblTitle.ToolTip"));
            // 
            // txtDatabase
            // 
            resources.ApplyResources(this.txtDatabase, "txtDatabase");
            this.txtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDatabase.Data = 0;
            this.txtDatabase.EnterColor = System.Drawing.Color.LightPink;
            this.txtDatabase.Format = null;
            this.txtDatabase.isAllowNegative = false;
            this.txtDatabase.isAllowSpecialChar = false;
            this.txtDatabase.isNumberOnly = false;
            this.txtDatabase.isNumeric = false;
            this.txtDatabase.LeaveColor = System.Drawing.Color.White;
            this.txtDatabase.Name = "txtDatabase";
            this.toolTip1.SetToolTip(this.txtDatabase, resources.GetString("txtDatabase.ToolTip"));
            this.txtDatabase.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // BackupDatabase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label16);
            this.MaximizeBox = false;
            this.Name = "BackupDatabase";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.BackupDatabase_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BackupDatabase_KeyDown);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.progressBar, 0);
            this.Controls.SetChildIndex(this.txtPath, 0);
            this.Controls.SetChildIndex(this.txtDatabase, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
