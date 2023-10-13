using System.Collections.Generic;
using System.Collections.Concurrent;

namespace HttpUtility.AdvancedCollections;

public class NonDuplicateConcurrentQueue<T> : IDisposable
{
    private HashSet<T> _set = new();
    private ConcurrentQueue<T> _que;

    public NonDuplicateConcurrentQueue(ref ConcurrentQueue<T> item)
    {
        _que = item;
    }
    public void AddToQueue(T item)
    {
        if (!_set.Contains(item))
        {
            _que.Enqueue(item);
            _set.Add(item);
        }
    }
    public bool TryAddToQueue(T item)
    {
        if (!_set.Contains(item))
        {
            _que.Enqueue(item);
            _set.Add(item);
            return true;
        }
        return false;
    }

    //Only use this method if you want the class to keep track of unique items in the set; otherwise, do not use this method
    public bool TryRemoveFromQueue(out T item)
    {
        if (_que.TryDequeue(out var item2))
        {
            item = item2;
            _set.Remove(item2);
            return true;
        }

        item = item2;
        return false;
    }
    public void Dispose()
    { }
}