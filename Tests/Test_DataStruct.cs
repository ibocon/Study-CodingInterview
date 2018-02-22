using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class Test_DataStruct
    {
        [TestMethod]
        public void Q1_1()
        {
            string[] uniqueWords = { "abcde", "kite", "padle" };
            foreach(string word in uniqueWords)
            {
                Assert.IsTrue(DataStruct.Q1_IsUniqueChar(word), $"'{word}' should be unique!");
            }
            string[] commonWords = { "hello", "apple" };
            foreach (string word in commonWords)
            {
                Assert.IsFalse(DataStruct.Q1_IsUniqueChar(word), $"'{word}' should not unique!");
            }
        }

        [TestMethod]
        public void Q1_2()
        {
            Assert.AreEqual(DataStruct.Q2_Reverse("vxyz"), "zyxv");
            Assert.AreEqual(DataStruct.Q2_Reverse("abcde"), "edcba");
            Assert.AreEqual(DataStruct.Q2_Reverse("cat"), "tac");
        }

        [TestMethod]
        public void Q1_3()
        {
            Assert.IsTrue(DataStruct.Q3_IsPermutation("apple", "papel"));
            Assert.IsTrue(DataStruct.Q3_IsPermutation("carrot", "tarroc"));

            Assert.IsFalse(DataStruct.Q3_IsPermutation("hello", "llloh"));
        }

        [TestMethod]
        public void Q1_4()
        {
            Assert.AreEqual(DataStruct.Q4_ReplaceSpaces("abc d e f"), "abc%20d%20e%20f");
        }

        [TestMethod]
        public void Q1_5()
        {
            Assert.AreEqual(DataStruct.Q5_Compress("abbccccccde"), "a1b2c6d1e1");
        }

        [TestMethod]
        public void Q1_6()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            DataStruct.Q6_Rotate(matrix);

            int[,] expectedAnswer = new int[,] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };

            Assert.AreEqual(matrix.Rank, expectedAnswer.Rank);

            foreach(int dimension in Enumerable.Range(0, matrix.Rank))
            {
                Assert.AreEqual(matrix.GetLength(dimension), expectedAnswer.GetLength(dimension));
            }

            Assert.IsTrue(matrix.Cast<int>().SequenceEqual(expectedAnswer.Cast<int>()));
            
        }

        [TestMethod]
        public void Q1_7()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 0, 6 }, { 7, 8, 9 } };
            DataStruct.Q07_SetZeros(matrix);

            int[,] expectedAnswer = new int[,] { { 1, 0, 3 }, { 0, 0, 0 }, { 7, 0, 9 } };
            Assert.IsTrue(matrix.Cast<int>().SequenceEqual(expectedAnswer.Cast<int>()));
        }

        [TestMethod]
        public void Q1_8()
        {
            Assert.IsTrue(DataStruct.Q08_IsRotation("apple", "pleap"));
            Assert.IsTrue(DataStruct.Q08_IsRotation("waterbottle", "erbottlewat"));

            Assert.IsFalse(DataStruct.Q08_IsRotation("camera", "macera"));
        }
    }
}
