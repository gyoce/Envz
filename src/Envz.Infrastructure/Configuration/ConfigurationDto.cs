using System.Text.Json.Serialization;

namespace Envz.Infrastructure.Configuration;

using Domain.Entities;

public class ConfigurationDto
{
    [JsonPropertyName("environments")]
    public List<EnvironmentDto> Environments { get; set; } = [];

    [JsonPropertyName("applications")]
    public List<ApplicationDto> Applications { get; set; } = [];
}

public class EnvironmentDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("applications")]
    public List<EnvironmentApplicationDto> Applications { get; set; } = [];

    public Environment ToDomainEntity()
    {
        return new Environment
        {
            Name = Name,
            Applications = Applications.Select(app => app.ToDomainEntity()).ToList()
        };
    }
}

public class ApplicationDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    public Application ToDomainEntity()
    {
        return new Application
        {
            Name = Name,
            Path = Path
        };
    }
}

public class EnvironmentApplicationDto
{
    [JsonPropertyName("applicationName")]
    public string ApplicationName { get; set; } = string.Empty;

    [JsonPropertyName("parameter")]
    public string? Parameter { get; set; }

    public EnvironmentApplication ToDomainEntity()
    {
        return new EnvironmentApplication
        {
            ApplicationName = ApplicationName,
            Parameter = Parameter
        };
    }
}

public static class ConfigurationDtoExtensionMethods
{
    extension(Application application)
    {
        public ApplicationDto ToDto()
        {
            return new ApplicationDto
            {
                Name = application.Name,
                Path = application.Path
            };
        }
    }

    extension(Environment environment)
    {
        public EnvironmentDto ToDto()
        {
            return new EnvironmentDto
            {
                Name = environment.Name,
                Applications = environment.Applications.Select(app => app.ToDto()).ToList()
            };
        }
    }

    extension(EnvironmentApplication environmentApplication)
    {
        public EnvironmentApplicationDto ToDto()
        {
            return new EnvironmentApplicationDto
            {
                ApplicationName = environmentApplication.ApplicationName,
                Parameter = environmentApplication.Parameter
            };
        }
    }
}