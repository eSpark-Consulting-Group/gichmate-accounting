using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class DatabaseAutoBackup : BaseForm
	{
		private string database;

		private string strQuery;

		private string strPath;

		private SqlConnection con;

		private string strDBFirstPath;

		private IContainer components = null;

		private ProgressBar progressBar1;

		private Timer timer1;

		private Label label1;

		private BackgroundWorker backgroundWorker1;

		public DatabaseAutoBackup()
		{
			InitializeComponent();
			backgroundWorker1.WorkerReportsProgress = true;
		}

		private void BakupMethod()
		{
			if (Settings.Default.BackupPath == string.Empty)
			{
				strPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Backup";
			}
			else
			{
				strPath = Settings.Default.BackupPath;
			}
			try
			{
				if (!Directory.Exists(strPath))
				{
					Directory.CreateDirectory(strPath);
				}
				strDBFirstPath = strPath + "\\BackupDBF-" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".Bak";
				if (File.Exists(strDBFirstPath))
				{
					File.Delete(strDBFirstPath);
				}
				strQuery = "BACKUP DATABASE [" + Settings.Default.strSQLCon + "] TO DISK='" + strDBFirstPath + "'";
				AshproCommon.DatabaseMethod(strQuery);
			}
			catch (Exception)
			{
			}
		}

		private void DBBackupMethod(string strCon, string Query)
		{
			try
			{
				using (con = new SqlConnection(strCon))
				{
					using SqlCommand sqlCommand = new SqlCommand(Query, con);
					if (con.State != ConnectionState.Open)
					{
						con.Open();
					}
					sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if (con.State == ConnectionState.Open)
				{
					con.Close();
				}
			}
		}

		private void DatabaseBackup_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
			backgroundWorker1.RunWorkerAsync();
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BakupMethod();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
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
			components = new System.ComponentModel.Container();
			progressBar1 = new System.Windows.Forms.ProgressBar();
			timer1 = new System.Windows.Forms.Timer(components);
			label1 = new System.Windows.Forms.Label();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(528, 1);
			progressBar1.Location = new System.Drawing.Point(29, 67);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(493, 23);
			progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			progressBar1.TabIndex = 0;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(48, 25);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(118, 17);
			label1.TabIndex = 1;
			label1.Text = "Prosessing......";
			backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
			backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
			backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(564, 162);
			base.Controls.Add(label1);
			base.Controls.Add(progressBar1);
			base.MaximizeBox = false;
			base.Name = "DatabaseAutoBackup";
			base.Load += new System.EventHandler(DatabaseBackup_Load);
			base.Controls.SetChildIndex(progressBar1, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
