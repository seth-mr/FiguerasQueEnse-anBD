using System;
using System.Collections.Generic;

namespace MicroservicioFiguras.Models;

public partial class Student
{
    public int IdStudent { get; set; }

    public int? IdTutor { get; set; }

    public int Age { get; set; }

    public char Genre { get; set; }

    public string? Country { get; set; }

    public string? Neurodivergency { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual Tutor? IdTutorNavigation { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
