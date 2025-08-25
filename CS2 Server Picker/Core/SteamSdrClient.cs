using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Client for retrieving Steam SDR (relay) configuration data for CS2.
    /// </summary>
    internal sealed class SteamSdrClient
    {
        // Optimized HttpClient with connection pooling and decompression
        private static readonly HttpClient _http = new(new SocketsHttpHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.All,
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
            MaxConnectionsPerServer = 4
        })
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        // Steam API endpoint for SDR config (AppID 730 = CS2)
        private const string Url = "https://api.steampowered.com/ISteamApps/GetSDRConfig/v1/?appid=730";

        /// <summary>
        /// Fetches and parses SDR POPs (regions with relay servers) from Steam API.
        /// </summary>
        /// <param name="ct">Optional cancellation token.</param>
        /// <returns>Sorted list of valid SDR POPs with relay IPs.</returns>
        public async Task<IReadOnlyList<SdrPop>> GetPopsAsync(CancellationToken ct = default)
        {
            // Create GET request with custom User-Agent
            using var req = new HttpRequestMessage(HttpMethod.Get, Url);
            req.Headers.UserAgent.Add(new ProductInfoHeaderValue("CS2ServerPicker", "1.0"));

            // Send request and ensure successful response
            using var res = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();

            // Read and deserialize JSON response stream
            await using var stream = await res.Content.ReadAsStreamAsync(ct).ConfigureAwait(false);
            var raw = await JsonSerializer.DeserializeAsync(stream, SdrJsonContext.Default.RawSdr, ct).ConfigureAwait(false)
                      ?? new RawSdr(); // Fallback to empty object if deserialization fails

            var list = new List<SdrPop>(raw.pops.Count);

            // Iterate over each POP entry
            foreach (var (code, pop) in raw.pops)
            {
                if (pop?.relays is null) continue; // Skip if no relays

                // Use description if available, fallback to uppercase region code
                var name = string.IsNullOrWhiteSpace(pop.desc) ? code.ToUpperInvariant() : pop.desc!;

                // Filter and map valid IPv4 relay addresses
                var ips = pop.relays
                             .Where(r => !string.IsNullOrWhiteSpace(r.ipv4))
                             .Select(r => new SdrRelay(r.ipv4!))
                             .ToList();

                // Add POP to list if it has valid relays
                if (ips.Count > 0)
                    list.Add(new SdrPop(code, name, ips));
            }

            // Sort POPs alphabetically by region code
            list.Sort((a, b) => string.Compare(a.Code, b.Code, StringComparison.OrdinalIgnoreCase));
            return list;
        }
    }
}
