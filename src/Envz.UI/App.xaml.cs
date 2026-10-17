using Envz.Application;
using Envz.Application.Mediator;
using Envz.Application.Requests.Settings;
using Envz.Application.Requests.Tools;
using Envz.Application.Requests.Workspaces;
using Envz.Common.UI.Loc;
using Envz.Domain.Entities;
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
        IMediator mediator = ServiceProvider!.GetRequiredService<IMediator>();

        UserSettings settings = mediator.Send(new GetSettingsRequest());
        Localizer.Instance.SetLanguage(SupportedLanguages.Find(settings.Language));

        mediator.Send(new GetToolsRequest());
        mediator.Send(new GetWorkspacesRequest());
    }

    private static ServiceCollection ConfigureServices()
    {
        ServiceCollection services = new();
        services.AddUi();
        services.AddInfrastructure();
        services.AddTool();
        return services;
    }
}