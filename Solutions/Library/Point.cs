using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class Point : IEquatable<Point>
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other)
        {
            return (other.X == X && other.Y == Y);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
