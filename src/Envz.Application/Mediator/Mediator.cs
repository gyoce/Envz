using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Application.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public void Send(IRequest request)
    {
        Type requestType = request.GetType();
        Type useCaseType = typeof(IUseCase<>).MakeGenericType(requestType);
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IUseCase<>.Execute))!;
        executeMethod.Invoke(useCase, [request]);
    }

    public TReturn Send<TReturn>(IRequest<TReturn> request)
    {
        Type requestType = request.GetType();
        Type useCaseType = typeof(IUseCase<,>).MakeGenericType(requestType, typeof(TReturn));
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IUseCase<,>.Execute))!;
        return (TReturn)executeMethod.Invoke(useCase, [request])!;
    }
}