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
    }
}
