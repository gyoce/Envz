namespace Envz.Domain.Exceptions;

public class EnvzException(string message, ErrorCode code, params object[] args) : Exception(message)
{
    public ErrorCode Code { get; } = code;
    public object[] Args { get; } = args;
}