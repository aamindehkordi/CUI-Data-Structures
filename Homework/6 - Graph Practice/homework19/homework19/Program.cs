using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphProject;

namespace homework19
{
    class Program
    {
        static void Main(string[] args)
        {
            MathGraph<int> x = new MathGraph<int>("default");
            x.AddVertex(1); x.AddVertex(2); x.AddVertex(3);
            x.AddVertex(4); x.AddVertex(5); x.AddVertex(6);
            x.AddVertex(7); x.AddVertex(8); x.AddVertex(9); x.AddVertex(0);

            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    x.AddEdge(i, j);
                }
            }
            x.AddEdge(1, 0);

            x.DumpGraph();
            Console.WriteLine($"Contains Edge(1,0):   expected:true   actual: {x.ContainsEdge(1, 0)}");
            Console.WriteLine($"Contains Edge(1,0):   expected:true   actual: {x.ContainsEdge(1, 2)}");
            Console.WriteLine($"Contains Vertex(0):   expected:true   actual: {x.ContainsVertex(0)}");
            Console.WriteLine($"Contains Vertex(10):  expected:false  actual: {x.ContainsVertex(10)}");
            Console.WriteLine($"Count Adjacent(0):    expected:10     actual: {x.CountAdjacent(0)}");
            Console.WriteLine($"Count Adjacent(1):    expected:20     actual: {x.CountAdjacent(1)}");
            Console.WriteLine($"TestConnectedTo(1, 9):expected:true   actual: {x.TestConnectedTo(1, 9)}");
            Console.WriteLine($"{x.FindFirstPath(1,9).ToArray()}");//?
            Console.WriteLine($"{x.FindShortestPath(1, 9).ToArray()}");//?
            Console.WriteLine($"{x.EnumAdjacent(1)} {x.EnumAdjacent(0)}");
        }
    }
}
