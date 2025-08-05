using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using System.Globalization;
using System.Threading;

namespace Ashpro
{
	public class PaymentAndReceiptView : BaseForm
	{
		private DBContext db = new DBContext();

		private List<ReceiptAndPayment> receiptAndPayments;

		private List<PurchaseInvoiceHdr> entPurchases;

		private List<SalesMaster> entSalesMasters;

		private int? iVchrTranId;

		private VchrTranLog vchrTranLog;

		private string sVType = "Receipt Voucher";

		private string sVchrName = "Cash Receipt";

		private string sVN = "CR";

		private decimal dABalance;

		private Company company;

		private IContainer components = null;

		private Label lblHead;

		private Label label4;

		private AshDateTimePicker dtmDate;

		private Label label3;

		private AshUpDown nudVoucherNo;

		private Label label6;

		private AshComboBox cmbParty;

		private AshComboBox cmbCashAccount;

		private Label label1;

		private AshComboBox cmbMode;

		private AshComboBox cmbSM;

		private Label label2;

		private Label label5;

		private DataGridView dgDetails;

		private Label label7;

		private AshTextBoxNormal txtAmount;

		private AshTextBoxNormal txtPaidAmount;

		private Label label8;

		private CheckBox cbDiscount;

		private AshTextBoxNormal txtDis;

		private Label lblAmountInWords;

		private Label label10;

		private AshTextBoxNormal txtNarration;

		private CheckBox cbMail;

		private CheckBox cbPrint;

		private Button btnClear;

		private Button btnMail;

		private Button btnPrint;

		private Button btnDelete;

		private Button btnEdit;

		private Button btnSave;

		private BindingSource bdsDetails;
        private DataGridViewCheckBoxColumn col_Settile;
        private DataGridViewTextBoxColumn col_InvNo;
        private DataGridViewTextBoxColumn col_InvDate;
        private DataGridViewTextBoxColumn col_InvAmount;
        private DataGridViewTextBoxColumn col_PaidAmoun;
        private DataGridViewTextBoxColumn col_Credit;
        private DataGridViewTextBoxColumn col_Payment;
        private DataGridViewTextBoxColumn col_Balance;
        private ToolTip toolTip;

		public UserPrevilegeDtl plgDtls { get; set; }

		public PaymentAndReceiptView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			col_InvAmount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_PaidAmoun.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Payment.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Credit.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Balance.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			txtDis.Format = CommonSettings.strCurencyFormat;
		}

