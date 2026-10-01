using Envz.Functional;
using Envz.Infrastructure;
using Envz.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Envz.UI.Services;

namespace Envz.UI;

public partial class App : System.Windows.Application
{
    private IServiceProvider? ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        ServiceProvider = ConfigureServices().BuildServiceProvider();
        ServiceProvider.GetRequiredService<GlobalExceptionHandler>().Register(this);

        MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static ServiceCollection ConfigureServices()
    {
        ServiceCollection services = new();
        services.AddUi();
        services.AddInfrastructure();
        services.AddFunctional();
        return services;
    }
}