using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject 
{
	[SerializeField] private List<T> _items = new List<T>();

	public bool Add(T p_item) 
	{
        if (!_items.Contains(p_item))
        {
            _items.Add(p_item);
            return true;
        }

        return false;
	}

	public bool Remove(T p_item) 
	{
		return _items.Remove(p_item);
	}

	public int Count() 
	{
		return _items.Count;
	}
}
