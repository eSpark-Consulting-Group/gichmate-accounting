using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class PriceListView : BaseForm
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

		private string strProductId;

		private int i;

		private int iArrayCount;

		private DBContext db;

		private IContainer components = null;

		private Label lblTitle;

		private Button btnShow;

		private Button btnClear;

		private CheckBox cbBrand;

		private CheckBox cbSpecification;

		private CheckBox cbGroup;

		private CheckBox cbProduct;

		private CheckBox cbSupplier;

		private CheckBox cbGodown;

		private AshComboBox cmbBrand;

		private AshComboBox cmbGroup;

		private AshComboBox cmbSupplier;

		private AshComboBox cmbGodown;

		private AshComboBox cmbSpecification;

		private AshComboBox cmbProduct;

		public PriceListView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ColumnsAddMethod(string strHeader, List<string> ary)
		{
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable("Select PCategoryName from PriceCategory");
			foreach (DataRow row in dataTable.Rows)
			{
				ary.Add(strHeader + row[0].ToString());
			}
		}

		private void ReportGenerateMethod()
		{
			List<string> list = new List<string> { "SL", "ID", "Product", "Group", "Brand", "Unit", "Sales Rate", "Pur Rate", "MRP", "Stock" };
			ColumnsAddMethod("Pur", list);
			ColumnsAddMethod("Sal", list);
			Details = AniHelper.GetDataTable(list.ToArray());
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(50, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "L");
			iArrayCount = list.Count - 9;
			for (int i = 0; i < iArrayCount; i++)
			{
				Gridview.Rows.Add(120, "R");
			}
			string text = "";
			if (cmbGodown.Text != "")
			{
				text = " and Product.GodwnId='" + cmbGodown.SelectedValue.ToInt32() + "' ";
			}
			string text2 = "";
			if (cmbGroup.Text != "")
			{
				text2 = " and Product.PGroupId='" + cmbGroup.SelectedValue.ToInt32() + "' ";
			}
			string text3 = "";
			if (cmbBrand.Text != "")
			{
				text3 = " and Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "' ";
			}
			string text4 = " ";
			if (cmbSupplier.Text != "")
			{
				text4 = " and Product.AccountId='" + cmbSupplier.SelectedValue.ToInt32() + "' ";
			}
			string text5 = " ";
			if (cmbProduct.Text != "")
			{
				text5 = " and Product.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
			}
			if (cmbSpecification.Text != "")
			{
				_ = " and Product.Specification='" + cmbSpecification.Text + "' ";
			}
			int num = 0;
			decimal num2 = default(decimal);
			try
			{
				strQuery = "SELECT ProductCode,ProductName,PGroupName,BrandName,UnitName,PurchasePrice,SellingPrice,MRP,ProductId FROM Product Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId Join Unit on Product.UnitId=Unit.UnitId where Product.ProductName <>' '" + text + text2 + text3 + text4 + text5 + text4;
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					num = Details.Rows.Count + 1;
					num2 = BalanceFindMethodForCE(row[8].ToString());
					Details.Rows.Add(num, row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], num2.ToString("F"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			try
			{
				this.i = 0;
				DataTable dataTable2 = new DataTable();
				dataTable2 = AshproCommon.GetDataTable("Select ProductId from Product where Product.description=''" + text + text2 + text3 + text4 + text5 + text4);
				foreach (DataRow row2 in dataTable2.Rows)
				{
					strProductId = row2[0].ToString();
					PriceAddMethod("PPrice", "Pur");
					PriceAddMethod("SPrice", "Sal");
					this.i++;
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
			}
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(45, 45);
				LineData.Rows.Add(105, 105);
				LineData.Rows.Add(255, 255);
				LineData.Rows.Add(355, 355);
				LineData.Rows.Add(455, 455);
				LineData.Rows.Add(505, 505);
				LineData.Rows.Add(590, 590);
				LineData.Rows.Add(675, 675);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("SL", 14);
				Header.Rows.Add("Pro ID", 50);
				Header.Rows.Add("Pro Name", 110);
				Header.Rows.Add("Group", 258);
				Header.Rows.Add("Brand", 360);
				Header.Rows.Add("Unit", 460);
				Header.Rows.Add("Pur Rate", 510);
				Header.Rows.Add("Sal Rate", 595);
				Header.Rows.Add("MRP", 680);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(0, 10, false, false, "", 0);
				data.Rows.Add(1, 50, false, false, "", 0);
				data.Rows.Add(2, 105, false, true, "", 150);
				data.Rows.Add(3, 255, false, true, "", 100);
				data.Rows.Add(4, 355, false, true, "", 100);
				data.Rows.Add(5, 455, false, false, "", 0);
				data.Rows.Add(6, 589, false, false, "Right", 0);
				data.Rows.Add(7, 674, false, false, "Right", 0);
				data.Rows.Add(8, 798, false, false, "Right", 0);
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
			LineDataPDF.Rows.Add(520, 50, 520, 800);
			LineDataPDF.Rows.Add(455, 50, 455, 800);
			LineDataPDF.Rows.Add(390, 50, 390, 800);
			LineDataPDF.Rows.Add(360, 50, 360, 800);
			LineDataPDF.Rows.Add(280, 50, 280, 800);
			LineDataPDF.Rows.Add(200, 50, 200, 800);
			LineDataPDF.Rows.Add(78, 50, 78, 800);
			LineDataPDF.Rows.Add(38, 50, 38, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("SL", 9);
			HeaderPDF.Rows.Add("P Code", 39);
			HeaderPDF.Rows.Add("Pro Name", 79);
			HeaderPDF.Rows.Add("Group", 201);
			HeaderPDF.Rows.Add("Brand", 281);
			HeaderPDF.Rows.Add("Unit", 361);
			HeaderPDF.Rows.Add("Pur Price", 391);
			HeaderPDF.Rows.Add("Sal Price", 456);
			HeaderPDF.Rows.Add("MRP", 521);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(0, 10, false, false, "");
			dataPDF.Rows.Add(1, 39, false, false, "", 0);
			dataPDF.Rows.Add(2, 79, false, false, "", 0);
			dataPDF.Rows.Add(3, 201, false, false, "", 0);
			dataPDF.Rows.Add(4, 281, false, false, "", 0);
			dataPDF.Rows.Add(5, 361, false, false, "", 0);
			dataPDF.Rows.Add(6, 454, false, false, "Right", 0);
			dataPDF.Rows.Add(7, 519, false, false, "Right", 0);
			dataPDF.Rows.Add(8, 584, false, false, "Right", 0);
		}

		private decimal BalanceFindMethodForCE(string strPId)
		{
			return AshproCommon.ValueFindMethod("Select sum(InQuantity-OutQuantity) as Balance from StockTranLog  where ProductId='" + strPId + "'");
		}

		private void PriceAddMethod(string strMod, string strType)
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select PCategoryId,PCategoryName from PriceCategory");
				foreach (DataRow row in dataTable.Rows)
				{
					SubMethod(strMod, strType, row[0].ToString(), row[1].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SubMethod(string strMod, string strType, string strPCategoryId, string strPCategoryName)
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select " + strMod + " from ProductPriceCategory where ProductId='" + strProductId + "' and PCategoryId='" + strPCategoryId + "'");
				foreach (DataRow row in dataTable.Rows)
				{
					Details.Rows[i][strType + strPCategoryName] = row[0].ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PriceListView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillGodown(cmbGodown);
			AniHelper.FillSpecification(cmbSpecification);
			AniHelper.ClearComboMethod(this);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearComboMethod(this);
			cbGodown.Checked = false;
			cbBrand.Checked = false;
			cbProduct.Checked = false;
			cbSpecification.Checked = false;
			cbSupplier.Checked = false;
			cbGroup.Checked = false;
			cmbGodown.Focus();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			sTitle = "Product Price List Report";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceListView));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbBrand = new System.Windows.Forms.CheckBox();
            this.cbSpecification = new System.Windows.Forms.CheckBox();
            this.cbGroup = new System.Windows.Forms.CheckBox();
            this.cbProduct = new System.Windows.Forms.CheckBox();
            this.cbSupplier = new System.Windows.Forms.CheckBox();
            this.cbGodown = new System.Windows.Forms.CheckBox();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.cmbSupplier = new AshUsercontrol.AshComboBox();
            this.cmbGodown = new AshUsercontrol.AshComboBox();
            this.cmbSpecification = new AshUsercontrol.AshComboBox();
            this.cmbProduct = new AshUsercontrol.AshComboBox();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbBrand
            // 
            resources.ApplyResources(this.cbBrand, "cbBrand");
            this.cbBrand.Name = "cbBrand";
            this.cbBrand.UseVisualStyleBackColor = true;
            // 
            // cbSpecification
            // 
            resources.ApplyResources(this.cbSpecification, "cbSpecification");
            this.cbSpecification.Name = "cbSpecification";
            this.cbSpecification.UseVisualStyleBackColor = true;
            // 
            // cbGroup
            // 
            resources.ApplyResources(this.cbGroup, "cbGroup");
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.UseVisualStyleBackColor = true;
            // 
            // cbProduct
            // 
            resources.ApplyResources(this.cbProduct, "cbProduct");
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.UseVisualStyleBackColor = true;
            // 
            // cbSupplier
            // 
            resources.ApplyResources(this.cbSupplier, "cbSupplier");
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.UseVisualStyleBackColor = true;
            // 
            // cbGodown
            // 
            resources.ApplyResources(this.cbGodown, "cbGodown");
            this.cbGodown.Name = "cbGodown";
            this.cbGodown.UseVisualStyleBackColor = true;
            // 
            // cmbBrand
            // 
            resources.ApplyResources(this.cmbBrand, "cmbBrand");
            this.cmbBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBrand.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.LeaveColor = System.Drawing.Color.White;
            this.cmbBrand.Name = "cmbBrand";
            // 
            // cmbGroup
            // 
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroup.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.LeaveColor = System.Drawing.Color.White;
            this.cmbGroup.Name = "cmbGroup";
            // 
            // cmbSupplier
            // 
            resources.ApplyResources(this.cmbSupplier, "cmbSupplier");
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplier.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.LeaveColor = System.Drawing.Color.White;
            this.cmbSupplier.Name = "cmbSupplier";
            // 
            // cmbGodown
            // 
            resources.ApplyResources(this.cmbGodown, "cmbGodown");
            this.cmbGodown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGodown.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGodown.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGodown.FormattingEnabled = true;
            this.cmbGodown.LeaveColor = System.Drawing.Color.White;
            this.cmbGodown.Name = "cmbGodown";
            // 
            // cmbSpecification
            // 
            resources.ApplyResources(this.cmbSpecification, "cmbSpecification");
            this.cmbSpecification.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSpecification.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSpecification.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSpecification.FormattingEnabled = true;
            this.cmbSpecification.LeaveColor = System.Drawing.Color.White;
            this.cmbSpecification.Name = "cmbSpecification";
            // 
            // cmbProduct
            // 
            resources.ApplyResources(this.cmbProduct, "cmbProduct");
            this.cmbProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProduct.EnterColor = System.Drawing.Color.LightPink;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.LeaveColor = System.Drawing.Color.White;
            this.cmbProduct.Name = "cmbProduct";
            // 
            // PriceListView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbBrand);
            this.Controls.Add(this.cbSpecification);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.cbProduct);
            this.Controls.Add(this.cbSupplier);
            this.Controls.Add(this.cbGodown);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.cmbSpecification);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.cmbGodown);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Name = "PriceListView";
            this.Load += new System.EventHandler(this.PriceListView_Load);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.cmbGodown, 0);
            this.Controls.SetChildIndex(this.cmbSupplier, 0);
            this.Controls.SetChildIndex(this.cmbGroup, 0);
            this.Controls.SetChildIndex(this.cmbSpecification, 0);
            this.Controls.SetChildIndex(this.cmbBrand, 0);
            this.Controls.SetChildIndex(this.cmbProduct, 0);
            this.Controls.SetChildIndex(this.cbGodown, 0);
            this.Controls.SetChildIndex(this.cbSupplier, 0);
            this.Controls.SetChildIndex(this.cbProduct, 0);
            this.Controls.SetChildIndex(this.cbGroup, 0);
            this.Controls.SetChildIndex(this.cbSpecification, 0);
            this.Controls.SetChildIndex(this.cbBrand, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
