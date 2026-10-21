using System.Collections;
using System.ComponentModel;

namespace Envz.Common.ViewModels;

public abstract class ValidatablePageViewModel : PageViewModel, INotifyDataErrorInfo
{
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public bool HasErrors => _errors.Count > 0;

    private readonly Dictionary<string, string> _errors = [];

    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName is not null && _errors.TryGetValue(propertyName, out string? error) ? new[] { error } : [];
    }

    protected void SetError(string propertyName, string? error)
    {
        bool changed = error is null
            ? _errors.Remove(propertyName)
            : !_errors.TryGetValue(propertyName, out string? current) | (current != (_errors[propertyName] = error));

        if (!changed)
            return;

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
    }

    protected void ClearErrors()
    {
        foreach (string propertyName in _errors.Keys.ToList())
            SetError(propertyName, null);
    }
}