using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    /// <summary>
    ///     Given an array of integers, find the majority element.
    ///     The majority element is the number which occurs at least n/2 times in the array.
    ///     Return "NO Majority Element" if no solution exists. Otherwise, return the number.
    /// </summary>
    /// <input>
    ///     [1,1,1,2,3]
    /// </input>
    /// <output>
    ///     "1"
    /// </output>
    public class MajorityElement
    {
        private const string NoSolutionMessage = "NO Majority Element";
        // Time:    O(n). Linear. Worst case, it will iterate over all numbers in the array to find the majority value.
        //                If the numbers are ordered favorably, it may finish faster, but this can't be relied on.
        // Memory:  O(n). Linear. Contains at most one entry in the dictionary for each value in the incoming array.
        public string DictionaryCount(int[] numbers, int divisor = 2)
        {
            if (IsInvalidParams(numbers, divisor)) return NoSolutionMessage;

            var goalFrequency = numbers.Length / divisor;
            var valueCountDict = new Dictionary<int, int>();

            // Keep a total of each number's occurences. Return the first value found with the desired frequency
            foreach (var number in numbers)
            {
                if (valueCountDict.ContainsKey(number))
                {
                    valueCountDict[number]++;
                    if (valueCountDict[number] > goalFrequency) return number.ToString();
                }
                else valueCountDict.Add(number, 1);
            }

            return NoSolutionMessage;
        }

        // Where n is the number of elements in the input array 'numbers'.
        // Time:    O(n). Linear. Worst case, it will iterate over all numbers in the array to find the majority value.
        //                If the numbers are ordered favorably, it may finish faster, but this can't be relied on.
        // Memory:  O(1). Constant. Only two variables are needed regardless of input: the current most common element, and a count.
        public string MajorityVote(int[] numbers, int divisor = 2)
        {
            if (IsInvalidParams(numbers, divisor)) return NoSolutionMessage;

            var mostCommonNumberFreq = 1;
            var mostCommonNumber = numbers[0];

            for (var i = 1; i < numbers.Length; i++)
            {
                var currentNumber = numbers[i];

                // Increment whenever the same number is seen, decrement whenever a different number is seen
                if (currentNumber == mostCommonNumber) mostCommonNumberFreq++;
                else mostCommonNumberFreq--;

                // Inevitably the majority element's count will equal the majority freq if it exists
                if (mostCommonNumberFreq >= numbers.Length / divisor) return mostCommonNumber.ToString();

                // If instances of a number reach 0, use the newly found number as the new most common number
                if (mostCommonNumberFreq <= 0) mostCommonNumber = currentNumber;
            }

            return NoSolutionMessage;
        }

        private static bool IsInvalidParams(int[] numbers, int divisor)
        {
            if (divisor == 0) return true;
            if (numbers == null || numbers.Length == 0) return true;

            return false;
        }
    }

    [TestClass]
    public class TestMajorityElement
    {
        [TestMethod]
        public void DictionaryCount()
        {
            var majorityElement = new MajorityElement();

            object result = majorityElement.DictionaryCount(new[] { 3, 1, 3, 3, 2 }, 0);
            Console.WriteLine(result); // Should print "3"
            result = majorityElement.DictionaryCount(new[] { 1, 2, 3 });
            Console.WriteLine(result); // Should print "NO Majority Element"
        }

        [TestMethod]
        public void MajorityVote()
        {
            var majorityElement = new MajorityElement();

            object result = majorityElement.MajorityVote(new[] { 3, 1, 3, 3, 2 }, 0);
            Console.WriteLine(result); // Should print "3"
            result = majorityElement.MajorityVote(new[] { 1, 2, 3 });
            Console.WriteLine(result); // Should print "NO Majority Element"
        }
    }
}
