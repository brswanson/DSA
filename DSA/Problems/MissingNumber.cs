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
        [TestMethod]
        public void BruteForce()
        {
            // Time:  O(n) Must iterate over each character in the passed in string, worst case.
            // Memory: o(n) Additional memory: counter int, answer char. Constant regardless of input.

            #region Test I/O

            const int repetitionCount = 3;
            var input = "11122333";
            var answer = '2';
            char? output = null;

            #endregion

            var currentCharCount = 0;
            var currentChar = input.First();

            // 1 1 1
            // 
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

            Assert.IsNotNull(output);
            Assert.AreEqual(answer, output.Value);
        }
    }
}