using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Envz.UI.Utils;

public class SearchableCollection<TViewModel, TItem>(Expression<Func<TItem, string>> searchSelector, Func<TItem, TViewModel> viewModelFactory) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<TViewModel> Items { get; } = [];
    public IEnumerable<TItem> UnfilteredItems
    {
        get;
        set
        {
            field = value;
            FilterElements();
        }
    } = [];
    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            FilterElements();
        }
    } = string.Empty;

    private readonly Func<TItem, string> _searchSelector = searchSelector.Compile();

    private void FilterElements()
    {
        Items.Clear();

        IEnumerable<TItem> filteredItems = string.IsNullOrWhiteSpace(SearchText)
            ? UnfilteredItems
            : UnfilteredItems.Where(item => _searchSelector(item).Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (TItem item in filteredItems)
            Items.Add(viewModelFactory(item));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}