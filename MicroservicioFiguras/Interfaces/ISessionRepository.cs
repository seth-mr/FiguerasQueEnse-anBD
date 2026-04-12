using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicioFiguras.DTOs;
using MicroservicioFiguras.Models;

namespace MicroservicioFiguras.Interfaces;

public interface ISessionRepository : IRepository<Session>
{
    Task<List<SessionDto>> GetAllWithRelationsAsync();
    Task<SessionDto?> GetByIdWithRelationsAsync(int id);
}
