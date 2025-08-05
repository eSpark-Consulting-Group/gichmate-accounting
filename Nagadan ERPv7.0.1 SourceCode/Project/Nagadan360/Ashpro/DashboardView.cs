using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AshFramework;

namespace Ashpro
{
	public class DashboardView : BaseFormTwo
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private FlowLayoutPanel flpGrpTotal;

		private Panel pnlCash;

		private Label lblCashInHand;

		private Label label2;

		private PictureBox pictureBox1;

		private Panel pnlCashAtBank;

		private Label lblCashAtBank;

		private Label label4;

		private PictureBox pictureBox2;

		private Panel pnlDebtors;

		private Label lblDebtors;

		private Label label5;

		private PictureBox pictureBox3;

		private Panel pnlCreditors;

		private Label lblCreditors;

		private Label label7;

		private PictureBox pictureBox4;

		private Panel pnlStockValue;

		private Label lblStockValue;

		private Label label6;

		private PictureBox pictureBox5;

		private FlowLayoutPanel flpDashBoard;

		private Chart chSalesAndPurchase;

		private Chart chGSales;

		private Chart chIncome;

		private Chart chExpense;

		private Chart chGroupStock;

		public DashboardView(DashboardPOS dashboardPOS)
		{
			InitializeComponent();
			base.Width = Utility.uResolutionWidth - dashboardPOS.btnPanelShow.Width * 2;
			base.Height = Utility.uResolutionHeight - dashboardPOS.pnlTitle.Height;
			int num = base.Width - 50;
			int num2 = base.Height / 7;
			flpGrpTotal.Size = new Size(num, num2);
			num /= 5;
			int num3 = (base.Width - 40) / 3;
			foreach (Control control in flpGrpTotal.Controls)
			{
				if (control is Panel)
				{
					Panel panel = (Panel)control;
					panel.Size = new Size(num, num2);
				}
			}
			chSalesAndPurchase.Size = new Size(num3 * 2, num2 * 3);
			chGSales.Size = (chExpense.Size = (chIncome.Size = (chGroupStock.Size = new Size(num3, num2 * 3))));
		}

		private async Task LoadChartLabelValue()
		{
			Label label = lblCashInHand;
			label.Text = await AniHelper.GetValueForChart("CASH IN HAND", AniHelper.GetDate(DateTime.Now.Date));
			Label label2 = lblCashAtBank;
			label2.Text = await AniHelper.GetValueForChart("CASH AT BANK", AniHelper.GetDate(DateTime.Now.Date));
			Label label3 = lblDebtors;
			label3.Text = await AniHelper.GetValueForChart("CUSTOMER/SUNDRY DEBTORS", AniHelper.GetDate(DateTime.Now.Date));
			Label label4 = lblCreditors;
			label4.Text = await AniHelper.GetValueForChart("SUPPLIERS/SUNDRY CREDITORS", AniHelper.GetDate(DateTime.Now.Date));
			Label label5 = lblStockValue;
			label5.Text = await AniHelper.GetStockValue();
		}

		private async Task GroupWiseSalesChartLoadMethod()
		{
			chGSales.Series[0].Points.Clear();
			new DataTable();
			foreach (DataRow dr in (await AniHelper.GetChartValuesForGroupWiseSales()).Rows)
			{
				chGSales.Series[0].Points.AddXY(dr[0].ToString(), dr[1].ToString());
			}
		}

		private async Task GroupWiseStockChartLoadMethod()
		{
			chGroupStock.Series[0].Points.Clear();
			new DataTable();
			foreach (DataRow dr in (await AniHelper.GetChartValuesForStock()).Rows)
			{
				chGroupStock.Series[0].Points.AddXY(dr[0].ToString(), dr[1].ToString());
			}
		}

		private async Task ExpenseChartLoadMethod()
		{
			chExpense.Series[0].Points.Clear();
			new DataTable();
			foreach (DataRow dr in (await AniHelper.GetChartValuesForExpense()).Rows)
			{
				chExpense.Series[0].Points.AddXY(dr[0].ToString(), dr[1].ToString());
			}
		}

