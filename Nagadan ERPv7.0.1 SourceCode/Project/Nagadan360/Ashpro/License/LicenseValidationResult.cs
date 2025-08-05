using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro
{
    public class LicenseValidationResult
    {
        public bool Valid { get; set; }
        public string FullName { get; set; }
        public string LicenseType { get; set; }
        public string ExpiresOn { get; set; }

        public async Task<LicenseValidationResult?> ValidateLicenseAsync(string licenseKey)
        {
            using var client = new HttpClient();

            try
            {
                // Replace with your actual API endpoint https://yourWebsite.com/api/license/validate
                // Ensure the API URL is correct and accessible.
                string url = $"http://license.somee.com/api/api/validate?key={licenseKey}";
                var result = await client.GetFromJsonAsync<LicenseValidationResult>(url);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }

}
