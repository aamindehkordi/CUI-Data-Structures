using GraphLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace KevinBaconGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a graph from database");
            MathGraph<string> master = new MathGraph<string>("Master Film List");
            Stopwatch stopwatch = new Stopwatch();
            string file = "top250.txt";
            stopwatch.Restart();
            CreateGraphFromFile(master, file);
            stopwatch.Stop();

            bool x = false;
            while (!x)
            {
                x = true;

                string[] actor = new string[2];
                bool j = false;
                if (args.Length != 0)
                {
                    actor[0] = args[0];
                    actor[1] = args[1];
                }
                else
                {
                    while (!j)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter two names of actors inclosed in quotes, Case Matters\n" +
                            "For example: Kevin Bacon or Harrison Ford");
                        Console.Write("Actor(ress) 1:");
                        actor[0] = Console.ReadLine();
                        foreach (char c in actor[0])
                        {
                            if (c < 0 || c > 9) { j = true; } else { j = false; }
                        }
                        Console.Write("Actor(ress) 2:");
                        actor[1] = Console.ReadLine();
                        foreach (char c in actor[1])
                        {
                            if (c < 0 || c > 9) { j = true; } else { j = false; }
                        }
                        Console.Clear();
                    }
                }
                
                Console.WriteLine($"Loaded {file} in {stopwatch.Elapsed}");
                Console.WriteLine($"> {actor[0]} and {actor[1]}");
                Console.WriteLine($"This database has {actor[0]} in {master.CountAdjacent(actor[0])} movie(s)");
                Console.WriteLine($"This database has {actor[1]} in {master.CountAdjacent(actor[1])} movie(s)\n" +
                                  $"There are {master.CountVertices() / 2} different sets of actors in this database \n");
                Console.WriteLine(Game(master, actor));
                Console.WriteLine("\n Would you like to restart? (Y/N)");
                string ans = Console.ReadLine();

                string[] tempNull = new string[0];
                if (args.Length > 1)
                {
                    args = tempNull;
                }
                if (ans.ToLower() == "y")
                    {
                        x = false; j = false;
                    }
            }
        }
        public static string Game(MathGraph<string> master, string[] actor)
        {

            List<string> y = master.FindShortestPath(actor[0], actor[1]);

            string returnPrompt = $"They are in a set with {master.CountVertices()} other movies and actors\n" +
                                  $"There are {y.Count} degrees of seperation between {actor[0]} and {actor[1]}\n" +
                                  $"{y[0]} was in {y[1]} with";
            for (int i = 2; i < y.Count; i++)
            {
                if (i % 2 == 0 && i!=y.Count-1)
                {
                    returnPrompt += $"{y[i]}. \n{y[i]} was in {y[i + 1]} with ";//I am stuck on this
                }
            }
            return returnPrompt;
        }
        public static MathGraph<string> CreateGraphFromFile(MathGraph<string> master, string file)
        {
            string text = File.ReadAllText(file);
            char[] splits = { '|', '\n', '\r' };
            int a;
            string[] words = text.Split(splits, StringSplitOptions.RemoveEmptyEntries);
            int documentWordCount = words.Length;
            for (int i = 0; i < documentWordCount; i++)
            {
                a = words[i].IndexOf('(');
                if (i % 2 == 0 && a > 0)
                {
                    words[i] = words[i].Substring(0, a-1);
                }
                if (i % 2 == 0 || i == 0)
                {
                    if (master.ContainsVertex(words[i])) { }
                    else
                    {
                        master.AddVertex(words[i]);
                    }
                }
                else
                {
                    if (master.ContainsVertex(words[i])) { master.AddEdge(words[i - 1], words[i]); }
                    else
                    {
                        master.AddVertex(words[i]);
                        master.AddEdge(words[i - 1], words[i]);
                    }
                }
            }
            return master;
        }
    }
}