using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions.Library;
using System.Collections.Generic;
using Solutions;
using System.Collections.ObjectModel;
using System;

namespace Tests
{
    [TestClass]
    public class Test_TreeGraph
    {
        [TestMethod]
        public void Q4_1()
        {
            BinaryTree<int> tree;

            BinaryTreeNode<int> balanced = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(3)
                    {
                        Left = new BinaryTreeNode<int>(7),
                        Right = new BinaryTreeNode<int>(8)
                    },
                    Right = new BinaryTreeNode<int>(4)
                    {
                        Left = new BinaryTreeNode<int>(9),
                        Right = new BinaryTreeNode<int>(10)
                    },
                },
                Right = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(5),
                    Right = new BinaryTreeNode<int>(6)
                }
            };

            tree = new BinaryTree<int>(Comparer<int>.Default, balanced);
            Assert.IsTrue(TreeGraph<int>.Q1_IsBalanced(tree));

            BinaryTreeNode<int> notBalanced = new BinaryTreeNode<int>(0)
            {
                Right = new BinaryTreeNode<int>(1)
                {
                    Right = new BinaryTreeNode<int>(2)
                    {
                        Right = new BinaryTreeNode<int>(3)
                        {
                            Right = new BinaryTreeNode<int>(4),
                        }
                    }
                }
            };

            tree = new BinaryTree<int>(Comparer<int>.Default, notBalanced);
            Assert.IsFalse(TreeGraph<int>.Q1_IsBalanced(tree));
        }

        [TestMethod]
        public void Q4_2()
        {
            var nodes = new Collection<GraphNode<string>>()
            {
                new GraphNode<string>("a"),
                new GraphNode<string>("b"),
                new GraphNode<string>("c"),
                new GraphNode<string>("d"),
                new GraphNode<string>("e"),
                new GraphNode<string>("f"),
            };

            nodes[0].Adjacent.Add(nodes[1]);
            nodes[0].Adjacent.Add(nodes[2]);
            nodes[3].Adjacent.Add(nodes[4]);
            nodes[4].Adjacent.Add(nodes[5]);

            var graph = new Graph<string>(nodes);

            Assert.IsTrue(TreeGraph<string>.Q2_Search(graph, nodes[3], nodes[5]));
            Assert.IsFalse(TreeGraph<string>.Q2_Search(graph, nodes[0], nodes[5]));
        }

        [TestMethod]
        public void Q4_3()
        {
            var array = new List<int>() { 1, 2, 3, 4, 5 };
            var treeNode = TreeGraph<int>.Q3_CreateMinimalBST(array);

            Assert.AreEqual(3, treeNode.Data);
            Assert.AreEqual(2, treeNode.Left.Data);
            Assert.AreEqual(1, treeNode.Left.Left.Data);
            Assert.AreEqual(5, treeNode.Right.Data);
            Assert.AreEqual(4, treeNode.Right.Left.Data);

            array = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            treeNode = TreeGraph<int>.Q3_CreateMinimalBST(array);

            Assert.AreEqual(6, treeNode.Data);
            Assert.AreEqual(3, treeNode.Left.Data);
            Assert.AreEqual(2, treeNode.Left.Left.Data);
            Assert.AreEqual(1, treeNode.Left.Left.Left.Data);
            Assert.AreEqual(5, treeNode.Left.Right.Data);
            Assert.AreEqual(4, treeNode.Left.Right.Left.Data);
            Assert.AreEqual(9, treeNode.Right.Data);
            Assert.AreEqual(8, treeNode.Right.Left.Data);
            Assert.AreEqual(10, treeNode.Right.Right.Data);
        }

        [TestMethod]
        public void Q4_4()
        {
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(3)
                    {
                        Left = new BinaryTreeNode<int>(7),
                        Right = new BinaryTreeNode<int>(8)
                    },
                    Right = new BinaryTreeNode<int>(4)
                    {
                        Left = new BinaryTreeNode<int>(9),
                        Right = new BinaryTreeNode<int>(10)
                    },
                },
                Right = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(5),
                    Right = new BinaryTreeNode<int>(6)
                }
            };

            var levels = TreeGraph<int>.Q4_CreateLevelLinkedList(root);

            Assert.IsTrue(levels[0].Contains(root));

            Assert.IsTrue(levels[1].Contains(root.Left));
            Assert.IsTrue(levels[1].Contains(root.Right));

            Assert.IsTrue(levels[2].Contains(root.Left.Left));
            Assert.IsTrue(levels[2].Contains(root.Left.Right));
            Assert.IsTrue(levels[2].Contains(root.Right.Left));
            Assert.IsTrue(levels[2].Contains(root.Right.Right));

            Assert.IsTrue(levels[3].Contains(root.Left.Left.Left));
            Assert.IsTrue(levels[3].Contains(root.Left.Left.Right));
            Assert.IsTrue(levels[3].Contains(root.Left.Right.Left));
            Assert.IsTrue(levels[3].Contains(root.Left.Right.Right));
        }

        [TestMethod]
        public void Q4_5()
        {
            BinaryTreeNode<IComparable> root = new BinaryTreeNode<IComparable>(0)
            {
                Left = new BinaryTreeNode<IComparable>(1)
                {
                    Left = new BinaryTreeNode<IComparable>(3),
                    Right = new BinaryTreeNode<IComparable>(4),
                },
                Right = new BinaryTreeNode<IComparable>(2)
            };

            Assert.IsFalse(TreeGraph<int>.Q5_CheckBST(root));

            root = new BinaryTreeNode<IComparable>(4)
            {
                Left = new BinaryTreeNode<IComparable>(2)
                {
                    Left = new BinaryTreeNode<IComparable>(1),
                    Right = new BinaryTreeNode<IComparable>(3)
                },
                Right = new BinaryTreeNode<IComparable>(6)
                {
                    Left = new BinaryTreeNode<IComparable>(5),
                    Right = new BinaryTreeNode<IComparable>(7)
                }
            };

            Assert.IsTrue(TreeGraph<int>.Q5_CheckBST(root));
        }

        [TestMethod]
        public void Q4_6()
        {
            BinaryTreeNode<IComparable> root = new BinaryTreeNode<IComparable>(4)
            {
                Left = new BinaryTreeNode<IComparable>(2)
                {
                    Left = new BinaryTreeNode<IComparable>(1),
                    Right = new BinaryTreeNode<IComparable>(3)
                },
                Right = new BinaryTreeNode<IComparable>(6)
                {
                    Left = new BinaryTreeNode<IComparable>(5),
                    Right = new BinaryTreeNode<IComparable>(7)
                }
            };

            Assert.AreEqual(5, TreeGraph<IComparable>.Q6_InorderFindSuccessor(root).Data);
            Assert.AreEqual(7, TreeGraph<IComparable>.Q6_InorderFindSuccessor(root.Right).Data);
            Assert.AreEqual(2, TreeGraph<IComparable>.Q6_InorderFindSuccessor(root.Left.Left).Data);

            Assert.AreEqual(null, TreeGraph<IComparable>.Q6_InorderFindSuccessor(root.Right.Right));
        }

        [TestMethod]
        public void Q4_7()
        {
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(3)
                    {
                        Left = new BinaryTreeNode<int>(7),
                        Right = new BinaryTreeNode<int>(8)
                    },
                    Right = new BinaryTreeNode<int>(4)
                    {
                        Left = new BinaryTreeNode<int>(9),
                        Right = new BinaryTreeNode<int>(10)
                    },
                },
                Right = new BinaryTreeNode<int>(2)
                {
                    Left = new BinaryTreeNode<int>(5),
                    Right = new BinaryTreeNode<int>(6)
                }
            };

            BinaryTreeNode<int> notin = new BinaryTreeNode<int>(11);

            Assert.AreEqual(null, TreeGraph<int>.Q7_GetCommonAncestor(root, root, root));
            Assert.AreEqual(root.Left, TreeGraph<int>.Q7_GetCommonAncestor(root, root.Left.Left, root.Left.Right));
            Assert.AreEqual(root, TreeGraph<int>.Q7_GetCommonAncestor(root, root.Right.Left, root.Left.Right));
            Assert.AreEqual(root.Left, TreeGraph<int>.Q7_GetCommonAncestor(root, root.Left.Left, root.Left.Left));
            Assert.AreEqual(null, TreeGraph<int>.Q7_GetCommonAncestor(root, null, root.Left.Left));
            Assert.AreEqual(null, TreeGraph<int>.Q7_GetCommonAncestor(root, notin, root.Left.Left));
        }

        [TestMethod]
        public void Q4_8()
        {
            BinaryTreeNode<IComparable> t1 = new BinaryTreeNode<IComparable>(0)
            {
                Left = new BinaryTreeNode<IComparable>(1)
                {
                    Left = new BinaryTreeNode<IComparable>(3)
                    {
                        Left = new BinaryTreeNode<IComparable>(7),
                        Right = new BinaryTreeNode<IComparable>(8)
                    },
                    Right = new BinaryTreeNode<IComparable>(4)
                    {
                        Left = new BinaryTreeNode<IComparable>(9),
                        Right = new BinaryTreeNode<IComparable>(10)
                    },
                },
                Right = new BinaryTreeNode<IComparable>(2)
                {
                    Left = new BinaryTreeNode<IComparable>(5),
                    Right = new BinaryTreeNode<IComparable>(6)
                }
            };

            BinaryTreeNode<IComparable> t2 = new BinaryTreeNode<IComparable>(3)
            {
                Left = new BinaryTreeNode<IComparable>(7),
                Right = new BinaryTreeNode<IComparable>(8)
            };

            Assert.IsTrue(TreeGraph<IComparable>.Q8_ContainsTree(t1, t2));

            t2.Left = new BinaryTreeNode<IComparable>(100);
            Assert.IsFalse(TreeGraph<IComparable>.Q8_ContainsTree(t1, t2));

            Assert.IsTrue(TreeGraph<IComparable>.Q8_ContainsTree(t1, null));
            Assert.IsFalse(TreeGraph<IComparable>.Q8_ContainsTree(null, t2));
        }
    }
}
