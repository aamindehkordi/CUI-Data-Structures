using System;
using System.IO;
using System.Collections.Generic;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the agent... from a library file

            MazeAgent agent = null;
            if (args.Length > 0)
            {
                string agentPath = Path.GetFullPath(args[0]);
                if (File.Exists(agentPath))
                {
                    agent = MazeAgentLoader.LoadSingleAgent(agentPath);
                }
                else
                {
                    Console.WriteLine($"You tried to load maze agent '{agentPath}' but the file does not exist");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error: not enough arguments, try again.");
                Console.WriteLine("MazeAgent <agent.dll> [map.txt]");
                return;
            }

            // Get the maze... either from a file or a random maze

            MazeEngine maze = null;
            if (args.Length > 1)
            {
                string mazePath = Path.GetFullPath(args[1]);
                if (File.Exists(mazePath))
                {
                    maze = new MazeEngine(mazePath);
                }
                else
                {
                    Console.WriteLine($"You tried to load maze '{mazePath}' but the file does not exist");
                }
            }
            else
            {
                maze = new MazeEngine(50, 30, false, false);
            }

            // Play the maze

            try
            {
                Console.Clear();
                Console.WriteLine($"{agent.Nickname}");
                maze.DrawMaze(0, 2);
                maze.PlayMaze(agent);
            }
            catch(InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            catch (InvalidDataException ide)
            {
                Console.WriteLine(ide.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
        }
    }
}