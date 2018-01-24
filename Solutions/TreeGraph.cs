using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.Library;

namespace Solutions
{
    public class TreeGraph<T>
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

        #region Quetion 4.2
        // 주어진 유향 그래프(directed graph)에서 특정한 두 노드 간에 경로(route)가 존재하는지를 판별하는 알고리즘을 구현하라.
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
    }
}
