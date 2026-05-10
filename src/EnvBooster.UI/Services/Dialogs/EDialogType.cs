using EnvBooster.UI.ViewModels.Dialogs;
using EnvBooster.UI.Views.Dialogs;

namespace EnvBooster.UI.Services.Dialogs;

public enum EDialogType
{
    SelectApplication
}

public static class DialogTypeExtension
{
    extension(EDialogType dialogType)
    {
        public Type ToDialogViewModelBaseType()
        {
            return dialogType switch
            {
                EDialogType.SelectApplication => typeof(SelectApplicationDialogViewModel),

                _ => throw new ArgumentOutOfRangeException(nameof(dialogType), dialogType, null)
            };
        }

        public Type ToDialogWindowType()
        {
            return dialogType switch
            {
                EDialogType.SelectApplication => typeof(SelectApplicationDialog),

                _ => throw new ArgumentOutOfRangeException(nameof(dialogType), dialogType, null)
            };
        }
    }
}