		private async Task IncomeChartLoadMethod()
		{
			chIncome.Series[0].Points.Clear();
			new DataTable();
			foreach (DataRow dr in (await AniHelper.GetChartValuesForIncome()).Rows)
			{
				chIncome.Series[0].Points.AddXY(dr[0].ToString(), dr[1].ToString());
			}
		}

		private async Task SalesAndPurchaseChartLoadMethod()
		{
			chSalesAndPurchase.Series["Sales"].Points.Clear();
			chSalesAndPurchase.Series["Purchase"].Points.Clear();
			new DataTable();
			foreach (DataRow dr in (await AniHelper.GetChartValuesForSalesAndPurchase()).Rows)
			{
				chSalesAndPurchase.Series["Sales"].Points.AddXY(dr[0].ToString(), dr[1].ToString());
				chSalesAndPurchase.Series["Purchase"].Points.AddXY(dr[0].ToString(), dr[2].ToString());
			}
		}

		private async Task ChartLoadMethod()
		{
			try
			{
				await LoadChartLabelValue();
				await SalesAndPurchaseChartLoadMethod();
				await GroupWiseSalesChartLoadMethod();
				await IncomeChartLoadMethod();
				await ExpenseChartLoadMethod();
				await GroupWiseStockChartLoadMethod();
			}
			catch (Exception)
			{
			}
		}

