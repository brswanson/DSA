using System.Collections.Generic;
using System.Linq;
using DSA.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
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
            // Time: O(n).      Linear, where n is the number of input nodes. Each node is read once to find all solutions, then the shortest one is picked.
            //                  Could shortcut some routes when a solution has been found, but this won't affect big o time.
            //                  Some additional time is taken to sort the solutions by count, but count is a property of List so it should be logn worst case.
            // Memory: O(n).    Linear, where n is the number of input nodes. Since the route is needed, copies of the nodes are made.
            //                  However, the algorithm takes the first (and thus shortest) path it can find from the Left and Right children of root.
            //                  Because of this, worst case scenario it will copy all nodes once.

            // Store valid solutions by their route length. Overwriting existing values is fine, since their length is the only value we care about.
            // Use the shortest route length value to get the route itself from the dict.
            if (node == null) return null;

            var solutionList = new List<List<int>>();

            DepthFirstSearch(node, sum, new List<int>(), solutionList);

            // Return the shortest list, if it exists
            return solutionList.OrderBy(c => c.Count).FirstOrDefault();
        }

        private static void DepthFirstSearch(BinaryTreeNode<int> root, int sum, List<int> route, ICollection<List<int>> solutionList)
        {
            if (root == null) return;

            route.Add(root.Value);

            if (sum == root.Value)
            {
                // Add the solution and break recursion. Length of the route can only become longer
                solutionList.Add(new List<int>(route));
                // Return to the root node
                route.RemoveRange(1, route.Count - 1);
                return;
            }

            DepthFirstSearch(root.Left, sum - root.Value, route, solutionList);
            DepthFirstSearch(root.Right, sum - root.Value, route, solutionList);

            // Return to the root node
            route.RemoveRange(1, route.Count - 1);
        }
    }

    [TestClass]
    public class TestBinaryTreePathSum
    {
        [TestMethod]
        public void NullRef()
        {
            // [Test 'null' for ref types]
            Assert.IsNull(BinaryTreePathSum.Algorithm(null, 1));
        }

        // TODO: Currently fails this test. Should protect the alg from int overflows
        [TestMethod]
        public void IntBoundaries()
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
        public void WithoutSolution()
        {
            // [Test a set which contains no solution]
            /*  Goal: 2. No route.
             *      1
             */
            var nodeWithoutSolution = new BinaryTreeNode<int>(1);
            Assert.IsNull(BinaryTreePathSum.Algorithm(nodeWithoutSolution, 2));
        }

        [TestMethod]
        public void WithSolution()
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
        public void SubOptimalSolution()
        {
            // [Test a set which contains an optimal and sub-optimal solution]
            /*  Goal: 10. Root, left, left, for a total of 3 steps. Root, right, for a total of 2 steps.
             *      1
             *    1   10
             *  8  1
             */
            var nodeWithSubOptimalSolution = new BinaryTreeNode<int>(1) { Left = new BinaryTreeNode<int>(1), Right = new BinaryTreeNode<int>(10) };
            nodeWithSubOptimalSolution.Left.Left = new BinaryTreeNode<int>(8);
            nodeWithSubOptimalSolution.Left.Right = new BinaryTreeNode<int>(1);

            var subOptimalSolutionResult = BinaryTreePathSum.Algorithm(nodeWithSubOptimalSolution, 11);
            Assert.IsNotNull(subOptimalSolutionResult);
            Assert.AreEqual(2, subOptimalSolutionResult.Count);
        }
    }
}
