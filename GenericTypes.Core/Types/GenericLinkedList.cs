using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTypes.Core.Types
{
    public class GenericLinkedList<T> : IEnumerable<T> {
        private GenericLinkedListNode head;

        public GenericLinkedList() {
        }

        public GenericLinkedList(T[] elements) {
            foreach(T e in elements) {
                AddHead(e);
            }
        }

        public void AddHead(T data) {
            GenericLinkedListNode newNode = new();
            newNode.data = data;
            newNode.next = head;
            head = newNode;
        }

        public void AddTail(T data) {
            if (head == null) {
                head = new GenericLinkedListNode();
                head.data = data;
                head.next = null;
            } else {
                GenericLinkedListNode newNode = new();
                newNode.data = data;
                GenericLinkedListNode current = head;

                while (current.next != null) {
                    current = current.next;
                }

                current.next = newNode;
            }
        }

        // TODO FIX
        public void InsertAt(T data, int i) {
            if (i >= Count() || i < 0) {
                return;
            }

            GenericLinkedListNode current = head;

            while(i-- != 0) {
                if (i == 0) {
                    GenericLinkedListNode newNode = new();
                    newNode.data = data;
                    newNode.next = head;
                    head.next = newNode;

                    return;
                }

                current = current.next;
            }
        }

        public int Count() {
            GenericLinkedListNode current = head;

            int i = 0;
            while (current != null) {
                current = current.next;
                i++;
            }

            return i;
        }

        public bool IsEmpty() {
            return head == null;
        }

        public T[] ToArray() {
            GenericLinkedListNode current = head;
            int len = 1;

            while (current.next != null) {
                len++;
                current = current.next;
            }

            T[] values = new T[len];

            current = head;
            for(int i = 0; i < len; i++) {
                values[i] = current.data;
                current = current.next;
            }

            return values;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (T t in ToArray()) {
                yield return t;
            }
        }

        class GenericLinkedListNode {
            public GenericLinkedListNode next;
            public T data;
        }
    }
}
