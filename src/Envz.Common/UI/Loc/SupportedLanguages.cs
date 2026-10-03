using System.Globalization;

namespace Envz.Common.UI.Loc;

public record LanguageOption(CultureInfo Culture)
{
    public string DisplayName { get; } = char.ToUpper(Culture.NativeName[0], Culture) + Culture.NativeName[1..];
}

public static class SupportedLanguages
{
    public static IReadOnlyList<LanguageOption> All { get; } = 
    [
        new(new CultureInfo("en")), 
        new(new CultureInfo("fr"))
    ];

    public static LanguageOption Find(string? name)
    {
        return All.FirstOrDefault(language => language.Culture.Name == name) ?? All[0];
    }        
}