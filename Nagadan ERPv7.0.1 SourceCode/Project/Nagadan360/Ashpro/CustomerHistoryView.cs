using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class CustomerHistoryView : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label lblTitle;

		private DataGridView dgvDetails;

		public int CustomerId { get; set; }

		public CustomerHistoryView()
		{
			InitializeComponent();
			DataTable dataTableWithIdParameter_SP = AshproCommon.GetDataTableWithIdParameter_SP("spCustomerHistory", new
			{
				NumberOfDecimalPlaces = 3,
				CustomerId = CustomerId,
				BranchId = Settings.Default.iBranchId
			});
			dgvDetails.DataSource = dataTableWithIdParameter_SP;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(566, 0);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblTitle.Location = new System.Drawing.Point(2, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(167, 20);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "Customer History";
            // 
            // dgvDetails
            // 
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(9, 71);
            this.dgvDetails.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(580, 285);
            this.dgvDetails.TabIndex = 15;
            // 
            // CustomerHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(38, 50);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.MaximumSize = new System.Drawing.Size(38, 50);
            this.MinimumSize = new System.Drawing.Size(38, 50);
            this.Name = "CustomerHistoryView";
            this.Text = "CustomerHistoryView";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.dgvDetails, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
