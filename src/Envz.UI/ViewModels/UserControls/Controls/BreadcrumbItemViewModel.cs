using System.Windows.Input;

namespace Envz.UI.ViewModels.UserControls.Controls;

public class BreadcrumbItemViewModel(string title, bool isFirst, bool isClickable, ICommand? navigateCommand)
{
    public string Title { get; } = title;
    public bool IsFirst { get; } = isFirst;
    public bool IsClickable { get; } = isClickable;
    public ICommand? NavigateCommand { get; } = navigateCommand;
}