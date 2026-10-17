using Envz.Common.UI.Loc;

namespace Envz.Common.Services.Navigation;

public enum ENavigationCategory
{
    Home,
    Workspaces,
    Tools,
    Settings
}

public static class NavigationCategoryExtensions
{
    extension(ENavigationCategory navigatonCategory)
    {
        public string ToBreadcrumbTitle()
        {
            return navigatonCategory switch
            {
                ENavigationCategory.Home => Strings.Global_Home,
                ENavigationCategory.Workspaces => Strings.Global_Workspaces,
                ENavigationCategory.Tools => Strings.Global_Tools,
                ENavigationCategory.Settings => Strings.Global_Settings,
                _ => throw new ArgumentOutOfRangeException(nameof(navigatonCategory), navigatonCategory, null)
            };
        }
    }
}