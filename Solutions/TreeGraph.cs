using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.Library;

namespace Solutions
{
    public class TreeGraph<T> where T : IComparable
    {
        #region Question 4.1
        // 주어진 이진 트리가 균형 이진 트리인지 판별하는 함수를 구현하라.
        // 이 문제에서 이진 트리는 어떤 노드의 두 자식 트리 깊이가 하나 이상 차이나지 않는 트리다.

        /// <summary>
        /// 변수 <paramref name="tree"/>이진 트리가 균형 이진 트리인지 판별한다.
        /// </summary>
        /// <param name="tree">균형을 검사할 이진 트리</param>
        /// <returns><paramref name="tree"/>이 균형이면 <code>true</code>를 반환한다.</returns>
        public static bool Q1_IsBalanced(BinaryTree<T> tree)
        {
            if (CheckHeight(tree.Root) is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 변수 <paramref name="node"/> 노드의 높이를 측정한다.
        /// </summary>
        /// <param name="node">높이를 검사할 이진트리 노드</param>
        /// <returns>노드의 높이를 반환한다. 다만, 균형이 깨졌을 경우 <code>null</code>을 반환한다.</returns>
        private static int? CheckHeight(BinaryTreeNode<T> node)
        {
            if (node == null) { return 0; } 

            var leftHeight = CheckHeight(node.Left);
            if (leftHeight == null) { return null; } 

            var rightHeight = CheckHeight(node.Right);
            if (rightHeight == null) { return null; } 

            var heightDiff = leftHeight - rightHeight;
            if (Math.Abs(heightDiff.GetValueOrDefault()) > 1) { return null; }
            else
            {
                return Math.Max(leftHeight.GetValueOrDefault(), rightHeight.GetValueOrDefault()) + 1;
            }
        }

        #endregion

        #region Question 4.2
        /*
         * 주어진 유향 그래프(directed graph)에서 특정한 두 노드 간에 경로(route)가 존재하는지를 판별하는 알고리즘을 구현하라.
         */

        public static bool Q2_Search(Graph<T> graph, GraphNode<T> start, GraphNode<T> end)
        {
            var nodeList = new LinkedList<GraphNode<T>>();

            // 모든 노드의 방문 상태를 초기화
            foreach (var node in graph.Nodes)
            {
                node.State = GraphNodeState.Unvisited;
            }

            //시작 노드 설정
            start.State = GraphNodeState.Visiting;
            nodeList.AddLast(start);

            // 그래프 너비 우선 탐색 시작
            while (nodeList.Count != 0)
            {
                var unvisited = nodeList.First.Value;
                nodeList.RemoveFirst();

                if (unvisited != null)
                {
                    // 인접 노드 확인
                    foreach (var adjacentNode in unvisited.Adjacent)
                    {
                        // 인접 노드를 방문하지 않은 상태인지 확인
                        if (adjacentNode.State == GraphNodeState.Unvisited)
                        {
                            // 원하던 목적지에 도달
                            if (adjacentNode == end)
                            {
                                return true;
                            }
                            else
                            {
                                // 현재 방문중인 인접 노드의 인접 노드를 추가
                                adjacentNode.State = GraphNodeState.Visiting;
                                nodeList.AddLast(adjacentNode);
                            }
                        }
                    }

                    // 모든 인접 노드를 확인하여 방문 완료 마킹
                    unvisited.State = GraphNodeState.Visited;
                }
            }

            return false;
        }

        /* C# 8.0 버전에는 지원될 수도?
         * 그래프 노드의 속성을 확장하는 방법이 좀더 객체지향적인 설계
        public extension GraphNodeState extends GraphNode { }
        */
        #endregion

        #region Question 4.3
        /* 오름차순으로 정렬된 배열로부터 그 높이가 가장 낮은 이진 탐색 트리를 생성하는 알고리즘을 작성하라.
         * 배열 내 모든 원소는 배열 내에서 유일한 값을 갖는다.
         */
        public static BinaryTreeNode<T> Q3_CreateMinimalBST(IEnumerable<T> array)
        {
            if(array.Count() == 0)
            {
                return null;
            }

            int midIndex = array.Count() / 2;
            IEnumerable<T> leftArray = array.Take(midIndex);
            IEnumerable<T> rightArray = array.Skip(midIndex + 1);

            BinaryTreeNode<T> node = new BinaryTreeNode<T>(array.ElementAt(midIndex))
            {
                Left = Q3_CreateMinimalBST(leftArray),
                Right = Q3_CreateMinimalBST(rightArray)
            };

            return node;
        }
        #endregion

        #region Question 4.4
        /* 주어진 이진 트리에서 깊이별로 연결 리스트를 만들어 내는 알고리즘을 작성하라.
         * (트리의 깊이가 D라면, 알고리즘 수행 결과로 D개의 연결 리스트가 만들어져야 한다.)
         */
        public static IList<LinkedList<BinaryTreeNode<T>>> Q4_CreateLevelLinkedList(BinaryTreeNode<T> root)
        {
            if(root == null) { return null; }

            IList<LinkedList<BinaryTreeNode<T>>> levels = new List<LinkedList<BinaryTreeNode<T>>>();
            var rootLevel = new LinkedList<BinaryTreeNode<T>>();
            rootLevel.AddLast(root);
            levels.Add(rootLevel);

            CreateALevelLinkedList(ref levels, rootLevel);

            return levels;
        }

        private static void CreateALevelLinkedList(ref IList<LinkedList<BinaryTreeNode<T>>> levels, LinkedList<BinaryTreeNode<T>> previewLevel)
        {
            var newLevel = new LinkedList<BinaryTreeNode<T>>();
            foreach(var parent in previewLevel)
            {
                if(parent.Left != null)
                {
                    newLevel.AddLast(parent.Left);
                }

                if (parent.Right != null)
                {
                    newLevel.AddLast(parent.Right);
                }
            }

            if (newLevel.Count() != 0)
            {
                levels.Add(newLevel);
                CreateALevelLinkedList(ref levels, newLevel);
            }
        }
        #endregion

        #region Question 4.5
        /*
         * 어떤 이진 트리가 이진 탐색 트리인지 판별하는 함수를 구현하라.
         */

        /// <summary>
        /// <paramref name="root"/>가 이진 탐색 트리가 맞는지 판별한다.
        /// </summary>
        /// <param name="root">검사할 이진 트리</param>
        /// <returns><paramref name="root"/>가 이진 트리가 맞다면 <code>true</code>를 반환한다.</returns>
        public static bool Q5_CheckBST(BinaryTreeNode<IComparable> root)
        {
            return CheckBST(root, null, null);
        }

        private static bool CheckBST(BinaryTreeNode<IComparable> node, IComparable min, IComparable max)
        {
            if(node == null) { return true; }

            if( (min != null && node.Data.CompareTo(min) <= 0) || (max != null &&  node.Data.CompareTo(max) >= 0))
            {
                return false;
            }

            if(!CheckBST(node.Left, min, node.Data) || !CheckBST(node.Right, node.Data, max))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Question 4.6
        /*
         * 정순회 기준으로, 이진 탐색 트리 내의 한 노드가 주어지면
         * 그 노드의 '다음'노드를 찾아내는 알고리즘을 작성하라.
         * (각 노드에는 부모 노드를 가리키는 링크가 존재한다고 가정한다.)
         */

        /// <summary>
        /// 이진 탐색 트리에서 <paramref name="node"/>의 다음 노드를 찾는다.
        /// </summary>
        /// <param name="node">다음 노드를 찾을 기준 노드</param>
        /// <returns><paramref name="node"/>의 다음 노드, 만약 마지막 노드라면 <code>null</code>을 반환한다.</returns>
        public static BinaryTreeNode<T> Q6_InorderFindSuccessor(BinaryTreeNode<T> node)
        {
            if (node is null) { return null; }

            // 노드가 루트 노드이거나, 오른쪽 트리가 존재하지 않을 경우
            if(node.Parent == null || node.Right != null)
            {
                return LeftMostChild(node.Right);
            }

            var current = node;
            var parent = node.Parent;

            // 다음 노드를 찾을 수 있도록, 완전히 순회를 끝내지 못한 노드를 찾는다.
            while (parent != null && parent.Left != current)
            {
                current = parent;
                parent = parent.Parent;
            }

            return parent;
        }
        
        /// <summary>
        /// <paramref name="node"/> 노드의 가장 왼쪽에 위치한 자식 노드를 반환한다.
        /// </summary>
        /// <param name="node">기준 노드</param>
        /// <returns>가장 외쪽에 위치한 자식 노드</returns>
        private static BinaryTreeNode<T> LeftMostChild(BinaryTreeNode<T> node)
        {
            if (node is null) { return null; }

            while(node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }
        #endregion

        #region Question 4.7
        /*
         * 이진 트리 내의 두 노드의 공통 선조 노드를 찾는 알고리즘을 설계하고 구현하라.
         * 자료구조 내에 부가적인 노드를 저장해 두는 일은 금한다.
         * (주의: 이진 탐색 트리가 아닐 수도 있다.)
         */

        /// <summary>
        /// <paramref name="root"/> 이진 트리 내 <paramref name="p"/>와 <paramref name="q"/>의 공통 선조 노드를 찾는다.
        /// </summary>
        /// <param name="root">탐색할 이진트리</param>
        /// <param name="p">공통 노드를 찾기 위한 기준 노드 P</param>
        /// <param name="q">공통 노드를 찾기 위한 기준 노드 Q</param>
        /// <returns>
        /// <paramref name="p"/>와 <paramref name="q"/>의 공통 조상을 반환한다.
        /// <paramref name="p"/>나 <paramref name="q"/>가 <paramref name="root"/>의 하위 트리 내에 없으면 <code>null</code>을 반환한다.
        /// <paramref name="p"/>가 <paramref name="q"/>의 하위 노드일 경우, <paramref name="q"/>의 조상 노드가 반환된다.
        /// <paramref name="p"/>나 <paramref name="q"/>가 루트 노드일 경우, <code>null</code>을 반환한다.
        /// </returns>
        public static BinaryTreeNode<T> Q7_GetCommonAncestor(BinaryTreeNode<T> root, BinaryTreeNode<T> p, BinaryTreeNode<T> q)
        {
            // root가 없거나 p 또는 q가 트리 내에 없다면 null을 반환한다.
            if(root is null || root == p || root == q || !CheckNodeExist(root, p) || !CheckNodeExist(root, q))
            {
                return null;
            }

            return FindCommonAncestor(root, p, q);
        }

        private static BinaryTreeNode<T> FindCommonAncestor(BinaryTreeNode<T> root, BinaryTreeNode<T> p, BinaryTreeNode<T> q)
        {
            if(root is null)
            {
                return null;
            }

            if(p == q)
            {
                return p.Parent;
            }

            if(root == p || root == q)
            {
                return root;
            }

            var leftResult = FindCommonAncestor(root.Left, p, q);
            var rightResult = FindCommonAncestor(root.Right, p, q);

            if (leftResult == null && rightResult == null)
            {
                return null;
            }
            else if(leftResult != null && rightResult == null)
            {
                return leftResult;
            }
            else if(leftResult == null && rightResult != null)
            {
                return rightResult;
            }
            else
            {
                return root;
            }
        }

        private static bool CheckNodeExist(BinaryTreeNode<T> root, BinaryTreeNode<T> node)
        {
            if(root is null || node is null) { return false; }
            if (root == node) { return true; }

            return CheckNodeExist(root.Left, node) || CheckNodeExist(root.Right, node);
        }

        #endregion

        #region Question 4.8
        /*
         * 두 개의 큰 이진 트리 T1, T2가 있다고 하자.
         * T1에는 수백만 개의 노드가 있고, T2에는 수백 개 정도의 노드가 있다.
         * T2가 T1의 하위 트리인지 판별하는 알고리즘을 만들라.
         * T1 안에 노드 n이 있어 그 노드의 하위 트리가 T2와 동일하면, T2는 T1의 하위 트리다.
         * 다시 말해, T1에서 n부터 시작하여 그 아래쪽을 끊어 내면, 그 결과가 T2와 동일해야 한다.
         */

        public static bool Q8_ContainsTree(BinaryTreeNode<IComparable> t1, BinaryTreeNode<IComparable> t2)
        {
            if(t2 == null)
            {
                // 빈 트리는 언제나 모든 트리의 하위 트리
                return true;
            }

            return SubTree(t1, t2);
        }

        /// <summary>
        /// <paramref name="r2"/>가 <paramref name="r1"/>의 하위 트리인지 판별한다.
        /// </summary>
        /// <param name="r1">전체 트리 노드</param>
        /// <param name="r2">하위 트리 노드</param>
        /// <returns></returns>
        private static bool SubTree(BinaryTreeNode<IComparable> r1, BinaryTreeNode<IComparable> r2)
        {
            if (r1 == null)
            {
                return false;
            }

            if(r1.Data.CompareTo(r2.Data) == 0)
            {
                if(MatchTree(r1, r2))
                {
                    return true;
                }
            }

            return (SubTree(r1.Left, r2) || SubTree(r1.Right, r2));
        }

        private static bool MatchTree(BinaryTreeNode<IComparable> r1, BinaryTreeNode<IComparable> r2)
        {
            if(r1 == null && r2 == null)
            {
                return true;
            }

            if(r1 == null || r2 == null)
            {
                return false;
            }

            if(r1.Data.CompareTo(r2.Data) != 0)
            {
                return false;
            }

            return (MatchTree(r1.Left, r2.Left) && MatchTree(r1.Right, r2.Right));
        }
        #endregion
    }
}
