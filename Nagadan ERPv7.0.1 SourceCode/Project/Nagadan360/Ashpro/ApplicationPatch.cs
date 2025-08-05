using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class ApplicationPatch : BaseForm
	{
		private DBContext db = new DBContext();

		private IContainer components = null;

		private Label label5;

		private Label label1;

		private AshComboBox cmbType;

		private Button btnOK;

		public ApplicationPatch()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
		}

		private void MultiSizePatch()
		{
			AshproCommon.InsertMethod_SP(null, null, "spMultiSizePatch");
		}

		private void ProBarcodePatch()
		{
			AshproCommon.InsertMethod_SP(null, null, "spProductBarcodePatch");
		}

		private List<ProductPriceCategory> CheckSumMethod()
		{
			List<ProductPriceCategory> list = new List<ProductPriceCategory>();
			List<ProductPriceCategory> source = db.ProductPriceCategories();
			List<ProductUnit> list2 = db.Productunits().ToList();
			foreach (ProductUnit item in list2)
			{
				if (!source.Any((ProductPriceCategory x) => x.Barcode == item.Barcode))
				{
					ProductPriceCategory item2 = new ProductPriceCategory
					{
						Barcode = item.Barcode,
						PCategoryId = 1,
						PDisc = 0m,
						SPDisc = 0m,
						SPrice = item.SalesPrice,
						PPrice = 0m,
						PGroupId = 1,
						UnitId = item.UnitId,
						ProductId = item.ProductId,
						ProPrceCatId = null
					};
					list.Add(item2);
				}
			}
			return list;
		}

		private async void btnOK_Click(object sender, EventArgs e)
		{
			string sValue = AniHelper.InputBoxMethod(Utility.uManufacturer, "Please enter the Password", "Password :", isPassword: true);
			if (sValue == AniHelper.PWordGeneration())
			{
				new AshUsercontrol.ProgressBar(this);
				if (cmbType.SelectedIndex == 0)
				{
					Task task2 = new Task(MultiSizePatch);
					task2.Start();
					await task2;
				}
				else if (cmbType.SelectedIndex == 1)
				{
					Task task = new Task(ProBarcodePatch);
					task.Start();
					await task;
				}
				else if (cmbType.SelectedIndex == 2)
				{
					AshproCommon.InsertToDatabase((await Task.Run(() => CheckSumMethod())).Cast<object>().ToList());
				}
				Close();
			}
			else
			{
				Messages.ErrorMessage("Wrong Password!");
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
			label5 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			cmbType = new AshUsercontrol.AshComboBox();
			btnOK = new System.Windows.Forms.Button();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(314, 0);
			btnEnd.TabIndex = 6;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label5.Location = new System.Drawing.Point(2, 3);
			label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(172, 24);
			label5.TabIndex = 7;
			label5.Text = "Application Patch";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(47, 82);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(68, 13);
			label1.TabIndex = 2;
			label1.Text = "Patch Type :";
			cmbType.EnterColor = System.Drawing.Color.LightPink;
			cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbType.FormattingEnabled = true;
			cmbType.Items.AddRange(new object[3] { "Multi Size Patch", "Product Barcode Patch", "Product Price Category Patch" });
			cmbType.LeaveColor = System.Drawing.Color.White;
			cmbType.Location = new System.Drawing.Point(126, 73);
			cmbType.Name = "cmbType";
			cmbType.Size = new System.Drawing.Size(149, 21);
			cmbType.TabIndex = 3;
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOK.BackColor = System.Drawing.Color.DodgerBlue;
			btnOK.FlatAppearance.BorderSize = 0;
			btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			btnOK.ForeColor = System.Drawing.Color.White;
			btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnOK.Location = new System.Drawing.Point(262, 131);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 30);
			btnOK.TabIndex = 4;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = false;
			btnOK.Click += new System.EventHandler(btnOK_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(348, 173);
			base.Controls.Add(btnOK);
			base.Controls.Add(cmbType);
			base.Controls.Add(label1);
			base.Controls.Add(label5);
			base.Name = "ApplicationPatch";
			Text = "ApplicationPatch";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(cmbType, 0);
			base.Controls.SetChildIndex(btnOK, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
