using System;
using System.Collections.Generic;

namespace Solutions
{

    public class StackQueue
    {
        #region Question 3.1
        /* 
         * 하나의 배열을 사용해 세 개의 스택을 구현하는 방법을 설명하라.
         */

        #region Helper Class
        /// <summary>
        /// 스택에 저장되는 실제 데이터를 보관한다.
        /// </summary>
        class StackData
        {
            /// <summary>
            /// 전체 스택에서 내부 스택이 시작하는 위치
            /// </summary>
            public int Start { get; set; }
            /// <summary>
            /// 내부 스택 안에서 현재 위치
            /// </summary>
            public int Pointer { get; set; }
            /// <summary>
            /// 내부 스택에 저장된 데이터 크기
            /// </summary>
            public int Size { get; set; }
            /// <summary>
            /// 내부 스택이 허용할 수 있는 데이터 크기
            /// </summary>
            public int Capacity { get; set; }

            /// <summary>
            /// 내부 스택에 대한 메타데이터 초기화
            /// </summary>
            /// <param name="start">전체 스택에서 내부 스택의 시작 위치</param>
            /// <param name="capacity">내부 스택이 허용할 수 있는 데이터 크기</param>
            public StackData(int start, int capacity)
            {
                this.Start = start;
                this.Pointer = start - 1;
                this.Size = 0;
                this.Capacity = capacity;
            }

            /// <summary>
            /// <paramref name="index"/>가 내부 스택에 존재하는지를 체크한다.
            /// </summary>
            /// <param name="index">내부 스택에 존재하는지를 체크하는 기준 위치</param>
            /// <param name="totalSize">내부 스택이 가질 수 있는 최대 크기</param>
            /// <returns>내부 스택에 존재한다면 <code>true</code>를 반환</returns>
            public bool IsWithinStack(int index, int totalSize)
            {
                // 내부 스택 안에 있는지 확인
                if (Start <= index && index < Start + Capacity)
                {
                    return true;
                }
                // 만약 내부 스택의 데이터가 전체 스택에 넘쳐서 데이터가 저장되었을 경우, 전체 스택 처음에 위치하는지 확인
                else if (Start + Capacity > totalSize && index < (Start + Capacity) % totalSize)
                {
                    return true;
                }

                // 만약 내부 스택에도 없고 넘쳐서도 존재하지 않는다면,
                // 즉 내부 스택이 허용할 수 있는 크기를 넘어서 존재한다면,
                return false;
            }
        }
        #endregion

        public class ThreeStacksInStack
        {
            #region Properties
            /// <summary>
            /// 내부 스택 개수
            /// </summary>
            public const int NumberOfStacks = 3;

            /// <summary>
            /// 내부 스택 기본 크기
            /// </summary>
            const int DefaultSize = 4;

            /// <summary>
            /// 전체 스택 크기
            /// </summary>
            const int TotalSize = DefaultSize * NumberOfStacks;

            /// <summary>
            /// 내부 스택 메타데이터
            /// </summary>
            readonly StackData[] _stacks =
            {
                new StackData(0, DefaultSize),
                new StackData(DefaultSize, DefaultSize),
                new StackData(DefaultSize * 2, DefaultSize)
            };

            /// <summary>
            /// 전체 스택
            /// </summary>
            readonly int[] _buffer = new int[TotalSize];

            #endregion

            #region Methods
            /// <summary>
            /// 내부 스택에 저장된 데이터 개수를 전부 더한 개수를 계산한다.
            /// </summary>
            /// <returns>전체 데이터 개수</returns>
            private int NumberOfElements()
            {
                return _stacks[0].Size + _stacks[1].Size + _stacks[2].Size;
            }

