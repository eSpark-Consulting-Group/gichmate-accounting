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
	public class BankReconciliation : BaseForm
	{
		private DBContext db;

		private List<BankReconciliationEntity> bankReconciliations;

		private IContainer components = null;

		private Label lblHead;

		private Label label1;

		private AshComboBox cmbBankAccount;

		private Label label2;

		private AshDateTimePicker dtmDate;

		private Button btnLoad;

		private Button btnUpdate;

		private AshDataGridview dgvDetails;

		private BindingSource bdsDetails;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn col_InvNo;
        private DataGridViewTextBoxColumn col_InvType;
        private DataGridViewTextBoxColumn col_Particulars;
        private DataGridViewTextBoxColumn col_InvDate;
        private DataGridViewDateTimeColumn col_BankDate;
        private DataGridViewTextBoxColumn col_Debit;
        private DataGridViewTextBoxColumn col_Credit;
        private DataGridViewTextBoxColumn col_Remarks;
        private ToolTip toolTip;

		public BankReconciliation()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
        }

		private void btnLoad_Click(object sender, EventArgs e)
		{
			List<TransactionLog> list = new List<TransactionLog>();
			list = (from x in db.TransactionLogs()
				where (x.Status == "Main" && x.VchrName == "Bank Payment") || (x.Status == "Sub" && x.VchrName == "Bank Receipt" && x.VchrDate <= dtmDate.Value.Date)
				select x).ToList();
			bankReconciliations = AniHelper.CopyListData<BankReconciliationEntity>(list.Cast<object>().ToList());
			int num = 1;
			foreach (BankReconciliationEntity item in bankReconciliations)
			{
				item.SL = num;
				item.BankDate = item.VchrDate;
				item.Particulars = db.Accounts().Single((AccountEntity x) => x.AccountId == item.OppAccountId).AccountName;
				num++;
			}
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = bankReconciliations;
			dgvDetails.DataSource = bdsDetails;
		}

		private void BankReconciliation_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			bankReconciliations = new List<BankReconciliationEntity>();
			AniHelper.FillBankAccount(cmbBankAccount);
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if (bankReconciliations.Count <= 0)
			{
				return;
			}
			foreach (BankReconciliationEntity item in bankReconciliations)
			{
				VchrTranLog vchrTranLog = db.VchrTransaction().Single((VchrTranLog x) => x.VchrTranId == item.VchrId && x.VchrType == item.VchrType && x.VchrName == item.VchrName);
				vchrTranLog.VchrDate = item.BankDate;
				int iValue = vchrTranLog.VchrTranId.toInt32();
				AshproCommon.UpdateToDatabaseObj(vchrTranLog, "VchrTranLog", "VchrTranId", iValue);
				foreach (TransactionLog item2 in (from x in db.TransactionLogs()
					where x.VchrId == item.VchrId && x.VchrType == item.VchrType && x.VchrName == item.VchrName
					select x).ToList())
				{
					item2.VchrDate = item.BankDate;
					iValue = item2.TranId.toInt32();
					item2.TranId = null;
					AshproCommon.UpdateToDatabaseObj(item2, "TransactionLog", "TranId", iValue);
				}
			}
			Messages.UpdateMessage();
		}

		private void BankReconciliation_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.U && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnUpdate_Click(null, null);
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnLoad_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankReconciliation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHead = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBankAccount = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvDetails = new AshUsercontrol.AshDataGridview();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BankDate = new AshUsercontrol.DataGridViewDateTimeColumn();
            this.col_Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            this.toolTip.SetToolTip(this.lblHead, resources.GetString("lblHead.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // cmbBankAccount
            // 
            resources.ApplyResources(this.cmbBankAccount, "cmbBankAccount");
            this.cmbBankAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.toolTip.SetToolTip(this.cmbBankAccount, resources.GetString("cmbBankAccount.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmDate.Name = "dtmDate";
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Name = "btnLoad";
            this.toolTip.SetToolTip(this.btnLoad, resources.GetString("btnLoad.ToolTip"));
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvDetails
            // 
            resources.ApplyResources(this.dgvDetails, "dgvDetails");
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.col_InvNo,
            this.col_InvType,
            this.col_Particulars,
            this.col_InvDate,
            this.col_BankDate,
            this.col_Debit,
            this.col_Credit,
            this.col_Remarks});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetails.EnableHeadersVisualStyles = false;
            this.dgvDetails.EnterKeyNavigation = false;
            this.dgvDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowHeadersVisible = false;
            this.toolTip.SetToolTip(this.dgvDetails, resources.GetString("dgvDetails.ToolTip"));
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // col_InvNo
            // 
            this.col_InvNo.DataPropertyName = "VchrNo";
            resources.ApplyResources(this.col_InvNo, "col_InvNo");
            this.col_InvNo.Name = "col_InvNo";
            this.col_InvNo.ReadOnly = true;
            // 
            // col_InvType
            // 
            this.col_InvType.DataPropertyName = "VchrType";
            resources.ApplyResources(this.col_InvType, "col_InvType");
            this.col_InvType.Name = "col_InvType";
            this.col_InvType.ReadOnly = true;
            // 
            // col_Particulars
            // 
            this.col_Particulars.DataPropertyName = "Particulars";
            resources.ApplyResources(this.col_Particulars, "col_Particulars");
            this.col_Particulars.Name = "col_Particulars";
            this.col_Particulars.ReadOnly = true;
            // 
            // col_InvDate
            // 
            this.col_InvDate.DataPropertyName = "VchrDate";
            resources.ApplyResources(this.col_InvDate, "col_InvDate");
            this.col_InvDate.Name = "col_InvDate";
            this.col_InvDate.ReadOnly = true;
            // 
            // col_BankDate
            // 
            this.col_BankDate.DataPropertyName = "BankDate";
            resources.ApplyResources(this.col_BankDate, "col_BankDate");
            this.col_BankDate.Name = "col_BankDate";
            this.col_BankDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_BankDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // col_Debit
            // 
            this.col_Debit.DataPropertyName = "DebitAmount";
            resources.ApplyResources(this.col_Debit, "col_Debit");
            this.col_Debit.Name = "col_Debit";
            this.col_Debit.ReadOnly = true;
            // 
            // col_Credit
            // 
            this.col_Credit.DataPropertyName = "CreditAmount";
            resources.ApplyResources(this.col_Credit, "col_Credit");
            this.col_Credit.Name = "col_Credit";
            this.col_Credit.ReadOnly = true;
            // 
            // col_Remarks
            // 
            this.col_Remarks.DataPropertyName = "Narration";
            resources.ApplyResources(this.col_Remarks, "col_Remarks");
            this.col_Remarks.Name = "col_Remarks";
            this.col_Remarks.ReadOnly = true;
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Name = "btnUpdate";
            this.toolTip.SetToolTip(this.btnUpdate, resources.GetString("btnUpdate.ToolTip"));
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // BankReconciliation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.cmbBankAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHead);
            this.Name = "BankReconciliation";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.BankReconciliation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BankReconciliation_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbBankAccount, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.btnLoad, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.dgvDetails, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
