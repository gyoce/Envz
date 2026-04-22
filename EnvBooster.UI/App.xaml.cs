using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public partial class App : Application
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
        return services;
    }
}