using Envz.UI.ViewModels.Pages;
using Envz.UI.ViewModels.Pages.Applications;
using Envz.UI.ViewModels.Pages.Environments;

namespace Envz.UI.Services.Navigation;

public enum ENavigationRegion
{
    Main,
    Environments,
    Applications
}

public static class NavigationRegionExtension
{
    private static readonly HashSet<Type> AllowedMainRegionTypes =
    [
        typeof(HomePageViewModel),
        typeof(EnvironmentsPageViewModel),
        typeof(ApplicationsPageViewModel),
        typeof(SettingsPageViewModel)
    ];

    private static readonly HashSet<Type> AllowedEnvironmentsRegionTypes =
    [
        typeof(HomeEnvironmentsSubPageViewModel),
        typeof(CreateEnvironmentSubPageViewModel),
        typeof(EditEnvironmentSubPageViewModel)
    ];

    private static readonly HashSet<Type> AllowedApplicationsRegionTypes =
    [
        typeof(HomeApplicationsSubPageViewModel),
        typeof(AddApplicationSubPageViewModel)
    ];

    extension(ENavigationRegion region)
    {
        public bool HasViewModelTypeInside<TViewModel>()
        {
            return region switch
            {
                ENavigationRegion.Main => AllowedMainRegionTypes.Contains(typeof(TViewModel)),
                ENavigationRegion.Environments => AllowedEnvironmentsRegionTypes.Contains(typeof(TViewModel)),
                ENavigationRegion.Applications => AllowedApplicationsRegionTypes.Contains(typeof(TViewModel)),

                _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
            };
        }
    }
}