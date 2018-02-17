using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Solutions;
using Solutions.Library;

namespace Tests
{
    [TestClass]
    public class Test_BitManipulation
    {
        [TestMethod]
        public void Q5_1()
        {
            Assert.AreEqual(0x0000044C, BitManipulation.Q1_InsertBits(0x00000400, 0x00000013, 2, 6));
            Assert.AreEqual(0x00000413, BitManipulation.Q1_InsertBits(0x00000400, 0x00000013, 0, 4));
            Assert.AreEqual(0x000BFFFF, BitManipulation.Q1_InsertBits(0x000B0000, 0x0000FFFF, 0, 15));
        }

        [TestMethod]
        public void Q5_2()
        {
            Assert.AreEqual(@"0.1", BitManipulation.Q2_PrintBinary(0.5));
            Assert.AreEqual(@"ERROR", BitManipulation.Q2_PrintBinary(1));
            Assert.AreEqual(@"0.11", BitManipulation.Q2_PrintBinary(0.75));
            Assert.AreEqual(@"0.001", BitManipulation.Q2_PrintBinary(0.125));
        }

        [TestMethod]
        public void Q5_3()
        {
            Assert.AreEqual(0x00000001, BitManipulation.Q3_GetPrevArith(0x00000002));
            Assert.AreEqual(0x00000850, BitManipulation.Q3_GetPrevArith(0x00000860));

            Assert.AreEqual(0x17FFFFFF, BitManipulation.Q3_GetNextArith(0x0FFFFFFF));
            Assert.AreEqual(0x00000881, BitManipulation.Q3_GetNextArith(0x00000860));
        }

        [TestMethod]
        public void Q5_5()
        {
            Assert.AreEqual(0, BitManipulation.Q5_BitSwapRequired(0x000000E2, 0x000000E2));
            Assert.AreEqual(4, BitManipulation.Q5_BitSwapRequired(0x00000860, 0x0000F860));
        }

        [TestMethod]
        public void Q5_6()
        {
            Assert.AreEqual(0x00005555, BitManipulation.Q6_SwapOddEvenBits(0x0000AAAA));
            Assert.AreEqual(0x0000D893, BitManipulation.Q6_SwapOddEvenBits(0x0000E463));
        }

        [TestMethod]
        public void Q5_7()
        {
            var array = new List<BitInteger>()
            {
                new BitInteger(0x0005),
                new BitInteger(0x0000),
                new BitInteger(0x0001),
                new BitInteger(0x0003),
                new BitInteger(0x0002),
                new BitInteger(0x0006),
            };

            Assert.AreEqual(0x0004, BitManipulation.Q7_FindMissing(array));

            array = new List<BitInteger>()
            {
                new BitInteger(0x0005),
                new BitInteger(0x0001),
                new BitInteger(0x0003),
                new BitInteger(0x0002),
                new BitInteger(0x0006),
                new BitInteger(0x0004),
            };

            Assert.AreEqual(0x0000, BitManipulation.Q7_FindMissing(array));
        }

        [TestMethod]
        public void Q5_8()
        {
            const int width = 8 * 2;
            const int height = 1;
            var screen = new byte[width * height / 8];

            BitManipulation.Q8_DrawHorizontalLine(screen, width, 8, 10, 0);

            Assert.AreEqual(0xE0, screen[1]);

            screen = new byte[width * height / 8];
            BitManipulation.Q8_DrawHorizontalLine(screen, width, 0, 15, 0);

            Assert.AreEqual(0xFF, screen[0]);
            Assert.AreEqual(0xFF, screen[1]);
        }   
    }
}
