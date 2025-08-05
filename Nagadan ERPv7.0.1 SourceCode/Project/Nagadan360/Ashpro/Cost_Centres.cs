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
	public class Cost_Centres : MasterForm
	{
		private int? iCostCenterId;

		private DBContext db;

		private IContainer components = null;

		private AshTextBoxNormal txtDescription;

		private GroupBox groupBox1;

		private ListView livDetails;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Label label1;

		private AshTextBoxNormal txtName;

		private Label label4;

		public UserPrevilegeDtl plgDtls { get; set; }

		public Cost_Centres()
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
			base.ActiveControl = txtName;
		}

		private void LoadListViewMethod()
		{
			try
			{
				livDetails.Items.Clear();
				foreach (CostCenter item in db.CostCenters())
				{
					ListViewItem listViewItem = new ListViewItem((livDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.CostCenterName);
					listViewItem.SubItems.Add(item.Description);
					livDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Cost_Centres_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			LoadListViewMethod();
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			btnDelete.Enabled = false;
			btnDelete.ForeColor = Color.Black;
			iCostCenterId = null;
			LoadListViewMethod();
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
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("CostCenter", "CostCenterID", iCostCenterId.ToInt32()))
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
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				CostCenter data = new CostCenter
				{
					CostCenterID = iCostCenterId,
					CostCenterName = txtName.Text,
					Description = txtDescription.Text
				};
				if (!iCostCenterId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(data, "CostCenter"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(data, "CostCenter", "CostCenterID", iCostCenterId.ToInt32()))
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

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livDetails.SelectedItems.Count != 0)
			{
				CostCenter costCenter = (from x in db.CostCenters()
					where x.CostCenterName == livDetails.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtName.Text = costCenter.CostCenterName;
				txtDescription.Text = costCenter.Description;
				iCostCenterId = costCenter.CostCenterID.ToInt32();
			}
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
		}

		private void txtDescription_KeyDown(object sender, KeyEventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cost_Centres));
            this.txtDescription = new AshUsercontrol.AshTextBoxNormal();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.livDetails = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
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
            resources.ApplyResources(this.btnEnd, "btnEnd");
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Data = 0;
            this.txtDescription.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Format = null;
            this.txtDescription.isAllowNegative = false;
            this.txtDescription.isAllowSpecialChar = false;
            this.txtDescription.isNumberOnly = false;
            this.txtDescription.isNumeric = false;
            this.txtDescription.LeaveColor = System.Drawing.Color.White;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.livDetails);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // livDetails
            // 
            this.livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2});
            this.livDetails.FullRowSelect = true;
            this.livDetails.GridLines = true;
            this.livDetails.HideSelection = false;
            resources.ApplyResources(this.livDetails, "livDetails");
            this.livDetails.Name = "livDetails";
            this.livDetails.UseCompatibleStateImageBehavior = false;
            this.livDetails.View = System.Windows.Forms.View.Details;
            this.livDetails.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            resources.ApplyResources(this.txtName, "txtName");
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // Cost_Centres
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.Name = "Cost_Centres";
            this.Load += new System.EventHandler(this.Cost_Centres_Load);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