            /// <summary>
            /// 전체 스택에서 <paramref name="index"/> 다음 데이터를 반환한다.
            /// </summary>
            /// <param name="index">찾을 다음 데이터의 기준 위치</param>
            /// <returns><paramref name="index"/> 다음 데이터</returns>
            private int NextElement(int index)
            {
                if (index + 1 == TotalSize)
                {
                    return 0;
                }
                else
                {
                    return index + 1;
                }
            }

            /// <summary>
            /// 전체 스택에서 <paramref name="index"/> 이전 데이터를 반환한다.
            /// </summary>
            /// <param name="index">찾을 이전 데이터의 기준 위치</param>
            /// <returns><paramref name="index"/> 이전 데이터</returns>
            private int PreviousElement(int index)
            {
                if (index == 0)
                {
                    return TotalSize - 1;
                }
                else
                {
                    return index - 1;
                }
            }

            /// <summary>
            /// <paramref name="stackNum"/> 번째 내부 스택의 모든 데이터를 전체 스택에서 오른쪽으로 1칸 이동한다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            public void Shift(int stackNum)
            {
                var stack = _stacks[stackNum];

                // 내부 스택의 최대 크기가 부족하다면 증가시킨다.
                if (stack.Size >= stack.Capacity)
                {
                    var nextStack = (stackNum + 1) % NumberOfStacks;
                    Shift(nextStack); // 다음 내부 스택을 오른쪽으로 한칸 움직여서 공간을 확보한다.
                    stack.Capacity++; // 내부 스택의 최대 크기를 일시적으로 증가시킨다.
                }

                for (var i = (stack.Start + stack.Capacity - 1) % TotalSize; // 배열의 끝
                              stack.IsWithinStack(i, TotalSize);
                              i = PreviousElement(i))
                {
                    // 역순으로 (내부 스택의 끝에서 처음까지) 데이터를 오른쪽으로 한칸씩 이동시킨다.
                    // 역순으로 이동해야 데이터의 손실없이 이동시킬 수 있다.
                    _buffer[i] = _buffer[PreviousElement(i)];
                }

                _buffer[stack.Start] = 0; // 내부 스택의 시작 위치 데이터를 초기화한다.
                stack.Start = NextElement(stack.Start); // 내부 스택의 시작 위치를 한칸 오른쪽으로 이동한다.
                stack.Pointer = NextElement(stack.Pointer); // 내부 스택이 가르키고 있는 현재 위치를 한칸 오른쪽으로 이동한다.
                stack.Capacity--; // 내부 스택의 최대 크기를 원래대로 복구한다.
            }

            /// <summary>
            /// <paramref name="stackNum"/>번째 내부 스택의 최대크기를 하나 증가시킨다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            public void Expand(int stackNum)
            {
                //내부 스택의 용량을 늘리기 전에 다음 스택을 한칸 이동시켜 공간을 확보한다.
                Shift((stackNum + 1) % NumberOfStacks);
                _stacks[stackNum].Capacity++;
            }

            /// <summary>
            /// <paramref name="stackNum"/> 번째 내부 스택에 <paramref name="value"/> 데이터를 저장한다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            /// <param name="value">저장할 데이터</param>
            public void Push(int stackNum, int value)
            {
                var stack = _stacks[stackNum];

                /* 전체 스택에 남은 공간이 있는지 확인 */
                if (stack.Size >= stack.Capacity)
                {
                    if (NumberOfElements() >= TotalSize)
                    { // 전체 스택에 공간이 없을 때
                        throw new Exception("Out of space.");
                    }
                    else
                    { // 전체 스택을 한칸 움직여 내부 스택 공간을 확보한다.
                        Expand(stackNum);
                    }
                }

                // 내부 스택 크기 증가
                stack.Size++;
                // 내부 스택 포인터 이동
                stack.Pointer = NextElement(stack.Pointer);
                // 내부 스택에 데이터 저장
                _buffer[stack.Pointer] = value;
            }

