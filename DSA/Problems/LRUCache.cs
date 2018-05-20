using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    public class LruCache<T>
    {
        private readonly HashSet<T> _cachedValuesSet;
        private readonly LinkedList<T> _recentNodesList;
        private readonly int _maxCacheCount;

        public LruCache(int maxCacheCount = 1000)
        {
            _recentNodesList = new LinkedList<T>();
            _cachedValuesSet = new HashSet<T>();
            _maxCacheCount = maxCacheCount;
        }

        public int Count => _recentNodesList.Count;
        private bool IsEmpty => _recentNodesList.Count <= 0;
        public bool IsCached(T value) { return _cachedValuesSet.Contains(value); }

        private void Add(T value)
        {
            // Performance is the same for .Contains() and .Add()
            ReOrderNodes(_cachedValuesSet.Add(value), value);
        }

        public T Get(T value)
        {
            Add(value);

            return value;
        }

        private void ReOrderNodes(bool isNewItem, T value)
        {
            if (Count > _maxCacheCount) RemoveLeastRecentlyUsedItem();

            if (!isNewItem) _recentNodesList.Remove(value);
            _recentNodesList.AddFirst(value);
        }

        private void RemoveLeastRecentlyUsedItem()
        {
            if (IsEmpty) return;

            _cachedValuesSet.Remove(_recentNodesList.Last.Value);
            _recentNodesList.RemoveLast();
        }
    }

    [TestClass]
    public class TestLruCache
    {
        [TestMethod]
        public void Test()
        {
            var lruCache = new LruCache<int>(5);

            lruCache.Get(1);
            lruCache.Get(2);
            lruCache.Get(3);
            lruCache.Get(4);
            // Should be 4 3 2 1
            lruCache.Get(5);
            // Should be 5 4 3 2 1
            lruCache.Get(6);
            lruCache.Get(7);
            // Order should be 7 6 5 4 3 (1 and 2 removed)

            Assert.AreEqual(lruCache.Count, 5);

            Assert.IsTrue(lruCache.IsCached(7));
            Assert.IsTrue(lruCache.IsCached(6));
            Assert.IsTrue(lruCache.IsCached(5));
            Assert.IsTrue(lruCache.IsCached(4));
            Assert.IsTrue(lruCache.IsCached(3));

            Assert.IsFalse(lruCache.IsCached(2));
            Assert.IsFalse(lruCache.IsCached(1));
        }
    }
}