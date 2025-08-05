using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class ContactUs : BaseForm
	{
		private Company company;

		private DBContext db;

		private IContainer components = null;

		private AshTextBoxNormal txtFromEmailID;

		private Button btnClear;

		private RichTextBox txtMessage;

		private AshTextBoxNormal txtSubjext;

		private Label label3;

		private AshTextBoxNormal txtAddress;

		private Label label2;

		private AshTextBoxNormal txtName;

		private Label label6;

		private Button btnSend;

		private Button button4;
        private Label label1;
        private Label label4;

		public ContactUs()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void MailSending()
		{
			if (!((txtAddress.Text != "") & (txtName.Text != "") & (txtSubjext.Text != "") & (txtFromEmailID.Text != "")))
			{
			}
		}

		private void ContactUs_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			company = db.Companies().ToList().FirstOrDefault();
			if (company != null)
			{
				txtName.Text = company.InstituteName;
				txtAddress.Text = company.Address + "," + company.Pincod;
				txtFromEmailID.Text = company.EmailId;
			}
		}

		private void ContactUs_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				MailSending();
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				Close();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			MailSending();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactUs));
            this.txtFromEmailID = new AshUsercontrol.AshTextBoxNormal();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.txtSubjext = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddress = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // txtFromEmailID
            // 
            resources.ApplyResources(this.txtFromEmailID, "txtFromEmailID");
            this.txtFromEmailID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFromEmailID.Data = 0;
            this.txtFromEmailID.EnterColor = System.Drawing.Color.LightPink;
            this.txtFromEmailID.Format = null;
            this.txtFromEmailID.isAllowNegative = false;
            this.txtFromEmailID.isAllowSpecialChar = false;
            this.txtFromEmailID.isNumberOnly = false;
            this.txtFromEmailID.isNumeric = false;
            this.txtFromEmailID.LeaveColor = System.Drawing.Color.White;
            this.txtFromEmailID.Name = "txtFromEmailID";
            this.txtFromEmailID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Name = "txtMessage";
            // 
            // txtSubjext
            // 
            resources.ApplyResources(this.txtSubjext, "txtSubjext");
            this.txtSubjext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubjext.Data = 0;
            this.txtSubjext.EnterColor = System.Drawing.Color.LightPink;
            this.txtSubjext.Format = null;
            this.txtSubjext.isAllowNegative = false;
            this.txtSubjext.isAllowSpecialChar = false;
            this.txtSubjext.isNumberOnly = false;
            this.txtSubjext.isNumeric = false;
            this.txtSubjext.LeaveColor = System.Drawing.Color.White;
            this.txtSubjext.Name = "txtSubjext";
            this.txtSubjext.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Data = 0;
            this.txtAddress.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddress.Format = null;
            this.txtAddress.isAllowNegative = false;
            this.txtAddress.isAllowSpecialChar = false;
            this.txtAddress.isNumberOnly = false;
            this.txtAddress.isNumeric = false;
            this.txtAddress.LeaveColor = System.Drawing.Color.White;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Name = "btnSend";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ContactUs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFromEmailID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtSubjext);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSend);
            this.MaximizeBox = false;
            this.Name = "ContactUs";
            this.Load += new System.EventHandler(this.ContactUs_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactUs_KeyDown);
            this.Controls.SetChildIndex(this.btnSend, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtAddress, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtSubjext, 0);
            this.Controls.SetChildIndex(this.txtMessage, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtFromEmailID, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
