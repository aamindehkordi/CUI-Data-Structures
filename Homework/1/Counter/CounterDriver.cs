using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterClass;

/// <summary>
/// The CounterDriver namespace contains the main entry point for the Counter program.
/// </summary>
namespace CounterDriver
{
    /// <summary> Program </summary>
    class Program
    {
        /// <summary> Main method </summary>
        /// <param name="args">command line arguments</param>
        static void Main(string[] args)
        {
            /** 
                * 1. Create a Counter object with an id of "Frank Sinatra"
                * 2. Display the Counter object
                * 3. Increment the Counter object
                * 4. Display the Counter object
                * 5. Display the tally of the Counter object
                * 6. Increment the Counter object
                * 7. Display the Counter object
                * 8. Display the tally of the Counter object
            */
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
