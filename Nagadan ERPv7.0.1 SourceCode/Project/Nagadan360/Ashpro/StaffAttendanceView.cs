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
	public class StaffAttendanceView : BaseForm
	{
		private DBContext db;

		private int iMasterId;

		private List<ExAttenSettings> exAttenSettings;

		private IContainer components = null;

		private AshComboBox cmbEmplySrch;

		private Label label5;

		private AshDateTimePicker dtmTo;

		private Label label4;

		private AshDateTimePicker dtmFrom;

		private Label label3;

		private ToolTip toolTip1;

		private Button btnLoad;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Button btnExcel;

		private AshGradientPanel ashGradientPanel1;

		private AshGradientPanel btnSelected;

		private Button btnReport;

		private Button btnAttendance;

		private Button button4;

		private Label lblHead;

		private Panel panel3;

		private TabControl tabAttendance;

		private TabPage tabEntry;

		private AshTextBoxNormal txtRemarks;

		private Label label6;

		private Panel panel1;

		private DataGridView dgvDetails;

		private CheckBox cbAfternoon;

		private CheckBox cbMorning;

		private AshDateTimePicker dtmDate;

		private Label label2;

		private AshUpDown nudInvNo;

		private Label label1;

		private TabPage tabReport;

		private Panel panel2;

		private DataGridView dgvReport;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_EmpName;

		private DataGridViewCheckBoxColumn col_Morning;

		private DataGridViewCheckBoxColumn col_Evening;

		private DataGridViewTextBoxColumn col_Leave;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column6;

		public StaffAttendanceView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void BindDGV()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exAttenSettings;
			dgvDetails.DataSource = bdsDetails;
		}

		private AttenMaster AttenMasterEmtityLoadMethod(int iInvNo)
		{
			return new AttenMaster
			{
				AttendanceMasterId = iMasterId,
				AtteDate = dtmDate.Value.Date,
				AtteInvNo = iInvNo,
				Remarks = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void LoadEmployeesMethod()
		{
			int sL = 1;
			foreach (EmployeeDetailsEntity item2 in from x in db.EmployeeDetails()
				where x.isActive
				select x)
			{
				ExAttenSettings item = new ExAttenSettings
				{
					SL = sL,
					EmpID = item2.EmpID.toInt32(),
					EmpName = item2.EmpName,
					Morning = true,
					Evening = true,
					AttenType = 0m,
					AtteDate = dtmDate.Value.Date
				};
				exAttenSettings.Add(item);
			}
			BindDGV();
		}

		private void StaffAttendanceView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			exAttenSettings = new List<ExAttenSettings>();
			nudInvNo.Value = ((from x in db.AttenMasters()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((AttenMaster x) => x.AtteInvNo) + 1) ?? 1;
			LoadEmployeesMethod();
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void cbMorning_CheckedChanged(object sender, EventArgs e)
		{
			double dMorning = 0.0;
			double dEvening = 0.0;
			exAttenSettings.ForEach(delegate(ExAttenSettings x)
			{
				x.Morning = cbMorning.Checked;
				dMorning = (x.Morning ? 0.0 : 0.5);
				dEvening = (x.Evening ? 0.0 : 0.5);
				x.AttenType = (dMorning + dEvening).ToDecimal();
			});
			BindDGV();
		}

		private void cbAfternoon_CheckedChanged(object sender, EventArgs e)
		{
			double dMorning = 0.0;
			double dEvening = 0.0;
			exAttenSettings.ForEach(delegate(ExAttenSettings x)
			{
				x.Evening = cbAfternoon.Checked;
				dMorning = (x.Morning ? 0.0 : 0.5);
				dEvening = (x.Evening ? 0.0 : 0.5);
				x.AttenType = (dMorning + dEvening).ToDecimal();
			});
			BindDGV();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (btnSave.Text == "Save")
			{
				int iInvNo = ((from x in db.AttenMasters()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((AttenMaster x) => x.AtteInvNo) + 1) ?? 1;
				AttenMaster attenMaster = AttenMasterEmtityLoadMethod(iInvNo);
				attenMaster.AttendanceMasterId = null;
				if (AshproCommon.InsertToDatabaseObj(attenMaster))
				{
					iMasterId = db.AttenMasters().Single((AttenMaster x) => x.AtteInvNo == iInvNo && x.FK_BranchId == Settings.Default.iBranchId).AttendanceMasterId.toInt32();
					List<AttenSettings> list = AniHelper.CopyListData<AttenSettings>(exAttenSettings.Cast<object>().ToList());
					list.ForEach(delegate(AttenSettings x)
					{
						x.AttenSettingsId = null;
						x.AttendanceMasterId = iMasterId;
						x.AtteDate = dtmDate.Value.Date;
					});
					if (AshproCommon.InsertToDatabase(list.Cast<object>().ToList()))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
			}
			else if (AshproCommon.DeleteFromDatabase("AttenSettings", "AttendanceMasterId", iMasterId) && AshproCommon.UpdateToDatabaseObj(AttenMasterEmtityLoadMethod(nudInvNo.Value.ToInt32()), "AttenMaster", "AttendanceMasterId", iMasterId))
			{
				List<AttenSettings> list2 = AniHelper.CopyListData<AttenSettings>(exAttenSettings.Cast<object>().ToList());
				list2.ForEach(delegate(AttenSettings x)
				{
					x.AttenSettingsId = null;
					x.AttendanceMasterId = iMasterId;
					x.AtteDate = dtmDate.Value.Date;
				});
				if (AshproCommon.InsertToDatabase(list2.Cast<object>().ToList()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("AttenSettings", "AttendanceMasterId", iMasterId) && AshproCommon.DeleteFromDatabase("AttenMaster", "AttendanceMasterId", iMasterId))
			{
				btnClear_Click(null, null);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			dtmDate.Value = DateTime.Now;
			cbMorning.Checked = false;
			cbAfternoon.Checked = false;
			txtRemarks.Text = string.Empty;
			nudInvNo.Value = ((from x in db.AttenMasters()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((AttenMaster x) => x.AtteInvNo) + 1) ?? 1;
			LoadEmployeesMethod();
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
		}

		private void nudInvNo_ValueChanged(object sender, EventArgs e)
		{
			exAttenSettings = new List<ExAttenSettings>();
			AttenMaster attenMaster = (from x in db.AttenMasters()
				where x.AtteInvNo == nudInvNo.Value.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
				select x).FirstOrDefault();
			if (attenMaster == null)
			{
				return;
			}
			iMasterId = attenMaster.AttendanceMasterId.toInt32();
			dtmDate.Value = attenMaster.AtteDate;
			txtRemarks.Text = attenMaster.Remarks;
			List<AttenSettings> source = (from x in db.AttenSettings()
				where x.AttendanceMasterId == iMasterId
				select x).ToList();
			exAttenSettings = AniHelper.CopyListData<ExAttenSettings>(source.Cast<object>().ToList());
			int num = 1;
			foreach (ExAttenSettings exAttenSetting in exAttenSettings)
			{
				exAttenSetting.SL = num;
				num++;
			}
			BindDGV();
			btnSave.Text = "Update";
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//AniHelper.ExportToExcelMethod(dgvReport, "Attendance Report from " + dtmFrom.Text + " to " + dtmTo.Text);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			List<ExAttenSettings> list = new List<ExAttenSettings>();
			int num = 1;
			foreach (EmployeeDetailsEntity item in from x in db.EmployeeDetails()
				where x.isActive && x.FK_BranchId == Settings.Default.iBranchId
				select x)
			{
				ExAttenSettings item2 = new ExAttenSettings
				{
					SL = num,
					EmpID = item.EmpID.toInt32(),
					EmpName = item.EmpName,
					AttenType = (from x in db.AttenSettings()
						where x.EmpID == item.EmpID && x.AtteDate > dtmFrom.Value.Date.AddDays(-1.0) && x.AtteDate < dtmTo.Value.Date.AddDays(1.0)
						select x).Sum((AttenSettings y) => y.AttenType)
				};
				list.Add(item2);
				num++;
			}
			dgvReport.AutoGenerateColumns = false;
			dgvReport.DataSource = list;
		}

		private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			double num = 0.0;
			double num2 = 0.0;
			if (dgvDetails.CurrentRow != null)
			{
				ExAttenSettings exAttenSettings = (ExAttenSettings)dgvDetails.CurrentRow.DataBoundItem;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_Morning.Name || dgvDetails.CurrentCell.OwningColumn.Name == col_Evening.Name)
				{
					dgvDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
					num = (exAttenSettings.Morning ? 0.0 : 0.5);
					num2 = (exAttenSettings.Evening ? 0.0 : 0.5);
					exAttenSettings.AttenType = (num + num2).ToDecimal();
				}
			}
		}

		private void btnAttendance_Click(object sender, EventArgs e)
		{
			tabAttendance.SelectedTab = tabEntry;
			btnAttendance.BackColor = Color.White;
			btnAttendance.ForeColor = Color.Black;
			btnReport.BackColor = Color.Transparent;
			btnReport.ForeColor = Color.Black;
			btnSelected.Top = btnAttendance.Top;
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			tabAttendance.SelectedTab = tabReport;
			btnReport.BackColor = Color.White;
			btnReport.ForeColor = Color.Black;
			btnAttendance.BackColor = Color.Transparent;
			btnAttendance.ForeColor = Color.Black;
			btnSelected.Top = btnReport.Top;
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
			cmbEmplySrch = new AshUsercontrol.AshComboBox();
			label5 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			label4 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label3 = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			btnLoad = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			ashGradientPanel1 = new AshUsercontrol.AshGradientPanel();
			btnSelected = new AshUsercontrol.AshGradientPanel();
			btnReport = new System.Windows.Forms.Button();
			btnAttendance = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			tabAttendance = new System.Windows.Forms.TabControl();
			tabEntry = new System.Windows.Forms.TabPage();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new System.Windows.Forms.DataGridView();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Morning = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Evening = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Leave = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cbAfternoon = new System.Windows.Forms.CheckBox();
			cbMorning = new System.Windows.Forms.CheckBox();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			nudInvNo = new AshUsercontrol.AshUpDown();
			label1 = new System.Windows.Forms.Label();
			tabReport = new System.Windows.Forms.TabPage();
			panel2 = new System.Windows.Forms.Panel();
			dgvReport = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			ashGradientPanel1.SuspendLayout();
			tabAttendance.SuspendLayout();
			tabEntry.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudInvNo).BeginInit();
			tabReport.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(874, 0);
			cmbEmplySrch.EnterColor = System.Drawing.Color.LightPink;
			cmbEmplySrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbEmplySrch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbEmplySrch.FormattingEnabled = true;
			cmbEmplySrch.LeaveColor = System.Drawing.Color.White;
			cmbEmplySrch.Location = new System.Drawing.Point(433, 20);
			cmbEmplySrch.Name = "cmbEmplySrch";
			cmbEmplySrch.Size = new System.Drawing.Size(235, 24);
			cmbEmplySrch.TabIndex = 5;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label5.Location = new System.Drawing.Point(351, 24);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(81, 16);
			label5.TabIndex = 4;
			label5.Text = "Employee :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(220, 19);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(126, 23);
			dtmTo.TabIndex = 3;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label4.Location = new System.Drawing.Point(185, 23);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(36, 16);
			label4.TabIndex = 2;
			label4.Text = "To :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(52, 17);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(126, 23);
			dtmFrom.TabIndex = 1;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label3.Location = new System.Drawing.Point(7, 21);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(46, 16);
			label3.TabIndex = 0;
			label3.Text = "From:";
			btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoad.ForeColor = System.Drawing.Color.White;
			btnLoad.Location = new System.Drawing.Point(672, 16);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new System.Drawing.Size(75, 30);
			btnLoad.TabIndex = 6;
			btnLoad.Text = "Load";
			toolTip1.SetToolTip(btnLoad, "Press Alt + L");
			btnLoad.UseVisualStyleBackColor = false;
			btnLoad.Click += new System.EventHandler(btnLoad_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(678, 392);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 12;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Location = new System.Drawing.Point(601, 392);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 11;
			btnDelete.Text = "Delete";
			toolTip1.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(524, 392);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 10;
			btnSave.Text = "Save";
			toolTip1.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(674, 374);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(75, 30);
			btnExcel.TabIndex = 10;
			btnExcel.Text = "Excel";
			toolTip1.SetToolTip(btnExcel, "Press Alt + E");
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			ashGradientPanel1.AllowMultiSelect = false;
			ashGradientPanel1.Angle = 0f;
			ashGradientPanel1.BackColor = System.Drawing.Color.DodgerBlue;
			ashGradientPanel1.BottomColor = System.Drawing.Color.DodgerBlue;
			ashGradientPanel1.Controls.Add(btnSelected);
			ashGradientPanel1.Controls.Add(btnReport);
			ashGradientPanel1.Controls.Add(btnAttendance);
			ashGradientPanel1.Location = new System.Drawing.Point(-4, 29);
			ashGradientPanel1.Name = "ashGradientPanel1";
			ashGradientPanel1.Selected = false;
			ashGradientPanel1.Size = new System.Drawing.Size(150, 454);
			ashGradientPanel1.TabIndex = 104;
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
			btnReport.BackColor = System.Drawing.Color.Transparent;
			btnReport.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnReport.FlatAppearance.BorderSize = 0;
			btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnReport.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnReport.ForeColor = System.Drawing.Color.Black;
			btnReport.Location = new System.Drawing.Point(3, 196);
			btnReport.Name = "btnReport";
			btnReport.Size = new System.Drawing.Size(147, 30);
			btnReport.TabIndex = 0;
			btnReport.Text = "Attendance Report";
			btnReport.UseVisualStyleBackColor = false;
			btnReport.Click += new System.EventHandler(btnReport_Click);
			btnAttendance.BackColor = System.Drawing.Color.White;
			btnAttendance.FlatAppearance.BorderColor = System.Drawing.Color.White;
			btnAttendance.FlatAppearance.BorderSize = 0;
			btnAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnAttendance.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnAttendance.ForeColor = System.Drawing.Color.Black;
			btnAttendance.Location = new System.Drawing.Point(3, 166);
			btnAttendance.Name = "btnAttendance";
			btnAttendance.Size = new System.Drawing.Size(147, 30);
			btnAttendance.TabIndex = 0;
			btnAttendance.Text = "Staff Attendance";
			btnAttendance.UseVisualStyleBackColor = false;
			btnAttendance.Click += new System.EventHandler(btnAttendance_Click);
			button4.Location = new System.Drawing.Point(-3, 25);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(181, 1);
			button4.TabIndex = 103;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 1);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(161, 24);
			lblHead.TabIndex = 102;
			lblHead.Text = "Staff Attendance";
			panel3.Location = new System.Drawing.Point(146, 28);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(767, 31);
			panel3.TabIndex = 105;
			tabAttendance.AllowDrop = true;
			tabAttendance.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			tabAttendance.Controls.Add(tabEntry);
			tabAttendance.Controls.Add(tabReport);
			tabAttendance.ItemSize = new System.Drawing.Size(380, 22);
			tabAttendance.Location = new System.Drawing.Point(141, 30);
			tabAttendance.Multiline = true;
			tabAttendance.Name = "tabAttendance";
			tabAttendance.RightToLeftLayout = true;
			tabAttendance.SelectedIndex = 0;
			tabAttendance.Size = new System.Drawing.Size(779, 458);
			tabAttendance.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			tabAttendance.TabIndex = 101;
			tabEntry.BackColor = System.Drawing.Color.White;
			tabEntry.Controls.Add(txtRemarks);
			tabEntry.Controls.Add(label6);
			tabEntry.Controls.Add(panel1);
			tabEntry.Controls.Add(btnClear);
			tabEntry.Controls.Add(btnDelete);
			tabEntry.Controls.Add(cbAfternoon);
			tabEntry.Controls.Add(cbMorning);
			tabEntry.Controls.Add(btnSave);
			tabEntry.Controls.Add(dtmDate);
			tabEntry.Controls.Add(label2);
			tabEntry.Controls.Add(nudInvNo);
			tabEntry.Controls.Add(label1);
			tabEntry.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			tabEntry.Location = new System.Drawing.Point(4, 26);
			tabEntry.Name = "tabEntry";
			tabEntry.Padding = new System.Windows.Forms.Padding(3);
			tabEntry.Size = new System.Drawing.Size(771, 428);
			tabEntry.TabIndex = 0;
			tabEntry.Text = "Attendance Entry";
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.Data = 0;
			txtRemarks.EnterColor = System.Drawing.Color.LightPink;
			txtRemarks.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtRemarks.Format = null;
			txtRemarks.isAllowNegative = false;
			txtRemarks.isAllowSpecialChar = false;
			txtRemarks.isNumberOnly = false;
			txtRemarks.isNumeric = false;
			txtRemarks.LeaveColor = System.Drawing.Color.White;
			txtRemarks.Location = new System.Drawing.Point(83, 338);
			txtRemarks.Margin = new System.Windows.Forms.Padding(4);
			txtRemarks.Multiline = true;
			txtRemarks.Name = "txtRemarks";
			txtRemarks.Size = new System.Drawing.Size(659, 36);
			txtRemarks.TabIndex = 8;
			txtRemarks.Value = new decimal(new int[4]);
			txtRemarks.WaterMark = null;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label6.Location = new System.Drawing.Point(24, 338);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(68, 16);
			label6.TabIndex = 7;
			label6.Text = "Remarks:";
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			panel1.Location = new System.Drawing.Point(11, 45);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(740, 262);
			panel1.TabIndex = 6;
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_EmpName, col_Morning, col_Evening, col_Leave);
			dgvDetails.Location = new System.Drawing.Point(12, 16);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dgvDetails.Size = new System.Drawing.Size(715, 235);
			dgvDetails.TabIndex = 0;
			dgvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellContentClick);
			dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetails_DataError);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL No";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 60;
			col_EmpName.DataPropertyName = "EmpName";
			col_EmpName.HeaderText = "Employee Name";
			col_EmpName.Name = "col_EmpName";
			col_EmpName.ReadOnly = true;
			col_EmpName.Width = 350;
			col_Morning.DataPropertyName = "Morning";
			col_Morning.HeaderText = "Morning";
			col_Morning.Name = "col_Morning";
			col_Evening.DataPropertyName = "Evening";
			col_Evening.HeaderText = "Evening";
			col_Evening.Name = "col_Evening";
			col_Leave.DataPropertyName = "AttenType";
			col_Leave.HeaderText = "Leave Days";
			col_Leave.Name = "col_Leave";
			col_Leave.ReadOnly = true;
			cbAfternoon.AutoSize = true;
			cbAfternoon.Checked = true;
			cbAfternoon.CheckState = System.Windows.Forms.CheckState.Checked;
			cbAfternoon.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cbAfternoon.Location = new System.Drawing.Point(536, 22);
			cbAfternoon.Name = "cbAfternoon";
			cbAfternoon.Size = new System.Drawing.Size(112, 20);
			cbAfternoon.TabIndex = 5;
			cbAfternoon.Text = "Afternoon All";
			cbAfternoon.UseVisualStyleBackColor = true;
			cbAfternoon.CheckedChanged += new System.EventHandler(cbAfternoon_CheckedChanged);
			cbMorning.AutoSize = true;
			cbMorning.Checked = true;
			cbMorning.CheckState = System.Windows.Forms.CheckState.Checked;
			cbMorning.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cbMorning.Location = new System.Drawing.Point(426, 22);
			cbMorning.Name = "cbMorning";
			cbMorning.Size = new System.Drawing.Size(98, 20);
			cbMorning.TabIndex = 4;
			cbMorning.Text = "Morning All";
			cbMorning.UseVisualStyleBackColor = true;
			cbMorning.CheckedChanged += new System.EventHandler(cbMorning_CheckedChanged);
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(253, 17);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(126, 23);
			dtmDate.TabIndex = 3;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label2.Location = new System.Drawing.Point(205, 20);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(50, 16);
			label2.TabIndex = 2;
			label2.Text = "Date :";
			nudInvNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			nudInvNo.Location = new System.Drawing.Point(94, 17);
			nudInvNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			nudInvNo.Maximum = new decimal(new int[4] { 276447232, 23283, 0, 0 });
			nudInvNo.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.Name = "nudInvNo";
			nudInvNo.Size = new System.Drawing.Size(91, 23);
			nudInvNo.TabIndex = 1;
			nudInvNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			nudInvNo.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			nudInvNo.ValueChanged += new System.EventHandler(nudInvNo_ValueChanged);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label1.Location = new System.Drawing.Point(18, 20);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(62, 16);
			label1.TabIndex = 0;
			label1.Text = "Inv No :";
			tabReport.BackColor = System.Drawing.Color.White;
			tabReport.Controls.Add(btnExcel);
			tabReport.Controls.Add(panel2);
			tabReport.Controls.Add(btnLoad);
			tabReport.Controls.Add(cmbEmplySrch);
			tabReport.Controls.Add(label5);
			tabReport.Controls.Add(dtmTo);
			tabReport.Controls.Add(label4);
			tabReport.Controls.Add(dtmFrom);
			tabReport.Controls.Add(label3);
			tabReport.Location = new System.Drawing.Point(4, 26);
			tabReport.Name = "tabReport";
			tabReport.Padding = new System.Windows.Forms.Padding(3);
			tabReport.Size = new System.Drawing.Size(771, 428);
			tabReport.TabIndex = 1;
			tabReport.Text = "Attendance Report";
			panel2.BackColor = System.Drawing.Color.DodgerBlue;
			panel2.Controls.Add(dgvReport);
			panel2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			panel2.Location = new System.Drawing.Point(15, 56);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(735, 308);
			panel2.TabIndex = 7;
			dgvReport.AllowUserToAddRows = false;
			dgvReport.AllowUserToDeleteRows = false;
			dgvReport.BackgroundColor = System.Drawing.Color.White;
			dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvReport.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, Column6);
			dgvReport.Location = new System.Drawing.Point(12, 16);
			dgvReport.Name = "dgvReport";
			dgvReport.RowHeadersVisible = false;
			dgvReport.Size = new System.Drawing.Size(714, 278);
			dgvReport.TabIndex = 0;
			dataGridViewTextBoxColumn1.DataPropertyName = "SL";
			dataGridViewTextBoxColumn1.HeaderText = "SL No";
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.Width = 60;
			dataGridViewTextBoxColumn2.DataPropertyName = "EmpName";
			dataGridViewTextBoxColumn2.HeaderText = "Employee Name";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.Width = 350;
			Column6.DataPropertyName = "AttenType";
			Column6.HeaderText = "Leave";
			Column6.Name = "Column6";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(909, 488);
			base.Controls.Add(ashGradientPanel1);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(panel3);
			base.Controls.Add(tabAttendance);
			base.Name = "StaffAttendanceView";
			base.Load += new System.EventHandler(StaffAttendanceView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(tabAttendance, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(ashGradientPanel1, 0);
			ashGradientPanel1.ResumeLayout(false);
			tabAttendance.ResumeLayout(false);
			tabEntry.ResumeLayout(false);
			tabEntry.PerformLayout();
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)nudInvNo).EndInit();
			tabReport.ResumeLayout(false);
			tabReport.PerformLayout();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
