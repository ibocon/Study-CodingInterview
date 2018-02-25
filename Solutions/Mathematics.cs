﻿using Solutions.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    public static class Mathematics
    {
        #region Question 7.4

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
    }
}