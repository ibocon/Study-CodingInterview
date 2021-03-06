﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    // 객체를 설계하는 방법은 정말 다양하고 개발자마다 다른 특색을 나타낸다.
    // 따라서 책 해답을 C# 코드로 전환하고 테스트했다.
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

            // Singleton 패턴에 따라 객체 반환
            private static CallHandler instance;
            public static CallHandler GetInstance(int numberOfResponders, int numberOfManagers, int numberOfDirectors)
            {
                if(instance is null)
                {
                    instance = new CallHandler(numberOfResponders, numberOfManagers, numberOfDirectors);
                }

                return instance;
            }

            // 직급별 직원 리스트
            IDictionary<Rank, List<Employee>> Employees;
            IDictionary<Rank, Queue<Call>> CallQueues;

            protected CallHandler(int numberOfResponders, int numberOfManagers, int numberOfDirectors)
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
                    call.SetHandler(employee);
                }
                else
                {
                    // 직급에 따라 대기 큐에 수신 전화를 삽입
                    call.Reply("Please wait for free employee to reply");
                    CallQueues[call.Rank].Enqueue(call);
                }
            }

            // 가용한 직원 발견. 해당 직원이 처리해야 할 전화를 큐에서 탐색
            // 새로운 전화를 배정했다면 true를 그렇지 않으면 false 반환
            public bool AssignCall(Employee employee)
            {
                throw new NotImplementedException();
            }
        }

        public class Call
        {
            // 전화를 처리할 수 있는 가장 낮은 직급
            public CallHandler.Rank Rank { get; private set; }

            // 전화를 거는 사람
            private Caller caller;

            // 응대 중인 직원
            private Employee handler;

            public Call(Caller caller)
            {
                Rank = CallHandler.Rank.Responder;
                this.caller = caller;
            }

            public void SetHandler(Employee employee)
            {
                throw new NotImplementedException();
            }

            public void Reply(string message)
            {
                throw new NotImplementedException();
            }

            public void IcrementRank()
            {
                throw new NotImplementedException();
            }

            public void Disconnect()
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

            public CallHandler.Rank Rank { get; protected set; }

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

        class Director : Employee
        {
            public Director()
            {
                Rank = CallHandler.Rank.Director;
            }
        }

        class Manager : Employee
        {
            public Manager()
            {
                Rank = CallHandler.Rank.Manager;
            }
        }

        class Respondent : Employee
        {
            public Respondent()
            {
                Rank = CallHandler.Rank.Responder;
            }
        }

        #endregion

        #region Question 8.3
        /*
         * 객체 지향 원칙에 따라 주크박스를 설계하라. 
         */

        // 객체 지향 설계 시, 설계 관련 제약사항을 명확하게 해 두는 것이 필요하다.

        /*
         * <시스템 구성요소>
         * Jukebox
         * CD
         * Song
         * Artist
         * Playlist
         * Display
         */

        // 시스템 구성요소가 어떻게 객체로 설계되는지 파악해보자

        public class JukeBox
        {
            private CDPlayer cdPlayer;
            private UserQ3 user;
            
        }

        public class CDPlayer
        {
            public Playlist Playlist { get; set; }
            public CD CD { get; set; }

            #region Constructor
            public CDPlayer(CD cd, Playlist playlist)
            {
                CD = cd;
                Playlist = playlist;
            }
            public CDPlayer(Playlist playlist)
            {
                Playlist = playlist;
            }
            public CDPlayer(CD cd)
            {
                CD = cd;
            }
            #endregion

            public void PlaySong(Song s)
            {
                throw new NotImplementedException();
            }
        }

        public class Playlist
        {
            private Song song;
            private Queue<Song> queue;
            public Playlist(Song song, Queue<Song> queue)
            {
                this.song = song;
                this.queue = queue;
            }

            public Song GetNextToPlay()
            {
                return queue.Peek();
            }

            public void QueueUpSong(Song song)
            {
                queue.Enqueue(song);
            }
        }

        public class CD
        {
            // id, 아티스트, 곡 목록 등의 정보 보관
        }

        public class Song
        {
            // id, CD (null일 수 있다), 곡명, 길이 등의 정보 보관
        }

        public class UserQ3
        {
            public string Name { get; set; }
            public long ID { get; set; }

            public UserQ3(string name, long id)
            {
                Name = name;
                ID = id;
            }

            public static UserQ3 AddUser(string name, long id)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Question 8.4
        /*
         * 객체 지향 원칙에 따라 주차장을 설계하라
         */

        public class ParkingSpot
        {
            private Vehicle vehicle;
            public VehicleSize SpotSize { get; }
            public int Row { get; }
            public int SpotNumber { get; }
            private Level level;

            public ParkingSpot(Level level, int row, int spotNumber, VehicleSize spotSize)
            {
                this.level = level;
                Row = row;
                SpotNumber = spotNumber;
                SpotSize = spotSize;
            }

            public bool IsAvailable
            {
                get { return vehicle == null; }
            }

            // 공간이 충분히 크고 가용한지 반환
            public bool CanFitVehicle(Vehicle vehicle)
            {
                return IsAvailable && vehicle.CanFitInSpot(this);
            }

            // 해당 공간에 차량 주차
            public bool Park(Vehicle v)
            {
                if (!CanFitVehicle(v))
                {
                    return false;
                }
                vehicle = v;
                vehicle.ParkInSpot(this);
                return true;
            }

            // 해당 주차 공간에 차량 제거. 주차 공간이 속한 층에는 빈 자리가 생겼다고 통보
            public void RemoveVehicle()
            {
                level.SpotFreed();
                vehicle = null;
            }

            public void Print()
            {
                if (vehicle == null)
                {
                    switch (SpotSize)
                    {
                        case VehicleSize.Compact:
                            Console.Write("c");
                            break;
                        case VehicleSize.Large:
                            Console.Write("l");
                            break;
                        case VehicleSize.Motorcycle:
                            Console.Write("m");
                            break;
                    }
                }
                else
                {
                    vehicle.Print();
                }
            }
        }

        public class ParkingLot
        {
            private IList<Level> levels;
            private int numberOfLevels;

            public ParkingLot(int numberOfLevels)
            {
                this.numberOfLevels = numberOfLevels;
                levels = new List<Level>();
                for (int index = 0; index < numberOfLevels; index++)
                {
                    levels.Add(new Level(index, 30));
                }
            }

            // 차량을 특정 장소(또는 여러장소에 걸쳐)에 주차한다. 
            // 실패하면 false를 반환한다.
            public bool ParkVehicle(Vehicle vehicle)
            {
                foreach (var level in levels)
                {
                    if (level.ParkVehicle(vehicle))
                    {
                        return true;
                    }
                }
                return false;
            }

            public void Print()
            {
                for (int index = 0; index < levels.Count; index++)
                {
                    Console.Write("Level" + index + ": ");
                    levels[index].Print();
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public enum VehicleSize
        {
            Motorcycle,
            Compact,
            Large,
        }

        public abstract class Vehicle
        {
            protected IList<ParkingSpot> parkingSpots;
            protected String licensePlate;
            public int SpotsNeeded { get; protected set; }
            public VehicleSize Size { get; protected set; }

            // 주어진 주차 공간에 차량 추자 (다른 차량 사이에 주차하게 될 수 있음)
            public void ParkInSpot(ParkingSpot spot)
            {
                parkingSpots = new List<ParkingSpot>
                {
                    spot
                };
            }

            // 출차. 해당 주차 공간에는 차량이 출차되었음을 통보
            public void ClearSpots()
            {
                foreach (var parkingSpot in parkingSpots)
                {
                    parkingSpot.RemoveVehicle();
                }
                parkingSpots.Clear();
            }

            // 주차 공간이 주차하려는 차량을 수용할 수 있으며, 비어 있는지 확인.
            // 크기만 비교하며, 주차 장소가 충분히 있는지는 확인하지 않음
            public abstract bool CanFitInSpot(ParkingSpot spot);
            public abstract void Print();
        }

        public class Motorcycle : Vehicle
        {
            public Motorcycle()
            {
                SpotsNeeded = 1;
                Size = VehicleSize.Motorcycle;
            }

            public override bool CanFitInSpot(ParkingSpot spot)
            {
                return true;
            }

            public override void Print()
            {
                Console.Write("M");
            }
        }
        public class Car : Vehicle
        {
            public Car()
            {
                SpotsNeeded = 1;
                Size = VehicleSize.Compact;
            }

            // 주차 공간이 소형인지 대형인지 확인
            public override bool CanFitInSpot(ParkingSpot spot)
            {
                return spot.SpotSize == VehicleSize.Large || spot.SpotSize == VehicleSize.Compact;
            }

            public override void Print()
            {
                Console.Write("C");
            }
        }
        public class Bus : Vehicle
        {
            public Bus()
            {
                SpotsNeeded = 5;
                Size = VehicleSize.Large;
            }

            // 주차 공간이 큰지 확인. 주차 공간 수는 검사하지 않음
            public override bool CanFitInSpot(ParkingSpot spot)
            {
                return spot.SpotSize == VehicleSize.Large;
            }

            public override void Print()
            {
                Console.Write("B");
            }
        }

        // 주차장 내의 한 층을 표현
        public class Level
        {
            private static readonly int SPOTS_PER_ROW = 10;

            private int floor;
            private ParkingSpot[] spots;
            private int availableSpots;

            public Level(int floor, int numberSpots)
            {
                this.floor = floor;
                spots = new ParkingSpot[numberSpots];
                int largeSpots = numberSpots / 4;
                int bikeSpots = numberSpots / 4;
                int compactSpots = numberSpots - largeSpots - bikeSpots;
                for (int i = 0; i < numberSpots; i++)
                {
                    VehicleSize sz = VehicleSize.Motorcycle;
                    if (i < largeSpots)
                    {
                        sz = VehicleSize.Large;
                    }
                    else if (i < largeSpots + compactSpots)
                    {
                        sz = VehicleSize.Compact;
                    }
                    int row = i / SPOTS_PER_ROW;
                    spots[i] = new ParkingSpot(this, row, i, sz);
                }
                availableSpots = numberSpots;
            }

            // 주어진 차량을 주차할 장소를 찾는다. 실패하면 false 반환
            public bool ParkVehicle(Vehicle vehicle)
            {
                if (availableSpots < vehicle.SpotsNeeded)
                {
                    return false;
                }
                int spotNumber = FindAvailableSpots(vehicle);
                if (spotNumber < 0)
                {
                    return false;
                }
                return ParkStartingAtSpot(spotNumber, vehicle);
            }

            // 차량을 SpotNumber가 가리키는 장소부터 vehicle.SpotsNeeded 만큼의 빈 자리에 주차시킨다.
            private bool ParkStartingAtSpot(int spotNumber, Vehicle vehicle)
            {
                vehicle.ClearSpots();
                bool success = true;
                for (int i = spotNumber; i < spotNumber + vehicle.SpotsNeeded; i++)
                {
                    success &= spots[i].Park(vehicle);
                }
                availableSpots -= vehicle.SpotsNeeded;
                return success;
            }

            // 이 차랑을 주차할 장소를 찾는다. 빈 자리를 가리키는 Index를 반환한다.
            // 실패하면 -1을 반환한다.
            private int FindAvailableSpots(Vehicle vehicle)
            {
                int spotsNeeded = vehicle.SpotsNeeded;
                int lastRow = -1;
                int spotsFound = 0;
                for (int i = 0; i < spots.Length; i++)
                {
                    ParkingSpot spot = spots[i];
                    if (lastRow != spot.Row)
                    {
                        spotsFound = 0;
                        lastRow = spot.Row;
                    }
                    if (spot.CanFitVehicle(vehicle))
                    {
                        spotsFound++;
                    }
                    else
                    {
                        spotsFound = 0;
                    }
                    if (spotsFound == spotsNeeded)
                    {
                        return i - (spotsNeeded - 1);
                    }
                }
                return -1;
            }

            public void Print()
            {
                int lastRow = -1;
                for (int i = 0; i < spots.Length; i++)
                {
                    ParkingSpot spot = spots[i];
                    if (spot.Row != lastRow)
                    {
                        Console.Write("  ");
                        lastRow = spot.Row;
                    }
                    spot.Print();
                }
            }

            // 한 차량이 출자하면, 가용한 주차장소의 수를 증가시킨다.
            public void SpotFreed()
            {
                availableSpots++;
            }
        }
        #endregion

        #region Question 8.5
        /*
         * 온라인 북 리더에 대한 자료구조를 설계하라.
         */ 

        /*
         * 다음 기능을 제공하는 기본적인 온라인 북 리더를 설계한다고 가정
         * 
         * 1 사용자 가입 정보 생성 및 확장
         * 2 서적 데이터베이스 검색
         * 3 책 읽기
         * 4 한 번에 한 명의 사용자만 활성화 상태
         * 5 활성화 된 사용자가 읽는 한 권의 책만 활성화 상태
         */

        // 작은 시스템이라 하더라도, 역할을 잘 분배하여 클래스를 나누어 두면
        // 시스템이 성장할 때 주 클래스가 엄청나게 비대해지는 것을 막을 수 있다.
        
        public class OnlineReaderSystem
        {
            public Library Library { get; private set; }
            public UserManager UserManager { get; private set; }
            public Display Display { get; private set; }

            private Book activeBook;
            public Book ActiveBook
            {
                get
                {
                    return activeBook;
                }
                set
                {
                    activeBook = value;
                    Display.DisplayBook(value);
                }
            }

            private UserQ5 activeUser;
            public UserQ5 ActiveUser
            {
                get
                {
                    return activeUser;
                }
                set
                {
                    activeUser = value;
                    Display.DisplayUser(value);
                }
            }

            public OnlineReaderSystem()
            {
                UserManager = new UserManager();
                Library = new Library();
                Display = new Display();
            }
        }

        public class Library
        {
            private IDictionary<int, Book> books;

            public Book AddBook(int id, string details)
            {
                if (books.ContainsKey(id))
                {
                    return null;
                }

                var book = new Book(id, details);
                if(books.TryAdd(id, book))
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }

            public bool Remove(Book book)
            {
                return books.Remove(book.Id);
            }

            public bool Remove(int id)
            {
                if (books.ContainsKey(id))
                {
                    return books.Remove(id);
                }
                else
                {
                    return false;
                }
            }

            public Book Find(int id)
            {
                if (books.ContainsKey(id))
                {
                    return books[id];
                }
                else
                {
                    return null;
                }
            }
        }

        public class UserManager
        {
            private IDictionary<int, UserQ5> users;

            public UserQ5 AddUser(int id, string details, int accountType)
            {
                if (users.ContainsKey(id))
                {
                    return null;
                }

                var user = new UserQ5(id, details, accountType);
                if (users.TryAdd(id, user))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }

            public bool Remove(UserQ5 user)
            {
                return users.Remove(user.Id);
            }

            public bool Remove(int id)
            {
                if (users.ContainsKey(id))
                {
                    return users.Remove(id);
                }
                else
                {
                    return false;
                }
            }

            public UserQ5 Find(int id)
            {
                if (users.ContainsKey(id))
                {
                    return users[id];
                }
                else
                {
                    return null;
                }
            }
        }

        public class Display
        {
            private Book activeBook;
            private UserQ5 activeUser;
            private int pageNumber = 0;

            public void TurnPageForward()
            {
                pageNumber++;
                RefreshPage();
            }

            public void TurnPageBackward()
            {
                pageNumber--;
                RefreshPage();
            }

            public void DisplayBook(Book book)
            {
                pageNumber = 0;
                activeBook = book;

                RefreshTitle();
                RefreshDetails();
                RefreshPage();
            }

            public void DisplayUser(UserQ5 user)
            {
                activeUser = user;
                RefreshUsername();
            }

            public void RefreshUsername() { /* 화면에 표시되는 username 갱신 */ }
            public void RefreshTitle() { /* 화면에 표시되는 title 갱신 */ }
            public void RefreshDetails() { /* 화면에 표시되는 details 갱신 */ }
            public void RefreshPage() { /* 화면에 표시되는 page 갱신 */ }

        }

        public class Book
        {
            public int Id { get; set; }
            public string Details { get; set; }

            public Book(int id, string details)
            {
                Id = id;
                Details = details;
            }
        }

        public class UserQ5
        {
            public int Id { get; set; }
            public string Details { get; set; }
            public int AccountType { get; set; }

            public void RenewMemberShip() { }

            public UserQ5(int id, string details, int accountType)
            {
                Id = id;
                Details = details;
                AccountType = accountType;
            }
        }

        #endregion

        #region Question 8.6
        /*
         * 직소 퍼즐을 구현하라. 자료구조를 설계하고, 퍼즐을 푸는 알고리즘을 설명하라.
         * 주어진 두 개의 조각이 들어맞는지를 판별하는 fitsWith 메서드가 주어진다고 가정하도록 하라.
         */

        /*
         * 절대 위치: 절대 위치는 Piece 클래스에 저장되어야 한다.
         * 상대 위치: 상대 위치는 Edge 클래스 내부에 기록되어야 한다.
         */

        // 여기서는 상대 위치를 사용하여 문제를 푼도록 할 것이다. 
        // 인접한 면에 면을 연결하여 상대 위치를 표현.


        #endregion

        #region Question 8.7
        /*
         * 채팅 서버를 어떻게 구현할 것인지 설명하라.
         * 서버를 뒷받침할 다양한 컴포넌트, 클래스, 메서드에 대해 설명하도록 하라.
         * 어떤 문제가 가장 풀기 어려울 것으로 예상되는가?
         * 
         */

        /*
         * 아래 순서대로 채팅 서버를 구현해 나갈 것이다.
         * 
         * 1. 사용자 입장에서 채팅 서버를 활용하는 시나리오를 작성한다.
         * 2. 사용자 시나리오를 구현하기 위해 필요한 기능들을 추려낸다.
         * 3. 추려낸 기능들을 구현하기 위한 기술을 검토한다.
         * 4. 검토한 기술을 바탕으로 핵심 컴포넌트 간에 상호작용을 그려본다.
         * 5. 각 핵심 컴포넌트에 들어갈 메서드를 정의한다.
         * 6. 메서드에서 기대되는 결과를 바탕으로 테스트를 작성한다.
         * 7. 핵심 컴포넌트를 하나씩 구현하며 테스트 결과를 확인한다.
         * 8. 핵심 컴포넌트를 연결하여 사용자 시나리오대로 동작하는지 확인한다.
         * 
         * 서버를 뒷받침할 컴포넌트, 클래스, 메서드는 아래 코드를 확인하길 바란다.
         * 
         * 가장 풀기 어려울 것으로 예상되는 문제는 '서버의 유지보수'다.
         * 데이터베이스를 확장하거나 축소할 때 기존 사용자나 기존 채팅 내역을 유지하는 것.
         * 서버에 문제가 생겼을 때를 대비하여 백업하거나 백업 서버를 즉시 준비하는 것.
         * 급격한 사용자 증가 및 축소에 대응할 수 있도록 클라우드 서버를 유연하게 운영하는 것.
         * 기능의 추가나 삭제로 사용자의 행동이 변화하면 이를 파악하고 적절히 대응하는 것.
         * 
         */

        /// <summary>
        /// 채팅 서버
        /// </summary>
        public static class ChatServer
        {
            public static ChatDbContext ChatContext;
            public static ChatUserController ChatUserController;
            public static ChatRoomController ChatRoomController;
            public static bool Running = true;

            public static void Run(string[] args)
            {
                ChatContext = new ChatDbContext("db://localhost:3306");
                ChatUserController = new ChatUserController(ChatContext.ChatUsers);
                ChatRoomController = new ChatRoomController(ChatContext.ChatRooms);
                
                while (Running)
                {
                    throw new NotImplementedException();
                }
            }

            public static void Stop()
            {
                Running = false;
            }
        }

        /// <summary>
        /// Repository 패턴 기반으로 데이터베이스와 상호작용을 담당
        /// 데이터베이스와 연결 및 Repository 생성
        /// </summary>
        public class ChatDbContext
        {
            public ChatUserRepository ChatUsers { get; }
            public ChatRoomRepository ChatRooms { get; }

            public ChatDbContext(string dbConnection)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 기본적인 Repository 기능 정의하는 인터페이스
        /// </summary>
        /// <typeparam name="T">테이블에 기록되는 데이터 타입</typeparam>
        public abstract class Repository<T> 
        {
            public virtual IList<T> ReadAll() { throw new NotImplementedException(); }
            public virtual T Create(T model) { throw new NotImplementedException(); }
            public virtual T ReadById(int id) { throw new NotImplementedException(); }
            public virtual T Update(T model) { throw new NotImplementedException(); }
            public virtual T Delete(T model) { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ChatRoom 테이블과 상호작용하는 Repository
        /// </summary>
        public class ChatRoomRepository : Repository<ChatRoom>
        {
            public IList<ChatRoom> ReadAllByUser(int userId) { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ChatUser 테이블과 상호작용하는 Repository
        /// </summary>
        public class ChatUserRepository : Repository<ChatUser>
        {

        }

        /// <summary>
        /// ChatUser 관리하는 Controller
        /// Repository와 상호작용하여 데이터 관리
        /// Chatuser 간에 상호작용 관리
        /// </summary>
        public class ChatUserController 
        {
            private ChatUserRepository repository;
            public ChatUserController(ChatUserRepository chatRoomRepository)
            {
                repository = chatRoomRepository;
            }

            public void AddChatUser(ChatUser chatUser)
            {
                repository.Create(chatUser);
            }

            public void RemoveChatuser(ChatUser chatUser) { throw new NotImplementedException(); }

        }

        /// <summary>
        /// 채팅 서버 사용자 모델
        /// </summary>
        public class ChatUser 
        {
            public int Id { get; private set; }
            public string Name { get; private set; }

            private IList<ChatRoom> JoinedChat;

            public ChatUser(int userId, string name)
            {
                Id = userId;
                Name = name;
            }

            public void SendMessage(int chatId, Message message)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 채팅방을 관리하는 Controller
        /// </summary>
        public class ChatRoomController 
        {
            private ChatRoomRepository repository;
            public ChatRoomController(ChatRoomRepository chatRoomRepository)
            {
                repository = chatRoomRepository;
            }

            public IList<ChatRoom> GetUserJoinedChat(int userId)
            {
                return repository.ReadAllByUser(userId);
            }

            public void JoinChatRoom(ChatRoom chatRoom, ChatUser user) { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 채팅 서버 채팅방 모델
        /// </summary>
        public abstract class ChatRoom 
        {
            private IList<Message> messages;

            public void RecordMessage(Message message)
            {
                messages.Add(message);
            }

            protected abstract void NotifyNewMessage();
        }

        /// <summary>
        /// 1대1 채팅방 모델
        /// </summary>
        public class PrivateChatRoom : ChatRoom 
        {
            private ChatUser userA;
            private ChatUser userB;

            protected override void NotifyNewMessage() { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 그룹 채팅방 모델
        /// </summary>
        public class GroupChatRoom : ChatRoom 
        {
            private IList<ChatUser> users;

            protected override void NotifyNewMessage() { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 채팅 메세지 모델
        /// </summary>
        public class Message 
        {
            private ChatUser fromUser;
            private string message;
        }
        #endregion
    }
}
