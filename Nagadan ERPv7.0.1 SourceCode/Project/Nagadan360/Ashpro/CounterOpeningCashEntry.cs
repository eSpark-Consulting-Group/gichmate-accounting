using System;
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
	public class CounterOpeningCashEntry : BaseForm
	{
		private DBContext db = new DBContext();

		private string VoucherType = "Journal Voucher";

		private int iManinCashAccountId;

		private MasterValue masterValue;

		private IContainer components = null;

		private Label label8;

		private Label label1;

		private AshComboBox cmbCashAccount;

		private Label label2;

		private AshTextBoxNormal txtAmount;

		private Button btnOK;

		private Button btnSalesCounter;

		private NumKeyBoardLogin numKeyBoardLogin1;

		public CounterOpeningCashEntry()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo)
		{
			vchrTranLog.VchrDate = DateTime.Now.Date;
			vchrTranLog.VchrTranId = null;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = txtAmount.Value;
			vchrTranLog.VchrType = VoucherType;
			vchrTranLog.VchrName = VoucherType;
			vchrTranLog.SalesManId = Settings.Default.intLogOnId;
			vchrTranLog.FromAccountId = iManinCashAccountId;
			vchrTranLog.ToAccountId = cmbCashAccount.SelectedValue.ToInt32();
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = "OPening Cash Entry";
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
		}

		private void CounterOpeningCashEntry_Load(object sender, EventArgs e)
		{
			AniHelper.FillCashAccount(cmbCashAccount);
			SalesCounterEntity salesCounterEntity = db.SalesCounter(new
			{
				Remarks = Utility.uTerminal
			}).FirstOrDefault();
			if (salesCounterEntity != null)
			{
				cmbCashAccount.SelectedValue = (Utility.uCounterCashAccountId = salesCounterEntity.CashAccountId.toInt32());
				string text2 = (Utility.uCounter = (Settings.Default.CounterName = salesCounterEntity.SalesCounterName));
				Settings.Default.Save();
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				masterValue = db.MasterValues(new
				{
					MasterType = "CounterOppeningCash",
					Description = Utility.uTerminal
				}).FirstOrDefault();
				iManinCashAccountId = db.Accounts(new
				{
					AccountId = 1
				}).FirstOrDefault().AccountId.ToInt32();
				VchrTranLog vchrTranLog = new VchrTranLog();
				int iVchrNo = (db.VchrTransaction(new
				{
					VchrType = VoucherType,
					FK_BranchId = Settings.Default.iBranchId
				}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				EntityLoadMethod(vchrTranLog, iVchrNo);
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog))
				{
					int num = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
						select x).FirstOrDefault().VchrTranId.ToInt32();
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), num.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrType, AniHelper.GetDate(DateTime.Now));
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), num.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrType, AniHelper.GetDate(DateTime.Now));
					if (masterValue == null)
					{
						masterValue = new MasterValue();
						masterValue.MasterType = "CounterOppeningCash";
						masterValue.Description = Utility.uTerminal;
						masterValue.Name = txtAmount.Value.ToString(CommonSettings.strCurencyFormat);
						AshproCommon.InsertToDatabaseObj(masterValue);
					}
					else
					{
						masterValue.Name = txtAmount.Value.ToString(CommonSettings.strCurencyFormat);
						AshproCommon.UpdateToDatabaseObj(masterValue, "MasterValue", "Id", masterValue.Id.toInt32());
					}
				}
				Utility.uCounterCashAccountId = cmbCashAccount.SelectedValue.ToInt32();
				Settings.Default.IsCashClosed = false;
				Settings.Default.Save();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSalesCounter_Click(object sender, EventArgs e)
		{
			SalesCounter salesCounter = new SalesCounter();
			salesCounter.isFromOtherForm = true;
			if (salesCounter.ShowDialog() == DialogResult.OK)
			{
				AniHelper.FillCashAccount(cmbCashAccount);
				cmbCashAccount.SelectedValue = Utility.uCounterCashAccountId;
				txtAmount.Focus();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CounterOpeningCashEntry));
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCashAccount = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new AshUsercontrol.AshTextBoxNormal();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSalesCounter = new System.Windows.Forms.Button();
            this.numKeyBoardLogin1 = new AshUsercontrol.NumKeyBoardLogin();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbCashAccount
            // 
            resources.ApplyResources(this.cmbCashAccount, "cmbCashAccount");
            this.cmbCashAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCashAccount.FormattingEnabled = true;
            this.cmbCashAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbCashAccount.Name = "cmbCashAccount";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Data = 0;
            this.txtAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtAmount.Format = null;
            this.txtAmount.isAllowNegative = false;
            this.txtAmount.isAllowSpecialChar = false;
            this.txtAmount.isNumberOnly = false;
            this.txtAmount.isNumeric = true;
            this.txtAmount.LeaveColor = System.Drawing.Color.White;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSalesCounter
            // 
            resources.ApplyResources(this.btnSalesCounter, "btnSalesCounter");
            this.btnSalesCounter.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalesCounter.ForeColor = System.Drawing.Color.White;
            this.btnSalesCounter.Name = "btnSalesCounter";
            this.btnSalesCounter.TabStop = false;
            this.btnSalesCounter.UseVisualStyleBackColor = false;
            this.btnSalesCounter.Click += new System.EventHandler(this.btnSalesCounter_Click);
            // 
            // numKeyBoardLogin1
            // 
            resources.ApplyResources(this.numKeyBoardLogin1, "numKeyBoardLogin1");
            this.numKeyBoardLogin1.Name = "numKeyBoardLogin1";
            // 
            // CounterOpeningCashEntry
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numKeyBoardLogin1);
            this.Controls.Add(this.btnSalesCounter);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCashAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Name = "CounterOpeningCashEntry";
            this.Load += new System.EventHandler(this.CounterOpeningCashEntry_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbCashAccount, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtAmount, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnSalesCounter, 0);
            this.Controls.SetChildIndex(this.numKeyBoardLogin1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
