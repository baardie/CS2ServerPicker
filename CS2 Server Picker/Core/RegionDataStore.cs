using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Provides cached access to region data derived from Steam SDR POPs.
    /// </summary>
    internal static class RegionDataStore
    {
        // Lazy async cache so we only fetch once per run
        private static readonly Lazy<Task<IReadOnlyList<Region>>> _regions
            = new(() => LoadAsync());

        /// <summary>
        /// Gets the cached region list, fetching from the API if needed.
        /// </summary>
        public static Task<IReadOnlyList<Region>> GetRegionsAsync() => _regions.Value;

        /// <summary>
        /// Fetches SDR POPs and maps them to Region objects.
        /// </summary>
        private static async Task<IReadOnlyList<Region>> LoadAsync()
        {
            var client = new SteamSdrClient();
            var pops = await client.GetPopsAsync().ConfigureAwait(false);

            // Map SdrPop -> Region
            var regions = new List<Region>(pops.Count);
            foreach (var pop in pops)
            {
                // Convert each SdrRelay to a RelayEntry with null notes
                var relays = pop.Relays
                    .Select(r => new RelayEntry(r.ipv4, null))
                    .ToArray();

                // Create Region with code, name, and relay list
                regions.Add(new Region(pop.Code, pop.Name, relays));
            }

            return regions;
        }
    }
}
