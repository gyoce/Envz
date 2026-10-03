using Envz.Common.UI.Loc;
using Envz.Domain.Entities;
using Envz.Functional;
using Envz.Functional.Mediator;
using Envz.Functional.Settings;
using Envz.Infrastructure;
using Envz.UI.Services;
using Envz.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Envz.UI;

public partial class App : System.Windows.Application
{
    private IServiceProvider? ServiceProvider { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        ServiceProvider = ConfigureServices().BuildServiceProvider();
        Init();

        MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void Init()
    {
        ServiceProvider!.GetRequiredService<GlobalExceptionHandler>().Register(this);

        UserSettings settings = ServiceProvider!.GetRequiredService<IMediator>().Send(new GetSettingsRequest());
        Localizer.Instance.SetLanguage(SupportedLanguages.Find(settings.Language));
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