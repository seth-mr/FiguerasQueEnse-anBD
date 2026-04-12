using Microsoft.EntityFrameworkCore;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Helpers;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;
using MicroservicioFiguras.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FigurasqeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ILevelResultRepository, LevelResultRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/students", async (IStudentRepository repository) =>
    Results.Ok(await repository.GetAllWithTutorAsync()));

app.MapGet("/students/{id:int}", async (int id, IStudentRepository repository) =>
{
    var student = await repository.GetByIdWithTutorAsync(id);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.MapPost("/students", async (CreateStudentDto dto, IStudentRepository repository) =>
{
    if (!DtoValidationHelper.TryValidate(dto, out var errors))
    {
        return Results.BadRequest(errors);
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

app.MapPost("/students/assign-tutor", async (AssignTutorDto dto, IStudentRepository repository) =>
{
    if (!DtoValidationHelper.TryValidate(dto, out var errors))
    {
        return Results.BadRequest(errors);
    }

    var assigned = await repository.AssignTutorByEmailAsync(dto.StudentEmail, dto.TutorEmail);
    return assigned ? Results.Ok() : Results.NotFound();
});

app.MapPut("/students/{id:int}", async (int id, UpdateStudentDto dto, IStudentRepository repository) =>
{
    if (!DtoValidationHelper.TryValidate(dto, out var errors))
    {
        return Results.BadRequest(errors);
    }

    var existingStudent = await repository.GetByIdAsync(id);
    if (existingStudent is null)
    {
        return Results.NotFound();
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
        return Results.BadRequest(errors);
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

app.MapPut("/tutors/{id:int}", async (int id, UpdateTutorDto dto, ITutorRepository repository) =>
{
    if (!DtoValidationHelper.TryValidate(dto, out var errors))
    {
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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
        return Results.BadRequest(errors);
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

app.Run();
