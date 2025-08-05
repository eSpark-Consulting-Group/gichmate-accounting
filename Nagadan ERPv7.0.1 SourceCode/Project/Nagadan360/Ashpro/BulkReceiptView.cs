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
using Ashpro.Properties;

namespace Ashpro
{
	public class BulkReceiptView : BaseForm
	{
		private DBContext db = new DBContext();

		private List<SalesMaster> entSalesMasters;

		private List<ExSalesMaster> exSalesMasters;

		private List<AccountEntity> entCashAccounts;

		private List<AccountEntity> entCardAccounts;

		private List<AccountEntity> entOnlineAccount;

		private IContainer components = null;

		private Label lblHead;

		private Label label4;

		private AshDateTimePicker dtmDate;

		private AshComboBox cmbRoute;

		private Label label1;

		private AshComboBox cmbVehicle;

		private Label label5;

		private AshComboBox cmbSlsExi;

		private Label label11;

		private AshTextBoxNormal txtRemarks;

		private Label label12;

		private AshDateTimePicker dtmFrom;

		private Label label2;

		private AshDateTimePicker dtmTo;

		private Label label3;

		private Button btnLoad;

		private AshDataGridview dgDetails;

		private Button btnSave;

		private Button btnClear;

		private AshTextBoxNormal txtCash;

		private Label label6;

		private Label label7;

		private AshTextBoxNormal txtCard;

		private Label label8;

		private AshTextBoxNormal txtOnline;

		private Label label9;

		private AshTextBoxNormal txtCredit;

		private BindingSource bdsDetails;

		private Label label10;

		private AshTextBoxNormal txtTotal;
        private DataGridViewTextBoxColumn col_SL;
        private DataGridViewTextBoxColumn Col_Date;
        private DataGridViewTextBoxColumn col_InvoiceNo;
        private DataGridViewTextBoxColumn col_Party;
        private DataGridViewTextBoxColumn col_InvoiceAmount;
        private DataGridViewTextBoxColumn col_CashPayment;
        private DataGridViewComboBoxColumn col_CashAccount;
        private DataGridViewTextBoxColumn col_CardPayment;
        private DataGridViewComboBoxColumn col_CardAccount;
        private DataGridViewTextBoxColumn col_OnlinePayment;
        private DataGridViewComboBoxColumn col_OnlinePaymentAccount;
        private DataGridViewTextBoxColumn col_Credit;
        private ToolTip toolTip;

		public BulkReceiptView()
		{
            Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
            InitializeComponent();
            ColorChange.Color(this);
            TranslatorHelper.LoadArabic(this);
            TranslatorHelper.ChangeToArabic(this);
            Col_Date.DefaultCellStyle.Format = CommonSettings.strDateFormat;
			col_InvoiceAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CashPayment.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CardPayment.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_OnlinePayment.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Credit.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			txtCash.Format = CommonSettings.strCurencyFormat;
			txtCard.Format = CommonSettings.strCurencyFormat;
			txtCredit.Format = CommonSettings.strCurencyFormat;
			txtOnline.Format = CommonSettings.strCurencyFormat;
			txtTotal.Format = CommonSettings.strCurencyFormat;
			PopulateCombos();
		}

		private void TotalFindMethod()
		{
			txtCash.Value = exSalesMasters.Sum((ExSalesMaster x) => x.CashPayment);
			txtCard.Value = exSalesMasters.Sum((ExSalesMaster x) => x.CardPayment);
			txtOnline.Value = exSalesMasters.Sum((ExSalesMaster x) => x.OtherPayment);
			txtCredit.Value = exSalesMasters.Sum((ExSalesMaster x) => x.Credit);
			txtTotal.Value = exSalesMasters.Sum((ExSalesMaster x) => x.GrandDiscount);
		}

