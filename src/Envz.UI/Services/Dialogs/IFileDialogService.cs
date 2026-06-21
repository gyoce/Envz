namespace Envz.UI.Services.Dialogs;

public interface IFileDialogService
{
    string? OpenFile(string title, string filter);
}