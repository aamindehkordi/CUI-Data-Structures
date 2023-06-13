using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircleLibrary;

namespace CircleTestProgram
{
    class CircleProgram
    {
        static void Main(string[] args)
        {
            Circle zCircle;
            Circle pCircle;
            Circle nCircle;

            try
            {
                zCircle = new Circle(0);
                pCircle = new Circle(1);
                nCircle = new Circle(-1);
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("no negative nancies!" + ae.Message);
                return;
            }
            Console.WriteLine($"Radius: {zCircle.Radius,5:N2} => Perimeter:{ zCircle.Circumference(),5:N2} Area: { zCircle.Area(),5:N2}");
            Console.WriteLine($"Radius: {pCircle.Radius,5:N2} => Perimeter:{ pCircle.Circumference(),5:N2} Area: { pCircle.Area(),5:N2}");
            Console.WriteLine($"Radius: {nCircle.Radius,5:N2} => Perimeter:{ nCircle.Circumference(),5:N2} Area: { nCircle.Area(),5:N2}");
        }
    }
}