		private async void DashboardView_Load(object sender, EventArgs e)
		{
			await ChartLoadMethod();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardView));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.flpGrpTotal = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.lblCashInHand = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlCashAtBank = new System.Windows.Forms.Panel();
            this.lblCashAtBank = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlDebtors = new System.Windows.Forms.Panel();
            this.lblDebtors = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlCreditors = new System.Windows.Forms.Panel();
            this.lblCreditors = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pnlStockValue = new System.Windows.Forms.Panel();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.flpDashBoard = new System.Windows.Forms.FlowLayoutPanel();
            this.chSalesAndPurchase = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chGSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chIncome = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chExpense = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chGroupStock = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flpGrpTotal.SuspendLayout();
            this.pnlCash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlCashAtBank.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlDebtors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnlCreditors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnlStockValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.flpDashBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chSalesAndPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGroupStock)).BeginInit();
            this.SuspendLayout();
            // 
            // flpGrpTotal
            // 
            resources.ApplyResources(this.flpGrpTotal, "flpGrpTotal");
            this.flpGrpTotal.Controls.Add(this.pnlCash);
            this.flpGrpTotal.Controls.Add(this.pnlCashAtBank);
            this.flpGrpTotal.Controls.Add(this.pnlDebtors);
            this.flpGrpTotal.Controls.Add(this.pnlCreditors);
            this.flpGrpTotal.Controls.Add(this.pnlStockValue);
            this.flpGrpTotal.Name = "flpGrpTotal";
            // 
            // pnlCash
            // 
            resources.ApplyResources(this.pnlCash, "pnlCash");
            this.pnlCash.Controls.Add(this.lblCashInHand);
            this.pnlCash.Controls.Add(this.label2);
            this.pnlCash.Controls.Add(this.pictureBox1);
            this.pnlCash.Name = "pnlCash";
            // 
            // lblCashInHand
            // 
            resources.ApplyResources(this.lblCashInHand, "lblCashInHand");
            this.lblCashInHand.ForeColor = System.Drawing.Color.White;
            this.lblCashInHand.Name = "lblCashInHand";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pnlCashAtBank
            // 
            resources.ApplyResources(this.pnlCashAtBank, "pnlCashAtBank");
            this.pnlCashAtBank.Controls.Add(this.lblCashAtBank);
            this.pnlCashAtBank.Controls.Add(this.label4);
            this.pnlCashAtBank.Controls.Add(this.pictureBox2);
            this.pnlCashAtBank.Name = "pnlCashAtBank";
            // 
            // lblCashAtBank
            // 
            resources.ApplyResources(this.lblCashAtBank, "lblCashAtBank");
            this.lblCashAtBank.ForeColor = System.Drawing.Color.White;
            this.lblCashAtBank.Name = "lblCashAtBank";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pnlDebtors
            // 
            resources.ApplyResources(this.pnlDebtors, "pnlDebtors");
            this.pnlDebtors.Controls.Add(this.lblDebtors);
            this.pnlDebtors.Controls.Add(this.label5);
            this.pnlDebtors.Controls.Add(this.pictureBox3);
            this.pnlDebtors.Name = "pnlDebtors";
            // 
            // lblDebtors
            // 
            resources.ApplyResources(this.lblDebtors, "lblDebtors");
            this.lblDebtors.ForeColor = System.Drawing.Color.White;
            this.lblDebtors.Name = "lblDebtors";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pnlCreditors
            // 
            resources.ApplyResources(this.pnlCreditors, "pnlCreditors");
            this.pnlCreditors.Controls.Add(this.lblCreditors);
            this.pnlCreditors.Controls.Add(this.label7);
            this.pnlCreditors.Controls.Add(this.pictureBox4);
            this.pnlCreditors.Name = "pnlCreditors";
            // 
            // lblCreditors
            // 
            resources.ApplyResources(this.lblCreditors, "lblCreditors");
            this.lblCreditors.ForeColor = System.Drawing.Color.White;
            this.lblCreditors.Name = "lblCreditors";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Name = "label7";
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pnlStockValue
            // 
            resources.ApplyResources(this.pnlStockValue, "pnlStockValue");
            this.pnlStockValue.Controls.Add(this.lblStockValue);
            this.pnlStockValue.Controls.Add(this.label6);
            this.pnlStockValue.Controls.Add(this.pictureBox5);
            this.pnlStockValue.Name = "pnlStockValue";
            // 
            // lblStockValue
            // 
            resources.ApplyResources(this.lblStockValue, "lblStockValue");
            this.lblStockValue.ForeColor = System.Drawing.Color.White;
            this.lblStockValue.Name = "lblStockValue";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // pictureBox5
            // 
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // flpDashBoard
            // 
            resources.ApplyResources(this.flpDashBoard, "flpDashBoard");
            this.flpDashBoard.Controls.Add(this.chSalesAndPurchase);
            this.flpDashBoard.Controls.Add(this.chGSales);
            this.flpDashBoard.Controls.Add(this.chIncome);
            this.flpDashBoard.Controls.Add(this.chExpense);
            this.flpDashBoard.Controls.Add(this.chGroupStock);
            this.flpDashBoard.Name = "flpDashBoard";
            // 
            // chSalesAndPurchase
            // 
            resources.ApplyResources(this.chSalesAndPurchase, "chSalesAndPurchase");
            this.chSalesAndPurchase.BackColor = System.Drawing.Color.Black;
            this.chSalesAndPurchase.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
            this.chSalesAndPurchase.BackSecondaryColor = System.Drawing.Color.White;
            this.chSalesAndPurchase.BorderSkin.BackColor = System.Drawing.Color.White;
            this.chSalesAndPurchase.BorderSkin.BorderColor = System.Drawing.Color.White;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX2.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY2.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.chSalesAndPurchase.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Black;
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.HeaderSeparatorColor = System.Drawing.Color.White;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.White;
            legend1.Name = "Legend1";
            legend1.TitleForeColor = System.Drawing.Color.Maroon;
            legend1.TitleSeparatorColor = System.Drawing.Color.White;
            this.chSalesAndPurchase.Legends.Add(legend1);
            this.chSalesAndPurchase.Name = "chSalesAndPurchase";
            this.chSalesAndPurchase.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.BorderColor = System.Drawing.Color.White;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.DodgerBlue;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.Name = "Sales";
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.LabelBackColor = System.Drawing.Color.Red;
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Legend = "Legend1";
            series2.Name = "Purchase";
            this.chSalesAndPurchase.Series.Add(series1);
            this.chSalesAndPurchase.Series.Add(series2);
            title1.ForeColor = System.Drawing.Color.White;
            title1.Name = "Title1";
            title1.Text = "Sales And Purchase";
            this.chSalesAndPurchase.Titles.Add(title1);
            // 
            // chGSales
            // 
            resources.ApplyResources(this.chGSales, "chGSales");
            this.chGSales.BackColor = System.Drawing.Color.Black;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisX.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.White;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            this.chGSales.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Black;
            legend2.ForeColor = System.Drawing.Color.White;
            legend2.Name = "Legend1";
            this.chGSales.Legends.Add(legend2);
            this.chGSales.Name = "chGSales";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.LabelForeColor = System.Drawing.Color.White;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chGSales.Series.Add(series3);
            title2.ForeColor = System.Drawing.Color.White;
            title2.Name = "Title1";
            title2.Text = "Group Wise Sales";
            this.chGSales.Titles.Add(title2);
            // 
            // chIncome
            // 
            resources.ApplyResources(this.chIncome, "chIncome");
            this.chIncome.BackColor = System.Drawing.Color.Black;
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisX.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea3.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea3.AxisX2.TitleForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LineColor = System.Drawing.Color.White;
            chartArea3.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea3.BackColor = System.Drawing.Color.Black;
            chartArea3.Name = "ChartArea1";
            this.chIncome.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Black;
            legend3.ForeColor = System.Drawing.Color.White;
            legend3.Name = "Legend1";
            this.chIncome.Legends.Add(legend3);
            this.chIncome.Name = "chIncome";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Income";
            this.chIncome.Series.Add(series4);
            // 
            // chExpense
            // 
            resources.ApplyResources(this.chExpense, "chExpense");
            this.chExpense.BackColor = System.Drawing.Color.Black;
            chartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea4.AxisX.LineColor = System.Drawing.Color.White;
            chartArea4.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea4.AxisY.LineColor = System.Drawing.Color.White;
            chartArea4.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea4.BackColor = System.Drawing.Color.Black;
            chartArea4.Name = "ChartArea1";
            this.chExpense.ChartAreas.Add(chartArea4);
            legend4.BackColor = System.Drawing.Color.Black;
            legend4.ForeColor = System.Drawing.Color.White;
            legend4.Name = "Legend1";
            this.chExpense.Legends.Add(legend4);
            this.chExpense.Name = "chExpense";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Expense";
            this.chExpense.Series.Add(series5);
            // 
            // chGroupStock
            // 
            resources.ApplyResources(this.chGroupStock, "chGroupStock");
            this.chGroupStock.BackColor = System.Drawing.Color.Black;
            chartArea5.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea5.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea5.AxisX.LineColor = System.Drawing.Color.White;
            chartArea5.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea5.AxisY.LineColor = System.Drawing.Color.White;
            chartArea5.BackColor = System.Drawing.Color.Black;
            chartArea5.Name = "ChartArea1";
            this.chGroupStock.ChartAreas.Add(chartArea5);
            legend5.BackColor = System.Drawing.Color.Black;
            legend5.ForeColor = System.Drawing.Color.White;
            legend5.Name = "Legend1";
            legend5.TitleForeColor = System.Drawing.Color.Transparent;
            this.chGroupStock.Legends.Add(legend5);
            this.chGroupStock.Name = "chGroupStock";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.LabelForeColor = System.Drawing.Color.Transparent;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chGroupStock.Series.Add(series6);
            title3.ForeColor = System.Drawing.Color.White;
            title3.Name = "Title1";
            title3.Text = "Group Wise Stock";
            this.chGroupStock.Titles.Add(title3);
            // 
            // DashboardView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.flpDashBoard);
            this.Controls.Add(this.flpGrpTotal);
            this.Name = "DashboardView";
            this.Load += new System.EventHandler(this.DashboardView_Load);
            this.flpGrpTotal.ResumeLayout(false);
            this.pnlCash.ResumeLayout(false);
            this.pnlCash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlCashAtBank.ResumeLayout(false);
            this.pnlCashAtBank.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlDebtors.ResumeLayout(false);
            this.pnlDebtors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnlCreditors.ResumeLayout(false);
            this.pnlCreditors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnlStockValue.ResumeLayout(false);
            this.pnlStockValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.flpDashBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chSalesAndPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGroupStock)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
