using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Helpers
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class VersionChecker
    {
        private static readonly string ApiUrl = "https://api.github.com/repos/baardie/CS2ServerPicker/releases/latest";

        public static async Task<string?> GetLatestVersionAsync()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("CS2ServerPicker");

            try
            {
                var json = await client.GetStringAsync(ApiUrl);
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("tag_name", out var tag))
                    return tag.GetString()?.Trim();
            }
            catch
            {
                // Optionally log or handle errors
            }

            return null;
        }

        public static bool IsNewVersionAvailable(string? latestTag, string currentVersion)
        {
            if (string.IsNullOrWhiteSpace(latestTag))
                return false;

            // Strip 'v' prefix and build metadata
            var latest = latestTag.TrimStart('v');
            var current = currentVersion.Split('+')[0];

            return !string.Equals(latest, current, StringComparison.OrdinalIgnoreCase);
        }

    }

}
