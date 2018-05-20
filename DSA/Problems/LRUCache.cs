using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems
{
    public class LruCache<T>
    {
        private readonly LinkedList<T> _recentNodes;
        private readonly HashSet<T> _cachedValues;
        private readonly int _maxCacheCount;

        // Nodes and Values should always be in sync, but using Max may prevent an overflow if they ever are
        public int Count => Math.Max(_recentNodes.Count, _cachedValues.Count);
        public bool IsCached(T value) { return _cachedValues.Contains(value); }

        public LruCache(int maxCacheCount = 1000)
        {
            _recentNodes = new LinkedList<T>();
            _cachedValues = new HashSet<T>();
            _maxCacheCount = maxCacheCount;
        }

        private void Add(T value)
        {
            // Performance is the same for .Contains() and .Add()
            ReOrderNodes(_cachedValues.Add(value), value);
        }

        public T Get(T value)
        {
            Add(value);

            return value;
        }

        private void ReOrderNodes(bool isNewItem, T value)
        {
            if (_cachedValues.Count > _maxCacheCount) RemoveLeastRecentlyUsedItem();

            if (!isNewItem) _recentNodes.Remove(value);
            _recentNodes.AddFirst(value);
        }

        private void RemoveLeastRecentlyUsedItem()
        {
            if (_recentNodes.Count <= 0) return;

            _cachedValues.Remove(_recentNodes.Last.Value);
            _recentNodes.RemoveLast();
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
