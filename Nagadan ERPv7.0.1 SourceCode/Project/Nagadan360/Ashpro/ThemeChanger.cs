using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ThemeChanger : BaseForm
	{
		private IContainer components = null;

		private Panel panel1;

		private Button btnCancel;

		private Button btnOK;

		private RadioButton rdbPOS;

		private RadioButton rdbMDI;

		private RadioButton rdbDashboard;

		private PictureBox pbPOS;

		private PictureBox pbMDI;

		private PictureBox pbDashboard;

		private Label lblTitle;

		private RadioButton rbRestaurant;

		private PictureBox pictureBox1;

		public bool isFromActivation { get; set; } = false;


		public ThemeChanger(bool _isFromActivation = false)
		{
			isFromActivation = _isFromActivation;
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
			ColorChange.Color(this);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (rdbMDI.Checked)
			{
				Settings.Default.MainForm = "MDI";
			}
			else if (rdbPOS.Checked)
			{
				Settings.Default.MainForm = "POS";
			}
			else if (rdbDashboard.Checked)
			{
				Settings.Default.MainForm = "Dashboard";
			}
			else
			{
				Settings.Default.MainForm = "Restaurant";
			}
			Settings.Default.Save();
			if (isFromActivation)
			{
				if (!rdbMDI.Checked)
				{
					POSLogin pOSLogin = new POSLogin();
					pOSLogin.Show();
				}
				else
				{
					Login login = new Login();
					login.Show();
				}
			}
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ThemeChanger_Load(object sender, EventArgs e)
		{
			if (Utility.uManufacturer == "DeskCloud ERP")
			{
				base.Icon = Resources.ico_logo1;
				pbMDI.BackgroundImage = Resources.AshproMDI;
			}
			else
			{
				base.Icon = Resources.ico_logo;
				pbMDI.BackgroundImage = Resources.wall_360;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeChanger));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbPOS = new System.Windows.Forms.RadioButton();
            this.rbRestaurant = new System.Windows.Forms.RadioButton();
            this.rdbMDI = new System.Windows.Forms.RadioButton();
            this.rdbDashboard = new System.Windows.Forms.RadioButton();
            this.pbPOS = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbMDI = new System.Windows.Forms.PictureBox();
            this.pbDashboard = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMDI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDashboard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(1169, 0);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnd.Size = new System.Drawing.Size(47, 43);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbPOS);
            this.panel1.Controls.Add(this.rbRestaurant);
            this.panel1.Controls.Add(this.rdbMDI);
            this.panel1.Controls.Add(this.rdbDashboard);
            this.panel1.Controls.Add(this.pbPOS);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pbMDI);
            this.panel1.Controls.Add(this.pbDashboard);
            this.panel1.Location = new System.Drawing.Point(7, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 508);
            this.panel1.TabIndex = 11;
            // 
            // rdbPOS
            // 
            this.rdbPOS.AutoSize = true;
            this.rdbPOS.Location = new System.Drawing.Point(863, 4);
            this.rdbPOS.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPOS.Name = "rdbPOS";
            this.rdbPOS.Size = new System.Drawing.Size(53, 20);
            this.rdbPOS.TabIndex = 1;
            this.rdbPOS.TabStop = true;
            this.rdbPOS.Text = "POS";
            this.rdbPOS.UseVisualStyleBackColor = true;
            // 
            // rbRestaurant
            // 
            this.rbRestaurant.AutoSize = true;
            this.rbRestaurant.Location = new System.Drawing.Point(484, 259);
            this.rbRestaurant.Margin = new System.Windows.Forms.Padding(4);
            this.rbRestaurant.Name = "rbRestaurant";
            this.rbRestaurant.Size = new System.Drawing.Size(130, 20);
            this.rbRestaurant.TabIndex = 1;
            this.rbRestaurant.TabStop = true;
            this.rbRestaurant.Text = "Restaurant POS";
            this.rbRestaurant.UseVisualStyleBackColor = true;
            // 
            // rdbMDI
            // 
            this.rdbMDI.AutoSize = true;
            this.rdbMDI.Location = new System.Drawing.Point(484, 4);
            this.rdbMDI.Margin = new System.Windows.Forms.Padding(4);
            this.rdbMDI.Name = "rdbMDI";
            this.rdbMDI.Size = new System.Drawing.Size(51, 20);
            this.rdbMDI.TabIndex = 1;
            this.rdbMDI.TabStop = true;
            this.rdbMDI.Text = "MDI";
            this.rdbMDI.UseVisualStyleBackColor = true;
            // 
            // rdbDashboard
            // 
            this.rdbDashboard.AutoSize = true;
            this.rdbDashboard.Checked = true;
            this.rdbDashboard.Location = new System.Drawing.Point(49, 2);
            this.rdbDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.rdbDashboard.Name = "rdbDashboard";
            this.rdbDashboard.Size = new System.Drawing.Size(95, 20);
            this.rdbDashboard.TabIndex = 1;
            this.rdbDashboard.TabStop = true;
            this.rdbDashboard.Text = "Dashboard";
            this.rdbDashboard.UseVisualStyleBackColor = true;
            // 
            // pbPOS
            // 
            this.pbPOS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPOS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbPOS.BackgroundImage")));
            this.pbPOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPOS.Location = new System.Drawing.Point(807, 32);
            this.pbPOS.Margin = new System.Windows.Forms.Padding(4);
            this.pbPOS.Name = "pbPOS";
            this.pbPOS.Size = new System.Drawing.Size(381, 219);
            this.pbPOS.TabIndex = 0;
            this.pbPOS.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(405, 285);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(388, 219);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pbMDI
            // 
            this.pbMDI.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbMDI.BackgroundImage")));
            this.pbMDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbMDI.Image = ((System.Drawing.Image)(resources.GetObject("pbMDI.Image")));
            this.pbMDI.Location = new System.Drawing.Point(405, 32);
            this.pbMDI.Margin = new System.Windows.Forms.Padding(4);
            this.pbMDI.Name = "pbMDI";
            this.pbMDI.Size = new System.Drawing.Size(388, 219);
            this.pbMDI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMDI.TabIndex = 0;
            this.pbMDI.TabStop = false;
            // 
            // pbDashboard
            // 
            this.pbDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbDashboard.BackgroundImage")));
            this.pbDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbDashboard.Image = ((System.Drawing.Image)(resources.GetObject("pbDashboard.Image")));
            this.pbDashboard.Location = new System.Drawing.Point(10, 32);
            this.pbDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.pbDashboard.Name = "pbDashboard";
            this.pbDashboard.Size = new System.Drawing.Size(381, 219);
            this.pbDashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDashboard.TabIndex = 0;
            this.pbDashboard.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1108, 546);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 67;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(1002, 546);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 68;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(7, 4);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 24);
            this.lblTitle.TabIndex = 69;
            this.lblTitle.Text = "Select Theme";
            // 
            // ThemeChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 586);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThemeChanger";
            this.Load += new System.EventHandler(this.ThemeChanger_Load);
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMDI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDashboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
