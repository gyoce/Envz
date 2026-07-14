using System.Reflection;
using System.Runtime.ExceptionServices;

using Microsoft.Extensions.DependencyInjection;

namespace Envz.Functional.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public void Send(IRequest request)
    {
        Type requestType = request.GetType();
        Type useCaseType = typeof(IUseCase<>).MakeGenericType(requestType);
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IUseCase<>.Execute))!;

        try
        {
            executeMethod.Invoke(useCase, [request]);
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
        }
    }

    public TReturn Send<TReturn>(IRequest<TReturn> request)
    {
        Type requestType = request.GetType();
        Type useCaseType = typeof(IUseCase<,>).MakeGenericType(requestType, typeof(TReturn));
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IUseCase<,>.Execute))!;

        try
        {
            return (TReturn)executeMethod.Invoke(useCase, [request])!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            throw;
        }
    }
}