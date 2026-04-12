using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class SessionDto
{
    public int IdSession { get; set; }
    public int IdStudent { get; set; }
    public DateTime? BeginningDate { get; set; }
    public DateTime? EndDate { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-_]{0,50}$", ErrorMessage = "Device may contain letters, numbers, spaces, hyphens and underscores.")]
    public string? Device { get; set; }
    public StudentBasicDto? Student { get; set; }
    public List<LevelResultBasicDto>? LevelResults { get; set; }
}

public class CreateSessionDto
{
    public int IdStudent { get; set; }
    public DateTime? BeginningDate { get; set; }
    public DateTime? EndDate { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-_]{0,50}$", ErrorMessage = "Device may contain letters, numbers, spaces, hyphens and underscores.")]
    public string? Device { get; set; }
}

public class UpdateSessionDto
{
    public int IdStudent { get; set; }
    public DateTime? BeginningDate { get; set; }
    public DateTime? EndDate { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-_]{0,50}$", ErrorMessage = "Device may contain letters, numbers, spaces, hyphens and underscores.")]
    public string? Device { get; set; }
}
