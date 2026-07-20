# dotnet-nuget-release-template

A GitHub template for packing and publishing multi-target .NET NuGet packages via Trusted
Publishing (OIDC), with an optional container-based integration testing pattern.

## What's included

- `.github/workflows/ci.yml` — build and test on every push/PR (unit tests always run;
  LocalStack-backed integration tests run as part of CI for this template's own example)
- `.github/workflows/release.yml` — tag-triggered pack and publish to NuGet.org via
  Trusted Publishing (`NuGet/login@v1`), no API key secrets required
- `src/ExampleLibrary/` — a small, disposable example library (a "notes" library) demonstrating
  both a pure unit-tested component (`NoteValidator`) and an integration-tested component
  (`S3NoteStore`, backed by LocalStack)
- `tests/ExampleLibrary.Tests/` — unit tests (always run) + integration tests (LocalStack,
  clearly marked/tagged)
- `docker-compose.yml` + `docker/` — LocalStack setup used to run the example's integration
  tests as shipped

## How to use this template

1. Click **"Use this template"** at the top of this repo (not Fork) to generate your own
   repo from this one.
2. Replace `src/ExampleLibrary` (and its test project) with your own library.
3. In your new repo's settings, add a `NUGET_USER` secret containing your NuGet.org profile
   name (not your email) — see [Trusted Publishing setup](#trusted-publishing-setup) below.
4. Rename the project: update the `.csproj` filename, `PackageId`, and root namespace to match
   your library; update `ExampleLibrary.sln` and any project references accordingly; do the
   same for the test project (`ExampleLibrary.Tests` → `YourLibrary.Tests`).
5. Push a tag matching `v*` (e.g. `v1.0.0`) to trigger `release.yml` — it packs and publishes
   automatically. The version in the package comes from the tag itself, not from anything
   hardcoded in the `.csproj`.

## Customizing for your project

This template is not net48-specific — `ci.yml`/`release.yml` run `dotnet restore`/`build`/
`test`/`pack` generically against whatever `<TargetFrameworks>` your `.csproj` declares. Adjust
that property to whatever frameworks you actually target.

**Mono step (`ci.yml`, `release.yml`):** `ExampleLibrary` multi-targets `net48;net10.0` to
demonstrate the pattern, so as shipped, this repo's own CI installs Mono to host the `net48`
test run. If your project doesn't target `net48` (or another pre-.NET-Core framework), remove
the `Setup Mono` step entirely — it's dead weight otherwise.

**LocalStack/Docker block (`ci.yml`, `docker-compose.yml`, `docker/seed.sh`):** required for
this template's own CI, since `S3NoteStoreTests` exercises it directly. Once you replace
`ExampleLibrary` with your own project, this becomes entirely optional — keep it if your
package touches an external dependency worth integration-testing, adapt it to a different
service (any Testcontainers-supported image works the same way), or delete it if it doesn't
apply.

## Trusted Publishing setup

1. On [NuGet.org](https://www.nuget.org), go to your account's **Trusted Publishing**
   settings and add a new trusted publisher, linking it to your GitHub repo, the
   `release.yml` workflow file, and (optionally) an environment name if you use one.
2. In your GitHub repo, add a repository secret named `NUGET_USER` containing your NuGet.org
   profile name (visible in your NuGet.org account settings — this is your username, not an
   API key or email address).
3. No other secrets are needed. `release.yml`'s `publish` job requests `id-token: write`
   permission and exchanges a short-lived OIDC token for a NuGet API key at publish time via
   `NuGet/login@v1` — nothing long-lived is stored in the repo.

## Repo hygiene (recommended)

This template repo itself uses a GitHub Ruleset on `main`: require a PR before merging, at
least 1 approval, a required status check tied to the CI job, and force-pushes blocked (with
the repo owner on the bypass list). Worth setting up something similar on your generated repo
once `ci.yml` has run at least once — the required status check needs an existing check run to
attach to, so add the Ruleset after your first CI run rather than before.

## License

MIT — see [LICENSE](https://github.com/dgates82/dotnet-nuget-release-template/blob/main/LICENSE).