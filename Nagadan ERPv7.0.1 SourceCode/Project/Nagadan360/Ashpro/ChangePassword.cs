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
	public class ChangePassword : BaseForm
	{
		private DBContext db;

		private User user;

		private IContainer components = null;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private Button btnClear;

		private Button btnUpdate;

		private AshTextBoxNormal lblUser;

		private AshTextBoxNormal txtOldPWord;

		private AshTextBoxNormal txtNewPWord;

		private AshTextBoxNormal txtCnfmPWord;

		private Label lblStatus;

		private ToolTip toolTip1;

		public ChangePassword()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ChangePassword_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			base.ActiveControl = txtOldPWord;
			user = db.Users(new
			{
				UserId = Settings.Default.intLogOnId
			}).FirstOrDefault();
			lblUser.Text = user.UserName;
		}

		private void ChangePassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnUpdate_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if ((txtOldPWord.Text == "") & (txtNewPWord.Text == "") & (txtCnfmPWord.Text == ""))
			{
				Messages.WarningMessage();
				return;
			}
			if (txtOldPWord.Text.Trim() != user.PassWord)
			{
				Messages.InformationMessage("The Password You enter is not correct.");
				return;
			}
			user.PassWord = txtCnfmPWord.Text;
			if (AshproCommon.UpdateToDatabaseObj(user, "[User]", "UserId", user.UserId.toInt32()))
			{
				if (Messages.DialogMessage("Do you want to restart the Application?"))
				{
					POSLogin pOSLogin = new POSLogin(_isFromPrograms: false);
					pOSLogin.Show();
					base.ParentForm.Hide();
				}
				else
				{
					Close();
				}
			}
		}

		private void txtCnfmPWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnUpdate_Click(null, null);
			}
		}

		private void txtCnfmPWord_TextChanged(object sender, EventArgs e)
		{
			if (txtCnfmPWord.Text == txtNewPWord.Text)
			{
				lblStatus.Text = "Correct";
				lblStatus.ForeColor = Color.Green;
			}
			else
			{
				lblStatus.Text = "Not Correct";
				lblStatus.ForeColor = Color.Red;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePassword));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblUser = new AshUsercontrol.AshTextBoxNormal();
            this.txtOldPWord = new AshUsercontrol.AshTextBoxNormal();
            this.txtNewPWord = new AshUsercontrol.AshTextBoxNormal();
            this.txtCnfmPWord = new AshUsercontrol.AshTextBoxNormal();
            this.lblStatus = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Name = "btnUpdate";
            this.toolTip1.SetToolTip(this.btnUpdate, resources.GetString("btnUpdate.ToolTip"));
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblUser
            // 
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.BackColor = System.Drawing.Color.White;
            this.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblUser.Data = 0;
            this.lblUser.EnterColor = System.Drawing.Color.LightPink;
            this.lblUser.Format = null;
            this.lblUser.isAllowNegative = false;
            this.lblUser.isAllowSpecialChar = false;
            this.lblUser.isNumberOnly = false;
            this.lblUser.isNumeric = false;
            this.lblUser.LeaveColor = System.Drawing.Color.White;
            this.lblUser.Name = "lblUser";
            this.lblUser.ReadOnly = true;
            this.toolTip1.SetToolTip(this.lblUser, resources.GetString("lblUser.ToolTip"));
            this.lblUser.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtOldPWord
            // 
            resources.ApplyResources(this.txtOldPWord, "txtOldPWord");
            this.txtOldPWord.BackColor = System.Drawing.Color.White;
            this.txtOldPWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOldPWord.Data = 0;
            this.txtOldPWord.EnterColor = System.Drawing.Color.LightPink;
            this.txtOldPWord.Format = null;
            this.txtOldPWord.isAllowNegative = false;
            this.txtOldPWord.isAllowSpecialChar = false;
            this.txtOldPWord.isNumberOnly = false;
            this.txtOldPWord.isNumeric = false;
            this.txtOldPWord.LeaveColor = System.Drawing.Color.White;
            this.txtOldPWord.Name = "txtOldPWord";
            this.toolTip1.SetToolTip(this.txtOldPWord, resources.GetString("txtOldPWord.ToolTip"));
            this.txtOldPWord.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtNewPWord
            // 
            resources.ApplyResources(this.txtNewPWord, "txtNewPWord");
            this.txtNewPWord.BackColor = System.Drawing.Color.White;
            this.txtNewPWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPWord.Data = 0;
            this.txtNewPWord.EnterColor = System.Drawing.Color.LightPink;
            this.txtNewPWord.Format = null;
            this.txtNewPWord.isAllowNegative = false;
            this.txtNewPWord.isAllowSpecialChar = false;
            this.txtNewPWord.isNumberOnly = false;
            this.txtNewPWord.isNumeric = false;
            this.txtNewPWord.LeaveColor = System.Drawing.Color.White;
            this.txtNewPWord.Name = "txtNewPWord";
            this.toolTip1.SetToolTip(this.txtNewPWord, resources.GetString("txtNewPWord.ToolTip"));
            this.txtNewPWord.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtCnfmPWord
            // 
            resources.ApplyResources(this.txtCnfmPWord, "txtCnfmPWord");
            this.txtCnfmPWord.BackColor = System.Drawing.Color.White;
            this.txtCnfmPWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCnfmPWord.Data = 0;
            this.txtCnfmPWord.EnterColor = System.Drawing.Color.LightPink;
            this.txtCnfmPWord.Format = null;
            this.txtCnfmPWord.isAllowNegative = false;
            this.txtCnfmPWord.isAllowSpecialChar = false;
            this.txtCnfmPWord.isNumberOnly = false;
            this.txtCnfmPWord.isNumeric = false;
            this.txtCnfmPWord.LeaveColor = System.Drawing.Color.White;
            this.txtCnfmPWord.Name = "txtCnfmPWord";
            this.toolTip1.SetToolTip(this.txtCnfmPWord, resources.GetString("txtCnfmPWord.ToolTip"));
            this.txtCnfmPWord.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCnfmPWord.TextChanged += new System.EventHandler(this.txtCnfmPWord_TextChanged);
            this.txtCnfmPWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCnfmPWord_KeyDown);
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Name = "lblStatus";
            this.toolTip1.SetToolTip(this.lblStatus, resources.GetString("lblStatus.ToolTip"));
            // 
            // ChangePassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtCnfmPWord);
            this.Controls.Add(this.txtNewPWord);
            this.Controls.Add(this.txtOldPWord);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.Name = "ChangePassword";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangePassword_KeyDown);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.lblUser, 0);
            this.Controls.SetChildIndex(this.txtOldPWord, 0);
            this.Controls.SetChildIndex(this.txtNewPWord, 0);
            this.Controls.SetChildIndex(this.txtCnfmPWord, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
