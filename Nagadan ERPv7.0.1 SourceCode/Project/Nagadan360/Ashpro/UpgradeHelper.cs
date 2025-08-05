using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;

namespace Ashpro
{
	public class UpgradeHelper : BaseForm
	{
		private int iFileVersionNumber;

		private IContainer components = null;

		private Label lblPercentage;

		private ProgressBar pbar;

		public bool isDbReset { get; set; } = false;


		public UpgradeHelper()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
		}

		private void ExecuteScriptsFromFile(string sFilePath)
		{
			if (isDbReset && sFilePath.Contains("_UT"))
			{
				return;
			}
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

		private void UpgradeHelper_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
		}

		private void UpgradeHelper_Shown(object sender, EventArgs e)
		{
			try
			{
				new DBContext();
				DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Upgrade");
				FileInfo[] files = directoryInfo.GetFiles("*.sql");
				int num = 1;
				pbar.Minimum = 0;
				pbar.Maximum = files.Count();
				lblPercentage.Text = "0%";
				double num2 = files.Count();
				string text = AshproCommon.IdFindMethod("Select Productserial From Institute");
				string text2 = text.Substring(0, 2);
				string text3 = text.Substring(3, 1);
				string text4 = text.Substring(5, 4);
				int num3 = (text2 + text3 + text4).ToInt32();
				foreach (FileInfo item in files.OrderBy((FileInfo x) => x.Name))
				{
					iFileVersionNumber = item.Name.Substring(0, 7).ToInt32();
					if (iFileVersionNumber >= num3)
					{
						ExecuteScriptsFromFile(item.FullName);
					}
					pbar.Value = num;
					double num4 = (double)num / num2 * 100.0;
					lblPercentage.Text = Math.Round(num4, 0) + "%";
					lblPercentage.Refresh();
					num++;
					if (num4 == 100.0)
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
			lblPercentage = new System.Windows.Forms.Label();
			pbar = new System.Windows.Forms.ProgressBar();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(528, -2);
			lblPercentage.AutoSize = true;
			lblPercentage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblPercentage.Location = new System.Drawing.Point(244, 61);
			lblPercentage.Name = "lblPercentage";
			lblPercentage.Size = new System.Drawing.Size(29, 16);
			lblPercentage.TabIndex = 3;
			lblPercentage.Text = "0%";
			pbar.Location = new System.Drawing.Point(42, 34);
			pbar.Name = "pbar";
			pbar.Size = new System.Drawing.Size(484, 23);
			pbar.TabIndex = 2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(564, 101);
			base.Controls.Add(lblPercentage);
			base.Controls.Add(pbar);
			base.Name = "UpgradeHelper";
			base.Load += new System.EventHandler(UpgradeHelper_Load);
			base.Shown += new System.EventHandler(UpgradeHelper_Shown);
			base.Controls.SetChildIndex(pbar, 0);
			base.Controls.SetChildIndex(lblPercentage, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
