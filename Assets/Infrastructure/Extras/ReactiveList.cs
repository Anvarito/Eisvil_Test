using System;
using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Extras
{
    public class ReactiveList<T> : IEnumerable<T>
    {
        private readonly List<T> _list = new List<T>();

        public event Action<int> OnCountChanged;
        public event Action<T> OnElementRemove;

        public int Count => _list.Count;

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public void Add(T item)
        {
            _list.Add(item);
            OnCountChanged?.Invoke(_list.Count);
        }

        public bool Remove(T item)
        {
            bool removed = _list.Remove(item);
            if (removed)
            {
                OnElementRemove?.Invoke(item);
            }
            return removed;
        }

        public void Clear()
        {
            _list.Clear();
            OnCountChanged?.Invoke(_list.Count);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            OnCountChanged?.Invoke(_list.Count);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            OnCountChanged?.Invoke(_list.Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}