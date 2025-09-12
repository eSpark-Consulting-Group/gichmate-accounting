using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsLibraryFramework
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
			components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MasterForm));
			pnlButton = new Panel();
			btnSave = new Button();
			btnDelete = new Button();
			btnSearch = new Button();
			btnClear = new Button();
			pnlMaster = new MasterGradientPanel();
			lblMasterTitle = new Label();
			ttMaster = new ToolTip(components);
			pnlButton.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new Point(527, 0);
			pnlButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			pnlButton.Controls.Add(btnSave);
			pnlButton.Controls.Add(btnDelete);
			pnlButton.Controls.Add(btnSearch);
			pnlButton.Controls.Add(btnClear);
			pnlButton.Location = new Point(203, 420);
			pnlButton.Name = "pnlButton";
			pnlButton.Size = new Size(332, 50);
			pnlButton.TabIndex = 11;
			btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnSave.BackColor = Color.DodgerBlue;
			btnSave.FlatAppearance.BorderColor = Color.LightGray;
			btnSave.FlatAppearance.BorderSize = 0;
			btnSave.FlatStyle = FlatStyle.Flat;
			btnSave.Font = new Font("Microsoft Sans Serif", 13f);
			btnSave.ForeColor = Color.White;
			btnSave.Image = (Image)resources.GetObject("btnSave.Image");
			btnSave.ImageAlign = ContentAlignment.BottomCenter;
			btnSave.ImeMode = ImeMode.NoControl;
			btnSave.Location = new Point(3, 2);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(75, 45);
			btnSave.TabIndex = 0;
			btnSave.Text = "Save";
			btnSave.TextImageRelation = TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnSave, "Press Alt + S");
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new EventHandler(btnSave_Click);
			btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnDelete.BackColor = Color.DodgerBlue;
			btnDelete.Enabled = false;
			btnDelete.FlatAppearance.BorderColor = Color.LightGray;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Microsoft Sans Serif", 13f);
			btnDelete.ForeColor = Color.White;
			btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
			btnDelete.ImageAlign = ContentAlignment.BottomCenter;
			btnDelete.ImeMode = ImeMode.NoControl;
			btnDelete.Location = new Point(83, 2);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(75, 45);
			btnDelete.TabIndex = 1;
			btnDelete.Text = "Delete";
			btnDelete.TextImageRelation = TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnDelete, "Press Alt + D");
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += new EventHandler(btnDelete_Click);
			btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnSearch.BackColor = Color.DodgerBlue;
			btnSearch.FlatAppearance.BorderColor = Color.LightGray;
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.FlatStyle = FlatStyle.Flat;
			btnSearch.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Bold);
			btnSearch.ForeColor = Color.White;
			btnSearch.ImageAlign = ContentAlignment.TopCenter;
			btnSearch.ImeMode = ImeMode.NoControl;
			btnSearch.Location = new Point(244, 2);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(83, 45);
			btnSearch.TabIndex = 2;
			btnSearch.Text = "Search";
			btnSearch.TextImageRelation = TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnSearch, "Press Alt + R");
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Visible = false;
			btnSearch.Click += new EventHandler(btnSearch_Click);
			btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnClear.BackColor = Color.DodgerBlue;
			btnClear.FlatAppearance.BorderColor = Color.LightGray;
			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.FlatStyle = FlatStyle.Flat;
			btnClear.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Bold);
			btnClear.ForeColor = Color.White;
			btnClear.Image = (Image)resources.GetObject("btnClear.Image");
			btnClear.ImageAlign = ContentAlignment.TopCenter;
			btnClear.ImeMode = ImeMode.NoControl;
			btnClear.Location = new Point(163, 2);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(75, 45);
			btnClear.TabIndex = 2;
			btnClear.Text = "New";
			btnClear.TextImageRelation = TextImageRelation.ImageAboveText;
			ttMaster.SetToolTip(btnClear, "Press Alt + N");
			btnClear.UseVisualStyleBackColor = false;
			btnClear.Click += new EventHandler(btnClear_Click);
			pnlMaster.AllowMultiSelect = false;
			pnlMaster.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			pnlMaster.Angle = 0f;
			pnlMaster.BackColor = Color.DodgerBlue;
			pnlMaster.BottomColor = Color.DodgerBlue;
			pnlMaster.Location = new Point(1, 41);
			pnlMaster.Name = "pnlMaster";
			pnlMaster.Selected = false;
			pnlMaster.Size = new Size(150, 442);
			pnlMaster.TabIndex = 12;
			pnlMaster.TextAdjestmentHeight = 0;
			pnlMaster.TextAlign = ContentAlignment.MiddleCenter;
			pnlMaster.TopColor = Color.White;
			lblMasterTitle.AutoSize = true;
			lblMasterTitle.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
			lblMasterTitle.Location = new Point(4, 7);
			lblMasterTitle.Name = "lblMasterTitle";
			lblMasterTitle.Size = new Size(118, 24);
			lblMasterTitle.TabIndex = 13;
			lblMasterTitle.Text = "Master Title";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 482);
            Controls.Add(lblMasterTitle);
            Controls.Add(pnlMaster);
            Controls.Add(pnlButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MasterForm";
            Load += new EventHandler(MasterForm_Load);
            KeyDown += new KeyEventHandler(MasterForm_KeyDown);
            Controls.SetChildIndex(pnlButton, 0);
            Controls.SetChildIndex(pnlMaster, 0);
            Controls.SetChildIndex(lblMasterTitle, 0);
            Controls.SetChildIndex(btnEnd, 0);
			pnlButton.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
