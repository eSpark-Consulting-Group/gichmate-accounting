using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccountsPro.SubProjects.AccountsUserControls
{
	public class ProgressBar : UserControl
	{
		private IContainer components = null;

		private Label label1;

		private PictureBox pictureBox1;

		public ProgressBar(Form frm)
		{
			InitializeComponent();
			Location = new Point(frm.Width / 2 - 103, frm.Height / 2 - 26);
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ProgressBar));
			label1 = new Label();
			pictureBox1 = new PictureBox();
			((ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new Font("MS Reference Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.Location = new Point(76, 18);
			label1.Name = "label1";
			label1.Size = new Size(124, 16);
			label1.TabIndex = 5;
			label1.Text = "Please wait....!";
			pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.ImageLocation = "";
			pictureBox1.Location = new Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(65, 53);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 4;
			pictureBox1.TabStop = false;
			AutoScaleDimensions = new SizeF(6f, 13f);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.WhiteSmoke;
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			Name = "ProgressBar";
			Size = new Size(206, 53);
			((ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
