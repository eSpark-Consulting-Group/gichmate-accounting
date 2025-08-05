using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class DatabaseInitialCreation : BaseForm
	{
		private int iFileVersionNumber;

		private IContainer components = null;

		private Label label1;

		private ProgressBar pbar;

		private Label lblPercentage;

		public DatabaseInitialCreation()
		{
			InitializeComponent();
		}

		public static void ExecuteScriptsFromFile(string sFilePath)
		{
			string text = string.Empty;
			try
			{
				if (File.Exists(sFilePath))
				{
					FileInfo fileInfo = new FileInfo(sFilePath);
					string text2 = fileInfo.OpenText().ReadToEnd();
					string[] array = text2.Split(new string[1] { "GO-" }, StringSplitOptions.None);
					string[] array2 = array;
					foreach (string text3 in array2)
					{
						text = text3;
						AshproCommon.DatabaseMethod(text3);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 146 " + text + " - " + sFilePath);
			}
		}

		private void UpdateVersionToDatabase(int iFileVersionNumber)
		{
			string text = iFileVersionNumber.ToString();
			string text2 = text.Substring(0, 2);
			string text3 = text.Substring(2, 1);
			string text4 = text.Substring(3, 4);
			text = text2 + "." + text3 + "." + text4;
			try
			{
				AshproCommon.DatabaseMethod("update Institute set Productserial='" + text + "'");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				base.DialogResult = DialogResult.OK;
				Thread.Sleep(500);
				Close();
			}
		}

		private void UpdateMethod()
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Upgrade");
				FileInfo[] files = directoryInfo.GetFiles("*.sql");
				int num = 1;
				pbar.Minimum = 0;
				pbar.Maximum = files.Count();
				lblPercentage.Text = "0%";
				double num2 = files.Count();
				foreach (FileInfo item in files.OrderBy((FileInfo x) => x.Name))
				{
					ExecuteScriptsFromFile(item.FullName);
					pbar.Value = num;
					double num3 = (double)num / num2 * 100.0;
					lblPercentage.Text = Math.Round(num3, 0) + "%";
					lblPercentage.Refresh();
					num++;
					if (num3 == 100.0)
					{
						UpdateVersionToDatabase(iFileVersionNumber);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 74");
			}
		}

		private void DatabaseInitialCreation_Load(object sender, EventArgs e)
		{
			string text = Environment.MachineName + "\\UNIPOSPRO";
			DBHelper.NewDatabaseMethodSQL("UniposDB", text, isShowMessage: false);
			UpdateMethod();
			FileInfo fileInfo = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\InitialData.sql");
			ExecuteScriptsFromFile(fileInfo.FullName);
			Settings.Default.strServer = text;
			Settings.Default.strSQLCon = "UniposDB";
			Settings.Default.sUserName = "sa";
			Settings.Default.sPassword = "sa1234";
			Settings.Default.strConnetionString = "Server = " + text + "; Database = UniposDB;User ID=" + Settings.Default.sUserName + ";Password=" + Settings.Default.sPassword;
			Settings.Default.Save();
			POSActivation.FormLoadMethodTwo();
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
			label1 = new System.Windows.Forms.Label();
			pbar = new System.Windows.Forms.ProgressBar();
			lblPercentage = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(551, -1);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(73, 37);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(199, 17);
			label1.TabIndex = 13;
			label1.Text = "Prosessing please wait.....";
			pbar.Location = new System.Drawing.Point(54, 79);
			pbar.Name = "pbar";
			pbar.Size = new System.Drawing.Size(493, 23);
			pbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			pbar.TabIndex = 12;
			lblPercentage.AutoSize = true;
			lblPercentage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblPercentage.Location = new System.Drawing.Point(266, 105);
			lblPercentage.Name = "lblPercentage";
			lblPercentage.Size = new System.Drawing.Size(29, 16);
			lblPercentage.TabIndex = 14;
			lblPercentage.Text = "0%";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(585, 141);
			base.Controls.Add(lblPercentage);
			base.Controls.Add(label1);
			base.Controls.Add(pbar);
			base.Name = "DatabaseInitialCreation";
			Text = "DatabaseInitialCreation";
			base.Load += new System.EventHandler(DatabaseInitialCreation_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(pbar, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(lblPercentage, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
