namespace Envz.Functional.Mediator;

public interface IMediator
{
    void Send(IRequest request);
    TReturn Send<TReturn>(IRequest<TReturn> request);
    Task<TReturn> SendAsync<TReturn>(IRequest<TReturn> request);
    Task SendAsync<TReturn>(IRequest request);
}