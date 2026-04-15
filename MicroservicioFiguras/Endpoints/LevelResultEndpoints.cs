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
            {
                var result = await repository.GetByIdWithRelationsAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            app.MapGet("/sessions/{sessionId:int}/level-results/ids", async (int sessionId, ILevelResultRepository repository) =>
            {
                var ids = await repository.GetIdsBySessionAsync(sessionId);
                return Results.Ok(ids);
            });

            app.MapPost("/level-results", async (CreateLevelResultDto dto, ILevelResultRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
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
                var createdDto = await repository.GetByIdWithRelationsAsync(created.IdResult);
                return createdDto is not null ? Results.Created($"/level-results/{createdDto.IdResult}", createdDto) : Results.BadRequest();
            });

            app.MapPut("/level-results/{id:int}", async (int id, UpdateLevelResultDto dto, ILevelResultRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
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
                var updatedResult = await repository.GetByIdWithRelationsAsync(id);
                return updatedResult is not null ? Results.Ok(updatedResult) : Results.BadRequest();
            });

            app.MapDelete("/level-results/{id:int}", async (int id, ILevelResultRepository repository) =>
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
