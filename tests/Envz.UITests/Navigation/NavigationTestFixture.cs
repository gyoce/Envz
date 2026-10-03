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
        services.AddSingleton<PageViewModelHomeWithoutTitle>();
        services.AddSingleton<PageViewModelHomeWithTitle>();
        services.AddSingleton<PageViewModelHomeWithAnotherTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithoutTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithTitle>();
        services.AddSingleton<PageViewModelHomeWithTitleThirdLevel>();
        services.AddSingleton<PageViewModelHomeResult>();
        ServiceProvider = services.BuildServiceProvider();

        NavigationService = new NavigationService(ServiceProvider);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}