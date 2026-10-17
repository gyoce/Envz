using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class ToolDtoBuilder
{
    private readonly ToolDto _toolDto = new();

    public ToolDtoBuilder WithName(string name)
    {
        _toolDto.Name = name;
        return this;
    }

    public ToolDtoBuilder WithPath(string path)
    {
        _toolDto.Path = path;
        return this;
    }

    public ToolDto Build()
    {
        return _toolDto;
    }
}