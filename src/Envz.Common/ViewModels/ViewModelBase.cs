using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Envz.Common.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void OnEnable()
    {

    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}