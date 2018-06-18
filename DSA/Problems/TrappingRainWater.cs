using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    public class TrappingRainWater
    {
        /// <summary>
        ///     Given n non-negative integers representing an elevation map where the width of each bar is 1, compute how much
        ///     water it is able to trap after raining.
        /// </summary>
        /// <input>
        ///     arr[] = [2, 0, 2]
        /// </input>
        /// <output>
        ///     2
        /// </output>
        /// <notes>
        ///     Given input:    [4, 0, 2, 0, 3, 0, 2, 0, 1]
        ///     Water trapped:  [0, 3, 1, 3, 0, 2, 0, 1, 0]
        ///     Example diagram:
        ///     |             |___     
        ///     |   |      ~  |   |_   
        ///     | | | |    ~  | | | |_ 
        ///     |_|_|_|_|     |_|_|_|_|
        /// </notes>
        public static int OnePass(int[] input)
        {
            // Time:    O(n).   One pass over the entire array. Reads from left and right while keeping track of the max left and right values to calculate water depth.
            // Memory:  O(1).   A constant amount of memory is used to keep track of max left, max right, and the output.

            // Checking edge cases
            if (input == null || input.Length <= 0) return 0;

            // Set left and right iterators to the bounds of the array
            var leftIterator = 0;
            var rightIterator = input.Length - 1;

            // Init maxes to left and right bounds
            var leftMax = input[leftIterator];
            var rightMax = input[rightIterator];

            var output = 0;

            // Iterate until left and right are equal. This should occur when the entire array has been read
            while (leftIterator != rightIterator)
            {
                // Compare left and right
                if (leftMax >= rightMax)
                {
                    // Add rightmost rainwater
                    output += CalculateRainwater(leftMax, rightMax, input[rightIterator]);

                    // Pull toward the left
                    rightIterator--;

                    // Update right max value if needed
                    UpdateMax(ref rightMax, input[rightIterator]);
                }
                else
                {
                    // Add leftmost rainwater
                    output += CalculateRainwater(leftMax, rightMax, input[leftIterator]);

                    // Pull toward the right
                    leftIterator++;

                    // Update left max value if needed
                    UpdateMax(ref leftMax, input[leftIterator]);
                }
            }

            return output;
        }

        private static int CalculateRainwater(int leftMax, int rightMax, int elevantion)
        {
            return Math.Max(Math.Min(leftMax, rightMax) - elevantion, 0);
        }

        private static void UpdateMax(ref int maxValue, int newValue)
        {
            maxValue = Math.Max(maxValue, newValue);
        }
    }

    [TestClass]
    public class TestTrappingRainWater
    {
        [TestMethod]
        public void OnePass()
        {
            int[] input = { 9, 1, 2, 5, 5, 0, 2, 0, 9 };
            const int expected = 48;

            var actual = TrappingRainWater.OnePass(input);

            Assert.AreEqual(expected, actual);
        }
    }
}