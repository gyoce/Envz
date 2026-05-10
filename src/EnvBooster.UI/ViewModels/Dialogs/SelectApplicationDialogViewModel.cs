using System.Windows.Input;

using EnvBooster.Domain.Entities;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels.Dialogs;

public class SelectApplicationDialogViewModel : DialogViewModelBase<EnvironmentApplication>
{
    public ICommand SelectApplicationCommand { get; }
    public ICommand CancelCommand { get; }
    public EnvironmentApplication Application { get; } = new();

    public SelectApplicationDialogViewModel()
    {
        SelectApplicationCommand = new RelayCommand(_ =>  Close(true, Application));
        CancelCommand = new RelayCommand(_ => Close(false));
    }
}