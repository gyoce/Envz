namespace Envz.Common.ViewModels;

public interface IResultPageViewModel
{
    void Cancel();
}

public abstract class ResultPageViewModel<TResult> : PageViewModel, IResultPageViewModel
{
    private TaskCompletionSource<TResult?>? _completionSource;

    public Task<TResult?> WaitForResult()
    {
        _completionSource?.TrySetResult(default);
        _completionSource = new TaskCompletionSource<TResult?>(TaskCreationOptions.RunContinuationsAsynchronously);
        return _completionSource.Task;
    }

    public void Cancel() => Complete(default);

    protected void Complete(TResult? result)
    {
        TaskCompletionSource<TResult?>? completionSource = _completionSource;
        _completionSource = null;
        completionSource?.TrySetResult(result);
    }
}