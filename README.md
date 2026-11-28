# testproject

The pilot repository for Agent Workbench's Stage 4: the platform pushes its agents' changes here, opens pull requests, and later deploys, releases and reverts them, so none of that touches the platform's own repository.

It holds a tiny ASP.NET Core app and its tests:

- `src/TestProject.Api`: answers `GET /` with what this is and `GET /health` with `{"status":"ok"}`.
- `tests/TestProject.Tests`: xUnit tests through `WebApplicationFactory`.

```powershell
dotnet test --solution TestProject.slnx
```

## CI

`.github/workflows/ci.yml` builds and tests on pushes to `main` and `ai/**` and on pull requests. It reads no secret, uses no environment, runs only on GitHub-hosted runners and gives its token read access to the contents alone, so a branch an agent's change is pushed to can start nothing more. Its actions are pinned by commit.

## Rules

- Changes reach `main` only through a reviewed pull request whose `build-and-test` check passes.
- The Agent Workbench GitHub App may create, update and delete only `ai/*` branches, and no tags.
