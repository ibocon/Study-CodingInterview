using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System;
using System.Collections.Generic;

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
            Assert.AreEqual(12 , popped);

            threeStacksInStack.Push(2, 31);

            threeStacksInStack.Push(0, 13);
            threeStacksInStack.Push(1, 22);

            threeStacksInStack.Push(2, 31);
            threeStacksInStack.Push(2, 32);
            threeStacksInStack.Push(2, 33);
            threeStacksInStack.Push(2, 34);

            popped = threeStacksInStack.Pop(1);
            Assert.AreEqual(22, popped);

            threeStacksInStack.Push(2, 35);

            Assert.IsFalse(threeStacksInStack.IsEmpty(0));
            Assert.AreEqual(13, threeStacksInStack.Peek(0));
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

            Assert.AreEqual(6, setOfstacks.Pop());
            Assert.AreEqual(5, setOfstacks.Pop());
            Assert.AreEqual(4, setOfstacks.Pop());

            Assert.AreEqual(3, setOfstacks.Pop());
            Assert.AreEqual(2, setOfstacks.Pop());
            Assert.AreEqual(1, setOfstacks.Pop());

            //stack 0
            setOfstacks.Push(1);
            setOfstacks.Push(2);
            setOfstacks.Push(3);

            //stack 1
            setOfstacks.Push(4);
            setOfstacks.Push(5);
            setOfstacks.Push(6);

            Assert.AreEqual(3, setOfstacks.PopAt(0));
            Assert.AreEqual(4, setOfstacks.PopAt(0));
            Assert.AreEqual(5, setOfstacks.PopAt(0));

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

            Assert.AreEqual(1, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size);
            Assert.AreEqual(2, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size);
            Assert.AreEqual(3, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size);
            Assert.AreEqual(4, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size);
            Assert.AreEqual(5, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop().Size);
            Assert.AreEqual(null, hanoiTower.Towers[StackQueue.HanoiTower.TowerLabel.Destination].Pop());
        }

        [TestMethod]
        public void Q3_5()
        {
            var myQueue = new StackQueue.MyQueue<int>();

            myQueue.Enqueue(1);
            myQueue.Enqueue(2);

            Assert.AreEqual(1, myQueue.Peek());
            Assert.AreEqual(1, myQueue.Dequeue());

            myQueue.Enqueue(3);

            Assert.AreEqual(2, myQueue.Dequeue());
            Assert.AreEqual(3, myQueue.Peek());

            myQueue.Enqueue(4);

            Assert.AreEqual(3, myQueue.Peek());

            Assert.AreEqual(3, myQueue.Dequeue());
            Assert.AreEqual(4, myQueue.Dequeue());
        }

        [TestMethod]
        public void Q3_6()
        {
            var stack = new Stack<int>();
            stack.Push(3);
            stack.Push(4);
            stack.Push(2);
            stack.Push(5);
            stack.Push(1);

            var sortedStack = StackQueue.Q6_Sort(stack);

            Assert.AreEqual(5, sortedStack.Pop());
            Assert.AreEqual(4, sortedStack.Pop());
            Assert.AreEqual(3, sortedStack.Pop());
            Assert.AreEqual(2, sortedStack.Pop());
            Assert.AreEqual(1, sortedStack.Pop());
        }

        [TestMethod]
        public void Q3_7()
        {
            var shelter = new StackQueue.AnimalShelter();

            shelter.Enqueue(new StackQueue.AnimalShelter.Cat("A", new DateTime(100)));
            Assert.IsNull(shelter.DequeueDog());
            Assert.AreEqual("A", shelter.DequeueCat().Name);

            shelter.Enqueue(new StackQueue.AnimalShelter.Cat("B", new DateTime(200)));
            Assert.AreEqual("B", shelter.DequeueAny().Name);

            shelter.Enqueue(new StackQueue.AnimalShelter.Dog("C", new DateTime(300)));
            shelter.Enqueue(new StackQueue.AnimalShelter.Cat("D", new DateTime(400)));
            Assert.AreEqual("C", shelter.DequeueAny().Name);

        }
    }
}
