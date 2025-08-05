using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
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
	public class KOTPrinterView : MasterForm
	{
		private DBContext db = new DBContext();

		private int? iMasterId;

		private List<KOTPrinter> kOTPrinters;

		private IContainer components = null;

		private AshTextBoxNormal txtName;

		private Label label3;

		private AshComboBox cmbPrinter;

		private Label label2;

		private AshComboBox cmbType;

		private Label label1;

		private AshComboBox cmbMode;

		private Label label4;

		private AshTextBoxNormal txtIPAddress;

		private Label label5;
        private DataGridViewTextBoxColumn col_Id;
        private DataGridViewTextBoxColumn col_Name;
        private DataGridViewTextBoxColumn col_Printer;
        private DataGridView dgDetails;

		public bool isFromAccount { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public KOTPrinterView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
		}

		private void DGVLoadMethod()
		{
			kOTPrinters = new List<KOTPrinter>();
			kOTPrinters = db.KOTPrinters(new
			{
				FK_BranchId = Settings.Default.iBranchId
			}).ToList();
			dgDetails.AutoGenerateColumns = false;
			dgDetails.DataSource = kOTPrinters;
		}

		private KOTPrinter EntityLoadMethod()
		{
			return new KOTPrinter
			{
				KOTName = txtName.Text,
				PrinterName = cmbPrinter.Text,
				ConnectionType = cmbType.Text,
				Mode = cmbMode.Text,
				Id = iMasterId,
				IPaddress = txtIPAddress.Text,
				FK_BranchId = Settings.Default.iBranchId
			};
		}

		private void KOTPrinterView_Load(object sender, EventArgs e)
		{
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cmbPrinter.Items.Add(installedPrinter);
			}
			DGVLoadMethod();
			base.ActiveControl = txtName;
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				KOTPrinter kOTPrinter = (KOTPrinter)dgDetails.CurrentRow.DataBoundItem;
				txtName.Text = kOTPrinter.KOTName;
				txtIPAddress.Text = kOTPrinter.IPaddress;
				cmbPrinter.Text = kOTPrinter.PrinterName;
				cmbType.Text = kOTPrinter.ConnectionType;
				cmbMode.Text = kOTPrinter.Mode;
				iMasterId = kOTPrinter.Id;
				btnDelete.Enabled = true;
				txtName.Focus();
			}
		}

		private void txtIPAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave.PerformClick();
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			DGVLoadMethod();
			iMasterId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				if (!iMasterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod()))
					{
						Messages.SavedMessage();
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "KOTPrinter", "Id", iMasterId.toInt32()))
					{
						Messages.UpdateMessage();
					}
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			if (plgDtls != null && !plgDtls.DeleteData)
			{
				Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
			}
			else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("KOTPrinter", "Id", iMasterId.toInt32()))
			{
				btnClear_Click(null, null);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KOTPrinterView));
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPrinter = new AshUsercontrol.AshComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMode = new AshUsercontrol.AshComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIPAddress = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.col_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Printer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbPrinter
            // 
            resources.ApplyResources(this.cmbPrinter, "cmbPrinter");
            this.cmbPrinter.EnterColor = System.Drawing.Color.LightPink;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.LeaveColor = System.Drawing.Color.White;
            this.cmbPrinter.Name = "cmbPrinter";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbType
            // 
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.EnterColor = System.Drawing.Color.LightPink;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            resources.GetString("cmbType.Items"),
            resources.GetString("cmbType.Items1")});
            this.cmbType.LeaveColor = System.Drawing.Color.White;
            this.cmbType.Name = "cmbType";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbMode
            // 
            resources.ApplyResources(this.cmbMode, "cmbMode");
            this.cmbMode.EnterColor = System.Drawing.Color.LightPink;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            resources.GetString("cmbMode.Items"),
            resources.GetString("cmbMode.Items1")});
            this.cmbMode.LeaveColor = System.Drawing.Color.White;
            this.cmbMode.Name = "cmbMode";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtIPAddress
            // 
            resources.ApplyResources(this.txtIPAddress, "txtIPAddress");
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIPAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIPAddress.Data = 0;
            this.txtIPAddress.EnterColor = System.Drawing.Color.LightPink;
            this.txtIPAddress.Format = null;
            this.txtIPAddress.isAllowNegative = false;
            this.txtIPAddress.isAllowSpecialChar = false;
            this.txtIPAddress.isNumberOnly = false;
            this.txtIPAddress.isNumeric = false;
            this.txtIPAddress.LeaveColor = System.Drawing.Color.White;
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtIPAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIPAddress_KeyDown);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Id,
            this.col_Name,
            this.col_Printer});
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            // 
            // col_Id
            // 
            this.col_Id.DataPropertyName = "Id";
            resources.ApplyResources(this.col_Id, "col_Id");
            this.col_Id.Name = "col_Id";
            this.col_Id.ReadOnly = true;
            // 
            // col_Name
            // 
            this.col_Name.DataPropertyName = "KOTName";
            resources.ApplyResources(this.col_Name, "col_Name");
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            // 
            // col_Printer
            // 
            this.col_Printer.DataPropertyName = "PrinterName";
            resources.ApplyResources(this.col_Printer, "col_Printer");
            this.col_Printer.Name = "col_Printer";
            this.col_Printer.ReadOnly = true;
            // 
            // KOTPrinterView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbPrinter);
            this.Controls.Add(this.label2);
            this.Name = "KOTPrinterView";
            this.Load += new System.EventHandler(this.KOTPrinterView_Load);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbPrinter, 0);
            this.Controls.SetChildIndex(this.cmbType, 0);
            this.Controls.SetChildIndex(this.cmbMode, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtIPAddress, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
