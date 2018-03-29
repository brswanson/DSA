using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    [TestClass]
    public class MissingNumber
    {
        /// <summary>
        ///     Given a contiguous sequence of numbers in which each number repeats thrice, there is exactly one missing number.
        ///     Find the missing number.
        /// </summary>
        /// <input>
        ///     11122333
        /// </input>
        /// <output>
        ///     Missing number 2
        /// </output>
        public static char? BruteForce(int repetitionCount, string input)
        {
            // Time:    O(n). Must iterate over each character in the passed in string, worst case.
            // Memory:  O(1).

            // Checking edge cases
            if (string.IsNullOrEmpty(input)) return null;
            if (repetitionCount <= 0) return null;

            char? output = null;
            var currentCharCount = 0;
            var currentChar = input[0];

            foreach (var c in input)
            {
                // Count occurences of the incoming characters
                if (c == currentChar) currentCharCount++;

                // Check the current char's repetition before updating the current char in order to find a match
                if (currentCharCount < repetitionCount && c != currentChar)
                {
                    output = currentChar;
                    break;
                }

                // Move the current character forward and reset the count when needed
                if (currentChar != c)
                {
                    currentChar = c;
                    currentCharCount = 1;
                }
            }

            return output;
        }

        [TestMethod]
        public void TestMissingNumberBruteForce()
        {
            const int repetitionCount = 3;
            const string input = "11122333";
            const char expected = '2';

            var actual = BruteForce(repetitionCount, input);

            Assert.AreEqual(expected, actual);
        }
    }
}