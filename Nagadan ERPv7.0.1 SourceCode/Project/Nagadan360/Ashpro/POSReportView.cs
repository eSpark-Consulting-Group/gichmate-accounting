using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;
using Microsoft.Reporting.WinForms;

namespace Ashpro
{
	public class POSReportView : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private Panel panel1;

		private Label lblHead;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label lblFrom;

		private Label lblTo;

		private Button btnShowReport;

		private Panel panel2;

		private ReportViewer rvReport;

		private AshTextBoxNormal txtProductName;

		private Label lblProductName;

		private AshComboBox cmbProductGroup;

		private Label lblProductGroup;

		private Button btnDate;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public POSReportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			rvReport.SetDisplayMode(DisplayMode.PrintLayout);
			rvReport.ZoomMode = ZoomMode.Percent;
		}

		private void AutoCompleteMethod()
		{
			try
			{
				AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
				foreach (ProductEntity item in db.Products())
				{
					autoCompleteStringCollection.Add(item.ProductName);
				}
				txtProductName.AutoCompleteMode = AutoCompleteMode.Suggest;
				txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
				txtProductName.AutoCompleteCustomSource = autoCompleteStringCollection;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AutoCompleteMethodUser()
		{
			try
			{
				AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
				foreach (User item in db.Users())
				{
					autoCompleteStringCollection.Add(item.UserName);
				}
				txtProductName.AutoCompleteMode = AutoCompleteMode.Suggest;
				txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
				txtProductName.AutoCompleteCustomSource = autoCompleteStringCollection;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void POSReportView_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			rvReport.RefreshReport();
			dtmFrom.Value = DateTime.Now.Date;
			if (CommonSettings.blnEnableMultiBranch)
			{
				Label label = lblBranch;
				cmbBranch.Visible = true;
				label.Visible = true;
			}
			AniHelper.FillBranch(cmbBranch);
			if (Utility.iReportFormNumber == 12)
			{
				lblHead.Text = MessageKeys.SaleTransactionReport;
				return;
			}
			if (Utility.iReportFormNumber == 13)
			{
				lblHead.Text = MessageKeys.POSSalesSummaryReport;
				return;
			}
			if (Utility.iReportFormNumber == 14)
			{
				lblHead.Text = MessageKeys.POSSalesTaxReport;
				return;
			}
			if (Utility.iReportFormNumber == 15)
			{
				lblHead.Text = MessageKeys.POSSalesTaxCategoryReport;
				return;
			}
			if (Utility.iReportFormNumber == 16)
			{
				lblHead.Text = MessageKeys.StockAndProfitReport;
				Label label2 = lblFrom;
				Label label3 = lblTo;
				AshDateTimePicker ashDateTimePicker = dtmFrom;
				AshDateTimePicker ashDateTimePicker2 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker2.Visible = false;
				ashDateTimePicker.Visible = false;
				label3.Visible = false;
				label2.Visible = false;
				Label label4 = lblProductName;
				txtProductName.Visible = true;
				label4.Visible = true;
				btnShowReport.Location = new Point(585, 45);
				AutoCompleteMethod();
				base.ActiveControl = txtProductName;
				return;
			}
			if (Utility.iReportFormNumber == 17)
			{
				lblHead.Text = MessageKeys.CategorySalesReport;
				Label label5 = lblProductGroup;
				AshComboBox ashComboBox = cmbProductGroup;
				Label label6 = lblProductName;
				txtProductName.Visible = true;
				label6.Visible = true;
				ashComboBox.Visible = true;
				label5.Visible = true;
				lblProductName.Text = "User Name :";
				AutoCompleteMethodUser();
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 18)
			{
				lblHead.Text = MessageKeys.ProductSalesProfitReport;
				return;
			}
			if (Utility.iReportFormNumber == 19)
			{
				lblHead.Text = MessageKeys.SalesByProductReport;
				Label label7 = lblProductName;
				AshTextBoxNormal ashTextBoxNormal = txtProductName;
				AshComboBox ashComboBox2 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox2.Visible = true;
				ashTextBoxNormal.Visible = true;
				label7.Visible = true;
				AutoCompleteMethod();
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 20)
			{
				lblHead.Text = MessageKeys.CloseCashReport;
				AshComboBox ashComboBox3 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox3.Visible = true;
				lblProductGroup.Text = MessageKeys.UserName;
				AniHelper.FillUser(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 21)
			{
				lblHead.Text = MessageKeys.CashFlowReport;
				AshComboBox ashComboBox4 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox4.Visible = true;
				lblProductGroup.Text = MessageKeys.CashAccount;
				AniHelper.FillCashAccount(cmbProductGroup);
				return;
			}
			if (Utility.iReportFormNumber == 22)
			{
				lblHead.Text = MessageKeys.CustomerReport;
				Label label8 = lblFrom;
				Label label9 = lblTo;
				AshDateTimePicker ashDateTimePicker3 = dtmFrom;
				AshDateTimePicker ashDateTimePicker4 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker4.Visible = false;
				ashDateTimePicker3.Visible = false;
				label9.Visible = false;
				label8.Visible = false;
				AshComboBox ashComboBox5 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox5.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 23)
			{
				lblHead.Text = MessageKeys.CustomerDiaryReport;
				AshComboBox ashComboBox6 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox6.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
				return;
			}
			if (Utility.iReportFormNumber == 24)
			{
				lblHead.Text = MessageKeys.CustomerSalesReport;
				AshComboBox ashComboBox7 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox7.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
				return;
			}
			if (Utility.iReportFormNumber == 25)
			{
				lblHead.Text = MessageKeys.CustomerItemSalesReport;
				AshComboBox ashComboBox8 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox8.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
				return;
			}
			if (Utility.iReportFormNumber == 26)
			{
				lblHead.Text = MessageKeys.ProductStockReport;
				Label label10 = lblFrom;
				Label label11 = lblTo;
				AshDateTimePicker ashDateTimePicker5 = dtmFrom;
				AshDateTimePicker ashDateTimePicker6 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker6.Visible = false;
				ashDateTimePicker5.Visible = false;
				label11.Visible = false;
				label10.Visible = false;
				AshComboBox ashComboBox9 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox9.Visible = true;
				lblProductGroup.Text = MessageKeys.Group;
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 27)
			{
				lblHead.Text = MessageKeys.ProWisePurchaseReport;
				AshComboBox ashComboBox10 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox10.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 28)
			{
				lblHead.Text = MessageKeys.ProTranSummaryReport;
				Label label12 = lblFrom;
				Label label13 = lblTo;
				AshDateTimePicker ashDateTimePicker7 = dtmFrom;
				AshDateTimePicker ashDateTimePicker8 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker8.Visible = false;
				ashDateTimePicker7.Visible = false;
				label13.Visible = false;
				label12.Visible = false;
				Label label14 = lblProductName;
				txtProductName.Visible = true;
				label14.Visible = true;
				return;
			}
			if (Utility.iReportFormNumber == 29)
			{
				lblHead.Text = MessageKeys.SupplierReport;
				Label label15 = lblFrom;
				Label label16 = lblTo;
				AshDateTimePicker ashDateTimePicker9 = dtmFrom;
				AshDateTimePicker ashDateTimePicker10 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker10.Visible = false;
				ashDateTimePicker9.Visible = false;
				label16.Visible = false;
				label15.Visible = false;
				AshComboBox ashComboBox11 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox11.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 30)
			{
				lblHead.Text = MessageKeys.SupplierLedgerReport;
				AshComboBox ashComboBox12 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox12.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				return;
			}
			if (Utility.iReportFormNumber == 32)
			{
				lblHead.Text = MessageKeys.SupplierPurchaseReport;
				AshComboBox ashComboBox13 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox13.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 31)
			{
				lblHead.Text = MessageKeys.SupplierItemPurchaseReport;
				AshComboBox ashComboBox14 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox14.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 33)
			{
				lblHead.Text = MessageKeys.OpeningStockReport;
				Label label17 = lblFrom;
				Label label18 = lblTo;
				AshDateTimePicker ashDateTimePicker11 = dtmFrom;
				AshDateTimePicker ashDateTimePicker12 = dtmTo;
				btnDate.Visible = false;
				ashDateTimePicker12.Visible = false;
				ashDateTimePicker11.Visible = false;
				label18.Visible = false;
				label17.Visible = false;
				AshComboBox ashComboBox15 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox15.Visible = true;
				lblProductGroup.Text = MessageKeys.Group;
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
				return;
			}
			if (Utility.iReportFormNumber == 34)
			{
				lblHead.Text = MessageKeys.UserSale;
				Label label19 = lblFrom;
				Label label20 = lblTo;
				AshDateTimePicker ashDateTimePicker13 = dtmFrom;
				dtmTo.Visible = true;
				ashDateTimePicker13.Visible = true;
				label20.Visible = true;
				label19.Visible = true;
				AshComboBox ashComboBox16 = cmbProductGroup;
				lblProductGroup.Visible = false;
				ashComboBox16.Visible = false;
				return;
			}
			if (Utility.iReportFormNumber == 35)
			{
				lblHead.Text = "Vat Report";
				Label label21 = lblFrom;
				Label label22 = lblTo;
				AshDateTimePicker ashDateTimePicker14 = dtmFrom;
				dtmTo.Visible = true;
				ashDateTimePicker14.Visible = true;
				label22.Visible = true;
				label21.Visible = true;
				AshComboBox ashComboBox17 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox17.Visible = true;
				lblProductGroup.Text = "Report Langauge :";
				Label label23 = lblProductName;
				txtProductName.Visible = true;
				label23.Visible = true;
				lblProductName.Text = "Sequence No :";
				txtProductName.isNumberOnly = true;
				string[] array = new string[2] { "Arabic", "English" };
				ComboBox.ObjectCollection items = cmbProductGroup.Items;
				object[] items2 = array;
				items.AddRange(items2);
				cmbProductGroup.SelectedIndex = 0;
				return;
			}
			if (Utility.iReportFormNumber == 36)
			{
				lblHead.Text = "KOT Void Items Report";
				Label label24 = lblFrom;
				Label label25 = lblTo;
				AshDateTimePicker ashDateTimePicker15 = dtmFrom;
				dtmTo.Visible = true;
				ashDateTimePicker15.Visible = true;
				label25.Visible = true;
				label24.Visible = true;
				AshComboBox ashComboBox18 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox18.Visible = true;
				lblProductGroup.Text = "Terminal :";
				Label label26 = lblProductName;
				txtProductName.Visible = true;
				label26.Visible = true;
				lblProductName.Text = "Sequence No :";
				txtProductName.isNumberOnly = true;
				{
					foreach (KOTOrderMaster item in db.KOTOrderMasters())
					{
						if (!cmbProductGroup.Items.Contains(item.Terminal))
						{
							cmbProductGroup.Items.Add(item.Terminal);
						}
					}
					return;
				}
			}
			if (Utility.iReportFormNumber != 37)
			{
				return;
			}
			lblHead.Text = "Pending KOT Report";
			Label label27 = lblFrom;
			Label label28 = lblTo;
			AshDateTimePicker ashDateTimePicker16 = dtmFrom;
			dtmTo.Visible = true;
			ashDateTimePicker16.Visible = true;
			label28.Visible = true;
			label27.Visible = true;
			AshComboBox ashComboBox19 = cmbProductGroup;
			lblProductGroup.Visible = true;
			ashComboBox19.Visible = true;
			lblProductGroup.Text = "Terminal :";
			Label label29 = lblProductName;
			txtProductName.Visible = true;
			label29.Visible = true;
			lblProductName.Text = "Sequence No :";
			txtProductName.isNumberOnly = true;
			foreach (KOTOrderMaster item2 in db.KOTOrderMasters())
			{
				if (!cmbProductGroup.Items.Contains(item2.Terminal))
				{
					cmbProductGroup.Items.Add(item2.Terminal);
				}
			}
		}

		private void btnShowReport_Click(object sender, EventArgs e)
		{
			try
			{
				int? iBranchId = null;
				if (lblBranch.Visible)
				{
					iBranchId = cmbBranch.SelectedValue.ToInt32();
				}
				DataTable dataTable = new DataTable();
				ReportDataSource item = new ReportDataSource();
				ReportParameter reportParameter = new ReportParameter("FromDate", dtmFrom.Value.AddSeconds(1.0).ToString());
				ReportParameter reportParameter2 = new ReportParameter("ToDate", dtmTo.Text);
				ReportParameter reportParameter3 = new ReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat);
				ReportParameter reportParameter4 = new ReportParameter("DateFormat", CommonSettings.strDateFormat);
				if (Utility.iReportFormNumber == 11)
				{
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptPOSPayment.rdlc";
					dataTable = ReportHelperForPOS.GetPaymentReportForPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					decimal num3 = default(decimal);
					foreach (DataRow row in dataTable.Rows)
					{
						num += row["Total"].ToString().ToDecimal();
						num2 += row["Credit"].ToString().ToDecimal();
					}
					ReportParameter reportParameter5 = new ReportParameter("PaymentTotal", (num - num2).ToString(CommonSettings.strCurencyFormat));
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter5 });
					item = new ReportDataSource("dtPOSPayment", dataTable);
				}
				else if (Utility.iReportFormNumber == 12)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptTransactionReportPOS.rdlc";
					dataTable = ReportHelperForPOS.GetTransactionReportForPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtTransactionReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 13)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSalesSummaryReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SalesSummaryReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					ReportParameter reportParameter6 = new ReportParameter("ShopName", db.Companies().FirstOrDefault().InstituteName);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter6 });
					item = new ReportDataSource("dtSalesSummaryReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 14)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSalesTaxReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SalesTaxReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtSalesTaxReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 15)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptTaxCategoryReportPOS.rdlc";
					dataTable = ReportHelperForPOS.TaxCategoryReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtTaxCategoryReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 16)
				{
					int? productId = null;
					if (txtProductName.Text != string.Empty)
					{
						ProductEntity productEntity = db.Products().FirstOrDefault((ProductEntity x) => x.ProductName == txtProductName.Text);
						if (productEntity == null)
						{
							ProductBarcodeEntity productBarcode3 = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity x) => x.Barcode == txtProductName.Text);
							if (productBarcode3 != null)
							{
								productEntity = db.Products().FirstOrDefault((ProductEntity x) => x.ProductId == productBarcode3.FK_ProductId);
							}
						}
						if (productEntity == null)
						{
							Messages.ErrorMessage("Product Not Found");
							return;
						}
						productId = productEntity.ProductId.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptStockAndProfitPOS.rdlc";
					dataTable = ReportHelperForPOS.StockAndProfitPOS(productId, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtStockAndProfitPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 17)
				{
					int? groupId = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						groupId = cmbProductGroup.SelectedValue.ToInt32();
					}
					int? userId = null;
					if (txtProductName.Text != string.Empty)
					{
						userId = db.Users().FirstOrDefault((User x) => x.UserName == txtProductName.Text).UserId;
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProductGroupSales.rdlc";
					dataTable = ReportHelperForPOS.ProductGroupSales(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), groupId, userId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductGroupSales", dataTable);
				}
				else if (Utility.iReportFormNumber == 18)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProductProfitSalePOS.rdlc";
					dataTable = ReportHelperForPOS.ProductProfitSalePOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value));
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductProfitSalePOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 19)
				{
					int? productId2 = null;
					if (txtProductName.Text != string.Empty)
					{
						ProductEntity productEntity2 = db.Products().FirstOrDefault((ProductEntity x) => x.ProductName == txtProductName.Text);
						if (productEntity2 == null)
						{
							ProductBarcodeEntity productBarcode2 = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity x) => x.Barcode == txtProductName.Text);
							if (productBarcode2 != null)
							{
								productEntity2 = db.Products().FirstOrDefault((ProductEntity x) => x.ProductId == productBarcode2.FK_ProductId);
							}
						}
						if (productEntity2 == null)
						{
							Messages.ErrorMessage("Product Not Found");
							return;
						}
						productId2 = productEntity2.ProductId;
					}
					int? groupId2 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						groupId2 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSaleByProductPOS.rdlc";
					dataTable = ReportHelperForPOS.SaleByProductPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), productId2, groupId2);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtSaleByProductPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 20)
				{
					int? userId2 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						userId2 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCashClosePOS.rdlc";
					dataTable = ReportHelperForPOS.CashClosePOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), userId2);
					ReportParameter reportParameter7 = new ReportParameter("ReportLanguage", Settings.Default.Language);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter7 });
					item = new ReportDataSource("dtCashClosePOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 21)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCashFlow.rdlc";
					dataTable = ReportHelperForPOS.CashFlow(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32());
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtCashFlow", dataTable);
				}
				else if (Utility.iReportFormNumber == 22)
				{
					int? customerId = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						customerId = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCustomerReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerReportPOS(customerId, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtCustomerReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 23)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCustomerLedgerReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerLedgerReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32());
					foreach (DataRow row2 in dataTable.Rows)
					{
						if (row2[2].ToString() == "Opening Balance" && row2[7].ToString() == string.Empty)
						{
							dataTable.Rows.Remove(row2);
							break;
						}
					}
					ReportParameter reportParameter8 = new ReportParameter("CustomerName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter8 });
					item = new ReportDataSource("dtCustomerLedgerReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 24)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCustomerSalesReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerSalesReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32());
					ReportParameter reportParameter9 = new ReportParameter("CustomerName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter9 });
					item = new ReportDataSource("dtCustomerSalesReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 25)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptCustomerItemSalesReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerItemSalesReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
					ReportParameter reportParameter10 = new ReportParameter("CustomerName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter10 });
					item = new ReportDataSource("dtCustomerItemSalesReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 26)
				{
					int? groupId3 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						groupId3 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProductStockReportPOS.rdlc";
					dataTable = ReportHelperForPOS.ProductStockReportPOS(groupId3, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductStockReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 27)
				{
					int? supplierId = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						supplierId = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProwisePurchasePO.rdlc";
					dataTable = ReportHelperForPOS.ProwisePurchasePO(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), supplierId, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProwisePurchasePO", dataTable);
				}
				else if (Utility.iReportFormNumber == 28)
				{
					int? productId3 = null;
					if (txtProductName.Text != string.Empty)
					{
						ProductEntity productEntity3 = db.Products().FirstOrDefault((ProductEntity x) => x.ProductName == txtProductName.Text);
						if (productEntity3 == null)
						{
							ProductBarcodeEntity productBarcode = db.ProductBarcodes().FirstOrDefault((ProductBarcodeEntity x) => x.Barcode == txtProductName.Text);
							if (productBarcode != null)
							{
								productEntity3 = db.Products().FirstOrDefault((ProductEntity x) => x.ProductId == productBarcode.FK_ProductId);
							}
						}
						if (productEntity3 == null)
						{
							Messages.ErrorMessage("Product Not Found");
							return;
						}
						productId3 = productEntity3.ProductId;
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProductDifferencePOS.rdlc";
					dataTable = ReportHelperForPOS.ProductDifferencePOS(productId3, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductDifferencePOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 29)
				{
					int? supplierId2 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						supplierId2 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSupplierReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SupplierReportPOS(supplierId2, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtSupplierReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 30)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSupplierLedgerPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerLedgerReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32());
					foreach (DataRow row3 in dataTable.Rows)
					{
						if (row3[2].ToString() == "Opening Balance" && row3[7].ToString() == string.Empty)
						{
							dataTable.Rows.Remove(row3);
							break;
						}
					}
					ReportParameter reportParameter11 = new ReportParameter("Supplier", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter11 });
					item = new ReportDataSource("dtCustomerLedgerReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 31)
				{
					int? supplierId3 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						supplierId3 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProwisePurchasePO.rdlc";
					dataTable = ReportHelperForPOS.SupplierItemPurchaseReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), supplierId3, iBranchId);
					ReportParameter reportParameter12 = new ReportParameter("SupplierName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter12 });
					item = new ReportDataSource("dtProwisePurchasePO", dataTable);
				}
				else if (Utility.iReportFormNumber == 32)
				{
					int? supplierId4 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						supplierId4 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptSupplierPurchaseReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SupplierPurchaseReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), supplierId4, iBranchId);
					ReportParameter reportParameter13 = new ReportParameter("SupplierName", cmbProductGroup.Text);
					ReportParameter reportParameter14 = new ReportParameter("ShopName", db.Companies().FirstOrDefault().InstituteName);
					rvReport.LocalReport.SetParameters(new ReportParameter[6] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter13, reportParameter14 });
					item = new ReportDataSource("dtSupplierPurchaseReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 33)
				{
					int? groupId4 = null;
					if (cmbProductGroup.Text != string.Empty)
					{
						groupId4 = cmbProductGroup.SelectedValue.ToInt32();
					}
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptProductOpeningStockReport.rdlc";
					dataTable = ReportHelperForPOS.ProductOpeningStockReportPOS(groupId4);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductOpeningStockReport", dataTable);
				}
				else if (Utility.iReportFormNumber == 34)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptUserSales.rdlc";
					dataTable = ReportHelperForPOS.UserSalesReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtUserSales", dataTable);
				}
				else if (Utility.iReportFormNumber == 35)
				{
					Company company = db.Companies().FirstOrDefault();
					if (cmbProductGroup.SelectedIndex == 0)
					{
						rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptVatSubmissionAr.rdlc";
					}
					else
					{
						rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptVatSubmissionEn.rdlc";
					}
					dataTable = ReportHelperForPOS.VatSubmissionReport(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					ReportParameter reportParameter15 = new ReportParameter("ShopName", company.InstituteName);
					ReportParameter reportParameter16 = new ReportParameter("ShopTaxId", company.GSTNo);
					ReportParameter reportParameter17 = new ReportParameter("SequenceNo", txtProductName.Data.ToString());
					ReportParameter reportParameter18 = new ReportParameter("CurrentDate", DateTime.Now.Date.ToString("dd-MM-yyyy"));
					decimal num4 = default(decimal);
					decimal num5 = default(decimal);
					decimal num6 = default(decimal);
					decimal num7 = default(decimal);
					decimal num8 = default(decimal);
					decimal num9 = default(decimal);
					decimal num10 = default(decimal);
					decimal num11 = default(decimal);
					decimal num12 = default(decimal);
					foreach (DataRow row4 in dataTable.Rows)
					{
						num4 = row4["TotalSalesAmount"].ToString().ToDecimal();
						num5 = row4["TotalVatSale"].ToString().ToDecimal();
						num6 = row4["TotalSalesVat"].ToString().ToDecimal();
						num7 = row4["TotalPurchaseAmount"].ToString().ToDecimal();
						num8 = row4["TotalVatPurchase"].ToString().ToDecimal();
						num9 = row4["ZeroVatSales"].ToString().ToDecimal();
						num10 = row4["ZeroVatPurchase"].ToString().ToDecimal();
						num11 = row4["VatDifference"].ToString().ToDecimal();
						num12 = row4["TotalPurchaseVat"].ToString().ToDecimal();
						num5 -= num6;
						num4 -= num6;
						num8 -= num12;
						num7 -= num12;
					}
					ReportParameter reportParameter19 = new ReportParameter("TotalSalesAmount", num4.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter20 = new ReportParameter("TotalVatSale", num5.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter21 = new ReportParameter("TotalSalesVat", num6.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter22 = new ReportParameter("TotalPurchaseAmount", num7.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter23 = new ReportParameter("TotalVatPurchase", num8.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter24 = new ReportParameter("ZeroVatSales", num9.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter25 = new ReportParameter("ZeroVatPurchase", num10.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter26 = new ReportParameter("VatDifference", num11.ToString(CommonSettings.strCurencyFormat));
					ReportParameter reportParameter27 = new ReportParameter("TotalPurchaseVat", num12.ToString(CommonSettings.strCurencyFormat));
					rvReport.LocalReport.SetParameters(new ReportParameter[17]
					{
						reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter15, reportParameter16, reportParameter17, reportParameter18, reportParameter19, reportParameter20,
						reportParameter21, reportParameter22, reportParameter23, reportParameter24, reportParameter25, reportParameter26, reportParameter27
					});
				}
				else if (Utility.iReportFormNumber == 36)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptVoidKOTReports.rdlc";
					int? iSequence = null;
					string terminal = null;
					if (txtProductName.Data > 0)
					{
						iSequence = txtProductName.Data;
					}
					if (cmbProductGroup.Text != string.Empty)
					{
						terminal = cmbProductGroup.Text;
					}
					dataTable = ReportHelperForPOS.VoidItemsReport(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), terminal, iSequence, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtVoidKOTReports", dataTable);
				}
				else if (Utility.iReportFormNumber == 37)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\ReportForms\\rptPendingKOT.rdlc";
					int? iSequence2 = null;
					string terminal2 = null;
					if (txtProductName.Data > 0)
					{
						iSequence2 = txtProductName.Data;
					}
					if (cmbProductGroup.Text != string.Empty)
					{
						terminal2 = cmbProductGroup.Text;
					}
					dataTable = ReportHelperForPOS.PendingKOTReport(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), terminal2, iSequence2, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtPendingKOT", dataTable);
				}
				if (Utility.iReportFormNumber != 35)
				{
					rvReport.LocalReport.DataSources.Clear();
					rvReport.LocalReport.DataSources.Add(item);
				}
				rvReport.RefreshReport();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtProductName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnShowReport.PerformClick();
			}
		}

		private void btnDate_Click(object sender, EventArgs e)
		{
			dtmTo.Value = DateTime.Now.Date.AddHours(24.0).AddSeconds(-1.0);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSReportView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDate = new System.Windows.Forms.Button();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.cmbProductGroup = new AshUsercontrol.AshComboBox();
            this.txtProductName = new AshUsercontrol.AshTextBoxNormal();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.lblBranch = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.lblProductGroup = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rvReport = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnDate);
            this.panel1.Controls.Add(this.cmbBranch);
            this.panel1.Controls.Add(this.cmbProductGroup);
            this.panel1.Controls.Add(this.txtProductName);
            this.panel1.Controls.Add(this.btnShowReport);
            this.panel1.Controls.Add(this.dtmTo);
            this.panel1.Controls.Add(this.lblBranch);
            this.panel1.Controls.Add(this.dtmFrom);
            this.panel1.Controls.Add(this.lblProductGroup);
            this.panel1.Controls.Add(this.lblProductName);
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Controls.Add(this.lblTo);
            this.panel1.Name = "panel1";
            // 
            // btnDate
            // 
            resources.ApplyResources(this.btnDate, "btnDate");
            this.btnDate.Name = "btnDate";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            // 
            // cmbProductGroup
            // 
            resources.ApplyResources(this.cmbProductGroup, "cmbProductGroup");
            this.cmbProductGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProductGroup.FormattingEnabled = true;
            this.cmbProductGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbProductGroup.Name = "cmbProductGroup";
            this.cmbProductGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            // 
            // txtProductName
            // 
            resources.ApplyResources(this.txtProductName, "txtProductName");
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductName.Data = 0;
            this.txtProductName.EnterColor = System.Drawing.Color.LightPink;
            this.txtProductName.Format = null;
            this.txtProductName.isAllowNegative = false;
            this.txtProductName.isAllowSpecialChar = false;
            this.txtProductName.isNumberOnly = false;
            this.txtProductName.isNumeric = false;
            this.txtProductName.LeaveColor = System.Drawing.Color.White;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductName_KeyDown);
            // 
            // btnShowReport
            // 
            resources.ApplyResources(this.btnShowReport, "btnShowReport");
            this.btnShowReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShowReport.ForeColor = System.Drawing.Color.White;
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.UseVisualStyleBackColor = false;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // lblProductGroup
            // 
            resources.ApplyResources(this.lblProductGroup, "lblProductGroup");
            this.lblProductGroup.Name = "lblProductGroup";
            // 
            // lblProductName
            // 
            resources.ApplyResources(this.lblProductName, "lblProductName");
            this.lblProductName.Name = "lblProductName";
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.Name = "lblFrom";
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.rvReport);
            this.panel2.Name = "panel2";
            // 
            // rvReport
            // 
            resources.ApplyResources(this.rvReport, "rvReport");
            this.rvReport.Name = "rvReport";
            this.rvReport.ServerReport.BearerToken = null;
            // 
            // POSReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblHead);
            this.Name = "POSReportView";
            this.Load += new System.EventHandler(this.POSReportView_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