            /// <summary>
            /// <paramref name="stackNum"/> 번째 내부 스택에서 데이터를 꺼낸다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            /// <returns><paramref name="stackNum"/>에 마지막으로 저장된 데이터</returns>
            public int Pop(int stackNum)
            {
                var stack = _stacks[stackNum];

                // 저장된 데이터가 없을 때
                if (stack.Size == 0)
                {
                    throw new Exception("Trying to pop an empty stack.");
                }

                // 데이터를 추출
                var value = _buffer[stack.Pointer];
                // 데이터를 0으로 초기화
                _buffer[stack.Pointer] = 0;
                // 내부 스택 포인터 변경
                stack.Pointer = PreviousElement(stack.Pointer);
                // 내부 스택 크기 감소
                stack.Size--;

                // 추출된 데이터를 반환
                return value;
            }

            /// <summary>
            /// <paramref name="stackNum"/> 번째 내부 스택에 마지막으로 저장된 데이터를 반환한다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            /// <returns><paramref name="stackNum"/> 번째 내부 스택에 마지막으로 저장된 데이터</returns>
            public int Peek(int stackNum)
            {
                var stack = _stacks[stackNum];

                return _buffer[stack.Pointer];
            }

            /// <summary>
            /// <paramref name="stackNum"/> 번째 내부 스택이 비었는지 확인한다.
            /// </summary>
            /// <param name="stackNum">내부 스택 번호</param>
            /// <returns>스택이 비었을 때는 <code>true</code>를 반환</returns>
            public bool IsEmpty(int stackNum)
            {
                var stack = _stacks[stackNum];

                return stack.Size == 0;
            }
            #endregion
        }
        #endregion

        #region Question 3.2
        /* 
         * push와 pop의 두가지 연산뿐 아니라, 최솟값을 갖는 원소를 반환하는 min 연산을 갖춘 스택은 어떻게 구현할 수 있겠는가?
        */
        public class StackWithMin
        {
            /// <summary>
            /// 데이터를 저장하는 스택
            /// </summary>
            private Stack<int> dataStack;
            /// <summary>
            /// 최솟값을 저장하는 스택
            /// </summary>
            private Stack<int> minStack;

            /// <summary>
            /// 스택 최솟값
            /// </summary>
            public int Min
            {
                get
                {
                    try
                    {
                        return minStack.Peek();
                    }
                    catch (InvalidOperationException)
                    {
                        return int.MaxValue;
                    }
                }
            }

            /// <summary>
            /// 클래스 초기화
            /// </summary>
            public StackWithMin()
            {
                this.minStack = new Stack<int>();
                this.dataStack = new Stack<int>();
            }

            /// <summary>
            /// 입력된 <paramref name="value"/> 데이터를 저장한다.
            /// </summary>
            /// <param name="value">저장할 데이터</param>
            public void Push(int value)
            {
                // 새로 입력된 데이터가 현재 최솟값보다 작은지 확인한다.
                if (value <= Min)
                {
                    this.minStack.Push(value);
                }

                this.dataStack.Push(value);
            }

            /// <summary>
            /// 스택에서 마지막으로 저장된 데이터를 반환한다.
            /// </summary>
            /// <returns>스택 최상단 데이터</returns>
            public int Pop()
            {
                try
                {
                    var value = this.dataStack.Pop();

                    // 만약 최솟값과 반환할 데이터가 같을 경우, 이전 최솟값으로 변경한다.
                    if (value == Min)
                    {
                        this.minStack.Pop();
                    }

                    return value;
                }
                catch (InvalidOperationException)
                {
                    throw new Exception("Stack is empty.");
                }

            }
        }
        #endregion

        #region Question 3.3
        /*
         * SetOfStacks는 여러 스택으로 구성되며, 이전 스택이 지정된 용량을 초과하는 경우 새로운 스택을 생성해야 한다.
         * Push()와 Pop()은 스택이 하나인 경우와 동일하게 동작해야 한다.
         * (특정한 하위 스택에 대해서 Pop()을 수행하는 PopAt(int index) 함수를 구현하라.)
        */
        /// <summary>
        /// 저장할 수 있는 데이터 개수에 제한이 있는 스택이다.ㄴ
        /// </summary>
        /// <typeparam name="T">데이터 타입</typeparam>
        public class LimitedStack<T>
        {
            /// <summary>
            /// 스택에 저장된 데이터
            /// </summary>
            private LinkedList<T> data;

