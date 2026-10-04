namespace Envz.Domain.Exceptions;

public class EnvzException(string message, ExceptionCode code, params object[] args) : Exception(message)
{
    public ExceptionCode Code { get; } = code;
    public object[] Args { get; } = args;
}