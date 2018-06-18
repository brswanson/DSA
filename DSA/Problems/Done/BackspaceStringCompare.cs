using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
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
        private const char BackspaceCharacter = '#';

        public static bool TwoPointer(string a, string b)
        {
            // Time: O(n + m)   Linear, where n and m are the lengths of the passed in string arrays.
            //                  Worst case, it will iterate over all characters in both strings before making a determination.
            //                  It may find a difference in the arrays more quickly but this can't be relied on.
            // Memory: O(1).    Constant. Additional memory is only used to keep track of string pointers.

            if (IsEqualShortcut(a, b)) return true;
            if (IsInvalidParams(a, b)) return false;

            var aIndex = a.Length - 1;
            var bIndex = b.Length - 1;

            // Exhaust matching characters
            while (aIndex >= 0 && bIndex >= 0)
            {
                var currentCharA = GetNextCharacter(a, ref aIndex);
                var currentCharB = GetNextCharacter(b, ref bIndex);

                if (currentCharA != currentCharB) return false;
            }

            // Compare trailing A, if it exists
            if (aIndex >= 0) { if (GetNextCharacter(a, ref aIndex) != null) return false; }

            // Compare trailing B, if it exists
            if (bIndex >= 0) { if (GetNextCharacter(b, ref bIndex) != null) return false; }

            return true;
        }

        private static char? GetNextCharacter(string input, ref int index)
        {
            var backspaceCount = 0;

            while (index >= 0)
            {
                var currentCharacter = input[index];
                index--;

                if (currentCharacter == BackspaceCharacter)
                {
                    backspaceCount++;
                    continue;
                }

                if (backspaceCount > 0)
                {
                    backspaceCount--;
                    continue;
                }

                return currentCharacter;
            }

            return null;
        }

        private static bool IsEqualShortcut(string a, string b)
        {
            if (a == null && b == null) return true;
            if (a == string.Empty && b == string.Empty) return true;

            return false;
        }

        private static bool IsInvalidParams(string a, string b)
        {
            if (a == null && b != null) return true;
            if (b == null && a != null) return true;

            return false;
        }
    }

    [TestClass]
    public class TestBackspaceStringCompare
    {
        [TestMethod]
        public void BruteForceEmptyString()
        {
            Assert.IsTrue(BackspaceStringCompare.TwoPointer(string.Empty, string.Empty));
        }

        [TestMethod]
        public void BruteForceNulls()
        {
            Assert.IsTrue(BackspaceStringCompare.TwoPointer(null, null));

            Assert.IsFalse(BackspaceStringCompare.TwoPointer("a", null));
            Assert.IsFalse(BackspaceStringCompare.TwoPointer(null, "b"));
        }

        [TestMethod]
        public void BruteForcePositive()
        {
            var testA = "abc#d";
            var testB = "abd";

            Assert.IsTrue(BackspaceStringCompare.TwoPointer(testA, testB));

            var testAA = "a########bc###";
            var testBB = "abc###";

            Assert.IsTrue(BackspaceStringCompare.TwoPointer(testAA, testBB));
        }

        [TestMethod]
        public void BruteForcePositiveBackspaceString()
        {
            var testA = "a########bc###";
            var testB = "a#b#c#";

            Assert.IsTrue(BackspaceStringCompare.TwoPointer(testA, testB));
        }

        [TestMethod]
        public void BruteForcePositiveDifferentLength()
        {
            var testA = "nzp#o#g";      //"nzg"
            var testB = "b#nzp#o#g";    //"nzg

            Assert.IsTrue(BackspaceStringCompare.TwoPointer(testA, testB));
        }

        [TestMethod]
        public void BruteForceNegativeLongerB()
        {
            var testA = "abcd";
            var testB = "abc";

            Assert.IsFalse(BackspaceStringCompare.TwoPointer(testA, testB));
        }

        [TestMethod]
        public void BruteForceNegativeLongerA()
        {
            var testA = "bbbextm";  //bbbextm
            var testB = "bbb#extm"; //bbextm

            Assert.IsFalse(BackspaceStringCompare.TwoPointer(testA, testB));
        }

        [TestMethod]
        public void BruteForceNegativeSameLength()
        {
            var testA = "a#c";  //c
            var testB = "b";    //b

            Assert.IsFalse(BackspaceStringCompare.TwoPointer(testA, testB));
        }
    }
}
