using System;
using System.Collections.Generic;

namespace dynamo_db_load_testing.Utilities;

public class CircularList<T>
{
    private readonly List<T> _items = new List<T>();
    private int _currentIndex = -1;
    private readonly object _lock = new object();

    public CircularList(IEnumerable<T> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        lock (_lock)
        {
            _items.AddRange(items);
        }
    }

    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _items.Count;
            }
        }
    }

    public void Add(T item)
    {
        lock (_lock)
        {
            _items.Add(item);
        }
    }

    public T GetNext()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            _currentIndex = (_currentIndex + 1) % _items.Count;
            return _items[_currentIndex];
        }
    }

    public T Peek()
    {
        lock (_lock)
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            return _items[(_currentIndex + 1) % _items.Count];
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _items.Clear();
            _currentIndex = -1;
        }
    }
}
