using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Utility for computing relay addresses to block based on region filtering.
    /// </summary>
    internal static class RulePlanner
    {
        /// <summary>
        /// Given all regions and a set of allowed region codes, returns a de‑duplicated,
        /// sorted list of addresses to block.
        /// </summary>
        /// <param name="allRegions">All known regions with relay endpoints.</param>
        /// <param name="allowedRegionCodes">Set of region codes that should not be blocked.</param>
        /// <returns>Sorted, unique list of relay IPs to block.</returns>
        public static IReadOnlyList<string> ComputeBlockedAddresses(
            IReadOnlyList<Region> allRegions,
            IReadOnlySet<string> allowedRegionCodes)
        {
            // Preallocate generously to reduce List growth overhead
            var list = new List<string>(capacity: allRegions.Count * 8);

            foreach (var region in allRegions)
            {
                // Skip regions that are explicitly allowed
                if (allowedRegionCodes.Contains(region.Code))
                    continue;

                // Collect all valid relay addresses from disallowed regions
                foreach (var relay in region.Relays)
                {
                    if (!string.IsNullOrWhiteSpace(relay.Address))
                        list.Add(relay.Address);
                }
            }

            // Sort for deterministic output and easier deduplication
            list.Sort(StringComparer.Ordinal);

            // Deduplicate in-place using a linear scan
            var dedup = new List<string>(list.Count);
            string? last = null;
            foreach (var addr in list)
            {
                if (!string.Equals(addr, last, StringComparison.Ordinal))
                    dedup.Add(addr);
                last = addr;
            }

            return dedup;
        }
    }
}
