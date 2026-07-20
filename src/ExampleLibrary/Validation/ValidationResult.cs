using System.Collections.Generic;

namespace ExampleLibrary.Validation;

public class ValidationResult
{
    public bool IsValid { get; }
    public IReadOnlyList<string> Errors { get; }

    public ValidationResult(bool isValid, IReadOnlyList<string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }
}