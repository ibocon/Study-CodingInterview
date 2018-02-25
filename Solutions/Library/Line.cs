using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class Line : IEquatable<Line>
    {
        public const double Epsilon = 0.0001;

        public double Slope { get; private set; }
        public double InterceptX { get; private set; }
        public double InterceptY { get; private set; }

        public Point Start { get; private set; }
        public Point End { get; private set; }

        public Line(Point a, Point b)
        {
            if (a.X < b.X)
            {
                Start = a;
                End = b;
            }
            else if (a.X > b.X)
            {
                Start = b;
                End = a;
            }
            else
            {
                if (a.Y < b.Y)
                {
                    Start = a;
                    End = b;
                }
                else if (a.Y > b.Y)
                {
                    Start = b;
                    End = a;
                }
                else
                {
                    throw new Exception("Not a line");
                }
            }

            CalcSlope();
            CalcIntercept();
        }

        private void CalcSlope()
        {
            if (Start.X == End.X)
            {
                Slope = Double.PositiveInfinity;
            }
            else
            {
                Slope = (End.Y - Start.Y) / (End.X - Start.X);
            }
        }

        private void CalcIntercept()
        {
            if (Start.X == End.X)
            {
                InterceptX = Start.X;
                InterceptY = 0;
            }
            else
            {
                InterceptX = 0;
                InterceptY = (End.X * Start.Y - Start.X * End.Y) / (End.X - Start.X);
            }
        }

        public bool Contains(Point point)
        {
            
            if (
                Start.X <= point.X && point.X <= End.X &&
                Start.Y <= point.Y && point.Y <= End.Y
               )
            {
                if (Slope == double.PositiveInfinity)
                {
                    return (Start.X == point.X);
                }

                return (point.Y == Slope * (point.X + InterceptX) + InterceptY);
            }

            return false;
        }

        public static Point Intersection(Line a, Line b)
        {
            double x, y;

            if (a.Slope == b.Slope && a.InterceptY != b.InterceptY)
            {
                x = Double.PositiveInfinity;
                y = Double.PositiveInfinity;
            }
            else if (a.Slope == Double.PositiveInfinity)
            {
                x = a.Start.X;
                y = b.Slope * x + b.InterceptY;
            }
            else if (b.Slope == Double.PositiveInfinity)
            {
                x = b.Start.X;
                y = a.Slope * x + a.InterceptY;
            }
            else
            {
                x = (b.InterceptY - a.InterceptY) / (a.Slope - b.Slope);
                y = a.Slope * x + a.InterceptY;
            }

            return new Point(x, y);            
        }

        public bool Equals(Line other)
        {
            if (
                Math.Abs(Slope - other.Slope) < Epsilon &&
                Math.Abs(InterceptX - other.InterceptX) < Epsilon &&
                Math.Abs(InterceptY - other.InterceptY) < Epsilon
               )
            {
                return true;
            }

            return false;
        }

        public static double FloorToNearestEpsilon(double d)
        {
            var r = (int)(d / Epsilon);
            return r * Epsilon;
        }
    }
}
