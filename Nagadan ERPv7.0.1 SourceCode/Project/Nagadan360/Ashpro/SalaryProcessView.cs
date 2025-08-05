using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SalaryProcessView : BaseForm
	{
		private DBContext db;

		private List<ExSlryPrcsSettings> slryPrcsSettings;

		private int iMasterId;

		private int iUserId;

		private int iDays;

		private string sVoucherName;

		private string sVoucherType;

		private string sNarration;

		private IContainer components = null;

		private Button btnLoadRpt;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Panel panel1;

		private AshTextBoxNormal txtDscrptn;

		private Label label8;

		private Button button4;

		private Label lblHead;

		private Label label7;

		private Button btnLoad;

		private AshDateTimePicker dtmTo;

		private Label label4;

		private AshDateTimePicker dtmDate;

		private Label label12;

		private AshDateTimePicker dtmFrom;

		private Label label3;

		private AshGradientPanel ashGradientPanel1;

		private AshGradientPanel btnSelected;

		private Button btnSalaryReport;

		private Button btnSalaryProcess;

		private AshComboBox cmbSlryPrdRpt;

		private Panel panel3;

		private Label label5;

		private AshComboBox cmbSlryPrd;

		private AshDateTimePicker dtmToRpt;

		private AshDateTimePicker dtmFrmRpt;

		private Label label10;

		private Label label2;

		private Label label9;

		private AshUpDown nudVchrNo;

		private TabControl tabSalary;

		private TabPage tabProcess;

		private Label label1;

		private TabPage tabReport;

		private Button btnClearRpt;

		private Button btnExcel;

		private Panel panel2;

		private AshDataGridview dgvDetails;

		private BindingSource bdsSalary;

		private AshDataGridview dgvReport;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_Name;

		private DataGridViewTextBoxColumn col_Leaves;

		private DataGridViewTextBoxColumn col_CasualLeaves;

		private DataGridViewTextBoxColumn col_BasicSalary;

		private DataGridViewTextBoxColumn col_CSalary;

		private DataGridViewTextBoxColumn col_LeaveCut;

		private DataGridViewTextBoxColumn col_Incentive;

		private DataGridViewTextBoxColumn col_Others;

		private DataGridViewTextBoxColumn col_SalaryCut;

		private DataGridViewTextBoxColumn col_OtherDeduction;

		private DataGridViewTextBoxColumn col_NetSalary;

		private DataGridViewTextBoxColumn col_Advance;

		private DataGridViewTextBoxColumn col_PaidAmount;

		private DataGridViewTextBoxColumn col_Balance;

		private DataGridViewTextBoxColumn col_Remarks;

		private AshComboBox cmbCashAcct;

		public SalaryProcessView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void BindDGV()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsSalary.DataSource = null;
			bdsSalary.DataSource = slryPrcsSettings;
			dgvDetails.DataSource = bdsSalary;
		}

		private SalaryProcessMaster SlryPrcsMasterEntityLoadMethod(int iInvNo)
		{
			return new SalaryProcessMaster
			{
				SPrcsMasterID = iMasterId,
				SPrcsVchrNo = iInvNo,
				SPrcsDate = dtmDate.Value.Date,
				SlryPrd = cmbSlryPrd.Text,
				DateFrom = dtmFrom.Value.Date,
				DateTo = dtmTo.Value.Date,
				StaffType = "All",
				Description = txtDscrptn.Text,
				CashOrBankAcctId = cmbCashAcct.SelectedValue.ToInt32(),
				CashOrBankAcctName = cmbCashAcct.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void SalaryPaymentEntryMethod()
		{
			try
			{
				int iVchrNo = 1;
				foreach (ExSlryPrcsSettings item in slryPrcsSettings)
				{
					int num = db.EmployeeDetails().Single((EmployeeDetailsEntity x) => x.EmpID == item.EmpID).SalaryAccntId.toInt32();
					if ((from x in db.TransactionLogs()
						where x.VchrType == sVoucherType && x.VchrName == sVoucherName
						select x).ToList().Count > 0)
					{
						iVchrNo = (from x in db.TransactionLogs()
							where x.VchrType == sVoucherType && x.VchrName == sVoucherName
							select x).Max((TransactionLog y) => y.VchrNo) + 1;
					}
					else
					{
						iVchrNo = 1;
					}
					POSHelper.VoucherTranLog(iVchrNo, dtmDate.Value.Date, item.PaidAmnt, sVoucherType, sNarration, cmbCashAcct.SelectedValue.ToInt32(), num, iUserId, iUserId, sVoucherName);
					int iVoucherID = db.VchrTransaction().Single((VchrTranLog x) => x.VchrNo == iVchrNo && x.VchrType == sVoucherType && x.VchrName == sVoucherName).VchrTranId.toInt32();
					POSHelper.TranLogMethod(0m, item.PaidAmnt, cmbCashAcct.SelectedValue.ToInt32(), num, iVoucherID, iVchrNo, sVoucherType, iUserId, "Main", sVoucherName, AniHelper.GetDate(dtmDate.Value));
					POSHelper.TranLogMethod(item.PaidAmnt, 0m, num, cmbCashAcct.SelectedValue.ToInt32(), iVoucherID, iVchrNo, sVoucherType, iUserId, "Sub", sVoucherName, AniHelper.GetDate(dtmDate.Value));
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void SalaryProcessView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillCashAndBankAccount(cmbCashAcct);
			nudVchrNo.Value = (db.SalaryProcesses(new
			{
				FK_BranchId = Settings.Default.iBranchId
			}).Max((SalaryProcessMaster x) => x.SPrcsVchrNo) + 1) ?? 1;
			iUserId = Settings.Default.intLogOnId;
			sVoucherType = "Payment Voucher";
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			switch (cmbSlryPrd.SelectedIndex)
			{
			default:
				iDays = 1;
				break;
			case 0:
				iDays = 365;
				break;
			case 1:
				iDays = 30;
				break;
			case 2:
				iDays = 7;
				break;
			}
			int sL = 1;
			int num = dtmTo.Value.Day - dtmFrom.Value.Day;
			if (num > iDays)
			{
				num = iDays;
			}
			slryPrcsSettings = new List<ExSlryPrcsSettings>();
			foreach (EmployeeDetailsEntity item in (from x in db.EmployeeDetails()
				where x.isActive
				select x).ToList())
			{
				ExSlryPrcsSettings exSlryPrcsSettings = new ExSlryPrcsSettings();
				exSlryPrcsSettings.SL = sL;
				exSlryPrcsSettings.EmpID = item.EmpID.ToInt32();
				exSlryPrcsSettings.EmpName = item.EmpName;
				exSlryPrcsSettings.LeavsTaken = (from x in db.AttenSettings()
					where x.EmpID == item.EmpID && x.AtteDate > dtmFrom.Value.Date && x.AtteDate <= dtmTo.Value.Date
					select x).Sum((AttenSettings y) => y.AttenType);
				exSlryPrcsSettings.CasualLeave = item.CasualLeave;
				exSlryPrcsSettings.BasicSalary = item.Salary;
				exSlryPrcsSettings.CSalary = item.Salary / (decimal)iDays * (decimal)num;
				decimal num2 = exSlryPrcsSettings.CSalary * (exSlryPrcsSettings.LeavsTaken - item.CasualLeave);
				exSlryPrcsSettings.LeaveDiduction = ((num2 > 0m) ? num2 : 0m);
				exSlryPrcsSettings.Incentive = 0m;
				exSlryPrcsSettings.OtherAmnt = 0m;
				exSlryPrcsSettings.OtherDiduction = 0m;
				exSlryPrcsSettings.SalaryCut = 0m;
				exSlryPrcsSettings.NetSalary = exSlryPrcsSettings.CSalary - exSlryPrcsSettings.LeaveDiduction;
				exSlryPrcsSettings.SlryAdvance = 0m;
				exSlryPrcsSettings.Balance = 0m;
				exSlryPrcsSettings.PaidAmnt = 0m;
				exSlryPrcsSettings.Remarks = string.Empty;
				slryPrcsSettings.Add(exSlryPrcsSettings);
			}
			BindDGV();
		}

		private void btnSalaryProcess_Click(object sender, EventArgs e)
		{
			tabSalary.SelectedTab = tabProcess;
			btnSalaryProcess.BackColor = Color.White;
			btnSalaryProcess.ForeColor = Color.Black;
			btnSalaryReport.BackColor = Color.Transparent;
			btnSalaryReport.ForeColor = Color.Black;
			btnSelected.Top = btnSalaryProcess.Top;
		}

		private void btnSalaryReport_Click(object sender, EventArgs e)
		{
			tabSalary.SelectedTab = tabReport;
			btnSalaryReport.BackColor = Color.White;
			btnSalaryReport.ForeColor = Color.Black;
			btnSalaryProcess.BackColor = Color.Transparent;
			btnSalaryProcess.ForeColor = Color.Black;
			btnSelected.Top = btnSalaryReport.Top;
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (dgvDetails.CurrentRow == null)
			{
				return;
			}
			ExSlryPrcsSettings exSlryPrcsSettings = (ExSlryPrcsSettings)dgvDetails.CurrentRow.DataBoundItem;
			if (!(dgvDetails.CurrentCell.OwningColumn.Name == col_Incentive.Name) && !(dgvDetails.CurrentCell.OwningColumn.Name == col_Others.Name) && !(dgvDetails.CurrentCell.OwningColumn.Name == col_SalaryCut.Name) && !(dgvDetails.CurrentCell.OwningColumn.Name == col_OtherDeduction.Name))
			{
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_Advance.Name)
				{
					exSlryPrcsSettings.PaidAmnt = exSlryPrcsSettings.NetSalary - exSlryPrcsSettings.SlryAdvance;
					exSlryPrcsSettings.Balance = 0m;
				}
				else if (dgvDetails.CurrentCell.OwningColumn.Name == col_PaidAmount.Name)
				{
					exSlryPrcsSettings.Balance = exSlryPrcsSettings.NetSalary - (exSlryPrcsSettings.PaidAmnt + exSlryPrcsSettings.SlryAdvance);
				}
			}
			else
			{
				exSlryPrcsSettings.NetSalary = exSlryPrcsSettings.CSalary + exSlryPrcsSettings.Incentive + exSlryPrcsSettings.OtherAmnt - (exSlryPrcsSettings.SalaryCut + exSlryPrcsSettings.OtherDiduction + exSlryPrcsSettings.LeaveDiduction);
				exSlryPrcsSettings.PaidAmnt = exSlryPrcsSettings.NetSalary - exSlryPrcsSettings.SlryAdvance;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (btnSave.Text == "Save")
			{
				int iInvNo = ((from x in db.SalaryProcesses()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((SalaryProcessMaster x) => x.SPrcsVchrNo) + 1) ?? 1;
				sNarration = "Salary Bulk Payment from Invoice = " + iMasterId + " Branch = " + Settings.Default.iBranchId;
				SalaryProcessMaster salaryProcessMaster = SlryPrcsMasterEntityLoadMethod(iInvNo);
				salaryProcessMaster.SPrcsMasterID = null;
				if (!AshproCommon.InsertToDatabaseObj(salaryProcessMaster))
				{
					return;
				}
				SalaryProcessMaster salaryProcessMaster2 = db.SalaryProcesses().FirstOrDefault((SalaryProcessMaster x) => x.SPrcsVchrNo == iInvNo && x.FK_BranchId == Settings.Default.iBranchId);
				if (salaryProcessMaster2 != null)
				{
					iMasterId = salaryProcessMaster2.SPrcsMasterID.toInt32();
					List<SlryPrcsSettings> list = AniHelper.CopyListData<SlryPrcsSettings>(slryPrcsSettings.Cast<object>().ToList());
					list.ForEach(delegate(SlryPrcsSettings x)
					{
						x.SprcsMasterID = iMasterId;
					});
					if (AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "SlryPrcsSettings"))
					{
						SalaryPaymentEntryMethod();
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				return;
			}
			sNarration = "Salary Bulk Payment from Invoice = " + iMasterId + " Branch = " + Settings.Default.iBranchId;
			foreach (TransactionLog item in from x in db.TransactionLogs()
				where x.Narration == sNarration
				select x)
			{
				AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item.TranId.toInt32());
			}
			AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", db.VchrTransaction().Single((VchrTranLog x) => x.Narration == sNarration).VchrTranId.toInt32());
			AshproCommon.DeleteFromDatabase("SlryPrcsSettings", "SprcsMasterID", iMasterId);
			if (AshproCommon.UpdateToDatabaseObj(SlryPrcsMasterEntityLoadMethod(nudVchrNo.Value.ToInt32()), "SalaryProcessMaster", "SPrcsMasterID", iMasterId))
			{
				List<SlryPrcsSettings> list2 = AniHelper.CopyListData<SlryPrcsSettings>(slryPrcsSettings.Cast<object>().ToList());
				list2.ForEach(delegate(SlryPrcsSettings x)
				{
					x.SPrcsStngsID = null;
					x.SprcsMasterID = iMasterId;
				});
				if (AshproCommon.InsertToDatabase(list2.Cast<object>().ToList(), "SlryPrcsSettings"))
				{
					SalaryPaymentEntryMethod();
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void cmbCashAcct_SelectedIndexChanged(object sender, EventArgs e)
		{
			sVoucherName = ((db.AccountGroup().Single((AccountGroupEntity x) => x.AGroupId == db.Accounts().Single((AccountEntity y) => y.AccountId == cmbCashAcct.SelectedValue.ToInt32()).AGroupId).GroupName == "CASH IN HAND") ? "Cash Payment" : "Bank Payment");
		}

		private void btnLoadRpt_Click(object sender, EventArgs e)
		{
			slryPrcsSettings = new List<ExSlryPrcsSettings>();
			SalaryProcessMaster salaryProcessMaster = db.SalaryProcesses().FirstOrDefault((SalaryProcessMaster x) => x.SlryPrd == cmbSlryPrdRpt.Text && x.DateFrom == dtmFrmRpt.Value.Date && x.DateTo == dtmToRpt.Value.Date && x.FK_BranchId == Settings.Default.iBranchId);
			if (salaryProcessMaster != null)
			{
				iMasterId = salaryProcessMaster.SPrcsMasterID.toInt32();
				List<SlryPrcsSettings> source = (from x in db.SlryPrcsSettings()
					where x.SprcsMasterID == iMasterId
					select x).ToList();
				slryPrcsSettings = AniHelper.CopyListData<ExSlryPrcsSettings>(source.Cast<object>().ToList());
				int num = 1;
				foreach (ExSlryPrcsSettings slryPrcsSetting in slryPrcsSettings)
				{
					slryPrcsSetting.SL = num;
					num++;
				}
			}
			dgvReport.AutoGenerateColumns = false;
			dgvReport.DataSource = slryPrcsSettings;
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//AniHelper.ExportToExcelMethod(dgvReport, "Salary Process Report from " + dtmFrom.Text + " to " + dtmTo.Text);
		}

		private void btnClearRpt_Click(object sender, EventArgs e)
		{
			AniHelper.ClearDateTimeMethod(this);
			dgvReport.DataSource = null;
		}

		private void nudVchrNo_ValueChanged(object sender, EventArgs e)
		{
			slryPrcsSettings = new List<ExSlryPrcsSettings>();
			SalaryProcessMaster salaryProcessMaster = (from x in db.SalaryProcesses()
				where x.SPrcsVchrNo == nudVchrNo.Value.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
				select x).FirstOrDefault();
			if (salaryProcessMaster == null)
			{
				return;
			}
			iMasterId = salaryProcessMaster.SPrcsMasterID.ToInt32();
			cmbSlryPrd.Text = salaryProcessMaster.SlryPrd;
			dtmDate.Value = salaryProcessMaster.SPrcsDate;
			dtmFrom.Value = salaryProcessMaster.DateFrom;
			dtmTo.Value = salaryProcessMaster.DateTo;
			cmbCashAcct.SelectedValue = salaryProcessMaster.CashOrBankAcctId;
			txtDscrptn.Text = salaryProcessMaster.Description;
			List<SlryPrcsSettings> source = (from x in db.SlryPrcsSettings()
				where x.SprcsMasterID == iMasterId
				select x).ToList();
			slryPrcsSettings = AniHelper.CopyListData<ExSlryPrcsSettings>(source.Cast<object>().ToList());
			int num = 1;
			foreach (ExSlryPrcsSettings slryPrcsSetting in slryPrcsSettings)
			{
				slryPrcsSetting.SL = num;
				num++;
			}
			BindDGV();
			btnSave.Text = "Update";
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			cmbSlryPrd.Focus();
			cmbSlryPrd.SelectAll();
		}

		private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				int iCount = 1;
				ExSlryPrcsSettings item = (ExSlryPrcsSettings)dgvDetails.CurrentRow.DataBoundItem;
				slryPrcsSettings.Remove(item);
				slryPrcsSettings.ForEach(delegate(ExSlryPrcsSettings x)
				{
					x.SL = iCount;
					iCount++;
				});
				BindDGV();
				dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 1].Cells[1];
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (!Messages.DeleteConfirmationMessage())
				{
					return;
				}
				sNarration = "Salary Bulk Payment from Invoice = " + iMasterId + " Branch = " + Settings.Default.iBranchId;
				foreach (TransactionLog item in from x in db.TransactionLogs()
					where x.Narration == sNarration
					select x)
				{
					AshproCommon.DeleteFromDatabase("TransactionLog", "TranId", item.TranId.toInt32());
				}
				AshproCommon.DeleteFromDatabase("VchrTranLog", "VchrTranId", db.VchrTransaction().Single((VchrTranLog x) => x.Narration == sNarration).VchrTranId.toInt32());
				AshproCommon.DeleteFromDatabase("SlryPrcsSettings", "SprcsMasterID", iMasterId);
				if (AshproCommon.DeleteFromDatabase("SalaryProcessMaster", "SPrcsMasterID", iMasterId))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DateTime dateTime2 = (dtmFrom.Value = (dtmTo.Value = DateTime.Now));
			nudVchrNo.Value = ((from x in db.SalaryProcesses()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((SalaryProcessMaster x) => x.SPrcsVchrNo) + 1) ?? 1;
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			btnDelete.ForeColor = Color.Black;
			slryPrcsSettings = new List<ExSlryPrcsSettings>();
			BindDGV();
			cmbSlryPrd.Focus();
			cmbSlryPrd.SelectAll();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
			btnLoadRpt = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Leaves = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CasualLeaves = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_BasicSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_LeaveCut = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Incentive = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Others = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SalaryCut = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_OtherDeduction = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NetSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Advance = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_PaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
			txtDscrptn = new AshUsercontrol.AshTextBoxNormal();
			label8 = new System.Windows.Forms.Label();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			btnLoad = new System.Windows.Forms.Button();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label4 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label12 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label3 = new System.Windows.Forms.Label();
			ashGradientPanel1 = new AshUsercontrol.AshGradientPanel();
			btnSelected = new AshUsercontrol.AshGradientPanel();
			btnSalaryReport = new System.Windows.Forms.Button();
			btnSalaryProcess = new System.Windows.Forms.Button();
			cmbSlryPrdRpt = new AshUsercontrol.AshComboBox();
			panel3 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			cmbSlryPrd = new AshUsercontrol.AshComboBox();
			dtmToRpt = new AshUsercontrol.AshDateTimePicker();
			dtmFrmRpt = new AshUsercontrol.AshDateTimePicker();
			label10 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			nudVchrNo = new AshUsercontrol.AshUpDown();
			tabSalary = new System.Windows.Forms.TabControl();
			tabProcess = new System.Windows.Forms.TabPage();
			label1 = new System.Windows.Forms.Label();
			tabReport = new System.Windows.Forms.TabPage();
			btnClearRpt = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			panel2 = new System.Windows.Forms.Panel();
			dgvReport = new AshUsercontrol.AshDataGridview();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			bdsSalary = new System.Windows.Forms.BindingSource(components);
			cmbCashAcct = new AshUsercontrol.AshComboBox();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			ashGradientPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudVchrNo).BeginInit();
			tabSalary.SuspendLayout();
			tabProcess.SuspendLayout();
			tabReport.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsSalary).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(1036, 0);
			btnLoadRpt.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoadRpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoadRpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoadRpt.ForeColor = System.Drawing.Color.White;
			btnLoadRpt.Location = new System.Drawing.Point(801, 14);
			btnLoadRpt.Name = "btnLoadRpt";
			btnLoadRpt.Size = new System.Drawing.Size(75, 30);
			btnLoadRpt.TabIndex = 10;
			btnLoadRpt.TabStop = false;
			btnLoadRpt.Text = "Load";
			btnLoadRpt.UseVisualStyleBackColor = false;
			btnLoadRpt.Click += new System.EventHandler(btnLoadRpt_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(836, 472);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 13;
			btnClear.TabStop = false;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(759, 473);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 12;
			btnDelete.TabStop = false;
			btnDelete.Text = "Delete";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(681, 472);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 11;
			btnSave.TabStop = false;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			panel1.Location = new System.Drawing.Point(11, 96);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(900, 357);
			panel1.TabIndex = 9;
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_Name, col_Leaves, col_CasualLeaves, col_BasicSalary, col_CSalary, col_LeaveCut, col_Incentive, col_Others, col_SalaryCut, col_OtherDeduction, col_NetSalary, col_Advance, col_PaidAmount, col_Balance, col_Remarks);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.EnterKeyNavigation = false;
			dgvDetails.LastKey = System.Windows.Forms.Keys.None;
			dgvDetails.Location = new System.Drawing.Point(12, 13);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dgvDetails.Size = new System.Drawing.Size(877, 331);
			dgvDetails.TabIndex = 1;
			dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellEndEdit);
			dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetails_DataError);
			dgvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetails_KeyDown);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 50;
			col_Name.DataPropertyName = "EmpName";
			col_Name.HeaderText = "Staff Name";
			col_Name.Name = "col_Name";
			col_Name.ReadOnly = true;
			col_Name.Width = 150;
			col_Leaves.DataPropertyName = "LeavsTaken";
			dataGridViewCellStyle3.Format = "N2";
			col_Leaves.DefaultCellStyle = dataGridViewCellStyle3;
			col_Leaves.HeaderText = "Leaves";
			col_Leaves.Name = "col_Leaves";
			col_Leaves.ReadOnly = true;
			col_Leaves.Width = 60;
			col_CasualLeaves.DataPropertyName = "CasualLeave";
			dataGridViewCellStyle4.Format = "N2";
			col_CasualLeaves.DefaultCellStyle = dataGridViewCellStyle4;
			col_CasualLeaves.HeaderText = "C Lv";
			col_CasualLeaves.Name = "col_CasualLeaves";
			col_CasualLeaves.ReadOnly = true;
			col_CasualLeaves.Width = 40;
			col_BasicSalary.DataPropertyName = "BasicSalary";
			dataGridViewCellStyle5.Format = "N2";
			col_BasicSalary.DefaultCellStyle = dataGridViewCellStyle5;
			col_BasicSalary.HeaderText = "Basic";
			col_BasicSalary.Name = "col_BasicSalary";
			col_BasicSalary.ReadOnly = true;
			col_CSalary.DataPropertyName = "CSalary";
			dataGridViewCellStyle6.Format = "N2";
			col_CSalary.DefaultCellStyle = dataGridViewCellStyle6;
			col_CSalary.HeaderText = "Salary For Period";
			col_CSalary.Name = "col_CSalary";
			col_CSalary.ReadOnly = true;
			col_LeaveCut.DataPropertyName = "LeaveDiduction";
			dataGridViewCellStyle7.Format = "N2";
			col_LeaveCut.DefaultCellStyle = dataGridViewCellStyle7;
			col_LeaveCut.HeaderText = "Leave Cut";
			col_LeaveCut.Name = "col_LeaveCut";
			col_LeaveCut.ReadOnly = true;
			col_Incentive.DataPropertyName = "Incentive";
			dataGridViewCellStyle8.Format = "N2";
			col_Incentive.DefaultCellStyle = dataGridViewCellStyle8;
			col_Incentive.HeaderText = "Incentive";
			col_Incentive.Name = "col_Incentive";
			col_Incentive.Width = 80;
			col_Others.DataPropertyName = "OtherAmnt";
			dataGridViewCellStyle9.Format = "N2";
			col_Others.DefaultCellStyle = dataGridViewCellStyle9;
			col_Others.HeaderText = "Others";
			col_Others.Name = "col_Others";
			col_Others.Width = 80;
			col_SalaryCut.DataPropertyName = "SalaryCut";
			dataGridViewCellStyle10.Format = "N2";
			col_SalaryCut.DefaultCellStyle = dataGridViewCellStyle10;
			col_SalaryCut.HeaderText = "Salary Cut";
			col_SalaryCut.Name = "col_SalaryCut";
			col_SalaryCut.Width = 80;
			col_OtherDeduction.DataPropertyName = "OtherDiduction";
			dataGridViewCellStyle11.Format = "N2";
			col_OtherDeduction.DefaultCellStyle = dataGridViewCellStyle11;
			col_OtherDeduction.HeaderText = "Other Diduction";
			col_OtherDeduction.Name = "col_OtherDeduction";
			col_OtherDeduction.Width = 80;
			col_NetSalary.DataPropertyName = "NetSalary";
			dataGridViewCellStyle12.Format = "N2";
			col_NetSalary.DefaultCellStyle = dataGridViewCellStyle12;
			col_NetSalary.HeaderText = "Net Salary";
			col_NetSalary.Name = "col_NetSalary";
			col_NetSalary.ReadOnly = true;
			col_NetSalary.Width = 120;
			col_Advance.DataPropertyName = "SlryAdvance";
			dataGridViewCellStyle13.Format = "N2";
			col_Advance.DefaultCellStyle = dataGridViewCellStyle13;
			col_Advance.HeaderText = "Advance";
			col_Advance.Name = "col_Advance";
			col_PaidAmount.DataPropertyName = "PaidAmnt";
			dataGridViewCellStyle14.Format = "N2";
			dataGridViewCellStyle14.NullValue = null;
			col_PaidAmount.DefaultCellStyle = dataGridViewCellStyle14;
			col_PaidAmount.HeaderText = "Paid Amnt";
			col_PaidAmount.Name = "col_PaidAmount";
			col_Balance.DataPropertyName = "Balance";
			dataGridViewCellStyle15.Format = "N2";
			col_Balance.DefaultCellStyle = dataGridViewCellStyle15;
			col_Balance.HeaderText = "Balance";
			col_Balance.Name = "col_Balance";
			col_Balance.ReadOnly = true;
			col_Remarks.DataPropertyName = "Remarks";
			col_Remarks.HeaderText = "Remarks";
			col_Remarks.Name = "col_Remarks";
			col_Remarks.Width = 120;
			txtDscrptn.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtDscrptn.Data = 0;
			txtDscrptn.EnterColor = System.Drawing.Color.LightPink;
			txtDscrptn.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtDscrptn.Format = null;
			txtDscrptn.isAllowNegative = false;
			txtDscrptn.isAllowSpecialChar = false;
			txtDscrptn.isNumberOnly = false;
			txtDscrptn.isNumeric = false;
			txtDscrptn.LeaveColor = System.Drawing.Color.White;
			txtDscrptn.Location = new System.Drawing.Point(106, 77);
			txtDscrptn.Margin = new System.Windows.Forms.Padding(4);
			txtDscrptn.Name = "txtDscrptn";
			txtDscrptn.Size = new System.Drawing.Size(802, 16);
			txtDscrptn.TabIndex = 8;
			txtDscrptn.Value = new decimal(new int[4]);
			txtDscrptn.WaterMark = null;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label8.Location = new System.Drawing.Point(14, 17);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(105, 16);
			label8.TabIndex = 4;
			label8.Text = "Salary Period :";
			button4.Location = new System.Drawing.Point(-1, 26);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(157, 1);
			button4.TabIndex = 104;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(5, 2);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(148, 24);
			lblHead.TabIndex = 103;
			lblHead.Text = "Salary Process";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label7.Location = new System.Drawing.Point(16, 74);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(92, 16);
			label7.TabIndex = 7;
			label7.Text = "Description :";
			btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoad.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			btnLoad.ForeColor = System.Drawing.Color.White;
			btnLoad.Location = new System.Drawing.Point(501, 32);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new System.Drawing.Size(75, 30);
			btnLoad.TabIndex = 6;
			btnLoad.TabStop = false;
			btnLoad.Text = "Load";
			btnLoad.UseVisualStyleBackColor = false;
			btnLoad.Click += new System.EventHandler(btnLoad_Click);
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(346, 41);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 5;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label4.Location = new System.Drawing.Point(308, 41);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(36, 16);
			label4.TabIndex = 4;
			label4.Text = "To :";
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(782, 39);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 5;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label12.Location = new System.Drawing.Point(643, 40);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(109, 16);
			label12.TabIndex = 4;
			label12.Text = "Voucher Date :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(346, 14);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 23);
			dtmFrom.TabIndex = 5;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label3.Location = new System.Drawing.Point(293, 15);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(51, 16);
			label3.TabIndex = 4;
			label3.Text = "From :";
			ashGradientPanel1.AllowMultiSelect = false;
			ashGradientPanel1.Angle = 0f;
			ashGradientPanel1.BackColor = System.Drawing.Color.DodgerBlue;
			ashGradientPanel1.BottomColor = System.Drawing.Color.DodgerBlue;
			ashGradientPanel1.Controls.Add(btnSelected);
			ashGradientPanel1.Controls.Add(btnSalaryReport);
			ashGradientPanel1.Controls.Add(btnSalaryProcess);
			ashGradientPanel1.Location = new System.Drawing.Point(-2, 31);
			ashGradientPanel1.Name = "ashGradientPanel1";
			ashGradientPanel1.Selected = false;
			ashGradientPanel1.Size = new System.Drawing.Size(150, 540);
			ashGradientPanel1.TabIndex = 105;
			ashGradientPanel1.TextAdjestmentHeight = 0;
			ashGradientPanel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			ashGradientPanel1.TopColor = System.Drawing.Color.White;
			btnSelected.AllowMultiSelect = false;
			btnSelected.Angle = 0f;
			btnSelected.BottomColor = System.Drawing.Color.Empty;
			btnSelected.Location = new System.Drawing.Point(3, 166);
			btnSelected.Name = "btnSelected";
			btnSelected.Selected = false;
			btnSelected.Size = new System.Drawing.Size(6, 30);
			btnSelected.TabIndex = 1;
			btnSelected.TextAdjestmentHeight = 0;
			btnSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			btnSelected.TopColor = System.Drawing.Color.Empty;
			btnSalaryReport.BackColor = System.Drawing.Color.Transparent;
			btnSalaryReport.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnSalaryReport.FlatAppearance.BorderSize = 0;
			btnSalaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSalaryReport.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnSalaryReport.ForeColor = System.Drawing.Color.Black;
			btnSalaryReport.Location = new System.Drawing.Point(3, 196);
			btnSalaryReport.Name = "btnSalaryReport";
			btnSalaryReport.Size = new System.Drawing.Size(147, 30);
			btnSalaryReport.TabIndex = 0;
			btnSalaryReport.Text = "Salary Report";
			btnSalaryReport.UseVisualStyleBackColor = false;
			btnSalaryReport.Click += new System.EventHandler(btnSalaryReport_Click);
			btnSalaryProcess.BackColor = System.Drawing.Color.White;
			btnSalaryProcess.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnSalaryProcess.FlatAppearance.BorderSize = 0;
			btnSalaryProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSalaryProcess.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnSalaryProcess.ForeColor = System.Drawing.Color.Black;
			btnSalaryProcess.Location = new System.Drawing.Point(3, 166);
			btnSalaryProcess.Name = "btnSalaryProcess";
			btnSalaryProcess.Size = new System.Drawing.Size(147, 30);
			btnSalaryProcess.TabIndex = 0;
			btnSalaryProcess.Text = "Salary Process";
			btnSalaryProcess.UseVisualStyleBackColor = false;
			btnSalaryProcess.Click += new System.EventHandler(btnSalaryProcess_Click);
			cmbSlryPrdRpt.EnterColor = System.Drawing.Color.LightPink;
			cmbSlryPrdRpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSlryPrdRpt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbSlryPrdRpt.FormattingEnabled = true;
			cmbSlryPrdRpt.Items.AddRange(new object[5] { "Year", "Month", "Week", "Daily", "Period" });
			cmbSlryPrdRpt.LeaveColor = System.Drawing.Color.White;
			cmbSlryPrdRpt.Location = new System.Drawing.Point(115, 17);
			cmbSlryPrdRpt.Name = "cmbSlryPrdRpt";
			cmbSlryPrdRpt.Size = new System.Drawing.Size(121, 24);
			cmbSlryPrdRpt.TabIndex = 5;
			cmbSlryPrdRpt.Text = "Month";
			panel3.Location = new System.Drawing.Point(148, 27);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(920, 31);
			panel3.TabIndex = 106;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label5.Location = new System.Drawing.Point(636, 12);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(117, 16);
			label5.TabIndex = 2;
			label5.Text = "Cash/Bank A/C :";
			cmbSlryPrd.EnterColor = System.Drawing.Color.LightPink;
			cmbSlryPrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSlryPrd.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbSlryPrd.FormattingEnabled = true;
			cmbSlryPrd.Items.AddRange(new object[4] { "Year", "Month", "Week", "Daily" });
			cmbSlryPrd.LeaveColor = System.Drawing.Color.White;
			cmbSlryPrd.Location = new System.Drawing.Point(106, 46);
			cmbSlryPrd.Name = "cmbSlryPrd";
			cmbSlryPrd.Size = new System.Drawing.Size(121, 24);
			cmbSlryPrd.TabIndex = 3;
			cmbSlryPrd.Text = "Month";
			dtmToRpt.CustomFormat = "dd-MMM-yyyy";
			dtmToRpt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmToRpt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmToRpt.Location = new System.Drawing.Point(471, 19);
			dtmToRpt.Name = "dtmToRpt";
			dtmToRpt.Size = new System.Drawing.Size(126, 23);
			dtmToRpt.TabIndex = 8;
			dtmFrmRpt.CustomFormat = "dd-MMM-yyyy";
			dtmFrmRpt.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmFrmRpt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrmRpt.Location = new System.Drawing.Point(297, 18);
			dtmFrmRpt.Name = "dtmFrmRpt";
			dtmFrmRpt.Size = new System.Drawing.Size(126, 23);
			dtmFrmRpt.TabIndex = 9;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label10.Location = new System.Drawing.Point(247, 19);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(51, 16);
			label10.TabIndex = 7;
			label10.Text = "From :";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label2.Location = new System.Drawing.Point(3, 46);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(105, 16);
			label2.TabIndex = 2;
			label2.Text = "Salary Period :";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label9.Location = new System.Drawing.Point(429, 19);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(36, 16);
			label9.TabIndex = 6;
			label9.Text = "To :";
			nudVchrNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			nudVchrNo.Location = new System.Drawing.Point(106, 15);
			nudVchrNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudVchrNo.Maximum = new decimal(new int[4] { 1000000000, 0, 0, 0 });
			nudVchrNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVchrNo.Name = "nudVchrNo";
			nudVchrNo.Size = new System.Drawing.Size(120, 23);
			nudVchrNo.TabIndex = 1;
			nudVchrNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudVchrNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudVchrNo.ValueChanged += new System.EventHandler(nudVchrNo_ValueChanged);
			tabSalary.AllowDrop = true;
			tabSalary.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			tabSalary.Controls.Add(tabProcess);
			tabSalary.Controls.Add(tabReport);
			tabSalary.ItemSize = new System.Drawing.Size(460, 22);
			tabSalary.Location = new System.Drawing.Point(143, 31);
			tabSalary.Multiline = true;
			tabSalary.Name = "tabSalary";
			tabSalary.RightToLeftLayout = true;
			tabSalary.SelectedIndex = 0;
			tabSalary.Size = new System.Drawing.Size(930, 545);
			tabSalary.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			tabSalary.TabIndex = 102;
			tabProcess.BackColor = System.Drawing.Color.White;
			tabProcess.Controls.Add(cmbCashAcct);
			tabProcess.Controls.Add(btnClear);
			tabProcess.Controls.Add(btnDelete);
			tabProcess.Controls.Add(btnSave);
			tabProcess.Controls.Add(panel1);
			tabProcess.Controls.Add(txtDscrptn);
			tabProcess.Controls.Add(label7);
			tabProcess.Controls.Add(btnLoad);
			tabProcess.Controls.Add(dtmTo);
			tabProcess.Controls.Add(label4);
			tabProcess.Controls.Add(dtmDate);
			tabProcess.Controls.Add(label12);
			tabProcess.Controls.Add(dtmFrom);
			tabProcess.Controls.Add(label3);
			tabProcess.Controls.Add(label5);
			tabProcess.Controls.Add(cmbSlryPrd);
			tabProcess.Controls.Add(label2);
			tabProcess.Controls.Add(nudVchrNo);
			tabProcess.Controls.Add(label1);
			tabProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tabProcess.Location = new System.Drawing.Point(4, 26);
			tabProcess.Name = "tabProcess";
			tabProcess.Padding = new System.Windows.Forms.Padding(3);
			tabProcess.Size = new System.Drawing.Size(922, 515);
			tabProcess.TabIndex = 0;
			tabProcess.Text = "Salary Process";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label1.Location = new System.Drawing.Point(13, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(95, 16);
			label1.TabIndex = 0;
			label1.Text = "Voucher No :";
			tabReport.BackColor = System.Drawing.Color.White;
			tabReport.Controls.Add(btnClearRpt);
			tabReport.Controls.Add(btnExcel);
			tabReport.Controls.Add(panel2);
			tabReport.Controls.Add(btnLoadRpt);
			tabReport.Controls.Add(dtmToRpt);
			tabReport.Controls.Add(label9);
			tabReport.Controls.Add(dtmFrmRpt);
			tabReport.Controls.Add(label10);
			tabReport.Controls.Add(cmbSlryPrdRpt);
			tabReport.Controls.Add(label8);
			tabReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			tabReport.Location = new System.Drawing.Point(4, 26);
			tabReport.Name = "tabReport";
			tabReport.Padding = new System.Windows.Forms.Padding(3);
			tabReport.Size = new System.Drawing.Size(922, 515);
			tabReport.TabIndex = 1;
			tabReport.Text = "Salary Report";
			btnClearRpt.BackColor = System.Drawing.Color.DodgerBlue;
			btnClearRpt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClearRpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClearRpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClearRpt.ForeColor = System.Drawing.Color.White;
			btnClearRpt.Location = new System.Drawing.Point(833, 472);
			btnClearRpt.Name = "btnClearRpt";
			btnClearRpt.Size = new System.Drawing.Size(75, 30);
			btnClearRpt.TabIndex = 84;
			btnClearRpt.Text = "Clear";
			btnClearRpt.UseVisualStyleBackColor = false;
			btnClearRpt.Click += new System.EventHandler(btnClearRpt_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(744, 472);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(75, 30);
			btnExcel.TabIndex = 82;
			btnExcel.Text = "Excel";
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			panel2.BackColor = System.Drawing.Color.DodgerBlue;
			panel2.Controls.Add(dgvReport);
			panel2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel2.Location = new System.Drawing.Point(8, 50);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(900, 409);
			panel2.TabIndex = 11;
			dgvReport.AllowUserToAddRows = false;
			dgvReport.AllowUserToDeleteRows = false;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
			dgvReport.BackgroundColor = System.Drawing.Color.White;
			dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvReport.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn12, dataGridViewTextBoxColumn13, dataGridViewTextBoxColumn14, dataGridViewTextBoxColumn15);
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvReport.DefaultCellStyle = dataGridViewCellStyle17;
			dgvReport.EnableHeadersVisualStyles = false;
			dgvReport.EnterKeyNavigation = false;
			dgvReport.LastKey = System.Windows.Forms.Keys.None;
			dgvReport.Location = new System.Drawing.Point(12, 13);
			dgvReport.Name = "dgvReport";
			dgvReport.ReadOnly = true;
			dgvReport.RowHeadersVisible = false;
			dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvReport.Size = new System.Drawing.Size(877, 382);
			dgvReport.TabIndex = 3;
			dataGridViewTextBoxColumn1.DataPropertyName = "SL";
			dataGridViewTextBoxColumn1.HeaderText = "SL";
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewTextBoxColumn1.Width = 50;
			dataGridViewTextBoxColumn2.DataPropertyName = "EmpName";
			dataGridViewTextBoxColumn2.HeaderText = "Staff Name";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.Width = 150;
			dataGridViewTextBoxColumn3.DataPropertyName = "LeavsTaken";
			dataGridViewCellStyle18.Format = "N2";
			dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle18;
			dataGridViewTextBoxColumn3.HeaderText = "Leaves";
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.ReadOnly = true;
			dataGridViewTextBoxColumn3.Width = 60;
			dataGridViewTextBoxColumn4.DataPropertyName = "CasualLeave";
			dataGridViewCellStyle19.Format = "N2";
			dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle19;
			dataGridViewTextBoxColumn4.HeaderText = "C Lv";
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.ReadOnly = true;
			dataGridViewTextBoxColumn4.Width = 40;
			dataGridViewTextBoxColumn5.DataPropertyName = "BasicSalary";
			dataGridViewCellStyle20.Format = "N2";
			dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle20;
			dataGridViewTextBoxColumn5.HeaderText = "Basic";
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewTextBoxColumn6.DataPropertyName = "LeaveDiduction";
			dataGridViewCellStyle21.Format = "N2";
			dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle21;
			dataGridViewTextBoxColumn6.HeaderText = "Leave Cut";
			dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			dataGridViewTextBoxColumn6.ReadOnly = true;
			dataGridViewTextBoxColumn7.DataPropertyName = "Incentive";
			dataGridViewCellStyle22.Format = "N2";
			dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle22;
			dataGridViewTextBoxColumn7.HeaderText = "Incentive";
			dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			dataGridViewTextBoxColumn7.ReadOnly = true;
			dataGridViewTextBoxColumn7.Width = 80;
			dataGridViewTextBoxColumn8.DataPropertyName = "OtherAmnt";
			dataGridViewCellStyle23.Format = "N2";
			dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle23;
			dataGridViewTextBoxColumn8.HeaderText = "Others";
			dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			dataGridViewTextBoxColumn8.ReadOnly = true;
			dataGridViewTextBoxColumn8.Width = 80;
			dataGridViewTextBoxColumn9.DataPropertyName = "SalaryCut";
			dataGridViewCellStyle24.Format = "N2";
			dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle24;
			dataGridViewTextBoxColumn9.HeaderText = "Salary Cut";
			dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			dataGridViewTextBoxColumn9.ReadOnly = true;
			dataGridViewTextBoxColumn9.Width = 80;
			dataGridViewTextBoxColumn10.DataPropertyName = "OtherDiduction";
			dataGridViewCellStyle25.Format = "N2";
			dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle25;
			dataGridViewTextBoxColumn10.HeaderText = "Other Diduction";
			dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			dataGridViewTextBoxColumn10.ReadOnly = true;
			dataGridViewTextBoxColumn10.Width = 80;
			dataGridViewTextBoxColumn11.DataPropertyName = "NetSalary";
			dataGridViewCellStyle26.Format = "N2";
			dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle26;
			dataGridViewTextBoxColumn11.HeaderText = "Net Salary";
			dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			dataGridViewTextBoxColumn11.ReadOnly = true;
			dataGridViewTextBoxColumn11.Width = 120;
			dataGridViewTextBoxColumn12.DataPropertyName = "SlryAdvance";
			dataGridViewCellStyle27.Format = "N2";
			dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle27;
			dataGridViewTextBoxColumn12.HeaderText = "Advance";
			dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			dataGridViewTextBoxColumn12.ReadOnly = true;
			dataGridViewTextBoxColumn13.DataPropertyName = "PaidAmnt";
			dataGridViewCellStyle28.Format = "N2";
			dataGridViewCellStyle28.NullValue = null;
			dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle28;
			dataGridViewTextBoxColumn13.HeaderText = "Paid Amnt";
			dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			dataGridViewTextBoxColumn13.ReadOnly = true;
			dataGridViewTextBoxColumn14.DataPropertyName = "Balance";
			dataGridViewCellStyle29.Format = "N2";
			dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle29;
			dataGridViewTextBoxColumn14.HeaderText = "Balance";
			dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			dataGridViewTextBoxColumn14.ReadOnly = true;
			dataGridViewTextBoxColumn15.DataPropertyName = "Remarks";
			dataGridViewTextBoxColumn15.HeaderText = "Remarks";
			dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			dataGridViewTextBoxColumn15.ReadOnly = true;
			dataGridViewTextBoxColumn15.Width = 120;
			cmbCashAcct.EnterColor = System.Drawing.Color.LightPink;
			cmbCashAcct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbCashAcct.FormattingEnabled = true;
			cmbCashAcct.LeaveColor = System.Drawing.Color.White;
			cmbCashAcct.Location = new System.Drawing.Point(782, 12);
			cmbCashAcct.Name = "cmbCashAcct";
			cmbCashAcct.Size = new System.Drawing.Size(121, 21);
			cmbCashAcct.TabIndex = 14;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1071, 574);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(ashGradientPanel1);
			base.Controls.Add(panel3);
			base.Controls.Add(tabSalary);
			base.Name = "SalaryProcessView";
			base.Load += new System.EventHandler(SalaryProcessView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(tabSalary, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(ashGradientPanel1, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			ashGradientPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)nudVchrNo).EndInit();
			tabSalary.ResumeLayout(false);
			tabProcess.ResumeLayout(false);
			tabProcess.PerformLayout();
			tabReport.ResumeLayout(false);
			tabReport.PerformLayout();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsSalary).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
