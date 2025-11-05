using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using TreeEditor;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;

public class Tree : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField treeInputField;
    public UnityEngine.UI.Button startButton;
    public TextMeshProUGUI resultText;

    private GeneralTree<string> binaryTree;

    void Start()
    {
        // Назначаем обработчик клика на кнопку
        startButton.onClick.AddListener(OnStartButtonClick);

        // Инициализируем дерево
        binaryTree = new GeneralTree<string>();

        if (treeInputField != null)
        {
            treeInputField.text = "a(b(d(g)),c(e,f(h,j)))";
        }
    }

    private void OnStartButtonClick()
    {
        resultText.text = "Создание дерева...\n";

        string inputText = treeInputField.text;
        // Создаем дерево из введенных значений
        binaryTree.BuildTreeFromString(inputText, x => x);

        string widthResult = binaryTree.WidthSearch();
        string depthResult = binaryTree.DepthSearch();

        resultText.text = $"<b>Результаты обхода:</b>\n";
        resultText.text += $"<color=green>В ширину:</color> {widthResult}\n";
        resultText.text += $"<color=blue>В глубину:</color> {depthResult}";
    }


}

//public static int[] toIntList(string[] list)
//{
//    int[] ans = new int[list.Length];

//    for (int i = 0; i < list.Length; i++)
//    {
//        ans[i] = int.Parse(list[i]);
//    }
//    return ans;
//}



//public class BinaryTree<T> where T : IComparable<T>
//{
//    public string WidthSearch()
//    {
//        StringBuilder result = new StringBuilder();

//        if (root == null) return result.ToString();

//        Queue<TreeNode> queue = new Queue<TreeNode>();
//        queue.Enqueue(root);

//        while (queue.Count > 0)
//        {
//            TreeNode current = queue.Dequeue();

//            if (current != null)
//            {
//                result.Append(current.Value + " ");

//                queue.Enqueue(current.Left);
//                queue.Enqueue(current.Right);
//            }
//            else
//            {
//                result.Append("* ");
//            }
//        }

//        return result.ToString().Trim();
//    }

//    public string DepthSearch()
//    {
//        StringBuilder result = new StringBuilder();

//        if (root == null) return result.ToString();

//        Stack<TreeNode> stack = new Stack<TreeNode>();
//        stack.Push(root);

//        while (stack.Count > 0)
//        {
//            TreeNode current = stack.Pop();

//            // Добавляем значение текущего узла в результат
//            if (current != null)
//            {
//                result.Append(current.Value + " ");

//                stack.Push(current.Left);
//                stack.Push(current.Right);
//            }
//            else
//            {
//                result.Append("* ");
//            }
//        }

//        return result.ToString().Trim();
//    }


//    // Структура узла дерева
//    private class TreeNode
//    {
//        public T Value { get; set; }
//        public TreeNode Left { get; set; }
//        public TreeNode Right { get; set; }

//        public TreeNode(T value)
//        {
//            Value = value;
//            Left = null;
//            Right = null;
//        }
//    }

//    private TreeNode root;

//    public BinaryTree()
//    {
//        root = null;
//    }

//    // 1. Вставка элемента (рекурсивный алгоритм)
//    public void Insert(T value)
//    {
//        root = InsertRecursive(root, value);
//    }

//    private TreeNode InsertRecursive(TreeNode node, T value)
//    {
//        // Если текущий узел пуст - создается новый узел
//        if (node == null)
//        {
//            return new TreeNode(value);
//        }

//        // Сравнение значений
//        int comparison = value.CompareTo(node.Value);

//        // Если значение меньше текущего узла - рекурсивно вставляется в левое поддерево
//        if (comparison < 0)
//        {
//            node.Left = InsertRecursive(node.Left, value);
//        }
//        // Если значение больше текущего узла - рекурсивно вставляется в правое поддерево
//        else if (comparison > 0)
//        {
//            node.Right = InsertRecursive(node.Right, value);
//        }
//        // Если значения равны, можно обработать дубликаты (здесь просто игнорируем)

