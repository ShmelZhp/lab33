using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

}

public class BinaryTree<T> where T : IComparable<T>
{
    public void WidthSearch()
    {
        if (root == null) return;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode current = queue.Dequeue();
            if (current.Left != null)
                queue.Enqueue(current.Left);
            if (current.Right != null)
                queue.Enqueue(current.Right);
        }
    }

    public void DepthSearch()
    {
        if (root == null) return;

        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            TreeNode current = stack.Pop();
            if (current.Left != null)
                stack.Push(current.Left);
            if (current.Right != null)
                stack.Push(current.Right);
        }
    }


    // Структура узла дерева
    private class TreeNode
    {
        public T Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private TreeNode root;

    public BinaryTree()
    {
        root = null;
    }

    // 1. Вставка элемента (рекурсивный алгоритм)
    public void Insert(T value)
    {
        root = InsertRecursive(root, value);
    }

    private TreeNode InsertRecursive(TreeNode node, T value)
    {
        // Если текущий узел пуст - создается новый узел
        if (node == null)
        {
            return new TreeNode(value);
        }

        // Сравнение значений
        int comparison = value.CompareTo(node.Value);

        // Если значение меньше текущего узла - рекурсивно вставляется в левое поддерево
        if (comparison < 0)
        {
            node.Left = InsertRecursive(node.Left, value);
        }
        // Если значение больше текущего узла - рекурсивно вставляется в правое поддерево
        else if (comparison > 0)
        {
            node.Right = InsertRecursive(node.Right, value);
        }
        // Если значения равны, можно обработать дубликаты (здесь просто игнорируем)

        return node;
    }

    // 2. Поиск элемента (рекурсивный обход)
    public bool Contains(T value)
    {
        return ContainsRecursive(root, value);
    }

    private bool ContainsRecursive(TreeNode node, T value)
    {
        // Если узел пуст - элемент не найден
        if (node == null)
        {
            return false;
        }

        // Сравнение значений
        int comparison = value.CompareTo(node.Value);

        // Если значение равно искомому - элемент найден
        if (comparison == 0)
        {
            return true;
        }
        // Если значение меньше - поиск в левом поддереве
        else if (comparison < 0)
        {
            return ContainsRecursive(node.Left, value);
        }
        // Если значение больше - поиск в правом поддереве
        else
        {
            return ContainsRecursive(node.Right, value);
        }
    }}

  