using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class UserPrevilegeView : BaseForm
	{
		private DBContext db;

		private List<ExUserPrevilages> userPrevilages;

		private List<UserPrevilegeDtl> previlegeDtls;

		private int iHdrId;

		private IContainer components = null;

		private Button button4;

		private Label lblHead;

		private AshTextBoxNormal txtName;

		private Label label1;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Button btnSearch;

		private Panel panel1;

		private DataGridView dgvDetails;

		private BindingSource bdsDetails;

		private DataGridViewCheckBoxColumn col_Enable;

		private DataGridViewTextBoxColumn col_MenuName;

		private DataGridViewTextBoxColumn col_MenuTitle;

		private DataGridViewCheckBoxColumn col_Save;

		private DataGridViewCheckBoxColumn col_Edit;

		private DataGridViewCheckBoxColumn col_Delete;

		private CheckBox cbEnableAll;

		private CheckBox cbSaveAll;

		private CheckBox cbEditAll;

		private CheckBox cbDeleteAll;

		public UserPrevilegeView()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void BindDGV()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = userPrevilages;
			dgvDetails.DataSource = userPrevilages;
		}

		private void InitialLoading()
		{
			userPrevilages = new List<ExUserPrevilages>();
			foreach (MenuDetails item in db.MenuDetails(new
			{
				IsForm = true
			}).ToList())
			{
				ExUserPrevilages item2 = new ExUserPrevilages
				{
					Enable = true,
					MenuCaption = item.MenuCaption,
					MenuId = item.Id.toInt32(),
					SaveData = true,
					EditData = true,
					DeleteData = true,
					MenuTitle = db.MenuDetails().Single((MenuDetails x) => x.Id == item.ParentId).MenuCaption
				};
				userPrevilages.Add(item2);
			}
			BindDGV();
		}

		private void DtlsEntityLoadMethod(int intHdrId, int? iId = null)
		{
			previlegeDtls = new List<UserPrevilegeDtl>();
			foreach (ExUserPrevilages userPrevilage in userPrevilages)
			{
				userPrevilage.Id = iId;
				userPrevilage.FK_MenuDetailsId = userPrevilage.MenuId;
				userPrevilage.FK_UserPrevilegeHdrId = intHdrId;
				userPrevilage.Visible = userPrevilage.Enable;
			}
			previlegeDtls = AniHelper.CopyListData<UserPrevilegeDtl>(userPrevilages.Cast<object>().ToList());
		}

		private void UserPrevilegeView_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			InitialLoading();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			InitialLoading();
			previlegeDtls = new List<UserPrevilegeDtl>();
			txtName.Text = string.Empty;
			btnSave.Text = "Save";
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (txtName.Text == string.Empty)
			{
				Messages.ErrorMessage("Please enter the User Role Name and try later");
				return;
			}
			UserPrevilegeHdr userPrevilegeHdr = new UserPrevilegeHdr
			{
				Id = iHdrId,
				Name = txtName.Text
			};
			if (btnSave.Text == "Save")
			{
				userPrevilegeHdr.Id = null;
				if (AshproCommon.InsertToDatabaseObj(userPrevilegeHdr))
				{
					iHdrId = db.UserPrevilegeHdrs().Single((UserPrevilegeHdr x) => x.Name == txtName.Text).Id.toInt32();
					DtlsEntityLoadMethod(iHdrId);
					if (AshproCommon.InsertToDatabase(previlegeDtls.Cast<object>().ToList()))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
			}
			else if (AshproCommon.UpdateToDatabaseObj(userPrevilegeHdr, "UserPrevilegeHdr", "Id", iHdrId))
			{
				previlegeDtls = AniHelper.CopyListData<UserPrevilegeDtl>(userPrevilages.Cast<object>().ToList());
				previlegeDtls.ForEach(delegate(UserPrevilegeDtl x)
				{
					x.Visible = x.Enable;
				});
				if (AshproCommon.UpdateToDatabase(previlegeDtls.Cast<object>().ToList(), "UserPrevilegeDtl", "Id"))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("UserPrevilegeDtl", "FK_UserPrevilegeHdrId", iHdrId) && AshproCommon.DeleteFromDatabase("UserPrevilegeHdr", "Id ", iHdrId))
			{
				btnClear_Click(null, null);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			SelectCustomer selectCustomer = new SelectCustomer("UserPrevilege");
			if (selectCustomer.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			UserPrevilegeHdr userPrevilegeHdr = db.UserPrevilegeHdrs(new
			{
				Id = selectCustomer.iId
			}).FirstOrDefault();
			iHdrId = userPrevilegeHdr.Id.toInt32();
			txtName.Text = userPrevilegeHdr.Name;
			previlegeDtls = new List<UserPrevilegeDtl>();
			previlegeDtls = db.UserPrevilegeDtls(new
			{
				FK_UserPrevilegeHdrId = iHdrId
			}).ToList();
			userPrevilages = new List<ExUserPrevilages>();
			userPrevilages = AniHelper.CopyListData<ExUserPrevilages>(previlegeDtls.Cast<object>().ToList());
			foreach (ExUserPrevilages userPrevilage in userPrevilages)
			{
				MenuDetails menuDetails = db.MenuDetails(new
				{
					Id = userPrevilage.FK_MenuDetailsId
				}).FirstOrDefault();
				userPrevilage.MenuId = userPrevilage.FK_MenuDetailsId.toInt32();
				userPrevilage.MenuCaption = menuDetails.MenuCaption;
				userPrevilage.MenuTitle = db.MenuDetails(new
				{
					Id = menuDetails.ParentId
				}).FirstOrDefault().MenuCaption;
			}
			BindDGV();
			btnSave.Text = "Update";
			btnDelete.Enabled = true;
			btnDelete.ForeColor = Color.White;
			txtName.Focus();
		}

		private void cbEnableAll_CheckedChanged(object sender, EventArgs e)
		{
			userPrevilages.ForEach(delegate(ExUserPrevilages x)
			{
				x.Enable = cbEnableAll.Checked;
			});
			dgvDetails.Refresh();
		}

		private void cbSaveAll_CheckedChanged(object sender, EventArgs e)
		{
			userPrevilages.ForEach(delegate(ExUserPrevilages x)
			{
				x.SaveData = cbSaveAll.Checked;
			});
			dgvDetails.Refresh();
		}

		private void cbEditAll_CheckedChanged(object sender, EventArgs e)
		{
			userPrevilages.ForEach(delegate(ExUserPrevilages x)
			{
				x.EditData = cbEditAll.Checked;
			});
			dgvDetails.Refresh();
		}

		private void cbDeleteAll_CheckedChanged(object sender, EventArgs e)
		{
			userPrevilages.ForEach(delegate(ExUserPrevilages x)
			{
				x.DeleteData = cbDeleteAll.Checked;
			});
			dgvDetails.Refresh();
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
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label1 = new System.Windows.Forms.Label();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			btnSearch = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new System.Windows.Forms.DataGridView();
			col_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_MenuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_MenuTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Save = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Edit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			col_Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			cbEnableAll = new System.Windows.Forms.CheckBox();
			cbSaveAll = new System.Windows.Forms.CheckBox();
			cbEditAll = new System.Windows.Forms.CheckBox();
			cbDeleteAll = new System.Windows.Forms.CheckBox();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(661, 0);
			button4.Location = new System.Drawing.Point(-3, 27);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(155, 1);
			button4.TabIndex = 98;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 3);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(147, 24);
			lblHead.TabIndex = 97;
			lblHead.Text = "User Previlege";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Location = new System.Drawing.Point(140, 38);
			txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(309, 16);
			txtName.TabIndex = 96;
			txtName.Value = new decimal(new int[4]);
			txtName.WaterMark = null;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 42);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(113, 13);
			label1.TabIndex = 95;
			label1.Text = "User Previlege Name :";
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(244, 498);
			btnClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 101;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(165, 498);
			btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 100;
			btnDelete.Text = "Delete";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(87, 498);
			btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 99;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
			btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSearch.ForeColor = System.Drawing.Color.White;
			btnSearch.Location = new System.Drawing.Point(322, 498);
			btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new System.Drawing.Size(75, 30);
			btnSearch.TabIndex = 101;
			btnSearch.Text = "Search";
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Location = new System.Drawing.Point(17, 98);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(667, 392);
			panel1.TabIndex = 102;
			dgvDetails.AllowUserToAddRows = false;
			dgvDetails.AllowUserToDeleteRows = false;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_Enable, col_MenuName, col_MenuTitle, col_Save, col_Edit, col_Delete);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.Location = new System.Drawing.Point(10, 10);
			dgvDetails.Name = "dgvDetails";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.RowHeadersWidth = 40;
			dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvDetails.Size = new System.Drawing.Size(643, 372);
			dgvDetails.TabIndex = 0;
			col_Enable.DataPropertyName = "Enable";
			col_Enable.HeaderText = "Enable";
			col_Enable.Name = "col_Enable";
			col_Enable.Width = 60;
			col_MenuName.DataPropertyName = "MenuCaption";
			col_MenuName.HeaderText = "Menu Name";
			col_MenuName.Name = "col_MenuName";
			col_MenuName.Width = 200;
			col_MenuTitle.DataPropertyName = "MenuTitle";
			col_MenuTitle.HeaderText = "Menu Title";
			col_MenuTitle.Name = "col_MenuTitle";
			col_MenuTitle.Width = 200;
			col_Save.DataPropertyName = "SaveData";
			col_Save.HeaderText = "Save";
			col_Save.Name = "col_Save";
			col_Save.Width = 60;
			col_Edit.DataPropertyName = "EditData";
			col_Edit.HeaderText = "Edit";
			col_Edit.Name = "col_Edit";
			col_Edit.Width = 60;
			col_Delete.DataPropertyName = "DeleteData";
			col_Delete.HeaderText = "Delete";
			col_Delete.Name = "col_Delete";
			col_Delete.Width = 60;
			cbEnableAll.AutoSize = true;
			cbEnableAll.Checked = true;
			cbEnableAll.CheckState = System.Windows.Forms.CheckState.Checked;
			cbEnableAll.Location = new System.Drawing.Point(32, 75);
			cbEnableAll.Name = "cbEnableAll";
			cbEnableAll.Size = new System.Drawing.Size(73, 17);
			cbEnableAll.TabIndex = 103;
			cbEnableAll.Text = "Enable All";
			cbEnableAll.UseVisualStyleBackColor = true;
			cbEnableAll.CheckedChanged += new System.EventHandler(cbEnableAll_CheckedChanged);
			cbSaveAll.AutoSize = true;
			cbSaveAll.Checked = true;
			cbSaveAll.CheckState = System.Windows.Forms.CheckState.Checked;
			cbSaveAll.Location = new System.Drawing.Point(482, 75);
			cbSaveAll.Name = "cbSaveAll";
			cbSaveAll.Size = new System.Drawing.Size(51, 17);
			cbSaveAll.TabIndex = 104;
			cbSaveAll.Text = "Save";
			cbSaveAll.UseVisualStyleBackColor = true;
			cbSaveAll.CheckedChanged += new System.EventHandler(cbSaveAll_CheckedChanged);
			cbEditAll.AutoSize = true;
			cbEditAll.Checked = true;
			cbEditAll.CheckState = System.Windows.Forms.CheckState.Checked;
			cbEditAll.Location = new System.Drawing.Point(551, 75);
			cbEditAll.Name = "cbEditAll";
			cbEditAll.Size = new System.Drawing.Size(44, 17);
			cbEditAll.TabIndex = 105;
			cbEditAll.Text = "Edit";
			cbEditAll.UseVisualStyleBackColor = true;
			cbEditAll.CheckedChanged += new System.EventHandler(cbEditAll_CheckedChanged);
			cbDeleteAll.AutoSize = true;
			cbDeleteAll.Checked = true;
			cbDeleteAll.CheckState = System.Windows.Forms.CheckState.Checked;
			cbDeleteAll.Location = new System.Drawing.Point(611, 75);
			cbDeleteAll.Name = "cbDeleteAll";
			cbDeleteAll.Size = new System.Drawing.Size(57, 17);
			cbDeleteAll.TabIndex = 106;
			cbDeleteAll.Text = "Delete";
			cbDeleteAll.UseVisualStyleBackColor = true;
			cbDeleteAll.CheckedChanged += new System.EventHandler(cbDeleteAll_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(696, 540);
			base.Controls.Add(cbDeleteAll);
			base.Controls.Add(cbEditAll);
			base.Controls.Add(cbSaveAll);
			base.Controls.Add(cbEnableAll);
			base.Controls.Add(panel1);
			base.Controls.Add(btnSearch);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(txtName);
			base.Controls.Add(label1);
			base.Name = "UserPrevilegeView";
			base.Load += new System.EventHandler(UserPrevilegeView_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(btnSearch, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(cbEnableAll, 0);
			base.Controls.SetChildIndex(cbSaveAll, 0);
			base.Controls.SetChildIndex(cbEditAll, 0);
			base.Controls.SetChildIndex(cbDeleteAll, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
