namespace Envz.Functional.Mediator;

public interface IUseCase<in TParam>
{
    void Execute(TParam request);
}

public interface IUseCase<in TParam, out TReturn>
{
    TReturn Execute(TParam request);
}