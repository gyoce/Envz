using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        return services;
    }
}