		private void DisableMethod()
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			btnEdit.Enabled = false;
			btnPrint.Enabled = false;
			btnMail.Enabled = false;
			btnDelete.Enabled = false;
		}

		private void EnableMethod()
		{
			foreach (Control control in base.Controls)
			{
				if (control.GetType() != typeof(Label))
				{
					control.Enabled = false;
				}
			}
			nudVoucherNo.Enabled = true;
			btnEdit.Enabled = true;
			btnDelete.Enabled = true;
			btnClear.Enabled = true;
			btnEnd.Enabled = true;
			btnPrint.Enabled = true;
			btnMail.Enabled = true;
		}

		private void EntityLoadMethod(VchrTranLog vchrTranLog, int iVchrNo, decimal dmlAmnt, string sVchrType, string sVchrName, int iFromAcctId, int iToAcctId, string sNarration)
		{
			vchrTranLog.VchrDate = dtmDate.Value;
			vchrTranLog.VchrTranId = iVchrTranId;
			vchrTranLog.VchrNo = iVchrNo;
			vchrTranLog.VchrAmount = dmlAmnt;
			vchrTranLog.VchrType = sVchrType;
			vchrTranLog.VchrName = sVchrName;
			vchrTranLog.SalesManId = cmbSM.SelectedValue.ToInt32();
			vchrTranLog.FromAccountId = iFromAcctId;
			vchrTranLog.ToAccountId = iToAcctId;
			vchrTranLog.TranDate = DateTime.Now.Date;
			vchrTranLog.UserId = Settings.Default.intLogOnId;
			vchrTranLog.Narration = sNarration;
			vchrTranLog.FK_BranchId = Settings.Default.iBranchId;
			vchrTranLog.SequenceNo = Utility.uSerialNumber;
			vchrTranLog.Terminal = Utility.uTerminal;
		}

		private void DiscountMethod(int iVchrNo)
		{
			string text = sVN + iVchrNo;
			if (cmbMode.SelectedIndex == 0)
			{
				if (!cbDiscount.Checked || !(txtDis.Value > 0m))
				{
					return;
				}
				if (dABalance > txtPaidAmount.Value)
				{
					int iFromAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName='OTHER DISCOUNT GIVEN'");
					int iVchrNo2 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Credit Note'", isVoucherNo: true);
					EntityLoadMethod(vchrTranLog, iVchrNo2, txtDis.Value, "Credit Note", "Credit Note", iFromAcctId, cmbParty.SelectedValue.ToInt32(), text);
				}
				else
				{
					int iToAcctId = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName='OTHER DISCOUNT RECEIVED'");
					int iVchrNo3 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Debit Note'", isVoucherNo: true);
					EntityLoadMethod(vchrTranLog, iVchrNo3, txtDis.Value, "Debit Note", "Debit Note", cmbParty.SelectedValue.ToInt32(), iToAcctId, text);
				}
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType
						select x).FirstOrDefault().VchrTranId.ToInt32();
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value), text);
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value), text);
				}
			}
			else
			{
				if (!cbDiscount.Checked || !(txtDis.Value > 0m))
				{
					return;
				}
				if (dABalance > txtPaidAmount.Value)
				{
					int iFromAcctId2 = AshproCommon.IdFindMethod("Select AccountId From Account  Where AccountName='OTHER DISCOUNT RECEIVED'");
					int iVchrNo4 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Debit Note'", isVoucherNo: true);
					vchrTranLog = new VchrTranLog();
					EntityLoadMethod(vchrTranLog, iVchrNo4, txtDis.Value, "Debit Note", "Debit Note", iFromAcctId2, cmbParty.SelectedValue.ToInt32(), text);
				}
				else
				{
					int iToAcctId2 = AshproCommon.IdFindMethod("Select AccountId From Account Where AccountName ='OTHER DISCOUNT GIVEN'");
					int iVchrNo5 = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from VchrTranLog where VchrType='Credit Note'", isVoucherNo: true);
					vchrTranLog = new VchrTranLog();
					EntityLoadMethod(vchrTranLog, iVchrNo5, txtDis.Value, "Credit Note", "Credit Note", cmbParty.SelectedValue.ToInt32(), iToAcctId2, text);
				}
				vchrTranLog.VchrTranId = null;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					iVchrTranId = (from x in db.VchrTransaction()
						where x.VchrNo == vchrTranLog.VchrNo && x.VchrType == vchrTranLog.VchrType && x.FK_BranchId == Settings.Default.iBranchId
						select x).FirstOrDefault().VchrTranId.ToInt32();
					POSHelper.TranLogMethod(0m, vchrTranLog.VchrAmount, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", null, AniHelper.GetDate(dtmDate.Value), text);
					POSHelper.TranLogMethod(vchrTranLog.VchrAmount, 0m, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), iVchrTranId.ToInt32(), vchrTranLog.VchrNo.ToInt32(), vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", null, AniHelper.GetDate(dtmDate.Value), text);
				}
			}
		}

		private void DeleteSubMethod()
		{
			DataTable dataTable = new DataTable();
			string[] obj = new string[7] { "Select TranId From TransactionLog Where VchrId='", null, null, null, null, null, null };
			int? num = iVchrTranId;
			obj[1] = num.ToString();
			obj[2] = "' and VchrType='";
			obj[3] = sVType;
			obj[4] = "'and VchrName='";
			obj[5] = sVchrName;
			obj[6] = "'";
			dataTable = AshproCommon.GetDataTable(string.Concat(obj));
			foreach (DataRow row in dataTable.Rows)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", row[0].ToString().ToInt32());
			}
			foreach (ReceiptAndPayment x in receiptAndPayments)
			{
				if (x.InvoiceType == "Sales")
				{
					SalesMaster salesMaster = entSalesMasters.FirstOrDefault((SalesMaster y) => y.SalesInvId == x.InvoiceId);
					salesMaster.IsSettiled = false;
					salesMaster.TotalPayment -= x.Payment;
					salesMaster.VoucherNumber = string.Empty;
					AshproCommon.UpdateToDatabaseObj(salesMaster, "SalesEstimateMaster", "SalesInvId", salesMaster.SalesInvId.toInt32());
				}
				else
				{
					PurchaseInvoiceHdr purchaseInvoiceHdr = entPurchases.FirstOrDefault((PurchaseInvoiceHdr y) => y.PurInvId == x.InvoiceId);
					purchaseInvoiceHdr.IsSettiled = false;
					purchaseInvoiceHdr.TotalPayment -= x.Payment;
					purchaseInvoiceHdr.VoucherNumber = string.Empty;
					AshproCommon.UpdateToDatabaseObj(purchaseInvoiceHdr, "PurchaseInvoiceHdr", "PurInvId", purchaseInvoiceHdr.PurInvId.toInt32());
				}
				AshproCommon.DeleteFromDatabase("ReceiptAndPayment", "Id", x.Id.ToInt32());
			}
			DltDiscount();
		}

		private void DltDiscount()
		{
			string narration = sVN + nudVoucherNo.Value;
			List<TransactionLog> list = db.TransactionLogs(new
			{
				Narration = narration
			});
			foreach (TransactionLog item in list)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item.TranId.toInt32());
			}
			VchrTranLog vchrTranLog = db.VchrTransaction(new
			{
				VchrName = sVchrName,
				VchrNo = nudVoucherNo.Value.ToInt32()
			}).FirstOrDefault();
			AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", vchrTranLog.VchrTranId.ToInt32());
		}

		private void LoadDGV()
		{
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			dgDetails.DataSource = null;
			bdsDetails.DataSource = receiptAndPayments;
			dgDetails.DataSource = bdsDetails;
		}

		private void PaymentAndReceiptView_Load(object sender, EventArgs e)
		{
			AniHelper.FillPartyAndSupplier(cmbParty);
			AniHelper.FillCashAndBankAccount(cmbCashAccount);
			AniHelper.FillEmployees(cmbSM);
			company = new Company();
			company = db.Companies().FirstOrDefault();
			dtmDate.Value = DateTime.Now.Date;
			cmbMode.SelectedIndex = 0;
			nudVoucherNo.Value = (db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sVchrName
			}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
		}

		private void cmbParty_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtAmount.Text = AniHelper.AccountBalanceFindMethod(cmbParty.SelectedValue.ToInt32(), AniHelper.GetDate(dtmDate.Value.AddHours(24.0).AddSeconds(-1.0)));
			string obj = txtAmount.Text.Remove(txtAmount.Text.Length - 3, 3);
			dABalance = obj.ToDecimal();
			entSalesMasters = new List<SalesMaster>();
			receiptAndPayments = new List<ReceiptAndPayment>();
			if (cmbMode.SelectedIndex == 0)
			{
				entSalesMasters = db.SalesMasters(new
				{
					PartyAccountId = cmbParty.SelectedValue.ToInt32(),
					IsSettiled = false,
					FK_BranchId = Settings.Default.iBranchId
				}).ToList();
				foreach (SalesMaster entSalesMaster in entSalesMasters)
				{
					ReceiptAndPayment receiptAndPayment = entSalesMaster.CopyTo<ReceiptAndPayment>();
					receiptAndPayment.InvoiceId = entSalesMaster.SalesInvId;
					receiptAndPayment.Balance = entSalesMaster.Credit - entSalesMaster.TotalPayment;
					receiptAndPayment.InvoiceType = "Sales";
					receiptAndPayments.Add(receiptAndPayment);
				}
			}
			else
			{
				entPurchases = db.PurchaseInvoiceHdrs(new
				{
					PartyAccId = cmbParty.SelectedValue.ToInt32(),
					IsSettiled = false
				}).ToList();
				foreach (PurchaseInvoiceHdr entPurchase in entPurchases)
				{
					ReceiptAndPayment item = new ReceiptAndPayment
					{
						InvoiceId = entPurchase.PurInvId,
						IsSettiled = entPurchase.IsSettiled,
						CntrInvNo = entPurchase.RefInvNo,
						SalesDate = entPurchase.PurDate,
						GrandDiscount = entPurchase.TotalAmnt,
						Credit = entPurchase.Credit,
						Balance = entPurchase.Credit - entPurchase.TotalPayment,
						InvoiceType = "Purchase"
					};
					receiptAndPayments.Add(item);
				}
			}
			LoadDGV();
		}

		private void txtPaidAmount_Validated(object sender, EventArgs e)
		{
			decimal num = txtPaidAmount.Value;
			txtDis.Value = Math.Abs(dABalance - num);
			foreach (ReceiptAndPayment receiptAndPayment in receiptAndPayments)
			{
				if (num >= receiptAndPayment.Credit - receiptAndPayment.TotalPayment)
				{
					receiptAndPayment.Payment = receiptAndPayment.Credit;
					receiptAndPayment.TotalPayment = receiptAndPayment.Credit;
					receiptAndPayment.Balance = 0m;
					num -= receiptAndPayment.Credit;
					receiptAndPayment.IsSettiled = true;
				}
				else
				{
					receiptAndPayment.Payment = num;
					receiptAndPayment.TotalPayment += num;
					receiptAndPayment.IsSettiled = false;
					receiptAndPayment.Balance = receiptAndPayment.Credit - receiptAndPayment.TotalPayment;
					num = default(decimal);
				}
			}
			dgDetails.Refresh();
			if (txtPaidAmount.Value > 0m)
			{
				lblAmountInWords.Text = "Amount in Words: " + AniHelper.GetAmountInWords(Math.Round(txtAmount.Value).ToInt32()) + " Only.";
			}
			else
			{
				lblAmountInWords.Text = "Amount in Words: ";
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			DisableMethod();
			cbDiscount.Enabled = false;
			txtDis.Enabled = false;
			dtmDate.Focus();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			cbDiscount.Enabled = true;
			iVchrTranId = null;
			dABalance = default(decimal);
			dtmDate.Value = DateTime.Now.Date;
			lblAmountInWords.Text = "Amount in Words:";
			nudVoucherNo.Value = (db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sVchrName
			}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
			entSalesMasters = new List<SalesMaster>();
			receiptAndPayments = new List<ReceiptAndPayment>();
			LoadDGV();
			DisableMethod();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtPaidAmount.Value <= 0m)
			{
				Messages.ErrorMessage("The amount paid must be greater than zero");
				return;
			}
			vchrTranLog = new VchrTranLog();
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			int VchrNo = 0;
			if (!iVchrTranId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
					return;
				}
				VchrNo = (db.VchrTransaction(new
				{
					VchrType = sVType,
					VchrName = sVchrName
				}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
				if (cmbMode.SelectedIndex == 0)
				{
					EntityLoadMethod(vchrTranLog, VchrNo, txtPaidAmount.Value, sVType, sVchrName, cmbParty.SelectedValue.ToInt32(), cmbCashAccount.SelectedValue.ToInt32(), txtNarration.Text + "#" + txtAmount.Text);
				}
				else
				{
					EntityLoadMethod(vchrTranLog, VchrNo, txtPaidAmount.Value, sVType, sVchrName, cmbCashAccount.SelectedValue.ToInt32(), cmbParty.SelectedValue.ToInt32(), txtNarration.Text + "#" + txtAmount.Text);
				}
				num = default(decimal);
				num2 = txtPaidAmount.Value;
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					vchrTranLog = (from x in db.VchrTransaction()
						where x.VchrNo == VchrNo && x.VchrType == sVType && x.VchrName == sVchrName
						select x).FirstOrDefault();
					POSHelper.TranLogMethod(num, num2, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
					POSHelper.TranLogMethod(num2, num, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
					InvoiceUpdateMethod(vchrTranLog.VchrNo);
				}
				DiscountMethod(VchrNo);
			}
			else
			{
				VchrNo = vchrTranLog.VchrNo.toInt32();
				if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
					return;
				}
				if (cmbMode.SelectedIndex == 0)
				{
					EntityLoadMethod(vchrTranLog, vchrTranLog.VchrNo.toInt32(), txtPaidAmount.Value, sVType, sVchrName, cmbParty.SelectedValue.ToInt32(), cmbCashAccount.SelectedValue.ToInt32(), txtNarration.Text);
					num = default(decimal);
					num2 = txtPaidAmount.Value;
				}
				else
				{
					EntityLoadMethod(vchrTranLog, vchrTranLog.VchrNo.toInt32(), txtPaidAmount.Value, sVType, sVchrName, cmbCashAccount.SelectedValue.ToInt32(), cmbParty.SelectedValue.ToInt32(), txtNarration.Text);
					num = txtPaidAmount.Value;
					num2 = default(decimal);
				}
				DeleteSubMethod();
				if (AshproCommon.InsertToDatabaseObj(vchrTranLog, "VchrTranLog"))
				{
					vchrTranLog = (from x in db.VchrTransaction()
						where x.VchrNo == nudVoucherNo.Value.ToInt32() && x.VchrType == sVType && x.VchrName == sVchrName
						select x).FirstOrDefault();
					POSHelper.TranLogMethod(num, num2, vchrTranLog.FromAccountId.toInt32(), vchrTranLog.ToAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Main", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
					POSHelper.TranLogMethod(num2, num, vchrTranLog.ToAccountId.toInt32(), vchrTranLog.FromAccountId.toInt32(), vchrTranLog.VchrTranId.ToInt32(), vchrTranLog.VchrNo, vchrTranLog.VchrType, vchrTranLog.UserId.toInt32(), "Sub", vchrTranLog.VchrName, AniHelper.GetDate(vchrTranLog.TranDate));
					InvoiceUpdateMethod(vchrTranLog.VchrNo);
				}
			}
			foreach (ReceiptAndPayment receiptAndPayment in receiptAndPayments)
			{
				receiptAndPayment.VchrNo = sVN + VchrNo;
				receiptAndPayment.VchrId = vchrTranLog.VchrTranId;
			}
			AshproCommon.InsertToDatabase(receiptAndPayments.Cast<object>().ToList());
			if (cbPrint.Checked)
			{
				btnPrint_Click(null, null);
			}
			btnClear_Click(null, null);
		}

		private void InvoiceUpdateMethod(int? iInvNo)
		{
			if (cmbMode.SelectedIndex == 0)
			{
				foreach (ReceiptAndPayment item in receiptAndPayments)
				{
					SalesMaster salesMaster = entSalesMasters.FirstOrDefault((SalesMaster x) => x.SalesInvId == item.InvoiceId);
					salesMaster.IsSettiled = item.IsSettiled;
					salesMaster.TotalPayment = item.TotalPayment;
					salesMaster.VoucherNumber = sVN + iInvNo;
					AshproCommon.UpdateToDatabaseObj(salesMaster, "SalesEstimateMaster", "SalesInvId", salesMaster.SalesInvId.toInt32());
				}
				return;
			}
			foreach (ReceiptAndPayment item2 in receiptAndPayments)
			{
				PurchaseInvoiceHdr purchaseInvoiceHdr = entPurchases.FirstOrDefault((PurchaseInvoiceHdr x) => x.PurInvId == item2.InvoiceId);
				purchaseInvoiceHdr.IsSettiled = item2.IsSettiled;
				purchaseInvoiceHdr.TotalPayment = item2.TotalPayment;
				purchaseInvoiceHdr.VoucherNumber = sVN + iInvNo;
				AshproCommon.UpdateToDatabaseObj(purchaseInvoiceHdr, "PurInvMaster", "PurInvId", purchaseInvoiceHdr.PurInvId.toInt32());
			}
		}

		private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbMode.SelectedIndex == 0)
			{
				sVType = "Receipt Voucher";
				sVchrName = "Cash Receipt";
				sVN = "CR";
			}
			else
			{
				sVType = "Payment Voucher";
				sVchrName = "Cash Payment";
				sVN = "CP";
			}
			nudVoucherNo.Value = (db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sVchrName
			}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
		}

		private void cmbCashAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			AccountEntity accountEntity = db.Accounts(new
			{
				AccountId = cmbCashAccount.SelectedValue.ToInt32()
			}).FirstOrDefault();
			AccountGroupEntity accountGroupEntity = db.AccountGroup(new { accountEntity.AGroupId }).FirstOrDefault();
			if (cmbMode.SelectedIndex == 0)
			{
				if (accountGroupEntity.GroupName == "CASH IN HAND")
				{
					sVchrName = "Cash Receipt";
					sVN = "CR";
				}
				else
				{
					sVchrName = "Bank Receipt";
					sVN = "BR";
				}
			}
			else if (accountGroupEntity.GroupName == "CASH IN HAND")
			{
				sVchrName = "Cash Payment";
				sVN = "CP";
			}
			else
			{
				sVchrName = "Bank Payment";
				sVN = "BP";
			}
			nudVoucherNo.Value = (db.VchrTransaction(new
			{
				VchrType = sVType,
				VchrName = sVchrName
			}).Max((VchrTranLog x) => x.VchrNo) + 1) ?? 1;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
					return;
				}
				if (Messages.DeleteConfirmationMessage())
				{
					DeleteSubMethod();
					if (AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", iVchrTranId.ToInt32()))
					{
						btnClear_Click(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			btnClear_Click(null, null);
		}

		private void nudVoucherNo_ValueChanged(object sender, EventArgs e)
		{
			this.vchrTranLog = db.VchrTransaction(new
			{
				VchrNo = nudVoucherNo.Value.ToInt32(),
				VchrType = sVType,
				VchrName = sVchrName
			}).FirstOrDefault();
			if (this.vchrTranLog == null)
			{
				btnClear_Click(null, null);
				return;
			}
			cmbParty.SelectedValue = this.vchrTranLog.FromAccountId;
			cmbCashAccount.SelectedValue = this.vchrTranLog.ToAccountId;
			txtPaidAmount.Value = this.vchrTranLog.VchrAmount;
			dtmDate.Value = this.vchrTranLog.VchrDate;
			if (this.vchrTranLog.Narration.Contains("#"))
			{
				string[] array = this.vchrTranLog.Narration.Split('#');
				txtNarration.Text = array[0];
				txtAmount.Text = array[1];
				string obj = txtAmount.Text.Remove(txtAmount.Text.Length - 3, 3);
				dABalance = obj.ToDecimal();
			}
			else
			{
				txtNarration.Text = this.vchrTranLog.Narration;
			}
			iVchrTranId = this.vchrTranLog.VchrTranId;
			receiptAndPayments = new List<ReceiptAndPayment>();
			receiptAndPayments = db.ReceiptAndPayments(new
			{
				VchrNo = sVN + nudVoucherNo.Value
			}).ToList();
			if (cmbMode.SelectedIndex == 0)
			{
				entSalesMasters = db.SalesMasters(new
				{
					VoucherNumber = sVN + nudVoucherNo.Value,
					FK_BranchId = Settings.Default.iBranchId
				}).ToList();
			}
			else
			{
				entPurchases = db.PurchaseInvoiceHdrs(new
				{
					VoucherNumber = sVN + this.vchrTranLog.VchrNo,
					FK_BranchId = Settings.Default.iBranchId
				}).ToList();
			}
			VchrTranLog vchrTranLog = db.VchrTransaction(new
			{
				Narration = sVN + nudVoucherNo.Value
			}).FirstOrDefault();
			if (vchrTranLog != null)
			{
				txtDis.Value = vchrTranLog.VchrAmount;
				cbDiscount.Checked = true;
			}
			dgDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			dgDetails.DataSource = null;
			bdsDetails.DataSource = receiptAndPayments;
			dgDetails.DataSource = bdsDetails;
			EnableMethod();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			AccountEntity accountEntity = db.Accounts(new
			{
				AccountId = cmbParty.SelectedValue.ToInt32()
			}).FirstOrDefault();
			decimal num = (cbDiscount.Checked ? txtDis.Value : 0m);
			decimal num2 = dABalance - (txtPaidAmount.Value + num);
			decimal num3 = receiptAndPayments.Sum((ReceiptAndPayment x) => x.Payment);
			if (txtPaidAmount.Value > dABalance)
			{
				num2 = default(decimal);
			}
			string parameterValue = num.ToString(CommonSettings.strCurencyFormat);
			string parameterValue2 = num2.ToString(CommonSettings.strCurencyFormat);
			List<VoucherInvoice> list = new List<VoucherInvoice>();
			foreach (ReceiptAndPayment receiptAndPayment in receiptAndPayments)
			{
				VoucherInvoice item = new VoucherInvoice
				{
					InvoiceNo = receiptAndPayment.CntrInvNo,
					Balance = receiptAndPayment.Balance,
					TotalAmount = receiptAndPayment.GrandDiscount,
					PaidAmount = receiptAndPayment.Payment
				};
				list.Add(item);
			}
			DataSet dataSet = new DataSet();
			DataTable dataTable = new DataTable();
			dataTable = AniHelper.ListToDataTable(list);
			dataTable.TableName = "dtVoucherInvoice";
			dataSet.Tables.Add(dataTable);
			PrinterSettings printerSettings = new PrinterSettings();
			string sReportPath = Application.StartupPath + "\\ReportForms\\rptVoucherInvoice.rdlc";
			List<atReportParameter> list2 = new List<atReportParameter>();
			list2.Add(new atReportParameter("CurrencyFormat", CommonSettings.strCurencyFormat));
			list2.Add(new atReportParameter("CompanyName", company.InstituteName));
			list2.Add(new atReportParameter("Address", company.Address));
			list2.Add(new atReportParameter("Place", company.Place));
			list2.Add(new atReportParameter("TaxID", company.GSTNo));
			list2.Add(new atReportParameter("PhoneNo", company.MobNo));
			list2.Add(new atReportParameter("VoucherNo", nudVoucherNo.Value.ToString()));
			list2.Add(new atReportParameter("VoucherDate", dtmDate.Text));
			list2.Add(new atReportParameter("PartyName", cmbParty.Text));
			list2.Add(new atReportParameter("PartyAddress", accountEntity.AddressOne));
			list2.Add(new atReportParameter("PartyTaxId", accountEntity.TinNo));
			list2.Add(new atReportParameter("VoucherType", sVchrName));
			list2.Add(new atReportParameter("PreviousBalance", txtAmount.Text));
			list2.Add(new atReportParameter("Discount", parameterValue));
			list2.Add(new atReportParameter("AfterBalance", parameterValue2));
			list2.Add(new atReportParameter("TotalPaidAmount", num3.ToString(CommonSettings.strCurencyFormat)));
			printerSettings.PrinterName = printerSettings.PrinterName;
			DirectReportPrint directReportPrint = new DirectReportPrint();
			directReportPrint.PrintReport(sReportPath, printerSettings, dataSet, list2);
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
		}

		private void PaymentAndReceiptView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnEdit_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentAndReceiptView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHead = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtmDate = new AshUsercontrol.AshDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nudVoucherNo = new AshUsercontrol.AshUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbParty = new AshUsercontrol.AshComboBox();
            this.cmbCashAccount = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMode = new AshUsercontrol.AshComboBox();
            this.cmbSM = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.col_Settile = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_InvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_InvAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PaidAmoun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Payment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAmount = new AshUsercontrol.AshTextBoxNormal();
            this.txtPaidAmount = new AshUsercontrol.AshTextBoxNormal();
            this.label8 = new System.Windows.Forms.Label();
            this.cbDiscount = new System.Windows.Forms.CheckBox();
            this.txtDis = new AshUsercontrol.AshTextBoxNormal();
            this.lblAmountInWords = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNarration = new AshUsercontrol.AshTextBoxNormal();
            this.cbMail = new System.Windows.Forms.CheckBox();
            this.cbPrint = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bdsDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // dtmDate
            // 
            resources.ApplyResources(this.dtmDate, "dtmDate");
            this.dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmDate.Name = "dtmDate";
            this.dtmDate.TabStop = false;
            this.toolTip.SetToolTip(this.dtmDate, resources.GetString("dtmDate.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // nudVoucherNo
            // 
            resources.ApplyResources(this.nudVoucherNo, "nudVoucherNo");
            this.nudVoucherNo.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudVoucherNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVoucherNo.Name = "nudVoucherNo";
            this.nudVoucherNo.TabStop = false;
            this.toolTip.SetToolTip(this.nudVoucherNo, resources.GetString("nudVoucherNo.ToolTip"));
            this.nudVoucherNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVoucherNo.ValueChanged += new System.EventHandler(this.nudVoucherNo_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // cmbParty
            // 
            resources.ApplyResources(this.cmbParty, "cmbParty");
            this.cmbParty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbParty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbParty.EnterColor = System.Drawing.Color.LightPink;
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.LeaveColor = System.Drawing.Color.White;
            this.cmbParty.Name = "cmbParty";
            this.toolTip.SetToolTip(this.cmbParty, resources.GetString("cmbParty.ToolTip"));
            this.cmbParty.SelectedIndexChanged += new System.EventHandler(this.cmbParty_SelectedIndexChanged);
            // 
            // cmbCashAccount
            // 
            resources.ApplyResources(this.cmbCashAccount, "cmbCashAccount");
            this.cmbCashAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCashAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCashAccount.EnterColor = System.Drawing.Color.LightPink;
            this.cmbCashAccount.FormattingEnabled = true;
            this.cmbCashAccount.LeaveColor = System.Drawing.Color.White;
            this.cmbCashAccount.Name = "cmbCashAccount";
            this.toolTip.SetToolTip(this.cmbCashAccount, resources.GetString("cmbCashAccount.ToolTip"));
            this.cmbCashAccount.SelectedIndexChanged += new System.EventHandler(this.cmbCashAccount_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // cmbMode
            // 
            resources.ApplyResources(this.cmbMode, "cmbMode");
            this.cmbMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMode.EnterColor = System.Drawing.Color.LightPink;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            resources.GetString("cmbMode.Items"),
            resources.GetString("cmbMode.Items1")});
            this.cmbMode.LeaveColor = System.Drawing.Color.White;
            this.cmbMode.Name = "cmbMode";
            this.toolTip.SetToolTip(this.cmbMode, resources.GetString("cmbMode.ToolTip"));
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
            // 
            // cmbSM
            // 
            resources.ApplyResources(this.cmbSM, "cmbSM");
            this.cmbSM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSM.EnterColor = System.Drawing.Color.LightPink;
            this.cmbSM.FormattingEnabled = true;
            this.cmbSM.LeaveColor = System.Drawing.Color.White;
            this.cmbSM.Name = "cmbSM";
            this.toolTip.SetToolTip(this.cmbSM, resources.GetString("cmbSM.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Settile,
            this.col_InvNo,
            this.col_InvDate,
            this.col_InvAmount,
            this.col_PaidAmoun,
            this.col_Credit,
            this.col_Payment,
            this.col_Balance});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersVisible = false;
            this.toolTip.SetToolTip(this.dgDetails, resources.GetString("dgDetails.ToolTip"));
            // 
            // col_Settile
            // 
            this.col_Settile.DataPropertyName = "IsSettiled";
            resources.ApplyResources(this.col_Settile, "col_Settile");
            this.col_Settile.Name = "col_Settile";
            // 
            // col_InvNo
            // 
            this.col_InvNo.DataPropertyName = "CntrInvNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_InvNo.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.col_InvNo, "col_InvNo");
            this.col_InvNo.Name = "col_InvNo";
            this.col_InvNo.ReadOnly = true;
            // 
            // col_InvDate
            // 
            this.col_InvDate.DataPropertyName = "SalesDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_InvDate.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.col_InvDate, "col_InvDate");
            this.col_InvDate.Name = "col_InvDate";
            this.col_InvDate.ReadOnly = true;
            // 
            // col_InvAmount
            // 
            this.col_InvAmount.DataPropertyName = "GrandDiscount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_InvAmount.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.col_InvAmount, "col_InvAmount");
            this.col_InvAmount.Name = "col_InvAmount";
            this.col_InvAmount.ReadOnly = true;
            // 
            // col_PaidAmoun
            // 
            this.col_PaidAmoun.DataPropertyName = "TotalPayment";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_PaidAmoun.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.col_PaidAmoun, "col_PaidAmoun");
            this.col_PaidAmoun.Name = "col_PaidAmoun";
            this.col_PaidAmoun.ReadOnly = true;
            // 
            // col_Credit
            // 
            this.col_Credit.DataPropertyName = "Credit";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Credit.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.col_Credit, "col_Credit");
            this.col_Credit.Name = "col_Credit";
            this.col_Credit.ReadOnly = true;
            // 
            // col_Payment
            // 
            this.col_Payment.DataPropertyName = "Payment";
            resources.ApplyResources(this.col_Payment, "col_Payment");
            this.col_Payment.Name = "col_Payment";
            // 
            // col_Balance
            // 
            this.col_Balance.DataPropertyName = "Balance";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_Balance.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.col_Balance, "col_Balance");
            this.col_Balance.Name = "col_Balance";
            this.col_Balance.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // txtAmount
            // 
            resources.ApplyResources(this.txtAmount, "txtAmount");
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Data = 0;
            this.txtAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtAmount.Format = null;
            this.txtAmount.isAllowNegative = false;
            this.txtAmount.isAllowSpecialChar = false;
            this.txtAmount.isNumberOnly = false;
            this.txtAmount.isNumeric = false;
            this.txtAmount.LeaveColor = System.Drawing.Color.White;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.toolTip.SetToolTip(this.txtAmount, resources.GetString("txtAmount.ToolTip"));
            this.txtAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtPaidAmount
            // 
            resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
            this.txtPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPaidAmount.Data = 0;
            this.txtPaidAmount.EnterColor = System.Drawing.Color.LightPink;
            this.txtPaidAmount.Format = null;
            this.txtPaidAmount.isAllowNegative = false;
            this.txtPaidAmount.isAllowSpecialChar = false;
            this.txtPaidAmount.isNumberOnly = false;
            this.txtPaidAmount.isNumeric = true;
            this.txtPaidAmount.LeaveColor = System.Drawing.Color.White;
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.toolTip.SetToolTip(this.txtPaidAmount, resources.GetString("txtPaidAmount.ToolTip"));
            this.txtPaidAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtPaidAmount.Validated += new System.EventHandler(this.txtPaidAmount_Validated);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // cbDiscount
            // 
            resources.ApplyResources(this.cbDiscount, "cbDiscount");
            this.cbDiscount.Name = "cbDiscount";
            this.toolTip.SetToolTip(this.cbDiscount, resources.GetString("cbDiscount.ToolTip"));
            this.cbDiscount.UseVisualStyleBackColor = true;
            // 
            // txtDis
            // 
            resources.ApplyResources(this.txtDis, "txtDis");
            this.txtDis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDis.Data = 0;
            this.txtDis.EnterColor = System.Drawing.Color.LightPink;
            this.txtDis.Format = null;
            this.txtDis.isAllowNegative = false;
            this.txtDis.isAllowSpecialChar = false;
            this.txtDis.isNumberOnly = false;
            this.txtDis.isNumeric = true;
            this.txtDis.LeaveColor = System.Drawing.Color.White;
            this.txtDis.Name = "txtDis";
            this.toolTip.SetToolTip(this.txtDis, resources.GetString("txtDis.ToolTip"));
            this.txtDis.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // lblAmountInWords
            // 
            resources.ApplyResources(this.lblAmountInWords, "lblAmountInWords");
            this.lblAmountInWords.ForeColor = System.Drawing.Color.Red;
            this.lblAmountInWords.Name = "lblAmountInWords";
            this.toolTip.SetToolTip(this.lblAmountInWords, resources.GetString("lblAmountInWords.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNarration.Data = 0;
            this.txtNarration.EnterColor = System.Drawing.Color.LightPink;
            this.txtNarration.Format = null;
            this.txtNarration.isAllowNegative = false;
            this.txtNarration.isAllowSpecialChar = false;
            this.txtNarration.isNumberOnly = false;
            this.txtNarration.isNumeric = false;
            this.txtNarration.LeaveColor = System.Drawing.Color.White;
            this.txtNarration.Name = "txtNarration";
            this.toolTip.SetToolTip(this.txtNarration, resources.GetString("txtNarration.ToolTip"));
            this.txtNarration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cbMail
            // 
            resources.ApplyResources(this.cbMail, "cbMail");
            this.cbMail.Name = "cbMail";
            this.toolTip.SetToolTip(this.cbMail, resources.GetString("cbMail.ToolTip"));
            this.cbMail.UseVisualStyleBackColor = true;
            // 
            // cbPrint
            // 
            resources.ApplyResources(this.cbPrint, "cbPrint");
            this.cbPrint.Name = "cbPrint";
            this.toolTip.SetToolTip(this.cbPrint, resources.GetString("cbPrint.ToolTip"));
            this.cbPrint.UseVisualStyleBackColor = true;
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
            // btnMail
            // 
            resources.ApplyResources(this.btnMail, "btnMail");
            this.btnMail.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMail.ForeColor = System.Drawing.Color.White;
            this.btnMail.Name = "btnMail";
            this.btnMail.TabStop = false;
            this.toolTip.SetToolTip(this.btnMail, resources.GetString("btnMail.ToolTip"));
            this.btnMail.UseVisualStyleBackColor = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.TabStop = false;
            this.toolTip.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabStop = false;
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TabStop = false;
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
            // PaymentAndReceiptView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbMail);
            this.Controls.Add(this.cbPrint);
            this.Controls.Add(this.lblAmountInWords);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNarration);
            this.Controls.Add(this.cbDiscount);
            this.Controls.Add(this.txtDis);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPaidAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSM);
            this.Controls.Add(this.cmbCashAccount);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.cmbParty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudVoucherNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtmDate);
            this.Controls.Add(this.lblHead);
            this.Name = "PaymentAndReceiptView";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.PaymentAndReceiptView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaymentAndReceiptView_KeyDown);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.dtmDate, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.nudVoucherNo, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbParty, 0);
            this.Controls.SetChildIndex(this.cmbMode, 0);
            this.Controls.SetChildIndex(this.cmbCashAccount, 0);
            this.Controls.SetChildIndex(this.cmbSM, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.Controls.SetChildIndex(this.txtAmount, 0);
            this.Controls.SetChildIndex(this.txtPaidAmount, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtDis, 0);
            this.Controls.SetChildIndex(this.cbDiscount, 0);
            this.Controls.SetChildIndex(this.txtNarration, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.lblAmountInWords, 0);
            this.Controls.SetChildIndex(this.cbPrint, 0);
            this.Controls.SetChildIndex(this.cbMail, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.Controls.SetChildIndex(this.btnMail, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudVoucherNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
