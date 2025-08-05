using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;

namespace Ashpro
{
	public class SalesView : BaseForm
	{
		private int intDGVRowsCount;

		private System.Windows.Forms.Timer timer;

		private string strPartyAccountId;

		private string strPartyName;

		private string strSumDisAmnt;

		private string strSumNet;

		private string strSalesInvId;

		private string strStafId;

		private string strSMan;

		private string strRouteId;

		private string strRoute;

		private string Query;

		private DataGridViewRow row;

		private Calculator calcu;

		private ShowProduct showProduct;

		private SalesEstimateView salesEstimate;

		private SalesInvoiceView salesInvoice;

		private int? iMasterID;

		private IContainer components = null;

		private RadioButton rdbShowAll;

		private RadioButton rdbCashOnly;

		private Button btnShowProduct;

		private Panel panel1;

		private DataGridView dgDetails;

		private CheckBox cbRefresh;

		private Panel panel2;

		private Panel panel3;

		private AshTextBoxNormal txtSalesType;

		private AshTextBoxNormal txtParty;

		private Label label3;

		private AshTextBoxNormal txtVoucherNo;

		private Label label2;

		private AshTextBoxNormal txtAdditionalDiscount;

		private Label label9;

		private AshTextBoxNormal txtAditional;

		private AshTextBoxNormal txtBalance;

		private AshTextBoxNormal txtCashReceived;

		private Label label12;

		private AshTextBoxNormal txtGrandTotal;

		private Label label11;

		private AshTextBoxNormal txtBillDiscount;

		private Label label10;

		private Label label7;

		private Label label8;

		private AshTextBoxNormal txtFreight;

		private Label label6;

		private AshTextBoxNormal txtNetTotal;

		private Label label5;

		private AshTextBoxNormal txtRoute;

		private Label label13;

		private AshTextBoxNormal txtSalesman;

		private Label label4;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem showProductToolStripMenuItem;

		private ToolStripMenuItem focusListToolStripMenuItem;

		private ToolStripMenuItem focusVchrNoTextToolStripMenuItem;

		private ToolStripMenuItem autoRefreshToolStripMenuItem;

		private ToolStripMenuItem focusDatePickerToolStripMenuItem;

		private ToolStripMenuItem focusRadioButtonToolStripMenuItem;

		private ToolStripMenuItem closeApplicationToolStripMenuItem;

		private ToolStripMenuItem calculatorToolStripMenuItem;

		private ToolTip toolTip1;

		private Label lblHeader;

		private AshDateTimePicker dtmTo;

		private AshDateTimePicker dtmFrom;

		private Label label14;

		private Label label15;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn col_GrandTotal;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn col_Tax;

		private DataGridViewTextBoxColumn col_AddAmnt;

		private DataGridViewTextBoxColumn col_Freight;

		private DataGridViewTextBoxColumn col_Discount;

		private DataGridViewTextBoxColumn col_AddDiscount;

		private DataGridViewTextBoxColumn col_Net;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn col_SalesId;

		private DataGridViewTextBoxColumn col_Cash;

		private DataGridViewTextBoxColumn col_Card;

		private DataGridViewTextBoxColumn col_Credit;

		public SalesView()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			txtNetTotal.Format = CommonSettings.strCurencyFormat;
			txtAditional.Format = CommonSettings.strCurencyFormat;
			txtFreight.Format = CommonSettings.strCurencyFormat;
			txtBillDiscount.Format = CommonSettings.strCurencyFormat;
			txtAdditionalDiscount.Format = CommonSettings.strCurencyFormat;
			txtCashReceived.Format = CommonSettings.strCurencyFormat;
			txtGrandTotal.Format = CommonSettings.strCurencyFormat;
			txtBalance.Format = CommonSettings.strCurencyFormat;
			col_AddAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_AddDiscount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Freight.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GrandTotal.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Net.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Tax.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
		}

