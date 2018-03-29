using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Sorting
{
    [TestClass]
    public class MergeSort
    {
        public static int[] SortIntegerArrayAscending(int[] input)
        {
            // Time: O(nlogn).  Iterates over each element in the array (n). Sorts the array elements usings swaps (logn)
            // Memory: 2n => n. Passes n, then n/2, n/4, etc ... In each subsequent call. This totals to at most 2n which reduces down to n.

            // Checking edge cases
            if (input == null || input.Length <= 1) return input;

            // TODO: Implement the sort
            return input;
        }

        [TestMethod]
        public void TestMergeSort()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = SortIntegerArrayAscending(input);

            Assert.AreEqual(expected.Length, input.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
