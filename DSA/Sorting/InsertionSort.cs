using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Sorting
{
    [TestClass]
    public class InsertionSort
    {
        public static void SortIntegerArrayAscending(int[] input)
        {
            // Time:    O(n2).  Iterates over each integer in the array at least once (n). May also iterate over each integer in the array once for each integer in the array (n).
            // Memory:  O(1).   Sorts in place.

            // Checking edge cases
            if (input == null || input.Length <= 1) return;

            var index = 1;

            while (index < input.Length)
            {
                var innerIndex = index;

                while (innerIndex > 0 && input[innerIndex - 1] > input[innerIndex])
                {
                    var placeHolder = input[innerIndex];
                    input[innerIndex] = input[innerIndex - 1];
                    input[innerIndex - 1] = placeHolder;

                    innerIndex--;
                }

                index++;
            }
        }

        [TestMethod]
        public void TestInsertionSort()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            SortIntegerArrayAscending(input);

            Assert.AreEqual(expected.Length, input.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], input[i]);
            }
        }
    }
}
