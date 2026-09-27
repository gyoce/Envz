using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.CommonTests.Builders;

public class IconsDtoBuilder
{
    private readonly IconsDto _iconsDto = new();

    public IconsDtoBuilder WithIcon(string applicationName, string icon)
    {
        _iconsDto.ApplicationIcons[applicationName] = icon;
        return this;
    }

    public IconsDto Build()
    {
        return _iconsDto;
    }
}