using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeClass
{
    /// <summary>
    /// A node in a binary search tree.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    class Node<K, V>
    {
        /// <summary>
        /// The key of the node.
        /// </summary>
        public K key;

        /// <summary>
        /// The value of the node.
        /// </summary>
        public V Val;

        /// <summary>
        /// The left child of the node.
        /// </summary>
        public Node<K, V> left;

        /// <summary>
        /// The right child of the node.
        /// </summary>
        public Node<K, V> rite;
        /// <summary>
        /// The number of nodes in the subtree rooted at this node.
        /// </summary>
        public int count;

        /// <summary>
        /// Constructs a new node with the given key and value.
        /// </summary>
        /// <param name="key">The key of the new node.</param>
        /// <param name="value">The value of the new node.</param>
        public Node(K key, V value)
        {
            this.key = key;
            this.Val = value;
            left = null; rite = null;
            count = 0;
        }
    }

    /// <summary>
    /// A binary search tree.
    /// </summary>
    /// <typeparam name="K">The type of the key.</typeparam>
    /// <typeparam name="V">The type of the value.</typeparam>
    public class BinarySearchTree<K, V> : IEnumerable<K> where K : IComparable<K>
    {
        private Node<K, V> root;
        private int count;

        /// <summary>
        /// Constructs a new empty binary search tree.
        /// </summary>
        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }

        /// <summary>
        /// Gets the node with the given key in the subtree rooted at the given node.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="root">The root of the subtree to search in.</param>
        /// <returns>The node with the given key, or null if it is not found.</returns>
        private Node<K, V> GetNode(K key, Node<K, V> root)
        {
            Node<K, V> curr = root;
            while (curr != null)
            {
                int comp = key.CompareTo(curr.key);
                if (comp == 0) { return curr; }
                if (comp < 0) { curr = curr.left; }
                if (comp > 0) { curr = curr.rite; }
            }
            return null;
        }

        /// <summary>
        /// Checks if the tree contains a node with the given key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>True if the tree contains a node with the given key, false otherwise.</returns>
        private bool ContainsKey(K key)
        {
            if (GetNode(key, root) != null) { return true; }
            return false;
        }

        /// <summary>
        /// Adds a new node with the given key and value to the tree.
        /// </summary>
        /// <param name="key">The key of the new node.</param>
        /// <param name="value">The value of the new node.</param>
        public void Add(K key, V value)
        {
            Node<K, V> newNode = new Node<K, V>(key, value);
            Node<K, V> oldNode = GetNode(key, root);

            if (root == null)
            {
                root = newNode;
            }
            else if (this.ContainsKey(key)) { oldNode.Val = value; }
            else
            {
                int comp;
                Node<K, V> curr = root;
                while (curr.left != null || curr.rite != null)
                {
                    comp = key.CompareTo(curr.key);

                    if (comp < 0) { curr = curr.left; }
                    if (comp > 0) { curr = curr.rite; }
                }
                comp = key.CompareTo(curr.key);

                if (comp < 0) { curr.left = newNode; }
                if (comp > 0) { curr.rite = newNode; }
            }
        }

        /// <summary>
        /// Removes the node with the given key from the tree.
        /// </summary>
        /// <param name="key">The key of the node to remove.</param>
        public void Remove(K key)
        {
            Node<K, V> curr = GetNode(key, root);
            if (HasChildren(curr.key))
            {

            }
            else
            {
                curr = null;
            }
        }

        /// <summary>
        /// Gets the number of nodes in the tree.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Gets or sets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key to get or set the value for.</param>
        /// <returns>The value associated with the given key.</returns>
        public V this[K key]
        {
            get
            {
                Node<K, V> node = GetNode(key, root);
                if (node == null)
                {
                    throw new KeyNotFoundException($"The key {key} was not found.");
                }
                return node.Val;
            }
            set
            {
                Node<K, V> node = GetNode(key, root);
                if (node == null)
                {
                    Add(key, value);
                }
                else
                {
                    node.Val = value;
                }
            }
        }

        /// <summary>
        /// Checks if the node with the given key has any children.
        /// </summary>
        /// <param name="key">The key of the node to check.</param>
        /// <returns>True if the node has children, false otherwise.</returns>
        private bool HasChildren(K key)
        {
            if (Predecessor(key) == null) { return false; }
            if (Successor(key) == null) { return false; }
            return true;
        }

        /// <summary>
        /// Adds the keys in the subtree rooted at the given node to the given queue in order.
        /// </summary>
        /// <param name="keys">The queue to add the keys to.</param>
        /// <param name="subroot">The root of the subtree to add the keys from.</param>
        private void PutKeysInLine(Queue<K> keys, Node<K, V> subroot)
        {
            if (subroot != null)
            {
                PutKeysInLine(keys, subroot.left);
                keys.Enqueue(subroot.key);
                PutKeysInLine(keys, subroot.rite);
            }
        }

        /// <summary>
        /// Gets the key of the smallest node in the tree.
        /// </summary>
        /// <returns>The key of the smallest node in the tree.</returns>
        public K Min()
        {
            Node<K, V> min = Min(root);
            return min.key;
        }

        /// <summary>
        /// Gets the smallest node in the subtree rooted at the given node.
        /// </summary>
        /// <param name="subroot">The root of the subtree to search in.</param>
        /// <returns>The smallest node in the subtree.</returns>
        private Node<K, V> Min(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            if (subroot.left == null) { return subroot; }
            return Min(subroot.left);
        }

        /// <summary>
        /// Gets the key of the largest node in the tree.
        /// </summary>
        /// <returns>The key of the largest node in the tree.</returns>
        public K Max()
        {
            Node<K, V> max = Max(root);
            return max.key;
        }

        /// <summary>
        /// Gets the largest node in the subtree rooted at the given node.
        /// </summary>
        /// <param name="subroot">The root of the subtree to search in.</param>
        /// <returns>The largest node in the subtree.</returns>
        private Node<K, V> Max(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            if (subroot.rite == null) { return subroot; }
            return Max(subroot.rite);
        }

        /// <summary>
        /// Gets the key of the node with the largest key less than the given key.
        /// </summary>
        /// <param name="key">The key to search for the predecessor of.</param>
        /// <returns>The key of the predecessor of the given key.</returns>
        public K Predecessor(K key) // not safe
        {
            Node<K, V> node = GetNode(key, root);
            Node<K, V> pred = Predecessor(node);
            return pred.key;
        }

        /// <summary>
        /// Gets the node with the largest key less than the key of the given node.
        /// </summary>
        /// <param name="subroot">The node to search for the predecessor of.</param>
        /// <returns>The node with the largest key less than the key of the given node.</returns>
        private Node<K, V> Predecessor(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            return Max(subroot.left);
        }

        /// <summary>
        /// Gets the key of the node with the smallest key greater than the given key.
        /// </summary>
        /// <param name="key">The key to search for the successor of.</param>
        /// <returns>The key of the successor of the given key.</returns>
        public K Successor(K key)
        {
            Node<K, V> node = GetNode(key, root);
            Node<K, V> succ = Successor(node);
            return succ.key;
        }

        /// <summary>
        /// Gets the node with the smallest key greater than the key of the given node.
        /// </summary>
        /// <param name="subroot">The node to search for the successor of.</param>
        /// <returns>The node with the smallest key greater than the key of the given node.</returns>
        private Node<K, V> Successor(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            return Min(subroot.rite);
        }

        /// <summary>
        /// Prints the keys in the tree in order.
        /// </summary>
        public void PrintInOrder()
        {
            Queue<K> keys = new Queue<K>();
            PutKeysInLine(keys, root);
            foreach (K key in keys)
            {
                Console.Write($"[{key}]->");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Gets an enumerator for the keys in the tree.
        /// </summary>
        /// <returns>An enumerator for the keys in the tree.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator for the keys in the tree.
        /// </summary>
        /// <returns>An enumerator for the keys in the tree.</returns>
        public IEnumerator<K> GetEnumerator()
        {
            Queue<K> keys = new Queue<K>();
            PutKeysInLine(keys, root);
            foreach (K key in keys)
            {
                yield return key;
            }
        }
    }
}