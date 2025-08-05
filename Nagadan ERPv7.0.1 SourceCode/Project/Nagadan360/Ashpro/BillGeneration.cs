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
	public class BillGeneration : BaseForm
	{
		private DBContext db = new DBContext();

		private string sCon;

		private MasterValue InvMasterValue;

		private List<SalesDetails> entSalesDetails = new List<SalesDetails>();

		private SalesMaster salesMaster;

		private int? iNormalCashAccount;

		private IContainer components = null;

		private Label label9;

		private Label label10;

		private Label label7;

		private Label label2;

		private Label label3;

		private Label label5;

		private Label label6;

		private CheckBox cbBillGeneration;

		private CheckBox cbRmvDisc;

		private CheckBox cbRndRate;

		private CheckBox cbMergeBill;

		private System.Windows.Forms.ProgressBar progressBar;

		private BackgroundWorker backgroundWorker;

		private CheckBox cbReadyCash;

		private Label label8;

		private AshDateTimePicker ashDateTimePicker1;

		private AshTextBoxNormal txtFrom;

		private AshTextBoxNormal txtQtyPercentage;

		private AshDateTimePicker dtmTo;

		private AshTextBoxNormal txtTo;

		private AshTextBoxNormal txtRtPtg;

		private AshTextBoxNormal txtPassword;

		private AshButton btnTransfer;

		public BillGeneration()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		public static void Savemethod(ExSalesMaster master, List<Transaction> transactions, string sCon, decimal dQtyP, decimal dRateP, bool blRemoveDisc, bool blRoundRate)
		{
			DBContext dBContext = new DBContext();
			AccountEntity accountEntity = dBContext.Accounts(new
			{
				AccountName = master.PartyAccountName
			}, sCon).FirstOrDefault();
			if (accountEntity == null)
			{
				AccountEntity accountEntity2 = dBContext.Accounts(new
				{
					AccountName = master.PartyAccountName
				}).FirstOrDefault();
				accountEntity = accountEntity2;
				if (AshproCommon.InsertToDatabaseObj(accountEntity, "Äccount", sCon))
				{
					master.PartyAccountId = dBContext.Accounts(new
					{
						AccountName = master.PartyAccountName
					}, sCon).FirstOrDefault().AccountId;
				}
			}
			else
			{
				master.PartyAccountId = accountEntity.AccountId;
			}
			List<SalesDetails> list = new List<SalesDetails>();
			foreach (Transaction transaction in transactions)
			{
				ProductEntity productEntity = dBContext.Products(new { transaction.ProductName }, null, sCon).FirstOrDefault();
				if (productEntity == null)
				{
					ProductEntity productEntity2 = dBContext.Products(new { transaction.ProductId }).FirstOrDefault();
					productEntity = productEntity2;
					if (AshproORM.InsertToDatabaseObj(productEntity, "Product", sCon))
					{
						transaction.ProductId = dBContext.Products(new { productEntity.ProductName }, null, sCon).FirstOrDefault().ProductId;
					}
				}
				else
				{
					transaction.ProductId = productEntity.ProductId;
				}
				UnitEntity unitEntity = dBContext.Units(new
				{
					UnitName = transaction.Unit
				}, sCon).FirstOrDefault();
				if (unitEntity == null)
				{
					UnitEntity unitEntity2 = dBContext.Units(new { transaction.UnitId }).FirstOrDefault();
					unitEntity = unitEntity2;
					if (AshproORM.InsertToDatabaseObj(unitEntity, "Unit", sCon))
					{
						transaction.UnitId = dBContext.Units(new
						{
							UnitName = transaction.Unit
						}, sCon).FirstOrDefault().UnitId;
					}
				}
				else
				{
					transaction.UnitId = unitEntity.UnitId;
				}
				decimal num3;
				if (blRemoveDisc)
				{
					num3 = (transaction.Discount = (transaction.DiscountAmount = default(decimal)));
				}
				if (dQtyP != 0m)
				{
					transaction.Quantity = transaction.Quantity * dQtyP / 100m;
				}
				if (dRateP != 0m)
				{
					transaction.Rate = transaction.Rate * dRateP / 100m;
				}
				if (blRoundRate)
				{
					transaction.Rate = Math.Round(transaction.Rate, 0);
				}
				transaction.GrossAmount = transaction.Quantity * transaction.Rate;
				if (transaction.Discount != 0m)
				{
					transaction.DiscountAmount = transaction.GrossAmount * transaction.Discount / 100m;
				}
				num3 = (transaction.Taxable = (transaction.NETAmount = transaction.GrossAmount - transaction.DiscountAmount));
				POSHelper.GetTransactionsAfterEdit(transaction, isEstimate: false, sCon);
			}
			master.CGSTAmount = transactions.Sum((Transaction x) => x.CGstAmount);
			master.SGSTAmount = transactions.Sum((Transaction x) => x.SGstAmount);
			master.IGSTAmount = transactions.Sum((Transaction x) => x.IGstAmount);
			master.TaxableAmount = transactions.Sum((Transaction x) => x.Taxable);
			master.TotalDiscount = transactions.Sum((Transaction x) => x.DiscountAmount);
			master.GrandDiscount = transactions.Sum((Transaction x) => x.NETAmount);
			SalesMaster salesMaster = new SalesMaster();
			salesMaster = master.CopyTo<SalesMaster>();
			list = POSHelper.GetSDetailsFrmTransaction(transactions, master.PartyAccountId.ToInt32(), master.SalesDate);
			int iGodwonId = dBContext.Godowns(sCon).FirstOrDefault().GodwnId.ToInt32();
			POSHelper.SaveSalesInvoice(salesMaster, list, iGodwonId, "Sales Voucher", sCon);
		}

		private void BillGeneration_Load(object sender, EventArgs e)
		{
			sCon = db.Companies().FirstOrDefault().LogoPath;
			if (sCon == string.Empty)
			{
				Messages.ErrorMessage("Path not set");
			}
			InvMasterValue = db.MasterValues(new
			{
				MasterType = "LastInvNoExported"
			}).FirstOrDefault();
			if (InvMasterValue != null)
			{
				txtFrom.Data = InvMasterValue.Description.ToInt32() + 1;
			}
			txtTo.Data = db.SalesMasters().Max((SalesMaster x) => x.SalesInvNo) ?? 1;
			iNormalCashAccount = CommonSettings.strDefaultCashAccountId.ToInt32();
		}

		private void btnTransfer_Click(object sender, EventArgs e)
		{
			bool flag = true;
			List<ExSalesMaster> list = new List<ExSalesMaster>();
			List<Transaction> list2 = new List<Transaction>();
			for (int i = txtFrom.Data; i <= txtTo.Data; i++)
			{
				salesMaster = db.SalesMasters(new
				{
					SalesInvNo = i,
					FK_BranchId = Settings.Default.iBranchId
				}).FirstOrDefault();
				flag = ((!cbReadyCash.Checked || salesMaster.PartyAccountId == iNormalCashAccount) ? true : false);
				entSalesDetails = db.SalesDetails(new { salesMaster.SalesInvId }).ToList();
				ExSalesMaster exSalesMaster = salesMaster.CopyTo<ExSalesMaster>();
				exSalesMaster.PartyAccountName = db.Accounts(new
				{
					AccountId = exSalesMaster.PartyAccountId
				}).FirstOrDefault().AccountName;
				List<Transaction> transactionsFromSDetails = POSHelper.GetTransactionsFromSDetails(entSalesDetails, db);
				if (flag)
				{
					if (!cbMergeBill.Checked)
					{
						Savemethod(exSalesMaster, transactionsFromSDetails, sCon, txtQtyPercentage.Value, txtRtPtg.Value, cbRmvDisc.Checked, cbRndRate.Checked);
						continue;
					}
					list.Add(exSalesMaster);
					list2.AddRange(transactionsFromSDetails);
				}
			}
			if (cbMergeBill.Checked && list.Count > 0 && list2.Count > 0)
			{
				ExSalesMaster master = list.FirstOrDefault();
				Savemethod(master, list2, sCon, txtQtyPercentage.Value, txtRtPtg.Value, cbRmvDisc.Checked, cbRndRate.Checked);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillGeneration));
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbBillGeneration = new System.Windows.Forms.CheckBox();
            this.cbRmvDisc = new System.Windows.Forms.CheckBox();
            this.cbRndRate = new System.Windows.Forms.CheckBox();
            this.cbMergeBill = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.cbReadyCash = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ashDateTimePicker1 = new AshUsercontrol.AshDateTimePicker();
            this.txtFrom = new AshUsercontrol.AshTextBoxNormal();
            this.txtQtyPercentage = new AshUsercontrol.AshTextBoxNormal();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.txtTo = new AshUsercontrol.AshTextBoxNormal();
            this.txtRtPtg = new AshUsercontrol.AshTextBoxNormal();
            this.txtPassword = new AshUsercontrol.AshTextBoxNormal();
            this.btnTransfer = new AshUsercontrol.AshButton();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cbBillGeneration
            // 
            resources.ApplyResources(this.cbBillGeneration, "cbBillGeneration");
            this.cbBillGeneration.Checked = true;
            this.cbBillGeneration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBillGeneration.Name = "cbBillGeneration";
            this.cbBillGeneration.UseVisualStyleBackColor = true;
            // 
            // cbRmvDisc
            // 
            resources.ApplyResources(this.cbRmvDisc, "cbRmvDisc");
            this.cbRmvDisc.Checked = true;
            this.cbRmvDisc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRmvDisc.Name = "cbRmvDisc";
            this.cbRmvDisc.UseVisualStyleBackColor = true;
            // 
            // cbRndRate
            // 
            resources.ApplyResources(this.cbRndRate, "cbRndRate");
            this.cbRndRate.Checked = true;
            this.cbRndRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRndRate.Name = "cbRndRate";
            this.cbRndRate.UseVisualStyleBackColor = true;
            // 
            // cbMergeBill
            // 
            resources.ApplyResources(this.cbMergeBill, "cbMergeBill");
            this.cbMergeBill.Name = "cbMergeBill";
            this.cbMergeBill.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // cbReadyCash
            // 
            resources.ApplyResources(this.cbReadyCash, "cbReadyCash");
            this.cbReadyCash.Checked = true;
            this.cbReadyCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReadyCash.Name = "cbReadyCash";
            this.cbReadyCash.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // ashDateTimePicker1
            // 
            resources.ApplyResources(this.ashDateTimePicker1, "ashDateTimePicker1");
            this.ashDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ashDateTimePicker1.Name = "ashDateTimePicker1";
            // 
            // txtFrom
            // 
            resources.ApplyResources(this.txtFrom, "txtFrom");
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Data = 0;
            this.txtFrom.EnterColor = System.Drawing.Color.LightPink;
            this.txtFrom.Format = null;
            this.txtFrom.isAllowNegative = false;
            this.txtFrom.isAllowSpecialChar = false;
            this.txtFrom.isNumberOnly = true;
            this.txtFrom.isNumeric = false;
            this.txtFrom.LeaveColor = System.Drawing.Color.White;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtQtyPercentage
            // 
            resources.ApplyResources(this.txtQtyPercentage, "txtQtyPercentage");
            this.txtQtyPercentage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQtyPercentage.Data = 100;
            this.txtQtyPercentage.EnterColor = System.Drawing.Color.LightPink;
            this.txtQtyPercentage.Format = null;
            this.txtQtyPercentage.isAllowNegative = false;
            this.txtQtyPercentage.isAllowSpecialChar = false;
            this.txtQtyPercentage.isNumberOnly = false;
            this.txtQtyPercentage.isNumeric = true;
            this.txtQtyPercentage.LeaveColor = System.Drawing.Color.White;
            this.txtQtyPercentage.Name = "txtQtyPercentage";
            this.txtQtyPercentage.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtmTo.Name = "dtmTo";
            // 
            // txtTo
            // 
            resources.ApplyResources(this.txtTo, "txtTo");
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTo.Data = 0;
            this.txtTo.EnterColor = System.Drawing.Color.LightPink;
            this.txtTo.Format = null;
            this.txtTo.isAllowNegative = false;
            this.txtTo.isAllowSpecialChar = false;
            this.txtTo.isNumberOnly = true;
            this.txtTo.isNumeric = false;
            this.txtTo.LeaveColor = System.Drawing.Color.White;
            this.txtTo.Name = "txtTo";
            this.txtTo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtRtPtg
            // 
            resources.ApplyResources(this.txtRtPtg, "txtRtPtg");
            this.txtRtPtg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRtPtg.Data = 100;
            this.txtRtPtg.EnterColor = System.Drawing.Color.LightPink;
            this.txtRtPtg.Format = null;
            this.txtRtPtg.isAllowNegative = false;
            this.txtRtPtg.isAllowSpecialChar = false;
            this.txtRtPtg.isNumberOnly = false;
            this.txtRtPtg.isNumeric = true;
            this.txtRtPtg.LeaveColor = System.Drawing.Color.White;
            this.txtRtPtg.Name = "txtRtPtg";
            this.txtRtPtg.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Data = 0;
            this.txtPassword.EnterColor = System.Drawing.Color.LightPink;
            this.txtPassword.Format = null;
            this.txtPassword.isAllowNegative = false;
            this.txtPassword.isAllowSpecialChar = false;
            this.txtPassword.isNumberOnly = false;
            this.txtPassword.isNumeric = false;
            this.txtPassword.LeaveColor = System.Drawing.Color.White;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnTransfer
            // 
            resources.ApplyResources(this.btnTransfer, "btnTransfer");
            this.btnTransfer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTransfer.FlatAppearance.BorderSize = 0;
            this.btnTransfer.ForeColor = System.Drawing.Color.White;
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // BillGeneration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtRtPtg);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.txtQtyPercentage);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.ashDateTimePicker1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.cbMergeBill);
            this.Controls.Add(this.cbRndRate);
            this.Controls.Add(this.cbRmvDisc);
            this.Controls.Add(this.cbReadyCash);
            this.Controls.Add(this.cbBillGeneration);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.Name = "BillGeneration";
            this.Load += new System.EventHandler(this.BillGeneration_Load);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cbBillGeneration, 0);
            this.Controls.SetChildIndex(this.cbReadyCash, 0);
            this.Controls.SetChildIndex(this.cbRmvDisc, 0);
            this.Controls.SetChildIndex(this.cbRndRate, 0);
            this.Controls.SetChildIndex(this.cbMergeBill, 0);
            this.Controls.SetChildIndex(this.progressBar, 0);
            this.Controls.SetChildIndex(this.btnTransfer, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.ashDateTimePicker1, 0);
            this.Controls.SetChildIndex(this.txtFrom, 0);
            this.Controls.SetChildIndex(this.txtQtyPercentage, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.txtTo, 0);
            this.Controls.SetChildIndex(this.txtRtPtg, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
