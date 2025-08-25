using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Represents a single IPv4 relay endpoint.
    /// </summary>
    public sealed record SdrRelay(string ipv4);

    /// <summary>
    /// Represents a POP (point of presence) region with a display name and associated relays.
    /// </summary>
    public sealed record SdrPop(string Code, string Name, IReadOnlyList<SdrRelay> Relays);

    /// <summary>
    /// Raw deserialization target for the SDR config root object.
    /// Maps region codes to POP definitions.
    /// </summary>
    internal sealed class RawSdr
    {
        public Dictionary<string, RawPop> pops { get; set; } = new();
    }

    /// <summary>
    /// Raw POP definition containing optional description and relay list.
    /// </summary>
    internal sealed class RawPop
    {
        public string? desc { get; set; } // Optional display name
        public RawRelay[]? relays { get; set; } // Array of relay endpoints
    }

    /// <summary>
    /// Raw relay definition containing optional IPv4 address.
    /// </summary>
    internal sealed class RawRelay
    {
        public string? ipv4 { get; set; } // IPv4 address of the relay
    }
}
