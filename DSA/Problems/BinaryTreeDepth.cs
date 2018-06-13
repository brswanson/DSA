using System;
using DSA.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    /// <summary>
    ///     Given a binary tree, find its maximum depth.
    ///     The maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.
    /// </summary>
    /// <input>
    ///           3
    ///         /   \
    ///        9     20
    ///       /       \
    ///      15        7
    ///                 \
    ///                  12 
    /// </input>
    /// <output>
    ///     4
    /// </output>
    public class BinaryTreeDepth
    {
        public static int RecursiveDepthFirstSearch(BinaryTreeNode<int> root)
        {
            // Time: O(n).      Linear, where n is the number of input nodes. Each node is visited once to find the maximum depth.
            // Memory: O(n).    Linear, where n is the number of input nodes. Since we're searching recursively,
            //                  up to the number of nodes in the tree could be on the stack at once.

            if (IsInvalid(root)) return -1;

            return GetMaximumNodeDepth(root);
        }

        private static int GetMaximumNodeDepth(BinaryTreeNode<int> root, int currentDepth = 0)
        {
            if (root == null) return currentDepth;

            currentDepth++;

            return Math.Max(GetMaximumNodeDepth(root.Left, currentDepth), GetMaximumNodeDepth(root.Right, currentDepth));
        }

        private static bool IsInvalid(BinaryTreeNode<int> root) => root == null;
    }

    [TestClass]
    public class TestBinaryTreeDepth
    {
        [TestMethod]
        public void BinaryTreeDepthRecursiveNullRef()
        {
            Assert.AreEqual(-1, BinaryTreeDepth.RecursiveDepthFirstSearch(null));
        }

        [TestMethod]
        public void BinaryTreeDepthRecursivePartialLeaves()
        {
            //           3
            //         /   \
            //        9     20
            //       /       \
            //      15        7
            //                 \
            //                  12 

            var testNode = new BinaryTreeNode<int>(3)
            {
                Left = new BinaryTreeNode<int>(9),
                Right = new BinaryTreeNode<int>(20)
            };
            testNode.Left.Left = new BinaryTreeNode<int>(15);

            testNode.Right.Right = new BinaryTreeNode<int>(7)
            {
                Right = new BinaryTreeNode<int>(12)
            };

            var expected = 4;
            var actual = BinaryTreeDepth.RecursiveDepthFirstSearch(testNode);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BinaryTreeDepthRecursiveFullLeaves()
        {
            //           3
            //         /   \
            //        9     20

            var testNode = new BinaryTreeNode<int>(3)
            {
                Left = new BinaryTreeNode<int>(9),
                Right = new BinaryTreeNode<int>(20)
            };

            var expected = 2;
            var actual = BinaryTreeDepth.RecursiveDepthFirstSearch(testNode);

            Assert.AreEqual(expected, actual);
        }
    }
}