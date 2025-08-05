using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class ProductTracing : BaseForm
	{
		private DBContext db = new DBContext();

		private string Query;

		private IContainer components = null;

		private CheckBox cbParty;

		private AshTextBoxNormal txtParty;

		private Label label1;

		private AshDateTimePicker dtmFrom;

		private Label label2;

		private AshDateTimePicker dtmTo;

		private Label lblFirst;

		private ListView livSales;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader11;

		private Label lblSecond;

		private Label label5;

		private Label lblProduct;

		private ColumnHeader columnHeader23;

		private ListView livPurchase;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader15;

		private ColumnHeader columnHeader16;

		private ColumnHeader columnHeader17;

		private ColumnHeader columnHeader18;

		private ColumnHeader columnHeader19;

		private ColumnHeader columnHeader20;

		private ColumnHeader columnHeader21;

		private ColumnHeader columnHeader24;

		private Button button4;

		private Label lblHead;

		public string TypeVoucher { get; set; }

		public int PartyAccntId { get; set; }

		public int ProductId { get; set; }

		public ProductTracing()
		{
			InitializeComponent();
		}

		private void LoadListAfterFormLoad()
		{
			if (TypeVoucher == "Sale")
			{
				FillListviewmethodAfterLoadingAsSales();
			}
			else if (TypeVoucher == "Purchase")
			{
				FillListviewmethodAfterLoadingAsPurchase();
			}
			else if (TypeVoucher == "Purchase Return")
			{
				FillListviewmethodAfterLoadingAsPurchaseReturn();
			}
			else
			{
				FillListviewmethodAfterLoadingAsSalesReturn();
			}
		}

		private void FillListviewmethodAfterLoadingAsSales()
		{
			if (cbParty.Checked)
			{
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "'and PartyAccountId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
			else
			{
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
			Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
			SalesDetailesFindMethod(Query, livPurchase, null, "PurInvMaster", "PurInvNo", "PurInvId");
		}

		private void FillListviewmethodAfterLoadingAsSalesReturn()
		{
			if (cbParty.Checked)
			{
				Query = "SELECT PartyAccountId,Date,SlsRtnId,ProQty,UnitId,ProRate,ProDis,NetAmnt FROM SlsRtnSettings where ProductId='" + ProductId + "' and PartyAccountId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Sales Return", "SalesRtnMaster", "SlRtnInvNo", "SlsRtrnId");
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and PartyAccountId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
			else
			{
				Query = "SELECT PartyAccountId,Date,SlsRtnId,ProQty,UnitId,ProRate,ProDis,NetAmnt FROM SlsRtnSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Sales Return", "SalesRtnMaster", "SlRtnInvNo", "SlsRtrnId");
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
		}

		private void FillListviewmethodAfterLoadingAsPurchase()
		{
			if (cbParty.Checked)
			{
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and PartyAccId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
			else
			{
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
			Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
			SalesDetailesFindMethod(Query, livPurchase, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
		}

		private void FillListviewmethodAfterLoadingAsPurchaseReturn()
		{
			if (cbParty.Checked)
			{
				Query = "SELECT PartyAccId,Date,PurRtnInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurRtnInvSttings where ProId='" + ProductId + "' and PartyAccId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Purchase Return", "PurRtnInvMaster", "PurRtnInvNo", "PurRtnInvId");
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and PartyAccId='" + PartyAccntId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
			else
			{
				Query = "SELECT PartyAccId,Date,PurRtnInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurRtnInvSttings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Purchase Return", "PurRtnInvMaster", "PurRtnInvNo", "PurRtnInvId");
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
		}

		private void SalesDetailesFindMethod(string strQuery, ListView listView, string strVoucherType = null, string strTable = null, string strInvNo = null, string strInvId = null)
		{
			string empty = string.Empty;
			string strID = string.Empty;
			listView.Items.Clear();
			DataTable dataTable = new DataTable();
			dataTable = AshproCommon.GetDataTable(strQuery);
			foreach (DataRow drw in dataTable.Rows)
			{
				AccountEntity accountEntity = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountId == drw[0].ToInt32());
				empty = drw[2].ToString();
				AshproCommon.IdFindMethod(strTable, strInvNo, strInvId, empty, out strID);
				if (strVoucherType == null)
				{
					AshproCommon.IdFindMethod(strTable, "InvType", strInvId, empty, out strVoucherType);
				}
				ListViewItem listViewItem = new ListViewItem((listView.Items.Count + 1).ToString());
				listViewItem.SubItems.Add(drw[1].ToString());
				listViewItem.SubItems.Add(accountEntity.AccountName);
				listViewItem.SubItems.Add(accountEntity.AddressOne);
				listViewItem.SubItems.Add(strVoucherType);
				listViewItem.SubItems.Add(strID);
				listViewItem.SubItems.Add(drw[3].ToString());
				listViewItem.SubItems.Add(db.Units().Single((UnitEntity x) => x.UnitId == drw[4].ToInt32()).UnitName);
				listViewItem.SubItems.Add(drw[5].ToString());
				listViewItem.SubItems.Add(drw[6].ToString());
				listViewItem.SubItems.Add(drw[7].ToString());
				listView.Items.Add(listViewItem);
			}
		}

		private void cbParty_CheckedChanged(object sender, EventArgs e)
		{
			LoadListAfterFormLoad();
		}

		private void ProductTracing_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.P && e.Alt)
			{
				if (cbParty.Checked)
				{
					cbParty.Checked = false;
				}
				else
				{
					cbParty.Checked = true;
				}
			}
			else if (e.KeyCode == Keys.X && e.Alt)
			{
				Close();
			}
		}

		private void ProductTracing_Load(object sender, EventArgs e)
		{
			dtmFrom.Value = dtmTo.Value.AddDays(-90.0);
			lblProduct.Text = db.Products().FirstOrDefault((ProductEntity x) => x.ProductId == ProductId).ProductName;
			txtParty.Text = db.Accounts().FirstOrDefault((AccountEntity x) => x.AccountId == PartyAccntId).AccountName;
			lblFirst.Text = TypeVoucher;
			if (TypeVoucher == "Sales")
			{
				lblSecond.Text = "Purchase";
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
			else if (TypeVoucher == "Purchase")
			{
				lblSecond.Text = "Sales";
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, null, "PurInvMaster", "PurInvNo", "PurInvId");
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
			else if (TypeVoucher == "Purchase Return")
			{
				lblSecond.Text = "Purchase";
				Query = "SELECT PartyAccId,Date,PurRtnInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurRtnInvSttings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Purchase Return", "PurRtnInvMaster", "PurRtnInvNo", "PurRtnInvId");
				Query = "SELECT PartyAccId,Date,PurInvId,ProQty,UnitId,ProRate,Disc,NetAmnt FROM PurInvSettings where ProId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "PurInvMaster", "PurInvNo", "PurInvId");
			}
			else
			{
				lblSecond.Text = "Sale";
				Query = "SELECT PartyAccountId,Date,SlsRtnId,ProQty,UnitId,ProRate,ProDis,NetAmnt FROM SlsRtnSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livSales, "Sales Return", "SalesRtnMaster", "SlRtnInvNo", "SlsRtrnId");
				Query = "SELECT PartyAccountId,Date,SalesInvId,ProQty,UnitId,ProRate,ProDiscont,NetAmnt FROM SalesEstimateSettings where ProductId='" + ProductId + "' and Date between '" + dtmFrom.Text + "' and '" + dtmTo.Text + "'";
				SalesDetailesFindMethod(Query, livPurchase, null, "SalesEstimateMaster", "SalesInvNo", "SalesInvId");
			}
		}

		private void dtmFrom_ValueChanged(object sender, EventArgs e)
		{
			LoadListAfterFormLoad();
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
			cbParty = new System.Windows.Forms.CheckBox();
			txtParty = new AshUsercontrol.AshTextBoxNormal();
			label1 = new System.Windows.Forms.Label();
			dtmFrom = new AshUsercontrol.AshDateTimePicker();
			label2 = new System.Windows.Forms.Label();
			dtmTo = new AshUsercontrol.AshDateTimePicker();
			lblFirst = new System.Windows.Forms.Label();
			livSales = new System.Windows.Forms.ListView();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();
			columnHeader4 = new System.Windows.Forms.ColumnHeader();
			columnHeader5 = new System.Windows.Forms.ColumnHeader();
			columnHeader6 = new System.Windows.Forms.ColumnHeader();
			columnHeader7 = new System.Windows.Forms.ColumnHeader();
			columnHeader23 = new System.Windows.Forms.ColumnHeader();
			columnHeader8 = new System.Windows.Forms.ColumnHeader();
			columnHeader9 = new System.Windows.Forms.ColumnHeader();
			columnHeader11 = new System.Windows.Forms.ColumnHeader();
			lblSecond = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			lblProduct = new System.Windows.Forms.Label();
			livPurchase = new System.Windows.Forms.ListView();
			columnHeader12 = new System.Windows.Forms.ColumnHeader();
			columnHeader13 = new System.Windows.Forms.ColumnHeader();
			columnHeader14 = new System.Windows.Forms.ColumnHeader();
			columnHeader15 = new System.Windows.Forms.ColumnHeader();
			columnHeader16 = new System.Windows.Forms.ColumnHeader();
			columnHeader17 = new System.Windows.Forms.ColumnHeader();
			columnHeader18 = new System.Windows.Forms.ColumnHeader();
			columnHeader19 = new System.Windows.Forms.ColumnHeader();
			columnHeader20 = new System.Windows.Forms.ColumnHeader();
			columnHeader21 = new System.Windows.Forms.ColumnHeader();
			columnHeader24 = new System.Windows.Forms.ColumnHeader();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(858, 1);
			cbParty.AutoSize = true;
			cbParty.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cbParty.Location = new System.Drawing.Point(8, 38);
			cbParty.Name = "cbParty";
			cbParty.Size = new System.Drawing.Size(62, 20);
			cbParty.TabIndex = 0;
			cbParty.Text = "Party";
			cbParty.UseVisualStyleBackColor = true;
			cbParty.CheckedChanged += new System.EventHandler(cbParty_CheckedChanged);
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
			txtParty.Location = new System.Drawing.Point(75, 38);
			txtParty.Margin = new System.Windows.Forms.Padding(4);
			txtParty.Name = "txtParty";
			txtParty.Size = new System.Drawing.Size(246, 16);
			txtParty.TabIndex = 1;
			txtParty.Value = new decimal(new int[4]);
			txtParty.WaterMark = null;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(337, 38);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(51, 16);
			label1.TabIndex = 2;
			label1.Text = "From :";
			dtmFrom.CustomFormat = "dd-MMM-yyyy";
			dtmFrom.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmFrom.Location = new System.Drawing.Point(389, 38);
			dtmFrom.Name = "dtmFrom";
			dtmFrom.Size = new System.Drawing.Size(128, 23);
			dtmFrom.TabIndex = 3;
			dtmFrom.ValueChanged += new System.EventHandler(dtmFrom_ValueChanged);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(542, 38);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(36, 16);
			label2.TabIndex = 2;
			label2.Text = "To :";
			dtmTo.CustomFormat = "dd-MMM-yyyy";
			dtmTo.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dtmTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtmTo.Location = new System.Drawing.Point(578, 38);
			dtmTo.Name = "dtmTo";
			dtmTo.Size = new System.Drawing.Size(128, 23);
			dtmTo.TabIndex = 3;
			dtmTo.ValueChanged += new System.EventHandler(dtmFrom_ValueChanged);
			lblFirst.AutoSize = true;
			lblFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblFirst.ForeColor = System.Drawing.Color.Red;
			lblFirst.Location = new System.Drawing.Point(7, 72);
			lblFirst.Name = "lblFirst";
			lblFirst.Size = new System.Drawing.Size(48, 17);
			lblFirst.TabIndex = 4;
			lblFirst.Text = "Sales";
			livSales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[11]
			{
				columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader23, columnHeader8, columnHeader9,
				columnHeader11
			});
			livSales.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			livSales.GridLines = true;
			livSales.HideSelection = false;
			livSales.Location = new System.Drawing.Point(10, 92);
			livSales.Name = "livSales";
			livSales.Size = new System.Drawing.Size(875, 299);
			livSales.TabIndex = 5;
			livSales.UseCompatibleStateImageBehavior = false;
			livSales.View = System.Windows.Forms.View.Details;
			columnHeader1.Text = "SL";
			columnHeader1.Width = 40;
			columnHeader2.Text = "Date";
			columnHeader2.Width = 80;
			columnHeader3.Text = "Party";
			columnHeader3.Width = 180;
			columnHeader4.Text = "Address";
			columnHeader4.Width = 100;
			columnHeader5.Text = "Form Type";
			columnHeader5.Width = 80;
			columnHeader6.Text = "Bill No";
			columnHeader6.Width = 70;
			columnHeader7.Text = "Qty";
			columnHeader23.Text = "Unit";
			columnHeader8.Text = "Rate";
			columnHeader8.Width = 80;
			columnHeader9.Text = "Disc";
			columnHeader11.Text = "Total";
			lblSecond.AutoSize = true;
			lblSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblSecond.ForeColor = System.Drawing.Color.Blue;
			lblSecond.Location = new System.Drawing.Point(5, 394);
			lblSecond.Name = "lblSecond";
			lblSecond.Size = new System.Drawing.Size(76, 17);
			lblSecond.TabIndex = 4;
			lblSecond.Text = "Purchase";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Location = new System.Drawing.Point(276, 73);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(70, 16);
			label5.TabIndex = 7;
			label5.Text = "Product: ";
			lblProduct.AutoSize = true;
			lblProduct.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblProduct.ForeColor = System.Drawing.Color.Black;
			lblProduct.Location = new System.Drawing.Point(342, 73);
			lblProduct.Name = "lblProduct";
			lblProduct.Size = new System.Drawing.Size(22, 16);
			lblProduct.TabIndex = 8;
			lblProduct.Text = "--";
			livPurchase.Columns.AddRange(new System.Windows.Forms.ColumnHeader[11]
			{
				columnHeader12, columnHeader13, columnHeader14, columnHeader15, columnHeader16, columnHeader17, columnHeader18, columnHeader19, columnHeader20, columnHeader21,
				columnHeader24
			});
			livPurchase.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			livPurchase.GridLines = true;
			livPurchase.HideSelection = false;
			livPurchase.Location = new System.Drawing.Point(10, 414);
			livPurchase.Name = "livPurchase";
			livPurchase.Size = new System.Drawing.Size(875, 171);
			livPurchase.TabIndex = 5;
			livPurchase.UseCompatibleStateImageBehavior = false;
			livPurchase.View = System.Windows.Forms.View.Details;
			columnHeader12.Text = "SL";
			columnHeader12.Width = 40;
			columnHeader13.Text = "Date";
			columnHeader13.Width = 80;
			columnHeader14.Text = "Party";
			columnHeader14.Width = 180;
			columnHeader15.Text = "Address";
			columnHeader15.Width = 100;
			columnHeader16.Text = "Form Type";
			columnHeader16.Width = 80;
			columnHeader17.Text = "Bill No";
			columnHeader17.Width = 70;
			columnHeader18.Text = "Qty";
			columnHeader19.Text = "Unit";
			columnHeader20.Text = "Rate";
			columnHeader20.Width = 80;
			columnHeader21.Text = "Disc";
			columnHeader24.Text = "Total";
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(163, 1);
			button4.TabIndex = 163;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 6);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(159, 24);
			lblHead.TabIndex = 162;
			lblHead.Text = "Product Tracing";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.CancelButton = btnEnd;
			base.ClientSize = new System.Drawing.Size(894, 592);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(lblProduct);
			base.Controls.Add(label5);
			base.Controls.Add(livPurchase);
			base.Controls.Add(livSales);
			base.Controls.Add(lblSecond);
			base.Controls.Add(lblFirst);
			base.Controls.Add(dtmTo);
			base.Controls.Add(label2);
			base.Controls.Add(dtmFrom);
			base.Controls.Add(label1);
			base.Controls.Add(txtParty);
			base.Controls.Add(cbParty);
			base.MaximizeBox = false;
			base.Name = "ProductTracing";
			base.Load += new System.EventHandler(ProductTracing_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ProductTracing_KeyDown);
			base.Controls.SetChildIndex(cbParty, 0);
			base.Controls.SetChildIndex(txtParty, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dtmFrom, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(dtmTo, 0);
			base.Controls.SetChildIndex(lblFirst, 0);
			base.Controls.SetChildIndex(lblSecond, 0);
			base.Controls.SetChildIndex(livSales, 0);
			base.Controls.SetChildIndex(livPurchase, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(lblProduct, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