//        return node;
//    }

//    // 2. Поиск элемента (рекурсивный обход)
//    public bool Contains(T value)
//    {
//        return ContainsRecursive(root, value);
//    }

//    private bool ContainsRecursive(TreeNode node, T value)
//    {
//        // Если узел пуст - элемент не найден
//        if (node == null)
//        {
//            return false;
//        }

//        // Сравнение значений
//        int comparison = value.CompareTo(node.Value);

//        // Если значение равно искомому - элемент найден
//        if (comparison == 0)
//        {
//            return true;
//        }
//        // Если значение меньше - поиск в левом поддереве
//        else if (comparison < 0)
//        {
//            return ContainsRecursive(node.Left, value);
//        }
//        // Если значение больше - поиск в правом поддереве
//        else
//        {
//            return ContainsRecursive(node.Right, value);
//        }
//    }}
public class GeneralTree<T> where T : IComparable<T>
{
    // Структура узла дерева с поддержкой нескольких детей
    private class TreeNode
    {
        public T Value { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode>();
        }
    }

    private TreeNode root;

    public GeneralTree()
    {
        root = null;
    }

    // Основной метод построения дерева из скобочной нотации
    public void BuildTreeFromString(string input, Func<string, T> converter)
    {
        if (string.IsNullOrEmpty(input))
            return;

        int index = 0;
        root = ParseNode(input, ref index, converter);
    }

    //private TreeNode ParseNode(string input, ref int index, Func<string, T> converter)
    //{
    //    // Пропускаем пробелы
    //    while (index < input.Length && char.IsWhiteSpace(input[index]))
    //        index++;

    //    if (index >= input.Length)
    //        return null;

    //    // Извлекаем значение узла (до скобки или конца строки)
    //    StringBuilder valueBuilder = new StringBuilder();
    //    while (index < input.Length && input[index] != '(' && input[index] != ')' && input[index] != ',')
    //    {
    //        valueBuilder.Append(input[index]);
    //        index++;
    //    }

    //    string nodeValueStr = valueBuilder.ToString().Trim();
    //    if (string.IsNullOrEmpty(nodeValueStr))
    //        return null;

    //    // Конвертируем строку в тип T
    //    T nodeValue = converter(nodeValueStr);
    //    TreeNode node = new TreeNode(nodeValue);

    //    // Пропускаем пробелы после значения
    //    while (index < input.Length && char.IsWhiteSpace(input[index]))
    //        index++;

    //    // Если следующий символ - открывающая скобка, обрабатываем детей
    //    if (index < input.Length && input[index] == '(')
    //    {
    //        index++; // Пропускаем '('

    //        // Обрабатываем всех детей, пока не дойдем до ')'
    //        while (index < input.Length && input[index] != ')')
    //        {
    //            // Пропускаем пробелы и запятые
    //            while (index < input.Length && (char.IsWhiteSpace(input[index]) || input[index] == ','))
    //                index++;

    //            if (index >= input.Length || input[index] == ')')
    //                break;

    //            // Рекурсивно парсим дочерний узел
    //            TreeNode child = ParseNode(input, ref index, converter);
    //            if (child != null)
    //                node.Children.Add(child);
    //        }

    //        if (index < input.Length && input[index] == ')')
    //            index++; // Пропускаем ')'
    //    }

    //    return node;
    //}

    // Обход в ширину (аналогично вашему WidthSearch)

