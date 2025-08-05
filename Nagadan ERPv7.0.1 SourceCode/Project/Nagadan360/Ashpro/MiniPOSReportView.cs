using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
	public class MiniPOSReportView : BaseForm
	{
		private DBContext db = new DBContext();

		private PrinterSettings printersetting = new PrinterSettings();

		private IContainer components = null;

		private Panel panel1;

		private AshComboBox cmbProductGroup;

		private AshTextBoxNormal txtProductName;

		private Button btnShowReport;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label lblProductGroup;

		private Label lblProductName;

		private Label lblFrom;

		private Label lblTo;

		private Label lblHead;

		private Panel panel2;

		private ReportViewer rvReport;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public MiniPOSReportView()
		{
			try
			{
				CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
				InitializeComponent();
				ColorChange.Color(this);
				TranslatorHelper.LoadArabic(this);
				TranslatorHelper.ChangeToArabic(this);
				string miniReportPrinter = Settings.Default.MiniReportPrinter;
				if (miniReportPrinter != string.Empty)
				{
					rvReport.PrinterSettings.PrinterName = miniReportPrinter;
				}
				rvReport.SetDisplayMode(DisplayMode.PrintLayout);
				rvReport.ZoomMode = ZoomMode.Percent;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
			}
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

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			Messages.CloseConfirmationMessage(this);
		}

		private void POSReportView_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			rvReport.RefreshReport();
			dtmFrom.Value = DateTime.Now.Date;
			if (Utility.iReportFormNumber == 12)
			{
				lblHead.Text = MessageKeys.SaleTransactionReport;
			}
			else if (Utility.iReportFormNumber == 13)
			{
				lblHead.Text = MessageKeys.POSSalesSummaryReport;
			}
			else if (Utility.iReportFormNumber == 14)
			{
				lblHead.Text = MessageKeys.POSSalesTaxReport;
			}
			else if (Utility.iReportFormNumber == 15)
			{
				lblHead.Text = MessageKeys.POSSalesTaxCategoryReport;
			}
			else if (Utility.iReportFormNumber == 16)
			{
				lblHead.Text = MessageKeys.StockAndProfitReport;
				Label label = lblFrom;
				Label label2 = lblTo;
				AshDateTimePicker ashDateTimePicker = dtmFrom;
				dtmTo.Visible = false;
				ashDateTimePicker.Visible = false;
				label2.Visible = false;
				label.Visible = false;
				Label label3 = lblProductName;
				txtProductName.Visible = true;
				label3.Visible = true;
				btnShowReport.Location = new Point(585, 45);
				AutoCompleteMethod();
				base.ActiveControl = txtProductName;
			}
			else if (Utility.iReportFormNumber == 17)
			{
				lblHead.Text = MessageKeys.CategorySalesReport;
				Label label4 = lblProductGroup;
				AshComboBox ashComboBox = cmbProductGroup;
				Label label5 = lblProductName;
				txtProductName.Visible = true;
				label5.Visible = true;
				ashComboBox.Visible = true;
				label4.Visible = true;
				lblProductName.Text = "User Name :";
				AutoCompleteMethodUser();
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 18)
			{
				lblHead.Text = MessageKeys.ProductSalesProfitReport;
			}
			else if (Utility.iReportFormNumber == 19)
			{
				lblHead.Text = MessageKeys.SalesByProductReport;
				Label label6 = lblProductName;
				AshTextBoxNormal ashTextBoxNormal = txtProductName;
				AshComboBox ashComboBox2 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox2.Visible = true;
				ashTextBoxNormal.Visible = true;
				label6.Visible = true;
				AutoCompleteMethod();
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 20)
			{
				lblHead.Text = MessageKeys.CloseCashReport;
				AshComboBox ashComboBox3 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox3.Visible = true;
				lblProductGroup.Text = MessageKeys.UserName;
				AniHelper.FillUser(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 21)
			{
				lblHead.Text = MessageKeys.CashFlowReport;
				AshComboBox ashComboBox4 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox4.Visible = true;
				lblProductGroup.Text = MessageKeys.CashAccount;
				AniHelper.FillCashAccount(cmbProductGroup);
			}
			else if (Utility.iReportFormNumber == 22)
			{
				lblHead.Text = MessageKeys.CustomerReport;
				Label label7 = lblFrom;
				Label label8 = lblTo;
				AshDateTimePicker ashDateTimePicker2 = dtmFrom;
				dtmTo.Visible = false;
				ashDateTimePicker2.Visible = false;
				label8.Visible = false;
				label7.Visible = false;
				AshComboBox ashComboBox5 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox5.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 23)
			{
				lblHead.Text = MessageKeys.CustomerDiaryReport;
				AshComboBox ashComboBox6 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox6.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 24)
			{
				lblHead.Text = MessageKeys.CustomerSalesReport;
				AshComboBox ashComboBox7 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox7.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 25)
			{
				lblHead.Text = MessageKeys.CustomerItemSalesReport;
				AshComboBox ashComboBox8 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox8.Visible = true;
				lblProductGroup.Text = MessageKeys.Customer;
				AniHelper.FillAccount(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 26)
			{
				lblHead.Text = MessageKeys.ProductStockReport;
				Label label9 = lblFrom;
				Label label10 = lblTo;
				AshDateTimePicker ashDateTimePicker3 = dtmFrom;
				dtmTo.Visible = false;
				ashDateTimePicker3.Visible = false;
				label10.Visible = false;
				label9.Visible = false;
				AshComboBox ashComboBox9 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox9.Visible = true;
				lblProductGroup.Text = MessageKeys.Group;
				AniHelper.FillProGroup(cmbProductGroup);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 27)
			{
				lblHead.Text = MessageKeys.ProWisePurchaseReport;
				AshComboBox ashComboBox10 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox10.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 28)
			{
				lblHead.Text = MessageKeys.ProTranSummaryReport;
				Label label11 = lblFrom;
				Label label12 = lblTo;
				AshDateTimePicker ashDateTimePicker4 = dtmFrom;
				dtmTo.Visible = false;
				ashDateTimePicker4.Visible = false;
				label12.Visible = false;
				label11.Visible = false;
				Label label13 = lblProductName;
				txtProductName.Visible = true;
				label13.Visible = true;
			}
			else if (Utility.iReportFormNumber == 29)
			{
				lblHead.Text = MessageKeys.SupplierReport;
				Label label14 = lblFrom;
				Label label15 = lblTo;
				AshDateTimePicker ashDateTimePicker5 = dtmFrom;
				dtmTo.Visible = false;
				ashDateTimePicker5.Visible = false;
				label15.Visible = false;
				label14.Visible = false;
				AshComboBox ashComboBox11 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox11.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
				cmbProductGroup.Text = string.Empty;
			}
			else if (Utility.iReportFormNumber == 30)
			{
				lblHead.Text = MessageKeys.SupplierLedgerReport;
				AshComboBox ashComboBox12 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox12.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 32)
			{
				lblHead.Text = MessageKeys.SupplierPurchaseReport;
				AshComboBox ashComboBox13 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox13.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 31)
			{
				lblHead.Text = MessageKeys.SupplierItemPurchaseReport;
				AshComboBox ashComboBox14 = cmbProductGroup;
				lblProductGroup.Visible = true;
				ashComboBox14.Visible = true;
				lblProductGroup.Text = MessageKeys.Supplier;
				AniHelper.FillAccountForPurchase(cmbProductGroup, isCashAccntIncluded: false);
			}
			else if (Utility.iReportFormNumber == 33)
			{
				lblHead.Text = MessageKeys.UserSale;
				AshComboBox ashComboBox15 = cmbProductGroup;
				lblProductGroup.Visible = false;
				ashComboBox15.Visible = false;
				Label label16 = lblFrom;
				Label label17 = lblTo;
				AshDateTimePicker ashDateTimePicker6 = dtmFrom;
				dtmTo.Visible = true;
				ashDateTimePicker6.Visible = true;
				label17.Visible = true;
				label16.Visible = true;
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
				ReportParameter reportParameter = new ReportParameter("FromDate", dtmFrom.Text);
				ReportParameter reportParameter2 = new ReportParameter("ToDate", dtmTo.Text);
				ReportParameter reportParameter3 = new ReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat);
				ReportParameter reportParameter4 = new ReportParameter("DateFormat", CommonSettings.strDateFormat);
				if (Utility.iReportFormNumber == 11)
				{
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal num3 = default(decimal);
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptPOSPayment.rdlc";
					dataTable = ReportHelperForPOS.GetPaymentReportForPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					decimal num4 = default(decimal);
					foreach (DataRow row in dataTable.Rows)
					{
						num += row["Cash"].ToString().ToDecimal();
						num2 += row["Card"].ToString().ToDecimal();
						num3 += row["Online"].ToString().ToDecimal();
					}
					ReportParameter reportParameter5 = new ReportParameter("PaymentTotal", (num + num2 + num3).ToString(CommonSettings.strCurencyFormat));
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter5 });
					item = new ReportDataSource("dtPOSPayment", dataTable);
				}
				else if (Utility.iReportFormNumber == 12)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptTransactionReportPOS.rdlc";
					dataTable = ReportHelperForPOS.GetTransactionReportForPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtTransactionReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 13)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSalesSummaryReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SalesSummaryReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					ReportParameter reportParameter6 = new ReportParameter("ShopName", db.Companies().FirstOrDefault().InstituteName);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter6 });
					item = new ReportDataSource("dtSalesSummaryReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 14)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSalesTaxReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SalesTaxReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtSalesTaxReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 15)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptTaxCategoryReportPOS.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptStockAndProfitPOS.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProductGroupSales.rdlc";
					dataTable = ReportHelperForPOS.ProductGroupSales(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), groupId, userId, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtProductGroupSales", dataTable);
				}
				else if (Utility.iReportFormNumber == 18)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProductProfitSalePOS.rdlc";
					dataTable = ReportHelperForPOS.ProductProfitSalePOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSaleByProductPOS.rdlc";
					dataTable = ReportHelperForPOS.SaleByProductPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), productId2, groupId2, iBranchId);
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCashClosePOS.rdlc";
					dataTable = ReportHelperForPOS.CashClosePOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), userId2, iBranchId);
					ReportParameter reportParameter7 = new ReportParameter("ReportLanguage", Settings.Default.Language);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter7 });
					item = new ReportDataSource("dtCashClosePOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 21)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCashFlow.rdlc";
					dataTable = ReportHelperForPOS.CashFlow(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCustomerReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerReportPOS(customerId, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtCustomerReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 23)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCustomerLedgerReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerLedgerReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCustomerSalesReportPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerSalesReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
					ReportParameter reportParameter9 = new ReportParameter("CustomerName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter9 });
					item = new ReportDataSource("dtCustomerSalesReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 25)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptCustomerItemSalesReportPOS.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProductStockReportPOS.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProwisePurchasePO.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProductDifferencePOS.rdlc";
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSupplierReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SupplierReportPOS(supplierId2, iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtSupplierReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 30)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSupplierLedgerPOS.rdlc";
					dataTable = ReportHelperForPOS.CustomerLedgerReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
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
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptProwisePurchasePO.rdlc";
					dataTable = ReportHelperForPOS.SupplierItemPurchaseReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), supplierId3, iBranchId);
					ReportParameter reportParameter12 = new ReportParameter("SupplierName", cmbProductGroup.Text);
					rvReport.LocalReport.SetParameters(new ReportParameter[5] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter12 });
					item = new ReportDataSource("dtProwisePurchasePO", dataTable);
				}
				else if (Utility.iReportFormNumber == 32)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptSupplierPurchaseReportPOS.rdlc";
					dataTable = ReportHelperForPOS.SupplierPurchaseReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), cmbProductGroup.SelectedValue.ToInt32(), iBranchId);
					ReportParameter reportParameter13 = new ReportParameter("SupplierName", cmbProductGroup.Text);
					ReportParameter reportParameter14 = new ReportParameter("ShopName", db.Companies().FirstOrDefault().InstituteName);
					rvReport.LocalReport.SetParameters(new ReportParameter[6] { reportParameter, reportParameter2, reportParameter3, reportParameter4, reportParameter13, reportParameter14 });
					item = new ReportDataSource("dtSupplierPurchaseReportPOS", dataTable);
				}
				else if (Utility.iReportFormNumber == 33)
				{
					rvReport.LocalReport.ReportPath = Application.StartupPath + "\\MiniReportForm\\rptUserSales.rdlc";
					dataTable = ReportHelperForPOS.UserSalesReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), iBranchId);
					rvReport.LocalReport.SetParameters(new ReportParameter[4] { reportParameter, reportParameter2, reportParameter3, reportParameter4 });
					item = new ReportDataSource("dtUserSales", dataTable);
				}
				rvReport.LocalReport.DataSources.Clear();
				rvReport.LocalReport.DataSources.Add(item);
				rvReport.RefreshReport();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void txtProductName_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void MiniPOSReportView_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			dtmFrom.Value = DateTime.Now.Date;
			if (CommonSettings.blnEnableMultiBranch)
			{
				Label label = lblBranch;
				cmbBranch.Visible = true;
				label.Visible = true;
			}
			AniHelper.FillBranch(cmbBranch);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniPOSReportView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbProductGroup = new AshUsercontrol.AshComboBox();
            this.txtProductName = new AshUsercontrol.AshTextBoxNormal();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
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
            this.panel1.Controls.Add(this.cmbBranch);
            this.panel1.Controls.Add(this.lblBranch);
            this.panel1.Controls.Add(this.cmbProductGroup);
            this.panel1.Controls.Add(this.txtProductName);
            this.panel1.Controls.Add(this.btnShowReport);
            this.panel1.Controls.Add(this.dtmTo);
            this.panel1.Controls.Add(this.dtmFrom);
            this.panel1.Controls.Add(this.lblProductGroup);
            this.panel1.Controls.Add(this.lblProductName);
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Controls.Add(this.lblTo);
            this.panel1.Name = "panel1";
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            // 
            // cmbProductGroup
            // 
            resources.ApplyResources(this.cmbProductGroup, "cmbProductGroup");
            this.cmbProductGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProductGroup.FormattingEnabled = true;
            this.cmbProductGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbProductGroup.Name = "cmbProductGroup";
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
            // MiniPOSReportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHead);
            this.Name = "MiniPOSReportView";
            this.Load += new System.EventHandler(this.MiniPOSReportView_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
