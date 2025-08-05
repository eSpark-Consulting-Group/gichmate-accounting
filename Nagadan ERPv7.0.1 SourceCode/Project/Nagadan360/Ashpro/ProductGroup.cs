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
	public class ProductGroup : MasterForm
	{
		private int? iPGroupId;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private AshTextBoxNormal txtRemarks;

		private Label label7;

		private AshTextBoxNormal txtSalesDiscount;

		private AshTextBoxNormal txtPurchaseDiscount;

		private Label label4;

		private AshComboBox cmbUnder;

		private AshTextBoxNormal txtSalesIncentive;

		private Label label2;

		private Label label1;

		private AshTextBoxNormal txtName;

		private Label label3;

		private Label label5;

		private Label label6;

		private AshTextBoxNormal txtListOrder;

		private CheckBox cbShowInGrid;

		public string ProductGroupName { get; set; }

		public bool isOpenFromAnotherForm { get; set; }

		public UserPrevilegeDtl plgDtls { get; set; }

		public ProductGroup()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
			btnSearch.Visible = true;
			AniHelper.FillProGroup(cmbUnder);
		}

		private ProductGroupEntity EntityLoadMethod()
		{
			return new ProductGroupEntity
			{
				PGroupId = iPGroupId,
				PGroupName = txtName.Text,
				Under = cmbUnder.Text,
				SalesInsentive = txtSalesIncentive.Value,
				SalesDis = txtSalesDiscount.Value,
				PurchDis = txtPurchaseDiscount.Value,
				Remarks = txtRemarks.Text,
				ListOrder = txtListOrder.Text.ToInt32(),
				isShowInGrid = cbShowInGrid.Checked
			};
		}

		private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			btnDelete.Enabled = false;
			iPGroupId = null;
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
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("ProductGroup", "PGroupId", iPGroupId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text == string.Empty)
			{
				Messages.WarningMessage();
				return;
			}
			if (!iPGroupId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					return;
				}
				_ = txtName.Text;
				if (db.ProductGroups().Any((ProductGroupEntity x) => x.PGroupName == txtName.Text))
				{
					Messages.ReferenceExistsMessage();
					return;
				}
				if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod(), "ProductGroup"))
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
				if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "ProductGroup", "PGroupId", iPGroupId.ToInt32()))
				{
					Messages.UpdateMessage();
				}
			}
			if (isOpenFromAnotherForm)
			{
				ProductGroupName = txtName.Text;
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				btnClear_Click(null, null);
			}
		}

		protected override void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				MasterSerchForm masterSerchForm = new MasterSerchForm("ID", "Product Group Name", "Under");
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable("select PGroupId as id,PGroupName as name,Under as description From ProductGroup");
				masterSerchForm.Masters = db.Masters(dataTable);
				masterSerchForm.proGroup = this;
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

		public void DataLoadFromSearch(MasterSerchForm frm)
		{
			ProductGroupEntity productGroupEntity = db.ProductGroups(new
			{
				PGroupId = frm.id
			}).FirstOrDefault();
			txtName.Text = productGroupEntity.PGroupName;
			iPGroupId = productGroupEntity.PGroupId;
			cmbUnder.Text = productGroupEntity.Under;
			txtSalesIncentive.Value = productGroupEntity.SalesInsentive;
			txtPurchaseDiscount.Value = productGroupEntity.PurchDis;
			txtSalesDiscount.Value = productGroupEntity.SalesDis;
			txtRemarks.Text = productGroupEntity.Remarks;
			cbShowInGrid.Checked = productGroupEntity.isShowInGrid;
			txtListOrder.Data = productGroupEntity.ListOrder.toInt32();
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtName.Focus();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductGroup));
            this.txtRemarks = new AshUsercontrol.AshTextBoxNormal();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSalesDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.txtPurchaseDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbUnder = new AshUsercontrol.AshComboBox();
            this.txtSalesIncentive = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtListOrder = new AshUsercontrol.AshTextBoxNormal();
            this.cbShowInGrid = new System.Windows.Forms.CheckBox();
            this.pnlButton.SuspendLayout();
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
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtSalesDiscount
            // 
            resources.ApplyResources(this.txtSalesDiscount, "txtSalesDiscount");
            this.txtSalesDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSalesDiscount.Data = 0;
            this.txtSalesDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtSalesDiscount.Format = null;
            this.txtSalesDiscount.isAllowNegative = false;
            this.txtSalesDiscount.isAllowSpecialChar = false;
            this.txtSalesDiscount.isNumberOnly = false;
            this.txtSalesDiscount.isNumeric = true;
            this.txtSalesDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtSalesDiscount.Name = "txtSalesDiscount";
            this.txtSalesDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtPurchaseDiscount
            // 
            resources.ApplyResources(this.txtPurchaseDiscount, "txtPurchaseDiscount");
            this.txtPurchaseDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPurchaseDiscount.Data = 0;
            this.txtPurchaseDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtPurchaseDiscount.Format = null;
            this.txtPurchaseDiscount.isAllowNegative = false;
            this.txtPurchaseDiscount.isAllowSpecialChar = false;
            this.txtPurchaseDiscount.isNumberOnly = false;
            this.txtPurchaseDiscount.isNumeric = true;
            this.txtPurchaseDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtPurchaseDiscount.Name = "txtPurchaseDiscount";
            this.txtPurchaseDiscount.Value = new decimal(new int[] {
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
            // cmbUnder
            // 
            resources.ApplyResources(this.cmbUnder, "cmbUnder");
            this.cmbUnder.EnterColor = System.Drawing.Color.LightPink;
            this.cmbUnder.FormattingEnabled = true;
            this.cmbUnder.LeaveColor = System.Drawing.Color.White;
            this.cmbUnder.Name = "cmbUnder";
            // 
            // txtSalesIncentive
            // 
            resources.ApplyResources(this.txtSalesIncentive, "txtSalesIncentive");
            this.txtSalesIncentive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSalesIncentive.Data = 0;
            this.txtSalesIncentive.EnterColor = System.Drawing.Color.LightPink;
            this.txtSalesIncentive.Format = null;
            this.txtSalesIncentive.isAllowNegative = false;
            this.txtSalesIncentive.isAllowSpecialChar = false;
            this.txtSalesIncentive.isNumberOnly = false;
            this.txtSalesIncentive.isNumeric = true;
            this.txtSalesIncentive.LeaveColor = System.Drawing.Color.White;
            this.txtSalesIncentive.Name = "txtSalesIncentive";
            this.txtSalesIncentive.Value = new decimal(new int[] {
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtListOrder
            // 
            resources.ApplyResources(this.txtListOrder, "txtListOrder");
            this.txtListOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtListOrder.Data = 0;
            this.txtListOrder.EnterColor = System.Drawing.Color.LightPink;
            this.txtListOrder.Format = null;
            this.txtListOrder.isAllowNegative = false;
            this.txtListOrder.isAllowSpecialChar = false;
            this.txtListOrder.isNumberOnly = true;
            this.txtListOrder.isNumeric = false;
            this.txtListOrder.LeaveColor = System.Drawing.Color.White;
            this.txtListOrder.Name = "txtListOrder";
            this.txtListOrder.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cbShowInGrid
            // 
            resources.ApplyResources(this.cbShowInGrid, "cbShowInGrid");
            this.cbShowInGrid.Checked = true;
            this.cbShowInGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInGrid.Name = "cbShowInGrid";
            this.cbShowInGrid.UseVisualStyleBackColor = true;
            // 
            // ProductGroup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbShowInGrid);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtListOrder);
            this.Controls.Add(this.txtSalesDiscount);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPurchaseDiscount);
            this.Controls.Add(this.txtSalesIncentive);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbUnder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.MaximizeBox = false;
            this.Name = "ProductGroup";
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmbUnder, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtSalesIncentive, 0);
            this.Controls.SetChildIndex(this.txtPurchaseDiscount, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtSalesDiscount, 0);
            this.Controls.SetChildIndex(this.txtListOrder, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtRemarks, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.cbShowInGrid, 0);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
