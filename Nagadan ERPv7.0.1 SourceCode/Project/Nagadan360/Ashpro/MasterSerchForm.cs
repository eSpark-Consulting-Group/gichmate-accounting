using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class MasterSerchForm : BaseForm
	{
		private List<Masters> entMasters = new List<Masters>();

		private bool isFromTransForm = false;

		private IContainer components = null;

		private Label lblTitle;

		private AshTextBoxNormal txtSearch;

		private Label lblSearch;

		private Button btnCancel;

		private Button btnOK;
        private DataGridViewTextBoxColumn col_Id;
        private DataGridViewTextBoxColumn col_Name;
        private DataGridViewTextBoxColumn col_Description;
        private DataGridView dgSearch;

		public int id { get; set; }

		public List<Masters> Masters { get; set; }

		public ProductGroup proGroup { get; set; }

		public Account account { get; set; }

		public Godown godown { get; set; }

		public Product product { get; set; }

		public BranchView branch { get; set; }

		public CurrencyView currency { get; set; }

		public CustomerPrevilegeCard previlegeCard { get; set; }

		public VehicleView vehicle { get; set; }

		public EmployeeDetails employee { get; set; }

		public MasterSerchForm(string sIdName, string sName, string sDescription, bool _isFromTransForm = false)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			col_Id.HeaderText = sIdName;
			col_Name.HeaderText = sName;
			col_Description.HeaderText = sDescription;
			btnEnd.Visible = false;
			isFromTransForm = _isFromTransForm;
		}

		private void SearchImpliment()
		{
			if (proGroup != null)
			{
				proGroup.DataLoadFromSearch(this);
			}
			if (account != null)
			{
				account.ReloadMethod(this);
			}
			if (godown != null)
			{
				godown.DataLoadFromSearch(this);
			}
			if (product != null)
			{
				product.DataLoadFromSearch(this);
			}
			if (branch != null)
			{
				branch.ReloadMethod(this);
			}
			if (currency != null)
			{
				currency.DataloadFromSearch(this);
			}
			if (previlegeCard != null)
			{
				previlegeCard.DataloadFromSearch(this);
			}
			if (vehicle != null)
			{
				vehicle.ReloadMethod(this);
			}
			if (employee != null)
			{
				employee.DataFromSearch(this);
			}
		}

		private void MasterSerchForm_Load(object sender, EventArgs e)
		{
			dgSearch.DataSource = (entMasters = Masters);
			base.ActiveControl = txtSearch;
		}

		private async void txtSearch_TextChanged(object sender, EventArgs e)
		{
			if (txtSearch.Text != string.Empty)
			{
				string sText = txtSearch.Text.ToUpper();
				if (!sText.isNumeric() || !isFromTransForm)
				{
					entMasters = await Task.Run(() => Masters.Where((Masters x) => x.name.ToUpper().Contains(sText)).ToList());
				}
				else
				{
					entMasters = await Task.Run(() => Masters.Where((Masters x) => x.description.ToUpper().Contains(sText)).ToList());
				}
			}
			else
			{
				entMasters = Masters;
			}
			dgSearch.DataSource = entMasters;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (entMasters.Count > 0)
			{
				id = entMasters.FirstOrDefault().id;
				if (!isFromTransForm)
				{
					SearchImpliment();
				}
				else
				{
					base.DialogResult = DialogResult.OK;
				}
				Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void dgSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			Masters masters = (Masters)dgSearch.CurrentRow.DataBoundItem;
			id = masters.id;
			if (!isFromTransForm)
			{
				SearchImpliment();
				return;
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && entMasters.Count > 0)
			{
				id = entMasters.FirstOrDefault().id;
				if (!isFromTransForm)
				{
					SearchImpliment();
				}
				else
				{
					base.DialogResult = DialogResult.OK;
				}
				Close();
			}
		}

		private void dgSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			Masters masters = (Masters)dgSearch.CurrentRow.DataBoundItem;
			id = masters.id;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void dgSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && isFromTransForm)
			{
				Masters masters = (Masters)dgSearch.CurrentRow.DataBoundItem;
				id = masters.id;
				base.DialogResult = DialogResult.OK;
				Close();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterSerchForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtSearch = new AshUsercontrol.AshTextBoxNormal();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgSearch = new System.Windows.Forms.DataGridView();
            this.col_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).BeginInit();
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
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Data = 0;
            this.txtSearch.EnterColor = System.Drawing.Color.LightPink;
            this.txtSearch.Format = null;
            this.txtSearch.isAllowNegative = false;
            this.txtSearch.isAllowSpecialChar = false;
            this.txtSearch.isNumberOnly = false;
            this.txtSearch.isNumeric = false;
            this.txtSearch.LeaveColor = System.Drawing.Color.White;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.Name = "lblSearch";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabStop = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.TabStop = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgSearch
            // 
            resources.ApplyResources(this.dgSearch, "dgSearch");
            this.dgSearch.AllowUserToAddRows = false;
            this.dgSearch.AllowUserToDeleteRows = false;
            this.dgSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgSearch.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Id,
            this.col_Name,
            this.col_Description});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSearch.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgSearch.EnableHeadersVisualStyles = false;
            this.dgSearch.Name = "dgSearch";
            this.dgSearch.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgSearch.RowHeadersVisible = false;
            this.dgSearch.RowTemplate.Height = 25;
            this.dgSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellContentClick);
            this.dgSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellContentDoubleClick);
            this.dgSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgSearch_KeyDown);
            // 
            // col_Id
            // 
            this.col_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Id.DataPropertyName = "id";
            resources.ApplyResources(this.col_Id, "col_Id");
            this.col_Id.Name = "col_Id";
            this.col_Id.ReadOnly = true;
            // 
            // col_Name
            // 
            this.col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Name.DataPropertyName = "name";
            resources.ApplyResources(this.col_Name, "col_Name");
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            // 
            // col_Description
            // 
            this.col_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Description.DataPropertyName = "description";
            resources.ApplyResources(this.col_Description, "col_Description");
            this.col_Description.Name = "col_Description";
            this.col_Description.ReadOnly = true;
            // 
            // MasterSerchForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblTitle);
            this.Name = "MasterSerchForm";
            this.Load += new System.EventHandler(this.MasterSerchForm_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.lblSearch, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            this.Controls.SetChildIndex(this.dgSearch, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
