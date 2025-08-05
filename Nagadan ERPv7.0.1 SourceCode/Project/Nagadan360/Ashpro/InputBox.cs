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
	public class InputBox : BaseForm
	{
		private IContainer components = null;

		private Button btnOK;

		private Button btnCancel;

		public AshTextBoxNormal txtInput;

		public Label lblHead;

		public Label lblCaption;

		public Label lblInput;

		public InputBox()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			lblHead.Text = Utility.uManufacturer;
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

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnOK_Click(null, null);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.InputBox));
			lblHead = new System.Windows.Forms.Label();
			lblCaption = new System.Windows.Forms.Label();
			lblInput = new System.Windows.Forms.Label();
			txtInput = new AshUsercontrol.AshTextBoxNormal();
			btnOK = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			resources.ApplyResources(lblHead, "lblHead");
			lblHead.Name = "lblHead";
			resources.ApplyResources(lblCaption, "lblCaption");
			lblCaption.Name = "lblCaption";
			resources.ApplyResources(lblInput, "lblInput");
			lblInput.Name = "lblInput";
			txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtInput.Data = 0;
			txtInput.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtInput, "txtInput");
			txtInput.Format = null;
			txtInput.isAllowNegative = false;
			txtInput.isAllowSpecialChar = false;
			txtInput.isNumberOnly = false;
			txtInput.isNumeric = false;
			txtInput.LeaveColor = System.Drawing.Color.White;
			txtInput.Name = "txtInput";
			txtInput.Value = new decimal(new int[4]);
			txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(txtInput_KeyDown);
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Name = "btnOK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Name = "btnCancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnOK);
			base.Controls.Add(txtInput);
			base.Controls.Add(lblInput);
			base.Controls.Add(lblCaption);
			base.Controls.Add(lblHead);
			base.Name = "InputBox";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(lblCaption, 0);
			base.Controls.SetChildIndex(lblInput, 0);
			base.Controls.SetChildIndex(txtInput, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
