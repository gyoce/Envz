using Envz.Common.UI.Loc;

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
                ENavigationCategory.Home => Strings.Global_Home,
                ENavigationCategory.Environments => Strings.Global_Environments,
                ENavigationCategory.Applications => Strings.Global_Applications,
                ENavigationCategory.Settings => Strings.Global_Settings,
                _ => throw new ArgumentOutOfRangeException(nameof(navigatonCategory), navigatonCategory, null)
            };
        }
    }
}