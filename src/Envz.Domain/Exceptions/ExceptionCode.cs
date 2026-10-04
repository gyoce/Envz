namespace Envz.Domain.Exceptions;

public enum ExceptionCode
{
    CreateApplicationAlreadyExists,
    DeleteApplicationNotFound,
    DeleteApplicationStillUsed,
    CreateEnvironmentApplicationNotFound,
    CreateEnvironmentInvalidEnvironmentName
}