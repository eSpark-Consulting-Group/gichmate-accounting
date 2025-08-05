using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class POSInputBox : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private DashboardPOSKeyboard nkPOS;

		private Button btnOK;

		public AshTextBoxNormal txtInput;

		public AshTextBoxNormal txtName;

		public CheckBox cbInclusive;

		public AshComboBox cmbTCategory;

		private Label lblHead;

		private Label lblCaption;

		public bool isNotInput { get; set; } = false;


		public bool isNameVisible { get; set; } = false;


		public bool isScaleWithNoRate { get; set; } = false;


		public POSInputBox()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			lblHead.Text = Utility.uManufacturer;
			nkPOS.StarButton.Text = "C";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!isNotInput || !(txtName.Text == string.Empty))
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void POSInputBox_Load(object sender, EventArgs e)
		{
			foreach (Control control in nkPOS.Controls)
			{
				if (control is Button)
				{
					control.BackColor = Settings.Default.OtherB;
					control.ForeColor = Settings.Default.OtherF;
				}
			}
			lblHead.Text = Utility.uManufacturer;
			CheckBox checkBox = cbInclusive;
			bool visible = (txtName.Visible = isNameVisible);
			checkBox.Visible = visible;
			if (isNotInput)
			{
				AshTextBoxNormal ashTextBoxNormal = txtName;
				AshComboBox ashComboBox = cmbTCategory;
				visible = true;
				ashComboBox.Visible = true;
				ashTextBoxNormal.Visible = true;
				AniHelper.FillTCategory(cmbTCategory);
				base.Size = new Size(280, 450);
			}
			if (isScaleWithNoRate)
			{
				txtName.isNumeric = true;
				txtName.WaterMark = "Mesearment";
			}
		}

		private void dashboardPOSKeyboard1_atStarClick(object source)
		{
			SendKeys.Send("{BACKSPACE}");
		}

		private void btnEnd_Click_1(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void txtName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (AshproCommon.GetList_SP<ProductEntity>("spGetProduct", new
				{
					ProductName = txtName.Text
				}).Count != 0)
				{
					Messages.ErrorMessage("Product already exist!");
				}
				else
				{
					btnOK.PerformClick();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.POSInputBox));
			lblHead = new System.Windows.Forms.Label();
			nkPOS = new AshUsercontrol.DashboardPOSKeyboard();
			txtInput = new AshUsercontrol.AshTextBoxNormal();
			btnOK = new System.Windows.Forms.Button();
			lblCaption = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			cbInclusive = new System.Windows.Forms.CheckBox();
			cmbTCategory = new AshUsercontrol.AshComboBox();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.Click += new System.EventHandler(btnEnd_Click_1);
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			resources.ApplyResources(nkPOS, "nkPOS");
			nkPOS.Name = "nkPOS";
			nkPOS.TabStop = false;
			nkPOS.atStarClick += new AshUsercontrol.StarClickEventHandler(dashboardPOSKeyboard1_atStarClick);
			txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtInput.Data = 0;
			txtInput.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtInput, "txtInput");
			txtInput.Format = null;
			txtInput.isAllowNegative = false;
			txtInput.isAllowSpecialChar = false;
			txtInput.isNumberOnly = false;
			txtInput.isNumeric = true;
			txtInput.LeaveColor = System.Drawing.Color.White;
			txtInput.Name = "txtInput";
			txtInput.Value = new decimal(new int[4]);
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Name = "btnOK";
			btnOK.TabStop = false;
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			resources.ApplyResources(lblCaption, "lblCaption");
			lblCaption.Name = "lblCaption";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtName, "txtName");
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Name = "txtName";
			txtName.Value = new decimal(new int[4]);
			txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(txtName_KeyDown);
			resources.ApplyResources(cbInclusive, "cbInclusive");
			cbInclusive.Checked = true;
			cbInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
			cbInclusive.Name = "cbInclusive";
			cbInclusive.TabStop = false;
			cbInclusive.UseVisualStyleBackColor = true;
			cmbTCategory.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(cmbTCategory, "cmbTCategory");
			cmbTCategory.FormattingEnabled = true;
			cmbTCategory.LeaveColor = System.Drawing.Color.White;
			cmbTCategory.Name = "cmbTCategory";
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(cmbTCategory);
			base.Controls.Add(cbInclusive);
			base.Controls.Add(lblCaption);
			base.Controls.Add(btnOK);
			base.Controls.Add(txtName);
			base.Controls.Add(txtInput);
			base.Controls.Add(nkPOS);
			base.Controls.Add(lblHead);
			base.Name = "POSInputBox";
			base.Load += new System.EventHandler(POSInputBox_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(nkPOS, 0);
			base.Controls.SetChildIndex(txtInput, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(lblCaption, 0);
			base.Controls.SetChildIndex(cbInclusive, 0);
			base.Controls.SetChildIndex(cmbTCategory, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
