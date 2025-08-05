using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using System.Globalization;
using System.Threading;
using Ashpro.Properties;

namespace Ashpro
{
	public class POSProductView : BaseForm
	{
		private Button btnGroup;

		private Button btnProduct;

		private DataTable dataTable;

		private int intYLocGroup = 12;

		private int iXProLocation = 12;

		private int iYProLocation = 12;

		private byte[] imageBt = null;

		private POSView POSView;

		private bool isPosDashboard;

		private int iIndex = 0;

		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label label25;

		private Panel pnlProduct;

		private Panel pnlGroup;

		private Button btnDown;

		private Button btnUp;

		public List<POSDetails> entPOSDetails { get; set; }

		public int iPCategoryID { get; set; }

		public POSProductView(Form _pOSView, bool _isPosDashboard = false)
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			isPosDashboard = _isPosDashboard;
			if (!isPosDashboard)
			{
				POSView = (POSView)_pOSView;
			}
		}

		protected override void btnEnd_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void POSProductView_Load(object sender, EventArgs e)
		{
			bool flag = true;
			int iPGrpId = 0;
			dataTable = AshproCommon.GetDataTable("Select PGroupId,PGroupName From ProductGroup");
			foreach (DataRow row in dataTable.Rows)
			{
				if (flag)
				{
					iPGrpId = row[0].ToInt32();
					flag = false;
				}
				btnGroup = new Button();
				btnGroup.Name = row[1].ToString();
				btnGroup.Text = row[1].ToString();
				btnGroup.Tag = row[0].ToString();
				btnGroup.BackColor = Color.LightGray;
				btnGroup.FlatStyle = FlatStyle.Flat;
				btnGroup.ForeColor = Color.Black;
				btnGroup.Size = new Size(130, 95);
				btnGroup.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
				btnGroup.Location = new Point(5, intYLocGroup);
				btnGroup.Click += BtnGroup_Click;
				pnlGroup.Controls.Add(btnGroup);
				intYLocGroup += 100;
			}
			ProductLoadMethod(iPGrpId);
		}

		private void BtnGroup_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			ProductLoadMethod(button.Tag.ToInt32());
			iIndex = button.TabIndex;
		}

		private void ProductLoadMethod(int iPGrpId)
		{
			pnlProduct.Controls.Clear();
			iYProLocation = 5;
			iXProLocation = 5;
			dataTable = AshproCommon.GetDataTable("Select ProductId,ProductName,image From Product Where PGroupId='" + iPGrpId + "'");
			foreach (DataRow row in dataTable.Rows)
			{
				btnProduct = new Button();
				btnProduct.Tag = row[0].ToString();
				btnProduct.Name = row[1].ToString();
				btnProduct.Text = row[1].ToString();
				btnProduct.FlatStyle = FlatStyle.Flat;
				if (row[2] != DBNull.Value)
				{
					imageBt = (byte[])row[2];
					MemoryStream stream = new MemoryStream(imageBt);
					btnProduct.BackgroundImage = Image.FromStream(stream);
					btnProduct.BackgroundImageLayout = ImageLayout.Zoom;
					btnProduct.TextAlign = ContentAlignment.BottomCenter;
					btnProduct.BackColor = Color.White;
				}
				else
				{
					btnProduct.BackColor = Color.DodgerBlue;
				}
				btnProduct.ForeColor = Color.White;
				btnProduct.Size = new Size(135, 95);
				btnProduct.Location = new Point(iXProLocation, iYProLocation);
				btnProduct.Font = new Font("MS Reference Sans Serif", 10f, FontStyle.Bold);
				btnProduct.Click += BtnProduct_Click;
				pnlProduct.Controls.Add(btnProduct);
				if (iXProLocation >= 600)
				{
					iXProLocation = 5;
					iYProLocation += 120;
				}
				else
				{
					iXProLocation += 155;
				}
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (pnlGroup.Controls.Count > 0)
			{
				Button button = new Button();
				if (iIndex < pnlGroup.Controls.Count - 1)
				{
					pnlGroup.SelectNextControl(pnlGroup.Controls[iIndex], forward: true, tabStopOnly: false, nested: false, wrap: false);
					iIndex++;
					button = (Button)pnlGroup.Controls[iIndex];
					ProductLoadMethod(button.Tag.ToInt32());
				}
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (pnlGroup.Controls.Count > 0 && iIndex != 0)
			{
				pnlGroup.SelectNextControl(pnlGroup.Controls[iIndex], forward: false, tabStopOnly: false, nested: false, wrap: false);
				iIndex--;
				Button button = (Button)pnlGroup.Controls[iIndex];
				ProductLoadMethod(button.Tag.ToInt32());
			}
		}

		private void BtnProduct_Click(object sender, EventArgs e)
		{
			try
			{
				Button button = (Button)sender;
				ProductEntity productEntity = db.Products(new
				{
					ProductId = button.Tag.ToInt32()
				}).FirstOrDefault();
				int pOSDetails = POSHelper.GetPOSDetails(productEntity, entPOSDetails, iPCategoryID);
				if (!isPosDashboard)
				{
					POSView.DgvLoadMethod(entPOSDetails, pOSDetails);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSProductView));
            this.label25 = new System.Windows.Forms.Label();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            resources.ApplyResources(this.btnEnd, "btnEnd");
            this.btnEnd.FlatAppearance.BorderSize = 0;
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // pnlProduct
            // 
            resources.ApplyResources(this.pnlProduct, "pnlProduct");
            this.pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProduct.Name = "pnlProduct";
            // 
            // pnlGroup
            // 
            resources.ApplyResources(this.pnlGroup, "pnlGroup");
            this.pnlGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGroup.Name = "pnlGroup";
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // POSProductView
            // 
            this.AcceptButton = this.btnDown;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.pnlGroup);
            this.Controls.Add(this.pnlProduct);
            this.Controls.Add(this.label25);
            this.Name = "POSProductView";
            this.Load += new System.EventHandler(this.POSProductView_Load);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.pnlProduct, 0);
            this.Controls.SetChildIndex(this.pnlGroup, 0);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.btnDown, 0);
            this.Controls.SetChildIndex(this.btnUp, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
