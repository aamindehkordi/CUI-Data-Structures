using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircleLibrary;

namespace CircleTestProgram
{
    /// <summary> Circle Program </summary>
    class CircleProgram
    {
        /// <summary> Main method </summary>
        /// <param name="args">command line arguments</param>
        static void Main(string[] args)
        {
            /** 
                * 1. Create a Circle object with a radius of 0
                * 2. Create a Circle object with a radius of 1
                * 3. Create a Circle object with a radius of -1
                * 4. Display the radius, perimeter, and area for each Circle object
                * 5. Display the number of Circle objects created
            */

            // Instantiate 3 Circle objects
            Circle zCircle;
            Circle pCircle;
            Circle nCircle;

            // Try to instantiate a Circle object with a negative radius
            try
            {
                zCircle = new Circle(0);
                pCircle = new Circle(1);
                nCircle = new Circle(-1);
            }
            // Catch the exception and display the error message
            catch(ArgumentException ae)
            {
                Console.WriteLine("no negative nancies!" + ae.Message);
                return;
            }
            
            // Display the number of Circle objects created
            Console.WriteLine($"Radius: {zCircle.Radius,5:N2} => Perimeter:{ zCircle.Circumference(),5:N2} Area: { zCircle.Area(),5:N2}");
            Console.WriteLine($"Radius: {pCircle.Radius,5:N2} => Perimeter:{ pCircle.Circumference(),5:N2} Area: { pCircle.Area(),5:N2}");
            Console.WriteLine($"Radius: {nCircle.Radius,5:N2} => Perimeter:{ nCircle.Circumference(),5:N2} Area: { nCircle.Area(),5:N2}");
        }
    }
}