		private void PopulateCombos()
		{
			string sTable = "Select * From Account Join AccountGroup on Account.AGroupId = AccountGroup.AGroupId Where GroupName='CASH IN HAND'";
			entCashAccounts = AshproCommon.GetListMethod<AccountEntity>(sTable);
			sTable = "Select * From Account Join AccountGroup on Account.AGroupId = AccountGroup.AGroupId Where GroupName='CASH AT BANK'";
			entCardAccounts = AshproCommon.GetListMethod<AccountEntity>(sTable);
			sTable = "Select * From Account Join AccountGroup on Account.AGroupId = AccountGroup.AGroupId Where GroupName='CASH AT BANK'";
			entOnlineAccount = AshproCommon.GetListMethod<AccountEntity>(sTable);
			col_CashAccount.DataSource = null;
			col_CashAccount.DataSource = entCashAccounts;
			col_CashAccount.ValueMember = "AccountId";
			col_CashAccount.DisplayMember = "AccountName";
			col_CardAccount.DataSource = null;
			col_CardAccount.DataSource = entCardAccounts;
			col_CardAccount.ValueMember = "AccountId";
			col_CardAccount.DisplayMember = "AccountName";
			col_OnlinePaymentAccount.DataSource = null;
			col_OnlinePaymentAccount.DataSource = entOnlineAccount;
			col_OnlinePaymentAccount.ValueMember = "AccountId";
			col_OnlinePaymentAccount.DisplayMember = "AccountName";
		}

		private void DGVLoadMethod()
		{
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			dgDetails.DataSource = null;
			bdsDetails.DataSource = exSalesMasters;
			dgDetails.DataSource = bdsDetails;
		}

		private ExSalesMaster getCurrent()
		{
			try
			{
				if (dgDetails.CurrentRow != null)
				{
					return (ExSalesMaster)dgDetails.CurrentRow.DataBoundItem;
				}
				return null;
			}
			catch (IndexOutOfRangeException)
			{
				return null;
			}
		}

