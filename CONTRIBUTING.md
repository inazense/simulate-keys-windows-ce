# Contributing to simulate-keys-windows-ce

Thank you for your interest in contributing! This document explains how to report bugs, propose features, and submit code changes.

---

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Reporting Bugs](#reporting-bugs)
- [Proposing Features](#proposing-features)
- [Pull Request Workflow](#pull-request-workflow)
- [Code Style](#code-style)
- [Review Process](#review-process)

---

## Code of Conduct

This project follows the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you agree to uphold it. Report unacceptable behavior to jclaver64@gmail.com.

---

## Reporting Bugs

Before opening a bug report, search [existing issues](https://github.com/inazense/simulate-keys-windows-ce/issues) to avoid duplicates.

When you open a new issue, include:

- **Device model** (e.g. Zebra MC3200, Symbol MC9090)
- **OS version** (e.g. Windows Mobile 6.1, Windows CE 5.2)
- **EMDK version** (if applicable)
- **Steps to reproduce** the problem
- **Expected behavior** vs. **actual behavior**
- **Any relevant logs or error messages**

Use the label `bug` when creating the issue.

---

## Proposing Features

Open an issue with the label `enhancement` and describe:

1. **The use case** — what problem does this solve?
2. **Proposed solution** — how should it work?
3. **Alternatives considered** — what else did you consider?

Please keep proposals focused. A small, well-defined change is easier to review and merge than a large refactor.

---

## Pull Request Workflow

1. **Fork** the repository and create a branch with a descriptive name:
   ```
   git checkout -b feature/add-key-combination-helper
   git checkout -b fix/process-matching-by-filename
   ```

2. **Make your changes.** Keep each commit focused on a single logical change. Write clear commit messages:
   ```
   Add helper method for sending key combinations (e.g. Ctrl+C)
   ```

3. **Update `CHANGELOG.md`** under the `[Unreleased]` section. Follow the [Keep a Changelog](https://keepachangelog.com) format (`Added`, `Changed`, `Fixed`, `Removed`).

4. **Push** your branch and **open a Pull Request** against `master`.

5. In the PR description, include:
   - **What** — what does this change do?
   - **Why** — why is this change needed?
   - **How to test** — steps to verify the behavior on a device or emulator

> Note: There is no automated CI at this time. Changes are reviewed manually by the maintainer.

---

## Code Style

This project targets **.NET Compact Framework 3.5** and must remain compatible with Visual Studio 2008.

- **Naming**: `PascalCase` for classes, methods, and public constants; `camelCase` for local variables and private fields.
- **Key codes**: Always use the named constants from `Keys.cs` instead of raw hex literals.
- **P/Invoke**: Keep all native imports (`DllImport`) grouped at the top of the class.
- **Comments**: Write comments in English. Only comment non-obvious behavior — avoid restating what the code already says.
- **No new dependencies**: Do not add NuGet packages or references that are not available in the .NET Compact Framework 3.5 base class library.
- **Compatibility first**: Any new code must compile and run on Windows CE 4.0 or later.

---

## Review Process

- The maintainer aims to review pull requests within **7 days**.
- Reviews may request changes before merging. Address all feedback before requesting a re-review.
- Once approved, PRs are merged with **squash merge** to keep a clean history.
- If a PR has not received a response within 14 days, feel free to ping the maintainer by commenting on the issue.
