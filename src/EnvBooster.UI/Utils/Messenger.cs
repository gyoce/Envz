namespace EnvBooster.UI.Utils;

public static class Messenger
{
    public static event Action? EnvironmentCreated;
    public static event Action? NavigationToCreateEnvironmentRequested;
    public static event Action? NavigationToHomeEnvironmentsRequested;

    public static void NotifyEnvironmentCreated() => EnvironmentCreated?.Invoke();
    public static void NotifyNavigationToCreateEnvironment() => NavigationToCreateEnvironmentRequested?.Invoke();
    public static void NotifyNavigationToHomeEnvironments() => NavigationToHomeEnvironmentsRequested?.Invoke();
}