using System;
using LinkedListLibrary;

namespace MazeSolver
{
    /// <summary>
    /// Represents a breadcrumb left by the maze agent.
    /// </summary>
    public class Breadcrumb
    {
        /// <summary>
        /// The direction the agent moved to leave this breadcrumb.
        /// </summary>
        public char lastMove;

        /// <summary>
        /// Whether the agent should try to move north from this breadcrumb.
        /// </summary>
        public bool tryNorth = false;

        /// <summary>
        /// Whether the agent should try to move south from this breadcrumb.
        /// </summary>
        public bool trySouth = false;

        /// <summary>
        /// Whether the agent should try to move east from this breadcrumb.
        /// </summary>
        public bool tryEast = false;

        /// <summary>
        /// Whether the agent should try to move west from this breadcrumb.
        /// </summary>
        public bool tryWest = false;

        /// <summary>
        /// Whether this breadcrumb represents a junction in the maze.
        /// </summary>
        public bool isJunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breadcrumb"/> class.
        /// </summary>
        /// <param name="tLastMove">The direction the agent moved to leave this breadcrumb.</param>
        /// <param name="sweep">The maze sweep containing information about the agent's surroundings.</param>
        /// <param name="hallCount">The number of hallways the agent can move through from this breadcrumb.</param>
        public Breadcrumb(char tLastMove, MazeSweep sweep, int hallCount)
        {
            lastMove = tLastMove;

            // Determine which directions the agent can move from this breadcrumb based on its surroundings.
            if (sweep.NorthView == MazeAgent.Hall && lastMove != 'S') { tryNorth = true; }
            if (sweep.SouthView == MazeAgent.Hall && lastMove != 'N') { trySouth = true; }
            if (sweep.EastView == MazeAgent.Hall && lastMove != 'W') { tryEast = true; }
            if (sweep.WestView == MazeAgent.Hall && lastMove != 'E') { tryWest = true; }

            // Determine whether this breadcrumb represents a junction in the maze.
            isJunction = (hallCount > 2);
        }
    }

    /// <summary>
    /// Represents an agent that can navigate a maze.
    /// </summary>
    public class Franc : MazeAgent
    {
        private int[] surr; // array to store the number of hallways in each direction
        private int count; // unused variable
        private LinkedList<Breadcrumb> crumbs; // list of breadcrumbs to track the agent's path
        private bool exploringMode; // unused variable

        /// <summary>
        /// Initializes a new instance of the <see cref="Franc"/> class.
        /// </summary>
        public Franc()
        {
            Nickname = "Frank Sinatra"; // set the agent's nickname
            Gliph = '∞'; // set the agent's glyph
            count = 0; // initialize the unused variable
            surr = new int[5]; // initialize the hallway count array
            crumbs = new LinkedList<Breadcrumb>(); // initialize the breadcrumb list
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"MazeAgent {Nickname}"; // return a string representation of the agent
        }

        /// <summary>
        /// Initializes the agent with the specified starting space and maze sweep.
        /// </summary>
        /// <param name="start">The starting space.</param>
        /// <param name="view">The maze sweep.</param>
        public override void Initialize(MazeSpace start, MazeSweep view)
        {
            // This method is currently empty
        }

        /// <summary>
        /// Counts the number of hallways in the agent's current view.
        /// </summary>
        /// <param name="view">The agent's current view.</param>
        /// <returns>The number of hallways in the agent's current view.</returns>
        public int CountHallways(MazeSweep view)
        {
            // Count the number of hallways in each direction
            if (view.NorthView == Wall) { surr[0] = 0; } else { surr[0] = 1; }
            if (view.EastView == Wall) { surr[1] = 0; } else { surr[1] = 1; }
            if (view.SouthView == Wall) { surr[2] = 0; } else { surr[2] = 1; }
            if (view.WestView == Wall) { surr[3] = 0; } else { surr[3] = 1; }
            surr[4] = surr[0] + surr[1] + surr[2] + surr[3]; // Calculate the total number of hallways
            if (surr[4] == 0) { throw new Exception("s t u c c, i     a m"); } // Throw an exception if the agent is stuck
            return surr[4]; // Return the total number of hallways
        }

        /// <summary>
        /// Determines the next move to make based on the last move and the current view of the maze.
        /// </summary>
        /// <param name="lastMove">The last move made.</param>
        /// <param name="sweep">The current view of the maze.</param>
        /// <returns>The next move to make.</returns>
        public char Turn(char lastMove, MazeSweep sweep)
        {
            // If the last move was east, check if there's a path to the north or south.
            if (lastMove == E)
            {
                if (sweep.NorthView == Hall) { return 'N'; }
                if (sweep.SouthView == Hall) { return 'S'; }
            }
            // If the last move was west, check if there's a path to the north or south.
            if (lastMove == W)
            {
                if (sweep.NorthView == Hall) { return 'N'; }
                if (sweep.SouthView == Hall) { return 'S'; }
            }
            // If the last move was north, check if there's a path to the east or west.
            if (lastMove == N)
            {
                if (sweep.EastView == Hall) { return 'E'; }
                if (sweep.WestView == Hall) { return 'W'; }
            }
            // If the last move was south, check if there's a path to the east or west.
            if (lastMove == S)
            {
                if (sweep.EastView == Hall) { return 'E'; }
                if (sweep.WestView == Hall) { return 'W'; }
            }

            // If there are no available paths, turn around.
            return 'X';
        }

