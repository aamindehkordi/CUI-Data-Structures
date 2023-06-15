using System;

namespace MazeSolver
{
    /// <summary>
    /// Represents an agent that always tries to turn right, then go straight, then turn left, and finally turn around and go backwards if all else fails.
    /// </summary>
    public class Lefty : MazeAgent
    {
        public Lefty()
        {
            Nickname = "Lefty";
            Gliph = 'L';
        }

        /// <summary>
        /// Returns a string representation of the maze agent.
        /// </summary>
        /// <returns>A string representation of the maze agent.</returns>
        public override string ToString()
        {
            return $"MazeAgent {Nickname}";
        }

        /// <summary>
        /// Initializes the maze agent with a starting space and a sweep object.
        /// </summary>
        /// <param name="start">The starting space.</param>
        /// <param name="view">The sweep object.</param>
        public override void Initialize(MazeSpace start, MazeSweep view)
        {
            // This agent does not need any initialization
        }

        /// <summary>
        /// Returns the next move for the maze agent based on the last move, the current space, and the sweep object.
        /// </summary>
        /// <param name="lastMove">The last move made by the maze agent.</param>
        /// <param name="space">The current space.</param>
        /// <param name="sweep">The sweep object.</param>
        /// <returns>The next move for the maze agent.</returns>
        public override char NextMove(char lastMove, MazeSpace space, MazeSweep sweep)
        {
            // Try to turn right, which depends on our direction of travel
            // If right is not available, try straight...
            // If straight is not available, try left...
            // If all else fails, turn around and go backwards

            if (lastMove == W) // we are traveling WEST
            {
                if (sweep.NorthView == Finish) // if we can see the finish to the north, go north
                {
                    return N;
                }
                else if (sweep.WestView == Finish) // if we can see the finish to the west, go west
                {
                    return W;
                }
                else if (sweep.NorthView == Finish) // if we can see the finish to the north, go north
                {
                    return S;
                }
                else if (sweep.NorthView == Hall) // if we can see a hall to the north, go north
                {
                    return N;
                }
                else if (sweep.WestView == Hall) // if we can see a hall to the west, go west
                {
                    return W;
                }
                else if (sweep.SouthView == Hall) // if we can see a hall to the south, go south
                {
                    return S;
                }
                else if (sweep.EastView == Hall) // if we can see a hall to the east, go east
                {
                    return E;
                }
                else // if we can't see anything, we're stuck
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }
            else if (lastMove == E) // we are traveling EAST
            {
                if (sweep.SouthView == Finish) // if we can see the finish to the south, go south
                {
                    return S;
                }
                else if (sweep.EastView == Finish) // if we can see the finish to the east, go east
                {
                    return E;
                }
                else if (sweep.NorthView == Finish) // if we can see the finish to the north, go north
                {
                    return N;
                }
                else if (sweep.SouthView == Hall) // if we can see a hall to the south, go south
                {
                    return S;
                }
                else if (sweep.EastView == Hall) // if we can see a hall to the east, go east
                {
                    return E;
                }
                else if (sweep.NorthView == Hall) // if we can see a hall to the north, go north
                {
                    return N;
                }
                else if (sweep.WestView == Hall) // if we can see a hall to the west, go west
                {
                    return W;
                }
                else // if we can't see anything, we're stuck
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }
            else if (lastMove == N) // we are traveling NORTH
            {
                if (sweep.EastView == Finish) // if we can see the finish to the east, go east
                {
                    return E;
                }
                else if (sweep.NorthView == Finish) // if we can see the finish to the north, go north
                {
                    return N;
                }
                else if (sweep.WestView == Finish) // if we can see the finish to the west, go west
                {
                    return W;
                }
                else if (sweep.EastView == Hall) // if we can see a hall to the east, go east
                {
                    return E;
                }
                else if (sweep.NorthView == Hall) // if we can see a hall to the north, go north
                {
                    return N;
                }
                else if (sweep.WestView == Hall) // if we can see a hall to the west, go west
                {
                    return W;
                }
                else if (sweep.SouthView == Hall) // if we can see a hall to the south, go south
                {
                    return S;
                }
                else // if we can't see anything, we're stuck
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }
            else if (lastMove == S) // we are traveling SOUTH
            {
                if (sweep.WestView == Finish) // if we can see the finish to the west, go west
                {
                    return W;
                }
                else if (sweep.SouthView == Finish) // if we can see the finish to the south, go south
                {
                    return S;
                }
                else if (sweep.EastView == Finish) // if we can see the finish to the east, go east
                {
                    return E;
                }
                else if (sweep.WestView == Hall) // if we can see a hall to the west, go west
                {
                    return W;
                }
                else if (sweep.SouthView == Hall) // if we can see a hall to the south, go south
                {
                    return S;
                }
                else if (sweep.EastView == Hall) // if we can see a hall to the east, go east
                {
                    return E;
                }
                else if (sweep.NorthView == Hall) // if we can see a hall to the north, go north
                {
                    return N;
                }
                else // if we can't see anything, we're stuck
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }
            else // if we don't know what direction we're travelling, we're stuck
            {
                throw new ArgumentOutOfRangeException($"Agent error: engine provided unknown last move {lastMove}. Expecting '{N}', '{S}', '{W}', or '{E}'");
            }
        }
    }
}
