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
	public class OutstandingReportView : BaseForm
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

		private AshComboBox cmbRoute;

		private AshComboBox cmbSalesMan;

		private RadioButton rdbAll;

		private CheckBox cbZeroBalance;

		private CheckBox cbPayable;

		private CheckBox cbReceivable;

		private RadioButton rdbDebtors;

		private RadioButton rdbCreditors;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private GroupBox groupBox1;

		private Label label2;

		private Label label3;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		private RadioButton rdbSummary;

		private RadioButton rdbDetails;

		public DashboardPOS form { get; set; }

		public OutstandingReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private decimal BalanceFindMethod(string strAccId)
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " and FK_BranchId='" + Settings.Default.iBranchId + "'";
			}
			if (cmbBranch.Text != string.Empty)
			{
				text = " and FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			return AshproCommon.ValueFindMethod("SELECT SUM(DebitAmount-CreditAmount) as creditTotal FROM TransactionLog Where AccountId='" + strAccId + "' " + text + " and VchrDate <='" + AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)) + "'" + text);
		}

		private void ReportGenerateMethod()
		{
			string[] headers = new string[5] { "SL", "Customer Name", "Address", "Phone No", "Balance" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(120, "R");
			int num = 1;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			if (cmbSalesMan.Text != string.Empty)
			{
				text = " and Root.SalesManId='" + cmbRoute.SelectedValue.ToInt32() + "' ";
			}
			if (cmbRoute.Text != string.Empty)
			{
				text2 = " and Account.RootId='" + cmbRoute.SelectedValue.ToInt32() + "' ";
				text3 = " Join Root on Account.RootId=Root.RootId ";
			}
			if (rdbCreditors.Checked)
			{
				strQuery = "Select AccountName,AddressOne,MobileNo,AccountId From Account Join AccountGroup ON Account.AGroupId=AccountGroup.AGroupId  where AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS' ";
			}
			else if (rdbDebtors.Checked)
			{
				strQuery = "Select Account.AccountName,Account.AddressOne,Account.MobileNo,AccountId From Account Join AccountGroup ON Account.AGroupId=AccountGroup.AGroupId " + text3 + "  where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS' " + text + text2;
			}
			else if (rdbAll.Checked)
			{
				strQuery = "Select Account.AccountName,Account.AddressOne,Account.MobileNo,AccountId From Account Join AccountGroup ON Account.AGroupId=AccountGroup.AGroupId  where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS' or AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS' ";
			}
			decimal num2 = default(decimal);
			string empty = string.Empty;
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow row in dataTable.Rows)
			{
				decimal num3 = BalanceFindMethod(row[3].ToString());
				empty = ((!(num3 >= 0m)) ? (Math.Abs(num3).ToString(CommonSettings.strCurencyFormat) + " CR") : (num3.ToString(CommonSettings.strCurencyFormat) + " DR"));
				if (cbReceivable.Checked && cbPayable.Checked && cbZeroBalance.Checked)
				{
					Details.Rows.Add(num, row[0], row[1], row[2], empty);
					num2 += num3;
				}
				else if (cbReceivable.Checked && cbPayable.Checked && !cbZeroBalance.Checked)
				{
					if (num3 != 0m)
					{
						Details.Rows.Add(num, row[0], row[1], row[2], empty);
						num2 += num3;
					}
				}
				else if (cbReceivable.Checked && !cbPayable.Checked && !cbZeroBalance.Checked)
				{
					if (num3 > 0m)
					{
						Details.Rows.Add(num, row[0], row[1], row[2], empty);
						num2 += num3;
					}
				}
				else if (!cbReceivable.Checked && cbPayable.Checked && !cbZeroBalance.Checked)
				{
					if (num3 < 0m)
					{
						Details.Rows.Add(num, row[0], row[1], row[2], empty);
						num2 += num3;
					}
				}
				else if (cbZeroBalance.Checked && !cbReceivable.Checked && !cbPayable.Checked)
				{
					if (num3 == 0m)
					{
						Details.Rows.Add(num, row[0], row[1], row[2], empty);
						num2 += num3;
					}
				}
				else if (cbZeroBalance.Checked && !cbReceivable.Checked && cbPayable.Checked)
				{
					if (num3 == 0m || num3 < 0m)
					{
						Details.Rows.Add(num, row[0], row[1], row[2], empty);
						num2 += num3;
					}
				}
				else if (cbZeroBalance.Checked && !cbPayable.Checked && cbReceivable.Checked && (num3 == 0m || num3 > 0m))
				{
					Details.Rows.Add(num, row[0], row[1], row[2], empty);
					num2 += num3;
				}
				num++;
			}
			empty = ((!(num2 >= 0m)) ? (Math.Abs(num2).ToString(CommonSettings.strCurencyFormat) + " CR") : (num2.ToString(CommonSettings.strCurencyFormat) + " DR"));
			Details.Rows.Add("", "", "", "", "");
			Details.Rows.Add("", "Total", "", "", empty);
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(65, 65);
				LineData.Rows.Add(355, 355);
				LineData.Rows.Add(490, 490);
				LineData.Rows.Add(625, 625);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 8);
				Header.Rows.Add("Party Name", 70);
				Header.Rows.Add("Address", 360);
				Header.Rows.Add("Phone No", 495);
				Header.Rows.Add("Balance", 630);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "");
				data.Rows.Add(1, 70, true, true, "", 285);
				data.Rows.Add(2, 360, true, true, "", 125);
				data.Rows.Add(3, 490, true, false, "");
				data.Rows.Add(4, 775, true, false, "Right");
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
			LineDataPDF.Rows.Add(400, 50, 400, 800);
			LineDataPDF.Rows.Add(280, 50, 280, 800);
			LineDataPDF.Rows.Add(40, 50, 40, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 10);
			HeaderPDF.Rows.Add("Account Name", 42);
			HeaderPDF.Rows.Add("Address", 282);
			HeaderPDF.Rows.Add("Phone No", 402);
			HeaderPDF.Rows.Add("Balance", 502);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 25, false, false, "Center");
			dataPDF.Rows.Add(1, 45, false, true, "");
			dataPDF.Rows.Add(2, 285, false, true, "");
			dataPDF.Rows.Add(3, 405, true, true, "");
			dataPDF.Rows.Add(4, 583, true, false, "Right");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			rdbAll.Checked = true;
			cbReceivable.Checked = true;
			cbPayable.Checked = true;
			cbZeroBalance.Checked = true;
			dtmDate.Value = DateTime.Now.Date;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (rdbDetails.Checked)
			{
				sTitle = "Account Payable and Receivable Report as on date " + dtmDate.Text;
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

		private void OutstandingReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillEmployees(cmbSalesMan);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			dtmDate.Value = DateTime.Now.Date;
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
			cmbRoute = new AshUsercontrol.AshComboBox();
			cmbSalesMan = new AshUsercontrol.AshComboBox();
			rdbAll = new System.Windows.Forms.RadioButton();
			cbZeroBalance = new System.Windows.Forms.CheckBox();
			cbPayable = new System.Windows.Forms.CheckBox();
			cbReceivable = new System.Windows.Forms.CheckBox();
			rdbDebtors = new System.Windows.Forms.RadioButton();
			rdbCreditors = new System.Windows.Forms.RadioButton();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			rdbSummary = new System.Windows.Forms.RadioButton();
			rdbDetails = new System.Windows.Forms.RadioButton();
			groupBox1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(330, 0);
			cmbRoute.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbRoute.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbRoute.EnterColor = System.Drawing.Color.LightPink;
			cmbRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbRoute.FormattingEnabled = true;
			cmbRoute.LeaveColor = System.Drawing.Color.White;
			cmbRoute.Location = new System.Drawing.Point(148, 161);
			cmbRoute.Name = "cmbRoute";
			cmbRoute.Size = new System.Drawing.Size(196, 24);
			cmbRoute.TabIndex = 1;
			cmbSalesMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSalesMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSalesMan.EnterColor = System.Drawing.Color.LightPink;
			cmbSalesMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSalesMan.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSalesMan.FormattingEnabled = true;
			cmbSalesMan.LeaveColor = System.Drawing.Color.White;
			cmbSalesMan.Location = new System.Drawing.Point(148, 191);
			cmbSalesMan.Name = "cmbSalesMan";
			cmbSalesMan.Size = new System.Drawing.Size(196, 24);
			cmbSalesMan.TabIndex = 3;
			rdbAll.AutoSize = true;
			rdbAll.Checked = true;
			rdbAll.Location = new System.Drawing.Point(26, 27);
			rdbAll.Name = "rdbAll";
			rdbAll.Size = new System.Drawing.Size(41, 20);
			rdbAll.TabIndex = 0;
			rdbAll.TabStop = true;
			rdbAll.Text = "All";
			rdbAll.UseVisualStyleBackColor = true;
			cbZeroBalance.AutoSize = true;
			cbZeroBalance.Checked = true;
			cbZeroBalance.CheckState = System.Windows.Forms.CheckState.Checked;
			cbZeroBalance.Location = new System.Drawing.Point(195, 62);
			cbZeroBalance.Name = "cbZeroBalance";
			cbZeroBalance.Size = new System.Drawing.Size(113, 20);
			cbZeroBalance.TabIndex = 5;
			cbZeroBalance.Text = "Zero Balance";
			cbZeroBalance.UseVisualStyleBackColor = true;
			cbPayable.AutoSize = true;
			cbPayable.Checked = true;
			cbPayable.CheckState = System.Windows.Forms.CheckState.Checked;
			cbPayable.Location = new System.Drawing.Point(112, 62);
			cbPayable.Name = "cbPayable";
			cbPayable.Size = new System.Drawing.Size(78, 20);
			cbPayable.TabIndex = 4;
			cbPayable.Text = "Payable";
			cbPayable.UseVisualStyleBackColor = true;
			cbReceivable.AutoSize = true;
			cbReceivable.Checked = true;
			cbReceivable.CheckState = System.Windows.Forms.CheckState.Checked;
			cbReceivable.Location = new System.Drawing.Point(17, 61);
			cbReceivable.Name = "cbReceivable";
			cbReceivable.Size = new System.Drawing.Size(97, 20);
			cbReceivable.TabIndex = 3;
			cbReceivable.Text = "Receivable";
			cbReceivable.UseVisualStyleBackColor = true;
			rdbDebtors.AutoSize = true;
			rdbDebtors.Location = new System.Drawing.Point(114, 27);
			rdbDebtors.Name = "rdbDebtors";
			rdbDebtors.Size = new System.Drawing.Size(77, 20);
			rdbDebtors.TabIndex = 1;
			rdbDebtors.Text = "Debtors";
			rdbDebtors.UseVisualStyleBackColor = true;
			rdbCreditors.AutoSize = true;
			rdbCreditors.Location = new System.Drawing.Point(219, 27);
			rdbCreditors.Name = "rdbCreditors";
			rdbCreditors.Size = new System.Drawing.Size(85, 20);
			rdbCreditors.TabIndex = 2;
			rdbCreditors.Text = "Creditors";
			rdbCreditors.UseVisualStyleBackColor = true;
			dtmDate.CustomFormat = "dd-MM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(148, 221);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(120, 23);
			dtmDate.TabIndex = 5;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(26, 225);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 16);
			label1.TabIndex = 4;
			label1.Text = "As On :";
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 9);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(190, 24);
			lblTitle.TabIndex = 46;
			lblTitle.Text = "Outstanding Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(193, 310);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 48;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(279, 310);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 49;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			groupBox1.Controls.Add(rdbAll);
			groupBox1.Controls.Add(rdbCreditors);
			groupBox1.Controls.Add(cbZeroBalance);
			groupBox1.Controls.Add(rdbDebtors);
			groupBox1.Controls.Add(cbPayable);
			groupBox1.Controls.Add(cbReceivable);
			groupBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			groupBox1.Location = new System.Drawing.Point(22, 55);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(322, 100);
			groupBox1.TabIndex = 51;
			groupBox1.TabStop = false;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(26, 164);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(93, 16);
			label2.TabIndex = 4;
			label2.Text = "Route Wise :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(26, 194);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(122, 16);
			label3.TabIndex = 4;
			label3.Text = "Sales Man Wise :";
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(148, 250);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(196, 24);
			cmbBranch.TabIndex = 100;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(26, 254);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 99;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			rdbSummary.AutoSize = true;
			rdbSummary.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbSummary.Location = new System.Drawing.Point(258, 280);
			rdbSummary.Name = "rdbSummary";
			rdbSummary.Size = new System.Drawing.Size(86, 20);
			rdbSummary.TabIndex = 189;
			rdbSummary.Text = "Summary";
			rdbSummary.UseVisualStyleBackColor = true;
			rdbDetails.AutoSize = true;
			rdbDetails.Checked = true;
			rdbDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbDetails.Location = new System.Drawing.Point(150, 280);
			rdbDetails.Name = "rdbDetails";
			rdbDetails.Size = new System.Drawing.Size(70, 20);
			rdbDetails.TabIndex = 190;
			rdbDetails.TabStop = true;
			rdbDetails.Text = "Details";
			rdbDetails.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(366, 349);
			base.Controls.Add(rdbSummary);
			base.Controls.Add(rdbDetails);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(cmbSalesMan);
			base.Controls.Add(cmbRoute);
			base.Controls.Add(groupBox1);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(lblTitle);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Name = "OutstandingReportView";
			base.Load += new System.EventHandler(OutstandingReportView_Load);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(cmbRoute, 0);
			base.Controls.SetChildIndex(cmbSalesMan, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			base.Controls.SetChildIndex(rdbDetails, 0);
			base.Controls.SetChildIndex(rdbSummary, 0);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
