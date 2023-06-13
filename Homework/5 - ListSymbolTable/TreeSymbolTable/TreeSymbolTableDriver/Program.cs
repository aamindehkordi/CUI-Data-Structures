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
        static void Main(string[] args)
        {
            BinarySearchTree<string, int> i = new BinarySearchTree<string, int>();
            i.Add("hello", 1);
            i.Add("foob", 3);
            i.Add("goob", 1);
            i.Add("hello", 2);
            i.PrintInOrder();
            Console.WriteLine($"Successor: {i.Successor("foob")}");//goob
            Console.WriteLine($"Predecessor: {i.Predecessor("hello")}");//goob
        }
    }
}
