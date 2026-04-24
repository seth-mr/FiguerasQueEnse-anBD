---
description: "Use when editing Razor Pages, page models, login/signup flows, HTTP client calls, or session/auth middleware in FigurasQE-Frontend."
name: "Frontend Web Client Instructions"
applyTo: "FigurasQE-Frontend/**"
---
# Frontend Instructions

## Scope
- Applies only to `FigurasQE-Frontend/` (ASP.NET Core Razor Pages, `net10.0`).
- Frontend communicates exclusively with the gateway at `http://localhost:3000` — never call backend services directly.

## Architecture Conventions
- Pages live in `Pages/User/`; page model code-behind files are `*.cshtml.cs`.
- Note: `Login.chstml.cs` has a filename typo (`.chstml` not `.cshtml`) — do not rename without testing, the view is correctly named `Login.cshtml`.
- Use `HttpClient` via DI (`AddHttpClient` in `Program.cs`); avoid constructing raw `new HttpClient()` instances in page models.

## Auth And Session
- No authentication middleware is wired in `Program.cs` yet — token storage is a known WIP gap.
- When implementing JWT storage, wire `AddAuthentication`/`UseAuthentication` in `Program.cs` and use cookie-based auth or session.
- Do not hardcode gateway URLs in page model code — read from `appsettings.json` (key pattern: `GatewayBaseUrl` or similar under `Services:`).

## Config Pattern
- Gateway base URL currently hardcoded in `Login.chstml.cs` as `http://localhost:3000` — this should move to `appsettings.json`.
- Add service URLs under a named section in `appsettings.json` and inject via `IConfiguration` or typed options.

## Known Bugs / WIP
- `Login.chstml.cs`: `ReadAsStringAsync()` is called twice on the same stream — second call returns empty string. Use the first result.
- `Login.chstml.cs`: missing `ModelState.IsValid` check before posting.
- `Signup.cshtml.cs`: `OnPostAsync` does not check `response.IsSuccessStatusCode` before redirecting to login.
- JWT from login response is read and role is extracted, but no session/cookie is set — auth state is lost after redirect.
- No session or claims principal is set after login.

## Quick References
- `FigurasQE-Frontend/Program.cs`
- `FigurasQE-Frontend/Pages/User/Login.cshtml` / `Login.chstml.cs`
- `FigurasQE-Frontend/Pages/User/Signup.cshtml` / `Signup.cshtml.cs`
- `FigurasQE-Frontend/appsettings.json`
