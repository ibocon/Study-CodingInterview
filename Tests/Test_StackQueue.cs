using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System;

namespace Tests
{
    [TestClass]
    public class Test_StackQueue
    {
        [TestMethod]
        public void Q3_1()
        {
            var threeStacksInStack = new StackQueue.ThreeStacksInStack();
            for (int i = 0; i < StackQueue.ThreeStacksInStack.NumberOfStacks; i++)
            {
                Assert.IsTrue(threeStacksInStack.IsEmpty(i));
            }

            threeStacksInStack.Push(0, 10);
            threeStacksInStack.Push(1, 20);
            threeStacksInStack.Push(2, 30);

            threeStacksInStack.Push(1, 21);
            threeStacksInStack.Push(0, 11);
            threeStacksInStack.Push(0, 12);

            var popped = threeStacksInStack.Pop(0);
            Assert.AreEqual(popped, 12);

            threeStacksInStack.Push(2, 31);

            threeStacksInStack.Push(0, 13);
            threeStacksInStack.Push(1, 22);

            threeStacksInStack.Push(2, 31);
            threeStacksInStack.Push(2, 32);
            threeStacksInStack.Push(2, 33);
            threeStacksInStack.Push(2, 34);

            popped = threeStacksInStack.Pop(1);
            Assert.AreEqual(popped, 22);

            threeStacksInStack.Push(2, 35);

            Assert.IsFalse(threeStacksInStack.IsEmpty(0));
            Assert.AreEqual(threeStacksInStack.Peek(0), 13);
        }

        [TestMethod]
        public void Q3_2()
        {
            var stackWithMin = new StackQueue.StackWithMin();

            stackWithMin.Push(6);
            stackWithMin.Push(3);
            stackWithMin.Push(8);

            Assert.AreEqual(3, stackWithMin.Min);

            Assert.AreEqual(8, stackWithMin.Pop());
            Assert.AreEqual(3, stackWithMin.Pop());

            Assert.AreEqual(6, stackWithMin.Min);
            Assert.AreEqual(6, stackWithMin.Pop());
        }

        [TestMethod]
        public void Q3_3()
        {
            var setOfstacks = new StackQueue.SetOfStacks<int>(3);

            //stack 0
            setOfstacks.Push(1);
            setOfstacks.Push(2);
            setOfstacks.Push(3);

            //stack 1
            setOfstacks.Push(4);
            setOfstacks.Push(5);
            setOfstacks.Push(6);

            Assert.AreEqual(setOfstacks.Pop(), 6);
            Assert.AreEqual(setOfstacks.Pop(), 5);
            Assert.AreEqual(setOfstacks.Pop(), 4);

            Assert.AreEqual(setOfstacks.Pop(), 3);
            Assert.AreEqual(setOfstacks.Pop(), 2);
            Assert.AreEqual(setOfstacks.Pop(), 1);

            //stack 0
            setOfstacks.Push(1);
            setOfstacks.Push(2);
            setOfstacks.Push(3);

            //stack 1
            setOfstacks.Push(4);
            setOfstacks.Push(5);
            setOfstacks.Push(6);

            Assert.AreEqual(setOfstacks.PopAt(0), 3);
            Assert.AreEqual(setOfstacks.PopAt(0), 4);
            Assert.AreEqual(setOfstacks.PopAt(0), 5);

        }

        [TestMethod]
        public void Q3_4()
        {
            var origin = new StackQueue.HanoiTower.Tower();
            origin.Push(new StackQueue.HanoiTower.Disk(5));
            origin.Push(new StackQueue.HanoiTower.Disk(4));
            origin.Push(new StackQueue.HanoiTower.Disk(3));
            origin.Push(new StackQueue.HanoiTower.Disk(2));
            origin.Push(new StackQueue.HanoiTower.Disk(1));

            var buffer = new StackQueue.HanoiTower.Tower();

            var destination = new StackQueue.HanoiTower.Tower();

            var hanoiTower = new StackQueue.HanoiTower(origin, buffer, destination);
            hanoiTower.Solve();

            Assert.IsTrue(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Origin].IsEmpty());
            Assert.IsTrue(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Buffer].IsEmpty());

            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size, 1);
            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size, 2);
            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size, 3);
            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size, 4);
            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size, 5);
            Assert.AreEqual(hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop(), null);
        }
    }
}
