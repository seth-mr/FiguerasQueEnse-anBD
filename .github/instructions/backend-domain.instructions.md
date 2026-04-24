---
description: "Use when editing minimal API endpoints, DTO mapping, repositories, or authorization policies in MicroservicioFiguras."
name: "Backend Domain Service Instructions"
applyTo: "MicroservicioFiguras/**"
---
# Domain Service Instructions

## Scope
- Applies only to `MicroservicioFiguras/`.
- Keep domain CRUD and business endpoints in this service.

## API And Architecture Conventions
- Preserve minimal API style and endpoint extension modules in `Endpoints/*.cs`.
- Keep endpoint registration in `Program.cs` and feature logic in endpoint modules.
- Keep repository abstractions in `Interfaces/` and implementations in `Repositories/`.

## DTO And Repository Rules
- Prefer DTOs at endpoint boundaries.
- Reuse existing validation and response helpers from `Helpers/`.
- Keep entity-to-DTO projections in repositories where that pattern already exists.

## Auth And Policy Compatibility
- Respect JWT policy expectations configured in `Program.cs`.
- Any claim-related changes must remain compatible with tokens from the auth service.

## Error Handling And Responses
- Keep centralized exception handling through existing global handler.
- Preserve established response patterns in endpoint helpers.

## Data And Config
- Prefer DI/configured database connection setup over hardcoded values.
- Avoid introducing new secrets in source files.

## Quick References
- `MicroservicioFiguras/Program.cs`
- `MicroservicioFiguras/Endpoints/*.cs`
- `MicroservicioFiguras/Repositories/Repository.cs`
- `MicroservicioFiguras/Helpers/EndpointResponseHelper.cs`
- `MicroservicioFiguras/Helpers/GlobalExceptionHandler.cs`
- `MicroservicioFiguras/Helpers/JwtClaimsHelper.cs`
