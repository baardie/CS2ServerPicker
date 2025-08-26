using CS2_Server_Picker.Core;
using CS2_Server_Picker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Region = CS2_Server_Picker.Core.Region;
using Timer = System.Windows.Forms.Timer;

namespace CS2_Server_Picker
{
    public partial class MainForm : Form
    {
        #region Fields

        private readonly FirewallManager _fw = new();
        private readonly BindingSource _bindingSource = new();
        private SortableBindingList<RegionRow> _rows = new();

        private readonly Timer _pingTimer = new() { Interval = 120_000 };
        private readonly Timer _uiUpdateTimer = new();

        private bool _pingCompleted;
        private volatile bool _pendingUiUpdate;
        private bool _statusLocked = false;
        private int _completedCount = 0;
        private int _lastCompletedCount = 0;
        private readonly int _maxConcurrency = Environment.ProcessorCount * 2;

        private const string FastestKey = "Fastest Servers (<30ms)";
        private const string GoodKey = "Good Performance (<60ms)";
        private const string AcceptableKey = "Acceptable Performance (<90ms)";

        Dictionary<string, string> geoPresetMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // 🇬🇧 United Kingdom
            ["GB"] = "UK Only",

            // 🇫🇷 France, 🇳🇱 Netherlands, 🇸🇪 Sweden, 🇪🇸 Spain, 🇩🇪 Germany
            ["FR"] = "EU Core",
            ["NL"] = "EU Core",
            ["SE"] = "EU Core",
            ["ES"] = "EU Core",
            ["DE"] = "EU Core",

            // 🇺🇸 United States, 🇨🇦 Canada
            ["US"] = "NA East",
            ["CA"] = "NA East",

            // 🇮🇳 India
            ["IN"] = "India",

            // 🇯🇵 Japan, 🇰🇷 South Korea, 🇨🇳 China
            ["JP"] = "Japan/Korea",
            ["KR"] = "Japan/Korea",
            ["CN"] = "China Mainland",

            // 🇸🇬 Singapore, 🇭🇰 Hong Kong
            ["SG"] = "SEA",
            ["HK"] = "SEA",

            // 🇦🇺 Australia, 🇳🇿 New Zealand
            ["AU"] = "Australia",
            ["NZ"] = "Oceania",

            // 🇧🇷 Brazil, 🇦🇷 Argentina, 🇨🇱 Chile, 🇵🇪 Peru
            ["BR"] = "South America",
            ["AR"] = "South America",
            ["CL"] = "South America",
            ["PE"] = "South America",

            // 🇿🇦 South Africa
            ["ZA"] = "Africa",

            // 🇦🇪 United Arab Emirates
            ["AE"] = "Middle East",

            // 🇵🇱 Poland, 🇦🇹 Austria, 🇫🇮 Finland
            ["PL"] = "Eastern Europe",
            ["AT"] = "Eastern Europe",
            ["FI"] = "Eastern Europe",

            // 🇺🇦 Ukraine, 🇷🇺 Russia
            ["UA"] = "Eastern Europe",
            ["RU"] = "Eastern Europe",

            // 🌐 Fallback
            ["IE"] = "EU Core",
            ["IT"] = "EU Core",
            ["MX"] = "North America",
            ["TH"] = "SEA",
            ["PH"] = "SEA",
            ["MY"] = "SEA",
            ["ID"] = "SEA",
            ["BD"] = "South Asia",
            ["PK"] = "South Asia",
            ["LK"] = "South Asia",
            ["TW"] = "East Asia",
            ["VN"] = "SEA",
            ["EG"] = "Middle East",
            ["NG"] = "Africa",
            ["KE"] = "Africa"
        };

