using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class FormTypes : BaseForm
	{
		private List<MasterValue> mvFormTypes;

		private DBContext db;

		private IContainer components = null;

		private GroupBox groupBox1;

		private ListView lvFormType;

		private ColumnHeader columnHeader2;

		private ColumnHeader SrchAccountGroup;

		private Button btnClose;

		private ToolTip toolTip1;

		private Button btnClear;

		private Button btnDelete;

		private Button btnOK;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshGradientPanel gpTest;

		private Button button2;

		private Label label8;

		private Button btnAdd;

		public FormTypes()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void LoadMethod()
		{
			mvFormTypes = new List<MasterValue>();
			lvFormType.Items.Clear();
			mvFormTypes = CommonSettings.GetFormType();
			foreach (MasterValue mvFormType in mvFormTypes)
			{
				ListViewItem listViewItem = new ListViewItem((lvFormType.Items.Count + 1).ToString());
				listViewItem.SubItems.Add(mvFormType.Name);
				lvFormType.Items.Add(listViewItem);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void FormTypes_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			btnEnd.Visible = false;
			LoadMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtName.Text = string.Empty;
			LoadMethod();
			btnOK.Enabled = true;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		private void lvFormType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvFormType.SelectedItems.Count > 0)
			{
				btnOK.Enabled = false;
				btnDelete.Enabled = true;
				txtName.Text = lvFormType.SelectedItems[0].SubItems[1].Text;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (Messages.DeleteConfirmationMessage())
			{
				int iValue = db.MasterValues().Find((MasterValue x) => x.Name == txtName.Text).Id.toInt32();
				if (AshproCommon.DeleteFromDatabase("MasterValue", "Id", iValue))
				{
					btnClear_Click(null, null);
				}
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			List<MasterValueTwo> list = new List<MasterValueTwo>();
			MasterValueTwo masterValueTwo = new MasterValueTwo();
			masterValueTwo.Name = txtName.Text;
			masterValueTwo.Description = txtName.Text;
			if (CommonSettings.blnGSTEnabled)
			{
				masterValueTwo.MasterType = "GSTFormTypes";
			}
			else
			{
				masterValueTwo.MasterType = "FormType";
			}
			list.Add(masterValueTwo);
			AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "MasterValue");
			btnClear_Click(null, null);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnAdd_Click(null, null);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvFormType = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SrchAccountGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtName = new AshUsercontrol.AshTextBoxNormal();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gpTest = new AshUsercontrol.AshGradientPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lvFormType);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // lvFormType
            // 
            resources.ApplyResources(this.lvFormType, "lvFormType");
            this.lvFormType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.SrchAccountGroup});
            this.lvFormType.FullRowSelect = true;
            this.lvFormType.GridLines = true;
            this.lvFormType.HideSelection = false;
            this.lvFormType.Name = "lvFormType";
            this.toolTip1.SetToolTip(this.lvFormType, resources.GetString("lvFormType.ToolTip"));
            this.lvFormType.UseCompatibleStateImageBehavior = false;
            this.lvFormType.View = System.Windows.Forms.View.Details;
            this.lvFormType.SelectedIndexChanged += new System.EventHandler(this.lvFormType_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // SrchAccountGroup
            // 
            resources.ApplyResources(this.SrchAccountGroup, "SrchAccountGroup");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Name = "btnClose";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabStop = false;
            this.toolTip1.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Name = "btnOK";
            this.toolTip1.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Data = 0;
            this.txtName.EnterColor = System.Drawing.Color.LightPink;
            this.txtName.Format = null;
            this.txtName.isAllowNegative = false;
            this.txtName.isAllowSpecialChar = false;
            this.txtName.isNumberOnly = false;
            this.txtName.isNumeric = false;
            this.txtName.LeaveColor = System.Drawing.Color.White;
            this.txtName.Name = "txtName";
            this.toolTip1.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // gpTest
            // 
            resources.ApplyResources(this.gpTest, "gpTest");
            this.gpTest.AllowMultiSelect = false;
            this.gpTest.Angle = 0F;
            this.gpTest.BackColor = System.Drawing.Color.DodgerBlue;
            this.gpTest.BottomColor = System.Drawing.Color.DodgerBlue;
            this.gpTest.Name = "gpTest";
            this.gpTest.Selected = false;
            this.gpTest.TextAdjestmentHeight = 0;
            this.gpTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.gpTest, resources.GetString("gpTest.ToolTip"));
            this.gpTest.TopColor = System.Drawing.Color.White;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.toolTip1.SetToolTip(this.button2, resources.GetString("button2.ToolTip"));
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Name = "btnAdd";
            this.toolTip1.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormTypes
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gpTest);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Name = "FormTypes";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormTypes_Load);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.gpTest, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
