using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ExportView : BaseForm
	{
		private FolderBrowserDialog dlg;

		private DBContext db = new DBContext();

		private string sFileName;

		private int iIndex = 0;

		private IContainer components = null;

		private AshTextBoxNormal txtPath;

		private Label label3;

		private Label lblStatus;

		private Button btnGnrt;

		private Button btnBrouse;

		private Label label2;

		private AshComboBox cmbTables;

		private Label label1;

		private Label lblHead;

		private Label lblTime;

		public ExportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ExportMethod()
		{
			string empty = string.Empty;
			empty = iIndex switch
			{
				0 => "Select AccountName,AccountGroup.GroupName,TinNo,OpeningBalance,DebitOrCridt,isActive,IsBuiltIn From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='CUSTOMER/SUNDRY DEBTORS'", 
				1 => "Select AccountName,AccountGroup.GroupName,TinNo,OpeningBalance,DebitOrCridt,isActive,IsBuiltIn From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='SUPPLIERS/SUNDRY CREDITORS'", 
				2 => "Select AccountName,AccountGroup.GroupName,OpeningBalance,DebitOrCridt,isActive,IsBuiltIn From Account Join AccountGroup on Account.AGroupId=AccountGroup.AGroupId Where AccountGroup.GroupName='INDIRECT EXPENSES'", 
				3 => "Select ProductCode,ProductName,Specification,ProductGroup.PGroupName as GroupName,Unit.UnitName,Brand.BrandName,PurchasePrice,SellingPrice,MRP,OpeningStock,ProductType,Barcode,LocalName,TCategoryName,isInclusive From Product Left Join ProductGroup on Product.PGroupId = ProductGroup.PGroupId Left Join Unit on Product.UnitId=Unit.UnitId Left Join Brand on Product.BrandId=Brand.BrandId Left Join TaxCategory on Product.TCategoryId= TaxCategory.TCategoryId", 
				_ => "Select PGroupName,Under From ProductGroup", 
			};
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(AshproCommon.GetDataTable(empty));
			AniHelper.DataSetToExcel(dataSet, sFileName);
		}

		private void btnBrouse_Click(object sender, EventArgs e)
		{
			dlg = new FolderBrowserDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = dlg.SelectedPath;
			}
		}

		private async void btnGnrt_Click(object sender, EventArgs e)
		{
			Stopwatch myStopWatch = new Stopwatch();
			myStopWatch.Start();
			AshUsercontrol.ProgressBar frm = new AshUsercontrol.ProgressBar(this);
			if (cmbTables.Text != string.Empty && txtPath.Text != string.Empty)
			{
				iIndex = cmbTables.SelectedIndex;
				sFileName = txtPath.Text + "\\" + cmbTables.Text + ".xlsx";
				Task task = new Task(ExportMethod);
				task.Start();
				await task;
				txtPath.Text = string.Empty;
				frm.Visible = false;
				myStopWatch.Stop();
				lblTime.Text = "Time Taken : " + myStopWatch.Elapsed.Hours + " : " + myStopWatch.Elapsed.Minutes + " : " + myStopWatch.Elapsed.Seconds + " : " + myStopWatch.Elapsed.Milliseconds;
			}
		}

		private void ExportView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.O && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBrouse_Click(null, null);
			}
			else if (e.KeyCode == Keys.E && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnGnrt.PerformClick();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportView));
            this.txtPath = new AshUsercontrol.AshTextBoxNormal();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnGnrt = new System.Windows.Forms.Button();
            this.btnBrouse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTables = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPath.Data = 0;
            this.txtPath.EnterColor = System.Drawing.Color.LightPink;
            this.txtPath.Format = null;
            this.txtPath.isAllowNegative = false;
            this.txtPath.isAllowSpecialChar = false;
            this.txtPath.isNumberOnly = false;
            this.txtPath.isNumeric = false;
            this.txtPath.LeaveColor = System.Drawing.Color.White;
            this.txtPath.Name = "txtPath";
            this.txtPath.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // btnGnrt
            // 
            resources.ApplyResources(this.btnGnrt, "btnGnrt");
            this.btnGnrt.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGnrt.ForeColor = System.Drawing.Color.White;
            this.btnGnrt.Name = "btnGnrt";
            this.btnGnrt.TabStop = false;
            this.btnGnrt.UseVisualStyleBackColor = false;
            this.btnGnrt.Click += new System.EventHandler(this.btnGnrt_Click);
            // 
            // btnBrouse
            // 
            resources.ApplyResources(this.btnBrouse, "btnBrouse");
            this.btnBrouse.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBrouse.ForeColor = System.Drawing.Color.White;
            this.btnBrouse.Name = "btnBrouse";
            this.btnBrouse.TabStop = false;
            this.btnBrouse.UseVisualStyleBackColor = false;
            this.btnBrouse.Click += new System.EventHandler(this.btnBrouse_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbTables
            // 
            resources.ApplyResources(this.cmbTables, "cmbTables");
            this.cmbTables.EnterColor = System.Drawing.Color.LightPink;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Items.AddRange(new object[] {
            resources.GetString("cmbTables.Items"),
            resources.GetString("cmbTables.Items1"),
            resources.GetString("cmbTables.Items2"),
            resources.GetString("cmbTables.Items3"),
            resources.GetString("cmbTables.Items4")});
            this.cmbTables.LeaveColor = System.Drawing.Color.White;
            this.cmbTables.Name = "cmbTables";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // ExportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnGnrt);
            this.Controls.Add(this.btnBrouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.label1);
            this.Name = "ExportView";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExportView_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbTables, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnBrouse, 0);
            this.Controls.SetChildIndex(this.btnGnrt, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtPath, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.lblTime, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
