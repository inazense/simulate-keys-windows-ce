# Usage Examples

These examples show common ways to use `simulate-keys-windows-ce`. All snippets assume you have added a reference to the `SimulateKeys` project and have the P/Invoke declarations in place (see `Program.cs`).

---

## Example 1 — Send ENTER to a process (default behavior)

The simplest case: find a process by name, bring it to the foreground, and send the Enter key.

```csharp
byte key = Keys.ENTER;
IntPtr pid = getMyProcessPID("MyProcess.exe");

if (pid != IntPtr.Zero)
{
    Process target = Process.GetProcessById(pid.ToInt32());
    if (target != null)
    {
        SetForegroundWindow(target.MainWindowHandle);
        keybd_event(key, 0, KEYEVENT_KEYDOWN, 0);
        keybd_event(key, 0, KEYEVENT_KEYUP, 0);
    }
}
```

---

## Example 2 — Send a function key (F2) to another application

Replace the key constant and the process name:

```csharp
byte key = Keys.F2;
IntPtr pid = getMyProcessPID("MyInventoryApp.exe");

if (pid != IntPtr.Zero)
{
    Process target = Process.GetProcessById(pid.ToInt32());
    if (target != null)
    {
        SetForegroundWindow(target.MainWindowHandle);
        keybd_event(key, 0, KEYEVENT_KEYDOWN, 0);
        keybd_event(key, 0, KEYEVENT_KEYUP, 0);
    }
}
```

All available keys are listed in `Keys.cs`. Common function key values:

| Constant   | Hex  |
|------------|------|
| `Keys.F1`  | 0x70 |
| `Keys.F2`  | 0x71 |
| `Keys.F3`  | 0x72 |
| `Keys.F4`  | 0x73 |
| `Keys.F5`  | 0x74 |
| `Keys.F12` | 0x7B |

---

## Example 3 — Send multiple keys sequentially

Loop over an array of key codes with a short pause between each keystroke:

```csharp
IntPtr pid = getMyProcessPID("MyWarehouseApp.exe");
if (pid == IntPtr.Zero) return;

Process target = Process.GetProcessById(pid.ToInt32());
if (target == null) return;

IntPtr handle = target.MainWindowHandle;

byte[] sequence = new byte[]
{
    Keys.KEY_H,
    Keys.KEY_E,
    Keys.KEY_L,
    Keys.KEY_L,
    Keys.KEY_O,
    Keys.ENTER
};

foreach (byte key in sequence)
{
    SetForegroundWindow(handle);
    keybd_event(key, 0, KEYEVENT_KEYDOWN, 0);
    keybd_event(key, 0, KEYEVENT_KEYUP, 0);
    System.Threading.Thread.Sleep(50); // 50 ms between keystrokes
}
```

> **Tip:** Increase the `Thread.Sleep` delay if the target application drops keystrokes under load.

---

## Example 4 — Simulate a key combination (e.g. Ctrl+C)

Send modifier key down, then the target key, then release both in reverse order:

```csharp
IntPtr handle = target.MainWindowHandle;
SetForegroundWindow(handle);

keybd_event(Keys.CTRL, 0, KEYEVENT_KEYDOWN, 0);  // hold Ctrl
keybd_event(Keys.KEY_C, 0, KEYEVENT_KEYDOWN, 0); // press C
keybd_event(Keys.KEY_C, 0, KEYEVENT_KEYUP, 0);   // release C
keybd_event(Keys.CTRL, 0, KEYEVENT_KEYUP, 0);    // release Ctrl
```

> **Note:** Key combination support is not yet officially documented. See [issue #2](https://github.com/inazense/simulate-keys-windows-ce/issues/2) for progress.
