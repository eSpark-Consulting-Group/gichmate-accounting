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
	public class BillPrint : BaseForm
	{
		private DotmatrixMaster entDMaster;

		private List<DotmatrixSettings> entDSettings = new List<DotmatrixSettings>();

		private ExCompany entcompany;

		private PartyDetails entPartyDtl;

		private List<SalesDetails> entSalesDetails;

		private SalesMaster entSalesMaster;

		private SalesRtnMaster entSalesRtnMaster;

		private List<SalesRtnDetails> entSalesrtnDetails;

		private TransactionMaster tMaster;

		private List<Transaction> enttransactions = new List<Transaction>();

		private AccountEntity account;

		private bool isCancelled = false;

		private int intTo;

		private int intFrom;

		private int iFYearId;

		private int iSalesInvId;

		private int iPartyAccountId;

		private int iMasterId;

		private string sInvoiceType;

		private string sFormType;

		private DBContext db;

		private string sBranchDetails = string.Empty;

		private IContainer components = null;

		private Label label1;

		private AshTextBoxNormal txtFrom;

		private Label label2;

		private AshTextBoxNormal txtTo;

		private Label label3;

		private AshComboBox cmbInvType;

		private Button btnClear;

		private Button btnPrint;

		private Button btnClose;

		private Label label4;

		private AshTextBoxNormal txtCopies;

		private RadioButton rbFrirst;

		private RadioButton rbLast;

		private Label label5;

		private Label lblPrintPage;

		private Button btnStop;

		private ToolTip toolTip1;

		private BackgroundWorker backgroundWorker1;

		private Button button8;

		private Label lblTitle;

		private Label label6;

		private AshComboBox cmbFormType;

		public BillPrint()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			backgroundWorker1.WorkerReportsProgress = true;
		}

		private void BillPrint_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			iFYearId = AshproCommon.IdFindMethod("Select FyearId from SelectedFYear");
			entcompany = new ExCompany();
			Company company = new Company();
			company = db.Companies().FirstOrDefault();
			entcompany = company.CopyTo<ExCompany>();
			entcompany.CompanyName = company.InstituteName;
			entcompany.TaxId = company.GSTNo;
			if (CommonSettings.blnEnableMultiBranch)
			{
				sBranchDetails = " and FK_BranchId = '" + Settings.Default.iBranchId + "'";
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (!isCancelled)
			{
				lblPrintPage.Text = "Invoice- " + e.ProgressPercentage;
				lblPrintPage.Update();
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (!(txtFrom.Text != "") || !(txtTo.Text != ""))
				{
					return;
				}
				intFrom = Convert.ToInt32(txtFrom.Text);
				intTo = Convert.ToInt32(txtTo.Text);
				if (rbFrirst.Checked && intFrom <= intTo)
				{
					for (int i = intFrom; i < intTo + 1; i++)
					{
						for (int j = 1; j < Convert.ToInt16(txtCopies.Text) + 1; j++)
						{
							if (!isCancelled)
							{
								PreprintMethod(i);
							}
						}
						backgroundWorker1.ReportProgress(i);
					}
					return;
				}
				for (int num = intTo; num > intFrom - 1; num--)
				{
					for (int k = 1; k < Convert.ToInt16(txtCopies.Text) + 1; k++)
					{
						if (!isCancelled)
						{
							PreprintMethod(num);
						}
					}
					backgroundWorker1.ReportProgress(num);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "  182");
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			btnPrint.Enabled = true;
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			isCancelled = false;
			btnPrint.Enabled = false;
			backgroundWorker1.RunWorkerAsync();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (backgroundWorker1.IsBusy)
			{
				isCancelled = true;
				backgroundWorker1.CancelAsync();
			}
			txtFrom.Text = "1";
			txtTo.Text = "1";
			cmbInvType.Text = "Sales Estimate";
			txtCopies.Text = "1";
			lblPrintPage.Text = string.Empty;
			btnPrint.Enabled = true;
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			if (backgroundWorker1.IsBusy)
			{
				isCancelled = true;
				backgroundWorker1.CancelAsync();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (backgroundWorker1.IsBusy)
			{
				DialogResult dialogResult = MessageBox.Show("Do you really want to Close? This will stop the printing process.", Utility.uManufacturer, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					isCancelled = true;
					backgroundWorker1.CancelAsync();
					Close();
				}
			}
			else
			{
				Close();
			}
		}

		private void cmbInvType_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			if (cmbFormType.DataSource != null)
			{
				cmbFormType.DataSource = null;
			}
			else
			{
				cmbFormType.Items.Clear();
			}
			if (cmbInvType.SelectedIndex == 0)
			{
				AniHelper.FillFormType(cmbFormType);
				return;
			}
			cmbFormType.Items.Add(cmbInvType.Text);
			cmbFormType.SelectedIndex = 0;
		}

		private void cmbFormType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtFrom.Text = string.Empty;
			txtTo.Text = string.Empty;
			lblPrintPage.Text = string.Empty;
			sInvoiceType = cmbInvType.Text;
			sFormType = cmbFormType.Text;
			LastInvNoFindMethod();
		}

		private void LastInvNoFindMethod()
		{
			int num = 0;
			if (cmbInvType.Text == "Sales Estimate")
			{
				iMasterId = AshproCommon.IdFindMethod("Select printMasterId From PrintDesignMaster where VochrType='" + cmbInvType.Text + "' and isDefault='" + true + "'");
				num = AshproCommon.IdFindMethod("select max(SalesInvNo) as maxValue from SalesEstimateMaster where InvType='Normal Sale' and FYearID='" + iFYearId + "'" + sBranchDetails);
			}
			else if (cmbInvType.Text == "Sales Invoice")
			{
				num = AshproCommon.IdFindMethod("select max(SalesInvNo) as maxValue from SalesEstimateMaster where InvType='" + cmbFormType.Text + "'and FYearID='" + iFYearId + "'" + sBranchDetails);
				iMasterId = AshproCommon.IdFindMethod("Select printMasterId From PrintDesignMaster where VochrType='" + cmbInvType.Text + "' and isDefault='" + true + "'");
			}
			else
			{
				num = AshproCommon.IdFindMethod("select max(SlRtnInvNo) as maxValue from SalesRtnMaster where FYearID='" + iFYearId + "' " + sBranchDetails);
				iMasterId = AshproCommon.IdFindMethod("Select printMasterId From PrintDesignMaster where VochrType='" + cmbInvType.Text + "' and isDefault='" + true + "'");
			}
			txtFrom.Text = num.ToString();
			txtTo.Text = num.ToString();
		}

		private void PreprintMethod(int intInvNo)
		{
			entPartyDtl = new PartyDetails();
			account = new AccountEntity();
			enttransactions = new List<Transaction>();
			tMaster = new TransactionMaster();
			entDMaster = new DotmatrixMaster();
			entDSettings = new List<DotmatrixSettings>();
			entDMaster = db.DotmatrixMasters().FirstOrDefault((DotmatrixMaster x) => x.printMasterId == iMasterId);
			entDSettings = (from x in db.DotmatrixSettings()
				where x.printMasterId == iMasterId
				select x).ToList();
			if (sInvoiceType == "Sales Estimate")
			{
				entSalesMaster = new SalesMaster();
				entSalesDetails = new List<SalesDetails>();
				iSalesInvId = AshproCommon.IdFindMethod("Select SalesInvId From SalesEstimateMaster Where InvType='Normal Sale'and SalesInvNo='" + intInvNo + "' and FYearID='" + iFYearId + "' " + sBranchDetails);
				entSalesMaster = db.SalesMasters().FirstOrDefault((SalesMaster x) => x.SalesInvId == iSalesInvId);
				entSalesDetails = (from x in db.SalesDetails()
					where x.SalesInvId == iSalesInvId
					select x).ToList();
				tMaster = POSHelper.GetTMaster(entSalesMaster);
				enttransactions = POSHelper.GetTransactionsFromSDetails(entSalesDetails, db);
				iPartyAccountId = entSalesMaster.PartyAccountId.toInt32();
			}
			else if (sInvoiceType == "Sales Invoice")
			{
				entSalesMaster = new SalesMaster();
				entSalesDetails = new List<SalesDetails>();
				iSalesInvId = AshproCommon.IdFindMethod("Select SalesInvId From SalesEstimateMaster Where InvType='" + sFormType + "'and SalesInvNo='" + intInvNo + "' and FYearID='" + iFYearId + "'" + sBranchDetails);
				entSalesMaster = db.SalesMasters().FirstOrDefault((SalesMaster x) => x.SalesInvId == iSalesInvId);
				entSalesDetails = (from x in db.SalesDetails()
					where x.SalesInvId == iSalesInvId
					select x).ToList();
				tMaster = POSHelper.GetTMaster(entSalesMaster);
				enttransactions = POSHelper.GetTransactionsFromSDetails(entSalesDetails, db);
				iPartyAccountId = entSalesMaster.PartyAccountId.toInt32();
			}
			else
			{
				entSalesrtnDetails = new List<SalesRtnDetails>();
				entSalesRtnMaster = new SalesRtnMaster();
				iSalesInvId = AshproCommon.IdFindMethod("Select SlsRtrnId From SalesRtnMaster Where SlRtnInvNo='" + intInvNo + "' and FYearID='" + iFYearId + "'" + sBranchDetails);
				entSalesRtnMaster = db.SalesRtnMasters().FirstOrDefault((SalesRtnMaster x) => x.SlsRtrnId == iSalesInvId);
				entSalesrtnDetails = (from x in db.SalesRtnDetails()
					where x.SlsRtnId == iSalesInvId
					select x).ToList();
				tMaster = POSHelper.GetTMaster(entSalesRtnMaster);
				enttransactions = POSHelper.GetTransactionsFromSRDetails(entSalesrtnDetails, db);
				iPartyAccountId = entSalesRtnMaster.PartyAccountId.toInt32();
			}
			account = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountId == iPartyAccountId);
			entPartyDtl = POSHelper.GetParty(account);
			PrintHelper printHelper = new PrintHelper();
			printHelper.PrintMethod(entDMaster, entDSettings, entcompany, tMaster, entPartyDtl, enttransactions, tMaster, null, tMaster, isPrint: true, Convert.ToInt32(txtCopies.Text));
			printHelper = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillPrint));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrom = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTo = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbInvType = new AshUsercontrol.AshComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCopies = new AshUsercontrol.AshTextBoxNormal();
            this.rbFrirst = new System.Windows.Forms.RadioButton();
            this.rbLast = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPrintPage = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button8 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFormType = new AshUsercontrol.AshComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // txtFrom
            // 
            resources.ApplyResources(this.txtFrom, "txtFrom");
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Data = 1;
            this.txtFrom.EnterColor = System.Drawing.Color.LightPink;
            this.txtFrom.Format = null;
            this.txtFrom.isAllowNegative = false;
            this.txtFrom.isAllowSpecialChar = false;
            this.txtFrom.isNumberOnly = true;
            this.txtFrom.isNumeric = false;
            this.txtFrom.LeaveColor = System.Drawing.Color.White;
            this.txtFrom.Name = "txtFrom";
            this.toolTip1.SetToolTip(this.txtFrom, resources.GetString("txtFrom.ToolTip"));
            this.txtFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // txtTo
            // 
            resources.ApplyResources(this.txtTo, "txtTo");
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTo.Data = 1;
            this.txtTo.EnterColor = System.Drawing.Color.LightPink;
            this.txtTo.Format = null;
            this.txtTo.isAllowNegative = false;
            this.txtTo.isAllowSpecialChar = false;
            this.txtTo.isNumberOnly = true;
            this.txtTo.isNumeric = false;
            this.txtTo.LeaveColor = System.Drawing.Color.White;
            this.txtTo.Name = "txtTo";
            this.toolTip1.SetToolTip(this.txtTo, resources.GetString("txtTo.ToolTip"));
            this.txtTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // cmbInvType
            // 
            resources.ApplyResources(this.cmbInvType, "cmbInvType");
            this.cmbInvType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbInvType.FormattingEnabled = true;
            this.cmbInvType.Items.AddRange(new object[] {
            resources.GetString("cmbInvType.Items"),
            resources.GetString("cmbInvType.Items1"),
            resources.GetString("cmbInvType.Items2"),
            resources.GetString("cmbInvType.Items3"),
            resources.GetString("cmbInvType.Items4"),
            resources.GetString("cmbInvType.Items5"),
            resources.GetString("cmbInvType.Items6"),
            resources.GetString("cmbInvType.Items7")});
            this.cmbInvType.LeaveColor = System.Drawing.Color.White;
            this.cmbInvType.Name = "cmbInvType";
            this.toolTip1.SetToolTip(this.cmbInvType, resources.GetString("cmbInvType.ToolTip"));
            this.cmbInvType.SelectedIndexChanged += new System.EventHandler(this.cmbInvType_SelectedIndexChanged_1);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.toolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // txtCopies
            // 
            resources.ApplyResources(this.txtCopies, "txtCopies");
            this.txtCopies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCopies.Data = 1;
            this.txtCopies.EnterColor = System.Drawing.Color.LightPink;
            this.txtCopies.Format = null;
            this.txtCopies.isAllowNegative = false;
            this.txtCopies.isAllowSpecialChar = false;
            this.txtCopies.isNumberOnly = true;
            this.txtCopies.isNumeric = false;
            this.txtCopies.LeaveColor = System.Drawing.Color.White;
            this.txtCopies.Name = "txtCopies";
            this.toolTip1.SetToolTip(this.txtCopies, resources.GetString("txtCopies.ToolTip"));
            this.txtCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbFrirst
            // 
            resources.ApplyResources(this.rbFrirst, "rbFrirst");
            this.rbFrirst.Checked = true;
            this.rbFrirst.Name = "rbFrirst";
            this.rbFrirst.TabStop = true;
            this.toolTip1.SetToolTip(this.rbFrirst, resources.GetString("rbFrirst.ToolTip"));
            this.rbFrirst.UseVisualStyleBackColor = true;
            // 
            // rbLast
            // 
            resources.ApplyResources(this.rbLast, "rbLast");
            this.rbLast.Name = "rbLast";
            this.toolTip1.SetToolTip(this.rbLast, resources.GetString("rbLast.ToolTip"));
            this.rbLast.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // lblPrintPage
            // 
            resources.ApplyResources(this.lblPrintPage, "lblPrintPage");
            this.lblPrintPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintPage.Name = "lblPrintPage";
            this.toolTip1.SetToolTip(this.lblPrintPage, resources.GetString("lblPrintPage.ToolTip"));
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Name = "btnStop";
            this.toolTip1.SetToolTip(this.btnStop, resources.GetString("btnStop.ToolTip"));
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.toolTip1.SetToolTip(this.button8, resources.GetString("button8.ToolTip"));
            this.button8.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.toolTip1.SetToolTip(this.lblTitle, resources.GetString("lblTitle.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbFormType
            // 
            resources.ApplyResources(this.cmbFormType, "cmbFormType");
            this.cmbFormType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbFormType.FormattingEnabled = true;
            this.cmbFormType.LeaveColor = System.Drawing.Color.White;
            this.cmbFormType.Name = "cmbFormType";
            this.toolTip1.SetToolTip(this.cmbFormType, resources.GetString("cmbFormType.ToolTip"));
            this.cmbFormType.SelectedIndexChanged += new System.EventHandler(this.cmbFormType_SelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // BillPrint
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.button8);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblPrintPage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbLast);
            this.Controls.Add(this.rbFrirst);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cmbFormType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbInvType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCopies);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "BillPrint";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.BillPrint_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtFrom, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtCopies, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtTo, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbInvType, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbFormType, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.rbFrirst, 0);
            this.Controls.SetChildIndex(this.rbLast, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblPrintPage, 0);
            this.Controls.SetChildIndex(this.btnStop, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.button8, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
