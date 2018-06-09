using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    /// <summary>
    ///     Given two strings A and B, return if they are equal. Strings may contain a backspace character represented by '#'.
    ///     Backspace characters cause the first previous occurence of a non-backspace character to be ignored.
    /// </summary>
    /// <input>
    ///     ['a','b','c','#','c,']
    ///     ['a','b','c,']
    /// </input>
    /// <output>
    ///     true
    /// </output>
    public class BackspaceStringCompare
    {
        // Time:    O(n). Linear. Worst case, it will iterate over all characters in both arrays before making a determination.
        //                It may find a difference in the arrays more quickly than n, but this can't be relied on.
        // Memory:  O(1). Constant. Additional memory is only used to keep track of string iterators and backspace occurences.
        public static bool BruteForce(string a, string b)
        {
            if (IsInvalidParams(a, b)) return false;

            return true;
        }

        private static bool IsInvalidParams(string a, string b)
        {

            return false;
        }
    }

    [TestClass]
    public class TestBackspaceStringCompare
    {
        [TestMethod]
        public void BruteForce()
        {
            var testA = "abc#d";
            var testB = "abd";

            Assert.IsTrue(BackspaceStringCompare.BruteForce(testA, testB));
        }
    }
}
