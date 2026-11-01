using Envz.Application.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Envz.Application.Validators;

namespace Envz.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());

        services.AddSingleton<ToolValidator>();

        return services;
    }
}