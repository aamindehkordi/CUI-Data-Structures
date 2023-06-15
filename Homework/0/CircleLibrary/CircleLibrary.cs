using System;

namespace CircleLibrary
{
    public class Circle
    {
        public double Radius;

        /// <summary> Constructor </summary>
        /// <param name="x">radius of the circle</param>
        public Circle(double x)
        {
            if (x < 0)
            {
                throw new ArgumentException();
            }

            Radius = x;

        }

        /// <summary> ToString method </summary>
        /// <returns>the radius of the circle</returns>
        public override string ToString()
        {
            return $"The Radius of this circle is : {Radius}.";
        }

        /// <summary> Area method </summary>
        /// <returns>the area of the circle</returns>
        public double Area()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }

        /// <summary> Circumference method </summary>
        /// <returns>the circumference of the circle</returns>
        public double Circumference()
        {
            return Math.PI * Radius;
        }
    }
}