    private TreeNode ParseNode(string input, ref int index, Func<string, T> converter)
    {
        while (index < input.Length && char.IsWhiteSpace(input[index]))
            index++;

        if (index >= input.Length)
            return null;

        // Если сразу запятая или скобка - возвращаем null
        if (input[index] == ',' || input[index] == ')')
        {
            return null;
        }

        StringBuilder valueBuilder = new StringBuilder();
        while (index < input.Length && input[index] != '(' && input[index] != ')' && input[index] != ',')
        {
            valueBuilder.Append(input[index]);
            index++;
        }

        string nodeValueStr = valueBuilder.ToString().Trim();

        // Если строка пустая после парсинга - это null узел
        if (string.IsNullOrEmpty(nodeValueStr))
            return null;

        try
        {
            T nodeValue = converter(nodeValueStr);
            TreeNode node = new TreeNode(nodeValue);

            // Пропускаем пробелы после значения
            while (index < input.Length && char.IsWhiteSpace(input[index]))
                index++;

            bool hasChildren = false;

            // Если следующий символ - открывающая скобка, обрабатываем детей
            if (index < input.Length && input[index] == '(')
            {
                index++; // Пропускаем '('
                hasChildren = true;

                // Обрабатываем всех детей, пока не дойдем до ')'
                while (index < input.Length && input[index] != ')')
                {
                    // Пропускаем пробелы и запятые
                    while (index < input.Length && (char.IsWhiteSpace(input[index]) || input[index] == ','))
                        index++;

                    if (index >= input.Length || input[index] == ')')
                        break;

                    // Рекурсивно парсим дочерний узел
                    TreeNode child = ParseNode(input, ref index, converter);
                    if (child != null)
                        node.Children.Add(child);
                }

                if (index < input.Length && input[index] == ')')
                    index++; // Пропускаем ')'
            }

            // ⭐ ВОТ ГЛАВНОЕ ИЗМЕНЕНИЕ: добавляем null для листьев
            if (!hasChildren)
            {
                // Добавляем два null-потомка для совместимости с вашим BinaryTree
                node.Children.Add(null);
                node.Children.Add(null);
            }

            return node;
        }
        catch
        {
            return null;
        }
    }


    public string WidthSearch()
    {
        StringBuilder result = new StringBuilder();

        if (root == null) return result.ToString();

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode current = queue.Dequeue();

            if (current != null)
            {
                result.Append(current.Value + " ");

                // Добавляем всех детей в очередь
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
            else
            {
                result.Append("* ");
            }
        }

        return result.ToString();
    }

    // Обход в глубину (аналогично вашему DepthSearch)
    public string DepthSearch()
    {
        StringBuilder result = new StringBuilder();

        if (root == null) return result.ToString();

        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            TreeNode current = stack.Pop();

            if (current != null)
            {
                result.Append(current.Value + " ");

                // Добавляем детей в обратном порядке для правильного обхода
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(current.Children[i]);
                }
            }
            else
            {
                result.Append("* ");
            }
        }

        return result.ToString();
    }

    // Поиск элемента
    public bool Contains(T value)
    {
        return ContainsRecursive(root, value);
    }

    private bool ContainsRecursive(TreeNode node, T value)
    {
        if (node == null)
        {
            return false;
        }

        // Проверяем текущий узел
        if (node.Value.CompareTo(value) == 0)
        {
            return true;
        }

        // Ищем в детях
        foreach (var child in node.Children)
        {
            if (ContainsRecursive(child, value))
            {
                return true;
            }
        }

        return false;
    }

    // Получение высоты дерева
    public int Height()
    {
        return GetHeight(root);
    }

    private int GetHeight(TreeNode node)
    {
        if (node == null)
            return 0;

        int maxChildHeight = 0;
        foreach (var child in node.Children)
        {
            int childHeight = GetHeight(child);
            if (childHeight > maxChildHeight)
                maxChildHeight = childHeight;
        }

        return 1 + maxChildHeight;
    }

    // Количество элементов в дереве
    public int Count()
    {
        return CountRecursive(root);
    }

    private int CountRecursive(TreeNode node)
    {
        if (node == null)
            return 0;

        int count = 1; // текущий узел
        foreach (var child in node.Children)
        {
            count += CountRecursive(child);
        }

        return count;
    }

    // Проверка на пустоту
    public bool IsEmpty
    {
        get { return root == null; }
    }
}



