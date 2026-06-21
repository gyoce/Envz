namespace Envz.Common.Services.Navigation;

public enum ENavigationCategory
{
    Home,
    Environments,
    Applications,
    Settings
}

public static class NavigationCategoryExtensionMethods
{
    extension(ENavigationCategory navigatonCategory)
    {
        public string ToBreadcrumbTitle()
        {
            return navigatonCategory switch
            {
                ENavigationCategory.Home => "Home",
                ENavigationCategory.Environments => "Environments",
                ENavigationCategory.Applications => "Applications",
                ENavigationCategory.Settings => "Settings",
                _ => throw new ArgumentOutOfRangeException(nameof(navigatonCategory), navigatonCategory, null)
            };
        }
    }
}