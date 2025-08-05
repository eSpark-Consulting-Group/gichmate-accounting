using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ReportForm : Form
	{
		private string strFormType;

		private string strConnection;

		private string strFirm;

		private string strEmailId;

		private int intLivRowCount;

		private string strFirstAct;

		private string strFYearId;

		private string strFromDate;

		private string strQuery;

		private string strPartyAccDetails;

		private string strRefNoDetails;

		private string strPartyAcctId;

		private decimal dmlBalance;

		private decimal dmlAmnt;

		private bool isPrinted;

		private bool isNotFirst;

		private int linesPrinted;

		private int y;

		private IContainer components = null;

		private ToolTip toolTip1;

		private Button btnShow;

		private Panel panel1;

		private DataGridView dataGridView1;

		private Button btnClear;

		private Button btnClose;

		private Button btnExcel;

		private Button btnPrint;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private PrintDocument printDocument1;

		private AshComboBox cmbPartyAccnt;

		private Label label3;

		private Label label2;

		private Label label1;

		private Label label4;

		private AshTextBoxNormal txtRefNo;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		public ReportForm(string strType)
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
			PrintController printController = new StandardPrintController();
			printDocument1.PrintController = printController;
			strFormType = strType;
		}

		private void FirmNameFindMethod()
		{
			strQuery = "SELECT InstituteName,EmailId FROM Institute";
			strFirm = AshproCommon.IdFindMethod("Select InstituteName From  Institute");
			strEmailId = AshproCommon.IdFindMethod("Select EmailId From  Institute");
		}

		private void ClearMethod()
		{
			cmbPartyAccnt.Text = "";
			txtRefNo.Text = "";
			FinanceialYearDate();
			if (dataGridView1.DataSource != null)
			{
				dataGridView1.DataSource = null;
			}
			else
			{
				dataGridView1.Rows.Clear();
			}
			cmbPartyAccnt.Focus();
		}

		private void ReportShowMethod()
		{
			if (cmbPartyAccnt.Text != "")
			{
				AshproCommon.IdFindMethod("Account", "AccountId", "AccountName", cmbPartyAccnt.Text, out strPartyAcctId);
				strPartyAccDetails = " and OppAccountId='" + strPartyAcctId + "'";
			}
			else
			{
				strPartyAccDetails = "";
			}
			if (txtRefNo.Text != " and RefNo='" + txtRefNo.Text + "'")
			{
				strRefNoDetails = "";
			}
			else
			{
				strRefNoDetails = "";
			}
			strQuery = "Select distinct VchrType From TransactionLog where Status='Main' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' and VchrName='" + strFormType + "'";
			LivLoadMethodTwo(strQuery);
		}

		private void LivLoadMethodTwo(string strQuery)
		{
			dmlBalance = default(decimal);
			if (dataGridView1.DataSource != null)
			{
				dataGridView1.DataSource = null;
			}
			else
			{
				dataGridView1.Rows.Clear();
			}
			intLivRowCount = 1;
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					switch (row[0].ToString())
					{
					case "Purchase Voucher":
						LoadListViewMethod("Purchase Voucher", "DebitAmount", "CreditAmount");
						break;
					case "Purchase Return Voucher":
						LoadListViewMethod("Purchase Return Voucher", "CreditAmount", "DebitAmount");
						break;
					case "Sales Return Voucher":
						LoadListViewMethod("Sales Return Voucher", "DebitAmount", "CreditAmount");
						break;
					case "Sales Voucher":
						LoadListViewMethod("Sales Voucher", "CreditAmount", "DebitAmount");
						break;
					case "Payment Voucher":
						LoadListViewMethod("Payment Voucher", "CreditAmount", "DebitAmount");
						break;
					case "Receipt Voucher":
						LoadListViewMethod("Receipt Voucher", "DebitAmount", "CreditAmount");
						break;
					case "Journal Voucher":
						LoadListViewMethod("Journal Voucher", "CreditAmount", "DebitAmount");
						break;
					}
				}
				dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
				int num = 1;
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows[i].Cells[0].Value = num;
					num++;
				}
				dataGridView1.Rows.Add("", "", "", "Total", dmlBalance.ToString(CommonSettings.strCurencyFormat), "", "");
				for (int j = 0; j < dataGridView1.Rows.Count; j++)
				{
					if (dataGridView1.Rows[j].Cells[0].Value.ToString() == "")
					{
						dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Red;
						dataGridView1.Rows[j].DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 420");
			}
		}

		private void LoadListViewMethod(string strVchrType, string strAmountClm, string strWhrAmmntClm)
		{
			dmlAmnt = default(decimal);
			try
			{
				strQuery = "Select VchrDate,VchrNo,OppAccountId," + strAmountClm + ",RefNo,Narration From TransactionLog Where VchrType='" + strVchrType + "' and Status='Main' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date) + "' " + strPartyAccDetails + strRefNoDetails;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[2].ToString(), out strFirstAct);
					DateTime dateTime = Convert.ToDateTime(row[0].ToString());
					dmlAmnt = Convert.ToDecimal(row[3].ToString());
					dmlBalance += dmlAmnt;
					dataGridView1.Rows.Add(intLivRowCount.ToString(), dateTime.ToString("dd-MMM-yyyy"), row[1].ToString(), strFirstAct, row[3].ToString(), row[4].ToString(), row[5].ToString());
					intLivRowCount++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 474");
			}
		}

		private void FinanceialYearDate()
		{
			strFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
			try
			{
				AshproCommon.IdFindMethod("FinancialYear", "DateFrom", "FyearId", strFYearId, out strFromDate);
				DateTime dateTime = Convert.ToDateTime(strFromDate);
				int days = (DateTime.Now - dateTime).Days;
				if (days < 91)
				{
					dateTime = dtmFrom.Value.AddDays(-(dtmFrom.Value.DayOfYear + 274));
				}
				dtmFrom.Value = dateTime;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PrintMethod()
		{
			if (dataGridView1.Rows.Count > 0)
			{
				FirmNameFindMethod();
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			try
			{
				//AniHelper.ExportToExcelMethod(dataGridView1, Text + " from " + dtmFrom.Text + " to " + dtmTo.Text);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			ReportShowMethod();
		}

		private void ReportForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				ReportShowMethod();
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnExcel_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				ClearMethod();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				if (dataGridView1.Rows.Count > 0)
				{
					e.SuppressKeyPress = true;
					dataGridView1.Focus();
					dataGridView1.Rows[0].Cells[0].Selected = true;
				}
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				PrintMethod();
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbPartyAccnt.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void ReportForm_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			Text = strFormType + " Report";
			strConnection = Settings.Default.strConnetionString;
			FirmNameFindMethod();
			FinanceialYearDate();
			AshproCommon.FillCombo(cmbPartyAccnt, "Select AccountId as Id,AccountName as Name From Account");
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
					e.Graphics.DrawLine(Pens.Black, 45, y, 45, num);
					e.Graphics.DrawLine(Pens.Black, 135, y, 135, num);
					e.Graphics.DrawLine(Pens.Black, 205, y, 205, num);
					e.Graphics.DrawLine(Pens.Black, 540, y, 540, num);
					e.Graphics.DrawLine(Pens.Black, 640, y, 640, num);
					e.Graphics.DrawString(strFirm, new Font("Courier New", 14f, FontStyle.Bold | FontStyle.Underline), Brushes.Black, new Point(400, 10), stringFormat2);
					e.Graphics.DrawString(Text + " From " + dtmFrom.Text + " To " + dtmTo.Text, new Font("Courier New", 12f, FontStyle.Bold), Brushes.Black, new Point(10, 40));
					e.Graphics.DrawLine(Pens.Black, 8, 100, e.MarginBounds.Right + 47, 100);
					e.Graphics.DrawString("SL", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(9, 70));
					e.Graphics.DrawString("Date", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(48, 70));
					e.Graphics.DrawString("Inv No", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(138, 70));
					e.Graphics.DrawString("Party Name", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(208, 70));
					e.Graphics.DrawString("Amount", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(543, 70));
					e.Graphics.DrawString("Ref No", new Font("Times New Roman", 12f, FontStyle.Bold), Brushes.Black, new Point(643, 70));
					isPrinted = true;
					y = 100;
				}
				if (isNotFirst)
				{
					y = 15;
					e.Graphics.DrawRectangle(Pens.Black, 8, y, e.MarginBounds.Right + 40, num + 25);
					num += 40;
					e.Graphics.DrawLine(Pens.Black, 45, y, 45, num);
					e.Graphics.DrawLine(Pens.Black, 135, y, 135, num);
					e.Graphics.DrawLine(Pens.Black, 205, y, 205, num);
					e.Graphics.DrawLine(Pens.Black, 540, y, 540, num);
					e.Graphics.DrawLine(Pens.Black, 640, y, 640, num);
				}
				while (true)
				{
					if (linesPrinted < dataGridView1.Rows.Count)
					{
						string text = dataGridView1.Rows[linesPrinted].Cells[0].Value.ToString();
						string s = dataGridView1.Rows[linesPrinted].Cells[1].Value.ToString();
						string s2 = dataGridView1.Rows[linesPrinted].Cells[2].Value.ToString();
						string s3 = dataGridView1.Rows[linesPrinted].Cells[3].Value.ToString();
						string s4 = dataGridView1.Rows[linesPrinted].Cells[4].Value.ToString();
						string s5 = dataGridView1.Rows[linesPrinted].Cells[5].Value.ToString();
						e.Graphics.DrawString(text, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(8, y));
						if (text == "")
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new RectangleF(47f, y, 230f, 15f));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(137, y));
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(207, y));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(640, y), stringFormat);
							e.Graphics.DrawString(s5, new Font("Times New Roman", 10f, FontStyle.Bold), Brushes.Black, new Point(642, y));
						}
						else
						{
							e.Graphics.DrawString(s, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(47, y));
							e.Graphics.DrawString(s2, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(137, y));
							e.Graphics.DrawString(s3, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(207, y));
							e.Graphics.DrawString(s4, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(640, y), stringFormat);
							e.Graphics.DrawString(s5, new Font("Times New Roman", 10f, FontStyle.Regular), Brushes.Black, new Point(642, y));
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			btnShow = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btnClear = new System.Windows.Forms.Button();
			btnClose = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			printDocument1 = new System.Drawing.Printing.PrintDocument();
			cmbPartyAccnt = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			txtRefNo = new AshUsercontrol.AshTextBoxNormal();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(460, 23);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 37;
			btnShow.Text = "Show";
			toolTip1.SetToolTip(btnShow, "Press Alt + S");
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dataGridView1);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(12, 87);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(1333, 540);
			panel1.TabIndex = 43;
			toolTip1.SetToolTip(panel1, "Press Alt + G");
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4, Column5, Column6, Column7);
			dataGridView1.GridColor = System.Drawing.Color.Black;
			dataGridView1.Location = new System.Drawing.Point(12, 14);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dataGridView1.Size = new System.Drawing.Size(1309, 512);
			dataGridView1.TabIndex = 0;
			Column1.HeaderText = "SL";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Width = 50;
			Column2.HeaderText = "Date";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column3.HeaderText = "Inv. No";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column4.HeaderText = "Party Name";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Width = 300;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			Column5.DefaultCellStyle = dataGridViewCellStyle2;
			Column5.HeaderText = "Amount";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.Width = 120;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			Column6.DefaultCellStyle = dataGridViewCellStyle3;
			Column6.HeaderText = "Reference";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column6.Width = 120;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			Column7.DefaultCellStyle = dataGridViewCellStyle4;
			Column7.HeaderText = "Narration";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column7.Width = 400;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(718, 23);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 41;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Location = new System.Drawing.Point(804, 23);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(75, 30);
			btnClose.TabIndex = 42;
			btnClose.Text = "Close";
			toolTip1.SetToolTip(btnClose, "Press Alt + X");
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(632, 23);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(75, 30);
			btnExcel.TabIndex = 40;
			btnExcel.Text = "Excel";
			toolTip1.SetToolTip(btnExcel, "Press Alt + E");
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Location = new System.Drawing.Point(546, 23);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(75, 30);
			btnPrint.TabIndex = 38;
			btnPrint.Text = "Print";
			toolTip1.SetToolTip(btnPrint, "Press Alt + P");
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(300, 60);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 34;
			toolTip1.SetToolTip(dtmTo, "Press Alt + T");
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(135, 58);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 23);
			dtmFrom.TabIndex = 32;
			toolTip1.SetToolTip(dtmFrom, "Press Alt + F");
			printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
			cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPartyAccnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPartyAccnt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbPartyAccnt.FormattingEnabled = true;
			cmbPartyAccnt.Location = new System.Drawing.Point(135, 3);
			cmbPartyAccnt.Name = "cmbPartyAccnt";
			cmbPartyAccnt.Size = new System.Drawing.Size(291, 24);
			cmbPartyAccnt.TabIndex = 23;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(18, 6);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(114, 16);
			label3.TabIndex = 22;
			label3.Text = "Party Account :";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(271, 64);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(31, 16);
			label2.TabIndex = 33;
			label2.Text = "To:";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(24, 64);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(82, 16);
			label1.TabIndex = 31;
			label1.Text = "Date From:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(25, 31);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(107, 16);
			label4.TabIndex = 44;
			label4.Text = "Reference No :";
			txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRefNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtRefNo.isAllowNegative = false;
			txtRefNo.isAllowSpecialChar = false;
			txtRefNo.isNumberOnly = false;
			txtRefNo.isNumeric = false;
			txtRefNo.Location = new System.Drawing.Point(135, 34);
			txtRefNo.Name = "txtRefNo";
			txtRefNo.Size = new System.Drawing.Size(291, 16);
			txtRefNo.TabIndex = 45;
			txtRefNo.WaterMark = null;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1354, 636);
			base.Controls.Add(txtRefNo);
			base.Controls.Add(label4);
			base.Controls.Add(cmbPartyAccnt);
			base.Controls.Add(label3);
			base.Controls.Add(btnShow);
			base.Controls.Add(panel1);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnClose);
			base.Controls.Add(btnExcel);
			base.Controls.Add(btnPrint);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label2);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.Name = "ReportForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Report Form";
			base.Load += new System.EventHandler(ReportForm_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ReportForm_KeyDown);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
