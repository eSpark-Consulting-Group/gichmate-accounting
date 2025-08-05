using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;


namespace Ashpro
{
	public class POSCashPayment : BaseForm
	{
		private bool isCashEntered = false;

		private bool isCardEntered = false;

		private bool isOthersEntered = false;

		private IContainer components = null;

		private Label label3;

		private Button btnOK;

		private Button btnClear;

		private Button btnClose;

		private Label label5;

		private Label label7;

		private Label label8;

		public AshComboBox cmbCashAccount;

		public AshComboBox cmbBankAccount;

		public AshTextBoxNormal txtCardPaid;

		private DashboardPOSKeyboard nkPOS;

		private TabControl tabPayments;

		private TabPage tpCash;

		private TabPage tpCard;

		private Panel panel1;

		private AshGradientPanel ashGradientPanel1;

		private Panel panel2;

		private Button btnCardPayment;

		private Button btnCashPayment;

		public AshTextBoxNormal txtCashPaid;

		private TabPage tpOthers;

		public AshComboBox cmbBankOthers;

		private Label label11;

		private Label label12;

		public AshTextBoxNormal txtOtherPaid;

		private Button btnOtherPayment;

		private Label label15;

		private Label label14;

		private Label label2;

		public AshNumericLabel lblBalanceAmount;

		public AshNumericLabel lblInvoiceAmount;

		public AshNumericLabel lblPaidAmount;

		public Label lblTitle;

		private Label label1;

		private Label label4;

		private Label lblOnlinePayment;

		public bool isShowNumberPad { get; set; }

		public int? iCashAccountId { get; set; }

		public int? iCardAccountId { get; set; }

		public int? iOthersAccountId { get; set; }

		public int? iPartyAccountId { get; set; }

		public decimal dCashPayment { get; set; }

		public decimal dCardPayment { get; set; }

		public decimal dOtherPayment { get; set; }

		public bool isReturn { get; set; } = false;


