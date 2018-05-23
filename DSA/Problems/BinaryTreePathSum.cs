using System;
using System.Collections.Generic;
using DSA.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    public class BinaryTreePathSum
    {
        /// <summary>
        ///     Given a binary tree node and an integer, find the shortest path to reach the sum within the tree, if it exists.
        ///     Mutation: As above, but all node values are positive integers.
        /// </summary>
        /// <input>
        ///     TreeNode node, int sum
        /// </input>
        /// <output>
        ///     Collection of node values which form the path to the sum or null if none exists
        /// </output>
        public static List<int> Algorithm(BinaryTreeNode<int> node, int sum)
        {
            // Time: O(n).      Linear, where n is the number of input nodes. Each node is read once to find the optimal solution.
            //                  Could shortcut some routes when a solution has been found, but this won't affect big o time.
            // Memory: O(n).    Linear, where n is the number of input nodes. The number of items in the optimal path may equal the number of incoming nodes.

            // Store valid solutions by their route length. Overwriting existing values is fine, since their length is the only value we care about.
            // Use the shortest route length value to get the route itself from the dict.
            Dictionary<int, List<int>> routeLengthDict = new Dictionary<int, List<int>>();
            int shortestRouteLength = int.MinValue;

            // TODO: Exhaustively search the tree. Record all valid solutions found.

            return routeLengthDict.ContainsKey(shortestRouteLength) ? routeLengthDict[shortestRouteLength] : null;
        }
    }

    [TestClass]
    public class TestBinaryTreePathSum
    {
        [TestMethod]
        public void BinaryTreePathSumNullRef()
        {
            // [Test 'null' for ref types]
            Assert.IsNull(BinaryTreePathSum.Algorithm(null, 1));
        }

        [TestMethod]
        public void BinaryTreePathSumIntBoundaries()
        {
            // [Testing int boundaries]
            // Create nodes that will cause wrapping since C# handles overflow by default this way
            // 2147483647 += 1 == -2147483648
            // 2147483648 -= 1 ==  2147483647

            var overflowNodeNegative = new BinaryTreeNode<int>(int.MaxValue) { Left = new BinaryTreeNode<int>(1) };
            var overflowNodePositive = new BinaryTreeNode<int>(int.MinValue) { Left = new BinaryTreeNode<int>(-1) };

            Assert.IsNull(BinaryTreePathSum.Algorithm(overflowNodeNegative, int.MinValue));
            Assert.IsNull(BinaryTreePathSum.Algorithm(overflowNodePositive, int.MaxValue));
        }

        [TestMethod]
        public void BinaryTreePathSumWithoutSolution()
        {
            // [Test a set which contains no solution]
            /*  Goal: 2. No route.
             *      1
             */
            var nodeWithoutSolution = new BinaryTreeNode<int>(1);
            Assert.IsNull(BinaryTreePathSum.Algorithm(nodeWithoutSolution, 2));
        }

        [TestMethod]
        public void BinaryTreePathSumWithSolution()
        {
            // [Test a set which contains a solution]
            /*  Goal: 2. Root, left.
             *      1
             *    1   0
             *          0
             */
            var nodeWithSolution = new BinaryTreeNode<int>(1) { Left = new BinaryTreeNode<int>(1) };
            var solutionResult = BinaryTreePathSum.Algorithm(nodeWithSolution, 2);

            Assert.IsNotNull(solutionResult);
            Assert.AreEqual(2, solutionResult.Count);
        }

        [TestMethod]
        public void BinaryTreePathSumSubOptimalSolution()
        {
            // [Test a set which contains an optimal and sub-optimal solution]
            /*  Goal: 10. Root, left, left, for a total of 3 steps. Root, right, for a total of 2 steps.
             *      1
             *    1   9
             *  8
             */
            var nodeWithSubOptimalSolution = new BinaryTreeNode<int>(1) { Left = new BinaryTreeNode<int>(1), Right = new BinaryTreeNode<int>(9) };
            nodeWithSubOptimalSolution.Left.Left = new BinaryTreeNode<int>(1) { Left = new BinaryTreeNode<int>(8) };

            var subOptimalSolutionResult = BinaryTreePathSum.Algorithm(nodeWithSubOptimalSolution, 2);
            Assert.IsNotNull(subOptimalSolutionResult);
            Assert.AreEqual(2, subOptimalSolutionResult.Count);
        }
    }
}
