using Envz.Common.Services.Navigation;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.FunctionalTests.Navigation;

[TestFixture]
public class NavigationTestFixture : IDisposable
{
    protected IServiceProvider ServiceProvider = null!;
    protected INavigationService NavigationService = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<PageViewModelHomeWithoutTitle>();
        services.AddSingleton<PageViewModelHomeWithTitle>();
        services.AddSingleton<PageViewModelHomeWithAnotherTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithoutTitle>();
        services.AddSingleton<PageViewModelEnvironmentsWithTitle>();
        services.AddSingleton<PageViewModelHomeWithTitleThirdLevel>();
        ServiceProvider = services.BuildServiceProvider();
    }

    [SetUp]
    public void Setup()
    {
        NavigationService = new NavigationService(ServiceProvider);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}