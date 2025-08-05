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
	public class StockTrnasferReportView : BaseForm
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

		private string sFormType;

		private string sBranchDetails;

		private decimal dmlTotalAmnt;

		private decimal dmlTotalQty;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshComboBox cmbProduct;

		private Label label2;

		private AshComboBox cmbGdwnFrm;

		private Label label1;

		private Label label9;

		private Label label11;

		private AshComboBox cmbProGrp;

		private AshComboBox cmbGdwnTo;

		private Label label4;

		private AshDateTimePicker dtmFrom;

		private Label label3;

		private AshDateTimePicker dtmTo;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public StockTrnasferReportView(string _sFormType)
		{
			InitializeComponent();
			ColorChange.Color(this);
			sFormType = _sFormType;
		}

		private void ProAddMethod(string strStockTransferId, string strProGrpDetailsPro, string strProDetailsPro)
		{
			dmlTotalAmnt = default(decimal);
			dmlTotalQty = default(decimal);
			string strID = "";
			try
			{
				strQuery = "Select ProductName,ProQuantity,UnitId,ProRate,Total from StockTranSettings where StockTransferId='" + strStockTransferId + "' " + strProGrpDetailsPro + strProDetailsPro + " ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[2].ToString(), out strID);
					Details.Rows.Add(row[0].ToString(), row[1], strID, row[3], row[4]);
					dmlTotalQty += row[1].ToString().ToDecimal();
					dmlTotalAmnt += row[4].ToString().ToDecimal();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ReportGenerateMethod()
		{
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				sBranchDetails = " And FK_BranchId='" + Settings.Default.iBranchId + "' ";
			}
			string[] headers = new string[5] { "Description", "Quantity", "Unit", "Rate", "Total Value" };
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(400, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(60, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			string text = "";
			string strProGrpDetailsPro = "";
			string text2 = "";
			string strProDetailsPro = "";
			string text3 = "";
			string text4 = "";
			try
			{
				if (cmbProGrp.Text != "")
				{
					text = " and StockTransferId in (select StockTransferId from StockTranSettings Join Product on StockTranSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId where PGroupName='" + cmbProGrp.Text + " ') ";
					strProGrpDetailsPro = " and StockTranSettingsId in (select StockTranSettingsId from StockTranSettings Join Product on StockTranSettings.ProductId=Product.ProductId Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId where PGroupName='" + cmbProGrp.Text + " ') ";
				}
				if (cmbProduct.Text != "")
				{
					text2 = " and StockTransferId in (select StockTransferId from StockTranSettings Join Product on StockTranSettings.ProductId=Product.ProductId where Product.ProductName='" + cmbProduct.Text + "') ";
					strProDetailsPro = " and StockTranSettingsId in (select StockTranSettingsId from StockTranSettings Join Product on StockTranSettings.ProductId=Product.ProductId where Product.ProductName='" + cmbProduct.Text + "') ";
				}
				if (cmbGdwnFrm.Text != "")
				{
					text3 = " and FromGodownName='" + cmbGdwnFrm.Text + "' ";
				}
				if (cmbGdwnTo.Text != "")
				{
					text4 = " and ToGodownName='" + cmbGdwnTo.Text + "' ";
				}
				DateTime now = DateTime.Now;
				string text5 = "";
				strQuery = "Select StockTransferMaster.StockTransferId,StockTransferMaster.StockTransferCode,StockTransferMaster.TransferDate,StockTransferMaster.FromGodownName,StockTransferMaster.ToGodownName From StockTransferMaster where TransferDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'" + text + text2 + text3 + text4 + sBranchDetails + " order by TransferDate,StockTransferCode ";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					if (row[2].ToString() != text5)
					{
						now = Convert.ToDateTime(row[2].ToString());
						text5 = now.ToString();
						Details.Rows.Add("Transfer Date - " + now.ToString("dd-MMM-yyyy"), " ", " ", " ", " ");
					}
					Details.Rows.Add("Stock Transfer Inv No- " + row[1].ToString(), " ", " ", " ", " ");
					Details.Rows.Add("From Godown " + row[3].ToString() + " To " + row[4].ToString(), " ", " ", " ", " ");
					ProAddMethod(row["StockTransferId"].ToString(), strProGrpDetailsPro, strProDetailsPro);
					Details.Rows.Add("", "", "", "Total Quantity", dmlTotalQty.ToString());
					Details.Rows.Add("", "", "", "Total Amount", dmlTotalAmnt.ToString());
				}
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
				LineData.Rows.Add(390, 390);
				LineData.Rows.Add(480, 480);
				LineData.Rows.Add(530, 530);
				LineData.Rows.Add(650, 650);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Description", 9);
				Header.Rows.Add("Quatity", 395);
				Header.Rows.Add("Unit", 485);
				Header.Rows.Add("Rate", 535);
				Header.Rows.Add("Stock Value", 655);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, true, true, "", 350);
				data.Rows.Add(1, 391, true, false, "", 0);
				data.Rows.Add(2, 481, true, false, "", 0);
				data.Rows.Add(3, 648, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(325, 50, 325, 800);
			LineDataPDF.Rows.Add(381, 50, 381, 800);
			LineDataPDF.Rows.Add(422, 50, 422, 800);
			LineDataPDF.Rows.Add(508, 50, 508, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Description", 9);
			HeaderPDF.Rows.Add("Quatity", 326);
			HeaderPDF.Rows.Add("Unit", 382);
			HeaderPDF.Rows.Add("Rate", 423);
			HeaderPDF.Rows.Add("Stock Value", 510);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, true, true, "", 30);
			dataPDF.Rows.Add(1, 327, true, false, "", 0);
			dataPDF.Rows.Add(2, 382, true, false, "", 0);
			dataPDF.Rows.Add(3, 508, true, false, "Right", 0);
			dataPDF.Rows.Add(4, 584, true, false, "Right", 0);
		}

		private void StockTrnasferReportView_Load(object sender, EventArgs e)
		{
			lblTitle.Text = sFormType;
			AniHelper.FillGodown(cmbGdwnFrm);
			AniHelper.FillGodown(cmbGdwnTo);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillProGroup(cmbProGrp);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cmbGdwnFrm.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = "Stock Transfer Report from " + dtmFrom.Text + " to " + dtmTo.Text;
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
			cmbProduct = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			cmbGdwnFrm = new AshUsercontrol.AshComboBox();
			label1 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			cmbProGrp = new AshUsercontrol.AshComboBox();
			cmbGdwnTo = new AshUsercontrol.AshComboBox();
			label4 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label3 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			cmbBranch = new AshUsercontrol.AshComboBox();
			lblBranch = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(368, -1);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 3);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(212, 24);
			lblTitle.TabIndex = 142;
			lblTitle.Text = "Stock Transfer Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(247, 239);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 138;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(326, 239);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 139;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProduct.EnterColor = System.Drawing.Color.LightPink;
			cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProduct.FormattingEnabled = true;
			cmbProduct.LeaveColor = System.Drawing.Color.White;
			cmbProduct.Location = new System.Drawing.Point(142, 96);
			cmbProduct.Name = "cmbProduct";
			cmbProduct.Size = new System.Drawing.Size(230, 24);
			cmbProduct.TabIndex = 146;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(70, 100);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(70, 16);
			label2.TabIndex = 145;
			label2.Text = "Product :";
			cmbGdwnFrm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGdwnFrm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGdwnFrm.EnterColor = System.Drawing.Color.LightPink;
			cmbGdwnFrm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGdwnFrm.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGdwnFrm.FormattingEnabled = true;
			cmbGdwnFrm.LeaveColor = System.Drawing.Color.White;
			cmbGdwnFrm.Location = new System.Drawing.Point(142, 41);
			cmbGdwnFrm.Name = "cmbGdwnFrm";
			cmbGdwnFrm.Size = new System.Drawing.Size(230, 24);
			cmbGdwnFrm.TabIndex = 144;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(32, 44);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(108, 16);
			label1.TabIndex = 143;
			label1.Text = "Godown From :";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(47, 72);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(93, 16);
			label9.TabIndex = 147;
			label9.Text = "Godown To :";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label11.Location = new System.Drawing.Point(83, 126);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(57, 16);
			label11.TabIndex = 149;
			label11.Text = "Group :";
			cmbProGrp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProGrp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProGrp.EnterColor = System.Drawing.Color.LightPink;
			cmbProGrp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProGrp.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProGrp.FormattingEnabled = true;
			cmbProGrp.LeaveColor = System.Drawing.Color.White;
			cmbProGrp.Location = new System.Drawing.Point(142, 123);
			cmbProGrp.Name = "cmbProGrp";
			cmbProGrp.Size = new System.Drawing.Size(230, 24);
			cmbProGrp.TabIndex = 150;
			cmbGdwnTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbGdwnTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbGdwnTo.EnterColor = System.Drawing.Color.LightPink;
			cmbGdwnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbGdwnTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbGdwnTo.FormattingEnabled = true;
			cmbGdwnTo.LeaveColor = System.Drawing.Color.White;
			cmbGdwnTo.Location = new System.Drawing.Point(143, 69);
			cmbGdwnTo.Name = "cmbGdwnTo";
			cmbGdwnTo.Size = new System.Drawing.Size(230, 24);
			cmbGdwnTo.TabIndex = 148;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(53, 151);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(87, 16);
			label4.TabIndex = 151;
			label4.Text = "Date From :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(142, 151);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(143, 23);
			dtmFrom.TabIndex = 152;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(68, 179);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(72, 16);
			label3.TabIndex = 153;
			label3.Text = "Date To :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(143, 179);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(143, 23);
			dtmTo.TabIndex = 154;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(142, 208);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(230, 24);
			cmbBranch.TabIndex = 181;
			cmbBranch.Visible = false;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(75, 208);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 180;
			lblBranch.Text = "Branch :";
			lblBranch.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(404, 274);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(lblBranch);
			base.Controls.Add(label3);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label4);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label9);
			base.Controls.Add(label11);
			base.Controls.Add(cmbProGrp);
			base.Controls.Add(cmbGdwnTo);
			base.Controls.Add(cmbProduct);
			base.Controls.Add(label2);
			base.Controls.Add(cmbGdwnFrm);
			base.Controls.Add(label1);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "StockTrnasferReportView";
			base.Load += new System.EventHandler(StockTrnasferReportView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cmbGdwnFrm, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbProduct, 0);
			base.Controls.SetChildIndex(cmbGdwnTo, 0);
			base.Controls.SetChildIndex(cmbProGrp, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
