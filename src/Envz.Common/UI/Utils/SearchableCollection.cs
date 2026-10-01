using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Envz.Common.UI.Utils;

public class SearchableCollection<TViewModel, TItem> : INotifyPropertyChanged
    where TViewModel : class
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICollectionView Items { get; private set; }
    public IEnumerable<TItem> UnfilteredItems
    {
        get;
        set
        {
            field = value;
            RebuildViewModels();
        }
    } = [];
    public string SearchText
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            OnPropertyChanged();
            Items.Refresh();
        }
    } = string.Empty;

    private readonly Func<TItem, string> _searchSelector;
    private readonly Func<TItem, TViewModel> _viewModelFactory;
    private readonly Dictionary<TViewModel, string> _searchKeys = new(ReferenceEqualityComparer.Instance);

    public SearchableCollection(Expression<Func<TItem, string>> searchSelector, Func<TItem, TViewModel> viewModelFactory)
    {
        _searchSelector = searchSelector.Compile();
        _viewModelFactory = viewModelFactory;
        Items = CreateView([]);
    }

    private ListCollectionView CreateView(List<TViewModel> viewModels)
    {
        return new ListCollectionView(viewModels) { Filter = Matches };
    }

    private void RebuildViewModels()
    {
        _searchKeys.Clear();
        List<TViewModel> viewModels = [];

        foreach (TItem item in UnfilteredItems)
        {
            TViewModel viewModel = _viewModelFactory(item);
            _searchKeys[viewModel] = _searchSelector(item);
            viewModels.Add(viewModel);
        }

        Items = CreateView(viewModels);
        OnPropertyChanged(nameof(Items));
    }

    private bool Matches(object obj)
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return true;

        return _searchKeys.TryGetValue((TViewModel)obj, out string? key) && key.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}