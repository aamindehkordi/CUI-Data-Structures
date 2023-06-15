using GraphLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace KevinBaconGame
{
    class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            // Print a message to the console.
            Console.WriteLine("Creating a graph from database");

            // Create a new MathGraph object.
            MathGraph<string> master = new MathGraph<string>("Master Film List");

            // Create a new Stopwatch object.
            Stopwatch stopwatch = new Stopwatch();

            // Set the file name.
            string file = "top250.txt";

            // Start the stopwatch.
            stopwatch.Restart();

            // Create the graph from the file.
            CreateGraphFromFile(master, file);

            // Stop the stopwatch.
            stopwatch.Stop();

            // Initialize a boolean variable.
            bool x = false;

            // Loop until the user chooses to exit.
            while (!x)
            {
                // Set the default value of the boolean variable.
                x = true;

                // Initialize an array to store the actor names.
                string[] actor = new string[2];

                // Initialize a boolean variable.
                bool j = false;

                // Check if the command-line arguments were provided.
                if (args.Length != 0)
                {
                    // Set the actor names from the command-line arguments.
                    actor[0] = args[0];
                    actor[1] = args[1];
                }
                else
                {
                    // Loop until the user enters valid actor names.
                    while (!j)
                    {
                        // Clear the console.
                        Console.Clear();

                        // Prompt the user to enter the actor names.
                        Console.WriteLine("Enter two names of actors inclosed in quotes, Case Matters\n" +
                            "For example: Kevin Bacon or Harrison Ford");
                        Console.Write("Actor(ress) 1:");
                        actor[0] = Console.ReadLine();

                        // Check if the actor name contains any digits.
                        foreach (char c in actor[0])
                        {
                            if (c < 0 || c > 9) { j = true; } else { j = false; }
                        }

                        // Prompt the user to enter the second actor name.
                        Console.Write("Actor(ress) 2:");
                        actor[1] = Console.ReadLine();

                        // Check if the actor name contains any digits.
                        foreach (char c in actor[1])
                        {
                            if (c < 0 || c > 9) { j = true; } else { j = false; }
                        }

                        // Clear the console.
                        Console.Clear();
                    }
                }

                // Print the file loading time to the console.
                Console.WriteLine($"Loaded {file} in {stopwatch.Elapsed}");

                // Print the actor names to the console.
                Console.WriteLine($"> {actor[0]} and {actor[1]}");

                // Print the number of movies for the first actor to the console.
                Console.WriteLine($"This database has {actor[0]} in {master.CountAdjacent(actor[0])} movie(s)");

                // Print the number of movies for the second actor to the console.
                Console.WriteLine($"This database has {actor[1]} in {master.CountAdjacent(actor[1])} movie(s)\n" +
                                $"There are {master.CountVertices() / 2} different sets of actors in this database \n");

                // Print the game result to the console.
                Console.WriteLine(Game(master, actor));

                // Prompt the user to restart the program.
                Console.WriteLine("\n Would you like to restart? (Y/N)");
                string ans = Console.ReadLine();

                // Reset the command-line arguments.
                string[] tempNull = new string[0];
                if (args.Length > 1)
                {
                    args = tempNull;
                }

                // Check if the user wants to restart the program.
                if (ans.ToLower() == "y")
                {
                    x = false; j = false;
                }
            }
        }

        /// <summary>
        /// Returns a string containing information about the shortest path between two actors/movies in a given graph.
        /// </summary>
        /// <param name="master">The graph to search for the shortest path.</param>
        /// <param name="actor">An array containing the names of the two actors/movies to find the shortest path between.</param>
        /// <returns>A string containing information about the shortest path between two actors/movies in a given graph.</returns>
        public static string Game(MathGraph<string> master, string[] actor)
        {
            // Find the shortest path between the two actors/movies.
            List<string> y = master.FindShortestPath(actor[0], actor[1]);

            // Build the return string.
            string returnPrompt = $"They are in a set with {master.CountVertices()} other movies and actors\n" +
                                $"There are {y.Count} degrees of seperation between {actor[0]} and {actor[1]}\n" +
                                $"{y[0]} was in {y[1]} with";
            for (int i = 2; i < y.Count; i++)
            {
                if (i % 2 == 0 && i != y.Count - 1)
                {
                    returnPrompt += $"{y[i]}. \n{y[i]} was in {y[i + 1]} with ";//I am stuck on this
                }
            }
            return returnPrompt;
        }

        /// <summary>
        /// Creates a graph from a file containing a list of actors/movies and the connections between them.
        /// </summary>
        /// <param name="master">The graph to add the actors/movies and connections to.</param>
        /// <param name="file">The path to the file containing the list of actors/movies and connections.</param>
        /// <returns>The updated graph.</returns>
        public static MathGraph<string> CreateGraphFromFile(MathGraph<string> master, string file)
        {
            // Read the file contents.
            string text = File.ReadAllText(file);

            // Define the characters to split the file contents by.
            char[] splits = { '|', '\n', '\r' };

            // Split the file contents into an array of words.
            string[] words = text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

            // Get the total number of words in the file.
            int documentWordCount = words.Length;

            // Loop through each word in the file.
            for (int i = 0; i < documentWordCount; i++)
            {
                // If the word is an actor/movie name, remove any parentheses from the end of the name.
                int a = words[i].IndexOf('(');
                if (i % 2 == 0 && a > 0)
                {
                    words[i] = words[i].Substring(0, a - 1);
                }

                // If the word is an actor/movie name, add it to the graph if it doesn't already exist.
                if (i % 2 == 0 || i == 0)
                {
                    if (master.ContainsVertex(words[i])) { }
                    else
                    {
                        master.AddVertex(words[i]);
                    }
                }
                // If the word is a connection, add it to the graph if it doesn't already exist.
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