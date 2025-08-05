using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class TransactionWithoutInventory : BaseForm
	{
		private DateTime estimateDate;

		private LedgerReport LdgrReport;

		private ReportForm rptForm;

		private ChequeReportView chqRpt;

		private int iVchrNo;

		private int iVchrId;

		private string strPAcctDetails = "";

		private string strRefNoDtls = "";

		private int? iAccountId;

		private DBContext db;

		private DataGridViewRow row;

		private string strVoucherType;

		private IContainer components = null;

		private GroupBox groupBox1;

		private Label lblCash;

		private Label label3;

		private AshTextBoxNormal txtCheqNoSrch;

		private Label label2;

		private AshDateTimePicker dtmFrom;

		private Label label6;

		private AshComboBox cmbVchrType;

		private Label label8;

		private AshComboBox cmbAccount;

		private Label label5;

		private AshComboBox cmbPartyAccnt;

		private Label label7;

		private AshTextBoxNormal txtAmount;

		private Label label9;

		private Button button1;

		private AshTextBoxNormal txtBranch;

		private Label label12;

		private AshTextBoxNormal txtBank;

		private Label label11;

		private Label label14;

		private Label label13;

		private AshComboBox cmbVchrTypSrch;

		private Label label1;

		private AshComboBox cmbPartyAcctSrch;

		private Label label10;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmDate;

		private Button button2;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Label label15;

		private AshTextBoxNormal txtCheqNo;

		private Label label16;

		private AshDateTimePicker dtmCheqDate;

		private Label label17;

		private AshComboBox cmbCheqStatus;

		private Label lblBank;

		private Label label18;

		private Label lblReceivable;

		private Label lblPurchase;

		private Label label22;

		private Label label28;

		private Label lblExp;

		private Label lblPayable;

		private Label label26;

		private Label label20;

		private Label lblSale;

		private Label label24;

		private ToolTip toolTip1;

		private Panel panel1;

		private Button btnAccCreate;

		private AshTextBoxNormal txtNewAcct;

		private Label label29;

		private AshComboBox cmbNewGroup;

		private Label label31;

		private AshTextBoxNormal txtNewAddress;

		private Label label34;

		private Button btnPanelClose;

		private AshComboBox cmbReport;

		private Label label32;

		private Button btnShow;

		private Label label4;

		private AshTextBoxNormal txtRefNo;

		private Label label19;

		private AshTextBoxNormal txtNarration;

		private AshComboBox cmbCrOrDr;

		private AshTextBoxNormal txtOppBlnce;

		private Label label33;

		private DataGridView dgDetails;

		private Label label35;

		private AshDateTimePicker dtmChqCollOrBnc;

		private AshDateTimePicker dtmNewAcct;

		private Label label21;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column10;

		private Button button3;

		private Label label23;

		public UserPrevilegeDtl plgDtls { get; set; }

		public TransactionWithoutInventory()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private async Task LoadGrpBalanceMethod()
		{
			Label label = lblCash;
			label.Text = await AniHelper.GetValueForChart("CASH IN HAND", AniHelper.GetDate(DateTime.Now.Date));
			Label label2 = lblBank;
			label2.Text = await AniHelper.GetValueForChart("CASH AT BANK", AniHelper.GetDate(DateTime.Now.Date));
			Label label3 = lblReceivable;
			label3.Text = await AniHelper.GetValueForChart("CUSTOMER/SUNDRY DEBTORS", AniHelper.GetDate(DateTime.Now.Date));
			Label label4 = lblPayable;
			label4.Text = await AniHelper.GetValueForChart("SUPPLIERS/SUNDRY CREDITORS", AniHelper.GetDate(DateTime.Now.Date));
			Label label5 = lblSale;
			label5.Text = await AniHelper.GetValueForChart("SALES ACCOUNT", AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), "CreditAmount");
			Label label6 = lblPurchase;
			label6.Text = await AniHelper.GetValueForChart("PURCHASE ACCOUNT", AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), "DebitAmount");
			Label label7 = lblExp;
			label7.Text = await AniHelper.GetValueForChart("INDIRECT EXPENSES", AniHelper.GetDate(dtmFrom.Value), AniHelper.GetDate(dtmTo.Value), "DebitAmount");
		}

		private void GroupBalanceFindMethodTwo(string strGroup, Label lbl, string DebtOrCrdt)
		{
			decimal num = default(decimal);
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select AccountName From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId where AccountGroup.GroupName='" + strGroup + "'");
				foreach (DataRow row in dataTable.Rows)
				{
					num += AccountBalanceFindMethodTwo(row[0].ToInt32(), DebtOrCrdt);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			if (num >= 0m)
			{
				lbl.Text = num.ToString(CommonSettings.strCurencyFormat) + "  Dr";
			}
			else
			{
				lbl.Text = Math.Abs(num).ToString(CommonSettings.strCurencyFormat) + "  Cr";
			}
		}

		private async Task ClearMethod()
		{
			AniHelper.ClearMethod(this);
			ChequeMethod(blStatus: false);
			btnSave.Text = "Save";
			label35.Visible = false;
			dtmChqCollOrBnc.Visible = false;
			cmbCheqStatus.Visible = false;
			btnDelete.ForeColor = Color.Black;
			await LoadGrpBalanceMethod();
			LivLoadMethod();
			cmbVchrType.Focus();
			cmbVchrType.SelectAll();
		}

		private void ChequeMethod(bool blStatus)
		{
			label11.Visible = blStatus;
			label12.Visible = blStatus;
			label15.Visible = blStatus;
			label16.Visible = blStatus;
			btnDelete.Enabled = blStatus;
			label17.Visible = blStatus;
			cmbCheqStatus.Visible = blStatus;
			txtBank.Visible = blStatus;
			txtBranch.Visible = blStatus;
			txtCheqNo.Visible = blStatus;
			dtmCheqDate.Visible = blStatus;
		}

		private async Task SaveMethod()
		{
			if (plgDtls != null && !plgDtls.SaveData)
			{
				Messages.ErrorMessage("You have no authorisation to Save the record. Please Contact the Admin.");
			}
			else if (txtAmount.Value > 0m)
			{
				switch (cmbVchrType.SelectedIndex)
				{
				default:
					SaveTransactionMethod(cmbVchrType.Text, 0m, txtAmount.Value);
					break;
				case 0:
					SaveTransactionMethod("Sales Voucher", 0m, txtAmount.Value);
					break;
				case 1:
					SaveTransactionMethod("Sales Return Voucher", txtAmount.Value, 0m);
					break;
				case 2:
					SaveTransactionMethod("Purchase Voucher", txtAmount.Value, 0m);
					break;
				case 3:
					SaveTransactionMethod("Purchase Return Voucher", 0m, txtAmount.Value);
					break;
				case 4:
					SaveTransactionMethod("Receipt Voucher", txtAmount.Value, 0m);
					break;
				case 5:
					SaveTransactionMethod("Receipt Voucher", txtAmount.Value, 0m);
					break;
				case 6:
					SaveTransactionMethod("Payment Voucher", 0m, txtAmount.Value);
					break;
				case 7:
					SaveTransactionMethod("Payment Voucher", 0m, txtAmount.Value);
					break;
				case 8:
					SaveTransactionMethod("Payment Voucher", 0m, txtAmount.Value);
					ChequeDetailsInsertMethod("Payment");
					break;
				case 9:
					SaveTransactionMethod("Receipt Voucher", txtAmount.Value, 0m);
					ChequeDetailsInsertMethod("Receipt");
					break;
				}
				await ClearMethod();
			}
			else
			{
				MessageBox.Show("Amount must be more than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void ChequeDetailsInsertMethod(string strType)
		{
			if (btnSave.Text == "Save")
			{
				ChequeDetails data = ChequeDetailsEntityAddMethod(strType);
				AshproCommon.InsertToDatabaseObj(data, "ChequeDetails");
				return;
			}
			int iValue = AshproCommon.IdFindMethod("Select CheqDtlsId From ChequeDetails where VchrId='" + iVchrId + "' and VchrType='" + cmbVchrType.Text + "'");
			try
			{
				if (cmbCheqStatus.SelectedIndex == 0)
				{
					ChequeDetails data2 = ChequeDetailsEntityAddMethod(strType);
					AshproCommon.UpdateToDatabaseObj(data2, "ChequeDetails", "", iValue);
					return;
				}
				ChequeDetails data3 = new ChequeDetails
				{
					Amount = txtAmount.Value,
					Bank = txtBank.Text,
					Branch = txtBranch.Text,
					CheqNo = txtCheqNo.Text,
					CheqDate = dtmCheqDate.Text,
					Status = cmbCheqStatus.Text,
					RefNo = txtRefNo.Text,
					Remarks = txtNarration.Text,
					TranDate = DateTime.Now.Date,
					JeId = Settings.Default.intLogOnId,
					CollectedDate = dtmChqCollOrBnc.Value.Date,
					Type = strType
				};
				AshproCommon.UpdateToDatabaseObj(data3, "ChequeDetails", "", iValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private ChequeDetails ChequeDetailsEntityAddMethod(string strType)
		{
			return new ChequeDetails
			{
				VchrId = iVchrNo,
				VchrNo = iVchrNo,
				VchrType = cmbVchrType.Text,
				AccountId = cmbPartyAccnt.SelectedValue.ToInt32(),
				BankAcctId = cmbAccount.SelectedValue.ToInt32(),
				Amount = txtAmount.Value,
				Bank = txtBank.Text,
				Branch = txtBranch.Text,
				CheqNo = txtCheqNo.Text,
				CheqDate = dtmCheqDate.Text,
				RefNo = txtRefNo.Text,
				Remarks = txtNarration.Text,
				TranDate = DateTime.Now.Date,
				JeId = Settings.Default.intLogOnId,
				Type = strType
			};
		}

		private void LivLoadMethod()
		{
			string text = string.Empty;
			if (dgDetails.DataSource != null)
			{
				dgDetails.DataSource = null;
			}
			else
			{
				dgDetails.Rows.Clear();
			}
			if (cmbVchrTypSrch.Text != "")
			{
				text = " and VchrName='" + cmbVchrTypSrch.Text + "'";
			}
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select distinct VchrType From TransactionLog where Status='Main' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'" + text);
				foreach (DataRow row in dataTable.Rows)
				{
					switch (row[0].ToString())
					{
					case "Purchase Voucher":
						LoadListViewMethod("Purchase Voucher", "DebitAmount");
						break;
					case "Purchase Return Voucher":
						LoadListViewMethod("Purchase Return Voucher", "CreditAmount");
						break;
					case "Sales Return Voucher":
						LoadListViewMethod("Sales Return Voucher", "DebitAmount");
						break;
					case "Sales Voucher":
						LoadListViewMethod("Sales Voucher", "CreditAmount");
						break;
					case "Payment Voucher":
						LoadListViewMethod("Payment Voucher", "CreditAmount");
						break;
					case "Receipt Voucher":
						LoadListViewMethod("Receipt Voucher", "DebitAmount");
						break;
					case "Journal Voucher":
						LoadListViewMethod("Journal Voucher", "CreditAmount");
						break;
					}
				}
				dgDetails.Sort(dgDetails.Columns[1], ListSortDirection.Ascending);
				int num = 1;
				for (int i = 0; i < dgDetails.Rows.Count; i++)
				{
					dgDetails.Rows[i].Cells[0].Value = num;
					num++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 420");
			}
		}

		private void LoadListViewMethod(string strVchrType, string strAmountClm)
		{
			string strID = null;
			string strID2 = null;
			string strID3 = null;
			int num = 1;
			if (cmbPartyAcctSrch.Text != "")
			{
				AshproCommon.IdFindMethod("Account", "AccountId", "AccountName", cmbPartyAcctSrch.Text, out strID);
				strPAcctDetails = " and OppAccountId='" + strID + "'";
			}
			if (txtCheqNoSrch.Text != "")
			{
				strRefNoDtls = " and RefNo='" + txtCheqNoSrch.Text + "'";
			}
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select VchrNo,VchrDate,VchrName,RefNo,AccountId,OppAccountId," + strAmountClm + ",Narration,VchrType From TransactionLog Where VchrType='" + strVchrType + "' and Status='Main' and VchrDate between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "' " + strPAcctDetails + strRefNoDtls);
				foreach (DataRow row in dataTable.Rows)
				{
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[4].ToString(), out strID2);
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", row[5].ToString(), out strID3);
					dgDetails.Rows.Add(num.ToString(), row[1].ToString().toDateTime().ToString("dd-MMM-yyyy"), row[2].ToString(), row[0].ToString(), row[3].ToString(), strID2, strID3, row[6].ToString(), row[7].ToString(), row[8].ToString());
					num++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 474");
			}
		}

		private void SaveTransactionMethod(string strVchrType, decimal dmlDebit, decimal dmlCredit)
		{
			iVchrNo = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from TransactionLog where VchrType='" + strVchrType + "' and VchrName='" + cmbVchrType.Text + "'", isVoucherNo: true);
			List<string> list = new List<string>();
			if (btnSave.Text == "Save")
			{
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("Select RefNo From TransactionLog Where VchrType='" + strVchrType + "' and OppAccountId='" + cmbPartyAccnt.SelectedValue.ToInt32() + "'");
				foreach (DataRow row in dataTable.Rows)
				{
					list.Add(row[0].ToString());
				}
				try
				{
					if (txtRefNo.Text != string.Empty)
					{
						if (!list.Contains(txtRefNo.Text))
						{
							POSHelper.TranLogMethod(dmlDebit, dmlCredit, cmbAccount.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Main", strVchrType, dtmDate.Text);
							POSHelper.TranLogMethod(dmlCredit, dmlDebit, cmbPartyAccnt.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Sub", strVchrType, dtmDate.Text);
						}
						else
						{
							MessageBox.Show("The Reference number you entered is already exist");
						}
					}
					else
					{
						POSHelper.TranLogMethod(dmlDebit, dmlCredit, cmbAccount.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Main", strVchrType, dtmDate.Text);
						POSHelper.TranLogMethod(dmlCredit, dmlDebit, cmbPartyAccnt.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Sub", strVchrType, dtmDate.Text);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + " 491");
					return;
				}
			}
			if (!(cmbVchrType.Text == "Cheque Payment") && !(cmbVchrType.Text == "Cheque Receipt"))
			{
				try
				{
					UpdateTranLogMethod(strVchrType, dmlDebit, dmlCredit, cmbAccount.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVchrNo, "Main");
					UpdateTranLogMethod(strVchrType, dmlCredit, dmlDebit, cmbPartyAccnt.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrNo, "Sub");
					return;
				}
				catch (Exception ex2)
				{
					MessageBox.Show(ex2.Message + "  504");
					return;
				}
			}
			try
			{
				if (cmbCheqStatus.SelectedIndex == 2)
				{
					strVchrType += " Rejected";
					POSHelper.TranLogMethod(dmlDebit, dmlCredit, cmbAccount.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Main", strVchrType, dtmDate.Text);
					POSHelper.TranLogMethod(dmlCredit, dmlDebit, cmbPartyAccnt.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrId, iVchrNo, cmbVchrType.Text, Settings.Default.intLogOnId, "Sub", strVchrType, dtmDate.Text);
				}
				else
				{
					UpdateTranLogMethod(strVchrType, dmlDebit, dmlCredit, cmbAccount.SelectedValue.ToInt32(), cmbPartyAccnt.SelectedValue.ToInt32(), iVchrNo, "Main");
					UpdateTranLogMethod(strVchrType, dmlCredit, dmlDebit, cmbPartyAccnt.SelectedValue.ToInt32(), cmbAccount.SelectedValue.ToInt32(), iVchrNo, "Sub");
				}
			}
			catch (Exception ex3)
			{
				MessageBox.Show(ex3.Message + "  504");
			}
		}

		private void UpdateTranLogMethod(string strVchrType, decimal dmlDebit, decimal dmlCredit, int iOnAccountId, int iOpAccountId, int intVouchrNo, string strRemarks)
		{
			string text = "";
			string text2 = "";
			DateTime vchrDate;
			if (strVchrType == "Opening Voucher")
			{
				vchrDate = dtmNewAcct.Value;
				text = "Opening Account Entry";
				text2 = strVchrType;
			}
			else
			{
				vchrDate = dtmDate.Value.Date;
				text = txtNarration.Text;
				text2 = cmbVchrType.Text;
			}
			TransactionLog transactionLog = new TransactionLog
			{
				VchrId = iVchrId,
				VchrNo = iVchrNo,
				VchrType = strVchrType,
				VchrDate = vchrDate,
				AccountId = iOnAccountId,
				OppAccountId = iOpAccountId,
				DebitAmount = dmlDebit,
				CreditAmount = dmlCredit,
				Narration = text,
				UserID = Settings.Default.intLogOnId,
				Status = strRemarks,
				TranDate = DateTime.Now.Date,
				RefNo = txtRefNo.Text,
				VchrName = text2
			};
			foreach (TransactionLog item in (from x in db.TransactionLogs()
				where x.VchrNo == iVchrNo && x.VchrType == strVoucherType && x.Status == strRemarks
				select x).ToList())
			{
				transactionLog.TranId = item.TranId;
				AshproCommon.UpdateToDatabaseObj(transactionLog, "TransactionLog", "TranId", item.TranId.toInt32());
			}
		}

		private decimal AccountBalanceFindMethod(string strAccountName)
		{
			return AshproCommon.ValueFindMethod("SELECT SUM(DebitAmount) as debitTotal,SUM(CreditAmount) as creditTotal FROM TransactionLog where AccountId='" + cmbAccount.SelectedValue.ToInt32() + "' and VchrDate <= '" + dtmDate.Text + "'");
		}

		private decimal AccountBalanceFindMethodTwo(int iAccountId, string strDebitOrCredit)
		{
			return AshproCommon.ValueFindMethod("SELECT SUM(" + strDebitOrCredit + ") as debitTotal FROM TransactionLog where AccountId='" + iAccountId + "' and VchrDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'");
		}

		private void AccountCreatMethod()
		{
			AccGrpMethod();
			panel1.Visible = true;
			panel1.BringToFront();
			panel1.Location = new Point(326, 11);
			txtNewAcct.Text = cmbPartyAccnt.Text;
			txtNewAcct.Focus();
			txtNewAcct.SelectAll();
		}

		private void AccGrpMethod()
		{
			cmbNewGroup.Items.Clear();
			switch (cmbVchrType.SelectedIndex)
			{
			default:
				cmbNewGroup.Items.Add("CUSTOMER/SUNDRY DEBTORS");
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				break;
			case 0:
				cmbNewGroup.Items.Add("CUSTOMER/SUNDRY DEBTORS");
				break;
			case 1:
				cmbNewGroup.Items.Add("CUSTOMER/SUNDRY DEBTORS");
				break;
			case 2:
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				break;
			case 3:
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				break;
			case 4:
				cmbNewGroup.Items.Add("CUSTOMER/SUNDRY DEBTORS");
				cmbNewGroup.Items.Add("INDIRECT INCOME");
				cmbNewGroup.Items.Add("CAPITAL ACCOUNT");
				cmbNewGroup.Items.Add("DIRECT INCOME");
				break;
			case 5:
				cmbNewGroup.Items.Add("CUSTOMER/SUNDRY DEBTORS");
				cmbNewGroup.Items.Add("INDIRECT INCOME");
				cmbNewGroup.Items.Add("CAPITAL ACCOUNT");
				cmbNewGroup.Items.Add("DIRECT INCOME");
				break;
			case 6:
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				cmbNewGroup.Items.Add("INDIRECT EXPENSES");
				cmbNewGroup.Items.Add("DRAWINGS ACCOUNT");
				cmbNewGroup.Items.Add("DIRECT EXPENSES");
				break;
			case 7:
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				cmbNewGroup.Items.Add("INDIRECT EXPENSES");
				cmbNewGroup.Items.Add("DRAWINGS ACCOUNT");
				cmbNewGroup.Items.Add("DIRECT EXPENSES");
				break;
			case 8:
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				cmbNewGroup.Items.Add("SUPPLIERS/SUNDRY CREDITORS");
				break;
			}
			if (cmbNewGroup.Items.Count > 0)
			{
				cmbNewGroup.SelectedIndex = 0;
			}
		}

		private void AccountAddMethod()
		{
			if (txtNewAcct.Text != "")
			{
				AccountEntity data = new AccountEntity
				{
					AGroupId = cmbNewGroup.SelectedValue.ToInt32(),
					AccountName = txtNewAcct.Text,
					OpeningBalance = 0m,
					DebitOrCridt = "DR",
					AddressOne = txtNewAddress.Text
				};
				if (!db.Accounts().Any((AccountEntity x) => x.AccountName == txtNewAcct.Text))
				{
					AshproCommon.InsertToDatabaseObj(data, "Account");
					return;
				}
				int iValue = AshproCommon.IdFindMethod("Select AccountId From Account where AccountName='" + txtNewAcct.Text + "'");
				AshproCommon.UpdateToDatabaseObj(data, "Account", "AccountId", iValue);
			}
			else
			{
				Messages.ErrorMessage("Please Enter Details");
			}
		}

		private void ReportShowMethod()
		{
			if (cmbReport.SelectedIndex == 8)
			{
				chqRpt = new ChequeReportView(cmbReport.Text);
				chqRpt.MdiParent = base.MdiParent;
				chqRpt.Show();
			}
			else if (cmbReport.SelectedIndex == 9)
			{
				chqRpt = new ChequeReportView(cmbReport.Text);
				chqRpt.MdiParent = base.MdiParent;
				chqRpt.Show();
			}
			else if (cmbReport.SelectedIndex == 10)
			{
				LdgrReport = new LedgerReport();
				LdgrReport.MdiParent = base.MdiParent;
				LdgrReport.Show();
			}
			else
			{
				rptForm = new ReportForm(cmbReport.Text);
				rptForm.MdiParent = base.MdiParent;
				rptForm.Show();
			}
		}

		private void LoadChequeDetails(int iChequeDtlsId)
		{
			try
			{
				ChequeDetails chequeDetails = (from x in db.ChequeDetails()
					where x.CheqDtlsId == iChequeDtlsId
					select x).FirstOrDefault();
				txtBank.Text = chequeDetails.Bank;
				txtBranch.Text = chequeDetails.Branch;
				txtCheqNo.Text = chequeDetails.CheqNo;
				dtmCheqDate.Text = chequeDetails.CheqDate;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			estimateDate = dtmFrom.Value;
		}

		private async void TransactionWithoutInventory_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			base.WindowState = FormWindowState.Normal;
			estimateDate = Settings.Default.appDate;
			LivLoadMethod();
			await LoadGrpBalanceMethod();
			cmbVchrType.SelectedIndex = 0;
			AniHelper.FillAccount(cmbPartyAcctSrch);
		}

		private async void btnClear_Click(object sender, EventArgs e)
		{
			await ClearMethod();
		}

		private async void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else
				{
					if (!Messages.DeleteConfirmationMessage())
					{
						return;
					}
					if (cmbVchrType.Text == "Cheque Payment" || cmbVchrType.Text == "Cheque Receipt")
					{
						int iChequeDtlsId = AshproCommon.IdFindMethod("Select CheqDtlsId From ChequeDetails where VchrId='" + iVchrId + "' and VchrType='" + cmbVchrType.Text + "'");
						AshproCommon.DeleteFromDatabase("ChequeDetails", "CheqDtlsId", iChequeDtlsId);
					}
					foreach (TransactionLog dr in (from x in db.TransactionLogs()
						where x.VchrId == iVchrId && x.VchrType == strVoucherType
						select x).ToList())
					{
						AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", dr.TranId.toInt32());
					}
					await ClearMethod();
				}
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void cmbVchrType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cmbVchrType.SelectedIndex)
			{
			default:
				AniHelper.FillPartyAndSupplier(cmbAccount);
				AniHelper.FillPartyAndSupplier(cmbPartyAccnt);
				break;
			case 0:
				ChequeMethod(blStatus: false);
				AniHelper.FillSalesAccount(cmbAccount);
				AniHelper.FillAccount(cmbPartyAccnt);
				break;
			case 1:
				ChequeMethod(blStatus: false);
				AniHelper.FillSalesRtnAccount(cmbAccount);
				AniHelper.FillAccount(cmbPartyAccnt);
				break;
			case 2:
				ChequeMethod(blStatus: false);
				AniHelper.FillPurchaseAccount(cmbAccount);
				AniHelper.FillAccountForPurchase(cmbPartyAccnt);
				break;
			case 3:
				ChequeMethod(blStatus: false);
				AniHelper.FillPurchaseRtnAccount(cmbAccount);
				AniHelper.FillAccountForPurchase(cmbPartyAccnt);
				break;
			case 4:
				ChequeMethod(blStatus: false);
				AniHelper.FillCashAccount(cmbAccount);
				AniHelper.FillAllAccount(cmbPartyAccnt);
				break;
			case 5:
				ChequeMethod(blStatus: false);
				AniHelper.FillBankAccount(cmbAccount);
				AniHelper.FillAllAccount(cmbPartyAccnt);
				break;
			case 6:
				ChequeMethod(blStatus: false);
				AniHelper.FillCashAccount(cmbAccount);
				AniHelper.FillAllAccount(cmbPartyAccnt);
				break;
			case 7:
				ChequeMethod(blStatus: false);
				AniHelper.FillBankAccount(cmbAccount);
				AniHelper.FillAllAccount(cmbPartyAccnt);
				break;
			case 8:
				ChequeMethod(blStatus: true);
				AniHelper.FillBankAccount(cmbAccount);
				AniHelper.FillPartyAndSupplier(cmbPartyAccnt);
				break;
			case 9:
				ChequeMethod(blStatus: true);
				AniHelper.FillBankAccount(cmbAccount);
				AniHelper.FillPartyAndSupplier(cmbPartyAccnt);
				break;
			}
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			await SaveMethod();
		}

		private async void TransactionWithoutInventory_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				await SaveMethod();
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				await ClearMethod();
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				if (txtAmount.Text != "")
				{
					e.SuppressKeyPress = true;
					btnDelete_Click(null, null);
				}
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbVchrTypSrch.Focus();
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				e.SuppressKeyPress = true;
				estimateDate = dtmDate.Value.Date;
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				AccountCreatMethod();
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbReport.Focus();
				if (cmbReport.Text != string.Empty)
				{
					cmbReport.SelectAll();
				}
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (dgDetails.Rows.Count > 0)
				{
					dgDetails.Focus();
					dgDetails.CurrentCell = dgDetails.Rows[0].Cells[0];
				}
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AccountCreatMethod();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			txtNewAcct.Text = string.Empty;
			txtNewAddress.Text = string.Empty;
			cmbNewGroup.Text = string.Empty;
			txtAmount.Focus();
			txtAmount.SelectAll();
			panel1.Visible = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			AccountAddMethod();
			try
			{
				string strID = null;
				strVoucherType = "Opening Voucher";
				AshproCommon.IdFindMethod("Account", "AccountId", "AccountName", txtNewAcct.Text, out strID);
				iVchrNo = AshproCommon.IdFindMethod("select max(VchrNo) as maxValue from TransactionLog where VchrType='" + strVoucherType + "' and VchrName='" + cmbVchrType.Text + "'", isVoucherNo: true);
				if (cmbCrOrDr.Text == "DR")
				{
					POSHelper.TranLogMethod(txtOppBlnce.Value, 0m, strID.ToInt32(), strID.ToInt32(), iVchrId, iVchrNo, strVoucherType, Settings.Default.intLogOnId, "Sub", strVoucherType, dtmDate.Text);
				}
				else
				{
					POSHelper.TranLogMethod(0m, txtOppBlnce.Value, strID.ToInt32(), strID.ToInt32(), iVchrId, iVchrNo, strVoucherType, Settings.Default.intLogOnId, "Sub", strVoucherType, dtmDate.Text);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			txtNewAcct.Text = string.Empty;
			txtNewAddress.Text = string.Empty;
			cmbNewGroup.Text = string.Empty;
			txtAmount.Focus();
			txtAmount.SelectAll();
			cmbPartyAccnt.Text = txtNewAcct.Text;
			panel1.Visible = false;
		}

		private void cmbVchrTypSrch_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cmbVchrTypSrch.SelectedIndex)
			{
			case 0:
				AniHelper.FillAccount(cmbPartyAcctSrch);
				break;
			case 1:
				AniHelper.FillAccount(cmbPartyAcctSrch);
				break;
			case 2:
				AniHelper.FillAccountForPurchase(cmbPartyAcctSrch);
				break;
			case 3:
				AniHelper.FillAccountForPurchase(cmbPartyAcctSrch);
				break;
			default:
				AniHelper.FillAllAccount(cmbPartyAcctSrch);
				break;
			case 8:
				AniHelper.FillPartyAndSupplier(cmbPartyAcctSrch);
				break;
			case 9:
				AniHelper.FillPartyAndSupplier(cmbPartyAcctSrch);
				break;
			}
			LivLoadMethod();
		}

		private async void dtmFrom_ValueChanged(object sender, EventArgs e)
		{
			LivLoadMethod();
			await LoadGrpBalanceMethod();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			ReportShowMethod();
		}

		private async void txtNarration_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (!txtBank.Visible)
				{
					await SaveMethod();
					return;
				}
				txtBank.Focus();
				txtBank.SelectAll();
			}
			else if (e.KeyCode == Keys.Up && e.Control)
			{
				txtAmount.Focus();
				txtAmount.SelectAll();
			}
		}

		private void cmbPartyAcctSrch_SelectedIndexChanged(object sender, EventArgs e)
		{
			LivLoadMethod();
		}

		private void txtCheqNoSrch_TextChanged(object sender, EventArgs e)
		{
			LivLoadMethod();
		}

		private void cmbReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				ReportShowMethod();
			}
			else if (e.KeyCode == Keys.Up && e.Control)
			{
				dtmDate.Focus();
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					row = dgDetails.Rows[e.RowIndex];
					dtmDate.Text = row.Cells[1].Value.ToString();
					cmbVchrType.Text = row.Cells[2].Value.ToString();
					iVchrId = row.Cells[3].Value.ToInt32();
					txtRefNo.Text = row.Cells[4].Value.ToString();
					cmbAccount.Text = row.Cells[5].Value.ToString();
					cmbPartyAccnt.Text = row.Cells[6].Value.ToString();
					txtAmount.Text = row.Cells[7].Value.ToString();
					txtNarration.Text = row.Cells[8].Value.ToString();
					strVoucherType = row.Cells[9].Value.ToString();
				}
				if (!(cmbVchrType.Text == "Cheque Payment") && !(cmbVchrType.Text == "Cheque Receipt"))
				{
					ChequeMethod(blStatus: false);
					label17.Visible = false;
					cmbCheqStatus.Visible = false;
				}
				else
				{
					label17.Visible = true;
					cmbCheqStatus.Visible = true;
					int iChequeDtlsId = AshproCommon.IdFindMethod("Select CheqDtlsId From ChequeDetails where VchrId='" + iVchrId + "' and VchrType='" + cmbVchrType.Text + "'");
					LoadChequeDetails(iChequeDtlsId);
				}
			}
			catch (Exception)
			{
			}
			btnSave.Text = "Update";
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			cmbVchrType.Focus();
			cmbVchrType.SelectAll();
		}

		private async void cmbCheqStatus_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				await SaveMethod();
			}
			else if (e.KeyCode == Keys.Up && e.Control)
			{
				dtmCheqDate.Focus();
			}
		}

		private void cmbCheqStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbCheqStatus.SelectedIndex == 0)
			{
				label35.Visible = false;
				dtmChqCollOrBnc.Visible = false;
			}
			else
			{
				label35.Visible = true;
				dtmChqCollOrBnc.Visible = true;
			}
		}

		private void dtmChqCollOrBnc_ValueChanged(object sender, EventArgs e)
		{
			dtmDate.Text = dtmChqCollOrBnc.Text;
		}

		private void panel1_VisibleChanged(object sender, EventArgs e)
		{
			if (txtNewAcct.Text != string.Empty && cmbNewGroup.Text != string.Empty)
			{
				AniHelper.FillAccountFromParameter(cmbPartyAccnt, cmbNewGroup.Text);
				cmbPartyAccnt.Text = txtNewAcct.Text;
				cmbPartyAccnt.Focus();
				cmbPartyAccnt.SelectAll();
			}
		}

		private void txtNewAcct_Validated(object sender, EventArgs e)
		{
			iAccountId = ((!(from x in db.Accounts()
				where x.AccountName == txtNewAcct.Text
				select x).FirstOrDefault().AccountId.HasValue) ? null : (from x in db.Accounts()
				where x.AccountName == txtNewAcct.Text
				select x).FirstOrDefault().AccountId);
		}

		private void TransactionWithoutInventory_Activated(object sender, EventArgs e)
		{
			cmbVchrType.Focus();
			cmbVchrType.SelectAll();
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
			components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			groupBox1 = new System.Windows.Forms.GroupBox();
			dgDetails = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label14 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			lblReceivable = new System.Windows.Forms.Label();
			lblPurchase = new System.Windows.Forms.Label();
			lblBank = new System.Windows.Forms.Label();
			label22 = new System.Windows.Forms.Label();
			label28 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			lblExp = new System.Windows.Forms.Label();
			lblPayable = new System.Windows.Forms.Label();
			label26 = new System.Windows.Forms.Label();
			label20 = new System.Windows.Forms.Label();
			lblSale = new System.Windows.Forms.Label();
			label24 = new System.Windows.Forms.Label();
			lblCash = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			txtCheqNoSrch = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			cmbVchrTypSrch = new AshUsercontrol.AshComboBox();
			label1 = new System.Windows.Forms.Label();
			cmbPartyAcctSrch = new AshUsercontrol.AshComboBox();
			label10 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label6 = new System.Windows.Forms.Label();
			cmbVchrType = new AshUsercontrol.AshComboBox();
			label8 = new System.Windows.Forms.Label();
			cmbAccount = new AshUsercontrol.AshComboBox();
			label5 = new System.Windows.Forms.Label();
			cmbPartyAccnt = new AshUsercontrol.AshComboBox();
			label7 = new System.Windows.Forms.Label();
			txtAmount = new AshUsercontrol.AshTextBoxNormal();
			label9 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			txtBranch = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			txtBank = new AshUsercontrol.AshTextBoxNormal();
			label11 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			button2 = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			label15 = new System.Windows.Forms.Label();
			txtCheqNo = new AshUsercontrol.AshTextBoxNormal();
			label16 = new System.Windows.Forms.Label();
			dtmCheqDate = new AshUsercontrol.AshDateTimePicker();
			label17 = new System.Windows.Forms.Label();
			cmbCheqStatus = new AshUsercontrol.AshComboBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			panel1 = new System.Windows.Forms.Panel();
			dtmNewAcct = new AshUsercontrol.AshDateTimePicker();
			label21 = new System.Windows.Forms.Label();
			cmbCrOrDr = new AshUsercontrol.AshComboBox();
			txtOppBlnce = new AshUsercontrol.AshTextBoxNormal();
			label33 = new System.Windows.Forms.Label();
			btnPanelClose = new System.Windows.Forms.Button();
			btnAccCreate = new System.Windows.Forms.Button();
			txtNewAddress = new AshUsercontrol.AshTextBoxNormal();
			label34 = new System.Windows.Forms.Label();
			txtNewAcct = new AshUsercontrol.AshTextBoxNormal();
			label29 = new System.Windows.Forms.Label();
			cmbNewGroup = new AshUsercontrol.AshComboBox();
			label31 = new System.Windows.Forms.Label();
			cmbReport = new AshUsercontrol.AshComboBox();
			label32 = new System.Windows.Forms.Label();
			btnShow = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			txtRefNo = new AshUsercontrol.AshTextBoxNormal();
			label19 = new System.Windows.Forms.Label();
			txtNarration = new AshUsercontrol.AshTextBoxNormal();
			label35 = new System.Windows.Forms.Label();
			dtmChqCollOrBnc = new AshUsercontrol.AshDateTimePicker();
			button3 = new System.Windows.Forms.Button();
			label23 = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(931, -1);
			groupBox1.BackColor = System.Drawing.Color.White;
			groupBox1.Controls.Add(dgDetails);
			groupBox1.Controls.Add(label14);
			groupBox1.Controls.Add(label13);
			groupBox1.Controls.Add(lblReceivable);
			groupBox1.Controls.Add(lblPurchase);
			groupBox1.Controls.Add(lblBank);
			groupBox1.Controls.Add(label22);
			groupBox1.Controls.Add(label28);
			groupBox1.Controls.Add(label18);
			groupBox1.Controls.Add(lblExp);
			groupBox1.Controls.Add(lblPayable);
			groupBox1.Controls.Add(label26);
			groupBox1.Controls.Add(label20);
			groupBox1.Controls.Add(lblSale);
			groupBox1.Controls.Add(label24);
			groupBox1.Controls.Add(lblCash);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(txtCheqNoSrch);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(cmbVchrTypSrch);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(cmbPartyAcctSrch);
			groupBox1.Controls.Add(label10);
			groupBox1.Controls.Add(dtmTo);
			groupBox1.Controls.Add(dtmFrom);
			groupBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			groupBox1.ForeColor = System.Drawing.Color.Black;
			groupBox1.Location = new System.Drawing.Point(11, 227);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(950, 371);
			groupBox1.TabIndex = 34;
			groupBox1.TabStop = false;
			groupBox1.Text = "Search";
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(Column1, Column2, Column3, Column4, Column8, Column5, Column6, Column7, Column9, Column10);
			dgDetails.Location = new System.Drawing.Point(12, 73);
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.Blue;
			dgDetails.RowsDefaultCellStyle = dataGridViewCellStyle;
			dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgDetails.Size = new System.Drawing.Size(930, 245);
			dgDetails.TabIndex = 24;
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.HeaderText = "SL";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Width = 40;
			Column2.HeaderText = "Date";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Width = 90;
			Column3.HeaderText = "Vchr Type";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column4.HeaderText = "Vr No";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Width = 70;
			Column8.HeaderText = "Ref No";
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column8.Width = 80;
			Column5.HeaderText = "Particulars";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.Width = 200;
			Column6.HeaderText = "Opp Account";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column6.Width = 160;
			Column7.HeaderText = "Amount";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column9.HeaderText = "Narration";
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column9.Width = 250;
			Column10.HeaderText = "Type";
			Column10.Name = "Column10";
			Column10.ReadOnly = true;
			Column10.Visible = false;
			label14.AutoSize = true;
			label14.ForeColor = System.Drawing.Color.Black;
			label14.Location = new System.Drawing.Point(399, 17);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(36, 16);
			label14.TabIndex = 2;
			label14.Text = "To :";
			label13.AutoSize = true;
			label13.ForeColor = System.Drawing.Color.Black;
			label13.Location = new System.Drawing.Point(40, 17);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(51, 16);
			label13.TabIndex = 0;
			label13.Text = "From :";
			lblReceivable.AutoSize = true;
			lblReceivable.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblReceivable.ForeColor = System.Drawing.Color.Black;
			lblReceivable.Location = new System.Drawing.Point(701, 328);
			lblReceivable.Name = "lblReceivable";
			lblReceivable.Size = new System.Drawing.Size(37, 16);
			lblReceivable.TabIndex = 23;
			lblReceivable.Text = "0.00";
			lblPurchase.AutoSize = true;
			lblPurchase.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblPurchase.ForeColor = System.Drawing.Color.Black;
			lblPurchase.Location = new System.Drawing.Point(274, 350);
			lblPurchase.Name = "lblPurchase";
			lblPurchase.Size = new System.Drawing.Size(37, 16);
			lblPurchase.TabIndex = 17;
			lblPurchase.Text = "0.00";
			lblBank.AutoSize = true;
			lblBank.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblBank.ForeColor = System.Drawing.Color.Black;
			lblBank.Location = new System.Drawing.Point(274, 325);
			lblBank.Name = "lblBank";
			lblBank.Size = new System.Drawing.Size(37, 16);
			lblBank.TabIndex = 15;
			lblBank.Text = "0.00";
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label22.ForeColor = System.Drawing.Color.Black;
			label22.Location = new System.Drawing.Point(615, 326);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(89, 16);
			label22.TabIndex = 22;
			label22.Text = "Receivable :";
			label28.AutoSize = true;
			label28.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label28.ForeColor = System.Drawing.Color.Black;
			label28.Location = new System.Drawing.Point(197, 347);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(79, 16);
			label28.TabIndex = 16;
			label28.Text = "Purchase :";
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label18.ForeColor = System.Drawing.Color.Black;
			label18.Location = new System.Drawing.Point(226, 326);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(50, 16);
			label18.TabIndex = 14;
			label18.Text = "Bank :";
			lblExp.AutoSize = true;
			lblExp.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblExp.ForeColor = System.Drawing.Color.Black;
			lblExp.Location = new System.Drawing.Point(478, 350);
			lblExp.Name = "lblExp";
			lblExp.Size = new System.Drawing.Size(37, 16);
			lblExp.TabIndex = 21;
			lblExp.Text = "0.00";
			lblPayable.AutoSize = true;
			lblPayable.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblPayable.ForeColor = System.Drawing.Color.Black;
			lblPayable.Location = new System.Drawing.Point(478, 325);
			lblPayable.Name = "lblPayable";
			lblPayable.Size = new System.Drawing.Size(37, 16);
			lblPayable.TabIndex = 19;
			lblPayable.Text = "0.00";
			label26.AutoSize = true;
			label26.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label26.ForeColor = System.Drawing.Color.Black;
			label26.Location = new System.Drawing.Point(408, 347);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(73, 16);
			label26.TabIndex = 20;
			label26.Text = "Expense :";
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label20.ForeColor = System.Drawing.Color.Black;
			label20.Location = new System.Drawing.Point(411, 326);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(70, 16);
			label20.TabIndex = 18;
			label20.Text = "Payable :";
			lblSale.AutoSize = true;
			lblSale.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblSale.ForeColor = System.Drawing.Color.Black;
			lblSale.Location = new System.Drawing.Point(82, 346);
			lblSale.Name = "lblSale";
			lblSale.Size = new System.Drawing.Size(37, 16);
			lblSale.TabIndex = 13;
			lblSale.Text = "0.00";
			label24.AutoSize = true;
			label24.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label24.ForeColor = System.Drawing.Color.Black;
			label24.Location = new System.Drawing.Point(32, 347);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(47, 16);
			label24.TabIndex = 12;
			label24.Text = "Sale :";
			lblCash.AutoSize = true;
			lblCash.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			lblCash.ForeColor = System.Drawing.Color.Black;
			lblCash.Location = new System.Drawing.Point(82, 325);
			lblCash.Name = "lblCash";
			lblCash.Size = new System.Drawing.Size(37, 16);
			lblCash.TabIndex = 11;
			lblCash.Text = "0.00";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(28, 326);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(51, 16);
			label3.TabIndex = 10;
			label3.Text = "Cash :";
			txtCheqNoSrch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtCheqNoSrch.Data = 0;
			txtCheqNoSrch.EnterColor = System.Drawing.Color.LightPink;
			txtCheqNoSrch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtCheqNoSrch.Format = null;
			txtCheqNoSrch.isAllowNegative = false;
			txtCheqNoSrch.isAllowSpecialChar = false;
			txtCheqNoSrch.isNumberOnly = false;
			txtCheqNoSrch.isNumeric = false;
			txtCheqNoSrch.LeaveColor = System.Drawing.Color.White;
			txtCheqNoSrch.Location = new System.Drawing.Point(779, 51);
			txtCheqNoSrch.Margin = new System.Windows.Forms.Padding(4);
			txtCheqNoSrch.Name = "txtCheqNoSrch";
			txtCheqNoSrch.Size = new System.Drawing.Size(136, 16);
			txtCheqNoSrch.TabIndex = 9;
			txtCheqNoSrch.Value = new decimal(new int[4]);
			txtCheqNoSrch.WaterMark = null;
			txtCheqNoSrch.TextChanged += new System.EventHandler(txtCheqNoSrch_TextChanged);
			label2.AutoSize = true;
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(707, 47);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(62, 16);
			label2.TabIndex = 8;
			label2.Text = "Ref No :";
			cmbVchrTypSrch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbVchrTypSrch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbVchrTypSrch.EnterColor = System.Drawing.Color.LightPink;
			cmbVchrTypSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbVchrTypSrch.FormattingEnabled = true;
			cmbVchrTypSrch.Items.AddRange(new object[11]
			{
				"Sales", "Sales Return", "Purchase", "Purchase Return", "Cash Receipt", "Bank Receipt", "Cash Payment", "Bank Payment", "Cheque Payment", "Cheque Receipt",
				"Journal Voucher"
			});
			cmbVchrTypSrch.LeaveColor = System.Drawing.Color.White;
			cmbVchrTypSrch.Location = new System.Drawing.Point(92, 43);
			cmbVchrTypSrch.Name = "cmbVchrTypSrch";
			cmbVchrTypSrch.Size = new System.Drawing.Size(251, 24);
			cmbVchrTypSrch.TabIndex = 5;
			cmbVchrTypSrch.SelectedIndexChanged += new System.EventHandler(cmbVchrTypSrch_SelectedIndexChanged);
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.Color.Black;
			label1.Location = new System.Drawing.Point(4, 47);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 16);
			label1.TabIndex = 4;
			label1.Text = "Vchr Type :";
			cmbPartyAcctSrch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPartyAcctSrch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPartyAcctSrch.EnterColor = System.Drawing.Color.LightPink;
			cmbPartyAcctSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPartyAcctSrch.FormattingEnabled = true;
			cmbPartyAcctSrch.LeaveColor = System.Drawing.Color.White;
			cmbPartyAcctSrch.Location = new System.Drawing.Point(434, 43);
			cmbPartyAcctSrch.Name = "cmbPartyAcctSrch";
			cmbPartyAcctSrch.Size = new System.Drawing.Size(238, 24);
			cmbPartyAcctSrch.TabIndex = 7;
			cmbPartyAcctSrch.SelectedIndexChanged += new System.EventHandler(cmbPartyAcctSrch_SelectedIndexChanged);
			label10.AutoSize = true;
			label10.ForeColor = System.Drawing.Color.Black;
			label10.Location = new System.Drawing.Point(353, 47);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(82, 16);
			label10.TabIndex = 6;
			label10.Text = "Party A/c :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(435, 17);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 3;
			dtmTo.ValueChanged += new System.EventHandler(dtmFrom_ValueChanged);
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(92, 17);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(128, 23);
			dtmFrom.TabIndex = 1;
			dtmFrom.ValueChanged += new System.EventHandler(dtmFrom_ValueChanged);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label6.Location = new System.Drawing.Point(31, 43);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(50, 16);
			label6.TabIndex = 12;
			label6.Text = "Date :";
			cmbVchrType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbVchrType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbVchrType.EnterColor = System.Drawing.Color.LightPink;
			cmbVchrType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbVchrType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbVchrType.FormattingEnabled = true;
			cmbVchrType.Items.AddRange(new object[11]
			{
				"Sales", "Sales Return", "Purchase", "Purchase Return", "Cash Receipt", "Bank Receipt", "Cash Payment", "Bank Payment", "Cheque Payment", "Cheque Receipt",
				"Journal Voucher"
			});
			cmbVchrType.LeaveColor = System.Drawing.Color.White;
			cmbVchrType.Location = new System.Drawing.Point(393, 42);
			cmbVchrType.Name = "cmbVchrType";
			cmbVchrType.Size = new System.Drawing.Size(230, 24);
			cmbVchrType.TabIndex = 1;
			cmbVchrType.SelectedIndexChanged += new System.EventHandler(cmbVchrType_SelectedIndexChanged);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label8.Location = new System.Drawing.Point(303, 46);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(87, 16);
			label8.TabIndex = 0;
			label8.Text = "Vchr Type :";
			cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbAccount.EnterColor = System.Drawing.Color.LightPink;
			cmbAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbAccount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbAccount.FormattingEnabled = true;
			cmbAccount.LeaveColor = System.Drawing.Color.White;
			cmbAccount.Location = new System.Drawing.Point(87, 69);
			cmbAccount.Name = "cmbAccount";
			cmbAccount.Size = new System.Drawing.Size(211, 24);
			cmbAccount.TabIndex = 3;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label5.Location = new System.Drawing.Point(7, 73);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(74, 16);
			label5.TabIndex = 2;
			label5.Text = "Account :";
			cmbPartyAccnt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbPartyAccnt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbPartyAccnt.EnterColor = System.Drawing.Color.LightPink;
			cmbPartyAccnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPartyAccnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			cmbPartyAccnt.FormattingEnabled = true;
			cmbPartyAccnt.LeaveColor = System.Drawing.Color.White;
			cmbPartyAccnt.Location = new System.Drawing.Point(393, 70);
			cmbPartyAccnt.Name = "cmbPartyAccnt";
			cmbPartyAccnt.Size = new System.Drawing.Size(200, 23);
			cmbPartyAccnt.TabIndex = 5;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label7.Location = new System.Drawing.Point(308, 68);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(82, 16);
			label7.TabIndex = 4;
			label7.Text = "Party A/c :";
			txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAmount.Data = 0;
			txtAmount.EnterColor = System.Drawing.Color.LightPink;
			txtAmount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtAmount.Format = null;
			txtAmount.isAllowNegative = false;
			txtAmount.isAllowSpecialChar = false;
			txtAmount.isNumberOnly = false;
			txtAmount.isNumeric = false;
			txtAmount.LeaveColor = System.Drawing.Color.White;
			txtAmount.Location = new System.Drawing.Point(393, 105);
			txtAmount.Margin = new System.Windows.Forms.Padding(4);
			txtAmount.Name = "txtAmount";
			txtAmount.Size = new System.Drawing.Size(230, 16);
			txtAmount.TabIndex = 9;
			txtAmount.Text = "0.00";
			txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtAmount.Value = new decimal(new int[4] { 0, 0, 0, 131072 });
			txtAmount.WaterMark = null;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label9.Location = new System.Drawing.Point(321, 103);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(69, 16);
			label9.TabIndex = 8;
			label9.Text = "Amount :";
			button1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			button1.Location = new System.Drawing.Point(597, 73);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(26, 23);
			button1.TabIndex = 29;
			button1.Text = "...";
			toolTip1.SetToolTip(button1, "Press Alt + T");
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBranch.Data = 0;
			txtBranch.EnterColor = System.Drawing.Color.LightPink;
			txtBranch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtBranch.Format = null;
			txtBranch.isAllowNegative = false;
			txtBranch.isAllowSpecialChar = false;
			txtBranch.isNumberOnly = false;
			txtBranch.isNumeric = false;
			txtBranch.LeaveColor = System.Drawing.Color.White;
			txtBranch.Location = new System.Drawing.Point(742, 66);
			txtBranch.Margin = new System.Windows.Forms.Padding(4);
			txtBranch.Name = "txtBranch";
			txtBranch.Size = new System.Drawing.Size(205, 16);
			txtBranch.TabIndex = 17;
			txtBranch.Value = new decimal(new int[4]);
			txtBranch.Visible = false;
			txtBranch.WaterMark = null;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label12.Location = new System.Drawing.Point(675, 69);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(64, 16);
			label12.TabIndex = 16;
			label12.Text = "Branch :";
			label12.Visible = false;
			txtBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBank.Data = 0;
			txtBank.EnterColor = System.Drawing.Color.LightPink;
			txtBank.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtBank.Format = null;
			txtBank.isAllowNegative = false;
			txtBank.isAllowSpecialChar = false;
			txtBank.isNumberOnly = false;
			txtBank.isNumeric = false;
			txtBank.LeaveColor = System.Drawing.Color.White;
			txtBank.Location = new System.Drawing.Point(742, 40);
			txtBank.Margin = new System.Windows.Forms.Padding(4);
			txtBank.Name = "txtBank";
			txtBank.Size = new System.Drawing.Size(205, 16);
			txtBank.TabIndex = 15;
			txtBank.Value = new decimal(new int[4]);
			txtBank.Visible = false;
			txtBank.WaterMark = null;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label11.Location = new System.Drawing.Point(689, 46);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(50, 16);
			label11.TabIndex = 14;
			label11.Text = "Bank :";
			label11.Visible = false;
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(86, 43);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(133, 23);
			dtmDate.TabIndex = 13;
			button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			button2.Location = new System.Drawing.Point(230, 41);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(26, 23);
			button2.TabIndex = 28;
			button2.Text = "...";
			toolTip1.SetToolTip(button2, "Press Alt + N");
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(260, 191);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 26;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(169, 191);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 25;
			btnDelete.Text = "Delete";
			toolTip1.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(82, 191);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 24;
			btnSave.Text = "Save";
			toolTip1.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label15.Location = new System.Drawing.Point(649, 95);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(90, 16);
			label15.TabIndex = 18;
			label15.Text = "Cheque No :";
			label15.Visible = false;
			txtCheqNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtCheqNo.Data = 0;
			txtCheqNo.EnterColor = System.Drawing.Color.LightPink;
			txtCheqNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtCheqNo.Format = null;
			txtCheqNo.isAllowNegative = false;
			txtCheqNo.isAllowSpecialChar = false;
			txtCheqNo.isNumberOnly = false;
			txtCheqNo.isNumeric = false;
			txtCheqNo.LeaveColor = System.Drawing.Color.White;
			txtCheqNo.Location = new System.Drawing.Point(742, 92);
			txtCheqNo.Margin = new System.Windows.Forms.Padding(4);
			txtCheqNo.Name = "txtCheqNo";
			txtCheqNo.Size = new System.Drawing.Size(205, 16);
			txtCheqNo.TabIndex = 19;
			txtCheqNo.Value = new decimal(new int[4]);
			txtCheqNo.Visible = false;
			txtCheqNo.WaterMark = null;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label16.Location = new System.Drawing.Point(713, 118);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(104, 16);
			label16.TabIndex = 20;
			label16.Text = "Cheque Date :";
			label16.Visible = false;
			dtmCheqDate.CustomFormat = "dd-MMM-yyyy";
			dtmCheqDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmCheqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmCheqDate.Location = new System.Drawing.Point(821, 118);
			dtmCheqDate.Name = "dtmCheqDate";
			dtmCheqDate.Size = new System.Drawing.Size(129, 23);
			dtmCheqDate.TabIndex = 21;
			dtmCheqDate.Visible = false;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label17.Location = new System.Drawing.Point(754, 147);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(63, 16);
			label17.TabIndex = 22;
			label17.Text = "Status :";
			label17.Visible = false;
			cmbCheqStatus.EnterColor = System.Drawing.Color.LightPink;
			cmbCheqStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbCheqStatus.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbCheqStatus.FormattingEnabled = true;
			cmbCheqStatus.Items.AddRange(new object[3] { "Pending", "Collected", "Bounced" });
			cmbCheqStatus.LeaveColor = System.Drawing.Color.White;
			cmbCheqStatus.Location = new System.Drawing.Point(821, 144);
			cmbCheqStatus.Name = "cmbCheqStatus";
			cmbCheqStatus.Size = new System.Drawing.Size(124, 24);
			cmbCheqStatus.TabIndex = 23;
			cmbCheqStatus.Text = "Pending";
			cmbCheqStatus.Visible = false;
			cmbCheqStatus.SelectedIndexChanged += new System.EventHandler(cmbCheqStatus_SelectedIndexChanged);
			cmbCheqStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbCheqStatus_KeyDown);
			panel1.BackColor = System.Drawing.SystemColors.Control;
			panel1.Controls.Add(dtmNewAcct);
			panel1.Controls.Add(label21);
			panel1.Controls.Add(cmbCrOrDr);
			panel1.Controls.Add(txtOppBlnce);
			panel1.Controls.Add(label33);
			panel1.Controls.Add(btnPanelClose);
			panel1.Controls.Add(btnAccCreate);
			panel1.Controls.Add(txtNewAddress);
			panel1.Controls.Add(label34);
			panel1.Controls.Add(txtNewAcct);
			panel1.Controls.Add(label29);
			panel1.Controls.Add(cmbNewGroup);
			panel1.Controls.Add(label31);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			panel1.Location = new System.Drawing.Point(967, 45);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(357, 195);
			panel1.TabIndex = 0;
			panel1.Visible = false;
			panel1.VisibleChanged += new System.EventHandler(panel1_VisibleChanged);
			dtmNewAcct.CustomFormat = "dd-MMM-yyyy";
			dtmNewAcct.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmNewAcct.Location = new System.Drawing.Point(128, 121);
			dtmNewAcct.Name = "dtmNewAcct";
			dtmNewAcct.Size = new System.Drawing.Size(143, 23);
			dtmNewAcct.TabIndex = 15;
			label21.AutoSize = true;
			label21.Location = new System.Drawing.Point(14, 121);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(45, 16);
			label21.TabIndex = 14;
			label21.Text = "Date:";
			cmbCrOrDr.EnterColor = System.Drawing.Color.LightPink;
			cmbCrOrDr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbCrOrDr.FormattingEnabled = true;
			cmbCrOrDr.Items.AddRange(new object[2] { "CR", "DR" });
			cmbCrOrDr.LeaveColor = System.Drawing.Color.White;
			cmbCrOrDr.Location = new System.Drawing.Point(282, 89);
			cmbCrOrDr.Name = "cmbCrOrDr";
			cmbCrOrDr.Size = new System.Drawing.Size(44, 24);
			cmbCrOrDr.TabIndex = 13;
			cmbCrOrDr.Text = "DR";
			txtOppBlnce.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtOppBlnce.Data = 0;
			txtOppBlnce.EnterColor = System.Drawing.Color.LightPink;
			txtOppBlnce.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtOppBlnce.Format = null;
			txtOppBlnce.isAllowNegative = false;
			txtOppBlnce.isAllowSpecialChar = false;
			txtOppBlnce.isNumberOnly = false;
			txtOppBlnce.isNumeric = false;
			txtOppBlnce.LeaveColor = System.Drawing.Color.White;
			txtOppBlnce.Location = new System.Drawing.Point(125, 89);
			txtOppBlnce.Margin = new System.Windows.Forms.Padding(4);
			txtOppBlnce.Name = "txtOppBlnce";
			txtOppBlnce.Size = new System.Drawing.Size(146, 16);
			txtOppBlnce.TabIndex = 7;
			txtOppBlnce.Text = "0";
			txtOppBlnce.Value = new decimal(new int[4]);
			txtOppBlnce.WaterMark = null;
			label33.AutoSize = true;
			label33.Location = new System.Drawing.Point(13, 94);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(123, 16);
			label33.TabIndex = 6;
			label33.Text = "Opening Balance:";
			btnPanelClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnPanelClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnPanelClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPanelClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPanelClose.ForeColor = System.Drawing.Color.White;
			btnPanelClose.Location = new System.Drawing.Point(206, 152);
			btnPanelClose.Name = "btnPanelClose";
			btnPanelClose.Size = new System.Drawing.Size(75, 30);
			btnPanelClose.TabIndex = 9;
			btnPanelClose.Text = "Close";
			btnPanelClose.UseVisualStyleBackColor = false;
			btnPanelClose.Click += new System.EventHandler(button3_Click);
			btnAccCreate.BackColor = System.Drawing.Color.DodgerBlue;
			btnAccCreate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnAccCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnAccCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnAccCreate.ForeColor = System.Drawing.Color.White;
			btnAccCreate.Location = new System.Drawing.Point(106, 152);
			btnAccCreate.Name = "btnAccCreate";
			btnAccCreate.Size = new System.Drawing.Size(75, 30);
			btnAccCreate.TabIndex = 8;
			btnAccCreate.Text = "Create";
			btnAccCreate.UseVisualStyleBackColor = false;
			btnAccCreate.Click += new System.EventHandler(button4_Click);
			txtNewAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNewAddress.Data = 0;
			txtNewAddress.EnterColor = System.Drawing.Color.LightPink;
			txtNewAddress.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNewAddress.Format = null;
			txtNewAddress.isAllowNegative = false;
			txtNewAddress.isAllowSpecialChar = false;
			txtNewAddress.isNumberOnly = false;
			txtNewAddress.isNumeric = false;
			txtNewAddress.LeaveColor = System.Drawing.Color.White;
			txtNewAddress.Location = new System.Drawing.Point(96, 64);
			txtNewAddress.Margin = new System.Windows.Forms.Padding(4);
			txtNewAddress.Name = "txtNewAddress";
			txtNewAddress.Size = new System.Drawing.Size(230, 16);
			txtNewAddress.TabIndex = 5;
			txtNewAddress.Value = new decimal(new int[4]);
			txtNewAddress.WaterMark = null;
			label34.AutoSize = true;
			label34.Location = new System.Drawing.Point(11, 70);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(66, 16);
			label34.TabIndex = 4;
			label34.Text = "Address:";
			txtNewAcct.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNewAcct.Data = 0;
			txtNewAcct.EnterColor = System.Drawing.Color.LightPink;
			txtNewAcct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNewAcct.Format = null;
			txtNewAcct.isAllowNegative = false;
			txtNewAcct.isAllowSpecialChar = false;
			txtNewAcct.isNumberOnly = false;
			txtNewAcct.isNumeric = false;
			txtNewAcct.LeaveColor = System.Drawing.Color.White;
			txtNewAcct.Location = new System.Drawing.Point(96, 10);
			txtNewAcct.Margin = new System.Windows.Forms.Padding(4);
			txtNewAcct.Name = "txtNewAcct";
			txtNewAcct.Size = new System.Drawing.Size(230, 16);
			txtNewAcct.TabIndex = 1;
			txtNewAcct.Value = new decimal(new int[4]);
			txtNewAcct.WaterMark = null;
			txtNewAcct.Validated += new System.EventHandler(txtNewAcct_Validated);
			label29.AutoSize = true;
			label29.Location = new System.Drawing.Point(11, 16);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(69, 16);
			label29.TabIndex = 0;
			label29.Text = "Account:";
			cmbNewGroup.EnterColor = System.Drawing.Color.LightPink;
			cmbNewGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbNewGroup.FormattingEnabled = true;
			cmbNewGroup.LeaveColor = System.Drawing.Color.White;
			cmbNewGroup.Location = new System.Drawing.Point(96, 36);
			cmbNewGroup.Name = "cmbNewGroup";
			cmbNewGroup.Size = new System.Drawing.Size(230, 24);
			cmbNewGroup.TabIndex = 3;
			label31.AutoSize = true;
			label31.Location = new System.Drawing.Point(9, 40);
			label31.Name = "label31";
			label31.Size = new System.Drawing.Size(52, 16);
			label31.TabIndex = 2;
			label31.Text = "Group:";
			cmbReport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			cmbReport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			cmbReport.EnterColor = System.Drawing.Color.LightPink;
			cmbReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbReport.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbReport.FormattingEnabled = true;
			cmbReport.Items.AddRange(new object[11]
			{
				"Sales", "Sales Return", "Purchase", "Purchase Return", "Cash Receipt", "Bank Receipt", "Cash Payment", "Bank Payment", "Cheque Payment", "Cheque Receipt",
				"Ledger Report"
			});
			cmbReport.LeaveColor = System.Drawing.Color.White;
			cmbReport.Location = new System.Drawing.Point(531, 201);
			cmbReport.Name = "cmbReport";
			cmbReport.Size = new System.Drawing.Size(158, 24);
			cmbReport.TabIndex = 31;
			cmbReport.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbReport_KeyDown);
			label32.AutoSize = true;
			label32.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label32.Location = new System.Drawing.Point(446, 203);
			label32.Name = "label32";
			label32.Size = new System.Drawing.Size(64, 16);
			label32.TabIndex = 30;
			label32.Text = "Reports:";
			btnShow.BackColor = System.Drawing.Color.DodgerBlue;
			btnShow.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShow.ForeColor = System.Drawing.Color.White;
			btnShow.Location = new System.Drawing.Point(700, 196);
			btnShow.Name = "btnShow";
			btnShow.Size = new System.Drawing.Size(75, 30);
			btnShow.TabIndex = 32;
			btnShow.Text = "Show";
			btnShow.UseVisualStyleBackColor = false;
			btnShow.Click += new System.EventHandler(button5_Click);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label4.Location = new System.Drawing.Point(19, 106);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(62, 16);
			label4.TabIndex = 6;
			label4.Text = "Ref No :";
			txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRefNo.Data = 0;
			txtRefNo.EnterColor = System.Drawing.Color.LightPink;
			txtRefNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtRefNo.Format = null;
			txtRefNo.isAllowNegative = false;
			txtRefNo.isAllowSpecialChar = false;
			txtRefNo.isNumberOnly = false;
			txtRefNo.isNumeric = false;
			txtRefNo.LeaveColor = System.Drawing.Color.White;
			txtRefNo.Location = new System.Drawing.Point(86, 105);
			txtRefNo.Margin = new System.Windows.Forms.Padding(4);
			txtRefNo.Name = "txtRefNo";
			txtRefNo.Size = new System.Drawing.Size(212, 16);
			txtRefNo.TabIndex = 7;
			txtRefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtRefNo.Value = new decimal(new int[4]);
			txtRefNo.WaterMark = null;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label19.Location = new System.Drawing.Point(8, 133);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(73, 16);
			label19.TabIndex = 10;
			label19.Text = "Remarks :";
			txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNarration.Data = 0;
			txtNarration.EnterColor = System.Drawing.Color.LightPink;
			txtNarration.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtNarration.Format = null;
			txtNarration.isAllowNegative = false;
			txtNarration.isAllowSpecialChar = false;
			txtNarration.isNumberOnly = false;
			txtNarration.isNumeric = false;
			txtNarration.LeaveColor = System.Drawing.Color.White;
			txtNarration.Location = new System.Drawing.Point(85, 130);
			txtNarration.Margin = new System.Windows.Forms.Padding(4);
			txtNarration.Multiline = true;
			txtNarration.Name = "txtNarration";
			txtNarration.Size = new System.Drawing.Size(538, 43);
			txtNarration.TabIndex = 11;
			txtNarration.Value = new decimal(new int[4]);
			txtNarration.WaterMark = null;
			txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNarration_KeyDown);
			label35.AutoSize = true;
			label35.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label35.Location = new System.Drawing.Point(639, 173);
			label35.Name = "label35";
			label35.Size = new System.Drawing.Size(178, 16);
			label35.TabIndex = 20;
			label35.Text = "Collected/Bounced Date :";
			label35.Visible = false;
			dtmChqCollOrBnc.CustomFormat = "dd-MMM-yyyy";
			dtmChqCollOrBnc.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmChqCollOrBnc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmChqCollOrBnc.Location = new System.Drawing.Point(821, 173);
			dtmChqCollOrBnc.Name = "dtmChqCollOrBnc";
			dtmChqCollOrBnc.Size = new System.Drawing.Size(132, 23);
			dtmChqCollOrBnc.TabIndex = 21;
			dtmChqCollOrBnc.Visible = false;
			dtmChqCollOrBnc.ValueChanged += new System.EventHandler(dtmChqCollOrBnc_ValueChanged);
			button3.Location = new System.Drawing.Point(3, 33);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(311, 1);
			button3.TabIndex = 71;
			button3.UseVisualStyleBackColor = true;
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label23.Location = new System.Drawing.Point(9, 9);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(286, 24);
			label23.TabIndex = 70;
			label23.Text = "Transaction Without Inventory";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(967, 599);
			base.Controls.Add(button3);
			base.Controls.Add(label23);
			base.Controls.Add(label32);
			base.Controls.Add(cmbReport);
			base.Controls.Add(panel1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(btnShow);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(txtCheqNo);
			base.Controls.Add(label15);
			base.Controls.Add(txtBranch);
			base.Controls.Add(label12);
			base.Controls.Add(txtBank);
			base.Controls.Add(label11);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.Controls.Add(txtNarration);
			base.Controls.Add(label19);
			base.Controls.Add(txtRefNo);
			base.Controls.Add(label4);
			base.Controls.Add(txtAmount);
			base.Controls.Add(label9);
			base.Controls.Add(cmbCheqStatus);
			base.Controls.Add(label17);
			base.Controls.Add(label7);
			base.Controls.Add(dtmChqCollOrBnc);
			base.Controls.Add(dtmCheqDate);
			base.Controls.Add(dtmDate);
			base.Controls.Add(cmbAccount);
			base.Controls.Add(label5);
			base.Controls.Add(label35);
			base.Controls.Add(cmbVchrType);
			base.Controls.Add(label16);
			base.Controls.Add(label8);
			base.Controls.Add(label6);
			base.Controls.Add(cmbPartyAccnt);
			base.MaximizeBox = false;
			base.Name = "TransactionWithoutInventory";
			base.Activated += new System.EventHandler(TransactionWithoutInventory_Activated);
			base.Load += new System.EventHandler(TransactionWithoutInventory_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(TransactionWithoutInventory_KeyDown);
			base.Controls.SetChildIndex(cmbPartyAccnt, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label16, 0);
			base.Controls.SetChildIndex(cmbVchrType, 0);
			base.Controls.SetChildIndex(label35, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(cmbAccount, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(dtmCheqDate, 0);
			base.Controls.SetChildIndex(dtmChqCollOrBnc, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(label17, 0);
			base.Controls.SetChildIndex(cmbCheqStatus, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(txtAmount, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(txtRefNo, 0);
			base.Controls.SetChildIndex(label19, 0);
			base.Controls.SetChildIndex(txtNarration, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(button2, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(txtBank, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtBranch, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(txtCheqNo, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnShow, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(cmbReport, 0);
			base.Controls.SetChildIndex(label32, 0);
			base.Controls.SetChildIndex(label23, 0);
			base.Controls.SetChildIndex(button3, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
