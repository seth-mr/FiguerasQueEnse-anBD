---
description: "Use when editing authentication, JWT generation, login/register flows, or auth controllers in FigurasQE-AuthenticationService."
name: "Backend Auth Service Instructions"
applyTo: "FigurasQE-AuthenticationService/**"
---
# Auth Service Instructions

## Scope
- Applies only to the authentication service in `FigurasQE-AuthenticationService/`.
- Keep auth concerns in this service: registration, login, JWT issuance, and auth-specific persistence.

## API And Architecture Conventions
- Preserve controller-based style (`AddControllers`, `MapControllers`) used by this service.
- Keep entrypoints in `Controllers/AuthController.cs` and business logic in `Services/`.
- Avoid moving domain CRUD concerns into this project.

## Security And JWT Rules
- Maintain compatibility with role claims expected by policies in the domain service.
- Avoid introducing hardcoded secrets; prefer configuration values.
- Keep password handling centralized in auth flows; avoid plaintext or reversible storage.

## Data And Config
- Use configured PostgreSQL connection string from app settings and DI.
- Do not duplicate runtime-only config across code paths unless required.

## Validation And Error Handling
- Keep request validation near boundary models (`Models/`).
- Prefer consistent API errors; if adding broad exception handling, keep it centralized and minimal.

## Quick References
- `FigurasQE-AuthenticationService/Program.cs`
- `FigurasQE-AuthenticationService/Controllers/AuthController.cs`
- `FigurasQE-AuthenticationService/Services/AuthService.cs`
- `FigurasQE-AuthenticationService/Services/JwtService.cs`
- `FigurasQE-AuthenticationService/Data/AppDbContext.cs`
