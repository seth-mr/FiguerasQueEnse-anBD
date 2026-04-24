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
                await EndpointResponseHelper.GetByIdAsync(id, repository.GetByIdWithStudentsAsync));

            app.MapGet("/tutors/{id:int}/students", async (int id, IStudentRepository studentRepository, ITutorRepository tutorRepository) =>
            {
                var tutor = await tutorRepository.GetByIdWithStudentsAsync(id);
                if (tutor is null) return Results.NotFound();
                var students = await studentRepository.GetStudentsByTutorIdAsync(id);
                return Results.Ok(students);
            });

            app.MapPost("/tutors", async (CreateTutorDto dto, ITutorRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var tutor = new Tutor
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = dto.PasswordHash,
                    Country = dto.Country,
                    Gender = dto.Gender,
                    Age = dto.Age,
                    Grade = dto.Grade
                };

                var created = await repository.AddAsync(tutor);
                return await EndpointResponseHelper.CreateWithDetailsAsync(created.IdTutor, "tutors", repository.GetByIdWithStudentsAsync);
            });

            app.MapPost("/tutors/assign-student", async (AssignTutorDto dto, IStudentRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var assigned = await repository.AssignTutorByEmailAsync(dto.StudentEmail, dto.TutorEmail);
                return assigned ? Results.Ok() : Results.NotFound();
            });

            app.MapPut("/tutors/{id:int}", async (int id, UpdateTutorDto dto, ITutorRepository repository) =>
            {
                if (!EndpointResponseHelper.TryValidateDto(dto, out var validationError))
                {
                    return validationError;
                }

                var existingTutor = await repository.GetByIdAsync(id);
                if (existingTutor is null)
                {
                    return Results.NotFound();
                }

                existingTutor.Email = dto.Email;
                existingTutor.Name = dto.Name;
                existingTutor.Country = dto.Country;
                existingTutor.Gender = dto.Gender;
                existingTutor.Age = dto.Age;
                existingTutor.Grade = dto.Grade;

                await repository.UpdateAsync(existingTutor);
                return await EndpointResponseHelper.UpdateWithDetailsAsync(id, repository.GetByIdWithStudentsAsync);
            });

            app.MapDelete("/tutors/{id:int}", async (int id, ITutorRepository repository) =>
                EndpointResponseHelper.DeleteResult(await repository.DeleteAsync(id)));
        }
    }
}
