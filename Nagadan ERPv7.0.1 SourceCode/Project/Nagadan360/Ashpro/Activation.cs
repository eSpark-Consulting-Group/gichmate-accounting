using AshFramework;
using Ashpro.Properties;
using AshUsercontrol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro
{
	public class Activation : BaseForm
	{
		public static string licenceKey;

		private string startdate;

		private string enddate;

		private string license;

		private IContainer components = null;

		private Label lblTitle;

		private Label label1;

		private AshTextBoxNormal txtKey;

		private AshButton btnRegister;

		public Activation()
		{
			InitializeComponent();
			AniHelper.IconChangeMethod(this);
		}

        public async Task<LicenseValidationResult?> ValidateLicenseAsync(string licenseKey)
        {
            using var client = new HttpClient();

            try
            {
                string url = $"https://localhost:7209/api/api/validate?key={licenseKey}";
                var result = await client.GetFromJsonAsync<LicenseValidationResult>(url);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<object> VerifyLicenceAsync(string licenceKey, string url, bool isActivation = true)
		{
			using HttpClient client = new HttpClient();
			string result = null;
			foreach (string item in Utility.uMacId)
			{
				var contentObject = new
				{
					licenseKey = licenceKey,
					macId = item,
					ipAddress = AniHelper.GetIpAddress()
				};
				StringContent content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("riolab123456");
				result = (await client.PostAsync(url, content).ConfigureAwait(continueOnCapturedContext: false)).Content.ReadAsStringAsync().Result;
				if (!result.Contains("400"))
				{
					Utility.uSpecMacId = item;
					break;
				}
			}
			if (isActivation)
			{
				List<RootObject> res = JsonConvert.DeserializeObject<List<RootObject>>("[" + result + "]");
				if (res == null)
				{
					res[0].message.message = res.ToString();
				}
				return res;
			}
			return null;
		}

		public static async Task<object> AshproVerifyLicenceAsync(string licenceKey)
		{
			string result = GetLicenseKey(licenceKey);
			return JsonConvert.DeserializeObject<List<Customerdetails>>("[" + result + "]");
		}

		private static string GetLicenseKey(string licenceKey)
		{
			string text = "https://localhost:7209/api/api/validate?key={licenseKey}";
			HttpMessageHandler handler = new HttpClientHandler();
			HttpClient httpClient = new HttpClient(handler)
			{
				BaseAddress = new Uri(text),
				Timeout = new TimeSpan(0, 2, 0)
			};
			httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
			byte[] bytes = Encoding.UTF8.GetBytes("riolab123456");
			string text2 = Convert.ToBase64String(bytes);
			httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + text2);
			string result = string.Empty;
			foreach (string item in Utility.uMacId)
			{
				var value = new
				{
					licenseKey = licenceKey,
					macId = item,
					ipAddress = AniHelper.GetIpAddress()
				};
				StringContent content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
				HttpResponseMessage result2 = httpClient.PostAsync(text, content).Result;
				result = result2.Content.ReadAsStringAsync().Result;
				if (result2.StatusCode != HttpStatusCode.BadRequest)
				{
					Utility.uSpecMacId = item;
					break;
				}
			}
			return result;
		}

		private void btnRegister_Click(object sender, EventArgs e)
		{
			try
			{
				licenceKey = txtKey.Text;
				if (!licenceKey.isNumeric())
				{
					Messages.ErrorMessage("The given Activation Key is not valid!");
					return;
				}
				if (Utility.uManufacturer == "DeskCloud ERP")
				{
					List<Customerdetails> list = (List<Customerdetails>)AshproVerifyLicenceAsync(licenceKey).GetAwaiter().GetResult();
					if (list.Count > 0)
					{
						license = list[0].licenseType;
						if (license == "demo")
						{
							startdate = list[0].startDate;
							enddate = list[0].expiryDate;
						}
						else
						{
							startdate = "0";
							enddate = "0";
						}
					}
				}
				else
				{
					string url = "https://localhost:7209/api/api/validate?key={licenseKey}";
					List<RootObject> list2 = (List<RootObject>)VerifyLicenceAsync(licenceKey, url).GetAwaiter().GetResult();
					if (list2.Count > 0)
					{
						license = list2[0].message.licenseType.ToString();
						if (license == "demo")
						{
							license = "demo";
							startdate = list2[0].message.startDate.ToString();
							enddate = list2[0].message.expiryDate.ToString();
						}
						else
						{
							license = "Permenant";
							startdate = "0";
							enddate = "0";
						}
					}
				}
				Utility.uLicenceNumber = license;
				Settings.Default.Activationlicense = license;
				Settings.Default.ActivationStartDate = startdate;
				Settings.Default.ActivationEndDate = enddate;
				Settings.Default.ActivationMacID = Utility.uSpecMacId;
				Settings.Default.ActivationKey = licenceKey;
				Settings.Default.Save();
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				string text = null;
				foreach (string item in Utility.uMacId)
				{
					text = text + " " + item;
				}
				CustomeMessageBox customeMessageBox = new CustomeMessageBox();
				customeMessageBox.lblMessage.Text = "The Registration is filed for the ID " + text + ". Please contact the dealer.";
				customeMessageBox.ShowDialog();
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		private void txtKey_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnRegister.PerformClick();
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new AshUsercontrol.AshTextBoxNormal();
            this.btnRegister = new AshUsercontrol.AshButton();
            this.SuspendLayout();
            // 
            // btnEnd
            // 
            this.btnEnd.FlatAppearance.BorderSize = 0;
            this.btnEnd.Location = new System.Drawing.Point(841, 0);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(5);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(1, 2);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 29);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Activation Window";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Enter License Key :";
            // 
            // txtKey
            // 
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKey.Data = 0;
            this.txtKey.EnterColor = System.Drawing.Color.LightPink;
            this.txtKey.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Format = null;
            this.txtKey.isAllowNegative = false;
            this.txtKey.isAllowSpecialChar = false;
            this.txtKey.isNumberOnly = false;
            this.txtKey.isNumeric = false;
            this.txtKey.LeaveColor = System.Drawing.Color.White;
            this.txtKey.Location = new System.Drawing.Point(261, 95);
            this.txtKey.Margin = new System.Windows.Forms.Padding(5);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(556, 20);
            this.txtKey.TabIndex = 14;
            this.txtKey.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtKey.WaterMark = null;
            this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("MS Reference Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(261, 140);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(152, 43);
            this.btnRegister.TabIndex = 41;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // Activation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 220);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Activation";
            this.Controls.SetChildIndex(this.btnEnd, 0);
            this.Controls.SetChildIndex(this.lblTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.btnRegister, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