		public POSCashPayment()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			btnCashPayment.BackColor = Settings.Default.BackColor;
			txtCashPaid.Format = CommonSettings.strCurencyFormat;
			txtCardPaid.Format = CommonSettings.strCurencyFormat;
			txtOtherPaid.Format = CommonSettings.strCurencyFormat;
			lblInvoiceAmount.Format = CommonSettings.strCurencyFormat;
			lblPaidAmount.Format = CommonSettings.strCurencyFormat;
			lblBalanceAmount.Format = CommonSettings.strCurencyFormat;
			nkPOS.StarButton.Text = "C";
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			if (CommonSettings.strOnlinePayment != null)
			{
				lblOnlinePayment.Text = CommonSettings.strOnlinePayment;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (tabPayments.SelectedIndex == 0)
			{
				dCashPayment = txtCashPaid.Value;
				if (dCardPayment == 0m)
				{
					dCardPayment = (isCardEntered ? txtCardPaid.Value : 0m);
				}
				if (dOtherPayment == 0m)
				{
					dOtherPayment = (isOthersEntered ? txtOtherPaid.Value : 0m);
				}
			}
			else if (tabPayments.SelectedIndex == 1)
			{
				dCardPayment = txtCardPaid.Value;
				if (dCashPayment == 0m)
				{
					dCashPayment = (isCashEntered ? txtCashPaid.Value : 0m);
				}
				if (dOtherPayment == 0m)
				{
					dOtherPayment = (isOthersEntered ? txtOtherPaid.Value : 0m);
				}
			}
			else
			{
				dOtherPayment = txtOtherPaid.Value;
				if (dCardPayment == 0m)
				{
					dCardPayment = (isCardEntered ? txtCardPaid.Value : 0m);
				}
				if (dCashPayment == 0m)
				{
					dCashPayment = (isCashEntered ? txtCashPaid.Value : 0m);
				}
			}
			decimal num = dCashPayment + dCardPayment + dOtherPayment;
			if (num >= lblInvoiceAmount.Value)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else if (iPartyAccountId == iCashAccountId)
			{
				Messages.ErrorMessage("Please pay full amount");
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void POSCashPayment_Load(object sender, EventArgs e)
		{
			foreach (Control control in nkPOS.Controls)
			{
				if (control is Button)
				{
					control.BackColor = Settings.Default.OtherB;
					control.ForeColor = Settings.Default.OtherF;
				}
			}
			Button button = btnClear;
			Button button2 = btnClose;
			Color color = (btnOK.BackColor = Settings.Default.BackColor);
			Color color4 = (button.BackColor = (button2.BackColor = color));
			Button button3 = btnClear;
			Button button4 = btnClose;
			color = (btnOK.ForeColor = Settings.Default.ForeColor);
			color4 = (button3.ForeColor = (button4.ForeColor = color));
			btnEnd.Visible = false;
			nkPOS.Visible = isShowNumberPad;
			if (!isShowNumberPad)
			{
				base.Size = new Size(567, 306);
			}
			AniHelper.FillCashAccount(cmbCashAccount);
			AniHelper.FillBankAccount(cmbBankAccount);
			AniHelper.FillBankAccount(cmbBankOthers);
			if (iCashAccountId.HasValue)
			{
				int? num = iCashAccountId;
				if (!((num.GetValueOrDefault() == 0) & num.HasValue))
				{
					cmbCashAccount.SelectedValue = iCashAccountId;
					goto IL_0194;
				}
			}
			cmbCashAccount.SelectedValue = CommonSettings.strDefaultCashAccountId.ToInt32();
			goto IL_0194;
			IL_01f6:
			if (iOthersAccountId.HasValue)
			{
				int? num = iOthersAccountId;
				if (!((num.GetValueOrDefault() == 0) & num.HasValue))
				{
					cmbBankOthers.SelectedValue = iOthersAccountId;
					goto IL_0258;
				}
			}
			cmbBankOthers.SelectedValue = CommonSettings.strDefaultOnlineAccountId.ToInt32();
			goto IL_0258;
			IL_0194:
			if (iCardAccountId.HasValue)
			{
				int? num = iCardAccountId;
				if (!((num.GetValueOrDefault() == 0) & num.HasValue))
				{
					cmbBankAccount.SelectedValue = iCardAccountId;
					goto IL_01f6;
				}
			}
			cmbBankAccount.SelectedValue = CommonSettings.strDefaultBankAccountId.ToInt32();
			goto IL_01f6;
			IL_0258:
			base.ActiveControl = txtCashPaid;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
		}

		private void dashboardPOSKeyboard1_atStarClick(object source)
		{
			SendKeys.Send("{BACKSPACE}");
		}

		private void tabPayments_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabPayments.SelectedIndex == 0)
			{
				txtCashPaid.Focus();
				txtCashPaid.SelectAll();
			}
			else if (tabPayments.SelectedIndex == 1)
			{
				txtCardPaid.Focus();
				txtCardPaid.SelectAll();
			}
			else
			{
				txtOtherPaid.Focus();
				txtOtherPaid.SelectAll();
			}
		}

		private void btnCashPayment_Click(object sender, EventArgs e)
		{
			tabPayments.SelectedTab = tpCash;
			btnCashPayment.BackColor = Settings.Default.BackColor;
			Color color2 = (btnCardPayment.BackColor = (btnOtherPayment.BackColor = Color.Transparent));
			if (txtCashPaid.Value == 0m)
			{
				txtCashPaid.Value = ((lblBalanceAmount.Value > 0m) ? lblBalanceAmount.Value : 0m);
			}
		}

		private void btnCardPayment_Click(object sender, EventArgs e)
		{
			tabPayments.SelectedTab = tpCard;
			btnCardPayment.BackColor = Settings.Default.BackColor;
			Color color2 = (btnCashPayment.BackColor = (btnOtherPayment.BackColor = Color.Transparent));
			if (txtCardPaid.Value == 0m)
			{
				txtCardPaid.Value = ((lblBalanceAmount.Value > 0m) ? lblBalanceAmount.Value : 0m);
			}
			txtCardPaid.SelectAll();
		}

		private void btnOtherPayment_Click(object sender, EventArgs e)
		{
			tabPayments.SelectedTab = tpOthers;
			btnOtherPayment.BackColor = Settings.Default.BackColor;
			Color color2 = (btnCardPayment.BackColor = (btnCashPayment.BackColor = Color.Transparent));
			if (txtOtherPaid.Value == 0m)
			{
				txtOtherPaid.Value = ((lblBalanceAmount.Value > 0m) ? lblBalanceAmount.Value : 0m);
			}
			txtOtherPaid.SelectAll();
		}

