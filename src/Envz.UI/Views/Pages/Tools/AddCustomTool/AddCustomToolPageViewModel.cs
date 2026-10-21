using Envz.Application.Factories;
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
using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace Envz.UI.Views.Pages.Tools.AddCustomTool;

public class AddCustomToolPageViewModel : ValidatablePageViewModel
{
    public override ENavigationCategory Category => ENavigationCategory.Tools;
    public override string Title => Strings.AddCustomTool_Title;
    public override int Level => 2;

    public string? InitialPath { get; set; }

    public ICommand BrowseToolCommand { get; }
    public ICommand BrowseIconCommand { get; }
    public ICommand AddToolCommand { get; }
    public ICommand CancelAddToolCommand { get; }

    public string ToolName { get; set { if (SetProperty(ref field, value)) Validate(); } } = string.Empty;
    public string ToolPath { get; set { if (SetProperty(ref field, value)) Validate(); } } = string.Empty;
    public ImageSource? ToolIcon => _iconExtractor.DecodeFromPngBytes(Icon);
    public byte[]? Icon { get; private set { if (SetProperty(ref field, value)) OnPropertyChanged(nameof(ToolIcon)); } }

    private readonly INavigationService _navigationService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IIconExtractor _iconExtractor;
    private readonly IMediator _mediator;

    private bool _showErrors;
    private readonly ToolFactory _toolFactory;

    public AddCustomToolPageViewModel(INavigationService navigationService, IFileDialogService fileDialogService, IIconExtractor iconExtractor, IMediator mediator, ToolFactory toolFactory)
    {
        _navigationService = navigationService;
        _fileDialogService = fileDialogService;
        _iconExtractor = iconExtractor;
        _mediator = mediator;
        _toolFactory = toolFactory;

        AddToolCommand = new RelayCommand(_ => AddTool());
        CancelAddToolCommand = new RelayCommand(_ => CancelAddTool());
        BrowseIconCommand = new RelayCommand(_ => BrowseIcon());
        BrowseToolCommand = new RelayCommand(_ => BrowseTool());

        Localizer.Instance.PropertyChanged += OnLanguageChanged;
    }

    public override void Dispose()
    {
        Localizer.Instance.PropertyChanged -= OnLanguageChanged;
    }

    public override void OnEnable()
    {
        _showErrors = false;
        ToolPath = string.Empty;
        ToolName = string.Empty;
        Icon = null;

        if (InitialPath is not null)
        {
            ApplyExecutable(InitialPath);
            InitialPath = null;
        }

        ClearErrors();
    }

    private bool Validate()
    {
        ValidationResult<Tool> result = _toolFactory.Create(ToolName, ToolPath, Icon);

        ClearErrors();
        if (_showErrors && !result.IsValid)
        {
            ErrorCode code = result.Error.Code;
            SetError(CodeToProperty[code], Localizer.Instance[$"Validation_{code}"]);
        }

        return result.IsValid;
    }


    private void AddTool()
    {
        _showErrors = true;
        if (!Validate())
            return;

        _mediator.Send(new CreateToolsRequest { Tools = [new ToolToCreate(ToolName.Trim(), ToolPath.Trim(), Icon)] });

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

    private void CancelAddTool()
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