        private readonly Dictionary<string, string[]> _presetMap = new()
        {
            ["UK Only"] = new[] { "lhr" },
            ["EU Core"] = new[] { "lhr", "ams", "fra", "sto", "mad" },
            ["NA East"] = new[] { "iad", "ord", "yyz" },
            ["Asia Core"] = new[] { "bom", "maa", "sin", "hkg", "tyo", "tsn", "sel" },
            ["SEA"] = new[] { "sin", "hkg" },
            ["East Asia"] = new[] { "tyo", "tsn", "sel", "hkg" },
            ["India"] = new[] { "bom", "maa" },
            ["North America"] = new[] { "ord", "iad", "yyz", "lax", "dfw" },
            ["South Asia"] = new[] { "bom", "maa" },
            ["Japan/Korea"] = new[] { "tyo", "tsn", "sel" },
            ["Australia"] = new[] { "syd" },
            ["Global Core"] = new[] { "lhr", "fra", "ams", "ord", "iad", "sin", "tyo", "syd" },
            ["South America"] = new[] { "gru", "eze", "lim", "scl" },
            ["Middle East"] = new[] { "dxb" },
            ["Africa"] = new[] { "jnb" },
            ["Oceania"] = new[] { "syd" },
            ["Eastern Europe"] = new[] { "vie", "waw", "hel" },
            ["China Mainland"] = new[] { "ctu", "pek", "tsn", "sha" }
        };

        #endregion

        #region Constructor & Initialization

        public MainForm()
        {
            InitializeComponent();
            WireEvents();
            InitializeUI();
            _pingTimer.Tick += async (_, __) => await PingCycleAsync();
            _pingTimer.Start();
        }

        private void WireEvents()
        {
            Load += MainForm_Load;
            btnApply.Click += async (_, __) => await ApplyAsync();
            btnRefreshRegions.Click += async (_, __) => await PingCycleAsync();
            btnUnblock.Click += async (_, __) => await UnblockAllAsync();
            btnClearAllowed.Click += async (_, __) => await ClearAllowedAsync();
            cmbPresets.SelectedIndexChanged += (_, __) => ApplyPreset();
            gridRegions.CellClick += GridRegions_CellClick;
            gridRegions.CellFormatting += GridRegions_CellFormatting;
            gridRegions.CellToolTipTextNeeded += GridRegions_CellToolTipTextNeeded;
            btnSortPing.Click += (_, __) => SortByPing();
        }

        private void InitializeUI()
        {
            // Grid setup
            SetupGrid();
            gridRegions.DataSource = _bindingSource;

            // Progress bar & status
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            SetStatus("Loading regions...");

            // UI throttle timer
            _uiUpdateTimer.Interval = 250;
            _uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            _uiUpdateTimer.Start();
        }

