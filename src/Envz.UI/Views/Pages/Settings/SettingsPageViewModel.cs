using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.ViewModels;
using Envz.Functional.Mediator;
using Envz.Functional.Settings;

namespace Envz.UI.Views.Pages.Settings;

public class SettingsPageViewModel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Settings;

    public static IReadOnlyList<LanguageOption> Languages => SupportedLanguages.All;
    public LanguageOption SelectedLanguage
    {
        get;
        set
        {
            if (Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged();
            Localizer.Instance.SetLanguage(value);
            _mediator.Send(new UpdateLanguageRequest { Language = value.Culture.Name });
        }
    } = SupportedLanguages.Find(Strings.Culture?.Name);

    private readonly IMediator _mediator;

    public SettingsPageViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }
}