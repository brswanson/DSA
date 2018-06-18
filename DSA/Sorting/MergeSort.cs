using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DSA.Sorting
{
    public class MergeSort
    {
        public static int[] Recursive(int[] input)
        {
            // Time:    O(nlogn).   Arrays are halved on each recursive call, leaving logn total merges. Each merge takes n time to sort the data.
            // Memory:  O(n).       New arrays are created and their memory is allocated at each recursive step.
            //                      This additional memory reduces down to linear space in big O notation though.

            // Checking edge cases
            if (input == null) return null;

            if (input.Length <= 1) return input;

            var mid = input.Length / 2;
            // Trade off here of using Skip/Take instead of Array.Copy()
            var arrayA = input.Take(mid).ToArray();
            var arrayB = input.Skip(mid).Take(input.Length - mid).ToArray();

            return Merge(Recursive(arrayA), Recursive(arrayB));
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
    }

    [TestClass]
    public class TestMergeSort
    {
        [TestMethod]
        public void MergeSortRecursive()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = MergeSort.Recursive(input);

            Assert.AreEqual(expected.Length, input.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