            /// <summary>
            /// 스택에 저장가능한 개수
            /// </summary>
            private int capacity;

            /// <summary>
            /// 저장된 데이터 총량
            /// </summary>
            public int Count
            {
                get { return data.Count; }
            }

            /// <summary>
            /// 저장할 수 있는 최대개수를 지정하고 인스턴스 생성한다.
            /// </summary>
            /// <param name="capacity">저장가능한 개수</param>
            public LimitedStack(int capacity)
            {
                this.data = new LinkedList<T>();
                this.capacity = capacity;
            }

            /// <summary>
            /// 스택에 데이터를 저장한다.
            /// </summary>
            /// <param name="value">저장할 데이터</param>
            public void Push(T value)
            {
                if(data.Count >= capacity)
                {
                    throw new InvalidOperationException("Stack is full.");
                }

                data.AddLast(new LinkedListNode<T>(value));
            }

            /// <summary>
            /// 스택에서 데이터 꺼낸다.
            /// </summary>
            /// <returns>마지막으로 저장된 스택 데이터</returns>
            public T Pop()
            {
                if(this.IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty.");
                }

                var value = data.Last.Value;
                data.RemoveLast();
                return value;
            }

            public bool IsEmpty()
            {
                if(data.Last == null || data.First == null)
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// 일반적인 스택과 달리, 큐처럼 제일 오래된 데이터를 꺼낸다.
            /// </summary>
            /// <returns>스택에서 제일 오래된 데이터</returns>
            public T PopBottom()
            {
                if (this.IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty.");
                }

                var value = data.First.Value;
                data.RemoveFirst();
                return value;
            }
        }

        /// <summary>
        /// 데이터 총량이 제한된 스택에 나누어 데이터를 저장하는 스택이다.
        /// </summary>
        /// <typeparam name="T">스택에 저장될 데이터 타입</typeparam>
        public class SetOfStacks<T>
        {
            private List<LimitedStack<T>> stacks = new List<LimitedStack<T>>();
            /// <summary>
            /// 각 제한된 스택에 가질 수 있는 데이터 총량
            /// </summary>
            public int Capacity { get; }

            public SetOfStacks(int capacity)
            {
                this.Capacity = capacity;
            }

            public LimitedStack<T> GetLastStack()
            {
                if(this.stacks.Count == 0)
                {
                    return null;
                }
                else
                {
                    return stacks[this.stacks.Count - 1];
                }
            }

            public void Push(T value)
            {
                var last = GetLastStack();

                try
                {
                    last.Push(value);
                }
                catch (Exception)
                {
                    var stack = new LimitedStack<T>(Capacity);
                    stack.Push(value);
                    this.stacks.Add(stack);
                }
            }

            public T Pop()
            {
                var last = GetLastStack();
                var value = last.Pop();

                if(last.Count == 0)
                {
                    this.stacks.Remove(last);
                }

                return value;
            }

            /// <summary>
            /// 특정 스택에서 최근 데이터를 꺼낸다.
            /// </summary>
            /// <param name="index">특정 스택 번호</param>
            /// <returns>특정 스택에서 꺼낸 데이터</returns>
            public T PopAt(int index)
            {
                if(index < 0 || index >= stacks.Count)
                {
                    throw new InvalidOperationException("There is no stack at " + index);
                }

                var value = this.stacks[index].Pop();

                if (this.stacks[index].IsEmpty())
                {
                    this.stacks.RemoveAt(index);
                }
                else
                {
                    while ((++index) < stacks.Count)
                    {
                        this.stacks[index - 1].Push(this.stacks[index].PopBottom());
                    }
                }

                return value;
            }
        }

