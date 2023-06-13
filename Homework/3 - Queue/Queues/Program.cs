using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue;

namespace Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> myQueue = new Queue<int>(); 
            myQueue.Enqueue(3);
            myQueue.Enqueue(5);
            myQueue.Enqueue(4);
            myQueue.Enqueue(6);
            myQueue.Dequeue();
            myQueue.Dequeue();
            myQueue.Dequeue();
            myQueue.Dequeue();
            myQueue.Enqueue(3);
            myQueue.Enqueue(5);
            myQueue.Enqueue(4);
            myQueue.Enqueue(6);
            
            Console.WriteLine($"{myQueue.Peek()}");//there is an error, i tried showing you in class but we ran out of time
            myQueue.Clear();
            Console.WriteLine($" the queue is empty? {myQueue.IsEmpty()}");

            myQueue.Enqueue(3);
            myQueue.Enqueue(5);
            myQueue.Enqueue(4);
            myQueue.Enqueue(6);

            int[] arr = myQueue.ToArray();
            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }

        }
    }
}
