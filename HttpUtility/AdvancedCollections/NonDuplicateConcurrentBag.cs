using System;
using System.Collections.Concurrent;
namespace HttpUtility.AdvancedCollections;

public class NonDuplicateConcurrentBag<T>:IDisposable
{
	private HashSet<T> _set;
	private ConcurrentBag<T> _bag;
	public NonDuplicateConcurrentBag(ref ConcurrentBag<T> item)
	{
		_set = new();
		_bag = item;
	}

	public bool TryAdd(T item)
	{
		if (!_set.Contains(item))
		{
			_set.Add(item);
			_set.Add(item);
			return true;
		}
		return false;
	}

	public bool TryTake(out T item)
	{
		if (_bag.TryTake(out var item2))
		{
			item = item2;
			_set.Remove(item2);
			return true;
		}
		item = item2;
		return false;
	}

	public void Dispose() { }
}

