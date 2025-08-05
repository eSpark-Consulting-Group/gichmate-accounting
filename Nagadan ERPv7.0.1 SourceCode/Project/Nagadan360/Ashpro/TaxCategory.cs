using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class TaxCategory : MasterForm
	{
		private int? iTCategoryId;

		private DBContext db;

		private IContainer components = null;

		private Label lblTax1;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshTextBoxNormal txtPCess;

		private AshTextBoxNormal txtSCess;

		private Label lblTax3;

		private AshTextBoxNormal txtPC;

		private AshTextBoxNormal txtSC;

		private Label lblTax2;

		private AshTextBoxNormal txtPX;

		private AshTextBoxNormal txtSX;

		private Label lblTax4;

		private Label label4;

		private AshTextBoxNormal txtPGST;

		private Label label1;

		private AshTextBoxNormal txtSGST;

		private GroupBox groupBox1;

		private ListView livTaxCategory;

		private ColumnHeader SrchAccountGroup;

		private ColumnHeader columnHeader19;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		public bool isOpenFromAnotherForm { get; set; }

		public string taxCategoryName { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public TaxCategory()
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
		}

		private void loadItems()
		{
			livTaxCategory.Items.Clear();
			try
			{
				foreach (TaxCategoryEntity item in db.TaxCategories())
				{
					ListViewItem listViewItem = new ListViewItem((livTaxCategory.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.TCategoryName);
					listViewItem.SubItems.Add(item.SalesVat.ToString(CommonSettings.strCurencyFormat));
					listViewItem.SubItems.Add(item.PurVat.ToString(CommonSettings.strCurencyFormat));
					livTaxCategory.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private TaxCategoryEntity EntityMethod()
		{
			return new TaxCategoryEntity
			{
				TCategoryId = iTCategoryId,
				TCategoryName = txtName.Text,
				SalesVat = txtSGST.Value,
				PurVat = txtPGST.Value,
				SaleCst = txtSC.Value,
				PurCst = txtPC.Value,
				SaleCess = txtSCess.Value,
				PurCess = txtPCess.Value,
				SalesXDuty = txtSX.Value,
				PurXDuty = txtPX.Value
			};
		}

		private void TaxCategory_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			loadItems();
			if (CommonSettings.blnGSTEnabled)
			{
				lblTax1.Text = "GST :";
				lblTax2.Text = "SGST :";
				lblTax3.Text = "CGST :";
				lblTax4.Text = "IGST :";
			}
			base.ActiveControl = txtName;
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text == string.Empty)
			{
				Messages.WarningMessage();
			}
			else if (!iTCategoryId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
				}
				else if (AshproCommon.InsertToDatabaseObj(EntityMethod(), "TaxCategory"))
				{
					Messages.SavedMessage();
					btnClear_Click(null, null);
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else if (AshproCommon.UpdateToDatabaseObj(EntityMethod(), "TaxCategory", "TCategoryId", iTCategoryId.ToInt32()))
			{
				Messages.UpdateMessage();
				btnClear_Click(null, null);
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
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("TaxCategory", "TCategoryId", iTCategoryId.ToInt32()))
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
			AniHelper.ClearMethod(this);
			loadItems();
			btnDelete.Enabled = false;
			iTCategoryId = null;
			txtName.Focus();
		}

		private void livTaxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livTaxCategory.SelectedItems.Count != 0)
			{
				TaxCategoryEntity taxCategoryEntity = db.TaxCategories().Find((TaxCategoryEntity x) => x.TCategoryName == livTaxCategory.SelectedItems[0].SubItems[1].Text);
				txtName.Text = taxCategoryEntity.TCategoryName;
				iTCategoryId = taxCategoryEntity.TCategoryId.ToInt32();
				txtSGST.Value = taxCategoryEntity.SalesVat;
				txtPGST.Value = taxCategoryEntity.PurVat;
				txtSC.Value = taxCategoryEntity.SaleCst;
				txtPC.Value = taxCategoryEntity.PurCst;
				txtSCess.Value = taxCategoryEntity.SaleCess;
				txtPCess.Value = taxCategoryEntity.PurCess;
				txtSX.Value = taxCategoryEntity.SalesXDuty;
				txtPX.Value = taxCategoryEntity.PurXDuty;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
			}
		}

		private void txtPX_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
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
            this.lblTax1 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPCess = new AshUsercontrol.AshTextBoxNormal();
            this.txtSCess = new AshUsercontrol.AshTextBoxNormal();
            this.lblTax3 = new System.Windows.Forms.Label();
            this.txtPC = new AshUsercontrol.AshTextBoxNormal();
            this.txtSC = new AshUsercontrol.AshTextBoxNormal();
            this.lblTax2 = new System.Windows.Forms.Label();
            this.txtPX = new AshUsercontrol.AshTextBoxNormal();
            this.txtSX = new AshUsercontrol.AshTextBoxNormal();
            this.lblTax4 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPGST = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSGST = new AshUsercontrol.AshTextBoxNormal();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.livTaxCategory = new System.Windows.Forms.ListView();
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SrchAccountGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.Location = new System.Drawing.Point(293, 536);
            // 
            // pnlMaster
            // 
            this.pnlMaster.Size = new System.Drawing.Size(150, 547);
            // 
            // lblMasterTitle
            // 
            this.lblMasterTitle.Size = new System.Drawing.Size(134, 24);
            this.lblMasterTitle.Text = "Tax Category";
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.BorderSize = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(619, 1);
            // 
            // lblTax1
            // 
            this.lblTax1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTax1.Location = new System.Drawing.Point(165, 91);
            this.lblTax1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTax1.Name = "lblTax1";
            this.lblTax1.Size = new System.Drawing.Size(145, 16);
            this.lblTax1.TabIndex = 4;
            this.lblTax1.Text = "VAT :";
            this.lblTax1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(324, 51);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(308, 16);
            this.txtName.TabIndex = 1;
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.WaterMark = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tax  Name :";
            // 
            // txtPCess
            // 
            this.txtPCess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPCess.Data = 0;
            this.txtPCess.EnterColor = System.Drawing.Color.LightPink;
            this.txtPCess.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPCess.Format = null;
            this.txtPCess.isAllowNegative = false;
            this.txtPCess.isAllowSpecialChar = false;
            this.txtPCess.isNumberOnly = false;
            this.txtPCess.isNumeric = true;
            this.txtPCess.LeaveColor = System.Drawing.Color.White;
            this.txtPCess.Location = new System.Drawing.Point(490, 145);
            this.txtPCess.Margin = new System.Windows.Forms.Padding(4);
            this.txtPCess.Name = "txtPCess";
            this.txtPCess.Size = new System.Drawing.Size(141, 16);
            this.txtPCess.TabIndex = 12;
            this.txtPCess.Text = "0";
            this.txtPCess.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPCess.WaterMark = null;
            // 
            // txtSCess
            // 
            this.txtSCess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSCess.Data = 0;
            this.txtSCess.EnterColor = System.Drawing.Color.LightPink;
            this.txtSCess.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtSCess.Format = null;
            this.txtSCess.isAllowNegative = false;
            this.txtSCess.isAllowSpecialChar = false;
            this.txtSCess.isNumberOnly = false;
            this.txtSCess.isNumeric = true;
            this.txtSCess.LeaveColor = System.Drawing.Color.White;
            this.txtSCess.Location = new System.Drawing.Point(323, 145);
            this.txtSCess.Margin = new System.Windows.Forms.Padding(4);
            this.txtSCess.Name = "txtSCess";
            this.txtSCess.Size = new System.Drawing.Size(121, 16);
            this.txtSCess.TabIndex = 11;
            this.txtSCess.Text = "0";
            this.txtSCess.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSCess.WaterMark = null;
            // 
            // lblTax3
            // 
            this.lblTax3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTax3.Location = new System.Drawing.Point(242, 141);
            this.lblTax3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTax3.Name = "lblTax3";
            this.lblTax3.Size = new System.Drawing.Size(68, 16);
            this.lblTax3.TabIndex = 10;
            this.lblTax3.Text = "Tax - 3 :";
            this.lblTax3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPC
            // 
            this.txtPC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPC.Data = 0;
            this.txtPC.EnterColor = System.Drawing.Color.LightPink;
            this.txtPC.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPC.Format = null;
            this.txtPC.isAllowNegative = false;
            this.txtPC.isAllowSpecialChar = false;
            this.txtPC.isNumberOnly = false;
            this.txtPC.isNumeric = true;
            this.txtPC.LeaveColor = System.Drawing.Color.White;
            this.txtPC.Location = new System.Drawing.Point(490, 121);
            this.txtPC.Margin = new System.Windows.Forms.Padding(4);
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(141, 16);
            this.txtPC.TabIndex = 9;
            this.txtPC.Text = "0";
            this.txtPC.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPC.WaterMark = null;
            // 
            // txtSC
            // 
            this.txtSC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSC.Data = 0;
            this.txtSC.EnterColor = System.Drawing.Color.LightPink;
            this.txtSC.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtSC.Format = null;
            this.txtSC.isAllowNegative = false;
            this.txtSC.isAllowSpecialChar = false;
            this.txtSC.isNumberOnly = false;
            this.txtSC.isNumeric = true;
            this.txtSC.LeaveColor = System.Drawing.Color.White;
            this.txtSC.Location = new System.Drawing.Point(323, 121);
            this.txtSC.Margin = new System.Windows.Forms.Padding(4);
            this.txtSC.Name = "txtSC";
            this.txtSC.Size = new System.Drawing.Size(121, 16);
            this.txtSC.TabIndex = 8;
            this.txtSC.Text = "0";
            this.txtSC.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSC.WaterMark = null;
            // 
            // lblTax2
            // 
            this.lblTax2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTax2.Location = new System.Drawing.Point(242, 115);
            this.lblTax2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTax2.Name = "lblTax2";
            this.lblTax2.Size = new System.Drawing.Size(68, 16);
            this.lblTax2.TabIndex = 7;
            this.lblTax2.Text = "Tax - 2 :";
            this.lblTax2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPX
            // 
            this.txtPX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPX.Data = 0;
            this.txtPX.EnterColor = System.Drawing.Color.LightPink;
            this.txtPX.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPX.Format = null;
            this.txtPX.isAllowNegative = false;
            this.txtPX.isAllowSpecialChar = false;
            this.txtPX.isNumberOnly = false;
            this.txtPX.isNumeric = true;
            this.txtPX.LeaveColor = System.Drawing.Color.White;
            this.txtPX.Location = new System.Drawing.Point(490, 169);
            this.txtPX.Margin = new System.Windows.Forms.Padding(4);
            this.txtPX.Name = "txtPX";
            this.txtPX.Size = new System.Drawing.Size(141, 16);
            this.txtPX.TabIndex = 15;
            this.txtPX.Text = "0";
            this.txtPX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPX.WaterMark = null;
            this.txtPX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPX_KeyDown);
            // 
            // txtSX
            // 
            this.txtSX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSX.Data = 0;
            this.txtSX.EnterColor = System.Drawing.Color.LightPink;
            this.txtSX.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtSX.Format = null;
            this.txtSX.isAllowNegative = false;
            this.txtSX.isAllowSpecialChar = false;
            this.txtSX.isNumberOnly = false;
            this.txtSX.isNumeric = true;
            this.txtSX.LeaveColor = System.Drawing.Color.White;
            this.txtSX.Location = new System.Drawing.Point(323, 169);
            this.txtSX.Margin = new System.Windows.Forms.Padding(4);
            this.txtSX.Name = "txtSX";
            this.txtSX.Size = new System.Drawing.Size(121, 16);
            this.txtSX.TabIndex = 14;
            this.txtSX.Text = "0";
            this.txtSX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSX.WaterMark = null;
            // 
            // lblTax4
            // 
            this.lblTax4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTax4.Location = new System.Drawing.Point(242, 167);
            this.lblTax4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTax4.Name = "lblTax4";
            this.lblTax4.Size = new System.Drawing.Size(68, 16);
            this.lblTax4.TabIndex = 13;
            this.lblTax4.Text = "Tax - 4 :";
            this.lblTax4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Purchase (%)";
            // 
            // txtPGST
            // 
            this.txtPGST.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPGST.Data = 0;
            this.txtPGST.EnterColor = System.Drawing.Color.LightPink;
            this.txtPGST.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtPGST.Format = null;
            this.txtPGST.isAllowNegative = false;
            this.txtPGST.isAllowSpecialChar = false;
            this.txtPGST.isNumberOnly = false;
            this.txtPGST.isNumeric = true;
            this.txtPGST.LeaveColor = System.Drawing.Color.White;
            this.txtPGST.Location = new System.Drawing.Point(490, 97);
            this.txtPGST.Margin = new System.Windows.Forms.Padding(4);
            this.txtPGST.Name = "txtPGST";
            this.txtPGST.Size = new System.Drawing.Size(141, 16);
            this.txtPGST.TabIndex = 6;
            this.txtPGST.Text = "0";
            this.txtPGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPGST.WaterMark = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sales (%)";
            // 
            // txtSGST
            // 
            this.txtSGST.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSGST.Data = 0;
            this.txtSGST.EnterColor = System.Drawing.Color.LightPink;
            this.txtSGST.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.txtSGST.Format = null;
            this.txtSGST.isAllowNegative = false;
            this.txtSGST.isAllowSpecialChar = false;
            this.txtSGST.isNumberOnly = false;
            this.txtSGST.isNumeric = true;
            this.txtSGST.LeaveColor = System.Drawing.Color.White;
            this.txtSGST.Location = new System.Drawing.Point(323, 93);
            this.txtSGST.Margin = new System.Windows.Forms.Padding(4);
            this.txtSGST.Name = "txtSGST";
            this.txtSGST.Size = new System.Drawing.Size(121, 16);
            this.txtSGST.TabIndex = 5;
            this.txtSGST.Text = "0";
            this.txtSGST.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSGST.WaterMark = null;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.livTaxCategory);
            this.groupBox1.Location = new System.Drawing.Point(165, 193);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(476, 338);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Tax Categories";
            // 
            // livTaxCategory
            // 
            this.livTaxCategory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.SrchAccountGroup,
            this.columnHeader1,
            this.columnHeader2});
            this.livTaxCategory.FullRowSelect = true;
            this.livTaxCategory.GridLines = true;
            this.livTaxCategory.HideSelection = false;
            this.livTaxCategory.Location = new System.Drawing.Point(11, 23);
            this.livTaxCategory.Margin = new System.Windows.Forms.Padding(4);
            this.livTaxCategory.Name = "livTaxCategory";
            this.livTaxCategory.Size = new System.Drawing.Size(453, 304);
            this.livTaxCategory.TabIndex = 0;
            this.livTaxCategory.UseCompatibleStateImageBehavior = false;
            this.livTaxCategory.View = System.Windows.Forms.View.Details;
            this.livTaxCategory.SelectedIndexChanged += new System.EventHandler(this.livTaxCategory_SelectedIndexChanged);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "SL No";
            // 
            // SrchAccountGroup
            // 
            this.SrchAccountGroup.Text = "Tax Name";
            this.SrchAccountGroup.Width = 202;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sales Tax";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Purchase Tax";
            this.columnHeader2.Width = 120;
            // 
            // TaxCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(654, 587);
            this.Controls.Add(this.txtPCess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSCess);
            this.Controls.Add(this.txtPGST);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTax3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSGST);
            this.Controls.Add(this.txtPC);
            this.Controls.Add(this.lblTax4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSC);
            this.Controls.Add(this.txtSX);
            this.Controls.Add(this.lblTax1);
            this.Controls.Add(this.lblTax2);
            this.Controls.Add(this.txtPX);
            this.Controls.Add(this.txtName);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "TaxCategory";
            this.Load += new System.EventHandler(this.TaxCategory_Load);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtPX, 0);
            this.Controls.SetChildIndex(this.lblTax2, 0);
            this.Controls.SetChildIndex(this.lblTax1, 0);
            this.Controls.SetChildIndex(this.txtSX, 0);
            this.Controls.SetChildIndex(this.txtSC, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.lblTax4, 0);
            this.Controls.SetChildIndex(this.txtPC, 0);
            this.Controls.SetChildIndex(this.txtSGST, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lblTax3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtPGST, 0);
            this.Controls.SetChildIndex(this.txtSCess, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtPCess, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