        #endregion

        #region Question 3.4
        /*
         * 스택을 사용하여 하노이탑 문제를 해결하라.
         * (첫번째 탑에 있는 모든 원판을 마지막 탑으로 옮기는 프로그램을 작성하라.)
         */
        public class HanoiTower
        {
            /// <summary>
            /// 하노이 원판
            /// </summary>
            public class Disk : IComparable<Disk>
            {
                public int Size { get; }
                public Disk(int size)
                {
                    this.Size = size;
                }

                public int CompareTo(Disk other)
                {
                    return this.Size - other.Size;
                }

                #region Method Overloading
                // 원판끼리 자연스럽게 크기를 비교할 수 있도록 한다.
                public static bool operator >(Disk b1, Disk b2)
                {
                    return (b1.CompareTo(b2) > 0);
                }

                public static bool operator <(Disk b1, Disk b2)
                {
                    return (b1.CompareTo(b2) < 0);
                }
                #endregion
            }

            /// <summary>
            /// 하노이 기둥
            /// </summary>
            public class Tower
            {
                /// <summary>
                /// 하노이 기둥에 꽃힌 원판들
                /// </summary>
                private Stack<Disk> disks;
                public int Count
                {
                    get { return this.disks.Count; }
                }

                public Tower()
                {
                    this.disks = new Stack<Disk>();
                }

                public void Push(Disk disk)
                {
                    if(this.disks.Count > 0 && this.disks.Peek() < disk)
                    {
                        throw new Exception("Logial Error!");
                    }

                    this.disks.Push(disk);
                }

                public Disk Pop()
                {
                    try
                    {
                        return this.disks.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        return null;
                    }
                }

                public bool IsEmpty()
                {
                    return this.disks.Count == 0;
                }
            }

            public enum TowerLabel { Origin, Buffer, Destination };

            /// <summary>
            /// 하노이 게임에서 사용될 기둥들
            /// </summary>
            public IDictionary<TowerLabel, Tower> Towers { get; }

            public HanoiTower(Tower origin, Tower buffer, Tower destination)
            {
                this.Towers = new Dictionary<TowerLabel, Tower>
                {
                    { TowerLabel.Origin, origin },
                    { TowerLabel.Buffer, buffer },
                    { TowerLabel.Destination, destination }
                };
            }

            /// <summary>
            /// 하노이 게임을 해결한다.
            /// </summary>
            public void Solve()
            {
                this.MoveDisk(this.Towers[TowerLabel.Origin].Count, TowerLabel.Origin, TowerLabel.Buffer, TowerLabel.Destination);
            }

            private void MoveDisk(int count, TowerLabel start, TowerLabel buffer, TowerLabel finish)
            {
                if (count == 0) return;

                MoveDisk(count - 1, start, finish, buffer);

                this.Towers[finish].Push(this.Towers[start].Pop());

                MoveDisk(count - 1, buffer, start, finish);
            }
        }

        #endregion

        #region Question 3.5
        /*
         * 두 개의 스택을 사용하여 큐를 구현하는 MyQueue 클래스를 작성해보라.
         */
        
        /// <summary>
        /// 두 개의 스택으로 구현한 큐 자료구조
        /// </summary>
        /// <typeparam name="T">저장할 데이터 타입</typeparam>
        public class MyQueue<T>
        {
            private readonly Stack<T> stackEnqueue;
            private readonly Stack<T> stackDequeue;

            public MyQueue()
            {
                stackEnqueue = new Stack<T>();
                stackDequeue = new Stack<T>();
            }

            public int Size()
            {
                return stackEnqueue.Count + stackDequeue.Count;
            }

            public void Enqueue(T value)
            {
                stackEnqueue.Push(value);
            }

