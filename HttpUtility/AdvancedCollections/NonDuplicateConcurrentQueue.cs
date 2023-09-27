using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace HttpUtility.AdvancedCollections;

public class NonDuplicateConcurrentQueue<T> : IDisposable
{
    private HashSet<T> set = new();
    public void AddToQueue(ref ConcurrentQueue<T> que, T item)
    {
        if (!set.Contains(item))
        {
            que.Enqueue(item);
            set.Add(item);
        }
    }

    //Only use this method if you want the class to keep track of unique items in the set; otherwise, do not use this method
    public bool RemoveFromQueue(ref ConcurrentQueue<T> que, out T item)
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