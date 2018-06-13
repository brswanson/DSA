using System;
using System.Collections.Generic;
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

        public static int IterativeBreadthFirstSearch(BinaryTreeNode<int> root)
        {
            // Time:    O(n).   Linear, where n is the number of input nodes. Each node is visited once to find the maximum depth.
            // Memory:  O(d*k). Linear, where d is the maximum depth of the tree and k is the number of leaves on each node (d*2).
            //                  Worst case, the widest level of the tree will exist in the heap using the queue's memory space.

            if (IsInvalid(root)) return -1;

            var maxDepth = 0;
            var queue = new Queue<BinaryTreeNode<int>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                // Mark the number of items currently in the queue for this level
                var currentLevelNodeCount = queue.Count;
                maxDepth++;

                // Dequeue all items for the current level and enqueue their children
                for (var i = 0; i < currentLevelNodeCount; i++)
                {
                    var currentNode = queue.Dequeue();
                    if (currentNode == null) { continue; }

                    if (currentNode.Left != null) queue.Enqueue(currentNode.Left);
                    if (currentNode.Right != null) queue.Enqueue(currentNode.Right);
                }
            }

            return maxDepth;
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

        [TestMethod]
        public void BinaryTreeDepthIterativeNullRef()
        {
            Assert.AreEqual(-1, BinaryTreeDepth.IterativeBreadthFirstSearch(null));
        }

        [TestMethod]
        public void BinaryTreeDepthIterativePartialLeaves()
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
            var actual = BinaryTreeDepth.IterativeBreadthFirstSearch(testNode);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BinaryTreeDepthIterativeFullLeaves()
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
            var actual = BinaryTreeDepth.IterativeBreadthFirstSearch(testNode);

            Assert.AreEqual(expected, actual);
        }
    }
}