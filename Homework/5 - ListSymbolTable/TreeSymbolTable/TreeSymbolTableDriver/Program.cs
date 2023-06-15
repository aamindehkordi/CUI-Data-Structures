using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySearchTreeClass;

namespace TreeSymbolTableDriver
{
    class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Create a new binary search tree with string keys and integer values.
            BinarySearchTree<string, int> i = new BinarySearchTree<string, int>();

            // Add some key-value pairs to the tree.
            i.Add("hello", 1);
            i.Add("foob", 3);
            i.Add("goob", 1);
            i.Add("hello", 2);

            // Print the contents of the tree in order.
            i.PrintInOrder();

            // Find the successor of the key "foob" and print it.
            Console.WriteLine($"Successor: {i.Successor("foob")}");//goob

            // Find the predecessor of the key "hello" and print it.
            Console.WriteLine($"Predecessor: {i.Predecessor("hello")}");//goob
        }
    }
}