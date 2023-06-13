using System;
using System.Collections.Generic;
using System.Collections;

namespace Queue
{
    public class Queue<T> : IEnumerable<T>
    {
        int count;
        Node<T> head;
        Node<T> tail;
        class Node<T>
        {
            public T data;
            public Node<T> next;

            public Node(T data = default)
            {
                this.data = data;
                this.next = null;
            }
        }

        public Queue()
        {
            count = 0;
            head = null;
            tail = null;
        }

        public void Enqueue(T value)
        {
            if (count == 0)
            {
                head = new Node<T>(value);
                tail = new Node<T>(value);
                count = 1;
            }
            else
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
                count++;
            }
        }

        public T Dequeue()
        {
            Node<T> n = head;
            head = head.next;
            n.next = null;
            count--;
            return n.data;
        }

        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        public int Count
        {
            get { return count; }
        }
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }

            Node<T> n = head;
            return n.data;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public T[] ToArray()
        {
            T[] arr = new T[count];
            for (int i = 0; i < count; i++)
            {
                Node<T> temp = head;
                temp = temp.next;
                temp.next = null;
                arr[i] = temp.data;
            }

            return arr;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> curr = head;
            while(curr != null)
            {
                yield return curr.data;
                curr = curr.next;
            }
        }

    }

    

}
