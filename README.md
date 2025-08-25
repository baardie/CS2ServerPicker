# 🎯 CS2 Server Picker (WinForms)

A modern, designer-friendly Windows app for customizing your CS2 server experience.  
Ping, filter, and firewall Steam Datagram Relay (SDR) regions with intuitive controls, emoji-enhanced feedback, and dynamic recommendations.

## 📥 Want to Download It?

Grab the latest release here:  
👉 [CS2 Server Picker v1.0.0](https://github.com/baardie/CS2ServerPicker/releases/tag/v1.0.0)

Just extract the `.zip` file and run the `.exe` — simple, fast, and ready to go!

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
   `git clone https://github.com/baardie/cs2-server-picker.git`

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

## 🔧 TODO
- [ ] ** Add release to github (its 5am atm)
    
- [ ] **Custom User Presets**  
  Allow users to define and save their own region combinations.  
  - UI: Add a “Custom” radio group with editable region checkboxes  
  - UX: Persist selections across sessions using local config or registry  
  - Feedback: Highlight custom presets with a subtle accent color or emoji

- [ ] **Ping Analysis & Recommendations**  
  Measure live ping to all regions and suggest optimal choices dynamically.  
  - Logic: Implement async ping sweep with timeout handling  
  - UI: Display recommended region with animated pulse or fade-in  
  - UX: Show “Best Match” label with confidence score or emoji indicator  
  - Performance: Ensure zero UI freezing during pinging
    
---

## 🧩 Contributing

Pull requests are welcome!  
If you'd like to add new presets, improve animations, or refactor ping logic, feel free to fork and submit a PR.

---

## 📄 License

GPL-3.0 license

---

## 🙌 Credits

Built with care by [baardie](https://github.com/baardie),  
a creative technologist blending systems-level engineering with delightful UI design.

## ☕ Fuel My Future Updates

I craft small, thoughtful apps with a focus on clarity, performance, and user delight.  
If you’ve enjoyed using one of them, consider fueling future updates — in coffee!

Your donation helps cover maintenance, midnight coding sessions, and the occasional espresso-powered breakthrough.  
Thanks for being part of the journey 💙

[![Buy Me a Coffee](https://github.com/baardie/CS2ServerPicker/blob/master/donation.png?raw=true)](https://www.paypal.com/donate/?business=96PVNH58EAZXJ&no_recurring=0&item_name=Your+donation+helps+cover+occasional+caffeine-fueled+midnight+sessions%21+%0AThanks+for+being+part+of+the+journey+%F0%9F%92%99&currency_code=GBP)



