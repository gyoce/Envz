using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class EnvironmentDtoBuilder
{
    private readonly EnvironmentDto _environmentDto = new();

    public EnvironmentDtoBuilder WithName(string name)
    {
        _environmentDto.Name = name;
        return this;
    }

    public EnvironmentDtoBuilder WithApplication(EnvironmentApplicationDto application)
    {
        _environmentDto.Applications.Add(application);
        return this;
    }

    public EnvironmentDto Build()
    {
        return _environmentDto;
    }
}