            private void SwitchStack()
            {
                // 꺼낼 스택이 비어있을 때만 스택 데이터를 옮긴다.
                if (stackDequeue.Count == 0)
                {
                    while (stackEnqueue.Count != 0)
                    {
                        stackDequeue.Push(stackEnqueue.Pop());
                    }
                }
            }

            public T Peek()
            {
                SwitchStack();

                return stackDequeue.Peek();
            }

            public T Dequeue()
            {
                SwitchStack();

                return stackDequeue.Pop();
            }
        }

        #endregion

        #region Question 3.6
        /*
         * 큰 값이 위로 오도록 스택을 오름차순 정렬하는 프로그램을 작성하라.
         * 여벌 스택은 하나까지만 사용할 수 있고, 스택에 보관된 요소를 배열 등의 다른 자료구조로는 복사할 수 없다.
         * 스택은 push, pop, peek, isEmpty의 네 가지 연산을 제공한다.
         */
        
        public static Stack<int> Q6_Sort(Stack<int> stack)
        {
            var sortedStack = new Stack<int>();
            while(stack.Count != 0)
            {
                var t = stack.Pop();

                while (sortedStack.Count != 0 && sortedStack.Peek() > t)
                {
                    stack.Push(sortedStack.Pop());
                }
                sortedStack.Push(t);
            }

            return sortedStack;
        }

        #endregion

        #region Question 3.7
        /*
         * 먼저 들어온 동물이 먼저 나가는 동물쉼터가 있다고 하자. 쉼터는 개와 고양이만 수용할 수 있다.
         * 사람들은 쉼터의 동물들 가운데 들어온 지 가장 오래된 동물부터 입양할 수 있는데, 개와 고양이 중,
         * 어떤 동물을 데려갈지 선택할 수도 있다. 특정한 동물을 지정해 데려가는 것은 금지되어 있다.
         * (enqueue, dequeueAny, dequeueDog, dequeueCat의 연산을 제공해야 한다.)
         */
        public class AnimalShelter
        {
            public abstract class Animal : IComparable<Animal>
            {
                public DateTime Time { get; }
                public string Name { get; }

                protected Animal(string name, DateTime time)
                {
                    this.Name = name;
                    this.Time = time;
                }

                public int CompareTo(Animal other)
                {
                    return this.Time.CompareTo(other.Time);
                }

                #region Method Overloading
                public static bool operator >(Animal b1, Animal b2)
                {
                    return (b1.CompareTo(b2) > 0);
                }

                public static bool operator <(Animal b1, Animal b2)
                {
                    return (b1.CompareTo(b2) < 0);
                }
                #endregion
            }

            public class Cat : Animal
            {
                public Cat(string name, DateTime time) : base(name, time)
                {

                }
            }

            public class Dog : Animal
            {
                public Dog(string name, DateTime time) : base(name, time)
                {

                }
            }

            private Queue<Dog> dogs;
            private Queue<Cat> cats;

            public AnimalShelter()
            {
                this.dogs = new Queue<Dog>();
                this.cats = new Queue<Cat>();
            }

            public void Enqueue(Animal animal)
            {
                switch (animal)
                {
                    case Dog d:
                        this.dogs.Enqueue(animal as Dog);
                        break;
                    case Cat c:
                        this.cats.Enqueue(animal as Cat);
                        break;
                }
            }

            public Animal DequeueAny()
            {
                if(dogs.Count == 0)
                {
                    return this.DequeueCat();
                }

                if(cats.Count == 0)
                {
                    return this.DequeueDog();
                }

                if(dogs.Peek() < cats.Peek())
                {
                    return this.DequeueDog();
                }
                else
                {
                    return this.DequeueCat();
                }
            }

            public Animal DequeueCat()
            {
                try
                {
                    return this.cats.Dequeue();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }

            public Animal DequeueDog()
            {
                try
                {
                    return this.dogs.Dequeue();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                
            }
        }
        #endregion
    }
}
