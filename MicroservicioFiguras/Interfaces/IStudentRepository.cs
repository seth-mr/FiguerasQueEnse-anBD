using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<List<StudentDto>> GetAllWithTutorAsync();
    Task<StudentDto?> GetByIdWithTutorAsync(int id);
    Task<List<int>> GetStudentIdsByTutorAsync(int tutorId);
    Task<List<int>> GetSessionIdsByStudentAsync(int studentId);
    Task<bool> AssignTutorByEmailAsync(string studentEmail, string tutorEmail);
}
