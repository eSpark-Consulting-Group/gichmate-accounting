using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class Route : MasterForm
	{
		private int? iRoutId;

		private DBContext db;

		private IContainer components = null;

		private GroupBox groupBox1;

		private ListView LivDetails;

		private ColumnHeader SrchAccountGroup;

		private AshTextBoxNormal txtName;

		private Label label2;

		private AshComboBox cmbSalesman;

		private Label label3;

		private AshComboBox cmbDay;

		private Label label1;

		private ColumnHeader columnHeader2;

		private AshComboBox cmbVehicle;

		private Label label5;

		private AshComboBox cmbDriver;

		private Label label4;

		public UserPrevilegeDtl plgDtls { get; set; }

		public Route()
		{
			Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language));
			InitializeComponent();
			ColorChange.Color(this);
			TranslatorHelper.LoadArabic(this);
			TranslatorHelper.ChangeToArabic(this);
			btnSave.Text = MessageKeys.SaveButton;
			btnDelete.Text = MessageKeys.DeleteButton;
			btnClear.Text = MessageKeys.NewButton;
			btnSearch.Text = MessageKeys.Search;
		}

		private void loadItems()
		{
			LivDetails.Items.Clear();
			try
			{
				foreach (RouteEntity item in db.Routes())
				{
					ListViewItem listViewItem = new ListViewItem((LivDetails.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(item.RootName);
					LivDetails.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private RouteEntity EntitMethod()
		{
			return new RouteEntity
			{
				RootId = iRoutId,
				RootName = txtName.Text,
				SalesManId = cmbSalesman.SelectedValue.ToInt32(),
				DriverId = cmbDriver.SelectedValue.ToInt32(),
				WeekDay = cmbDay.Text,
				VehicleId = cmbVehicle.SelectedValue.ToInt32()
			};
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (LivDetails.SelectedItems.Count != 0)
			{
				RouteEntity routeEntity = (from x in db.Routes()
					where x.RootName == LivDetails.SelectedItems[0].SubItems[1].Text
					select x).FirstOrDefault();
				txtName.Text = routeEntity.RootName;
				iRoutId = routeEntity.RootId;
				cmbDay.Text = routeEntity.WeekDay;
				cmbSalesman.SelectedValue = routeEntity.SalesManId.GetValueOrDefault();
				cmbDriver.SelectedValue = routeEntity.DriverId.GetValueOrDefault();
				cmbVehicle.SelectedValue = routeEntity.VehicleId.GetValueOrDefault();
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
			}
		}

		private void Root_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			loadItems();
			cmbDay.SelectedIndex = 0;
			AniHelper.FillEmployees(cmbSalesman);
			AniHelper.FillEmployees(cmbDriver);
			AniHelper.FillVehicle(cmbVehicle);
			base.ActiveControl = txtName;
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
				}
				else if (!iRoutId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					string text = txtName.Text;
					ListViewItem listViewItem = LivDetails.FindItemWithText(text);
					if (listViewItem != null)
					{
						MessageBox.Show("Already Exist", Utility.uManufacturer);
					}
					else if (AshproCommon.InsertToDatabaseObj(EntitMethod(), "Root"))
					{
						Messages.SavedMessage();
						btnClear_Click(null, null);
					}
				}
				else if (plgDtls != null && !plgDtls.EditData)
				{
					Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
				}
				else if (AshproCommon.UpdateToDatabaseObj(EntitMethod(), "Root", "RootId", iRoutId.ToInt32()))
				{
					Messages.UpdateMessage();
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (plgDtls != null && !plgDtls.DeleteData)
				{
					Messages.ErrorMessage("You have no authorisation to Delete the record. Please Contact the Admin.");
				}
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Root", "RootId", iRoutId.ToInt32()))
				{
					btnClear_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		protected override void btnClear_Click(object sender, EventArgs e)
		{
			loadItems();
			txtName.Text = "";
			btnDelete.Enabled = false;
			iRoutId = null;
			txtName.Focus();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Route));
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			groupBox1 = new System.Windows.Forms.GroupBox();
			LivDetails = new System.Windows.Forms.ListView();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			SrchAccountGroup = new System.Windows.Forms.ColumnHeader();
			label2 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			cmbVehicle = new AshUsercontrol.AshComboBox();
			cmbSalesman = new AshUsercontrol.AshComboBox();
			cmbDay = new AshUsercontrol.AshComboBox();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			cmbDriver = new AshUsercontrol.AshComboBox();
			pnlButton.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(pnlButton, "pnlButton");
			resources.ApplyResources(pnlMaster, "pnlMaster");
			resources.ApplyResources(lblMasterTitle, "lblMasterTitle");
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnDelete, "btnDelete");
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnClear, "btnClear");
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnSearch, "btnSearch");
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.BackColor = System.Drawing.Color.White;
			groupBox1.Controls.Add(LivDetails);
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			resources.ApplyResources(LivDetails, "LivDetails");
			LivDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { columnHeader2, SrchAccountGroup });
			LivDetails.FullRowSelect = true;
			LivDetails.GridLines = true;
			LivDetails.HideSelection = false;
			LivDetails.Name = "LivDetails";
			LivDetails.UseCompatibleStateImageBehavior = false;
			LivDetails.View = System.Windows.Forms.View.Details;
			LivDetails.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
			resources.ApplyResources(columnHeader2, "columnHeader2");
			resources.ApplyResources(SrchAccountGroup, "SrchAccountGroup");
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			resources.ApplyResources(cmbVehicle, "cmbVehicle");
			cmbVehicle.EnterColor = System.Drawing.Color.LightPink;
			cmbVehicle.FormattingEnabled = true;
			cmbVehicle.Items.AddRange(new object[1] { resources.GetString("cmbVehicle.Items") });
			cmbVehicle.LeaveColor = System.Drawing.Color.White;
			cmbVehicle.Name = "cmbVehicle";
			resources.ApplyResources(cmbSalesman, "cmbSalesman");
			cmbSalesman.EnterColor = System.Drawing.Color.LightPink;
			cmbSalesman.FormattingEnabled = true;
			cmbSalesman.Items.AddRange(new object[1] { resources.GetString("cmbSalesman.Items") });
			cmbSalesman.LeaveColor = System.Drawing.Color.White;
			cmbSalesman.Name = "cmbSalesman";
			resources.ApplyResources(cmbDay, "cmbDay");
			cmbDay.EnterColor = System.Drawing.Color.LightPink;
			cmbDay.FormattingEnabled = true;
			cmbDay.Items.AddRange(new object[7]
			{
				resources.GetString("cmbDay.Items"),
				resources.GetString("cmbDay.Items1"),
				resources.GetString("cmbDay.Items2"),
				resources.GetString("cmbDay.Items3"),
				resources.GetString("cmbDay.Items4"),
				resources.GetString("cmbDay.Items5"),
				resources.GetString("cmbDay.Items6")
			});
			cmbDay.LeaveColor = System.Drawing.Color.White;
			cmbDay.Name = "cmbDay";
			resources.ApplyResources(txtName, "txtName");
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Name = "txtName";
			txtName.Value = new decimal(new int[4]);
			resources.ApplyResources(cmbDriver, "cmbDriver");
			cmbDriver.EnterColor = System.Drawing.Color.LightPink;
			cmbDriver.FormattingEnabled = true;
			cmbDriver.Items.AddRange(new object[1] { resources.GetString("cmbDriver.Items") });
			cmbDriver.LeaveColor = System.Drawing.Color.White;
			cmbDriver.Name = "cmbDriver";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(cmbVehicle);
			base.Controls.Add(label5);
			base.Controls.Add(txtName);
			base.Controls.Add(groupBox1);
			base.Controls.Add(cmbDriver);
			base.Controls.Add(label2);
			base.Controls.Add(label4);
			base.Controls.Add(label1);
			base.Controls.Add(cmbSalesman);
			base.Controls.Add(cmbDay);
			base.Controls.Add(label3);
			base.MaximizeBox = false;
			base.Name = "Route";
			base.Load += new System.EventHandler(Root_Load);
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(cmbDay, 0);
			base.Controls.SetChildIndex(cmbSalesman, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(cmbDriver, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(cmbVehicle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
