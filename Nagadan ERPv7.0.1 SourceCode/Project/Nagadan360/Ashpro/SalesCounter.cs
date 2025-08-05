using System;
using System.ComponentModel;
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
	public class SalesCounter : MasterForm
	{
		private int? iSalesCounterId;

		private DBContext db;

		private IContainer components = null;

		private GroupBox groupBox1;

		private ListView livDetails;

		private ColumnHeader columnHeader3;

		private ColumnHeader SrchAccountGroup;

		private ColumnHeader Under;

		private AshComboBox cmbCashAcc;

		private Label label3;

		private AshTextBoxNormal txtCounter;

		private Label label2;

		private Label label1;

		private AshTextBoxNormal txtRemarks;

		private ColumnHeader columnHeader1;

		private Button btnNewCashAccount;

		public UserPrevilegeDtl plgDtls { get; set; }

		public bool isFromOtherForm { get; set; }

		public SalesCounter()
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

		private void LoadListViewMethod()
		{
			try
			{
				livDetails.Items.Clear();
				foreach (SalesCounterEntity cntr in db.SalesCounter().ToList())
				{
					ListViewItem listViewItem = new ListViewItem((livDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(cntr.SalesCounterName);
					listViewItem.SubItems.Add(db.Accounts().Find((AccountEntity x) => x.AccountId == cntr.CashAccountId).AccountName);
					listViewItem.SubItems.Add(cntr.Remarks);
					livDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SalesCounter_Load(object sender, EventArgs e)
		{
			txtRemarks.Text = Utility.uTerminal;
			if (Settings.Default.CounterName != string.Empty)
			{
				txtCounter.Text = Settings.Default.CounterName;
			}
			db = new DBContext();
			LoadListViewMethod();
			AniHelper.FillCashAccount(cmbCashAcc, isLoadFull: true);
			if (isFromOtherForm && Utility.uCounterCashAccountId != 0)
			{
				cmbCashAcc.SelectedValue = Utility.uCounterCashAccountId;
			}
			base.ActiveControl = txtCounter;
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livDetails.SelectedItems.Count != 0)
			{
				SalesCounterEntity salesCounterEntity = db.SalesCounter().Find((SalesCounterEntity x) => x.SalesCounterName == livDetails.SelectedItems[0].SubItems[1].Text);
				txtCounter.Text = salesCounterEntity.SalesCounterName;
				cmbCashAcc.SelectedValue = salesCounterEntity.CashAccountId.GetValueOrDefault();
				txtRemarks.Text = salesCounterEntity.Remarks;
				iSalesCounterId = salesCounterEntity.SalesCounterId.toInt32();
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
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
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("SalesCounter", "SalesCounterId", iSalesCounterId.ToInt32()))
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
			try
			{
				if (txtCounter.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				SalesCounterEntity data = new SalesCounterEntity
				{
					SalesCounterId = iSalesCounterId,
					SalesCounterName = txtCounter.Text,
					CashAccountId = cmbCashAcc.SelectedValue.ToInt32(),
					Remarks = txtRemarks.Text,
					FK_BranchId = Settings.Default.iBranchId
				};
				if (!iSalesCounterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.InsertToDatabaseObj(data, "SalesCounter"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.UpdateToDatabaseObj(data, "SalesCounter", "SalesCounterId", iSalesCounterId.ToInt32()))
					{
						Messages.UpdateMessage();
						btnClear_Click(null, null);
					}
				}
				if (isFromOtherForm)
				{
					Utility.uCounterCashAccountId = db.SalesCounter().FirstOrDefault((SalesCounterEntity x) => x.Remarks == Utility.uTerminal).CashAccountId.ToInt32();
					base.DialogResult = DialogResult.OK;
					Close();
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
			btnDelete.Enabled = false;
			btnDelete.ForeColor = Color.Black;
			iSalesCounterId = null;
			LoadListViewMethod();
			txtCounter.Focus();
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void btnNewCashAccount_Click(object sender, EventArgs e)
		{
			Account account = new Account();
			account.isOpenFromAnotherForm = true;
			account.accountGroupName = "CASH IN HAND";
			account.sFormName = "Chart Of Account";
			if (account.ShowDialog() == DialogResult.OK)
			{
				AniHelper.FillCashAccount(cmbCashAcc, isLoadFull: true);
				cmbCashAcc.SelectedValue = account.Id;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SalesCounter));
			groupBox1 = new System.Windows.Forms.GroupBox();
			livDetails = new System.Windows.Forms.ListView();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();
			SrchAccountGroup = new System.Windows.Forms.ColumnHeader();
			Under = new System.Windows.Forms.ColumnHeader();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			cmbCashAcc = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			txtCounter = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtRemarks = new AshUsercontrol.AshTextBoxNormal();
			btnNewCashAccount = new System.Windows.Forms.Button();
			pnlButton.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(pnlButton, "pnlButton");
			resources.ApplyResources(pnlMaster, "pnlMaster");
			resources.ApplyResources(lblMasterTitle, "lblMasterTitle");
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnSearch, "btnSearch");
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.BackColor = System.Drawing.Color.White;
			groupBox1.Controls.Add(livDetails);
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(livDetails, "livDetails");
			livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[4] { columnHeader3, SrchAccountGroup, Under, columnHeader1 });
			livDetails.FullRowSelect = true;
			livDetails.GridLines = true;
			livDetails.HideSelection = false;
			livDetails.Name = "livDetails";
			livDetails.UseCompatibleStateImageBehavior = false;
			livDetails.View = System.Windows.Forms.View.Details;
			livDetails.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
			resources.ApplyResources(columnHeader3, "columnHeader3");
			resources.ApplyResources(SrchAccountGroup, "SrchAccountGroup");
			resources.ApplyResources(Under, "Under");
			resources.ApplyResources(columnHeader1, "columnHeader1");
			resources.ApplyResources(cmbCashAcc, "cmbCashAcc");
			cmbCashAcc.EnterColor = System.Drawing.Color.LightPink;
			cmbCashAcc.FormattingEnabled = true;
			cmbCashAcc.LeaveColor = System.Drawing.Color.White;
			cmbCashAcc.Name = "cmbCashAcc";
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			resources.ApplyResources(txtCounter, "txtCounter");
			txtCounter.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtCounter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtCounter.Data = 0;
			txtCounter.EnterColor = System.Drawing.Color.LightPink;
			txtCounter.Format = null;
			txtCounter.isAllowNegative = false;
			txtCounter.isAllowSpecialChar = false;
			txtCounter.isNumberOnly = false;
			txtCounter.isNumeric = false;
			txtCounter.LeaveColor = System.Drawing.Color.White;
			txtCounter.Name = "txtCounter";
			txtCounter.Value = new decimal(new int[4]);
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			resources.ApplyResources(txtRemarks, "txtRemarks");
			txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
			resources.ApplyResources(btnNewCashAccount, "btnNewCashAccount");
			btnNewCashAccount.BackColor = System.Drawing.Color.DodgerBlue;
			btnNewCashAccount.ForeColor = System.Drawing.Color.White;
			btnNewCashAccount.Name = "btnNewCashAccount";
			btnNewCashAccount.TabStop = false;
			btnNewCashAccount.UseVisualStyleBackColor = false;
			btnNewCashAccount.Click += new System.EventHandler(btnNewCashAccount_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(btnNewCashAccount);
			base.Controls.Add(groupBox1);
			base.Controls.Add(cmbCashAcc);
			base.Controls.Add(label3);
			base.Controls.Add(txtRemarks);
			base.Controls.Add(label1);
			base.Controls.Add(txtCounter);
			base.Controls.Add(label2);
			base.MaximizeBox = false;
			base.Name = "SalesCounter";
			base.Load += new System.EventHandler(SalesCounter_Load);
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtCounter, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtRemarks, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbCashAcc, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(btnNewCashAccount, 0);
			pnlButton.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
