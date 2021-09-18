using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTypes.Core.Types
{
    public class CLinkedList<T> : IEnumerable<T> {
        private CLinkedListNode Head;

        public CLinkedList() { }

        public CLinkedList(T[] elements) {
            foreach(T e in elements) {
                AddHead(e);
            }
        }

        public void AddHead(T data) {
            CLinkedListNode newNode = new();
            newNode.Data = data;
            newNode.Next = Head;
            Head = newNode;
        }

        public void AddTail(T data) {
            if (Head == null) {
                Head = new CLinkedListNode();
                Head.Data = data;
                Head.Next = null;
            } else {
                CLinkedListNode newNode = new();
                newNode.Data = data;
                CLinkedListNode current = Head;

                while (current.Next != null) {
                    current = current.Next;
                }

                current.Next = newNode;
            }
        }

        public bool Contains(T data) {
            CLinkedListNode current = Head;

            while (!current.Data.Equals(data)) {
                if (current.Next == null)
                    return false;

                current = current.Next;
            }

            return true;
        }

        // TODO FIX
        public void InsertAt(T data, int i) {
            if (i < 0 || i >= Count()) {
                return;
            }

            CLinkedListNode current = Head;
            while (i != 0) {
                current = current.Next;
                i--;
            }

            CLinkedListNode newNode = new();
            newNode.Data = data;
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public int Count() {
            if (Head == null) {
                return 0;
            }

            int i = 1;
            CLinkedListNode current = Head;
 
            while (current.Next != null) {
                Console.WriteLine($"{current.Data}");
                current = current.Next;
                i++;
            }

            return i;
        }

        public T[] ToArray() {
            CLinkedListNode current = Head;
            int len = Count();

            Console.WriteLine($"{len} test");

            T[] values = new T[len];

            current = Head;

            for(int i = 0; i < len; i++) {
                values[i] = current.Data;
                current = current.Next;
            }

            return values;
        }

        public bool IsEmpty() {
            return Head == null;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (T t in ToArray()) {
                yield return t;
            }
        }

        class CLinkedListNode {
            public CLinkedListNode Next;
            public T Data;
        }
    }
}