        private void SetupGrid()
        {
            gridRegions.AutoGenerateColumns = false;
            gridRegions.ScrollBars = ScrollBars.Vertical;
            gridRegions.RowHeadersVisible = false;
            gridRegions.BorderStyle = BorderStyle.None;

            foreach (DataGridViewColumn col in gridRegions.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
                col.AutoSizeMode = col.Name == "colPing"
                    ? DataGridViewAutoSizeColumnMode.Fill
                    : DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void BindPresets()
        {
            cmbPresets.Items.Clear();
            cmbPresets.Items.AddRange(_presetMap.Keys.ToArray());
        }

        #endregion

        #region Load & Ping Cycle

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            try
            {

                string? latestTag = await VersionChecker.GetLatestVersionAsync();
                string currentVersion = Application.ProductVersion;

                if (VersionChecker.IsNewVersionAvailable(latestTag, currentVersion))
                {
                    MessageBox.Show($"A new version ({latestTag}) is available!", "Update Available",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var regions = await Task.Run(() => RegionDataStore.GetRegionsAsync());

                InitializeRows(regions);
                BindPresets();

                await PingAllRegionsAsync(regions);
                await InitializeGeo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load regions: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task InitializeGeo()
        {
            var countryCode = await GeoHelper.GetCountryCodeAsync();
            if (countryCode == null) return;

            if (!geoPresetMap.TryGetValue(countryCode, out var presetKey)) return;
            if (!_presetMap.TryGetValue(presetKey, out var regionCodes)) return;

            var regions = await Task.Run(() => RegionDataStore.GetRegionsAsync());

            var pingResults = new List<(string Code, int? Ping)>();
            foreach (var code in regionCodes)
            {
                var region = regions.FirstOrDefault(r => r.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
                if (region == null) continue;

                var bestPing = int.MaxValue;
                foreach (var relay in region.Relays)
                {
                    var ms = await PingHelper.PingAddressAsync(relay.Address);
                    if (ms.HasValue && ms.Value < bestPing)
                        bestPing = ms.Value;
                }

                pingResults.Add((code, bestPing == int.MaxValue ? null : bestPing));
            }

            var avgPing = pingResults.Where(p => p.Ping.HasValue).Select(p => p.Ping.Value).DefaultIfEmpty(int.MaxValue).Average();

            if (avgPing < 60)
            {
                cmbPresets.SelectedItem = presetKey;
                //ApplyPreset();
                SetStatus($"🌍 You're in {countryCode}, and '{presetKey}' servers are averaging {avgPing:F0}ms — applied automatically.");
            }
            else
            {
                SetStatus($"🌍 You're in {countryCode}, but '{presetKey}' servers are averaging {avgPing:F0}ms — not applied automatically.");
            }
        }

        private void InitializeRows(IReadOnlyList<Region> regions)
        {
            var list = regions
                .Select(r => new RegionRow(
                    r.Code,
                    r.Name,
                    r.Relays?.Count ?? 0,
                    false))
                .ToList();

            _rows = new SortableBindingList<RegionRow>(list);
            _bindingSource.DataSource = _rows;

            progressBarLoading.Style = ProgressBarStyle.Continuous;
            progressBarLoading.Maximum = _rows.Count;
            _lastCompletedCount = 0;
            _pendingUiUpdate = false;
            _pingCompleted = false;
        }

        private async Task PingCycleAsync()
        {
            try
            {
                _statusLocked = false;
                SetStatus("🔄 Refreshing regions and starting ping...");
                progressBarLoading.Value = 0;
                progressBarLoading.Style = ProgressBarStyle.Marquee;

                var regions = await Task.Run(() => RegionDataStore.GetRegionsAsync());

                InitializeRows(regions);
                await PingAllRegionsAsync(regions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ping cycle failed: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Ping Logic

        private async Task PingAllRegionsAsync(IReadOnlyList<Region> regions)
        {
            var semaphore = new SemaphoreSlim(_maxConcurrency);
            int completed = 0;
            _completedCount = 0;


            var pingTasks = _rows.Select(async row =>
            {
                await semaphore.WaitAsync().ConfigureAwait(false);
                try
                {
                    var region = regions.First(r => r.Code == row.Code);
                    var best = int.MaxValue;

                    foreach (var relay in region.Relays)
                    {
                        var ms = await PingHelper.PingAddressAsync(relay.Address).ConfigureAwait(false);
                        if (ms.HasValue && ms.Value < best)
                            best = ms.Value;
                    }

                    var finalPing = best == int.MaxValue ? (int?)null : best;

                    // Marshal property update to UI thread
                    BeginInvoke(() =>
                    {
                        row.PingMs = finalPing;
                        _completedCount++;

                    });
                }
                finally
                {
                    semaphore.Release();
                    _pendingUiUpdate = true;
                    Interlocked.Increment(ref completed);

                }
            });

            await Task.WhenAll(pingTasks).ConfigureAwait(false);
            _pingCompleted = true;

            if (InvokeRequired)
                BeginInvoke((Action)UpdateRecommendedOptions);
            else
                UpdateRecommendedOptions();

            // Reset progress bar
            if (InvokeRequired)
                BeginInvoke(() => progressBarLoading.Value = 0);
            else
                progressBarLoading.Value = 0;
        }

        private void UpdateRecommendedOptions()
        {
            flowRecommended.Controls.Clear();
            AddRecommendedOption(FastestKey, _rows.Where(r => r.PingMs < 30));
            AddRecommendedOption(GoodKey, _rows.Where(r => r.PingMs < 60));
            AddRecommendedOption(AcceptableKey, _rows.Where(r => r.PingMs < 90));

            SetStatus("✅ Recommended options updated.");
            _statusLocked = true;
        }

        #endregion

        #region UI Update Throttle

        private void UiUpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (!_pendingUiUpdate) return;

            SortByPing();
            gridRegions.Invalidate();
            UpdateProgress();

            _pendingUiUpdate = false;
        }


        private void UpdateProgress()
        {
            if (_statusLocked) return;

            progressBarLoading.Value = Math.Min(_completedCount, progressBarLoading.Maximum);
            lblStatus.Text = $"Pinging servers... ({_completedCount}/{_rows.Count})";
        }

        private void SetStatus(string text) => lblStatus.Text = text;

        #endregion

        #region Event Handlers

        private void GridRegions_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = gridRegions.Columns[e.ColumnIndex];
            if (col.Name == nameof(RegionRow.Allowed)) return;

            var row = _rows[e.RowIndex];
            row.Allowed = !row.Allowed;

            gridRegions.InvalidateRow(e.RowIndex);
            gridRegions.Refresh();
        }

        private void GridRegions_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _rows.Count)
                return;

            var row = _rows[e.RowIndex];
            var colName = gridRegions.Columns[e.ColumnIndex].Name;

            // Ping column formatting
            if (colName == "colPing")
            {
                var baseFont = gridRegions.Font ?? SystemFonts.DefaultFont;
                e.CellStyle.Font = new Font("Segoe UI Emoji", baseFont.Size);

                string emoji, text;
                Color color;

                if (row.PingMs == null)
                {
                    emoji = !_pingCompleted ? "⏳" : "❌";
                    text = !_pingCompleted ? "Loading..." : "BLOCKED";
                    color = !_pingCompleted ? Color.Gray : Color.Red;
                }
                else
                {
                    var p = row.PingMs.Value;
                    text = $"{p} ms";

                    if (p < 30) { emoji = "🟢"; color = Color.ForestGreen; }
                    else if (p < 60) { emoji = "🟡"; color = Color.Orange; }
                    else if (p < 90) { emoji = "🟠"; color = Color.DarkOrange; }
                    else { emoji = "🔴"; color = Color.Red; }
                }

                e.CellStyle.ForeColor = color;
                e.Value = $"{emoji}  {text}";
                e.FormattingApplied = true;
            }
        }

        private void GridRegions_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _rows.Count)
                return;

            if (e.RowIndex < 0 ||
                gridRegions.Columns[e.ColumnIndex].Name != "colPing")
                return;

            var row = _rows[e.RowIndex];
            e.ToolTipText = row.PingMs switch
            {
                null => "Blocked or unreachable",
                < 30 => "Excellent connection",
                < 60 => "Good performance",
                < 90 => "Acceptable latency",
                _ => "High latency"
            };
        }

        private void SortByPing()
        {
            var sorted = _rows
                .OrderBy(r => r.PingMs == null)   // blocked last
                .ThenBy(r => r.PingMs ?? int.MaxValue)
                .ToList();

            gridRegions.SuspendLayout();

            _rows = new SortableBindingList<RegionRow>(sorted);
            _bindingSource.DataSource = _rows;

            gridRegions.ResumeLayout();
            gridRegions.Refresh();
        }

        #endregion

        #region Preset & Apply

        private void ApplyPreset()
        {
            if (cmbPresets.SelectedItem is not string key) return;
            if (!_presetMap.TryGetValue(key, out var codes)) return;

            var set = new HashSet<string>(codes, StringComparer.OrdinalIgnoreCase);
            foreach (var row in _rows)
                row.Allowed = set.Contains(row.Code);

            gridRegions.Refresh();
        }

        private async Task ClearAllowedAsync()
        {

            foreach (var row in _rows)
                row.Allowed = false;

            foreach (RadioButton radio in flowRecommended.Controls.OfType<RadioButton>())
                radio.Checked = false;

            gridRegions.Refresh();
        }

        private async Task ApplyAsync()
        {
            btnApply.Enabled = false;
            try
            {
                var allowed = _rows
                    .Where(r => r.Allowed)
                    .Select(r => r.Code)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                var regions = await Task.Run(() => RegionDataStore.GetRegionsAsync());
                var blocked = await Task.Run(() => RulePlanner.ComputeBlockedAddresses(regions, allowed));
                await Task.Run(() => _fw.ApplyBlockedAddresses(ModeLabel(allowed), blocked));

                MessageBox.Show("Firewall rules updated.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnApply.Enabled = true;
            }
        }

        private async Task UnblockAllAsync()
        {
            btnUnblock.Enabled = false;
            try
            {
                await Task.Run(() => _fw.RemoveAll());
                MessageBox.Show("All CS2 Server Picker rules removed.", "Done",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                await PingCycleAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUnblock.Enabled = true;
            }
        }

        private string ModeLabel(HashSet<string> allowed)
        {
            if (chkBlockAllButSelected.Checked)
            {
                return allowed.Count == 0
                    ? "Blocking All (no region allowed)"
                    : "Blocking Others (allowed: " + string.Join(", ", allowed) + ")";
            }
            return "Blocking Selected";
        }

        #endregion

        #region Recommended Helpers

        private void AddRecommendedOption(string label, IEnumerable<RegionRow> rows)
        {
            var codes = rows.Select(r => r.Code).Distinct().ToArray();
            if (codes.Length == 0) return;

            var radio = new RadioButton
            {
                Text = $"{label} — {codes.Length} regions",
                Tag = codes,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent, // Avoid Transparent
            };

            radio.FlatAppearance.BorderSize = 0;
            radio.FlatAppearance.CheckedBackColor = flowRecommended.BackColor;
            radio.FlatAppearance.MouseOverBackColor = flowRecommended.BackColor;

            radio.CheckedChanged += (_, __) =>
            {
                if (!radio.Checked) return;
                foreach (var row in _rows)
                    row.Allowed = codes.Contains(row.Code);

                gridRegions.Refresh();
                SetStatus($"Applied recommended preset: {label}");
                radio.Font = new Font(radio.Font, FontStyle.Bold);
            };

            flowRecommended.Controls.Add(radio);
        }

        #endregion

        #region RegionRow Definition

        private sealed class RegionRow : INotifyPropertyChanged
        {
            public string Code { get; }
            public string Name { get; }
            public int CidrCount { get; }

            private bool _allowed;
            private int? _pingMs;

            public bool Allowed
            {
                get => _allowed;
                set { if (_allowed != value) { _allowed = value; OnPropertyChanged(nameof(Allowed)); } }
            }
            public int? PingMs
            {
                get => _pingMs;
                set { if (_pingMs != value) { _pingMs = value; OnPropertyChanged(nameof(PingMs)); } }
            }

            public RegionRow(string code, string name, int cidrCount, bool allowed)
            {
                Code = code;
                Name = name;
                CidrCount = cidrCount;
                _allowed = allowed;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            private void OnPropertyChanged(string prop) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        private void buttonDonate_Click(object sender, EventArgs e)
        {
            var url = "https://www.paypal.com/donate/?business=96PVNH58EAZXJ&no_recurring=0&item_name=Your+donation+helps+cover+occasional+caffeine-fueled+midnight+sessions%21+%0AThanks+for+being+part+of+the+journey+%F0%9F%92%99&currency_code=GBP";

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open browser: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGithub_Click(object sender, EventArgs e)
        {
            var url = "https://github.com/baardie/CS2ServerPicker";

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open browser: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
    }
}
