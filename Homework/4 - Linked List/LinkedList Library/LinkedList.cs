using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a linked list data structure.
/// </summary>
namespace LinkedListLibrary
{
    /// <summary>
    /// Represents a node in a linked list.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the node.</typeparam>
    public class Node<T>
    {
        /// <summary> The data stored in the node. </summary>
        public T data;
        /// <summary> The next node in the linked list. </summary>
        public Node<T> next;
        /// <summary> The previous node in the linked list. </summary>
        public Node<T> prev;

        /// <summary> Initializes a new instance of the Node class. </summary>
        public Node(T data)
        {
            this.data = data;
            next = null;
            prev = null;
        }

        /// <summary> Prints the node and all subsequent nodes in the linked list. </summary>
        public void Print()
        {
            Console.Write($"|{data}|->");
            if (next != null)
            {
                next.Print();
            }
        }

        /// <summary> Adds a node to the end of the linked list. </summary>
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

    /// <summary>
    /// Represents a linked list data structure.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the linked list.</typeparam>
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {

        /// <summary> The number of elements in the linked list. </summary>
        private int count;
        /// <summary> The last node in the linked list. </summary>
        private Node<T> tail;
        /// <summary> The first node in the linked list. </summary>
        private Node<T> head;

        /// <summary> Initializes a new instance of the LinkedList class. </summary>
        public LinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary> Enqueues a new node to the end of the linked list. </summary>
        /// <param name="value">The data to enqueue.</param>
        public void AddToEnd(T value)
        {
            // If the list is empty, set head and tail to the new node.
            if (count == 0)
            {
                head = new Node<T>(value);
                tail = head;
                count = 1;
            }
            else // add the new node to the end of the list.
            {
                tail.next = new Node<T>(value);
                tail = tail.next;
                count++;
            }
        }

        /// <summary> Enqueues a new node to the beginning of the linked list. </summary>
        /// <param name="data">The data to enqueue.</param>
        public void Push(T data)
        {
            // If the list is empty, set head and tail to the new node.
            if (head == null)
            {
                head = new Node<T>(data);
                tail = head;
            }
            else // add the new node to the beginning of the list.
            {
                Node<T> temp = new Node<T>(data);
                temp.next = head;
                head = temp;

            }
            count++;
        }

        /// <summary> Inserts a new node at the specified index in the linked list. </summary>
        /// <param name="idx">The index to insert the new node at.</param>
        /// <param name="data">The data to insert.</param>
        public void Insert(int idx, T data)
        {
            // Set the current node to the head of the linked list
            Node<T> n = head;

            // Set the next node to the head's next node and set its previous node to the head
            Node<T> j = head.next; j.prev = n;

            // Traverse the linked list until we reach the node before the desired index
            for (int i = 0; i < idx - 1; i++)
            {
                // Set the current node to the next node
                n = j;

                // Set the current node's previous node to the previous node of the next node
                n.prev = j.prev;

                // Set the next node to the next node of the current node
                j = n.next;

                // Set the previous node of the next node to the current node
                j.prev = n;
            }

            // Create a new node with the given data
            Node<T> newNode = new Node<T>(data);

            // Set the current node's next node to the new node and set the new node's previous node to the current node
            n.next = newNode; newNode.prev = n;

            // Set the new node's next node to the next node and set the next node's previous node to the new node
            newNode.next = j; j.prev = newNode;

            // Increment the count of nodes in the linked list
            count++;
        }

        /// <summary>
        /// Removes and returns the first element from the list.
        /// </summary>
        /// <returns>The first element in the list.</returns>
        public T Pop()
        {
            // Save a reference to the head node.
            Node<T> n = head;

            // Set the head node to the next node in the list.
            head = head.next;

            // Remove the reference to the next node from the old head node.
            n.next = null;

            // Decrement the count of nodes in the list.
            count--;

            // Return the data from the old head node.
            return n.data;
        }

        /// <summary>
        /// Determines whether the list is empty.
        /// </summary>
        /// <returns>True if the list is empty; otherwise, false.</returns>
        public bool IsEmpty()
        {
            // Check if the count of nodes in the list is zero.
            if (count == 0)
            {
                // The list is empty.
                return true;
            }

            // The list is not empty.
            return false;
        }

        /// <summary>
        /// Searches for the specified value in the list.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The index of the first occurrence of the value in the list, or -1 if the value is not found.</returns>
        public int IndexOf(T value)
        {
            // Initialize the index to zero.
            int idx = 0;

            // Start at the head node.
            Node<T> curr = this.head;

            // Traverse the list until the end is reached or the value is found.
            while (curr != null)
            {
                // Check if the current node's data matches the value.
                if (curr.data.Equals(value))
                {
                    // The value was found; return the index.
                    return idx;
                }

                // Move to the next node in the list.
                curr = curr.next;

                // Increment the index.
                idx++;
            }

            // The value was not found.
            return -1;
        }

        /// <summary>
        /// Walks the linked list to the nth node and returns it.
        /// </summary>
        /// <param name="idx">The index of the node to return.</param>
        /// <returns>The nth node in the linked list.</returns>
        public Node<T> WalkToNthNode(int idx)
        {
            Node<T> n = head; // start at the head of the linked list
            n.prev = null; // set the previous node to null
            Node<T> j = head.next; // set the next node to the head's next node
            j.prev = n; // set the previous node of the next node to the head

            // loop through the linked list until we reach the nth node
            for (int i = 0; i < idx - 1; i++)
            {
                n = j; // set the current node to the next node
                n.prev = j.prev; // set the previous node of the current node to the previous node of the next node
                j = n.next; // set the next node to the current node's next node
                j.prev = n; // set the previous node of the next node to the current node
            }

            return n; // return the nth node
        }

        /// <summary>
        /// Removes the element at the specified index from the linked list.
        /// </summary>
        /// <param name="idx">The zero-based index of the element to remove.</param>
        public void RemoveAt(int idx)
        {
            // Find the node at the specified index
            Node<T> n = head;
            Node<T> j = head.next; j.prev = n;
            for (int i = 0; i < idx - 1; i++)
            {
                n = j;
                n.prev = j.prev;
                j = n.next;
                j.prev = n;
            }

            // Remove the node from the linked list
            n.next = j.next; j = null;
            count--;
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="idx">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int idx]
        {
            get
            {
                // Walk to the node at the specified index
                Node<T> node = WalkToNthNode(idx);

                // Return the data at the node
                return node.data;
            }
            set
            {
                // Walk to the node at the specified index
                Node<T> node = WalkToNthNode(idx);

                // Set the data at the node
                node.data = value;
            }
        }

        /// <summary> Gets the number of elements in the list. </summary>
        /// <returns>The number of elements in the list.</returns>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Sorts the linked list in ascending order using the selection sort algorithm.
        /// </summary>
        public void SlowSort()
        {//Selection Sort

            // Iterate through the list, selecting the minimum element in each iteration
            for (Node<T> start = head; start != null; start = start.next)
            {
                Node<T> min = start;

                // Find the minimum element in the unsorted portion of the list
                for (Node<T> eval = start.next; eval != null; eval = eval.next)
                {
                    if (eval.data.CompareTo(min.data) < 0)
                    {
                        min = eval;
                    }
                }

                // Swap the minimum element with the first element in the unsorted portion of the list
                T temp = min.data;
                min.data = start.data;
                start.data = temp;
            }
        }


        /// <summary>
        /// Sorts the given array using the QuickSort algorithm.
        /// </summary>
        /// <param name="arr">The array to sort.</param>
        /// <param name="A">The starting index of the subarray to sort.</param>
        /// <param name="B">The ending index of the subarray to sort.</param>
        public void QuickSort(int[] arr, int A, int B)
        {//Merge Sort

            // Print the current subarray being sorted
            for (int i = A; i <= B; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();

            // If the subarray has 1 or fewer elements, it is already sorted
            int count = B - A + 1;
            if (count <= 1)
            {
                return;
            }

            // Find the middle index of the subarray
            int M = A + (B - A) / 2;

            // Recursively sort the left and right halves of the subarray
            QuickSort(arr, A, M);
            QuickSort(arr, M + 1, B);

            // Merge the sorted left and right halves of the subarray
            MergeLists(arr, A, M, B);

        }

        /// <summary>
        /// Merges two sorted subarrays of the given array into a single sorted subarray.
        /// </summary>
        /// <param name="arr">The array containing the subarrays to merge.</param>
        /// <param name="A">The starting index of the first subarray.</param>
        /// <param name="M">The ending index of the first subarray.</param>
        /// <param name="B">The ending index of the second subarray.</param>
        private void MergeLists(int[] arr, int A, int M, int B)
        {
            // Create a temporary array to hold the merged subarray
            int[] tArr = new int[B - A + 1];
            int L = A;
            int R = M + 1;
            int T = 0;

            // Merge the two subarrays into the temporary array
            while (L <= M && R <= B)
            {
                if (arr[L] < arr[R])
                {
                    tArr[T++] = arr[L++];
                }
                else { tArr[T++] = arr[R++]; }
            }

            // Copy any remaining elements from the left subarray into the temporary array
            while (L <= M)
            {
                tArr[T++] = arr[L++];
            }

            // Copy any remaining elements from the right subarray into the temporary array
            while (R <= B)
            {
                tArr[T++] = arr[R++];
            }

            // Copy the merged subarray back into the original array
            for (int i = 0; i < T; i++)
            {
                arr[A + i] = tArr[i];
            }

            // Print the merged subarray
            for (int i = A; i <= B; i++)
            {
                Console.Write(arr[i]);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Sorts the given array in ascending order using the Bubble Sort algorithm.
        /// </summary>
        /// <param name="arr">The array to sort.</param>
        public void Sort(int[] arr)
        {
            int temp;

            // Iterate through the array
            for (int i = 0; i < arr.Length; i++)
            {
                // Compare each element with the elements that come after it
                for (int j = i + 1; j < arr.Length; j++)
                {
                    // If the current element is greater than the next element, swap them
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// Determines whether the given array is sorted in ascending order.
        /// </summary>
        /// <param name="arr">The array to check.</param>
        /// <returns>True if the array is sorted in ascending order, false otherwise.</returns>
        public bool ArrayIsSorted(int[] arr)
        {
            int i, j;
            i = 0; j = 1;

            // If the array is empty, it is not sorted
            if (arr.Length < 1) { return false; }

            // Iterate through the array and compare each element with the next element
            while (arr[i].CompareTo(arr[j]) == -1)
            {
                i++; j++;
            }

            // If the last element was not reached, the array is not sorted
            if (i != arr.Length - 1) { return false; }

            // If all elements were compared and no out-of-order elements were found, the array is sorted
            return true;
        }

        /// <summary>
        /// Returns the data of the first node in the queue.
        /// </summary>
        /// <returns>The data of the first node in the queue.</returns>
        public T Head()
        {
            // If the queue is empty, throw an exception.
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }

            // Get the first node in the queue.
            Node<T> n = head;

            // Return the data of the first node.
            return n.data;
        }

        /// <summary>
        /// Returns the data of the second node in the queue.
        /// </summary>
        /// <returns>The data of the second node in the queue.</returns>
        public T Peek()
        {
            // If the queue is empty, throw an exception.
            if (count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }

            // Get the second node in the queue.
            Node<T> n = head.next;

            // Return the data of the second node.
            return n.data;
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            // Set the head of the queue to null and the count to 0.
            head = null;
            count = 0;
        }
        /// <summary>
        /// Converts the linked list to an array.
        /// </summary>
        /// <returns>An array containing the elements of the linked list.</returns>
        public T[] ToArray()
        {
            T[] arr = new T[count];
            for (int i = 0; i < count; i++)
            {
                Node<T> temp = head;
                temp = temp.next;
                temp.next = null; // This line is unnecessary and causes a null reference exception
                arr[i] = temp.data;
            }

            return arr;
        }

        /// <summary>
        /// Determines whether the linked list contains a specific value.
        /// </summary>
        /// <param name="x">The value to locate in the linked list.</param>
        /// <returns>true if the value is found; otherwise, false.</returns>
        public bool Contains(T x)
        {
            if (this.IndexOf(x) == -1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Prints the elements of the linked list to the console.
        /// </summary>
        public void Print()
        {
            Node<T> n = head;
            while (n != null)
            {
                Console.Write($"|{n.data}|->");
                n = n.next;
            }
            Console.WriteLine($"|{n?.data}|"); // Use the null-conditional operator to avoid a null reference exception
        }

        /// <summary>
        /// Returns an enumerator that iterates through the linked list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the linked list.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the linked list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the linked list.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Start at the head of the linked list
            Node<T> curr = head;

            // Iterate through the linked list
            while (curr != null)
            {
                // Return the current element
                yield return curr.data;

                // Move to the next element
                curr = curr.next;
            }
        }

    }

}
