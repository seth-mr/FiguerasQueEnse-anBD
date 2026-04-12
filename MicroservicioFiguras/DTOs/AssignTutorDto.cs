using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class AssignTutorDto
{
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "StudentEmail must be a valid address.")]
    public string StudentEmail { get; set; } = null!;

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "TutorEmail must be a valid address.")]
    public string TutorEmail { get; set; } = null!;
}
