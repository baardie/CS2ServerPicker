using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Source-generated JSON context for deserializing RawSdr.
    /// Improves performance by avoiding runtime reflection.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)] // Compact output
    [JsonSerializable(typeof(RawSdr))] // Register RawSdr for generation
    internal partial class SdrJsonContext : JsonSerializerContext { }
}
