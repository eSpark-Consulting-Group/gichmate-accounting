using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class POSFormSearch : BaseForm
	{
		private List<SalesMaster> entsalesMasters;

		private List<ExSalesMaster> exSalesMasters;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private AshDateTimePicker dtmFrom;

		private AshDateTimePicker dtmTo;

		private Label label2;

		private Label lblSearch;

		private AshTextBoxNormal txtInvoice;

		private DataGridView dgSearch;

		private Button btnOK;

		private Button btnCancel;

		private NumKeyBoardPOS nkPOS;

		private CheckBox cbDate;
        private DataGridViewTextBoxColumn col_InvoiceNo;
        private DataGridViewTextBoxColumn col_InvDate;
        private DataGridViewTextBoxColumn col_Party;
        private DataGridViewTextBoxColumn col_BillAmount;
        private Button btnDeleteAll;

		public string FormType { get; set; }

		public string searchType { get; set; }

		public SalesMaster SalesMaster { get; set; }

		public int iHoldCount { get; set; }

		public bool isShowDeleteAll { get; set; } = false;


		public POSFormSearch()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			col_InvDate.DefaultCellStyle.Format = CommonSettings.strDateFormat;
			col_BillAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void DeleteHoldedBill(int iMasterId)
		{
			try
			{
				if (AshproCommon.DeleteMethod("Delete From POSHoldSettings where SalesInvId = '" + iMasterId.ToInt32() + "'"))
				{
					AshproCommon.DeleteMethod("Delete From POSHoldMaster Where Id = '" + iMasterId.ToInt32() + "'");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DGVLoadMethod(bool isDatewise = true)
		{
			try
			{
				entsalesMasters = new List<SalesMaster>();
				string sTable = null;
				if (searchType == "BILL")
				{
					if (isDatewise)
					{
						sTable = "Select * From SalesEstimateMaster Where InvType='" + FormType + "' and SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' ";
					}
					else if (txtInvoice.Text != string.Empty)
					{
						sTable = ((!cbDate.Checked) ? ("Select * From SalesEstimateMaster Where InvType='" + FormType + "' and  SalesInvNo like '%" + txtInvoice.Text + "' ") : ("Select * From SalesEstimateMaster Where InvType='" + FormType + "' and SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' and SalesInvNo like '%" + txtInvoice.Text + "' "));
					}
				}
				else if (!(searchType == "KOT"))
				{
					sTable = ((!isDatewise) ? ("Select * From POSHoldMaster Where  Terminal='" + Utility.uTerminal + "' and SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' and SalesInvNo like '%" + txtInvoice.Text + "' ") : ("Select * From POSHoldMaster Where Terminal='" + Utility.uTerminal + "' and SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' "));
				}
				else if (isShowDeleteAll)
				{
					sTable = "Select TockenNo as SalesInvNo,DateAndTime as SalesDate,FK_CustomerId as PartyAccountId,TotalAmount as GrandDiscount,KOTMasterId as SalesInvId From KOTOrderMaster Where Remarks = 'KOT'";
				}
				else if (isDatewise)
				{
					sTable = "Select TockenNo as SalesInvNo,DateAndTime as SalesDate,FK_CustomerId as PartyAccountId,TotalAmount as GrandDiscount,KOTMasterId as SalesInvId From KOTOrderMaster Where Remarks = 'KOT' and DateAndTime between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' ";
				}
				else if (txtInvoice.Text != string.Empty)
				{
					sTable = "Select TockenNo as SalesInvNo,DateAndTime as SalesDate,FK_CustomerId as PartyAccountId,TotalAmount as GrandDiscount,KOTMasterId as SalesInvId From KOTOrderMaster Where Remarks = 'KOT' and  DateAndTime between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' and TockenNo like '%" + txtInvoice.Text + "' ";
				}
				entsalesMasters = AshproCommon.GetListMethod<SalesMaster>(sTable);
				exSalesMasters = AniHelper.CopyListData<ExSalesMaster>(entsalesMasters.Cast<object>().ToList());
				foreach (ExSalesMaster exSalesMaster in exSalesMasters)
				{
					exSalesMaster.PartyAccountName = AshproCommon.IdFindMethod("Select AccountName From Account Where AccountId='" + exSalesMaster.PartyAccountId + "'");
				}
				dgSearch.AutoGenerateColumns = false;
				dgSearch.DataSource = exSalesMasters;
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void POSFormSearch_Load(object sender, EventArgs e)
		{
			foreach (Control control in nkPOS.Controls)
			{
				if (control is Button)
				{
					control.BackColor = Settings.Default.OtherB;
					control.ForeColor = Settings.Default.OtherF;
				}
			}
			dtmFrom.Value = DateTime.Now.Date;
			if (searchType == "Retrieve" || isShowDeleteAll)
			{
				btnDeleteAll.Visible = true;
			}
			if (searchType == "KOT")
			{
				cbDate.Visible = false;
			}
			DGVLoadMethod();
		}

		private void dgSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				ExSalesMaster source = (ExSalesMaster)dgSearch.CurrentRow.DataBoundItem;
				SalesMaster = source.CopyTo<SalesMaster>();
			}
		}

		private void dgSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				ExSalesMaster source = (ExSalesMaster)dgSearch.CurrentRow.DataBoundItem;
				SalesMaster = source.CopyTo<SalesMaster>();
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void dtmTo_ValueChanged(object sender, EventArgs e)
		{
			DGVLoadMethod();
		}

		private void txtInvoice_TextChanged(object sender, EventArgs e)
		{
			if (txtInvoice.Text == string.Empty)
			{
				DGVLoadMethod();
			}
			else
			{
				DGVLoadMethod(isDatewise: false);
			}
		}

		private void dgSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && dgSearch.Rows.Count > 0)
			{
				string sCntrInvNo = dgSearch.CurrentRow.Cells[0].Value.ToString();
				SalesMaster = entsalesMasters.Where((SalesMaster x) => x.CntrInvNo == sCntrInvNo).FirstOrDefault();
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void KOTdeletemethod(int iKotMasterId)
		{
			if (AshproCommon.DeleteFromDatabase("KOTOrderSettings", "FK_KOTMasterId", iKotMasterId))
			{
				AshproCommon.DeleteFromDatabase("KOTOrderMaster", "KOTMasterId", iKotMasterId);
			}
		}

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			if (searchType == "KOT")
			{
				User user = db.Users().FirstOrDefault((User x) => x.UserName.ToUpper() == "ADMIN");
				string text = AniHelper.InputBoxMethod("Admin Password", "Please enter Admin Password.", "Password :", isPassword: true);
				if (text != user.PassWord)
				{
					Messages.WarningMessage("Wrong Admin Password!");
					return;
				}
				foreach (KOTOrderMaster item in db.KOTOrderMasters(new
				{
					Remarks = "KOT"
				}).ToList())
				{
					KOTdeletemethod(item.KOTMasterId.toInt32());
				}
			}
			else
			{
				foreach (SalesMaster item2 in db.POSHoldMasters(new
				{
					Terminal = Utility.uTerminal
				}).ToList())
				{
					int iMasterId = item2.SalesInvId.toInt32();
					DeleteHoldedBill(iMasterId);
				}
				dgSearch.DataSource = null;
				iHoldCount = 0;
			}
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
		{
			if (entsalesMasters.Count > 0 && e.KeyCode == Keys.Return)
			{
				SalesMaster = entsalesMasters.FirstOrDefault();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSFormSearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtInvoice = new AshUsercontrol.AshTextBoxNormal();
            this.dgSearch = new System.Windows.Forms.DataGridView();
            this.col_InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Party = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nkPOS = new AshUsercontrol.NumKeyBoardPOS();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.btnDeleteAll = new System.Windows.Forms.Button();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.ValueChanged += new System.EventHandler(this.dtmTo_ValueChanged);
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.ValueChanged += new System.EventHandler(this.dtmTo_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.Name = "lblSearch";
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
            this.txtInvoice.isNumberOnly = false;
            this.txtInvoice.isNumeric = false;
            this.txtInvoice.LeaveColor = System.Drawing.Color.White;
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtInvoice.TextChanged += new System.EventHandler(this.txtInvoice_TextChanged);
            this.txtInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoice_KeyDown);
            // 
            // dgSearch
            // 
            resources.ApplyResources(this.dgSearch, "dgSearch");
            this.dgSearch.AllowUserToAddRows = false;
            this.dgSearch.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgSearch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSearch.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_InvoiceNo,
            this.col_InvDate,
            this.col_Party,
            this.col_BillAmount});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSearch.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgSearch.EnableHeadersVisualStyles = false;
            this.dgSearch.Name = "dgSearch";
            this.dgSearch.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgSearch.RowHeadersVisible = false;
            this.dgSearch.RowTemplate.Height = 25;
            this.dgSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellContentClick);
            this.dgSearch.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSearch_CellDoubleClick);
            this.dgSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgSearch_KeyDown);
            // 
            // col_InvoiceNo
            // 
            this.col_InvoiceNo.DataPropertyName = "SalesInvNo";
            resources.ApplyResources(this.col_InvoiceNo, "col_InvoiceNo");
            this.col_InvoiceNo.Name = "col_InvoiceNo";
            this.col_InvoiceNo.ReadOnly = true;
            // 
            // col_InvDate
            // 
            this.col_InvDate.DataPropertyName = "SalesDate";
            resources.ApplyResources(this.col_InvDate, "col_InvDate");
            this.col_InvDate.Name = "col_InvDate";
            this.col_InvDate.ReadOnly = true;
            this.col_InvDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // col_Party
            // 
            this.col_Party.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_Party.DataPropertyName = "PartyAccountName";
            resources.ApplyResources(this.col_Party, "col_Party");
            this.col_Party.Name = "col_Party";
            this.col_Party.ReadOnly = true;
            // 
            // col_BillAmount
            // 
            this.col_BillAmount.DataPropertyName = "GrandDiscount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_BillAmount.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.col_BillAmount, "col_BillAmount");
            this.col_BillAmount.Name = "col_BillAmount";
            this.col_BillAmount.ReadOnly = true;
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
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // nkPOS
            // 
            resources.ApplyResources(this.nkPOS, "nkPOS");
            this.nkPOS.Name = "nkPOS";
            // 
            // cbDate
            // 
            resources.ApplyResources(this.cbDate, "cbDate");
            this.cbDate.Name = "cbDate";
            this.cbDate.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAll
            // 
            resources.ApplyResources(this.btnDeleteAll, "btnDeleteAll");
            this.btnDeleteAll.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDeleteAll.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // POSFormSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.nkPOS);
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
            this.Name = "POSFormSearch";
            this.Load += new System.EventHandler(this.POSFormSearch_Load);
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
            this.Controls.SetChildIndex(this.nkPOS, 0);
            this.Controls.SetChildIndex(this.cbDate, 0);
            this.Controls.SetChildIndex(this.btnDeleteAll, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
