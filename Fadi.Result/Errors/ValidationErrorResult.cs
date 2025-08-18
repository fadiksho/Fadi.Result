namespace Fadi.Result.Errors;

public record ValidationErrorResult : ResultError
{
  public ValidationErrorResult()
    : base(nameof(ValidationErrorResult))
  {

  }
  public ValidationErrorResult(params ValidationError[] validationErrors) : base(nameof(ValidationErrorResult))
  {
    ValidationErrors = validationErrors;
  }

  public ValidationError[] ValidationErrors { get; set; } = [];
}

public class ValidationError
{
  public string Message { get; set; } = string.Empty;
  public string Identifier { get; set; } = string.Empty;
  public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
}

public enum ValidationSeverity
{
  Error = 0,
  Warning = 1,
  Info = 2
}
