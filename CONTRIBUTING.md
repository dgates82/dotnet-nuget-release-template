# Contributing

Thanks for your interest in improving this template. A few guidelines to keep things
consistent.

## Before you start

- For anything beyond a small fix, open an issue first to discuss the change — especially
  changes to `release.yml` or `ci.yml`, since those are the actual artifact this repo exists
  to provide.
- This is a template repo, not a growing product. Keep additions minimal and generic — if a
  change only makes sense for a specific tech stack or use case, it probably belongs in your
  own generated repo, not here.

## Development setup

<!-- TODO: fill in once solution/csproj files exist -->
- `dotnet restore`
- `docker compose up -d --wait` — starts LocalStack, seeds the example bucket/note
  automatically
- `dotnet test` — runs everything; use `--filter "Category!=Integration"` for unit tests only

## Making changes

- Branch from `main`, open a PR — direct pushes to `main` are blocked.
- Commit format: `type: lowercase description` (e.g. `feat:`, `fix:`, `docs:`, `style:`)
- Keep `ExampleLibrary` / `ExampleLibrary.Tests` naming as-is — it's deliberately generic and
  disposable so consumers understand it's meant to be replaced.
- If you touch `S3NoteStore` or `ListNotesAsync`, keep the "lightweight summary via
  `HeadObject`, never `GetObject` per item" pattern — this is a deliberate design choice, not
  an oversight.
- LocalStack/integration testing is required for this repo's own CI to pass. If your change
  affects `docker-compose.yml` or `docker/seed.sh`, confirm `ci.yml` still passes end to end,
  not just that it builds.
- Update `CHANGELOG.md` under `[Unreleased]` for any user-facing change (workflow behavior,
  template structure, README instructions). Use `### Added` for new capability, `### Changed`
  for altering existing behavior.

## Pull requests

- 1 approval required before merge (GitHub Ruleset on `main`).
- CI must pass — build, unit tests, and LocalStack-backed integration tests.
- Squash merge is the default for `main`.

## What not to contribute

- API keys, secrets, or anything that would require moving off Trusted Publishing (OIDC) —
  this template is intentionally secret-free.
- net48-specific requirements baked in as defaults — Mono support stays commented-out/opt-in.
- A hard dependency on LocalStack/S3 specifically for downstream consumers — the template's
  own example uses it, but the pattern must stay swappable, not mandatory, in how it's
  documented.

## Questions

Open an issue, or start a discussion if you're not sure whether something fits the template's
scope.
