using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AshFramework;
using AshUsercontrol;

namespace Ashpro
{
	public class QRCodeGenerator : BaseForm
	{
		private IContainer components = null;

		private Label lblHead;

		private Label label12;

		private AshTextBoxNormal txtText;

		private PictureBox pbQRCode;

		private Button btnSaveImage;

		public QRCodeGenerator()
		{
			InitializeComponent();
			ColorChange.Color(this);
		}

		private void ClearQRCode()
		{
			if (pbQRCode.Image != null)
			{
				Image image = pbQRCode.Image;
				pbQRCode.Image = null;
				image.Dispose();
			}
		}

		private Image EditImage(Image image)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			Bitmap bitmap = new Bitmap(pbQRCode.Width, pbQRCode.Height);
			string s = txtText.Text;
			bitmap = (Bitmap)pbQRCode.Image;
			_ = pbQRCode.Width / 2;
			int num = pbQRCode.Height - 60;
			int num2 = pbQRCode.Width - 20;
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using Font font = new Font("Arial", 14f, FontStyle.Bold);
				graphics.DrawString(s, font, Brushes.Black, new RectangleF(0f, num, num2, 45f));
			}
			return bitmap;
		}

		private void txtEditWith_TextChanged(object sender, EventArgs e)
		{
			ClearQRCode();
			int num = Math.Min(pbQRCode.Width, pbQRCode.Height);
			pbQRCode.Image = ProductBarcode.CreateQRCode(txtText.Text, num, num);
			pbQRCode.BackColor = Color.White;
		}

		private void btnSaveImage_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = null;
			try
			{
				saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save Image";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					EditImage(pbQRCode.Image).Save(saveFileDialog.FileName + ".jpg");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				saveFileDialog.Dispose();
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
			lblHead = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			txtText = new AshUsercontrol.AshTextBoxNormal();
			pbQRCode = new System.Windows.Forms.PictureBox();
			btnSaveImage = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pbQRCode).BeginInit();
			SuspendLayout();
			btnEnd.FlatAppearance.BorderSize = 0;
			btnEnd.Location = new System.Drawing.Point(504, 0);
			lblHead.AutoSize = true;
			lblHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
			lblHead.Location = new System.Drawing.Point(3, 3);
			lblHead.Name = "lblHead";
			lblHead.Size = new System.Drawing.Size(102, 24);
			lblHead.TabIndex = 94;
			lblHead.Text = "QR Code ";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label12.Location = new System.Drawing.Point(31, 53);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(49, 16);
			label12.TabIndex = 95;
			label12.Text = "Text :";
			txtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtText.Data = 0;
			txtText.EnterColor = System.Drawing.Color.LightPink;
			txtText.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			txtText.Format = null;
			txtText.isAllowNegative = false;
			txtText.isAllowSpecialChar = false;
			txtText.isNumberOnly = false;
			txtText.isNumeric = false;
			txtText.LeaveColor = System.Drawing.Color.White;
			txtText.Location = new System.Drawing.Point(94, 55);
			txtText.Margin = new System.Windows.Forms.Padding(4);
			txtText.Multiline = true;
			txtText.Name = "txtText";
			txtText.Size = new System.Drawing.Size(420, 51);
			txtText.TabIndex = 96;
			txtText.Value = new decimal(new int[4]);
			txtText.WaterMark = null;
			txtText.TextChanged += new System.EventHandler(txtEditWith_TextChanged);
			pbQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pbQRCode.Location = new System.Drawing.Point(16, 138);
			pbQRCode.Name = "pbQRCode";
			pbQRCode.Size = new System.Drawing.Size(510, 395);
			pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pbQRCode.TabIndex = 97;
			pbQRCode.TabStop = false;
			btnSaveImage.BackColor = System.Drawing.Color.DodgerBlue;
			btnSaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSaveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnSaveImage.ForeColor = System.Drawing.Color.White;
			btnSaveImage.Location = new System.Drawing.Point(457, 538);
			btnSaveImage.Name = "btnSaveImage";
			btnSaveImage.Size = new System.Drawing.Size(70, 30);
			btnSaveImage.TabIndex = 98;
			btnSaveImage.Text = "Save";
			btnSaveImage.UseVisualStyleBackColor = false;
			btnSaveImage.Click += new System.EventHandler(btnSaveImage_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(539, 571);
			base.Controls.Add(btnSaveImage);
			base.Controls.Add(pbQRCode);
			base.Controls.Add(txtText);
			base.Controls.Add(label12);
			base.Controls.Add(lblHead);
			base.Name = "QRCodeGenerator";
			base.Controls.SetChildIndex(btnEnd, 0);
			base.Controls.SetChildIndex(lblHead, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(txtText, 0);
			base.Controls.SetChildIndex(pbQRCode, 0);
			base.Controls.SetChildIndex(btnSaveImage, 0);
			((System.ComponentModel.ISupportInitialize)pbQRCode).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
