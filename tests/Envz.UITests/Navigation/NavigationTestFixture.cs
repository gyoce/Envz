using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Envz.UITests.Navigation;

public class NavigationTestFixture : IDisposable
{
    protected IServiceProvider ServiceProvider;
    protected INavigationService NavigationService;

    public NavigationTestFixture()
    {
        Strings.Culture = new CultureInfo("en");

        IServiceCollection services = new ServiceCollection();
        services.AddTransient<PageViewModelHomeWithoutTitle>();
        services.AddTransient<PageViewModelHomeWithTitle>();
        services.AddTransient<PageViewModelHomeWithAnotherTitle>();
        services.AddTransient<PageViewModelWorkspacesWithoutTitle>();
        services.AddTransient<PageViewModelWorkspacesWithTitle>();
        services.AddTransient<PageViewModelHomeWithTitleThirdLevel>();
        services.AddTransient<PageViewModelHomeResult>();
        services.AddTransient<PageViewModelHomeSiblingResult>();
        services.AddTransient<TrackedPage>();
        ServiceProvider = services.BuildServiceProvider();

        NavigationService = new NavigationService(ServiceProvider);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}