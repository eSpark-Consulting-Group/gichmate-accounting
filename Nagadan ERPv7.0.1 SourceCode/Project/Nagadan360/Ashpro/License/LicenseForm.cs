using Ashpro.License.Helper;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro
{
    public partial class LicenseForm : Form
    {
        Timer syncTimer = new Timer();


        public LicenseForm()
        {
            InitializeComponent();

            // Create and configure the timer
            syncTimer.Interval = 10000; // The server will check every 10 seconds.
            syncTimer.Tick += SyncTimer_Tick;
            syncTimer.Start();

        }

        private async void SyncTimer_Tick(object sender, EventArgs e)
        {
            var cached = CachedLicense.LoadLicense();
            if (cached == null) return;

            var result = await ValidateLicenseAsync(cached.LicenseKey);
            if (result?.Valid == true)
            {
                DateTime serverDate = DateTime.Parse(result.ExpiresOn);
                if (serverDate > cached.ExpiresOn)
                {
                    // ✅ Newer expiry from server → update cache
                    CachedLicense.SaveLicense(new CachedLicense
                    {
                        LicenseKey = cached.LicenseKey,
                        ExpiresOn = serverDate
                    });

                    syncTimer.Stop(); // No need to check anymore.

                    MessageBox.Show("✅ License renewed from server!");

                    new Login().Show();
                    this.Hide();
                }
            }
        }

        public class LicenseValidationResult
        {
            public bool Valid { get; set; }
            public string FullName { get; set; }
            public string LicenseType { get; set; }
            public string ExpiresOn { get; set; }
        }

        private async Task<LicenseValidationResult?> ValidateLicenseAsync(string licenseKey)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Please ensure the API URL is correct and accessible. https://yourWebsite.com/api/license/validate
                    // For local testing, you can use localhost with the appropriate port.
                    string url = $"http://license.somee.com/api/license/validate?key={Uri.EscapeDataString(licenseKey)}";
                    var response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<LicenseValidationResult>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API error: " + ex.Message);
                    return null;
                }
            }
        }

        

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            string key = txtLicense.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Please Enter License Key।");
                return;
            }

            var result = await ValidateLicenseAsync(key);

            if (result?.Valid == true)
            {
                CachedLicense.SaveLicense(new CachedLicense
                {
                    LicenseKey = key,
                    ExpiresOn = DateTime.Parse(result.ExpiresOn)
                });

                new Login().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("❌ License is invalid or expired!");
                Application.Exit();
            }
        }

        private void txtLicense_TextChanged(object sender, EventArgs e)
        {
            // Optional: handle text change
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://license.somee.com/Customer/Create";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open browser: " + ex.Message);
            }
        }
    }
}