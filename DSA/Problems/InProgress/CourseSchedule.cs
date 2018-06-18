using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DSA.Problems.InProgress
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
        public static bool DFS(int numCourses, List<int[]> prerequisites)
        {
            // Time:    O(v + n).   One pass over each vertex and node.
            // Memory:  O(v + n).   Extra memory for each vertex and node.

            // Checking edge cases
            if (numCourses <= 0 || prerequisites.Count <= 0) return true;

            //TODO:
            // Build a structure for the graph so it can be searched
            // Search each node DFS. If false, return false.

            return true;
        }

        // TODO:
        /*
            private bool DFS(...)
            Return true if perm[i] is true;
            Return false if temp[i] for node is true;

            Set temp[i] = true;
            Iterate over all children for the node; return false if false.
            
            perm[i] = true;
            temp[i] = false;
         */
    }

    [TestClass]
    public class TestCourseSchedule
    {
        [TestMethod]
        public void DirectedGraph()
        {
            var input = new List<int[]> { new[] { 1, 0 }, new[] { 0, 1 } };
            const bool expected = false;

            var actual = CourseSchedule.DFS(input.Count, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UndirectedGraph()
        {
            var input = new List<int[]> { new[] { 0, 1 }, new[] { 1, 2 } };
            const bool expected = true;

            var actual = CourseSchedule.DFS(input.Count, input);

            Assert.AreEqual(expected, actual);
        }
    }
}