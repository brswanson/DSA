using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DSA.Structures
{
    public class BinarySearchTree
    {
        private BinaryTreeNode<int> _root;
        private bool TreeIsEmpty => _root == null;

        public void Add(int value)
        {
            Add(new[] { value });
        }

        public void Add(IEnumerable<int> values, bool recursive = false)
        {
            foreach (var value in values)
            {
                if (recursive) AddRecursively(value);
                else AddIteratively(value);
            }
        }

        private void AddRecursively(int value)
        {
            // Start the tree if it doesn't exist
            if (TreeIsEmpty)
            {
                _root = new BinaryTreeNode<int>(value);
                return;
            }

            // Else, insert the node appropriately
            RecursiveInsertNode(_root, value);
        }

        private void AddIteratively(int value)
        {
            // Start the tree if it doesn't exist
            if (TreeIsEmpty)
            {
                _root = new BinaryTreeNode<int>(value);
                return;
            }

            // Else, insert the node appropriately
            IterativeInsertNode(_root, value);
        }

        private static BinaryTreeNode<int> RecursiveInsertNode(BinaryTreeNode<int> root, int value)
        {
            // Return a new node if the child doesn't exist
            if (root == null) return new BinaryTreeNode<int>(value);

            // Otherwise, compare the current node and traverse
            if (value >= root.Value)
            {
                root.Right = RecursiveInsertNode(root.Right, value);
            }
            else
            {
                root.Left = RecursiveInsertNode(root.Left, value);
            }

            return root;
        }

        private static void IterativeInsertNode(BinaryTreeNode<int> root, int value)
        {
            while (true)
            {
                if (value >= root.Value)
                {
                    if (root.Right == null)
                    {
                        root.Right = new BinaryTreeNode<int>(value);
                        return;
                    }

                    root = root.Right;
                }
                else
                {
                    if (root.Left == null)
                    {
                        root.Left = new BinaryTreeNode<int>(value);
                        return;
                    }

                    root = root.Left;
                }
            }
        }

        public BinaryTreeNode<int> Search(int value, bool recursive = false)
        {
            if (TreeIsEmpty) return null;

            return recursive ? RecursiveSearch(_root, value) : IterativeSearch(_root, value);
        }

        private static BinaryTreeNode<int> IterativeSearch(BinaryTreeNode<int> head, int value)
        {
            while (true)
            {
                // Return match
                if (head.Value == value) return head;

                // Search Left or Right, depending on value
                if (value >= head.Value && head.Right != null)
                {
                    head = head.Right;
                    continue;
                }
                else if (head.Left != null)
                {
                    head = head.Left;
                    continue;
                }

                // Item does not exist
                return null;
            }
        }

        private static BinaryTreeNode<int> RecursiveSearch(BinaryTreeNode<int> head, int value)
        {
            // Return match
            if (head.Value == value) return head;

            // Search Left or Right, depending on value
            if (value >= head.Value && head.Right != null)
            {
                return RecursiveSearch(head.Right, value);
            }
            else if (head.Left != null)
            {
                return RecursiveSearch(head.Left, value);
            }

            // Item does not exist
            return null;
        }

    }

    public class BinaryTreeNode<T>
    {
        public T Value;
        public BinaryTreeNode<T> Left;
        public BinaryTreeNode<T> Right;

        public BinaryTreeNode(T value)
        {
            Value = value;
        }
    }

    [TestClass]
    public class TestBinarySearchTree
    {

        [TestMethod]
        public void BinarySearchTree()
        {
            int[] inputIterative = { 7, 9, 12, 10 };
            int[] inputRecursive = { 3, 1, 4, 8 };
            var tree = new BinarySearchTree();

            tree.Add(inputIterative);
            tree.Add(inputRecursive, true);

            // Test iterative search
            Assert.AreEqual(tree.Search(10).Value, 10);

            // Test recursive search
            Assert.AreEqual(tree.Search(8, true).Value, 8);

            // Test null
            Assert.IsNull(tree.Search(99));
            Assert.IsNull(tree.Search(99, true));
        }
    }
}
