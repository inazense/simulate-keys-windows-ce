# simulate-keys-windows-ce

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
[![Platform: Windows CE](https://img.shields.io/badge/Platform-Windows%20CE%204.0%2B-lightgrey)](https://learn.microsoft.com/en-us/windows/win32/wincebase/windows-ce-foundations)
[![.NET Compact Framework](https://img.shields.io/badge/.NET-Compact%20Framework%203.5-purple)](https://learn.microsoft.com/en-us/dotnet/framework/index)

A C# library for simulating keyboard input on **Windows CE / Windows Mobile** devices. It sends OS-level key events to any running process by name, enabling automation and testing scenarios on embedded handheld devices.

---

## Table of Contents

- [Objective](#objective)
- [Architecture](#architecture)
- [Requirements](#requirements)
- [Compatibility](#compatibility)
- [Usage](#usage)
- [Available Key Codes](#available-key-codes)
- [Limitations](#limitations)
- [Contributing](#contributing)
- [License](#license)

---

## Objective

On Windows CE devices (e.g. Zebra/Symbol handheld scanners), there is no straightforward API to programmatically send keyboard events to an arbitrary running application. This project demonstrates how to:

1. Enumerate all running processes on the device.
2. Target a specific process by executable name.
3. Set that process as the foreground window.
4. Inject OS-level keyboard events using `keybd_event` from `coredll.dll`.

This is especially useful for **test automation**, **barcode scanner integrations**, and **kiosk-style applications** that need to drive legacy Windows CE software without modifying it.

---

## Architecture

```
┌─────────────────────────────────────────────────────┐
│                     Program.cs                      │
│  (entry point: select key, find process, send key)  │
└──────────────┬────────────────────┬─────────────────┘
               │                    │
               ▼                    ▼
  ┌────────────────────┐  ┌──────────────────────────┐
  │    ProcessCE.cs    │  │         Keys.cs           │
  │ Enumerates running │  │ 100+ key code constants   │
  │ processes via      │  │ (hex values, VK codes)    │
  │ toolhelp.dll       │  └──────────────────────────┘
  └──────────┬─────────┘
             │
             ▼
  ┌──────────────────────────────────────────────────┐
  │               coredll.dll (native)               │
  │  SetForegroundWindow()  │  keybd_event()         │
  └──────────────────────────────────────────────────┘
```

**Key files:**

| File | Responsibility |
|------|---------------|
| `Program.cs` | Entry point. Selects the key, finds the target process, sends the event. |
| `Keys.cs` | Static constants for all supported key codes (virtual key values). |
| `ProcessCE.cs` | Windows CE process enumeration using `toolhelp.dll` snapshot API. |

---

## Requirements

| Requirement | Details |
|-------------|---------|
| IDE | Visual Studio 2008 |
| SDK | [Windows Mobile 6 Professional SDK](https://www.microsoft.com/en-us/download/details.aspx?id=3138) |
| EMDK | [EMDK for .NET 2.9](https://www.zebra.com/us/en/support-downloads/software/developer-tools/emdk-for-net.html) (Zebra devices) |
| .NET | .NET Compact Framework 3.5+ |

---

## Compatibility

| Platform | Status |
|----------|--------|
| Windows CE 4.0 | Supported |
| Windows CE 5.0 / 5.2 | Supported (tested) |
| Windows Mobile 6.x | Supported (tested on 6.1) |
| Windows Embedded Compact 7 | Untested ([help wanted](https://github.com/inazense/simulate-keys-windows-ce/issues)) |
| Windows Embedded Compact 2013 | Untested ([help wanted](https://github.com/inazense/simulate-keys-windows-ce/issues)) |
| Zebra/Symbol MC3000, MC9000 | Tested |

> **Note:** This project targets the .NET Compact Framework and uses CE-specific native DLLs (`coredll.dll`, `toolhelp.dll`). It **cannot** run on desktop Windows.

---

## Usage

### 1. Clone and open the solution

```
git clone https://github.com/inazense/simulate-keys-windows-ce.git
```

Open `SimulateKeys/SimulateKeys.sln` in Visual Studio 2008.

### 2. Configure the target process and key

In `Program.cs`, change two lines:

```csharp
// Line 60 — choose the key to simulate (see Keys.cs for all options)
byte key = Keys.ENTER;

// Line 63 — replace with the full path or name of your target process
IntPtr myProcessPID = getMyProcessPID("MyProcess.exe");
```

### 3. Example: send F2 to a custom application

```csharp
static void Main(string[] args)
{
    byte key = Keys.F2;                                  // Key to send
    IntPtr pid = getMyProcessPID("MyInventoryApp.exe"); // Target process

    if (pid != IntPtr.Zero)
    {
        Process myProcess = Process.GetProcessById(pid.ToInt32());
        if (myProcess != null)
        {
            SetForegroundWindow(myProcess.MainWindowHandle);
            keybd_event(key, 0, KEYEVENT_KEYDOWN, 0);
            keybd_event(key, 0, KEYEVENT_KEYUP, 0);
        }
    }
}
```

### 4. Example: send multiple keys sequentially

```csharp
byte[] sequence = { Keys.KEY_H, Keys.KEY_I };

foreach (byte key in sequence)
{
    SetForegroundWindow(foregroundHandle);
    keybd_event(key, 0, KEYEVENT_KEYDOWN, 0);
    keybd_event(key, 0, KEYEVENT_KEYUP, 0);
    System.Threading.Thread.Sleep(50); // brief pause between keystrokes
}
```

See the [`examples/`](examples/) folder for more usage scenarios.

### 5. Deploy and run

Build the solution in Release mode, deploy the `.exe` to your Windows CE device, and execute it. The target application must be running when the key injection occurs.

---

## Available Key Codes

All key codes are defined in `Keys.cs` as `public const byte` values. Key groups:

| Group | Examples |
|-------|---------|
| Control keys | `Keys.ENTER`, `Keys.ESC`, `Keys.BACKSPACE`, `Keys.TAB` |
| Modifier keys | `Keys.SHIFT`, `Keys.CTRL`, `Keys.ALT` |
| Navigation | `Keys.UPARROW`, `Keys.DOWNARROW`, `Keys.HOME`, `Keys.END`, `Keys.PAGEUP` |
| Function keys | `Keys.F1` … `Keys.F24` |
| Alphanumeric | `Keys.KEY_A` … `Keys.KEY_Z`, `Keys.KEY_0` … `Keys.KEY_9` |
| Keypad | `Keys.KEYPAD_0` … `Keys.KEYPAD_9` |
| Special | `Keys.INS`, `Keys.DEL`, `Keys.CAPSLOCK`, `Keys.NUMLOCK`, `Keys.SCROLLLOCK` |
| Media | `Keys.Play`, `Keys.Zoom` |

A full table with hex values is planned — see [#1](https://github.com/inazense/simulate-keys-windows-ce/issues/1).

---

## Limitations

- **Foreground requirement**: `keybd_event` sends input to the current foreground window. The code sets the target process as foreground before injecting, but rapid focus changes may cause events to land in the wrong application.
- **8-bit key codes only**: Virtual key codes are `byte` values (0x00–0xFF). Extended keys that require a 16-bit scan code are not directly supported.
- **No simultaneous key combinations**: Sending `Ctrl+C` requires two sequential `keybd_event` calls (key down Ctrl, key down C, key up C, key up Ctrl). There is no built-in helper for this pattern yet — see [#2](https://github.com/inazense/simulate-keys-windows-ce/issues/2).
- **No mouse event emulation**: Only keyboard events are supported. Mouse click simulation would require a separate `mouse_event` P/Invoke implementation.
- **Process matching by full path**: `ProcessCE.FindProcessPID` matches on the full executable path. The default example uses just the filename; adjust accordingly for your environment.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on opening issues, submitting pull requests, and code style.

Please note that this project is released with a [Contributor Code of Conduct](CODE_OF_CONDUCT.md). By participating you agree to abide by its terms.

---

## License

This project is licensed under the [GNU General Public License v3.0](LICENSE).

`ProcessCE.cs` is based on code by Frank van de Ven (2009) and is licensed under the [Code Project Open License (CPOL)](https://www.codeproject.com/info/cpol10.aspx).
