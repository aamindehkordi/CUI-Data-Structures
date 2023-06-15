using System;
using System.Collections.Generic;
using StackLibrary;

namespace StackDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two instances of the Stack class
            var intStack = new Stack<int>(32);
            var stringStack = new Stack<string>();

            // Push an element to intStack and print the top element
            intStack.Push(1);
            Console.WriteLine($"{intStack.Peek()}");

            // Clear intStack
            intStack.Clear();

            // Check if the stacks are empty
            Console.WriteLine($"{stringStack.IsEmpty()}");
            Console.WriteLine($"{intStack.IsEmpty()}");

            // Push elements to the stacks
            for (int i = 0; i < 32; i++)
            {
                // Print the count of elements in the stacks every 10 iterations
                if (i % 10 == 0 || i == 0)
                {
                    Console.WriteLine($"{stringStack.Count}");
                    Console.WriteLine($"{intStack.Count}");
                }

                // Push elements to the stacks
                intStack.Push(i);
                if (i % 2 == 0)
                {
                    stringStack.Push($"hi {i}");
                }
                else
                {
                    stringStack.Push("nothing to see here");
                }
            }

            // Check if the stacks are empty
            Console.WriteLine($"{stringStack.IsEmpty()}");
            Console.WriteLine($"{intStack.IsEmpty()}");

            // Pop elements from the stacks
            foreach (var item in intStack)
            {
                // Print the count of elements in the stacks every 10 iterations
                if (item % 10 == 0 || item == 0)
                {
                    Console.WriteLine($"{stringStack.Count}");
                    Console.WriteLine($"{intStack.Count}");
                }

                // Pop elements from the stacks and print them
                intStack.Pop();
                if (item % 2 == 0)
                {
                    Console.WriteLine($"{stringStack.Pop()}");
                }
                else
                {
                    Console.WriteLine($"{stringStack.Pop()}");
                }
            }

            // Check if the stacks are empty
            Console.WriteLine($"{stringStack.IsEmpty()}");
            Console.WriteLine($"{intStack.IsEmpty()}");

            // Convert stringStack to an array
            string[] myArr = stringStack.ToArray();
        }
    }
}