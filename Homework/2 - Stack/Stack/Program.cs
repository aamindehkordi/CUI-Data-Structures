using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackLibrary;

namespace StackDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> myStack32 = new Stack<int>(32);
            Stack<string> myStack = new Stack<string>();//should be 64

            myStack32.Push(1);//idk why but peak is returning a 0 instead of a 1 here
            Console.WriteLine($"{myStack32.Peak()}");
            myStack32.Clear();

            Console.WriteLine($"{myStack.IsEmpty()}");
            Console.WriteLine($"{myStack32.IsEmpty()}");

            for(int i = 0; i<32; i++)
            {
                if(i%10 == 0 || i == 0)
                {
                    Console.WriteLine($"{myStack.Count}");
                    Console.WriteLine($"{myStack32.Count}");
                }
                
                myStack32.Push(i);
                if(i%2 == 0)
                {
                    myStack.Push($"hi {i}");
                }
                else
                {
                    myStack.Push("nothing to see here");
                }
                
            }

            Console.WriteLine($"{myStack.IsEmpty()}");
            Console.WriteLine($"{myStack32.IsEmpty()}");

            //<-----Breakpoint here

            for (int i = 0; i < 32; i++)
            {
                if (i % 10 == 0 || i == 0)
                {
                    Console.WriteLine($"{myStack.Count}");
                    Console.WriteLine($"{myStack32.Count}");
                }

                myStack32.Pop();
                if (i % 2 == 0)
                {
                    Console.WriteLine($"{myStack.Pop()}");
                }
                else
                {
                    Console.WriteLine($"{myStack.Pop()}");
                }

            }

            Console.WriteLine($"{myStack.IsEmpty()}");
            Console.WriteLine($"{myStack32.IsEmpty()}");

             string[] myArr = myStack.ToArray();



        }
    }
}
