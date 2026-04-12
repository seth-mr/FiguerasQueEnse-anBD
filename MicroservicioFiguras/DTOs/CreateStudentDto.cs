using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class CreateStudentDto
{
    public int? IdTutor { get; set; }

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s+]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^.{8,255}$", ErrorMessage = "PasswordHash must be between 8 and 255 characters.")]
    public string? PasswordHash { get; set; }

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int Age { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z]$", ErrorMessage = "Genre must be a single letter.")]
    public char Genre { get; set; }

    [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Country must be a 2-letter code.")]
    public string? Country { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-_]{0,50}$", ErrorMessage = "Neurodivergency may contain letters, numbers, spaces, hyphens and underscores.")]
    public string? Neurodivergency { get; set; }
}
