using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            if (input == null) return null;

            return MergeSortRecursive(input);
        }

        private static int[] MergeSortRecursive(int[] input)
        {
            if (input.Length <= 1) return input;

            var mid = input.Length / 2;
            // Trade off here of using Skip/Take instead of Array.Copy()
            var arrayA = input.Take(mid).ToArray();
            var arrayB = input.Skip(mid).Take(input.Length - mid).ToArray();

            return Merge(MergeSortRecursive(arrayA), MergeSortRecursive(arrayB));
        }

        private static int[] Merge(int[] arrayA, int[] arrayB)
        {
            var mergedArray = new int[arrayA.Length + arrayB.Length];
            var mergedIndex = 0;
            var indexA = 0;
            var indexB = 0;

            // Merge A & B until one is finished
            while (indexA < arrayA.Length && indexB < arrayB.Length)
            {
                if (arrayA[indexA] > arrayB[indexB])
                {
                    mergedArray[mergedIndex] = arrayB[indexB];
                    indexB++;
                }
                else
                {
                    mergedArray[mergedIndex] = arrayA[indexA];
                    indexA++;
                }

                mergedIndex++;
            }

            // Merge the remainer of A, if any
            while (indexA < arrayA.Length)
            {
                mergedArray[mergedIndex] = arrayA[indexA];

                indexA++;
                mergedIndex++;
            }

            // Merge the remainer of B, if any
            while (indexB < arrayB.Length)
            {
                mergedArray[mergedIndex] = arrayB[indexB];

                indexB++;
                mergedIndex++;
            }

            return mergedArray;
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
