using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DSA.Problems
{
    public class NthDigit
    {
        /// <summary>
        ///     Find the nth digit of the positive infinite integer sequence 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, ...
        /// </summary>
        /// <input>
        ///     11
        /// </input>
        /// <output>
        ///     0
        /// </output>
        /// <notes>
        ///     n is positive and will fit within the range of a 32-bit signed integer (n less than 231).
        /// </notes>
        public static int BruteForce(int input)
        {
            // Time:    O(n). Iterates over integers in the sequence until it reaches the desired length.
            // Memory:  O(1). Requires a constant amount of additonal memory to keep track of its place in the integer sequence.

            // Safe to return early here since integers must be positive
            if (input <= 9) return input;

            var digitIndex = 10;
            var currentNumber = 10;

            while (true)
            {
                var numberToString = currentNumber.ToString();
                foreach (var c in numberToString)
                {
                    digitIndex++;

                    if (digitIndex > input) return Convert.ToInt32(char.GetNumericValue(c));
                }

                currentNumber++;
            }
        }
    }

    [TestClass]
    public class TestNthDigit
    {
        [TestMethod]
        public void BruteForce()
        {
            const int input = 11;
            const int expected = 0;
            var actual = NthDigit.BruteForce(input);

            Assert.AreEqual(expected, actual);
        }
    }
}