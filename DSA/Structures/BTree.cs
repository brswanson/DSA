using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Structures
{
    [TestClass]
    public class BTree
    {
        private BTreeNode _root;

        public int? Search(int? value, bool recursive = false)
        {
            return recursive
                ? SearchNodeRecursively(value, _root)
                : SearchNodeIterative(value, _root);
        }

        private static int? SearchNodeRecursively(int? value, BTreeNode node)
        {
            // Check the children of the node
            if (value < node.Min && node.Left != null) return SearchNodeRecursively(value, node.Left);
            if (value > node.Max && node.Right != null) return SearchNodeRecursively(value, node.Right);
            if (value > node.Min && value < node.Max && node.Middle != null) return SearchNodeRecursively(value, node.Middle);

            // Check if the current node contains the value
            for (var i = 0; i < node.Count; i++) if (node.Values[i] == value) return value;

            return SearchNodeValues(value, node);
        }

        private static int? SearchNodeIterative(int? value, BTreeNode node)
        {
            // Check the children of the node
            while (true)
            {
                if (value < node.Min && node.Left != null)
                {
                    node = node.Left;
                    continue;
                }

                if (value > node.Max && node.Right != null)
                {
                    node = node.Right;
                    continue;
                }

                if (value > node.Min && value < node.Max && node.Middle != null)
                {
                    node = node.Middle;
                    continue;
                }

                break;
            }

            return SearchNodeValues(value, node);
        }

        private static int? SearchNodeValues(int? value, BTreeNode node)
        {
            for (var i = 0; i < node.Count; i++) if (node.Values[i] == value) return value;

            return null;
        }

        public void AddValue(int value)
        {
            if (_root == null)
            {
                _root = new BTreeNode(value);
                return;
            }

            AddValue(value, _root);
        }

        public void AddValue(int value, BTreeNode node)
        {
            if (node.IsFull)
            {
                Split(value, node);
                return;
            }

            node.AddValue(value);
        }

        private void Split(int value, BTreeNode node)
        {
            // TODO: Implement splitting the node
            return;
        }

        [TestMethod]
        public void TestBTree()
        {
            var actual = new BTree();
            actual.AddValue(1);
            actual.AddValue(2);
            actual.AddValue(3);
            actual.AddValue(4);
            actual.AddValue(5);

            Assert.AreEqual(1, actual.Search(1));
            Assert.AreEqual(2, actual.Search(2));
            Assert.AreEqual(3, actual.Search(3));
            Assert.AreEqual(4, actual.Search(4));
            Assert.AreEqual(5, actual.Search(5));
        }
    }

    public class BTreeNode
    {
        public BTreeNode Left;
        public BTreeNode Middle;
        public BTreeNode Right;
        public int?[] Values;

        public BTreeNode(int value)
        {
            AddValue(value);
        }

        public bool HasChild => Left != null || Middle != null || Right != null;
        public bool IsFull => Count >= Values.Length;

        // Note: Could set min, max, and count whenever the Values array changes to improve performance
        public int Count => Values.Count(v => v.HasValue);
        public int? Min => Values.Min();
        public int? Max => Values.Max();

        public int? Median(int value)
        {
            var medianArraySize = Values.Length + 1;
            var medianArray = new int?[medianArraySize];
            Values.CopyTo(medianArray, 0);

            medianArray[Values.Length + 1] = value;

            Array.Sort(medianArray);

            return medianArray[medianArraySize / 2];
        }

        public void AddValue(int value, int maxValues = 2)
        {
            if (Values == null) Values = new int?[maxValues];
            if (IsFull) return;

            Values[Count] = value;
        }
    }
}