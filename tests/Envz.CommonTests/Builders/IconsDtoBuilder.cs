using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class IconsDtoBuilder
{
    private readonly IconsDto _iconsDto = new();

    public IconsDtoBuilder WithIcon(string toolName, string icon)
    {
        _iconsDto.ToolIcons[toolName] = icon;
        return this;
    }

    public IconsDto Build()
    {
        return _iconsDto;
    }
}