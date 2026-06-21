using Envz.Common.Services.Navigation;
using Envz.Common.ViewModels;

namespace Envz.FunctionalTests.Navigation;

public class PageViewModelHomeWithoutTitle : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Home;
}

public class PageViewModelHomeWithTitle : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Home;
    public override string Title => "Page View Model With Title";
}

public class PageViewModelHomeWithTitleThirdLevel : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Home;
    public override string Title => "Third level";
    public override int Level => 2;
}

public class PageViewModelHomeWithAnotherTitle : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Home;
    public override string Title => "Another Title";
}

public class PageViewModelEnvironmentsWithoutTitle : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
}

public class PageViewModelEnvironmentsWithTitle : PageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Environments;
    public override string Title => "Environments Sub Page";
}