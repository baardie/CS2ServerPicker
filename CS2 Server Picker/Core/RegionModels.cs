using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Represents a single relay IP address with optional notes (e.g. ping, recommendation, emoji).
    /// </summary>
    public sealed record RelayEntry(string Address, string? Notes);

    /// <summary>
    /// Represents a region grouping multiple relay entries.
    /// </summary>
    public sealed record Region(string Code, string Name, IReadOnlyList<RelayEntry> Relays);
}
