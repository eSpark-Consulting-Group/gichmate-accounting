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
	public class SelectedFYear
	{
		public int? SlectedFY { get; set; }

		public int? FyearId { get; set; }
	}
	public class SelectedFyear : BaseForm
	{
		private FinancialYear financialYear;

		private SelectedFYear selectedFYear;

		private int iFYearId;

		private int? iSelectedFY;

		private DBContext db;

		private IContainer components = null;

		private Button btnSave;

		private AshDateTimePicker dtTo;

		private AshDateTimePicker dtFrom;

		private AshTextBoxNormal txtFYName;

		private Label label4;

		private Label label3;

		private Label label2;

		private AshDataGridview dgDetails;

		private ToolTip toolTip1;

		private Label label1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewDateTimeColumn Column3;

		private DataGridViewDateTimeColumn Column4;

		public UserPrevilegeDtl plgDtls { get; set; }

		public SelectedFyear()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void loadSelectedAY()
		{
			selectedFYear = db.SelectedFYears().FirstOrDefault();
			if (selectedFYear != null)
			{
				iFYearId = selectedFYear.FyearId.ToInt32();
				iSelectedFY = selectedFYear.SlectedFY.ToInt32();
			}
			financialYear = (from x in db.FinancialYears()
				where x.FyearId == iFYearId
				select x).FirstOrDefault();
			txtFYName.Text = financialYear.FyearName;
			dtFrom.Text = financialYear.DateFrom;
			dtTo.Text = financialYear.DateTo;
		}

		private void loadlist()
		{
			dgDetails.AutoGenerateColumns = false;
			dgDetails.DataSource = (from x in db.FinancialYears()
				where !x.Old && !x.Closed
				select x).ToList();
		}

		private void SelectedFyear_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			loadSelectedAY();
			loadlist();
		}

		private void btnEnd_MouseEnter(object sender, EventArgs e)
		{
			btnEnd.ForeColor = Color.White;
			btnEnd.BackColor = Color.Red;
		}

		private void btnEnd_MouseLeave(object sender, EventArgs e)
		{
			btnEnd.ForeColor = Color.DarkGray;
			btnEnd.BackColor = Color.White;
		}

		private void SelectedFyear_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
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

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				financialYear = (FinancialYear)dgDetails.CurrentRow.DataBoundItem;
				iFYearId = financialYear.FyearId.ToInt32();
				txtFYName.Text = financialYear.FyearName;
				dtFrom.Text = financialYear.DateFrom;
				dtTo.Text = financialYear.DateTo;
				dtFrom.Enabled = false;
				dtTo.Enabled = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtFYName.Text == string.Empty)
				{
					Messages.WarningMessage();
				}
				selectedFYear = new SelectedFYear();
				selectedFYear.SlectedFY = iSelectedFY;
				selectedFYear.FyearId = iFYearId.ToInt32();
				if (!iSelectedFY.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
					}
					else if (AshproCommon.InsertToDatabaseObj(selectedFYear, "SelectedFYear"))
					{
						Messages.SavedMessage();
						iSelectedFY = db.SelectedFYears().FirstOrDefault().FyearId;
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(selectedFYear, "SelectedFYear", "SlectedFY", iSelectedFY.ToInt32()))
				{
					Messages.UpdateMessage();
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SelectedFyear));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			btnSave = new System.Windows.Forms.Button();
			dtTo = new AshUsercontrol.AshDateTimePicker();
			dtFrom = new AshUsercontrol.AshDateTimePicker();
			txtFYName = new AshUsercontrol.AshTextBoxNormal();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			dgDetails = new AshUsercontrol.AshDataGridview();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new AshUsercontrol.DataGridViewDateTimeColumn();
			Column4 = new AshUsercontrol.DataGridViewDateTimeColumn();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			SuspendLayout();
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			toolTip1.SetToolTip(btnEnd, resources.GetString("btnEnd.ToolTip"));
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Name = "btnSave";
			toolTip1.SetToolTip(btnSave, resources.GetString("btnSave.ToolTip"));
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			resources.ApplyResources(dtTo, "dtTo");
			dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtTo.Name = "dtTo";
			toolTip1.SetToolTip(dtTo, resources.GetString("dtTo.ToolTip"));
			resources.ApplyResources(dtFrom, "dtFrom");
			dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtFrom.Name = "dtFrom";
			toolTip1.SetToolTip(dtFrom, resources.GetString("dtFrom.ToolTip"));
			resources.ApplyResources(txtFYName, "txtFYName");
			txtFYName.BackColor = System.Drawing.Color.White;
			txtFYName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFYName.Data = 0;
			txtFYName.EnterColor = System.Drawing.Color.LightPink;
			txtFYName.Format = null;
			txtFYName.isAllowNegative = false;
			txtFYName.isAllowSpecialChar = false;
			txtFYName.isNumberOnly = false;
			txtFYName.isNumeric = false;
			txtFYName.LeaveColor = System.Drawing.Color.White;
			txtFYName.Name = "txtFYName";
			txtFYName.ReadOnly = true;
			toolTip1.SetToolTip(txtFYName, resources.GetString("txtFYName.ToolTip"));
			txtFYName.Value = new decimal(new int[4]);
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			toolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip"));
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			toolTip1.SetToolTip(label3, resources.GetString("label3.ToolTip"));
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			toolTip1.SetToolTip(label2, resources.GetString("label2.ToolTip"));
			resources.ApplyResources(dgDetails, "dgDetails");
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(Column2, Column3, Column4);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgDetails.EnableHeadersVisualStyles = false;
			dgDetails.EnterKeyNavigation = false;
			dgDetails.LastKey = System.Windows.Forms.Keys.None;
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			toolTip1.SetToolTip(dgDetails, resources.GetString("dgDetails.ToolTip"));
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellContentClick);
			Column2.DataPropertyName = "FyearName";
			resources.ApplyResources(Column2, "Column2");
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column3.DataPropertyName = "DateFrom";
			resources.ApplyResources(Column3, "Column3");
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			Column4.DataPropertyName = "DateTo";
			resources.ApplyResources(Column4, "Column4");
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
			base.AcceptButton = btnSave;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(dgDetails);
			base.Controls.Add(label1);
			base.Controls.Add(btnSave);
			base.Controls.Add(dtTo);
			base.Controls.Add(dtFrom);
			base.Controls.Add(txtFYName);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.MaximizeBox = false;
			base.Name = "SelectedFyear";
			toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			base.Load += new System.EventHandler(SelectedFyear_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SelectedFyear_KeyDown);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(txtFYName, 0);
			base.Controls.SetChildIndex(dtFrom, 0);
			base.Controls.SetChildIndex(dtTo, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
