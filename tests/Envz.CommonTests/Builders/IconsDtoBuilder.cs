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

    public IconsDtoBuilder WithIcons(params (string toolName, string icon)[] icons)
    {
        foreach ((string toolName, string icon) in icons)
            WithIcon(toolName, icon);
        return this;
    }

    public IconsDto Build()
    {
        return _iconsDto;
    }

    public static implicit operator IconsDto(IconsDtoBuilder builder) => builder.Build();
}