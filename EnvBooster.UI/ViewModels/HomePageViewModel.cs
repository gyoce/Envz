using System.Collections.ObjectModel;
using System.Windows.Input;
using EnvBooster.Application.Environments;
using EnvBooster.UI.Utils;

namespace EnvBooster.UI.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    public ObservableCollection<Environment> Environments { get; } = [];
    public ICommand GetEnvironmentsCommand { get; }

    private readonly GetEnvironmentsUseCase _getEnvironmentsUseCase;

    public HomePageViewModel(GetEnvironmentsUseCase getEnvironmentsUseCase)
    {
        this._getEnvironmentsUseCase = getEnvironmentsUseCase;
        GetEnvironmentsCommand = new RelayCommand(_ => LoadEnvironments());
    }

    private void LoadEnvironments()
    {
        Environments.Clear();
        IReadOnlyCollection<Environment> environments = _getEnvironmentsUseCase.Execute();
        foreach (Environment environment in environments)
        {
            Environments.Add(environment);
        }
    }
}