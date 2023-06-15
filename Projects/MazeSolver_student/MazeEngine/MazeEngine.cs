using System;
using System.IO;
using System.Collections.Generic;
/* Professor Tallman's Code */
namespace MazeSolver
{
    public class MazeEngine
    {
        //private const char wallGliph = '█';
        private const char wallGliph = '\u2588';
        private const char hallGliph = ' ';
        private const char startGliph = 'S';
        private const char finishGliph = 'F';
        private char playerGliph;
        private char[,] maze;
        private int top;
        private int left;
        private int width;
        private int height;
        private int moveCount;
        private MazeSpace start;
        private MazeSpace finish;
        private MazeSpace player;
        private bool validMazeLoaded;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public void Initialize()
        {
            validMazeLoaded = false;
            playerGliph = MazeAgent.defaultGliph;
            player = null;
            finish = null;
            start = null;
            height = 0;
            width = 0;
            left = 0;
            top = 0;
            maze = null;
        }

        public MazeEngine()
        {
            Initialize();
        }

        public MazeEngine(string filename)
        {
            Initialize();
            this.maze = LoadMaze(filename);
            this.width = maze.GetLength(0);
            this.height = maze.GetLength(1);
            this.validMazeLoaded = SetStartAndFinish();
        }

        public MazeEngine(int width, int height, bool loops = false, bool show = true)
        {
            Initialize();
            this.maze = GenerateRandomMaze(width, height, loops, show);
            this.width = width;
            this.height = height;
            this.validMazeLoaded = SetStartAndFinish();
        }

        private bool SetStartAndFinish()
        {
            int found = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze[x, y] == 'S')
                    {
                        start = new MazeSpace(x, y);
                        found++;
                    }
                    else if (maze[x, y] == MazeAgent.Finish)
                    {
                        finish = new MazeSpace(x, y);
                        found++;
                    }
                    if (found == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static private char[,] CreateAllWalls(int width, int height, bool show)
        {
            char[,] map = new char[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = MazeAgent.Wall;
                    if (show)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(wallGliph);
                    }
                }
            }
            return map;
        }

        static public char[,] GenerateRandomMaze(int width = 10, int height = 10, bool loops = false, bool show = true)
        {
            Stack<MazeSpace> stack = new Stack<MazeSpace>();
            Random rng = new Random();

            //Console.CursorVisible = false;
            char[,] map = CreateAllWalls(width, height, show);

            // Create the starting point somewhere along the west wall
            MazeSpace start = new MazeSpace(0, rng.Next(1, height - 1));
            map[start.X, start.Y] = MazeAgent.Start;
            if (show)
            {
                Console.SetCursorPosition(start.X, start.Y);
                Console.Write(startGliph);
            }

            // Player immediately moves east from the starting point
            map[start.X + 1, start.Y] = MazeAgent.Hall;
            if (show)
            {
                Console.SetCursorPosition(start.X + 1, start.Y);
                Console.Write(hallGliph);
            }

            // Randomly generate maze from this point forward
            stack.Push(new MazeSpace(start.X + 1, start.Y));
            while (stack.Count > 0)
            {
                MazeSpace curr = stack.Pop();
                if (show)
                {
                    Console.SetCursorPosition(curr.X, curr.Y);
                    Console.Write('X');
                    System.Threading.Thread.Sleep(50);
                }

                // Start by generating the coordinates of the spaces around us (NSEW)
                // These might not be valid moves, we'll figure that out later
                MazeSpace north = new MazeSpace(curr.X, curr.Y - 1);
                MazeSpace south = new MazeSpace(curr.X, curr.Y + 1);
                MazeSpace east = new MazeSpace(curr.X + 1, curr.Y);
                MazeSpace west = new MazeSpace(curr.X - 1, curr.Y);

                // Check the four diagonal corners around us. If any of them are a
                // hallway, then we run the danger of creating "rooms". To avoid this
                // problem, we mark those spaces (NSEW) as invalid
                if (map[curr.X - 1, curr.Y - 1] == MazeAgent.Hall)
                {
                    north = null;
                    west = null;
                }
                if (map[curr.X - 1, curr.Y + 1] == MazeAgent.Hall)
                {
                    west = null;
                    south = null;
                }
                if (map[curr.X + 1, curr.Y - 1] == MazeAgent.Hall)
                {
                    north = null;
                    east = null;
                }
                if (map[curr.X + 1, curr.Y + 1] == MazeAgent.Hall)
                {
                    east = null;
                    south = null;
                }

                // Check the four spaces NSEW around us. If any of them are:
                //   a) Part of the outside border
                //   b) Already a hallway
                //   c) Will create an *unwanted* loop by connecting to another hallway
                // then we mark those spaces (NSEW) as invalid
                if (curr.X <= 1 || map[curr.X - 1, curr.Y] == MazeAgent.Hall ||
                    (!loops && map[curr.X - 2, curr.Y] == MazeAgent.Hall))
                {
                    west = null;
                }
                if (curr.X >= width - 2 || map[curr.X + 1, curr.Y] == MazeAgent.Hall ||
                    (!loops && map[curr.X + 2, curr.Y] == MazeAgent.Hall))
                {
                    east = null;
                }
                if (curr.Y <= 1 || map[curr.X, curr.Y - 1] == MazeAgent.Hall ||
                    (!loops && map[curr.X, curr.Y - 2] == MazeAgent.Hall))
                {
                    north = null;
                }
                if (curr.Y >= height - 2 || map[curr.X, curr.Y + 1] == MazeAgent.Hall ||
                    (!loops && map[curr.X, curr.Y + 2] == MazeAgent.Hall))
                {
                    south = null;
                }

                // Create an array of whichever four spaces NSEW still remain valid
                int count = 0;
                MazeSpace[] options = new MazeSpace[4];
                if (north != null)
                {
                    options[count++] = north;
                }
                if (south != null)
                {
                    options[count++] = south;
                }
                if (east != null)
                {
                    options[count++] = east;
                }
                if (west != null)
                {
                    options[count++] = west;
                }

                // If there is at least one valid moves from here, continue on...
                if (count > 0)
                {
                    // ...randomly choose one of the valid moves
                    MazeSpace next = options[rng.Next(count)];
                    map[next.X, next.Y] = MazeAgent.Hall;
                    if (show)
                    {
                        Console.SetCursorPosition(next.X, next.Y);
                        Console.Write(hallGliph);
                    }

                    // Push the new current space and the next move onto the stack
                    stack.Push(curr);
                    stack.Push(next);
                }

                // This is for the animation
                if (show)
                {
                    Console.SetCursorPosition(curr.X, curr.Y);
                    Console.WriteLine(' ');
                }
            }

            // Create the finish point somewhere along the east wall (it has to be reachable)
            MazeSpace finish = new MazeSpace(width - 1, rng.Next(1, height - 1));
            while (map[finish.X - 1, finish.Y] != MazeAgent.Hall)
            {
                finish = new MazeSpace(width - 1, rng.Next(1, height - 1));

            }
            map[finish.X, finish.Y] = MazeAgent.Finish;
            if (show)
            {
                Console.SetCursorPosition(finish.X, finish.Y);
                Console.Write(finishGliph);
            }

            return map;
        }

