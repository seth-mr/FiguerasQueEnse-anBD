using Microsoft.AspNetCore.Builder;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Endpoints
{
    public static class LevelEndpoints
    {
        public static void MapLevelEndpoints(this WebApplication app)
        {
            app.MapGet("/levels", async (ILevelRepository repository) =>
                Results.Ok(await repository.GetAllWithResultsAsync()));

            app.MapGet("/levels/{id:int}", async (int id, ILevelRepository repository) =>
            {
                var level = await repository.GetByIdWithResultsAsync(id);
                return level is not null ? Results.Ok(level) : Results.NotFound();
            });

            app.MapPost("/levels", async (CreateLevelDto dto, ILevelRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var level = new Level
                {
                    Name = dto.Name,
                    Difficulty = dto.Difficulty
                };

                var created = await repository.AddAsync(level);
                var createdDto = await repository.GetByIdWithResultsAsync(created.IdLevel);
                return createdDto is not null ? Results.Created($"/levels/{createdDto.IdLevel}", createdDto) : Results.BadRequest();
            });

            app.MapPut("/levels/{id:int}", async (int id, UpdateLevelDto dto, ILevelRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var existingLevel = await repository.GetByIdAsync(id);
                if (existingLevel is null)
                {
                    return Results.NotFound();
                }

                existingLevel.Name = dto.Name;
                existingLevel.Difficulty = dto.Difficulty;

                await repository.UpdateAsync(existingLevel);
                var updatedLevel = await repository.GetByIdWithResultsAsync(id);
                return updatedLevel is not null ? Results.Ok(updatedLevel) : Results.BadRequest();
            });

            app.MapDelete("/levels/{id:int}", async (int id, ILevelRepository repository) =>
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
