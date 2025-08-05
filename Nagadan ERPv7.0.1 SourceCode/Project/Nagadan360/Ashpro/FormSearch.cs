using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class FormSearch : BaseForm
	{
		private List<SerchEntity> serchEntities = new List<SerchEntity>();

		private SerchEntity SerchEntity = new SerchEntity();

		private int iFYearId;

		private DBContext db;

		private IContainer components = null;

		private Button btnCancel;

		private Button btnOK;

		private DataGridView dgSearch;

		private AshTextBoxNormal txtInvoice;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label lblSearch;

		private Label label2;

		private Label label1;
        private DataGridViewTextBoxColumn col_InvoiceNo;
        private DataGridViewTextBoxColumn col_InvDate;
        private DataGridViewTextBoxColumn col_Party;
        private DataGridViewTextBoxColumn col_BillAmount;
        private Label lblTitle;

		public int iInvoiceId { get; set; }

		public string sFormType { get; set; }

		public string sInvType { get; set; }

		public FormSearch()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void SerchMethod(bool isDateBased)
		{
			string text = string.Empty;
			if (CommonSettings.blnEnableMultiBranch)
			{
				text = " and FK_BranchId = '" + Settings.Default.iBranchId + "' ";
			}
			if (sInvType == "Sales Order")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "'" + text + " and InvDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'");
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and VchrNo like'%" + txtInvoice.Data + "%'");
				}
			}
			else if (sInvType == "Purchase Order")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "'" + text + " and InvDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'");
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and VchrNo like'%" + txtInvoice.Data + "%'");
				}
			}
			else if (sInvType == "Sales Invoice")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesInvId as Id, SalesInvNo as InvoiceNo, SalesDate as InvDate,PartyAccountId as PartyAccntId, Account.AccountName as PartyName, GrandDiscount as InvAmount From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'");
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesInvId as Id, SalesInvNo as InvoiceNo, SalesDate as InvDate,PartyAccountId as PartyAccntId, Account.AccountName as PartyName, GrandDiscount as InvAmount From SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and FYearID='" + iFYearId + "' and SalesInvNo like'%" + txtInvoice.Data + "%'");
				}
			}
			else if (sInvType == "Purchase Invoice")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select PurInvId as Id, RefInvNo as InvoiceNo, PurDate as InvDate,PartyAccId as PartyAccntId, Account.AccountName as PartyName, TotalAmnt as InvAmount From PurInvMaster Join Account on PurInvMaster.PartyAccId=Account.AccountId Where PurDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'" + text);
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select PurInvId as Id, RefInvNo as InvoiceNo, PurDate as InvDate,PartyAccId as PartyAccntId, Account.AccountName as PartyName, TotalAmnt as InvAmount From PurInvMaster Join Account on PurInvMaster.PartyAccId=Account.AccountId Where   RefInvNo like'%" + txtInvoice.Data + "%'" + text);
				}
			}
			else if (sInvType == "Sales Return")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SlsRtrnId as Id, SlRtnInvNo as InvoiceNo, SlsRtnDate as InvDate,PartyAccountId as PartyAccntId, Account.AccountName as PartyName, TotalAmount as InvAmount From SalesRtnMaster Join Account on SalesRtnMaster.PartyAccountId=Account.AccountId Where  SlsRtnDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'" + text);
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SlsRtrnId as Id, SlRtnInvNo as InvoiceNo, SlsRtnDate as InvDate,PartyAccountId as PartyAccntId, Account.AccountName as PartyName, TotalAmount as InvAmount From SalesRtnMaster Join Account on SalesRtnMaster.PartyAccountId=Account.AccountId Where FYearID='" + iFYearId + "' " + text + " and SlRtnInvNo like'%" + txtInvoice.Data + "%'");
				}
			}
			else if (sInvType == "Purchase Return")
			{
				if (isDateBased)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select PurRtnInvId as Id, PurRtnInvNo as InvoiceNo, PurRtnDate as InvDate,PartyAcId as PartyAccntId, Account.AccountName as PartyName, TotalAmnt as InvAmount From PurRtnInvMaster Join Account on PurRtnInvMaster.PartyAcId=Account.AccountId Where PurRtnDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' " + text);
				}
				else if (txtInvoice.Text != string.Empty)
				{
					serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select PurRtnInvId as Id, PurRtnInvNo as InvoiceNo, PurRtnDate as InvDate,PartyAcId as PartyAccntId, Account.AccountName as PartyName, TotalAmnt as InvAmount From PurRtnInvMaster Join Account on PurRtnInvMaster.PartyAcId=Account.AccountId Where  PurRtnInvNo like'%" + txtInvoice.Data + "%' " + text);
				}
			}
			else if (isDateBased)
			{
				serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and InvDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'");
			}
			else if (txtInvoice.Text != string.Empty)
			{
				serchEntities = AshproCommon.GetListMethod<SerchEntity>("Select SalesOrderMasterId as Id, VchrNo as InvoiceNo, InvDate as InvDate,CustomerId as PartyAccntId, Account.AccountName as PartyName, GrandTotal as InvAmount From SalesOrderMaster Join Account on SalesOrderMaster.CustomerId=Account.AccountId Where InvType='" + sFormType + "' " + text + " and VchrNo like'%" + txtInvoice.Data + "%'");
			}
			dgSearch.AutoGenerateColumns = false;
			dgSearch.DataSource = serchEntities;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (dgSearch.Rows.Count > 0)
			{
				SerchEntity = (SerchEntity)dgSearch.CurrentRow.DataBoundItem;
				iInvoiceId = SerchEntity.Id;
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void FormSearch_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			iFYearId = db.SelectedFYears().FirstOrDefault().FyearId.toInt32();
			if (sInvType == "Purchase Return")
			{
				col_InvoiceNo.HeaderText = "Reference No";
			}
			col_InvDate.DefaultCellStyle.Format = "dd-MMM-yyyy";
			SerchMethod(isDateBased: true);
		}

		private void dgSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				SerchEntity = (SerchEntity)dgSearch.CurrentRow.DataBoundItem;
				iInvoiceId = SerchEntity.Id;
			}
		}

		private void dtmFrom_ValueChanged(object sender, EventArgs e)
		{
			SerchMethod(isDateBased: true);
		}

		private void txtInvoice_TextChanged(object sender, EventArgs e)
		{
			try
			{
				SerchMethod(isDateBased: false);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dgSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				SerchEntity = (SerchEntity)dgSearch.CurrentRow.DataBoundItem;
				iInvoiceId = SerchEntity.Id;
				btnOK_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgSearch = new System.Windows.Forms.DataGridView();
            this.col_InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Party = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtInvoice = new AshUsercontrol.AshTextBoxNormal();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
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
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgSearch
            // 
            resources.ApplyResources(this.dgSearch, "dgSearch");
            this.dgSearch.AllowUserToAddRows = false;
            this.dgSearch.AllowUserToDeleteRows = false;
            this.dgSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.col_InvoiceNo,
            this.col_InvDate,
            this.col_Party,
            this.col_BillAmount});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSearch.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgSearch.EnableHeadersVisualStyles = false;
            this.dgSearch.Name = "dgSearch";
            this.dgSearch.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSearch.RowHeadersVisible = false;
            this.dgSearch.RowTemplate.Height = 25;
            this.dgSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellContentClick);
            this.dgSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellContentDoubleClick);
            // 
            // col_InvoiceNo
            // 
            this.col_InvoiceNo.DataPropertyName = "InvoiceNo";
            resources.ApplyResources(this.col_InvoiceNo, "col_InvoiceNo");
            this.col_InvoiceNo.Name = "col_InvoiceNo";
            this.col_InvoiceNo.ReadOnly = true;
            // 
            // col_InvDate
            // 
            this.col_InvDate.DataPropertyName = "InvDate";
            resources.ApplyResources(this.col_InvDate, "col_InvDate");
            this.col_InvDate.Name = "col_InvDate";
            this.col_InvDate.ReadOnly = true;
            // 
            // col_Party
            // 
            this.col_Party.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Party.DataPropertyName = "PartyName";
            resources.ApplyResources(this.col_Party, "col_Party");
            this.col_Party.Name = "col_Party";
            this.col_Party.ReadOnly = true;
            // 
            // col_BillAmount
            // 
            this.col_BillAmount.DataPropertyName = "InvAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_BillAmount.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.col_BillAmount, "col_BillAmount");
            this.col_BillAmount.Name = "col_BillAmount";
            this.col_BillAmount.ReadOnly = true;
            // 
            // txtInvoice
            // 
            resources.ApplyResources(this.txtInvoice, "txtInvoice");
            this.txtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvoice.Data = 0;
            this.txtInvoice.EnterColor = System.Drawing.Color.LightPink;
            this.txtInvoice.Format = null;
            this.txtInvoice.isAllowNegative = false;
            this.txtInvoice.isAllowSpecialChar = false;
            this.txtInvoice.isNumberOnly = true;
            this.txtInvoice.isNumeric = false;
            this.txtInvoice.LeaveColor = System.Drawing.Color.White;
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtInvoice.TextChanged += new System.EventHandler(this.txtInvoice_TextChanged);
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.ValueChanged += new System.EventHandler(this.dtmFrom_ValueChanged);
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.ValueChanged += new System.EventHandler(this.dtmFrom_ValueChanged);
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.Name = "lblSearch";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // FormSearch
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgSearch);
            this.Controls.Add(this.txtInvoice);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Name = "FormSearch";
            this.Load += new System.EventHandler(this.FormSearch_Load);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblSearch, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.txtInvoice, 0);
            this.Controls.SetChildIndex(this.dgSearch, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
