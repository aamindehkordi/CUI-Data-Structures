namespace MazeSolver
{
    public class CountVonCount : MazeAgent
    {
        private byte[,] map;
        private int width;
        private int height;

        public CountVonCount()
        {
            Nickname = "CountVonCount";
            Gliph = 'C';
        }

        public override string ToString()
        {
            return $"MazeAgent {Nickname}";
        }

        public override void Initialize(MazeSpace start, MazeSweep view)
        {
            // Initialize our internal map grid
            // On average, we should start in the middle of the maze...
            // So create the map to be twice as large as our current position

            width = 2*(start.X+1);
            height = 2*(start.Y+1);
            map = new byte[width, height];
            map[start.X, start.Y] = byte.MaxValue;
        }

        private byte[,] ResizeMap(int newWidth, int newHeight)
        {
            // Our map is too small to contain the maze, make it bigger
            // and copy the old map into the new one

            byte[,] biggerMap = new byte[newWidth, newHeight];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    biggerMap[x, y] = map[x, y];
                }
            }

            return biggerMap;
        }

        private void AddWallsToSurroundingSpaces(MazeSpace currentSpace, MazeSweep currentSweep)
        {
            // If space to my NSEW is a wall or the start, give it the max count

            if (currentSpace.Y - 1 >= 0 && (currentSweep.NorthView == Wall || currentSweep.NorthView == Start))
            {
                map[currentSpace.X, currentSpace.Y - 1] = byte.MaxValue;
            }
            if (currentSpace.Y + 1 < height && (currentSweep.SouthView == Wall || currentSweep.SouthView == Start))
            {
                map[currentSpace.X, currentSpace.Y + 1] = byte.MaxValue;
            }
            if (currentSpace.X + 1 < width && (currentSweep.EastView == Wall || currentSweep.EastView == Start))
            {
                map[currentSpace.X + 1, currentSpace.Y] = byte.MaxValue;
            }
            if (currentSpace.X - 1 >= 0 && (currentSweep.WestView == Wall || currentSweep.WestView == Start))
            {
                map[currentSpace.X - 1, currentSpace.Y] = byte.MaxValue;
            }
        }


        public override char NextMove(char lastMove, MazeSpace space, MazeSweep sweep)
        {
            // If we're about to move off the current map, resize it appropriately

            if (space.X+1 >= width)
            {
                map = ResizeMap(2*width, height);
                width = 2*width;
            }
            if (space.Y+1 >= height)
            {
                map = ResizeMap(width, 2*height); ;
                height = 2*height;
            }

            // Take care of this current space on the map

            map[space.X, space.Y]++;
            AddWallsToSurroundingSpaces(space, sweep);

            // Check for the finish

            if (sweep.NorthView == Finish) return N;
            if (sweep.SouthView == Finish) return S;
            if (sweep.EastView == Finish) return E;
            if (sweep.WestView == Finish) return W;

            // Get the lowest count value between all four directions NSEW

            int minCount = Wall;
            if (space.Y-1 >= 0     && map[space.X, space.Y-1] < minCount) minCount = map[space.X, space.Y-1];
            if (space.Y+1 < height && map[space.X, space.Y+1] < minCount) minCount = map[space.X, space.Y+1];
            if (space.X+1 < width  && map[space.X+1, space.Y] < minCount) minCount = map[space.X+1, space.Y];
            if (space.X-1 >= 0     && map[space.X-1, space.Y] < minCount) minCount = map[space.X-1, space.Y];

            // Check for ties--how many directions NSEW have the lowest count?

            int choiceCount = 0;
            if (space.Y-1 >= 0     && map[space.X, space.Y-1] == minCount) choiceCount++;
            if (space.Y+1 < height && map[space.X, space.Y+1] == minCount) choiceCount++;
            if (space.X+1 < width  && map[space.X+1, space.Y] == minCount) choiceCount++;
            if (space.X-1 >= 0     && map[space.X-1, space.Y] == minCount) choiceCount++;

            // We have one single direction that has the lowest count

            if (choiceCount == 1)
            {
                if (space.Y-1 >= 0     && map[space.X, space.Y-1] == minCount) return N;
                if (space.Y+1 < height && map[space.X, space.Y+1] == minCount) return S;
                if (space.X+1 < width  && map[space.X+1, space.Y] == minCount) return E;
                if (space.X-1 >= 0     && map[space.X-1, space.Y] == minCount) return W;
            }

            // We have a tie, there are multiple directions we could go while still moving to the lowest count

            char avoid = '\0';
            if (lastMove == N) avoid = S;
            if (lastMove == S) avoid = N;
            if (lastMove == E) avoid = W;
            if (lastMove == W) avoid = E;

            if (map[space.X, space.Y-1] == minCount && avoid != N) return N;
            if (map[space.X, space.Y+1] == minCount && avoid != S) return S;
            if (map[space.X+1, space.Y] == minCount && avoid != E) return E;
            if (map[space.X-1, space.Y] == minCount && avoid != W) return W;

            return avoid;
        }
    }
}
