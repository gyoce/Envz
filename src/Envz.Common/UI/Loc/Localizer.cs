using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace Envz.Common.UI.Loc;

public class Localizer : INotifyPropertyChanged
{
    public static Localizer Instance { get; } = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    public string this[string key] => Strings.ResourceManager.GetString(key, Strings.Culture) ?? $"!{key}!";

    public void SetLanguage(LanguageOption language)
    {
        Strings.Culture = language.Culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Binding.IndexerName));
    }
}

public class TranslateExtension(string key) : MarkupExtension
{
    public override object ProvideValue(IServiceProvider sp) =>
        new Binding($"[{key}]") { Source = Localizer.Instance, Mode = BindingMode.OneWay }.ProvideValue(sp);
}