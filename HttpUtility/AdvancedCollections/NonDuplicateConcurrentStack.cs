using System;
using System.Collections.Concurrent;

namespace HttpUtility.AdvancedCollections;

public class NonDuplicateConcurrentStack<T>: IDisposable
{
	private HashSet<T> _set = new();
	private ConcurrentStack<T> _stack;
	public NonDuplicateConcurrentStack(ref ConcurrentStack<T> item)
	{
		_stack = item;
	}

	public bool TryPush(T item)
	{
		if (!_set.Contains(item))
		{
			_stack.Push(item);
			_set.Add(item);
			return true;
		}
		return false;
	}

	public bool TryPop(out T item)
	{
		if (_stack.TryPop(out var item2))
		{
			item = item2;
			return true;
		}
		item = item2;
		return false;
	}

	public void Dispose() { }
}

