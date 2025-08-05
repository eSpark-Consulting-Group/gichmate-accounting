using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using AshFramework;

namespace Ashpro
{
	public class WebcamView : BaseForm
	{
		private WebCam webcam;

		private int b = 0;

		private Timer timer;

		private IContainer components = null;

		private PictureBox pictureBox2;

		private PictureBox pictureBox1;

		public string sFileName { get; set; }

		public WebcamView()
		{
			InitializeComponent();
			timer = new Timer();
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			pictureBox2.Image = pictureBox1.Image;
			if (b < 5)
			{
				b++;
			}
			else
			{
				if (pictureBox2.Image == null)
				{
					return;
				}
				try
				{
					sFileName = Application.StartupPath + "\\a.png";
					if (File.Exists(sFileName))
					{
						return;
					}
					Bitmap bitmap = new Bitmap(pictureBox2.Image);
					bitmap.Save(sFileName, ImageFormat.Png);
					bitmap.Dispose();
					bitmap = null;
					webcam.Stop();
				}
				catch (Exception)
				{
					timer.Stop();
					base.DialogResult = DialogResult.OK;
					Close();
				}
				timer.Stop();
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void WebcamView_Load(object sender, EventArgs e)
		{
			webcam = new WebCam();
			webcam.InitializeWebCam(ref pictureBox1);
			webcam.Start();
			timer.Start();
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
			pictureBox2 = new System.Windows.Forms.PictureBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(647, -1);
			btnEnd.Visible = false;
			pictureBox2.Location = new System.Drawing.Point(357, 10);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new System.Drawing.Size(321, 337);
			pictureBox2.TabIndex = 1;
			pictureBox2.TabStop = false;
			pictureBox1.Location = new System.Drawing.Point(11, 10);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(321, 337);
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(683, 353);
			base.Controls.Add(pictureBox2);
			base.Controls.Add(pictureBox1);
			base.Name = "WebcamView";
			base.Opacity = 0.0;
			base.Load += new System.EventHandler(WebcamView_Load);
			base.Controls.SetChildIndex(pictureBox1, 0);
			base.Controls.SetChildIndex(pictureBox2, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
