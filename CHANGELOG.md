# Changelog

All notable changes to this template will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Template releases are tagged `template-vX.Y.Z` — see [CONTRIBUTING.md](CONTRIBUTING.md#versioning-this-template)
for why this differs from the `vX.Y.Z` tags `release.yml` watches for.

## [Unreleased]

### Added
- Initial repo scaffolding: `ci.yml`, `release.yml` (Trusted Publishing via `NuGet/login@v1`,
  no API key secrets), `docker-compose.yml` + `docker/seed.sh` (LocalStack)
- `ExampleLibrary`: multi-targets `net48;net10.0`, demonstrates a "notes" library
  (`Note`, `NoteSummary`, `NoteValidator`, `INoteStore`/`S3NoteStore`)
- `S3NoteStore`: full CRUD against S3, lightweight `ListNotesAsync` via object metadata
  (`HeadObject`, never `GetObject` per item)
- `ExampleLibrary.Tests`: unit tests for `NoteValidator` (always run), integration tests for
  `S3NoteStore` tagged `Category=Integration` (LocalStack-backed, required for this repo's own
  CI)
- Solution-level docs: `README.md`, `CONTRIBUTING.md`, `LOCAL_DEV.md`
- `TODO(template)` marker convention for consumer edit-points (`ExampleLibrary.csproj`,
  `LICENSE`, `dotnet-version` in both workflows, Mono/LocalStack steps)
- GitHub Ruleset on `main` (require PR, 1 approval, required CI status check, block
  force-pushes)

<!--
## [X.Y.Z] - YYYY-MM-DD

### Added
-

### Changed
-

### Fixed
-
-->