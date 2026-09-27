using Envz.Functional;
using Envz.Functional.Mediator;
using Envz.Infrastructure;
using Envz.Infrastructure.Configuration;
using Envz.Infrastructure.Configuration.Dtos;
using Envz.Infrastructure.Configuration.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Envz.CommonTests;

public abstract class BaseTestFixture : IDisposable
{
    private readonly Dictionary<Type, Mock> _mocks = [];
    private readonly Lazy<ServiceProvider> _serviceProvider;
    private readonly Lazy<IServiceScope> _scope;
    private IServiceCollection Services { get; } = new ServiceCollection();

    protected BaseTestFixture()
    {
        _serviceProvider = new Lazy<ServiceProvider>(BuildServiceProvider);
        _scope = new Lazy<IServiceScope>(() => _serviceProvider.Value.CreateScope());

        Services.AddFunctional();
        Services.AddInfrastructure();
        Services.Replace<IFileSystem, InMemoryFileSystem>();
    }

    public IServiceCollection ReplaceService<TOld, TNew>()
        where TNew : class
        where TOld : class
    {
        EnsureServiceProviderNotBuilt();
        return Services.Replace<TOld, TNew>();
    }

    public IServiceCollection ReplaceByMock<TService>()
        where TService : class
    {
        EnsureServiceProviderNotBuilt();
        return Services.ReplaceByMock(GetMock<TService>());
    }

    public TService GetService<TService>()
        where TService : class
    {
        return _scope.Value.ServiceProvider.GetRequiredService<TService>();
    }

    public TCast GetServiceAs<TService, TCast>()
        where TService : class
        where TCast : class
    {
        return (GetService<TService>() as TCast)!;
    }

    public Mock<TService> GetMock<TService>()
        where TService : class
    {
        if (!_mocks.TryGetValue(typeof(TService), out Mock? mock))
            _mocks[typeof(TService)] = mock = new Mock<TService>();
        return (Mock<TService>)mock;
    }

    public TReturn Send<TReturn>(IRequest<TReturn> request)
    {
        return GetService<IMediator>().Send(request);
    }

    public void Send(IRequest request)
    {
        GetService<IMediator>().Send(request);
    }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    public void SetConfiguration(ConfigurationDto configuration, IconsDto? icons = null)
    {
        ReplaceByMock<IConfigurationStore>();
        ReplaceByMock<IIconStore>();

        GetMock<IConfigurationStore>().Setup(store => store.Configuration).Returns(configuration);
        GetMock<IIconStore>().Setup(store => store.Icons).Returns(icons ?? new IconsDto());
    }

    private void EnsureServiceProviderNotBuilt()
    {
        if (_serviceProvider.IsValueCreated)
            throw new InvalidOperationException("Services cannot be modified once the service provider has been built.");
    }

    private ServiceProvider BuildServiceProvider()
    {
        ConfigureServices(Services);
        return Services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (_scope.IsValueCreated)
            _scope.Value.Dispose();
        if (_serviceProvider.IsValueCreated)
            _serviceProvider.Value.Dispose();
        GC.SuppressFinalize(this);
    }
}
