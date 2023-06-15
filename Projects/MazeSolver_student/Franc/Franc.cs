using System;
using LinkedListLibrary;

namespace MazeSolver
{
    public class Breadcrumb
    {
        public char lastMove;
        public bool tryNorth = false;
        public bool trySouth = false;
        public bool tryEast = false;
        public bool tryWest = false;
        public bool isJunction;
        public Breadcrumb(char tLastMove, MazeSweep sweep, int hallCount)
        {
            lastMove = tLastMove;
            if (sweep.NorthView == MazeAgent.Hall && lastMove != 'S') { tryNorth = true; }
            if (sweep.SouthView == MazeAgent.Hall && lastMove != 'N') { trySouth = true; }
            if (sweep.EastView == MazeAgent.Hall && lastMove != 'W') { tryEast = true; }
            if (sweep.WestView == MazeAgent.Hall && lastMove != 'E') { tryWest = true; }
            isJunction = (hallCount > 2);
        }
    }
    public class Franc : MazeAgent
    {
        private int[] surr;
        private int count;
        private LinkedList<Breadcrumb> crumbs;
        private bool exploringMode;
        public Franc()
        {

            Nickname = "Frank Sinatra";
            Gliph = '∞';
            count = 0;
            surr = new int[5];

            crumbs = new LinkedList<Breadcrumb>();
        }

        public override string ToString()
        {
            return $"MazeAgent {Nickname}";
        }

        public override void Initialize(MazeSpace start, MazeSweep view)
        {

        }

        public int CountHallways(MazeSweep view)
        {

            if (view.NorthView == Wall) { surr[0] = 0; } else { surr[0] = 1; }
            if (view.EastView == Wall) { surr[1] = 0; } else { surr[1] = 1; }
            if (view.SouthView == Wall) { surr[2] = 0; } else { surr[2] = 1; }
            if (view.WestView == Wall) { surr[3] = 0; } else { surr[3] = 1; }
            surr[4] = surr[0] + surr[1] + surr[2] + surr[3];// Hallway Count
            if (surr[4] == 0) { throw new Exception("s t u c c, i     a m"); }
            return surr[4];

        }

        public char Turn(char lastMove, MazeSweep sweep)
        {
            if (lastMove == E)
            {
                if (sweep.NorthView == Hall) { return 'N'; }
                if (sweep.SouthView == Hall) { return 'S'; }
            }
            if (lastMove == W)
            {
                if (sweep.NorthView == Hall) { return 'N'; }
                if (sweep.SouthView == Hall) { return 'S'; }
            }
            if (lastMove == N)
            {
                if (sweep.EastView == Hall) { return 'E'; }
                if (sweep.WestView == Hall) { return 'W'; }
            }
            if (lastMove == S)
            {
                if (sweep.EastView == Hall) { return 'E'; }
                if (sweep.WestView == Hall) { return 'W'; }
            }

            return 'X';
        }

        public char MoveForeward(char lastMove, MazeSweep sweep)
        {
            if (lastMove == E && sweep.EastView == Hall) { return 'E'; }
            if (lastMove == N && sweep.NorthView == Hall) { return 'N'; }
            if (lastMove == S && sweep.SouthView == Hall) { return 'S'; }
            if (lastMove == W && sweep.WestView == Hall) { return 'W'; }
            else { return Turn(lastMove, sweep); }
        }
        public char MoveBackward(char lastMove, MazeSweep sweep)
        {
            Breadcrumb bc = crumbs.Pop();
            if (lastMove == N) { bc.tryNorth = false; }
            else if (lastMove == E) { bc.tryEast = false; }
            else if (lastMove == S) { bc.trySouth = false; }
            else if (lastMove == W) { bc.tryWest = false; }
            crumbs.Push(bc);
            if (lastMove == E && sweep.WestView == Hall) { return 'W'; }
            if (lastMove == S && sweep.NorthView == Hall) { return 'N'; }
            if (lastMove == N && sweep.SouthView == Hall) { return 'S'; }
            if (lastMove == W && sweep.EastView == Hall) { return 'E'; }
            else { return Turn(lastMove, sweep); }
        }
        public char TryNewRouteFromJunction(char lastMove, MazeSweep sweep, Breadcrumb tBc)
        {
            Breadcrumb bc = tBc;
            if (bc.tryEast) { bc.tryEast = false; crumbs.Push(bc); return 'E'; }
            else if (bc.tryNorth) { bc.tryNorth = false; crumbs.Push(bc); return 'N'; }
            else if (bc.trySouth) { bc.trySouth = false; crumbs.Push(bc); return 'S'; }
            else if (bc.tryWest) { bc.tryWest = false; crumbs.Push(bc); return 'W'; }
            exploringMode = false;
            return MoveBackward(bc.lastMove, sweep);
        }
        public override char NextMove(char lastMove, MazeSpace space, MazeSweep sweep)
        {
            int hallCount = CountHallways(sweep);

            if (sweep.NorthView == Finish) return N;
            if (sweep.SouthView == Finish) return S;
            if (sweep.EastView == Finish) return E;
            if (sweep.WestView == Finish) return W;

            if (count == 0)//move 1
            {
                exploringMode = true;
                crumbs.Push(new Breadcrumb(lastMove, sweep, hallCount));
                count++;
                return MoveForeward(lastMove, sweep);
            }

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
                        if (sweep.EastView == Hall && bc.tryEast) { bc.tryEast = false; crumbs.Push(bc); return 'E'; }
                        else if (sweep.NorthView == Hall && bc.tryNorth) { bc.tryNorth = false; crumbs.Push(bc); return 'N'; }
                        else if (sweep.SouthView == Hall && bc.trySouth) { bc.trySouth = false; crumbs.Push(bc); return 'S'; }
                        else { bc.tryWest = false; crumbs.Push(bc); return 'W'; }

                    }

                }
            }
            else //(!exploringMode)
            {
                Breadcrumb bc = crumbs.Pop();

                if (bc.isJunction)
                {
                    hallCount--;
                    exploringMode = true;
                    count++;
                    return TryNewRouteFromJunction(lastMove, sweep, bc);
                }


                count++;
                return MoveForeward(lastMove, sweep);



            }
            return 'X';
        }
    }
}
