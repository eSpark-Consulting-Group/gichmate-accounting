using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshUsercontrol;

namespace Ashpro
{
	public class CustomTransReport : Form
	{
		private string Query;

		private string strPWord;

		private DBContext db;

		private IContainer components = null;

		private Label label1;

		private AshComboBox cmbSavedTran;

		private Button btnLoad;

		private Button btnExcel;

		private Button btnClear;

		private Button btnClose;

		private Button btnGenerate;

		private Label label2;

		private AshTextBoxNormal txtPWord;

		private Button btnSave;

		private Label label3;

		private AshTextBoxNormal txtQName;

		private DataGridView dgReports;

		private CheckBox cbType;

		private RichTextBox txtQuery;

		public CustomTransReport()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void LoadCombo()
		{
			try
			{
				cmbSavedTran.ValueMember = "id";
				cmbSavedTran.DisplayMember = "CustmQryName";
				cmbSavedTran.DataSource = db.CustomQueries().ToList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearMethod()
		{
			txtQName.Text = string.Empty;
			txtQuery.Text = string.Empty;
			txtPWord.Text = string.Empty;
			cmbSavedTran.Text = string.Empty;
			dgReports.DataSource = null;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void CustomTransReport_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			base.WindowState = FormWindowState.Maximized;
			LoadCombo();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				CustomQuery data = new CustomQuery
				{
					id = null,
					CustmQryName = txtQName.Text,
					Query = txtQuery.Text
				};
				AshproCommon.InsertToDatabaseObj(data, "CustomQuery");
				LoadCombo();
				ClearMethod();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtQuery.Text == string.Empty)
				{
					MessageBox.Show("Please Enter Details");
					return;
				}
				strPWord = AniHelper.PWordGeneration();
				if (txtPWord.Text != strPWord)
				{
					MessageBox.Show("The Password entered is wrong");
					return;
				}
				if (txtQuery.SelectedText.Length > 0)
				{
					Query = txtQuery.SelectedText;
				}
				else
				{
					Query = txtQuery.Text;
				}
				if (cbType.Checked)
				{
					dgReports.DataSource = AshproCommon.GetDataTable(Query);
					return;
				}
				if (txtPWord.Text == string.Empty)
				{
					MessageBox.Show("Please Enter Password");
					return;
				}
				AshproCommon.DatabaseMethod(Query);
				MessageBox.Show("Successfylly Completed the process.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ClearMethod();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			try
			{
				CustomQuery customQuery = (from x in db.CustomQueries()
					where x.CustmQryName == cmbSavedTran.Text
					select x).FirstOrDefault();
				txtQuery.Text = customQuery.Query;
				txtQName.Text = cmbSavedTran.Text;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			//AniHelper.ExportToExcelMethod(dgReports, txtQName.Text);
		}

		private void CustomTransReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F6)
			{
				btnGenerate_Click(null, null);
			}
			else if (e.KeyCode == Keys.F2)
			{
				if (cbType.Checked)
				{
					dgReports.DataSource = AshproCommon.GetDataTable(Query);
				}
				else
				{
					AshproCommon.DatabaseMethod(Query);
				}
			}
			else if (e.KeyCode == Keys.F3)
			{
				txtPWord.Text = AniHelper.PWordGeneration();
			}
		}

		private async void btnCheckSum_Click(object sender, EventArgs e)
		{
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
			label1 = new System.Windows.Forms.Label();
			cmbSavedTran = new AshUsercontrol.AshComboBox();
			btnLoad = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			btnClose = new System.Windows.Forms.Button();
			btnGenerate = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			txtPWord = new AshUsercontrol.AshTextBoxNormal();
			btnSave = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			txtQName = new AshUsercontrol.AshTextBoxNormal();
			dgReports = new System.Windows.Forms.DataGridView();
			cbType = new System.Windows.Forms.CheckBox();
			txtQuery = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)dgReports).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 18);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(144, 16);
			label1.TabIndex = 0;
			label1.Text = "Select Transaction :";
			cmbSavedTran.EnterColor = System.Drawing.Color.LightPink;
			cmbSavedTran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSavedTran.FormattingEnabled = true;
			cmbSavedTran.LeaveColor = System.Drawing.Color.White;
			cmbSavedTran.Location = new System.Drawing.Point(151, 18);
			cmbSavedTran.Margin = new System.Windows.Forms.Padding(4);
			cmbSavedTran.Name = "cmbSavedTran";
			cmbSavedTran.Size = new System.Drawing.Size(281, 24);
			cmbSavedTran.TabIndex = 1;
			btnLoad.BackColor = System.Drawing.Color.DodgerBlue;
			btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnLoad.ForeColor = System.Drawing.Color.White;
			btnLoad.Location = new System.Drawing.Point(444, 10);
			btnLoad.Margin = new System.Windows.Forms.Padding(4);
			btnLoad.Name = "btnLoad";
			btnLoad.Size = new System.Drawing.Size(100, 37);
			btnLoad.TabIndex = 22;
			btnLoad.Text = "Load";
			btnLoad.UseVisualStyleBackColor = false;
			btnLoad.Click += new System.EventHandler(btnLoad_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(903, 10);
			btnExcel.Margin = new System.Windows.Forms.Padding(4);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(100, 37);
			btnExcel.TabIndex = 22;
			btnExcel.Text = "Excel";
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(1036, 10);
			btnClear.Margin = new System.Windows.Forms.Padding(4);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(100, 37);
			btnClear.TabIndex = 22;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Location = new System.Drawing.Point(1165, 10);
			btnClose.Margin = new System.Windows.Forms.Padding(4);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(100, 37);
			btnClose.TabIndex = 22;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnGenerate.BackColor = System.Drawing.Color.DodgerBlue;
			btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnGenerate.ForeColor = System.Drawing.Color.White;
			btnGenerate.Location = new System.Drawing.Point(260, 183);
			btnGenerate.Margin = new System.Windows.Forms.Padding(4);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new System.Drawing.Size(100, 37);
			btnGenerate.TabIndex = 22;
			btnGenerate.Text = "Generate";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += new System.EventHandler(btnGenerate_Click);
			label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(35, 194);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(81, 16);
			label2.TabIndex = 23;
			label2.Text = "Password :";
			txtPWord.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			txtPWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtPWord.Data = 0;
			txtPWord.EnterColor = System.Drawing.Color.LightPink;
			txtPWord.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtPWord.Format = null;
			txtPWord.isAllowNegative = false;
			txtPWord.isAllowSpecialChar = false;
			txtPWord.isNumberOnly = false;
			txtPWord.isNumeric = false;
			txtPWord.LeaveColor = System.Drawing.Color.White;
			txtPWord.Location = new System.Drawing.Point(119, 192);
			txtPWord.Margin = new System.Windows.Forms.Padding(4);
			txtPWord.Name = "txtPWord";
			txtPWord.PasswordChar = '*';
			txtPWord.Size = new System.Drawing.Size(132, 16);
			txtPWord.TabIndex = 24;
			txtPWord.Value = new decimal(new int[4]);
			txtPWord.WaterMark = null;
			btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(861, 183);
			btnSave.Margin = new System.Windows.Forms.Padding(4);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(100, 37);
			btnSave.TabIndex = 22;
			btnSave.Text = "Save Query";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(433, 198);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(99, 16);
			label3.TabIndex = 23;
			label3.Text = "Query Name :";
			txtQName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			txtQName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtQName.Data = 0;
			txtQName.EnterColor = System.Drawing.Color.LightPink;
			txtQName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtQName.Format = null;
			txtQName.isAllowNegative = false;
			txtQName.isAllowSpecialChar = false;
			txtQName.isNumberOnly = false;
			txtQName.isNumeric = false;
			txtQName.LeaveColor = System.Drawing.Color.White;
			txtQName.Location = new System.Drawing.Point(529, 194);
			txtQName.Margin = new System.Windows.Forms.Padding(4);
			txtQName.Name = "txtQName";
			txtQName.Size = new System.Drawing.Size(323, 16);
			txtQName.TabIndex = 24;
			txtQName.Value = new decimal(new int[4]);
			txtQName.WaterMark = null;
			dgReports.AllowUserToAddRows = false;
			dgReports.AllowUserToDeleteRows = false;
			dgReports.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgReports.BackgroundColor = System.Drawing.Color.White;
			dgReports.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dgReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgReports.Location = new System.Drawing.Point(17, 254);
			dgReports.Margin = new System.Windows.Forms.Padding(4);
			dgReports.Name = "dgReports";
			dgReports.ReadOnly = true;
			dgReports.RowHeadersVisible = false;
			dgReports.Size = new System.Drawing.Size(1248, 327);
			dgReports.TabIndex = 26;
			cbType.AutoSize = true;
			cbType.Location = new System.Drawing.Point(583, 18);
			cbType.Margin = new System.Windows.Forms.Padding(4);
			cbType.Name = "cbType";
			cbType.Size = new System.Drawing.Size(70, 20);
			cbType.TabIndex = 27;
			cbType.Text = "Report";
			cbType.UseVisualStyleBackColor = true;
			txtQuery.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtQuery.AutoWordSelection = true;
			txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtQuery.Location = new System.Drawing.Point(20, 52);
			txtQuery.Margin = new System.Windows.Forms.Padding(4);
			txtQuery.Name = "txtQuery";
			txtQuery.Size = new System.Drawing.Size(1236, 117);
			txtQuery.TabIndex = 28;
			txtQuery.Text = "";
			txtQuery.WordWrap = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1292, 651);
			base.Controls.Add(txtQuery);
			base.Controls.Add(cbType);
			base.Controls.Add(dgReports);
			base.Controls.Add(txtQName);
			base.Controls.Add(txtPWord);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(btnClose);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnSave);
			base.Controls.Add(btnGenerate);
			base.Controls.Add(btnExcel);
			base.Controls.Add(btnLoad);
			base.Controls.Add(cmbSavedTran);
			base.Controls.Add(label1);
			Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "CustomTransReport";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Custom Trans/Report";
			base.Load += new System.EventHandler(CustomTransReport_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(CustomTransReport_KeyDown);
			((System.ComponentModel.ISupportInitialize)dgReports).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
