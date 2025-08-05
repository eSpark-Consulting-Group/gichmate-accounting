using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class VideoTutorial : BaseForm
	{
		private IContainer components = null;

		private Panel panel1;

		private Label label2;

		private Button btnPlay;

		private AshComboBox cmbURL;

		private Button button4;

		private Label lblHead;

		private WebBrowser wbPlayer;

		public VideoTutorial()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void VideoTutorial_Load(object sender, EventArgs e)
		{
		}

		private void btnPlay_Click(object sender, EventArgs e)
		{
			string text = "<html><head>";
			text += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
			text += "<iframe id='video' src= 'https://www.youtube.com/embed/pOvwybaw4po?si=qIVON3DlAZ72Br6m' width='720' height='460' frameborder='0' allowfullscreen></iframe>";
			text += "</body></html>";
			wbPlayer.DocumentText = string.Format(text, cmbURL.Text.Split('=')[1]);
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnPlay.Enabled = true;
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
			panel1 = new System.Windows.Forms.Panel();
			wbPlayer = new System.Windows.Forms.WebBrowser();
			label2 = new System.Windows.Forms.Label();
			btnPlay = new System.Windows.Forms.Button();
			cmbURL = new AshUsercontrol.AshComboBox();
			button4 = new System.Windows.Forms.Button();
			lblHead = new System.Windows.Forms.Label();
			panel1.SuspendLayout();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(711, 0);
			panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(wbPlayer);
			panel1.Location = new System.Drawing.Point(-1, 69);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(747, 493);
			panel1.TabIndex = 196;
			wbPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			wbPlayer.Location = new System.Drawing.Point(0, 0);
			wbPlayer.MinimumSize = new System.Drawing.Size(20, 20);
			wbPlayer.Name = "wbPlayer";
			wbPlayer.Size = new System.Drawing.Size(747, 493);
			wbPlayer.TabIndex = 0;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 44);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(83, 16);
			label2.TabIndex = 11;
			label2.Text = "Video List :";
			btnPlay.BackColor = System.Drawing.Color.DodgerBlue;
			btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnPlay.ForeColor = System.Drawing.Color.White;
			btnPlay.Location = new System.Drawing.Point(659, 36);
			btnPlay.Name = "btnPlay";
			btnPlay.Size = new System.Drawing.Size(75, 30);
			btnPlay.TabIndex = 10;
			btnPlay.Text = "Play";
			btnPlay.UseVisualStyleBackColor = false;
			btnPlay.Click += new System.EventHandler(btnPlay_Click);
			cmbURL.EnterColor = System.Drawing.Color.LightPink;
			cmbURL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmbURL.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cmbURL.FormattingEnabled = true;
			cmbURL.Items.AddRange(new object[1] { "Nagadan ERP Demo Video" });
			cmbURL.LeaveColor = System.Drawing.Color.White;
			cmbURL.Location = new System.Drawing.Point(95, 40);
			cmbURL.Name = "cmbURL";
			cmbURL.Size = new System.Drawing.Size(556, 24);
			cmbURL.TabIndex = 0;
			cmbURL.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			button4.Location = new System.Drawing.Point(1, 30);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(156, 1);
			button4.TabIndex = 198;
			button4.UseVisualStyleBackColor = true;
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(7, 6);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(141, 24);
			lblHead.TabIndex = 197;
			lblHead.Text = "Video Tutorial";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(746, 564);
			base.Controls.Add(button4);
			base.Controls.Add(lblHead);
			base.Controls.Add(panel1);
			base.Controls.Add(label2);
			base.Controls.Add(btnPlay);
			base.Controls.Add(cmbURL);
			base.MaximizeBox = false;
			base.Name = "VideoTutorial";
			base.Load += new System.EventHandler(VideoTutorial_Load);
			base.Controls.SetChildIndex(cmbURL, 0);
			base.Controls.SetChildIndex(btnPlay, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(button4, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
