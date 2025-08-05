using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SelectCustomer : BaseForm
	{
		private DataTable dataTable = new DataTable();

		private string sType;

		private IContainer components = null;

		private PictureBox pcSearch;

		private AshTextBoxNormal txtSearch;

		private DataGridView dgDetail;

		private Button btnOK;

		private Button btnClear;

		private Button btnClose;

		private DataGridViewTextBoxColumn colslno;

		private DataGridViewTextBoxColumn col_Code;

		private DataGridViewTextBoxColumn col_Name;

		public int iId { get; set; }

		public string sName { get; set; }

		public int ProductId { get; set; }

		public SelectCustomer(string _sType)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			sType = _sType;
			btnEnd.Visible = false;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			dataTable = new DataTable();
			dgDetail.DataSource = null;
			if (sType == "Customer")
			{
				int num = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName='CUSTOMER/SUNDRY DEBTORS'", isVoucherNo: false);
				dataTable = AshproCommon.GetDataTable("Select AccountId as Id, AccountName as Name From Account Where AGroupId= '" + num + "' and AccountName like'%" + txtSearch.Text + "%'");
			}
			else if (sType == "SAccount")
			{
				int num2 = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName='SALES ACCOUNT'", isVoucherNo: false);
				dataTable = AshproCommon.GetDataTable("Select AccountId as Id, AccountName as Name From Account Where AGroupId= '" + num2 + "' and AccountName like'%" + txtSearch.Text + "%'");
			}
			else if (sType == "Salesman")
			{
				dataTable = AshproCommon.GetDataTable("Select EmployeeID as Id, EmpName as Name From EmployeeDetails Where EmpName like '%" + txtSearch.Text + "%'");
			}
			else if (sType == "Depot")
			{
				dataTable = AshproCommon.GetDataTable("Select GodwnId as Id, GodownName as Name From Godown Where GodownName like '%" + txtSearch.Text + "%'");
			}
			else if (sType == "SType")
			{
				if (!CommonSettings.blnGSTEnabled)
				{
					dataTable = AshproCommon.GetDataTable("Select Id, Description as Name From MasterValue Where MasterType='FormType' and  Description like'%" + txtSearch.Text + "%'");
				}
				else
				{
					dataTable = AshproCommon.GetDataTable("Select Id, Description as Name From MasterValue Where MasterType='GSTFormTypes' and  Description like'%" + txtSearch.Text + "%'");
				}
			}
			else if (sType == "PCategory")
			{
				dataTable = AshproCommon.GetDataTable("Select PCategoryId as Id, PCategoryName as Name From PriceCategory Where PCategoryName like'%" + txtSearch.Text + "%'");
			}
			else if (sType == "Unit")
			{
				dataTable = AshproCommon.GetDataTable("Select Unit.UnitId as Id, Unit.UnitName as Name From Unit Join ProductUnit on ProductUnit.UnitId=Unit.UnitId Where ProductUnit.ProductId='" + ProductId + "' and UnitName like '%" + txtSearch.Text + "%'");
			}
			else if (sType == "UserPrevilege")
			{
				dataTable = AshproCommon.GetDataTable("Select Id, Name From UserPrevilegeHdr Where Name like '%" + txtSearch.Text + "%'");
			}
			else if (sType == "Raw_Material")
			{
				dataTable = AshproCommon.GetDataTable("Select ProductCode as Id, ProductName as Name From Product where ProductType='Raw Metrial' and ProductName like '%" + txtSearch.Text + "%'");
			}
			else if (sType == "Finished_Good")
			{
				dataTable = AshproCommon.GetDataTable("Select ProductId as Id, ProductName as Name From Product where ProductType='Finished Good' and ProductName like '%" + txtSearch.Text + "%'");
			}
			else
			{
				dataTable = AshproCommon.GetDataTable("Select ProductId as Id, ProductName as Name From Product Where  ProductName like'%" + txtSearch.Text + "%'");
			}
			dgDetail.AutoGenerateColumns = false;
			dgDetail.DataSource = dataTable;
		}

		private void SelectCustomer_Load(object sender, EventArgs e)
		{
			if (sName != string.Empty)
			{
				txtSearch.Text = sName;
				txtSearch.SelectionStart = txtSearch.Text.Length;
			}
			if (sType == "Customer")
			{
				int num = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName='CUSTOMER/SUNDRY DEBTORS'", isVoucherNo: false);
				int num2 = AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName='CASH IN HAND'", isVoucherNo: false);
				dataTable = AshproCommon.GetDataTable("Select AccountId as Id, AccountName as Nmae From Account Where AGroupId in (" + num + "," + num2 + ")");
			}
			else if (sType == "SAccount")
			{
				dataTable = AshproCommon.GetDataTable("Select AccountId as Id, AccountName as Name From Account Where AGroupId ='" + AshproCommon.IdFindMethod("Select AGroupId From AccountGroup Where GroupName='SALES ACCOUNT'", isVoucherNo: false) + "'");
			}
			else if (sType == "Salesman")
			{
				dataTable = AshproCommon.GetDataTable("Select EmployeeID as Id, EmpName as Name From EmployeeDetails");
			}
			else if (sType == "Depot")
			{
				dataTable = AshproCommon.GetDataTable("Select GodwnId Id, GodownName as Name From Godown");
			}
			else if (sType == "PCategory")
			{
				dataTable = AshproCommon.GetDataTable("Select PCategoryId as Id, PCategoryName as Name From PriceCategory");
			}
			else if (sType == "SType")
			{
				if (!CommonSettings.blnGSTEnabled)
				{
					dataTable = AshproCommon.GetDataTable("Select Id, Description as Name From MasterValue Where MasterType='FormType'");
				}
				else
				{
					dataTable = AshproCommon.GetDataTable("Select Id, Description as Nmae From MasterValue Where MasterType='GSTFormTypes'");
				}
			}
			else if (sType == "Unit")
			{
				dataTable = AshproCommon.GetDataTable("Select Unit.UnitId as Id, Unit.UnitName as Name From Unit Join ProductUnit on ProductUnit.UnitId=Unit.UnitId Where ProductUnit.ProductId='" + ProductId + "'");
			}
			else if (sType == "UserPrevilege")
			{
				dataTable = AshproCommon.GetDataTable("Select Id as Id, Name From UserPrevilegeHdr");
			}
			else if (sType == "Raw_Material")
			{
				dataTable = AshproCommon.GetDataTable("Select ProductId as Id, ProductName as Name From Product where ProductType='Raw Metrial'");
			}
			else if (sType == "Finished_Good")
			{
				dataTable = AshproCommon.GetDataTable("Select ProductId as Id, ProductName as Name From Product where ProductType='Finished Good'");
			}
			else
			{
				col_Code.Visible = true;
				dataTable = AshproCommon.GetDataTable("Select ProductId as Id, ProductName as Name From Product");
			}
			dgDetail.DataSource = null;
			dgDetail.AutoGenerateColumns = false;
			dgDetail.DataSource = dataTable;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtSearch.Text = string.Empty;
			SelectCustomer_Load(null, null);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void dgDetail_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgDetail.Rows.Count > 0)
				{
					iId = dgDetail.CurrentRow.Cells[1].Value.ToInt32();
				}
				sName = dgDetail.CurrentRow.Cells[2].Value.ToString();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void dgDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				iId = dgDetail.CurrentRow.Cells[1].Value.ToInt32();
				sName = dgDetail.CurrentRow.Cells[2].Value.ToString();
			}
		}

		private void dgDetail_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			try
			{
				if (dgDetail.Rows.Count > 0)
				{
					iId = dgDetail.CurrentRow.Cells[1].Value.ToInt32();
				}
				sName = dgDetail.CurrentRow.Cells[2].Value.ToString();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception)
			{
				throw;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SelectCustomer));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			pcSearch = new System.Windows.Forms.PictureBox();
			txtSearch = new AshUsercontrol.AshTextBoxNormal();
			dgDetail = new System.Windows.Forms.DataGridView();
			colslno = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btnOK = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pcSearch).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetail).BeginInit();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(pcSearch, "pcSearch");
			pcSearch.Name = "pcSearch";
			pcSearch.TabStop = false;
			resources.ApplyResources(txtSearch, "txtSearch");
			txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSearch.Data = 0;
			txtSearch.EnterColor = System.Drawing.Color.LightPink;
			txtSearch.Format = null;
			txtSearch.isAllowNegative = false;
			txtSearch.isAllowSpecialChar = false;
			txtSearch.isNumberOnly = false;
			txtSearch.isNumeric = false;
			txtSearch.LeaveColor = System.Drawing.Color.White;
			txtSearch.Name = "txtSearch";
			txtSearch.Value = new decimal(new int[4]);
			txtSearch.TextChanged += new System.EventHandler(txtSearch_TextChanged);
			resources.ApplyResources(dgDetail, "dgDetail");
			dgDetail.AllowUserToAddRows = false;
			dgDetail.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.Gray;
			dgDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgDetail.BackgroundColor = System.Drawing.Color.White;
			dgDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgDetail.Columns.AddRange(colslno, col_Code, col_Name);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgDetail.DefaultCellStyle = dataGridViewCellStyle3;
			dgDetail.EnableHeadersVisualStyles = false;
			dgDetail.Name = "dgDetail";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			dgDetail.RowHeadersVisible = false;
			dgDetail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgDetail.RowTemplate.Height = 35;
			dgDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetail_CellContentClick);
			dgDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(dgDetail_KeyDown);
			dgDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(dgDetail_MouseDoubleClick);
			colslno.FillWeight = 22.84264f;
			resources.ApplyResources(colslno, "colslno");
			colslno.Name = "colslno";
			colslno.ReadOnly = true;
			col_Code.DataPropertyName = "Id";
			col_Code.FillWeight = 70.34451f;
			resources.ApplyResources(col_Code, "col_Code");
			col_Code.Name = "col_Code";
			col_Code.ReadOnly = true;
			col_Name.DataPropertyName = "Name";
			col_Name.FillWeight = 206.8129f;
			resources.ApplyResources(col_Name, "col_Name");
			col_Name.Name = "col_Name";
			col_Name.ReadOnly = true;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Name = "btnOK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Name = "btnClear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			resources.ApplyResources(btnClose, "btnClose");
			btnClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Name = "btnClose";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.CancelButton = btnClose;
			base.Controls.Add(btnClose);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnOK);
			base.Controls.Add(dgDetail);
			base.Controls.Add(txtSearch);
			base.Controls.Add(pcSearch);
			base.Name = "SelectCustomer";
			base.Load += new System.EventHandler(SelectCustomer_Load);
			base.Controls.SetChildIndex(pcSearch, 0);
			base.Controls.SetChildIndex(txtSearch, 0);
			base.Controls.SetChildIndex(dgDetail, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnClose, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			((System.ComponentModel.ISupportInitialize)pcSearch).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetail).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
