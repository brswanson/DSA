using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    /// <summary>
    /// Given an array of integers 'a' and a number 'd', perform a rotation of value 'd' on the array.
    /// For example: d = -2 would cause two left rotations on the array. d = 2 would cause two right rotations of the array.
    /// Return the updated array.
    /// </summary>
    /// <input>
    /// new[] { 1, 2, 3, 4, 5 }, -2
    /// </input>
    /// <output>
    /// new[] { 3, 4, 5, 1, 2 }
    /// </output>
    public class ArrayRotation
    {
        public static int[] Rotate(int[] a, int direction)
        {
            return a;
        }
    }

    [TestClass]
    public class TestArrayRotation
    {
        [TestMethod]
        public void RotateRight()
        {
            var testArray = new[] { 1, 2, 3, 4, 5 };
            const int testDirection = 2;

            var expected = new[] { 4, 5, 1, 2, 3 };

            Assert.AreEqual(ArrayRotation.Rotate(testArray, testDirection), expected);
        }

        [TestMethod]
        public void RotateLeft()
        {
            var testArray = new[] { 1, 2, 3, 4, 5 };
            const int testDirection = -2;

            var expected = new[] { 3, 4, 5, 1, 2 };

            Assert.AreEqual(ArrayRotation.Rotate(testArray, testDirection), expected);
        }
    }
}
