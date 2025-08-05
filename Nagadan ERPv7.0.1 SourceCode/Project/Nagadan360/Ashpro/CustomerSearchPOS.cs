using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class CustomerSearchPOS : BaseFormTwo
	{
		private DBContext db = new DBContext();

		private AccountGroupEntity accountGroup;

		private List<AccountEntity> accounts;

		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private AshTextBoxNormal txtTaxID;

		private Label label2;

		private Button btnCancel;

		private Button btnSave;

		private Button btnOK;

		public AshTextBoxNormal txtName;

		private Button button1;

		public AshTextBoxNormal txtNameInLocalLanguage;

		private Label lblNameInLocalLanguage;

		private CheckBox cbTransliterate;

		private DataGridView dgCustomers;

		private DataGridViewTextBoxColumn col_Id;

		private DataGridViewTextBoxColumn col_Name;

		public bool isNewCustomer { get; set; } = false;


		public CustomerSearchPOS()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void CustomerSearchPOS_Load(object sender, EventArgs e)
		{
			if (Settings.Default.Language == "ar")
			{
				txtNameInLocalLanguage.Visible = true;
				lblNameInLocalLanguage.Visible = true;
				cbTransliterate.Visible = true;
				cbTransliterate.Checked = true;
			}
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				AccountEntity accountEntity = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountName == txtName.Text);
				if (accountEntity == null && accounts.Count > 0)
				{
					accountEntity = accounts.FirstOrDefault();
				}
				if (accountEntity != null)
				{
					txtName.Tag = accountEntity.AccountId.toInt32();
					txtTaxID.Text = accountEntity.TinNo;
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text != string.Empty && !db.Accounts().Any((AccountEntity x) => x.AccountName == txtName.Text) && AshproCommon.InsertToDatabaseObj(new AccountEntity
			{
				AccountName = txtName.Text,
				AGroupId = accountGroup.AGroupId.ToInt32(),
				TinNo = txtTaxID.Text
			}, "Account"))
			{
				txtName.Tag = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountName == txtName.Text && x.AGroupId == accountGroup.AGroupId).AccountId.toInt32();
				btnOK_Click(null, null);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			isNewCustomer = true;
			btnOK_Click(null, null);
		}

		private void txtTaxID_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (txtName.Tag != null)
				{
					btnOK.PerformClick();
				}
				else
				{
					btnSave.PerformClick();
				}
			}
		}

		private void txtName_Validated(object sender, EventArgs e)
		{
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			try
			{
				accountGroup = db.AccountGroup().FirstOrDefault((AccountGroupEntity x) => x.GroupName == "CUSTOMER/SUNDRY DEBTORS");
				accounts = new List<AccountEntity>();
				accounts = (from x in db.Accounts(new { accountGroup.AGroupId }).ToList()
					where x.AccountName.StartsWith(txtName.Text)
					select x).ToList();
				if (accounts.Count > 0)
				{
					if (!dgCustomers.Visible)
					{
						dgCustomers.Visible = true;
					}
					dgCustomers.AutoGenerateColumns = false;
					dgCustomers.DataSource = null;
					dgCustomers.DataSource = accounts;
				}
				else if (dgCustomers.Visible)
				{
					dgCustomers.Visible = false;
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgCustomers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && accounts.Count > 0)
			{
				AccountEntity accountEntity = (AccountEntity)dgCustomers.CurrentRow.DataBoundItem;
				txtName.Text = accountEntity.AccountName;
				txtName.Tag = accountEntity.AccountId;
				txtTaxID.Text = accountEntity.TinNo;
				if (dgCustomers.Visible)
				{
					dgCustomers.Visible = false;
				}
			}
		}

		private void dgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (accounts.Count > 0)
			{
				AccountEntity accountEntity = (AccountEntity)dgCustomers.CurrentRow.DataBoundItem;
				txtName.Text = accountEntity.AccountName;
				txtName.Tag = accountEntity.AccountId;
				if (dgCustomers.Visible)
				{
					dgCustomers.Visible = false;
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerSearchPOS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTaxID = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNameInLocalLanguage = new AshUsercontrol.AshTextBoxNormal();
            this.lblNameInLocalLanguage = new System.Windows.Forms.Label();
            this.cbTransliterate = new System.Windows.Forms.CheckBox();
            this.dgCustomers = new System.Windows.Forms.DataGridView();
            this.col_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Validated += new System.EventHandler(this.txtName_Validated);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtTaxID
            // 
            resources.ApplyResources(this.txtTaxID, "txtTaxID");
            this.txtTaxID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTaxID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaxID.Data = 0;
            this.txtTaxID.EnterColor = System.Drawing.Color.LightPink;
            this.txtTaxID.Format = null;
            this.txtTaxID.isAllowNegative = false;
            this.txtTaxID.isAllowSpecialChar = false;
            this.txtTaxID.isNumberOnly = false;
            this.txtTaxID.isNumeric = false;
            this.txtTaxID.LeaveColor = System.Drawing.Color.White;
            this.txtTaxID.Name = "txtTaxID";
            this.txtTaxID.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTaxID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaxID_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabStop = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabStop = false;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.btnOK.TabStop = false;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Name = "button1";
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNameInLocalLanguage
            // 
            resources.ApplyResources(this.txtNameInLocalLanguage, "txtNameInLocalLanguage");
            this.txtNameInLocalLanguage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNameInLocalLanguage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameInLocalLanguage.Data = 0;
            this.txtNameInLocalLanguage.EnterColor = System.Drawing.Color.LightPink;
            this.txtNameInLocalLanguage.Format = null;
            this.txtNameInLocalLanguage.isAllowNegative = false;
            this.txtNameInLocalLanguage.isAllowSpecialChar = false;
            this.txtNameInLocalLanguage.isNumberOnly = false;
            this.txtNameInLocalLanguage.isNumeric = false;
            this.txtNameInLocalLanguage.LeaveColor = System.Drawing.Color.White;
            this.txtNameInLocalLanguage.Name = "txtNameInLocalLanguage";
            this.txtNameInLocalLanguage.TabStop = false;
            this.txtNameInLocalLanguage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNameInLocalLanguage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // lblNameInLocalLanguage
            // 
            resources.ApplyResources(this.lblNameInLocalLanguage, "lblNameInLocalLanguage");
            this.lblNameInLocalLanguage.Name = "lblNameInLocalLanguage";
            // 
            // cbTransliterate
            // 
            resources.ApplyResources(this.cbTransliterate, "cbTransliterate");
            this.cbTransliterate.Name = "cbTransliterate";
            this.cbTransliterate.UseVisualStyleBackColor = true;
            // 
            // dgCustomers
            // 
            resources.ApplyResources(this.dgCustomers, "dgCustomers");
            this.dgCustomers.AllowUserToAddRows = false;
            this.dgCustomers.AllowUserToDeleteRows = false;
            this.dgCustomers.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCustomers.ColumnHeadersVisible = false;
            this.dgCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Id,
            this.col_Name});
            this.dgCustomers.EnableHeadersVisualStyles = false;
            this.dgCustomers.Name = "dgCustomers";
            this.dgCustomers.ReadOnly = true;
            this.dgCustomers.RowHeadersVisible = false;
            this.dgCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCustomers_CellContentClick);
            this.dgCustomers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgCustomers_KeyDown);
            // 
            // col_Id
            // 
            this.col_Id.DataPropertyName = "AccountId";
            resources.ApplyResources(this.col_Id, "col_Id");
            this.col_Id.Name = "col_Id";
            this.col_Id.ReadOnly = true;
            // 
            // col_Name
            // 
            this.col_Name.DataPropertyName = "AccountName";
            resources.ApplyResources(this.col_Name, "col_Name");
            this.col_Name.Name = "col_Name";
            this.col_Name.ReadOnly = true;
            // 
            // CustomerSearchPOS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.dgCustomers);
            this.Controls.Add(this.cbTransliterate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNameInLocalLanguage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTaxID);
            this.Controls.Add(this.txtNameInLocalLanguage);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CustomerSearchPOS";
            this.Load += new System.EventHandler(this.CustomerSearchPOS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
