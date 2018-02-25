using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class Square
    {
        public Line Top { get; private set; }
        public Line Bottom { get; private set; }
        public Line Left { get; private set; }
        public Line Right { get; private set; }

        public Point CenterPoint { get; private set; }

        public Square(Point bottomLeft, Point TopRight)
        {
            if(bottomLeft.X > TopRight.X || bottomLeft.Y > TopRight.Y)
            {
                throw new Exception("Not a Square");
            }

            Top = new Line(new Point(bottomLeft.X, TopRight.Y), new Point(TopRight.X, TopRight.Y));
            Bottom = new Line(new Point(bottomLeft.X, bottomLeft.Y), new Point(TopRight.X, bottomLeft.Y));

            Left = new Line(new Point(bottomLeft.X, bottomLeft.Y), new Point(bottomLeft.X, TopRight.Y));
            Right = new Line(new Point(TopRight.X, bottomLeft.Y), new Point(TopRight.X, TopRight.Y));

            CenterPoint = new Point((bottomLeft.X + TopRight.X) / 2.0, (bottomLeft.Y + TopRight.Y) / 2.0);
        }

        public Line Intersection(Line line)
        {
            Point a = null, b = null;

            var sides = new List<Line>() { Top, Bottom, Left, Right };
            foreach (var side in sides)
            {
                var intersection = Line.Intersection(side, line);
                if (!side.Contains(intersection))
                {
                    continue;
                }
                else
                {
                    if (a == null)
                    {
                        a = intersection;
                    }
                    else
                    {
                        b = intersection;
                        break;
                    }
                }
            }

            return new Line(a, b);
        }
    }
}
