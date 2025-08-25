using System.Net.Http;
using System.Text.Json;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Provides geolocation utilities based on public IP.
    /// </summary>
    public static class GeoHelper
    {
        // Shared HttpClient instance for efficient reuse
        private static readonly HttpClient _http = new();

        /// <summary>
        /// Asynchronously retrieves the user's ISO 3166-1 alpha-2 country code (e.g., "GB", "US") 
        /// using the ip-api.com geolocation service.
        /// </summary>
        /// <returns>
        /// A two-letter country code string if successful; otherwise, null.
        /// </returns>
        public static async Task<string?> GetCountryCodeAsync()
        {
            try
            {
                // Send GET request to ip-api and retrieve raw JSON response
                var json = await _http.GetStringAsync("http://ip-api.com/json/");

                // Parse the JSON response
                using var doc = JsonDocument.Parse(json);

                // Attempt to extract the "countryCode" property
                if (doc.RootElement.TryGetProperty("countryCode", out var code))
                    return code.GetString(); // Return the country code as string
            }
            catch
            {
                // Silent fail: return null if any exception occurs
            }

            return null;
        }
    }
}
