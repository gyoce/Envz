namespace EnvBooster.UI.ViewModels.UserControls;

public class EnvironmentViewModel : ViewModelBase
{
    public Environment Environment { get; }

    public EnvironmentViewModel(Environment environment)
    {
        Environment = environment;
    }
}