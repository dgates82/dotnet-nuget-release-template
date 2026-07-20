# dotnet-nuget-release-template

<!-- TODO: one-line description, matches repo "About" field -->
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

<!-- TODO: confirm this list matches final repo contents once scaffolding is done -->

## How to use this template

1. Click **"Use this template"** at the top of this repo (not Fork) to generate your own
   repo from this one.
2. Replace `src/ExampleLibrary` (and its test project) with your own library.
3. <!-- TODO: NUGET_USER secret setup instructions -->
4. <!-- TODO: renaming steps — .csproj, namespace, solution file references -->
5. <!-- TODO: how to trigger a release (tag format, e.g. vX.Y.Z) -->

## Customizing for your project

<!-- TODO: TargetFrameworks guidance — this template is not net48-specific; explain how to
     adjust <TargetFrameworks> in the .csproj -->

<!-- TODO: Mono step — where it lives in ci.yml, when to uncomment it (targeting net48 or
     earlier) -->

<!-- TODO: LocalStack/Testcontainers block — explain this is REQUIRED for this template's own
     CI (S3NoteStore integration test) but OPTIONAL once you replace ExampleLibrary with your
     own project. Keep it, adapt it to a different service, or delete it entirely. -->

## Trusted Publishing setup

<!-- TODO: step-by-step for configuring Trusted Publishing on NuGet.org (linking the GitHub
     repo/workflow) and adding the NUGET_USER repo secret -->

## Repo hygiene (recommended)

<!-- TODO: brief note on GitHub Rulesets used in this template repo itself — require PR,
     1 approval, required status check, block force-pushes — and a suggestion that consumers
     set up something similar on their generated repo -->

## License

<!-- TODO: confirm MIT LICENSE file is present; reference it here -->
MIT — see [LICENSE](https://github.com/dgates82/dotnet-nuget-release-template/blob/main/LICENSE).
