using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MicroservicioFiguras.Helpers;

public static class DtoValidationHelper
{
    public static bool TryValidate<T>(T? dto, out List<string> errors)
    {
        if (dto is null)
        {
            errors = new List<string> { "Request body cannot be null." };
            return false;
        }

        var validationContext = new ValidationContext(dto);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        errors = validationResults.Select(result => result.ErrorMessage ?? "Validation failed").ToList();
        return isValid;
    }
}
