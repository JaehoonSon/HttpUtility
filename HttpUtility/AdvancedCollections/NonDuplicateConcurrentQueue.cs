using System.Collections.Generic;
using System.Collections.Concurrent;

namespace HttpUtility.AdvancedCollections;

public class NonDuplicateConcurrentQueue<T> : IDisposable
{
    private HashSet<T> set = new();
    private ConcurrentQueue<T> que;

    public NonDuplicateConcurrentQueue(ref ConcurrentQueue<T> item)
    {
        que = item;
    }
    public void AddToQueue(T item)
    {
        if (!set.Contains(item))
        {
            que.Enqueue(item);
            set.Add(item);
        }
    }
    public bool TryAddToQueue(T item)
    {
        if (!set.Contains(item))
        {
            que.Enqueue(item);
            set.Add(item);
            return true;
        }
        return false;
    }

    //Only use this method if you want the class to keep track of unique items in the set; otherwise, do not use this method
    public bool TryRemoveFromQueue(out T item)
    {
        if (que.TryDequeue(out var item2))
        {
            item = item2;
            set.Remove(item2);
            return true;
        }

        item = item2;
        return false;
    }
    public void Dispose()
    { }
}