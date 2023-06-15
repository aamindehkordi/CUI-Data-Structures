using System;

namespace MazeSolver
{
    public class MazeSpace
    {
        public int X;
        public int Y;

        public MazeSpace(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override bool Equals(object obj)
        {
            MazeSpace rhs = obj as MazeSpace;
            return (this == rhs);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public bool Equals(MazeSpace rhs)
        {
            if (Object.ReferenceEquals(rhs, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, rhs))
            {
                return true;
            }
            if (this.GetType() != rhs.GetType())
            {
                return false;
            }
            return (this.X == rhs.X && this.Y == rhs.Y);
        }
    }

    public class MazeSweep
    {
        public char NorthView;
        public char SouthView;
        public char WestView;
        public char EastView;

        public MazeSweep(char north=MazeAgent.Wall, char south=MazeAgent.Wall, char west=MazeAgent.Wall, char east=MazeAgent.Wall)
        {
            NorthView = north;
            SouthView = south;
            WestView = west;
            EastView = east;
        }

        public override string ToString()
        {
            string view = string.Empty;
            if (NorthView == MazeAgent.Hall) view += "N;";
            if (SouthView == MazeAgent.Hall) view += "S;";
            if (WestView == MazeAgent.Hall) view += "W;";
            if (EastView == MazeAgent.Hall) view += "E;";
            return $"{{{view.Substring(0, view.Length-1)}}}";
        }
    }

    public abstract class MazeAgent
    {
        public const char N = 'N';
        public const char S = 'S';
        public const char W = 'W';
        public const char E = 'E';
        public const char Wall = 'X';
        public const char Hall = ' ';
        public const char Start = 'S';
        public const char Finish = 'F';
        public const char defaultGliph = '☺';

        public char Gliph;         // these must be set in the constructor
        public string Nickname;    // these must be set in the constructor

        abstract public void Initialize(MazeSpace start, MazeSweep view);

        abstract public char NextMove(char lastMove, MazeSpace space, MazeSweep sweep);
    }
}
