using System;
using System.Collections.Generic;

namespace QueueDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> q = new Queue<string>();
            Stack<string> s = new Stack<string>();

            Console.WriteLine("Stack:");
            s.Push("First");
            Console.WriteLine("  Push First");
            s.Push("Second");
            Console.WriteLine("  Push Second");
            s.Push("Third");
            Console.WriteLine("  Push Third");
            while (s.Count > 0)
            {
                Console.WriteLine($"   Pop {s.Pop()}");
            }


            Console.WriteLine("\nQueue:");
            q.Enqueue("First");
            Console.WriteLine("  Enqueue First");
            q.Enqueue("Second");
            Console.WriteLine("  Enqueue Second");
            q.Enqueue("Third");
            Console.WriteLine("  Enqueue Third");
            while (q.Count > 0)
            {
                Console.WriteLine($"  Dequeue {q.Dequeue()}");
            }
        }
    }
}
