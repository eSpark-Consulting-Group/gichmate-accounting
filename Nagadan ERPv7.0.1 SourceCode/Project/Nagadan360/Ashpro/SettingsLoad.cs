using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;

namespace Ashpro
{
	public class SettingsLoad : BaseFormTwo
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private ProgressBar progressBar;

		private Label label1;

		private BackgroundWorker backgroundWorker;

		public SettingsLoad()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
		}

		private void SettingsLoad_Load(object sender, EventArgs e)
		{
			backgroundWorker.RunWorkerAsync();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			CommonSettings.LoadSettings();
			Utility.gbValues = new MasterValue();
			Utility.pbValues = new MasterValue();
			Utility.gbValues = db.MasterValues(new
			{
				MasterType = "GButtonSize"
			}).FirstOrDefault();
			Utility.pbValues = db.MasterValues(new
			{
				MasterType = "PButtonSize"
			}).FirstOrDefault();
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
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
			progressBar = new System.Windows.Forms.ProgressBar();
			label1 = new System.Windows.Forms.Label();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			SuspendLayout();
			progressBar.Location = new System.Drawing.Point(47, 86);
			progressBar.Margin = new System.Windows.Forms.Padding(4);
			progressBar.Name = "progressBar";
			progressBar.Size = new System.Drawing.Size(657, 28);
			progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			progressBar.TabIndex = 1;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(53, 39);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(194, 20);
			label1.TabIndex = 2;
			label1.Text = "Loading Settings . . . .";
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
			backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(750, 200);
			base.Controls.Add(label1);
			base.Controls.Add(progressBar);
			base.Name = "SettingsLoad";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "SettingsLoad";
			base.Load += new System.EventHandler(SettingsLoad_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
