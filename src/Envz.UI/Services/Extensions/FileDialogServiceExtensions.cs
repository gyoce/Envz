using Envz.Common.Services.Dialogs;
using Envz.Common.UI.Loc;

namespace Envz.UI.Services.Extensions;

public static class FileDialogServiceExtensions
{
    extension(IFileDialogService fileDialogService)
    {
        public string? BrowseApplication()
        {
            return fileDialogService.OpenFile(Strings.FileDialog_BrowseApplicationTitle, "Executable files (*.exe)|*.exe|All files (*.*)|*.*");
        }

        public string? BrowseIcon()
        {
            return fileDialogService.OpenFile(Strings.FileDialog_BrowseIconTitle, "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*");
        }
    }
}