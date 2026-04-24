---
name: run-both-services
description: 'Start and verify both backend services (auth + domain) in this workspace. Use for local integration checks, JWT handoff validation, and smoke tests after API changes.'
argument-hint: 'Optional: provide "build-only" or "run-and-check" (default)'
user-invocable: true
---

# Run Both Services

Use this skill to consistently bootstrap both services and validate that they start correctly.

## When To Use
- After backend refactors affecting startup, auth, routing, or policies.
- Before manual API testing.
- When checking that auth/domain boundaries still work together.

## Procedure

1. From workspace root, restore and build both solutions:
   - `dotnet restore .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.sln`
   - `dotnet restore .\MicroservicioFiguras\MicroservicioFiguras.sln`
   - `dotnet build .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.sln -c Debug`
   - `dotnet build .\MicroservicioFiguras\MicroservicioFiguras.sln -c Debug`

2. If the argument is `build-only`, stop after successful build and report results.

3. Start both services in separate terminals:
   - Auth: `dotnet run --project .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.csproj --launch-profile http`
   - Domain: `dotnet run --project .\MicroservicioFiguras\MicroservicioFiguras.csproj --launch-profile http`

4. Verify basic reachability:
   - Auth URL expected from launch profile: `http://localhost:5041`
   - Domain URL expected from launch profile: `http://localhost:5124`
   - Check each service root or known endpoint with PowerShell `Invoke-WebRequest`.

5. If startup fails, report:
   - Failing project
   - First actionable error message
   - Whether issue appears SDK, DB connection, or config related

## Notes
- Auth targets `net9.0`; domain targets `net10.0`. Ensure both SDKs are installed.
- No test projects are currently present; this workflow is a startup/smoke-check routine.
