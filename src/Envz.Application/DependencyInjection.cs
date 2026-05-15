using System.Reflection;

using Envz.Application.Mediator;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());
        return services;
    }
}