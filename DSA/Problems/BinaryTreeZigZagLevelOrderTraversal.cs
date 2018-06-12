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
        ///          /  \
        ///         15   7
        /// </input>
        /// <output>
        ///     [[3], [20,9], [15,7]]
        /// </output>
        public static List<List<int>> DepthFirstSearch(BinaryTreeNode<int> rootNode)
        {
            // TODO: Add time and space complexity

            var traversal = new List<List<int>>();

            if (InputIsInvalid(rootNode)) return traversal;

            // TODO: Implement alg

            return traversal;
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
        public void BinaryTreeZigZagLevelOrderTraversalPositive()
        {
            var testNode = new BinaryTreeNode<int>(3)
            {
                Left = new BinaryTreeNode<int>(9),
                Right = new BinaryTreeNode<int>(20)
            };
            testNode.Right.Left = new BinaryTreeNode<int>(15);
            testNode.Right.Right = new BinaryTreeNode<int>(7);

            var expected = new List<IList<int>>
            {
                new List<int> {3},
                new List<int> {20, 9},
                new List<int> {15, 7}
            };

            var actual = BinaryTreeZigZagLevelOrderTraversal.DepthFirstSearch(testNode);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
