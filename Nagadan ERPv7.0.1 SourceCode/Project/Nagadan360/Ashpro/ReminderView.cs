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
	public class ReminderView : BaseForm
	{
		private DBContext db;

		private int iReminderId;

		private IContainer components = null;

		private Label label4;

		private ToolTip toolTip1;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private AshTextBoxNormal txtReminder;

		private Label label10;

		private AshComboBox cmbTrgtUser;

		private Label label3;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshDateTimePicker dtmDate;

		private Label label1;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader1;

		private AshComboBox cmbSrchBy;

		private Label label20;

		private AshComboBox cmbSrch;

		private AshDateTimePicker dtmSrch;

		private ListView livDetails;

		private Button button4;

		private GroupBox grpSearch;

		public bool blPassV { get; set; } = false;


		public ReminderView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void LivLoadMethod(List<ReminderTB> reminders)
		{
			livDetails.Items.Clear();
			int num = 1;
			foreach (ReminderTB x in reminders)
			{
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(x.RemainderHead);
				listViewItem.SubItems.Add(x.ReminderDate.toDateTime().ToString("dd-MMM-yyyy"));
				listViewItem.SubItems.Add((from y in db.Users()
					where y.UserId == x.TargetUserId
					select y).FirstOrDefault().UserName);
				listViewItem.SubItems.Add(x.CurrentDate.toDateTime().ToString("dd-MMM-yyyy"));
				listViewItem.SubItems.Add((from y in db.Users()
					where y.UserId == x.UserId
					select y).FirstOrDefault().UserName);
				livDetails.Items.Add(listViewItem);
				num++;
			}
		}

		private void ReminderLoadMethod()
		{
			List<ReminderTB> list = new List<ReminderTB>();
			list = ((!blPassV) ? (from x in db.Reminders()
				where x.UserId == Settings.Default.intLogOnId && x.TargetUserId == Settings.Default.intLogOnId && x.FK_BranchId == Settings.Default.iBranchId
				select x).ToList() : (from x in db.Reminders()
				where x.TargetUserId == Settings.Default.intLogOnId && x.ReminderDate == AniHelper.GetDate(DateTime.Now.Date) && x.FK_BranchId == Settings.Default.iBranchId
				select x).ToList());
			LivLoadMethod(list);
		}

		private void ReminderView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillUser(cmbTrgtUser);
			ReminderLoadMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			dtmDate.Value = DateTime.Now;
			ReminderLoadMethod();
			txtName.Focus();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("ReminderTB", "ReminderId", iReminderId))
				{
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
			try
			{
				if (!(txtName.Text == string.Empty) && !(txtReminder.Text == string.Empty))
				{
					ReminderTB reminderTB = new ReminderTB
					{
						ReminderId = iReminderId,
						RemainderHead = txtName.Text,
						Remainder = txtReminder.Text,
						CurrentDate = AniHelper.GetDate(DateTime.Now),
						ReminderDate = AniHelper.GetDate(dtmDate.Value),
						TargetUserId = cmbTrgtUser.SelectedValue.ToInt32(),
						UserId = Settings.Default.intLogOnId,
						FK_BranchId = Settings.Default.iBranchId
					};
					if (btnSave.Text == "Save")
					{
						reminderTB.ReminderId = null;
						if (AshproCommon.InsertToDatabaseObj(reminderTB, "ReminderTB"))
						{
							Messages.SavedMessage();
							btnClear_Click(null, null);
						}
					}
					else if (AshproCommon.UpdateToDatabaseObj(reminderTB, "ReminderTB", "ReminderId", iReminderId))
					{
						Messages.UpdateMessage();
						btnClear_Click(null, null);
					}
				}
				else
				{
					MessageBox.Show("Please Enter Full Details", Utility.uManufacturer);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void livDetails_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livDetails.SelectedItems.Count != 0)
			{
				ReminderTB reminderTB = (from x in db.Reminders()
					where x.RemainderHead == livDetails.SelectedItems[0].SubItems[1].Text && x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).FirstOrDefault();
				txtName.Text = reminderTB.RemainderHead;
				txtReminder.Text = reminderTB.Remainder;
				dtmDate.Value = reminderTB.ReminderDate.toDateTime();
				cmbTrgtUser.SelectedValue = reminderTB.TargetUserId;
				iReminderId = reminderTB.ReminderId.ToInt32();
				btnSave.Text = "Update";
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
				txtName.Focus();
				txtName.SelectAll();
			}
		}

		private void cmbSrchBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			List<ReminderTB> list = new List<ReminderTB>();
			switch (cmbSrchBy.SelectedIndex)
			{
			default:
				cmbSrch.Visible = true;
				cmbSrch.ValueMember = "ReminderId";
				cmbSrch.DisplayMember = "RemainderHead";
				cmbSrch.DataSource = (from x in db.Reminders()
					where x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				list = (from x in db.Reminders()
					where x.RemainderHead == dtmSrch.Text && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			case 0:
				cmbSrch.Visible = false;
				list = (from x in db.Reminders()
					where x.UserId == Settings.Default.intLogOnId && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			case 1:
				cmbSrch.Visible = true;
				AniHelper.FillUser(cmbSrch);
				list = (from x in db.Reminders()
					where x.TargetUserId == cmbSrch.SelectedValue.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			case 2:
				cmbSrch.Visible = false;
				list = (from x in db.Reminders()
					where x.CurrentDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			case 3:
				cmbSrch.Visible = false;
				list = (from x in db.Reminders()
					where x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			}
			LivLoadMethod(list);
		}

		private void dtmSrch_ValueChanged(object sender, EventArgs e)
		{
			List<ReminderTB> list = new List<ReminderTB>();
			LivLoadMethod(cmbSrchBy.SelectedIndex switch
			{
				0 => (from x in db.Reminders()
					where x.UserId == Settings.Default.intLogOnId && x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList(), 
				1 => (from x in db.Reminders()
					where x.TargetUserId == cmbSrch.SelectedValue.ToInt32() && x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList(), 
				2 => (from x in db.Reminders()
					where x.CurrentDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList(), 
				3 => (from x in db.Reminders()
					where x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList(), 
				_ => (from x in db.Reminders()
					where x.RemainderHead == dtmSrch.Text && x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList(), 
			});
		}

		private void cmbSrch_SelectedIndexChanged(object sender, EventArgs e)
		{
			List<ReminderTB> reminders = new List<ReminderTB>();
			switch (cmbSrchBy.SelectedIndex)
			{
			case 4:
				reminders = (from x in db.Reminders()
					where x.RemainderHead == dtmSrch.Text && x.ReminderDate == AniHelper.GetDate(dtmSrch.Value) && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			case 1:
				reminders = (from x in db.Reminders()
					where x.TargetUserId == cmbSrch.SelectedValue.ToInt32() && x.FK_BranchId == Settings.Default.iBranchId
					select x).ToList();
				break;
			}
			LivLoadMethod(reminders);
		}

		private void ReminderView_KeyDown(object sender, KeyEventArgs e)
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
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				cmbSrchBy.Focus();
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				txtName.Focus();
				txtName.SelectAll();
			}
			else if (e.KeyCode == Keys.L && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (livDetails.Items.Count > 0)
				{
					livDetails.Focus();
					livDetails.Items[0].Selected = true;
				}
			}
			else
			{
				e.SuppressKeyPress = false;
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
			label4 = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			txtReminder = new AshUsercontrol.AshTextBoxNormal();
			label10 = new System.Windows.Forms.Label();
			cmbTrgtUser = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			dtmDate = new AshUsercontrol.AshDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			columnHeader6 = new System.Windows.Forms.ColumnHeader();
			columnHeader5 = new System.Windows.Forms.ColumnHeader();
			columnHeader4 = new System.Windows.Forms.ColumnHeader();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			cmbSrchBy = new AshUsercontrol.AshComboBox();
			label20 = new System.Windows.Forms.Label();
			cmbSrch = new AshUsercontrol.AshComboBox();
			dtmSrch = new AshUsercontrol.AshDateTimePicker();
			livDetails = new System.Windows.Forms.ListView();
			button4 = new System.Windows.Forms.Button();
			grpSearch = new System.Windows.Forms.GroupBox();
			grpSearch.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(804, 1);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(7, 6);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(101, 24);
			label4.TabIndex = 75;
			label4.Text = "Reminder";
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(333, 375);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 72;
			btnClear.Text = "Clear";
			toolTip1.SetToolTip(btnClear, "Press Alt + C");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(237, 375);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 71;
			btnDelete.Text = "Delete";
			toolTip1.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(138, 375);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 70;
			btnSave.Text = "Save";
			toolTip1.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			txtReminder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtReminder.Data = 0;
			txtReminder.EnterColor = System.Drawing.Color.LightPink;
			txtReminder.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtReminder.Format = null;
			txtReminder.isAllowNegative = false;
			txtReminder.isAllowSpecialChar = false;
			txtReminder.isNumberOnly = false;
			txtReminder.isNumeric = false;
			txtReminder.LeaveColor = System.Drawing.Color.White;
			txtReminder.Location = new System.Drawing.Point(150, 123);
			txtReminder.Margin = new System.Windows.Forms.Padding(4);
			txtReminder.Multiline = true;
			txtReminder.Name = "txtReminder";
			txtReminder.Size = new System.Drawing.Size(301, 224);
			txtReminder.TabIndex = 69;
			txtReminder.Value = new decimal(new int[4]);
			txtReminder.WaterMark = null;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label10.Location = new System.Drawing.Point(57, 126);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(86, 16);
			label10.TabIndex = 68;
			label10.Text = "Remainder :";
			cmbTrgtUser.EnterColor = System.Drawing.Color.LightPink;
			cmbTrgtUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbTrgtUser.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbTrgtUser.FormattingEnabled = true;
			cmbTrgtUser.LeaveColor = System.Drawing.Color.White;
			cmbTrgtUser.Location = new System.Drawing.Point(150, 94);
			cmbTrgtUser.Name = "cmbTrgtUser";
			cmbTrgtUser.Size = new System.Drawing.Size(301, 24);
			cmbTrgtUser.TabIndex = 67;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(46, 95);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(97, 16);
			label3.TabIndex = 66;
			label3.Text = "Target User :";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Location = new System.Drawing.Point(150, 45);
			txtName.Margin = new System.Windows.Forms.Padding(4);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(301, 16);
			txtName.TabIndex = 63;
			txtName.Value = new decimal(new int[4]);
			txtName.WaterMark = null;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(16, 41);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(127, 16);
			label2.TabIndex = 62;
			label2.Text = "Remainder Name :";
			dtmDate.CustomFormat = "dd-MMM-yyyy";
			dtmDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmDate.Location = new System.Drawing.Point(150, 66);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(146, 23);
			dtmDate.TabIndex = 65;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(21, 67);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(122, 16);
			label1.TabIndex = 64;
			label1.Text = "Remainder Date :";
			columnHeader6.Text = "Created By";
			columnHeader5.Text = "Created Date";
			columnHeader5.Width = 100;
			columnHeader4.Text = "Target User";
			columnHeader3.Text = "Remainder Date";
			columnHeader3.Width = 100;
			columnHeader2.Text = "Remainder Name";
			columnHeader2.Width = 150;
			columnHeader1.Text = "SL";
			columnHeader1.Width = 40;
			cmbSrchBy.EnterColor = System.Drawing.Color.LightPink;
			cmbSrchBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSrchBy.FormattingEnabled = true;
			cmbSrchBy.Items.AddRange(new object[5] { "Created User", "Targeted User", "Created Date", "Remainder Date", "Remainder Name" });
			cmbSrchBy.LeaveColor = System.Drawing.Color.White;
			cmbSrchBy.Location = new System.Drawing.Point(161, 19);
			cmbSrchBy.Name = "cmbSrchBy";
			cmbSrchBy.Size = new System.Drawing.Size(176, 24);
			cmbSrchBy.TabIndex = 1;
			cmbSrchBy.SelectedIndexChanged += new System.EventHandler(cmbSrchBy_SelectedIndexChanged);
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(71, 24);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(86, 16);
			label20.TabIndex = 0;
			label20.Text = "Search By :";
			cmbSrch.EnterColor = System.Drawing.Color.LightPink;
			cmbSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbSrch.FormattingEnabled = true;
			cmbSrch.LeaveColor = System.Drawing.Color.White;
			cmbSrch.Location = new System.Drawing.Point(161, 46);
			cmbSrch.Name = "cmbSrch";
			cmbSrch.Size = new System.Drawing.Size(176, 24);
			cmbSrch.TabIndex = 2;
			cmbSrch.SelectedIndexChanged += new System.EventHandler(cmbSrch_SelectedIndexChanged);
			dtmSrch.CustomFormat = "dd-MMM-yyyy";
			dtmSrch.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmSrch.Location = new System.Drawing.Point(161, 73);
			dtmSrch.Name = "dtmSrch";
			dtmSrch.Size = new System.Drawing.Size(176, 23);
			dtmSrch.TabIndex = 3;
			dtmSrch.ValueChanged += new System.EventHandler(dtmSrch_ValueChanged);
			livDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[6] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
			livDetails.FullRowSelect = true;
			livDetails.GridLines = true;
			livDetails.HideSelection = false;
			livDetails.Location = new System.Drawing.Point(10, 102);
			livDetails.Name = "livDetails";
			livDetails.Size = new System.Drawing.Size(347, 267);
			livDetails.TabIndex = 4;
			livDetails.UseCompatibleStateImageBehavior = false;
			livDetails.View = System.Windows.Forms.View.Details;
			livDetails.SelectedIndexChanged += new System.EventHandler(livDetails_SelectedIndexChanged);
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(111, 1);
			button4.TabIndex = 76;
			button4.UseVisualStyleBackColor = true;
			grpSearch.BackColor = System.Drawing.Color.White;
			grpSearch.Controls.Add(cmbSrchBy);
			grpSearch.Controls.Add(label20);
			grpSearch.Controls.Add(cmbSrch);
			grpSearch.Controls.Add(dtmSrch);
			grpSearch.Controls.Add(livDetails);
			grpSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			grpSearch.Location = new System.Drawing.Point(463, 36);
			grpSearch.Name = "grpSearch";
			grpSearch.Size = new System.Drawing.Size(367, 382);
			grpSearch.TabIndex = 74;
			grpSearch.TabStop = false;
			grpSearch.Text = "Search";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(839, 423);
			base.Controls.Add(label4);
			base.Controls.Add(txtReminder);
			base.Controls.Add(label10);
			base.Controls.Add(cmbTrgtUser);
			base.Controls.Add(label3);
			base.Controls.Add(txtName);
			base.Controls.Add(label2);
			base.Controls.Add(dtmDate);
			base.Controls.Add(label1);
			base.Controls.Add(button4);
			base.Controls.Add(grpSearch);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Name = "ReminderView";
			base.Load += new System.EventHandler(ReminderView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ReminderView_KeyDown);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(grpSearch, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmDate, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbTrgtUser, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(txtReminder, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			grpSearch.ResumeLayout(false);
			grpSearch.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
