# simulate-keys-windows-ce
How to simulate and send keys in Windows CE applications.
This project simulates the key-pressed actions on devices with Windows CE sending an hexadecimal code to the choosen process.
This code uses the ***Terranova.API***

## Previous requeriments
- Visual Studio 2008
- EMDK for .net 2.9 (https://www.zebra.com/us/en/support-downloads/software/developer-tools/emdk-for-net.html)
- Windows SDK 6.0 (https://www.microsoft.com/en-us/download/details.aspx?id=3138)
- .net Framework 3.5 at least

## How it works
This example gets the name of the process we want to send a key (in this case, the Enter key), set it as foreground window and then send the key.

## What I need to change?
You need to replace line 63 on _Program.cs_, ```MyProcess.exe``` by your own process name.
```
IntPtr myProcessPID = getMyProcessPID("MyProcess.exe");
```
And, in line 60 of same file, you can select what key simulate. All the possible keys are in ```Keys.cs```. Just replace that line by a key of your preference
```
byte key = Keys.ENTER;
```
