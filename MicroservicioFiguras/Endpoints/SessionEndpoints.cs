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
                await EndpointResponseHelper.GetByIdAsync(id, repository.GetByIdWithRelationsAsync));

            app.MapPost("/sessions", async (CreateSessionDto dto, ISessionRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var session = new Session
                {
                    IdStudent = dto.IdStudent,
                    BeginningDate = dto.BeginningDate,
                    EndDate = dto.EndDate,
                    Device = dto.Device
                };

                var created = await repository.AddAsync(session);
                return await EndpointResponseHelper.CreateWithDetailsAsync(created.IdSession, "sessions", repository.GetByIdWithRelationsAsync);
            });

            app.MapPut("/sessions/{id:int}", async (int id, UpdateSessionDto dto, ISessionRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
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
                return await EndpointResponseHelper.UpdateWithDetailsAsync(id, repository.GetByIdWithRelationsAsync);
            });

            app.MapDelete("/sessions/{id:int}", async (int id, ISessionRepository repository) =>
                EndpointResponseHelper.DeleteResult(await repository.DeleteAsync(id)));
        }
    }
}
