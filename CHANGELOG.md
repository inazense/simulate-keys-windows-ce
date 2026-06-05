# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.0] - 2017-05-08

### Added
- Initial implementation of keyboard simulation via `keybd_event` (`coredll.dll`)
- `Keys.cs` with 100+ virtual key code constants: alphanumeric, function keys (F1–F24), navigation, modifier, keypad, and special keys
- `ProcessCE.cs` for Windows CE process enumeration using the `toolhelp.dll` snapshot API (based on work by Frank van de Ven, CPOL license)
- Support for targeting any running process by executable name and setting it as the foreground window before sending events
- GPLv3 license

[Unreleased]: https://github.com/inazense/simulate-keys-windows-ce/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/inazense/simulate-keys-windows-ce/releases/tag/v1.0.0
