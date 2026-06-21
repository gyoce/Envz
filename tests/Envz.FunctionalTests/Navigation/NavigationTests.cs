using Shouldly;

namespace Envz.FunctionalTests.Navigation;

public class NavigationTests : NavigationTestFixture
{
    [Test]
    public void ShouldNavigateCorrectlyFirstTime()
    {
        NavigationService.OnNavigationChanged += viewModel =>
        {
            viewModel.GetType().ShouldBe(typeof(PageViewModelHomeWithoutTitle));
        };
        NavigationService.NavigateTo<PageViewModelHomeWithoutTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(1);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");
    }

    [Test(Description = "Home[Home] > PageViewModelWithTitle[Home] => Environments")]
    public void ShouldResetAndRebuildBreadcrumbWhenNavigatingToAnotherCategory()
    {
        NavigationService.NavigateTo<PageViewModelHomeWithoutTitle>();
        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);

        NavigationService.NavigateTo<PageViewModelEnvironmentsWithoutTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(1);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Environments");
    }

    [Test(Description = "Home[Home] > PageViewModelWithTitle[Home] => Environments[Env] > PageViewModelWithTitle[Env]")]
    public void ShouldResetAndGoToSecondLevel()
    {
        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);

        NavigationService.NavigateTo<PageViewModelEnvironmentsWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Environments");
        NavigationService.Breadcrumb[1].Title.ShouldBe("Environments Sub Page");
    }

    [Test(Description = "Home[Home] > PageViewModelWithTitle[Home] => Home[Home] > AnotherTitle[Home]")]
    public void ShouldReplaceCurrentLevelWhenNavigatingToSiblingWithTitle()
    {
        NavigationService.NavigateTo<PageViewModelHomeWithoutTitle>();
        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);
        NavigationService.Breadcrumb[1].Title.ShouldBe("Page View Model With Title");

        NavigationService.NavigateTo<PageViewModelHomeWithAnotherTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");
        NavigationService.Breadcrumb[1].Title.ShouldBe("Another Title");
    }

    [Test(Description = "Home[Home] => Home[Home] > PageViewModelWithTitle[Home]")]
    public void ShouldAppendItemAtEndOfBreadcrumbWhenNavigatingToSubPage()
    {
        NavigationService.NavigateTo<PageViewModelHomeWithoutTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(1);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");

        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");
        NavigationService.Breadcrumb[1].Title.ShouldBe("Page View Model With Title");
    }

    [Test(Description = "Home[Home] > PageViewModelWithTitle[Home] => Home[Home] > PageViewModelWithTitle[Home] > ThirdLevel[Home]")]
    public void ShouldWorkWithThirdLevel()
    {
        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        NavigationService.Breadcrumb.Count.ShouldBe(2);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");
        NavigationService.Breadcrumb[1].Title.ShouldBe("Page View Model With Title");

        NavigationService.NavigateTo<PageViewModelHomeWithTitleThirdLevel>();
        NavigationService.Breadcrumb.Count.ShouldBe(3);
        NavigationService.Breadcrumb[0].Title.ShouldBe("Home");
        NavigationService.Breadcrumb[1].Title.ShouldBe("Page View Model With Title");
        NavigationService.Breadcrumb[2].Title.ShouldBe("Third level");
    }

    [Test]
    public void ShouldCallCallbackAsManyTimesAsNavigateTo()
    {
        int numberOfCallback = 0;
        NavigationService.OnNavigationChanged += viewModel =>
        {
            numberOfCallback++;
        };

        NavigationService.NavigateTo<PageViewModelHomeWithTitle>();
        numberOfCallback.ShouldBe(1);

        NavigationService.NavigateTo<PageViewModelHomeWithTitleThirdLevel>();
        numberOfCallback.ShouldBe(2);
    }
}
