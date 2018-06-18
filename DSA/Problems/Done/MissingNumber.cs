using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
{
    /// <summary>
    ///     Given a contiguous sequence of numbers in which each number repeats thrice, there is exactly one missing number.
    ///     Find the missing number.
    /// </summary>
    /// <input>
    ///     [1,1,1,2,2,3,3,3]
    /// </input>
    /// <output>
    ///     2
    /// </output>
    public class MissingNumber
    {
        public static int? CountFrequency(int repetitionCount, int[] input)
        {
            // Time:    O(n).   Linear, iterates over each number in the passed in array.
            // Memory:  O(1).   A constant amount of memory is used to keep track of the current character, its count, and to return the output number.

            // Checking edge cases
            if (input == null) return null;
            if (repetitionCount <= 0) return null;

            int? output = null;
            var currentNumCount = 0;
            var currentNum = input[0];

            foreach (var c in input)
            {
                // Count occurences of the incoming characters
                if (c == currentNum) currentNumCount++;

                // Check the current char's repetition before updating the current char in order to find a match
                if (currentNumCount < repetitionCount && c != currentNum)
                {
                    output = currentNum;
                    break;
                }

                // Move the current character forward and reset the count when needed
                if (currentNum != c)
                {
                    currentNum = c;
                    currentNumCount = 1;
                }
            }

            return output;
        }

        public static int? Bitwise(int repetitionCount, int[] input)
        {
            // Time:    O(n).   Linear, iterates over each number in the passed in array.
            // Memory:  O(1).   A constant amount of memory is used to keep track of the current XOR'd value.

            // Checking edge cases
            if (input == null) return null;
            if (repetitionCount <= 0) return null;

            int xorValue = 0;

            foreach (var num in input) xorValue = xorValue ^ num;

            return xorValue;
        }
    }

    [TestClass]
    public class TestMissingNumber
    {
        [TestMethod]
        public void BruteForce()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 1, 1, 2, 2, 3, 3, 3 };
            const int expected = 2;

            var actual = MissingNumber.CountFrequency(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Bitwise()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 1, 1, 2, 2, 3, 3, 3 };
            const int expected = 2;

            var actual = MissingNumber.Bitwise(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }
    }
}