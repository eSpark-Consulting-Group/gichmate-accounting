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
	public class TrialBalanceView : BaseForm
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

		private string Query;

		private decimal dmlDebitTotal;

		private decimal dmlCreditTotal;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private Label label1;

		private AshComboBox cmbRType;

		private AshDateTimePicker dtmFrom;

		private Label lblTo;

		private Label lblFrom;

		private AshDateTimePicker dtmTo;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		private Panel panel1;

		public DashboardPOS form { get; set; }

		public TrialBalanceView()
		{
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private decimal TotalFindMethod(string strGrpName, bool isPeriod)
		{
			if (isPeriod)
			{
				Query = "Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog Join Account on TransactionLog.AccountId=Account.AccountId Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId  where AccountGroup.GroupName='" + strGrpName + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			else
			{
				Query = "Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog Join Account on TransactionLog.AccountId=Account.AccountId Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId  where AccountGroup.GroupName='" + strGrpName + "' " + sBranchDetails + " and VchrDate<='" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			return AshproCommon.ValueFindMethod(Query);
		}

		private decimal AccntTotalFindMethod(string strAccId)
		{
			if (cmbRType.SelectedIndex == 0)
			{
				Query = "Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog where TransactionLog.AccountId='" + strAccId + "' " + sBranchDetails + " and VchrDate <= '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			else
			{
				Query = "Select SUM(DebitAmount-CreditAmount) as Amount from TransactionLog where TransactionLog.AccountId='" + strAccId + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value.Date) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			return AshproCommon.ValueFindMethod(Query);
		}

		private void GrpDtlsMethod(string strGrpId)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			Query = "Select AccountId,AccountName From Account where AGroupId='" + strGrpId + "'";
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query);
			foreach (DataRow row in dataTable.Rows)
			{
				string strAccId = row[0].ToString();
				string text = row[1].ToString();
				num3 = AccntTotalFindMethod(strAccId);
				if (num3 > 0m)
				{
					num = num3;
					num2 = default(decimal);
				}
				else
				{
					num3 = Math.Abs(num3);
					num2 = num3;
					num = default(decimal);
				}
				Details.Rows.Add(" ", " ", text, num.ToString("F"), num2.ToString("F"));
				dmlCreditTotal += num2;
				dmlDebitTotal += num;
			}
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			string[] headers = new string[5] { "SL", "Account Group", "Account Name", "Debit", "Credit" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			int num = 1;
			decimal num2 = default(decimal);
			bool flag = false;
			bool flag2 = false;
			flag2 = ((cmbRType.SelectedIndex != 0) ? true : false);
			Details.Rows.Add(" ", " ", " ", " ", " ");
			Query = "Select AGroupId,GroupName From AccountGroup";
			string text = "";
			string text2 = "";
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(Query);
			foreach (DataRow row in dataTable.Rows)
			{
				text = row[0].ToString();
				text2 = row[1].ToString();
				num2 = TotalFindMethod(text2, flag2);
				if (num2 < 0m)
				{
					Details.Rows.Add(num, text2, " ", " ", " ");
					GrpDtlsMethod(text);
					num2 = Math.Abs(num2);
					Details.Rows.Add(" ", "Sub Total", " ", " ", num2.ToString("F"));
					num++;
				}
				else if (num2 > 0m)
				{
					Details.Rows.Add(num, text2, " ", " ", " ");
					GrpDtlsMethod(text);
					Details.Rows.Add(" ", "Sub Total", " ", num2.ToString("F"), " ");
					num++;
				}
				if (flag)
				{
					flag = false;
				}
			}
			Details.Rows.Add(" ", "Total", " ", dmlDebitTotal.ToString("F"), dmlCreditTotal.ToString("F"));
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(55, 55);
				LineData.Rows.Add(310, 310);
				LineData.Rows.Add(565, 565);
				LineData.Rows.Add(670, 670);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 8);
				Header.Rows.Add("Account Group", 60);
				Header.Rows.Add("Account Name", 315);
				Header.Rows.Add("Debit", 570);
				Header.Rows.Add("Credit", 675);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "", 0);
				data.Rows.Add(1, 58, true, true, "", 250);
				data.Rows.Add(2, 312, false, true, "", 355);
				data.Rows.Add(3, 670, true, false, "Right", 0);
				data.Rows.Add(4, 798, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(500, 50, 500, 800);
			LineDataPDF.Rows.Add(415, 50, 415, 800);
			LineDataPDF.Rows.Add(227, 50, 227, 800);
			LineDataPDF.Rows.Add(40, 50, 40, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 10);
			HeaderPDF.Rows.Add("Account Group", 42);
			HeaderPDF.Rows.Add("Account Name", 229);
			HeaderPDF.Rows.Add("Debit", 417);
			HeaderPDF.Rows.Add("Credit", 502);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 42, true, false, "", 0);
			dataPDF.Rows.Add(2, 229, false, true, "", 0);
			dataPDF.Rows.Add(3, 498, true, true, "Right", 0);
			dataPDF.Rows.Add(4, 583, true, true, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			cmbRType.SelectedIndex = 0;
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			cmbRType.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				if (cmbRType.SelectedIndex == 0)
				{
					sTitle = "Trial Balance Report as on  " + dtmTo.Text;
				}
				else
				{
					sTitle = "Trial Balance Report from " + dtmFrom.Text + " to " + dtmTo.Text;
				}
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

		private void TrialBalanceView_Load(object sender, EventArgs e)
		{
			cmbRType.SelectedIndex = 0;
			AniHelper.FillBranch(cmbBranch);
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
		}

		private void cmbRType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbRType.SelectedIndex == 0)
			{
				lblTo.Text = "As On: ";
				lblFrom.Visible = false;
				dtmFrom.Visible = false;
			}
			else
			{
				lblTo.Text = "To: ";
				lblFrom.Visible = true;
				dtmFrom.Visible = true;
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
			label1 = new System.Windows.Forms.Label();
			cmbRType = new AshUsercontrol.AshComboBox();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			lblTo = new System.Windows.Forms.Label();
			lblFrom = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			rdbSummary = new System.Windows.Forms.RadioButton();
			rdbDetails = new System.Windows.Forms.RadioButton();
			panel1 = new System.Windows.Forms.Panel();
			panel1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(341, -1);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(15, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(100, 16);
			label1.TabIndex = 81;
			label1.Text = "Report Type :";
			cmbRType.EnterColor = System.Drawing.Color.LightPink;
			cmbRType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbRType.FormattingEnabled = true;
			cmbRType.Items.AddRange(new object[2] { "As on Date", "Period Wise" });
			cmbRType.LeaveColor = System.Drawing.Color.White;
			cmbRType.Location = new System.Drawing.Point(132, 12);
			cmbRType.Name = "cmbRType";
			cmbRType.Size = new System.Drawing.Size(196, 24);
			cmbRType.TabIndex = 80;
			cmbRType.SelectedIndexChanged += new System.EventHandler(cmbRType_SelectedIndexChanged);
			dtmFrom.CustomFormat = "dd-MM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(132, 40);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(129, 23);
			dtmFrom.TabIndex = 83;
			lblTo.AutoSize = true;
			lblTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblTo.Location = new System.Drawing.Point(15, 68);
			lblTo.Name = "lblTo";
			lblTo.Size = new System.Drawing.Size(36, 16);
			lblTo.TabIndex = 84;
			lblTo.Text = "To :";
			lblFrom.AutoSize = true;
			lblFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblFrom.Location = new System.Drawing.Point(15, 40);
			lblFrom.Name = "lblFrom";
			lblFrom.Size = new System.Drawing.Size(51, 16);
			lblFrom.TabIndex = 82;
			lblFrom.Text = "From :";
			dtmTo.CustomFormat = "dd-MM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(132, 68);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(129, 23);
			dtmTo.TabIndex = 85;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(132, 24);
			lblTitle.TabIndex = 89;
			lblTitle.Text = "Trial Balance";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(198, 218);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 86;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(284, 218);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 87;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(132, 96);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(196, 24);
			cmbBranch.TabIndex = 106;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(15, 100);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 105;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			rdbSummary.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			rdbSummary.AutoSize = true;
			rdbSummary.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbSummary.Location = new System.Drawing.Point(242, 131);
			rdbSummary.Name = "rdbSummary";
			rdbSummary.Size = new System.Drawing.Size(86, 20);
			rdbSummary.TabIndex = 195;
			rdbSummary.Text = "Summary";
			rdbSummary.UseVisualStyleBackColor = true;
			rdbDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			rdbDetails.AutoSize = true;
			rdbDetails.Checked = true;
			rdbDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbDetails.Location = new System.Drawing.Point(134, 131);
			rdbDetails.Name = "rdbDetails";
			rdbDetails.Size = new System.Drawing.Size(70, 20);
			rdbDetails.TabIndex = 196;
			rdbDetails.TabStop = true;
			rdbDetails.Text = "Details";
			rdbDetails.UseVisualStyleBackColor = true;
			panel1.Controls.Add(cmbRType);
			panel1.Controls.Add(rdbSummary);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(rdbDetails);
			panel1.Controls.Add(dtmTo);
			panel1.Controls.Add(cmbBranch);
			panel1.Controls.Add(lblFrom);
			panel1.Controls.Add(lblBranch);
			panel1.Controls.Add(lblTo);
			panel1.Controls.Add(dtmFrom);
			panel1.Location = new System.Drawing.Point(12, 52);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(347, 160);
			panel1.TabIndex = 197;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(377, 260);
			base.Controls.Add(panel1);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "TrialBalanceView";
			base.Load += new System.EventHandler(TrialBalanceView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(panel1, 0);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
