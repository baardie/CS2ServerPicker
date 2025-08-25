using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// A BindingList that supports sorting by property, useful for WinForms data grids.
/// </summary>
public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor? _sortProperty;

    // Constructors for various input types
    public SortableBindingList() : base() { }
    public SortableBindingList(IEnumerable<T> collection) : base(new List<T>(collection)) { }
    public SortableBindingList(List<T> list) : base(list) { }

    // Sorting support flags
    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;
    protected override ListSortDirection SortDirectionCore => _sortDirection;

    /// <summary>
    /// Applies sorting to the list based on the given property and direction.
    /// </summary>
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var items = (List<T>)Items;
        var comparer = new PropertyComparer<T>(prop, direction);
        items.Sort(comparer);

        _isSorted = true;
        _sortDirection = direction;
        _sortProperty = prop;

        // Notify UI that the list has changed
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    /// <summary>
    /// Removes any applied sort, restoring original order.
    /// </summary>
    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }

    /// <summary>
    /// Comparer that sorts items by a given property and direction.
    /// </summary>
    private class PropertyComparer<TItem> : IComparer<TItem>
    {
        private readonly PropertyDescriptor _property;
        private readonly ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _property = property;
            _direction = direction;
        }

        public int Compare(TItem? x, TItem? y)
        {
            var xValue = _property.GetValue(x);
            var yValue = _property.GetValue(y);

            int result = Comparer<object>.Default.Compare(xValue, yValue);
            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}
