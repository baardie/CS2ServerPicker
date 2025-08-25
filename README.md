# 🎯 CS2 Server Picker (WinForms)

A modern, designer-friendly Windows app for customizing your CS2 server experience.  
Ping, filter, and firewall Steam Datagram Relay (SDR) regions with intuitive controls, emoji-enhanced feedback, and dynamic recommendations.

---

## ✨ Features

- 🌍 **Geo-Aware Presets**  
  Automatically recommends optimal regions based on your location and live ping results.

- 📡 **Live Ping Measurement**  
  Asynchronously pings all relay endpoints with throttled UI updates and animated feedback.

- ✅ **Recommended Options**  
  Highlights the fastest, good, and acceptable regions with emoji indicators and one-click selection.

- 🔥 **Firewall Integration**  
  Applies Windows Firewall rules to block unwanted regions — outbound and inbound — with safe chunking and clear labeling.

- 🎨 **Modern UI**  
  Custom controls, Segoe UI Emoji font, animated transitions, and responsive layout for a smooth desktop experience.

---

## 🖼️ Screenshots

> Coming soon — animated previews of region selection, ping feedback, and firewall application.

---

## 🚀 Getting Started

### Requirements

- Windows 10/11
- .NET 6+
- Administrator privileges (required for firewall rule management)

### Build Instructions

1. Clone the repo  
   `git clone https://github.com/yourusername/cs2-server-picker.git`

2. Open in Visual Studio  
   Ensure NuGet packages are restored and build the solution.

3. Run as Administrator  
   The app checks for elevation before launching.

---

## 🧠 Architecture

- **Core Layer**  
  Handles region parsing, pinging, firewall rule planning, and geo detection.

- **UI Layer**  
  WinForms-based interface with custom controls, animated feedback, and dynamic presets.

- **Data Models**  
  Immutable records for relays, regions, and raw SDR config.

- **Interop**  
  Uses `NetFwTypeLib` for COM-based firewall rule management.

---

## 🛡️ Privacy & Safety

This app uses public IP-based geolocation and Steam's SDR config API.  
No personal data is stored or transmitted.  
Firewall rules are scoped to CS2 relay IPs only.

---

## 🧩 Contributing

Pull requests are welcome!  
If you'd like to add new presets, improve animations, or refactor ping logic, feel free to fork and submit a PR.

---

## 📄 License

MIT License — free to use, modify, and distribute.

---

## 🙌 Credits

Built with care by [Luke](https://github.com/yourusername),  
a creative technologist blending systems-level engineering with delightful UI design.

