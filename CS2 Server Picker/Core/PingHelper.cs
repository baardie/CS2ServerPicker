using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Utility for asynchronously pinging relay addresses.
    /// </summary>
    internal static class PingHelper
    {
        /// <summary>
        /// Sends an ICMP ping to the specified address and returns roundtrip time in milliseconds.
        /// </summary>
        /// <param name="address">IPv4 address to ping.</param>
        /// <param name="timeoutMs">Timeout in milliseconds (default: 500ms).</param>
        /// <returns>Roundtrip time in ms if successful; otherwise, null.</returns>
        public static async Task<int?> PingAddressAsync(string address, int timeoutMs = 500)
        {
            try
            {
                using var ping = new Ping();

                // Send ping and await response
                var reply = await ping.SendPingAsync(address, timeoutMs).ConfigureAwait(false);

                // Return latency if ping succeeded
                if (reply.Status == IPStatus.Success)
                    return (int)reply.RoundtripTime;
            }
            catch
            {
                // Ignore failures (e.g. unreachable, blocked ICMP)
            }

            return null;
        }
    }
}
