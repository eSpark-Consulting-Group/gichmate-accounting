using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class ChangeDate : BaseForm
	{
		private IContainer components = null;

		private Panel panel1;

		private DateTimePicker dtmDate;

		private Button btnSave;

		private Label label2;

		private PictureBox pictureBox1;

		private Label label1;

		public ChangeDate()
		{
			InitializeComponent();
			ColorChange.Color(this);
			btnEnd.Visible = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Settings.Default.appDate = dtmDate.Value;
			Settings.Default.Save();
			Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.ChangeDate));
			panel1 = new System.Windows.Forms.Panel();
			dtmDate = new System.Windows.Forms.DateTimePicker();
			btnSave = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			label1 = new System.Windows.Forms.Label();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(480, -1);
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(dtmDate);
			panel1.Controls.Add(btnSave);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(pictureBox1);
			panel1.Location = new System.Drawing.Point(25, 54);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(469, 148);
			panel1.TabIndex = 0;
			dtmDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dtmDate.Location = new System.Drawing.Point(232, 59);
			dtmDate.Name = "dtmDate";
			dtmDate.Size = new System.Drawing.Size(108, 20);
			dtmDate.TabIndex = 1;
			btnSave.BackColor = System.Drawing.Color.DodgerBlue;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(366, 55);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(75, 30);
			btnSave.TabIndex = 2;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += new System.EventHandler(btnSave_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(176, 59);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 13);
			label2.TabIndex = 0;
			label2.Text = "Date:";
			pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(13, 13);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(145, 122);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(21, 18);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(131, 24);
			label1.TabIndex = 1;
			label1.Text = "Change Date";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.DodgerBlue;
			base.ClientSize = new System.Drawing.Size(515, 220);
			base.Controls.Add(panel1);
			base.Controls.Add(label1);
			base.Name = "ChangeDate";
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btnEnd, 0);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
