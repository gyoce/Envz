using Envz.Functional;
using Envz.Functional.Mediator;
using Envz.Infrastructure;
using Envz.Infrastructure.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.CommonTests;

public class BaseTestFixture : IDisposable
{
    private IServiceCollection Services { get; } = new ServiceCollection();

    private ServiceProvider? ServiceProvider
    {
        get => field ??= Services.BuildServiceProvider();
    }

    private IServiceScope Scope
    {
        get => field ??= ServiceProvider!.CreateScope();
    }

    public BaseTestFixture()
    {
        Services.AddFunctional();
        Services.AddInfrastructure();
    }

    public IServiceCollection ReplaceService<TOld, TNew>()
        where TNew : class
        where TOld : class
    {
        return Services.Replace<TOld, TNew>();
    }

    public IServiceCollection ReplaceByMock<TService>()
        where TService : class
    {
        return Services.ReplaceByMock<TService>();
    }

    public TCast GetServiceAs<TService, TCast>()
        where TService : class
        where TCast : class
    {
        return (Scope.ServiceProvider.GetRequiredService<TService>() as TCast)!;
    }

    public TService GetService<TService>()
        where TService : class
    {
        return Scope.ServiceProvider.GetRequiredService<TService>();
    }

    public Mock<TService> GetMock<TService>()
        where TService : class
    {
        return Scope.ServiceProvider.GetRequiredService<Mock<TService>>();
    }

    public TReturn Send<TReturn>(IRequest<TReturn> request)
    {
        return GetService<IMediator>().Send(request);
    }

    public void Send(IRequest request)
    {
        GetService<IMediator>().Send(request);
    }

    public void SetConfiguration(ConfigurationDto configuration)
    {
        Services.ReplaceByMock<IConfigurationStore>();
        GetMock<IConfigurationStore>().Setup(store => store.Configuration).Returns(configuration);
    }

    public void Dispose()
    {
        ServiceProvider?.Dispose();
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}
