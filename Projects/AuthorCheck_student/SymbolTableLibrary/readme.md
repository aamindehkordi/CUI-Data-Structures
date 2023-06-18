# [Homework 5 - ListSymbolTable](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/5%20-%20ListSymbolTable)

This directory contains a subdirectory named 'ListSymbolTable' which focuses on the implementation of a symbol table using a list. A symbol table is a data structure used in compilers to hold information about source-program constructs. The information is entered in the table by the compiler's lexical analyzer, and the information is used by the analysis and synthesis parts of a compiler.

## ListSymbolTable

This subdirectory contains a 'SymbolTableDriver' subdirectory.

### SymbolTableDriver

This subdirectory contains a `Program.cs` file which demonstrates the usage of a symbol table implemented as a list. The program reads a text file, splits the text into words, and counts the frequency of each word using the symbol table. This is done by creating a new entry in the symbol table for each unique word, and incrementing the count of the entry each time the word is encountered again. The program then prints out the words and their frequencies in descending order of frequency.

The program also provides a `Counter` class to keep track of word count and frequency. The `Counter` class has two properties: `Word`, which holds the word, and `Count`, which holds the count of the word. It also overrides the `ToString` method to return a string representation of the `Counter` object in the format 'Word: Count'.

## TreeSymbolTable

The `TreeSymbolTable` is an implementation of a symbol table data structure using a Binary Search Tree (BST). A symbol table is a data structure for key-value pairs that supports two operations: insert (put) a new pair, and search for (get) a pair given the key.

### BinarySearchTree.cs

This file contains the implementation of the BST used in the `TreeSymbolTable`. The BST is a node-based binary tree data structure with the properties that the left subtree of a node contains only nodes with keys lesser than the node’s key, the right subtree of a node contains only nodes with keys greater than the node’s key, and the left and right subtree each must also be a binary search tree.

The `BinarySearchTree.cs` file includes the following classes and methods:

- `Node<K, V>`: This class represents a node in the BST. It has properties for the key, value, left child, right child, and count (the number of nodes in the subtree rooted at this node).

- `BinarySearchTree<K, V>`: This class represents the BST itself. It includes methods for adding a node, removing a node, checking if a node with a specific key exists, getting the node with a specific key, and getting the number of nodes in the tree. It also includes methods for finding the minimum and maximum keys, the predecessor and successor of a key, and for printing the keys in order.

This BST implementation is generic, meaning it can be used with any data types for the keys and values, as long as the key type implements the `IComparable` interface. This is necessary for the BST to be able to compare keys and maintain its properties.

This Binary Search Tree is a fundamental part of the `TreeSymbolTable` implementation, providing efficient search, insert, and delete operations.
