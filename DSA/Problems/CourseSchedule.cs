using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public bool AdjacencyMatrix(int numCourses, List<int[]> prerequisites)
        {
            // Time:    O(n).   One pass over each edge.
            // Memory:  O(n^2). One value for each edge combination. Edges are from 0 to n, so the matrix must be nxn in size.

            // Checking edge cases
            if (numCourses <= 0 || prerequisites.Count <= 0) return true;

            const int courseIndex = 0;
            const int prereqIndex = 1;
            var adjMatrix = new bool[numCourses, numCourses];

            for (var i = 0; i < prerequisites.Count; i++)
            {
                var edgeA = prerequisites[i][courseIndex];
                var edgeB = prerequisites[i][prereqIndex];

                adjMatrix[edgeA, edgeB] = true;

                // If the opposite incoming/outgoing edge also exists, we know we have a prereq loop
                if (adjMatrix[edgeB, edgeA]) return false;
            }

            return true;
        }

        [TestMethod]
        public void TestCourseSchedule()
        {
            var input = new List<int[]>();
            input.Add(new[] { 1, 0 });
            input.Add(new[] { 0, 1 });

            const bool expected = false;

            var actual = AdjacencyMatrix(input.Count, input);

            Assert.AreEqual(expected, actual);
        }
    }
}