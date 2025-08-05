using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class SendMail : BaseForm
	{
		private string picPath;

		private Company company;

		private DBContext db;

		private IContainer components = null;

		private Button btnClear;

		private Button btnAttachment;

		private RichTextBox rtbMessage;

		private Label label8;

		private AshTextBoxNormal txtSubject;

		private Label label3;

		private AshTextBoxNormal txtTo;

		private Label label2;

		private AshTextBoxNormal txtFrom;

		private Label label6;

		private Button btnSend;

		private ToolTip toolTip1;

		private AshTextBoxNormal txtAttachment;

		private Button button4;

		private Label label1;

		public SendMail()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void FileAttachment()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Open File";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					picPath = openFileDialog.FileName.ToString();
					txtAttachment.Text = picPath;
				}
				openFileDialog.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			rtbMessage.Focus();
		}

		private void MailSending()
		{
			if ((txtTo.Text != "") & (txtFrom.Text != "") & (txtSubject.Text != ""))
			{
				try
				{
					MailMessage mailMessage = new MailMessage(txtFrom.Text, txtTo.Text, txtSubject.Text, rtbMessage.Text);
					if (txtAttachment.Text != "")
					{
						mailMessage.Attachments.Add(new Attachment(picPath));
					}
					SmtpClient smtpClient = new SmtpClient(CommonSettings.strClientName);
					smtpClient.Port = CommonSettings.strClientPort.ToInt32();
					smtpClient.Credentials = new NetworkCredential(CommonSettings.strUserName, CommonSettings.strPassword);
					smtpClient.EnableSsl = true;
					smtpClient.Send(mailMessage);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				btnClear_Click(null, null);
			}
			else
			{
				MessageBox.Show("Please fill all field", "Error");
			}
		}

		private void SendMail_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			company = db.Companies().FirstOrDefault();
			txtFrom.Text = company.EmailId;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			MailSending();
		}

		private void btnAttachment_Click(object sender, EventArgs e)
		{
			FileAttachment();
		}

		private void SendMail_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				MailSending();
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.A && e.Alt)
			{
				e.SuppressKeyPress = true;
				FileAttachment();
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
			components = new System.ComponentModel.Container();
			btnClear = new System.Windows.Forms.Button();
			btnAttachment = new System.Windows.Forms.Button();
			rtbMessage = new System.Windows.Forms.RichTextBox();
			label8 = new System.Windows.Forms.Label();
			txtSubject = new AshUsercontrol.AshTextBoxNormal();
			label3 = new System.Windows.Forms.Label();
			txtTo = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			txtFrom = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			btnSend = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			txtAttachment = new AshUsercontrol.AshTextBoxNormal();
			button4 = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(599, 1);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(549, 398);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(77, 30);
			btnClear.TabIndex = 37;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnAttachment.BackColor = System.Drawing.Color.DodgerBlue;
			btnAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnAttachment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnAttachment.ForeColor = System.Drawing.Color.White;
			btnAttachment.Location = new System.Drawing.Point(549, 119);
			btnAttachment.Name = "btnAttachment";
			btnAttachment.Size = new System.Drawing.Size(75, 30);
			btnAttachment.TabIndex = 34;
			btnAttachment.Text = "Attach";
			toolTip1.SetToolTip(btnAttachment, "Press Alt + A");
			btnAttachment.UseVisualStyleBackColor = false;
			btnAttachment.Click += new System.EventHandler(btnAttachment_Click);
			rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			rtbMessage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rtbMessage.Location = new System.Drawing.Point(194, 154);
			rtbMessage.Name = "rtbMessage";
			rtbMessage.Size = new System.Drawing.Size(430, 212);
			rtbMessage.TabIndex = 35;
			rtbMessage.Text = "";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(7, 122);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(184, 16);
			label8.TabIndex = 32;
			label8.Text = "Screen Shot Attachment :";
			txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSubject.Data = 0;
			txtSubject.EnterColor = System.Drawing.Color.LightPink;
			txtSubject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtSubject.Format = null;
			txtSubject.isAllowNegative = false;
			txtSubject.isAllowSpecialChar = false;
			txtSubject.isNumberOnly = false;
			txtSubject.isNumeric = false;
			txtSubject.LeaveColor = System.Drawing.Color.White;
			txtSubject.Location = new System.Drawing.Point(192, 100);
			txtSubject.Margin = new System.Windows.Forms.Padding(4);
			txtSubject.Name = "txtSubject";
			txtSubject.Size = new System.Drawing.Size(430, 16);
			txtSubject.TabIndex = 31;
			txtSubject.Value = new decimal(new int[4]);
			txtSubject.WaterMark = null;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(121, 98);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(70, 16);
			label3.TabIndex = 30;
			label3.Text = "Subject :";
			txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtTo.Data = 0;
			txtTo.EnterColor = System.Drawing.Color.LightPink;
			txtTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtTo.Format = null;
			txtTo.isAllowNegative = false;
			txtTo.isAllowSpecialChar = false;
			txtTo.isNumberOnly = false;
			txtTo.isNumeric = false;
			txtTo.LeaveColor = System.Drawing.Color.White;
			txtTo.Location = new System.Drawing.Point(192, 75);
			txtTo.Margin = new System.Windows.Forms.Padding(4);
			txtTo.Name = "txtTo";
			txtTo.Size = new System.Drawing.Size(430, 16);
			txtTo.TabIndex = 29;
			txtTo.Value = new decimal(new int[4]);
			txtTo.WaterMark = null;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(110, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(81, 16);
			label2.TabIndex = 28;
			label2.Text = "To e-mail :";
			txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFrom.Data = 0;
			txtFrom.EnterColor = System.Drawing.Color.LightPink;
			txtFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtFrom.Format = null;
			txtFrom.isAllowNegative = false;
			txtFrom.isAllowSpecialChar = false;
			txtFrom.isNumberOnly = false;
			txtFrom.isNumeric = false;
			txtFrom.LeaveColor = System.Drawing.Color.White;
			txtFrom.Location = new System.Drawing.Point(192, 50);
			txtFrom.Margin = new System.Windows.Forms.Padding(4);
			txtFrom.Name = "txtFrom";
			txtFrom.Size = new System.Drawing.Size(430, 16);
			txtFrom.TabIndex = 27;
			txtFrom.Value = new decimal(new int[4]);
			txtFrom.WaterMark = null;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(95, 46);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(96, 16);
			label6.TabIndex = 26;
			label6.Text = "From e-mail :";
			btnSend.BackColor = System.Drawing.Color.DodgerBlue;
			btnSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSend.ForeColor = System.Drawing.Color.White;
			btnSend.Location = new System.Drawing.Point(194, 375);
			btnSend.Name = "btnSend";
			btnSend.Size = new System.Drawing.Size(77, 30);
			btnSend.TabIndex = 36;
			btnSend.Text = "Send";
			toolTip1.SetToolTip(btnSend, "Press Alt + S");
			btnSend.UseVisualStyleBackColor = false;
			btnSend.Click += new System.EventHandler(btnSend_Click);
			txtAttachment.BackColor = System.Drawing.Color.White;
			txtAttachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAttachment.Data = 0;
			txtAttachment.EnterColor = System.Drawing.Color.LightPink;
			txtAttachment.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtAttachment.Format = null;
			txtAttachment.isAllowNegative = false;
			txtAttachment.isAllowSpecialChar = false;
			txtAttachment.isNumberOnly = false;
			txtAttachment.isNumeric = false;
			txtAttachment.LeaveColor = System.Drawing.Color.White;
			txtAttachment.Location = new System.Drawing.Point(192, 125);
			txtAttachment.Margin = new System.Windows.Forms.Padding(4);
			txtAttachment.Name = "txtAttachment";
			txtAttachment.ReadOnly = true;
			txtAttachment.Size = new System.Drawing.Size(351, 16);
			txtAttachment.TabIndex = 31;
			txtAttachment.Value = new decimal(new int[4]);
			txtAttachment.WaterMark = null;
			button4.Location = new System.Drawing.Point(0, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(115, 1);
			button4.TabIndex = 51;
			button4.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(6, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(103, 24);
			label1.TabIndex = 50;
			label1.Text = "Send Mail";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(635, 433);
			base.Controls.Add(button4);
			base.Controls.Add(label1);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnAttachment);
			base.Controls.Add(rtbMessage);
			base.Controls.Add(label8);
			base.Controls.Add(txtAttachment);
			base.Controls.Add(txtSubject);
			base.Controls.Add(label3);
			base.Controls.Add(txtTo);
			base.Controls.Add(label2);
			base.Controls.Add(txtFrom);
			base.Controls.Add(label6);
			base.Controls.Add(btnSend);
			base.MaximizeBox = false;
			base.Name = "SendMail";
			base.Load += new System.EventHandler(SendMail_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SendMail_KeyDown);
			base.Controls.SetChildIndex(btnSend, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(txtFrom, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtTo, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtSubject, 0);
			base.Controls.SetChildIndex(txtAttachment, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(rtbMessage, 0);
			base.Controls.SetChildIndex(btnAttachment, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
