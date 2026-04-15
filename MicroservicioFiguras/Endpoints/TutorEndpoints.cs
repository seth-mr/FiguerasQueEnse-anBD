using Microsoft.AspNetCore.Builder;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Endpoints
{
    public static class TutorEndpoints
    {
        public static void MapTutorEndpoints(this WebApplication app)
        {
            app.MapGet("/tutors", async (ITutorRepository repository) =>
                Results.Ok(await repository.GetAllWithStudentsAsync()));

            app.MapGet("/tutors/{id:int}", async (int id, ITutorRepository repository) =>
            {
                var tutor = await repository.GetByIdWithStudentsAsync(id);
                return tutor is not null ? Results.Ok(tutor) : Results.NotFound();
            });

            app.MapPost("/tutors", async (CreateTutorDto dto, ITutorRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var tutor = new Tutor
                {
                    Email = dto.Email,
                    PasswordHash = dto.PasswordHash,
                    Country = dto.Country
                };

                var created = await repository.AddAsync(tutor);
                var createdDto = await repository.GetByIdWithStudentsAsync(created.IdTutor);
                return createdDto is not null ? Results.Created($"/tutors/{createdDto.IdTutor}", createdDto) : Results.BadRequest();
            });

            app.MapPost("/tutors/assign-student", async (AssignTutorDto dto, IStudentRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var assigned = await repository.AssignTutorByEmailAsync(dto.StudentEmail, dto.TutorEmail);
                return assigned ? Results.Ok() : Results.NotFound();
            });

            app.MapPut("/tutors/{id:int}", async (int id, UpdateTutorDto dto, ITutorRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var existingTutor = await repository.GetByIdAsync(id);
                if (existingTutor is null)
                {
                    return Results.NotFound();
                }

                existingTutor.Email = dto.Email;
                existingTutor.Country = dto.Country;

                await repository.UpdateAsync(existingTutor);
                var updatedTutor = await repository.GetByIdWithStudentsAsync(id);
                return updatedTutor is not null ? Results.Ok(updatedTutor) : Results.BadRequest();
            });

            app.MapDelete("/tutors/{id:int}", async (int id, ITutorRepository repository) =>
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