		private void BultReceiptView_Load(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmDate.Value = DateTime.Now.Date));
			AniHelper.FillRoute(cmbRoute);
			AniHelper.FillEmployees(cmbSlsExi);
			AniHelper.FillVehicle(cmbVehicle);
			entSalesMasters = new List<SalesMaster>();
			AniHelper.ClearComboMethod(this);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			if (cmbRoute.Text != string.Empty)
			{
				text = " And RouteId = '" + cmbRoute.SelectedValue.ToInt32() + "'";
			}
			if (cmbSlsExi.Text != string.Empty)
			{
				text2 = " And StafId = '" + cmbSlsExi.SelectedValue.ToInt32() + "'";
			}
			if (cmbVehicle.Text != string.Empty)
			{
				text3 = " And VehId= '" + cmbVehicle.SelectedValue.ToInt32() + "'";
			}
			string sTable = "Select * From SalesEstimateMaster Where FK_BranchId = '" + Settings.Default.iBranchId + "'" + text + text3 + text2 + " And SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' And '" + AniHelper.GetDate(dtmTo.Value) + "'";
			entSalesMasters = AshproCommon.GetListMethod<SalesMaster>(sTable);
			exSalesMasters = AniHelper.CopyListData<ExSalesMaster>(entSalesMasters.Cast<object>().ToList());
			exSalesMasters = exSalesMasters.Where((ExSalesMaster x) => x.Credit > 0m).ToList();
			int num = 1;
			foreach (ExSalesMaster exSalesMaster in exSalesMasters)
			{
				exSalesMaster.SL = num;
				exSalesMaster.PartyAccountName = db.Accounts(new
				{
					AccountId = exSalesMaster.PartyAccountId
				}).FirstOrDefault().AccountName;
				num++;
			}
			DGVLoadMethod();
			TotalFindMethod();
		}

		private void dgDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExSalesMaster current = getCurrent();
			if (dgDetails.CurrentCell.OwningColumn.Name == col_CashPayment.Name)
			{
				current.Credit = current.GrandDiscount - (current.CashPayment + current.CardPayment + current.OtherPayment);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_CardAccount.Name)
			{
				current.Credit = current.GrandDiscount - (current.CashPayment + current.CardPayment + current.OtherPayment);
			}
			else if (dgDetails.CurrentCell.OwningColumn.Name == col_OnlinePayment.Name)
			{
				current.Credit = current.GrandDiscount - (current.CashPayment + current.CardPayment + current.OtherPayment);
			}
			if (current.Credit == 0m)
			{
				current.IsSettiled = true;
			}
			else
			{
				current.IsSettiled = false;
			}
			dgDetails.Refresh();
			TotalFindMethod();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (exSalesMasters.Count <= 0)
			{
				return;
			}
			List<SalesMaster> list = AniHelper.CopyListData<SalesMaster>(exSalesMasters.Cast<object>().ToList());
			foreach (SalesMaster item in list)
			{
				item.Terminal = Utility.uTerminal;
				item.SequenceNo = Utility.uSerialNumber;
				AshproCommon.UpdateToDatabaseObj(item, "SalesEstimateMaster", "SalesInvId", item.SalesInvId.toInt32());
				if (item.CashPayment > 0m)
				{
					GetVchrTranLog(item, "Cash Receipt", item.CashPayment, item.CashAccountId);
				}
				if (item.CardPayment > 0m)
				{
					GetVchrTranLog(item, "Bank Receipt", item.CardPayment, item.CardAccountId);
				}
				if (item.OtherPayment > 0m)
				{
					GetVchrTranLog(item, "Bank Receipt", item.OtherPayment, item.OnlineAccountId);
				}
			}
			btnClear_Click(null, null);
		}

		private bool GetVchrTranLog(SalesMaster exSalesMaster, string sTitle, decimal dAmount, int? iAccountId)
		{
			try
			{
				VchrTranLog vchrTranLog = new VchrTranLog
				{
					VchrTranId = null,
					VchrNo = ((db.VchrTransaction(new
					{
						VchrType = "Receipt Voucher",
						VchrName = sTitle
					}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1),
					VchrName = sTitle,
					VchrType = "Receipt Voucher",
					VchrAmount = dAmount,
					VchrDate = dtmDate.Value,
					SalesManId = cmbSlsExi.SelectedValue.ToInt32(),
					FromAccountId = exSalesMaster.PartyAccountId,
					ToAccountId = iAccountId,
					TranDate = DateTime.Now,
					UserId = Settings.Default.intLogOnId,
					Narration = txtRemarks.Text,
					FK_BranchId = Settings.Default.iBranchId
				};
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog))
				{
					vchrTranLog = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.VchrName == vchrTranLog.VchrName
						select x).FirstOrDefault();
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
				return false;
			}
			return true;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			entSalesMasters = new List<SalesMaster>();
			exSalesMasters = new List<ExSalesMaster>();
			DGVLoadMethod();
		}

		private void BulkReceiptView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkReceiptView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHead = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.cmbRoute = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVehicle = new AshUsercontrol.AshComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSlsExi = new AshUsercontrol.AshComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label12 = new System.Windows.Forms.Label();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgDetails = new AshUsercontrol.AshDataGridview();
            this.col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Party = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvoiceAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CashPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CashAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_CardPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CardAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_OnlinePayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_OnlinePaymentAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtCash = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCard = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOnline = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCredit = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotal = new AshUsercontrol.AshTextBoxNormal();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnEnd, "btnEnd");
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            // 
            // cmbRoute
            // 
            resources.ApplyResources(this.cmbRoute, "cmbRoute");
            this.cmbRoute.EnterColor = System.Drawing.Color.LightPink;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.LeaveColor = System.Drawing.Color.White;
            this.cmbRoute.Name = "cmbRoute";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbVehicle
            // 
            resources.ApplyResources(this.cmbVehicle, "cmbVehicle");
            this.cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
            this.cmbVehicle.FormattingEnabled = true;
            this.cmbVehicle.LeaveColor = System.Drawing.Color.White;
            this.cmbVehicle.Name = "cmbVehicle";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cmbSlsExi
            // 
            resources.ApplyResources(this.cmbSlsExi, "cmbSlsExi");
            this.cmbSlsExi.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSlsExi.FormattingEnabled = true;
            this.cmbSlsExi.LeaveColor = System.Drawing.Color.White;
            this.cmbSlsExi.Name = "cmbSlsExi";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Data = 0;
            this.txtRemarks.EnterColor = System.Drawing.Color.LightPink;
            this.txtRemarks.Format = null;
            this.txtRemarks.isAllowNegative = false;
            this.txtRemarks.isAllowSpecialChar = false;
            this.txtRemarks.isNumberOnly = false;
            this.txtRemarks.isNumeric = false;
            this.txtRemarks.LeaveColor = System.Drawing.Color.White;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.TabStop = false;
            this.toolTip.SetToolTip(this.btnLoad, resources.GetString("btnLoad.ToolTip"));
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_SL,
            this.Col_Date,
            this.col_InvoiceNo,
            this.col_Party,
            this.col_InvoiceAmount,
            this.col_CashPayment,
            this.col_CashAccount,
            this.col_CardPayment,
            this.col_CardAccount,
            this.col_OnlinePayment,
            this.col_OnlinePaymentAccount,
            this.col_Credit});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.EnterKeyNavigation = false;
            this.dgDetails.LastKey = System.Windows.Forms.Keys.None;
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellEndEdit);
            this.dgDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgDetails_DataError);
            // 
            // col_SL
            // 
            this.col_SL.DataPropertyName = "SL";
            resources.ApplyResources(this.col_SL, "col_SL");
            this.col_SL.Name = "col_SL";
            this.col_SL.ReadOnly = true;
            // 
            // Col_Date
            // 
            this.Col_Date.DataPropertyName = "SalesDate";
            resources.ApplyResources(this.Col_Date, "Col_Date");
            this.Col_Date.Name = "Col_Date";
            // 
            // col_InvoiceNo
            // 
            this.col_InvoiceNo.DataPropertyName = "SalesInvNo";
            resources.ApplyResources(this.col_InvoiceNo, "col_InvoiceNo");
            this.col_InvoiceNo.Name = "col_InvoiceNo";
            this.col_InvoiceNo.ReadOnly = true;
            // 
            // col_Party
            // 
            this.col_Party.DataPropertyName = "PartyAccountName";
            resources.ApplyResources(this.col_Party, "col_Party");
            this.col_Party.Name = "col_Party";
            this.col_Party.ReadOnly = true;
            // 
            // col_InvoiceAmount
            // 
            this.col_InvoiceAmount.DataPropertyName = "GrandDiscount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_InvoiceAmount.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.col_InvoiceAmount, "col_InvoiceAmount");
            this.col_InvoiceAmount.Name = "col_InvoiceAmount";
            this.col_InvoiceAmount.ReadOnly = true;
            // 
            // col_CashPayment
            // 
            this.col_CashPayment.DataPropertyName = "CashPayment";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CashPayment.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.col_CashPayment, "col_CashPayment");
            this.col_CashPayment.Name = "col_CashPayment";
            // 
            // col_CashAccount
            // 
            this.col_CashAccount.DataPropertyName = "CashAccountId";
            this.col_CashAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.col_CashAccount, "col_CashAccount");
            this.col_CashAccount.Name = "col_CashAccount";
            // 
            // col_CardPayment
            // 
            this.col_CardPayment.DataPropertyName = "CardPayment";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_CardPayment.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_CardPayment, "col_CardPayment");
            this.col_CardPayment.Name = "col_CardPayment";
            // 
            // col_CardAccount
            // 
            this.col_CardAccount.DataPropertyName = "CardAccountId";
            this.col_CardAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.col_CardAccount, "col_CardAccount");
            this.col_CardAccount.Name = "col_CardAccount";
            // 
            // col_OnlinePayment
            // 
            this.col_OnlinePayment.DataPropertyName = "OtherPayment";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_OnlinePayment.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.col_OnlinePayment, "col_OnlinePayment");
            this.col_OnlinePayment.Name = "col_OnlinePayment";
            // 
            // col_OnlinePaymentAccount
            // 
            this.col_OnlinePaymentAccount.DataPropertyName = "OnlineAccountId";
            this.col_OnlinePaymentAccount.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.col_OnlinePaymentAccount, "col_OnlinePaymentAccount");
            this.col_OnlinePaymentAccount.Name = "col_OnlinePaymentAccount";
            // 
            // col_Credit
            // 
            this.col_Credit.DataPropertyName = "Credit";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Credit.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.col_Credit, "col_Credit");
            this.col_Credit.Name = "col_Credit";
            this.col_Credit.ReadOnly = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabStop = false;
            this.toolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.TabStop = false;
            this.toolTip.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtCash
            // 
            resources.ApplyResources(this.txtCash, "txtCash");
            this.txtCash.BackColor = System.Drawing.Color.White;
            this.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCash.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCash.Data = 0;
            this.txtCash.EnterColor = System.Drawing.Color.LightPink;
            this.txtCash.Format = null;
            this.txtCash.isAllowNegative = false;
            this.txtCash.isAllowSpecialChar = false;
            this.txtCash.isNumberOnly = false;
            this.txtCash.isNumeric = true;
            this.txtCash.LeaveColor = System.Drawing.Color.White;
            this.txtCash.Name = "txtCash";
            this.txtCash.ReadOnly = true;
            this.txtCash.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtCard
            // 
            resources.ApplyResources(this.txtCard, "txtCard");
            this.txtCard.BackColor = System.Drawing.Color.White;
            this.txtCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCard.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCard.Data = 0;
            this.txtCard.EnterColor = System.Drawing.Color.LightPink;
            this.txtCard.Format = null;
            this.txtCard.isAllowNegative = false;
            this.txtCard.isAllowSpecialChar = false;
            this.txtCard.isNumberOnly = false;
            this.txtCard.isNumeric = true;
            this.txtCard.LeaveColor = System.Drawing.Color.White;
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtOnline
            // 
            resources.ApplyResources(this.txtOnline, "txtOnline");
            this.txtOnline.BackColor = System.Drawing.Color.White;
            this.txtOnline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOnline.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOnline.Data = 0;
            this.txtOnline.EnterColor = System.Drawing.Color.LightPink;
            this.txtOnline.Format = null;
            this.txtOnline.isAllowNegative = false;
            this.txtOnline.isAllowSpecialChar = false;
            this.txtOnline.isNumberOnly = false;
            this.txtOnline.isNumeric = true;
            this.txtOnline.LeaveColor = System.Drawing.Color.White;
            this.txtOnline.Name = "txtOnline";
            this.txtOnline.ReadOnly = true;
            this.txtOnline.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtCredit
            // 
            resources.ApplyResources(this.txtCredit, "txtCredit");
            this.txtCredit.BackColor = System.Drawing.Color.White;
            this.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCredit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCredit.Data = 0;
            this.txtCredit.EnterColor = System.Drawing.Color.LightPink;
            this.txtCredit.Format = null;
            this.txtCredit.isAllowNegative = false;
            this.txtCredit.isAllowSpecialChar = false;
            this.txtCredit.isNumberOnly = false;
            this.txtCredit.isNumeric = true;
            this.txtCredit.LeaveColor = System.Drawing.Color.White;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotal.Data = 0;
            this.txtTotal.EnterColor = System.Drawing.Color.LightPink;
            this.txtTotal.Format = null;
            this.txtTotal.isAllowNegative = false;
            this.txtTotal.isAllowSpecialChar = false;
            this.txtTotal.isNumberOnly = false;
            this.txtTotal.isNumeric = true;
            this.txtTotal.LeaveColor = System.Drawing.Color.White;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // BulkReceiptView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCredit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtOnline);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbSlsExi);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbVehicle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbRoute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.lblHead);
            this.Name = "BulkReceiptView";
            this.Load += new System.EventHandler(this.BultReceiptView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BulkReceiptView_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbRoute, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbVehicle, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.cmbSlsExi, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.btnLoad, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtCash, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtOnline, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtCard, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtCredit, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
