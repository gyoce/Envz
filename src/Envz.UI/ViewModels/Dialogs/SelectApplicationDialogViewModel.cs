using System.Windows.Input;

using Envz.Domain.Entities;
using Envz.Functional.Applications;
using Envz.Functional.Mediator;
using Envz.UI.Services;
using Envz.UI.Utils;
using Envz.UI.ViewModels.UserControls;

namespace Envz.UI.ViewModels.Dialogs;

public class SelectApplicationDialogViewModel : DialogViewModelBase<EnvironmentApplication>
{
    public ICommand SelectApplicationCommand { get; }
    public ICommand CancelCommand { get; }
    public SearchableCollection<ApplicationViewModel, Application> SearchableApplications { get; }
    public ApplicationViewModel? SelectedApplication
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }
    public string Parameter
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;

    public SelectApplicationDialogViewModel(IMediator mediator, ApplicationViewModelFactory applicationViewModelFactory)
    {
        SearchableApplications = new SearchableCollection<ApplicationViewModel, Application>(app => app.Name, applicationViewModelFactory.Create)
        {
            UnfilteredItems = mediator.Send(new GetApplicationsRequest())
        };

        SelectApplicationCommand = new RelayCommand(_ => SelectApplication(), _ => SelectedApplication is not null);
        CancelCommand = new RelayCommand(_ => Close(false));
    }

    private void SelectApplication()
    {
        Close(true, new EnvironmentApplication
        {
            ApplicationId = SelectedApplication!.Application.Id,
            Parameter = Parameter
        });
    }
}
