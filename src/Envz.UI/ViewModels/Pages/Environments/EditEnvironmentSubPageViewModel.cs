namespace Envz.UI.ViewModels.Pages.Environments;

public class EditEnvironmentSubPageViewModel : ViewModelBase
{
    public override string Title => "Edit environment";
    public Environment Environment { get; set; } = null!;

    public EditEnvironmentSubPageViewModel()
    {
    }
}
