using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class TutorDto
{
    public int IdTutor { get; set; }

    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Country must be a 2-letter code.")]
    public string? Country { get; set; }

    public DateTime? RegistrationDate { get; set; }
    public List<StudentBasicDto>? Students { get; set; }
}

public class CreateTutorDto
{
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^.{8,255}$", ErrorMessage = "PasswordHash must be between 8 and 255 characters.")]
    public string PasswordHash { get; set; } = null!;

    [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Country must be a 2-letter code.")]
    public string? Country { get; set; }
}

public class UpdateTutorDto
{
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Country must be a 2-letter code.")]
    public string? Country { get; set; }
}