		private void txtCardPaid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				btnOK.PerformClick();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void POSCashPayment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				e.SuppressKeyPress = true;
				if (tabPayments.SelectedIndex == 0)
				{
					btnCardPayment.PerformClick();
				}
				else if (tabPayments.SelectedIndex == 2)
				{
					btnCashPayment.PerformClick();
				}
				else
				{
					btnOtherPayment.PerformClick();
				}
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void txtCashPaid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (txtCashPaid.Value > 0m)
			{
				isCashEntered = true;
			}
			else
			{
				isCashEntered = false;
			}
		}

		private void txtCardPaid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (txtCardPaid.Value > 0m)
			{
				isCardEntered = true;
			}
			else
			{
				isCardEntered = false;
			}
		}

		private void txtOtherPaid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (txtOtherPaid.Value > 0m)
			{
				isOthersEntered = true;
			}
			else
			{
				isOthersEntered = false;
			}
		}

		private void txtPaid_Validated(object sender, EventArgs e)
		{
			decimal num = (isCashEntered ? txtCashPaid.Value : 0m);
			decimal num2 = (isOthersEntered ? txtOtherPaid.Value : 0m);
			decimal num3 = (isCardEntered ? txtCardPaid.Value : 0m);
			if (dCashPayment + dCardPayment + dOtherPayment == 0m)
			{
				lblPaidAmount.Value = num3 + num + num2;
			}
			else
			{
				lblPaidAmount.Value = dCashPayment + dCardPayment + dOtherPayment;
			}
			lblBalanceAmount.Value = lblInvoiceAmount.Value - lblPaidAmount.Value;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSCashPayment));
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbBankAccount = new AshUsercontrol.AshComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCardPaid = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCashAccount = new AshUsercontrol.AshComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.nkPOS = new AshUsercontrol.DashboardPOSKeyboard();
            this.tabPayments = new System.Windows.Forms.TabControl();
            this.tpCash = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCashPaid = new AshUsercontrol.AshTextBoxNormal();
            this.tpCard = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tpOthers = new System.Windows.Forms.TabPage();
            this.lblOnlinePayment = new System.Windows.Forms.Label();
            this.cmbBankOthers = new AshUsercontrol.AshComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOtherPaid = new AshUsercontrol.AshTextBoxNormal();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ashGradientPanel1 = new AshUsercontrol.AshGradientPanel();
            this.btnOtherPayment = new System.Windows.Forms.Button();
            this.btnCardPayment = new System.Windows.Forms.Button();
            this.btnCashPayment = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblPaidAmount = new AshUsercontrol.AshNumericLabel();
            this.lblBalanceAmount = new AshUsercontrol.AshNumericLabel();
            this.lblInvoiceAmount = new AshUsercontrol.AshNumericLabel();
            this.tabPayments.SuspendLayout();
            this.tpCash.SuspendLayout();
            this.tpCard.SuspendLayout();
            this.tpOthers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ashGradientPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // cmbBankAccount
            // 
            resources.ApplyResources(this.cmbBankAccount, "cmbBankAccount");
            this.cmbBankAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbBankAccount.Name = "cmbBankAccount";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtCardPaid
            // 
            resources.ApplyResources(this.txtCardPaid, "txtCardPaid");
            this.txtCardPaid.BackColor = System.Drawing.Color.LightPink;
            this.txtCardPaid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardPaid.Data = 0;
            this.txtCardPaid.EnterColor = System.Drawing.Color.LightPink;
            this.txtCardPaid.Format = null;
            this.txtCardPaid.isAllowNegative = false;
            this.txtCardPaid.isAllowSpecialChar = false;
            this.txtCardPaid.isNumberOnly = false;
            this.txtCardPaid.isNumeric = true;
            this.txtCardPaid.LeaveColor = System.Drawing.Color.White;
            this.txtCardPaid.Name = "txtCardPaid";
            this.txtCardPaid.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCardPaid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardPaid_KeyDown);
            this.txtCardPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardPaid_KeyPress);
            this.txtCardPaid.Validated += new System.EventHandler(this.txtPaid_Validated);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cmbCashAccount
            // 
            resources.ApplyResources(this.cmbCashAccount, "cmbCashAccount");
            this.cmbCashAccount.BackColor = System.Drawing.Color.White;
            this.cmbCashAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCashAccount.FormattingEnabled = true;
            this.cmbCashAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbCashAccount.Name = "cmbCashAccount";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // nkPOS
            // 
            resources.ApplyResources(this.nkPOS, "nkPOS");
            this.nkPOS.Name = "nkPOS";
            this.nkPOS.atStarClick += new AshUsercontrol.StarClickEventHandler(this.dashboardPOSKeyboard1_atStarClick);
            // 
            // tabPayments
            // 
            resources.ApplyResources(this.tabPayments, "tabPayments");
            this.tabPayments.Controls.Add(this.tpCash);
            this.tabPayments.Controls.Add(this.tpCard);
            this.tabPayments.Controls.Add(this.tpOthers);
            this.tabPayments.Name = "tabPayments";
            this.tabPayments.SelectedIndex = 0;
            this.tabPayments.SelectedIndexChanged += new System.EventHandler(this.tabPayments_SelectedIndexChanged);
            // 
            // tpCash
            // 
            resources.ApplyResources(this.tpCash, "tpCash");
            this.tpCash.Controls.Add(this.label1);
            this.tpCash.Controls.Add(this.txtCashPaid);
            this.tpCash.Controls.Add(this.cmbCashAccount);
            this.tpCash.Controls.Add(this.label5);
            this.tpCash.Controls.Add(this.label3);
            this.tpCash.Name = "tpCash";
            this.tpCash.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCashPaid
            // 
            resources.ApplyResources(this.txtCashPaid, "txtCashPaid");
            this.txtCashPaid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashPaid.Data = 0;
            this.txtCashPaid.EnterColor = System.Drawing.Color.LightPink;
            this.txtCashPaid.Format = null;
            this.txtCashPaid.isAllowNegative = false;
            this.txtCashPaid.isAllowSpecialChar = false;
            this.txtCashPaid.isNumberOnly = false;
            this.txtCashPaid.isNumeric = true;
            this.txtCashPaid.LeaveColor = System.Drawing.Color.White;
            this.txtCashPaid.Name = "txtCashPaid";
            this.txtCashPaid.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCashPaid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardPaid_KeyDown);
            this.txtCashPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCashPaid_KeyPress);
            this.txtCashPaid.Validated += new System.EventHandler(this.txtPaid_Validated);
            // 
            // tpCard
            // 
            resources.ApplyResources(this.tpCard, "tpCard");
            this.tpCard.Controls.Add(this.label4);
            this.tpCard.Controls.Add(this.cmbBankAccount);
            this.tpCard.Controls.Add(this.label7);
            this.tpCard.Controls.Add(this.label8);
            this.tpCard.Controls.Add(this.txtCardPaid);
            this.tpCard.Name = "tpCard";
            this.tpCard.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tpOthers
            // 
            resources.ApplyResources(this.tpOthers, "tpOthers");
            this.tpOthers.Controls.Add(this.lblOnlinePayment);
            this.tpOthers.Controls.Add(this.cmbBankOthers);
            this.tpOthers.Controls.Add(this.label11);
            this.tpOthers.Controls.Add(this.label12);
            this.tpOthers.Controls.Add(this.txtOtherPaid);
            this.tpOthers.Name = "tpOthers";
            this.tpOthers.UseVisualStyleBackColor = true;
            // 
            // lblOnlinePayment
            // 
            resources.ApplyResources(this.lblOnlinePayment, "lblOnlinePayment");
            this.lblOnlinePayment.Name = "lblOnlinePayment";
            // 
            // cmbBankOthers
            // 
            resources.ApplyResources(this.cmbBankOthers, "cmbBankOthers");
            this.cmbBankOthers.EnterColor = System.Drawing.Color.LightPink;
            this.cmbBankOthers.FormattingEnabled = true;
            this.cmbBankOthers.LeaveColor = System.Drawing.Color.White;
            this.cmbBankOthers.Name = "cmbBankOthers";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // txtOtherPaid
            // 
            resources.ApplyResources(this.txtOtherPaid, "txtOtherPaid");
            this.txtOtherPaid.BackColor = System.Drawing.Color.LightPink;
            this.txtOtherPaid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherPaid.Data = 0;
            this.txtOtherPaid.EnterColor = System.Drawing.Color.LightPink;
            this.txtOtherPaid.Format = null;
            this.txtOtherPaid.isAllowNegative = false;
            this.txtOtherPaid.isAllowSpecialChar = false;
            this.txtOtherPaid.isNumberOnly = false;
            this.txtOtherPaid.isNumeric = true;
            this.txtOtherPaid.LeaveColor = System.Drawing.Color.White;
            this.txtOtherPaid.Name = "txtOtherPaid";
            this.txtOtherPaid.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtOtherPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOtherPaid_KeyPress);
            this.txtOtherPaid.Validated += new System.EventHandler(this.txtPaid_Validated);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Name = "panel1";
            // 
            // ashGradientPanel1
            // 
            resources.ApplyResources(this.ashGradientPanel1, "ashGradientPanel1");
            this.ashGradientPanel1.AllowMultiSelect = false;
            this.ashGradientPanel1.Angle = 0F;
            this.ashGradientPanel1.BackColor = System.Drawing.Color.DimGray;
            this.ashGradientPanel1.BottomColor = System.Drawing.Color.White;
            this.ashGradientPanel1.Controls.Add(this.btnOtherPayment);
            this.ashGradientPanel1.Controls.Add(this.btnCardPayment);
            this.ashGradientPanel1.Controls.Add(this.btnCashPayment);
            this.ashGradientPanel1.Name = "ashGradientPanel1";
            this.ashGradientPanel1.Selected = false;
            this.ashGradientPanel1.TextAdjestmentHeight = 0;
            this.ashGradientPanel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ashGradientPanel1.TopColor = System.Drawing.Color.DodgerBlue;
            // 
            // btnOtherPayment
            // 
            resources.ApplyResources(this.btnOtherPayment, "btnOtherPayment");
            this.btnOtherPayment.BackColor = System.Drawing.Color.Transparent;
            this.btnOtherPayment.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOtherPayment.FlatAppearance.BorderSize = 0;
            this.btnOtherPayment.ForeColor = System.Drawing.Color.Black;
            this.btnOtherPayment.Name = "btnOtherPayment";
            this.btnOtherPayment.UseVisualStyleBackColor = false;
            this.btnOtherPayment.Click += new System.EventHandler(this.btnOtherPayment_Click);
            // 
            // btnCardPayment
            // 
            resources.ApplyResources(this.btnCardPayment, "btnCardPayment");
            this.btnCardPayment.BackColor = System.Drawing.Color.Transparent;
            this.btnCardPayment.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCardPayment.FlatAppearance.BorderSize = 0;
            this.btnCardPayment.ForeColor = System.Drawing.Color.Black;
            this.btnCardPayment.Name = "btnCardPayment";
            this.btnCardPayment.UseVisualStyleBackColor = false;
            this.btnCardPayment.Click += new System.EventHandler(this.btnCardPayment_Click);
            // 
            // btnCashPayment
            // 
            resources.ApplyResources(this.btnCashPayment, "btnCashPayment");
            this.btnCashPayment.BackColor = System.Drawing.Color.Transparent;
            this.btnCashPayment.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCashPayment.FlatAppearance.BorderSize = 0;
            this.btnCashPayment.ForeColor = System.Drawing.Color.Black;
            this.btnCashPayment.Name = "btnCashPayment";
            this.btnCashPayment.UseVisualStyleBackColor = false;
            this.btnCashPayment.Click += new System.EventHandler(this.btnCashPayment_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.lblPaidAmount);
            this.panel2.Controls.Add(this.lblBalanceAmount);
            this.panel2.Controls.Add(this.lblInvoiceAmount);
            this.panel2.Name = "panel2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // lblPaidAmount
            // 
            resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
            this.lblPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPaidAmount.ForeColor = System.Drawing.Color.Red;
            this.lblPaidAmount.Format = null;
            this.lblPaidAmount.Name = "lblPaidAmount";
            this.lblPaidAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblBalanceAmount
            // 
            resources.ApplyResources(this.lblBalanceAmount, "lblBalanceAmount");
            this.lblBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Format = null;
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblInvoiceAmount
            // 
            resources.ApplyResources(this.lblInvoiceAmount, "lblInvoiceAmount");
            this.lblInvoiceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblInvoiceAmount.Format = null;
            this.lblInvoiceAmount.Name = "lblInvoiceAmount";
            this.lblInvoiceAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // POSCashPayment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.ashGradientPanel1);
            this.Controls.Add(this.nkPOS);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabPayments);
            this.Controls.Add(this.lblTitle);
            this.Name = "POSCashPayment";
            this.Load += new System.EventHandler(this.POSCashPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.POSCashPayment_KeyDown);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.tabPayments, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.nkPOS, 0);
            this.Controls.SetChildIndex(this.ashGradientPanel1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.tabPayments.ResumeLayout(false);
            this.tpCash.ResumeLayout(false);
            this.tpCash.PerformLayout();
            this.tpCard.ResumeLayout(false);
            this.tpCard.PerformLayout();
            this.tpOthers.ResumeLayout(false);
            this.tpOthers.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ashGradientPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
