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
	public class RawmaterialMapping : BaseForm
	{
		private DBContext db;

		private List<ExRawmaterialMapping> exRawmaterials;

		private DataGridViewTextBoxEditingControl textbox;

		private IContainer components = null;

		private Label lblHead;

		private Panel panel1;

		public AshDataGridview dgvDetails;

		private Button btnCancel;

		private Button btnOk;

		private AshTextBoxNormal txtTotalCost;

		private Label label5;

		private BindingSource bdsDetails;

		private DataGridViewTextBoxColumn col_SL;

		private DataGridViewTextBoxColumn col_ProCode;

		private DataGridViewTextBoxColumn col_ProName;

		private DataGridViewTextBoxColumn col_Qty;

		private DataGridViewTextBoxColumn col_Unit;

		private DataGridViewTextBoxColumn col_Rate;

		private DataGridViewTextBoxColumn col_TotalAmount;

		public RawmaterialMapping(List<ExRawmaterialMapping> _exRawmaterials)
		{
			InitializeComponent();
			ColorChange.Color(this);
			panel1.BackColor = Settings.Default.BackColor;
			exRawmaterials = _exRawmaterials;
		}

		private void BindDgv()
		{
			dgvDetails.AutoGenerateColumns = false;
			bdsDetails.DataSource = null;
			bdsDetails.DataSource = exRawmaterials;
			dgvDetails.DataSource = bdsDetails;
		}

		private void RawmaterialMapping_Load(object sender, EventArgs e)
		{
			btnEnd.Visible = false;
			db = new DBContext();
			BindDgv();
			if (exRawmaterials != null)
			{
				txtTotalCost.Value = exRawmaterials.Sum((ExRawmaterialMapping x) => x.NetAmount).GetValueOrDefault();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			ExRawmaterialMapping t = (ExRawmaterialMapping)dgvDetails.CurrentRow.DataBoundItem;
			if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProCode.Name)
			{
				ProductEntity product = db.Products().Single((ProductEntity x) => x.ProductCode == t.ProCode);
				t.ProductName = product.ProductName;
				t.SL = dgvDetails.Rows.Count;
				t.Quantity = 1m;
				t.FK_UnitId = product.UnitId;
				t.UnitName = db.Units().Single((UnitEntity x) => x.UnitId == product.UnitId).UnitName;
				t.Rate = product.PurchasePrice;
				t.NetAmount = product.PurchasePrice;
				dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[3];
			}
			else if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
			{
				t.NetAmount = t.Quantity * t.Rate;
			}
			txtTotalCost.Value = Math.Round(exRawmaterials.Sum((ExRawmaterialMapping x) => x.NetAmount).GetValueOrDefault(), 0);
		}

		private void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (e.Control is DataGridViewTextBoxEditingControl)
			{
				textbox = (DataGridViewTextBoxEditingControl)e.Control;
				if (textbox != null)
				{
					textbox.KeyPress += Textbox_KeyPress;
					textbox.KeyUp += Textbox_KeyUp;
				}
			}
		}

		private void Textbox_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				textbox = (DataGridViewTextBoxEditingControl)sender;
				ExRawmaterialMapping t = (ExRawmaterialMapping)dgvDetails.CurrentRow.DataBoundItem;
				_ = dgvDetails.CurrentCell.RowIndex;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProName.Name)
				{
					if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
					{
						return;
					}
					e.Handled = true;
					SelectCustomer frm2 = new SelectCustomer("Raw_Material");
					frm2.sName = textbox.Text;
					if (frm2.ShowDialog() == DialogResult.OK)
					{
						dgvDetails.CurrentCell.Value = frm2.sName;
						ProductEntity product = db.Products().Single((ProductEntity x) => x.ProductCode == frm2.iId);
						t.SL = dgvDetails.Rows.Count - 1;
						t.FK_ProductId = product.ProductId.ToInt32();
						t.ProCode = product.ProductCode.toInt32();
						t.ProductName = product.ProductName;
						t.Quantity = 1m;
						t.FK_UnitId = product.UnitId;
						t.UnitName = db.Units().Single((UnitEntity x) => x.UnitId == product.UnitId).UnitName;
						t.Rate = product.PurchasePrice;
						t.NetAmount = product.PurchasePrice;
						txtTotalCost.Value = exRawmaterials.Sum((ExRawmaterialMapping x) => x.NetAmount).GetValueOrDefault();
						dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[3];
					}
				}
				else
				{
					if (!(dgvDetails.CurrentCell.OwningColumn.Name == col_Unit.Name))
					{
						return;
					}
					SelectCustomer frm = new SelectCustomer("Unit");
					frm.sName = textbox.Text;
					frm.ProductId = t.FK_ProductId.toInt32();
					if (frm.ShowDialog() == DialogResult.OK)
					{
						dgvDetails.CurrentCell.Value = frm.sName;
						t.Rate = (from x in db.Productunits()
							where x.UnitId == frm.iId && x.ProductId == t.FK_ProductId
							select x).FirstOrDefault().UnitQty * t.Rate;
						t.NetAmount = t.Rate * t.Quantity;
						txtTotalCost.Value = exRawmaterials.Sum((ExRawmaterialMapping x) => x.NetAmount).GetValueOrDefault();
						dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[5];
					}
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar.ToString();
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name)
				{
					if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || (e.KeyChar == '.' && !(sender as TextBox).Text.Contains('.')) || (e.KeyChar == '.' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength) || (e.KeyChar == '-' && !(sender as TextBox).Text.Contains('-')) || (e.KeyChar == '-' && (sender as TextBox).SelectionLength == (sender as TextBox).TextLength))
					{
						return;
					}
					e.Handled = true;
				}
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_ProCode.Name)
				{
					dgvDetails.NotifyCurrentCellDirty(dirty: true);
				}
			}
			catch (Exception ex)
			{
				Messages.ErrorMessage(ex.Message);
			}
		}

		private void dgvDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (dgvDetails.CurrentRow.DataBoundItem != null)
			{
				dgvDetails.CurrentRow.ErrorText = string.Empty;
				if (dgvDetails.CurrentCell.OwningColumn.Name == col_Qty.Name && e.FormattedValue.ToString().ToDouble() == 0.0)
				{
					dgvDetails.CurrentRow.ErrorText = "Quantity must be entered";
					e.Cancel = true;
				}
			}
		}

		private void RawmaterialMapping_FormClosing(object sender, FormClosingEventArgs e)
		{
			BindDgv();
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
			lblHead = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			dgvDetails = new AshUsercontrol.AshDataGridview();
			col_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_ProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			col_TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btnCancel = new System.Windows.Forms.Button();
			btnOk = new System.Windows.Forms.Button();
			txtTotalCost = new AshUsercontrol.AshTextBoxNormal();
			label5 = new System.Windows.Forms.Label();
			bdsDetails = new System.Windows.Forms.BindingSource(components);
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(637, 0);
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(1, 3);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(210, 24);
			lblHead.TabIndex = 129;
			lblHead.Text = "Rawmaterial Mapping";
			panel1.BackColor = System.Drawing.Color.DodgerBlue;
			panel1.Controls.Add(dgvDetails);
			panel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(8, 43);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(657, 343);
			panel1.TabIndex = 137;
			dgvDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
			dgvDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			dgvDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgvDetails.BackgroundColor = System.Drawing.Color.White;
			dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDetails.Columns.AddRange(col_SL, col_ProCode, col_ProName, col_Qty, col_Unit, col_Rate, col_TotalAmount);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightPink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvDetails.DefaultCellStyle = dataGridViewCellStyle2;
			dgvDetails.EnableHeadersVisualStyles = false;
			dgvDetails.EnterKeyNavigation = false;
			dgvDetails.LastKey = System.Windows.Forms.Keys.None;
			dgvDetails.Location = new System.Drawing.Point(9, 10);
			dgvDetails.Name = "dgvDetails";
			dgvDetails.RowHeadersVisible = false;
			dgvDetails.Size = new System.Drawing.Size(638, 321);
			dgvDetails.TabIndex = 0;
			dgvDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetails_CellEndEdit);
			dgvDetails.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(dgvDetails_CellValidating);
			dgvDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetails_DataError);
			dgvDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetails_EditingControlShowing);
			col_SL.DataPropertyName = "SL";
			col_SL.HeaderText = "SL";
			col_SL.Name = "col_SL";
			col_SL.ReadOnly = true;
			col_SL.Width = 35;
			col_ProCode.DataPropertyName = "ProCode";
			col_ProCode.HeaderText = "Pro Code";
			col_ProCode.Name = "col_ProCode";
			col_ProName.DataPropertyName = "ProductName";
			col_ProName.HeaderText = "Product Name";
			col_ProName.Name = "col_ProName";
			col_ProName.Width = 200;
			col_Qty.DataPropertyName = "Quantity";
			col_Qty.HeaderText = "Quantity";
			col_Qty.Name = "col_Qty";
			col_Qty.Width = 80;
			col_Unit.DataPropertyName = "UnitName";
			col_Unit.HeaderText = "Unit";
			col_Unit.Name = "col_Unit";
			col_Unit.Width = 40;
			col_Rate.DataPropertyName = "Rate";
			col_Rate.HeaderText = "Rate";
			col_Rate.Name = "col_Rate";
			col_Rate.ReadOnly = true;
			col_Rate.Width = 80;
			col_TotalAmount.DataPropertyName = "NetAmount";
			col_TotalAmount.HeaderText = "Net Amount";
			col_TotalAmount.Name = "col_TotalAmount";
			col_TotalAmount.ReadOnly = true;
			btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(592, 412);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 30);
			btnCancel.TabIndex = 139;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnOk.BackColor = System.Drawing.Color.DodgerBlue;
			btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnOk.ForeColor = System.Drawing.Color.White;
			btnOk.Location = new System.Drawing.Point(515, 412);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(75, 30);
			btnOk.TabIndex = 138;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = false;
			btnOk.Click += new System.EventHandler(btnOk_Click);
			txtTotalCost.BackColor = System.Drawing.Color.White;
			txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtTotalCost.Data = 0;
			txtTotalCost.EnterColor = System.Drawing.Color.LightPink;
			txtTotalCost.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtTotalCost.Format = null;
			txtTotalCost.isAllowNegative = false;
			txtTotalCost.isAllowSpecialChar = false;
			txtTotalCost.isNumberOnly = false;
			txtTotalCost.isNumeric = true;
			txtTotalCost.LeaveColor = System.Drawing.Color.White;
			txtTotalCost.Location = new System.Drawing.Point(109, 412);
			txtTotalCost.Margin = new System.Windows.Forms.Padding(4);
			txtTotalCost.Name = "txtTotalCost";
			txtTotalCost.ReadOnly = true;
			txtTotalCost.Size = new System.Drawing.Size(155, 16);
			txtTotalCost.TabIndex = 165;
			txtTotalCost.Text = "0";
			txtTotalCost.Value = new decimal(new int[4]);
			txtTotalCost.WaterMark = null;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(31, 412);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(58, 16);
			label5.TabIndex = 164;
			label5.Text = "Total  :";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(672, 450);
			base.Controls.Add(txtTotalCost);
			base.Controls.Add(label5);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnOk);
			base.Controls.Add(panel1);
			base.Controls.Add(lblHead);
			base.Name = "RawmaterialMapping";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(RawmaterialMapping_FormClosing);
			base.Load += new System.EventHandler(RawmaterialMapping_Load);
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnOk, 0);
			base.Controls.SetChildIndex(btnCancel, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(txtTotalCost, 0);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)bdsDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
