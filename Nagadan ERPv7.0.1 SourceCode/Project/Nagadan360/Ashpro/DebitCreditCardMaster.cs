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
	public class DebitCreditCardMaster : MasterForm
	{
		private int? iCardId;

		private DBContext db;

		private IContainer components = null;

		private GroupBox groupBox1;

		private ListView livDetails;

		private ColumnHeader columnHeader3;

		private ColumnHeader SrchAccountGroup;

		private ColumnHeader Under;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Label label3;

		private AshTextBoxNormal txtRemarks;

		private Label label1;

		private AshTextBoxNormal txtCardName;

		private Label label2;

		private AshTextBoxNormal txtBankComm;

		private AshTextBoxNormal txtCardComm;

		private Label label4;

		public UserPrevilegeDtl plgDtls { get; set; }

		public DebitCreditCardMaster()
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
			base.ActiveControl = txtCardName;
		}

		private DebitCreditCardMasterEntity EntityLoadMethod()
		{
			return new DebitCreditCardMasterEntity
			{
				CardId = iCardId,
				CardName = txtCardName.Text,
				BankComm = txtBankComm.Value,
				CardComm = txtCardComm.Value,
				Remarks = txtRemarks.Text
			};
		}

		private void LoadListViewMethod()
		{
			int num = 0;
			try
			{
				livDetails.Items.Clear();
				foreach (DebitCreditCardMasterEntity item in db.DebitCreditCardMasters())
				{
					ListViewItem listViewItem = new ListViewItem((livDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.CardName);
					listViewItem.SubItems.Add(item.BankComm.ToString(CommonSettings.strCurencyFormat));
					listViewItem.SubItems.Add(item.CardComm.ToString(CommonSettings.strCurencyFormat));
					listViewItem.SubItems.Add(item.Remarks);
					livDetails.Items.Add(listViewItem);
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
			iCardId = null;
			LoadListViewMethod();
			txtCardName.Focus();
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("DebitCreditCardMaster", "CardId", iCardId.ToInt32()))
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
				if (txtCardName.Text == "")
				{
					Messages.WarningMessage();
				}
				else if (!iCardId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod(), "DebitCreditCardMaster"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "DebitCreditCardMaster", "CardId", iCardId.ToInt32()))
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

		private void DebitCreditCardMaster_Load(object sender, EventArgs e)
		{
			try
			{
				db = new DBContext();
				LoadListViewMethod();
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livDetails.SelectedItems.Count != 0)
			{
				DebitCreditCardMasterEntity debitCreditCardMasterEntity = (from x in db.DebitCreditCardMasters()
					where x.CardName == livDetails.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtCardName.Text = debitCreditCardMasterEntity.CardName;
				txtBankComm.Value = debitCreditCardMasterEntity.BankComm;
				txtCardComm.Value = debitCreditCardMasterEntity.CardComm;
				txtRemarks.Text = debitCreditCardMasterEntity.Remarks;
				iCardId = debitCreditCardMasterEntity.CardId.toInt32();
			}
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebitCreditCardMaster));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.livDetails = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SrchAccountGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Under = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardName = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBankComm = new AshUsercontrol.AshTextBoxNormal();
            this.txtCardComm = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnClear.FlatAppearance.BorderSize = 0;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.livDetails);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // livDetails
            // 
            resources.ApplyResources(this.livDetails, "livDetails");
            this.livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.SrchAccountGroup,
            this.Under,
            this.columnHeader1,
            this.columnHeader2});
            this.livDetails.FullRowSelect = true;
            this.livDetails.GridLines = true;
            this.livDetails.HideSelection = false;
            this.livDetails.Name = "livDetails";
            this.livDetails.UseCompatibleStateImageBehavior = false;
            this.livDetails.View = System.Windows.Forms.View.Details;
            this.livDetails.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // SrchAccountGroup
            // 
            resources.ApplyResources(this.SrchAccountGroup, "SrchAccountGroup");
            // 
            // Under
            // 
            resources.ApplyResources(this.Under, "Under");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCardName
            // 
            resources.ApplyResources(this.txtCardName, "txtCardName");
            this.txtCardName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCardName.Data = 0;
            this.txtCardName.EnterColor = System.Drawing.Color.LightPink;
            this.txtCardName.Format = null;
            this.txtCardName.isAllowNegative = false;
            this.txtCardName.isAllowSpecialChar = false;
            this.txtCardName.isNumberOnly = false;
            this.txtCardName.isNumeric = false;
            this.txtCardName.LeaveColor = System.Drawing.Color.White;
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBankComm
            // 
            resources.ApplyResources(this.txtBankComm, "txtBankComm");
            this.txtBankComm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBankComm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBankComm.Data = 0;
            this.txtBankComm.EnterColor = System.Drawing.Color.LightPink;
            this.txtBankComm.Format = null;
            this.txtBankComm.isAllowNegative = false;
            this.txtBankComm.isAllowSpecialChar = false;
            this.txtBankComm.isNumberOnly = false;
            this.txtBankComm.isNumeric = true;
            this.txtBankComm.LeaveColor = System.Drawing.Color.White;
            this.txtBankComm.Name = "txtBankComm";
            this.txtBankComm.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // txtCardComm
            // 
            resources.ApplyResources(this.txtCardComm, "txtCardComm");
            this.txtCardComm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardComm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCardComm.Data = 0;
            this.txtCardComm.EnterColor = System.Drawing.Color.LightPink;
            this.txtCardComm.Format = null;
            this.txtCardComm.isAllowNegative = false;
            this.txtCardComm.isAllowSpecialChar = false;
            this.txtCardComm.isNumberOnly = false;
            this.txtCardComm.isNumeric = true;
            this.txtCardComm.LeaveColor = System.Drawing.Color.White;
            this.txtCardComm.Name = "txtCardComm";
            this.txtCardComm.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // DebitCreditCardMaster
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCardComm);
            this.Controls.Add(this.txtBankComm);
            this.Controls.Add(this.txtCardName);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "DebitCreditCardMaster";
            this.Load += new System.EventHandler(this.DebitCreditCardMaster_Load);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtCardName, 0);
            this.Controls.SetChildIndex(this.txtBankComm, 0);
            this.Controls.SetChildIndex(this.txtCardComm, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
