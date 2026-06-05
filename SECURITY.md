# Security Policy

## Supported Versions

Only the latest release receives security fixes.

| Version | Supported |
|---------|-----------|
| 1.0.x   | Yes       |

## Reporting a Vulnerability

**Do not open a public GitHub issue for security vulnerabilities.**

Send a private email to **jclaver64@gmail.com** with:

- A description of the vulnerability
- Steps to reproduce it
- The potential impact

You can expect an acknowledgement within **72 hours**. We will coordinate a fix and disclosure timeline with you before publishing anything publicly.

## Security Context

This library injects OS-level keyboard events into a target process using `keybd_event` from `coredll.dll`. Keep the following in mind:

- Only use this on applications you own or have explicit authorization to automate.
- Sending key events to system processes on a Windows CE device may cause unexpected behavior.
- This tool is intended for legitimate automation and testing scenarios on embedded devices.
