using Envz.Common.Services.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.FunctionalTests.Navigation;

public class NavigationTestFixture : IDisposable
{
    protected IServiceProvider ServiceProvider = null!;
    protected INavigationService NavigationService = null!;

    public NavigationTestFixture()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<PageViewModelHomeWithoutTitle>();
        services.AddSingleton<PageViewModelHomeWithTitle>();
        services.AddSingleton<PageViewModelHomeWithAnotherTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithoutTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithTitle>();
        services.AddSingleton<PageViewModelHomeWithTitleThirdLevel>();
        ServiceProvider = services.BuildServiceProvider();

        NavigationService = new NavigationService(ServiceProvider);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}