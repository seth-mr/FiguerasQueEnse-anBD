using System.ComponentModel.DataAnnotations;

namespace MicroservicioFiguras.DTOs;

public class LevelResultDto
{
    public int IdResult { get; set; }
    public int IdSession { get; set; }
    public int IdLevel { get; set; }
    public int? FinishingTime { get; set; }
    public int? Attempts { get; set; }
    public int? Fails { get; set; }
    public bool? Completed { get; set; }
    public SessionBasicDto? Session { get; set; }
    public LevelBasicDto? Level { get; set; }
}

public class CreateLevelResultDto
{
    public int IdSession { get; set; }
    public int IdLevel { get; set; }
    public int? FinishingTime { get; set; }
    public int? Attempts { get; set; }
    public int? Fails { get; set; }
    public bool? Completed { get; set; }
}

public class UpdateLevelResultDto
{
    public int IdSession { get; set; }
    public int IdLevel { get; set; }
    public int? FinishingTime { get; set; }
    public int? Attempts { get; set; }
    public int? Fails { get; set; }
    public bool? Completed { get; set; }
}
