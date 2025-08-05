using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class TransactionReportView : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string sTitle;

		private string strQuery;

		private IContainer components = null;

		private RadioButton rdbCredit;

		private RadioButton rdbDebit;

		private RadioButton rdbAll;

		private Label label5;

		private Label label6;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private Panel panel1;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		private Panel panel2;

		public DashboardPOS form { get; set; }

		public TransactionReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string[] headers = new string[11]
			{
				"Date", "Inv. No", "Vchr Type", "Account Name", "Particulars", "Debit", "Credit", "Narration", "Entry Date", "User",
				"Tran Date"
			};
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(100, "L");
			num2 = default(decimal);
			num = num2;
			if (rdbAll.Checked)
			{
				strQuery = "Select Convert(nvarchar,VchrDate,103) as Date,VchrNo,VchrType,AccountId,OppAccountId,DebitAmount,CreditAmount,Narration,Convert(nvarchar,TranDate,103) as EntryDate,UserID,Convert(nvarchar,TranDate,103) as TranDate  from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + " order by VchrDate";
			}
			else if (rdbDebit.Checked)
			{
				strQuery = "Select Convert(nvarchar,VchrDate,103) as Date,VchrNo,VchrType,AccountId,OppAccountId,DebitAmount,CreditAmount,Narration,Convert(nvarchar,TranDate,103) as EntryDate,UserID,Convert(nvarchar,TranDate,103) as TranDate from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + " and CreditAmount='" + 0 + "' order by VchrDate";
			}
			else
			{
				if (!rdbCredit.Checked)
				{
					return;
				}
				strQuery = "Select Convert(nvarchar,VchrDate,103) as Date,VchrNo,VchrType,AccountId,OppAccountId,DebitAmount,CreditAmount,Narration,Convert(nvarchar,TranDate,103) as EntryDate,UserID,Convert(nvarchar,TranDate,103) as TranDate from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.Date.AddHours(24.0).AddSeconds(-1.0)) + "' " + text + " and DebitAmount='" + 0 + "' order by VchrDate";
			}
			string strID = "";
			string strID2 = "";
			string strID3 = "";
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				num += Convert.ToDecimal(row[5].ToString());
				num2 += Convert.ToDecimal(row[6].ToString());
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[3].ToString(), out strID);
				AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[4].ToString(), out strID2);
				AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", row[9].ToString(), out strID3);
				Details.Rows.Add(row[0], row[1], row[2], strID, strID2, row[5], row[6], row[7], row[8], strID3, row[10]);
			}
			Details.Rows.Add("", "", "", "Total", "", num.ToString("F"), num2.ToString("F"), "", "", "", "");
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(80, 80);
				LineData.Rows.Add(140, 140);
				LineData.Rows.Add(240, 240);
				LineData.Rows.Add(400, 400);
				LineData.Rows.Add(560, 560);
				LineData.Rows.Add(660, 660);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Date", 8);
				Header.Rows.Add("Inv No", 81);
				Header.Rows.Add("Inv Type", 142);
				Header.Rows.Add("Account Name", 242);
				Header.Rows.Add("Particulars", 402);
				Header.Rows.Add("Debit", 562);
				Header.Rows.Add("Credit", 662);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "");
				data.Rows.Add(1, 81, false, false, "", 0);
				data.Rows.Add(2, 141, false, true, "", 98);
				data.Rows.Add(3, 241, true, true, "", 155);
				data.Rows.Add(4, 401, false, true, "", 155);
				data.Rows.Add(5, 659, true, false, "Right", 0);
				data.Rows.Add(6, 798, true, false, "Right", 0);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MailMethod()
		{
			LineDataPDF = AniHelper.GetLineDatatablePDF();
			LineDataPDF.Rows.Add(8, 50, 585, 50);
			LineDataPDF.Rows.Add(8, 50, 8, 800);
			LineDataPDF.Rows.Add(8, 800, 585, 800);
			LineDataPDF.Rows.Add(585, 50, 585, 800);
			LineDataPDF.Rows.Add(510, 50, 510, 800);
			LineDataPDF.Rows.Add(450, 50, 450, 800);
			LineDataPDF.Rows.Add(330, 50, 330, 800);
			LineDataPDF.Rows.Add(210, 50, 210, 800);
			LineDataPDF.Rows.Add(110, 50, 110, 800);
			LineDataPDF.Rows.Add(65, 50, 65, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Date", 9);
			HeaderPDF.Rows.Add("Inv No", 70);
			HeaderPDF.Rows.Add("Inv Type", 115);
			HeaderPDF.Rows.Add("Account Name", 215);
			HeaderPDF.Rows.Add("Particulars", 335);
			HeaderPDF.Rows.Add("Debit", 455);
			HeaderPDF.Rows.Add("Credit", 515);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 66, false, false, "", 0);
			dataPDF.Rows.Add(2, 111, false, false, "", 0);
			dataPDF.Rows.Add(3, 211, true, false, "", 0);
			dataPDF.Rows.Add(4, 331, false, false, "", 0);
			dataPDF.Rows.Add(5, 509, true, false, "Right");
			dataPDF.Rows.Add(6, 584, true, false, "Right");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			rdbAll.Checked = true;
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
		}

		private void TransactionReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillBranch(cmbBranch);
			dtmFrom.Value = DateTime.Now.Date;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				sTitle = "Trasaction Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				ReportGenerateMethod();
				PrintMethod();
				MailMethod();
				ReportView reportView = new ReportView();
				reportView = new ReportView();
				reportView.MdiParent = base.MdiParent;
				reportView.Details = Details;
				reportView.Gridview = Gridview;
				reportView.Title = sTitle;
				reportView.data = data;
				reportView.dataPDF = dataPDF;
				reportView.LineData = LineData;
				reportView.LineDataPDF = LineDataPDF;
				reportView.Header = Header;
				reportView.HeaderPDF = HeaderPDF;
				if (form != null)
				{
					form.OpenForm(reportView, 0, 0);
				}
				else
				{
					reportView.Show();
				}
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
			rdbCredit = new System.Windows.Forms.RadioButton();
			rdbDebit = new System.Windows.Forms.RadioButton();
			rdbAll = new System.Windows.Forms.RadioButton();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			rdbDetails = new System.Windows.Forms.RadioButton();
			rdbSummary = new System.Windows.Forms.RadioButton();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(398, 0);
			rdbCredit.AutoSize = true;
			rdbCredit.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbCredit.Location = new System.Drawing.Point(284, 3);
			rdbCredit.Name = "rdbCredit";
			rdbCredit.Size = new System.Drawing.Size(65, 20);
			rdbCredit.TabIndex = 61;
			rdbCredit.Text = "Credit";
			rdbCredit.UseVisualStyleBackColor = true;
			rdbDebit.AutoSize = true;
			rdbDebit.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbDebit.Location = new System.Drawing.Point(197, 3);
			rdbDebit.Name = "rdbDebit";
			rdbDebit.Size = new System.Drawing.Size(60, 20);
			rdbDebit.TabIndex = 62;
			rdbDebit.Text = "Debit";
			rdbDebit.UseVisualStyleBackColor = true;
			rdbAll.AutoSize = true;
			rdbAll.Checked = true;
			rdbAll.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbAll.Location = new System.Drawing.Point(129, 3);
			rdbAll.Name = "rdbAll";
			rdbAll.Size = new System.Drawing.Size(41, 20);
			rdbAll.TabIndex = 60;
			rdbAll.TabStop = true;
			rdbAll.Text = "All";
			rdbAll.UseVisualStyleBackColor = true;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(47, 58);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 16);
			label5.TabIndex = 58;
			label5.Text = "Date To :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(32, 31);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(87, 16);
			label6.TabIndex = 56;
			label6.Text = "Date From :";
			dtmTo.CustomFormat = "dd-MM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(138, 58);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(128, 23);
			dtmTo.TabIndex = 59;
			dtmFrom.CustomFormat = "dd-MM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(139, 29);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(128, 23);
			dtmFrom.TabIndex = 57;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(187, 24);
			lblTitle.TabIndex = 94;
			lblTitle.Text = "Transaction Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(262, 197);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 91;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(348, 197);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 92;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(139, 120);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(226, 24);
			cmbBranch.TabIndex = 104;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(55, 124);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 103;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			panel1.Controls.Add(panel2);
			panel1.Controls.Add(dtmFrom);
			panel1.Controls.Add(cmbBranch);
			panel1.Controls.Add(dtmTo);
			panel1.Controls.Add(lblBranch);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(rdbAll);
			panel1.Controls.Add(rdbDebit);
			panel1.Controls.Add(rdbCredit);
			panel1.Location = new System.Drawing.Point(13, 35);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(410, 158);
			panel1.TabIndex = 105;
			panel2.Controls.Add(rdbDetails);
			panel2.Controls.Add(rdbSummary);
			panel2.Location = new System.Drawing.Point(129, 79);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(200, 33);
			panel2.TabIndex = 199;
			rdbDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			rdbDetails.AutoSize = true;
			rdbDetails.Checked = true;
			rdbDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbDetails.Location = new System.Drawing.Point(8, 7);
			rdbDetails.Name = "rdbDetails";
			rdbDetails.Size = new System.Drawing.Size(70, 20);
			rdbDetails.TabIndex = 198;
			rdbDetails.TabStop = true;
			rdbDetails.Text = "Details";
			rdbDetails.UseVisualStyleBackColor = true;
			rdbSummary.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			rdbSummary.AutoSize = true;
			rdbSummary.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbSummary.Location = new System.Drawing.Point(105, 6);
			rdbSummary.Name = "rdbSummary";
			rdbSummary.Size = new System.Drawing.Size(86, 20);
			rdbSummary.TabIndex = 197;
			rdbSummary.Text = "Summary";
			rdbSummary.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(435, 239);
			base.Controls.Add(panel1);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "TransactionReportView";
			base.Load += new System.EventHandler(TransactionReportView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(panel1, 0);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
