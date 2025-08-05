using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;
using AshproMessage;
using AshUsercontrol;

namespace Ashpro
{
	public class Shelfs : MasterForm
	{
		private DBContext db;

		private int? iShelfId;

		private IContainer components = null;

		private ColumnHeader Under;

		private ColumnHeader SrchAccountGroup;

		private GroupBox groupBox1;

		private ListView livShelf;

		private AshComboBox cmbDepot;

		private Label label3;

		private AshTextBoxNormal txtName;

		private Label label2;

		private ColumnHeader columnHeader3;

		public bool isOpenFromAnotherForm { get; set; }

		public string shelfName { get; set; }

		public string godwonName { get; set; } = string.Empty;


		public UserPrevilegeDtl plgDtls { get; set; }

		public Shelfs()
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
			livShelf.Items.Clear();
			try
			{
				foreach (SelfEntity x in db.Shelfs())
				{
					ListViewItem listViewItem = new ListViewItem((livShelf.Items.Count + 1).ToString());
					listViewItem.SubItems.Add(x.SelfName);
					listViewItem.SubItems.Add(db.Godowns().Find((GodownEntity y) => y.GodwnId == x.GodownId).GodownName);
					livShelf.Items.Add(listViewItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private SelfEntity EntityLoadMethod()
		{
			return new SelfEntity
			{
				SelfId = iShelfId,
				SelfName = txtName.Text,
				GodownId = cmbDepot.SelectedValue.ToInt32()
			};
		}

		private void Shelfs_Load(object sender, EventArgs e)
		{
			db = new DBContext();
			AniHelper.FillGodown(cmbDepot);
			loadItems();
			if (godwonName != string.Empty)
			{
				cmbDepot.Text = godwonName;
			}
			base.ActiveControl = txtName;
		}

		protected override void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtName.Text == string.Empty)
				{
					Messages.WarningMessage();
					return;
				}
				if (!iShelfId.HasValue)
				{
					if (plgDtls != null && !plgDtls.SaveData)
					{
						Messages.ErrorMessage("You have no authorisation to save the record. Please Contact the Admin.");
						return;
					}
					string text = txtName.Text;
					ListViewItem listViewItem = livShelf.FindItemWithText(text);
					if (listViewItem != null)
					{
						Messages.ReferenceExistsMessage();
						return;
					}
					if (AshproCommon.InsertToDatabaseObj(EntityLoadMethod(), "Self"))
					{
						Messages.SavedMessage();
					}
				}
				else
				{
					if (plgDtls != null && !plgDtls.EditData)
					{
						Messages.ErrorMessage("You have no authorisation to Update the record. Please Contact the Admin.");
						return;
					}
					if (AshproCommon.UpdateToDatabaseObj(EntityLoadMethod(), "Self", "SelfId", iShelfId.ToInt32()))
					{
						Messages.UpdateMessage();
					}
				}
				if (isOpenFromAnotherForm)
				{
					shelfName = txtName.Text;
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
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
				else if (Messages.DeleteConfirmationMessage() && AshproCommon.DeleteFromDatabase("Self", "SelfId", iShelfId.ToInt32()))
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
			txtName.Text = "";
			loadItems();
			cmbDepot.SelectedIndex = 0;
			btnDelete.Enabled = false;
			iShelfId = null;
			txtName.Focus();
		}

		private void cmbDepot_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
			}
		}

		private void livShelf_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (livShelf.SelectedItems.Count != 0)
			{
				SelfEntity selfEntity = db.Shelfs().Find((SelfEntity x) => x.SelfName == livShelf.SelectedItems[0].SubItems[1].Text);
				txtName.Text = selfEntity.SelfName;
				cmbDepot.SelectedValue = selfEntity.GodownId.GetValueOrDefault();
				iShelfId = selfEntity.SelfId;
				btnDelete.Enabled = true;
				btnDelete.ForeColor = Color.White;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.Shelfs));
			Under = new System.Windows.Forms.ColumnHeader();
			SrchAccountGroup = new System.Windows.Forms.ColumnHeader();
			groupBox1 = new System.Windows.Forms.GroupBox();
			livShelf = new System.Windows.Forms.ListView();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();
			cmbDepot = new AshUsercontrol.AshComboBox();
			label3 = new System.Windows.Forms.Label();
			txtName = new AshUsercontrol.AshTextBoxNormal();
			label2 = new System.Windows.Forms.Label();
			pnlButton.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			resources.ApplyResources(pnlButton, "pnlButton");
			resources.ApplyResources(pnlMaster, "pnlMaster");
			resources.ApplyResources(lblMasterTitle, "lblMasterTitle");
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnEnd.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(btnEnd, "btnEnd");
			resources.ApplyResources(Under, "Under");
			resources.ApplyResources(SrchAccountGroup, "SrchAccountGroup");
			groupBox1.BackColor = System.Drawing.Color.White;
			groupBox1.Controls.Add(livShelf);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			livShelf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { columnHeader3, SrchAccountGroup, Under });
			livShelf.FullRowSelect = true;
			livShelf.GridLines = true;
			livShelf.HideSelection = false;
			resources.ApplyResources(livShelf, "livShelf");
			livShelf.Name = "livShelf";
			livShelf.UseCompatibleStateImageBehavior = false;
			livShelf.View = System.Windows.Forms.View.Details;
			livShelf.SelectedIndexChanged += new System.EventHandler(livShelf_SelectedIndexChanged);
			resources.ApplyResources(columnHeader3, "columnHeader3");
			cmbDepot.BackColor = System.Drawing.Color.White;
			cmbDepot.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(cmbDepot, "cmbDepot");
			cmbDepot.FormattingEnabled = true;
			cmbDepot.LeaveColor = System.Drawing.Color.White;
			cmbDepot.Name = "cmbDepot";
			cmbDepot.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbDepot_KeyDown);
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			txtName.Data = 0;
			txtName.EnterColor = System.Drawing.Color.LightPink;
			resources.ApplyResources(txtName, "txtName");
			txtName.Format = null;
			txtName.isAllowNegative = false;
			txtName.isAllowSpecialChar = false;
			txtName.isNumberOnly = false;
			txtName.isNumeric = false;
			txtName.LeaveColor = System.Drawing.Color.White;
			txtName.Name = "txtName";
			txtName.Value = new decimal(new int[4]);
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(groupBox1);
			base.Controls.Add(label2);
			base.Controls.Add(cmbDepot);
			base.Controls.Add(txtName);
			base.Controls.Add(label3);
			base.MaximizeBox = false;
			base.Name = "Shelfs";
			base.Load += new System.EventHandler(Shelfs_Load);
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(txtName, 0);
			base.Controls.SetChildIndex(cmbDepot, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(groupBox1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
