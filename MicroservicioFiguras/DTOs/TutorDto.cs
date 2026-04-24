using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class TutorDto
{
    public int IdTutor { get; set; }

    [StringLength(120, ErrorMessage = "Name must be 120 characters or fewer.")]
    public string? Name { get; set; }

    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country must be a 2-letter uppercase code.")]
    public string? Country { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Grade { get; set; }

    public DateTime? RegistrationDate { get; set; }
    public List<StudentBasicDto>? Students { get; set; }
}

public class CreateTutorDto
{
    [Required]
    [StringLength(120, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 120 characters.")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "PasswordHash must be between 8 and 255 characters.")]
    public string PasswordHash { get; set; } = null!;

    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country must be a 2-letter uppercase code.")]
    public string? Country { get; set; }

    [RegularExpression(@"^(Masculino|Femenino|Otro)$", ErrorMessage = "Gender must be 'Masculino', 'Femenino', or 'Otro'.")]
    public string? Gender { get; set; }

    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int? Age { get; set; }

    [RegularExpression("^(licenciatura|Maestria|Doctorado|Post Doctorado|Padre o Madre)$",
        ErrorMessage = "Grade must be one of: licenciatura, Maestria, Doctorado, Post Doctorado, Padre o Madre.")]
    public string? Grade { get; set; }
}

public class UpdateTutorDto
{
    [Required]
    [StringLength(120, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 120 characters.")]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be a valid address.")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country must be a 2-letter uppercase code.")]
    public string? Country { get; set; }

    [RegularExpression(@"^(Masculino|Femenino|Otro)$", ErrorMessage = "Gender must be 'Masculino', 'Femenino', or 'Otro'.")]
    public string? Gender { get; set; }

    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int? Age { get; set; }

    [RegularExpression("^(licenciatura|Maestria|Doctorado|Post Doctorado|Padre o Madre)$",
        ErrorMessage = "Grade must be one of: licenciatura, Maestria, Doctorado, Post Doctorado, Padre o Madre.")]
    public string? Grade { get; set; }
}
