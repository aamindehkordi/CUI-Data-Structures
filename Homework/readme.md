# Homework

This folder contains the homework assignments completed for the course. These homework assignments are data structures created from scratch in C#.

## [Homework 0 - Circle](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/0)

This directory contains the implementation of a Circle class in C#. The Circle class is defined with a radius and provides methods to calculate the area and circumference of the circle. The Circle class is used in a test program to create Circle objects with different radii and display their properties.

### - Files

- `CircleProgram.cs`: This is the main program that creates Circle objects and displays their properties. It creates three Circle objects with radii of 0, 1, and -1, and displays their radius, perimeter, and area. If a Circle object is attempted to be created with a negative radius, an exception is thrown and caught, displaying an error message.

- `CircleLibrary/CircleLibrary.cs`: This file contains the definition of the Circle class. The Circle class has a public double field for the radius and a constructor that takes a double for the radius. If the radius is negative, the constructor throws an ArgumentException. The Circle class also has methods for calculating the area and circumference of the circle, and a ToString method that returns a string representation of the Circle object.

## [Homework 1 - Counter](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/1)

This directory contains the implementation of a Counter class in C# and a driver program that uses the Counter class. The Counter class is defined with an id and a count, and provides methods to increment the count and get the current tally. The driver program creates a Counter object, increments the count, and displays the current tally.

### - Files

- `CounterDriver.cs`: This is the main program that creates a Counter object and displays its properties. It creates a Counter object with an id of "Frank Sinatra", increments the count twice, and displays the current tally each time.

- `CounterClass/Counter.cs`: This file contains the definition of the Counter class. The Counter class has a private string field for the id and a private int field for the count. It has a constructor that takes a string for the id and initializes the count to 0. It also has methods for incrementing the count and getting the current tally, and a ToString method that returns a string representation of the Counter object.

## [Homework 2 - Stack](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/2%20-%20Stack)

This directory contains the implementation of a Stack data structure for the second homework assignment. The Stack data structure follows the Last-In-First-Out (LIFO) principle. The directory is divided into two main subdirectories:

### - Stack

This subdirectory contains the main program that utilizes the Stack data structure. The `Program.cs` file creates two instances of the Stack class, one for integers and one for strings. It then performs various operations such as pushing elements to the stacks, checking if the stacks are empty, popping elements from the stacks, and converting the string stack to an array.

### - StackLibrary

This subdirectory contains the `StackLibrary.cs` file which implements the Stack data structure. The Stack class includes methods for pushing an item to the stack, popping an item from the stack, checking if the stack is empty, getting the count of elements in the stack, peeking at the top item of the stack, clearing the stack, and converting the stack to an array.

The Stack data structure is implemented generically, meaning it can hold elements of any type. The Stack class also implements the `IEnumerable` interface, allowing the elements in the stack to be iterated over in a foreach loop.

## [Homework 3 - Queue](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/3%20-%20Queue)

This directory contains two subdirectories, each with a different focus on the Queue data structure.

### - Queue

This subdirectory contains a single file, `Queue.cs`, which implements a generic Queue data structure. The Queue follows the First-In-First-Out (FIFO) principle. It provides methods for enqueueing and dequeueing elements, checking if the queue is empty, getting the count of elements, clearing the queue, peeking at the first element, converting the queue to an array, and getting an enumerator for the queue.

### - QueueDemo

This subdirectory contains a `Program.cs` file which demonstrates the usage of the Queue data structure implemented in the `Queue.cs` file. It creates a Queue and a Stack, pushes and pops elements from the Stack, and enqueues and dequeues elements from the Queue.

## [Homework 4 - Linked List](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/4%20-%20Linked%20List)

This homework assignment focuses on the implementation and usage of a linked list data structure. The main components of this assignment are:

### - Linked List: This is a simple implementation of a singly linked list. The linked list has methods for adding elements to the beginning and end of the list, removing elements from the beginning and end of the list, and accessing elements at a specific index. It also includes methods for inserting elements at a specific index and removing elements at a specific index

### - Program: This is the main driver program that uses the linked list. It reads a text file, converts the text to lowercase, and splits the text into words. It then counts the total number of words and the frequency of each word using the linked list

## [Homework 5 - ListSymbolTable](https://github.com/aamindehkordi/CUI-Data-Structures/tree/main/Homework/5%20-%20ListSymbolTable)

This directory contains a subdirectory named 'ListSymbolTable' which focuses on the implementation of a symbol table using a list. A symbol table is a data structure used in compilers to hold information about source-program constructs. The information is entered in the table by the compiler's lexical analyzer, and the information is used by the analysis and synthesis parts of a compiler.

### - ListSymbolTable

This subdirectory contains a 'SymbolTableDriver' subdirectory.

### - TreeSymbolTable

The `TreeSymbolTable` is an implementation of a symbol table data structure using a Binary Search Tree (BST). A symbol table is a data structure for key-value pairs that supports two operations: insert (put) a new pair, and search for (get) a pair given the key.
