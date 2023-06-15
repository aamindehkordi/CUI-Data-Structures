using System;

namespace MazeSolver
{
    /// <summary>
    /// Represents a space in the maze.
    /// </summary>
    public class MazeSpace
    {
        /// <summary>
        /// The X coordinate of the space.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y coordinate of the space.
        /// </summary>
        public int Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSpace"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the space.</param>
        /// <param name="y">The Y coordinate of the space.</param>
        public MazeSpace(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"({X},{Y})";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MazeSpace rhs = obj as MazeSpace;
            return (this == rhs);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="rhs">The object to compare with the current object.</param>
        public bool Equals(MazeSpace rhs)
        {
            // Check if rhs is null
            if (Object.ReferenceEquals(rhs, null))
            {
                return false;
            }

            // Check if rhs is the same object as this
            if (Object.ReferenceEquals(this, rhs))
            {
                return true;
            }

            // Check if rhs is of the same type as this
            if (this.GetType() != rhs.GetType())
            {
                return false;
            }

            // Check if the X and Y values of this and rhs are equal
            return (this.X == rhs.X && this.Y == rhs.Y);
        }
    }

    /// <summary>
    /// Represents a sweep of the maze, storing the views in each direction.
    /// </summary>
    public class MazeSweep
    {
        /// <summary>
        /// The view to the north of the agent.
        /// </summary>
        public char NorthView;

        /// <summary>
        /// The view to the south of the agent.
        /// </summary>
        public char SouthView;

        /// <summary>
        /// The view to the west of the agent.
        /// </summary>
        public char WestView;

        /// <summary>
        /// The view to the east of the agent.
        /// </summary>
        public char EastView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSweep"/> class.
        /// </summary>
        /// <param name="north">The view to the north of the agent.</param>
        /// <param name="south">The view to the south of the agent.</param>
        /// <param name="west">The view to the west of the agent.</param>
        /// <param name="east">The view to the east of the agent.</param>
        public MazeSweep(char north = MazeAgent.Wall, char south = MazeAgent.Wall, char west = MazeAgent.Wall, char east = MazeAgent.Wall)
        {
            NorthView = north;
            SouthView = south;
            WestView = west;
            EastView = east;
        }

        /// <summary>
        /// Returns a string representation of the agent's view of the maze.
        /// </summary>
        /// <returns>A string in the format "{N;S;W;E;}" representing the agent's view of the maze.</returns>
        public override string ToString()
        {
            // Initialize an empty string to hold the view
            string view = string.Empty;

            // Check each direction and add it to the view if it's a hall
            if (NorthView == MazeAgent.Hall) view += "N;";
            if (SouthView == MazeAgent.Hall) view += "S;";
            if (WestView == MazeAgent.Hall) view += "W;";
            if (EastView == MazeAgent.Hall) view += "E;";

            // Remove the trailing semicolon and return the view
            return $"{{{view.Substring(0, view.Length - 1)}}}";
        }
    }

    /// <summary>
    /// Abstract class representing an agent that can navigate a maze.
    /// </summary>
    public abstract class MazeAgent
    {
        // Constants representing the different directions and maze elements
        public const char N = 'N';
        public const char S = 'S';
        public const char W = 'W';
        public const char E = 'E';
        public const char Wall = 'X';
        public const char Hall = ' ';
        public const char Start = 'S';
        public const char Finish = 'F';
        public const char defaultGliph = '☺';

        // Fields that must be set in the constructor
        public char Gliph;
        public string Nickname;

        /// <summary>
        /// Initializes the agent with the starting space and the maze sweep.
        /// </summary>
        /// <param name="start">The starting space of the agent.</param>
        /// <param name="view">The maze sweep used by the agent to navigate the maze.</param>
        abstract public void Initialize(MazeSpace start, MazeSweep view);

        /// <summary>
        /// Determines the next move of the agent based on the last move, the current space, and the maze sweep.
        /// </summary>
        /// <param name="lastMove">The last move made by the agent.</param>
        /// <param name="space">The current space of the agent.</param>
        /// <param name="sweep">The maze sweep used by the agent to navigate the maze.</param>
        /// <returns>The next move of the agent.</returns>
        abstract public char NextMove(char lastMove, MazeSpace space, MazeSweep sweep);
    }
}
