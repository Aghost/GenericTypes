using System;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

namespace GenericTypes.Core.Types
{
    public class GenericListBase<T> : IEnumerable<T> {
        private T[] _Data;
        private int _Capacity;
        private int _Size = 0;

        public T[] Data { get => _Data; }
        public int Capacity { get => _Capacity; }
        public int Size { get => _Size; }

        private bool IsEmpty { get => _Size == 0; }

        public GenericListBase(T[] data) {
            _Data = data;
            _Capacity = data.Length;
            _Size = data.Length;
        }

        public GenericListBase(int initCapacity = 4) {
            _Capacity = initCapacity < 1 ? 0 : initCapacity;
            _Data = new T[initCapacity];
        }

        public GenericListBase(GenericListBase<T> rhs) {
            _Data = rhs.ToArray();
            _Capacity = rhs._Capacity;
            _Size = rhs._Size;
        }

        public void Add(T element) {
            if (_Size == _Capacity) {
                Resize();
            }

            _Data[_Size] = element;
            _Size++;
        }

        public void Add(T[] elements) {
            Resize(_Size + elements.Length); // check for offby1

            foreach(T e in elements) {
                _Data[_Size] = e;
                _Size++;
            }
        }

        public bool Contains(T element) {
            if (IsEmpty) {
                return false;
            }

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
            foreach(T t in _Data) {
                yield return t;
            }
        }

        public override int GetHashCode() => (_Data).GetHashCode(); //HashCode.Combine(_Data);

        public override bool Equals(Object obj) {
            if (this.GetType() != obj.GetType())
                return false;

            return ValueEquals((GenericListBase<T>)obj);
        }

        public bool ValueEquals(GenericListBase<T> rhs) {
            if (this._Size != rhs.Size)
                return false;

            for (int i = 0; i < _Size; i++) {
                if (this._Data[i].GetHashCode() != rhs.Data[i].GetHashCode())
                    return false;
            }

            return true;
        }

        public static bool operator ==(GenericListBase<T> lhs, Object rhs) => lhs.Equals(rhs);
        public static bool operator !=(GenericListBase<T> lhs, Object rhs) => !(lhs == rhs);

        public static GenericListBase<T> operator +(GenericListBase<T> lhs, GenericListBase<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericListBase<T> newList = new(lhs.ToArray());
            newList.Add(rhs.ToArray());

            return newList;
        }

        public static GenericListBase<T> operator -(GenericListBase<T> lhs, GenericListBase<T> rhs) {
            if (lhs.Size == 0) { return rhs; }
            if (rhs.Size == 0) { return lhs; }

            GenericListBase<T> newlist = new();

            foreach (T item in lhs) {
                //if (!ReferenceEquals(item, null) && !newlist.Contains(item) && !rhs.Contains(item)) {
                if (!ReferenceEquals(item, null) && !rhs.Contains(item)) {
                    newlist.Add(item);
                }
            }

            return newlist;
        }

        //public static GenericListBase<T> operator /(GenericList<T> lhs, GenericList<T> rhs) { }

        public static bool operator <(GenericListBase<T> lhs, GenericListBase<T> rhs) {
            var def = default(T);
            // is (lhs) a subset of (rhs) ?
            foreach (T item in lhs) {
                //if (ReferenceEquals(item, null)) { continue; }
                //T tmp = item ?? item;
                if (!ReferenceEquals(item, def) && !rhs.Contains(item)) 
                    return true;
            }

            return false;
        }

        public static bool operator >(GenericListBase<T> lhs, GenericListBase<T> rhs) => !(lhs < rhs);
            /*
        public static bool operator >(GenericListBase<T> lhs, GenericListBase<T> rhs) {
            var def = default(T);
            // is (lhs) a superset of (rhs) ?
            foreach(T item in rhs) {
                if (!ReferenceEquals(item, def) && !lhs.Contains(item))
                    return true;
            }

            return false;
        }
        */
    }
}
