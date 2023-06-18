## TreeSymbolTable

The `TreeSymbolTable` is an implementation of a symbol table data structure using a Binary Search Tree (BST). A symbol table is a data structure for key-value pairs that supports two operations: insert (put) a new pair, and search for (get) a pair given the key.

### BinarySearchTree.cs

This file contains the implementation of the BST used in the `TreeSymbolTable`. The BST is a node-based binary tree data structure with the properties that the left subtree of a node contains only nodes with keys lesser than the node’s key, the right subtree of a node contains only nodes with keys greater than the node’s key, and the left and right subtree each must also be a binary search tree.

The `BinarySearchTree.cs` file includes the following classes and methods:

- `Node<K, V>`: This class represents a node in the BST. It has properties for the key, value, left child, right child, and count (the number of nodes in the subtree rooted at this node).

- `BinarySearchTree<K, V>`: This class represents the BST itself. It includes methods for adding a node, removing a node, checking if a node with a specific key exists, getting the node with a specific key, and getting the number of nodes in the tree. It also includes methods for finding the minimum and maximum keys, the predecessor and successor of a key, and for printing the keys in order.

This BST implementation is generic, meaning it can be used with any data types for the keys and values, as long as the key type implements the `IComparable` interface. This is necessary for the BST to be able to compare keys and maintain its properties.

This Binary Search Tree is a fundamental part of the `TreeSymbolTable` implementation, providing efficient search, insert, and delete operations.
