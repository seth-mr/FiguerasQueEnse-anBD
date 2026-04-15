using Microsoft.AspNetCore.Builder;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Endpoints
{
    public static class StudentEndpoints
    {
        public static void MapStudentEndpoints(this WebApplication app)
        {
            app.MapGet("/students", async (HttpContext http, IStudentRepository repository) =>
            {
                var userId = http.User.GetUserId();
                var role = http.User.GetUserRole();

                if (role == "student")
                {
                    if (!userId.HasValue)
                    {
                        return Results.Forbid();
                    }

                    var student = await repository.GetByIdWithTutorAsync(userId.Value);
                    return student is not null ? Results.Ok(new[] { student }) : Results.NotFound();
                }

                if (role == "tutor")
                {
                    if (!userId.HasValue)
                    {
                        return Results.Forbid();
                    }

                    var students = await repository.GetStudentsByTutorIdAsync(userId.Value);
                    return Results.Ok(students);
                }

                return Results.Forbid();
            });

            app.MapGet("/students/{id:int}", async (HttpContext http, int id, IStudentRepository repository) =>
            {
                var userId = http.User.GetUserId();
                var role = http.User.GetUserRole();

                if (role == "student")
                {
                    if (userId != id)
                    {
                        return Results.Forbid();
                    }
                }
                else if (role == "tutor")
                {
                    if (!userId.HasValue || !await repository.IsStudentAssignedToTutorAsync(id, userId.Value))
                    {
                        return Results.Forbid();
                    }
                }
                else
                {
                    return Results.Forbid();
                }

                var student = await repository.GetByIdWithTutorAsync(id);
                return student is not null ? Results.Ok(student) : Results.NotFound();
            });

            app.MapPost("/students", async (CreateStudentDto dto, IStudentRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                if (await repository.IsEmailTakenAsync(dto.Email!))
                {
                    return Results.BadRequest(new { errors = new[] { "Email is already registered." } });
                }

                if (dto.IdTutor.HasValue && !await repository.TutorExistsAsync(dto.IdTutor.Value))
                {
                    return Results.BadRequest(new { errors = new[] { "IdTutor does not exist." } });
                }

                var student = new Student
                {
                    IdTutor = dto.IdTutor,
                    Email = dto.Email,
                    PasswordHash = dto.PasswordHash,
                    Age = dto.Age,
                    Genre = dto.Genre,
                    Country = dto.Country,
                    Neurodivergency = dto.Neurodivergency
                };

                var created = await repository.AddAsync(student);
                var createdDto = await repository.GetByIdWithTutorAsync(created.IdStudent);
                return createdDto is not null ? Results.Created($"/students/{createdDto.IdStudent}", createdDto) : Results.BadRequest();
            });

            app.MapPut("/students/{id:int}", async (HttpContext http, int id, UpdateStudentDto dto, IStudentRepository repository) =>
            {
                if (!DtoValidationHelper.TryValidate(dto, out var errors))
                {
                    return Results.BadRequest(new { errors });
                }

                var userId = http.User.GetUserId();
                var role = http.User.GetUserRole();

                if (role == "student")
                {
                    if (userId != id)
                    {
                        return Results.Forbid();
                    }
                }
                else if (role == "tutor")
                {
                    if (!userId.HasValue || !await repository.IsStudentAssignedToTutorAsync(id, userId.Value))
                    {
                        return Results.Forbid();
                    }
                }
                else
                {
                    return Results.Forbid();
                }

                var existingStudent = await repository.GetByIdAsync(id);
                if (existingStudent is null)
                {
                    return Results.NotFound();
                }

                if (await repository.IsEmailTakenByOtherAsync(id, dto.Email!))
                {
                    return Results.BadRequest(new { errors = new[] { "Email is already registered by another student." } });
                }

                if (dto.IdTutor.HasValue && !await repository.TutorExistsAsync(dto.IdTutor.Value))
                {
                    return Results.BadRequest(new { errors = new[] { "IdTutor does not exist." } });
                }

                existingStudent.IdTutor = dto.IdTutor;
                existingStudent.Email = dto.Email;
                existingStudent.PasswordHash = dto.PasswordHash;
                existingStudent.Age = dto.Age;
                existingStudent.Genre = dto.Genre;
                existingStudent.Country = dto.Country;
                existingStudent.Neurodivergency = dto.Neurodivergency;

                await repository.UpdateAsync(existingStudent);
                var updatedDto = await repository.GetByIdWithTutorAsync(id);
                return updatedDto is not null ? Results.Ok(updatedDto) : Results.BadRequest();
            });

            app.MapDelete("/students/{id:int}", async (int id, IStudentRepository repository) =>
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? Results.Ok() : Results.NotFound();
            });

            app.MapGet("/students/tutor/{tutorId:int}/ids", async (int tutorId, IStudentRepository repository) =>
            {
                var studentIds = await repository.GetStudentIdsByTutorAsync(tutorId);
                return Results.Ok(studentIds);
            });

            app.MapGet("/students/{studentId:int}/sessions/ids", async (int studentId, IStudentRepository repository) =>
            {
                var sessionIds = await repository.GetSessionIdsByStudentAsync(studentId);
                return Results.Ok(sessionIds);
            });
        }
    }
}
