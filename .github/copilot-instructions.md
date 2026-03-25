## Change Request Protocol

Follow these steps for every change request in this project, in order:

1. **Generate a 6-digit code** before touching any code. The code must be derived using a Unix timestamp hash to ensure genuine randomness: take the current Unix epoch time in seconds, multiply by a prime (e.g. 2654435761), take modulo 900000, then add 100000 to guarantee a 6-digit result. The code must be unique to the change request and unique within the current chat session. Present it clearly to the user before doing anything else.
2. **Wait for confirmation.** The user must reply with that exact 6-digit code to proceed. Do not begin implementing until they do.
3. **If the code is not confirmed immediately**, treat it as a signal that the solution needs more discussion. Discard the code, do not reuse it, and re-engage to better understand the problem before proposing a new approach.
4. **If the user asks "Are you ready?"**, respond with a short TLDR — a tight bulleted list of exactly what will be changed. No code snippets, no prose padding, no fluff. Then generate a **new** 6-digit code for acceptance of that specific plan.
   - The TLDR must include a dedicated **Unit Tests** section that bullets each test being written, names it, and gives a one-line plain-English description of what it is asserting and why.
   - **Always use this exact format for the TLDR response:**

     > **Changes**
     > - \<what is being added, changed, or removed — one bullet per file or concern\>
     >
     > **Unit Tests**
     > - `TestClassName_MethodName_Condition` — asserts \<what it checks and why in plain English\>
     >
     > **`XXXXXX`** ← acceptance code
5. **If the "Are you ready?" code is not confirmed immediately**, discard it and return to step 2 — treat it as a signal that the plan needs more discussion before proceeding.
6. **Only after a code is confirmed** proceed with implementation. When presenting the confirmed plan before coding begins, include the same unit test bullets from step 4 so it is always clear what test coverage is being added.
   - **Always use this exact format for the implementation response:**

     > Confirmed. Implementing now.
     >
     > ---
     >
     > ### 1 — \<first change area\>
     > \<make the change — no commentary padding\>
     >
     > ### 2 — \<second change area\>
     > \<make the change\>
     >
     > _(continue numbered sections for each logical change)_
     >
     > ---
     >
     > ### N — Build and run all tests
     > \<run `dotnet test` and show output\>
     >
     > ---
     >
     > _Summary table at the end:_
     >
     > | What | Detail |
     > |---|---|
     > | `FileName.cs` | one-line description of what changed and why |
7. **After implementation, update `README.md`** to reflect any changes that affect how the project is set up, run, or understood. Keep it current at all times.
8. **Stage and commit all changes with git.** After every change:
   - Run `git add` on every file that was created or modified as part of the change. Do not leave new files untracked.
   - Run `git commit` with a meaningful message that describes what was changed and why. Use the conventional commits format: `type(scope): short description` — e.g. `feat(server): add AppDbContext with Npgsql EF Core provider` or `fix(tests): correct assembly reference check in ServerReferenceTests`.
   - Do not leave changes staged but uncommitted.
9. **Run all unit tests before calling the change complete.** Execute `dotnet test` and confirm every test passes. If any test is failing — whether caused by the change or pre-existing — it must be fixed before the change is considered done. A change is not complete until the test suite is fully green.
10. **Every code change must be backed by a unit test using Moq.** This is a hard requirement. No change is complete without a corresponding xUnit test in the designated test project that uses Moq to isolate dependencies. Tests must be written for new code and updated for modified code. There are no exceptions.

---