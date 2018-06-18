using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Sorting
{
    public class QuickSort
    {
        public static int[] Recursive(int[] input)
        {
            // Time:    
            // Memory:  

            // TODO: Implement this
            return null;
        }

        public static int[] Iterative(int[] input)
        {
            // Time:    
            // Memory:  

            // TODO: Implement this
            return null;
        }
    }

    [TestClass]
    public class TestQuickSort
    {
        [TestMethod]
        public void Recursive()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = QuickSort.Recursive(input);

            Assert.AreEqual(expected.Length, input.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void Iterative()
        {
            var input = new[] { 7, 3, 1, 9, 8, 2, 4, 6, 5, 0 };
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var actual = QuickSort.Iterative(input);

            Assert.AreEqual(expected.Length, input.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
