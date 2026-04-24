# AGENTS.md

Scope: applies to the entire workspace.

## Project Layout

- `FigurasQE-AuthenticationService/`: authentication API (controller-based), JWT issuance, user registration/login.
- `MicroservicioFiguras/`: domain API (minimal endpoints), repositories, DTO validation, JWT authorization.
- `FigurasQE-Frontend/`: Razor Pages web client (`net10.0`) — login, signup, and student views. Calls the gateway, not backend services directly.
- `FigurasQE-Gateway/`: Express 5 API gateway (Node.js). Routes `/auth/*` to the auth service and `/data/*` to the domain service. Config via `.env` (`AUTH_SERVICE`, `DATA_SERVICE` URLs).
- Both backend services connect to PostgreSQL and are developed/run independently.

## Build And Run (PowerShell, from workspace root)

- Restore:
  - `dotnet restore .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.sln`
  - `dotnet restore .\MicroservicioFiguras\MicroservicioFiguras.sln`
- Build:
  - `dotnet build .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.sln -c Debug`
  - `dotnet build .\MicroservicioFiguras\MicroservicioFiguras.sln -c Debug`
- Run auth service:
  - `dotnet run --project .\FigurasQE-AuthenticationService\FigurasQE-AuthenticationService.csproj --launch-profile http`
- Run domain service:
  - `dotnet run --project .\MicroservicioFiguras\MicroservicioFiguras.csproj --launch-profile http`
- Run frontend:
  - `dotnet run --project .\FigurasQE-Frontend\FigurasQE-WebClient.csproj`
- Run gateway (from `FigurasQE-Gateway/`):
  - `node src/server.js` (requires a `.env` file with `AUTH_SERVICE` and `DATA_SERVICE` set)

## Framework And Runtime Notes

- Auth service targets `net9.0`.
- Microservicio targets `net10.0`.
- Frontend targets `net10.0`.
- Gateway runs on Node.js with Express 5 (`express ^5.2.1`) and `axios` for upstream proxying.
- Ensure both .NET SDKs are installed before building both projects.
- Gateway requires a `.env` file in `FigurasQE-Gateway/` — no `.env` is committed. See `.env` config pattern below.

## Architecture Boundaries

- Keep auth concerns in `FigurasQE-AuthenticationService`:
  - Controller entrypoint: `FigurasQE-AuthenticationService/Controllers/AuthController.cs`
  - JWT generation: `FigurasQE-AuthenticationService/Services/JwtService.cs`
- Keep domain CRUD/business endpoints in `MicroservicioFiguras`:
  - Endpoint mapping: `MicroservicioFiguras/Program.cs`
  - Endpoint modules: `MicroservicioFiguras/Endpoints/*.cs`
- Frontend calls only the gateway (`http://localhost:3000`), never backend services directly.
- Gateway routes: `/auth/*` → auth service, `/data/*` → domain service (URLs from env vars).

## Coding Conventions In This Repo

- Preserve existing API styles by project:
  - Auth uses MVC controllers.
  - Microservicio uses minimal API endpoint extension methods.
- For Microservicio changes:
  - Prefer DTOs at endpoint boundaries (`MicroservicioFiguras/DTOs/`).
  - Use repository interfaces and implementations (`MicroservicioFiguras/Interfaces/`, `MicroservicioFiguras/Repositories/`).
  - Reuse helpers for validation/response/error handling (`MicroservicioFiguras/Helpers/`).
- Keep projections to DTOs in repositories where pattern already exists.

## Security And Config Guardrails

- Do not commit new hardcoded secrets (JWT keys, DB credentials).
- Prefer reading connection strings and JWT settings from configuration, not hardcoded context setup.
- JWT role claim compatibility matters across services:
  - Policies in `MicroservicioFiguras/Program.cs` depend on role claims emitted by auth service.

## Testing

- There are currently no test projects (`*Test*.csproj` not present).
- If adding tests, place them in dedicated test projects and wire `dotnet test` from workspace root.

## High-Value Files For Quick Context

- `FigurasQE-AuthenticationService/Program.cs`
- `FigurasQE-AuthenticationService/Services/AuthService.cs`
- `FigurasQE-AuthenticationService/Services/JwtService.cs`
- `MicroservicioFiguras/Program.cs`
- `MicroservicioFiguras/Endpoints/StudentEndpoints.cs`
- `MicroservicioFiguras/Repositories/Repository.cs`
- `MicroservicioFiguras/Helpers/GlobalExceptionHandler.cs`
- `FigurasQE-Frontend/Program.cs`
- `FigurasQE-Frontend/Pages/User/Login.cshtml` / `Login.chstml.cs` (note: `.cs` filename has typo — `.chstml` not `.cshtml`; pages live under `Pages/User/`)
- `FigurasQE-Gateway/src/server.js`
- `FigurasQE-Gateway/src/routes/` (`auth.js` — login+register proxy; `students.js` — student list proxy)

## Known Gaps / WIP Areas

- Gateway `students.js` route does not forward the `Authorization` header — JWTs won't reach the domain service.
- Frontend has no JWT storage or session middleware wired up; token is read and role-checked in `Login.chstml.cs` but never persisted to a cookie or session.
- Gateway URL is hardcoded in `FigurasQE-Frontend/Pages/User/Login.chstml.cs` and `Signup.cshtml.cs`; should move to `appsettings.json`.
- `Login.chstml.cs` calls `ReadAsStringAsync()` twice on the same response stream — second call returns empty; also missing `ModelState.IsValid` check.
- `Signup.cshtml.cs` `OnPostAsync` does not check `response.IsSuccessStatusCode` before redirecting.
- No `AddHttpClient` registration in `FigurasQE-Frontend/Program.cs` — pages use `new HttpClient()` directly.
- No `start` script in `FigurasQE-Gateway/package.json`.
