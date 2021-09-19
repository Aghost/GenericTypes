using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTypes.Core.Types
{
    public class CEnumerable<T> : IEnumerable<T>
    {
        private T[] Elements;

        public CEnumerable(T[] elements) {
            this.Elements = elements;
        }

        public IEnumerator<T> GetEnumerator() {
            return new CEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        class CEnumerator : IEnumerator<T>
        {
            private CEnumerable<T> Collection;
            private T _Current;
            private int Index;

            public T Current {
                get { return _Current; }
            }

            object IEnumerator.Current {
                get {
                    return Current;
                }
            }

            public CEnumerator(CEnumerable<T> collection) {
                Index = -1;
                Collection = collection;
            }

            public bool MoveNext() {
                if (++Index >= Collection.Elements.Length) {
                    return false;
                } else {
                    _Current = Collection.Elements[Index];
                    return true;
                }
            }

            public void Reset() {
                _Current = default(T);
                Index = 0;
            }

            public void Dispose() {
                try {
                    _Current = default(T);
                    Index = Collection.Elements.Length;
                } finally { }
            }
        }
    }
}
