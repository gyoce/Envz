namespace EnvBooster.UI.ViewModels.Dialogs;

public abstract class DialogViewModelBase<TResult> : ViewModelBase
{
    public TResult? Result { get; set; }
    public event Action<bool?>? RequestClose;

    protected void Close(bool dialogResult, TResult? result = default)
    {
        Result = result;
        RequestClose?.Invoke(dialogResult);
    }
}