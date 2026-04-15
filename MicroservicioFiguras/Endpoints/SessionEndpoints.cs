using Microsoft.AspNetCore.Builder;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Endpoints
{
    public static class SessionEndpoints
    {
        public static void MapSessionEndpoints(this WebApplication app)
        {
            app.MapGet("/sessions", async (ISessionRepository repository) =>
                Results.Ok(await repository.GetAllWithRelationsAsync()));

            app.MapGet("/sessions/{id:int}", async (int id, ISessionRepository repository) =>
            {
                var session = await repository.GetByIdWithRelationsAsync(id);
                return session is not null ? Results.Ok(session) : Results.NotFound();
            });

            app.MapPost("/sessions", async (CreateSessionDto dto, ISessionRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var session = new Session
                {
                    IdStudent = dto.IdStudent,
                    BeginningDate = dto.BeginningDate,
                    EndDate = dto.EndDate,
                    Device = dto.Device
                };

                var created = await repository.AddAsync(session);
                var createdDto = await repository.GetByIdWithRelationsAsync(created.IdSession);
                return createdDto is not null ? Results.Created($"/sessions/{createdDto.IdSession}", createdDto) : Results.BadRequest();
            });

            app.MapPut("/sessions/{id:int}", async (int id, UpdateSessionDto dto, ISessionRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var existingSession = await repository.GetByIdAsync(id);
                if (existingSession is null)
                {
                    return Results.NotFound();
                }

                existingSession.IdStudent = dto.IdStudent;
                existingSession.BeginningDate = dto.BeginningDate;
                existingSession.EndDate = dto.EndDate;
                existingSession.Device = dto.Device;

                await repository.UpdateAsync(existingSession);
                var updatedSession = await repository.GetByIdWithRelationsAsync(id);
                return updatedSession is not null ? Results.Ok(updatedSession) : Results.BadRequest();
            });

            app.MapDelete("/sessions/{id:int}", async (int id, ISessionRepository repository) =>
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