        static public char[,] LoadMaze(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            int height = lines.Length;
            if (height < 5)
            {
                throw new InvalidDataException($"MazeEngine: height is {height}. Maze height must be at least 5.");
            }

            int width = lines[0].Length;
            if (width < 5)
            {
                throw new InvalidDataException($"MazeEngine: width is {width}. Maze width must be at least 5.");
            }

            char[,] maze = new char[width, height];
            int startCount = 0;
            int finishCount = 0;
            MazeSpace start;
            MazeSpace finish;

            for (int y = 0; y < lines.Length; y++)
            {
                if (lines[y].Length != width)
                {
                    throw new InvalidDataException($"MazeEngine: width of line 1 is {width} and width of line {y + 1} is {lines[y].Length}. Maze width must be consistent.");
                }

                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        maze[x, y] = MazeAgent.Start;
                        start = new MazeSpace(x, y);

                        startCount++;
                        if (startCount > 1)
                        {
                            throw new InvalidDataException($"MazeEngine: start space at character ({x + 1},{y + 1}) and ({start.X + 1},{start.Y + 1}). Only one start is allowed.");
                        }
                    }
                    else if (lines[y][x] == 'F')
                    {
                        maze[x, y] = MazeAgent.Finish;
                        finish = new MazeSpace(x, y);

                        finishCount++;
                        if (finishCount > 1)
                        {
                            throw new InvalidDataException($"MazeEngine: finish space at character ({x + 1},{y + 1}) and ({finish.X + 1},{finish.Y + 1}). Only one finish is allowed.");
                        }
                    }
                    else if (lines[y][x] == MazeAgent.Wall)
                    {
                        maze[x, y] = MazeAgent.Wall;
                    }
                    else if (lines[y][x] == MazeAgent.Hall)
                    {
                        maze[x, y] = MazeAgent.Hall;
                    }
                    else
                    {
                        throw new InvalidDataException($"MazeEngine: invalid character '{lines[y][x]}'. Maze contents must be either 'X' for wall or ' ' for passages with a single start 'S' and a single finish 'F'.");
                    }

                    if ((x == 0 || x == width - 1) && lines[y][x] == ' ')
                    {
                        throw new InvalidDataException($"MazeEngine: maze is not enclosed at character ({x + 1},{y + 1}). Maze perimeter must be solid walls with one start space and one finish space.");
                    }
                }
            }

            if (startCount < 1)
            {
                throw new InvalidDataException($"MazeEngine: maze has no start square. Maze requires one start space 'S' on the left side.");
            }

            if (finishCount < 1)
            {
                throw new InvalidDataException($"MazeEngine: maze has no finish square. Maze requires one finish space 'F' on the right side.");
            }

            return maze;
        }

        static private string[] wallMessages = {
            "With much confidence, you have walked into a wall.",
            "Thud.",
            "What 'e lacks in brains 'e makes up for in courage.",
            "Ouch!",
            "The stars came out early and they're spinning fast.",
            "Would you please knock that off!?",
            "Wall 1 - Player 0",
            "But ay cannot a go dat way cap'n.",
            "Bumped his head, went to bed, couldn't get up in the morning"
        };

        static public string GetWallMessage()
        {
            Random rng = new Random();
            return wallMessages[rng.Next(wallMessages.Length)];
        }


        private char GetMazeGliph(int x, int y)
        {
            if (maze[x, y] == MazeAgent.Hall)
            {
                return hallGliph;
            }
            else if (maze[x, y] == MazeAgent.Wall)
            {
                return wallGliph;
            }
            else if (maze[x, y] == MazeAgent.Start)
            {
                return startGliph;
            }
            else if (maze[x, y] == MazeAgent.Finish)
            {
                return finishGliph;
            }
            else
            {
                throw new InvalidDataException($"Invalize maze: maze has unknown space {maze[x, y]}. Maze contents must be either 'X' for wall or ' ' for passages with a single start 'S' and a single finish 'F'.");
            }
        }

        public void DrawMaze(int left = 0, int top = 0)
        {
            this.top = top;
            this.left = left;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(left + x, top + y);
                    Console.Write(GetMazeGliph(x, y));
                }
            }
            Console.SetCursorPosition(left, top + height);
        }

        private void ClearPlayer()
        {
            Console.SetCursorPosition(left + player.X, top + player.Y);
            Console.Write(GetMazeGliph(player.X, player.Y));
        }

        private void DrawPlayer()
        {
            Console.SetCursorPosition(left + player.X, top + player.Y);
            Console.Write(playerGliph);
        }

        private MazeSweep GetSurroundings(int x, int y)
        {
            MazeSweep view = new MazeSweep();
            if (x > 0) view.WestView = maze[x - 1, y];
            if (x < width - 1) view.EastView = maze[x + 1, y];
            if (y > 0) view.NorthView = maze[x, y - 1];
            if (y < height - 1) view.SouthView = maze[x, y + 1];

            return view;
        }

        private MazeSweep GetSurroundings()
        {
            return GetSurroundings(player.X, player.Y);
        }

        private MazeSweep MovePlayer(int deltaX, int deltaY)
        {
            moveCount++;

            int newX = player.X + deltaX;
            int newY = player.Y + deltaY;
            if (newY >= 0 && newY < height && newX >= 0 && newX < width && maze[newX, newY] != MazeAgent.Wall)
            {
                ClearPlayer();
                player.X += deltaX;
                player.Y += deltaY;
                DrawPlayer();
                Console.SetCursorPosition(left, top + height);
            }
            else
            {
                Console.WriteLine(GetWallMessage());
            }

            return GetSurroundings();
        }

        public MazeSweep MovePlayer(char direction)
        {
            switch (direction)
            {
                case MazeAgent.N: return MovePlayer(0, -1);
                case MazeAgent.S: return MovePlayer(0, 1);
                case MazeAgent.W: return MovePlayer(-1, 0);
                case MazeAgent.E: return MovePlayer(1, 0);
                default: return GetSurroundings();
            }
        }

        private bool PlayerFoundExit()
        {
            return (player.X == finish.X && player.Y == finish.Y);
        }

        private MazeSweep InsertAgentIntoMaze(char gliph)
        {
            moveCount = 0;

            if (!validMazeLoaded)
            {
                throw new InvalidOperationException($"MazeEngine: no maze has been loaded.");
            }

            player = new MazeSpace(start.X, start.Y);
            playerGliph = gliph;
            //Console.CursorVisible = false;
            DrawPlayer();
            Console.SetCursorPosition(left, top + height);

            return GetSurroundings();
        }

        public int PlayMaze(MazeAgent agent, int msDelay = 100)
        {
            // Position the player at the starting point in the maze and then
            // give pass the surroundings (Up, Down, Left, Right) to the player
            MazeSweep view = InsertAgentIntoMaze(agent.Gliph);
            agent.Initialize(start, view);

            // The first move into the starting point is arbitrarily 'Right'
            // We allow 4 times the number of moves as there are maze squares
            char move = 'E';
            int maxMoves = 4 * width * height;
            while (moveCount < maxMoves)
            {
                // Let the agent make their next move (move Up, Down, Left, or 
                // Right) and then play out that move on the board.
                move = agent.NextMove(move, player, view);
                view = MovePlayer(move);

                // Check if the player has found a way out
                if (PlayerFoundExit())
                {
                    Console.WriteLine($"Congratulations, you have escaped from the maze in {moveCount} moves");
                    return moveCount;
                }

                System.Threading.Thread.Sleep(msDelay);
            }

            //Console.CursorVisible = true;
            return moveCount;
        }
    }

}
