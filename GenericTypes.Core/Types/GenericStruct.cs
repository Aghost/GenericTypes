using System;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

namespace GenericTypes.Core.Types
{
    public class GenericStruct<T> : IEnumerable<T> {
        private T[] _Data;
        private int _Capacity;
        private int _Size = 0;

        public T[] Data     { get => _Data; }
        public int Capacity { get => _Capacity; }
        public int Size     { get => _Size; }

        public GenericStruct(T[] data) {
            _Data = data;
            _Capacity = data.Length;
            _Size = data.Length;
        }

        public GenericStruct(int initCapacity = 4) {
            _Capacity = initCapacity < 1 ? 0 : initCapacity;
            _Data = new T[initCapacity];
        }

        public GenericStruct(GenericStruct<T> rhs) {
            _Data = rhs.ToArray();
            _Capacity = rhs._Capacity;
            _Size = rhs._Size;
        }

        public void Add(T element) {
            if (_Size == _Capacity)
                Resize();

            _Data[_Size] = element;
            _Size++;
        }

        public void Add(T[] elements) {
            Resize(_Size + elements.Length);

            foreach(T e in elements) {
                _Data[_Size] = e;
                _Size++;
            }
        }

        public bool Contains(T element) {
            for (int i = 0; i < _Size; i++) {
                if (_Data[i].Equals(element)) {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(T element, out int position) {
            position = 0;

            for(int i = 0; i < _Size; i++) {
                if (Data[i].Equals(element)) {
                    position = i;
                    return true;
                }
            }

            return false;
        }

        private void Resize() {
            T[] resized = new T[_Capacity * 2];

            for (int i = 0; i < _Size; i++) {
                resized[i] = _Data[i];
            }

            _Data = resized;
            _Capacity *= 2;
        }

        public void Resize(int newSize) {
            T[] resized = new T[newSize];

            int i = 0;
            for (; i < newSize && i < _Size; i++) {
                resized[i] = _Data[i];
            }

            _Size = i;
            _Data = resized;
            _Capacity = newSize;
        }

        public T[] ToArray() {
            T[] tmp = new T[_Capacity];
            Array.Copy(_Data, tmp, _Capacity);

            return tmp;
        }

        public void Clear() {
            _Capacity = 4;
            _Data = new T[Capacity];
            _Size = 0;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < _Size; i++ ) {
                yield return _Data[i];
            }
        }

        public override int GetHashCode() => (_Data).GetHashCode(); //HashCode.Combine(_Data);

        public override bool Equals(Object obj) {
            if (this.GetType() != obj.GetType())
                return false;

            return ValueEquals((GenericStruct<T>)obj);
        }

        public bool ValueEquals(GenericStruct<T> rhs) {
            if (this._Size != rhs.Size)
                return false;

            for (int i = 0; i < _Size; i++) {
                if (this._Data[i].GetHashCode() != rhs.Data[i].GetHashCode())
                    return false;
            }

            return true;
        }

        public static bool operator ==(GenericStruct<T> lhs, Object rhs) => lhs.Equals(rhs);

        public static bool operator !=(GenericStruct<T> lhs, Object rhs) => !(lhs == rhs);

        // UNION
        // set of all objects in A and B
        public static GenericStruct<T> operator +(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericStruct<T> newList = new(lhs.ToArray());
            newList.Add(rhs.ToArray());

            return newList;
        }

        // COMPLEMENT / SET DIFFERENCE
        // set of all objects that are not members of A
        public static GenericStruct<T> operator -(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericStruct<T> newlist = new();

            foreach (T item in lhs) {
                // uncomment to only add unique items!
                //if (!newlist.Contains(item) && !rhs.Contains(item)) {
                if (!rhs.Contains(item))
                    newlist.Add(item);
            }

            return newlist;
        }

        // INTERSECTION
        // the set of all objects that are members of both A and B
        public static GenericStruct<T> operator /(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            GenericStruct<T> newlist = new();

            foreach (T item in rhs) {
                if (lhs.Contains(item))
                    newlist.Add(item);
            }

            return newlist;
        }

        // IS LHS SUBSET OF RHS
        public static bool operator <(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            if (lhs.Size > rhs.Size)
                return false;

            foreach (T item in lhs) {
                if (!rhs.Contains(item))
                    return false;
            }

            return true;
        }

        // IS LHS SUPERSET OF RHS
        public static bool operator >(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            if (rhs.Size > lhs.Size)
                return false;

            foreach (T item in rhs) {
                if (!lhs.Contains(item))
                    return false;
            }

            return true;
        }

        // CARTESIAN PRODUCT
        public static GenericStruct<GenericStruct<T>> operator *(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            GenericStruct<GenericStruct<T>> result = new();

            foreach(T lht in lhs) {
                foreach(T rht in rhs) {
                    GenericStruct<T> tmpresult = new();
                    tmpresult.Add(rht);
                    tmpresult.Add(lht);
                    result.Add(tmpresult);
                }
            }

            return result;
        }


        // POWERSET
        public GenericStruct<GenericStruct<T>> PowerSet() {
            GenericStruct<GenericStruct<T>> result = new();

            for (int i = 0; i < (1 << _Size); i++) {
                GenericStruct<T> sublist = new();

                for (int j = 0; j < _Size; j++) {
                    if ((i & (1 << j)) != 0) {
                        sublist.Add(_Data[j]);
                    }
                }

                result.Add(sublist);
            }

            return result;
        }

        // ZIP ???
        public static GenericStruct<GenericStruct<T>> operator ^(GenericStruct<T> lhs, GenericStruct<T> rhs) {
            GenericStruct<GenericStruct<T>> result = new();

            foreach(T lht in lhs) {
                GenericStruct<T> tmpresult = new();
                tmpresult.Add(lht);

                foreach(T rht in rhs) {
                    if (!tmpresult.Contains(rht))
                        tmpresult.Add(rht);
                }

                if (tmpresult.Size > 0)
                    result.Add(tmpresult);
            }

            return result;
        }


    }
}
