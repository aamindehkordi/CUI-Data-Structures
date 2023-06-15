using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using LinkedListLibrary;

namespace LinkedListDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print multiplication table
            int N = 9;
            Console.Write("  |");
            for (int i = 1; i <= N; i++)
            {
                Console.Write($" {i,2} |");
            }
            Console.WriteLine("\n----------------------------");
            for (int i = 1; i <= N; i++)
            {
                Console.Write($"{i,2}|");
                for (int j = 1; j <= N; j++)
                {
                    Console.Write($" {i * j,2} |");
                }
                Console.WriteLine();
            }

            // Sort array
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++) { arr[i] = i; }
            arr[1] = 90; arr[6] = 4; arr[arr.Length - 1] = 6;
            ///////////////////////////////////////
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    // Swap elements if they are out of order
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            ///////////////////////////////////////

            // Compare array elements
            int i, j;
            i = 0; j = 1;
            if (arr.Length < 1) { return false; }
            while (arr[i].CompareTo(arr[j]) == -1)
            {
                i++;
                j++;
                if (j >= arr.Length) { break; }
            }
        }
    }
}