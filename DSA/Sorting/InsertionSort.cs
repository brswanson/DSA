using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Sorting
{
    [TestClass]
    public class InsertionSort
    {
        public static int[] SortIntegerArrayAscending(int[] input)
        {
            // TODO: Actually implement this function
            return input;
        }

        [TestMethod]
        public void TestInsertionSort()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = SortIntegerArrayAscending(input);

            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
