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
	public class ShowProduct : BaseForm
	{
		private IContainer components = null;

		private Label label1;

		private Button button2;

		private Label lblTitle;

		private AshNumericLabel lblTotal;

		public AshDataGridview dgDetails;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_Barcode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_HSN;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_Gross;

		private DataGridViewTextBoxColumn col_Dis;

		private DataGridViewTextBoxColumn col_Discount;

		private DataGridViewTextBoxColumn col_Taxable;

		private DataGridViewTextBoxColumn col_Vat;

		private DataGridViewTextBoxColumn col_VatAmnt;

		private DataGridViewTextBoxColumn col_GST;

		private DataGridViewTextBoxColumn col_GSTAmnt;

		private DataGridViewTextBoxColumn col_SGST;

		private DataGridViewTextBoxColumn col_SGSTAmnt;

		private DataGridViewTextBoxColumn col_CGST;

		private DataGridViewTextBoxColumn col_CGSTAmnt;

		private DataGridViewTextBoxColumn col_IGST;

		private DataGridViewTextBoxColumn col_IGSTAmnt;

		private DataGridViewTextBoxColumn col_NetAmnt;

		public int SalesId { get; set; }

		public decimal TotalAmount { get; set; }

		public ShowProduct()
		{
			InitializeComponent();
			ColorChange.Color(this);
			col_Qty.DefaultCellStyle.Format = CommonSettings.strQuantityFormat;
			col_Rate.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Gross.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Dis.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Discount.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Taxable.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_Vat.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_VatAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_SGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_CGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_IGSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GST.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_GSTAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
			col_NetAmnt.DefaultCellStyle.Format = CommonSettings.strCurencyFormat;
		}

		private void ShowProduct_Load(object sender, EventArgs e)
		{
			lblTotal.Value = TotalAmount;
			List<SalesDetails> list = new List<SalesDetails>();
			List<Transaction> list2 = new List<Transaction>();
			DBContext dBContext = new DBContext();
			list = dBContext.SalesDetails(new
			{
				SalesInvId = SalesId
			}).ToList();
			list2 = POSHelper.GetTransactionsFromSDetails(list, dBContext);
			dgDetails.AutoGenerateColumns = false;
			dgDetails.DataSource = list2;
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			Close();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			label1 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			lblTitle = new System.Windows.Forms.Label();
			lblTotal = new AshUsercontrol.AshNumericLabel();
			dgDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_HSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Dis = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Taxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_VatAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_GST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_GSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_SGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_CGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_IGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_IGSTAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_NetAmnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dgDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(1046, 0);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.Red;
			label1.Location = new System.Drawing.Point(192, 384);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(78, 13);
			label1.TabIndex = 61;
			label1.Text = "Bill Amount: ";
			button2.Location = new System.Drawing.Point(2, 29);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(160, 1);
			button2.TabIndex = 63;
			button2.UseVisualStyleBackColor = true;
			lblTitle.AutoSize = true;
			lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTitle.Location = new System.Drawing.Point(8, 5);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(150, 24);
			lblTitle.TabIndex = 62;
			lblTitle.Text = "Product Details";
			lblTotal.AutoSize = true;
			lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTotal.ForeColor = System.Drawing.Color.Red;
			lblTotal.Format = null;
			lblTotal.Location = new System.Drawing.Point(276, 384);
			lblTotal.Name = "lblTotal";
			lblTotal.Size = new System.Drawing.Size(16, 16);
			lblTotal.TabIndex = 65;
			lblTotal.Text = "0";
			lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			lblTotal.Value = new decimal(new int[4]);
			dgDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgDetails.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(61, 77, 125);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgDetails.ColumnHeadersHeight = 40;
			dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgDetails.Columns.AddRange(col_SL, col_ProCode, col_Barcode, col_ProName, col_HSN, col_Qty, col_Unit, col_Rate, col_Gross, col_Dis, col_Discount, col_Taxable, col_Vat, col_VatAmnt, col_GST, col_GSTAmnt, col_SGST, col_SGSTAmnt, col_CGST, col_CGSTAmnt, col_IGST, col_IGSTAmnt, col_NetAmnt);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgDetails.DefaultCellStyle = dataGridViewCellStyle3;
			dgDetails.EnableHeadersVisualStyles = false;
			dgDetails.EnterKeyNavigation = false;
			dgDetails.LastKey = System.Windows.Forms.Keys.None;
			dgDetails.Location = new System.Drawing.Point(2, 53);
			dgDetails.Name = "dgDetails";
			dgDetails.RowHeadersVisible = false;
			dgDetails.RowTemplate.Height = 25;
			dgDetails.Size = new System.Drawing.Size(1077, 328);
			dgDetails.TabIndex = 84;
			col_SL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 40;
			col_ProCode.DataPropertyName = "ProductCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProCode.Visible = false;
			col_Barcode.DataPropertyName = "Barcode";
			col_Barcode.HeaderText = "Barcode";
			col_Barcode.Name = "col_Barcode";
			col_ProName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_ProName.DataPropertyName = "ProductName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.Width = 150;
			col_HSN.DataPropertyName = "Hsn";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_HSN.DefaultCellStyle = dataGridViewCellStyle4;
			col_HSN.HeaderText = "HSN";
			col_HSN.Name = "col_HSN";
			col_HSN.Visible = false;
			col_Qty.DataPropertyName = "Quantity";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Qty.DefaultCellStyle = dataGridViewCellStyle5;
			col_Qty.HeaderText = "Qty";
			col_Qty.Name = "col_Qty";
			col_Unit.DataPropertyName = "Unit";
			col_Unit.HeaderText = "Unit";
			col_Unit.Name = "col_Unit";
			col_Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Rate.DefaultCellStyle = dataGridViewCellStyle6;
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_Gross.DataPropertyName = "GrossAmount";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Gross.DefaultCellStyle = dataGridViewCellStyle7;
			col_Gross.HeaderText = "Gross Amnt";
			col_Gross.Name = "col_Gross";
			col_Gross.ReadOnly = true;
			col_Dis.DataPropertyName = "Discount";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Dis.DefaultCellStyle = dataGridViewCellStyle8;
			col_Dis.HeaderText = "Dis%";
			col_Dis.Name = "col_Dis";
			col_Discount.DataPropertyName = "DiscountAmount";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Discount.DefaultCellStyle = dataGridViewCellStyle9;
			col_Discount.HeaderText = "Discount";
			col_Discount.Name = "col_Discount";
			col_Discount.ReadOnly = true;
			col_Taxable.DataPropertyName = "Taxable";
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_Taxable.DefaultCellStyle = dataGridViewCellStyle10;
			col_Taxable.HeaderText = "Taxable Amnt";
			col_Taxable.Name = "col_Taxable";
			col_Taxable.ReadOnly = true;
			col_Vat.DataPropertyName = "Vat";
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_Vat.DefaultCellStyle = dataGridViewCellStyle11;
			col_Vat.HeaderText = "VAT";
			col_Vat.Name = "col_Vat";
			col_Vat.Visible = false;
			col_VatAmnt.DataPropertyName = "VatAmount";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_VatAmnt.DefaultCellStyle = dataGridViewCellStyle12;
			col_VatAmnt.HeaderText = "VAT Amount";
			col_VatAmnt.Name = "col_VatAmnt";
			col_VatAmnt.ReadOnly = true;
			col_VatAmnt.Visible = false;
			col_GST.DataPropertyName = "Gst";
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_GST.DefaultCellStyle = dataGridViewCellStyle13;
			col_GST.HeaderText = "GST(%)";
			col_GST.Name = "col_GST";
			col_GST.Visible = false;
			col_GSTAmnt.DataPropertyName = "GstAmount";
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_GSTAmnt.DefaultCellStyle = dataGridViewCellStyle14;
			col_GSTAmnt.HeaderText = "GST Amount";
			col_GSTAmnt.Name = "col_GSTAmnt";
			col_GSTAmnt.ReadOnly = true;
			col_GSTAmnt.Visible = false;
			col_SGST.DataPropertyName = "Sgst";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_SGST.DefaultCellStyle = dataGridViewCellStyle15;
			col_SGST.HeaderText = "SGST(%)";
			col_SGST.Name = "col_SGST";
			col_SGST.ReadOnly = true;
			col_SGST.Visible = false;
			col_SGSTAmnt.DataPropertyName = "SGstAmount";
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_SGSTAmnt.DefaultCellStyle = dataGridViewCellStyle16;
			col_SGSTAmnt.HeaderText = "SGST(Rs)";
			col_SGSTAmnt.Name = "col_SGSTAmnt";
			col_SGSTAmnt.ReadOnly = true;
			col_SGSTAmnt.Visible = false;
			col_CGST.DataPropertyName = "CGst";
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_CGST.DefaultCellStyle = dataGridViewCellStyle17;
			col_CGST.HeaderText = "CGST(%)";
			col_CGST.Name = "col_CGST";
			col_CGST.ReadOnly = true;
			col_CGST.Visible = false;
			col_CGSTAmnt.DataPropertyName = "CGstAmount";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_CGSTAmnt.DefaultCellStyle = dataGridViewCellStyle18;
			col_CGSTAmnt.HeaderText = "CGST(Rs)";
			col_CGSTAmnt.Name = "col_CGSTAmnt";
			col_CGSTAmnt.ReadOnly = true;
			col_CGSTAmnt.Visible = false;
			col_IGST.DataPropertyName = "IGst";
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			col_IGST.DefaultCellStyle = dataGridViewCellStyle19;
			col_IGST.HeaderText = "IGST(%)";
			col_IGST.Name = "col_IGST";
			col_IGST.ReadOnly = true;
			col_IGST.Visible = false;
			col_IGSTAmnt.DataPropertyName = "IGstAmount";
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_IGSTAmnt.DefaultCellStyle = dataGridViewCellStyle20;
			col_IGSTAmnt.HeaderText = "IGST(Rs)";
			col_IGSTAmnt.Name = "col_IGSTAmnt";
			col_IGSTAmnt.ReadOnly = true;
			col_IGSTAmnt.Visible = false;
			col_NetAmnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			col_NetAmnt.DataPropertyName = "NETAmount";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			col_NetAmnt.DefaultCellStyle = dataGridViewCellStyle21;
			col_NetAmnt.HeaderText = "Net Amount";
			col_NetAmnt.Name = "col_NetAmnt";
			col_NetAmnt.ReadOnly = true;
			col_NetAmnt.Width = 90;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1081, 412);
			base.Controls.Add(dgDetails);
			base.Controls.Add(lblTotal);
			base.Controls.Add(button2);
			base.Controls.Add(lblTitle);
			base.Controls.Add(label1);
			base.MaximizeBox = false;
			base.Name = "ShowProduct";
			base.Load += new System.EventHandler(ShowProduct_Load);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(lblTitle, 0);
			base.Controls.SetChildIndex(button2, 0);
			base.Controls.SetChildIndex(lblTotal, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(dgDetails, 0);
			((System.ComponentModel.ISupportInitialize)dgDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
