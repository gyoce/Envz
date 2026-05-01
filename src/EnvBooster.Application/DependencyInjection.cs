using EnvBooster.Application.Environments;

using Microsoft.Extensions.DependencyInjection;

namespace EnvBooster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetEnvironmentsUseCase>();
        services.AddTransient<CreateEnvironmentUseCase>();
        return services;
    }
}