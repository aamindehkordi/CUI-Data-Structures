using System;
using System.Collections.Generic;
using System.Collections;

namespace Queue
{
    /// <summary>
    /// Represents a Queue data structure that follows the First-In-First-Out (FIFO) principle.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        /// <summary> The number of elements in the queue. </summary>
        private int count;
        /// <summary> The first element in the queue. </summary>
        private Node<T> head;
        /// <summary> The last element in the queue. </summary>
        private Node<T> tail;

        /// <summary> Represents a node in the queue. </summary>
        /// <typeparam name="T">The type of data stored in the node.</typeparam>
        private class Node<T>
        {
            /// <summary> The data stored in the node. </summary>
            public T data;
            /// <summary> The next node in the queue. </summary>
            public Node<T> next;

            /// <summary>
            /// Initializes a new instance of the Node class with the specified data.
            /// </summary>
            /// <param name="data">The data to store in the node.</param>
            public Node(T data = default)
            {
                /// <summary> The data stored in the node. </summary>
                this.data = data;
                /// <summary> The next node in the queue. </summary>
                this.next = null;
            }
        }

        /// <summary> Initializes a new instance of the Queue class. </summary>
        public Queue()
        {
            /// <summary> The number of elements in the queue. </summary>
            count = 0;
            /// <summary> The first element in the queue. </summary>
            head = null;
            /// <summary> The last element in the queue. </summary>
            tail = null;
        }

        /// <summary> Adds an element to the end of the queue. </summary>
        /// <param name="value">The element to add to the queue.</param>
        public void Enqueue(T value)
        {
            // If the queue is empty, set the head and tail to the new node.
            if (count == 0)
            {
                head = new Node<T>(value);
                tail = new Node<T>(value);
                count = 1;
            }
            else // add the new node to the end of the queue.
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
                count++;
            }
        }

        /// <summary> Removes and returns the element at the beginning of the queue. </summary>
        /// <returns>The element at the beginning of the queue.</returns>
        public T Dequeue()
        {
            // If the queue is empty, throw an exception.
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
            // Get the data from the head node.
            T data = head.data;
            // Move the head to the next node in the queue.
            head = head.next;
            // Decrement the count.
            count--;
            // Return the data.
            return data;
        }

        /// <summary> Checks if the queue is empty. </summary>
        /// <returns>True if the queue is empty; otherwise, false.</returns>
        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary> The number of elements in the queue. </summary>
        /// <returns>The number of elements in the queue.</returns>
        public int Count
        {
            get { return count; }
        }

        /// <summary> Removes all elements from the queue. </summary>
        public void Clear()
        {
            // Set the head and tail to null and the count to 0.
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary> Peeks at the element at the beginning of the queue. </summary>
        /// <returns>The element at the beginning of the queue.</returns>
        public T Peek()
        {
            // If the queue is empty, throw an exception.
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            // Return the data at the head of the queue.
            Node<T> n = head;
            return n.data;
        }

        /// <summary> Copies the queue to a new array. </summary>
        public T[] ToArray()
        {
            // Create a new array of the same size as the queue.
            T[] arr = new T[count];
            // Copy the queue to the array.
            for (int i = 0; i < count; i++)
            {
                Node<T> temp = head;
                temp = temp.next;
                temp.next = null;
                arr[i] = temp.data;
            }
            // Return the new array.
            return arr;
        }

        /// <summary> Returns an enumerator that iterates through the queue. </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary> Gets an enumerator for the queue. </summary>
        /// <returns>An enumerator for the queue.</returns>
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
