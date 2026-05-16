using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Functional.Mediator;

public static class MediatorConfigurator
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IMediator, Mediator>();

        IEnumerable<Type> useCases = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Where(t => t.GetInterfaces().Any(IsUseCaseInterface));

        foreach (Type useCaseType in useCases)
        {
            IEnumerable<Type> useCaseInterfaces = useCaseType.GetInterfaces().Where(IsUseCaseInterface);
            foreach (Type useCaseInterface in useCaseInterfaces)
                services.AddTransient(useCaseInterface, useCaseType);
        }

        return services;
    }

    private static bool IsUseCaseInterface(Type type)
    {
        if (!type.IsGenericType)
            return false;

        Type definition = type.GetGenericTypeDefinition();
        return definition == typeof(IUseCase<>) || definition == typeof(IUseCase<,>);
    }
}