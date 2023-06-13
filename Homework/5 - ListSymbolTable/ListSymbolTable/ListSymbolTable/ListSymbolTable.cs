using System;
using System.Collections;
using System.Collections.Generic;

namespace ListSymbolTable
{
    public class Node<K, V>
    {
        public K Key;
        public V Value;
        public Node<K, V> next;
        /*public Node<K,V> prev;*/

        public Node(K key, V value)
        {
            this.Key = key;
            this.Value = value;
            next = null;
            /*prev = null;*/
        }
    }
    public class ListSymbolTable<K, V> : IEnumerable<K> where K : IComparable<K>
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
        public bool ContainsKey(K key)
        {
            if (GetNode(key) == null) { return false; }
            return true;
        }
        public void Add(K key, V value)
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
        public int Count
        {
            get { return count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<K> GetEnumerator()
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
