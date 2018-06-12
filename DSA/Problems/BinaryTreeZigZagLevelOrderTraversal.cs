using System.Collections.Generic;
using DSA.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    public class BinaryTreeZigZagLevelOrderTraversal
    {
        /// <summary>
        ///     Given a binary tree, return the zigzag level order traversal of its nodes' values.
        ///     For example: from left to right, then right to left for the next level and alternate between).
        /// </summary>
        /// <input>
        ///     [3,9,20,null,null,15,7]
        ///     Tree representation:
        ///       3
        ///     /   \
        ///    9     20
        ///         /  \
        ///        15   7
        /// </input>
        /// <output>
        ///     [[3], [20,9], [15,7]]
        /// </output>
        public static List<List<int>> DepthFirstSearch(BinaryTreeNode<int> rootNode)
        {
            // Time: O(n).      Linear, where n is the number of input nodes.
            //                  Every node's value has to be read and returned so this is likely optimal.
            // Memory: O(n).    Linear, where n is the number of input nodes.
            //                  Every node's value has to be returned in a different structure and order so this is likely optimal (node to list of node values)

            var traversalList = new List<List<int>>();

            if (InputIsInvalid(rootNode)) return traversalList;

            var leftStack = new Stack<BinaryTreeNode<int>>();
            var rightStack = new Stack<BinaryTreeNode<int>>();

            leftStack.Push(rootNode);

            while (leftStack.Count > 0 || rightStack.Count > 0)
            {
                EmptyStack(leftStack, rightStack, traversalList, false);
                EmptyStack(rightStack, leftStack, traversalList, true);
            }

            return traversalList;
        }

        private static void EmptyStack(Stack<BinaryTreeNode<int>> leftStack, Stack<BinaryTreeNode<int>> rightStack, ICollection<List<int>> traversalList, bool leftToRight)
        {
            var newTraversal = new List<int>();

            while (rightStack.Count > 0)
            {
                var node = rightStack.Pop();
                if (node == null) continue;

                newTraversal.Add(node.Value);

                if (leftToRight)
                {
                    if (node.Left != null) leftStack.Push(node.Left);
                    if (node.Right != null) leftStack.Push(node.Right);
                }
                else
                {
                    if (node.Right != null) leftStack.Push(node.Right);
                    if (node.Left != null) leftStack.Push(node.Left);
                }
            }

            if (newTraversal.Count > 0) traversalList.Add(newTraversal);
        }

        public static bool InputIsInvalid<T>(BinaryTreeNode<T> rootNode)
        {
            return rootNode == null;
        }
    }

    [TestClass]
    public class TestBinaryTreeZigZagLevelOrderTraversal
    {
        [TestMethod]
        public void BinaryTreeZigZagLevelOrderTraversalNullRef()
        {
            var expected = new List<List<int>>();
            var actual = BinaryTreeZigZagLevelOrderTraversal.DepthFirstSearch(null);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BinaryTreeZigZagLevelOrderTraversalPositivePartialLevels()
        {
            //       3
            //     /   \
            //    9     20
            //         /  \
            //        15   7
            var testNode = new BinaryTreeNode<int>(3)
            {
                Left = new BinaryTreeNode<int>(9),
                Right = new BinaryTreeNode<int>(20)
            };
            testNode.Right.Left = new BinaryTreeNode<int>(15);
            testNode.Right.Right = new BinaryTreeNode<int>(7);

            var expected = new List<List<int>>
            {
                new List<int> {3},
                new List<int> {20, 9},
                new List<int> {15, 7}
            };

            var actual = BinaryTreeZigZagLevelOrderTraversal.DepthFirstSearch(testNode);

            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            CollectionAssert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        public void BinaryTreeZigZagLevelOrderTraversalPositiveFullLevels()
        {
            //        3
            //      /   \
            //     9     20
            //    / \   /  \
            //   8   6 15   7
            var testNode = new BinaryTreeNode<int>(3)
            {
                Left = new BinaryTreeNode<int>(9),
                Right = new BinaryTreeNode<int>(20)
            };
            testNode.Right.Left = new BinaryTreeNode<int>(15);
            testNode.Right.Right = new BinaryTreeNode<int>(7);
            testNode.Left.Left = new BinaryTreeNode<int>(8);
            testNode.Left.Right = new BinaryTreeNode<int>(6);

            var expected = new List<List<int>>
            {
                new List<int> {3},
                new List<int> {20, 9},
                new List<int> {8, 6, 15, 7}
            };

            var actual = BinaryTreeZigZagLevelOrderTraversal.DepthFirstSearch(testNode);

            CollectionAssert.AreEqual(expected[0], actual[0]);
            CollectionAssert.AreEqual(expected[1], actual[1]);
            CollectionAssert.AreEqual(expected[2], actual[2]);
        }
    }
}
