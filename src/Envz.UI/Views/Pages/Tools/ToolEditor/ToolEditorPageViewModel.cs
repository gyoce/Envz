using Envz.Application.Mediator;
using Envz.Application.Requests.Tools;
using Envz.Common.Services;
using Envz.Common.Services.Dialogs;
using Envz.Common.Services.Navigation;
using Envz.Common.UI.Loc;
using Envz.Common.Utils;
using Envz.Common.ViewModels;
using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Validation;
using Envz.UI.Services.Extensions;
using Envz.UI.Views.Pages.Tools.HomeTools;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using Envz.Application.Validators;

namespace Envz.UI.Views.Pages.Tools.ToolEditor;

public class ToolEditorPageViewModel : ValidatablePageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Tools;
    public override string Title => IsEditMode ? Strings.ToolEditorEdit_Title : Strings.ToolEditorAdd_Title;
    public override int Level => 2;
    
    public ICommand BrowseToolCommand { get; }
    public ICommand BrowseIconCommand { get; }
    public ICommand SaveToolCommand { get; }
    public ICommand CancelSaveToolCommand { get; }

    public string ToolName { get; set { if (SetProperty(ref field, value)) Validate(); } } = string.Empty;
    public string ToolPath { get; set { if (SetProperty(ref field, value)) Validate(); } } = string.Empty;
    public ImageSource? ToolIcon => _iconExtractor.DecodeFromPngBytes(Icon);
    public byte[]? Icon { get; private set { if (SetProperty(ref field, value)) OnPropertyChanged(nameof(ToolIcon)); } }
    public string AcceptActionText => IsEditMode ? Strings.Global_Save : Strings.Global_Add;

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IIconExtractor _iconExtractor;
    private readonly IMediator _mediator;

    [MemberNotNullWhen(true, nameof(_originalTool))]
    private bool IsEditMode => _originalTool is not null;

    private bool _showErrors;
    private readonly ToolValidator _toolValidator;
    private Tool? _originalTool;    

    public ToolEditorPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor, IMediator mediator, ToolValidator toolValidator)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _iconExtractor = iconExtractor;
        _mediator = mediator;
        _toolValidator = toolValidator;

        SaveToolCommand = new RelayCommand(_ => SaveTool());
        CancelSaveToolCommand = new RelayCommand(_ => CancelSaveTool());
        BrowseIconCommand = new RelayCommand(_ => BrowseIcon());
        BrowseToolCommand = new RelayCommand(_ => BrowseTool());

        Localizer.Instance.PropertyChanged += OnLanguageChanged;
    }

    public override void OnRelease()
    {
        Localizer.Instance.PropertyChanged -= OnLanguageChanged;
    }

    public void UseExecutable(string path)
    {
        ApplyExecutable(path);
    }

    public void UseTool(Tool tool)
    {
        _originalTool = tool;
        ToolPath = tool.Path;
        ToolName = tool.Name;
        Icon = tool.Icon;
    }

    private bool Validate()
    {
        ValidationResult<Tool> result = IsEditMode 
            ? _toolValidator.ValidateEdition(_originalTool, ToolName, ToolPath, Icon)
            : _toolValidator.ValidateCreation(ToolName, ToolPath, Icon);

        ClearErrors();
        if (_showErrors && !result.IsValid)
        {
            ErrorCode code = result.Error.Code;
            SetError(CodeToProperty[code], Localizer.Instance[$"Validation_{code}"]);
        }

        return result.IsValid;
    }

    private void SaveTool()
    {
        _showErrors = true;
        if (!Validate())
            return;

        if (IsEditMode)
            _mediator.Send(new EditToolRequest { ToolName = _originalTool.Name, NewName = ToolName, NewPath = ToolPath, NewIcon = Icon });
        else
            _mediator.Send(new CreateToolsRequest { Tools = [new ToolToCreate(ToolName, ToolPath, Icon)] });

        _navigationService.NavigateTo<HomeToolsPageViewModel>();
    }

    private void BrowseIcon()
    {
        string? path = _fileDialogService.BrowseIcon();
        if (path is not null)
            Icon = File.ReadAllBytes(path);
    }

    private void BrowseTool()
    {
        string? path = _fileDialogService.BrowseTool();
        if (path is not null)
            ApplyExecutable(path);
    }

    private void ApplyExecutable(string path)
    {
        ToolPath = path;
        ToolName = Path.GetFileNameWithoutExtension(path);
        Icon = _iconExtractor.ExtractPngBytes(path);
    }

    private void CancelSaveTool()
    {
        _navigationService.NavigateTo<HomeToolsPageViewModel>();
    }

    private void OnLanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate();
    }

    private static readonly Dictionary<ErrorCode, string> CodeToProperty = new()
    {
        [ErrorCode.ToolInvalidName] = nameof(ToolName),
        [ErrorCode.ToolInvalidPath] = nameof(ToolPath),
        [ErrorCode.CreateToolAlreadyExists] = nameof(ToolName),
    };
}
