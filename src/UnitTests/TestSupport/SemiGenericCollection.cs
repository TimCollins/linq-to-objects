using System;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.TestSupport
{
    public class SemiGenericCollection : ICollection, IEnumerable<int>
    {
        private readonly List<int> _list;

        public SemiGenericCollection()
        {
            _list = new List<int>();
        }

        public SemiGenericCollection(IEnumerable<int> items)
        {
            _list = new List<int>(items);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_list).CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
