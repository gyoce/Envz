namespace Envz.Functional.Mediator;

public interface IUseCase<in TParam>
{
    public void Execute(TParam parameter);
}

public interface IUseCase<in TParam, out TReturn>
{
    public TReturn Execute(TParam parameter);
}