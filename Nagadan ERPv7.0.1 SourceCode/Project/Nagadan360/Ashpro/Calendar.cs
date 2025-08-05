using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class Calendar : BaseForm
	{
		private int iCPlaningId;

		private byte[] bye;

		private byte[] Outbyt;

		private CulenderPlanning cPlanning;

		private DBContext db;

		private IContainer components = null;

		private Panel panel1;

		private RichTextBox rtbDetails;

		private MonthCalendar monthCalendar;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private ToolTip toolTip1;

		private Button button4;

		private Label lblHead;

		public Calendar()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void Calendar_Load(object sender, EventArgs e)
		{
			db = new DBContext();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("CulenderPlanning", "CPlaningId", iCPlaningId))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message + " 79");
			}
		}

		private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
		{
			rtbDetails.Text = "";
			try
			{
				cPlanning = db.CulenderPlannings().FirstOrDefault((CulenderPlanning x) => x.CplanninDate == monthCalendar.SelectionStart.Date && x.FK_BranchId == Settings.Default.iBranchId);
				if (cPlanning != null)
				{
					iCPlaningId = cPlanning.CPlaningId.toInt32();
					Outbyt = cPlanning.Description;
					rtbDetails.Rtf = Encoding.Unicode.GetString(Outbyt);
					btnDelete.Enabled = true;
					btnSave.Text = "Update";
					btnDelete.ForeColor = Color.White;
				}
				else
				{
					btnSave.Text = "Save";
					btnDelete.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " 113");
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			bye = Encoding.Unicode.GetBytes(rtbDetails.Rtf);
			try
			{
				cPlanning = new CulenderPlanning();
				cPlanning.CPlaningId = iCPlaningId;
				cPlanning.CplanninDate = monthCalendar.SelectionRange.Start.Date;
				cPlanning.Description = bye;
				cPlanning.FK_BranchId = Settings.Default.iBranchId;
				if (btnSave.Text == "Save")
				{
					cPlanning.CPlaningId = null;
					if (AshproCommon.InsertToDatabaseObj(cPlanning, "CulenderPlanning"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (AshproCommon.UpdateToDatabaseObj(cPlanning, "CulenderPlanning", "CPlaningId", iCPlaningId))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message + " 221");
			}
		}

		private void Calendar_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnSave_Click(null, null);
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
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				if (rtbDetails.Rtf != "")
				{
					e.SuppressKeyPress = true;
					btnDelete_Click(null, null);
				}
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				rtbDetails.Focus();
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			rtbDetails.Text = string.Empty;
			monthCalendar.TodayDate = DateTime.Today;
			btnDelete.ForeColor = Color.Black;
			btnDelete.Enabled = false;
			btnSave.Text = "Save";
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
			panel1 = new System.Windows.Forms.Panel();
			rtbDetails = new System.Windows.Forms.RichTextBox();
			monthCalendar = new System.Windows.Forms.MonthCalendar();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			panel1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(604, 1);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(rtbDetails);
			panel1.Controls.Add(monthCalendar);
			panel1.Location = new System.Drawing.Point(12, 36);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(615, 187);
			panel1.TabIndex = 0;
			rtbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			rtbDetails.Location = new System.Drawing.Point(247, 13);
			rtbDetails.Name = "rtbDetails";
			rtbDetails.Size = new System.Drawing.Size(359, 162);
			rtbDetails.TabIndex = 1;
			rtbDetails.Text = "";
			monthCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			monthCalendar.Location = new System.Drawing.Point(8, 13);
			monthCalendar.MaxSelectionCount = 1;
			monthCalendar.Name = "monthCalendar";
			monthCalendar.TabIndex = 0;
			monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar1_DateSelected);
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(552, 235);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 3;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(475, 235);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 2;
			btnDelete.Text = "Delete";
			toolTip1.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(398, 235);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 1;
			btnSave.Text = "Save";
			toolTip1.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			button4.Location = new System.Drawing.Point(1, 29);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(200, 1);
			button4.TabIndex = 94;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 5);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(183, 24);
			lblHead.TabIndex = 93;
			lblHead.Text = "Calender Planning";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(639, 277);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(panel1);
			base.Name = "Calendar";
			base.Load += new System.EventHandler(Calendar_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(Calendar_KeyDown);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
