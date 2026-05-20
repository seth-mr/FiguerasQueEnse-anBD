# AGENTS

## Purpose
Guidance for AI coding agents working in this repository.

## Project Snapshot
- Stack: ASP.NET Core Minimal API + EF Core + PostgreSQL + JWT auth.
- Entry point: [Program.cs](Program.cs).
- Endpoint registration lives in [Program.cs](Program.cs) via `app.Map*Endpoints()` extension methods.

## Run And Validate
- Restore: `dotnet restore`
- Build: `dotnet build`
- Run API: `dotnet run`
- Tests: no test project is currently present in this workspace.

## Architecture Map
- Endpoints: [Endpoints](Endpoints)
- Data models and DbContext: [Models](Models)
- DTOs: [DTOs](DTOs)
- Data access: [Repositories](Repositories), [Interfaces](Interfaces)
- Cross-cutting helpers: [Helpers](Helpers)
- HTTP request samples: [Rest](Rest)

## Auth And Access Rules
- A global fallback policy enforces authentication for all routes (see [Program.cs](Program.cs)).
- Role-based behavior is implemented inside endpoint handlers for student/tutor access checks (see [Endpoints/StudentEndpoints.cs](Endpoints/StudentEndpoints.cs)).
- **Self-access pattern**: tutor endpoints extract JWT user ID via `JwtClaimsHelper.GetUserId()`, check role via `GetUserRole()`, then return `Results.Forbid()` if the caller's ID doesn't match the requested resource ID.
- **Sessions ownership rule**: session access is restricted by ownership. Tutors can access only sessions of their assigned students; students can access only their own sessions.
- **Session end-time source of truth**: `Session.EndDate` is server-managed and updated when level-result events are recorded; clients should not rely on manually setting session end-time.
- **Level-results ownership rule**: level-result access is restricted through the linked session's student. Tutors can access only results for sessions of their assigned students; students can access only results for their own sessions.
- **Claim name variants**: `JwtClaimsHelper` tries `NameIdentifier`, then falls back to `"sub"`/`"userId"`; role tries standard `Role` claim then `"role"`.
- This service validates JWT tokens; login/register endpoints are not exposed here (they are consumed from another service in [Rest/auth.http](Rest/auth.http)).

## Exposed Routes Inventory
Source of truth: all `app.MapGet/MapPost/MapPut/MapDelete` in [Endpoints](Endpoints).

### Students
- `GET /students`
- `GET /students/{id:int}`
- `GET /students/{id:int}/tutor`
- `POST /students`
- `PUT /students/{id:int}`
- `DELETE /students/{id:int}`
- `GET /students/tutor/{tutorId:int}/ids`
- `GET /students/{studentId:int}/sessions/ids`

### Admins
- `GET /admins`
- `GET /admins/{id:int}`
- `POST /admins` — permite bootstrap anonimo solo cuando todavia no existe ningun admin; despues requiere rol `admin`
- `PUT /admins/{id:int}`
- `DELETE /admins/{id:int}`

### Dashboard
- `GET /dashboard/summary` — admin-only; resumen agregado para panel con estadisticas de estudiantes, tutores y actividad

### Tutors
- `GET /tutors` — **returns the calling tutor's own record** (role-gated: tutor only; does NOT return all tutors)
- `GET /tutors/{id:int}`
- `GET /tutors/{id:int}/students`
- `POST /tutors`
- `POST /tutors/assign-student`
- `PUT /tutors/{id:int}`
- `DELETE /tutors/{id:int}`

### Sessions
- `GET /tutors/{tutorId:int}/sessions`
- `GET /students/{studentId:int}/sessions`
- `GET /sessions`
- `GET /sessions/{id:int}`
- `POST /sessions`
- `PUT /sessions/{id:int}`
- `DELETE /sessions/{id:int}`

### Levels
- `GET /levels`
- `GET /levels/{id:int}`
- `POST /levels`
- `PUT /levels/{id:int}`
- `DELETE /levels/{id:int}`

### Level Results
- `GET /level-results`
- `GET /level-results/{id:int}`
- `GET /sessions/{sessionId:int}/level-results/ids`
- `POST /level-results`
- `PUT /level-results/{id:int}`
- `DELETE /level-results/{id:int}`

## Agent Working Conventions
- Keep endpoint logic in endpoint files under [Endpoints](Endpoints); avoid embedding query logic there.
- Put DB/data-shaping logic in repositories under [Repositories](Repositories).
- Validate incoming DTOs with `EndpointResponseHelper.TryValidateDto(...)` to preserve current API behavior.
- When adding new routes, update this file route inventory and add a sample request in [Rest](Rest).
- **DTO conventions**: CRUD triad is `XxxDto` / `CreateXxxDto` / `UpdateXxxDto`; lightweight projections live in [DTOs/ReferenceDtos.cs](DTOs/ReferenceDtos.cs) as `XxxBasicDto`.
- **Repository queries**: use `.AsNoTracking()` for all reads; use `.Include()` for eager loading; project to DTOs inside the repository method (not in endpoint handlers).
- **Exception mapping**: throw domain exceptions from repositories; `GlobalExceptionHandler` maps them to 400/404/409/503/500 automatically.