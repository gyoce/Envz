namespace Envz.Application.Mediator;

public interface IMediator
{
    void Send(IRequest request);
    TReturn Send<TReturn>(IRequest<TReturn> request);
}