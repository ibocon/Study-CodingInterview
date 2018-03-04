using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    // 객체를 설계하는 방법은 정말 다양하고 개발자마다 다른 특색을 나타낸다.
    // 따라서 책 해답을 C# 코드로 전환하고 테
    public static class ObjectOrientedDesign
    {
        #region Question 8.1
        /*
         * 카드 게임에 쓰이는 카드 한 벌을 표현하기 위한 자료구조를 설계하라.
         * 블랙잭 게임을 구현하려면 이 자료구조의 하위 클래스를 어떻게 만들어야 하는지 설명하라.
         */ 
        
        // 문제에 블랙잭 게임이 구체적으로 어떻게 진행되는지에 대한 설명이 없어, 책 해답을 C# 코드로 정리만 했다.
        // 이 문제에서 반드시 알아야 하는 부분은 '상속과 제네릭의 활용'이다.

        public enum Suit
        {
            Club = 0, Diamond = 1, Heart = 2, Spade = 3
        }

        public abstract class Card
        {
            public bool Available { get; set; }
            public Suit Suit { get; private set; }
            public int Number { get; private set; }

            public Card(Suit suit, int number)
            {
                Available = true;
                Suit = suit;
                Number = number;
            }
        }

        public class Deck<T> where T : Card
        {
            IList<T> cards;
            int dealtIndex;

            public int RemainingCards
            {
                get { return cards.Count - dealtIndex; }
            }

            public Deck(IList<T> cards)
            {
                this.cards = cards;
                dealtIndex = 0;
            }

            public void Shuffle()
            {
                throw new NotImplementedException();
            }

            public T[] DealHand(int number)
            {
                throw new NotImplementedException();
            }

            public T DealCard()
            {
                throw new NotImplementedException();
            }
        }

        public class Hand<T> where T : Card
        {
            IList<T> cards;

            public Hand()
            {
                cards = new List<T>();
            }

            public virtual int Score()
            {
                var score = 0;
                foreach(T card in cards)
                {
                    score += card.Number;
                }

                return score;
            }

            public void AddCard(T card)
            {
                cards.Add(card);
            }
        }

        public class BlackJackHand : Hand<BlackJackCard>
        {
            public bool Busted
            {
                get { return Score() > 21; }
            }

            public bool Is21
            {
                get { return Score() == 21; }
            }

            public bool IsBlackJack
            {
                get { throw new NotImplementedException(); }
            }

            public override int Score()
            {
                var scores = PossibleScores();
                var maxUnder = int.MaxValue;
                var minOver = int.MinValue;

                foreach(var score in scores)
                {
                    if(score > 21 && score < minOver)
                    {
                        minOver = score;
                    }
                    else if (score <= 21 && score > maxUnder)
                    {
                        maxUnder = score;
                    }
                }

                return maxUnder == int.MinValue ? minOver : maxUnder;
            }

            private IList<int> PossibleScores()
            {
                throw new NotImplementedException();
            }
        }

        public class BlackJackCard : Card
        {
            public bool IsAce
            {
                get { return Number == 1; }
            }

            public int Score
            {
                get
                {
                    if (IsAce) { return 1; }
                    else if (11 <= Number && Number <= 13) return 10;
                    else return Number;
                }
            }

            public int MaxValue
            {
                get { return (IsAce ? 11 : Score); }
            }

            public int MinValue
            {
                get { return (IsAce ? 1 : Score); }
            }

            public bool IsFaceCard
            {
                get { return (11 <= Number && Number <= 13); }
            }

            public BlackJackCard(Suit suit, int number) : base(suit, number) { }
        }

        #endregion

        #region Question 8.2
        /*
         * 고객응대담당자, 관리자 그리고 감독관이라는 세 부류직원들로 구성된 콜 센터가 있다고 하자.
         * 콜 센터로 오는 전화는 처음에는 무조건 상담 가능 고객응대담당자로 연결된다.
         * 고객응대담당자가 처리할 수 없는 전화는 관리자로 연결된다.
         * 관리자가 처리할 수 없는 전화는 다시 감독관에게 연결된다.
         * 이 문제를 풀기 위한 자료구조를 설계하라.
         * 응대 가능한 첫 번째 직원에게 전화를 연결시키는 dispatchCall 메서드를 구현하라.
         */
        
        // 책의 해답과 달리, C#은 프레임워크 디자인 가이드라인에 따라 작성한다.
        // https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines
        public class CallHandler
        {
            // 직급은 3레벨로 나뉜다. 고객응대담당자, 관리자, 감독관
            public enum Rank
            {
                Responder, Manager, Director
            }

            // 10명의 담당자와 4명의 관리자, 2명의 감독관을 만들어 초기화
            private readonly int NumberOfResponders = 10;
            private readonly int NumberOfManagers = 4;
            private readonly int NumberOfDirectors = 2;

            // Singleton 패턴에 따라 객체 반환
            private static CallHandler instance;
            public static CallHandler Instance
            {
                get
                {
                    if (instance is null)
                    {
                        instance = new CallHandler();
                    }

                    return instance;
                }
            }

            // 직급별 직원 리스트
            IDictionary<Rank, List<Employee>> Employees;
            IDictionary<Rank, Queue<Call>> CallQueues;

            protected CallHandler()
            {
                Employees = new Dictionary<Rank, List<Employee>>();
                CallQueues = new Dictionary<Rank, Queue<Call>>();
            }

            // 전화 응대 가능한 첫 직원 가져오기
            public Employee GetHandlerForCall(Call call)
            {
                throw new NotImplementedException();
            }

            // 응대 가능한 직원에게 전화를 연결하거나, 가능한 직원이 없으면 큐에 보관
            public void DispatchCall(Caller caller)
            {
                var call = new Call(caller);
                DispatchCall(call);
            }

            // 응대 가능한 직원에게 전화를 연결하거나, 간읗나 직원이 없으면 큐에 보관
            public void DispatchCall(Call call)
            {
                // 직급이 낮은 직원에 연결 시도
                var employee = GetHandlerForCall(call);
                if(employee is null)
                {
                    employee.ReceiveCall(call);
                }
            }
        }

        public class Call
        {
            public Call(Caller caller)
            {
                throw new NotImplementedException();
            }
        }

        public abstract class Employee
        {
            private Call currentCall;

            // 직원이 상담중인지 아닌지를 반환
            public bool IsFree
            {
                get
                {
                    return (currentCall is null);
                }
            }

            public CallHandler.Rank Rank
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            // 고객 상담 시작
            public virtual void ReceiveCall(Call call)
            {
                throw new NotImplementedException();
            }

            // 문제가 해결되었으므로 상담 종료
            public virtual void CallCompleted(Call call)
            {
                throw new NotImplementedException();
            }

            // 문제가 해결되지 않았음. 상위 직급 직원에게 새로 걸려온 전화 배정
            public virtual void EscalateAndReassign(Call call)
            {
                throw new NotImplementedException();
            }

            // 상담 중이지 않은 경우, 직원에게 새로 걸려온 전화 배정
            public virtual bool AssignNewCall()
            {
                throw new NotImplementedException();
            }

        }

        public abstract class Caller
        {

        }

        #endregion
    } 
}
