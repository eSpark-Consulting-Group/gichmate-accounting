using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class ReportBugs : BaseForm
	{
		private Company company;

		private DBContext db;

		private string picPath;

		private IContainer components = null;

		private Button btnClear;

		private Button btnAttach;

		private RichTextBox txtMessage;

		private Label label8;

		private AshTextBoxNormal txtSubject;

		private Label label3;

		private AshTextBoxNormal txtEmailId;

		private Label label2;

		private AshTextBoxNormal txtName;

		private Label label6;

		private Button btnSend;

		private ToolTip toolTip1;

		private AshTextBoxNormal txtAttachment;

		private Button button4;

		private Label label1;

		public ReportBugs()
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
			txtMessage.Focus();
		}

		private void MailSendingTwo()
		{
			if ((txtName.Text != "") & (txtSubject.Text != "") & (txtEmailId.Text != ""))
			{
			}
			btnClear_Click(null, null);
		}

		private void btnAttach_Click(object sender, EventArgs e)
		{
			FileAttachment();
		}

		private void txtMessage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Shift && e.Control)
			{
				txtSubject.Focus();
				if (txtSubject.Text != "")
				{
					txtSubject.SelectAll();
				}
			}
		}

		private void txtSubject_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				FileAttachment();
			}
		}

		private void ReportBugs_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				MailSendingTwo();
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

		private void ReportBugs_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			company = db.Companies().FirstOrDefault();
			if (company != null)
			{
				txtName.Text = company.InstituteName + ", " + company.Address + ", " + company.Place + ", " + company.State + ", " + company.Pincod;
				txtEmailId.Text = company.EmailId;
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			MailSendingTwo();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
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
			btnAttach = new System.Windows.Forms.Button();
			txtMessage = new System.Windows.Forms.RichTextBox();
			label8 = new System.Windows.Forms.Label();
			txtSubject = new AshUsercontrol.AshTextBoxNormal();
			label3 = new System.Windows.Forms.Label();
			txtEmailId = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			btnSend = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			txtAttachment = new AshUsercontrol.AshTextBoxNormal();
			button4 = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(587, 2);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(540, 398);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(77, 30);
			btnClear.TabIndex = 24;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnAttach.BackColor = System.Drawing.Color.DodgerBlue;
			btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnAttach.ForeColor = System.Drawing.Color.White;
			btnAttach.Location = new System.Drawing.Point(540, 117);
			btnAttach.Name = "btnAttach";
			btnAttach.Size = new System.Drawing.Size(75, 30);
			btnAttach.TabIndex = 21;
			btnAttach.Text = "Attach";
			toolTip1.SetToolTip(btnAttach, "Press Alt + A");
			btnAttach.UseVisualStyleBackColor = false;
			btnAttach.Click += new System.EventHandler(btnAttach_Click);
			txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtMessage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtMessage.Location = new System.Drawing.Point(185, 156);
			txtMessage.Name = "txtMessage";
			txtMessage.Size = new System.Drawing.Size(430, 212);
			txtMessage.TabIndex = 22;
			txtMessage.Text = "";
			txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(txtMessage_KeyDown);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(0, 122);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(184, 16);
			label8.TabIndex = 19;
			label8.Text = "Screen Shot Attachment :";
			txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSubject.Data = 0;
			txtSubject.EnterColor = System.Drawing.Color.LightPink;
			txtSubject.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSubject.Format = null;
			txtSubject.isAllowNegative = false;
			txtSubject.isAllowSpecialChar = false;
			txtSubject.isNumberOnly = false;
			txtSubject.isNumeric = false;
			txtSubject.LeaveColor = System.Drawing.Color.White;
			txtSubject.Location = new System.Drawing.Point(185, 100);
			txtSubject.Margin = new System.Windows.Forms.Padding(4);
			txtSubject.Name = "txtSubject";
			txtSubject.Size = new System.Drawing.Size(430, 16);
			txtSubject.TabIndex = 18;
			txtSubject.Value = new decimal(new int[4]);
			txtSubject.WaterMark = null;
			txtSubject.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSubject_KeyDown);
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(114, 98);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(70, 16);
			label3.TabIndex = 17;
			label3.Text = "Subject :";
			txtEmailId.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtEmailId.Data = 0;
			txtEmailId.EnterColor = System.Drawing.Color.LightPink;
			txtEmailId.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtEmailId.Format = null;
			txtEmailId.isAllowNegative = false;
			txtEmailId.isAllowSpecialChar = false;
			txtEmailId.isNumberOnly = false;
			txtEmailId.isNumeric = false;
			txtEmailId.LeaveColor = System.Drawing.Color.White;
			txtEmailId.Location = new System.Drawing.Point(185, 75);
			txtEmailId.Margin = new System.Windows.Forms.Padding(4);
			txtEmailId.Name = "txtEmailId";
			txtEmailId.Size = new System.Drawing.Size(430, 16);
			txtEmailId.TabIndex = 16;
			txtEmailId.Value = new decimal(new int[4]);
			txtEmailId.WaterMark = null;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(75, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(109, 16);
			label2.TabIndex = 15;
			label2.Text = "Email Address :";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Location = new System.Drawing.Point(185, 50);
			txtName.Margin = new System.Windows.Forms.Padding(4);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(430, 16);
			txtName.TabIndex = 14;
			txtName.Value = new decimal(new int[4]);
			txtName.WaterMark = null;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(43, 46);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(141, 16);
			label6.TabIndex = 13;
			label6.Text = "Name and Address :";
			btnSend.BackColor = System.Drawing.Color.DodgerBlue;
			btnSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSend.ForeColor = System.Drawing.Color.White;
			btnSend.Location = new System.Drawing.Point(185, 373);
			btnSend.Name = "btnSend";
			btnSend.Size = new System.Drawing.Size(77, 30);
			btnSend.TabIndex = 23;
			btnSend.Text = "Send";
			toolTip1.SetToolTip(btnSend, "Press Alt + S");
			btnSend.UseVisualStyleBackColor = false;
			btnSend.Click += new System.EventHandler(btnSend_Click);
			txtAttachment.BackColor = System.Drawing.Color.White;
			txtAttachment.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAttachment.Data = 0;
			txtAttachment.EnterColor = System.Drawing.Color.LightPink;
			txtAttachment.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAttachment.Format = null;
			txtAttachment.isAllowNegative = false;
			txtAttachment.isAllowSpecialChar = false;
			txtAttachment.isNumberOnly = false;
			txtAttachment.isNumeric = false;
			txtAttachment.LeaveColor = System.Drawing.Color.White;
			txtAttachment.Location = new System.Drawing.Point(185, 125);
			txtAttachment.Margin = new System.Windows.Forms.Padding(4);
			txtAttachment.Name = "txtAttachment";
			txtAttachment.Size = new System.Drawing.Size(349, 16);
			txtAttachment.TabIndex = 18;
			txtAttachment.Value = new decimal(new int[4]);
			txtAttachment.WaterMark = null;
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(139, 1);
			button4.TabIndex = 54;
			button4.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(7, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(125, 24);
			label1.TabIndex = 53;
			label1.Text = "Report Bugs";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(622, 432);
			base.Controls.Add(button4);
			base.Controls.Add(label1);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnAttach);
			base.Controls.Add(txtMessage);
			base.Controls.Add(label8);
			base.Controls.Add(txtAttachment);
			base.Controls.Add(txtSubject);
			base.Controls.Add(label3);
			base.Controls.Add(txtEmailId);
			base.Controls.Add(label2);
			base.Controls.Add(txtName);
			base.Controls.Add(label6);
			base.Controls.Add(btnSend);
			base.MaximizeBox = false;
			base.Name = "ReportBugs";
			base.Load += new System.EventHandler(ReportBugs_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ReportBugs_KeyDown);
			base.Controls.SetChildIndex(btnSend, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtEmailId, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtSubject, 0);
			base.Controls.SetChildIndex(txtAttachment, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(txtMessage, 0);
			base.Controls.SetChildIndex(btnAttach, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
