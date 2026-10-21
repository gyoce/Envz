using Envz.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Envz.Domain.Validation;

public class ValidationResult<T>
    where T : class
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsValid => Error is null;

    public T? Value { get; }
    public ValidationException? Error { get; }

    private ValidationResult(T? value, ValidationException? error)
    {
        Value = value;
        Error = error;
    }

    public static ValidationResult<T> Success(T value) => new(value, null);
    public static ValidationResult<T> Fail(ValidationException error) => new(null, error);
}