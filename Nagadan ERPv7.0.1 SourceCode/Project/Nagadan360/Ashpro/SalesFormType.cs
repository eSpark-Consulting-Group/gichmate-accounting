using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SalesFormType : BaseForm
	{
		private List<MasterValue> entMasterValues = new List<MasterValue>();

		private DBContext db = new DBContext();

		private Button btnFormType;

		private IContainer components = null;

		private Label lblTitle;

		private FlowLayoutPanel flpFormTypes;

		private AshTextBoxNormal txtFormType;

		private Button btnCreate;

		private Button btnClearAll;

		public string sFormType { get; set; }

		public SalesFormType()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void ButtonLoadMethod()
		{
			entMasterValues = db.MasterValues(new
			{
				MasterType = "SalesFormType"
			}).ToList();
			if (entMasterValues.Count <= 0)
			{
				return;
			}
			flpFormTypes.Controls.Clear();
			foreach (MasterValue entMasterValue in entMasterValues)
			{
				btnFormType = new Button();
				btnFormType.Text = entMasterValue.Description;
				btnFormType.Name = entMasterValue.Name;
				btnFormType.BackColor = Settings.Default.ProductBB;
				btnFormType.ForeColor = Settings.Default.ProductBF;
				btnFormType.FlatStyle = FlatStyle.Flat;
				btnFormType.FlatAppearance.BorderSize = 0;
				btnFormType.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
				btnFormType.Size = new Size(Utility.pbValues.Name.ToInt32(), Utility.pbValues.Description.ToInt32());
				btnFormType.Click += BtnFormType_Click;
				flpFormTypes.Controls.Add(btnFormType);
			}
		}

		private void SalesFormType_Load(object sender, EventArgs e)
		{
			ButtonLoadMethod();
		}

		private void BtnFormType_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			sFormType = button.Text;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			if (!(txtFormType.Text == string.Empty) && AshproCommon.InsertToDatabaseObj(new
			{
				Name = txtFormType.Text,
				Description = txtFormType.Text,
				MasterType = "SalesFormType"
			}, "MasterValue"))
			{
				ButtonLoadMethod();
				AniHelper.ClearMethod(this);
			}
		}

		private void btnClearAll_Click(object sender, EventArgs e)
		{
			if (Messages.DeleteConfirmationMessage())
			{
				string query = "Delete From MasterValue Where MasterType='SalesFormType'";
				if (AshproCommon.DatabaseMethod(query))
				{
					flpFormTypes.Controls.Clear();
					entMasterValues = new List<MasterValue>();
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
			lblTitle = new System.Windows.Forms.Label();
			flpFormTypes = new System.Windows.Forms.FlowLayoutPanel();
			txtFormType = new AshUsercontrol.AshTextBoxNormal();
			btnCreate = new System.Windows.Forms.Button();
			btnClearAll = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(751, 0);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblTitle.Location = new System.Drawing.Point(0, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(213, 29);
			lblTitle.TabIndex = 58;
			lblTitle.Text = "Sales Form Type";
			flpFormTypes.Location = new System.Drawing.Point(53, 56);
			flpFormTypes.Name = "flpFormTypes";
			flpFormTypes.Size = new System.Drawing.Size(684, 314);
			flpFormTypes.TabIndex = 59;
			txtFormType.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFormType.Data = 0;
			txtFormType.EnterColor = System.Drawing.Color.LightPink;
			txtFormType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtFormType.Format = null;
			txtFormType.isAllowNegative = false;
			txtFormType.isAllowSpecialChar = false;
			txtFormType.isNumberOnly = false;
			txtFormType.isNumeric = false;
			txtFormType.LeaveColor = System.Drawing.Color.White;
			txtFormType.Location = new System.Drawing.Point(94, 403);
			txtFormType.Margin = new System.Windows.Forms.Padding(4);
			txtFormType.Name = "txtFormType";
			txtFormType.Size = new System.Drawing.Size(353, 20);
			txtFormType.TabIndex = 60;
			txtFormType.Value = new decimal(new int[4]);
			txtFormType.WaterMark = null;
			btnCreate.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnCreate.FlatAppearance.BorderSize = 0;
			btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCreate.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnCreate.Location = new System.Drawing.Point(487, 392);
			btnCreate.Name = "btnCreate";
			btnCreate.Size = new System.Drawing.Size(111, 46);
			btnCreate.TabIndex = 61;
			btnCreate.Text = "Create";
			btnCreate.UseVisualStyleBackColor = false;
			btnCreate.Click += new System.EventHandler(btnCreate_Click);
			btnClearAll.BackColor = System.Drawing.Color.FromArgb(0, 192, 192);
			btnClearAll.FlatAppearance.BorderSize = 0;
			btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClearAll.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnClearAll.Location = new System.Drawing.Point(621, 392);
			btnClearAll.Name = "btnClearAll";
			btnClearAll.Size = new System.Drawing.Size(111, 46);
			btnClearAll.TabIndex = 61;
			btnClearAll.Text = "Clear All";
			btnClearAll.UseVisualStyleBackColor = false;
			btnClearAll.Click += new System.EventHandler(btnClearAll_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(800, 450);
			base.Controls.Add(btnClearAll);
			base.Controls.Add(btnCreate);
			base.Controls.Add(txtFormType);
			base.Controls.Add(flpFormTypes);
			base.Controls.Add(lblTitle);
			base.Name = "SalesFormType";
			Text = "SalesFormType";
			base.Load += new System.EventHandler(SalesFormType_Load);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(flpFormTypes, 0);
			base.Controls.SetChildIndex(txtFormType, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(btnCreate, 0);
			base.Controls.SetChildIndex(btnClearAll, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
