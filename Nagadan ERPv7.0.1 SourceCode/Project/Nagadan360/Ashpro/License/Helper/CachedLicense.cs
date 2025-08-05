using Newtonsoft.Json;
using System;
using System.IO;

namespace Ashpro.License.Helper
{
    public class CachedLicense
    {
        public string LicenseKey { get; set; }
        public DateTime ExpiresOn { get; set; }

        private static string filePath = "license.json";

        public static void SaveLicense(CachedLicense license)
        {
            string json = JsonConvert.SerializeObject(license);
            File.WriteAllText(filePath, json);
        }

        public static CachedLicense? LoadLicense()
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CachedLicense>(json);
        }
    }
}