using EnvBooster.UI.ViewModels.Pages;
using EnvBooster.UI.ViewModels.Pages.Environments;

namespace EnvBooster.UI.Services.Navigation;

public enum ENavigationMenu
{
    // Main
    HomePage,
    EnvironmentsPage,
    SettingsPage,

    // Environments
    HomeEnvironment,
    CreateEnvironment,
    EditEnvironment
}

public static class NavigationMenuExtension
{
    extension(ENavigationMenu navigation)
    {
        public Type ToViewModelType()
        {
            return navigation switch
            {
                ENavigationMenu.HomePage => typeof(HomePageViewModel),
                ENavigationMenu.EnvironmentsPage => typeof(EnvironmentsPageViewModel),
                ENavigationMenu.SettingsPage => typeof(SettingsPageViewModel),

                ENavigationMenu.HomeEnvironment => typeof(HomeEnvironmentsSubPageViewModel),
                ENavigationMenu.CreateEnvironment => typeof(CreateEnvironmentSubPageViewModel),
                ENavigationMenu.EditEnvironment => typeof(EditEnvironmentSubPageViewModel),

                _ => throw new ArgumentOutOfRangeException(nameof(navigation), navigation, null)
            };
        }

        public bool InsideOfRegion(ENavigationRegion region)
        {
            return region switch
            {
                ENavigationRegion.Main => navigation is ENavigationMenu.EnvironmentsPage or ENavigationMenu.HomePage or ENavigationMenu.SettingsPage,

                ENavigationRegion.Environments => navigation is ENavigationMenu.HomeEnvironment or ENavigationMenu.EditEnvironment or ENavigationMenu.CreateEnvironment,

                _ => throw new ArgumentOutOfRangeException(nameof(region), region, null)
            };
        }
    }
}