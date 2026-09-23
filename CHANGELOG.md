# Changelog

All notable changes to this template will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Template releases are tagged `template-vX.Y.Z` — see [CONTRIBUTING.md](CONTRIBUTING.md#versioning-this-template)
for why this differs from the `vX.Y.Z` tags `release.yml` watches for.

## [Unreleased]

## [1.2.0] - 2026-09-22

### Added
- CodeQL static analysis (`.github/workflows/codeql.yml`), analyzing the `csharp`
  language via GitHub's Advanced Setup — run independently of SonarQube Cloud. Path
  exclusions cover `bin`/`obj`. Runs on push to `main`/`release/**`, on PRs, weekly on a
  schedule, and via `workflow_dispatch`. No token or setup required, unlike SonarQube
  Cloud's `SONAR_TOKEN` — repos generated from this template get it for free.

### Changed
- README: added a "Proven in production" callout naming the two real NuGet packages
  scaffolded from this template, a CodeQL badge and customization paragraph, and a
  package-ecosystem table.

## [1.1.0] - 2026-09-15

### Added
- `SonarAnalyzer.CSharp` as a build-time Roslyn analyzer on `ExampleLibrary` (`PrivateAssets=all`,
  never flows to consumers).
- SonarQube Cloud static analysis, wired into `ci.yml`'s `build-and-test` job via
  `dotnet-sonarscanner` and gated on the quality gate result, with coverage
  (`dotnet test --collect:"XPlat Code Coverage"` across both unit and integration test runs)
  fed into the scan via `sonar.cs.cobertura.reportsPaths`. Explicit `sonar.branch.name` for
  non-PR triggers, `SONAR_PROJECT_KEY`/`SONAR_ORG` repo variables instead of hardcoded literals,
  and every Sonar-related step gated on `SONAR_TOKEN` being set so forks and repos generated
  from this template build cleanly before SonarQube Cloud is configured.

### Fixed
- `NuGet/login@v1` pinned to a commit SHA (SonarQube Cloud finding).
- `S3NoteStore.GetNoteAsync` now passes its `CancellationToken` through to the underlying
  `StreamReader.ReadToEndAsync` call on `net10.0` (SonarQube Cloud finding, reliability/S8949);
  gated behind `NET7_0_OR_GREATER` since that overload isn't available on `net48`.

## [1.0.0] - 2026-07-21

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
- Solution-level docs: `README.md`, `CONTRIBUTING.md`, `docs/LOCAL_DEV.md`, including this
  repo's own `template-vX.Y.Z` versioning convention (distinct from the `vX.Y.Z` tags
  `release.yml` watches for)
- `TODO(template)` marker convention for consumer edit-points (`ExampleLibrary.csproj`,
  `LICENSE`, `DOTNET_VERSION` in both workflows, Mono/LocalStack steps)
- Workflow-level `DOTNET_VERSION` env var in `ci.yml`/`release.yml`, a single edit-point per
  file instead of per-job inline values
- Verified end-to-end: generated a throwaway repo via "Use this template", followed the README
  as a first-time consumer, published `DGates.NuGetTemplateVerification` to NuGet.org via
  Trusted Publishing, confirmed success, unlisted the package, and deleted the throwaway repo

<!--
## [X.Y.Z] - YYYY-MM-DD

### Added
-

### Changed
-

### Fixed
-
-->