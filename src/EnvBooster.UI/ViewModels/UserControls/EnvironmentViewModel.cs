namespace EnvBooster.UI.ViewModels.UserControls;

public class EnvironmentViewModel(Environment environment) : ViewModelBase
{
    public Environment Environment { get; private set; } = environment;
}