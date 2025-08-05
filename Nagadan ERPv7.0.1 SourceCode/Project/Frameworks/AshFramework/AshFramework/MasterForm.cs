using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshFramework
{
	public class MasterForm : BaseForm
	{
		private IContainer components = null;

		public Panel pnlButton;

		public MasterGradientPanel pnlMaster;

		public Label lblMasterTitle;

		public Button btnSave;

		public Button btnDelete;

		public Button btnClear;

		private ToolTip ttMaster;

		public Button btnSearch;

		public MasterForm()
		{
			InitializeComponent();
		}

		protected virtual void MasterForm_Load(object sender, EventArgs e)
		{
		}

		protected virtual void btnSave_Click(object sender, EventArgs e)
		{
		}

		protected virtual void btnDelete_Click(object sender, EventArgs e)
		{
		}

		protected virtual void btnClear_Click(object sender, EventArgs e)
		{
		}

		protected virtual void btnSearch_Click(object sender, EventArgs e)
		{
		}

		private void MasterForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				btnSave_Click(null, null);
			}
			else if (e.KeyCode == Keys.D && e.Alt)
			{
				btnDelete_Click(null, null);
			}
			else if (e.KeyCode == Keys.N && e.Alt)
			{
				btnClear_Click(null, null);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshFramework.MasterForm));
			pnlButton = new System.Windows.Forms.Panel();
			btnSave = new System.Windows.Forms.Button();
			btnDelete = new System.Windows.Forms.Button();
			btnSearch = new System.Windows.Forms.Button();
			btnClear = new System.Windows.Forms.Button();
			pnlMaster = new AshFramework.MasterGradientPanel();
			lblMasterTitle = new System.Windows.Forms.Label();
			ttMaster = new System.Windows.Forms.ToolTip(components);
			pnlButton.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(527, 0);
			pnlButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			pnlButton.Controls.Add(btnSave);
			pnlButton.Controls.Add(btnDelete);
			pnlButton.Controls.Add(btnSearch);
			pnlButton.Controls.Add(btnClear);
			pnlButton.Location = new System.Drawing.Point(203, 420);
			pnlButton.Name = "pnlButton";
			pnlButton.Size = new System.Drawing.Size(332, 50);
			pnlButton.TabIndex = 11;
			btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Image = (System.Drawing.Image)resources.GetObject("btnSave.Image");
			btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSave.Location = new System.Drawing.Point(3, 2);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 45);
			btnSave.TabIndex = 0;
			btnSave.Text = "Save";
			btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnDelete.BackColor = System.Drawing.Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f);
			btnDelete.ForeColor = System.Drawing.Color.White;
			btnDelete.Image = (System.Drawing.Image)resources.GetObject("btnDelete.Image");
			btnDelete.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnDelete.Location = new System.Drawing.Point(83, 2);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new System.Drawing.Size(75, 45);
			btnDelete.TabIndex = 1;
			btnDelete.Text = "Delete";
			btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new System.EventHandler(btnDelete_Click);
			btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
			btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold);
			btnSearch.ForeColor = System.Drawing.Color.White;
			btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnSearch.Location = new System.Drawing.Point(244, 2);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new System.Drawing.Size(83, 45);
			btnSearch.TabIndex = 2;
			btnSearch.Text = "Search";
			btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnSearch, "Press Alt + R");
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Visible = false;
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
			btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnClear.BackColor = System.Drawing.Color.DodgerBlue;
			btnClear.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Bold);
			btnClear.ForeColor = System.Drawing.Color.White;
			btnClear.Image = (System.Drawing.Image)resources.GetObject("btnClear.Image");
			btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnClear.Location = new System.Drawing.Point(163, 2);
			btnClear.Name = "btnClear";
			btnClear.Size = new System.Drawing.Size(75, 45);
			btnClear.TabIndex = 2;
			btnClear.Text = "New";
			btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnClear, "Press Alt + N");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new System.EventHandler(btnClear_Click);
			pnlMaster.AllowMultiSelect = false;
			pnlMaster.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			pnlMaster.Angle = 0f;
			pnlMaster.BackColor = System.Drawing.Color.DodgerBlue;
			pnlMaster.BottomColor = System.Drawing.Color.DodgerBlue;
			pnlMaster.Location = new System.Drawing.Point(1, 41);
			pnlMaster.Name = "pnlMaster";
			pnlMaster.Selected = false;
			pnlMaster.Size = new System.Drawing.Size(150, 442);
			pnlMaster.TabIndex = 12;
			pnlMaster.TextAdjestmentHeight = 0;
			pnlMaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			pnlMaster.TopColor = System.Drawing.Color.White;
			lblMasterTitle.AutoSize = true;
			lblMasterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblMasterTitle.Location = new System.Drawing.Point(4, 7);
			lblMasterTitle.Name = "lblMasterTitle";
			lblMasterTitle.Size = new System.Drawing.Size(118, 24);
			lblMasterTitle.TabIndex = 13;
			lblMasterTitle.Text = "Master Title";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(562, 482);
			base.Controls.Add(lblMasterTitle);
			base.Controls.Add(pnlMaster);
			base.Controls.Add(pnlButton);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MasterForm";
			base.Load += new System.EventHandler(MasterForm_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(MasterForm_KeyDown);
			base.Controls.SetChildIndex(pnlButton, 0);
			base.Controls.SetChildIndex(pnlMaster, 0);
			base.Controls.SetChildIndex(lblMasterTitle, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
