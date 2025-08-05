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
	public class VehicleView : MasterForm
	{
		private int? iVehcleId;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private AshTextBoxNormal txtRemarks;

		private Label label16;

		private AshTextBoxNormal txtEmi;

		private Label label15;

		private Label label14;

		private Label label1;

		private AshTextBoxNormal txtColor;

		private AshTextBoxNormal txtNo;

		private Label label13;

		private Label label12;

		private Label label3;

		private AshTextBoxNormal txtMake;

		private Label label11;

		private Label label4;

		private AshTextBoxNormal txtModel;

		private Label label10;

		private Label label5;

		private AshTextBoxNormal txtChasisNo;

		private Label label9;

		private AshTextBoxNormal txtYear;

		private Label label6;

		private AshTextBoxNormal txtEngineNo;

		private AshTextBoxNormal txtRCOwner;

		private Label label8;

		private Label label7;

		private AshTextBoxNormal txtOwnerShip;

		private AshDateTimePicker dtEMI;

		private AshDateTimePicker dtTax;

		private AshDateTimePicker dtPolution;

		private AshDateTimePicker dtInsurence;

		private Label label2;

		private AshTextBoxNormal txtName;

		public UserPrevilegeDtl plgDtls { get; set; }

		public VehicleView()
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

		private VehicleEntity EntityMethod()
		{
			return new VehicleEntity
			{
				VanId = iVehcleId,
				VehName = txtName.Text,
				Manufacturer = txtMake.Text,
				Model = txtModel.Text,
				YearOfModel = txtYear.Text,
				RegNo = txtNo.Text,
				Ownership = txtOwnerShip.Text,
				RegisterdOwner = txtRCOwner.Text,
				EngineNo = txtEngineNo.Text,
				ChasisNo = txtChasisNo.Text,
				InsurenceDate = dtInsurence.Value,
				TaxDate = dtTax.Value,
				PolusionDate = dtPolution.Value,
				BColor = txtColor.Text,
				LoanEMIDate = AniHelper.GetDate(dtEMI.Value).toDateTime(),
				LoanEMIAmount = txtEmi.Text,
				Remarks = txtRemarks.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		public void ReloadMethod(MasterSerchForm frm)
		{
			VehicleEntity vehicleEntity = db.Vehicles(new
			{
				VanId = frm.id
			}).FirstOrDefault();
			txtName.Text = vehicleEntity.VehName;
			iVehcleId = vehicleEntity.VanId.toInt32();
			txtMake.Text = vehicleEntity.Manufacturer;
			txtModel.Text = vehicleEntity.Model;
			txtYear.Text = vehicleEntity.YearOfModel;
			txtNo.Text = vehicleEntity.RegNo;
			txtOwnerShip.Text = vehicleEntity.Ownership;
			txtRCOwner.Text = vehicleEntity.RegisterdOwner;
			txtEngineNo.Text = vehicleEntity.EngineNo;
			txtChasisNo.Text = vehicleEntity.ChasisNo;
			dtInsurence.Value = vehicleEntity.InsurenceDate.ToDateTime();
			dtTax.Value = vehicleEntity.TaxDate.ToDateTime();
			dtPolution.Value = vehicleEntity.PolusionDate.ToDateTime();
			txtColor.Text = vehicleEntity.BColor;
			dtEMI.Value = vehicleEntity.LoanEMIDate.ToDateTime();
			txtEmi.Value = vehicleEntity.LoanEMIAmount.ToDecimal();
			txtRemarks.Text = vehicleEntity.Remarks;
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtNo.Focus();
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			dtInsurence.Value = DateTime.Now;
			dtPolution.Value = DateTime.Now;
			dtTax.Value = DateTime.Now;
			dtEMI.Value = DateTime.Now;
			btnDelete.Enabled = false;
			iVehcleId = null;
			txtName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Vehicle", "VanId", iVehcleId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			if (txtNo.Text != string.Empty)
			{
				if (!iVehcleId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(EntityMethod(), "Vehicle"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntityMethod(), "Vehicle", "VanId", iVehcleId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			else
			{
				MessageBox.Show("Please fill the feild");
			}
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Vehicle Name", "Registration No");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select VanId as id,VehName as name,RegNo as description From Vehicle");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.vehicle = this;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.VehicleView));
			dtEMI = new AshUsercontrol.AshDateTimePicker();
			dtTax = new AshUsercontrol.AshDateTimePicker();
			dtPolution = new AshUsercontrol.AshDateTimePicker();
			dtInsurence = new AshUsercontrol.AshDateTimePicker();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			label16 = new System.Windows.Forms.Label();
			txtEmi = new AshUsercontrol.AshTextBoxNormal();
			label15 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtColor = new AshUsercontrol.AshTextBoxNormal();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			txtNo = new AshUsercontrol.AshTextBoxNormal();
			label13 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			txtMake = new AshUsercontrol.AshTextBoxNormal();
			label11 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			txtModel = new AshUsercontrol.AshTextBoxNormal();
			label10 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			txtChasisNo = new AshUsercontrol.AshTextBoxNormal();
			label9 = new System.Windows.Forms.Label();
			txtYear = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			txtEngineNo = new AshUsercontrol.AshTextBoxNormal();
			txtRCOwner = new AshUsercontrol.AshTextBoxNormal();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			txtOwnerShip = new AshUsercontrol.AshTextBoxNormal();
			pnlButton.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(pnlButton, "pnlButton");
			resources.ApplyResources(pnlMaster, "pnlMaster");
			resources.ApplyResources(lblMasterTitle, "lblMasterTitle");
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.TabStop = false;
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.TabStop = false;
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.TabStop = false;
			resources.ApplyResources(btnSearch, "btnSearch");
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.TabStop = false;
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(dtEMI, "dtEMI");
			dtEMI.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtEMI.Name = "dtEMI";
			resources.ApplyResources(dtTax, "dtTax");
			dtTax.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtTax.Name = "dtTax";
			resources.ApplyResources(dtPolution, "dtPolution");
			dtPolution.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtPolution.Name = "dtPolution";
			resources.ApplyResources(dtInsurence, "dtInsurence");
			dtInsurence.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtInsurence.Name = "dtInsurence";
			resources.ApplyResources(txtRemarks, "txtRemarks");
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.Data = 0;
			txtRemarks.EnterColor = System.Drawing.Color.LightPink;
			txtRemarks.Format = null;
			txtRemarks.isAllowNegative = false;
			txtRemarks.isAllowSpecialChar = false;
			txtRemarks.isNumberOnly = false;
			txtRemarks.isNumeric = false;
			txtRemarks.LeaveColor = System.Drawing.Color.White;
			txtRemarks.Name = "txtRemarks";
			txtRemarks.Value = new decimal(new int[4]);
			txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRemarks_KeyDown);
			resources.ApplyResources(label16, "label16");
			label16.Name = "label16";
			resources.ApplyResources(txtEmi, "txtEmi");
			txtEmi.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtEmi.Data = 0;
			txtEmi.EnterColor = System.Drawing.Color.LightPink;
			txtEmi.Format = null;
			txtEmi.isAllowNegative = false;
			txtEmi.isAllowSpecialChar = false;
			txtEmi.isNumberOnly = false;
			txtEmi.isNumeric = true;
			txtEmi.LeaveColor = System.Drawing.Color.White;
			txtEmi.Name = "txtEmi";
			txtEmi.Value = new decimal(new int[4]);
			resources.ApplyResources(label15, "label15");
			label15.Name = "label15";
			resources.ApplyResources(label14, "label14");
			label14.Name = "label14";
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			resources.ApplyResources(txtColor, "txtColor");
			txtColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtColor.Data = 0;
			txtColor.EnterColor = System.Drawing.Color.LightPink;
			txtColor.Format = null;
			txtColor.isAllowNegative = false;
			txtColor.isAllowSpecialChar = false;
			txtColor.isNumberOnly = false;
			txtColor.isNumeric = false;
			txtColor.LeaveColor = System.Drawing.Color.White;
			txtColor.Name = "txtColor";
			txtColor.Value = new decimal(new int[4]);
			resources.ApplyResources(txtName, "txtName");
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Name = "txtName";
			txtName.Value = new decimal(new int[4]);
			resources.ApplyResources(txtNo, "txtNo");
			txtNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtNo.Data = 0;
			txtNo.EnterColor = System.Drawing.Color.LightPink;
			txtNo.Format = null;
			txtNo.isAllowNegative = false;
			txtNo.isAllowSpecialChar = false;
			txtNo.isNumberOnly = false;
			txtNo.isNumeric = false;
			txtNo.LeaveColor = System.Drawing.Color.White;
			txtNo.Name = "txtNo";
			txtNo.Value = new decimal(new int[4]);
			resources.ApplyResources(label13, "label13");
			label13.Name = "label13";
			resources.ApplyResources(label12, "label12");
			label12.Name = "label12";
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			resources.ApplyResources(txtMake, "txtMake");
			txtMake.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtMake.Data = 0;
			txtMake.EnterColor = System.Drawing.Color.LightPink;
			txtMake.Format = null;
			txtMake.isAllowNegative = false;
			txtMake.isAllowSpecialChar = false;
			txtMake.isNumberOnly = false;
			txtMake.isNumeric = false;
			txtMake.LeaveColor = System.Drawing.Color.White;
			txtMake.Name = "txtMake";
			txtMake.Value = new decimal(new int[4]);
			resources.ApplyResources(label11, "label11");
			label11.Name = "label11";
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			resources.ApplyResources(txtModel, "txtModel");
			txtModel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtModel.Data = 0;
			txtModel.EnterColor = System.Drawing.Color.LightPink;
			txtModel.Format = null;
			txtModel.isAllowNegative = false;
			txtModel.isAllowSpecialChar = false;
			txtModel.isNumberOnly = false;
			txtModel.isNumeric = false;
			txtModel.LeaveColor = System.Drawing.Color.White;
			txtModel.Name = "txtModel";
			txtModel.Value = new decimal(new int[4]);
			resources.ApplyResources(label10, "label10");
			label10.Name = "label10";
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			resources.ApplyResources(txtChasisNo, "txtChasisNo");
			txtChasisNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtChasisNo.Data = 0;
			txtChasisNo.EnterColor = System.Drawing.Color.LightPink;
			txtChasisNo.Format = null;
			txtChasisNo.isAllowNegative = false;
			txtChasisNo.isAllowSpecialChar = false;
			txtChasisNo.isNumberOnly = false;
			txtChasisNo.isNumeric = false;
			txtChasisNo.LeaveColor = System.Drawing.Color.White;
			txtChasisNo.Name = "txtChasisNo";
			txtChasisNo.Value = new decimal(new int[4]);
			resources.ApplyResources(label9, "label9");
			label9.Name = "label9";
			resources.ApplyResources(txtYear, "txtYear");
			txtYear.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtYear.Data = 0;
			txtYear.EnterColor = System.Drawing.Color.LightPink;
			txtYear.Format = null;
			txtYear.isAllowNegative = false;
			txtYear.isAllowSpecialChar = false;
			txtYear.isNumberOnly = false;
			txtYear.isNumeric = false;
			txtYear.LeaveColor = System.Drawing.Color.White;
			txtYear.Name = "txtYear";
			txtYear.Value = new decimal(new int[4]);
			resources.ApplyResources(label6, "label6");
			label6.Name = "label6";
			resources.ApplyResources(txtEngineNo, "txtEngineNo");
			txtEngineNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtEngineNo.Data = 0;
			txtEngineNo.EnterColor = System.Drawing.Color.LightPink;
			txtEngineNo.Format = null;
			txtEngineNo.isAllowNegative = false;
			txtEngineNo.isAllowSpecialChar = false;
			txtEngineNo.isNumberOnly = false;
			txtEngineNo.isNumeric = false;
			txtEngineNo.LeaveColor = System.Drawing.Color.White;
			txtEngineNo.Name = "txtEngineNo";
			txtEngineNo.Value = new decimal(new int[4]);
			resources.ApplyResources(txtRCOwner, "txtRCOwner");
			txtRCOwner.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRCOwner.Data = 0;
			txtRCOwner.EnterColor = System.Drawing.Color.LightPink;
			txtRCOwner.Format = null;
			txtRCOwner.isAllowNegative = false;
			txtRCOwner.isAllowSpecialChar = false;
			txtRCOwner.isNumberOnly = false;
			txtRCOwner.isNumeric = false;
			txtRCOwner.LeaveColor = System.Drawing.Color.White;
			txtRCOwner.Name = "txtRCOwner";
			txtRCOwner.Value = new decimal(new int[4]);
			resources.ApplyResources(label8, "label8");
			label8.Name = "label8";
			resources.ApplyResources(label7, "label7");
			label7.Name = "label7";
			resources.ApplyResources(txtOwnerShip, "txtOwnerShip");
			txtOwnerShip.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtOwnerShip.Data = 0;
			txtOwnerShip.EnterColor = System.Drawing.Color.LightPink;
			txtOwnerShip.Format = null;
			txtOwnerShip.isAllowNegative = false;
			txtOwnerShip.isAllowSpecialChar = false;
			txtOwnerShip.isNumberOnly = false;
			txtOwnerShip.isNumeric = false;
			txtOwnerShip.LeaveColor = System.Drawing.Color.White;
			txtOwnerShip.Name = "txtOwnerShip";
			txtOwnerShip.Value = new decimal(new int[4]);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(dtEMI);
			base.Controls.Add(dtTax);
			base.Controls.Add(dtPolution);
			base.Controls.Add(label2);
			base.Controls.Add(dtInsurence);
			base.Controls.Add(txtOwnerShip);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label7);
			base.Controls.Add(label16);
			base.Controls.Add(txtEmi);
			base.Controls.Add(label8);
			base.Controls.Add(label15);
			base.Controls.Add(txtRCOwner);
			base.Controls.Add(label14);
			base.Controls.Add(txtColor);
			base.Controls.Add(txtEngineNo);
			base.Controls.Add(label13);
			base.Controls.Add(label6);
			base.Controls.Add(label12);
			base.Controls.Add(label11);
			base.Controls.Add(txtYear);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(txtChasisNo);
			base.Controls.Add(label5);
			base.Controls.Add(txtModel);
			base.Controls.Add(label1);
			base.Controls.Add(label4);
			base.Controls.Add(txtMake);
			base.Controls.Add(txtName);
			base.Controls.Add(label3);
			base.Controls.Add(txtNo);
			base.MaximizeBox = false;
			base.Name = "VehicleView";
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(txtNo, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(txtMake, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtModel, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(txtChasisNo, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(txtYear, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(txtEngineNo, 0);
			base.Controls.SetChildIndex(txtColor, 0);
			base.Controls.SetChildIndex(label14, 0);
			base.Controls.SetChildIndex(txtRCOwner, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(txtEmi, 0);
			base.Controls.SetChildIndex(label16, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(txtOwnerShip, 0);
			base.Controls.SetChildIndex(dtInsurence, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtPolution, 0);
			base.Controls.SetChildIndex(dtTax, 0);
			base.Controls.SetChildIndex(dtEMI, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
