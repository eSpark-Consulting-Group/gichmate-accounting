using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class EmployeeDetails : MasterForm
	{
		private int? iEmpPrimeryKeyId;

		private EmployeeDetailsEntity employee;

		private DBContext db;

		private IContainer components = null;

		private AshDateTimePicker dtmJoining;

		private AshTextBoxNormal txtRemarks;

		private Label label16;

		private AshTextBoxNormal txtParentAdd;

		private Label label15;

		private Label label14;

		private Label label2;

		private Label label1;

		private AshTextBoxNormal txtParent;

		private AshTextBoxNormal txtName;

		private Label label13;

		private Label label12;

		private Label label3;

		private AshTextBoxNormal txtAddOne;

		private Label label11;

		private Label label4;

		private AshTextBoxNormal txtAddTwo;

		private Label label10;

		private Label label5;

		private AshTextBoxNormal txtDesignation;

		private Label label9;

		private AshTextBoxNormal txtAddThree;

		private Label label6;

		private Label label8;

		private Label label7;

		private AshTextBoxNormal txtContactNo;

		private AshUpDown nuEmpId;

		private Label label17;

		private AshTextBoxNormal txtAdhar;

		private AshTextBoxNormal txtSalary;

		private AshTextBoxNormal txtQualification;

		private AshTextBoxNormal txtRelation;

		private AshComboBox cmbGender;

		private AshTextBoxNormal txtParentPh;

		private AshTextBoxNormal txtEmail;

		private CheckBox cbActive;

		private AshTextBoxNormal txtCasual;

		private Label label18;

		public UserPrevilegeDtl plgDtls { get; set; }

		public EmployeeDetails()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
			base.ActiveControl = txtName;
		}

		private EmployeeDetailsEntity EntityLoadMethod(int iEmpID)
		{
			return new EmployeeDetailsEntity
			{
				EmpID = iEmpPrimeryKeyId,
				EmployeeID = iEmpID,
				EmpName = txtName.Text,
				AddressOne = txtAddOne.Text,
				AddressTwo = txtAddTwo.Text,
				AddressThree = txtAddThree.Text,
				PhNumber = txtContactNo.Text,
				EmailId = txtEmail.Text,
				Designation = txtDesignation.Text,
				Salary = txtSalary.Value,
				CasualLeave = txtCasual.Value,
				Gender = cmbGender.Text,
				Qualification = txtQualification.Text,
				ParentName = txtParent.Text,
				Relation = txtRelation.Text,
				ParentAddress = txtParentAdd.Text,
				ParentPhNo = txtParentPh.Text,
				Remarks = txtRemarks.Text,
				isActive = cbActive.Checked,
				AdharNo = txtAdhar.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void LoadControlDataFromDatabase(EmployeeDetailsEntity employee)
		{
			if (employee != null)
			{
				iEmpPrimeryKeyId = employee.EmpID.ToInt32();
				txtName.Text = employee.EmpName;
				txtAddOne.Text = employee.AddressOne;
				txtAddTwo.Text = employee.AddressTwo;
				txtAddThree.Text = employee.AddressThree;
				txtContactNo.Text = employee.PhNumber;
				txtEmail.Text = employee.EmailId;
				txtDesignation.Text = employee.Designation;
				txtSalary.Value = employee.Salary;
				txtCasual.Value = employee.CasualLeave;
				cmbGender.Text = employee.Gender;
				txtQualification.Text = employee.Qualification;
				txtParent.Text = employee.ParentName;
				txtRelation.Text = employee.Relation;
				txtParentAdd.Text = employee.ParentAddress;
				txtParentPh.Text = employee.ParentPhNo;
				txtRemarks.Text = employee.Remarks;
				cbActive.Checked = employee.isActive;
				txtAdhar.Text = employee.AdharNo;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
			}
		}

		private int GenerateSalaryAccountMethod(string sGroupName, string sAccountName)
		{
			try
			{
				AccountEntity data = new AccountEntity
				{
					AccountId = null,
					AGroupId = db.AccountGroup().Single((AccountGroupEntity x) => x.GroupName == sGroupName).AGroupId.toInt32(),
					AccountName = sAccountName,
					OpeningBalance = 0m,
					DebitOrCridt = "DR",
					isActive = true,
					IsBuiltIn = false
				};
				AshproCommon.InsertToDatabaseObj(data, "Account");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			return db.Accounts().Single((AccountEntity x) => x.AccountName == sAccountName).AccountId.toInt32();
		}

		private void EmployeeDetails_Load(object sender, EventArgs e)
		{
			try
			{
				db = new DBContext();
				nuEmpId.Value = ((from x in db.EmployeeDetails()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).Max((EmployeeDetailsEntity x) => x.EmployeeID) + 1) ?? 1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (!iEmpPrimeryKeyId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					employee = EntityLoadMethod(((from x in db.EmployeeDetails()
						where x.FK_BranchId == Settings.Default.iBranchId
						select x).Max((EmployeeDetailsEntity x) => x.EmployeeID) + 1) ?? 1);
					employee.SalaryAccntId = GenerateSalaryAccountMethod("INDIRECT EXPENSES", txtName.Text + " Salary Account");
					employee.SalaryAdvAccntId = GenerateSalaryAccountMethod("INDIRECT EXPENSES", txtName.Text + " Salary Advance Account");
					employee.SalaryPayableId = GenerateSalaryAccountMethod("SALARY PAYABLE", txtName.Text + " Salary Payable Account");
					if (AshproCommon.InsertToDatabaseObj(employee, "EmployeeDetails"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(nuEmpId.Value.ToInt32()), "EmployeeDetails", "EmpID", iEmpPrimeryKeyId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("EmployeeDetails", "EmpID", iEmpPrimeryKeyId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			nuEmpId.Value = ((from x in db.EmployeeDetails()
				where x.FK_BranchId == Settings.Default.iBranchId
				select x).Max((EmployeeDetailsEntity x) => x.EmployeeID) + 1) ?? 1;
			AniHelper.ClearMethod(this);
			iEmpPrimeryKeyId = null;
			btnDelete.ForeColor = Color.Black;
			cmbGender.SelectedIndex = 0;
			cbActive.Checked = true;
			txtName.Focus();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			EmployeeDetailsEntity employeeDetailsEntity = (from x in db.EmployeeDetails()
				where x.FK_BranchId == Settings.Default.iBranchId && x.EmployeeID == nuEmpId.Value.ToInt32()
				select x).FirstOrDefault();
			LoadControlDataFromDatabase(employeeDetailsEntity);
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Employee Name", "Employee Code");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select EmpID as id,EmpName as name,EmployeeID as description From EmployeeDetails");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.employee = this;
				masterSerchForm.TopLevel = false;
				masterSerchForm.Location = new Point(base.Width + 50, 25);
				if (!base.IsMdiChild)
				{
					TabPage tabPage = (TabPage)base.Parent;
					tabPage.Controls.Add(masterSerchForm);
				}
				else
				{
					masterSerchForm.MdiParent = base.MdiParent;
				}
				masterSerchForm.Show();
				masterSerchForm.BringToFront();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		public void DataFromSearch(MasterSerchForm frm)
		{
			EmployeeDetailsEntity employeeDetailsEntity = db.EmployeeDetails(new
			{
				EmpID = frm.id
			}).FirstOrDefault();
			LoadControlDataFromDatabase(employeeDetailsEntity);
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtName.Focus();
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave.PerformClick();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeDetails));
            this.dtmJoining = new AshUsercontrol.AshDateTimePicker();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label16 = new System.Windows.Forms.Label();
            this.txtParentAdd = new AshUsercontrol.AshTextBoxNormal();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtParent = new AshUsercontrol.AshTextBoxNormal();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddOne = new AshUsercontrol.AshTextBoxNormal();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddTwo = new AshUsercontrol.AshTextBoxNormal();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDesignation = new AshUsercontrol.AshTextBoxNormal();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddThree = new AshUsercontrol.AshTextBoxNormal();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContactNo = new AshUsercontrol.AshTextBoxNormal();
            this.nuEmpId = new AshUsercontrol.AshUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.txtAdhar = new AshUsercontrol.AshTextBoxNormal();
            this.txtSalary = new AshUsercontrol.AshTextBoxNormal();
            this.txtQualification = new AshUsercontrol.AshTextBoxNormal();
            this.txtRelation = new AshUsercontrol.AshTextBoxNormal();
            this.cmbGender = new AshUsercontrol.AshComboBox();
            this.txtParentPh = new AshUsercontrol.AshTextBoxNormal();
            this.txtEmail = new AshUsercontrol.AshTextBoxNormal();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.txtCasual = new AshUsercontrol.AshTextBoxNormal();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuEmpId)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            resources.ApplyResources(this.pnlButton, "pnlButton");
            // 
            // pnlMaster
            // 
            resources.ApplyResources(this.pnlMaster, "pnlMaster");
            // 
            // lblMasterTitle
            // 
            resources.ApplyResources(this.lblMasterTitle, "lblMasterTitle");
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.TabStop = false;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.TabStop = false;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.TabStop = false;
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // dtmJoining
            // 
            resources.ApplyResources(this.dtmJoining, "dtmJoining");
            this.dtmJoining.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmJoining.Name = "dtmJoining";
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemarks_KeyDown);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txtParentAdd
            // 
            resources.ApplyResources(this.txtParentAdd, "txtParentAdd");
            this.txtParentAdd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtParentAdd.Data = 0;
            this.txtParentAdd.EnterColor = System.Drawing.Color.LightPink;
            this.txtParentAdd.Format = null;
            this.txtParentAdd.isAllowNegative = false;
            this.txtParentAdd.isAllowSpecialChar = false;
            this.txtParentAdd.isNumberOnly = false;
            this.txtParentAdd.isNumeric = false;
            this.txtParentAdd.LeaveColor = System.Drawing.Color.White;
            this.txtParentAdd.Name = "txtParentAdd";
            this.txtParentAdd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtParent
            // 
            resources.ApplyResources(this.txtParent, "txtParent");
            this.txtParent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtParent.Data = 0;
            this.txtParent.EnterColor = System.Drawing.Color.LightPink;
            this.txtParent.Format = null;
            this.txtParent.isAllowNegative = false;
            this.txtParent.isAllowSpecialChar = false;
            this.txtParent.isNumberOnly = false;
            this.txtParent.isNumeric = false;
            this.txtParent.LeaveColor = System.Drawing.Color.White;
            this.txtParent.Name = "txtParent";
            this.txtParent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtAddOne
            // 
            resources.ApplyResources(this.txtAddOne, "txtAddOne");
            this.txtAddOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddOne.Data = 0;
            this.txtAddOne.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddOne.Format = null;
            this.txtAddOne.isAllowNegative = false;
            this.txtAddOne.isAllowSpecialChar = false;
            this.txtAddOne.isNumberOnly = false;
            this.txtAddOne.isNumeric = false;
            this.txtAddOne.LeaveColor = System.Drawing.Color.White;
            this.txtAddOne.Name = "txtAddOne";
            this.txtAddOne.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtAddTwo
            // 
            resources.ApplyResources(this.txtAddTwo, "txtAddTwo");
            this.txtAddTwo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddTwo.Data = 0;
            this.txtAddTwo.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddTwo.Format = null;
            this.txtAddTwo.isAllowNegative = false;
            this.txtAddTwo.isAllowSpecialChar = false;
            this.txtAddTwo.isNumberOnly = false;
            this.txtAddTwo.isNumeric = false;
            this.txtAddTwo.LeaveColor = System.Drawing.Color.White;
            this.txtAddTwo.Name = "txtAddTwo";
            this.txtAddTwo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtDesignation
            // 
            resources.ApplyResources(this.txtDesignation, "txtDesignation");
            this.txtDesignation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDesignation.Data = 0;
            this.txtDesignation.EnterColor = System.Drawing.Color.LightPink;
            this.txtDesignation.Format = null;
            this.txtDesignation.isAllowNegative = false;
            this.txtDesignation.isAllowSpecialChar = false;
            this.txtDesignation.isNumberOnly = false;
            this.txtDesignation.isNumeric = false;
            this.txtDesignation.LeaveColor = System.Drawing.Color.White;
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtAddThree
            // 
            resources.ApplyResources(this.txtAddThree, "txtAddThree");
            this.txtAddThree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddThree.Data = 0;
            this.txtAddThree.EnterColor = System.Drawing.Color.LightPink;
            this.txtAddThree.Format = null;
            this.txtAddThree.isAllowNegative = false;
            this.txtAddThree.isAllowSpecialChar = false;
            this.txtAddThree.isNumberOnly = false;
            this.txtAddThree.isNumeric = false;
            this.txtAddThree.LeaveColor = System.Drawing.Color.White;
            this.txtAddThree.Name = "txtAddThree";
            this.txtAddThree.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtContactNo
            // 
            resources.ApplyResources(this.txtContactNo, "txtContactNo");
            this.txtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContactNo.Data = 0;
            this.txtContactNo.EnterColor = System.Drawing.Color.LightPink;
            this.txtContactNo.Format = null;
            this.txtContactNo.isAllowNegative = false;
            this.txtContactNo.isAllowSpecialChar = false;
            this.txtContactNo.isNumberOnly = false;
            this.txtContactNo.isNumeric = false;
            this.txtContactNo.LeaveColor = System.Drawing.Color.White;
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nuEmpId
            // 
            resources.ApplyResources(this.nuEmpId, "nuEmpId");
            this.nuEmpId.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nuEmpId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuEmpId.Name = "nuEmpId";
            this.nuEmpId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuEmpId.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtAdhar
            // 
            resources.ApplyResources(this.txtAdhar, "txtAdhar");
            this.txtAdhar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAdhar.Data = 0;
            this.txtAdhar.EnterColor = System.Drawing.Color.LightPink;
            this.txtAdhar.Format = null;
            this.txtAdhar.isAllowNegative = false;
            this.txtAdhar.isAllowSpecialChar = false;
            this.txtAdhar.isNumberOnly = false;
            this.txtAdhar.isNumeric = false;
            this.txtAdhar.LeaveColor = System.Drawing.Color.White;
            this.txtAdhar.Name = "txtAdhar";
            this.txtAdhar.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtSalary
            // 
            resources.ApplyResources(this.txtSalary, "txtSalary");
            this.txtSalary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSalary.Data = 0;
            this.txtSalary.EnterColor = System.Drawing.Color.LightPink;
            this.txtSalary.Format = null;
            this.txtSalary.isAllowNegative = false;
            this.txtSalary.isAllowSpecialChar = false;
            this.txtSalary.isNumberOnly = false;
            this.txtSalary.isNumeric = true;
            this.txtSalary.LeaveColor = System.Drawing.Color.White;
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtQualification
            // 
            resources.ApplyResources(this.txtQualification, "txtQualification");
            this.txtQualification.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQualification.Data = 0;
            this.txtQualification.EnterColor = System.Drawing.Color.LightPink;
            this.txtQualification.Format = null;
            this.txtQualification.isAllowNegative = false;
            this.txtQualification.isAllowSpecialChar = false;
            this.txtQualification.isNumberOnly = false;
            this.txtQualification.isNumeric = false;
            this.txtQualification.LeaveColor = System.Drawing.Color.White;
            this.txtQualification.Name = "txtQualification";
            this.txtQualification.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtRelation
            // 
            resources.ApplyResources(this.txtRelation, "txtRelation");
            this.txtRelation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRelation.Data = 0;
            this.txtRelation.EnterColor = System.Drawing.Color.LightPink;
            this.txtRelation.Format = null;
            this.txtRelation.isAllowNegative = false;
            this.txtRelation.isAllowSpecialChar = false;
            this.txtRelation.isNumberOnly = false;
            this.txtRelation.isNumeric = false;
            this.txtRelation.LeaveColor = System.Drawing.Color.White;
            this.txtRelation.Name = "txtRelation";
            this.txtRelation.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbGender
            // 
            resources.ApplyResources(this.cmbGender, "cmbGender");
            this.cmbGender.EnterColor = System.Drawing.Color.LightPink;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            resources.GetString("cmbGender.Items"),
            resources.GetString("cmbGender.Items1"),
            resources.GetString("cmbGender.Items2")});
            this.cmbGender.LeaveColor = System.Drawing.Color.White;
            this.cmbGender.Name = "cmbGender";
            // 
            // txtParentPh
            // 
            resources.ApplyResources(this.txtParentPh, "txtParentPh");
            this.txtParentPh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtParentPh.Data = 0;
            this.txtParentPh.EnterColor = System.Drawing.Color.LightPink;
            this.txtParentPh.Format = null;
            this.txtParentPh.isAllowNegative = false;
            this.txtParentPh.isAllowSpecialChar = false;
            this.txtParentPh.isNumberOnly = false;
            this.txtParentPh.isNumeric = false;
            this.txtParentPh.LeaveColor = System.Drawing.Color.White;
            this.txtParentPh.Name = "txtParentPh";
            this.txtParentPh.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Data = 0;
            this.txtEmail.EnterColor = System.Drawing.Color.LightPink;
            this.txtEmail.Format = null;
            this.txtEmail.isAllowNegative = false;
            this.txtEmail.isAllowSpecialChar = false;
            this.txtEmail.isNumberOnly = false;
            this.txtEmail.isNumeric = false;
            this.txtEmail.LeaveColor = System.Drawing.Color.White;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cbActive
            // 
            resources.ApplyResources(this.cbActive, "cbActive");
            this.cbActive.Checked = true;
            this.cbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActive.Name = "cbActive";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // txtCasual
            // 
            resources.ApplyResources(this.txtCasual, "txtCasual");
            this.txtCasual.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCasual.Data = 0;
            this.txtCasual.EnterColor = System.Drawing.Color.LightPink;
            this.txtCasual.Format = null;
            this.txtCasual.isAllowNegative = false;
            this.txtCasual.isAllowSpecialChar = false;
            this.txtCasual.isNumberOnly = false;
            this.txtCasual.isNumeric = true;
            this.txtCasual.LeaveColor = System.Drawing.Color.White;
            this.txtCasual.Name = "txtCasual";
            this.txtCasual.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // EmployeeDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.nuEmpId);
            this.Controls.Add(this.dtmJoining);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtParentAdd);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtParent);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddOne);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAddTwo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCasual);
            this.Controls.Add(this.txtParentPh);
            this.Controls.Add(this.txtRelation);
            this.Controls.Add(this.txtQualification);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.txtAdhar);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtDesignation);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAddThree);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtContactNo);
            this.MaximizeBox = false;
            this.Name = "EmployeeDetails";
            this.Load += new System.EventHandler(this.EmployeeDetails_Load);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.txtContactNo, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtAddThree, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtDesignation, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.txtAdhar, 0);
            this.Controls.SetChildIndex(this.txtSalary, 0);
            this.Controls.SetChildIndex(this.txtQualification, 0);
            this.Controls.SetChildIndex(this.txtRelation, 0);
            this.Controls.SetChildIndex(this.txtParentPh, 0);
            this.Controls.SetChildIndex(this.txtCasual, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtAddTwo, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.txtAddOne, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtParent, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.txtParentAdd, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.dtmJoining, 0);
            this.Controls.SetChildIndex(this.nuEmpId, 0);
            this.Controls.SetChildIndex(this.cmbGender, 0);
            this.Controls.SetChildIndex(this.cbActive, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nuEmpId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
