using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class StockReportView : BaseForm
	{
		private DBContext db = new DBContext();

		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private string sTitle;

		private string sBranchDetails = "";

		private DashboardPOS form;

		private int? iProductId;

		private int? ibranchId;

		private int? ibrandId;

		private int? iSupplierId;

		private int? iTCategoryId;

		private int? iProGroupId;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private AshTextBoxNormal txtProCode;

		private Label label7;

		private Label label4;

		private Label label3;

		private AshComboBox cmbBrand;

		private Label label2;

		private AshComboBox cmbSupplier;

		private AshComboBox cmbProGroup;

		private Label label1;

		private AshComboBox cmbTRate;

		private Label label12;

		private AshComboBox cmbStckType;

		private AshComboBox cmbValidation;

		private Label label13;

		private Label label6;

		private Label label5;

		private AshDateTimePicker dtmDate;

		private RadioButton rdbDetails;

		private RadioButton rdbSummary;

		private Label lblBranch;

		private AshComboBox cmbBranch;

		private AshTextBoxNormal txtProductName;

		public string PassingValue { get; set; } = string.Empty;


		public StockReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		public StockReportView(DashboardPOS _form)
		{
			InitializeComponent();
			form = _form;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportShowMetodSQL()
		{
			try
			{
				string type = null;
				if (cmbStckType.Text != string.Empty)
				{
					type = cmbStckType.Text;
				}
				if (cmbBranch.Text != string.Empty)
				{
					ibranchId = cmbBranch.SelectedValue.ToInt32();
				}
				if (cmbBrand.Text != string.Empty)
				{
					ibrandId = cmbBrand.SelectedValue.ToInt32();
				}
				if (cmbProGroup.Text != string.Empty)
				{
					iProGroupId = cmbProGroup.SelectedValue.ToInt32();
				}
				if (cmbTRate.Text != string.Empty)
				{
					iTCategoryId = cmbTRate.SelectedValue.ToInt32();
				}
				if (cmbSupplier.Text != string.Empty)
				{
					iSupplierId = cmbSupplier.SelectedValue.ToInt32();
				}
				if (txtProductName.Text != string.Empty)
				{
					ProductEntity productEntity = db.Products(new
					{
						ProductName = txtProductName.Text
					}).FirstOrDefault();
					if (productEntity != null)
					{
						iProductId = productEntity.ProductId;
					}
				}
				if (txtProCode.Text != string.Empty)
				{
					ProductEntity productEntity2 = db.Products(new
					{
						Barcode = txtProCode.Text
					}).FirstOrDefault();
					if (productEntity2 != null)
					{
						iProductId = productEntity2.ProductId;
					}
				}
				Details = new DataTable();
				Details = AshproCommon.GetDataTableWithIdParameter_SP("spStockReportNormal", new
				{
					Validation = cmbValidation.Text,
					Date = AniHelper.GetDate(dtmDate.Value.Date),
					ProductGroupId = iProGroupId,
					ProductId = iProductId,
					BranchId = ibranchId,
					BrandId = ibrandId,
					SupplierId = iSupplierId,
					TaxCategoryId = iTCategoryId,
					Type = type
				});
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(54, 54);
				LineData.Rows.Add(350, 350);
				LineData.Rows.Add(430, 430);
				LineData.Rows.Add(470, 470);
				LineData.Rows.Add(540, 540);
				LineData.Rows.Add(620, 620);
				LineData.Rows.Add(680, 680);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 9);
				Header.Rows.Add("Product Name", 56);
				Header.Rows.Add("Quantity", 353);
				Header.Rows.Add("Unit", 432);
				Header.Rows.Add("Rate", 472);
				Header.Rows.Add("Gross Val", 542);
				Header.Rows.Add("Dis", 623);
				Header.Rows.Add("Net Val", 683);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 8, false, false, "", 0);
				data.Rows.Add(2, 56, true, true, "", 235);
				data.Rows.Add(3, 353, true, false, "", 0);
				data.Rows.Add(4, 432, false, false, "", 0);
				data.Rows.Add(5, 540, false, false, "Right", 0);
				data.Rows.Add(6, 620, true, false, "Right", 0);
				data.Rows.Add(8, 680, true, false, "Right", 0);
				data.Rows.Add(9, 780, true, false, "Right", 0);
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
			LineDataPDF.Rows.Add(30, 50, 30, 800);
			LineDataPDF.Rows.Add(245, 50, 245, 800);
			LineDataPDF.Rows.Add(305, 50, 305, 800);
			LineDataPDF.Rows.Add(328, 50, 328, 800);
			LineDataPDF.Rows.Add(385, 50, 385, 800);
			LineDataPDF.Rows.Add(450, 50, 450, 800);
			LineDataPDF.Rows.Add(515, 50, 515, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("Product name", 32);
			HeaderPDF.Rows.Add("Quantity", 246);
			HeaderPDF.Rows.Add("Unit", 306);
			HeaderPDF.Rows.Add("Rate", 329);
			HeaderPDF.Rows.Add("Gross Val", 386);
			HeaderPDF.Rows.Add("Disc", 451);
			HeaderPDF.Rows.Add("Net Val", 516);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(2, 32, true, false, "", 0);
			dataPDF.Rows.Add(3, 246, true, false, "", 0);
			dataPDF.Rows.Add(4, 306, false, false, "", 0);
			dataPDF.Rows.Add(5, 380, false, false, "Right", 0);
			dataPDF.Rows.Add(6, 450, true, false, "Right", 0);
			dataPDF.Rows.Add(8, 515, true, false, "Right", 0);
			dataPDF.Rows.Add(9, 585, true, false, "Right", 0);
		}

		private async void StockReportView_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbProGroup);
			AniHelper.FillBrand(cmbBrand);
			await AniHelper.AutoCompleteMethod(txtProductName);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillTCategory(cmbTRate);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
			cmbValidation.SelectedIndex = 0;
			if (PassingValue != string.Empty)
			{
				dtmDate.Text = PassingValue;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			AniHelper.ClearComboMethod(this);
			dtmDate.Value = DateTime.Now;
			cmbProGroup.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			if (rdbDetails.Checked)
			{
				sTitle = "Stock Report as on the date " + dtmDate.Text;
				ReportShowMetodSQL();
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
			else
			{
				DataSet dataSet = new DataSet();
				List<atReportParameter> list = new List<atReportParameter>();
				list.Add(new atReportParameter("FromDate", dtmDate.Text));
				list.Add(new atReportParameter("ToDate", dtmDate.Text));
				list.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
				list.Add(new atReportParameter("DateFormat", CommonSettings.strDateFormat));
				int productGroupId = 0;
				if (cmbProGroup.Text != string.Empty)
				{
					productGroupId = cmbProGroup.SelectedValue.ToInt32();
				}
				string sReportPath = Application.StartupPath + "\\ReportForms\\rptProductStockReportPOS.rdlc";
				DataTable dataTableWithIdParameter_SP = AshproCommon.GetDataTableWithIdParameter_SP("spProductStockReportPOS", new
				{
					ProductGroupId = productGroupId
				});
				dataTableWithIdParameter_SP.TableName = "dtProductStockReportPOS";
				dataSet.Tables.Add(dataTableWithIdParameter_SP);
				AshReportViewer ashReportViewer = new AshReportViewer();
				if (form != null)
				{
					form.OpenForm(ashReportViewer, 0, 0);
				}
				else
				{
					ashReportViewer.Show();
				}
				ashReportViewer.ShowReport(sReportPath, dataSet, list);
			}
			Cursor = Cursors.Arrow;
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
			txtProCode = new AshUsercontrol.AshTextBoxNormal();
			label7 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			cmbBrand = new AshUsercontrol.AshComboBox();
			label2 = new System.Windows.Forms.Label();
			cmbSupplier = new AshUsercontrol.AshComboBox();
			cmbProGroup = new AshUsercontrol.AshComboBox();
			label1 = new System.Windows.Forms.Label();
			cmbTRate = new AshUsercontrol.AshComboBox();
			label12 = new System.Windows.Forms.Label();
			cmbStckType = new AshUsercontrol.AshComboBox();
			cmbValidation = new AshUsercontrol.AshComboBox();
			label13 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			rdbDetails = new System.Windows.Forms.RadioButton();
			rdbSummary = new System.Windows.Forms.RadioButton();
			lblBranch = new System.Windows.Forms.Label();
			cmbBranch = new AshUsercontrol.AshComboBox();
			txtProductName = new AshUsercontrol.AshTextBoxNormal();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(664, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 1);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(129, 24);
			lblTitle.TabIndex = 32;
			lblTitle.Text = "Stock Report";
			btnShow.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(541, 195);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 28;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(btnShow_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(621, 195);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 29;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			txtProCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProCode.Data = 0;
			txtProCode.EnterColor = System.Drawing.Color.LightPink;
			txtProCode.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProCode.Format = null;
			txtProCode.isAllowNegative = false;
			txtProCode.isAllowSpecialChar = false;
			txtProCode.isNumberOnly = false;
			txtProCode.isNumeric = false;
			txtProCode.LeaveColor = System.Drawing.Color.White;
			txtProCode.Location = new System.Drawing.Point(115, 131);
			txtProCode.Margin = new System.Windows.Forms.Padding(4);
			txtProCode.Name = "txtProCode";
			txtProCode.Size = new System.Drawing.Size(230, 16);
			txtProCode.TabIndex = 7;
			txtProCode.Value = new decimal(new int[4]);
			txtProCode.WaterMark = null;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(6, 128);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(72, 16);
			label7.TabIndex = 6;
			label7.Text = "Barcode :";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(6, 158);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(71, 16);
			label4.TabIndex = 8;
			label4.Text = "Supplier :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(6, 103);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(70, 16);
			label3.TabIndex = 4;
			label3.Text = "Product :";
			cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbBrand.EnterColor = System.Drawing.Color.LightPink;
			cmbBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBrand.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBrand.FormattingEnabled = true;
			cmbBrand.LeaveColor = System.Drawing.Color.White;
			cmbBrand.Location = new System.Drawing.Point(115, 72);
			cmbBrand.Name = "cmbBrand";
			cmbBrand.Size = new System.Drawing.Size(230, 24);
			cmbBrand.TabIndex = 3;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(6, 75);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(56, 16);
			label2.TabIndex = 2;
			label2.Text = "Brand :";
			cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
			cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSupplier.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbSupplier.FormattingEnabled = true;
			cmbSupplier.LeaveColor = System.Drawing.Color.White;
			cmbSupplier.Location = new System.Drawing.Point(115, 152);
			cmbSupplier.Name = "cmbSupplier";
			cmbSupplier.Size = new System.Drawing.Size(230, 24);
			cmbSupplier.TabIndex = 9;
			cmbProGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbProGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbProGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbProGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbProGroup.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbProGroup.FormattingEnabled = true;
			cmbProGroup.LeaveColor = System.Drawing.Color.White;
			cmbProGroup.Location = new System.Drawing.Point(115, 44);
			cmbProGroup.Name = "cmbProGroup";
			cmbProGroup.Size = new System.Drawing.Size(230, 24);
			cmbProGroup.TabIndex = 1;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(6, 47);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 16);
			label1.TabIndex = 0;
			label1.Text = "Group :";
			cmbTRate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbTRate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbTRate.EnterColor = System.Drawing.Color.LightPink;
			cmbTRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbTRate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbTRate.FormattingEnabled = true;
			cmbTRate.LeaveColor = System.Drawing.Color.White;
			cmbTRate.Location = new System.Drawing.Point(480, 106);
			cmbTRate.Name = "cmbTRate";
			cmbTRate.Size = new System.Drawing.Size(194, 24);
			cmbTRate.TabIndex = 21;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(358, 109);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(108, 16);
			label12.TabIndex = 20;
			label12.Text = "Tax Category :";
			cmbStckType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbStckType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbStckType.EnterColor = System.Drawing.Color.LightPink;
			cmbStckType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbStckType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbStckType.FormattingEnabled = true;
			cmbStckType.Items.AddRange(new object[3] { "Finished Good", "Raw Metrial", "Others" });
			cmbStckType.LeaveColor = System.Drawing.Color.White;
			cmbStckType.Location = new System.Drawing.Point(480, 75);
			cmbStckType.Name = "cmbStckType";
			cmbStckType.Size = new System.Drawing.Size(194, 24);
			cmbStckType.TabIndex = 17;
			cmbValidation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbValidation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbValidation.EnterColor = System.Drawing.Color.LightPink;
			cmbValidation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbValidation.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbValidation.FormattingEnabled = true;
			cmbValidation.Items.AddRange(new object[9] { "Selling Price", "Average Selling Price", "Highest Selling Price", "Last Selling Price", "Purchase Price", "Average Purchase Price", "Highest Purchase Price", "Last Purchase Price", "MRP" });
			cmbValidation.LeaveColor = System.Drawing.Color.White;
			cmbValidation.Location = new System.Drawing.Point(480, 47);
			cmbValidation.Name = "cmbValidation";
			cmbValidation.Size = new System.Drawing.Size(194, 24);
			cmbValidation.TabIndex = 15;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label13.Location = new System.Drawing.Point(358, 79);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(95, 16);
			label13.TabIndex = 16;
			label13.Text = "Stock Type :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(358, 50);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(120, 16);
			label6.TabIndex = 14;
			label6.Text = "Valuation Using :";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(358, 165);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(58, 16);
			label5.TabIndex = 26;
			label5.Text = "As On :";
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(480, 165);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 27;
			rdbDetails.AutoSize = true;
			rdbDetails.Checked = true;
			rdbDetails.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbDetails.Location = new System.Drawing.Point(107, 190);
			rdbDetails.Name = "rdbDetails";
			rdbDetails.Size = new System.Drawing.Size(70, 20);
			rdbDetails.TabIndex = 180;
			rdbDetails.TabStop = true;
			rdbDetails.Text = "Details";
			rdbDetails.UseVisualStyleBackColor = true;
			rdbSummary.AutoSize = true;
			rdbSummary.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			rdbSummary.Location = new System.Drawing.Point(215, 191);
			rdbSummary.Name = "rdbSummary";
			rdbSummary.Size = new System.Drawing.Size(86, 20);
			rdbSummary.TabIndex = 180;
			rdbSummary.Text = "Summary";
			rdbSummary.UseVisualStyleBackColor = true;
			lblBranch.AutoSize = true;
			lblBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblBranch.Location = new System.Drawing.Point(358, 138);
			lblBranch.Name = "lblBranch";
			lblBranch.Size = new System.Drawing.Size(64, 16);
			lblBranch.TabIndex = 20;
			lblBranch.Text = "Branch :";
			cmbBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbBranch.EnterColor = System.Drawing.Color.LightPink;
			cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbBranch.FormattingEnabled = true;
			cmbBranch.LeaveColor = System.Drawing.Color.White;
			cmbBranch.Location = new System.Drawing.Point(480, 135);
			cmbBranch.Name = "cmbBranch";
			cmbBranch.Size = new System.Drawing.Size(194, 24);
			cmbBranch.TabIndex = 21;
			txtProductName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtProductName.Data = 0;
			txtProductName.EnterColor = System.Drawing.Color.LightPink;
			txtProductName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtProductName.Format = null;
			txtProductName.isAllowNegative = false;
			txtProductName.isAllowSpecialChar = false;
			txtProductName.isNumberOnly = false;
			txtProductName.isNumeric = false;
			txtProductName.LeaveColor = System.Drawing.Color.White;
			txtProductName.Location = new System.Drawing.Point(114, 106);
			txtProductName.Margin = new System.Windows.Forms.Padding(4);
			txtProductName.Name = "txtProductName";
			txtProductName.Size = new System.Drawing.Size(230, 16);
			txtProductName.TabIndex = 7;
			txtProductName.Value = new decimal(new int[4]);
			txtProductName.WaterMark = null;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(700, 227);
			base.Controls.Add(rdbSummary);
			base.Controls.Add(rdbDetails);
			base.Controls.Add(label5);
			base.Controls.Add(dtmDate);
			base.Controls.Add(cmbStckType);
			base.Controls.Add(cmbValidation);
			base.Controls.Add(label13);
			base.Controls.Add(label6);
			base.Controls.Add(cmbBranch);
			base.Controls.Add(cmbTRate);
			base.Controls.Add(lblBranch);
			base.Controls.Add(label12);
			base.Controls.Add(txtProductName);
			base.Controls.Add(txtProCode);
			base.Controls.Add(label7);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(cmbBrand);
			base.Controls.Add(label2);
			base.Controls.Add(cmbSupplier);
			base.Controls.Add(cmbProGroup);
			base.Controls.Add(label1);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Name = "StockReportView";
			base.Load += new System.EventHandler(StockReportView_Load);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cmbProGroup, 0);
			base.Controls.SetChildIndex(cmbSupplier, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbBrand, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(txtProCode, 0);
			base.Controls.SetChildIndex(txtProductName, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(lblBranch, 0);
			base.Controls.SetChildIndex(cmbTRate, 0);
			base.Controls.SetChildIndex(cmbBranch, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(cmbValidation, 0);
			base.Controls.SetChildIndex(cmbStckType, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(rdbDetails, 0);
			base.Controls.SetChildIndex(rdbSummary, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
