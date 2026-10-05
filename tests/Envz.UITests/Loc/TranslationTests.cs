using Envz.Common.UI.Loc;
using Envz.Domain.Exceptions;
using System.Resources;

namespace Envz.UITests.Loc;

public class TranslationTests
{
    [Fact]
    public void ShouldHaveAllTranslationForExceptionCode()
    {
        foreach (LanguageOption language in SupportedLanguages.All)
        {
            ResourceSet? resources = Strings.ResourceManager.GetResourceSet(language.Culture, true, false);
            resources.ShouldNotBeNull($"No resources for {language.Culture.Name}");

            foreach (ExceptionCode code in Enum.GetValues<ExceptionCode>())
                resources.GetString($"KnownError_{code}").ShouldNotBeNullOrEmpty($"{code} is not translated in `{language.Culture.Name}`.");
        }
    }
}