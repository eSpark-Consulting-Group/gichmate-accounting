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
	public class UserManagement : BaseForm
	{
		private int? iUserId;

		private DBContext db;

		private ExUser exUser;

		private IContainer components = null;

		private Label label1;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshTextBoxNormal txtPassword;

		private Label label3;

		private AshComboBox cmbPrevilege;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSave;

		private Button button4;

		private Label lblHead;

		private BindingSource bdsUser;

		private DataGridView dgDetails;

		private DataGridViewTextBoxColumn col_User;

		private DataGridViewTextBoxColumn col_Previlege;

		public UserManagement()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void DataLoadMethod()
		{
			try
			{
				List<User> list = new List<User>();
				list = (from x in db.Users()
					where x.UserName.ToUpper() != "ADMIN"
					select x).ToList();
				List<ExUser> list2 = new List<ExUser>();
				list2 = AniHelper.CopyListData<ExUser>(list.Cast<object>().ToList());
				foreach (ExUser item in list2)
				{
					if (item.PrevilageId.HasValue)
					{
						int? previlageId = item.PrevilageId;
						if (!((previlageId.GetValueOrDefault() == 0) & previlageId.HasValue))
						{
							item.PrevilegeName = db.UserPrevilegeHdrs(new
							{
								Id = item.PrevilageId
							}).FirstOrDefault().Name;
						}
					}
				}
				dgDetails.AutoGenerateColumns = false;
				bdsUser.DataSource = null;
				bdsUser.DataSource = list2;
				dgDetails.DataSource = bdsUser;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void UserManagement_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			if (db.UserPrevilegeHdrs().ToList().Count == 0)
			{
				Messages.ErrorMessage("Firstly Create an User Role. And try again later.");
				return;
			}
			AniHelper.FillPrevilege(cmbPrevilege);
			DataLoadMethod();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			AniHelper.ClearMethod(this);
			DataLoadMethod();
			iUserId = null;
			btnDelete.Enabled = false;
			txtName.Focus();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (Messages.DeleteConfirmationMessage())
				{
					AshproCommon.DeleteFromDatabase("[User]", "UserId", iUserId.ToInt32());
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
			btnClear_Click(null, null);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
				}
				User user = new User
				{
					PrevilageId = cmbPrevilege.SelectedValue.ToInt32(),
					UserName = txtName.Text,
					PassWord = txtPassword.Text,
					UserId = iUserId
				};
				if (!iUserId.HasValue)
				{
					user.UserId = null;
					if (AshproCommon.InsertToDatabaseObj(user, "[User]"))
					{
						Messages.SavedMessage();
					}
				}
				else if (AshproCommon.UpdateToDatabaseObj(user, "[User]", "UserId", iUserId.ToInt32()))
				{
					Messages.UpdateMessage();
				}
				btnClear_Click(null, null);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					exUser = (ExUser)dgDetails.CurrentRow.DataBoundItem;
					txtName.Text = exUser.UserName;
					cmbPrevilege.SelectedValue = exUser.PrevilageId ?? 1;
					iUserId = exUser.UserId;
					txtPassword.Text = exUser.PassWord;
					btnDelete.Enabled = true;
					btnDelete.ForeColor = Color.White;
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
			label1 = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			txtPassword = new AshUsercontrol.AshTextBoxNormal();
			label3 = new System.Windows.Forms.Label();
			cmbPrevilege = new AshUsercontrol.AshComboBox();
			btnClear = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			bdsUser = new System.Windows.Forms.BindingSource(components);
			dgDetails = new System.Windows.Forms.DataGridView();
			col_User = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Previlege = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)bdsUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(654, 0);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label1.Location = new System.Drawing.Point(11, 47);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 16);
			label1.TabIndex = 0;
			label1.Text = "User Name :";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Location = new System.Drawing.Point(101, 50);
			txtName.Margin = new System.Windows.Forms.Padding(4);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(246, 16);
			txtName.TabIndex = 1;
			txtName.Value = new decimal(new int[4]);
			txtName.WaterMark = null;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label2.Location = new System.Drawing.Point(19, 71);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(81, 16);
			label2.TabIndex = 2;
			label2.Text = "Password :";
			txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtPassword.Data = 0;
			txtPassword.EnterColor = System.Drawing.Color.LightPink;
			txtPassword.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			txtPassword.Format = null;
			txtPassword.isAllowNegative = false;
			txtPassword.isAllowSpecialChar = false;
			txtPassword.isNumberOnly = false;
			txtPassword.isNumeric = false;
			txtPassword.LeaveColor = System.Drawing.Color.White;
			txtPassword.Location = new System.Drawing.Point(101, 74);
			txtPassword.Margin = new System.Windows.Forms.Padding(4);
			txtPassword.Name = "txtPassword";
			txtPassword.Size = new System.Drawing.Size(246, 16);
			txtPassword.TabIndex = 3;
			txtPassword.Value = new decimal(new int[4]);
			txtPassword.WaterMark = null;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label3.Location = new System.Drawing.Point(22, 99);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(78, 16);
			label3.TabIndex = 4;
			label3.Text = "Previlege :";
			cmbPrevilege.EnterColor = System.Drawing.Color.LightPink;
			cmbPrevilege.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbPrevilege.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			cmbPrevilege.FormattingEnabled = true;
			cmbPrevilege.LeaveColor = System.Drawing.Color.White;
			cmbPrevilege.Location = new System.Drawing.Point(101, 95);
			cmbPrevilege.Name = "cmbPrevilege";
			cmbPrevilege.Size = new System.Drawing.Size(246, 24);
			cmbPrevilege.TabIndex = 5;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Location = new System.Drawing.Point(259, 147);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 30);
			btnClear.TabIndex = 8;
			btnClear.Text = "Clear";
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnDelete.ForeColor = System.Drawing.Color.Black;
			btnDelete.Location = new System.Drawing.Point(182, 147);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 30);
			btnDelete.TabIndex = 7;
			btnDelete.Text = "Delete";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(105, 147);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 6;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			button4.Location = new System.Drawing.Point(2, 29);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(141, 1);
			button4.TabIndex = 12;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(8, 5);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(137, 24);
			lblHead.TabIndex = 11;
			lblHead.Text = "User Creation";
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(col_User, col_Previlege);
			dgDetails.Location = new System.Drawing.Point(363, 41);
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			dgDetails.Size = new System.Drawing.Size(313, 138);
			dgDetails.TabIndex = 13;
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgDetails_CellContentClick);
			col_User.DataPropertyName = "UserName";
			col_User.HeaderText = "User Name";
			col_User.Name = "col_User";
			col_User.ReadOnly = true;
			col_User.Width = 180;
			col_Previlege.DataPropertyName = "PrevilegeName";
			col_Previlege.HeaderText = "Previlege";
			col_Previlege.Name = "col_Previlege";
			col_Previlege.ReadOnly = true;
			col_Previlege.Width = 130;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(690, 189);
			base.Controls.Add(dgDetails);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnClear);
			base.Controls.Add(btnDelete);
			base.Controls.Add(btnSave);
			base.Controls.Add(cmbPrevilege);
			base.Controls.Add(label3);
			base.Controls.Add(txtPassword);
			base.Controls.Add(label2);
			base.Controls.Add(txtName);
			base.Controls.Add(label1);
			base.MaximizeBox = false;
			base.Name = "UserManagement";
			base.Load += new System.EventHandler(UserManagement_Load);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(txtPassword, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbPrevilege, 0);
			base.Controls.SetChildIndex(btnSave, 0);
			base.Controls.SetChildIndex(btnDelete, 0);
			base.Controls.SetChildIndex(btnClear, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			((System.ComponentModel.ISupportInitialize)bdsUser).EndInit();
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
