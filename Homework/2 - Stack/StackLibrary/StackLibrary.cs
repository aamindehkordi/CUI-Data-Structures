using System;
using System.Collections;
using System.Collections.Generic;

namespace StackLibrary
{
    public class Stack<T> : IEnumerable<T>
    {
        private int count;
        private T[] arr;
        public Stack(int capacity = 64)
        {
            arr = new T[capacity];
            count = 0;
        }

        private void Resize(int newCapacity)
        {
            if (newCapacity < arr.Length)
            {
                throw new ArgumentException("New Capacity must be larger than the old");
            }
            T[] arr2 = new T[newCapacity];

            for (int i = 0; i < arr.Length; i++)
            {
                arr2[i] = arr[i];
            }
            arr = arr2;
        }

        /// <summary>
        /// Adds an item to the top of the stack.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (count == arr.Length)
            {
                Resize(2 * arr.Length);
            }
            arr[count++] = item;
        }

        /// <summary>
        /// Removes an item from the top of the stack.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            --count;
            T data = arr[count];
            arr[count] = default;

            return data;
        }

        /// <summary>
        /// Returns true if the stack is empty and vice versa.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The index, count, is returned.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        public T Peak()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            T data = arr[count];
            return data;
        }

        public void Clear()
        {
            T[] arr2 = new T[arr.Length];
            arr = arr2;
            count = 0;
        }

        public T[] ToArray()
        {
            return arr;
        }                   

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return arr[i];
            }
        }
    }
}
