using System;
using System.Collections;
using System.Collections.Generic;

using static System.Console;

namespace GenericTypes.Core.Types
{
    public class SetList<T> : IEnumerable<T> {
        public T[] Data     { get; private set;}
        public int Size     { get; private set;}

        public SetList(T[] data) {
            Data = data;
            Size = data.Length;
        }

        public SetList(int initCapacity = 4) {
            Data = new T[initCapacity];
        }

        public SetList(SetList<T> set) {
            Data = set.ToArray();
            Size = set.Size;
        }

        public void Add(T element) {
            if (Size == Data.Length)
                Resize();

            Data[Size] = element;
            Size++;
        }

        public void Add(T[] elements) {
            Resize(Size + elements.Length);

            foreach(T e in elements) {
                Data[Size] = e;
                Size++;
            }
        }

        public bool Contains(T element) {
            for (int i = 0; i < Size; i++) {
                if (Data[i].Equals(element)) {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(T element, out int position) {
            position = 0;

            for (int i = 0; i < Size; i++) {
                if (Data[i].Equals(element)) {
                    position = i;
                    return true;
                }
            }

            return false;
        }

        public void Delete(T element) {
            if (Contains(element, out int position)) {
                DeleteAt(position);
            }
        }

        public void DeleteAt(int position) {
            for (int i = position; i < Size - 1; i++) {
                Data[i] = Data[i + 1];
            }

            Data[Size - 1] = default(T);
            Size--;
        }

        private void Resize() {
            T[] resized = new T[Data.Length * 2];

            for (int i = 0; i < Data.Length; i++) {
                resized[i] = Data[i];
            }

            Data = resized;
        }

        public void Resize(int newSize) {
            T[] resized = new T[newSize];

            int i = 0;
            for (; i < newSize && i < Size; i++) {
                resized[i] = Data[i];
            }

            Size = i;
            Data = resized;
        }

        public T[] ToArray() {
            T[] tmp = new T[Data.Length];
            Array.Copy(Data, tmp, Data.Length);

            return tmp;
        }

        public void Clear() {
            Data = new T[Data.Length];
            Size = 0;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < Size; i++ ) {
                yield return Data[i];
            }
        }

        public override int GetHashCode() => (Data).GetHashCode(); //HashCode.Combine(Data);

        public override bool Equals(Object obj) {
            if (this.GetType() != obj.GetType())
                return false;

            return ValueEquals((SetList<T>)obj);
        }

        public bool ValueEquals(SetList<T> set) {
            if (this.Size != set.Size)
                return false;

            for (int i = 0; i < Size; i++) {
                if (this.Data[i].GetHashCode() != set.Data[i].GetHashCode())
                    return false;
            }

            return true;
        }

        public static bool operator ==(SetList<T> setA, Object setB) => setA.Equals(setB);
        public static bool operator !=(SetList<T> setA, Object setB) => !(setA == setB);

        // UNION: set of all objects in A and B
        public static SetList<T> operator +(SetList<T> setA, SetList<T> setB) {
            if (setA.Size == 0) { return setB; }
            if (setB.Size == 0) { return setA; }

            SetList<T> newList = new(setA.ToArray());
            newList.Add(setB.ToArray());

            return newList;
        }

        // COMPLEMENT / SET DIFFERENCE: set of all objects that are not members of A
        public static SetList<T> operator -(SetList<T> setA, SetList<T> setB) {
            if (setA.Size == 0) { return setB; }
            if (setB.Size == 0) { return setA; }

            SetList<T> newlist = new();

            foreach (T item in setA) {
                // uncomment to only add unique items!
                //if (!newlist.Contains(item) && !setB.Contains(item)) {
                if (!setB.Contains(item))
                    newlist.Add(item);
            }

            return newlist;
        }

        // INTERSECTION: the set of all objects that are members of both A and B
        public static SetList<T> operator /(SetList<T> setA, SetList<T> setB) {
            SetList<T> newlist = new();

            if (setB.Size == 0) 
                return newlist;

            foreach (T item in setB) {
                if (setA.Contains(item))
                    newlist.Add(item);
            }

            return newlist;
        }

        // IS LHS SUBSET OF RHS
        public static bool operator <(SetList<T> setA, SetList<T> setB) {
            if (setA.Size > setB.Size)
                return false;

            foreach (T item in setA) {
                if (!setB.Contains(item))
                    return false;
            }

            return true;
        }

        // IS LHS SUPERSET OF RHS
        public static bool operator >(SetList<T> setA, SetList<T> setB) {
            if (setB.Size > setA.Size)
                return false;

            foreach (T item in setB) {
                if (!setA.Contains(item))
                    return false;
            }

            return true;
        }

        // CARTESIAN PRODUCT
        public static SetList<SetList<T>> operator *(SetList<T> setA, SetList<T> setB) {
            SetList<SetList<T>> result = new();

            foreach(T lht in setA) {
                foreach(T rht in setB) {
                    SetList<T> tmpresult = new();
                    tmpresult.Add(rht);
                    tmpresult.Add(lht);
                    result.Add(tmpresult);
                }
            }

            return result;
        }

        // POWERSET
        public SetList<SetList<T>> PowerSet() {
            SetList<SetList<T>> result = new();

            for (int i = 0; i < (1 << Size); i++) {
                SetList<T> sublist = new();

                for (int j = 0; j < Size; j++) {
                    if ((i & (1 << j)) != 0) {
                        sublist.Add(Data[j]);
                    }
                }

                result.Add(sublist);
            }

            return result;
        }

        // ZIP ???
        public static SetList<SetList<T>> operator ^(SetList<T> setA, SetList<T> setB) {
            SetList<SetList<T>> result = new();

            foreach(T lht in setA) {
                SetList<T> tmpresult = new();
                tmpresult.Add(lht);

                foreach(T rht in setB) {
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
