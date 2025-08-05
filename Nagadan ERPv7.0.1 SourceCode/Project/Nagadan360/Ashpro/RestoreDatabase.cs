using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class RestoreDatabase : BaseForm
	{
		private string strFormType;

		private string strConnection;

		private string strQuery;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnOK;

		private AshTextBoxNormal txtDatabase;

		private AshTextBoxNormal txtPath;

		private System.Windows.Forms.ProgressBar progressBar;

		private Label lblStatus;

		private Label label19;

		private Button btnBrowse;

		private Label label16;

		public RestoreDatabase()
		{
			InitializeComponent();
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDatabase = new AshUsercontrol.AshTextBoxNormal();
            this.txtPath = new AshUsercontrol.AshTextBoxNormal();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(616, 0);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 24);
            this.lblTitle.TabIndex = 41;
            this.lblTitle.Text = "Database Restore";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(459, 114);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 40;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // txtDatabase
            // 
            this.txtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDatabase.Data = 0;
            this.txtDatabase.EnterColor = System.Drawing.Color.LightPink;
            this.txtDatabase.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtDatabase.Format = null;
            this.txtDatabase.isAllowNegative = false;
            this.txtDatabase.isAllowSpecialChar = false;
            this.txtDatabase.isNumberOnly = false;
            this.txtDatabase.isNumeric = false;
            this.txtDatabase.LeaveColor = System.Drawing.Color.White;
            this.txtDatabase.Location = new System.Drawing.Point(135, 58);
            this.txtDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(399, 16);
            this.txtDatabase.TabIndex = 38;
            this.txtDatabase.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDatabase.WaterMark = null;
            // 
            // txtPath
            // 
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPath.Data = 0;
            this.txtPath.EnterColor = System.Drawing.Color.LightPink;
            this.txtPath.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPath.Format = null;
            this.txtPath.isAllowNegative = false;
            this.txtPath.isAllowSpecialChar = false;
            this.txtPath.isNumberOnly = false;
            this.txtPath.isNumeric = false;
            this.txtPath.LeaveColor = System.Drawing.Color.White;
            this.txtPath.Location = new System.Drawing.Point(135, 85);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(399, 16);
            this.txtPath.TabIndex = 39;
            this.txtPath.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPath.WaterMark = null;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(72, 163);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(526, 15);
            this.progressBar.TabIndex = 37;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(141, 131);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 36;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label19.Location = new System.Drawing.Point(20, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 16);
            this.label19.TabIndex = 33;
            this.label19.Text = "Database File :";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(546, 78);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(27, 24);
            this.btnBrowse.TabIndex = 35;
            this.btnBrowse.Text = "....";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.label16.Location = new System.Drawing.Point(79, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 16);
            this.label16.TabIndex = 34;
            this.label16.Text = "Path :";
            // 
            // RestoreDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 192);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label16);
            this.Name = "RestoreDatabase";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.btnBrowse, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.progressBar, 0);
            this.Controls.SetChildIndex(this.txtPath, 0);
            this.Controls.SetChildIndex(this.txtDatabase, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
