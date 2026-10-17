using Envz.Application.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Envz.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTool(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());
        return services;
    }
}