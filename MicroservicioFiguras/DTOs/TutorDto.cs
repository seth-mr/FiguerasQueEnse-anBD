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
}
