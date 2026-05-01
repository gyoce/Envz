namespace EnvBooster.UI.Utils;

public static class Messenger
{
    public static event Action? EnvironmentCreated;

    public static void NotifyEnvironmentCreated() => EnvironmentCreated?.Invoke();
}