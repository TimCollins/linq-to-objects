using System;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.TestSupport
{
    public class NonEnumerableList<T> : IList<T>
    {
        private readonly List<T> _backingList;

        public NonEnumerableList(params T[] items) : this((IEnumerable<T>) items)
        {
        }

        public NonEnumerableList(IEnumerable<T> items)
        {
            _backingList = new List<T>(items);
        }

        public T this[int index]
        {
            get { return _backingList[index]; }
            set { _backingList[index] = value; }
        }

        public int Count
        {
            get { return _backingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)_backingList).IsReadOnly; }
        }

        public void Add(T item)
        {
            _backingList.Add(item);
        }

        public void Clear()
        {
            _backingList.Clear();
        }

        public bool Contains(T item)
        {
            return _backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _backingList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        public int IndexOf(T item)
        {
            return _backingList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _backingList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _backingList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _backingList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
