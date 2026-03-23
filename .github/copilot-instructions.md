# Copilot Instructions for AviAI

## 1. Change Request Protocol

Follow these steps for every change request in this project, in order:

1. Generate a 6-digit code before touching any code.
1.1. The code must be random.
1.2. The code must be unique to the change request.
1.3. The code must be unique within the current chat session.
1.4. Present the code clearly to the user before doing anything else.

2. Wait for confirmation.
2.1. The user must reply with that exact 6-digit code before implementation begins.
2.2. Do not begin implementing until the code is confirmed.

3. If the code is not confirmed immediately, treat that as a signal that the solution needs more discussion.
3.1. Discard the code.
3.2. Do not reuse the code.
3.3. Re-engage to better understand the problem before proposing a new approach.

4. If the user asks "Are you ready?", respond with a short TLDR.
4.1. The TLDR must be a tight bulleted list of exactly what will be changed.
4.2. Do not include code snippets.
4.3. Do not add prose padding or fluff.
4.4. Generate a new 6-digit code for acceptance of that specific plan.
4.5. The TLDR must include a dedicated Unit Tests section.
4.6. The Unit Tests section must bullet each test being written, name it, and give a one-line plain-English description of what it is asserting and why.
4.7. Always use this exact format for the TLDR response:

> **Changes**
> - <what is being added, changed, or removed — one bullet per file or concern>
>
> **Unit Tests**
> - `TestClassName_MethodName_Condition` — asserts <what it checks and why in plain English>
>
> **`XXXXXX`** ← acceptance code

5. If the "Are you ready?" code is not confirmed immediately, discard it and return to step 2.
5.1. Treat that as a signal that the plan needs more discussion before proceeding.

6. Only after a code is confirmed proceed with implementation.
6.1. When presenting the confirmed plan before coding begins, include the same unit test bullets from step 4 so it is always clear what test coverage is being added.
6.2. Always use this exact format for the implementation response:

> Confirmed. Implementing now.
>
> ---
>
> ### 1 — <first change area>
> <make the change — no commentary padding>
>
> ### 2 — <second change area>
> <make the change>
>
> _(continue numbered sections for each logical change)_
>
> ---
>
> ### N — Build and run all tests
> <run `dotnet test` and show output>
>
> ---
>
> _Summary table at the end:_
>
> | What | Detail |
> |---|---|
> | `FileName.cs` | one-line description of what changed and why |

7. After implementation, update `README.md` to reflect any changes that affect how the project is set up, run, or understood.
7.1. Keep `README.md` current at all times.

8. Stage and commit all changes with git after every change.
8.1. Run `git add` on every file that was created or modified as part of the change.
8.2. Do not leave new files untracked.
8.3. Run `git commit` with a meaningful message that describes what was changed and why.
8.4. Use the conventional commits format: `type(scope): short description`.
8.5. Example: `feat(server): add AppDbContext with Npgsql EF Core provider`.
8.6. Example: `fix(tests): correct assembly reference check in ServerReferenceTests`.
8.7. Do not leave changes staged but uncommitted.

9. Run all unit tests before calling the change complete.
9.1. Execute `dotnet test`.
9.2. Confirm every test passes.
9.3. If any test is failing, whether caused by the change or pre-existing, it must be fixed before the change is considered done.
9.4. A change is not complete until the test suite is fully green.

10. Every code change must be backed by a unit test using Moq.
10.1. This is a hard requirement.
10.2. No change to `MFAIAgent.Server` or `MFAIAgent.Client.Lib` is complete without a corresponding xUnit test in `MFAIAgent.Tests` that uses Moq to isolate dependencies.
10.3. Tests must be written for new code and updated for modified code.
10.4. There are no exceptions.

## 2. General Guidelines

1. Target framework is `net10.0` for all projects.
2. Keep server and client concerns separated between `AviAI.Server` and `AviAI.Client` / `AviAI.Client.Lib`.
3. Prefer `async/await` throughout.
4. Follow standard C# naming conventions.
4.1. Use PascalCase for types and methods.
4.2. Use camelCase for locals.
5. XML doc comments are required on every public type, method, property, and constructor.
5.1. Use `<summary>`, `<param>`, `<returns>`, and `<remarks>` tags as appropriate.
5.2. Comments must be clear, descriptive, and explain purpose instead of merely restating the name.
6. Tests must be kept in sync with server and library code.
6.1. Any time a class or method is added or changed in `AviAI.Server` or `AviAI.Client.Lib`, the corresponding unit tests in `AviAI.Tests` must be added or updated in the same change.
7. All unit tests must use Moq for mocking dependencies.
7.1. This is a hard requirement.
7.2. Every test that involves a dependency must mock it via `Moq`.
7.3. The `Moq` package is installed in `AviAI.Tests`.
8. Never add a third-party NuGet package without explicit permission from the user.
8.1. Always prefer Microsoft .NET native APIs first.
8.2. For all external service communication, including Redmine, GitLab, BookStack, or any future integration, use `System.Net.Http.HttpClient`.
8.3. Do not introduce wrapper or SDK packages unless explicitly approved.
9. Always use the latest available stable NuGet package versions.
9.1. When adding or updating any NuGet package reference, whether first-party or approved third-party, target the latest stable release available at the time of the change.
9.2. Do not pin to older versions unless a specific version is explicitly required by the user.
9.3. When in doubt, run `dotnet package search <package> --take 1` or check NuGet.org to confirm the latest stable version before referencing it.

## 3. Solution Expectations

1. Treat `AviAI.slnx` as the entry point for the solution.
2. If the solution remains empty or contains placeholder files only, confirm the intended application type before generating code.
3. When projects are added, keep the solution organized by responsibility, such as `src`, `tests`, and optional `docs`.
4. Keep buildable code in projects and keep one-off experiments or notes out of the main solution unless explicitly requested.

## 4. Code Quality

1. Favor readable, maintainable code over clever shortcuts.
2. Match the conventions already used in the repository for naming, formatting, and file layout.
3. Add brief comments only where intent is not obvious from the code itself.
4. Avoid dead code, placeholder methods, and speculative abstractions unless the user explicitly asks for scaffolding.

## 5. Dependencies

1. Do not add NuGet packages, npm packages, external services, or SDK requirements without a clear need.
2. When a new dependency is necessary, explain why it is needed and keep the choice conservative.
3. Prefer first-party .NET and platform capabilities before introducing third-party libraries.

## 6. Testing

1. For every code change, add or update a supporting automated test in xUnit in the same change set.
2. If a requested code change is not realistically testable through xUnit, state that clearly and explain why.
3. Prefer focused unit tests over broad integration coverage unless the scenario specifically requires integration testing.
4. If tests cannot be run locally, say so explicitly and explain what still needs validation.

## 7. Documentation

1. Keep setup and usage documentation aligned with the actual solution structure.
2. Document any new commands, required configuration, environment variables, or external dependencies when they are introduced.
3. If architecture or folder conventions become established, update documentation in the same change set.
4. Add XML documentation comments as broadly as practical when writing or modifying code.
5. Prefer clear, plain-language XML documentation over minimal or boilerplate summaries.

## 8. Collaboration

1. Surface assumptions clearly when requirements are ambiguous.
2. If a requested change would lock the solution into a major architectural direction, confirm that direction first.
3. When multiple reasonable implementation paths exist, recommend the simplest one that preserves future flexibility.
