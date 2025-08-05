using System;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Windows.Forms;
using AshUsercontrol;

namespace Ashpro
{
	public class MyMsgBox : Form
	{
		private IContainer components = null;

		private Label lblMessage;

		private AshTextBoxNormal txtEmailAddress;

		private Button btnClose;

		private Button btnOK;

		private AshComboBox cmbInstances;

		private Label lblHead;

		public string Passvalue { get; set; }

		public MyMsgBox()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void MyMsgBox_Load(object sender, EventArgs e)
		{
			if (Passvalue == "Server Instance")
			{
				txtEmailAddress.Visible = false;
				cmbInstances.Visible = true;
				cmbInstances.Location = new Point(96, 43);
				lblHead.Text = "Select Server Instance";
				lblMessage.Text = "Server Name:";
				DataTable dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
				foreach (DataRow row in dataSources.Rows)
				{
					cmbInstances.Items.Add(row["ServerName"].ToString() + "\\" + row["InstanceName"].ToString());
				}
				cmbInstances.SelectedIndex = 0;
			}
			else if (Passvalue == "Activation")
			{
				lblMessage.Location = new Point(96, 16);
				lblMessage.Text = "Please check you Email Id and put trial activation code below";
				lblHead.Text = "Activation Code Window";
			}
			else if (Passvalue == "Message")
			{
				lblMessage.Location = new Point(80, 40);
				cmbInstances.Visible = false;
				txtEmailAddress.Visible = false;
				lblMessage.Text = "Are you want to set this as Current Financial Year";
				lblHead.Text = Passvalue;
			}
			else if (Passvalue == "Closing")
			{
				lblMessage.Location = new Point(80, 40);
				cmbInstances.Visible = false;
				txtEmailAddress.Visible = false;
				lblMessage.Text = "Do You want to close the Application?";
				lblHead.Text = Utility.uManufacturer;
			}
			else
			{
				lblHead.Text = Passvalue;
				lblMessage.Text = Passvalue + ":";
			}
		}

		private void MyMsgBox_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Passvalue == "Activation")
			{
				Application.Exit();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (Passvalue == "Activation")
			{
				Application.Exit();
			}
			else if (Passvalue == "Message")
			{
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
			else
			{
				Close();
			}
		}

		private void txtEmailAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Passvalue == "Activation")
			{
				char keyChar = e.KeyChar;
				if (!char.IsDigit(keyChar) && keyChar != '\b')
				{
					e.Handled = true;
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (Passvalue == "Server Instance")
			{
				Passvalue = cmbInstances.Text;
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else if (Passvalue == "Message")
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				Passvalue = txtEmailAddress.Text;
				base.DialogResult = DialogResult.OK;
				Close();
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
			lblMessage = new System.Windows.Forms.Label();
			txtEmailAddress = new AshUsercontrol.AshTextBoxNormal();
			btnClose = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
			cmbInstances = new AshUsercontrol.AshComboBox();
			lblHead = new System.Windows.Forms.Label();
			SuspendLayout();
			lblMessage.AutoSize = true;
			lblMessage.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblMessage.Location = new System.Drawing.Point(13, 66);
			lblMessage.Name = "lblMessage";
			lblMessage.Size = new System.Drawing.Size(109, 16);
			lblMessage.TabIndex = 0;
			lblMessage.Text = "Email Address :";
			txtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtEmailAddress.Data = 0;
			txtEmailAddress.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtEmailAddress.Format = null;
			txtEmailAddress.isAllowNegative = false;
			txtEmailAddress.isAllowSpecialChar = false;
			txtEmailAddress.isNumberOnly = false;
			txtEmailAddress.isNumeric = false;
			txtEmailAddress.Location = new System.Drawing.Point(122, 69);
			txtEmailAddress.Margin = new System.Windows.Forms.Padding(4);
			txtEmailAddress.Name = "txtEmailAddress";
			txtEmailAddress.Size = new System.Drawing.Size(369, 16);
			txtEmailAddress.TabIndex = 1;
			txtEmailAddress.Value = new decimal(new int[4]);
			txtEmailAddress.WaterMark = null;
			txtEmailAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtEmailAddress_KeyPress);
			btnClose.BackColor = System.Drawing.Color.DodgerBlue;
			btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnClose.ForeColor = System.Drawing.Color.White;
			btnClose.Location = new System.Drawing.Point(388, 97);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(77, 30);
			btnClose.TabIndex = 27;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += new System.EventHandler(btnClose_Click);
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.Location = new System.Drawing.Point(289, 97);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(77, 30);
			btnOK.TabIndex = 26;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			cmbInstances.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbInstances.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbInstances.FormattingEnabled = true;
			cmbInstances.Location = new System.Drawing.Point(122, 38);
			cmbInstances.Name = "cmbInstances";
			cmbInstances.Size = new System.Drawing.Size(369, 24);
			cmbInstances.TabIndex = 28;
			cmbInstances.Visible = false;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(9, 5);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(100, 24);
			lblHead.TabIndex = 95;
			lblHead.Text = "Activation";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(510, 136);
			base.Controls.Add(lblHead);
			base.Controls.Add(cmbInstances);
			base.Controls.Add(btnClose);
			base.Controls.Add(btnOK);
			base.Controls.Add(txtEmailAddress);
			base.Controls.Add(lblMessage);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.Name = "MyMsgBox";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "MyMsgBox";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MyMsgBox_FormClosing);
			base.Load += new System.EventHandler(MyMsgBox_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
