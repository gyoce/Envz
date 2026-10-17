using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Envz.Application.Mediator;

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

    public async Task<TReturn> SendAsync<TReturn>(IRequest<TReturn> request)
    {
        Type useCaseType = typeof(IAsyncUseCase<,>).MakeGenericType(request.GetType(), typeof(TReturn));
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IAsyncUseCase<,>.ExecuteAsync))!;

        Task<TReturn> task;
        try
        {
            task = (Task<TReturn>)executeMethod.Invoke(useCase, [request])!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            throw;
        }

        return await task;
    }

    public async Task SendAsync<TReturn>(IRequest request)
    {
        Type useCaseType = typeof(IAsyncUseCase<>).MakeGenericType(request.GetType());
        object useCase = serviceProvider.GetRequiredService(useCaseType);
        MethodInfo executeMethod = useCaseType.GetMethod(nameof(IAsyncUseCase<>.ExecuteAsync))!;

        Task task;
        try
        {
            task = (Task)executeMethod.Invoke(useCase, [request])!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
            throw;
        }

        await task;
    }
}