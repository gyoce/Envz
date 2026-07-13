using Envz.Infrastructure.Configuration;

namespace Envz.CommonTests.Builders;

public class ConfigurationDtoBuilder
{
    private readonly ConfigurationDto _configuration = new();

    public ConfigurationDtoBuilder WithApplication(ApplicationDto application)
    {
        _configuration.Applications.Add(application);
        return this;
    }

    public ConfigurationDto Build()
    {
        return _configuration;
    }
}