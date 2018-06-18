using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Structures
{
    public class BTree
    {
        private BTreeNode _root;

        public int? Search(int? value, bool recursive = false)
        {
            if (_root == null) return null;

            var node = recursive ? SearchNodesRecursively(value, _root) : SearchNodesIteratively(value, _root);

            return SearchNodeValues(value, node);
        }

        private static BTreeNode SearchNodesRecursively(int? value, BTreeNode node)
        {
            // Check the children of the node
            // TODO: Refactor to use key indexes instead of hard coding based on three nodes
            if (value < node.Min && node.Children[0] != null) return SearchNodesRecursively(value, node.Children[0]);
            if (value >= node.Min && value <= node.Max && node.Children[1] != null) return SearchNodesRecursively(value, node.Children[1]);
            if (value > node.Max && node.Children[2] != null) return SearchNodesRecursively(value, node.Children[2]);

            return node;
        }

        private static BTreeNode SearchNodesIteratively(int? value, BTreeNode node)
        {
            // Check the children of the node
            // TODO: Refactor to use key indexes instead of hard coding based on three nodes
            while (true)
            {
                if (value < node.Min && node.Children[0] != null)
                {
                    node = node.Children[0];
                    continue;
                }

                if (value >= node.Min && value <= node.Max && node.Children[1] != null)
                {
                    node = node.Children[1];
                    continue;
                }

                if (value > node.Max && node.Children[2] != null)
                {
                    node = node.Children[2];
                    continue;
                }

                break;
            }

            return node;
        }

        private static int? SearchNodeValues(int? value, BTreeNode node)
        {
            if (node == null) return null;

            for (var i = 0; i < node.Count; i++) if (node.Values[i] == value) return value;

            return null;
        }

        public void AddValue(int? value, bool recursive = false)
        {
            if (_root == null)
            {
                _root = new BTreeNode(value);
                return;
            }

            var node = recursive ? SearchNodesRecursively(value, _root) : SearchNodesIteratively(value, _root);

            AddValue(value, node);
        }

        public void AddValue(int? value, BTreeNode node)
        {
            if (node.IsFull)
            {
                Split(value, node);
                return;
            }

            node.AddValue(value);
        }

        private void Split(int? value, BTreeNode node)
        {
            /* TODO: Handle case where node is root/has no parent

                TODO: Distribute child nodes
                Refactor such that there is no concept of Left, Middle, Right; just array size + 1 children.
                Then distribute keys/children such that they balance out
             */

            return;
        }
    }

    public class BTreeNode
    {
        public BTreeNode Parent;
        public BTreeNode[] Children;
        public int?[] Values;

        public BTreeNode(int? value, int nodes = 3)
        {
            if (nodes < 3) nodes = 3;

            Children = new BTreeNode[nodes];
            Values = new int?[nodes - 1];

            AddValue(value);
        }

        public bool IsFull => Values?[Values.Length - 1] != null;

        // Note: Could set min, max, and count whenever the Values array changes to improve performance
        public int Count => Values?.Count(v => v.HasValue) ?? 0;
        public int? Min => Values?.Min();
        public int? Max => Values?.Max();

        public void AddValue(int? value)
        {
            if (IsFull) return;

            Values[Count] = value;
        }
    }

    [TestClass]
    public class TestBTree
    {
        [TestMethod]
        public void BTreeSearch()
        {
            var actual = new BTree();

            // Search before adding elements
            Assert.AreEqual(null, actual.Search(1));

            actual.AddValue(1);
            actual.AddValue(2);
            actual.AddValue(3);
            actual.AddValue(4);
            actual.AddValue(5);

            // Search after adding elements
            Assert.AreEqual(1, actual.Search(1));
            Assert.AreEqual(2, actual.Search(2));
            Assert.AreEqual(3, actual.Search(3));
            Assert.AreEqual(4, actual.Search(4));
            Assert.AreEqual(5, actual.Search(5));
        }
    }
}