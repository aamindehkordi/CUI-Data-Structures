using System;

namespace CircleLibrary
{
    public class Circle
    {
        public double Radius;

        public Circle(double x)
        {
            if (x < 0)
            {
                throw new ArgumentException();
            }

            Radius = x;

        }

        public override string ToString()
        {
            return $"The Radius of this circle is : {Radius}.";
        }

        public double Area()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        public double Circumference()
        {
            return Math.PI * Radius;
        }
    }
}
