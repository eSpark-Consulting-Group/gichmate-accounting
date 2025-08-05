using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AshUsercontrol
{
	public class ProgressBar : UserControl
	{
		private IContainer components = null;

		private Label label1;

		private PictureBox pictureBox1;

		public ProgressBar(Form frm)
		{
			InitializeComponent();
			base.Location = new Point(frm.Width / 2 - 103, frm.Height / 2 - 26);
			frm.Controls.Add(this);
			BringToFront();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AshUsercontrol.ProgressBar));
			label1 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(76, 18);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(124, 16);
			label1.TabIndex = 5;
			label1.Text = "Please wait....!";
			pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.ImageLocation = "";
			pictureBox1.Location = new System.Drawing.Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(65, 53);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.Controls.Add(label1);
			base.Controls.Add(pictureBox1);
			base.Name = "ProgressBar";
			base.Size = new System.Drawing.Size(206, 53);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
