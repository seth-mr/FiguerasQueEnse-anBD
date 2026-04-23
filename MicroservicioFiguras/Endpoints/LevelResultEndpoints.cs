using Microsoft.AspNetCore.Builder;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Endpoints
{
    public static class LevelResultEndpoints
    {
        public static void MapLevelResultEndpoints(this WebApplication app)
        {
            app.MapGet("/level-results", async (ILevelResultRepository repository) =>
                Results.Ok(await repository.GetAllWithRelationsAsync()));

            app.MapGet("/level-results/{id:int}", async (int id, ILevelResultRepository repository) =>
                await EndpointResponseHelper.GetByIdAsync(id, repository.GetByIdWithRelationsAsync));

            app.MapGet("/sessions/{sessionId:int}/level-results/ids", async (int sessionId, ILevelResultRepository repository) =>
            {
                var ids = await repository.GetIdsBySessionAsync(sessionId);
                return Results.Ok(ids);
            });

            app.MapPost("/level-results", async (CreateLevelResultDto dto, ILevelResultRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var levelResult = new LevelResult
                {
                    IdSession = dto.IdSession,
                    IdLevel = dto.IdLevel,
                    FinishingTime = dto.FinishingTime,
                    Attempts = dto.Attempts,
                    Fails = dto.Fails,
                    Completed = dto.Completed
                };

                var created = await repository.AddAsync(levelResult);
                return await EndpointResponseHelper.CreateWithDetailsAsync(created.IdResult, "level-results", repository.GetByIdWithRelationsAsync);
            });

            app.MapPut("/level-results/{id:int}", async (int id, UpdateLevelResultDto dto, ILevelResultRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var existingResult = await repository.GetByIdAsync(id);
                if (existingResult is null)
                {
                    return Results.NotFound();
                }

                existingResult.IdSession = dto.IdSession;
                existingResult.IdLevel = dto.IdLevel;
                existingResult.FinishingTime = dto.FinishingTime;
                existingResult.Attempts = dto.Attempts;
                existingResult.Fails = dto.Fails;
                existingResult.Completed = dto.Completed;

                await repository.UpdateAsync(existingResult);
                return await EndpointResponseHelper.UpdateWithDetailsAsync(id, repository.GetByIdWithRelationsAsync);
            });

            app.MapDelete("/level-results/{id:int}", async (int id, ILevelResultRepository repository) =>
                EndpointResponseHelper.DeleteResult(await repository.DeleteAsync(id)));
        }
    }
}
