using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class RemoteSyncView : BaseForm
	{
		private DBContext db = new DBContext();

		private RDBContext rdb = new RDBContext();

		private MasterValue config = new MasterValue();

		private MasterValue url = new MasterValue();

		private string con = null;

		private Company company = new Company();

		private CurrencyInfo currencyInfo = new CurrencyInfo();

		private IContainer components = null;

		private Label lblTitle;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label lblFrom;

		private Label lblTo;

		private CheckBox cbSalesSummary;

		private Button btnSync;

		public RemoteSyncView()
		{
			InitializeComponent();
		}

		private void SalesSummaryMethod()
		{
			try
			{
				if (cbSalesSummary.Checked)
				{
					DataTable dataTable = new DataTable();
					dataTable = ReportHelperForPOS.SalesSummaryReportPOS(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value));
					List<SalesSummaryEntity> list = new List<SalesSummaryEntity>();
					list = rdb.SalesSummaryEntities(dataTable, company.CompanyId);
					AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "salesSummary", con);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async Task LoadDashboardData()
		{
			try
			{
				AshproCommon.DatabaseMethod("Delete From dashboardDatas Where CompanyId='" + company.CompanyId + "'", con);
				DashboardData dashboardData = new DashboardData();
				DashboardData dashboardData2 = dashboardData;
				dashboardData2.CashInHand = await AniHelper.GetValueForChart("CASH IN HAND", AniHelper.GetDate(DateTime.Now.Date));
				DashboardData dashboardData3 = dashboardData;
				dashboardData3.CashAtBank = await AniHelper.GetValueForChart("CASH AT BANK", AniHelper.GetDate(DateTime.Now.Date));
				DashboardData dashboardData4 = dashboardData;
				dashboardData4.Debtors = await AniHelper.GetValueForChart("CUSTOMER/SUNDRY DEBTORS", AniHelper.GetDate(DateTime.Now.Date));
				DashboardData dashboardData5 = dashboardData;
				dashboardData5.Creditors = await AniHelper.GetValueForChart("SUPPLIERS/SUNDRY CREDITORS", AniHelper.GetDate(DateTime.Now.Date));
				DashboardData dashboardData6 = dashboardData;
				dashboardData6.CloasingStock = await AniHelper.GetStockValue();
				dashboardData.Company = company.InstituteName;
				dashboardData.Currency = currencyInfo.CurrencyCode;
				dashboardData.Date = DateTime.Now.ToShortDateString();
				dashboardData.CompanyId = company.CompanyId;
				dashboardData.TaxId = company.GSTNo;
				AshproCommon.InsertToDatabaseObj(dashboardData, "dashboardDatas", con);
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void TopDebtorsLoadMethods()
		{
			try
			{
				AshproCommon.DatabaseMethod("Delete From topDeptors Where Company='" + company.CompanyId + "'", con);
				List<TopDeptors> list = new List<TopDeptors>();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.TopDebtorsReport(AniHelper.GetDate(DateTime.Now.Date));
				list = rdb.TopDeptors(dataTable, company.CompanyId);
				AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "topDeptors", con);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void TopCreditorsLoadMethods()
		{
			try
			{
				AshproCommon.DatabaseMethod("Delete From topCreditors Where Company='" + company.CompanyId + "'", con);
				List<TopCreditors> list = new List<TopCreditors>();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.TopCreditors(AniHelper.GetDate(DateTime.Now.Date));
				list = rdb.TopCreditors(dataTable, company.CompanyId);
				AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "topCreditors", con);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void TopStockedProductLoadMethods()
		{
			try
			{
				AshproCommon.DatabaseMethod("Delete From topStocks Where Company='" + company.CompanyId + "'", con);
				List<TopStockedProduct> list = new List<TopStockedProduct>();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.TopStockedProducts(AniHelper.GetDate(DateTime.Now.Date));
				list = rdb.TopStockedProducts(dataTable, company.CompanyId);
				AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "topStocks", con);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void TopSoldProductLoadMethods()
		{
			try
			{
				AshproCommon.DatabaseMethod("Delete From topSoldProducts Where Company='" + company.CompanyId + "'", con);
				List<TopSoldProdduct> list = new List<TopSoldProdduct>();
				DataTable dataTable = new DataTable();
				dataTable = ReportHelperForPOS.TopSoldProduct(AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value));
				list = rdb.TopSoldProdducts(dataTable, company.CompanyId);
				AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "topSoldProducts", con);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void RemoteSyncView_Load(object sender, EventArgs e)
		{
			try
			{
				dtmFrom.Value = DateTime.Now.Date;
				company = db.Companies().FirstOrDefault();
				currencyInfo = db.CurrencyInfos(new
				{
					CurrencyID = CommonSettings.strDefaultCurrency.ToInt32()
				}).FirstOrDefault();
				con = "Server = 103.174.102.109; Database = Unipospro;User ID=sa;Password=ash@Pro#";
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private async void btnSync_Click(object sender, EventArgs e)
		{
			try
			{
				if (!AniHelper.CheckInternet())
				{
					Messages.WarningMessage("Please Check Your Internet Connection.");
					return;
				}
				if (company.CompanyId == string.Empty)
				{
					Messages.ErrorMessage("Company ID Not Created. Please contact the Distributor");
					return;
				}
				AshUsercontrol.ProgressBar frm = new AshUsercontrol.ProgressBar(this);
				await Task.Run(delegate
				{
					SalesSummaryMethod();
				});
				await LoadDashboardData();
				await Task.Run(delegate
				{
					TopDebtorsLoadMethods();
				});
				await Task.Run(delegate
				{
					TopCreditorsLoadMethods();
				});
				await Task.Run(delegate
				{
					TopSoldProductLoadMethods();
				});
				await Task.Run(delegate
				{
					TopStockedProductLoadMethods();
				});
				frm.Visible = false;
				Messages.SavedMessage();
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
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
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			lblFrom = new System.Windows.Forms.Label();
			lblTo = new System.Windows.Forms.Label();
			cbSalesSummary = new System.Windows.Forms.CheckBox();
			btnSync = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(432, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblTitle.Location = new System.Drawing.Point(-2, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(167, 29);
			lblTitle.TabIndex = 13;
			lblTitle.Text = "Remote Sync";
			dtmTo.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(183, 105);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(215, 23);
			dtmTo.TabIndex = 121;
			dtmFrom.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(182, 76);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(216, 23);
			dtmFrom.TabIndex = 122;
			lblFrom.AutoSize = true;
			lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			lblFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblFrom.Location = new System.Drawing.Point(93, 81);
			lblFrom.Name = "lblFrom";
			lblFrom.Size = new System.Drawing.Size(54, 17);
			lblFrom.TabIndex = 119;
			lblFrom.Text = "From :";
			lblTo.AutoSize = true;
			lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			lblTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblTo.Location = new System.Drawing.Point(109, 111);
			lblTo.Name = "lblTo";
			lblTo.Size = new System.Drawing.Size(37, 17);
			lblTo.TabIndex = 120;
			lblTo.Text = "To :";
			cbSalesSummary.AutoSize = true;
			cbSalesSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			cbSalesSummary.Location = new System.Drawing.Point(182, 148);
			cbSalesSummary.Name = "cbSalesSummary";
			cbSalesSummary.Size = new System.Drawing.Size(141, 21);
			cbSalesSummary.TabIndex = 123;
			cbSalesSummary.Text = "Sales Summary";
			cbSalesSummary.UseVisualStyleBackColor = true;
			btnSync.BackColor = System.Drawing.Color.DodgerBlue;
			btnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold);
			btnSync.ForeColor = System.Drawing.Color.White;
			btnSync.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSync.Location = new System.Drawing.Point(363, 188);
			btnSync.Name = "btnSync";
			btnSync.Size = new System.Drawing.Size(87, 59);
			btnSync.TabIndex = 126;
			btnSync.Text = "Sync";
			btnSync.UseVisualStyleBackColor = false;
			btnSync.Click += new System.EventHandler(btnSync_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(477, 268);
			base.Controls.Add(btnSync);
			base.Controls.Add(cbSalesSummary);
			base.Controls.Add(dtmTo);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(lblFrom);
			base.Controls.Add(lblTo);
			base.Controls.Add(lblTitle);
			base.Name = "RemoteSyncView";
			Text = "RemoteSyncView";
			base.Load += new System.EventHandler(RemoteSyncView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(lblTo, 0);
			base.Controls.SetChildIndex(lblFrom, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(cbSalesSummary, 0);
			base.Controls.SetChildIndex(btnSync, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
