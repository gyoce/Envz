using System.Windows;

using Envz.Functional;
using Envz.Functional.Environments;
using Envz.Functional.Mediator;
using Envz.Infrastructure;
using Envz.UI.Views;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.UI;

public partial class App : System.Windows.Application
{
    private IServiceProvider? ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        ServiceCollection serviceCollection = ConfigureServices();
        ServiceProvider = serviceCollection.BuildServiceProvider();

        // TEMPORARY
        IMediator mediator = ServiceProvider.GetRequiredService<IMediator>();
        for (int i = 0; i < 10; i++)
            mediator.Send(new CreateEnvironmentRequest
            {
                Name = Random.Shared.NextInt64().ToString()
            });

        MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static ServiceCollection ConfigureServices()
    {
        ServiceCollection services = new();
        services.AddUi();
        services.AddInfrastructure();
        services.AddApplication();
        return services;
    }
}