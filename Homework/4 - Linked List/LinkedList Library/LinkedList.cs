using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListLibrary
{
    public class Node<T>
    {
        public T data;
        public Node<T> next;
        public Node<T> prev;

        public Node(T data)
        {
            this.data = data;
            next = null;
            prev = null;
        }

        public void Print()
        {
            Console.Write($"|{data}|->");
            if (next != null)
            {
                next.Print();
            }
        }

        public void AddToEnd(T data)
        {
            Node<T> foo = new Node<T>(data);
            if (next == null)
            {
                next = foo;
                foo.prev = this.prev;
            }
            else
            {
                next.AddToEnd(data);
            }
        }
    }
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {

        private Node<T> tail;
        private Node<T> head;
        private int count;

        public LinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void AddToEnd(T value)//Enqueue
        {
            if (count == 0)
            {
                head = new Node<T>(value);
                tail = head;
                count = 1;
            }
            else
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
                count++;
            }
        }

        public void Push(T data)//Push
        {
            if (head == null)
            {
                head = new Node<T>(data);
            }
            else
            {
                Node<T> temp = new Node<T>(data);
                temp.next = head;
                head = temp;

            }
            count++;
        }

        public void Insert(int idx, T data)
        {
            Node<T> n = head;
            Node<T> j = head.next; j.prev = n;
            for (int i = 0; i < idx - 1; i++)
            {
                n = j;
                n.prev = j.prev;
                j = n.next;
                j.prev = n;
            }
            Node<T> newNode = new Node<T>(data);
            n.next = newNode; newNode.prev = n;
            newNode.next = j; j.prev = newNode;
            count++;
        }

        public T Pop()//Pop
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

        public int IndexOf(T value)
        {
            int idx = 0;
            Node<T> curr = this.head;
            while (curr != null)
            {
                if (curr.data.Equals(value))
                {
                    return idx;
                }
                curr = curr.next;
                idx++;
            }
            return -1;
        }

        public Node<T> WalkToNthNode(int idx)
        {
            Node<T> n = head; n.prev = null;
            Node<T> j = head.next; j.prev = n;
            for (int i = 0; i < idx - 1; i++)
            {
                n = j;
                n.prev = j.prev;
                j = n.next;
                j.prev = n;
            }
            return n;
        }

        public void RemoveAt(int idx)
        {
            Node<T> n = head;
            Node<T> j = head.next; j.prev = n;
            for (int i = 0; i < idx - 1; i++)
            {
                n = j;
                n.prev = j.prev;
                j = n.next;
                j.prev = n;
            }
            n.next = j.next; j = null;
            count--;
        }

        public T this[int idx]
        {
            get
            {
                Node<T> node = WalkToNthNode(idx);
                return node.data;
            }
            set
            {
                Node<T> node = WalkToNthNode(idx);
                node.data = value;
            }
        }

        public int Count
        {
            get { return count; }
        }
        public void SlowSort()
        {//Selection Sort


            for (Node<T> start = head; start != null; start = start.next)
            {
                Node<T> min = start;

                for (Node<T> eval = start.next; eval != null; eval = eval.next)
                {
                    if (eval.data.CompareTo(min.data) < 0)
                    {
                        min = eval;
                    }
                }

                T temp = min.data;
                min.data = start.data;
                start.data = temp;
            }
        }


        public void QuickSort(int[] arr, int A, int B)
        {//Merge Sort

            for (int i = A; i <= B; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();

            int count = B - A + 1;
            if (count <= 1)
            {
                return;
            }

            int M = A + (B - A) / 2;

            QuickSort(arr, A, M);
            QuickSort(arr, M + 1, B);

            MergeLists(arr, A, M, B);

        }

        private void MergeLists(int[] arr, int A, int M, int B)
        {
            int[] tArr = new int[B - A + 1];
            int L = A;
            int R = M + 1;
            int T = 0;

            while (L <= M && R <= B)
            {
                if (arr[L] < arr[R])
                {
                    tArr[T++] = arr[L++];
                }
                else { tArr[T++] = arr[R++]; }
            }

            for (int i = 0; i <= T; i++)
            {
                Console.Write(tArr[i]);
            }
            Console.WriteLine();
        }
        public void Sort(int[] arr)
        {
            int temp;
            for(int i = 0; i < arr.Length; i++)
            {
                for( int j = i+1; j< arr.Length; j++)
                {
                    if(arr[i] > arr[j]) { temp = arr[i]; arr[i] = arr[j]; arr[j] = arr[i]; arr[j] = temp; }
                }
            }
        }
        public bool ArrayIsSorted(int[] arr)
        {
            int i, j;
            i = 0;j = 1;
            if(arr.Length < 1) { return false; }
            while(arr[i].CompareTo(arr[j]) == -1)
            {
                i++;j++;
            }
            if(i != arr.Length - 1) { return false; }
            return true;
        }
        public T Now()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            Node<T> n = head;
            return n.data;
        }

        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            Node<T> n = head.next;
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

        public bool Contains(T x)
        {
            if (this.IndexOf(x) == -1)
            {
                return false;
            }
            return true;
        }

        public void Print()
        {
            Node<T> n = head;
            while (n != null)
            {
                Console.Write($"|{n.data}|->");
                n = n.next;
            }
            Console.WriteLine($"|{n.data}|");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> curr = head;
            while (curr != null)
            {
                yield return curr.data;
                curr = curr.next;
            }
        }




    }

}
