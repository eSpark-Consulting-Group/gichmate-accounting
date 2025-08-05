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
	public class UnSoldProductReportView : BaseForm
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

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbProGroup;

		private Label label3;

		private AshComboBox cmbValUsing;

		private AshComboBox cmbSupplier;

		private AshComboBox cmbBrand;

		private Label label2;

		private Label label4;

		private Label label1;

		private Label label5;

		private Label label6;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public UnSoldProductReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void ReportGenerateMethod()
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string[] headers = new string[8] { "SL", "Product Name", "Group", "Brand", "Stock", "Unit", "Rate", "Stock Value" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(150, "L");
			Gridview.Rows.Add(150, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(60, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			if (cmbProGroup.Text != "")
			{
				text2 = " and ProductGroup.PGroupName='" + cmbProGroup.Text + "' ";
			}
			if (cmbBrand.Text != "")
			{
				text3 = " and Brand.BrandName='" + cmbBrand.Text + "' ";
			}
			if (cmbSupplier.Text != "")
			{
				text4 = " Join Account on Product.AccountId= Account.AccountId ";
				text5 = " and Account.AccountId='" + cmbSupplier.SelectedValue.ToInt32() + "'";
			}
			text6 = ((cmbValUsing.SelectedIndex == 0) ? " Product.SellingPrice " : ((cmbValUsing.SelectedIndex != 1) ? "Product.MRP" : "Product.PurchasePrice"));
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			string strID = "";
			int num6 = 0;
			try
			{
				string query = "Select Product.ProductName,ProductGroup.PGroupName,Brand.BrandName,(select sum(StockTranLog.InQuantity - StockTranLog.OutQuantity) as Clostock from StockTranLog where StockTranLog.ProductId=Product.ProductId and " + text + " StockTranLog.VchrDate <= '" + dtmTo.Text + "') as Clostock,Product.UnitId, " + text6 + " from Product  Join ProductGroup on Product.PGroupId =ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId " + text4 + " where Product.ProductId not in (select ProductId from SalesEstimateSettings where Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' )" + text2 + text3 + text5 + " order by Product.ProductName";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(query);
				foreach (DataRow row in dataTable.Rows)
				{
					num = row[3].ToString().ToDecimal();
					num2 += num;
					num3 = Convert.ToDecimal(row[5].ToString());
					num4 = num3 * num;
					num5 += num4;
					num6 = Details.Rows.Count + 1;
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[4].ToString(), out strID);
					Details.Rows.Add(num6, row[0], row[1], row[2], num.ToString(CommonSettings.strCurencyFormat), strID, row[5], num4);
				}
				Details.Rows.Add("", "Total", "", "", num2, "", "", num5);
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
				LineData.Rows.Add(68, 68);
				LineData.Rows.Add(308, 308);
				LineData.Rows.Add(519, 519);
				LineData.Rows.Add(594, 594);
				LineData.Rows.Add(679, 679);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 9);
				Header.Rows.Add("Product Name", 70);
				Header.Rows.Add("Group", 310);
				Header.Rows.Add("Stock", 520);
				Header.Rows.Add("Rate", 595);
				Header.Rows.Add("Stock Value", 680);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, true, false, "", 0);
				data.Rows.Add(1, 69, true, true, "", 235);
				data.Rows.Add(2, 309, true, false, "", 0);
				data.Rows.Add(4, 520, true, false, "", 0);
				data.Rows.Add(6, 678, true, false, "Right", 0);
				data.Rows.Add(7, 798, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(57, 50, 57, 800);
			LineDataPDF.Rows.Add(219, 50, 219, 800);
			LineDataPDF.Rows.Add(409, 50, 409, 800);
			LineDataPDF.Rows.Add(459, 50, 459, 800);
			LineDataPDF.Rows.Add(519, 50, 519, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("Product Name", 59);
			HeaderPDF.Rows.Add("Group", 220);
			HeaderPDF.Rows.Add("Stock", 410);
			HeaderPDF.Rows.Add("Rate", 460);
			HeaderPDF.Rows.Add("Stock Value", 520);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, true, false, "");
			dataPDF.Rows.Add(1, 58, true, true, "", 30);
			dataPDF.Rows.Add(2, 220, true, false, "", 0);
			dataPDF.Rows.Add(4, 410, true, false, "", 0);
			dataPDF.Rows.Add(6, 518, true, false, "Right", 0);
			dataPDF.Rows.Add(7, 583, true, false, "Right", 0);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cmbProGroup.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = "Unsold product Report from " + dtmFrom.Text + " to " + dtmTo.Text;
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

		private void UnSoldProductReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbProGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
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
			cmbProGroup = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			cmbValUsing = new AshUsercontrol.AshComboBox();
			cmbSupplier = new AshUsercontrol.AshComboBox();
			cmbBrand = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(369, -1);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(224, 24);
			lblTitle.TabIndex = 137;
			lblTitle.Text = "UnSold Product Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(247, 250);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 133;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(326, 250);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 134;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbProGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProGroup.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProGroup.FormattingEnabled = true;
			cmbProGroup.LeaveColor = System.Drawing.Color.White;
			cmbProGroup.Location = new System.Drawing.Point(152, 46);
			cmbProGroup.Name = "cmbProGroup";
			cmbProGroup.Size = new System.Drawing.Size(230, 24);
			cmbProGroup.TabIndex = 139;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(79, 105);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(71, 16);
			label3.TabIndex = 142;
			label3.Text = "Supplier :";
			cmbValUsing.EnterColor = System.Drawing.Color.LightPink;
			cmbValUsing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbValUsing.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbValUsing.FormattingEnabled = true;
			cmbValUsing.Items.AddRange(new object[3] { "Selling Price", "Purchase Price", "MRP" });
			cmbValUsing.LeaveColor = System.Drawing.Color.White;
			cmbValUsing.Location = new System.Drawing.Point(152, 129);
			cmbValUsing.Name = "cmbValUsing";
			cmbValUsing.Size = new System.Drawing.Size(230, 24);
			cmbValUsing.TabIndex = 145;
			cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
			cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSupplier.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSupplier.FormattingEnabled = true;
			cmbSupplier.LeaveColor = System.Drawing.Color.White;
			cmbSupplier.Location = new System.Drawing.Point(152, 100);
			cmbSupplier.Name = "cmbSupplier";
			cmbSupplier.Size = new System.Drawing.Size(230, 24);
			cmbSupplier.TabIndex = 143;
			cmbBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBrand.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBrand.FormattingEnabled = true;
			cmbBrand.LeaveColor = System.Drawing.Color.White;
			cmbBrand.Location = new System.Drawing.Point(152, 73);
			cmbBrand.Name = "cmbBrand";
			cmbBrand.Size = new System.Drawing.Size(230, 24);
			cmbBrand.TabIndex = 141;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(94, 76);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(56, 16);
			label2.TabIndex = 140;
			label2.Text = "Brand :";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(30, 132);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(120, 16);
			label4.TabIndex = 144;
			label4.Text = "Valuation Using :";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(93, 49);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 16);
			label1.TabIndex = 138;
			label1.Text = "Group :";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(79, 193);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 16);
			label5.TabIndex = 148;
			label5.Text = "Date To :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(64, 163);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(87, 16);
			label6.TabIndex = 146;
			label6.Text = "Date From :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(152, 193);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 149;
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(152, 159);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 23);
			dtmFrom.TabIndex = 147;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(152, 220);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(230, 24);
			cmbBranch.TabIndex = 183;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(85, 220);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 182;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(404, 284);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(label5);
			base.Controls.Add(label6);
			base.Controls.Add(dtmTo);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(cmbProGroup);
			base.Controls.Add(label3);
			base.Controls.Add(cmbValUsing);
			base.Controls.Add(cmbSupplier);
			base.Controls.Add(cmbBrand);
			base.Controls.Add(label2);
			base.Controls.Add(label4);
			base.Controls.Add(label1);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "UnSoldProductReportView";
			base.Load += new System.EventHandler(UnSoldProductReportView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbBrand, 0);
			base.Controls.SetChildIndex(cmbSupplier, 0);
			base.Controls.SetChildIndex(cmbValUsing, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbProGroup, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
