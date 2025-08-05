using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ReportView : Form
	{
		private IContainer components = null;

		private ToolTip toolTip1;

		private Button btnClose;

		private Button btnExcel;

		private Button btnMail;

		private Button btnPrint;

		private Panel panel1;

		public DataGridView dgvReport;

		private Label lblTitle;

		public string Title { get; set; }

		public DataTable data { get; set; }

		public DataTable Header { get; set; }

		public DataTable LineData { get; set; }

		public DataTable dataPDF { get; set; }

		public DataTable HeaderPDF { get; set; }

		public DataTable LineDataPDF { get; set; }

		public DataTable Details { get; set; }

		public DataTable Gridview { get; set; }

		public bool blPrint { get; set; } = true;


		public bool blMail { get; set; } = true;


		public bool blLandscape { get; set; } = false;


		public ReportView()
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
		}

		private void ProductFlowReport_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			btnMail.Visible = blMail;
			btnPrint.Visible = blPrint;
			lblTitle.Text = Title;
			dgvReport.DataSource = Details;
			try
			{
				if (Gridview == null)
				{
					return;
				}
				for (int i = 0; i < Gridview.Rows.Count; i++)
				{
					dgvReport.Columns[i].Width = Gridview.Rows[i][0].ToInt32();
					if (Gridview.Rows[i][1].ToString() == "L")
					{
						dgvReport.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
					}
					else
					{
						dgvReport.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnExcel_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			using (DataSet dataSet = new DataSet())
			{
				DataTable dataTable = new DataTable();
				dataTable = Details.Copy();
				dataTable.TableName = "Report";
				dataSet.Tables.Add(dataTable);
				AniHelper.DataSetToExcel(dataSet, Application.StartupPath + "Report.xlsx");
			}
			Cursor = Cursors.Arrow;
		}

		private void ProductFlowReport_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.M && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnMail_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnExcel_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				if (dgvReport.Rows.Count > 0)
				{
					e.SuppressKeyPress = true;
					dgvReport.Focus();
					dgvReport.Rows[0].Cells[0].Selected = true;
				}
			}
			else if (e.KeyCode == Keys.P && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnPrint_Click(null, null);
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			AniHelper aniHelper = new AniHelper();
			aniHelper.PrintMethod(data, Header, LineData, Title, Details, blLandscape);
			Cursor = Cursors.Arrow;
		}

		private void btnMail_Click(object sender, EventArgs e)
		{
			AniHelper aniHelper = new AniHelper();
			aniHelper.MailSendMethod(dataPDF, HeaderPDF, LineDataPDF, Title, dgvReport, blLandscape);
		}

		private void dgvReport_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < dgvReport.Rows.Count; i++)
			{
				if (dgvReport.Rows[i].Cells[0].Value.ToString() == "")
				{
					dgvReport.Rows[i].DefaultCellStyle.Font = new Font(dgvReport.Font, FontStyle.Bold);
				}
			}
		}

		private void ReportView_Activated(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
		}

		private void ReportView_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(new Pen(Color.Silver, 2f), DisplayRectangle);
		}

		private void dgvReport_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
			components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			btnClose = new System.Windows.Forms.Button();
			btnExcel = new System.Windows.Forms.Button();
			btnMail = new System.Windows.Forms.Button();
			btnPrint = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dgvReport = new System.Windows.Forms.DataGridView();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			lblTitle = new System.Windows.Forms.Label();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
			SuspendLayout();
			btnClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Location = new System.Drawing.Point(955, 48);
			btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(100, 37);
			btnClose.TabIndex = 133;
			btnClose.Text = "Close";
			toolTip1.SetToolTip(btnClose, "Press Alt + X");
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			btnExcel.BackColor = System.Drawing.Color.DodgerBlue;
			btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnExcel.ForeColor = System.Drawing.Color.White;
			btnExcel.Location = new System.Drawing.Point(847, 48);
			btnExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnExcel.Name = "btnExcel";
			btnExcel.Size = new System.Drawing.Size(100, 37);
			btnExcel.TabIndex = 132;
			btnExcel.Text = "Excel";
			toolTip1.SetToolTip(btnExcel, "Press Alt + E");
			btnExcel.UseVisualStyleBackColor = false;
			btnExcel.Click += new System.EventHandler(btnExcel_Click);
			btnMail.BackColor = System.Drawing.Color.DodgerBlue;
			btnMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnMail.ForeColor = System.Drawing.Color.White;
			btnMail.Location = new System.Drawing.Point(731, 48);
			btnMail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnMail.Name = "btnMail";
			btnMail.Size = new System.Drawing.Size(100, 37);
			btnMail.TabIndex = 129;
			btnMail.Text = "Mail";
			toolTip1.SetToolTip(btnMail, "Press Alt + M");
			btnMail.UseVisualStyleBackColor = false;
			btnMail.Visible = false;
			btnMail.Click += new System.EventHandler(btnMail_Click);
			btnPrint.BackColor = System.Drawing.Color.DodgerBlue;
			btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPrint.ForeColor = System.Drawing.Color.White;
			btnPrint.Location = new System.Drawing.Point(615, 48);
			btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(100, 37);
			btnPrint.TabIndex = 131;
			btnPrint.Text = "Print";
			toolTip1.SetToolTip(btnPrint, "Press Alt + P");
			btnPrint.UseVisualStyleBackColor = false;
			btnPrint.Visible = false;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvReport);
			panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(15, 124);
			panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(1028, 321);
			panel1.TabIndex = 128;
			dgvReport.AllowUserToAddRows = false;
			dgvReport.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dgvReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvReport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgvReport.BackgroundColor = System.Drawing.Color.WhiteSmoke;
			dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgvReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dgvReport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgvReport.ColumnHeadersHeight = 40;
			dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvReport.DefaultCellStyle = dataGridViewCellStyle3;
			dgvReport.Location = new System.Drawing.Point(16, 15);
			dgvReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			dgvReport.Name = "dgvReport";
			dgvReport.ReadOnly = true;
			dgvReport.RowHeadersVisible = false;
			dgvReport.RowHeadersWidth = 51;
			dgvReport.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dgvReport.RowTemplate.Height = 20;
			dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvReport.Size = new System.Drawing.Size(997, 292);
			dgvReport.TabIndex = 0;
			dgvReport.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvReport_DataError);
			dgvReport.Paint += new System.Windows.Forms.PaintEventHandler(dgvReport_Paint);
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(31, 96);
			lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(27, 20);
			lblTitle.TabIndex = 134;
			lblTitle.Text = "---";
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = btnClose;
			base.ClientSize = new System.Drawing.Size(1059, 458);
			base.Controls.Add(lblTitle);
			base.Controls.Add(btnClose);
			base.Controls.Add(btnExcel);
			base.Controls.Add(btnMail);
			base.Controls.Add(btnPrint);
			base.Controls.Add(panel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MinimizeBox = false;
			base.Name = "ReportView";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "ProductFlowReport";
			base.Activated += new System.EventHandler(ReportView_Activated);
			base.Load += new System.EventHandler(ProductFlowReport_Load);
			base.Paint += new System.Windows.Forms.PaintEventHandler(ReportView_Paint);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ProductFlowReport_KeyDown);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
