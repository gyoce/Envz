namespace Envz.Common.ViewModels;

public interface IDialogViewModel
{
    string Title { get; }
    bool IsCancellable { get; }
}

public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModel
{
    public event Action<bool?>? RequestClose;
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

    protected void Close(bool dialogResult = true) => RequestClose?.Invoke(dialogResult);
}

public abstract class DialogViewModelBase<TResult> : DialogViewModelBase
{
    public TResult? Result { get; set; }

    protected void Close(bool dialogResult, TResult? result)
    {
        Result = result;
        base.Close(dialogResult);
    }
}