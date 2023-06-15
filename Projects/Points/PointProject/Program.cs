using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointLibrary;

namespace PointProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(5, 7);

            Console.WriteLine($"The magniturde of {p} is {p.Magnitude():N2}");
        }
    }
}
