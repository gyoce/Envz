namespace Envz.Functional.Mediator;

public interface IUseCase<in TParam>
{
    void Execute(TParam parameter);
}

public interface IUseCase<in TParam, out TReturn>
{
    TReturn Execute(TParam parameter);
}