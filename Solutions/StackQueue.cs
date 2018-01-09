using System;

namespace Solutions
{
    /// <summary>
    /// Question 3.1
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

    public class StackQueue
    {
        #region Question 3.1

        #region Properties
        /// <summary>
        /// Question 3.1
        /// 내부 스택 개수
        /// </summary>
        public const int NumberOfStacks = 3;

        /// <summary>
        /// Question 3.1
        /// 내부 스택 기본 크기
        /// </summary>
        const int DefaultSize = 4;

        /// <summary>
        /// Question 3.1
        /// 전체 스택 크기
        /// </summary>
        const int TotalSize = DefaultSize * NumberOfStacks;

        /// <summary>
        /// Question 3.1
        /// 내부 스택 메타데이터
        /// </summary>
        readonly StackData[] _stacks =
        {
            new StackData(0, DefaultSize),
            new StackData(DefaultSize, DefaultSize),
            new StackData(DefaultSize * 2, DefaultSize)
        };

        /// <summary>
        /// Question 3.1
        /// 전체 스택
        /// </summary>
        readonly int[] _buffer = new int[TotalSize];

        #endregion

        #region Methods
        /// <summary>
        /// Question 3.1
        /// 내부 스택에 저장된 데이터 개수를 전부 더한 개수를 계산한다.
        /// </summary>
        /// <returns>전체 데이터 개수</returns>
        private int NumberOfElements()
        {
            return _stacks[0].Size + _stacks[1].Size + _stacks[2].Size;
        }

        /// <summary>
        /// Question 3.1
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
        /// Question 3.1
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
        /// Question 3.1
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
        /// Question 3.1
        /// <paramref name="stackNum"/>번째 내부 스택의 최대크기를 하나 증가시킨다.
        /// </summary>
        /// <param name="stackNum">내부 스택 번호</param>
        /* Expand stack by shifting over other stacks */
        public void Expand(int stackNum)
        {
            //내부 스택의 용량을 늘리기 전에 다음 스택을 한칸 이동시켜 공간을 확보한다.
            Shift((stackNum + 1) % NumberOfStacks);
            _stacks[stackNum].Capacity++;
        }

        /// <summary>
        /// Question 3.1
        /// 
        /// </summary>
        /// <param name="stackNum"></param>
        /// <param name="value"></param>
        public void Push(int stackNum, int value)
        {
            var stack = _stacks[stackNum];

            /* Check that we have space */
            if (stack.Size >= stack.Capacity)
            {
                if (NumberOfElements() >= TotalSize)
                { // Totally full
                    throw new Exception("Out of space.");
                }
                else
                { // just need to shift things around
                    Expand(stackNum);
                }
            }

            /* Find the index of the top element in the array + 1, 
		     * and increment the stack pointer */
            stack.Size++;
            stack.Pointer = NextElement(stack.Pointer);
            _buffer[stack.Pointer] = value;
        }

        /// <summary>
        /// Question 3.1
        /// </summary>
        /// <param name="stackNum"></param>
        /// <returns></returns>
        public int Pop(int stackNum)
        {
            var stack = _stacks[stackNum];

            if (stack.Size == 0)
            {
                throw new Exception("Trying to pop an empty stack.");
            }

            var value = _buffer[stack.Pointer];
            _buffer[stack.Pointer] = 0;
            stack.Pointer = PreviousElement(stack.Pointer);
            stack.Size--;

            return value;
        }

        /// <summary>
        /// Question 3.1
        /// </summary>
        /// <param name="stackNum"></param>
        /// <returns></returns>
        public int Peek(int stackNum)
        {
            var stack = _stacks[stackNum];

            return _buffer[stack.Pointer];
        }

        /// <summary>
        /// Question 3.1
        /// </summary>
        /// <param name="stackNum"></param>
        /// <returns></returns>
        public bool IsEmpty(int stackNum)
        {
            var stack = _stacks[stackNum];

            return stack.Size == 0;
        }
        #endregion

        #endregion
    }
}
