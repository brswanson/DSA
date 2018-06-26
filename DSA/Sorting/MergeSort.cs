﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DSA.Sorting
{
    public class MergeSort
    {
        public static int[] Recursive(int[] input)
        {
            // Time:    O(nlog(n)).   Arrays are halved on each recursive call, leaving logn total merges. Sorting all the halved arrays takes n time.
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

        public static int[] Iterative(int[] input)
        {
            // Time:    
            // Memory:  

            // TODO: Implement this
            return null;
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
        public void Recursive()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = MergeSort.Recursive(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Iterative()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = MergeSort.Iterative(input);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
