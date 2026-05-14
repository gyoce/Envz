using Microsoft.Win32;

namespace Envz.UI.Services.Dialogs;

public interface IFileDialogService
{
    string? OpenFile(string title, string filter);
}

public class FileDialogService : IFileDialogService
{
    public string? OpenFile(string title, string filter)
    {
        OpenFileDialog dialog = new()
        {
            Title = title,
            Filter = filter,
            CheckFileExists = true,
            Multiselect = false
        };

        bool? result = dialog.ShowDialog();
        return result == true ? dialog.FileName : null;
    }
}