using System;
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

            QuickSortRecursive(input, 0, input.Length - 1);

            return input;
        }

        private static void QuickSortRecursive(int[] input, int start, int end)
        {
            if (end == start) return;

            int pivot = PartitionGetPivot(input, start, end);

            // Quicksort both partitions
            QuickSortRecursive(input, start, Math.Max(pivot - 1, 0));
            QuickSortRecursive(input, Math.Min(pivot + 1, end), end);
        }

        private static int PartitionGetPivot(int[] input, int low, int high)
        {
            // Always taking the right-most value as the pivot
            int pivot = input[high];
            // Init the index of the lowest value
            int lowIndex = (low - 1);

            // Sort the array such that:
            //  a) All elements < the pivot appear to the left of the pivot
            //  b) All elements > the pivot appear to the right of the pivot

            // Read left to right
            for (int i = low; i < high; i++)
            {
                if (input[i] <= pivot)
                {
                    // Increment the lowest index; this divides the array between low and high
                    lowIndex++;

                    // Swap the lowest value with the current value since it's also lower than the pivot
                    Swap(input, lowIndex, i);
                }
            }

            // Swap the pivot with the first value greater than the pivot
            Swap(input, lowIndex + 1, high);

            return lowIndex + 1;
        }

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
