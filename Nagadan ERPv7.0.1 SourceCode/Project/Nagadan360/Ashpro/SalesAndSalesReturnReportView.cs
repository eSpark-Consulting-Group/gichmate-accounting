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
	public class SalesAndSalesReturnReportView : BaseForm
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

		private string sBranchDetails = "";

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private Label label4;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label label1;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public string ValueDetails { get; set; }

		public SalesAndSalesReturnReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private decimal SalesAndReturn(string VchrType, DateTime dtt)
		{
			if (VchrType.Contains("Return"))
			{
				if (ValueDetails != "Purchase")
				{
					strQuery = "SELECT SUM(DebitAmount) as debitTotal FROM TransactionLog where VchrType='" + VchrType + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtt) + "' and '" + AniHelper.GetDate(dtt.AddHours(24.0).AddSeconds(-1.0)) + "'";
				}
				else
				{
					strQuery = "SELECT SUM(CreditAmount) as debitTotal FROM TransactionLog where VchrType ='" + VchrType + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtt) + "' and '" + AniHelper.GetDate(dtt.AddHours(24.0).AddSeconds(-1.0)) + "'";
				}
			}
			else if (ValueDetails != "Purchase")
			{
				strQuery = "SELECT SUM(CreditAmount) as debitTotal FROM TransactionLog where VchrType='" + VchrType + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtt) + "' and '" + AniHelper.GetDate(dtt.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			else
			{
				strQuery = "SELECT SUM(DebitAmount) as debitTotal FROM TransactionLog where VchrType='" + VchrType + "' " + sBranchDetails + " and VchrDate between '" + AniHelper.GetDate(dtt) + "' and '" + AniHelper.GetDate(dtt.AddHours(24.0).AddSeconds(-1.0)) + "'";
			}
			return AshproCommon.ValueFindMethod(strQuery);
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string[] headers = new string[5]
			{
				"SL",
				"Date",
				ValueDetails + " Amount",
				ValueDetails + " Return Amount",
				ValueDetails + " After Return"
			};
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			string empty = string.Empty;
			string empty2 = string.Empty;
			decimal num3 = default(decimal);
			string text = "";
			string text2 = "";
			if (ValueDetails == "Purchase")
			{
				empty = "Purchase Voucher";
				empty2 = "Purchase Return Voucher";
				strQuery = "Select distinct Convert(nvarchar ,VchrDate,102) as VchrDate from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + sBranchDetails + " and VchrType in('Purchase Voucher','Purchase Return Voucher') order by VchrDate";
			}
			else
			{
				empty = "Sales Voucher";
				empty2 = "Sales Return Voucher";
				strQuery = "Select distinct Convert(nvarchar ,VchrDate,102) as VchrDate from TransactionLog where VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value.AddHours(24.0).AddSeconds(-1.0)) + "' " + sBranchDetails + " and VchrType in('Sales Voucher','Sales Return Voucher') order by VchrDate";
			}
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					if (row[0] != DBNull.Value)
					{
						DateTime dtt = row[0].ToString().toDateTime();
						num = SalesAndReturn(empty, dtt);
						num2 = SalesAndReturn(empty2, dtt);
						num3 = num3 + num - num2;
						text = ((!(num == 0m)) ? num.ToString(CommonSettings.strCurencyFormat) : "");
						text2 = ((!(num2 == 0m)) ? num2.ToString(CommonSettings.strCurencyFormat) : "");
						if (num != 0m || num2 != 0m)
						{
							Details.Rows.Add(Details.Rows.Count + 1, row[0].ToString().toDateTime().ToString("dd-MMM-yyyy"), text, text2, (num - num2).ToString(CommonSettings.strCurencyFormat));
						}
						num = default(decimal);
						num2 = default(decimal);
					}
				}
				Details.Rows.Add("", "", "Closing Bal", "", num3);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(90, 90);
				LineData.Rows.Add(250, 250);
				LineData.Rows.Add(410, 410);
				LineData.Rows.Add(560, 560);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 9);
				Header.Rows.Add("Date", 92);
				if (ValueDetails != "Purchase")
				{
					Header.Rows.Add("Sales Amount", 252);
					Header.Rows.Add("Return Amount", 412);
					Header.Rows.Add("Sales after Return", 565);
				}
				else
				{
					Header.Rows.Add("Purchase Amount", 252);
					Header.Rows.Add("Return Amount", 412);
					Header.Rows.Add("Purchase after Return", 565);
				}
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 10, false, false, "", 0);
				data.Rows.Add(1, 92, false, false, "", 0);
				data.Rows.Add(2, 408, true, false, "Right", 0);
				data.Rows.Add(3, 558, false, false, "Right", 0);
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
			LineDataPDF.Rows.Add(65, 50, 65, 800);
			LineDataPDF.Rows.Add(200, 50, 200, 800);
			LineDataPDF.Rows.Add(315, 50, 315, 800);
			LineDataPDF.Rows.Add(430, 50, 430, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("Date", 67);
			if (ValueDetails != "Purchase")
			{
				HeaderPDF.Rows.Add("Sales Amount", 202);
				HeaderPDF.Rows.Add("Return Amount", 317);
				HeaderPDF.Rows.Add("Sales after Rtn", 432);
			}
			else
			{
				HeaderPDF.Rows.Add("Purchase Amount", 202);
				HeaderPDF.Rows.Add("Return Amount", 317);
				HeaderPDF.Rows.Add("Purchase after Rtn", 432);
			}
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 66, false, false, "", 0);
			dataPDF.Rows.Add(2, 313, true, false, "Right", 0);
			dataPDF.Rows.Add(3, 428, false, false, "Right", 0);
			dataPDF.Rows.Add(4, 583, true, false, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			if (ValueDetails != "Purchase")
			{
				sTitle = "Sales and Sales Return Report Report from " + dtmFrom.Text + " to " + dtmTo.Text;
			}
			else
			{
				sTitle = "Puchase and Purchase Return Report Report from " + dtmFrom.Text + " to " + dtmTo.Text;
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
			reportView.Show();
		}

		private void SalesAndSalesReturnReportView_Load(object sender, EventArgs e)
		{
			try
			{
				DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now.Date));
				AniHelper.FillBranch(cmbBranch);
				Label label = lblBranch;
				bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
				label.Visible = visible;
				AniHelper.ClearComboMethod(this);
				if (ValueDetails == "Purchase")
				{
					lblTitle.Text = "Purchase And Purchase Return Report";
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
			lblTitle = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(400, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(3, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(298, 24);
			lblTitle.TabIndex = 121;
			lblTitle.Text = "Sales And Sales Return Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(278, 157);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 117;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(357, 157);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 118;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(55, 74);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(36, 16);
			label4.TabIndex = 115;
			label4.Text = "To :";
			dtmTo.CustomFormat = "dd-MM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(99, 70);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(120, 23);
			dtmTo.TabIndex = 116;
			dtmFrom.CustomFormat = "dd-MM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(99, 42);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(120, 23);
			dtmFrom.TabIndex = 114;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(4, 46);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 16);
			label1.TabIndex = 113;
			label1.Text = "Date From :";
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(99, 99);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(299, 24);
			cmbBranch.TabIndex = 191;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(27, 102);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 190;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(435, 189);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(label4);
			base.Controls.Add(dtmTo);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label1);
			base.Name = "SalesAndSalesReturnReportView";
			base.Load += new System.EventHandler(SalesAndSalesReturnReportView_Load);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
