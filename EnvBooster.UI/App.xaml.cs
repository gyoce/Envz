using System.Windows;
using EnvBooster.Application;
using EnvBooster.Infrastructure;
using EnvBooster.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public partial class App : System.Windows.Application
{
    private IServiceProvider? ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        ServiceCollection serviceCollection = ConfigureServices();
        ServiceProvider = serviceCollection.BuildServiceProvider();

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