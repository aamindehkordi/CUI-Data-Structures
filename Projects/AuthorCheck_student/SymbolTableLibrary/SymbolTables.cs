using System;
using System.Collections;
using System.Collections.Generic;

namespace SymbolTableLibrary
{
    public abstract class SymbolTable<K, V> : IEnumerable<K>
    {
        public abstract int Count { get; }
        public abstract V this[K key] { get; set; }
        public abstract void Add(K key, V val);
        public abstract bool ContainsKey(K key);
        public abstract IEnumerator<K> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    class Node<K, V>
    {
        public K Key;
        public V Val;
        public Node<K, V> left, rite;
        public Node<K, V> next;
        public int count;

        public Node(K key, V value)
        {
            Key = key;
            Val = value;
            left = null; rite = null;
            next = null;
            count = 0;
        }
    }
    public class TreeSymbolTable<K, V> : SymbolTable<K, V>, IEnumerable<K> where K : IComparable<K>
    {
        private Node<K, V> root;
        private int count;
        public TreeSymbolTable()
        {
            root = null;
            count = 0;
        }
        private Node<K, V> GetNode(K key, Node<K, V> root)
        {
            Node<K, V> curr = root;
            while (curr != null)
            {
                int comp = key.CompareTo(curr.Key);
                if (comp == 0) { return curr; }
                if (comp < 0) { curr = curr.left; }
                if (comp > 0) { curr = curr.rite; }
            }
            return null;
        }
        public override bool ContainsKey(K key)
        {
            if (GetNode(key, root) != null) { return true; }
            return false;
        }

        public override void Add(K key, V value)
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
                while (curr != null)
                {
                    comp = key.CompareTo(curr.Key);
                    if (comp < 0)
                    {
                        if (curr.left != null) { curr = curr.left; }
                        else { curr.left = newNode; return; }
                    }
                    if (comp > 0)
                    {
                        if (curr.rite != null) { curr = curr.rite; }
                        else { curr.rite = newNode; return; }
                    }
                }
            }
        }
        /*public void Remove(K key)
        {
            Node<K, V> curr = GetNode(key, root);
            if (HasChildren(curr.Key))
            {

            }
            else
            {
                curr = null;
            }
        }*/
        public override int Count
        {
            get { return count; }
        }

        public override V this[K key]
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
        private bool HasChildren(K key)
        {
            if (Predecessor(key) == null) { return false; }
            if (Successor(key) == null) { return false; }
            return true;
        }
        private void PutKeysInLine(Queue<K> keys, Node<K, V> subroot)
        {
            if (subroot != null)
            {
                PutKeysInLine(keys, subroot.left);
                keys.Enqueue(subroot.Key);
                PutKeysInLine(keys, subroot.rite);
            }
        }
        public K Min()
        {
            Node<K, V> min = Min(root);
            return min.Key;
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
            return max.Key;
        }
        private Node<K, V> Max(Node<K, V> subroot)
        {
            if (subroot == null) { return null; }
            if (subroot.rite == null) { return subroot; }
            return Max(subroot.rite);
        }
        public K Predecessor(K key) // not safe
        {
            Node<K, V> node = GetNode(key, root);
            Node<K, V> pred = Predecessor(node);
            return pred.Key;
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
            return succ.Key;
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

        public override IEnumerator<K> GetEnumerator()
        {
            Queue<K> keys = new Queue<K>();
            PutKeysInLine(keys, root);
            foreach (K key in keys)
            {
                yield return key;
            }
        }
    }
    public class ListSymbolTable<K, V> : SymbolTable<K, V>, IEnumerable<K> where K : IComparable<K>
    {
        private Node<K, V> head;
        private Node<K, V> tail;
        private int count;
        public ListSymbolTable()
        {
            count = 0;
            head = null;
            tail = null;
        }
        private Node<K, V> GetNode(K key)
        {
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
        public override bool ContainsKey(K key)
        {
            if (GetNode(key) == null) { return false; }
            return true;
        }
        public override void Add(K key, V value)
        {
            if (count == 0)
            {
                head = new Node<K, V>(key, value);
                tail = head;
                count = 1;
            }
            else
            {
                tail.next = new Node<K, V>(key, value);
                tail = tail.next;
                count++;
            }
        }
        public void Remove(K key)
        {
            count--;
            Node<K, V> curr = head;
            if (curr.Key.Equals(key))
            {
                curr = null;
                head = curr.next;
            }
            else if (tail.Key.Equals(key))
            {
                while (curr.next != tail) { curr = curr.next; }
                curr.next = null;
                tail = curr;
            }
            else if (curr.next.Key.Equals(key))
            {
                curr.next = null;
                curr.next = curr.next.next;
            }
            curr = curr.next;
        }

        public override V this[K key]
        {
            get
            {
                Node<K, V> node = GetNode(key);
                if (node == null)
                {
                    throw new KeyNotFoundException($"The key {key} was not found.");
                }
                return node.Val;
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
                    node.Val = value;
                }
            }
        }
        public override int Count
        {
            get { return count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override IEnumerator<K> GetEnumerator()
        {
            Node<K, V> curr = head;
            while (curr != null)
            {
                yield return curr.Key;
                curr = curr.next;
            }
        }
    }
}
