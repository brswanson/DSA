using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
{
    /// <summary>
    ///     Given an array of integers 'a' and a number 'd', perform a rotation of value 'd' on the array.
    ///     For example: d = -2 would cause two left rotations on the array. d = 2 would cause two right rotations of the
    ///     array.
    ///     Return the updated array.
    /// </summary>
    /// <input>
    ///     new[] { 1, 2, 3, 4, 5 }, -2
    /// </input>
    /// <output>
    ///     new[] { 3, 4, 5, 1, 2 }
    /// </output>
    public class ArrayRotation
    {
        public static int[] Rotate(int[] a, int direction)
        {
            // Time: O(n)       Linear, where n is the length of the passed in array of integers.
            // Memory: O(n).    Linear, where n is the length of the passed in array of integers.
            //                  Since we're creating a new array instead of updating the passed in array the additional memory is needed.

            // Handle edge cases
            if (a == null || a.Length == 0) return a;
            if (direction == 0) return a;

            // Declare a new memory space for the rotated array.
            var rotatedArray = new int[a.Length];

            for (var i = 0; i < a.Length; i++)
            {
                var rotatedLocation = GetRotatedLocation(a.Length, i, direction);

                rotatedArray[rotatedLocation] = a[i];
            }

            return rotatedArray;
        }

        private static int GetRotatedLocation(int arrayLength, int index, int direction)
        {
            // Rotate the index, left or right, depending on the direction
            // Mod ensures the change in index stays within the boundaries of the array
            var newIndex = (index + direction) % arrayLength;

            // If the change is negative (a Left rotation), add the length of the array to the negative value to get the correct index
            if (newIndex < 0) newIndex += arrayLength;

            return newIndex;
        }
    }

    [TestClass]
    public class TestArrayRotation
    {
        [TestMethod]
        public void RotateRight()
        {
            var testArray = new[] {1, 2, 3, 4, 5};
            const int testDirection = 2;

            var expected = new[] {4, 5, 1, 2, 3};
            var actual = ArrayRotation.Rotate(testArray, testDirection);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateLeft()
        {
            var testArray = new[] {1, 2, 3, 4, 5};
            const int testDirection = -2;

            var expected = new[] {3, 4, 5, 1, 2};
            var actual = ArrayRotation.Rotate(testArray, testDirection);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateLeftMultipleIterations()
        {
            var testArray = new[] {1, 2, 3, 4, 5};
            const int testDirection = -30;

            var expected = new[] {1, 2, 3, 4, 5};
            var actual = ArrayRotation.Rotate(testArray, testDirection);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}