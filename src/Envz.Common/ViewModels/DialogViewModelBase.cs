namespace Envz.Common.ViewModels;

public interface IDialogViewModel
{
    string Title { get; }
    bool IsCancellable { get; }
}

public abstract class DialogViewModelBase<TResult> : ViewModelBase, IDialogViewModel
{
    public TResult? Result { get; set; }
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

    protected void Close(bool dialogResult, TResult? result = default)
    {
        Result = result;
        RequestClose?.Invoke(dialogResult);
    }
}