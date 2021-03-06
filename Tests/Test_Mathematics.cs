﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Solutions;
using Solutions.Library;

namespace Tests
{
    [TestClass]
    public class Test_Mathematics
    {
        [TestMethod]
        public void Q7_4()
        {
            //Minus
            Assert.AreEqual(0, Mathematics.Q4_Minus(1, 1));
            Assert.AreEqual(2, Mathematics.Q4_Minus(3, 1));
            Assert.AreEqual(-2, Mathematics.Q4_Minus(1, 3));
            Assert.AreEqual(-2, Mathematics.Q4_Minus(-1, 1));
            Assert.AreEqual(0, Mathematics.Q4_Minus(-1, -1));

            //Multiply
            Assert.AreEqual(1, Mathematics.Q4_Multiply(-1, -1));
            Assert.AreEqual(2, Mathematics.Q4_Multiply(1, 2));
            Assert.AreEqual(1, Mathematics.Q4_Multiply(-1, -1));
            Assert.AreEqual(-3, Mathematics.Q4_Multiply(3, -1));

            //Divide
            Assert.ThrowsException<ArithmeticException>(() => Mathematics.Q4_Divide(3, 0));
            Assert.AreEqual(2, Mathematics.Q4_Divide(4, 2));
            Assert.AreEqual(0, Mathematics.Q4_Divide(1, 3));
            Assert.AreEqual(3, Mathematics.Q4_Divide(-6, -2));
            Assert.AreEqual(-1, Mathematics.Q4_Divide(3, -2));
        }

        [TestMethod]
        public void Q7_5()
        {
            
            var line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(0, 0), new Point(2, 2)),
                new Square(new Point(2, 2), new Point(4, 4))
            );

            Assert.AreEqual(0, line.Start.X);
            Assert.AreEqual(0, line.Start.Y);

            Assert.AreEqual(4, line.End.X);
            Assert.AreEqual(4, line.End.Y);

            
            line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(0, 0), new Point(3, 3)),
                new Square(new Point(2, 2), new Point(4, 4))
            );

            Assert.AreEqual(0, line.Start.X);
            Assert.AreEqual(0, line.Start.Y);

            Assert.AreEqual(4, line.End.Y);
            Assert.AreEqual(4, line.End.Y);

            line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(0, 0), new Point(4, 4)),
                new Square(new Point(1, 1), new Point(2, 2))
            );

            Assert.AreEqual(0, line.Start.X);
            Assert.AreEqual(0, line.Start.Y);

            Assert.AreEqual(4, line.End.X);
            Assert.AreEqual(4, line.End.Y);

            line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(2, 2), new Point(4, 4)),
                new Square(new Point(4, 0), new Point(6, 2))
            );

            Assert.AreEqual(2, line.Start.X);
            Assert.AreEqual(4, line.Start.Y);

            Assert.AreEqual(6, line.End.X);
            Assert.AreEqual(0, line.End.Y);

            line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(0, 0), new Point(4, 4)),
                new Square(new Point(1, 0), new Point(3, 2))
            );

            Assert.AreEqual(2, line.Start.X);
            Assert.AreEqual(0, line.Start.Y);

            Assert.AreEqual(2, line.End.X);
            Assert.AreEqual(4, line.End.Y);

            line = Mathematics.Q5_CutTwoSquares
            (
                new Square(new Point(0, 0), new Point(4, 4)),
                new Square(new Point(2, 1), new Point(4, 3))
            );

            Assert.AreEqual(0, line.Start.X);
            Assert.AreEqual(2, line.Start.Y);

            Assert.AreEqual(4, line.End.X);
            Assert.AreEqual(2, line.End.Y);

        }

        [TestMethod]
        public void Q7_6()
        {
            var points = new List<Point>()
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1),
                new Point(1, 4),
                new Point(2, 2),
                new Point(2, 3),
                new Point(3, 3),
                new Point(3, 4),
                new Point(4, 4),
            };

            var bestLine = Mathematics.Q6_FindBestLine(points);

            
            Assert.AreEqual(1, bestLine.Slope);
            Assert.AreEqual(0, bestLine.InterceptY);

            points = new List<Point>()
            {
                new Point(4, 0),
                new Point(3, 1),
                new Point(2, 2),
                new Point(1, 3),
                new Point(0, 4),
                new Point(2, 4),
                new Point(3, 5),
            };

            bestLine = Mathematics.Q6_FindBestLine(points);

            Assert.AreEqual(-1, bestLine.Slope);
            Assert.AreEqual(4, bestLine.InterceptY);
        }

        [TestMethod]
        public void Q7_7()
        {
            Assert.AreEqual(1, Mathematics.Q7_GetKthMagicNumber(1));
            Assert.AreEqual(3, Mathematics.Q7_GetKthMagicNumber(2));
            Assert.AreEqual(5, Mathematics.Q7_GetKthMagicNumber(3));
            Assert.AreEqual(7, Mathematics.Q7_GetKthMagicNumber(4));
            Assert.AreEqual(9, Mathematics.Q7_GetKthMagicNumber(5));
            Assert.AreEqual(15, Mathematics.Q7_GetKthMagicNumber(6));
            Assert.AreEqual(21, Mathematics.Q7_GetKthMagicNumber(7));
            Assert.AreEqual(25, Mathematics.Q7_GetKthMagicNumber(8));
            Assert.AreEqual(27, Mathematics.Q7_GetKthMagicNumber(9));
            Assert.AreEqual(35, Mathematics.Q7_GetKthMagicNumber(10));
        }
    }
}
