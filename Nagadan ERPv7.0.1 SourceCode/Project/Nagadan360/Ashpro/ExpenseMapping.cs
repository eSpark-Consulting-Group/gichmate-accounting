using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class ExpenseMapping : BaseForm
	{
		private DBContext db;

		private List<ExManufacturingExp> exManufacturingExps;

		private IContainer components = null;

		private Label label5;

		private Button btnCancel;

		private Button btnOk;

		public AshDataGridview dgvDetails;

		private AshTextBoxNormal txtTotalCost;

		private Panel panel1;

		private Label lblHead;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewComboBoxColumn col_ExpenseAccount;
        private DataGridViewComboBoxColumn col_PaymentAccount;
        private DataGridViewDateTimeColumn col_Date;
        private DataGridViewTextBoxColumn col_Amount;
        private BindingSource bdsDetails;

		public ExpenseMapping(List<ExManufacturingExp> manufacturingExps)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			exManufacturingExps = manufacturingExps;
		}

		private void BindDgv()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exManufacturingExps;
			dgvDetails.DataSource = bdsDetails;
		}

		private void ExpenseMapping_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
			db = new DBContext();
			col_ExpenseAccount.DataSource = null;
			List<AccountEntity> list = new List<AccountEntity>();
			list = AshproCommon.GetListMethod<AccountEntity>("Select * From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='DIRECT EXPENSES' or AccountGroup.GroupName='INDIRECT EXPENSES'");
			col_ExpenseAccount.DataSource = list;
			col_ExpenseAccount.DisplayMember = "AccountName";
			col_ExpenseAccount.ValueMember = "AccountId";
			col_PaymentAccount.DataSource = null;
			list = AshproCommon.GetListMethod<AccountEntity>("Select * From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CASH IN HAND' or AccountGroup.GroupName='CASH AT BANK'");
			col_PaymentAccount.DataSource = list;
			col_PaymentAccount.DisplayMember = "AccountName";
			col_PaymentAccount.ValueMember = "AccountId";
			BindDgv();
			if (exManufacturingExps != null)
			{
				txtTotalCost.Value = exManufacturingExps.Sum((ExManufacturingExp x) => x.Amount).GetValueOrDefault();
			}
			base.ActiveControl = dgvDetails;
			dgvDetails.CurrentCell = dgvDetails.Rows[0].Cells[1];
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				ExManufacturingExp exManufacturingExp = (ExManufacturingExp)dgvDetails.CurrentRow.DataBoundItem;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ExpenseAccount.Name)
				{
					exManufacturingExp.SL = dgvDetails.Rows.Count - 1;
					exManufacturingExp.Date = DateTime.Now.Date;
				}
				else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Amount.Name)
				{
					txtTotalCost.Value = exManufacturingExps.Sum((ExManufacturingExp x) => x.Amount).GetValueOrDefault();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
		}

		private void ExpenseMapping_FormClosing(object sender, FormClosingEventArgs e)
		{
			BindDgv();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseMapping));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgvDetails = new AshUsercontrol.AshDataGridview();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ExpenseAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_PaymentAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_Date = new AshUsercontrol.DataGridViewDateTimeColumn();
            this.col_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalCost = new AshUsercontrol.AshTextBoxNormal();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHead = new System.Windows.Forms.Label();
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgvDetails
            // 
            resources.ApplyResources(this.dgvDetails, "dgvDetails");
            this.dgvDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_ExpenseAccount,
            this.col_PaymentAccount,
            this.col_Date,
            this.col_Amount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.EnterKeyNavigation = false;
            this.dgvDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetails_CellEndEdit);
            this.dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetails_DataError);
            this.dgvDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDetails_EditingControlShowing);
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_ExpenseAccount
            // 
            this.col_ExpenseAccount.DataPropertyName = "FK_ExpenseAccountId";
            this.col_ExpenseAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.col_ExpenseAccount, "col_ExpenseAccount");
            this.col_ExpenseAccount.Name = "col_ExpenseAccount";
            this.col_ExpenseAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_ExpenseAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // col_PaymentAccount
            // 
            this.col_PaymentAccount.DataPropertyName = "FK_PaymentAccountId";
            this.col_PaymentAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.col_PaymentAccount, "col_PaymentAccount");
            this.col_PaymentAccount.Name = "col_PaymentAccount";
            this.col_PaymentAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_PaymentAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // col_Date
            // 
            this.col_Date.DataPropertyName = "Date";
            resources.ApplyResources(this.col_Date, "col_Date");
            this.col_Date.Name = "col_Date";
            // 
            // col_Amount
            // 
            this.col_Amount.DataPropertyName = "Amount";
            resources.ApplyResources(this.col_Amount, "col_Amount");
            this.col_Amount.Name = "col_Amount";
            // 
            // txtTotalCost
            // 
            resources.ApplyResources(this.txtTotalCost, "txtTotalCost");
            this.txtTotalCost.BackColor = System.Drawing.Color.White;
            this.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalCost.Data = 0;
            this.txtTotalCost.EnterColor = System.Drawing.Color.LightPink;
            this.txtTotalCost.Format = null;
            this.txtTotalCost.isAllowNegative = false;
            this.txtTotalCost.isAllowSpecialChar = false;
            this.txtTotalCost.isNumberOnly = false;
            this.txtTotalCost.isNumeric = true;
            this.txtTotalCost.LeaveColor = System.Drawing.Color.White;
            this.txtTotalCost.Name = "txtTotalCost";
            this.txtTotalCost.ReadOnly = true;
            this.txtTotalCost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.dgvDetails);
            this.panel1.Name = "panel1";
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // ExpenseMapping
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtTotalCost);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHead);
            this.Name = "ExpenseMapping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExpenseMapping_FormClosing);
            this.Load += new System.EventHandler(this.ExpenseMapping_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.txtTotalCost, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
