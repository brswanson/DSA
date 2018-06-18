using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Structures
{
    public class StringHashtable
    {
        private LinkedList<string>[] _valueSpace;

        public StringHashtable()
        {
            _valueSpace = new LinkedList<string>[0];
        }

        public StringHashtable(string[] input)
        {
            _valueSpace = new LinkedList<string>[input.Length];

            foreach (var value in input)
            {
                Add(value);
            }
        }

        public void Add(string value)
        {
            var hashedKey = HashFunction(value);

            // If the key lies outside the space of the table, we increase it
            if (hashedKey >= _valueSpace.Length) IncreaseSpace(hashedKey);

            if (_valueSpace[hashedKey] == null)
            {
                _valueSpace[HashFunction(value)] = new LinkedList<string>(new[] { value });
            }
            else
            {
                _valueSpace[HashFunction(value)].AddLast(value);
            }
        }

        // This method is prone to memory exceptions
        private void IncreaseSpace(int spaceRequired)
        {
            var doubledMemory = new LinkedList<string>[spaceRequired * 2];
            _valueSpace.CopyTo(doubledMemory, 0);

            _valueSpace = doubledMemory;
        }

        private string GetValue(string value)
        {
            var key = HashFunction(value);

            // If only one value exists for this key, return the value in constant time
            if (_valueSpace[key].Count == 1) return _valueSpace[key].First.Value;

            // Otherwise search the list for the value
            return _valueSpace[key].Find(value)?.Value;
        }

        // A very poor hashing function. Uses the length of the passed in string.
        private static int HashFunction(string value)
        {
            return value.Length;
        }

        public string this[string key] => GetValue(key);
    }

    [TestClass]
    public class TestStringHashtable
    {
        [TestMethod]
        public void Hashtable()
        {
            // Check default constructor
            var actual = new StringHashtable();
            actual.Add("1");
            Assert.AreEqual(actual["1"], "1");

            // Re-init using var constructor
            var input = new[] { "", "1", "22", "333", "4444", "55555" };
            actual = new StringHashtable(input);

            // Check contents
            Assert.AreEqual(actual[""], "");
            Assert.AreEqual(actual["1"], "1");
            Assert.AreEqual(actual["22"], "22");
            Assert.AreEqual(actual["333"], "333");
            Assert.AreEqual(actual["4444"], "4444");
            Assert.AreEqual(actual["55555"], "55555");

            // Force size increase
            actual.Add("666666");
            Assert.AreEqual(actual["666666"], "666666");

            // Force chaining
            actual.Add("1");
            Assert.AreEqual(actual["1"], "1");
        }
    }
}
