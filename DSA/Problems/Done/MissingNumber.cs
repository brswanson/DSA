using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
{
    /// <summary>
    ///     Given a contiguous sequence of sorted numbers in which each number repeats thrice, there is exactly one missing number.
    ///     Find the missing number.
    /// </summary>
    /// <input>
    ///     [1,1,1,2,2,3,3,3]
    /// </input>
    /// <mutation>
    ///     Numbers are not guaranteed to be sorted.
    /// </mutation>
    /// <output>
    ///     2
    /// </output>
    public class MissingNumber
    {
        public static int? Bitwise(int repetitionCount, int[] input)
        {
            // Time:    O(n).   Linear, iterates over each number in the passed in array.
            // Memory:  O(1).   A constant amount of memory is used to keep track of the current XOR'd value.

            // Checking edge cases
            if (IsInvalid(repetitionCount, input)) return null;

            int output = 0;

            foreach (var num in input) output = output ^ num;

            return output;
        }

        public static int? CountFrequency(int repetitionCount, int[] input)
        {
            // Time:    O(n).   Linear, iterates over each number in the passed in array. Numbers must be ordered.
            // Memory:  O(1).   A constant amount of memory is used to keep track of the current character, its count, and to return the output number.

            // Checking edge cases
            if (IsInvalid(repetitionCount, input)) return null;

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

        private static bool IsInvalid(int repetitionCount, int[] input)
        {
            if (input == null) return true;
            if (repetitionCount <= 0) return true;

            return false;
        }
    }

    [TestClass]
    public class TestMissingNumber
    {
        [TestMethod]
        public void BitwiseSorted()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 1, 1, 2, 2, 3, 3, 3 };
            const int expected = 2;

            var actual = MissingNumber.Bitwise(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BitwiseUnsorted()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 2, 3, 1, 2, 3, 1, 3 };
            const int expected = 2;

            var actual = MissingNumber.Bitwise(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountFrequencySorted()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 1, 1, 2, 2, 3, 3, 3 };
            const int expected = 2;

            var actual = MissingNumber.CountFrequency(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountFrequencyUnsorted()
        {
            const int repetitionCount = 3;
            var input = new[] { 1, 2, 3, 1, 2, 3, 1, 3 };
            const int expected = 2;

            var actual = MissingNumber.CountFrequency(repetitionCount, input);

            // Testing 'are not equal' because this algorithm only works when the numbers are sorted by value
            // It should fail on an unsorted array of numbers
            Assert.AreNotEqual(expected, actual);
        }
    }
}