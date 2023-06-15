using System;
using System.Collections;
using System.Collections.Generic;

namespace StackLibrary
{
    /// <summary>
    /// Represents a Stack data structure that follows the Last-In-First-Out (LIFO) principle.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack.</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private int count;
        private T[] arr;

        /// <summary>
        /// Initializes a new instance of the Stack class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the stack.</param>
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
        /// <param name="item">The item to add to the stack.</param>
        public void Push(T item)
        {
            if (count == arr.Length)
            {
                Resize(2 * arr.Length);
            }
            arr[count++] = item;
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
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
        /// <returns>True if the stack is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the number of elements in the stack.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        /// <returns>The item at the top of the stack.</returns>
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            T data = arr[count - 1];
            return data;
        }

        /// <summary>
        /// Removes all elements from the stack.
        /// </summary>
        public void Clear()
        {
            T[] arr2 = new T[arr.Length];
            arr = arr2;
            count = 0;
        }

        /// <summary>
        /// Returns an array containing all elements in the stack.
        /// </summary>
        /// <returns>An array containing all elements in the stack.</returns>
        public T[] ToArray()
        {
            return arr;
        }                   

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the stack.
        /// </summary>
        /// <returns>An enumerator that iterates through the stack.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return arr[i];
            }
        }
    }
}