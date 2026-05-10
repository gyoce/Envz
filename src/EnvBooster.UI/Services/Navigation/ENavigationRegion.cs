using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.Pages.Environments;

namespace EnvBooster.UI.Services.Navigation;

public enum ENavigationRegion
{
    Main,
    Environments
}

public static class NavigationRegionExtension
{
    private static readonly HashSet<Type> AllowedMainRegionTypes =
    [
        typeof(HomePageViewModel),
        typeof(EnvironmentsPageViewModel),
        typeof(SettingsPageViewModel)
    ];

    private static readonly HashSet<Type> AllowedEnvironmentsRegionTypes =
    [
        typeof(HomeEnvironmentsSubPageViewModel),
        typeof(CreateEnvironmentSubPageViewModel),
        typeof(EditEnvironmentSubPageViewModel)
    ];

    extension(ENavigationRegion region)
    {
        public bool HasViewModelTypeInside<TViewModel>()
        {
            return region switch
            {
                ENavigationRegion.Main => AllowedMainRegionTypes.Contains(typeof(TViewModel)),
                ENavigationRegion.Environments => AllowedEnvironmentsRegionTypes.Contains(typeof(TViewModel)),

                _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
            };
        }
    }
}