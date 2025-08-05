using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ashpro.License.Helper
{
    public static class LicenseManager
    {
        // ... other methods ...

        public class LicenseValidationResult
        {
            public bool Valid { get; set; }
            public string FullName { get; set; }
            public string LicenseType { get; set; }
            public string ExpiresOn { get; set; }
        }

        public static async Task<LicenseValidationResult?> ValidateLicenseWithServer(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
                return null;

            using (var client = new HttpClient())
            {
                try
                {
                    // Replace with your actual API endpoint https://yourWebsite.com/api/license/validate
                    // Ensure the API URL is correct and accessible.
                    string url = $"http://license.somee.com/api/license/validate?key={Uri.EscapeDataString(licenseKey)}";
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        return null;

                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LicenseValidationResult>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}