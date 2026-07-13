using Envz.Infrastructure.Configuration;

namespace Envz.CommonTests.Builders;

public class ApplicationDtoBuilder
{
    private readonly ApplicationDto _applicationDto = new();

    public ApplicationDtoBuilder WithName(string name)
    {
        _applicationDto.Name = name;
        return this;
    }

    public ApplicationDto Build()
    {
        return _applicationDto;
    }
}