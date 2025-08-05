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
	public class ProductEditing : BaseForm
	{
		private List<ExProduct> exProducts;

		private DBContext db;

		private string strAdapterResult;

		private string strUpdateData;

		private string strUnitDetails;

		private string strTCategoryDtls;

		private string strGdwnDtls;

		private string strSelfDtls;

		private string strHSNDtls;

		private string strPurDisDtls;

		private string strSlsDisDtls;

		private string strSpecDtls;

		private string strSearchItem;

		private string strAdapterData;

		private string strGrpDtls;

		private string strBrndDtls;

		private string strQuery;

		private string strProDtls;

		private IContainer components = null;

		private Button btnClear;

		private Button btnSave;

		private Panel panel1;

		public DataGridView dgDetails;

		private Button btnLoad;

		private Label label12;

		private Label label3;

		private AshComboBox cmbSearchWith;

		private AshComboBox cmbSearchBy;

		private Label label4;

		private AshComboBox cmbEditCategory;

		private Label label1;

		private AshComboBox cmbEditWith;

		private Label label2;

		private AshTextBoxNormal txtEditWith;

		private Button btnGenerate;

		private AshComboBox cmbSearchWith2;

		private Button button4;

		private Label lblHead;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private BindingSource bdsDetails;

		public ProductEditing()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ClearMethod()
		{
			AniHelper.ClearMethod(this);
			exProducts = new List<ExProduct>();
			BindDgv();
			label2.Visible = false;
			btnGenerate.Visible = false;
			cmbEditWith.Visible = false;
			txtEditWith.Visible = false;
			cmbSearchBy.Focus();
		}

		private void ReportShowMethod()
		{
			exProducts = new List<ExProduct>();
			if (cmbEditCategory.Text != "")
			{
				dgDetails.Columns[3].HeaderText = cmbEditCategory.Text;
			}
			try
			{
				LoadSwitchMethod();
				if (cmbEditCategory.Text == "")
				{
					strQuery = "Select ProductId,ProductCode,ProductName From Product  where Product.description=' '" + strGrpDtls + strBrndDtls + strUnitDetails + strTCategoryDtls + strGdwnDtls + strSelfDtls + strHSNDtls + strPurDisDtls + strSlsDisDtls + strSpecDtls + strProDtls + "Order by ProductCode";
					DataTable dataTable = new DataTable();
					dataTable = AshproCommon.GetDataTable(strQuery);
					foreach (DataRow row in dataTable.Rows)
					{
						ExProduct item = new ExProduct
						{
							SL = dgDetails.Rows.Count + 1,
							ProductId = row[0].ToInt32(),
							ProCode = row[1].ToInt32(),
							ProName = row[2].ToString(),
							Particulars = string.Empty
						};
						exProducts.Add(item);
					}
				}
				else
				{
					strAdapterData = string.Empty;
					LoadEditSwitchStatement();
					strQuery = "Select ProductId,ProductCode,ProductName  From Product  where Product.description=' '" + strGrpDtls + strBrndDtls + strUnitDetails + strTCategoryDtls + strGdwnDtls + strSelfDtls + strHSNDtls + strPurDisDtls + strSlsDisDtls + strSpecDtls + strProDtls + "Order by ProductCode";
					DataTable dataTable2 = new DataTable();
					dataTable2 = AshproCommon.GetDataTable(strQuery);
					foreach (DataRow row2 in dataTable2.Rows)
					{
						ExProduct item2 = new ExProduct
						{
							SL = exProducts.Count + 1,
							ProductId = row2[0].ToInt32(),
							ProCode = row2[1].ToInt32(),
							ProName = row2[2].ToString(),
							Particulars = strSearchItem
						};
						exProducts.Add(item2);
					}
				}
				BindDgv();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "213");
			}
			if (dgDetails.Rows.Count > 0)
			{
				dgDetails.Focus();
				dgDetails.CurrentCell = dgDetails.Rows[0].Cells[3];
				dgDetails.Columns[3].DefaultCellStyle.BackColor = Color.LightBlue;
			}
		}

		private void BindDgv()
		{
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exProducts;
			dgDetails.DataSource = bdsDetails;
		}

		private void AdapterSwitchStatement()
		{
			switch (cmbEditCategory.SelectedIndex)
			{
			default:
				strAdapterData = strAdapterData.Trim();
				break;
			case 0:
				AshproCommon.IdFindMethod("ProductGroup", "PGroupName", "PGroupId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			case 1:
				AshproCommon.IdFindMethod("Brand", "BrandName", "BrandId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			case 2:
				AshproCommon.IdFindMethod("Unit", "UnitName", "UnitId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			case 3:
				AshproCommon.IdFindMethod("TaxCategory", "TCategoryName", "TCategoryId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			case 4:
				AshproCommon.IdFindMethod("Godown", "GodownName", "GodwnId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			case 5:
				AshproCommon.IdFindMethod("Self", "SelfName", "SelfId", strAdapterData, out strAdapterResult);
				strAdapterData = strAdapterResult;
				break;
			}
		}

		private void LoadEditSwitchStatement()
		{
			int selectedIndex = cmbEditCategory.SelectedIndex;
			if (selectedIndex != 0 && selectedIndex != 1 && selectedIndex != 2 && selectedIndex != 3 && selectedIndex != 4 && selectedIndex != 5)
			{
				strSearchItem = txtEditWith.Text;
			}
			else
			{
				strSearchItem = cmbEditWith.Text;
			}
		}

		private void LoadSwitchMethod()
		{
			switch (cmbSearchBy.SelectedIndex)
			{
			case 0:
				if (cmbSearchWith.Text != "")
				{
					strGrpDtls = " and Product.PGroupId='" + cmbSearchWith.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strGrpDtls = "";
				}
				if (cmbSearchWith2.Text != "")
				{
					strBrndDtls = " and Product.BrandId='" + cmbSearchWith2.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strBrndDtls = "";
				}
				break;
			case 1:
				if (cmbSearchWith.Text != "")
				{
					strBrndDtls = " and Product.BrandId='" + cmbSearchWith.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strBrndDtls = "";
				}
				break;
			case 2:
				if (cmbSearchWith.Text != "")
				{
					strUnitDetails = " and Product.UnitId='" + cmbSearchWith.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strUnitDetails = "";
				}
				break;
			case 3:
				if (cmbSearchWith.Text != "")
				{
					strTCategoryDtls = " And Product.TCategoryId='" + cmbSearchWith.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strTCategoryDtls = "";
				}
				break;
			case 4:
				if (cmbSearchWith.Text != "")
				{
					strGdwnDtls = " and Product.GodwnId='" + cmbSearchWith.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strGdwnDtls = "";
				}
				break;
			case 5:
				if (cmbSearchWith.Text != "")
				{
					strSelfDtls = " and Product.GodwnId='" + cmbSearchWith.SelectedValue.ToInt32() + "' and  Product.SelfId='" + cmbSearchWith2.SelectedValue.ToInt32() + "' ";
				}
				else
				{
					strSelfDtls = "";
				}
				break;
			case 6:
				if (cmbSearchWith.Text != "")
				{
					strHSNDtls = " and Product.HSNNo='" + cmbSearchWith.Text + "' ";
				}
				else
				{
					strHSNDtls = "";
				}
				break;
			case 7:
				if (cmbSearchWith.Text != "")
				{
					strPurDisDtls = " and Product.PurchaseDis='" + cmbSearchWith.Text + "' ";
				}
				else
				{
					strPurDisDtls = "";
				}
				break;
			case 8:
				if (cmbSearchWith.Text != "")
				{
					strSlsDisDtls = " and Product.SaleDis='" + cmbSearchWith.Text + "' ";
				}
				else
				{
					strSlsDisDtls = "";
				}
				break;
			case 9:
				if (cmbSearchWith.Text != "")
				{
					strSpecDtls = " and Product.Specification='" + cmbSearchWith.Text + "' ";
				}
				else
				{
					strSpecDtls = "";
				}
				break;
			case 10:
				if (cmbSearchWith.Text != "")
				{
					strProDtls = " and Product.ProductName='" + cmbSearchWith.Text + "' ";
				}
				else
				{
					strProDtls = "";
				}
				break;
			}
		}

		private void SwitchStatement()
		{
			switch (cmbEditCategory.SelectedIndex)
			{
			case 0:
				AniHelper.FillProGroup(cmbEditWith);
				break;
			case 1:
				AniHelper.FillBrand(cmbEditWith);
				break;
			case 2:
				AniHelper.FillUnit(cmbEditWith);
				break;
			case 3:
				AniHelper.FillTCategory(cmbEditWith);
				break;
			case 4:
				AniHelper.FillGodown(cmbEditWith);
				break;
			case 5:
				AshproCommon.FillCombo(cmbEditWith, "Select SelfId as Id,SelfName as Name From Self");
				break;
			}
		}

		private string LoadUpdateSwitchStatement(string strUpdateDetails)
		{
			string result = string.Empty;
			switch (cmbEditCategory.SelectedIndex)
			{
			case 0:
				strUpdateData = "PGroupId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 1:
				strUpdateData = "BrandId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 2:
				strUpdateData = "UnitId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 3:
				strUpdateData = "TCategoryId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 4:
				strUpdateData = "GodwnId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 5:
				strUpdateData = "SelfId";
				result = cmbEditWith.SelectedValue.ToString();
				break;
			case 6:
				strUpdateData = "HSNNo";
				result = strUpdateDetails;
				break;
			case 7:
				strUpdateData = "PurchasePrice";
				result = strUpdateDetails;
				break;
			case 8:
				strUpdateData = "SellingPrice";
				result = strUpdateDetails;
				break;
			case 9:
				strUpdateData = "MRP";
				result = strUpdateDetails;
				break;
			case 10:
				strUpdateData = "PurchaseDis";
				result = strUpdateDetails;
				break;
			case 11:
				strUpdateData = "SaleDis";
				result = strUpdateDetails;
				break;
			case 12:
				strUpdateData = "OpeningStock";
				result = strUpdateDetails;
				break;
			case 13:
				strUpdateData = "Specification";
				result = strUpdateDetails;
				break;
			}
			return result;
		}

		private void ProductEditing_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			exProducts = new List<ExProduct>();
			base.ActiveControl = cmbSearchBy;
			cmbSearchBy.Focus();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			ReportShowMethod();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			foreach (ExProduct exProduct in exProducts)
			{
				if (cmbEditWith.Visible)
				{
					exProduct.Particulars = cmbEditWith.Text;
				}
				else
				{
					exProduct.Particulars = txtEditWith.Text;
				}
			}
			dgDetails.Refresh();
		}

		private void cmbEditCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbEditCategory.SelectedIndex != 0 && cmbEditCategory.SelectedIndex != 1 && cmbEditCategory.SelectedIndex != 2 && cmbEditCategory.SelectedIndex != 3 && cmbEditCategory.SelectedIndex != 4 && cmbEditCategory.SelectedIndex != 5)
			{
				cmbEditWith.Visible = false;
				txtEditWith.Visible = true;
				txtEditWith.Location = new Point(400, 67);
			}
			else
			{
				cmbEditWith.Visible = true;
				txtEditWith.Location = new Point(400, 98);
				txtEditWith.Visible = false;
			}
			label2.Visible = true;
			btnGenerate.Visible = true;
		}

		private void cmbEditWith_DropDown(object sender, EventArgs e)
		{
			SwitchStatement();
		}

		private void cmbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cmbSearchBy.SelectedIndex)
			{
			case 0:
				label3.Text = "Group:";
				AniHelper.FillProGroup(cmbSearchWith);
				label1.Visible = true;
				cmbSearchWith2.Visible = true;
				AniHelper.FillBrand(cmbSearchWith2);
				break;
			case 1:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Brand:";
				AniHelper.FillBrand(cmbSearchWith);
				break;
			case 2:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Unit:";
				AniHelper.FillUnit(cmbSearchWith);
				break;
			case 3:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Tax Category:";
				AniHelper.FillTCategory(cmbSearchWith);
				break;
			case 4:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Godown:";
				AniHelper.FillGodown(cmbSearchWith);
				break;
			case 5:
				label1.Visible = true;
				cmbSearchWith2.Visible = true;
				label3.Text = "Godown:";
				AniHelper.FillGodown(cmbSearchWith);
				label1.Text = "Self";
				break;
			case 6:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "HSN Code:";
				AshproCommon.FillCombo(cmbSearchWith, "Select ProductId as Id,HSNNo as Name From Product");
				break;
			case 7:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Pur Dis(%):";
				AshproCommon.FillCombo(cmbSearchWith, "Select ProductId as Id,PurchaseDis as Name From Product");
				break;
			case 8:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Sales Dis(%):";
				AshproCommon.FillCombo(cmbSearchWith, "Select ProductId as Id,SaleDis as Name From Product");
				break;
			case 9:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Specification:";
				AshproCommon.FillCombo(cmbSearchWith, "Select ProductId as Id,Specification as Name From Product");
				break;
			case 10:
				label1.Visible = false;
				cmbSearchWith2.Visible = false;
				label3.Text = "Pro Name:";
				AshproCommon.FillCombo(cmbSearchWith, "Select ProductId as Id,ProductName as Name From Product");
				break;
			}
		}

		private void cmbSearchWith_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (label3.Text == "Godown")
			{
				AshproCommon.FillCombo(cmbSearchWith2, "Select SelfId as Id,SelfName as Name From Self  where GodownId='" + cmbSearchWith.SelectedValue.ToInt32() + "'");
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			foreach (ExProduct exProduct in exProducts)
			{
				empty = LoadUpdateSwitchStatement(exProduct.Particulars);
				AshproCommon.DatabaseMethod("Update Product set " + strUpdateData + " ='" + empty + "' Where ProductId ='" + exProduct.ProductId + "'");
			}
			Messages.UpdateMessage();
			ClearMethod();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductEditing));
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSearchWith = new AshUsercontrol.AshComboBox();
            this.cmbSearchBy = new AshUsercontrol.AshComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEditCategory = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEditWith = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEditWith = new AshUsercontrol.AshTextBoxNormal();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbSearchWith2 = new AshUsercontrol.AshComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.lblHead = new System.Windows.Forms.Label();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
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
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.dgDetails);
            this.panel1.Name = "panel1";
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersVisible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SL";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ProCode";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ProName";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Particulars";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbSearchWith
            // 
            resources.ApplyResources(this.cmbSearchWith, "cmbSearchWith");
            this.cmbSearchWith.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSearchWith.FormattingEnabled = true;
            this.cmbSearchWith.LeaveColor = System.Drawing.Color.White;
            this.cmbSearchWith.Name = "cmbSearchWith";
            this.cmbSearchWith.SelectedIndexChanged += new System.EventHandler(this.cmbSearchWith_SelectedIndexChanged);
            // 
            // cmbSearchBy
            // 
            resources.ApplyResources(this.cmbSearchBy, "cmbSearchBy");
            this.cmbSearchBy.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Items.AddRange(new object[] {
            resources.GetString("cmbSearchBy.Items"),
            resources.GetString("cmbSearchBy.Items1"),
            resources.GetString("cmbSearchBy.Items2"),
            resources.GetString("cmbSearchBy.Items3"),
            resources.GetString("cmbSearchBy.Items4"),
            resources.GetString("cmbSearchBy.Items5"),
            resources.GetString("cmbSearchBy.Items6"),
            resources.GetString("cmbSearchBy.Items7"),
            resources.GetString("cmbSearchBy.Items8"),
            resources.GetString("cmbSearchBy.Items9"),
            resources.GetString("cmbSearchBy.Items10")});
            this.cmbSearchBy.LeaveColor = System.Drawing.Color.White;
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.SelectedIndexChanged += new System.EventHandler(this.cmbSearchBy_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmbEditCategory
            // 
            resources.ApplyResources(this.cmbEditCategory, "cmbEditCategory");
            this.cmbEditCategory.EnterColor = System.Drawing.Color.LightPink;
            this.cmbEditCategory.FormattingEnabled = true;
            this.cmbEditCategory.Items.AddRange(new object[] {
            resources.GetString("cmbEditCategory.Items"),
            resources.GetString("cmbEditCategory.Items1"),
            resources.GetString("cmbEditCategory.Items2"),
            resources.GetString("cmbEditCategory.Items3"),
            resources.GetString("cmbEditCategory.Items4"),
            resources.GetString("cmbEditCategory.Items5"),
            resources.GetString("cmbEditCategory.Items6"),
            resources.GetString("cmbEditCategory.Items7"),
            resources.GetString("cmbEditCategory.Items8"),
            resources.GetString("cmbEditCategory.Items9"),
            resources.GetString("cmbEditCategory.Items10"),
            resources.GetString("cmbEditCategory.Items11"),
            resources.GetString("cmbEditCategory.Items12"),
            resources.GetString("cmbEditCategory.Items13")});
            this.cmbEditCategory.LeaveColor = System.Drawing.Color.White;
            this.cmbEditCategory.Name = "cmbEditCategory";
            this.cmbEditCategory.SelectedIndexChanged += new System.EventHandler(this.cmbEditCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbEditWith
            // 
            resources.ApplyResources(this.cmbEditWith, "cmbEditWith");
            this.cmbEditWith.EnterColor = System.Drawing.Color.LightPink;
            this.cmbEditWith.FormattingEnabled = true;
            this.cmbEditWith.LeaveColor = System.Drawing.Color.White;
            this.cmbEditWith.Name = "cmbEditWith";
            this.cmbEditWith.DropDown += new System.EventHandler(this.cmbEditWith_DropDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtEditWith
            // 
            resources.ApplyResources(this.txtEditWith, "txtEditWith");
            this.txtEditWith.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEditWith.Data = 0;
            this.txtEditWith.EnterColor = System.Drawing.Color.LightPink;
            this.txtEditWith.Format = null;
            this.txtEditWith.isAllowNegative = false;
            this.txtEditWith.isAllowSpecialChar = false;
            this.txtEditWith.isNumberOnly = false;
            this.txtEditWith.isNumeric = false;
            this.txtEditWith.LeaveColor = System.Drawing.Color.White;
            this.txtEditWith.Name = "txtEditWith";
            this.txtEditWith.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnGenerate
            // 
            resources.ApplyResources(this.btnGenerate, "btnGenerate");
            this.btnGenerate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbSearchWith2
            // 
            resources.ApplyResources(this.cmbSearchWith2, "cmbSearchWith2");
            this.cmbSearchWith2.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSearchWith2.FormattingEnabled = true;
            this.cmbSearchWith2.LeaveColor = System.Drawing.Color.White;
            this.cmbSearchWith2.Name = "cmbSearchWith2";
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // ProductEditing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.txtEditWith);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSearchWith2);
            this.Controls.Add(this.cmbSearchWith);
            this.Controls.Add(this.cmbEditWith);
            this.Controls.Add(this.cmbEditCategory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.Name = "ProductEditing";
            this.Load += new System.EventHandler(this.ProductEditing_Load);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbSearchBy, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbEditCategory, 0);
            this.Controls.SetChildIndex(this.cmbEditWith, 0);
            this.Controls.SetChildIndex(this.cmbSearchWith, 0);
            this.Controls.SetChildIndex(this.cmbSearchWith2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnLoad, 0);
            this.Controls.SetChildIndex(this.btnGenerate, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.txtEditWith, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
