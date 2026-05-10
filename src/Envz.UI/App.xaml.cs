using System.Windows;

using Envz.Application;
using Envz.Application.Environments;
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
        CreateEnvironmentUseCase useCase = ServiceProvider.GetRequiredService<CreateEnvironmentUseCase>();
        for (int i = 0; i < 10; i++)
            useCase.Execute(new CreateEnvironmentRequest
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