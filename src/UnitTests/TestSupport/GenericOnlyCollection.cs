using System;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.TestSupport
{
    class GenericOnlyCollection<T> : ICollection<T>
    {
        private List<T> _backingList;

        public GenericOnlyCollection(IEnumerable<T> items)
        {
            _backingList = new List<T>(items);
        }

        public int Count
        {
            get
            {
                return _backingList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                // Cast the List to an ICollection and then check that 
                // object's IsReadOnly property.
                return ((ICollection<T>)_backingList).IsReadOnly;
            }
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
            return _backingList.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return _backingList.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
