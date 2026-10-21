using Envz.Application.Factories;
using Envz.Application.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Envz.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTool(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());

        services.AddSingleton<ToolFactory>();

        return services;
    }
}