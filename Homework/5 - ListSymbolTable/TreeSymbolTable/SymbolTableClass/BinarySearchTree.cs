using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeClass
{
    class Node<K, V>
    {
        public K key;
        public V Val;
        public Node<K, V> left, rite;
        public int count;

        public Node(K key, V value)
        {
            this.key = key;
            this.Val = value;
            left = null; rite = null;
            count = 0;
        }
    }
    public class BinarySearchTree<K, V> : IEnumerable<K> where K : IComparable<K>
    {
        private Node<K, V> root;
        private int count;
        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }
        private Node<K,V> GetNode(K key, Node<K,V> root)
        {
            Node<K, V> curr = root;
            while (curr != null)
            {
                int comp = key.CompareTo(curr.key);
                if(comp == 0) { return curr; }
                if (comp < 0) { curr = curr.left; }
                if (comp > 0) { curr = curr.rite; }
            }
            return null;
        }
        private bool ContainsKey(K key)
        {
            if (GetNode(key,root) != null) { return true; }
            return false;
        }

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
        public void Remove(K key)
        {
            Node<K,V> curr = GetNode(key, root);
            if (HasChildren(curr.key))
            {

            }
            else
            {
                curr = null;
            }
        }
        public int Count
        {
            get { return count; }
        }

        public V this[K key]
        {
            get
            {
                Node<K, V> node = GetNode(key,root);
                if (node == null)
                {
                    throw new KeyNotFoundException($"The key {key} was not found.");
                }
                return node.Val;
            }
            set
            {
                Node<K, V> node = GetNode(key,root);
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
        private bool HasChildren(K key)
        {
            if (Predecessor(key) == null) { return false; }
            if (Successor(key) == null) { return false; }
            return true;
        }
        private void PutKeysInLine(Queue<K> keys, Node<K,V> subroot)
        {
            if(subroot != null)
            {
                PutKeysInLine(keys, subroot.left);
                keys.Enqueue(subroot.key);
                PutKeysInLine(keys, subroot.rite);
            }
        }
        public K Min()
        {
            Node<K, V> min = Min(root);
            return min.key;
        }
        private Node<K, V> Min(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            if (subroot.left == null) { return subroot; }
            return Min(subroot.left);
        }
        public K Max()
        {
            Node<K, V> max = Max(root);
            return max.key;
        }
        private Node<K, V> Max(Node<K, V> subroot)
        {
            if(subroot == null) { return null; }
            if(subroot.rite == null) { return subroot; }
            return Max(subroot.rite);
        }
        public K Predecessor(K key) // not safe
        {
            Node<K, V> node = GetNode(key, root);
            Node<K, V> pred = Predecessor(node);
            return pred.key;
        }
        private Node<K, V> Predecessor(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            return Max(subroot.left);
        }
        public K Successor(K key)
        {
            Node<K, V> node = GetNode(key, root);
            Node<K, V> succ = Successor(node);
            return succ.key;
        }
        private Node<K, V> Successor(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            return Min(subroot.rite);
        }
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
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<K> GetEnumerator()
        {
            Queue<K> keys = new Queue<K>();
            PutKeysInLine(keys, root);
            foreach(K key in keys)
            {
                yield return key;
            }
        }
    }
}
