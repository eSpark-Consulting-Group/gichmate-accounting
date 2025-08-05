using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class FinancialPeriod : BaseForm
	{
		private int? iFYear;

		private int iSFY;

		private DBContext db;

		private IContainer components = null;

		private CheckBox cbClosed;

		private CheckBox cbOld;

		private Label label4;

		private Label label3;

		private Label label2;

		private Button btnClear;

		private Button btnSave;

		private Button btnDelete;

		private ToolTip toolTip1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripMenuItem clearToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem gridviewToolStripMenuItem;

		private ToolStripMenuItem financialYearToolStripMenuItem;

		private Label label1;

		private AshDataGridview dgDetails;

		public AshDateTimePicker dtmTo;

		public AshDateTimePicker dtmFrom;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewDateTimeColumn Column5;
        private DataGridViewDateTimeColumn Column6;
        private DataGridViewCheckBoxColumn Column3;
        private DataGridViewCheckBoxColumn Column4;
        public AshTextBoxNormal txtFYName;

		public UserPrevilegeDtl plgDtls { get; set; }

		public bool isFirstOpening { get; set; } = false;


		public FinancialPeriod()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void loadlist()
		{
			if (db.FinancialYears() != null)
			{
				dgDetails.AutoGenerateColumns = false;
				dgDetails.DataSource = db.FinancialYears();
			}
		}

		private FinancialYear EntityGenrateMethod()
		{
			return new FinancialYear
			{
				FyearId = iFYear,
				FyearName = txtFYName.Text,
				DateFrom = AniHelper.GetDate(dtmFrom.Value),
				DateTo = AniHelper.GetDate(dtmTo.Value),
				Old = cbOld.Checked,
				Closed = cbClosed.Checked
			};
		}

		private void FinanacialYearSelectMethod()
		{
			try
			{
				if (db.SelectedFYears().FirstOrDefault() != null)
				{
					iFYear = db.SelectedFYears().FirstOrDefault().FyearId.ToInt32();
					iSFY = db.SelectedFYears().FirstOrDefault().SlectedFY.toInt32();
					FinancialYear financialYear = (from x in db.FinancialYears()
						where x.FyearId == iFYear
						select x).FirstOrDefault();
					DateTime dateTime = financialYear.DateTo.toDateTime();
					if (dateTime < DateTime.Now)
					{
						MyMsgBox myMsgBox = new MyMsgBox();
						myMsgBox.Passvalue = "Message";
						if (myMsgBox.ShowDialog() == DialogResult.OK)
						{
							financialYear.FyearId = null;
							financialYear.Closed = true;
							financialYear.Old = true;
							AshproCommon.UpdateToDatabaseObj(financialYear, "FinancialYear", "FyearId", iFYear.toInt32());
							SetCurrentFYear(isInsert: false);
						}
					}
				}
				else
				{
					SetCurrentFYear();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SetCurrentFYear(bool isInsert = true)
		{
			iFYear = (from x in db.FinancialYears()
				where x.FyearName == txtFYName.Text
				select x).FirstOrDefault().FyearId.ToInt32();
			SelectedFYear selectedFYear = new SelectedFYear();
			selectedFYear.SlectedFY = null;
			selectedFYear.FyearId = iFYear;
			if (isInsert)
			{
				AshproCommon.InsertToDatabaseObj(selectedFYear, "SelectedFYear");
			}
			else
			{
				AshproCommon.UpdateToDatabaseObj(selectedFYear, "SelectedFYear", "SlectedFY", iSFY);
			}
		}

		private void FinancialPeriod_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			loadlist();
		}

		private void FinancialPeriod_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.F && e.Alt)
			{
				e.SuppressKeyPress = true;
				txtFYName.Focus();
				if (txtFYName.Text != string.Empty)
				{
					txtFYName.SelectAll();
				}
			}
			else if (e.KeyCode == Keys.G && e.Alt)
			{
				e.SuppressKeyPress = true;
				dgDetails.Focus();
				dgDetails.CurrentCell = dgDetails.Rows[0].Cells[0];
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnClear_Click(null, null);
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				e.SuppressKeyPress = true;
				Close();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtFYName.Text = string.Empty;
			dtmFrom.Value = DateTime.Now;
			dtmTo.Value = DateTime.Now;
			cbOld.Checked = false;
			cbClosed.Checked = false;
			iFYear = null;
			btnDelete.Enabled = false;
			loadlist();
			txtFYName.Focus();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("FinancialYear", "FyearId ", iFYear.toInt32()))
				{
					Messages.DeleteMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtFYName.Text == string.Empty)
			{
				Messages.WarningMessage();
			}
			if (!iFYear.HasValue)
			{
				if (plgDtls != null && !plgDtls.SaveData)
				{
					Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
				}
				else if (AshproCommon.InsertToDatabaseObj(EntityGenrateMethod(), "FinancialYear"))
				{
					FinanacialYearSelectMethod();
					if (isFirstOpening)
					{
						Close();
						return;
					}
					btnClear_Click(null, null);
					Messages.SavedMessage();
				}
			}
			else if (plgDtls != null && !plgDtls.EditData)
			{
				Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
			}
			else if (AshproCommon.UpdateToDatabaseObj(EntityGenrateMethod(), "FinancialYear", "FyearId ", iFYear.toInt32()))
			{
				Messages.UpdateMessage();
				btnClear_Click(null, null);
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				FinancialYear financialYear = (FinancialYear)dgDetails.CurrentRow.DataBoundItem;
				txtFYName.Text = financialYear.FyearName;
				iFYear = financialYear.FyearId.ToInt32();
				dtmFrom.Text = financialYear.DateFrom;
				dtmTo.Text = financialYear.DateTo;
				cbOld.Checked = financialYear.Old;
				cbClosed.Checked = financialYear.Closed;
				btnDelete.Enabled = true;
				txtFYName.Focus();
				txtFYName.SelectAll();
			}
		}

		private void txtFYName_KeyDown(object sender, KeyEventArgs e)
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinancialPeriod));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbClosed = new System.Windows.Forms.CheckBox();
            this.dtmTo = new AshUsercontrol.AshDateTimePicker();
            this.dtmFrom = new AshUsercontrol.AshDateTimePicker();
            this.cbOld = new System.Windows.Forms.CheckBox();
            this.txtFYName = new AshUsercontrol.AshTextBoxNormal();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.financialYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.dgDetails = new AshUsercontrol.AshDataGridview();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new AshUsercontrol.DataGridViewDateTimeColumn();
            this.Column6 = new AshUsercontrol.DataGridViewDateTimeColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.toolTip1.SetToolTip(this.btnEnd, resources.GetString("btnEnd.ToolTip"));
            // 
            // cbClosed
            // 
            resources.ApplyResources(this.cbClosed, "cbClosed");
            this.cbClosed.Name = "cbClosed";
            this.cbClosed.TabStop = false;
            this.toolTip1.SetToolTip(this.cbClosed, resources.GetString("cbClosed.ToolTip"));
            this.cbClosed.UseVisualStyleBackColor = true;
            // 
            // dtmTo
            // 
            resources.ApplyResources(this.dtmTo, "dtmTo");
            this.dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmTo.Name = "dtmTo";
            this.toolTip1.SetToolTip(this.dtmTo, resources.GetString("dtmTo.ToolTip"));
            // 
            // dtmFrom
            // 
            resources.ApplyResources(this.dtmFrom, "dtmFrom");
            this.dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtmFrom.Name = "dtmFrom";
            this.toolTip1.SetToolTip(this.dtmFrom, resources.GetString("dtmFrom.ToolTip"));
            // 
            // cbOld
            // 
            resources.ApplyResources(this.cbOld, "cbOld");
            this.cbOld.Name = "cbOld";
            this.cbOld.TabStop = false;
            this.toolTip1.SetToolTip(this.cbOld, resources.GetString("cbOld.ToolTip"));
            this.cbOld.UseVisualStyleBackColor = true;
            // 
            // txtFYName
            // 
            resources.ApplyResources(this.txtFYName, "txtFYName");
            this.txtFYName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFYName.Data = 0;
            this.txtFYName.EnterColor = System.Drawing.Color.LightPink;
            this.txtFYName.Format = null;
            this.txtFYName.isAllowNegative = false;
            this.txtFYName.isAllowSpecialChar = false;
            this.txtFYName.isNumberOnly = false;
            this.txtFYName.isNumeric = false;
            this.txtFYName.LeaveColor = System.Drawing.Color.White;
            this.txtFYName.Name = "txtFYName";
            this.toolTip1.SetToolTip(this.txtFYName, resources.GetString("txtFYName.ToolTip"));
            this.txtFYName.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFYName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFYName_KeyDown);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Name = "btnClear";
            this.btnClear.TabStop = false;
            this.toolTip1.SetToolTip(this.btnClear, resources.GetString("btnClear.ToolTip"));
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.gridviewToolStripMenuItem,
            this.financialYearToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.toolTip1.SetToolTip(this.contextMenuStrip1, resources.GetString("contextMenuStrip1.ToolTip"));
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // deleteToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            // 
            // clearToolStripMenuItem
            // 
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            // 
            // closeToolStripMenuItem
            // 
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            // 
            // gridviewToolStripMenuItem
            // 
            resources.ApplyResources(this.gridviewToolStripMenuItem, "gridviewToolStripMenuItem");
            this.gridviewToolStripMenuItem.Name = "gridviewToolStripMenuItem";
            // 
            // financialYearToolStripMenuItem
            // 
            resources.ApplyResources(this.financialYearToolStripMenuItem, "financialYearToolStripMenuItem");
            this.financialYearToolStripMenuItem.Name = "financialYearToolStripMenuItem";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // dgDetails
            // 
            resources.ApplyResources(this.dgDetails, "dgDetails");
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column5,
            this.Column6,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightPink;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.EnterKeyNavigation = false;
            this.dgDetails.LastKey = System.Windows.Forms.Keys.None;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.TabStop = false;
            this.toolTip1.SetToolTip(this.dgDetails, resources.GetString("dgDetails.ToolTip"));
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellContentClick);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FyearName";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "DateFrom";
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "DateTo";
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Old";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Closed";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FinancialPeriod
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dgDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbClosed);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.cbOld);
            this.Controls.Add(this.txtFYName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "FinancialPeriod";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FinancialPeriod_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FinancialPeriod_KeyDown);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtFYName, 0);
            this.Controls.SetChildIndex(this.cbOld, 0);
            this.Controls.SetChildIndex(this.dtmFrom, 0);
            this.Controls.SetChildIndex(this.dtmTo, 0);
            this.Controls.SetChildIndex(this.cbClosed, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgDetails, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
