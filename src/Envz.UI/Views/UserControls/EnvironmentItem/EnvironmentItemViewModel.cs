using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.UI.Views.Pages.Environments.EditEnvironment;
using System.Windows.Input;

namespace Envz.UI.Views.UserControls.EnvironmentItem;

public class EnvironmentItemViewModel(INavigationService navigationService, Environment environment) : ViewModelBase
{
    public ICommand EditEnvironmentCommand { get; } = new RelayCommand(_ => navigationService.NavigateTo<EditEnvironmentPageViewModel>());
    public Environment Environment { get; } = environment;
    public int NumberOfApplications => Environment.Applications.Count;
    public string NumberOfApplicationsText => NumberOfApplications switch
    {
        0 => Strings.EnvironmentItem_ApplicationCountZero,
        1 => string.Format(Strings.EnvironmentItem_ApplicationCountOne, NumberOfApplications),
        _ => string.Format(Strings.EnvironmentItem_ApplicationCountMany, NumberOfApplications)
    };
}