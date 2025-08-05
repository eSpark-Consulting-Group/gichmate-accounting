using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class Product_Register_Settings : BaseForm
	{
		private DataTable Details;

		private DataTable Gridview;

		private DataTable LineData;

		private DataTable Header;

		private DataTable data;

		private DataTable LineDataPDF;

		private DataTable HeaderPDF;

		private DataTable dataPDF;

		private bool isTaxable;

		private int iUnitId;

		private decimal dmlProStkTotal;

		private decimal dmlProStock;

		private decimal dmlQty;

		private decimal dmlLastPurRt;

		private decimal dmlDisAmnt;

		private decimal dmlPurQty;

		private decimal dmlLastPurCost;

		private decimal dmlLastSlsRt;

		private decimal dmlSlsQty;

		private decimal dmlLastSlsCost;

		private decimal dmlTaxRt;

		private string strGroupDetails;

		private string strBrandDetails;

		private string strProductDetails;

		private string strSupplierDetails;

		private string strGdwnDtls;

		private string strShelfDtls;

		private string strTCategoryDtls;

		private string strSpecDtls;

		private string strSlsDiscDtls;

		private string strSlsRtDtls;

		private string strPurRtDtls;

		private string strQuery;

		private string strProId;

		private string strBaseUnitName;

		private string strSupplierName;

		private string strGodownName;

		private string strShelfName;

		private string strTCategoryName;

		private string strPurUnitName;

		private string strSlsUnitName;

		private string strRptUnitName;

		private IContainer components = null;

		private Label label1;

		private AshComboBox cmbGroup;

		private Label label3;

		private AshComboBox cmbProduct;

		private AshComboBox cmbSupplier;

		private AshComboBox cmbBrand;

		private Label label2;

		private Label label4;

		private Label label5;

		private AshComboBox cmbGodown;

		private Label label6;

		private AshComboBox cmbShelf;

		private Label label7;

		private AshTextBoxNormal txtSpecification;

		private Label label8;

		private AshTextBoxNormal txtSlsDiscount;

		private Label label9;

		private AshComboBox cmbTCategory;

		private Button btnShow;

		private Button btnClear;

		private Label label11;

		private AshTextBoxNormal txtSlsRtFrom;

		private AshTextBoxNormal txtSlsRtTo;

		private Label label12;

		private AshTextBoxNormal txtPurRtFrom;

		private AshTextBoxNormal txtPurRtTo;

		private ToolTip toolTip1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem showRepoortToolStripMenuItem;

		private ToolStripMenuItem cliearFieldToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem focusProductGroupToolStripMenuItem;

		private Button button6;

		private Label label10;

		private AshDateTimePicker dtmDate;

		private Label label13;

		private AshComboBox cmbBranch;

		private Label lblBranch;

		public Product_Register_Settings()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ReportGenerateMethod()
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			if (CommonSettings.blnEnableMultiBranch && cmbBranch.Text != string.Empty)
			{
				text = " And SalesEstimateMaster.FK_BranchId='" + Settings.Default.iBranchId + "' ";
				text3 = "  And PurInvMaster.FK_BranchId='" + Settings.Default.iBranchId + "' ";
				text2 = " Join SalesEstimateMaster on SalesEstimateSettings.SalesInvId = SalesEstimateMaster.SalesInvId ";
				text4 = " Join PurInvMaster on PurInvSettings.PurInvId = PurInvMaster.PurInvId ";
			}
			string[] headers = new string[28]
			{
				"Code", "Product Name", "Group", "Brand", "Base Unit", "Pur Price", "Sls price", "MRP", "Stock", "Name in Second Language",
				"Supplier", "Godown", "Shelf", "Tax Category", "Last Pur Price", "Last Pur Cost", "Last Sls Rate", "Last Sls Amnt", "Pur Dis", "Sls Dis",
				"Tax", "Reorder Stock", "Default Sls Unit", "Default Pur Unit", "Default Rpt Unit", "Taxable", "Specification", "Description"
			};
			Details = AniHelper.GetDataTable(headers);
			Gridview = AniHelper.GetGridviewDatatable();
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(250, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(150, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(300, "L");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(120, "L");
			Gridview.Rows.Add(70, "L");
			Gridview.Rows.Add(80, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(120, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "L");
			Gridview.Rows.Add(100, "R");
			Gridview.Rows.Add(200, "L");
			Gridview.Rows.Add(200, "L");
			dmlProStkTotal = default(decimal);
			try
			{
				if (cmbGroup.Text != string.Empty)
				{
					strGroupDetails = " and Product.PGroupId='" + cmbGroup.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strGroupDetails = "";
				}
				if (cmbBrand.Text != string.Empty)
				{
					strBrandDetails = " and Product.BrandId='" + cmbBrand.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strBrandDetails = "";
				}
				if (cmbProduct.Text != string.Empty)
				{
					strProductDetails = " where Product.ProductId='" + cmbProduct.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strProductDetails = " where Product.ProductName<>'' ";
				}
				if (cmbSupplier.Text != string.Empty)
				{
					strSupplierDetails = " and Product.AccountId='" + cmbSupplier.SelectedValue.ToInt32() + "'";
				}
				else
				{
					strSupplierDetails = "";
				}
				if (cmbGodown.Text != string.Empty)
				{
					strGdwnDtls = " and Product.GodwnId='" + cmbGodown.SelectedValue.ToInt32() + "'";
				}
				else
				{
					strGdwnDtls = string.Empty;
				}
				if (cmbShelf.Text != string.Empty)
				{
					strShelfDtls = " and Product.SelfId='" + cmbShelf.SelectedValue.ToInt32() + "'";
				}
				else
				{
					strShelfDtls = string.Empty;
				}
				if (cmbTCategory.Text != string.Empty)
				{
					strTCategoryDtls = " and Product.TCategoryId='" + cmbTCategory.SelectedValue.ToInt32() + "'";
				}
				else
				{
					strTCategoryDtls = string.Empty;
				}
				if (txtSpecification.Text != string.Empty)
				{
					strSpecDtls = " and Prduct.Specification='" + txtSpecification.Text + "'";
				}
				else
				{
					strSpecDtls = string.Empty;
				}
				if (txtSlsDiscount.Value != 0m)
				{
					strSlsDiscDtls = " and Product.SaleDis='" + txtSlsDiscount.Value + "'";
				}
				else
				{
					strSlsDiscDtls = string.Empty;
				}
				if (txtSlsRtFrom.Value != 0m && txtSlsRtTo.Value != 0m)
				{
					strSlsRtDtls = " and Product.SellingPrice between '" + txtSlsRtFrom.Value + "' and '" + txtSlsRtTo.Value + "'";
				}
				else
				{
					strSlsRtDtls = string.Empty;
				}
				if (txtPurRtFrom.Value != 0m && txtPurRtTo.Value != 0m)
				{
					strPurRtDtls = " and Product.PurchasePrice between '" + txtPurRtFrom.Value + "' and '" + txtPurRtTo.Value + "'";
				}
				else
				{
					strPurRtDtls = string.Empty;
				}
				strQuery = "Select Product.ProductCode,Product.ProductName,ProductGroup.PGroupName,Brand.BrandName,Product.ProductId,Product.UnitId,Product.PurchasePrice,Product.SellingPrice,Product.MRP,Product.LocalName,Product.AccountId,Product.GodwnId, Product.SelfId, Product.TCategoryId,Product.PurchaseDis,Product.SaleDis,Product.ReOrderStock,Product.PurchaseUnit,Product.SalesUnit,Product.ReportUnit,Product.Specification,Product.Remarks from Product  Join ProductGroup on Product.PGroupId=ProductGroup.PGroupId Join Brand on Product.BrandId=Brand.BrandId " + strProductDetails + strGroupDetails + strBrandDetails + strSupplierDetails + strGdwnDtls + strShelfDtls + strTCategoryDtls + strSpecDtls + strSlsDiscDtls + strSlsRtDtls + strPurRtDtls + " order by Product.ProductCode";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(strQuery);
				foreach (DataRow row in dataTable.Rows)
				{
					strProId = row[4].ToString();
					iUnitId = row[5].ToInt32();
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", iUnitId.ToString(), out strBaseUnitName);
					dmlProStock = AshproCommon.ValueFindMethod("Select Sum(InQuantity-OutQuantity) as OppStock From StockTranLog Where ProductId='" + strProId + "' and VchrDate <= '" + AniHelper.GetDate(dtmDate.Value) + "'");
					dmlProStkTotal += dmlProStock;
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[10].ToString(), out strSupplierName);
					AshproCommon.IdFindMethod("Godown", "GodownName", "GodwnId", row[11].ToString(), out strGodownName);
					AshproCommon.IdFindMethod("Self", "SelfName", "SelfId", row[12].ToString(), out strShelfName);
					AshproCommon.IdFindMethod("TaxCategory", "TCategoryName", "TCategoryId", row[13].ToString(), out strTCategoryName);
					string text5 = null;
					DateTime? obj = AshproCommon.IdFindMethod("Select max(Date) as lastdate From PurInvSettings " + text4 + " Where ProId='" + strProId + "'" + text3);
					if (obj.HasValue)
					{
						text5 = AniHelper.GetDate(obj.ToDateTime());
						dmlLastPurRt = AshproCommon.ValueFindMethod("Select ProRate From PurInvSettings " + text4 + " Where ProId='" + strProId + "'and Date='" + text5 + "'" + text3);
						dmlDisAmnt = AshproCommon.ValueFindMethod("Select DiscAmnt From PurInvSettings " + text4 + " Where ProId='" + strProId + "' and Date='" + text5 + "'" + text3);
						dmlPurQty = AshproCommon.ValueFindMethod("Select ProQty From PurInvSettings " + text4 + " Where ProId='" + strProId + "' and Date='" + text5 + "' " + text3);
						iUnitId = AshproCommon.IdFindMethod("Select UnitId From PurInvSettings " + text4 + " Where ProId='" + strProId + "' and Date='" + text5 + "' " + text3);
						dmlQty = AshproCommon.ValueFindMethod("Select UnitQty From ProductUnit  Where ProductId='" + strProId + "' and UnitId='" + iUnitId + "'");
						if (dmlLastPurRt != 0m && dmlQty != 0m)
						{
							dmlLastPurRt /= dmlQty;
						}
						if (dmlDisAmnt != 0m && dmlPurQty != 0m && dmlQty != 0m)
						{
							dmlDisAmnt /= dmlPurQty;
							dmlLastPurCost = dmlDisAmnt / dmlQty;
						}
						else
						{
							dmlLastPurCost = dmlLastPurRt;
						}
					}
					else
					{
						dmlLastPurCost = (dmlLastPurRt = 0m);
					}
					obj = AshproCommon.IdFindMethod("Select max(Date) as lastdate From SalesEstimateSettings Where ProductId='" + strProId + "'");
					if (obj.HasValue)
					{
						text5 = AniHelper.GetDate(obj.ToDateTime());
						dmlLastSlsRt = AshproCommon.ValueFindMethod("Select ProRate From SalesEstimateSettings " + text2 + " Where ProductId='" + strProId + "'and Date='" + text5 + "'" + text);
						dmlDisAmnt = AshproCommon.ValueFindMethod("Select DiscAmnt From SalesEstimateSettings " + text2 + " Where ProductId='" + strProId + "' and Date='" + text5 + "'" + text);
						dmlSlsQty = AshproCommon.ValueFindMethod("Select ProQty From SalesEstimateSettings " + text2 + " Where ProductId='" + strProId + "' and Date='" + text5 + "' " + text);
						iUnitId = AshproCommon.IdFindMethod("Select UnitId From SalesEstimateSettings " + text2 + " Where ProductId='" + strProId + "' and Date='" + text5 + "' " + text);
						dmlQty = AshproCommon.ValueFindMethod("Select UnitQty From ProductUnit Where ProductId='" + strProId + "' and UnitId='" + iUnitId + "'");
						if (dmlLastSlsRt != 0m && dmlQty != 0m)
						{
							dmlLastSlsRt /= dmlQty;
						}
						if (dmlDisAmnt != 0m && dmlPurQty != 0m && dmlQty != 0m)
						{
							dmlDisAmnt /= dmlPurQty;
							dmlLastSlsCost = dmlDisAmnt / dmlQty;
						}
						else
						{
							dmlLastSlsCost = dmlLastSlsRt;
						}
					}
					else
					{
						dmlLastSlsCost = (dmlLastSlsRt = 0m);
					}
					dmlTaxRt = AshproCommon.IdFindMethod("Select SalesVat From TaxCategory Where TCategoryId='" + row[13].ToInt32() + "'");
					if (dmlTaxRt != 0m)
					{
						isTaxable = true;
					}
					else
					{
						isTaxable = false;
					}
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[17].ToString(), out strPurUnitName);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[18].ToString(), out strSlsUnitName);
					AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", row[19].ToString(), out strRptUnitName);
					Details.Rows.Add(row[0], row[1], row[2], row[3], strBaseUnitName, row[6], row[7], row[8], dmlProStock.ToString("F"), row[9], strSupplierName, strGodownName, strShelfName, strTCategoryName, dmlLastPurRt, dmlLastPurCost, dmlLastSlsRt, dmlLastSlsCost, row[14], row[15], dmlTaxRt.ToString("F"), row[16], strPurUnitName, strSlsUnitName, strRptUnitName, isTaxable, row[20], row[21]);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			Details.Rows.Add("", "Total", "", "", "", "", "", "", dmlProStkTotal, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
		}

		private void ClearMethood()
		{
			cmbGroup.Text = string.Empty;
			cmbBrand.Text = string.Empty;
			cmbSupplier.Text = string.Empty;
			cmbGodown.Text = string.Empty;
			cmbShelf.Text = string.Empty;
			cmbTCategory.Text = string.Empty;
			txtSlsDiscount.Text = "0.00";
			txtSpecification.Text = string.Empty;
			txtPurRtFrom.Text = "0.00";
			txtPurRtTo.Text = "0.00";
			cmbGroup.Focus();
		}

		private void PrintMethod()
		{
			try
			{
				LineData = AniHelper.GetLineDatatable();
				LineData.Rows.Add(239, 239);
				LineData.Rows.Add(319, 319);
				LineData.Rows.Add(399, 399);
				LineData.Rows.Add(479, 479);
				LineData.Rows.Add(559, 559);
				LineData.Rows.Add(639, 639);
				LineData.Rows.Add(719, 719);
				Header = AniHelper.GetHeadDatatable();
				Header.Rows.Add("Product Name", 9);
				Header.Rows.Add("Pur Price", 240);
				Header.Rows.Add("Sls Price", 320);
				Header.Rows.Add("MRP", 400);
				Header.Rows.Add("Pur Rt (L)", 480);
				Header.Rows.Add("Sls Rt (L)", 560);
				Header.Rows.Add("Stock", 640);
				Header.Rows.Add("Unit", 720);
				data = AniHelper.GetDetailsDatatable();
				data.Rows.Add(1, 8, true, true, "", 230);
				data.Rows.Add(5, 240, true, false, "");
				data.Rows.Add(6, 320, true, false, "");
				data.Rows.Add(7, 400, true, false, "");
				data.Rows.Add(14, 480, true, false, "");
				data.Rows.Add(16, 560, true, false, "");
				data.Rows.Add(8, 640, true, false, "");
				data.Rows.Add(4, 720, false, false, "");
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
			LineDataPDF.Rows.Add(555, 50, 555, 800);
			LineDataPDF.Rows.Add(490, 50, 490, 800);
			LineDataPDF.Rows.Add(425, 50, 425, 800);
			LineDataPDF.Rows.Add(360, 50, 360, 800);
			LineDataPDF.Rows.Add(295, 50, 295, 800);
			LineDataPDF.Rows.Add(230, 50, 230, 800);
			LineDataPDF.Rows.Add(165, 50, 165, 800);
			HeaderPDF = AniHelper.GetHeadDatatable();
			HeaderPDF.Rows.Add("Product Name", 9);
			HeaderPDF.Rows.Add("Pur Price", 168);
			HeaderPDF.Rows.Add("Sls Price", 231);
			HeaderPDF.Rows.Add("MRP", 296);
			HeaderPDF.Rows.Add("Pur Rt (L)", 361);
			HeaderPDF.Rows.Add("Sls Rt (L)", 426);
			HeaderPDF.Rows.Add("Stock", 491);
			HeaderPDF.Rows.Add("Unit", 556);
			dataPDF = AniHelper.GetDetailsDatatable();
			dataPDF.Rows.Add(1, 10, true, true, "");
			dataPDF.Rows.Add(5, 166, true, false, "");
			dataPDF.Rows.Add(6, 231, true, false, "");
			dataPDF.Rows.Add(7, 296, true, false, "");
			dataPDF.Rows.Add(14, 361, true, false, "");
			dataPDF.Rows.Add(16, 426, true, false, "");
			dataPDF.Rows.Add(8, 491, true, false, "");
			dataPDF.Rows.Add(4, 556, false, false, "");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethood();
		}

		private void Product_Register_Settings_Load(object sender, EventArgs e)
		{
			AniHelper.FillProGroup(cmbGroup);
			AniHelper.FillBrand(cmbBrand);
			AniHelper.FillProduct(cmbProduct);
			AniHelper.FillGodown(cmbGodown);
			AniHelper.FillTCategory(cmbTCategory);
			AniHelper.FillAccountForPurchase(cmbSupplier, isCashAccntIncluded: false);
			AniHelper.FillBranch(cmbBranch);
			Label label = lblBranch;
			bool visible = (cmbBranch.Visible = CommonSettings.blnEnableMultiBranch);
			label.Visible = visible;
			AniHelper.ClearComboMethod(this);
		}

		private void cmbGodown_SelectedIndexChanged(object sender, EventArgs e)
		{
			AshproCommon.FillCombo(cmbShelf, "Select SelfId as Id,SelfName as Name From Self Where GodownId='" + cmbGodown.SelectedValue.ToInt32() + "'");
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			ReportGenerateMethod();
			PrintMethod();
			MailMethod();
			ReportView reportView = new ReportView();
			reportView = new ReportView();
			reportView.MdiParent = base.MdiParent;
			reportView.Details = Details;
			reportView.Gridview = Gridview;
			reportView.Title = "Product Register Report as on Date " + dtmDate.Text;
			reportView.data = data;
			reportView.dataPDF = dataPDF;
			reportView.LineData = LineData;
			reportView.LineDataPDF = LineDataPDF;
			reportView.Header = Header;
			reportView.HeaderPDF = HeaderPDF;
			reportView.Show();
		}

		private void Product_Register_Settings_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnShow_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				ClearMethood();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.G && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbGroup.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product_Register_Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGroup = new AshUsercontrol.AshComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProduct = new AshUsercontrol.AshComboBox();
            this.cmbSupplier = new AshUsercontrol.AshComboBox();
            this.cmbBrand = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGodown = new AshUsercontrol.AshComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbShelf = new AshUsercontrol.AshComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSpecification = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSlsDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTCategory = new AshUsercontrol.AshComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSlsRtFrom = new AshUsercontrol.AshTextBoxNormal();
            this.txtSlsRtTo = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPurRtFrom = new AshUsercontrol.AshTextBoxNormal();
            this.txtPurRtTo = new AshUsercontrol.AshTextBoxNormal();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showRepoortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cliearFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.focusProductGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbBranch = new AshUsercontrol.AshComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbGroup, resources.GetString("cmbGroup.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbProduct, resources.GetString("cmbProduct.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbSupplier, resources.GetString("cmbSupplier.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbBrand, resources.GetString("cmbBrand.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
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
            this.toolTip1.SetToolTip(this.cmbGodown, resources.GetString("cmbGodown.ToolTip"));
            this.cmbGodown.SelectedIndexChanged += new System.EventHandler(this.cmbGodown_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbShelf
            // 
            resources.ApplyResources(this.cmbShelf, "cmbShelf");
            this.cmbShelf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShelf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShelf.EnterColor = System.Drawing.Color.LightPink;
            this.cmbShelf.FormattingEnabled = true;
            this.cmbShelf.LeaveColor = System.Drawing.Color.White;
            this.cmbShelf.Name = "cmbShelf";
            this.toolTip1.SetToolTip(this.cmbShelf, resources.GetString("cmbShelf.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txtSpecification
            // 
            resources.ApplyResources(this.txtSpecification, "txtSpecification");
            this.txtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSpecification.Data = 0;
            this.txtSpecification.EnterColor = System.Drawing.Color.LightPink;
            this.txtSpecification.Format = null;
            this.txtSpecification.isAllowNegative = false;
            this.txtSpecification.isAllowSpecialChar = false;
            this.txtSpecification.isNumberOnly = false;
            this.txtSpecification.isNumeric = false;
            this.txtSpecification.LeaveColor = System.Drawing.Color.White;
            this.txtSpecification.Name = "txtSpecification";
            this.toolTip1.SetToolTip(this.txtSpecification, resources.GetString("txtSpecification.ToolTip"));
            this.txtSpecification.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // txtSlsDiscount
            // 
            resources.ApplyResources(this.txtSlsDiscount, "txtSlsDiscount");
            this.txtSlsDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsDiscount.Data = 0;
            this.txtSlsDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsDiscount.Format = null;
            this.txtSlsDiscount.isAllowNegative = false;
            this.txtSlsDiscount.isAllowSpecialChar = false;
            this.txtSlsDiscount.isNumberOnly = false;
            this.txtSlsDiscount.isNumeric = false;
            this.txtSlsDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtSlsDiscount.Name = "txtSlsDiscount";
            this.toolTip1.SetToolTip(this.txtSlsDiscount, resources.GetString("txtSlsDiscount.ToolTip"));
            this.txtSlsDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // cmbTCategory
            // 
            resources.ApplyResources(this.cmbTCategory, "cmbTCategory");
            this.cmbTCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbTCategory.FormattingEnabled = true;
            this.cmbTCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbTCategory.Name = "cmbTCategory";
            this.toolTip1.SetToolTip(this.cmbTCategory, resources.GetString("cmbTCategory.ToolTip"));
            // 
            // btnShow
            // 
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Name = "btnShow";
            this.toolTip1.SetToolTip(this.btnShow, resources.GetString("btnShow.ToolTip"));
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
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // txtSlsRtFrom
            // 
            resources.ApplyResources(this.txtSlsRtFrom, "txtSlsRtFrom");
            this.txtSlsRtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsRtFrom.Data = 0;
            this.txtSlsRtFrom.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsRtFrom.Format = null;
            this.txtSlsRtFrom.isAllowNegative = false;
            this.txtSlsRtFrom.isAllowSpecialChar = false;
            this.txtSlsRtFrom.isNumberOnly = false;
            this.txtSlsRtFrom.isNumeric = false;
            this.txtSlsRtFrom.LeaveColor = System.Drawing.Color.White;
            this.txtSlsRtFrom.Name = "txtSlsRtFrom";
            this.toolTip1.SetToolTip(this.txtSlsRtFrom, resources.GetString("txtSlsRtFrom.ToolTip"));
            this.txtSlsRtFrom.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtSlsRtTo
            // 
            resources.ApplyResources(this.txtSlsRtTo, "txtSlsRtTo");
            this.txtSlsRtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSlsRtTo.Data = 0;
            this.txtSlsRtTo.EnterColor = System.Drawing.Color.LightPink;
            this.txtSlsRtTo.Format = null;
            this.txtSlsRtTo.isAllowNegative = false;
            this.txtSlsRtTo.isAllowSpecialChar = false;
            this.txtSlsRtTo.isNumberOnly = false;
            this.txtSlsRtTo.isNumeric = false;
            this.txtSlsRtTo.LeaveColor = System.Drawing.Color.White;
            this.txtSlsRtTo.Name = "txtSlsRtTo";
            this.toolTip1.SetToolTip(this.txtSlsRtTo, resources.GetString("txtSlsRtTo.ToolTip"));
            this.txtSlsRtTo.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // txtPurRtFrom
            // 
            resources.ApplyResources(this.txtPurRtFrom, "txtPurRtFrom");
            this.txtPurRtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurRtFrom.Data = 0;
            this.txtPurRtFrom.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurRtFrom.Format = null;
            this.txtPurRtFrom.isAllowNegative = false;
            this.txtPurRtFrom.isAllowSpecialChar = false;
            this.txtPurRtFrom.isNumberOnly = false;
            this.txtPurRtFrom.isNumeric = false;
            this.txtPurRtFrom.LeaveColor = System.Drawing.Color.White;
            this.txtPurRtFrom.Name = "txtPurRtFrom";
            this.toolTip1.SetToolTip(this.txtPurRtFrom, resources.GetString("txtPurRtFrom.ToolTip"));
            this.txtPurRtFrom.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtPurRtTo
            // 
            resources.ApplyResources(this.txtPurRtTo, "txtPurRtTo");
            this.txtPurRtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurRtTo.Data = 0;
            this.txtPurRtTo.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurRtTo.Format = null;
            this.txtPurRtTo.isAllowNegative = false;
            this.txtPurRtTo.isAllowSpecialChar = false;
            this.txtPurRtTo.isNumberOnly = false;
            this.txtPurRtTo.isNumeric = false;
            this.txtPurRtTo.LeaveColor = System.Drawing.Color.White;
            this.txtPurRtTo.Name = "txtPurRtTo";
            this.toolTip1.SetToolTip(this.txtPurRtTo, resources.GetString("txtPurRtTo.ToolTip"));
            this.txtPurRtTo.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showRepoortToolStripMenuItem,
            this.cliearFieldToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.focusProductGroupToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.toolTip1.SetToolTip(this.contextMenuStrip1, resources.GetString("contextMenuStrip1.ToolTip"));
            // 
            // showRepoortToolStripMenuItem
            // 
            resources.ApplyResources(this.showRepoortToolStripMenuItem, "showRepoortToolStripMenuItem");
            this.showRepoortToolStripMenuItem.Name = "showRepoortToolStripMenuItem";
            // 
            // cliearFieldToolStripMenuItem
            // 
            resources.ApplyResources(this.cliearFieldToolStripMenuItem, "cliearFieldToolStripMenuItem");
            this.cliearFieldToolStripMenuItem.Name = "cliearFieldToolStripMenuItem";
            // 
            // closeToolStripMenuItem
            // 
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            // 
            // focusProductGroupToolStripMenuItem
            // 
            resources.ApplyResources(this.focusProductGroupToolStripMenuItem, "focusProductGroupToolStripMenuItem");
            this.focusProductGroupToolStripMenuItem.Name = "focusProductGroupToolStripMenuItem";
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.toolTip1.SetToolTip(this.button6, resources.GetString("button6.ToolTip"));
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmDate.Name = "dtmDate";
            this.toolTip1.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // cmbBranch
            // 
            resources.ApplyResources(this.cmbBranch, "cmbBranch");
            this.cmbBranch.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Items.AddRange(new object[] {
            resources.GetString("cmbBranch.Items"),
            resources.GetString("cmbBranch.Items1"),
            resources.GetString("cmbBranch.Items2")});
            this.cmbBranch.LeaveColor = System.Drawing.Color.White;
            this.cmbBranch.Name = "cmbBranch";
            this.toolTip1.SetToolTip(this.cmbBranch, resources.GetString("cmbBranch.ToolTip"));
            // 
            // lblBranch
            // 
            resources.ApplyResources(this.lblBranch, "lblBranch");
            this.lblBranch.Name = "lblBranch";
            this.toolTip1.SetToolTip(this.lblBranch, resources.GetString("lblBranch.ToolTip"));
            // 
            // Product_Register_Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtPurRtTo);
            this.Controls.Add(this.txtPurRtFrom);
            this.Controls.Add(this.txtSlsRtTo);
            this.Controls.Add(this.txtSlsRtFrom);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSlsDiscount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSpecification);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbProduct);
            this.Controls.Add(this.cmbTCategory);
            this.Controls.Add(this.cmbShelf);
            this.Controls.Add(this.cmbGodown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.Name = "Product_Register_Settings";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Product_Register_Settings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Product_Register_Settings_KeyDown);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbBrand, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbSupplier, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.cmbGodown, 0);
            this.Controls.SetChildIndex(this.cmbShelf, 0);
            this.Controls.SetChildIndex(this.cmbTCategory, 0);
            this.Controls.SetChildIndex(this.cmbProduct, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbGroup, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtSpecification, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.txtSlsDiscount, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtSlsRtFrom, 0);
            this.Controls.SetChildIndex(this.txtSlsRtTo, 0);
            this.Controls.SetChildIndex(this.txtPurRtFrom, 0);
            this.Controls.SetChildIndex(this.txtPurRtTo, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.button6, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblBranch, 0);
            this.Controls.SetChildIndex(this.cmbBranch, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
