using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees;

public class Node<T> where T : IComparable
{
    public T Value;
    public int Weight = 1;
    public Node<T> Left, Right, Parent;
}

public class BinaryTree<T> : IEnumerable<T>
    where T : IComparable
{
    Node<T> binaryTree;

    Node<T> CreateNewNode(T item)
    {
        return new Node<T> { Value = item };
    }

    private void RestoreBinaryTree()
    {
        if (binaryTree != null)
            while (binaryTree.Parent != null)
                binaryTree = binaryTree.Parent;
    }

    public void Add(T item)
    {
        var newNode = CreateNewNode(item);
        if (binaryTree == null)
        {
            binaryTree = newNode;
            return;
        }
        while (binaryTree != null)
        {
            if (item.CompareTo(binaryTree.Value) >= 0)
            {
                if (binaryTree.Right != null)
                {
                    binaryTree.Weight++;
                    binaryTree = binaryTree.Right;
                }
                else
                {
                    newNode.Parent = binaryTree;
                    binaryTree.Weight++;
                    binaryTree.Right = newNode;
                    break;
                }
            }
            else
            {
                if (binaryTree.Left != null)
                {
                    binaryTree.Weight++;
                    binaryTree = binaryTree.Left;
                }
                else
                {
                    newNode.Parent = binaryTree;
                    binaryTree.Weight++;
                    binaryTree.Left = newNode;
                    break;
                }
            }
        }
        RestoreBinaryTree();
    }

    public bool Contains(T item)
    {
        while (binaryTree != null)
        {
            if (item.CompareTo(binaryTree.Value) == 0)
            {
                RestoreBinaryTree();
                return true;
            }

            else if (item.CompareTo(binaryTree.Value) >= 0)
            {
                if (binaryTree.Right != null)
                    binaryTree = binaryTree.Right;
                else
                    break;
            }
            else
            {
                if (binaryTree.Left != null)
                    binaryTree = binaryTree.Left;
                else
                    break;
            }
        }
        RestoreBinaryTree();
        return false;
    }

    public T this[int i]
    {
        get
        {
            i++;

            while (true)
            {
                while (binaryTree.Left != null && binaryTree.Left.Weight >= i)
                    binaryTree = binaryTree.Left;
                if (binaryTree.Left != null)
                    i -= binaryTree.Left.Weight;
                if (i == 1)
                {
                    var res = binaryTree.Value;
                    RestoreBinaryTree();
                    return res;
                }
                i--;
                binaryTree = binaryTree.Right;
            }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var node in NextNode(binaryTree))
            yield return node.Value;
    }

    IEnumerable<Node<T>> NextNode(Node<T> root)
    {
        if (root != null)
        {
            foreach (Node<T> node in NextNode(root.Left))
                yield return node;
            yield return root;
            foreach (Node<T> node in NextNode(root.Right))
                yield return node;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}