using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterClass;

namespace CounterDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            //i am not sure why it is not working, this importing never seems to work on the first try for me. 
            //i dont know what i am doing wrong
            Counter myCount = new Counter("Frank Sinatra");

            Console.WriteLine(myCount.ToString());
            myCount.Increment();
            Console.WriteLine(myCount.ToString());
            Console.WriteLine($"get tally: {myCount.GetTally()}");
            myCount.Increment();
            Console.WriteLine(myCount.ToString());
            Console.WriteLine($"get tally: {myCount.GetTally()}");
        }
    }
}
