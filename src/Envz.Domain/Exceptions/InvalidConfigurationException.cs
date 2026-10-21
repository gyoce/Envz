namespace Envz.Domain.Exceptions;

public class InvalidConfigurationException(string detail)
    : EnvzException($"Configuration is invalid: {detail}", ErrorCode.ConfigurationInvalid, detail);