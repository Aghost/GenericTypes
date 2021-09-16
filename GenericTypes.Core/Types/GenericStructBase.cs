using System;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

namespace GenericTypes.Core.Types
{
    public class GenericStructBase<T> : IEnumerable<T> {
        private T[] _Data;
        private int _Capacity;
        private int _Size = 0;

        public T[] Data { get => _Data; }
        public int Capacity { get => _Capacity; }
        public int Size { get => _Size; }

        private bool IsEmpty { get => _Size == 0; }

        public GenericStructBase(T[] data) {
            _Data = data;
            _Capacity = data.Length;
            _Size = data.Length;
        }

        public GenericStructBase(int initCapacity = 4) {
            _Capacity = initCapacity < 1 ? 0 : initCapacity;
            _Data = new T[initCapacity];
        }

        public GenericStructBase(GenericStructBase<T> rhs) {
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
            for (int i = 0; i < Size; i++) {
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

            return ValueEquals((GenericStructBase<T>)obj);
        }

        public bool ValueEquals(GenericStructBase<T> rhs) {
            if (this._Size != rhs.Size)
                return false;

            for (int i = 0; i < _Size; i++) {
                if (this._Data[i].GetHashCode() != rhs.Data[i].GetHashCode())
                    return false;
            }

            return true;
        }

        public static bool operator ==(GenericStructBase<T> lhs, Object rhs) => lhs.Equals(rhs);

        public static bool operator !=(GenericStructBase<T> lhs, Object rhs) => !(lhs == rhs);

        // UNION
        // set of all objects
        public static GenericStructBase<T> operator +(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericStructBase<T> newList = new(lhs.ToArray());
            newList.Add(rhs.ToArray());

            return newList;
        }

        // COMPLEMENT / SET DIFFERENCE
        // set of all objects that are not members of A
        public static GenericStructBase<T> operator -(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericStructBase<T> newlist = new();

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
        public static GenericStructBase<T> operator /(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            GenericStructBase<T> newlist = new();

            foreach (T item in rhs) {
                if (lhs.Contains(item))
                    newlist.Add(item);
            }

            return newlist;
        }

        // LHS SUBSET OF RHS
        public static bool operator <(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            if (lhs.Size > rhs.Size)
                return false;

            foreach (T item in lhs) {
                if (!rhs.Contains(item))
                    return false;
            }

            return true;
        }

        // LHS SUPERSET OF RHS
        public static bool operator >(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            if (rhs.Size > lhs.Size)
                return false;

            foreach (T item in rhs) {
                if (!lhs.Contains(item))
                    return false;
            }

            return true;
        }

        // CARTESIAN PRODUCT ???
        public static GenericStructBase<GenericStructBase<T>> operator *(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            GenericStructBase<GenericStructBase<T>> result = new();

            foreach(T lht in lhs) {
                foreach(T rht in rhs) {
                    GenericStructBase<T> tmpresult = new();
                    tmpresult.Add(rht);
                    tmpresult.Add(lht);
                    result.Add(tmpresult);
                }
            }

            return result;
        }

        // POWERSET ???
        public static GenericStructBase<GenericStructBase<T>> operator ^(GenericStructBase<T> lhs, GenericStructBase<T> rhs) {
            GenericStructBase<GenericStructBase<T>> result = new();

            foreach(T lht in lhs) {
                if (lhs.Size > 0) {
                    GenericStructBase<T> tmpresult = new();
                    tmpresult.Add(lht);

                    foreach(T rht in rhs) {
                        if (!tmpresult.Contains(rht))
                            tmpresult.Add(rht);
                    }

                    if (tmpresult.Size > 0)
                        result.Add(tmpresult);
                }
            }

            return result;
        }
    }
}
