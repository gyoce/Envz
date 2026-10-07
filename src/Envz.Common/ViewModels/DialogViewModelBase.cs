namespace Envz.Common.ViewModels;

public interface IDialogViewModel
{
    string Title { get; }
    bool IsCancellable { get; }
}

public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModel
{
    public event Action? RequestClose;
    public string Title
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;
    public virtual bool IsCancellable => true;

    protected void Close() => RequestClose?.Invoke();
}

public abstract class DialogViewModelBase<TResult> : DialogViewModelBase
{
    public TResult? Result { get; set; }

    protected void Close(TResult? result)
    {
        Result = result;
        Close();
    }
}