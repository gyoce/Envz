using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class ApplicationDtoBuilder
{
    private readonly ApplicationDto _applicationDto = new();

    public ApplicationDtoBuilder WithName(string name)
    {
        _applicationDto.Name = name;
        return this;
    }

    public ApplicationDtoBuilder WithPath(string path)
    {
        _applicationDto.Path = path;
        return this;
    }

    public ApplicationDto Build()
    {
        return _applicationDto;
    }
}