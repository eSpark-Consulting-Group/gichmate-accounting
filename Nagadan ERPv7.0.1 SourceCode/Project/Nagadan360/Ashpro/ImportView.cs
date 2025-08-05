using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class ImportView : BaseForm
	{
		private OpenFileDialog openFile;

		private DBContext db = new DBContext();

		private string sFileName;

		private int iIndex = 0;

		private IContainer components = null;

		private Label lblTime;

		private AshTextBoxNormal txtPath;

		private Label lblHead;

		private Button btnFormat;

		private Button btnGnrt;

		private Button btnBrouse;

		private Label label2;

		private AshComboBox cmbTables;

		private Label label1;

		public ImportView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void ImportMethod()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable = AniHelper.WorksheetToTable(sFileName);
				if (dataTable.Rows.Count == 0)
				{
					return;
				}
				switch (iIndex)
				{
				case 0:
					AccountCreatMethod(dataTable, "SUPPLIERS/SUNDRY CREDITORS");
					return;
				case 1:
					AccountCreatMethod(dataTable, "CUSTOMER/SUNDRY DEBTORS");
					return;
				case 2:
					AccountCreatMethodex(dataTable, "INDIRECT EXPENSES");
					return;
				case 3:
				{
					List<ExProductImport> list = new List<ExProductImport>();
					list = AniHelper.DataTableToList<ExProductImport>(dataTable);
					if (list.Count <= 0)
					{
						return;
					}
					foreach (ExProductImport item2 in list)
					{
						item2.UserId = Settings.Default.intLogOnId;
						item2.BranchId = Settings.Default.iBranchId;
					}
					AshproCommon.InsertMethod_SP(list.Cast<object>().ToList(), "spImportproduct");
					return;
				}
				}
				List<ProductGroupEntity> list2 = new List<ProductGroupEntity>();
				list2 = AniHelper.DataTableToList<ProductGroupEntity>(dataTable);
				int num = list2.Count;
				foreach (ProductGroupEntity item in list2.ToList())
				{
					item.PGroupName = item.PGroupName.ToUpper();
					item.Under = item.Under ?? "GENERAL";
					item.Remarks = string.Empty;
					item.ListOrder = num;
					item.PurchDis = 0m;
					item.SalesDis = 0m;
					item.SalesInsentive = 0m;
					if (db.ProductGroups().Any((ProductGroupEntity x) => x.PGroupName == item.PGroupName))
					{
						list2.Remove(item);
					}
					else
					{
						num--;
					}
				}
				AshproCommon.InsertToDatabase(list2.Cast<object>().ToList(), "ProductGroup");
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void AccountCreatMethodex(DataTable dt, string sGroupName)
		{
			int value = db.AccountGroup().FirstOrDefault((AccountGroupEntity x) => x.GroupName == sGroupName).AGroupId.ToInt32();
			List<AccountEntity> list = new List<AccountEntity>();
			list = AniHelper.DataTableToList<AccountEntity>(dt);
			foreach (AccountEntity item in list)
			{
				item.AGroupId = value;
				if (db.Accounts().Any((AccountEntity x) => x.AccountName == item.AccountName))
				{
					list.Remove(item);
				}
			}
			if (!AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "Account"))
			{
				return;
			}
			foreach (AccountEntity item2 in list)
			{
				if (item2.OpeningBalance > 0m)
				{
					AniHelper.OpeningBalanceVoucherMethod(item2.OpeningBalance, item2.DebitOrCridt, db, item2.AccountName, DateTime.Now);
				}
			}
		}

		private void AccountCreatMethod(DataTable dt, string sGroupName)
		{
			int value = db.AccountGroup().FirstOrDefault((AccountGroupEntity x) => x.GroupName == sGroupName).AGroupId.ToInt32();
			List<AccountEntity> list = new List<AccountEntity>();
			List<ExAccountEntity> list2 = new List<ExAccountEntity>();
			list2 = AniHelper.DataTableToList<ExAccountEntity>(dt);
			foreach (ExAccountEntity item in list2)
			{
				item.AGroupId = value;
				item.TinNo = item.TaxId;
				if (db.Accounts().Any((AccountEntity x) => x.AccountName == item.AccountName))
				{
					list.Remove(item);
				}
			}
			list = AniHelper.CopyListData<AccountEntity>(list2.Cast<object>().ToList());
			if (!AshproCommon.InsertToDatabase(list.Cast<object>().ToList(), "Account"))
			{
				return;
			}
			foreach (AccountEntity item2 in list)
			{
				if (item2.OpeningBalance > 0m)
				{
					AniHelper.OpeningBalanceVoucherMethod(item2.OpeningBalance, item2.DebitOrCridt, db, item2.AccountName, DateTime.Now);
				}
			}
		}

		private void btnBrouse_Click(object sender, EventArgs e)
		{
			openFile = new OpenFileDialog();
			if (openFile.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = openFile.FileName;
			}
		}

		private void btnFormat_Click(object sender, EventArgs e)
		{
			try
			{
				sFileName = cmbTables.Text + ".xlsx";
				DataTable dataTable = new DataTable();
				switch (cmbTables.SelectedIndex)
				{
				case 0:
				{
					string[] headers = new string[7] { "AccountName", "GroupName", "TaxId", "OpeningBalance", "DebitOrCridt", "isActive", "IsBuiltIn" };
					dataTable = AniHelper.GetDataTable(headers);
					break;
				}
				case 1:
				{
					string[] headers = new string[7] { "AccountName", "GroupName", "TaxId", "OpeningBalance", "DebitOrCridt", "isActive", "IsBuiltIn" };
					dataTable = AniHelper.GetDataTable(headers);
					break;
				}
				case 2:
				{
					string[] headers = new string[6] { "AccountName", "GroupName", "OpeningBalance", "DebitOrCridt", "isActive", "IsBuiltIn" };
					dataTable = AniHelper.GetDataTable(headers);
					break;
				}
				case 3:
				{
					string[] headers = new string[27]
					{
						"ProductCode", "ProductName", "Specification", "GroupName", "UnitName", "BrandName", "PurchasePrice", "SellingPrice", "MRP", "OpeningStock",
						"ProductType", "Barcode", "LocalName", "TCategoryName", "isInclusive", "FUnitName", "FProductName", "FQuantity", "FRate", "FBarcode",
						"FLocalName", "SUnitName", "SProductName", "SQuantity", "SRate", "SBarcode", "SLocalName"
					};
					dataTable = AniHelper.GetDataTable(headers);
					break;
				}
				default:
				{
					string[] headers = new string[2] { "PGroupName", "Under" };
					dataTable = AniHelper.GetDataTable(headers);
					break;
				}
				}
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(dataTable);
				AniHelper.DataSetToExcel(dataSet, sFileName, isExport: false);
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
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
				sFileName = txtPath.Text;
				Task task = new Task(ImportMethod);
				task.Start();
				await task;
				txtPath.Text = string.Empty;
				frm.Visible = false;
				myStopWatch.Stop();
				lblTime.Text = "Time Taken : " + myStopWatch.Elapsed.Hours + " : " + myStopWatch.Elapsed.Minutes + " : " + myStopWatch.Elapsed.Seconds + " : " + myStopWatch.Elapsed.Milliseconds;
			}
		}

		private void ImportView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.O && e.Alt)
			{
				e.SuppressKeyPress = true;
				btnBrouse_Click(null, null);
			}
			else if (e.KeyCode == Keys.I && e.Alt)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportView));
            this.lblTime = new System.Windows.Forms.Label();
            this.txtPath = new AshUsercontrol.AshTextBoxNormal();
            this.lblHead = new System.Windows.Forms.Label();
            this.btnFormat = new System.Windows.Forms.Button();
            this.btnGnrt = new System.Windows.Forms.Button();
            this.btnBrouse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTables = new AshUsercontrol.AshComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
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
            // lblHead
            // 
            resources.ApplyResources(this.lblHead, "lblHead");
            this.lblHead.Name = "lblHead";
            // 
            // btnFormat
            // 
            resources.ApplyResources(this.btnFormat, "btnFormat");
            this.btnFormat.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnFormat.ForeColor = System.Drawing.Color.White;
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.TabStop = false;
            this.btnFormat.UseVisualStyleBackColor = false;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
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
            // ImportView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.btnFormat);
            this.Controls.Add(this.btnGnrt);
            this.Controls.Add(this.btnBrouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.label1);
            this.Name = "ImportView";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImportView_KeyDown);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbTables, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnBrouse, 0);
            this.Controls.SetChildIndex(this.btnGnrt, 0);
            this.Controls.SetChildIndex(this.btnFormat, 0);
            this.Controls.SetChildIndex(this.lblHead, 0);
            this.Controls.SetChildIndex(this.txtPath, 0);
            this.Controls.SetChildIndex(this.lblTime, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
