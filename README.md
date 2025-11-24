# FreeDayPlugin

A CounterStrikeSharp plugin for CS2 that implements "Sut" a jailbreak Free Day mechanic. Players on the Terrorist team can opt in to become a Sut, giving up hostility in exchange of freedom.

## Installation

1. Install [CS2MenuManager](https://github.com/schwarper/CS2MenuManager)
2. Download the latest release from the [Releases](https://github.com/Reborn-CS2-Plugins/FreeDayPlugin/releases) page
3. Extract and drop the compiled plugin into `addons/counterstrikesharp/plugins/`
4. Restart your CS2 server
5. Configure settings from `addons/counterstrikesharp/configs/plugins/FreeDayPlugin`

## Features

- **Weapon removal** — Suts lose all weapons but gain invulnerability, cannot pick weapons up anymore
- **Custom model** — Suts display a custom character model (configurable path)
- **Permission control** — authorized admins or the warden can disable/enable Sut mode or reset all active Suts

### Commands

- `css_sutol` / `css_sütol` — Player command to become a Sut (if enabled and alive on Terrorist team)
- `css_sut0` / `css_süt0` — Admin command to disable Sut mode (respawns all Suts and resets them)
- `css_sut1` / `css_süt1` — Admin command to re-enable Sut mode
- `css_resetsut` / `css_resetsüt` — Admin command to reset all active Suts (respawn at saved position)

### Configuration

The plugin uses a JSON config file at `addons/counterstrikesharp/configs/plugins/FreeDayPlugin/FreeDayPlugin.json`:

- `command_permission` — Admin roles allowed to control Sut (default: `["@css/slay", "@jailbreak/warden", "@css/warden"]`)
- `command_aliases_sut` — Aliases for the player Sut command (default: `["css_sutol", "css_sütol"]`)
- `command_aliases_disable_sut` — Aliases to disable Sut mode (default: `["css_sut0", "css_süt0"]`)
- `command_aliases_enable_sut` — Aliases to enable Sut mode (default: `["css_sut1", "css_süt1"]`)
- `command_aliases_reset_sut` — Aliases to reset all Suts (default: `["css_resetsut", "css_resetsüt"]`)
- `SutModelPath` — Path to the custom Sut model (default: `"characters/models/ambrosian/reborn/sut/sut.vmdl"`)

*Supported languages: TR*

## License

This project is licensed under the **Creative Commons Attribution-NonCommercial 4.0 International License (CC BY-NC 4.0)**.

You are free to:
- Share, copy, and redistribute the material in any medium or format for **non-commercial purposes**.

Under the following terms:
- **Attribution**: You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
- **NonCommercial**: You may not use the material for commercial purposes (e.g., selling or monetizing the software).

For full details, please see the [CC BY-NC 4.0 License](https://creativecommons.org/licenses/by-nc/4.0/legalcode).
