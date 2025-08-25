using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace CS2_Server_Picker.Core
{
    /// <summary>
    /// Manages Windows Firewall rules for blocking CS2 SDR relay traffic.
    /// </summary>
    internal sealed class FirewallManager
    {
        private const string RulePrefix = "CS2 Server Picker";
        // Conservative limit to avoid COM's RemoteAddresses string overflow
        private const int RemoteCsvSoftLimit = 3500;

        /// <summary>
        /// Gets the firewall policy COM object.
        /// </summary>
        private static INetFwPolicy2 GetPolicy()
            => (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2")!
            )!;

        /// <summary>
        /// Removes all firewall rules created by this app.
        /// </summary>
        public void RemoveAll()
        {
            var policy = GetPolicy();
            var toRemove = new List<string>();

            // Collect matching rule names
            foreach (INetFwRule r in policy.Rules)
            {
                if (r.Name.StartsWith(RulePrefix, StringComparison.Ordinal))
                    toRemove.Add(r.Name);
            }

            // Remove them in bulk
            foreach (var name in toRemove)
                policy.Rules.Remove(name);
        }

        /// <summary>
        /// Applies a set of blocked addresses under a labeled rule group.
        /// </summary>
        /// <param name="ruleLabel">Label to group rules under.</param>
        /// <param name="blocked">List of IPs to block.</param>
        public void ApplyBlockedAddresses(string ruleLabel, IReadOnlyList<string> blocked)
        {
            var policy = GetPolicy();
            var labelPrefix = $"{RulePrefix} - {ruleLabel}";

            // Remove any existing rules for this label
            var toRemove = new List<string>();
            foreach (INetFwRule r in policy.Rules)
            {
                if (r.Name.StartsWith(labelPrefix, StringComparison.Ordinal))
                    toRemove.Add(r.Name);
            }
            foreach (var name in toRemove)
                policy.Rules.Remove(name);

            if (blocked.Count == 0)
                return;

            // Chunk IPs into safe CSV blocks
            var chunks = ChunkAddresses(blocked, RemoteCsvSoftLimit);
            for (int i = 0; i < chunks.Count; i++)
            {
                var suffix = chunks.Count == 1 ? "" : $" #{i + 1}";
                var name = $"{labelPrefix}{suffix}";

                // Add outbound and inbound rules for each chunk
                AddRule(policy, name, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT, chunks[i]);
                AddRule(policy, name + " (Inbound)", NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, chunks[i]);
            }
        }

        /// <summary>
        /// Adds a firewall rule with the given name, direction, and remote IPs.
        /// </summary>
        private static void AddRule(INetFwPolicy2 policy, string name, NET_FW_RULE_DIRECTION_ dir, string remoteCsv)
        {
            var rule = (INetFwRule2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FWRule")!
            )!;
            rule.Name = name;
            rule.Description = "Blocks selected CS2 SDR relays.";
            rule.Enabled = true;
            rule.Direction = dir;
            rule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            rule.InterfaceTypes = "All";
            rule.RemoteAddresses = remoteCsv;
            rule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;

            policy.Rules.Add(rule);
        }

        /// <summary>
        /// Chunks a list of IP addresses into CSV strings that stay under a soft length limit.
        /// </summary>
        private static List<string> ChunkAddresses(IReadOnlyList<string> addrs, int softLimit)
        {
            var result = new List<string>(Math.Max(1, addrs.Count / 150));
            var sb = new StringBuilder(capacity: Math.Min(8192, addrs.Count * 16));

            for (int i = 0; i < addrs.Count; i++)
            {
                var addr = addrs[i];
                if (sb.Length == 0)
                {
                    sb.Append(addr);
                }
                else
                {
                    // If adding this address would exceed the soft limit, start a new chunk
                    if (sb.Length + 1 + addr.Length > softLimit)
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                        sb.Append(addr);
                    }
                    else
                    {
                        sb.Append(',').Append(addr);
                    }
                }
            }

            if (sb.Length > 0)
                result.Add(sb.ToString());

            return result;
        }
    }
}
