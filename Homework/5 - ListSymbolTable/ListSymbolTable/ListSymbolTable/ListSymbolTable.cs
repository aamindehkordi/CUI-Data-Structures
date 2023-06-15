using System;
using System.Collections;
using System.Collections.Generic;

namespace ListSymbolTable
{
    /// <summary>
    /// Represents a node in a linked list.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    public class Node<K, V>
    {
        /// <summary>
        /// The key of the node.
        /// </summary>
        public K Key;

        /// <summary>
        /// The value of the node.
        /// </summary>
        public V Value;

        /// <summary>
        /// The next node in the linked list.
        /// </summary>
        public Node<K, V> next;

        /*public Node<K,V> prev;*/

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{K, V}"/> class.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="value">The value of the node.</param>
        public Node(K key, V value)
        {
            this.Key = key;
            this.Value = value;
            next = null;
            /*prev = null;*/
        }
    }

    /// <summary>
    /// Represents a symbol table implemented using a linked list.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    public class ListSymbolTable<K, V> : IEnumerable<K> where K : IComparable<K>
    {
        /// <summary> The head of the list symbol table. </summary>
        private Node<K, V> head;
        /// <summary> The tail of the list symbol table. </summary>
        private Node<K, V> tail;
        /// <summary> The number of elements in the list symbol table. </summary>
        private int count;

        /// <summary> Initializes a new instance of the <see cref="ListSymbolTable{K, V}"/> class. </summary>
        public ListSymbolTable()
        {
            count = 0; // Initialize the number of elements in the symbol table to 0.
            head = null; // Initialize the head of the symbol table to null.
            tail = null; // Initialize the tail of the symbol table to null.
        }

        /// <summary> Gets the node with the specified key. </summary>
        /// <param name="key">The key of the node to get.</param>
        private Node<K, V> GetNode(K key)
        {
            // Traverse the list to find the node with the specified key.
            Node<K, V> curr = this.head;
            while (curr != null)
            {
                if (curr.Key.Equals(key))
                {
                    return curr;
                }
                curr = curr.next;
            }
            return null;
        }

        /// <summary> 
        /// Determines whether the symbol table contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the symbol table.</param>
        public bool ContainsKey(K key)
        {
            if (GetNode(key) == null) { return false; }
            return true;
        }

        /// <summary>
        /// Adds the specified key-value pair to the symbol table.
        /// </summary>
        /// <param name="key">The key of the node to add.</param>
        /// <param name="value">The value of the node to add.</param>
        public void Add(K key, V value)
        {
            if (count == 0) // If the symbol table is empty
            {
                head = new Node<K, V>(key, value); // Add the node as the head
                tail = head; // Update the tail
                count = 1; // Update the number of elements in the symbol table
            }
            else
            {
                tail.next = new Node<K, V>(key, value); // Add the node to the end of the list
                tail = tail.next; // Update the tail
                count++; // Update the number of elements in the symbol table
            }
        }

        /// <summary>
        /// Removes the node with the specified key from the symbol table.
        /// </summary>
        /// <param name="key">The key of the node to remove.</param>
        public void Remove(K key)
        {
            count--;
            Node<K, V> curr = head;
            if (curr.Key.Equals(key)) // If the head node is the one to remove
            {
                curr = null; // Remove the node
                head = curr.next; // Update the head
            }
            else if (tail.Key.Equals(key)) // If the tail node is the one to remove
            {
                while (curr.next != tail) { curr = curr.next; } // Traverse the list to find the node before the tail
                curr.next = null; // Remove the tail node
                tail = curr; // Update the tail
            }
            else if (curr.next.Key.Equals(key)) // If a node in the middle of the list is the one to remove
            {
                curr.next = null; // Remove the node
                curr.next = curr.next.next; // Update the links
            }
            curr = curr.next;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public V this[K key]
        {
            get
            {
                Node<K, V> node = GetNode(key);
                if (node == null)
                {
                    throw new KeyNotFoundException($"The key {key} was not found.");
                }
                return node.Value;
            }
            set
            {
                Node<K, V> node = GetNode(key);
                if (node == null)
                {
                    Add(key, value);
                }
                else
                {
                    node.Value = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of elements in the symbol table.
        /// </summary>
        /// <returns>The number of elements in the symbol table.</returns>
        public int Count
        {
            get { return count; } // Return the number of elements in the symbol table.
        }

        /// <summary>
        /// Returns an enumerator that iterates through the keys in the symbol table.
        /// </summary>
        /// <returns>An enumerator that iterates through the keys in the symbol table.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator(); // Return an enumerator that iterates through the keys in the symbol table.
        }

        /// <summary>
        /// Returns an enumerator that iterates through the keys in the symbol table.
        /// </summary>
        /// <returns>An enumerator that iterates through the keys in the symbol table.</returns>
        public IEnumerator<K> GetEnumerator()
        {
            Node<K, V> curr = head; // Start at the head of the linked list.
            while (curr != null) // While there are still nodes to iterate through.
            {
                yield return curr.Key; // Return the current node's key.
                curr = curr.next; // Move to the next node.
            }
        }
    }
}
