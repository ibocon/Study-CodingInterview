using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System;

namespace Tests
{
    [TestClass]
    class Test_StackQueue
    {
        private readonly StackQueue stackQueue;
        
        public Test_StackQueue()
        {
            this.stackQueue = new StackQueue();
        }
        [TestMethod]
        public void Q3_1()
        {
            StackQueue stackQueue = new StackQueue();
            for(int i = 0; i < StackQueue.NumberOfStacks; i++)
            {
                Assert.IsTrue(stackQueue.IsEmpty(i));
            }

            stackQueue.Push(0, 10);
            stackQueue.Push(1, 20);
            stackQueue.Push(2, 30);

            stackQueue.Push(1, 21);
            stackQueue.Push(0, 11);
            stackQueue.Push(0, 12);

            var popped = stackQueue.Pop(0);
            Console.WriteLine("popped: {0}", popped);
            Console.WriteLine();

            stackQueue.Push(2, 31);

            stackQueue.Push(0, 13);
            stackQueue.Push(1, 22);

            stackQueue.Push(2, 31);
            stackQueue.Push(2, 32);
            stackQueue.Push(2, 33);
            stackQueue.Push(2, 34);

            popped = stackQueue.Pop(1);
            Console.WriteLine("popped: {0}", popped);
            Console.WriteLine();
            stackQueue.Push(2, 35);

            Console.WriteLine("IsEmpty stack 0? {0}", stackQueue.IsEmpty(0));
            Console.WriteLine("What's on top of stack 0? {0}", stackQueue.Peek(0));

        }
    }
}
