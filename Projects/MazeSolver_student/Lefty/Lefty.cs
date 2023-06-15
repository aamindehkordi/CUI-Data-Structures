using System;

namespace MazeSolver
{
    public class Lefty : MazeAgent
    {
        public Lefty()
        {
            Nickname = "Lefty";
            Gliph = 'L';
        }

        public override string ToString()
        {
            return $"MazeAgent {Nickname}";
        }

        public override void Initialize(MazeSpace start, MazeSweep view)
        {
        }

        public override char NextMove(char lastMove, MazeSpace space, MazeSweep sweep)
        {
            // Try to turn right, which depends on our direction of travel
            // If right is not available, try straight...
            // If straight is not available, try left...
            // If all else fails, turn around and go backwards

            if (lastMove == W) // we are travelling WEST
            {

                if (sweep.NorthView == Finish)
                {
                    return N;
                }
                else if (sweep.WestView == Finish)
                {
                    return W;
                }
                else if (sweep.NorthView == Finish)
                {
                    return S;
                }
                else if (sweep.NorthView == Hall)
                {
                    return N;
                }
                else if (sweep.WestView == Hall)
                {
                    return W;
                }
                else if (sweep.SouthView == Hall)
                {
                    return S;
                }
                else if (sweep.EastView == Hall)
                {
                    return E;
                }
                else
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }

            else if (lastMove == E) // we are travellin EAST
            {
                if (sweep.SouthView == Finish)
                {
                    return S;
                }
                else if (sweep.EastView == Finish)
                {
                    return E;
                }
                else if (sweep.NorthView == Finish)
                {
                    return N;
                }
                else if (sweep.SouthView == Hall)
                {
                    return S;
                }
                else if (sweep.EastView == Hall)
                {
                    return E;
                }
                else if (sweep.NorthView == Hall)
                {
                    return N;
                }
                else if (sweep.WestView == Hall)
                {
                    return W;
                }
                else
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }

            else if (lastMove == N) // we are travelling NORTH
            {
                if (sweep.EastView == Finish)
                {
                    return E;
                }
                else if (sweep.NorthView == Finish)
                {
                    return N;
                }
                else if (sweep.WestView == Finish)
                {
                    return W;
                }
                else if (sweep.EastView == Hall)
                {
                    return E;
                }
                else if (sweep.NorthView == Hall)
                {
                    return N;
                }
                else if (sweep.WestView == Hall)
                {
                    return W;
                }
                else if (sweep.SouthView == Hall)
                {
                    return S;
                }
                else
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }

            else if (lastMove == S) // we are travelling SOUTH
            {
                if (sweep.WestView == Finish)
                {
                    return W;
                }
                else if (sweep.SouthView == Finish)
                {
                    return S;
                }
                else if (sweep.EastView == Finish)
                {
                    return E;
                }
                else if (sweep.WestView == Hall)
                {
                    return W;
                }
                else if (sweep.SouthView == Hall)
                {
                    return S;
                }
                else if (sweep.EastView == Hall)
                {
                    return E;
                }
                else if (sweep.NorthView == Hall)
                {
                    return N;
                }
                else
                {
                    throw new ArgumentException($"Agent error: I seem to be stuck {sweep}");
                }
            }

            else
            {
                throw new ArgumentOutOfRangeException($"Agent error: engine provided unknown last move {lastMove}. Expecting '{N}', '{S}', '{W}', or '{E}'");
            }
        }
    }
}
