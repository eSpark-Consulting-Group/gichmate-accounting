using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SecondDisplaySettings : BaseForm
	{
		private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

		private IContainer components = null;

		private Label lblTitle;

		private Button btnCompanyBColor;

		private Button btnCompanyFColor;

		private Button btnProductBColor;

		private Button btnProDetailsBColor;

		private Button btnCancel;

		private Button btnOK;

		private Button btnInvoiceTotalBColor;

		private AshTextBoxNormal txtIntervells;

		private Label label1;

		private Label label2;

		private AshTextBoxNormal txtFolderPath;

		private Button btnFolderBrouse;

		public SecondDisplaySettings()
		{
			InitializeComponent();
		}

		private void btnFolderBrouse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtFolderPath.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Settings.Default.CompanyBColor = btnCompanyBColor.BackColor;
			Settings.Default.CompanyFColor = btnCompanyFColor.BackColor;
			Settings.Default.ProImageBColor = btnProductBColor.BackColor;
			Settings.Default.ProDetailsBColor = btnProDetailsBColor.BackColor;
			Settings.Default.TotalLabelBColor = btnInvoiceTotalBColor.BackColor;
			Settings.Default.IdleTime = txtIntervells.Data;
			Settings.Default.strImageFolderSecondDisplay = txtFolderPath.Text;
			Settings.Default.Save();
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCompanyBColor_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				button.BackColor = colorDialog.Color;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void SecondDisplaySettings_Load(object sender, EventArgs e)
		{
			btnCompanyBColor.BackColor = Settings.Default.CompanyBColor;
			btnCompanyFColor.BackColor = Settings.Default.CompanyFColor;
			btnProductBColor.BackColor = Settings.Default.ProImageBColor;
			btnProDetailsBColor.BackColor = Settings.Default.ProDetailsBColor;
			btnInvoiceTotalBColor.BackColor = Settings.Default.TotalLabelBColor;
			txtIntervells.Data = Settings.Default.IdleTime;
			txtFolderPath.Text = Settings.Default.strImageFolderSecondDisplay;
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
			btnCompanyBColor = new System.Windows.Forms.Button();
			btnCompanyFColor = new System.Windows.Forms.Button();
			btnProductBColor = new System.Windows.Forms.Button();
			btnProDetailsBColor = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			btnInvoiceTotalBColor = new System.Windows.Forms.Button();
			txtIntervells = new AshUsercontrol.AshTextBoxNormal();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			txtFolderPath = new AshUsercontrol.AshTextBoxNormal();
			btnFolderBrouse = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(335, 1);
			btnEnd.Size = new System.Drawing.Size(38, 32);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(2, 3);
			lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(297, 29);
			lblTitle.TabIndex = 18;
			lblTitle.Text = "Second Display Settings";
			btnCompanyBColor.BackColor = System.Drawing.Color.Black;
			btnCompanyBColor.FlatAppearance.BorderSize = 0;
			btnCompanyBColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCompanyBColor.ForeColor = System.Drawing.Color.White;
			btnCompanyBColor.Location = new System.Drawing.Point(24, 70);
			btnCompanyBColor.Name = "btnCompanyBColor";
			btnCompanyBColor.Size = new System.Drawing.Size(246, 42);
			btnCompanyBColor.TabIndex = 19;
			btnCompanyBColor.Text = "Company Name Back Color";
			btnCompanyBColor.UseVisualStyleBackColor = false;
			btnCompanyBColor.Click += new System.EventHandler(btnCompanyBColor_Click);
			btnCompanyFColor.BackColor = System.Drawing.Color.Black;
			btnCompanyFColor.FlatAppearance.BorderSize = 0;
			btnCompanyFColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCompanyFColor.ForeColor = System.Drawing.Color.White;
			btnCompanyFColor.Location = new System.Drawing.Point(24, 118);
			btnCompanyFColor.Name = "btnCompanyFColor";
			btnCompanyFColor.Size = new System.Drawing.Size(246, 42);
			btnCompanyFColor.TabIndex = 19;
			btnCompanyFColor.Text = "Company Name Fore Color";
			btnCompanyFColor.UseVisualStyleBackColor = false;
			btnCompanyFColor.Click += new System.EventHandler(btnCompanyBColor_Click);
			btnProductBColor.BackColor = System.Drawing.Color.Black;
			btnProductBColor.FlatAppearance.BorderSize = 0;
			btnProductBColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnProductBColor.ForeColor = System.Drawing.Color.White;
			btnProductBColor.Location = new System.Drawing.Point(24, 166);
			btnProductBColor.Name = "btnProductBColor";
			btnProductBColor.Size = new System.Drawing.Size(246, 42);
			btnProductBColor.TabIndex = 19;
			btnProductBColor.Text = "Product Image Back Color";
			btnProductBColor.UseVisualStyleBackColor = false;
			btnProductBColor.Click += new System.EventHandler(btnCompanyBColor_Click);
			btnProDetailsBColor.BackColor = System.Drawing.Color.Black;
			btnProDetailsBColor.FlatAppearance.BorderSize = 0;
			btnProDetailsBColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnProDetailsBColor.ForeColor = System.Drawing.Color.White;
			btnProDetailsBColor.Location = new System.Drawing.Point(24, 214);
			btnProDetailsBColor.Name = "btnProDetailsBColor";
			btnProDetailsBColor.Size = new System.Drawing.Size(246, 42);
			btnProDetailsBColor.TabIndex = 19;
			btnProDetailsBColor.Text = "Product Details Back Color";
			btnProDetailsBColor.UseVisualStyleBackColor = false;
			btnProDetailsBColor.Click += new System.EventHandler(btnCompanyBColor_Click);
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(268, 405);
			btnCancel.Margin = new System.Windows.Forms.Padding(4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(100, 37);
			btnCancel.TabIndex = 21;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Location = new System.Drawing.Point(160, 405);
			btnOK.Margin = new System.Windows.Forms.Padding(4);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(100, 37);
			btnOK.TabIndex = 20;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			btnInvoiceTotalBColor.BackColor = System.Drawing.Color.Black;
			btnInvoiceTotalBColor.FlatAppearance.BorderSize = 0;
			btnInvoiceTotalBColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnInvoiceTotalBColor.ForeColor = System.Drawing.Color.White;
			btnInvoiceTotalBColor.Location = new System.Drawing.Point(24, 262);
			btnInvoiceTotalBColor.Name = "btnInvoiceTotalBColor";
			btnInvoiceTotalBColor.Size = new System.Drawing.Size(246, 42);
			btnInvoiceTotalBColor.TabIndex = 19;
			btnInvoiceTotalBColor.Text = "Invoice Total Back Color";
			btnInvoiceTotalBColor.UseVisualStyleBackColor = false;
			btnInvoiceTotalBColor.Click += new System.EventHandler(btnCompanyBColor_Click);
			txtIntervells.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtIntervells.Data = 8000;
			txtIntervells.EnterColor = System.Drawing.Color.LightPink;
			txtIntervells.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtIntervells.Format = null;
			txtIntervells.isAllowNegative = false;
			txtIntervells.isAllowSpecialChar = false;
			txtIntervells.isNumberOnly = true;
			txtIntervells.isNumeric = false;
			txtIntervells.LeaveColor = System.Drawing.Color.White;
			txtIntervells.Location = new System.Drawing.Point(127, 324);
			txtIntervells.Margin = new System.Windows.Forms.Padding(5);
			txtIntervells.Name = "txtIntervells";
			txtIntervells.Size = new System.Drawing.Size(143, 20);
			txtIntervells.TabIndex = 23;
			txtIntervells.Text = "8000";
			txtIntervells.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtIntervells.Value = new decimal(new int[4] { 8000, 0, 0, 0 });
			txtIntervells.WaterMark = null;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(2, 324);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(97, 22);
			label1.TabIndex = 22;
			label1.Text = "Intervell :";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(2, 354);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(121, 22);
			label2.TabIndex = 22;
			label2.Text = "Folder Path :";
			txtFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFolderPath.Data = 0;
			txtFolderPath.EnterColor = System.Drawing.Color.LightPink;
			txtFolderPath.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtFolderPath.Format = null;
			txtFolderPath.isAllowNegative = false;
			txtFolderPath.isAllowSpecialChar = false;
			txtFolderPath.isNumberOnly = false;
			txtFolderPath.isNumeric = false;
			txtFolderPath.LeaveColor = System.Drawing.Color.White;
			txtFolderPath.Location = new System.Drawing.Point(127, 354);
			txtFolderPath.Margin = new System.Windows.Forms.Padding(5);
			txtFolderPath.Name = "txtFolderPath";
			txtFolderPath.Size = new System.Drawing.Size(143, 20);
			txtFolderPath.TabIndex = 23;
			txtFolderPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			txtFolderPath.Value = new decimal(new int[4]);
			txtFolderPath.WaterMark = null;
			btnFolderBrouse.BackColor = System.Drawing.Color.DimGray;
			btnFolderBrouse.FlatAppearance.BorderSize = 0;
			btnFolderBrouse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnFolderBrouse.ForeColor = System.Drawing.Color.White;
			btnFolderBrouse.Location = new System.Drawing.Point(280, 351);
			btnFolderBrouse.Name = "btnFolderBrouse";
			btnFolderBrouse.Size = new System.Drawing.Size(34, 23);
			btnFolderBrouse.TabIndex = 24;
			btnFolderBrouse.Text = "..";
			btnFolderBrouse.UseVisualStyleBackColor = false;
			btnFolderBrouse.Click += new System.EventHandler(btnFolderBrouse_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(375, 450);
			base.Controls.Add(btnFolderBrouse);
			base.Controls.Add(txtFolderPath);
			base.Controls.Add(label2);
			base.Controls.Add(txtIntervells);
			base.Controls.Add(label1);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnOK);
			base.Controls.Add(btnInvoiceTotalBColor);
			base.Controls.Add(btnProDetailsBColor);
			base.Controls.Add(btnProductBColor);
			base.Controls.Add(btnCompanyFColor);
			base.Controls.Add(btnCompanyBColor);
			base.Controls.Add(lblTitle);
			base.Name = "SecondDisplaySettings";
			Text = "SecondDisplaySettings";
			base.Load += new System.EventHandler(SecondDisplaySettings_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(btnCompanyBColor, 0);
			base.Controls.SetChildIndex(btnCompanyFColor, 0);
			base.Controls.SetChildIndex(btnProductBColor, 0);
			base.Controls.SetChildIndex(btnProDetailsBColor, 0);
			base.Controls.SetChildIndex(btnInvoiceTotalBColor, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtIntervells, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtFolderPath, 0);
			base.Controls.SetChildIndex(btnFolderBrouse, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
