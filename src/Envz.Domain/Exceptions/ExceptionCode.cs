namespace Envz.Domain.Exceptions;

public enum ExceptionCode
{
    ApplicationInvalidName,
    ApplicationInvalidPath,
    EnvironmentInvalidName,

    ConfigurationInvalid,

    CreateEnvironmentApplicationNotFound,
    CreateApplicationAlreadyExists,
    CreateEnvironmentAlreadyExists,
    DeleteApplicationStillUsed,
    DeleteApplicationNotFound,
}