using Solutions.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    public static class Mathematics
    {
        #region Question 7.4
        /*
         * 정수 곱셉, 뺄셈, 나눗셈을 구현하는 메서드들을 작성하라.
         * 더하기 연산자만 사용하라.
         */

        public static int Q4_Minus(int number1, int number2)
        {
            return number1 + Negate(number2);
        }

        public static int Q4_Multiply(int number1, int number2)
        {
            // 덧셈을 줄여, 성능 향상
            if (number1 < number2)
            {
                return Q4_Multiply(number2, number1);
            }

            var sum = 0;

            // 곱셈 연산을 위한 덧셈 횟수
            for (var i = Abs(number2); i > 0; i--)
            {
                sum += number1;
            }

            if (number2 < 0)
            {
                sum = Negate(sum);
            }

            return sum;
        }

        // Minus 함수를 활용하여 나눗셈을 할 수도 있다.
        public static int Q4_Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new ArithmeticException("Divide by zero");
            }

            var absA = Abs(a);
            var absB = Abs(b);

            var product = 0;
            var x = 0;

            while (product + absB <= absA)
            {
                product += absB;
                x++;
            }

            if ((a < 0 && b < 0) || (a > 0 && b > 0))
            {
                return x;
            }

            return Negate(x);
        }

        // 비트 조작을 활용하면 효율을 높일 수 있다.
        // C# 컴파일러는 2의 보수를 활용할 것으로 추정
        private static int Negate(int number)
        {
            var negativeNumber = 0;
            var d = number < 0 ? 1 : -1;

            while (number != 0)
            {
                negativeNumber += d;
                number += d;
            }

            return negativeNumber;
        }

        private static int Abs(int number)
        {
            if (number < 0)
            {
                return Negate(number);
            }

            return number;
        }

        #endregion

        #region Question 7.5
        /*
         * 이차원 명편상에 두 개의 정사각형이 있다.
         * 이 두 정사각형을 반으로 나누는 직선을 찾아라.
         * 정사각형의 윗변과 아랫변은 x축에 평행하다고 가정한다.
         */

        public static Line Q5_CutTwoSquares(Square a, Square b)
        {
            var centerLine = new Line(a.CenterPoint, b.CenterPoint);

            var lineA = a.Intersection(centerLine);
            var lineB = b.Intersection(centerLine);

            return new Line(GetStartPoint(lineA.Start, lineB.Start), GetEndPoint(lineA.End, lineB.End));
        }

        private static Point GetStartPoint(Point a, Point b)
        {
            if (a.X < b.X)
            {
                return a;
            }
            else if (a.X > b.X)
            {
                return b;
            }
            else
            {
                if (a.Y < b.Y)
                {
                    return a;
                }
                else if (a.Y > b.Y)
                {
                    return b;
                }
                else
                {
                    return a;
                }
            }
        }

        private static Point GetEndPoint(Point a, Point b)
        {
            if (a.X < b.X)
            {
                return b;
            }
            else if (a.X > b.X)
            {
                return a;
            }
            else
            {
                if (a.Y < b.Y)
                {
                    return b;
                }
                else if (a.Y > b.Y)
                {
                    return a;
                }
                else
                {
                    return b;
                }
            }
        }

        #endregion

        #region Question 7.6
        /*
         * 점들이 찍혀 있는 이차원 그래프가 있다.
         * 가장 많은 수의 점들을 관통하는 선 하나를 찾아라.
         */

        public static Line Q6_FindBestLine(IList<Point> points)
        {
            Line bestLine = null;
            var bestCount = 0;

            var linesBySlope = new Dictionary<Double, List<Line>>();

            for (var i = 0; i < points.Count; i++)
            {
                for (var j = i + 1; j < points.Count; j++)
                {
                    var line = new Line(points[i], points[j]);
                    InsertLine(linesBySlope, line);

                    var count = CountEquivalentLines(linesBySlope, line);

                    if (count > bestCount)
                    {
                        bestLine = line;
                        bestCount = count;
                    }
                }
            }

            return bestLine;
        }

        private static int CountEquivalentLines(Dictionary<Double, List<Line>> linesBySlope, Line line)
        {
            var key = Line.FloorToNearestEpsilon(line.Slope);
            var count = CountEquivalentLines(linesBySlope[key], line);

            count += linesBySlope.ContainsKey(key - Line.Epsilon) ? CountEquivalentLines(linesBySlope[key - Line.Epsilon], line) : 0;
            count += linesBySlope.ContainsKey(key + Line.Epsilon) ? CountEquivalentLines(linesBySlope[key + Line.Epsilon], line) : 0;

            return count;
        }

        private static int CountEquivalentLines(List<Line> lines, Line line)
        {
            if (lines == null)
            {
                return 0;
            }

            int count = 0;

            foreach (var parallelLine in lines)
            {
                if (parallelLine.Equals(line))
                {
                    count++;
                }
            }

            return count;
        }

        private static void InsertLine(Dictionary<Double, List<Line>> linesBySlope, Line line)
        {
            List<Line> lines;
            var key = Line.FloorToNearestEpsilon(line.Slope);

            if (!linesBySlope.ContainsKey(key))
            {
                lines = new List<Line>();
                linesBySlope.Add(key, lines);
            }
            else
            {
                lines = linesBySlope[key];
            }

            lines.Add(line);
        }

        #endregion

        #region Question 7.7
        /*
         * 3, 5, 7의 세 소수만을 약수로 갖는 k번째 수를 찾는 알고리즘을 설계하라.
         */

        public static int Q7_GetKthMagicNumber(int k)
        {
            if (k < 0)
            {
                return 0;
            }

            var value = 0;

            var queue3 = new Queue<int>();
            var queue5 = new Queue<int>();
            var queue7 = new Queue<int>();

            queue3.Enqueue(1);

            for(int i = 0; i < k; i++)
            {
                var value3 = queue3.Count > 0 ? queue3.Peek() : int.MaxValue;
                var value5 = queue5.Count > 0 ? queue5.Peek() : int.MaxValue;
                var value7 = queue7.Count > 0 ? queue7.Peek() : int.MaxValue;

                value = Math.Min(value3, Math.Min(value5, value7));

                if (value == value3)
                {
                    queue3.Dequeue();
                    queue3.Enqueue(3 * value);
                    queue5.Enqueue(5 * value);
                    queue7.Enqueue(7 * value);
                }
                else if (value == value5)
                {
                    queue5.Dequeue();
                    queue5.Enqueue(5 * value);
                    queue7.Enqueue(7 * value);
                }
                else if(value == value7)
                {
                    queue7.Dequeue();
                    queue7.Enqueue(7 * value);
                }
            }

            return value;
        }

        #endregion
    }
}