		private void DGVLoadMethod()
		{
			if (dgDetails.DataSource != null)
			{
				dgDetails.DataSource = null;
			}
			else
			{
				dgDetails.Rows.Clear();
			}
			try
			{
				if (rdbShowAll.Checked)
				{
					Query = "SELECT * FROM SalesEstimateMaster where SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "'";
				}
				else
				{
					Query = "SELECT * FROM SalesEstimateMaster Join Account on SalesEstimateMaster.PartyAccountId=Account.AccountId where SalesDate between '" + AniHelper.GetDate(dtmFrom.Value) + "' and '" + AniHelper.GetDate(dtmTo.Value) + "' and AccountName='CASH ACCOUNT'";
				}
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					intDGVRowsCount = dgDetails.Rows.Count + 1;
					strPartyAccountId = row["PartyAccountId"].ToString();
					AshproCommon.IdFindMethod("Account", "AccountName", "AccountId", strPartyAccountId, out strPartyName);
					strSalesInvId = row["SalesInvId"].ToString();
					DisAmntAndNetTotalFindMethod();
					strStafId = row["StafId"].ToString();
					AshproCommon.IdFindMethod("EmployeeDetails", "EmpName", "EmpID", strStafId, out strSMan);
					strRouteId = row["RouteId"].ToString();
					AshproCommon.IdFindMethod("Root", "RootName", "RootId", strRouteId, out strRoute);
					dgDetails.Rows.Add(intDGVRowsCount, row["SalesInvNo"], row["InvType"], row["GrandDiscount"], strPartyName, "0.00", row["AditionalAmount"], row["Frieght"], strSumDisAmnt, row["BillDiscount"], strSumNet, strSMan, strRoute, row["SalesInvId"], row["CashPayment"], row["CardPayment"], row["Credit"]);
				}
				if (dgDetails.Rows.Count > 0)
				{
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[0];
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DisAmntAndNetTotalFindMethod()
		{
			try
			{
				Query = "SELECT SUM(DiscAmnt) as disTotal,SUM(NetAmnt) as netTotal FROM SalesEstimateSettings where SalesInvId='" + strSalesInvId + "'";
				DataTable dataTable = new DataTable();
				dataTable = AshproCommon.GetDataTable(Query);
				foreach (DataRow row in dataTable.Rows)
				{
					if (row[0] != DBNull.Value)
					{
						strSumDisAmnt = row[0].ToString();
					}
					else
					{
						strSumDisAmnt = "0";
					}
					if (row[1] != DBNull.Value)
					{
						strSumNet = row[1].ToString();
					}
					else
					{
						strSumNet = "0";
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void CalculatorMethod()
		{
			if (calcu == null)
			{
				calcu = new Calculator();
				calcu.MdiParent = base.MdiParent;
				calcu.FormClosing += Calcu_FormClosing;
				calcu.Show();
			}
			else
			{
				calcu.Activate();
				calcu.BringToFront();
			}
		}

		private void SalesView_Load(object sender, EventArgs e)
		{
			dtmFrom.Value = DateTime.Now.Date;
			DGVLoadMethod();
			timer = new System.Windows.Forms.Timer();
			timer.Interval = 10000;
			timer.Tick += timer_Tick;
			timer.Start();
		}

		private void SalesView_FormClosing(object sender, FormClosingEventArgs e)
		{
			timer.Stop();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			dtmTo.Value = DateTime.Now;
			if (cbRefresh.Checked)
			{
				DGVLoadMethod();
			}
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			DGVLoadMethod();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			DGVLoadMethod();
		}

		private void SalesView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				e.SuppressKeyPress = true;
				ShowProductMethod();
			}
			else if (e.KeyCode == Keys.C && e.Alt)
			{
				e.SuppressKeyPress = true;
				CalculatorMethod();
			}
			else if (e.KeyCode == Keys.T && e.Alt)
			{
				e.SuppressKeyPress = true;
				txtVoucherNo.Focus();
				txtVoucherNo.SelectAll();
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				e.SuppressKeyPress = true;
				dtmFrom.Focus();
			}
			else if (e.KeyCode == Keys.R && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (cbRefresh.Checked)
				{
					cbRefresh.Checked = false;
				}
				else
				{
					cbRefresh.Checked = true;
				}
			}
			else if (e.KeyCode == Keys.B && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (rdbShowAll.Checked)
				{
					rdbShowAll.Checked = false;
				}
				else
				{
					rdbShowAll.Checked = true;
				}
			}
			else if (e.KeyCode == Keys.V && e.Alt)
			{
				e.SuppressKeyPress = true;
				if (dgDetails.Rows.Count > 0)
				{
					dgDetails.Focus();
					dgDetails.CurrentCell = dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[1];
				}
			}
			else
			{
				e.SuppressKeyPress = false;
			}
		}

		private void Calcu_FormClosing(object sender, FormClosingEventArgs e)
		{
			calcu = null;
		}

		private void cbRefresh_CheckedChanged(object sender, EventArgs e)
		{
			if (cbRefresh.Checked)
			{
				timer.Start();
			}
			else
			{
				timer.Stop();
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				row = dgDetails.Rows[e.RowIndex];
				iMasterID = row.Cells[col_SalesId.Name].Value.ToInt32();
				txtVoucherNo.Data = row.Cells[1].Value.ToString().ToInt32();
				txtSalesType.Text = row.Cells[2].Value.ToString();
				txtParty.Text = row.Cells[4].Value.ToString();
				txtSalesman.Text = row.Cells[11].Value.ToString();
				txtRoute.Text = row.Cells[12].Value.ToString();
				txtNetTotal.Value = row.Cells[10].Value.ToString().ToDecimal();
				txtFreight.Value = row.Cells[7].Value.ToString().ToDecimal();
				txtAditional.Value = row.Cells[6].Value.ToString().ToDecimal();
				txtBillDiscount.Value = row.Cells[8].Value.ToString().ToDecimal();
				txtAdditionalDiscount.Value = row.Cells[9].Value.ToString().ToDecimal();
				txtGrandTotal.Value = row.Cells[3].Value.ToString().ToDecimal();
				txtCashReceived.Value = row.Cells[col_Cash.Name].Value.ToString().ToDecimal() + row.Cells[col_Card.Name].Value.ToString().ToDecimal();
				txtBalance.Value = row.Cells[col_Credit.Name].Value.ToString().ToDecimal();
			}
		}

		private void btnShowProduct_Click(object sender, EventArgs e)
		{
			ShowProductMethod();
		}

		private void ShowProductMethod()
		{
			if (txtVoucherNo.Value > 0m)
			{
				showProduct = new ShowProduct();
				showProduct.SalesId = iMasterID.toInt32();
				showProduct.TotalAmount = txtNetTotal.Value;
				showProduct.MdiParent = base.MdiParent;
				showProduct.Show();
			}
			else
			{
				MessageBox.Show("No Voucher Selected!", Utility.uManufacturer, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (CommonSettings.blnSalesEstimateEnable)
			{
				salesEstimate = new SalesEstimateView();
				salesEstimate.iInvNo = dgDetails.CurrentRow.Cells[1].Value.ToInt32();
				salesEstimate.MdiParent = base.MdiParent;
				salesEstimate.Show();
			}
			else
			{
				salesInvoice = new SalesInvoiceView();
				salesInvoice.MdiParent = base.MdiParent;
				salesInvoice.Show();
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
			rdbShowAll = new System.Windows.Forms.RadioButton();
			rdbCashOnly = new System.Windows.Forms.RadioButton();
			btnShowProduct = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dgDetails = new System.Windows.Forms.DataGridView();
			cbRefresh = new System.Windows.Forms.CheckBox();
			panel2 = new System.Windows.Forms.Panel();
			panel3 = new System.Windows.Forms.Panel();
			txtSalesType = new AshUsercontrol.AshTextBoxNormal();
			txtAdditionalDiscount = new AshUsercontrol.AshTextBoxNormal();
			label9 = new System.Windows.Forms.Label();
			txtAditional = new AshUsercontrol.AshTextBoxNormal();
			txtBalance = new AshUsercontrol.AshTextBoxNormal();
			txtCashReceived = new AshUsercontrol.AshTextBoxNormal();
			label12 = new System.Windows.Forms.Label();
			txtGrandTotal = new AshUsercontrol.AshTextBoxNormal();
			label11 = new System.Windows.Forms.Label();
			txtBillDiscount = new AshUsercontrol.AshTextBoxNormal();
			label10 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			txtFreight = new AshUsercontrol.AshTextBoxNormal();
			label6 = new System.Windows.Forms.Label();
			txtNetTotal = new AshUsercontrol.AshTextBoxNormal();
			label5 = new System.Windows.Forms.Label();
			txtRoute = new AshUsercontrol.AshTextBoxNormal();
			label13 = new System.Windows.Forms.Label();
			txtSalesman = new AshUsercontrol.AshTextBoxNormal();
			label4 = new System.Windows.Forms.Label();
			txtParty = new AshUsercontrol.AshTextBoxNormal();
			label3 = new System.Windows.Forms.Label();
			txtVoucherNo = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			showProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			focusListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			focusVchrNoTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			focusDatePickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			focusRadioButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			closeApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			calculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			lblHeader = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_GrandTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_AddAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Freight = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_AddDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Net = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SalesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Cash = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Card = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(996, 0);
			rdbShowAll.AutoSize = true;
			rdbShowAll.Checked = true;
			rdbShowAll.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbShowAll.Location = new System.Drawing.Point(143, 52);
			rdbShowAll.Name = "rdbShowAll";
			rdbShowAll.Size = new System.Drawing.Size(104, 20);
			rdbShowAll.TabIndex = 2;
			rdbShowAll.TabStop = true;
			rdbShowAll.Text = "Show All Bill";
			rdbShowAll.UseVisualStyleBackColor = true;
			rdbShowAll.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
			rdbCashOnly.AutoSize = true;
			rdbCashOnly.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			rdbCashOnly.Location = new System.Drawing.Point(253, 52);
			rdbCashOnly.Name = "rdbCashOnly";
			rdbCashOnly.Size = new System.Drawing.Size(132, 20);
			rdbCashOnly.TabIndex = 3;
			rdbCashOnly.Text = "Cash Sales Only";
			rdbCashOnly.UseVisualStyleBackColor = true;
			rdbCashOnly.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
			btnShowProduct.BackColor = System.Drawing.Color.DodgerBlue;
			btnShowProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnShowProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnShowProduct.ForeColor = System.Drawing.Color.White;
			btnShowProduct.Location = new System.Drawing.Point(390, 45);
			btnShowProduct.Name = "btnShowProduct";
			btnShowProduct.Size = new System.Drawing.Size(101, 30);
			btnShowProduct.TabIndex = 4;
			btnShowProduct.Text = "Show Product";
			toolTip1.SetToolTip(btnShowProduct, "Press Alt + S");
			btnShowProduct.UseVisualStyleBackColor = false;
			btnShowProduct.Click += new System.EventHandler(btnShowProduct_Click);
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgDetails);
			panel1.Location = new System.Drawing.Point(12, 82);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(641, 285);
			panel1.TabIndex = 6;
			dgDetails.AllowUserToAddRows = false;
			dgDetails.AllowUserToDeleteRows = false;
			dgDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgDetails.Columns.AddRange(Column1, Column2, Column14, col_GrandTotal, Column4, col_Tax, col_AddAmnt, col_Freight, col_Discount, col_AddDiscount, col_Net, Column11, Column12, col_SalesId, col_Cash, col_Card, col_Credit);
			dgDetails.Location = new System.Drawing.Point(10, 12);
			dgDetails.Name = "dgDetails";
			dgDetails.ReadOnly = true;
			dgDetails.RowHeadersVisible = false;
			dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgDetails.Size = new System.Drawing.Size(621, 259);
			dgDetails.TabIndex = 0;
			dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			dgDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
			cbRefresh.AutoSize = true;
			cbRefresh.Checked = true;
			cbRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
			cbRefresh.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbRefresh.Location = new System.Drawing.Point(496, 52);
			cbRefresh.Name = "cbRefresh";
			cbRefresh.Size = new System.Drawing.Size(112, 20);
			cbRefresh.TabIndex = 5;
			cbRefresh.Text = "Auto Refresh";
			cbRefresh.UseVisualStyleBackColor = true;
			cbRefresh.CheckedChanged += new System.EventHandler(cbRefresh_CheckedChanged);
			panel2.BackColor = System.Drawing.Color.DodgerBlue;
			panel2.Controls.Add(panel3);
			panel2.Location = new System.Drawing.Point(661, 39);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(366, 330);
			panel2.TabIndex = 7;
			panel3.BackColor = System.Drawing.Color.White;
			panel3.Controls.Add(txtSalesType);
			panel3.Controls.Add(txtAdditionalDiscount);
			panel3.Controls.Add(label9);
			panel3.Controls.Add(txtAditional);
			panel3.Controls.Add(txtBalance);
			panel3.Controls.Add(txtCashReceived);
			panel3.Controls.Add(label12);
			panel3.Controls.Add(txtGrandTotal);
			panel3.Controls.Add(label11);
			panel3.Controls.Add(txtBillDiscount);
			panel3.Controls.Add(label10);
			panel3.Controls.Add(label7);
			panel3.Controls.Add(label8);
			panel3.Controls.Add(txtFreight);
			panel3.Controls.Add(label6);
			panel3.Controls.Add(txtNetTotal);
			panel3.Controls.Add(label5);
			panel3.Controls.Add(txtRoute);
			panel3.Controls.Add(label13);
			panel3.Controls.Add(txtSalesman);
			panel3.Controls.Add(label4);
			panel3.Controls.Add(txtParty);
			panel3.Controls.Add(label3);
			panel3.Controls.Add(txtVoucherNo);
			panel3.Controls.Add(label2);
			panel3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			panel3.Location = new System.Drawing.Point(8, 9);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(349, 306);
			panel3.TabIndex = 0;
			txtSalesType.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSalesType.Data = 0;
			txtSalesType.EnterColor = System.Drawing.Color.LightPink;
			txtSalesType.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSalesType.Format = null;
			txtSalesType.isAllowNegative = false;
			txtSalesType.isAllowSpecialChar = false;
			txtSalesType.isNumberOnly = false;
			txtSalesType.isNumeric = false;
			txtSalesType.LeaveColor = System.Drawing.Color.White;
			txtSalesType.Location = new System.Drawing.Point(234, 10);
			txtSalesType.Margin = new System.Windows.Forms.Padding(4);
			txtSalesType.Name = "txtSalesType";
			txtSalesType.Size = new System.Drawing.Size(100, 16);
			txtSalesType.TabIndex = 2;
			txtSalesType.Value = new decimal(new int[4]);
			txtSalesType.WaterMark = null;
			txtAdditionalDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAdditionalDiscount.Data = 0;
			txtAdditionalDiscount.EnterColor = System.Drawing.Color.LightPink;
			txtAdditionalDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAdditionalDiscount.Format = null;
			txtAdditionalDiscount.isAllowNegative = false;
			txtAdditionalDiscount.isAllowSpecialChar = false;
			txtAdditionalDiscount.isNumberOnly = false;
			txtAdditionalDiscount.isNumeric = true;
			txtAdditionalDiscount.LeaveColor = System.Drawing.Color.White;
			txtAdditionalDiscount.Location = new System.Drawing.Point(259, 162);
			txtAdditionalDiscount.Margin = new System.Windows.Forms.Padding(4);
			txtAdditionalDiscount.Name = "txtAdditionalDiscount";
			txtAdditionalDiscount.Size = new System.Drawing.Size(75, 16);
			txtAdditionalDiscount.TabIndex = 18;
			txtAdditionalDiscount.Text = "0";
			txtAdditionalDiscount.Value = new decimal(new int[4]);
			txtAdditionalDiscount.WaterMark = null;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(194, 164);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(63, 16);
			label9.TabIndex = 17;
			label9.Text = "Add Dis:";
			txtAditional.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtAditional.Data = 0;
			txtAditional.EnterColor = System.Drawing.Color.LightPink;
			txtAditional.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtAditional.Format = null;
			txtAditional.isAllowNegative = false;
			txtAditional.isAllowSpecialChar = false;
			txtAditional.isNumberOnly = false;
			txtAditional.isNumeric = true;
			txtAditional.LeaveColor = System.Drawing.Color.White;
			txtAditional.Location = new System.Drawing.Point(259, 140);
			txtAditional.Margin = new System.Windows.Forms.Padding(4);
			txtAditional.Name = "txtAditional";
			txtAditional.Size = new System.Drawing.Size(75, 16);
			txtAditional.TabIndex = 14;
			txtAditional.Text = "0";
			txtAditional.Value = new decimal(new int[4]);
			txtAditional.WaterMark = null;
			txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBalance.Data = 0;
			txtBalance.EnterColor = System.Drawing.Color.LightPink;
			txtBalance.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtBalance.Format = null;
			txtBalance.isAllowNegative = false;
			txtBalance.isAllowSpecialChar = false;
			txtBalance.isNumberOnly = false;
			txtBalance.isNumeric = true;
			txtBalance.LeaveColor = System.Drawing.Color.White;
			txtBalance.Location = new System.Drawing.Point(106, 244);
			txtBalance.Margin = new System.Windows.Forms.Padding(4);
			txtBalance.Name = "txtBalance";
			txtBalance.Size = new System.Drawing.Size(228, 16);
			txtBalance.TabIndex = 24;
			txtBalance.Text = "0";
			txtBalance.Value = new decimal(new int[4]);
			txtBalance.WaterMark = null;
			txtCashReceived.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtCashReceived.Data = 0;
			txtCashReceived.EnterColor = System.Drawing.Color.LightPink;
			txtCashReceived.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtCashReceived.Format = null;
			txtCashReceived.isAllowNegative = false;
			txtCashReceived.isAllowSpecialChar = false;
			txtCashReceived.isNumberOnly = false;
			txtCashReceived.isNumeric = true;
			txtCashReceived.LeaveColor = System.Drawing.Color.White;
			txtCashReceived.Location = new System.Drawing.Point(106, 218);
			txtCashReceived.Margin = new System.Windows.Forms.Padding(4);
			txtCashReceived.Name = "txtCashReceived";
			txtCashReceived.Size = new System.Drawing.Size(228, 16);
			txtCashReceived.TabIndex = 22;
			txtCashReceived.Text = "0";
			txtCashReceived.Value = new decimal(new int[4]);
			txtCashReceived.WaterMark = null;
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(34, 247);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(65, 16);
			label12.TabIndex = 23;
			label12.Text = "Balance:";
			txtGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtGrandTotal.Data = 0;
			txtGrandTotal.EnterColor = System.Drawing.Color.LightPink;
			txtGrandTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtGrandTotal.Format = null;
			txtGrandTotal.isAllowNegative = false;
			txtGrandTotal.isAllowSpecialChar = false;
			txtGrandTotal.isNumberOnly = false;
			txtGrandTotal.isNumeric = true;
			txtGrandTotal.LeaveColor = System.Drawing.Color.White;
			txtGrandTotal.Location = new System.Drawing.Point(106, 190);
			txtGrandTotal.Margin = new System.Windows.Forms.Padding(4);
			txtGrandTotal.Name = "txtGrandTotal";
			txtGrandTotal.Size = new System.Drawing.Size(228, 16);
			txtGrandTotal.TabIndex = 20;
			txtGrandTotal.Text = "0";
			txtGrandTotal.Value = new decimal(new int[4]);
			txtGrandTotal.WaterMark = null;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(8, 221);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(91, 16);
			label11.TabIndex = 21;
			label11.Text = "Cash Recvd:";
			txtBillDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtBillDiscount.Data = 0;
			txtBillDiscount.EnterColor = System.Drawing.Color.LightPink;
			txtBillDiscount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtBillDiscount.Format = null;
			txtBillDiscount.isAllowNegative = false;
			txtBillDiscount.isAllowSpecialChar = false;
			txtBillDiscount.isNumberOnly = false;
			txtBillDiscount.isNumeric = true;
			txtBillDiscount.LeaveColor = System.Drawing.Color.White;
			txtBillDiscount.Location = new System.Drawing.Point(106, 166);
			txtBillDiscount.Margin = new System.Windows.Forms.Padding(4);
			txtBillDiscount.Name = "txtBillDiscount";
			txtBillDiscount.Size = new System.Drawing.Size(75, 16);
			txtBillDiscount.TabIndex = 16;
			txtBillDiscount.Text = "0";
			txtBillDiscount.Value = new decimal(new int[4]);
			txtBillDiscount.WaterMark = null;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(8, 195);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(91, 16);
			label10.TabIndex = 19;
			label10.Text = "Grand Total:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(179, 138);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(78, 16);
			label7.TabIndex = 13;
			label7.Text = "Additional:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(44, 169);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(55, 16);
			label8.TabIndex = 15;
			label8.Text = "Bill Dis:";
			txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtFreight.Data = 0;
			txtFreight.EnterColor = System.Drawing.Color.LightPink;
			txtFreight.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtFreight.Format = null;
			txtFreight.isAllowNegative = false;
			txtFreight.isAllowSpecialChar = false;
			txtFreight.isNumberOnly = false;
			txtFreight.isNumeric = true;
			txtFreight.LeaveColor = System.Drawing.Color.White;
			txtFreight.Location = new System.Drawing.Point(106, 140);
			txtFreight.Margin = new System.Windows.Forms.Padding(4);
			txtFreight.Name = "txtFreight";
			txtFreight.Size = new System.Drawing.Size(75, 16);
			txtFreight.TabIndex = 12;
			txtFreight.Text = "0";
			txtFreight.Value = new decimal(new int[4]);
			txtFreight.WaterMark = null;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(39, 143);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(60, 16);
			label6.TabIndex = 11;
			label6.Text = "Freight:";
			txtNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtNetTotal.Data = 0;
			txtNetTotal.EnterColor = System.Drawing.Color.LightPink;
			txtNetTotal.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtNetTotal.Format = null;
			txtNetTotal.isAllowNegative = false;
			txtNetTotal.isAllowSpecialChar = false;
			txtNetTotal.isNumberOnly = false;
			txtNetTotal.isNumeric = true;
			txtNetTotal.LeaveColor = System.Drawing.Color.White;
			txtNetTotal.Location = new System.Drawing.Point(106, 114);
			txtNetTotal.Margin = new System.Windows.Forms.Padding(4);
			txtNetTotal.Name = "txtNetTotal";
			txtNetTotal.Size = new System.Drawing.Size(228, 16);
			txtNetTotal.TabIndex = 10;
			txtNetTotal.Text = "0";
			txtNetTotal.Value = new decimal(new int[4]);
			txtNetTotal.WaterMark = null;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(23, 117);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(76, 16);
			label5.TabIndex = 9;
			label5.Text = "Net Total:";
			txtRoute.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtRoute.Data = 0;
			txtRoute.EnterColor = System.Drawing.Color.LightPink;
			txtRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtRoute.Format = null;
			txtRoute.isAllowNegative = false;
			txtRoute.isAllowSpecialChar = false;
			txtRoute.isNumberOnly = false;
			txtRoute.isNumeric = false;
			txtRoute.LeaveColor = System.Drawing.Color.White;
			txtRoute.Location = new System.Drawing.Point(106, 88);
			txtRoute.Margin = new System.Windows.Forms.Padding(4);
			txtRoute.Name = "txtRoute";
			txtRoute.Size = new System.Drawing.Size(228, 16);
			txtRoute.TabIndex = 8;
			txtRoute.Value = new decimal(new int[4]);
			txtRoute.WaterMark = null;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(47, 91);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(52, 16);
			label13.TabIndex = 7;
			label13.Text = "Route:";
			txtSalesman.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtSalesman.Data = 0;
			txtSalesman.EnterColor = System.Drawing.Color.LightPink;
			txtSalesman.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtSalesman.Format = null;
			txtSalesman.isAllowNegative = false;
			txtSalesman.isAllowSpecialChar = false;
			txtSalesman.isNumberOnly = false;
			txtSalesman.isNumeric = false;
			txtSalesman.LeaveColor = System.Drawing.Color.White;
			txtSalesman.Location = new System.Drawing.Point(106, 62);
			txtSalesman.Margin = new System.Windows.Forms.Padding(4);
			txtSalesman.Name = "txtSalesman";
			txtSalesman.Size = new System.Drawing.Size(228, 16);
			txtSalesman.TabIndex = 6;
			txtSalesman.Value = new decimal(new int[4]);
			txtSalesman.WaterMark = null;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(18, 65);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(81, 16);
			label4.TabIndex = 5;
			label4.Text = "Sales Man:";
			txtParty.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtParty.Data = 0;
			txtParty.EnterColor = System.Drawing.Color.LightPink;
			txtParty.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtParty.Format = null;
			txtParty.isAllowNegative = false;
			txtParty.isAllowSpecialChar = false;
			txtParty.isNumberOnly = false;
			txtParty.isNumeric = false;
			txtParty.LeaveColor = System.Drawing.Color.White;
			txtParty.Location = new System.Drawing.Point(106, 36);
			txtParty.Margin = new System.Windows.Forms.Padding(4);
			txtParty.Name = "txtParty";
			txtParty.Size = new System.Drawing.Size(228, 16);
			txtParty.TabIndex = 4;
			txtParty.Value = new decimal(new int[4]);
			txtParty.WaterMark = null;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(50, 39);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(49, 16);
			label3.TabIndex = 3;
			label3.Text = "Party:";
			txtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtVoucherNo.Data = 0;
			txtVoucherNo.EnterColor = System.Drawing.Color.LightPink;
			txtVoucherNo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtVoucherNo.Format = null;
			txtVoucherNo.isAllowNegative = false;
			txtVoucherNo.isAllowSpecialChar = false;
			txtVoucherNo.isNumberOnly = true;
			txtVoucherNo.isNumeric = false;
			txtVoucherNo.LeaveColor = System.Drawing.Color.White;
			txtVoucherNo.Location = new System.Drawing.Point(106, 10);
			txtVoucherNo.Margin = new System.Windows.Forms.Padding(4);
			txtVoucherNo.Name = "txtVoucherNo";
			txtVoucherNo.Size = new System.Drawing.Size(112, 16);
			txtVoucherNo.TabIndex = 1;
			txtVoucherNo.Text = "0";
			txtVoucherNo.Value = new decimal(new int[4]);
			txtVoucherNo.WaterMark = null;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(9, 13);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(90, 16);
			label2.TabIndex = 0;
			label2.Text = "Voucher No:";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { showProductToolStripMenuItem, focusListToolStripMenuItem, focusVchrNoTextToolStripMenuItem, autoRefreshToolStripMenuItem, focusDatePickerToolStripMenuItem, focusRadioButtonToolStripMenuItem, closeApplicationToolStripMenuItem, calculatorToolStripMenuItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(251, 180);
			showProductToolStripMenuItem.Name = "showProductToolStripMenuItem";
			showProductToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + S";
			showProductToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			showProductToolStripMenuItem.Text = "Show Product";
			focusListToolStripMenuItem.Name = "focusListToolStripMenuItem";
			focusListToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + V";
			focusListToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			focusListToolStripMenuItem.Text = "Focus List";
			focusVchrNoTextToolStripMenuItem.Name = "focusVchrNoTextToolStripMenuItem";
			focusVchrNoTextToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + T";
			focusVchrNoTextToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			focusVchrNoTextToolStripMenuItem.Text = "Focus Vchr No Text";
			autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
			autoRefreshToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + R";
			autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			autoRefreshToolStripMenuItem.Text = "Auto Refresh";
			focusDatePickerToolStripMenuItem.Name = "focusDatePickerToolStripMenuItem";
			focusDatePickerToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + D";
			focusDatePickerToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			focusDatePickerToolStripMenuItem.Text = "Focus Date Picker";
			focusRadioButtonToolStripMenuItem.Name = "focusRadioButtonToolStripMenuItem";
			focusRadioButtonToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + B";
			focusRadioButtonToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			focusRadioButtonToolStripMenuItem.Text = "Focus Radio Button";
			closeApplicationToolStripMenuItem.Name = "closeApplicationToolStripMenuItem";
			closeApplicationToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + X";
			closeApplicationToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			closeApplicationToolStripMenuItem.Text = "Close Application";
			calculatorToolStripMenuItem.Name = "calculatorToolStripMenuItem";
			calculatorToolStripMenuItem.ShortcutKeyDisplayString = "Press Alt + C";
			calculatorToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
			calculatorToolStripMenuItem.Text = "Calculator";
			lblHeader.AutoSize = true;
			lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHeader.Location = new System.Drawing.Point(3, 3);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new System.Drawing.Size(113, 24);
			lblHeader.TabIndex = 10;
			lblHeader.Text = "Sales View";
			dtmTo.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(534, 10);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(194, 23);
			dtmTo.TabIndex = 55;
			dtmTo.Visible = false;
			dtmFrom.CustomFormat = "dd-MM-yyyy HH:mm:ss";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(240, 9);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(197, 23);
			dtmFrom.TabIndex = 56;
			dtmFrom.Visible = false;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label14.Location = new System.Drawing.Point(450, 10);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(72, 16);
			label14.TabIndex = 53;
			label14.Text = "Date To :";
			label14.Visible = false;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f);
			label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label15.Location = new System.Drawing.Point(149, 10);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(87, 16);
			label15.TabIndex = 54;
			label15.Text = "Date From :";
			label15.Visible = false;
			Column1.HeaderText = "SL";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Width = 50;
			Column2.HeaderText = "Voucher No";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Width = 80;
			Column14.HeaderText = "Invoice Type";
			Column14.Name = "Column14";
			Column14.ReadOnly = true;
			col_GrandTotal.HeaderText = "Grand Total";
			col_GrandTotal.Name = "col_GrandTotal";
			col_GrandTotal.ReadOnly = true;
			Column4.HeaderText = "Party Name";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Width = 200;
			col_Tax.HeaderText = "Tax";
			col_Tax.Name = "col_Tax";
			col_Tax.ReadOnly = true;
			col_Tax.Width = 80;
			col_AddAmnt.HeaderText = "Additinal Amount";
			col_AddAmnt.Name = "col_AddAmnt";
			col_AddAmnt.ReadOnly = true;
			col_AddAmnt.Width = 80;
			col_Freight.HeaderText = "Freight";
			col_Freight.Name = "col_Freight";
			col_Freight.ReadOnly = true;
			col_Freight.Width = 70;
			col_Discount.HeaderText = "Discount";
			col_Discount.Name = "col_Discount";
			col_Discount.ReadOnly = true;
			col_Discount.Width = 80;
			col_AddDiscount.HeaderText = "Aditional Discount";
			col_AddDiscount.Name = "col_AddDiscount";
			col_AddDiscount.ReadOnly = true;
			col_AddDiscount.Width = 80;
			col_Net.HeaderText = "Net Amount";
			col_Net.Name = "col_Net";
			col_Net.ReadOnly = true;
			Column11.HeaderText = "SalesMan";
			Column11.Name = "Column11";
			Column11.ReadOnly = true;
			Column11.Visible = false;
			Column12.HeaderText = "Route";
			Column12.Name = "Column12";
			Column12.ReadOnly = true;
			Column12.Visible = false;
			col_SalesId.HeaderText = "InvoiceId";
			col_SalesId.Name = "col_SalesId";
			col_SalesId.ReadOnly = true;
			col_SalesId.Visible = false;
			col_Cash.HeaderText = "Cash";
			col_Cash.Name = "col_Cash";
			col_Cash.ReadOnly = true;
			col_Cash.Visible = false;
			col_Card.HeaderText = "Card";
			col_Card.Name = "col_Card";
			col_Card.ReadOnly = true;
			col_Card.Visible = false;
			col_Credit.HeaderText = "Credit";
			col_Credit.Name = "col_Credit";
			col_Credit.ReadOnly = true;
			col_Credit.Visible = false;
			base.AcceptButton = btnShowProduct;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1032, 380);
			ContextMenuStrip = contextMenuStrip1;
			base.Controls.Add(dtmTo);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label14);
			base.Controls.Add(label15);
			base.Controls.Add(lblHeader);
			base.Controls.Add(panel2);
			base.Controls.Add(cbRefresh);
			base.Controls.Add(panel1);
			base.Controls.Add(btnShowProduct);
			base.Controls.Add(rdbCashOnly);
			base.Controls.Add(rdbShowAll);
			base.MaximizeBox = false;
			base.Name = "SalesView";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SalesView_FormClosing);
			base.Load += new System.EventHandler(SalesView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SalesView_KeyDown);
			base.Controls.SetChildIndex(rdbShowAll, 0);
			base.Controls.SetChildIndex(rdbCashOnly, 0);
			base.Controls.SetChildIndex(btnShowProduct, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(cbRefresh, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(lblHeader, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(label14, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			panel2.ResumeLayout(false);
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
