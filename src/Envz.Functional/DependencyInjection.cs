using System.Reflection;

using Envz.Functional.Mediator;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Functional;

public static class DependencyInjection
{
    public static IServiceCollection AddFunctional(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());
        return services;
    }
}