using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    [TestClass]
    public class CourseSchedule
    {
        /// <summary>
        ///     There are a total of n courses you have to take, labeled from 0 to n - 1.
        ///     Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
        ///     Given the total number of courses and a list of prerequisite pairs, is it possible for you to finish all courses?
        /// </summary>
        /// <input>
        ///     2, [[1,0]]
        /// </input>
        /// <output>
        ///     true
        /// </output>
        /// <notes>
        ///     Example input:
        ///     2, [[1,0],[0,1]]
        ///     There are a total of 2 courses to take.
        ///     To take course 1 you should have finished course 0, and to take course 0 you should also have finished course 1.
        ///     So it is impossible.
        /// </notes>
        public bool AdjacencyMatrix(int numCourses, int[,] prerequisites)
        {
            // Time:    O(n).   One pass over each edge.
            // Memory:  O(n^2). One value for each edge combination. Edges are from 0 to n, so the matrix must be nxn in size.

            // Checking edge cases
            if (numCourses <= 0 || prerequisites.Length <= 0) return true;

            var adjMatrix = new bool[numCourses, numCourses];

            // Note: Incomplete. The adj matrix has been created, but the search still needs to exist.
            //       Also may want to keep track of incoming edges.
            for (var i = 0; i < prerequisites.GetLength(0) - 1; i++)
            {
                var edgeA = prerequisites[i, 0];
                var edgeB = prerequisites[i, 1];

                adjMatrix[edgeA, edgeB] = true;
            }

            return true;
        }

        [TestMethod]
        public void TestCourseSchedule()
        {
            var input = new[,] { { 1, 0 }, { 0, 1 } };
            const bool expected = false;

            var actual = AdjacencyMatrix(2, input);

            Assert.AreEqual(expected, actual);
        }
    }
}