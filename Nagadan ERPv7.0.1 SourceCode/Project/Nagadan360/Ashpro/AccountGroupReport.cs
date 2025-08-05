using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class AccountGroupReport : BaseForm
	{
		private Company company;

		private DataTable dataTable = new DataTable();

		private string Query;

		private string strAccName;

		private decimal dmlAccTotal;

		private decimal dmlDbt;

		private decimal dmlCrt;

		private decimal dmlDbtTotal;

		private decimal dmlCrtTotal;

		private decimal dmlBalance;

		private int intDGVCount;

		private string strEmailId;

		private bool isPrinted;

		private bool isNotFirst;

		private int linesPrinted;

		private int y;

		private IContainer components = null;

		private Panel panel1;

		private DataGridView dgvReport;

		private Label lblReport;

		private PrintDocument printDocument1;

		private ToolTip toolTip1;

		private AshButton btnExcel;

		private AshButton btnPrint;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column1;
        private AshButton btnMail;

		public string PassingValue { get; set; }

		public string PassingValueTwo { get; set; }

		public string PassingValueThree { get; set; }

		public bool PassingValueFour { get; set; }

		public AccountGroupReport()
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
			PrintController printController = new StandardPrintController();
			printDocument1.PrintController = printController;
		}

		private void LoadDGVMethod()
		{
			if (PassingValueTwo != "")
			{
				Query = "Select AccountId,AccountName From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId where AccountGroup.Under='" + PassingValue + "' order by AccountName";
			}
			else
			{
				Query = "Select AccountId,AccountName From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId where AccountGroup.GroupName='" + PassingValue + "' order by AccountName";
			}
			intDGVCount = 1;
			try
			{
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					dmlAccTotal = default(decimal);
					dmlDbt = default(decimal);
					dmlCrt = default(decimal);
					if (PassingValueFour)
					{
						dmlAccTotal = AshproCommon.ValueFindMethod("Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog where AccountId='" + row[0].ToString() + "' and VchrDate between'" + PassingValueTwo + "' and '" + PassingValueThree + "'");
					}
					else
					{
						dmlAccTotal = AshproCommon.ValueFindMethod("Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog  where AccountId='" + row[0].ToString() + "' and VchrDate<'" + PassingValueThree + "'");
					}
					strAccName = row[1].ToString();
					if (dmlAccTotal > 0m)
					{
						dmlDbt = dmlAccTotal;
						dmlCrt = default(decimal);
					}
					else if (dmlAccTotal < 0m)
					{
						dmlDbt = default(decimal);
						dmlCrt = Math.Abs(dmlAccTotal);
					}
					dmlDbtTotal += dmlDbt;
					dmlCrtTotal += dmlCrt;
					dmlBalance = dmlBalance + dmlDbt - dmlCrt;
					dgvReport.Rows.Add(intDGVCount, strAccName, dmlDbt.ToString("F"), dmlCrt.ToString("F"), dmlBalance.ToString("F"));
					intDGVCount++;
				}
				dgvReport.Rows.Add(" ", "Total", dmlDbtTotal.ToString("F"), dmlCrtTotal.ToString("F"), dmlBalance.ToString("F"));
				if (PassingValueFour)
				{
					lblReport.Text = "Account Group Report of " + PassingValue + " From " + PassingValueTwo + " To " + PassingValueThree;
				}
				else
				{
					lblReport.Text = "Account Group Report of " + PassingValue + " As on " + PassingValueThree;
				}
				for (int i = 0; i < dgvReport.Rows.Count; i++)
				{
					if (dgvReport.Rows[i].Cells[0].Value.ToString() == " ")
					{
						dgvReport.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
						dgvReport.Rows[i].DefaultCellStyle.Font = new Font(dgvReport.Font, FontStyle.Bold);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void PrintMethod()
		{
			if (dgvReport.Rows.Count > 0)
			{
				isPrinted = false;
				isNotFirst = false;
				linesPrinted = 0;
				printDocument1.Print();
			}
			else
			{
				MessageBox.Show("There is no data to print in the table");
			}
		}

		private void MailMethod()
		{
			if (dgvReport.Rows.Count > 0)
			{
				isNotFirst = false;
				isPrinted = false;
				linesPrinted = 0;
				MailSendMethod();
			}
			else
			{
				MessageBox.Show("There is no data in the table to send mail.");
			}
		}

		private void MailSendMethod()
		{
			AniHelper.PDFGenerateMethod(dgvReport, lblReport.Text);
			try
			{
				if (company.InstituteName == null || company.InstituteName == "")
				{
					MyMsgBox myMsgBox = new MyMsgBox();
					myMsgBox.Passvalue = "Mail Address";
					myMsgBox.ShowDialog();
					strEmailId = myMsgBox.Passvalue;
				}
				int port = CommonSettings.strClientPort.ToInt32();
				string strEmailAddress = CommonSettings.strEmailAddress;
				string to = strEmailId;
				string text = lblReport.Text;
				string strClientName = CommonSettings.strClientName;
				string strUserName = CommonSettings.strUserName;
				string strPassword = CommonSettings.strPassword;
				string body = "Dear Sir/Madam,\n      \n" + text + "\n       \n        \nThanks and Regards\nAccounts Team\n" + company.InstituteName;
				try
				{
					using MailMessage mailMessage = new MailMessage(strEmailAddress, to, text, body);
					mailMessage.Attachments.Add(new Attachment("Report.pdf"));
					SmtpClient smtpClient = new SmtpClient(strClientName);
					smtpClient.Port = port;
					smtpClient.Credentials = new NetworkCredential(strUserName, strPassword);
					smtpClient.EnableSsl = true;
					smtpClient.Send(mailMessage);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void AccountGroupReport_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			company = AniHelper.GetCompany();
			LoadDGVMethod();
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//AniHelper.ExportToExcelMethod(dgvReport, lblReport.Text);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			PrintMethod();
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				_ = e.MarginBounds.Left;
				y = e.MarginBounds.Top;
				int num = e.MarginBounds.Bottom;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = StringAlignment.Center;
				if (!isPrinted)
				{
					y = 60;
					e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num - 20);
					num += 40;
					e.Graphics.DrawLine(Pens.Black, 60, y, 60, num);
					e.Graphics.DrawLine(Pens.Black, 360, y, 360, num);
					e.Graphics.DrawLine(Pens.Black, 490, y, 490, num);
					e.Graphics.DrawLine(Pens.Black, 620, y, 620, num);
					e.Graphics.DrawString(company.InstituteName, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
					e.Graphics.DrawString(lblReport.Text, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
					e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
					e.Graphics.DrawString("SL", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(8, 70));
					e.Graphics.DrawString("Account Name", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(61, 70));
					e.Graphics.DrawString("Debit Amount", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(361, 70));
					e.Graphics.DrawString("Credit Amount", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(491, 70));
					e.Graphics.DrawString("Balance Amount", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(621, 70));
					isPrinted = true;
					y = 100;
				}
				if (isNotFirst)
				{
					y = 15;
					e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num + 25);
					num += 40;
					e.Graphics.DrawLine(Pens.Black, 60, y, 60, num);
					e.Graphics.DrawLine(Pens.Black, 360, y, 360, num);
					e.Graphics.DrawLine(Pens.Black, 490, y, 490, num);
					e.Graphics.DrawLine(Pens.Black, 620, y, 620, num);
				}
				while (true)
				{
					if (linesPrinted < dgvReport.Rows.Count)
					{
						string text = dgvReport.Rows[linesPrinted].Cells[0].Value.ToString();
						string s = dgvReport.Rows[linesPrinted].Cells[1].Value.ToString();
						string s2 = dgvReport.Rows[linesPrinted].Cells[2].Value.ToString();
						string s3 = dgvReport.Rows[linesPrinted].Cells[3].Value.ToString();
						string s4 = dgvReport.Rows[linesPrinted].Cells[4].Value.ToString();
						e.Graphics.DrawString(text, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(8, y));
						if (text == " ")
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(61f, y, 300f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(490, y), stringFormat);
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(620, y), stringFormat);
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(769, y), stringFormat);
						}
						else
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new RectangleF(61f, y, 300f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(490, y), stringFormat);
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(620, y), stringFormat);
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(769, y), stringFormat);
						}
						linesPrinted++;
						y += 20;
						if (y >= e.MarginBounds.Bottom + 30)
						{
							break;
						}
						e.Graphics.DrawLine(Pens.Black, 8, y - 2, 776, y - 2);
						continue;
					}
					linesPrinted = 0;
					isNotFirst = false;
					return;
				}
				isNotFirst = true;
				e.HasMorePages = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
			MailMethod();
		}

		private void AccountGroupReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				MailMethod();
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnExcel_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				PrintMethod();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountGroupReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblReport = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnExcel = new AshUsercontrol.AshButton();
            this.btnPrint = new AshUsercontrol.AshButton();
            this.btnMail = new AshUsercontrol.AshButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.dgvReport);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // dgvReport
            // 
            resources.ApplyResources(this.dgvReport, "dgvReport");
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column7,
            this.Column5,
            this.Column6,
            this.Column1});
            this.dgvReport.GridColor = System.Drawing.Color.Black;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dgvReport, resources.GetString("dgvReport.ToolTip"));
            // 
            // Column8
            // 
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column7
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column5
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column1
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // lblReport
            // 
            resources.ApplyResources(this.lblReport, "lblReport");
            this.lblReport.Name = "lblReport";
            this.toolTip1.SetToolTip(this.lblReport, resources.GetString("lblReport.ToolTip"));
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // btnExcel
            // 
            resources.ApplyResources(this.btnExcel, "btnExcel");
            this.btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Name = "btnExcel";
            this.toolTip1.SetToolTip(this.btnExcel, resources.GetString("btnExcel.ToolTip"));
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.toolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnMail
            // 
            resources.ApplyResources(this.btnMail, "btnMail");
            this.btnMail.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMail.FlatAppearance.BorderSize = 0;
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.Name = "btnMail";
            this.toolTip1.SetToolTip(this.btnMail, resources.GetString("btnMail.ToolTip"));
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // AccountGroupReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.panel1);
            this.Name = "AccountGroupReport";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.AccountGroupReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountGroupReport_KeyDown);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblReport, 0);
            this.Controls.SetChildIndex(this.btnMail, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnExcel, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
