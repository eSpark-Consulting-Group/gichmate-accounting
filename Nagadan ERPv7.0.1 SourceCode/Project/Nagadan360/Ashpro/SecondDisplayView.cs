using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public class SecondDisplayView : BaseFormTwo
	{
		private Timer tmrTotal;

		private Timer tmrImageSliding;

		private int iTmr;

		private string[] folderFile;

		private int selected = 0;

		private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

		private DBContext db = new DBContext();

		private IContainer components = null;

		private Panel pnlTitle;

		private TabControl tbMain;

		private TabPage tabPage1;

		private PictureBox pbShow;

		private TabPage tabPage2;

		private PictureBox pbLogo;

		private Label lblCompany;

		private Label lblTotal;

		private Label lblProductName;

		private Button btnImage;

		private Panel pnlPrtoduct;

		private Panel pnlProImage;

		private Panel pnlInvoiceTotal;

		private TabPage tabPage3;

		private Label lblTotalShow;

		private Panel pnlProductDetails;

		private Panel pnlSlide;

		private PictureBox pbSlide;

		public SecondDisplayView()
		{
			InitializeComponent();
			tmrTotal = new Timer();
			tmrTotal.Interval = 500;
			tmrTotal.Tick += TmrTotal_Tick;
			tmrImageSliding = new Timer();
			tmrImageSliding.Interval = 3000;
			tmrImageSliding.Tick += TmrImageSliding_Tick;
		}

		private void TmrImageSliding_Tick(object sender, EventArgs e)
		{
			nextImage();
		}

		private void FolderBrouse()
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.strImageFolderSecondDisplay = folderBrowserDialog.SelectedPath;
				Settings.Default.Save();
			}
		}

		private void ImageLoadMethod()
		{
			string[] array = null;
			string[] array2 = null;
			string[] array3 = null;
			array = Directory.GetFiles(Settings.Default.strImageFolderSecondDisplay, "*.jpg");
			array2 = Directory.GetFiles(Settings.Default.strImageFolderSecondDisplay, "*.jpeg");
			array3 = Directory.GetFiles(Settings.Default.strImageFolderSecondDisplay, "*.png");
			folderFile = new string[array.Length + array2.Length + array3.Length];
			Array.Copy(array, 0, folderFile, 0, array.Length);
			Array.Copy(array2, 0, folderFile, array.Length, array2.Length);
			Array.Copy(array3, 0, folderFile, array.Length + array2.Length, array3.Length);
			selected = 0;
			showImage(folderFile[selected]);
		}

		private void showImage(string path)
		{
			Image backgroundImage = Image.FromFile(path);
			if (tbMain.SelectedIndex == 0)
			{
				pbShow.BackgroundImage = backgroundImage;
				pbShow.BackgroundImageLayout = ImageLayout.Zoom;
			}
			else if (tbMain.SelectedIndex == 1)
			{
				pbSlide.BackgroundImage = backgroundImage;
				pbSlide.BackgroundImageLayout = ImageLayout.Zoom;
			}
		}

		private void nextImage()
		{
			try
			{
				if (selected == folderFile.Length - 1)
				{
					selected = 0;
					showImage(folderFile[selected]);
				}
				else
				{
					selected++;
					showImage(folderFile[selected]);
				}
			}
			catch (Exception)
			{
			}
		}

		private void ColorChangeMethod()
		{
			lblCompany.BackColor = Settings.Default.CompanyBColor;
			lblCompany.ForeColor = Settings.Default.CompanyFColor;
			btnImage.BackColor = Settings.Default.ProImageBColor;
			lblProductName.BackColor = Settings.Default.ProDetailsBColor;
			pnlInvoiceTotal.BackColor = Settings.Default.TotalLabelBColor;
		}

		public void ShowProductDetails(string sMessage, string sTotal, Image imgProduct = null, string sProName = null)
		{
			btnImage.Text = string.Empty;
			tbMain.SelectedIndex = 1;
			lblProductName.Text = sMessage;
			btnImage.BackgroundImage = imgProduct;
			btnImage.BackgroundImageLayout = ImageLayout.Zoom;
			if (imgProduct == null && sProName != null)
			{
				btnImage.Text = sProName;
			}
			lblTotal.Text = sTotal;
			tmrTotal.Enabled = true;
		}

		public void ShowImageSlider()
		{
			tmrTotal.Enabled = false;
			tbMain.SelectedIndex = 0;
		}

		public void ShowTotalAmountOnly(string sValue)
		{
			tmrTotal.Enabled = true;
			tbMain.SelectedIndex = 2;
			lblTotalShow.Text = sValue;
		}

		private void TmrTotal_Tick(object sender, EventArgs e)
		{
			iTmr++;
			if (iTmr >= 10)
			{
				iTmr = 0;
				tmrTotal.Enabled = false;
			}
			else if (lblTotal.ForeColor == Color.Green)
			{
				lblTotal.ForeColor = Color.Red;
				lblProductName.ForeColor = Color.Green;
				lblTotalShow.ForeColor = Color.Red;
			}
			else
			{
				lblTotal.ForeColor = Color.Green;
				lblProductName.ForeColor = Color.Red;
				lblTotalShow.ForeColor = Color.Green;
			}
		}

		private void SecondDisplayView_Load(object sender, EventArgs e)
		{
			base.Location = Screen.AllScreens[1].WorkingArea.Location;
			base.WindowState = FormWindowState.Maximized;
			ColorChangeMethod();
			lblCompany.Text = db.Companies().FirstOrDefault().InstituteName;
			if (lblCompany.Text.Length > 25)
			{
				lblCompany.Font = new Font(lblCompany.Font.FontFamily, 24f);
			}
			try
			{
				if (Settings.Default.strImageFolderSecondDisplay == string.Empty)
				{
					FolderBrouse();
				}
				ImageLoadMethod();
				tmrImageSliding.Enabled = true;
			}
			catch (Exception)
			{
			}
		}

		private void SecondDisplayView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Alt)
			{
				SecondDisplaySettings secondDisplaySettings = new SecondDisplaySettings();
				if (secondDisplaySettings.ShowDialog() == DialogResult.OK)
				{
					ImageLoadMethod();
					tmrImageSliding.Enabled = true;
					ColorChangeMethod();
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ashpro.SecondDisplayView));
			pnlTitle = new System.Windows.Forms.Panel();
			lblCompany = new System.Windows.Forms.Label();
			pbLogo = new System.Windows.Forms.PictureBox();
			tbMain = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			pbShow = new System.Windows.Forms.PictureBox();
			tabPage2 = new System.Windows.Forms.TabPage();
			pnlSlide = new System.Windows.Forms.Panel();
			pbSlide = new System.Windows.Forms.PictureBox();
			pnlPrtoduct = new System.Windows.Forms.Panel();
			pnlProductDetails = new System.Windows.Forms.Panel();
			lblProductName = new System.Windows.Forms.Label();
			pnlProImage = new System.Windows.Forms.Panel();
			btnImage = new System.Windows.Forms.Button();
			pnlInvoiceTotal = new System.Windows.Forms.Panel();
			lblTotal = new System.Windows.Forms.Label();
			tabPage3 = new System.Windows.Forms.TabPage();
			lblTotalShow = new System.Windows.Forms.Label();
			pnlTitle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
			tbMain.SuspendLayout();
			tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbShow).BeginInit();
			tabPage2.SuspendLayout();
			pnlSlide.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbSlide).BeginInit();
			pnlPrtoduct.SuspendLayout();
			pnlProductDetails.SuspendLayout();
			pnlProImage.SuspendLayout();
			pnlInvoiceTotal.SuspendLayout();
			tabPage3.SuspendLayout();
			SuspendLayout();
			pnlTitle.Controls.Add(lblCompany);
			pnlTitle.Controls.Add(pbLogo);
			pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			pnlTitle.Location = new System.Drawing.Point(0, 0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new System.Drawing.Size(1013, 100);
			pnlTitle.TabIndex = 0;
			lblCompany.BackColor = System.Drawing.Color.Black;
			lblCompany.Dock = System.Windows.Forms.DockStyle.Fill;
			lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblCompany.ForeColor = System.Drawing.Color.White;
			lblCompany.Location = new System.Drawing.Point(0, 0);
			lblCompany.Name = "lblCompany";
			lblCompany.Size = new System.Drawing.Size(510, 100);
			lblCompany.TabIndex = 0;
			lblCompany.Text = "Company Name";
			lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			pbLogo.BackColor = System.Drawing.Color.Teal;
			pbLogo.BackgroundImage = (System.Drawing.Image)resources.GetObject("pbLogo.BackgroundImage");
			pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			pbLogo.Dock = System.Windows.Forms.DockStyle.Right;
			pbLogo.Location = new System.Drawing.Point(510, 0);
			pbLogo.Name = "pbLogo";
			pbLogo.Size = new System.Drawing.Size(503, 100);
			pbLogo.TabIndex = 1;
			pbLogo.TabStop = false;
			tbMain.Controls.Add(tabPage1);
			tbMain.Controls.Add(tabPage2);
			tbMain.Controls.Add(tabPage3);
			tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			tbMain.ItemSize = new System.Drawing.Size(0, 1);
			tbMain.Location = new System.Drawing.Point(0, 100);
			tbMain.Name = "tbMain";
			tbMain.SelectedIndex = 0;
			tbMain.Size = new System.Drawing.Size(1013, 641);
			tbMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			tbMain.TabIndex = 1;
			tabPage1.Controls.Add(pbShow);
			tabPage1.Location = new System.Drawing.Point(4, 5);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(1005, 632);
			tabPage1.TabIndex = 0;
			tabPage1.UseVisualStyleBackColor = true;
			pbShow.Dock = System.Windows.Forms.DockStyle.Fill;
			pbShow.Location = new System.Drawing.Point(3, 3);
			pbShow.Name = "pbShow";
			pbShow.Size = new System.Drawing.Size(999, 626);
			pbShow.TabIndex = 0;
			pbShow.TabStop = false;
			tabPage2.Controls.Add(pnlSlide);
			tabPage2.Controls.Add(pnlPrtoduct);
			tabPage2.Controls.Add(pnlInvoiceTotal);
			tabPage2.Location = new System.Drawing.Point(4, 5);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(1005, 632);
			tabPage2.TabIndex = 1;
			tabPage2.UseVisualStyleBackColor = true;
			pnlSlide.Controls.Add(pbSlide);
			pnlSlide.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlSlide.Location = new System.Drawing.Point(3, 3);
			pnlSlide.Name = "pnlSlide";
			pnlSlide.Size = new System.Drawing.Size(430, 526);
			pnlSlide.TabIndex = 7;
			pbSlide.Dock = System.Windows.Forms.DockStyle.Fill;
			pbSlide.Location = new System.Drawing.Point(0, 0);
			pbSlide.Name = "pbSlide";
			pbSlide.Size = new System.Drawing.Size(430, 526);
			pbSlide.TabIndex = 0;
			pbSlide.TabStop = false;
			pnlPrtoduct.Controls.Add(pnlProductDetails);
			pnlPrtoduct.Controls.Add(pnlProImage);
			pnlPrtoduct.Dock = System.Windows.Forms.DockStyle.Right;
			pnlPrtoduct.Location = new System.Drawing.Point(433, 3);
			pnlPrtoduct.Name = "pnlPrtoduct";
			pnlPrtoduct.Size = new System.Drawing.Size(569, 526);
			pnlPrtoduct.TabIndex = 6;
			pnlProductDetails.BackColor = System.Drawing.Color.White;
			pnlProductDetails.Controls.Add(lblProductName);
			pnlProductDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
			pnlProductDetails.Location = new System.Drawing.Point(0, 320);
			pnlProductDetails.Name = "pnlProductDetails";
			pnlProductDetails.Size = new System.Drawing.Size(569, 206);
			pnlProductDetails.TabIndex = 1;
			lblProductName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			lblProductName.BackColor = System.Drawing.Color.Black;
			lblProductName.Font = new System.Drawing.Font("MS Reference Sans Serif", 19.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblProductName.ForeColor = System.Drawing.Color.White;
			lblProductName.Location = new System.Drawing.Point(27, 9);
			lblProductName.Name = "lblProductName";
			lblProductName.Size = new System.Drawing.Size(515, 187);
			lblProductName.TabIndex = 0;
			lblProductName.Text = "Item Details";
			lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			pnlProImage.BackColor = System.Drawing.Color.White;
			pnlProImage.Controls.Add(btnImage);
			pnlProImage.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlProImage.Location = new System.Drawing.Point(0, 0);
			pnlProImage.Name = "pnlProImage";
			pnlProImage.Size = new System.Drawing.Size(569, 526);
			pnlProImage.TabIndex = 0;
			btnImage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			btnImage.BackColor = System.Drawing.Color.Black;
			btnImage.FlatAppearance.BorderSize = 0;
			btnImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			btnImage.ForeColor = System.Drawing.Color.White;
			btnImage.Location = new System.Drawing.Point(28, 14);
			btnImage.Name = "btnImage";
			btnImage.Size = new System.Drawing.Size(514, 285);
			btnImage.TabIndex = 3;
			btnImage.Text = "Product Name";
			btnImage.UseVisualStyleBackColor = false;
			pnlInvoiceTotal.BackColor = System.Drawing.Color.Black;
			pnlInvoiceTotal.Controls.Add(lblTotal);
			pnlInvoiceTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
			pnlInvoiceTotal.Location = new System.Drawing.Point(3, 529);
			pnlInvoiceTotal.Name = "pnlInvoiceTotal";
			pnlInvoiceTotal.Size = new System.Drawing.Size(999, 100);
			pnlInvoiceTotal.TabIndex = 5;
			lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			lblTotal.BackColor = System.Drawing.Color.Black;
			lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 48f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblTotal.ForeColor = System.Drawing.Color.Red;
			lblTotal.Location = new System.Drawing.Point(468, 12);
			lblTotal.Name = "lblTotal";
			lblTotal.Size = new System.Drawing.Size(526, 85);
			lblTotal.TabIndex = 2;
			lblTotal.Text = "0.00";
			lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			tabPage3.Controls.Add(lblTotalShow);
			tabPage3.Location = new System.Drawing.Point(4, 5);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new System.Windows.Forms.Padding(3);
			tabPage3.Size = new System.Drawing.Size(1005, 632);
			tabPage3.TabIndex = 2;
			tabPage3.UseVisualStyleBackColor = true;
			lblTotalShow.Dock = System.Windows.Forms.DockStyle.Fill;
			lblTotalShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 48f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lblTotalShow.Location = new System.Drawing.Point(3, 3);
			lblTotalShow.Name = "lblTotalShow";
			lblTotalShow.Size = new System.Drawing.Size(999, 626);
			lblTotalShow.TabIndex = 0;
			lblTotalShow.Text = "Total 0.00";
			lblTotalShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1013, 741);
			base.Controls.Add(tbMain);
			base.Controls.Add(pnlTitle);
			base.Name = "SecondDisplayView";
			Text = "SecondDisplayView";
			base.Load += new System.EventHandler(SecondDisplayView_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(SecondDisplayView_KeyDown);
			pnlTitle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
			tbMain.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pbShow).EndInit();
			tabPage2.ResumeLayout(false);
			pnlSlide.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pbSlide).EndInit();
			pnlPrtoduct.ResumeLayout(false);
			pnlProductDetails.ResumeLayout(false);
			pnlProImage.ResumeLayout(false);
			pnlInvoiceTotal.ResumeLayout(false);
			tabPage3.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