        /// <summary>
        /// Moves the player forward in the direction of the last move, if possible.
        /// </summary>
        /// <param name="lastMove">The last move made.</param>
        /// <param name="sweep">The current view of the maze.</param>
        /// <returns>The next move to make.</returns>
        public char MoveForeward(char lastMove, MazeSweep sweep)
        {
            // If the last move was east and there's a path to the east, move east.
            if (lastMove == E && sweep.EastView == Hall) { return 'E'; }
            // If the last move was north and there's a path to the north, move north.
            if (lastMove == N && sweep.NorthView == Hall) { return 'N'; }
            // If the last move was south and there's a path to the south, move south.
            if (lastMove == S && sweep.SouthView == Hall) { return 'S'; }
            // If the last move was west and there's a path to the west, move west.
            if (lastMove == W && sweep.WestView == Hall) { return 'W'; }
            // If there are no available paths, turn around.
            else { return Turn(lastMove, sweep); }
        }
        /// <summary>
        /// Moves the player backward and returns the direction of the move.
        /// </summary>
        /// <param name="lastMove">The direction of the last move.</param>
        /// <param name="sweep">The MazeSweep object containing the current view of the maze.</param>
        /// <returns>The direction of the move.</returns>
        public char MoveBackward(char lastMove, MazeSweep sweep)
        {
            // Pop the last breadcrumb from the stack
            Breadcrumb bc = crumbs.Pop();

            // Set the corresponding try direction to false
            if (lastMove == N) { bc.tryNorth = false; }
            else if (lastMove == E) { bc.tryEast = false; }
            else if (lastMove == S) { bc.trySouth = false; }
            else if (lastMove == W) { bc.tryWest = false; }

            // Push the updated breadcrumb back to the stack
            crumbs.Push(bc);

            // Check if there is a hall in the opposite direction of the last move
            if (lastMove == E && sweep.WestView == Hall) { return 'W'; }
            if (lastMove == S && sweep.NorthView == Hall) { return 'N'; }
            if (lastMove == N && sweep.SouthView == Hall) { return 'S'; }
            if (lastMove == W && sweep.EastView == Hall) { return 'E'; }

            // If there is no hall in the opposite direction, turn and move backward
            else { return Turn(lastMove, sweep); }
        }

        /// <summary>
        /// Tries a new route from a junction and returns the direction of the move.
        /// </summary>
        /// <param name="lastMove">The direction of the last move.</param>
        /// <param name="sweep">The MazeSweep object containing the current view of the maze.</param>
        /// <param name="tBc">The breadcrumb object containing the information about the junction.</param>
        /// <returns>The direction of the move.</returns>
        public char TryNewRouteFromJunction(char lastMove, MazeSweep sweep, Breadcrumb tBc)
        {
            // Create a copy of the breadcrumb object
            Breadcrumb bc = tBc;

            // Try the next available direction
            if (bc.tryEast) { bc.tryEast = false; crumbs.Push(bc); return 'E'; }
            else if (bc.tryNorth) { bc.tryNorth = false; crumbs.Push(bc); return 'N'; }
            else if (bc.trySouth) { bc.trySouth = false; crumbs.Push(bc); return 'S'; }
            else if (bc.tryWest) { bc.tryWest = false; crumbs.Push(bc); return 'W'; }

            // If there is no available direction, move backward
            exploringMode = false;
            return MoveBackward(bc.lastMove, sweep);
        }
        /// <summary>
        /// Determines the next move to make in the maze.
        /// </summary>
        /// <param name="lastMove">The last move made.</param>
        /// <param name="space">The current space in the maze.</param>
        /// <param name="sweep">The current view of the maze.</param>
        /// <returns>The next move to make.</returns>
        public override char NextMove(char lastMove, MazeSpace space, MazeSweep sweep)
        {
            // Count the number of hallways in the current view
            int hallCount = CountHallways(sweep);

            // If the finish is in view, move towards it
            if (sweep.NorthView == Finish) return N;
            if (sweep.SouthView == Finish) return S;
            if (sweep.EastView == Finish) return E;
            if (sweep.WestView == Finish) return W;

            // If this is the first move, start exploring
            if (count == 0)
            {
                exploringMode = true;
                crumbs.Push(new Breadcrumb(lastMove, sweep, hallCount));
                count++;
                return MoveForeward(lastMove, sweep);
            }

            // If in exploring mode, continue exploring
            if (exploringMode)
            {
                Breadcrumb bc = new Breadcrumb(lastMove, sweep, hallCount);
                if (hallCount == 1)
                {//dead end
                    exploringMode = false;
                    return MoveBackward(lastMove, sweep);
                }
                else
                {
                    if (hallCount == 2)//2-way 
                    {
                        crumbs.Push(bc);
                        return MoveForeward(lastMove, sweep);
                    }
                    if (hallCount > 2)//3-way 
                    {
                        //Make a note of the Intersection
                        crumbs.Push(bc);
                        return MoveForeward(lastMove, sweep);
                    }
                }
            }
            else //(!exploringMode) // If we're not in exploring mode
            {
                Breadcrumb bc = crumbs.Pop(); // Pop the last breadcrumb from the stack

                if (bc.isJunction) // If the breadcrumb is a junction
                {
                    hallCount--; // Decrement the hall count
                    exploringMode = true; // Set exploring mode to true
                    count++; // Increment the count
                    return TryNewRouteFromJunction(lastMove, sweep, bc); // Try a new route from the junction
                }

                count++; // Increment the count
                return MoveForeward(lastMove, sweep); // Move forward
            }

            return 'X'; // Return 'X' if none of the above conditions are met
        }
    }
}
