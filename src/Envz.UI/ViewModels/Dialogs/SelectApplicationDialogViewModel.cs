using System.Windows.Input;

using Envz.Domain.Entities;
using Envz.UI.Utils;

namespace Envz.UI.ViewModels.Dialogs;

public class SelectApplicationDialogViewModel : DialogViewModelBase<EnvironmentApplication>
{
    public ICommand SelectApplicationCommand { get; }
    public ICommand CancelCommand { get; }
    public EnvironmentApplication Application { get; } = new();

    public SelectApplicationDialogViewModel()
    {
        SelectApplicationCommand = new RelayCommand(_ => Close(true, Application));
        CancelCommand = new RelayCommand(_ => Close(false));
    }
}