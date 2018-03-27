using System.Linq;
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
        private const int RepetitionCount = 3;
        private const string  Input = "11122333";
        private const char Answer = '2';

        [TestMethod]
        public void BruteForce()
        {
            // Time:  O(n). Must iterate over each character in the passed in string, worst case.
            // Memory: n. Additional memory: counter int, answer char.

            // Checking edge cases
            if (Input == null || Input.Length <= 0) return;

            char? output = null;
            var currentCharCount = 0;
            var currentChar = Input[0];

            foreach (var c in Input)
            {
                // Count occurences of the incoming characters
                if (c == currentChar) currentCharCount++;

                // Check the current char's repetition before updating the current char in order to find a match
                if (currentCharCount < RepetitionCount && c != currentChar)
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

            Assert.IsNotNull(output);
            Assert.AreEqual(Answer, output.Value);
        }
    }
}