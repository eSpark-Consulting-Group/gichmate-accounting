using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ResetDatabase : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label lblHead;

		private Button btnReset;

		private Button btnTransaction;

		private Button btnTranExtended;

		private Button btnOppStockReset;

		public ResetDatabase()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		public void ResetDB(int itype)
		{
			try
			{
                if (!Messages.DialogMessage("This will remove your data. Are you sure to reset the database?"))
                {
                    return;
                }

				if (itype == 1)
				{
					FullResetMethod();
					return;
				}
				AshproCommon.DBExicutionMethod_SP("spDatabaseReset", new
				{
					ResetType = itype
				});
				Messages.InformationMessage("Completed Successfully.");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		public static void FullResetMethod()
		{
			AshproCommon.DBExicutionMethod_SP("spDatabaseReset");
			if (DBHelper.CreatTables(Settings.Default.strConnetionString))
			{
				UpgradeHelper upgradeHelper = new UpgradeHelper();
				upgradeHelper.isDbReset = true;
				if (upgradeHelper.ShowDialog() == DialogResult.OK)
				{
					Messages.InformationMessage("Completed Successfully.");
					Application.Restart();
				}
			}
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnReset.PerformClick();
			}
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			ResetDB(button.Tag.ToInt32());
			if (button.Tag.ToInt32() != 4)
			{
				Utility.uSerialNumber = 1;
				AniHelper.SerialNumberMethod(1);
				Settings.Default.iTockenNo = 1;
				Settings.Default.Save();
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
			lblHead = new System.Windows.Forms.Label();
			btnReset = new System.Windows.Forms.Button();
			btnTransaction = new System.Windows.Forms.Button();
			btnTranExtended = new System.Windows.Forms.Button();
			btnOppStockReset = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(546, 1);
			btnEnd.TabIndex = 3;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 3);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(155, 24);
			lblHead.TabIndex = 4;
			lblHead.Text = "Database Reset";
			btnReset.BackColor = System.Drawing.Color.DodgerBlue;
			btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnReset.ForeColor = System.Drawing.Color.White;
			btnReset.Location = new System.Drawing.Point(44, 59);
			btnReset.Name = "btnReset";
			btnReset.Size = new System.Drawing.Size(145, 30);
			btnReset.TabIndex = 2;
			btnReset.TabStop = false;
			btnReset.Tag = "1";
			btnReset.Text = "Full Reset";
			btnReset.UseVisualStyleBackColor = false;
			btnReset.Click += new System.EventHandler(btnReset_Click);
			btnTransaction.BackColor = System.Drawing.Color.DodgerBlue;
			btnTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnTransaction.ForeColor = System.Drawing.Color.White;
			btnTransaction.Location = new System.Drawing.Point(195, 59);
			btnTransaction.Name = "btnTransaction";
			btnTransaction.Size = new System.Drawing.Size(145, 30);
			btnTransaction.TabIndex = 2;
			btnTransaction.TabStop = false;
			btnTransaction.Tag = "2";
			btnTransaction.Text = "Transaction Only";
			btnTransaction.UseVisualStyleBackColor = false;
			btnTransaction.Click += new System.EventHandler(btnReset_Click);
			btnTranExtended.BackColor = System.Drawing.Color.DodgerBlue;
			btnTranExtended.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnTranExtended.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnTranExtended.ForeColor = System.Drawing.Color.White;
			btnTranExtended.Location = new System.Drawing.Point(130, 95);
			btnTranExtended.Name = "btnTranExtended";
			btnTranExtended.Size = new System.Drawing.Size(296, 30);
			btnTranExtended.TabIndex = 2;
			btnTranExtended.TabStop = false;
			btnTranExtended.Tag = "3";
			btnTranExtended.Text = "Transaction With Customers and Suppliers";
			btnTranExtended.UseVisualStyleBackColor = false;
			btnTranExtended.Click += new System.EventHandler(btnReset_Click);
			btnOppStockReset.BackColor = System.Drawing.Color.DodgerBlue;
			btnOppStockReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOppStockReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnOppStockReset.ForeColor = System.Drawing.Color.White;
			btnOppStockReset.Location = new System.Drawing.Point(346, 59);
			btnOppStockReset.Name = "btnOppStockReset";
			btnOppStockReset.Size = new System.Drawing.Size(173, 30);
			btnOppStockReset.TabIndex = 2;
			btnOppStockReset.TabStop = false;
			btnOppStockReset.Tag = "4";
			btnOppStockReset.Text = "Reset Oppening Stock";
			btnOppStockReset.UseVisualStyleBackColor = false;
			btnOppStockReset.Click += new System.EventHandler(btnReset_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(582, 145);
			base.Controls.Add(lblHead);
			base.Controls.Add(btnTranExtended);
			base.Controls.Add(btnOppStockReset);
			base.Controls.Add(btnTransaction);
			base.Controls.Add(btnReset);
			base.MaximizeBox = false;
			base.Name = "ResetDatabase";
			base.Controls.SetChildIndex(btnReset, 0);
			base.Controls.SetChildIndex(btnTransaction, 0);
			base.Controls.SetChildIndex(btnOppStockReset, 0);
			base.Controls.SetChildIndex(btnTranExtended, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
