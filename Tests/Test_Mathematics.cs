using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Solutions;

namespace Tests
{
    [TestClass]
    public class Test_Mathematics
    {
        [TestMethod]
        public void Q7_4()
        {
            //Minus
            Assert.AreEqual(0, Mathematics.Minus(1, 1));
            Assert.AreEqual(2, Mathematics.Minus(3, 1));
            Assert.AreEqual(-2, Mathematics.Minus(1, 3));
            Assert.AreEqual(-2, Mathematics.Minus(-1, 1));
            Assert.AreEqual(0, Mathematics.Minus(-1, -1));

            //Multiply
            Assert.AreEqual(1, Mathematics.Multiply(-1, -1));
            Assert.AreEqual(2, Mathematics.Multiply(1, 2));
            Assert.AreEqual(1, Mathematics.Multiply(-1, -1));
            Assert.AreEqual(-3, Mathematics.Multiply(3, -1));

            //Divide
            Assert.ThrowsException<ArithmeticException>(() => Mathematics.Divide(3, 0));
            Assert.AreEqual(2, Mathematics.Divide(4, 2));
            Assert.AreEqual(0, Mathematics.Divide(1, 3));
            Assert.AreEqual(3, Mathematics.Divide(-6, -2));
            Assert.AreEqual(-1, Mathematics.Divide(3, -2));
        }
    }
}
