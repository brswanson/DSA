using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Sorting
{
    public class QuickSort
    {
        public static int[] Recursive(int[] input)
        {
            // Time:    O(n^2)      Array is halved log(n) times. Each element in the array is swapped once for a total of n.    
            // Memory:  O(log(n)).  The same array is used on each recursive call, but because of the recursive implemenation there will be up to log(n) stack frames.

            // Edge cases
            if (IsInvalidInput(input)) return input;

            QuickSortRecursive(input, 0, input.Length);

            return input;
        }

        private static void QuickSortRecursive(int[] input, int start, int end)
        {
            if (end == start) return;

            int pivot = GetMedianOfThreeIndex(Tuple.Create(input.First(), 0), Tuple.Create(input[input.Length / 2], input.Length / 2), Tuple.Create(input.Last(), input.Length - 1));

            // Sort the array such that:
            //  a) All elements < the pivot appear to the left of the pivot
            //  b) All elements > the pivot appear to the right of the pivot

            // Reading from left to right
            for (int i = start; i < end; i++)
            {
                var currentNumber = input[i];

                // Anything less than the pivot value can stay in place
                if (currentNumber < input[pivot])
                {
                    if (i < pivot) continue;

                    Swap(input, i, pivot);
                }

                // Anything greater than the pivot value changes the desired pivot index
                if (currentNumber >= input[pivot])
                {
                    if (pivot < i) continue;

                    // Choose new pivot index
                    pivot = i;
                }
            }

            int leftStart = start;
            int leftEnd = Math.Max(pivot - 1, 0);
            int rightStart = Math.Min(pivot + 1, end);
            int rightEnd = end;

            // Quicksort both partitions
            QuickSortRecursive(input, leftStart, leftEnd);
            QuickSortRecursive(input, rightStart, rightEnd);
        }

        // Note: It would be faster to compare each value and return the median instead of creating a new array, sorting it, and returning the median. I really like LINQ though
        private static int GetMedianOfThreeIndex(Tuple<int, int> a, Tuple<int, int> b, Tuple<int, int> c) => new[] { a, b, c }.OrderBy(num => num).Skip(1).First().Item2;

        private static void Swap(int[] input, int a, int b)
        {
            int copy = input[a];
            input[a] = input[b];
            input[b] = copy;
        }

        public static int[] Iterative(int[] input)
        {
            // Time:    O(nlog(n))  Array is halved log(n) times. Each element in the array is swapped once for a total of n.    
            // Memory:  O(log(n)).  The same array is used during the loop, but an auxilary stack of log(n) entries is needed to keep track of start/end of each array partition.

            // Edge cases
            if (IsInvalidInput(input)) return input;

            // TODO: Implement this
            return null;
        }

        private static bool IsInvalidInput(int[] input) => input == null || input.Length <= 1;
    }

    [TestClass]
    public class TestQuickSort
    {
        [TestMethod]
        public void Recursive()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = QuickSort.Recursive(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RecursiveNullRef()
        {
            int[] input = null;
            int[] expected = null;

            var actual = QuickSort.Recursive(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Iterative()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = QuickSort.Iterative(input);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IterativeNullRef()
        {
            int[] input = null;
            int[] expected = null;

            var actual = QuickSort.Iterative(input);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
