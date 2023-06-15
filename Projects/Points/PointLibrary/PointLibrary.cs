using System;

namespace PointLibrary
{
    public class Point
    {
        private double x, y;

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }
        public Point(double x=0.0, double y=0.0)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }

        static public double DistanceBetween(Point p1, Point p2)
        {
            double a = Math.Pow((p1.x - p2.x),2) + Math.Pow((p1.y - p2.y), 2);
            return Math.Sqrt(a);
        }

        public double DistanceTo(Point b)
        {
            return DistanceBetween(this, b);
        }
        
        public double Magnitude()
        {
            return DistanceBetween(this, new Point(0, 0));
        }
    }
}
