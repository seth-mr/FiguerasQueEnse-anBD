using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Interfaces;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly FigurasqeContext _context;

    public StudentRepository(FigurasqeContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> GetAllWithTutorAsync()
    {
        return await _context.Students
            .Include(s => s.IdTutorNavigation)
            .AsNoTracking()
            .Select(s => new StudentDto
            {
                IdStudent = s.IdStudent,
                IdTutor = s.IdTutor,
                Email = s.Email,
                Age = s.Age,
                Genre = s.Genre,
                Country = s.Country,
                Neurodivergency = s.Neurodivergency,
                RegistrationDate = s.RegistrationDate,
                Tutor = s.IdTutorNavigation == null
                    ? null
                    : new TutorDto
                    {
                        IdTutor = s.IdTutorNavigation.IdTutor,
                        Email = s.IdTutorNavigation.Email,
                        Country = s.IdTutorNavigation.Country
                    }
            })
            .ToListAsync();
    }

    public async Task<StudentDto?> GetByIdWithTutorAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.IdTutorNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.IdStudent == id);

        return student == null ? null : MapStudentDto(student);
    }

    public async Task<List<int>> GetStudentIdsByTutorAsync(int tutorId)
    {
        return await _context.Students
            .AsNoTracking()
            .Where(s => s.IdTutor == tutorId)
            .Select(s => s.IdStudent)
            .ToListAsync();
    }

    public async Task<List<int>> GetSessionIdsByStudentAsync(int studentId)
    {
        return await _context.Sessions
            .AsNoTracking()
            .Where(s => s.IdStudent == studentId)
            .Select(s => s.IdSession)
            .ToListAsync();
    }

    internal static StudentDto MapStudentDto(Student student)
    {
        return new StudentDto
        {
            IdStudent = student.IdStudent,
            IdTutor = student.IdTutor,
            Email = student.Email,
            Age = student.Age,
            Genre = student.Genre,
            Country = student.Country,
            Neurodivergency = student.Neurodivergency,
            RegistrationDate = student.RegistrationDate,
            Tutor = student.IdTutorNavigation == null
                ? null
                : new TutorDto
                {
                    IdTutor = student.IdTutorNavigation.IdTutor,
                    Email = student.IdTutorNavigation.Email,
                    Country = student.IdTutorNavigation.Country
                }
        };
    }

    public async Task<bool> AssignTutorByEmailAsync(string studentEmail, string tutorEmail)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == studentEmail);
        if (student == null)
        {
            return false;
        }

        var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.Email == tutorEmail);
        if (tutor == null)
        {
            return false;
        }

        student.IdTutor = tutor.IdTutor;
        await _context.SaveChangesAsync();
        return true;
    }
}
