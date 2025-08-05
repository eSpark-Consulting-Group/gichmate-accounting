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
	public class PriceCategory : MasterForm
	{
		private int? iPCategoryId;

		private DBContext db;

		private IContainer components = null;

		private AshTextBoxNormal txtDiscount;

		private Label label5;

		private ColumnHeader SrchAccountGroup;

		private ListView livPriceCategory;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshTextBoxNormal txtPercentage;

		private GroupBox groupBox1;

		private Label label3;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader6;

		public UserPrevilegeDtl plgDtls { get; set; }

		public PriceCategory()
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

		private void loadItems()
		{
			livPriceCategory.Items.Clear();
			try
			{
				foreach (PriceCategoryEntity item in db.PriceCategories())
				{
					ListViewItem listViewItem = new ListViewItem((livPriceCategory.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.PCategoryName);
					listViewItem.SubItems.Add(item.Percentage.ToString(CommonSettings.strCurencyFormat));
					listViewItem.SubItems.Add(item.SDiscountPercentage.ToString(CommonSettings.strCurencyFormat));
					livPriceCategory.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private PriceCategoryEntity EntityMethod()
		{
			return new PriceCategoryEntity
			{
				PCategoryId = iPCategoryId,
				PCategoryName = txtName.Text,
				Percentage = txtPercentage.Value,
				SDiscountPercentage = txtDiscount.Value
			};
		}

		private void PriceCategory_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			loadItems();
		}

		private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void livPriceCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livPriceCategory.SelectedItems.Count != 0)
			{
				PriceCategoryEntity priceCategoryEntity = (from x in db.PriceCategories()
					where x.PCategoryName == livPriceCategory.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtName.Text = priceCategoryEntity.PCategoryName;
				txtPercentage.Value = priceCategoryEntity.Percentage;
				iPCategoryId = priceCategoryEntity.PCategoryId;
				txtDiscount.Value = priceCategoryEntity.SDiscountPercentage;
			}
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			loadItems();
			AniHelper.ClearMethod(this);
			txtName.Focus();
			btnDelete.Enabled = false;
			iPCategoryId = null;
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("PriceCategory", "PCategoryId", iPCategoryId.ToInt32()))
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
			if (txtName.Text == string.Empty)
			{
				Messages.WarningMessage();
			}
			else if (!iPCategoryId.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
				}
				else if (AshproCommon.InsertToDatabaseObj(EntityMethod(), "PriceCategory"))
				{
					Messages.SavedMessage();
					btnClear_Click(null, null);
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else if (AshproCommon.UpdateToDatabaseObj(EntityMethod(), "PriceCategory", "PCategoryId", iPCategoryId.ToInt32()))
			{
				Messages.UpdateMessage();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceCategory));
            this.txtDiscount = new AshUsercontrol.AshTextBoxNormal();
            this.label5 = new System.Windows.Forms.Label();
            this.SrchAccountGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.livPriceCategory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPercentage = new AshUsercontrol.AshTextBoxNormal();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
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
            // txtDiscount
            // 
            resources.ApplyResources(this.txtDiscount, "txtDiscount");
            this.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiscount.Data = 0;
            this.txtDiscount.EnterColor = System.Drawing.Color.LightPink;
            this.txtDiscount.Format = null;
            this.txtDiscount.isAllowNegative = false;
            this.txtDiscount.isAllowSpecialChar = false;
            this.txtDiscount.isNumberOnly = false;
            this.txtDiscount.isNumeric = true;
            this.txtDiscount.LeaveColor = System.Drawing.Color.White;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscount_KeyDown);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // SrchAccountGroup
            // 
            resources.ApplyResources(this.SrchAccountGroup, "SrchAccountGroup");
            // 
            // livPriceCategory
            // 
            resources.ApplyResources(this.livPriceCategory, "livPriceCategory");
            this.livPriceCategory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.SrchAccountGroup,
            this.columnHeader4,
            this.columnHeader6});
            this.livPriceCategory.FullRowSelect = true;
            this.livPriceCategory.GridLines = true;
            this.livPriceCategory.HideSelection = false;
            this.livPriceCategory.Name = "livPriceCategory";
            this.livPriceCategory.UseCompatibleStateImageBehavior = false;
            this.livPriceCategory.View = System.Windows.Forms.View.Details;
            this.livPriceCategory.SelectedIndexChanged += new System.EventHandler(this.livPriceCategory_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtPercentage
            // 
            resources.ApplyResources(this.txtPercentage, "txtPercentage");
            this.txtPercentage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercentage.Data = 0;
            this.txtPercentage.EnterColor = System.Drawing.Color.LightPink;
            this.txtPercentage.Format = null;
            this.txtPercentage.isAllowNegative = false;
            this.txtPercentage.isAllowSpecialChar = false;
            this.txtPercentage.isNumberOnly = false;
            this.txtPercentage.isNumeric = true;
            this.txtPercentage.LeaveColor = System.Drawing.Color.White;
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.livPriceCategory);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // PriceCategory
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPercentage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "PriceCategory";
            this.Load += new System.EventHandler(this.PriceCategory_Load);
            this.Controls.SetChildIndex(this.pnlButton, 0);
            this.Controls.SetChildIndex(this.pnlMaster, 0);
            this.Controls.SetChildIndex(this.lblMasterTitle, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtPercentage, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtDiscount, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
