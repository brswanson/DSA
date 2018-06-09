using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    // Get/Add/Delete Time: O(1). Constant time should be possible. Currently Gets are linear due to the effort of finding the appropriate ranking index and searching its collection.
    // Memory:  O(2n) -> O(n). Scales linearly  with incoming data.
    public class LfuCache<T>
    {
        private readonly HashSet<T> _cachedValuesSet;
        private readonly Dictionary<T, int> _valueFreqDict;
        // TODO: Could use an ArrayList type and keep track of the index of each element. Otherwise updating the rank is linear time instead of constant.
        private readonly LinkedList<FrequencyRanking<T>> _freqRanking;
        private readonly int _maxCacheCount;

        public LfuCache(int maxCacheCount = 1000)
        {
            _cachedValuesSet = new HashSet<T>();
            _valueFreqDict = new Dictionary<T, int>();
            _maxCacheCount = maxCacheCount;
        }

        public int Count => _cachedValuesSet.Count;

        public T Get(T value)
        {
            Add(value);

            return value;
        }

        private void Add(T value)
        {
            _cachedValuesSet.Add(value);

            RankFrequency(value);
        }

        private void RankFrequency(T value)
        {
            if (Count > _maxCacheCount) RemoveLeastFrequencyUsed();

            if (_valueFreqDict.ContainsKey(value)) UpdateExistingRank(value);
            else CreateNewRank(value);
        }

        private void RemoveLeastFrequencyUsed()
        {
            // TODO: Get the last item in the ranking order.
            // TODO: Remove the first item in its collection.
            // TODO: If the collection is empty, remove the ranking item.
        }

        private void UpdateExistingRank(T value)
        {
            _valueFreqDict[value]++;

            // TODO: Increment the value's ranking index by one
        }

        private void CreateNewRank(T value)
        {
            _valueFreqDict.Add(value, 1);

            // TODO: Add the passed in value to the current lowest ranking if == 1, otherwise create a new ranking for 1
        }

        private struct FrequencyRanking<T>
        {
            public int Frequency;
            private List<T> Items;

            public void Add(T item)
            {
                if (Items == null) Items = new List<T>();

                Items.Add(item);
            }

            public void Remove(T item)
            {
                if (Items == null || Items.Count <= 0) return;

                Items.Remove(item);
            }
        }
    }


    [TestClass]
    public class TestLfuCache
    {
        [TestMethod]
        public void LfuCache()
        {
            var lfuCache = new LfuCache<int>(5);

            // Intentionally failing test
            Assert.IsTrue(false);
        }
    }
}