using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class LockApplication : BaseForm
	{
		private string strPASSWORD;

		private IContainer components = null;

		private Panel panel1;

		private Button btnUnLock;

		private AshTextBoxNormal txtPassword;

		private Label label2;

		private PictureBox pictureBox1;

		private Label label1;

		public LockApplication()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnEnd.Visible = false;
		}

		private void btnUnLock_Click(object sender, EventArgs e)
		{
			strPASSWORD = AshproCommon.IdFindMethod("SELECT PassWord FROM [User] where UserId='" + Settings.Default.intLogOnId + "'");
			if (txtPassword.Text.Trim() == strPASSWORD)
			{
				Close();
			}
			else
			{
				MessageBox.Show("Password is incorrect", Utility.uManufacturer);
			}
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnUnLock_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockApplication));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnLock = new System.Windows.Forms.Button();
            this.txtPassword = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnUnLock);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Name = "panel1";
            // 
            // btnUnLock
            // 
            resources.ApplyResources(this.btnUnLock, "btnUnLock");
            this.btnUnLock.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUnLock.ForeColor = System.Drawing.Color.White;
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.UseVisualStyleBackColor = false;
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Data = 0;
            this.txtPassword.EnterColor = System.Drawing.Color.LightPink;
            this.txtPassword.Format = null;
            this.txtPassword.isAllowNegative = false;
            this.txtPassword.isAllowSpecialChar = false;
            this.txtPassword.isNumberOnly = false;
            this.txtPassword.isNumeric = false;
            this.txtPassword.LeaveColor = System.Drawing.Color.White;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // LockApplication
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "LockApplication";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
	}
}
