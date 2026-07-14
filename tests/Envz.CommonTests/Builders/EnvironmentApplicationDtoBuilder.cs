using Envz.Infrastructure.Configuration;

namespace Envz.CommonTests.Builders;

public class EnvironmentApplicationDtoBuilder
{
    private readonly EnvironmentApplicationDto _environmentApplicationDto = new();

    public EnvironmentApplicationDtoBuilder WithApplicationName(string applicationName)
    {
        _environmentApplicationDto.ApplicationName = applicationName;
        return this;
    }

    public EnvironmentApplicationDtoBuilder WithParameter(string? parameter)
    {
        _environmentApplicationDto.Parameter = parameter;
        return this;
    }

    public EnvironmentApplicationDto Build()
    {
        return _environmentApplicationDto;
    }
}